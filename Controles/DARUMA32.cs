using System;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using DARUMA32_CSharp;

namespace DARUMA32_CSharp
{
    /// <summary>
    /// Classe Para uso de Produtos DARUMA FRAMEWORK DARUMA32.DLL
    /// FrameWork de comunicacao que interliga todos os produtos DARUMA Automacao, Telecom e SmartCard
    /// 
    /// </summary>
    public class DARUMA32
    {
        //Var Utilizadas pela Classe
        //public static int Int_Retorno;
        public static int iACK, iST1, iST2, iRET, iValue;
        public static short shortValue;

        public static string sRetorno;
        public static int Int_Retorno;
        public static string sBuffer, sBuffer2;
        public static string Str_Label, Str_Texto, Str_Retorno_InputBox;

        
        public DARUMA32()
        {
            Daruma_Registry_StatusFuncao("1");
            
        }
        //InputBox
        public static string InputBox(string LB_Label, string TX_Texto)
        {

            Str_Label = string.Empty;
            Str_Texto = string.Empty;
            Str_Retorno_InputBox = string.Empty;

            Str_Label = LB_Label;
            Str_Texto = TX_Texto;     
          
            //Daruma_Framework_InputBox Form_Daruma = new Daruma_Framework_InputBox();
            //Form_Daruma.ShowDialog();

            return Str_Retorno_InputBox;
        }

 #region Declarações de Metodos de Acesso ao ECF

        #region Metodos de Acesso ao Registry

