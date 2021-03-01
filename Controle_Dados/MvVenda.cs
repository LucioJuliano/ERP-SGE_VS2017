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
    public class MvVenda
    {
        private int _IdVenda;

        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _IdCaixa;
        public int IdCaixa
        {
            get { return _IdCaixa; }
            set { _IdCaixa = value; }
        }
        private int _ImpNF;
        public int ImpNF
        {
            get { return _ImpNF; }
            set { _ImpNF = value; }
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
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private int _IdRota;
        public int IdRota
        {
            get { return _IdRota; }
            set { _IdRota = value; }
        }
        private int _IdFormaPgto;
        public int IdFormaPgto
        {
            get { return _IdFormaPgto; }
            set { _IdFormaPgto = value; }
        }
        private string _PrazoPgto;
        public string PrazoPgto
        {
            get { return _PrazoPgto; }
            set { _PrazoPgto = value; }
        }
        private string _TpVenda;
        public string TpVenda
        {
            get { return _TpVenda; }
            set { _TpVenda = value; }
        }
        private string _NumDocumento;
        public string NumDocumento
        {
            get { return _NumDocumento; }
            set { _NumDocumento = value; }
        }
        private int _IdEntregador;
        public int IdEntregador
        {
            get { return _IdEntregador; }
            set { _IdEntregador = value; }
        }
        private DateTime _PrevEntrega;
        public DateTime PrevEntrega
        {
            get { return _PrevEntrega; }
            set { _PrevEntrega = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _Status; // 0-Em Aberto 1-Confirmado 2-Faturado 3-Entregue 4-Cancelado
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private DateTime _DataCancel;
        public DateTime DataCancel
        {
            get { return _DataCancel; }
            set { _DataCancel = value; }
        }
        private decimal _VlrSubTotal;
        public decimal VlrSubTotal
        {
            get { return _VlrSubTotal; }
            set { _VlrSubTotal = value; }
        }
        private decimal _VlrDesconto;
        public decimal VlrDesconto
        {
            get { return _VlrDesconto; }
            set { _VlrDesconto = value; }
        }
        private decimal _VlrCredito;
        public decimal VlrCredito
        {
            get { return _VlrCredito; }
            set { _VlrCredito = value; }
        }
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private DateTime _DtHrLanc;
        public DateTime DtHrLanc
        {
            get { return _DtHrLanc; }
            set { _DtHrLanc = value; }
        }
        //Itens do Endereço da Pessoa
        private string _CnpjCpf;
        public string CnpjCpf
        {
            get { return _CnpjCpf; }
            set { _CnpjCpf = value; }
        }
        private string _NmPessoa;
        public string NmPessoa
        {
            get { return _NmPessoa; }
            set { _NmPessoa = value; }
        }
        private string _InscUF;
        public string InscUF
        {
            get { return _InscUF; }
            set { _InscUF = value; }
        }
        private string _Cep;
        public string Cep
        {
            get { return _Cep; }
            set { _Cep = value; }
        }
        private string _Endereco;
        public string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; }
        }
        private string _Numero;
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        private string _Complemento;
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }
        private string _Cidade;
        public string Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }
        private string _Bairro;
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        private int _IdUF;
        public int IdUF
        {
            get { return _IdUF; }
            set { _IdUF = value; }
        }
        private string _Fone;
        public string Fone
        {
            get { return _Fone; }
            set { _Fone = value; }
        }
        private int _IdVdMaster;
        public int IdVdMaster
        {
            get { return _IdVdMaster; }
            set { _IdVdMaster = value; }
        }
        private string _VinculoVd;
        public string VinculoVd
        {
            get { return _VinculoVd; }
            set { _VinculoVd = value; }
        }
        private string _FormNF;
        public string FormNF
        {
            get { return _FormNF; }
            set { _FormNF = value; }
        }
        private int _IdUsuAutDeb;
        public int IdUsuAutDeb
        {
            get { return _IdUsuAutDeb; }
            set { _IdUsuAutDeb = value; }
        }
        private DateTime _DataConfirmacao;
        public DateTime DataConfirmacao
        {
            get { return _DataConfirmacao; }
            set { _DataConfirmacao = value; }
        }
        private int _IdUltUsuario;
        public int IdUltUsuario
        {
            get { return _IdUltUsuario; }
            set { _IdUltUsuario = value; }
        }
        private int _Faturado;
        public int Faturado
        {
            get { return _Faturado; }
            set { _Faturado = value; }
        }
        private int _IdVdTroca;
        public int IdVdTroca
        {
            get { return _IdVdTroca; }
            set { _IdVdTroca = value; }
        }
        private int _VbBalcao;
        public int VdBalcao
        {
            get { return _VbBalcao; }
            set { _VbBalcao = value; }
        }
        private int _IdVdMatriz;
        public int IdVdMatriz
        {
            get { return _IdVdMatriz; }
            set { _IdVdMatriz = value; }
        }
        private int _SemMovEst;
        public int SemMovEst
        {
            get { return _SemMovEst; }
            set { _SemMovEst = value; }
        }
        private int _FrenteLoja;
        public int FrenteLoja
        {
            get { return _FrenteLoja; }
            set { _FrenteLoja = value; }
        }
        private decimal _VlrLiberado;
        public decimal VlrLiberado
        {
            get { return _VlrLiberado; }
            set { _VlrLiberado = value; }
        }
        private string _Pais;
        public string Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }
        private int _IdLancCF;
        public int IdLancCF
        {
            get { return _IdLancCF; }
            set { _IdLancCF = value; }
        }

        private int _VdImpFat;
        public int VdImpFat
        {
            get { return _VdImpFat; }
            set { _VdImpFat = value; }
        }

        private int _CliReativado;
        public int CliReativado
        {
            get { return _CliReativado; }
            set { _CliReativado = value; }
        }

        private int _IdFilialEntrega;
        public int IdFilialEntrega
        {
            get { return _IdFilialEntrega; }
            set { _IdFilialEntrega = value; }
        }

        private int _IdVdOrigem;
        public int IdVdOrigem
        {
            get { return _IdVdOrigem; }
            set { _IdVdOrigem = value; }
        }

        private int _IdFilialOrigem;
        public int IdFilialOrigem
        {
            get { return _IdFilialOrigem; }
            set { _IdFilialOrigem = value; }
        }

        private int _IdVdDestino;
        public int IdVdDestino
        {
            get { return _IdVdDestino; }
            set { _IdVdDestino = value; }
        }

        private string _ObsCancelamento;
        public string ObsCancelamento
        {
            get { return _ObsCancelamento; }
            set { _ObsCancelamento = value; }
        }

        private int _IgnoraDesc; //Ignora o desconto no calculo da comissao
        public int IgnoraDesc
        {
            get { return _IgnoraDesc; }
            set { _IgnoraDesc = value; }
        }

        private int _IdUsuLibParc; //Ignora o desconto no calculo da comissao
        public int IdUsuLibParc
        {
            get { return _IdUsuLibParc; }
            set { _IdUsuLibParc = value; }
        }

        
        private DateTime _DtEnvioRec; //Ignora o desconto no calculo da comissao
        public DateTime DtEnvioRec
        {
            get { return _DtEnvioRec; }
            set { _DtEnvioRec = value; }
        }

        private string _NumPedido; //Ignora o desconto no calculo da comissao
        public string NumPedido
        {
            get { return _NumPedido; }
            set { _NumPedido = value; }
        }
        private int _IdUsuboleto;
        public int IdUsuboleto
        {
            get { return _IdUsuboleto; }
            set { _IdUsuboleto = value; }
        }

        private int _IdUsuLibClieNv;
        public int IdUsuLibClieNv
        {
            get { return _IdUsuLibClieNv; }
            set { _IdUsuLibClieNv = value; }
        }
        

        public Funcoes Controle;

        public void LerDados(int Id)
        {            
            IdVenda         = 0;
            Data            = DateTime.Now;
            ImpNF           = 1;
            IdCaixa         = 0;
            IdPessoa        = 0;
            IdFilial        = 0;
            IdVendedor      = 0;
            IdUsuario       = 0;
            IdRota          = 0;
            IdFormaPgto     = 0;
            PrazoPgto       = "";
            TpVenda         = "";
            NumDocumento    = "";
            IdEntregador    = 0;
            Observacao      = "";
            FormNF          = "";
            Status          = 0;
            VlrSubTotal     = 0;
            VlrDesconto     = 0;
            VlrCredito      = 0;
            VlrTotal        = 0;
            CnpjCpf         = "";
            NmPessoa        = "";
            InscUF          = "";
            Cep             = "";
            Endereco        = "";
            Complemento     = "";
            Cidade          = "";
            Bairro          = "";
            Numero          = "";
            IdUF            = 0;
            Fone            = "";
            VinculoVd       = "";
            IdVdMaster      = 0;
            IdVdTroca       = 0;
            IdUsuAutDeb     = 0;
            PrevEntrega     = DateTime.Now;
            DtHrLanc        = DateTime.Now;
            IdUltUsuario    = 0;
            Faturado        = 0;
            VdBalcao        = 0;
            IdVdMatriz      = 0;
            SemMovEst       = 0;
            FrenteLoja      = 0;
            VlrLiberado     = 0;
            Pais            = "1058";
            IdLancCF        = 0;
            VdImpFat        = 0;
            CliReativado    = 0;
            IdFilialEntrega = 0;
            IdVdOrigem      = 0;
            IdFilialOrigem  = 0;
            IdVdDestino     = 0;
            IgnoraDesc      = 0;
            ObsCancelamento = "";
            IdUsuLibParc    = 0;
            NumPedido       = "";
            IdUsuboleto     = 0;
            IdUsuLibClieNv  = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Mvvenda WHERE Id_Venda=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdVenda      = Id;
                    Data         = DateTime.Parse(Tabela["Data"].ToString());
                    ImpNF        = int.Parse(Tabela["ImpNF"].ToString());
                    IdCaixa      = int.Parse(Tabela["Id_Caixa"].ToString());
                    IdPessoa     = int.Parse(Tabela["Id_Pessoa"].ToString());
                    IdFilial     = int.Parse(Tabela["Id_Filial"].ToString());
                    IdVendedor   = int.Parse(Tabela["Id_Vendedor"].ToString());
                    IdUsuario    = int.Parse(Tabela["Id_Usuario"].ToString());
                    IdRota       = int.Parse(Tabela["Id_Rota"].ToString());
                    IdFormaPgto  = int.Parse(Tabela["Id_FormaPgto"].ToString());                    
                    TpVenda      = Tabela["TpVenda"].ToString().Trim();
                    NumDocumento = Tabela["NumDocumento"].ToString().Trim();
                    IdEntregador = int.Parse(Tabela["Id_Entregador"].ToString());
                    Observacao   = Tabela["Observacao"].ToString().Trim();                    
                    VlrSubTotal  = decimal.Parse(Tabela["VlrSubTotal"].ToString());
                    VlrDesconto  = decimal.Parse(Tabela["VlrDesconto"].ToString());
                    VlrTotal     = decimal.Parse(Tabela["VlrTotal"].ToString());
                    VlrCredito   = decimal.Parse(Tabela["Credito"].ToString());
                    FormNF       = Tabela["FormNF"].ToString().Trim();
                    //
                    CnpjCpf         = Tabela["CnpjCpf"].ToString().Trim();
                    NmPessoa        = Tabela["Pessoa"].ToString().Trim();
                    InscUF          = Tabela["InscUF"].ToString().Trim();
                    Cep             = Tabela["Cep"].ToString().Trim();
                    Endereco        = Tabela["Endereco"].ToString().Trim();
                    Numero          = Tabela["Numero"].ToString().Trim();
                    Complemento     = Tabela["Complemento"].ToString().Trim();
                    Cidade          = Tabela["Cidade"].ToString().Trim();
                    Bairro          = Tabela["Bairro"].ToString().Trim();
                    IdUF            = int.Parse(Tabela["Id_Uf"].ToString());
                    Fone            = Tabela["Fone"].ToString().Trim();
                    IdVdMaster      = int.Parse(Tabela["Id_VdMaster"].ToString());                    
                    IdUltUsuario    = int.Parse(Tabela["Id_UltUsuario"].ToString());
                    VinculoVd       = Tabela["VinculoVd"].ToString().Trim();
                    IdUsuAutDeb     = int.Parse(Tabela["Id_UsuAutDeb"].ToString());                    
                    PrevEntrega     = DateTime.Parse(Tabela["PrevEntrega"].ToString());
                    PrazoPgto       = Tabela["PrazoPgto"].ToString().Trim();
                    ObsCancelamento = Tabela["ObsCancelamento"].ToString().Trim();
                    NumPedido       = Tabela["NumPedido"].ToString().Trim();


                    if (Tabela["Status"].ToString() != "")                        
                       Status = int.Parse(Tabela["Status"].ToString());

                    if (Tabela["Id_VdTroca"].ToString() != "")
                        IdVdTroca = int.Parse(Tabela["Id_VdTroca"].ToString());

                    if (Tabela["VdBalcao"].ToString()!="")
                        VdBalcao = int.Parse(Tabela["VdBalcao"].ToString());

                    if (Tabela["Id_VdMatriz"].ToString() != "")
                        IdVdMatriz = int.Parse(Tabela["Id_VdMatriz"].ToString());

                    if (Tabela["SemMovEst"].ToString() != "")
                        SemMovEst = int.Parse(Tabela["SemMovEst"].ToString());

                    if (Tabela["FrenteLoja"].ToString() != "")
                        FrenteLoja = int.Parse(Tabela["FrenteLoja"].ToString());                   
                    
                    VinculoVd = Tabela["VinculoVd"].ToString().Trim();

                    if (Tabela["DataCancel"].ToString() != "")
                        DataCancel  = DateTime.Parse(Tabela["DataCancel"].ToString());

                    if (Tabela["VlrLiberado"].ToString() != "")
                        VlrLiberado = decimal.Parse(Tabela["VlrLiberado"].ToString());

                    if (Tabela["Pais"].ToString() != "")
                        Pais = Tabela["Pais"].ToString().Trim();

                    if (Tabela["Id_LancCF"].ToString() != "")
                        IdLancCF = int.Parse(Tabela["ID_LancCF"].ToString());

                    if (Tabela["VdImpFat"].ToString() != "")
                        VdImpFat = int.Parse(Tabela["VdImpFat"].ToString());

                    if (Tabela["Cli_Reativado"].ToString() != "")
                        CliReativado = int.Parse(Tabela["Cli_Reativado"].ToString());

                    if (Tabela["Id_FilialEntrega"].ToString() != "")
                        IdFilialEntrega = int.Parse(Tabela["Id_FilialEntrega"].ToString());

                    if (Tabela["Id_VdOrigem"].ToString() != "")
                        IdVdOrigem = int.Parse(Tabela["Id_VdOrigem"].ToString());

                    if (Tabela["Id_FilialOrigem"].ToString() != "")
                        IdFilialOrigem = int.Parse(Tabela["Id_FilialOrigem"].ToString());

                    if (Tabela["Id_VdDestino"].ToString() != "")
                        IdVdDestino = int.Parse(Tabela["Id_VdDestino"].ToString());

                    if (Tabela["IgnoraDesc"].ToString() != "")
                        IgnoraDesc = int.Parse(Tabela["IgnoraDesc"].ToString());

                    if (Tabela["DtEnvioRec"].ToString() != "")
                        DtEnvioRec = DateTime.Parse(Tabela["DtEnvioRec"].ToString());

                    if (Tabela["Id_UsuLibParc"].ToString() != "")
                        IdUsuLibParc = int.Parse(Tabela["Id_UsuLibParc"].ToString());

                    if (Tabela["Id_UsuBoleto"].ToString() != "")
                        IdUsuboleto  = int.Parse(Tabela["Id_UsuBoleto"].ToString());

                    if (Tabela["Id_UsuLibClieNv"].ToString() != "")
                        IdUsuLibClieNv = int.Parse(Tabela["Id_UsuLibClieNv"].ToString());

                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdVenda > 0)
            {
                sSQL = "UPDATE MvVenda SET Id_Venda=@Id,Data=Convert(DateTime,@Data,103),ImpNF=@ImpNF,Id_Caixa=@IdCaixa,Id_Pessoa=@IdPessoa,Id_Filial=@IdFilial,Id_Vendedor=@IdVendedor,Id_Rota=@IdRota," +
                       "TpVenda=@TpVenda,NumDocumento=@NumDocumento,Id_FormaPgto=@IdFormaPgto,PrevEntrega=Convert(DateTime,@PrevEntrega,103),Observacao=@Observacao,VlrSubTotal=@VlrSubTotal,VlrDesconto=@VlrDesconto,VlrTotal=@VlrTotal," +
                       "CnpjCpf=@CnpjCpf,Pessoa=@NmPessoa,InscUF=@InscUF,Cep=@Cep,Endereco=@Endereco,Numero=@Numero,Complemento=@Complemento,Cidade=@Cidade,Bairro=@Bairro,Id_Uf=@IdUf,Fone=@Fone,FormNF=@FormNF,Id_UltUsuario=@IdUltUsuario,Credito=@VlrCredito,"+
                       "PrazoPgto=@PrazoPgto,Id_VdTroca=@IdVdTroca,VdBalcao=@VdBalcao,Id_VdMatriz=@IdVdMatriz,SemMovEst=@SemMovEst,FrenteLoja=@FrenteLoja,Pais=@Pais,Id_LancCF=@IdLancCF,VdImpFat=@VdImpFat,Cli_Reativado=@CliReativado,Id_FilialEntrega=@IdFilialEntrega,"+
                       "IgnoraDesc=@IgnoraDesc,Id_FilialOrigem=@IdFilialOrigem,NumPedido=@NumPedido Where Id_Venda=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdVenda);
            }
            else
            {
                IdVenda = Controle.ProximoID("MVVENDA");
                if (TpVenda.Trim() == "OC")
                    NumDocumento = "OC" + string.Format("{0:D5}", Controle.ProximoID("VD_ORCAM"));
                if (TpVenda.Trim() == "OE")
                    NumDocumento = "OE" + string.Format("{0:D5}", Controle.ProximoID("VD_ORDEM"));
                if (TpVenda.Trim() == "PV")
                    NumDocumento = "PV" + string.Format("{0:D5}", Controle.ProximoID("VD_PEDIDO"));
                if (TpVenda.Trim() == "CO")
                    NumDocumento = "CO" + string.Format("{0:D5}", Controle.ProximoID("VD_COMODATO"));
                if (TpVenda.Trim() == "TROCA")
                    NumDocumento = "TM" + string.Format("{0:D5}", Controle.ProximoID("VD_TROCA"));
                if (TpVenda.Trim() == "PR")
                    NumDocumento = "PR" + string.Format("{0:D5}", Controle.ProximoID("VD_PROMOCAO"));
                if (TpVenda.Trim() == "BONIF")
                    NumDocumento = "BF" + string.Format("{0:D5}", Controle.ProximoID("VD_BONIF"));
                if (TpVenda.Trim() == "AM")
                    NumDocumento = "AM" + string.Format("{0:D5}", Controle.ProximoID("VD_AMOSTRA"));
                if (TpVenda.Trim() == "VF")
                    NumDocumento = "VF" + string.Format("{0:D5}", Controle.ProximoID("VD_VDFINANC"));
                if (TpVenda.Trim() == "EMVF")
                    NumDocumento = "EM" + string.Format("{0:D5}", Controle.ProximoID("VD_EMFINANC"));
                if (TpVenda.Trim() == "PI")
                    NumDocumento = "PI" + string.Format("{0:D5}", Controle.ProximoID("VD_INTERNET"));
                if (TpVenda.Trim() == "PC")
                    NumDocumento = "PC" + string.Format("{0:D5}", Controle.ProximoID("VD_CONSIGACAO"));

                sSQL = "INSERT INTO MVVENDA (Id_Venda,Data,ImpNF,Id_Caixa,Id_Pessoa,Id_Filial,Id_Vendedor,Id_Usuario,Id_Rota,TpVenda,NumDocumento,Id_FormaPgto,PrevEntrega,Observacao,VlrSubTotal,VlrDesconto,VlrTotal,CnpjCpf,Pessoa,InscUF,Cep,Endereco," +
                       "Numero,Complemento,Cidade,Bairro,Id_Uf,Fone,Id_Entregador,Id_VdMaster,VinculoVd,FormNF,Id_UsuAutDeb,Id_UltUsuario,Credito,PrazoPgto,Faturado,Id_VdTroca,VdBalcao,Id_VdMatriz,SemMovEst,FrenteLoja,Pais,Id_LancCF,VdImpFat,Cli_Reativado,Id_FilialEntrega,ObsCancelamento,IgnoraDesc,Id_FilialOrigem,Id_UsuLibParc,NumPedido,Id_Usuboleto,Status)" +
                       " VALUES(@Id,Convert(DateTime,@Data,103),@ImpNF,@IdCaixa,@IdPessoa,@IdFilial,@IdVendedor,@IdUsuario,@IdRota,@TpVenda,@NumDocumento,@IdFormaPgto,Convert(DateTime,@PrevEntrega,103),@Observacao,@VlrSubTotal,@VlrDesconto,@VlrTotal,@CnpjCpf,@NmPessoa,"+
                       "@InscUF,@Cep,@Endereco,@Numero,@Complemento,@Cidade,@Bairro,@IdUf,@Fone,0,@VdMaster,'',@FormNF,0,@IdUltUsuario,@VlrCredito,@PrazoPgto,0,@IdVdTroca,@VdBalcao,@IdVdMatriz,@SemMovEst,@FrenteLoja,@Pais,@IdLancCF,@VdImpFat,@CliReativado,@IdFilialEntrega,' ',@IgnoraDesc,@IdFilialOrigem,@IdUsuLibParc,@NumPedido,@IdUsuboleto,@Status)";
                Nm_param.Add("@IdUsuario"); Vr_param.Add(IdUsuario);
                Nm_param.Add("@VdMaster"); Vr_param.Add(IdVenda);
            }

            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdVenda);
                Nm_param.Add("@Data"); Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdCaixa"); Vr_param.Add(IdCaixa);
                Nm_param.Add("@ImpNF"); Vr_param.Add(ImpNF);
                Nm_param.Add("@IdPessoa"); Vr_param.Add(IdPessoa);
                Nm_param.Add("@IdFilial"); Vr_param.Add(IdFilial);
                Nm_param.Add("@IdVendedor"); Vr_param.Add(IdVendedor);                                
                Nm_param.Add("@IdRota"); Vr_param.Add(IdRota);
                Nm_param.Add("@IdFormaPgto"); Vr_param.Add(IdFormaPgto);
                Nm_param.Add("@TpVenda"); Vr_param.Add(TpVenda);
                Nm_param.Add("@NumDocumento"); Vr_param.Add(NumDocumento);
                Nm_param.Add("@PrevEntrega"); Vr_param.Add(PrevEntrega.ToShortDateString());
                Nm_param.Add("@Observacao"); Vr_param.Add(Observacao);
                Nm_param.Add("@Status"); Vr_param.Add(Status);
                Nm_param.Add("@VlrSubTotal"); Vr_param.Add(Controle.FloatToStr(VlrSubTotal, 2));
                Nm_param.Add("@VlrDesconto"); Vr_param.Add(Controle.FloatToStr(VlrDesconto, 2));
                Nm_param.Add("@VlrCredito"); Vr_param.Add(Controle.FloatToStr(VlrCredito, 2));
                Nm_param.Add("@VlrTotal"); Vr_param.Add(Controle.FloatToStr(VlrTotal, 2));
                Nm_param.Add("@CnpjCpf"); Vr_param.Add(CnpjCpf);
                Nm_param.Add("@NmPessoa"); Vr_param.Add(NmPessoa);
                Nm_param.Add("@InscUF"); Vr_param.Add(InscUF);
                Nm_param.Add("@Cep"); Vr_param.Add(Cep);
                Nm_param.Add("@Endereco"); Vr_param.Add(Endereco);
                Nm_param.Add("@Numero"); Vr_param.Add(Numero);
                Nm_param.Add("@Complemento"); Vr_param.Add(Complemento);
                Nm_param.Add("@Cidade"); Vr_param.Add(Cidade);
                Nm_param.Add("@Bairro"); Vr_param.Add(Bairro);
                Nm_param.Add("@IdUf"); Vr_param.Add(IdUF);
                Nm_param.Add("@Fone"); Vr_param.Add(Fone);
                Nm_param.Add("@FormNF"); Vr_param.Add(FormNF);
                Nm_param.Add("@IdUltUsuario"); Vr_param.Add(IdUltUsuario);
                Nm_param.Add("@PrazoPgto"); Vr_param.Add(PrazoPgto);
                Nm_param.Add("@IdVdTroca"); Vr_param.Add(IdVdTroca);
                Nm_param.Add("@VdBalcao"); Vr_param.Add(VdBalcao);
                Nm_param.Add("@IdVdMatriz"); Vr_param.Add(IdVdMatriz);
                Nm_param.Add("@SemMovEst"); Vr_param.Add(SemMovEst);
                Nm_param.Add("@FrenteLoja"); Vr_param.Add(FrenteLoja);
                Nm_param.Add("@Pais"); Vr_param.Add(Pais);
                Nm_param.Add("@IdLancCF"); Vr_param.Add(IdLancCF);
                Nm_param.Add("@VdImpFat"); Vr_param.Add(VdImpFat);
                Nm_param.Add("@CliReativado"); Vr_param.Add(CliReativado);
                Nm_param.Add("@IdFilialEntrega"); Vr_param.Add(IdFilialEntrega);
                Nm_param.Add("@IdFilialOrigem"); Vr_param.Add(IdFilialOrigem);
                Nm_param.Add("@IgnoraDesc"); Vr_param.Add(IgnoraDesc);
                Nm_param.Add("@IdUsuLibParc"); Vr_param.Add(IdUsuLibParc);
                Nm_param.Add("@NumPedido"); Vr_param.Add(NumPedido);
                Nm_param.Add("@IdUsuboleto"); Vr_param.Add(IdUsuboleto);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdVenda > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Mvvenda        WHERE Id_Venda=" + IdVenda.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM MvVendaItens   WHERE Id_Venda=" + IdVenda.ToString().Trim());                
            }
        }
        public void Concluir(int Sta)
        {
            string sSQL = "UPDATE MvVenda Set Status="+Sta.ToString()+",DataCancel=Null Where Id_Venda=" + IdVenda.ToString();
            Status = Sta;
            if (Sta == 2)
            {
                sSQL = "UPDATE MvVenda Set DtHrFaturamento=GetDate(),Status=2,DataCancel=Null Where Id_Venda=" + IdVenda.ToString();
                Status = 2;
            }
            Controle.ExecutaSQL(sSQL);            
        }
        public void Cancelar(int Sta)
        {
            Controle.ExecutaSQL("UPDATE MvVenda Set Status=4,DataConfirmacao=Null,Id_VdDestino=0,DataCancel=Convert(DateTime,'"+DateTime.Now.Date.ToShortDateString()+"',103) Where Id_VdMaster=" + IdVdMaster.ToString());            
            Status = 4;            
        }
        public void SetaAutDebito(int IdUsu,decimal VlrLiberado)
        {            
            string sSQL = "UPDATE MvVenda Set VlrLiberado=" + Controle.FloatToStr(VlrLiberado, 2) + ",Id_UsuAutDeb=" + IdUsu.ToString() + ",DtAutDeb=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103)  Where Id_Venda=" + IdVenda.ToString();
            Controle.ExecutaSQL(sSQL);
        }

        public void SetaAutParcelas(int IdUsu, bool NaoVerPrzo)
        {
            string sSQL = "UPDATE MvVenda Set Id_UsuLibParc=" + IdUsu.ToString() + "  Where Id_Venda=" + IdVenda.ToString();
            Controle.ExecutaSQL(sSQL);
            if (NaoVerPrzo)
            {
                sSQL = "UPDATE Pessoas Set NaoVerPrazoPg=1  Where Id_Pessoa=" + IdPessoa.ToString();
                Controle.ExecutaSQL(sSQL);
            }
        }
        public void SetaAutPessoaF(int IdUsu, bool NaoVerPrzo)
        {
            string sSQL = "UPDATE MvVenda Set Id_UsuBoleto=" + IdUsu.ToString() + "  Where Id_Venda=" + IdVenda.ToString();
            Controle.ExecutaSQL(sSQL);
            if (NaoVerPrzo)
            {
                sSQL = "UPDATE Pessoas Set NaoVerPrazoPg=1  Where Id_Pessoa=" + IdPessoa.ToString();
                Controle.ExecutaSQL(sSQL);
            }
        }
        public void SetaAutPrimeira(int IdUsu, bool NaoVerPrzo)
        {
            string sSQL = "UPDATE MvVenda Set Id_UsuLibClieNv=" + IdUsu.ToString() + "  Where Id_Venda=" + IdVenda.ToString();
            Controle.ExecutaSQL(sSQL);            
        }

        public bool SetaLiberacaoProduto(int IdFilial, int IdUsu, int IdPrd, decimal Saldo)
        {
            try
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM LiberacaoProduto WHERE Id_Filial=" + IdFilial.ToString() + "and Id_Venda=" + IdVenda.ToString() + " and Id_Produto=" + IdPrd.ToString());

                if (Tabela.HasRows)
                    Controle.ExecutaSQL("Update LiberacaoProduto Set Id_Filial=" + IdFilial.ToString() + ", Id_Usuario=" + IdUsuario.ToString() + ",Data=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103),Estoque=" + Controle.FloatToStr(Saldo, 3) + " Where Id_Venda=" + IdVenda.ToString() + " and Id_Produto=" + IdPrd.ToString());                    
                else
                    Controle.ExecutaSQL("Insert into LiberacaoProduto (Id_Venda,Id_Produto,Id_Usuario,Data,Id_Filial,Estoque) Values (" + IdVenda.ToString() + "," + IdPrd.ToString() + "," + IdUsuario.ToString() + ",Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103)," + IdFilial.ToString() + "," + Controle.FloatToStr(Saldo, 3) + ")");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProdutoLiberado(int IdProduto)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM LiberacaoProduto WHERE Id_Venda=" + IdVenda.ToString() + " and Id_Produto=" + IdProduto.ToString());
            if (Tabela.HasRows)
                return true;
            else
                return false;
        }
        public string SqlRelatorio(int IdDoc)
        {
            string TxtSql = "";

            if (TpVenda == "EMVF")
            {
                TxtSql = "SELECT T2.Id_Venda, T2.Data, T2.ImpNF, T2.Id_Caixa, T2.Id_Pessoa, T2.Id_Filial, T2.Id_Vendedor, T2.Id_Usuario, T2.Id_Rota, T2.Id_FormaPgto, T2.TpVenda, " +
                       " T2.NumDocumento, T2.PrevEntrega, T2.Id_Entregador, T2.Observacao, T2.Status, T2.DataCancel, 0.00 AS VlrSubTotal, 0.00 AS VlrDesconto, 0.00 AS VlrTotal, T2.Pessoa, " +
                       " T2.CNPJCPF, T2.InscUF, T2.Cep, T2.Endereco, T2.Numero, T2.Complemento, T2.Cidade, T2.Bairro, T2.Id_Uf, T2.Fone, T2.Id_VdMaster, T2.VinculoVd, " +
                       " T2.FormNF, T1.Id_Produto, T1.Qtde, 0.00 AS VlrUnitario, 0.00 AS TOTALITEM, T3.Vendedor, T4.Referencia, T4.Descricao, T5.Descricao AS MOVIMENTO, " +
                       " T6.FormaPgto, T7.Entregador, T8.Sigla AS UF, T4.Unidade,T1.TIPOITEM,0.00 AS CREDITO,T2.PRAZOPGTO,T9.Fantasia as Filial,T0.Obs_Entrega,T0.Fantasia,TB.FANTASIA AS FILIALENTREGA,GRP.GRUPO,T4.NCM, " +
                       " CASE ISNULL((SELECT COUNT(*) FROM LiberacaoProduto WHERE Id_Venda=(CASE ISNULL(T2.ID_VDORIGEM,0) WHEN 0 THEN T1.Id_Venda ELSE ISNULL(T2.ID_VDORIGEM,0) END) AND Id_Produto=T1.Id_Produto),0) WHEN 0 THEN 0 ELSE 1 END AS PRDLIB ,T4.PRODUTOKIT,CASE T1.TIPOITEM WHEN 'N' THEN 0 ELSE (T4.PESOBRUTO * T1.QTDE) END AS PESOBRUTO" +                       
                       "   FROM MvVendaItens AS T1 " +
                       " LEFT OUTER JOIN MvVenda AS T2 ON T2.Id_Venda = T1.Id_Venda " +
                       " LEFT OUTER JOIN Vendedores AS T3 ON T3.Id_Vendedor = T2.Id_Vendedor " +
                       " LEFT OUTER JOIN Produtos AS T4 ON T4.Id_Produto = T1.Id_Produto " +
                       " LEFT OUTER JOIN TabelasAux AS T5 ON T5.Campo = 'VENDA' AND T5.Chave = T2.TpVenda " +
                       " LEFT OUTER JOIN FormaPagamento AS T6 ON T6.Id_FormaPgto = T2.Id_FormaPgto " +
                       " LEFT OUTER JOIN Entregadores AS T7 ON T7.Id_Entregador = T2.Id_Entregador " +
                       " LEFT OUTER JOIN Estados AS T8 ON T8.Id_Uf = T2.Id_Uf " +
                       " LEFT OUTER JOIN Empresa_Filial T9 on (T9.Id_Filial=T2.Id_Filial) " +
                       " LEFT OUTER JOIN Empresa_Filial TB on (TB.Id_Filial=T2.Id_FilialEntrega) " +
                       " LEFT OUTER JOIN Pessoas AS T0 ON T0.Id_Pessoa = T2.Id_Pessoa" +
                       " LEFT OUTER JOIN GRUPOPRODUTO AS GRP ON GRP.Id_Grupo = T4.Id_Grupo" +
                       " Where T1.Id_Venda=" + IdDoc.ToString() + " ORDER BY T1.TIPOITEM,T1.ID_ITEM";
            }
            else
            {
                TxtSql = "SELECT T2.Id_Venda, T2.Data, T2.ImpNF, T2.Id_Caixa, T2.Id_Pessoa, T2.Id_Filial, T2.Id_Vendedor, T2.Id_Usuario, T2.Id_Rota, T2.Id_FormaPgto, T2.TpVenda, " +
                           " T2.NumDocumento, T2.PrevEntrega, T2.Id_Entregador, T2.Observacao, T2.Status, T2.DataCancel, T2.VlrSubTotal, T2.VlrDesconto, T2.VlrTotal, T2.Pessoa, " +
                           " T2.CNPJCPF, T2.InscUF, T2.Cep, T2.Endereco, T2.Numero, T2.Complemento, T2.Cidade, T2.Bairro, T2.Id_Uf, T2.Fone, T2.Id_VdMaster, T2.VinculoVd, " +
                           " T2.FormNF, T1.Id_Produto, T1.Qtde, T1.VlrUnitario, T1.VlrTotal AS TOTALITEM, T3.Vendedor, T4.Referencia, T4.Descricao, T5.Descricao AS MOVIMENTO, " +
                           " T6.FormaPgto, T7.Entregador, T8.Sigla AS UF, T4.Unidade,T1.TIPOITEM,T2.CREDITO,T2.PRAZOPGTO,T9.Fantasia as Filial,T0.Obs_Entrega,T0.Fantasia,TB.FANTASIA AS FILIALENTREGA, GRP.GRUPO,T4.NCM, " +
                           " CASE ISNULL((SELECT COUNT(*) FROM LiberacaoProduto WHERE Id_Venda=(CASE ISNULL(T2.ID_VDORIGEM,0) WHEN 0 THEN T1.Id_Venda ELSE ISNULL(T2.ID_VDORIGEM,0) END) AND Id_Produto=T1.Id_Produto),0) WHEN 0 THEN 0 ELSE 1 END AS PRDLIB,T4.PRODUTOKIT,CASE T1.TIPOITEM WHEN 'N' THEN 0 ELSE (T4.PESOBRUTO * T1.QTDE) END AS PESOBRUTO  " +
                           "   FROM MvVendaItens AS T1 " +
                           " LEFT OUTER JOIN MvVenda AS T2 ON T2.Id_Venda = T1.Id_Venda " +
                           " LEFT OUTER JOIN Vendedores AS T3 ON T3.Id_Vendedor = T2.Id_Vendedor " +
                           " LEFT OUTER JOIN Produtos AS T4 ON T4.Id_Produto = T1.Id_Produto " +
                           " LEFT OUTER JOIN TabelasAux AS T5 ON T5.Campo = 'VENDA' AND T5.Chave = T2.TpVenda " +
                           " LEFT OUTER JOIN FormaPagamento AS T6 ON T6.Id_FormaPgto = T2.Id_FormaPgto " +
                           " LEFT OUTER JOIN Entregadores AS T7 ON T7.Id_Entregador = T2.Id_Entregador " +
                           " LEFT OUTER JOIN Estados AS T8 ON T8.Id_Uf = T2.Id_Uf " +
                           " LEFT OUTER JOIN Empresa_Filial T9 on (T9.Id_Filial=T2.Id_Filial) " +
                           " LEFT OUTER JOIN Empresa_Filial TB on (TB.Id_Filial=T2.Id_FilialEntrega) " +
                           " LEFT OUTER JOIN Pessoas AS T0 ON T0.Id_Pessoa = T2.Id_Pessoa" +
                           " LEFT OUTER JOIN GRUPOPRODUTO AS GRP ON GRP.Id_Grupo = T4.Id_Grupo" +
                           " Where T1.Id_Venda=" + IdDoc.ToString() + " ORDER BY T1.TIPOITEM,T1.ID_ITEM";
            }
            return TxtSql;
        }   
    }
}
