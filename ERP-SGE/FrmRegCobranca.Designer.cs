namespace ERP_SGE
{
    partial class FrmRegCobranca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegCobranca));
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.TxtObservacao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TxtMostraObs = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PnlCob = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtVencimento = new System.Windows.Forms.DateTimePicker();
            this.TxtVlrOriginal = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtReferente = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtNotaFiscal = new System.Windows.Forms.TextBox();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtRetorno = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PnlCob.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.ForeColor = System.Drawing.Color.Maroon;
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(404, 431);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(117, 30);
            this.BtnConfirmar.TabIndex = 68;
            this.BtnConfirmar.Text = "Confirmar";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // TxtObservacao
            // 
            this.TxtObservacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtObservacao.Location = new System.Drawing.Point(0, 18);
            this.TxtObservacao.Multiline = true;
            this.TxtObservacao.Name = "TxtObservacao";
            this.TxtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtObservacao.Size = new System.Drawing.Size(533, 82);
            this.TxtObservacao.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Informações:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.TxtObservacao);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 327);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(533, 100);
            this.panel2.TabIndex = 67;
            // 
            // TxtMostraObs
            // 
            this.TxtMostraObs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtMostraObs.Location = new System.Drawing.Point(0, 0);
            this.TxtMostraObs.Multiline = true;
            this.TxtMostraObs.Name = "TxtMostraObs";
            this.TxtMostraObs.ReadOnly = true;
            this.TxtMostraObs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtMostraObs.Size = new System.Drawing.Size(533, 229);
            this.TxtMostraObs.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.TxtMostraObs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 229);
            this.panel1.TabIndex = 66;
            // 
            // PnlCob
            // 
            this.PnlCob.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.PnlCob.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlCob.Controls.Add(this.label10);
            this.PnlCob.Controls.Add(this.TxtVencimento);
            this.PnlCob.Controls.Add(this.TxtVlrOriginal);
            this.PnlCob.Controls.Add(this.label9);
            this.PnlCob.Controls.Add(this.TxtReferente);
            this.PnlCob.Controls.Add(this.label8);
            this.PnlCob.Controls.Add(this.TxtNotaFiscal);
            this.PnlCob.Controls.Add(this.TxtNumDoc);
            this.PnlCob.Controls.Add(this.label3);
            this.PnlCob.Controls.Add(this.label7);
            this.PnlCob.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlCob.Location = new System.Drawing.Point(0, 0);
            this.PnlCob.Name = "PnlCob";
            this.PnlCob.Size = new System.Drawing.Size(533, 98);
            this.PnlCob.TabIndex = 71;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(253, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 126;
            this.label10.Text = "Vencimento:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtVencimento
            // 
            this.TxtVencimento.Enabled = false;
            this.TxtVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtVencimento.Location = new System.Drawing.Point(253, 69);
            this.TxtVencimento.Name = "TxtVencimento";
            this.TxtVencimento.Size = new System.Drawing.Size(97, 20);
            this.TxtVencimento.TabIndex = 125;
            // 
            // TxtVlrOriginal
            // 
            this.TxtVlrOriginal.DecimalPlaces = 2;
            this.TxtVlrOriginal.Enabled = false;
            this.TxtVlrOriginal.Location = new System.Drawing.Point(139, 69);
            this.TxtVlrOriginal.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrOriginal.Name = "TxtVlrOriginal";
            this.TxtVlrOriginal.Size = new System.Drawing.Size(93, 20);
            this.TxtVlrOriginal.TabIndex = 124;
            this.TxtVlrOriginal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrOriginal.ThousandsSeparator = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(136, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 123;
            this.label9.Text = "Valor Origem R$:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtReferente
            // 
            this.TxtReferente.Enabled = false;
            this.TxtReferente.Location = new System.Drawing.Point(139, 25);
            this.TxtReferente.MaxLength = 40;
            this.TxtReferente.Name = "TxtReferente";
            this.TxtReferente.Size = new System.Drawing.Size(290, 20);
            this.TxtReferente.TabIndex = 122;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(136, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 121;
            this.label8.Text = "Referente:";
            // 
            // TxtNotaFiscal
            // 
            this.TxtNotaFiscal.Enabled = false;
            this.TxtNotaFiscal.Location = new System.Drawing.Point(5, 69);
            this.TxtNotaFiscal.MaxLength = 40;
            this.TxtNotaFiscal.Name = "TxtNotaFiscal";
            this.TxtNotaFiscal.Size = new System.Drawing.Size(127, 20);
            this.TxtNotaFiscal.TabIndex = 120;
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.Enabled = false;
            this.TxtNumDoc.Location = new System.Drawing.Point(5, 25);
            this.TxtNumDoc.MaxLength = 25;
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.Size = new System.Drawing.Size(127, 20);
            this.TxtNumDoc.TabIndex = 117;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 119;
            this.label3.Text = "Nota Fiscal:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 118;
            this.label7.Text = "No. Documento:";
            // 
            // TxtRetorno
            // 
            this.TxtRetorno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRetorno.Location = new System.Drawing.Point(95, 433);
            this.TxtRetorno.Mask = "00/00/0000";
            this.TxtRetorno.Name = "TxtRetorno";
            this.TxtRetorno.Size = new System.Drawing.Size(87, 21);
            this.TxtRetorno.TabIndex = 72;
            this.TxtRetorno.ValidatingType = typeof(System.DateTime);
            this.TxtRetorno.Validated += new System.EventHandler(this.TxtRetorno_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 440);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Data Retorno:";
            // 
            // FrmRegCobranca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(533, 485);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtRetorno);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PnlCob);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegCobranca";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Cobrança";
            this.Load += new System.EventHandler(this.FrmRegCobranca_Load);
            this.Shown += new System.EventHandler(this.FrmRegCobranca_Shown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PnlCob.ResumeLayout(false);
            this.PnlCob.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.TextBox TxtObservacao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TxtMostraObs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PnlCob;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker TxtVencimento;
        private System.Windows.Forms.NumericUpDown TxtVlrOriginal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtReferente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtNotaFiscal;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox TxtRetorno;
        private System.Windows.Forms.Label label2;
    }
}