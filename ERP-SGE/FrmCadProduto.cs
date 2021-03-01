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
using System.Drawing.Imaging;
using System.IO;


namespace ERP_SGE
{
    public partial class FrmCadProduto : Form
    {
        Funcoes Controle          = new Funcoes();
        Produtos Produto          = new Produtos();
        ProdutosKitItens Kits     = new ProdutosKitItens();        
        ReducaoFiscal RedFiscal   = new ReducaoFiscal();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private int IdGrupo = 0;

        private DataSet TabItens;
        private BindingSource Source_Itens;

       
        private DataSet TabComodItens;
        private BindingSource Source_ComodItens;

        public FrmCadProduto()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;
            Produto.Controle      = Controle;
            Kits.Controle         = Controle;
            RedFiscal.Controle    = Controle;            
            Produto.IdProduto     = 0;
            TabItens              = new DataSet();
            Source_Itens          = new BindingSource();
            
            LstPesquisa.SelectedIndex = 1;
            CampoLista();
            PopularGridGrp();
            TxtDtCadastro.Value    = DateTime.Now;
            //BtnLimpaMov.Visible    = FrmPrincipal.Perfil_Usuario.LimpaEstoque == 1;
            BtnMovPrd.Visible      = FrmPrincipal.Perfil_Usuario.AtualizaEstoque == 1;

