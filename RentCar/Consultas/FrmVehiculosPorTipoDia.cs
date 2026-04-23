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
    public partial class FrmVehiculosPorTipoDia : Form
    {
        public FrmVehiculosPorTipoDia()
        {
            InitializeComponent();
        }

        void CargarDatos()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
        SELECT 
            r.reserva_id,
            v.marca AS vehiculo,
            DATE_ADD(r.fecha_inicio, INTERVAL n DAY) AS fecha,

            CASE 
                WHEN f.fecha IS NOT NULL THEN 'Feriado'
                WHEN DAYOFWEEK(DATE_ADD(r.fecha_inicio, INTERVAL n DAY)) IN (1,7) THEN 'Fin de Semana'
                ELSE 'Normal'
            END AS tipo_dia,

            pv.precio

        FROM Reservas r
        INNER JOIN Reserva_Vehiculos rv ON r.reserva_id = rv.reserva_id
        INNER JOIN Vehiculos v ON rv.vehiculo_id = v.vehiculo_id

        -- 🔥 GENERAR DIAS DEL ALQUILER
        JOIN (
            SELECT 0 n UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 
            UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9
        ) AS dias

        ON DATE_ADD(r.fecha_inicio, INTERVAL n DAY) < r.fecha_fin

        LEFT JOIN Feriados f 
        ON DATE_ADD(r.fecha_inicio, INTERVAL n DAY) = f.fecha

        INNER JOIN Precios_Vehiculo pv 
        ON pv.vehiculo_id = v.vehiculo_id
        AND pv.tipo_dia_id = 
            CASE 
                WHEN f.fecha IS NOT NULL THEN 3
                WHEN DAYOFWEEK(DATE_ADD(r.fecha_inicio, INTERVAL n DAY)) IN (1,7) THEN 2
                ELSE 1
            END

        WHERE DATE_ADD(r.fecha_inicio, INTERVAL n DAY) 
        BETWEEN @inicio AND @fin
        ";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddWithValue("@inicio", dtpInicio.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@fin", dtpFin.Value.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvDatos.DataSource = dt;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmVehiculosPorTipoDia_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Today.AddDays(-7);
            dtpFin.Value = DateTime.Today;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();

        }
    }
}
