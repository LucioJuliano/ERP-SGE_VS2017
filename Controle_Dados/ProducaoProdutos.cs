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
    public class ProducaoProdutos
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
        
        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc     = 0;
            IdProduto  = 0;
            IdProducao = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ProducaoProdutos WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    IdProducao = int.Parse(Tabela["Id_Producao"].ToString());
                    IdProduto  = int.Parse(Tabela["Id_Produto"].ToString());                    
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
                sSQL = "UPDATE ProducaoProdutos SET Id_Lanc=@Id,Id_Producao=@IdProducao,Id_Produto=@IdProduto Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("ProducaoProdutos");
                sSQL = "INSERT INTO ProducaoProdutos (Id_Lanc,Id_Producao,Id_Produto) VALUES(@Id,@IdProducao,@IdProduto)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");         Vr_param.Add(IdLanc);
                Nm_param.Add("@IdProducao"); Vr_param.Add(IdProducao);
                Nm_param.Add("@IdProduto");  Vr_param.Add(IdProduto);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM ProducaoProdutos WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}

