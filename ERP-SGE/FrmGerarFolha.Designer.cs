namespace ERP_SGE
{
    partial class FrmGerarFolha
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button BtnBuscaFunc;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGerarFolha));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label57 = new System.Windows.Forms.Label();
            this.LstFilialPesq = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtAnoEventos = new System.Windows.Forms.NumericUpDown();
            this.LstMesEventos = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.BtnGerarFP = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn13Sal = new System.Windows.Forms.Button();
            this.BtnGeraAdiant = new System.Windows.Forms.Button();
            this.BarProc = new System.Windows.Forms.ProgressBar();
            this.BoxItens = new System.Windows.Forms.GroupBox();
            this.GridEventos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProvDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEvento = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColQtde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColVlrPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Navegador = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnInc = new System.Windows.Forms.ToolStripButton();
            this.BtnExc = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TxtMatricula = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCodFunc = new System.Windows.Forms.TextBox();
            this.TxtFuncionario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LblSaldo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LblTotalDesc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblTotalProv = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtLotado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            BtnBuscaFunc = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAnoEventos)).BeginInit();
            this.panel2.SuspendLayout();
            this.BoxItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridEventos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Navegador)).BeginInit();
            this.Navegador.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnBuscaFunc
            // 
            BtnBuscaFunc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBuscaFunc.BackgroundImage")));
            BtnBuscaFunc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            BtnBuscaFunc.Location = new System.Drawing.Point(80, 7);
            BtnBuscaFunc.Name = "BtnBuscaFunc";
            BtnBuscaFunc.Size = new System.Drawing.Size(20, 20);
            BtnBuscaFunc.TabIndex = 151;
            BtnBuscaFunc.UseVisualStyleBackColor = true;
            BtnBuscaFunc.Click += new System.EventHandler(this.BtnBuscaFunc_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label57);
            this.panel1.Controls.Add(this.LstFilialPesq);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.TxtAnoEventos);
            this.panel1.Controls.Add(this.LstMesEventos);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 31);
            this.panel1.TabIndex = 0;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label57.Location = new System.Drawing.Point(247, 9);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(37, 13);
            this.label57.TabIndex = 189;
            this.label57.Text = "Filial:";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label57.UseMnemonic = false;
            // 
            // LstFilialPesq
            // 
            this.LstFilialPesq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilialPesq.FormattingEnabled = true;
            this.LstFilialPesq.Items.AddRange(new object[] {
            "Analfaberto",
            "Fundamental",
            "Médio",
            "Superior Incompleto",
            "Superior Completo",
            "Curso Técnico"});
            this.LstFilialPesq.Location = new System.Drawing.Point(286, 4);
            this.LstFilialPesq.Name = "LstFilialPesq";
            this.LstFilialPesq.Size = new System.Drawing.Size(287, 21);
            this.LstFilialPesq.TabIndex = 188;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(147, 9);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(33, 13);
            this.label29.TabIndex = 64;
            this.label29.Text = "Ano:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtAnoEventos
            // 
            this.TxtAnoEventos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAnoEventos.Location = new System.Drawing.Point(180, 4);
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
            this.TxtAnoEventos.TabIndex = 63;
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
            this.LstMesEventos.Location = new System.Drawing.Point(40, 4);
            this.LstMesEventos.Name = "LstMesEventos";
            this.LstMesEventos.Size = new System.Drawing.Size(101, 21);
            this.LstMesEventos.TabIndex = 62;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(5, 9);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(34, 13);
            this.label28.TabIndex = 61;
            this.label28.Text = "Mês:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnGerarFP
            // 
            this.BtnGerarFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnGerarFP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGerarFP.Image = ((System.Drawing.Image)(resources.GetObject("BtnGerarFP.Image")));
            this.BtnGerarFP.Location = new System.Drawing.Point(8, 3);
            this.BtnGerarFP.Name = "BtnGerarFP";
            this.BtnGerarFP.Size = new System.Drawing.Size(165, 32);
            this.BtnGerarFP.TabIndex = 66;
            this.BtnGerarFP.Text = "Gerar Folha de Pagamento";
            this.BtnGerarFP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnGerarFP.UseVisualStyleBackColor = true;
            this.BtnGerarFP.Click += new System.EventHandler(this.BtnGerarFP_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.Btn13Sal);
            this.panel2.Controls.Add(this.BtnGeraAdiant);
            this.panel2.Controls.Add(this.BarProc);
            this.panel2.Controls.Add(this.BtnGerarFP);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 345);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 48);
            this.panel2.TabIndex = 1;
            // 
            // Btn13Sal
            // 
            this.Btn13Sal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn13Sal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn13Sal.Image = ((System.Drawing.Image)(resources.GetObject("Btn13Sal.Image")));
            this.Btn13Sal.Location = new System.Drawing.Point(459, 3);
            this.Btn13Sal.Name = "Btn13Sal";
            this.Btn13Sal.Size = new System.Drawing.Size(129, 32);
            this.Btn13Sal.TabIndex = 84;
            this.Btn13Sal.Text = "13º Salário";
            this.Btn13Sal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Btn13Sal.UseVisualStyleBackColor = true;
            this.Btn13Sal.Click += new System.EventHandler(this.Btn13Sal_Click);
            // 
            // BtnGeraAdiant
            // 
            this.BtnGeraAdiant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnGeraAdiant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGeraAdiant.Image = ((System.Drawing.Image)(resources.GetObject("BtnGeraAdiant.Image")));
            this.BtnGeraAdiant.Location = new System.Drawing.Point(214, 3);
            this.BtnGeraAdiant.Name = "BtnGeraAdiant";
            this.BtnGeraAdiant.Size = new System.Drawing.Size(196, 32);
            this.BtnGeraAdiant.TabIndex = 83;
            this.BtnGeraAdiant.Text = "Folha de Adiantamento (Quizenal)";
            this.BtnGeraAdiant.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnGeraAdiant.UseVisualStyleBackColor = true;
            this.BtnGeraAdiant.Click += new System.EventHandler(this.BtnGeraAdiant_Click);
            // 
            // BarProc
            // 
            this.BarProc.Location = new System.Drawing.Point(605, 6);
            this.BarProc.Name = "BarProc";
            this.BarProc.Size = new System.Drawing.Size(179, 23);
            this.BarProc.Step = 1;
            this.BarProc.TabIndex = 82;
            this.BarProc.Visible = false;
            // 
            // BoxItens
            // 
            this.BoxItens.BackColor = System.Drawing.Color.Transparent;
            this.BoxItens.Controls.Add(this.GridEventos);
            this.BoxItens.Controls.Add(this.Navegador);
            this.BoxItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BoxItens.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxItens.Location = new System.Drawing.Point(0, 64);
            this.BoxItens.Name = "BoxItens";
            this.BoxItens.Size = new System.Drawing.Size(880, 248);
            this.BoxItens.TabIndex = 177;
            this.BoxItens.TabStop = false;
            this.BoxItens.Text = "Eventos (Proventos e Descontos por Funcionario)";
            // 
            // GridEventos
            // 
            this.GridEventos.AllowUserToAddRows = false;
            this.GridEventos.AllowUserToDeleteRows = false;
            this.GridEventos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridEventos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.ColProvDesc,
            this.ColEvento,
            this.ColQtde,
            this.ColVlrPerc,
            this.ColDescricao});
            this.GridEventos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridEventos.Location = new System.Drawing.Point(3, 16);
            this.GridEventos.MultiSelect = false;
            this.GridEventos.Name = "GridEventos";
            this.GridEventos.Size = new System.Drawing.Size(874, 209);
            this.GridEventos.TabIndex = 6;
            this.GridEventos.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridEventos_CellBeginEdit);
            this.GridEventos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridEventos_CellEndEdit);
            this.GridEventos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridEventos_KeyDown);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Id_Lanc";
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = "000";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "Cód.Item";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // ColProvDesc
            // 
            this.ColProvDesc.DataPropertyName = "ProvDesc";
            this.ColProvDesc.HeaderText = "Tp";
            this.ColProvDesc.Name = "ColProvDesc";
            this.ColProvDesc.Visible = false;
            this.ColProvDesc.Width = 47;
            // 
            // ColEvento
            // 
            this.ColEvento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColEvento.DataPropertyName = "Id_ProvDesc";
            this.ColEvento.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ColEvento.HeaderText = "Provento/Desconto";
            this.ColEvento.Name = "ColEvento";
            this.ColEvento.Width = 350;
            // 
            // ColQtde
            // 
            this.ColQtde.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColQtde.DataPropertyName = "Qtde_Ref";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = "0";
            this.ColQtde.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColQtde.HeaderText = "Qtde.Ref.";
            this.ColQtde.Name = "ColQtde";
            this.ColQtde.Width = 70;
            // 
            // ColVlrPerc
            // 
            this.ColVlrPerc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColVlrPerc.DataPropertyName = "Valor";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0,00";
            this.ColVlrPerc.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColVlrPerc.HeaderText = "R$ ou %";
            this.ColVlrPerc.Name = "ColVlrPerc";
            this.ColVlrPerc.Width = 80;
            // 
            // ColDescricao
            // 
            this.ColDescricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColDescricao.DataPropertyName = "Descricao";
            this.ColDescricao.HeaderText = "Desc.Complemento";
            this.ColDescricao.MaxInputLength = 30;
            this.ColDescricao.Name = "ColDescricao";
            this.ColDescricao.Width = 150;
            // 
            // Navegador
            // 
            this.Navegador.AddNewItem = null;
            this.Navegador.AutoSize = false;
            this.Navegador.CountItem = this.bindingNavigatorCountItem;
            this.Navegador.CountItemFormat = "de {0}";
            this.Navegador.DeleteItem = null;
            this.Navegador.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Navegador.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.BtnInc,
            this.BtnExc});
            this.Navegador.Location = new System.Drawing.Point(3, 225);
            this.Navegador.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.Navegador.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.Navegador.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.Navegador.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.Navegador.Name = "Navegador";
            this.Navegador.PositionItem = this.bindingNavigatorPositionItem;
            this.Navegador.Size = new System.Drawing.Size(874, 20);
            this.Navegador.TabIndex = 33;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(37, 17);
            this.bindingNavigatorCountItem.Text = "de {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 17);
            this.bindingNavigatorMoveFirstItem.Text = "Primeiro Registro";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 17);
            this.bindingNavigatorMovePreviousItem.Text = "Registro Anterior";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 20);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Registro Atual";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 20);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 17);
            this.bindingNavigatorMoveNextItem.Text = "Proximo Registro";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 17);
            this.bindingNavigatorMoveLastItem.Text = "Ultimo Registro";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 20);
            // 
            // BtnInc
            // 
            this.BtnInc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnInc.Image = ((System.Drawing.Image)(resources.GetObject("BtnInc.Image")));
            this.BtnInc.Name = "BtnInc";
            this.BtnInc.RightToLeftAutoMirrorImage = true;
            this.BtnInc.Size = new System.Drawing.Size(23, 17);
            this.BtnInc.Text = "Incluir Registro";
            this.BtnInc.Click += new System.EventHandler(this.BtnInc_Click);
            // 
            // BtnExc
            // 
            this.BtnExc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExc.Image = ((System.Drawing.Image)(resources.GetObject("BtnExc.Image")));
            this.BtnExc.Name = "BtnExc";
            this.BtnExc.RightToLeftAutoMirrorImage = true;
            this.BtnExc.Size = new System.Drawing.Size(23, 17);
            this.BtnExc.Text = "Excluir Registro";
            this.BtnExc.Click += new System.EventHandler(this.BtnExc_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.TxtLotado);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.TxtMatricula);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.TxtCodFunc);
            this.panel3.Controls.Add(this.TxtFuncionario);
            this.panel3.Controls.Add(BtnBuscaFunc);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(880, 33);
            this.panel3.TabIndex = 178;
            // 
            // TxtMatricula
            // 
            this.TxtMatricula.Enabled = false;
            this.TxtMatricula.Location = new System.Drawing.Point(480, 7);
            this.TxtMatricula.MaxLength = 40;
            this.TxtMatricula.Name = "TxtMatricula";
            this.TxtMatricula.Size = new System.Drawing.Size(163, 20);
            this.TxtMatricula.TabIndex = 155;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(418, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 154;
            this.label2.Text = "Matricula:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtCodFunc
            // 
            this.TxtCodFunc.Enabled = false;
            this.TxtCodFunc.Location = new System.Drawing.Point(100, 7);
            this.TxtCodFunc.MaxLength = 40;
            this.TxtCodFunc.Name = "TxtCodFunc";
            this.TxtCodFunc.Size = new System.Drawing.Size(48, 20);
            this.TxtCodFunc.TabIndex = 153;
            this.TxtCodFunc.Text = "0";
            // 
            // TxtFuncionario
            // 
            this.TxtFuncionario.Enabled = false;
            this.TxtFuncionario.Location = new System.Drawing.Point(150, 7);
            this.TxtFuncionario.MaxLength = 40;
            this.TxtFuncionario.Name = "TxtFuncionario";
            this.TxtFuncionario.Size = new System.Drawing.Size(260, 20);
            this.TxtFuncionario.TabIndex = 152;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Funcionario:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.LblSaldo);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.LblTotalDesc);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.LblTotalProv);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 312);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(880, 33);
            this.panel4.TabIndex = 179;
            // 
            // LblSaldo
            // 
            this.LblSaldo.BackColor = System.Drawing.Color.Transparent;
            this.LblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaldo.ForeColor = System.Drawing.Color.Maroon;
            this.LblSaldo.Location = new System.Drawing.Point(678, 8);
            this.LblSaldo.Name = "LblSaldo";
            this.LblSaldo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblSaldo.Size = new System.Drawing.Size(106, 18);
            this.LblSaldo.TabIndex = 160;
            this.LblSaldo.Text = "0,00";
            this.LblSaldo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(602, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 18);
            this.label6.TabIndex = 159;
            this.label6.Text = "Saldo R$:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTotalDesc
            // 
            this.LblTotalDesc.BackColor = System.Drawing.Color.Transparent;
            this.LblTotalDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalDesc.ForeColor = System.Drawing.Color.Maroon;
            this.LblTotalDesc.Location = new System.Drawing.Point(465, 7);
            this.LblTotalDesc.Name = "LblTotalDesc";
            this.LblTotalDesc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblTotalDesc.Size = new System.Drawing.Size(106, 18);
            this.LblTotalDesc.TabIndex = 158;
            this.LblTotalDesc.Text = "0,00";
            this.LblTotalDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(309, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 18);
            this.label5.TabIndex = 157;
            this.label5.Text = "Total Descontos R$:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTotalProv
            // 
            this.LblTotalProv.BackColor = System.Drawing.Color.Transparent;
            this.LblTotalProv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalProv.ForeColor = System.Drawing.Color.Maroon;
            this.LblTotalProv.Location = new System.Drawing.Point(177, 8);
            this.LblTotalProv.Name = "LblTotalProv";
            this.LblTotalProv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblTotalProv.Size = new System.Drawing.Size(106, 18);
            this.LblTotalProv.TabIndex = 156;
            this.LblTotalProv.Text = "0,00";
            this.LblTotalProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 18);
            this.label3.TabIndex = 155;
            this.label3.Text = "Total Proventos R$:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtLotado
            // 
            this.TxtLotado.Enabled = false;
            this.TxtLotado.Location = new System.Drawing.Point(696, 7);
            this.TxtLotado.MaxLength = 40;
            this.TxtLotado.Name = "TxtLotado";
            this.TxtLotado.Size = new System.Drawing.Size(161, 20);
            this.TxtLotado.TabIndex = 157;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(646, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 156;
            this.label4.Text = "Lotado:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmGerarFolha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(880, 393);
            this.Controls.Add(this.BoxItens);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmGerarFolha";
            this.Text = "Gerar Folha de Pagamento";
            this.Activated += new System.EventHandler(this.Frm_Activated);
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.FrmGerarFolha_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAnoEventos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.BoxItens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridEventos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Navegador)).EndInit();
            this.Navegador.ResumeLayout(false);
            this.Navegador.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.NumericUpDown TxtAnoEventos;
        private System.Windows.Forms.ComboBox LstMesEventos;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.Button BtnGerarFP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox BoxItens;
        private System.Windows.Forms.DataGridView GridEventos;
        private System.Windows.Forms.BindingNavigator Navegador;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton BtnInc;
        private System.Windows.Forms.ToolStripButton BtnExc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCodFunc;
        private System.Windows.Forms.TextBox TxtFuncionario;
        private System.Windows.Forms.TextBox TxtMatricula;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProvDesc;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColEvento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQtde;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVlrPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescricao;
        private System.Windows.Forms.ProgressBar BarProc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LblSaldo;
        private System.Windows.Forms.Label LblTotalDesc;
        private System.Windows.Forms.Label LblTotalProv;
        public System.Windows.Forms.Button BtnGeraAdiant;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox LstFilialPesq;
        public System.Windows.Forms.Button Btn13Sal;
        private System.Windows.Forms.TextBox TxtLotado;
        private System.Windows.Forms.Label label4;
    }
}