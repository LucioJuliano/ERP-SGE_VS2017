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
    public class ProvDescFunc
    {
        private int _IdLanc;

        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdFunc;
        public int IdFunc
        {
            get { return _IdFunc; }
            set { _IdFunc = value; }
        }
        private string _MesAno;
        public string MesAno
        {
            get { return _MesAno; }
            set { _MesAno = value; }
        }
        private int _IdProvDesc;
        public int IdProvDesc
        {
            get { return _IdProvDesc; }
            set { _IdProvDesc = value; }
        }
        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }


        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc     = 0;
            IdFunc     = 0;
            MesAno     = "";
            IdProvDesc = 0;
            Valor      = 0;
            Descricao  = "";

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ProvDescFunc WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc     = Id;
                    IdFunc     = int.Parse(Tabela["Id_Func"].ToString());
                    MesAno     = Tabela["MesAno"].ToString().Trim();
                    IdProvDesc = int.Parse(Tabela["Id_ProvDesc"].ToString());
                    Valor      = decimal.Parse(Tabela["Valor"].ToString());
                    Descricao  = Tabela["Descricao"].ToString().Trim();

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
                sSQL = "UPDATE ProvDescFunc SET Id_Lanc=@Id,Id_Func=@IdFunc,MesAno=@MesAno,Id_ProvDesc=@IdProvDesc,Valor=@Valor,Descricao=@Descricao Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("ProvDescFunc");
                sSQL = "INSERT INTO ProvDescFunc (Id_Lanc,Id_Func,MesAno,Id_ProvDesc,Valor,Descricao) " +
                       " VALUES (@Id,@IdFunc,@MesAno,@IdProvDesc,@Valor,@Descricao)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");         Vr_param.Add(IdLanc);
                Nm_param.Add("@IdFunc");     Vr_param.Add(IdFunc);
                Nm_param.Add("@MesAno");     Vr_param.Add(MesAno);
                Nm_param.Add("@IdProvDesc"); Vr_param.Add(IdProvDesc);
                Nm_param.Add("@Valor");      Vr_param.Add(Controle.FloatToStr(Valor, 2));
                Nm_param.Add("@Descricao");  Vr_param.Add(Descricao);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM ProvDescFunc WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}
