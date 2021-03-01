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
    public class Premiacao
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        
        private string _Premio;
        public string Premio
        {
            get { return _Premio; }
            set { _Premio = value; }
        }

        private decimal _Financeiro1;
        public decimal Financeiro1
        {
            get { return _Financeiro1; }
            set { _Financeiro1 = value; }
        }
        private decimal _PercFinanc1;
        public decimal PercFinanc1
        {
            get { return _PercFinanc1; }
            set { _PercFinanc1 = value; }
        }
        private decimal _Rentabilidade1;
        public decimal Rentabilidade1
        {
            get { return _Rentabilidade1; }
            set { _Rentabilidade1 = value; }
        }
        private decimal _PercRentab1;
        public decimal PercRentab1
        {
            get { return _PercRentab1; }
            set { _PercRentab1 = value; }
        }
        private decimal _Cliente1;
        public decimal Cliente1
        {
            get { return _Cliente1; }
            set { _Cliente1 = value; }
        }
        private decimal _PercCliente1;
        public decimal PercCliente1
        {
            get { return _PercCliente1; }
            set { _PercCliente1 = value; }
        }


        private decimal _Financeiro2;
        public decimal Financeiro2
        {
            get { return _Financeiro2; }
            set { _Financeiro2 = value; }
        }
        private decimal _PercFinanc2;
        public decimal PercFinanc2
        {
            get { return _PercFinanc2; }
            set { _PercFinanc2 = value; }
        }
        private decimal _Rentabilidade2;
        public decimal Rentabilidade2
        {
            get { return _Rentabilidade2; }
            set { _Rentabilidade2 = value; }
        }
        private decimal _PercRentab2;
        public decimal PercRentab2
        {
            get { return _PercRentab2; }
            set { _PercRentab2 = value; }
        }
        private decimal _Cliente2;
        public decimal Cliente2
        {
            get { return _Cliente2; }
            set { _Cliente2 = value; }
        }
        private decimal _PercCliente2;
        public decimal PercCliente2
        {
            get { return _PercCliente2; }
            set { _PercCliente2 = value; }
        }


        private decimal _Financeiro3;
        public decimal Financeiro3
        {
            get { return _Financeiro3; }
            set { _Financeiro3 = value; }
        }
        private decimal _PercFinanc3;
        public decimal PercFinanc3
        {
            get { return _PercFinanc3; }
            set { _PercFinanc3 = value; }
        }
        private decimal _Rentabilidade3;
        public decimal Rentabilidade3
        {
            get { return _Rentabilidade3; }
            set { _Rentabilidade3 = value; }
        }
        private decimal _PercRentab3;
        public decimal PercRentab3
        {
            get { return _PercRentab3; }
            set { _PercRentab3 = value; }
        }
        private int _Cliente3;
        public int Cliente3
        {
            get { return _Cliente3; }
            set { _Cliente3 = value; }
        }
        private decimal _PercCliente3;
        public decimal PercCliente3
        {
            get { return _PercCliente3; }
            set { _PercCliente3 = value; }
        }


        private decimal _Financeiro4;
        public decimal Financeiro4
        {
            get { return _Financeiro4; }
            set { _Financeiro4 = value; }
        }
        private decimal _PercFinanc4;
        public decimal PercFinanc4
        {
            get { return _PercFinanc4; }
            set { _PercFinanc4 = value; }
        }
        private decimal _Rentabilidade4;
        public decimal Rentabilidade4
        {
            get { return _Rentabilidade4; }
            set { _Rentabilidade4 = value; }
        }
        private decimal _PercRentab4;
        public decimal PercRentab4
        {
            get { return _PercRentab4; }
            set { _PercRentab4 = value; }
        }
        private int _Cliente4;
        public int Cliente4
        {
            get { return _Cliente4; }
            set { _Cliente4 = value; }
        }
        private decimal _PercCliente4;
        public decimal PercCliente4
        {
            get { return _PercCliente4; }
            set { _PercCliente4 = value; }
        }

        private int _Agrupar1;
        public int Agrupar1
        {
            get { return _Agrupar1; }
            set { _Agrupar1 = value; }
        }

        private int _Agrupar2;
        public int Agrupar2
        {
            get { return _Agrupar2; }
            set { _Agrupar2 = value; }
        }

        private int _Agrupar3;
        public int Agrupar3
        {
            get { return _Agrupar3; }
            set { _Agrupar3 = value; }
        }

        private int _Agrupar4;
        public int Agrupar4
        {
            get { return _Agrupar4; }
            set { _Agrupar4 = value; }
        }

        private int _ProxPremio;
        public int ProxPremio
        {
            get { return _ProxPremio; }
            set { _ProxPremio = value; }
        }

        private int _CotaFinanceira;
        public int CotaFinanceira
        {
            get { return _CotaFinanceira; }
            set { _CotaFinanceira = value; }
        }
        
        private decimal _VlrRentab;
        public decimal VlrRentab
        {
            get { return _VlrRentab; }
            set { _VlrRentab = value; }
        }
        private decimal _VlrCliente;
        public decimal VlrCliente
        {
            get { return _VlrCliente; }
            set { _VlrCliente = value; }
        }

        private decimal _VlrGrdClientes;
        public decimal VlrGrdClientes
        {
            get { return _VlrGrdClientes; }
            set { _VlrGrdClientes = value; }
        }

        private decimal _VlrGradePrd1;
        public decimal VlrGradePrd1
        {
            get { return _VlrGradePrd1; }
            set { _VlrGradePrd1 = value; }
        }

        private decimal _VlrGradePrd2;
        public decimal VlrGradePrd2
        {
            get { return _VlrGradePrd2; }
            set { _VlrGradePrd2 = value; }
        }

        private int _GradeClientes;
        public int GradeClientes
        {
            get { return _GradeClientes; }
            set { _GradeClientes = value; }
        }
        

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc         = 0;           
            Premio         = "";
            Financeiro1    = 0;
            PercFinanc1    = 0;
            Rentabilidade1 = 0;
            PercRentab1    = 0;
            Cliente1       = 0;
            PercCliente1   = 0;
            Financeiro2    = 0;
            PercFinanc2    = 0;
            Rentabilidade2 = 0;
            PercRentab2    = 0;
            Cliente2       = 0;
            PercCliente2   = 0;
            Financeiro3    = 0;
            PercFinanc3    = 0;
            Rentabilidade3 = 0;
            PercRentab3    = 0;
            Cliente3       = 0;
            PercCliente3   = 0;
            Financeiro4    = 0;
            PercFinanc4    = 0;
            Rentabilidade4 = 0;
            PercRentab4    = 0;
            Cliente4       = 0;
            PercCliente4   = 0;
            ProxPremio     = 0;
            Agrupar1       = 0;
            Agrupar2       = 0;
            Agrupar3       = 0;
            Agrupar4       = 0;
            CotaFinanceira = 0;
            VlrRentab      = 0;
            VlrCliente     = 0;
            VlrGrdClientes = 0;
            VlrGradePrd1   = 0;
            VlrGradePrd2   = 0;
            GradeClientes  = 0;


            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Premiacao WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    Premio         = Tabela["Premio"].ToString();
                    Financeiro1    = decimal.Parse(Tabela["Financeiro1"].ToString());
                    PercFinanc1    = decimal.Parse(Tabela["Perc_Financ1"].ToString());
                    Rentabilidade1 = decimal.Parse(Tabela["Rentabilidade1"].ToString());
                    PercRentab1    = decimal.Parse(Tabela["Perc_Rentab1"].ToString());
                    Cliente1       = int.Parse(Tabela["Cliente1"].ToString());
                    PercCliente1   = decimal.Parse(Tabela["Perc_Cliente1"].ToString());
                    Financeiro2    = decimal.Parse(Tabela["Financeiro2"].ToString());
                    PercFinanc2    = decimal.Parse(Tabela["Perc_Financ2"].ToString());
                    Rentabilidade2 = decimal.Parse(Tabela["Rentabilidade2"].ToString());
                    PercRentab2    = decimal.Parse(Tabela["Perc_Rentab2"].ToString());
                    Cliente2       = int.Parse(Tabela["Cliente2"].ToString());
                    PercCliente2   = decimal.Parse(Tabela["Perc_Cliente2"].ToString());
                    Financeiro3    = decimal.Parse(Tabela["Financeiro3"].ToString());
                    PercFinanc3    = decimal.Parse(Tabela["Perc_Financ3"].ToString());
                    Rentabilidade3 = decimal.Parse(Tabela["Rentabilidade3"].ToString());
                    PercRentab3    = decimal.Parse(Tabela["Perc_Rentab3"].ToString());
                    Cliente3       = int.Parse(Tabela["Cliente3"].ToString());
                    PercCliente3   = decimal.Parse(Tabela["Perc_Cliente3"].ToString());
                    Financeiro4    = decimal.Parse(Tabela["Financeiro4"].ToString());
                    PercFinanc4    = decimal.Parse(Tabela["Perc_Financ4"].ToString());
                    Rentabilidade4 = decimal.Parse(Tabela["Rentabilidade4"].ToString());
                    PercRentab4    = decimal.Parse(Tabela["Perc_Rentab4"].ToString());
                    Cliente4       = int.Parse(Tabela["Cliente4"].ToString());
                    PercCliente4   = decimal.Parse(Tabela["Perc_Cliente4"].ToString());
                    ProxPremio     = int.Parse(Tabela["Prox_Premio"].ToString());
                    Agrupar1       = int.Parse(Tabela["Agrupar1"].ToString());
                    Agrupar2       = int.Parse(Tabela["Agrupar2"].ToString());
                    Agrupar3       = int.Parse(Tabela["Agrupar3"].ToString());
                    Agrupar4       = int.Parse(Tabela["Agrupar4"].ToString());
                    CotaFinanceira = int.Parse(Tabela["CotaFinanceira"].ToString());
                    VlrRentab      = decimal.Parse(Tabela["VlrRentab"].ToString()); ;
                    VlrCliente     = decimal.Parse(Tabela["VlrCliente"].ToString()); ;
                    VlrGrdClientes = decimal.Parse(Tabela["VlrGrdClientes"].ToString()); ;
                    VlrGradePrd1   = decimal.Parse(Tabela["VlrGradePrd1"].ToString()); ;
                    VlrGradePrd2   = decimal.Parse(Tabela["VlrGradePrd2"].ToString()); ;
                    GradeClientes  = int.Parse(Tabela["GradeClientes"].ToString()); ;
                    
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
                sSQL = "UPDATE Premiacao SET Id_Lanc=@Id,Premio=@Premio,Financeiro1=@Financeiro1,Perc_Financ1=@PercFinanc1,Rentabilidade1=@Rentabilidade1,Perc_Rentab1=@PercRentab1,Cliente1=@Cliente1,Perc_Cliente1=@PercCliente1,"+
                       "Financeiro2=@Financeiro2,Perc_Financ2=@PercFinanc2,Rentabilidade2=@Rentabilidade2,Perc_Rentab2=@PercRentab2,Cliente2=@Cliente2,Perc_Cliente2=@PercCliente2,"+
                       "Financeiro3=@Financeiro3,Perc_Financ3=@PercFinanc3,Rentabilidade3=@Rentabilidade3,Perc_Rentab3=@PercRentab3,Cliente3=@Cliente3,Perc_Cliente3=@PercCliente3,"+
                       "Financeiro4=@Financeiro4,Perc_Financ4=@PercFinanc4,Rentabilidade4=@Rentabilidade4,Perc_Rentab4=@PercRentab4,Cliente4=@Cliente4,Perc_Cliente4=@PercCliente4,Prox_Premio=@ProxPremio,"+
                       "Agrupar1=@Agrupar1,Agrupar2=@Agrupar2,Agrupar3=@Agrupar3,Agrupar4=@Agrupar4,CotaFinanceira=@CotaFinanceira,VlrRentab=@VlrRentab,VlrCliente=@VlrCliente,VlrGrdClientes=@VlrGrdClientes,VlrGradePrd1=@VlrGradePrd1,"+
                       "VlrGradePrd2=@VlrGradePrd2,GradeClientes=@GradeClientes  Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("PREMIACAO");
                sSQL = "INSERT INTO Premiacao (Id_Lanc,Premio,Financeiro1,Perc_Financ1,Rentabilidade1,Perc_Rentab1,Cliente1,Perc_Cliente1,Financeiro2,Perc_Financ2,Rentabilidade2,Perc_Rentab2,Cliente2,Perc_Cliente2,"+
                       "Financeiro3,Perc_Financ3,Rentabilidade3,Perc_Rentab3,Cliente3,Perc_Cliente3,Financeiro4,Perc_Financ4,Rentabilidade4,Perc_Rentab4,Cliente4,Perc_Cliente4,Prox_Premio,Agrupar1,Agrupar2,Agrupar3,Agrupar4,CotaFinanceira," +
                       "VlrRentab,VlrCliente,VlrGrdClientes,VlrGradePrd1,VlrGradePrd2,GradeClientes) "+
                       " VALUES (@Id,@Premio,@Financeiro1,@PercFinanc1,@Rentabilidade1,@PercRentab1,@Cliente1,@PercCliente1,@Financeiro2,@PercFinanc2,@Rentabilidade2,@PercRentab2,@Cliente2,@PercCliente2,"+
                       "@Financeiro3,@PercFinanc3,@Rentabilidade3,@PercRentab3,@Cliente3,@PercCliente3,@Financeiro4,@PercFinanc4,@Rentabilidade4,@PercRentab4,@Cliente4,@PercCliente4,@ProxPremio,@Agrupar1,@Agrupar2,@Agrupar3,@Agrupar4,@CotaFinanceira,"+
                       "@VlrRentab,@VlrCliente,@VlrGrdClientes,@VlrGradePrd1,@VlrGradePrd2,@GradeClientes)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);                
                Nm_param.Add("@Premio");         Vr_param.Add(Premio);
                Nm_param.Add("@Financeiro1");    Vr_param.Add(Controle.FloatToStr(Financeiro1, 2));
                Nm_param.Add("@PercFinanc1");    Vr_param.Add(Controle.FloatToStr(PercFinanc1, 2));
                Nm_param.Add("@Rentabilidade1"); Vr_param.Add(Controle.FloatToStr(Rentabilidade1, 2));
                Nm_param.Add("@PercRentab1");    Vr_param.Add(Controle.FloatToStr(PercRentab1, 2));
                Nm_param.Add("@Cliente1");       Vr_param.Add(Cliente1);
                Nm_param.Add("@PercCliente1");   Vr_param.Add(Controle.FloatToStr(PercCliente1, 2));
                Nm_param.Add("@Financeiro2");    Vr_param.Add(Controle.FloatToStr(Financeiro2, 2));
                Nm_param.Add("@PercFinanc2");    Vr_param.Add(Controle.FloatToStr(PercFinanc2, 2));
                Nm_param.Add("@Rentabilidade2"); Vr_param.Add(Controle.FloatToStr(Rentabilidade2, 2));
                Nm_param.Add("@PercRentab2");    Vr_param.Add(Controle.FloatToStr(PercRentab2, 2));
                Nm_param.Add("@Cliente2");       Vr_param.Add(Cliente2);
                Nm_param.Add("@PercCliente2");   Vr_param.Add(Controle.FloatToStr(PercCliente2, 2));
                Nm_param.Add("@Financeiro3");    Vr_param.Add(Controle.FloatToStr(Financeiro3, 2));
                Nm_param.Add("@PercFinanc3");    Vr_param.Add(Controle.FloatToStr(PercFinanc3, 2));
                Nm_param.Add("@Rentabilidade3"); Vr_param.Add(Controle.FloatToStr(Rentabilidade3, 2));
                Nm_param.Add("@PercRentab3");    Vr_param.Add(Controle.FloatToStr(PercRentab3, 2));
                Nm_param.Add("@Cliente3");       Vr_param.Add(Cliente3);
                Nm_param.Add("@PercCliente3");   Vr_param.Add(Controle.FloatToStr(PercCliente3, 2));
                Nm_param.Add("@Financeiro4");    Vr_param.Add(Controle.FloatToStr(Financeiro4, 2));
                Nm_param.Add("@PercFinanc4");    Vr_param.Add(Controle.FloatToStr(PercFinanc4, 2));
                Nm_param.Add("@Rentabilidade4"); Vr_param.Add(Controle.FloatToStr(Rentabilidade4, 2));
                Nm_param.Add("@PercRentab4");    Vr_param.Add(Controle.FloatToStr(PercRentab4, 2));
                Nm_param.Add("@Cliente4");       Vr_param.Add(Cliente4);
                Nm_param.Add("@PercCliente4");   Vr_param.Add(Controle.FloatToStr(PercCliente4, 2));
                Nm_param.Add("@ProxPremio");     Vr_param.Add(ProxPremio);
                Nm_param.Add("@Agrupar1");       Vr_param.Add(Agrupar1);
                Nm_param.Add("@Agrupar2");       Vr_param.Add(Agrupar2);
                Nm_param.Add("@Agrupar3");       Vr_param.Add(Agrupar3);
                Nm_param.Add("@Agrupar4");       Vr_param.Add(Agrupar4);
                Nm_param.Add("@CotaFinanceira"); Vr_param.Add(CotaFinanceira);
                Nm_param.Add("@VlrRentab");      Vr_param.Add(Controle.FloatToStr(VlrRentab, 2));
                Nm_param.Add("@VlrCliente");     Vr_param.Add(Controle.FloatToStr(VlrCliente, 2));
                Nm_param.Add("@VlrGrdClientes"); Vr_param.Add(Controle.FloatToStr(VlrGrdClientes, 2));
                Nm_param.Add("@VlrGradePrd1");   Vr_param.Add(Controle.FloatToStr(VlrGradePrd1, 2));
                Nm_param.Add("@VlrGradePrd2");   Vr_param.Add(Controle.FloatToStr(VlrGradePrd2, 2));
                Nm_param.Add("@GradeClientes "); Vr_param.Add(Controle.FloatToStr(GradeClientes, 2));

                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM Premiacao WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
      
    }
}

