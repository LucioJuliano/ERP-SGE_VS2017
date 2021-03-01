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
    public class Departamentos
    {
        private int _IdDepartamento;
        public int IdDepartamento
        {
            get { return _IdDepartamento; }
            set { _IdDepartamento = value; }
        }
        private string _Departamento;
        public string Departamento
        {
            get { return _Departamento; }
            set { _Departamento = value; }
        }
        private string _Responsavel;
        public string Responsavel
        {
            get { return _Responsavel; }
            set { _Responsavel = value; }
        }
        private int _Ramal;
        public int Ramal
        {
            get { return _Ramal; }
            set { _Ramal = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Departamentos WHERE Id_Departamento=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdDepartamento = Id;
                    Departamento = Tabela["Departamento"].ToString().Trim();
                    Responsavel = Tabela["Responsavel"].ToString().Trim();
                    Ramal = int.Parse(Tabela["Ramal"].ToString());
                }
            }
            else
            {
                IdDepartamento = 0;
                Departamento = "";
                Responsavel = "";
                Ramal = 0;
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdDepartamento > 0)
            {
                sSQL = "UPDATE DEPARTAMENTOS SET Id_Departamento=@Id,Departamento=@Departamento,Responsavel=@Responsavel,Ramal=@Ramal Where Id_Departamento=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdDepartamento);
            }
            else
            {
                IdDepartamento = Controle.ProximoID("DEPARTAMENTOS");
                sSQL = "INSERT INTO DEPARTAMENTOS (ID_DEPARTAMENTO,DEPARTAMENTO,RESPONSAVEL,RAMAL) VALUES(@Id,@Departamento,@Responsavel,@Ramal)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdDepartamento);
                Nm_param.Add("@Departamento"); Vr_param.Add(Departamento);
                Nm_param.Add("@Responsavel"); Vr_param.Add(Responsavel);
                Nm_param.Add("@Ramal"); Vr_param.Add(Ramal);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdDepartamento > 0)
            {
                Controle.ExecutaSQL("DELETE FROM DEPARTAMENTOS WHERE Id_Departamento=" + IdDepartamento.ToString().Trim());
            }
        }
    }
}
