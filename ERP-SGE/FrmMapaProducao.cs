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
using System.Collections;

namespace ERP_SGE
{
    public partial class FrmMapaProducao : Form
    {

        Funcoes Controle              = new Funcoes();
        MapaProducao Mapa             = new MapaProducao();        
        MapaProducaoProdutos MapaProd = new MapaProducaoProdutos();
        MapaProducaoItens MapaItens   = new MapaProducaoItens();
        Auditoria RegAuditoria        = new Auditoria();
        FrmBuscaProduto BuscaPrd      = new FrmBuscaProduto();

        private DataSet TabItens;
        private BindingSource Source_Itens;

        private DataSet TabProd;
        private BindingSource Source_Prod;

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private BindingSource Source_Mapa;

        public FrmMapaProducao()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao      = FrmPrincipal.Conexao;
            Mapa.Controle         = Controle;
            MapaItens.Controle    = Controle;
            MapaProd.Controle     = Controle;
            RegAuditoria.Controle = Controle;
            Source_Mapa           = new BindingSource();
            TabItens              = new DataSet();
            Source_Itens          = new BindingSource();
            TabProd               = new DataSet();
            Source_Prod           = new BindingSource();
            BuscaPrd.FrmPrincipal = this.FrmPrincipal;
            Chk_Periodo.Checked   = false;
            Dt1.Value             = DateTime.Now;
            Dt2.Value             = DateTime.Now;
            Rb_Aberto.Checked     = true;
            LstProduto = FrmPrincipal.PopularCombo("SELECT Id_Producao,Substring(Produto,1,70) AS PRODUTO FROM Producao ORDER BY PRODUTO", LstProduto, "Nenhum");
            PopularGrid();
        }

        private void PopularGrid()
        {
            string Filtro = "";

            if (Rb_Todos.Checked)
                Filtro = " WHERE T1.STATUS IN (0,1,2)";
            else if (Rb_Aberto.Checked)
                Filtro = " WHERE T1.STATUS=0";
            else if (Rb_Producao.Checked)
                Filtro = " WHERE T1.STATUS=1";
            else
                Filtro = " WHERE T1.STATUS=2";

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND CONVERT(DATETIME,T1.DATA,103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,T1.DATA,103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqNumMapa.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_MAPA=" + TxtPesqNumMapa.Text.Trim();

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.Id_Mapa,T1.Data,T2.Produto,T1.Qtde,T1.Lote,CASE T1.STATUS WHEN 0 THEN 'EM ABERTO' WHEN 1 THEN 'EM PRODUÇÃO' ELSE 'CONCLUIDO' END AS STATUS,T1.ID_MVPRODUCAO,T1.ID_MVMATPRIMA FROM MapaProducao T1" +
                                             " LEFT JOIN Producao T2 ON (T2.Id_Producao=T1.Id_ProdProducao) "+ Filtro);
            Source_Mapa.DataSource = Tabela;
            Source_Mapa.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source_Mapa;
            int Lanc = Source_Mapa.Find("Id_Mapa", Mapa.IdMapa);
            Source_Mapa.Position = Lanc;
        }

