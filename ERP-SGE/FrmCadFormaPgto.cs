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
    public partial class FrmCadFormaPgto : Form
    {
        Funcoes Controle = new Funcoes();
        FormaPagamento FormaPgto = new FormaPagamento();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadFormaPgto()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            FormaPgto.Controle = Controle;
            FormaPgto.IdFormaPgto = 0;
            ChkNome.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento ORDER BY FormaPgto");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_FormaPgto", FormaPgto.IdFormaPgto);
            Source.Position = item;

        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            FormaPgto.LerDados(Isn);
            TxtCodigo.Text           = FormaPgto.IdFormaPgto.ToString();
            TxtFormaPgto.Text        = FormaPgto.FormaPgto;
            TxtNumParcelas.Value     = FormaPgto.NumParcelas;
            TxtPrimParcela.Value     = FormaPgto.PrimParcela;
            TxtIntervalo.Value       = FormaPgto.Intervalo;
            TxtIdServidor.Value      = FormaPgto.IdServidor;
            TxtVlrParc.Value         = FormaPgto.VlrParcelamento;
            LstTipoDoc.SelectedValue = FormaPgto.IdTpDocumento.ToString();
            Cb_Financeiro.Checked    = FormaPgto.Financeiro == 1;
            Cb_Baixa.Checked         = FormaPgto.Baixa == 1;
            Cb_VerDebito.Checked     = FormaPgto.VerDebito == 1;
            Cb_VerCredito.Checked    = FormaPgto.VerCredito == 1;
            TxtDesconto.Value        = FormaPgto.Desconto;
            Chk_Ativo.Checked        = FormaPgto.Ativo == 1;
            Cb_BloqPF.Checked        = FormaPgto.BloqPF == 1;
            Cb_LibClieNovo.Checked   = FormaPgto.LibClieNovo == 1;
        }


        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtFormaPgto.Focus();
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
                TxtFormaPgto.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtFormaPgto.Text.Trim() != "")
            {
                FormaPgto.IdFormaPgto     = int.Parse(TxtCodigo.Text);
                FormaPgto.FormaPgto       = TxtFormaPgto.Text;
                FormaPgto.NumParcelas     = int.Parse(TxtNumParcelas.Value.ToString());
                FormaPgto.PrimParcela     = int.Parse(TxtPrimParcela.Value.ToString());
                FormaPgto.Intervalo       = int.Parse(TxtIntervalo.Value.ToString());
                FormaPgto.IdTpDocumento   = int.Parse(LstTipoDoc.SelectedValue.ToString());
                FormaPgto.IdServidor      = int.Parse(TxtIdServidor.Value.ToString());
                FormaPgto.VlrParcelamento = TxtVlrParc.Value;
                
                if (Cb_Financeiro.Checked) FormaPgto.Financeiro = 1; else FormaPgto.Financeiro = 0;
                if (Cb_Baixa.Checked) FormaPgto.Baixa = 1; else FormaPgto.Baixa = 0;
                if (Cb_VerDebito.Checked) FormaPgto.VerDebito = 1; else FormaPgto.VerDebito= 0;
                if (Cb_VerCredito.Checked) FormaPgto.VerCredito = 1; else FormaPgto.VerCredito = 0;
                if (Chk_Ativo.Checked) FormaPgto.Ativo = 1; else FormaPgto.Ativo = 0;
                if (Cb_BloqPF.Checked) FormaPgto.BloqPF = 1; else FormaPgto.BloqPF = 0;
                if (Cb_LibClieNovo.Checked) FormaPgto.LibClieNovo = 1; else FormaPgto.LibClieNovo = 0;
                FormaPgto.Desconto = TxtDesconto.Value;
                FormaPgto.GravarDados();
                PopularGrid();
                PopularCampos(FormaPgto.IdFormaPgto);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("FormaPgto não Informada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtFormaPgto.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FormaPgto.IdFormaPgto = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    FormaPgto.Excluir();
                    PopularGrid();
                    LimpaDados();
                    GridDados.Focus();
                }
            }
            else
                MessageBox.Show("Não existe Registro para Excluir", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /*private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
            PopularGrid();
        }*/
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
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
            LstTipoDoc = FrmPrincipal.PopularCombo("SELECT Id_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstTipoDoc);
        }
        private void LimpaDados()
        {
            TxtCodigo.Text           = "0";
            TxtFormaPgto.Text        = "";
            TxtNumParcelas.Value     = 1;
            TxtPrimParcela.Value     = 0;
            TxtIntervalo.Value       = 0;
            Cb_Financeiro.Checked    = true;
            Cb_Baixa.Checked         = false;
            Cb_BloqPF.Checked        = false;
            Cb_VerDebito.Checked     = false;
            Cb_VerCredito.Checked    = false;
            Cb_LibClieNovo.Checked   = false;
            TxtDesconto.Value        = 0;
            LstTipoDoc.SelectedValue = 0;
            TxtIdServidor.Value      = 0;
            TxtVlrParc.Value         = 0;
            Chk_Ativo.Checked        = false;
            FormaPgto.LerDados(0);
        }
        private void Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[0].Value.ToString() != "")
                    BtnEditar_Click(FrmPrincipal.BtnEditar, null);
            }
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
                    BtnEditar_Click(FrmPrincipal.BtnEditar, null);
            }

        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            if (TxtPesquisa.Text.Trim() != "")
            {
                try
                {
                    DataSet Tabela = new DataSet();
                    if (ChkCodigo.Checked)
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento WHERE Id_FormaPgto={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento WHERE FormaPgto LIKE '%{0}%' order by FormaPgto", TxtPesquisa.Text.Trim()));
                    GridDados.DataSource = Tabela;
                    GridDados.DataMember = Tabela.Tables[0].TableName;
                }
                catch
                {
                    MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                PopularGrid();
            }
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }

        
    }
}
