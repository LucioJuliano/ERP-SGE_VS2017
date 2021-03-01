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

public partial class Login : System.Web.UI.Page
{
    Controles.Login ValidarLogin = new Controles.Login();

    private Controle_Dados.Usuarios Usuario;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            Session["LoginUsuario"] = null;        
        Login1.Focus();
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Usuario = ValidarLogin.Verificar_Login(Login1.UserName.Trim(), Login1.Password.Trim());
        if (Usuario != null)
        {
            Session["LoginUsuario"] = Usuario;
            Response.Redirect("Menu.aspx");                
        }
        else
        {
            string vScriptMensagem = "<script language='javascript'> alert('Autorização negada, Favor verificar Usuário e Senha') </script>";
            this.RegisterStartupScript("Mensagem", vScriptMensagem);
        }
        
    }
}
