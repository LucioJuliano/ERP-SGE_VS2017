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
    public class GeneroProduto
    {
        private int _IdGenero;
        public int IdGenero
        {
            get { return _IdGenero; }
            set { _IdGenero = value; }
        }
        private string _Genero;
        public string Genero
        {
            get { return _Genero; }
            set { _Genero = value; }
        }
        
        public Funcoes Controle;
        public bool IncReg;
        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM GeneroProduto WHERE Id_Genero=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdGenero = Id;
                    Genero = Tabela["Genero"].ToString().Trim();                    
                }
            }
            else
            {
                IdGenero = 0;
                Genero = "";                
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (!IncReg)
            {
                sSQL = "UPDATE GENEROPRODUTO SET Id_Genero=@Id,Genero=@Genero Where Id_Genero=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdGenero);
            }
            else
            {                
                sSQL = "INSERT INTO GENEROPRODUTO (ID_GENERO,GENERO) VALUES(@Id,@Genero)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdGenero);
                Nm_param.Add("@Genero"); Vr_param.Add(Genero);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdGenero > 0)
            {
                Controle.ExecutaSQL("DELETE FROM GENEROPRODUTO WHERE Id_Genero=" + IdGenero.ToString().Trim());
            }
        }
    }
}
