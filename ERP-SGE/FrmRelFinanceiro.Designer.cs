namespace ERP_SGE
{
    partial class FrmRelFinanceiro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelFinanceiro));
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.Dt2 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Op05 = new System.Windows.Forms.RadioButton();
            this.Op04 = new System.Windows.Forms.RadioButton();
            this.Op03 = new System.Windows.Forms.RadioButton();
            this.Op02 = new System.Windows.Forms.RadioButton();
            this.Op01 = new System.Windows.Forms.RadioButton();
            this.BoxCaixa = new System.Windows.Forms.GroupBox();
            this.LstCaixa = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BoxCusto = new System.Windows.Forms.GroupBox();
            this.LstCusto = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BoxDepart = new System.Windows.Forms.GroupBox();
            this.LstDepartamento = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BoxFilial = new System.Windows.Forms.GroupBox();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Op06 = new System.Windows.Forms.RadioButton();
            this.Op07 = new System.Windows.Forms.RadioButton();
            this.BoxItemPesq.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.BoxCaixa.SuspendLayout();
            this.panel1.SuspendLayout();
            this.BoxCusto.SuspendLayout();
            this.BoxDepart.SuspendLayout();
            this.BoxFilial.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("BtnImprimir.Image")));
            this.BtnImprimir.Location = new System.Drawing.Point(439, 301);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(128, 30);
            this.BtnImprimir.TabIndex = 71;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.BackColor = System.Drawing.Color.Transparent;
            this.BoxItemPesq.Controls.Add(this.label16);
            this.BoxItemPesq.Controls.Add(this.Dt2);
            this.BoxItemPesq.Controls.Add(this.label6);
            this.BoxItemPesq.Controls.Add(this.Dt1);
            this.BoxItemPesq.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxItemPesq.Location = new System.Drawing.Point(0, 0);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(377, 44);
            this.BoxItemPesq.TabIndex = 70;
            this.BoxItemPesq.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(202, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "a";
            // 
            // Dt2
            // 
            this.Dt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt2.Location = new System.Drawing.Point(222, 15);
            this.Dt2.Name = "Dt2";
            this.Dt2.Size = new System.Drawing.Size(100, 20);
            this.Dt2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Data:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Dt1
            // 
            this.Dt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt1.Location = new System.Drawing.Point(96, 14);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(100, 20);
            this.Dt1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Op07);
            this.groupBox1.Controls.Add(this.Op06);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.Op05);
            this.groupBox1.Controls.Add(this.Op04);
            this.groupBox1.Controls.Add(this.Op03);
            this.groupBox1.Controls.Add(this.Op02);
            this.groupBox1.Controls.Add(this.Op01);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 359);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selecione um Relatório";
            // 
            // Op05
            // 
            this.Op05.AutoSize = true;
            this.Op05.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op05.Location = new System.Drawing.Point(3, 84);
            this.Op05.Name = "Op05";
            this.Op05.Size = new System.Drawing.Size(303, 17);
            this.Op05.TabIndex = 103;
            this.Op05.TabStop = true;
            this.Op05.Text = "Totalizador por Centro de Custo";
            this.Op05.UseVisualStyleBackColor = true;
            this.Op05.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op04
            // 
            this.Op04.AutoSize = true;
            this.Op04.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op04.Location = new System.Drawing.Point(3, 67);
            this.Op04.Name = "Op04";
            this.Op04.Size = new System.Drawing.Size(303, 17);
            this.Op04.TabIndex = 102;
            this.Op04.TabStop = true;
            this.Op04.Text = "Resumo do Centro de Custo";
            this.Op04.UseVisualStyleBackColor = true;
            this.Op04.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op03
            // 
            this.Op03.AutoSize = true;
            this.Op03.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op03.Location = new System.Drawing.Point(3, 50);
            this.Op03.Name = "Op03";
            this.Op03.Size = new System.Drawing.Size(303, 17);
            this.Op03.TabIndex = 99;
            this.Op03.TabStop = true;
            this.Op03.Text = "Movimentação por Centro de Custo";
            this.Op03.UseVisualStyleBackColor = true;
            this.Op03.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op02
            // 
            this.Op02.AutoSize = true;
            this.Op02.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op02.Location = new System.Drawing.Point(3, 33);
            this.Op02.Name = "Op02";
            this.Op02.Size = new System.Drawing.Size(303, 17);
            this.Op02.TabIndex = 98;
            this.Op02.TabStop = true;
            this.Op02.Text = "Resumo Receita X Despesa À Vencer";
            this.Op02.UseVisualStyleBackColor = true;
            this.Op02.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op01
            // 
            this.Op01.AutoSize = true;
            this.Op01.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op01.Location = new System.Drawing.Point(3, 16);
            this.Op01.Name = "Op01";
            this.Op01.Size = new System.Drawing.Size(303, 17);
            this.Op01.TabIndex = 96;
            this.Op01.TabStop = true;
            this.Op01.Text = "Livro Caixa";
            this.Op01.UseVisualStyleBackColor = true;
            this.Op01.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // BoxCaixa
            // 
            this.BoxCaixa.BackColor = System.Drawing.Color.Transparent;
            this.BoxCaixa.Controls.Add(this.LstCaixa);
            this.BoxCaixa.Controls.Add(this.label4);
            this.BoxCaixa.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxCaixa.Location = new System.Drawing.Point(0, 44);
            this.BoxCaixa.Name = "BoxCaixa";
            this.BoxCaixa.Size = new System.Drawing.Size(377, 36);
            this.BoxCaixa.TabIndex = 72;
            this.BoxCaixa.TabStop = false;
            // 
            // LstCaixa
            // 
            this.LstCaixa.FormattingEnabled = true;
            this.LstCaixa.Location = new System.Drawing.Point(45, 11);
            this.LstCaixa.Name = "LstCaixa";
            this.LstCaixa.Size = new System.Drawing.Size(277, 21);
            this.LstCaixa.TabIndex = 150;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 149;
            this.label4.Text = "Caixa:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BoxCusto);
            this.panel1.Controls.Add(this.BoxDepart);
            this.panel1.Controls.Add(this.BoxFilial);
            this.panel1.Controls.Add(this.BoxCaixa);
            this.panel1.Controls.Add(this.BoxItemPesq);
            this.panel1.Location = new System.Drawing.Point(315, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 281);
            this.panel1.TabIndex = 73;
            // 
            // BoxCusto
            // 
            this.BoxCusto.BackColor = System.Drawing.Color.Transparent;
            this.BoxCusto.Controls.Add(this.LstCusto);
            this.BoxCusto.Controls.Add(this.label1);
            this.BoxCusto.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxCusto.Location = new System.Drawing.Point(0, 152);
            this.BoxCusto.Name = "BoxCusto";
            this.BoxCusto.Size = new System.Drawing.Size(377, 36);
            this.BoxCusto.TabIndex = 73;
            this.BoxCusto.TabStop = false;
            // 
            // LstCusto
            // 
            this.LstCusto.FormattingEnabled = true;
            this.LstCusto.Location = new System.Drawing.Point(105, 11);
            this.LstCusto.Name = "LstCusto";
            this.LstCusto.Size = new System.Drawing.Size(266, 21);
            this.LstCusto.TabIndex = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 149;
            this.label1.Text = "Centro de Custo:";
            // 
            // BoxDepart
            // 
            this.BoxDepart.BackColor = System.Drawing.Color.Transparent;
            this.BoxDepart.Controls.Add(this.LstDepartamento);
            this.BoxDepart.Controls.Add(this.label2);
            this.BoxDepart.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxDepart.Location = new System.Drawing.Point(0, 116);
            this.BoxDepart.Name = "BoxDepart";
            this.BoxDepart.Size = new System.Drawing.Size(377, 36);
            this.BoxDepart.TabIndex = 74;
            this.BoxDepart.TabStop = false;
            // 
            // LstDepartamento
            // 
            this.LstDepartamento.FormattingEnabled = true;
            this.LstDepartamento.Location = new System.Drawing.Point(96, 11);
            this.LstDepartamento.Name = "LstDepartamento";
            this.LstDepartamento.Size = new System.Drawing.Size(275, 21);
            this.LstDepartamento.TabIndex = 150;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 149;
            this.label2.Text = "Departamento:";
            // 
            // BoxFilial
            // 
            this.BoxFilial.BackColor = System.Drawing.Color.Transparent;
            this.BoxFilial.Controls.Add(this.LstFilial);
            this.BoxFilial.Controls.Add(this.label3);
            this.BoxFilial.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxFilial.Location = new System.Drawing.Point(0, 80);
            this.BoxFilial.Name = "BoxFilial";
            this.BoxFilial.Size = new System.Drawing.Size(377, 36);
            this.BoxFilial.TabIndex = 75;
            this.BoxFilial.TabStop = false;
            // 
            // LstFilial
            // 
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Location = new System.Drawing.Point(45, 11);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(326, 21);
            this.LstFilial.TabIndex = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 149;
            this.label3.Text = "Filial:";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 101);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(303, 11);
            this.panel2.TabIndex = 117;
            // 
            // Op06
            // 
            this.Op06.AutoSize = true;
            this.Op06.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op06.Location = new System.Drawing.Point(3, 112);
            this.Op06.Name = "Op06";
            this.Op06.Size = new System.Drawing.Size(303, 17);
            this.Op06.TabIndex = 118;
            this.Op06.TabStop = true;
            this.Op06.Text = "Listagem Agendamento de Cobrança";
            this.Op06.UseVisualStyleBackColor = true;
            this.Op06.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op07
            // 
            this.Op07.AutoSize = true;
            this.Op07.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op07.Location = new System.Drawing.Point(3, 129);
            this.Op07.Name = "Op07";
            this.Op07.Size = new System.Drawing.Size(303, 17);
            this.Op07.TabIndex = 119;
            this.Op07.TabStop = true;
            this.Op07.Text = "Registro de Cobranças Realizadas";
            this.Op07.UseVisualStyleBackColor = true;
            this.Op07.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // FrmRelFinanceiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(731, 359);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnImprimir);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelFinanceiro";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatórios Financeiro";
            this.Load += new System.EventHandler(this.FrmRelFinanceiro_Load);
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.BoxCaixa.ResumeLayout(false);
            this.BoxCaixa.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.BoxCusto.ResumeLayout(false);
            this.BoxCusto.PerformLayout();
            this.BoxDepart.ResumeLayout(false);
            this.BoxDepart.PerformLayout();
            this.BoxFilial.ResumeLayout(false);
            this.BoxFilial.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker Dt2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker Dt1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Op01;
        private System.Windows.Forms.RadioButton Op02;
        private System.Windows.Forms.RadioButton Op03;
        private System.Windows.Forms.GroupBox BoxCaixa;
        private System.Windows.Forms.ComboBox LstCaixa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox BoxCusto;
        private System.Windows.Forms.ComboBox LstCusto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox BoxDepart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox BoxFilial;
        private System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox LstDepartamento;
        private System.Windows.Forms.RadioButton Op04;
        private System.Windows.Forms.RadioButton Op05;
        private System.Windows.Forms.RadioButton Op06;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton Op07;
    }
}