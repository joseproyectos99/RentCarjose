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
    public partial class FrmPenalidadesGenerales : Form
    {
        public FrmPenalidadesGenerales()
        {
            InitializeComponent();
        }

        private void FrmPenalidadesGenerales_Load(object sender, EventArgs e)
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
            tipo,
            COUNT(*) AS cantidad,
            SUM(monto) AS total_monto
        FROM Penalidades
        WHERE fecha_registro BETWEEN @inicio AND @fin
        GROUP BY tipo
        ORDER BY total_monto DESC";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                da.SelectCommand.Parameters.AddWithValue("@inicio", dtpInicio.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@fin", dtpFin.Value.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("Ranking", typeof(string));
                dt.Columns["Ranking"].SetOrdinal(0);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Ranking"] = "#" + (i + 1);
                }

                dgvDatos.DataSource = dt;

                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["total_monto"]);
                }

                txtTotalPenalidades.Text = "RD$ " + total.ToString();
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "PDF (*.pdf)|*.pdf";
            guardar.FileName = "Penalidades.pdf";

            if (guardar.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (PdfWriter writer = new PdfWriter(guardar.FileName))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    doc.Add(new Paragraph("REPORTE DE PENALIDADES")
                        .SetFont(bold)
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER));

                    doc.Add(new Paragraph($"Desde: {dtpInicio.Value.ToShortDateString()}  Hasta: {dtpFin.Value.ToShortDateString()}"));

                    doc.Add(new Paragraph(" "));

                    Table tabla = new Table(4).UseAllAvailableWidth();

                    tabla.AddHeaderCell("Ranking");
                    tabla.AddHeaderCell("Tipo");
                    tabla.AddHeaderCell("Cantidad");
                    tabla.AddHeaderCell("Total");

                    foreach (DataGridViewRow row in dgvDatos.Rows)
                    {
                        if (row.Cells["tipo"].Value == null) continue;

                        tabla.AddCell(row.Cells["Ranking"].Value.ToString());
                        tabla.AddCell(row.Cells["tipo"].Value.ToString());
                        tabla.AddCell(row.Cells["cantidad"].Value.ToString());
                        tabla.AddCell("RD$ " + row.Cells["total_monto"].Value.ToString());
                    }

                    doc.Add(tabla);

                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("TOTAL GENERAL: " + txtTotalPenalidades.Text)
                        .SetFont(bold)
                        .SetTextAlignment(TextAlignment.RIGHT));
                }

                MessageBox.Show("PDF generado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
