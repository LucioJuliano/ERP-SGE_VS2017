namespace ERP_SGE
{
    partial class FrmDIEF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDIEF));
            this.ArqOrigem = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtArqOrigem = new System.Windows.Forms.TextBox();
            this.BtnAbrirOrigem = new System.Windows.Forms.Button();
            this.ArqDestino = new System.Windows.Forms.SaveFileDialog();
            this.BtnSalvarDestino = new System.Windows.Forms.Button();
            this.TxtArqDestino = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BarProc = new System.Windows.Forms.ProgressBar();
            this.BoxPeriodo = new System.Windows.Forms.GroupBox();
            this.BtnProcessar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.LstMes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtAno = new System.Windows.Forms.NumericUpDown();
            this.BoxPeriodo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAno)).BeginInit();
            this.SuspendLayout();
            // 
            // ArqOrigem
            // 
            this.ArqOrigem.DefaultExt = "*.txt";
            this.ArqOrigem.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*\"";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arquivo de Origem:";
            // 
            // TxtArqOrigem
            // 
            this.TxtArqOrigem.Enabled = false;
            this.TxtArqOrigem.Location = new System.Drawing.Point(128, 44);
            this.TxtArqOrigem.Name = "TxtArqOrigem";
            this.TxtArqOrigem.Size = new System.Drawing.Size(358, 20);
            this.TxtArqOrigem.TabIndex = 1;
            // 
            // BtnAbrirOrigem
            // 
            this.BtnAbrirOrigem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnAbrirOrigem.BackgroundImage")));
            this.BtnAbrirOrigem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnAbrirOrigem.Location = new System.Drawing.Point(492, 44);
            this.BtnAbrirOrigem.Name = "BtnAbrirOrigem";
            this.BtnAbrirOrigem.Size = new System.Drawing.Size(19, 20);
            this.BtnAbrirOrigem.TabIndex = 9;
            this.BtnAbrirOrigem.UseVisualStyleBackColor = true;
            this.BtnAbrirOrigem.Click += new System.EventHandler(this.BtnAbrirOrigem_Click);
            // 
            // ArqDestino
            // 
            this.ArqDestino.DefaultExt = "TXT";
            this.ArqDestino.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*\"";
            // 
            // BtnSalvarDestino
            // 
            this.BtnSalvarDestino.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSalvarDestino.BackgroundImage")));
            this.BtnSalvarDestino.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnSalvarDestino.Location = new System.Drawing.Point(492, 70);
            this.BtnSalvarDestino.Name = "BtnSalvarDestino";
            this.BtnSalvarDestino.Size = new System.Drawing.Size(19, 20);
            this.BtnSalvarDestino.TabIndex = 12;
            this.BtnSalvarDestino.UseVisualStyleBackColor = true;
            this.BtnSalvarDestino.Click += new System.EventHandler(this.BtnSalvarDestino_Click);
            // 
            // TxtArqDestino
            // 
            this.TxtArqDestino.Enabled = false;
            this.TxtArqDestino.Location = new System.Drawing.Point(128, 70);
            this.TxtArqDestino.Name = "TxtArqDestino";
            this.TxtArqDestino.Size = new System.Drawing.Size(358, 20);
            this.TxtArqDestino.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Arquivo de Destino:";
            // 
            // BarProc
            // 
            this.BarProc.Location = new System.Drawing.Point(4, 138);
            this.BarProc.Name = "BarProc";
            this.BarProc.Size = new System.Drawing.Size(505, 23);
            this.BarProc.Step = 1;
            this.BarProc.TabIndex = 13;
            // 
            // BoxPeriodo
            // 
            this.BoxPeriodo.BackColor = System.Drawing.Color.Transparent;
            this.BoxPeriodo.Controls.Add(this.TxtAno);
            this.BoxPeriodo.Controls.Add(this.label3);
            this.BoxPeriodo.Controls.Add(this.LstMes);
            this.BoxPeriodo.Controls.Add(this.BtnProcessar);
            this.BoxPeriodo.Controls.Add(this.label16);
            this.BoxPeriodo.Location = new System.Drawing.Point(4, 93);
            this.BoxPeriodo.Name = "BoxPeriodo";
            this.BoxPeriodo.Size = new System.Drawing.Size(480, 39);
            this.BoxPeriodo.TabIndex = 71;
            this.BoxPeriodo.TabStop = false;
            // 
            // BtnProcessar
            // 
            this.BtnProcessar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProcessar.ForeColor = System.Drawing.Color.Maroon;
            this.BtnProcessar.Image = ((System.Drawing.Image)(resources.GetObject("BtnProcessar.Image")));
            this.BtnProcessar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnProcessar.Location = new System.Drawing.Point(335, 8);
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
            this.label5.Location = new System.Drawing.Point(10, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Filial:";
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Location = new System.Drawing.Point(48, 5);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(436, 21);
            this.LstFilial.TabIndex = 73;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(6, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "Mês:";
            // 
            // LstMes
            // 
            this.LstMes.FormattingEnabled = true;
            this.LstMes.Items.AddRange(new object[] {
            "Janeiro",
            "Fevereiro",
            "Março",
            "Abril",
            "Maio",
            "Junho",
            "Julho",
            "Agosto",
            "Setembro",
            "Outubro",
            "Novembro",
            "Dezembro"});
            this.LstMes.Location = new System.Drawing.Point(35, 12);
            this.LstMes.Name = "LstMes";
            this.LstMes.Size = new System.Drawing.Size(77, 21);
            this.LstMes.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(121, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Ano:";
            // 
            // TxtAno
            // 
            this.TxtAno.Location = new System.Drawing.Point(151, 12);
            this.TxtAno.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.TxtAno.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.TxtAno.Name = "TxtAno";
            this.TxtAno.Size = new System.Drawing.Size(58, 20);
            this.TxtAno.TabIndex = 14;
            this.TxtAno.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // FrmDIEF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(521, 174);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LstFilial);
            this.Controls.Add(this.BoxPeriodo);
            this.Controls.Add(this.BarProc);
            this.Controls.Add(this.BtnSalvarDestino);
            this.Controls.Add(this.TxtArqDestino);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnAbrirOrigem);
            this.Controls.Add(this.TxtArqOrigem);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDIEF";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DIEF";
            this.Load += new System.EventHandler(this.FrmDIEF_Load);
            this.BoxPeriodo.ResumeLayout(false);
            this.BoxPeriodo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAno)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ArqOrigem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtArqOrigem;
        private System.Windows.Forms.Button BtnAbrirOrigem;
        private System.Windows.Forms.SaveFileDialog ArqDestino;
        private System.Windows.Forms.Button BtnSalvarDestino;
        private System.Windows.Forms.TextBox TxtArqDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar BarProc;
        private System.Windows.Forms.GroupBox BoxPeriodo;
        private System.Windows.Forms.Button BtnProcessar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox LstMes;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.NumericUpDown TxtAno;
    }
}