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
    public class ListaVendedores : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
        [WebMethod]
        public string Pesquisa()
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
                                
                string sSQL = "SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1  ORDER BY Vendedor";
                DataSet Tab = Executar.ConsultaTabela(sSQL);
                //XMLCad.LoadXml(Tab.GetXml());
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
