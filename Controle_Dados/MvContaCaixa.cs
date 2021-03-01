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
    public class MvContaCaixa
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
        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private int _TpLanc; //1-Debito / 2-Credito
        public int TpLanc
        {
            get { return _TpLanc; }
            set { _TpLanc = value; }
        }        
        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _IdLancOrig;
        public int IdLancOrig
        {
            get { return _IdLancOrig; }
            set { _IdLancOrig = value; }
        }        
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private int _IdCaixaDest;
        public int IdCaixaDest
        {
            get { return _IdCaixaDest; }
            set { _IdCaixaDest = value; }
        }
        private int _IdAgente;
        public int IdAgente
        {
            get { return _IdAgente; }
            set { _IdAgente = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int _IdTpDocDest;
        public int IdTpDocDest
        {
            get { return _IdTpDocDest; }
            set { _IdTpDocDest = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc      = 0;
            IdCaixa     = 0;
            Data        = DateTime.Now;
            IdPessoa    = 0;
            IdDocumento = 0;
            Descricao   = "";
            TpLanc      = 0;            
            Valor       = 0;
            Observacao  = "";
            IdLancOrig  = 0;
            IdUsuario   = 0;
            IdCaixaDest = 0;
            IdAgente    = 0;
            Status      = 0;
            IdTpDocDest = 0;
            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MVCONTACAIXA WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc = Id;
                    Data        = DateTime.Parse(Tabela["Data"].ToString());
                    IdCaixa     = int.Parse(Tabela["Id_Caixa"].ToString());
                    IdPessoa    = int.Parse(Tabela["Id_Pessoa"].ToString());
                    IdDocumento = int.Parse(Tabela["Id_Documento"].ToString());
                    Descricao   = Tabela["Descricao"].ToString().Trim();
                    TpLanc      = int.Parse(Tabela["TpLanc"].ToString());                    
                    Valor       = decimal.Parse(Tabela["Valor"].ToString());
                    Observacao  = Tabela["Observacao"].ToString().Trim();
                    IdLancOrig  = int.Parse(Tabela["Id_lancOrig"].ToString());
                    IdUsuario   = int.Parse(Tabela["Id_Usuario"].ToString());
                    IdCaixaDest = int.Parse(Tabela["Id_CaixaDest"].ToString());
                    IdTpDocDest = int.Parse(Tabela["Id_TpDocDest"].ToString());
                    IdAgente    = int.Parse(Tabela["Id_Agente"].ToString());
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
                sSQL = "UPDATE MVCONTACAIXA SET Id_Lanc=@Id,Id_caixa=@IdCaixa,Data=Convert(DateTime,@Data,103),Id_Pessoa=@IdPessoa,Id_Documento=@IdDocumento,Descricao=@Descricao,TpLanc=@TpLanc,Valor=@Valor,Observacao=@Observacao,Id_LancOrig=@IdLancOrig,Id_Usuario=@IdUsuario,Id_CaixaDest=@IdCaixaDest,Id_Agente=@IdAgente,Status=@Status,Id_TpDocDest=@IdTpDocDest Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("MVCONTACAIXA");
                sSQL = "INSERT INTO MVCONTACAIXA (Id_Lanc,Id_Caixa,Data,Id_Pessoa,Id_Documento,Descricao,TpLanc,Valor,Observacao,Id_LancOrig,Id_Usuario,Id_CaixaDest,Id_Agente,Status,Id_TpDocDest) " +
                       "VALUES (@Id,@IdCaixa,Convert(DateTime,@Data,103),@IdPessoa,@IdDocumento,@Descricao,@TpLanc,@Valor,@Observacao,@IdLancOrig,@IdUsuario,@IdCaixaDest,@IdAgente,@Status,@IdTpDocDest)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");          Vr_param.Add(IdLanc);
                Nm_param.Add("@IdCaixa");     Vr_param.Add(IdCaixa);
                Nm_param.Add("@Data");        Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdPessoa");    Vr_param.Add(IdPessoa);
                Nm_param.Add("@IdDocumento"); Vr_param.Add(IdDocumento);
                Nm_param.Add("@Descricao");   Vr_param.Add(Descricao);
                Nm_param.Add("@TpLanc");      Vr_param.Add(TpLanc);                
                Nm_param.Add("@Valor");       Vr_param.Add(Controle.FloatToStr(Valor, 2));
                Nm_param.Add("@Observacao");  Vr_param.Add(Observacao);
                Nm_param.Add("@IdLancOrig");  Vr_param.Add(IdLancOrig);
                Nm_param.Add("@IdUsuario");   Vr_param.Add(IdUsuario);
                Nm_param.Add("@IdCaixaDest"); Vr_param.Add(IdCaixaDest);
                Nm_param.Add("@IdTpDocDest"); Vr_param.Add(IdTpDocDest);
                Nm_param.Add("@IdAgente");    Vr_param.Add(IdAgente);
                Nm_param.Add("@Status");      Vr_param.Add(Status);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param); 
            }
        }
        public void Estorno()
        {
            if (IdLanc > 0)
            {
                Atlz_SaldoContaCaixa(TpLanc, IdCaixa, IdDocumento, Data, Valor);
            }
        }
        public int StatusLivroCxa(int IdCx,DateTime Dt)
        {
            SqlDataReader TabCx;
            TabCx = Controle.ConsultaSQL("SELECT * FROM SALDOCONTACAIXA WHERE  ID_CAIXA="+IdCx.ToString()+" AND DATA = CONVERT(DATETIME,'" + Dt.ToShortDateString() + "',103)");
            if (TabCx.HasRows)
            {
                TabCx.Read();
                if (TabCx["Status"].ToString() != "")
                    return int.Parse(TabCx["Status"].ToString());
                else
                    return 0;
            }
            return 0;

        }
        //
        public void Atlz_SaldoContaCaixa(int Tipo, int IdCxa, int IdDoc,DateTime Data, decimal VlrMov) //Mov L-Lançamento E-Estorno // Tipo 1-Debito 2-Credito
        {
            decimal SaldoAnterior = 0;
            // Verificando o Saldo Anterior

            SqlDataReader TabSldAnt;
            TabSldAnt = Controle.ConsultaSQL("SELECT * FROM SALDOCONTACAIXA WHERE Id_CAIXA=" + IdCxa.ToString() + " AND ID_DOCUMENTO=" + IdDoc.ToString() + " AND DATA < CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103) ORDER BY DATA DESC");
            if (TabSldAnt.HasRows)
            {
                TabSldAnt.Read();
                SaldoAnterior = decimal.Parse(TabSldAnt["Saldo"].ToString());
            }

            SqlDataReader TabSld;
            TabSld = Controle.ConsultaSQL("SELECT * FROM SALDOCONTACAIXA WHERE Id_CAIXA=" + IdCxa.ToString() + " AND ID_DOCUMENTO=" + IdDoc.ToString() + " AND DATA = CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)");
            if (TabSld.HasRows)
            {
                TabSld.Read();
                if (Tipo == 2)
                {
                    Controle.ExecutaSQL("UPDATE SALDOCONTACAIXA SET SALDOANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 2) + ",CREDITO=CREDITO+" + Controle.FloatToStr(VlrMov, 2) + ",SALDO=SALDO+" + Controle.FloatToStr(VlrMov, 2) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                    SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) + VlrMov;

                }
                else
                {
                    Controle.ExecutaSQL("UPDATE SALDOCONTACAIXA  SET SALDOANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 2) + ",DEBITO=DEBITO+" + Controle.FloatToStr(VlrMov, 2) + ",SALDO=SALDO-" + Controle.FloatToStr(VlrMov, 2) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                    SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) - VlrMov;
                }
            }
            else
            {
                int IdLanc = Controle.ProximoID("SaldoContaCaixa");
                if (Tipo == 2)
                {

                    Controle.ExecutaSQL("INSERT INTO SALDOCONTACAIXA (ID_LANC,ID_CAIXA,ID_DOCUMENTO,DATA,SALDOANTERIOR,CREDITO,DEBITO,SALDO,STATUS) VALUES (" + IdLanc.ToString() + "," + IdCxa.ToString() + "," + IdDoc.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 2) + "," + Controle.FloatToStr(VlrMov, 2) + ",0," + Controle.FloatToStr(SaldoAnterior + VlrMov, 2) + ",0)");
                    SaldoAnterior = SaldoAnterior + VlrMov;

                }
                else
                {
                        Controle.ExecutaSQL("INSERT INTO SALDOCONTACAIXA (ID_LANC,ID_CAIXA,ID_DOCUMENTO,DATA,SALDOANTERIOR,CREDITO,DEBITO,SALDO,STATUS) VALUES (" + IdLanc.ToString() + "," + IdCxa.ToString() + "," + IdDoc.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 2) + ",0," + Controle.FloatToStr(VlrMov, 3) + "," + Controle.FloatToStr(SaldoAnterior + (-1 * VlrMov), 3) + ",0)");
                        SaldoAnterior = SaldoAnterior - VlrMov;
                }
            }
            // Atualização dos Saldo nas Datas posteriores
            DataSet SldMov = new DataSet();
            SldMov = Controle.ConsultaTabela("SELECT * FROM SALDOCONTACAIXA WHERE Id_Caixa=" + IdCxa.ToString() + " AND ID_documento="+IdDoc.ToString()+" AND DATA > CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103) ORDER BY DATA");
            if (SldMov.Tables[0].Rows.Count > 0)
            {
                for (int I = 0; I <= SldMov.Tables[0].Rows.Count - 1; I++)
                {
                    Controle.ExecutaSQL("UPDATE SALDOCONTACAIXA SET SALDOANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",SALDO=" + Controle.FloatToStr(SaldoAnterior, 3) + "+(CREDITO-DEBITO) WHERE ID_LANC=" + SldMov.Tables[0].Rows[I]["Id_Lanc"].ToString());
                    SaldoAnterior = SaldoAnterior + (decimal.Parse(SldMov.Tables[0].Rows[I]["CREDITO"].ToString()) - decimal.Parse(SldMov.Tables[0].Rows[I]["DEBITO"].ToString()));
                }
            }
        }
    }
}
