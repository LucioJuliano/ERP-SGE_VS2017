namespace ERP_SGE
{
    partial class FrmAutDebitoPessoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutDebitoPessoa));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtIdVenda = new System.Windows.Forms.NumericUpDown();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.TxtVlrLiberado = new System.Windows.Forms.NumericUpDown();
            this.LblVlrLiberacao = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.BoxTipo = new System.Windows.Forms.GroupBox();
            this.Rb_PessoaF = new System.Windows.Forms.RadioButton();
            this.Rb_Todos = new System.Windows.Forms.RadioButton();
            this.Rb_Financeira = new System.Windows.Forms.RadioButton();
            this.Rb_Parcela = new System.Windows.Forms.RadioButton();
            this.Ck_NaoPrzPg = new System.Windows.Forms.CheckBox();
            this.Rb_PrimeiraCompra = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.TxtIdVenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrLiberado)).BeginInit();
            this.BoxTipo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Informe o Nº da Venda:";
            // 
            // TxtIdVenda
            // 
            this.TxtIdVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIdVenda.Location = new System.Drawing.Point(187, 138);
            this.TxtIdVenda.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.TxtIdVenda.Name = "TxtIdVenda";
            this.TxtIdVenda.Size = new System.Drawing.Size(78, 23);
            this.TxtIdVenda.TabIndex = 1;
            this.TxtIdVenda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.ForeColor = System.Drawing.Color.Maroon;
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(159, 223);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(107, 37);
            this.BtnConfirmar.TabIndex = 3;
            this.BtnConfirmar.Text = "Confirmar";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // TxtVlrLiberado
            // 
            this.TxtVlrLiberado.DecimalPlaces = 2;
            this.TxtVlrLiberado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVlrLiberado.Location = new System.Drawing.Point(158, 177);
            this.TxtVlrLiberado.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.TxtVlrLiberado.Name = "TxtVlrLiberado";
            this.TxtVlrLiberado.Size = new System.Drawing.Size(108, 23);
            this.TxtVlrLiberado.TabIndex = 2;
            this.TxtVlrLiberado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LblVlrLiberacao
            // 
            this.LblVlrLiberacao.AutoSize = true;
            this.LblVlrLiberacao.BackColor = System.Drawing.Color.Transparent;
            this.LblVlrLiberacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVlrLiberacao.Location = new System.Drawing.Point(13, 180);
            this.LblVlrLiberacao.Name = "LblVlrLiberacao";
            this.LblVlrLiberacao.Size = new System.Drawing.Size(145, 17);
            this.LblVlrLiberacao.TabIndex = 147;
            this.LblVlrLiberacao.Text = "Valor Liberado R$:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label34.Location = new System.Drawing.Point(0, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(37, 13);
            this.label34.TabIndex = 170;
            this.label34.Text = "Filial:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia"});
            this.LstFilial.Location = new System.Drawing.Point(38, 10);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(245, 21);
            this.LstFilial.TabIndex = 5;
            // 
            // BoxTipo
            // 
            this.BoxTipo.BackColor = System.Drawing.Color.Transparent;
            this.BoxTipo.Controls.Add(this.Ck_NaoPrzPg);
            this.BoxTipo.Controls.Add(this.Rb_PrimeiraCompra);
            this.BoxTipo.Controls.Add(this.Rb_PessoaF);
            this.BoxTipo.Controls.Add(this.Rb_Todos);
            this.BoxTipo.Controls.Add(this.Rb_Financeira);
            this.BoxTipo.Controls.Add(this.Rb_Parcela);
            this.BoxTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxTipo.Location = new System.Drawing.Point(6, 37);
            this.BoxTipo.Name = "BoxTipo";
            this.BoxTipo.Size = new System.Drawing.Size(407, 84);
            this.BoxTipo.TabIndex = 4;
            this.BoxTipo.TabStop = false;
            this.BoxTipo.Text = "Tipo de Liberação";
            // 
            // Rb_PessoaF
            // 
            this.Rb_PessoaF.AutoSize = true;
            this.Rb_PessoaF.Location = new System.Drawing.Point(240, 14);
            this.Rb_PessoaF.Name = "Rb_PessoaF";
            this.Rb_PessoaF.Size = new System.Drawing.Size(103, 17);
            this.Rb_PessoaF.TabIndex = 4;
            this.Rb_PessoaF.TabStop = true;
            this.Rb_PessoaF.Text = "Pessoa Fisica";
            this.Rb_PessoaF.UseVisualStyleBackColor = true;
            // 
            // Rb_Todos
            // 
            this.Rb_Todos.AutoSize = true;
            this.Rb_Todos.Location = new System.Drawing.Point(349, 14);
            this.Rb_Todos.Name = "Rb_Todos";
            this.Rb_Todos.Size = new System.Drawing.Size(60, 17);
            this.Rb_Todos.TabIndex = 3;
            this.Rb_Todos.TabStop = true;
            this.Rb_Todos.Text = "Todos";
            this.Rb_Todos.UseVisualStyleBackColor = true;
            this.Rb_Todos.Visible = false;
            // 
            // Rb_Financeira
            // 
            this.Rb_Financeira.AutoSize = true;
            this.Rb_Financeira.Location = new System.Drawing.Point(7, 14);
            this.Rb_Financeira.Name = "Rb_Financeira";
            this.Rb_Financeira.Size = new System.Drawing.Size(84, 17);
            this.Rb_Financeira.TabIndex = 2;
            this.Rb_Financeira.TabStop = true;
            this.Rb_Financeira.Text = "Financeiro";
            this.Rb_Financeira.UseVisualStyleBackColor = true;
            // 
            // Rb_Parcela
            // 
            this.Rb_Parcela.AutoSize = true;
            this.Rb_Parcela.Location = new System.Drawing.Point(92, 14);
            this.Rb_Parcela.Name = "Rb_Parcela";
            this.Rb_Parcela.Size = new System.Drawing.Size(142, 17);
            this.Rb_Parcela.TabIndex = 0;
            this.Rb_Parcela.TabStop = true;
            this.Rb_Parcela.Text = "Prazo de Pagamento";
            this.Rb_Parcela.UseVisualStyleBackColor = true;
            // 
            // Ck_NaoPrzPg
            // 
            this.Ck_NaoPrzPg.AutoSize = true;
            this.Ck_NaoPrzPg.BackColor = System.Drawing.Color.Transparent;
            this.Ck_NaoPrzPg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_NaoPrzPg.Location = new System.Drawing.Point(7, 61);
            this.Ck_NaoPrzPg.Name = "Ck_NaoPrzPg";
            this.Ck_NaoPrzPg.Size = new System.Drawing.Size(217, 17);
            this.Ck_NaoPrzPg.TabIndex = 188;
            this.Ck_NaoPrzPg.Text = "Não Verifica Prazo de Pagamento";
            this.Ck_NaoPrzPg.UseVisualStyleBackColor = false;
            // 
            // Rb_PrimeiraCompra
            // 
            this.Rb_PrimeiraCompra.AutoSize = true;
            this.Rb_PrimeiraCompra.Location = new System.Drawing.Point(7, 37);
            this.Rb_PrimeiraCompra.Name = "Rb_PrimeiraCompra";
            this.Rb_PrimeiraCompra.Size = new System.Drawing.Size(203, 17);
            this.Rb_PrimeiraCompra.TabIndex = 5;
            this.Rb_PrimeiraCompra.TabStop = true;
            this.Rb_PrimeiraCompra.Text = "Primeira Compra ou Reativação";
            this.Rb_PrimeiraCompra.UseVisualStyleBackColor = true;
            // 
            // FrmAutDebitoPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(445, 281);
            this.Controls.Add(this.BoxTipo);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.LstFilial);
            this.Controls.Add(this.LblVlrLiberacao);
            this.Controls.Add(this.TxtVlrLiberado);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.TxtIdVenda);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAutDebitoPessoa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liberação de débito";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.Shown += new System.EventHandler(this.FrmAutDebitoPessoa_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.TxtIdVenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrLiberado)).EndInit();
            this.BoxTipo.ResumeLayout(false);
            this.BoxTipo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown TxtIdVenda;
        public System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.NumericUpDown TxtVlrLiberado;
        private System.Windows.Forms.Label LblVlrLiberacao;
        public System.Windows.Forms.Label label34;
        public System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.GroupBox BoxTipo;
        private System.Windows.Forms.RadioButton Rb_Financeira;
        private System.Windows.Forms.RadioButton Rb_Parcela;
        private System.Windows.Forms.RadioButton Rb_Todos;
        private System.Windows.Forms.CheckBox Ck_NaoPrzPg;
        private System.Windows.Forms.RadioButton Rb_PessoaF;
        private System.Windows.Forms.RadioButton Rb_PrimeiraCompra;
    }
}