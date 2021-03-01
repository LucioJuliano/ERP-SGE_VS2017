namespace ERP_SGE
{
    partial class FrmCadFormaPgto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadFormaPgto));
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.ChkNome = new System.Windows.Forms.RadioButton();
            this.ChkCodigo = new System.Windows.Forms.RadioButton();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.IdUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pag02 = new System.Windows.Forms.TabPage();
            this.Chk_Ativo = new System.Windows.Forms.CheckBox();
            this.TxtIdServidor = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cb_BloqPF = new System.Windows.Forms.CheckBox();
            this.Cb_VerCredito = new System.Windows.Forms.CheckBox();
            this.Cb_VerDebito = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtVlrParc = new System.Windows.Forms.NumericUpDown();
            this.LstTipoDoc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtDesconto = new System.Windows.Forms.NumericUpDown();
            this.Cb_Baixa = new System.Windows.Forms.CheckBox();
            this.Cb_Financeiro = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtIntervalo = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtPrimParcela = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtNumParcelas = new System.Windows.Forms.NumericUpDown();
            this.TxtFormaPgto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPesquisa = new System.Windows.Forms.Button();
            this.Paginas = new System.Windows.Forms.TabControl();
            this.Pag01 = new System.Windows.Forms.TabPage();
            this.BoxPesquisa = new System.Windows.Forms.GroupBox();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Cb_LibClieNovo = new System.Windows.Forms.CheckBox();
            this.BoxItemPesq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.Pag02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtIdServidor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrParc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDesconto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtIntervalo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPrimParcela)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNumParcelas)).BeginInit();
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
            this.BoxItemPesq.Size = new System.Drawing.Size(150, 40);
            this.BoxItemPesq.TabIndex = 6;
            this.BoxItemPesq.TabStop = false;
            // 
            // ChkNome
            // 
            this.ChkNome.AutoSize = true;
            this.ChkNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNome.Location = new System.Drawing.Point(6, 22);
            this.ChkNome.Name = "ChkNome";
            this.ChkNome.Size = new System.Drawing.Size(144, 17);
            this.ChkNome.TabIndex = 8;
            this.ChkNome.TabStop = true;
            this.ChkNome.Text = "Forma de Pagamento";
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
            this.GridDados.Size = new System.Drawing.Size(946, 350);
            this.GridDados.TabIndex = 4;
            this.GridDados.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_DoubleClick);
            // 
            // IdUF
            // 
            this.IdUF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdUF.DataPropertyName = "Id_FormaPgto";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "000";
            this.IdUF.DefaultCellStyle = dataGridViewCellStyle1;
            this.IdUF.HeaderText = "Codigo";
            this.IdUF.Name = "IdUF";
            this.IdUF.ReadOnly = true;
            this.IdUF.Width = 50;
            // 
            // DsRota
            // 
            this.DsRota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DsRota.DataPropertyName = "FormaPgto";
            this.DsRota.HeaderText = "Forma de Pagamento";
            this.DsRota.Name = "DsRota";
            this.DsRota.ReadOnly = true;
            this.DsRota.Width = 300;
            // 
            // Pag02
            // 
            this.Pag02.BackColor = System.Drawing.Color.White;
            this.Pag02.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.Pag02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pag02.Controls.Add(this.Chk_Ativo);
            this.Pag02.Controls.Add(this.TxtIdServidor);
            this.Pag02.Controls.Add(this.label13);
            this.Pag02.Controls.Add(this.groupBox1);
            this.Pag02.Controls.Add(this.TxtFormaPgto);
            this.Pag02.Controls.Add(this.label2);
            this.Pag02.Controls.Add(this.TxtCodigo);
            this.Pag02.Controls.Add(this.label1);
            this.Pag02.Location = new System.Drawing.Point(4, 22);
            this.Pag02.Name = "Pag02";
            this.Pag02.Padding = new System.Windows.Forms.Padding(3);
            this.Pag02.Size = new System.Drawing.Size(952, 409);
            this.Pag02.TabIndex = 1;
            this.Pag02.Text = "Pagina de Dados";
            // 
            // Chk_Ativo
            // 
            this.Chk_Ativo.AutoSize = true;
            this.Chk_Ativo.BackColor = System.Drawing.Color.Transparent;
            this.Chk_Ativo.Location = new System.Drawing.Point(562, 42);
            this.Chk_Ativo.Name = "Chk_Ativo";
            this.Chk_Ativo.Size = new System.Drawing.Size(95, 17);
            this.Chk_Ativo.TabIndex = 58;
            this.Chk_Ativo.Text = "Cadastro Ativo";
            this.Chk_Ativo.UseVisualStyleBackColor = false;
            // 
            // TxtIdServidor
            // 
            this.TxtIdServidor.Location = new System.Drawing.Point(635, 16);
            this.TxtIdServidor.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.TxtIdServidor.Name = "TxtIdServidor";
            this.TxtIdServidor.Size = new System.Drawing.Size(69, 20);
            this.TxtIdServidor.TabIndex = 55;
            this.TxtIdServidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(559, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 13);
            this.label13.TabIndex = 54;
            this.label13.Text = "Cód. Matriz:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Cb_LibClieNovo);
            this.groupBox1.Controls.Add(this.Cb_BloqPF);
            this.groupBox1.Controls.Add(this.Cb_VerCredito);
            this.groupBox1.Controls.Add(this.Cb_VerDebito);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.TxtVlrParc);
            this.groupBox1.Controls.Add(this.LstTipoDoc);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TxtDesconto);
            this.groupBox1.Controls.Add(this.Cb_Baixa);
            this.groupBox1.Controls.Add(this.Cb_Financeiro);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TxtIntervalo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TxtPrimParcela);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtNumParcelas);
            this.groupBox1.Location = new System.Drawing.Point(23, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 267);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalhes:";
            // 
            // Cb_BloqPF
            // 
            this.Cb_BloqPF.AutoSize = true;
            this.Cb_BloqPF.Location = new System.Drawing.Point(180, 112);
            this.Cb_BloqPF.Name = "Cb_BloqPF";
            this.Cb_BloqPF.Size = new System.Drawing.Size(159, 17);
            this.Cb_BloqPF.TabIndex = 54;
            this.Cb_BloqPF.Text = "Bloqueia para Pessoa Fisica";
            this.Cb_BloqPF.UseVisualStyleBackColor = true;
            // 
            // Cb_VerCredito
            // 
            this.Cb_VerCredito.AutoSize = true;
            this.Cb_VerCredito.Location = new System.Drawing.Point(180, 89);
            this.Cb_VerCredito.Name = "Cb_VerCredito";
            this.Cb_VerCredito.Size = new System.Drawing.Size(195, 17);
            this.Cb_VerCredito.TabIndex = 53;
            this.Cb_VerCredito.Text = "Verificar Limite de Credito de Cliente";
            this.Cb_VerCredito.UseVisualStyleBackColor = true;
            // 
            // Cb_VerDebito
            // 
            this.Cb_VerDebito.AutoSize = true;
            this.Cb_VerDebito.Location = new System.Drawing.Point(180, 66);
            this.Cb_VerDebito.Name = "Cb_VerDebito";
            this.Cb_VerDebito.Size = new System.Drawing.Size(148, 17);
            this.Cb_VerDebito.TabIndex = 52;
            this.Cb_VerDebito.Text = "Verificar Debito de Cliente";
            this.Cb_VerDebito.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(6, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(171, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Vlr. Minimo p/ Parcelamento:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtVlrParc
            // 
            this.TxtVlrParc.DecimalPlaces = 2;
            this.TxtVlrParc.Location = new System.Drawing.Point(179, 165);
            this.TxtVlrParc.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.TxtVlrParc.Name = "TxtVlrParc";
            this.TxtVlrParc.Size = new System.Drawing.Size(90, 20);
            this.TxtVlrParc.TabIndex = 50;
            this.TxtVlrParc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LstTipoDoc
            // 
            this.LstTipoDoc.FormattingEnabled = true;
            this.LstTipoDoc.Location = new System.Drawing.Point(9, 213);
            this.LstTipoDoc.Name = "LstTipoDoc";
            this.LstTipoDoc.Size = new System.Drawing.Size(356, 21);
            this.LstTipoDoc.TabIndex = 49;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(6, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Tipo de Documento:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(19, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "% Deconto:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtDesconto
            // 
            this.TxtDesconto.DecimalPlaces = 2;
            this.TxtDesconto.Location = new System.Drawing.Point(90, 98);
            this.TxtDesconto.Name = "TxtDesconto";
            this.TxtDesconto.Size = new System.Drawing.Size(69, 20);
            this.TxtDesconto.TabIndex = 46;
            this.TxtDesconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Cb_Baixa
            // 
            this.Cb_Baixa.AutoSize = true;
            this.Cb_Baixa.Location = new System.Drawing.Point(180, 43);
            this.Cb_Baixa.Name = "Cb_Baixa";
            this.Cb_Baixa.Size = new System.Drawing.Size(107, 17);
            this.Cb_Baixa.TabIndex = 45;
            this.Cb_Baixa.Text = "Baixa automática";
            this.Cb_Baixa.UseVisualStyleBackColor = true;
            // 
            // Cb_Financeiro
            // 
            this.Cb_Financeiro.AutoSize = true;
            this.Cb_Financeiro.Location = new System.Drawing.Point(180, 20);
            this.Cb_Financeiro.Name = "Cb_Financeiro";
            this.Cb_Financeiro.Size = new System.Drawing.Size(108, 17);
            this.Cb_Financeiro.TabIndex = 44;
            this.Cb_Financeiro.Text = "Lança Financeiro";
            this.Cb_Financeiro.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(2, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Dias Intervalo:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtIntervalo
            // 
            this.TxtIntervalo.Location = new System.Drawing.Point(90, 72);
            this.TxtIntervalo.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.TxtIntervalo.Name = "TxtIntervalo";
            this.TxtIntervalo.Size = new System.Drawing.Size(69, 20);
            this.TxtIntervalo.TabIndex = 42;
            this.TxtIntervalo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Dias 1ª Parc.:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtPrimParcela
            // 
            this.TxtPrimParcela.Location = new System.Drawing.Point(90, 46);
            this.TxtPrimParcela.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.TxtPrimParcela.Name = "TxtPrimParcela";
            this.TxtPrimParcela.Size = new System.Drawing.Size(69, 20);
            this.TxtPrimParcela.TabIndex = 40;
            this.TxtPrimParcela.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "No. Parcelas:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtNumParcelas
            // 
            this.TxtNumParcelas.Location = new System.Drawing.Point(90, 20);
            this.TxtNumParcelas.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.TxtNumParcelas.Name = "TxtNumParcelas";
            this.TxtNumParcelas.Size = new System.Drawing.Size(69, 20);
            this.TxtNumParcelas.TabIndex = 38;
            this.TxtNumParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtFormaPgto
            // 
            this.TxtFormaPgto.Location = new System.Drawing.Point(273, 16);
            this.TxtFormaPgto.MaxLength = 60;
            this.TxtFormaPgto.Name = "TxtFormaPgto";
            this.TxtFormaPgto.Size = new System.Drawing.Size(281, 20);
            this.TxtFormaPgto.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(143, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Forma de Pagamento:";
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
            this.BtnPesquisa.Location = new System.Drawing.Point(561, 15);
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
            this.Paginas.Size = new System.Drawing.Size(960, 435);
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
            this.Pag01.Size = new System.Drawing.Size(952, 409);
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
            this.BoxPesquisa.Size = new System.Drawing.Size(946, 53);
            this.BoxPesquisa.TabIndex = 2;
            this.BoxPesquisa.TabStop = false;
            this.BoxPesquisa.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(223, 22);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(331, 20);
            this.TxtPesquisa.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(160, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Conteúdo:";
            // 
            // Cb_LibClieNovo
            // 
            this.Cb_LibClieNovo.AutoSize = true;
            this.Cb_LibClieNovo.Location = new System.Drawing.Point(180, 135);
            this.Cb_LibClieNovo.Name = "Cb_LibClieNovo";
            this.Cb_LibClieNovo.Size = new System.Drawing.Size(230, 17);
            this.Cb_LibClieNovo.TabIndex = 55;
            this.Cb_LibClieNovo.Text = "Não Bloquear primeira compra e reativação";
            this.Cb_LibClieNovo.UseVisualStyleBackColor = true;
            // 
            // FrmCadFormaPgto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 435);
            this.Controls.Add(this.Paginas);
            this.KeyPreview = true;
            this.Name = "FrmCadFormaPgto";
            this.Text = "Cadastro das Formas de Pagamento";
            this.Activated += new System.EventHandler(this.Frm_Activated);
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.Frm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.Pag02.ResumeLayout(false);
            this.Pag02.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtIdServidor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrParc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDesconto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtIntervalo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPrimParcela)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNumParcelas)).EndInit();
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
        private System.Windows.Forms.TabPage Pag02;
        private System.Windows.Forms.TextBox TxtFormaPgto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnPesquisa;
        private System.Windows.Forms.TabControl Paginas;
        private System.Windows.Forms.TabPage Pag01;
        private System.Windows.Forms.GroupBox BoxPesquisa;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsRota;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown TxtIntervalo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown TxtPrimParcela;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown TxtNumParcelas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown TxtDesconto;
        private System.Windows.Forms.CheckBox Cb_Baixa;
        private System.Windows.Forms.CheckBox Cb_Financeiro;
        private System.Windows.Forms.ComboBox LstTipoDoc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown TxtIdServidor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown TxtVlrParc;
        private System.Windows.Forms.CheckBox Cb_VerCredito;
        private System.Windows.Forms.CheckBox Cb_VerDebito;
        private System.Windows.Forms.CheckBox Chk_Ativo;
        private System.Windows.Forms.CheckBox Cb_BloqPF;
        private System.Windows.Forms.CheckBox Cb_LibClieNovo;
    }
}