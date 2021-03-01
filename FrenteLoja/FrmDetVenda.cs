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

namespace FrenteLoja
{
    public partial class FrmDetVenda : Form
    {
        Funcoes Controle = new Funcoes();
        public FrmFrenteLoja FrmFrenteLoja;
        public int IdVenda=0;

        public FrmDetVenda()
        {
            InitializeComponent();
        }

        private void FrmDetVenda_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmFrenteLoja.FrmPrincipal.Conexao;
            PopularItens();
            PopularFinanceiro();
        }
        private void PopularItens()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL " +
                                                       " FROM MvVendaItens T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.Id_Venda=" + IdVenda.ToString());
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridItens.DataSource = Source;
        }
        private void PopularFinanceiro()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.NUMDOCUMENTO,T2.DOCUMENTO,T1.VENCIMENTO,T1.VLRORIGINAL,T1.VLRBAIXA FROM LANCFINANCEIRO T1 " +
                                             " LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE Id_Venda > 0 and Id_Venda=" + IdVenda.ToString());
            GridFinanc.DataSource = Tabela;
            GridFinanc.DataMember = Tabela.Tables[0].TableName;
        }
    }
}
