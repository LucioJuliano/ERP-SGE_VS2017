namespace ERP_SGE
{
    partial class FrmCxaAbrir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCxaAbrir));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnAbrir = new System.Windows.Forms.Button();
            this.TxtObservacao = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtVlrInicial = new System.Windows.Forms.NumericUpDown();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDtCaixa = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrInicial)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.BtnAbrir);
            this.panel1.Controls.Add(this.TxtObservacao);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtVlrInicial);
            this.panel1.Controls.Add(this.TxtUsuario);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtDtCaixa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 187);
            this.panel1.TabIndex = 0;
            // 
            // BtnAbrir
            // 
            this.BtnAbrir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAbrir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("BtnAbrir.Image")));
            this.BtnAbrir.Location = new System.Drawing.Point(336, 116);
            this.BtnAbrir.Name = "BtnAbrir";
            this.BtnAbrir.Size = new System.Drawing.Size(110, 25);
            this.BtnAbrir.TabIndex = 122;
            this.BtnAbrir.Text = "Abrir Caixa";
            this.BtnAbrir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnAbrir.UseVisualStyleBackColor = true;
            this.BtnAbrir.Click += new System.EventHandler(this.BtnAbrir_Click);
            // 
            // TxtObservacao
            // 
            this.TxtObservacao.Location = new System.Drawing.Point(6, 87);
            this.TxtObservacao.Multiline = true;
            this.TxtObservacao.Name = "TxtObservacao";
            this.TxtObservacao.Size = new System.Drawing.Size(234, 83);
            this.TxtObservacao.TabIndex = 119;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(3, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 120;
            this.label11.Text = "Observações:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Fundo de Caixa:";
            // 
            // TxtVlrInicial
            // 
            this.TxtVlrInicial.DecimalPlaces = 2;
            this.TxtVlrInicial.Location = new System.Drawing.Point(101, 40);
            this.TxtVlrInicial.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrInicial.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.TxtVlrInicial.Name = "TxtVlrInicial";
            this.TxtVlrInicial.Size = new System.Drawing.Size(100, 20);
            this.TxtVlrInicial.TabIndex = 49;
            this.TxtVlrInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrInicial.ThousandsSeparator = true;
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.Enabled = false;
            this.TxtUsuario.Location = new System.Drawing.Point(276, 5);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(191, 20);
            this.TxtUsuario.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(217, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Operador:";
            // 
            // TxtDtCaixa
            // 
            this.TxtDtCaixa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtDtCaixa.Location = new System.Drawing.Point(92, 5);
            this.TxtDtCaixa.Name = "TxtDtCaixa";
            this.TxtDtCaixa.Size = new System.Drawing.Size(81, 20);
            this.TxtDtCaixa.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data do Caixa:";
            // 
            // FrmCxaAbrir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 187);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCxaAbrir";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abrir Caixa Balcão";
            this.Load += new System.EventHandler(this.FrmCxaAbrir_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrInicial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker TxtDtCaixa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TxtVlrInicial;
        private System.Windows.Forms.TextBox TxtObservacao;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button BtnAbrir;
    }
}