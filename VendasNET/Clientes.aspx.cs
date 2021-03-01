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
using System.Windows.Forms;

public partial class Clientes : System.Web.UI.Page
{
    Controle_Dados.Funcoes Controle = new Funcoes();
    int IdVendedor = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IdVendedor = ((Controle_Dados.Usuarios)(((System.Web.UI.UserControl)(this.Master)).Session["LoginUsuario"])).IdVendedor;
        if (!this.IsPostBack)
        {
            
            LstTpMov.SelectedValue = "0";
            TxtPesquisa.Focus();
        }
    }
    private void PopuparGrid()
    {
        string sSQL = "SELECT T1.Id_Pessoa,T1.Cnpj,T1.RazaoSocial,T1.Fantasia,T1.Fone,T1.CONTATO,Rtrim(T1.Endereco)+','+RTrim(T1.Numero)+' '+Rtrim(T1.Complemento)+' '+RTrim(T1.Cep)+' '+RTrim(T1.Bairro)+' '+Rtrim(T1.Cidade) as Logradouro,T2.VENDEDOR FROM Pessoas T1 LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) ";
        
        if (LstTpMov.SelectedIndex == 0)
            GridDados.DataSource = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Ativo=1 and T1.RazaoSocial LIKE '%{0}%' order by T1.RazaoSocial", TxtPesquisa.Text.Trim()));            
        else if (LstTpMov.SelectedIndex == 1)
            GridDados.DataSource = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Ativo=1 and T1.Fantasia LIKE '%{0}%' order by T1.Fantasia", TxtPesquisa.Text.Trim()));            
        else if (LstTpMov.SelectedIndex == 2)
            GridDados.DataSource = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Ativo=1 and T1.Cnpj LIKE '%{0}%' order by T1.Cnpj", TxtPesquisa.Text.Trim()));
        else if (LstTpMov.SelectedIndex == 3)
            GridDados.DataSource = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Ativo=1 and T1.Endereco LIKE '%{0}%' order by T1.Fantasia", TxtPesquisa.Text.Trim()));             
        GridDados.DataBind();
    }
    protected void GridDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();
            if (e.Row.Cells[1].Text.Trim().Length <= 11)
                MaskCol.Mask = "000,000,000-00";
            else
                MaskCol.Mask = "00,000,000/0000-00";
            MaskCol.Text = e.Row.Cells[1].Text.Trim();
            e.Row.Cells[1].Text = MaskCol.Text;

            MaskCol.Mask = "(00) 0000-0000";
            MaskCol.Text = e.Row.Cells[4].Text.Trim();
            e.Row.Cells[4].Text = MaskCol.Text;
            MaskCol.Dispose();
        }
    }
    protected void GridDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridDados.PageIndex = e.NewPageIndex;
        PopuparGrid();
    }
        
    protected void TxtPesquisa_TextChanged(object sender, EventArgs e)
    {
        PopuparGrid();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        PopuparGrid();
    }
}
