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
    public class CotaPremiacao
    {
        private int _IdPremiacao;
        public int IdPremiacao
        {
            get { return _IdPremiacao; }
            set { _IdPremiacao = value; }
        }

        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }


        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }

        private decimal _PRentab;
        public decimal PRentab
        {
            get { return _PRentab; }
            set { _PRentab = value; }
        }
        private decimal _VlrRentab;
        public decimal VlrRentab
        {
            get { return _VlrRentab; }
            set { _VlrRentab = value; }
        }
        private int _Clientes;
        public int Clientes
        {
            get { return _Clientes; }
            set { _Clientes = value; }
        }
        private decimal _VlrClientes;
        public decimal VlrClientes
        {
            get { return _VlrClientes; }
            set { _VlrClientes = value; }
        }
        private decimal _Cota1;
        public decimal Cota1
        {
            get { return _Cota1; }
            set { _Cota1 = value; }
        }
        private decimal _PCota1;
        public decimal PCota1
        {
            get { return _PCota1; }
            set { _PCota1 = value; }
        }


        private decimal _Cota2;
        public decimal Cota2
        {
            get { return _Cota2; }
            set { _Cota2 = value; }
        }
        private decimal _PCota2;
        public decimal PCota2
        {
            get { return _PCota2; }
            set { _PCota2 = value; }
        }

        private decimal _Cota3;
        public decimal Cota3
        {
            get { return _Cota3; }
            set { _Cota3 = value; }
        }
        private decimal _PCota3;
        public decimal PCota3
        {
            get { return _PCota3; }
            set { _PCota3 = value; }
        }

        private int _GradeClientes;
        public int GradeClientes
        {
            get { return _GradeClientes; }
            set { _GradeClientes = value; }
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
        

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdPremiacao    = 0;
            Descricao      = "";
            IdVendedor     = 0;
            PRentab        = 0;
            VlrRentab      = 0;
            Clientes       = 0;
            VlrClientes    = 0;
            Cota1          = 0;
            PCota1         = 0;
            Cota2          = 0;
            PCota2         = 0;
            Cota3          = 0;
            PCota3         = 0;
            GradeClientes  = 0;
            VlrGrdClientes = 0;
            VlrGradePrd1   = 0;
            VlrGradePrd2   = 0;
            

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CotaPremiacao WHERE Id_Premiacao=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdPremiacao   = Id;                    
                    IdVendedor     = int.Parse(Tabela["Id_Vendedor"].ToString());
                    Descricao      = Tabela["Descricao"].ToString().Trim();
                    PRentab        = decimal.Parse(Tabela["P_Rentab"].ToString());
                    VlrRentab      = decimal.Parse(Tabela["Vlr_Rentab"].ToString());
                    Clientes       = int.Parse(Tabela["Clientes"].ToString());                    
                    VlrClientes    = decimal.Parse(Tabela["Vlr_Clientes"].ToString());
                    Cota1          = decimal.Parse(Tabela["Cota1"].ToString());
                    PCota1         = decimal.Parse(Tabela["P_Cota1"].ToString());
                    Cota2          = decimal.Parse(Tabela["Cota2"].ToString());
                    PCota2         = decimal.Parse(Tabela["P_Cota2"].ToString());
                    Cota3          = decimal.Parse(Tabela["Cota3"].ToString());
                    PCota3         = decimal.Parse(Tabela["P_Cota3"].ToString());
                    GradeClientes  = int.Parse(Tabela["Grade_Clientes"].ToString());
                    VlrGrdClientes = decimal.Parse(Tabela["Vlr_GrdClientes"].ToString());
                    VlrGradePrd1   = decimal.Parse(Tabela["Vlr_GradePrd1"].ToString());
                    VlrGradePrd2   = decimal.Parse(Tabela["Vlr_GradePrd2"].ToString());                    

                }
            }
        }


        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdPremiacao > 0)
            {
                sSQL = "UPDATE CotaPremiacao SET Id_Premiacao=@Id,Descricao=@Descricao,ID_Vendedor=@IdVendedor,P_Rentab=@PRentab,Vlr_Rentab=@VlrRentab,Clientes=@Clientes,Vlr_Clientes=@VlrCliente,Cota1=@Cota1," +
                       "P_Cota1=@PCota,Cota2=@Cota2,P_Cota2=@PCota2,Cota3=@Cota3,P_Cota3=@PCota3,Grade_Clientes=@GradeClientes,Vlr_GradeClientes=@VlrGrdClientes,Vlr_GradePrd1=@VlrGradePrd1,Vlr_GradePrd2=@VlrGradePrd2 " +                       
                       "Where Id_Premiacao=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdPremiacao);
            }
            else
            {
                IdPremiacao = Controle.ProximoID("COTAPREMIACAO");
                sSQL = "INSERT INTO Premiacao (Id_Premiacao,Descricao,ID_Vendedor,P_Rentab,Vlr_Rentab,Clientes,Vlr_Clientes,Cota1,P_Cota1,Cota2,P_Cota2,Cota3,P_Cota3,Grade_Clientes,Vlr_GradeClientes,Vlr_GradePrd1,Vlr_GradePrd2)" +
                       " VALUES (@Id,@Descricao,@IdVendedor,@PRentab,@VlrRentab,@Clientes,@VlrCliente,@Cota1,@PCota,@Cota2,@PCota2,@Cota3,@PCota3,@GradeClientes,@VlrGrdClientes,@VlrGradePrd1,@VlrGradePrd2)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");             Vr_param.Add(IdPremiacao);
                Nm_param.Add("@Descricao");      Vr_param.Add(Descricao);
                Nm_param.Add("@IdVendedor");     Vr_param.Add(IdVendedor);
                Nm_param.Add("@PRentab");        Vr_param.Add(Controle.FloatToStr(PRentab, 2));
                Nm_param.Add("@VlrRentab");      Vr_param.Add(Controle.FloatToStr(VlrRentab, 2));
                Nm_param.Add("@Clientes");       Vr_param.Add(Clientes);
                Nm_param.Add("@VlrClientes");    Vr_param.Add(Controle.FloatToStr(VlrClientes, 2));
                Nm_param.Add("@Cota1");          Vr_param.Add(Controle.FloatToStr(Cota1, 2));
                Nm_param.Add("@PCota1");         Vr_param.Add(Controle.FloatToStr(PCota1, 2));
                Nm_param.Add("@Cota2");          Vr_param.Add(Controle.FloatToStr(Cota2, 2));
                Nm_param.Add("@PCota2");         Vr_param.Add(Controle.FloatToStr(PCota2, 2));
                Nm_param.Add("@Cota3");          Vr_param.Add(Controle.FloatToStr(Cota1, 2));
                Nm_param.Add("@PCota3");         Vr_param.Add(Controle.FloatToStr(PCota3, 2));
                Nm_param.Add("@GradeClientes");  Vr_param.Add(GradeClientes);
                Nm_param.Add("@VlrGrdClientes"); Vr_param.Add(Controle.FloatToStr(VlrGrdClientes, 2));
                Nm_param.Add("@VlrGradePrd1");   Vr_param.Add(VlrGradePrd1);
                Nm_param.Add("@VlrGradePrd2");   Vr_param.Add(VlrGradePrd1);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdPremiacao > 0)
                Controle.ExecutaSQL("DELETE FROM CotaPremiacao WHERE Id_Premiacai=" + IdPremiacao.ToString().Trim());
        }

    }
}

