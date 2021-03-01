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
    public class CotacaoPessoas
    {
        private int _IdCotacao;
        public int IdCotacao
        {
            get { return _IdCotacao; }
            set { _IdCotacao = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private int _IdItem;
        public int IdItem
        {
            get { return _IdItem; }
            set { _IdItem = value; }
        }
        private decimal _VlrUnitario;
        public decimal VlrUnitario
        {
            get { return _VlrUnitario; }
            set { _VlrUnitario = value; }
        }
        public bool Incluir;

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CotacaoPessoas WHERE Id_Item=" + Id.ToString()+" and Id_Pessoa="+IdPessoa.ToString());
                if (Tabela.HasRows)
                {
                    Tabela.Read();                    
                    IdCotacao   = int.Parse(Tabela["Id_Cotacao"].ToString());
                    IdItem      = int.Parse(Tabela["Id_Item"].ToString());
                    IdPessoa    = int.Parse(Tabela["Id_Pessoa"].ToString());                    
                    VlrUnitario = decimal.Parse(Tabela["VlrUnitario"].ToString());                    
                }
            }
            else
            {
                IdItem      = 0;
                IdCotacao   = 0;                
                IdPessoa    = 0;                
                VlrUnitario = 0;                
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (!Incluir)
            {
                sSQL = "UPDATE CotacaoPessoas SET Id_Cotacao=@Id,Id_Item=@IdItem,Id_Pessoa=@IdPessoa,VlrUnitario=@VlrUnitario Where Id_Item=@Chave1 and Id_Pessoa=@Chave2";
                Nm_param.Add("@Chave1"); Vr_param.Add(IdItem);
                Nm_param.Add("@Chave2"); Vr_param.Add(IdPessoa);
            }
            else
            {                
                sSQL = "INSERT INTO CotacaoPessoas (ID_Cotacao,Id_Item,Id_Pessoa,VlrUnitario) VALUES(@Id,@IdItem,@IdPessoa,@VlrUnitario)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdCotacao);
                Nm_param.Add("@IdItem"); Vr_param.Add(IdItem);
                Nm_param.Add("@IdPessoa"); Vr_param.Add(IdPessoa);                
                Nm_param.Add("@VlrUnitario"); Vr_param.Add(Controle.FloatToStr(VlrUnitario,2));                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);                
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
            {                
                Controle.ExecutaSQL("DELETE FROM CotacaoPessoas WHERE Id_Pessoa=" + IdPessoa.ToString().Trim() + " and Id_Item=" + IdItem.ToString()+" and Id_Pessoa="+IdPessoa.ToString());
            }
        }
    }
}

