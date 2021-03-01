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
    public partial class FrmEventosProvDesc : Form
    {
        Funcoes Controle = new Funcoes();
        ProvDescontos Eventos = new ProvDescontos();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private DataSet Tabela;
        private BindingSource Source;

        public FrmEventosProvDesc()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Eventos.Controle = Controle;
            Eventos.IdCodigo = 0;
            LstProvDesc.SelectedIndex = 0;            
            ChkNome.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            string Filtro = " ORDER BY DESCRICAO";

            if (TxtPesquisa.Text.Trim() != "")
            {
                if (ChkCodigo.Checked)
                    Filtro = " WHERE ID_CODIGO=" + TxtPesquisa.Text.Trim() + " ORDER BY ID_CODIGO";
                else
                    Filtro = " WHERE DESCRICAO LIKE ='%" + TxtPesquisa.Text.Trim() + "%' ORDER BY DESCRICAO";
            }
            Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Codigo,Descricao,CASE PROVDESC WHEN 0 THEN 'Provento' ELSE 'Desconto' END AS PROVDESC FROM PROVENTOSDESCONTOS " + Filtro);
            Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("ID_Codigo", Eventos.IdCodigo);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Eventos.LerDados(Isn);
            TxtCodigo.Text            = Eventos.IdCodigo.ToString();
            TxtDescricao.Text         = Eventos.Descricao;
            LstProvDesc.SelectedIndex = Eventos.ProvDesc;
            Ck_FolhaBruta.Checked     = Eventos.FolhaBruta == 1;        
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            LstProvDesc.Focus();
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
               /* if (int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()) <= 10)
                {
                    MessageBox.Show("Evento resevado pelo sistema, não pode ser alterado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }*/
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
                TxtDescricao.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtDescricao.Text.Trim() != "")
            {
                Eventos.IdCodigo  = int.Parse(TxtCodigo.Text);
                Eventos.Descricao = TxtDescricao.Text;
                Eventos.ProvDesc  = LstProvDesc.SelectedIndex;
                if (Ck_FolhaBruta.Checked) Eventos.FolhaBruta = 1; else Eventos.FolhaBruta = 0;
                Eventos.GravarDados();
                PopularGrid();
                PopularCampos(Eventos.IdCodigo);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Descrição do Evento não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtDescricao.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()) <= 10)
                {
                    MessageBox.Show("Evento resevado pelo sistema, não pode ser excluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Eventos.IdCodigo = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Eventos.Excluir();
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
            TxtDescricao.Text = "";
            LstProvDesc.SelectedIndex = 0;
            Ck_FolhaBruta.Checked = false;
            Eventos.LerDados(0);
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
            PopularGrid();

        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
    }
}
