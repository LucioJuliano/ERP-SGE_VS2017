using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using Controle_Dados;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace Controle_Dados
{
    public class Pessoas
    {
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private string _RazaoSocial;
        public string RazaoSocial
        {
            get { return _RazaoSocial; }
            set { _RazaoSocial = value; }
        }
        private string _Fantasia;
        public string Fantasia
        {
            get { return _Fantasia; }
            set { _Fantasia = value; }
        }
        private string _Cnpj;
        public string Cnpj
        {
            get { return _Cnpj; }
            set { _Cnpj = value; }
        }
        private string _InscUF;
        public string InscUF
        {
            get { return _InscUF; }
            set { _InscUF = value; }
        }
        private string _Cep;
        public string Cep
        {
            get { return _Cep; }
            set { _Cep = value; }
        }
        private string _Endereco;
        public string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; }
        }
        private string _Numero;
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        private string _Complemento;
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }
        private string _Bairro;
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        private string _Cidade;
        public string Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }
        private int _IdUF;
        public int IdUF
        {
            get { return _IdUF; }
            set { _IdUF = value; }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Fone;
        public string Fone
        {
            get { return _Fone; }
            set { _Fone = value; }
        }
        private string _Fax;
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        private string _Contato;
        public string Contato
        {
            get { return _Contato; }
            set { _Contato = value; }
        }
        private string _Celular;
        public string Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }
        private int _IdCusto;
        public int IdCusto
        {
            get { return _IdCusto; }
            set { _IdCusto = value; }
        }
        private int _IdDepartamento;
        public int IdDepartamento
        {
            get { return _IdDepartamento; }
            set { _IdDepartamento = value; }
        }
        private int _IdAtividade;
        public int IdAtividade
        {
            get { return _IdAtividade; }
            set { _IdAtividade = value; }
        }
        private int _IdTransportadora;
        public int IdTransportadora
        {
            get { return _IdTransportadora; }
            set { _IdTransportadora = value; }
        }
        private int _IdCfop;
        public int IdCfop
        {
            get { return _IdCfop; }
            set { _IdCfop = value; }
        }
        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }
        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private int _IdFormaPgto;
        public int IdFormaPgto
        {
            get { return _IdFormaPgto; }
            set { _IdFormaPgto = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _IdRota;
        public int IdRota
        {
            get { return _IdRota; }
            set { _IdRota = value; }
        }
        private int _Clie_Forn;
        public int Clie_Forn
        {
            get { return _Clie_Forn; }
            set { _Clie_Forn = value; }
        } // 0 - Ambos / 1-Cliente / 2-Fornecedor
        private string _CepCobranca;
        public string CepCobranca
        {
            get { return _CepCobranca; }
            set { _CepCobranca = value; }
        }
        private string _EndCobranca;
        public string EndCobranca
        {
            get { return _EndCobranca; }
            set { _EndCobranca = value; }
        }
        private string _NumCobranca;
        public string NumCobranca
        {
            get { return _NumCobranca; }
            set { _NumCobranca = value; }
        }
        private string _ComplCobranca;
        public string ComplCobranca
        {
            get { return _ComplCobranca; }
            set { _ComplCobranca = value; }
        }
        private string _BairroCobranca;
        public string BairroCobranca
        {
            get { return _BairroCobranca; }
            set { _BairroCobranca = value; }
        }
        private string _CidadeCobranca;
        public string CidadeCobranca
        {
            get { return _CidadeCobranca; }
            set { _CidadeCobranca = value; }
        }
        private int _IdUfCobranca;
        public int IdUfCobranca
        {
            get { return _IdUfCobranca; }
            set { _IdUfCobranca = value; }
        }
        private decimal _LimiteCredito;
        public decimal LimiteCredito
        {
            get { return _LimiteCredito; }
            set { _LimiteCredito = value; }
        }
        private DateTime _DataCadastro;
        public DateTime DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private int _Frete;
        public int Frete
        {
            get { return _Frete; }
            set { _Frete = value; }
        }
        private decimal _ComissaoFixa;
        public decimal ComissaoFixa
        {
            get { return _ComissaoFixa; }
            set { _ComissaoFixa = value; }
        }
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private decimal _Credito;
        public decimal Credito
        {
            get { return _Credito; }
            set { _Credito = value; }
        }
        private int _BloqFormaPgto;
        public int BloqFormaPgto
        {
            get { return _BloqFormaPgto; }
            set { _BloqFormaPgto = value; }
        }
        private string _ObsSerasa;
        public string ObsSerasa
        {
            get { return _ObsSerasa; }
            set { _ObsSerasa = value; }
        }
        private string _Senha;
        public string Senha
        {
            get { return _Senha; }
            set { _Senha = value; }
        }
        private int _IdServidor;
        public int IdServidor
        {
            get { return _IdServidor; }
            set { _IdServidor = value; }
        }
        private int _MargemNegocio;
        public int MargemNegocio
        {
            get { return _MargemNegocio; }
            set { _MargemNegocio = value; }
        }
        private string _Pais;
        public string Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }
        public Funcoes Controle;
        private string _EmailNFE;
        public string EmailNFE
        {
            get { return _EmailNFE; }
            set { _EmailNFE = value; }
        }
        private int _IdVinculo;
        public int IdVinculo
        {
            get { return _IdVinculo; }
            set { _IdVinculo = value; }
        }
        private int _NotificaAltPrc;
        public int NotificaAltPrc
        {
            get { return _NotificaAltPrc; }
            set { _NotificaAltPrc = value; }
        }
        private string _ObsEntrega;
        public string ObsEntrega
        {
            get { return _ObsEntrega; }
            set { _ObsEntrega = value; }
        }
        private string _PrazoPgto;
        public string PrazoPgto
        {
            get { return _PrazoPgto; }
            set { _PrazoPgto = value; }
        }
        private decimal _PDescNFGrpTalimpo;
        public decimal PDescNFGrpTalimpo
        {
            get { return _PDescNFGrpTalimpo; }
            set { _PDescNFGrpTalimpo = value; }
        }

        private decimal _PDescNFGrpOutros;
        public decimal PDescNFGrpOutros
        {
            get { return _PDescNFGrpOutros; }
            set { _PDescNFGrpOutros = value; }
        }

        private int _ForaMediaCom;
        public int ForaMediaCom
        {
            get { return _ForaMediaCom; }
            set { _ForaMediaCom = value; }
        }

        private int _NaoVerifQtdeCx;
        public int NaoVerifQtdeCx
        {
            get { return _NaoVerifQtdeCx; }
            set { _NaoVerifQtdeCx = value; }
        }


        private int _Comodato;
        public int Comodato
        {
            get { return _Comodato; }
            set { _Comodato = value; }
        }

        private int _KitNfe;
        public int KitNfe
        {
            get { return _KitNfe; }
            set { _KitNfe = value; }
        }

        private int _NaoVerPrazoPg;
        public int NaoVerPrazoPg
        {
            get { return _NaoVerPrazoPg; }
            set { _NaoVerPrazoPg = value; }
        }

        private int _CodMun;
        public int CodMun
        {
            get { return _CodMun; }
            set { _CodMun = value; }
        }

        private int _LiberaPrc;
        public int LiberaPrc
        {
            get { return _LiberaPrc; }
            set { _LiberaPrc = value; }
        }

        private int _Serasa;
        public int Serasa
        {
            get { return _Serasa; }
            set { _Serasa = value; }
        }
        public void LerDados(int Id)
        {
            IdPessoa = 0;
            RazaoSocial = "";
            Fantasia = "";
            Cnpj = "";
            InscUF = "";
            Cep = "";
            Endereco = "";
            Numero = "";
            Complemento = "";
            Bairro = "";
            Cidade = "";
            IdUF = 0;
            Fone = "";
            Fax = "";
            Email = "";
            Contato = "";
            Celular = "";
            IdCusto = 0;
            IdDepartamento = 0;
            IdAtividade = 0;
            IdTransportadora = 0;
            IdCfop = 0;
            Ativo = 0;
            Tipo = 0;
            IdFormaPgto = 0;
            Observacao = "";
            IdRota = 0;
            Clie_Forn = 0;
            CepCobranca = "";
            EndCobranca = "";
            NumCobranca = "";
            ComplCobranca = "";
            BairroCobranca = "";
            CidadeCobranca = "";
            IdUfCobranca = 0;
            LimiteCredito = 0;
            DataCadastro = DateTime.Now;
            IdFilial = 0;
            Frete = 0;
            ComissaoFixa = 0;
            IdVendedor = 0;
            Credito = 0;
            BloqFormaPgto = 0;
            ObsSerasa = "";
            Senha = "";
            IdServidor = 0;
            MargemNegocio = 0;
            Pais = "1058";
            EmailNFE = "";
            IdVinculo = 0;
            NotificaAltPrc = 0;
            ObsEntrega = "";
            PrazoPgto = "";
            PDescNFGrpTalimpo = 0;
            PDescNFGrpOutros = 0;
            ForaMediaCom = 0;
            NaoVerifQtdeCx = 0;
            Comodato = 0;
            KitNfe = 0;
            NaoVerPrazoPg = 0;
            CodMun = 2304400;
            LiberaPrc = 0;
            Serasa = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Pessoas WHERE Id_Pessoa=" + Id.ToString().Trim());
            
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdPessoa = Id;
                    RazaoSocial      = Tabela["RazaoSocial"].ToString().Trim();
                    Fantasia         = Tabela["Fantasia"].ToString().Trim();
                    Cnpj             = Tabela["Cnpj"].ToString().Trim();
                    InscUF           = Tabela["Insc_UF"].ToString().Trim();
                    Cep              = Tabela["CEP"].ToString().Trim();
                    Endereco         = Tabela["Endereco"].ToString().Trim();
                    Numero           = Tabela["Numero"].ToString().Trim();
                    Complemento      = Tabela["Complemento"].ToString().Trim();
                    Bairro           = Tabela["Bairro"].ToString().Trim();
                    Cidade           = Tabela["Cidade"].ToString().Trim();
                    IdUF             = int.Parse(Tabela["ID_UF"].ToString());
                    Fone             = Tabela["Fone"].ToString().Trim();
                    Fax              = Tabela["Fax"].ToString().Trim();
                    Email            = Tabela["Email"].ToString().Trim();
                    Contato          = Tabela["Contato"].ToString().Trim();
                    Celular          = Tabela["Celular"].ToString().Trim();
                    IdCusto          = int.Parse(Tabela["Id_Custo"].ToString());
                    IdDepartamento   = int.Parse(Tabela["Id_Departamento"].ToString());
                    IdAtividade      = int.Parse(Tabela["Id_Atividade"].ToString());
                    IdTransportadora = int.Parse(Tabela["Id_Transportadora"].ToString());
                    IdCfop           = int.Parse(Tabela["Id_Cfop"].ToString());
                    Ativo            = int.Parse(Tabela["Ativo"].ToString());
                    Tipo             = int.Parse(Tabela["Tipo"].ToString());
                    IdFormaPgto      = int.Parse(Tabela["Id_FormaPgto"].ToString());
                    Observacao       = Tabela["Observacao"].ToString().Trim();
                    Clie_Forn        = int.Parse(Tabela["Clie_Forn"].ToString());
                    IdRota           = int.Parse(Tabela["Id_Rota"].ToString());
                    CepCobranca      = Tabela["CEPCobranca"].ToString().Trim();
                    EndCobranca      = Tabela["EndCobranca"].ToString().Trim();
                    NumCobranca      = Tabela["NumCobranca"].ToString().Trim();
                    ComplCobranca    = Tabela["ComplCobranca"].ToString().Trim();
                    BairroCobranca   = Tabela["BairroCobranca"].ToString().Trim();
                    CidadeCobranca   = Tabela["CidadeCobranca"].ToString().Trim();
                    IdUfCobranca     = int.Parse(Tabela["ID_UFCobranca"].ToString());
                    LimiteCredito    = decimal.Parse(Tabela["LimiteCredito"].ToString());
                    DataCadastro     = DateTime.Parse(Tabela["DataCadastro"].ToString());
                    IdFilial         = int.Parse(Tabela["Id_Filial"].ToString());
                    Frete            = int.Parse(Tabela["Frete"].ToString());
                    ComissaoFixa     = decimal.Parse(Tabela["ComissaoFixa"].ToString());
                    IdVendedor       = int.Parse(Tabela["Id_Vendedor"].ToString());
                    Credito          = decimal.Parse(Tabela["Credito"].ToString());
                    BloqFormaPgto    = int.Parse(Tabela["BloqFormaPgto"].ToString());
                    ObsSerasa        = Tabela["Obs_SERASA"].ToString().Trim();
                    Senha            = Tabela["Senha"].ToString().Trim();
                    EmailNFE         = Tabela["EmailNFE"].ToString().Trim();
                    ObsEntrega       = Tabela["Obs_Entrega"].ToString().Trim();
                    PrazoPgto        = Tabela["PrazoPgto"].ToString().Trim();
                    

                    if (Tabela["IdServidor"].ToString() != "")
                        IdServidor = int.Parse(Tabela["IdServidor"].ToString());
                    
                    if (Tabela["MargemNegocio"].ToString() != "")
                        MargemNegocio = int.Parse(Tabela["MargemNegocio"].ToString());
                    
                    if (Tabela["Pais"].ToString() != "")
                        Pais = Tabela["Pais"].ToString().Trim();

                    if (Tabela["Id_Vinculo"].ToString() != "")
                        IdVinculo = int.Parse(Tabela["Id_Vinculo"].ToString().Trim());

                    if (Tabela["NotificaAltPrc"].ToString() != "")
                        NotificaAltPrc = int.Parse(Tabela["NotificaAltPrc"].ToString().Trim());

                    if (Tabela["PDescNFGrpTalimpo"].ToString() != "")
                        PDescNFGrpTalimpo = decimal.Parse(Tabela["PDescNFGrpTalimpo"].ToString().Trim());

                    if (Tabela["PDescNFGrpOutros"].ToString() != "")
                        PDescNFGrpOutros = decimal.Parse(Tabela["PDescNFGrpOutros"].ToString().Trim());

                    if (Tabela["ForaMediaCom"].ToString() != "")
                        ForaMediaCom = int.Parse(Tabela["ForaMediaCom"].ToString().Trim());

                    if (Tabela["NaoVerifQtdeCx"].ToString() != "")
                        NaoVerifQtdeCx = int.Parse(Tabela["NaoVerifQtdeCx"].ToString().Trim());

                    if (Tabela["Comodato"].ToString() != "")
                        Comodato = int.Parse(Tabela["Comodato"].ToString().Trim());

                    if (Tabela["KitNfe"].ToString() != "")
                        KitNfe = int.Parse(Tabela["KitNfe"].ToString().Trim());

                    if (Tabela["NaoVerPrazoPg"].ToString() != "")
                        NaoVerPrazoPg = int.Parse(Tabela["NaoVerPrazoPg"].ToString().Trim());

                    if (Tabela["CodMun"].ToString() != "")
                        CodMun = int.Parse(Tabela["CodMun"].ToString().Trim());

                    if (Tabela["LiberaPrc"].ToString() != "")
                        LiberaPrc = int.Parse(Tabela["LiberaPrc"].ToString().Trim());

                    if (Tabela["Serasa"].ToString() != "")
                        Serasa = int.Parse(Tabela["Serasa"].ToString().Trim());


                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdPessoa > 0)
            {
                sSQL = "UPDATE Pessoas SET Id_Pessoa=@Id,RazaoSocial=@RazaoSocial,Fantasia=@Fantasia,Cnpj=@Cnpj,Insc_Uf=@InscUF,Endereco=@Endereco," +
                       "Numero=@Numero,Complemento=@Complemento,Cep=@Cep,Bairro=@Bairro,Cidade=@Cidade,Id_UF=@UF,Fone=@Fone,Fax=@Fax,Email=@Email,Contato=@Contato,Celular=@Celular,"+ 
                       "Id_Custo=@IdCusto,Id_Departamento=@IdDepartamento,Id_Atividade=@IdAtividade,Id_Transportadora=@IdTransportadora,Id_Cfop=@IdCfop,Ativo=@Ativo,Tipo=@Tipo,"+
                       "Id_FormaPgto=@IdFormaPgto,Observacao=@Observacao,Id_Rota=@IdRota,CepCobranca=@CepCobranca,EndCobranca=@EndCobranca,NumCobranca=@NumCobranca,ComplCobranca=@ComplCobranca,"+
                       "BairroCobranca=@BairroCobranca,CidadeCobranca=@CidadeCobranca,Id_UFCobranca=@IdUFCobranca,LimiteCredito=@LimiteCredito,DataCadastro=Convert(DateTime,@DataCadastro,103),Clie_Forn=@ClieForn,"+
                       "Id_Filial=@IdFilial,Frete=@Frete,ComissaoFixa=@ComissaoFixa,Id_Vendedor=@IdVendedor,BloqFormaPgto=@BloqFormaPgto,Obs_Serasa=@ObsSerasa,Senha=@Psw,IdServidor=@IdServidor,MargemNegocio=@MargemNegocio,"+
                       "Pais=@Pais,EmailNFE=@EmailNFE,Id_Vinculo=@IdVinculo,NotificaAltPrc=@NotificaAltPrc,Obs_Entrega=@ObsEntrega,PrazoPgto=@PrazoPgto,PDescNFGrpTalimpo=@PDescNFGrpTalimpo,PDescNFGrpOutros=@PDescNFGrpOutros,"+
                       "ForaMediaCom=@ForaMediaCom,NaoVerifQtdeCx=@NaoVerifQtdeCx,Comodato=@Comodato,KitNfe=@KitNfe,NaoVerPrazoPg=@NaoVerPrazoPg,CodMun=@CodMun,LiberaPrc=@LiberaPrc,Serasa=@Serasa Where Id_Pessoa=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdPessoa);
            }
            else
            {
                IdPessoa = Controle.ProximoID("Pessoas");
                sSQL = "INSERT INTO Pessoas (Id_Pessoa,RazaoSocial,Fantasia,Cnpj,Insc_Uf,Endereco,Numero,Complemento,Cep,Bairro,Cidade,Id_UF,Fone,Fax,Email,Contato,Celular,Id_Custo,Id_Departamento,Id_Atividade,"+
                       "Id_Transportadora,Id_Cfop,Ativo,Tipo,Id_FormaPgto,Observacao,Id_Rota,EndCobranca,NumCobranca,ComplCobranca,CepCobranca,BairroCobranca,CidadeCobranca,Id_UFCobranca,LimiteCredito,DataCadastro,Clie_Forn,Id_Filial,Frete,ComissaoFixa,Id_Vendedor,Credito,BloqFormaPgto,Obs_Serasa,Senha,IdServidor,MargemNegocio,Pais,EmailNFE,"+
                       "Id_Vinculo,NotificaAltPrc,Obs_Entrega,PrazoPgto,PDescNFGrpTalimpo,PDescNFGrpOutros,ForaMediaCom,NaoVerifQtdeCx,Comodato,KitNfe,NaoVerPrazoPg,CodMun,LiberaPrc,Serasa)" +
                       " Values (@Id,@RazaoSocial,@Fantasia,@Cnpj,@InscUF,@Endereco,@Numero,@Complemento,@Cep,@Bairro,@Cidade,@UF,@Fone,@Fax,@Email,@Contato,@Celular,@IdCusto,@IdDepartamento,@IdAtividade,@IdTransportadora,@IdCfop,@Ativo,@Tipo,@IdFormaPgto,@Observacao,@IdRota,@EndCobranca,@NumCobranca,@ComplCobranca,@CepCobranca,"+
                       "@BairroCobranca,@CidadeCobranca,@IdUFCobranca,@LimiteCredito,convert(DateTime,@DataCadastro,103),@ClieForn,@IdFilial,@Frete,@ComissaoFixa,@IdVendedor,0,@BloqFormaPgto,@ObsSerasa,@Psw,@IdServidor,@MargemNegocio,@Pais,@EmailNFE,@IdVinculo,@NotificaAltPrc,@ObsEntrega,@PrazoPgto,@PDescNFGrpTalimpo,@PDescNFGrpOutros,@ForaMediaCom,@NaoVerifQtdeCx,@Comodato,@KitNfe,@NaoVerPrazoPg,@CodMun,@LiberaPrc,@Serasa)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdPessoa);
                Nm_param.Add("@RazaoSocial"); Vr_param.Add(RazaoSocial);
                Nm_param.Add("@Fantasia"); Vr_param.Add(Fantasia);
                Nm_param.Add("@Cnpj"); Vr_param.Add(Cnpj);
                Nm_param.Add("@InscUF"); Vr_param.Add(InscUF);
                Nm_param.Add("@Cep"); Vr_param.Add(Cep);
                Nm_param.Add("@Endereco"); Vr_param.Add(Endereco);
                Nm_param.Add("@Numero"); Vr_param.Add(Numero);
                Nm_param.Add("@Complemento"); Vr_param.Add(Complemento);
                Nm_param.Add("@Bairro"); Vr_param.Add(Bairro);
                Nm_param.Add("@Cidade"); Vr_param.Add(Cidade);
                Nm_param.Add("@UF"); Vr_param.Add(IdUF);
                Nm_param.Add("@Fone"); Vr_param.Add(Fone);
                Nm_param.Add("@Fax"); Vr_param.Add(Fax);
                Nm_param.Add("@Email"); Vr_param.Add(Email);
                Nm_param.Add("@Contato"); Vr_param.Add(Contato);
                Nm_param.Add("@Celular"); Vr_param.Add(Celular);
                Nm_param.Add("@IdCusto"); Vr_param.Add(IdCusto);
                Nm_param.Add("@IdDepartamento"); Vr_param.Add(IdDepartamento);
                Nm_param.Add("@IdAtividade"); Vr_param.Add(IdAtividade);
                Nm_param.Add("@IdTransportadora"); Vr_param.Add(IdTransportadora);
                Nm_param.Add("@IdCfop"); Vr_param.Add(IdCfop);
                Nm_param.Add("@Ativo"); Vr_param.Add(Ativo);
                Nm_param.Add("@Tipo"); Vr_param.Add(Tipo);
                Nm_param.Add("@IdFormaPgto"); Vr_param.Add(IdFormaPgto);
                Nm_param.Add("@Observacao"); Vr_param.Add(Observacao);
                Nm_param.Add("@CepCobranca"); Vr_param.Add(CepCobranca);
                Nm_param.Add("@IdRota"); Vr_param.Add(IdRota);
                Nm_param.Add("@EndCobranca"); Vr_param.Add(EndCobranca);
                Nm_param.Add("@NumCobranca"); Vr_param.Add(NumCobranca);
                Nm_param.Add("@ComplCobranca"); Vr_param.Add(ComplCobranca);
                Nm_param.Add("@BairroCobranca"); Vr_param.Add(BairroCobranca);
                Nm_param.Add("@CidadeCobranca"); Vr_param.Add(CidadeCobranca);
                Nm_param.Add("@IdUFCobranca"); Vr_param.Add(IdUfCobranca);
                Nm_param.Add("@LimiteCredito"); Vr_param.Add(Controle.FloatToStr(LimiteCredito,2));
                Nm_param.Add("@DataCadastro"); Vr_param.Add(DataCadastro.ToShortDateString());
                Nm_param.Add("@ClieForn"); Vr_param.Add(Clie_Forn);
                Nm_param.Add("@IdFilial"); Vr_param.Add(IdFilial);
                Nm_param.Add("@Frete"); Vr_param.Add(Frete);
                Nm_param.Add("@ComissaoFixa"); Vr_param.Add(ComissaoFixa);
                Nm_param.Add("@IdVendedor"); Vr_param.Add(IdVendedor);
                Nm_param.Add("@BloqFormaPgto"); Vr_param.Add(BloqFormaPgto);
                Nm_param.Add("@ObsSerasa"); Vr_param.Add(ObsSerasa);
                Nm_param.Add("@Psw"); Vr_param.Add(Senha);
                Nm_param.Add("@IdServidor"); Vr_param.Add(IdServidor);
                Nm_param.Add("@MargemNegocio"); Vr_param.Add(MargemNegocio);
                Nm_param.Add("@Pais"); Vr_param.Add(Pais);
                Nm_param.Add("@EmailNFE"); Vr_param.Add(EmailNFE);
                Nm_param.Add("@IdVinculo"); Vr_param.Add(IdVinculo);
                Nm_param.Add("@NotificaAltPrc"); Vr_param.Add(NotificaAltPrc);
                Nm_param.Add("@ObsEntrega"); Vr_param.Add(ObsEntrega);
                Nm_param.Add("@PrazoPgto"); Vr_param.Add(PrazoPgto);
                Nm_param.Add("@PDescNFGrpTalimpo"); Vr_param.Add(PDescNFGrpTalimpo);
                Nm_param.Add("@PDescNFGrpOutros"); Vr_param.Add(PDescNFGrpOutros);
                Nm_param.Add("@ForaMediaCom"); Vr_param.Add(ForaMediaCom);
                Nm_param.Add("@NaoVerifQtdeCx"); Vr_param.Add(NaoVerifQtdeCx);
                Nm_param.Add("@Comodato"); Vr_param.Add(Comodato);
                Nm_param.Add("@KitNfe"); Vr_param.Add(KitNfe);
                Nm_param.Add("@NaoVerPrazoPg"); Vr_param.Add(NaoVerPrazoPg);
                Nm_param.Add("@CodMun"); Vr_param.Add(CodMun);
                Nm_param.Add("@LiberaPrc"); Vr_param.Add(LiberaPrc);
                Nm_param.Add("@Serasa"); Vr_param.Add(Serasa);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdPessoa > 0)
            {                
                Controle.ExecutaSQL("DELETE FROM Pessoas WHERE Id_Pessoa=" + IdPessoa.ToString().Trim());
                if (Clie_Forn==4)
                    Controle.ExecutaSQL("UPDATE Pessoas SET ID_VINCULO=0 WHERE ID_VINCULO=" + IdPessoa.ToString().Trim());
            }
        }        
    }
}

