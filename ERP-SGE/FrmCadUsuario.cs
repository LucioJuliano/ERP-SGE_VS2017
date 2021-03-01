using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controles;
using Controle_Dados;
using System.Data.SqlClient;
namespace ERP_SGE
{
    public partial class FrmCadUsuario : Form
    {
        Funcoes Controle     = new Funcoes();
        Usuarios CadUsuarios = new Usuarios();        
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadUsuario()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            CadUsuarios.Controle = Controle;            
            CadUsuarios.IdUsuario = 0;
            ChkNome.Checked = true;
            PopularGrid();
            MontaOpcoes();
            //            
            CK_EmailAltPrd.Visible      = !FrmPrincipal.VersaoDistribuidor;
            CK_EnviarFinanceiro.Visible = !FrmPrincipal.VersaoDistribuidor;
            CK_AltInstalacao.Visible    = !FrmPrincipal.VersaoDistribuidor;
            CK_EmailAltPrd.Visible      = !FrmPrincipal.VersaoDistribuidor;
            CK_CadDistrib.Visible       = !FrmPrincipal.VersaoDistribuidor;
            Ck_VerSldDeposito.Visible   = !FrmPrincipal.VersaoDistribuidor;
            LstPromocao                 = FrmPrincipal.PopularCombo("SELECT Id_Promocao,Substring(Descricao,1,60) as Descricao FROM PromocaoProdutos ORDER BY Descricao", LstPromocao, "Nenhuma");
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Usuario,Usuario FROM Usuarios ORDER BY Usuario");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Usuario", CadUsuarios.IdUsuario);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            CadUsuarios.LerDados(Isn);
            TxtCodigo.Text = CadUsuarios.IdUsuario.ToString();
            TxtUsuario.Text = CadUsuarios.Usuario;
            TxtSenha.Text = CadUsuarios.Senha;
            TxtEmail.Text = CadUsuarios.Email;
            LstFilial.SelectedValue = CadUsuarios.IdFilial.ToString();
            LstVendedor.SelectedValue = CadUsuarios.IdVendedor.ToString();
            LstEntregador.SelectedValue = CadUsuarios.IdEntregador.ToString();
            LstPromocao.SelectedValue = CadUsuarios.IdPromocao.ToString();
            CK_SeusMov.Checked = CadUsuarios.SeusMov == 1;
            CK_SolicAutCanc.Checked = CadUsuarios.SolicAutCanc == 1;
            CK_LiberaDebito.Checked = CadUsuarios.LiberaDebito == 1;
            CK_LiberaEstoque.Checked = CadUsuarios.LiberaEstoque == 1;
            CK_LiberaPreco.Checked = CadUsuarios.LiberaPreco == 1;
            CK_Precos.Checked = CadUsuarios.MostraPreco == 1;
            CK_Faturamento.Checked = CadUsuarios.Faturamento == 1;
            CK_Instancia.Checked = CadUsuarios.MultplaInstancia == 1;
            CK_AltFinanceiro.Checked = CadUsuarios.AlteraFinanceiro== 1;
            CK_AltProduto.Checked = CadUsuarios.AlterarProduto == 1;
            CK_AltPessoa.Checked = CadUsuarios.AlterarPessoa == 1;
            CK_AltInstalacao.Checked = CadUsuarios.AlterarInstalacao == 1;
            CK_ImpResumido.Checked = CadUsuarios.ImpResumido == 1;
            CK_EmailAltPrd.Checked = CadUsuarios.EmailAltPrd == 1;
            CK_SemMovEst.Checked = CadUsuarios.SemMovEst == 1;
            CK_VerificarEstMin.Checked = CadUsuarios.VerificarEstMin == 1;
            CK_EnviarFinanceiro.Checked = CadUsuarios.EnviarFinanc == 1;
            CK_LimpaEstoque.Checked = CadUsuarios.LimpaEstoque == 1;
            CK_AtualizaEstoque.Checked = CadUsuarios.AtualizaEstoque == 1;
            CK_ExcluirReg.Checked = CadUsuarios.ExcluirReg == 1;
            CK_AtlzBD.Checked = CadUsuarios.AtlzBD == 1;
            CK_AtlItemVd.Checked = CadUsuarios.AltItemVD == 1;
            Ck_BloqDesc.Checked = CadUsuarios.BloqDesc == 1;
            Ck_Telemarketing.Checked = CadUsuarios.Telemarketing == 1;
            CK_CadDistrib.Checked = CadUsuarios.CadDistrib == 1;
            Ck_VerSldDeposito.Checked = CadUsuarios.VerSldDeposito == 1;
            Ck_AtivarProduto.Checked = CadUsuarios.AtivarProduto == 1;
            Ck_CancelarNF.Checked = CadUsuarios.CancelarNF == 1;
            Ck_CancVenda.Checked = CadUsuarios.CancVenda == 1;
            Ck_AlterarVenda.Checked = CadUsuarios.AlterarVenda == 1;
            Ck_CancAmostra.Checked = CadUsuarios.CancAmostra == 1;
            Ck_CancMovEst.Checked = CadUsuarios.CancMovEst == 1;
            Ck_AltSenha.Checked = CadUsuarios.AltSenha == 1;
            Ck_VendedorBalcao.Checked = CadUsuarios.VendedorBalcao == 1;
            Ck_PrcDistrib.Checked = CadUsuarios.PrcDistrib== 1;
            Ck_AutCadPF.Checked = CadUsuarios.AutCadPF == 1;
            Ck_UsuCaixaLj.Checked = CadUsuarios.UsuCaixaLj == 1;
            Ck_PrcCusto.Checked = CadUsuarios.LiberaPrcCusto == 1;
            Ck_MostraCustoVd.Checked = CadUsuarios.MostraCustoVd == 1;
            Ck_BxVdFrenteLj.Checked = CadUsuarios.BxVdFrenteLj == 1;
            Ck_PrcEspDist.Checked = CadUsuarios.UsaPrcEspDist == 1;
            LerAcesso();            
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {            
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtUsuario.Focus();
            BoxOpcao.Enabled = false;
            StaFormEdicao = true;
            LerAcesso();
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
                TxtUsuario.Focus();
                BoxOpcao.Enabled = true;
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtUsuario.Text.Trim() != "")
            {
                CadUsuarios.IdUsuario    = int.Parse(TxtCodigo.Text);
                CadUsuarios.Usuario      = TxtUsuario.Text;
                CadUsuarios.Email        = TxtEmail.Text;
                CadUsuarios.IdFilial     = int.Parse(LstFilial.SelectedValue.ToString());
                CadUsuarios.IdVendedor   = int.Parse(LstVendedor.SelectedValue.ToString());
                CadUsuarios.IdEntregador = int.Parse(LstEntregador.SelectedValue.ToString());
                CadUsuarios.IdPromocao   = int.Parse(LstPromocao.SelectedValue.ToString());
                if (CK_SeusMov.Checked) CadUsuarios.SeusMov = 1; else CadUsuarios.SeusMov = 0;
                if (CK_SolicAutCanc.Checked) CadUsuarios.SolicAutCanc = 1; else CadUsuarios.SolicAutCanc = 0;
                if (CK_LiberaDebito.Checked) CadUsuarios.LiberaDebito = 1; else CadUsuarios.LiberaDebito = 0;
                if (CK_LiberaEstoque.Checked) CadUsuarios.LiberaEstoque = 1; else CadUsuarios.LiberaEstoque = 0;
                if (CK_LiberaPreco.Checked) CadUsuarios.LiberaPreco = 1; else CadUsuarios.LiberaPreco = 0;
                if (CK_Precos.Checked) CadUsuarios.MostraPreco = 1; else CadUsuarios.MostraPreco = 0;
                if (CK_Faturamento.Checked) CadUsuarios.Faturamento = 1; else CadUsuarios.Faturamento = 0;
                if (CK_Instancia.Checked) CadUsuarios.MultplaInstancia = 1; else CadUsuarios.MultplaInstancia = 0;
                if (CK_AltFinanceiro.Checked) CadUsuarios.AlteraFinanceiro = 1; else CadUsuarios.AlteraFinanceiro = 0;
                if (CK_AltProduto.Checked) CadUsuarios.AlterarProduto = 1; else CadUsuarios.AlterarProduto = 0;
                if (CK_AltPessoa.Checked) CadUsuarios.AlterarPessoa = 1; else CadUsuarios.AlterarPessoa = 0;
                if (CK_AltInstalacao.Checked && !FrmPrincipal.VersaoDistribuidor) CadUsuarios.AlterarInstalacao = 1; else CadUsuarios.AlterarInstalacao = 0;
                if (CK_ImpResumido.Checked) CadUsuarios.ImpResumido = 1; else CadUsuarios.ImpResumido = 0;
                if (CK_EmailAltPrd.Checked && !FrmPrincipal.VersaoDistribuidor) CadUsuarios.EmailAltPrd = 1; else CadUsuarios.EmailAltPrd = 0;
                if (CK_SemMovEst.Checked) CadUsuarios.SemMovEst = 1; else CadUsuarios.SemMovEst = 0;
                if (CK_VerificarEstMin.Checked) CadUsuarios.VerificarEstMin = 1; else CadUsuarios.VerificarEstMin = 0;
                if (CK_EnviarFinanceiro.Checked && !FrmPrincipal.VersaoDistribuidor) CadUsuarios.EnviarFinanc = 1; else CadUsuarios.EnviarFinanc = 0;
                if (CK_LimpaEstoque.Checked) CadUsuarios.LimpaEstoque = 1; else CadUsuarios.LimpaEstoque = 0;
                if (CK_AtualizaEstoque.Checked) CadUsuarios.AtualizaEstoque = 1; else CadUsuarios.AtualizaEstoque = 0;
                if (CK_ExcluirReg.Checked) CadUsuarios.ExcluirReg = 1; else CadUsuarios.ExcluirReg = 0;
                if (CK_AtlzBD.Checked) CadUsuarios.AtlzBD = 1; else CadUsuarios.AtlzBD = 0;
                if (CK_AtlItemVd.Checked) CadUsuarios.AltItemVD = 1; else CadUsuarios.AltItemVD = 0;
                if (Ck_BloqDesc.Checked) CadUsuarios.BloqDesc = 1; else CadUsuarios.BloqDesc = 0;
                if (Ck_Telemarketing.Checked) CadUsuarios.Telemarketing = 1; else CadUsuarios.Telemarketing = 0;
                if (CK_CadDistrib.Checked) CadUsuarios.CadDistrib = 1; else CadUsuarios.CadDistrib = 0;
                if (Ck_VerSldDeposito.Checked) CadUsuarios.VerSldDeposito = 1; else CadUsuarios.VerSldDeposito = 0;
                if (Ck_AtivarProduto.Checked) CadUsuarios.AtivarProduto = 1; else CadUsuarios.AtivarProduto = 0;
                if (Ck_CancelarNF.Checked) CadUsuarios.CancelarNF = 1; else CadUsuarios.CancelarNF = 0;
                if (Ck_CancVenda.Checked) CadUsuarios.CancVenda = 1; else CadUsuarios.CancVenda = 0;
                if (Ck_AlterarVenda.Checked) CadUsuarios.AlterarVenda = 1; else CadUsuarios.AlterarVenda = 0;
                if (Ck_CancAmostra.Checked) CadUsuarios.CancAmostra = 1; else CadUsuarios.CancAmostra = 0;
                if (Ck_CancMovEst.Checked) CadUsuarios.CancMovEst = 1; else CadUsuarios.CancMovEst = 0;
                if (Ck_AltSenha.Checked) CadUsuarios.AltSenha = 1; else CadUsuarios.AltSenha = 0;
                if (Ck_VendedorBalcao.Checked) CadUsuarios.VendedorBalcao = 1; else CadUsuarios.VendedorBalcao = 0;
                if (Ck_PrcDistrib.Checked) CadUsuarios.PrcDistrib = 1; else CadUsuarios.PrcDistrib = 0;
                if (Ck_AutCadPF.Checked) CadUsuarios.AutCadPF = 1; else CadUsuarios.AutCadPF = 0;
                if (Ck_UsuCaixaLj.Checked) CadUsuarios.UsuCaixaLj = 1; else CadUsuarios.UsuCaixaLj = 0;
                if (Ck_PrcCusto.Checked) CadUsuarios.LiberaPrcCusto = 1; else CadUsuarios.LiberaPrcCusto = 0;
                if (Ck_MostraCustoVd.Checked) CadUsuarios.MostraCustoVd = 1; else CadUsuarios.MostraCustoVd = 0;
                if (Ck_BxVdFrenteLj.Checked) CadUsuarios.BxVdFrenteLj = 1; else CadUsuarios.BxVdFrenteLj = 0;
                if (Ck_PrcEspDist.Checked) CadUsuarios.UsaPrcEspDist = 1; else CadUsuarios.UsaPrcEspDist = 0;
                if (TxtSenha.Text.Trim() == "")
                    CadUsuarios.Senha = "";
                else
                {
                    if (TxtSenha.Text.Trim() != CadUsuarios.Senha.Trim())
                        CadUsuarios.Senha = Controle.Crypt(TxtSenha.Text.Trim());
                }
                CadUsuarios.GravarDados();                
                PopularGrid();                
                StaFormEdicao = false;
                PopularCampos(CadUsuarios.IdUsuario);
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Nome Usuario não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtUsuario.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CadUsuarios.IdUsuario = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    CadUsuarios.Excluir();
                    PopularGrid();
                    LimpaDados();
                    GridDados.Focus();
                }
            }
            else
                MessageBox.Show("Não existe Registro para Excluir", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
                Paginas.SelectTab(0);
                GridDados.Focus();
                LimpaDados();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.ToString());
            }
        }
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }       
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void Frm_Activated(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
            FrmPrincipal.ClickBtnNovo     += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar   += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar   += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir  += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);            
            FrmPrincipal.ClickBtnFechar   += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
            //
            LstFilial     = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,60) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilial);
            LstVendedor   = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", LstVendedor);
            LstEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstEntregador);
        }
        private void Frm_Deactivate(object sender, EventArgs e) 
        {
            FrmPrincipal.LimpaClickBotoes();
        }       
        private void LimpaDados()
        {
            TxtCodigo.Text  = "0";
            TxtUsuario.Text = "";
            TxtSenha.Text   = "";
            TxtEmail.Text   = "";
            CK_SeusMov.Checked          = true;
            CK_SolicAutCanc.Checked     = true;
            CK_Faturamento.Checked      = false;
            CK_LiberaPreco.Checked      = false;  
            CK_Instancia.Checked        = false;
            CK_AltFinanceiro.Checked    = false;
            CK_AltProduto.Checked       = false;
            CK_AltPessoa.Checked        = false;
            CK_AltInstalacao.Checked    = false;
            CK_ImpResumido.Checked      = false;
            CK_EmailAltPrd.Checked      = false;
            CK_VerificarEstMin.Checked  = false;
            CK_EnviarFinanceiro.Checked = false;
            CK_SemMovEst.Checked        = false;
            CK_LimpaEstoque.Checked     = false;
            CK_AtualizaEstoque.Checked  = false;
            CK_ExcluirReg.Checked       = false;
            CK_AtlzBD.Checked           = false;
            CK_AtlItemVd.Checked        = false;
            Ck_BloqDesc.Checked         = false;
            Ck_Telemarketing.Checked    = false;
            CK_CadDistrib.Checked       = false;
            Ck_VerSldDeposito.Checked   = false;
            Ck_AtivarProduto.Checked    = false;
            Ck_CancelarNF.Checked       = false;
            Ck_CancVenda.Checked        = false;
            Ck_AlterarVenda.Checked     = false;
            Ck_CancAmostra.Checked      = false;
            Ck_CancMovEst.Checked       = false;
            Ck_AltSenha.Checked         = false;
            Ck_VendedorBalcao.Checked   = false;
            Ck_PrcDistrib.Checked       = false;
            Ck_AutCadPF.Checked         = false;
            Ck_UsuCaixaLj.Checked       = false;
            Ck_PrcCusto.Checked         = false;
            Ck_MostraCustoVd.Checked    = false;
            Ck_BxVdFrenteLj.Checked     = false;
            Ck_PrcEspDist.Checked       = false;
            LstFilial.SelectedValue     = 0;
            LstVendedor.SelectedValue   = 0;
            LstEntregador.SelectedValue = 0;
            LstPromocao.SelectedValue   = 0;
            CadUsuarios.LerDados(0);
        }
        private void Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[0].Value.ToString() != "")
                    BtnEditar_Click(FrmPrincipal.BtnEditar,null);
            }
        }
        private void Paginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Paginas.SelectedIndex == 0)
                PopularGrid();

            if (GridDados.CurrentRow != null)
            {
                if (Paginas.SelectedIndex == 0)
                    BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
                else
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                  //  BtnEditar_Click(FrmPrincipal.BtnEditar, null);                
            }
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            if (TxtPesquisa.Text.Trim() != "")
            {
                try
                {
                    DataSet Tabela = new DataSet();
                    if (ChkCodigo.Checked)
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Usuario,Usuario FROM Usuarios WHERE ID_Usuario={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Usuario,Usuario FROM Usuarios WHERE Usuario LIKE '%{0}%' order by Usuario", TxtPesquisa.Text.Trim()));
                    GridDados.DataSource = Tabela;
                    GridDados.DataMember = Tabela.Tables[0].TableName;
                }
                catch
                {
                    MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                PopularGrid();
            }
        }        
        private void MontaOpcoes()
        {
            // Cadastros
            OpAcesso.Nodes.Add("Cadastros", "CADASTROS");
            for (int I = 0; I <= FrmPrincipal.Cadastros.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Cadastros.DropDownItems[I].Text != "")
                    OpAcesso.Nodes[0].Nodes.Add(FrmPrincipal.Cadastros.DropDownItems[I].Name, FrmPrincipal.Cadastros.DropDownItems[I].Text);
            }
            //Pessoas
            OpAcesso.Nodes.Add("Pessoas", "PESSOAS");
            for (int I = 0; I <= FrmPrincipal.Pessoas.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Pessoas.DropDownItems[I].Text != "")
                    OpAcesso.Nodes[1].Nodes.Add(FrmPrincipal.Pessoas.DropDownItems[I].Name, FrmPrincipal.Pessoas.DropDownItems[I].Text);
            }
            //Estoque
            OpAcesso.Nodes.Add("Estoque", "ESTOQUE");
            for (int I = 0; I <= FrmPrincipal.Estoque.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Estoque.DropDownItems[I].Text != "")
                {
                    OpAcesso.Nodes[2].Nodes.Add(FrmPrincipal.Estoque.DropDownItems[I].Name, FrmPrincipal.Estoque.DropDownItems[I].Text);
                    if (FrmPrincipal.Estoque.DropDownItems[I].Text == "Produtos")
                    {
                        OpAcesso.Nodes[2].Nodes[I].Nodes.Add("OpGrupoPrd", "Grupos");
                        OpAcesso.Nodes[2].Nodes[I].Nodes.Add("OpGeneroPrd", "Gênero dos Produtos");
                        OpAcesso.Nodes[2].Nodes[I].Nodes.Add("OpProdutoServ", "Produtos");
                        OpAcesso.Nodes[2].Nodes[I].Nodes.Add("OpPromocao", "Produtos em Destaque para Venda");
                        OpAcesso.Nodes[2].Nodes[I].Nodes.Add("OpGradeComodato", "Grade de Comodato");
                        OpAcesso.Nodes[2].Nodes[I].Nodes.Add("OpVerifSldEst", "Verificar e Ajustar Saldo Estoque");
                    }
                    if (FrmPrincipal.Estoque.DropDownItems[I].Text == "Movimentação do Estoque")
                    {
                        SqlDataReader TabOp = Controle.ConsultaSQL("SELECT * FROM TABELASAUX WHERE CAMPO='TPMVEST' ORDER BY DESCRICAO");
                        while (TabOp.Read())
                            OpAcesso.Nodes[2].Nodes[I].Nodes.Add("Op_" + TabOp["CHAVE"].ToString().Trim(), TabOp["Descricao"].ToString().Trim());                        
                    }
                }
            }
            // Vendas
            OpAcesso.Nodes.Add("Venda", "VENDAS");
            for (int I = 0; I <= FrmPrincipal.Venda.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Venda.DropDownItems[I].Text != "")
                {
                    OpAcesso.Nodes[3].Nodes.Add(FrmPrincipal.Venda.DropDownItems[I].Name, FrmPrincipal.Venda.DropDownItems[I].Text);
                    
                    if (FrmPrincipal.Venda.DropDownItems[I].Text == "Caixa Balcão")
                    {
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpAbrirCx", "Abrir Caixa");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpPDV", "PDV");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpFechaCaixa", "Fechar Caixa");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpLeituraX", "Leitura X");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpLeituraZ", "Leitura Z");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpMemoriaFiscal", "Memória Fiscal");                        
                    }
                    if (FrmPrincipal.Venda.DropDownItems[I].Text == "Venda")
                    {
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpPedidoVenda", "Pedido de Venda");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpOrcamento", "Orçamento");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpOrdemEntrega", "Ordem de Entrega");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpComodato", "Comotado");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpTroca", "Troca de Mercadoria");                        
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpBonificacao", "Bonificação");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpAmostra", "Amostra");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("VendaProgramada", "Venda Programada");
                        OpAcesso.Nodes[3].Nodes[I].Nodes[7].Nodes.Add("OpVendaFinanc", "Venda Financeira");
                        OpAcesso.Nodes[3].Nodes[I].Nodes[7].Nodes.Add("OpEntregaMerc", "Entrega de Mercadoria");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpVdInternet", "Pedido Distribuidor (Internet)");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpPedConsignacao", "Pedido Consignação");
                    }
                    if (FrmPrincipal.Venda.DropDownItems[I].Text == "Expedição")
                    {
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpMapaEntrega", "Mapa de Entrega");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpConfEntrega", "Confirmar Entregas");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpSepararPrd", "Separação de Produtos");
                    }
                    if (FrmPrincipal.Venda.DropDownItems[I].Text == "Telemarketing")
                    {
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpCriarCamp", "Criar Campanha de Venda");
                        OpAcesso.Nodes[3].Nodes[I].Nodes.Add("OpTrabCamp", "Trabalhar Campanha");
                    }
                }
            }
            // Faturamento
            OpAcesso.Nodes.Add("Faturamentos","FATURAMENTO");
            for (int I = 0; I <= FrmPrincipal.Faturamentos.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Faturamentos.DropDownItems[I].Text != "")
                    OpAcesso.Nodes[4].Nodes.Add(FrmPrincipal.Faturamentos.DropDownItems[I].Name, FrmPrincipal.Faturamentos.DropDownItems[I].Text);
            }
            // Financeiro
            OpAcesso.Nodes.Add("Financeiro", "FINANCEIRO");
            for (int I = 0; I <= FrmPrincipal.Financeiro.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Financeiro.DropDownItems[I].Text != "")
                    OpAcesso.Nodes[5].Nodes.Add(FrmPrincipal.Financeiro.DropDownItems[I].Name, FrmPrincipal.Financeiro.DropDownItems[I].Text);
            }
           /* // Produção
            OpAcesso.Nodes.Add("Producao", "PRODUÇÃO");
            for (int I = 0; I <= FrmPrincipal.Producao.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Producao.DropDownItems[I].Text != "")
                    OpAcesso.Nodes[6].Nodes.Add(FrmPrincipal.Producao.DropDownItems[I].Name, FrmPrincipal.Producao.DropDownItems[I].Text);
            }*/
            // R&H-Recursos Humanos
            OpAcesso.Nodes.Add("Rh", "RH - RECURSOS HUMANOS");
            for (int I = 0; I <= FrmPrincipal.Rh.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Rh.DropDownItems[I].Text != "")
                    OpAcesso.Nodes[6].Nodes.Add(FrmPrincipal.Rh.DropDownItems[I].Name, FrmPrincipal.Rh.DropDownItems[I].Text);
            }
            //Relatórios
            OpAcesso.Nodes.Add("Relatorios", "RELATÓRIOS");
            for (int I = 0; I <= FrmPrincipal.Relatorios.DropDownItems.Count - 1; I++)
            {
                if (FrmPrincipal.Relatorios.DropDownItems[I].Text != "")
                {
                    OpAcesso.Nodes[7].Nodes.Add(FrmPrincipal.Relatorios.DropDownItems[I].Name, FrmPrincipal.Relatorios.DropDownItems[I].Text);
                    if (FrmPrincipal.Relatorios.DropDownItems[I].Text == "Faturamento")
                    {
                        OpAcesso.Nodes[7].Nodes[I].Nodes.Add("OpBoleto", "Boleto");
                        OpAcesso.Nodes[7].Nodes[I].Nodes.Add("OpRelRecibo", "Recibo");
                        OpAcesso.Nodes[7].Nodes[I].Nodes.Add("OpPromissoria", "Promissória");
                        OpAcesso.Nodes[7].Nodes[I].Nodes.Add("OpRelFatDiversos", "Relátorios");                        
                    }
                }
            }
        }
        private void OpAcesso_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (StaFormEdicao && CadUsuarios.IdUsuario > 0)
            {
                int AutAcesso = 0;
                if (e.Node.Checked)
                    AutAcesso = 1;
                GravarAcesso(e.Node.Name, AutAcesso);

                if (e.Node.Nodes.Count > 0)
                {
                    for (int I = 0; I <= e.Node.Nodes.Count - 1; I++)
                    {
                        AutAcesso = 0;
                        e.Node.Nodes[I].Checked = e.Node.Checked;
                        if (e.Node.Nodes[I].Checked)
                            AutAcesso = 1;
                        GravarAcesso(e.Node.Nodes[I].Name, AutAcesso);
                    }
                }
            } 
        }
        private void GravarAcesso(string Op,int Acesso)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM ACESSOUSUARIO WHERE ID_USUARIO=" + CadUsuarios.IdUsuario.ToString() + " AND OPCAO='" + Op.Trim() + "'");
            if (Tabela.HasRows)
                Controle.ExecutaSQL("UPDATE ACESSOUSUARIO SET ACESSO=" + Acesso.ToString() + " WHERE ID_USUARIO=" + CadUsuarios.IdUsuario.ToString() + " AND OPCAO='" + Op.Trim() + "'");
            else
                Controle.ExecutaSQL("INSERT INTO ACESSOUSUARIO (ID_USUARIO,OPCAO,ACESSO) VALUES (" + CadUsuarios.IdUsuario.ToString() + ",'" + Op.Trim() + "'," + Acesso.ToString() + ")");
        }
        private void LerAcesso()
        {
            for (int Nivel1 = 0; Nivel1 <= OpAcesso.Nodes.Count - 1; Nivel1++) // Nivel 1
            {
                OpAcesso.Nodes[Nivel1].Checked = false;
                for (int Nivel2 = 0; Nivel2 <= OpAcesso.Nodes[Nivel1].Nodes.Count - 1; Nivel2++) // Nivel 2
                {
                    OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Checked = false;

                    for (int Nivel3 = 0; Nivel3 <= OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes.Count - 1; Nivel3++) // Nivel 3
                    {
                        OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Checked = false;

                        for (int Nivel4 = 0; Nivel4 <= OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Nodes.Count - 1; Nivel4++) // Nivel 4
                        {
                            OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Nodes[Nivel4].Checked = false;
                        }
                    }
                }
            }

            if (CadUsuarios.IdUsuario > 0)
            {
                DataSet TabAcesso = new DataSet();
                TabAcesso = Controle.ConsultaTabela("SELECT * FROM ACESSOUSUARIO WHERE ID_USUARIO=" + CadUsuarios.IdUsuario.ToString());
                BoxOpcao.Enabled = false;
                for (int I = 0; I <= TabAcesso.Tables[0].Rows.Count - 1; I++)
                {                    
                    for (int Nivel1 = 0; Nivel1 <= OpAcesso.Nodes.Count - 1; Nivel1++) // Nivel 1
                    {                        
                        if (OpAcesso.Nodes[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            OpAcesso.Nodes[Nivel1].Checked = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";

                        for (int Nivel2 = 0; Nivel2 <= OpAcesso.Nodes[Nivel1].Nodes.Count - 1; Nivel2++) // Nivel 2
                        {                            
                            if (OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                                OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Checked = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";

                            for (int Nivel3 = 0; Nivel3 <= OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes.Count - 1; Nivel3++) // Nivel 3
                            {                                
                                if (OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                                    OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Checked = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";

                                for (int Nivel4 = 0; Nivel4 <= OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Nodes.Count - 1; Nivel4++) // Nivel 4
                                {                                    
                                    if (OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Nodes[Nivel4].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                                        OpAcesso.Nodes[Nivel1].Nodes[Nivel2].Nodes[Nivel3].Nodes[Nivel4].Checked = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                                }
                            }
                        }
                    }
                }
            }
        }

              
    }
}
