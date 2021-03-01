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
    public class ReducaoFiscal
    {
        private int _IdReducao;
        public int IdReducao
        {
            get { return _IdReducao; }
            set { _IdReducao = value; }
        }
        private string _CodRed;
        public string CodRed
        {
            get { return _CodRed; }
            set { _CodRed = value; }
        }
        private string _RefReducao;
        public string RefReducao
        {
            get { return _RefReducao; }
            set { _RefReducao = value; }
        }
        private decimal _Perc;
        public decimal Perc
        {
            get { return _Perc; }
            set { _Perc = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdReducao  = 0;
            CodRed     = "";
            RefReducao = "";
            Perc       = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ReducaoFiscal WHERE Id_Reducao=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdReducao  = Id;
                    CodRed     = Tabela["CodRed"].ToString().Trim();
                    RefReducao = Tabela["RefReducao"].ToString().Trim();
                    Perc       = decimal.Parse(Tabela["Perc"].ToString());
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdReducao > 0)
            {
                sSQL = "UPDATE ReducaoFiscal SET Id_Reducao=@Id,CodRed=@CodRed,RefReducao=@RefReducao,Perc=@Perc Where Id_Reducao=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdReducao);
            }
            else
            {
                IdReducao = Controle.ProximoID("ReducaoFiscal");
                sSQL = "INSERT INTO ReducaoFiscal (ID_Reducao,CodRed,RefReducao,Perc) VALUES(@Id,@CodRed,@RefReducao,@Perc)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");         Vr_param.Add(IdReducao);
                Nm_param.Add("@CodRed");     Vr_param.Add(CodRed);
                Nm_param.Add("@RefReducao"); Vr_param.Add(RefReducao);
                Nm_param.Add("@Perc");       Vr_param.Add(Controle.FloatToStr(Perc, 2));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdReducao > 0)
            {
                Controle.ExecutaSQL("DELETE FROM ReducaoFiscal WHERE Id_Reducao=" + IdReducao.ToString().Trim());
            }
        }
    }
}
