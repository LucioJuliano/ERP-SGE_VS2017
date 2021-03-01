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
using System.Collections;
using System.Data.SqlClient;

namespace ERP_SGE
{
    public partial class FrmProducao : Form
    {
        Funcoes Controle         = new Funcoes();
        Producao CadProducao     = new Producao();
        ProducaoItens ProdItens  = new ProducaoItens();
        ProducaoProdutos ProdPrd = new ProducaoProdutos();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        private DataSet TabItens;
        private BindingSource Source_Itens;

        private DataSet TabPrd;
        private BindingSource Source_Prd;

        public FrmProducao()
        {
            InitializeComponent();
        }

        private void FrmProducao_Load(object sender, EventArgs e)
        {
            Controle.Conexao     = FrmPrincipal.Conexao;
            CadProducao.Controle = Controle;
            ProdItens.Controle   = Controle;
            ProdPrd.Controle     = Controle;
            TabItens             = new DataSet();
            Source_Itens         = new BindingSource();
            TabPrd               = new DataSet();
            Source_Prd           = new BindingSource();            
            PopularGrid();
        }

        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT ID_PRODUCAO,PRODUTO FROM PRODUCAO ORDER BY PRODUTO");
            BindingSource Source = new BindingSource();
            Source.DataSource    = Tabela;
            Source.DataMember    = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Producao", CadProducao.IdProducao);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            CadProducao.LerDados(Isn);
            TxtCodigo.Text       = CadProducao.IdProducao.ToString();            
            TxtProduto.Text      = CadProducao.Produto;
            TxtObservacao.Text   = CadProducao.Observacao;
            TxtQtdeFabrica.Value = CadProducao.QtdeFabrica;
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularCampos(0);
            PopularGridItens();
            PopularGridPrd();
            FrmPrincipal.ControleBotoes(true);
            TxtProduto.Focus();
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
                PopularGridItens();
                PopularGridPrd();
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
                TxtProduto.Focus();                
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtProduto.Text.Trim() != "")
            {
                CadProducao.IdProducao  = int.Parse(TxtCodigo.Text);
                CadProducao.Produto     = TxtProduto.Text;
                CadProducao.Observacao  = TxtObservacao.Text;
                CadProducao.QtdeFabrica = TxtQtdeFabrica.Value;
                StaFormEdicao = false;
                CadProducao.GravarDados();
                PopularGrid();
                PopularCampos(CadProducao.IdProducao);
                PopularGridItens();
                PopularGridPrd();
                FrmPrincipal.ControleBotoes(false);
                Panel2.Enabled = true;                
            }
            else
            {
                MessageBox.Show("Favor informar o Produto de Fabricação", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtProduto.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {

                CadProducao.IdProducao = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                CadProducao.LerDados(CadProducao.IdProducao);

                if (FrmPrincipal.Perfil_Usuario.ExcluirReg == 0)
                {
                    FrmAutorizacao Autorizacao = new FrmAutorizacao();
                    Autorizacao.FrmPrincipal = FrmPrincipal;
                    Autorizacao.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (Autorizacao.AcessoOk)
                    {
                        if (Autorizacao.Usuario.ExcluirReg == 0)
                        {
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CadProducao.IdProducao = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    CadProducao.Excluir();
                    //Registrando Movimento de Auditoria
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
        private void LimpaDados()
        {
            TxtCodigo.Text       = "0";            
            TxtProduto.Text      = "";
            TxtObservacao.Text   = "";
            TxtQtdeFabrica.Value = 0;
            CadProducao.LerDados(0);
            PopularGridItens();
            PopularGridPrd();
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
                {
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                    PopularGridItens();
                    PopularGridPrd();
                    
                }
            }            
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }
        private void Frm_Activated(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
            FrmPrincipal.ClickBtnNovo += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
            FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }


        //Composicao dos Produtos
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE FROM PRODUCAOITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_PRODUCAO=" + CadProducao.IdProducao.ToString());
            Source_Itens.DataSource     = TabItens;
            Source_Itens.DataMember     = TabItens.Tables[0].TableName;
            GridItens.DataSource        = Source_Itens;
            NavegadorComp.BindingSource = Source_Itens;
            int item = Source_Itens.Find("ID_Lanc", ProdItens.IdLanc);
            Source_Itens.Position = item;
        }
        private void PopularGridPrd()
        {
            TabPrd = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO FROM PRODUCAOPRODUTOS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_PRODUCAO=" + CadProducao.IdProducao.ToString());
            Source_Prd.DataSource     = TabPrd;
            Source_Prd.DataMember     = TabPrd.Tables[0].TableName;
            GridPrd.DataSource        = Source_Prd;
            NavegadorPrd.BindingSource = Source_Prd;
            int item = Source_Prd.Find("ID_Lanc", ProdPrd.IdLanc);
            Source_Prd.Position = item;
        }
        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    IncluirItemComp();
                }
            }
        }
        private void GridPrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridPrd.CurrentRow == null || GridPrd.Rows.Count - 1 == GridPrd.CurrentRow.Index)
                {
                    IncluirPrd();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro de Composição dos Produto em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }

