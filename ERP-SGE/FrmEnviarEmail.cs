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
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.IO;

namespace ERP_SGE
{
    public partial class FrmEnviarEmail : Form
    {
        Funcoes Controle       = new Funcoes();
        Parametros ParamFilial = new Parametros();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        public string destino;
        public string copia;
        public string assunto;
        public string texto = "";

        public FrmEnviarEmail()
        {
            InitializeComponent();
        }

        private void FrmEnviarEmail_Load(object sender, EventArgs e)
        {

        }

        private void FrmEnviarEmail_Shown(object sender, EventArgs e)
        {
            txtEmailDest.Text = destino.Trim();
            txtCopia.Text     = copia.Trim();
            txtAssunto.Text   = assunto.Trim();
            if (texto != "")
                txtTexto.Text = texto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Anexo.ShowDialog();
            txtAnexo.Text = Anexo.FileName;
        }

        private void BtnEnvia_Click(object sender, EventArgs e)
        {
            EnviarEmail();
            MessageBox.Show("Email Enviado");
        }

        public void EnviarEmail()
        {
            try
            {
                BtnEnvia.Enabled = false;
                Application.DoEvents();
                if (txtEmailDest.Text.Trim() == "")
                {
                    MessageBox.Show("Favor informar o EMAIL destino");
                    BtnEnvia.Enabled = true;
                    return;
                }
                Controles.EnviarEmail Enviar = new Controles.EnviarEmail();
                Enviar.Enviar_EmailCobranca(txtAssunto.Text, txtEmailDest.Text, txtCopia.Text, txtTexto.Text, txtAnexo.Text);
                BtnEnvia.Enabled = true;
                
            }
            catch
            {
                MessageBox.Show("Erro ao envia o email");
                BtnEnvia.Enabled = true;
            }
        }
    }
}
