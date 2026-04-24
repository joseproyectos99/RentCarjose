using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentCar.Mantenimientos
{
    public partial class MantenimientoVehiculos : Form
    {
        byte[] imagenBytes;
        bool cargandoDesdeBusqueda = false;

        public MantenimientoVehiculos()
        {
            InitializeComponent();
        }

        public void CargarGamas()
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "SELECT gama_id, nombre FROM Gamas WHERE estado='Activo'";
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            cbGama.DataSource = dt;
            cbGama.DisplayMember = "nombre";
            cbGama.ValueMember = "gama_id";

            con.Close();
        }

        public void CargarVehiculos()
        {
            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = @"SELECT 
        v.vehiculo_id AS Codigo,
        v.marca,
        v.modelo,
        v.anio,
        v.placa,
        v.color,
        v.precio_diario,
        v.disponible,
        g.nombre AS gama
        FROM Vehiculos v
        INNER JOIN Gamas g ON v.gama_id = g.gama_id
        WHERE v.estado='Activo'";

            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvVehiculos.DataSource = dt;

            con.Close();

            foreach (DataGridViewRow row in dgvVehiculos.Rows)
            {
                if (row.Cells["disponible"].Value != null)
                {
                    bool estado = Convert.ToBoolean(row.Cells["disponible"].Value);

                    if (estado)
                    {
                        row.Cells["disponible"].Style.BackColor = Color.LightGreen;
                        row.Cells["disponible"].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.Cells["disponible"].Style.BackColor = Color.LightCoral;
                        row.Cells["disponible"].Style.ForeColor = Color.Black;
                    }

                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LlenarCamposDesdeBusqueda(FrmBuscarVehiculo frm)
        {
            txtCodigo.Text = frm.idVehiculo;
            txtMarca.Text = frm.marca;
            txtModelo.Text = frm.modelo;
            numAnio.Value = Convert.ToDecimal(frm.anio);
            txtPlaca.Text = frm.placa;
            txtColor.Text = frm.color;
            txtPrecio.Text = frm.precio;
            cbGama.Text = frm.gama;
            cbDisponible.Text = frm.disponible;

            if (frm.imagen != null)
            {
                MemoryStream ms = new MemoryStream(frm.imagen);
                pbImagen.Image = Image.FromStream(ms);

                imagenBytes = frm.imagen;
            }
            else
            {
                pbImagen.Image = null;
                imagenBytes = null;
            }


        }

        public void Limpiar()
        {
            txtCodigo.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtPlaca.Clear();
            txtColor.Clear();
            txtPrecio.Clear();
          
            numAnio.Value = 2020;
            pbImagen.Image = null;
            imagenBytes = null;
            cbDisponible.SelectedIndex = -1;
        }

        private void MantenimientoVehiculos_Load(object sender, EventArgs e)
        {
            CargarVehiculos();
            CargarGamas();
        }

        private void dgvVehiculos_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (cargandoDesdeBusqueda) return;

            if (dgvVehiculos.CurrentRow == null) return;

            try
            {
                txtCodigo.Text = dgvVehiculos.CurrentRow.Cells["Codigo"].Value.ToString();
                txtMarca.Text = dgvVehiculos.CurrentRow.Cells["marca"].Value.ToString();
                txtModelo.Text = dgvVehiculos.CurrentRow.Cells["modelo"].Value.ToString();
                numAnio.Value = Convert.ToDecimal(dgvVehiculos.CurrentRow.Cells["anio"].Value);
                txtPlaca.Text = dgvVehiculos.CurrentRow.Cells["placa"].Value.ToString();
                txtColor.Text = dgvVehiculos.CurrentRow.Cells["color"].Value.ToString();
                txtPrecio.Text = dgvVehiculos.CurrentRow.Cells["precio_diario"].Value.ToString();

                cbGama.Text = dgvVehiculos.CurrentRow.Cells["gama"].Value.ToString();
                          
                string estado = dgvVehiculos.CurrentRow.Cells["disponible"].Value.ToString();
                cbDisponible.Text = estado;

                int id = Convert.ToInt32(txtCodigo.Text);

                MySqlConnection con = Conexion.obtenerConexion();
                con.Open();

                string query = "SELECT imagen FROM Vehiculos WHERE vehiculo_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["imagen"] != DBNull.Value)
                    {
                        byte[] img = (byte[])reader["imagen"];
                        MemoryStream ms = new MemoryStream(img);
                        pbImagen.Image = Image.FromStream(ms);

                        imagenBytes = img;
                    }
                    else
                    {
                        pbImagen.Image = null;
                        imagenBytes = null;
                    }
                }

                con.Close();
            }
            catch
            {
                
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMarca.Text == "" || txtModelo.Text == "" || txtPlaca.Text == "" || txtPrecio.Text == "")
            {
                MessageBox.Show("Complete los campos obligatorios");
                return;
            }

            if (imagenBytes == null)
            {
                MessageBox.Show("Debe cargar una imagen");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string validar = "SELECT COUNT(*) FROM Vehiculos WHERE placa = @placa";
            MySqlCommand cmdValidar = new MySqlCommand(validar, con);
            cmdValidar.Parameters.AddWithValue("@placa", txtPlaca.Text);

            int existe = Convert.ToInt32(cmdValidar.ExecuteScalar());

            if (existe > 0)
            {
                MessageBox.Show("Esta placa ya está registrada");
                con.Close();
                return;
            }

            try
            {
                
                string query = @"INSERT INTO Vehiculos
        (marca, modelo, anio, placa, color, precio_diario, gama_id, imagen, disponible)
        VALUES(@marca, @modelo, @anio, @placa, @color, @precio, @gama, @imagen, @disponible)";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@marca", txtMarca.Text);
                cmd.Parameters.AddWithValue("@modelo", txtModelo.Text);
                cmd.Parameters.AddWithValue("@anio", numAnio.Value);
                cmd.Parameters.AddWithValue("@placa", txtPlaca.Text);
                cmd.Parameters.AddWithValue("@color", txtColor.Text);
                cmd.Parameters.AddWithValue("@precio", txtPrecio.Text);
                cmd.Parameters.AddWithValue("@gama", cbGama.SelectedValue);
                cmd.Parameters.AddWithValue("@imagen", imagenBytes);
                cmd.Parameters.AddWithValue("@disponible", true);

                cmd.ExecuteNonQuery();

                
                int vehiculoId = Convert.ToInt32(cmd.LastInsertedId);

               
                string insertPrecios = @"INSERT INTO Precios_Vehiculo 
        (vehiculo_id, tipo_dia_id, precio) VALUES
        (@vehiculo, 1, @precioNormal),
        (@vehiculo, 2, @precioFinSemana),
        (@vehiculo, 3, @precioFeriado)";

                MySqlCommand cmdPrecios = new MySqlCommand(insertPrecios, con);

                decimal precioBase = Convert.ToDecimal(txtPrecio.Text);

                cmdPrecios.Parameters.AddWithValue("@vehiculo", vehiculoId);
                cmdPrecios.Parameters.AddWithValue("@precioNormal", precioBase);
                cmdPrecios.Parameters.AddWithValue("@precioFinSemana", precioBase + 500);
                cmdPrecios.Parameters.AddWithValue("@precioFeriado", precioBase + 1000);

                cmdPrecios.ExecuteNonQuery();

                MessageBox.Show("Vehículo agregado con precios correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            con.Close();

            CargarVehiculos();
            Limpiar();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione un vehículo");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string validar = "SELECT COUNT(*) FROM Vehiculos WHERE placa = @placa AND vehiculo_id <> @id";
            MySqlCommand cmdValidar = new MySqlCommand(validar, con);

            cmdValidar.Parameters.AddWithValue("@placa", txtPlaca.Text);
            cmdValidar.Parameters.AddWithValue("@id", txtCodigo.Text);

            int existe = Convert.ToInt32(cmdValidar.ExecuteScalar());

            if (existe > 0)
            {
                MessageBox.Show("Ya existe otro vehículo con esa placa");
                con.Close();
                return;
            }

            try
            {
                string query = @"UPDATE Vehiculos SET 
        marca=@marca,
        modelo=@modelo,
        anio=@anio,
        placa=@placa,
        color=@color,
        precio_diario=@precio,
        gama_id=@gama,
        imagen=@imagen,
        disponible=@disponible
        WHERE vehiculo_id=@id";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@marca", txtMarca.Text);
                cmd.Parameters.AddWithValue("@modelo", txtModelo.Text);
                cmd.Parameters.AddWithValue("@anio", numAnio.Value);
                cmd.Parameters.AddWithValue("@placa", txtPlaca.Text);
                cmd.Parameters.AddWithValue("@color", txtColor.Text);
                cmd.Parameters.AddWithValue("@precio", txtPrecio.Text);
                cmd.Parameters.AddWithValue("@gama", cbGama.SelectedValue);
                cmd.Parameters.AddWithValue("@id", txtCodigo.Text);
                cmd.Parameters.AddWithValue("@imagen", imagenBytes);

                bool disponible = cbDisponible.Text == "Disponible";
                cmd.Parameters.AddWithValue("@disponible", disponible);

                cmd.ExecuteNonQuery();

                string delete = "DELETE FROM Precios_Vehiculo WHERE vehiculo_id=@vehiculo";
                MySqlCommand cmdDelete = new MySqlCommand(delete, con);
                cmdDelete.Parameters.AddWithValue("@vehiculo", txtCodigo.Text);
                cmdDelete.ExecuteNonQuery();

                string insertPrecios = @"INSERT INTO Precios_Vehiculo 
        (vehiculo_id, tipo_dia_id, precio) VALUES
        (@vehiculo, 1, @precioNormal),
        (@vehiculo, 2, @precioFinSemana),
        (@vehiculo, 3, @precioFeriado)";

                MySqlCommand cmdInsert = new MySqlCommand(insertPrecios, con);

                decimal precioBase = Convert.ToDecimal(txtPrecio.Text);

                cmdInsert.Parameters.AddWithValue("@vehiculo", txtCodigo.Text);
                cmdInsert.Parameters.AddWithValue("@precioNormal", precioBase);
                cmdInsert.Parameters.AddWithValue("@precioFinSemana", precioBase + 500);
                cmdInsert.Parameters.AddWithValue("@precioFeriado", precioBase + 1000);

                cmdInsert.ExecuteNonQuery();

                MessageBox.Show("Vehículo modificado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            con.Close();

            CargarVehiculos();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Seleccione un vehículo");
                return;
            }

            MySqlConnection con = Conexion.obtenerConexion();
            con.Open();

            string query = "UPDATE Vehiculos SET estado='Inactivo' WHERE vehiculo_id=@id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtCodigo.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Vehículo eliminado");

            CargarVehiculos();
            Limpiar();
        }


        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarVehiculos();
            Limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Imagenes|*.jpg;*.png;*.jpeg";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                pbImagen.Image = Image.FromFile(abrir.FileName);

                MemoryStream ms = new MemoryStream();
                pbImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imagenBytes = ms.ToArray();
            }
        }

        private void pbImagen_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarVehiculo_Click(object sender, EventArgs e)
        {
            FrmBuscarVehiculo frm = new FrmBuscarVehiculo();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                cargandoDesdeBusqueda = true;

                LlenarCamposDesdeBusqueda(frm);

                cargandoDesdeBusqueda = false;
            }
        }

        private void cbGama_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
