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
    public partial class FrmNovaSenha : Form
    {
        public string Senha = "";
        public FrmNovaSenha()
        {
            InitializeComponent();
        }

        private void BtnConfirma_Click(object sender, EventArgs e)
        {
            if (TxtSenha.Text.Trim() == "")
            {
                MessageBox.Show("Digite a nova senha", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtSenha.Focus();
                return;
            }

            if (TxtSenha.Text.Trim() != TxtConfSenha.Text.Trim())
            {
                MessageBox.Show("Senhas digirados são diferentes", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtSenha.Text = "";
                TxtConfSenha.Text = "";
                TxtSenha.Focus();
                return;
            }
            Senha = TxtSenha.Text;
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
