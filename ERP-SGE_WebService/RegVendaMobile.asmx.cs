using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Controle_Dados;
using Controles;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
namespace ERP_SGE_WebService
{
    /// <summary>
    /// Summary description for RegVendaMobile
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RegVendaMobile : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        Auditoria RegAuditoria = new Auditoria();

        SqlConnection Conexao = null;
        [WebMethod]
        public int RegistrarVenda(int IdFilial, int IdPessoa, int IdVendedor, int IdFormaPgto, string PrazoPgto, string Obs, string Subtotal, string Desconto, string Credito, string Total, string XmlItens) 
        {
            
            try
            {
                //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();
                
                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                RegAuditoria.Controle = Executar;

                Parametros ParamFilial = new Parametros();
                ParamFilial.Controle = Executar;
                ParamFilial.LerDados(1);

                Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                ControleEstoque.Controle = Executar;

                Pessoas CadPessoa = new Pessoas();
                CadPessoa.Controle = Executar;
                CadPessoa.LerDados(IdPessoa);

                MvVenda Venda = new MvVenda();
                Venda.Controle = Executar;

                MvVendaItens MvItens = new MvVendaItens();
                MvItens.Controle = Executar;
                             
                Venda.LerDados(0);
                Venda.TpVenda     = "PV";
                Venda.IdPessoa    = IdPessoa;
                Venda.IdVendedor  = IdVendedor;
                Venda.IdFormaPgto = IdFormaPgto;
                Venda.PrazoPgto   = PrazoPgto;
                Venda.Observacao  = Obs;
                Venda.NmPessoa    = CadPessoa.RazaoSocial;
                Venda.CnpjCpf     = CadPessoa.Cnpj;
                Venda.InscUF      = CadPessoa.InscUF;
                Venda.Endereco    = CadPessoa.Endereco;
                Venda.Numero      = CadPessoa.Numero;
                Venda.Complemento = CadPessoa.Complemento;
                Venda.Fone        = CadPessoa.Fone;
                Venda.Cep         = CadPessoa.Cep;
                Venda.Bairro      = CadPessoa.Bairro;
                Venda.Cidade      = CadPessoa.Cidade;
                Venda.IdUF        = CadPessoa.IdUF;
                Venda.Pais        = CadPessoa.Pais;
                Venda.VlrSubTotal = decimal.Parse(Subtotal.ToString().Replace(".", ","));
                Venda.VlrDesconto = decimal.Parse(Desconto.ToString().Replace(".", ","));
                Venda.VlrCredito  = decimal.Parse(Credito.ToString().Replace(".", ","));
                Venda.VlrTotal    = decimal.Parse(Total.ToString().Replace(".", ","));
                if (CadPessoa.IdFilial == 0)
                    Venda.IdFilial = IdFilial;
                else
                    Venda.IdFilial = CadPessoa.IdFilial;
                Venda.GravarDados();                
                RegistrarAuditoria("Venda Mobile Venda", Venda.IdVenda, Venda.NumDocumento, 1, "Pessoa:" + Venda.NmPessoa);
                                
                //Incluindo os Itens;
                System.IO.StringReader Xml = new System.IO.StringReader(XmlItens);
                DataSet Tab = new DataSet();
                Tab.ReadXml(Xml);                
                for (int i = 0; i < Tab.Tables[0].Rows.Count; i++)
                {                    
                    MvItens.LerDados(0);
                    MvItens.IdVenda     = Venda.IdVenda;
                    MvItens.TipoItem    = "S";
                    MvItens.IdProduto   = int.Parse(Tab.Tables[0].Rows[i]["cProd"].ToString());
                    MvItens.PrcEspecial = decimal.Parse(Tab.Tables[0].Rows[i]["cPrcE"].ToString().Replace(".", ","));
                    MvItens.PrcVarejo   = decimal.Parse(Tab.Tables[0].Rows[i]["cPrcV"].ToString().Replace(".", ","));
                    MvItens.PrcMinimo   = decimal.Parse(Tab.Tables[0].Rows[i]["cPrcM"].ToString().Replace(".", ","));
                    MvItens.PrcAtacado  = decimal.Parse(Tab.Tables[0].Rows[i]["cPrcA"].ToString().Replace(".", ","));
                    MvItens.Qtde        = decimal.Parse(Tab.Tables[0].Rows[i]["cQtde"].ToString().Replace(".", ","));
                    MvItens.VlrUnitario = decimal.Parse(Tab.Tables[0].Rows[i]["cPrcU"].ToString().Replace(".", ","));
                    MvItens.VlrTotal    = MvItens.Qtde * MvItens.VlrUnitario;
                    MvItens.GravarDados();                    
                    RegistrarAuditoria("Venda Mobile Item", MvItens.IdItem, Venda.NumDocumento, 1, "Incluindo Item Produto:" + MvItens.IdProduto.ToString() + "  Vr.Unit:" + MvItens.VlrUnitario.ToString() + "  Qtde:" + MvItens.Qtde.ToString());
                }
                //Atualizando o Estoque e Concluindo a Venda

                //Atualizando o Credito do Cliente
                if (Venda.VlrCredito > 0)
                    Executar.ExecutaSQL("UPDATE PESSOAS SET CREDITO=CREDITO-" + Executar.FloatToStr(Venda.VlrCredito, 2) + " WHERE ID_PESSOA=" + Venda.IdPessoa.ToString());
                
                //Calculando a comissao
                SqlDataReader TabComissao = Executar.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                                 " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + Venda.IdVenda.ToString());
                Controles.Comissao CalcComissao = new Controles.Comissao();
                CalcComissao.Controle = Executar;                

                decimal PDesconto = 0;
                if ((Venda.VlrDesconto + Venda.VlrCredito) > 0)
                    PDesconto = 100 / (Venda.VlrSubTotal / (Venda.VlrDesconto + Venda.VlrCredito));
                CalcComissao.CalcularMovimento(TabComissao, PDesconto, CadPessoa.Clie_Forn == 3, ParamFilial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);                             
                
                //Atualizando o Saldo do Estoque
                SqlDataReader TabSaida = Executar.ConsultaSQL("SELECT * FROM MvVendaItens WHERE TipoItem='S' and Id_Venda=" + Venda.IdVenda.ToString());
                ControleEstoque.MovimentoEstoque(TabSaida, 2, 1, false, Venda.TpVenda, Venda.Data,0);

                //Finalizando a venda
                Venda.Concluir(1);
                RegistrarAuditoria("Venda Mobile", Venda.IdVenda, Venda.NumDocumento, 5, "Confirmação do Movimento");
                
            }
            catch
            {
                Conexao.Dispose();
                return -1;
            }
            Conexao.Dispose();
            return 0;
        }
        public void RegistrarAuditoria(string Opcao, int Id, string Doc, int Operacao, string Descricao)
        {
            RegAuditoria.IdUsuario = 0;
            RegAuditoria.Terminal = Conexao.WorkstationId;
            RegAuditoria.Data = DateTime.Now;
            RegAuditoria.Opcao = Opcao;
            RegAuditoria.IdChave = Id;
            RegAuditoria.Documento = Doc;
            RegAuditoria.Operacao = Operacao;
            RegAuditoria.Descricao = Descricao;
            RegAuditoria.Registrar();
        }
    }
}
