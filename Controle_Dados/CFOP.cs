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
    public class CFOP
    {
        private int _IdCfop;
        public int IdCfop
        {
            get { return _IdCfop; }
            set { _IdCfop = value; }
        }
        private string _Cfop;
        public string Cfop
        {
            get { return _Cfop; }
            set { _Cfop = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private string _Naturaza;
        public string Naturaza
        {
            get { return _Naturaza; }
            set { _Naturaza = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CFOP WHERE Id_Cfop=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdCfop    = Id;
                    Cfop      = Tabela["Cfop"].ToString().Trim();
                    Descricao = Tabela["Descricao"].ToString().Trim();
                    Naturaza  = Tabela["Natureza"].ToString().Trim();
                }
            }
            else
            {
                IdCfop    = 0;
                Cfop      = "";
                Descricao = "";
                Naturaza  = "";
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdCfop > 0)
            {
                sSQL = "UPDATE CFOP SET Id_Cfop=@Id,Cfop=@Cfop,Descricao=@Descricao,Natureza=@Natureza Where Id_Cfop=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdCfop);
            }
            else
            {
                IdCfop = Controle.ProximoID("CFOP");
                sSQL = "INSERT INTO CFOP (ID_CFOP,CFOP,DESCRICAO,NATUREZA) VALUES(@Id,@Cfop,@Descricao,@Natureza)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdCfop);
                Nm_param.Add("@Cfop"); Vr_param.Add(Cfop);
                Nm_param.Add("@Descricao"); Vr_param.Add(Descricao);
                Nm_param.Add("@Natureza"); Vr_param.Add(Naturaza);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdCfop > 0)
            {
                Controle.ExecutaSQL("DELETE FROM CFOP WHERE Id_Cfop=" + IdCfop.ToString().Trim());
            }
        }
    }
}
