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

namespace WSCadPessoa
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AtualizarCadastro : System.Web.Services.WebService
    {
        private string StringConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

        public void RegistroLog(string erro)
        {
            string ArqLog = Server.MapPath("~/") + "ERP_SGE_Log.LOG";
            StreamWriter Registrar;

            if (!File.Exists(ArqLog))
                Registrar = new StreamWriter(ArqLog);
            else
                Registrar = File.AppendText(ArqLog);

            Registrar.WriteLine(DateTime.Now.ToString());
            Registrar.WriteLine("   " + erro);
            Registrar.WriteLine("***----------------------------------------------------------------------***");
            Registrar.Close();
        }

        [WebMethod]
        public int Atualizar(XmlDocument XMLCad,int IdUsuario,int IdFilial)
        {
            
            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();
                
                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                DataSet TabCad = new DataSet();
                XmlNodeReader xmlReader = new XmlNodeReader(XMLCad);
                TabCad.ReadXml(xmlReader);

                int IdCad = 0;
                bool NovoReg = true;
                if (TabCad.Tables[0].Rows.Count > 0)
                {

                    string Cnpj = TabCad.Tables[0].Rows[0]["Cnpj"].ToString().Trim();
                    if ((Cnpj == "00000000000000" || Cnpj == "11111111111111" || Cnpj == "22222222222222" || Cnpj == "33333333333333" || Cnpj == "44444444444444"
                           || Cnpj == "55555555555555" || Cnpj == "66666666666666" || Cnpj == "77777777777777" || Cnpj == "88888888888888" || Cnpj == "99999999999999"
                           || Cnpj == "00000000000" || Cnpj == "11111111111" || Cnpj == "22222222222" || Cnpj == "33333333333" || Cnpj == "44444444444"
                           || Cnpj == "55555555555" || Cnpj == "66666666666" || Cnpj == "77777777777" || Cnpj == "88888888888" || Cnpj == "99999999999"))
                    {
                        Conexao.Dispose();
                        return 0;
                    }

                    //SqlDataReader PesqCad = Executar.ConsultaSQL("SELECT * FROM Pessoas WHERE Id_Pessoa=" + TabCad.Tables[0].Rows[0]["IdServidor"].ToString().Trim());
                    SqlDataReader PesqCad = Executar.ConsultaSQL("SELECT * FROM Pessoas WHERE Cnpj='" + TabCad.Tables[0].Rows[0]["Cnpj"].ToString().Trim() + "'");

                    if (PesqCad.HasRows)
                    {
                        PesqCad.Read();
                        IdCad = int.Parse(PesqCad["Id_Pessoa"].ToString());
                    }                    
                }
                else
                {
                    Conexao.Dispose();
                    return 0;
                }
                              
                Auditoria RegAuditoria = new Auditoria();
                RegAuditoria.Controle  = Executar;
                
                Pessoas CadPessoa  = new Pessoas();
                CadPessoa.Controle = Executar;

                NovoReg = IdCad == 0;
                CadPessoa.LerDados(IdCad);                     
                CadPessoa.RazaoSocial      = TabCad.Tables[0].Rows[0]["RazaoSocial"].ToString().Trim();
                CadPessoa.Tipo             = int.Parse(TabCad.Tables[0].Rows[0]["Tipo"].ToString());
                CadPessoa.Fantasia         = TabCad.Tables[0].Rows[0]["Fantasia"].ToString().Trim();
                CadPessoa.Cnpj             = TabCad.Tables[0].Rows[0]["Cnpj"].ToString().Trim();
                CadPessoa.InscUF           = TabCad.Tables[0].Rows[0]["Insc_UF"].ToString().Trim();
                CadPessoa.Cep              = TabCad.Tables[0].Rows[0]["CEP"].ToString().Trim();
                CadPessoa.Endereco         = TabCad.Tables[0].Rows[0]["Endereco"].ToString().Trim();
                CadPessoa.Numero           = TabCad.Tables[0].Rows[0]["Numero"].ToString().Trim();
                CadPessoa.Complemento      = TabCad.Tables[0].Rows[0]["Complemento"].ToString().Trim();
                CadPessoa.Bairro           = TabCad.Tables[0].Rows[0]["Bairro"].ToString().Trim();
                CadPessoa.Cidade           = TabCad.Tables[0].Rows[0]["Cidade"].ToString().Trim();
                CadPessoa.IdUF             = int.Parse(TabCad.Tables[0].Rows[0]["ID_UF"].ToString());
                CadPessoa.Fone             = TabCad.Tables[0].Rows[0]["Fone"].ToString().Trim();
                CadPessoa.Fax              = TabCad.Tables[0].Rows[0]["Fax"].ToString().Trim();
                CadPessoa.Email            = TabCad.Tables[0].Rows[0]["Email"].ToString().Trim();
                CadPessoa.Contato          = TabCad.Tables[0].Rows[0]["Contato"].ToString().Trim();
                CadPessoa.Celular          = TabCad.Tables[0].Rows[0]["Celular"].ToString().Trim();
                CadPessoa.IdCusto          = int.Parse(TabCad.Tables[0].Rows[0]["Id_Custo"].ToString());
                CadPessoa.IdDepartamento   = int.Parse(TabCad.Tables[0].Rows[0]["Id_Departamento"].ToString());
                CadPessoa.IdAtividade      = int.Parse(TabCad.Tables[0].Rows[0]["Id_Atividade"].ToString());
                CadPessoa.IdTransportadora = int.Parse(TabCad.Tables[0].Rows[0]["Id_Transportadora"].ToString());
                CadPessoa.IdFormaPgto      = int.Parse(TabCad.Tables[0].Rows[0]["Id_FormaPgto"].ToString());
                CadPessoa.BloqFormaPgto    = int.Parse(TabCad.Tables[0].Rows[0]["BloqFormaPgto"].ToString());                
                CadPessoa.IdCfop           = int.Parse(TabCad.Tables[0].Rows[0]["Id_Cfop"].ToString());
                CadPessoa.Observacao       = TabCad.Tables[0].Rows[0]["Observacao"].ToString().Trim();                
                CadPessoa.IdRota           = int.Parse(TabCad.Tables[0].Rows[0]["Id_Rota"].ToString());
                CadPessoa.CepCobranca      = TabCad.Tables[0].Rows[0]["CEPCobranca"].ToString().Trim();
                CadPessoa.EndCobranca      = TabCad.Tables[0].Rows[0]["EndCobranca"].ToString().Trim();
                CadPessoa.NumCobranca      = TabCad.Tables[0].Rows[0]["NumCobranca"].ToString().Trim();
                CadPessoa.ComplCobranca    = TabCad.Tables[0].Rows[0]["ComplCobranca"].ToString().Trim();
                CadPessoa.BairroCobranca   = TabCad.Tables[0].Rows[0]["BairroCobranca"].ToString().Trim();
                CadPessoa.CidadeCobranca   = TabCad.Tables[0].Rows[0]["CidadeCobranca"].ToString().Trim();
                CadPessoa.IdUfCobranca     = int.Parse(TabCad.Tables[0].Rows[0]["ID_UFCobranca"].ToString());                                
                CadPessoa.Frete            = int.Parse(TabCad.Tables[0].Rows[0]["Frete"].ToString());
                CadPessoa.EmailNFE         = TabCad.Tables[0].Rows[0]["EmailNFE"].ToString().Trim();
                CadPessoa.IdVinculo        = int.Parse(TabCad.Tables[0].Rows[0]["Id_Vinculo"].ToString());
                CadPessoa.MargemNegocio    = int.Parse(TabCad.Tables[0].Rows[0]["MargemNegocio"].ToString());
                CadPessoa.NotificaAltPrc   = int.Parse(TabCad.Tables[0].Rows[0]["NotificaAltPrc"].ToString());
                CadPessoa.PrazoPgto        = TabCad.Tables[0].Rows[0]["PrazoPgto"].ToString().Trim();
                CadPessoa.ObsEntrega       = TabCad.Tables[0].Rows[0]["Obs_Entrega"].ToString().Trim();
                CadPessoa.CodMun           = int.Parse(TabCad.Tables[0].Rows[0]["CodMun"].ToString());
                CadPessoa.LimiteCredito    = decimal.Parse(TabCad.Tables[0].Rows[0]["LimiteCredito"].ToString());

                if (CadPessoa.IdPessoa == 0)
                {
                    int IdVendedor = 0;

                    if (TabCad.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim() != "")
                    {
                        Vendedores Vendedor = new Vendedores();
                        Verificar VerificarCad = new Verificar();
                        Vendedor.Controle = Executar;
                        VerificarCad.Controle = Executar;

                        IdVendedor = VerificarCad.Verificar_ExisteCadastro("Id_Vendedor", "SELECT * FROM VENDEDORES WHERE VENDEDOR='" + TabCad.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim() + "'");

                        if (IdVendedor == 0)
                        {
                            Vendedor.LerDados(0);
                            Vendedor.Vendedor = TabCad.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim();
                            Vendedor.Ativo = 0;
                            Vendedor.GravarDados();
                            IdVendedor = Vendedor.IdVendedor;
                        }
                    }
                    CadPessoa.PDescNFGrpOutros  = 0;
                    CadPessoa.PDescNFGrpTalimpo = 0;
                    CadPessoa.ForaMediaCom      = 0;
                    CadPessoa.NaoVerifQtdeCx    = 0;
                    CadPessoa.IdFilial          = int.Parse(TabCad.Tables[0].Rows[0]["Id_Filial"].ToString());
                    CadPessoa.Clie_Forn         = int.Parse(TabCad.Tables[0].Rows[0]["Clie_Forn"].ToString());
                    CadPessoa.IdVendedor        = IdVendedor;
                    CadPessoa.LimiteCredito     = 500;
                    CadPessoa.Ativo             = 1;
                }
                CadPessoa.GravarDados();

                if (NovoReg && CadPessoa.IdServidor==0)
                {
                    //Executar.ExecutaSQL("Update Pessoas set IdServidor=" + CadPessoa.IdPessoa.ToString() + " Where Id_Pessoa=" + CadPessoa.IdPessoa.ToString());
                    RegAuditoria.Operacao = 1;
                }
                else
                    RegAuditoria.Operacao = 2; 
               
                RegAuditoria.IdUsuario = IdUsuario;
                RegAuditoria.Terminal  = Conexao.WorkstationId;
                RegAuditoria.Data      = DateTime.Now;                
                RegAuditoria.IdChave   = CadPessoa.IdPessoa;
                RegAuditoria.Documento = CadPessoa.Cnpj;
                RegAuditoria.Opcao     = "WEB SERVICE: Filial: " + IdFilial.ToString();
                RegAuditoria.Descricao = "Registro Via WEBSERVICE";
                RegAuditoria.Registrar();
                int Id = CadPessoa.IdPessoa;
                Conexao.Dispose();
                return Id;
            }
            catch (Exception erro)
            {
                RegistroLog("Erro Registro de Venda: " + erro.ToString());
                return 0;  // Erro na consulta
            }
        }

        [WebMethod]
        public int AtualizarFilial(XmlDocument XMLCad, int IdUsuario, int IdFilial)
        {

            SqlConnection Conexao = null;
            try
            {
                //string StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();

                Funcoes Executar = new Funcoes();
                Executar.Conexao = Conexao;

                DataSet TabCad = new DataSet();
                XmlNodeReader xmlReader = new XmlNodeReader(XMLCad);
                TabCad.ReadXml(xmlReader);

                int IdCad = 0;
                bool NovoReg = true;

                if (TabCad.Tables[0].Rows.Count > 0)
                {
                    string Cnpj = TabCad.Tables[0].Rows[0]["Cnpj"].ToString().Trim();
                    if ((Cnpj == "00000000000000" || Cnpj == "11111111111111" || Cnpj == "22222222222222" || Cnpj == "33333333333333" || Cnpj == "44444444444444"
                           || Cnpj == "55555555555555" || Cnpj == "66666666666666" || Cnpj == "77777777777777" || Cnpj == "88888888888888" || Cnpj == "99999999999999"
                           || Cnpj == "00000000000" || Cnpj == "11111111111" || Cnpj == "22222222222" || Cnpj == "33333333333" || Cnpj == "44444444444"
                           || Cnpj == "55555555555" || Cnpj == "66666666666" || Cnpj == "77777777777" || Cnpj == "88888888888" || Cnpj == "99999999999"))
                    {
                        Conexao.Dispose();
                        return 0;
                    }

                    SqlDataReader PesqCad = Executar.ConsultaSQL("SELECT * FROM Pessoas WHERE Cnpj='" + TabCad.Tables[0].Rows[0]["Cnpj"].ToString().Trim() + "'");
                    if (PesqCad.HasRows)
                    {
                        PesqCad.Read();
                        IdCad = int.Parse(PesqCad["Id_Pessoa"].ToString());
                    }                    
                }
                else
                {
                    Conexao.Dispose();
                    return 0;
                }

                Auditoria RegAuditoria = new Auditoria();
                RegAuditoria.Controle = Executar;

                Pessoas CadPessoa = new Pessoas();
                CadPessoa.Controle = Executar;

                CadPessoa.LerDados(IdCad);

                if (CadPessoa.IdPessoa > 0)
                    NovoReg = false;

                CadPessoa.RazaoSocial      = TabCad.Tables[0].Rows[0]["RazaoSocial"].ToString().Trim();
                CadPessoa.Tipo             = int.Parse(TabCad.Tables[0].Rows[0]["Tipo"].ToString());
                CadPessoa.Fantasia         = TabCad.Tables[0].Rows[0]["Fantasia"].ToString().Trim();
                CadPessoa.Cnpj             = TabCad.Tables[0].Rows[0]["Cnpj"].ToString().Trim();
                CadPessoa.InscUF           = TabCad.Tables[0].Rows[0]["Insc_UF"].ToString().Trim();
                CadPessoa.Cep              = TabCad.Tables[0].Rows[0]["CEP"].ToString().Trim();
                CadPessoa.Endereco         = TabCad.Tables[0].Rows[0]["Endereco"].ToString().Trim();
                CadPessoa.Numero           = TabCad.Tables[0].Rows[0]["Numero"].ToString().Trim();
                CadPessoa.Complemento      = TabCad.Tables[0].Rows[0]["Complemento"].ToString().Trim();
                CadPessoa.Bairro           = TabCad.Tables[0].Rows[0]["Bairro"].ToString().Trim();
                CadPessoa.Cidade           = TabCad.Tables[0].Rows[0]["Cidade"].ToString().Trim();
                CadPessoa.IdUF             = int.Parse(TabCad.Tables[0].Rows[0]["ID_UF"].ToString());
                CadPessoa.Fone             = TabCad.Tables[0].Rows[0]["Fone"].ToString().Trim();
                CadPessoa.Fax              = TabCad.Tables[0].Rows[0]["Fax"].ToString().Trim();
                CadPessoa.Email            = TabCad.Tables[0].Rows[0]["Email"].ToString().Trim();
                CadPessoa.Contato          = TabCad.Tables[0].Rows[0]["Contato"].ToString().Trim();
                CadPessoa.Celular          = TabCad.Tables[0].Rows[0]["Celular"].ToString().Trim();
                CadPessoa.IdCusto          = int.Parse(TabCad.Tables[0].Rows[0]["Id_Custo"].ToString());
                CadPessoa.IdDepartamento   = int.Parse(TabCad.Tables[0].Rows[0]["Id_Departamento"].ToString());
                CadPessoa.IdAtividade      = int.Parse(TabCad.Tables[0].Rows[0]["Id_Atividade"].ToString());
                CadPessoa.IdTransportadora = int.Parse(TabCad.Tables[0].Rows[0]["Id_Transportadora"].ToString());
                CadPessoa.IdCfop           = int.Parse(TabCad.Tables[0].Rows[0]["Id_Cfop"].ToString());
                CadPessoa.Observacao       = TabCad.Tables[0].Rows[0]["Observacao"].ToString().Trim();
                CadPessoa.IdRota           = int.Parse(TabCad.Tables[0].Rows[0]["Id_Rota"].ToString());
                CadPessoa.CepCobranca      = TabCad.Tables[0].Rows[0]["CEPCobranca"].ToString().Trim();
                CadPessoa.EndCobranca      = TabCad.Tables[0].Rows[0]["EndCobranca"].ToString().Trim();
                CadPessoa.NumCobranca      = TabCad.Tables[0].Rows[0]["NumCobranca"].ToString().Trim();
                CadPessoa.ComplCobranca    = TabCad.Tables[0].Rows[0]["ComplCobranca"].ToString().Trim();
                CadPessoa.BairroCobranca   = TabCad.Tables[0].Rows[0]["BairroCobranca"].ToString().Trim();
                CadPessoa.CidadeCobranca   = TabCad.Tables[0].Rows[0]["CidadeCobranca"].ToString().Trim();
                CadPessoa.IdUfCobranca     = int.Parse(TabCad.Tables[0].Rows[0]["ID_UFCobranca"].ToString());
                CadPessoa.Frete            = int.Parse(TabCad.Tables[0].Rows[0]["Frete"].ToString());
                CadPessoa.EmailNFE         = TabCad.Tables[0].Rows[0]["EmailNFE"].ToString().Trim();
                CadPessoa.IdVinculo        = int.Parse(TabCad.Tables[0].Rows[0]["Id_Vinculo"].ToString());
                CadPessoa.MargemNegocio    = int.Parse(TabCad.Tables[0].Rows[0]["MargemNegocio"].ToString());
                CadPessoa.NotificaAltPrc   = int.Parse(TabCad.Tables[0].Rows[0]["NotificaAltPrc"].ToString());
                CadPessoa.PrazoPgto        = TabCad.Tables[0].Rows[0]["PrazoPgto"].ToString().Trim();
                CadPessoa.ObsEntrega       = TabCad.Tables[0].Rows[0]["Obs_Entrega"].ToString().Trim();
                CadPessoa.LimiteCredito    = decimal.Parse(TabCad.Tables[0].Rows[0]["LimiteCredito"].ToString());
                CadPessoa.PDescNFGrpOutros = 0;
                CadPessoa.PDescNFGrpTalimpo= 0;
                CadPessoa.ForaMediaCom     = 0;
                CadPessoa.NaoVerifQtdeCx   = 0;
                //CadPessoa.IdServidor       = int.Parse(TabCad.Tables[0].Rows[0]["IdServidor"].ToString());

                if (CadPessoa.IdPessoa == 0)
                {
                    int IdVendedor = 0;

                    if (TabCad.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim() != "")
                    {
                        Vendedores Vendedor    = new Vendedores();
                        Verificar VerificarCad = new Verificar();
                        Vendedor.Controle      = Executar;
                        VerificarCad.Controle  = Executar;

                        IdVendedor = VerificarCad.Verificar_ExisteCadastro("Id_Vendedor", "SELECT * FROM VENDEDORES WHERE VENDEDOR='" + TabCad.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim() + "'");

                        if (IdVendedor == 0)
                        {
                            Vendedor.LerDados(0);
                            Vendedor.Vendedor = TabCad.Tables[0].Rows[0]["NomeVendedor"].ToString().Trim();
                            Vendedor.Ativo    = 0;
                            Vendedor.GravarDados();
                            IdVendedor = Vendedor.IdVendedor;
                        }
                    }

                    CadPessoa.IdFilial      = int.Parse(TabCad.Tables[0].Rows[0]["Id_Filial"].ToString());
                    CadPessoa.Clie_Forn     = int.Parse(TabCad.Tables[0].Rows[0]["Clie_Forn"].ToString());
                    CadPessoa.IdVendedor    = IdVendedor;
                    CadPessoa.LimiteCredito = 500;
                    CadPessoa.Ativo = 1;
                }

                CadPessoa.GravarDados();

                if (NovoReg && CadPessoa.IdServidor==0)
                    RegAuditoria.Operacao = 1;
                else
                    RegAuditoria.Operacao = 2;

                RegAuditoria.IdUsuario = IdUsuario;
                RegAuditoria.Terminal  = Conexao.WorkstationId;
                RegAuditoria.Data      = DateTime.Now;
                RegAuditoria.IdChave   = CadPessoa.IdPessoa;
                RegAuditoria.Documento = CadPessoa.Cnpj;
                RegAuditoria.Opcao     = "WEB SERVICE: Filial: " + IdFilial.ToString();
                RegAuditoria.Descricao = "Registro Via WEBSERVICE";
                RegAuditoria.Registrar();
                int Id = CadPessoa.IdPessoa;
                Conexao.Dispose();
                return Id;
            }
            catch (Exception erro)
            {
                RegistroLog("Erro Registro de Venda Filiais: " + erro.ToString());
                return 0;  // Erro na consulta
            }
        }
    }

}
