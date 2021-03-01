namespace ERP_SGE
{
    partial class FrmEnviarEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnviarEmail));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtAnexo = new System.Windows.Forms.TextBox();
            this.BtnEnvia = new System.Windows.Forms.Button();
            this.txtAssunto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCopia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmailDest = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.Anexo = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtAnexo);
            this.panel1.Controls.Add(this.BtnEnvia);
            this.panel1.Controls.Add(this.txtAssunto);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCopia);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtEmailDest);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 109);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 149;
            this.button1.Text = "Anexo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtAnexo
            // 
            this.txtAnexo.Enabled = false;
            this.txtAnexo.Location = new System.Drawing.Point(60, 83);
            this.txtAnexo.MaxLength = 40;
            this.txtAnexo.Name = "txtAnexo";
            this.txtAnexo.Size = new System.Drawing.Size(505, 20);
            this.txtAnexo.TabIndex = 148;
            // 
            // BtnEnvia
            // 
            this.BtnEnvia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEnvia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEnvia.Image = ((System.Drawing.Image)(resources.GetObject("BtnEnvia.Image")));
            this.BtnEnvia.Location = new System.Drawing.Point(571, 62);
            this.BtnEnvia.Name = "BtnEnvia";
            this.BtnEnvia.Size = new System.Drawing.Size(79, 41);
            this.BtnEnvia.TabIndex = 146;
            this.BtnEnvia.Text = "Enviar Email";
            this.BtnEnvia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnEnvia.UseVisualStyleBackColor = true;
            this.BtnEnvia.Click += new System.EventHandler(this.BtnEnvia_Click);
            // 
            // txtAssunto
            // 
            this.txtAssunto.Location = new System.Drawing.Point(60, 57);
            this.txtAssunto.MaxLength = 40;
            this.txtAssunto.Name = "txtAssunto";
            this.txtAssunto.Size = new System.Drawing.Size(505, 20);
            this.txtAssunto.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Assunto:";
            // 
            // txtCopia
            // 
            this.txtCopia.Location = new System.Drawing.Point(60, 31);
            this.txtCopia.MaxLength = 40;
            this.txtCopia.Name = "txtCopia";
            this.txtCopia.Size = new System.Drawing.Size(505, 20);
            this.txtCopia.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Cc.:";
            // 
            // txtEmailDest
            // 
            this.txtEmailDest.Location = new System.Drawing.Point(60, 5);
            this.txtEmailDest.MaxLength = 40;
            this.txtEmailDest.Name = "txtEmailDest";
            this.txtEmailDest.Size = new System.Drawing.Size(505, 20);
            this.txtEmailDest.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Para:";
            // 
            // txtTexto
            // 
            this.txtTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTexto.Location = new System.Drawing.Point(0, 109);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTexto.Size = new System.Drawing.Size(662, 269);
            this.txtTexto.TabIndex = 174;
            // 
            // FrmEnviarEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(662, 378);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEnviarEmail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar Email";
            this.Load += new System.EventHandler(this.FrmEnviarEmail_Load);
            this.Shown += new System.EventHandler(this.FrmEnviarEmail_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtAnexo;
        private System.Windows.Forms.Button BtnEnvia;
        private System.Windows.Forms.OpenFileDialog Anexo;
        public System.Windows.Forms.TextBox txtCopia;
        public System.Windows.Forms.TextBox txtEmailDest;
        public System.Windows.Forms.TextBox txtAssunto;
    }
}