using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Controle_Dados;
using Controles;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Configuration;

namespace ERP_SGE_WebService
{
    /// <summary>
    /// Summary description for RegistroVenda
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RegistroVenda : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        Auditoria RegAuditoria   = new Auditoria();
        Pessoas CadPessoa        = new Pessoas();
        Funcoes Executar         = new Funcoes();
        MvVenda Vendas           = new MvVenda();
        MvVendaItens MvItens     = new MvVendaItens();
        FormaPagamento FormaPgto = new FormaPagamento();
        Vendedores Vendedor      = new Vendedores();
        Verificar VerificarCad   = new Verificar();

        SqlConnection Conexao = null;

        public void RegistroLog(string erro)
        {
            string ArqLog = Server.MapPath("~/") + "ERP_SGE_Log.LOG";
            StreamWriter Registrar;

            if (!File.Exists(ArqLog))
                Registrar = new StreamWriter(ArqLog);
            else
                Registrar = File.AppendText(ArqLog);

            Registrar.WriteLine(DateTime.Now.ToString());
            Registrar.WriteLine("   " + erro);
            Registrar.WriteLine("***----------------------------------------------------------------------***");
            Registrar.Close();
        }

        [WebMethod]
        public string RegistrarVenda(int IdFilial, XmlDocument XmlCadPessoa, XmlDocument XmlVenda, XmlDocument XmlItens)
        {

            int IdVenda = 0;

            try
            {
                if (IdFilial == 0)
                    return "-3";

                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Executar.Conexao      = Conexao;
                RegAuditoria.Controle = Executar;
                FormaPgto.Controle    = Executar;
                Vendedor.Controle     = Executar;
                Vendas.Controle       = Executar;                
                MvItens.Controle      = Executar;                
                VerificarCad.Controle = Executar;

                Parametros ParamFilial = new Parametros();
                ParamFilial.Controle = Executar;
                ParamFilial.LerDados(1);

                Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                ControleEstoque.Controle = Executar;

                // Abrindo os XML de Cliente                
                DataSet TabCliente = new DataSet();
                XmlNodeReader XmlCliente = new XmlNodeReader(XmlCadPessoa);
                TabCliente.ReadXml(XmlCliente);

                CadPessoa.Controle = Executar;

                if (!Ver_CadCliente(TabCliente))
                    return "-1";  //Cadastro de Cliente não Localizado
                                
                DataSet TabVenda = new DataSet();
                XmlNodeReader XmlVd = new XmlNodeReader(XmlVenda);
                TabVenda.ReadXml(XmlVd);
                
                //Verificando a Forma de Pagamento
                int IdFormaPgto = 0;

                if (int.Parse(TabVenda.Tables[0].Rows[0]["Id_FormaPgto"].ToString()) > 0)
                {
                    IdFormaPgto = VerificarCad.Verificar_ExisteCadastro("Id_FormaPgto", "SELECT * FROM FORMAPAGAMENTO WHERE FORMAPGTO='" + TabVenda.Tables[0].Rows[0]["NomeFormaPgto"].ToString().Trim() + "'");
                    if (IdFormaPgto == 0)
                    {
                        FormaPgto.LerDados(0);
                        FormaPgto.FormaPgto   = TabVenda.Tables[0].Rows[0]["NomeFormaPgto"].ToString().Trim();
                        FormaPgto.NumParcelas = 1;
                        FormaPgto.Financeiro  = 1;
                        FormaPgto.GravarDados();
                        IdFormaPgto = FormaPgto.IdFormaPgto;
                    }
                }

                //Verificando Vendedor
                int IdVendedor = VerificarCad.Verificar_ExisteCadastro("Id_Vendedor", "SELECT * FROM VENDEDORES WHERE VENDEDOR='" + TabVenda.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim() + "'");

                if (IdVendedor == 0)
                {
                    Vendedor.LerDados(0);
                    Vendedor.Vendedor = TabVenda.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim();
                    Vendedor.Ativo    = 1;
                    Vendedor.GravarDados();
                    IdVendedor = Vendedor.IdVendedor;
                }                

                Vendas.LerDados(0);
                Vendas.TpVenda         = TabVenda.Tables[0].Rows[0]["TpVenda"].ToString();
                Vendas.IdPessoa        = CadPessoa.IdPessoa;
                Vendas.IdVendedor      = IdVendedor;
                Vendas.IdFormaPgto     = IdFormaPgto;
                Vendas.PrazoPgto       = TabVenda.Tables[0].Rows[0]["PrazoPgto"].ToString();
                Vendas.Observacao      = TabVenda.Tables[0].Rows[0]["Observacao"].ToString(); 
                Vendas.NmPessoa        = CadPessoa.RazaoSocial;
                Vendas.CnpjCpf         = CadPessoa.Cnpj;
                Vendas.InscUF          = CadPessoa.InscUF;
                Vendas.Endereco        = CadPessoa.Endereco;
                Vendas.Numero          = CadPessoa.Numero;
                Vendas.Complemento     = CadPessoa.Complemento;
                Vendas.Fone            = CadPessoa.Fone;
                Vendas.Cep             = CadPessoa.Cep;
                Vendas.Bairro          = CadPessoa.Bairro;
                Vendas.Cidade          = CadPessoa.Cidade;
                Vendas.IdUF            = CadPessoa.IdUF;
                Vendas.Pais            = CadPessoa.Pais;
                Vendas.VlrSubTotal     = decimal.Parse(TabVenda.Tables[0].Rows[0]["VlrSubTotal"].ToString().Replace(".", ","));
                Vendas.VlrDesconto     = decimal.Parse(TabVenda.Tables[0].Rows[0]["VlrDesconto"].ToString().Replace(".", ","));
                Vendas.VlrCredito      = decimal.Parse(TabVenda.Tables[0].Rows[0]["Credito"].ToString().Replace(".", ","));
                Vendas.VlrTotal        = decimal.Parse(TabVenda.Tables[0].Rows[0]["VlrTotal"].ToString().Replace(".", ","));
                Vendas.IdFilial        = int.Parse(TabVenda.Tables[0].Rows[0]["Id_Filial"].ToString());                                
                Vendas.IdFilialEntrega = int.Parse(TabVenda.Tables[0].Rows[0]["Id_FilialEntrega"].ToString());
                Vendas.IdUsuAutDeb     = int.Parse(TabVenda.Tables[0].Rows[0]["Id_UsuAutDeb"].ToString());
                Vendas.IdUsuboleto     = int.Parse(TabVenda.Tables[0].Rows[0]["Id_UsuBoleto"].ToString());
                Vendas.NumPedido       = TabVenda.Tables[0].Rows[0]["NumPedido"].ToString();

                if (Vendas.TpVenda.Trim() == "PC" || Vendas.TpVenda.Trim() == "OE")
                    Vendas.Status = 2;
                else
                    Vendas.Status = 1;
                Vendas.GravarDados();

                IdVenda = Vendas.IdVenda;
                RegistrarAuditoria("Rec.Movimento", Vendas.IdVenda, Vendas.NumDocumento, 1, "Pessoa:" + Vendas.NmPessoa);

                Executar.ExecutaSQL("Update MvVenda set DtEnvioRec=GetDate(), Id_VdOrigem=" + TabVenda.Tables[0].Rows[0]["Id_Venda"].ToString() + ",Id_FilialOrigem=" + IdFilial.ToString() + " WHERE ID_VENDA=" + IdVenda.ToString());

                if (TabVenda.Tables[0].Rows[0]["Id_UsuLibParc"].ToString() != "")
                    Executar.ExecutaSQL("Update MvVenda set Id_UsuLibParc=" + TabVenda.Tables[0].Rows[0]["Id_UsuLibParc"].ToString() + " WHERE ID_VENDA=" + IdVenda.ToString());
                    

                //Incluindo os Itens; 
                DataSet TabItens = new DataSet();
                XmlNodeReader XmlVdItens = new XmlNodeReader(XmlItens);
                TabItens.ReadXml(XmlVdItens);
                int IdPrd = 0;

                for (int i = 0; i < TabItens.Tables[0].Rows.Count; i++)
                {
                    IdPrd = VerificarCad.Verificar_ExisteCadastro("Id_Produto", "SELECT T1.ID_PRODUTO FROM PRODUTOS T1 LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE T1.ATIVO=1 AND T2.ATIVO=1 AND T2.LISTAVENDA=1 AND T1.REFERENCIA='" + TabItens.Tables[0].Rows[i]["REFERENCIA"].ToString().Trim() + "'");

                    if (IdPrd == 0)
                    {
                        Executar.ExecutaSQL("DELETE FROM MVVENDA WHERE ID_VENDA=" + Vendas.IdVenda.ToString());
                        Executar.ExecutaSQL("DELETE FROM MVVENDAITENS WHERE ID_VENDA=" + Vendas.IdVenda.ToString());
                        return "-2 Ref.Produto: " + TabItens.Tables[0].Rows[i]["REFERENCIA"].ToString().Trim(); //Produto não Localizado
                        
                    }
                    MvItens.LerDados(0);
                    MvItens.IdVenda  = Vendas.IdVenda;
                    MvItens.TipoItem = TabItens.Tables[0].Rows[i]["TipoItem"].ToString();
                    if (Vendas.TpVenda != "TROCA")
                    {
                        if (TabItens.Tables[0].Rows[i]["TipoItem"].ToString() == "S")
                            MvItens.TipoItem = "N";
                        else
                        {
                            if (int.Parse(TabItens.Tables[0].Rows[i]["Vinculado"].ToString()) == 0)
                                MvItens.TipoItem = "S";
                            else
                                MvItens.TipoItem = "N";
                        }
                    }

                    MvItens.IdProduto = IdPrd;
                    MvItens.PrcEspecial = decimal.Parse(TabItens.Tables[0].Rows[i]["PrcEspecial"].ToString().Replace(".", ","));
                    MvItens.PrcVarejo   = decimal.Parse(TabItens.Tables[0].Rows[i]["PrcVarejo"].ToString().Replace(".", ","));
                    MvItens.PrcMinimo   = decimal.Parse(TabItens.Tables[0].Rows[i]["PrcMinimo"].ToString().Replace(".", ","));
                    MvItens.PrcAtacado  = decimal.Parse(TabItens.Tables[0].Rows[i]["PrcAtacado"].ToString().Replace(".", ","));
                    MvItens.PrcSensacional = decimal.Parse(TabItens.Tables[0].Rows[i]["PrcSensacional"].ToString().Replace(".", ","));
                    MvItens.Qtde        = decimal.Parse(TabItens.Tables[0].Rows[i]["Qtde"].ToString().Replace(".", ","));
                    MvItens.VlrUnitario = decimal.Parse(TabItens.Tables[0].Rows[i]["VlrUnitario"].ToString().Replace(".", ","));
                    MvItens.VlrTotal    = MvItens.Qtde * MvItens.VlrUnitario;
                    MvItens.PrcCusto    = decimal.Parse(TabItens.Tables[0].Rows[i]["PrcCusto"].ToString().Replace(".", ","));
                    MvItens.Vinculado   = int.Parse(TabItens.Tables[0].Rows[i]["VInculado"].ToString());
                    MvItens.ItemPed     = int.Parse(TabItens.Tables[0].Rows[i]["ItemPed"].ToString());                    
                    MvItens.GravarDados();
                    RegistrarAuditoria("Rec. Item", MvItens.IdItem, Vendas.NumDocumento, 1, "Incluindo Item Produto:" + MvItens.IdProduto.ToString() + "  Vr.Unit:" + MvItens.VlrUnitario.ToString() + "  Qtde:" + MvItens.Qtde.ToString());
                }
                //Concluindo a Venda
                if (int.Parse(TabVenda.Tables[0].Rows[0]["Id_Caixa"].ToString()) > 0)
                {
                    Vendas.IdFilialOrigem = IdFilial;
                    Vendas.VlrDesconto    = Vendas.VlrSubTotal;
                    Vendas.VlrCredito     = 0;
                    Vendas.VlrTotal       = 0;
                    Vendas.GravarDados();
                }

                //Calculando a comissao
                SqlDataReader TabComissao = Executar.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                                 " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + Vendas.IdVenda.ToString());
                Controles.Comissao CalcComissao = new Controles.Comissao();
                CalcComissao.Controle = Executar;

                decimal PDesconto = 0;
                if ((Vendas.VlrDesconto + Vendas.VlrCredito) > 0)
                    PDesconto = 100 / (Vendas.VlrSubTotal / (Vendas.VlrDesconto + Vendas.VlrCredito));

                CalcComissao.CalcularMovimento(TabComissao, PDesconto, CadPessoa.Clie_Forn == 3, ParamFilial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);

                //Atualizando o Saldo do Estoque
                SqlDataReader TabSaida = Executar.ConsultaSQL("SELECT * FROM MvVendaItens WHERE TipoItem='S' and Id_Venda=" + Vendas.IdVenda.ToString());
                ControleEstoque.MovimentoEstoque(TabSaida, 2, 1, false, Vendas.TpVenda, Vendas.Data,0);

                //Finalizando a venda                
                RegistrarAuditoria("Rec.Movimento", Vendas.IdVenda, Vendas.NumDocumento, 5, "Confirmação do Movimento");

            }
            catch (Exception erro)
            {
                /*StreamWriter Xmlerr;
                Xmlerr = File.CreateText(@"D:\WS_erro.txt");
                Xmlerr.Write("Erro ao Atualizar: " + erro.ToString());
                Xmlerr.Close();*/

                RegistroLog("Erro Registro de Venda: " + erro.ToString());

                Executar.ExecutaSQL("DELETE FROM MVVENDA WHERE ID_VENDA=" + Vendas.IdVenda.ToString());
                Executar.ExecutaSQL("DELETE FROM MVVENDAITENS WHERE ID_VENDA=" + Vendas.IdVenda.ToString());

                Conexao.Dispose();
                return "-3";
            }
            Conexao.Dispose();
            return string.Format("{0:D6}",IdVenda);
        }

