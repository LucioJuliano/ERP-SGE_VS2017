namespace ERP_SGE
{
    partial class FrmConsMovPrd
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
            System.Windows.Forms.Button BtnBuscaPrd;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsMovPrd));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblSaldo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnPesquisa = new System.Windows.Forms.Button();
            this.PnlPeriodo = new System.Windows.Forms.Panel();
            this.Dt2 = new System.Windows.Forms.DateTimePicker();
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.LstTpMv = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCodPrd = new System.Windows.Forms.TextBox();
            this.TxtDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.ColLanc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescMov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estoque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VlrUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pessoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Filial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            BtnBuscaPrd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.PnlPeriodo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnBuscaPrd
            // 
            BtnBuscaPrd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBuscaPrd.BackgroundImage")));
            BtnBuscaPrd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            BtnBuscaPrd.Location = new System.Drawing.Point(50, 12);
            BtnBuscaPrd.Name = "BtnBuscaPrd";
            BtnBuscaPrd.Size = new System.Drawing.Size(20, 20);
            BtnBuscaPrd.TabIndex = 140;
            BtnBuscaPrd.UseVisualStyleBackColor = true;
            BtnBuscaPrd.Click += new System.EventHandler(this.BtnBuscaPrd_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.LblSaldo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BtnPesquisa);
            this.panel1.Controls.Add(this.PnlPeriodo);
            this.panel1.Controls.Add(this.LstTpMv);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtCodPrd);
            this.panel1.Controls.Add(this.TxtDescricao);
            this.panel1.Controls.Add(BtnBuscaPrd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 100);
            this.panel1.TabIndex = 0;
            // 
            // LblSaldo
            // 
            this.LblSaldo.BackColor = System.Drawing.Color.Transparent;
            this.LblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.LblSaldo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LblSaldo.Location = new System.Drawing.Point(532, 13);
            this.LblSaldo.Name = "LblSaldo";
            this.LblSaldo.Size = new System.Drawing.Size(94, 19);
            this.LblSaldo.TabIndex = 149;
            this.LblSaldo.Text = "0,000";
            this.LblSaldo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(415, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 148;
            this.label3.Text = "Saldo do Estoque:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnPesquisa
            // 
            this.BtnPesquisa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPesquisa.BackgroundImage")));
            this.BtnPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnPesquisa.Location = new System.Drawing.Point(326, 63);
            this.BtnPesquisa.Name = "BtnPesquisa";
            this.BtnPesquisa.Size = new System.Drawing.Size(35, 30);
            this.BtnPesquisa.TabIndex = 147;
            this.BtnPesquisa.UseVisualStyleBackColor = true;
            this.BtnPesquisa.Click += new System.EventHandler(this.BtnPesquisa_Click);
            // 
            // PnlPeriodo
            // 
            this.PnlPeriodo.Controls.Add(this.Dt2);
            this.PnlPeriodo.Controls.Add(this.Dt1);
            this.PnlPeriodo.Controls.Add(this.label6);
            this.PnlPeriodo.Controls.Add(this.label16);
            this.PnlPeriodo.Location = new System.Drawing.Point(5, 65);
            this.PnlPeriodo.Name = "PnlPeriodo";
            this.PnlPeriodo.Size = new System.Drawing.Size(305, 26);
            this.PnlPeriodo.TabIndex = 146;
            // 
            // Dt2
            // 
            this.Dt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt2.Location = new System.Drawing.Point(198, 1);
            this.Dt2.Name = "Dt2";
            this.Dt2.Size = new System.Drawing.Size(100, 20);
            this.Dt2.TabIndex = 1;
            // 
            // Dt1
            // 
            this.Dt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt1.Location = new System.Drawing.Point(81, 1);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(100, 20);
            this.Dt1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Período de:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(183, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "a";
            // 
            // LstTpMv
            // 
            this.LstTpMv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstTpMv.FormattingEnabled = true;
            this.LstTpMv.Location = new System.Drawing.Point(70, 38);
            this.LstTpMv.Name = "LstTpMv";
            this.LstTpMv.Size = new System.Drawing.Size(339, 21);
            this.LstTpMv.TabIndex = 144;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(2, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 145;
            this.label1.Text = "Movimento:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtCodPrd
            // 
            this.TxtCodPrd.Enabled = false;
            this.TxtCodPrd.Location = new System.Drawing.Point(70, 12);
            this.TxtCodPrd.MaxLength = 40;
            this.TxtCodPrd.Name = "TxtCodPrd";
            this.TxtCodPrd.Size = new System.Drawing.Size(48, 20);
            this.TxtCodPrd.TabIndex = 143;
            this.TxtCodPrd.Text = "0";
            // 
            // TxtDescricao
            // 
            this.TxtDescricao.Enabled = false;
            this.TxtDescricao.Location = new System.Drawing.Point(120, 12);
            this.TxtDescricao.MaxLength = 40;
            this.TxtDescricao.Name = "TxtDescricao";
            this.TxtDescricao.Size = new System.Drawing.Size(289, 20);
            this.TxtDescricao.TabIndex = 141;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(2, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 142;
            this.label2.Text = "Produto";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GridDados
            // 
            this.GridDados.AllowUserToAddRows = false;
            this.GridDados.AllowUserToDeleteRows = false;
            this.GridDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLanc,
            this.Data,
            this.DescMov,
            this.Estoque,
            this.Documento,
            this.NumDoc,
            this.Entrada,
            this.Saida,
            this.VlrUnitario,
            this.ColPI,
            this.Pessoa,
            this.Filial});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle7;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 100);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            this.GridDados.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.GridDados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridDados.Size = new System.Drawing.Size(983, 361);
            this.GridDados.TabIndex = 5;
            this.GridDados.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridDados_CellFormatting);
            // 
            // ColLanc
            // 
            this.ColLanc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColLanc.DataPropertyName = "Lanc";
            this.ColLanc.HeaderText = "No.Lanç";
            this.ColLanc.Name = "ColLanc";
            this.ColLanc.ReadOnly = true;
            this.ColLanc.Width = 50;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Data.DataPropertyName = "Data";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.Data.DefaultCellStyle = dataGridViewCellStyle2;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Width = 80;
            // 
            // DescMov
            // 
            this.DescMov.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DescMov.DataPropertyName = "DescMov";
            this.DescMov.HeaderText = "Movimento";
            this.DescMov.Name = "DescMov";
            this.DescMov.ReadOnly = true;
            this.DescMov.Width = 150;
            // 
            // Estoque
            // 
            this.Estoque.DataPropertyName = "Estoque";
            this.Estoque.HeaderText = "TpMov";
            this.Estoque.Name = "Estoque";
            this.Estoque.ReadOnly = true;
            this.Estoque.Visible = false;
            this.Estoque.Width = 66;
            // 
            // Documento
            // 
            this.Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Documento.DataPropertyName = "Documento";
            this.Documento.HeaderText = "Tipo Doc.";
            this.Documento.Name = "Documento";
            this.Documento.ReadOnly = true;
            this.Documento.Width = 80;
            // 
            // NumDoc
            // 
            this.NumDoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NumDoc.DataPropertyName = "NumDoc";
            this.NumDoc.HeaderText = "No Docum.";
            this.NumDoc.Name = "NumDoc";
            this.NumDoc.ReadOnly = true;
            this.NumDoc.Width = 90;
            // 
            // Entrada
            // 
            this.Entrada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Entrada.DataPropertyName = "Entrada";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this.Entrada.DefaultCellStyle = dataGridViewCellStyle3;
            this.Entrada.HeaderText = "Entrada";
            this.Entrada.Name = "Entrada";
            this.Entrada.ReadOnly = true;
            this.Entrada.Width = 60;
            // 
            // Saida
            // 
            this.Saida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Saida.DataPropertyName = "Saida";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N3";
            dataGridViewCellStyle4.NullValue = null;
            this.Saida.DefaultCellStyle = dataGridViewCellStyle4;
            this.Saida.HeaderText = "Saida";
            this.Saida.Name = "Saida";
            this.Saida.ReadOnly = true;
            this.Saida.Width = 60;
            // 
            // VlrUnitario
            // 
            this.VlrUnitario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VlrUnitario.DataPropertyName = "VlrUnitario";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.VlrUnitario.DefaultCellStyle = dataGridViewCellStyle5;
            this.VlrUnitario.HeaderText = "Vlr. Unit. R$";
            this.VlrUnitario.Name = "VlrUnitario";
            this.VlrUnitario.ReadOnly = true;
            this.VlrUnitario.Width = 90;
            // 
            // ColPI
            // 
            this.ColPI.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColPI.DataPropertyName = "P_IPI";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0,00";
            this.ColPI.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColPI.HeaderText = "% IPI";
            this.ColPI.Name = "ColPI";
            this.ColPI.Width = 60;
            // 
            // Pessoa
            // 
            this.Pessoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Pessoa.DataPropertyName = "RazaoSocial";
            this.Pessoa.HeaderText = "Cliente/Fornecedor";
            this.Pessoa.Name = "Pessoa";
            this.Pessoa.ReadOnly = true;
            this.Pessoa.Width = 250;
            // 
            // Filial
            // 
            this.Filial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Filial.DataPropertyName = "Filial";
            this.Filial.HeaderText = "Filial";
            this.Filial.Name = "Filial";
            this.Filial.ReadOnly = true;
            this.Filial.Width = 200;
            // 
            // FrmConsMovPrd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(983, 461);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConsMovPrd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Movimentação do Estoque do Produto";
            this.Load += new System.EventHandler(this.FrmConsMovPrd_Load);
            this.Shown += new System.EventHandler(this.FrmConsMovPrd_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PnlPeriodo.ResumeLayout(false);
            this.PnlPeriodo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtCodPrd;
        private System.Windows.Forms.TextBox TxtDescricao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox LstTpMv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PnlPeriodo;
        private System.Windows.Forms.DateTimePicker Dt2;
        private System.Windows.Forms.DateTimePicker Dt1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BtnPesquisa;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.Label LblSaldo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLanc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescMov;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estoque;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saida;
        private System.Windows.Forms.DataGridViewTextBoxColumn VlrUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pessoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Filial;
    }
}