namespace ERP_SGE
{
    partial class FrmRelatorios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelatorios));
            this.cryRepRelatorio = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cryRepRelatorio
            // 
            this.cryRepRelatorio.ActiveViewIndex = -1;
            this.cryRepRelatorio.AutoScroll = true;
            this.cryRepRelatorio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cryRepRelatorio.Cursor = System.Windows.Forms.Cursors.Default;
            this.cryRepRelatorio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cryRepRelatorio.Location = new System.Drawing.Point(0, 0);
            this.cryRepRelatorio.Name = "cryRepRelatorio";
            this.cryRepRelatorio.PrintMode = CrystalDecisions.Windows.Forms.PrintMode.PrintOutputController;
            this.cryRepRelatorio.SelectionFormula = "";
            this.cryRepRelatorio.ShowGroupTreeButton = false;
            this.cryRepRelatorio.ShowParameterPanelButton = false;
            this.cryRepRelatorio.ShowRefreshButton = false;
            this.cryRepRelatorio.Size = new System.Drawing.Size(690, 397);
            this.cryRepRelatorio.TabIndex = 0;
            this.cryRepRelatorio.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.cryRepRelatorio.ViewTimeSelectionFormula = "";
            this.cryRepRelatorio.Load += new System.EventHandler(this.cryRepRelatorio_Load);
            // 
            // FrmRelatorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(690, 397);
            this.Controls.Add(this.cryRepRelatorio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRelatorios";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visualização dos Relatórios";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer cryRepRelatorio;





    }
}