            if (FrmPrincipal.Perfil_Usuario.UsaPrcEspDist==0)
            {
                LblPrcEspDist.Visible = false;
                TxtPrcEspDist.Visible = false;
                GridDados.Columns[9].Visible = false;
            }

        }
        private void CampoLista()
        {
            LstGrupo       = FrmPrincipal.PopularCombo("SELECT Id_Grupo,Grupo FROM GrupoProduto ORDER BY Grupo", LstGrupo);
            LstUnidade     = FrmPrincipal.PopularCombo("SELECT CHAVE,SUBSTRING(DESCRICAO,1,30) AS DESCRICAO FROM TABELASAUX WHERE CAMPO='UNIDADE' ORDER BY DESCRICAO", LstUnidade);
            LstGenero      = FrmPrincipal.PopularCombo("SELECT ID_GENERO,SUBSTRING(GENERO,1,90) AS GENERO FROM GENEROPRODUTO ORDER BY GENERO", LstGenero);
            LstCfopVD      = FrmPrincipal.PopularCombo("SELECT Id_Cfop,Cfop+'  '+Natureza FROM Cfop where substring(cfop,1,1)=5 ORDER BY Natureza", LstCfopVD);
            LstCfopVF      = FrmPrincipal.PopularCombo("SELECT Id_Cfop,Cfop+'  '+Natureza FROM Cfop where substring(cfop,1,1)=6 ORDER BY Natureza", LstCfopVF);
            LstCfopED      = FrmPrincipal.PopularCombo("SELECT Id_Cfop,Cfop+'  '+Natureza FROM Cfop where substring(cfop,1,1)=1 ORDER BY Natureza", LstCfopED);
            LstCfopEF      = FrmPrincipal.PopularCombo("SELECT Id_Cfop,Cfop+'  '+Natureza FROM Cfop where substring(cfop,1,1)=2 ORDER BY Natureza", LstCfopEF);
            ColLstGrupo    = FrmPrincipal.PopularComboGrid("SELECT Id_Grupo,Grupo FROM GrupoProduto ORDER BY Grupo", ColLstGrupo);
            LstReducao     = FrmPrincipal.PopularCombo("SELECT ID_REDUCAO,rtrim(CodRed+' - '+Rtrim(CONVERT(char,Perc))+'  '+RefReducao) as Reducao from REDUCAOFISCAL", LstReducao);
            LstPromocaoPrd = FrmPrincipal.PopularCombo("SELECT Id_Promocao,SUBSTRING(DESCRICAO,1,70) as descricao FROM PromocaoProdutos WHERE TipoPromocao=1", LstPromocaoPrd);
        }
        private void PopularGridGrp()
        {
            DataSet TabGrp = new DataSet();
            TabGrp = Controle.ConsultaTabela("SELECT Id_Grupo,Grupo,Ativo  FROM GrupoProduto ORDER BY Grupo");
            GridGrupos.DataSource = TabGrp;
            GridGrupos.DataMember = TabGrp.Tables[0].TableName;
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            if (TxtPesquisa.Text.Trim() != "")
            {                
                if (LstPesquisa.SelectedIndex == 0)
                    Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,T1.SaldoEstoque,T1.PrcSensacional,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado,T1.PrcEspDist,T1.UltPrcCompra,T1.NCM,T1.Ativo,T1.ID_GRUPO FROM Produtos T1  WHERE T1.Referencia LIKE '%{0}%'  ORDER BY T1.Referencia", TxtPesquisa.Text.Trim()));
                else if (LstPesquisa.SelectedIndex == 1)
                    Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,T1.SaldoEstoque,T1.PrcSensacional,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado,T1.PrcEspDist,T1.UltPrcCompra,T1.NCM,T1.Ativo,T1.ID_GRUPO FROM Produtos T1  WHERE T1.Descricao LIKE '%{0}%' ORDER BY T1.DESCRICAO", TxtPesquisa.Text.Trim()));
                else if (LstPesquisa.SelectedIndex == 2)
                    Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,T1.SaldoEstoque,T1.PrcSensacional,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado,T1.PrcEspDist,T1.UltPrcCompra,T1.NCM,T1.Ativo,T1.ID_GRUPO FROM Produtos T1  WHERE T1.RefFornecedor LIKE '%{0}%' ORDER BY T1.RefFornecedor", TxtPesquisa.Text.Trim()));
                else if (LstPesquisa.SelectedIndex == 3)
                    Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,T1.SaldoEstoque,T1.PrcSensacional,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado,T1.PrcEspDist,T1.UltPrcCompra,T1.NCM,T1.Ativo,T1.ID_GRUPO FROM Produtos T1  WHERE T1.CodBarra LIKE '%{0}%' ORDER BY T1.CodBarra", TxtPesquisa.Text.Trim()));
            }
            else
                Tabela = Controle.ConsultaTabela("SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,T1.SaldoEstoque,T1.PrcSensacional,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado,T1.PrcEspDist,T1.UltPrcCompra,T1.NCM,T1.Ativo,T1.ID_GRUPO FROM Produtos T1  WHERE T1.Id_Grupo=" + IdGrupo.ToString() + " ORDER BY T1.Descricao");                        
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Produto", Produto.IdProduto);
            Source.Position = item;
    
            
        }
        private void PopularCampos(int Isn)
        {
            //Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Produto.LerDados(Isn);
            TxtDtUltAlt.Visible = true;
            TxtDtUltCompra.Visible = false;
            TxtCodigo.Text = Produto.IdProduto.ToString();
            TxtReferencia.Text = Produto.Referencia;
            TxtDescricao.Text = Produto.Descricao;
            TxtDescResumida.Text = Produto.DescResumida;
            LstGrupo.SelectedValue = Produto.IdGrupo.ToString();
            LstCfopVD.SelectedValue = Produto.IdCfopVD.ToString();
            LstCfopVF.SelectedValue = Produto.IdCfopVF.ToString();
            LstCfopED.SelectedValue = Produto.IdCfopED.ToString();
            LstCfopEF.SelectedValue = Produto.IdCfopEF.ToString();
            Ck_EnviarEmail.Checked = Produto.EnviarEmail == 1;;
            if (Produto.Tipo == 0) Rb_Produto.Checked = true; else Rb_Servico.Checked = true;
            if (Produto.ProdutoKit == 0) Chk_ProdutoKit.Checked = false; else Chk_ProdutoKit.Checked = true;
            if (Produto.QtdeCxDist == 0) Cb_QtdeCxDist.Checked = false; else Cb_QtdeCxDist.Checked = true;                        
            TxtRefFornecedor.Text     = Produto.RefFornecedor;
            TxtCodBarra.Text          = Produto.CodBarra;
            TxtIcmsIss.Value          = Produto.IcmsIss;
            TxtReducao.Value          = Produto.Reducao;
            LstSitTrib.SelectedIndex  = Produto.SitTributaria;
            LstReducao.SelectedValue  = Produto.IdReducao;
            TxtEstMinimo.Value        = Produto.EstMinimo;
            TxtEstMaximo.Value        = Produto.EstMaximo;
            TxtPesoBruto.Value        = Produto.PesoBruto;
            TxtPesoLiquido.Value      = Produto.PesoLiquido;
            LstUnidade.SelectedValue  = Produto.Unidade.ToString().Trim();
            TxtPrcEspecial.Value      = Produto.PrcEspecial;
            TxtPrcMinimo.Value        = Produto.PrcMinimo;
            TxtPrcVarejo.Value        = Produto.PrcVarejo;
            TxtPrcAtacado.Value       = Produto.PrcAtacado;
            TxtPrcEspDist.Value       = Produto.PrcEspDist;
            TxtPrcSensacional.Value   = Produto.PrcSensacional;
            TxtUltPrcCompra.Value     = Produto.UltPrcCompra;                        
            TxtDtCadastro.Value       = Produto.DtCadastro.Date;
            TxtDtUltAlt.Value         = Produto.DtAlteracao.Date;
            TxtNCM.Text               = Produto.NCM;
            TxtQtdeCaixa.Value        = Produto.QtdeCaixa;
            TxtQtdeUnd.Value          = Produto.QtdeUnd;
            TxtPontos.Value           = Produto.Pontos;
            TxtLocEstRua.Value        = Produto.LocEstRua;
            TxtPalete.Text            = Produto.Palete;
            LstGenero.SelectedValue   = Produto.IdGenero.ToString();
            LstPromocaoPrd.SelectedValue = Produto.IdPromocao.ToString();
            TxtDetProduto.Text        = Produto.DetProduto;
            if (Produto.DtUltCompra.Date.Year > 1)
            {
                TxtDtUltCompra.Visible = true;
                TxtDtUltCompra.Value = Produto.DtUltCompra.Date;
            }            
            TxtObservacao.Text = Produto.Observacao;
            LerFoto();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (TabGrupo.SelectedIndex == 0)
                    TabGrupo.SelectTab(1);

                StaFormEdicao = true;
                Paginas.SelectTab(1);
                LimpaDados();
                PopularGridItens();                
                FrmPrincipal.ControleBotoes(true);
                TxtIcmsIss.Value = 17;
                TxtReferencia.Enabled = true;
                TxtReferencia.Focus();
            }
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
                if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                    MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Paginas.SelectTab(1);
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                    PopularGridItens();                    
                    StaFormEdicao = true;
                    TxtReferencia.Enabled = false;
                    FrmPrincipal.ControleBotoes(true);
                    TxtReferencia.Focus();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            bool AlteracaoCadastro = true;
            bool AlteracaoPrc      = false;
            bool AltPrcDist        = false;
            int Ativo              = 0;
            

