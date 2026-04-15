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
    public partial class FrmAlquiler : Form
    {

        MySqlConnection conexion = Conexion.obtenerConexion();

        public FrmAlquiler()
        {
            InitializeComponent();
        }

       private void CargarClientes()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT cliente_id, nombre FROM clientes WHERE estado='Activo'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbCliente.DataSource = dt;
                cmbCliente.DisplayMember = "nombre";
                cmbCliente.ValueMember = "cliente_id";
            }
        }

        private void CargarVehiculos()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT vehiculo_id, marca 
                       FROM vehiculos 
                       WHERE estado='Activo' AND disponible=1";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbVehiculo.DataSource = dt;
                cmbVehiculo.DisplayMember = "marca";
                cmbVehiculo.ValueMember = "vehiculo_id";
            }
        }

        private void CargarEstado()
        {
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Cancelado");

            cmbEstado.SelectedIndex = 0;
        }

        void CargarTipoDia()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT tipo_dia_id, nombre FROM tipo_dia";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbTipoDia.DataSource = dt;
                cmbTipoDia.DisplayMember = "nombre";
                cmbTipoDia.ValueMember = "tipo_dia_id";
            }
        }

        private bool VehiculoDisponible(int vehiculoId)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT COUNT(*) 
                       FROM reservas r
                       INNER JOIN reserva_vehiculos rv ON r.reserva_id = rv.reserva_id
                       WHERE rv.vehiculo_id = @id AND r.estado='Pendiente'";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", vehiculoId);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                return count == 0;
            }
        }

        decimal CalcularPrecio(int vehiculoId)
        {
            decimal total = 0;

            DateTime inicio = dtpInicio.Value;
            DateTime fin = dtpFin.Value;

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                con.Open();

                for (DateTime fecha = inicio; fecha < fin; fecha = fecha.AddDays(1))
                {
                    int tipoDia = (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday) ? 2 : 1;

                    string sql = "SELECT precio FROM precios_vehiculo WHERE vehiculo_id=@v AND tipo_dia_id=@t";

                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@v", vehiculoId);
                    cmd.Parameters.AddWithValue("@t", tipoDia);

                    decimal precio = Convert.ToDecimal(cmd.ExecuteScalar());
                    total += precio;
                }

                con.Close();
            }

            return total;
        }

        void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (row.Cells[4].Value != null)
                    total += Convert.ToDecimal(row.Cells[4].Value);
            }

            txtTotal.Text = total.ToString();
        }

       private void Limpiar()
        {
            txtReservaId.Text = "";
            cmbCliente.SelectedIndex = -1;
            cmbVehiculo.SelectedIndex = -1;

            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = DateTime.Now;
            dtpReserva.Value = DateTime.Now;

            txtTotal.Text = "0";
            txtObservacion.Text = "";

            dgvDetalle.Rows.Clear();
        }

        private bool Validar()
        {
            if (cmbCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione cliente");
                return false;
            }

            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Agregue al menos un vehículo");
                return false;
            }

            if (dtpFin.Value <= dtpInicio.Value)
            {
                MessageBox.Show("Fechas inválidas");
                return false;
            }

            return true;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmAlquiler_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarVehiculos();
            CargarEstado();
            CargarTipoDia();

            txtReservaId.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (cmbVehiculo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un vehículo");
                return;
            }

            int vehiculoId = Convert.ToInt32(cmbVehiculo.SelectedValue);

            // ❌ Evitar duplicados en el grid
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (row.Cells[0].Value != null &&
                    row.Cells[0].Value.ToString() == vehiculoId.ToString())
                {
                    MessageBox.Show("Este vehículo ya fue agregado");
                    return;
                }
            }

            // ❌ Validar fechas
            if (dtpFin.Value <= dtpInicio.Value)
            {
                MessageBox.Show("La fecha fin debe ser mayor que la fecha inicio");
                return;
            }

            // ❌ Validar disponibilidad (extra seguridad)
            if (!VehiculoDisponible(vehiculoId))
            {
                MessageBox.Show("Vehículo no disponible");
                return;
            }

            decimal subtotal = CalcularPrecio(vehiculoId);
            int dias = (dtpFin.Value - dtpInicio.Value).Days;

            dgvDetalle.Rows.Add(vehiculoId, cmbVehiculo.Text, subtotal / dias, dias, subtotal);

            CalcularTotal();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();

                try
                {
                    string sql = @"INSERT INTO reservas
                          (cliente_id, fecha_reserva, fecha_inicio, fecha_fin, estado)
                          VALUES(@cliente, @reserva, @inicio, @fin, @estado)";

                    MySqlCommand cmd = new MySqlCommand(sql, con, trans);

                    cmd.Parameters.AddWithValue("@cliente", cmbCliente.SelectedValue);
                    cmd.Parameters.AddWithValue("@reserva", dtpReserva.Value);
                    cmd.Parameters.AddWithValue("@inicio", dtpInicio.Value);
                    cmd.Parameters.AddWithValue("@fin", dtpFin.Value);
                    cmd.Parameters.AddWithValue("@estado", cmbEstado.Text);

                    cmd.ExecuteNonQuery();

                    int reservaId = Convert.ToInt32(cmd.LastInsertedId);

                    foreach (DataGridViewRow row in dgvDetalle.Rows)
                    {
                        if (row.Cells[0].Value == null) continue;

                        string sqlDet = @"INSERT INTO reserva_vehiculos
            (reserva_id, vehiculo_id, precio_unitario, dias, subtotal)
            VALUES(@reserva, @vehiculo, @precio, @dias, @subtotal)";

                        MySqlCommand cmdDet = new MySqlCommand(sqlDet, con, trans);

                        cmdDet.Parameters.AddWithValue("@reserva", reservaId);
                        cmdDet.Parameters.AddWithValue("@vehiculo", row.Cells[0].Value);
                        cmdDet.Parameters.AddWithValue("@precio", row.Cells[2].Value);
                        cmdDet.Parameters.AddWithValue("@dias", row.Cells[3].Value);
                        cmdDet.Parameters.AddWithValue("@subtotal", row.Cells[4].Value);

                        cmdDet.ExecuteNonQuery();

                        // 🔥 BLOQUEAR VEHÍCULO (IMPORTANTE)
                        string updateVehiculo = "UPDATE Vehiculos SET disponible = 0 WHERE vehiculo_id=@id";
                        MySqlCommand cmdUpdate = new MySqlCommand(updateVehiculo, con, trans);
                        cmdUpdate.Parameters.AddWithValue("@id", row.Cells[0].Value);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    trans.Commit();
                    MessageBox.Show("Guardado");

                    CargarVehiculos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvDetalle_RowsRemoved(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Si se hace click en columna eliminar
            if (dgvDetalle.Columns[e.ColumnIndex].Name == "eliminar")
            {
                dgvDetalle.Rows.RemoveAt(e.RowIndex);



                CalcularTotal();
            }
        }
    }
}
