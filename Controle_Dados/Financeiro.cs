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
    public class Financeiro
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private DateTime _DataLanc;
        public DateTime DataLanc
        {
            get { return _DataLanc; }
            set { _DataLanc = value; }
        }
        private int _PagRec;  // 1-Pagar 2-Receber
        public int PagRec
        {
            get { return _PagRec; }
            set { _PagRec = value; }
        }
        private int _IdMov;
        public int IdMov
        {
            get { return _IdMov; }
            set { _IdMov = value; }
        }
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private int _IdTipoDocumento;
        public int IdTipoDocumento
        {
            get { return _IdTipoDocumento; }
            set { _IdTipoDocumento = value; }
        }
        private int _IdCusto;
        public int IdCusto
        {
            get { return _IdCusto; }
            set { _IdCusto = value; }
        }
        private int _IdDepartamento;
        public int IdDepartamento
        {
            get { return _IdDepartamento; }
            set { _IdDepartamento = value; }
        }
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private int _IdFormaPgto;
        public int IdFormaPgto
        {
            get { return _IdFormaPgto; }
            set { _IdFormaPgto = value; }
        }
        private int _IdBanco;
        public int IdBanco
        {
            get { return _IdBanco; }
            set { _IdBanco = value; }
        }
        private int _IdCaixa;
        public int IdCaixa
        {
            get { return _IdCaixa; }
            set { _IdCaixa = value; }
        }
        private string _NumDoc;
        public string NumDoc
        {
            get { return _NumDoc; }
            set { _NumDoc = value; }
        }
        private string _NotaFiscal;
        public string NotaFiscal
        {
            get { return _NotaFiscal; }
            set { _NotaFiscal = value; }
        }
        private string _Referente;
        public string Referente
        {
            get { return _Referente; }
            set { _Referente = value; }
        }       
        private DateTime _Vencimento;
        public DateTime Vencimento
        {
            get { return _Vencimento; }
            set { _Vencimento = value; }
        }
        private decimal _VlrOriginal;
        public decimal VlrOriginal
        {
            get { return _VlrOriginal; }
            set { _VlrOriginal = value; }
        }
        private decimal _VlrAtual;
        public decimal VlrAtual
        {
            get { return _VlrAtual; }
            set { _VlrAtual = value; }
        }
        private DateTime _DtBaixa;
        public DateTime DtBaixa
        {
            get { return _DtBaixa; }
            set { _DtBaixa = value; }
        }
        private decimal _VlrJuro;
        public decimal VlrJuro
        {
            get { return _VlrJuro; }
            set { _VlrJuro = value; }
        }
        private decimal _VlrMulta;
        public decimal VlrMulta
        {
            get { return _VlrMulta; }
            set { _VlrMulta = value; }
        }
        private decimal _VlrDesconto;
        public decimal VlrDesconto
        {
            get { return _VlrDesconto; }
            set { _VlrDesconto = value; }
        }
        private decimal _VlrBaixa;
        public decimal VlrBaixa
        {
            get { return _VlrBaixa; }
            set { _VlrBaixa = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _IdLancOrigem;
        public int IdLancOrigem
        {
            get { return _IdLancOrigem; }
            set { _IdLancOrigem = value; }
        }
        private string LinhaDigBoleto = "";
        private string CodBarraBoleto = "";
        private string NossoNumero = "";
        private int _IdUsuLanc;
        public int IdUsuLanc
        {
            get { return _IdUsuLanc; }
            set { _IdUsuLanc = value; }
        }
        private int _IdUsuBaixa;
        public int IdUsuBaixa
        {
            get { return _IdUsuBaixa; }
            set { _IdUsuBaixa = value; }
        }
        private int _IdAgente;
        public int IdAgente
        {
            get { return _IdAgente; }
            set { _IdAgente = value; }
        }
        private int _IdCaixaPgto;
        public int IdCaixaPgto
        {
            get { return _IdCaixaPgto; }
            set { _IdCaixaPgto = value; }
        }
        private int _IdLancServ;
        public int IdLancServ
        {
            get { return _IdLancServ; }
            set { _IdLancServ = value; }
        }
        private int _Inativa;
        public int Inativa
        {
            get { return _Inativa; }
            set { _Inativa = value; }
        }
        private int _DocRec;
        public int DocRec
        {
            get { return _DocRec; }
            set { _DocRec = value; }
        }

        private int _DespForn;
        public int DespForn
        {
            get { return _DespForn; }
            set { _DespForn = value; }
        }
        private int _NSUCartao;
        public int NSUCartao
        {
            get { return _NSUCartao; }
            set { _NSUCartao = value; }
        }
        private int _IDPagMFe;
        public int IDPagMFe
        {
            get { return _IDPagMFe; }
            set { _IDPagMFe = value; }
        }
        private int _IDRespMFe;
        public int IDRespMFe
        {
            get { return _IDRespMFe; }
            set { _IDRespMFe = value; }
        }

        private int _Perdido;
        public int Perdido
        {
            get { return _Perdido; }
            set { _Perdido = value; }
        }       

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc          = 0;
            DataLanc        = DateTime.Now;
            PagRec          = 0;
            IdMov           = 0;
            IdVenda         = 0;
            IdPessoa        = 0;
            IdFilial        = 0;
            IdTipoDocumento = 0;
            IdCusto         = 0;
            IdDepartamento  = 0;
            IdVendedor      = 0;
            IdFormaPgto     = 0;
            IdBanco         = 0;
            IdCaixa         = 0;
            NumDoc          = "";
            NotaFiscal      = "";
            Referente       = "";
            Vencimento      = DateTime.Now;
            VlrOriginal     = 0;
            VlrAtual        = 0;
            VlrJuro         = 0;
            VlrMulta        = 0;
            VlrDesconto     = 0;
            VlrBaixa        = 0;
            Status          = 0;
            Observacao      = "";
            IdLancOrigem    = 0;
            IdUsuLanc       = 0;
            IdAgente        = 0;
            IdCaixaPgto     = 0;
            IdLancServ      = 0;
            Inativa         = 0;
            DocRec          = 0;
            DespForn        = 0;
            Perdido         = 0;
            NSUCartao       = 0;
            IDPagMFe        = 0;
            IDRespMFe       = 0;            

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM LancFinanceiro WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc          = Id;
                    DataLanc        = DateTime.Parse(Tabela["DataLanc"].ToString().Trim());
                    PagRec          = int.Parse(Tabela["PagRec"].ToString());
                    IdMov           = int.Parse(Tabela["Id_Mov"].ToString());
                    IdVenda         = int.Parse(Tabela["Id_Venda"].ToString());
                    IdPessoa        = int.Parse(Tabela["Id_Pessoa"].ToString());
                    IdFilial        = int.Parse(Tabela["Id_Filial"].ToString());
                    IdTipoDocumento = int.Parse(Tabela["Id_TipoDocumento"].ToString());
                    IdCusto         = int.Parse(Tabela["Id_Custo"].ToString());
                    IdDepartamento  = int.Parse(Tabela["Id_Departamento"].ToString());
                    IdVendedor      = int.Parse(Tabela["Id_Vendedor"].ToString());
                    IdFormaPgto     = int.Parse(Tabela["Id_FormaPgto"].ToString());
                    IdBanco         = int.Parse(Tabela["Id_Banco"].ToString());
                    IdCaixa         = int.Parse(Tabela["Id_Caixa"].ToString());
                    NumDoc          = Tabela["NumDocumento"].ToString();
                    NotaFiscal      = Tabela["NotaFiscal"].ToString();
                    Referente       = Tabela["Referente"].ToString();                    
                    Vencimento      = DateTime.Parse(Tabela["Vencimento"].ToString().Trim());
                    VlrOriginal     = decimal.Parse(Tabela["VlrOriginal"].ToString());
                    VlrAtual        = decimal.Parse(Tabela["VlrAtual"].ToString());
                    VlrJuro         = decimal.Parse(Tabela["VlrJuro"].ToString());
                    VlrMulta        = decimal.Parse(Tabela["VlrMulta"].ToString());
                    VlrDesconto     = decimal.Parse(Tabela["VlrDesconto"].ToString());
                    VlrBaixa        = decimal.Parse(Tabela["VlrBaixa"].ToString());
                    Status          = int.Parse(Tabela["Status"].ToString());
                    Observacao      = Tabela["Observacao"].ToString().Trim();
                    IdLancOrigem    = int.Parse(Tabela["Id_LancOrigem"].ToString());                    
                    if (Tabela["DtBaixa"].ToString() != "")
                        DtBaixa = DateTime.Parse(Tabela["DtBaixa"].ToString().Trim());
                    if (Tabela["Id_Agente"].ToString() != "")
                        IdAgente = int.Parse(Tabela["Id_Agente"].ToString().Trim());
                    if (Tabela["Id_CaixaPgto"].ToString() != "")
                        IdCaixaPgto = int.Parse(Tabela["Id_CaixaPgto"].ToString().Trim());
                    if (Tabela["IdLancServ"].ToString() != "")
                        IdLancServ = int.Parse(Tabela["IdLancServ"].ToString().Trim());
                    if (Tabela["Inativa"].ToString() != "")
                        Inativa = int.Parse(Tabela["Inativa"].ToString().Trim());
                    if (Tabela["DocRec"].ToString() != "")
                        DocRec = int.Parse(Tabela["DocRec"].ToString().Trim());
                    if (Tabela["DespForn"].ToString() != "")
                        DespForn = int.Parse(Tabela["DespForn"].ToString().Trim());
                    if (Tabela["Perdido"].ToString() != "")
                        Perdido = int.Parse(Tabela["Perdido"].ToString().Trim());
                    if (Tabela["NSU"].ToString() != "")
                        NSUCartao = int.Parse(Tabela["NSU"].ToString().Trim());
                    if (Tabela["IDPagMFe"].ToString() != "")
                        IDPagMFe = int.Parse(Tabela["IDPagMFe"].ToString().Trim());
                    if (Tabela["IDRespMFe"].ToString() != "")
                        IDRespMFe = int.Parse(Tabela["IDRespMFe"].ToString().Trim());                                       
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
                sSQL = "UPDATE LancFinanceiro SET Id_Lanc=@Id,DataLanc=Convert(DateTime,@DataLanc,103),PagRec=@PagRec,Id_Mov=@IdMov,Id_Venda=@IdVenda,Id_Pessoa=@IdPessoa,Id_Filial=@IdFilial," +
                       "Id_TipoDocumento=@IdTipoDocumento,Id_Custo=@IdCusto,Id_Departamento=@IdDepartamento,Id_Vendedor=@IdVendedor,Id_FormaPgto=@IdFormaPgto,Id_Banco=@IdBanco,Id_Caixa=@IdCaixa,NumDocumento=@NumDocumento," +
                       "NotaFiscal=@NotaFiscal,Referente=@Referente,Vencimento=Convert(DateTime,@Vencimento,103),VlrOriginal=@VlrOriginal,VlrAtual=@VlrAtual,Observacao=@Observacao,Status=@Status,Id_LancOrigem=@IdLancOrigem,"+
                       "CodBarraBoleto=@CodBarraBoleto,LinhaBoleto=@LinhaBoleto,NossoNumero=@NossoNumero,Id_UsuLanc=@IdUsulanc,Id_Agente=@IdAgente,Id_CaixaPgto=@IdCaixaPgto,IdLancServ=@IdLancServ,Inativa=@Inativa,DocRec=@DocRec,"+
                       "DespForn=@DespForn,Perdido=@Perdido,NSU=@NSUCartao,IDPagMFe=@IDPagMFe,IDRespMFe=@IDRespMFE Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("Financeiro");
                sSQL = "INSERT INTO LancFinanceiro (Id_Lanc,DataLanc,PagRec,Id_Mov,Id_Venda,Id_Pessoa,Id_Filial,Id_TipoDocumento,Id_Custo,Id_Departamento,Id_Vendedor,Id_FormaPgto,Id_Banco,Id_Caixa,NumDocumento," +
                       "NotaFiscal,Referente,Vencimento,VlrOriginal,VlrAtual,VlrJuro,VlrMulta,VlrDesconto,VlrBaixa,Observacao,Status,Id_LancOrigem,CodBarraBoleto,LinhaBoleto,NossoNumero,Id_UsuLanc,Id_UsuBaixa,"+
                       "Id_Agente,Id_CaixaPgto,IdLancServ,Inativa,DocRec,DespForn,Perdido,NSU,IDPagMFe,IDRespMFe) " +
                       " VALUES (@Id,Convert(DateTime,@DataLanc,103),@PagRec,@IdMov,@IdVenda,@IdPessoa,@IdFilial,@IdTipoDocumento,@IdCusto,@IdDepartamento,@IdVendedor,@IdFormaPgto,@IdBanco,@IdCaixa,@NumDocumento,@NotaFiscal,@Referente,"+
                       "Convert(DateTime,@Vencimento,103),@VlrOriginal,@VlrAtual,0,0,0,0,@Observacao,@Status,@IdLancOrigem,@CodBarraBoleto,@LinhaBoleto,@NossoNumero,@IdUsuLanc,0,@IdAgente,@IdCaixaPgto,@IdLancServ,@Inativa,@DocRec,@DespForn,@Perdido,@NSUCartao,@IDPagMFe,@IDRespMFe)";                
            }
            if (sSQL != "")
            {
                //if (IdFormaPgto == 8)
                //    DadosBoleto();
                //else
                // {
                CodBarraBoleto = "";
                LinhaDigBoleto = "";
                NossoNumero    = "";
                //}
                Nm_param.Add("@Id");              Vr_param.Add(IdLanc);
                Nm_param.Add("@DataLanc");        Vr_param.Add(DataLanc.ToShortDateString());
                Nm_param.Add("@PagRec");          Vr_param.Add(PagRec);
                Nm_param.Add("@IdMov");           Vr_param.Add(IdMov);
                Nm_param.Add("@IdVenda");         Vr_param.Add(IdVenda);
                Nm_param.Add("@IdPessoa");        Vr_param.Add(IdPessoa);
                Nm_param.Add("@IdFilial");        Vr_param.Add(IdFilial);
                Nm_param.Add("@IdTipoDocumento"); Vr_param.Add(IdTipoDocumento);
                Nm_param.Add("@IdCusto");         Vr_param.Add(IdCusto);
                Nm_param.Add("@IdDepartamento");  Vr_param.Add(IdDepartamento);
                Nm_param.Add("@IdVendedor");      Vr_param.Add(IdVendedor);
                Nm_param.Add("@IdFormaPgto");     Vr_param.Add(IdFormaPgto);
                Nm_param.Add("@IdBanco");         Vr_param.Add(IdBanco);
                Nm_param.Add("@IdCaixa");         Vr_param.Add(IdCaixa);
                Nm_param.Add("@NumDocumento");    Vr_param.Add(NumDoc);
                Nm_param.Add("@NotaFiscal");      Vr_param.Add(NotaFiscal);                
                Nm_param.Add("@Referente");       Vr_param.Add(Referente);                
                Nm_param.Add("@Vencimento");      Vr_param.Add(Vencimento.ToShortDateString());
                Nm_param.Add("@VlrOriginal");     Vr_param.Add(Controle.FloatToStr(VlrOriginal, 2));
                Nm_param.Add("@VlrAtual");        Vr_param.Add(Controle.FloatToStr(VlrAtual,2));
                Nm_param.Add("@Observacao");      Vr_param.Add(Observacao);
                Nm_param.Add("@Status");          Vr_param.Add(Status);
                Nm_param.Add("@IdLancOrigem");    Vr_param.Add(IdLancOrigem);
                Nm_param.Add("@CodBarraBoleto");  Vr_param.Add(CodBarraBoleto);
                Nm_param.Add("@LinhaBoleto");     Vr_param.Add(LinhaDigBoleto);
                Nm_param.Add("@NossoNUmero");     Vr_param.Add(NossoNumero);
                Nm_param.Add("@IdUsuLanc");       Vr_param.Add(IdUsuLanc);
                Nm_param.Add("@IdAgente");        Vr_param.Add(IdAgente);
                Nm_param.Add("@IdCaixaPgto");     Vr_param.Add(IdCaixaPgto);
                Nm_param.Add("@IdLancServ");      Vr_param.Add(IdLancServ);
                Nm_param.Add("@Inativa");         Vr_param.Add(Inativa);
                Nm_param.Add("@DocRec");          Vr_param.Add(DocRec);
                Nm_param.Add("@DespForn");        Vr_param.Add(DespForn);
                Nm_param.Add("@Perdido");         Vr_param.Add(Perdido);
                Nm_param.Add("@NSUCartao");       Vr_param.Add(NSUCartao);
                Nm_param.Add("@IDPagMFe");        Vr_param.Add(IDPagMFe);
                Nm_param.Add("@IDRespMFe");       Vr_param.Add(IDRespMFe);               
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM LancFinanceiro WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }
        public void Baixar()
        {
            if (IdLanc > 0)
            {
                ArrayList Nm_param = new ArrayList();
                ArrayList Vr_param = new ArrayList();
                string sSQL = "UPDATE LancFinanceiro Set Id_Caixa=@IdCaixa,Id_TipoDocumento=@IdTipoDoc,Id_UsuBaixa=@IdUsuBaixa,Status=1,DtBaixa=Convert(DateTime,@Dt,103),VlrJuro=@VlrJuro,VlrMulta=@VlrMulta,VlrDesconto=@VlrDesc,VlrBaixa=@VlrBaixa Where Id_Lanc=" + IdLanc.ToString();
                Nm_param.Add("@Dt");         Vr_param.Add(DtBaixa.ToShortDateString());
                Nm_param.Add("@VlrJuro");    Vr_param.Add(Controle.FloatToStr(VlrJuro, 2));
                Nm_param.Add("@VlrMulta");   Vr_param.Add(Controle.FloatToStr(VlrMulta, 2));
                Nm_param.Add("@VlrDesc");    Vr_param.Add(Controle.FloatToStr(VlrDesconto, 2));
                Nm_param.Add("@VlrBaixa");   Vr_param.Add(Controle.FloatToStr(VlrBaixa, 2));
                Nm_param.Add("@IdUsuBaixa"); Vr_param.Add(IdUsuBaixa);
                Nm_param.Add("@IdCaixa");    Vr_param.Add(IdCaixa);
                Nm_param.Add("@IdTipoDoc");  Vr_param.Add(IdTipoDocumento);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
                Status = 1;

                // Lançamento no Movimento do Livro Caixa
                if (IdCaixa > 0 && IdTipoDocumento >0)
                {
                    MvContaCaixa MvContaCx = new MvContaCaixa();
                    MvContaCx.Controle     = Controle;
                    MvContaCx.IdLanc       = 0;
                    MvContaCx.IdCaixa      = IdCaixa;
                    MvContaCx.IdAgente     = IdAgente;
                    MvContaCx.Data         = DtBaixa;
                    MvContaCx.IdDocumento  = IdTipoDocumento;
                    MvContaCx.IdPessoa     = IdPessoa;
                    MvContaCx.Descricao    = "Doc:" + NumDoc.Trim();
                    MvContaCx.IdLancOrig   = IdLanc;
                    MvContaCx.IdUsuario    = IdUsuBaixa;
                    MvContaCx.Valor        = VlrBaixa;
                    MvContaCx.Observacao   = Referente;
                    
                    if (PagRec == 1) MvContaCx.TpLanc = 1; else MvContaCx.TpLanc = 2;
                    MvContaCx.GravarDados();
                    MvContaCx.Atlz_SaldoContaCaixa(MvContaCx.TpLanc, IdCaixa, IdTipoDocumento, DtBaixa, VlrBaixa);
                }
            }
        }
        public void CancelarBaixa()
        {
            if (IdLanc > 0)
            {                
                string sSQL = "UPDATE LancFinanceiro Set Id_UsuBaixa=0,Status=0,DtBaixa=Null,VlrJuro=0,VlrMulta=0,VlrDesconto=0,VlrBaixa=0 Where Id_Lanc=" + IdLanc.ToString();
                Controle.ExecutaSQL(sSQL);
                // Estorno no Movimento do Livro Caixa
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MvContaCaixa WHERE Id_LancOrig=" + IdLanc.ToString().Trim());
                if (Tabela.HasRows)
                {
                    MvContaCaixa MvContaCx = new MvContaCaixa();
                    MvContaCx.Controle     = Controle;
                    MvContaCx.IdLanc       = 0;
                    MvContaCx.IdCaixa      = IdCaixa;
                    MvContaCx.IdAgente     = IdAgente;
                    MvContaCx.Data         = DtBaixa;
                    MvContaCx.IdDocumento  = IdTipoDocumento;
                    MvContaCx.IdPessoa     = IdPessoa;
                    MvContaCx.Descricao    = "ESTORNO <-> " + "Doc:" + NumDoc.Trim();
                    MvContaCx.IdLancOrig   = IdLanc;
                    MvContaCx.IdUsuario    = IdUsuBaixa;
                    MvContaCx.Valor        = VlrBaixa;
                    MvContaCx.Observacao   = Referente;
                    if (PagRec == 1) MvContaCx.TpLanc = 2; else MvContaCx.TpLanc = 1;
                    MvContaCx.GravarDados();
                    MvContaCx.Estorno();
                }
                //--------------------------------------------
                Status      = 0;
                VlrMulta    = 0;
                VlrJuro     = 0;
                VlrDesconto = 0;
                VlrBaixa    = 0;
            }
        }
        private void DadosBoleto()
        {
            CodBarraBoleto = " ";
            LinhaDigBoleto = " ";
            NossoNumero    = " ";
            Filiais Filial = new Filiais();
            Filial.Controle = Controle;
            Filial.LerDados(IdFilial);
            if (Filial.IdFilial > 0)
            {
                Bancos Banco = new Bancos();
                Banco.Controle = Controle;
                Banco.LerDados(Filial.IdBanco);
                if (Banco.IdBanco > 0)
                {
                    Boleto bolBB = new BoletoBrasil();
                    bolBB.Aceite         = false;
                    bolBB.CedenteAgencia = Banco.NumAgencia;
                    bolBB.CedenteConta   = Banco.Conta;
                    bolBB.CedenteContaDV = Banco.DigConta.ToString();
                    bolBB.CedenteNome    = Filial.Filial;
                    bolBB.Contrato       = int.Parse(Filial.Convenio);
                    bolBB.Carteira       = 18;
                    bolBB.Sequencial     = IdLanc;
                    bolBB.DtVencimento   = Vencimento;
                    bolBB.Valor          = float.Parse(VlrOriginal.ToString());
                    string lNossoNumero, lLinhaDigitavel, lCodigoBarras;
                    bolBB.MontaCodigos(out lNossoNumero, out lLinhaDigitavel, out lCodigoBarras);
                    LinhaDigBoleto = lLinhaDigitavel;
                    CodBarraBoleto = lCodigoBarras;
                    NossoNumero    = lNossoNumero;
                    IdBanco        = Banco.IdBanco;
                }
            }
        }       
    }
}
