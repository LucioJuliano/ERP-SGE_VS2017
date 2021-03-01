using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controle_Dados;

namespace ERP_SGE
{
    public partial class FrmSaldoGeralEstoque : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;

        public FrmSaldoGeralEstoque()
        {
            InitializeComponent();
        }

        private void FrmSaldoGeralEstoque_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            //
            Controles.WsVerificarEstoque Verificar = new Controles.WsVerificarEstoque();
            Verificar.Controle = Controle;
            DataTable Tabela = Verificar.Ver_Saldo(TxtRef.Text.Trim(),FrmPrincipal.VersaoDistribuidor);           
            GridDados.DataSource = Tabela;
            GridDados.DataMember = Tabela.TableName;
        }

    }
}
