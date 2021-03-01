using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controles;
using Controle_Dados;
using System.Data.SqlClient;
namespace ERP_SGE
{
    public partial class FrmFichaFinanc : Form
    {
        Funcoes Controle = new Funcoes();                
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        public int IdPessoa = 0;
        public decimal LimiteCredito = 0;
        public bool TpCliente = true;
        
        public FrmFichaFinanc()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao = FrmPrincipal.Conexao;
        }

        private void FrmFichaFinanc_Shown(object sender, EventArgs e)
        {
            LblVlrMedia.Text = "";
            LblDtUlt.Text    = "";
            LblVlrUlt.Text   = "";
            LblLimiteCredito.Text = string.Format("{0:N2}", LimiteCredito);
            decimal CreditoUsado = 0;

            
            
            if (!TpCliente)
            {
                PnlCliente.Visible = false;
                label5.Visible     = false;
                LblDtCad.Visible   = false;
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);                
            }
            // Titulos em Atraso
            DataSet TabAtraso = new DataSet();
            TabAtraso = Controle.ConsultaTabela("SELECT T1.ID_LANC, CASE T1.PAGREC WHEN 1 THEN 'À Pagar' WHEN 2 THEN 'À Receber' ELSE ' ' END AS TIPOLANC,T1.NUMDOCUMENTO," +
                                                " T1.VENCIMENTO,T1.VLRORIGINAL,T1.REFERENTE,T3.DOCUMENTO AS TIPODOC, T4.FANTASIA AS FILIAL" +
                                                " FROM LANCFINANCEIRO T1 LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) LEFT JOIN TIPODOCUMENTO T3 ON (T3.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO)" +
                                                " LEFT JOIN EMPRESA_FILIAL T4 ON (T4.ID_FILIAL=T1.ID_FILIAL) Where t1.Id_Pessoa="+IdPessoa.ToString()+" and T1.Vencimento < Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) and T1.Status=0 ORDER BY T1.VENCIMENTO DESC");
            BindingSource Source_Atraso = new BindingSource();
            Source_Atraso.DataSource = TabAtraso;
            Source_Atraso.DataMember = TabAtraso.Tables[0].TableName;
            GridAtraso.DataSource = Source_Atraso;

            decimal VlrTotal = 0;
            for (int I = 0; I <= GridAtraso.RowCount - 1; I++)
                VlrTotal = VlrTotal + decimal.Parse(GridAtraso.Rows[I].Cells[4].Value.ToString());

            CreditoUsado = CreditoUsado + VlrTotal;
            LblVlrAtraso.Text = string.Format("{0:N2}", VlrTotal);

            // Titulos a vencer
            DataSet TabVencer = new DataSet();
            TabVencer = Controle.ConsultaTabela("SELECT T1.ID_LANC, CASE T1.PAGREC WHEN 1 THEN 'À Pagar' WHEN 2 THEN 'À Receber' ELSE ' ' END AS TIPOLANC,T1.NUMDOCUMENTO," +
                                                " T1.VENCIMENTO,T1.VLRORIGINAL,T1.REFERENTE,T3.DOCUMENTO AS TIPODOC, T4.FANTASIA AS FILIAL" +
                                                " FROM LANCFINANCEIRO T1 LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) LEFT JOIN TIPODOCUMENTO T3 ON (T3.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO)" +
                                                " LEFT JOIN EMPRESA_FILIAL T4 ON (T4.ID_FILIAL=T1.ID_FILIAL) Where t1.Id_Pessoa=" + IdPessoa.ToString() + " and T1.Vencimento >= Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) and T1.Status=0 ORDER BY T1.VENCIMENTO DESC");
            BindingSource Source_Vencer = new BindingSource();
            Source_Vencer.DataSource = TabVencer;
            Source_Vencer.DataMember = TabVencer.Tables[0].TableName;
            GridVencer.DataSource = Source_Vencer;
            VlrTotal = 0;
            for (int I = 0; I <= GridVencer.RowCount - 1; I++)
                VlrTotal = VlrTotal + decimal.Parse(GridVencer.Rows[I].Cells[4].Value.ToString());

            LblVlrVencer.Text = string.Format("{0:N2}", VlrTotal);
            CreditoUsado = CreditoUsado + VlrTotal;

