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
using Controle_Dados;
using Controles;
using System.Data.Sql;
using System.Data.SqlClient;
using ERP_SGE_PEDWEB;

public partial class ERP_SGE : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BtnTelaPrincipal.Visible = false;
            BtnFinanceiro.Visible    = false;
            BtnPedido.Visible        = false;
            BtnTabela.Visible        = false;
            BtnSld.Visible           = false;

            if (Session["LoginUsuario"] == null && Page.ToString().ToUpper() != "ASP.LOGIN_ASPX")
                Response.Redirect("Login.aspx");
            else if (Session["LoginUsuario"] != null)
            {
                BtnTelaPrincipal.Visible = true;
                BtnFinanceiro.Visible    = true;
                BtnPedido.Visible        = true;
                BtnTabela.Visible        = true;
                BtnSld.Visible           = true;
            }
        }
        Session.Timeout = 120;
    }
    protected void BtnFinanceiro_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConsultaFinanc.aspx");
    }
    protected void BtnTelaPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("TelaPrincipal.aspx");
    }
    protected void BtnTabela_Click(object sender, EventArgs e)
    {
        Response.Redirect("TabelaPreco.aspx");
    }
    protected void BtnPedido_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pedido.aspx");
    }

    protected void BtnSld_Click(object sender, EventArgs e)
    {
        Response.Redirect("SaldoVdFinanc.aspx");
    }
}
