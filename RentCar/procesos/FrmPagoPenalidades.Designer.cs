namespace RentCar.procesos
{
    partial class FrmPagoPenalidades
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
            this.btnPagar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbMetodo = new System.Windows.Forms.ComboBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbReserva = new System.Windows.Forms.ComboBox();
            this.dgvPenalidades = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPenalidades)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 92);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 28F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(305, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 67);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pagos de penalidades";
            // 
            // btnPagar
            // 
            this.btnPagar.Location = new System.Drawing.Point(424, 219);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(312, 98);
            this.btnPagar.TabIndex = 104;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(95, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 24);
            this.label5.TabIndex = 103;
            this.label5.Text = "Metodo de pago";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(85, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 24);
            this.label4.TabIndex = 102;
            this.label4.Text = "Total penalidades";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(534, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 24);
            this.label2.TabIndex = 101;
            this.label2.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(424, 146);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(297, 26);
            this.dtpFecha.TabIndex = 100;
            // 
            // cmbMetodo
            // 
            this.cmbMetodo.FormattingEnabled = true;
            this.cmbMetodo.Location = new System.Drawing.Point(23, 217);
            this.cmbMetodo.Name = "cmbMetodo";
            this.cmbMetodo.Size = new System.Drawing.Size(312, 28);
            this.cmbMetodo.TabIndex = 99;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(23, 291);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(312, 26);
            this.txtTotal.TabIndex = 98;
            this.txtTotal.TextChanged += new System.EventHandler(this.txtTotalExtras_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(130, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 24);
            this.label3.TabIndex = 97;
            this.label3.Text = "Reservas";
            // 
            // cmbReserva
            // 
            this.cmbReserva.FormattingEnabled = true;
            this.cmbReserva.Location = new System.Drawing.Point(23, 146);
            this.cmbReserva.Name = "cmbReserva";
            this.cmbReserva.Size = new System.Drawing.Size(315, 28);
            this.cmbReserva.TabIndex = 96;
            this.cmbReserva.SelectedIndexChanged += new System.EventHandler(this.cmbReserva_SelectedIndexChanged);
            // 
            // dgvPenalidades
            // 
            this.dgvPenalidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPenalidades.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvPenalidades.Location = new System.Drawing.Point(0, 362);
            this.dgvPenalidades.Name = "dgvPenalidades";
            this.dgvPenalidades.RowHeadersWidth = 62;
            this.dgvPenalidades.RowTemplate.Height = 28;
            this.dgvPenalidades.Size = new System.Drawing.Size(1090, 290);
            this.dgvPenalidades.TabIndex = 105;
            // 
            // FrmPagoPenalidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1090, 652);
            this.Controls.Add(this.dgvPenalidades);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.cmbMetodo);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbReserva);
            this.Controls.Add(this.panel1);
            this.Name = "FrmPagoPenalidades";
            this.Text = "FrmPagoPenalidades";
            this.Load += new System.EventHandler(this.FrmPagoPenalidades_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPenalidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox cmbMetodo;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbReserva;
        private System.Windows.Forms.DataGridView dgvPenalidades;
    }
}