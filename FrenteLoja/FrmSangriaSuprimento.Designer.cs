namespace FrenteLoja
{
    partial class FrmSangriaSuprimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSangriaSuprimento));
            this.LstTipoDoc = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtValor = new System.Windows.Forms.NumericUpDown();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnCancMov = new System.Windows.Forms.Button();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.TxtDescricao = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtDtCaixa = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.LblNomeMov = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TxtValor)).BeginInit();
            this.SuspendLayout();
            // 
            // LstTipoDoc
            // 
            this.LstTipoDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstTipoDoc.FormattingEnabled = true;
            this.LstTipoDoc.Location = new System.Drawing.Point(277, 82);
            this.LstTipoDoc.Name = "LstTipoDoc";
            this.LstTipoDoc.Size = new System.Drawing.Size(229, 21);
            this.LstTipoDoc.TabIndex = 131;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(206, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(75, 13);
            this.label21.TabIndex = 132;
            this.label21.Text = "Documento:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 130;
            this.label3.Text = "Valor Movimento:";
            // 
            // TxtValor
            // 
            this.TxtValor.DecimalPlaces = 2;
            this.TxtValor.Location = new System.Drawing.Point(107, 82);
            this.TxtValor.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtValor.Name = "TxtValor";
            this.TxtValor.Size = new System.Drawing.Size(95, 20);
            this.TxtValor.TabIndex = 129;
            this.TxtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtValor.ThousandsSeparator = true;
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.Enabled = false;
            this.TxtUsuario.Location = new System.Drawing.Point(277, 47);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(191, 20);
            this.TxtUsuario.TabIndex = 128;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(218, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 127;
            this.label2.Text = "Operador:";
            // 
            // BtnCancMov
            // 
            this.BtnCancMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancMov.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancMov.Image")));
            this.BtnCancMov.Location = new System.Drawing.Point(376, 143);
            this.BtnCancMov.Name = "BtnCancMov";
            this.BtnCancMov.Size = new System.Drawing.Size(110, 25);
            this.BtnCancMov.TabIndex = 134;
            this.BtnCancMov.Text = "Cancelar";
            this.BtnCancMov.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancMov.UseVisualStyleBackColor = true;
            this.BtnCancMov.Click += new System.EventHandler(this.BtnCancMov_Click);
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(250, 143);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(110, 25);
            this.BtnConfirmar.TabIndex = 133;
            this.BtnConfirmar.Text = "Confirmar";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // TxtDescricao
            // 
            this.TxtDescricao.Location = new System.Drawing.Point(71, 109);
            this.TxtDescricao.MaxLength = 85;
            this.TxtDescricao.Name = "TxtDescricao";
            this.TxtDescricao.Size = new System.Drawing.Size(435, 20);
            this.TxtDescricao.TabIndex = 136;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 135;
            this.label4.Text = "Descrição:";
            // 
            // TxtDtCaixa
            // 
            this.TxtDtCaixa.Enabled = false;
            this.TxtDtCaixa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtDtCaixa.Location = new System.Drawing.Point(107, 47);
            this.TxtDtCaixa.Name = "TxtDtCaixa";
            this.TxtDtCaixa.Size = new System.Drawing.Size(81, 20);
            this.TxtDtCaixa.TabIndex = 126;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 125;
            this.label1.Text = "Data Movimento:";
            // 
            // LblNomeMov
            // 
            this.LblNomeMov.BackColor = System.Drawing.Color.Transparent;
            this.LblNomeMov.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblNomeMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNomeMov.Location = new System.Drawing.Point(0, 0);
            this.LblNomeMov.Name = "LblNomeMov";
            this.LblNomeMov.Size = new System.Drawing.Size(513, 29);
            this.LblNomeMov.TabIndex = 137;
            this.LblNomeMov.Text = "Sangria";
            this.LblNomeMov.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSangriaSuprimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(513, 180);
            this.Controls.Add(this.LblNomeMov);
            this.Controls.Add(this.TxtDescricao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnCancMov);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.LstTipoDoc);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtValor);
            this.Controls.Add(this.TxtUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtDtCaixa);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSangriaSuprimento";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSangriaSuprimento";
            this.Load += new System.EventHandler(this.FrmSangriaSuprimento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TxtValor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox LstTipoDoc;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TxtValor;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button BtnCancMov;
        public System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.TextBox TxtDescricao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker TxtDtCaixa;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label LblNomeMov;
    }
}