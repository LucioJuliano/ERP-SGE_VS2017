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
    public partial class FrmRegCobranca : Form
    {
        public TelaPrincipal FrmPrincipal;
        public int IdPagRec    = 0;        
        Financeiro Financ      = new Financeiro();
        Funcoes Controle       = new Funcoes();
        Auditoria RegAuditoria = new Auditoria();
        RegCobranca Cobranca   = new RegCobranca();

        public FrmRegCobranca()
        {
            InitializeComponent();
        }

        private void FrmRegCobranca_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;
            Cobranca.Controle     = Controle;
            Financ.Controle       = Controle;
            RegAuditoria.Controle = Controle;        
            LerInformacoes();            
        }

        private void LerInformacoes()
        {
            Financ.LerDados(IdPagRec);
            TxtNumDoc.Text       = Financ.NumDoc;
            TxtReferente.Text    = Financ.Referente;
            TxtNotaFiscal.Text   = Financ.NotaFiscal;
            TxtVlrOriginal.Value = Financ.VlrOriginal;            
            TxtVencimento.Value  = Financ.Vencimento;
            TxtMostraObs.Text    = "";
            TxtObservacao.Text   = "";                    
            //Buscando Registro de Cobrança

            SqlDataReader Tab = Controle.ConsultaSQL("SELECT * From RegCobranca Where ID_PagRec=" + IdPagRec.ToString() + " Order by id_Lanc Desc");
            if (Tab.HasRows)
            {
                while (Tab.Read())
                    TxtMostraObs.Text += Tab["Informacao"].ToString().Trim() + Environment.NewLine;
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma a Informação de Cobrança ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {                		

                if (TxtRetorno.Text.Trim() != "/  /")
                    TxtObservacao.Text = TxtObservacao.Text + " Data Retorno: " + TxtRetorno.Text.Trim();
 
                string Obs = "(" + FrmPrincipal.Perfil_Usuario.Usuario.Trim() + ") " + FrmPrincipal.DtHrServidor().ToShortDateString() + " " + FrmPrincipal.DtHrServidor().ToShortTimeString() + " - " + TxtObservacao.Text.Trim() +" \r\n";
                Cobranca.LerDados(0);
                Cobranca.IdPagRec   = Financ.IdLanc;
                Cobranca.IdPessoa   = Financ.IdPessoa;
                Cobranca.Data       = FrmPrincipal.DtHrServidor();
                Cobranca.Informacao = Obs;
                Cobranca.GravarDados();

                if (TxtRetorno.Text.Trim() != "/  /")
                    Controle.ExecutaSQL("Update RegCobranca set DtRetorno=Convert(DateTime,'" + TxtRetorno.Text.Trim() + "',103) where Id_lanc=" + Cobranca.IdLanc.ToString());
                LerInformacoes();                
            }
        }

        private void FrmRegCobranca_Shown(object sender, EventArgs e)
        {
            TxtObservacao.Focus();
        }

        private void TxtRetorno_Validated(object sender, EventArgs e)
        {
            if (TxtRetorno.Text.Trim() != "/  /")
            {
                DateTime Dt;
                try
                {
                    Dt = DateTime.Parse(TxtRetorno.Text);

                    if (Dt.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Data de Retorno não pode ser inferior a Data Atual");
                        TxtRetorno.Text = "";
                        TxtRetorno.Focus();
                    }
                    
                }
                catch
                {
                    MessageBox.Show("Data Invalida");
                    TxtRetorno.Text = "";
                    TxtRetorno.Focus();
                }
            }

        }
    }
}
