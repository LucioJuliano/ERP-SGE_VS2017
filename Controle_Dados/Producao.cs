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
    public class Producao
    {
        private int _IdProducao;
        public int IdProducao
        {
            get { return _IdProducao; }
            set { _IdProducao = value; }
        }
        private string _Produto;
        public string Produto
        {
            get { return _Produto; }
            set { _Produto = value; }
        }
        private decimal _QtdeFabrica;
        public decimal QtdeFabrica
        {
            get { return _QtdeFabrica; }
            set { _QtdeFabrica = value; }
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
            IdProducao  = 0;
            Produto     = "";
            QtdeFabrica = 0;
            Observacao  = "";

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Producao WHERE Id_Producao=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdProducao  = Id;                    
                    Produto     = Tabela["Produto"].ToString().Trim();
                    QtdeFabrica = decimal.Parse(Tabela["QtdeFabrica"].ToString());
                    Observacao  = Tabela["Observacao"].ToString().Trim();
                    
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdProducao > 0)
            {
                sSQL = "UPDATE Producao SET Id_Producao=@Id,Produto=@Produto,QtdeFabrica=@QtdeFabrica,Observacao=@Observacao Where Id_Producao=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdProducao);
            }
            else
            {
                IdProducao = Controle.ProximoID("Producao");
                sSQL = "INSERT INTO Producao (Id_Producao,Produto,QtdeFabrica,Observacao) VALUES(@Id,@Produto,@QtdeFabrica,@Observacao)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");          Vr_param.Add(IdProducao);                
                Nm_param.Add("@Produto");     Vr_param.Add(Produto);
                Nm_param.Add("@QtdeFabrica"); Vr_param.Add(Controle.FloatToStr(QtdeFabrica));
                Nm_param.Add("@Observacao");  Vr_param.Add(Observacao);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdProducao > 0)
                Controle.ExecutaSQL("DELETE FROM Producao WHERE Id_Producao=" + IdProducao.ToString().Trim());
        }
    }
}

