namespace FrenteLoja
{
    partial class FrmFecharVenda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFecharVenda));
            this.LstTipoDoc = new System.Windows.Forms.ComboBox();
            this.LblTotalVenda = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtVlrMov = new System.Windows.Forms.NumericUpDown();
            this.LblVlrRec = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnCancMov = new System.Windows.Forms.Button();
            this.LblVlrDif = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtDesconto = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtNParc = new System.Windows.Forms.NumericUpDown();
            this.LblParc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrMov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDesconto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNParc)).BeginInit();
            this.SuspendLayout();
            // 
            // LstTipoDoc
            // 
            this.LstTipoDoc.Dock = System.Windows.Forms.DockStyle.Left;
            this.LstTipoDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.LstTipoDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstTipoDoc.FormattingEnabled = true;
            this.LstTipoDoc.Location = new System.Drawing.Point(0, 0);
            this.LstTipoDoc.Name = "LstTipoDoc";
            this.LstTipoDoc.Size = new System.Drawing.Size(207, 343);
            this.LstTipoDoc.TabIndex = 132;
            this.LstTipoDoc.SelectedIndexChanged += new System.EventHandler(this.LstTipoDoc_SelectedIndexChanged);
            this.LstTipoDoc.Enter += new System.EventHandler(this.LstTipoDoc_Enter);
            this.LstTipoDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstTipoDoc_KeyPress);
            // 
            // LblTotalVenda
            // 
            this.LblTotalVenda.BackColor = System.Drawing.Color.Gray;
            this.LblTotalVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalVenda.Location = new System.Drawing.Point(333, 152);
            this.LblTotalVenda.Name = "LblTotalVenda";
            this.LblTotalVenda.Size = new System.Drawing.Size(114, 31);
            this.LblTotalVenda.TabIndex = 134;
            this.LblTotalVenda.Text = "0,00";
            this.LblTotalVenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(255, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 133;
            this.label2.Text = "Total R$:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(254, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 135;
            this.label1.Text = "Valor R$:";
            // 
            // TxtVlrMov
            // 
            this.TxtVlrMov.DecimalPlaces = 2;
            this.TxtVlrMov.Enabled = false;
            this.TxtVlrMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVlrMov.Location = new System.Drawing.Point(333, 108);
            this.TxtVlrMov.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrMov.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.TxtVlrMov.Name = "TxtVlrMov";
            this.TxtVlrMov.Size = new System.Drawing.Size(114, 29);
            this.TxtVlrMov.TabIndex = 135;
            this.TxtVlrMov.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrMov.ThousandsSeparator = true;
            this.TxtVlrMov.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.TxtVlrMov.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVlrMov_KeyPress);
            this.TxtVlrMov.Validated += new System.EventHandler(this.TxtVlrMov_Validated);
            // 
            // LblVlrRec
            // 
            this.LblVlrRec.BackColor = System.Drawing.Color.Gray;
            this.LblVlrRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVlrRec.Location = new System.Drawing.Point(333, 195);
            this.LblVlrRec.Name = "LblVlrRec";
            this.LblVlrRec.Size = new System.Drawing.Size(114, 31);
            this.LblVlrRec.TabIndex = 138;
            this.LblVlrRec.Text = "0,00";
            this.LblVlrRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(219, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.TabIndex = 137;
            this.label4.Text = "Recebido R$:";
            // 
            // BtnCancMov
            // 
            this.BtnCancMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancMov.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancMov.Image")));
            this.BtnCancMov.Location = new System.Drawing.Point(277, 289);
            this.BtnCancMov.Name = "BtnCancMov";
            this.BtnCancMov.Size = new System.Drawing.Size(116, 34);
            this.BtnCancMov.TabIndex = 139;
            this.BtnCancMov.Text = "Cancelar";
            this.BtnCancMov.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancMov.UseVisualStyleBackColor = true;
            this.BtnCancMov.Click += new System.EventHandler(this.BtnCancMov_Click);
            // 
            // LblVlrDif
            // 
            this.LblVlrDif.BackColor = System.Drawing.Color.Gray;
            this.LblVlrDif.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVlrDif.Location = new System.Drawing.Point(333, 236);
            this.LblVlrDif.Name = "LblVlrDif";
            this.LblVlrDif.Size = new System.Drawing.Size(114, 31);
            this.LblVlrDif.TabIndex = 141;
            this.LblVlrDif.Text = "0,00";
            this.LblVlrDif.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(217, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 20);
            this.label6.TabIndex = 140;
            this.label6.Text = "Diferença R$:";
            // 
            // TxtDesconto
            // 
            this.TxtDesconto.DecimalPlaces = 2;
            this.TxtDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDesconto.Location = new System.Drawing.Point(333, 12);
            this.TxtDesconto.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtDesconto.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.TxtDesconto.Name = "TxtDesconto";
            this.TxtDesconto.Size = new System.Drawing.Size(114, 29);
            this.TxtDesconto.TabIndex = 133;
            this.TxtDesconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtDesconto.ThousandsSeparator = true;
            this.TxtDesconto.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.TxtDesconto.Validated += new System.EventHandler(this.TxtDesconto_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(213, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 143;
            this.label3.Text = "Desconto R$:";
            // 
            // TxtNParc
            // 
            this.TxtNParc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNParc.Location = new System.Drawing.Point(392, 73);
            this.TxtNParc.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.TxtNParc.Name = "TxtNParc";
            this.TxtNParc.Size = new System.Drawing.Size(55, 29);
            this.TxtNParc.TabIndex = 134;
            this.TxtNParc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtNParc.ThousandsSeparator = true;
            this.TxtNParc.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // LblParc
            // 
            this.LblParc.AutoSize = true;
            this.LblParc.BackColor = System.Drawing.Color.Transparent;
            this.LblParc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblParc.Location = new System.Drawing.Point(306, 78);
            this.LblParc.Name = "LblParc";
            this.LblParc.Size = new System.Drawing.Size(87, 20);
            this.LblParc.TabIndex = 145;
            this.LblParc.Text = "No. Parc.:";
            // 
            // FrmFecharVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(474, 343);
            this.Controls.Add(this.TxtNParc);
            this.Controls.Add(this.LblParc);
            this.Controls.Add(this.TxtDesconto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LblVlrDif);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BtnCancMov);
            this.Controls.Add(this.LblVlrRec);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtVlrMov);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblTotalVenda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LstTipoDoc);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFecharVenda";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Fechamento da Venda";
            this.Load += new System.EventHandler(this.FrmFecharVenda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrMov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDesconto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNParc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox LstTipoDoc;
        private System.Windows.Forms.Label LblTotalVenda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown TxtVlrMov;
        private System.Windows.Forms.Label LblVlrRec;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button BtnCancMov;
        private System.Windows.Forms.Label LblVlrDif;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown TxtDesconto;
        public System.Windows.Forms.NumericUpDown TxtNParc;
        private System.Windows.Forms.Label LblParc;
    }
}