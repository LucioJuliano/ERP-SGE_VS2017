namespace ERP_SGE
{
    partial class FrmCadDepartamento
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadDepartamento));
            this.BoxItemPesq = new System.Windows.Forms.GroupBox();
            this.ChkNome = new System.Windows.Forms.RadioButton();
            this.ChkCodigo = new System.Windows.Forms.RadioButton();
            this.GridDados = new System.Windows.Forms.DataGridView();
            this.IdUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsRota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pag02 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtRamal = new System.Windows.Forms.NumericUpDown();
            this.TxtResponsavel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDepartamento = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Paginas = new System.Windows.Forms.TabControl();
            this.Pag01 = new System.Windows.Forms.TabPage();
            this.BoxPesquisa = new System.Windows.Forms.GroupBox();
            this.BtnPesquisa = new System.Windows.Forms.Button();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BoxItemPesq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).BeginInit();
            this.Pag02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtRamal)).BeginInit();
            this.Paginas.SuspendLayout();
            this.Pag01.SuspendLayout();
            this.BoxPesquisa.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoxItemPesq
            // 
            this.BoxItemPesq.Controls.Add(this.ChkNome);
            this.BoxItemPesq.Controls.Add(this.ChkCodigo);
            this.BoxItemPesq.Location = new System.Drawing.Point(4, 10);
            this.BoxItemPesq.Name = "BoxItemPesq";
            this.BoxItemPesq.Size = new System.Drawing.Size(116, 40);
            this.BoxItemPesq.TabIndex = 6;
            this.BoxItemPesq.TabStop = false;
            // 
            // ChkNome
            // 
            this.ChkNome.AutoSize = true;
            this.ChkNome.Location = new System.Drawing.Point(6, 22);
            this.ChkNome.Name = "ChkNome";
            this.ChkNome.Size = new System.Drawing.Size(104, 17);
            this.ChkNome.TabIndex = 8;
            this.ChkNome.TabStop = true;
            this.ChkNome.Text = "Departamento";
            this.ChkNome.UseVisualStyleBackColor = true;
            // 
            // ChkCodigo
            // 
            this.ChkCodigo.AutoSize = true;
            this.ChkCodigo.Location = new System.Drawing.Point(6, 5);
            this.ChkCodigo.Name = "ChkCodigo";
            this.ChkCodigo.Size = new System.Drawing.Size(64, 17);
            this.ChkCodigo.TabIndex = 7;
            this.ChkCodigo.TabStop = true;
            this.ChkCodigo.Text = "Código";
            this.ChkCodigo.UseVisualStyleBackColor = true;
            // 
            // GridDados
            // 
            this.GridDados.AllowUserToAddRows = false;
            this.GridDados.AllowUserToDeleteRows = false;
            this.GridDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdUF,
            this.DsRota});
            this.GridDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDados.Location = new System.Drawing.Point(3, 56);
            this.GridDados.MultiSelect = false;
            this.GridDados.Name = "GridDados";
            this.GridDados.ReadOnly = true;
            this.GridDados.Size = new System.Drawing.Size(855, 324);
            this.GridDados.TabIndex = 3;
            this.GridDados.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_DoubleClick);
            // 
            // IdUF
            // 
            this.IdUF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdUF.DataPropertyName = "Id_Departamento";
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "000";
            this.IdUF.DefaultCellStyle = dataGridViewCellStyle3;
            this.IdUF.HeaderText = "Codigo";
            this.IdUF.Name = "IdUF";
            this.IdUF.ReadOnly = true;
            this.IdUF.Width = 50;
            // 
            // DsRota
            // 
            this.DsRota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DsRota.DataPropertyName = "Departamento";
            this.DsRota.HeaderText = "Departamento";
            this.DsRota.Name = "DsRota";
            this.DsRota.ReadOnly = true;
            this.DsRota.Width = 300;
            // 
            // Pag02
            // 
            this.Pag02.BackColor = System.Drawing.Color.White;
            this.Pag02.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.Pag02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pag02.Controls.Add(this.label4);
            this.Pag02.Controls.Add(this.TxtRamal);
            this.Pag02.Controls.Add(this.TxtResponsavel);
            this.Pag02.Controls.Add(this.label2);
            this.Pag02.Controls.Add(this.TxtDepartamento);
            this.Pag02.Controls.Add(this.label5);
            this.Pag02.Controls.Add(this.TxtCodigo);
            this.Pag02.Controls.Add(this.label1);
            this.Pag02.Location = new System.Drawing.Point(4, 22);
            this.Pag02.Name = "Pag02";
            this.Pag02.Padding = new System.Windows.Forms.Padding(3);
            this.Pag02.Size = new System.Drawing.Size(861, 383);
            this.Pag02.TabIndex = 1;
            this.Pag02.Text = "Pagina de Dados";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(406, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Ramal:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtRamal
            // 
            this.TxtRamal.Location = new System.Drawing.Point(452, 52);
            this.TxtRamal.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.TxtRamal.Name = "TxtRamal";
            this.TxtRamal.Size = new System.Drawing.Size(56, 20);
            this.TxtRamal.TabIndex = 33;
            this.TxtRamal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtResponsavel
            // 
            this.TxtResponsavel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtResponsavel.Location = new System.Drawing.Point(80, 52);
            this.TxtResponsavel.MaxLength = 30;
            this.TxtResponsavel.Name = "TxtResponsavel";
            this.TxtResponsavel.Size = new System.Drawing.Size(320, 20);
            this.TxtResponsavel.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(2, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Reponsável:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtDepartamento
            // 
            this.TxtDepartamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDepartamento.Location = new System.Drawing.Point(235, 16);
            this.TxtDepartamento.MaxLength = 60;
            this.TxtDepartamento.Name = "TxtDepartamento";
            this.TxtDepartamento.Size = new System.Drawing.Size(320, 20);
            this.TxtDepartamento.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(143, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Departamento:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtCodigo
            // 
            this.TxtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtCodigo.Enabled = false;
            this.TxtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TxtCodigo.Location = new System.Drawing.Point(80, 16);
            this.TxtCodigo.Name = "TxtCodigo";
            this.TxtCodigo.Size = new System.Drawing.Size(58, 20);
            this.TxtCodigo.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 19);
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
            this.Paginas.Size = new System.Drawing.Size(869, 409);
            this.Paginas.TabIndex = 6;
            this.Paginas.SelectedIndexChanged += new System.EventHandler(this.Paginas_SelectedIndexChanged);
            // 
            // Pag01
            // 
            this.Pag01.Controls.Add(this.GridDados);
            this.Pag01.Controls.Add(this.BoxPesquisa);
            this.Pag01.Location = new System.Drawing.Point(4, 22);
            this.Pag01.Name = "Pag01";
            this.Pag01.Padding = new System.Windows.Forms.Padding(3);
            this.Pag01.Size = new System.Drawing.Size(861, 383);
            this.Pag01.TabIndex = 0;
            this.Pag01.Text = "Pesquisa";
            this.Pag01.UseVisualStyleBackColor = true;
            // 
            // BoxPesquisa
            // 
            this.BoxPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.BoxPesquisa.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BoxPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BoxPesquisa.Controls.Add(this.BoxItemPesq);
            this.BoxPesquisa.Controls.Add(this.BtnPesquisa);
            this.BoxPesquisa.Controls.Add(this.TxtPesquisa);
            this.BoxPesquisa.Controls.Add(this.label3);
            this.BoxPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BoxPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxPesquisa.Location = new System.Drawing.Point(3, 3);
            this.BoxPesquisa.Name = "BoxPesquisa";
            this.BoxPesquisa.Size = new System.Drawing.Size(855, 53);
            this.BoxPesquisa.TabIndex = 2;
            this.BoxPesquisa.TabStop = false;
            this.BoxPesquisa.Text = "Pesquisa";
            // 
            // BtnPesquisa
            // 
            this.BtnPesquisa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPesquisa.BackgroundImage")));
            this.BtnPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnPesquisa.Location = new System.Drawing.Point(526, 15);
            this.BtnPesquisa.Name = "BtnPesquisa";
            this.BtnPesquisa.Size = new System.Drawing.Size(35, 30);
            this.BtnPesquisa.TabIndex = 2;
            this.BtnPesquisa.UseVisualStyleBackColor = true;
            this.BtnPesquisa.Click += new System.EventHandler(this.BtnPesquisa_Click);
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(188, 22);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(331, 20);
            this.TxtPesquisa.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(125, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Conteúdo:";
            // 
            // FrmCadDepartamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 409);
            this.Controls.Add(this.Paginas);
            this.KeyPreview = true;
            this.Name = "FrmCadDepartamento";
            this.Text = "Cadastro dos Departamentos";
            this.Deactivate += new System.EventHandler(this.Frm_Deactivate);
            this.Load += new System.EventHandler(this.Frm_Load);
            this.Activated += new System.EventHandler(this.Frm_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MudarCampo);
            this.BoxItemPesq.ResumeLayout(false);
            this.BoxItemPesq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDados)).EndInit();
            this.Pag02.ResumeLayout(false);
            this.Pag02.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtRamal)).EndInit();
            this.Paginas.ResumeLayout(false);
            this.Pag01.ResumeLayout(false);
            this.BoxPesquisa.ResumeLayout(false);
            this.BoxPesquisa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BoxItemPesq;
        private System.Windows.Forms.RadioButton ChkNome;
        private System.Windows.Forms.RadioButton ChkCodigo;
        private System.Windows.Forms.DataGridView GridDados;
        private System.Windows.Forms.TabPage Pag02;
        private System.Windows.Forms.TextBox TxtDepartamento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl Paginas;
        private System.Windows.Forms.TabPage Pag01;
        private System.Windows.Forms.GroupBox BoxPesquisa;
        private System.Windows.Forms.Button BtnPesquisa;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsRota;
        private System.Windows.Forms.TextBox TxtResponsavel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown TxtRamal;
    }
}