namespace ERP_SGE
{
    partial class FrmSincronismo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSincronismo));
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.Dt2 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.BtnEnviar = new System.Windows.Forms.Button();
            this.ProcBar = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.BoxItemPesq.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.BackColor = System.Drawing.Color.Transparent;
            this.BoxItemPesq.Controls.Add(this.label8);
            this.BoxItemPesq.Controls.Add(this.label16);
            this.BoxItemPesq.Controls.Add(this.LstFilial);
            this.BoxItemPesq.Controls.Add(this.Dt2);
            this.BoxItemPesq.Controls.Add(this.label6);
            this.BoxItemPesq.Controls.Add(this.Dt1);
            this.BoxItemPesq.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxItemPesq.Location = new System.Drawing.Point(0, 0);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(329, 69);
            this.BoxItemPesq.TabIndex = 72;
            this.BoxItemPesq.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(185, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "a";
            // 
            // Dt2
            // 
            this.Dt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt2.Location = new System.Drawing.Point(205, 43);
            this.Dt2.Name = "Dt2";
            this.Dt2.Size = new System.Drawing.Size(100, 20);
            this.Dt2.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Período de:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Dt1
            // 
            this.Dt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt1.Location = new System.Drawing.Point(79, 43);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(100, 20);
            this.Dt1.TabIndex = 1;
            // 
            // BtnEnviar
            // 
            this.BtnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEnviar.Image = ((System.Drawing.Image)(resources.GetObject("BtnEnviar.Image")));
            this.BtnEnviar.Location = new System.Drawing.Point(62, 75);
            this.BtnEnviar.Name = "BtnEnviar";
            this.BtnEnviar.Size = new System.Drawing.Size(189, 34);
            this.BtnEnviar.TabIndex = 3;
            this.BtnEnviar.Text = "Iniciar Processo";
            this.BtnEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnEnviar.UseVisualStyleBackColor = true;
            this.BtnEnviar.Click += new System.EventHandler(this.BtnEnviar_Click);
            // 
            // ProcBar
            // 
            this.ProcBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProcBar.Location = new System.Drawing.Point(0, 117);
            this.ProcBar.Name = "ProcBar";
            this.ProcBar.Size = new System.Drawing.Size(329, 23);
            this.ProcBar.TabIndex = 70;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "Origem:";
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia"});
            this.LstFilial.Location = new System.Drawing.Point(62, 12);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(243, 21);
            this.LstFilial.TabIndex = 0;
            // 
            // FrmSincronismo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(329, 140);
            this.Controls.Add(this.BoxItemPesq);
            this.Controls.Add(this.BtnEnviar);
            this.Controls.Add(this.ProcBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSincronismo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sincronismo das Notas Fiscais";
            this.Load += new System.EventHandler(this.FrmSincronismo_Load);
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker Dt2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker Dt1;
        public System.Windows.Forms.Button BtnEnviar;
        private System.Windows.Forms.ProgressBar ProcBar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox LstFilial;
    }
}