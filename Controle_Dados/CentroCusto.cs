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
    public class CentroCusto
    {
        private int _IdCusto;
        public int IdCusto
        {
            get { return _IdCusto; }
            set { _IdCusto = value; }
        }
        private string _Custo;
        public string Custo
        {
            get { return _Custo; }
            set { _Custo = value; }
        }
        private int _IdGrpCusto; 
        public int IdGrpCusto
        {
            get { return _IdGrpCusto; }
            set { _IdGrpCusto = value; }
        }      

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdCusto    = 0;
            Custo      = "";
            IdGrpCusto = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CentroCusto WHERE Id_Custo=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdCusto    = Id;
                    Custo      = Tabela["Custo"].ToString().Trim();
                    IdGrpCusto = int.Parse(Tabela["Id_GrpCusto"].ToString());
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdCusto > 0)
            {
                sSQL = "UPDATE CentroCusto SET Id_Custo=@Id,Custo=@Nm,Id_GrpCusto=@IdGrpCusto Where Id_Custo=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdCusto);
            }
            else
            {
                IdCusto = Controle.ProximoID("CentroCusto");
                sSQL = "INSERT INTO CentroCusto (ID_Custo,Custo,Id_GrpCusto) VALUES(@Id,@Nm,@IdGrpCusto)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdCusto);
                Nm_param.Add("@Nm"); Vr_param.Add(Custo);
                Nm_param.Add("@IdGrpCusto"); Vr_param.Add(IdGrpCusto);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdCusto > 0)
            {
                Controle.ExecutaSQL("DELETE FROM CentroCusto WHERE Id_Custo=" + IdCusto.ToString().Trim());
            }
        }
    }
}
