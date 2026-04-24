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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Borders;
using iText.Kernel.Colors;

namespace RentCar.Consultas
{
    public partial class FrmEstadoCuenta : Form
    {
        public FrmEstadoCuenta()
        {
            InitializeComponent();
        }

        void CargarEncabezado(int reservaId)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT c.nombre, r.fecha_inicio, r.fecha_fin, r.estado
                       FROM Reservas r
                       INNER JOIN Clientes c ON r.cliente_id = c.cliente_id
                       WHERE r.reserva_id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", reservaId);

                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtCliente.Text = dr["nombre"].ToString();
                    txtFechaInicio.Text = Convert.ToDateTime(dr["fecha_inicio"]).ToShortDateString();
                    txtFechaFin.Text = Convert.ToDateTime(dr["fecha_fin"]).ToShortDateString();
                    txtEstado.Text = dr["estado"].ToString();
                }

                con.Close();
            }
        }

        void CargarDetalle(int reservaId)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT v.marca, rv.dias, rv.precio_unitario, rv.subtotal
                       FROM Reserva_Vehiculos rv
                       INNER JOIN Vehiculos v ON rv.vehiculo_id = v.vehiculo_id
                       WHERE rv.reserva_id = @id";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddWithValue("@id", reservaId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvDetalle.DataSource = dt;
            }
        }

        void CargarTotales(int reservaId)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
SELECT 
    r.reserva_id,

    -- ALQUILER
    IFNULL((
        SELECT SUM(rv.subtotal)
        FROM Reserva_Vehiculos rv
        WHERE rv.reserva_id = r.reserva_id
    ),0) AS total_alquiler,

    -- EXTRAS
    IFNULL((
        SELECT SUM(rd.cargo_extra)
        FROM Recepcion_Detalle rd
        INNER JOIN Recepciones re ON rd.recepcion_id = re.recepcion_id
        WHERE re.reserva_id = r.reserva_id
    ),0) AS total_extras,

    -- PENALIDADES
    IFNULL((
        SELECT SUM(pe.monto)
        FROM Penalidades pe
        WHERE pe.reserva_id = r.reserva_id
    ),0) AS penalidades,

    -- PAGOS SEPARADOS
    IFNULL((
        SELECT SUM(p.total)
        FROM Pagos p
        WHERE p.reserva_id = r.reserva_id AND p.tipo = 'Alquiler'
    ),0) AS pagado_alquiler,

    IFNULL((
        SELECT SUM(p.total)
        FROM Pagos p
        WHERE p.reserva_id = r.reserva_id AND p.tipo = 'Extra'
    ),0) AS pagado_extras,

    IFNULL((
        SELECT SUM(p.total)
        FROM Pagos p
        WHERE p.reserva_id = r.reserva_id AND p.tipo = 'Penalidad'
    ),0) AS pagado_penalidades

