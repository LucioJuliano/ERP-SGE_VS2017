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

public partial class ListaPedidos : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    int IdVendedor = 0;
    int IdUsuario = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IdVendedor = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdVendedor;
        IdUsuario  = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdUsuario;
        TxtNumPed.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
        TxtNumPed.Style.Add("text-align", "right");
        if (!this.IsPostBack)
        {
            TxtNumPed.Text = "0";
            LstTpMov.SelectedValue = "5";
            //Vendedores
            LstVendedor.DataSource = Controle.ConsultaTabela("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor");
            LstVendedor.DataTextField = "Vendedor";
            LstVendedor.DataValueField = "Id_Vendedor";
            LstVendedor.DataBind();
            LstVendedor.Items.Insert(0, "TODOS");
            LstVendedor.SelectedValue = IdVendedor.ToString();
            LstVendedor.Enabled = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).SeusMov == 0;
            PopuparGrid();
        }
    }
    private void PopuparGrid()
    {
        string sSQL = "SELECT T1.ID_VENDA,T1.DATA,T1.STATUS,T1.PESSOA,T1.VLRTOTAL,T1.PREVENTREGA,ISNULL(T2.ENTREGADOR,' ') AS ENTREGADOR,T3.VENDEDOR FROM MVVENDA T1 " +
                      "  LEFT JOIN ENTREGADORES T2 ON (T2.ID_ENTREGADOR=T1.ID_ENTREGADOR) "+
                      "  LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR) ";

        if (LstVendedor.SelectedValue.ToString() != "TODOS")
            sSQL = sSQL + " WHERE T1.TPVENDA='PV' AND (T1.ID_VENDEDOR=" + LstVendedor.SelectedValue.ToString() + " OR T1.ID_USUARIO=" + IdUsuario.ToString() + ") ";
        else
            sSQL = sSQL + " WHERE T1.TPVENDA='PV'";

        if (int.Parse(TxtNumPed.Text) > 0)
            sSQL = sSQL + " AND T1.ID_VENDA=" + TxtNumPed.Text;

        if (TxtCliente.Text.Trim() != "")
            sSQL = sSQL + " AND T1.PESSOA LIKE '%" + TxtCliente.Text.Trim() + "%'";

        if (LstTpMov.SelectedIndex == 5)
            sSQL = sSQL + " AND T1.STATUS=2 AND T1.ID_ENTREGADOR > 0 ORDER BY ID_VENDA DESC";
        else
        {
            if (LstTpMov.SelectedIndex == 6)
                sSQL = sSQL + " ORDER BY ID_VENDA DESC";
            else
                sSQL = sSQL + " AND T1.STATUS=" + LstTpMov.SelectedIndex.ToString() + " ORDER BY ID_VENDA DESC";
        }        
        GridDados.DataSource = Controle.ConsultaTabela(sSQL);
        GridDados.DataBind();
    }
    protected void GridDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[3].Text.Trim() == "0")
                e.Row.Cells[3].Text = "Em Aberto";
            if (e.Row.Cells[3].Text.Trim() == "1")
                e.Row.Cells[3].Text = "Confirmado";
            else if (e.Row.Cells[3].Text.Trim() == "2" && e.Row.Cells[7].Text.Trim() == "")
                e.Row.Cells[3].Text = "Faturado";
            else if (e.Row.Cells[3].Text.Trim() == "2" && e.Row.Cells[7].Text.Trim() != "")
                e.Row.Cells[3].Text = "Em Rota";
            else if (e.Row.Cells[3].Text.Trim() == "3")
                e.Row.Cells[3].Text = "Entregue";
            else if (e.Row.Cells[3].Text.Trim() == "4")
                e.Row.Cells[3].Text = "Cancelado";           
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
        Response.Redirect("Pedidos.aspx?VD=" + IdVenda.ToString());
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
    protected void TxtCliente_TextChanged(object sender, EventArgs e)
    {
        PopuparGrid();
    }
    protected void LstVendedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopuparGrid();
    }
}
