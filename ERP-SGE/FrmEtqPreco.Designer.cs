namespace ERP_SGE
{
    partial class FrmEtqPreco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEtqPreco));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Rb_ImpArgox = new System.Windows.Forms.RadioButton();
            this.Rb_ImpDr800 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtQtdeEtq = new System.Windows.Forms.NumericUpDown();
            this.BtnImpGrupo = new System.Windows.Forms.Button();
            this.BoxPreco = new System.Windows.Forms.GroupBox();
            this.Rb_PrcEspecial = new System.Windows.Forms.RadioButton();
            this.Rb_PrcMinimo = new System.Windows.Forms.RadioButton();
            this.Rb_PrcVarejo = new System.Windows.Forms.RadioButton();
            this.TxtCodPrd = new System.Windows.Forms.TextBox();
            this.TxtDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LstGrupo = new System.Windows.Forms.ComboBox();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.Col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEstoque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rb_PrcSensac = new System.Windows.Forms.RadioButton();
            BtnBuscaPrd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtQtdeEtq)).BeginInit();
            this.BoxPreco.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnBuscaPrd
            // 
            BtnBuscaPrd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBuscaPrd.BackgroundImage")));
            BtnBuscaPrd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            BtnBuscaPrd.Location = new System.Drawing.Point(57, 85);
            BtnBuscaPrd.Name = "BtnBuscaPrd";
            BtnBuscaPrd.Size = new System.Drawing.Size(20, 20);
            BtnBuscaPrd.TabIndex = 145;
            BtnBuscaPrd.UseVisualStyleBackColor = true;
            BtnBuscaPrd.Click += new System.EventHandler(this.BtnBuscaPrd_Click);
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("BtnImprimir.Image")));
            this.BtnImprimir.Location = new System.Drawing.Point(480, 82);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(90, 25);
            this.BtnImprimir.TabIndex = 141;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtQtdeEtq);
            this.panel1.Controls.Add(this.BtnImpGrupo);
            this.panel1.Controls.Add(this.BoxPreco);
            this.panel1.Controls.Add(this.TxtCodPrd);
            this.panel1.Controls.Add(this.TxtDescricao);
            this.panel1.Controls.Add(BtnBuscaPrd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.LstGrupo);
            this.panel1.Controls.Add(this.BtnImprimir);
            this.panel1.Controls.Add(this.BtnExcluir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 109);
            this.panel1.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Rb_ImpArgox);
            this.groupBox1.Controls.Add(this.Rb_ImpDr800);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(515, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(83, 63);
            this.groupBox1.TabIndex = 193;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Impressora";
            // 
            // Rb_ImpArgox
            // 
            this.Rb_ImpArgox.AutoSize = true;
            this.Rb_ImpArgox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_ImpArgox.Location = new System.Drawing.Point(6, 16);
            this.Rb_ImpArgox.Name = "Rb_ImpArgox";
            this.Rb_ImpArgox.Size = new System.Drawing.Size(57, 17);
            this.Rb_ImpArgox.TabIndex = 1;
            this.Rb_ImpArgox.TabStop = true;
            this.Rb_ImpArgox.Text = "Argox";
            this.Rb_ImpArgox.UseVisualStyleBackColor = true;
            // 
            // Rb_ImpDr800
            // 
            this.Rb_ImpDr800.AutoSize = true;
            this.Rb_ImpDr800.Location = new System.Drawing.Point(6, 39);
            this.Rb_ImpDr800.Name = "Rb_ImpDr800";
            this.Rb_ImpDr800.Size = new System.Drawing.Size(68, 17);
            this.Rb_ImpDr800.TabIndex = 2;
            this.Rb_ImpDr800.TabStop = true;
            this.Rb_ImpDr800.Text = "DR-800";
            this.Rb_ImpDr800.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(337, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 192;
            this.label3.Text = "Qtde. Etiq. por Item:";
            // 
            // TxtQtdeEtq
            // 
            this.TxtQtdeEtq.Location = new System.Drawing.Point(459, 18);
            this.TxtQtdeEtq.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.TxtQtdeEtq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TxtQtdeEtq.Name = "TxtQtdeEtq";
            this.TxtQtdeEtq.Size = new System.Drawing.Size(49, 20);
            this.TxtQtdeEtq.TabIndex = 191;
            this.TxtQtdeEtq.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // BtnImpGrupo
            // 
            this.BtnImpGrupo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImpGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImpGrupo.Image = ((System.Drawing.Image)(resources.GetObject("BtnImpGrupo.Image")));
            this.BtnImpGrupo.Location = new System.Drawing.Point(369, 49);
            this.BtnImpGrupo.Name = "BtnImpGrupo";
            this.BtnImpGrupo.Size = new System.Drawing.Size(105, 25);
            this.BtnImpGrupo.TabIndex = 190;
            this.BtnImpGrupo.Text = "Importar Grupo";
            this.BtnImpGrupo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImpGrupo.UseVisualStyleBackColor = true;
            this.BtnImpGrupo.Click += new System.EventHandler(this.BtnImpGrupo_Click);
            // 
            // BoxPreco
            // 
            this.BoxPreco.Controls.Add(this.Rb_PrcSensac);
            this.BoxPreco.Controls.Add(this.Rb_PrcEspecial);
            this.BoxPreco.Controls.Add(this.Rb_PrcMinimo);
            this.BoxPreco.Controls.Add(this.Rb_PrcVarejo);
            this.BoxPreco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxPreco.Location = new System.Drawing.Point(10, 5);
            this.BoxPreco.Name = "BoxPreco";
            this.BoxPreco.Size = new System.Drawing.Size(324, 35);
            this.BoxPreco.TabIndex = 149;
            this.BoxPreco.TabStop = false;
            this.BoxPreco.Text = "Selecione o Preço";
            // 
            // Rb_PrcEspecial
            // 
            this.Rb_PrcEspecial.AutoSize = true;
            this.Rb_PrcEspecial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_PrcEspecial.Location = new System.Drawing.Point(106, 16);
            this.Rb_PrcEspecial.Name = "Rb_PrcEspecial";
            this.Rb_PrcEspecial.Size = new System.Drawing.Size(73, 17);
            this.Rb_PrcEspecial.TabIndex = 1;
            this.Rb_PrcEspecial.TabStop = true;
            this.Rb_PrcEspecial.Text = "Especial";
            this.Rb_PrcEspecial.UseVisualStyleBackColor = true;
            // 
            // Rb_PrcMinimo
            // 
            this.Rb_PrcMinimo.AutoSize = true;
            this.Rb_PrcMinimo.Location = new System.Drawing.Point(254, 16);
            this.Rb_PrcMinimo.Name = "Rb_PrcMinimo";
            this.Rb_PrcMinimo.Size = new System.Drawing.Size(64, 17);
            this.Rb_PrcMinimo.TabIndex = 3;
            this.Rb_PrcMinimo.TabStop = true;
            this.Rb_PrcMinimo.Text = "Minimo";
            this.Rb_PrcMinimo.UseVisualStyleBackColor = true;
            // 
            // Rb_PrcVarejo
            // 
            this.Rb_PrcVarejo.AutoSize = true;
            this.Rb_PrcVarejo.Location = new System.Drawing.Point(187, 16);
            this.Rb_PrcVarejo.Name = "Rb_PrcVarejo";
            this.Rb_PrcVarejo.Size = new System.Drawing.Size(61, 17);
            this.Rb_PrcVarejo.TabIndex = 2;
            this.Rb_PrcVarejo.TabStop = true;
            this.Rb_PrcVarejo.Text = "Varejo";
            this.Rb_PrcVarejo.UseVisualStyleBackColor = true;
            // 
            // TxtCodPrd
            // 
            this.TxtCodPrd.Enabled = false;
            this.TxtCodPrd.Location = new System.Drawing.Point(77, 85);
            this.TxtCodPrd.MaxLength = 40;
            this.TxtCodPrd.Name = "TxtCodPrd";
            this.TxtCodPrd.Size = new System.Drawing.Size(48, 20);
            this.TxtCodPrd.TabIndex = 148;
            this.TxtCodPrd.Text = "0";
            // 
            // TxtDescricao
            // 
            this.TxtDescricao.Enabled = false;
            this.TxtDescricao.Location = new System.Drawing.Point(127, 85);
            this.TxtDescricao.MaxLength = 40;
            this.TxtDescricao.Name = "TxtDescricao";
            this.TxtDescricao.Size = new System.Drawing.Size(236, 20);
            this.TxtDescricao.TabIndex = 146;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(7, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 147;
            this.label2.Text = "Produto";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 144;
            this.label1.Text = "Grupo:";
            // 
            // LstGrupo
            // 
            this.LstGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstGrupo.FormattingEnabled = true;
            this.LstGrupo.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia"});
            this.LstGrupo.Location = new System.Drawing.Point(57, 51);
            this.LstGrupo.Name = "LstGrupo";
            this.LstGrupo.Size = new System.Drawing.Size(306, 21);
            this.LstGrupo.TabIndex = 143;
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("BtnExcluir.Image")));
            this.BtnExcluir.Location = new System.Drawing.Point(369, 82);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(90, 25);
            this.BtnExcluir.TabIndex = 71;
            this.BtnExcluir.Text = "Excluir Item";
            this.BtnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
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
            this.Col1,
            this.Col2,
            this.Col3,
            this.Col4,
            this.ColEstoque,
            this.Col5,
            this.Col6});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle6;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 109);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.GridDados.RowHeadersWidth = 5;
            this.GridDados.Size = new System.Drawing.Size(676, 314);
            this.GridDados.TabIndex = 7;
            this.GridDados.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridDados_CellEndEdit);
            // 
            // Col1
            // 
            this.Col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Col1.DataPropertyName = "Id_Produto";
            this.Col1.HeaderText = "Codigo";
            this.Col1.Name = "Col1";
            this.Col1.ReadOnly = true;
            this.Col1.Visible = false;
            this.Col1.Width = 70;
            // 
            // Col2
            // 
            this.Col2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Col2.DataPropertyName = "Referencia";
            this.Col2.HeaderText = "Referencia";
            this.Col2.Name = "Col2";
            this.Col2.ReadOnly = true;
            this.Col2.Width = 70;
            // 
            // Col3
            // 
            this.Col3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Col3.DataPropertyName = "Descricao";
            dataGridViewCellStyle2.NullValue = null;
            this.Col3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Col3.FillWeight = 200F;
            this.Col3.HeaderText = "Descrição";
            this.Col3.Name = "Col3";
            this.Col3.ReadOnly = true;
            this.Col3.Width = 250;
            // 
            // Col4
            // 
            this.Col4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Col4.DataPropertyName = "Preco";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0,00";
            this.Col4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Col4.HeaderText = "Preço";
            this.Col4.Name = "Col4";
            this.Col4.Width = 80;
            // 
            // ColEstoque
            // 
            this.ColEstoque.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColEstoque.DataPropertyName = "ESTOQUE";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N0";
            this.ColEstoque.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColEstoque.HeaderText = "Estoque";
            this.ColEstoque.Name = "ColEstoque";
            this.ColEstoque.ReadOnly = true;
            this.ColEstoque.Width = 70;
            // 
            // Col5
            // 
            this.Col5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Col5.DataPropertyName = "CodBarra";
            this.Col5.HeaderText = "Cad.Barra";
            this.Col5.Name = "Col5";
            this.Col5.ReadOnly = true;
            // 
            // Col6
            // 
            this.Col6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Col6.DataPropertyName = "Qtde";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.Col6.DefaultCellStyle = dataGridViewCellStyle5;
            this.Col6.HeaderText = "Qtde.Etq";
            this.Col6.Name = "Col6";
            this.Col6.Width = 60;
            // 
            // Rb_PrcSensac
            // 
            this.Rb_PrcSensac.AutoSize = true;
            this.Rb_PrcSensac.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_PrcSensac.Location = new System.Drawing.Point(6, 15);
            this.Rb_PrcSensac.Name = "Rb_PrcSensac";
            this.Rb_PrcSensac.Size = new System.Drawing.Size(94, 17);
            this.Rb_PrcSensac.TabIndex = 4;
            this.Rb_PrcSensac.TabStop = true;
            this.Rb_PrcSensac.Text = "Sensacional";
            this.Rb_PrcSensac.UseVisualStyleBackColor = true;
            // 
            // FrmEtqPreco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 423);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEtqPreco";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiqueta de Preço ";
            this.Load += new System.EventHandler(this.FrmEtqPreco_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtQtdeEtq)).EndInit();
            this.BoxPreco.ResumeLayout(false);
            this.BoxPreco.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.TextBox TxtCodPrd;
        private System.Windows.Forms.TextBox TxtDescricao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LstGrupo;
        public System.Windows.Forms.GroupBox BoxPreco;
        private System.Windows.Forms.RadioButton Rb_PrcEspecial;
        private System.Windows.Forms.RadioButton Rb_PrcMinimo;
        private System.Windows.Forms.RadioButton Rb_PrcVarejo;
        private System.Windows.Forms.Button BtnImpGrupo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TxtQtdeEtq;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEstoque;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col6;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Rb_ImpArgox;
        private System.Windows.Forms.RadioButton Rb_ImpDr800;
        private System.Windows.Forms.RadioButton Rb_PrcSensac;
    }
}