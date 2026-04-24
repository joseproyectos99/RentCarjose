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
    public partial class FrmVehiculosFeriados : Form
    {
        public FrmVehiculosFeriados()
        {
            InitializeComponent();
        }

        private void FrmVehiculosFeriados_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFin.Value = DateTime.Today;

            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"
        SELECT 
            CASE 
                WHEN f.fecha IS NOT NULL THEN 'Feriado'
                ELSE 'No Feriado'
            END AS tipo_dia,
            COUNT(rv.vehiculo_id) AS total_alquileres,
            SUM(rv.subtotal) AS total_generado
        FROM Reservas r
        INNER JOIN Reserva_Vehiculos rv ON r.reserva_id = rv.reserva_id
        LEFT JOIN Feriados f ON r.fecha_inicio = f.fecha
        WHERE r.fecha_inicio BETWEEN @inicio AND @fin
        GROUP BY tipo_dia";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                da.SelectCommand.Parameters.AddWithValue("@inicio", dtpInicio.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@fin", dtpFin.Value.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvDatos.DataSource = dt;
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "PDF (*.pdf)|*.pdf";
            guardar.FileName = "Reporte_Feriados.pdf";

            if (guardar.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (PdfWriter writer = new PdfWriter(guardar.FileName))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    doc.Add(new Paragraph("REPORTE DÍAS FERIADOS")
                        .SetFont(bold)
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER));

                    doc.Add(new Paragraph(" "));

                    Table tabla = new Table(3).UseAllAvailableWidth();

                    tabla.AddHeaderCell("Tipo Día");
                    tabla.AddHeaderCell("Cantidad");
                    tabla.AddHeaderCell("Total");

                    foreach (DataGridViewRow row in dgvDatos.Rows)
                    {
                        if (row.Cells[0].Value == null) continue;

                        tabla.AddCell(row.Cells[0].Value.ToString());
                        tabla.AddCell(row.Cells[1].Value.ToString());
                        tabla.AddCell("RD$ " + row.Cells[2].Value.ToString());
                    }

                    doc.Add(tabla);
                }

                MessageBox.Show("PDF generado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
