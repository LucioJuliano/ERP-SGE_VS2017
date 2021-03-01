namespace ERP_SGE
{
    partial class FrmRelFaturamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelFaturamento));
            System.Windows.Forms.Button BtnBuscaPessoa;
            this.Op02 = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.BoxStatus = new System.Windows.Forms.GroupBox();
            this.Rb_Todos = new System.Windows.Forms.RadioButton();
            this.Rb_Cancelada = new System.Windows.Forms.RadioButton();
            this.Rb_Emitida = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.Dt2 = new System.Windows.Forms.DateTimePicker();
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.Op03 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Op04 = new System.Windows.Forms.RadioButton();
            this.Op01 = new System.Windows.Forms.RadioButton();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.BoxTipoNota = new System.Windows.Forms.GroupBox();
            this.Rb_NFE = new System.Windows.Forms.RadioButton();
            this.Rb_Form = new System.Windows.Forms.RadioButton();
            this.PnlItens = new System.Windows.Forms.Panel();
            this.BoxFilial = new System.Windows.Forms.GroupBox();
            this.PnlPessoa = new System.Windows.Forms.Panel();
            this.TxtCodCliente = new System.Windows.Forms.TextBox();
            this.TxtCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            BtnBuscaPessoa = new System.Windows.Forms.Button();
            this.BoxStatus.SuspendLayout();
            this.BoxItemPesq.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.BoxTipoNota.SuspendLayout();
            this.PnlItens.SuspendLayout();
            this.BoxFilial.SuspendLayout();
            this.PnlPessoa.SuspendLayout();
            this.SuspendLayout();
            // 
            // Op02
            // 
            this.Op02.AutoSize = true;
            this.Op02.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op02.Location = new System.Drawing.Point(3, 33);
            this.Op02.Name = "Op02";
            this.Op02.Size = new System.Drawing.Size(241, 17);
            this.Op02.TabIndex = 1;
            this.Op02.TabStop = true;
            this.Op02.Text = "Listagem dos Cupons Fiscais";
            this.Op02.UseVisualStyleBackColor = true;
            this.Op02.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(190, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "a";
            // 
            // BoxStatus
            // 
            this.BoxStatus.BackColor = System.Drawing.Color.Transparent;
            this.BoxStatus.Controls.Add(this.Rb_Todos);
            this.BoxStatus.Controls.Add(this.Rb_Cancelada);
            this.BoxStatus.Controls.Add(this.Rb_Emitida);
            this.BoxStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxStatus.Location = new System.Drawing.Point(0, 90);
            this.BoxStatus.Name = "BoxStatus";
            this.BoxStatus.Size = new System.Drawing.Size(371, 30);
            this.BoxStatus.TabIndex = 75;
            this.BoxStatus.TabStop = false;
            // 
            // Rb_Todos
            // 
            this.Rb_Todos.AutoSize = true;
            this.Rb_Todos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_Todos.Location = new System.Drawing.Point(6, 11);
            this.Rb_Todos.Name = "Rb_Todos";
            this.Rb_Todos.Size = new System.Drawing.Size(60, 17);
            this.Rb_Todos.TabIndex = 1;
            this.Rb_Todos.TabStop = true;
            this.Rb_Todos.Text = "Todos";
            this.Rb_Todos.UseVisualStyleBackColor = true;
            // 
            // Rb_Cancelada
            // 
            this.Rb_Cancelada.AutoSize = true;
            this.Rb_Cancelada.Location = new System.Drawing.Point(161, 11);
            this.Rb_Cancelada.Name = "Rb_Cancelada";
            this.Rb_Cancelada.Size = new System.Drawing.Size(91, 17);
            this.Rb_Cancelada.TabIndex = 3;
            this.Rb_Cancelada.TabStop = true;
            this.Rb_Cancelada.Text = "Canceladas";
            this.Rb_Cancelada.UseVisualStyleBackColor = true;
            // 
            // Rb_Emitida
            // 
            this.Rb_Emitida.AutoSize = true;
            this.Rb_Emitida.Location = new System.Drawing.Point(72, 11);
            this.Rb_Emitida.Name = "Rb_Emitida";
            this.Rb_Emitida.Size = new System.Drawing.Size(72, 17);
            this.Rb_Emitida.TabIndex = 2;
            this.Rb_Emitida.TabStop = true;
            this.Rb_Emitida.Text = "Emitidas";
            this.Rb_Emitida.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Periodo de:";
            // 
            // Dt2
            // 
            this.Dt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt2.Location = new System.Drawing.Point(208, 15);
            this.Dt2.Name = "Dt2";
            this.Dt2.Size = new System.Drawing.Size(100, 20);
            this.Dt2.TabIndex = 1;
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
            this.BoxItemPesq.Size = new System.Drawing.Size(371, 44);
            this.BoxItemPesq.TabIndex = 70;
            this.BoxItemPesq.TabStop = false;
            // 
            // Dt1
            // 
            this.Dt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt1.Location = new System.Drawing.Point(80, 16);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(100, 20);
            this.Dt1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 72;
            this.label5.Text = "Filial:";
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Location = new System.Drawing.Point(49, 19);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(312, 21);
            this.LstFilial.TabIndex = 2;
            // 
            // Op03
            // 
            this.Op03.AutoSize = true;
            this.Op03.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op03.Location = new System.Drawing.Point(3, 50);
            this.Op03.Name = "Op03";
            this.Op03.Size = new System.Drawing.Size(241, 17);
            this.Op03.TabIndex = 2;
            this.Op03.TabStop = true;
            this.Op03.Text = "Notas Fiscais por Cliente";
            this.Op03.UseVisualStyleBackColor = true;
            this.Op03.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Op04);
            this.groupBox1.Controls.Add(this.Op03);
            this.groupBox1.Controls.Add(this.Op02);
            this.groupBox1.Controls.Add(this.Op01);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 262);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selecione um Relatório";
            // 
            // Op04
            // 
            this.Op04.AutoSize = true;
            this.Op04.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op04.Location = new System.Drawing.Point(3, 67);
            this.Op04.Name = "Op04";
            this.Op04.Size = new System.Drawing.Size(241, 17);
            this.Op04.TabIndex = 3;
            this.Op04.TabStop = true;
            this.Op04.Text = "Vendas com Pendências de CF ou NF";
            this.Op04.UseVisualStyleBackColor = true;
            this.Op04.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // Op01
            // 
            this.Op01.AutoSize = true;
            this.Op01.Dock = System.Windows.Forms.DockStyle.Top;
            this.Op01.Location = new System.Drawing.Point(3, 16);
            this.Op01.Name = "Op01";
            this.Op01.Size = new System.Drawing.Size(241, 17);
            this.Op01.TabIndex = 0;
            this.Op01.TabStop = true;
            this.Op01.Text = "Relação de Notas Fiscais";
            this.Op01.UseVisualStyleBackColor = true;
            this.Op01.Click += new System.EventHandler(this.AtualizarTela);
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("BtnImprimir.Image")));
            this.BtnImprimir.Location = new System.Drawing.Point(352, 220);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(128, 30);
            this.BtnImprimir.TabIndex = 72;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
            // 
            // BoxTipoNota
            // 
            this.BoxTipoNota.BackColor = System.Drawing.Color.Transparent;
            this.BoxTipoNota.Controls.Add(this.Rb_NFE);
            this.BoxTipoNota.Controls.Add(this.Rb_Form);
            this.BoxTipoNota.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxTipoNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxTipoNota.Location = new System.Drawing.Point(0, 120);
            this.BoxTipoNota.Name = "BoxTipoNota";
            this.BoxTipoNota.Size = new System.Drawing.Size(371, 30);
            this.BoxTipoNota.TabIndex = 76;
            this.BoxTipoNota.TabStop = false;
            this.BoxTipoNota.Text = "Tipo de Nota";
            // 
            // Rb_NFE
            // 
            this.Rb_NFE.AutoSize = true;
            this.Rb_NFE.Location = new System.Drawing.Point(161, 11);
            this.Rb_NFE.Name = "Rb_NFE";
            this.Rb_NFE.Size = new System.Drawing.Size(113, 17);
            this.Rb_NFE.TabIndex = 3;
            this.Rb_NFE.TabStop = true;
            this.Rb_NFE.Text = "Nota Eletrônica";
            this.Rb_NFE.UseVisualStyleBackColor = true;
            // 
            // Rb_Form
            // 
            this.Rb_Form.AutoSize = true;
            this.Rb_Form.Location = new System.Drawing.Point(6, 11);
            this.Rb_Form.Name = "Rb_Form";
            this.Rb_Form.Size = new System.Drawing.Size(139, 17);
            this.Rb_Form.TabIndex = 2;
            this.Rb_Form.TabStop = true;
            this.Rb_Form.Text = "Formulário Contínuo";
            this.Rb_Form.UseVisualStyleBackColor = true;
            // 
            // PnlItens
            // 
            this.PnlItens.BackColor = System.Drawing.Color.Transparent;
            this.PnlItens.Controls.Add(this.PnlPessoa);
            this.PnlItens.Controls.Add(this.BoxTipoNota);
            this.PnlItens.Controls.Add(this.BoxStatus);
            this.PnlItens.Controls.Add(this.BoxFilial);
            this.PnlItens.Controls.Add(this.BoxItemPesq);
            this.PnlItens.Location = new System.Drawing.Point(253, 0);
            this.PnlItens.Name = "PnlItens";
            this.PnlItens.Size = new System.Drawing.Size(371, 197);
            this.PnlItens.TabIndex = 77;
            // 
            // BoxFilial
            // 
            this.BoxFilial.BackColor = System.Drawing.Color.Transparent;
            this.BoxFilial.Controls.Add(this.LstFilial);
            this.BoxFilial.Controls.Add(this.label5);
            this.BoxFilial.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxFilial.Location = new System.Drawing.Point(0, 44);
            this.BoxFilial.Name = "BoxFilial";
            this.BoxFilial.Size = new System.Drawing.Size(371, 46);
            this.BoxFilial.TabIndex = 77;
            this.BoxFilial.TabStop = false;
            // 
            // PnlPessoa
            // 
            this.PnlPessoa.BackColor = System.Drawing.Color.Transparent;
            this.PnlPessoa.Controls.Add(this.TxtCodCliente);
            this.PnlPessoa.Controls.Add(this.TxtCliente);
            this.PnlPessoa.Controls.Add(BtnBuscaPessoa);
            this.PnlPessoa.Controls.Add(this.label4);
            this.PnlPessoa.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlPessoa.Location = new System.Drawing.Point(0, 150);
            this.PnlPessoa.Name = "PnlPessoa";
            this.PnlPessoa.Size = new System.Drawing.Size(371, 31);
            this.PnlPessoa.TabIndex = 81;
            // 
            // TxtCodCliente
            // 
            this.TxtCodCliente.Enabled = false;
            this.TxtCodCliente.Location = new System.Drawing.Point(74, 7);
            this.TxtCodCliente.MaxLength = 40;
            this.TxtCodCliente.Name = "TxtCodCliente";
            this.TxtCodCliente.Size = new System.Drawing.Size(48, 20);
            this.TxtCodCliente.TabIndex = 135;
            this.TxtCodCliente.Text = "0";
            // 
            // TxtCliente
            // 
            this.TxtCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCliente.Enabled = false;
            this.TxtCliente.Location = new System.Drawing.Point(124, 7);
            this.TxtCliente.MaxLength = 40;
            this.TxtCliente.Name = "TxtCliente";
            this.TxtCliente.Size = new System.Drawing.Size(236, 20);
            this.TxtCliente.TabIndex = 133;
            // 
            // BtnBuscaPessoa
            // 
            BtnBuscaPessoa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBuscaPessoa.BackgroundImage")));
            BtnBuscaPessoa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            BtnBuscaPessoa.Location = new System.Drawing.Point(54, 7);
            BtnBuscaPessoa.Name = "BtnBuscaPessoa";
            BtnBuscaPessoa.Size = new System.Drawing.Size(20, 20);
            BtnBuscaPessoa.TabIndex = 132;
            BtnBuscaPessoa.UseVisualStyleBackColor = true;
            BtnBuscaPessoa.Click += new System.EventHandler(this.BtnBuscaPessoa_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(6, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 134;
            this.label4.Text = "Cliente:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmRelFaturamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(646, 262);
            this.Controls.Add(this.PnlItens);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnImprimir);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelFaturamento";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatórios  Faturamento";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.BoxStatus.ResumeLayout(false);
            this.BoxStatus.PerformLayout();
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.BoxTipoNota.ResumeLayout(false);
            this.BoxTipoNota.PerformLayout();
            this.PnlItens.ResumeLayout(false);
            this.BoxFilial.ResumeLayout(false);
            this.BoxFilial.PerformLayout();
            this.PnlPessoa.ResumeLayout(false);
            this.PnlPessoa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton Op02;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.GroupBox BoxStatus;
        private System.Windows.Forms.RadioButton Rb_Todos;
        private System.Windows.Forms.RadioButton Rb_Cancelada;
        private System.Windows.Forms.RadioButton Rb_Emitida;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker Dt2;
        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker Dt1;
        private System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.RadioButton Op03;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Op01;
        private System.Windows.Forms.Button BtnImprimir;
        public System.Windows.Forms.GroupBox BoxTipoNota;
        private System.Windows.Forms.RadioButton Rb_NFE;
        private System.Windows.Forms.RadioButton Rb_Form;
        private System.Windows.Forms.Panel PnlItens;
        public System.Windows.Forms.GroupBox BoxFilial;
        private System.Windows.Forms.RadioButton Op04;
        private System.Windows.Forms.Panel PnlPessoa;
        private System.Windows.Forms.TextBox TxtCodCliente;
        private System.Windows.Forms.TextBox TxtCliente;
        private System.Windows.Forms.Label label4;
    }
}