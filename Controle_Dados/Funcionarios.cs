using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using Controle_Dados;

namespace Controle_Dados
{
    public class Funcionarios
    {
        private int _IdFunc;
        public int IdFunc
        {
            get { return _IdFunc; }
            set { _IdFunc = value; }
        }
        private int _Matricula;
        public int Matricula
        {
            get { return _Matricula; }
            set { _Matricula = value; }
        }
        private string _Nome;
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        private String _Cep;
        public String Cep
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
        private int _IdUf;
        public int IdUf
        {
            get { return _IdUf; }
            set { _IdUf = value; }
        }
        private string _Telefone;
        public string Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value; }
        }
        private string _Celular;
        public string Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }
        private DateTime _DtNasc;
        public DateTime DtNasc
        {
            get { return _DtNasc; }
            set { _DtNasc = value; }
        }
        private string _Rg;
        public string Rg
        {
            get { return _Rg; }
            set { _Rg = value; }
        }
        private DateTime _DtEmissao;
        public DateTime DtEmissao
        {
            get { return _DtEmissao; }
            set { _DtEmissao = value; }
        }
        private string _NomePai;
        public string NomePai
        {
            get { return _NomePai; }
            set { _NomePai = value; }
        }
        private string _NomeMae;
        public string NomeMae
        {
            get { return _NomeMae; }
            set { _NomeMae = value; }
        }
        private string _Cpf;                
        public string Cpf
        {
            get { return _Cpf; }
            set { _Cpf = value; }
        }
        private string _Ctps;
        public string Ctps
        {
            get { return _Ctps; }
            set { _Ctps = value; }
        }
        private string _Serie;
        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }
        private string _TituloEleitoral;
        public string TituloEleitoral
        {
            get { return _TituloEleitoral; }
            set { _TituloEleitoral = value; }
        }
        private string _RefPessoal;
        public string RefPessoal
        {
            get { return _RefPessoal; }
            set { _RefPessoal = value; }
        }
        private string _RefTelefone;
        public string RefTelefone
        {
            get { return _RefTelefone; }
            set { _RefTelefone = value; }
        }
        private string _Parentesco;
        public string Parentesco
        {
            get { return _Parentesco; }
            set { _Parentesco = value; }
        }
        private int _TipoConta;
        public int TipoConta
        {
            get { return _TipoConta; }
            set { _TipoConta = value; }
        }
        private string _Banco;
        public string Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
        }
        private string _Agencia;
        public string Agencia
        {
            get { return _Agencia; }
            set { _Agencia = value; }
        }
        private string _Conta;
        public string Conta
        {
            get { return _Conta; }
            set { _Conta = value; }
        }
        private DateTime _DtAdmissao;
        public DateTime DtAdmissao
        {
            get { return _DtAdmissao; }
            set { _DtAdmissao = value; }
        }
        private decimal _SalarioCtps;
        public decimal SalarioCtps
        {
            get { return _SalarioCtps; }
            set { _SalarioCtps = value; }
        }
        private decimal _SalarioAtual;
        public decimal SalarioAtual
        {
            get { return _SalarioAtual; }
            set { _SalarioAtual = value; }
        }
        private DateTime _DtUltFerias;
        public DateTime DtUltFerias
        {
            get { return _DtUltFerias; }
            set { _DtUltFerias = value; }
        }
        private string _Funcao;
        public string Funcao
        {
            get { return _Funcao; }
            set { _Funcao = value; }
        }
        private string _CBO;
        public string CBO
        {
            get { return _CBO; }
            set { _CBO = value; }
        }
        private string _ObsAdvertencia;
        public string ObsAdvertencia
        {
            get { return _ObsAdvertencia; }
            set { _ObsAdvertencia = value; }
        }
        private string _ObsAltSalario;
        public string ObsAltSalario
        {
            get { return _ObsAltSalario; }
            set { _ObsAltSalario = value; }
        }
        private string _ObsOutras;
        public string ObsOutras
        {
            get { return _ObsOutras; }
            set { _ObsOutras = value; }
        }
        private DateTime _DtDemissao;
        public DateTime DtDemissao
        {
            get { return _DtDemissao; }
            set { _DtDemissao = value; }
        }
        private int _Dependentes;
        public int Dependentes
        {
            get { return _Dependentes; }
            set { _Dependentes = value; }
        }
        private int _PlanoSaude;
        public int PlanoSaude
        {
            get { return _PlanoSaude; }
            set { _PlanoSaude = value; }
        }
        private int _ContratoExp;
        public int ContratoExp
        {
            get { return _ContratoExp; }
            set { _ContratoExp = value; }
        }
        private int _IdDepartamento;
        public int IdDepartamento
        {
            get { return _IdDepartamento; }
            set { _IdDepartamento = value; }
        }
        private int _IdFilialTrab;
        public int IdFilialTrab
        {
            get { return _IdFilialTrab; }
            set { _IdFilialTrab = value; }
        }
        private int _IdFilialReg;
        public int IdFilialReg
        {
            get { return _IdFilialReg; }
            set { _IdFilialReg = value; }
        }
        private int _Escolaridade;
        public int Escolaridade
        {
            get { return _Escolaridade; }
            set { _Escolaridade = value; }
        }
        private int _EstadoCivil;
        public int EstadoCivil
        {
            get { return _EstadoCivil; }
            set { _EstadoCivil = value; }
        }        
        private string _CNH;
        public string CNH
        {
            get { return _CNH; }
            set { _CNH = value; }
        }
        private string _PIS;
        public string PIS
        {
            get { return _PIS; }
            set { _PIS = value; }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private int _Demissao;
        public int Demissao
        {
            get { return _Demissao; }
            set { _Demissao = value; }
        }
        private string _MotivoDemissao;
        public string MotivoDemissao
        {
            get { return _MotivoDemissao; }
            set { _MotivoDemissao = value; }
        }
        private int _SalBaseHR;
        public int SalBaseHR
        {
            get { return _SalBaseHR; }
            set { _SalBaseHR = value; }
        }
        private decimal _AdiantSalario;
        public decimal AdiantSalario
        {
            get { return _AdiantSalario; }
            set { _AdiantSalario = value; }
        }

        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }

        private string _Foto;
        public string Foto
        {
            get { return _Foto; }
            set { _Foto = value; }
        }

        private string _Curso;
        public string Curso
        {
            get { return _Curso; }
            set { _Curso = value; }
        }

        private string _Celular2;
        public string Celular2
        {
            get { return _Celular2; }
            set { _Celular2 = value; }
        }
        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdFunc          = 0;
            Matricula       = 0;
            Nome            = "";
            Cep             = "";
            Endereco        = "";
            Numero          = "";
            Complemento     = "";
            Bairro          = "";
            Cidade          = "";
            IdUf            = 7;
            Telefone        = "";
            Celular         = "";
            DtNasc          = DateTime.Now;
            Rg              = "";
            DtEmissao       = DateTime.Now;
            NomePai         = "";
            NomeMae         = "";
            Cpf             = "";
            Ctps            = "";
            Serie           = "";
            TituloEleitoral = "";
            RefPessoal      = "";
            RefTelefone     = "";
            Parentesco      = "";
            TipoConta       = 0;
            Banco           = "";
            Agencia         = "";
            Conta           = "";
            DtAdmissao      = DateTime.Now;
            DtDemissao      = DateTime.Now;
            SalarioCtps     = 0;
            SalarioAtual    = 0;            
            Funcao          = "";
            CBO             = "";
            ObsAdvertencia  = "";
            ObsAltSalario   = "";
            ObsOutras       = "";
            Dependentes     = 0;
            PlanoSaude      = 0;
            ContratoExp     = 0;
            IdDepartamento  = 0;
            IdFilialTrab    = 0;
            IdFilialReg     = 0;
            Escolaridade    = 0;
            EstadoCivil     = 0;            
            CNH             = "";
            PIS             = "";
            Email           = "";
            IdVendedor      = 0;
            Demissao        = 0;
            MotivoDemissao  = "";
            AdiantSalario   = 0;
            SalBaseHR       = 0;
            IdPessoa        = 0;
            Foto            = "";
            Curso           = "";
            Celular2 = ""; 

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Funcionarios WHERE Id_Func=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdFunc          = Id;                    
                    Matricula       = int.Parse(Tabela["Matricula"].ToString().Trim());
                    Nome            = Tabela["Nome"].ToString().Trim();
                    Cep             = Tabela["CEP"].ToString().Trim();
                    Endereco        = Tabela["Endereco"].ToString().Trim();
                    Numero          = Tabela["Numero"].ToString().Trim();
                    Complemento     = Tabela["Complemento"].ToString().Trim();
                    Bairro          = Tabela["Bairro"].ToString().Trim();
                    Cidade          = Tabela["Cidade"].ToString().Trim();
                    IdUf            = int.Parse(Tabela["Id_Uf"].ToString().Trim());
                    Telefone        = Tabela["Telefone"].ToString().Trim();
                    Celular         = Tabela["Celular"].ToString().Trim();
                    Celular2        = Tabela["Celular2"].ToString().Trim();
                    DtNasc          = DateTime.Parse(Tabela["DtNascim"].ToString().Trim());
                    Rg              = Tabela["Rg"].ToString().Trim();
                    DtEmissao       = DateTime.Parse(Tabela["DtEmissao"].ToString().Trim());
                    NomePai         = Tabela["NomePai"].ToString().Trim();
                    NomeMae         = Tabela["NomeMae"].ToString().Trim();
                    Cpf             = Tabela["Cpf"].ToString().Trim();
                    Ctps            = Tabela["Ctps"].ToString().Trim();
                    Serie           = Tabela["Serie"].ToString().Trim();
                    TituloEleitoral = Tabela["TituloEleitoral"].ToString().Trim();
                    RefPessoal      = Tabela["RefPessoal"].ToString().Trim();
                    RefTelefone     = Tabela["RefTelefone"].ToString().Trim();
                    Parentesco      = Tabela["Parentesco"].ToString().Trim();
                    TipoConta       = int.Parse(Tabela["TipoConta"].ToString().Trim());
                    Banco           = Tabela["Banco"].ToString().Trim();
                    Agencia         = Tabela["Agencia"].ToString().Trim();
                    Conta           = Tabela["Conta"].ToString().Trim();
                    DtAdmissao      = DateTime.Parse(Tabela["DtAdmissao"].ToString().Trim());
                    SalarioCtps     = decimal.Parse(Tabela["SalarioCtps"].ToString().Trim());
                    SalarioAtual    = decimal.Parse(Tabela["SalarioAtual"].ToString().Trim());                    
                    Funcao          = Tabela["Funcao"].ToString().Trim();
                    CBO             = Tabela["CBO"].ToString().Trim();
                    ObsAdvertencia  = Tabela["ObsAdvertencia"].ToString().Trim();
                    ObsAltSalario   = Tabela["ObsAltSalario"].ToString().Trim();
                    ObsOutras       = Tabela["ObsOutras"].ToString().Trim();
                    Dependentes     = int.Parse(Tabela["Dependentes"].ToString().Trim());
                    PlanoSaude      = int.Parse(Tabela["PlanoSaude"].ToString().Trim());
                    ContratoExp     = int.Parse(Tabela["ContratoExp"].ToString().Trim());
                    IdDepartamento  = int.Parse(Tabela["ID_Departamento"].ToString().Trim());
                    IdFilialTrab    = int.Parse(Tabela["ID_FilialTrab"].ToString().Trim());
                    IdFilialReg     = int.Parse(Tabela["ID_FilialReg"].ToString().Trim());
                    Escolaridade    = int.Parse(Tabela["Escolaridade"].ToString().Trim());
                    EstadoCivil     = int.Parse(Tabela["EstadoCivil"].ToString().Trim());
                    IdVendedor      = int.Parse(Tabela["Id_Vendedor"].ToString().Trim());
                    Curso           = Tabela["Curso"].ToString().Trim();
                    CNH             = Tabela["CNH"].ToString().Trim();
                    PIS             = Tabela["PIS"].ToString().Trim();
                    Email           = Tabela["Email"].ToString().Trim();
                    Foto            = Tabela["Foto"].ToString().Trim();
                    MotivoDemissao  = Tabela["MotivoDemissao"].ToString().Trim();
                    if (Tabela["DtUltFerias"].ToString() != "")
                        DtUltFerias = DateTime.Parse(Tabela["DtUltFerias"].ToString().Trim());
                    if (Tabela["Demissao"].ToString() != "")
                        Demissao = int.Parse(Tabela["Demissao"].ToString().Trim());
                    if (Tabela["DtDemissao"].ToString() != "")
                        DtDemissao = DateTime.Parse(Tabela["DtDemissao"].ToString().Trim());
                    if (Tabela["AdiantSalario"].ToString() != "")
                        AdiantSalario = decimal.Parse(Tabela["AdiantSalario"].ToString().Trim());
                    if (Tabela["SalBaseHR"].ToString() != "")
                        SalBaseHR = int.Parse(Tabela["SalBaseHR"].ToString().Trim());
                    if (Tabela["ID_Pessoa"].ToString() != "")
                        IdPessoa = int.Parse(Tabela["ID_PESSOA"].ToString().Trim());
                    


                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdFunc > 0)
            {
                sSQL = "UPDATE Funcionarios SET Id_Func=@Id,Matricula=@Matricula,Nome=@Nome,Cep=@Cep,Endereco=@Endereco,Numero=@Numero,Complemento=@Complemento,Bairro=@Bairro,Cidade=@Cidade,Id_UF=@IdUf,Telefone=@Telefone,Celular=@Celular,DtNascim=Convert(DateTime,@DtNascim,103),Rg=@Rg," +
                       "DtEmissao=Convert(DateTime,@DtEmissao,103),NomePai=@NomePai,NomeMae=@NomeMae,Cpf=@Cpf,Ctps=@Ctps,Serie=@Serie,TituloEleitoral=@TituloEleitoral,RefPessoal=@RefPessoal,RefTelefone=@RefTelefone,Parentesco=@Parentesco,TipoConta=@TipoConta,Banco=@Banco,Agencia=@Agencia,Conta=@Conta,"+
                       "DtAdmissao=Convert(DateTime,@DtAdmissao,103),SalarioCtps=@SalarioCtps,SalarioAtual=@SalarioAtual,Funcao=@Funcao,Cbo=@Cbo,ObsAdvertencia=@ObsAdvertencia,ObsAltSalario=@ObsAltSalario,ObsOutras=@ObsOutras,Dependentes=@Dependentes,PlanoSaude=@PlanoSaude,ContratoExp=@ContratoExp," +
                       "Id_Departamento=@IdDepartamento,Id_FilialTrab=@IdFilialTrab,Id_FilialReg=@IdFilialReg,Escolaridade=@Escolaridade,EstadoCivil=@EstadoCivil,CNH=@CNH,PIS=@PIS,Email=@Email,Id_Vendedor=@IdVendedor,Demissao=@Demissao,MotivoDemissao=@MotivoDemissao,AdiantSalario=@AdiantSalario,SalBaseHR=@SalBaseHR,"+
                       "ID_Pessoa=@IDPessoa,Celular2=@Celular2,Curso=@Curso Where Id_Func=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdFunc);
            }
            else
            {
                IdFunc = Controle.ProximoID("Funcionarios");
                sSQL = "INSERT INTO Funcionarios (Id_Func,Matricula,Nome,Cep,Endereco,Numero,Complemento,Bairro,Cidade,Id_UF,Telefone,Celular,DtNascim,Rg,DtEmissao,NomePai,NomeMae,Cpf,Ctps,Serie,TituloEleitoral,RefPessoal,RefTelefone,Parentesco,TipoConta,Banco,Agencia,Conta,DtAdmissao,SalarioCtps," +
                       "SalarioAtual,Funcao,Cbo,ObsAdvertencia,ObsAltSalario,ObsOutras,Dependentes,PlanoSaude,ContratoExp,Id_Departamento,Id_FilialTrab,Id_FilialReg,Escolaridade,EstadoCivil,CNH,PIS,Email,Id_Vendedor,Demissao,MotivoDemissao,AdiantSalario,SalBaseHR,ID_Pessoa,Celular2,Curso)" +
                       " VALUES (@Id,@Matricula,@Nome,@Cep,@Endereco,@Numero,@Complemento,@Bairro,@Cidade,@IdUf,@Telefone,@Celular,Convert(DateTime,@DtNascim,103),@Rg,Convert(DateTime,@DtEmissao,103),@NomePai,@NomeMae,@Cpf,@Ctps,@Serie,@TituloEleitoral,@RefPessoal,@RefTelefone,@Parentesco," +
                       "@TipoConta,@Banco,@Agencia,@Conta,Convert(DateTime,@DtAdmissao,103),@SalarioCtps,@SalarioAtual,@Funcao,@Cbo,@ObsAdvertencia,@ObsAltSalario,@ObsOutras,@Dependentes,@PlanoSaude,@ContratoExp,@IdDepartamento,@IdFilialTrab,@IdFilialReg,@Escolaridade,@EstadoCivil,@CNH,@PIS,"+
                       "@Email,@IdVendedor,@Demissao,@MotivoDemissao,@AdiantSalario,@SalBaseHR,@IDPessoa,@Celular2,@Curso)";
            }

            if (sSQL != "")
            {              
                Nm_param.Add("@Id");              Vr_param.Add(IdFunc);
                Nm_param.Add("@Matricula");       Vr_param.Add(Matricula);
                Nm_param.Add("@Nome");            Vr_param.Add(Nome);
                Nm_param.Add("@Cep");             Vr_param.Add(Cep);
                Nm_param.Add("@Endereco");        Vr_param.Add(Endereco);
                Nm_param.Add("@Numero");          Vr_param.Add(Numero);
                Nm_param.Add("@Complemento");     Vr_param.Add(Complemento);
                Nm_param.Add("@Bairro");          Vr_param.Add(Bairro);
                Nm_param.Add("@Cidade");          Vr_param.Add(Cidade);
                Nm_param.Add("@IdUf");            Vr_param.Add(IdUf);
                Nm_param.Add("@Telefone");        Vr_param.Add(Telefone);
                Nm_param.Add("@Celular");         Vr_param.Add(Celular);
                Nm_param.Add("@DtNascim");        Vr_param.Add(DtNasc.ToShortDateString());
                Nm_param.Add("@Rg");              Vr_param.Add(Rg);
                Nm_param.Add("@DtEmissao");       Vr_param.Add(DtEmissao.ToShortDateString());
                Nm_param.Add("@NomePai");         Vr_param.Add(NomePai);
                Nm_param.Add("@NomeMae");         Vr_param.Add(NomeMae);
                Nm_param.Add("@CPF");             Vr_param.Add(Cpf);
                Nm_param.Add("@Ctps");            Vr_param.Add(Ctps);
                Nm_param.Add("@Serie");           Vr_param.Add(Serie);
                Nm_param.Add("@TituloEleitoral"); Vr_param.Add(TituloEleitoral);
                Nm_param.Add("@RefPessoal");      Vr_param.Add(RefPessoal);
                Nm_param.Add("@RefTelefone");     Vr_param.Add(RefTelefone);
                Nm_param.Add("@Parentesco");      Vr_param.Add(Parentesco);
                Nm_param.Add("@TipoConta");       Vr_param.Add(TipoConta);
                Nm_param.Add("@Banco");           Vr_param.Add(Banco);
                Nm_param.Add("@Agencia");         Vr_param.Add(Agencia);
                Nm_param.Add("@Conta");           Vr_param.Add(Conta);
                Nm_param.Add("@DtAdmissao");      Vr_param.Add(DtAdmissao.ToShortDateString());
                Nm_param.Add("@SalarioCtps");     Vr_param.Add(Controle.FloatToStr(SalarioCtps, 2));
                Nm_param.Add("@SalarioAtual");    Vr_param.Add(Controle.FloatToStr(SalarioAtual, 2));                
                Nm_param.Add("@Funcao");          Vr_param.Add(Funcao);
                Nm_param.Add("@Cbo");             Vr_param.Add(CBO);
                Nm_param.Add("@ObsAdvertencia");  Vr_param.Add(ObsAdvertencia);
                Nm_param.Add("@ObsAltSalario");   Vr_param.Add(ObsAltSalario);
                Nm_param.Add("@ObsOutras");       Vr_param.Add(ObsOutras);
                Nm_param.Add("@Dependentes");     Vr_param.Add(Dependentes);
                Nm_param.Add("@PlanoSaude");      Vr_param.Add(PlanoSaude);
                Nm_param.Add("@ContratoExp");     Vr_param.Add(ContratoExp);
                Nm_param.Add("@IDDepartamento");  Vr_param.Add(IdDepartamento);
                Nm_param.Add("@IDFilialTrab");    Vr_param.Add(IdFilialTrab);
                Nm_param.Add("@IDFilialReg");     Vr_param.Add(IdFilialReg);
                Nm_param.Add("@Escolaridade");    Vr_param.Add(Escolaridade);
                Nm_param.Add("@EstadoCivil");     Vr_param.Add(EstadoCivil);                
                Nm_param.Add("@CNH");             Vr_param.Add(CNH);
                Nm_param.Add("@PIS");             Vr_param.Add(PIS);
                Nm_param.Add("@Email");           Vr_param.Add(Email);
                Nm_param.Add("@IdVendedor");      Vr_param.Add(IdVendedor);
                Nm_param.Add("@Demissao");        Vr_param.Add(Demissao);
                Nm_param.Add("@MotivoDemissao");  Vr_param.Add(MotivoDemissao);
                Nm_param.Add("@AdiantSalario");   Vr_param.Add(AdiantSalario);
                Nm_param.Add("@SalBaseHR");       Vr_param.Add(SalBaseHR);
                Nm_param.Add("@IDPessoa");        Vr_param.Add(IdPessoa);
                Nm_param.Add("@Celular2");        Vr_param.Add(Celular2);
                Nm_param.Add("@Curso");           Vr_param.Add(Curso);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
                GravaDemissao();
            }
        }
        public void Excluir()
        {
            if (IdFunc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Funcionarios WHERE Id_Func=" + IdFunc.ToString().Trim());
            }
        }

        public void GravaDemissao()
        {
            if (Demissao == 1)
                Controle.ExecutaSQL("Update Funcionarios set Demissao=1,DtDemissao=Convert(DateTime,'" + DtDemissao.ToShortDateString() + "',103),MotivoDemissao='" + MotivoDemissao.Trim() + "' WHERE ID_FUNC=" + IdFunc.ToString());
            else
                Controle.ExecutaSQL("Update Funcionarios set Demissao=0,DtDemissao=Null,MotivoDemissao='' WHERE ID_FUNC=" + IdFunc.ToString());
        }
    
    }
}

