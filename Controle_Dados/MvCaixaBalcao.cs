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
    public class MvCaixaBalcao
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdCaixa;
        public int IdCaixa
        {
            get { return _IdCaixa; }
            set { _IdCaixa = value; }
        }
        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc      = 0;
            IdCaixa     = 0;
            Tipo        = 0;
            Descricao   = "";
            Valor       = 0;
            IdDocumento = 0;
            Status      = 0;            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MvCaixaBalcao WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc      = Id;                    
                    IdCaixa     = int.Parse(Tabela["Id_Caixa"].ToString());
                    Tipo        = int.Parse(Tabela["Tipo"].ToString());
                    Descricao   = Tabela["Descricao"].ToString();
                    Valor       = decimal.Parse(Tabela["Valor"].ToString());
                    IdDocumento = int.Parse(Tabela["Id_Documento"].ToString());
                    Status      = int.Parse(Tabela["Status"].ToString());
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
                sSQL = "UPDATE MvCaixaBalcao SET Id_Lanc=@Id,Id_Caixa=@IdCaixa,Tipo=@Tipo,Descricao=@Descricao,Valor=@Valor," +
                       "Id_Documento=@IdDocumento,Status=@Status Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("MVCAIXABALCAO");
                sSQL = "INSERT INTO MvCaixaBalcao (ID_Lanc,Id_Caixa,Tipo,Descricao,Valor,Id_Documento,Status) VALUES(@Id,@IdCaixa,@Tipo,@Descricao,@Valor,@IdDocumento,@Status)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);
                Nm_param.Add("@IdCaixa"); Vr_param.Add(IdCaixa);
                Nm_param.Add("@Tipo"); Vr_param.Add(Tipo);
                Nm_param.Add("@Descricao"); Vr_param.Add(Descricao);
                Nm_param.Add("@Valor"); Vr_param.Add(Controle.FloatToStr(Valor, 2));
                Nm_param.Add("@IdDocumento"); Vr_param.Add(IdDocumento);                
                Nm_param.Add("@Status"); Vr_param.Add(Status);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM MvCaixaBalcao WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }
    }
}
