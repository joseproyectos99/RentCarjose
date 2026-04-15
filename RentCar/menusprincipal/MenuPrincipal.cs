using RentCar.Mantenimientos;
using RentCar.procesos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentCar
{
    public partial class MenuPrincipal : Form
    {
        string rolUsuario;

        public MenuPrincipal(string rol)
        {
            InitializeComponent();
            rolUsuario = rol;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MantenimientoClientes frm = new MantenimientoClientes();
            frm.Show();
        }

        private void OcultarPaneles()
        {
            panelMantenimientos.Height = 0;
            panelProcesos.Height = 0;
            panelConsultas.Height = 0;
            panelReportes.Height = 0;


        }

        private void btnMantenimientos_Click(object sender, EventArgs e)
        {
            if (panelMantenimientos.Height == 0)
            {
                OcultarPaneles();
                panelMantenimientos.Height = 230;
            }
            else
            {
                panelMantenimientos.Height = 0;
            }
        }

        private void btnProcesos_Click(object sender, EventArgs e)
        {

            if (panelProcesos.Height == 0)
            {
                OcultarPaneles();
                panelProcesos.Height = 180;
            }
            else
            {
                panelProcesos.Height = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {

            if (panelConsultas.Height == 0)
            {
                OcultarPaneles();
                panelConsultas.Height = 210;
            }
            else
            {
                panelConsultas.Height = 0;
            }
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            MantenimientoVehiculos frm = new MantenimientoVehiculos();
            frm.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

            if (panelReportes.Height == 0)
            {
                OcultarPaneles();
                panelReportes.Height = 210;
            }
            else
            {
                panelReportes.Height = 0;
            }
        }

        private void panelMantenimientos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnGamas_Click(object sender, EventArgs e)
        {
            MantenimientoGamas frm = new MantenimientoGamas();
            frm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
        "¿Deseas cerrar sesión?",
        "Cerrar sesión",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (r == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();

                this.Hide(); // 🔥 IMPORTANTE: NO usar Close()
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            MantenimientoUsuarios frm = new MantenimientoUsuarios();
            frm.Show();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            if (rolUsuario == "Empleado")
            {
                // 🔴 BLOQUEAR
                btnUsuarios.Enabled = false;
                btnGamas.Enabled = false;
                btnVehiculos.Enabled = false;
                btnIngresos.Enabled = false;
            }
        }

        private void btnAlquiler_Click(object sender, EventArgs e)
        {
            FrmAlquiler frm = new FrmAlquiler();
                frm.Show();
        }
    }
}