        private bool Ver_CadCliente(DataSet TabCad)
        {
            SqlDataReader PesqCad;
            /*if (int.Parse(TabCad.Tables[0].Rows[0]["IdServidor"].ToString().Trim()) > 0)
            {
                PesqCad = Executar.ConsultaSQL("SELECT * FROM Pessoas WHERE Ativo=1 and IdServidor=" + TabCad.Tables[0].Rows[0]["IdServidor"].ToString().Trim());
                if (PesqCad.HasRows)
                {
                    PesqCad.Read();
                    CadPessoa.LerDados(int.Parse(PesqCad["Id_Pessoa"].ToString()));
                    return true;
                }                
            }*/
                                           
            string Cnpj = TabCad.Tables[0].Rows[0]["CNPJ"].ToString().Trim();
            if ((Cnpj == "00000000000000" || Cnpj == "11111111111111" || Cnpj == "22222222222222" || Cnpj == "33333333333333" || Cnpj == "44444444444444"
              || Cnpj == "55555555555555" || Cnpj == "66666666666666" || Cnpj == "77777777777777" || Cnpj == "88888888888888" || Cnpj == "99999999999999"
              || Cnpj == "00000000000" || Cnpj == "11111111111" || Cnpj == "22222222222" || Cnpj == "33333333333" || Cnpj == "44444444444"
              || Cnpj == "55555555555" || Cnpj == "66666666666" || Cnpj == "77777777777" || Cnpj == "88888888888" || Cnpj == "99999999999"))
            {
                return false;
            }
            else
            {
                PesqCad = Executar.ConsultaSQL("SELECT * FROM Pessoas WHERE Ativo=1 and Cnpj='" + TabCad.Tables[0].Rows[0]["Cnpj"].ToString().Trim() + "'");
                if (PesqCad.HasRows)
                {
                    PesqCad.Read();
                    CadPessoa.LerDados(int.Parse(PesqCad["Id_Pessoa"].ToString()));
                    return true;
                }
                else
                    return false;

            }         
        }

        public void RegistrarAuditoria(string Opcao, int Id, string Doc, int Operacao, string Descricao)
        {
            RegAuditoria.IdUsuario = 0;
            RegAuditoria.Terminal  = Conexao.WorkstationId;
            RegAuditoria.Data      = DateTime.Now;
            RegAuditoria.Opcao     = Opcao;
            RegAuditoria.IdChave   = Id;
            RegAuditoria.Documento = Doc;
            RegAuditoria.Operacao  = Operacao;
            RegAuditoria.Descricao = Descricao;
            RegAuditoria.Registrar();
        }
    }
}
