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
    public partial class FrmPagoPenalidades : Form
    {
        bool cargando = false;


        public FrmPagoPenalidades()
        {
            InitializeComponent();
        }

        private void CargarReservas()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
                SELECT DISTINCT reserva_id
                FROM Penalidades
                WHERE monto > 0 AND pagado = 0";

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

        private void CargarPenalidades(int reservaId)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
                SELECT 
                    penalidad_id,
                    tipo,
                    descripcion,
                    dias_retraso,
                    monto
                FROM Penalidades
                WHERE reserva_id = @id AND pagado = 0";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", reservaId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPenalidades.DataSource = dt;

                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["monto"]);
                }

                txtTotal.Text = total.ToString("N2");
            }
        }


        private void txtTotalExtras_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmPagoPenalidades_Load(object sender, EventArgs e)
        {
            cargando = true;

            CargarReservas();
            CargarMetodos();

            dgvPenalidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
                    CargarPenalidades(id);
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
                    decimal total = Convert.ToDecimal(txtTotal.Text);

                 
                    string sqlPago = @"
                    INSERT INTO Pagos
                    (reserva_id, fecha_pago, metodo, total, tipo)
                    VALUES (@reserva, @fecha, @metodo, @total, 'Penalidad')";

                    MySqlCommand cmdPago = new MySqlCommand(sqlPago, con, trans);

                    cmdPago.Parameters.AddWithValue("@reserva", reservaId);
                    cmdPago.Parameters.AddWithValue("@fecha", dtpFecha.Value);
                    cmdPago.Parameters.AddWithValue("@metodo", cmbMetodo.Text);
                    cmdPago.Parameters.AddWithValue("@total", total);

                    cmdPago.ExecuteNonQuery();


                    string update = @"
                    UPDATE Penalidades
                    SET pagado = 1
                    WHERE reserva_id = @id AND pagado = 0";

                    MySqlCommand cmdUpdate = new MySqlCommand(update, con, trans);
                    cmdUpdate.Parameters.AddWithValue("@id", reservaId);
                    cmdUpdate.ExecuteNonQuery();

                    trans.Commit();

                    MessageBox.Show("Penalidades pagadas correctamente");

                    dgvPenalidades.DataSource = null;
                    txtTotal.Clear();
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
