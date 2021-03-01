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
    public class ProdutosKitItens
    {
        private int _IdItem;
        public int IdItem
        {
            get { return _IdItem; }
            set { _IdItem = value; }
        }
        private int _IdPrdMaster;
        public int IdPrdMaster
        {
            get { return _IdPrdMaster; }
            set { _IdPrdMaster = value; }
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

        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdItem      = 0;
            IdProduto   = 0;
            Qtde        = 0;
            Valor       = 0;
            IdPrdMaster = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ProdutosKit WHERE Id_Item=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {                    
                    Tabela.Read();
                    IdItem         = Id;
                    IdProduto      = int.Parse(Tabela["Id_Produto"].ToString());
                    IdPrdMaster    = int.Parse(Tabela["Id_PrdMaster"].ToString());
                    Qtde           = decimal.Parse(Tabela["Qtde"].ToString());
                    
                    if (Tabela["Valor"].ToString() != "")
                        Valor = decimal.Parse(Tabela["Valor"].ToString());
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdItem > 0)
            {
                sSQL = "UPDATE ProdutosKit SET Id_PrdMaster=@Id,Id_Item=@IdItem,Id_Produto=@IdProduto,Qtde=@Qtde,Valor=@Valor Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("PRODUTOSKIT");
                sSQL = "INSERT INTO ProdutosKit (Id_PrdMaster,Id_Item,Id_Produto,Qtde,Valor) VALUES(@Id,@IdItem,@IdProduto,@Qtde,@Valor)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdPrdMaster);
                Nm_param.Add("@IdItem"); Vr_param.Add(IdItem);
                Nm_param.Add("@IdProduto"); Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde"); Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@Valor"); Vr_param.Add(Controle.FloatToStr(Valor,2));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
                Controle.ExecutaSQL("DELETE FROM ProdutosKit WHERE Id_Item=" + IdItem.ToString().Trim());
        }        
    }
}

