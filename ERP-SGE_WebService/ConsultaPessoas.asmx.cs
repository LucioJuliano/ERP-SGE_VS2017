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
using System.Configuration;


namespace ERP_SGE_WebService
{
    /// <summary>
    /// Summary description for ConsultaPessoas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ConsultaPessoas : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
        [WebMethod]
        public string Pesquisa(int Op, string Conteudo)
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                string sSQL = "SELECT T1.ID_PESSOA,RTRIM(T1.Cnpj) AS CNPJ,RTRIM(T1.Fantasia) AS FANTASIA,RTRIM(T1.RazaoSocial) AS RAZAOSOCIAL,RTRIM(T1.Fone) AS FONE,RTRIM(T1.CONTATO) AS CONTATO," +
                              " Rtrim(T1.Endereco)+','+RTrim(T1.Numero)+' '+Rtrim(T1.Complemento)+' '+RTrim(T1.Cep)+' '+RTrim(T1.Bairro)+' '+Rtrim(T1.Cidade) as LOGRADOURO,RTRIM(T2.VENDEDOR) AS VENDEDOR,ISNULL(T1.ID_VENDEDOR,0) AS ID_VENDEDOR, ISNULL(CREDITO,0) AS CREDITO,ISNULL(ID_FORMAPGTO,0) AS ID_FORMAPGTO,ISNULL(BLOQFORMAPGTO,0) AS BLOQFORMAPGTO " +
                              " FROM Pessoas T1  LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) WHERE T1.Ativo=1";

                if (Op == 1)  // RAZAOSOCIAL
                    sSQL = sSQL + " AND T1.FANTASIA LIKE '%" + Conteudo + "%' ORDER BY FANTASIA";                    
                if (Op == 2)  // FANTASIA
                    sSQL = sSQL + " AND T1.RAZAOSOCIAL LIKE '%" + Conteudo + "%' ORDER BY RAZAOSOCIAL";
                if (Op == 3)  // CNPJ
                    sSQL = sSQL + " AND T1.CNPJ LIKE '%" + Conteudo + "%' ORDER BY CNPJ";

                DataSet Tab = Executar.ConsultaTabela(sSQL);
                
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null; // Erro ao conectar o Servidor
            }
        }
    }
}
