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
    public class Bancos
    {
        private int _IdBanco;
        public int IdBanco
        {
            get { return _IdBanco; }
            set { _IdBanco = value; }
        }
        private string _Banco;
        public string Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
        }
        private int _NumBanco;
        public int NumBanco
        {
            get { return _NumBanco; }
            set { _NumBanco = value; }
        }
        private string _Agencia;
        public string Agencia
        {
            get { return _Agencia; }
            set { _Agencia = value; }
        }
        private string _NumAgencia;
        public string NumAgencia
        {
            get { return _NumAgencia; }
            set { _NumAgencia = value; }
        }
        private string _Conta;
        public string Conta
        {
            get { return _Conta; }
            set { _Conta = value; }
        }
        private int _DigConta;
        public int DigConta
        {
            get { return _DigConta; }
            set { _DigConta = value; }
        }
        private string _Fone;
        public string Fone
        {
            get { return _Fone; }
            set { _Fone = value; }
        }
        private string _Gerente;
        public string Gerente
        {
            get { return _Gerente; }
            set { _Gerente = value; }
        }
        

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM BANCOS WHERE Id_BANCO=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdBanco    = Id;
                    Banco      = Tabela["Banco"].ToString().Trim();
                    NumBanco   = int.Parse(Tabela["NumBanco"].ToString());
                    Agencia    = Tabela["Agencia"].ToString().Trim();
                    NumAgencia = Tabela["NumAgencia"].ToString().Trim();
                    Conta      = Tabela["Conta"].ToString().Trim();
                    DigConta   = int.Parse(Tabela["DigConta"].ToString());
                    Fone       = Tabela["Fone"].ToString().Trim();
                    Gerente    = Tabela["Gerente"].ToString().Trim();                    
                    
                }
            }
            else
            {
                IdBanco = 0;
                Banco = "";
                NumBanco = 0;
                Agencia = "";
                NumAgencia = "";
                Conta = "";
                DigConta = 0;
                Fone = "";
                Gerente = "";
                
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdBanco > 0)
            {
                sSQL = "UPDATE BANCOS SET Id_BANCO=@Id,BANCO=@BANCO,NUMBANCO=@NUMBANCO,AGENCIA=@AGENCIA,NUMAGENCIA=@NUMAGENCIA,CONTA=@CONTA,FONE=@FONE,GERENTE=@GERENTE,DIGCONTA=@DIGCONTA "+
                       "Where Id_BANCO=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdBanco);
            }
            else
            {
                IdBanco = Controle.ProximoID("BANCOS");
                sSQL = "INSERT INTO BANCOS (Id_BANCO,BANCO,NUMBANCO,AGENCIA,NUMAGENCIA,CONTA,FONE,GERENTE,DIGCONTA) Values(@Id,@BANCO,@NUMBANCO,@AGENCIA,@NUMAGENCIA,@CONTA,@FONE,@GERENTE,@DIGCONTA)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdBanco);
                Nm_param.Add("@BANCO"); Vr_param.Add(Banco);
                Nm_param.Add("@NUMBANCO"); Vr_param.Add(NumBanco);
                Nm_param.Add("@AGENCIA"); Vr_param.Add(Agencia);
                Nm_param.Add("@NUMAGENCIA"); Vr_param.Add(NumAgencia);
                Nm_param.Add("@CONTA"); Vr_param.Add(Conta);
                Nm_param.Add("@FONE"); Vr_param.Add(Fone);
                Nm_param.Add("@GERENTE"); Vr_param.Add(Gerente);
                Nm_param.Add("@DIGCONTA"); Vr_param.Add(DigConta);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdBanco > 0)
            {
                Controle.ExecutaSQL("DELETE FROM BANCOS WHERE Id_Banco=" + IdBanco.ToString().Trim());
            }
        }
    }
}
