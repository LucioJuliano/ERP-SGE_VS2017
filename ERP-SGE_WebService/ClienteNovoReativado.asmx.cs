using System;
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
    /// Descrição resumida de ClienteNovoReativado
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ClienteNovoReativado : System.Web.Services.WebService
    {

        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

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
            Registrar.WriteLine("***Verificação: Cliente Novo e Reativado***");
            Registrar.Close();
        }

        [WebMethod]
        public string NovoReativado(string Cnpj, int Dias)
        {
            SqlConnection Conexao = null;            
            try
            {             
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                SqlDataReader PesqVd;
                PesqVd = Executar.ConsultaSQL("SELECT TOP 1 * FROM MVVENDA T1" +
                                              " LEFT JOIN PESSOAS T2 ON(T2.CNPJ = '" + Cnpj.Trim() + "')" +
                                              " WHERE T1.TPVENDA IN('PV', 'VF') AND T1.ID_PESSOA IN(T2.Id_Pessoa)" +
                                              " AND STATUS in (1, 2, 3) AND DATA > CONVERT(DATETIME, CONVERT(CHAR, GETDATE() -" + Dias.ToString() + ", 103), 103)" +
                                              " order by data desc");

                if (PesqVd.HasRows)
                {
                    Conexao.Dispose();
                    return "0";
                }
                else
                {
                    Conexao.Dispose();
                    return "1";
                }
            }
            catch (Exception erro)
            {                
                RegistroLog("Erro Verificando Cliente Novo ou Reativado: " + erro.ToString());
                Conexao.Dispose();
                return "-0";
            }
        }
    }    
}
