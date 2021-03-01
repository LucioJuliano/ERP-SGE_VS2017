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
using CDSSoftware;
using System.Collections;
using System.Net;
using System.Net.Mail;
namespace ERP_SGE
{
    public partial class FrmPromocaoPrd : Form
    {

        Funcoes Controle                    = new Funcoes();
        PromocaoProdutos Promocao           = new PromocaoProdutos();
        PromocaoProdutosItens PromocaoItens = new PromocaoProdutosItens();
        Auditoria RegAuditoria              = new Auditoria();
        FrmBuscaProduto BuscaPrd            = new FrmBuscaProduto();        

        private DataSet TabItens;
        private BindingSource Source_Itens;

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private BindingSource Source_Promocao;

        public FrmPromocaoPrd()
        {
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao       = FrmPrincipal.Conexao;
            Promocao.Controle      = Controle;
            PromocaoItens.Controle = Controle;
            RegAuditoria.Controle  = Controle;
            Source_Promocao        = new BindingSource();
            TabItens               = new DataSet();
            Source_Itens           = new BindingSource();
            BuscaPrd.FrmPrincipal  = this.FrmPrincipal;
            Chk_Periodo.Checked    = false;
            TxtCodPrd.Text         = "0";
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            PopularGrid();
        }

        private void PopularGrid()
        {
            string Filtro = " WHERE 1=1";

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND CONVERT(DATETIME,T1.DTINICIO,103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,T1.DTFINAL,103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqPrd.Text.Trim() != "")
                Filtro = Filtro + " AND T2.DESCRICAO LIKE '%" + TxtPesqPrd.Text.Trim() + "%'";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_Promocao,T1.Descricao,T1.DTINICIO,T1.DTFINAL,CASE T1.ATIVO WHEN 1 THEN 'SIM' ELSE 'NÃO' END AS ATIVO FROM PROMOCAOPRODUTOS T1 " + Filtro + "ORDER BY T1.DESCRICAO,T1.DTINICIO DESC");
            Source_Promocao.DataSource = Tabela;
            Source_Promocao.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource       = Source_Promocao;
            int Lanc = Source_Promocao.Find("Id_Promocao", Promocao.IdPromocao);
            Source_Promocao.Position = Lanc;
        }

