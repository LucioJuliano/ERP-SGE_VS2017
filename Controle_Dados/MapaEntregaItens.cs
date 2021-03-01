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
    public class MapaEntregaItens
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdMapa;
        public int IdMapa
        {
            get { return _IdMapa; }
            set { _IdMapa = value; }
        }
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Obs;
        public string Obs
        {
            get { return _Obs; }
            set { _Obs = value; }
        }
        
        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc       = 0;
            IdMapa       = 0;
            IdVenda       = 0;
            Obs          = "";
            Status       = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MapaEntregaItens WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    IdMapa = int.Parse(Tabela["Id_Mapa"].ToString());
                    IdVenda = int.Parse(Tabela["Id_Venda"].ToString());
                    Status = int.Parse(Tabela["Status"].ToString());
                    Obs = Tabela["Observacao"].ToString().Trim();                    
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
                sSQL = "UPDATE MapaEntregaItens SET Id_Lanc=@Id,Id_Mapa=@IdMapa,Id_Venda=@IdVenda,Observacao=@Obs Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("MAPAENTREGAITENS");
                sSQL   = "INSERT INTO MAPAENTREGAITENS (Id_Lanc,Id_Mapa,Id_Venda,Observacao,Status) VALUES (@Id,@IdMapa,@IdVenda,@Obs,0)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");      Vr_param.Add(IdLanc);
                Nm_param.Add("@IdMapa");  Vr_param.Add(IdMapa);
                Nm_param.Add("@IdVenda"); Vr_param.Add(IdVenda);
                Nm_param.Add("@Obs");     Vr_param.Add(Obs);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM MAPAENTREGAITENS WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}