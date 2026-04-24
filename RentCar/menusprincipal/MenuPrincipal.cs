using RentCar.Consultas;
using RentCar.Mantenimientos;
using RentCar.procesos;
using RentCar.Reportes;
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
                panelProcesos.Height = 350;
            }
            else
            {
                panelProcesos.Height = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmVehiculosPorTipoDia frm = new FrmVehiculosPorTipoDia();
            frm.Show();
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

                this.Hide(); 
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

        private void btnPagos_Click(object sender, EventArgs e)
        {

            FrmPagos frm = new FrmPagos();
            frm.Show();
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            FrmDevolucion frm = new FrmDevolucion();
            frm.Show();
        }

        private void btnVehiculosDisponibles_Click(object sender, EventArgs e)
        {
            FrmVehiculosDisponibles frm = new FrmVehiculosDisponibles();
            frm.Show();
        }

        private void btnAlquilerActivos_Click(object sender, EventArgs e)
        {
            FrmVehiculosAlquilados frm = new FrmVehiculosAlquilados();
            frm.Show();
        }

        private void btnHistorialCliente_Click(object sender, EventArgs e)
        {
            FrmHistorialCliente frm = new FrmHistorialCliente();
            frm.ShowDialog();
        }

        private void btnEstadoCuenta_Click(object sender, EventArgs e)
        {
            FrmEstadoCuenta frm = new FrmEstadoCuenta();
            frm.Show();
        }

        private void btnIngresos_Click(object sender, EventArgs e)
        {
            FrmIngresosPorFecha frm = new FrmIngresosPorFecha();
            frm.Show(); 

        }

        private void btnVehiculosRentados_Click(object sender, EventArgs e)
        {
            FrmVehiculosMasAlquilados frm = new FrmVehiculosMasAlquilados();
            frm.Show();
        }

        private void btnClientesFrecuentes_Click(object sender, EventArgs e)
        {
            FrmClientesFrecuentes frm = new FrmClientesFrecuentes();
            frm.Show();
        }

        private void btnPenalidades_Click(object sender, EventArgs e)
        {
            FrmPenalidadesGenerales frm = new FrmPenalidadesGenerales();
            frm.Show();
        }

        private void btnReporteVehiculosAlquilados_Click(object sender, EventArgs e)
        {
            FrmVehiculosFeriados frm = new FrmVehiculosFeriados();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmPagoExtras frm = new FrmPagoExtras();
            frm.Show();
        }

        private void btnPagodePenalidad_Click(object sender, EventArgs e)
        {
            FrmPagoPenalidades frm = new FrmPagoPenalidades();
            frm.Show();
        }
    }
}
