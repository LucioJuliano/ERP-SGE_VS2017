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
    public partial class FrmCadGrupoProduto : Form
    {
        Funcoes Controle = new Funcoes();
        GrupoProduto Grupo = new GrupoProduto();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadGrupoProduto()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Grupo.Controle = Controle;
            Grupo.IdGrupo = 0;
            ChkNome.Checked = true;
            Ck_ExcNfeTransf.Visible = !FrmPrincipal.VersaoDistribuidor;
            //CriaLista_CST_DIEF();
            //CriaLista_CST_SPED();
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Grupo,Grupo FROM GrupoProduto ORDER BY Grupo");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Grupo", Grupo.IdGrupo);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Grupo.LerDados(Isn);
            TxtCodigo.Text          = Grupo.IdGrupo.ToString();
            TxtGrupo.Text           = Grupo.Grupo;
            TxtPercVerDesc.Value    = Grupo.PercVerDesc;
            Ck_ListaEstMin.Checked  = Grupo.ListaEstMin == 1;
            Ck_ListaWeb.Checked     = Grupo.ListaWeb == 1;
            Ck_ListaVenda.Checked   = Grupo.ListaVenda == 1;
            Ck_ExcNfeTransf.Checked = Grupo.ExcluirNFETrans == 1;
            Ck_NaoEstoque.Checked   = Grupo.Estoque == 1;
            Ck_Ativo.Checked        = Grupo.Ativo == 1;
            
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtGrupo.Focus();
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
                TxtGrupo.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtGrupo.Text.Trim() != "")
            {
                Grupo.IdGrupo     = int.Parse(TxtCodigo.Text);
                Grupo.Grupo       = TxtGrupo.Text;
                Grupo.PercVerDesc = TxtPercVerDesc.Value;
                if (Ck_ListaWeb.Checked) Grupo.ListaWeb = 1; else Grupo.ListaWeb = 0;
                if (Ck_ListaEstMin.Checked) Grupo.ListaEstMin = 1; else Grupo.ListaEstMin = 0;
                if (Ck_ListaVenda.Checked) Grupo.ListaVenda = 1; else Grupo.ListaVenda = 0;
                if (Ck_ExcNfeTransf.Checked && !FrmPrincipal.VersaoDistribuidor) Grupo.ExcluirNFETrans = 1; else Grupo.ExcluirNFETrans = 0;
                if (Ck_NaoEstoque.Checked) Grupo.Estoque = 1; else Grupo.Estoque = 0;
                if (Ck_Ativo.Checked) Grupo.Ativo = 1; else Grupo.Ativo = 0;
                Grupo.GravarDados();
                PopularGrid();
                PopularCampos(Grupo.IdGrupo);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Grupo não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtGrupo.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Grupo.IdGrupo = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Grupo.Excluir();
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
            TxtGrupo.Text = "";
            TxtPercVerDesc.Value    = 0;
            Ck_ListaEstMin.Checked  = false;
            Ck_ListaWeb.Checked     = false;
            Ck_ListaVenda.Checked   = false;
            Ck_ExcNfeTransf.Checked = false;
            Ck_NaoEstoque.Checked   = false;
            Ck_Ativo.Checked        = false;
            Grupo.LerDados(0);
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
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Grupo,Grupo FROM GrupoProduto WHERE ID_Grupo={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Grupo,Grupo FROM GrupoProduto WHERE Grupo LIKE '%{0}%' order by Grupo", TxtPesquisa.Text.Trim()));
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

        private void BtnReajuste_Click(object sender, EventArgs e)
        {            
            if (GridDados.CurrentRow != null)
            {
                if (Grupo.IdGrupo == 0)
                    return;

                FrmReajustePrc FrmReajuste = new FrmReajustePrc();
                FrmReajuste.FrmPrincipal   = FrmPrincipal;
                FrmReajuste.IdGrupo        = Grupo.IdGrupo;
                FrmReajuste.LblGrupo.Text  = "Grupo: " + Grupo.Grupo;
                FrmReajuste.ShowDialog();
                FrmReajuste.Dispose();
            }
        }

        /*private void CriaLista_CST_DIEF()
        {
            DataTable Tabela = new DataTable();
            Tabela.Columns.Add("Codigo", Type.GetType("System.Int32"));
            Tabela.Columns.Add("Cts", Type.GetType("System.String"));
            //Add os Itens            
            Tabela.Rows.Add(1, "Mercadorias p/Revenda");
            Tabela.Rows.Add(2, "Matéria Prima");
            Tabela.Rows.Add(3, "Produto em Elaboração");
            Tabela.Rows.Add(4, "Produto Acabado");
            Tabela.Rows.Add(5, "Material de Acondicionamento e embalagem");
            Tabela.Rows.Add(6, "Mercadorias Recebida em Consignação");
            Tabela.Rows.Add(7, "Outras Mercadorias ou Produtos");
            LstCST_Dief.DataSource = Tabela;
            LstCST_Dief.ValueMember = Tabela.Columns[0].ColumnName;
            LstCST_Dief.DisplayMember = Tabela.Columns[1].ColumnName;            
            LstCST_Dief.SelectedValue = 7;
        }
        private void CriaLista_CST_SPED()
        {
            DataTable Tabela = new DataTable();
            Tabela.Columns.Add("Codigo", Type.GetType("System.Int32"));
            Tabela.Columns.Add("Cts", Type.GetType("System.String"));
            //Add os Itens            
            Tabela.Rows.Add(0, "Mercadorias p/Revenda");
            Tabela.Rows.Add(1, "Matéria Prima");
            Tabela.Rows.Add(2, "Embalagem");
            Tabela.Rows.Add(3, "Produto em Processo");
            Tabela.Rows.Add(4, "Produto Acabado");
            Tabela.Rows.Add(5, "SubProduto");
            Tabela.Rows.Add(6, "Produto Intermediário");
            Tabela.Rows.Add(7, "Material de Uso e Consumo");
            Tabela.Rows.Add(8, "Ativo Imobilizado");
            Tabela.Rows.Add(9, "Serviços");
            Tabela.Rows.Add(10, "Outros Insumos");
            Tabela.Rows.Add(99, "Outras");            
            LstCST_Sped.DataSource = Tabela;
            LstCST_Sped.ValueMember = Tabela.Columns[0].ColumnName;
            LstCST_Sped.DisplayMember = Tabela.Columns[1].ColumnName;
            LstCST_Sped.SelectedValue = 99;
        }*/

    }
}
