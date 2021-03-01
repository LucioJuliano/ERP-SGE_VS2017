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
    public class CupomFiscal
    {
        private int _IdLanc;

        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _NumCF;
        public int NumCF
        {
            get { return _NumCF; }
            set { _NumCF = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private decimal _VlrSubTotal;
        public decimal VlrSubTotal
        {
            get { return _VlrSubTotal; }
            set { _VlrSubTotal = value; }
        }
        private decimal _VlrDesconto;
        public decimal VlrDesconto
        {
            get { return _VlrDesconto; }
            set { _VlrDesconto = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc      = 0;
            NumCF       = 0;
            Data        = DateTime.Now;
            IdVenda     = 0;
            VlrSubTotal = 0;
            VlrDesconto = 0;
            VlrTotal    = 0;            
            Status      = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CUPOMFISCAL WHERE Id_LANC=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc      = Id;                    
                    NumCF       = int.Parse(Tabela["Num_CF"].ToString());
                    Data        = DateTime.Parse(Tabela["Data"].ToString());                    
                    IdVenda     = int.Parse(Tabela["Id_Venda"].ToString());
                    VlrSubTotal = decimal.Parse(Tabela["VlrSubTotal"].ToString());
                    VlrDesconto = decimal.Parse(Tabela["VlrDesconto"].ToString());
                    VlrTotal    = decimal.Parse(Tabela["VlrTotal"].ToString());
                    Status      = int.Parse(Tabela["Status"].ToString());
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
                sSQL = "UPDATE CUPOMFISCAL SET Id_LANC=@Id,NUM_CF=@NUMCF,DATA=Convert(DateTime,@DATA,103),ID_VENDA=@IDVENDA,VLRSUBTOTAL=@VLRSUBTOTAL,VLRDESCONTO=@VLRDESCONTO,VLRTOTAL=@VLRTOTAL,STATUS=@STATUS Where Id_LANC=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("CUPOMFISCAL");
                sSQL = "INSERT INTO CUPOMFISCAL (Id_LANC,NUM_CF,DATA,ID_VENDA,VLRSUBTOTAL,VLRDESCONTO,VLRTOTAL,STATUS) Values(@Id,@NUMCF,Convert(DateTime,@DATA,103),@IDVENDA,@VLRSUBTOTAL,@VLRDESCONTO,@VLRTOTAL,@STATUS)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");          Vr_param.Add(IdLanc);
                Nm_param.Add("@NUMCF");       Vr_param.Add(NumCF);
                Nm_param.Add("@DATA");        Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IDVENDA");     Vr_param.Add(IdVenda);
                Nm_param.Add("@VLRSUBTOTAL"); Vr_param.Add(Controle.FloatToStr(VlrSubTotal,2));
                Nm_param.Add("@VLRDESCONTO"); Vr_param.Add(Controle.FloatToStr(VlrDesconto, 2));
                Nm_param.Add("@VLRTOTAL");    Vr_param.Add(Controle.FloatToStr(VlrTotal, 2));
                Nm_param.Add("@STATUS");      Vr_param.Add(Status);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM CUPOMFISCAL WHERE Id_LANC=" + IdLanc.ToString().Trim());
            }
        }
        public void CancelarCF(int Id)
        {
            if (Id > 0)
            {
                Controle.ExecutaSQL("UPDATE CUPOMFISCAL SET STATUS=2 WHERE Id_LANC=" + Id.ToString().Trim());
            }
        }

    }
}
