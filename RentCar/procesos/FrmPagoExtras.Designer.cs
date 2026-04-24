namespace RentCar.procesos
{
    partial class FrmPagoExtras
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
            this.cmbReserva = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvExtras = new System.Windows.Forms.DataGridView();
            this.txtTotalExtras = new System.Windows.Forms.TextBox();
            this.cmbMetodo = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPagar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtras)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 92);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 28F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(395, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 67);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pagos Extra";
            // 
            // cmbReserva
            // 
            this.cmbReserva.FormattingEnabled = true;
            this.cmbReserva.Location = new System.Drawing.Point(53, 148);
            this.cmbReserva.Name = "cmbReserva";
            this.cmbReserva.Size = new System.Drawing.Size(315, 28);
            this.cmbReserva.TabIndex = 8;
            this.cmbReserva.SelectedIndexChanged += new System.EventHandler(this.cmbReserva_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(160, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 24);
            this.label3.TabIndex = 36;
            this.label3.Text = "Reservas";
            // 
            // dgvExtras
            // 
            this.dgvExtras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtras.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvExtras.Location = new System.Drawing.Point(0, 377);
            this.dgvExtras.Name = "dgvExtras";
            this.dgvExtras.RowHeadersWidth = 62;
            this.dgvExtras.RowTemplate.Height = 28;
            this.dgvExtras.Size = new System.Drawing.Size(1089, 290);
            this.dgvExtras.TabIndex = 37;
            // 
            // txtTotalExtras
            // 
            this.txtTotalExtras.Location = new System.Drawing.Point(53, 293);
            this.txtTotalExtras.Name = "txtTotalExtras";
            this.txtTotalExtras.ReadOnly = true;
            this.txtTotalExtras.Size = new System.Drawing.Size(312, 26);
            this.txtTotalExtras.TabIndex = 89;
            // 
            // cmbMetodo
            // 
            this.cmbMetodo.FormattingEnabled = true;
            this.cmbMetodo.Location = new System.Drawing.Point(53, 219);
            this.cmbMetodo.Name = "cmbMetodo";
            this.cmbMetodo.Size = new System.Drawing.Size(312, 28);
            this.cmbMetodo.TabIndex = 90;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(454, 148);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(297, 26);
            this.dtpFecha.TabIndex = 91;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(564, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 24);
            this.label2.TabIndex = 92;
            this.label2.Text = "Fecha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(144, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 24);
            this.label4.TabIndex = 93;
            this.label4.Text = "Total Extra";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(125, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 24);
            this.label5.TabIndex = 94;
            this.label5.Text = "Metodo de pago";
            // 
            // btnPagar
            // 
            this.btnPagar.Location = new System.Drawing.Point(454, 221);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(312, 98);
            this.btnPagar.TabIndex = 95;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // FrmPagoExtras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1089, 667);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.cmbMetodo);
            this.Controls.Add(this.txtTotalExtras);
            this.Controls.Add(this.dgvExtras);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbReserva);
            this.Controls.Add(this.panel1);
            this.Name = "FrmPagoExtras";
            this.Text = "FrmPagoExtras";
            this.Load += new System.EventHandler(this.FrmPagoExtras_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbReserva;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvExtras;
        private System.Windows.Forms.TextBox txtTotalExtras;
        private System.Windows.Forms.ComboBox cmbMetodo;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPagar;
    }
}