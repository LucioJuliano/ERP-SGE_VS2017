namespace ERP_SGE
{
    partial class FrmLancItensProm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblTotal = new System.Windows.Forms.Label();
            this.LblItens = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.IdRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cnpj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRefPrd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQtde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSensacional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrcFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PEspecial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PVarejo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PMinimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PAtacado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.IdRota,
            this.Cnpj,
            this.ColRefPrd,
            this.ColQtde,
            this.PSensacional,
            this.ColPrcFinal,
            this.PEspecial,
            this.PVarejo,
            this.PMinimo,
            this.PAtacado});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDados.DefaultCellStyle = dataGridViewCellStyle6;
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(0, 0);
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
            this.GridDados.RowHeadersWidth = 15;
            this.GridDados.Size = new System.Drawing.Size(589, 299);
            this.GridDados.TabIndex = 6;
            this.GridDados.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridDados_CellEndEdit);
            this.GridDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridDados_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.LblTotal);
            this.panel1.Controls.Add(this.LblItens);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 299);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 34);
            this.panel1.TabIndex = 7;
            // 
            // LblTotal
            // 
            this.LblTotal.BackColor = System.Drawing.Color.Transparent;
            this.LblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.ForeColor = System.Drawing.Color.Maroon;
            this.LblTotal.Location = new System.Drawing.Point(330, 10);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(81, 15);
            this.LblTotal.TabIndex = 3;
            this.LblTotal.Text = "0,00";
            this.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblItens
            // 
            this.LblItens.BackColor = System.Drawing.Color.Transparent;
            this.LblItens.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItens.ForeColor = System.Drawing.Color.Maroon;
            this.LblItens.Location = new System.Drawing.Point(129, 10);
            this.LblItens.Name = "LblItens";
            this.LblItens.Size = new System.Drawing.Size(39, 15);
            this.LblItens.TabIndex = 2;
            this.LblItens.Text = "0";
            this.LblItens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.Maroon;
            this.Label2.Location = new System.Drawing.Point(244, 10);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(80, 15);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Valor Total:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Maroon;
            this.Label1.Location = new System.Drawing.Point(25, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(98, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Total de Itens:";
            // 
            // IdRota
            // 
            this.IdRota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdRota.DataPropertyName = "Id_Produto";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "000";
            this.IdRota.DefaultCellStyle = dataGridViewCellStyle2;
            this.IdRota.HeaderText = "Codigo";
            this.IdRota.Name = "IdRota";
            this.IdRota.Visible = false;
            this.IdRota.Width = 60;
            // 
            // Cnpj
            // 
            this.Cnpj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Cnpj.DataPropertyName = "Descricao";
            this.Cnpj.HeaderText = "Descrição do Produto";
            this.Cnpj.Name = "Cnpj";
            this.Cnpj.ReadOnly = true;
            this.Cnpj.Width = 250;
            // 
            // ColRefPrd
            // 
            this.ColRefPrd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColRefPrd.DataPropertyName = "Referencia";
            this.ColRefPrd.HeaderText = "Referência";
            this.ColRefPrd.Name = "ColRefPrd";
            this.ColRefPrd.ReadOnly = true;
            this.ColRefPrd.Width = 65;
            // 
            // ColQtde
            // 
            this.ColQtde.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColQtde.DataPropertyName = "Qtde";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "0";
            this.ColQtde.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColQtde.HeaderText = "Qtde.";
            this.ColQtde.Name = "ColQtde";
            this.ColQtde.Width = 60;
            // 
            // PSensacional
            // 
            this.PSensacional.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PSensacional.DataPropertyName = "PrcSensacional";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0,00";
            this.PSensacional.DefaultCellStyle = dataGridViewCellStyle4;
            this.PSensacional.HeaderText = "Prc.Sensac.";
            this.PSensacional.Name = "PSensacional";
            this.PSensacional.Width = 70;
            // 
            // ColPrcFinal
            // 
            this.ColPrcFinal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColPrcFinal.DataPropertyName = "VlrFinal";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0,00";
            this.ColPrcFinal.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColPrcFinal.HeaderText = "Valor Final";
            this.ColPrcFinal.Name = "ColPrcFinal";
            this.ColPrcFinal.ReadOnly = true;
            this.ColPrcFinal.Width = 90;
            // 
            // PEspecial
            // 
            this.PEspecial.DataPropertyName = "PrcEspecial";
            this.PEspecial.HeaderText = "Prc.Especial";
            this.PEspecial.Name = "PEspecial";
            this.PEspecial.Visible = false;
            this.PEspecial.Width = 91;
            // 
            // PVarejo
            // 
            this.PVarejo.DataPropertyName = "PrcVarejo";
            this.PVarejo.HeaderText = "Prc.Varejo";
            this.PVarejo.Name = "PVarejo";
            this.PVarejo.Visible = false;
            this.PVarejo.Width = 81;
            // 
            // PMinimo
            // 
            this.PMinimo.DataPropertyName = "PrcMinimo";
            this.PMinimo.HeaderText = "Prc.Minimo";
            this.PMinimo.Name = "PMinimo";
            this.PMinimo.Visible = false;
            this.PMinimo.Width = 84;
            // 
            // PAtacado
            // 
            this.PAtacado.DataPropertyName = "PrcAtacado";
            this.PAtacado.HeaderText = "PrcAtacado";
            this.PAtacado.Name = "PAtacado";
            this.PAtacado.Visible = false;
            this.PAtacado.Width = 88;
            // 
            // FrmLancItensProm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 333);
            this.Controls.Add(this.GridDados);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLancItensProm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informe as Quantidade por Item";
            this.Load += new System.EventHandler(this.FrmLancItensProm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label LblItens;
        private System.Windows.Forms.Label LblTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRota;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cnpj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRefPrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQtde;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSensacional;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrcFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn PEspecial;
        private System.Windows.Forms.DataGridViewTextBoxColumn PVarejo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PMinimo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PAtacado;
    }
}