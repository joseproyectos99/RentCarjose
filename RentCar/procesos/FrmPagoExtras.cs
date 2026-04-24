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

namespace RentCar.procesos
{
    public partial class FrmPagoExtras : Form
    {

        bool cargando = false;

        public FrmPagoExtras()
        {
            InitializeComponent();
        }

       private void CargarReservas()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
                SELECT DISTINCT r.reserva_id
                FROM Reservas r
                INNER JOIN Recepciones re ON r.reserva_id = re.reserva_id
                INNER JOIN Recepcion_Detalle rd ON re.recepcion_id = rd.recepcion_id
                WHERE rd.cargo_extra > 0 AND rd.pagado = 0";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbReserva.DataSource = dt;
                cmbReserva.DisplayMember = "reserva_id";
                cmbReserva.ValueMember = "reserva_id";
            }
        }


        private void CargarMetodos()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT nombre FROM Metodos_Pago WHERE estado='Activo'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbMetodo.DataSource = dt;
                cmbMetodo.DisplayMember = "nombre";
            }
        }


       private void CargarExtras(int reservaId)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
                SELECT 
                    rd.detalle_recepcion_id,
                    v.marca,
                    rd.combustible_devuelto,
                    rd.kilometraje_devuelto,
                    rd.danos,
                    rd.cargo_extra
                FROM Recepcion_Detalle rd
                INNER JOIN Recepciones re ON rd.recepcion_id = re.recepcion_id
                INNER JOIN Vehiculos v ON rd.vehiculo_id = v.vehiculo_id
                WHERE re.reserva_id = @id AND rd.pagado = 0";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", reservaId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvExtras.DataSource = dt;

                
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["cargo_extra"]);
                }

                txtTotalExtras.Text = total.ToString("N2");
            }
        }



        private void FrmPagoExtras_Load(object sender, EventArgs e)
        {
            cargando = true;

            CargarReservas();
            CargarMetodos();

            dgvExtras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cargando = false;
        }

        private void cmbReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando) return;

            if (cmbReserva.SelectedValue != null)
            {
                int id;
                if (int.TryParse(cmbReserva.SelectedValue.ToString(), out id))
                {
                    CargarExtras(id);
                }
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (cmbReserva.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una reserva");
                return;
            }

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();

                try
                {
                    int reservaId = Convert.ToInt32(cmbReserva.SelectedValue);
                    decimal total = Convert.ToDecimal(txtTotalExtras.Text);

                    string sql = @"INSERT INTO Pagos
                    (reserva_id, fecha_pago, metodo, total, tipo)
                    VALUES (@reserva, @fecha, @metodo, @total, 'Extra')";

                    MySqlCommand cmd = new MySqlCommand(sql, con, trans);

                    cmd.Parameters.AddWithValue("@reserva", reservaId);
                    cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value);
                    cmd.Parameters.AddWithValue("@metodo", cmbMetodo.Text);
                    cmd.Parameters.AddWithValue("@total", total);

                    cmd.ExecuteNonQuery();

                    string update = @"
                    UPDATE Recepcion_Detalle rd
                    INNER JOIN Recepciones re ON rd.recepcion_id = re.recepcion_id
                    SET rd.pagado = 1
                    WHERE re.reserva_id = @id";

                    MySqlCommand cmdUpdate = new MySqlCommand(update, con, trans);
                    cmdUpdate.Parameters.AddWithValue("@id", reservaId);
                    cmdUpdate.ExecuteNonQuery();

                    trans.Commit();

                    MessageBox.Show("Extras pagados correctamente");

                    dgvExtras.DataSource = null;
                    txtTotalExtras.Clear();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

    }
    
}
