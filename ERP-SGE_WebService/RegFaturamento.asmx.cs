using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using Controle_Dados;
using Controles;
using System.Xml;
using System.Configuration;

namespace ERP_SGE_WebService
{
    /// <summary>
    /// Summary description for RegFaturamento
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RegFaturamento : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
        [WebMethod]        
        public int Enviar(DataSet TabFinanc, string NmUsuario)
        {            
            SqlConnection Conexao = null;
            //XmlDocument XMLRet = new XmlDocument();

            //string StringConexao = "Data Source=SERVIDOR;Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
            Conexao = new SqlConnection(StringConexao);
            Conexao.Open();

            Funcoes Executar = new Funcoes();
            Executar.Conexao = Conexao;

            Verificar VerificarCad = new Verificar();
            VerificarCad.Controle = Executar;

            //TipoDocumento TipoDoc = new TipoDocumento();
            //TipoDoc.Controle = Executar;

            Vendedores Vendedor = new Vendedores();
            Vendedor.Controle = Executar;

            //FormaPagamento FormaPgto = new FormaPagamento();
            //FormaPgto.Controle = Executar;

            Usuarios CadUsu = new Usuarios();
            CadUsu.Controle = Executar;

            Financeiro CadFinanceiro = new Financeiro();
            CadFinanceiro.Controle = Executar;

            Auditoria RegAuditoria = new Auditoria();
            RegAuditoria.Controle = Executar;

            //DataSet TabFinanc = new DataSet();
            //XmlNodeReader xmlReader = new XmlNodeReader(XMLCad);
            //TabReadXml(xmlReader);
            //
            int IdTipoDoc = -1;
            int IdFormaPgto = -1;
            int IdVendedor = -1;
            int IdUsuario = -1;
            int IdLancServ=0;
            //
            for (int I = 0; I <= TabFinanc.Tables[0].Rows.Count - 1; I++)
            {
                /*IdTipoDoc = VerificarCad.Verificar_ExisteCadastro("Id_Documento", "SELECT * FROM TIPODOCUMENTO WHERE DOCUMENTO='" + TabFinanc.Tables[0].Rows[I]["NomeTipoDoc"].ToString().Trim() + "'");
                if (IdTipoDoc == 0)
                {
                    TipoDoc.LerDados(0);
                    TipoDoc.Documento = TabFinanc.Tables[0].Rows[I]["NomeTipoDoc"].ToString().Trim();
                    TipoDoc.GravarDados();
                    IdTipoDoc = TipoDoc.IdDocumento;
                }
                IdFormaPgto = VerificarCad.Verificar_ExisteCadastro("Id_FormaPgto", "SELECT * FROM FORMAPAGAMENTO WHERE FORMAPGTO='" + TabFinanc.Tables[0].Rows[I]["NomeFormaPgto"].ToString().Trim() + "'");
                if (IdFormaPgto == 0)
                {
                    FormaPgto.LerDados(0);
                    FormaPgto.FormaPgto = TabFinanc.Tables[0].Rows[I]["NomeFormaPgto"].ToString().Trim();
                    FormaPgto.NumParcelas = 1;
                    FormaPgto.Financeiro = 1;
                    FormaPgto.GravarDados();
                    IdFormaPgto = FormaPgto.IdFormaPgto;
                }*/
                IdVendedor = VerificarCad.Verificar_ExisteCadastro("Id_Vendedor", "SELECT * FROM VENDEDORES WHERE VENDEDOR='" + TabFinanc.Tables[0].Rows[I]["NomeVendedor"].ToString().Trim() + "'");
                if (IdVendedor == 0)
                {
                    Vendedor.LerDados(0);
                    Vendedor.Vendedor = TabFinanc.Tables[0].Rows[I]["NomeVendedor"].ToString().Trim();
                    Vendedor.Ativo = 1;
                    Vendedor.GravarDados();
                    IdVendedor = Vendedor.IdVendedor;
                }
                IdUsuario = VerificarCad.Verificar_ExisteCadastro("Id_Usuario", "SELECT * FROM USUARIOS WHERE USUARIO='" + TabFinanc.Tables[0].Rows[I]["Usuario"].ToString().Trim() + "'");
                if (IdUsuario == 0)
                {
                    CadUsu.LerDados(0);
                    CadUsu.Usuario = TabFinanc.Tables[0].Rows[I]["Usuario"].ToString().Trim();
                    CadUsu.GravarDados();
                    IdUsuario = CadUsu.IdUsuario;
                }

                if (TabFinanc.Tables[0].Rows[I]["IdLancServ"].ToString() != "")
                    IdLancServ = int.Parse(TabFinanc.Tables[0].Rows[I]["IdLancServ"].ToString());
                else
                    IdLancServ = 0;

                //Verificando o Cadastro Pessoa
                int IdPessoa = 0;
                SqlDataReader PesqCad;
                string Cnpj = TabFinanc.Tables[0].Rows[I]["Cnpj"].ToString().Trim();
                if ((Cnpj == "00000000000000" || Cnpj == "11111111111111" || Cnpj == "22222222222222" || Cnpj == "33333333333333" || Cnpj == "44444444444444"
                  || Cnpj == "55555555555555" || Cnpj == "66666666666666" || Cnpj == "77777777777777" || Cnpj == "88888888888888" || Cnpj == "99999999999999"
                  || Cnpj == "00000000000" || Cnpj == "11111111111" || Cnpj == "22222222222" || Cnpj == "33333333333" || Cnpj == "44444444444"
                  || Cnpj == "55555555555" || Cnpj == "66666666666" || Cnpj == "77777777777" || Cnpj == "88888888888" || Cnpj == "99999999999"))
                {
                    IdPessoa = 0;
                    if (int.Parse(TabFinanc.Tables[0].Rows[I]["IDSERVPESSOA"].ToString().Trim()) > 0)
                    {
                        PesqCad = Executar.ConsultaSQL("SELECT ID_PESSOA FROM Pessoas WHERE Ativo=1 and IdServidor=" + TabFinanc.Tables[0].Rows[I]["IDSERVPESSOA"].ToString().Trim());
                        if (PesqCad.HasRows)
                        {
                            PesqCad.Read();
                            IdPessoa = int.Parse(PesqCad["Id_Pessoa"].ToString());
                        }                            
                    }
                }
                else
                {
                    PesqCad = Executar.ConsultaSQL("SELECT ID_PESSOA FROM Pessoas WHERE Ativo=1 and Cnpj='" + TabFinanc.Tables[0].Rows[I]["Cnpj"].ToString().Trim() + "'");
                    if (PesqCad.HasRows)
                    {
                        PesqCad.Read();
                        IdPessoa = int.Parse(PesqCad["Id_Pessoa"].ToString());
                    }
                }

                if (IdPessoa > 0)
                {
                    CadFinanceiro.LerDados(IdLancServ);
                    CadFinanceiro.DataLanc = DateTime.Parse(TabFinanc.Tables[0].Rows[I]["Data"].ToString());
                    CadFinanceiro.PagRec = int.Parse(TabFinanc.Tables[0].Rows[I]["PagRec"].ToString());
                    CadFinanceiro.IdPessoa = IdPessoa;
                    CadFinanceiro.NumDoc = TabFinanc.Tables[0].Rows[I]["NumDocumento"].ToString().Trim();
                    CadFinanceiro.Referente = TabFinanc.Tables[0].Rows[I]["Referente"].ToString().Trim();
                    CadFinanceiro.NotaFiscal = TabFinanc.Tables[0].Rows[I]["NotaFiscal"].ToString().Trim();
                    CadFinanceiro.VlrOriginal = decimal.Parse(TabFinanc.Tables[0].Rows[I]["VlrOriginal"].ToString().Replace(".", ","));
                    CadFinanceiro.Vencimento = DateTime.Parse(TabFinanc.Tables[0].Rows[I]["Vencimento"].ToString());
                    CadFinanceiro.Observacao = TabFinanc.Tables[0].Rows[I]["Observacao"].ToString().Trim();
                    CadFinanceiro.IdFilial = int.Parse(TabFinanc.Tables[0].Rows[I]["ID_Filial"].ToString());
                    CadFinanceiro.IdTipoDocumento = int.Parse(TabFinanc.Tables[0].Rows[I]["IDTIPODOC"].ToString());
                    CadFinanceiro.IdVendedor = IdVendedor;
                    CadFinanceiro.IdFormaPgto = int.Parse(TabFinanc.Tables[0].Rows[I]["IDFORMAPGTO"].ToString());
                    CadFinanceiro.IdUsuLanc = IdUsuario;
                    CadFinanceiro.GravarDados();
                    TabFinanc.Tables[0].Rows[I]["IDLancServ"] = CadFinanceiro.IdLanc.ToString();

                    if (IdLancServ == 0)
                        RegAuditoria.Operacao = 1;
                    else
                        RegAuditoria.Operacao = 2;
                    RegAuditoria.IdUsuario = IdUsuario;
                    RegAuditoria.Terminal = Conexao.WorkstationId;
                    RegAuditoria.Data = DateTime.Now;
                    RegAuditoria.IdChave = CadFinanceiro.IdLanc;
                    RegAuditoria.Documento = CadFinanceiro.NumDoc.Trim();
                    RegAuditoria.Opcao = "Financeiro WEB SERVICE: Filial: " + CadFinanceiro.IdFilial.ToString();
                    RegAuditoria.Descricao = "Registro Via WEBSERVICE Usuario:" + NmUsuario.Trim();
                    RegAuditoria.Registrar();
                    //XMLRet.LoadXml(TabGetXml());
                    Conexao.Dispose();
                    return CadFinanceiro.IdLanc;
                }
                else
                    return 0;
            }
            return 0;
        }        
    }
}