        private void PopularCampos(int Isn)
        {
            Mapa.LerDados(Isn);
            TxtCodigo.Text     = string.Format("{0:D5}", int.Parse(Mapa.IdMapa.ToString()));
            TxtData.Value      = Mapa.Data;
            TxtQtde.Value      = Mapa.Qtde;
            TxtLote.Value      = Mapa.Lote;
            TxtObservacao.Text = Mapa.Observacao;
            LstProduto.SelectedValue = Mapa.IdProdProd.ToString();            
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularCampos(0);
            PopularGridItens();
            PopularGridProd();
            FrmPrincipal.ControleBotoes(true);
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
                if (Mapa.Status == 1)
                {
                    MessageBox.Show("Mapa já em Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Mapa.Status == 2)
                {
                    MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                PopularGridItens();
                PopularGridProd();
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (LstProduto.SelectedValue.ToString() != "0")
            {
                if (TxtQtde.Value == 0)
                {
                    MessageBox.Show("Informe a Quantidade de Produção", "Atenção",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Mapa.IdMapa     = int.Parse(TxtCodigo.Text);
                Mapa.Data       = TxtData.Value;
                Mapa.Qtde       = TxtQtde.Value;
                Mapa.Lote       = int.Parse(TxtLote.Value.ToString());
                Mapa.Observacao = TxtObservacao.Text;
                Mapa.IdProdProd = int.Parse(LstProduto.SelectedValue.ToString());
                Mapa.GravarDados();
                GerarMatPrima();
                StaFormEdicao = false;
                //Registrando Movimento de Auditoria
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdProdProd.ToString(), 1, "Inclusão: " + LstProduto.SelectedText.ToString().Trim() + " Qtde: " + Mapa.Qtde.ToString());
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdProdProd.ToString(), 2, "Alteração: " + LstProduto.SelectedText.ToString().Trim() + " Qtde: " + Mapa.Qtde.ToString());
                PopularGrid();
                PopularGridItens();
                PopularGridProd();
                PopularCampos(Mapa.IdMapa);
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Favor informe o Produto para Produção", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {                
                Mapa.IdMapa = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Mapa.LerDados(Mapa.IdMapa);

                if (Mapa.Status == 1)
                {
                    MessageBox.Show("Mapa já em Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Mapa.Status == 2)
                {
                    MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                StaFormEdicao = true;
                if (MessageBox.Show("Confirma a Exclusão do Mapa de Produção", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Mapa.IdMapa = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Mapa.Excluir();
                    //Registrando Movimento de Auditoria                    
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdProdProd.ToString(), 3, "Exclusão: " + LstProduto.SelectedText.ToString().Trim() + " Qtde: " + Mapa.Qtde.ToString());
                    PopularGrid();
                    LimpaDados();
                    GridDados.Focus();
                    StaFormEdicao = false;
                }
            }
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
            TxtCodigo.Text     = "0";
            TxtData.Value      = DateTime.Now;
            TxtQtde.Value      = 0;
            TxtLote.Value      = 0;
            TxtObservacao.Text = "";
            LstProduto.SelectedValue = "0";            
            Mapa.LerDados(0);
            PopularCampos(Mapa.IdMapa);
            PopularGridItens();
            PopularGridProd();
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
                    PopularGridProd();
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

        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE FROM MAPAPRODUCAOITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_MAPA=" + Mapa.IdMapa.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource    = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int Lanc = Source_Itens.Find("ID_Lanc", MapaItens.IdLanc);
            Source_Itens.Position = Lanc;
        }

        private void PopularGridProd()
        {
            TabProd = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.QTDEEXTRA FROM MAPAPRODUCAOPRODUTOS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_MAPA=" + Mapa.IdMapa.ToString());
            Source_Prod.DataSource     = TabProd;
            Source_Prod.DataMember     = TabProd.Tables[0].TableName;
            GridPrd.DataSource         = Source_Prod;
            NavegadorMat.BindingSource = Source_Prod;
            int Lanc = Source_Prod.Find("ID_Lanc", MapaProd.IdLanc);
            Source_Itens.Position = Lanc;
        }
        private void GridPrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridPrd.CurrentRow == null || GridPrd.Rows.Count - 1 == GridPrd.CurrentRow.Index)
                {
                    IncluirMatPrima();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }


            if (Mapa.Status == 2)
            {
                MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            if (Mapa.IdMapa == 0)
            {
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
        }

        private void GridPrd_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Prod.CancelEdit();
                e.Cancel = true;
            }


            if (Mapa.Status == 2)
            {
                MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (Mapa.IdMapa == 0)
            {
                Source_Prod.CancelEdit();
                e.Cancel = true;
            }

        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Mapa.IdMapa > 0)
                {
                    MapaItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    MapaItens.Qtde = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                    MapaItens.GravarDados();
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, MapaItens.IdLanc.ToString(), 2, "Alteração do Item: " + MapaItens.IdProduto.ToString()+"  Qtde: "+MapaItens.Qtde.ToString());
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }

        private void GridPrd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Mapa.IdMapa > 0)
                {
                    MapaProd.LerDados(int.Parse(GridPrd.CurrentRow.Cells[0].Value.ToString()));
                    MapaProd.Qtde      = decimal.Parse(GridPrd.CurrentRow.Cells[3].Value.ToString());
                    MapaProd.QtdeExtra = decimal.Parse(GridPrd.CurrentRow.Cells[4].Value.ToString());
                    MapaProd.GravarDados();
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, MapaProd.IdLanc.ToString(), 2, "Alteração Mat.Prima Item: " + MapaProd.IdProduto.ToString() + "  Qtde: " + MapaProd.Qtde.ToString());
                    PopularGridProd();
                    GridPrd.CurrentCell = GridPrd.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Mapa.Status == 2)
                {
                    MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Mapa.IdMapa > 0)
                    IncluirMatPrima();
            }
        }
        

        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Mapa.IdMapa > 0)
                {
                    if (Mapa.Status == 2)
                    {
                        MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);                     
                        return;
                    }
                    if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MapaProd.LerDados(int.Parse(GridPrd.CurrentRow.Cells[0].Value.ToString()));
                        if (MapaProd.Qtde > 0)
                        {
                            MessageBox.Show("Item de Origem da Composição do Produto, Não pode ser Excluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {                            
                            FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, MapaProd.IdLanc.ToString(), 3, "Exclusão Mat. Prima do Item: " + MapaProd.IdProduto.ToString() + "  Qtde: " + MapaProd.Qtde.ToString());
                            MapaProd.Excluir();
                            MapaProd.IdLanc = 0;
                            PopularGridProd();
                        }
                    }
                }
            }
        }
        private void IncluirMatPrima()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Mapa.IdMapa > 0)
                {
                    if (Mapa.Status == 1)
                    {
                        MessageBox.Show("Mapa já em Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Mapa.Status == 2)
                    {
                        MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                    BuscaPrd.FrmPrincipal = this.FrmPrincipal;
                    BuscaPrd.IdProduto = 0;
                    BuscaPrd.LstMvEst = true;
                    BuscaPrd.ShowDialog();
                    if (BuscaPrd.IdProduto > 0)
                    {
                        MapaProd.IdLanc    = 0;
                        MapaProd.IdMapa    = Mapa.IdMapa;
                        MapaProd.IdProduto = BuscaPrd.IdProduto;
                        MapaProd.Qtde      = 0;
                        MapaProd.GravarDados();
                        PopularGridProd();
                        GridPrd.CurrentCell = GridPrd.CurrentRow.Cells[3];
                        FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, MapaProd.IdLanc.ToString(), 1, "Inclusão Mat. Prima do Item: " + MapaProd.IdProduto.ToString() + "  Qtde: " + MapaItens.Qtde.ToString());
                    }
                    else
                        Source_Prod.CancelEdit();
                    BuscaPrd.Dispose();
                }
            }
        }

        private void BtnEnviarProd_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Mapa.IdMapa > 0)
                {
                    if (Mapa.Status == 1)
                    {
                        MessageBox.Show("Mapa já em Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Mapa.Status == 2)
                    {
                        MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (MessageBox.Show("Confirma o Envio do Mapa para Produção", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BtnEnviarProd.Enabled = false;
                    Application.DoEvents();
                    //
                    try
                    {                        
                        //Atualizando o Estoque dos Produtos Envasados                        
                        MvEstoque MvProducao = new MvEstoque();
                        MvEstoqueItens MvItens = new MvEstoqueItens();

                        Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                        ControleEstoque.Controle = Controle;

                        MvItens.Controle = Controle;
                        MvProducao.Controle = Controle;
                        MvProducao.LerDados(0);

                        //Gerando o Movimento Lanc. Produção
                        DataSet TabPrd = new DataSet();
                        TabPrd = Controle.ConsultaTabela("SELECT * FROM MapaProducaoProdutos WHERE ID_MAPA=" + Mapa.IdMapa.ToString());
                        if (TabPrd.Tables[0].Rows.Count > 0)
                        {
                            MvProducao.LerDados(0);
                            MvProducao.TpMov = "BMAPR";
                            MvProducao.Observacao = "Movimento Gerado Automatico Pelo Sistema Ref. Mapa de Produção No." + string.Format("{0:D6}", Mapa.IdMapa);
                            MvProducao.Data = Mapa.Data;
                            MvProducao.IdFilial = FrmPrincipal.IdFilialConexao;
                            MvProducao.DtEmissao = Mapa.Data;
                            MvProducao.DtEntSai = Mapa.Data;
                            MvProducao.Documento = "PRODUÇÂO AUT.";
                            MvProducao.NumDocumento = string.Format("{0:D6}", Mapa.IdMapa);
                            MvProducao.GravarDados();

                            for (int I = 0; I <= TabPrd.Tables[0].Rows.Count - 1; I++)
                            {
                                MvItens.LerDados(0);
                                MvItens.IdMov = MvProducao.IdMov;
                                MvItens.IdProduto = int.Parse(TabPrd.Tables[0].Rows[I]["ID_PRODUTO"].ToString());
                                MvItens.Qtde = decimal.Parse(TabPrd.Tables[0].Rows[I]["Qtde"].ToString());
                                MvItens.GravarDados();
                            }
                        }

                        //Concluindo o Movimento de Produção
                        SqlDataReader Tab;
                        if (MvProducao.IdMov > 0)
                        {
                            Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvProducao.IdMov.ToString());
                            ControleEstoque.MovimentoEstoque(Tab, 1, 2, false, MvProducao.TpMov, MvProducao.DtEntSai, MvProducao.IdFilial);
                            MvProducao.Concluir();
                        }        
                        Mapa.Status = 1;
                        Mapa.GravarDados();
                        FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdProdProd.ToString(), 4, "Mapa Enviado para Produção: " + LstProduto.SelectedText.ToString().Trim() + " Qtde: " + Mapa.Qtde.ToString());
                        PopularGridItens();
                        MessageBox.Show("Mapa Enviado para Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {                        
                        MessageBox.Show("Erro ao enviar o Mapa para Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                    }
                }
                BtnEnviarProd.Enabled = true;
            }            
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Mapa.IdMapa > 0)
                {
                    if (Mapa.Status == 0)
                    {
                        MessageBox.Show("Mapa em Aberto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string sSql = "SELECT T1.Id_Mapa,T1.Data,T1.Observacao,T1.Qtde,T1.Status,T2.Produto,T2.QtdeFabrica,T2.Observacao AS ObsProduto,T1.LOTE FROM MapaProducao T1"+
                                  " LEFT JOIN Producao T2 ON (T2.Id_Producao=T1.Id_ProdProducao) Where T1.Id_Mapa="+Mapa.IdMapa.ToString();

                    string sSql2 = "SELECT T1.Id_Produto,t2.Referencia,t2.Descricao,T1.QTDE FROM MapaProducaoItens T1"+
                                   " LEFT JOIN Produtos t2 on (t2.Id_Produto=T1.Id_Produto) Where T1.Id_Mapa=" + Mapa.IdMapa.ToString();

                    string sSql3 = "SELECT T1.Id_Produto,t2.Referencia,t2.Descricao,T1.QTDE,T1.QtdeExtra FROM MapaProducaoProdutos T1"+
                                   " LEFT JOIN Produtos t2 on (t2.Id_Produto=T1.Id_Produto) Where T1.Id_Mapa=" + Mapa.IdMapa.ToString();
                                        
                    FrmRelatorios FrmRel = new FrmRelatorios();
                    Relatorios.RelMapaProducao Rel001 = new Relatorios.RelMapaProducao();
                    DataSet TabRel  = new DataSet();
                    DataSet TabRel1 = new DataSet();
                    DataSet TabRel2 = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql);
                    TabRel1 = Controle.ConsultaTabela(sSql2);
                    TabRel2 = Controle.ConsultaTabela(sSql3);
                    Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                    Rel001.Database.Tables[1].SetDataSource(TabRel1.Tables[0]);
                    Rel001.Database.Tables[2].SetDataSource(TabRel2.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                }
            }
        }

        private void GerarMatPrima()
        {
            try
            {
                if (Mapa.Qtde == 0)
                {
                    MessageBox.Show("Favor Informar a Quantidade de Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Controle.ExecutaSQL("DELETE MAPAPRODUCAOITENS WHERE ID_MAPA=" + Mapa.IdMapa.ToString());
                Controle.ExecutaSQL("DELETE MAPAPRODUCAOPRODUTOS WHERE ID_MAPA=" + Mapa.IdMapa.ToString());
                //Gerando Lançamentos da Materia Prima
                SqlDataReader TabProducao = Controle.ConsultaSQL("Select * from ProducaoItens T1 LEFT JOIN PRODUCAO T2 ON (T2.ID_PRODUCAO=T1.ID_PRODUCAO) WHERE T1.Id_Producao=" + Mapa.IdProdProd.ToString());
                while (TabProducao.Read())
                {
                    decimal QtdeBase = Mapa.Qtde / decimal.Parse(TabProducao["QtdeFabrica"].ToString());
                    MapaProd.LerDados(0);
                    MapaProd.IdMapa    = Mapa.IdMapa;
                    MapaProd.IdProduto = int.Parse(TabProducao["ID_Produto"].ToString());
                    MapaProd.Qtde = QtdeBase * decimal.Parse(TabProducao["Qtde"].ToString());
                    MapaProd.GravarDados();
                }
                //Gerando Lançamentos Produtos Envasados
                SqlDataReader TabPrd = Controle.ConsultaSQL("Select * from ProducaoProdutos T1 LEFT JOIN PRODUCAO T2 ON (T2.ID_PRODUCAO=T1.ID_PRODUCAO) WHERE T1.Id_Producao=" + Mapa.IdProdProd.ToString());
                while (TabPrd.Read())
                {
                    MapaItens.LerDados(0);
                    MapaItens.IdMapa    = Mapa.IdMapa;
                    MapaItens.IdProduto = int.Parse(TabPrd["ID_Produto"].ToString());
                    MapaItens.GravarDados();
                }
            }
            catch
            {
                Controle.ExecutaSQL("DELETE MAPAPRODUCAOITENS WHERE ID_MAPA=" + Mapa.IdMapa.ToString());
                Controle.ExecutaSQL("DELETE MAPAPRODUCAOPRODUTOS WHERE ID_MAPA=" + Mapa.IdMapa.ToString());
                MessageBox.Show("Erro ao enviar o Mapa para Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Mapa.IdMapa > 0)
                {
                    if (Mapa.Status == 0)
                    {
                        MessageBox.Show("Mapa de Produção em Aberto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Mapa.Status == 2)
                    {
                        MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (MessageBox.Show("Confirma a Conclusão Mapa para Produção", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BtnConcluir.Enabled = false;
                    Application.DoEvents();
                    //
                    try
                    {
                        //Atualizando o Estoque dos Produtos Envasados                        
                        MvEstoque MvProducao = new MvEstoque();
                        MvEstoqueItens MvItens = new MvEstoqueItens();

                        Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                        ControleEstoque.Controle = Controle;

                        MvItens.Controle = Controle;
                        MvProducao.Controle = Controle;
                        MvProducao.LerDados(0);

                        //Gerando o Movimento Lanc. Produção
                        DataSet TabPrd = new DataSet();
                        TabPrd = Controle.ConsultaTabela("SELECT * FROM MAPAPRODUCAOITENS WHERE ID_MAPA=" + Mapa.IdMapa.ToString());
                        if (TabPrd.Tables[0].Rows.Count > 0)
                        {
                            MvProducao.LerDados(0);
                            MvProducao.TpMov = "ENTPR";
                            MvProducao.Observacao = "Movimento Gerado Automatico Pelo Sistema Ref. Mapa de Produção No." + string.Format("{0:D6}", Mapa.IdMapa);
                            MvProducao.Data = Mapa.Data;
                            MvProducao.IdFilial = FrmPrincipal.IdFilialConexao;
                            MvProducao.DtEmissao = Mapa.Data;
                            MvProducao.DtEntSai = Mapa.Data;
                            MvProducao.Documento = "PRODUÇÂO AUT.";
                            MvProducao.NumDocumento = string.Format("{0:D6}", Mapa.IdMapa);
                            MvProducao.GravarDados();

                            for (int I = 0; I <= TabPrd.Tables[0].Rows.Count - 1; I++)
                            {
                                MvItens.LerDados(0);
                                MvItens.IdMov = MvProducao.IdMov;
                                MvItens.IdProduto = int.Parse(TabPrd.Tables[0].Rows[I]["ID_PRODUTO"].ToString());
                                MvItens.Qtde = decimal.Parse(TabPrd.Tables[0].Rows[I]["Qtde"].ToString());
                                MvItens.GravarDados();
                            }
                        }

                        //Concluindo o Movimento de Produção
                        SqlDataReader Tab;
                        if (MvProducao.IdMov > 0)
                        {
                            Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvProducao.IdMov.ToString());
                            ControleEstoque.MovimentoEstoque(Tab, 1, 1, false, MvProducao.TpMov, MvProducao.DtEntSai,MvProducao.IdFilial);
                            MvProducao.Concluir();
                        }

                        BaixaExtraMatPrima();

                        Mapa.Status = 2;
                        Mapa.IdMvMatPrima = MvProducao.IdMov;
                        Mapa.GravarDados();
                        FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdProdProd.ToString(), 5, "Concluir o Mapa de Produção");
                        PopularCampos(Mapa.IdMapa);
                        PopularGridItens();
                        PopularGridProd();
                        MessageBox.Show("Mapa de Produção Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao Concluir o Mapa para Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                BtnConcluir.Enabled = true;
            }        
        }

        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Mapa de Produção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Mapa.Status == 2)
                {
                    MessageBox.Show("Mapa já Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Confirma o Cancelamento do Mapa de Produção", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MvEstoque MvProducao = new MvEstoque();
                    MvEstoqueItens MvItens = new MvEstoqueItens();

                    Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                    ControleEstoque.Controle = Controle;

                    MvItens.Controle = Controle;
                    MvProducao.Controle = Controle;
                    MvProducao.LerDados(Mapa.IdMvMatPrima);
                    
                    SqlDataReader Tab;
                    if (MvProducao.IdMov > 0)
                    {
                        Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvProducao.IdMov.ToString());
                        ControleEstoque.MovimentoEstoque(Tab, 2, 1, false, MvProducao.TpMov, MvProducao.DtEntSai,MvProducao.IdFilial);
                        MvProducao.Cancelar();
                    }   

                    Mapa.Status = 0;
                    Mapa.GravarDados();
                    PopularCampos(Mapa.IdMapa);
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdProdProd.ToString(), 6, "Cancelamento do Mapa de Produção: " + LstProduto.SelectedText.ToString().Trim() + " Qtde: " + Mapa.Qtde.ToString());
                    MessageBox.Show("Mapa de Produção Cancelado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BaixaExtraMatPrima()
        {
            try
            {
                //Atualizando o Estoque dos Produtos Envasados                        
                MvEstoque MvProducao = new MvEstoque();
                MvEstoqueItens MvItens = new MvEstoqueItens();

                Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                ControleEstoque.Controle = Controle;

                MvItens.Controle = Controle;
                MvProducao.Controle = Controle;
                MvProducao.LerDados(0);

                //Gerando o Movimento Lanc. Produção
                DataSet TabPrd = new DataSet();
                TabPrd = Controle.ConsultaTabela("SELECT * FROM MapaProducaoProdutos WHERE QTDEEXTRA > 0 AND ID_MAPA=" + Mapa.IdMapa.ToString());
                if (TabPrd.Tables[0].Rows.Count > 0)
                {
                    MvProducao.LerDados(0);
                    MvProducao.TpMov = "BMAPR";
                    MvProducao.Observacao = "Movimento extra Gerado Automatico Pelo Sistema Ref. Mapa de Produção No." + string.Format("{0:D6}", Mapa.IdMapa);
                    MvProducao.Data = Mapa.Data;
                    MvProducao.IdFilial = FrmPrincipal.IdFilialConexao;
                    MvProducao.DtEmissao = Mapa.Data;
                    MvProducao.DtEntSai = Mapa.Data;
                    MvProducao.Documento = "PRODUÇÂO AUT.";
                    MvProducao.NumDocumento = string.Format("{0:D6}", Mapa.IdMapa);
                    MvProducao.GravarDados();

                    for (int I = 0; I <= TabPrd.Tables[0].Rows.Count - 1; I++)
                    {
                        MvItens.LerDados(0);
                        MvItens.IdMov     = MvProducao.IdMov;
                        MvItens.IdProduto = int.Parse(TabPrd.Tables[0].Rows[I]["ID_PRODUTO"].ToString());
                        MvItens.Qtde      = decimal.Parse(TabPrd.Tables[0].Rows[I]["QtdeExtra"].ToString());
                        MvItens.GravarDados();
                    }
                }

                //Concluindo o Movimento de Produção
                SqlDataReader Tab;
                if (MvProducao.IdMov > 0)
                {
                    Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvProducao.IdMov.ToString());
                    ControleEstoque.MovimentoEstoque(Tab, 1, 2, false, MvProducao.TpMov, MvProducao.DtEntSai, MvProducao.IdFilial);
                    MvProducao.Concluir();
                }
                Mapa.Status = 1;
                Mapa.GravarDados();
                FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdProdProd.ToString(), 4, "Baixa Extra da Produção: " + LstProduto.SelectedText.ToString().Trim() + " Qtde: " + Mapa.Qtde.ToString());
                
            }
            catch
            {
                MessageBox.Show("Erro ao enviar o Mapa para Produção", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
