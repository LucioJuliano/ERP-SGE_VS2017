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
    /// Summary description for UltCompraCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UltCompraCliente : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        [WebMethod]
        public XmlDocument UltimaCompra(string CNPJCPF)
        {            
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            int IdPessoa=0;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                SqlDataReader PesqCad;
                PesqCad = Executar.ConsultaSQL("SELECT * FROM Pessoas WHERE Cnpj='" + CNPJCPF.Trim() + "'");
                if (PesqCad.HasRows)
                {
                    PesqCad.Read();
                    IdPessoa = int.Parse(PesqCad["Id_Pessoa"].ToString().Trim());                    
                }

                if (IdPessoa > 0)
                {
                    string sSQL = "SELECT TOP 1 T1.ID_VENDA,Convert(char,T1.Data,103) as Data,T1.VlrTotal,T2.Vendedor FROM MvVenda T1" +
                                  "  LEFT JOIN Vendedores T2 ON (T2.Id_Vendedor=T1.Id_Vendedor)" +
                                  " WHERE T1.Status in (2,3) and T1.TpVenda='PV' and IsNull(T1.Id_VdOrigem,0)=0 and T1.Id_Pessoa=" + IdPessoa.ToString() +
                                  " order by t1.data desc,T1.ID_Venda Desc";

                    DataSet Tab = Executar.ConsultaTabela(sSQL);
                    if (Tab.Tables[0].Rows.Count > 0)
                    {
                        XmlDocument XMLVenda = new XmlDocument();
                        XMLVenda.LoadXml(Tab.GetXml());

                        Conexao.Dispose();
                        return XMLVenda;
                    }
                    else
                    {
                        Conexao.Dispose();
                        return null;
                    }

                }
                else
                {
                    Conexao.Dispose();
                    return null; 
                }
            }
            catch
            {
                Conexao.Dispose();
                return null; // Erro ao conectar o Servidor
            }
        }

        [WebMethod]
        public XmlDocument UltimoOrcamento(string CNPJCPF, int IdServidor)
        {
            XmlDocument XMLCad = new XmlDocument();
            SqlConnection Conexao = null;
            int IdPessoa = 0;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                SqlDataReader PesqCad;
                
                PesqCad = Executar.ConsultaSQL("SELECT * FROM Pessoas WHERE Cnpj='" + CNPJCPF.Trim() + "'");
                if (PesqCad.HasRows)
                {
                    PesqCad.Read();
                    IdPessoa = int.Parse(PesqCad["Id_Pessoa"].ToString());
                }

                if (IdPessoa > 0)
                {
                    string sSQL = "SELECT TOP 1 T1.ID_VENDA,Convert(char,T1.Data,103) as Data,T1.VlrTotal,T2.Vendedor," +
                                  " (SELECT CASE WHEN NOT EXISTS (SELECT TOP 1 * FROM MVVENDA WHERE TPVENDA IN ('PV','VF') AND ID_PESSOA=" + IdPessoa.ToString() +
                                  " AND STATUS in (1,2,3) AND DATA <= CONVERT(DATETIME,CONVERT(CHAR,GETDATE(),103),103)) THEN 1 ELSE 0 END) AS NovoCli," +
                                  "(SELECT CASE WHEN NOT EXISTS (SELECT TOP 1 * FROM MVVENDA WHERE TPVENDA IN ('PV','VF') AND ID_PESSOA=" + IdPessoa.ToString() +
                                  " AND STATUS in (1,2,3) AND DATA > CONVERT(DATETIME,CONVERT(CHAR,GETDATE()-90,103),103)) THEN 1 ELSE 0 END) AS Reativ,"+
                                  "(Select Data from Auditoria where documento=t1.NumDocumento and Opcao='Venda (Orçamento)' and Operacao=1) as DTHR FROM MvVenda T1" +
                                  "  LEFT JOIN Vendedores T2 ON (T2.Id_Vendedor=T1.Id_Vendedor)" +
                                  " WHERE RTRIM(T1.VINCULOVD)='' AND T1.TpVenda='OC' and T1.Id_Pessoa=" + IdPessoa.ToString() +
                                  " order by T1.ID_Venda Desc";

                    DataSet Tab = Executar.ConsultaTabela(sSQL);
                    if (Tab.Tables[0].Rows.Count > 0)
                    {
                        XmlDocument XMLVenda = new XmlDocument();
                        XMLVenda.LoadXml(Tab.GetXml());

                        Conexao.Dispose();
                        return XMLVenda;
                    }
                    else
                    {
                        Conexao.Dispose();
                        return null;
                    }

                }
                else
                {
                    Conexao.Dispose();
                    return null;
                }
            }
            catch
            {
                Conexao.Dispose();
                return null; // Erro ao conectar o Servidor
            }
        }

    }
}
