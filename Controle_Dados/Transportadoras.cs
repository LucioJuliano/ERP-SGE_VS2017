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
    public class Transportadoras
    {
        private int _IdTransportadora;
        public int IdTransportadora
        {
            get { return _IdTransportadora; }
            set { _IdTransportadora = value; }
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


        public Funcoes Controle;
        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Transportadoras WHERE Id_Transportadora=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdTransportadora = Id;
                    RazaoSocial = Tabela["RazaoSocial"].ToString().Trim();
                    Fantasia = Tabela["Fantasia"].ToString().Trim();
                    Cnpj = Tabela["Cnpj"].ToString().Trim();
                    InscUF = Tabela["Insc_UF"].ToString().Trim();
                    Cep = Tabela["CEP"].ToString().Trim();
                    Endereco = Tabela["Endereco"].ToString().Trim();
                    Numero = Tabela["Numero"].ToString().Trim();
                    Complemento = Tabela["Complemento"].ToString().Trim();
                    Bairro = Tabela["Bairro"].ToString().Trim();
                    Cidade = Tabela["Cidade"].ToString().Trim();
                    IdUF = int.Parse(Tabela["ID_UF"].ToString());
                    Fone = Tabela["Fone"].ToString().Trim();                    
                    Fax = Tabela["Fax"].ToString().Trim();
                    Email = Tabela["Email"].ToString().Trim();
                    Contato = Tabela["Contato"].ToString().Trim();
                    Celular = Tabela["Celular"].ToString().Trim();
                }
            }
            else
            {
                IdTransportadora = 0;
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
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdTransportadora > 0)
            {
                sSQL = "UPDATE Transportadoras SET Id_Transportadora=@Id,RazaoSocial=@RazaoSocial,Fantasia=@Fantasia,Cnpj=@Cnpj,Insc_Uf=@InscUF,Endereco=@Endereco," +
                       "Numero=@Numero,Complemento=@Complemento,Cep=@Cep,Bairro=@Bairro,Cidade=@Cidade,Id_UF=@UF,Fone=@Fone,Fax=@Fax,Email=@Email,Contato=@Contato,Celular=@Celular Where Id_Transportadora=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdTransportadora);
            }
            else
            {
                IdTransportadora = Controle.ProximoID("Transportadoras");
                sSQL = "INSERT INTO Transportadoras (Id_Transportadora,RazaoSocial,Fantasia,Cnpj,Insc_Uf,Endereco,Numero,Complemento,Cep,Bairro,Cidade,Id_UF,Fone,Fax,Email,Contato,Celular) " +
                       "Values (@Id,@RazaoSocial,@Fantasia,@Cnpj,@InscUF,@Endereco,@Numero,@Complemento,@Cep,@Bairro,@Cidade,@UF,@Fone,@Fax,@Email,@Contato,@Celular)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdTransportadora);
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
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdTransportadora > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Transportadoras WHERE Id_Transportadora=" + IdTransportadora.ToString().Trim());
            }
        }
    }
}
