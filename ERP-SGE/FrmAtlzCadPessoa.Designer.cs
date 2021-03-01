namespace ERP_SGE
{
    partial class FrmAtlzCadPessoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAtlzCadPessoa));
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.TxtCelular = new System.Windows.Forms.MaskedTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.TxtFax = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TxtFone = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.LstAtividade = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtEmail
            // 
            this.TxtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.TxtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmail.Location = new System.Drawing.Point(54, 12);
            this.TxtEmail.MaxLength = 200;
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(345, 20);
            this.TxtEmail.TabIndex = 169;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(12, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 13);
            this.label16.TabIndex = 170;
            this.label16.Text = "Email:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.ForeColor = System.Drawing.Color.Maroon;
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(292, 97);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(107, 23);
            this.BtnConfirmar.TabIndex = 171;
            this.BtnConfirmar.Text = "Confirmar";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // TxtCelular
            // 
            this.TxtCelular.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.TxtCelular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCelular.Location = new System.Drawing.Point(305, 38);
            this.TxtCelular.Mask = "(00)00000-0000";
            this.TxtCelular.Name = "TxtCelular";
            this.TxtCelular.Size = new System.Drawing.Size(92, 20);
            this.TxtCelular.TabIndex = 175;
            this.TxtCelular.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(257, 42);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 179;
            this.label18.Text = "Celular:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtFax
            // 
            this.TxtFax.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.TxtFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFax.Location = new System.Drawing.Point(170, 38);
            this.TxtFax.Mask = "(00)0000-0000";
            this.TxtFax.Name = "TxtFax";
            this.TxtFax.Size = new System.Drawing.Size(85, 20);
            this.TxtFax.TabIndex = 174;
            this.TxtFax.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(140, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 177;
            this.label15.Text = "Fax:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtFone
            // 
            this.TxtFone.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.TxtFone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFone.Location = new System.Drawing.Point(54, 38);
            this.TxtFone.Mask = "(00)0000-0000";
            this.TxtFone.Name = "TxtFone";
            this.TxtFone.Size = new System.Drawing.Size(80, 20);
            this.TxtFone.TabIndex = 173;
            this.TxtFone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(17, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 176;
            this.label14.Text = "Fone:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstAtividade
            // 
            this.LstAtividade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstAtividade.FormattingEnabled = true;
            this.LstAtividade.Location = new System.Drawing.Point(68, 64);
            this.LstAtividade.Name = "LstAtividade";
            this.LstAtividade.Size = new System.Drawing.Size(329, 21);
            this.LstAtividade.TabIndex = 180;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(4, 69);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 13);
            this.label22.TabIndex = 181;
            this.label22.Text = "Atividade:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmAtlzCadPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(420, 131);
            this.Controls.Add(this.LstAtividade);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.TxtCelular);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.TxtFax);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.TxtFone);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.TxtEmail);
            this.Controls.Add(this.label16);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAtlzCadPessoa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualização dos Dados Cadastrais";
            this.Load += new System.EventHandler(this.FrmAtlzCadPessoa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.MaskedTextBox TxtCelular;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.MaskedTextBox TxtFax;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox TxtFone;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox LstAtividade;
        private System.Windows.Forms.Label label22;
    }
}