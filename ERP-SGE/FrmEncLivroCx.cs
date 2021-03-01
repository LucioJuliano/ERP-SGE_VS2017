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
    public partial class FrmEncLivroCx : Form
    {
        Funcoes Controle = new Funcoes();        
        public TelaPrincipal FrmPrincipal;

        public FrmEncLivroCx()
        {
            InitializeComponent();
        }

        private void FrmEncLivroCx_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Dt1.Value = DateTime.Now;
            LstCaixa = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstCaixa);
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstCaixa.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor informar o Caixa Financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Confirma o Encerramento do Dia ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Controle.ExecutaSQL("Update SaldoContaCaixa Set Status=1 Where Id_Caixa="+LstCaixa.SelectedValue.ToString()+" AND  Data = Convert(DateTime,'" + Dt1.Value.Date.ToShortDateString() + "',103)");
                    MessageBox.Show("Encerramento Concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ocorreu um erro tente novamente", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
