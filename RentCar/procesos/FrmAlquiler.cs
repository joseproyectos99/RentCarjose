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



        void CargarClientes()
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

        void CargarVehiculos()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT vehiculo_id, marca FROM vehiculos WHERE disponible=1";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbVehiculo.DataSource = dt;
                cmbVehiculo.DisplayMember = "marca";
                cmbVehiculo.ValueMember = "vehiculo_id";
            }
        }

        Dictionary<int, decimal> ObtenerPrecios(int vehiculoId)
        {
            Dictionary<int, decimal> precios = new Dictionary<int, decimal>();

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT tipo_dia_id, precio FROM precios_vehiculo WHERE vehiculo_id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", vehiculoId);

                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    precios.Add(
                        Convert.ToInt32(dr["tipo_dia_id"]),
                        Convert.ToDecimal(dr["precio"])
                    );
                }

                con.Close();
            }

            return precios;
        }

        void CalcularDiasYPrecios(int vehiculoId,
    out int dn,
    out int df,
    out int dfer,
    out decimal pn,
    out decimal pf,
    out decimal pfer,
    out decimal subtotal)
        {
            dn = df = dfer = 0;
            pn = pf = pfer = subtotal = 0;

            var precios = ObtenerPrecios(vehiculoId);

            DateTime inicio = dtpInicio.Value.Date;
            DateTime fin = dtpFin.Value.Date;

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                con.Open();

                for (DateTime fecha = inicio; fecha < fin; fecha = fecha.AddDays(1))
                {
                    int tipo = 1;

                    string sqlF = "SELECT COUNT(*) FROM feriados WHERE fecha=@f";
                    MySqlCommand cmdF = new MySqlCommand(sqlF, con);
                    cmdF.Parameters.AddWithValue("@f", fecha);

                    if (Convert.ToInt32(cmdF.ExecuteScalar()) > 0)
                    {
                        tipo = 3;
                        dfer++;
                    }
                    else if (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
                    {
                        tipo = 2;
                        df++;
                    }
                    else
                    {
                        dn++;
                    }

                    decimal precio = precios.ContainsKey(tipo) ? precios[tipo] : 0;

                    if (tipo == 1) pn += precio;
                    if (tipo == 2) pf += precio;
                    if (tipo == 3) pfer += precio;

                    subtotal += precio;
                }

                con.Close();
            }
        }

        void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (row.Cells[7].Value != null)
                    total += Convert.ToDecimal(row.Cells[7].Value);
            }

            txtTotal.Text = total.ToString();
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

            dtpInicio.MinDate = DateTime.Today;
            dtpFin.MinDate = DateTime.Today;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int vehiculoId = Convert.ToInt32(cmbVehiculo.SelectedValue);

            decimal precioVehiculo = ObtenerPrecioVehiculo(vehiculoId);

            int dn, df, dfer;
            decimal pn, pf, pfer, subtotal;

            CalcularDiasYPrecios(vehiculoId, out dn, out df, out dfer, out pn, out pf, out pfer, out subtotal);

            int totalDias = dn + df + dfer;

            dgvDetalle.Rows.Add(
                vehiculoId,
                cmbVehiculo.Text,
                precioVehiculo,
                pn,
                pf,
                pfer,
                totalDias,
                subtotal
            );

            CalcularTotal();

        }

        decimal ObtenerPrecioVehiculo(int vehiculoId)
        {
            decimal precio = 0;

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT precio_diario FROM Vehiculos WHERE vehiculo_id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", vehiculoId);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    precio = Convert.ToDecimal(result);

                con.Close();
            }

            return precio;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
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

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            dtpFin.MinDate = dtpInicio.Value.AddDays(1);

        }
    }
}
