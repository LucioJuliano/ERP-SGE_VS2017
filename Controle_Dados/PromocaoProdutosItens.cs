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
    public class PromocaoProdutosItens
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdPromocao;
        public int IdPromocao
        {
            get { return _IdPromocao; }
            set { _IdPromocao = value; }
        }
        private int _IdProduto;
        public int IdProduto
        {
            get { return _IdProduto; }
            set { _IdProduto = value; }
        }

        private decimal _PrcVarejo;
        public decimal PrcVarejo
        {
            get { return _PrcVarejo; }
            set { _PrcVarejo = value; }
        }
        private decimal _PrcMinimo;
        public decimal PrcMinimo
        {
            get { return _PrcMinimo; }
            set { _PrcMinimo = value; }
        }
        private decimal _PrcAtacado;
        public decimal PrcAtacado
        {
            get { return _PrcAtacado; }
            set { _PrcAtacado = value; }
        }
        private decimal _PrcEspecial;
        public decimal PrcEspecial
        {
            get { return _PrcEspecial; }
            set { _PrcEspecial = value; }
        }
        private decimal _PrcSensacional;
        public decimal PrcSensacional
        {
            get { return _PrcSensacional; }
            set { _PrcSensacional = value; }
        }
        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc      = 0;
            IdPromocao  = 0;
            IdProduto   = 0;
            PrcVarejo   = 0;
            PrcMinimo   = 0;
            PrcAtacado  = 0;
            PrcEspecial = 0;
            PrcSensacional = 0;
            Ativo       = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PromocaoProdutosItens WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    IdPromocao  = int.Parse(Tabela["Id_Promocao"].ToString());
                    IdProduto   = int.Parse(Tabela["Id_Produto"].ToString());
                    PrcVarejo   = decimal.Parse(Tabela["PrcVarejo"].ToString());
                    PrcMinimo   = decimal.Parse(Tabela["PrcMinimo"].ToString());
                    PrcAtacado  = decimal.Parse(Tabela["PrcAtacado"].ToString());
                    PrcEspecial = decimal.Parse(Tabela["PrcEspecial"].ToString());
                    PrcSensacional = decimal.Parse(Tabela["PrcSensacional"].ToString());
                    Ativo       = int.Parse(Tabela["Ativo"].ToString());
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
                sSQL = "UPDATE PromocaoProdutosItens SET Id_Lanc=@Id,Id_Promocao=@IdPromocao,Id_Produto=@IdProduto," +
                       "PrcVarejo=@PrcVarejo,PrcMinimo=@PrcMinimo,PrcAtacado=@PrcAtacado,PrcEspecial=@PrcEspecial,PrcSensacional=@PrcSensacional,Ativo=@Ativo Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("PROMOCAOPRODUTOSITENS");
                sSQL = "INSERT INTO PromocaoProdutosItens (Id_Lanc,Id_Promocao,Id_Produto,PrcVarejo,PrcMinimo,PrcAtacado,PrcEspecial,PrcSensacional,Ativo) " +
                       " VALUES (@Id,@IdPromocao,@IdProduto,@PrcVarejo,@PrcMinimo,@PrcAtacado,@PrcEspecial,@PrcSensacional,@Ativo)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);
                Nm_param.Add("@IdPromocao");  Vr_param.Add(IdPromocao);
                Nm_param.Add("@IdProduto");   Vr_param.Add(IdProduto);
                Nm_param.Add("@PrcVarejo");   Vr_param.Add(Controle.FloatToStr(PrcVarejo, 2));
                Nm_param.Add("@PrcMinimo");   Vr_param.Add(Controle.FloatToStr(PrcMinimo, 2));
                Nm_param.Add("@PrcAtacado");  Vr_param.Add(Controle.FloatToStr(PrcAtacado, 2));
                Nm_param.Add("@PrcEspecial"); Vr_param.Add(Controle.FloatToStr(PrcEspecial, 2));
                Nm_param.Add("@PrcSensacional"); Vr_param.Add(Controle.FloatToStr(PrcSensacional, 2));                
                Nm_param.Add("@Ativo"); Vr_param.Add(Controle.FloatToStr(Ativo, 2));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM PromocaoProdutosItens WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }

    }
}

