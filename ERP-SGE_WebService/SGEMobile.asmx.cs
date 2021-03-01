using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
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
    /// Summary description for SGEMobile
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SGEMobile : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        [WebMethod]
        public string Filiais()
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT ID_FILIAL,FILIAL,FANTASIA,SERVIDORREMOTO,PORTA FROM EMPRESA_FILIAL ORDER BY Id_FILIAL";
                DataSet Tab = Executar.ConsultaTabela(sSQL);
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null;
            }
        }
        [WebMethod]
        public string Estados()
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            try
            {
               // string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT ID_UF,SIGLA,ESTADO FROM ESTADOS ORDER BY ESTADO";
                DataSet Tab = Executar.ConsultaTabela(sSQL);
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null;
            }
        }
        [WebMethod]
        public string FormaPagamento()
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT ID_FORMAPGTO,FORMAPGTO FROM FORMAPAGAMENTO WHERE ATIVO=1 ORDER BY ID_FORMAPGTO";
                DataSet Tab = Executar.ConsultaTabela(sSQL, "FORMAPAGAMENTO");                
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null; 
            }
        }
        [WebMethod]
        public string Vendedores()
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT ID_VENDEDOR,VENDEDOR FROM VENDEDORES WHERE ATIVO=1 ORDER BY ID_VENDEDOR";
                DataSet Tab = Executar.ConsultaTabela(sSQL);
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null;
            }
        }
        [WebMethod]
        public string GrupoProduto()
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT ID_GRUPO,GRUPO FROM GRUPOPRODUTO WHERE Ativo=1 and ListaVenda=1 ORDER BY Id_Grupo";
                DataSet Tab = Executar.ConsultaTabela(sSQL,"GRUPOPRD");
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null;
            }
        }
        [WebMethod]
        public string Produtos(string Data)
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT TOP 2000 T1.ID_PRODUTO,RTRIM(T1.DESCRICAO) AS DESCRICAO,RTRIM(T1.REFERENCIA) AS REFERENCIA,T1.ID_GRUPO,T1.ATIVO,T1.UNIDADE,T1.PRCMINIMO,T1.PRCVAREJO,T1.PRCATACADO,T1.PRCESPECIAL,CONVERT(INT,T1.SALDOESTOQUE) AS SALDOESTOQUE,T1.NCM,T1.PRODUTOKIT,T1.QTDECAIXA,T1.QTDECXDIST,T1.QTDEUND,T1.ID_PROMOCAO" +
                              " FROM PRODUTOS T1 LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.Id_Grupo)  WHERE T1.ATIVO=1 AND T2.ListaVenda=1";

                if (Data != "")
                    sSQL = sSQL + " AND T1.DTALTERACAO >= CONVERT(DATETIME,'" + Data.Trim() + "',103)";

                DataSet Tab = Executar.ConsultaTabela(sSQL,"PRODUTOS");
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null;
            }
        }
        [WebMethod]
        public string Clientes(int IdVendedor)
        {
            XmlDocument XMLCad = new XmlDocument();            
            SqlConnection Conexao = null;
            
            try
            {
                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT T1.ID_PESSOA,T1.CNPJ,T1.FANTASIA,T1.RAZAOSOCIAL,T1.FONE,T1.FAX,T1.CELULAR,T1.CONTATO,Rtrim(T1.Endereco)+','+RTrim(T1.Numero)+' '+Rtrim(T1.Complemento)+' '+RTrim(T1.Cep)+' '+RTrim(T1.Bairro)+' '+Rtrim(T1.Cidade) as ENDERECO," +
                              "  T2.VENDEDOR,T1.ID_VENDEDOR,T1.ID_FORMAPGTO,T1.BLOQFORMAPGTO,T1.ATIVO,T1.CLIE_FORN,convert(decimal(13,2),T1.LimiteCredito) AS LIMITECREDITO,convert(decimal(13,2),T1.Credito) as CREDITO,CONVERT(CHAR,T1.DATACADASTRO,103) AS DTCADASTRO FROM Pessoas T1 " +
                              "  LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                              " WHERE EXISTS (SELECT * FROM MVVENDA T2 WHERE T2.ID_PESSOA=T1.ID_PESSOA AND T2.TPVENDA='PV' AND T2.STATUS=3 AND YEAR(T2.Data) >= YEAR(GETDATE())-1)";

                if (IdVendedor > 0)
                    sSQL = sSQL + " AND T1.ID_VENDEDOR=" + IdVendedor.ToString();

                DataSet Tab = Executar.ConsultaTabela(sSQL,"CLIENTES");
                Conexao.Dispose();
                return Tab.GetXml().ToString().Replace("NewDataSet", "CLIENTES");
                
            }

            catch
            {
                Conexao.Dispose();
                return null;
            }
        }
        [WebMethod]
        public int Login(string NmUsu, string Pwd)
        { 
            SqlConnection Conexao = null;
            try
            {

                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                Controles.Login LoginUsuario = new Controles.Login();
                LoginUsuario.Controle = Executar;

                Usuarios Usu = new Usuarios();
                Usu.Controle = Executar;

                Usu = LoginUsuario.Verificar_Login(NmUsu, Pwd);

                if (Usu != null)
                {
                    Conexao.Dispose();
                    return 1; // Login Efetuado com sucesso
                }
                else
                {
                    Conexao.Dispose();
                    return 0; // Senha errada ou Usuario Invalido
                }
            }
            catch
            {
                return -1; // Erro ao conectar o Servidor
            }
        }
    }
}
