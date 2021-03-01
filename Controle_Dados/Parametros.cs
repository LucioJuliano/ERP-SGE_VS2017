using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using Controle_Dados;
using System.Drawing;

namespace Controle_Dados
{
    public class Parametros
    {
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private int _UsaTabComissao;
        public int UsaTabComissao
        {
            get { return _UsaTabComissao; }
            set { _UsaTabComissao = value; }
        }
        private decimal _Comissao1;
        public decimal Comissao1
        {
            get { return _Comissao1; }
            set { _Comissao1 = value; }
        }
        private decimal _Comissao2;
        public decimal Comissao2
        {
            get { return _Comissao2; }
            set { _Comissao2 = value; }
        }
        private decimal _Comissao3;
        public decimal Comissao3
        {
            get { return _Comissao3; }
            set { _Comissao3 = value; }
        }
        private decimal _Comissao4;
        public decimal Comissao4
        {
            get { return _Comissao4; }
            set { _Comissao4 = value; }
        }
        private int _EstoqueZero;
        public int EstoqueZero
        {
            get { return _EstoqueZero; }
            set { _EstoqueZero = value; }
        }
        private int _ClienteAtraso;
        public int ClienteAtraso
        {
            get { return _ClienteAtraso; }
            set { _ClienteAtraso = value; }
        }
        private int _LimiteCredito;
        public int LimiteCredito
        {
            get { return _LimiteCredito; }
            set { _LimiteCredito = value; }
        }
        private int _NotaFiscal;
        public int NotaFiscal
        {
            get { return _NotaFiscal; }
            set { _NotaFiscal = value; }
        }
        private int _Formulario;
        public int Formulario
        {
            get { return _Formulario; }
            set { _Formulario = value; }
        }
        private int _NFE;
        public int NFE
        {
            get { return _NFE; }
            set { _NFE = value; }
        }
        private int _NotaIPI;
        public int NotaIPI
        {
            get { return _NotaIPI; }
            set { _NotaIPI = value; }
        }
        private int _LinhasNota;
        public int LinhasNota
        {
            get { return _LinhasNota; }
            set { _LinhasNota = value; }
        }
        private int _IdConsumidor;
        public int IdConsumidor
        {
            get { return _IdConsumidor; }
            set { _IdConsumidor = value; }
        }
        private int _WSClienteAtraso;
        public int WSClienteAtraso
        {
            get { return _WSClienteAtraso; }
            set { _WSClienteAtraso = value; }
        }       
        private int _NFEAmbiente;
        public int NFEAmbiente
        {
            get { return _NFEAmbiente; }
            set { _NFEAmbiente = value; }
        }
        private string _Certificado;
        public string Certificado
        {
            get { return _Certificado; }
            set { _Certificado = value; }
        }
        private decimal _PercPIS;
        public decimal PercPIS
        {
            get { return _PercPIS; }
            set { _PercPIS = value; }
        }
        private decimal _PercCOFINS;
        public decimal PercCOFINS
        {
            get { return _PercCOFINS; }
            set { _PercCOFINS = value; }
        }
        private int _NFEVersao;
        public int NFEVersao
        {
            get { return _NFEVersao; }
            set { _NFEVersao = value; }
        }
        private int _WSNumNFE;
        public int WSNumNFE
        {
            get { return _WSNumNFE; }
            set { _WSNumNFE = value; }
        }
        private int _WSCadPessoa;
        public int WSCadPessoa
        {
            get { return _WSCadPessoa; }
            set { _WSCadPessoa = value; }
        }
        private int _NotaNFE;
        public int NotaNFE
        {
            get { return _NotaNFE; }
            set { _NotaNFE = value; }
        }
        private int _FormularioNFE;
        public int FormularioNFE
        {
            get { return _FormularioNFE; }
            set { _FormularioNFE = value; }
        }

        private int _CliDiasInativo;
        public int CliDiasInativo
        {
            get { return _CliDiasInativo; }
            set { _CliDiasInativo = value; }
        }

