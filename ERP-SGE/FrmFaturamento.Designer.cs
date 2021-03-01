namespace ERP_SGE
{
    partial class FrmFaturamento
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFaturamento));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.BoxPesquisa = new System.Windows.Forms.GroupBox();
            this.BoxItensPesq = new System.Windows.Forms.GroupBox();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.TxtPesqNumDoc = new System.Windows.Forms.TextBox();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.LstPesqFilial = new System.Windows.Forms.ComboBox();
            this.TxtPesqNumVd = new System.Windows.Forms.MaskedTextBox();
            this.LblVenda = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LstEntrega = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.BtnPesquisa = new System.Windows.Forms.Button();
            this.TxtPesqPessoa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Chk_Periodo = new System.Windows.Forms.CheckBox();
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.Dt2 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.IdUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtEntSai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodPessoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColIdPag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFormaPgto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Vendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrevEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFilial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFilialEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COlDtHrEnvio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.BoxPesquisa.SuspendLayout();
            this.BoxItensPesq.SuspendLayout();
            this.BoxItemPesq.SuspendLayout();
            this.SuspendLayout();
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
            this.IdUF,
            this.DtEntSai,
            this.NumDoc,
            this.CodPessoa,
            this.Column1,
            this.Column2,
            this.ColIdPag,
            this.ColFormaPgto,
            this.Col_Vendedor,
            this.ColPrevEntrega,
            this.ColFilial,
            this.ColUsuario,
            this.ColFilialEntrega,
            this.COlDtHrEnvio});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle6;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 79);
            this.GridDados.Name = "GridDados";
            this.GridDados.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            this.GridDados.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.GridDados.RowTemplate.Height = 18;
            this.GridDados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridDados.Size = new System.Drawing.Size(997, 344);
            this.GridDados.TabIndex = 5;
            // 
            // BoxPesquisa
            // 
            this.BoxPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.BoxPesquisa.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BoxPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BoxPesquisa.Controls.Add(this.BoxItensPesq);
            this.BoxPesquisa.Controls.Add(this.Chk_Periodo);
            this.BoxPesquisa.Controls.Add(this.BoxItemPesq);
            this.BoxPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BoxPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxPesquisa.Location = new System.Drawing.Point(0, 0);
            this.BoxPesquisa.Name = "BoxPesquisa";
            this.BoxPesquisa.Size = new System.Drawing.Size(997, 79);
            this.BoxPesquisa.TabIndex = 3;
            this.BoxPesquisa.TabStop = false;
            // 
            // BoxItensPesq
            // 
            this.BoxItensPesq.Controls.Add(this.BtnImprimir);
            this.BoxItensPesq.Controls.Add(this.TxtPesqNumDoc);
            this.BoxItensPesq.Controls.Add(this.BtnConfirmar);
            this.BoxItensPesq.Controls.Add(this.label8);
            this.BoxItensPesq.Controls.Add(this.LstPesqFilial);
            this.BoxItensPesq.Controls.Add(this.TxtPesqNumVd);
            this.BoxItensPesq.Controls.Add(this.LblVenda);
            this.BoxItensPesq.Controls.Add(this.label5);
            this.BoxItensPesq.Controls.Add(this.LstEntrega);
            this.BoxItensPesq.Controls.Add(this.label13);
            this.BoxItensPesq.Controls.Add(this.BtnPesquisa);
            this.BoxItensPesq.Controls.Add(this.TxtPesqPessoa);
            this.BoxItensPesq.Controls.Add(this.label3);
            this.BoxItensPesq.Location = new System.Drawing.Point(142, 10);
            this.BoxItensPesq.Name = "BoxItensPesq";
            this.BoxItensPesq.Size = new System.Drawing.Size(843, 64);
            this.BoxItensPesq.TabIndex = 9;
            this.BoxItensPesq.TabStop = false;
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("BtnImprimir.Image")));
            this.BtnImprimir.Location = new System.Drawing.Point(733, 30);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(98, 30);
            this.BtnImprimir.TabIndex = 67;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
            // 
            // TxtPesqNumDoc
            // 
            this.TxtPesqNumDoc.Location = new System.Drawing.Point(107, 13);
            this.TxtPesqNumDoc.Name = "TxtPesqNumDoc";
            this.TxtPesqNumDoc.Size = new System.Drawing.Size(69, 20);
            this.TxtPesqNumDoc.TabIndex = 66;
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(629, 30);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(98, 30);
            this.BtnConfirmar.TabIndex = 65;
            this.BtnConfirmar.Text = "Faturamento";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(325, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Filial NF:";
            // 
            // LstPesqFilial
            // 
            this.LstPesqFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstPesqFilial.FormattingEnabled = true;
            this.LstPesqFilial.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia"});
            this.LstPesqFilial.Location = new System.Drawing.Point(383, 39);
            this.LstPesqFilial.Name = "LstPesqFilial";
            this.LstPesqFilial.Size = new System.Drawing.Size(199, 21);
            this.LstPesqFilial.TabIndex = 27;
            this.LstPesqFilial.SelectedIndexChanged += new System.EventHandler(this.LstPesqFilial_SelectedIndexChanged);
            // 
            // TxtPesqNumVd
            // 
            this.TxtPesqNumVd.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.TxtPesqNumVd.Location = new System.Drawing.Point(248, 13);
            this.TxtPesqNumVd.Mask = "0000000";
            this.TxtPesqNumVd.Name = "TxtPesqNumVd";
            this.TxtPesqNumVd.PromptChar = ' ';
            this.TxtPesqNumVd.Size = new System.Drawing.Size(65, 20);
            this.TxtPesqNumVd.TabIndex = 26;
            this.TxtPesqNumVd.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // LblVenda
            // 
            this.LblVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVenda.Location = new System.Drawing.Point(176, 16);
            this.LblVenda.Name = "LblVenda";
            this.LblVenda.Size = new System.Drawing.Size(71, 13);
            this.LblVenda.TabIndex = 24;
            this.LblVenda.Text = "No. Venda:";
            this.LblVenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(312, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "F. Entrega:";
            // 
            // LstEntrega
            // 
            this.LstEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstEntrega.FormattingEnabled = true;
            this.LstEntrega.Location = new System.Drawing.Point(383, 13);
            this.LstEntrega.Name = "LstEntrega";
            this.LstEntrega.Size = new System.Drawing.Size(199, 21);
            this.LstEntrega.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(11, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "No. Documento:";
            // 
            // BtnPesquisa
            // 
            this.BtnPesquisa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPesquisa.BackgroundImage")));
            this.BtnPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnPesquisa.Location = new System.Drawing.Point(588, 30);
            this.BtnPesquisa.Name = "BtnPesquisa";
            this.BtnPesquisa.Size = new System.Drawing.Size(35, 30);
            this.BtnPesquisa.TabIndex = 21;
            this.BtnPesquisa.UseVisualStyleBackColor = true;
            this.BtnPesquisa.Click += new System.EventHandler(this.BtnPesquisa_Click);
            // 
            // TxtPesqPessoa
            // 
            this.TxtPesqPessoa.Location = new System.Drawing.Point(61, 39);
            this.TxtPesqPessoa.Name = "TxtPesqPessoa";
            this.TxtPesqPessoa.Size = new System.Drawing.Size(252, 20);
            this.TxtPesqPessoa.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Pessoa:";
            // 
            // Chk_Periodo
            // 
            this.Chk_Periodo.AutoSize = true;
            this.Chk_Periodo.Checked = true;
            this.Chk_Periodo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_Periodo.Location = new System.Drawing.Point(15, 9);
            this.Chk_Periodo.Name = "Chk_Periodo";
            this.Chk_Periodo.Size = new System.Drawing.Size(110, 17);
            this.Chk_Periodo.TabIndex = 8;
            this.Chk_Periodo.Text = "Incluir Período";
            this.Chk_Periodo.UseVisualStyleBackColor = true;
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.Controls.Add(this.label16);
            this.BoxItemPesq.Controls.Add(this.Dt2);
            this.BoxItemPesq.Controls.Add(this.label6);
            this.BoxItemPesq.Controls.Add(this.Dt1);
            this.BoxItemPesq.Location = new System.Drawing.Point(6, 10);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(136, 64);
            this.BoxItemPesq.TabIndex = 1;
            this.BoxItemPesq.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "a";
            // 
            // Dt2
            // 
            this.Dt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt2.Location = new System.Drawing.Point(30, 39);
            this.Dt2.Name = "Dt2";
            this.Dt2.Size = new System.Drawing.Size(100, 20);
            this.Dt2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "de:";
            // 
            // Dt1
            // 
            this.Dt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt1.Location = new System.Drawing.Point(30, 15);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(100, 20);
            this.Dt1.TabIndex = 0;
            // 
            // IdUF
            // 
            this.IdUF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdUF.DataPropertyName = "Id_Venda";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "000";
            this.IdUF.DefaultCellStyle = dataGridViewCellStyle2;
            this.IdUF.HeaderText = "Nº Venda";
            this.IdUF.Name = "IdUF";
            this.IdUF.ReadOnly = true;
            this.IdUF.Width = 80;
            // 
            // DtEntSai
            // 
            this.DtEntSai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DtEntSai.DataPropertyName = "Data";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.DtEntSai.DefaultCellStyle = dataGridViewCellStyle3;
            this.DtEntSai.HeaderText = "Data";
            this.DtEntSai.Name = "DtEntSai";
            this.DtEntSai.ReadOnly = true;
            this.DtEntSai.Width = 90;
            // 
            // NumDoc
            // 
            this.NumDoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NumDoc.DataPropertyName = "NumDocumento";
            this.NumDoc.HeaderText = "No.Documento";
            this.NumDoc.Name = "NumDoc";
            this.NumDoc.ReadOnly = true;
            this.NumDoc.Width = 90;
            // 
            // CodPessoa
            // 
            this.CodPessoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CodPessoa.DataPropertyName = "Id_Pessoa";
            this.CodPessoa.HeaderText = "Cód.";
            this.CodPessoa.Name = "CodPessoa";
            this.CodPessoa.ReadOnly = true;
            this.CodPessoa.Width = 50;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "Cliente";
            this.Column1.HeaderText = "Cliente";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 250;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.DataPropertyName = "VlrTotal";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0,00";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "Vlr. Total R$";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // ColIdPag
            // 
            this.ColIdPag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColIdPag.DataPropertyName = "Id_FormaPgto";
            this.ColIdPag.HeaderText = "Cód.";
            this.ColIdPag.Name = "ColIdPag";
            this.ColIdPag.ReadOnly = true;
            this.ColIdPag.Width = 50;
            // 
            // ColFormaPgto
            // 
            this.ColFormaPgto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColFormaPgto.DataPropertyName = "FormaPgto";
            this.ColFormaPgto.HeaderText = "Forma de Pagamento";
            this.ColFormaPgto.Name = "ColFormaPgto";
            this.ColFormaPgto.ReadOnly = true;
            this.ColFormaPgto.Width = 150;
            // 
            // Col_Vendedor
            // 
            this.Col_Vendedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Col_Vendedor.DataPropertyName = "Vendedor";
            this.Col_Vendedor.HeaderText = "Vendedor";
            this.Col_Vendedor.Name = "Col_Vendedor";
            this.Col_Vendedor.ReadOnly = true;
            this.Col_Vendedor.Width = 130;
            // 
            // ColPrevEntrega
            // 
            this.ColPrevEntrega.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColPrevEntrega.DataPropertyName = "PrevEntrega";
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.ColPrevEntrega.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColPrevEntrega.HeaderText = "Prev.Entrega";
            this.ColPrevEntrega.Name = "ColPrevEntrega";
            this.ColPrevEntrega.ReadOnly = true;
            this.ColPrevEntrega.Width = 90;
            // 
            // ColFilial
            // 
            this.ColFilial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColFilial.DataPropertyName = "Filial";
            this.ColFilial.HeaderText = "Filial";
            this.ColFilial.Name = "ColFilial";
            this.ColFilial.ReadOnly = true;
            this.ColFilial.Width = 250;
            // 
            // ColUsuario
            // 
            this.ColUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColUsuario.DataPropertyName = "Usuario";
            this.ColUsuario.HeaderText = "Usuário";
            this.ColUsuario.Name = "ColUsuario";
            this.ColUsuario.ReadOnly = true;
            this.ColUsuario.Width = 130;
            // 
            // ColFilialEntrega
            // 
            this.ColFilialEntrega.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColFilialEntrega.DataPropertyName = "FilialEntrega";
            this.ColFilialEntrega.HeaderText = "Filial de Entrega";
            this.ColFilialEntrega.Name = "ColFilialEntrega";
            this.ColFilialEntrega.ReadOnly = true;
            this.ColFilialEntrega.Width = 150;
            // 
            // COlDtHrEnvio
            // 
            this.COlDtHrEnvio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.COlDtHrEnvio.DataPropertyName = "DtEnvioRec";
            this.COlDtHrEnvio.HeaderText = "DtHr Envio";
            this.COlDtHrEnvio.Name = "COlDtHrEnvio";
            this.COlDtHrEnvio.ReadOnly = true;
            this.COlDtHrEnvio.Width = 120;
            // 
            // FrmFaturamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 423);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.BoxPesquisa);
            this.KeyPreview = true;
            this.Name = "FrmFaturamento";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Faturamento";
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.Frm_Load);
            this.Activated += new System.EventHandler(this.Frm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.BoxPesquisa.ResumeLayout(false);
            this.BoxPesquisa.PerformLayout();
            this.BoxItensPesq.ResumeLayout(false);
            this.BoxItensPesq.PerformLayout();
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BoxPesquisa;
        public System.Windows.Forms.GroupBox BoxItensPesq;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox LstPesqFilial;
        private System.Windows.Forms.MaskedTextBox TxtPesqNumVd;
        public System.Windows.Forms.Label LblVenda;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox LstEntrega;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button BtnPesquisa;
        private System.Windows.Forms.TextBox TxtPesqPessoa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Chk_Periodo;
        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker Dt2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker Dt1;
        private System.Windows.Forms.DataGridView GridDados;
        public System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.TextBox TxtPesqNumDoc;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtEntSai;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodPessoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColIdPag;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFormaPgto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Vendedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrevEntrega;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFilial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFilialEntrega;
        private System.Windows.Forms.DataGridViewTextBoxColumn COlDtHrEnvio;
    }
}