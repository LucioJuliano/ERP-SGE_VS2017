namespace ERP_SGE
{
    partial class FrmPDV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPDV));
            this.label1 = new System.Windows.Forms.Label();
            this.NumPedido = new System.Windows.Forms.NumericUpDown();
            this.BtnCupom = new System.Windows.Forms.Button();
            this.BtnConcluir = new System.Windows.Forms.Button();
            this.BtnLanc = new System.Windows.Forms.Button();
            this.BtnCancCupom = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.BtnImpRecibo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumPedido)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº do Pedido:";
            // 
            // NumPedido
            // 
            this.NumPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPedido.Location = new System.Drawing.Point(95, 12);
            this.NumPedido.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.NumPedido.Name = "NumPedido";
            this.NumPedido.Size = new System.Drawing.Size(73, 21);
            this.NumPedido.TabIndex = 1;
            this.NumPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnCupom
            // 
            this.BtnCupom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCupom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCupom.Image = ((System.Drawing.Image)(resources.GetObject("BtnCupom.Image")));
            this.BtnCupom.Location = new System.Drawing.Point(12, 88);
            this.BtnCupom.Name = "BtnCupom";
            this.BtnCupom.Size = new System.Drawing.Size(156, 30);
            this.BtnCupom.TabIndex = 69;
            this.BtnCupom.Text = "Imprimir Cupom Fiscal";
            this.BtnCupom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCupom.UseVisualStyleBackColor = true;
            this.BtnCupom.Click += new System.EventHandler(this.BtnCupom_Click);
            // 
            // BtnConcluir
            // 
            this.BtnConcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConcluir.Image = ((System.Drawing.Image)(resources.GetObject("BtnConcluir.Image")));
            this.BtnConcluir.Location = new System.Drawing.Point(12, 52);
            this.BtnConcluir.Name = "BtnConcluir";
            this.BtnConcluir.Size = new System.Drawing.Size(156, 30);
            this.BtnConcluir.TabIndex = 67;
            this.BtnConcluir.Text = "Concluir Pedido";
            this.BtnConcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConcluir.UseVisualStyleBackColor = true;
            this.BtnConcluir.Click += new System.EventHandler(this.BtnConcluir_Click);
            // 
            // BtnLanc
            // 
            this.BtnLanc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnLanc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLanc.Image = ((System.Drawing.Image)(resources.GetObject("BtnLanc.Image")));
            this.BtnLanc.Location = new System.Drawing.Point(186, 88);
            this.BtnLanc.Name = "BtnLanc";
            this.BtnLanc.Size = new System.Drawing.Size(156, 30);
            this.BtnLanc.TabIndex = 70;
            this.BtnLanc.Text = "Despesas e Receitas";
            this.BtnLanc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLanc.UseVisualStyleBackColor = true;
            this.BtnLanc.Click += new System.EventHandler(this.BtnLanc_Click);
            // 
            // BtnCancCupom
            // 
            this.BtnCancCupom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancCupom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancCupom.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancCupom.Image")));
            this.BtnCancCupom.Location = new System.Drawing.Point(186, 52);
            this.BtnCancCupom.Name = "BtnCancCupom";
            this.BtnCancCupom.Size = new System.Drawing.Size(156, 30);
            this.BtnCancCupom.TabIndex = 71;
            this.BtnCancCupom.Text = "Cancelar Cupom Fiscal";
            this.BtnCancCupom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancCupom.UseVisualStyleBackColor = true;
            this.BtnCancCupom.Click += new System.EventHandler(this.BtnCancCupom_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(174, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 72;
            this.label2.Text = "Data do Caixa:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(273, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(95, 20);
            this.dateTimePicker1.TabIndex = 73;
            // 
            // BtnImpRecibo
            // 
            this.BtnImpRecibo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnImpRecibo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImpRecibo.Image = ((System.Drawing.Image)(resources.GetObject("BtnImpRecibo.Image")));
            this.BtnImpRecibo.Location = new System.Drawing.Point(12, 142);
            this.BtnImpRecibo.Name = "BtnImpRecibo";
            this.BtnImpRecibo.Size = new System.Drawing.Size(89, 30);
            this.BtnImpRecibo.TabIndex = 74;
            this.BtnImpRecibo.Text = "Recibo";
            this.BtnImpRecibo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImpRecibo.UseVisualStyleBackColor = true;
            this.BtnImpRecibo.Click += new System.EventHandler(this.BtnImpRecibo_Click);
            // 
            // FrmPDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ERP_SGE.Properties.Resources.Bloco;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(379, 184);
            this.Controls.Add(this.BtnImpRecibo);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnCancCupom);
            this.Controls.Add(this.BtnLanc);
            this.Controls.Add(this.BtnCupom);
            this.Controls.Add(this.BtnConcluir);
            this.Controls.Add(this.NumPedido);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPDV";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDV - Caixa Balcão ";
            this.Load += new System.EventHandler(this.FrmPDV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumPedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NumPedido;
        private System.Windows.Forms.Button BtnCupom;
        public System.Windows.Forms.Button BtnConcluir;
        private System.Windows.Forms.Button BtnLanc;
        public System.Windows.Forms.Button BtnCancCupom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button BtnImpRecibo;
    }
}