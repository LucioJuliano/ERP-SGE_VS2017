namespace ERP_SGE
{
    partial class FrmRelRH
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
            System.Windows.Forms.Button BtnBuscaFunc;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelRH));
            this.label29 = new System.Windows.Forms.Label();
            this.TxtAnoEventos = new System.Windows.Forms.NumericUpDown();
            this.LstMesEventos = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.LstDepartamento = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Op11 = new System.Windows.Forms.RadioButton();
            this.Op10 = new System.Windows.Forms.RadioButton();
            this.Op08 = new System.Windows.Forms.RadioButton();
            this.Op07 = new System.Windows.Forms.RadioButton();
            this.Op06 = new System.Windows.Forms.RadioButton();
            this.Op09 = new System.Windows.Forms.RadioButton();
            this.Op05 = new System.Windows.Forms.RadioButton();
            this.Op04 = new System.Windows.Forms.RadioButton();
            this.Op03 = new System.Windows.Forms.RadioButton();
            this.Op02 = new System.Windows.Forms.RadioButton();
            this.Op01 = new System.Windows.Forms.RadioButton();
            this.BoxMesAno = new System.Windows.Forms.GroupBox();
            this.Cb_Quizena = new System.Windows.Forms.CheckBox();
            this.BoxFilial = new System.Windows.Forms.GroupBox();
            this.BoxDepart = new System.Windows.Forms.GroupBox();
            this.BoxEventos = new System.Windows.Forms.GroupBox();
            this.LstEventos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BoxFiliaCtps = new System.Windows.Forms.GroupBox();
            this.LstFilialCtps = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BoxFunc = new System.Windows.Forms.GroupBox();
            this.TxtCodFunc = new System.Windows.Forms.TextBox();
            this.TxtFuncionario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Op12 = new System.Windows.Forms.RadioButton();
            BtnBuscaFunc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAnoEventos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.BoxMesAno.SuspendLayout();
            this.BoxFilial.SuspendLayout();
            this.BoxDepart.SuspendLayout();
            this.BoxEventos.SuspendLayout();
            this.BoxFiliaCtps.SuspendLayout();
            this.BoxFunc.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnBuscaFunc
            // 
            BtnBuscaFunc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBuscaFunc.BackgroundImage")));
            BtnBuscaFunc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            BtnBuscaFunc.Location = new System.Drawing.Point(6, 24);
            BtnBuscaFunc.Name = "BtnBuscaFunc";
            BtnBuscaFunc.Size = new System.Drawing.Size(20, 20);
            BtnBuscaFunc.TabIndex = 155;
            BtnBuscaFunc.UseVisualStyleBackColor = true;
            BtnBuscaFunc.Click += new System.EventHandler(this.BtnBuscaFunc_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(146, 21);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(33, 13);
            this.label29.TabIndex = 68;
            this.label29.Text = "Ano:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtAnoEventos
            // 
            this.TxtAnoEventos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAnoEventos.Location = new System.Drawing.Point(179, 16);
            this.TxtAnoEventos.Maximum = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.TxtAnoEventos.Minimum = new decimal(new int[] {
            2012,
            0,
            0,
            0});
            this.TxtAnoEventos.Name = "TxtAnoEventos";
            this.TxtAnoEventos.Size = new System.Drawing.Size(61, 20);
            this.TxtAnoEventos.TabIndex = 67;
            this.TxtAnoEventos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAnoEventos.Value = new decimal(new int[] {
            2012,
            0,
            0,
            0});
            // 
            // LstMesEventos
            // 
            this.LstMesEventos.FormattingEnabled = true;
            this.LstMesEventos.Items.AddRange(new object[] {
            "Todos",
            "Janeiro",
            "Fevereiro",
            "Março",
            "Abril",
            "Maio",
            "Junho",
            "Julho",
            "Agosto",
            "Setembro",
            "Outubro",
            "Novembro",
            "Dezembro",
            "13º Salario"});
            this.LstMesEventos.Location = new System.Drawing.Point(39, 16);
            this.LstMesEventos.Name = "LstMesEventos";
            this.LstMesEventos.Size = new System.Drawing.Size(101, 21);
            this.LstMesEventos.TabIndex = 66;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(4, 21);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(34, 13);
            this.label28.TabIndex = 65;
            this.label28.Text = "Mês:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label43.Location = new System.Drawing.Point(11, 16);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(90, 13);
            this.label43.TabIndex = 191;
            this.label43.Text = "Departamento:";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label43.UseMnemonic = false;
            // 
            // LstDepartamento
            // 
            this.LstDepartamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstDepartamento.FormattingEnabled = true;
            this.LstDepartamento.Items.AddRange(new object[] {
            "Analfaberto",
            "Fundamental",
            "Médio",
            "Superior Incompleto",
            "Superior Completo",
            "Curso Técnico"});
            this.LstDepartamento.Location = new System.Drawing.Point(100, 11);
            this.LstDepartamento.Name = "LstDepartamento";
            this.LstDepartamento.Size = new System.Drawing.Size(230, 21);
            this.LstDepartamento.TabIndex = 189;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label34.Location = new System.Drawing.Point(7, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(37, 13);
            this.label34.TabIndex = 190;
            this.label34.Text = "Filial:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label34.UseMnemonic = false;
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Items.AddRange(new object[] {
            "Analfaberto",
            "Fundamental",
            "Médio",
            "Superior Incompleto",
            "Superior Completo",
            "Curso Técnico"});
            this.LstFilial.Location = new System.Drawing.Point(39, 11);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(291, 21);
            this.LstFilial.TabIndex = 188;
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("BtnImprimir.Image")));
            this.BtnImprimir.Location = new System.Drawing.Point(340, 235);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(128, 30);
            this.BtnImprimir.TabIndex = 192;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Op12);
            this.groupBox1.Controls.Add(this.Op11);
            this.groupBox1.Controls.Add(this.Op10);
            this.groupBox1.Controls.Add(this.Op08);
            this.groupBox1.Controls.Add(this.Op07);
            this.groupBox1.Controls.Add(this.Op06);
            this.groupBox1.Controls.Add(this.Op09);
            this.groupBox1.Controls.Add(this.Op05);
            this.groupBox1.Controls.Add(this.Op04);
            this.groupBox1.Controls.Add(this.Op03);
            this.groupBox1.Controls.Add(this.Op02);
            this.groupBox1.Controls.Add(this.Op01);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 279);
            this.groupBox1.TabIndex = 193;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selecione um Relatório";
            // 
            // Op11
            // 
            this.Op11.AutoSize = true;
            this.Op11.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op11.Location = new System.Drawing.Point(3, 186);
            this.Op11.Name = "Op11";
            this.Op11.Size = new System.Drawing.Size(234, 17);
            this.Op11.TabIndex = 109;
            this.Op11.TabStop = true;
            this.Op11.Text = "Ficha do Funcionário";
            this.Op11.UseVisualStyleBackColor = true;
            this.Op11.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op10
            // 
            this.Op10.AutoSize = true;
            this.Op10.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op10.Location = new System.Drawing.Point(3, 169);
            this.Op10.Name = "Op10";
            this.Op10.Size = new System.Drawing.Size(234, 17);
            this.Op10.TabIndex = 108;
            this.Op10.TabStop = true;
            this.Op10.Text = "Folha Bruta Mensal";
            this.Op10.UseVisualStyleBackColor = true;
            this.Op10.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op08
            // 
            this.Op08.AutoSize = true;
            this.Op08.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op08.Location = new System.Drawing.Point(3, 152);
            this.Op08.Name = "Op08";
            this.Op08.Size = new System.Drawing.Size(234, 17);
            this.Op08.TabIndex = 106;
            this.Op08.TabStop = true;
            this.Op08.Text = "Ficha de Lanç. Fixos e Mensais";
            this.Op08.UseVisualStyleBackColor = true;
            this.Op08.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op07
            // 
            this.Op07.AutoSize = true;
            this.Op07.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op07.Location = new System.Drawing.Point(3, 135);
            this.Op07.Name = "Op07";
            this.Op07.Size = new System.Drawing.Size(234, 17);
            this.Op07.TabIndex = 105;
            this.Op07.TabStop = true;
            this.Op07.Text = "Resumo dos Eventos da Folha";
            this.Op07.UseVisualStyleBackColor = true;
            this.Op07.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op06
            // 
            this.Op06.AutoSize = true;
            this.Op06.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op06.Location = new System.Drawing.Point(3, 118);
            this.Op06.Name = "Op06";
            this.Op06.Size = new System.Drawing.Size(234, 17);
            this.Op06.TabIndex = 104;
            this.Op06.TabStop = true;
            this.Op06.Text = "Resumo da Folha (Banco)";
            this.Op06.UseVisualStyleBackColor = true;
            this.Op06.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op09
            // 
            this.Op09.AutoSize = true;
            this.Op09.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op09.Location = new System.Drawing.Point(3, 101);
            this.Op09.Name = "Op09";
            this.Op09.Size = new System.Drawing.Size(234, 17);
            this.Op09.TabIndex = 107;
            this.Op09.TabStop = true;
            this.Op09.Text = "Resumo da Folha (Dinheiro)";
            this.Op09.UseVisualStyleBackColor = true;
            this.Op09.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op05
            // 
            this.Op05.AutoSize = true;
            this.Op05.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op05.Location = new System.Drawing.Point(3, 84);
            this.Op05.Name = "Op05";
            this.Op05.Size = new System.Drawing.Size(234, 17);
            this.Op05.TabIndex = 103;
            this.Op05.TabStop = true;
            this.Op05.Text = "Lista de Salarios";
            this.Op05.UseVisualStyleBackColor = true;
            this.Op05.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op04
            // 
            this.Op04.AutoSize = true;
            this.Op04.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op04.Location = new System.Drawing.Point(3, 67);
            this.Op04.Name = "Op04";
            this.Op04.Size = new System.Drawing.Size(234, 17);
            this.Op04.TabIndex = 102;
            this.Op04.TabStop = true;
            this.Op04.Text = "Lanç. dos Eventos ";
            this.Op04.UseVisualStyleBackColor = true;
            this.Op04.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op03
            // 
            this.Op03.AutoSize = true;
            this.Op03.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op03.Location = new System.Drawing.Point(3, 50);
            this.Op03.Name = "Op03";
            this.Op03.Size = new System.Drawing.Size(234, 17);
            this.Op03.TabIndex = 99;
            this.Op03.TabStop = true;
            this.Op03.Text = "Resumo da Folha Mensal";
            this.Op03.UseVisualStyleBackColor = true;
            this.Op03.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op02
            // 
            this.Op02.AutoSize = true;
            this.Op02.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op02.Location = new System.Drawing.Point(3, 33);
            this.Op02.Name = "Op02";
            this.Op02.Size = new System.Drawing.Size(234, 17);
            this.Op02.TabIndex = 98;
            this.Op02.TabStop = true;
            this.Op02.Text = "Demonstrativo Mensal";
            this.Op02.UseVisualStyleBackColor = true;
            this.Op02.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op01
            // 
            this.Op01.AutoSize = true;
            this.Op01.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op01.Location = new System.Drawing.Point(3, 16);
            this.Op01.Name = "Op01";
            this.Op01.Size = new System.Drawing.Size(234, 17);
            this.Op01.TabIndex = 96;
            this.Op01.TabStop = true;
            this.Op01.Text = "Folha Quizenal";
            this.Op01.UseVisualStyleBackColor = true;
            this.Op01.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // BoxMesAno
            // 
            this.BoxMesAno.BackColor = System.Drawing.Color.Transparent;
            this.BoxMesAno.Controls.Add(this.Cb_Quizena);
            this.BoxMesAno.Controls.Add(this.TxtAnoEventos);
            this.BoxMesAno.Controls.Add(this.label28);
            this.BoxMesAno.Controls.Add(this.LstMesEventos);
            this.BoxMesAno.Controls.Add(this.label29);
            this.BoxMesAno.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxMesAno.Location = new System.Drawing.Point(240, 0);
            this.BoxMesAno.Name = "BoxMesAno";
            this.BoxMesAno.Size = new System.Drawing.Size(346, 44);
            this.BoxMesAno.TabIndex = 194;
            this.BoxMesAno.TabStop = false;
            // 
            // Cb_Quizena
            // 
            this.Cb_Quizena.AutoSize = true;
            this.Cb_Quizena.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cb_Quizena.Location = new System.Drawing.Point(247, 18);
            this.Cb_Quizena.Name = "Cb_Quizena";
            this.Cb_Quizena.Size = new System.Drawing.Size(72, 17);
            this.Cb_Quizena.TabIndex = 69;
            this.Cb_Quizena.Text = "Quizena";
            this.Cb_Quizena.UseVisualStyleBackColor = true;
            this.Cb_Quizena.Visible = false;
            // 
            // BoxFilial
            // 
            this.BoxFilial.BackColor = System.Drawing.Color.Transparent;
            this.BoxFilial.Controls.Add(this.LstFilial);
            this.BoxFilial.Controls.Add(this.label34);
            this.BoxFilial.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxFilial.Location = new System.Drawing.Point(240, 44);
            this.BoxFilial.Name = "BoxFilial";
            this.BoxFilial.Size = new System.Drawing.Size(346, 34);
            this.BoxFilial.TabIndex = 196;
            this.BoxFilial.TabStop = false;
            // 
            // BoxDepart
            // 
            this.BoxDepart.BackColor = System.Drawing.Color.Transparent;
            this.BoxDepart.Controls.Add(this.LstDepartamento);
            this.BoxDepart.Controls.Add(this.label43);
            this.BoxDepart.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxDepart.Location = new System.Drawing.Point(240, 78);
            this.BoxDepart.Name = "BoxDepart";
            this.BoxDepart.Size = new System.Drawing.Size(346, 33);
            this.BoxDepart.TabIndex = 197;
            this.BoxDepart.TabStop = false;
            // 
            // BoxEventos
            // 
            this.BoxEventos.BackColor = System.Drawing.Color.Transparent;
            this.BoxEventos.Controls.Add(this.LstEventos);
            this.BoxEventos.Controls.Add(this.label1);
            this.BoxEventos.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxEventos.Location = new System.Drawing.Point(240, 111);
            this.BoxEventos.Name = "BoxEventos";
            this.BoxEventos.Size = new System.Drawing.Size(346, 34);
            this.BoxEventos.TabIndex = 198;
            this.BoxEventos.TabStop = false;
            this.BoxEventos.Visible = false;
            // 
            // LstEventos
            // 
            this.LstEventos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstEventos.FormattingEnabled = true;
            this.LstEventos.Items.AddRange(new object[] {
            "Analfaberto",
            "Fundamental",
            "Médio",
            "Superior Incompleto",
            "Superior Completo",
            "Curso Técnico"});
            this.LstEventos.Location = new System.Drawing.Point(67, 11);
            this.LstEventos.Name = "LstEventos";
            this.LstEventos.Size = new System.Drawing.Size(263, 21);
            this.LstEventos.TabIndex = 189;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 191;
            this.label1.Text = "Eventos:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.UseMnemonic = false;
            // 
            // BoxFiliaCtps
            // 
            this.BoxFiliaCtps.BackColor = System.Drawing.Color.Transparent;
            this.BoxFiliaCtps.Controls.Add(this.LstFilialCtps);
            this.BoxFiliaCtps.Controls.Add(this.label2);
            this.BoxFiliaCtps.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxFiliaCtps.Location = new System.Drawing.Point(240, 145);
            this.BoxFiliaCtps.Name = "BoxFiliaCtps";
            this.BoxFiliaCtps.Size = new System.Drawing.Size(346, 34);
            this.BoxFiliaCtps.TabIndex = 199;
            this.BoxFiliaCtps.TabStop = false;
            // 
            // LstFilialCtps
            // 
            this.LstFilialCtps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilialCtps.FormattingEnabled = true;
            this.LstFilialCtps.Items.AddRange(new object[] {
            "Analfaberto",
            "Fundamental",
            "Médio",
            "Superior Incompleto",
            "Superior Completo",
            "Curso Técnico"});
            this.LstFilialCtps.Location = new System.Drawing.Point(78, 11);
            this.LstFilialCtps.Name = "LstFilialCtps";
            this.LstFilialCtps.Size = new System.Drawing.Size(252, 21);
            this.LstFilialCtps.TabIndex = 188;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 190;
            this.label2.Text = "Filial CTPS:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.UseMnemonic = false;
            // 
            // BoxFunc
            // 
            this.BoxFunc.BackColor = System.Drawing.Color.Transparent;
            this.BoxFunc.Controls.Add(this.TxtCodFunc);
            this.BoxFunc.Controls.Add(this.TxtFuncionario);
            this.BoxFunc.Controls.Add(BtnBuscaFunc);
            this.BoxFunc.Controls.Add(this.label3);
            this.BoxFunc.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxFunc.Location = new System.Drawing.Point(240, 179);
            this.BoxFunc.Name = "BoxFunc";
            this.BoxFunc.Size = new System.Drawing.Size(346, 50);
            this.BoxFunc.TabIndex = 200;
            this.BoxFunc.TabStop = false;
            // 
            // TxtCodFunc
            // 
            this.TxtCodFunc.Enabled = false;
            this.TxtCodFunc.Location = new System.Drawing.Point(32, 24);
            this.TxtCodFunc.MaxLength = 40;
            this.TxtCodFunc.Name = "TxtCodFunc";
            this.TxtCodFunc.Size = new System.Drawing.Size(48, 20);
            this.TxtCodFunc.TabIndex = 157;
            this.TxtCodFunc.Text = "0";
            // 
            // TxtFuncionario
            // 
            this.TxtFuncionario.Enabled = false;
            this.TxtFuncionario.Location = new System.Drawing.Point(86, 24);
            this.TxtFuncionario.MaxLength = 40;
            this.TxtFuncionario.Name = "TxtFuncionario";
            this.TxtFuncionario.Size = new System.Drawing.Size(252, 20);
            this.TxtFuncionario.TabIndex = 156;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 154;
            this.label3.Text = "Funcionario:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Op12
            // 
            this.Op12.AutoSize = true;
            this.Op12.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op12.Location = new System.Drawing.Point(3, 203);
            this.Op12.Name = "Op12";
            this.Op12.Size = new System.Drawing.Size(234, 17);
            this.Op12.TabIndex = 110;
            this.Op12.TabStop = true;
            this.Op12.Text = "Listagem Unimed";
            this.Op12.UseVisualStyleBackColor = true;
            this.Op12.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // FrmRelRH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(586, 279);
            this.Controls.Add(this.BoxFunc);
            this.Controls.Add(this.BoxFiliaCtps);
            this.Controls.Add(this.BoxEventos);
            this.Controls.Add(this.BoxDepart);
            this.Controls.Add(this.BoxFilial);
            this.Controls.Add(this.BoxMesAno);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnImprimir);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelRH";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatórios R&H-Recursos Humanos";
            this.Load += new System.EventHandler(this.FrmRelRH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TxtAnoEventos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.BoxMesAno.ResumeLayout(false);
            this.BoxMesAno.PerformLayout();
            this.BoxFilial.ResumeLayout(false);
            this.BoxFilial.PerformLayout();
            this.BoxDepart.ResumeLayout(false);
            this.BoxDepart.PerformLayout();
            this.BoxEventos.ResumeLayout(false);
            this.BoxEventos.PerformLayout();
            this.BoxFiliaCtps.ResumeLayout(false);
            this.BoxFiliaCtps.PerformLayout();
            this.BoxFunc.ResumeLayout(false);
            this.BoxFunc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.NumericUpDown TxtAnoEventos;
        private System.Windows.Forms.ComboBox LstMesEventos;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ComboBox LstDepartamento;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Op04;
        private System.Windows.Forms.RadioButton Op03;
        private System.Windows.Forms.RadioButton Op02;
        private System.Windows.Forms.RadioButton Op01;
        private System.Windows.Forms.GroupBox BoxMesAno;
        private System.Windows.Forms.GroupBox BoxFilial;
        private System.Windows.Forms.GroupBox BoxDepart;
        private System.Windows.Forms.GroupBox BoxEventos;
        private System.Windows.Forms.ComboBox LstEventos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton Op05;
        private System.Windows.Forms.RadioButton Op06;
        private System.Windows.Forms.RadioButton Op07;
        private System.Windows.Forms.GroupBox BoxFiliaCtps;
        private System.Windows.Forms.ComboBox LstFilialCtps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton Op08;
        private System.Windows.Forms.GroupBox BoxFunc;
        private System.Windows.Forms.TextBox TxtCodFunc;
        private System.Windows.Forms.TextBox TxtFuncionario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Cb_Quizena;
        private System.Windows.Forms.RadioButton Op09;
        private System.Windows.Forms.RadioButton Op10;
        private System.Windows.Forms.RadioButton Op11;
        private System.Windows.Forms.RadioButton Op12;
    }
}