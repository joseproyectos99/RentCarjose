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
    public partial class MantenimientoClientes : Form
    {

        public MantenimientoClientes()
        {
            InitializeComponent();
        }

        public void CargarClientes()
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"SELECT cliente_id AS Codigo, nombre, apellido, cedula, telefono, email,  direccion FROM Clientes WHERE estado='Activo'";
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvClientes.DataSource = dt;

            con.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void Limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
        }

        private void dgvClientes_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvClientes.CurrentRow != null)
            {
                txtCodigo.Text = dgvClientes.CurrentRow.Cells["Codigo"].Value.ToString();
                txtNombre.Text = dgvClientes.CurrentRow.Cells["nombre"].Value.ToString();
                txtApellido.Text = dgvClientes.CurrentRow.Cells["apellido"].Value.ToString();
                txtCedula.Text = dgvClientes.CurrentRow.Cells["cedula"].Value.ToString();
                txtTelefono.Text = dgvClientes.CurrentRow.Cells["telefono"].Value.ToString();
                txtEmail.Text = dgvClientes.CurrentRow.Cells["email"].Value.ToString();
                txtDireccion.Text = dgvClientes.CurrentRow.Cells["direccion"].Value.ToString();
            }
        }

        private void MantenimientoClientes_Load(object sender, EventArgs e)
        {

            CargarClientes();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese el nombre");
                txtNombre.Focus();
                return;
            }

            if (txtApellido.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese el apellido");
                txtApellido.Focus();
                return;
            }

            if (txtCedula.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese la cédula");
                txtCedula.Focus();
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();



            string query = @"INSERT INTO Clientes(nombre, apellido, cedula, telefono, email, direccion) 
            VALUES(@nom, @ape, @ced, @tel, @email, @dir)";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
            cmd.Parameters.AddWithValue("@ape", txtApellido.Text);
            cmd.Parameters.AddWithValue("@ced", txtCedula.Text);
            cmd.Parameters.AddWithValue("@tel", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@dir", txtDireccion.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Cliente agregado");

            CargarClientes();
            Limpiar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "UPDATE Clientes SET nombre=@nom, apellido=@ape, cedula=@ced, telefono=@tel, email=@email, direccion=@dir WHERE cliente_id=@id";
            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
            cmd.Parameters.AddWithValue("@ape", txtApellido.Text);
            cmd.Parameters.AddWithValue("@ced", txtCedula.Text);
            cmd.Parameters.AddWithValue("@tel", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@dir", txtDireccion.Text);
            cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

            int filas = cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Cliente modificado");

            CargarClientes();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "UPDATE Clientes SET estado='Inactivo' WHERE cliente_id=@id";
            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

            int filas = cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("cliente eliminado " + filas);

            CargarClientes();
            Limpiar();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarClientes();
            Limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string campo = "nombre";

            if (rbCodigo.Checked) campo = "cliente_id";
            else if (rbNombre.Checked) campo = "nombre";
            else if (rbDireccion.Checked) campo = "direccion";
            else if (rbCedula.Checked) campo = "cedula";
            else if (rbEmail.Checked) campo = "email";

            string query = $@"SELECT 
                    cliente_id AS Codigo,
                    nombre,
                    apellido,
                    cedula,
                    telefono,
                    email,
                    direccion
                    FROM Clientes 
                    WHERE {campo} LIKE @valor 
                    AND estado='Activo'";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@valor", "%" + txtBuscar.Text + "%");

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvClientes.DataSource = dt;

            con.Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
