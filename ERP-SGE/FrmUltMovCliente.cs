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
    public partial class FrmUltMovCliente : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        public int IdPessoa       = 0;
        public MvVenda Vendas;
        public Pessoas CadPessoa;
        

        public FrmUltMovCliente()
        {
            InitializeComponent();
        }

        private void FrmUltMovCliente_Load(object sender, EventArgs e)
        {
            Controle.Conexao   = FrmPrincipal.Conexao;                        
            //PopularGrids();
        }

        private void PopularGrids()
        {            
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
                                             " WHERE T1.STATUS=3 AND T1.TPVENDA='PV' AND T1.ID_PESSOA=" + IdPessoa.ToString() + " ORDER BY T1.ID_VENDA DESC");

            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridMov.DataSource = Source;
            //
            PopularGridItens();            
        }

        private void PopularGridItens()
        {
            int IdVenda = 0;
            if (GridMov.CurrentRow != null)
                IdVenda = int.Parse(GridMov.CurrentRow.Cells[0].Value.ToString());
            DataSet TabItens = new DataSet();
            BindingSource Source_Itens = new BindingSource();
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T1.TIPOITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL " +
                                               " FROM MvVendaItens T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.Id_Venda=" + IdVenda.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
        }

        private void GridMov_SelectionChanged(object sender, EventArgs e)
        {
            PopularGridItens();
        }

        private void BtnImportar_Click(object sender, EventArgs e)
        {
            int IdVenda = 0;
            if (GridMov.CurrentRow != null)
                IdVenda = int.Parse(GridMov.CurrentRow.Cells[0].Value.ToString());

            bool AtlzPrc = false;

            if (MessageBox.Show("Confirma o movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (MessageBox.Show("Deseja Atualizar os Preços ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    AtlzPrc = true;

                //Incluindo os Itens                    
                MvVendaItens ItensVdDestino = new MvVendaItens(); ItensVdDestino.Controle = Controle;
                Produtos CadPrd = new Produtos(); CadPrd.Controle = Controle;
                DataSet TabItens = new DataSet();
                TabItens = Controle.ConsultaTabela("SELECT * FROM MvVendaItens WHERE Id_Venda=" + IdVenda.ToString());
                decimal VlrSubTotal = 0;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    //TabItens.Read();
                    CadPrd.LerDados(int.Parse(TabItens.Tables[0].Rows[I]["Id_Produto"].ToString()));

                    ItensVdDestino.IdItem      = 0;
                    ItensVdDestino.IdVenda     = Vendas.IdVenda;
                    ItensVdDestino.TipoItem    = TabItens.Tables[0].Rows[I]["TipoItem"].ToString();
                    ItensVdDestino.IdProduto   = int.Parse(TabItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                    ItensVdDestino.Qtde        = decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString());
                    ItensVdDestino.VlrUnitario = decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString());

                    if (AtlzPrc)
                    {
                        if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6)
                            ItensVdDestino.VlrUnitario = CadPrd.PrcAtacado;
                        else
                            ItensVdDestino.VlrUnitario = CadPrd.PrcEspecial;
                    }
                    else
                    {
                        if (CadPessoa.Clie_Forn == 3 && CadPessoa.Clie_Forn == 6)
                        {
                            if (decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString()) < CadPrd.PrcAtacado)
                                ItensVdDestino.VlrUnitario = CadPrd.PrcAtacado;
                        }
                        else
                        {
                            if (decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString()) < CadPrd.PrcMinimo)
                                ItensVdDestino.VlrUnitario = CadPrd.PrcMinimo;
                        }
                    }
                    ItensVdDestino.PComissao      = 0;
                    ItensVdDestino.VlrComissao    = 0;
                    ItensVdDestino.PDesconto      = 0;
                    ItensVdDestino.VlrUntComissao = 0;
                    ItensVdDestino.PrcCusto       = CadPrd.UltPrcCompra;
                    ItensVdDestino.PrcEspecial    = CadPrd.PrcEspecial;
                    ItensVdDestino.PrcMinimo      = CadPrd.PrcMinimo;
                    ItensVdDestino.PrcVarejo      = CadPrd.PrcVarejo;
                    ItensVdDestino.PrcAtacado     = CadPrd.PrcAtacado;
                    ItensVdDestino.VlrTotal       = ItensVdDestino.VlrUnitario * ItensVdDestino.Qtde;

                    if (CadPrd.SaldoEstoque <= 0 && ItensVdDestino.TipoItem == "S")
                    {
                        MessageBox.Show("Produto:" + CadPrd.Descricao.Trim() + " não tem saldo Suficiente no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ItensVdDestino.Qtde = 0;
                        ItensVdDestino.VlrTotal = ItensVdDestino.VlrUnitario * ItensVdDestino.Qtde;
                    }

                    ItensVdDestino.GravarDados();

                    VlrSubTotal = VlrSubTotal + ItensVdDestino.VlrTotal;

                    Vendas.VlrSubTotal = VlrSubTotal;
                    Vendas.VlrTotal = VlrSubTotal - (Vendas.VlrDesconto + Vendas.VlrCredito);
                    if (Vendas.IdVenda > 0)
                    {
                        Vendas.IdVdMaster = Vendas.IdVenda;
                        Vendas.GravarDados();
                    }
                }
                MessageBox.Show("Movimento concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void FrmUltMovCliente_Shown(object sender, EventArgs e)
        {
            PopularGrids();
        }
    }
}
