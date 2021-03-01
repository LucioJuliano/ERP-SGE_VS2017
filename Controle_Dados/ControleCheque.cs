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
    public class ControleCheque
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private DateTime _DataLanc;
        public DateTime DataLanc
        {
            get { return _DataLanc; }
            set { _DataLanc = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private string _Titular;
        public string Titular
        {
            get { return _Titular; }
            set { _Titular = value; }
        }
        private int _NumBanco;
        public int NumBanco
        {
            get { return _NumBanco; }
            set { _NumBanco = value; }
        }
        private string _NumAgencia;
        public string NumAgencia
        {
            get { return _NumAgencia; }
            set { _NumAgencia = value; }
        }
        private string _NumConta;
        public string NumConta
        {
            get { return _NumConta; }
            set { _NumConta = value; }
        }
        private string _NumCheque;
        public string NumCheque
        {
            get { return _NumCheque; }
            set { _NumCheque = value; }
        }
        private string _DocumVenda;
        public string DocumVenda
        {
            get { return _DocumVenda; }
            set { _DocumVenda = value; }
        }
        private DateTime _DtVencimento;
        public DateTime DtVencimento
        {
            get { return _DtVencimento; }
            set { _DtVencimento = value; }
        }
        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _IdPessoaDest;
        public int IdPessoaDest
        {
            get { return _IdPessoaDest; }
            set { _IdPessoaDest = value; }
        }
        private DateTime _DtDestino;
        public DateTime DtDestino
        {
            get { return _DtDestino; }
            set { _DtDestino = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private string _CnpjCpf;
        public string CnpjCpf
        {
            get { return _CnpjCpf; }
            set { _CnpjCpf = value; }
        }
        

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc = 0;
            DataLanc = DateTime.Now;
            IdPessoa = 0;
            Titular = "";
            NumBanco = 0;
            NumAgencia = "";
            NumConta = "";
            NumCheque = "";
            DtVencimento = DateTime.Now;
            Valor = 0;
            Observacao = "";
            IdPessoaDest = 0;
            Status = 0;
            DocumVenda = "";
            CnpjCpf = "";
            IdFilial = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MOVCHEQUEPRE WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    DataLanc     = DateTime.Parse(Tabela["DataLanc"].ToString());
                    IdPessoa     = int.Parse(Tabela["Id_Pessoa"].ToString());
                    Titular      = Tabela["Titular"].ToString().Trim();
                    NumBanco     = int.Parse(Tabela["NumBanco"].ToString());
                    NumAgencia   = Tabela["NumAgencia"].ToString().Trim();
                    NumConta     = Tabela["NumConta"].ToString().Trim();
                    NumCheque    = Tabela["NumCheque"].ToString().Trim();
                    DtVencimento = DateTime.Parse(Tabela["DtVencimento"].ToString());
                    Valor        = decimal.Parse(Tabela["Valor"].ToString());
                    Observacao   = Tabela["Observacao"].ToString().Trim();
                    IdPessoaDest = int.Parse(Tabela["Id_PessoaDest"].ToString());
                    Status       = int.Parse(Tabela["Status"].ToString());
                    DocumVenda   = Tabela["DocumVenda"].ToString().Trim();
                    IdFilial     = int.Parse(Tabela["Id_Filial"].ToString());
                    CnpjCpf      = Tabela["CnpjCpf"].ToString().Trim();
                    if (IdPessoaDest > 0)
                        DtDestino = DateTime.Parse(Tabela["DtDestino"].ToString());
                }
            }         
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdLanc > 0)
            {
                sSQL = "UPDATE MOVCHEQUEPRE SET Id_Lanc=@Id,DataLanc=Convert(DateTime,@DataLanc,103),Id_Pessoa=@IdPessoa,Titular=@Titular,NumBanco=@NumBanco,NumAgencia=@NumAgencia,DocumVenda=@DocumVenda," +
                       "NumConta=@NumConta,NumCheque=@NumCheque,DtVencimento=Convert(DateTime,@DtVencimento,103),Valor=@Valor,Observacao=@Observacao,Status=@Status,Id_PessoaDest=@IdPessoaDest,Id_Filial=@IdFilial,CnpjCPF=@CnpjCpf Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("MVCHEQUE");
                sSQL = "INSERT INTO MOVCHEQUEPRE (Id_Lanc,DataLanc,Id_Pessoa,Titular,NumBanco,NumAgencia,NumConta,NumCheque,DtVencimento,Valor,Observacao,Status,Id_PessoaDest,DocumVenda,Id_Filial,CnpjCpf) " +
                       "VALUES (@Id,Convert(DateTime,@DataLanc,103),@IdPessoa,@Titular,@NumBanco,@NumAgencia,@NumConta,@NumCheque,Convert(DateTime,@DtVencimento,103),@Valor,@Observacao,@Status,@IdPessoaDest,@DocumVenda,@IdFilial,@CnpjCpf)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);
                Nm_param.Add("@DataLanc"); Vr_param.Add(DataLanc.ToShortDateString());
                Nm_param.Add("@IdPessoa"); Vr_param.Add(IdPessoa);
                Nm_param.Add("@Titular"); Vr_param.Add(Titular);
                Nm_param.Add("@NumBanco"); Vr_param.Add(NumBanco);
                Nm_param.Add("@NumAgencia"); Vr_param.Add(NumAgencia);
                Nm_param.Add("@NumConta"); Vr_param.Add(NumConta);
                Nm_param.Add("@NumCheque"); Vr_param.Add(NumCheque);
                Nm_param.Add("@DtVencimento"); Vr_param.Add(DtVencimento.ToShortDateString());
                Nm_param.Add("@Valor"); Vr_param.Add(Controle.FloatToStr(Valor,2));
                Nm_param.Add("@Observacao"); Vr_param.Add(Observacao);
                Nm_param.Add("@Status"); Vr_param.Add(Status);
                Nm_param.Add("@IdPessoaDest"); Vr_param.Add(IdPessoaDest);
                Nm_param.Add("@DocumVenda"); Vr_param.Add(DocumVenda);
                Nm_param.Add("@IdFilial"); Vr_param.Add(IdFilial);
                Nm_param.Add("@CnpjCpf"); Vr_param.Add(CnpjCpf);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM MOVCHEQUEPRE WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }
    }
}
