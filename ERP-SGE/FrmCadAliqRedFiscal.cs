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
    public partial class FrmCadAliqRedFiscal : Form
    {

        Funcoes Controle = new Funcoes();
        ReducaoFiscal RedFiscal = new ReducaoFiscal();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        
        private DataSet TabItens;
        private BindingSource Source_Itens;

        public FrmCadAliqRedFiscal()
        {
            InitializeComponent();
        }

        private void FrmCadAliqRedFiscal_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            RedFiscal.Controle = Controle;
            TabItens = new DataSet();
            Source_Itens = new BindingSource();            
            PopularGrid();
        }

        private void PopularGrid()
        {
            TabItens = Controle.ConsultaTabela("SELECT ID_REDUCAO,CODRED,REFREDUCAO,PERC FROM REDUCAOFISCAL");
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("ID_Reducao", RedFiscal.IdReducao);
            Source_Itens.Position = item; 
        }

        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    IncluirItem();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {   
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal Perc = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
            RedFiscal.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
            RedFiscal.CodRed     = GridItens.CurrentRow.Cells[1].Value.ToString();
            RedFiscal.RefReducao = GridItens.CurrentRow.Cells[2].Value.ToString();
            RedFiscal.Perc       = Perc;
            RedFiscal.GravarDados();
            PopularGrid();
            GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
            
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            IncluirItem();
            
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RedFiscal.IdReducao = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                RedFiscal.Excluir();
                RedFiscal.IdReducao = 0;
                PopularGrid();
            }
        }
        private void IncluirItem()
        {
            RedFiscal.LerDados(0);
            RedFiscal.GravarDados();
            PopularGrid();
            GridItens.CurrentCell = GridItens.CurrentRow.Cells[1];
        }

    }
}
