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
    public class BalancoItens
    {
        private int _IdBalanco;
        public int IdBalanco
        {
            get { return _IdBalanco; }
            set { _IdBalanco = value; }
        }
        private int _IdItem;
        public int IdItem
        {
            get { return _IdItem; }
            set { _IdItem = value; }
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
        private decimal _SaldoEstoque;
        public decimal SaldoEstoque
        {
            get { return _SaldoEstoque; }
            set { _SaldoEstoque = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdBalanco    = 0;
            IdItem       = 0;
            IdProduto    = 0;
            Qtde         = 0;
            SaldoEstoque = 0;
            Observacao   = "";
            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM BALANCOITENS WHERE ID_ITEM=" + Id.ToString().Trim());

                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdItem       = Id;
                    IdBalanco    = int.Parse(Tabela["ID_BALANCO"].ToString());
                    IdProduto    = int.Parse(Tabela["Id_PRODUTO"].ToString());
                    Qtde         = decimal.Parse(Tabela["Qtde"].ToString());
                    SaldoEstoque = decimal.Parse(Tabela["SaldoEstoque"].ToString());
                    Observacao   = Tabela["Observacao"].ToString().Trim();
                    
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
                sSQL = "UPDATE BALANCOITENS SET ID_ITEM=@Id,Id_BALANCO=@IdBalanco,ID_PRODUTO=@IDPRODUTO,QTDE=@QTDE,SALDOESTOQUE=@SALDOESTOQUE,OBSERVACAO=@OBSERVACAO Where Id_ITEM=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("BALANCOITENS");
                sSQL = "INSERT INTO BALANCOITENS (ID_ITEM,Id_BALANCO,ID_PRODUTO,QTDE,SALDOESTOQUE,OBSERVACAO) Values(@Id,@IdBalanco,@IdProduto,@Qtde,@SaldoEstoque,@OBSERVACAO)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");           Vr_param.Add(IdItem);
                Nm_param.Add("@IdBalanco");    Vr_param.Add(IdBalanco);
                Nm_param.Add("@IdProduto");    Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");         Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@SaldoEstoque"); Vr_param.Add(Controle.FloatToStr(SaldoEstoque));
                Nm_param.Add("@OBSERVACAO");   Vr_param.Add(Observacao);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
            {
                Controle.ExecutaSQL("DELETE FROM BALANCOITENS WHERE Id_Item=" + IdItem.ToString().Trim());
            }
        }
    }
}
