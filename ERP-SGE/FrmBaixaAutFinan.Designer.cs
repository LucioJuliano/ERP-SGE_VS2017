namespace ERP_SGE
{
    partial class FrmBaixaAutFinan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaixaAutFinan));
            this.LstTipoDoc = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.LstCaixa = new System.Windows.Forms.ComboBox();
            this.BtnBaixar = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.LstAgente = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LstTipoDoc
            // 
            this.LstTipoDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstTipoDoc.FormattingEnabled = true;
            this.LstTipoDoc.Location = new System.Drawing.Point(109, 37);
            this.LstTipoDoc.Name = "LstTipoDoc";
            this.LstTipoDoc.Size = new System.Drawing.Size(259, 21);
            this.LstTipoDoc.TabIndex = 131;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(8, 41);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 13);
            this.label21.TabIndex = 132;
            this.label21.Text = "Doc. Financeiro:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstCaixa
            // 
            this.LstCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstCaixa.FormattingEnabled = true;
            this.LstCaixa.Location = new System.Drawing.Point(109, 10);
            this.LstCaixa.Name = "LstCaixa";
            this.LstCaixa.Size = new System.Drawing.Size(259, 21);
            this.LstCaixa.TabIndex = 129;
            // 
            // BtnBaixar
            // 
            this.BtnBaixar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnBaixar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBaixar.Image = ((System.Drawing.Image)(resources.GetObject("BtnBaixar.Image")));
            this.BtnBaixar.Location = new System.Drawing.Point(256, 104);
            this.BtnBaixar.Name = "BtnBaixar";
            this.BtnBaixar.Size = new System.Drawing.Size(110, 35);
            this.BtnBaixar.TabIndex = 128;
            this.BtnBaixar.Text = "Efetuar Baixa";
            this.BtnBaixar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnBaixar.UseVisualStyleBackColor = true;
            this.BtnBaixar.Click += new System.EventHandler(this.BtnBaixar_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(4, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(105, 13);
            this.label24.TabIndex = 130;
            this.label24.Text = "Caixa Financeiro:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstAgente
            // 
            this.LstAgente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstAgente.FormattingEnabled = true;
            this.LstAgente.Location = new System.Drawing.Point(109, 65);
            this.LstAgente.Name = "LstAgente";
            this.LstAgente.Size = new System.Drawing.Size(259, 21);
            this.LstAgente.TabIndex = 133;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label31.Location = new System.Drawing.Point(3, 68);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(106, 13);
            this.label31.TabIndex = 134;
            this.label31.Text = "Agente Cobrador:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmBaixaAutFinan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(378, 151);
            this.Controls.Add(this.LstAgente);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.LstTipoDoc);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.LstCaixa);
            this.Controls.Add(this.BtnBaixar);
            this.Controls.Add(this.label24);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaixaAutFinan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baixa Automática";
            this.Load += new System.EventHandler(this.FrmBaixaAutFinan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button BtnBaixar;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.ComboBox LstTipoDoc;
        public System.Windows.Forms.ComboBox LstCaixa;
        private System.Windows.Forms.Label label31;
        public System.Windows.Forms.ComboBox LstAgente;
    }
}