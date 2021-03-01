using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using Controle_Dados;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Threading;
using System.Xml.Schema;
using System.Windows.Forms;
using System.Net;

namespace Controles
{    
    public class NF_e
    {
        public Funcoes Controle    = new Funcoes();
        public Parametros ParamNFE = new Parametros();
        public Estados CadUF       = new Estados();
        public Filiais CadFilial   = new Filiais();
        public string nChaveNF;
        public string nProtocoloNF;
        public string nReciboNF;
        public string vMotivoRet;
        public string vXMLRetorno;
        public string cStat = "0";
        public string CnpjNFe = "";                

        public void Inicializar_parametros(int IdFilial)
        {
            ParamNFE.Controle = Controle;
            ParamNFE.LerDados(IdFilial);
            CadFilial.Controle = Controle;
            CadFilial.LerDados(IdFilial);
            CadUF.Controle = Controle;
            CadUF.LerDados(CadFilial.Uf);
        }

        public string NaturezaOpNFE(int Op)
        {
            if (Op == 0)
                return "VENDA";
            else if (Op == 1)
                return "TRANSFERÊNCIA";
            else if (Op == 2)
                return "BONIFICAÇÃO";
            else if (Op == 3)
                return "DEVOLUÇÃO";
            else if (Op == 4)
                return "COMPRA";
            else if (Op == 5)
                return "REMESSA";
            else if (Op == 6)
                return "DEMOSTRAÇÃO";
            else if (Op == 7)
                return "RETORNO";
            else if (Op == 8)
                return "EXPOSIÇÃO";
            else if (Op == 9)
                return "OUTRAS";
            else if (Op == 10)
                return "VENDA A ORDEM";
            else if (Op == 11)
                return "REMESSA MERC. POR CONTA E ORDERM DE TERC.";
            else if (Op == 12)
                return "COMPLEMENTAR";                       
            else
                return "";            
        }

        public string CodCST(int Op)
        {
            if (Op == 0)
                return "00";
            else if (Op == 1)
                return "00";
            else if (Op == 2)
                return "10";
            else if (Op == 3)
                return "20";
            else if (Op == 4)
                return "30";
            else if (Op == 5)
                return "40";
            else if (Op == 6)
                return "40";
            else if (Op == 7)
                return "50";
            else if (Op == 8)
                return "60";
            else if (Op == 9)
                return "70";
            else if (Op == 10)
                return "90";
            else
                return "";
        }
        
