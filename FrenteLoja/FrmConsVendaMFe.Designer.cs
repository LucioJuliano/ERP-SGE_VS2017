namespace FrenteLoja
{
    partial class FrmConsVendaMFe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsVendaMFe));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.LstPesquisa = new System.Windows.Forms.ComboBox();
            this.BtnCancCFe = new System.Windows.Forms.Button();
            this.BtnVerCartao = new System.Windows.Forms.Button();
            this.BtnDetalhes = new System.Windows.Forms.Button();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.IdUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColChave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNumNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDocPag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRespMFE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TpPag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NFE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STANFE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.ColStatus,
            this.Column1,
            this.Column2,
            this.ColChave,
            this.ColNumNota,
            this.IdNota,
            this.ColDocPag,
            this.IdRespMFE,
            this.TpPag,
            this.NFE,
            this.STANFE});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle4;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 49);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDados.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.GridDados.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.GridDados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridDados.Size = new System.Drawing.Size(1128, 362);
            this.GridDados.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BoxItemPesq);
            this.panel1.Controls.Add(this.BtnCancCFe);
            this.panel1.Controls.Add(this.BtnVerCartao);
            this.panel1.Controls.Add(this.BtnDetalhes);
            this.panel1.Controls.Add(this.BtnImprimir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1128, 49);
            this.panel1.TabIndex = 6;
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.Controls.Add(this.LstPesquisa);
            this.BoxItemPesq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxItemPesq.Location = new System.Drawing.Point(0, 3);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(153, 42);
            this.BoxItemPesq.TabIndex = 74;
            this.BoxItemPesq.TabStop = false;
            this.BoxItemPesq.Text = "Pesquisa";
            // 
            // LstPesquisa
            // 
            this.LstPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstPesquisa.FormattingEnabled = true;
            this.LstPesquisa.Items.AddRange(new object[] {
            "Todos",
            "Cartões Pendentes",
            "Concluidos"});
            this.LstPesquisa.Location = new System.Drawing.Point(6, 19);
            this.LstPesquisa.Name = "LstPesquisa";
            this.LstPesquisa.Size = new System.Drawing.Size(136, 21);
            this.LstPesquisa.TabIndex = 0;
            this.LstPesquisa.SelectedIndexChanged += new System.EventHandler(this.LstPesquisa_SelectedIndexChanged);
            // 
            // BtnCancCFe
            // 
            this.BtnCancCFe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancCFe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancCFe.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancCFe.Image")));
            this.BtnCancCFe.Location = new System.Drawing.Point(564, 10);
            this.BtnCancCFe.Name = "BtnCancCFe";
            this.BtnCancCFe.Size = new System.Drawing.Size(124, 31);
            this.BtnCancCFe.TabIndex = 73;
            this.BtnCancCFe.Text = "Cancelar CF-e ";
            this.BtnCancCFe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancCFe.UseVisualStyleBackColor = true;
            this.BtnCancCFe.Click += new System.EventHandler(this.BtnCancCFe_Click);
            // 
            // BtnVerCartao
            // 
            this.BtnVerCartao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnVerCartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVerCartao.Image = ((System.Drawing.Image)(resources.GetObject("BtnVerCartao.Image")));
            this.BtnVerCartao.Location = new System.Drawing.Point(373, 10);
            this.BtnVerCartao.Name = "BtnVerCartao";
            this.BtnVerCartao.Size = new System.Drawing.Size(168, 31);
            this.BtnVerCartao.TabIndex = 72;
            this.BtnVerCartao.Text = "Enviar Cartões Pendentes";
            this.BtnVerCartao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnVerCartao.UseVisualStyleBackColor = true;
            this.BtnVerCartao.Click += new System.EventHandler(this.BtnVerCartao_Click);
            // 
            // BtnDetalhes
            // 
            this.BtnDetalhes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDetalhes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDetalhes.Image = ((System.Drawing.Image)(resources.GetObject("BtnDetalhes.Image")));
            this.BtnDetalhes.Location = new System.Drawing.Point(199, 10);
            this.BtnDetalhes.Name = "BtnDetalhes";
            this.BtnDetalhes.Size = new System.Drawing.Size(138, 31);
            this.BtnDetalhes.TabIndex = 71;
            this.BtnDetalhes.Text = "Detalhes da Venda";
            this.BtnDetalhes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnDetalhes.UseVisualStyleBackColor = true;
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("BtnImprimir.Image")));
            this.BtnImprimir.Location = new System.Drawing.Point(735, 10);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(145, 31);
            this.BtnImprimir.TabIndex = 68;
            this.BtnImprimir.Text = "Reimprimir CF-e SAT";
            this.BtnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
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
            // ColStatus
            // 
            this.ColStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColStatus.DataPropertyName = "Status";
            this.ColStatus.HeaderText = "Status";
            this.ColStatus.Name = "ColStatus";
            this.ColStatus.ReadOnly = true;
            this.ColStatus.Width = 80;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "Pessoa";
            this.Column1.HeaderText = "Cliente";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 180;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.DataPropertyName = "VlrTotal";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0,00";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Vlr.Total R$";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 90;
            // 
            // ColChave
            // 
            this.ColChave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColChave.DataPropertyName = "ChaveNFe";
            this.ColChave.HeaderText = "CF-e SAT";
            this.ColChave.Name = "ColChave";
            this.ColChave.ReadOnly = true;
            this.ColChave.Width = 300;
            // 
            // ColNumNota
            // 
            this.ColNumNota.DataPropertyName = "NumNota";
            this.ColNumNota.HeaderText = "NumCFe";
            this.ColNumNota.Name = "ColNumNota";
            this.ColNumNota.Visible = false;
            this.ColNumNota.Width = 73;
            // 
            // IdNota
            // 
            this.IdNota.DataPropertyName = "Id_Nota";
            this.IdNota.HeaderText = "Id_Nota";
            this.IdNota.Name = "IdNota";
            this.IdNota.Visible = false;
            this.IdNota.Width = 70;
            // 
            // ColDocPag
            // 
            this.ColDocPag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColDocPag.DataPropertyName = "Documento";
            this.ColDocPag.HeaderText = "Tipo Pagamento";
            this.ColDocPag.Name = "ColDocPag";
            this.ColDocPag.Width = 150;
            // 
            // IdRespMFE
            // 
            this.IdRespMFE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdRespMFE.DataPropertyName = "IdRespMFE";
            this.IdRespMFE.HeaderText = "Resp.Fiscal";
            this.IdRespMFE.Name = "IdRespMFE";
            // 
            // TpPag
            // 
            this.TpPag.DataPropertyName = "TpPag";
            this.TpPag.HeaderText = "Tp.Pag";
            this.TpPag.Name = "TpPag";
            this.TpPag.Visible = false;
            this.TpPag.Width = 67;
            // 
            // NFE
            // 
            this.NFE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NFE.DataPropertyName = "NFE";
            this.NFE.HeaderText = "NFE";
            this.NFE.Name = "NFE";
            this.NFE.Visible = false;
            this.NFE.Width = 30;
            // 
            // STANFE
            // 
            this.STANFE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.STANFE.DataPropertyName = "STANFE";
            this.STANFE.HeaderText = "STANFE";
            this.STANFE.Name = "STANFE";
            this.STANFE.Visible = false;
            this.STANFE.Width = 30;
            // 
            // FrmConsVendaMFe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1128, 411);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConsVendaMFe";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta as Vendas CF-e SAT MFE";
            this.Load += new System.EventHandler(this.FrmConsVendaMFe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.panel1.ResumeLayout(false);
            this.BoxItemPesq.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnDetalhes;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.Button BtnVerCartao;
        public System.Windows.Forms.Button BtnCancCFe;
        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.ComboBox LstPesquisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChave;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNumNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDocPag;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRespMFE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TpPag;
        private System.Windows.Forms.DataGridViewTextBoxColumn NFE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STANFE;
    }
}