        //Metodos de Acesso ao Registry
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Porta(System.String FlagPorta);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Path(System.String FlagPath);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Status(System.String FlagStatus);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_StatusFuncao(System.String FlagStatusFuncao);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Retorno(System.String FlagRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_ControlePorta(System.String FlagControlePorta);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_ModoGaveta(System.String FlagConfigRede);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_ConfigRede(System.String FlagModoGaveta);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Log(System.String FlagLog);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_NomeLog(System.String FlagNomeLog);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Emulador(System.String FlagEmulador);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Separador(System.String CharSeparador);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_SeparaMsgPromo(System.String FlagSeparaMsgPromo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_ZAutomatica(System.String FlagZAutomatica);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_XAutomatica(System.String FlagXAutomatica);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_VendeItemUmaLinha(System.String FlagVendeUmaLinha);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Default();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_ImprimeRegistry(System.String NomeProduto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_RetornaValor(System.String Produto, System.String Chave, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Valor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_AlteraRegistry(System.String cValue, System.String cValor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_TerminalServer(System.String tserver);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_ErroExtendidoOk(System.String eextok);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_AbrirDiaFiscal(System.String abredia);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_VendaAutomatica(System.String vauto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_IgnorarPoucoPapel(System.String IgnorarPoucoPapel);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_Velocidade(System.String Velocidade);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_LogTamMaxMB(System.String LogTamMaxMB);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_MFD_ArredondaValor(System.String Flag);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_MFD_ArredondaQuantidade(System.String Flag);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_AplMensagem1(System.String AplMsg1);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_AplMensagem2(System.String AplMsg2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_NumeroSerieNaoFormatado(System.String Flag);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_TEF_NumeroLinhasImpressao(System.String NumeroLinhas);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_MFD_ProgramarSinalSonoro(System.String Flag, System.String FlagNumeroBeeps);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_SintegraSeparador(System.String Separador);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_SintegraPath(System.String Path);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_SintegraUF(System.String UF);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_TextoExtra(System.String TextoEtra);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_MFD_LegendaInmetro(System.String LegendaInmetro);
        #endregion

        # region Metodos de Acesso aos Comandos de Cupom Fiscal
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AbreCupom(System.String CPF_ou_CNPJ);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VendeItem(System.String Codigo, System.String Descricao, System.String Aliquota, System.String TipoQuantidade, System.String Quantidade, int CasasDecimais, System.String Vlr_Unitario, System.String TipoDesconto, System.String Desconto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VendeItemDepartamento(System.String Codigo, System.String Descricao, System.String Aliquota, System.String ValorUnitario, System.String Quantidade, System.String ValorAcrescimo, System.String ValorDesconto, System.String IndiceDepartamento, System.String UnidadeMedida);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VendeItemTresDecimais(string Codigo, string Descricao, string Aliquota, string Quantidade, string Vlr_Unitario, string Acres_ou_Desc, string Percentual_Acresc_Desc);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CancelaItemAnterior();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CancelaItemGenerico(string NumeroItem);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_IniciaFechamentoCupom(string cAcrescimoDesconto, string cTipoAcrescimoDesconto, string cValorAcrescimoDesconto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EfetuaFormaPagamento(string cFormaPagamento, string cValorFormaPagamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EfetuaFormaPagamentoDescricaoForma(string cFormaPagamento, string cValorFormaPagamento, string cTextoLivre);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_IdentificaConsumidor(string cNome, string cEndereco, string cCpf_ou_Cnpj);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_TerminaFechamentoCupom(string cMensagem);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FechaCupom(string FormaPagamento, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorPago, string Mensagem);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FechaCupomResumido(string FormaPagamento, string Mensagem);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EmitirCupomAdicional();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CancelaCupom();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AumentaDescricaoItem(string Descricao);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_UsaUnidadeMedida(string UnidadeMedida);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EstornoFormasPagamento(string FormaOrigem, string FormaDestino, string Valor);
#endregion

        #region Metodos Nao Fiscal e Vinculados
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RecebimentoNaoFiscal(string IndiceTotalizador, string ValorRecebimento, string FormaPagamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AbreRecebimentoNaoFiscal(string IndiceTotalizador, string Acrescimo_ou_Desconto, string Tipo_Acrescimo_ou_Desconto,string Valor_Acrescimo_ou_Desconto, string ValorRecebimento, string TextoLivre);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EfetuaFormaPagamentoNaoFiscal(string FormaPagamento, string ValorFormaPagamento, string TextoLivre);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AbreComprovanteNaoFiscalVinculado(string FormaPagamento, string Valor, string NumeroCupom);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_UsaComprovanteNaoFiscalVinculado(string Texto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FechaComprovanteNaoFiscalVinculado();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Sangria(string Valor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Suprimento(string Valor, string FormaPagamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FundoCaixa(string ValorFundoCaixa, string FormaPagamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AbreRelatorioGerencial();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EnviarTextoCNF(string texto);
#endregion

        #region Metodos de Acesso A Leitura da Memoria Fiscal
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LeituraMemoriaFiscalData(string DataInicial, string DataFinal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LeituraMemoriaFiscalReducao(string ReducaoInicial, string ReducaoFinal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LeituraMemoriaFiscalSerialReducao(string ReducaoInicial, string ReducaoFinal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LeituraMemoriaFiscalSerialData(string DataInicial, string DataFinal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FechaRelatorioGerencial();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RelatorioGerencial(string Texto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ReducaoZ(string cData, string cHora);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ReducaoZAjustaDataHora(string cData, string cHora);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LeituraX();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LeituraXSerial();

#endregion

        #region Metodos de Informacoes da Impressora
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string VarRetNumeroCupom);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornoImpressora(ref int iACK, ref int iST1, ref int iST2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaEstadoImpressora(ref int iAckNak, ref int iST1, ref int iST2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornoAliquotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string Aliquotas);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaTotalizadoresParciais([MarshalAs(UnmanagedType.VBByRefStr)] ref string cTotalizadores);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_SubTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string SubTotal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Troco([MarshalAs(UnmanagedType.VBByRefStr)] ref string Trocro);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_SaldoAPagar([MarshalAs(UnmanagedType.VBByRefStr)] ref string Saldo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_MonitoramentoPapel([MarshalAs(UnmanagedType.VBByRefStr)] ref string LinhasImpressas);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_DadosUltimaReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string DadosReducao);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_UltimaFormaPagamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string FormaPagamento, [MarshalAs(UnmanagedType.VBByRefStr)]  ref string ValorForma);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_TipoUltimoDocumento([MarshalAs(UnmanagedType.VBByRefStr)]  ref string TipoUltimoDoc);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroSerie([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerie);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VersaoFirmware([MarshalAs(UnmanagedType.VBByRefStr)] ref string VersaoFirmware);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CGC_IE([MarshalAs(UnmanagedType.VBByRefStr)] ref string CGC, [MarshalAs(UnmanagedType.VBByRefStr)] ref string IE);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_GrandeTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string GrandeTotal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VendaBruta([MarshalAs(UnmanagedType.VBByRefStr)] ref string VendaBruta);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VendaBrutaAcumulada([MarshalAs(UnmanagedType.VBByRefStr)] ref string VendaBrutaAcumulada);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Descontos([MarshalAs(UnmanagedType.VBByRefStr)] ref string Descontos);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Cancelamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string Cancelamentos);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroOperacoesNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Operacoes);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroCuponsCancelados([MarshalAs(UnmanagedType.VBByRefStr)] ref string CuponsCancelados);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroReducoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string Reducoes);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroIntervencoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string Intervencoes);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroSubstituicoesProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string Substituicoes);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_UltimoItemVendido([MarshalAs(UnmanagedType.VBByRefStr)] ref string UltimoItem);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ClicheProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string ClicheProprietario);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ClicheProprietarioEx([MarshalAs(UnmanagedType.VBByRefStr)] ref string ClicheProprietario);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroCaixa([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCaixa);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NumeroLoja([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroLoja);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_SimboloMoeda([MarshalAs(UnmanagedType.VBByRefStr)] ref string SimboloMoeda);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FlagsFiscais(ref int FlagFiscal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_MinutosLigada([MarshalAs(UnmanagedType.VBByRefStr)] ref string MinutosLigada);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_MinutosImprimindo([MarshalAs(UnmanagedType.VBByRefStr)] ref string MinutosImprimindo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaModoOperacao([MarshalAs(UnmanagedType.VBByRefStr)] ref string cModo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_StatusCupomFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string cStatus);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_StatusComprovanteNaoFiscalVinculado([MarshalAs(UnmanagedType.VBByRefStr)] ref string cStatus);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_StatusComprovanteNaoFiscalNaoVinculado([MarshalAs(UnmanagedType.VBByRefStr)] ref string cStatus);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_StatusRelatorioGerencial([MarshalAs(UnmanagedType.VBByRefStr)] ref string cStatus);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaEpromConectada([MarshalAs(UnmanagedType.VBByRefStr)] ref string FlagEprom);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaZPendente([MarshalAs(UnmanagedType.VBByRefStr)] ref string FlagZPendente);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaXPendente([MarshalAs(UnmanagedType.VBByRefStr)] ref string FlagXPendente);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaDiaAberto([MarshalAs(UnmanagedType.VBByRefStr)] ref string FlagDiaAberto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaHorarioVerao([MarshalAs(UnmanagedType.VBByRefStr)] ref string FlagHorarioVerao);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ValorPagoUltimoCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string Valor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIDataHoraImpressora([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ContadoresTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Contadores);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Totalizadores);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaTotalizadoresNaoFiscaisEx([MarshalAs(UnmanagedType.VBByRefStr)] ref string TotalizadoresEx);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIDataHoraReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string DataReducao, [MarshalAs(UnmanagedType.VBByRefStr)] ref string HoraReducao);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIDataMovimento([MarshalAs(UnmanagedType.VBByRefStr)] ref string DataMovimento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaTruncamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string FlagTruncamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string AliquotasIss);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Acrescimos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorAcrescimo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaFormasPagamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string cFormas);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaFormasPagamentoEx([MarshalAs(UnmanagedType.VBByRefStr)] ref string cFormas);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaDescricaoFormasPagamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string cFormas);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaRecebimentoNaoFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string cRecebimentos);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaTipoImpressora(ref int TipoImpressora);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaModeloECF();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaIndiceAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string cIndices);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ValorFormaPagamento(string cFormaPagamento, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Valor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ValorTotalizadorNaoFiscal( string Totalizador, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Valor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaImpressoraLigada();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_MapaResumo();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RelatorioTipo60Analitico();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RelatorioTipo60Mestre();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_COO([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCooInicial, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cCooFinal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaErroExtendido([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCooInicial);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CRO([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCRO);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_PalavraStatus([MarshalAs(UnmanagedType.VBByRefStr)] ref string psatus);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_PalavraStatusBinario([MarshalAs(UnmanagedType.VBByRefStr)] ref string pstatusbin);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LerAliquotasComIndice([MarshalAs(UnmanagedType.VBByRefStr)] ref string aindice);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornarVersaoDLL([MarshalAs(UnmanagedType.VBByRefStr)] ref string Versao);

        #region Funções Exclusivas para MFD
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_IndicePrimeiroVinculado([MarshalAs(UnmanagedType.VBByRefStr)] ref string aindice);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_CasasDecimaisProgramada([MarshalAs(UnmanagedType.VBByRefStr)] ref string DecimaisQuantidade,ref string DecimaisValor);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_DownloadDaMFD(string COOInicial, string COOFinal);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_RetornaInformacao(string Indice, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Valor);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_TerminaFechamentoCupomCodigoBarras(string Mensagem, string Tipo,string Codigo,string Largura, string Altura,string Posicao );

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_ImprimeCodigoBarras(string Tipo, string Codigo, string Largura, string Altura, string Posicao);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_ImprimeCodigoBarrasCupomAdicional(string Tipo, string Codigo, string Largura, string Altura, string Posicao);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_StatusCupomFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string cStatus_Cupom);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_SinalSonoro(string Str_Valor);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_EqualizarVelocidade(StringBuilder EqualizarVelocidade);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_AcionarGuilhotina();

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_ProgramaRelatoriosGerenciais(string Str_Valor);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_AbreRelatorioGerencial(string Str_Valor);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_EmitirCupomAdicional();

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_RetornaInformacao(string Indice, StringBuilder Valor);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_AbreRecebimentoNaoFiscal(string CPF, string Nome, string Endereco);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_RecebimentoNaoFiscal(string DescricaoTotalizador,string AcresDesc,string TipoAcresDesc, string ValorAcresDesc, string ValorRecebimento);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_IniciaFechamentoNaoFiscal(string AcresDesc, string TipoAcresDesc, string ValorAcresDesc);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_EfetuaFormaPagamentoNaoFiscal(string FormaPgto, string Valor,string Observacao);
        
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_TerminaFechamentoNaoFiscal(string MsgPromo);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_ProgramarGuilhotina(System.String Separacao_Entre_Documentos, System.String Linhas_para_Acionamento_Guilhotina, System.String Status_da_Guilhotina, System.String Impressao_Antecipada_Cliche);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_StatusCupomFiscal(StringBuilder cStatus);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_GrandeTotal(StringBuilder GrandeTotal);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_SubTotal(StringBuilder SubTotal);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_CupomAdicionalDll(System.String CupomAdicional);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_CupomAdicionalDllConfig(System.String CupomAdicional);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_MFD_LeituraMFCompleta(string valor);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_DescontoAcrescimoItem(string NumItemDescontoAcrescimo, string DescAcres, string TipoDescAcres, string ValorDescAcres);



        #region  Bilhete de Passagem

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIB_AbreBilhetePassagem(string Origem, string Destino, string UF, string Percurso, string Prestadora, string Plataforma, string Poltrona, string Modalidade, string Categoria, string DataEmbarque, string PassRg, string PassNome, string PassEndereco);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIB_VendeItem(string Descricao, string St, string Valor, string DescontoAcrescimo, string TipoDesconto, string ValorDesconto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIB_UfOrigem(string UfOrigem);

        #endregion


        #endregion


        #endregion

        #region        Retornos
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaGNF([MarshalAs(UnmanagedType.VBByRefStr)] ref string cGeralNaoFiscal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaValorComprovanteNaoFiscal(string Indice,[MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorComprNaoFiscal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaIndiceComprovanteNaoFiscal(string Indice, [MarshalAs(UnmanagedType.VBByRefStr)] ref string IndiceCNF);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCFCancelados([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCupomCancelado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCNFCancelados([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCupomNFCancelado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCLX([MarshalAs(UnmanagedType.VBByRefStr)] ref string cLeituraX);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCRO([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCRO);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCRZ([MarshalAs(UnmanagedType.VBByRefStr)] ref string cReducaoZ);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCRZRestante([MarshalAs(UnmanagedType.VBByRefStr)] ref string cReducaoRestante);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaTotalPagamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string CtotalFormasPago);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaTroco([MarshalAs(UnmanagedType.VBByRefStr)] ref string cTroco);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCNFNV([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCNaoFiscalNaoVinculado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaDescricaoCNFV([MarshalAs(UnmanagedType.VBByRefStr)] ref string cDescricaoNaoFiscalVinculado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaDescontoNF([MarshalAs(UnmanagedType.VBByRefStr)] ref string cDescNF);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaAcrescimoNF([MarshalAs(UnmanagedType.VBByRefStr)] ref string cAcrescimoNF);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCancelamentoNF([MarshalAs(UnmanagedType.VBByRefStr)] ref string cCancelamentoNF);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaCNFV([MarshalAs(UnmanagedType.VBByRefStr)] ref string CContadorNFVinculado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaTempoLigado([MarshalAs(UnmanagedType.VBByRefStr)] ref string cTimeLigado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaTempoImprimindo([MarshalAs(UnmanagedType.VBByRefStr)] ref string CTimeImprimindo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaRegistradoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string CRgistradoresNF);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaRegistradoresFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string CRgistradoresFiscais);
        
#endregion

        #region Configuracaoes do ECF
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Cfg(int VariavelDoEcf, string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgFechaAutomaticoCupom(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgRedZAutomatico(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgImpEstGavVendas(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgLeituraXAuto(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgCalcArredondamento(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgHorarioVerao(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgSensorAut(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgCupomAdicional(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgEspacamentoCupons(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgHoraMinReducaoZ(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgLimiarNearEnd(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgPermMensPromCNF(string VarConfigEcf);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_CfgLegProdutos(string VarConfigEcf);
        #endregion

        #region Outras
        //Outras					   
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AbrePortaSerial();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FechaPortaSerial();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AberturaDoDia(string Str_Valor,string Str_FormaPagamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_FechamentoDoDia();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ImprimeConfiguracoesImpressora();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RegistraNumeroSerie();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaNumeroSerie();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_RetornaSerialCriptografado(string SerialCriptografado, string NumeroSerial);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ConfiguraHorarioVerao(string DataEntrada,string DataSaida,string Controle );
        #endregion

        #region Programacao do ECF
        //Programacao do ECF
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AlteraSimboloMoeda(string cMoeda);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgramaAliquota(string cAliquota, int ICMS_ou_ISS);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgramaHorarioVerao();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_NomeiaTotalizadorNaoSujeitoIcms(int iIndice, string cTotalizador);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgramaArredondamento();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgramaTruncamento();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_LinhasEntreCupons(int Linhas);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EspacoEntreLinhas(int Dots);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ForcaImpactoAgulhas(int ForcaImpacto);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ResetaImpressora();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgramaFormasPagamento(string FormaPagamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgFormasPagtoSemVincular(string FormaPagamento);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgramaVinculados(string DescricaoVinculado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_EqualizaFormasPgto();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_ProgramaOperador(string Operador);
        #endregion
        
        #region Metodos de Autenticacao e Gaveta
        //Metodos de Autenticacao e Gaveta

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AutenticacaoStr(string TextoStr);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_Autenticacao();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaDocAutenticacao();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_AcionaGaveta();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaEstadoGaveta(ref int Estado);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI_VerificaEstadoGavetaStr([MarshalAs(UnmanagedType.VBByRefStr)] ref string EstadoGaveta);
        [DllImport("Daruma32.dll")]  
        public static extern int Daruma_FI_SaltarLinhas(int Linhas);   
        #endregion

        #region Metodos de TEF
        //Metodos de TEF
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TEF_EsperarArquivo(string cArquivo, string cTempo, string cTravar);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TEF_ImprimirResposta(string cArquivoResp, string cForma, string cTravar);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TEF_ImprimirRespostaCartao(string cArquivoResp, string cForma, string cTravar,string ValorPago);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TEF_SetFocus(string cWndFocus);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TEF_TravarTeclado(string cTravar);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TEF_FechaRelatorio();
        #endregion

        #region Métodos para o Síntegra
        //Metodo de Alto Nivel
         [DllImport("Daruma32.dll")]
         public static extern int Daruma_Sintegra_GerarRegistrosArq(string cDataInicio, string cDataFim, string cMunicipio, string cFax, string cCodIdConvenio, string cCodIdNatureza, string cCodIdfinalidade, string cLogradouro, string cNumero, string cComplemento, string cBairro, string cCep, string cNomeContato, string cTelefone);

        //Metodo de Médio Nível
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro10(string cDataInicio, string cDataFim, string cMunicipio, string cFax, string cCodIdConvenio, string cCodIdNatureza, string cCodIdfinalidade, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro11(string cLogradouro, string cNumero, string cComplemento, string cBairro, string cCEP, string cNomeContato, string cTelefone, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro60M(string cDataInicio, string cDataFim, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro60A(string cDataInicio, string cDataFim, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro60D(string cDataInicio, string cDataFim, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro60I(string cDataInicio, string cDataFim, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro60R(string cDataInicio, string cDataFim, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Sintegra_GerarRegistro90(string cRetorno);
    /* [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_SintegraSeparador(string cSeparador);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_SintegraPath(string cPath);*/

        //Metodo de Baixo Nivel
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_RetornarInfoDownloadMFD(string cTipoDownload, string cData_ou_COOInicio, string cData_ou_COOFim, string cIndice, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cRetorno);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_RetornarInfoDownloadMFDArquivo(string cTipoDownload, string cData_ou_COOInicio, string cData_ou_COOFim, string cIndice);

        //Metodos para Gerar o Ato Cotepe NF Paulista
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_RFD_GerarArquivo( string cDataInicial, string cDataFinal);



        #endregion

        #region Metodos para o PAF-ECF
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_RSA_CarregaChavePrivada_Arquivo(System.String Arquivo);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_RSA_RetornaChavePublica([MarshalAs(UnmanagedType.VBByRefStr)] ref string N, [MarshalAs(UnmanagedType.VBByRefStr)] ref string E);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_RSA_CriarAssinatura(string caminhoDoArquivo, [MarshalAs(UnmanagedType.VBByRefStr)] ref string sMD5, [MarshalAs(UnmanagedType.VBByRefStr)] ref string sAssinaturaEletronica);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_RSA_CodificaInformacao(System.String Texto, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Codigo);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_RSA_DecodificaInformacao(System.String Codigo, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Texto);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_GTCodificado([MarshalAs(UnmanagedType.VBByRefStr)] ref string GTCodificado);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_Verifica_GTCodificado(System.String GTCodificado);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_NumeroSerialCodificado([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerialCodificado);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_Verifica_NumeroSerialCodificado(System.String NumeroSerialCodificado);

        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FIMFD_CodigoModeloFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string CodigoModeloFiscal);

        #endregion

        #region Funções de Acesso a Impressora FS2000
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SelecionaBanco(string cBanco);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SelecionaCidade(string cCidade);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SelecionaData(string cData);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SelecionaFavorecido(string cFavor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SelecionaValorChequeH(string cValor);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SelecionaValorChequeV(string cChequeV);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SelecionaTextoVersoCheque(string cChequeH);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_LeituraCodigoMICR(ref string cMicr);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_LiberarCheque();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_CarregarCheque();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_CorrigirGeometriaCheque(string cGeo, string cGeo2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_LeituraTabelaCheque(ref string cTabchq);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_AbreRelatorioGerencial(string cRgerencial);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_CriaRelatorioGerencial(string cCgerencial);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_VerificaRelatorioGerencial([MarshalAs(UnmanagedType.VBByRefStr)]ref string cVgerencial);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_StatusCheque(ref string cScheque);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_ImprimirCheque(string cparm1, string cparm2, string cparm3, string cparm4, string cparm5, string cparm6);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_ImprimirVersoCheque(string cparm1);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_CancelarCheque();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_DescontoSobreItemVendido(string cparm1, string cparm2, string cparm3);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_FS2000_CupomAdicional(string cparm1);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_SegundaViaCNFVinculado();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_CancelamentoCNFV(string cparm1);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_AcrescimosICMSISS([MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm1, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_CancelamentosICMSISS([MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm1, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_DescontosICMSISS([MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm1, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_LeituraInformacaoUltimoDoc([MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm1, [MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm2);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_FI2000_LeituraInformacaoUltimosCNF([MarshalAs(UnmanagedType.VBByRefStr)] ref string cparm1);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_FS2000_TempoEsperaCheque(string TempoEspCheque);
        #endregion

        #region Função que analisa os retornos do ECF
        //Funcao que analiza o retorno da impressora
        //FrameWork Daruma32.dll
        public static void Daruma_Mostrar_Retorno()
        {
            string Str_Erro_Extendido = new string(' ', 4); ;

            int Int_ACK = 0;
            int Int_ST1 = 0;
            int Int_ST2 = 0;

            Daruma_FI_RetornoImpressora(ref Int_ACK, ref Int_ST1, ref Int_ST2);

            Daruma_FI_RetornaErroExtendido(ref Str_Erro_Extendido);

            MessageBox.Show("Retorno do Metodo = "
            + Int_Retorno.ToString() + "\r\n"
            + "ACK = " + Int_ACK.ToString() + "\r\n"
            + "ST1 = " + Int_ST1.ToString() + "\r\n"
            + "ST2 = " + Int_ST2.ToString() + "\r\n"
            + "Erro Extendido = " + Str_Erro_Extendido.ToString(), "Daruma Framework Retorno do Metodo");
        }
        #endregion

        #endregion

 #region Impressora DUAL

        #region Funções de Acesso a Impressora DUAL
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_DUAL_Enter(string cEnter);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_DUAL_Porta(string cPorta);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_DUAL_Espera(string cEspera);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_DUAL_ModoEscrita(string cEscrita);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_DUAL_Tabulacao(string cTabulacao);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_DUAL_Termica(string cTermica);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_Registry_DUAL_Velocidade(string cVelocidade);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_ImprimirTexto(string cTexto, int cTam);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_ImprimirArquivo(string cArquivo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_VerificaStatus();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_StatusGaveta();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_VerificaDocumento();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_Autenticar(string Local, string cTexto, string cSec);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_AcionaGaveta();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_EnviarBMP(string cPath_do_BMP);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_VerificarGuilhotina();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_DUAL_ConfigurarGuilhotina(int cFlag, int cLinhasAcionamento);

        #endregion

        #region Função que analisa os retornos da DUAL
        //Funcao que analiza o retorno da impressora DUAL (nao fiscal)
        //FrameWork Daruma32.dll
        public static void Retorno_DUAL()
        {
            if (Int_Retorno == 0)
                MessageBox.Show("0(zero) - Impressora Desligada!", "Daruma_Framework_CSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (Int_Retorno == 1)
                MessageBox.Show("1(um) - Impressora OK!", "Daruma_Framework_CSharp", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (Int_Retorno == -50)
                MessageBox.Show("-50 - Impressora OFF-LINE!", "Daruma_Framework_CSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (Int_Retorno == -51)
                MessageBox.Show("-51 - Impressora Sem Papel!", "Daruma_Framework_CSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (Int_Retorno == -27)
                MessageBox.Show("-27 - Erro Generico!", "Daruma_Framework_CSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        #endregion

 #endregion 
        
 #region Funções de Acesso ao Terminal Autonomo (TA1000)
        //Comunicando com o TA1000
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_LeStatusTransferencia();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_LeStatusRecebimento();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_EnviarBancoProdutos();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_ReceberBancoProdutos();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_ReceberProdutosVendidos();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_CadastrarProdutos(string cDescricao, string cCodigo, string cCasasDecimaisPreco, string cCasasDecimaisQuantidade, string cPreco, string cDA, string cDAValor, string cUnidade, string cAlicota, string cProx, string cAnterior, string cEstoque);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_EliminarProdutos(string cCodigo);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_ZerarProdutos();
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_ConsultarProdutos(string cCodigo_Consultar, string cDescricao, string cCodigo, string cCasasDecimaisPreco, string cCasasDecimaisQuantidade, string cPreco, string cDA, string cDAValor, string cUnidade, string cAliquota, string cProx, string cAnterior, string cEstoque);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_ConsultarProdutosVendidos(string cDescricao, string cCodigo, string cPreco, string cGrupo, string cAcrecimo, string cCasasDecimaisPreco, string cCasasDecimaisQuantidade, string cUnidade, string cAlicota, string Estoque, string cParam1, string cParam2, string cQtdadeVendidoHoje, string cTotalVendido, string cQtdadeVendidoTotal);
        [DllImport("Daruma32.dll")]
        public static extern int Daruma_TA1000_ZerarProdutosVendidos();
        #endregion
    
    }
}