        public string GerarXmlNF(int IdNota, Filiais Filial, int TpEmissao)
        {
            /// TpEmissao 1-Normal 2-Contigência Form Seguraca 3-Contigência SCAN 4-Contigencia DPEC 5-Contigencia FS-DA Form Segurança DANFE         
            
            try
            {
                Verificar BuscaAux = new Verificar();
                BuscaAux.Controle = Controle;
                //
                /*Estados CadUF = new Estados();
                CadUF.Controle = Controle;
                CadUF.LerDados(Filial.Uf);*/
                
                ArrayList XmlNF = new ArrayList();
                DataSet Nota = new DataSet();
                string sSQL = "SELECT T2.ENTSAIDA,T2.RAZAOSOCIAL,T2.CNPJCPF,T2.INSC_UF,T2.DTEMISSAO,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO," +
                              "       T2.TELEFONE,T2.CEP,T2.BAIRRO,T2.CIDADE,T4.SIGLA,T4.CODIBGE,T3.CODBARRA,T3.REFERENCIA,T3.DESCRICAO,T3.UNIDADE,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T1.PICMS,T1.VLRICMS AS ICMSITEM,T1.PERCRED,T1.PIPI,T1.VlrIpi as VlrIpiItem," +
                              "       T2.BICMS,T2.VLRICMS,T2.BICMSSUB,T2.VLRICMSSUB,T2.VLRFRETE,T2.VLRIPI AS IPINOTA,T2.VLRSEGURO,T2.VLROUTRADESP,T2.VLRFRETE,T2.VLRPRODUTOS,T2.VLRNOTA,T2.VLRDESCONTO,T2.FRETE," +
                              "       T2.QTDEVOLUME,T2.ESPECIE,T2.MARCA,T2.PESOBRUTO,T2.PESOLIQUIDO,T2.NUMFORMULARIO,T2.NUMNOTA,T2.OBSERVACAO,T5.CFOP,T5.NATUREZA,T7.VENDEDOR,ISNULL(T8.PRIMPARCELA,0) AS PRIMPARCELA,T3.NCM,T2.CONSUMIDOR,T2.ATENDIMENTO,T2.DESTOPERACAO," +
                              "       T2.FINALIDADE,T2.ChaveNfeDev,ISNULL(T2.NATOP,0) AS NATOP, IsNull(T1.CST,0) as CST_ICMS,T2.CODMUN,T2.NUMPEDIDO,ISNULL(T1.ITEMPED,0) AS ITEMPED,Isnull(T2.ICMSInterno,0) as ICMSInterno, Isnull(T2.PercDifal,0) as PercDifal, " +
                              "       T2.ID_TRANSPORTADORA,T9.RazaoSocial AS TRANSPORTADORA, T9.CNPJ AS CNPJTRANSP,T9.Cidade AS CidadeTransp,ET.Sigla as UFTransp, T1.CODPRDCLIENTE" +
                              " FROM NOTAFISCALITENS T1" +
                              "   LEFT JOIN NOTAFISCAL T2 ON (T2.ID_NOTA=T1.ID_NOTA)" +
                              "   LEFT JOIN PRODUTOS   T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              "   LEFT JOIN ESTADOS    T4 ON (T4.ID_UF=T2.ID_UF)" +
                              "   LEFT JOIN CFOP       T5 ON (T5.ID_CFOP=T1.ID_CFOP)" +
                              "   LEFT OUTER JOIN MVVENDA AS T6 ON T6.ID_VENDA=T2.ID_VENDA" +
                              "   LEFT OUTER JOIN VENDEDORES AS T7 ON T7.ID_VENDEDOR=T6.ID_VENDEDOR" +
                              "   LEFT OUTER JOIN FORMAPAGAMENTO AS T8 ON T8.ID_FORMAPGTO=T6.ID_FORMAPGTO" +
                              "   LEFT OUTER JOIN Transportadoras T9 ON (T9.Id_Transportadora=T2.Id_Transportadora)" +
                              "   LEFT JOIN ESTADOS    ET ON (ET.ID_UF=T9.ID_UF)" +
                              " WHERE T1.ID_NOTA=" + IdNota.ToString();

                Nota = Controle.ConsultaTabela(sSQL);
                string ChaveNF = "";
                // Cabeçalho 
                //string ChaveNF = "23" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + Filial.Cnpj.Trim() + "55001" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString()));           
                if (CadUF.Sigla == "CE")
                    ChaveNF = "23" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + Filial.Cnpj.Trim() + "55001" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "1" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString()));
                else
                    ChaveNF = "25" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + Filial.Cnpj.Trim() + "55001" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "1" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString()));

                nChaveNF = ChaveNF + DVChaveNF(ChaveNF);

                XmlNF.Add("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");


                if (ParamNFE.NFEVersao == 0)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.10\">");
                else if (ParamNFE.NFEVersao == 1)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">");
                else if (ParamNFE.NFEVersao == 2)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"3.10\">");
                else if (ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">");

                //XmlNF.Add("<idLote>" + string.Format("{0:D5}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + string.Format("{0:D6}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString())) + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + "</idLote>");
                XmlNF.Add("<idLote>" + string.Format("{0:D12}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "</idLote>");//+ DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + "</idLote>");
                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<indSinc>0</indSinc>");

                XmlNF.Add("<NFe>");

                if (ParamNFE.NFEVersao == 0)
                    XmlNF.Add("<infNFe xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Id=\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"1.10\">");
                else if (ParamNFE.NFEVersao == 1)
                    XmlNF.Add("<infNFe xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Id=\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"2.00\">");
                else if (ParamNFE.NFEVersao == 2)
                    XmlNF.Add("<infNFe xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Id=\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"3.10\">");
                else if (ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<infNFe Id =\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"4.00\">");

                //Dados da Nota
                XmlNF.Add("<ide>");
                if (CadUF.Sigla == "CE")
                    XmlNF.Add("<cUF>23</cUF>");
                else
                    XmlNF.Add("<cUF>25</cUF>");
                XmlNF.Add("<cNF>" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString())) + "</cNF>");
                XmlNF.Add("<natOp>" + NaturezaOpNFE(int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString())) + "</natOp>");

                if (ParamNFE.NFEVersao == 2)
                {
                    if (int.Parse(Nota.Tables[0].Rows[0]["PRIMPARCELA"].ToString()) > 0)
                        XmlNF.Add("<indPag>1</indPag>");
                    else
                        XmlNF.Add("<indPag>0</indPag>");
                }

                XmlNF.Add("<mod>55</mod>");
                XmlNF.Add("<serie>1</serie>");
                XmlNF.Add("<nNF>" + Nota.Tables[0].Rows[0]["NUMNOTA"].ToString() + "</nNF>");
                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                {
                    XmlNF.Add("<dhEmi>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhEmi>");
                    XmlNF.Add("<dhSaiEnt>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhSaiEnt>");
                }
                else
                {
                    XmlNF.Add("<dEmi>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-dd") + "</dEmi>");
                    XmlNF.Add("<dSaiEnt>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-dd") + "</dSaiEnt>");
                }
                if (Nota.Tables[0].Rows[0]["EntSaida"].ToString() == "0")
                    XmlNF.Add("<tpNF>1</tpNF>");
                else
                    XmlNF.Add("<tpNF>0</tpNF>");

                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<idDest>" + (int.Parse(Nota.Tables[0].Rows[0]["DESTOPERACAO"].ToString()) + 1).ToString() + "</idDest>");

                //XmlNF.Add("<cMunFG>2507507</cMunFG>");
                XmlNF.Add("<cMunFG>"+CadUF.CodIBGE.ToString().Trim()+"</cMunFG>"); 
                XmlNF.Add("<tpImp>1</tpImp>");
                XmlNF.Add("<tpEmis>" + TpEmissao.ToString() + "</tpEmis>");
                XmlNF.Add("<cDV>" + DVChaveNF(ChaveNF) + "</cDV>");

                if (ParamNFE.NFEAmbiente == 0)
                    XmlNF.Add("<tpAmb>2</tpAmb>");
                else
                    XmlNF.Add("<tpAmb>1</tpAmb>");
                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<finNFe>" + (int.Parse(Nota.Tables[0].Rows[0]["FINALIDADE"].ToString()) + 1).ToString() + "</finNFe>");
                else
                    XmlNF.Add("<finNFe>1</finNFe>");

                XmlNF.Add("<indFinal>" + Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString() + "</indFinal>");

                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                {
                    if (int.Parse(Nota.Tables[0].Rows[0]["ATENDIMENTO"].ToString()) == 5)
                        XmlNF.Add("<indPres>9</indPres>");
                    else
                        XmlNF.Add("<indPres>" + Nota.Tables[0].Rows[0]["ATENDIMENTO"].ToString() + "</indPres>");
                }
                XmlNF.Add("<procEmi>0</procEmi>");
                XmlNF.Add("<verProc>ERP-SGE 1.0</verProc>");
                if (int.Parse(Nota.Tables[0].Rows[0]["FINALIDADE"].ToString()) != 0)
                {
                    XmlNF.Add("<NFref>");
                    XmlNF.Add("<refNFe>" + Nota.Tables[0].Rows[0]["ChaveNfeDev"].ToString().Trim() + "</refNFe>");
                    XmlNF.Add("</NFref>");
                }
                XmlNF.Add("</ide>");


                //Dados do Emitente
                XmlNF.Add("<emit>");
                XmlNF.Add("<CNPJ>" + Filial.Cnpj.Trim() + "</CNPJ>");
                XmlNF.Add("<xNome>" + Filial.Filial.Trim().Replace("-", "") + "</xNome>");
                XmlNF.Add("<xFant>" + Filial.Fantasia.Trim() + "</xFant>");
                XmlNF.Add("<enderEmit>");
                XmlNF.Add("<xLgr>" + Filial.Endereco.Trim() + "</xLgr> ");
                XmlNF.Add("<nro>" + Filial.Numero.Trim() + "</nro>"); ;
                XmlNF.Add("<xCpl>" + Filial.Complemento.Trim() + "</xCpl>");
                XmlNF.Add("<xBairro>" + Filial.Bairro.Trim() + "</xBairro>");
                //XmlNF.Add("<cMun>"+CadUF.CodIBGE.ToString().Trim()+"</cMun>");
                XmlNF.Add("<cMun>" + Filial.CodMun.ToString().Trim() + "</cMun>");
                XmlNF.Add("<xMun>" + Filial.Cidade.Trim() + "</xMun>");
                XmlNF.Add("<UF>" + BuscaAux.Busca_SiglaUF(Filial.Uf) + "</UF>");
                XmlNF.Add("<CEP>" + Filial.Cep.Trim() + "</CEP>");
                XmlNF.Add("<cPais>1058</cPais>");
                XmlNF.Add("<xPais>Brasil</xPais>");
                XmlNF.Add("<fone>" + Filial.Fone1.Trim() + "</fone>");
                XmlNF.Add("</enderEmit>");
                XmlNF.Add("<IE>" + Filial.InscUF.Replace(".", "").Replace("-", "").Replace("/", "").Replace(",", "").Trim() + "</IE>");
                XmlNF.Add("<CRT>" + (Filial.Regime + 1).ToString() + "</CRT>");
                XmlNF.Add("</emit>");

                //Dados Destinatario
                XmlNF.Add("<dest>");
                if (Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim().Length > 11)
                    XmlNF.Add("<CNPJ>" + Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim() + "</CNPJ>");
                else
                    XmlNF.Add("<CPF>" + Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim() + "</CPF>");
                XmlNF.Add("<xNome>" + Nota.Tables[0].Rows[0]["RAZAOSOCIAL"].ToString().Replace("Ç", "C").Trim() + "</xNome>");
                XmlNF.Add("<enderDest>");
                XmlNF.Add("<xLgr>" + Nota.Tables[0].Rows[0]["ENDERECO"].ToString().Trim() + "</xLgr>");
                XmlNF.Add("<nro>" + Nota.Tables[0].Rows[0]["NUMERO"].ToString().Trim() + "</nro>");

                if (Nota.Tables[0].Rows[0]["COMPLEMENTO"].ToString().Trim() != "")
                    XmlNF.Add("<xCpl>" + Nota.Tables[0].Rows[0]["COMPLEMENTO"].ToString().Trim() + "</xCpl>");

                XmlNF.Add("<xBairro>" + Nota.Tables[0].Rows[0]["BAIRRO"].ToString().Trim() + "</xBairro>");
                XmlNF.Add("<cMun>" + Nota.Tables[0].Rows[0]["CODMUN"].ToString().Trim() + "</cMun>");
                XmlNF.Add("<xMun>" + Nota.Tables[0].Rows[0]["CIDADE"].ToString().Trim() + "</xMun>");
                XmlNF.Add("<UF>" + Nota.Tables[0].Rows[0]["SIGLA"].ToString().Trim() + "</UF>");
                XmlNF.Add("<CEP>" + Nota.Tables[0].Rows[0]["CEP"].ToString().Trim() + "</CEP>");
                XmlNF.Add("<cPais>1058</cPais>");
                XmlNF.Add("<xPais>BRASIL</xPais>");
                XmlNF.Add("<fone>" + Nota.Tables[0].Rows[0]["TELEFONE"].ToString().Trim() + "</fone>");
                XmlNF.Add("</enderDest>");

                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                {
                    if (Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim().Replace(",", "").Trim() == "")
                        XmlNF.Add("<indIEDest>9</indIEDest>");
                    else
                    {
                        XmlNF.Add("<indIEDest>1</indIEDest>");
                        XmlNF.Add("<IE>" + Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim().Replace(",", "").Trim() + "</IE>");
                    }
                }
                else
                    XmlNF.Add("<IE>" + Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim().Replace(",", "").Trim() + "</IE>");
                XmlNF.Add("</dest>");

                // Itens da Nota
                decimal TotalPis = 0;
                decimal TotalCofins = 0;
                decimal TotalDesconto = 0;
                decimal TotIcmsDest = 0;
                decimal TotIcmsReme = 0;
                decimal TotFCPDest = 0;

                for (int I = 0; I <= Nota.Tables[0].Rows.Count - 1; I++)
                {
                    XmlNF.Add("<det nItem=\"" + (I + 1).ToString() + "\">");
                    XmlNF.Add("<prod>");
                    XmlNF.Add("<cProd>" + Nota.Tables[0].Rows[I]["Referencia"].ToString().Trim() + "</cProd>");
                    if (Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() != "")
                        XmlNF.Add("<cEAN>" + Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() + "</cEAN>");
                    else
                        XmlNF.Add("<cEAN></cEAN>");

                    if (int.Parse(Nota.Tables[0].Rows[I]["ItemPed"].ToString()) > 0)
                        XmlNF.Add("<xProd>" + Nota.Tables[0].Rows[I]["Descricao"].ToString().Trim() + " cProdCliente:\"" + Nota.Tables[0].Rows[I]["CodPrdCliente"].ToString().Trim() + "\" OC:" + Nota.Tables[0].Rows[I]["ItemPed"].ToString().Trim() + "</xProd>");
                    else
                        XmlNF.Add("<xProd>" + Nota.Tables[0].Rows[I]["Descricao"].ToString().Trim() + "</xProd>");

                    if (Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() != "")
                        XmlNF.Add("<NCM>" + Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() + "</NCM>");
                    else
                        XmlNF.Add("<NCM>38089919</NCM>");
                    XmlNF.Add("<CEST>1300402</CEST>");
                    XmlNF.Add("<CFOP>" + Nota.Tables[0].Rows[I]["CFOP"].ToString().Replace(".", "").Trim() + "</CFOP>");
                    XmlNF.Add("<uCom>" + Nota.Tables[0].Rows[I]["Unidade"].ToString().Trim() + "</uCom>");
                    XmlNF.Add("<qCom>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["Qtde"].ToString())).Replace(".", "").Replace(",", ".") + "</qCom>");
                    XmlNF.Add("<vUnCom>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrUnitario"].ToString())).Replace(".", "").Replace(",", ".") + "</vUnCom>");
                    XmlNF.Add("<vProd>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vProd>");
                    if (Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() != "")
                        XmlNF.Add("<cEANTrib>" + Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() + "</cEANTrib>");
                    else
                        XmlNF.Add("<cEANTrib></cEANTrib>");
                    XmlNF.Add("<uTrib>" + Nota.Tables[0].Rows[I]["Unidade"].ToString().Trim() + "</uTrib>");
                    XmlNF.Add("<qTrib>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["Qtde"].ToString())).Replace(".", "").Replace(",", ".") + "</qTrib>");                    
                    XmlNF.Add("<vUnTrib>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrUnitario"].ToString())).Replace(".", "").Replace(",", ".") + "</vUnTrib>");

                    if (decimal.Parse(Nota.Tables[0].Rows[0]["VLRFRETE"].ToString()) > 0)
                        XmlNF.Add("<vFrete>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRFRETE"].ToString())).Replace(".", "").Replace(",", ".") + "</vFrete>");

                    if (decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString()) > 0)
                    {
                        decimal VlrDesconto = Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * (100 / (decimal.Parse(Nota.Tables[0].Rows[I]["VLRPRODUTOS"].ToString()) / decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString())) / 100), 2);
                        TotalDesconto = TotalDesconto + VlrDesconto;
                        if (string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * (100 / (decimal.Parse(Nota.Tables[0].Rows[I]["VLRPRODUTOS"].ToString()) / decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString())) / 100)).Replace(".", "").Replace(",", ".") != "0.00")
                            XmlNF.Add("<vDesc>" + string.Format("{0:N2}", ((VlrDesconto).ToString()).Replace(".", "").Replace(",", ".")) + "</vDesc>");
                    }

                    XmlNF.Add("<indTot>1</indTot>");
                    if (int.Parse(Nota.Tables[0].Rows[I]["ItemPed"].ToString()) > 0)
                    {
                        XmlNF.Add("<xPed>" + Nota.Tables[0].Rows[I]["NumPedido"].ToString().Trim() + "</xPed>");
                        XmlNF.Add("<nItemPed>" + Nota.Tables[0].Rows[I]["ItemPed"].ToString().Trim() + "</nItemPed>");
                    }
                    XmlNF.Add("</prod>");
                    XmlNF.Add("<imposto>");
                    //ICMS                
                    if (decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString()) > 0)
                    {
                        XmlNF.Add("<ICMS>");
                        //XmlNF.Add("<ICMS00>");
                        XmlNF.Add("<ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                        XmlNF.Add("<orig>0</orig>");
                        XmlNF.Add("<CST>" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + "</CST>");
                        XmlNF.Add("<modBC>0</modBC>");
                        if (decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString()) > 0)
                        {
                            if (decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) > 0)
                            {
                                XmlNF.Add("<pRedBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString())).Replace(".", "").Replace(",", ".") + "</pRedBC>");
                                XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) - Math.Round((decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) / 100), 2)).Replace(".", "").Replace(",", ".") + "</vBC>");
                            }
                            else
                                XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                        }
                        else
                            XmlNF.Add("<vBC>0.00</vBC>");
                        XmlNF.Add("<pICMS>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString())).Replace(".", "").Replace(",", ".") + "</pICMS>");
                        XmlNF.Add("<vICMS>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["IcmsItem"].ToString())).Replace(".", "").Replace(",", ".") + "</vICMS>");
                        //XmlNF.Add("</ICMS00>");
                        XmlNF.Add("</ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                        XmlNF.Add("</ICMS>");
                    }
                    else
                    {
                        if (Filial.Regime != 2)
                        {
                            XmlNF.Add("<ICMS>");
                            XmlNF.Add("<ICMSSN102>");
                            XmlNF.Add("<orig>0</orig>");
                            XmlNF.Add("<CSOSN>102</CSOSN>");
                            /*if (int.Parse(Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString()) == 1)
                            {
                                XmlNF.Add("<pRedBCEfet>0.00</pRedBCEfet>");
                                XmlNF.Add("<vBCEfet>0.00</vBCEfet>");
                                XmlNF.Add("<pICMSEfet>0.00</pICMSEfet>");
                                XmlNF.Add("<vICMSEfet>0.00</vICMSEfet>");
                            }
                            else
                            {
                                XmlNF.Add("<vBCSTRet>0.00</vBCSTRet>");
                                XmlNF.Add("<pST>0.00</pST>");
                                XmlNF.Add("<vICMSSubstituto>0.00</vICMSSubstituto>");
                                XmlNF.Add("<vICMSSTRet>0.00</vICMSSTRet>");
                            }*/
                            XmlNF.Add("</ICMSSN102>");
                            XmlNF.Add("</ICMS>");
                        }
                        else
                        {
                            XmlNF.Add("<ICMS>");
                            //XmlNF.Add("<ICMS40>");
                            XmlNF.Add("<ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                            XmlNF.Add("<orig>0</orig>");
                            //XmlNF.Add("<CST>41</CST>");
                            XmlNF.Add("<CST>" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + "</CST>");
                            //XmlNF.Add("</ICMS40>");
                           /* if (int.Parse(Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString()) == 1)
                            {
                                //XmlNF.Add("<pRedBCEfet>0.00</pRedBCEfet>");
                                XmlNF.Add("<vBCEfet>0.00</vBCEfet>");
                                XmlNF.Add("<pICMSEfet>0.00</pICMSEfet>");
                                XmlNF.Add("<vICMSEfet>0.00</vICMSEfet>");
                            }
                            else
                            {
                                XmlNF.Add("<vBCSTRet>0.00</vBCSTRet>");
                                XmlNF.Add("<pST>0.00</pST>");
                                XmlNF.Add("<vICMSSubstituto>0.00</vICMSSubstituto>");
                                XmlNF.Add("<vICMSSTRet>0.00</vICMSSTRet>");
                            }*/
                            XmlNF.Add("</ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                            XmlNF.Add("</ICMS>");
                        }
                    }
                    //IPI
                    XmlNF.Add("<IPI>");
                    if (decimal.Parse(Nota.Tables[0].Rows[I]["Pipi"].ToString()) > 0)
                    {
                        XmlNF.Add("<cEnq>999</cEnq>");
                        XmlNF.Add("<IPITrib>");
                        XmlNF.Add("<CST>50</CST>");
                        XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                        XmlNF.Add("<pIPI>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["Pipi"].ToString())).Replace(".", "").Replace(",", ".") + "</pIPI>");
                        XmlNF.Add("<vIPI>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrIpiItem"].ToString())).Replace(".", "").Replace(",", ".") + "</vIPI>");
                        XmlNF.Add("</IPITrib>");
                    }
                    else
                    {
                        XmlNF.Add("<cEnq>999</cEnq>");
                        XmlNF.Add("<IPINT>");
                        XmlNF.Add("<CST>53</CST>");
                        XmlNF.Add("</IPINT>");
                    }
                    XmlNF.Add("</IPI>");
                    //PIS
                    XmlNF.Add("<PIS>");
                    XmlNF.Add("<PISAliq>");
                    XmlNF.Add("<CST>01</CST>");
                    XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                    //XmlNF.Add("<pPIS>1.65</pPIS>");
                    XmlNF.Add("<pPIS>" + string.Format("{0:N2}", ParamNFE.PercPIS).Replace(".", "").Replace(",", ".") + "</pPIS>");
                    XmlNF.Add("<vPIS>" + string.Format("{0:N2}", (Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercPIS) / 100), 2).Replace(".", "").Replace(",", ".") + "</vPIS>");
                    XmlNF.Add("</PISAliq>");
                    XmlNF.Add("</PIS>");
                    //TotalPis = TotalPis + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("1,65") / 100, 2);
                    TotalPis = TotalPis + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercPIS / 100, 2);
                    //COFINS
                    XmlNF.Add("<COFINS>");
                    XmlNF.Add("<COFINSAliq>");
                    XmlNF.Add("<CST>01</CST>");
                    XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                    //XmlNF.Add("<pCOFINS>7.60</pCOFINS>");
                    XmlNF.Add("<pCOFINS>" + string.Format("{0:N2}", ParamNFE.PercCOFINS).Replace(".", "").Replace(",", ".") + "</pCOFINS>");
                    XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercCOFINS) / 100), 2).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                    //XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("7,60")) / 100).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                    XmlNF.Add("</COFINSAliq>");
                    XmlNF.Add("</COFINS>");
                    //TotalCofins = TotalCofins + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("7,60") / 100, 2);
                    TotalCofins = TotalCofins + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercCOFINS / 100, 2);

                    //Grupo de Tributação do ICMS para UF de Destino
                    if ((int.Parse(Nota.Tables[0].Rows[0]["DESTOPERACAO"].ToString()) + 1) == 2 && int.Parse(Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString()) == 1 && Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim() == "" && Nota.Tables[0].Rows[0]["EntSaida"].ToString() == "0")
                    {
                        decimal BaseIcms = 0;
                        if (decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) > 0)
                            BaseIcms = decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) - Math.Round((decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) / 100), 2);
                        else
                            BaseIcms = decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString());
                        //if (decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString()) > 0 && (int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 5 && int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 7))
                        if ((int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 5 && int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 7))
                        {

                            decimal DifIcms  = decimal.Parse(Nota.Tables[0].Rows[I]["ICMSInterno"].ToString()) - decimal.Parse("12,00");
                            decimal VlrDif   = Math.Round((BaseIcms * DifIcms) / 100, 2);
                            decimal IcmsDest = Math.Round((VlrDif * decimal.Parse(Nota.Tables[0].Rows[I]["PercDifal"].ToString())) / 100, 2);
                            decimal IcmsReme = Math.Round((VlrDif * (100 - decimal.Parse(Nota.Tables[0].Rows[I]["PercDifal"].ToString()))) / 100, 2);

                            TotIcmsDest = TotIcmsDest + IcmsDest;
                            TotIcmsReme = TotIcmsReme + IcmsReme;
                            TotFCPDest  = TotFCPDest + Math.Round((BaseIcms * decimal.Parse("2,00")) / 100, 2, MidpointRounding.AwayFromZero);

                            XmlNF.Add("<ICMSUFDest>");
                            XmlNF.Add("<vBCUFDest>" + string.Format("{0:N2}", BaseIcms).Replace(".", "").Replace(",", ".") + "</vBCUFDest>");
                            XmlNF.Add("<vBCFCPUFDest>" + string.Format("{0:N2}", BaseIcms).Replace(".", "").Replace(",", ".") + "</vBCFCPUFDest>");
                            XmlNF.Add("<pFCPUFDest>2.00</pFCPUFDest>");
                            XmlNF.Add("<pICMSUFDest>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["ICMSInterno"].ToString())).Replace(".", "").Replace(",", ".") + "</pICMSUFDest>");
                            XmlNF.Add("<pICMSInter>" + string.Format("{0:N2}", decimal.Parse("12,00")).Replace(".", "").Replace(",", ".") + "</pICMSInter>");
                            XmlNF.Add("<pICMSInterPart>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["PercDifal"].ToString())).Replace(".", "").Replace(",", ".") + "</pICMSInterPart>");
                            XmlNF.Add("<vFCPUFDest>" + string.Format("{0:N2}", Math.Round((BaseIcms * decimal.Parse("2,00")) / 100, 2, MidpointRounding.AwayFromZero)).Replace(".", "").Replace(",", ".") + "</vFCPUFDest>");
                            XmlNF.Add("<vICMSUFDest>" + string.Format("{0:N2}", IcmsDest).Replace(".", "").Replace(",", ".") + "</vICMSUFDest>");
                            XmlNF.Add("<vICMSUFRemet>" + string.Format("{0:N2}", IcmsReme).Replace(".", "").Replace(",", ".") + "</vICMSUFRemet>");
                            XmlNF.Add("</ICMSUFDest>");
                        }
                    }
                    XmlNF.Add("</imposto>");
                    XmlNF.Add("</det>");
                }
                // Total da Nota
                XmlNF.Add("<total>");
                XmlNF.Add("<ICMSTot>");
                XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["BICMS"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                XmlNF.Add("<vICMS>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRICMS"].ToString())).Replace(".", "").Replace(",", ".") + "</vICMS>");
                XmlNF.Add("<vICMSDeson>0.00</vICMSDeson>");
                                
                if (TotFCPDest > 0)
                    XmlNF.Add("<vFCPUFDest>" + string.Format("{0:N2}", TotFCPDest).Replace(".", "").Replace(",", ".") + "</vFCPUFDest>");
                if (TotIcmsDest > 0)
                    XmlNF.Add("<vICMSUFDest>" + string.Format("{0:N2}", TotIcmsDest).Replace(".", "").Replace(",", ".") + "</vICMSUFDest>");
                if (TotIcmsReme > 0)
                    XmlNF.Add("<vICMSUFRemet>" + string.Format("{0:N2}", TotIcmsReme).Replace(".", "").Replace(",", ".") + "</vICMSUFRemet>");

                XmlNF.Add("<vFCP>0.00</vFCP>");
                XmlNF.Add("<vBCST>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["BICMSSUB"].ToString())).Replace(".", "").Replace(",", ".") + "</vBCST>");
                XmlNF.Add("<vST>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRICMSSUB"].ToString())).Replace(".", "").Replace(",", ".") + "</vST>");
                XmlNF.Add("<vFCPST>0.00</vFCPST>");
                XmlNF.Add("<vFCPSTRet>0.00</vFCPSTRet>");
                
                XmlNF.Add("<vProd>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRPRODUTOS"].ToString())).Replace(".", "").Replace(",", ".") + "</vProd>");
                XmlNF.Add("<vFrete>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRFRETE"].ToString())).Replace(".", "").Replace(",", ".") + "</vFrete>");
                XmlNF.Add("<vSeg>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRSEGURO"].ToString())).Replace(".", "").Replace(",", ".") + "</vSeg>");
                XmlNF.Add("<vDesc>" + string.Format("{0:N2}", TotalDesconto.ToString().Replace(".", "").Replace(",", ".")) + "</vDesc>");
                //XmlNF.Add("<vDesc>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRDESCONTO"].ToString())).Replace(".", "").Replace(",", ".") + "</vDesc>");
                XmlNF.Add("<vII>0.00</vII>");
                XmlNF.Add("<vIPI>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["IPINOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vIPI>");
                if (ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<vIPIDevol>0.00</vIPIDevol>");
                XmlNF.Add("<vPIS>" + string.Format("{0:N2}", TotalPis).Replace(".", "").Replace(",", ".") + "</vPIS>");
                XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", TotalCofins).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                //XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (decimal.Parse(Nota.Tables[0].Rows[0]["VlrProdutos"].ToString()) * decimal.Parse("7,60")) / 100).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                XmlNF.Add("<vOutro>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLROUTRADESP"].ToString())).Replace(".", "").Replace(",", ".") + "</vOutro>");
                XmlNF.Add("<vNF>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRNOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vNF>");
                if (ParamNFE.NFEVersao == 2)
                    XmlNF.Add("<vTotTrib>0.00</vTotTrib>");
                XmlNF.Add("</ICMSTot>");
                XmlNF.Add("</total>");
                //Dados da Transportadora            
                XmlNF.Add("<transp>");
                XmlNF.Add("<modFrete>" + Nota.Tables[0].Rows[0]["Frete"].ToString().Trim() + "</modFrete>"); ;
                if (int.Parse(Nota.Tables[0].Rows[0]["ID_TRANSPORTADORA"].ToString()) > 0)
                {
                    XmlNF.Add("<transporta>");
                    if (Nota.Tables[0].Rows[0]["CNPJTRANSP"].ToString().Trim().Length > 11)
                        XmlNF.Add("<CNPJ>" + Nota.Tables[0].Rows[0]["CNPJTRANSP"].ToString().Trim() + "</CNPJ>");
                    else
                        XmlNF.Add("<CPF>" + Nota.Tables[0].Rows[0]["CNPJTRANSP"].ToString().Trim() + "</CPF>");
                    XmlNF.Add("<xNome>" + Nota.Tables[0].Rows[0]["TRANSPORTADORA"].ToString().Trim() + "</xNome>");
                    XmlNF.Add("<xMun>" + Nota.Tables[0].Rows[0]["CidadeTransp"].ToString().Trim() + "</xMun>");
                    XmlNF.Add("<UF>" + Nota.Tables[0].Rows[0]["UFTransp"].ToString().Trim() + "</UF>");
                    XmlNF.Add("</transporta>");
                }
                if (int.Parse(Nota.Tables[0].Rows[0]["QTDEVOLUME"].ToString()) > 0)
                {
                    XmlNF.Add("<vol>");
                    XmlNF.Add("<qVol>" + Nota.Tables[0].Rows[0]["QTDEVOLUME"].ToString() + "</qVol>");

                    if (Nota.Tables[0].Rows[0]["ESPECIE"].ToString().Trim() != "")
                        XmlNF.Add("<esp>" + Nota.Tables[0].Rows[0]["ESPECIE"].ToString().Trim() + "</esp>");
                    if (Nota.Tables[0].Rows[0]["Marca"].ToString().Trim() != "")
                        XmlNF.Add("<marca>" + Nota.Tables[0].Rows[0]["Marca"].ToString().Trim() + "</marca>");

                    XmlNF.Add("<pesoL>" + string.Format("{0:N3}", decimal.Parse(Nota.Tables[0].Rows[0]["PESOLIQUIDO"].ToString())).Replace(".", "").Replace(",", ".") + "</pesoL>");
                    XmlNF.Add("<pesoB>" + string.Format("{0:N3}", decimal.Parse(Nota.Tables[0].Rows[0]["PESOLIQUIDO"].ToString())).Replace(".", "").Replace(",", ".") + "</pesoB>");
                    XmlNF.Add("</vol>");
                }
                XmlNF.Add("</transp>");

                //Dados do Pagamento
                
                XmlNF.Add("<pag>");
                XmlNF.Add("<detPag>");
                if (int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) == 1)
                {
                    XmlNF.Add("<tPag>99</tPag>");
                    XmlNF.Add("<vPag>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRNOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vPag>");
                }
                else
                {
                    XmlNF.Add("<tPag>90</tPag>");
                    XmlNF.Add("<vPag>0.00</vPag>");
                }
                XmlNF.Add("</detPag>");
                XmlNF.Add("</pag>");
                
                //Dados Adicionais
                XmlNF.Add("<infAdic>");
                if (Nota.Tables[0].Rows[0]["OBSERVACAO"].ToString() != "")
                    XmlNF.Add("<infCpl>" + Nota.Tables[0].Rows[0]["OBSERVACAO"].ToString() + "</infCpl>");
                XmlNF.Add("</infAdic>");
                XmlNF.Add("</infNFe>");
                XmlNF.Add("</NFe>");
                XmlNF.Add("</enviNFe>");
                //XmlNF.Add("</msgDados>");

                

                string Xml = "";

                for (int i = 0; i <= XmlNF.Count - 1; i++)
                    Xml = Xml + XmlNF[i].ToString();

                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();
                
                DateTime Dt = oCertificado.NotAfter.Date;
                if ((Dt - DateTime.Now).Days <= 15)
                    MessageBox.Show("Certificado valido ate: " + Dt.Date.ToShortDateString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AssinarXmlNfe AssinarXml = new AssinarXmlNfe();
                //Console.Write(”URI a ser assinada (Ex.: infCanc, infNFe, infInut, etc.) :”);   
                AssinarXml.Assinar(Xml, "infNFe", oCertificado);

                StreamWriter XmlNota;
                DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeEnviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial));
                if (!VerPath.Exists)
                    VerPath.Create();

                string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeEnviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\nfe" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + ".xml";
                XmlNota = File.CreateText(vArqXML);
                Xml = AssinarXml.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "");
                Xml = Xml.Replace("</NFe>", "").Replace("</enviNFe>", "");
                Xml = Xml + "</NFe></enviNFe>";
                XmlNota.Write(Xml);
                XmlNota.Close();
                return Xml;
            }
            catch (Exception erro)
            {
                 MessageBox.Show("Falha ao Criar o XML erro:"+erro.ToString());
                return "";
            }

            
        }
        public string CancelarNFe(string Chave,string Protocolo,string Justificativa)
        {
            try
            {
                string Xml = "";
                
                Xml = Xml + "<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\">";
                Xml = Xml + "<infEvento Id=\"ID110111" + Chave + "01\">";
                if (CadUF.Sigla == "CE")
                    Xml = Xml + "<cOrgao>23</cOrgao>";
                else
                    Xml = Xml + "<cOrgao>25</cOrgao>";

                if (ParamNFE.NFEAmbiente == 0)
                    Xml = Xml + "<tpAmb>2</tpAmb>";
                else
                    Xml = Xml + "<tpAmb>1</tpAmb>";
                Xml = Xml + "<CNPJ>" + Chave.Substring(6, 14) + "</CNPJ>";
                Xml = Xml + "<chNFe>" + Chave + "</chNFe>";
                Xml = Xml + "<dhEvento>" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhEvento>";
                Xml = Xml + "<tpEvento>110111</tpEvento>";
                Xml = Xml + "<nSeqEvento>1</nSeqEvento>";
                Xml = Xml + "<verEvento>1.00</verEvento>";
                Xml = Xml + "<detEvento versao=\"1.00\">";
                Xml = Xml + "<descEvento>Cancelamento</descEvento>";
                Xml = Xml + "<nProt>" + Protocolo + "</nProt>";
                Xml = Xml + "<xJust>" + Justificativa + "</xJust>";
                Xml = Xml + "</detEvento>";
                Xml = Xml + "</infEvento>";
                Xml = Xml + "</evento>";
                


                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();

                AssinarXmlNfe AssinarXml = new AssinarXmlNfe();                
                AssinarXml.Assinar(Xml, "infEvento", oCertificado);

                Xml = AssinarXml.XMLStringAssinado;
                Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><envEvento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\"><idLote>" + Protocolo + "</idLote>" + Xml + "</envEvento>";

                
                XmlDocument MsgXML = new XmlDocument();
                MsgXML.LoadXml(Xml);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                NFeRecepcaoEvento4.NFeRecepcaoEvento4 oServico = new NFeRecepcaoEvento4.NFeRecepcaoEvento4();
                if (CadUF.Sigla == "CE")
                    oServico.Url = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?WSDL";            
                else
                    oServico.Url = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx?WSDL";
                oServico.ClientCertificates.Add(oCertificado);
                oServico.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                XmlNode retorno = oServico.nfeRecepcaoEvento(MsgXML);
                string vStrXmlRetorno = retorno.OuterXml.ToString();
                
                string nProcCanc = ProtocoloCanc(vStrXmlRetorno);
                if (nProcCanc != "")
                {
                    StreamWriter XmlCancNota;
                    DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeCancelada\\" + string.Format("{0:D3}", ParamNFE.IdFilial));
                    if (!VerPath.Exists)
                        VerPath.Create();

                    string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeCancelada\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\Cancnfe" + Chave + ".xml";
                    XmlCancNota = File.CreateText(vArqXML);
                    XmlCancNota.Write(vStrXmlRetorno);
                    XmlCancNota.Close();
                }
                return nProcCanc;
            }
            catch
            {
                return "";
            }
        }
        public string CartaCorrecao(string Chave, string Protocolo, string correcao)
        {
            try
            {
                string vCabMsg = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><cabecMsg xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.02\"><versaoDados>1.07</versaoDados></cabecMsg>";
                string Xml = "";

                
                if (ParamNFE.NFEVersao == 0)
                    Xml = Xml + "<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.07\">";
                else if (ParamNFE.NFEVersao == 1)
                    Xml = Xml + "<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\">";
                else if (ParamNFE.NFEVersao == 2)
                    Xml = Xml + "<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\">";
                else if (ParamNFE.NFEVersao == 3)
                    Xml = Xml + "<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\">";
                //Xml = Xml + "<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"3.10\">";
                
                Xml = Xml + "<infEvento Id=\"ID110110" + Chave + "01\">";

                if (CadUF.Sigla == "CE")
                    Xml = Xml + "<cOrgao>23</cOrgao>";
                else
                    Xml = Xml + "<cOrgao>25</cOrgao>";

                if (ParamNFE.NFEAmbiente == 0)
                    Xml = Xml + "<tpAmb>2</tpAmb>";
                else
                    Xml = Xml + "<tpAmb>1</tpAmb>";
                Xml = Xml + "<CNPJ>" + Chave.Substring(6, 14) + "</CNPJ>";
                Xml = Xml + "<chNFe>" + Chave + "</chNFe>";
                Xml = Xml + "<dhEvento>" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhEvento>";
                Xml = Xml + "<tpEvento>110110</tpEvento>";
                Xml = Xml + "<nSeqEvento>1</nSeqEvento>";
                Xml = Xml + "<verEvento>1.00</verEvento>";
                Xml = Xml + "<detEvento versao=\"1.00\">";
                Xml = Xml + "<descEvento>Carta de Correcao</descEvento>";
                Xml = Xml + "<xCorrecao>" + correcao + "</xCorrecao>";
                Xml = Xml + "<xCondUso>A Carta de Correção é disciplinada pelo § 1º-A do art. 7º do Convênio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularização de erro ocorrido na emissão de documento fiscal, desde que o erro não esteja relacionado com: I - as variáveis que determinam o valor do imposto tais como: base de cálculo, alíquota, diferença de preço, quantidade, valor da operação ou da prestação; II - a correção de dados cadastrais que implique mudança do remetente ou do destinatário; III - a data de emissão ou de saída.</xCondUso>";              
                Xml = Xml + "</detEvento>";
                Xml = Xml + "</infEvento>";
                Xml = Xml + "</evento>";
                //Xml = Xml + "</envEvento>";

                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();

                AssinarXmlNfe AssinarXml = new AssinarXmlNfe();                
                AssinarXml.Assinar(Xml, "infEvento", oCertificado);

                Xml = AssinarXml.XMLStringAssinado;
                Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><envEvento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\"><idLote>" + Protocolo + "</idLote>" + Xml + "</envEvento>";

                XmlDocument MsgXML = new XmlDocument();
                MsgXML.LoadXml(Xml);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                NFeRecepcaoEvento4.NFeRecepcaoEvento4 oServico = new NFeRecepcaoEvento4.NFeRecepcaoEvento4();
                if (CadUF.Sigla == "CE")
                    oServico.Url = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?WSDL";
                else
                    oServico.Url = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx?WSDL";                
                oServico.ClientCertificates.Add(oCertificado);
                oServico.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                XmlNode retorno = oServico.nfeRecepcaoEvento(MsgXML);
                string vStrXmlRetorno = retorno.OuterXml.ToString();

                string nProcCanc = ProtocoloCanc(vStrXmlRetorno);                
                return nProcCanc;
            }
            catch
            {
                return "";
            }
        }  
        public string DVChaveNF(string Chave)
        {
            int[] multiplicador1 = new int[43] { 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempChave;
            string digito;
            int soma;
            int resto;
            Chave = Chave.Trim();
            Chave = Chave.Replace(".", "").Replace("-", "");
            tempChave = Chave.Substring(0, 43);
            soma = 0;
            for (int i = 0; i < 43; i++)
                soma += int.Parse(tempChave[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            return digito;
        }
        public X509Certificate2 PrepararCertificacao()
        {   
            //string Certificado = "CN=CLEAN SYSTEM COMERCIAL LTDA:05217739000173, OU=AR SERASA, OU=RFB e-CNPJ A3, OU=Secretaria da Receita Federal do Brasil - RFB, O=ICP-Brasil, L=FORTALEZA, S=CE, C=BR";                                  
            string Certificado = "";
            if (ParamNFE.Certificado != null)
                Certificado = ParamNFE.Certificado.Trim();
            //Ajustar o certificado digital de String para o tipo X509Certificate2             
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection  = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, Certificado, false);
            X509Certificate2 oCertificado = null;
            for (int I = 0; I <= collection.Count - 1; I++)
            {
                if (collection[I].Subject==Certificado)
                    oCertificado = new X509Certificate2(collection[I]);            
            }            
            //oCertificado = collection1[0];
            return oCertificado;            
        }
        public X509Certificate2 SelecionarCertificado()
        {            
            X509Certificate2 oX509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                string msgResultado = "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
                MessageBox.Show(msgResultado, "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else
            {
                oX509Cert = scollection[0];
                return oX509Cert;
            }            
        }
        public string EnviarNFE(int IdNota,string XmlNfe)
        {
            //Incluindo a Certificao no Envio;
            X509Certificate2 oCertificado = new X509Certificate2();
            oCertificado = PrepararCertificacao();

            XmlDocument MsgXML = new XmlDocument();
            MsgXML.LoadXml(XmlNfe);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            NFeAutorizacao4.NFeAutorizacao4 oServico = new NFeAutorizacao4.NFeAutorizacao4();
            if (CadUF.Sigla == "CE")
                oServico.Url = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeAutorizacao4?WSDL";
            else
                oServico.Url = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx?WSDL";            
            oServico.ClientCertificates.Add(oCertificado);
            oServico.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            XmlNode retorno = oServico.nfeAutorizacaoLote(MsgXML);
            string vStrXmlRetorno = retorno.OuterXml.ToString();

            nReciboNF = Recibo(vStrXmlRetorno);
            LerMotivoRetNfeXml(vStrXmlRetorno);
            ConsultaRetornoNfe(nReciboNF);
            return nProtocoloNF;
        }
        public string ConsultaProcotoclo(string Cnpj, string Chave)
        {
            try
            {                
                string Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
                Xml = Xml + "<consSitNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">";
                if (ParamNFE.NFEAmbiente == 0)
                    Xml = Xml + "<tpAmb>2</tpAmb><xServ>CONSULTAR</xServ>";
                else
                    Xml = Xml + "<tpAmb>1</tpAmb><xServ>CONSULTAR</xServ>";
                Xml = Xml + "<chNFe>" + Chave + "</chNFe></consSitNFe>";

                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();

                XmlDocument MsgXML = new XmlDocument();
                MsgXML.LoadXml(Xml);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                NFeConsultaProtocolo4.NFeConsultaProtocolo4 oServico = new NFeConsultaProtocolo4.NFeConsultaProtocolo4();
                if (CadUF.Sigla == "CE")
                    oServico.Url = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?WSDL";
                else
                    oServico.Url = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx?WSDL";
                oServico.ClientCertificates.Add(oCertificado);
                oServico.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                XmlNode retorno = oServico.nfeConsultaNF(MsgXML);
                string vStrXmlRetorno = retorno.OuterXml.ToString();

                string nProtocoloNF = Protocolo(vStrXmlRetorno);
                GravarXmlRetorno(vStrXmlRetorno, Chave);
                return nProtocoloNF;
            }
            catch
            {
                return "";
            }

        }
        public string ConsultaRetornoNfe(string Recibo)
        {
            try
            {
              
                string Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
                Xml = Xml + "<consReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">";
                if (ParamNFE.NFEAmbiente == 0)
                    Xml = Xml + "<tpAmb>2</tpAmb>";
                else
                    Xml = Xml + "<tpAmb>1</tpAmb>";
                Xml = Xml + "<nRec>" + Recibo + "</nRec></consReciNFe>";


                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();

                XmlDocument MsgXML = new XmlDocument();
                MsgXML.LoadXml(Xml);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                NFeRetAutorizacao4.NFeRetAutorizacao4 oServico = new NFeRetAutorizacao4.NFeRetAutorizacao4();
                if (CadUF.Sigla == "CE")
                    oServico.Url = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRetAutorizacao4?WSDL";
                else
                    oServico.Url = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx?WSDL";
                oServico.ClientCertificates.Add(oCertificado);
                oServico.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                XmlNode retorno = oServico.nfeRetAutorizacaoLote(MsgXML);
                string vStrXmlRetorno = retorno.OuterXml.ToString();
                                
                LerMotivoRetNfeXml(vStrXmlRetorno);
                vXMLRetorno = vStrXmlRetorno;
                GravarXmlRetorno(vStrXmlRetorno, nChaveNF);
                return nProtocoloNF;
            }
            catch
            {
                return "";
            }

        }
        public void GravarXmlRetorno(string Xml,string NumRec)
        {
            DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeRetorno\\"+string.Format("{0:D3}",ParamNFE.IdFilial));

            if (!VerPath.Exists)
                VerPath.Create();

            StreamWriter XmlNota;
            string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeRetorno\\"+string.Format("{0:D3}",ParamNFE.IdFilial)+"\\Ret" + NumRec + ".xml";
            XmlNota = File.CreateText(vArqXML);            
            XmlNota.Write(Xml);
            XmlNota.Close();
        }
        public void GravarXmlRetornoNFE(int IdNota, string XmlNFe, string XmlRet)
        {           

            DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeProc\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}",int.Parse(DateTime.Now.Date.Month.ToString())));

            if (!VerPath.Exists)
                VerPath.Create();

            XmlDocument xmlNota = new XmlDocument();
            xmlNota.LoadXml(XmlNFe);
            XmlNodeList NFeList = xmlNota.GetElementsByTagName("NFe");
            XmlNode NFeNode = NFeList[0];
            string strNFe = NFeNode.OuterXml;

            XmlDocument xmlRetNota = new XmlDocument();
            xmlRetNota.LoadXml(XmlRet);
            XmlNodeList NFeRetList = xmlRetNota.GetElementsByTagName("protNFe");
            XmlNode NFeRetNode = NFeRetList[0];
            string strNFeRet = NFeRetNode.OuterXml;

            string strXmlProcNfe="";
            if (ParamNFE.NFEVersao == 0)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.10\">" + strNFe + strNFeRet + "</nfeProc>";
            else if (ParamNFE.NFEVersao == 1)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">" + strNFe + strNFeRet + "</nfeProc>";
            else if (ParamNFE.NFEVersao == 2)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"3.10\">" + strNFe + strNFeRet + "</nfeProc>";
            else if (ParamNFE.NFEVersao == 3)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">" + strNFe + strNFeRet + "</nfeProc>";

            StreamWriter ArqXmlNota;
            string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeProc\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())) + "\\Nfe-" + string.Format("{0:D8}", IdNota) + "-procNFE.xml";
            ArqXmlNota = File.CreateText(vArqXML);
            ArqXmlNota.Write(strXmlProcNfe);
            ArqXmlNota.Close();
        }        
        public string Recibo(string strXml)
        {
            cStat = "0";
            vMotivoRet = "";
            string nRec = "";
            int tMed = 0;
            MemoryStream memoryStream = StringXmlToStream(strXml);
            

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(memoryStream);

                XmlNodeList retEnviNFeList = xml.GetElementsByTagName("retEnviNFe");

                foreach (XmlNode retEnviNFeNode in retEnviNFeList)
                {
                    XmlElement retEnviNFeElemento = (XmlElement)retEnviNFeNode;
                    cStat = retEnviNFeElemento.GetElementsByTagName("cStat")[0].InnerText;
                    vMotivoRet = retEnviNFeElemento.GetElementsByTagName("xMotivo")[0].InnerText;
                    XmlNodeList infRecList = xml.GetElementsByTagName("infRec");

                    foreach (XmlNode infRecNode in infRecList)
                    {
                        XmlElement infRecElemento = (XmlElement)infRecNode;
                        nRec = infRecElemento.GetElementsByTagName("nRec")[0].InnerText;
                        tMed = Convert.ToInt32(infRecElemento.GetElementsByTagName("tMed")[0].InnerText);
                        cStat = retEnviNFeElemento.GetElementsByTagName("cStat")[0].InnerText;
                    }
                }
                return nRec;
            }
            catch (Exception ex)
            {
                throw (ex);                
            }
        }
        public string ProtocoloCanc(string strXml)
        {
            MemoryStream memoryStream = StringXmlToStream(strXml);
            cStat = "0";
            string nProt = "";
            string digVal = "";
            vMotivoRet = "";

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(memoryStream);

                XmlNodeList retEnviNFeList = xml.GetElementsByTagName("retEnvEvento");

                foreach (XmlNode retEnviNFeNode in retEnviNFeList)
                {
                    XmlElement retEnviNFeElemento = (XmlElement)retEnviNFeNode;
                    cStat = retEnviNFeElemento.GetElementsByTagName("cStat")[0].InnerText;
                    XmlNodeList infRecList = xml.GetElementsByTagName("retEvento");
                    foreach (XmlNode infRecNode in infRecList)
                    {
                        XmlNodeList infRec = xml.GetElementsByTagName("infEvento");
                        foreach (XmlNode infRecN in infRec)
                        {
                            XmlElement infRecElemento = (XmlElement)infRecN;
                            vMotivoRet = infRecElemento.GetElementsByTagName("xMotivo")[0].InnerText;
                            nProt = infRecElemento.GetElementsByTagName("nProt")[0].InnerText;                            
                        }
                    }
                }
                if (nProt == "")
                {

                }
                return nProt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string Protocolo(string strXml)
        {
            MemoryStream memoryStream = StringXmlToStream(strXml);

            cStat  = "0";
            string nProt  = "";
            string digVal = "";

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(memoryStream);

                XmlNodeList retEnviNFeList = xml.GetElementsByTagName("retConsSitNFe");

                foreach (XmlNode retEnviNFeNode in retEnviNFeList)
                {
                    XmlElement retEnviNFeElemento = (XmlElement)retEnviNFeNode;

                    cStat = retEnviNFeElemento.GetElementsByTagName("cStat")[0].InnerText;                  

                    XmlNodeList infRecList = xml.GetElementsByTagName("infProt");

                    foreach (XmlNode infRecNode in infRecList)
                    {
                        XmlElement infRecElemento = (XmlElement)infRecNode;

                        nProt  = infRecElemento.GetElementsByTagName("nProt")[0].InnerText;
                        //digVal = infRecElemento.GetElementsByTagName("digVal>")[0].InnerText;
                    }
                }
                               
                return nProt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void LerMotivoRetNfeXml(string Xml)
        {
            MemoryStream memoryStream = StringXmlToStream(Xml);
            vMotivoRet = "";
            cStat = "0";
                        
            try
            {                
                XmlDocument xml = new XmlDocument();
                xml.Load(memoryStream);

                XmlNodeList retEnviNFeList = xml.GetElementsByTagName("retConsReciNFe");

                foreach (XmlNode retEnviNFeNode in retEnviNFeList)
                {
                    XmlElement retEnviNFeElemento = (XmlElement)retEnviNFeNode;

                    cStat = retEnviNFeElemento.GetElementsByTagName("cStat")[0].InnerText;

                    XmlNodeList infRecList = xml.GetElementsByTagName("infProt");

                    foreach (XmlNode infRecNode in infRecList)
                    {
                        XmlElement infRecElemento = (XmlElement)infRecNode;
                        vMotivoRet    = infRecElemento.GetElementsByTagName("xMotivo")[0].InnerText;
                        cStat         = infRecElemento.GetElementsByTagName("cStat")[0].InnerText;
                        if (vMotivoRet == "Autorizado o uso da NF-e")
                        {

                            if (infRecElemento.GetElementsByTagName("nProt")[0].InnerText != null)
                                nProtocoloNF = infRecElemento.GetElementsByTagName("nProt")[0].InnerText;
                        }
                        else
                            nProtocoloNF = "";
                    }
                }                
            }
            catch (Exception ex)
            {
                vXMLRetorno  = "";
                nProtocoloNF = "";
                throw (ex); 
            }
        }
        public static MemoryStream StringXmlToStream(string strXml)
        {
            byte[] byteArray = new byte[strXml.Length];
            System.Text.ASCIIEncoding encoding = new
            System.Text.ASCIIEncoding();
            byteArray = encoding.GetBytes(strXml);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
        public string BuscaXMLNFe(string Recibo)
        {
            try
            {
                string Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
                Xml = Xml + "<distDFeInt xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.01\">";
                if (ParamNFE.NFEAmbiente == 0)
                    Xml = Xml + "<tpAmb>2</tpAmb>";
                else
                    Xml = Xml + "<tpAmb>1</tpAmb>";
                Xml = Xml + "<cUFAutor>23</cUFAutor>";
                Xml = Xml + "<CNPJ>11831930000103</CNPJ>";
                Xml = Xml + "<consNSU>";
                Xml = Xml + "<NSU>000000000060643</NSU>";
                Xml = Xml + "</consNSU>";
                /* Xml = Xml + "<consChNFe>";
                 Xml = Xml + "<chNFe>26190811071732000270550000000035041989100439</chNFe>";
                 Xml = Xml + "</consChNFe>"; 000000000060643*/
                Xml = Xml + "</distDFeInt>";
                
                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();

                XmlDocument MsgXML = new XmlDocument();
                MsgXML.LoadXml(Xml);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                NFeDistribuicaoDFe.NFeDistribuicaoDFe oServico = new NFeDistribuicaoDFe.NFeDistribuicaoDFe();
                oServico.Url = "	https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx?WSDL";                
                oServico.ClientCertificates.Add(oCertificado);
                oServico.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                XmlNode retorno = oServico.nfeDistDFeInteresse(MsgXML);
                string vStrXmlRetorno = retorno.OuterXml.ToString();

                LerMotivoRetNfeXml(vStrXmlRetorno);
                vXMLRetorno = vStrXmlRetorno;
                GravarXmlRetorno(vStrXmlRetorno, nChaveNF);
                return nProtocoloNF;
            }
            catch
            {
                return "";
            }

        }

        public string GerarMDF(int IdNota, Filiais Filial, int TpEmissao)
        {
            /// TpEmissao 1-Normal 2-Contigência Form Seguraca 3-Contigência SCAN 4-Contigencia DPEC 5-Contigencia FS-DA Form Segurança DANFE         

            try
            {
                Verificar BuscaAux = new Verificar();
                BuscaAux.Controle = Controle;
                //
                /*Estados CadUF = new Estados();
                CadUF.Controle = Controle;
                CadUF.LerDados(Filial.Uf);*/

                ArrayList XmlNF = new ArrayList();
                DataSet Nota = new DataSet();
                string sSQL = "SELECT T2.ENTSAIDA,T2.RAZAOSOCIAL,T2.CNPJCPF,T2.INSC_UF,T2.DTEMISSAO,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO," +
                              "       T2.TELEFONE,T2.CEP,T2.BAIRRO,T2.CIDADE,T4.SIGLA,T4.CODIBGE,T3.CODBARRA,T3.REFERENCIA,T3.DESCRICAO,T3.UNIDADE,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T1.PICMS,T1.VLRICMS AS ICMSITEM,T1.PERCRED,T1.PIPI,T1.VlrIpi as VlrIpiItem," +
                              "       T2.BICMS,T2.VLRICMS,T2.BICMSSUB,T2.VLRICMSSUB,T2.VLRFRETE,T2.VLRIPI AS IPINOTA,T2.VLRSEGURO,T2.VLROUTRADESP,T2.VLRFRETE,T2.VLRPRODUTOS,T2.VLRNOTA,T2.VLRDESCONTO,T2.FRETE," +
                              "       T2.QTDEVOLUME,T2.ESPECIE,T2.MARCA,T2.PESOBRUTO,T2.PESOLIQUIDO,T2.NUMFORMULARIO,T2.NUMNOTA,T2.OBSERVACAO,T5.CFOP,T5.NATUREZA,T7.VENDEDOR,ISNULL(T8.PRIMPARCELA,0) AS PRIMPARCELA,T3.NCM,T2.CONSUMIDOR,T2.ATENDIMENTO,T2.DESTOPERACAO," +
                              "       T2.FINALIDADE,T2.ChaveNfeDev,ISNULL(T2.NATOP,0) AS NATOP, IsNull(T1.CST,0) as CST_ICMS,T2.CODMUN,T2.NUMPEDIDO,ISNULL(T1.ITEMPED,0) AS ITEMPED,Isnull(T2.ICMSInterno,0) as ICMSInterno, Isnull(T2.PercDifal,0) as PercDifal, " +
                              "       T2.ID_TRANSPORTADORA,T9.RazaoSocial AS TRANSPORTADORA, T9.CNPJ AS CNPJTRANSP,T9.Cidade AS CidadeTransp,ET.Sigla as UFTransp, T1.CODPRDCLIENTE" +
                              " FROM NOTAFISCALITENS T1" +
                              "   LEFT JOIN NOTAFISCAL T2 ON (T2.ID_NOTA=T1.ID_NOTA)" +
                              "   LEFT JOIN PRODUTOS   T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              "   LEFT JOIN ESTADOS    T4 ON (T4.ID_UF=T2.ID_UF)" +
                              "   LEFT JOIN CFOP       T5 ON (T5.ID_CFOP=T1.ID_CFOP)" +
                              "   LEFT OUTER JOIN MVVENDA AS T6 ON T6.ID_VENDA=T2.ID_VENDA" +
                              "   LEFT OUTER JOIN VENDEDORES AS T7 ON T7.ID_VENDEDOR=T6.ID_VENDEDOR" +
                              "   LEFT OUTER JOIN FORMAPAGAMENTO AS T8 ON T8.ID_FORMAPGTO=T6.ID_FORMAPGTO" +
                              "   LEFT OUTER JOIN Transportadoras T9 ON (T9.Id_Transportadora=T2.Id_Transportadora)" +
                              "   LEFT JOIN ESTADOS    ET ON (ET.ID_UF=T9.ID_UF)" +
                              " WHERE T1.ID_NOTA=" + IdNota.ToString();

                Nota = Controle.ConsultaTabela(sSQL);
                string ChaveNF = "";
                if (CadUF.Sigla == "CE")
                    ChaveNF = "23" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + Filial.Cnpj.Trim() + "58001" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "1" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString()));
                else
                    ChaveNF = "25" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + Filial.Cnpj.Trim() + "58001" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "1" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString()));

                nChaveNF = ChaveNF + DVChaveNF(ChaveNF);

                XmlNF.Add("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");


                if (ParamNFE.NFEVersao == 0)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.10\">");
                else if (ParamNFE.NFEVersao == 1)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">");
                else if (ParamNFE.NFEVersao == 2)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"3.10\">");
                else if (ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">");

                
                XmlNF.Add("<idLote>" + string.Format("{0:D12}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "</idLote>");
                
                XmlNF.Add("<NFe>");

                if (ParamNFE.NFEVersao == 0)
                    XmlNF.Add("<infNFe xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Id=\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"1.10\">");
                else if (ParamNFE.NFEVersao == 1)
                    XmlNF.Add("<infNFe xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Id=\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"2.00\">");
                else if (ParamNFE.NFEVersao == 2)
                    XmlNF.Add("<infNFe xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Id=\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"3.10\">");
                else if (ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<infNFe Id =\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"4.00\">");

                //Dados da Nota
                XmlNF.Add("<ide>");
                if (CadUF.Sigla == "CE")
                    XmlNF.Add("<cUF>23</cUF>");
                else
                    XmlNF.Add("<cUF>25</cUF>");
                XmlNF.Add("<cNF>" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString())) + "</cNF>");
                XmlNF.Add("<natOp>" + NaturezaOpNFE(int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString())) + "</natOp>");

                if (ParamNFE.NFEVersao == 2)
                {
                    if (int.Parse(Nota.Tables[0].Rows[0]["PRIMPARCELA"].ToString()) > 0)
                        XmlNF.Add("<indPag>1</indPag>");
                    else
                        XmlNF.Add("<indPag>0</indPag>");
                }

                XmlNF.Add("<mod>55</mod>");
                XmlNF.Add("<serie>1</serie>");
                XmlNF.Add("<nNF>" + Nota.Tables[0].Rows[0]["NUMNOTA"].ToString() + "</nNF>");
                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                {
                    XmlNF.Add("<dhEmi>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhEmi>");
                    XmlNF.Add("<dhSaiEnt>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhSaiEnt>");
                }
                else
                {
                    XmlNF.Add("<dEmi>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-dd") + "</dEmi>");
                    XmlNF.Add("<dSaiEnt>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-dd") + "</dSaiEnt>");
                }
                if (Nota.Tables[0].Rows[0]["EntSaida"].ToString() == "0")
                    XmlNF.Add("<tpNF>1</tpNF>");
                else
                    XmlNF.Add("<tpNF>0</tpNF>");

                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<idDest>" + (int.Parse(Nota.Tables[0].Rows[0]["DESTOPERACAO"].ToString()) + 1).ToString() + "</idDest>");

                //XmlNF.Add("<cMunFG>2507507</cMunFG>");
                XmlNF.Add("<cMunFG>" + CadUF.CodIBGE.ToString().Trim() + "</cMunFG>");
                XmlNF.Add("<tpImp>1</tpImp>");
                XmlNF.Add("<tpEmis>" + TpEmissao.ToString() + "</tpEmis>");
                XmlNF.Add("<cDV>" + DVChaveNF(ChaveNF) + "</cDV>");

                if (ParamNFE.NFEAmbiente == 0)
                    XmlNF.Add("<tpAmb>2</tpAmb>");
                else
                    XmlNF.Add("<tpAmb>1</tpAmb>");
                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<finNFe>" + (int.Parse(Nota.Tables[0].Rows[0]["FINALIDADE"].ToString()) + 1).ToString() + "</finNFe>");
                else
                    XmlNF.Add("<finNFe>1</finNFe>");

                XmlNF.Add("<indFinal>" + Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString() + "</indFinal>");

                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                {
                    if (int.Parse(Nota.Tables[0].Rows[0]["ATENDIMENTO"].ToString()) == 5)
                        XmlNF.Add("<indPres>9</indPres>");
                    else
                        XmlNF.Add("<indPres>" + Nota.Tables[0].Rows[0]["ATENDIMENTO"].ToString() + "</indPres>");
                }
                XmlNF.Add("<procEmi>0</procEmi>");
                XmlNF.Add("<verProc>ERP-SGE 1.0</verProc>");
                if (int.Parse(Nota.Tables[0].Rows[0]["FINALIDADE"].ToString()) == 3)
                {
                    XmlNF.Add("<NFref>");
                    XmlNF.Add("<refNFe>" + Nota.Tables[0].Rows[0]["ChaveNfeDev"].ToString().Trim() + "</refNFe>");
                    XmlNF.Add("</NFref>");
                }
                XmlNF.Add("</ide>");


                //Dados do Emitente
                XmlNF.Add("<emit>");
                XmlNF.Add("<CNPJ>" + Filial.Cnpj.Trim() + "</CNPJ>");
                XmlNF.Add("<xNome>" + Filial.Filial.Trim().Replace("-", "") + "</xNome>");
                XmlNF.Add("<xFant>" + Filial.Fantasia.Trim() + "</xFant>");
                XmlNF.Add("<enderEmit>");
                XmlNF.Add("<xLgr>" + Filial.Endereco.Trim() + "</xLgr> ");
                XmlNF.Add("<nro>" + Filial.Numero.Trim() + "</nro>"); ;
                XmlNF.Add("<xCpl>" + Filial.Complemento.Trim() + "</xCpl>");
                XmlNF.Add("<xBairro>" + Filial.Bairro.Trim() + "</xBairro>");
                //XmlNF.Add("<cMun>"+CadUF.CodIBGE.ToString().Trim()+"</cMun>");
                XmlNF.Add("<cMun>" + Filial.CodMun.ToString().Trim() + "</cMun>");
                XmlNF.Add("<xMun>" + Filial.Cidade.Trim() + "</xMun>");
                XmlNF.Add("<UF>" + BuscaAux.Busca_SiglaUF(Filial.Uf) + "</UF>");
                XmlNF.Add("<CEP>" + Filial.Cep.Trim() + "</CEP>");
                XmlNF.Add("<cPais>1058</cPais>");
                XmlNF.Add("<xPais>Brasil</xPais>");
                XmlNF.Add("<fone>" + Filial.Fone1.Trim() + "</fone>");
                XmlNF.Add("</enderEmit>");
                XmlNF.Add("<IE>" + Filial.InscUF.Replace(".", "").Replace("-", "").Replace("/", "").Replace(",", "").Trim() + "</IE>");
                XmlNF.Add("<CRT>" + (Filial.Regime + 1).ToString() + "</CRT>");
                XmlNF.Add("</emit>");

                //Dados Destinatario
                XmlNF.Add("<dest>");
                if (Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim().Length > 11)
                    XmlNF.Add("<CNPJ>" + Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim() + "</CNPJ>");
                else
                    XmlNF.Add("<CPF>" + Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim() + "</CPF>");
                XmlNF.Add("<xNome>" + Nota.Tables[0].Rows[0]["RAZAOSOCIAL"].ToString().Replace("Ç", "C").Trim() + "</xNome>");
                XmlNF.Add("<enderDest>");
                XmlNF.Add("<xLgr>" + Nota.Tables[0].Rows[0]["ENDERECO"].ToString().Trim() + "</xLgr>");
                XmlNF.Add("<nro>" + Nota.Tables[0].Rows[0]["NUMERO"].ToString().Trim() + "</nro>");

                if (Nota.Tables[0].Rows[0]["COMPLEMENTO"].ToString().Trim() != "")
                    XmlNF.Add("<xCpl>" + Nota.Tables[0].Rows[0]["COMPLEMENTO"].ToString().Trim() + "</xCpl>");

                XmlNF.Add("<xBairro>" + Nota.Tables[0].Rows[0]["BAIRRO"].ToString().Trim() + "</xBairro>");
                XmlNF.Add("<cMun>" + Nota.Tables[0].Rows[0]["CODMUN"].ToString().Trim() + "</cMun>");
                XmlNF.Add("<xMun>" + Nota.Tables[0].Rows[0]["CIDADE"].ToString().Trim() + "</xMun>");
                XmlNF.Add("<UF>" + Nota.Tables[0].Rows[0]["SIGLA"].ToString().Trim() + "</UF>");
                XmlNF.Add("<CEP>" + Nota.Tables[0].Rows[0]["CEP"].ToString().Trim() + "</CEP>");
                XmlNF.Add("<cPais>1058</cPais>");
                XmlNF.Add("<xPais>BRASIL</xPais>");
                XmlNF.Add("<fone>" + Nota.Tables[0].Rows[0]["TELEFONE"].ToString().Trim() + "</fone>");
                XmlNF.Add("</enderDest>");

                if (ParamNFE.NFEVersao == 2 || ParamNFE.NFEVersao == 3)
                {
                    if (Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim().Replace(",", "").Trim() == "")
                        XmlNF.Add("<indIEDest>9</indIEDest>");
                    else
                    {
                        XmlNF.Add("<indIEDest>1</indIEDest>");
                        XmlNF.Add("<IE>" + Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim().Replace(",", "").Trim() + "</IE>");
                    }
                }
                else
                    XmlNF.Add("<IE>" + Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim().Replace(",", "").Trim() + "</IE>");
                XmlNF.Add("</dest>");

                // Itens da Nota
                decimal TotalPis = 0;
                decimal TotalCofins = 0;
                decimal TotalDesconto = 0;
                decimal TotIcmsDest = 0;
                decimal TotIcmsReme = 0;
                decimal TotFCPDest = 0;

                for (int I = 0; I <= Nota.Tables[0].Rows.Count - 1; I++)
                {
                    XmlNF.Add("<det nItem=\"" + (I + 1).ToString() + "\">");
                    XmlNF.Add("<prod>");
                    XmlNF.Add("<cProd>" + Nota.Tables[0].Rows[I]["Referencia"].ToString().Trim() + "</cProd>");
                    if (Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() != "")
                        XmlNF.Add("<cEAN>" + Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() + "</cEAN>");
                    else
                        XmlNF.Add("<cEAN></cEAN>");

                    if (int.Parse(Nota.Tables[0].Rows[I]["ItemPed"].ToString()) > 0)
                        XmlNF.Add("<xProd>" + Nota.Tables[0].Rows[I]["Descricao"].ToString().Trim() + " cProdCliente:\"" + Nota.Tables[0].Rows[I]["CodPrdCliente"].ToString().Trim() + "\" OC:" + Nota.Tables[0].Rows[I]["ItemPed"].ToString().Trim() + "</xProd>");
                    else
                        XmlNF.Add("<xProd>" + Nota.Tables[0].Rows[I]["Descricao"].ToString().Trim() + "</xProd>");

                    if (Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() != "")
                        XmlNF.Add("<NCM>" + Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() + "</NCM>");
                    else
                        XmlNF.Add("<NCM>38089919</NCM>");
                    XmlNF.Add("<CEST>1300402</CEST>");
                    XmlNF.Add("<CFOP>" + Nota.Tables[0].Rows[I]["CFOP"].ToString().Replace(".", "").Trim() + "</CFOP>");
                    XmlNF.Add("<uCom>" + Nota.Tables[0].Rows[I]["Unidade"].ToString().Trim() + "</uCom>");
                    XmlNF.Add("<qCom>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["Qtde"].ToString())).Replace(".", "").Replace(",", ".") + "</qCom>");
                    XmlNF.Add("<vUnCom>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrUnitario"].ToString())).Replace(".", "").Replace(",", ".") + "</vUnCom>");
                    XmlNF.Add("<vProd>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vProd>");
                    if (Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() != "")
                        XmlNF.Add("<cEANTrib>" + Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() + "</cEANTrib>");
                    else
                        XmlNF.Add("<cEANTrib></cEANTrib>");
                    XmlNF.Add("<uTrib>" + Nota.Tables[0].Rows[I]["Unidade"].ToString().Trim() + "</uTrib>");
                    XmlNF.Add("<qTrib>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["Qtde"].ToString())).Replace(".", "").Replace(",", ".") + "</qTrib>");
                    XmlNF.Add("<vUnTrib>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrUnitario"].ToString())).Replace(".", "").Replace(",", ".") + "</vUnTrib>");

                    if (decimal.Parse(Nota.Tables[0].Rows[0]["VLRFRETE"].ToString()) > 0)
                        XmlNF.Add("<vFrete>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRFRETE"].ToString())).Replace(".", "").Replace(",", ".") + "</vFrete>");

                    if (decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString()) > 0)
                    {
                        decimal VlrDesconto = Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * (100 / (decimal.Parse(Nota.Tables[0].Rows[I]["VLRPRODUTOS"].ToString()) / decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString())) / 100), 2);
                        TotalDesconto = TotalDesconto + VlrDesconto;
                        if (string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * (100 / (decimal.Parse(Nota.Tables[0].Rows[I]["VLRPRODUTOS"].ToString()) / decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString())) / 100)).Replace(".", "").Replace(",", ".") != "0.00")
                            XmlNF.Add("<vDesc>" + string.Format("{0:N2}", ((VlrDesconto).ToString()).Replace(".", "").Replace(",", ".")) + "</vDesc>");
                    }

                    XmlNF.Add("<indTot>1</indTot>");
                    if (int.Parse(Nota.Tables[0].Rows[I]["ItemPed"].ToString()) > 0)
                    {
                        XmlNF.Add("<xPed>" + Nota.Tables[0].Rows[I]["NumPedido"].ToString().Trim() + "</xPed>");
                        XmlNF.Add("<nItemPed>" + Nota.Tables[0].Rows[I]["ItemPed"].ToString().Trim() + "</nItemPed>");
                    }
                    XmlNF.Add("</prod>");
                    XmlNF.Add("<imposto>");
                    //ICMS                
                    if (decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString()) > 0)
                    {
                        XmlNF.Add("<ICMS>");
                        //XmlNF.Add("<ICMS00>");
                        XmlNF.Add("<ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                        XmlNF.Add("<orig>0</orig>");
                        XmlNF.Add("<CST>" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + "</CST>");
                        XmlNF.Add("<modBC>0</modBC>");
                        if (decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString()) > 0)
                        {
                            if (decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) > 0)
                            {
                                XmlNF.Add("<pRedBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString())).Replace(".", "").Replace(",", ".") + "</pRedBC>");
                                XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) - Math.Round((decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) / 100), 2)).Replace(".", "").Replace(",", ".") + "</vBC>");
                            }
                            else
                                XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                        }
                        else
                            XmlNF.Add("<vBC>0.00</vBC>");
                        XmlNF.Add("<pICMS>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString())).Replace(".", "").Replace(",", ".") + "</pICMS>");
                        XmlNF.Add("<vICMS>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["IcmsItem"].ToString())).Replace(".", "").Replace(",", ".") + "</vICMS>");
                        //XmlNF.Add("</ICMS00>");
                        XmlNF.Add("</ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                        XmlNF.Add("</ICMS>");
                    }
                    else
                    {
                        if (Filial.Regime != 2)
                        {
                            XmlNF.Add("<ICMS>");
                            XmlNF.Add("<ICMSSN102>");
                            XmlNF.Add("<orig>0</orig>");
                            XmlNF.Add("<CSOSN>102</CSOSN>");
                            /*if (int.Parse(Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString()) == 1)
                            {
                                XmlNF.Add("<pRedBCEfet>0.00</pRedBCEfet>");
                                XmlNF.Add("<vBCEfet>0.00</vBCEfet>");
                                XmlNF.Add("<pICMSEfet>0.00</pICMSEfet>");
                                XmlNF.Add("<vICMSEfet>0.00</vICMSEfet>");
                            }
                            else
                            {
                                XmlNF.Add("<vBCSTRet>0.00</vBCSTRet>");
                                XmlNF.Add("<pST>0.00</pST>");
                                XmlNF.Add("<vICMSSubstituto>0.00</vICMSSubstituto>");
                                XmlNF.Add("<vICMSSTRet>0.00</vICMSSTRet>");
                            }*/
                            XmlNF.Add("</ICMSSN102>");
                            XmlNF.Add("</ICMS>");
                        }
                        else
                        {
                            XmlNF.Add("<ICMS>");
                            //XmlNF.Add("<ICMS40>");
                            XmlNF.Add("<ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                            XmlNF.Add("<orig>0</orig>");
                            //XmlNF.Add("<CST>41</CST>");
                            XmlNF.Add("<CST>" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + "</CST>");
                            //XmlNF.Add("</ICMS40>");
                            /* if (int.Parse(Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString()) == 1)
                             {
                                 //XmlNF.Add("<pRedBCEfet>0.00</pRedBCEfet>");
                                 XmlNF.Add("<vBCEfet>0.00</vBCEfet>");
                                 XmlNF.Add("<pICMSEfet>0.00</pICMSEfet>");
                                 XmlNF.Add("<vICMSEfet>0.00</vICMSEfet>");
                             }
                             else
                             {
                                 XmlNF.Add("<vBCSTRet>0.00</vBCSTRet>");
                                 XmlNF.Add("<pST>0.00</pST>");
                                 XmlNF.Add("<vICMSSubstituto>0.00</vICMSSubstituto>");
                                 XmlNF.Add("<vICMSSTRet>0.00</vICMSSTRet>");
                             }*/
                            XmlNF.Add("</ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                            XmlNF.Add("</ICMS>");
                        }
                    }
                    //IPI
                    XmlNF.Add("<IPI>");
                    if (decimal.Parse(Nota.Tables[0].Rows[I]["Pipi"].ToString()) > 0)
                    {
                        XmlNF.Add("<cEnq>999</cEnq>");
                        XmlNF.Add("<IPITrib>");
                        XmlNF.Add("<CST>50</CST>");
                        XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                        XmlNF.Add("<pIPI>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["Pipi"].ToString())).Replace(".", "").Replace(",", ".") + "</pIPI>");
                        XmlNF.Add("<vIPI>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrIpiItem"].ToString())).Replace(".", "").Replace(",", ".") + "</vIPI>");
                        XmlNF.Add("</IPITrib>");
                    }
                    else
                    {
                        XmlNF.Add("<cEnq>999</cEnq>");
                        XmlNF.Add("<IPINT>");
                        XmlNF.Add("<CST>53</CST>");
                        XmlNF.Add("</IPINT>");
                    }
                    XmlNF.Add("</IPI>");
                    //PIS
                    XmlNF.Add("<PIS>");
                    XmlNF.Add("<PISAliq>");
                    XmlNF.Add("<CST>01</CST>");
                    XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                    //XmlNF.Add("<pPIS>1.65</pPIS>");
                    XmlNF.Add("<pPIS>" + string.Format("{0:N2}", ParamNFE.PercPIS).Replace(".", "").Replace(",", ".") + "</pPIS>");
                    XmlNF.Add("<vPIS>" + string.Format("{0:N2}", (Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercPIS) / 100), 2).Replace(".", "").Replace(",", ".") + "</vPIS>");
                    XmlNF.Add("</PISAliq>");
                    XmlNF.Add("</PIS>");
                    //TotalPis = TotalPis + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("1,65") / 100, 2);
                    TotalPis = TotalPis + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercPIS / 100, 2);
                    //COFINS
                    XmlNF.Add("<COFINS>");
                    XmlNF.Add("<COFINSAliq>");
                    XmlNF.Add("<CST>01</CST>");
                    XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                    //XmlNF.Add("<pCOFINS>7.60</pCOFINS>");
                    XmlNF.Add("<pCOFINS>" + string.Format("{0:N2}", ParamNFE.PercCOFINS).Replace(".", "").Replace(",", ".") + "</pCOFINS>");
                    XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercCOFINS) / 100), 2).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                    //XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("7,60")) / 100).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                    XmlNF.Add("</COFINSAliq>");
                    XmlNF.Add("</COFINS>");
                    //TotalCofins = TotalCofins + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("7,60") / 100, 2);
                    TotalCofins = TotalCofins + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * ParamNFE.PercCOFINS / 100, 2);

                    //Grupo de Tributação do ICMS para UF de Destino
                    if ((int.Parse(Nota.Tables[0].Rows[0]["DESTOPERACAO"].ToString()) + 1) == 2 && int.Parse(Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString()) == 1 && Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim() == "" && Nota.Tables[0].Rows[0]["EntSaida"].ToString() == "0")
                    {
                        decimal BaseIcms = 0;
                        if (decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) > 0)
                            BaseIcms = decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) - Math.Round((decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) / 100), 2);
                        else
                            BaseIcms = decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString());
                        //if (decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString()) > 0 && (int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 5 && int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 7))
                        if ((int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 5 && int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) != 7))
                        {

                            decimal DifIcms = decimal.Parse(Nota.Tables[0].Rows[I]["ICMSInterno"].ToString()) - decimal.Parse("12,00");
                            decimal VlrDif = Math.Round((BaseIcms * DifIcms) / 100, 2);
                            decimal IcmsDest = Math.Round((VlrDif * decimal.Parse(Nota.Tables[0].Rows[I]["PercDifal"].ToString())) / 100, 2);
                            decimal IcmsReme = Math.Round((VlrDif * (100 - decimal.Parse(Nota.Tables[0].Rows[I]["PercDifal"].ToString()))) / 100, 2);

                            TotIcmsDest = TotIcmsDest + IcmsDest;
                            TotIcmsReme = TotIcmsReme + IcmsReme;
                            TotFCPDest = TotFCPDest + Math.Round((BaseIcms * decimal.Parse("2,00")) / 100, 2, MidpointRounding.AwayFromZero);

                            XmlNF.Add("<ICMSUFDest>");
                            XmlNF.Add("<vBCUFDest>" + string.Format("{0:N2}", BaseIcms).Replace(".", "").Replace(",", ".") + "</vBCUFDest>");
                            XmlNF.Add("<vBCFCPUFDest>" + string.Format("{0:N2}", BaseIcms).Replace(".", "").Replace(",", ".") + "</vBCFCPUFDest>");
                            XmlNF.Add("<pFCPUFDest>2.00</pFCPUFDest>");
                            XmlNF.Add("<pICMSUFDest>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["ICMSInterno"].ToString())).Replace(".", "").Replace(",", ".") + "</pICMSUFDest>");
                            XmlNF.Add("<pICMSInter>" + string.Format("{0:N2}", decimal.Parse("12,00")).Replace(".", "").Replace(",", ".") + "</pICMSInter>");
                            XmlNF.Add("<pICMSInterPart>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["PercDifal"].ToString())).Replace(".", "").Replace(",", ".") + "</pICMSInterPart>");
                            XmlNF.Add("<vFCPUFDest>" + string.Format("{0:N2}", Math.Round((BaseIcms * decimal.Parse("2,00")) / 100, 2, MidpointRounding.AwayFromZero)).Replace(".", "").Replace(",", ".") + "</vFCPUFDest>");
                            XmlNF.Add("<vICMSUFDest>" + string.Format("{0:N2}", IcmsDest).Replace(".", "").Replace(",", ".") + "</vICMSUFDest>");
                            XmlNF.Add("<vICMSUFRemet>" + string.Format("{0:N2}", IcmsReme).Replace(".", "").Replace(",", ".") + "</vICMSUFRemet>");
                            XmlNF.Add("</ICMSUFDest>");
                        }
                    }
                    XmlNF.Add("</imposto>");
                    XmlNF.Add("</det>");
                }
                // Total da Nota
                XmlNF.Add("<total>");
                XmlNF.Add("<ICMSTot>");
                XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["BICMS"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                XmlNF.Add("<vICMS>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRICMS"].ToString())).Replace(".", "").Replace(",", ".") + "</vICMS>");
                XmlNF.Add("<vICMSDeson>0.00</vICMSDeson>");

                if (TotFCPDest > 0)
                    XmlNF.Add("<vFCPUFDest>" + string.Format("{0:N2}", TotFCPDest).Replace(".", "").Replace(",", ".") + "</vFCPUFDest>");
                if (TotIcmsDest > 0)
                    XmlNF.Add("<vICMSUFDest>" + string.Format("{0:N2}", TotIcmsDest).Replace(".", "").Replace(",", ".") + "</vICMSUFDest>");
                if (TotIcmsReme > 0)
                    XmlNF.Add("<vICMSUFRemet>" + string.Format("{0:N2}", TotIcmsReme).Replace(".", "").Replace(",", ".") + "</vICMSUFRemet>");

                XmlNF.Add("<vFCP>0.00</vFCP>");
                XmlNF.Add("<vBCST>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["BICMSSUB"].ToString())).Replace(".", "").Replace(",", ".") + "</vBCST>");
                XmlNF.Add("<vST>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRICMSSUB"].ToString())).Replace(".", "").Replace(",", ".") + "</vST>");
                XmlNF.Add("<vFCPST>0.00</vFCPST>");
                XmlNF.Add("<vFCPSTRet>0.00</vFCPSTRet>");

                XmlNF.Add("<vProd>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRPRODUTOS"].ToString())).Replace(".", "").Replace(",", ".") + "</vProd>");
                XmlNF.Add("<vFrete>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRFRETE"].ToString())).Replace(".", "").Replace(",", ".") + "</vFrete>");
                XmlNF.Add("<vSeg>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRSEGURO"].ToString())).Replace(".", "").Replace(",", ".") + "</vSeg>");
                XmlNF.Add("<vDesc>" + string.Format("{0:N2}", TotalDesconto.ToString().Replace(".", "").Replace(",", ".")) + "</vDesc>");
                //XmlNF.Add("<vDesc>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRDESCONTO"].ToString())).Replace(".", "").Replace(",", ".") + "</vDesc>");
                XmlNF.Add("<vII>0.00</vII>");
                XmlNF.Add("<vIPI>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["IPINOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vIPI>");
                if (ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<vIPIDevol>0.00</vIPIDevol>");
                XmlNF.Add("<vPIS>" + string.Format("{0:N2}", TotalPis).Replace(".", "").Replace(",", ".") + "</vPIS>");
                XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", TotalCofins).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                //XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (decimal.Parse(Nota.Tables[0].Rows[0]["VlrProdutos"].ToString()) * decimal.Parse("7,60")) / 100).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                XmlNF.Add("<vOutro>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLROUTRADESP"].ToString())).Replace(".", "").Replace(",", ".") + "</vOutro>");
                XmlNF.Add("<vNF>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRNOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vNF>");
                if (ParamNFE.NFEVersao == 2)
                    XmlNF.Add("<vTotTrib>0.00</vTotTrib>");
                XmlNF.Add("</ICMSTot>");
                XmlNF.Add("</total>");
                //Dados da Transportadora            
                XmlNF.Add("<transp>");
                XmlNF.Add("<modFrete>" + Nota.Tables[0].Rows[0]["Frete"].ToString().Trim() + "</modFrete>"); ;
                if (int.Parse(Nota.Tables[0].Rows[0]["ID_TRANSPORTADORA"].ToString()) > 0)
                {
                    XmlNF.Add("<transporta>");
                    if (Nota.Tables[0].Rows[0]["CNPJTRANSP"].ToString().Trim().Length > 11)
                        XmlNF.Add("<CNPJ>" + Nota.Tables[0].Rows[0]["CNPJTRANSP"].ToString().Trim() + "</CNPJ>");
                    else
                        XmlNF.Add("<CPF>" + Nota.Tables[0].Rows[0]["CNPJTRANSP"].ToString().Trim() + "</CPF>");
                    XmlNF.Add("<xNome>" + Nota.Tables[0].Rows[0]["TRANSPORTADORA"].ToString().Trim() + "</xNome>");
                    XmlNF.Add("<xMun>" + Nota.Tables[0].Rows[0]["CidadeTransp"].ToString().Trim() + "</xMun>");
                    XmlNF.Add("<UF>" + Nota.Tables[0].Rows[0]["UFTransp"].ToString().Trim() + "</UF>");
                    XmlNF.Add("</transporta>");
                }
                if (int.Parse(Nota.Tables[0].Rows[0]["QTDEVOLUME"].ToString()) > 0)
                {
                    XmlNF.Add("<vol>");
                    XmlNF.Add("<qVol>" + Nota.Tables[0].Rows[0]["QTDEVOLUME"].ToString() + "</qVol>");

                    if (Nota.Tables[0].Rows[0]["ESPECIE"].ToString().Trim() != "")
                        XmlNF.Add("<esp>" + Nota.Tables[0].Rows[0]["ESPECIE"].ToString().Trim() + "</esp>");
                    if (Nota.Tables[0].Rows[0]["Marca"].ToString().Trim() != "")
                        XmlNF.Add("<marca>" + Nota.Tables[0].Rows[0]["Marca"].ToString().Trim() + "</marca>");

                    XmlNF.Add("<pesoL>" + string.Format("{0:N3}", decimal.Parse(Nota.Tables[0].Rows[0]["PESOLIQUIDO"].ToString())).Replace(".", "").Replace(",", ".") + "</pesoL>");
                    XmlNF.Add("<pesoB>" + string.Format("{0:N3}", decimal.Parse(Nota.Tables[0].Rows[0]["PESOLIQUIDO"].ToString())).Replace(".", "").Replace(",", ".") + "</pesoB>");
                    XmlNF.Add("</vol>");
                }
                XmlNF.Add("</transp>");

                //Dados do Pagamento

                XmlNF.Add("<pag>");
                XmlNF.Add("<detPag>");
                if (int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString()) == 1)
                {
                    XmlNF.Add("<tPag>99</tPag>");
                    XmlNF.Add("<vPag>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRNOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vPag>");
                }
                else
                {
                    XmlNF.Add("<tPag>90</tPag>");
                    XmlNF.Add("<vPag>0.00</vPag>");
                }
                XmlNF.Add("</detPag>");
                XmlNF.Add("</pag>");

                //Dados Adicionais
                XmlNF.Add("<infAdic>");
                if (Nota.Tables[0].Rows[0]["OBSERVACAO"].ToString() != "")
                    XmlNF.Add("<infCpl>" + Nota.Tables[0].Rows[0]["OBSERVACAO"].ToString() + "</infCpl>");
                XmlNF.Add("</infAdic>");
                XmlNF.Add("</infNFe>");
                XmlNF.Add("</NFe>");
                XmlNF.Add("</enviNFe>");
                //XmlNF.Add("</msgDados>");



                string Xml = "";

                for (int i = 0; i <= XmlNF.Count - 1; i++)
                    Xml = Xml + XmlNF[i].ToString();


                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();
                DateTime Dt = oCertificado.NotAfter.Date;
                if ((Dt - DateTime.Now).Days <= 15)
                    MessageBox.Show("Certificado valido ate: " + Dt.Date.ToShortDateString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AssinarXmlNfe AssinarXml = new AssinarXmlNfe();
                //Console.Write(”URI a ser assinada (Ex.: infCanc, infNFe, infInut, etc.) :”);          
                AssinarXml.Assinar(Xml, "infNFe", oCertificado);

                StreamWriter XmlNota;
                DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeEnviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial));
                if (!VerPath.Exists)
                    VerPath.Create();

                string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeEnviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\nfe" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + ".xml";
                XmlNota = File.CreateText(vArqXML);
                Xml = AssinarXml.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "");
                Xml = Xml.Replace("</NFe>", "").Replace("</enviNFe>", "");
                Xml = Xml + "</NFe></enviNFe>";
                XmlNota.Write(Xml);
                XmlNota.Close();
                return Xml;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Falha ao Criar o XML erro:" + erro.ToString());
                return "";
            }

        }



    }
}
