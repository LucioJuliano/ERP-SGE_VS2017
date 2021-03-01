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

namespace ERP_SGE
{
    public partial class FrmMostrarEstMin : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;

        public FrmMostrarEstMin()
        {
            InitializeComponent();
        }

        private void FrmMostrarEstMin_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            DataSet Tabela = new DataSet();
            string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,GRPPRD.GRUPO, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                          " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,(SELECT TOP 1 PED1.PREVENTREGA FROM PEDCOMPRA PED1 " +
                          " LEFT JOIN PEDCOMPRAITENS PED2 ON (PED2.ID_DOCUMENTO=PED1.ID_DOCUMENTO) WHERE PED2.ID_PRODUTO=T1.ID_PRODUTO AND PED1.STATUS=1 ORDER BY PED1.PREVENTREGA) AS PREVENTREGA,T1.ESTMINIMO " +
                          " FROM Produtos T1 LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) WHERE T1.PRODUTOKIT=0 AND T1.Ativo=1 AND GRPPRD.LISTAESTMIN=1 AND T1.SALDOESTOQUE <= T1.ESTMINIMO ORDER BY GRPPRD.GRUPO,T1.DESCRICAO";
            Tabela = Controle.ConsultaTabela(sSQL);
            GridDados.DataSource = Tabela;
            GridDados.DataMember = Tabela.Tables[0].TableName;

            if (Tabela.Tables[0].Rows.Count == 0)
                Close();
        }
    }
}
