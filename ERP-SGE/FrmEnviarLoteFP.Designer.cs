namespace ERP_SGE
{
    partial class FrmEnviarLoteFP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnviarLoteFP));
            this.BoxFilial = new System.Windows.Forms.GroupBox();
            this.LstFilial = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.BoxMesAno = new System.Windows.Forms.GroupBox();
            this.Cb_Quizena = new System.Windows.Forms.CheckBox();
            this.TxtAnoEventos = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.LstMesEventos = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.BtnLote = new System.Windows.Forms.Button();
            this.ProcBar = new System.Windows.Forms.ProgressBar();
            this.BoxFilial.SuspendLayout();
            this.BoxMesAno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAnoEventos)).BeginInit();
            this.SuspendLayout();
            // 
            // BoxFilial
            // 
            this.BoxFilial.BackColor = System.Drawing.Color.Transparent;
            this.BoxFilial.Controls.Add(this.LstFilial);
            this.BoxFilial.Controls.Add(this.label34);
            this.BoxFilial.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxFilial.Location = new System.Drawing.Point(0, 44);
            this.BoxFilial.Name = "BoxFilial";
            this.BoxFilial.Size = new System.Drawing.Size(342, 34);
            this.BoxFilial.TabIndex = 203;
            this.BoxFilial.TabStop = false;
            // 
            // LstFilial
            // 
            this.LstFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstFilial.FormattingEnabled = true;
            this.LstFilial.Items.AddRange(new object[] {
            "Analfaberto",
            "Fundamental",
            "Médio",
            "Superior Incompleto",
            "Superior Completo",
            "Curso Técnico"});
            this.LstFilial.Location = new System.Drawing.Point(39, 11);
            this.LstFilial.Name = "LstFilial";
            this.LstFilial.Size = new System.Drawing.Size(291, 21);
            this.LstFilial.TabIndex = 188;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label34.Location = new System.Drawing.Point(7, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(37, 13);
            this.label34.TabIndex = 190;
            this.label34.Text = "Filial:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label34.UseMnemonic = false;
            // 
            // BoxMesAno
            // 
            this.BoxMesAno.BackColor = System.Drawing.Color.Transparent;
            this.BoxMesAno.Controls.Add(this.Cb_Quizena);
            this.BoxMesAno.Controls.Add(this.TxtAnoEventos);
            this.BoxMesAno.Controls.Add(this.label28);
            this.BoxMesAno.Controls.Add(this.LstMesEventos);
            this.BoxMesAno.Controls.Add(this.label29);
            this.BoxMesAno.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoxMesAno.Location = new System.Drawing.Point(0, 0);
            this.BoxMesAno.Name = "BoxMesAno";
            this.BoxMesAno.Size = new System.Drawing.Size(342, 44);
            this.BoxMesAno.TabIndex = 202;
            this.BoxMesAno.TabStop = false;
            // 
            // Cb_Quizena
            // 
            this.Cb_Quizena.AutoSize = true;
            this.Cb_Quizena.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cb_Quizena.Location = new System.Drawing.Point(247, 18);
            this.Cb_Quizena.Name = "Cb_Quizena";
            this.Cb_Quizena.Size = new System.Drawing.Size(72, 17);
            this.Cb_Quizena.TabIndex = 69;
            this.Cb_Quizena.Text = "Quizena";
            this.Cb_Quizena.UseVisualStyleBackColor = true;
            this.Cb_Quizena.Visible = false;
            // 
            // TxtAnoEventos
            // 
            this.TxtAnoEventos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAnoEventos.Location = new System.Drawing.Point(179, 16);
            this.TxtAnoEventos.Maximum = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.TxtAnoEventos.Minimum = new decimal(new int[] {
            2012,
            0,
            0,
            0});
            this.TxtAnoEventos.Name = "TxtAnoEventos";
            this.TxtAnoEventos.Size = new System.Drawing.Size(61, 20);
            this.TxtAnoEventos.TabIndex = 67;
            this.TxtAnoEventos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAnoEventos.Value = new decimal(new int[] {
            2012,
            0,
            0,
            0});
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(4, 21);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(34, 13);
            this.label28.TabIndex = 65;
            this.label28.Text = "Mês:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstMesEventos
            // 
            this.LstMesEventos.FormattingEnabled = true;
            this.LstMesEventos.Items.AddRange(new object[] {
            "Todos",
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
            "Dezembro",
            "13º Salario"});
            this.LstMesEventos.Location = new System.Drawing.Point(39, 16);
            this.LstMesEventos.Name = "LstMesEventos";
            this.LstMesEventos.Size = new System.Drawing.Size(101, 21);
            this.LstMesEventos.TabIndex = 66;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(146, 21);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(33, 13);
            this.label29.TabIndex = 68;
            this.label29.Text = "Ano:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnLote
            // 
            this.BtnLote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLote.Image = ((System.Drawing.Image)(resources.GetObject("BtnLote.Image")));
            this.BtnLote.Location = new System.Drawing.Point(112, 93);
            this.BtnLote.Name = "BtnLote";
            this.BtnLote.Size = new System.Drawing.Size(128, 30);
            this.BtnLote.TabIndex = 201;
            this.BtnLote.Text = "Gerar Lote";
            this.BtnLote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLote.UseVisualStyleBackColor = true;
            this.BtnLote.Click += new System.EventHandler(this.BtnLote_Click);
            // 
            // ProcBar
            // 
            this.ProcBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProcBar.Location = new System.Drawing.Point(0, 139);
            this.ProcBar.Name = "ProcBar";
            this.ProcBar.Size = new System.Drawing.Size(342, 23);
            this.ProcBar.TabIndex = 204;
            // 
            // FrmEnviarLoteFP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(342, 162);
            this.Controls.Add(this.ProcBar);
            this.Controls.Add(this.BoxFilial);
            this.Controls.Add(this.BoxMesAno);
            this.Controls.Add(this.BtnLote);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEnviarLoteFP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar Lote Banco Santander";
            this.Load += new System.EventHandler(this.FrmEnviarLoteFP_Load);
            this.BoxFilial.ResumeLayout(false);
            this.BoxFilial.PerformLayout();
            this.BoxMesAno.ResumeLayout(false);
            this.BoxMesAno.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAnoEventos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BoxFilial;
        private System.Windows.Forms.ComboBox LstFilial;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox BoxMesAno;
        private System.Windows.Forms.CheckBox Cb_Quizena;
        private System.Windows.Forms.NumericUpDown TxtAnoEventos;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox LstMesEventos;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button BtnLote;
        private System.Windows.Forms.ProgressBar ProcBar;
    }
}