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

public partial class Pedidos : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    Controle_Dados.Produtos CadPrd = new Produtos();
    private DataTable TabItens;
    int IdVendedor = 0;
    int IdUsuario = 0;
    int IdVenda = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        BtnEnviar.Attributes.Add("onclick", "return Mensagem(\"Confirma o Envio do Pedido ?\");");
        BtnPesqPrd.Attributes.Add("onclick", "ShowDialog('PesqProdutos.aspx','500px','700px');");
        IdVendedor = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdVendedor;
        IdUsuario = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdUsuario;
        if (!this.IsPostBack)
        {

            TxtTotal.Style.Add("text-align", "right");
            TxtQtde.Style.Add("text-align", "right");
            TxtVlrUnt.Style.Add("text-align", "right");
            TxtVlrTotal.Style.Add("text-align", "right");

            TxtReferencia.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
            TxtQtde.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
            LstFormaPgto.DataSource = Controle.ConsultaTabela("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento ORDER BY FormaPgto");
            LstFormaPgto.DataTextField = "FormaPgto";
            LstFormaPgto.DataValueField = "Id_FormaPgto";
            LstFormaPgto.DataBind();

            LstVendedor.DataSource = Controle.ConsultaTabela("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor");
            LstVendedor.DataTextField = "Vendedor";
            LstVendedor.DataValueField = "Id_Vendedor";
            LstVendedor.DataBind();
            LstVendedor.Items.Insert(0, "Nenhum");
            LstVendedor.SelectedValue = IdVendedor.ToString();
            LstVendedor.Enabled = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).SeusMov == 0;

            if (Request.QueryString["VD"] != null)
            {
                IdVenda = int.Parse(Request.QueryString["VD"].ToString());
                BtnEnviar.Visible     = false;
                BtnCancelar.Visible   = false;
                TxtReferencia.Enabled = false;
                TxtQtde.Enabled       = false;
                TxtObs.Enabled        = false;
                TxtPrazoPgto.Enabled  = false;
                LstFormaPgto.Enabled  = false;
                LstVendedor.Enabled   = false;
                GridDados.Columns[6].Visible = false;
            }
            TabItens = new DataTable();
            TabItens = CriarTabela();
            PopuparGrid();
        }
        VendasNET TelaMaster = (VendasNET)this.Master;
        ScriptManager Cmp = (ScriptManager)TelaMaster.FindControl("ScriptManager1");
        Cmp.SetFocus(TxtReferencia);

    }
    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);
        this.TabItens = (DataTable)this.ViewState["TabItens"];
    }
    protected override object SaveViewState()
    {
        this.ViewState["TabItens"] = this.TabItens;
        return base.SaveViewState();
    }
    private void PopuparGrid()
    {
        GridDados.DataSource = TabItens;
        GridDados.DataBind();
        decimal VlrTotal = 0;
        for (int I = 0; I <= TabItens.Rows.Count - 1; I++)
            VlrTotal = VlrTotal + decimal.Parse(TabItens.Rows[I]["VLRTOTAL"].ToString());
        TxtTotal.Text = string.Format("{0:n2}", VlrTotal);
    }
    protected void BtnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListaPedidos.aspx");
    }
    protected void TxtReferencia_TextChanged(object sender, EventArgs e)
    {
        VendasNET TelaMaster = (VendasNET)this.Master;
        ScriptManager Cmp = (ScriptManager)TelaMaster.FindControl("ScriptManager1");
        if (TxtReferencia.Text.Trim() != "")
        {
            CadPrd.Controle = Controle;
            CadPrd.LerDados(TxtReferencia.Text.Trim());
            if (CadPrd.IdProduto > 0)
            {
                if (CadPrd.Ativo == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Atenção: Produto inativo');", true);
                    Cmp.SetFocus(TxtReferencia);
                    return;
                }
                TxtCodigo.Text = string.Format("{0:d6}", CadPrd.IdProduto);
                TxtDescricao.Text = CadPrd.Descricao;
                TxtVlrUnt.Text = string.Format("{0:n2}", CadPrd.PrcAtacado);
                TxtQtde.Text = "1";
                TxtVlrTotal.Text = string.Format("{0:n2}", CadPrd.PrcAtacado*1);
                Cmp.SetFocus(TxtQtde);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Atenção: Produto não localizado');", true);
                Cmp.SetFocus(TxtReferencia);
            }
        }
        else
            Cmp.SetFocus(TxtReferencia);
    }
    protected void TxtQtde_TextChanged(object sender, EventArgs e)
    {
        if (int.Parse(TxtCodigo.Text) > 0)
        {
            TxtVlrTotal.Text = string.Format("{0:n2}", decimal.Parse(TxtVlrUnt.Text) * decimal.Parse(TxtQtde.Text));
            TabItens.Rows.Add(int.Parse(TxtCodigo.Text), TxtReferencia.Text, TxtDescricao.Text, decimal.Parse(TxtQtde.Text), decimal.Parse(TxtVlrUnt.Text), decimal.Parse(TxtQtde.Text) * decimal.Parse(TxtVlrUnt.Text));
            PopuparGrid();
            LimpaCampos();
        }
        VendasNET TelaMaster = (VendasNET)this.Master;
        ScriptManager Cmp = (ScriptManager)TelaMaster.FindControl("ScriptManager1");
        Cmp.SetFocus(TxtReferencia);
    }
    private void LimpaCampos()
    {
        TxtReferencia.Text = "";
        TxtCodigo.Text     = "0";
        TxtDescricao.Text  = "";
        TxtQtde.Text       = "0";
        TxtVlrUnt.Text     = "0";
        TxtVlrTotal.Text   = "0";
        CadPrd.IdProduto   = 0;
    }

    private DataTable CriarTabela()
    {
        //Atualizando a Tabela
        DataTable Tab01 = new DataTable();
        Tab01.Columns.Add("ID_PRODUTO",  Type.GetType("System.Int32"));
        Tab01.Columns.Add("REFERENCIA",  Type.GetType("System.String"));
        Tab01.Columns.Add("DESCRICAO",   Type.GetType("System.String"));
        Tab01.Columns.Add("QTDE",        Type.GetType("System.Decimal"));
        Tab01.Columns.Add("VLRUNITARIO", Type.GetType("System.Decimal"));
        Tab01.Columns.Add("VLRTOTAL",    Type.GetType("System.Decimal"));

        //Popular Tabela caso seja consulta de venda
        if (IdVenda > 0)
        {
            string sSQL = "SELECT T2.ID_FORMAPGTO,T2.PRAZOPGTO,T2.OBSERVACAO,T1.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO,T1.VLRUNITARIO,T1.QTDE,T1.VLRTOTAL FROM MVVENDAITENS T1 " +
                          " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_VENDA=" + IdVenda.ToString();
            SqlDataReader ItemCons = Controle.ConsultaSQL(sSQL);
            while (ItemCons.Read())
            {
                TxtObs.Text = ItemCons["Observacao"].ToString().Trim();
                TxtPrazoPgto.Text = ItemCons["PrazoPgto"].ToString().Trim();
                LstFormaPgto.SelectedValue = ItemCons["Id_FormaPgto"].ToString();                
                Tab01.Rows.Add(int.Parse(ItemCons["ID_PRODUTO"].ToString()), ItemCons["REFERENCIA"].ToString(), ItemCons["DESCRICAO"].ToString(), decimal.Parse(ItemCons["QTDE"].ToString()), decimal.Parse(ItemCons["VLRUNITARIO"].ToString()), decimal.Parse(ItemCons["QTDE"].ToString()) * decimal.Parse(ItemCons["VLRUNITARIO"].ToString()));
            }

        }
        return Tab01;
    }
    protected void BtnEnviar_Click(object sender, EventArgs e)
    {
        if (BtnEnviar.Enabled)
        {
            if (TabItens.Rows.Count == 0)
                return;

            BtnEnviar.Enabled = false;
            try
            {
                decimal VlrTotal = 0;
                for (int I = 0; I <= TabItens.Rows.Count - 1; I++)
                    VlrTotal = VlrTotal + decimal.Parse(TabItens.Rows[I]["VLRTOTAL"].ToString());

                MvVenda Venda = new MvVenda();
                Venda.Controle = Controle;
                Venda.LerDados(0);
                //
                Venda.TpVenda = "PI";
                Venda.IdFilial = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdFilial;
                Venda.IdVendedor = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdVendedor;
                Venda.IdPessoa = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdPessoa;
                Venda.IdFormaPgto = 0; // int.Parse(LstFormaPgto.SelectedValue.ToString());
                Venda.Observacao = TxtObs.Text;
                Venda.VlrSubTotal = VlrTotal;
                Venda.VlrTotal = VlrTotal;
                Venda.NmPessoa = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).RazaoSocial;
                Venda.CnpjCpf = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Cnpj;
                Venda.Endereco = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Endereco;
                Venda.InscUF = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).InscUF;
                Venda.Cep = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Cep;
                Venda.Numero = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Numero;
                Venda.Complemento = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Complemento;
                Venda.Bairro = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Bairro;
                Venda.Cidade = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Cidade;
                Venda.IdUF = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdUF;
                Venda.Fone = ((Controle_Dados.Pessoas)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).Fone;
                Venda.PrazoPgto = TxtPrazoPgto.Text;
                Venda.ImpNF = 0;
                Venda.GravarDados();
                //
                if (Venda.IdVenda > 0)
                {
                    MvVendaItens Itens = new MvVendaItens();
                    Itens.Controle = Controle;
                    CadPrd.Controle = Controle;
                    for (int P = 0; P <= TabItens.Rows.Count - 1; P++)
                    {
                        CadPrd.LerDados(int.Parse(TabItens.Rows[P]["ID_PRODUTO"].ToString()));
                        if (CadPrd.IdProduto > 0)
                        {
                            Itens.LerDados(0);
                            Itens.TipoItem = "S";
                            Itens.IdVenda = Venda.IdVenda;
                            Itens.IdProduto = CadPrd.IdProduto;
                            Itens.Qtde = decimal.Parse(TabItens.Rows[P]["QTDE"].ToString());
                            Itens.VlrUnitario = decimal.Parse(TabItens.Rows[P]["VLRUNITARIO"].ToString());
                            Itens.VlrTotal = decimal.Parse(TabItens.Rows[P]["VLRTOTAL"].ToString());
                            Itens.VlrUntComissao = decimal.Parse(TabItens.Rows[P]["VLRUNITARIO"].ToString());
                            Itens.PrcCusto = CadPrd.Custo;
                            Itens.PrcMinimo = CadPrd.PrcMinimo;
                            Itens.PrcVarejo = CadPrd.PrcVarejo;
                            Itens.PrcAtacado = CadPrd.PrcAtacado;
                            Itens.GravarDados();
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Pedido Enviado');", true);
                Response.Redirect("TelaPrincipal.aspx");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Atenção: Erro no envio do arquivo, tente novamente');", true);
                BtnEnviar.Enabled = true;
            }
        }
    }

    protected void GridDados_SelectedIndexChanged(object sender, EventArgs e)
    {
        TabItens.Rows.RemoveAt(GridDados.SelectedRow.RowIndex);
        //TabItens.Rows[GridDados.SelectedRow.RowIndex].Delete;
        PopuparGrid();
    }
}
