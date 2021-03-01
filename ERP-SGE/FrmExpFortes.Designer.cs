namespace ERP_SGE
{
    partial class FrmExpFortes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpFortes));
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.BtnProcessar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.BoxPeriodo = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.Dt2 = new System.Windows.Forms.DateTimePicker();
            this.Dt1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.BarProc = new System.Windows.Forms.ProgressBar();
            this.ArqDestino = new System.Windows.Forms.SaveFileDialog();
            this.BtnSalvarDestino = new System.Windows.Forms.Button();
            this.TxtArqDestino = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtEmpresaAC = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Ck_Invetario = new System.Windows.Forms.CheckBox();
            this.Ck_CFiscal = new System.Windows.Forms.CheckBox();
            this.Ck_NF_Saida = new System.Windows.Forms.CheckBox();
            this.Ck_NF_Entrada = new System.Windows.Forms.CheckBox();
            this.BoxPeriodo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Location = new System.Drawing.Point(52, 3);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(436, 21);
            this.LstFilial.TabIndex = 83;
            // 
            // BtnProcessar
            // 
            this.BtnProcessar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProcessar.ForeColor = System.Drawing.Color.Maroon;
            this.BtnProcessar.Image = ((System.Drawing.Image)(resources.GetObject("BtnProcessar.Image")));
            this.BtnProcessar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnProcessar.Location = new System.Drawing.Point(360, 9);
            this.BtnProcessar.Name = "BtnProcessar";
            this.BtnProcessar.Size = new System.Drawing.Size(139, 30);
            this.BtnProcessar.TabIndex = 11;
            this.BtnProcessar.Text = "Processar";
            this.BtnProcessar.UseVisualStyleBackColor = true;
            this.BtnProcessar.Click += new System.EventHandler(this.BtnProcessar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "Filial:";
            // 
            // BoxPeriodo
            // 
            this.BoxPeriodo.BackColor = System.Drawing.Color.Transparent;
            this.BoxPeriodo.Controls.Add(this.label16);
            this.BoxPeriodo.Controls.Add(this.BtnProcessar);
            this.BoxPeriodo.Controls.Add(this.Dt2);
            this.BoxPeriodo.Controls.Add(this.Dt1);
            this.BoxPeriodo.Controls.Add(this.label6);
            this.BoxPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxPeriodo.Location = new System.Drawing.Point(8, 131);
            this.BoxPeriodo.Name = "BoxPeriodo";
            this.BoxPeriodo.Size = new System.Drawing.Size(505, 39);
            this.BoxPeriodo.TabIndex = 82;
            this.BoxPeriodo.TabStop = false;
            this.BoxPeriodo.Text = "Período";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(136, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 88;
            this.label16.Text = "a";
            // 
            // Dt2
            // 
            this.Dt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt2.Location = new System.Drawing.Point(154, 13);
            this.Dt2.Name = "Dt2";
            this.Dt2.Size = new System.Drawing.Size(100, 20);
            this.Dt2.TabIndex = 86;
            // 
            // Dt1
            // 
            this.Dt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt1.Location = new System.Drawing.Point(30, 13);
            this.Dt1.Name = "Dt1";
            this.Dt1.Size = new System.Drawing.Size(100, 20);
            this.Dt1.TabIndex = 85;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 87;
            this.label6.Text = "de:";
            // 
            // BarProc
            // 
            this.BarProc.Location = new System.Drawing.Point(8, 176);
            this.BarProc.Name = "BarProc";
            this.BarProc.Size = new System.Drawing.Size(505, 23);
            this.BarProc.Step = 1;
            this.BarProc.TabIndex = 81;
            // 
            // ArqDestino
            // 
            this.ArqDestino.DefaultExt = "FS";
            this.ArqDestino.Filter = "fs files (*.fs)|*.fs|All files (*.*)|*.*\"";
            // 
            // BtnSalvarDestino
            // 
            this.BtnSalvarDestino.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSalvarDestino.BackgroundImage")));
            this.BtnSalvarDestino.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnSalvarDestino.Location = new System.Drawing.Point(496, 30);
            this.BtnSalvarDestino.Name = "BtnSalvarDestino";
            this.BtnSalvarDestino.Size = new System.Drawing.Size(19, 20);
            this.BtnSalvarDestino.TabIndex = 80;
            this.BtnSalvarDestino.UseVisualStyleBackColor = true;
            this.BtnSalvarDestino.Click += new System.EventHandler(this.BtnSalvarDestino_Click);
            // 
            // TxtArqDestino
            // 
            this.TxtArqDestino.Enabled = false;
            this.TxtArqDestino.Location = new System.Drawing.Point(52, 30);
            this.TxtArqDestino.Name = "TxtArqDestino";
            this.TxtArqDestino.Size = new System.Drawing.Size(438, 20);
            this.TxtArqDestino.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-2, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 78;
            this.label2.Text = "Gravar:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 85;
            this.label1.Text = "Empresa no AC Fiscal:";
            // 
            // TxtEmpresaAC
            // 
            this.TxtEmpresaAC.Location = new System.Drawing.Point(151, 61);
            this.TxtEmpresaAC.Name = "TxtEmpresaAC";
            this.TxtEmpresaAC.Size = new System.Drawing.Size(335, 20);
            this.TxtEmpresaAC.TabIndex = 86;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Ck_Invetario);
            this.groupBox1.Controls.Add(this.Ck_CFiscal);
            this.groupBox1.Controls.Add(this.Ck_NF_Saida);
            this.groupBox1.Controls.Add(this.Ck_NF_Entrada);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 38);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selecione os Itens a Exportar";
            // 
            // Ck_Invetario
            // 
            this.Ck_Invetario.AutoSize = true;
            this.Ck_Invetario.Checked = true;
            this.Ck_Invetario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ck_Invetario.Location = new System.Drawing.Point(336, 15);
            this.Ck_Invetario.Name = "Ck_Invetario";
            this.Ck_Invetario.Size = new System.Drawing.Size(83, 17);
            this.Ck_Invetario.TabIndex = 3;
            this.Ck_Invetario.Text = "Inventario";
            this.Ck_Invetario.UseVisualStyleBackColor = true;
            // 
            // Ck_CFiscal
            // 
            this.Ck_CFiscal.AutoSize = true;
            this.Ck_CFiscal.Checked = true;
            this.Ck_CFiscal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ck_CFiscal.Location = new System.Drawing.Point(229, 15);
            this.Ck_CFiscal.Name = "Ck_CFiscal";
            this.Ck_CFiscal.Size = new System.Drawing.Size(101, 17);
            this.Ck_CFiscal.TabIndex = 2;
            this.Ck_CFiscal.Text = "Cupom Fiscal";
            this.Ck_CFiscal.UseVisualStyleBackColor = true;
            // 
            // Ck_NF_Saida
            // 
            this.Ck_NF_Saida.AutoSize = true;
            this.Ck_NF_Saida.Checked = true;
            this.Ck_NF_Saida.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ck_NF_Saida.Location = new System.Drawing.Point(122, 15);
            this.Ck_NF_Saida.Name = "Ck_NF_Saida";
            this.Ck_NF_Saida.Size = new System.Drawing.Size(101, 17);
            this.Ck_NF_Saida.TabIndex = 1;
            this.Ck_NF_Saida.Text = "Notas Saidas";
            this.Ck_NF_Saida.UseVisualStyleBackColor = true;
            // 
            // Ck_NF_Entrada
            // 
            this.Ck_NF_Entrada.AutoSize = true;
            this.Ck_NF_Entrada.Checked = true;
            this.Ck_NF_Entrada.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ck_NF_Entrada.Location = new System.Drawing.Point(9, 15);
            this.Ck_NF_Entrada.Name = "Ck_NF_Entrada";
            this.Ck_NF_Entrada.Size = new System.Drawing.Size(107, 17);
            this.Ck_NF_Entrada.TabIndex = 0;
            this.Ck_NF_Entrada.Text = "Notas Entrada";
            this.Ck_NF_Entrada.UseVisualStyleBackColor = true;
            // 
            // FrmExpFortes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(530, 238);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TxtEmpresaAC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LstFilial);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BoxPeriodo);
            this.Controls.Add(this.BarProc);
            this.Controls.Add(this.BtnSalvarDestino);
            this.Controls.Add(this.TxtArqDestino);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExpFortes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportação de Movimentos para Sistema das Fortes AC Fiscal";
            this.Load += new System.EventHandler(this.FrmExpFortes_Load);
            this.BoxPeriodo.ResumeLayout(false);
            this.BoxPeriodo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.Button BtnProcessar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox BoxPeriodo;
        private System.Windows.Forms.ProgressBar BarProc;
        private System.Windows.Forms.SaveFileDialog ArqDestino;
        private System.Windows.Forms.Button BtnSalvarDestino;
        private System.Windows.Forms.TextBox TxtArqDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker Dt2;
        private System.Windows.Forms.DateTimePicker Dt1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtEmpresaAC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Ck_CFiscal;
        private System.Windows.Forms.CheckBox Ck_NF_Saida;
        private System.Windows.Forms.CheckBox Ck_NF_Entrada;
        private System.Windows.Forms.CheckBox Ck_Invetario;
    }
}