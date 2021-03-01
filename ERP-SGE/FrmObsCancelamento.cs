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
    public partial class FrmObsCancelamento : Form
    {

        public TelaPrincipal FrmPrincipal;
        public int IdVenda     = 0;
        public bool RegInf     = false;
        MvVenda Vendas         = new MvVenda();
        Funcoes Controle       = new Funcoes();
        Auditoria RegAuditoria = new Auditoria();
        

        public FrmObsCancelamento()
        {
            InitializeComponent();
        }

        private void FrmObsCancelamento_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;
            Vendas.Controle       = Controle;
            RegAuditoria.Controle = Controle;        
            LerInformacoes();            
        }

        private void LerInformacoes()
        {
            Vendas.LerDados(IdVenda);
            TxtMostraObs.Text  = Vendas.ObsCancelamento;
            TxtObservacao.Text = "";            
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma o Registro da Informação", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string Obs = "'(" + FrmPrincipal.Perfil_Usuario.Usuario.Trim() + ") " + FrmPrincipal.DtHrServidor().ToShortDateString() + " " + FrmPrincipal.DtHrServidor().ToShortTimeString() + " - " + TxtObservacao.Text.Trim() + " \r\n'";
                Controle.ExecutaSQL("Update MVVenda set ObsCancelamento=rtrim(convert(char(3000),isnull(ObsCancelamento,''))) + " + Obs + " WHERE ID_VENDA=" + IdVenda.ToString());
                LerInformacoes();
                RegInf = true;
                Close();
            }
        }

        private void FrmObsCancelamento_Shown(object sender, EventArgs e)
        {
            TxtObservacao.Focus();
        }
    }
}
