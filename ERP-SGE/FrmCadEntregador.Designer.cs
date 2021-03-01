namespace ERP_SGE
{
    partial class FrmCadEntregador
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadEntregador));
            this.TxtFone = new System.Windows.Forms.MaskedTextBox();
            this.TxtCelular = new System.Windows.Forms.MaskedTextBox();
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.ChkNome = new System.Windows.Forms.RadioButton();
            this.ChkCodigo = new System.Windows.Forms.RadioButton();
            this.Paginas = new System.Windows.Forms.TabControl();
            this.Pag01 = new System.Windows.Forms.TabPage();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.IdUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxPesquisa = new System.Windows.Forms.GroupBox();
            this.BtnPesquisa = new System.Windows.Forms.Button();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Pag02 = new System.Windows.Forms.TabPage();
            this.Ck_MapaEntrega = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtEntregador = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BoxItemPesq.SuspendLayout();
            this.Paginas.SuspendLayout();
            this.Pag01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.BoxPesquisa.SuspendLayout();
            this.Pag02.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtFone
            // 
            this.TxtFone.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.TxtFone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFone.Location = new System.Drawing.Point(70, 53);
            this.TxtFone.Mask = "(00)0000-0000";
            this.TxtFone.Name = "TxtFone";
            this.TxtFone.Size = new System.Drawing.Size(95, 20);
            this.TxtFone.TabIndex = 125;
            this.TxtFone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // TxtCelular
            // 
            this.TxtCelular.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.TxtCelular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCelular.Location = new System.Drawing.Point(222, 53);
            this.TxtCelular.Mask = "(00)00000-0000";
            this.TxtCelular.Name = "TxtCelular";
            this.TxtCelular.Size = new System.Drawing.Size(95, 20);
            this.TxtCelular.TabIndex = 126;
            this.TxtCelular.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
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
            this.ChkNome.Size = new System.Drawing.Size(87, 17);
            this.ChkNome.TabIndex = 8;
            this.ChkNome.TabStop = true;
            this.ChkNome.Text = "Entregador";
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
            // Paginas
            // 
            this.Paginas.Controls.Add(this.Pag01);
            this.Paginas.Controls.Add(this.Pag02);
            this.Paginas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Paginas.Location = new System.Drawing.Point(0, 0);
            this.Paginas.Name = "Paginas";
            this.Paginas.SelectedIndex = 0;
            this.Paginas.Size = new System.Drawing.Size(898, 349);
            this.Paginas.TabIndex = 9;
            this.Paginas.SelectedIndexChanged += new System.EventHandler(this.Paginas_SelectedIndexChanged);
            // 
            // Pag01
            // 
            this.Pag01.Controls.Add(this.GridDados);
            this.Pag01.Controls.Add(this.BoxPesquisa);
            this.Pag01.Location = new System.Drawing.Point(4, 22);
            this.Pag01.Name = "Pag01";
            this.Pag01.Padding = new System.Windows.Forms.Padding(3);
            this.Pag01.Size = new System.Drawing.Size(890, 323);
            this.Pag01.TabIndex = 0;
            this.Pag01.Text = "Pesquisa";
            this.Pag01.UseVisualStyleBackColor = true;
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
            this.GridDados.Size = new System.Drawing.Size(884, 264);
            this.GridDados.TabIndex = 4;
            this.GridDados.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_DoubleClick);
            // 
            // IdUF
            // 
            this.IdUF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdUF.DataPropertyName = "Id_Entregador";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "000";
            this.IdUF.DefaultCellStyle = dataGridViewCellStyle1;
            this.IdUF.HeaderText = "Código";
            this.IdUF.Name = "IdUF";
            this.IdUF.ReadOnly = true;
            this.IdUF.Width = 50;
            // 
            // DsRota
            // 
            this.DsRota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DsRota.DataPropertyName = "Entregador";
            this.DsRota.HeaderText = "Entregador";
            this.DsRota.Name = "DsRota";
            this.DsRota.ReadOnly = true;
            this.DsRota.Width = 300;
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
            this.BoxPesquisa.Size = new System.Drawing.Size(884, 53);
            this.BoxPesquisa.TabIndex = 2;
            this.BoxPesquisa.TabStop = false;
            this.BoxPesquisa.Text = "Pesquisa";
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
            // Pag02
            // 
            this.Pag02.BackColor = System.Drawing.Color.White;
            this.Pag02.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.Pag02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pag02.Controls.Add(this.Ck_MapaEntrega);
            this.Pag02.Controls.Add(this.TxtCelular);
            this.Pag02.Controls.Add(this.label18);
            this.Pag02.Controls.Add(this.TxtFone);
            this.Pag02.Controls.Add(this.label14);
            this.Pag02.Controls.Add(this.TxtEntregador);
            this.Pag02.Controls.Add(this.label2);
            this.Pag02.Controls.Add(this.TxtCodigo);
            this.Pag02.Controls.Add(this.label1);
            this.Pag02.Location = new System.Drawing.Point(4, 22);
            this.Pag02.Name = "Pag02";
            this.Pag02.Padding = new System.Windows.Forms.Padding(3);
            this.Pag02.Size = new System.Drawing.Size(890, 323);
            this.Pag02.TabIndex = 1;
            this.Pag02.Text = "Pagina de Dados";
            // 
            // Ck_MapaEntrega
            // 
            this.Ck_MapaEntrega.AutoSize = true;
            this.Ck_MapaEntrega.BackColor = System.Drawing.Color.Transparent;
            this.Ck_MapaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_MapaEntrega.Location = new System.Drawing.Point(23, 94);
            this.Ck_MapaEntrega.Name = "Ck_MapaEntrega";
            this.Ck_MapaEntrega.Size = new System.Drawing.Size(261, 17);
            this.Ck_MapaEntrega.TabIndex = 130;
            this.Ck_MapaEntrega.Text = "Entra na Verificação do Mapa de Entrega";
            this.Ck_MapaEntrega.UseVisualStyleBackColor = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(174, 57);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 129;
            this.label18.Text = "Celular:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(27, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 128;
            this.label14.Text = "Fones:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtEntregador
            // 
            this.TxtEntregador.Location = new System.Drawing.Point(208, 16);
            this.TxtEntregador.MaxLength = 60;
            this.TxtEntregador.Name = "TxtEntregador";
            this.TxtEntregador.Size = new System.Drawing.Size(281, 20);
            this.TxtEntregador.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(135, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Entregador:";
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
            // FrmCadEntregador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 349);
            this.Controls.Add(this.Paginas);
            this.KeyPreview = true;
            this.Name = "FrmCadEntregador";
            this.Text = "Cadastro dos Entregadores";
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.Frm_Load);
            this.Activated += new System.EventHandler(this.Frm_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            this.Paginas.ResumeLayout(false);
            this.Pag01.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.BoxPesquisa.ResumeLayout(false);
            this.BoxPesquisa.PerformLayout();
            this.Pag02.ResumeLayout(false);
            this.Pag02.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox TxtFone;
        private System.Windows.Forms.MaskedTextBox TxtCelular;
        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.RadioButton ChkNome;
        private System.Windows.Forms.RadioButton ChkCodigo;
        private System.Windows.Forms.TabControl Paginas;
        private System.Windows.Forms.TabPage Pag01;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsRota;
        private System.Windows.Forms.GroupBox BoxPesquisa;
        private System.Windows.Forms.Button BtnPesquisa;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage Pag02;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TxtEntregador;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Ck_MapaEntrega;
    }
}