using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP_SGE;
using Controle_Dados;
using Controles;
using System.Xml;
using System.IO;
using System.Collections;
using System.Diagnostics;



namespace FrenteLoja
{
    public partial class FrmMFeEnviarPgCartao : Form
    {
        Funcoes Controle = new Funcoes();        
        public FrmFrenteLoja FrmFrenteLoja;
        public bool PagOK = false;
        public DataRow PagCartao;
        public int IdPagamento = 0;
        public int IdRespFiscal = 0;
        public bool NovoIDPag = false;
        public string Bandeira = "";
        public string AdqBandeira = "";
        public decimal VlrVenda = 0;

        public FrmMFeEnviarPgCartao()
        {
            InitializeComponent();
        }
        private void FrmMFeEnviarPgCartao_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmFrenteLoja.FrmPrincipal.Conexao;

            Process[] processo = Process.GetProcessesByName("Integrador");
            bool UsoIntegrador = false;
            foreach (Process proc in processo)
            {
                UsoIntegrador = true;
                break;
            }

            if (!UsoIntegrador)
            {
                if (MessageBox.Show("Integrador não esta em execução, Deseja continuar ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                    Close();
            }

        }
        private void FrmMFeEnviarPgCartao_Shown(object sender, EventArgs e)
        {
            TxtNSU.Text    = "0";
            TxtNrAut.Text  = "0";            
            LblCartao.Text = PagCartao["DOCUMENTO"].ToString();
            LblValor.Text  = string.Format("{0:N2}", decimal.Parse(PagCartao["VlrDoc"].ToString()));

            if (int.Parse(PagCartao["IdPagMFE"].ToString()) == 0)
                IdPagamento = EnviarPagCartão(decimal.Parse(PagCartao["VlrDoc"].ToString()));
            else
                IdPagamento = int.Parse(PagCartao["IdPagMFE"].ToString());

            TxtIdFila.Text = IdPagamento.ToString();
        }       

        private void BtnEnviarPg_Click(object sender, EventArgs e)
        {
            string ArqRet = "";
            string[] ListArquivos = Directory.GetFiles(@"C:\Integrador\Output");
            foreach (string Arq in ListArquivos)
            {
                ArqRet = Path.GetFileName(Arq);
                System.IO.File.Delete(@"C:\Integrador\Output\" + ArqRet);
            }

            if (IdPagamento > 0)
            {
                if (TxtNSU.Text.Trim() == "0")
                {
                    VerifiscarStatusPag();

                    if (NovoIDPag)
                    {
                        TxtNSU.Focus();
                        NovoIDPag = false;
                        return;
                    }
                    if (TxtNSU.Text.Trim() == "0")
                    {
                        if (MessageBox.Show("Deseja Informar Manualmente o Codigo de Autorização e Codigo Pagamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            TxtNSU.Focus();
                            return;
                        }                        
                    }
                }

                if (TxtNSU.Text.Trim() != "0")
                    IdRespFiscal = EnviarRespFiscal();
                
                if (IdRespFiscal > 0)
                    MessageBox.Show("Pagamento Enviado com Sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Pagamento não enviado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            Close();
        }

        private void VerifiscarStatusPag()
        {
            TxtNrAut.Text = "0";
            TxtNSU.Text   = "0";

            ArrayList XmlPg = new ArrayList();
            XmlPg.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            XmlPg.Add("<Integrador>");
            XmlPg.Add("<Identificador>");
            XmlPg.Add("<Valor>1</Valor>");
            XmlPg.Add("</Identificador>");
            XmlPg.Add("<Componente Nome=\"VFP-e\">");
            XmlPg.Add("<Metodo Nome=\"VerificarStatusValidador\">");
            XmlPg.Add("<Construtor>");
            XmlPg.Add("<Parametros>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>chaveAcessoValidador</Nome>");
            XmlPg.Add("<Valor>" + FrmFrenteLoja.FrmPrincipal.Parametros_Filial.ChaveValidador.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("</Parametros>");
            XmlPg.Add("</Construtor>");
            XmlPg.Add("<Parametros>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>idFila</Nome>");
            XmlPg.Add("<Valor>" + IdPagamento + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>Cnpj</Nome>");
            if (FrmFrenteLoja.FrmPrincipal.Parametros_Filial.NFEAmbiente == 0)
                XmlPg.Add("<Valor>28763293000188</Valor>");
            else
                XmlPg.Add("<Valor>" + FrmFrenteLoja.CadFilial.Cnpj.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("</Parametros>");
            XmlPg.Add("</Metodo>");
            XmlPg.Add("</Componente>");
            XmlPg.Add("</Integrador>");

            try
            {
                string Xml = "";
                for (int i = 0; i <= XmlPg.Count - 1; i++)
                    Xml = Xml + XmlPg[i].ToString();

                StreamWriter XmlNota;
                DirectoryInfo VerPath = new DirectoryInfo(@"C:\Integrador\Input");

                if (!VerPath.Exists)
                {
                    MessageBox.Show("Pasta Input do Integrador não localizada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string vArqXML = @"C:\Integrador\Input\VerificarStatusPag.xml";
                XmlNota = File.CreateText(vArqXML);
                XmlNota.Write(Xml);
                XmlNota.Close();

                VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Input\\" + string.Format("{0:D3}", FrmFrenteLoja.FrmPrincipal.Parametros_Filial.IdFilial));
                if (!VerPath.Exists)
                    VerPath.Create();
                vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Input\\" + string.Format("{0:D3}", FrmFrenteLoja.FrmPrincipal.Parametros_Filial.IdFilial) + "\\VerStaPag" + PagCartao["ChaveCFe"].ToString().Trim() + ".xml";
                XmlNota = File.CreateText(vArqXML);
                XmlNota.Write(Xml);
                XmlNota.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao salvar na pasta Input do Integrador, erro: " + e.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Lendo o Retorno
            string ArqRet = "";
            for (int a = 1; a <= 10; a++)
            {
                string[] ListArquivos = Directory.GetFiles(@"C:\Integrador\Output");
                foreach (string Arq in ListArquivos)
                {
                    ArqRet = Path.GetFileName(Arq);
                }
                if (ArqRet != "")
                    break;
                else
                    System.Threading.Thread.Sleep(1000);
            }

            if (ArqRet == "")
            {
                System.IO.File.Delete(@"C:\Integrador\Input\VerificarStatusPag.xml");
                MessageBox.Show("Arquivo de Retorno do Pagamento não localizadona na pastra Output do Integrador", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(@"C:\Integrador\Output\" + ArqRet);
                try
                {
                    if (oXML.GetElementsByTagName("IdFila")[0].InnerText.ToString().Trim() == "0")
                        MessageBox.Show("Registro de Pagamento não Localizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        TxtNrAut.Text = oXML.GetElementsByTagName("CodigoAutorizacao")[0].InnerText;
                        TxtNSU.Text   = oXML.GetElementsByTagName("CodigoPagamento")[0].InnerText;                        
                        decimal Vlr   = decimal.Parse(oXML.GetElementsByTagName("ValorPagamento")[0].InnerText.ToString().Replace(".",","));
                        Bandeira      = oXML.GetElementsByTagName("Tipo")[0].InnerText;
                        AdqBandeira   = oXML.GetElementsByTagName("InstituicaoFinanceira")[0].InnerText;

                        if (Vlr!=decimal.Parse(PagCartao["VlrDoc"].ToString()))
                        {
                            System.IO.File.Delete(@"C:\Integrador\Output\" + ArqRet);
                            MessageBox.Show("Valor da VENDA diferente do valor recebido, favor estornar o recebimento e efetuar novo recebimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            IdPagamento           = EnviarPagCartão(decimal.Parse(PagCartao["VlrDoc"].ToString()));
                            PagCartao["IDPagMFE"] = IdPagamento.ToString();                            
                            IdRespFiscal   = 0;
                            TxtNrAut.Text  = "0";
                            TxtNSU.Text    = "0";
                            TxtIdFila.Text = IdPagamento.ToString();
                            Controle.ExecutaSQL("Update LancFinanceiro set IDPagMFE="+IdPagamento.ToString()+" where id_lanc=" + PagCartao["IdFinanc"].ToString());
                            NovoIDPag = true;
                            return;
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Registro de Pagamento não Autorizado, erro: " + e.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                System.IO.File.Delete(@"C:\Integrador\Output\" + ArqRet);
            }
            return;
        }
       
        public int EnviarPagCartão(decimal Vlr)
        {
            int NFila = 0;
            ArrayList XmlPg = new ArrayList();
            XmlPg.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");            
            XmlPg.Add("<Integrador>");
            XmlPg.Add("<Identificador>");
            XmlPg.Add("<Valor>1</Valor>");
            XmlPg.Add("</Identificador>");
            XmlPg.Add("<Componente Nome=\"VFP-e\">");
            XmlPg.Add("<Metodo Nome=\"EnviarPagamento\">");
            XmlPg.Add("<Construtor>");
            XmlPg.Add("<Parametros>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>chaveAcessoValidador</Nome>");
            XmlPg.Add("<Valor>" + FrmFrenteLoja.FrmPrincipal.Parametros_Filial.ChaveValidador.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("</Parametros>");
            XmlPg.Add("</Construtor>");
            XmlPg.Add("<Parametros>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>ChaveRequisicao</Nome>");
            XmlPg.Add("<Valor>" + FrmFrenteLoja.FrmPrincipal.Parametros_Filial.ChaveRequisicao.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>Estabelecimento</Nome>");
            XmlPg.Add("<Valor>0</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>SerialPos</Nome>");
            XmlPg.Add("<Valor>" + FrmFrenteLoja.FrmPrincipal.Parametros_Filial.SerialPOS.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>Cnpj</Nome>");
            if (FrmFrenteLoja.FrmPrincipal.Parametros_Filial.NFEAmbiente == 0)
                XmlPg.Add("<Valor>28763293000199</Valor>");  //SNPJ Contribuinte
            else
                XmlPg.Add("<Valor>" + FrmFrenteLoja.CadFilial.Cnpj.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>IcmsBase</Nome>");
            //XmlPg.Add("<Valor>0,00</Valor>");
            XmlPg.Add("<Valor>" + string.Format("{0:N2}", VlrVenda) + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>ValorTotalVenda</Nome>");
            XmlPg.Add("<Valor>" + string.Format("{0:N2}", decimal.Parse(Vlr.ToString())) + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>OrigemPagamento</Nome>");
            XmlPg.Add("<Valor>Venda 1</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>HabilitarMultiplosPagamentos</Nome>");
            XmlPg.Add("<Valor>true</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>HabilitarControleAntiFraude</Nome>");
            XmlPg.Add("<Valor>false</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>CodigoMoeda</Nome>");
            XmlPg.Add("<Valor>BRL</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>EmitirCupomNFCE</Nome>");
            XmlPg.Add("<Valor>false</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("</Parametros>");
            XmlPg.Add("</Metodo>");
            XmlPg.Add("</Componente>");
            XmlPg.Add("</Integrador>");

            try
            {
                string Xml = "";
                for (int i = 0; i <= XmlPg.Count - 1; i++)
                    Xml = Xml + XmlPg[i].ToString();

                StreamWriter XmlNota;
                DirectoryInfo VerPath = new DirectoryInfo(@"C:\Integrador\Input");
                
                if (!VerPath.Exists)
                {
                    MessageBox.Show("Pasta Input do Integrador não localizada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }
                string vArqXML = @"C:\Integrador\Input\IdPagamento.xml";                
                XmlNota = File.CreateText(vArqXML);
                XmlNota.Write(Xml);
                XmlNota.Close();
                                
                VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Input\\" + string.Format("{0:D3}", FrmFrenteLoja.FrmPrincipal.Parametros_Filial.IdFilial));
                if (!VerPath.Exists)
                    VerPath.Create();
                vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Input\\" + string.Format("{0:D3}", FrmFrenteLoja.FrmPrincipal.Parametros_Filial.IdFilial) + "\\IdPag" + PagCartao["ChaveCFe"].ToString().Trim() + ".xml";
                XmlNota = File.CreateText(vArqXML);
                XmlNota.Write(Xml);
                XmlNota.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao salvar na pasta Input do Integrador, erro: " + e.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            // Lendo o Retorno
            string ArqRet = "";
            for (int a = 1; a <= 10; a++)
            {
                string[] ListArquivos = Directory.GetFiles(@"C:\Integrador\Output");
                foreach (string Arq in ListArquivos)
                {
                    ArqRet = Path.GetFileName(Arq);
                }
                if (ArqRet != "")
                    break;
                else
                    System.Threading.Thread.Sleep(1000);
            }

            if (ArqRet == "")
            {
                System.IO.File.Delete(@"C:\Integrador\Input\IdPagamento.xml");
                MessageBox.Show("Arquivo de Retorno do Pagamento não localizadona na pastra Output do Integrador", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            else
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(@"C:\Integrador\Output\"+ArqRet);
                try
                {
                    string IdPag = oXML.GetElementsByTagName("IdPagamento")[0].InnerText;
                    NFila = int.Parse(IdPag);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Registro de Pagamento não Autorizado, erro: " + e.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NFila = 0;
                }

                System.IO.File.Delete(@"C:\Integrador\Output\" + ArqRet);
            }
            return NFila;
        }

        public int EnviarRespFiscal()
        {
            int NResp = 0;
            ArrayList XmlPg = new ArrayList();
            XmlPg.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            XmlPg.Add("<Integrador>");
            XmlPg.Add("<Identificador>");
            XmlPg.Add("<Valor>1</Valor>");
            XmlPg.Add("</Identificador>");
            XmlPg.Add("<Componente Nome=\"VFP-e\">");
            XmlPg.Add("<Metodo Nome=\"RespostaFiscal\">");
            XmlPg.Add("<Construtor>");
            XmlPg.Add("<Parametros>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>chaveAcessoValidador</Nome>");
            XmlPg.Add("<Valor>" + FrmFrenteLoja.FrmPrincipal.Parametros_Filial.ChaveValidador.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("</Parametros>");
            XmlPg.Add("</Construtor>");
            XmlPg.Add("<Parametros>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>idFila</Nome>");
            XmlPg.Add("<Valor>"+IdPagamento+"</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>ChaveAcesso</Nome>");
            XmlPg.Add("<Valor>" + PagCartao["ChaveCFe"].ToString() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>Nsu</Nome>");
            XmlPg.Add("<Valor>" + TxtNSU.Text.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>NumerodeAprovacao</Nome>");
            XmlPg.Add("<Valor>" + TxtNrAut.Text.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>Bandeira</Nome>");
            if (Bandeira == "")
                XmlPg.Add("<Valor>" + PagCartao["Documento"].ToString() + "</Valor>");
            else
                XmlPg.Add("<Valor>" + Bandeira + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>Adquirente</Nome>");
            if (AdqBandeira == "")
                XmlPg.Add("<Valor>" + PagCartao["Adquirente"].ToString() + "</Valor>");
            else
                XmlPg.Add("<Valor>" + AdqBandeira + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>Cnpj</Nome>");
            if (FrmFrenteLoja.FrmPrincipal.Parametros_Filial.NFEAmbiente == 0)
                XmlPg.Add("<Valor>28763293000188</Valor>");
            else
                XmlPg.Add("<Valor>" + FrmFrenteLoja.CadFilial.Cnpj.Trim() + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>ImpressaoFiscal</Nome>");
           // if (Bandeira != "")
            //{
           //     XmlPg.Add("<Valor><![CDATA[" + ImpressaoFiscal());
           //     XmlPg.Add("]]></Valor>");
           // }
            XmlPg.Add("</Parametro>");
            XmlPg.Add("<Parametro>");
            XmlPg.Add("<Nome>NumeroDocumento</Nome>");
            XmlPg.Add("<Valor>"+ PagCartao["ChaveCFe"].ToString().Substring(34,6) + "</Valor>");
            XmlPg.Add("</Parametro>");
            XmlPg.Add("</Parametros>");
            XmlPg.Add("</Metodo>");
            XmlPg.Add("</Componente>");
            XmlPg.Add("</Integrador>");

            try
            {
                string Xml = "";
                for (int i = 0; i <= XmlPg.Count - 1; i++)
                    Xml = Xml + XmlPg[i].ToString();

                StreamWriter XmlNota;
                DirectoryInfo VerPath = new DirectoryInfo(@"C:\Integrador\Input");

                if (!VerPath.Exists)
                {
                    MessageBox.Show("Pasta Input do Integrador não localizada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }
                string vArqXML = @"C:\Integrador\Input\RespostaFiscal.xml";
                XmlNota = File.CreateText(vArqXML);
                XmlNota.Write(Xml);
                XmlNota.Close();

                VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Input\\" + string.Format("{0:D3}", FrmFrenteLoja.FrmPrincipal.Parametros_Filial.IdFilial));
                if (!VerPath.Exists)
                    VerPath.Create();
                vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Input\\" + string.Format("{0:D3}", FrmFrenteLoja.FrmPrincipal.Parametros_Filial.IdFilial) + "\\IdRespFiscal" + PagCartao["ChaveCFe"].ToString().Trim() + ".xml";
                XmlNota = File.CreateText(vArqXML);
                XmlNota.Write(Xml);
                XmlNota.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao salvar na pasta Input do Integrador, erro: " + e.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            // Lendo o Retorno
            string ArqRet = "";
            for (int a = 1; a <= 10; a++)
            {
                string[] ListArquivos = Directory.GetFiles(@"C:\Integrador\Output");
                foreach (string Arq in ListArquivos)
                {
                    ArqRet = Path.GetFileName(Arq);
                }
                if (ArqRet != "")
                    break;
                else
                    System.Threading.Thread.Sleep(1000);
            }

            if (ArqRet == "")
            {
                MessageBox.Show("Arquivo de Retorno da Respota Fiscal não localizadona na pastra Output do Integrador", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            else
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(@"C:\Integrador\Output\" + ArqRet);
                try
                {
                    string IdResp = oXML.GetElementsByTagName("retorno")[0].InnerText;
                    NResp = int.Parse(IdResp);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Registro de Pagamento não Autorizado, erro: " + e.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NResp = 0;
                }
                System.IO.File.Delete(@"C:\Integrador\Output\" + ArqRet);
            }
            return NResp;
        }

        private string ImpressaoFiscal()
        {
            string imp = "TANCA INFORMATICA EIRELI \n";
            imp = imp + "RUA MARECHAL FLORIANO PEIXOTO, 166 \n";
            imp = imp + "VILA MARCONDES \n";
            imp = imp + "PRESIDENTE PRUDENTE  CEP:19030020 \n";
            imp = imp + "CNPJ: 08723218000186 IE: 562377111111 \n";
            imp = imp + "------------------------------------------------- \n";
            imp = imp + "EXTRATO No."+ PagCartao["ChaveCFe"].ToString().Substring(34, 6)+" do CUPOM FISCAL ELETRONICO - SAT \n";
            imp = imp + "#COD | DES | UN | QTD | VL UN R$ | (VL TR R$)*|VL ITEM R$ \n";
            imp = imp + "------------------------------------------------- \n";
            imp = imp + "001 010119 AIR IGUATEMI Und 1,000  x 12,10  12,10 \n";
            imp = imp + "------------------------------------------------- \n";
            imp = imp + "TOTAL R$                                    12,10 \n";
            imp = imp + "cartão                                      12,10 \n";
            imp = imp + "OBSERVACOES DO CONTRIBUINTE \n";
            imp = imp + "OP: 01 Nome OP: Lucio Op PDV: 01 Nr.NF:" + PagCartao["ChaveCFe"].ToString().Substring(34, 6) + " \n";
            imp = imp + "Valor aproximado dos tributos deste cupom: R$  0,00 \n";
            imp = imp + "(Conforme Lei Federal 12.741 / 2012) \n";

            return imp;

        }

    }
}
;