        private string _Smtp;
        public string Smtp
        {
            get { return _Smtp; }
            set { _Smtp = value; }
        }
        private int _Porta;
        public int Porta
        {
            get { return _Porta; }
            set { _Porta = value; }
        }        
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Senha;
        public string Senha
        {
            get { return _Senha; }
            set { _Senha = value; }
        }        
        
        private decimal _InssFaixa1;
        public decimal InssFaixa1
        {
            get { return _InssFaixa1; }
            set { _InssFaixa1 = value; }
        }
        private decimal _InssPerc1;
        public decimal InssPerc1
        {
            get { return _InssPerc1; }
            set { _InssPerc1 = value; }
        }

        private decimal _InssFaixa2;
        public decimal InssFaixa2
        {
            get { return _InssFaixa2; }
            set { _InssFaixa2 = value; }
        }
        private decimal _InssPerc2;
        public decimal InssPerc2
        {
            get { return _InssPerc2; }
            set { _InssPerc2 = value; }
        }

        private decimal _InssFaixa3;
        public decimal InssFaixa3
        {
            get { return _InssFaixa3; }
            set { _InssFaixa3 = value; }
        }
        private decimal _InssPerc3;
        public decimal InssPerc3
        {
            get { return _InssPerc3; }
            set { _InssPerc3 = value; }
        }

        private string _Release;
        public string Release
        {
            get { return _Release; }
            set { _Release = value; }
        }
        private int _VerCancBxFin;
        public int VerCancBxFin
        {
            get { return _VerCancBxFin; }
            set { _VerCancBxFin = value; }
        }

        private int _IdEntregador;
        public int IdEntregador
        {
            get { return _IdEntregador; }
            set { _IdEntregador = value; }
        }

        private string _ObsNF;
        public string ObsNF
        {
            get { return _ObsNF; }
            set { _ObsNF = value; }
        }
        private int _EmissorCF;
        public int EmissorCF
        {
            get { return _EmissorCF; }
            set { _EmissorCF = value; }
        }
        private DateTime _UltDataAtlz;
        public DateTime UltDataAtlz
        {
            get { return _UltDataAtlz; }
            set { _UltDataAtlz = value; }
        }

        private int _NumNFce;
        public int NumNFce
        {
            get { return _NumNFce; }
            set { _NumNFce = value; }
        }

        private int _NumCFe;
        public int NumCFe
        {
            get { return _NumCFe; }
            set { _NumCFe = value; }
        }

        private string _CodigoMFe;
        public string CodigoMFe
        {
            get { return _CodigoMFe; }
            set { _CodigoMFe = value; }
        }
        private string _ChaveMFe;
        public string ChaveMFe
        {
            get { return _ChaveMFe; }
            set { _ChaveMFe = value; }
        }
        private string _ChaveRequisicao;
        public string ChaveRequisicao
        {
            get { return _ChaveRequisicao; }
            set { _ChaveRequisicao = value; }
        }
        private string _ChaveValidador;
        public string ChaveValidador
        {
            get { return _ChaveValidador; }
            set { _ChaveValidador = value; }
        }
        private string _SerialPOS;
        public string SerialPOS
        {
            get { return _SerialPOS; }
            set { _SerialPOS = value; }
        }

       /* private string _Logo;
        public string Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }*/

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdFilial = 0;
            UsaTabComissao = 0;
            Comissao1 = 0;
            Comissao2 = 0;
            Comissao3 = 0;
            Comissao4 = 0;
            EstoqueZero = 0;
            ClienteAtraso = 0;
            LimiteCredito = 0;
            NotaFiscal = 0;
            Formulario = 0;
            NFE = 0;
            NotaIPI = 0;
            LinhasNota = 0;
            IdConsumidor = 0;
            WSClienteAtraso = 0;
            NFEAmbiente = 0;
            PercCOFINS = 0;
            PercPIS = 0;
            NFEVersao = 0;
            Certificado = "";
            WSCadPessoa = 0;
            NotaNFE = 0;
            FormularioNFE = 0;            
            WSNumNFE = 0;
            Smtp = "";
            Porta = 0;
            Email = "";
            Senha = "";
            CliDiasInativo = 0;
            InssFaixa1 = 0;
            InssPerc1  = 0;
            InssFaixa2 = 0;
            InssPerc2  = 0;
            InssFaixa3 = 0;
            InssPerc3  = 0;
            Release = "";
            VerCancBxFin = 0;
            IdEntregador = 0;
            ObsNF = "";
            EmissorCF = 0;
            NumNFce = 0;
            ChaveMFe = "";
            CodigoMFe = "";
            ChaveRequisicao = "";
            ChaveValidador = "";
            SerialPOS = "";
            NumCFe = 0;
           // Logo = "";

