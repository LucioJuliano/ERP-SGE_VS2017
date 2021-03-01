namespace AtualizarERP_SGE
{
    partial class FrmAtualizar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAtualizar));
            this.label1 = new System.Windows.Forms.Label();
            this.BarProc = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.LblArq = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Atualizando a Versão do Sistema";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BarProc
            // 
            this.BarProc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BarProc.Location = new System.Drawing.Point(0, 58);
            this.BarProc.Name = "BarProc";
            this.BarProc.Size = new System.Drawing.Size(371, 23);
            this.BarProc.Step = 1;
            this.BarProc.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 83;
            this.label2.Text = "Copiando o Arquivo:";
            // 
            // LblArq
            // 
            this.LblArq.AutoSize = true;
            this.LblArq.BackColor = System.Drawing.Color.Transparent;
            this.LblArq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblArq.ForeColor = System.Drawing.Color.Maroon;
            this.LblArq.Location = new System.Drawing.Point(135, 35);
            this.LblArq.Name = "LblArq";
            this.LblArq.Size = new System.Drawing.Size(54, 15);
            this.LblArq.TabIndex = 84;
            this.LblArq.Text = "Arquivo";
            // 
            // FrmAtualizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(371, 81);
            this.Controls.Add(this.LblArq);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BarProc);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAtualizar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualização do Sistema ";
            this.Load += new System.EventHandler(this.FrmAtualizar_Load);
            this.Shown += new System.EventHandler(this.FrmAtualizar_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar BarProc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblArq;
    }
}

