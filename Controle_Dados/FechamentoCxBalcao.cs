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
    public class FechamentoCxBalcao
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }

        private int _IdCaixa;
        public int IdCaixa
        {
            get { return _IdCaixa; }
            set { _IdCaixa = value; }
        }
        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        private decimal _VlrCalculado;
        public decimal VlrCalculado
        {
            get { return _VlrCalculado; }
            set { _VlrCalculado = value; }
        }
        private decimal _VlrInformado;
        public decimal VlrInformado
        {
            get { return _VlrInformado; }
            set { _VlrInformado = value; }
        }
        private decimal _VlrReceita;
        public decimal VlrReceita
        {
            get { return _VlrReceita; }
            set { _VlrReceita = value; }
        }

        private decimal _VlrDespesa;
        public decimal VlrDespesa
        {
            get { return _VlrDespesa; }
            set { _VlrDespesa = value; }
        }
        private int _ResumoCx;
        public int ResumoCx
        {
            get { return _ResumoCx; }
            set { _ResumoCx = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc       = 0;
            IdCaixa      = 0;
            IdDocumento  = 0;
            VlrCalculado = 0;
            VlrInformado = 0;
            VlrReceita   = 0;
            VlrDespesa   = 0;
            ResumoCx     = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM FechamentoCxBalcao WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc       = Id;
                    IdCaixa      = int.Parse(Tabela["IdCaixa"].ToString());
                    IdDocumento  = int.Parse(Tabela["IdDocumento"].ToString());
                    VlrCalculado = decimal.Parse(Tabela["VlrCalculado"].ToString());
                    VlrInformado = decimal.Parse(Tabela["VlrInformado"].ToString());
                    VlrReceita   = decimal.Parse(Tabela["VlrReceita"].ToString());
                    VlrDespesa   = decimal.Parse(Tabela["VlrDespesa"].ToString());
                    ResumoCx     = int.Parse(Tabela["ResumoCx"].ToString());
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
                sSQL = "UPDATE FECHAMENTOCXBALCAO SET Id_Lanc=@Id,Id_Caixa=@IdCaixa,Id_Documento=@IdDocumento,VlrCalculado=@VlrCalculado,VlrInformado=@VlrInformado,"+
                       "VlrReceita=@VlrReceita,VlrDespesa=@VlrDespesa,ResumoCx=@ResumoCx Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("FECHAMENTOCXBALCAO");
                sSQL = "INSERT INTO FECHAMENTOCXBALCAO (Id_LANC,ID_CAIXA,ID_DOCUMENTO,VLRCALCULADO,VLRINFORMADO,VlrReceita,VlrDespesa,ResumoCx)" +
                       " Values(@Id,@IDCAIXA,@IDDOCUMENTO,@VLRCALCULADO,@VLRINFORMADO,@VlrReceita,@VlrDespesa,@ResumoCx)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");           Vr_param.Add(IdLanc);
                Nm_param.Add("@IDCAIXA");      Vr_param.Add(IdCaixa);
                Nm_param.Add("@IDDOCUMENTO");  Vr_param.Add(IdDocumento);
                Nm_param.Add("@VLRCALCULADO"); Vr_param.Add(Controle.FloatToStr(VlrCalculado,2));
                Nm_param.Add("@VLRINFORMADO"); Vr_param.Add(Controle.FloatToStr(VlrInformado, 2));
                Nm_param.Add("@VlrReceita");   Vr_param.Add(Controle.FloatToStr(VlrReceita, 2));
                Nm_param.Add("@VlrDespesa");   Vr_param.Add(Controle.FloatToStr(VlrDespesa, 2));
                Nm_param.Add("@ResumoCx");     Vr_param.Add(Controle.FloatToStr(ResumoCx, 2));                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM  FECHAMENTOCXBALCAO WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }
    }
}
