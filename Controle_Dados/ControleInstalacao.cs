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
    public class ControleInstalacao
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
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private string _Servico;
        public string Servico
        {
            get { return _Servico; }
            set { _Servico = value; }
        }
        private DateTime _DtPrevista;
        public DateTime DtPrevista
        {
            get { return _DtPrevista; }
            set { _DtPrevista = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private DateTime _DtConcluido;
        public DateTime DtConcluido
        {
            get { return _DtConcluido; }
            set { _DtConcluido = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Endereco;
        public string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; }
        }
        private string _Contato;
        public string Contato
        {
            get { return _Contato; }
            set { _Contato = value; }
        }
        private string _Telefone;
        public string Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value; }
        }
        private int _QtdeEquip;
        public int QtdeEquip
        {
            get { return _QtdeEquip; }
            set { _QtdeEquip = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc = 0;
            Data = DateTime.Now;
            IdPessoa = 0;
            IdVenda = 0;
            IdVendedor = 0;
            Servico = "";            
            Observacao = "";            
            DtPrevista = DateTime.Now.AddDays(2);                        
            Status = 0;
            Endereco = "";
            Contato = "";
            Telefone = "";
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CONTROLEINSTALACAO WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    Data       = DateTime.Parse(Tabela["Data"].ToString());
                    IdPessoa   = int.Parse(Tabela["Id_Pessoa"].ToString());
                    IdVenda    = int.Parse(Tabela["Id_Venda"].ToString());
                    IdVendedor = int.Parse(Tabela["Id_Vendedor"].ToString());
                    Servico    = Tabela["Servico"].ToString().Trim();
                    DtPrevista = DateTime.Parse(Tabela["DtPrevista"].ToString());                    
                    Observacao = Tabela["Observacao"].ToString().Trim();
                    Endereco   = Tabela["Endereco"].ToString().Trim();
                    Contato    = Tabela["Contato"].ToString().Trim();
                    Telefone   = Tabela["Telefone"].ToString().Trim();
                    Status     = int.Parse(Tabela["Status"].ToString());
                    QtdeEquip  = int.Parse(Tabela["QtdeEquip"].ToString());
                    if (Status== 1)
                        DtConcluido = DateTime.Parse(Tabela["DtConcluido"].ToString());
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
                sSQL = "UPDATE CONTROLEINSTALACAO SET Id_Lanc=@Id,Data=Convert(DateTime,@Data,103),Id_Pessoa=@IdPessoa,Id_Venda=@IdVenda,Id_Vendedor=@IdVendedor,Servico=@Servico,DtPrevista=Convert(DateTime,@DtPrevista,103)," +
                       "Observacao=@Observacao,Status=@Status,Endereco=@Endereco,Contato=@Contato,Telefone=@Telefone,QtdeEquip=@QtdeEquip Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("INSTALACAO");
                sSQL = "INSERT INTO CONTROLEINSTALACAO (Id_Lanc,Data,Id_Pessoa,Id_Venda,Id_Vendedor,Servico,DtPrevista,Observacao,Status,Endereco,Contato,Telefone,QtdeEquip) " +
                       "VALUES (@Id,Convert(DateTime,@Data,103),@IdPessoa,@IdVenda,@IdVendedor,@Servico,Convert(DateTime,@DtPrevista,103),@Observacao,@Status,@Endereco,@Contato,@Telefone,@QtdeEquip)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);
                Nm_param.Add("@Data"); Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdPessoa"); Vr_param.Add(IdPessoa);
                Nm_param.Add("@IdVenda"); Vr_param.Add(IdVenda);
                Nm_param.Add("@IdVendedor"); Vr_param.Add(IdVendedor);
                Nm_param.Add("@Servico"); Vr_param.Add(Servico);
                Nm_param.Add("@DtPrevista"); Vr_param.Add(DtPrevista.ToShortDateString());
                Nm_param.Add("@Observacao"); Vr_param.Add(Observacao);                
                Nm_param.Add("@Status"); Vr_param.Add(Status);
                Nm_param.Add("@Endereco"); Vr_param.Add(Endereco);
                Nm_param.Add("@Contato"); Vr_param.Add(Contato);
                Nm_param.Add("@Telefone"); Vr_param.Add(Telefone);
                Nm_param.Add("@QtdeEquip"); Vr_param.Add(QtdeEquip);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM CONTROLEINSTALACAO WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }
        public void Concluir()
        {
            string sSQL = "UPDATE CONTROLEINSTALACAO Set Status=1,DtConcluido=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) Where Id_Lanc=" + IdLanc.ToString();
            Controle.ExecutaSQL(sSQL);
            Status = 1;
        }
        public void Cancelar()
        {
            string sSQL = "UPDATE CONTROLEINSTALACAO Set Status=2,DtConcluido=Null Where Id_Lanc=" + IdLanc.ToString();
            Controle.ExecutaSQL(sSQL);
            Status = 2;
        }
    }
}
