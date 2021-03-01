namespace ERP_SGE
{
    partial class FrmBuscaPessoa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBuscaPessoa));
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.IdRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTpCad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cnpj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NmFantasia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLimite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColContato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColVendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEndereco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDtCad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxPesquisa = new System.Windows.Forms.GroupBox();
            this.BtnUltCompra = new System.Windows.Forms.Button();
            this.BtnFicha = new System.Windows.Forms.Button();
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.LstPesquisa = new System.Windows.Forms.ComboBox();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.BoxPesquisa.SuspendLayout();
            this.BoxItemPesq.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridDados
            // 
            this.GridDados.AllowUserToAddRows = false;
            this.GridDados.AllowUserToDeleteRows = false;
            this.GridDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.GridDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdRota,
            this.ColTpCad,
            this.Cnpj,
            this.DsRota,
            this.NmFantasia,
            this.ColLimite,
            this.ColCredito,
            this.Telefone,
            this.ColContato,
            this.ColVendedor,
            this.ColEndereco,
            this.ColDtCad});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle23;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 53);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            this.GridDados.ReadOnly = true;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.GridDados.Size = new System.Drawing.Size(971, 436);
            this.GridDados.TabIndex = 5;
            this.GridDados.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridDados_CellFormatting);
            this.GridDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridDados_KeyDown);
            // 
            // IdRota
            // 
            this.IdRota.DataPropertyName = "Id_Pessoa";
            dataGridViewCellStyle18.Format = "N0";
            dataGridViewCellStyle18.NullValue = "000";
            this.IdRota.DefaultCellStyle = dataGridViewCellStyle18;
            this.IdRota.HeaderText = "Código";
            this.IdRota.Name = "IdRota";
            this.IdRota.ReadOnly = true;
            this.IdRota.Width = 65;
            // 
            // ColTpCad
            // 
            this.ColTpCad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColTpCad.DataPropertyName = "TipoCad";
            this.ColTpCad.HeaderText = "";
            this.ColTpCad.Name = "ColTpCad";
            this.ColTpCad.ReadOnly = true;
            this.ColTpCad.Width = 90;
            // 
            // Cnpj
            // 
            this.Cnpj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Cnpj.DataPropertyName = "Cnpj";
            this.Cnpj.HeaderText = "No. CNPJ ou CPF";
            this.Cnpj.Name = "Cnpj";
            this.Cnpj.ReadOnly = true;
            this.Cnpj.Width = 120;
            // 
            // DsRota
            // 
            this.DsRota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DsRota.DataPropertyName = "RazaoSocial";
            this.DsRota.HeaderText = "Razão Social";
            this.DsRota.Name = "DsRota";
            this.DsRota.ReadOnly = true;
            this.DsRota.Width = 190;
            // 
            // NmFantasia
            // 
            this.NmFantasia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NmFantasia.DataPropertyName = "Fantasia";
            this.NmFantasia.HeaderText = "Nome Fantasia";
            this.NmFantasia.Name = "NmFantasia";
            this.NmFantasia.ReadOnly = true;
            this.NmFantasia.Width = 190;
            // 
            // ColLimite
            // 
            this.ColLimite.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColLimite.DataPropertyName = "LimiteCredito";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle19.Format = "C2";
            dataGridViewCellStyle19.NullValue = "0,00";
            this.ColLimite.DefaultCellStyle = dataGridViewCellStyle19;
            this.ColLimite.HeaderText = "Limite Crédito";
            this.ColLimite.Name = "ColLimite";
            this.ColLimite.ReadOnly = true;
            this.ColLimite.Width = 95;
            // 
            // ColCredito
            // 
            this.ColCredito.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColCredito.DataPropertyName = "Credito";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle20.Format = "N2";
            dataGridViewCellStyle20.NullValue = "0,00";
            this.ColCredito.DefaultCellStyle = dataGridViewCellStyle20;
            this.ColCredito.HeaderText = "Credito R$";
            this.ColCredito.Name = "ColCredito";
            this.ColCredito.ReadOnly = true;
            this.ColCredito.Width = 90;
            // 
            // Telefone
            // 
            this.Telefone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Telefone.DataPropertyName = "Fone";
            dataGridViewCellStyle21.Format = "(00) 0000-0000";
            dataGridViewCellStyle21.NullValue = null;
            this.Telefone.DefaultCellStyle = dataGridViewCellStyle21;
            this.Telefone.HeaderText = "Telefone";
            this.Telefone.Name = "Telefone";
            this.Telefone.ReadOnly = true;
            this.Telefone.Width = 90;
            // 
            // ColContato
            // 
            this.ColContato.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColContato.DataPropertyName = "Contato";
            this.ColContato.HeaderText = "Contato";
            this.ColContato.Name = "ColContato";
            this.ColContato.ReadOnly = true;
            // 
            // ColVendedor
            // 
            this.ColVendedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColVendedor.DataPropertyName = "Vendedor";
            this.ColVendedor.HeaderText = "Vendedor";
            this.ColVendedor.Name = "ColVendedor";
            this.ColVendedor.ReadOnly = true;
            // 
            // ColEndereco
            // 
            this.ColEndereco.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColEndereco.DataPropertyName = "Logradouro";
            this.ColEndereco.HeaderText = "Endereço";
            this.ColEndereco.Name = "ColEndereco";
            this.ColEndereco.ReadOnly = true;
            this.ColEndereco.Width = 500;
            // 
            // ColDtCad
            // 
            this.ColDtCad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColDtCad.DataPropertyName = "DtCadastro";
            dataGridViewCellStyle22.Format = "d";
            dataGridViewCellStyle22.NullValue = null;
            this.ColDtCad.DefaultCellStyle = dataGridViewCellStyle22;
            this.ColDtCad.HeaderText = "Dt.Cadastro";
            this.ColDtCad.Name = "ColDtCad";
            this.ColDtCad.ReadOnly = true;
            this.ColDtCad.Width = 70;
            // 
            // BoxPesquisa
            // 
            this.BoxPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.BoxPesquisa.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BoxPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BoxPesquisa.Controls.Add(this.BtnUltCompra);
            this.BoxPesquisa.Controls.Add(this.BtnFicha);
            this.BoxPesquisa.Controls.Add(this.BoxItemPesq);
            this.BoxPesquisa.Controls.Add(this.TxtPesquisa);
            this.BoxPesquisa.Controls.Add(this.label3);
            this.BoxPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BoxPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxPesquisa.Location = new System.Drawing.Point(0, 0);
            this.BoxPesquisa.Name = "BoxPesquisa";
            this.BoxPesquisa.Size = new System.Drawing.Size(971, 53);
            this.BoxPesquisa.TabIndex = 4;
            this.BoxPesquisa.TabStop = false;
            this.BoxPesquisa.Text = "Pesquisa";
            // 
            // BtnUltCompra
            // 
            this.BtnUltCompra.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnUltCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUltCompra.Location = new System.Drawing.Point(684, 10);
            this.BtnUltCompra.Name = "BtnUltCompra";
            this.BtnUltCompra.Size = new System.Drawing.Size(116, 35);
            this.BtnUltCompra.TabIndex = 8;
            this.BtnUltCompra.Text = "Ultima Compra do Cliente";
            this.BtnUltCompra.UseVisualStyleBackColor = true;
            this.BtnUltCompra.Click += new System.EventHandler(this.BtnUltCompra_Click);
            // 
            // BtnFicha
            // 
            this.BtnFicha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnFicha.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFicha.Image = ((System.Drawing.Image)(resources.GetObject("BtnFicha.Image")));
            this.BtnFicha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFicha.Location = new System.Drawing.Point(511, 10);
            this.BtnFicha.Name = "BtnFicha";
            this.BtnFicha.Size = new System.Drawing.Size(134, 35);
            this.BtnFicha.TabIndex = 7;
            this.BtnFicha.Text = "Ficha Financeira";
            this.BtnFicha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnFicha.UseVisualStyleBackColor = true;
            this.BtnFicha.Click += new System.EventHandler(this.BtnFicha_Click);
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.Controls.Add(this.LstPesquisa);
            this.BoxItemPesq.Location = new System.Drawing.Point(4, 10);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(153, 40);
            this.BoxItemPesq.TabIndex = 6;
            this.BoxItemPesq.TabStop = false;
            // 
            // LstPesquisa
            // 
            this.LstPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstPesquisa.FormattingEnabled = true;
            this.LstPesquisa.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia",
            "Endereço",
            "Telefone"});
            this.LstPesquisa.Location = new System.Drawing.Point(6, 12);
            this.LstPesquisa.Name = "LstPesquisa";
            this.LstPesquisa.Size = new System.Drawing.Size(136, 21);
            this.LstPesquisa.TabIndex = 0;
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(225, 24);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(280, 20);
            this.TxtPesquisa.TabIndex = 1;
            this.TxtPesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPesquisa_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(163, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Conteúdo:";
            // 
            // FrmBuscaPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 489);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.BoxPesquisa);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBuscaPessoa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pesquisa de pessoas";
            this.Load += new System.EventHandler(this.Frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.BoxPesquisa.ResumeLayout(false);
            this.BoxPesquisa.PerformLayout();
            this.BoxItemPesq.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.GroupBox BoxPesquisa;
        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.ComboBox LstPesquisa;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnFicha;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRota;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTpCad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cnpj;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsRota;
        private System.Windows.Forms.DataGridViewTextBoxColumn NmFantasia;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLimite;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefone;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColContato;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVendedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEndereco;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDtCad;
        private System.Windows.Forms.Button BtnUltCompra;
    }
}