namespace ERP_SGE
{
    partial class FrmImpBoleto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImpBoleto));
            this.TxtIdVenda = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.BoxBoleto = new System.Windows.Forms.GroupBox();
            this.Rb_BoletoForm = new System.Windows.Forms.RadioButton();
            this.Rb_BoletoA4 = new System.Windows.Forms.RadioButton();
            this.BoxRecibo = new System.Windows.Forms.GroupBox();
            this.Rb_Nao = new System.Windows.Forms.RadioButton();
            this.Rb_Sim = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.TxtIdVenda)).BeginInit();
            this.BoxBoleto.SuspendLayout();
            this.BoxRecibo.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtIdVenda
            // 
            this.TxtIdVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIdVenda.Location = new System.Drawing.Point(192, 9);
            this.TxtIdVenda.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.TxtIdVenda.Name = "TxtIdVenda";
            this.TxtIdVenda.Size = new System.Drawing.Size(78, 23);
            this.TxtIdVenda.TabIndex = 67;
            this.TxtIdVenda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 17);
            this.label1.TabIndex = 66;
            this.label1.Text = "Informe o Nº da Venda:";
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("BtnImprimir.Image")));
            this.BtnImprimir.Location = new System.Drawing.Point(93, 79);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(123, 34);
            this.BtnImprimir.TabIndex = 68;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
            // 
            // BoxBoleto
            // 
            this.BoxBoleto.BackColor = System.Drawing.Color.Transparent;
            this.BoxBoleto.Controls.Add(this.Rb_BoletoForm);
            this.BoxBoleto.Controls.Add(this.Rb_BoletoA4);
            this.BoxBoleto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxBoleto.Location = new System.Drawing.Point(165, 38);
            this.BoxBoleto.Name = "BoxBoleto";
            this.BoxBoleto.Size = new System.Drawing.Size(166, 41);
            this.BoxBoleto.TabIndex = 69;
            this.BoxBoleto.TabStop = false;
            this.BoxBoleto.Text = "Tipo de Boleto";
            this.BoxBoleto.Visible = false;
            // 
            // Rb_BoletoForm
            // 
            this.Rb_BoletoForm.AutoSize = true;
            this.Rb_BoletoForm.Location = new System.Drawing.Point(97, 18);
            this.Rb_BoletoForm.Name = "Rb_BoletoForm";
            this.Rb_BoletoForm.Size = new System.Drawing.Size(134, 17);
            this.Rb_BoletoForm.TabIndex = 1;
            this.Rb_BoletoForm.TabStop = true;
            this.Rb_BoletoForm.Text = "Formuário Continuo";
            this.Rb_BoletoForm.UseVisualStyleBackColor = true;
            // 
            // Rb_BoletoA4
            // 
            this.Rb_BoletoA4.AutoSize = true;
            this.Rb_BoletoA4.Location = new System.Drawing.Point(7, 18);
            this.Rb_BoletoA4.Name = "Rb_BoletoA4";
            this.Rb_BoletoA4.Size = new System.Drawing.Size(80, 17);
            this.Rb_BoletoA4.TabIndex = 0;
            this.Rb_BoletoA4.TabStop = true;
            this.Rb_BoletoA4.Text = "Boleto A4";
            this.Rb_BoletoA4.UseVisualStyleBackColor = true;
            // 
            // BoxRecibo
            // 
            this.BoxRecibo.BackColor = System.Drawing.Color.Transparent;
            this.BoxRecibo.Controls.Add(this.Rb_Nao);
            this.BoxRecibo.Controls.Add(this.Rb_Sim);
            this.BoxRecibo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxRecibo.Location = new System.Drawing.Point(12, 38);
            this.BoxRecibo.Name = "BoxRecibo";
            this.BoxRecibo.Size = new System.Drawing.Size(147, 41);
            this.BoxRecibo.TabIndex = 70;
            this.BoxRecibo.TabStop = false;
            this.BoxRecibo.Text = "Recibo do Valor Total";
            // 
            // Rb_Nao
            // 
            this.Rb_Nao.AutoSize = true;
            this.Rb_Nao.Location = new System.Drawing.Point(80, 18);
            this.Rb_Nao.Name = "Rb_Nao";
            this.Rb_Nao.Size = new System.Drawing.Size(48, 17);
            this.Rb_Nao.TabIndex = 1;
            this.Rb_Nao.TabStop = true;
            this.Rb_Nao.Text = "Nâo";
            this.Rb_Nao.UseVisualStyleBackColor = true;
            // 
            // Rb_Sim
            // 
            this.Rb_Sim.AutoSize = true;
            this.Rb_Sim.Location = new System.Drawing.Point(7, 18);
            this.Rb_Sim.Name = "Rb_Sim";
            this.Rb_Sim.Size = new System.Drawing.Size(45, 17);
            this.Rb_Sim.TabIndex = 0;
            this.Rb_Sim.TabStop = true;
            this.Rb_Sim.Text = "Sim";
            this.Rb_Sim.UseVisualStyleBackColor = true;
            // 
            // FrmImpBoleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(329, 125);
            this.Controls.Add(this.BoxRecibo);
            this.Controls.Add(this.BoxBoleto);
            this.Controls.Add(this.BtnImprimir);
            this.Controls.Add(this.TxtIdVenda);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImpBoleto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Imprimir Boleto de Venda";
            this.Load += new System.EventHandler(this.FrmImpBoleto_Load);
            this.Shown += new System.EventHandler(this.FrmImpBoleto_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.TxtIdVenda)).EndInit();
            this.BoxBoleto.ResumeLayout(false);
            this.BoxBoleto.PerformLayout();
            this.BoxRecibo.ResumeLayout(false);
            this.BoxRecibo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown TxtIdVenda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.GroupBox BoxBoleto;
        private System.Windows.Forms.RadioButton Rb_BoletoForm;
        private System.Windows.Forms.RadioButton Rb_BoletoA4;
        private System.Windows.Forms.GroupBox BoxRecibo;
        private System.Windows.Forms.RadioButton Rb_Nao;
        private System.Windows.Forms.RadioButton Rb_Sim;
    }
}