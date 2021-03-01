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
    /// Summary description for ConsultaPedVenda
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ConsultaPedVenda : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        [WebMethod]
        public string Pesquisa(string Dt1, string Dt2, string Cliente,string IdVenda, int Sta, int IdVendedor)
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

                string sSQL = "SELECT T1.Id_Venda,CONVERT(CHAR,T1.DATA,103) AS Data,T1.Status,T1.NumDocumento,T1.Pessoa as Cliente,T1.VlrTotal,CONVERT(CHAR,T1.PREVENTREGA,103) AS PrevEntrega,T2.Entregador,T3.Vendedor FROM MVVENDA T1" +
                              " LEFT JOIN Entregadores T2 ON (T2.Id_Entregador=T1.Id_Entregador)" +
                              " LEFT JOIN Vendedores T3 ON (T3.Id_Vendedor=T1.Id_Vendedor) WHERE Data >= convert(DateTime,'" + Dt1 + "',103) AND Data <= convert(DateTime,'" + Dt2 + "',103)";
 
                if (Cliente!="")  // Cliente
                    sSQL = sSQL + " AND T1.PESSOA LIKE '%" + Cliente + "%'";

                if (IdVenda != "")  // Numero da Venda
                    sSQL = sSQL + " AND T1.IdVenda=" + IdVenda;

                if (Sta != 6)  // Status
                {
                    if (Sta == 5)
                        sSQL = sSQL + " AND T1.Status=2 and T1.Id_Entregador > 0";
                    else
                        sSQL = sSQL + " AND T1.Status=" + Sta.ToString();
                }
                if (IdVendedor > 0) //Vendedor
                    sSQL = sSQL + " AND T1.Id_Vendedor=" + IdVendedor.ToString();

                sSQL = sSQL + " ORDER BY T1.Id_Venda Desc";


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
