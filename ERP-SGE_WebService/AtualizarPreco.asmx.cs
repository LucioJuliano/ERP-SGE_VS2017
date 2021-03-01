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
    /// Descrição resumida de AtualizarPreco
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class AtualizarPreco : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        [WebMethod]
        public string Pesquisa(string Dt1, string Dt2)
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

                string sSQL = "SELECT T1.ATIVO,T1.ID_Produto,rtrim(T1.Referencia) as Referencia,rtrim(T1.Descricao) as Descricao,T1.PrcSensacional,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado,rtrim(replace(t2.grupo,'&','')) as Grupo,t1.Unidade FROM PRODUTOS T1" +
                              " LEFT JOIN GRUPOPRODUTO T2 ON(T2.ID_GRUPO = T1.ID_GRUPO)" +
                              " WHERE DTALTERACAO >= Convert(DateTime, '" + Dt1 + "', 103) AND DTALTERACAO <= Convert(DateTime, '" + Dt2 + "', 103)";
                                
                DataSet Tab = Executar.ConsultaTabela(sSQL, "PRODUTOS");
                Conexao.Dispose();
                return Tab.GetXml().ToString();
            }
            catch
            {
                Conexao.Dispose();
                return null;
            }
        }
    }
}
