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
using CDSSoftware;
using System.Collections;

namespace ERP_SGE
{
    public partial class FrmPremiacao : Form
    {

        Funcoes Controle = new Funcoes();
        Premiacao Prem   = new Premiacao();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private BindingSource Source_Premiacao;

        public FrmPremiacao()
        {
            InitializeComponent();
        }

        private void FrmPremiacao_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Prem.Controle    = Controle;            
            Source_Premiacao = new BindingSource();            
            PopularGrid();
        }

        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT ID_LANC,PREMIO FROM PREMIACAO ORDER BY ID_LANC");
            Source_Premiacao.DataSource = Tabela;
            Source_Premiacao.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source_Premiacao;
            int item = Source_Premiacao.Find("Id_Lanc", Prem.IdLanc);
            Source_Premiacao.Position = item;
        }

        private void PopularCampos(int Isn)
        {            
            Prem.LerDados(Isn);
            TxtCodigo.Text        = string.Format("{0:D5}", int.Parse(Prem.IdLanc.ToString()));
            TxtPremio.Text        = Prem.Premio;
            TxtVlrFinanc1.Value   = Prem.Financeiro1;
            TxtPercFinanc1.Value  = Prem.PercFinanc1;
            TxtVlrRentab1.Value   = Prem.Rentabilidade1;
            TxtPercRentab1.Value  = Prem.PercRentab1;
            TxtCliente1.Value     = Prem.Cliente1;
            TxtPercCliente1.Value = Prem.PercCliente1;
            TxtVlrFinanc2.Value   = Prem.Financeiro2;
            TxtPercFinanc2.Value  = Prem.PercFinanc2;           
            TxtVlrFinanc3.Value   = Prem.Financeiro3;
            TxtPercFinanc3.Value      = Prem.PercFinanc3;
            TxtVlrRentab.Value        = Prem.VlrRentab;
            TxtVlrCliente.Value       = Prem.VlrCliente;
            TxtVlrGrdClientes.Value   = Prem.VlrGrdClientes;
            TxtVlrGradePrd1.Value     = Prem.VlrGradePrd1;
            TxtVlrGradePrd2.Value     = Prem.VlrGradePrd2;
            TxtGradeClientes.Value    = Prem.GradeClientes;
            Ck_CotaFinanceira.Checked = Prem.CotaFinanceira == 1;

        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularCampos(0);
            FrmPrincipal.ControleBotoes(true);
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtPremio.Text.Trim() == "")
            {
                MessageBox.Show("Favor o nome da Premiação", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Prem.IdLanc         = int.Parse(TxtCodigo.Text);
            Prem.Premio         = TxtPremio.Text;
            Prem.Financeiro1    = TxtVlrFinanc1.Value;
            Prem.PercFinanc1    = TxtPercFinanc1.Value;
            Prem.Rentabilidade1 = TxtVlrRentab1.Value;
            Prem.PercRentab1    = TxtPercRentab1.Value;
            Prem.Cliente1       = int.Parse(TxtCliente1.Value.ToString());
            Prem.PercCliente1   = TxtPercCliente1.Value;
            Prem.Financeiro2    = TxtVlrFinanc2.Value;
            Prem.PercFinanc2    = TxtPercFinanc2.Value;
            Prem.Financeiro3    = TxtVlrFinanc3.Value;
            Prem.PercFinanc3    = TxtPercFinanc3.Value;
            Prem.VlrRentab      = TxtVlrRentab.Value;
            Prem.VlrCliente     = TxtVlrCliente.Value;
            Prem.VlrGrdClientes = TxtVlrGrdClientes.Value;
            Prem.VlrGradePrd1   = TxtVlrGradePrd1.Value;
            Prem.VlrGradePrd2   = TxtVlrGradePrd2.Value;
            Prem.GradeClientes  = int.Parse(TxtGradeClientes.Value.ToString());
            
            if (Ck_CotaFinanceira.Checked) Prem.CotaFinanceira = 1; else Prem.CotaFinanceira = 0;

            Prem.GravarDados();
            PopularGrid();
            PopularCampos(Prem.IdLanc);
            FrmPrincipal.ControleBotoes(false);

        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                Prem.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Prem.LerDados(Prem.IdLanc);

                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Prem.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Prem.Excluir();                    
                    PopularGrid();
                    LimpaDados();
                    GridDados.Focus();
                }
            }
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
                Paginas.SelectTab(0);
                GridDados.Focus();
                LimpaDados();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.ToString());
            }
        }
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void LimpaDados()
        {
            TxtCodigo.Text = "0";            
            PopularCampos(0);            
        }
        private void Paginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Paginas.SelectedIndex == 0)
                PopularGrid();

            if (GridDados.CurrentRow != null)
            {
                if (Paginas.SelectedIndex == 0)
                    BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
                else
                {
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                }
            }
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }
        private void Frm_Activated(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
            FrmPrincipal.ClickBtnNovo     += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar   += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar   += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir  += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
            FrmPrincipal.ClickBtnFechar   += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
    }
}
