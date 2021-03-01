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
    public class Veiculos
    {
        private int _IdVeiculo;
        public int IdVeiculo
        {
            get { return _IdVeiculo; }
            set { _IdVeiculo = value; }
        }
        private string _Veiculo;
        public string Veiculo
        {
            get { return _Veiculo; }
            set { _Veiculo = value; }
        }
        private string _Placa;
        public string Placa
        {
            get { return _Placa; }
            set { _Placa = value; }
        }

        private decimal _VlrCarga;
        public decimal VlrCarga
        {
            get { return _VlrCarga; }
            set { _VlrCarga = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdVeiculo = 0;
            Veiculo   = "";
            Placa     = "";

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM VEICULOS WHERE Id_Veiculo=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdVeiculo = Id;
                    Veiculo   = Tabela["Veiculo"].ToString().Trim();
                    Placa     = Tabela["Placa"].ToString().Trim();
                    VlrCarga  = decimal.Parse(Tabela["VlrCarga"].ToString());
                }
            }
        }

        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdVeiculo > 0)
            {
                sSQL = "UPDATE VEICULOS SET Id_Veiculo=@Id,Veiculo=@Veiculo,Placa=@Placa,VlrCarga=@VlrCarga Where Id_Veiculo=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdVeiculo);
            }
            else
            {
                IdVeiculo = Controle.ProximoID("VEICULOS");
                sSQL = "INSERT INTO VEICULOS (Id_Veiculo,Veiculo,Placa,VlrCarga)" +
                       " VALUES (@Id,@Veiculo,@Placa,@VlrCarga)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdVeiculo);
                Nm_param.Add("@Veiculo"); Vr_param.Add(Veiculo);
                Nm_param.Add("@Placa"); Vr_param.Add(Placa);
                Nm_param.Add("@VlrCarga"); Vr_param.Add(Controle.FloatToStr(VlrCarga, 2));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdVeiculo > 0)
                Controle.ExecutaSQL("DELETE FROM VEICULOS WHERE Id_Mapa=" + IdVeiculo.ToString().Trim());
        }
    }
}
