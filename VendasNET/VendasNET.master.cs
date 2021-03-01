using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class VendasNET : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BtnListaPedido.Visible = false;
            BtnCliente.Visible = false;
            BtnPedido.Visible = false;
            BtnTabela.Visible = false;

            if (Session["LoginUsuario"] == null && Page.ToString().ToUpper() != "ASP.LOGIN_ASPX")
                Response.Redirect("Login.aspx");
            else if (Session["LoginUsuario"] != null)
            {
                BtnListaPedido.Visible = true;
                BtnCliente.Visible = true;
                BtnPedido.Visible = true;
                BtnTabela.Visible = true;
            }

        }
        Session.Timeout = 1400;
    }    
    protected void BtnListaPedido_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListaPedidos.aspx");
    }
    protected void BtnPedido_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pedidos.aspx");
    }
    protected void BtnTabela_Click(object sender, EventArgs e)
    {
        Response.Redirect("TabelaPreco.aspx");
    }
    protected void BtnCliente_Click(object sender, EventArgs e)
    {
        Response.Redirect("Clientes.aspx");
    }

}
