using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentCar.procesos
{
    public partial class FrmDevolucion : Form
    {
        bool cargando = false;
        public FrmDevolucion()
        {
            InitializeComponent();
        }

       private  void CargarReservas()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT reserva_id FROM Reservas WHERE estado='Activo'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbReserva.DataSource = dt;
                cmbReserva.DisplayMember = "reserva_id";
                cmbReserva.ValueMember = "reserva_id";
            }
        }

        void CargarDetalle(int reservaId)
        {
            dgvDetalle.Rows.Clear();

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT rv.vehiculo_id, v.marca
                       FROM Reserva_Vehiculos rv
                       INNER JOIN Vehiculos v ON rv.vehiculo_id = v.vehiculo_id
                       WHERE rv.reserva_id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", reservaId);

                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    dgvDetalle.Rows.Add(
                        Convert.ToInt32(dr["vehiculo_id"]),
                        dr["marca"].ToString(),
                        100,   // 🔥 combustible por defecto (lleno)
                        0,     // 🔥 km por defecto
                        "",    // daños
                        0      // cargo extra
                    );
                }

                con.Close();
            }
        }

        decimal CalcularPenalidad(int reservaId, DateTime fechaDevolucion)
        {
            decimal penalidad = 0;

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                con.Open();

                string sql = "SELECT fecha_fin FROM Reservas WHERE reserva_id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", reservaId);

                DateTime fechaFin = Convert.ToDateTime(cmd.ExecuteScalar());

                if (fechaDevolucion > fechaFin)
                {
                    int diasRetraso = (fechaDevolucion - fechaFin).Days;
                    penalidad = diasRetraso * 1000; // 🔥 tarifa
                }

                con.Close();
            }

            return penalidad;
        }



        private void FrmDevolucion_Load(object sender, EventArgs e)
        {
            cargando = true;

            CargarReservas();

            dtpFechaRecepcion.Value = DateTime.Now;

            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDetalle.DataSource = null;

            
            dgvDetalle.AutoGenerateColumns = false;

            dgvDetalle.Columns.Clear();

            dgvDetalle.Columns.Add("vehiculo_id", "ID");
            dgvDetalle.Columns["vehiculo_id"].Visible = false;

            dgvDetalle.Columns.Add("marca", "Vehículo");

            dgvDetalle.Columns.Add("Combustible", "Combustible (%)");
            dgvDetalle.Columns.Add("Kilometraje", "KM");
            dgvDetalle.Columns.Add("Daños", "Daños");
            dgvDetalle.Columns.Add("CargoExtra", "Cargo Extra");

            dgvDetalle.CellEndEdit += dgvDetalle_CellEndEdit;


            cargando = false;

           


        }

        private void btnGuardarDevolucion_Click(object sender, EventArgs e)
        {
            if (cmbReserva.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una reserva");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            MySqlTransaction trans = con.BeginTransaction();

            try
            {
                int reservaId = Convert.ToInt32(cmbReserva.SelectedValue);

                decimal penalidad = CalcularPenalidad(reservaId, dtpFechaRecepcion.Value);

               
                string insertRecepcion = @"INSERT INTO Recepciones
        (reserva_id, fecha_recepcion, observaciones)
        VALUES (@reserva, @fecha, @obs)";

                MySqlCommand cmdRec = new MySqlCommand(insertRecepcion, con, trans);

                cmdRec.Parameters.AddWithValue("@reserva", reservaId);
                cmdRec.Parameters.AddWithValue("@fecha", dtpFechaRecepcion.Value);
                cmdRec.Parameters.AddWithValue("@obs", txtObservaciones.Text);

                cmdRec.ExecuteNonQuery();

                int recepcionId = Convert.ToInt32(cmdRec.LastInsertedId);

                decimal totalExtra = 0;

                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    if (row.Cells["vehiculo_id"].Value == null) continue;

                    int vehiculoId = Convert.ToInt32(row.Cells["vehiculo_id"].Value);

                    decimal combustible = Convert.ToDecimal(row.Cells["Combustible"].Value ?? 0);
                    int km = Convert.ToInt32(row.Cells["Kilometraje"].Value ?? 0);
                    string danos = row.Cells["Daños"].Value?.ToString();

                    decimal cargo = 0;

               
                    if (combustible < 100)
                    {
                        cargo += 500;
                    }

                 
                    if (!string.IsNullOrEmpty(danos))
                    {
                        cargo += 2000;
                    }

                    int limiteKm = 100;
                    if (km > limiteKm)
                    {
                        int kmExtra = km - limiteKm;
                        cargo += kmExtra * 10;
                    }

                    totalExtra += cargo;

                    string insertDetalle = @"INSERT INTO Recepcion_Detalle
            (recepcion_id, vehiculo_id, combustible_devuelto, kilometraje_devuelto, danos, cargo_extra)
            VALUES (@rec, @vehiculo, @comb, @km, @danos, @cargo)";

                    MySqlCommand cmdDet = new MySqlCommand(insertDetalle, con, trans);

                    cmdDet.Parameters.AddWithValue("@rec", recepcionId);
                    cmdDet.Parameters.AddWithValue("@vehiculo", vehiculoId);
                    cmdDet.Parameters.AddWithValue("@comb", combustible);
                    cmdDet.Parameters.AddWithValue("@km", km);
                    cmdDet.Parameters.AddWithValue("@danos", danos);
                    cmdDet.Parameters.AddWithValue("@cargo", cargo);

                    cmdDet.ExecuteNonQuery();

                    
                    string updateVehiculo = "UPDATE Vehiculos SET disponible=1 WHERE vehiculo_id=@id";
                    MySqlCommand cmdVeh = new MySqlCommand(updateVehiculo, con, trans);
                    cmdVeh.Parameters.AddWithValue("@id", vehiculoId);
                    cmdVeh.ExecuteNonQuery();
                }

             
                if (penalidad > 0)
                {
                    string insertPenalidad = @"INSERT INTO Penalidades
            (reserva_id, tipo, descripcion, dias_retraso, monto)
            VALUES (@reserva, 'Retraso', 'Entrega tardía', @dias, @monto)";

                    MySqlCommand cmdPen = new MySqlCommand(insertPenalidad, con, trans);

                    string sqlFecha = "SELECT fecha_fin FROM Reservas WHERE reserva_id=@id";
                    MySqlCommand cmdFecha = new MySqlCommand(sqlFecha, con, trans);
                    cmdFecha.Parameters.AddWithValue("@id", reservaId);

                    DateTime fechaFin = Convert.ToDateTime(cmdFecha.ExecuteScalar());

                    int dias = (dtpFechaRecepcion.Value - fechaFin).Days;
                    cmdPen.Parameters.AddWithValue("@reserva", reservaId);
                    cmdPen.Parameters.AddWithValue("@dias", dias);
                    cmdPen.Parameters.AddWithValue("@monto", penalidad);

                    cmdPen.ExecuteNonQuery();
                }

               
                string updateReserva = "UPDATE Reservas SET estado='Finalizado' WHERE reserva_id=@id";
                MySqlCommand cmdRes = new MySqlCommand(updateReserva, con, trans);
                cmdRes.Parameters.AddWithValue("@id", reservaId);
                cmdRes.ExecuteNonQuery();

                trans.Commit();

                MessageBox.Show($"Devolución completada\nExtra: {totalExtra + penalidad}");

                dgvDetalle.DataSource = null;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show("Error: " + ex.Message);
            }

            con.Close();
        }

        private void cmbReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando) return;

            if (cmbReserva.SelectedValue != null)
            {
                int id;
                if (int.TryParse(cmbReserva.SelectedValue.ToString(), out id))
                {
                    CargarDetalle(id);
                }
            }
        }

        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDetalle.Rows[e.RowIndex];

            if (row.Cells["Combustible"].Value == null &&
                row.Cells["Daños"].Value == null)
                return;

            decimal combustible = 100;
            string danos = "";
            int km = 0;
            decimal cargo = 0;

          
            if (row.Cells["Combustible"].Value != null)
            {
                decimal.TryParse(row.Cells["Combustible"].Value.ToString(), out combustible);
            }

            if (row.Cells["Daños"].Value != null)
            {
                danos = row.Cells["Daños"].Value.ToString();
            }

            if (row.Cells["Kilometraje"].Value != null)
            {
                int.TryParse(row.Cells["Kilometraje"].Value.ToString(), out km);

            }

            if (combustible < 100)
                cargo += 500;

            if (!string.IsNullOrEmpty(danos))
                cargo += 2000;

            int limiteKm = 100; 
            if (km > limiteKm)
            {
                int kmExtra = km - limiteKm;
                cargo += kmExtra * 10; 
            }


            row.Cells["CargoExtra"].Value = cargo;
        }
    }
}
