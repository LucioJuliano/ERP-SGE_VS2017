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
using System.Text;

public partial class PesqProdutos : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            PopuparGrid();
        }
        PopuparGrid();
        TxtDescricao.Focus();

    }
    private void PopuparGrid()
    {
        string sSQL = "SELECT T1.REFERENCIA,T1.DESCRICAO,T2.GRUPO,T1.PRCATACADO FROM PRODUTOS T1 " +
                      " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE T1.ATIVO=1 AND T2.LISTAWEB=1";

        if (TxtReferencia.Text.Trim() != "")
            sSQL = sSQL + " AND T1.REFERENCIA LIKE '%" + TxtReferencia.Text.Trim() + "%'";
        if (TxtDescricao.Text.Trim() != "")
            sSQL = sSQL + " AND T1.DESCRICAO LIKE '%" + TxtDescricao.Text.Trim() + "%'";
        sSQL = sSQL + " ORDER BY T1.DESCRICAO";
        GridDados.DataSource = Controle.ConsultaTabela(sSQL);
        GridDados.DataBind();
    }
    protected void GridDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDados.PageIndex = e.NewPageIndex;
        PopuparGrid();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        PopuparGrid();
    }
    protected void GridDados_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*StringBuilder Script = new StringBuilder();
        Script.Append("<script type='text/javascript'>");
        //Script.Append("window.opener.document.getElementById('ctl00_ContentPlaceHolder1_TxtReferencia').value='" + GridDados.SelectedRow.Cells[1].Text.ToString().Trim() + "';");
        Script.Append(" window.returnValue=" + GridDados.SelectedRow.Cells[1].Text.ToString().Trim() );
        Script.Append(" window.close(); </script>");        
        this.RegisterClientScriptBlock("Client", Script.ToString());*/


        StringBuilder js = new StringBuilder();
        js.Append("<script>");
        js.Append(" window.returnValue=document.getElementById('txtDataHidden').value;");
        js.Append(" window.close();");
        js.Append("</script>");
        if (!this.IsClientScriptBlockRegistered("Wclose"))
            this.RegisterClientScriptBlock("Wclose", js.ToString());
        this.RegisterHiddenField("txtDataHidden", GridDados.SelectedRow.Cells[1].Text.ToString().Trim());
    }
}
