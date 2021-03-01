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

public partial class TabelaPreco : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    int IdVendedor = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IdVendedor = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdVendedor;
        //if (!this.IsPostBack)
        //{
        //    PopuparGrid();
       // }
        TxtReferencia.Focus();
    }
    private void PopuparGrid()
    {
        //string sSQL = "SELECT T1.ID_PRODUTO,T1.REFERENCIA,T1.DESCRICAO,T1.PRCVAREJO,T1.PRCMINIMO,T1.PRCATACADO FROM PRODUTOS T1 " +
        //              " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE T1.ATIVO=1 AND T2.LISTAVENDA=1";

        string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                       " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado "+
                       " FROM Produtos T1 LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) WHERE T1.Ativo=1 AND GRPPRD.LISTAVENDA=1";
        
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
}
