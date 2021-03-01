namespace ERP_SGE
{
    partial class FrmImpNfeTransf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImpNfeTransf));
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.LblTag = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CkListGrupos = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.Dt2 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.ProcBar = new System.Windows.Forms.ProgressBar();
            this.BoxItemPesq.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.BackColor = System.Drawing.Color.Transparent;
            this.BoxItemPesq.Controls.Add(this.LblTag);
            this.BoxItemPesq.Controls.Add(this.label1);
            this.BoxItemPesq.Controls.Add(this.CkListGrupos);
            this.BoxItemPesq.Controls.Add(this.label8);
            this.BoxItemPesq.Controls.Add(this.label16);
            this.BoxItemPesq.Controls.Add(this.LstFilial);
            this.BoxItemPesq.Controls.Add(this.Dt2);
            this.BoxItemPesq.Controls.Add(this.label6);
            this.BoxItemPesq.Controls.Add(this.Dt1);
            this.BoxItemPesq.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxItemPesq.Location = new System.Drawing.Point(0, 0);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(405, 232);
            this.BoxItemPesq.TabIndex = 69;
            this.BoxItemPesq.TabStop = false;
            // 
            // LblTag
            // 
            this.LblTag.AutoSize = true;
            this.LblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTag.ForeColor = System.Drawing.Color.Maroon;
            this.LblTag.Location = new System.Drawing.Point(12, 214);
            this.LblTag.Name = "LblTag";
            this.LblTag.Size = new System.Drawing.Size(98, 13);
            this.LblTag.TabIndex = 126;
            this.LblTag.Text = "Desmarca todos";
            this.LblTag.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 125;
            this.label1.Text = "Loja de Origem das Vendas";
            // 
            // CkListGrupos
            // 
            this.CkListGrupos.CheckOnClick = true;
            this.CkListGrupos.FormattingEnabled = true;
            this.CkListGrupos.IntegralHeight = false;
            this.CkListGrupos.Location = new System.Drawing.Point(12, 101);
            this.CkListGrupos.MultiColumn = true;
            this.CkListGrupos.Name = "CkListGrupos";
            this.CkListGrupos.Size = new System.Drawing.Size(381, 110);
            this.CkListGrupos.TabIndex = 124;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 13);
            this.label8.TabIndex = 76;
            this.label8.Text = "Loja de Origem das Vendas";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(192, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "a";
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia"});
            this.LstFilial.Location = new System.Drawing.Point(9, 60);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(303, 21);
            this.LstFilial.TabIndex = 75;
            // 
            // Dt2
            // 
            this.Dt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt2.Location = new System.Drawing.Point(212, 19);
            this.Dt2.Name = "Dt2";
            this.Dt2.Size = new System.Drawing.Size(100, 20);
            this.Dt2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 22);
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
            this.Dt1.Location = new System.Drawing.Point(86, 19);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(100, 20);
            this.Dt1.TabIndex = 0;
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(86, 238);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(186, 34);
            this.BtnConfirmar.TabIndex = 68;
            this.BtnConfirmar.Text = "Importar os Itens ";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // ProcBar
            // 
            this.ProcBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProcBar.Location = new System.Drawing.Point(0, 278);
            this.ProcBar.Name = "ProcBar";
            this.ProcBar.Size = new System.Drawing.Size(405, 23);
            this.ProcBar.TabIndex = 67;
            // 
            // FrmImpNfeTransf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(405, 301);
            this.Controls.Add(this.BoxItemPesq);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.ProcBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImpNfeTransf";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerar Nota de Transfência";
            this.Load += new System.EventHandler(this.FrmImpNfeTransf_Load);
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.ProgressBar ProcBar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox CkListGrupos;
        private System.Windows.Forms.Label LblTag;
        public System.Windows.Forms.DateTimePicker Dt2;
        public System.Windows.Forms.DateTimePicker Dt1;
    }
}