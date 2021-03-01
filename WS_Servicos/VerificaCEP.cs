using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WS_Servicos
{
    public class VerificaCEP
    {
        public String BuscaCEP(string NumCEP)
        {
            WS_BuscaCEP.CEPService B_Cep = new WS_BuscaCEP.CEPService();            
            try
            {
                return B_Cep.obterLogradouro(NumCEP);
            }
            catch
            {
                return "";
            }
            
        }
    }    
}
