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
    public class MapaProducaoProdutos
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdMapa;
        public int IdMapa
        {
            get { return _IdMapa; }
            set { _IdMapa = value; }
        }
        private int _IdProduto;
        public int IdProduto
        {
            get { return _IdProduto; }
            set { _IdProduto = value; }
        }
        private decimal _Qtde;
        public decimal Qtde
        {
            get { return _Qtde; }
            set { _Qtde = value; }
        }
        private decimal _QtdeExtra;
        public decimal QtdeExtra
        {
            get { return _QtdeExtra; }
            set { _QtdeExtra = value; }
        }


        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc    = 0;
            IdMapa    = 0;
            IdProduto = 0;
            Qtde      = 0;
            QtdeExtra = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MapaProducaoProdutos WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc    = Id;
                    IdMapa    = int.Parse(Tabela["Id_Mapa"].ToString());
                    IdProduto = int.Parse(Tabela["Id_Produto"].ToString());
                    Qtde      = decimal.Parse(Tabela["Qtde"].ToString());
                    QtdeExtra = decimal.Parse(Tabela["QtdeExtra"].ToString());
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
                sSQL = "UPDATE MapaProducaoProdutos SET Id_Lanc=@Id,Id_Mapa=@IdMapa,Id_Produto=@IdProduto,Qtde=@Qtde,QtdeExtra=@QtdeExtra Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("MapaProducaoProdutos");
                sSQL = "INSERT INTO MapaProducaoProdutos (Id_Lanc,Id_Mapa,Id_Produto,Qtde,QtdeExtra) VALUES(@Id,@IdMapa,@IdProduto,@Qtde,@QtdeExtra)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");        Vr_param.Add(IdLanc);
                Nm_param.Add("@IdMapa");    Vr_param.Add(IdMapa);
                Nm_param.Add("@IdProduto"); Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");      Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@QtdeExtra"); Vr_param.Add(Controle.FloatToStr(QtdeExtra));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM MapaProducaoProdutos WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }

        }
    }
}

