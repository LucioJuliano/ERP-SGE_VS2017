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


public partial class TelaPrincipal : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    int IdPessoa = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IdPessoa = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdPessoa;
        TxtNumPed.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
        TxtNumPed.Style.Add("text-align", "right");
        if (!this.IsPostBack)
        {
            TxtNumPed.Text = "0";
            LstTpMov.SelectedValue = "1";
            PopuparGrid();
        }
    }
    private void PopuparGrid()
    {
        string sSQL = "SELECT T1.ID_VENDA,T1.DATA,T1.STATUS,T1.VLRTOTAL,T1.PREVENTREGA,ISNULL(T2.ENTREGADOR,' ') AS ENTREGADOR,T1.FORMNF," +
                      "CASE T1.TPVENDA WHEN 'PV' THEN 'PEDIDO VENDA' WHEN 'PI' THEN 'SOLICTAÇÃO PEDIDO' WHEN 'VF' THEN 'VENDA FINANCEIRA' WHEN 'EMVF' THEN 'ENTREGA DE MERCADORIA' END AS TIPOVENDA " +
                      ",VinculoVD FROM MVVENDA T1 " +                      
                      "  LEFT JOIN ENTREGADORES T2 ON (T2.ID_ENTREGADOR=T1.ID_ENTREGADOR)" +
                      " WHERE T1.STATUS <> 4 AND T1.ID_PESSOA=" + IdPessoa.ToString();

        if (int.Parse(TxtNumPed.Text) > 0)
            sSQL = sSQL + " AND T1.ID_VENDA=" + TxtNumPed.Text;

        if (LstTpMov.SelectedIndex==0)
            sSQL = sSQL + " AND T1.TPVENDA IN ('PV','PI','VF','EMVF') ORDER BY ID_VENDA DESC";
        else if (LstTpMov.SelectedIndex == 1)
            sSQL = sSQL + " AND T1.TPVENDA ='PV' ORDER BY ID_VENDA DESC";
        else if (LstTpMov.SelectedIndex == 2)
            sSQL = sSQL + " AND T1.TPVENDA ='PI' ORDER BY ID_VENDA DESC";
        else if (LstTpMov.SelectedIndex == 3)
            sSQL = sSQL + " AND T1.TPVENDA ='VF' ORDER BY ID_VENDA DESC";
        else if (LstTpMov.SelectedIndex == 4)
            sSQL = sSQL + " AND T1.TPVENDA ='EMVF' ORDER BY ID_VENDA DESC";
        
        GridDados.DataSource = Controle.ConsultaTabela(sSQL);
        GridDados.DataBind();
    }
    protected void GridDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[3].Text.Trim() == "0")
                e.Row.Cells[3].Text = "Em Aberto";
            else if (e.Row.Cells[3].Text.Trim() == "1")
                e.Row.Cells[3].Text = "Confirmado";
            else if (e.Row.Cells[3].Text.Trim() == "2" && e.Row.Cells[6].Text.Trim() == "")
                e.Row.Cells[3].Text = "Faturado";
            else if (e.Row.Cells[3].Text.Trim() == "2" && e.Row.Cells[6].Text.Trim() != "")
                e.Row.Cells[3].Text = "Em Rota";
            else if (e.Row.Cells[3].Text.Trim() == "3")
                e.Row.Cells[3].Text = "Entregue";
            else if (e.Row.Cells[3].Text.Trim() == "4")
                e.Row.Cells[3].Text = "Cancelado";
            else
                e.Row.Cells[3].Text = "";
        }
    }
    protected void GridDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDados.PageIndex = e.NewPageIndex;
        PopuparGrid();
    }   
    protected void GridDados_SelectedIndexChanged(object sender, EventArgs e)
    {
        int IdVenda = int.Parse(GridDados.SelectedRow.Cells[1].Text);
        Response.Redirect("Pedido.aspx?VD=" + IdVenda.ToString());
    }
    protected void LstTpMov_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopuparGrid();
    }
    protected void TxtNumPed_TextChanged(object sender, EventArgs e)
    {
        LstTpMov.SelectedIndex = 0;
        if (TxtNumPed.Text.Trim() == "")
            TxtNumPed.Text = "0";
        PopuparGrid();
    }
}
