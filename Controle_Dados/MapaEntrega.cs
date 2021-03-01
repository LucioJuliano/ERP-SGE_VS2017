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
    public class MapaEntrega
    {
        private int _IdMapa;
        public int IdMapa
        {
            get { return _IdMapa; }
            set { _IdMapa = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _IdEntregador;
        public int IdEntregador
        {
            get { return _IdEntregador; }
            set { _IdEntregador = value; }
        }
        private int _IdVeiculo;
        public int IdVeiculo
        {
            get { return _IdVeiculo; }
            set { _IdVeiculo = value; }
        }
        private string _Obs;
        public string Obs
        {
            get { return _Obs; }
            set { _Obs = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Conferente;
        public string Conferente
        {
            get { return _Conferente; }
            set { _Conferente = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdMapa       = 0;
            Data         = DateTime.Now;
            IdEntregador = 0;
            Obs          = "";
            Status       = 0;
            Conferente   = "";
            IdVeiculo    = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MapaEntrega WHERE Id_Mapa=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdMapa = Id;
                    Data = DateTime.Parse(Tabela["Data"].ToString());
                    IdEntregador = int.Parse(Tabela["Id_Entregador"].ToString());
                    IdVeiculo = int.Parse(Tabela["Id_Veiculo"].ToString());
                    Status = int.Parse(Tabela["Status"].ToString());
                    Obs = Tabela["Observacao"].ToString().Trim();
                    Conferente = Tabela["Conferente"].ToString().Trim();

                }
            }
        }

        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdMapa > 0)
            {
                sSQL = "UPDATE MapaEntrega SET Id_Mapa=@Id,Data=Convert(DateTime,@Data,103),Id_Entregador=@IdEntregador,Observacao=@Obs,Conferente=@Conferente,ID_Veiculo=@IdVeiculo Where Id_Mapa=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdMapa);
            }
            else
            {
                IdMapa = Controle.ProximoID("MAPAENTREGA");
                sSQL = "INSERT INTO MAPAENTREGA (Id_Mapa,Data,Id_Entregador,Observacao,Status,Conferente,Id_Veiculo)" +
                       " VALUES (@Id,Convert(DateTime,@Data,103),@IdEntregador,@Obs,0,@Conferente,@IdVeiculo)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdMapa);
                Nm_param.Add("@Data"); Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdEntregador"); Vr_param.Add(IdEntregador);
                Nm_param.Add("@IdVeiculo"); Vr_param.Add(IdVeiculo);
                Nm_param.Add("@Obs"); Vr_param.Add(Obs);
                Nm_param.Add("@Conferente"); Vr_param.Add(Conferente);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdMapa > 0)
            {
                Controle.ExecutaSQL("DELETE FROM MAPAENTREGA WHERE Id_Mapa=" + IdMapa.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM MAPAENTREGAITENS WHERE Id_Mapa=" + IdMapa.ToString().Trim());
            }
        }
    }
}
