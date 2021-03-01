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
    public partial class FrmCadDepartamento : Form
    {
        Funcoes Controle = new Funcoes();
        Departamentos Departamento = new Departamentos();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadDepartamento()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Departamento.Controle = Controle;
            Departamento.IdDepartamento = 0;
            ChkNome.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Departamento,Departamento FROM Departamentos ORDER BY Departamento");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Departamento", Departamento.IdDepartamento);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Departamento.LerDados(Isn);
            TxtCodigo.Text = Departamento.IdDepartamento.ToString();
            TxtDepartamento.Text = Departamento.Departamento;
            TxtResponsavel.Text = Departamento.Responsavel;
            TxtRamal.Value = Departamento.Ramal;
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtDepartamento.Focus();
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
                TxtDepartamento.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtDepartamento.Text.Trim() != "")
            {
                Departamento.IdDepartamento = int.Parse(TxtCodigo.Text);
                Departamento.Departamento = TxtDepartamento.Text;
                Departamento.Responsavel = TxtResponsavel.Text;
                Departamento.Ramal = int.Parse(TxtRamal.Value.ToString());
                Departamento.GravarDados();
                PopularGrid();
                PopularCampos(Departamento.IdDepartamento);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Ramo de Departamento não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtDepartamento.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Departamento.IdDepartamento = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Departamento.Excluir();
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
            TxtCodigo.Text = "0";
            TxtDepartamento.Text = "";
            TxtResponsavel.Text = "";
            TxtRamal.Value = 0;
            Departamento.LerDados(0);
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
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Departamento,Departamento FROM Departamentos WHERE ID_Departamento={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Departamento,Departamento FROM Departamentos WHERE Departamento LIKE '%{0}%' order by Departamento", TxtPesquisa.Text.Trim()));
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
