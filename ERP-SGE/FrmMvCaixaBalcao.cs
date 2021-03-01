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
namespace ERP_SGE
{
    public partial class FrmMvCaixaBalcao : Form
    {
        Funcoes Controle      = new Funcoes();
        MvCaixaBalcao MvCaixa = new MvCaixaBalcao();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;        
        public FrmMvCaixaBalcao()
        {
            InitializeComponent();
        }
        public int IdCaixa;
        private void FrmMvCaixaBalcao_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            MvCaixa.Controle = Controle;
            Rb_Despesa.Checked = true;
            LstTipoDoc = FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstTipoDoc);
            PnlDados.Enabled = false;
            HabilitarControles();
            PopularGrid();
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {

        }   
        private void HabilitarControles()
        {
            BtnNovo.Enabled      = !StaFormEdicao;
            BtnCancelar.Enabled  = !StaFormEdicao;
            GridDados.Enabled    = !StaFormEdicao;
            BtnConfirmar.Enabled = StaFormEdicao;            
            PnlDados.Enabled     = StaFormEdicao;
            PopularGrid();
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void PopularGrid()
        {
            try
            {
                string SqlConsulta = "SELECT ID_LANC,CASE STATUS WHEN 1 THEN 'Confirmado' WHEN 2 THEN 'Cancelado' ELSE ' ' END AS STATUS," +
                                     " CASE TIPO WHEN 0 THEN 'Despesa' WHEN 1 THEN 'Receita' END AS TIPOLANC,DESCRICAO,VALOR FROM MVCAIXABALCAO WHERE ID_CAIXA=" + IdCaixa.ToString();
                DataSet Tabela = new DataSet();
                Tabela = Controle.ConsultaTabela(SqlConsulta);
                BindingSource Source = new BindingSource();
                Source.DataSource = Tabela;
                Source.DataMember = Tabela.Tables[0].TableName;
                GridDados.DataSource = Source;
                int item = Source.Find("Id_Lanc", MvCaixa.IdLanc);
                Source.Position = item;                
            }
            catch
            {
                MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            TxtDescricao.Text = "";
            TxtValor.Value    = 0;
            LstTipoDoc.SelectedValue = "0";
            //
            MvCaixa.LerDados(0);
            StaFormEdicao = true;
            HabilitarControles();
            TxtDescricao.Focus();
        }
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstTipoDoc.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor informar o tipo de documento", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Rb_Despesa.Checked) MvCaixa.Tipo = 0; else MvCaixa.Tipo = 1;
            MvCaixa.Descricao   = TxtDescricao.Text;
            MvCaixa.Valor       = TxtValor.Value;
            MvCaixa.IdDocumento = int.Parse(LstTipoDoc.SelectedValue.ToString());
            MvCaixa.Status      = 1;
            MvCaixa.IdCaixa     = IdCaixa;
            MvCaixa.GravarDados();
            StaFormEdicao = false;
            HabilitarControles();
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                MvCaixa.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                MvCaixa.LerDados(MvCaixa.IdLanc);
                if (MvCaixa.Status == 2)
                    MessageBox.Show("Lançamento já Cancelado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (MessageBox.Show("Confirma o cancelamento do lançamento", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MvCaixa.Status = 2;
                        MvCaixa.GravarDados();
                        HabilitarControles();
                    }
                }
            }    
        }
    }
}
