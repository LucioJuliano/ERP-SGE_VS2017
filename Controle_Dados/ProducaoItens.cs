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
    public class ProducaoItens
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdProducao;
        public int IdProducao
        {
            get { return _IdProducao; }
            set { _IdProducao = value; }
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
        private int _Etapa;
        public int Etapa
        {
            get { return _Etapa; }
            set { _Etapa = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc      = 0;
            IdProduto   = 0;
            Qtde        = 0;
            IdProducao  = 0;
            Etapa       = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ProducaoItens WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {                    
                    Tabela.Read();
                    IdLanc         = Id;
                    IdProducao     = int.Parse(Tabela["Id_Producao"].ToString());
                    IdProduto      = int.Parse(Tabela["Id_Produto"].ToString());                    
                    Qtde           = decimal.Parse(Tabela["Qtde"].ToString());
                    Etapa          = int.Parse(Tabela["Etapa"].ToString());
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
                sSQL = "UPDATE ProducaoItens SET Id_Lanc=@Id,Id_Producao=@IdProducao,Id_Produto=@IdProduto,Qtde=@Qtde,Etapa=@Etapa Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("ProducaoItens");
                sSQL = "INSERT INTO ProducaoItens (Id_Lanc,Id_Producao,Id_Produto,Qtde,Etapa) VALUES(@Id,@IdProducao,@IdProduto,@Qtde,@Etapa)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");        Vr_param.Add(IdLanc);
                Nm_param.Add("@IdProducao");Vr_param.Add(IdProducao);
                Nm_param.Add("@IdProduto"); Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");      Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@Etapa");     Vr_param.Add(Etapa);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM ProducaoItens WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }        
    }
}

