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

namespace RentCar.Mantenimientos
{
    public partial class MantenimientoGamas : Form
    {
        public MantenimientoGamas()
        {
            InitializeComponent();
        }

        public void CargarGamas()
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"SELECT 
                    gama_id AS Codigo,
                    nombre,
                    descripcion
                    FROM Gamas
                    WHERE estado='Activo'";

            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvGamas.DataSource = dt;

            con.Close();
        }

        public void Limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese el nombre de la gama");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "INSERT INTO Gamas(nombre, descripcion) VALUES(@nom, @desc)";
            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
            cmd.Parameters.AddWithValue("@desc", txtDescripcion.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Gama agregada");

            CargarGamas();
            Limpiar();
        }

        private void MantenimientoGamas_Load(object sender, EventArgs e)
        {
          
            CargarGamas();

        }

        private void dgvGamas_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGamas.CurrentRow != null)
            {
                txtCodigo.Text = dgvGamas.CurrentRow.Cells["Codigo"].Value.ToString();
                txtNombre.Text = dgvGamas.CurrentRow.Cells["nombre"].Value.ToString();
                txtDescripcion.Text = dgvGamas.CurrentRow.Cells["descripcion"].Value.ToString();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione una gama");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"UPDATE Gamas SET 
                    nombre=@nom,
                    descripcion=@desc
                    WHERE gama_id=@id";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
            cmd.Parameters.AddWithValue("@desc", txtDescripcion.Text);
            cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Gama modificada");

            CargarGamas();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione una gama");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "UPDATE Gamas SET estado='Inactivo' WHERE gama_id=@id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Gama eliminada");

            CargarGamas();
            Limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() == "")
            {
                CargarGamas();
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string campo = "nombre";

            if (rbCodigo.Checked) campo = "gama_id";
            else if (rbNombre.Checked) campo = "nombre";

            string query = $@"SELECT 
                    gama_id AS Codigo,
                    nombre,
                    descripcion
                    FROM Gamas
                    WHERE {campo} LIKE @valor AND estado='Activo'";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@valor", "%" + txtBuscar.Text + "%");

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvGamas.DataSource = dt;

            con.Close();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarGamas();
            Limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
