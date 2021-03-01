namespace GerenciarImpResumida
{
    partial class FrmGerencial
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGerencial));
            this.Icone = new System.Windows.Forms.NotifyIcon(this.components);
            this.Tempo = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Icone
            // 
            this.Icone.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Icone.BalloonTipText = "Impressão Resumida";
            this.Icone.BalloonTipTitle = "Impressão Resumida";
            this.Icone.Icon = ((System.Drawing.Icon)(resources.GetObject("Icone.Icon")));
            this.Icone.Text = "Impressão Resumida";
            this.Icone.Visible = true;
            // 
            // Tempo
            // 
            this.Tempo.Tick += new System.EventHandler(this.Tempo_Tick);
            // 
            // FrmGerencial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 107);
            this.Name = "FrmGerencial";
            this.Text = "Gerenciador de Impressão Resumida";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.FrmGerencial_Load);
            this.Shown += new System.EventHandler(this.FrmGerencial_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon Icone;
        private System.Windows.Forms.Timer Tempo;
    }
}

