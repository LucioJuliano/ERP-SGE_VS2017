namespace ERP_SGE
{
    partial class FrmReplicarFinanc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReplicarFinanc));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.BtnCancMov = new System.Windows.Forms.Button();
            this.TxtNumParc = new System.Windows.Forms.NumericUpDown();
            this.PnlFim = new System.Windows.Forms.Panel();
            this.BtnConcluir = new System.Windows.Forms.Button();
            this.PnlTopo = new System.Windows.Forms.Panel();
            this.TxtDtBase = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Parc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Venc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VlrParc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNumDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNumParc)).BeginInit();
            this.PnlFim.SuspendLayout();
            this.PnlTopo.SuspendLayout();
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
            this.Parc,
            this.Venc,
            this.VlrParc,
            this.ColNumDoc});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle4;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 35);
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
            this.GridDados.Size = new System.Drawing.Size(426, 194);
            this.GridDados.TabIndex = 8;
            this.GridDados.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridDados_CellBeginEdit);
            this.GridDados.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GridDados_DataError);
            // 
            // BtnCancMov
            // 
            this.BtnCancMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancMov.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancMov.Image")));
            this.BtnCancMov.Location = new System.Drawing.Point(87, 2);
            this.BtnCancMov.Name = "BtnCancMov";
            this.BtnCancMov.Size = new System.Drawing.Size(97, 25);
            this.BtnCancMov.TabIndex = 4;
            this.BtnCancMov.Text = "Cancelar";
            this.BtnCancMov.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancMov.UseVisualStyleBackColor = true;
            this.BtnCancMov.Click += new System.EventHandler(this.BtnCancMov_Click);
            // 
            // TxtNumParc
            // 
            this.TxtNumParc.Location = new System.Drawing.Point(261, 3);
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
            // PnlFim
            // 
            this.PnlFim.BackColor = System.Drawing.Color.Transparent;
            this.PnlFim.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.PnlFim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlFim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlFim.Controls.Add(this.BtnCancMov);
            this.PnlFim.Controls.Add(this.BtnConcluir);
            this.PnlFim.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlFim.Location = new System.Drawing.Point(0, 229);
            this.PnlFim.Name = "PnlFim";
            this.PnlFim.Size = new System.Drawing.Size(426, 34);
            this.PnlFim.TabIndex = 9;
            // 
            // BtnConcluir
            // 
            this.BtnConcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConcluir.Image = ((System.Drawing.Image)(resources.GetObject("BtnConcluir.Image")));
            this.BtnConcluir.Location = new System.Drawing.Point(210, 2);
            this.BtnConcluir.Name = "BtnConcluir";
            this.BtnConcluir.Size = new System.Drawing.Size(96, 25);
            this.BtnConcluir.TabIndex = 3;
            this.BtnConcluir.Text = "Confirma";
            this.BtnConcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConcluir.UseVisualStyleBackColor = true;
            this.BtnConcluir.Click += new System.EventHandler(this.BtnConcluir_Click);
            // 
            // PnlTopo
            // 
            this.PnlTopo.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.PnlTopo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlTopo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlTopo.Controls.Add(this.TxtDtBase);
            this.PnlTopo.Controls.Add(this.label8);
            this.PnlTopo.Controls.Add(this.TxtNumParc);
            this.PnlTopo.Controls.Add(this.label3);
            this.PnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTopo.Location = new System.Drawing.Point(0, 0);
            this.PnlTopo.Name = "PnlTopo";
            this.PnlTopo.Size = new System.Drawing.Size(426, 35);
            this.PnlTopo.TabIndex = 7;
            // 
            // TxtDtBase
            // 
            this.TxtDtBase.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtDtBase.Location = new System.Drawing.Point(75, 3);
            this.TxtDtBase.Name = "TxtDtBase";
            this.TxtDtBase.Size = new System.Drawing.Size(96, 20);
            this.TxtDtBase.TabIndex = 126;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 127;
            this.label8.Text = "Data Base:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(177, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 124;
            this.label3.Text = "No. Parcela:";
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
            // Venc
            // 
            this.Venc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Venc.DataPropertyName = "Vencimento";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.Venc.DefaultCellStyle = dataGridViewCellStyle2;
            this.Venc.HeaderText = "Vencimento";
            this.Venc.Name = "Venc";
            this.Venc.Width = 80;
            // 
            // VlrParc
            // 
            this.VlrParc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VlrParc.DataPropertyName = "Valor";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0,00";
            this.VlrParc.DefaultCellStyle = dataGridViewCellStyle3;
            this.VlrParc.HeaderText = "Valor Parc.";
            this.VlrParc.Name = "VlrParc";
            this.VlrParc.Width = 90;
            // 
            // ColNumDoc
            // 
            this.ColNumDoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColNumDoc.DataPropertyName = "NumDocumento";
            this.ColNumDoc.HeaderText = "No.Documento";
            this.ColNumDoc.MaxInputLength = 25;
            this.ColNumDoc.Name = "ColNumDoc";
            this.ColNumDoc.Width = 150;
            // 
            // FrmReplicarFinanc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 263);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.PnlFim);
            this.Controls.Add(this.PnlTopo);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReplicarFinanc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replicar Lançamento";
            this.Load += new System.EventHandler(this.FrmReplicarFinanc_Load);
            this.Shown += new System.EventHandler(this.FrmReplicarFinanc_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtNumParc)).EndInit();
            this.PnlFim.ResumeLayout(false);
            this.PnlTopo.ResumeLayout(false);
            this.PnlTopo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.Button BtnCancMov;
        private System.Windows.Forms.NumericUpDown TxtNumParc;
        private System.Windows.Forms.Panel PnlFim;
        private System.Windows.Forms.Button BtnConcluir;
        private System.Windows.Forms.Panel PnlTopo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker TxtDtBase;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Venc;
        private System.Windows.Forms.DataGridViewTextBoxColumn VlrParc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNumDoc;

    }
}