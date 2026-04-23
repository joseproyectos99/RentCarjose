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

namespace RentCar.Consultas
{
    public partial class FrmVehiculosDisponibles : Form
    {

        public FrmVehiculosDisponibles()
        {
            InitializeComponent();
        }

        private void CargarVehiculosDisponibles(DateTime inicio, DateTime fin)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT v.vehiculo_id, v.marca, v.modelo, v.anio, v.placa, v.precio_diario
                       FROM Vehiculos v
                       WHERE v.estado = 'Activo'
                       AND v.vehiculo_id NOT IN (
                           SELECT rv.vehiculo_id
                           FROM Reserva_Vehiculos rv
                           INNER JOIN Reservas r ON rv.reserva_id = r.reserva_id
                           WHERE r.estado IN ('Pendiente','Activa')
                           AND (
                               @inicio BETWEEN r.fecha_inicio AND r.fecha_fin
                               OR
                               @fin BETWEEN r.fecha_inicio AND r.fecha_fin
                           )
                       )";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fin", fin);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvVehiculos.DataSource = dt;
            }
        }

        private void FrmVehiculosDisponibles_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today.AddDays(1);
            dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVehiculos.ReadOnly = true;
            dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVehiculosDisponibles(dtpInicio.Value, dtpFin.Value);

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
