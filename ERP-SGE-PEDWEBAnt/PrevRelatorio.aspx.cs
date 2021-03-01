using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using Controle_Dados;
using Controles;

public partial class PrevRelatorio : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
            Controle_Dados.Funcoes Controle = new Funcoes();
            if (Session["SQLRel"]==null)
                Response.Redirect("TelaPrincipal.aspx");
            else
            {
                string SQL = Session["SQLRel"].ToString();
                //Variáveis
                CrystalDecisions.CrystalReports.Engine.ReportDocument crReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Cria a instância do Report
                if (Request.QueryString["Relatorio"].ToString() == "TabPreco")
                    crReportDocument = new ERP_SGE_PEDWEB.RelTabPreco();
                if (Request.QueryString["Relatorio"].ToString() == "SldVdFin")
                    crReportDocument = new ERP_SGE_PEDWEB.RelSldVdFinanc();
                
                SqlConnection Conn = new SqlConnection();
                //SqlDataAdapter daRelatorio = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                //Abre o Banco de Dados
                Conn.ConnectionString = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conn.Open();       
                //Propriedades do Command
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Conn;
                //Passa as propriedades do Command para o DataAdapter
                SqlDataAdapter daRelatorio = new SqlDataAdapter(cmd);
                //Carrega o DataSet com o nome de "ProductsReport"
                daRelatorio.Fill(dataSet); 
                //Passa o DataSer para o relatório
                crReportDocument.SetDataSource(dataSet);
                crReportDocument.SetDataSource(dataSet.Tables[0]);
                //Parra para o Viewer o nosso relatório                                                
                crvRelatorio.ReportSource = crReportDocument;                
            }
        //}        
    }
    protected void BtnRetorno_Click(object sender, EventArgs e)
    {
        Response.Redirect("TabelaPreco.aspx");
    }
    protected void crvRelatorio_Init(object sender, EventArgs e)
    {

    }
}
