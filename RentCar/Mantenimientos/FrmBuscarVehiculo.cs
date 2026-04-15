using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RentCar.Mantenimientos
{
    public partial class FrmBuscarVehiculo : Form
    {
        public string idVehiculo;
        public string marca;
        public string modelo;
        public string placa;
        public string color;
        public string precio;
        public string anio;
        public string gama;
        public string disponible;
        public byte[] imagen;

        public FrmBuscarVehiculo()
        {
            InitializeComponent();
        }

        private void dgvVehiculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSeleccionar.PerformClick();
        }

        public void CargarVehiculos()
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"SELECT 
                v.vehiculo_id AS Codigo,
                v.marca,
                v.modelo,
                v.anio,
                v.placa,
                v.color,
                v.precio_diario,
                v.disponible,
                g.nombre AS gama
                FROM Vehiculos v
                INNER JOIN Gamas g ON v.gama_id = g.gama_id
                WHERE v.estado='Activo'";

            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvVehiculos.DataSource = dt;

            con.Close();
        }

        private void FrmBuscarVehiculo_Load(object sender, EventArgs e)
        {
            CargarVehiculos();

        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"SELECT 
        v.vehiculo_id AS Codigo,
        v.marca,
        v.modelo,
        v.anio,
        v.placa,
        v.color,
        v.precio_diario,
        v.disponible,
        g.nombre AS gama
    FROM Vehiculos v
    INNER JOIN Gamas g ON v.gama_id = g.gama_id
    WHERE v.estado='Activo'
    AND (v.marca LIKE @valor OR v.modelo LIKE @valor OR v.placa LIKE @valor)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@valor", "%" + txtBuscar.Text + "%");

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvVehiculos.DataSource = dt;

            con.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvVehiculos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un vehículo");
                return;
            }

            idVehiculo = dgvVehiculos.CurrentRow.Cells["Codigo"].Value.ToString();
            marca = dgvVehiculos.CurrentRow.Cells["marca"].Value.ToString();
            modelo = dgvVehiculos.CurrentRow.Cells["modelo"].Value.ToString();
            anio = dgvVehiculos.CurrentRow.Cells["anio"].Value.ToString();
            placa = dgvVehiculos.CurrentRow.Cells["placa"].Value.ToString();
            color = dgvVehiculos.CurrentRow.Cells["color"].Value.ToString();
            precio = dgvVehiculos.CurrentRow.Cells["precio_diario"].Value.ToString();
            gama = dgvVehiculos.CurrentRow.Cells["gama"].Value.ToString();
            disponible = dgvVehiculos.CurrentRow.Cells["disponible"].Value.ToString();

            // 🔥 TRAER IMAGEN
            int id = Convert.ToInt32(idVehiculo);

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "SELECT imagen FROM Vehiculos WHERE vehiculo_id = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (reader["imagen"] != DBNull.Value)
                {
                    imagen = (byte[])reader["imagen"];
                }
                else
                {
                    imagen = null;
                }
            }

            con.Close();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
