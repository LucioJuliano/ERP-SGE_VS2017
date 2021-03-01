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
    public class GradeComodatoVinc
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdGrade;
        public int IdGrade
        {
            get { return _IdGrade; }
            set { _IdGrade = value; }
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
            IdLanc = 0;
            IdProduto = 0;
            IdGrade = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM GradeComodatoVinc WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    IdProduto = int.Parse(Tabela["Id_Produto"].ToString());
                    IdGrade = int.Parse(Tabela["Id_Grade"].ToString());

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
                sSQL = "UPDATE GradeComodatoVinc SET Id_Grade=@Id,Id_Lanc=@IdLanc,Id_Produto=@IdProduto Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("GradeComodatoVinc");
                sSQL = "INSERT INTO GradeComodatoVinc (Id_Grade,Id_Lanc,Id_Produto) VALUES(@Id,@IdLanc,@IdProduto)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdGrade);
                Nm_param.Add("@IdLanc"); Vr_param.Add(IdLanc);
                Nm_param.Add("@IdProduto"); Vr_param.Add(IdProduto);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM GradeComodatoVinc WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}
