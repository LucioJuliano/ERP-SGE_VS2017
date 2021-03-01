using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Controle_Dados;
using Controles;
using System.Data.Sql;
using System.Data.SqlClient;


public partial class ConsultaFinanc : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    int IdPessoa = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IdPessoa = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdPessoa;
        if (!this.IsPostBack)
        {
            PopuparGrid();
        }
    }

    private void PopuparGrid()
    {
        string sSQL = "SELECT T1.ID_LANC,T1.DATALANC,T1.ID_VENDA,T1.VLRORIGINAL,T1.VENCIMENTO,T2.DOCUMENTO,T1.NOTAFISCAL,T1.REFERENTE,T1.VLRBAIXA,T1.DTBAIXA FROM LANCFINANCEIRO T1" +
                      "   LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO)" +
                      " WHERE T1.ID_PESSOA=" + IdPessoa.ToString() + " ORDER BY T1.VENCIMENTO DESC";
        GridDados.DataSource = Controle.ConsultaTabela(sSQL);
        GridDados.DataBind();
    }
    protected void GridDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         GridDados.PageIndex = e.NewPageIndex;
         PopuparGrid();
    }
}