/*
 * public XmlDocument Enviar(XmlDocument XMLCad, string NmUsuario)
        {
            SqlConnection Conexao = null;
            XmlDocument XMLRet = new XmlDocument();

            string StringConexao = "Data Source=LUCIO-NOTEBOOK;Initial Catalog=BD_ERP_SGE; User ID=sa; MultipleActiveResultSets=True;";
            Conexao = new SqlConnection(StringConexao);
            Conexao.Open();

            Funcoes Executar = new Funcoes();
            Executar.Conexao = Conexao;

            Verificar VerificarCad = new Verificar();
            VerificarCad.Controle = Executar;

            TipoDocumento TipoDoc = new TipoDocumento();
            TipoDoc.Controle = Executar;

            Vendedores Vendedor = new Vendedores();
            Vendedor.Controle = Executar;

            FormaPagamento FormaPgto = new FormaPagamento();
            FormaPgto.Controle = Executar;

            Usuarios CadUsu = new Usuarios();
            CadUsu.Controle = Executar;

            Financeiro CadFinanceiro = new Financeiro();
            CadFinanceiro.Controle = Executar;

            Auditoria RegAuditoria = new Auditoria();
            RegAuditoria.Controle = Executar;

            DataSet TabFinanc = new DataSet();
            XmlNodeReader xmlReader = new XmlNodeReader(XMLCad);
            TabReadXml(xmlReader);
            //
            int IdTipoDoc = -1;
            int IdFormaPgto = -1;
            int IdVendedor = -1;
            int IdUsuario = -1;
            int IdLancServ=0;
            //
            for (int I = 0; I <= TabFinanc.Tables[0].Rows.Count - 1; I++)
            {
                IdTipoDoc = VerificarCad.Verificar_ExisteCadastro("Id_Documento", "SELECT * FROM TIPODOCUMENTO WHERE DOCUMENTO='" + TabFinanc.Tables[0].Rows[I]["NomeTipoDoc"].ToString().Trim() + "'");
                if (IdTipoDoc == 0)
                {
                    TipoDoc.LerDados(0);
                    TipoDoc.Documento = TabFinanc.Tables[0].Rows[I]["NomeTipoDoc"].ToString().Trim();
                    TipoDoc.GravarDados();
                    IdTipoDoc = TipoDoc.IdDocumento;
                }
                IdFormaPgto = VerificarCad.Verificar_ExisteCadastro("Id_FormaPgto", "SELECT * FROM FORMAPAGAMENTO WHERE FORMAPGTO='" + TabFinanc.Tables[0].Rows[I]["NomeFormaPgto"].ToString().Trim() + "'");
                if (IdFormaPgto == 0)
                {
                    FormaPgto.LerDados(0);
                    FormaPgto.FormaPgto = TabFinanc.Tables[0].Rows[I]["NomeFormaPgto"].ToString().Trim();
                    FormaPgto.NumParcelas = 1;
                    FormaPgto.Financeiro = 1;
                    FormaPgto.GravarDados();
                    IdFormaPgto = FormaPgto.IdFormaPgto;
                }
                IdVendedor = VerificarCad.Verificar_ExisteCadastro("Id_Vendedor", "SELECT * FROM VENDEDORES WHERE VENDEDOR='" + TabFinanc.Tables[0].Rows[I]["NomeVendedor"].ToString().Trim() + "'");
                if (IdVendedor == 0)
                {
                    Vendedor.LerDados(0);
                    Vendedor.Vendedor = TabFinanc.Tables[0].Rows[I]["NomeVendedor"].ToString().Trim();
                    Vendedor.Ativo = 1;
                    Vendedor.GravarDados();
                    IdVendedor = Vendedor.IdVendedor;
                }
                IdUsuario = VerificarCad.Verificar_ExisteCadastro("Id_Usuario", "SELECT * FROM USUARIOS WHERE USUARIO='" + TabFinanc.Tables[0].Rows[I]["Usuario"].ToString().Trim() + "'");
                if (IdUsuario == 0)
                {
                    CadUsu.LerDados(0);
                    CadUsu.Usuario = TabFinanc.Tables[0].Rows[I]["Usuario"].ToString().Trim();
                    CadUsu.GravarDados();
                    IdUsuario = CadUsu.IdUsuario;
                }

                if (TabFinanc.Tables[0].Rows[I]["IdLancServ"].ToString() != "")
                    IdLancServ = int.Parse(TabFinanc.Tables[0].Rows[I]["IdLancServ"].ToString());
                else
                    IdLancServ = 0;

                CadFinanceiro.LerDados(IdLancServ);
                CadFinanceiro.DataLanc = DateTime.Parse(TabFinanc.Tables[0].Rows[I]["DataLanc"].ToString());
                CadFinanceiro.PagRec = int.Parse(TabFinanc.Tables[0].Rows[I]["PagRec"].ToString());
                CadFinanceiro.IdPessoa = int.Parse(TabFinanc.Tables[0].Rows[I]["IdServPessoa"].ToString());
                CadFinanceiro.NumDoc = TabFinanc.Tables[0].Rows[I]["NumDocumento"].ToString().Trim();
                CadFinanceiro.Referente = TabFinanc.Tables[0].Rows[I]["Referente"].ToString().Trim();
                CadFinanceiro.NotaFiscal = TabFinanc.Tables[0].Rows[I]["NotaFiscal"].ToString().Trim();
                CadFinanceiro.VlrOriginal = decimal.Parse(TabFinanc.Tables[0].Rows[I]["VlrOriginal"].ToString().Replace(".",","));
                CadFinanceiro.Vencimento = DateTime.Parse(TabFinanc.Tables[0].Rows[I]["Vencimento"].ToString());
                CadFinanceiro.Observacao = TabFinanc.Tables[0].Rows[I]["Observacao"].ToString().Trim();
                CadFinanceiro.IdFilial = int.Parse(TabFinanc.Tables[0].Rows[I]["ID_Filial"].ToString());
                CadFinanceiro.IdTipoDocumento = IdTipoDoc;
                CadFinanceiro.IdVendedor = IdVendedor;
                CadFinanceiro.IdFormaPgto = IdFormaPgto;
                CadFinanceiro.IdUsuLanc = IdUsuario;
                CadFinanceiro.GravarDados();
                TabFinanc.Tables[0].Rows[I]["IDLancServ"] = CadFinanceiro.IdLanc.ToString();

                if (IdLancServ == 0)
                    RegAuditoria.Operacao = 1;
                else
                    RegAuditoria.Operacao = 2;
                RegAuditoria.IdUsuario = IdUsuario;
                RegAuditoria.Terminal = Conexao.WorkstationId;
                RegAuditoria.Data = DateTime.Now;
                RegAuditoria.IdChave = CadFinanceiro.IdLanc;
                RegAuditoria.Documento = CadFinanceiro.NumDoc.Trim();
                RegAuditoria.Opcao = "Financeiro WEB SERVICE: Filial: " + CadFinanceiro.IdFilial.ToString();
                RegAuditoria.Descricao = "Registro Via WEBSERVICE Usuario:" + NmUsuario.Trim();
                RegAuditoria.Registrar();
            }
            XMLRet.LoadXml(TabGetXml());
            return XMLRet;
        }

*/