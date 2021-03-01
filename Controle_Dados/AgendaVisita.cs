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
    public class AgendaVisita
    {
        private int _IdLanc;

        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private DateTime _DtVisita;
        public DateTime DtVisita
        {
            get { return _DtVisita; }
            set { _DtVisita = value; }
        }
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private string _Objetivo;
        public string Objetivo
        {
            get { return _Objetivo; }
            set { _Objetivo = value; }
        }
        private DateTime _DtRetorno;
        public DateTime DtRetorno
        {
            get { return _DtRetorno; }
            set { _DtRetorno = value; }
        }
        private string _Retorno;
        public string Retorno
        {
            get { return _Retorno; }
            set { _Retorno = value; }
        }
        private int _IdUsuLanc;
        public int IdUsuLanc
        {
            get { return _IdUsuLanc; }
            set { _IdUsuLanc = value; }
        }
        private int _IdUsuRet;
        public int IdUsuRet
        {
            get { return _IdUsuRet; }
            set { _IdUsuRet = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Cliente;
        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private int _IdVendVisita;
        public int IdVendVisita
        {
            get { return _IdVendVisita; }
            set { _IdVendVisita = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc     = 0;
            Data       = DateTime.Now;
            IdVendedor = 0;
            IdPessoa   = 0;
            DtVisita   = DateTime.Now;
            IdVenda    = 0;
            Objetivo   = "";
            Retorno    = "";
            IdUsuLanc  = 0;
            IdUsuRet   = 0;
            Status     = 0;
            Cliente    = "";
            IdVendVisita = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM AGENDAVISITA WHERE Id_LANC=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc     = Id;
                    Data       =  DateTime.Parse(Tabela["Data"].ToString().Trim());
                    IdVendedor = int.Parse(Tabela["Id_Vendedor"].ToString().Trim());
                    IdPessoa   = int.Parse(Tabela["Id_Pessoa"].ToString().Trim());
                    DtVisita   = DateTime.Parse(Tabela["DtVisita"].ToString().Trim());
                    IdVenda    = int.Parse(Tabela["Id_Venda"].ToString().Trim());
                    Objetivo   = Tabela["Objetivo"].ToString().Trim();
                    Retorno    = Tabela["Retorno"].ToString().Trim();
                    IdUsuLanc  = int.Parse(Tabela["Id_UsuLanc"].ToString().Trim());
                    IdUsuRet   = int.Parse(Tabela["Id_UsuRet"].ToString().Trim());
                    Status     = int.Parse(Tabela["Status"].ToString().Trim());
                    Cliente    = Tabela["Cliente"].ToString().Trim();

                    if (Tabela["Id_VendVisita"].ToString() != "")
                        IdVendVisita = int.Parse(Tabela["Id_VendVisita"].ToString());
                    
                    if (Tabela["DtRetorno"].ToString() != "")
                        DtRetorno = DateTime.Parse(Tabela["DtRetorno"].ToString());
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
                sSQL = "UPDATE AGENDAVISITA SET Id_LANC=@Id,Data=Convert(DateTime,@Data,103),Id_Vendedor=@IdVendedor,Id_Pessoa=@IdPessoa,DtVisita=Convert(DateTime,@DtVisita),"+
                       "Objetivo=@Objetivo,Id_UsuLanc=@IdUsuLanc,Cliente=@Cliente,Id_VendVisita=@IdVendVisita Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("AgendaVisita");
                sSQL = "INSERT INTO AGENDAVISITA (Id_LANC,DATA,ID_VENDEDOR,ID_PESSOA,DTVISITA,OBJETIVO,ID_USULANC,ID_VENDA,ID_USURET,STATUS,Cliente,Id_VendVisita) Values(@Id,Convert(DateTime,@Data,103),@IdVendedor,@IdPessoa,@DtVisita,@Objetivo,@IdUsuLanc,0,0,0,@Cliente,@IdVendVisita)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");           Vr_param.Add(IdLanc);
                Nm_param.Add("@Data");         Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdVendedor");   Vr_param.Add(IdVendedor);
                Nm_param.Add("@IdPessoa");     Vr_param.Add(IdPessoa);
                Nm_param.Add("@DtVisita");     Vr_param.Add(DtVisita);
                Nm_param.Add("@Objetivo");     Vr_param.Add(Objetivo);
                Nm_param.Add("@IdUsuLanc");    Vr_param.Add(IdUsuLanc);
                Nm_param.Add("@Cliente");      Vr_param.Add(Cliente);
                Nm_param.Add("@IdVendVisita"); Vr_param.Add(IdVendVisita);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM AGENDAVISITA WHERE ID_LANC=" + IdLanc.ToString().Trim());
            }
        }
    }
}
