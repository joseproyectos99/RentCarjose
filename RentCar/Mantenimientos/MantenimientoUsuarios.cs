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
    public partial class MantenimientoUsuarios : Form
    {
        public MantenimientoUsuarios()
        {
            InitializeComponent();
        }

        public void CargarRoles()
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "SELECT rol_id, nombre FROM Roles WHERE estado='Activo'";
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            cbRol.DataSource = dt;
            cbRol.DisplayMember = "nombre";
            cbRol.ValueMember = "rol_id";

            con.Close();
        }

        public void CargarUsuarios()
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"SELECT 
        u.usuario_id AS Codigo,
        u.nombre,
        u.apellido,
        u.username,
        u.password,
        r.nombre AS rol
        FROM Usuarios u
        INNER JOIN Roles r ON u.rol_id = r.rol_id
        WHERE u.estado='Activo'";

            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvUsuarios.DataSource = dt;

            con.Close();
        }

        public void Limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtBuscar.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MantenimientoUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarRoles();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Complete los campos obligatorios");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            // 🔴 VALIDAR USERNAME DUPLICADO
            string validar = "SELECT COUNT(*) FROM Usuarios WHERE username=@user";
            MySqlCommand cmdVal = new MySqlCommand(validar, con);
            cmdVal.Parameters.AddWithValue("@user", txtUsername.Text);

            int existe = Convert.ToInt32(cmdVal.ExecuteScalar());

            if (existe > 0)
            {
                MessageBox.Show("El username ya existe");
                con.Close();
                return;
            }

            string query = @"INSERT INTO Usuarios
(nombre, apellido, username, password, rol_id)
VALUES(@nom, @ape, @user, @pass, @rol)";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
            cmd.Parameters.AddWithValue("@ape", txtApellido.Text);
            cmd.Parameters.AddWithValue("@user", txtUsername.Text);
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
            cmd.Parameters.AddWithValue("@rol", cbRol.SelectedValue);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Usuario agregado");

            CargarUsuarios();
            Limpiar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione un usuario");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"UPDATE Usuarios SET
    nombre=@nom,
    apellido=@ape,
    username=@user,
    password=@pass,
    rol_id=@rol
    WHERE usuario_id=@id";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
            cmd.Parameters.AddWithValue("@ape", txtApellido.Text);
            cmd.Parameters.AddWithValue("@user", txtUsername.Text);
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
            cmd.Parameters.AddWithValue("@rol", cbRol.SelectedValue);
            cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Usuario modificado");

            CargarUsuarios();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione un usuario");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "UPDATE Usuarios SET estado='Inactivo' WHERE usuario_id=@id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Usuario eliminado");

            CargarUsuarios();
            Limpiar();
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                txtCodigo.Text = dgvUsuarios.CurrentRow.Cells["Codigo"].Value.ToString();
                txtNombre.Text = dgvUsuarios.CurrentRow.Cells["nombre"].Value.ToString();
                txtApellido.Text = dgvUsuarios.CurrentRow.Cells["apellido"].Value.ToString();
                txtUsername.Text = dgvUsuarios.CurrentRow.Cells["username"].Value.ToString();
                txtPassword.Text = dgvUsuarios.CurrentRow.Cells["password"].Value.ToString();

                cbRol.Text = dgvUsuarios.CurrentRow.Cells["rol"].Value.ToString();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
            Limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string campo = "username"; // por defecto

            if (rbCodigo.Checked)
                campo = "usuario_id";
            else if (rbUsuario.Checked)
                campo = "username";
            else if (rbNombre.Checked)
                campo = "nombre";

            string query = $"SELECT u.usuario_id, u.nombre, u.username, r.nombre AS rol, u.estado " +
                           $"FROM Usuarios u " +
                           $"INNER JOIN Roles r ON u.rol_id = r.rol_id " +
                           $"WHERE {campo} LIKE @valor";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@valor", "%" + txtBuscar.Text + "%");

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvUsuarios.DataSource = dt;

            con.Close();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
