namespace ERP_SGE
{
    partial class FrmCadContaCaixa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadContaCaixa));
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.ChkNome = new System.Windows.Forms.RadioButton();
            this.ChkCodigo = new System.Windows.Forms.RadioButton();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.IdUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pag02 = new System.Windows.Forms.TabPage();
            this.TxtCaixa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPesquisa = new System.Windows.Forms.Button();
            this.Paginas = new System.Windows.Forms.TabControl();
            this.Pag01 = new System.Windows.Forms.TabPage();
            this.BoxPesquisa = new System.Windows.Forms.GroupBox();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BoxItemPesq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.Pag02.SuspendLayout();
            this.Paginas.SuspendLayout();
            this.Pag01.SuspendLayout();
            this.BoxPesquisa.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.Controls.Add(this.ChkNome);
            this.BoxItemPesq.Controls.Add(this.ChkCodigo);
            this.BoxItemPesq.Location = new System.Drawing.Point(4, 10);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(116, 40);
            this.BoxItemPesq.TabIndex = 6;
            this.BoxItemPesq.TabStop = false;
            // 
            // ChkNome
            // 
            this.ChkNome.AutoSize = true;
            this.ChkNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNome.Location = new System.Drawing.Point(6, 22);
            this.ChkNome.Name = "ChkNome";
            this.ChkNome.Size = new System.Drawing.Size(56, 17);
            this.ChkNome.TabIndex = 8;
            this.ChkNome.TabStop = true;
            this.ChkNome.Text = "Caixa";
            this.ChkNome.UseVisualStyleBackColor = true;
            // 
            // ChkCodigo
            // 
            this.ChkCodigo.AutoSize = true;
            this.ChkCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCodigo.Location = new System.Drawing.Point(6, 5);
            this.ChkCodigo.Name = "ChkCodigo";
            this.ChkCodigo.Size = new System.Drawing.Size(64, 17);
            this.ChkCodigo.TabIndex = 7;
            this.ChkCodigo.TabStop = true;
            this.ChkCodigo.Text = "Código";
            this.ChkCodigo.UseVisualStyleBackColor = true;
            // 
            // GridDados
            // 
            this.GridDados.AllowUserToAddRows = false;
            this.GridDados.AllowUserToDeleteRows = false;
            this.GridDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdUF,
            this.DsRota});
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(3, 56);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            this.GridDados.ReadOnly = true;
            this.GridDados.Size = new System.Drawing.Size(808, 337);
            this.GridDados.TabIndex = 4;
            this.GridDados.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_DoubleClick);
            // 
            // IdUF
            // 
            this.IdUF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdUF.DataPropertyName = "Id_Caixa";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "000";
            this.IdUF.DefaultCellStyle = dataGridViewCellStyle2;
            this.IdUF.HeaderText = "Codigo";
            this.IdUF.Name = "IdUF";
            this.IdUF.ReadOnly = true;
            this.IdUF.Width = 50;
            // 
            // DsRota
            // 
            this.DsRota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DsRota.DataPropertyName = "Caixa";
            this.DsRota.HeaderText = "Descrição do Caixa";
            this.DsRota.Name = "DsRota";
            this.DsRota.ReadOnly = true;
            this.DsRota.Width = 300;
            // 
            // Pag02
            // 
            this.Pag02.BackColor = System.Drawing.Color.White;
            this.Pag02.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.Pag02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pag02.Controls.Add(this.TxtCaixa);
            this.Pag02.Controls.Add(this.label2);
            this.Pag02.Controls.Add(this.TxtCodigo);
            this.Pag02.Controls.Add(this.label1);
            this.Pag02.Location = new System.Drawing.Point(4, 22);
            this.Pag02.Name = "Pag02";
            this.Pag02.Padding = new System.Windows.Forms.Padding(3);
            this.Pag02.Size = new System.Drawing.Size(814, 396);
            this.Pag02.TabIndex = 1;
            this.Pag02.Text = "Pagina de Dados";
            // 
            // TxtCaixa
            // 
            this.TxtCaixa.Location = new System.Drawing.Point(180, 16);
            this.TxtCaixa.MaxLength = 40;
            this.TxtCaixa.Name = "TxtCaixa";
            this.TxtCaixa.Size = new System.Drawing.Size(281, 20);
            this.TxtCaixa.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(140, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Caixa:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtCodigo
            // 
            this.TxtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtCodigo.Enabled = false;
            this.TxtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TxtCodigo.Location = new System.Drawing.Point(70, 16);
            this.TxtCodigo.Name = "TxtCodigo";
            this.TxtCodigo.Size = new System.Drawing.Size(58, 20);
            this.TxtCodigo.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Código:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnPesquisa
            // 
            this.BtnPesquisa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPesquisa.BackgroundImage")));
            this.BtnPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnPesquisa.Location = new System.Drawing.Point(526, 15);
            this.BtnPesquisa.Name = "BtnPesquisa";
            this.BtnPesquisa.Size = new System.Drawing.Size(35, 30);
            this.BtnPesquisa.TabIndex = 2;
            this.BtnPesquisa.UseVisualStyleBackColor = true;
            this.BtnPesquisa.Click += new System.EventHandler(this.BtnPesquisa_Click);
            // 
            // Paginas
            // 
            this.Paginas.Controls.Add(this.Pag01);
            this.Paginas.Controls.Add(this.Pag02);
            this.Paginas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Paginas.Location = new System.Drawing.Point(0, 0);
            this.Paginas.Name = "Paginas";
            this.Paginas.SelectedIndex = 0;
            this.Paginas.Size = new System.Drawing.Size(822, 422);
            this.Paginas.TabIndex = 8;
            this.Paginas.SelectedIndexChanged += new System.EventHandler(this.Paginas_SelectedIndexChanged);
            // 
            // Pag01
            // 
            this.Pag01.Controls.Add(this.GridDados);
            this.Pag01.Controls.Add(this.BoxPesquisa);
            this.Pag01.Location = new System.Drawing.Point(4, 22);
            this.Pag01.Name = "Pag01";
            this.Pag01.Padding = new System.Windows.Forms.Padding(3);
            this.Pag01.Size = new System.Drawing.Size(814, 396);
            this.Pag01.TabIndex = 0;
            this.Pag01.Text = "Pesquisa";
            this.Pag01.UseVisualStyleBackColor = true;
            // 
            // BoxPesquisa
            // 
            this.BoxPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.BoxPesquisa.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BoxPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BoxPesquisa.Controls.Add(this.BoxItemPesq);
            this.BoxPesquisa.Controls.Add(this.BtnPesquisa);
            this.BoxPesquisa.Controls.Add(this.TxtPesquisa);
            this.BoxPesquisa.Controls.Add(this.label3);
            this.BoxPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BoxPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxPesquisa.Location = new System.Drawing.Point(3, 3);
            this.BoxPesquisa.Name = "BoxPesquisa";
            this.BoxPesquisa.Size = new System.Drawing.Size(808, 53);
            this.BoxPesquisa.TabIndex = 2;
            this.BoxPesquisa.TabStop = false;
            this.BoxPesquisa.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(188, 22);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(331, 20);
            this.TxtPesquisa.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(125, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Conteúdo:";
            // 
            // FrmCadContaCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 422);
            this.Controls.Add(this.Paginas);
            this.KeyPreview = true;
            this.Name = "FrmCadContaCaixa";
            this.Text = "Cadastro das Contas de Caixa";
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.Frm_Load);
            this.Activated += new System.EventHandler(this.Frm_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.Pag02.ResumeLayout(false);
            this.Pag02.PerformLayout();
            this.Paginas.ResumeLayout(false);
            this.Pag01.ResumeLayout(false);
            this.BoxPesquisa.ResumeLayout(false);
            this.BoxPesquisa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.RadioButton ChkNome;
        private System.Windows.Forms.RadioButton ChkCodigo;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsRota;
        private System.Windows.Forms.TabPage Pag02;
        private System.Windows.Forms.TextBox TxtCaixa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnPesquisa;
        private System.Windows.Forms.TabControl Paginas;
        private System.Windows.Forms.TabPage Pag01;
        private System.Windows.Forms.GroupBox BoxPesquisa;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Label label3;
    }
}