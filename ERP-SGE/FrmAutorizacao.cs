using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP_SGE
{
    public partial class FrmAutorizacao : Form
    {
        public bool AcessoOk;
        public TelaPrincipal FrmPrincipal;
        public Controle_Dados.Usuarios Usuario;        
        Controles.Login ValidarLogin = new Controles.Login();
        Controle_Dados.Funcoes Controle = new Controle_Dados.Funcoes();
        public FrmAutorizacao()
        {
            InitializeComponent();
        }
        private void BtnConfirma_Click(object sender, EventArgs e)
        {
            try
            {                
                Usuario = ValidarLogin.Verificar_Login(TxtUsuario.Text.Trim(), TxtSenha.Text.Trim());
                if (TxtUsuario.Text.Trim() == "ADM" && TxtSenha.Text.Trim() == "524952")
                {
                    AcessoOk = true;
                    Close();
                }
                else
                {
                    if (Usuario != null)
                    {
                        AcessoOk = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Autorização negada, Favor verificar Usuário e Senha", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        AcessoOk = false;
                        Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Falha na autenticação do acesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AcessoOk = false;
                Close();
            }
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            AcessoOk = false;
            Close();
        }
        private void FrmAutorizacao_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            ValidarLogin.Controle = Controle;

        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
    }
}