            // Titulos Liquidados            
            DataSet TabLiq = new DataSet();
            TabLiq = Controle.ConsultaTabela("SELECT T1.ID_LANC, CASE T1.PAGREC WHEN 1 THEN 'À Pagar' WHEN 2 THEN 'À Receber' ELSE ' ' END AS TIPOLANC,T1.NUMDOCUMENTO," +
                                             " T1.VENCIMENTO,T1.VLRORIGINAL,T1.DTBAIXA,T1.VLRBAIXA,T1.REFERENTE,T3.DOCUMENTO AS TIPODOC" +
                                             " FROM LANCFINANCEIRO T1 LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) LEFT JOIN TIPODOCUMENTO T3 ON (T3.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO)" +
                                             " LEFT JOIN EMPRESA_FILIAL T4 ON (T4.ID_FILIAL=T1.ID_FILIAL) Where t1.Id_Pessoa=" + IdPessoa.ToString() + " and T1.Status=1 ORDER BY T1.VENCIMENTO DESC");
            BindingSource Source_Liq = new BindingSource();
            Source_Liq.DataSource = TabLiq;
            Source_Liq.DataMember = TabLiq.Tables[0].TableName;
            GridLiquidados.DataSource = Source_Liq;
            VlrTotal = 0;
            for (int I = 0; I <= GridLiquidados.RowCount - 1; I++)
                VlrTotal = VlrTotal + decimal.Parse(GridLiquidados.Rows[I].Cells[6].Value.ToString());
            LblPago.Text = string.Format("{0:N2}", VlrTotal);

