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
    public class CotaGrupoPrdVend
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private int _IdGrupo;
        public int IdGrupo
        {
            get { return _IdGrupo; }
            set { _IdGrupo = value; }
        }
        private int _IdGrupoAux;
        public int IdGrupoAux
        {
            get { return _IdGrupoAux; }
            set { _IdGrupoAux = value; }
        }
        private decimal _VlrCota;
        public decimal VlrCota
        {
            get { return _VlrCota; }
            set { _VlrCota = value; }
        }

        private decimal _VlrPremio;
        public decimal VlrPremio
        {
            get { return _VlrPremio; }
            set { _VlrPremio = value; }
        }

        private decimal _VlrCota2;
        public decimal VlrCota2
        {
            get { return _VlrCota2; }
            set { _VlrCota2 = value; }
        }

        private decimal _VlrPremio2;
        public decimal VlrPremio2
        {
            get { return _VlrPremio2; }
            set { _VlrPremio2 = value; }
        }

        private string _TpPremio;
        public string TpPremio
        {
            get { return _TpPremio; }
            set { _TpPremio = value; }
        }
        
        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc      = 0;
            IdVendedor  = 0;
            IdGrupo     = 0;
            VlrCota     = 0;
            IdGrupoAux  = 0;
            VlrPremio   = 0;
            VlrCota2    = 0;
            VlrPremio2  = 0;
            TpPremio    = "$";
            

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CotaGrupoPrdVendedor WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc      = Id;
                    IdVendedor  = int.Parse(Tabela["Id_Vendedor"].ToString());
                    IdGrupo     = int.Parse(Tabela["Id_Grupo"].ToString());
                    IdGrupoAux  = int.Parse(Tabela["Id_GrupoAux"].ToString());
                    VlrCota     = decimal.Parse(Tabela["VlrCota"].ToString());                    
                    VlrPremio   = decimal.Parse(Tabela["VlrPremio"].ToString());
                    VlrCota2    = decimal.Parse(Tabela["VlrCota2"].ToString());
                    VlrPremio2  = decimal.Parse(Tabela["VlrPremio2"].ToString());
                    TpPremio    = Tabela["TpPremio"].ToString().Trim();
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
                sSQL = "UPDATE CotaGrupoPrdVendedor SET Id_Lanc=@Id,Id_Vendedor=@IdVendedor,Id_Grupo=@IdGrupo,VlrCota=@VlrCota,Id_GrupoAux=@IdGrupoAux,VlrPremio=@VlrPremio,"+
                       "VlrCota2=@VlrCota2,VlrPremio2=@VlrPremio2,TpPremio=@TpPremio Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("COTAGRUPOPRDVEND");
                sSQL = "INSERT INTO CotaGrupoPrdVendedor (Id_Lanc,Id_Vendedor,Id_Grupo,VlrCota,Id_GrupoAux,VlrPremio,VlrCota2,VlrPremio2,TpPremio) "+
                       " VALUES (@Id,@IdVendedor,@IdGrupo,@VlrCota,@IdGrupoAux,@VlrPremio,@VlrCota2,@VlrPremio2,@TpPremio)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");          Vr_param.Add(IdLanc);
                Nm_param.Add("@IdVendedor");  Vr_param.Add(IdVendedor);
                Nm_param.Add("@IdGrupo");     Vr_param.Add(IdGrupo);
                Nm_param.Add("@VlrCota");     Vr_param.Add(Controle.FloatToStr(VlrCota, 2));
                Nm_param.Add("@IdGrupoAux");  Vr_param.Add(IdGrupoAux);
                Nm_param.Add("@VlrPremio");   Vr_param.Add(Controle.FloatToStr(VlrPremio, 2));
                Nm_param.Add("@VlrCota2");    Vr_param.Add(Controle.FloatToStr(VlrCota2, 2));
                Nm_param.Add("@VlrPremio2");  Vr_param.Add(Controle.FloatToStr(VlrPremio2, 2));
                Nm_param.Add("@TpPremio");    Vr_param.Add(TpPremio);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM CotaGrupoPrdVendedor WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}