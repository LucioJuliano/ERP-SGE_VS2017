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
    public partial class FrmAcertoEstoque : Form
    {
        Funcoes Controle = new Funcoes();        
        public TelaPrincipal FrmPrincipal;

        public FrmAcertoEstoque()
        {
            InitializeComponent();
        }
        private void FrmAcertoEstoque_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            ListaGrupo = FrmPrincipal.PopularCombo("SELECT Id_Grupo,Grupo FROM GrupoProduto ORDER BY Grupo", ListaGrupo, "Todos");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sFiltro = "";

            if (int.Parse(ListaGrupo.SelectedValue.ToString()) > 0)
                sFiltro = " AND PRD.ID_GRUPO=" + ListaGrupo.SelectedValue.ToString();

            string sSQL = "SELECT PRD.ID_PRODUTO,PRD.DESCRICAO,PRD.REFERENCIA,PRD.SALDOESTOQUE," +
                 "((SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVESTOQUEITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM)" +
                 " LEFT JOIN MVESTOQUE T5 ON (T5.ID_MOV=T4.ID_MOV)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='TPMVEST' AND T3.ESTOQUE=1 AND T1.ID_Produto=PRD.ID_PRODUTO)" +
                 " + (SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVVENDAITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM AND T4.TIPOITEM IN ('S','E'))" +
                 " LEFT JOIN MVVENDA T5 ON (T5.ID_VENDA=T4.ID_VENDA)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='VENDA' AND T1.ID_Produto=PRD.ID_PRODUTO AND T4.TIPOITEM='E'))" +
                 " - ((SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVESTOQUEITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM)" +
                 " LEFT JOIN MVESTOQUE T5 ON (T5.ID_MOV=T4.ID_MOV)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='TPMVEST' AND T3.ESTOQUE=2 AND T1.ID_Produto=PRD.ID_PRODUTO)" +
                 " + (SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVVENDAITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM AND T4.TIPOITEM IN ('S','E'))" +
                 " LEFT JOIN MVVENDA T5 ON (T5.ID_VENDA=T4.ID_VENDA)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='VENDA' AND T1.ID_Produto=PRD.ID_PRODUTO AND T4.TIPOITEM='S')" +
                 " ) AS SALDOEXTRATO  " +
                 " FROM PRODUTOS PRD" +
                 "  LEFT JOIN GRUPOPRODUTO GRP ON (GRP.ID_GRUPO=PRD.ID_GRUPO)" +
                 " WHERE PRD.PRODUTOKIT=0 AND PRD.ATIVO=1 AND PRD.SALDOESTOQUE <>" +
                 " ((SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVESTOQUEITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM)" +
                 " LEFT JOIN MVESTOQUE T5 ON (T5.ID_MOV=T4.ID_MOV)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='TPMVEST' AND T3.ESTOQUE=1 AND T1.ID_Produto=PRD.ID_PRODUTO)" +
                 " + (SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVVENDAITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM AND T4.TIPOITEM IN ('S','E'))" +
                 " LEFT JOIN MVVENDA T5 ON (T5.ID_VENDA=T4.ID_VENDA)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='VENDA'  AND T1.ID_Produto=PRD.ID_PRODUTO AND T4.TIPOITEM='E')" +
                 " )-((SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVESTOQUEITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM)" +
                 " LEFT JOIN MVESTOQUE T5 ON (T5.ID_MOV=T4.ID_MOV)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='TPMVEST' AND T3.ESTOQUE=2 AND T1.ID_Produto=PRD.ID_PRODUTO)" +
                 " + (SELECT ISNULL(SUM(T1.QTDE),0) FROM EXTRATOESTOQUE T1" +
                 " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                 " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                 " LEFT JOIN MVVENDAITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM AND T4.TIPOITEM IN ('S','E'))" +
                 " LEFT JOIN MVVENDA T5 ON (T5.ID_VENDA=T4.ID_VENDA)" +
                 " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                 " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                 " WHERE T3.CAMPO='VENDA'  AND T1.ID_Produto=PRD.ID_PRODUTO AND T4.TIPOITEM='S'))" + sFiltro +
                 " ORDER BY PRD.ID_PRODUTO";
            DataSet Tabela = new DataSet();            
            Tabela = Controle.ConsultaTabela(sSQL);
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            MessageBox.Show("Pesquisa Concluida");
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            progressBar1.Maximum = GridDados.SelectedRows.Count;
            progressBar1.Value = 0;
            for (int G = 0; G <= GridDados.SelectedRows.Count - 1; G++)
            {
                int IdPrd = 0;
                IdPrd = int.Parse(GridDados.SelectedRows[G].Cells[0].Value.ToString());
                label1.Text = GridDados.SelectedRows[G].Cells[0].Value.ToString();
                Application.DoEvents();

                Controle.ExecutaSQL("DELETE FROM SALDOESTOQUE  WHERE ID_PRODUTO in (" + IdPrd.ToString() + ")");
                string sSql = "SELECT T1.ID_ITEM,T1.ID_PRODUTO,T7.GRUPO,T1.DTMOVIM,T1.QTDE,T1.TPMOV,T1.VLRUNITARIO,T2.REFERENCIA,T2.DESCRICAO,T3.ESTOQUE,T3.DESCRICAO AS TIPOMOVIMENTO," +
                      "   T6.RAZAOSOCIAL,T5.DOCUMENTO,T5.NUMDOCUMENTO " +
                      " FROM EXTRATOESTOQUE T1" +
                      " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                      " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                      " LEFT JOIN MVESTOQUEITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM)" +
                      " LEFT JOIN MVESTOQUE T5 ON (T5.ID_MOV=T4.ID_MOV)" +
                      " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                      " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                      " WHERE T3.CAMPO='TPMVEST'" +
                      " AND T1.ID_Produto=" + IdPrd.ToString() +
                      " UNION" +
                      " SELECT T1.ID_ITEM,T1.ID_PRODUTO,T7.GRUPO,T1.DTMOVIM,T1.QTDE,T1.TPMOV,T1.VLRUNITARIO,T2.REFERENCIA,T2.DESCRICAO," +
                      "        CASE T4.TIPOITEM WHEN 'E' THEN 1 ELSE 2 END AS Estoque,T3.DESCRICAO AS TIPOMOVIMENTO," +
                      "        T6.RAZAOSOCIAL,CONVERT(CHAR,T5.ID_VENDA) AS DOCUMENTO,T5.NUMDOCUMENTO" +
                      " FROM EXTRATOESTOQUE T1" +
                      " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                      " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                      " LEFT JOIN MVVENDAITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM AND T4.TIPOITEM IN ('S','E'))" +
                      " LEFT JOIN MVVENDA T5 ON (T5.ID_VENDA=T4.ID_VENDA)" +
                      " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                      " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                      " WHERE T3.CAMPO='VENDA' " +
                      " AND T1.ID_Produto=" + IdPrd.ToString() +
                      " ORDER BY DTMOVIM,ID_ITEM";
                SqlDataReader Tab = Controle.ConsultaSQL(sSql);
                Controles.ControleEstoque Estoque = new ControleEstoque();
                Estoque.Controle = Controle;
                while (Tab.Read())
                {
                    if (Tab["Estoque"].ToString() == "1")
                        Estoque.Atlz_SaldoEstoque("E", "I", int.Parse(Tab["Id_Produto"].ToString()), DateTime.Parse(Tab["DtMovim"].ToString()), decimal.Parse(Tab["Qtde"].ToString()));
                    else
                        Estoque.Atlz_SaldoEstoque("S", "I", int.Parse(Tab["Id_Produto"].ToString()), DateTime.Parse(Tab["DtMovim"].ToString()), decimal.Parse(Tab["Qtde"].ToString()));
                }
                progressBar1.Value = progressBar1.Value + 1;
            }            
            button2.Enabled = true;
            MessageBox.Show("Ok Fim");
        }        
    }
}
