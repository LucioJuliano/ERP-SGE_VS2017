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

namespace ERP_SGE_PEDWEB
{
    public partial class SaldoVdFinanc : System.Web.UI.Page
    {
        Controle_Dados.Funcoes Controle = new Funcoes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {                
                PopuparGrid();
            }
            TxtDescPrd.Focus();
        }
        private void PopuparGrid()
        {
            string sSQL = "SELECT T2.Id_Produto,T2.Referencia,T2.Descricao,T3.Grupo,T1.Saldo FROM SaldoPrdCliente T1 " +
                          " LEFT JOIN Produtos T2 ON (T2.Id_Produto=T1.Id_Produto)" +
                          " LEFT JOIN GrupoProduto T3 ON (T3.Id_Grupo=T2.Id_Grupo)" +
                          " WHERE T1.Saldo > 0 AND T1.ID_PESSOA=" + ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdPessoa.ToString();

            if (TxtDescPrd.Text.Trim() != "")
                sSQL = sSQL + " AND T2.DESCRICAO LIKE '%" + TxtDescPrd.Text.Trim() + "%'";
            sSQL = sSQL + " ORDER BY T2.DESCRICAO";
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

        protected void ImpRel_Click(object sender, ImageClickEventArgs e)
        {
            string sSQL = "SELECT T2.Id_Produto,T2.Referencia,T2.Descricao,T3.Grupo,T1.Saldo FROM SaldoPrdCliente T1 " +
                         " LEFT JOIN Produtos T2 ON (T2.Id_Produto=T1.Id_Produto)" +
                         " LEFT JOIN GrupoProduto T3 ON (T3.Id_Grupo=T2.Id_Grupo)" +
                         " WHERE T1.Saldo > 0 AND T1.ID_PESSOA=" + ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdPessoa.ToString();

            if (TxtDescPrd.Text.Trim() != "")
                sSQL = sSQL + " AND T2.DESCRICAO LIKE '%" + TxtDescPrd.Text.Trim() + "%'";
            sSQL = sSQL + " ORDER BY T3.GRUPO,T2.DESCRICAO";

            Session["SQLRel"] = sSQL;
            // Preview do Relatorio
            Response.Redirect("PrevRelatorio.aspx?Relatorio=SldVdFin");
        }

    }
}
