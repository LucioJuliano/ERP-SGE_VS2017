namespace ERP_SGE
{
    partial class FrmMvCaixaBalcao
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMvCaixaBalcao));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PnlDados = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtValor = new System.Windows.Forms.NumericUpDown();
            this.LstTipoDoc = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.TxtDescricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Rb_Receita = new System.Windows.Forms.RadioButton();
            this.Rb_Despesa = new System.Windows.Forms.RadioButton();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.BtnNovo = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TpMov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtValor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlDados
            // 
            this.PnlDados.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.PnlDados.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlDados.Controls.Add(this.label2);
            this.PnlDados.Controls.Add(this.TxtValor);
            this.PnlDados.Controls.Add(this.LstTipoDoc);
            this.PnlDados.Controls.Add(this.label21);
            this.PnlDados.Controls.Add(this.TxtDescricao);
            this.PnlDados.Controls.Add(this.label1);
            this.PnlDados.Controls.Add(this.groupBox1);
            this.PnlDados.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlDados.Enabled = false;
            this.PnlDados.Location = new System.Drawing.Point(0, 0);
            this.PnlDados.Name = "PnlDados";
            this.PnlDados.Size = new System.Drawing.Size(612, 67);
            this.PnlDados.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(430, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Valor R$:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtValor
            // 
            this.TxtValor.DecimalPlaces = 2;
            this.TxtValor.Location = new System.Drawing.Point(491, 37);
            this.TxtValor.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtValor.Name = "TxtValor";
            this.TxtValor.Size = new System.Drawing.Size(100, 20);
            this.TxtValor.TabIndex = 49;
            this.TxtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtValor.ThousandsSeparator = true;
            // 
            // LstTipoDoc
            // 
            this.LstTipoDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstTipoDoc.FormattingEnabled = true;
            this.LstTipoDoc.Location = new System.Drawing.Point(195, 36);
            this.LstTipoDoc.Name = "LstTipoDoc";
            this.LstTipoDoc.Size = new System.Drawing.Size(229, 21);
            this.LstTipoDoc.TabIndex = 46;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(124, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(75, 13);
            this.label21.TabIndex = 47;
            this.label21.Text = "Documento:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtDescricao
            // 
            this.TxtDescricao.Location = new System.Drawing.Point(195, 10);
            this.TxtDescricao.MaxLength = 100;
            this.TxtDescricao.Name = "TxtDescricao";
            this.TxtDescricao.Size = new System.Drawing.Size(396, 20);
            this.TxtDescricao.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(132, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Referente:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Rb_Receita);
            this.groupBox1.Controls.Add(this.Rb_Despesa);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo do Lançamento";
            // 
            // Rb_Receita
            // 
            this.Rb_Receita.AutoSize = true;
            this.Rb_Receita.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_Receita.Location = new System.Drawing.Point(6, 33);
            this.Rb_Receita.Name = "Rb_Receita";
            this.Rb_Receita.Size = new System.Drawing.Size(69, 17);
            this.Rb_Receita.TabIndex = 1;
            this.Rb_Receita.TabStop = true;
            this.Rb_Receita.Text = "Receita";
            this.Rb_Receita.UseVisualStyleBackColor = true;
            // 
            // Rb_Despesa
            // 
            this.Rb_Despesa.AutoSize = true;
            this.Rb_Despesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_Despesa.Location = new System.Drawing.Point(6, 14);
            this.Rb_Despesa.Name = "Rb_Despesa";
            this.Rb_Despesa.Size = new System.Drawing.Size(74, 17);
            this.Rb_Despesa.TabIndex = 0;
            this.Rb_Despesa.TabStop = true;
            this.Rb_Despesa.Text = "Despesa";
            this.Rb_Despesa.UseVisualStyleBackColor = true;
            // 
            // GridDados
            // 
            this.GridDados.AllowUserToAddRows = false;
            this.GridDados.AllowUserToDeleteRows = false;
            this.GridDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.GridDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Status,
            this.Column4,
            this.TpMov,
            this.NumDoc});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle22;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GridDados.Location = new System.Drawing.Point(0, 104);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            this.GridDados.ReadOnly = true;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            this.GridDados.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.GridDados.Size = new System.Drawing.Size(612, 233);
            this.GridDados.TabIndex = 5;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancelar.Image")));
            this.BtnCancelar.Location = new System.Drawing.Point(481, 73);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(110, 25);
            this.BtnCancelar.TabIndex = 124;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(365, 73);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(110, 25);
            this.BtnConfirmar.TabIndex = 123;
            this.BtnConfirmar.Text = "Confirmar";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // BtnNovo
            // 
            this.BtnNovo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNovo.Location = new System.Drawing.Point(3, 73);
            this.BtnNovo.Name = "BtnNovo";
            this.BtnNovo.Size = new System.Drawing.Size(110, 25);
            this.BtnNovo.TabIndex = 125;
            this.BtnNovo.Text = "Novo Lanç.";
            this.BtnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnNovo.UseVisualStyleBackColor = true;
            this.BtnNovo.Click += new System.EventHandler(this.BtnNovo_Click);
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Id.DataPropertyName = "Id_Lanc";
            dataGridViewCellStyle20.Format = "N0";
            dataGridViewCellStyle20.NullValue = "000";
            this.Id.DefaultCellStyle = dataGridViewCellStyle20;
            this.Id.HeaderText = "NºLanç.";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 60;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 80;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.DataPropertyName = "TipoLanc";
            this.Column4.HeaderText = "Tipo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 60;
            // 
            // TpMov
            // 
            this.TpMov.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TpMov.DataPropertyName = "Descricao";
            this.TpMov.HeaderText = "Referente";
            this.TpMov.Name = "TpMov";
            this.TpMov.ReadOnly = true;
            this.TpMov.Width = 200;
            // 
            // NumDoc
            // 
            this.NumDoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NumDoc.DataPropertyName = "Valor";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle21.Format = "N2";
            dataGridViewCellStyle21.NullValue = "0,00";
            this.NumDoc.DefaultCellStyle = dataGridViewCellStyle21;
            this.NumDoc.HeaderText = "Valor";
            this.NumDoc.Name = "NumDoc";
            this.NumDoc.ReadOnly = true;
            // 
            // FrmMvCaixaBalcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(612, 337);
            this.Controls.Add(this.BtnNovo);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.PnlDados);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMvCaixaBalcao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimentação do Caixa Balcão";
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.FrmMvCaixaBalcao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.PnlDados.ResumeLayout(false);
            this.PnlDados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtValor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlDados;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Rb_Receita;
        private System.Windows.Forms.RadioButton Rb_Despesa;
        private System.Windows.Forms.TextBox TxtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LstTipoDoc;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown TxtValor;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.Button BtnNovo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn TpMov;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumDoc;
    }
}