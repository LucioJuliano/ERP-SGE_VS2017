using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bematech;
using DARUMA32_CSharp;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using Controle_Dados;
using Bematech.Fiscal.ECF;
using Bematech.Fiscal.ECF.CupomFiscal;
using Bematech.Fiscal.ECF.OperacoesNaoFiscais;
using Bematech.Fiscal.ECF.Informacoes;
using Bematech.Fiscal.ECF.Inicializacao;
using Bematech.Fiscal.GerenciamentoDados;
using Bematech.Fiscal.TEF;
using Bematech.MiniImpressoras;
using Bematech.Texto;
using System.ComponentModel;
using System.IO;
using DFW;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Controles
{
    public class ImpressoraFiscal
    {
        public Funcoes Controle = new Funcoes();
        public string PortaImpECF = "USB";
        public ModeloImpressora ImpFiscal;
        Bematech.Fiscal.ECF.ImpressoraFiscal BematechFiscal = null;//Bematech.Fiscal.ECF.ImpressoraFiscal.Construir();
          
        public void InicializarBematech()
        {
            BematechFiscal = Bematech.Fiscal.ECF.ImpressoraFiscal.Construir();
        }
        public void InicializarDaruma()
        {          
            Declaracoes.iRetorno = Declaracoes.eBuscarPortaVelocidade_ECF_Daruma();
            if (Declaracoes.iRetorno != 1)
            {
                MessageBox.Show("Impressora desligada!", "DarumaFramework");
            }
            Declaracoes.eDefinirProduto_Daruma("ECF");            
        }

        public void InicializarDarumaDR800()
        {
            Declaracoes.iRetorno = Declaracoes.eBuscarPortaVelocidade_DUAL_DarumaFramework();           
            Declaracoes.eDefinirProduto_Daruma("DUAL");
        }

        public void ImpDR800(DataTable TabEtq)
        {
            //Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework(TxtImp, 0);

            for (int I = 0; I <= TabEtq.Rows.Count - 1; I++)
            {
                for (int L = 0; L <= int.Parse(TabEtq.Rows[I]["Qtde"].ToString()) - 1; L++)
                {

                    Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<b>Ref:" + TabEtq.Rows[I]["Referencia"].ToString() + "  Unidade:" + TabEtq.Rows[I]["UND"].ToString() + "</b><l></l>", 0);
                    Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<ean13><w3><h90><txt>" + TabEtq.Rows[I]["CodBarra"].ToString().Trim().Substring(0, 12) + "</txt></h90></w3></ean13><sp>5</sp><e>R$" + string.Format("{0:N2}", decimal.Parse(TabEtq.Rows[I]["Preco"].ToString())) + "</e>", 0);
                    //Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<e><b>R$" + string.Format("{0:N2}", decimal.Parse(TabEtq.Rows[I]["Preco"].ToString()))+ "</b></e>\x0d\x0a", 2);
                    if (TabEtq.Rows[I]["Descricao"].ToString().Trim().Length <= 37)
                        Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<c>" + TabEtq.Rows[I]["Descricao"].ToString().Trim() + "</c><l></l>", 0);
                    else
                    {
                        int tamcmp = TabEtq.Rows[I]["Descricao"].ToString().Trim().Length - 37;
                        Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<c>" + TabEtq.Rows[I]["Descricao"].ToString().Trim().Substring(37, tamcmp) + " </c><l></l>", 0);
                        Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<c>" + TabEtq.Rows[I]["Descricao"].ToString().Trim().Substring(0, 36) + " </c><l></l>", 0);
                    }
                    Declaracoes.iRetorno = Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>2</sl>", 0);
                }
            }            
        }

        public enum ModeloImpressora
        {
            Nenhuma,
            ImpBematech,
            ImpDaruma,
            ImpDarumaVelha,
            MFE
        }
        public void LeituraX()
        {            
            if (ImpFiscal == ModeloImpressora.ImpBematech)
            {
                /*Inicia teste = BematechFiscal.Inicializacao;
                /*teste.ProgramarFormaPagamento("VISA CREDITO");
                teste.ProgramarFormaPaLgamento("VISA ELETRO");
                teste.ProgramarFormaPagamento("MASTERSHOP");
                teste.ProgramarFormaPagamento("MASTERCARD");
                teste.ProgramarFormaPagamento("CHEQUE-PRE");
                BematechFiscal.AbrirPorta("COM1");*/
                //teste.ProgramarAliquota(17, Aliquota.TipoAliquota.ICMS);
                //teste.ProgramarAliquota(12, Aliquota.TipoAliquota.ICMS);
                BematechFiscal.RelatoriosFiscais.ImprimirLeituraX();
                BematechFiscal.FecharPorta();
            }
            if (ImpFiscal == ModeloImpressora.ImpDaruma)
            {
                //DARUMA32.Int_Retorno = DARUMA32.Daruma_FI_LeituraX();                               
                Declaracoes.iRetorno = Declaracoes.iLeituraX_ECF_Daruma();
                Declaracoes.TrataRetorno(Declaracoes.iRetorno);
            }
            if (ImpFiscal == ModeloImpressora.ImpDarumaVelha)
            {
                DARUMA32.Int_Retorno = DARUMA32.Daruma_FI_LeituraX();                                               
            }
        }
        public void LeituraZ()
        {
            if (ImpFiscal == ModeloImpressora.ImpBematech)
            {                
                //BematechFiscal.AbrirPorta(PortaImpECF);
                BematechFiscal.RelatoriosFiscais.ImprimirReducaoZ();
                BematechFiscal.FecharPorta();
            }
            if (ImpFiscal == ModeloImpressora.ImpDaruma)
            {
                string message = "Tem Certeza que Dezeja Efetuar a Redução?";
                string caption = "Daruma Framework";
                DialogResult result;
                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    //DARUMA32.Int_Retorno = DARUMA32.Daruma_FI_ReducaoZ("", "");                    
                    //DARUMA32.Daruma_Mostrar_Retorno();
                    Declaracoes.iRetorno = Declaracoes.iReducaoZ_ECF_Daruma("", "");
                    Declaracoes.TrataRetorno(Declaracoes.iRetorno);
                }
            }
            if (ImpFiscal == ModeloImpressora.ImpDarumaVelha)
            {
                string message = "Tem Certeza que Dezeja Efetuar a Redução?";
                string caption = "Daruma Framework";
                DialogResult result;
                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    DARUMA32.Int_Retorno = DARUMA32.Daruma_FI_ReducaoZ("", "");                    
                    DARUMA32.Daruma_Mostrar_Retorno();                    
                }
            }
        }
        public void ImpCupomFiscal(int IdVenda)
        {
            try
            {
                // Busca Venda      
                SqlDataReader DadosCliente = Controle.ConsultaSQL("SELECT T2.CNPJCPF,T2.PESSOA,T2.ENDERECO,T2.NUMERO,T2.BAIRRO,T2.VLRDESCONTO,T3.REFERENCIA,T3.DESCRICAO,T3.SITTRIBUTARIA,T3.ICMSISS,T1.QTDE,T1.VLRUNITARIO FROM MVVENDAITENS T1 " +
                                                                  " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_VENDA=" + IdVenda.ToString());
                SqlDataReader Venda        = Controle.ConsultaSQL("SELECT T2.CNPJCPF,T2.PESSOA,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO,T2.BAIRRO,T2.VLRDESCONTO,T2.CREDITO,T3.REFERENCIA,T3.DESCRICAO,T3.SITTRIBUTARIA,T3.ICMSISS,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL AS TOTALITEM, T2.VLRTOTAL AS VLRVENDA,T2.VLRSUBTOTAL,T1.ID_PRODUTO,T3.NCM,T1.ID_VENDA FROM MVVENDAITENS T1 " +
                                                                  " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_VENDA=" + IdVenda.ToString());
                SqlDataReader Pag          = Controle.ConsultaSQL("SELECT T1.VLRORIGINAL,T2.ID_DOCUMENTO,T2.DOCUMENTO,T2.CODECF FROM LANCFINANCEIRO T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO)  WHERE T1.ID_VENDA=" + IdVenda.ToString());
                

                if (ImpFiscal == ModeloImpressora.MFE)
                {
                    if (GeraCFe_TDS(Venda, Pag))
                        MessageBox.Show("Cupom Eletronico Gerado com Sucesso !!!", "Cupom Fiscal Eletronico", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Erro ao Gerar o Cupom Eletronico !!!", "Cupom Fiscal Eletronico", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                //Inicializando Cupom                                                           
                DadosCliente.Read();
                if (ImpFiscal == ModeloImpressora.ImpBematech)
                {
                    try
                    {
                        if (DadosCliente.HasRows)
                        {
                            BematechFiscal.Cupom.Abrir(DadosCliente["CNPJCPF"].ToString().Trim(), Controle.Space(DadosCliente["PESSOA"].ToString().Trim(), 30), Controle.Space(DadosCliente["ENDERECO"].ToString().Trim() + " " + DadosCliente["BAIRRO"].ToString().Trim(), 80));
                        }
                        else
                            BematechFiscal.Cupom.Abrir();
                        //                        
                        decimal VlrDesconto = 0;
                        string  ST          = "1700";
                        decimal VlrVenda    = 0;
                        decimal TAliqNCM    = 0;
                        decimal TVlrItem    = 0;
                        while (Venda.Read())
                        {
                            ST = string.Format("{0:N2}", decimal.Parse(Venda["IcmsISS"].ToString())).Replace(".", "").Replace(",", "");
                            if (int.Parse(Venda["SitTributaria"].ToString()) == 1)
                                ST = "NN";                                
                            else if (int.Parse(Venda["SitTributaria"].ToString()) == 2)
                                ST = "II";
                            else if (int.Parse(Venda["SitTributaria"].ToString()) == 3)
                                ST = "FF";

                            VlrDesconto = decimal.Parse(Venda["VLRDESCONTO"].ToString()) + decimal.Parse(Venda["CREDITO"].ToString());
                            VlrVenda    = decimal.Parse(Venda["VLRVENDA"].ToString());

                            BematechFiscal.Cupom.Vender(Venda["REFERENCIA"].ToString().Trim(), Controle.Space(Venda["DESCRICAO"].ToString().Trim(), 19), ST, Bematech.Fiscal.ECF.TipoQuantidade.Fracionaria, decimal.Parse(Venda["QTDE"].ToString()),3,  decimal.Parse(Venda["VlrUnitario"].ToString()),TipoAcrescimoDesconto.Valor,0);

                            TVlrItem = decimal.Parse(Venda["VlrUnitario"].ToString()) * decimal.Parse(Venda["QTDE"].ToString());

                            TAliqNCM = TAliqNCM + ((TVlrItem * BuscaAliqNCM(Venda["NCM"].ToString())) / 100);
                            
                        }
                        BematechFiscal.Cupom.SubTotalizar();
                        //Verificando se existe desconto no cupom
                        if (VlrDesconto > 0)
                            BematechFiscal.Cupom.AcrescerDescontarSubTotal(Bematech.Fiscal.ECF.TipoOperacao.Desconto, Bematech.Fiscal.ECF.TipoAcrescimoDesconto.Valor, VlrDesconto);

                        BematechFiscal.Cupom.Totalizar();
                        while (Pag.Read())
                        {
                            BematechFiscal.Cupom.EfetuarPagamento(int.Parse(Pag["CodECF"].ToString()), Math.Round(decimal.Parse(Pag["VlrOriginal"].ToString()), 2));
                        }
                        int NumCupom = BematechFiscal.Cupom.Numero;
                        //BematechFiscal.Cupom.Fechar("Vlr Aprox dos Tributos R$ " + string.Format("{0}N2", TAliqNCM) + " (" + string.Format("{0}N2", (TAliqNCM / BematechFiscal.Cupom.SubTotal) * 100)); 
                        Mensagens msgs = new Mensagens();
                        msgs.Adicionar(new TextoFormatado("Val Aprox dos Tributos R$ " + string.Format("{0:N2}", TAliqNCM) + " (" + string.Format("{0:N2}", (TAliqNCM / BematechFiscal.Cupom.SubTotal) * 100)+"%) Fonte:IBPT", Bematech.Texto.TextoFormatado.TamanhoCaracter.Normal, TextoFormatado.FormatoCaracter.Negrito, TextoFormatado.TipoAlinhamento.Esquerda));                                               
                        BematechFiscal.Cupom.Fechar(msgs); 
                        
                        BematechFiscal.FecharPorta();
                        //                        
                        RegistrarCupom(IdVenda, NumCupom, VlrVenda, VlrDesconto);
                        //Controle.ExecutaSQL("Update MvVenda set FormNF='ECF-" + NumCupom.ToString() + "' Where Id_Venda=" + IdVenda.ToString());                        
                    }
                    catch
                    {
                        CancelarCupom();
                        BematechFiscal.FecharPorta();
                    }
                }
                if (ImpFiscal == ModeloImpressora.ImpDaruma)
                {
                   try
                   {                       
                       if (DadosCliente.HasRows)
                       {
                           //DARUMA32_CSharp.DARUMA32.Daruma_FI_AbreCupom(DadosCliente["CNPJCPF"].ToString().Trim());                                                        
                           Declaracoes.iRetorno = Declaracoes.iCFAbrir_ECF_Daruma(DadosCliente["CNPJCPF"].ToString().Trim(), Controle.Space(DadosCliente["PESSOA"].ToString().Trim(), 30), Controle.Space(DadosCliente["ENDERECO"].ToString().Trim() + " " + DadosCliente["BAIRRO"].ToString().Trim(), 80));                           
                       }
                       else
                       {
                           //DARUMA32_CSharp.DARUMA32.Daruma_FI_AbreCupom("");
                           Declaracoes.iRetorno = Declaracoes.iCFAbrirPadrao_ECF_Daruma();
                       }
                        
                        decimal VlrDesconto = 0;
                        string ST           = "Ta";
                        decimal VlrVenda    = 0;
                        decimal TAliqNCM    = 0;
                        decimal TVlrItem    = 0;                        
                        while (Venda.Read())
                        {
                            ST = "Ta";
                            if (int.Parse(Venda["SitTributaria"].ToString()) == 1)
                                ST = "NN";
                            else if (int.Parse(Venda["SitTributaria"].ToString()) == 2)
                                ST = "II";
                            else if (int.Parse(Venda["SitTributaria"].ToString()) == 3)
                                ST = "I1";
                            VlrDesconto = decimal.Parse(Venda["VLRDESCONTO"].ToString()) + decimal.Parse(Venda["CREDITO"].ToString());
                            VlrVenda    = decimal.Parse(Venda["VLRVENDA"].ToString());

                            Declaracoes.iRetorno = Declaracoes.iCFVender_ECF_Daruma(ST, Venda["QTDE"].ToString(), string.Format("{0:N2}", decimal.Parse(Venda["VlrUnitario"].ToString())), "D$", "0,00", Venda["REFERENCIA"].ToString().Trim(), "UN", Controle.Space(Venda["DESCRICAO"].ToString().Trim(), 19));                           
                            TVlrItem = decimal.Parse(Venda["VlrUnitario"].ToString()) * decimal.Parse(Venda["QTDE"].ToString());
                            TAliqNCM = TAliqNCM + ((TVlrItem * BuscaAliqNCM(Venda["NCM"].ToString())) / 100);
                        }
                        //Verificando se existe desconto no cupom
                        //DARUMA32_CSharp.DARUMA32.Daruma_FI_IniciaFechamentoCupom("D", "$", string.Format("{0:N2}", VlrDesconto));                                                
                        Declaracoes.iRetorno = Declaracoes.iCFTotalizarCupom_ECF_Daruma("D$", string.Format("{0:N2}", VlrDesconto));                        
                        while (Pag.Read())
                        {                            
                            //DARUMA32_CSharp.DARUMA32.Daruma_FI_EfetuaFormaPagamento(Pag["Documento"].ToString(), string.Format("{0:N2}",decimal.Parse(Pag["VlrOriginal"].ToString())));
                            Declaracoes.iRetorno = Declaracoes.iCFEfetuarPagamentoFormatado_ECF_Daruma(Pag["Documento"].ToString().Trim(), string.Format("{0:N2}", decimal.Parse(Pag["VlrOriginal"].ToString())));                            
                        }
                        //if (DadosCliente.HasRows)
                        //  DARUMA32_CSharp.DARUMA32.Daruma_FI_IdentificaConsumidor(Controle.Space(DadosCliente["PESSOA"].ToString().Trim(), 30), Controle.Space(DadosCliente["ENDERECO"].ToString().Trim() + " " + DadosCliente["BAIRRO"].ToString().Trim(), 80), DadosCliente["CNPJCPF"].ToString().Trim());

                        //string Str_Informacao = new string(' ', 6);
                        //DARUMA32.Daruma_FI_NumeroCupom(ref Str_Informacao);                        
                        
                        int NumCupom = int.Parse(InformacaoCF_Daruma("51"));

                        //int NumCupom = 0; // int.Parse(Str_Informacao);                       

                        string Msg = "Val Aprox dos Tributos R$ " + string.Format("{0:N2}", TAliqNCM) + " (" + string.Format("{0:N2}", (TAliqNCM / VlrVenda) * 100) + "%) Fonte:IBPT";
                        Declaracoes.iRetorno = Declaracoes.iCFEncerrarConfigMsg_ECF_Daruma(Msg + " Obrigado, Volte Sempre !!!");                        

                        RegistrarCupom(IdVenda, NumCupom, VlrVenda, VlrDesconto);
                        Controle.ExecutaSQL("Update MvVenda set FormNF='ECF-" + NumCupom.ToString() + "' Where Id_Venda=" + IdVenda.ToString());                       
                    }
                    catch
                    {

                        CancelarCupom();
                        //DARUMA32.Daruma_FI_CancelaCupom();
                    }
                }
                //Impressora Velha
                if (ImpFiscal == ModeloImpressora.ImpDarumaVelha)
                {
                    try
                    {
                        if (DadosCliente.HasRows)
                        {
                            DARUMA32_CSharp.DARUMA32.Daruma_FI_AbreCupom(DadosCliente["CNPJCPF"].ToString().Trim());                                                                                    
                        }
                        else
                        {
                            DARUMA32_CSharp.DARUMA32.Daruma_FI_AbreCupom("");                            
                        }

                        decimal VlrDesconto = 0;
                        string ST = "Ta";
                        decimal VlrVenda = 0;
                        decimal TAliqNCM = 0;
                        decimal TVlrItem = 0;
                        while (Venda.Read())
                        {
                            ST = "Ta";
                            if (int.Parse(Venda["SitTributaria"].ToString()) == 1)
                                ST = "NN";
                            else if (int.Parse(Venda["SitTributaria"].ToString()) == 2)
                                ST = "II";
                            else if (int.Parse(Venda["SitTributaria"].ToString()) == 3)
                                ST = "I1";
                            VlrDesconto = decimal.Parse(Venda["VLRDESCONTO"].ToString()) + decimal.Parse(Venda["CREDITO"].ToString());
                            VlrVenda = decimal.Parse(Venda["VLRVENDA"].ToString());

                            DARUMA32_CSharp.DARUMA32.Daruma_FI_VendeItem(Venda["REFERENCIA"].ToString().Trim(), Controle.Space(Venda["DESCRICAO"].ToString().Trim(), 19), ST, "F", Venda["QTDE"].ToString(), 2, string.Format("{0:N2}",decimal.Parse(Venda["VlrUnitario"].ToString())), "$","0,00");
                            TVlrItem = decimal.Parse(Venda["VlrUnitario"].ToString()) * decimal.Parse(Venda["QTDE"].ToString());
                            TAliqNCM = TAliqNCM + ((TVlrItem * BuscaAliqNCM(Venda["NCM"].ToString())) / 100);
                        }
                        //Verificando se existe desconto no cupom
                        DARUMA32_CSharp.DARUMA32.Daruma_FI_IniciaFechamentoCupom("D", "$", string.Format("{0:N2}", VlrDesconto));                                                
                        while (Pag.Read())
                        {
                            DARUMA32_CSharp.DARUMA32.Daruma_FI_EfetuaFormaPagamento(Pag["Documento"].ToString(), string.Format("{0:N2}",decimal.Parse(Pag["VlrOriginal"].ToString())));                        
                        }
                        if (DadosCliente.HasRows)
                            DARUMA32_CSharp.DARUMA32.Daruma_FI_IdentificaConsumidor(Controle.Space(DadosCliente["PESSOA"].ToString().Trim(), 30), Controle.Space(DadosCliente["ENDERECO"].ToString().Trim() + " " + DadosCliente["BAIRRO"].ToString().Trim(), 80), DadosCliente["CNPJCPF"].ToString().Trim());

                        string Str_Informacao = new string(' ', 6);
                        DARUMA32.Daruma_FI_NumeroCupom(ref Str_Informacao);                        
                        
                        int NumCupom = 0; // int.Parse(Str_Informacao);


                        string Msg = "Val Aprox dos Tributos R$ " + string.Format("{0:N2}", TAliqNCM) + " (" + string.Format("{0:N2}", (TAliqNCM / VlrVenda) * 100) + "%) Fonte:IBPT";
                        DARUMA32_CSharp.DARUMA32.Daruma_FI_TerminaFechamentoCupom(Msg + " Obrigado, Volte Sempre !!!");                        

                        RegistrarCupom(IdVenda, NumCupom, VlrVenda, VlrDesconto);
                        Controle.ExecutaSQL("Update MvVenda set FormNF='ECF-" + NumCupom.ToString() + "' Where Id_Venda=" + IdVenda.ToString());
                    }
                    catch
                    {
                        CancelarCupom();
                        //DARUMA32.Daruma_FI_CancelaCupom();
                    }
                }
            }
            catch (Exception a)
            {             
            }
        }

        private string InformacaoCF_Daruma(string Str_Indice)
        {            
            string Str_Tamanho;
                        
            Str_Tamanho = "1000";

            int iTamanho;
            int.TryParse(Str_Tamanho, out iTamanho);

            StringBuilder Str_Informacao = new StringBuilder(iTamanho);
            Str_Informacao.Length = iTamanho;

            Declaracoes.iRetorno = Declaracoes.rRetornarInformacao_ECF_Daruma(Str_Indice, Str_Informacao);            
            return Str_Informacao.ToString().Trim();
        }

        private decimal BuscaAliqNCM(string ncm)
        {
            SqlDataReader TabNCM = Controle.ConsultaSQL("SELECT * FROM NCM WHERE NCM='"+ncm+"'");


            if (TabNCM.HasRows)
            {
                TabNCM.Read();
                return decimal.Parse(TabNCM["AliqNac"].ToString());
            }
            else
                return decimal.Parse("32,09");
            
        }

        public void CancelarCupom()
        {
            //BematechFiscal.AbrirPorta(PortaImpECF);
            if (ImpFiscal == ModeloImpressora.ImpBematech)
            {
                BematechFiscal.Cupom.Cancelar();
                int NumCF = BematechFiscal.Cupom.Numero;
                BematechFiscal.FecharPorta();
                //
                Controle.ExecutaSQL("UPDATE CUPOMFISCAL SET STATUS=2 WHERE Num_CF=" + NumCF.ToString().Trim());
            }
            if (ImpFiscal == ModeloImpressora.ImpDaruma)
            {
                //DARUMA32.Daruma_FI_CancelaCupom();
                int NumCupom = int.Parse(InformacaoCF_Daruma("51"));             
                Declaracoes.iRetorno = Declaracoes.iCFCancelar_ECF_Daruma();
                
                /*string Str_Informacao = new string(' ', 6);
                DARUMA32.Daruma_FI_NumeroCupom(ref Str_Informacao);
                int NumCF = int.Parse(Str_Informacao);*/
                Controle.ExecutaSQL("UPDATE CUPOMFISCAL SET STATUS=2 WHERE Num_CF=" + NumCupom.ToString().Trim());
            }
            if (ImpFiscal == ModeloImpressora.ImpDarumaVelha)
            {
                DARUMA32.Daruma_FI_CancelaCupom();                
                string Str_Informacao = new string(' ', 6);
                DARUMA32.Daruma_FI_NumeroCupom(ref Str_Informacao);
                int NumCF = int.Parse(Str_Informacao);
                Controle.ExecutaSQL("UPDATE CUPOMFISCAL SET STATUS=2 WHERE Num_CF=" + NumCF.ToString().Trim());
            }
        }
        public void RegistrarFormaPagamento(string FormaPgto)
        {
            if (ImpFiscal == ModeloImpressora.ImpBematech)
                BematechFiscal.Inicializacao.ProgramarFormaPagamento(FormaPgto);
        }

        public void RegistrarCupom(int IdVenda, int NumCF, decimal Vlr, decimal VlrDesc)
        {
            Controle_Dados.CupomFiscal RegCF = new Controle_Dados.CupomFiscal();
            RegCF.Controle = Controle;
            //
            RegCF.LerDados(0);
            RegCF.IdVenda     = IdVenda;
            RegCF.NumCF       = NumCF;
            RegCF.VlrSubTotal = Vlr + VlrDesc;
            RegCF.VlrDesconto = VlrDesc;
            RegCF.VlrTotal    = Vlr;
            RegCF.Status      = 1;
            RegCF.GravarDados();
            Controle.ExecutaSQL("Update MvVenda set Id_LancCF=" + RegCF.IdLanc.ToString() + " Where Id_Venda=" + IdVenda.ToString());
            //
            //Registrando os Itens
            Controle_Dados.CupomFiscalItens ItensCF = new CupomFiscalItens();
            ItensCF.Controle = Controle;
            //
            SqlDataReader Itens = Controle.ConsultaSQL("SELECT T1.*,T2.SITTRIBUTARIA,T2.ICMSISS FROM MVVENDAITENS T1 " +
                                                       " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_VENDA=" + IdVenda.ToString());
            while (Itens.Read())
            {
                ItensCF.LerDados(0);
                ItensCF.IdLanc        = RegCF.IdLanc;
                ItensCF.IdProduto     = int.Parse(Itens["Id_Produto"].ToString());
                ItensCF.Qtde          = decimal.Parse(Itens["Qtde"].ToString());
                ItensCF.VlrUnitario   = decimal.Parse(Itens["VlrUnitario"].ToString());
                ItensCF.SitTributaria = int.Parse(Itens["SitTributaria"].ToString());
                ItensCF.PIcms         = decimal.Parse(Itens["ICMSISS"].ToString());
                ItensCF.VlrTotal      = ItensCF.Qtde * ItensCF.VlrUnitario;

                if (decimal.Parse(Itens["P_Desconto"].ToString()) > 0)
                    ItensCF.VlrTotal = decimal.Parse(Itens["VlrTotal"].ToString()) - (decimal.Parse(Itens["VlrTotal"].ToString()) * decimal.Parse(Itens["P_Desconto"].ToString()) / 100);

                if (ItensCF.SitTributaria == 0)
                    ItensCF.VlrBIcms = ItensCF.VlrTotal;
                else if (ItensCF.SitTributaria == 1)
                    ItensCF.VlrNaotributado = ItensCF.VlrTotal;
                else if (ItensCF.SitTributaria == 2)
                    ItensCF.VlrIsento = ItensCF.VlrTotal;
                else if (ItensCF.SitTributaria == 3)
                    ItensCF.VlrSubstituicao = ItensCF.VlrTotal;

                if (ItensCF.PIcms > 0)
                    ItensCF.VlrIcms = Math.Round((ItensCF.VlrBIcms * ItensCF.PIcms) / 100, 2);
                ItensCF.GravarDados();
            }
            Itens.Dispose();            
        }
        public void RegistrarCupom(DateTime Dt, int IdVenda, int NumCF, decimal Vlr, decimal VlrDesc)
        {
            Controle_Dados.CupomFiscal RegCF = new Controle_Dados.CupomFiscal();
            RegCF.Controle = Controle;
            //
            RegCF.LerDados(0);
            RegCF.IdVenda     = IdVenda;
            RegCF.Data        = Dt;
            RegCF.NumCF       = NumCF;
            RegCF.VlrSubTotal = Vlr + VlrDesc;
            RegCF.VlrDesconto = VlrDesc;
            RegCF.VlrTotal    = Vlr;
            RegCF.Status      = 1;
            RegCF.GravarDados();
            Controle.ExecutaSQL("Update MvVenda set Id_LancCF=" + RegCF.IdLanc.ToString() + " Where Id_Venda=" + IdVenda.ToString());
            //
            //Registrando os Itens
            Controle_Dados.CupomFiscalItens ItensCF = new CupomFiscalItens();
            ItensCF.Controle = Controle;
            //
            SqlDataReader Itens = Controle.ConsultaSQL("SELECT T1.*,T2.SITTRIBUTARIA,T2.ICMSISS FROM MVVENDAITENS T1 " +
                                                       " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_VENDA=" + IdVenda.ToString());
            while (Itens.Read())
            {
                ItensCF.LerDados(0);
                ItensCF.IdLanc        = RegCF.IdLanc;
                ItensCF.IdProduto     = int.Parse(Itens["Id_Produto"].ToString());
                ItensCF.Qtde          = decimal.Parse(Itens["Qtde"].ToString());
                ItensCF.VlrUnitario   = decimal.Parse(Itens["VlrUnitario"].ToString());                
                ItensCF.SitTributaria = int.Parse(Itens["SitTributaria"].ToString());
                ItensCF.PIcms         = decimal.Parse(Itens["ICMSISS"].ToString());
                ItensCF.VlrTotal      = ItensCF.Qtde * ItensCF.VlrUnitario;

                if (decimal.Parse(Itens["P_Desconto"].ToString()) > 0)
                    ItensCF.VlrTotal = decimal.Parse(Itens["VlrTotal"].ToString()) - (decimal.Parse(Itens["VlrTotal"].ToString()) * decimal.Parse(Itens["P_Desconto"].ToString()) / 100);
                
                if (ItensCF.SitTributaria == 0)
                    ItensCF.VlrBIcms = ItensCF.VlrTotal;
                else if (ItensCF.SitTributaria == 1)
                    ItensCF.VlrNaotributado = ItensCF.VlrTotal;
                else if (ItensCF.SitTributaria == 2)
                    ItensCF.VlrIsento = ItensCF.VlrTotal;
                else if (ItensCF.SitTributaria == 3)
                    ItensCF.VlrSubstituicao = ItensCF.VlrTotal;
                
                if (ItensCF.PIcms > 0)
                    ItensCF.VlrIcms = Math.Round((ItensCF.VlrBIcms * ItensCF.PIcms) / 100,2);

                ItensCF.GravarDados();
            }
            Itens.Dispose();
        }
        //Impressora não Fiscal
        public void ImpMiniBemacth(string texto,string Porta, int Modelo)
        {
            if (Modelo == 1)
                BemacthMP4000(texto, Porta);
            if (Modelo == 2)
                BemacthMP4200(texto, Porta);
            
        }

        public void BemacthMP4000(string texto, string Porta)
        {
            ImpressoraNaoFiscal Impressora = new ImpressoraNaoFiscal(Bematech.ModeloImpressoraNaoFiscal.MP4000TH, Porta);
            TextoFormatado Printtexto = new TextoFormatado(texto + "\r\n", TextoFormatado.TamanhoCaracter.Condensado, TextoFormatado.FormatoCaracter.Negrito);//  "\r\n"
            // Printtexto.TabelaCaracteres = Impressora.TabelaCaracteres;
            try
            {
                Impressora.Imprimir(Printtexto);
                Impressora.CortarPapel(true);
            }
            catch (MiniImpressoraException erro)
            {
                MessageBox.Show(erro.Message, "TestMiniPrinter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        public void BemacthMP4200(string texto, string Porta)
        {
                        
            try
            {
                MP2032.ConfiguraModeloImpressora(7);                
                MP2032.IniciaPorta(Porta);
                
                MP2032.FormataTX(texto + "\r\n", 1, 0, 0, 0, 0);
                MP2032.AcionaGuilhotina(1);
                //Impressora.Imprimir(Printtexto);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro ao Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------Gera Cupom Fiscal TDS, Função temporaria ------------------
        private bool GeraCFe_TDS(SqlDataReader Venda,SqlDataReader Pag)
        {
            SqlConnection BANCOCFe;
            try
            {
                string conexaoCFe = "";
                conexaoCFe = "Data Source=SERVIDOR; Initial Catalog=NFce; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                BANCOCFe = new SqlConnection(conexaoCFe);
                BANCOCFe.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao BANCO TDS, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            IntegracaoTDS CabTDS = new IntegracaoTDS();
            IntegracaoItensTDS ItensTDS = new IntegracaoItensTDS();
            CabTDS.ConexaoCFe   = BANCOCFe;
            ItensTDS.ConexaoCFe = BANCOCFe;
            CabTDS.LimpaDados();

            while (Venda.Read())
            {
                if (CabTDS.Lancamento == 0)
                {
                    CabTDS.Pedido      = Venda["ID_VENDA"].ToString().Trim();                    
                    CabTDS.RazaoSocial = Controle.Space(Venda["PESSOA"].ToString().Trim(), 60);
                    CabTDS.Endereco    = Controle.Space(Venda["ENDERECO"].ToString().Trim(), 50);
                    CabTDS.Numero      = Controle.Space(Venda["NUMERO"].ToString().Trim(), 5);
                    CabTDS.Complemento = Controle.Space(Venda["COMPLEMENTO"].ToString().Trim(), 30);
                    CabTDS.Bairro      = Controle.Space(Venda["BAIRRO"].ToString().Trim(), 30);
                    CabTDS.Cidade      = "Fortaleza";
                    CabTDS.Estado      = "CE";
                    if (Venda["CNPJCPF"].ToString().Trim().Length > 11)
                        CabTDS.Cnpj = Venda["CNPJCPF"].ToString().Trim();
                    else
                        CabTDS.Cpf = Venda["CNPJCPF"].ToString().Trim();
                    CabTDS.ValorNota     = decimal.Parse(Venda["VLRVENDA"].ToString());
                    CabTDS.ValorIcms     = 0;
                    CabTDS.BaseCalculo   = 0;
                    CabTDS.ValorProduto  = decimal.Parse(Venda["VLRSUBTOTAL"].ToString()); ;
                    //CabTDS.ValorPIS      = Math.Round((CabTDS.ValorNota * decimal.Parse("1,65") / 100), 2); ;
                    //CabTDS.ValorCOFINS   = Math.Round((CabTDS.ValorNota * decimal.Parse("7,60") / 100), 2); ; ;
                    CabTDS.ValorDesconto = decimal.Parse(Venda["VLRDESCONTO"].ToString()) + decimal.Parse(Venda["CREDITO"].ToString());
                    CabTDS.GravarCFe();
                }
                ItensTDS.LimpaDados();
                ItensTDS.Pedido       = CabTDS.Pedido;
                ItensTDS.Produto      = int.Parse(Venda["ID_PRODUTO"].ToString());
                ItensTDS.Quantidade   = decimal.Parse(Venda["QTDE"].ToString());
                ItensTDS.Preco        = decimal.Parse(Venda["VLRUNITARIO"].ToString());
                ItensTDS.Total        = decimal.Parse(Venda["TOTALITEM"].ToString());
                ItensTDS.BaseCalculo  = 0;
                ItensTDS.ValorIcms    = 0;
                ItensTDS.AliqIcms     = 0;
                //ItensTDS.BasePis      = decimal.Parse(Venda["TOTALITEM"].ToString());
                //ItensTDS.ValorPis     = Math.Round((ItensTDS.BasePis * decimal.Parse("1,65")/ 100) , 2);
                //ItensTDS.AliqPis      = decimal.Parse("1,65");
                //ItensTDS.BaseCofins   = decimal.Parse(Venda["TOTALITEM"].ToString());
                //ItensTDS.ValorCofins  = Math.Round((ItensTDS.BasePis * decimal.Parse("7,60") / 100), 2);
                //ItensTDS.AliqCofins   = decimal.Parse("7,60"); ;
                ItensTDS.Desconto     = 0;
                ItensTDS.Cfop         = "5403";
                ItensTDS.ValorImposto = 0;
                ItensTDS.GravarItensCFe();
            }
            return true;
        }

        public class IntegracaoTDS 
        {
            private int _Lancamento;
            public int Lancamento { get { return _Lancamento; } set { _Lancamento = value; } }            
            private string _Pedido;
            public string Pedido { get { return _Pedido; } set { _Pedido = value; } }
            private DateTime _DataNota;
            public DateTime DataNota { get { return _DataNota; } set { _DataNota = value; } }            
            private string _Cfop;
            public string Cfop { get { return _Cfop; } set { _Cfop = value; } }
            private int _Cliente;
            public int Cliente { get { return _Cliente; } set { _Cliente = value; } }                        
            private string _RazaoSocial;
            public string RazaoSocial { get { return _RazaoSocial; } set { _RazaoSocial = value; } }
            private string _Endereco;
            public string Endereco { get { return _Endereco; } set { _Endereco = value; } }
            private string _Numero;
            public string Numero { get { return _Numero; } set { _Numero = value; } }
            private string _Complemento;
            public string Complemento { get { return _Complemento; } set { _Complemento = value; } }
            private string _Bairro;
            public string Bairro { get { return _Bairro; } set { _Bairro = value; } }
            private string _Cidade;
            public string Cidade { get { return _Cidade; } set { _Cidade = value; } }
            private string _Estado;
            public string Estado { get { return _Estado; } set { _Estado = value; } }
            private string _Cnpj;
            public string Cnpj { get { return _Cnpj; } set { _Cnpj = value; } }
            private string _Cpf;
            public string Cpf { get { return _Cpf; } set { _Cpf = value; } }
            private decimal _ValorNota;
            public decimal ValorNota { get { return _ValorNota; } set { _ValorNota = value; } }
            private decimal _ValorIcms;
            public decimal ValorIcms { get { return _ValorIcms; } set { _ValorIcms = value; } }
            private decimal _BaseCalculo;
            public decimal BaseCalculo { get { return _BaseCalculo; } set { _BaseCalculo = value; } }
            private decimal _ValorProduto;
            public decimal ValorProduto { get { return _ValorProduto; } set { _ValorProduto = value; } }
            private decimal _ValorPIS;
            public decimal ValorPIS { get { return _ValorPIS; } set { _ValorPIS = value; } }
            private decimal _ValorCOFINS;
            public decimal ValorCOFINS { get { return _ValorCOFINS; } set { _ValorCOFINS = value; } }
            private decimal _ValorDesconto;
            public decimal ValorDesconto { get { return _ValorDesconto; } set { _ValorDesconto = value; } }

            Funcoes Controle = new Funcoes();
            public SqlConnection ConexaoCFe;

            public void LimpaDados()
            {
                Lancamento    = 0;
                Pedido        = "";
                DataNota      = DateTime.Now.Date;
                Cfop          = "5403";
                Cliente       = 1;
                RazaoSocial   = "";
                Endereco      = "";
                Numero        = "";
                Complemento   = "";
                Bairro        = "";
                Cidade        = "Fortaleza";
                Estado        = "CE";
                Cnpj          = "";
                Cpf           = "";
                ValorNota     = 0;
                ValorIcms     = 0;
                BaseCalculo   = 0;
                ValorProduto  = 0;
                ValorPIS      = 0;
                ValorCOFINS   = 0;
                ValorDesconto = 0;
            }

            public void GravarCFe()
            {
                string sSQL = "INSERT INTO CINTEGRACAO (Pedido,DataNota,Cfop,Cliente,RazaoSocial,Endereco,Numero,Complemento,Bairro,Cidade,Estado,Cnpj,Cpf,ValorNota,ValorIcms,BaseCalculo,ValorProduto,ValorPIS,ValorCOFINS,ValorDesconto)" +
                           " output INSERTED.Lancamento VALUES (@Pedido,CONVERT(Datetime,@DataNota,103),@Cfop,@Cliente,@RazaoSocial,@Endereco,@Numero,@Complemento,@Bairro,@Cidade,@Estado,@Cnpj,@Cpf,@ValorNota,@ValorIcms,@BaseCalculo,@ValorProduto,@ValorPIS,@ValorCOFINS,@ValorDesconto)";

                SqlCommand CmdSql = new SqlCommand(sSQL, ConexaoCFe);
                CmdSql.Parameters.AddWithValue("@Pedido",        Pedido);
                CmdSql.Parameters.AddWithValue("@DataNota",      DataNota.ToShortDateString());
                CmdSql.Parameters.AddWithValue("@Cfop",          Cfop);
                CmdSql.Parameters.AddWithValue("@Cliente",       Cliente);
                CmdSql.Parameters.AddWithValue("@RazaoSocial",   RazaoSocial);
                CmdSql.Parameters.AddWithValue("@Endereco",      Endereco);
                CmdSql.Parameters.AddWithValue("@Numero",        Numero);
                CmdSql.Parameters.AddWithValue("@Complemento",   Complemento);
                CmdSql.Parameters.AddWithValue("@Bairro",        Bairro);
                CmdSql.Parameters.AddWithValue("@Cidade",        Cidade);
                CmdSql.Parameters.AddWithValue("@Estado",        Estado);
                CmdSql.Parameters.AddWithValue("@Cnpj",          Cnpj);
                CmdSql.Parameters.AddWithValue("@Cpf",           Cpf);
                CmdSql.Parameters.AddWithValue("@ValorNota",     Controle.FloatToStr(ValorNota, 2));
                CmdSql.Parameters.AddWithValue("@ValorICms",     Controle.FloatToStr(ValorIcms, 2));
                CmdSql.Parameters.AddWithValue("@BaseCalculo",   Controle.FloatToStr(BaseCalculo, 2));
                CmdSql.Parameters.AddWithValue("@ValorProduto",  Controle.FloatToStr(ValorProduto, 2));
                CmdSql.Parameters.AddWithValue("@ValorPis",      Controle.FloatToStr(ValorPIS, 2));
                CmdSql.Parameters.AddWithValue("@ValorCofins",   Controle.FloatToStr(ValorCOFINS, 2));
                CmdSql.Parameters.AddWithValue("@ValorDesconto", Controle.FloatToStr(ValorDesconto, 2));                
                Lancamento = (int)CmdSql.ExecuteScalar();
            }            
        }

        public class IntegracaoItensTDS
        {
            private int _Lanc;
            public int Lanc { get { return _Lanc; } set { _Lanc = value; } }
            private string _Pedido;
            public string Pedido { get { return _Pedido; } set { _Pedido = value; } }
            private DateTime _Data;
            public DateTime Data { get { return _Data; } set { _Data = value; } }
            private int _Produto;
            public int Produto { get { return _Produto; } set { _Produto = value; } }
            private decimal _Quantidade;
            public decimal Quantidade { get { return _Quantidade; } set { _Quantidade = value; } }
            private decimal _Preco;
            public decimal Preco { get { return _Preco; } set { _Preco = value; } }
            private decimal _Total;
            public decimal Total { get { return _Total; } set { _Total = value; } }
            private decimal _BaseCalculo;
            public decimal BaseCalculo { get { return _BaseCalculo; } set { _BaseCalculo = value; } }
            private decimal _ValorIcms;
            public decimal ValorIcms { get { return _ValorIcms; } set { _ValorIcms = value; } }
            private decimal _AliqIcms;
            public decimal AliqIcms { get { return _AliqIcms; } set { _AliqIcms = value; } }
            private decimal _BasePis;
            public decimal BasePis { get { return _BasePis; } set { _BasePis = value; } }
            private decimal _ValorPis;
            public decimal ValorPis { get { return _ValorPis; } set { _ValorPis = value; } }
            private decimal _AliqPis;
            public decimal AliqPis { get { return _AliqPis; } set { _AliqPis = value; } }
            private decimal _BaseCofins;
            public decimal BaseCofins { get { return _BaseCofins; } set { _BaseCofins = value; } }
            private decimal _ValorCofins;
            public decimal ValorCofins { get { return _ValorCofins; } set { _ValorCofins = value; } }
            private decimal _AliqCofins;
            public decimal AliqCofins { get { return _AliqCofins; } set { _AliqCofins = value; } }
            private decimal _Desconto;
            public decimal Desconto { get { return _Desconto; } set { _Desconto = value; } }
            private string _Cfop;
            public string Cfop { get { return _Cfop; } set { _Cfop = value; } }
            private decimal _ValorImposto;
            public decimal ValorImposto { get { return _ValorImposto; } set { _ValorImposto = value; } }

            Funcoes Controle = new Funcoes();
            public SqlConnection ConexaoCFe;

            public void LimpaDados()
            {
                Lanc         = 0;
                Pedido       = "";
                Data         = DateTime.Now.Date;
                Produto      = 0;
                Quantidade   = 0;
                Preco        = 0;
                Total        = 0;
                BaseCalculo  = 0;
                ValorIcms    = 0;
                AliqIcms     = 0;
                BasePis      = 0;
                ValorPis     = 0;
                AliqPis      = 0;
                BaseCofins   = 0;
                ValorCofins  = 0;
                AliqCofins   = 0;
                Desconto     = 0;
                Cfop         = "5403";
                ValorImposto = 0;
            }

            public void GravarItensCFe()
            {
                string sSQL = "INSERT INTO IINTEGRACAO (Pedido,Data,Produto,Quantidade,Preco,Total,BaseCalculo,ValorIcms,AliqIcms,BasePis,ValorPis,AliqPis,BaseCofins,ValorCofins,AliqCofins,Desconto,Cfop,ValorImposto)" +
                           "  VALUES (@Pedido,CONVERT(Datetime,@DataNota,103),@Produto,@Quantidade,@Preco,@Total,@BaseCalculo,@ValorIcms,@AliqIcms,@BasePis,@ValorPis,@AliqPis,@BaseCofins,@ValorCofins,@AliqCofins,@Desconto,@Cfop,@ValorImposto)";

                SqlCommand CmdSql = new SqlCommand(sSQL, ConexaoCFe);
                CmdSql.Parameters.Add("@Pedido",       Pedido);
                CmdSql.Parameters.Add("@DataNota",     Data.ToShortDateString());                
                CmdSql.Parameters.Add("@Produto",      Produto);
                CmdSql.Parameters.Add("@Quantidade",   Quantidade);
                CmdSql.Parameters.Add("@Preco",        Controle.FloatToStr(Preco, 2));
                CmdSql.Parameters.Add("@Total",        Controle.FloatToStr(Total, 2));
                CmdSql.Parameters.Add("@BaseCalculo",  Controle.FloatToStr(BaseCalculo, 2));
                CmdSql.Parameters.Add("@ValorIcms",    Controle.FloatToStr(ValorIcms, 2));
                CmdSql.Parameters.Add("@AliqIcms",     Controle.FloatToStr(AliqIcms, 2));
                CmdSql.Parameters.Add("@BasePis",      Controle.FloatToStr(BasePis, 2));
                CmdSql.Parameters.Add("@ValorPis",     Controle.FloatToStr(ValorPis, 2));
                CmdSql.Parameters.Add("@AliqPis",      Controle.FloatToStr(AliqPis, 2));
                CmdSql.Parameters.Add("@BaseCofins",   Controle.FloatToStr(BaseCofins, 2));
                CmdSql.Parameters.Add("@ValorCofins",  Controle.FloatToStr(ValorCofins, 2));
                CmdSql.Parameters.Add("@AliqCofins",   Controle.FloatToStr(AliqCofins, 2));
                CmdSql.Parameters.Add("@Desconto",     Controle.FloatToStr(Desconto, 2));
                CmdSql.Parameters.Add("@Cfop",         Cfop);
                CmdSql.Parameters.Add("@ValorImposto", Controle.FloatToStr(ValorImposto, 2));
                CmdSql.ExecuteNonQuery();
            }
        }

 //-------------------------------- Modulo MFE -----------------------------------------------
        public void MFE_VerStatus()
        {
            int num = GetNumeroSessao();

            string xml = "<?xml version=\"1.0\"?>\r\n" +
                                "<Integrador>\r\n" +
                                  "<Identificador>\r\n" +
                                    "<Valor>" + num + "</Valor>\r\n" +
                                  "</Identificador>\r\n" +
                                  "<Componente Nome=\"MF-e\">\r\n" +
                                    "<Metodo Nome=\"ConsultarStatusOperacional\">\r\n" +
                                      "<Parametros>\r\n" +
                                        "<Parametro>\r\n" +
                                          "<Nome>numeroSessao</Nome>\r\n" +
                                          "<Valor>" + num + "</Valor>\r\n" +
                                        "</Parametro>\r\n" +
                                        "<Parametro>\r\n" +
                                          "<Nome>codigoDeAtivacao</Nome>\r\n" +
                                          "<Valor>123456789</Valor>\r\n" +
                                        "</Parametro>\r\n" +
                                      "</Parametros>\r\n" +
                                    "</Metodo>\r\n" +
                                  "</Componente>\r\n" +
                                "</Integrador>";
            DateTime time = DateTime.Now;
            GerarXMLInput(xml);
            LerXMLOutPut(num.ToString(), time);

        }
        public void GerarXMLInput(string xml)
        {
            string arquivo = @"C:\Integrador\Input\comando.xml";
            StreamWriter sw = new StreamWriter(arquivo);
            sw.Write(xml);
            sw.Close();

        }
        public string LerXMLOutPut(string id, DateTime time)
        {

            int i;
            string caminho = @"C:\Integrador\Output";
            string param = "";
            Boolean contition = false;

            while (!contition)
            {
                string[] dados = Directory.GetFiles(caminho);
                for (i = 0; i < dados.Length; i++)
                {

                    DateTime f = File.GetLastWriteTime(dados[i]);

                    if (DateTime.Compare(f, time) > 0)
                    {
                        try
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(dados[i]);

                            param = doc.SelectSingleNode("Integrador").ChildNodes[0].ChildNodes[0].InnerText;

                            if (param == id)
                            {
                                MessageBox.Show(doc.InnerXml.ToString());
                                contition = true;
                                param = dados[i];
                                break;
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.ToString());

                        }
                    }
                }
            }
            return param;
        }
        public int GetNumeroSessao()
        {
            String numero, arquivo;
            int numConv;

            arquivo = "numeroSessao.txt";
            StreamWriter sw;
            StreamReader sr;

            if (!File.Exists(arquivo))
            {
                sw = new StreamWriter(arquivo);
                sw.Write("1");
                sw.Close();
            }
            else
            {
                sr = new StreamReader(arquivo);
                if (sr.ReadLine() == "")
                {
                    sr.Close();
                    sw = new StreamWriter(arquivo);
                    sw.Write("1");
                    sw.Close();
                }
                else
                {
                    sr.Close();
                }
            }

            sr = new StreamReader(arquivo);
            numero = sr.ReadLine();
            try
            {
                numConv = Convert.ToInt32(numero);
                sr.Close();

                if (numConv == 999999)
                {
                    sw = new StreamWriter(arquivo);
                    sw.Write("1");
                    sw.Close();
                }
                else
                {
                    sw = new StreamWriter(arquivo);
                    sw.Write(Convert.ToString(numConv + 1));
                    sw.Close();
                }
                return numConv;
            }
            catch (FormatException e)
            {
                MessageBox.Show("Conteudo do arquivo de configuração do numero de sessao não é um numero valido");
                MessageBox.Show(numero + " << Conteudo lido");
                throw;
            }
        }

        public void MFE_Venda()
        {

        }

    }
}