        private void PopularCampos(int Isn)
        {
            Promocao.LerDados(Isn);
            TxtCodigo.Text      = string.Format("{0:D5}", int.Parse(Promocao.IdPromocao.ToString()));
            txtDtInicio.Value   = Promocao.DtInicio;
            txtDtFinal.Value    = Promocao.DtFinal;
            TxtPromocao.Text    = Promocao.Descricao;
            TxtAutorizado.Text  = Promocao.Autorizado;
            TxtObservacao.Text  = Promocao.Observacao;
            TxtIdServidor.Text  = string.Format("{0:D4}", int.Parse(Promocao.IdServidor.ToString()));
            Cb_Ativo.Checked    = Promocao.Ativo == 1;
            Cb_Segunda.Checked  = Promocao.Segunda == 1;
            Cb_Terca.Checked    = Promocao.Terca == 1;
            Cb_Quarta.Checked   = Promocao.Quarta == 1;
            Cb_Quinta.Checked   = Promocao.Quinta == 1;
            Cb_Sexta.Checked    = Promocao.Sexta == 1;
            Cb_Sabado.Checked   = Promocao.Sabado == 1;
            Cb_Domingo.Checked  = Promocao.Domingo == 1;
            Cb_DescSegUnd.Checked = Promocao.DescSegUnd == 1;
            Ck_TipoPrc.Checked  = Promocao.TipoPromocao == 0;
            Ck_TipoQtde.Checked = Promocao.TipoPromocao == 1;
            Ck_TipoVlrPed.Checked  = Promocao.TipoPromocao == 2;
            Ck_TipoProduto.Checked = Promocao.TipoPromocao == 3;
            Ck_TipoQtdPrc.Checked = Promocao.TipoPromocao == 4;
            Cb_PorUsuario.Checked = Promocao.PorUsuario == 1;
            PopularProduto(Promocao.IdProduto);
            TxtQtdeTotal.Value  = Promocao.QtdeTotal;
            TxtQtdeItem.Value   = Promocao.QtdeItem;
            TxtPDescItem.Value  = Promocao.PDesc;
            TxtPComissao.Value  = Promocao.PComissao;
            TxtVlrPedido.Value  = Promocao.VlrPedido;
            TxtQtdeSen.Value = Promocao.QtdeSen;
            TxtQtdeEsp.Value = Promocao.QtdeEsp;
            TxtQtdeVar.Value = Promocao.QtdeVar;
            TxtQtdeMin.Value = Promocao.QtdeMin;
            TxtQtdeAta.Value = Promocao.QtdeAta;
            LstTipoCliente.SelectedIndex = Promocao.TipoCliente;
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();            
            PopularCampos(0);
            PopularGridItens();
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
                PopularGridItens();
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtPromocao.Text.Trim()!="")
            {                
                Promocao.IdPromocao = int.Parse(TxtCodigo.Text);
                Promocao.Descricao  = TxtPromocao.Text;
                Promocao.DtInicio   = txtDtInicio.Value;
                Promocao.DtFinal    = txtDtFinal.Value;
                Promocao.Autorizado = TxtAutorizado.Text;
                Promocao.Observacao = TxtObservacao.Text;
                Promocao.QtdeTotal  = int.Parse(TxtQtdeTotal.Value.ToString());
                Promocao.QtdeItem   = int.Parse(TxtQtdeItem.Value.ToString());
                Promocao.QtdeSen    = int.Parse(TxtQtdeSen.Value.ToString());
                Promocao.QtdeEsp    = int.Parse(TxtQtdeEsp.Value.ToString());
                Promocao.QtdeVar    = int.Parse(TxtQtdeVar.Value.ToString());
                Promocao.QtdeMin    = int.Parse(TxtQtdeMin.Value.ToString());
                Promocao.QtdeAta    = int.Parse(TxtQtdeAta.Value.ToString());
                Promocao.PDesc      = TxtPDescItem.Value;
                Promocao.PComissao  = TxtPComissao.Value;
                Promocao.VlrPedido  = TxtVlrPedido.Value;
                Promocao.TipoCliente = LstTipoCliente.SelectedIndex;
                Promocao.IdProduto   = int.Parse(TxtCodPrd.Text);
                if (Cb_Ativo.Checked)   Promocao.Ativo   = 1;   else Promocao.Ativo  = 0;
                if (Cb_Segunda.Checked) Promocao.Segunda = 1; else Promocao.Segunda  = 0;
                if (Cb_Terca.Checked)   Promocao.Terca   = 1;   else Promocao.Terca  = 0;
                if (Cb_Quarta.Checked)  Promocao.Quarta  = 1;  else Promocao.Quarta  = 0;
                if (Cb_Quinta.Checked)  Promocao.Quinta  = 1;  else Promocao.Quinta  = 0;
                if (Cb_Sexta.Checked)   Promocao.Sexta   = 1;   else Promocao.Sexta  = 0;
                if (Cb_Sabado.Checked)  Promocao.Sabado  = 1;  else Promocao.Sabado  = 0;
                if (Cb_Domingo.Checked) Promocao.Domingo = 1; else Promocao.Domingo  = 0;
                if (Cb_DescSegUnd.Checked) Promocao.DescSegUnd = 1; else Promocao.DescSegUnd = 0;
                if (Ck_TipoPrc.Checked)     Promocao.TipoPromocao = 0;
                if (Ck_TipoQtde.Checked)    Promocao.TipoPromocao = 1;
                if (Ck_TipoVlrPed.Checked)  Promocao.TipoPromocao = 2;
                if (Ck_TipoProduto.Checked) Promocao.TipoPromocao = 3;
                if (Ck_TipoQtdPrc.Checked)  Promocao.TipoPromocao = 4;
                if (Cb_PorUsuario.Checked) Promocao.PorUsuario = 1; else Promocao.PorUsuario = 0;
                Promocao.GravarDados();
                StaFormEdicao = false;
                //Registrando Movimento de Auditoria
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdPromocao, Promocao.IdPromocao.ToString(), 1, "Inclusão: " + TxtPromocao.Text);
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdPromocao, Promocao.IdPromocao.ToString(), 2, "Alteração: " + TxtPromocao.Text);
                PopularGrid();
                PopularGridItens();
                PopularCampos(Promocao.IdPromocao);
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Favor informe o Nome da Promoção", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                Promocao.IdPromocao = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Promocao.LerDados(Promocao.IdPromocao);


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
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Promocao.IdPromocao = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Promocao.Excluir();
                    //Registrando Movimento de Auditoria                    
                    FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdPromocao, Promocao.IdPromocao.ToString(), 3, "Exclusão: " + TxtPromocao.Text);
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
            TxtCodigo.Text        = "0";
            txtDtInicio.Value     = DateTime.Now;
            txtDtFinal.Value      = DateTime.Now;
            TxtPromocao.Text      = "";
            TxtAutorizado.Text    = "";
            TxtObservacao.Text    = "";
            TxtIdServidor.Text    = "0";
            Cb_Ativo.Checked      = false;
            Cb_Segunda.Checked    = false;
            Cb_Terca.Checked      = false;
            Cb_Quarta.Checked     = false;
            Cb_Quinta.Checked     = false;
            Cb_Sexta.Checked      = false;
            Cb_Sabado.Checked     = false;
            Cb_Domingo.Checked    = false;
            Ck_TipoPrc.Checked    = true;
            Ck_TipoVlrPed.Checked = false;
            Ck_TipoQtde.Checked   = false;
            Cb_DescSegUnd.Checked = false;
            Cb_PorUsuario.Checked = false;
            TxtQtdeTotal.Value    = 0;
            TxtQtdeItem.Value     = 0;
            TxtPDescItem.Value    = 0;
            TxtPComissao.Value    = 0;
            TxtVlrPedido.Value    = 0;
            TxtCodPrd.Text        = "0";
            TxtDescricao.Text     = "";
            TxtQtdeSen.Value = 0;
            TxtQtdeEsp.Value = 0;
            TxtQtdeVar.Value = 0;
            TxtQtdeMin.Value = 0;
            TxtQtdeAta.Value = 0;
            LstTipoCliente.SelectedIndex = 0;
            Promocao.LerDados(0);
            PopularCampos(Promocao.IdPromocao);
            PopularGridItens();
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
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO,T1.PRCATACADO,T1.PRCMINIMO,T1.PRCVAREJO,T1.PRCESPECIAL,T1.PRCSENSACIONAL,T1.ATIVO FROM PROMOCAOPRODUTOSITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_PROMOCAO=" + Promocao.IdPromocao.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int Lanc = Source_Itens.Find("ID_Lanc", PromocaoItens.IdLanc);
            Source_Itens.Position = Lanc;
        }
        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    IncluirLanc();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro de Promoção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
            {
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (Promocao.IdPromocao == 0)
                {
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                }
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Promocao.IdPromocao > 0)
                {
                    PromocaoItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    string TxtGravar = "(" + PromocaoItens.PrcAtacado.ToString() + " A-> " + GridItens.CurrentRow.Cells[3].Value.ToString() + ")  (" + PromocaoItens.PrcMinimo.ToString() + " M-> " + GridItens.CurrentRow.Cells[4].Value.ToString() + ")  (" +
                                       PromocaoItens.PrcVarejo.ToString() + " V-> " + GridItens.CurrentRow.Cells[5].Value.ToString() + ")  (" + PromocaoItens.PrcEspecial.ToString() + " E-> " + GridItens.CurrentRow.Cells[6].Value.ToString() + " S-> " + GridItens.CurrentRow.Cells[7].Value.ToString() + ")";                    
                    PromocaoItens.PrcAtacado     = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                    PromocaoItens.PrcMinimo      = decimal.Parse(GridItens.CurrentRow.Cells[4].Value.ToString());
                    PromocaoItens.PrcVarejo      = decimal.Parse(GridItens.CurrentRow.Cells[5].Value.ToString());
                    PromocaoItens.PrcEspecial    = decimal.Parse(GridItens.CurrentRow.Cells[6].Value.ToString());
                    PromocaoItens.PrcSensacional = decimal.Parse(GridItens.CurrentRow.Cells[7].Value.ToString());
                    PromocaoItens.Ativo          = int.Parse(GridItens.CurrentRow.Cells[8].Value.ToString());
                    PromocaoItens.GravarDados();
                    FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdPromocao, PromocaoItens.IdProduto.ToString(), 2, "Alteração do Item: "+TxtGravar);
                    PopularGridItens();                    
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Promoção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Promocao.IdPromocao > 0)
                    IncluirLanc();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Promoção em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Promocao.IdPromocao > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (MessageBox.Show("Confirma a Exclusão do Lanc", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            PromocaoItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                            FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdPromocao, PromocaoItens.IdProduto.ToString(), 3, "Exclusão do Item");
                            PromocaoItens.Excluir();
                            PromocaoItens.IdLanc = 0;
                            PopularGridItens();
                        }
                    }
                }
            }
        }
        private void IncluirLanc()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do produto em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Promocao.IdPromocao > 0)
                {

                    FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                    BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                    BuscaPrd.IdProduto       = 0;
                    BuscaPrd.LstMvEst        = true;
                    BuscaPrd.ShowDialog();
                    if (BuscaPrd.IdProduto > 0)
                    {
                        PromocaoItens.PrcAtacado  = BuscaPrd.CadProd.PrcAtacado;
                        PromocaoItens.PrcMinimo   = BuscaPrd.CadProd.PrcMinimo;
                        PromocaoItens.PrcVarejo   = BuscaPrd.CadProd.PrcVarejo;
                        PromocaoItens.PrcEspecial = BuscaPrd.CadProd.PrcEspecial;
                        PromocaoItens.PrcSensacional = BuscaPrd.CadProd.PrcSensacional;
                        PromocaoItens.IdLanc      = 0;
                        PromocaoItens.IdPromocao  = Promocao.IdPromocao;
                        PromocaoItens.IdProduto   = BuscaPrd.IdProduto;
                        PromocaoItens.Ativo       = 1;
                        PromocaoItens.GravarDados();
                        PopularGridItens();
                        GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];
                        FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdPromocao, PromocaoItens.IdProduto.ToString(), 1, "Inclusão Itens: " + BuscaPrd.CadProd.Descricao.Trim());
                    }
                    else
                        Source_Itens.CancelEdit();
                    BuscaPrd.Dispose();
                }
            }
        }

        private void BtnBuscaPrd_Click(object sender, EventArgs e)
        {
            BuscaPrd.IdProduto = 0;
            BuscaPrd.ShowDialog();
            if (BuscaPrd.IdProduto > 0)
            {                
                TxtCodPrd.Text = BuscaPrd.CadProd.IdProduto.ToString();
                TxtDescricao.Text = BuscaPrd.CadProd.Descricao;
            }
            else
                TxtCodPrd.Text = "0";
        }

        private void PopularProduto(int Prd)
        {
            Produtos CadPrd = new Produtos();
            CadPrd.Controle = Controle;
            CadPrd.LerDados(Prd);
            if (CadPrd.IdProduto > 0)
            {
                TxtCodPrd.Text    = CadPrd.IdProduto.ToString();
                TxtDescricao.Text = CadPrd.Descricao;
            }
            else
            {
                TxtCodPrd.Text    = "0";
                TxtDescricao.Text = "";
            }
        }
             
    }
}
