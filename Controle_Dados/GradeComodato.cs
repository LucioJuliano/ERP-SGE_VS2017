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
    public class GradeComodato
    {
        private int _IdGrade;
        public int IdGrade
        {
            get { return _IdGrade; }
            set { _IdGrade = value; }
        }
        private string _Grade;
        public string Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        private int _Qtde;
        public int Qtde
        {
            get { return _Qtde; }
            set { _Qtde = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdGrade = 0;
            Grade   = "";
            Qtde    = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM GradeComodato WHERE Id_Grade=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdGrade = Id;
                    Grade   = Tabela["Grade"].ToString().Trim();
                    Qtde    = int.Parse(Tabela["Qtde"].ToString());

                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdGrade > 0)
            {
                sSQL = "UPDATE GradeComodato SET Id_Grade=@Id,Grade=@Grade,Qtde=@Qtde Where Id_Grade=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdGrade);
            }
            else
            {
                IdGrade = Controle.ProximoID("GRADECOMODATO");
                sSQL = "INSERT INTO GradeComodato (Id_Grade,Grade,Qtde) VALUES(@Id,@Grade,@Qtde)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");    Vr_param.Add(IdGrade);
                Nm_param.Add("@Grade"); Vr_param.Add(Grade);
                Nm_param.Add("@Qtde");  Vr_param.Add(Qtde);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdGrade > 0)
                Controle.ExecutaSQL("DELETE FROM GradeComodato WHERE Id_Grade=" + IdGrade.ToString().Trim());
        }
    }
}
