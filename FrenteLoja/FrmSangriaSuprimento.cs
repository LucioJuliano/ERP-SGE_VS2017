using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP_SGE;
using Controle_Dados;
using Controles;

namespace FrenteLoja
{
    public partial class FrmSangriaSuprimento : Form
    {
        Funcoes Controle = new Funcoes();        
        MvCaixaBalcao MvCaixa = new MvCaixaBalcao();
        public FrmFrenteLoja FrmFrenteLoja;
        public FrmFrenteLoja.SanGriaSuprimento TpMovim;

        public FrmSangriaSuprimento()
        {
            InitializeComponent();            
        }
        private void FrmSangriaSuprimento_Load(object sender, EventArgs e)
        {
            LblNomeMov.Text  = this.Text;
            Controle.Conexao = FrmFrenteLoja.FrmPrincipal.Conexao;
            MvCaixa.Controle = Controle;

            TxtDtCaixa.Value = DateTime.Now;
            TxtUsuario.Text  = FrmFrenteLoja.FrmPrincipal.Perfil_Usuario.Usuario;
            LstTipoDoc       = FrmFrenteLoja.FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstTipoDoc,"");
            TxtValor.Focus();
        }
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (TxtValor.Value <= 0)
            {
                MessageBox.Show("Favor verifique o valor do movimento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtValor.Focus();
                return;
            }
            if (int.Parse(LstTipoDoc.SelectedValue.ToString())==0)
            {
                MessageBox.Show("Favor informar o Documento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LstTipoDoc.Focus();
                return;
            }
            
            if (MessageBox.Show("Confirma o Movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MvCaixa.LerDados(0);
                if (TpMovim == FrmFrenteLoja.SanGriaSuprimento.Sangria)
                {
                    MvCaixa.Tipo = 0;
                    MvCaixa.Descricao = "Sangria: "+TxtDescricao.Text.Trim();
                }
                else
                {
                    MvCaixa.Descricao = "Suprimento: " + TxtDescricao.Text.Trim();
                    MvCaixa.Tipo = 1;
                }                
                MvCaixa.Valor       = TxtValor.Value;
                MvCaixa.IdDocumento = int.Parse(LstTipoDoc.SelectedValue.ToString());
                MvCaixa.Status      = 1;
                MvCaixa.IdCaixa     = FrmFrenteLoja.IdCaixa;
                MvCaixa.GravarDados();
                MessageBox.Show("Operação concluida");
                Close();
            }
        }

        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
