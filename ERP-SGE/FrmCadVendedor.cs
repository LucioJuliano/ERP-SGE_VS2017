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
    public partial class FrmCadVendedor : Form
    {
        Funcoes Controle = new Funcoes();
        Vendedores Vendedor = new Vendedores();
        CotaGrupoPrdVend CotaGrdPrd = new CotaGrupoPrdVend();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        private DataSet TabItens;
        private BindingSource Source_Itens;

        public FrmCadVendedor()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao    = FrmPrincipal.Conexao;
            Vendedor.Controle   = Controle;
            CotaGrdPrd.Controle = Controle;
            Vendedor.IdVendedor = 0;
            ChkNome.Checked     = true;
            TabItens            = new DataSet();
            Source_Itens        = new BindingSource();
            PopularGrid();            
            LstUsuario    = FrmPrincipal.PopularCombo("SELECT Id_Usuario,Usuario FROM Usuarios where telemarketing=1 ORDER BY Usuario", LstUsuario, "Nenhum");
            LstGrupoVenda = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 and CotaFinanceira=1", LstGrupoVenda, "Nenhum");
            ColGrupoPrd   = FrmPrincipal.PopularComboGrid("SELECT Id_Grupo,Grupo FROM GrupoProduto ORDER BY Grupo", ColGrupoPrd);
            //ColGrupoAux   = FrmPrincipal.PopularComboGrid("SELECT Id_Grupo,Grupo FROM GrupoProduto ORDER BY Grupo", ColGrupoAux);
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Vendedor,Vendedor FROM Vendedores ORDER BY Vendedor");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Vendedor", Vendedor.IdVendedor);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Vendedor.LerDados(Isn);
            TxtCodigo.Text              = Vendedor.IdVendedor.ToString();
            TxtVendedor.Text            = Vendedor.Vendedor;
            TxtComissao.Value           = Vendedor.Comissao;
            TxtFone.Text                = Vendedor.Telefone;
            TxtCelular.Text             = Vendedor.Celular;
            TxtEmail.Text               = Vendedor.Email;
            Ck_Ativo.Checked            = Vendedor.Ativo == 1;
            Ck_Relatorios.Checked       = Vendedor.EntraRel == 1;
            Cb_Distribuidor.Checked     = Vendedor.Distribuidor == 1;            
            LstUsuario.SelectedValue    = Vendedor.IdUsuario.ToString();
            LstGrupoVenda.SelectedValue = Vendedor.IdVendGrupo.ToString();
            TxtVlrFinanc1.Value         = Vendedor.Financeiro1;
            TxtPercFinanc1.Value        = Vendedor.PercFinanc1;
            TxtVlrRentab1.Value         = Vendedor.Rentabilidade1;            
            TxtCliente1.Value           = Vendedor.Cliente1;            
            TxtVlrFinanc2.Value         = Vendedor.Financeiro2;
            TxtPercFinanc2.Value        = Vendedor.PercFinanc2;
            TxtVlrFinanc3.Value         = Vendedor.Financeiro3;
            TxtPercFinanc3.Value        = Vendedor.PercFinanc3;
            TxtVlrFinanc4.Value         = Vendedor.Financeiro4;
            TxtPercFinanc4.Value        = Vendedor.PercFinanc4;
            TxtVlrRentab.Value          = Vendedor.VlrRentab;
            TxtVlrCliente.Value         = Vendedor.VlrCliente;
            TxtVlrGrdClientes.Value     = Vendedor.VlrGrdClientes;
            TxtVlrGradePrd1.Value       = Vendedor.VlrGradePrd1;
            TxtVlrGradePrd2.Value       = Vendedor.VlrGradePrd2;
            TxtVlrGradePrd3.Value       = Vendedor.VlrGradePrd3;
            TxtGradeClientes.Value      = Vendedor.GradeClientes;
            TxtVlrReAtivClie.Value      = Vendedor.VlrReAtivClie;
            TxtPercEntrega.Value        = Vendedor.PercEntrega;
            TxtVlrPercEntrega.Value     = Vendedor.VlrPercEntrega;
            Ck_CotaFinanceira.Checked   = Vendedor.CotaFinanceira == 1;
            PopularGridItens();
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtVendedor.Focus();
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
                TxtVendedor.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtVendedor.Text.Trim() != "")
            {
                Vendedor.IdVendedor  = int.Parse(TxtCodigo.Text);
                Vendedor.Vendedor    = TxtVendedor.Text;
                Vendedor.Comissao    = TxtComissao.Value;
                Vendedor.Telefone    = TxtFone.Text;
                Vendedor.Celular     = TxtCelular.Text;
                Vendedor.Email       = TxtEmail.Text;                
                Vendedor.IdUsuario   = int.Parse(LstUsuario.SelectedValue.ToString());
                Vendedor.IdVendGrupo = int.Parse(LstGrupoVenda.SelectedValue.ToString());
                if (Ck_Ativo.Checked) Vendedor.Ativo = 1; else Vendedor.Ativo = 0;
                if (Ck_Relatorios.Checked) Vendedor.EntraRel           = 1; else Vendedor.EntraRel = 0;
                if (Cb_Distribuidor.Checked) Vendedor.Distribuidor     = 1; else Vendedor.Distribuidor = 0;
                if (Ck_CotaFinanceira.Checked) Vendedor.CotaFinanceira = 1; else Vendedor.CotaFinanceira = 0;
                Vendedor.Financeiro1 = TxtVlrFinanc1.Value;
                Vendedor.PercFinanc1 = TxtPercFinanc1.Value;
                Vendedor.Rentabilidade1 = TxtVlrRentab1.Value;                
                Vendedor.Cliente1 = int.Parse(TxtCliente1.Value.ToString());                
                Vendedor.Financeiro2 = TxtVlrFinanc2.Value;
                Vendedor.PercFinanc2 = TxtPercFinanc2.Value;
                Vendedor.Financeiro3 = TxtVlrFinanc3.Value;
                Vendedor.PercFinanc3 = TxtPercFinanc3.Value;
                Vendedor.Financeiro4 = TxtVlrFinanc4.Value;
                Vendedor.PercFinanc4 = TxtPercFinanc4.Value;
                Vendedor.VlrRentab = TxtVlrRentab.Value;
                Vendedor.VlrCliente = TxtVlrCliente.Value;
                Vendedor.VlrGrdClientes = TxtVlrGrdClientes.Value;
                Vendedor.VlrGradePrd1   = TxtVlrGradePrd1.Value;
                Vendedor.VlrGradePrd2   = TxtVlrGradePrd2.Value;
                Vendedor.VlrGradePrd3   = TxtVlrGradePrd3.Value;
                Vendedor.GradeClientes  = int.Parse(TxtGradeClientes.Value.ToString());
                Vendedor.VlrReAtivClie  = TxtVlrReAtivClie.Value;
                Vendedor.PercEntrega    = TxtPercEntrega.Value;
                Vendedor.VlrPercEntrega = TxtVlrPercEntrega.Value;
                Vendedor.GravarDados();
                PopularGrid();
                PopularCampos(Vendedor.IdVendedor);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Vendedor não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtVendedor.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Vendedor.IdVendedor = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Vendedor.Excluir();
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
            TxtVendedor.Text  = "";
            TxtComissao.Value = 0;
            TxtFone.Text      = "";
            TxtCelular.Text   = "";
            TxtEmail.Text     = "";            
            LstUsuario.SelectedValue    = "0";
            LstGrupoVenda.SelectedValue = "0";
            Ck_Ativo.Checked        = false;
            Ck_Relatorios.Checked   = false;
            Cb_Distribuidor.Checked = false;
            TxtVlrFinanc1.Value     = 0;
            TxtPercFinanc1.Value    = 0;
            TxtVlrRentab1.Value     = 0;            
            TxtCliente1.Value       = 0;            
            TxtVlrFinanc2.Value     = 0;
            TxtPercFinanc2.Value    = 0;
            TxtVlrFinanc3.Value     = 0;
            TxtPercFinanc3.Value    = 0;
            TxtVlrFinanc4.Value     = 0;
            TxtPercFinanc4.Value    = 0;
            TxtVlrRentab.Value      = 0;
            TxtVlrCliente.Value     = 0;
            TxtVlrGrdClientes.Value = 0;
            TxtVlrGradePrd1.Value   = 0;
            TxtVlrGradePrd2.Value   = 0;
            TxtVlrGradePrd3.Value   = 0;
            TxtGradeClientes.Value  = 0;
            TxtVlrReAtivClie.Value  = 0;
            TxtPercEntrega.Value    = 0;
            TxtVlrPercEntrega.Value = 0;
            Ck_CotaFinanceira.Checked = false;
            Vendedor.LerDados(0);
            PopularGridItens();
            
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
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Vendedor,Vendedor FROM Vendedores WHERE Id_Vendedor={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Vendedor,Vendedor FROM Vendedores WHERE Vendedor LIKE '%{0}%' order by Vendedor", TxtPesquisa.Text.Trim()));
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


        //Grade de produtos        
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela("SELECT ID_LANC,ID_GRUPO,TpPremio,VLRCOTA,VlrPremio,VLRCOTA2,VlrPremio2 FROM COTAGRUPOPRDVENDEDOR  WHERE ID_VENDEDOR=" + Vendedor.IdVendedor.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource    = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("ID_Lanc", CotaGrdPrd.IdLanc);
            Source_Itens.Position = item;
        }
        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    IncluirItem();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro de Vendedor em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }

            if (Vendedor.IdVendedor == 0)
            {
                Source_Itens.CancelEdit();
                e.Cancel = true;

            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Vendedor.IdVendedor > 0)
                {
                    int IdGrupo    = int.Parse(GridItens.CurrentRow.Cells[1].Value.ToString());
                    string TpPrem  = GridItens.CurrentRow.Cells[2].Value.ToString();
                    decimal Valor  = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                    decimal VrPremio = decimal.Parse(GridItens.CurrentRow.Cells[4].Value.ToString());
                    decimal Valor2 = decimal.Parse(GridItens.CurrentRow.Cells[5].Value.ToString());
                    decimal VrPremio2 = decimal.Parse(GridItens.CurrentRow.Cells[6].Value.ToString());

                    CotaGrdPrd.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    CotaGrdPrd.IdGrupo    = IdGrupo;
                    CotaGrdPrd.TpPremio   = TpPrem; ;
                    CotaGrdPrd.VlrCota    = Valor;                    
                    CotaGrdPrd.VlrPremio  = VrPremio;
                    CotaGrdPrd.VlrCota2   = Valor2;
                    CotaGrdPrd.VlrPremio2 = VrPremio2;
                    CotaGrdPrd.GravarDados();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Vendedor em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Vendedor.IdVendedor > 0)
                    IncluirItem();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Vendedor em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Vendedor.IdVendedor > 0)
                {

                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        CotaGrdPrd.IdLanc = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                        CotaGrdPrd.Excluir();
                        CotaGrdPrd.IdLanc = 0;
                        PopularGridItens();
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Vendedor em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Vendedor.IdVendedor > 0)
                {
                    CotaGrdPrd.LerDados(0);
                    CotaGrdPrd.IdLanc     = 0;
                    CotaGrdPrd.IdVendedor = Vendedor.IdVendedor;
                    CotaGrdPrd.GravarDados();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[1];

                }
                else
                    Source_Itens.CancelEdit();
            }
        }

    }
}

