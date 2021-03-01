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
    public class CotacaoItens
    {
        private int _IdCotacao;
        public int IdCotacao
        {
            get { return _IdCotacao; }
            set { _IdCotacao = value; }
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
        private decimal _VlrUnitario;
        public decimal VlrUnitario
        {
            get { return _VlrUnitario; }
            set { _VlrUnitario = value; }
        }
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CotacaoItens WHERE Id_Item=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdItem = Id;
                    IdCotacao = int.Parse(Tabela["Id_Cotacao"].ToString());
                    IdProduto = int.Parse(Tabela["Id_Produto"].ToString());
                    IdPessoa = int.Parse(Tabela["Id_Pessoa"].ToString());
                    Qtde = decimal.Parse(Tabela["Qtde"].ToString());
                    VlrUnitario = decimal.Parse(Tabela["VlrUnitario"].ToString());
                    VlrTotal = decimal.Parse(Tabela["VlrTotal"].ToString());                    
                }
            }
            else
            {
                IdItem = 0;
                IdCotacao = 0;
                IdProduto = 0;
                IdPessoa = 0;
                Qtde = 0;
                VlrUnitario = 0;
                VlrTotal = 0;                    
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdItem > 0)
            {
                sSQL = "UPDATE CotacaoItens SET Id_Cotacao=@Id,Id_Item=@IdItem,Id_Produto=@IdProduto,Qtde=@Qtde,VlrUnitario=@VlrUnitario,VlrTotal=@VlrTotal,Id_Pessoa=@IdPessoa Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("CotacaoItens");
                sSQL = "INSERT INTO CotacaoItens (ID_Cotacao,Id_Item,Id_Produto,Qtde,VlrUnitario,VlrTotal,Id_Pessoa) VALUES(@Id,@IdItem,@IdProduto,@Qtde,@VlrUnitario,@VlrTotal,@IdPessoa)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdCotacao);
                Nm_param.Add("@IdItem"); Vr_param.Add(IdItem);
                Nm_param.Add("@IdProduto"); Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde"); Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@VlrUnitario"); Vr_param.Add(Controle.FloatToStr(VlrUnitario,2));
                Nm_param.Add("@VlrTotal"); Vr_param.Add(Controle.FloatToStr(VlrUnitario*Qtde,2));
                Nm_param.Add("@IdPessoa"); Vr_param.Add(IdPessoa);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
            {
                Controle.ExecutaSQL("DELETE FROM CotacaoItens   WHERE Id_Item=" + IdItem.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM CotacaoPessoas WHERE Id_Cotacao=" + IdCotacao.ToString().Trim() + " and Id_Item=" + IdItem.ToString());
            }
        }
    }
}
