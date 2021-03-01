namespace FrenteLoja
{
    partial class FrmCancelarItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCancelarItem));
            this.LblNomeMov = new System.Windows.Forms.Label();
            this.TxtNumItem = new System.Windows.Forms.TextBox();
            this.BtnCancMov = new System.Windows.Forms.Button();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblNomeMov
            // 
            this.LblNomeMov.AutoSize = true;
            this.LblNomeMov.BackColor = System.Drawing.Color.Transparent;
            this.LblNomeMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNomeMov.Location = new System.Drawing.Point(1, 18);
            this.LblNomeMov.Name = "LblNomeMov";
            this.LblNomeMov.Size = new System.Drawing.Size(110, 26);
            this.LblNomeMov.TabIndex = 138;
            this.LblNomeMov.Text = "No. Item:";
            this.LblNomeMov.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtNumItem
            // 
            this.TxtNumItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNumItem.Location = new System.Drawing.Point(107, 18);
            this.TxtNumItem.Name = "TxtNumItem";
            this.TxtNumItem.Size = new System.Drawing.Size(75, 26);
            this.TxtNumItem.TabIndex = 139;
            this.TxtNumItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumItem_KeyPress);
            // 
            // BtnCancMov
            // 
            this.BtnCancMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancMov.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancMov.Image")));
            this.BtnCancMov.Location = new System.Drawing.Point(156, 66);
            this.BtnCancMov.Name = "BtnCancMov";
            this.BtnCancMov.Size = new System.Drawing.Size(110, 25);
            this.BtnCancMov.TabIndex = 141;
            this.BtnCancMov.Text = "Cancelar";
            this.BtnCancMov.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancMov.UseVisualStyleBackColor = true;
            this.BtnCancMov.Click += new System.EventHandler(this.BtnCancMov_Click);
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.Image")));
            this.BtnConfirmar.Location = new System.Drawing.Point(31, 66);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(110, 25);
            this.BtnConfirmar.TabIndex = 140;
            this.BtnConfirmar.Text = "Confirmar";
            this.BtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // FrmCancelarItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(279, 105);
            this.Controls.Add(this.BtnCancMov);
            this.Controls.Add(this.BtnConfirmar);
            this.Controls.Add(this.TxtNumItem);
            this.Controls.Add(this.LblNomeMov);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCancelarItem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancelamento de Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label LblNomeMov;
        private System.Windows.Forms.TextBox TxtNumItem;
        public System.Windows.Forms.Button BtnCancMov;
        public System.Windows.Forms.Button BtnConfirmar;
    }
}