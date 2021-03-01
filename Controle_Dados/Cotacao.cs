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
    public class Cotacao
    {
        private int _IdCotacao;
        public int IdCotacao
        {
            get { return _IdCotacao; }
            set { _IdCotacao = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _Tipo; //
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private string _Documento;
        public string Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        private string _Responsavel;
        public string Responsavel
        {
            get { return _Responsavel; }
            set { _Responsavel = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private int _NItens;
        public int NItens
        {
            get { return _NItens; }
            set { _NItens = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private DateTime _DataConclusao;
        public DateTime DataConclusao
        {
            get { return _DataConclusao; }
            set { _DataConclusao = value; }
        }
        private int _Concluido;
        public int Concluido
        {
            get { return _Concluido; }
            set { _Concluido = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Cotacao WHERE Id_Cotacao=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdCotacao = Id;
                    Data = DateTime.Parse(Tabela["Data"].ToString());
                    Tipo = int.Parse(Tabela["Tipo"].ToString());
                    Documento = Tabela["Documento"].ToString().Trim();
                    Responsavel = Tabela["Responsavel"].ToString().Trim();
                    Observacao = Tabela["Observacao"].ToString().Trim();
                    VlrTotal = decimal.Parse(Tabela["VlrTotal"].ToString());
                    NItens = int.Parse(Tabela["NItens"].ToString());
                    Status = int.Parse(Tabela["Status"].ToString());
                    Concluido = int.Parse(Tabela["Concluido"].ToString());
                    if (Tabela["DataConclusao"].ToString() != "")
                        DataConclusao = DateTime.Parse(Tabela["DataConclusao"].ToString());                  
                }
            }
            else
            {
                IdCotacao = 0;
                Data = DateTime.Now;
                Tipo = 0;
                Documento = "";
                Responsavel = "";
                Observacao = "";
                VlrTotal = 0;
                NItens = 0;
                Status = 0;
                Concluido = 0;
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdCotacao > 0)
            {
                sSQL = "UPDATE Cotacao SET Id_Cotacao=@Id,Data=Convert(DateTime,@Data,103),Responsavel=@Responsavel,Tipo=@Tipo,Documento=@Documento,Observacao=@Observacao,VlrTotal=@VlrTotal,NItens=@NItens,Status=@Status,Concluido=@Concluido Where Id_Cotacao=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdCotacao);
            }
            else
            {
                IdCotacao = Controle.ProximoID("Cotacao");
                sSQL = "INSERT INTO Cotacao (ID_Cotacao,Data,Responsavel,Tipo,Documento,Observacao,VlrTotal,NItens,Status,Concluido) VALUES(@Id,Convert(DateTime,@Data,103),@Responsavel,@Tipo,@Documento,@Observacao,@VlrTotal,@NItens,@Status,@Concluido)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdCotacao);
                Nm_param.Add("@Data"); Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@Tipo"); Vr_param.Add(Tipo);
                Nm_param.Add("@Documento"); Vr_param.Add(Documento);
                Nm_param.Add("@Responsavel"); Vr_param.Add(Responsavel);
                Nm_param.Add("@Observacao"); Vr_param.Add(Observacao);
                Nm_param.Add("@VlrTotal"); Vr_param.Add(Controle.FloatToStr(VlrTotal,2));
                Nm_param.Add("@NItens"); Vr_param.Add(NItens);
                Nm_param.Add("@Status"); Vr_param.Add(Status);
                Nm_param.Add("@Concluido"); Vr_param.Add(Concluido);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdCotacao > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Cotacao        WHERE Id_Cotacao=" + IdCotacao.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM CotacaoItens   WHERE Id_Cotacao=" + IdCotacao.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM CotacaoPessoas WHERE Id_Cotacao=" + IdCotacao.ToString().Trim());
            }
        }
        public void Concluir()
        {
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            string sSQL="UPDATE Cotacao Set Status=1,DataConclusao=Convert(DateTime,@Dt,103) Where Id_Cotacao="+IdCotacao.ToString();
            Nm_param.Add("@Dt"); Vr_param.Add(DateTime.Now.ToShortDateString());
            Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            Status = 1;
        }
        public void Cancelar()
        {
            Controle.ExecutaSQL("UPDATE Cotacao Set Status=0,DataConclusao=Null,Concluido=0,VlrTotal=0 Where Id_Cotacao=" + IdCotacao.ToString());
            Controle.ExecutaSQL("UPDATE CotacaoItens Set VlrUnitario=0,Id_Pessoa=0,VlrTotal=0 Where Id_Cotacao=" + IdCotacao.ToString());
            Status = 0;
            Concluido = 0;
        }
        public string SqlRelatorio(int IdCotacao)
        {            
            string TxtSql = "SELECT T1.Id_Cotacao, T1.Data, T1.Tipo, T1.Documento, T1.Responsavel, T1.Observacao, T1.VlrTotal, T1.NItens, T1.Status, T1.DataConclusao, T1.Concluido, " +
                            "       CASE T1.TIPO WHEN 0 THEN 'Menor Preço' WHEN 1 THEN 'Total Cotação' END AS TipoCotacao, T2.Id_Item, T2.Id_Produto, T2.Qtde, T2.VlrUnitario, " +
                            "       T2.VlrTotal AS TOTALITEM, T3.Referencia, T3.Descricao, T2.Id_Pessoa, T4.RazaoSocial " +
                            "   FROM Cotacao AS T1 "+
                            " LEFT OUTER JOIN  CotacaoItens AS T2 ON T2.Id_Cotacao = T1.Id_Cotacao "+
                            " LEFT OUTER JOIN  Produtos AS T3 ON T3.Id_Produto = T2.Id_Produto "+
                            " LEFT OUTER JOIN Pessoas AS T4 ON T4.Id_Pessoa = T2.Id_Pessoa "+
                            " Where t1.Id_Cotacao=" + IdCotacao.ToString();
            return TxtSql;
        }      
    }
}
