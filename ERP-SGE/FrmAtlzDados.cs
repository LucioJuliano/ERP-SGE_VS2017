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
using System.Data.Sql;
using System.Data.SqlClient;

namespace ERP_SGE
{
    public partial class FrmAtlzDados : Form
    {
        public TelaPrincipal FrmPrincipal;
        Produtos Produto = new Produtos();
        Funcoes Controle = new Funcoes();
        private SqlConnection Serv_Conexao;

        public FrmAtlzDados()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles         
            Controle.Conexao = FrmPrincipal.Conexao;            
            //Dt1.Value = DateTime.Now;            
            Dt1.Value = FrmPrincipal.Parametros_Filial.UltDataAtlz;
            Dt2.Value = DateTime.Now;
        }
        private bool ConectarServidor()
        {
            try
            {
                
                if (!FrmPrincipal.VersaoDistribuidor)
                    Serv_Conexao = new SqlConnection("Data Source=" + FrmPrincipal.URLMatriz + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;");
                else
                    Serv_Conexao = new SqlConnection("Data Source=" + FrmPrincipal.URLMatriz + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;");
                Serv_Conexao.Open();
                //MessageBox.Show("Etapa 1", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch
            {
                MessageBox.Show("Atenção: Erro ao Conectar com o servidor principal", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma o processo de atualização ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string ChaveRef = "";
                //try
                {
                    BtnConfirmar.Enabled = false;
                    ProcBar.Value = 0;

                    Filiais CadFilial = new Filiais();
                    CadFilial.Controle = Controle;
                    CadFilial.LerDados(FrmPrincipal.IdFilialConexao);

                    //Conenctando com o matriz
                    if (ConectarServidor())
                    {
                        if (Controle.Conexao.ConnectionString == Serv_Conexao.ConnectionString)
                        {
                            MessageBox.Show("Atenção: Usuário conectado ao servidor principal, Acesse o servidor local para poder atualizar o banco de dados", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        BtnConfirmar.Text = "Aguarde..";
                        Funcoes ControleServ = new Funcoes();
                        ControleServ.Conexao = Serv_Conexao;

                        //Cadastro de Produto Servidor Principal
                        Produtos ServCadPrd = new Produtos();
                        ServCadPrd.Controle = ControleServ;

                        //Cadastro de Produtos Local
                        Produtos CadPrd = new Produtos();
                        CadPrd.Controle = Controle;

                        //Cadastro de Produtos Local
                        GrupoProduto GrpPrd = new GrupoProduto();
                        GrpPrd.Controle = Controle;

                        Controles.Verificar PesqPrd = new Verificar();
                        PesqPrd.Controle = Controle;

                        ProdutosKitItens ItensKit = new ProdutosKitItens();
                        ItensKit.Controle = Controle;

                        Produtos CadPrdKit = new Produtos();
                        CadPrdKit.Controle = Controle;

                        FrmPrincipal.RegistrarAuditoria(this.Text, 0, "AtlzDados", 1, "Atualização: " + Dt1.Value.Date.ToShortDateString() + " a " + Dt2.Value.Date.ToShortDateString());

                        //MessageBox.Show("Etapa 2", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Atualização dos Produtos
                        DataSet ConsProduto = new DataSet();
                        ConsProduto = ControleServ.ConsultaTabela("SELECT T1.*,T2.GRUPO,ISNULL(T2.LISTAESTMIN,0) AS LISTAESTMIN,ISNULL(T2.ListaVenda,0) AS ListaVenda,ISNULL(T2.LISTAWEB,0) AS LISTAWEB,"+
                                                                  " ISNULL(T2.CST_DIEF,0) AS CST_DIEF ,ISNULL(T2.CST_SPED,0) AS CST_SPED,ISNULL(T2.ESTOQUE,0) AS ESTOQUE,ISNULL(T2.ATIVO,0) AS GRPATIVO,T2.PercVerDesc FROM PRODUTOS T1 " +
                                                                  " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE DTALTERACAO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND DTALTERACAO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)");

                        if (ConsProduto.Tables[0].Rows.Count > 0)
                        {
                            ProcBar.Maximum = ConsProduto.Tables[0].Rows.Count;
                            int IdPrd = 0;
                            for (int I = 0; I <= ConsProduto.Tables[0].Rows.Count - 1; I++)
                            {

                                //MessageBox.Show("Etapa 3 / "+I.ToString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Atualizando o Grupo do Grupo
                                if (int.Parse(ConsProduto.Tables[0].Rows[I]["Id_Grupo"].ToString()) > 0)
                                {
                                    GrpPrd.LerDados(int.Parse(ConsProduto.Tables[0].Rows[I]["Id_Grupo"].ToString()));
                                    GrpPrd.Grupo       = ConsProduto.Tables[0].Rows[I]["Grupo"].ToString().Trim();
                                    GrpPrd.ListaEstMin = int.Parse(ConsProduto.Tables[0].Rows[I]["LISTAESTMIN"].ToString());
                                    GrpPrd.ListaVenda  = int.Parse(ConsProduto.Tables[0].Rows[I]["ListaVenda"].ToString());
                                    GrpPrd.ListaWeb    = int.Parse(ConsProduto.Tables[0].Rows[I]["ListaWeb"].ToString());
                                    GrpPrd.CstDief     = int.Parse(ConsProduto.Tables[0].Rows[I]["CST_DIEF"].ToString());
                                    GrpPrd.CstSped     = int.Parse(ConsProduto.Tables[0].Rows[I]["CST_SPED"].ToString());
                                    GrpPrd.Estoque     = int.Parse(ConsProduto.Tables[0].Rows[I]["ESTOQUE"].ToString());
                                    GrpPrd.PercVerDesc = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PercVerDesc"].ToString());
                                    if (GrpPrd.IdGrupo == 0)
                                        GrpPrd.Ativo = int.Parse(ConsProduto.Tables[0].Rows[I]["GRPATIVO"].ToString());
                                    GrpPrd.GravarDados();
                                }                               
                                ChaveRef = ConsProduto.Tables[0].Rows[I]["Referencia"].ToString();
                                IdPrd = int.Parse(ConsProduto.Tables[0].Rows[I]["Id_Produto"].ToString());
                                CadPrd.LerDados(ChaveRef);
                                //Atualizando os Dados
                                CadPrd.Referencia     = ConsProduto.Tables[0].Rows[I]["Referencia"].ToString().Trim();
                                CadPrd.Descricao      = ConsProduto.Tables[0].Rows[I]["Descricao"].ToString().Trim();
                                CadPrd.DescResumida   = ConsProduto.Tables[0].Rows[I]["DescResumida"].ToString().Trim();
                                CadPrd.IdGrupo        = int.Parse(ConsProduto.Tables[0].Rows[I]["Id_Grupo"].ToString());
                                CadPrd.Tipo           = int.Parse(ConsProduto.Tables[0].Rows[I]["Tipo"].ToString());
                                CadPrd.RefFornecedor  = ConsProduto.Tables[0].Rows[I]["RefFornecedor"].ToString().Trim();
                                CadPrd.CodBarra       = ConsProduto.Tables[0].Rows[I]["CodBarra"].ToString().Trim();
                                CadPrd.IcmsIss        = decimal.Parse(ConsProduto.Tables[0].Rows[I]["IcmsIss"].ToString());
                                CadPrd.Reducao        = decimal.Parse(ConsProduto.Tables[0].Rows[I]["Reducao"].ToString());
                                CadPrd.Ipi            = decimal.Parse(ConsProduto.Tables[0].Rows[I]["Ipi"].ToString());
                                CadPrd.SitTributaria  = int.Parse(ConsProduto.Tables[0].Rows[I]["SitTributaria"].ToString());
                                CadPrd.IcmsIss2       = decimal.Parse(ConsProduto.Tables[0].Rows[I]["IcmsIss2"].ToString());                                
                                CadPrd.SitTrib2       = int.Parse(ConsProduto.Tables[0].Rows[I]["SitTrib2"].ToString());
                                CadPrd.CodSefaz       = ConsProduto.Tables[0].Rows[I]["CodSefaz"].ToString().Trim();
                                CadPrd.PesoBruto      = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PesoBruto"].ToString());
                                CadPrd.PesoLiquido    = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PesoLiquido"].ToString());
                                CadPrd.Unidade        = ConsProduto.Tables[0].Rows[I]["Unidade"].ToString();
                                CadPrd.PrcMinimo      = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PrcMinimo"].ToString());
                                CadPrd.PrcVarejo      = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PrcVarejo"].ToString());
                                CadPrd.PrcAtacado     = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PrcAtacado"].ToString());
                                CadPrd.PrcEspecial    = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PrcEspecial"].ToString());
                                CadPrd.PrcEspDist     = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PrcEspDist"].ToString());
                                CadPrd.PrcSensacional = decimal.Parse(ConsProduto.Tables[0].Rows[I]["PrcSensacional"].ToString());
                                CadPrd.UltPrcCompra   = decimal.Parse(ConsProduto.Tables[0].Rows[I]["UltPrcCompra"].ToString());
                                CadPrd.UltPrcCompra2  = decimal.Parse(ConsProduto.Tables[0].Rows[I]["UltPrcCompra2"].ToString());
                                CadPrd.Custo          = decimal.Parse(ConsProduto.Tables[0].Rows[I]["Custo"].ToString());
                                CadPrd.DtCadastro     = DateTime.Parse(ConsProduto.Tables[0].Rows[I]["DtCadastro"].ToString());
                                CadPrd.DtAlteracao    = DateTime.Parse(ConsProduto.Tables[0].Rows[I]["DtAlteracao"].ToString());
                                CadPrd.Observacao     = ConsProduto.Tables[0].Rows[I]["Observacao"].ToString();
                                CadPrd.Composicao     = ConsProduto.Tables[0].Rows[I]["Composicao"].ToString();
                                CadPrd.ProdutoKit     = int.Parse(ConsProduto.Tables[0].Rows[I]["ProdutoKit"].ToString());
                                CadPrd.IdGenero       = int.Parse(ConsProduto.Tables[0].Rows[I]["Id_Genero"].ToString());
                                CadPrd.NCM            = ConsProduto.Tables[0].Rows[I]["NCM"].ToString();
                                CadPrd.QtdeCxDist     = int.Parse(ConsProduto.Tables[0].Rows[I]["QtdeCxDist"].ToString());
                                CadPrd.QtdeCaixa      = int.Parse(ConsProduto.Tables[0].Rows[I]["QtdeCaixa"].ToString());
                                CadPrd.QtdeUnd        = int.Parse(ConsProduto.Tables[0].Rows[I]["QtdeUnd"].ToString());
                                CadPrd.IdPromocao     = int.Parse(ConsProduto.Tables[0].Rows[I]["Id_Promocao"].ToString());
                                CadPrd.Pontos         = int.Parse(ConsProduto.Tables[0].Rows[I]["Pontos"].ToString());
                                CadPrd.DtAltPrc       = DateTime.Parse(ConsProduto.Tables[0].Rows[I]["DtAltPrc"].ToString());
                                CadPrd.Foto           = ConsProduto.Tables[0].Rows[I]["Foto"].ToString().Trim();
                                CadPrd.Ativo          = int.Parse(ConsProduto.Tables[0].Rows[I]["ATIVO"].ToString());
                                //--------------------------------------------------
                                /*if (CadPrd.IdProduto == 0)
                                    CadPrd.Ativo = 1;*/
                                
                                //--------------------------------------------------
                                if (CadPrd.IdProduto == 0)
                                {
                                    CadPrd.IdProduto    = 0;
                                    CadPrd.SaldoEstoque = 0;
                                }                                
                                if (CadFilial.Regime != 2)
                                {
                                    CadPrd.SitTributaria = 3;
                                    CadPrd.Reducao       = 0;
                                    CadPrd.IcmsIss       = 0;
                                    CadPrd.SitTrib2      = 3;                                    
                                    CadPrd.IcmsIss2      = 0;                                    
                                }
                                
                                CadPrd.GravarDados();
                                //MessageBox.Show("Etapa 4", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (CadPrd.ProdutoKit == 1)
                                {
                                    Controle.ExecutaSQL("DELETE FROM PRODUTOSKIT WHERE ID_PRDMASTER=" + CadPrd.IdProduto.ToString());
                                    DataSet ConsPrdKit = new DataSet();
                                    ConsPrdKit = ControleServ.ConsultaTabela("SELECT T2.REFERENCIA,T1.QTDE,T1.VALOR FROM PRODUTOSKIT T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_PRDMASTER=" + IdPrd.ToString());// CadPrd.IdProduto.ToString());

                                    if (ConsPrdKit.Tables[0].Rows.Count > 0)
                                    {
                                        for (int K = 0; K <= ConsPrdKit.Tables[0].Rows.Count - 1; K++)
                                        {
                                            CadPrdKit.LerDados(ConsPrdKit.Tables[0].Rows[K]["Referencia"].ToString());
                                            if (CadPrdKit.IdProduto > 0)
                                            {
                                                ItensKit.IdItem = 0;
                                                ItensKit.IdPrdMaster = CadPrd.IdProduto;
                                                ItensKit.IdProduto   = CadPrdKit.IdProduto;
                                                ItensKit.Qtde        = decimal.Parse(ConsPrdKit.Tables[0].Rows[K]["Qtde"].ToString());
                                                ItensKit.Valor       = decimal.Parse(ConsPrdKit.Tables[0].Rows[K]["VALOR"].ToString());
                                                ItensKit.GravarDados();
                                            }
                                        }
                                    }
                                    ConsPrdKit.Dispose();
                                }
                                ProcBar.Value = ProcBar.Value + 1;
                            }
                        }
                        ConsProduto.Dispose();


                        //Atualizando as Promoções
                        
                        DataSet ConsPromocao = new DataSet();
                        ConsPromocao = ControleServ.ConsultaTabela("Select t2.*,t1.*,t3.referencia,t1.Ativo as AtivoPrd,isnull(t4.Referencia,0) as RefPromocao from PromocaoProdutosItens T1" +
                                                                   " Left Join PromocaoProdutos t2 on (t2.Id_Promocao=T1.Id_Promocao) " +
                                                                   " left join Produtos t3 on (t3.id_produto=t1.id_produto)"+
                                                                   " left join Produtos t4 on (t4.id_produto=t2.id_produto)" +
                                                                   " Where convert(DateTime,convert(char,GETDATE(),103),103) <= CONVERT(DATETIME,T2.DTFINAL,103) "+
                                                                   " ORDER BY T1.ID_PROMOCAO,T1.ID_PRODUTO");

                        PromocaoProdutos CadPromocao = new PromocaoProdutos();
                        CadPromocao.Controle = Controle;
                        //
                        PromocaoProdutosItens CadPromocaoItens = new PromocaoProdutosItens();
                        CadPromocaoItens.Controle = Controle;

                        int IdPromocao = 0;

                        if (ConsPromocao.Tables[0].Rows.Count > 0)
                        {
                            ProcBar.Value = 0;
                            ProcBar.Maximum = ConsPromocao.Tables[0].Rows.Count;                            
                            for (int I = 0; I <= ConsPromocao.Tables[0].Rows.Count - 1; I++)
                            {
                                if (IdPromocao != int.Parse(ConsPromocao.Tables[0].Rows[I]["Id_Promocao"].ToString()))
                                {
                                    IdPromocao = int.Parse(ConsPromocao.Tables[0].Rows[I]["Id_Promocao"].ToString());

                                    CadPromocao.LerDados(CadPromocao.VerificarPromocaoServidor(int.Parse(ConsPromocao.Tables[0].Rows[I]["Id_Promocao"].ToString())));
                                    if (CadPromocao.IdPromocao == 0)
                                    {
                                        CadPromocao.Ativo     = int.Parse(ConsPromocao.Tables[0].Rows[I]["Ativo"].ToString());
                                        CadPromocao.PComissao = decimal.Parse(ConsPromocao.Tables[0].Rows[I]["PComissao"].ToString());
                                    }
                                    
                                    CadPromocao.Descricao    = ConsPromocao.Tables[0].Rows[I]["Descricao"].ToString();
                                    CadPromocao.DtInicio     = DateTime.Parse(ConsPromocao.Tables[0].Rows[I]["DtInicio"].ToString());
                                    CadPromocao.DtFinal      = DateTime.Parse(ConsPromocao.Tables[0].Rows[I]["DtFinal"].ToString());
                                    CadPromocao.Autorizado   = ConsPromocao.Tables[0].Rows[I]["Autorizado"].ToString();
                                    CadPromocao.Observacao   = ConsPromocao.Tables[0].Rows[I]["Observacao"].ToString();
                                    CadPromocao.DtFinal      = DateTime.Parse(ConsPromocao.Tables[0].Rows[I]["DtFinal"].ToString());
                                    CadPromocao.Segunda      = int.Parse(ConsPromocao.Tables[0].Rows[I]["Segunda"].ToString());
                                    CadPromocao.Terca        = int.Parse(ConsPromocao.Tables[0].Rows[I]["Terca"].ToString());
                                    CadPromocao.Quarta       = int.Parse(ConsPromocao.Tables[0].Rows[I]["Quarta"].ToString());
                                    CadPromocao.Quinta       = int.Parse(ConsPromocao.Tables[0].Rows[I]["Quinta"].ToString());
                                    CadPromocao.Sexta        = int.Parse(ConsPromocao.Tables[0].Rows[I]["Sexta"].ToString());
                                    CadPromocao.Sabado       = int.Parse(ConsPromocao.Tables[0].Rows[I]["Sabado"].ToString());
                                    CadPromocao.Domingo      = int.Parse(ConsPromocao.Tables[0].Rows[I]["Domingo"].ToString());
                                    CadPromocao.IdServidor   = int.Parse(ConsPromocao.Tables[0].Rows[I]["Id_Promocao"].ToString());
                                    CadPromocao.TipoPromocao = int.Parse(ConsPromocao.Tables[0].Rows[I]["TipoPromocao"].ToString());
                                    CadPromocao.QtdeItem     = int.Parse(ConsPromocao.Tables[0].Rows[I]["QtdeItem"].ToString());
                                    CadPromocao.QtdeTotal    = int.Parse(ConsPromocao.Tables[0].Rows[I]["QtdeTotal"].ToString());
                                    CadPromocao.QtdeSen      = int.Parse(ConsPromocao.Tables[0].Rows[I]["QtdeSen"].ToString());
                                    CadPromocao.QtdeEsp      = int.Parse(ConsPromocao.Tables[0].Rows[I]["QtdeEsp"].ToString());
                                    CadPromocao.QtdeVar      = int.Parse(ConsPromocao.Tables[0].Rows[I]["QtdeVar"].ToString());
                                    CadPromocao.QtdeMin      = int.Parse(ConsPromocao.Tables[0].Rows[I]["QtdeMin"].ToString());
                                    CadPromocao.QtdeAta      = int.Parse(ConsPromocao.Tables[0].Rows[I]["QtdeAta"].ToString());
                                    CadPromocao.PDesc        = int.Parse(ConsPromocao.Tables[0].Rows[I]["PDesc"].ToString());
                                    CadPromocao.VlrPedido    = decimal.Parse(ConsPromocao.Tables[0].Rows[I]["VlrPedido"].ToString());
                                    CadPromocao.TipoCliente  = int.Parse(ConsPromocao.Tables[0].Rows[I]["TipoCliente"].ToString());
                                    CadPromocao.DescSegUnd   = int.Parse(ConsPromocao.Tables[0].Rows[I]["DescSegUnd"].ToString());
                                    CadPromocao.PorUsuario   = int.Parse(ConsPromocao.Tables[0].Rows[I]["PorUsuario"].ToString());
                                    if (int.Parse(ConsPromocao.Tables[0].Rows[I]["RefPromocao"].ToString().Trim()) > 0)
                                        CadPromocao.IdProduto = PesqPrd.Verificar_ExisteCadastro("ID_produto", "Select id_produto from Produtos where Referencia='" + ConsPromocao.Tables[0].Rows[I]["RefPromocao"].ToString().Trim() + "'");
                                    else
                                        CadPromocao.IdProduto = 0;
                                    
                                    CadPromocao.GravarDados();
                                    Controle.ExecutaSQL("DELETE FROM PROMOCAOPRODUTOSITENS WHERE ID_PROMOCAO=" + CadPromocao.IdPromocao.ToString());
                                }

                                CadPromocaoItens.LerDados(0);
                                CadPrd.LerDados(ConsPromocao.Tables[0].Rows[I]["Referencia"].ToString().Trim());

                                if (CadPrd.IdProduto > 0)
                                {
                                    CadPromocaoItens.IdProduto   = CadPrd.IdProduto;
                                    CadPromocaoItens.IdPromocao  = IdPromocao;
                                    CadPromocaoItens.PrcEspecial = decimal.Parse(ConsPromocao.Tables[0].Rows[I]["PrcEspecial"].ToString());
                                    CadPromocaoItens.PrcVarejo   = decimal.Parse(ConsPromocao.Tables[0].Rows[I]["PrcVarejo"].ToString());
                                    CadPromocaoItens.PrcMinimo   = decimal.Parse(ConsPromocao.Tables[0].Rows[I]["PrcMinimo"].ToString());
                                    CadPromocaoItens.PrcAtacado  = decimal.Parse(ConsPromocao.Tables[0].Rows[I]["PrcAtacado"].ToString());
                                    CadPromocaoItens.PrcSensacional = decimal.Parse(ConsPromocao.Tables[0].Rows[I]["PrcSensacional"].ToString());
                                    CadPromocaoItens.Ativo          = int.Parse(ConsPromocao.Tables[0].Rows[I]["AtivoPrd"].ToString());
                                    CadPromocaoItens.GravarDados();
                                }
                                ProcBar.Value = ProcBar.Value + 1;
                            }
                        }
                        //----------
                        //Atualizando as Entregas das Vendas                        
                        /* string IdVdMatriz = "";
                        DataSet ConsVendas = new DataSet();
                        ConsVendas = Controle.ConsultaTabela("SELECT * FROM MVVENDA WHERE ID_VDMATRIZ > 0 AND STATUS=2 AND DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)");
                        SqlDataReader VendaMatriz;
                        if (ConsVendas.Tables[0].Rows.Count > 0)
                        {
                            ProcBar.Value = 0;
                            ProcBar.Maximum = ConsVendas.Tables[0].Rows.Count;
                            int IdPrd = 0;
                            for (int I = 0; I <= ConsVendas.Tables[0].Rows.Count - 1; I++)
                            {
                                VendaMatriz = ControleServ.ConsultaSQL("SELECT * FROM MVVENDA WHERE STATUS=3 AND ID_VENDA=" + ConsVendas.Tables[0].Rows[I]["ID_VDMATRIZ"].ToString());
                                while (VendaMatriz.Read())
                                {
                                    if (int.Parse(VendaMatriz["Status"].ToString()) == 3)
                                    {
                                        Controle.ExecutaSQL("UPDATE MVVENDA SET STATUS=3,ID_ENTREGADOR=" + VendaMatriz["ID_ENTREGADOR"].ToString() + ",DATACONFIRMACAO=Convert(DateTime,'" + VendaMatriz["DATACONFIRMACAO"].ToString() + "',103),PREVENTREGA=Convert(DateTime,'" + VendaMatriz["PREVENTREGA"].ToString() + "',103) WHERE ID_VENDA=" + ConsVendas.Tables[0].Rows[I]["ID_VENDA"].ToString());
                                        Controle.ExecutaSQL("UPDATE MvVenda Set Faturado=1 Where VinculoVd='" + string.Format("{0:D6}", int.Parse(ConsVendas.Tables[0].Rows[I]["ID_VENDA"].ToString())) + "'");
                                    }
                                    else
                                    {
                                        Controle.ExecutaSQL("UPDATE MVVENDA SET STATUS=" + VendaMatriz["Status"].ToString() + ",ID_ENTREGADOR=" + VendaMatriz["ID_ENTREGADOR"].ToString() + ",DATACONFIRMACAO=NULL,PREVENTREGA=Convert(DateTime,'" + VendaMatriz["PREVENTREGA"].ToString() + "',103) WHERE ID_VENDA=" + ConsVendas.Tables[0].Rows[I]["ID_VENDA"].ToString());
                                        Controle.ExecutaSQL("UPDATE MvVenda Set Faturado=0 Where VinculoVd='" + string.Format("{0:D6}", int.Parse(ConsVendas.Tables[0].Rows[I]["ID_VENDA"].ToString())) + "'");
                                    }
                                }
                                ProcBar.Value = ProcBar.Value + 1;
                            }
                        }*/
                    }
                    Controle.ExecutaSQL("UPDATE PARAMETROS SET UltDataAtlz=Convert(DateTime, '" + Dt2.Value.Date.ToString() + "',103) WHERE ID_Filial=" + FrmPrincipal.Parametros_Filial.IdFilial.ToString());
                    MessageBox.Show("Atualização concluida", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnConfirmar.Text = "Atualizar";
                    BtnConfirmar.Enabled = true;
                }
                /*catch
                {
                    MessageBox.Show("Atenção: Erro na atualização da Referencia: " + ChaveRef + ", tente novamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }*/
            }
        }
    }
}
