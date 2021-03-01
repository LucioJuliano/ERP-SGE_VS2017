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
    public class ContaCaixa
    {
        private int _IdCaixa;
        public int IdCaixa
        {
            get { return _IdCaixa; }
            set { _IdCaixa = value; }
        }
        private string _Caixa;
        public string Caixa
        {
            get { return _Caixa; }
            set { _Caixa = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ContaCaixa WHERE Id_Caixa=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdCaixa = Id;
                    Caixa = Tabela["Caixa"].ToString().Trim();                    
                }
            }
            else
            {
                IdCaixa = 0;
                Caixa = "";             
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdCaixa > 0)
            {
                sSQL = "UPDATE ContaCaixa SET Id_Caixa=@Id,Caixa=@Caixa Where Id_Caixa=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdCaixa);
            }
            else
            {
                IdCaixa = Controle.ProximoID("ContaCaixa");
                sSQL = "INSERT INTO ContaCaixa (ID_Caixa,Caixa) VALUES(@Id,@Caixa)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdCaixa);
                Nm_param.Add("@Caixa"); Vr_param.Add(Caixa);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdCaixa > 0)
            {
                Controle.ExecutaSQL("DELETE FROM ContaCaixa WHERE Id_Caixa=" + IdCaixa.ToString().Trim());
            }
        }
    }
}
