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

namespace RentCar
{
    public partial class Form1 : Form
    {
        MySqlConnection conexion = new MySqlConnection("server=localhost;database=rentcar;uid=root;pwd=0725;");

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();

                string query = @"SELECT u.nombre, r.nombre AS rol 
                         FROM Usuarios u
                         INNER JOIN Roles r ON u.rol_id = r.rol_id
                         WHERE u.username=@user 
                         AND u.password=@pass 
                         AND u.estado='Activo'";

                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@user", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string nombre = reader["nombre"].ToString();
                    string rol = reader["rol"].ToString();

                    MessageBox.Show("Bienvenido " + nombre + " - Rol: " + rol);

                    MenuPrincipal menu = new MenuPrincipal(rol);
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
