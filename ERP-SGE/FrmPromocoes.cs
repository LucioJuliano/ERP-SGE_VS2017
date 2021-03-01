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
using System.Net; 
using System.Net.Mail;
namespace ERP_SGE
{
    public partial class FrmPromocoes : Form
    {

        Funcoes Controle = new Funcoes();
        Promocoes Promocao = new Promocoes();
        Auditoria RegAuditoria = new Auditoria();
        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto(); 
        Produtos CadPrd = new Produtos();
        
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private BindingSource Source_Promocao;

        public FrmPromocoes()
        {
            InitializeComponent();
        }


        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao      = FrmPrincipal.Conexao;
            Promocao.Controle     = Controle;
            CadPrd.Controle       = Controle;
            RegAuditoria.Controle = Controle;
            Source_Promocao       = new BindingSource();
            BuscaPrd.FrmPrincipal = this.FrmPrincipal;
            Chk_Periodo.Checked   = false;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            PopularGrid();
        }

        private void PopularGrid()
        {
            string Filtro = " WHERE 1=1";

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND CONVERT(DATETIME,T1.DTINICIO,103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,T1.DTFINAL,103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqPrd.Text.Trim() != "")
                Filtro = Filtro + " AND T2.DESCRICAO LIKE '%" + TxtPesqPrd.Text.Trim() + "%'";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.CodPromocao,T2.Referencia,T2.Descricao,T1.DTINICIO,T1.DTFINAL FROM PROMOCOES T1 "+
                                             " LEFT JOIN Produtos T2 ON (T2.Id_Produto=T1.ID_PRODUTO) "+Filtro + " ORDER BY T2.DESCRICAO,T1.DTINICIO DESC");
            Source_Promocao.DataSource = Tabela;
            Source_Promocao.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source_Promocao;
            int item = Source_Promocao.Find("Id_Lanc", Promocao.IdLanc);
            Source_Promocao.Position = item;

        }

        private void PopularCampos(int Isn)
        {
            Promocao.LerDados(Isn,"");
            TxtCodigo.Text          = string.Format("{0:D5}", int.Parse(Promocao.IdLanc.ToString()));
            txtDtInicio.Value       = Promocao.DtInicio;
            txtDtFinal.Value        = Promocao.DtFinal;
            TxtQtde.Value           = Promocao.Qtde;
            TxtQtdeVenda.Value      = Promocao.QtdeVenda;
            TxtPrcVarejo.Value      = Promocao.PrcVarejo;
            TxtPrcMinimo.Value      = Promocao.PrcMinimo;
            TxtPrcAtacado.Value     = Promocao.PrcAtacado;
            TxtPrcEspecial.Value    = Promocao.PrcEspecial;
            TxtCodPromocao.Text     = Promocao.CodPromocao;
            Ck_VerifEstoque.Checked = Promocao.VerifSldGeral == 1;
            Rb_Todos.Checked        = Promocao.Distribuidor == 0;
            Rb_Distrib.Checked      = Promocao.Distribuidor == 1;
            Rb_Clientes.Checked     = Promocao.Distribuidor == 2;
            TxtObservacao.Text      = Promocao.Observacao;
            SetaProduto(Promocao.IdProduto);
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
            if (int.Parse(TxtCodPrd.Text) > 0)
            {
                if (TxtCodPromocao.Text.Trim() == "")
                {
                    MessageBox.Show("Favor informar o Codigo da Promoção", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Promocao.IdLanc      = int.Parse(TxtCodigo.Text);
                Promocao.IdProduto   = int.Parse(TxtCodPrd.Text);
                Promocao.DtInicio    = txtDtInicio.Value;
                Promocao.DtFinal     = txtDtFinal.Value;
                Promocao.Qtde        = TxtQtde.Value;
                Promocao.PrcVarejo   = TxtPrcVarejo.Value;
                Promocao.PrcMinimo   = TxtPrcMinimo.Value;
                Promocao.PrcAtacado  = TxtPrcAtacado.Value;
                Promocao.PrcEspecial = TxtPrcEspecial.Value;
                Promocao.CodPromocao = TxtCodPromocao.Text;
                Promocao.Observacao  = TxtObservacao.Text;
                if (Ck_VerifEstoque.Checked) Promocao.VerifSldGeral = 1; else Promocao.VerifSldGeral = 0;
                if (Rb_Todos.Checked) Promocao.Distribuidor = 0; else if (Rb_Distrib.Checked) Promocao.Distribuidor = 1; else Promocao.Distribuidor = 2;                
                Promocao.GravarDados();

                //Registrando Movimento de Auditoria
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdLanc, TxtCodPrd.Text, 1, "Produto:" + TxtCodPrd.Text + " - " + TxtDescricao.Text);
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdLanc, TxtCodPrd.Text, 2, "Produto:" + TxtCodPrd.Text + " - " + TxtDescricao.Text);
                PopularGrid();
                PopularCampos(Promocao.IdLanc);
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Favor informar o Produto", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                Promocao.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Promocao.LerDados(Promocao.IdLanc,"");

                
                if (FrmPrincipal.Perfil_Usuario.ExcluirReg == 0)
                {
                    FrmAutorizacao Autorizacao = new FrmAutorizacao();
                    Autorizacao.FrmPrincipal = FrmPrincipal;
                    Autorizacao.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (Autorizacao.AcessoOk)
                    {
                        if (Autorizacao.Usuario.ExcluirReg == 0)
                        {
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Promocao.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Promocao.Excluir();
                    //Registrando Movimento de Auditoria
                    FrmPrincipal.RegistrarAuditoria(this.Text, Promocao.IdLanc, TxtCodPrd.Text, 3, "Excluindo");
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
            TxtCodigo.Text          = "0";            
            txtDtInicio.Value       = DateTime.Now;
            txtDtFinal.Value        = DateTime.Now;
            TxtQtde.Value           = 0;
            TxtQtdeVenda.Value      = 0;
            TxtPrcVarejo.Value      = 0;
            TxtPrcMinimo.Value      = 0;
            TxtPrcAtacado.Value     = 0;
            TxtPrcEspecial.Value    = 0;
            TxtCodPromocao.Text     = "";
            TxtObservacao.Text      = "";
            Ck_VerifEstoque.Checked = false;
            Rb_Todos.Checked        = true;
            Promocao.LerDados(0,"");
            PopularCampos(Promocao.IdLanc);
            SetaProduto(0);
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
        private void SetaProduto(int IdPrd)
        {
            CadPrd.LerDados(IdPrd);
            Promocao.IdProduto = CadPrd.IdProduto;
            TxtCodPrd.Text     = CadPrd.IdProduto.ToString();
            TxtDescricao.Text  = CadPrd.Descricao.Trim();
        }


        private void BtnBuscaProduto_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                BuscaPrd.IdProduto = 0;
                BuscaPrd.ShowDialog();
                if (BuscaPrd.IdProduto > 0)
                {
                    TxtCodPrd.Text = BuscaPrd.CadProd.IdProduto.ToString();
                    TxtDescricao.Text = BuscaPrd.CadProd.Descricao;
                }
                else
                {
                    TxtCodPrd.Text = "0";
                    TxtDescricao.Text = "";
                }
            }
        }
    }
}
