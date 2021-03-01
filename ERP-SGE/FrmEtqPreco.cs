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
    public partial class FrmEtqPreco : Form
    {
        Funcoes Controle = new Funcoes();
        Produtos CadPrd  = new Produtos();

        private BindingSource Source_Itens;
        public TelaPrincipal  FrmPrincipal;
        private DataTable     TabLancItens;

        private DataTable CriarTabela()
        {
            DataTable Tabela = new DataTable();
            Tabela.Columns.Add("Id_Produto", Type.GetType("System.Int32"));
            Tabela.Columns.Add("Referencia", Type.GetType("System.String"));
            Tabela.Columns.Add("Descricao",  Type.GetType("System.String"));            
            Tabela.Columns.Add("Preco",      Type.GetType("System.Decimal"));
            Tabela.Columns.Add("Estoque",    Type.GetType("System.Decimal"));
            Tabela.Columns.Add("CodBarra",   Type.GetType("System.String"));
            Tabela.Columns.Add("Qtde",       Type.GetType("System.Int32"));
            Tabela.Columns.Add("Und",        Type.GetType("System.String"));
            return Tabela;
        }

        public FrmEtqPreco()
        {
            InitializeComponent();
        }

        private void FrmEtqPreco_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            CadPrd.Controle  = Controle;
            Source_Itens     = new BindingSource();
            TabLancItens     = CriarTabela();
            TxtQtdeEtq.Value = 5;
            Rb_ImpArgox.Checked = true;
            Rb_PrcSensac.Checked = true;            
            LstGrupo = FrmPrincipal.PopularCombo("SELECT Id_Grupo,Grupo FROM GrupoProduto where ativo=1 ORDER BY Grupo", LstGrupo, "Nenhum");            
        }

        private void BtnImpGrupo_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstGrupo.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor Selecionar um Grupo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Confirma a importação do Grupo: " + LstGrupo.Text.ToString().Trim(), "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TabLancItens= CriarTabela();
                Source_Itens.DataSource = TabLancItens;
                Source_Itens.DataMember = TabLancItens.TableName;
                GridDados.DataSource    = Source_Itens;
                GridDados.Refresh();

                SqlDataReader Tab = Controle.ConsultaSQL("SELECT ID_PRODUTO,REFERENCIA,DESCRICAO,PRCSENSACIONAL,PRCESPECIAL,PRCVAREJO,PRCMINIMO,PRCATACADO,CODBARRA,SALDOESTOQUE,UNIDADE FROM PRODUTOS WHERE ATIVO=1 AND ID_GRUPO=" + LstGrupo.SelectedValue.ToString());
                if (Tab.HasRows)
                {

                    decimal PrcItem = 0;
                    while (Tab.Read())
                    {
                        PrcItem = 0;
                        if (Rb_PrcSensac.Checked)
                            PrcItem = decimal.Parse(Tab["PRCSENSACIONAL"].ToString());
                        else if (Rb_PrcEspecial.Checked)
                            PrcItem = decimal.Parse(Tab["PRCESPECIAL"].ToString());
                        else if (Rb_PrcVarejo.Checked)
                            PrcItem = decimal.Parse(Tab["PRCVAREJO"].ToString());
                        else if (Rb_PrcMinimo.Checked)
                            PrcItem = decimal.Parse(Tab["PRCMINIMO"].ToString());

                        TabLancItens.Rows.Add(int.Parse(Tab["ID_PRODUTO"].ToString()), Tab["REFERENCIA"].ToString().Trim(), Tab["DESCRICAO"].ToString().Trim(), PrcItem, decimal.Parse(Tab["SALDOESTOQUE"].ToString()),Tab["CODBARRA"].ToString(), int.Parse(TxtQtdeEtq.Value.ToString()), Tab["UNIDADE"].ToString().Trim());
                        Source_Itens.DataSource = TabLancItens;
                        Source_Itens.DataMember = TabLancItens.TableName;
                        GridDados.DataSource    = Source_Itens;
                        GridDados.Refresh(); 
                    }
                }
            }
        }

        private void GridDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void BtnBuscaPrd_Click(object sender, EventArgs e)
        {
            FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
            BuscaPrd.FrmPrincipal = this.FrmPrincipal;
            BuscaPrd.IdProduto = 0;
            BuscaPrd.ShowDialog();
            decimal PrcItem = 0;
            if (BuscaPrd.IdProduto > 0)
            {
                TxtCodPrd.Text = BuscaPrd.CadProd.IdProduto.ToString();
                TxtDescricao.Text = BuscaPrd.CadProd.Descricao;

                PrcItem = 0;
                if (Rb_PrcSensac.Checked)
                    PrcItem = BuscaPrd.CadProd.PrcSensacional;
                else if (Rb_PrcEspecial.Checked)
                    PrcItem = BuscaPrd.CadProd.PrcEspecial;
                else if (Rb_PrcVarejo.Checked)
                    PrcItem = BuscaPrd.CadProd.PrcVarejo;
                else if (Rb_PrcMinimo.Checked)
                    PrcItem = BuscaPrd.CadProd.PrcMinimo;

                TabLancItens.Rows.Add(BuscaPrd.CadProd.IdProduto, BuscaPrd.CadProd.Referencia.Trim(), BuscaPrd.CadProd.Descricao.Trim(), PrcItem, BuscaPrd.CadProd.SaldoEstoque, BuscaPrd.CadProd.CodBarra.ToString(), int.Parse(TxtQtdeEtq.Value.ToString()),BuscaPrd.CadProd.Unidade);
                Source_Itens.DataSource = TabLancItens;
                Source_Itens.DataMember = TabLancItens.TableName;
                GridDados.DataSource = Source_Itens;
                GridDados.Refresh();
            }
            else
                TxtCodPrd.Text = "0";  
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Confirma a Exclusão do Item ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GridDados.Rows.Remove(GridDados.CurrentRow);
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                MessageBox.Show("Não existe Item para Imprimir", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Confirma a Impressão do Item ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Rb_ImpArgox.Checked)
                {
                    Controles.ImprimirEtqCodBarra ImpEtq = new ImprimirEtqCodBarra();
                    ImpEtq.VersaoDistr = FrmPrincipal.VersaoDistribuidor;
                    ImpEtq.ImprimirEtq(TabLancItens);
                }
                if (Rb_ImpDr800.Checked)
                    ImpDr800(TabLancItens);


            }
        }
        private void ImpDr800(DataTable TabEtq)
        {
            ImpressoraFiscal ImpDr800 = new ImpressoraFiscal();
            ImpDr800.InicializarDarumaDR800();
            ImpDr800.ImpDR800(TabEtq);
        }
    }
}
