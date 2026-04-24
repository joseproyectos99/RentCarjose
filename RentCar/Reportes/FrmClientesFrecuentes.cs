using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Layout;
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

namespace RentCar.Reportes
{
    public partial class FrmClientesFrecuentes : Form
    {
        public FrmClientesFrecuentes()
        {
            InitializeComponent();
        }

        private void FrmClientesFrecuentes_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFin.Value = DateTime.Today;

            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = Conexion.obtenerConexion())
            {
                string sql = @"SELECT 
                c.cliente_id,
                CONCAT(c.nombre, ' ', c.apellido) AS cliente,
                COUNT(r.reserva_id) AS total_alquileres,
                SUM(rv.subtotal) AS total_gastado
            FROM Reservas r
            INNER JOIN Clientes c ON r.cliente_id = c.cliente_id
            LEFT JOIN Reserva_Vehiculos rv ON r.reserva_id = rv.reserva_id
            WHERE r.fecha_inicio BETWEEN @inicio AND @fin
            GROUP BY c.cliente_id, cliente
            ORDER BY total_alquileres DESC";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                da.SelectCommand.Parameters.AddWithValue("@inicio", dtpInicio.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@fin", dtpFin.Value.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                // 🔥 RANKING
                dt.Columns.Add("Ranking", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Ranking"] = "#" + (i + 1);
                }

                dgvDatos.DataSource = dt;
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "PDF (*.pdf)|*.pdf";
            guardar.FileName = "Clientes_Frecuentes.pdf";

            if (guardar.ShowDialog() != DialogResult.OK) return;

            try
            {
                PdfWriter writer = new PdfWriter(guardar.FileName);
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf);

                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                doc.Add(new Paragraph("REPORTE CLIENTES FRECUENTES")
                    .SetFont(bold)
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER));

                doc.Add(new Paragraph(" "));

                Table tabla = new Table(4).UseAllAvailableWidth();

                tabla.AddHeaderCell("Ranking");
                tabla.AddHeaderCell("Cliente");
                tabla.AddHeaderCell("Alquileres");
                tabla.AddHeaderCell("Total Gastado");

                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (row.Cells["cliente"].Value == null) continue;

                    tabla.AddCell(row.Cells["Ranking"].Value.ToString());
                    tabla.AddCell(row.Cells["cliente"].Value.ToString());
                    tabla.AddCell(row.Cells["total_alquileres"].Value.ToString());
                    tabla.AddCell("RD$ " + row.Cells["total_gastado"].Value.ToString());
                }

                doc.Add(tabla);
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
