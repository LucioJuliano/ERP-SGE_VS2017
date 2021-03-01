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
using System.Data.Sql;
using System.Data.SqlClient;


namespace ERP_SGE
{
    public partial class FrmFormaPgtoVd : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public MvVenda Vendas;



        public FrmFormaPgtoVd()
        {
            InitializeComponent();
        }

        private void FrmFormaPgtoVd_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            LstFormaPgto     = FrmPrincipal.PopularCombo("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento ORDER BY FormaPgto", LstFormaPgto);

        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstFormaPgto.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor colocar a forma de pagamento", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                return;
            }
            
            Vendas.LerDados(Vendas.IdVenda);
            if (Vendas.Status == 3)
            {
                MessageBox.Show("Movimento foi Entregue", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }
            if (Vendas.Status == 4)
            {
                MessageBox.Show("Movimento Cancelado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            if (MessageBox.Show("Confirma essa forma de pagamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Vendas.IdFormaPgto = int.Parse(LstFormaPgto.SelectedValue.ToString());
                Vendas.PrazoPgto   = TxtPrazoPgto.Text;
                Vendas.Status      = 1;
                Vendas.GravarDados();
                Controle.ExecutaSQL("Delete from LancFinanceiro where id_venda=" + Vendas.IdVenda.ToString());
                Close();

            }
        }

        private void FrmFormaPgtoVd_Shown(object sender, EventArgs e)
        {
            LstFormaPgto.SelectedValue = Vendas.IdFormaPgto;
            TxtPrazoPgto.Text = Vendas.PrazoPgto;
        }

    }
}
