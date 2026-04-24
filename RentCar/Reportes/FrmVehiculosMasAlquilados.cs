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
    public partial class FrmVehiculosMasAlquilados : Form
    {
        public FrmVehiculosMasAlquilados()
        {
            InitializeComponent();
        }

        private void FrmVehiculosMasAlquilados_Load(object sender, EventArgs e)
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
                v.vehiculo_id,
                v.marca,
                COUNT(rv.vehiculo_id) AS veces_alquilado,
                SUM(rv.subtotal) AS total_generado
               FROM Reserva_Vehiculos rv
               INNER JOIN Vehiculos v ON rv.vehiculo_id = v.vehiculo_id
               INNER JOIN Reservas r ON rv.reserva_id = r.reserva_id
               WHERE r.fecha_inicio BETWEEN @inicio AND @fin
               GROUP BY v.vehiculo_id, v.marca
               ORDER BY veces_alquilado DESC";

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
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "PDF (*.pdf)|*.pdf";
            guardar.FileName = "VehiculosMasAlquilados.pdf";

            if (guardar.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (PdfWriter writer = new PdfWriter(guardar.FileName))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    doc.Add(new Paragraph("VEHÍCULOS MÁS ALQUILADOS")
                        .SetFont(bold)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(16));

                    doc.Add(new Paragraph($"Desde: {dtpInicio.Value.ToShortDateString()}  Hasta: {dtpFin.Value.ToShortDateString()}"));

                    doc.Add(new Paragraph(" "));

                    Table tabla = new Table(5).UseAllAvailableWidth();

                    tabla.AddHeaderCell("Ranking");
                    tabla.AddHeaderCell("ID");
                    tabla.AddHeaderCell("Vehículo");
                    tabla.AddHeaderCell("Veces");
                    tabla.AddHeaderCell("Total");

                    foreach (DataGridViewRow row in dgvDatos.Rows)
                    {
                        if (row.Cells["vehiculo_id"].Value == null) continue;

                        tabla.AddCell(row.Cells["Ranking"].Value.ToString());
                        tabla.AddCell(row.Cells["vehiculo_id"].Value.ToString());
                        tabla.AddCell(row.Cells["marca"].Value.ToString());
                        tabla.AddCell(row.Cells["veces_alquilado"].Value.ToString());
                        tabla.AddCell("RD$ " + row.Cells["total_generado"].Value.ToString());
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
