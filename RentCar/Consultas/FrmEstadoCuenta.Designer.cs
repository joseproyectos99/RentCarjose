namespace RentCar.Consultas
{
    partial class FrmEstadoCuenta
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
            this.label5 = new System.Windows.Forms.Label();
            this.cmbReserva = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFechaFin = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtFechaInicio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.txtPenalidades = new System.Windows.Forms.TextBox();
            this.txtPagado = new System.Windows.Forms.TextBox();
            this.txtTotalAlquiler = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtExtras = new System.Windows.Forms.TextBox();
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
            this.panel1.Size = new System.Drawing.Size(1192, 92);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 28F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(390, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 67);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estado de cuentas";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(96, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "Reserva";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // cmbReserva
            // 
            this.cmbReserva.FormattingEnabled = true;
            this.cmbReserva.Items.AddRange(new object[] {
            "Disponible",
            "No disponible"});
            this.cmbReserva.Location = new System.Drawing.Point(12, 138);
            this.cmbReserva.Name = "cmbReserva";
            this.cmbReserva.Size = new System.Drawing.Size(245, 28);
            this.cmbReserva.TabIndex = 22;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(100, 172);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(135, 72);
            this.btnBuscar.TabIndex = 49;
            this.btnBuscar.Text = "Cargar o buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(0, 267);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.RowHeadersWidth = 62;
            this.dgvDetalle.RowTemplate.Height = 28;
            this.dgvDetalle.Size = new System.Drawing.Size(1192, 215);
            this.dgvDetalle.TabIndex = 50;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(944, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 24);
            this.label7.TabIndex = 56;
            this.label7.Text = "Fecha fin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(398, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 24);
            this.label6.TabIndex = 57;
            this.label6.Text = "Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(655, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 24);
            this.label3.TabIndex = 58;
            this.label3.Text = "Fecha de inicio";
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.Location = new System.Drawing.Point(891, 140);
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.ReadOnly = true;
            this.txtFechaFin.Size = new System.Drawing.Size(245, 26);
            this.txtFechaFin.TabIndex = 59;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(305, 140);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(258, 26);
            this.txtCliente.TabIndex = 60;
            // 
            // txtFechaInicio
            // 
            this.txtFechaInicio.Location = new System.Drawing.Point(608, 140);
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.ReadOnly = true;
            this.txtFechaInicio.Size = new System.Drawing.Size(248, 26);
            this.txtFechaInicio.TabIndex = 61;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(398, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 24);
            this.label4.TabIndex = 63;
            this.label4.Text = "estado";
            // 
            // txtEstado
            // 
            this.txtEstado.Location = new System.Drawing.Point(305, 212);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(258, 26);
            this.txtEstado.TabIndex = 64;
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new System.Drawing.Point(922, 531);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(245, 26);
            this.txtBalance.TabIndex = 65;
            // 
            // txtPenalidades
            // 
            this.txtPenalidades.Location = new System.Drawing.Point(467, 531);
            this.txtPenalidades.Name = "txtPenalidades";
            this.txtPenalidades.ReadOnly = true;
            this.txtPenalidades.Size = new System.Drawing.Size(151, 26);
            this.txtPenalidades.TabIndex = 66;
            // 
            // txtPagado
            // 
            this.txtPagado.Location = new System.Drawing.Point(259, 531);
            this.txtPagado.Name = "txtPagado";
            this.txtPagado.ReadOnly = true;
            this.txtPagado.Size = new System.Drawing.Size(150, 26);
            this.txtPagado.TabIndex = 67;
            this.txtPagado.TextChanged += new System.EventHandler(this.txtPagado_TextChanged);
            // 
            // txtTotalAlquiler
            // 
            this.txtTotalAlquiler.Location = new System.Drawing.Point(23, 531);
            this.txtTotalAlquiler.Name = "txtTotalAlquiler";
            this.txtTotalAlquiler.ReadOnly = true;
            this.txtTotalAlquiler.Size = new System.Drawing.Size(166, 26);
            this.txtTotalAlquiler.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1005, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 24);
            this.label2.TabIndex = 69;
            this.label2.Text = "Balance";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(476, 504);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 24);
            this.label8.TabIndex = 70;
            this.label8.Text = "Penalidades";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(288, 504);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 24);
            this.label9.TabIndex = 71;
            this.label9.Text = "Pagado";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 504);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(182, 24);
            this.label10.TabIndex = 72;
            this.label10.Text = "Total del alquiler";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(706, 185);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(135, 72);
            this.btnImprimir.TabIndex = 73;
            this.btnImprimir.Text = "Imprimir ";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(891, 185);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(135, 72);
            this.btnCerrar.TabIndex = 74;
            this.btnCerrar.Text = "cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(687, 504);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 24);
            this.label11.TabIndex = 75;
            this.label11.Text = "cargos extrax";
            // 
            // txtExtras
            // 
            this.txtExtras.Location = new System.Drawing.Point(690, 531);
            this.txtExtras.Name = "txtExtras";
            this.txtExtras.ReadOnly = true;
            this.txtExtras.Size = new System.Drawing.Size(151, 26);
            this.txtExtras.TabIndex = 76;
            // 
            // FrmEstadoCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1192, 581);
            this.Controls.Add(this.txtExtras);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTotalAlquiler);
            this.Controls.Add(this.txtPagado);
            this.Controls.Add(this.txtPenalidades);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.txtEstado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFechaInicio);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.txtFechaFin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cmbReserva);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Name = "FrmEstadoCuenta";
            this.Text = "FrmEstadoCuenta";
            this.Load += new System.EventHandler(this.FrmEstadoCuenta_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbReserva;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFechaFin;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtFechaInicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.TextBox txtPenalidades;
        private System.Windows.Forms.TextBox txtPagado;
        private System.Windows.Forms.TextBox txtTotalAlquiler;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtExtras;
    }
}