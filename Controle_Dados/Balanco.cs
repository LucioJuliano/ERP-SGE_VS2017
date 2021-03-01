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
    public class Balanco
    {
        private int _IdBalanco;
        public int IdBalanco
        {
            get { return _IdBalanco; }
            set { _IdBalanco = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private string _Responsavel;
        public string Responsavel
        {
            get { return _Responsavel; }
            set { _Responsavel = value; }
        }

        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int _IdEntrada;
        public int IdEntrada
        {
            get { return _IdEntrada; }
            set { _IdEntrada = value; }
        }
        private int _IdSaida;
        public int IdSaida
        {
            get { return _IdSaida; }
            set { _IdSaida = value; }
        }
        private int _IdSaldo;
        public int IdSaldo
        {
            get { return _IdSaldo; }
            set { _IdSaldo = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdBalanco  = 0;
            Data       = DateTime.Now;
            Observacao = "";
            Status     = 0;
            IdEntrada  = 0;
            IdSaida    = 0;
            IdSaldo    = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM BALANCO WHERE ID_BALANCO=" + Id.ToString().Trim());

                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdBalanco   = Id;
                    Data        = DateTime.Parse(Tabela["Data"].ToString());
                    Observacao  = Tabela["Observacao"].ToString().Trim();
                    Responsavel = Tabela["Responsavel"].ToString().Trim();
                    IdEntrada   = int.Parse(Tabela["Id_Entrada"].ToString());
                    IdSaida     = int.Parse(Tabela["Id_Saida"].ToString());
                    IdSaldo     = int.Parse(Tabela["Id_Saldo"].ToString());
                    Status      = int.Parse(Tabela["Status"].ToString()); 
                }
            }            
        }

        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdBalanco > 0)
            {
                sSQL = "UPDATE BALANCO SET Id_BALANCO=@Id,DATA=Convert(DateTime,@Data,103),OBSERVACAO=@OBSERVACAO,RESPONSAVEL=@RESPONSAVEL Where Id_BALANCO=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdBalanco);
            }
            else
            {
                IdBalanco = Controle.ProximoID("BALANCO");
                sSQL = "INSERT INTO BALANCO (Id_BALANCO,DATA,OBSERVACAO,RESPONSAVEL,ID_ENTRADA,ID_SAIDA,STATUS,ID_SALDO) Values(@Id,Convert(DateTime,@Data,103),@OBSERVACAO,@RESPONSAVEL,0,0,0,0)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");         Vr_param.Add(IdBalanco);
                Nm_param.Add("@DATA");       Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@OBSERVACAO"); Vr_param.Add(Observacao);
                Nm_param.Add("@RESPONSAVEL"); Vr_param.Add(Responsavel);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdBalanco > 0)
            {
                Controle.ExecutaSQL("DELETE FROM BALANCO WHERE Id_Balanco=" + IdBalanco.ToString().Trim());
            }
        }
    }
}