            if (Produto.PrcAtacado != TxtPrcAtacado.Value || Produto.PrcEspecial != TxtPrcEspecial.Value || Produto.PrcMinimo != TxtPrcMinimo.Value || Produto.PrcVarejo != TxtPrcVarejo.Value)
                AlteracaoPrc = true;

            if (int.Parse(LstGrupo.SelectedValue.ToString()) != Produto.IdGrupo)
                AlteracaoCadastro = true;
            
            if (Produto.PrcAtacado != TxtPrcAtacado.Value)
                AltPrcDist = true;

            if (TxtDescricao.Text.Trim() != "") 
            {
                Produto.IdProduto     = int.Parse(TxtCodigo.Text);                
                Produto.Referencia    = TxtReferencia.Text;
                Produto.Descricao     = TxtDescricao.Text;
                Produto.DescResumida  = TxtDescResumida.Text;
                Produto.IdGrupo       = int.Parse(LstGrupo.SelectedValue.ToString());
                Produto.IdGenero      = int.Parse(LstGenero.SelectedValue.ToString());
                Produto.IdCfopVD      = int.Parse(LstCfopVD.SelectedValue.ToString());
                Produto.IdCfopVF      = int.Parse(LstCfopVF.SelectedValue.ToString());
                Produto.IdCfopED      = int.Parse(LstCfopED.SelectedValue.ToString());
                Produto.IdCfopEF      = int.Parse(LstCfopEF.SelectedValue.ToString());
                Produto.RefFornecedor = TxtRefFornecedor.Text;
                Produto.CodBarra      = TxtCodBarra.Text;
                Produto.IcmsIss       = TxtIcmsIss.Value;
                Produto.Reducao       = TxtReducao.Value;
                Produto.SitTributaria = LstSitTrib.SelectedIndex;
                Produto.EstMinimo     = TxtEstMinimo.Value;
                Produto.EstMaximo     = TxtEstMaximo.Value;
                Produto.PesoBruto     = TxtPesoBruto.Value;
                Produto.PesoLiquido   = TxtPesoLiquido.Value;
                Produto.Unidade       = LstUnidade.SelectedValue.ToString();
                Produto.IdReducao     = int.Parse(LstReducao.SelectedValue.ToString());
                Produto.PrcMinimo     = TxtPrcMinimo.Value;
                Produto.PrcVarejo     = TxtPrcVarejo.Value;
                Produto.PrcEspecial   = TxtPrcEspecial.Value;
                Produto.PrcEspDist    = TxtPrcEspDist.Value;
                Produto.PrcSensacional = TxtPrcSensacional.Value;
                Produto.PrcAtacado    = TxtPrcAtacado.Value;
                Produto.UltPrcCompra  = TxtUltPrcCompra.Value;                                
                Produto.DtCadastro    = TxtDtCadastro.Value;
                Produto.DtUltCompra   = TxtDtUltCompra.Value;
                //Produto.DtUltVenda  = TxtDtUltAlt.Value;
                Produto.Observacao    = TxtObservacao.Text;                
                Produto.NCM           = TxtNCM.Text;                
                Produto.QtdeCaixa     = int.Parse(TxtQtdeCaixa.Value.ToString());
                Produto.QtdeUnd       = int.Parse(TxtQtdeUnd.Value.ToString());
                Produto.IdPromocao    = int.Parse(LstPromocaoPrd.SelectedValue.ToString());
                Produto.Pontos        = int.Parse(TxtPontos.Value.ToString());
                Produto.LocEstRua     = int.Parse(TxtLocEstRua.Value.ToString());
                Produto.Palete        = TxtPalete.Text;
                Produto.DetProduto    = TxtDetProduto.Text;
                if (Ck_EnviarEmail.Checked) Produto.EnviarEmail = 1; else Produto.EnviarEmail = 0;
                if (Chk_ProdutoKit.Checked) Produto.ProdutoKit  = 1; else Produto.ProdutoKit = 0;
                if (Rb_Produto.Checked) Produto.Tipo = 0; else Produto.Tipo = 1;                
                if (Cb_QtdeCxDist.Checked) Produto.QtdeCxDist = 1; else Produto.QtdeCxDist = 0;
                Produto.DtAlteracao = DateTime.Now;

                if (AlteracaoPrc)
                    Produto.DtAltPrc = DateTime.Now;

                Produto.GravarDados();

                if (TxtCodigo.Text == "0")
                    FrmPrincipal.RegistrarAuditoria(this.Text, Produto.IdProduto, Produto.Referencia, 1, "Inclusão do Produto");
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Produto.IdProduto, Produto.Referencia, 2, "Alteração do Produto");

