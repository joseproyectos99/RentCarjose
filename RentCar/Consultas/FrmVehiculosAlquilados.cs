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
    public partial class FrmVehiculosAlquilados : Form
    {
        public FrmVehiculosAlquilados()
        {
            InitializeComponent();
        }

        void CargarVehiculosAlquilados()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
                SELECT 
                    v.vehiculo_id,
                    v.marca,
                    v.modelo,
                    v.placa,
                    r.reserva_id,
                    CONCAT(c.nombre, ' ', c.apellido) AS cliente,
                    r.fecha_inicio,
                    r.fecha_fin,
                    r.estado
                FROM Vehiculos v
                INNER JOIN Reserva_Vehiculos rv ON v.vehiculo_id = rv.vehiculo_id
                INNER JOIN Reservas r ON rv.reserva_id = r.reserva_id
                INNER JOIN Clientes c ON r.cliente_id = c.cliente_id
                WHERE r.estado IN ('Pendiente','Activa')
                ";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAlquilados.DataSource = dt;
            }
        }
    



private void FrmVehiculosAlquilados_Load(object sender, EventArgs e)
        {
            CargarVehiculosAlquilados();

        }
    }
}
