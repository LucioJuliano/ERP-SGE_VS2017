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
using System.IO;
using System.Collections;

namespace ERP_SGE
{
    public partial class FrmVinculoOE : Form
    {
        Funcoes Controle = new Funcoes();
        MvVenda Vendas = new MvVenda();                    
        public TelaPrincipal FrmPrincipal;
        public int IdPessoa = 0;
        public int IdPV  = 0;
        private int IdVenda = 0;


        public FrmVinculoOE()
        {
            InitializeComponent();
        }

        private void FrmVinculoOE_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Vendas.Controle  = Controle;
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,CASE T1.VINCULOVD WHEN '' THEN ' ' ELSE 'OK' END AS VINCULO,T1.DATA,T1.NUMDOCUMENTO,T1.VLRTOTAL,T2.VENDEDOR FROM MVVENDA T1" +
                                             " LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) WHERE T1.STATUS=3 AND T1.TPVENDA IN ('OE') AND T1.FATURADO=0 AND T1.ID_PESSOA=" + IdPessoa.ToString());
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Venda", IdVenda);
            Source.Position = item; 
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                IdVenda = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                if (GridDados.CurrentRow.Cells[1].Value.ToString() == "OK")
                {
                    Vendas.LerDados(IdVenda);
                    if (Vendas.VinculoVd!=string.Format("{0:D6}", IdPV.ToString()))
                    {
                        MessageBox.Show("Ordem de Entrega já vinculado a venda de No.: " + Vendas.VinculoVd, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                   
                    if (MessageBox.Show("Ordem de entrega já foi vinculada deseja continuar ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }                
                
                Controle.ExecutaSQL("Update MvVenda set VinculoVD='" + string.Format("{0:D6}", IdPV.ToString()) + "' WHERE ID_VENDA=" + IdVenda.ToString());
                IncluirItens();
                PopularGrid();
                GridDados.Focus();
                MessageBox.Show("Vinculo da ordem de entrega concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            CancelarVinculo();
        }

        private void IncluirItens()
        {
            MvVendaItens ItensVd = new MvVendaItens();
            ItensVd.Controle = Controle;
            DataSet ConsItens = new DataSet();
            ConsItens = Controle.ConsultaTabela("SELECT * FROM MVVENDAITENS WHERE ID_VENDA=" + IdVenda.ToString());
            if (ConsItens.Tables[0].Rows.Count > 0)
            {
                FrmPrincipal.BSta_BarProcesso.Maximum = ConsItens.Tables[0].Rows.Count;                
                for (int I = 0; I <= ConsItens.Tables[0].Rows.Count - 1; I++)
                {
                    ItensVd.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Item"].ToString()));
                    if (ItensVd.IdItem > 0)
                    {
                        ItensVd.IdItem = 0;
                        ItensVd.IdVenda = IdPV;
                        ItensVd.TipoItem = "N";
                        ItensVd.Vinculado = IdVenda;
                        ItensVd.GravarDados();
                    }
                    FrmPrincipal.BSta_BarProcesso.Maximum = FrmPrincipal.BSta_BarProcesso.Maximum + 1;
                }
            }            
        }
        private void CancelarVinculo()
        {
            if (GridDados.CurrentRow == null)
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                IdVenda = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                if (GridDados.CurrentRow.Cells[1].Value.ToString() != "OK")
                {
                    MessageBox.Show("Ordem de entrega não esta vinculada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }                
                Controle.ExecutaSQL("Update MvVenda set VinculoVD=' ' WHERE ID_VENDA=" + IdVenda.ToString());
                Controle.ExecutaSQL("Delete From MvVendaItens WHERE Vinculado=" + IdVenda.ToString());
                PopularGrid();
                GridDados.Focus();
                MessageBox.Show("Cancelamento do vinculo da ordem de entrega concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        
    }
}
