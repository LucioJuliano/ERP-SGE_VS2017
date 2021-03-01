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
    public class CaixaBalcao
    {
        private int _IdCaixa;

        public int IdCaixa
        {
            get { return _IdCaixa; }
            set { _IdCaixa = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }       
        private decimal _VlrInicial;
        public decimal VlrInicial
        {
            get { return _VlrInicial; }
            set { _VlrInicial = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private DateTime _DtHrAbertura;
        public DateTime DtHrAbertura
        {
            get { return _DtHrAbertura; }
            set { _DtHrAbertura = value; }
        }
        private DateTime _DtHrEnc;
        public DateTime DtHrEnc
        {
            get { return _DtHrEnc; }
            set { _DtHrEnc = value; }
        }
        private string _Observcao;
        public string Observcao
        {
            get { return _Observcao; }
            set { _Observcao = value; }
        }
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }

        public Funcoes Controle;
        public void LerCaixa(int Id)
        {
            IdCaixa = 0;
            IdFilial = 0;
            VlrInicial = 0;
            Status = 0;
            IdUsuario = 0;
            DtHrAbertura = DateTime.Now;
            Observcao = "";
            VlrTotal = 0;
            Data = DateTime.Now;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CaixaBalcao WHERE Id_Caixa=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdCaixa      = Id;
                    IdFilial     = int.Parse(Tabela["Id_Filial"].ToString());                    
                    VlrInicial   = decimal.Parse(Tabela["VlrInicial"].ToString());
                    Status       = int.Parse(Tabela["Status"].ToString());
                    IdUsuario    = int.Parse(Tabela["Id_Usuario"].ToString());
                    DtHrAbertura = DateTime.Parse(Tabela["DtHrAbertura"].ToString());
                    Data         = DateTime.Parse(Tabela["Data"].ToString());
                    Observcao    = Tabela["Observacao"].ToString().Trim();
                    //VlrTotal     = decimal.Parse(Tabela["VlrTotal"].ToString());
                    VlrTotal     = 0;
                    if (Tabela["DtHrEnc"].ToString() != "")
                        DtHrEnc = DateTime.Parse(Tabela["DtHrEnc"].ToString().Trim());                    
                }
            }            
        }
        public void AbrirCaixa()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            IdCaixa = Controle.ProximoID("CAIXABALCAO");
            sSQL = "INSERT INTO CaixaBalcao (Id_Caixa,Id_Filial,VlrInicial,Status,Id_Usuario,DtHrAbertura,Observacao,Data) VALUES(@Id,@IdFilial,@VlrInicial,0,@IdUsuario,@DtHrAbertura,@Observacao,Convert(DateTime,@Data,103))";
            Nm_param.Add("@Id"); Vr_param.Add(IdCaixa);
            Nm_param.Add("@IdFilial"); Vr_param.Add(IdFilial);
            Nm_param.Add("@VlrInicial"); Vr_param.Add(Controle.FloatToStr(VlrInicial,2));
            Nm_param.Add("@IdUsuario"); Vr_param.Add(IdUsuario);
            Nm_param.Add("@DtHrAbertura"); Vr_param.Add(DtHrAbertura);
            Nm_param.Add("@Observacao"); Vr_param.Add(Observcao);
            Nm_param.Add("@Data"); Vr_param.Add(DateTime.Now.Date.ToShortDateString());
            Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);            
        }
        public void FecharCaixa()
        {
            if (IdCaixa > 0)
            {
                ArrayList Nm_param = new ArrayList();
                ArrayList Vr_param = new ArrayList();
                string sSQL = "UPDATE CaixaBalcao Set Status=1,DtHrEnc=@DtHrEnc Where Id_Caixa=" + IdCaixa.ToString();
                Nm_param.Add("@DtHrEnc"); Vr_param.Add(DtHrEnc);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param); 
            }
        }        
    }
}

