using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controle_Dados;
using Controles;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Configuration;

namespace ERP_SGE_WebService
{
    /// <summary>
    /// Descrição resumida de StatusPedido
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class StatusPedido : System.Web.Services.WebService
    {
        

        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        Funcoes Executar = new Funcoes();
        MvVenda Vendas = new MvVenda();

        [WebMethod]
        public string Status(int NumPed)
        {
            SqlConnection Conexao = null;

            Conexao = new SqlConnection(StringConexao);
            Conexao.Open();

            Executar.Conexao = Conexao;
            DataTable Resultado = new DataTable();
            Resultado.Columns.Add("Sta", Type.GetType("System.Int32"));
            Resultado.Columns.Add("DtHr", Type.GetType("System.String"));
            Resultado.Columns.Add("Status", Type.GetType("System.String"));

            string sSQL = "SELECT * FROM MVVENDA WHERE ID_VENDA=" + NumPed.ToString();

            SqlDataReader Tab = Executar.ConsultaSQL(sSQL);
            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    Resultado.Rows.Add(1, Tab["DtEnvioRec"].ToString(), "CONFIRMADO");

                    if (int.Parse(Tab["Status"].ToString()) == 2)
                        Resultado.Rows.Add(2, Tab["DtHrFaturamento"].ToString(), "FATURADO");

                    if (int.Parse(Tab["Id_Entregador"].ToString()) > 2 && int.Parse(Tab["Status"].ToString()) == 2)
                        Resultado.Rows.Add(3, Tab["PrevEntrega"].ToString(), "Em Rota");

                    if (int.Parse(Tab["Status"].ToString()) == 3)
                        Resultado.Rows.Add(3, Tab["DataConfirmacao"].ToString(), "Entregue");

                    if (int.Parse(Tab["Status"].ToString()) == 4)
                        Resultado.Rows.Add(4, Tab["DataCancel"].ToString(), "Cancelado");
                }
            }

            DataSet ret = new DataSet("Status");
            ret.Tables.Add(Resultado);
            Conexao.Dispose();
            return ret.GetXml().ToString();
        }
    }
}
