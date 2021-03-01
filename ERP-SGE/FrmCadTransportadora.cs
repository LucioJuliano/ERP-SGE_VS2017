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
    public partial class FrmCadTransportadora : Form
    {
        Funcoes Controle = new Funcoes();
        Transportadoras Transportadora = new Transportadoras();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        public FrmCadTransportadora()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Transportadora.Controle = Controle;
            Transportadora.IdTransportadora = 0;
            ChkNome.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Transportadora,RazaoSocial,Fantasia,Cnpj,Contato,Fone FROM Transportadoras ORDER BY RazaoSocial");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Transportadora", Transportadora.IdTransportadora);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            Transportadora.LerDados(Isn);
            TxtCodigo.Text = Transportadora.IdTransportadora.ToString();
            TxtRazaoSocial.Text = Transportadora.RazaoSocial;
            TxtFantasia.Text = Transportadora.Fantasia;
            TxtCnpj.Text = Transportadora.Cnpj;
            TxtInscUF.Text = Transportadora.InscUF;
            TxtCep.Text = Transportadora.Cep;
            TxtEndereco.Text = Transportadora.Endereco;
            TxtNumero.Text = Transportadora.Numero;
            TxtComplemento.Text = Transportadora.Complemento;
            TxtBairro.Text = Transportadora.Bairro;
            TxtCidade.Text = Transportadora.Cidade;
            LstUF.SelectedValue = Transportadora.IdUF.ToString();
            TxtFone.Text = Transportadora.Fone;            
            TxtFax.Text = Transportadora.Fax;
            TxtEmail.Text = Transportadora.Email;
            TxtContato.Text = Transportadora.Contato;
            TxtCelular.Text = Transportadora.Celular;
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtCnpj.Focus();
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
                TxtCnpj.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtCnpj.Text.Trim() != "")
            {
                Transportadora.IdTransportadora = int.Parse(TxtCodigo.Text);
                Transportadora.RazaoSocial = TxtRazaoSocial.Text;
                Transportadora.Fantasia = TxtFantasia.Text;
                Transportadora.Cnpj = TxtCnpj.Text;
                Transportadora.InscUF = TxtInscUF.Text;
                Transportadora.Cep = TxtCep.Text.Replace("-","");
                Transportadora.Endereco = TxtEndereco.Text;
                Transportadora.Numero = TxtNumero.Text;
                Transportadora.Complemento = TxtComplemento.Text;
                Transportadora.Bairro = TxtBairro.Text;
                Transportadora.Cidade = TxtCidade.Text;
                Transportadora.IdUF = int.Parse(LstUF.SelectedValue.ToString());
                Transportadora.Fone = TxtFone.Text;                
                Transportadora.Fax = TxtFax.Text;
                Transportadora.Email = TxtEmail.Text;
                Transportadora.Contato = TxtContato.Text;
                Transportadora.Celular = TxtCelular.Text;
                Transportadora.GravarDados();
                PopularGrid();
                PopularCampos(Transportadora.IdTransportadora);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("CNPJ não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCnpj.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Transportadora.IdTransportadora = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Transportadora.Excluir();
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
            //
            LstUF = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUF);
        }
        private void LimpaDados()
        {
            TxtCodigo.Text = "0";
            TxtRazaoSocial.Text = "";
            TxtFantasia.Text = "";
            TxtCnpj.Text = "";
            TxtInscUF.Text = "";
            TxtCep.Text = "";
            TxtEndereco.Text = "";
            TxtNumero.Text = "";
            TxtComplemento.Text = "";
            TxtBairro.Text = "";
            TxtCidade.Text = "";
            LstUF.SelectedValue = 0;
            TxtFone.Text = "";            
            TxtFax.Text = "";
            TxtEmail.Text = "";
            TxtContato.Text = "";
            TxtCelular.Text = "";
            Transportadora.LerDados(0);
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
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Transportadora,RazaoSocial,Fantasia,Cnpj,Contato,Fone FROM Transportadoras WHERE ID_Transportadora={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Transportadora,RazaoSocial,Fantasia,Cnpj,Contato,Fone FROM Transportadoras WHERE RazaoSocial LIKE '%{0}%' order by RazaoSocial", TxtPesquisa.Text.Trim()));
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
        private void TxtCnpj_Validated(object sender, EventArgs e)
        {
            if (TxtCnpj.Text != "")
            {
                if (!Controle.ValidarCnpj(TxtCnpj.Text))
                {
                    MessageBox.Show("CNPJ inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtCnpj.Focus();
                }                
            }
        }

        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();
            if (e.ColumnIndex == 3)
            {
                MaskCol.Mask = "00,000,000/0000-00";
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            else if (e.ColumnIndex == 5)
            {
                MaskCol.Mask = "(00) 0000-0000";
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            MaskCol.Dispose();
        }

        private void TxtCep_Validated(object sender, EventArgs e)
        {
            if (TxtCep.Text.Replace("-", "").Trim() != "")
            {
                if (TxtCep.Text.Replace("-", "").Trim() != Transportadora.Cep.Trim())
                {
                    Verificar VerificaUF = new Verificar();
                    VerificaUF.Controle = Controle;
                    ConsultaCEP ConsultaCEP = new ConsultaCEP();
                    ConsultaCEP.VerificaCEP(TxtCep.Text);
                    TxtEndereco.Text = ConsultaCEP.Tipo + " " + ConsultaCEP.Endereco;
                    TxtBairro.Text = ConsultaCEP.Bairro;
                    TxtCidade.Text = ConsultaCEP.Cidade;
                    LstUF.SelectedValue = VerificaUF.Busca_IdUF(ConsultaCEP.UF);
                }
            }
        }        
    }
}