FROM Reservas r
WHERE r.reserva_id = @id
";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", reservaId);

                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    decimal total = Convert.ToDecimal(dr["total_alquiler"]);
                    decimal extras = Convert.ToDecimal(dr["total_extras"]);
                    decimal penalidad = Convert.ToDecimal(dr["penalidades"]);

                    decimal pagadoAlquiler = Convert.ToDecimal(dr["pagado_alquiler"]);
                    decimal pagadoExtras = Convert.ToDecimal(dr["pagado_extras"]);
                    decimal pagadoPenalidades = Convert.ToDecimal(dr["pagado_penalidades"]);

                    decimal totalPagado = pagadoAlquiler + pagadoExtras + pagadoPenalidades;

                    decimal balance = (total + extras + penalidad) - totalPagado;

                    txtTotalAlquiler.Text = total.ToString("N2");
                    txtPagado.Text = totalPagado.ToString("N2");
                    txtPenalidades.Text = penalidad.ToString("N2");
                    txtExtras.Text = extras.ToString("N2");
                    txtBalance.Text = balance.ToString("N2");
                }

                con.Close();
            }
        }


        void CargarReservas()
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = "SELECT reserva_id FROM Reservas";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbReserva.DataSource = dt;
                cmbReserva.DisplayMember = "reserva_id";
                cmbReserva.ValueMember = "reserva_id";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmEstadoCuenta_Load(object sender, EventArgs e)
        {
            CargarReservas();

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cmbReserva.SelectedValue == null) return;

            int reservaId = Convert.ToInt32(cmbReserva.SelectedValue);

            CargarEncabezado(reservaId);
            CargarDetalle(reservaId);
            CargarTotales(reservaId);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "PDF (*.pdf)|*.pdf";
            guardar.FileName = "Factura_Alquiler.pdf";

            if (guardar.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (PdfWriter writer = new PdfWriter(guardar.FileName))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    
                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Paragraph titulo = new Paragraph("RENT CAR")
                        .SetFont(bold)
                        .SetFontSize(20)
                        .SetFontColor(ColorConstants.WHITE)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBackgroundColor(ColorConstants.BLACK)
                        .SetPadding(10);

                    doc.Add(titulo);

                    doc.Add(new Paragraph("FACTURA DE ALQUILER")
                        .SetFont(bold)
                        .SetFontSize(14)
                        .SetTextAlignment(TextAlignment.CENTER));

                    doc.Add(new Paragraph(" "));

                    // 📄 INFO CLIENTE
                    Table info = new Table(2).UseAllAvailableWidth();

                    info.AddCell(new Cell().Add(new Paragraph("Cliente:").SetFont(bold)).SetBorder(Border.NO_BORDER));
                    info.AddCell(new Cell().Add(new Paragraph(txtCliente.Text)).SetBorder(Border.NO_BORDER));

                    info.AddCell(new Cell().Add(new Paragraph("Fecha Inicio:").SetFont(bold)).SetBorder(Border.NO_BORDER));
                    info.AddCell(new Cell().Add(new Paragraph(txtFechaInicio.Text)).SetBorder(Border.NO_BORDER));

                    info.AddCell(new Cell().Add(new Paragraph("Fecha Fin:").SetFont(bold)).SetBorder(Border.NO_BORDER));
                    info.AddCell(new Cell().Add(new Paragraph(txtFechaFin.Text)).SetBorder(Border.NO_BORDER));

                    info.AddCell(new Cell().Add(new Paragraph("Estado:").SetFont(bold)).SetBorder(Border.NO_BORDER));
                    info.AddCell(new Cell().Add(new Paragraph(txtEstado.Text)).SetBorder(Border.NO_BORDER));

                    doc.Add(info);

                    doc.Add(new Paragraph(" "));

                    
                    Table tabla = new Table(4).UseAllAvailableWidth();

                    tabla.AddHeaderCell(new Cell().Add(new Paragraph("Vehículo").SetFont(bold)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                    tabla.AddHeaderCell(new Cell().Add(new Paragraph("Días").SetFont(bold)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                    tabla.AddHeaderCell(new Cell().Add(new Paragraph("Precio").SetFont(bold)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                    tabla.AddHeaderCell(new Cell().Add(new Paragraph("Subtotal").SetFont(bold)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));

                    foreach (DataGridViewRow row in dgvDetalle.Rows)
                    {
                        if (row.Cells[0].Value == null) continue;

                        tabla.AddCell(new Paragraph(row.Cells[0].Value.ToString()));
                        tabla.AddCell(new Paragraph(row.Cells[1].Value.ToString()));
                        tabla.AddCell(new Paragraph("RD$ " + row.Cells[2].Value.ToString()));
                        tabla.AddCell(new Paragraph("RD$ " + row.Cells[3].Value.ToString()));
                    }

                    doc.Add(tabla);

                    doc.Add(new Paragraph(" "));

                  
                    Table totales = new Table(2)
                        .SetWidth(250)
                        .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT);

                    totales.AddCell(new Cell().Add(new Paragraph("Total Alquiler:").SetFont(bold)));
                    totales.AddCell(new Cell().Add(new Paragraph("RD$ " + txtTotalAlquiler.Text)));

                    totales.AddCell(new Cell().Add(new Paragraph("Pagado:").SetFont(bold)));
                    totales.AddCell(new Cell().Add(new Paragraph("RD$ " + txtPagado.Text)));

                    totales.AddCell(new Cell().Add(new Paragraph("Penalidades:").SetFont(bold)));
                    totales.AddCell(new Cell().Add(new Paragraph("RD$ " + txtPenalidades.Text)));

                    totales.AddCell(new Cell().Add(new Paragraph("Extras:").SetFont(bold)));
                    totales.AddCell(new Cell().Add(new Paragraph("RD$ " + txtExtras.Text)));

                    Cell lblBalance = new Cell().Add(new Paragraph("BALANCE:").SetFont(bold));
                    Cell valBalance = new Cell().Add(new Paragraph("RD$ " + txtBalance.Text).SetFont(bold));

                    decimal balance = 0;
                    decimal.TryParse(txtBalance.Text, out balance);

                    valBalance.SetFontColor(balance > 0 ? ColorConstants.RED : ColorConstants.GREEN);

                    totales.AddCell(lblBalance);
                    totales.AddCell(valBalance);

                    doc.Add(totales);

                    doc.Add(new Paragraph("\nFecha emisión: " + DateTime.Now.ToShortDateString())
                        .SetFontSize(9)
                        .SetTextAlignment(TextAlignment.RIGHT));

                    doc.Add(new Paragraph("\nGracias por preferirnos 🚗")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(10));
                }

                MessageBox.Show("Factura PDF generada correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error real: " + ex.ToString()); // 🔥 importante
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtPagado_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
