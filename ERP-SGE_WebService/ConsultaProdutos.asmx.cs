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
    /// Summary description for ConsultaProdutos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ConsultaProdutos : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        [WebMethod]
        public string Pesquisa(int Op, string Conteudo)
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

                string sSQL = "SELECT T1.ID_PRODUTO,RTRIM(T1.REFERENCIA) AS REFERENCIA,RTRIM(T1.DESCRICAO) AS DESCRICAO,SUBSTRING(T2.GRUPO,1,15) GRUPO,T1.PRCESPECIAL,T1.PRCVAREJO,T1.PRCMINIMO,T1.PRCATACADO," +
                            " CASE T1.PRODUTOKIT WHEN 0 THEN CONVERT(INT,T1.SaldoEstoque)  ELSE (SELECT MIN(CONVERT(INT,KT2.SALDOESTOQUE)) FROM PRODUTOSKIT KT1" +
                            " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE" +
                            " FROM Produtos T1  LEFT JOIN GrupoProduto T2 ON (T2.Id_Grupo=T1.Id_Grupo) WHERE T1.Ativo=1";
                           
                if (Op == 1)  // Referencia
                    sSQL = sSQL + " AND T1.REFERENCIA LIKE '%" + Conteudo + "%' ORDER BY REFERENCIA";
                if (Op == 2)  // Descrição
                    sSQL = sSQL + " AND T1.DESCRICAO LIKE '%" + Conteudo + "%' ORDER BY DESCRICAO";

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
