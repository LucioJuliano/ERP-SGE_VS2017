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
using System.Collections;
using System.Data.SqlClient;

namespace ERP_SGE
{
    public partial class FrmConsMovPrd : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public Produtos CadProd;
        public int IdProduto = 0;

        public FrmConsMovPrd()
        {
            InitializeComponent();
        }

        private void FrmConsMovPrd_Load(object sender, EventArgs e)
        {
            CadProd = new Produtos();
            Controle.Conexao = FrmPrincipal.Conexao;            
            CadProd.Controle = Controle;
            Dt1.Value = DateTime.Now.AddDays(-30);
            Dt2.Value = DateTime.Now;
            CadProd.LerDados(0);
            LstTpMv = FrmPrincipal.PopularCombo("SELECT CHAVE,SUBSTRING(DESCRICAO,1,30) AS DESCRICAO FROM TABELASAUX  WHERE ESTOQUE<>0 or CAMPO='TPMVEST' ORDER BY DESCRICAO", LstTpMv, "TODOS");
        }
        private void PopularGrid()
        {   
            DataSet Tabela = new DataSet();
            string sSQL = "SELECT T2.ID_MOV AS LANC,T2.DTENTSAI AS DATA,T4.DESCRICAO AS DESCMOV,T4.ESTOQUE,T2.DOCUMENTO,T2.NUMFORMULARIO AS NUMDOC,CASE T4.ESTOQUE WHEN 1 THEN T1.QTDE ELSE 0 END AS ENTRADA,CASE T4.ESTOQUE WHEN 2 THEN T1.QTDE ELSE 0 END AS SAIDA,T1.VLRUNITARIO,T1.P_IPI,T5.RAZAOSOCIAL,T6.FANTASIA AS FILIAL FROM MVESTOQUEITENS T1" +
                          "  LEFT JOIN MVESTOQUE T2 ON (T2.ID_MOV=T1.ID_MOV) " +
                          "  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) " +
                          "  LEFT JOIN TABELASAUX T4 ON (T4.CHAVE=T2.TPMOV)  " +
                          "  LEFT JOIN PESSOAS T5 ON (T5.ID_PESSOA=T2.ID_PESSOA) " +
                          "  LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T2.ID_FILIALORIGDEST) " +
                          " WHERE T1.ID_PRODUTO=" + TxtCodPrd.Text + " AND (T4.ESTOQUE<>0 OR T4.CAMPO='TPMVEST') AND T2.STATUS=1 " +
                          "  AND T2.DTENTSAI >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DTENTSAI <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
            if (LstTpMv.SelectedValue.ToString() != "0")
                sSQL = sSQL + "AND T2.TPMOV='" + LstTpMv.SelectedValue.ToString() + "'";

            sSQL = sSQL + "UNION " +
                          "SELECT T2.ID_VENDA AS LANC,T2.PREVENTREGA AS DATA,T4.DESCRICAO AS DESCMOV,T4.ESTOQUE,'',T2.NUMDOCUMENTO AS NUMDOC,CASE T1.TIPOITEM WHEN 'E' THEN T1.QTDE ELSE 0 END AS ENTRADA,CASE T1.TIPOITEM WHEN 'S' THEN T1.QTDE ELSE 0 END AS SAIDA,T1.VLRUNITARIO,0 AS P_IPI,T5.RAZAOSOCIAL,T6.FANTASIA AS FILIAL FROM MVVENDAITENS T1" +
                          "  LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                          "  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) " +
                          "  LEFT JOIN TABELASAUX T4 ON (T4.CHAVE=T2.TPVENDA) " +
                          "  LEFT JOIN PESSOAS T5 ON (T5.ID_PESSOA=T2.ID_PESSOA) " +
                          "  LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T2.ID_FILIAL) " +
                          " WHERE T1.ID_PRODUTO=" + TxtCodPrd.Text + " AND (T4.ESTOQUE<>0 OR T4.CAMPO='TPMVEST') AND T2.STATUS IN (1,2,3) " +
                          "  AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
            if (LstTpMv.SelectedValue.ToString() != "0")
                sSQL = sSQL + "AND T2.TPVENDA='" + LstTpMv.SelectedValue.ToString() + "'";

            sSQL = sSQL + " ORDER BY 2 DESC";

            
            Tabela = Controle.ConsultaTabela(sSQL);
            GridDados.DataSource = Tabela;
            GridDados.DataMember = Tabela.Tables[0].TableName;
            if (GridDados.CurrentRow != null)
                GridDados.Focus();

        }

        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }

        private void FrmConsMovPrd_Shown(object sender, EventArgs e)
        {
            if (IdProduto > 0)
            {
                CadProd.LerDados(IdProduto);
                LblSaldo.Text     = string.Format("{0:N3}", CadProd.SaldoEstoque);
                TxtCodPrd.Text    = CadProd.IdProduto.ToString();
                TxtDescricao.Text = CadProd.Descricao;
                PopularGrid();
            }
        }

        private void BtnBuscaPrd_Click(object sender, EventArgs e)
        {
            FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
            BuscaPrd.FrmPrincipal = this.FrmPrincipal;
            BuscaPrd.IdProduto = 0;
            BuscaPrd.ShowDialog();
            if (BuscaPrd.IdProduto > 0)
            {
                TxtCodPrd.Text    = BuscaPrd.CadProd.IdProduto.ToString();
                TxtDescricao.Text = BuscaPrd.CadProd.Descricao;
                LblSaldo.Text     = string.Format("{0:N3}", CadProd.SaldoEstoque);
            }
            else
            {
                LblSaldo.Text     = "0,000";
                TxtCodPrd.Text    = "0";
                TxtDescricao.Text = "";
            }
        }

        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
            {
                if (e.Value.ToString() == "0,000")
                    e.Value = "";
            }
        }        
    }
}