                // Aviso de alteração no cadastro
                if (Produto.EnviarEmail == 1 && !FrmPrincipal.VersaoDistribuidor)
                {
                    if (int.Parse(TxtCodigo.Text) == 0)
                        AvisoEmail("Novo Produto  Referencia: " + Produto.Referencia.Trim() + " - " + Produto.Descricao.Trim() + "\n Prç.Varejo:  " + string.Format("{0:n2}", TxtPrcVarejo.Value) + "\n Prç.Minimo:  " + string.Format("{0:n2}", TxtPrcMinimo.Value) + "\n Prç.Destribuidor:  " + string.Format("{0:n2}", TxtPrcAtacado.Value));
                    else
                    {
                        if (AlteracaoCadastro)
                            AvisoEmail("Alteração do Produto Referencia: " + Produto.Referencia.Trim() + " - " + Produto.Descricao.Trim() + "\n Prç.Especial:  " + string.Format("{0:n2}", TxtPrcEspecial.Value) + "\n Prç.Varejo:  " + string.Format("{0:n2}", TxtPrcVarejo.Value) + "\n Prç.Minimo:  " + string.Format("{0:n2}", TxtPrcMinimo.Value) + "\n Prç.Destribuidor:  " + string.Format("{0:n2}", TxtPrcAtacado.Value));

                        if (AltPrcDist)
                            AvisoEmailDistribuidor("(Alteração de Preço) Referencia: " + Produto.Referencia.Trim() + " - " + Produto.Descricao.Trim() + "\n  Prç.Destribuidor:  " + string.Format("{0:n2}", TxtPrcAtacado.Value) + "\n Sugestão do Preço Venda:  " + string.Format("{0:n2}", TxtPrcEspecial.Value));
                    }
                }
                PopularGrid();
                PopularCampos(Produto.IdProduto);
                PopularGridItens();
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Informe a Descrição do Produto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtReferencia.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                    MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
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
                        Produto.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));                                                
                        Produto.Excluir();
                        FrmPrincipal.RegistrarAuditoria(this.Text, Produto.IdProduto, Produto.Referencia, 3, "Exclusão do Produto");
                        PopularGrid();
                        LimpaDados();
                        GridDados.Focus();
                    }
                }
            }
            else
                MessageBox.Show("Não existe Registro para Excluir", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            StaFormEdicao = false;
            PopularCampos(0);
            FrmPrincipal.ControleBotoes(false);
        }
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        /*private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
            PopularGrid();
        }*/
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
            FrmPrincipal.ClickBtnNovo += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
            FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);            
        }
        private void LimpaDados()
        {
            TxtCodigo.Text           = "0";
            TxtReferencia.Text       = "";
            TxtDescricao.Text        = "";
            TxtDescResumida.Text     = "";
            LstGrupo.SelectedValue   = IdGrupo;
            LstGenero.SelectedValue  = "0";
            LstCfopVD.SelectedValue  = "0";
            LstCfopVF.SelectedValue  = "0";
            LstCfopED.SelectedValue  = "0";
            LstCfopEF.SelectedValue  = "0";
            Ck_EnviarEmail.Checked   = false;            
            Cb_QtdeCxDist.Checked    = false;
            Rb_Produto.Checked       = true;
            TxtRefFornecedor.Text    = "";
            TxtCodBarra.Text         = "";
            TxtIcmsIss.Value         = 0;
            TxtReducao.Value         = 0;
            LstSitTrib.SelectedIndex = 0;
            TxtEstMinimo.Value       = 0;
            TxtEstMaximo.Value       = 0;
            TxtPesoBruto.Value       = 0;
            TxtPesoLiquido.Value     = 0;
            LstUnidade.SelectedValue = 0;
            TxtPrcMinimo.Value       = 0;
            TxtPrcVarejo.Value       = 0;
            TxtPrcAtacado.Value      = 0;
            TxtPrcEspecial.Value     = 0;
            TxtPrcEspDist.Value      = 0;
            TxtUltPrcCompra.Value    = 0;                        
            TxtDtCadastro.Value      = DateTime.Now;
            TxtDtUltCompra.Visible   = false;
            TxtDtUltAlt.Visible      = false;
            TxtObservacao.Text       = "";            
            TxtQtdeCaixa.Value       = 0;
            TxtQtdeUnd.Value         = 0;
            TxtPontos.Value          = 0;
            TxtNCM.Text              = "";
            TxtPalete.Text           = "";
            TxtLocEstRua.Value       = 0;
            TxtDetProduto.Text       = "";
            PicFoto.Image            = null;
            Chk_ProdutoKit.Checked   = false;
            TxtReferencia.Enabled    = false;
            LstPromocaoPrd.SelectedValue = 0;
            Produto.LerDados(0);
            PopularGridItens();
            
        }
        private void Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[0].Value.ToString() != "")
                    BtnEditar_Click(FrmPrincipal.BtnEditar, null);
            }*/
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
                    //BtnEditar_Click(FrmPrincipal.BtnEditar, null);
                }
            }
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();            
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
        private void TabGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabGrupo.SelectedIndex == 1)
            {
                if (GridGrupos.CurrentRow != null)
                {
                    if (GridGrupos.CurrentRow.Cells[0].Value.ToString() != "" && !StaFormEdicao)
                    {
                        IdGrupo = int.Parse(GridGrupos.CurrentRow.Cells[0].Value.ToString());
                        Paginas.SelectTab(0);
                        PopularGrid();
                    }
                }
                else
                {
                    MessageBox.Show("Não existe Grupo Selecionado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TabGrupo.SelectTab(0);
                }
            }
            else
            {
                if (StaFormEdicao)
                {
                    MessageBox.Show("Cadastro de Produtos em Edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TabGrupo.SelectTab(1);
                }
            }
        }     
        private void TxtComposicao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                TxtReferencia.Focus();
        }

        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
        //Produtos Kit
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VALOR FROM PRODUTOSKIT T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_PRDMASTER=" + Produto.IdProduto.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("ID_Item", Kits.IdItem);
            Source_Itens.Position = item;            
        }
        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    IncluirItem();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro do produto em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (Produto.IdProduto == 0)
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
                if (Produto.IdProduto > 0)
                {
                    decimal Qtde = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                    decimal Valor = decimal.Parse(GridItens.CurrentRow.Cells[4].Value.ToString());
                    Kits.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));                    
                    Kits.Qtde  = Qtde;
                    Kits.Valor = Valor;
                    Kits.GravarDados();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do produto em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Produto.IdProduto > 0)
                    IncluirItem();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do produto em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Produto.IdProduto > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Kits.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                            Kits.Excluir();
                            Kits.IdItem = 0;
                            PopularGridItens();
                        }
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do produto em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Produto.IdProduto > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;                        
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.ShowDialog();
                        if (BuscaPrd.IdProduto > 0)
                        {
                            if (BuscaPrd.IdProduto == Produto.IdProduto)
                            {
                                MessageBox.Show("Item não pode ser igual ao produto principal", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Source_Itens.CancelEdit();
                            }
                            else
                            {
                                Verificar ExistePrd = new Verificar();
                                ExistePrd.Controle  = Controle;
                                Kits.Qtde           = 1;
                                Kits.Valor          = 0;
                                Kits.IdItem         = 0;
                                Kits.IdPrdMaster    = Produto.IdProduto;
                                Kits.IdProduto      = BuscaPrd.IdProduto;
                                Kits.GravarDados();
                                PopularGridItens();
                                GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];
                            }
                        }
                        else
                            Source_Itens.CancelEdit();
                        BuscaPrd.Dispose();
                    }
                }
            }
        }
        //-----------------
        private void AvisoEmail(string texto)
        {
            Controles.EnviarEmail Enviar = new Controles.EnviarEmail();
            Enviar.Controle = Controle;
            Enviar.MontarEmail("Informação (Alteração no Cadastro de produtos)",texto);               
        }
        private void AvisoEmailDistribuidor(string texto)
        {
            Controles.EnviarEmail Enviar = new Controles.EnviarEmail();
            Enviar.Controle = Controle;
            Enviar.MontarEmailDist("Informação ao Distribuidor Talimpo (Alteração na Tabela de Preço)", texto);
        }
        private void BtnLimpaMov_Click(object sender, EventArgs e)
        {
            if (!StaFormEdicao && GridDados.SelectedRows.Count >= 0)
            {
                string Ids = "";
                for (int I = 0; I <= GridDados.SelectedRows.Count - 1; I++)
                {
                    if (Ids == "")
                        Ids = GridDados.SelectedRows[I].Cells[0].Value.ToString();
                    else
                        Ids = Ids + "," + GridDados.SelectedRows[I].Cells[0].Value.ToString();
                }
                if (Ids == "")
                {
                    MessageBox.Show("Atenção: Nenhum produto foi selecionado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Atenção: Esse processamento apaga todo historico do extrato desse produto. Deseja continuar ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //Controle.ExecutaSQL("UPDATE PRODUTOS SET ATIVO=0,SALDOESTOQUE=0 WHERE ID_PRODUTO in (" + Ids + ")");
                        Controle.ExecutaSQL("UPDATE PRODUTOS SET SALDOESTOQUE=0 WHERE ID_PRODUTO in (" + Ids + ")");
                        Controle.ExecutaSQL("DELETE FROM SALDOESTOQUE  WHERE ID_PRODUTO in (" + Ids + ")");
                        Controle.ExecutaSQL("DELETE FROM EXTRATOESTOQUE  WHERE ID_PRODUTO in (" + Ids + ")");
                        MessageBox.Show("Atualização concluida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PopularGrid();                        
                        //PopularCampos(Produto.IdProduto);
                    }
                    catch
                    {
                        MessageBox.Show("Atenção: Erro no processamento, tente novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }
        private void TxtPesquisa_Validated(object sender, EventArgs e)
        {
            
        }
        private void TxtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
                PopularGrid();
        }
                
        /*private void Ck_Ativo_CheckedChanged(object sender, EventArgs e)
        {
            if (Produto.IdProduto > 0 && !StaFormEdicao)
            {
                if (FrmPrincipal.Perfil_Usuario.AtivarProduto == 0)
                {
                    FrmAutorizacao Autorizacao = new FrmAutorizacao();
                    Autorizacao.FrmPrincipal = FrmPrincipal;
                    Autorizacao.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (Autorizacao.AcessoOk)
                    {
                        if (Autorizacao.Usuario.AtivarProduto == 0)
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

                if (Ck_Ativo.Checked)
                {
                    Controle.ExecutaSQL("UPDATE PRODUTOS SET ATIVO=1 WHERE ID_PRODUTO=" + Produto.IdProduto.ToString());
                    FrmPrincipal.RegistrarAuditoria(this.Text, Produto.IdProduto, Produto.Referencia, 9, "ATIVO=1");
                }
                else
                {
                    Controle.ExecutaSQL("UPDATE PRODUTOS SET ATIVO=0 WHERE ID_PRODUTO=" + Produto.IdProduto.ToString());
                    FrmPrincipal.RegistrarAuditoria(this.Text, Produto.IdProduto, Produto.Referencia, 9, "ATIVO=0");
                }
            }
        }*/

             
        private void BtnMovPrd_Click(object sender, EventArgs e)
        {
            FrmConsMovPrd FrmMvPrd = new FrmConsMovPrd();
            FrmMvPrd.FrmPrincipal  = FrmPrincipal;
            FrmMvPrd.IdProduto     = Produto.IdProduto;
            FrmMvPrd.ShowDialog();
        }        
        private void BtnAtlzEstoque_Click(object sender, EventArgs e)
        {
            
        }

        private void GridDados_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (GridDados.Rows[e.RowIndex].Cells[12].Value.ToString() == "0")
                GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            else
                GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }
        private void GridDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           /* if (e.ColumnIndex == 9)
            {
                if (GridDados.CurrentRow != null)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                    {
                        FrmAutorizacao Autorizacao = new FrmAutorizacao();
                        Autorizacao.FrmPrincipal = FrmPrincipal;
                        Autorizacao.ShowDialog();
                        //Verificando se o Acesso foi liberado
                        if (Autorizacao.AcessoOk)
                        {
                            if (Autorizacao.Usuario.AlterarProduto == 1)
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

                    if (GridDados.CurrentRow.Cells[9].Value.ToString() == "1")
                        Controle.ExecutaSQL("UPDATE PRODUTOS SET ATIVO=0,DtAlteracao=convert(datetime,'" + DateTime.Now.ToShortDateString() + "',103) WHERE ID_PRODUTO=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                    else
                        Controle.ExecutaSQL("UPDATE PRODUTOS SET ATIVO=1,DtAlteracao=convert(datetime,'" + DateTime.Now.ToShortDateString() + "',103) WHERE ID_PRODUTO=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                }
            }*/
        }
        private void GridDados_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (e.ColumnIndex == 12)
                {
                    if (FrmPrincipal.Perfil_Usuario.AtivarProduto == 0)
                    {
                        FrmAutorizacao Autorizacao = new FrmAutorizacao();
                        Autorizacao.FrmPrincipal = FrmPrincipal;
                        Autorizacao.ShowDialog();
                        //Verificando se o Acesso foi liberado
                        if (Autorizacao.AcessoOk)
                        {
                            if (Autorizacao.Usuario.AtivarProduto == 0)
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                e.Cancel = true;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                if (e.ColumnIndex == 13)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarProduto == 0)
                    {
                        FrmAutorizacao Autorizacao = new FrmAutorizacao();
                        Autorizacao.FrmPrincipal = FrmPrincipal;
                        Autorizacao.ShowDialog();
                        //Verificando se o Acesso foi liberado
                        if (Autorizacao.AcessoOk)
                        {
                            if (Autorizacao.Usuario.AlterarProduto == 0)
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                e.Cancel = true;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                            return;
                        }
                    }
                }                
            }
        }
        private void GridDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {                
                if (e.ColumnIndex == 12)
                {
                    Controle.ExecutaSQL("UPDATE PRODUTOS SET ATIVO=" + GridDados.CurrentRow.Cells[12].Value.ToString() + ",DtAlteracao=convert(datetime,'" + DateTime.Now.ToShortDateString() + "',103) WHERE ID_PRODUTO=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                    FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()), GridDados.CurrentRow.Cells[2].Value.ToString(), 9, "ATIVO=" + GridDados.CurrentRow.Cells[12].Value.ToString());                    
                }

                if (e.ColumnIndex == 13)
                {
                    Controle.ExecutaSQL("UPDATE PRODUTOS SET Id_Grupo=" + GridDados.CurrentRow.Cells[13].Value.ToString() + ",DtAlteracao=convert(datetime,'" + DateTime.Now.ToShortDateString() + "',103) WHERE ID_PRODUTO=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                    FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()), GridDados.CurrentRow.Cells[2].Value.ToString(), 9, "Grupo=" + GridDados.CurrentRow.Cells[13].Value.ToString());                    
                }
            }
        }

        private void LstReducao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                RedFiscal.LerDados(int.Parse(LstReducao.SelectedValue.ToString()));
                TxtReducao.Value = RedFiscal.Perc;
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            FrmCadAliqRedFiscal Frm = new FrmCadAliqRedFiscal();
            Frm.FrmPrincipal = FrmPrincipal;
            Frm.ShowDialog();
            LstReducao = FrmPrincipal.PopularCombo("SELECT ID_REDUCAO,rtrim(CodRed+' - '+Rtrim(CONVERT(char,Perc))+'  '+RefReducao) as Reducao from REDUCAOFISCAL", LstReducao);
            LstReducao.SelectedValue = Produto.IdReducao;
        }

        private void GridGrupos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (GridGrupos.Rows[e.RowIndex].Cells[2].Value.ToString() == "0")
                GridGrupos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            else                
                GridGrupos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        private void BtnFoto_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                if (Produto.IdProduto > 0)
                {
                    BuscaFoto.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
                    DialogResult res = BuscaFoto.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        PicFoto.Image = Image.FromFile(BuscaFoto.FileName);
                        // Salvar Imagem
                        MemoryStream ms = new MemoryStream();
                        PicFoto.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] photo_aray = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(photo_aray, 0, photo_aray.Length);
                        SqlCommand CmdSql = new SqlCommand("Update Produtos set Foto=@Foto WHERE id_Produto=" + Produto.IdProduto.ToString(), Controle.Conexao);
                        CmdSql.Parameters.AddWithValue("@Foto", photo_aray);
                        CmdSql.ExecuteNonQuery();
                    }
                }
            }
        }
        private void LerFoto()
        {
            if (Produto.IdProduto > 0)
            {
                PicFoto.Image = null;
                if (Produto.Foto != "")
                {
                    SqlDataAdapter Tab = new SqlDataAdapter(new SqlCommand("SELECT Foto FROM Produtos WHERE ID_Produto=" + Produto.IdProduto.ToString(), Controle.Conexao));
                    DataSet DBFoto = new DataSet();
                    Tab.Fill(DBFoto);

                    if (((byte[])DBFoto.Tables[0].Rows[0]["Foto"]).Count() > 1)
                    {
                        byte[] photo_aray = (byte[])DBFoto.Tables[0].Rows[0]["Foto"];
                        MemoryStream ms = new MemoryStream(photo_aray);
                        PicFoto.Image = Image.FromStream(ms);
                    }
                }
            }
        }

    }
}
