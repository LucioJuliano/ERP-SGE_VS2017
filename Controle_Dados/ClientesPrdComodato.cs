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
    public class ClientesPrdComodato
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
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

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc    = 0;
            IdProduto = 0;
            Qtde      = 0;
            IdPessoa  = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ClientesPrdComodato WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    IdProduto = int.Parse(Tabela["Id_Produto"].ToString());
                    IdPessoa  = int.Parse(Tabela["Id_Pessoa"].ToString());
                    Qtde      = decimal.Parse(Tabela["Qtde"].ToString());
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
                sSQL = "UPDATE ClientesPrdComodato SET Id_Pessoa=@Id,Id_Lanc=@IdLanc,Id_Produto=@IdProduto,Qtde=@Qtde Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("CLIENTESPRDCOMODATO");
                sSQL = "INSERT INTO ClientesPrdComodato (Id_Pessoa,Id_Lanc,Id_Produto,Qtde) VALUES(@Id,@IdLanc,@IdProduto,@Qtde)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");        Vr_param.Add(IdPessoa);
                Nm_param.Add("@IdLanc");    Vr_param.Add(IdLanc);
                Nm_param.Add("@IdProduto"); Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");      Vr_param.Add(Controle.FloatToStr(Qtde));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM ClientesPrdComodato WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}

