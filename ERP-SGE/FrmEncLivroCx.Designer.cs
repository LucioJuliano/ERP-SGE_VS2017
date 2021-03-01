namespace ERP_SGE
{
    partial class FrmEncLivroCx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEncLivroCx));
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.LstCaixa = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Dt1
            // 
            this.Dt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt1.Location = new System.Drawing.Point(153, 50);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(110, 20);
            this.Dt1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Data p/ Encerramento:";
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(70, 85);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(181, 30);
            this.BtnConfirmar.TabIndex = 124;
            this.BtnConfirmar.Text = "Confirmar Encerramento";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(10, 3);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(105, 13);
            this.label24.TabIndex = 129;
            this.label24.Text = "Caixa Financeiro:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstCaixa
            // 
            this.LstCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstCaixa.FormattingEnabled = true;
            this.LstCaixa.Location = new System.Drawing.Point(12, 19);
            this.LstCaixa.Name = "LstCaixa";
            this.LstCaixa.Size = new System.Drawing.Size(301, 21);
            this.LstCaixa.TabIndex = 128;
            // 
            // FrmEncLivroCx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(327, 136);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.LstCaixa);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Dt1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEncLivroCx";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Encerramento Diario do Livro Caixa";
            this.Load += new System.EventHandler(this.FrmEncLivroCx_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker Dt1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox LstCaixa;
    }
}