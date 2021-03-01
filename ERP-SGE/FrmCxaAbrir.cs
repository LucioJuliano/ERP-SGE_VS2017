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
    public partial class FrmCxaAbrir : Form
    {
        Funcoes Controle = new Funcoes();
        CaixaBalcao CxBalcao = new CaixaBalcao();
        MvCaixaBalcao MvCaixa = new MvCaixaBalcao();

        public TelaPrincipal FrmPrincipal;

        public FrmCxaAbrir()
        {
            InitializeComponent();
        }

        private void FrmCxaAbrir_Load(object sender, EventArgs e)
        {
            Controle.Conexao  = FrmPrincipal.Conexao;
            CxBalcao.Controle = Controle;
            MvCaixa.Controle  = Controle;
            TxtDtCaixa.Value  = DateTime.Now;
            TxtUsuario.Text   = FrmPrincipal.Perfil_Usuario.Usuario;
            TxtVlrInicial.Focus();
            //
            TxtVlrInicial.Enabled = false;
            CalcAberturaCX();
        }

        private void BtnAbrir_Click(object sender, EventArgs e)
        {            
            if (MessageBox.Show("Confirma abertura do caixa?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    BtnAbrir.Enabled = false;
                    Application.DoEvents();

                    CxBalcao.LerCaixa(0);
                    CxBalcao.IdFilial     = FrmPrincipal.Perfil_Usuario.IdFilial;
                    CxBalcao.Data         = TxtDtCaixa.Value;
                    CxBalcao.DtHrAbertura = DateTime.Now;
                    CxBalcao.IdUsuario    = FrmPrincipal.Perfil_Usuario.IdUsuario;
                    CxBalcao.Observcao    = TxtObservacao.Text.Trim();
                    CxBalcao.VlrInicial   = TxtVlrInicial.Value;
                    CxBalcao.Status       = 0;
                    CxBalcao.AbrirCaixa();
                    // Gerando o Movimento 
                    MvCaixa.Tipo          = 1;
                    MvCaixa.Descricao     = "Abertura do Caixa";
                    MvCaixa.Valor         = TxtVlrInicial.Value;
                    MvCaixa.IdDocumento   = 1;
                    MvCaixa.Status        = 1;
                    MvCaixa.IdCaixa       = CxBalcao.IdCaixa;
                    MvCaixa.GravarDados();
                    //

                    if (FrmPrincipal.TipoImpressoraFiscal() != Controles.ImpressoraFiscal.ModeloImpressora.Nenhuma && FrmPrincipal.TipoImpressoraFiscal() != Controles.ImpressoraFiscal.ModeloImpressora.MFE)
                    {
                        if (MessageBox.Show("Imprime a Leitura X ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (FrmPrincipal.TipoImpressoraFiscal() != Controles.ImpressoraFiscal.ModeloImpressora.Nenhuma)
                            {
                                FrmPrincipal.PDV_ImpressoraFiscal.ImpFiscal = FrmPrincipal.TipoImpressoraFiscal();
                                FrmPrincipal.PDV_ImpressoraFiscal.LeituraX();
                            }
                            else
                            {
                                MessageBox.Show("Impressora Fiscal não instalada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    MessageBox.Show("O caixa foi aberto", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                   MessageBox.Show("Atenção: Erro na abertura do caixa tente novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CalcAberturaCX()
        {
            Controles.Verificar BuscaTab = new Controles.Verificar();
            BuscaTab.Controle=Controle;
            int IdUsu = FrmPrincipal.Perfil_Usuario.IdUsuario;
            int IdCx  = BuscaTab.Verificar_ExisteCadastro("Id_Caixa", "SELECT TOP 1 ID_CAIXA FROM CAIXABALCAO WHERE STATUS=1 AND ID_USUARIO=" + IdUsu.ToString() + " ORDER BY DATA DESC");

            //Totalizando o Lanç do Caixa
            decimal TotCaixa = 0;
            
            SqlDataReader Tab; 
            string sSQL = "SELECT * FROM MvCaixaBalcao WHERE Status=1 and Id_Caixa=" + IdCx.ToString();
            Tab = Controle.ConsultaSQL(sSQL);
            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (int.Parse(Tab["Tipo"].ToString()) == 0)
                        TotCaixa = TotCaixa - decimal.Parse(Tab["Valor"].ToString());
                    else
                        TotCaixa = TotCaixa + decimal.Parse(Tab["Valor"].ToString());
                }
            }
            //Totalizando o Movimentos
            sSQL = "SELECT ISNULL(SUM(T2.VLRORIGINAL),0) AS VALOR" +
                   " FROM MVVENDA T1" +
                   " JOIN LANCFINANCEIRO T2 ON (T2.ID_VENDA=T1.ID_VENDA) " +
                   " LEFT JOIN TIPODOCUMENTO T3 ON (T3.ID_DOCUMENTO=T2.ID_TIPODOCUMENTO) " +
                   " WHERE T1.ID_CAIXA=" + IdCx.ToString() +
                   " AND T1.ID_CAIXA<>0" +
                   " AND T1.TPVENDA IN ('PV','TROCA','VF') " +
                   " AND T1.VLRTOTAL > 0 " +
                   " AND T3.RESUMOCX=1 " +
                   " AND T1.STATUS=3 ";
            Tab = Controle.ConsultaSQL(sSQL);

            if (Tab.HasRows)
            {
                while (Tab.Read())
                    TotCaixa = TotCaixa + decimal.Parse(Tab["Valor"].ToString());
            }
            TxtVlrInicial.Value = TotCaixa;
        }
    }
}
