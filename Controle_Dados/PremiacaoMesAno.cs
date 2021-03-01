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
    public class PremiacaoMesAno
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private string _MesAno;
        public string MesAno
        {
            get { return _MesAno; }
            set { _MesAno = value; }
        }

        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }

        private int _IdPremio;
        public int IdPremio
        {
            get { return _IdPremio; }
            set { _IdPremio = value; }
        }
        private decimal _Financeiro;
        public decimal Financeiro
        {
            get { return _Financeiro; }
            set { _Financeiro = value; }
        }
        private decimal _PercFinanc;
        public decimal PercFinanc
        {
            get { return _PercFinanc; }
            set { _PercFinanc = value; }
        }
        private decimal _Rentabilidade;
        public decimal Rentabilidade
        {
            get { return _Rentabilidade; }
            set { _Rentabilidade = value; }
        }
        private decimal _PercRentab;
        public decimal PercRentab
        {
            get { return _PercRentab; }
            set { _PercRentab = value; }
        }
        private decimal _Cliente;
        public decimal Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }
        private decimal _PercCliente;
        public decimal PercCliente
        {
            get { return _PercCliente; }
            set { _PercCliente = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc        = 0;
            MesAno        = "";
            IdVendedor    = 0;
            IdPremio      = 0;
            Financeiro    = 0;
            PercFinanc    = 0;
            Rentabilidade = 0;
            PercRentab    = 0;
            Cliente       = 0;
            PercCliente   = 0;
            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PremiacaoMesAno WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc        = Id;
                    MesAno        = Tabela["MesAno"].ToString();
                    IdVendedor    = int.Parse(Tabela["Id_Vendedor"].ToString());
                    IdPremio      = int.Parse(Tabela["Id_Premio"].ToString());
                    Financeiro    = decimal.Parse(Tabela["Financeiro"].ToString());
                    PercFinanc    = decimal.Parse(Tabela["Perc_Financ"].ToString());
                    Rentabilidade = decimal.Parse(Tabela["Rentabilidade"].ToString());
                    PercRentab    = decimal.Parse(Tabela["PercRentab"].ToString());
                    Cliente       = int.Parse(Tabela["Cliente"].ToString());
                    PercCliente   = decimal.Parse(Tabela["PercCliente"].ToString());
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
                sSQL = "UPDATE PremiacaoMesAno SET Id_Lanc=@Id,MesAno=@MesAno,Id_Vendedor=@IdVendedor,Id_Premio=@IdPremio,Financeiro=@Financeiro,PercFinanc=@PercFinanc,Rentabilidade=@Rentabilidade,"+
                       "PercRentab=@PercRentab,Cliente=@Cliente,PercCliente=@PercCliente Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("PREMIACAOMESANO");
                sSQL = "INSERT INTO PremiacaoMesAno (Id_Lanc,MesAno,Id_Vendedor,Id_Premio,Financeiro,PercFinanc,Rentabilidade,PercRentab,Cliente,PercCliente) " +
                       " VALUES (@Id,@MesAno,@IdVendedor,@IdPremio,@Financeiro,@PercFinanc,@Rentabilidade,@PercRentab,@Cliente,@PercCliente)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");            Vr_param.Add(IdLanc);
                Nm_param.Add("@MesAno");        Vr_param.Add(MesAno);
                Nm_param.Add("@IdVendedor");    Vr_param.Add(IdVendedor);
                Nm_param.Add("@IdPremio");      Vr_param.Add(IdPremio);
                Nm_param.Add("@Financeiro");    Vr_param.Add(Controle.FloatToStr(Financeiro, 2));
                Nm_param.Add("@PercFinanc");    Vr_param.Add(Controle.FloatToStr(PercFinanc, 2));
                Nm_param.Add("@Rentabilidade"); Vr_param.Add(Controle.FloatToStr(Rentabilidade, 2));
                Nm_param.Add("@PercRentab");    Vr_param.Add(Controle.FloatToStr(PercRentab, 2));
                Nm_param.Add("@Cliente");       Vr_param.Add(Cliente);
                Nm_param.Add("@PercCliente");   Vr_param.Add(Controle.FloatToStr(PercCliente, 2));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM PremiacaoMesAno WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }

    }
}

