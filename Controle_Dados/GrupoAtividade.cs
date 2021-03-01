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
    public class GrupoAtividade
    {
        private int _IdGrpAtividade;
        public int IdGrpAtividade
        {
            get { return _IdGrpAtividade; }
            set { _IdGrpAtividade = value; }
        }
        private string _Grupo;
        public string Grupo
        {
            get { return _Grupo; }
            set { _Grupo = value; }
        }
        

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdGrpAtividade = 0;
            Grupo = "";
            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM GrupoAtividade WHERE Id_GrpAtividade=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdGrpAtividade = Id;
                    Grupo = Tabela["Grupo"].ToString().Trim();
                    
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdGrpAtividade > 0)
            {
                sSQL = "UPDATE GrupoAtividade SET Id_GrpAtividade=@Id,Grupo=@Nm Where Id_GrpAtividade=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdGrpAtividade);
            }
            else
            {
                IdGrpAtividade = Controle.ProximoID("GrupoAtividade");
                sSQL = "INSERT INTO GrupoAtividade (ID_GrpAtividade,Grupo) VALUES(@Id,@Nm)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdGrpAtividade);
                Nm_param.Add("@Nm"); Vr_param.Add(Grupo);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdGrpAtividade > 0)
            {
                Controle.ExecutaSQL("DELETE FROM GrupoAtividade WHERE Id_GrpAtividade=" + IdGrpAtividade.ToString().Trim());
            }
        }
    }
}
