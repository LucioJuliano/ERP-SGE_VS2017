namespace ERP_SGE
{
    partial class FrmCadAtividades
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("RAMO DE ATIVIDADE");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadAtividades));
            this.LstContas = new System.Windows.Forms.TreeView();
            this.Imagens = new System.Windows.Forms.ImageList(this.components);
            this.BtnExc = new System.Windows.Forms.Button();
            this.BtnInc = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LstContas
            // 
            this.LstContas.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.LstContas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstContas.ImageIndex = 0;
            this.LstContas.ImageList = this.Imagens;
            this.LstContas.LabelEdit = true;
            this.LstContas.Location = new System.Drawing.Point(0, 0);
            this.LstContas.Name = "LstContas";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "Node_Receita";
            treeNode1.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode1.Tag = "R";
            treeNode1.Text = "RAMO DE ATIVIDADE";
            this.LstContas.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.LstContas.SelectedImageIndex = 0;
            this.LstContas.Size = new System.Drawing.Size(411, 417);
            this.LstContas.TabIndex = 7;
            this.LstContas.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LstContas_AfterLabelEdit);
            // 
            // Imagens
            // 
            this.Imagens.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Imagens.ImageStream")));
            this.Imagens.TransparentColor = System.Drawing.Color.Transparent;
            this.Imagens.Images.SetKeyName(0, "Sign 08-007.png");
            this.Imagens.Images.SetKeyName(1, "Sign 23-002.png");
            this.Imagens.Images.SetKeyName(2, "Sign 22-002.png");
            this.Imagens.Images.SetKeyName(3, "Sign 26-006.png");
            this.Imagens.Images.SetKeyName(4, "Sign 25-006.png");
            // 
            // BtnExc
            // 
            this.BtnExc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExc.ForeColor = System.Drawing.Color.Maroon;
            this.BtnExc.Image = ((System.Drawing.Image)(resources.GetObject("BtnExc.Image")));
            this.BtnExc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnExc.Location = new System.Drawing.Point(227, 12);
            this.BtnExc.Name = "BtnExc";
            this.BtnExc.Size = new System.Drawing.Size(162, 30);
            this.BtnExc.TabIndex = 13;
            this.BtnExc.Text = "Excluir Atividade";
            this.BtnExc.UseVisualStyleBackColor = true;
            this.BtnExc.Click += new System.EventHandler(this.BtnExc_Click);
            // 
            // BtnInc
            // 
            this.BtnInc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnInc.ForeColor = System.Drawing.Color.Maroon;
            this.BtnInc.Image = ((System.Drawing.Image)(resources.GetObject("BtnInc.Image")));
            this.BtnInc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnInc.Location = new System.Drawing.Point(22, 12);
            this.BtnInc.Name = "BtnInc";
            this.BtnInc.Size = new System.Drawing.Size(162, 30);
            this.BtnInc.TabIndex = 12;
            this.BtnInc.Text = "Nova Atividade";
            this.BtnInc.UseVisualStyleBackColor = true;
            this.BtnInc.Click += new System.EventHandler(this.BtnInc_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BtnExc);
            this.panel1.Controls.Add(this.BtnInc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 48);
            this.panel1.TabIndex = 8;
            // 
            // FrmCadAtividades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 465);
            this.Controls.Add(this.LstContas);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCadAtividades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro dos Ramos de Atividades";
            this.Load += new System.EventHandler(this.FrmCadAtivides_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView LstContas;
        private System.Windows.Forms.ImageList Imagens;
        private System.Windows.Forms.Button BtnExc;
        private System.Windows.Forms.Button BtnInc;
        private System.Windows.Forms.Panel panel1;

    }
}