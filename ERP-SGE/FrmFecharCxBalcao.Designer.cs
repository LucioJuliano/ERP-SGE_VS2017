namespace ERP_SGE
{
    partial class FrmFecharCxBalcao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFecharCxBalcao));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblCaixa = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LblSado = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GridCx = new System.Windows.Forms.DataGridView();
            this.LblSaldoFin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblDifFin = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnConcluir = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColVlrRec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColVrlDesp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQtde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColResCx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCx)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.LblCaixa);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 36);
            this.panel1.TabIndex = 0;
            // 
            // LblCaixa
            // 
            this.LblCaixa.AutoSize = true;
            this.LblCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCaixa.ForeColor = System.Drawing.Color.Maroon;
            this.LblCaixa.Location = new System.Drawing.Point(3, 9);
            this.LblCaixa.Name = "LblCaixa";
            this.LblCaixa.Size = new System.Drawing.Size(52, 17);
            this.LblCaixa.TabIndex = 0;
            this.LblCaixa.Text = "Caixa:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.BtnConcluir);
            this.panel2.Controls.Add(this.LblDifFin);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.LblSaldoFin);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.LblSado);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 270);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(351, 120);
            this.panel2.TabIndex = 1;
            // 
            // LblSado
            // 
            this.LblSado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSado.ForeColor = System.Drawing.Color.Maroon;
            this.LblSado.Location = new System.Drawing.Point(222, 13);
            this.LblSado.Name = "LblSado";
            this.LblSado.Size = new System.Drawing.Size(111, 17);
            this.LblSado.TabIndex = 4;
            this.LblSado.Text = "0,00";
            this.LblSado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(90, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Saldo do Dia R$";
            // 
            // GridCx
            // 
            this.GridCx.AllowUserToAddRows = false;
            this.GridCx.AllowUserToDeleteRows = false;
            this.GridCx.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridCx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridCx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.ColCalc,
            this.ColVlrRec,
            this.ColVrlDesp,
            this.ColQtde,
            this.ColDif,
            this.ColResCx});
            this.GridCx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridCx.Location = new System.Drawing.Point(0, 36);
            this.GridCx.MultiSelect = false;
            this.GridCx.Name = "GridCx";
            this.GridCx.RowHeadersWidth = 15;
            this.GridCx.Size = new System.Drawing.Size(351, 234);
            this.GridCx.TabIndex = 7;
            this.GridCx.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCx_CellEndEdit);
            // 
            // LblSaldoFin
            // 
            this.LblSaldoFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaldoFin.ForeColor = System.Drawing.Color.Maroon;
            this.LblSaldoFin.Location = new System.Drawing.Point(222, 47);
            this.LblSaldoFin.Name = "LblSaldoFin";
            this.LblSaldoFin.Size = new System.Drawing.Size(111, 17);
            this.LblSaldoFin.TabIndex = 6;
            this.LblSaldoFin.Text = "0,00";
            this.LblSaldoFin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(61, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Saldo Financeiro R$";
            // 
            // LblDifFin
            // 
            this.LblDifFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDifFin.ForeColor = System.Drawing.Color.Maroon;
            this.LblDifFin.Location = new System.Drawing.Point(222, 74);
            this.LblDifFin.Name = "LblDifFin";
            this.LblDifFin.Size = new System.Drawing.Size(111, 17);
            this.LblDifFin.TabIndex = 8;
            this.LblDifFin.Text = "0,00";
            this.LblDifFin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(113, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Diferença R$";
            // 
            // BtnConcluir
            // 
            this.BtnConcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConcluir.Image = ((System.Drawing.Image)(resources.GetObject("BtnConcluir.Image")));
            this.BtnConcluir.Location = new System.Drawing.Point(6, 74);
            this.BtnConcluir.Name = "BtnConcluir";
            this.BtnConcluir.Size = new System.Drawing.Size(101, 26);
            this.BtnConcluir.TabIndex = 65;
            this.BtnConcluir.Text = "Confirmar";
            this.BtnConcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConcluir.UseVisualStyleBackColor = true;
            this.BtnConcluir.Click += new System.EventHandler(this.BtnConcluir_Click);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Id_Documento";
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = "000";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "IdDoc";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Documento";
            this.dataGridViewTextBoxColumn3.HeaderText = "Documento";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // ColCalc
            // 
            this.ColCalc.DataPropertyName = "VlrCalculado";
            this.ColCalc.HeaderText = "VlrCalculado";
            this.ColCalc.Name = "ColCalc";
            this.ColCalc.ReadOnly = true;
            this.ColCalc.Visible = false;
            this.ColCalc.Width = 91;
            // 
            // ColVlrRec
            // 
            this.ColVlrRec.DataPropertyName = "VlrReceita";
            this.ColVlrRec.HeaderText = "VlrReceita";
            this.ColVlrRec.Name = "ColVlrRec";
            this.ColVlrRec.Visible = false;
            this.ColVlrRec.Width = 81;
            // 
            // ColVrlDesp
            // 
            this.ColVrlDesp.DataPropertyName = "VlrDespesa";
            this.ColVrlDesp.HeaderText = "VlrDespesa";
            this.ColVrlDesp.Name = "ColVrlDesp";
            this.ColVrlDesp.Visible = false;
            this.ColVrlDesp.Width = 86;
            // 
            // ColQtde
            // 
            this.ColQtde.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColQtde.DataPropertyName = "VlrInformado";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0,00";
            this.ColQtde.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColQtde.HeaderText = "Vlr.Informado";
            this.ColQtde.Name = "ColQtde";
            this.ColQtde.Width = 90;
            // 
            // ColDif
            // 
            this.ColDif.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColDif.DataPropertyName = "VlrDif";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0,00";
            this.ColDif.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColDif.HeaderText = "Diferença";
            this.ColDif.Name = "ColDif";
            this.ColDif.ReadOnly = true;
            this.ColDif.Visible = false;
            this.ColDif.Width = 90;
            // 
            // ColResCx
            // 
            this.ColResCx.DataPropertyName = "ResumoCx";
            this.ColResCx.HeaderText = "ResumoCx";
            this.ColResCx.Name = "ColResCx";
            this.ColResCx.Visible = false;
            this.ColResCx.Width = 83;
            // 
            // FrmFecharCxBalcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(351, 390);
            this.Controls.Add(this.GridCx);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFecharCxBalcao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fechamento do Caixa";
            this.Load += new System.EventHandler(this.FrmFecharCxBalcao_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LblCaixa;
        private System.Windows.Forms.DataGridView GridCx;
        private System.Windows.Forms.Label LblSado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblSaldoFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblDifFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnConcluir;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVlrRec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVrlDesp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQtde;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDif;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColResCx;
    }
}