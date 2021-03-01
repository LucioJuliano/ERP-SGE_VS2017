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
    public class AIDFs
    {
        private int _IdLanc;

        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private DateTime _DtAutorizacao;
        public DateTime DtAutorizacao
        {
            get { return _DtAutorizacao; }
            set { _DtAutorizacao = value; }
        }
        private string _NumAutorizacao;
        public string NumAutorizacao
        {
            get { return _NumAutorizacao; }
            set { _NumAutorizacao = value; }
        }
        private int _NumInicial;
        public int NumInicial
        {
            get { return _NumInicial; }
            set { _NumInicial = value; }
        }
        private int _NumFinal;
        public int NumFinal
        {
            get { return _NumFinal; }
            set { _NumFinal = value; }
        }
        private DateTime _Validade;
        public DateTime Validade
        {
            get { return _Validade; }
            set { _Validade = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc         = 0;
            DtAutorizacao  = DateTime.Now;
            NumAutorizacao = "";
            NumInicial     = 0;
            NumFinal       = 0;
            Validade       = DateTime.Now;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM AIDF WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    DtAutorizacao = DateTime.Parse(Tabela["DtAutorizacao"].ToString());
                    NumAutorizacao = Tabela["NumAutorizacao"].ToString().Trim();
                    NumInicial = int.Parse(Tabela["NumInicial"].ToString());
                    NumFinal = int.Parse(Tabela["NumFinal"].ToString());
                    Validade = DateTime.Parse(Tabela["Validade"].ToString());                    
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
                sSQL = "UPDATE AIDF SET Id_Lanc=@Id,DtAutorizacao=Convert(DateTime,@DtAutorizacao,103),NumAutorizacao=@NumAutorizacao,NumInicial=@NumInicial,NumFinal=@NumFinal,Validade=Convert(DateTime,@Validade,103) Where Id_lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("AIDF");
                sSQL = "INSERT INTO AIDF (ID_Lanc,DtAutorizacao,NumAutorizacao,NumInicial,NumFinal,Validade) VALUES(@Id,Convert(DateTime,@DtAutorizacao,103),@NumAutorizacao,@NumInicial,@NumFinal,Convert(DateTime,@Validade,103))";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);
                Nm_param.Add("@DtAutorizacao"); Vr_param.Add(DtAutorizacao.Date.ToShortDateString());
                Nm_param.Add("@NumAutorizacao"); Vr_param.Add(NumAutorizacao);
                Nm_param.Add("@NumInicial"); Vr_param.Add(NumInicial);
                Nm_param.Add("@NumFinal"); Vr_param.Add(NumFinal);
                Nm_param.Add("@Validade"); Vr_param.Add(Validade.Date.ToShortDateString());                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM AIDF WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }

    }
}
