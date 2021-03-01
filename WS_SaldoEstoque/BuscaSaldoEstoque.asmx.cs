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
using System.Configuration;
namespace WS_SaldoEstoque
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ConsultaSaldo : System.Web.Services.WebService

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
        public decimal SaldoEstoque(string Ref, string NomeServidor, string Porta)
        {
            decimal CmpRet = 0;
            try
            {
                
                //string StringConexao = "Data Source=" + NomeServidor.Trim() + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                //StringConexao = "Data Source=" + NomeServidor + Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True";

                SqlConnection Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                SqlCommand Cmd = new SqlCommand("ConsultaSaldoEstoque", Conexao);
                Cmd.Parameters.Add("@Ref", SqlDbType.Char);
                Cmd.Parameters[0].Value = Ref;
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader Tabela = Cmd.ExecuteReader();

                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    CmpRet = decimal.Parse(Tabela[0].ToString());
                    Conexao.Close();
                    Conexao.Dispose();
                    return CmpRet;
                }
                else
                {
                    Conexao.Close();
                    Conexao.Dispose();
                    return CmpRet;
                }
            }
            catch (Exception erro)
            {
                RegistroLog("Erro Registro de Venda: " + erro.ToString());
                return CmpRet;   // Erro na consulta
            }
        }

        [WebMethod]
        public string PrevEntrega(string Ref, string NomeServidor, string Porta)
        {
            string CmpRet = null;
            try
            {
                
                //StringConexao = "Data Source=" + NomeServidor + Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True";

                SqlConnection Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                SqlDataReader Tabela;
                SqlCommand objCmd = new SqlCommand("SELECT TOP 1 T1.PREVENTREGA FROM PEDCOMPRA T1" +
                                                   "  LEFT JOIN PEDCOMPRAITENS T2 ON (T2.ID_DOCUMENTO=T1.ID_DOCUMENTO)" +
                                                   "  LEFT JOIN Produtos T3 ON (T3.Id_Produto=T2.ID_PRODUTO)" +
                                                   " WHERE T3.REFERENCIA='" + Ref + "' AND T1.STATUS=1 ORDER BY T1.PREVENTREGA ", Conexao);
                objCmd.CommandTimeout = 0;
                Tabela = objCmd.ExecuteReader();

                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    CmpRet = DateTime.Parse(Tabela[0].ToString()).ToShortDateString();
                    Conexao.Close();
                    Conexao.Dispose();
                    return CmpRet;
                }
                else
                {
                    Conexao.Close();
                    Conexao.Dispose();
                    return CmpRet;
                }
            }
            catch (Exception erro)
            {
                RegistroLog("Erro Registro de Venda: " + erro.ToString());
                return CmpRet;  // Erro na consulta
            }
        }

        [WebMethod]
        public string DtBalanco(string Ref, string NomeServidor, string Porta)
        {
            string CmpRet = null;
            //string StringConexao = "Data Source=SERVIDOR" + Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

            try
            {
                SqlConnection Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                SqlDataReader Tabela;
                SqlCommand objCmd = new SqlCommand("SELECT TOP 1 T1.DATA FROM BALANCO T1" +
                                                   "  LEFT JOIN BALANCOITENS T2 ON (T2.ID_BALANCO=T1.ID_BALANCO)" +
                                                   "  LEFT JOIN Produtos T3 ON (T3.Id_Produto=T2.ID_PRODUTO)" +
                                                   " WHERE T3.REFERENCIA='" + Ref + "' AND T1.STATUS=1 ORDER BY T1.DATA DESC ", Conexao);
                objCmd.CommandTimeout = 0;
                Tabela = objCmd.ExecuteReader();

                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    CmpRet = DateTime.Parse(Tabela[0].ToString()).ToShortDateString();
                    Conexao.Close();
                    Conexao.Dispose();
                    return CmpRet;
                }
                else
                {
                    Conexao.Close();
                    Conexao.Dispose();
                    return CmpRet;
                }
            }
            catch (Exception erro)
            {
                RegistroLog("Erro Registro de Venda: " + erro.ToString());
                return CmpRet;  // Erro na consulta
            }
        }
    }
}
