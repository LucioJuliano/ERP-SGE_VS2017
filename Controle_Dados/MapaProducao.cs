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
    public class MapaProducao
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
        private int _IdProdProd;
        public int IdProdProd
        {
            get { return _IdProdProd; }
            set { _IdProdProd = value; }
        }
        private decimal _Qtde;
        public decimal Qtde
        {
            get { return _Qtde; }
            set { _Qtde = value; }
        }

        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private int _IdMvProducao;
        public int IdMvProducao
        {
            get { return _IdMvProducao; }
            set { _IdMvProducao = value; }
        }

        private int _IdMvMatPrima;
        public int IdMvMatPrima
        {
            get { return _IdMvMatPrima; }
            set { _IdMvMatPrima = value; }
        }

        private int _Lote;
        public int Lote
        {
            get { return _Lote; }
            set { _Lote = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)        
        {
            IdMapa       = 0;
            Data         = DateTime.Now;
            IdProdProd   = 0;
            Qtde         = 0;
            Observacao   = "";
            Status       = 0;
            IdMvProducao = 0;
            IdMvMatPrima = 0;
            Lote         = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MapaProducao WHERE Id_Mapa=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdMapa       = Id;
                    Data         = DateTime.Parse(Tabela["Data"].ToString());
                    IdProdProd   = int.Parse(Tabela["Id_ProdProducao"].ToString());
                    Qtde         = decimal.Parse(Tabela["Qtde"].ToString());
                    Observacao   = Tabela["Observacao"].ToString().Trim();
                    Status       = int.Parse(Tabela["Status"].ToString());
                    IdMvProducao = int.Parse(Tabela["Id_MvProducao"].ToString());
                    IdMvMatPrima = int.Parse(Tabela["Id_MvMatPrima"].ToString());
                    Lote         = int.Parse(Tabela["Lote"].ToString());
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
                sSQL = "UPDATE MapaProducao SET Id_Mapa=@Id,Data=Convert(DateTime,@Data,103),Id_ProdProducao=@IdProdProd,Qtde=@Qtde,Observacao=@Observacao,Status=@Status,Id_MvProducao=@IdMvProducao,Id_MvMatPrima=@IdMvMatPrima,Lote=@Lote Where Id_Mapa=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdMapa);
            }
            else
            {
                IdMapa = Controle.ProximoID("MapaProducao");
                sSQL = "INSERT INTO MapaProducao (Id_Mapa,Data,Id_ProdProducao,Qtde,Observacao,Status,Id_MvProducao,Id_MvMatPrima,Lote) VALUES(@Id,Convert(DateTime,@Data,103),@IdProdProd,@Qtde,@Observacao,@Status,@IdMvProducao,@IdMvMatPrima,@Lote)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");           Vr_param.Add(IdMapa);
                Nm_param.Add("@Data");         Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdProdProd");   Vr_param.Add(IdProdProd);
                Nm_param.Add("@Qtde");         Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@Observacao");   Vr_param.Add(Observacao);
                Nm_param.Add("@Status");       Vr_param.Add(Status);
                Nm_param.Add("@IdMvProducao"); Vr_param.Add(IdMvProducao);
                Nm_param.Add("@IdMvMatPrima"); Vr_param.Add(IdMvMatPrima);
                Nm_param.Add("@Lote");         Vr_param.Add(Lote);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdMapa > 0)
            {
                Controle.ExecutaSQL("DELETE FROM MapaProducao WHERE Id_Mapa=" + IdMapa.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM MapaProducaoItens WHERE Id_Mapa=" + IdMapa.ToString().Trim());
            }

        }
    }
}

