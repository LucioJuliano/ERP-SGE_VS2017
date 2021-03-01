namespace ERP_SGE
{
    partial class FrmFormaPgtoVd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFormaPgtoVd));
            this.TxtPrazoPgto = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.LblPgto = new System.Windows.Forms.Label();
            this.LstFormaPgto = new System.Windows.Forms.ComboBox();
            this.BtnConcluir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtPrazoPgto
            // 
            this.TxtPrazoPgto.Location = new System.Drawing.Point(83, 38);
            this.TxtPrazoPgto.MaxLength = 20;
            this.TxtPrazoPgto.Name = "TxtPrazoPgto";
            this.TxtPrazoPgto.Size = new System.Drawing.Size(254, 20);
            this.TxtPrazoPgto.TabIndex = 163;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(9, 42);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(73, 13);
            this.label25.TabIndex = 162;
            this.label25.Text = "Prazo Pgto:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPgto
            // 
            this.LblPgto.AutoSize = true;
            this.LblPgto.BackColor = System.Drawing.Color.Transparent;
            this.LblPgto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.LblPgto.Location = new System.Drawing.Point(10, 16);
            this.LblPgto.Name = "LblPgto";
            this.LblPgto.Size = new System.Drawing.Size(75, 13);
            this.LblPgto.TabIndex = 161;
            this.LblPgto.Text = "Forma Pgto:";
            this.LblPgto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstFormaPgto
            // 
            this.LstFormaPgto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFormaPgto.FormattingEnabled = true;
            this.LstFormaPgto.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia"});
            this.LstFormaPgto.Location = new System.Drawing.Point(83, 12);
            this.LstFormaPgto.Name = "LstFormaPgto";
            this.LstFormaPgto.Size = new System.Drawing.Size(254, 21);
            this.LstFormaPgto.TabIndex = 160;
            // 
            // BtnConcluir
            // 
            this.BtnConcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConcluir.Image = ((System.Drawing.Image)(resources.GetObject("BtnConcluir.Image")));
            this.BtnConcluir.Location = new System.Drawing.Point(241, 64);
            this.BtnConcluir.Name = "BtnConcluir";
            this.BtnConcluir.Size = new System.Drawing.Size(96, 25);
            this.BtnConcluir.TabIndex = 164;
            this.BtnConcluir.Text = "Confirma";
            this.BtnConcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConcluir.UseVisualStyleBackColor = true;
            this.BtnConcluir.Click += new System.EventHandler(this.BtnConcluir_Click);
            // 
            // FrmFormaPgtoVd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(350, 98);
            this.Controls.Add(this.BtnConcluir);
            this.Controls.Add(this.TxtPrazoPgto);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.LblPgto);
            this.Controls.Add(this.LstFormaPgto);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFormaPgtoVd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alteração da Forma de Pagamento";
            this.Load += new System.EventHandler(this.FrmFormaPgtoVd_Load);
            this.Shown += new System.EventHandler(this.FrmFormaPgtoVd_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPrazoPgto;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.Label LblPgto;
        public System.Windows.Forms.ComboBox LstFormaPgto;
        private System.Windows.Forms.Button BtnConcluir;
    }
}