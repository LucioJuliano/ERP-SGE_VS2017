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
    [Serializable()] 
    public class RamoAtividade
    {
        private int _IdAtividade;
        public int IdAtividade
        {
            get { return _IdAtividade; }
            set { _IdAtividade = value; }
        }

        private int _IdGrpAtividade;
        public int IdGrpAtividade
        {
            get { return _IdGrpAtividade; }
            set { _IdGrpAtividade = value; }
        }
        private string _Atividade;
        public string Atividade
        {
            get { return _Atividade; }
            set { _Atividade = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdAtividade    = 0;
            Atividade      = "";
            IdGrpAtividade = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM RamoAtividade WHERE Id_Atividade=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdAtividade = Id;
                    Atividade = Tabela["Atividade"].ToString().Trim();
                    IdGrpAtividade = int.Parse(Tabela["Id_GrpAtividade"].ToString().Trim());
                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdAtividade > 0)
            {
                sSQL = "UPDATE RAMOATIVIDADE SET Id_Atividade=@Id,ID_GrpAtividade=@IdGrpAtividade,Atividade=@Nm Where Id_Atividade=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdAtividade);
            }
            else
            {
                IdAtividade = Controle.ProximoID("Atividade");
                sSQL = "INSERT INTO RAMOATIVIDADE (ID_ATIVIDADE,Id_GrpAtividade,ATIVIDADE) VALUES(@Id,@IdGrpAtividade,@Nm)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");             Vr_param.Add(IdAtividade);
                Nm_param.Add("@IdGrpAtividade"); Vr_param.Add(IdGrpAtividade);
                Nm_param.Add("@Nm");             Vr_param.Add(Atividade);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdAtividade > 0)
            {
                Controle.ExecutaSQL("DELETE FROM RamoAtividade WHERE Id_Atividade=" + IdAtividade.ToString().Trim());
            }
        }

    }
}
