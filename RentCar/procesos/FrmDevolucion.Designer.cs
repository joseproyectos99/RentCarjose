namespace RentCar.procesos
{
    partial class FrmDevolucion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbReserva = new System.Windows.Forms.ComboBox();
            this.dtpFechaRecepcion = new System.Windows.Forms.DateTimePicker();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.vehiculo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vehículo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Combustible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kilometraje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Daños = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CargoExtra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardarDevolucion = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1152, 92);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 28F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(325, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 67);
            this.label1.TabIndex = 0;
            this.label1.Text = "Devolucion de alquileres";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(876, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 24);
            this.label3.TabIndex = 36;
            this.label3.Text = "Observaciones";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(482, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 24);
            this.label4.TabIndex = 38;
            this.label4.Text = "Fecha de recepcion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(145, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 24);
            this.label6.TabIndex = 40;
            this.label6.Text = "Reservas";
            // 
            // cmbReserva
            // 
            this.cmbReserva.FormattingEnabled = true;
            this.cmbReserva.Location = new System.Drawing.Point(28, 185);
            this.cmbReserva.Name = "cmbReserva";
            this.cmbReserva.Size = new System.Drawing.Size(315, 28);
            this.cmbReserva.TabIndex = 41;
            this.cmbReserva.SelectedIndexChanged += new System.EventHandler(this.cmbReserva_SelectedIndexChanged);
            // 
            // dtpFechaRecepcion
            // 
            this.dtpFechaRecepcion.Location = new System.Drawing.Point(418, 189);
            this.dtpFechaRecepcion.Name = "dtpFechaRecepcion";
            this.dtpFechaRecepcion.Size = new System.Drawing.Size(315, 26);
            this.dtpFechaRecepcion.TabIndex = 42;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(800, 187);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(324, 26);
            this.txtObservaciones.TabIndex = 43;
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vehiculo_id,
            this.Vehículo,
            this.Combustible,
            this.Kilometraje,
            this.Daños,
            this.CargoExtra});
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDetalle.Location = new System.Drawing.Point(0, 380);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.RowHeadersWidth = 62;
            this.dgvDetalle.RowTemplate.Height = 28;
            this.dgvDetalle.Size = new System.Drawing.Size(1152, 274);
            this.dgvDetalle.TabIndex = 44;
            this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellEndEdit);
            // 
            // vehiculo_id
            // 
            this.vehiculo_id.HeaderText = "vehiculo_id";
            this.vehiculo_id.MinimumWidth = 8;
            this.vehiculo_id.Name = "vehiculo_id";
            this.vehiculo_id.Visible = false;
            this.vehiculo_id.Width = 150;
            // 
            // Vehículo
            // 
            this.Vehículo.HeaderText = "Vehículo";
            this.Vehículo.MinimumWidth = 8;
            this.Vehículo.Name = "Vehículo";
            this.Vehículo.Width = 150;
            // 
            // Combustible
            // 
            this.Combustible.HeaderText = "Combustible";
            this.Combustible.MinimumWidth = 8;
            this.Combustible.Name = "Combustible";
            this.Combustible.Width = 150;
            // 
            // Kilometraje
            // 
            this.Kilometraje.HeaderText = "Kilometraje";
            this.Kilometraje.MinimumWidth = 8;
            this.Kilometraje.Name = "Kilometraje";
            this.Kilometraje.Width = 150;
            // 
            // Daños
            // 
            this.Daños.HeaderText = "Daños";
            this.Daños.MinimumWidth = 8;
            this.Daños.Name = "Daños";
            this.Daños.Width = 150;
            // 
            // CargoExtra
            // 
            this.CargoExtra.HeaderText = "CargoExtra";
            this.CargoExtra.MinimumWidth = 8;
            this.CargoExtra.Name = "CargoExtra";
            this.CargoExtra.Width = 150;
            // 
            // btnGuardarDevolucion
            // 
            this.btnGuardarDevolucion.Location = new System.Drawing.Point(418, 267);
            this.btnGuardarDevolucion.Name = "btnGuardarDevolucion";
            this.btnGuardarDevolucion.Size = new System.Drawing.Size(315, 72);
            this.btnGuardarDevolucion.TabIndex = 45;
            this.btnGuardarDevolucion.Text = "Guardar devolucion";
            this.btnGuardarDevolucion.UseVisualStyleBackColor = true;
            this.btnGuardarDevolucion.Click += new System.EventHandler(this.btnGuardarDevolucion_Click);
            // 
            // FrmDevolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1152, 654);
            this.Controls.Add(this.btnGuardarDevolucion);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.dtpFechaRecepcion);
            this.Controls.Add(this.cmbReserva);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "FrmDevolucion";
            this.Text = "FrmDevolucion";
            this.Load += new System.EventHandler(this.FrmDevolucion_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbReserva;
        private System.Windows.Forms.DateTimePicker dtpFechaRecepcion;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehiculo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vehículo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Combustible;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kilometraje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Daños;
        private System.Windows.Forms.DataGridViewTextBoxColumn CargoExtra;
        private System.Windows.Forms.Button btnGuardarDevolucion;
    }
}