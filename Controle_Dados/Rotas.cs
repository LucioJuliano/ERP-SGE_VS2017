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
    public class Rotas
    {
        private int _IdRota;
        public int IdRota
        {
            get { return _IdRota; }
            set { _IdRota = value; }
        }
        private string _Rota;
        public string Rota
        {
            get { return _Rota; }
            set { _Rota = value; }
        }
        private string _Responsavel;
        public string Responsavel
        {
            get { return _Responsavel; }
            set { _Responsavel = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Rotas WHERE Id_Rota=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdRota = Id;
                    Rota = Tabela["Rota"].ToString().Trim();
                    Responsavel = Tabela["Responsavel"].ToString().Trim();                    
                }
            }
            else
            {
                IdRota = 0;
                Rota = "";
                Responsavel = "";                
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdRota > 0)
            {
                sSQL = "UPDATE ROTAS SET Id_Rota=@Id,Rota=@Rota,Responsavel=@Responsavel Where Id_Rota=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdRota);
            }
            else
            {
                IdRota = Controle.ProximoID("ROTAS");
                sSQL = "INSERT INTO ROTAS (ID_ROTA,ROTA,RESPONSAVEL) VALUES(@Id,@Rota,@Responsavel)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdRota);
                Nm_param.Add("@Rota"); Vr_param.Add(Rota);
                Nm_param.Add("@Responsavel"); Vr_param.Add(Responsavel);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdRota > 0)
            {
                Controle.ExecutaSQL("DELETE FROM ROTAS WHERE Id_Rota=" + IdRota.ToString().Trim());
            }
        }
    }
}
