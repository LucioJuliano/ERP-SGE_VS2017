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
    /// Summary description for ConsultaItemPed
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    

    public class ConsultaItemPed : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        [WebMethod]
        public string Pesquisa(int IdVenda)
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

                string sSQL = "SELECT T1.TIPOITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL FROM MVVENDAITENS T1" +
                              " LEFT JOIN Produtos T2 ON (T2.Id_Produto=T1.Id_Produto)" +
                              " WHERE T1.ID_VENDA=" + IdVenda.ToString();
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
