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
    public class RegCobranca
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdPagRec;
        public int IdPagRec
        {
            get { return _IdPagRec; }
            set { _IdPagRec = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private string _Informacao;
        public string Informacao
        {
            get { return _Informacao; }
            set { _Informacao = value; }
        }
        

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc     = 0;
            IdPagRec   = 0;
            Data       = DateTime.Now;
            IdPessoa   = 0;
            Informacao = "";
            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM RegCobranca WHERE ID_Lanc=" + Id.ToString().Trim());

                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc     = Id;
                    IdPagRec   = int.Parse(Tabela["Id_PagRec"].ToString());
                    Data       = DateTime.Parse(Tabela["Data"].ToString());
                    IdPessoa   = int.Parse(Tabela["Id_Pessoa"].ToString());
                    Informacao = Tabela["Informacao"].ToString().Trim();                    
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
                sSQL = "UPDATE RegCobranca SET Id_Lanc=@Id,Id_PagRec=@IdPagRec,DATA=Convert(DateTime,@Data,103),Id_Pessoa=@IdPessoa,Informacao=@Informacao Where Id_LANC=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("REGCOBRANCA");
                sSQL = "INSERT INTO REGCOBRANCA (Id_LANC,ID_PAGREC,DATA,ID_PESSOA,INFORMACAO) Values(@Id,@IdPagRec,Convert(DateTime,@Data,103),@IdPessoa,@Informacao)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");         Vr_param.Add(IdLanc);
                Nm_param.Add("@IdPagRec");   Vr_param.Add(IdPagRec);
                Nm_param.Add("@DATA");       Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdPessoa");   Vr_param.Add(IdPessoa);
                Nm_param.Add("@Informacao"); Vr_param.Add(Informacao);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM REGCOBRANCA WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }
    }
}
