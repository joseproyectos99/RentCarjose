using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;
using iText.Layout.Borders;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace RentCar.Reportes
{
    public partial class FrmIngresosPorFecha : Form
    {
        public FrmIngresosPorFecha()
        {
            InitializeComponent();
        }

        private void FrmIngresosPorFecha_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Today.AddDays(-7);
            dtpFin.Value = DateTime.Today;

            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT 
                        p.pago_id,
                        p.fecha_pago,
                        c.nombre AS cliente,
                        p.metodo,
                        p.total
                       FROM Pagos p
                       INNER JOIN Reservas r ON p.reserva_id = r.reserva_id
                       INNER JOIN Clientes c ON r.cliente_id = c.cliente_id
                       WHERE p.fecha_pago BETWEEN @inicio AND @fin";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                da.SelectCommand.Parameters.AddWithValue("@inicio", dtpInicio.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@fin", dtpFin.Value.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvDatos.DataSource = dt;

                // 🔥 CALCULAR TOTAL
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["total"]);
                }

                txtTotal.Text = total.ToString("N2");
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "PDF (*.pdf)|*.pdf";
            guardar.FileName = "Ingresos.pdf";

            if (guardar.ShowDialog() != DialogResult.OK) return;

            try
            {
                PdfWriter writer = new PdfWriter(guardar.FileName);
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf);

                var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                doc.Add(new Paragraph("REPORTE DE INGRESOS")
                    .SetFont(bold)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(16));

                doc.Add(new Paragraph($"Desde: {dtpInicio.Value.ToShortDateString()}  Hasta: {dtpFin.Value.ToShortDateString()}"));

                doc.Add(new Paragraph(" "));

                Table tabla = new Table(5).UseAllAvailableWidth();

                tabla.AddHeaderCell("ID");
                tabla.AddHeaderCell("Fecha");
                tabla.AddHeaderCell("Cliente");
                tabla.AddHeaderCell("Método");
                tabla.AddHeaderCell("Total");

                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (row.Cells[0].Value == null) continue;

                    tabla.AddCell(row.Cells[0].Value.ToString());
                    tabla.AddCell(row.Cells[1].Value.ToString());
                    tabla.AddCell(row.Cells[2].Value.ToString());
                    tabla.AddCell(row.Cells[3].Value.ToString());
                    tabla.AddCell("RD$ " + row.Cells[4].Value.ToString());
                }

                doc.Add(tabla);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("TOTAL: RD$ " + txtTotal.Text)
                    .SetFont(bold)
                    .SetTextAlignment(TextAlignment.RIGHT));

                doc.Close();

                MessageBox.Show("PDF generado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
