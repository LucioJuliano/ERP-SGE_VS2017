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
    public class ComissaoPrdCliente
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
        private decimal _PComissao;
        public decimal PComissao
        {
            get { return _PComissao; }
            set { _PComissao = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc = 0;
            IdPessoa = 0;
            IdProduto = 0;
            PComissao = 0;
           
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ComissaoPrdCliente WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    IdProduto = int.Parse(Tabela["Id_Produto"].ToString());
                    IdPessoa = int.Parse(Tabela["Id_Pessoa"].ToString());
                    PComissao = decimal.Parse(Tabela["P_Comissao"].ToString());
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
                sSQL = "UPDATE ComissaoPrdCliente SET Id_Lanc=@Id,Id_Pessoa=@IdPessoa,Id_Produto=@IdProduto,P_Comissao=@PComissao Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("PRODUTOSKIT");
                sSQL = "INSERT INTO ComissaoPrdCliente (Id_Lanc,Id_Pessoa,Id_Produto,P_Comissao) VALUES(@Id,@IdPessoa,@IdProduto,@PComissao)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);
                Nm_param.Add("@IdPessoa"); Vr_param.Add(IdPessoa);
                Nm_param.Add("@IdProduto"); Vr_param.Add(IdProduto);
                Nm_param.Add("@PComissao"); Vr_param.Add(Controle.FloatToStr(PComissao));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM ComissaoPrdCliente  WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }        
    }
}
