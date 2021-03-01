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
    public partial class FrmBaixaAutFinan : Form
    {
        Funcoes Controle = new Funcoes();
        public bool Baixar;
        public TelaPrincipal FrmPrincipal;

        public FrmBaixaAutFinan()
        {
            InitializeComponent();
        }

        private void FrmBaixaAutFinan_Load(object sender, EventArgs e)
        {
            LstTipoDoc = FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstTipoDoc);            
            LstCaixa   = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstCaixa);
            LstAgente = FrmPrincipal.PopularCombo("SELECT ID_Agente,Agente FROM AgenteCobrador ORDER BY Agente", LstAgente);
            Baixar     = false;
        }

        private void BtnBaixar_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstCaixa.SelectedValue.ToString()) == 0)
                MessageBox.Show("Favor informar o Caixa Financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (int.Parse(LstTipoDoc.SelectedValue.ToString()) == 0)
                MessageBox.Show("Favor informar o Documento Financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (int.Parse(LstAgente.SelectedValue.ToString()) == 0)
                MessageBox.Show("Favor informar o Agente Cobrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
            if (MessageBox.Show("Confirma a baixa do titulo ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Baixar = true;
                Close();
            }
        }
    }
}
