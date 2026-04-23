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
    public partial class FrmHistorialCliente : Form
    {
        public FrmHistorialCliente()
        {
            InitializeComponent();
        }


        void CargarClientes()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT cliente_id, nombre FROM Clientes WHERE estado='Activo'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbCliente.DataSource = dt;
                cmbCliente.DisplayMember = "nombre";
                cmbCliente.ValueMember = "cliente_id";
            }
        }

        void CalcularTotales(DataTable dt)
        {
            decimal totalPagado = 0;
            decimal totalPendiente = 0;
            decimal totalPenalidades = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal alquiler = Convert.ToDecimal(row["total_alquiler"]);
                decimal pagado = Convert.ToDecimal(row["total_pagado"]);
                decimal penal = Convert.ToDecimal(row["penalidades"]);

                totalPagado += pagado;
                totalPenalidades += penal;

                decimal pendiente = alquiler - pagado;
                totalPendiente += pendiente;
            }

            txtTotalPagado.Text = totalPagado.ToString("N2");
            txtTotalPendiente.Text = totalPendiente.ToString("N2");
            txtTotalPenalidades.Text = totalPenalidades.ToString("N2");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void FrmHistorialCliente_Load(object sender, EventArgs e)
        {
            CargarClientes();

            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
        SELECT 
            r.reserva_id,
            r.fecha_inicio,
            r.fecha_fin,
            r.estado,
            IFNULL(SUM(rv.subtotal),0) AS total_alquiler,
            IFNULL(SUM(p.total),0) AS total_pagado,
            IFNULL(SUM(pe.monto),0) AS penalidades
        FROM Reservas r
        LEFT JOIN Reserva_Vehiculos rv ON r.reserva_id = rv.reserva_id
        LEFT JOIN Pagos p ON r.reserva_id = p.reserva_id
        LEFT JOIN Penalidades pe ON r.reserva_id = pe.reserva_id
        WHERE r.cliente_id = @cliente
        AND r.fecha_inicio BETWEEN @desde AND @hasta
        GROUP BY r.reserva_id
        ";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cliente", cmbCliente.SelectedValue);
                cmd.Parameters.AddWithValue("@desde", dtpDesde.Value.Date);
                cmd.Parameters.AddWithValue("@hasta", dtpHasta.Value.Date);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvHistorial.DataSource = dt;

                CalcularTotales(dt);
            }
        }
    }
}
