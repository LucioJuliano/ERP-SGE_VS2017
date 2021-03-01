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
using System.Configuration;

namespace WSDebitoCliente
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://grupotalimpo.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]       
    public class DebitoCliente : System.Web.Services.WebService
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
            Registrar.WriteLine("***----------------------------------------------------------------------***");
            Registrar.Close();
        }

        [WebMethod]
        public int VerificarDebito(string DocCnpj )
        {

            SqlConnection Conexao=null;
            try
            {
                // ArrayList Parametros = new ArrayList();
                // StreamReader LerParam = new StreamReader("ERP-SGE.ini");
                // while (!LerParam.EndOfStream)
                //     Parametros.Add(LerParam.ReadLine());

                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
               
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();                

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;
                

                Controles.Verificar ProcVerificar = new Controles.Verificar();
                ProcVerificar.Controle = Executar;

                
                // Localizando o cadastro da pessoa                
                SqlDataReader TabCliente;                
                TabCliente = Executar.ConsultaSQL("SELECT ID_PESSOA FROM PESSOAS WHERE CNPJ='" + DocCnpj.Trim() + "'");
                if (TabCliente.HasRows)
                {
                    TabCliente.Read();
                    SqlDataReader TabDeb;
                    TabDeb = Executar.ConsultaSQL("SELECT * FROM LANCFINANCEIRO WHERE PagRec=2 AND ID_PESSOA=" + TabCliente["ID_PESSOA"].ToString() + " AND VENCIMENTO < Convert(DateTime,'" + DateTime.Now.Date.AddDays(-3).ToShortDateString() + "',103) AND STATUS=0");
                    if (TabDeb.HasRows)
                    {
                        Conexao.Dispose();
                        return 1;
                    }
                    TabDeb = Executar.ConsultaSQL("SELECT * FROM MOVCHEQUEPRE WHERE ID_PESSOA=" + TabCliente["ID_PESSOA"].ToString() + " AND STATUS=1");
                    if (TabDeb.HasRows)
                    {
                        Conexao.Dispose();
                        return 1;
                    }
                    else
                    {
                        Conexao.Dispose();
                        return 0;
                    }
                }
                else
                {
                    Conexao.Dispose();
                    return -1; // Cliente nao localizado
                }
            }
            catch (Exception erro)
            {
                RegistroLog("Erro na Consulta: " + erro.ToString());
                Conexao.Dispose();
                return -2;  // Erro na consulta
            }
        }
    }
}
