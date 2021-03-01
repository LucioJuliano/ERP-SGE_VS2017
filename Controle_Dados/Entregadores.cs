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
    public class Entregadores
    {
        private int _IdEntregador;
        public int IdEntregador
        {
            get { return _IdEntregador; }
            set { _IdEntregador = value; }
        }
        private string _Entregador;
        public string Entregador
        {
            get { return _Entregador; }
            set { _Entregador = value; }
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

        private int _MapaEntrega;
        public int MapaEntrega
        {
            get { return _MapaEntrega; }
            set { _MapaEntrega = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {

            IdEntregador = 0;
            Entregador   = "";
            Telefone     = "";
            Celular      = "";
            MapaEntrega  = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Entregadores WHERE Id_Entregador=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdEntregador = Id;
                    Entregador = Tabela["Entregador"].ToString().Trim();                    
                    Telefone = Tabela["Telefone"].ToString().Trim();
                    Celular = Tabela["Celular"].ToString().Trim();

                    if (Tabela["MapaEntrega"].ToString() != "")
                        MapaEntrega = int.Parse(Tabela["MapaEntrega"].ToString());
                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdEntregador > 0)
            {
                sSQL = "UPDATE Entregadores SET Id_Entregador=@Id,Entregador=@Entregador,Telefone=@Telefone,Celular=@Celular,MapaEntrega=@MapaEntrega Where Id_Entregador=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdEntregador);
            }
            else
            {
                IdEntregador = Controle.ProximoID("Entregador");
                sSQL = "INSERT INTO Entregadores (ID_Entregador,Entregador,Telefone,Celular,MapaEntrega) VALUES(@Id,@Entregador,@Telefone,@Celular,@MapaEntrega)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdEntregador);
                Nm_param.Add("@Entregador"); Vr_param.Add(Entregador);                
                Nm_param.Add("@Telefone"); Vr_param.Add(Telefone);
                Nm_param.Add("@Celular"); Vr_param.Add(Celular);
                Nm_param.Add("@MapaEntrega"); Vr_param.Add(MapaEntrega);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdEntregador > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Entregadores WHERE Id_Entregador=" + IdEntregador.ToString().Trim());
            }
        }

    }
}
