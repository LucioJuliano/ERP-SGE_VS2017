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
    [Serializable()]
    public class Filiais
    {
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private string _Filial;
        public string Filial
        {
            get { return _Filial; }
            set { _Filial = value; }
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
        private int _Uf;
        public int Uf
        {
            get { return _Uf; }
            set { _Uf = value; }
        }
        private string _Fone1;
        public string Fone1
        {
            get { return _Fone1; }
            set { _Fone1 = value; }
        }
        private string _Fone2;
        public string Fone2
        {
            get { return _Fone2; }
            set { _Fone2 = value; }
        }
        private string _Fax;
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Convenio;
        public string Convenio
        {
            get { return _Convenio; }
            set { _Convenio = value; }
        }        
        private decimal _Juros;
        public decimal Juros
        {
            get { return _Juros; }
            set { _Juros = value; }
        }
        private decimal _Multa;
        public decimal Multa
        {
            get { return _Multa; }
            set { _Multa = value; }
        }
        private string _Instrucao;
        public string Instrucao
        {
            get { return _Instrucao; }
            set { _Instrucao = value; }
        }
        private int _IdBanco;
        public int IdBanco
        {
            get { return _IdBanco; }
            set { _IdBanco = value; }
        }
        private int _Regime;
        public int Regime
        {
            get { return _Regime; }
            set { _Regime = value; }
        }
        private string _ServidorRemoto;
        public string ServidorRemoto
        {
            get { return _ServidorRemoto; }
            set { _ServidorRemoto = value; }
        }
        
        private string _Porta;
        public string Porta
        {
            get { return _Porta; }
            set { _Porta = value; }
        }

        private int _CodMun;
        public int CodMun
        {
            get { return _CodMun; }
            set { _CodMun = value; }
        }
        private string _Logo;
        public string Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }



        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdFilial       = 0;
            Filial         = "";
            Fantasia       = "";
            Cnpj           = "";
            InscUF         = "";
            Cep            = "";
            Endereco       = "";
            Numero         = "";
            Complemento    = "";
            Bairro         = "";
            Cidade         = "";
            Uf             = 0;
            Fone1          = "";
            Fone2          = "";
            Fax            = "";
            Email          = "";
            Convenio       = "";
            Juros          = 0;
            Multa          = 0;
            Instrucao      = "";
            IdBanco        = 0; 
            Regime         = 0;
            ServidorRemoto = "";
            Porta          = "";
            CodMun         = 0;
            Logo           = "";


            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Empresa_Filial WHERE Id_Filial=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdFilial       = Id;
                    Filial         = Tabela["Filial"].ToString().Trim();
                    Fantasia       = Tabela["Fantasia"].ToString().Trim();
                    Cnpj           = Tabela["Cnpj"].ToString().Trim();
                    InscUF         = Tabela["Insc_UF"].ToString().Trim();
                    Cep            = Tabela["CEP"].ToString().Trim();
                    Endereco       = Tabela["Endereco"].ToString().Trim();
                    Numero         = Tabela["Numero"].ToString().Trim();
                    Complemento    = Tabela["Complemento"].ToString().Trim();
                    Bairro         = Tabela["Bairro"].ToString().Trim();
                    Cidade         = Tabela["Cidade"].ToString().Trim();
                    Uf             = int.Parse(Tabela["ID_UF"].ToString());
                    Fone1          = Tabela["Fone1"].ToString().Trim();
                    Fone2          = Tabela["Fone2"].ToString().Trim();
                    Fax            = Tabela["Fax"].ToString().Trim();
                    Email          = Tabela["Email"].ToString().Trim();
                    Convenio       = Tabela["Convenio"].ToString().Trim();                    
                    Juros          = decimal.Parse(Tabela["Juros"].ToString());
                    Multa          = decimal.Parse(Tabela["Multa"].ToString());
                    Instrucao      = Tabela["Instrucao"].ToString().Trim();
                    IdBanco        = int.Parse(Tabela["ID_Banco"].ToString());
                    Regime         = int.Parse(Tabela["Regime"].ToString());
                    ServidorRemoto = Tabela["ServidorRemoto"].ToString().Trim();
                    Porta          = Tabela["Porta"].ToString().Trim();
                    Logo           = Tabela["Logo"].ToString().Trim();

                    if (Tabela["CodMun"].ToString() != "")
                        CodMun = int.Parse(Tabela["CodMun"].ToString());
                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdFilial > 0)
            {
                sSQL = "UPDATE EMPRESA_FILIAL SET Id_Filial=@Id,Filial=@Filial,Fantasia=@Fantasia,Cnpj=@Cnpj,Insc_Uf=@InscUF,Endereco=@Endereco,"+
                       "Numero=@Numero,Complemento=@Complemento,Cep=@Cep,Bairro=@Bairro,Cidade=@Cidade,Id_UF=@UF,Fone1=@Fone1,Fone2=@Fone2,Fax=@Fax,Email=@Email,"+
                       "Convenio=@Convenio,Juros=@Juros,Multa=@Multa,Instrucao=@Instrucao,Id_Banco=@IdBanco,Regime=@Regime,CodMun=@CodMun Where Id_Filial=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdFilial);
            }
            else
            {
                IdFilial = Controle.ProximoID("Empresas");
                sSQL = "INSERT INTO EMPRESA_FILIAL (Id_Filial,Filial,Fantasia,Cnpj,Insc_Uf,Endereco,Numero,Complemento,Cep,Bairro,Cidade,Id_UF,Fone1,Fone2,Fax,Email,Convenio,Juros,Multa,Instrucao,Id_Banco,Regime,CodMun) " +
                       "Values (@Id,@Filial,@Fantasia,@Cnpj,@InscUF,@Endereco,@Numero,@Complemento,@Cep,@Bairro,@Cidade,@UF,@Fone1,@Fone2,@Fax,@Email,@Convenio,@Juros,@Multa,@Instrucao,@IdBanco,@Regime,@CodMun)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");          Vr_param.Add(IdFilial);
                Nm_param.Add("@Filial");      Vr_param.Add(Filial);
                Nm_param.Add("@Fantasia");    Vr_param.Add(Fantasia);
                Nm_param.Add("@Cnpj");        Vr_param.Add(Cnpj);
                Nm_param.Add("@InscUF");      Vr_param.Add(InscUF);
                Nm_param.Add("@Cep");         Vr_param.Add(Cep);
                Nm_param.Add("@Endereco");    Vr_param.Add(Endereco);
                Nm_param.Add("@Numero");      Vr_param.Add(Numero);
                Nm_param.Add("@Complemento"); Vr_param.Add(Complemento);
                Nm_param.Add("@Bairro");      Vr_param.Add(Bairro);
                Nm_param.Add("@Cidade");      Vr_param.Add(Cidade);
                Nm_param.Add("@UF");          Vr_param.Add(Uf);
                Nm_param.Add("@Fone1");       Vr_param.Add(Fone1);
                Nm_param.Add("@Fone2");       Vr_param.Add(Fone2);
                Nm_param.Add("@Fax");         Vr_param.Add(Fax);
                Nm_param.Add("@Email");       Vr_param.Add(Email);
                Nm_param.Add("@Convenio");    Vr_param.Add(Convenio);                
                Nm_param.Add("@Juros");       Vr_param.Add(Controle.FloatToStr(Juros));
                Nm_param.Add("@Multa");       Vr_param.Add(Controle.FloatToStr(Multa));
                Nm_param.Add("@Instrucao");   Vr_param.Add(Instrucao);
                Nm_param.Add("@IdBanco");     Vr_param.Add(IdBanco);
                Nm_param.Add("@Regime");      Vr_param.Add(Regime);
                Nm_param.Add("@CodMun");      Vr_param.Add(CodMun);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdFilial > 0)
            {
                Controle.ExecutaSQL("DELETE FROM EMPRESA_FILIAL WHERE Id_Filial=" + IdFilial.ToString().Trim());
            }
        }
    }
}
