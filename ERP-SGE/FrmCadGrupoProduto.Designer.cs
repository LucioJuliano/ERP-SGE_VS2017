namespace ERP_SGE
{
    partial class FrmCadGrupoProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadGrupoProduto));
            this.Paginas = new System.Windows.Forms.TabControl();
            this.Pag01 = new System.Windows.Forms.TabPage();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.IdUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxPesquisa = new System.Windows.Forms.GroupBox();
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.ChkNome = new System.Windows.Forms.RadioButton();
            this.ChkCodigo = new System.Windows.Forms.RadioButton();
            this.BtnPesquisa = new System.Windows.Forms.Button();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Pag02 = new System.Windows.Forms.TabPage();
            this.BtnReajuste = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPercVerDesc = new System.Windows.Forms.NumericUpDown();
            this.Ck_Ativo = new System.Windows.Forms.CheckBox();
            this.Ck_NaoEstoque = new System.Windows.Forms.CheckBox();
            this.Ck_ExcNfeTransf = new System.Windows.Forms.CheckBox();
            this.Ck_ListaVenda = new System.Windows.Forms.CheckBox();
            this.Ck_ListaEstMin = new System.Windows.Forms.CheckBox();
            this.Ck_ListaWeb = new System.Windows.Forms.CheckBox();
            this.TxtGrupo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Paginas.SuspendLayout();
            this.Pag01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.BoxPesquisa.SuspendLayout();
            this.BoxItemPesq.SuspendLayout();
            this.Pag02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPercVerDesc)).BeginInit();
            this.SuspendLayout();
            // 
            // Paginas
            // 
            this.Paginas.Controls.Add(this.Pag01);
            this.Paginas.Controls.Add(this.Pag02);
            this.Paginas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Paginas.Location = new System.Drawing.Point(0, 0);
            this.Paginas.Name = "Paginas";
            this.Paginas.SelectedIndex = 0;
            this.Paginas.Size = new System.Drawing.Size(852, 429);
            this.Paginas.TabIndex = 6;
            this.Paginas.SelectedIndexChanged += new System.EventHandler(this.Paginas_SelectedIndexChanged);
            // 
            // Pag01
            // 
            this.Pag01.Controls.Add(this.GridDados);
            this.Pag01.Controls.Add(this.BoxPesquisa);
            this.Pag01.Location = new System.Drawing.Point(4, 22);
            this.Pag01.Name = "Pag01";
            this.Pag01.Padding = new System.Windows.Forms.Padding(3);
            this.Pag01.Size = new System.Drawing.Size(844, 403);
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
            this.GridDados.Size = new System.Drawing.Size(838, 344);
            this.GridDados.TabIndex = 3;
            this.GridDados.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_DoubleClick);
            // 
            // IdUF
            // 
            this.IdUF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdUF.DataPropertyName = "Id_Grupo";
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
            this.DsRota.DataPropertyName = "Grupo";
            this.DsRota.HeaderText = "Nome do Grupo";
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
            this.BoxPesquisa.Size = new System.Drawing.Size(838, 53);
            this.BoxPesquisa.TabIndex = 2;
            this.BoxPesquisa.TabStop = false;
            this.BoxPesquisa.Text = "Pesquisa";
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
            this.ChkNome.Location = new System.Drawing.Point(6, 22);
            this.ChkNome.Name = "ChkNome";
            this.ChkNome.Size = new System.Drawing.Size(59, 17);
            this.ChkNome.TabIndex = 8;
            this.ChkNome.TabStop = true;
            this.ChkNome.Text = "Grupo";
            this.ChkNome.UseVisualStyleBackColor = true;
            // 
            // ChkCodigo
            // 
            this.ChkCodigo.AutoSize = true;
            this.ChkCodigo.Location = new System.Drawing.Point(6, 5);
            this.ChkCodigo.Name = "ChkCodigo";
            this.ChkCodigo.Size = new System.Drawing.Size(64, 17);
            this.ChkCodigo.TabIndex = 7;
            this.ChkCodigo.TabStop = true;
            this.ChkCodigo.Text = "Código";
            this.ChkCodigo.UseVisualStyleBackColor = true;
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
            this.Pag02.Controls.Add(this.BtnReajuste);
            this.Pag02.Controls.Add(this.label2);
            this.Pag02.Controls.Add(this.TxtPercVerDesc);
            this.Pag02.Controls.Add(this.Ck_Ativo);
            this.Pag02.Controls.Add(this.Ck_NaoEstoque);
            this.Pag02.Controls.Add(this.Ck_ExcNfeTransf);
            this.Pag02.Controls.Add(this.Ck_ListaVenda);
            this.Pag02.Controls.Add(this.Ck_ListaEstMin);
            this.Pag02.Controls.Add(this.Ck_ListaWeb);
            this.Pag02.Controls.Add(this.TxtGrupo);
            this.Pag02.Controls.Add(this.label5);
            this.Pag02.Controls.Add(this.TxtCodigo);
            this.Pag02.Controls.Add(this.label1);
            this.Pag02.Location = new System.Drawing.Point(4, 22);
            this.Pag02.Name = "Pag02";
            this.Pag02.Padding = new System.Windows.Forms.Padding(3);
            this.Pag02.Size = new System.Drawing.Size(844, 403);
            this.Pag02.TabIndex = 1;
            this.Pag02.Text = "Pagina de Dados";
            // 
            // BtnReajuste
            // 
            this.BtnReajuste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnReajuste.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReajuste.Image = ((System.Drawing.Image)(resources.GetObject("BtnReajuste.Image")));
            this.BtnReajuste.Location = new System.Drawing.Point(23, 218);
            this.BtnReajuste.Name = "BtnReajuste";
            this.BtnReajuste.Size = new System.Drawing.Size(150, 47);
            this.BtnReajuste.TabIndex = 189;
            this.BtnReajuste.Text = "Reajuste de Preço de Venda";
            this.BtnReajuste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnReajuste.UseVisualStyleBackColor = true;
            this.BtnReajuste.Click += new System.EventHandler(this.BtnReajuste_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(360, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "% Verificação de Preço";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtPercVerDesc
            // 
            this.TxtPercVerDesc.DecimalPlaces = 2;
            this.TxtPercVerDesc.Location = new System.Drawing.Point(392, 41);
            this.TxtPercVerDesc.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtPercVerDesc.Name = "TxtPercVerDesc";
            this.TxtPercVerDesc.Size = new System.Drawing.Size(80, 20);
            this.TxtPercVerDesc.TabIndex = 43;
            this.TxtPercVerDesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPercVerDesc.ThousandsSeparator = true;
            // 
            // Ck_Ativo
            // 
            this.Ck_Ativo.AutoSize = true;
            this.Ck_Ativo.BackColor = System.Drawing.Color.Transparent;
            this.Ck_Ativo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_Ativo.Location = new System.Drawing.Point(134, 19);
            this.Ck_Ativo.Name = "Ck_Ativo";
            this.Ck_Ativo.Size = new System.Drawing.Size(93, 17);
            this.Ck_Ativo.TabIndex = 39;
            this.Ck_Ativo.Text = "Grupo Ativo";
            this.Ck_Ativo.UseVisualStyleBackColor = false;
            // 
            // Ck_NaoEstoque
            // 
            this.Ck_NaoEstoque.AutoSize = true;
            this.Ck_NaoEstoque.BackColor = System.Drawing.Color.Transparent;
            this.Ck_NaoEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_NaoEstoque.Location = new System.Drawing.Point(23, 156);
            this.Ck_NaoEstoque.Name = "Ck_NaoEstoque";
            this.Ck_NaoEstoque.Size = new System.Drawing.Size(262, 17);
            this.Ck_NaoEstoque.TabIndex = 38;
            this.Ck_NaoEstoque.Text = "Não Verificar Saldo de Estoque na Venda";
            this.Ck_NaoEstoque.UseVisualStyleBackColor = false;
            // 
            // Ck_ExcNfeTransf
            // 
            this.Ck_ExcNfeTransf.AutoSize = true;
            this.Ck_ExcNfeTransf.BackColor = System.Drawing.Color.Transparent;
            this.Ck_ExcNfeTransf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_ExcNfeTransf.Location = new System.Drawing.Point(23, 179);
            this.Ck_ExcNfeTransf.Name = "Ck_ExcNfeTransf";
            this.Ck_ExcNfeTransf.Size = new System.Drawing.Size(247, 17);
            this.Ck_ExcNfeTransf.TabIndex = 37;
            this.Ck_ExcNfeTransf.Text = "Excluir Grupo da NFe de Transferencia";
            this.Ck_ExcNfeTransf.UseVisualStyleBackColor = false;
            // 
            // Ck_ListaVenda
            // 
            this.Ck_ListaVenda.AutoSize = true;
            this.Ck_ListaVenda.BackColor = System.Drawing.Color.Transparent;
            this.Ck_ListaVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_ListaVenda.Location = new System.Drawing.Point(23, 110);
            this.Ck_ListaVenda.Name = "Ck_ListaVenda";
            this.Ck_ListaVenda.Size = new System.Drawing.Size(165, 17);
            this.Ck_ListaVenda.TabIndex = 36;
            this.Ck_ListaVenda.Text = "Lista Produtos na Venda";
            this.Ck_ListaVenda.UseVisualStyleBackColor = false;
            // 
            // Ck_ListaEstMin
            // 
            this.Ck_ListaEstMin.AutoSize = true;
            this.Ck_ListaEstMin.BackColor = System.Drawing.Color.Transparent;
            this.Ck_ListaEstMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_ListaEstMin.Location = new System.Drawing.Point(23, 133);
            this.Ck_ListaEstMin.Name = "Ck_ListaEstMin";
            this.Ck_ListaEstMin.Size = new System.Drawing.Size(146, 17);
            this.Ck_ListaEstMin.TabIndex = 35;
            this.Ck_ListaEstMin.Text = "Lista Estoque Minimo";
            this.Ck_ListaEstMin.UseVisualStyleBackColor = false;
            // 
            // Ck_ListaWeb
            // 
            this.Ck_ListaWeb.AutoSize = true;
            this.Ck_ListaWeb.BackColor = System.Drawing.Color.Transparent;
            this.Ck_ListaWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ck_ListaWeb.Location = new System.Drawing.Point(23, 87);
            this.Ck_ListaWeb.Name = "Ck_ListaWeb";
            this.Ck_ListaWeb.Size = new System.Drawing.Size(149, 17);
            this.Ck_ListaWeb.TabIndex = 34;
            this.Ck_ListaWeb.Text = "Lista no sistema WEB";
            this.Ck_ListaWeb.UseVisualStyleBackColor = false;
            // 
            // TxtGrupo
            // 
            this.TxtGrupo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGrupo.Location = new System.Drawing.Point(70, 41);
            this.TxtGrupo.MaxLength = 40;
            this.TxtGrupo.Name = "TxtGrupo";
            this.TxtGrupo.Size = new System.Drawing.Size(277, 20);
            this.TxtGrupo.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(25, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Grupo:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // FrmCadGrupoProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 429);
            this.Controls.Add(this.Paginas);
            this.KeyPreview = true;
            this.Name = "FrmCadGrupoProduto";
            this.Text = "Cadastro do Grupo de Produto";
            this.Activated += new System.EventHandler(this.Frm_Activated);
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.Frm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.Paginas.ResumeLayout(false);
            this.Pag01.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.BoxPesquisa.ResumeLayout(false);
            this.BoxPesquisa.PerformLayout();
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            this.Pag02.ResumeLayout(false);
            this.Pag02.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPercVerDesc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.TabControl Paginas;
        private System.Windows.Forms.TabPage Pag01;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.GroupBox BoxPesquisa;
        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.RadioButton ChkNome;
        private System.Windows.Forms.RadioButton ChkCodigo;
        private System.Windows.Forms.Button BtnPesquisa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage Pag02;
        private System.Windows.Forms.TextBox TxtGrupo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsRota;
        private System.Windows.Forms.CheckBox Ck_ListaEstMin;
        private System.Windows.Forms.CheckBox Ck_ListaWeb;
        private System.Windows.Forms.CheckBox Ck_ListaVenda;
        private System.Windows.Forms.CheckBox Ck_ExcNfeTransf;
        private System.Windows.Forms.CheckBox Ck_NaoEstoque;
        private System.Windows.Forms.CheckBox Ck_Ativo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown TxtPercVerDesc;
        private System.Windows.Forms.Button BtnReajuste;
    }
}