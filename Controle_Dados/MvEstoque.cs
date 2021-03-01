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
    public class MvEstoque
    {
        private int _IdMov;
        public int IdMov
        {
            get { return _IdMov; }
            set { _IdMov = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private string _TpMov;
        public string TpMov
        {
            get { return _TpMov; }
            set { _TpMov = value; }
        }
        private int _IdPedCompra;
        public int IdPedCompra
        {
            get { return _IdPedCompra; }
            set { _IdPedCompra = value; }
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
        private int _IdFilialOrigDest;
        public int IdFilialOrigDest
        {
            get { return _IdFilialOrigDest; }
            set { _IdFilialOrigDest = value; }
        }
        private int _IdFormaPgto;
        public int IdFormaPgto
        {
            get { return _IdFormaPgto; }
            set { _IdFormaPgto = value; }
        }
        private string _Documento;
        public string Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        private string _NumDocumento;
        public string NumDocumento
        {
            get { return _NumDocumento; }
            set { _NumDocumento = value; }
        }
        private string _NumFormulario;
        public string NumFormulario
        {
            get { return _NumFormulario; }
            set { _NumFormulario = value; }
        }
        private string _Solicitante;
        public string Solicitante
        {
            get { return _Solicitante; }
            set { _Solicitante = value; }
        }
        private string _Autorizado;
        public string Autorizado
        {
            get { return _Autorizado; }
            set { _Autorizado = value; }
        }
        private DateTime _DtEmissao;
        public DateTime DtEmissao
        {
            get { return _DtEmissao; }
            set { _DtEmissao = value; }
        }
        private DateTime _DtEntSai;
        public DateTime DtEntSai
        {
            get { return _DtEntSai; }
            set { _DtEntSai = value; }
        }
        private int _TpFrete;
        public int TpFrete
        {
            get { return _TpFrete; }
            set { _TpFrete = value; }
        }
        private int _TipoPgto;
        public int TipoPgto
        {
            get { return _TipoPgto; }
            set { _TipoPgto = value; }
        }
        private string _ChaveNFE;
        public string ChaveNFE
        {
            get { return _ChaveNFE; }
            set { _ChaveNFE = value; }
        }
        private int _IdCFOP;
        public int IdCFOP
        {
            get { return _IdCFOP; }
            set { _IdCFOP = value; }
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
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private decimal _BIcms;
        public decimal BIcms
        {
            get { return _BIcms; }
            set { _BIcms = value; }
        }
        private decimal _VlrIcms;
        public decimal VlrIcms
        {
            get { return _VlrIcms; }
            set { _VlrIcms = value; }
        }
        private decimal _BIcmsSub;
        public decimal BIcmsSub
        {
            get { return _BIcmsSub; }
            set { _BIcmsSub = value; }
        }
        private decimal _VlrIcmsSub;
        public decimal VlrIcmsSub
        {
            get { return _VlrIcmsSub; }
            set { _VlrIcmsSub = value; }
        }
        private decimal _VlrFrete;
        public decimal VlrFrete
        {
            get { return _VlrFrete; }
            set { _VlrFrete = value; }
        }
        private decimal _VlrSeguro;
        public decimal VlrSeguro
        {
            get { return _VlrSeguro; }
            set { _VlrSeguro = value; }
        }
        private decimal _VlrOutraDesp;
        public decimal VlrOutraDesp
        {
            get { return _VlrOutraDesp; }
            set { _VlrOutraDesp = value; }
        }
        private decimal _VlrIpi;
        public decimal VlrIpi
        {
            get { return _VlrIpi; }
            set { _VlrIpi = value; }
        }
        private decimal _OutrosIPI;
        public decimal OutrosIPI
        {
            get { return _OutrosIPI; }
            set { _OutrosIPI = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _ObsSelo;
        public string ObsSelo
        {
            get { return _ObsSelo; }
            set { _ObsSelo = value; }
        }
        private int _Conferido;
        public int Conferido
        {
            get { return _Conferido; }
            set { _Conferido = value; }
        }
        private int _IdMovChave;
        public int IdMovChave
        {
            get { return _IdMovChave; }
            set { _IdMovChave = value; }
        }

        private string _NFeSerie;
        public string NFeSerie
        {
            get { return _NFeSerie; }
            set { _NFeSerie = value; }
        }

        private int _NtServico;
        public int NtServico
        {
            get { return _NtServico; }
            set { _NtServico = value; }
        }

        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        private int _Pendencia;
        public int Pendencia
        {
            get { return _Pendencia; }
            set { _Pendencia = value; }
        }
        private string _ObsPendencia;
        public string ObsPendencia
        {
            get { return _ObsPendencia; }
            set { _ObsPendencia = value; }
        }

        private int _TpAvaria;
        public int TpAvaria
        {
            get { return _TpAvaria; }
            set { _TpAvaria = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdMov         = 0;
            Data          = DateTime.Now;
            TpMov         = "";
            IdPedCompra   = 0;
            IdPessoa      = 0;
            Documento     = "";
            NumDocumento  = "";
            NumFormulario = "";
            IdFilial      = 0;
            IdFilialOrigDest = 0;
            IdFormaPgto  = 0;
            Solicitante  = "";
            Autorizado   = "";
            DtEntSai     = DateTime.Now;
            DtEmissao    = DateTime.Now;
            TipoPgto     = 0;
            ChaveNFE     = "";
            TpFrete      = 0;
            IdCFOP       = 0;
            VlrSubTotal  = 0;
            VlrDesconto  = 0;
            VlrTotal     = 0;
            BIcms        = 0;
            VlrIcms      = 0;
            BIcmsSub     = 0;
            VlrIcmsSub   = 0;
            VlrFrete     = 0;
            VlrSeguro    = 0;
            VlrOutraDesp = 0;
            VlrIpi       = 0;
            Observacao   = "";
            ObsSelo      = "";
            Status       = 0;
            OutrosIPI    = 0;
            Conferido    = 0;
            IdMovChave   = 0;
            NtServico    = 0;
            NFeSerie     = "1";
            IdUsuario    = 0;
            Pendencia    = 0;
            TpAvaria     = 0;
            ObsPendencia = "";

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MvEstoque WHERE Id_Mov=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdMov      = Id;
                    Data             = DateTime.Parse(Tabela["Data"].ToString());
                    TpMov            = Tabela["TpMov"].ToString().Trim();
                    IdPedCompra      = int.Parse(Tabela["Id_PedCompra"].ToString());
                    IdPessoa         = int.Parse(Tabela["Id_Pessoa"].ToString());
                    Documento        = Tabela["Documento"].ToString().Trim();
                    NumDocumento     = Tabela["NumDocumento"].ToString().Trim();
                    NumFormulario    = Tabela["NumFormulario"].ToString().Trim();
                    IdFilial         = int.Parse(Tabela["Id_Filial"].ToString());
                    IdFilialOrigDest = int.Parse(Tabela["Id_FilialOrigDest"].ToString());
                    IdFormaPgto      = int.Parse(Tabela["Id_FormaPgto"].ToString());
                    Solicitante      = Tabela["Solicitante"].ToString().Trim();
                    Autorizado       = Tabela["Autorizado"].ToString().Trim();
                    DtEntSai         = DateTime.Parse(Tabela["DtEntSai"].ToString());
                    DtEmissao        = DateTime.Parse(Tabela["DtEmissao"].ToString());
                    TpFrete          = int.Parse(Tabela["TpFrete"].ToString());                    
                    IdCFOP           = int.Parse(Tabela["Id_Cfop"].ToString());
                    VlrSubTotal      = decimal.Parse(Tabela["VlrSubTotal"].ToString());
                    VlrDesconto      = decimal.Parse(Tabela["VlrDesconto"].ToString());
                    VlrTotal         = decimal.Parse(Tabela["VlrTotal"].ToString());
                    BIcms            = decimal.Parse(Tabela["B_Icms"].ToString());
                    VlrIcms          = decimal.Parse(Tabela["VlrIcms"].ToString());
                    BIcmsSub         = decimal.Parse(Tabela["B_IcmsSub"].ToString());
                    VlrIcmsSub       = decimal.Parse(Tabela["VlrIcmsSub"].ToString());
                    VlrFrete         = decimal.Parse(Tabela["VlrFrete"].ToString());
                    VlrSeguro        = decimal.Parse(Tabela["VlrSeguro"].ToString());
                    VlrOutraDesp     = decimal.Parse(Tabela["VlrOutrasDesp"].ToString());
                    VlrIpi           = decimal.Parse(Tabela["VlrIpi"].ToString());                    
                    Observacao       = Tabela["Observacao"].ToString().Trim();
                    ObsSelo          = Tabela["ObsSelo"].ToString().Trim();
                    Status           = int.Parse(Tabela["Status"].ToString());
                    TipoPgto         = int.Parse(Tabela["TipoPgto"].ToString());
                    ChaveNFE         = Tabela["ChaveNFE"].ToString().Trim();
                    NFeSerie         = Tabela["NFeSerie"].ToString().Trim();
                    ObsPendencia     = Tabela["ObsPendencia"].ToString().Trim();

                    if (Tabela["OutrosIPI"].ToString() != "")
                        OutrosIPI = decimal.Parse(Tabela["OutrosIPI"].ToString());
            
                    if (Tabela["Conferido"].ToString() != "")
                        Conferido = int.Parse(Tabela["Conferido"].ToString());

                    if (Tabela["Id_MovChave"].ToString() != "")
                        IdMovChave = int.Parse(Tabela["Id_MovChave"].ToString());

                    if (Tabela["NtServico"].ToString() != "")
                        NtServico = int.Parse(Tabela["NtServico"].ToString());

                    if (Tabela["Id_Usuario"].ToString() != "")
                        IdUsuario = int.Parse(Tabela["Id_Usuario"].ToString());

                    if (Tabela["Pendencia"].ToString() != "")
                        Pendencia = int.Parse(Tabela["Pendencia"].ToString());

                    if (Tabela["TpAvaria"].ToString() != "")
                        TpAvaria = int.Parse(Tabela["TpAvaria"].ToString());                    
                }
            }
            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdMov > 0)
            {
                sSQL = "UPDATE MvEstoque SET Id_Mov=@Id,Data=Convert(DateTime,@Data,103),TpMov=@TpMov,Id_PedCompra=@IdPedCompra,Id_Pessoa=@IdPessoa,Documento=@Documento," +
                       "NumDocumento=@NumDocumento,NumFormulario=@NumFormulario,Id_Filial=@IdFilial,Id_FilialOrigDest=@IdFilialOrigDest,Id_FormaPgto=@IdFormaPgto,Solicitante=@Solicitante," +
                       "Autorizado=@Autorizado,DtEntSai=Convert(DateTime,@DtEntSai,103),TpFrete=@TpFrete,Id_CFOP=@IdCFOP,VlrSubTotal=@VlrSubTotal," +
                       "VlrDesconto=@VlrDesconto,VlrTotal=@VlrTotal,B_Icms=@BIcms,VlrIcms=@VlrIcms,B_IcmsSub=@BIcmsSub,VlrIcmsSub=@VlrIcmsSub,VlrFrete=@VlrFrete,VlrSeguro=@VlrSeguro," +
                       "VlrOutrasDesp=@VlrOutraDesp,VlrIpi=@VlrIpi,Observacao=@Observacao,Status=@Status,ObsSelo=@ObsSelo,ChaveNFE=@ChaveNFE,TipoPgto=@TipoPgto,DtEmissao=Convert(DateTime,@DtEmissao,103),"+
                       "OutrosIPI=@OutrosIPI,Conferido=@Conferido,Id_MovChave=@IdMovChave,NtServico=@NtServico,NFeSerie=@NFeSerie,Id_Usuario=@IdUsuario,Pendencia=@Pendencia,ObsPendencia=@ObsPendencia,TpAvaria=@TpAvaria Where Id_Mov=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdMov);
            }
            else
            {
                IdMov = Controle.ProximoID("MvEstoque");
                sSQL = "INSERT INTO MvEstoque (Id_Mov,Data,TpMov,Id_PedCompra,Id_Pessoa,Documento,NumDocumento,NumFormulario,Id_Filial,Id_FilialOrigDest,Id_FormaPgto,Solicitante," +
                       "Autorizado,DtEntSai,TpFrete,Id_CFOP,VlrSubTotal,VlrDesconto,VlrTotal,B_Icms,VlrIcms,B_IcmsSub,VlrIcmsSub,VlrFrete,VlrSeguro,VlrOutrasDesp,VlrIpi,Observacao,Status,ObsSelo,ChaveNFE,"+
                       "TipoPgto,DtEmissao,OutrosIPI,Conferido,Id_MovChave,NtServico,NFeSerie,Id_Usuario,Pendencia,ObsPendencia,TpAvaria) " +
                       "Values (@Id,Convert(DateTime,@Data,103),@TpMov,@IdPedCompra,@IdPessoa,@Documento,@NumDocumento,@NumFormulario,@IdFilial,@IdFilialOrigDest,@IdFormaPgto,@Solicitante," +
                       "@Autorizado,Convert(DateTime,@DtEntSai,103),@TpFrete,@IdCFOP,@VlrSubTotal,@VlrDesconto,@VlrTotal,@BIcms,@VlrIcms,@BIcmsSub,@VlrIcmsSub,@VlrFrete,@VlrSeguro," +
                       "@VlrOutraDesp,@VlrIpi,@Observacao,@Status,@ObsSelo,@ChaveNFE,@TipoPgto,Convert(DateTime,@DtEmissao,103),@OutrosIPI,@Conferido,@IdMovChave,@NtServico,@NFeSerie,@IdUsuario,@Pendencia,@ObsPendencia,@TpAvaria)";
            }

            if (sSQL != "")
            {   
                // Calcular os Impostos
                //CalcularImposto();
                VlrTotal = VlrTotal + VlrIpi + VlrOutraDesp + VlrIcmsSub + OutrosIPI;
                //------------------
                Nm_param.Add("@Id");               Vr_param.Add(IdMov);
                Nm_param.Add("@Data");             Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@TpMov");            Vr_param.Add(TpMov);
                Nm_param.Add("@IdPedCompra");      Vr_param.Add(IdPedCompra);
                Nm_param.Add("@IdPessoa");         Vr_param.Add(IdPessoa);                
                Nm_param.Add("@Documento");        Vr_param.Add(Documento);
                Nm_param.Add("@NumDocumento");     Vr_param.Add(NumDocumento);
                Nm_param.Add("@NumFormulario");    Vr_param.Add(NumFormulario);
                Nm_param.Add("@IdFilial");         Vr_param.Add(IdFilial);
                Nm_param.Add("@IdFilialOrigDest"); Vr_param.Add(IdFilialOrigDest);
                Nm_param.Add("@IdFormaPgto");      Vr_param.Add(IdFormaPgto);
                Nm_param.Add("@Solicitante");      Vr_param.Add(Solicitante);
                Nm_param.Add("@Autorizado");       Vr_param.Add(Autorizado);
                Nm_param.Add("@DtEntSai");         Vr_param.Add(DtEntSai.ToShortDateString());
                Nm_param.Add("@TpFrete");          Vr_param.Add(TpFrete);                
                Nm_param.Add("@IdCfop");           Vr_param.Add(IdCFOP);
                Nm_param.Add("@VlrSubTotal");      Vr_param.Add(Controle.FloatToStr(VlrSubTotal,2));
                Nm_param.Add("@VlrDesconto");      Vr_param.Add(Controle.FloatToStr(VlrDesconto,2));
                Nm_param.Add("@VlrTotal");         Vr_param.Add(Controle.FloatToStr(VlrTotal,2));
                Nm_param.Add("@BIcms");            Vr_param.Add(Controle.FloatToStr(BIcms,2));
                Nm_param.Add("@VlrIcms");          Vr_param.Add(Controle.FloatToStr(VlrIcms,2));
                Nm_param.Add("@BIcmsSub");         Vr_param.Add(Controle.FloatToStr(BIcmsSub,2));
                Nm_param.Add("@VlrIcmsSub");       Vr_param.Add(Controle.FloatToStr(VlrIcmsSub,2));
                Nm_param.Add("@VlrFrete");         Vr_param.Add(Controle.FloatToStr(VlrFrete,2));
                Nm_param.Add("@VlrSeguro");        Vr_param.Add(Controle.FloatToStr(VlrSeguro,2));
                Nm_param.Add("@VlrOutraDesp");     Vr_param.Add(Controle.FloatToStr(VlrOutraDesp,2));
                Nm_param.Add("@OutrosIPI");        Vr_param.Add(Controle.FloatToStr(OutrosIPI, 2));
                Nm_param.Add("@VlrIpi");           Vr_param.Add(Controle.FloatToStr(VlrIpi,2));
                Nm_param.Add("@Observacao");       Vr_param.Add(Observacao);
                Nm_param.Add("@ObsSelo");          Vr_param.Add(ObsSelo);
                Nm_param.Add("@Status");           Vr_param.Add(Status);
                Nm_param.Add("@ChaveNFE");         Vr_param.Add(ChaveNFE);
                Nm_param.Add("@TipoPgto");         Vr_param.Add(TipoPgto);
                Nm_param.Add("@DtEmissao");        Vr_param.Add(DtEmissao.ToShortDateString());
                Nm_param.Add("@Conferido");        Vr_param.Add(Conferido);
                Nm_param.Add("@IdMovChave");       Vr_param.Add(IdMovChave);
                Nm_param.Add("@NtServico");        Vr_param.Add(NtServico);
                Nm_param.Add("@NFeSerie");         Vr_param.Add(NFeSerie);
                Nm_param.Add("@IdUsuario");        Vr_param.Add(IdUsuario);
                Nm_param.Add("@Pendencia");        Vr_param.Add(Pendencia);
                Nm_param.Add("@ObsPendencia");     Vr_param.Add(ObsPendencia);
                Nm_param.Add("@TpAvaria");         Vr_param.Add(TpAvaria);
                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdMov > 0)
            {
                Controle.ExecutaSQL("DELETE FROM MvEstoque        WHERE Id_Mov=" + IdMov.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM MvEstoqueItens   WHERE Id_Mov=" + IdMov.ToString().Trim());
            }
        }

        public void Concluir()
        {
            Controle.ExecutaSQL("UPDATE MvEstoque Set Status=1,Id_FormaPgto="+IdFormaPgto.ToString()+" Where Id_Mov=" + IdMov.ToString());
            Status = 1;
        }
        public void Cancelar()
        {
            if (TpMov == "S_TRA")
                Controle.ExecutaSQL("UPDATE MvEstoque Set ID_MovChave=0,Status=0,Id_FormaPgto=0 Where Id_Mov=" + IdMov.ToString());
            else
                Controle.ExecutaSQL("UPDATE MvEstoque Set Status=0,Id_FormaPgto=0 Where Id_Mov=" + IdMov.ToString());           
            Status = 0;
            IdFormaPgto = 0;
        }
        public void CalcularImposto()
        {
            DataSet Itens = new DataSet();
            Itens = Controle.ConsultaTabela("SELECT * FROM MVESTOQUEITENS WHERE ID_MOV=" + IdMov.ToString());
            decimal T_BIcms = 0;
            decimal V_Icms  = 0;
            decimal V_Ipi   = 0;
            decimal VlrItem = 0;
            if (Itens.Tables[0].Rows.Count > 0)
            {              
                for (int I = 0; I <= Itens.Tables[0].Rows.Count - 1; I++)
                {
                    if (decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms"].ToString()) > 0)
                    {
                        if (decimal.Parse(Itens.Tables[0].Rows[I]["PercRed"].ToString()) > 0)
                            VlrItem = decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString()) - (decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Itens.Tables[0].Rows[I]["PercRed"].ToString()) / 100);
                        else
                            VlrItem = decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString());

                        T_BIcms = T_BIcms + VlrItem;
                        V_Icms = V_Icms + decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms"].ToString());
                    }
                    if (decimal.Parse(Itens.Tables[0].Rows[I]["VlrIpi"].ToString()) > 0)
                        V_Ipi = V_Ipi + decimal.Parse(Itens.Tables[0].Rows[I]["VlrIpi"].ToString());
                }
            }
            BIcms   = T_BIcms;
            VlrIcms = V_Icms;
            VlrIpi  = V_Ipi;
        }
    }
}
