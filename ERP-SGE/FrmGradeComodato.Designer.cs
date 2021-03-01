namespace ERP_SGE
{
    partial class FrmGradeComodato
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGradeComodato));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.IdGrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsGrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pag02 = new System.Windows.Forms.TabPage();
            this.BoxVinculo = new System.Windows.Forms.GroupBox();
            this.GridVinc = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NavegadorVinc = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.BoxComodato = new System.Windows.Forms.GroupBox();
            this.GridItens = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NavegadorComod = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnIncComod = new System.Windows.Forms.ToolStripButton();
            this.BtnExcComod = new System.Windows.Forms.ToolStripButton();
            this.TxtQtde = new System.Windows.Forms.NumericUpDown();
            this.label37 = new System.Windows.Forms.Label();
            this.TxtGrade = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Paginas = new System.Windows.Forms.TabControl();
            this.Pag01 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.Pag02.SuspendLayout();
            this.BoxVinculo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridVinc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavegadorVinc)).BeginInit();
            this.NavegadorVinc.SuspendLayout();
            this.BoxComodato.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavegadorComod)).BeginInit();
            this.NavegadorComod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtQtde)).BeginInit();
            this.Paginas.SuspendLayout();
            this.Pag01.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridDados
            // 
            this.GridDados.AllowUserToAddRows = false;
            this.GridDados.AllowUserToDeleteRows = false;
            this.GridDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdGrade,
            this.DsGrade});
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(3, 3);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            this.GridDados.ReadOnly = true;
            this.GridDados.RowHeadersWidth = 15;
            this.GridDados.Size = new System.Drawing.Size(843, 507);
            this.GridDados.TabIndex = 4;
            // 
            // IdGrade
            // 
            this.IdGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdGrade.DataPropertyName = "Id_Grade";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "000";
            this.IdGrade.DefaultCellStyle = dataGridViewCellStyle1;
            this.IdGrade.HeaderText = "Código";
            this.IdGrade.Name = "IdGrade";
            this.IdGrade.ReadOnly = true;
            this.IdGrade.Width = 50;
            // 
            // DsGrade
            // 
            this.DsGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DsGrade.DataPropertyName = "Grade";
            this.DsGrade.HeaderText = "Descrição da Grade do Comodato";
            this.DsGrade.Name = "DsGrade";
            this.DsGrade.ReadOnly = true;
            this.DsGrade.Width = 300;
            // 
            // Pag02
            // 
            this.Pag02.BackColor = System.Drawing.Color.White;
            this.Pag02.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.Pag02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pag02.Controls.Add(this.BoxVinculo);
            this.Pag02.Controls.Add(this.BoxComodato);
            this.Pag02.Controls.Add(this.TxtQtde);
            this.Pag02.Controls.Add(this.label37);
            this.Pag02.Controls.Add(this.TxtGrade);
            this.Pag02.Controls.Add(this.label2);
            this.Pag02.Controls.Add(this.TxtCodigo);
            this.Pag02.Controls.Add(this.label1);
            this.Pag02.Location = new System.Drawing.Point(4, 22);
            this.Pag02.Name = "Pag02";
            this.Pag02.Padding = new System.Windows.Forms.Padding(3);
            this.Pag02.Size = new System.Drawing.Size(849, 513);
            this.Pag02.TabIndex = 1;
            this.Pag02.Text = "Pagina de Dados";
            // 
            // BoxVinculo
            // 
            this.BoxVinculo.BackColor = System.Drawing.Color.Transparent;
            this.BoxVinculo.Controls.Add(this.GridVinc);
            this.BoxVinculo.Controls.Add(this.NavegadorVinc);
            this.BoxVinculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxVinculo.Location = new System.Drawing.Point(3, 276);
            this.BoxVinculo.Name = "BoxVinculo";
            this.BoxVinculo.Size = new System.Drawing.Size(544, 205);
            this.BoxVinculo.TabIndex = 142;
            this.BoxVinculo.TabStop = false;
            this.BoxVinculo.Text = "Produtos Vinculados  a Grade";
            // 
            // GridVinc
            // 
            this.GridVinc.AllowUserToAddRows = false;
            this.GridVinc.AllowUserToDeleteRows = false;
            this.GridVinc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridVinc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridVinc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.GridVinc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridVinc.Location = new System.Drawing.Point(3, 16);
            this.GridVinc.MultiSelect = false;
            this.GridVinc.Name = "GridVinc";
            this.GridVinc.ReadOnly = true;
            this.GridVinc.RowHeadersWidth = 15;
            this.GridVinc.Size = new System.Drawing.Size(538, 166);
            this.GridVinc.TabIndex = 6;
            this.GridVinc.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridItemComodVinc_CellBeginEdit);
            this.GridVinc.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridItemComodVinc_CellEndEdit);
            this.GridVinc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridItemVinc_KeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id_Lanc";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "000";
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.HeaderText = "Cód.Item";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Referencia";
            this.dataGridViewTextBoxColumn2.HeaderText = "Ref.";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Descricao";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn3.HeaderText = "Descrição do Produto";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 400;
            // 
            // NavegadorVinc
            // 
            this.NavegadorVinc.AddNewItem = null;
            this.NavegadorVinc.AutoSize = false;
            this.NavegadorVinc.CountItem = this.toolStripLabel2;
            this.NavegadorVinc.CountItemFormat = "de {0}";
            this.NavegadorVinc.DeleteItem = null;
            this.NavegadorVinc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.NavegadorVinc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripSeparator4,
            this.toolStripTextBox2,
            this.toolStripLabel2,
            this.toolStripSeparator5,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripSeparator6,
            this.toolStripButton9,
            this.toolStripButton10});
            this.NavegadorVinc.Location = new System.Drawing.Point(3, 182);
            this.NavegadorVinc.MoveFirstItem = this.toolStripButton5;
            this.NavegadorVinc.MoveLastItem = this.toolStripButton8;
            this.NavegadorVinc.MoveNextItem = this.toolStripButton7;
            this.NavegadorVinc.MovePreviousItem = this.toolStripButton6;
            this.NavegadorVinc.Name = "NavegadorVinc";
            this.NavegadorVinc.PositionItem = this.toolStripTextBox2;
            this.NavegadorVinc.Size = new System.Drawing.Size(538, 20);
            this.NavegadorVinc.TabIndex = 33;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(37, 17);
            this.toolStripLabel2.Text = "de {0}";
            this.toolStripLabel2.ToolTipText = "Total number of items";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.RightToLeftAutoMirrorImage = true;
            this.toolStripButton5.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton5.Text = "Primeiro Registro";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.RightToLeftAutoMirrorImage = true;
            this.toolStripButton6.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton6.Text = "Registro Anterior";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.AccessibleName = "Position";
            this.toolStripTextBox2.AutoSize = false;
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(50, 23);
            this.toolStripTextBox2.Text = "0";
            this.toolStripTextBox2.ToolTipText = "Registro Atual";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.RightToLeftAutoMirrorImage = true;
            this.toolStripButton7.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton7.Text = "Proximo Registro";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.RightToLeftAutoMirrorImage = true;
            this.toolStripButton8.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton8.Text = "Ultimo Registro";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.RightToLeftAutoMirrorImage = true;
            this.toolStripButton9.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton9.Text = "Incluir Registro";
            this.toolStripButton9.Click += new System.EventHandler(this.BtnIncComodVinc_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.RightToLeftAutoMirrorImage = true;
            this.toolStripButton10.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton10.Text = "Excluir Registro";
            this.toolStripButton10.Click += new System.EventHandler(this.BtnExcComodVinc_Click);
            // 
            // BoxComodato
            // 
            this.BoxComodato.BackColor = System.Drawing.Color.Transparent;
            this.BoxComodato.Controls.Add(this.GridItens);
            this.BoxComodato.Controls.Add(this.NavegadorComod);
            this.BoxComodato.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxComodato.Location = new System.Drawing.Point(6, 51);
            this.BoxComodato.Name = "BoxComodato";
            this.BoxComodato.Size = new System.Drawing.Size(544, 219);
            this.BoxComodato.TabIndex = 141;
            this.BoxComodato.TabStop = false;
            this.BoxComodato.Text = "Produtos da Grade";
            // 
            // GridItens
            // 
            this.GridItens.AllowUserToAddRows = false;
            this.GridItens.AllowUserToDeleteRows = false;
            this.GridItens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.GridItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridItens.Location = new System.Drawing.Point(3, 16);
            this.GridItens.MultiSelect = false;
            this.GridItens.Name = "GridItens";
            this.GridItens.ReadOnly = true;
            this.GridItens.RowHeadersWidth = 15;
            this.GridItens.Size = new System.Drawing.Size(538, 180);
            this.GridItens.TabIndex = 6;
            this.GridItens.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridItemComod_CellBeginEdit);
            this.GridItens.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridItemComod_CellEndEdit);
            this.GridItens.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridItemComod_KeyDown);
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Id_Lanc";
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = "000";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.HeaderText = "Cód.Item";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Referencia";
            this.dataGridViewTextBoxColumn6.HeaderText = "Ref.";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Descricao";
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn7.HeaderText = "Descrição do Produto";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 400;
            // 
            // NavegadorComod
            // 
            this.NavegadorComod.AddNewItem = null;
            this.NavegadorComod.AutoSize = false;
            this.NavegadorComod.CountItem = this.toolStripLabel1;
            this.NavegadorComod.CountItemFormat = "de {0}";
            this.NavegadorComod.DeleteItem = null;
            this.NavegadorComod.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.NavegadorComod.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripTextBox1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator3,
            this.BtnIncComod,
            this.BtnExcComod});
            this.NavegadorComod.Location = new System.Drawing.Point(3, 196);
            this.NavegadorComod.MoveFirstItem = this.toolStripButton1;
            this.NavegadorComod.MoveLastItem = this.toolStripButton4;
            this.NavegadorComod.MoveNextItem = this.toolStripButton3;
            this.NavegadorComod.MovePreviousItem = this.toolStripButton2;
            this.NavegadorComod.Name = "NavegadorComod";
            this.NavegadorComod.PositionItem = this.toolStripTextBox1;
            this.NavegadorComod.Size = new System.Drawing.Size(538, 20);
            this.NavegadorComod.TabIndex = 33;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(37, 17);
            this.toolStripLabel1.Text = "de {0}";
            this.toolStripLabel1.ToolTipText = "Total number of items";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeftAutoMirrorImage = true;
            this.toolStripButton1.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton1.Text = "Primeiro Registro";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.RightToLeftAutoMirrorImage = true;
            this.toolStripButton2.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton2.Text = "Registro Anterior";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AccessibleName = "Position";
            this.toolStripTextBox1.AutoSize = false;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(50, 23);
            this.toolStripTextBox1.Text = "0";
            this.toolStripTextBox1.ToolTipText = "Registro Atual";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.RightToLeftAutoMirrorImage = true;
            this.toolStripButton3.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton3.Text = "Proximo Registro";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.RightToLeftAutoMirrorImage = true;
            this.toolStripButton4.Size = new System.Drawing.Size(23, 17);
            this.toolStripButton4.Text = "Ultimo Registro";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 20);
            // 
            // BtnIncComod
            // 
            this.BtnIncComod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnIncComod.Image = ((System.Drawing.Image)(resources.GetObject("BtnIncComod.Image")));
            this.BtnIncComod.Name = "BtnIncComod";
            this.BtnIncComod.RightToLeftAutoMirrorImage = true;
            this.BtnIncComod.Size = new System.Drawing.Size(23, 17);
            this.BtnIncComod.Text = "Incluir Registro";
            this.BtnIncComod.Click += new System.EventHandler(this.BtnIncComod_Click);
            // 
            // BtnExcComod
            // 
            this.BtnExcComod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExcComod.Image = ((System.Drawing.Image)(resources.GetObject("BtnExcComod.Image")));
            this.BtnExcComod.Name = "BtnExcComod";
            this.BtnExcComod.RightToLeftAutoMirrorImage = true;
            this.BtnExcComod.Size = new System.Drawing.Size(23, 17);
            this.BtnExcComod.Text = "Excluir Registro";
            this.BtnExcComod.Click += new System.EventHandler(this.BtnExcComod_Click);
            // 
            // TxtQtde
            // 
            this.TxtQtde.Location = new System.Drawing.Point(576, 14);
            this.TxtQtde.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.TxtQtde.Name = "TxtQtde";
            this.TxtQtde.Size = new System.Drawing.Size(63, 20);
            this.TxtQtde.TabIndex = 139;
            this.TxtQtde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtQtde.ThousandsSeparator = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label37.Location = new System.Drawing.Point(495, 20);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(81, 13);
            this.label37.TabIndex = 140;
            this.label37.Text = "Qtde.Minima:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtGrade
            // 
            this.TxtGrade.Location = new System.Drawing.Point(177, 16);
            this.TxtGrade.MaxLength = 100;
            this.TxtGrade.Name = "TxtGrade";
            this.TxtGrade.Size = new System.Drawing.Size(312, 20);
            this.TxtGrade.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(135, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Grade:";
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
            // Paginas
            // 
            this.Paginas.Controls.Add(this.Pag01);
            this.Paginas.Controls.Add(this.Pag02);
            this.Paginas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Paginas.Location = new System.Drawing.Point(0, 0);
            this.Paginas.Name = "Paginas";
            this.Paginas.SelectedIndex = 0;
            this.Paginas.Size = new System.Drawing.Size(857, 539);
            this.Paginas.TabIndex = 10;
            this.Paginas.SelectedIndexChanged += new System.EventHandler(this.Paginas_SelectedIndexChanged);
            // 
            // Pag01
            // 
            this.Pag01.Controls.Add(this.GridDados);
            this.Pag01.Location = new System.Drawing.Point(4, 22);
            this.Pag01.Name = "Pag01";
            this.Pag01.Padding = new System.Windows.Forms.Padding(3);
            this.Pag01.Size = new System.Drawing.Size(849, 513);
            this.Pag01.TabIndex = 0;
            this.Pag01.Text = "Pesquisa";
            this.Pag01.UseVisualStyleBackColor = true;
            // 
            // FrmGradeComodato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 539);
            this.Controls.Add(this.Paginas);
            this.Name = "FrmGradeComodato";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Grade de Comodato";
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.FrmPremiacao_Load);
            this.Activated += new System.EventHandler(this.Frm_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.Pag02.ResumeLayout(false);
            this.Pag02.PerformLayout();
            this.BoxVinculo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridVinc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavegadorVinc)).EndInit();
            this.NavegadorVinc.ResumeLayout(false);
            this.NavegadorVinc.PerformLayout();
            this.BoxComodato.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavegadorComod)).EndInit();
            this.NavegadorComod.ResumeLayout(false);
            this.NavegadorComod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtQtde)).EndInit();
            this.Paginas.ResumeLayout(false);
            this.Pag01.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.TabPage Pag02;
        private System.Windows.Forms.TextBox TxtGrade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl Paginas;
        private System.Windows.Forms.TabPage Pag01;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdGrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsGrade;
        private System.Windows.Forms.NumericUpDown TxtQtde;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.GroupBox BoxVinculo;
        private System.Windows.Forms.DataGridView GridVinc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.BindingNavigator NavegadorVinc;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.GroupBox BoxComodato;
        private System.Windows.Forms.DataGridView GridItens;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.BindingNavigator NavegadorComod;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton BtnIncComod;
        private System.Windows.Forms.ToolStripButton BtnExcComod;
    }
}