            //Movimentos
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,T1.DATA,T7.DESCRICAO AS TIPOVENDA,CASE T1.STATUS WHEN 0 THEN 'Em Aberto' WHEN 1 THEN 'Confirmado' WHEN 2 THEN 'Faturado' WHEN 3 THEN 'Entregue' WHEN 4 THEN 'Cancelado' END AS STATUS," +
                                             " T1.NUMDOCUMENTO,T1.VLRTOTAL,T1.PREVENTREGA,T3.ENTREGADOR,T6.FANTASIA AS FILIAL,T1.FORMNF,T4.VENDEDOR,T1.DATACANCEL,T5.USUARIO,T8.FORMAPGTO,T1.TPVENDA FROM MVVENDA T1  " +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                             " LEFT JOIN ENTREGADORES T3 ON (T3.ID_ENTREGADOR=T1.ID_ENTREGADOR) " +
                                             " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                                             " LEFT JOIN USUARIOS T5 ON (T5.ID_USUARIO=T1.ID_USUARIO) " +
                                             " LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T1.ID_FILIAL)" +
                                             " LEFT JOIN TABELASAUX T7 ON (T7.CAMPO='VENDA' AND T7.CHAVE=T1.TPVENDA)" +
                                             " LEFT JOIN FORMAPAGAMENTO T8 ON (T8.ID_FORMAPGTO=T1.ID_FORMAPGTO)" +
                                             " WHERE T1.STATUS IN (2,3) AND T1.ID_PESSOA=" + IdPessoa.ToString() + " ORDER BY T1.ID_VENDA DESC");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridMov.DataSource = Source;
            LblDtUlt.Text    = "";
            LblVlrUlt.Text   = "0,00";
            LblVlrMedia.Text = "0,00";
            LblFormaPgto.Text = "";
            LblDtMaiorCompra.Text = ""; 
            VlrTotal = 0;
            if (GridMov.RowCount > 0)
            {
                LblDtUlt.Text = GridMov.Rows[0].Cells[1].Value.ToString();
                LblVlrUlt.Text = string.Format("{0:N2}",decimal.Parse(GridMov.Rows[0].Cells[5].Value.ToString()));
            }
            for (int I = 0; I <= GridMov.RowCount - 1; I++)
            {
                if (VlrTotal < decimal.Parse(GridMov.Rows[I].Cells[5].Value.ToString()))
                {
                    VlrTotal = decimal.Parse(GridMov.Rows[I].Cells[5].Value.ToString());
                    LblDtMaiorCompra.Text = GridMov.Rows[I].Cells[1].Value.ToString();
                    LblFormaPgto.Text     = GridMov.Rows[I].Cells[13].Value.ToString();
                }
                    
            }
            LblVlrMedia.Text = string.Format("{0:N2}", VlrTotal);                

            //if (VlrTotal > 0)
            ///   LblVlrMedia.Text = string.Format("{0:N2}", VlrTotal / GridMov.RowCount);
            PopularGridItens();

            //Lista de Produtos
            DataSet TabPrd = new DataSet();
            BindingSource Source_Prd = new BindingSource();
            TabPrd = Controle.ConsultaTabela(" SELECT DISTINCT T3.REFERENCIA,T3.DESCRICAO,T1.VLRUNITARIO FROM MvVendaItens T1 " +
                                             " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                             " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) " +
                                             " WHERE T1.TIPOITEM='S'" +
                                             " AND T2.STATUS IN (1,2,3)" +
                                             " AND T2.ID_PESSOA=" + IdPessoa.ToString() +
                                             " ORDER BY T3.DESCRICAO");
            Source_Prd.DataSource = TabPrd;
            Source_Prd.DataMember = TabPrd.Tables[0].TableName;
            GridPrd.DataSource = Source_Prd;

            //Movimento de Cheques            
            DataSet TabCheque = new DataSet();
            BindingSource Source_Cheque = new BindingSource();
            TabCheque = Controle.ConsultaTabela("SELECT T1.ID_LANC,CASE T1.STATUS WHEN 0 THEN ' ' WHEN 1 THEN 'Devolvido' END AS STATUS, T1.TITULAR,T1.DTVENCIMENTO,T1.VALOR,T1.NUMAGENCIA,T1.NUMCONTA,T1.NUMCHEQUE,T2.RAZAOSOCIAL AS RESPONSAVEL," +
                                                " T1.DTDESTINO,T3.RAZAOSOCIAL AS DESTINATARIO,T1.DOCUMVENDA,T4.FANTASIA AS FILIAL FROM MOVCHEQUEPRE T1" +
                                                " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                                " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T1.ID_PESSOADEST)" +
                                                " LEFT JOIN EMPRESA_FILIAL T4 ON (T4.ID_FILIAL=T1.ID_FILIAL) WHERE T1.ID_PESSOA=" + IdPessoa.ToString() +
                                                " AND (T1.DTVENCIMENTO >= Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) OR T1.STATUS=1) ORDER BY T1.DTVENCIMENTO");                                             
            Source_Cheque.DataSource = TabCheque;
            Source_Cheque.DataMember = TabCheque.Tables[0].TableName;
            GridCheque.DataSource = Source_Cheque;
            VlrTotal = 0;
            for (int I = 0; I <= GridCheque.RowCount - 1; I++)
                VlrTotal = VlrTotal + decimal.Parse(GridCheque.Rows[I].Cells[4].Value.ToString());
            CreditoUsado = CreditoUsado + VlrTotal;

            //Media de Atraso
            SqlDataReader TabMedia;
            TabMedia = Controle.ConsultaSQL("SELECT ISNULL(CONVERT(INT,MAX(DTBAIXA-VENCIMENTO)),0) as Dias FROM LANCFINANCEIRO WHERE STATUS=1 and ID_PESSOA="+IdPessoa.ToString());
            LblDias.Text="0";
            if (TabMedia.HasRows)
            {
                TabMedia.Read();
                LblDias.Text = TabMedia["Dias"].ToString().Trim();
            }
            LblCheque.Text = string.Format("{0:N2}", VlrTotal);
            //Limite disponivel            
            LblLimite.Text = string.Format("{0:N2}", LimiteCredito - CreditoUsado);
        }

        private void PopularGridItens()
        {            
             int IdVenda=0;
             if (GridMov.CurrentRow != null)            
                 IdVenda=int.Parse(GridMov.CurrentRow.Cells[0].Value.ToString());  
             DataSet TabItens = new DataSet();
             BindingSource Source_Itens = new BindingSource();
             TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T1.TIPOITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL " +
                                                " FROM MvVendaItens T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.Id_Venda=" + IdVenda.ToString());
             Source_Itens.DataSource = TabItens;
             Source_Itens.DataMember = TabItens.Tables[0].TableName;
             GridItens.DataSource    = Source_Itens;
        }

        private void GridMov_SelectionChanged(object sender, EventArgs e)
        {
            PopularGridItens();
        }
                
    }
}
