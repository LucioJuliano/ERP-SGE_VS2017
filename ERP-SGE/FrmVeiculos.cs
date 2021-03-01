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
    public partial class FrmVeiculos : Form
    {
        Funcoes Controle = new Funcoes();
        Veiculos CadVeiculo = new Veiculos();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private DataSet Tabela;
        private BindingSource Source;

        public FrmVeiculos()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            CadVeiculo.Controle = Controle;
            ChkVeiculo.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Veiculo,Veiculo,Placa FROM Veiculos ORDER BY Veiculo");
            Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("ID_Veiculo", CadVeiculo.IdVeiculo);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            CadVeiculo.LerDados(Isn);
            TxtCodigo.Text    = CadVeiculo.IdVeiculo.ToString();
            TxtVeiculo.Text   = CadVeiculo.Veiculo;
            TxtPlaca.Text     = CadVeiculo.Placa;
            TxtVlrCarga.Value = CadVeiculo.VlrCarga;

        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtVeiculo.Focus();
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
                TxtVeiculo.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtVeiculo.Text.Trim() != "")
            {
                CadVeiculo.IdVeiculo = int.Parse(TxtCodigo.Text);
                CadVeiculo.Veiculo   = TxtVeiculo.Text;
                CadVeiculo.Placa     = TxtPlaca.Text;
                CadVeiculo.VlrCarga  = TxtVlrCarga.Value;
                CadVeiculo.GravarDados();
                PopularGrid();
                PopularCampos(CadVeiculo.IdVeiculo);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Veiculo não Informad0", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtVeiculo.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CadVeiculo.IdVeiculo = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    CadVeiculo.Excluir();
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
            FrmPrincipal.ClickBtnNovo += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
            FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
        }
        private void LimpaDados()
        {
            TxtCodigo.Text    = "0";
            TxtVeiculo.Text   = "";
            TxtPlaca.Text     = "";
            TxtVlrCarga.Value = 0;
            CadVeiculo.LerDados(0);
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
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Veiculo,Veiculo,Placa FROM Veiculos WHERE Id_Veiculo={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Veiculo,Veiculo,Placa FROM Veiculos WHERE Veiculo LIKE '%{0}%' order by Veiculo", TxtPesquisa.Text.Trim()));
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
