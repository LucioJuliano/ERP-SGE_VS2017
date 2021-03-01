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
    public class TabAuxFinanceiro
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdItem;
        public int IdItem
        {
            get { return _IdItem; }
            set { _IdItem = value; }
        }

        private int _IdCusto;
        public int IdCusto
        {
            get { return _IdCusto; }
            set { _IdCusto = value; }
        }

        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }

        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc   = 0;
            IdItem   = 0;
            IdCusto  = 0;
            IdFilial = 0;
            Valor    = 0;
            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM TabAuxFinanceiro WHERE Id_Item=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdItem   = Id;
                    IdLanc   = int.Parse(Tabela["Id_Lanc"].ToString());
                    IdCusto  = int.Parse(Tabela["Id_Custo"].ToString());
                    IdFilial = int.Parse(Tabela["Id_Filial"].ToString());                    
                    Valor    = decimal.Parse(Tabela["Valor"].ToString());                    
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdItem > 0)
            {
                sSQL = "UPDATE TabAuxFinanceiro SET Id_Item=@Id,Id_Lanc=@IdLanc,Id_Filial=@IdFilial,Valor=@Valor,Id_Custo=@IdCusto Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("TabAuxFinanceiro");
                sSQL = "INSERT INTO TabAuxFinanceiro (Id_Item,Id_Lanc,Id_Filial,Valor,Id_Custo) " +
                       " VALUES (@Id,@IdLanc,@IdFilial,@Valor,@IdCusto)";
            }
            if (sSQL != "")
            {
                //}
                Nm_param.Add("@Id");       Vr_param.Add(IdItem);
                Nm_param.Add("@IdLanc");   Vr_param.Add(IdLanc);
                Nm_param.Add("@IdFilial"); Vr_param.Add(IdFilial);
                Nm_param.Add("@IdCusto");  Vr_param.Add(IdCusto);
                Nm_param.Add("@Valor");    Vr_param.Add(Controle.FloatToStr(Valor, 2));                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdItem> 0)
            {
                Controle.ExecutaSQL("DELETE FROM TabAuxFinanceiro WHERE Id_Item=" + IdItem.ToString().Trim());
            }
        }
    }
}