            UltDataAtlz = DateTime.Now;

            if (Id > 0)
            {                
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PARAMETROS WHERE Id_FILIAL=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdFilial = Id;
                    UsaTabComissao  = int.Parse(Tabela["UsaTabComissao"].ToString());
                    Comissao1       = decimal.Parse(Tabela["Comissao1"].ToString());
                    Comissao2       = decimal.Parse(Tabela["Comissao2"].ToString());
                    Comissao3       = decimal.Parse(Tabela["Comissao3"].ToString());
                    Comissao4       = decimal.Parse(Tabela["Comissao4"].ToString());
                    EstoqueZero     = int.Parse(Tabela["EstoqueZero"].ToString());
                    ClienteAtraso   = int.Parse(Tabela["ClienteAtraso"].ToString());
                    LimiteCredito   = int.Parse(Tabela["LimiteCredito"].ToString());
                    NotaFiscal      = int.Parse(Tabela["NotaFiscal"].ToString());
                    Formulario      = int.Parse(Tabela["Formulario"].ToString());
                    NFE             = int.Parse(Tabela["NFE"].ToString());
                    NotaIPI         = int.Parse(Tabela["NotaIPI"].ToString());
                    LinhasNota      = int.Parse(Tabela["LinhasNota"].ToString());
                    IdConsumidor    = int.Parse(Tabela["ID_Consumidor"].ToString());
                    WSClienteAtraso = int.Parse(Tabela["WS_ClienteAtraso"].ToString());
                    NFEAmbiente     = int.Parse(Tabela["NFE_Ambiente"].ToString());
                    Certificado     = Tabela["Certificado"].ToString().Trim();
                    PercPIS         = decimal.Parse(Tabela["Perc_PIS"].ToString());
                    PercCOFINS      = decimal.Parse(Tabela["Perc_COFINS"].ToString());
                    NFEVersao       = int.Parse(Tabela["NFE_Versao"].ToString());
                    WSCadPessoa     = int.Parse(Tabela["WS_CadPessoa"].ToString());
                    NotaNFE         = int.Parse(Tabela["NotaNFE"].ToString());
                    FormularioNFE   = int.Parse(Tabela["FormularioNFE"].ToString());
                    Smtp            = Tabela["SMTP"].ToString().Trim();
                    Email           = Tabela["EMAIL"].ToString().Trim();
                    Senha           = Tabela["Senha"].ToString().Trim();
                    Release         = Tabela["Release"].ToString().Trim();
                    InssFaixa1      = decimal.Parse(Tabela["InssFaixa1"].ToString());
                    InssPerc1       = decimal.Parse(Tabela["InssPerc1"].ToString());
                    InssFaixa2      = decimal.Parse(Tabela["InssFaixa2"].ToString());
                    InssPerc2       = decimal.Parse(Tabela["InssPerc2"].ToString());
                    InssFaixa3      = decimal.Parse(Tabela["InssFaixa3"].ToString());
                    InssPerc3       = decimal.Parse(Tabela["InssPerc3"].ToString());
                    ObsNF           = Tabela["ObsNF"].ToString().Trim();
                    CodigoMFe       = Tabela["CodigoMFe"].ToString().Trim();
                    ChaveMFe        = Tabela["ChaveMFe"].ToString().Trim();
                    ChaveRequisicao  = Tabela["ChaveRequisicao"].ToString().Trim();
                    ChaveValidador  = Tabela["ChaveValidador"].ToString().Trim();
                    SerialPOS       = Tabela["SerialPOS"].ToString().Trim();
                   // Logo            = Tabela["Logo"].ToString().Trim();

                    if (Tabela["WSNumNFE"].ToString() != "")
                        WSNumNFE = int.Parse(Tabela["WSNumNFE"].ToString());

                    if (Tabela["Cli_DiasInativo"].ToString() != "")
                        CliDiasInativo = int.Parse(Tabela["Cli_DiasInativo"].ToString());

                    if (Tabela["Porta"].ToString() != "")
                        Porta = int.Parse(Tabela["Porta"].ToString());

                    if (Tabela["VerCancBxFin"].ToString() != "")
                        VerCancBxFin = int.Parse(Tabela["VerCancBxFin"].ToString());

                    if (Tabela["Id_Entregador"].ToString() != "")
                        IdEntregador = int.Parse(Tabela["Id_Entregador"].ToString());

                    if (Tabela["EmissorCF"].ToString() != "")
                        EmissorCF = int.Parse(Tabela["EmissorCF"].ToString());

                    if (Tabela["UltDataAtlz"].ToString() != "")
                        UltDataAtlz = DateTime.Parse(Tabela["UltDataAtlz"].ToString());

                    if (Tabela["NumNFce"].ToString() != "")
                        NumNFce = int.Parse(Tabela["NumNFce"].ToString());
                    if (Tabela["NumCFe"].ToString() != "")
                        NumCFe = int.Parse(Tabela["NumCFe"].ToString());

                }             
            }            
        }
        public void GravarDados(bool Incluir)
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (!Incluir)
            {
                sSQL = "UPDATE PARAMETROS SET Id_FILIAL=@Id,USATABCOMISSAO=@USATABCOMISSAO,COMISSAO1=@COMISSAO1,COMISSAO2=@COMISSAO2,COMISSAO3=@COMISSAO3,COMISSAO4=@COMISSAO4," +
                       "ESTOQUEZERO=@ESTOQUEZERO,CLIENTEATRASO=@CLIENTEATRASO,LIMITECREDITO=@LIMITECREDITO,NOTAFISCAL=@NOTAFISCAL,FORMULARIO=@FORMULARIO,NFE=@NFE,NotaIPI=@NotaIPI,"+
                       "LinhasNota=@LinhasNota,Id_Consumidor=@IdConsumidor,WS_ClienteAtraso=@WSClienteAtraso,Nfe_Ambiente=@NFEAmbiente,Certificado=@Certificado,Perc_Pis=@PercPis,"+
                       "Perc_Cofins=@PercCofins,NFE_Versao=@NFEVersao,WS_CadPessoa=@WSCadPessoa,NotaNFE=@NotaNFE,FormularioNFE=@FormularioNFE,WSNumNFE=@WSNumNFE,Smtp=@Smtp,Porta=@Porta,"+
                       "Email=@Email,Senha=@Senha,Cli_DiasInativo=@CliDiasInativo,InssFaixa1=@InssFaixa1,InssPerc1=@InssPerc1,InssFaixa2=@InssFaixa2,InssPerc2=@InssPerc2,InssFaixa3=@InssFaixa3,"+
                       "InssPerc3=@InssPerc3,Release=@Release,VerCancBxFin=@VerCancBxFin,Id_Entregador=@IdEntregador,ObsNF=@ObsNF,EmissorCF=@EmissorCF,NumNFce=@NumNFce,"+
                       "CodigoMFE=@CodigoMFe,ChaveMFE=@ChaveMFe,ChaveRequisicao=@ChaveRequisicao,ChaveValidador=@ChaveValidador,SerialPOS=@SerialPOS,NumCFe=@NumCFe Where Id_FILIAL=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdFilial);
            }
            else
            {
                sSQL = "INSERT INTO PARAMETROS (Id_FILIAL,USATABCOMISSAO,COMISSAO1,COMISSAO2,COMISSAO3,COMISSAO4,ESTOQUEZERO,CLIENTEATRASO,LIMITECREDITO,NOTAFISCAL,FORMULARIO,NFE," +
                       "NotaIPI,LinhasNota,Id_Consumidor,WS_ClienteAtraso,Nfe_Ambiente,Certificado,Perc_Pis,Perc_Cofins,NFE_Versao,WS_CadPessoa,NotaNFE,FormularioNFE,WSNumNFE,Smtp,Porta,Email,Senha,"+
                       "Cli_DiasInativo,InssFaixa1,InssPerc1,InssFaixa2,InssPerc2,InssFaixa3,InssPerc3,Release,VerCancBxFin,Id_Entregador,ObsNF,EmissorCF,NumNFce,CodigoMFE,ChaveMFE,ChaveRequisicao,ChaveValidador,SerialPOS,NumCFe) " +
                       "Values (@Id,@USATABCOMISSAO,@COMISSAO1,@COMISSAO2,@COMISSAO3,@COMISSAO4,@ESTOQUEZERO,@CLIENTEATRASO,@LIMITECREDITO,@NOTAFISCAL,@FORMULARIO,@NFE,@NotaIPI,@LinhasNota," +
                       "@IdConsumidor,@WSClienteAtraso,@NFEAmbiente,@Certificado,@PercPis,@PercCofins,@NFEVersao,@WSCadPessoa,@NotaNFE,@FormularioNFE,@WSNumNFE,@Smtp,@Porta,@Email,@Senha,@CliDiasInativo,"+
                       "@InssFaixa1,@InssPerc1,@InssFaixa2,@InssPerc2,@InssFaixa3,@InssPerc3,@Release,@VerCancBxFin,@IdEntregador,@ObsNF,@EmissorCF,@NumNFce,@CodigoMFe,@ChaveMFe,@ChaveRequisicao,@ChaveValidador,@SerialPOS,@NumCFe)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdFilial);
                Nm_param.Add("@USATABCOMISSAO"); Vr_param.Add(UsaTabComissao);
                Nm_param.Add("@COMISSAO1"); Vr_param.Add(Controle.FloatToStr(Comissao1,2));
                Nm_param.Add("@COMISSAO2"); Vr_param.Add(Controle.FloatToStr(Comissao2,2));
                Nm_param.Add("@COMISSAO3"); Vr_param.Add(Controle.FloatToStr(Comissao3,2));
                Nm_param.Add("@COMISSAO4"); Vr_param.Add(Controle.FloatToStr(Comissao4,2));
                Nm_param.Add("@ESTOQUEZERO"); Vr_param.Add(EstoqueZero);
                Nm_param.Add("@CLIENTEATRASO"); Vr_param.Add(ClienteAtraso);
                Nm_param.Add("@LIMITECREDITO"); Vr_param.Add(LimiteCredito);
                Nm_param.Add("@NOTAFISCAL"); Vr_param.Add(NotaFiscal);
                Nm_param.Add("@FORMULARIO"); Vr_param.Add(Formulario);
                Nm_param.Add("@NFE"); Vr_param.Add(NFE);
                Nm_param.Add("@NotaIPI"); Vr_param.Add(NotaIPI);
                Nm_param.Add("@LinhasNota"); Vr_param.Add(LinhasNota);
                Nm_param.Add("@IdConsumidor"); Vr_param.Add(IdConsumidor);
                Nm_param.Add("@WSClienteAtraso"); Vr_param.Add(WSClienteAtraso);
                Nm_param.Add("@NFEAmbiente"); Vr_param.Add(NFEAmbiente);
                Nm_param.Add("@Certificado"); Vr_param.Add(Certificado);
                Nm_param.Add("@PercPIS"); Vr_param.Add(Controle.FloatToStr(PercPIS, 2));
                Nm_param.Add("@PercCOFINS"); Vr_param.Add(Controle.FloatToStr(PercCOFINS, 2));
                Nm_param.Add("@NFEVersao"); Vr_param.Add(NFEVersao);
                Nm_param.Add("@WSCadPessoa"); Vr_param.Add(WSCadPessoa);
                Nm_param.Add("@NotaNFE"); Vr_param.Add(NotaNFE);
                Nm_param.Add("@FormularioNFE"); Vr_param.Add(FormularioNFE);
                Nm_param.Add("@WSNumNFE"); Vr_param.Add(WSNumNFE);
                Nm_param.Add("@Smtp"); Vr_param.Add(Smtp);
                Nm_param.Add("@Porta"); Vr_param.Add(Porta);
                Nm_param.Add("@Email"); Vr_param.Add(Email);
                Nm_param.Add("@Senha"); Vr_param.Add(Senha);
                Nm_param.Add("@CliDiasInativo"); Vr_param.Add(CliDiasInativo);                
                Nm_param.Add("@InssFaixa1"); Vr_param.Add(Controle.FloatToStr(InssFaixa1, 2));
                Nm_param.Add("@InssPerc1");  Vr_param.Add(Controle.FloatToStr(InssPerc1, 2));
                Nm_param.Add("@InssFaixa2"); Vr_param.Add(Controle.FloatToStr(InssFaixa2, 2));
                Nm_param.Add("@InssPerc2");  Vr_param.Add(Controle.FloatToStr(InssPerc2, 2));
                Nm_param.Add("@InssFaixa3"); Vr_param.Add(Controle.FloatToStr(InssFaixa3, 2));
                Nm_param.Add("@InssPerc3");  Vr_param.Add(Controle.FloatToStr(InssPerc3, 2));
                Nm_param.Add("@Release"); Vr_param.Add(Release);
                Nm_param.Add("@VerCancBxFin"); Vr_param.Add(VerCancBxFin);
                Nm_param.Add("@IdEntregador"); Vr_param.Add(IdEntregador);                
                Nm_param.Add("@ObsNF"); Vr_param.Add(ObsNF);
                Nm_param.Add("@EmissorCF"); Vr_param.Add(EmissorCF);
                Nm_param.Add("@NumNFce"); Vr_param.Add(NumNFce);
                Nm_param.Add("@CodigoMFe"); Vr_param.Add(CodigoMFe);
                Nm_param.Add("@ChaveMFe"); Vr_param.Add(ChaveMFe);
                Nm_param.Add("@ChaveRequisicao"); Vr_param.Add(ChaveRequisicao);
                Nm_param.Add("@ChaveValidador"); Vr_param.Add(ChaveValidador);
                Nm_param.Add("@SerialPOS"); Vr_param.Add(SerialPOS);
                Nm_param.Add("@NumCFe"); Vr_param.Add(NumCFe);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);            
            }
        }
        public void Excluir()
        {
            if (IdFilial > 0)
            {
                Controle.ExecutaSQL("DELETE FROM PARAMETROS WHERE Id_Filial=" + IdFilial.ToString().Trim());
            }
        }
        public void ProxNotaFiscal(int Id, bool NFE)
        {
            LerDados(Id);

            if (IdFilial > 0)
            {
                if (NFE)
                {                    
                    NotaNFE = NotaNFE + 1;
                    FormularioNFE = FormularioNFE + 1;
                }
                else
                {
                    NotaFiscal = NotaFiscal + 1;
                    Formulario = Formulario + 1;
                }
                GravarDados(false);
            }
            else
            {
                IdFilial = Id;
                if (NFE)
                {
                    NotaNFE = NotaNFE + 1;
                    FormularioNFE = FormularioNFE + 1;
                }
                else
                {
                    NotaFiscal = 1;
                    Formulario = 1;
                }
                GravarDados(true);
            }
        }
        public void ProxNFce(int Id)
        {
            LerDados(Id);
            if (IdFilial > 0)
            {
                NumNFce = NumNFce + 1;
                GravarDados(false);
            }            
        }
        public void ProxCFe(int Id)
        {
            LerDados(Id);
            if (IdFilial > 0)
            {
                NumCFe = NumCFe + 1;
                GravarDados(false);
            }
        }

    }
}
