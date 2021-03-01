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
using System.IO;
using System.Configuration;

namespace ERP_SGE_WebService
{
    /// <summary>
    /// Summary description for ValidarLogin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ValidarLogin : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        [WebMethod]
        public string Validar_Login(string NmUsu, string Pwd)
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
                    return "1"; // Login Efetuado com sucesso
                }
                else
                {
                    Conexao.Dispose();
                    return "0"; // Senha errada ou Usuario Invalido
                }
            }
            catch
            {
                return "-1"; // Erro ao conectar o Servidor
            }
        }
    }
}
