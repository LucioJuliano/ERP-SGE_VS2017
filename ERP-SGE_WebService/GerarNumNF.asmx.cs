using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using Controle_Dados;
using Controles;
using System.Xml;
using System.Configuration;

namespace ERP_SGE_WebService
{
    /// <summary>
    /// Summary description for GerarNumNF
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GerarNumNF : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
        [WebMethod]
        public ArrayList ProxNotaFiscal(int Id, bool NFE)
        {
            SqlConnection Conexao = null;
            //XmlDocument XMLRet = new XmlDocument();

            //string StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
            Conexao = new SqlConnection(StringConexao);
            Conexao.Open();

            Funcoes Executar = new Funcoes();
            Executar.Conexao = Conexao;
            
            Parametros ParamFilial = new Parametros();
            ParamFilial.Controle = Executar;

            ArrayList Resultado = new ArrayList();
            Resultado.Add(0);
            Resultado.Add(0);

            ParamFilial.LerDados(Id);
            if (ParamFilial.IdFilial > 0)
            {
                if (NFE)
                {
                    ParamFilial.NotaNFE = ParamFilial.NotaNFE + 1;
                    ParamFilial.FormularioNFE = ParamFilial.FormularioNFE + 1;
                }
                else
                {
                    ParamFilial.NotaFiscal = ParamFilial.NotaFiscal + 1;
                    ParamFilial.Formulario = ParamFilial.Formulario + 1;
                }
                ParamFilial.GravarDados(false);
                
            }
            else
            {
                ParamFilial.IdFilial = Id;
                if (NFE)
                {
                    ParamFilial.NotaNFE = ParamFilial.NotaNFE + 1;
                    ParamFilial.FormularioNFE = ParamFilial.FormularioNFE + 1;
                }
                else
                {
                    ParamFilial.NotaFiscal = 1;
                    ParamFilial.Formulario = 1;
                }
                ParamFilial.GravarDados(true);
            }
            if (NFE)
            {
                Resultado[0] = ParamFilial.NotaNFE;
                Resultado[1] = ParamFilial.FormularioNFE;
            }
            else
            {
                Resultado[0] = ParamFilial.NotaFiscal;
                Resultado[1] = ParamFilial.Formulario;
            }
            Conexao.Dispose();
            return Resultado;
        }
    }
}