            if (CadProducao.IdProducao == 0)
            {
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (CadProducao.IdProducao > 0)
                {
                    decimal Qtde = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                    ProdItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    ProdItens.Qtde = Qtde;
                    ProdItens.GravarDados();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        
        private void BtnIncComp_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (CadProducao.IdProducao > 0)
                    IncluirItemComp();
            }
        }

        private void BtnIncPrd_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (CadProducao.IdProducao > 0)
                    IncluirPrd();
            }
        }
        private void BtnExcComp_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (CadProducao.IdProducao > 0)
                {
                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ProdItens.IdLanc = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                        ProdItens.Excluir();
                        ProdItens.IdLanc = 0;
                        PopularGridItens();
                    }
                }
            }
        }
        private void BtnExcPrd_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (CadProducao.IdProducao > 0)
                {
                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ProdPrd.IdLanc = int.Parse(GridPrd.CurrentRow.Cells[0].Value.ToString());
                        ProdPrd.Excluir();
                        ProdPrd.IdLanc = 0;
                        PopularGridPrd();
                    }
                }
            }
        }
        private void IncluirItemComp()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (CadProducao.IdProducao > 0)
                {

                    FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                    BuscaPrd.FrmPrincipal = this.FrmPrincipal;
                    BuscaPrd.IdProduto = 0;
                    BuscaPrd.ShowDialog();
                    if (BuscaPrd.IdProduto > 0)
                    {                        
                        ProdItens.Qtde = 1;
                        ProdItens.IdLanc = 0;
                        ProdItens.IdProducao = CadProducao.IdProducao;
                        ProdItens.IdProduto  = BuscaPrd.IdProduto;
                        ProdItens.GravarDados();
                        PopularGridItens();
                        GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];

                    }
                    else
                        Source_Itens.CancelEdit();
                    BuscaPrd.Dispose();
                }
            }
        }
        private void IncluirPrd()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (CadProducao.IdProducao > 0)
                {

                    FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                    BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                    BuscaPrd.IdProduto       = 0;
                    BuscaPrd.LstMvEst        = true;
                    BuscaPrd.ShowDialog();

                    if (BuscaPrd.IdProduto > 0)
                    {
                        ProdPrd.IdLanc     = 0;
                        ProdPrd.IdProducao = CadProducao.IdProducao;
                        ProdPrd.IdProduto  = BuscaPrd.IdProduto;
                        ProdPrd.GravarDados();
                        PopularGridPrd();                        

                    }
                    else
                        Source_Prd.CancelEdit();
                    BuscaPrd.Dispose();
                }
            }
        }
        //-----------------
    }
}
