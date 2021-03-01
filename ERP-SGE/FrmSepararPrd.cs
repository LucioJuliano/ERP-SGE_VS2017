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
    public partial class FrmSepararPrd : Form
    {
        Funcoes Controle = new Funcoes();        
        MvVenda Vendas = new MvVenda();
        private BindingSource Source_Lanc;
        public TelaPrincipal FrmPrincipal;
        private DataTable TabLancVenda;
        

        public FrmSepararPrd()
        {
            InitializeComponent();
        }
        private DataTable CriarTabela()
        {
            DataTable Tabela = new DataTable();
            Tabela.Columns.Add("Id_Venda", Type.GetType("System.Int32"));
            Tabela.Columns.Add("NumDocumento", Type.GetType("System.String"));
            Tabela.Columns.Add("Pessoa", Type.GetType("System.String"));            
            return Tabela;
        }

        private void FrmSepararPrd_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Vendas.Controle  = Controle;            
            Source_Lanc      = new BindingSource();
            TabLancVenda     = CriarTabela();
            TxtIdVenda.Text = "";
            TxtIdVenda.Focus();
        }

        private void TxtIdVenda_Validated(object sender, EventArgs e)
        {
            if (TxtIdVenda.Text.Trim() == "")            
                return;
            
            Vendas.LerDados(int.Parse(TxtIdVenda.Value.ToString()));

            if (Vendas.IdVenda == 0)
            {
                MessageBox.Show("Atenção Venda não Localizada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtIdVenda.Text = "";
                TxtIdVenda.Focus();
                return;
            }
            else
            {
               /* if (Vendas.Status != 2 && Vendas.TpVenda!="OE")
                {
                    MessageBox.Show("Atenção Venda não esta faturada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }*/

                if (Vendas.TpVenda == "OE" && (Vendas.Status==1 || Vendas.Status == 4))
                {
                    MessageBox.Show("Atenção Ordem de Entrega não esta Faturada ou Entregue", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }

                if (GridDados.CurrentRow != null)
                {
                    if (Source_Lanc.Find("Id_Venda", Vendas.IdVenda) > -1)
                    {
                        MessageBox.Show("Venda ja foi informada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TxtIdVenda.Text = "";
                        TxtIdVenda.Focus();                 
                        return;
                    }
                }
            
                TxtPessoa.Text = Vendas.NmPessoa.Trim();
                TabLancVenda.Rows.Add(Vendas.IdVenda, Vendas.NumDocumento, Vendas.NmPessoa);
                Source_Lanc.DataSource = TabLancVenda;
                Source_Lanc.DataMember = TabLancVenda.TableName;
                GridDados.DataSource = Source_Lanc;
                GridDados.Refresh();                
                TxtIdVenda.Text = "";
                TxtIdVenda.Focus();                 
            }
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {            
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GridDados.Rows.Remove(GridDados.CurrentRow);
            }
        }

        private void BtnNovoMapa_Click(object sender, EventArgs e)
        {
            TabLancVenda = CriarTabela();
            Source_Lanc.DataSource = TabLancVenda;
            Source_Lanc.DataMember = TabLancVenda.TableName;
            GridDados.DataSource = Source_Lanc;
            GridDados.Refresh();
            TxtIdVenda.Text = "";
            TxtPessoa.Text  = "";
            TxtIdVenda.Focus(); 

        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                MessageBox.Show("Não existe Registro para Imprimir", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string NumVendas = "";
            string NumImp = "";
            for (int I = 0; I <= GridDados.RowCount - 1; I++)
            {
                if (NumVendas == "")
                {
                    NumVendas = GridDados.Rows[I].Cells[0].Value.ToString();
                    NumImp    = GridDados.Rows[I].Cells[0].Value.ToString();
                }
                else
                {
                    NumVendas = NumVendas + "," + GridDados.Rows[I].Cells[0].Value.ToString();
                    NumImp    = NumImp + " / " + GridDados.Rows[I].Cells[0].Value.ToString();
                }
            }
            string sSQL = "SELECT T2.ID_PRODUTO,T2.REFERENCIA,T2.DESCRICAO,T2.LOCESTRUA,T2.PALETE, SUM(T1.QTDE) AS QTDE FROM MVVENDAITENS T1 " +
                          " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                          "  WHERE T1.TIPOITEM='S' AND T1.ID_VENDA IN (" + NumVendas + ")" +
                          " GROUP BY T2.ID_PRODUTO,T2.REFERENCIA,T2.DESCRICAO,T2.LOCESTRUA,T2.PALETE" +
                          " ORDER BY T2.DESCRICAO";
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelSeparacaoPrd Rel001 = new Relatorios.RelSeparacaoPrd();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(sSQL);
            Rel001.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = Rel001;            
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblVendas"])).Text = "Vendas: " + NumImp;            
            FrmRel.ShowDialog();
        }
    }
}
