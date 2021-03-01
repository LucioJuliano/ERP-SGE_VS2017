namespace FrenteLoja
{
    partial class FrmFrenteLoja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFrenteLoja));
            this.BarraBaixo = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BSta_NmUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BSta_Estacao = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BSta_Servidor = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BSta_Banco = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BSta_StaConexao = new System.Windows.Forms.ToolStripStatusLabel();
            this.BSta_BarProcesso = new System.Windows.Forms.ToolStripProgressBar();
            this.DivisorTela = new System.Windows.Forms.SplitContainer();
            this.PnlTroco = new System.Windows.Forms.Panel();
            this.LblTroco = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LblMensagem = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.LblRelCaixa = new System.Windows.Forms.Label();
            this.LblFecharVenda = new System.Windows.Forms.Label();
            this.LblAltPreco = new System.Windows.Forms.Label();
            this.LblCancItem = new System.Windows.Forms.Label();
            this.LblSuprimento = new System.Windows.Forms.Label();
            this.LblSangria = new System.Windows.Forms.Label();
            this.LblFecharCx = new System.Windows.Forms.Label();
            this.LblAbriCx = new System.Windows.Forms.Label();
            this.PnlVenda = new System.Windows.Forms.Panel();
            this.LblVendedor = new System.Windows.Forms.Label();
            this.LstVendedor = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtVlrDesc = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtVlrTotal = new System.Windows.Forms.NumericUpDown();
            this.TxtVlrUnt = new System.Windows.Forms.NumericUpDown();
            this.TxtCodBarra = new System.Windows.Forms.TextBox();
            this.TxtQtde = new System.Windows.Forms.NumericUpDown();
            this.TxtProduto = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblCliente = new System.Windows.Forms.Label();
            this.TxtCupom = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblNumItens = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblTotalVenda = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SelecionarImp = new System.Windows.Forms.PrintDialog();
            this.BarraBaixo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DivisorTela)).BeginInit();
            this.DivisorTela.Panel1.SuspendLayout();
            this.DivisorTela.Panel2.SuspendLayout();
            this.DivisorTela.SuspendLayout();
            this.PnlTroco.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PnlVenda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrUnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtQtde)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarraBaixo
            // 
            this.BarraBaixo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BarraBaixo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.BSta_NmUsuario,
            this.toolStripStatusLabel3,
            this.BSta_Estacao,
            this.toolStripStatusLabel2,
            this.BSta_Servidor,
            this.toolStripStatusLabel5,
            this.BSta_Banco,
            this.toolStripStatusLabel4,
            this.BSta_StaConexao,
            this.BSta_BarProcesso});
            this.BarraBaixo.Location = new System.Drawing.Point(0, 536);
            this.BarraBaixo.Name = "BarraBaixo";
            this.BarraBaixo.Size = new System.Drawing.Size(784, 26);
            this.BarraBaixo.TabIndex = 1;
            this.BarraBaixo.Text = "Barra_Status";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel1.Image")));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 21);
            this.toolStripStatusLabel1.Text = "Usuário:";
            // 
            // BSta_NmUsuario
            // 
            this.BSta_NmUsuario.AutoSize = false;
            this.BSta_NmUsuario.ForeColor = System.Drawing.Color.Maroon;
            this.BSta_NmUsuario.Name = "BSta_NmUsuario";
            this.BSta_NmUsuario.Size = new System.Drawing.Size(120, 21);
            this.BSta_NmUsuario.Text = "Administrador";
            this.BSta_NmUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel3.Image")));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(67, 21);
            this.toolStripStatusLabel3.Text = "Estação:";
            // 
            // BSta_Estacao
            // 
            this.BSta_Estacao.AutoSize = false;
            this.BSta_Estacao.ForeColor = System.Drawing.Color.Maroon;
            this.BSta_Estacao.Name = "BSta_Estacao";
            this.BSta_Estacao.Size = new System.Drawing.Size(120, 21);
            this.BSta_Estacao.Text = "1.0.0.0";
            this.BSta_Estacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel2.Image")));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(74, 21);
            this.toolStripStatusLabel2.Text = "Servidor:";
            // 
            // BSta_Servidor
            // 
            this.BSta_Servidor.AutoSize = false;
            this.BSta_Servidor.ForeColor = System.Drawing.Color.Maroon;
            this.BSta_Servidor.Name = "BSta_Servidor";
            this.BSta_Servidor.Size = new System.Drawing.Size(120, 21);
            this.BSta_Servidor.Text = " Servidor_NET";
            this.BSta_Servidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel5.Image")));
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(112, 21);
            this.toolStripStatusLabel5.Text = "Banco de dados:";
            // 
            // BSta_Banco
            // 
            this.BSta_Banco.AutoSize = false;
            this.BSta_Banco.ForeColor = System.Drawing.Color.Maroon;
            this.BSta_Banco.Name = "BSta_Banco";
            this.BSta_Banco.Size = new System.Drawing.Size(120, 21);
            this.BSta_Banco.Text = "DB";
            this.BSta_Banco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel4.Image")));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(112, 16);
            this.toolStripStatusLabel4.Text = "Status Conexão:";
            // 
            // BSta_StaConexao
            // 
            this.BSta_StaConexao.AutoSize = false;
            this.BSta_StaConexao.ForeColor = System.Drawing.Color.Maroon;
            this.BSta_StaConexao.Name = "BSta_StaConexao";
            this.BSta_StaConexao.Size = new System.Drawing.Size(90, 21);
            this.BSta_StaConexao.Text = "Desconectado";
            this.BSta_StaConexao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BSta_BarProcesso
            // 
            this.BSta_BarProcesso.Name = "BSta_BarProcesso";
            this.BSta_BarProcesso.Size = new System.Drawing.Size(100, 20);
            this.BSta_BarProcesso.Step = 1;
            // 
            // DivisorTela
            // 
            this.DivisorTela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DivisorTela.Location = new System.Drawing.Point(0, 0);
            this.DivisorTela.Name = "DivisorTela";
            // 
            // DivisorTela.Panel1
            // 
            this.DivisorTela.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.DivisorTela.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DivisorTela.Panel1.BackgroundImage")));
            this.DivisorTela.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DivisorTela.Panel1.Controls.Add(this.PnlTroco);
            this.DivisorTela.Panel1.Controls.Add(this.LblMensagem);
            this.DivisorTela.Panel1.Controls.Add(this.panel2);
            this.DivisorTela.Panel1.Controls.Add(this.PnlVenda);
            this.DivisorTela.Panel1.Controls.Add(this.LblCliente);
            this.DivisorTela.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // DivisorTela.Panel2
            // 
            this.DivisorTela.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.DivisorTela.Panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DivisorTela.Panel2.BackgroundImage")));
            this.DivisorTela.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DivisorTela.Panel2.Controls.Add(this.TxtCupom);
            this.DivisorTela.Panel2.Controls.Add(this.label1);
            this.DivisorTela.Panel2.Controls.Add(this.panel1);
            this.DivisorTela.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DivisorTela.Size = new System.Drawing.Size(784, 536);
            this.DivisorTela.SplitterDistance = 507;
            this.DivisorTela.TabIndex = 3;
            // 
            // PnlTroco
            // 
            this.PnlTroco.BackColor = System.Drawing.Color.Transparent;
            this.PnlTroco.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PnlTroco.BackgroundImage")));
            this.PnlTroco.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlTroco.Controls.Add(this.LblTroco);
            this.PnlTroco.Controls.Add(this.label12);
            this.PnlTroco.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTroco.Location = new System.Drawing.Point(0, 129);
            this.PnlTroco.Name = "PnlTroco";
            this.PnlTroco.Size = new System.Drawing.Size(507, 104);
            this.PnlTroco.TabIndex = 5;
            this.PnlTroco.Visible = false;
            // 
            // LblTroco
            // 
            this.LblTroco.BackColor = System.Drawing.Color.Gray;
            this.LblTroco.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTroco.Location = new System.Drawing.Point(174, 54);
            this.LblTroco.Name = "LblTroco";
            this.LblTroco.Size = new System.Drawing.Size(192, 39);
            this.LblTroco.TabIndex = 1;
            this.LblTroco.Text = "0,00";
            this.LblTroco.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(178, 39);
            this.label12.TabIndex = 0;
            this.label12.Text = "Troco R$:";
            // 
            // LblMensagem
            // 
            this.LblMensagem.BackColor = System.Drawing.Color.Transparent;
            this.LblMensagem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblMensagem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LblMensagem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblMensagem.Font = new System.Drawing.Font("Arial", 32F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensagem.Location = new System.Drawing.Point(0, 378);
            this.LblMensagem.Name = "LblMensagem";
            this.LblMensagem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblMensagem.Size = new System.Drawing.Size(507, 81);
            this.LblMensagem.TabIndex = 4;
            this.LblMensagem.Text = "Caixa Fechado";
            this.LblMensagem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.LblRelCaixa);
            this.panel2.Controls.Add(this.LblFecharVenda);
            this.panel2.Controls.Add(this.LblAltPreco);
            this.panel2.Controls.Add(this.LblCancItem);
            this.panel2.Controls.Add(this.LblSuprimento);
            this.panel2.Controls.Add(this.LblSangria);
            this.panel2.Controls.Add(this.LblFecharCx);
            this.panel2.Controls.Add(this.LblAbriCx);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 459);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(507, 77);
            this.panel2.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(289, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(205, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Alt+ I - Importar Venda Retaguarda";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(116, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(148, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Alt+ V - Consulta Vendas";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(3, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Alt+ C - Clientes";
            // 
            // LblRelCaixa
            // 
            this.LblRelCaixa.AutoSize = true;
            this.LblRelCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRelCaixa.ForeColor = System.Drawing.Color.Maroon;
            this.LblRelCaixa.Location = new System.Drawing.Point(346, 20);
            this.LblRelCaixa.Name = "LblRelCaixa";
            this.LblRelCaixa.Size = new System.Drawing.Size(123, 13);
            this.LblRelCaixa.TabIndex = 7;
            this.LblRelCaixa.Text = "F9 - Cancelar Venda";
            // 
            // LblFecharVenda
            // 
            this.LblFecharVenda.AutoSize = true;
            this.LblFecharVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFecharVenda.ForeColor = System.Drawing.Color.Maroon;
            this.LblFecharVenda.Location = new System.Drawing.Point(346, 5);
            this.LblFecharVenda.Name = "LblFecharVenda";
            this.LblFecharVenda.Size = new System.Drawing.Size(112, 13);
            this.LblFecharVenda.TabIndex = 6;
            this.LblFecharVenda.Text = "F8 - Fechar Venda";
            // 
            // LblAltPreco
            // 
            this.LblAltPreco.AutoSize = true;
            this.LblAltPreco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAltPreco.ForeColor = System.Drawing.Color.Maroon;
            this.LblAltPreco.Location = new System.Drawing.Point(230, 20);
            this.LblAltPreco.Name = "LblAltPreco";
            this.LblAltPreco.Size = new System.Drawing.Size(107, 13);
            this.LblAltPreco.TabIndex = 5;
            this.LblAltPreco.Text = "F7 - Alterar Preço";
            // 
            // LblCancItem
            // 
            this.LblCancItem.AutoSize = true;
            this.LblCancItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCancItem.ForeColor = System.Drawing.Color.Maroon;
            this.LblCancItem.Location = new System.Drawing.Point(230, 5);
            this.LblCancItem.Name = "LblCancItem";
            this.LblCancItem.Size = new System.Drawing.Size(111, 13);
            this.LblCancItem.TabIndex = 4;
            this.LblCancItem.Text = "F6 - Cancelar Item";
            // 
            // LblSuprimento
            // 
            this.LblSuprimento.AutoSize = true;
            this.LblSuprimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSuprimento.ForeColor = System.Drawing.Color.Maroon;
            this.LblSuprimento.Location = new System.Drawing.Point(116, 20);
            this.LblSuprimento.Name = "LblSuprimento";
            this.LblSuprimento.Size = new System.Drawing.Size(96, 13);
            this.LblSuprimento.TabIndex = 3;
            this.LblSuprimento.Text = "F5 - Suprimento";
            // 
            // LblSangria
            // 
            this.LblSangria.AutoSize = true;
            this.LblSangria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSangria.ForeColor = System.Drawing.Color.Maroon;
            this.LblSangria.Location = new System.Drawing.Point(116, 5);
            this.LblSangria.Name = "LblSangria";
            this.LblSangria.Size = new System.Drawing.Size(76, 13);
            this.LblSangria.TabIndex = 2;
            this.LblSangria.Text = "F4 - Sangria";
            // 
            // LblFecharCx
            // 
            this.LblFecharCx.AutoSize = true;
            this.LblFecharCx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFecharCx.ForeColor = System.Drawing.Color.Maroon;
            this.LblFecharCx.Location = new System.Drawing.Point(3, 20);
            this.LblFecharCx.Name = "LblFecharCx";
            this.LblFecharCx.Size = new System.Drawing.Size(107, 13);
            this.LblFecharCx.TabIndex = 1;
            this.LblFecharCx.Text = "F3 - Fechar Caixa";
            // 
            // LblAbriCx
            // 
            this.LblAbriCx.AutoSize = true;
            this.LblAbriCx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAbriCx.ForeColor = System.Drawing.Color.Maroon;
            this.LblAbriCx.Location = new System.Drawing.Point(3, 5);
            this.LblAbriCx.Name = "LblAbriCx";
            this.LblAbriCx.Size = new System.Drawing.Size(94, 13);
            this.LblAbriCx.TabIndex = 0;
            this.LblAbriCx.Text = "F2 - Abrir Caixa";
            // 
            // PnlVenda
            // 
            this.PnlVenda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlVenda.Controls.Add(this.LblVendedor);
            this.PnlVenda.Controls.Add(this.LstVendedor);
            this.PnlVenda.Controls.Add(this.label10);
            this.PnlVenda.Controls.Add(this.label3);
            this.PnlVenda.Controls.Add(this.TxtVlrDesc);
            this.PnlVenda.Controls.Add(this.label8);
            this.PnlVenda.Controls.Add(this.TxtVlrTotal);
            this.PnlVenda.Controls.Add(this.TxtVlrUnt);
            this.PnlVenda.Controls.Add(this.TxtCodBarra);
            this.PnlVenda.Controls.Add(this.TxtQtde);
            this.PnlVenda.Controls.Add(this.TxtProduto);
            this.PnlVenda.Controls.Add(this.label9);
            this.PnlVenda.Controls.Add(this.label7);
            this.PnlVenda.Controls.Add(this.label6);
            this.PnlVenda.Controls.Add(this.label5);
            this.PnlVenda.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlVenda.Enabled = false;
            this.PnlVenda.Location = new System.Drawing.Point(0, 29);
            this.PnlVenda.Name = "PnlVenda";
            this.PnlVenda.Size = new System.Drawing.Size(507, 100);
            this.PnlVenda.TabIndex = 2;
            // 
            // LblVendedor
            // 
            this.LblVendedor.AutoSize = true;
            this.LblVendedor.BackColor = System.Drawing.Color.Transparent;
            this.LblVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.LblVendedor.Location = new System.Drawing.Point(2, 5);
            this.LblVendedor.Name = "LblVendedor";
            this.LblVendedor.Size = new System.Drawing.Size(65, 13);
            this.LblVendedor.TabIndex = 152;
            this.LblVendedor.Text = "Vendedor:";
            this.LblVendedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LstVendedor
            // 
            this.LstVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstVendedor.FormattingEnabled = true;
            this.LstVendedor.Items.AddRange(new object[] {
            "CNPJ ou CPF",
            "Razão Social",
            "Nome Fantasia"});
            this.LstVendedor.Location = new System.Drawing.Point(73, 1);
            this.LstVendedor.Name = "LstVendedor";
            this.LstVendedor.Size = new System.Drawing.Size(263, 21);
            this.LstVendedor.TabIndex = 151;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(343, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 31);
            this.label10.TabIndex = 48;
            this.label10.Text = "=";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(218, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 31);
            this.label3.TabIndex = 47;
            this.label3.Text = "x";
            // 
            // TxtVlrDesc
            // 
            this.TxtVlrDesc.DecimalPlaces = 2;
            this.TxtVlrDesc.Enabled = false;
            this.TxtVlrDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVlrDesc.Location = new System.Drawing.Point(407, 76);
            this.TxtVlrDesc.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrDesc.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.TxtVlrDesc.Name = "TxtVlrDesc";
            this.TxtVlrDesc.Size = new System.Drawing.Size(82, 22);
            this.TxtVlrDesc.TabIndex = 45;
            this.TxtVlrDesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrDesc.ThousandsSeparator = true;
            this.TxtVlrDesc.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.TxtVlrDesc.Visible = false;
            this.TxtVlrDesc.Validated += new System.EventHandler(this.TxtVlrDesc_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(319, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Desc. R$";
            this.label8.Visible = false;
            // 
            // TxtVlrTotal
            // 
            this.TxtVlrTotal.DecimalPlaces = 2;
            this.TxtVlrTotal.Enabled = false;
            this.TxtVlrTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVlrTotal.Location = new System.Drawing.Point(375, 49);
            this.TxtVlrTotal.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrTotal.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.TxtVlrTotal.Name = "TxtVlrTotal";
            this.TxtVlrTotal.Size = new System.Drawing.Size(94, 22);
            this.TxtVlrTotal.TabIndex = 46;
            this.TxtVlrTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrTotal.ThousandsSeparator = true;
            this.TxtVlrTotal.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // TxtVlrUnt
            // 
            this.TxtVlrUnt.DecimalPlaces = 2;
            this.TxtVlrUnt.Enabled = false;
            this.TxtVlrUnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVlrUnt.Location = new System.Drawing.Point(252, 49);
            this.TxtVlrUnt.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtVlrUnt.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.TxtVlrUnt.Name = "TxtVlrUnt";
            this.TxtVlrUnt.Size = new System.Drawing.Size(84, 22);
            this.TxtVlrUnt.TabIndex = 44;
            this.TxtVlrUnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVlrUnt.ThousandsSeparator = true;
            this.TxtVlrUnt.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.TxtVlrUnt.Validated += new System.EventHandler(this.TxtVlrUnt_Validated);
            // 
            // TxtCodBarra
            // 
            this.TxtCodBarra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCodBarra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCodBarra.Location = new System.Drawing.Point(3, 49);
            this.TxtCodBarra.Name = "TxtCodBarra";
            this.TxtCodBarra.Size = new System.Drawing.Size(121, 22);
            this.TxtCodBarra.TabIndex = 43;
            this.TxtCodBarra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodBarra_KeyPress);
            // 
            // TxtQtde
            // 
            this.TxtQtde.DecimalPlaces = 3;
            this.TxtQtde.Enabled = false;
            this.TxtQtde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtQtde.Location = new System.Drawing.Point(130, 49);
            this.TxtQtde.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.TxtQtde.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.TxtQtde.Name = "TxtQtde";
            this.TxtQtde.Size = new System.Drawing.Size(82, 22);
            this.TxtQtde.TabIndex = 42;
            this.TxtQtde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtQtde.ThousandsSeparator = true;
            this.TxtQtde.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.TxtQtde.Value = new decimal(new int[] {
            1000,
            0,
            0,
            196608});
            this.TxtQtde.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtQtde_KeyPress);
            // 
            // TxtProduto
            // 
            this.TxtProduto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TxtProduto.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProduto.Location = new System.Drawing.Point(0, 76);
            this.TxtProduto.Name = "TxtProduto";
            this.TxtProduto.Size = new System.Drawing.Size(503, 20);
            this.TxtProduto.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(383, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "Vlr. Total R$";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(252, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Prç. Unit.R$";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Código Barra";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(131, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Quantidade";
            // 
            // LblCliente
            // 
            this.LblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblCliente.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCliente.Location = new System.Drawing.Point(0, 0);
            this.LblCliente.Name = "LblCliente";
            this.LblCliente.Size = new System.Drawing.Size(507, 29);
            this.LblCliente.TabIndex = 1;
            this.LblCliente.Text = "Cliente: ";
            // 
            // TxtCupom
            // 
            this.TxtCupom.BackColor = System.Drawing.Color.White;
            this.TxtCupom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtCupom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtCupom.Font = new System.Drawing.Font("Courier New", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCupom.HideSelection = false;
            this.TxtCupom.Location = new System.Drawing.Point(0, 29);
            this.TxtCupom.Name = "TxtCupom";
            this.TxtCupom.ReadOnly = true;
            this.TxtCupom.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TxtCupom.Size = new System.Drawing.Size(273, 375);
            this.TxtCupom.TabIndex = 9;
            this.TxtCupom.TabStop = false;
            this.TxtCupom.Text = "";
            this.TxtCupom.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cupom Fiscal";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.LblNumItens);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.LblTotalVenda);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 404);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 132);
            this.panel1.TabIndex = 0;
            // 
            // LblNumItens
            // 
            this.LblNumItens.BackColor = System.Drawing.Color.Gray;
            this.LblNumItens.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumItens.Location = new System.Drawing.Point(119, 9);
            this.LblNumItens.Name = "LblNumItens";
            this.LblNumItens.Size = new System.Drawing.Size(142, 31);
            this.LblNumItens.TabIndex = 3;
            this.LblNumItens.Text = "0";
            this.LblNumItens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 29);
            this.label4.TabIndex = 2;
            this.label4.Text = "No. Itens:";
            // 
            // LblTotalVenda
            // 
            this.LblTotalVenda.BackColor = System.Drawing.Color.Gray;
            this.LblTotalVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalVenda.Location = new System.Drawing.Point(119, 53);
            this.LblTotalVenda.Name = "LblTotalVenda";
            this.LblTotalVenda.Size = new System.Drawing.Size(142, 31);
            this.LblTotalVenda.TabIndex = 1;
            this.LblTotalVenda.Text = "0,00";
            this.LblTotalVenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total R$:";
            // 
            // SelecionarImp
            // 
            this.SelecionarImp.UseEXDialog = true;
            // 
            // FrmFrenteLoja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.DivisorTela);
            this.Controls.Add(this.BarraBaixo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmFrenteLoja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP  (SGE -Sistema de Gestão Empresarial) Versão  1.0 .NET - 2009   Copyright ©  " +
    "- Talimpo   (Frente de Loja)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFrenteLoja_FormClosing);
            this.Load += new System.EventHandler(this.FrmFrenteLoja_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmFrenteLoja_KeyDown);
            this.BarraBaixo.ResumeLayout(false);
            this.BarraBaixo.PerformLayout();
            this.DivisorTela.Panel1.ResumeLayout(false);
            this.DivisorTela.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DivisorTela)).EndInit();
            this.DivisorTela.ResumeLayout(false);
            this.PnlTroco.ResumeLayout(false);
            this.PnlTroco.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PnlVenda.ResumeLayout(false);
            this.PnlVenda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtVlrUnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtQtde)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip BarraBaixo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel BSta_NmUsuario;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel BSta_Estacao;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel BSta_Servidor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel BSta_Banco;
        public System.Windows.Forms.ToolStripProgressBar BSta_BarProcesso;
        private System.Windows.Forms.SplitContainer DivisorTela;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox TxtCupom;
        private System.Windows.Forms.Label LblTotalVenda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel BSta_StaConexao;
        private System.Windows.Forms.Label LblNumItens;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel PnlVenda;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label TxtProduto;
        private System.Windows.Forms.NumericUpDown TxtVlrDesc;
        private System.Windows.Forms.NumericUpDown TxtVlrTotal;
        private System.Windows.Forms.Label LblMensagem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LblAbriCx;
        private System.Windows.Forms.Label LblSangria;
        private System.Windows.Forms.Label LblFecharCx;
        private System.Windows.Forms.Label LblSuprimento;
        private System.Windows.Forms.Label LblFecharVenda;
        private System.Windows.Forms.Label LblAltPreco;
        private System.Windows.Forms.Label LblCancItem;
        private System.Windows.Forms.Label LblRelCaixa;
        private System.Windows.Forms.Panel PnlTroco;
        private System.Windows.Forms.Label LblTroco;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblVendedor;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox TxtCodBarra;
        public System.Windows.Forms.NumericUpDown TxtQtde;
        public System.Windows.Forms.NumericUpDown TxtVlrUnt;
        public System.Windows.Forms.Label LblCliente;
        public System.Windows.Forms.ComboBox LstVendedor;
        public System.Windows.Forms.PrintDialog SelecionarImp;
    }
}