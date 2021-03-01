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
    public class GrupoCusto
    {
        private int _IdGrpCusto;
        public int IdGrpCusto
        {
            get { return _IdGrpCusto; }
            set { _IdGrpCusto = value; }
        }
        private string _Grupo;
        public string Grupo
        {
            get { return _Grupo; }
            set { _Grupo = value; }
        }        
        private int _Tipo; // 0-Receita 1-Despesa
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdGrpCusto = 0;
            Grupo      = "";            
            Tipo       = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM GrupoCCusto WHERE Id_GrpCusto=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdGrpCusto = Id;
                    Grupo      = Tabela["Grupo"].ToString().Trim();
                    Tipo       = int.Parse(Tabela["Tipo"].ToString());
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdGrpCusto > 0)
            {
                sSQL = "UPDATE GrupoCCusto SET Id_GrpCusto=@Id,Grupo=@Nm,Tipo=@Tipo Where Id_GrpCusto=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdGrpCusto);
            }
            else
            {
                IdGrpCusto = Controle.ProximoID("GrupoCCusto");
                sSQL = "INSERT INTO GrupoCCusto (ID_GrpCusto,Grupo,Tipo) VALUES(@Id,@Nm,@Tipo)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdGrpCusto);
                Nm_param.Add("@Nm"); Vr_param.Add(Grupo);                
                Nm_param.Add("@Tipo"); Vr_param.Add(Tipo);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdGrpCusto > 0)
            {
                Controle.ExecutaSQL("DELETE FROM GrupoCCusto WHERE Id_GrpCusto=" + IdGrpCusto.ToString().Trim());
            }
        }
    }
}
