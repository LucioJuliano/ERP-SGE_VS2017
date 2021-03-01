namespace ERP_SGE
{
    partial class FrmFechaMovimento
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFechaMovimento));
            this.PnlTopo = new System.Windows.Forms.Panel();
            this.LstAgente = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtNumParc = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtPessoa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BoxTotal = new System.Windows.Forms.GroupBox();
            this.TxtDtBase = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtVlrTotal = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.TxtVlrDesconto = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtVlrSubTotal = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.LstFormaPgto = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.Parc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNDias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Venc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VlrParc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LstTpDoc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PnlFim = new System.Windows.Forms.Panel();
            this.BtnCancMov = new System.Windows.Forms.Button();
            this.BtnConcluir = new System.Windows.Forms.Button();
            this.PnlTopo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNumParc)).BeginInit();
            this.BoxTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrDesconto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrSubTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.PnlFim.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlTopo
            // 
            this.PnlTopo.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.PnlTopo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlTopo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlTopo.Controls.Add(this.LstAgente);
            this.PnlTopo.Controls.Add(this.label4);
            this.PnlTopo.Controls.Add(this.TxtNumParc);
            this.PnlTopo.Controls.Add(this.label3);
            this.PnlTopo.Controls.Add(this.TxtPessoa);
            this.PnlTopo.Controls.Add(this.label2);
            this.PnlTopo.Controls.Add(this.BoxTotal);
            this.PnlTopo.Controls.Add(this.LstFormaPgto);
            this.PnlTopo.Controls.Add(this.label1);
            this.PnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTopo.Location = new System.Drawing.Point(0, 0);
            this.PnlTopo.Name = "PnlTopo";
            this.PnlTopo.Size = new System.Drawing.Size(470, 145);
            this.PnlTopo.TabIndex = 0;
            // 
            // LstAgente
            // 
            this.LstAgente.FormattingEnabled = true;
            this.LstAgente.Location = new System.Drawing.Point(133, 83);
            this.LstAgente.Name = "LstAgente";
            this.LstAgente.Size = new System.Drawing.Size(291, 21);
            this.LstAgente.TabIndex = 127;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Agente Cobrador:";
            // 
            // TxtNumParc
            // 
            this.TxtNumParc.Location = new System.Drawing.Point(384, 110);
            this.TxtNumParc.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.TxtNumParc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TxtNumParc.Name = "TxtNumParc";
            this.TxtNumParc.Size = new System.Drawing.Size(40, 20);
            this.TxtNumParc.TabIndex = 125;
            this.TxtNumParc.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TxtNumParc.Validated += new System.EventHandler(this.TxtNumParc_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(304, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 124;
            this.label3.Text = "No. Parcela:";
            // 
            // TxtPessoa
            // 
            this.TxtPessoa.Enabled = false;
            this.TxtPessoa.Location = new System.Drawing.Point(52, 110);
            this.TxtPessoa.Name = "TxtPessoa";
            this.TxtPessoa.Size = new System.Drawing.Size(246, 20);
            this.TxtPessoa.TabIndex = 123;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 122;
            this.label2.Text = "Pessoa:";
            // 
            // BoxTotal
            // 
            this.BoxTotal.BackColor = System.Drawing.Color.Transparent;
            this.BoxTotal.Controls.Add(this.TxtDtBase);
            this.BoxTotal.Controls.Add(this.label8);
            this.BoxTotal.Controls.Add(this.TxtVlrTotal);
            this.BoxTotal.Controls.Add(this.label30);
            this.BoxTotal.Controls.Add(this.TxtVlrDesconto);
            this.BoxTotal.Controls.Add(this.label29);
            this.BoxTotal.Controls.Add(this.TxtVlrSubTotal);
            this.BoxTotal.Controls.Add(this.label28);
            this.BoxTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxTotal.Location = new System.Drawing.Point(0, 0);
            this.BoxTotal.Name = "BoxTotal";
            this.BoxTotal.Size = new System.Drawing.Size(466, 51);
            this.BoxTotal.TabIndex = 0;
            this.BoxTotal.TabStop = false;
            // 
            // TxtDtBase
            // 
            this.TxtDtBase.Enabled = false;
            this.TxtDtBase.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtDtBase.Location = new System.Drawing.Point(6, 25);
            this.TxtDtBase.Name = "TxtDtBase";
            this.TxtDtBase.Size = new System.Drawing.Size(96, 20);
            this.TxtDtBase.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 122;
            this.label8.Text = "Data Base:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtVlrTotal
            // 
            this.TxtVlrTotal.DecimalPlaces = 2;
            this.TxtVlrTotal.Enabled = false;
            this.TxtVlrTotal.Location = new System.Drawing.Point(304, 25);
            this.TxtVlrTotal.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrTotal.Name = "TxtVlrTotal";
            this.TxtVlrTotal.Size = new System.Drawing.Size(93, 20);
            this.TxtVlrTotal.TabIndex = 3;
            this.TxtVlrTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrTotal.ThousandsSeparator = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label30.Location = new System.Drawing.Point(314, 12);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(79, 13);
            this.label30.TabIndex = 53;
            this.label30.Text = "Vlr. Total R$";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtVlrDesconto
            // 
            this.TxtVlrDesconto.DecimalPlaces = 2;
            this.TxtVlrDesconto.Enabled = false;
            this.TxtVlrDesconto.Location = new System.Drawing.Point(205, 25);
            this.TxtVlrDesconto.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrDesconto.Name = "TxtVlrDesconto";
            this.TxtVlrDesconto.Size = new System.Drawing.Size(93, 20);
            this.TxtVlrDesconto.TabIndex = 2;
            this.TxtVlrDesconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrDesconto.ThousandsSeparator = true;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label29.Location = new System.Drawing.Point(215, 12);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(83, 13);
            this.label29.TabIndex = 51;
            this.label29.Text = "Vlr. Desc. R$";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtVlrSubTotal
            // 
            this.TxtVlrSubTotal.DecimalPlaces = 2;
            this.TxtVlrSubTotal.Enabled = false;
            this.TxtVlrSubTotal.Location = new System.Drawing.Point(110, 25);
            this.TxtVlrSubTotal.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrSubTotal.Name = "TxtVlrSubTotal";
            this.TxtVlrSubTotal.Size = new System.Drawing.Size(93, 20);
            this.TxtVlrSubTotal.TabIndex = 1;
            this.TxtVlrSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrSubTotal.ThousandsSeparator = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label28.Location = new System.Drawing.Point(110, 12);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(82, 13);
            this.label28.TabIndex = 49;
            this.label28.Text = "Sub-Total R$";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstFormaPgto
            // 
            this.LstFormaPgto.FormattingEnabled = true;
            this.LstFormaPgto.Location = new System.Drawing.Point(133, 57);
            this.LstFormaPgto.Name = "LstFormaPgto";
            this.LstFormaPgto.Size = new System.Drawing.Size(291, 21);
            this.LstFormaPgto.TabIndex = 1;
            this.LstFormaPgto.SelectedIndexChanged += new System.EventHandler(this.LstFormaPgto_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Forma de Pagamento:";
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
            this.Parc,
            this.ColNDias,
            this.Venc,
            this.VlrParc,
            this.LstTpDoc});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle5;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 145);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.GridDados.Size = new System.Drawing.Size(470, 127);
            this.GridDados.TabIndex = 2;
            this.GridDados.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridDados_CellBeginEdit);
            this.GridDados.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridDados_CellEndEdit);
            this.GridDados.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GridDadosDataError);
            // 
            // Parc
            // 
            this.Parc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Parc.DataPropertyName = "Parcela";
            this.Parc.HeaderText = "No.Parc.";
            this.Parc.Name = "Parc";
            this.Parc.ReadOnly = true;
            this.Parc.Width = 50;
            // 
            // ColNDias
            // 
            this.ColNDias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColNDias.DataPropertyName = "Dias";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.ColNDias.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColNDias.HeaderText = "Dias";
            this.ColNDias.Name = "ColNDias";
            this.ColNDias.Width = 35;
            // 
            // Venc
            // 
            this.Venc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Venc.DataPropertyName = "Vencimento";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.Venc.DefaultCellStyle = dataGridViewCellStyle3;
            this.Venc.HeaderText = "Vencimento";
            this.Venc.Name = "Venc";
            this.Venc.Width = 80;
            // 
            // VlrParc
            // 
            this.VlrParc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VlrParc.DataPropertyName = "Valor";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0,00";
            this.VlrParc.DefaultCellStyle = dataGridViewCellStyle4;
            this.VlrParc.HeaderText = "Valor Parc.";
            this.VlrParc.Name = "VlrParc";
            this.VlrParc.Width = 90;
            // 
            // LstTpDoc
            // 
            this.LstTpDoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LstTpDoc.DataPropertyName = "IdTpDoc";
            this.LstTpDoc.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.LstTpDoc.HeaderText = "Tipo de Documento";
            this.LstTpDoc.Name = "LstTpDoc";
            this.LstTpDoc.Width = 150;
            // 
            // PnlFim
            // 
            this.PnlFim.BackColor = System.Drawing.Color.Transparent;
            this.PnlFim.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.PnlFim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlFim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlFim.Controls.Add(this.BtnCancMov);
            this.PnlFim.Controls.Add(this.BtnConcluir);
            this.PnlFim.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlFim.Location = new System.Drawing.Point(0, 272);
            this.PnlFim.Name = "PnlFim";
            this.PnlFim.Size = new System.Drawing.Size(470, 34);
            this.PnlFim.TabIndex = 6;
            // 
            // BtnCancMov
            // 
            this.BtnCancMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancMov.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancMov.Image")));
            this.BtnCancMov.Location = new System.Drawing.Point(188, 3);
            this.BtnCancMov.Name = "BtnCancMov";
            this.BtnCancMov.Size = new System.Drawing.Size(97, 25);
            this.BtnCancMov.TabIndex = 4;
            this.BtnCancMov.Text = "Cancelar";
            this.BtnCancMov.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancMov.UseVisualStyleBackColor = true;
            this.BtnCancMov.Click += new System.EventHandler(this.BtnCancMov_Click);
            // 
            // BtnConcluir
            // 
            this.BtnConcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConcluir.Image = ((System.Drawing.Image)(resources.GetObject("BtnConcluir.Image")));
            this.BtnConcluir.Location = new System.Drawing.Point(328, 3);
            this.BtnConcluir.Name = "BtnConcluir";
            this.BtnConcluir.Size = new System.Drawing.Size(96, 25);
            this.BtnConcluir.TabIndex = 3;
            this.BtnConcluir.Text = "Confirma";
            this.BtnConcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConcluir.UseVisualStyleBackColor = true;
            this.BtnConcluir.Click += new System.EventHandler(this.BtnConcluir_Click);
            // 
            // FrmFechaMovimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 306);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.PnlFim);
            this.Controls.Add(this.PnlTopo);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFechaMovimento";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Concluir Movimento (Financeiro)";
            this.Load += new System.EventHandler(this.FrmFechaMovimento_Load);
            this.Shown += new System.EventHandler(this.FrmFechaMovimento_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.PnlTopo.ResumeLayout(false);
            this.PnlTopo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNumParc)).EndInit();
            this.BoxTotal.ResumeLayout(false);
            this.BoxTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrDesconto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrSubTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.PnlFim.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlTopo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.GroupBox BoxTotal;
        private System.Windows.Forms.DateTimePicker TxtDtBase;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel PnlFim;
        private System.Windows.Forms.Button BtnCancMov;
        private System.Windows.Forms.Button BtnConcluir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        public System.Windows.Forms.NumericUpDown TxtVlrTotal;
        public System.Windows.Forms.NumericUpDown TxtVlrDesconto;
        public System.Windows.Forms.NumericUpDown TxtVlrSubTotal;
        public System.Windows.Forms.TextBox TxtPessoa;
        public System.Windows.Forms.ComboBox LstFormaPgto;
        private System.Windows.Forms.NumericUpDown TxtNumParc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNDias;
        private System.Windows.Forms.DataGridViewTextBoxColumn Venc;
        private System.Windows.Forms.DataGridViewTextBoxColumn VlrParc;
        private System.Windows.Forms.DataGridViewComboBoxColumn LstTpDoc;
        public System.Windows.Forms.ComboBox LstAgente;
        private System.Windows.Forms.Label label4;
    }
}