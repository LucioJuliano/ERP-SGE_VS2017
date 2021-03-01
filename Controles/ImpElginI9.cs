using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using Controle_Dados;
using System.Drawing.Printing;
using System.ComponentModel;
using System.IO;
using DFW;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Controles
{
    public class ImpElginI9
    {
        public Funcoes Controle = new Funcoes();

        public void ImprimirNaoFiscal(int IdVenda, string ImpNome)
        {
            StringBuilder Cupom = new StringBuilder();
            Cupom.AppendLine("              DOCUMENTO NÃO FISCAL              ");             
            try
            {
                // Busca Venda      
                SqlDataReader DadosCliente = Controle.ConsultaSQL("SELECT T2.CNPJCPF,T2.PESSOA,T2.ENDERECO,T2.NUMERO,T2.BAIRRO,T2.VLRDESCONTO,T3.REFERENCIA,T3.DESCRICAO,T3.SITTRIBUTARIA,T3.ICMSISS,T1.QTDE,T1.VLRUNITARIO FROM MVVENDAITENS T1 " +
                                                                  " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_VENDA=" + IdVenda.ToString());
                SqlDataReader Venda = Controle.ConsultaSQL("SELECT T2.CNPJCPF,T2.PESSOA,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO,T2.BAIRRO,T2.VLRDESCONTO,T2.CREDITO,T3.REFERENCIA,T3.DESCRICAO,T3.SITTRIBUTARIA,T3.ICMSISS,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL AS TOTALITEM, T2.VLRTOTAL AS VLRVENDA,T2.VLRSUBTOTAL,T1.ID_PRODUTO,T3.NCM,T1.ID_VENDA,T3.UNIDADE FROM MVVENDAITENS T1 " +
                                                                  " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_VENDA=" + IdVenda.ToString());
                SqlDataReader Pag = Controle.ConsultaSQL("SELECT T1.VLRORIGINAL,T2.ID_DOCUMENTO,T2.DOCUMENTO,T2.CODECF FROM LANCFINANCEIRO T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO)  WHERE T1.ID_VENDA=" + IdVenda.ToString());

                //Inicializando Cupom                                                           
                DadosCliente.Read();
                try
                {
                    Cupom.AppendLine("     Data:" + DateTime.Now.Date.ToShortDateString() + "   No. Venda:" + IdVenda.ToString());
                    if (DadosCliente.HasRows)
                    {
                        Cupom.AppendLine("CPF/CNPJ: " + DadosCliente["CNPJCPF"].ToString().Trim());
                        Cupom.AppendLine("Cliente.: " + Controle.Space(DadosCliente["PESSOA"].ToString().Trim(), 30));
                    }

                    decimal VlrDesconto = 0;
                    decimal VlrVenda = 0;                    
                    decimal TVlrItem = 0;
                    int Item = 1;
                    Cupom.AppendLine("Item  Ref    Descrição");
                    Cupom.AppendLine("  Qtde.   Und.      Vlr.Unt         Vlr.Total");
                    Cupom.AppendLine("------------------------------------------------");
                    while (Venda.Read())
                    {                    
                        VlrDesconto = decimal.Parse(Venda["VLRDESCONTO"].ToString()) + decimal.Parse(Venda["CREDITO"].ToString());
                        VlrVenda    = decimal.Parse(Venda["VLRVENDA"].ToString());

                        Cupom.AppendLine(Controle.Space(Item.ToString(), 4) +" "+ Controle.Space(Venda["REFERENCIA"].ToString().Trim(), 7) + Controle.Space(Venda["DESCRICAO"].ToString().Trim(), 25));

                        Cupom.AppendLine(Controle.NumSpace(string.Format("{0:N0}", decimal.Parse(Venda["Qtde"].ToString())).ToString(), 6) + "    " + Controle.Space(Venda["UNIDADE"].ToString().Trim(), 3) + "    " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(Venda["VLRUNITARIO"].ToString())).ToString(), 10) + "         " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(Venda["TOTALITEM"].ToString())).ToString(), 10));
                        
                        TVlrItem = decimal.Parse(Venda["VlrUnitario"].ToString()) * decimal.Parse(Venda["QTDE"].ToString());
                        Item++;
                    }
                    Cupom.AppendLine("------------------------------------------------");
                    Cupom.AppendLine("                   (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", VlrVenda+VlrDesconto).ToString(), 10));
                    Cupom.AppendLine("                   (-) Desconto  R$:" + Controle.NumSpace(string.Format("{0:N2}", VlrDesconto).ToString(), 10));
                    Cupom.AppendLine("                                    -----------");
                    Cupom.AppendLine("                   (=) Total     R$:" + Controle.NumSpace(string.Format("{0:N2}", VlrVenda).ToString(), 10));
                    Cupom.AppendLine("------------------------------------------------");
                    Cupom.AppendLine("Documento sem valor Fiscal");
                    Cupom.AppendLine(" ");
                    Cupom.AppendLine(" ");
                    Cupom.AppendLine(" ");
                    Cupom.AppendLine(" ");
                    Cupom.AppendLine(" ");
                    Cupom.AppendLine(" ");
                    Cupom.AppendLine(" ");
                    Cupom.AppendLine(" ");
                    ElginI9.SendStringToPrinter(ImpNome, Cupom.ToString());                    
                }
                catch
                {
                    return;
                }
            }
            catch (Exception a)
            {
            }
        }
    }
}
