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
    [Serializable()]
    public class Estados
    {
        private int _IdUf;
        public int IdUf
        {
            get { return _IdUf; }
            set { _IdUf = value; }
        }
        private string _Sigla;
        public string Sigla
        {
            get { return _Sigla; }
            set { _Sigla = value; }
        }
        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        private decimal _IcmsEntrada;
        public decimal IcmsEntrada
        {
            get { return _IcmsEntrada; }
            set { _IcmsEntrada = value; }
        }
        private decimal _IcmsSaida;
        public decimal IcmsSaida
        {
            get { return _IcmsSaida; }
            set { _IcmsSaida = value; }
        }
        private decimal _CodIBGE;
        public decimal CodIBGE
        {
            get { return _CodIBGE; }
            set { _CodIBGE = value; }
        }

        private decimal _PercDifal;
        public decimal PercDifal
        {
            get { return _PercDifal; }
            set { _PercDifal = value; }
        }

        private decimal _ICMSInterno;
        public decimal ICMSInterno
        {
            get { return _ICMSInterno; }
            set { _ICMSInterno = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ESTADOS WHERE Id_UF=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdUf = Id;
                    Sigla       = Tabela["Sigla"].ToString().Trim();
                    Estado      = Tabela["Estado"].ToString().Trim();
                    IcmsEntrada = decimal.Parse(Tabela["IcmsEntrada"].ToString().Trim());
                    IcmsSaida   = decimal.Parse(Tabela["IcmsSaida"].ToString().Trim());
                    CodIBGE     = int.Parse(Tabela["CodIBGE"].ToString().Trim());
                    PercDifal   = decimal.Parse(Tabela["PercDifal"].ToString().Trim());
                    ICMSInterno = decimal.Parse(Tabela["ICMSInterno"].ToString().Trim());
                }
            }
            else
            {
                IdUf = 0;
                Sigla       = "";
                Estado      = "";
                IcmsEntrada = 0;
                IcmsSaida   = 0;
                CodIBGE     = 0;
                PercDifal   = 0;
                ICMSInterno = 0;
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdUf > 0)
            {
                sSQL = "UPDATE ESTADOS SET Id_UF=@Id,Sigla=@Sigla,Estado=@Estado,IcmsEntrada=@IcmsEntrada,IcmsSaida=@IcmsSaida,CodIBGE=@CodIBGE,PercDifal=@PercDifal,ICMSInterno=@ICMSInterno Where Id_Uf=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdUf);
            }
            else
            {
                IdUf = Controle.ProximoID("Estados");
                sSQL = "INSERT INTO ESTADOS (Id_UF,Sigla,Estado,IcmsEntrada,IcmsSaida,CodIBGE,PercDifal,ICMSInterno) Values(@Id,@Sigla,@Estado,@IcmsEntrada,@IcmsSaida,@CodIBGE,@PercDifal,@ICMSInterno)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdUf);
                Nm_param.Add("@Sigla"); Vr_param.Add(Sigla);
                Nm_param.Add("@Estado"); Vr_param.Add(Estado);
                Nm_param.Add("@IcmsEntrada"); Vr_param.Add(Controle.FloatToStr(IcmsEntrada,2));
                Nm_param.Add("@IcmsSaida"); Vr_param.Add(Controle.FloatToStr(IcmsSaida,2));
                Nm_param.Add("@PercDifal"); Vr_param.Add(Controle.FloatToStr(PercDifal, 2));
                Nm_param.Add("@ICMSInterno"); Vr_param.Add(Controle.FloatToStr(ICMSInterno, 2));                
                Nm_param.Add("@CodIBGE"); Vr_param.Add(CodIBGE);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdUf > 0)
            {
                Controle.ExecutaSQL("DELETE FROM ESTADOS WHERE Id_UF=" + IdUf.ToString().Trim());
            }
        }
    }
}
