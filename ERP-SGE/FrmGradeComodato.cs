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
    public partial class FrmGradeComodato : Form
    {
        Funcoes Controle              = new Funcoes();
        GradeComodato Grade           = new GradeComodato();
        GradeComodatoItens GradeItens = new GradeComodatoItens();
        GradeComodatoVinc  GradeVinc  = new GradeComodatoVinc();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private BindingSource Source_Grade;

        private DataSet TabComodItens;
        private BindingSource Source_ComodItens;

        private DataSet TabComodVinc;
        private BindingSource Source_ComodVinc;

        public FrmGradeComodato()
        {
            InitializeComponent();
        }
        private void FrmPremiacao_Load(object sender, EventArgs e)
        {
            Controle.Conexao    = FrmPrincipal.Conexao;
            Grade.Controle      = Controle;
            GradeItens.Controle = Controle;
            GradeVinc.Controle  = Controle;
            Source_Grade        = new BindingSource();            
            TabComodItens       = new DataSet();
            Source_ComodItens   = new BindingSource();
            TabComodVinc        = new DataSet();
            Source_ComodVinc    = new BindingSource();
            PopularGrid();
            PopularGridItens();
            PopularGridItensVinc();
        }

        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT ID_GRADE,GRADE FROM GRADECOMODATO ORDER BY ID_GRADE");
            Source_Grade.DataSource = Tabela;
            Source_Grade.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source_Grade;
            int item = Source_Grade.Find("Id_Grade", Grade.IdGrade);
            Source_Grade.Position = item;
        }

        private void PopularCampos(int Isn)
        {
            Grade.LerDados(Isn);
            TxtCodigo.Text = string.Format("{0:D5}", int.Parse(Grade.IdGrade.ToString()));
            TxtGrade.Text = Grade.Grade;
            TxtQtde.Value = Grade.Qtde;            
            PopularGridItens();
            PopularGridItensVinc();
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
            if (TxtGrade.Text.Trim() == "")
            {
                MessageBox.Show("Favor o nome da Grade", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Grade.IdGrade = int.Parse(TxtCodigo.Text);
            Grade.Grade = TxtGrade.Text;
            Grade.Qtde = int.Parse(TxtQtde.Value.ToString());
            Grade.GravarDados();
            PopularGrid();
            PopularCampos(Grade.IdGrade);
            StaFormEdicao = false;            
            FrmPrincipal.ControleBotoes(false);

        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                Grade.IdGrade = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Grade.LerDados(Grade.IdGrade);

                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Grade.Excluir();
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
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }

        //Produtos da Grade de Comodato        
        private void PopularGridItens()
        {
            TabComodItens = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO FROM GRADECOMODATOITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_GRADE=" + Grade.IdGrade.ToString());
            Source_ComodItens.DataSource = TabComodItens;
            Source_ComodItens.DataMember = TabComodItens.Tables[0].TableName;
            GridItens.DataSource = Source_ComodItens;
            NavegadorComod.BindingSource = Source_ComodItens;
            int item = Source_ComodItens.Find("ID_Lanc", GradeItens.IdLanc);
            Source_ComodItens.Position = item;
        }
        private void GridItemComod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    IncluirItemComod();
                }
            }
        }
        private void GridItemComod_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_ComodItens.CancelEdit();
                e.Cancel = true;
            }

            if (Grade.IdGrade == 0)
            {
                Source_ComodItens.CancelEdit();
                e.Cancel = true;
            }

        }
        private void GridItemComod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Grade.IdGrade > 0)
                {
                    GradeItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    GradeItens.GravarDados();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnIncComod_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Grade.IdGrade > 0)
                    IncluirItemComod();
            }
        }
        private void BtnExcComod_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Grade.IdGrade > 0)
                {

                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        GradeItens.IdLanc = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                        GradeItens.Excluir();
                        GradeItens.IdLanc = 0;
                        PopularGridItens();
                    }

                }
            }
        }
        private void IncluirItemComod()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Grade.IdGrade > 0)
                {

                    FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                    BuscaPrd.FrmPrincipal = this.FrmPrincipal;
                    BuscaPrd.IdProduto = 0;
                    BuscaPrd.ShowDialog();
                    if (BuscaPrd.IdProduto > 0)
                    {                        
                        GradeItens.IdLanc = 0;
                        GradeItens.IdGrade = Grade.IdGrade;
                        GradeItens.IdProduto = BuscaPrd.IdProduto;
                        GradeItens.GravarDados();
                        PopularGridItens();


                    }
                    else
                        Source_ComodItens.CancelEdit();
                    BuscaPrd.Dispose();
                }
            }
        }
                
        //Produtos com Vinculo no Comodato
        private void PopularGridItensVinc()
        {
            TabComodVinc = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO FROM GRADECOMODATOVINC T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_GRADE=" + Grade.IdGrade.ToString());
            Source_ComodVinc.DataSource = TabComodVinc;
            Source_ComodVinc.DataMember = TabComodVinc.Tables[0].TableName;
            GridVinc.DataSource = Source_ComodVinc;
            NavegadorVinc.BindingSource = Source_ComodVinc;
            int item = Source_ComodVinc.Find("ID_Lanc", GradeVinc.IdLanc);
            Source_ComodVinc.Position = item;
        }
        private void GridItemVinc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridVinc.CurrentRow == null || GridVinc.Rows.Count - 1 == GridVinc.CurrentRow.Index)
                {
                    IncluirItemComodVinc();
                }
            }
        }
        private void GridItemComodVinc_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_ComodVinc.CancelEdit();
                e.Cancel = true;
            }

            if (Grade.IdGrade == 0)
            {
                Source_ComodVinc.CancelEdit();
                e.Cancel = true;
            }

        }
        private void GridItemComodVinc_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Grade.IdGrade > 0)
                {
                    GradeVinc.LerDados(int.Parse(GridVinc.CurrentRow.Cells[0].Value.ToString()));
                    GradeVinc.GravarDados();
                    PopularGridItensVinc();
                    GridVinc.CurrentCell = GridVinc.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnIncComodVinc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Grade.IdGrade > 0)
                    IncluirItemComodVinc();
            }
        }
        private void BtnExcComodVinc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Grade.IdGrade > 0)
                {

                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        GradeVinc.IdLanc = int.Parse(GridVinc.CurrentRow.Cells[0].Value.ToString());
                        GradeVinc.Excluir();
                        GradeVinc.IdLanc = 0;
                        PopularGridItensVinc();
                    }

                }
            }
        }
        private void IncluirItemComodVinc()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro da Grade em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Grade.IdGrade > 0)
                {

                    FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                    BuscaPrd.FrmPrincipal = this.FrmPrincipal;
                    BuscaPrd.IdProduto = 0;
                    BuscaPrd.ShowDialog();
                    if (BuscaPrd.IdProduto > 0)
                    {
                        GradeVinc.IdLanc = 0;
                        GradeVinc.IdGrade = Grade.IdGrade;
                        GradeVinc.IdProduto = BuscaPrd.IdProduto;
                        GradeVinc.GravarDados();
                        PopularGridItensVinc();
                    }
                    else
                        Source_ComodVinc.CancelEdit();
                    BuscaPrd.Dispose();
                }
            }
        }
    }
}
