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

public partial class TabelaPreco : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LstGrupo.DataSource = Controle.ConsultaTabela("SELECT ID_GRUPO,GRUPO FROM GRUPOPRODUTO WHERE LISTAWEB=1 AND LISTAVENDA=1 ORDER BY GRUPO");
            LstGrupo.DataTextField = "GRUPO";
            LstGrupo.DataValueField = "ID_GRUPO";
            LstGrupo.DataBind();
            LstGrupo.Items.Insert(0, "TODOS");
            LstGrupo.SelectedValue = "0";
            PopuparGrid();
        }
        TxtReferencia.Focus();
    }
    private void PopuparGrid()
    {
        string sSQL = "SELECT T1.ID_PRODUTO,T1.REFERENCIA,T1.DESCRICAO,T1.CODBARRA,T2.GRUPO,T1.PRCATACADO FROM PRODUTOS T1 " +
                      " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE T1.ATIVO=1 AND T2.LISTAWEB=1 AND T2.LISTAVENDA=1";

        if (LstGrupo.SelectedValue.ToString() != "TODOS")
            sSQL = sSQL + " AND T1.ID_GRUPO=" + LstGrupo.SelectedValue.ToString();
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

    protected void ImpTabPrc_Click(object sender, ImageClickEventArgs e)
    {
        string sSQL = "SELECT T1.ID_PRODUTO,T1.REFERENCIA,T1.DESCRICAO,T1.CODBARRA,T2.GRUPO,T1.PRCATACADO FROM PRODUTOS T1 " +
                      " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE T1.ATIVO=1 AND T2.LISTAWEB=1 AND T2.LISTAVENDA=1";

        if (LstGrupo.SelectedValue.ToString() != "TODOS")
            sSQL = sSQL + " AND T1.ID_GRUPO=" + LstGrupo.SelectedValue.ToString();
        if (TxtReferencia.Text.Trim() != "")
            sSQL = sSQL + " AND T1.REFERENCIA LIKE '%" + TxtReferencia.Text.Trim() + "%'";
        if (TxtDescricao.Text.Trim() != "")
            sSQL = sSQL + " AND T1.DESCRICAO LIKE '%" + TxtDescricao.Text.Trim() + "%'";
        sSQL = sSQL + " ORDER BY T2.GRUPO,T1.DESCRICAO";

        Session["SQLRel"] = sSQL;
        // Preview do Relatorio
        Response.Redirect("PrevRelatorio.aspx?Relatorio=TabPreco");
    }
}
