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
    public partial class FrmPagos : Form
    {
        public FrmPagos()
        {
            InitializeComponent();
        }

        void CargarReservas()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT reserva_id FROM Reservas WHERE estado='Pendiente'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbReserva.DataSource = dt;
                cmbReserva.DisplayMember = "reserva_id";
                cmbReserva.ValueMember = "reserva_id";
            }
        }

        void CargarMetodos()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT nombre FROM Metodos_Pago WHERE estado='Activo'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbMetodo.DataSource = dt;
                cmbMetodo.DisplayMember = "nombre";
            }
        }

        void CargarDetalle(int reservaId)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT rv.vehiculo_id, v.marca, rv.dias, rv.subtotal
                       FROM Reserva_Vehiculos rv
                       INNER JOIN Vehiculos v ON rv.vehiculo_id = v.vehiculo_id
                       WHERE rv.reserva_id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", reservaId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvDetalle.DataSource = dt;

     
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["subtotal"]);
                }

                txtTotal.Text = total.ToString();
            }
        }




        private void FrmPagos_Load(object sender, EventArgs e)
        {
            CargarReservas();
            CargarMetodos();
            cmbEstado.Text = "Procesado";
            cmbEstado.Enabled = false;
        }

        private void cmbReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReserva.SelectedValue != null)
            {
                int id;

                if (int.TryParse(cmbReserva.SelectedValue.ToString(), out id))
                {
                    CargarDetalle(id);
                }
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
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
                decimal total = Convert.ToDecimal(txtTotal.Text);

                string insertPago = @"INSERT INTO Pagos
(reserva_id, fecha_pago, metodo, total, tipo)
VALUES (@reserva, @fecha, @metodo, @total, 'Alquiler')";
                ;

                MySqlCommand cmdPago = new MySqlCommand(insertPago, con, trans);

                cmdPago.Parameters.AddWithValue("@reserva", reservaId);
                cmdPago.Parameters.AddWithValue("@fecha", dtpFechaPago.Value);
                cmdPago.Parameters.AddWithValue("@metodo", cmbMetodo.Text);
                cmdPago.Parameters.AddWithValue("@total", total);

                cmdPago.ExecuteNonQuery();

                int pagoId = Convert.ToInt32(cmdPago.LastInsertedId);

                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    if (row.Cells["vehiculo_id"].Value == null) continue;

                    int vehiculoId = Convert.ToInt32(row.Cells["vehiculo_id"].Value);
                    decimal monto = Convert.ToDecimal(row.Cells["subtotal"].Value);

                    string insertDetalle = @"INSERT INTO Pago_Detalle
            (pago_id, vehiculo_id, monto)
            VALUES (@pago, @vehiculo, @monto)";

                    MySqlCommand cmdDetalle = new MySqlCommand(insertDetalle, con, trans);

                    cmdDetalle.Parameters.AddWithValue("@pago", pagoId);
                    cmdDetalle.Parameters.AddWithValue("@vehiculo", vehiculoId);
                    cmdDetalle.Parameters.AddWithValue("@monto", monto);

                    cmdDetalle.ExecuteNonQuery();

                    string updateVehiculo = "UPDATE Vehiculos SET disponible=0 WHERE vehiculo_id=@id";
                    MySqlCommand cmdVehiculo = new MySqlCommand(updateVehiculo, con, trans);
                    cmdVehiculo.Parameters.AddWithValue("@id", vehiculoId);
                    cmdVehiculo.ExecuteNonQuery();
                }

                // 🔹 CAMBIAR ESTADO RESERVA
                string updateReserva = "UPDATE Reservas SET estado='Activo' WHERE reserva_id=@id";
                MySqlCommand cmdReserva = new MySqlCommand(updateReserva, con, trans);
                cmdReserva.Parameters.AddWithValue("@id", reservaId);
                cmdReserva.ExecuteNonQuery();

                trans.Commit();

                MessageBox.Show("Pago realizado correctamente");

                dgvDetalle.DataSource = null;
                txtTotal.Clear();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show("Error: " + ex.Message);
            }

            con.Close();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
