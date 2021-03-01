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
    public class PagamentoMFE
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        private int _IdNota;
        public int IdNota
        {
            get { return _IdNota; }
            set { _IdNota = value; }
        }
        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        private int _NSU;
        public int NSU
        {
            get { return _NSU; }
            set { _NSU = value; }
        }
        private int _Autorizacao;
        public int Autorizacao
        {
            get { return _Autorizacao; }
            set { _Autorizacao = value; }
        }
        private int _IdPagMFE;
        public int IdPagMFE
        {
            get { return _IdPagMFE; }
            set { _IdPagMFE = value; }
        }
        private int _IdRespMFE;
        public int IdRespMFE
        {
            get { return _IdRespMFE; }
            set { _IdRespMFE = value; }
        }
        private int _NParc;
        public int NParc
        {
            get { return _NParc; }
            set { _NParc = value; }
        }

        private int _TpPag;
        public int TpPag
        {
            get { return _TpPag; }
            set { _TpPag = value; }
        }
        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc      = 0;
            IdVenda     = 0;
            IdDocumento = 0;
            IdNota      = 0;
            Valor       = 0;
            NSU         = 0;
            Autorizacao = 0;
            IdPagMFE    = 0;
            IdRespMFE   = 0;
            NParc       = 0;
            TpPag       = 0;


            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PagamentoMFE WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc      = Id;
                    IdVenda     = int.Parse(Tabela["ID_Venda"].ToString());
                    IdDocumento = int.Parse(Tabela["Id_Documento"].ToString());
                    IdNota      = int.Parse(Tabela["Id_Nota"].ToString());
                    Valor       = decimal.Parse(Tabela["Valor"].ToString());
                    NSU         = int.Parse(Tabela["NSU"].ToString());
                    Autorizacao = int.Parse(Tabela["Autorizacao"].ToString());
                    IdPagMFE    = int.Parse(Tabela["IdPagMFE"].ToString());
                    IdRespMFE   = int.Parse(Tabela["IdREspMFE"].ToString());
                    NParc       = int.Parse(Tabela["NParc"].ToString());
                    TpPag        = int.Parse(Tabela["TpPag"].ToString());
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
                sSQL = "UPDATE PagamentoMFE SET Id_Lanc=@Id,Id_Venda=@IdVenda,Id_Documento=@IdDocumento,Id_Nota=@IdNota,Valor=@Valor,NSU=@NSU,Autorizacao=@Autorizacao," +
                       "IDPagMFe=@IDPagMFe,IDRespMFe=@IDRespMFE,NParc=@NParc,TpPag=@TpPag Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("PagamentoMFE");
                sSQL = "INSERT INTO PagamentoMFE (Id_Lanc,Id_Venda,Id_Documento,Id_Nota,Valor,NSU,Autorizacao,IdPagMFe,IdRespMFe,NParc,TpPag) " +
                       " VALUES (@Id,@IdVenda,@IdDocumento,@IdNota,@Valor,@NSU,@Autorizacao,@IdPagMFe,@IdRespMFe,@NParc,@TpPag)";
            }
            if (sSQL != "")
            {
                //}
                Nm_param.Add("@Id");          Vr_param.Add(IdLanc);
                Nm_param.Add("@IdVenda");     Vr_param.Add(IdVenda);
                Nm_param.Add("@IdDocumento"); Vr_param.Add(IdDocumento);
                Nm_param.Add("@IdNota");      Vr_param.Add(IdNota);
                Nm_param.Add("@Valor");       Vr_param.Add(Controle.FloatToStr(Valor, 2));
                Nm_param.Add("@NSU");         Vr_param.Add(NSU);
                Nm_param.Add("@Autorizacao"); Vr_param.Add(Autorizacao);
                Nm_param.Add("@IdPagMFE");    Vr_param.Add(IdPagMFE);
                Nm_param.Add("@IdRespMFE");   Vr_param.Add(IdRespMFE);
                Nm_param.Add("@NParc");     Vr_param.Add(NParc);
                Nm_param.Add("@TpPag");       Vr_param.Add(TpPag);

                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM PagamentoMFE WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }
    }
}
