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
    public class NFce
    {
        public Funcoes Controle = new Funcoes();
        public Parametros ParamNFE = new Parametros();
        public string nChaveNF;
        public string nProtocoloNF;
        public string nReciboNF;
        public string vMotivoRet;
        public string vXMLRetorno;
        public string cStat = "0";
        public string CnpjNFe = "";
        public string UltXmlPrc = "";
        public int NrSessao = 0;

        public void Inicializar_parametros(int IdFilial)
        {
            ParamNFE.Controle = Controle;
            ParamNFE.LerDados(IdFilial);            
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
                return "41";
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

        public string GerarXmlNFce(int IdNota, Filiais Filial, int TpEmissao)
        {
            /// TpEmissao 1-Normal 2-Contigência Form Seguraca 3-Contigência SCAN 4-Contigencia DPEC 5-Contigencia FS-DA Form Segurança DANFE         

            try
            {
                Verificar BuscaAux = new Verificar();
                BuscaAux.Controle = Controle;
                //
                Estados CadUF = new Estados();
                CadUF.Controle = Controle;

                CadUF.LerDados(Filial.Uf);

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

                // Cabeçalho 
                string ChaveNF = "25" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyMM") + Filial.Cnpj.Trim() + "65000" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "1" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString()));
                nChaveNF = ChaveNF + DVChaveNF(ChaveNF);

                 XmlNF.Add("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                 XmlNF.Add("<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">");

                 XmlNF.Add("<idLote>" + string.Format("{0:D12}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + "</idLote>");
                 XmlNF.Add("<indSinc>0</indSinc>");

                
                XmlNF.Add("<NFe>");

                XmlNF.Add("<infNFe Id =\"NFe" + ChaveNF + DVChaveNF(ChaveNF) + "\" versao=\"4.00\">");

                //Dados da Nota
                XmlNF.Add("<ide>");
                XmlNF.Add("<cUF>25</cUF>");
                XmlNF.Add("<cNF>" + string.Format("{0:D8}", int.Parse(Nota.Tables[0].Rows[0]["NUMFORMULARIO"].ToString())) + "</cNF>");                
                XmlNF.Add("<natOp>" + NaturezaOpNFE(int.Parse(Nota.Tables[0].Rows[0]["NATOP"].ToString())) + "</natOp>");
                XmlNF.Add("<mod>65</mod>");
                XmlNF.Add("<serie>0</serie>");
                XmlNF.Add("<nNF>" + Nota.Tables[0].Rows[0]["NUMNOTA"].ToString() + "</nNF>");
                //XmlNF.Add("<dhEmi>" + DateTime.Parse(Nota.Tables[0].Rows[0]["DTEmissao"].ToString()).Date.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhEmi>");                
                XmlNF.Add("<dhEmi>" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss-03:00") + "</dhEmi>");
                XmlNF.Add("<tpNF>1</tpNF>");
                XmlNF.Add("<idDest>1</idDest>");
                XmlNF.Add("<cMunFG>2507507</cMunFG>");                
                XmlNF.Add("<tpImp>4</tpImp>");
                XmlNF.Add("<tpEmis>" + TpEmissao.ToString() + "</tpEmis>");
                XmlNF.Add("<cDV>" + DVChaveNF(ChaveNF) + "</cDV>");

                XmlNF.Add("<tpAmb>1</tpAmb>");
                XmlNF.Add("<finNFe>1</finNFe>");                
                XmlNF.Add("<indFinal>1</indFinal>");
                XmlNF.Add("<indPres>1</indPres>");
                XmlNF.Add("<procEmi>0</procEmi>");
                XmlNF.Add("<verProc>ERP-SGE 1.0</verProc>");

                /*if (int.Parse(Nota.Tables[0].Rows[0]["FINALIDADE"].ToString()) == 3)
                {
                    XmlNF.Add("<NFref>");
                    XmlNF.Add("<refNFe>" + Nota.Tables[0].Rows[0]["ChaveNfeDev"].ToString().Trim() + "</refNFe>");
                    XmlNF.Add("</NFref>");
                }*/
                XmlNF.Add("</ide>");


                //Dados do Emitente
                XmlNF.Add("<emit>");
                XmlNF.Add("<CNPJ>" + Filial.Cnpj.Trim() + "</CNPJ>");
                XmlNF.Add("<xNome>" + Filial.Filial.Trim().Replace("-", "").Substring(0,55) + "</xNome>");
                XmlNF.Add("<xFant>" + Filial.Fantasia.Trim() + "</xFant>");
                XmlNF.Add("<enderEmit>");
                XmlNF.Add("<xLgr>" + Filial.Endereco.Trim() + "</xLgr> ");
                XmlNF.Add("<nro>" + Filial.Numero.Trim() + "</nro>"); ;
                XmlNF.Add("<xCpl>" + Filial.Complemento.Trim() + "</xCpl>");
                XmlNF.Add("<xBairro>" + Filial.Bairro.Trim() + "</xBairro>");              
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
                    XmlNF.Add("<xProd>" + Nota.Tables[0].Rows[I]["Descricao"].ToString().Trim() + "</xProd>");
                    //XmlNF.Add("<xProd>NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xProd>");
                    

                    if (Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() != "")
                        XmlNF.Add("<NCM>" + Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() + "</NCM>");
                    else
                        XmlNF.Add("<NCM>38089919</NCM>");
                    XmlNF.Add("<CEST>1300402</CEST>");
                    //XmlNF.Add("<CFOP>" + Nota.Tables[0].Rows[I]["CFOP"].ToString().Replace(".", "").Trim() + "</CFOP>");
                    XmlNF.Add("<CFOP>5405</CFOP>");
                    XmlNF.Add("<uCom>" + Nota.Tables[0].Rows[I]["Unidade"].ToString().Trim() + "</uCom>");
                    XmlNF.Add("<qCom>" + string.Format("{0:N3}", decimal.Parse(Nota.Tables[0].Rows[I]["Qtde"].ToString())).Replace(".", "").Replace(",", ".") + "</qCom>");
                    XmlNF.Add("<vUnCom>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrUnitario"].ToString())).Replace(".", "").Replace(",", ".") + "</vUnCom>");
                    XmlNF.Add("<vProd>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vProd>");
                    if (Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() != "")
                        XmlNF.Add("<cEANTrib>" + Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() + "</cEANTrib>");
                    else
                        XmlNF.Add("<cEANTrib></cEANTrib>");
                    XmlNF.Add("<uTrib>" + Nota.Tables[0].Rows[I]["Unidade"].ToString().Trim() + "</uTrib>");
                    XmlNF.Add("<qTrib>" + string.Format("{0:N3}", decimal.Parse(Nota.Tables[0].Rows[I]["Qtde"].ToString())).Replace(".", "").Replace(",", ".") + "</qTrib>");
                    XmlNF.Add("<vUnTrib>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrUnitario"].ToString())).Replace(".", "").Replace(",", ".") + "</vUnTrib>");
                    // XmlNF.Add("<vFrete>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRFRETE"].ToString())).Replace(".", "").Replace(",", ".") + "</vFrete>");

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
                    XmlNF.Add("<vTotTrib>0.00</vTotTrib>");
                    XmlNF.Add("<ICMS>");
                    XmlNF.Add("<ICMSSN500>");
                    XmlNF.Add("<orig>0</orig>");
                    XmlNF.Add("<CSOSN>500</CSOSN>");
                    XmlNF.Add("</ICMSSN500>");
                    XmlNF.Add("</ICMS>");
                    XmlNF.Add("<PIS>");
                    XmlNF.Add("<PISNT>");
                    XmlNF.Add("<CST>09</CST>");
                    XmlNF.Add("</PISNT>");
                    XmlNF.Add("</PIS>");
                    XmlNF.Add("<COFINS>");
                    XmlNF.Add("<COFINSNT>");
                    XmlNF.Add("<CST>09</CST>");
                    XmlNF.Add("</COFINSNT>");
                    XmlNF.Add("</COFINS>");
                    //ICMS                
                    /*if (decimal.Parse(Nota.Tables[0].Rows[I]["PIcms"].ToString()) > 0)
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
                            XmlNF.Add("</ICMSSN102>");
                            XmlNF.Add("</ICMS>");
                        }
                        else
                        {
                            XmlNF.Add("<ICMS>");                            
                            XmlNF.Add("<ICMS" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + ">");
                            XmlNF.Add("<orig>0</orig>");                            
                            XmlNF.Add("<CST>" + CodCST(int.Parse(Nota.Tables[0].Rows[I]["CST_ICMS"].ToString())) + "</CST>");                            
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
                    XmlNF.Add("<pPIS>1.65</pPIS>");
                    XmlNF.Add("<vPIS>" + string.Format("{0:N2}", (Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("1,65")) / 100), 2).Replace(".", "").Replace(",", ".") + "</vPIS>");
                    XmlNF.Add("</PISAliq>");
                    XmlNF.Add("</PIS>");
                    TotalPis = TotalPis + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("1,65") / 100, 2);
                    //COFINS
                    XmlNF.Add("<COFINS>");
                    XmlNF.Add("<COFINSAliq>");
                    XmlNF.Add("<CST>01</CST>");
                    XmlNF.Add("<vBC>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString())).Replace(".", "").Replace(",", ".") + "</vBC>");
                    XmlNF.Add("<pCOFINS>7.60</pCOFINS>");
                    XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("7,60")) / 100), 2).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                    //XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", (decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("7,60")) / 100).Replace(".", "").Replace(",", ".") + "</vCOFINS>");
                    XmlNF.Add("</COFINSAliq>");
                    XmlNF.Add("</COFINS>");
                    TotalCofins = TotalCofins + Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse("7,60") / 100, 2);

                    //Grupo de Tributação do ICMS para UF de Destino
                    if ((int.Parse(Nota.Tables[0].Rows[0]["DESTOPERACAO"].ToString()) + 1) == 2 && int.Parse(Nota.Tables[0].Rows[0]["CONSUMIDOR"].ToString()) == 1 && Nota.Tables[0].Rows[0]["INSC_UF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "").Trim() == "" && Nota.Tables[0].Rows[0]["EntSaida"].ToString() == "0")
                    {
                        decimal BaseIcms = 0;
                        if (decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) > 0)
                            BaseIcms = decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) - Math.Round((decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Nota.Tables[0].Rows[I]["PercRed"].ToString()) / 100), 2);
                        else
                            BaseIcms = decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString());
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
                    }*/
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
                XmlNF.Add("<vII>0.00</vII>");
                XmlNF.Add("<vIPI>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["IPINOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vIPI>");
                if (ParamNFE.NFEVersao == 3)
                    XmlNF.Add("<vIPIDevol>0.00</vIPIDevol>");
                XmlNF.Add("<vPIS>" + string.Format("{0:N2}", TotalPis.ToString().Replace(".", "").Replace(",", ".") + "</vPIS>"));
                XmlNF.Add("<vCOFINS>" + string.Format("{0:N2}", TotalCofins.ToString().Replace(".", "").Replace(",", ".") + "</vCOFINS>"));                
                XmlNF.Add("<vOutro>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLROUTRADESP"].ToString())).Replace(".", "").Replace(",", ".") + "</vOutro>");
                XmlNF.Add("<vNF>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRNOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vNF>");
                if (ParamNFE.NFEVersao == 2)
                    XmlNF.Add("<vTotTrib>0.00</vTotTrib>");
                XmlNF.Add("</ICMSTot>");
                XmlNF.Add("</total>");
                            
                XmlNF.Add("<transp>");
                XmlNF.Add("<modFrete>9</modFrete>");
                XmlNF.Add("</transp>");
                XmlNF.Add("<pag>");
                XmlNF.Add("<detPag>");
                XmlNF.Add("<tPag>01</tPag>");
                XmlNF.Add("<vPag>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VLRNOTA"].ToString())).Replace(".", "").Replace(",", ".") + "</vPag>");                
                XmlNF.Add("</detPag>");
                XmlNF.Add("</pag>");

                //Dados Adicionais
                XmlNF.Add("<infAdic>");
                if (Nota.Tables[0].Rows[0]["OBSERVACAO"].ToString() != "")
                    XmlNF.Add("<infCpl>" + Nota.Tables[0].Rows[0]["OBSERVACAO"].ToString() + "</infCpl>");
                XmlNF.Add("</infAdic>");
                XmlNF.Add("</infNFe>");

                XmlNF.Add("<infNFeSupl>");
                XmlNF.Add("<qrCode>");
                XmlNF.Add("<![CDATA[http://www.receita.pb.gov.br/nfce?p=" + GerarLinkConsulta(nChaveNF) + "]]>"); // + "|2|1|1|" + GetHashString(nChaveNF + "|2|1|1|3B605138-08E1-EEFC-BC54-5A81384F50F5") + "]]>");
                XmlNF.Add("</qrCode>");
                XmlNF.Add("<urlChave>www.receita.pb.gov.br/nfce/consulta</urlChave>");
                XmlNF.Add("</infNFeSupl>");
                XmlNF.Add("</NFe>");
                XmlNF.Add("</enviNFe>");            

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
                DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NFceEnviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial));
                if (!VerPath.Exists)
                    VerPath.Create();

                string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NFceEnviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\NFce" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + ".xml";
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
        public string GerarXmlMFE(int IdNota, Filiais Filial, int TpEmissao)
        {
            /// TpEmissao 1-Normal 2-Contigência Form Seguraca 3-Contigência SCAN 4-Contigencia DPEC 5-Contigencia FS-DA Form Segurança DANFE         

            try
            {
                Verificar BuscaAux = new Verificar();
                BuscaAux.Controle = Controle;
                //
                Estados CadUF = new Estados();
                CadUF.Controle = Controle;

                CadUF.LerDados(Filial.Uf);

                ArrayList XmlNF = new ArrayList();
                DataSet Nota = new DataSet();
                string sSQL = "SELECT T2.ENTSAIDA,T2.ID_PESSOA,T2.RAZAOSOCIAL,T2.CNPJCPF,T2.INSC_UF,T2.DTEMISSAO,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO," +
                              "       T2.TELEFONE,T2.CEP,T2.BAIRRO,T2.CIDADE,T4.SIGLA,T4.CODIBGE,T3.CODBARRA,T3.REFERENCIA,T3.DESCRICAO,T3.UNIDADE,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T1.PICMS,T1.VLRICMS AS ICMSITEM,T1.PERCRED,T1.PIPI,T1.VlrIpi as VlrIpiItem," +
                              "       T2.BICMS,T2.VLRICMS,T2.BICMSSUB,T2.VLRICMSSUB,T2.VLRFRETE,T2.VLRIPI AS IPINOTA,T2.VLRSEGURO,T2.VLROUTRADESP,T2.VLRFRETE,T2.VLRPRODUTOS,T2.VLRNOTA,T2.VLRDESCONTO,T2.FRETE," +
                              "       T2.QTDEVOLUME,T2.ESPECIE,T2.MARCA,T2.PESOBRUTO,T2.PESOLIQUIDO,T2.NUMFORMULARIO,T2.NUMNOTA,T2.OBSERVACAO,T5.CFOP,T5.NATUREZA,T7.VENDEDOR,ISNULL(T8.PRIMPARCELA,0) AS PRIMPARCELA,T3.NCM,T2.CONSUMIDOR,T2.ATENDIMENTO,T2.DESTOPERACAO," +
                              "       T2.FINALIDADE,T2.ChaveNfeDev,ISNULL(T2.NATOP,0) AS NATOP, IsNull(T1.CST,0) as CST_ICMS,T2.CODMUN,T2.NUMPEDIDO,ISNULL(T1.ITEMPED,0) AS ITEMPED,Isnull(T2.ICMSInterno,0) as ICMSInterno, Isnull(T2.PercDifal,0) as PercDifal, " +
                              "       T2.ID_TRANSPORTADORA,T9.RazaoSocial AS TRANSPORTADORA, T9.CNPJ AS CNPJTRANSP,T9.Cidade AS CidadeTransp,ET.Sigla as UFTransp, T1.CODPRDCLIENTE, T2.ID_VENDA, Isnull(T2.NrSessao,0) as NrSessao" +
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
                if (int.Parse(Nota.Tables[0].Rows[0]["NrSessao"].ToString()) > 0)
                    NrSessao = int.Parse(Nota.Tables[0].Rows[0]["NrSessao"].ToString());
                else
                {
                    NrSessao = Controle.ProximoID("SessaoMFE");
                    Controle.ExecutaSQL("Update NotaFiscal set NrSessao=" + NrSessao.ToString() + " Where Id_nota=" + IdNota.ToString());
                }
                
                XmlNF.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                XmlNF.Add("<CFe>");
                XmlNF.Add("<infCFe versaoDadosEnt=\"0.07\">");
                XmlNF.Add("<ide>");
                if (ParamNFE.NFEAmbiente == 0)
                {
                    XmlNF.Add("<CNPJ>16716114000172</CNPJ>");
                    XmlNF.Add("<signAC>SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT</signAC>");
                }
                else
                {
                    XmlNF.Add("<CNPJ>11831930000103</CNPJ>"); //CNPJ do Desenvolvedor
                    XmlNF.Add("<signAC>" + ParamNFE.ChaveMFe.ToString() + "</signAC>");
                }
                XmlNF.Add(" <numeroCaixa>002</numeroCaixa>");

                XmlNF.Add("</ide>");
                XmlNF.Add("<emit>");
                if (ParamNFE.NFEAmbiente == 0)
                {
                    XmlNF.Add("<CNPJ>08723218000186</CNPJ>");
                    XmlNF.Add("<IE>562377111111</IE>");
                }
                else
                {
                    XmlNF.Add("<CNPJ>" + Filial.Cnpj.Trim() + "</CNPJ>");
                    XmlNF.Add("<IE>" + string.Format("{0:D12}", int.Parse(Filial.InscUF.Replace(".", "").Replace("-", "").Replace("/", "").Replace(",", "").Trim())) + "</IE>");
                }
                XmlNF.Add("<indRatISSQN>N</indRatISSQN>");
                XmlNF.Add("</emit>");
                XmlNF.Add("<dest>");
                
                if (Consumidor(Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim()))
                    XmlNF.Add("<xNome>VENDA A CONSUMIDOR</xNome>");
                else
                {
                    if (Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim().Length > 11)
                        XmlNF.Add("<CNPJ>"+ Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim()+"</CNPJ>");
                    else
                        XmlNF.Add("<CPF>" + Nota.Tables[0].Rows[0]["CNPJCPF"].ToString().Trim() + "</CPF>");
                    if (Nota.Tables[0].Rows[0]["RAZAOSOCIAL"].ToString().Trim().Length > 55)
                        XmlNF.Add("<xNome>" + Nota.Tables[0].Rows[0]["RAZAOSOCIAL"].ToString().Replace("&", "").Trim().Substring(0,55) + "</xNome>");
                    else
                        XmlNF.Add("<xNome>" + Nota.Tables[0].Rows[0]["RAZAOSOCIAL"].ToString().Replace("&", "").Trim() + "</xNome>");
                }
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
                   /* if (Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() != "")
                        XmlNF.Add("<cEAN>" + Nota.Tables[0].Rows[I]["CODBARRA"].ToString().Trim() + "</cEAN>");
                    else
                        XmlNF.Add("<cEAN></cEAN>");*/
                    XmlNF.Add("<xProd>" + Nota.Tables[0].Rows[I]["Descricao"].ToString().Trim() + "</xProd>");
                    //XmlNF.Add("<xProd>NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xProd>");


                    if (Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() != "")
                        XmlNF.Add("<NCM>" + Nota.Tables[0].Rows[I]["NCM"].ToString().Trim() + "</NCM>");
                    else
                        XmlNF.Add("<NCM>38089919</NCM>");
                   
                    XmlNF.Add("<CFOP>5405</CFOP>");
                    XmlNF.Add("<uCom>" + Nota.Tables[0].Rows[I]["Unidade"].ToString().Trim() + "</uCom>");
                    XmlNF.Add("<qCom>" + string.Format("{0:N4}", decimal.Parse(Nota.Tables[0].Rows[I]["Qtde"].ToString())).Replace(".", "").Replace(",", ".") + "</qCom>");
                    XmlNF.Add("<vUnCom>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrUnitario"].ToString())).Replace(".", "").Replace(",", ".") + "</vUnCom>");
                    XmlNF.Add("<indRegra>A</indRegra>");
                     if (decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString()) > 0)
                     {
                         decimal VlrDesconto = Math.Round(decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * (100 / (decimal.Parse(Nota.Tables[0].Rows[I]["VLRPRODUTOS"].ToString()) / decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString())) / 100), 2);
                         TotalDesconto = TotalDesconto + VlrDesconto;

                         decimal difDesc = 0;
                         

                        if (I == Nota.Tables[0].Rows.Count - 1)
                        {
                            if (TotalDesconto != decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString()))
                                difDesc = decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString()) - TotalDesconto;
                            VlrDesconto  = VlrDesconto + difDesc;
                            TotalDesconto = TotalDesconto + difDesc;
                        }


                         if (string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[I]["VlrTotal"].ToString()) * (100 / (decimal.Parse(Nota.Tables[0].Rows[I]["VLRPRODUTOS"].ToString()) / decimal.Parse(Nota.Tables[0].Rows[I]["VlrDesconto"].ToString())) / 100)).Replace(".", "").Replace(",", ".") != "0.00")
                             XmlNF.Add("<vDesc>" + string.Format("{0:N2}", ((VlrDesconto).ToString()).Replace(".", "").Replace(",", ".")) + "</vDesc>");
                     }
                    XmlNF.Add("</prod>");
                    XmlNF.Add("<imposto>");
                    XmlNF.Add("<vItem12741>0.00</vItem12741>");                    
                    XmlNF.Add("<ICMS>");
                    XmlNF.Add("<ICMSSN102>");
                    XmlNF.Add("<Orig>0</Orig>");
                    XmlNF.Add("<CSOSN>102</CSOSN>");
                    XmlNF.Add("</ICMSSN102>");
                    XmlNF.Add("</ICMS>");
                    XmlNF.Add("<PIS>");
                    XmlNF.Add("<PISNT>");
                    XmlNF.Add("<CST>09</CST>");
                    XmlNF.Add("</PISNT>");
                    XmlNF.Add("</PIS>");
                    XmlNF.Add("<COFINS>");
                    XmlNF.Add("<COFINSNT>");
                    XmlNF.Add("<CST>09</CST>");
                    XmlNF.Add("</COFINSNT>");
                    XmlNF.Add("</COFINS>");                   
                    XmlNF.Add("</imposto>");
                    XmlNF.Add("</det>");
                }
                // Total da Nota
                XmlNF.Add("<total>");
                XmlNF.Add("<vCFeLei12741>0.00</vCFeLei12741>");
               // XmlNF.Add("<vDesc>" + string.Format("{0:N2}", decimal.Parse(Nota.Tables[0].Rows[0]["VlrDesconto"].ToString())).Replace(".", "").Replace(",", ".") + "</vDesc>");
                XmlNF.Add("</total>");


                XmlNF.Add("<pgto>");
                SqlDataReader TabPag;
                TabPag = Controle.ConsultaSQL("select t1.TpPag,t1.Valor from PagamentoMFe t1   where t1.id_venda =" + Nota.Tables[0].Rows[0]["id_Venda"].ToString().Trim());
                if (TabPag.HasRows)
                {
                    while (TabPag.Read())
                    {
                        XmlNF.Add("<MP>");
                        XmlNF.Add("<cMP>" + string.Format("{0:D2}",int.Parse(TabPag["TpPag"].ToString() ))+ "</cMP>");
                        XmlNF.Add("<vMP>" + string.Format("{0:N2}", decimal.Parse(TabPag["Valor"].ToString())).Replace(".", "").Replace(",", ".") + "</vMP>");
                       if (int.Parse(TabPag["TpPag"].ToString())==3 || int.Parse(TabPag["TpPag"].ToString())==4)
                            XmlNF.Add("<cAdmC>999</cAdmC>");  
                        XmlNF.Add("</MP>");
                    }
                }
                XmlNF.Add("</pgto>");
                //Dados Adicionais
                XmlNF.Add("<infAdic>");
                XmlNF.Add("<infCpl>Nao Aceitamos Devolucoes sem este Comprovante</infCpl>");
                XmlNF.Add("</infAdic>");
                XmlNF.Add("</infCFe>");
                XmlNF.Add("</CFe>");
               /* XmlNF.Add("]]>");
                XmlNF.Add("</Valor>");
                XmlNF.Add("</Parametro>");                
                XmlNF.Add("</Parametros>");
                XmlNF.Add("</Metodo>");
                XmlNF.Add("</Componente>");
                XmlNF.Add("</Integrador>");*/

                /*XmlNF.Add("<infNFeSupl>");
                XmlNF.Add("<qrCode>");
                XmlNF.Add("<![CDATA[http://www.receita.pb.gov.br/nfce?p=" + GerarLinkConsulta(nChaveNF) + "]]>"); // + "|2|1|1|" + GetHashString(nChaveNF + "|2|1|1|3B605138-08E1-EEFC-BC54-5A81384F50F5") + "]]>");
                XmlNF.Add("</qrCode>");
                XmlNF.Add("<urlChave>www.receita.pb.gov.br/nfce/consulta</urlChave>");
                XmlNF.Add("</infNFeSupl>");/
                XmlNF.Add("</NFe>");*/

                string Xml = "";

                for (int i = 0; i <= XmlNF.Count - 1; i++)
                    Xml = Xml + XmlNF[i].ToString();


                /*X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();
                DateTime Dt = oCertificado.NotAfter.Date;
                if ((Dt - DateTime.Now).Days <= 15)
                    MessageBox.Show("Certificado valido ate: " + Dt.Date.ToShortDateString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AssinarXmlNfe AssinarXml = new AssinarXmlNfe();
                //Console.Write(”URI a ser assinada (Ex.: infCanc, infNFe, infInut, etc.) :”);          
                AssinarXml.Assinar(Xml, "infNFe", oCertificado);*/

                StreamWriter XmlNota;
                DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Enviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial));
                if (!VerPath.Exists)
                    VerPath.Create();

                string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFe_Enviada\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\MFe" + string.Format("{0:D9}", int.Parse(Nota.Tables[0].Rows[0]["NUMNOTA"].ToString())) + ".xml";
                XmlNota = File.CreateText(vArqXML);
                /*Xml = AssinarXml.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "");
                Xml = Xml.Replace("</NFe>", "").Replace("</enviNFe>", "");
                Xml = Xml + "</NFe></enviNFe>";*/
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

        private bool Consumidor(string Cnpj)
        {
            if ((Cnpj == "00000000000000" || Cnpj == "11111111111111" || Cnpj == "22222222222222" || Cnpj == "33333333333333" || Cnpj == "44444444444444"
                          || Cnpj == "55555555555555" || Cnpj == "66666666666666" || Cnpj == "77777777777777" || Cnpj == "88888888888888" || Cnpj == "99999999999999"
                          || Cnpj == "00000000000" || Cnpj == "11111111111" || Cnpj == "22222222222" || Cnpj == "33333333333" || Cnpj == "44444444444"
                          || Cnpj == "55555555555" || Cnpj == "66666666666" || Cnpj == "77777777777" || Cnpj == "88888888888" || Cnpj == "99999999999"))
            {
                return true;
            }
            else
                return false;
        }
        public string GerarLinkConsulta(string ChaveAcesso)
        {
            string ParametrosQR = ChaveAcesso + "|2|1|1";

            string HashQRCode = GetSHA1HashData(ParametrosQR + "3B605138-08E1-EEFC-BC54-5A81384F50F5", true);
            string ParametrosLinkConsulta = ParametrosQR.Trim() + "|" + HashQRCode.Trim();
            return ParametrosLinkConsulta;
        }
        public static string GetSHA1HashData(string data, bool toUpper)
        {
            HashAlgorithm algorithm = new SHA1CryptoServiceProvider();
            byte[] buffer = algorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(data));
            System.Text.StringBuilder builder = new System.Text.StringBuilder(buffer.Length);
            foreach (byte num in buffer)
            {
                if (toUpper)
                    builder.Append(num.ToString("X2"));
                else
                    builder.Append(num.ToString("x2"));
            }

            return builder.ToString();
        }
        public string CancelarNFe(string Chave, string Protocolo, string Justificativa)
        {
            try
            {
                string Xml = "";

                Xml = Xml + "<evento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\">";
                Xml = Xml + "<infEvento Id=\"ID110111" + Chave + "01\">";
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
                oServico.Url = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?WSDL12";
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
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, Certificado, false);
            X509Certificate2 oCertificado = null;
            for (int I = 0; I <= collection.Count - 1; I++)
            {
                if (collection[I].Subject == Certificado)
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
        public string EnviarNFce(string XmlNfe)
        {
            //Incluindo a Certificao no Envio;
            X509Certificate2 oCertificado = new X509Certificate2();
            oCertificado = PrepararCertificacao();

            XmlDocument MsgXML = new XmlDocument();
            MsgXML.LoadXml(XmlNfe);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            NFeAutorizacao4.NFeAutorizacao4 oServico = new NFeAutorizacao4.NFeAutorizacao4();
            //oServico.Url = "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx?WSDL12";
            oServico.Url = "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx?WSDL12";        
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
                oServico.Url = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?WSDL12";
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
                //if (ParamNFE.NFEAmbiente == 0)
                    Xml = Xml + "<tpAmb>1</tpAmb>";
                //else
                //    Xml = Xml + "<tpAmb>1</tpAmb>";
                Xml = Xml + "<nRec>" + Recibo + "</nRec></consReciNFe>";


                X509Certificate2 oCertificado = new X509Certificate2();
                oCertificado = PrepararCertificacao();

                XmlDocument MsgXML = new XmlDocument();
                MsgXML.LoadXml(Xml);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                NFeRetAutorizacao4.NFeRetAutorizacao4 oServico = new NFeRetAutorizacao4.NFeRetAutorizacao4();
                //oServico.Url = "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx?WSDL12";                                
                oServico.Url = "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx";
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
        public void GravarXmlRetorno(string Xml, string NumRec)
        {
            DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NFceRetorno\\" + string.Format("{0:D3}", ParamNFE.IdFilial));

            if (!VerPath.Exists)
                VerPath.Create();

            StreamWriter XmlNota;
            string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NFceRetorno\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\RetNFce" + NumRec + ".xml";
            XmlNota = File.CreateText(vArqXML);
            XmlNota.Write(Xml);
            XmlNota.Close();
        }
        public void GravarXmlRetornoNFE(int IdNota, string XmlNFe, string XmlRet)
        {

            //XmlRet= "<retConsReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" xmlns:ns2=\"http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4\" versao=\"4.00\"><tpAmb>1</tpAmb><verAplic>PR_NFCe_V4.0.18</verAplic><nRec>231000359119113</nRec><cStat>104</cStat><xMotivo>Lote processado</xMotivo><cUF>25</cUF><dhRecbto>"+DateTime.Now.ToString()+"</dhRecbto><protNFe versao=\"4.00\"><infProt Id=\"ID123190028325387\"><tpAmb>1</tpAmb><verAplic>PR_NFCe_V4.0.18</verAplic><chNFe>"+nChaveNF+"</chNFe><dhRecbto>"+DateTime.Now.ToString()+"</dhRecbto><nProt>123190028325387</nProt><digVal>LU0hYBv+JMmc9gxf6TpoSyK0qo0=</digVal><cStat>100</cStat><xMotivo>Autorizado o uso da NF-e</xMotivo></infProt></protNFe></retConsReciNFe>";
            DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NFceProc\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())));

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

            string strXmlProcNfe = "";
            if (ParamNFE.NFEVersao == 0)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.10\">" + strNFe + strNFeRet + "</nfeProc>";
            else if (ParamNFE.NFEVersao == 1)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\">" + strNFe + strNFeRet + "</nfeProc>";
            else if (ParamNFE.NFEVersao == 2)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"3.10\">" + strNFe + strNFeRet + "</nfeProc>";
            else if (ParamNFE.NFEVersao == 3)
                strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">" + strNFe + strNFeRet + "</nfeProc>";

            StreamWriter ArqXmlNota;
            string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\NFceProc\" + string.Format("{0:D3}", ParamNFE.IdFilial) + @"\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + @"\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())) + @"\NFce-" + string.Format("{0:D8}", IdNota) + "-procNFce.xml";
            ArqXmlNota = File.CreateText(vArqXML);
            ArqXmlNota.Write(strXmlProcNfe);
            ArqXmlNota.Close();
            UltXmlPrc = vArqXML;
        }

        public string[] GravarXmlRetornoMFE(int IdNota, string XmlRet)
        {                        
            DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFeProc\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())));

            if (!VerPath.Exists)
                VerPath.Create();

            string[] param = XmlRet.Split(char.Parse("|"));
            if (param[0].Substring(0, 6) != "Timeou")
            {
                if (param[1].ToString() == "06000")
                {
                    string base64Encoded = param[6].ToString();
                    string base64Decoded;
                    byte[] data = System.Convert.FromBase64String(base64Encoded);
                    base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);



                    StreamWriter ArqXmlNota;
                    string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\MFeProc\" + string.Format("{0:D3}", ParamNFE.IdFilial) + @"\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + @"\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())) + @"\MFe-" + string.Format("{0:D8}", IdNota) + "-procMFe.xml";
                    ArqXmlNota = File.CreateText(vArqXML);
                    ArqXmlNota.Write(base64Decoded);
                    ArqXmlNota.Close();
                    UltXmlPrc = vArqXML;
                }
            }

            return param;
        }

        public string[] GravarXmlRetCancMFE(int IdNota, string XmlRet)
        {

            //XmlRet= "<retConsReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" xmlns:ns2=\"http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4\" versao=\"4.00\"><tpAmb>1</tpAmb><verAplic>PR_NFCe_V4.0.18</verAplic><nRec>231000359119113</nRec><cStat>104</cStat><xMotivo>Lote processado</xMotivo><cUF>25</cUF><dhRecbto>"+DateTime.Now.ToString()+"</dhRecbto><protNFe versao=\"4.00\"><infProt Id=\"ID123190028325387\"><tpAmb>1</tpAmb><verAplic>PR_NFCe_V4.0.18</verAplic><chNFe>"+nChaveNF+"</chNFe><dhRecbto>"+DateTime.Now.ToString()+"</dhRecbto><nProt>123190028325387</nProt><digVal>LU0hYBv+JMmc9gxf6TpoSyK0qo0=</digVal><cStat>100</cStat><xMotivo>Autorizado o uso da NF-e</xMotivo></infProt></protNFe></retConsReciNFe>";
            DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\MFeProc\\" + string.Format("{0:D3}", ParamNFE.IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())));

            if (!VerPath.Exists)
                VerPath.Create();

            string[] param = XmlRet.Split(char.Parse("|"));

            if (param[0].Substring(0, 6) != "Timeou")
            {
                if (param[1].ToString() == "07000")
                {
                    string base64Encoded = param[6].ToString();
                    string base64Decoded;
                    byte[] data = System.Convert.FromBase64String(base64Encoded);
                    base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);

                    StreamWriter ArqXmlNota;
                    string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\MFeProc\" + string.Format("{0:D3}", ParamNFE.IdFilial) + @"\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + @"\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())) + @"\MFe-" + string.Format("{0:D8}", IdNota) + "-CancMFe.xml";
                    ArqXmlNota = File.CreateText(vArqXML);
                    ArqXmlNota.Write(base64Decoded);
                    ArqXmlNota.Close();
                    UltXmlPrc = vArqXML;
                }
            }
            return param;
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

            cStat = "0";
            string nProt = "";
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

                        nProt = infRecElemento.GetElementsByTagName("nProt")[0].InnerText;
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
                        vMotivoRet = infRecElemento.GetElementsByTagName("xMotivo")[0].InnerText;
                        cStat = infRecElemento.GetElementsByTagName("cStat")[0].InnerText;
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
                vXMLRetorno = "";
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

        public int GerarNFce(int IdVenda, int NFE)
        {
            decimal TProdutos = 0;
            int NumNFce = 0;
            try
            {   
                DataSet ConsItens = new DataSet();
                ConsItens = Controle.ConsultaTabela("SELECT * FROM MVVENDAITENS T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) WHERE T1.ID_VENDA=" + IdVenda.ToString() + " ORDER BY T1.ID_ITEM");
                if (ConsItens.Tables[0].Rows.Count > 0)
                {
                    Pessoas CadPessoa = new Pessoas();
                    NotaFiscal CadNota = new NotaFiscal();
                    Produtos CadProd = new Produtos();
                    NotaFiscalItens Itens = new NotaFiscalItens();
                    MvVenda Vendas = new MvVenda();
                    Parametros ParamFilial = new Parametros();
                    Estados CadUF = new Estados();
                    Vendas.Controle = Controle;
                    Itens.Controle = Controle;
                    CadProd.Controle = Controle;
                    CadPessoa.Controle = Controle;
                    CadNota.Controle = Controle;
                    ParamFilial.Controle = Controle;
                    CadUF.Controle = Controle;
                    Vendas.LerDados(IdVenda);

                    Filiais CadFilial = new Filiais();
                    CadFilial.Controle = Controle;

                    int NLin = 1;
                    int SeqNota = 0;
                    for (int I = 0; I <= ConsItens.Tables[0].Rows.Count - 1; I++)
                    {
                        if (NLin == 1)
                        {
                            //  string CnpjCpf = ConsItens.Tables[0].Rows[I]["CnpjCpf"].ToString().Trim();
                            /*  if (CnpjCpf == "00000000000000" || CnpjCpf == "11111111111111" || CnpjCpf == "22222222222222" || CnpjCpf == "33333333333333" || CnpjCpf == "44444444444444"
                               || CnpjCpf == "55555555555555" || CnpjCpf == "66666666666666" || CnpjCpf == "77777777777777" || CnpjCpf == "88888888888888" || CnpjCpf == "99999999999999")
                                  return -1;
                              if (CnpjCpf == "00000000000" || CnpjCpf == "11111111111" || CnpjCpf == "22222222222" || CnpjCpf == "33333333333" || CnpjCpf == "44444444444"
                               || CnpjCpf == "55555555555" || CnpjCpf == "66666666666" || CnpjCpf == "77777777777" || CnpjCpf == "88888888888" || CnpjCpf == "99999999999")
                              {
                                  return -1;
                              }*/
                            SeqNota++;
                            CadFilial.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Filial"].ToString()));
                            CadPessoa.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Pessoa"].ToString()));
                            CadNota.LerDados(0);
                            CadNota.DtEmissao = DateTime.Now.Date;
                            CadNota.IdFilial = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Filial"].ToString());
                            CadNota.CnpjCpf = ConsItens.Tables[0].Rows[I]["CnpjCpf"].ToString();
                            CadNota.IdPessoa = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Pessoa"].ToString());
                            CadNota.NmPessoa = ConsItens.Tables[0].Rows[I]["Pessoa"].ToString();
                            CadNota.InscUf = ConsItens.Tables[0].Rows[I]["InscUF"].ToString();
                            CadNota.Cep = ConsItens.Tables[0].Rows[I]["Cep"].ToString().Replace("-", "");
                            CadNota.Endereco = ConsItens.Tables[0].Rows[I]["Endereco"].ToString();
                            CadNota.Numero = ConsItens.Tables[0].Rows[I]["Numero"].ToString();
                            CadNota.Compl = ConsItens.Tables[0].Rows[I]["Complemento"].ToString();
                            CadNota.Cidade = ConsItens.Tables[0].Rows[I]["Cidade"].ToString();
                            CadNota.Bairro = ConsItens.Tables[0].Rows[I]["Bairro"].ToString();
                            CadNota.Telefone = ConsItens.Tables[0].Rows[I]["Fone"].ToString();
                            CadNota.IdUf = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Uf"].ToString());
                            CadNota.Pais = ConsItens.Tables[0].Rows[I]["Pais"].ToString();
                            CadNota.Observacao = ConsItens.Tables[0].Rows[I]["Observacao"].ToString();
                            CadNota.NumPedido = ConsItens.Tables[0].Rows[I]["NumPedido"].ToString();
                            CadNota.Especie = "VARIADAS";
                            CadNota.Marca = "VARIADAS";
                            CadNota.IdCfop = CadPessoa.IdCfop;
                            CadNota.Frete = CadPessoa.Frete;
                            CadNota.CodMun = CadPessoa.CodMun;
                            CadNota.EntSaida = 0;
                            CadNota.IdVenda = IdVenda;
                            CadNota.SeqImp = SeqNota;
                            CadNota.Status = 0;
                            CadNota.Consumidor = 1;
                            CadNota.NFE = NFE;
                            ParamFilial.LerDados(CadNota.IdFilial);

                            CadUF.LerDados(CadNota.IdUf);
                            CadNota.ICMSInterno = CadUF.ICMSInterno;
                            CadNota.PercDifal = CadUF.PercDifal;
                            CadNota.VlrDesconto = Vendas.VlrDesconto;

                            if (NFE == 3)
                            {
                                ParamFilial.ProxCFe(CadNota.IdFilial);
                                CadNota.NumNota = ParamFilial.NumCFe;
                                CadNota.NumFormulario = ParamFilial.NumCFe;
                            }
                            else
                            {
                                ParamFilial.ProxNFce(CadNota.IdFilial);
                                CadNota.NumNota = ParamFilial.NumNFce;
                                CadNota.NumFormulario = ParamFilial.NumNFce;
                            }

                            CadNota.IdTransportadora = CadPessoa.IdTransportadora;
                            CadNota.ReciboNfe = "";
                            CadNota.ProtocoloNfe = "";

                            if (ParamFilial.ObsNF.Trim() != "")
                                CadNota.Observacao = ParamFilial.ObsNF;

                            CadNota.ChaveNfe = "";
                            CadNota.GravarDados();

                            NumNFce = CadNota.IdNota;

                            //Registro de Auditoria
                            //RegistrarAuditoria(this.Text, ItemCadNota.IdItem, CadNota.NumNota.ToString(), 1, "Gerar Nota Fiscal");                            
                        }
                        CadProd.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString()));

                        //Verificação do Kit do Produto

                        Itens.LerDados(0);
                        Itens.IdNota = CadNota.IdNota;
                        Itens.IdProduto = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                        Itens.Qtde = decimal.Parse(ConsItens.Tables[0].Rows[I]["Qtde"].ToString());
                        Itens.VlrUnitario = decimal.Parse(ConsItens.Tables[0].Rows[I]["VlrUnitario"].ToString());
                        Itens.ItemPed = int.Parse(ConsItens.Tables[0].Rows[I]["ItemPed"].ToString());
                        Itens.IdPromocao = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Promocao"].ToString());
                        Itens.VlrTotal = Itens.Qtde * Itens.VlrUnitario;
                        Itens.PIcms = CadProd.IcmsIss;
                        Itens.SitTributaria = CadProd.SitTributaria;
                        Itens.IdCfop = CadNota.IdCfop;
                        TProdutos = TProdutos + Itens.VlrTotal;

                        if (Itens.SitTributaria == 3)
                            Itens.PIcms = 0;

                        if (ParamFilial.NotaIPI == 1)
                            Itens.PIpi = CadProd.Ipi;


                        if (Itens.SitTributaria == 3)
                            Itens.IdCfop = 40;
                        else
                            Itens.IdCfop = 1;


                        if (Itens.SitTributaria != 0)
                            Itens.PIcms = 0;

                        ValidarCST(CadNota, Itens);
                        Itens.GravarDados();

                        if (CadNota.IdUf != CadFilial.Uf)
                            CadNota.DestOperacao = 1;

                        CadNota.GravarDados();
                        NLin = NLin + 1;
                    }

                    if (NumNFce > 0)
                    {
                        Controle.ExecutaSQL("UPDATE MVVENDA SET ID_LancCF='" + NumNFce.ToString() + "' WHERE ID_VENDA=" + IdVenda.ToString());
                        Controle.ExecutaSQL("UPDATE LANCFINANCEIRO SET NOTAFISCAL='" + NumNFce.ToString() + "' WHERE ID_VENDA=" + IdVenda.ToString());
                    }
                    CadNota.VlrProdutos = TProdutos;
                    CadNota.VlrNota = TProdutos - CadNota.VlrDesconto;
                    if (CadNota.VlrIpi > 0)
                        CadNota.VlrDesconto = CadNota.VlrDesconto + CadNota.VlrIpi;

                    CadNota.GravarDados();

                }
            }
            catch
            {
                return -0;
            }
            return NumNFce;
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        private void ValidarCST(NotaFiscal CadNFe, NotaFiscalItens Item)
        {
            Filiais CadFilial = new Filiais();
            CadFilial.Controle = Controle;
            CadFilial.LerDados(CadNFe.IdFilial);

            Produtos CadPrd = new Produtos();
            CadPrd.Controle = Controle;
            CadPrd.LerDados(Item.IdProduto);


            //if (Item.SitTributaria == 0 && (CadNFe.IdFilial == 1 || CadNFe.IdFilial == 6) && CadNFe.IdUf != CadFilial.Uf)
            if (Item.SitTributaria == 0 && (CadNFe.IdFilial == 1 || CadNFe.IdFilial == 6) && CadNFe.IdUf != CadFilial.Uf && CadNFe.InscUf.Trim() != "")
                Item.Cst = 10;
            else
            {
                if (Item.SitTributaria == 0)
                {
                    Item.Cst = 1;

                    if (Item.PercRed > 0)
                        Item.Cst = 3;
                }
                else if (Item.SitTributaria == 1)
                {
                    Item.Cst = 6;
                }
                else if (Item.SitTributaria == 2)
                    Item.Cst = 5;
                else if (Item.SitTributaria == 3)
                {
                    Item.Cst = 8;
                    //if (Item.PercRed > 0)
                    //    Item.Cst = 9;
                }
            }
        }
    }  

}
