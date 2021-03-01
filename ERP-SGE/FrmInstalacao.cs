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
    public partial class FrmInstalacao : Form
    {
        Funcoes Controle = new Funcoes();
        ControleInstalacao MvInstalacao = new ControleInstalacao();
        Pessoas CadPessoa = new Pessoas();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private string sSQLFiltro = "";

        public FrmInstalacao()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            MvInstalacao.Controle = Controle;
            CadPessoa.Controle = Controle;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            Chk_Periodo.Checked = true;
            Rb_Todos.Checked = true;
            TxtPesqIdPessoa.Text = "0";
            LstVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstVendedor);
            LstPesqVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstPesqVendedor);
            LstPesqVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();
            //LstPesqVendedor.Enabled = FrmPrincipal.Perfil_Usuario.SeusMov == 0;
            PopularGrid();
        }
        private void PopularGrid()
        {
            string Filtro = "";
            if (Rb_Todos.Checked)
                Filtro = "WHERE  T1.STATUS IN (0,1)";
            else
            {
                if (Rb_Concluido.Checked)
                    Filtro = "WHERE  T1.STATUS=1";
                else
                    Filtro = "WHERE  T1.STATUS=0";
            }
            if (Chk_Periodo.Checked)
            {
                if (Rb_Concluido.Checked)
                    Filtro = Filtro + " AND T1.DTConcluido >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTConcluido <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                else
                    Filtro = Filtro + " AND T1.DTPREVISTA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTPREVISTA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
            }
            
            if (int.Parse(TxtPesqIdPessoa.Text) > 0)
                Filtro = Filtro + " AND T1.ID_PESSOA=" + TxtPesqIdPessoa.Text;
            if (int.Parse(LstPesqVendedor.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Vendedor=" + LstPesqVendedor.SelectedValue.ToString();

            string sSQL = "SELECT T1.ID_LANC,CASE T1.STATUS WHEN 0 THEN 'Pendente' WHEN 1 THEN 'Concluido' END AS STATUS,T1.DATA,T1.ID_VENDA,T2.RAZAOSOCIAL," +
                          " T3.VENDEDOR,T1.DTPREVISTA,T1.DTCONCLUIDO,T1.CONTATO,T1.TELEFONE FROM CONTROLEINSTALACAO T1" +
                          " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                          " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR) " + Filtro;

            sSQLFiltro = "SELECT T1.ID_LANC,CASE T1.STATUS WHEN 0 THEN 'Pendente' WHEN 1 THEN 'Concluido' END AS STATUS,T1.DATA,T1.ID_VENDA,T2.RAZAOSOCIAL," +
                         " T3.VENDEDOR,T1.DTPREVISTA,T1.DTCONCLUIDO,T1.ENDERECO,T1.CONTATO,T1.TELEFONE,T1.OBSERVACAO,T1.SERVICO,T1.QTDEEQUIP FROM CONTROLEINSTALACAO T1" +
                         " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                         " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)" + Filtro;
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(sSQL);
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_LANC", MvInstalacao.IdLanc);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            MvInstalacao.LerDados(Isn);
            TxtCodigo.Text = MvInstalacao.IdLanc.ToString();
            TxtData.Value = MvInstalacao.Data;
            SetaPessoa(MvInstalacao.IdPessoa);            
            TxtNumVenda.Value= MvInstalacao.IdVenda;
            TxtDtPrevisao.Value = MvInstalacao.DtPrevista;
            LstVendedor.SelectedValue = MvInstalacao.IdVendedor.ToString();
            TxtObservacao.Text = MvInstalacao.Observacao;
            TxtServico.Text = MvInstalacao.Servico;
            Chk_Concluido.Checked = MvInstalacao.Status == 1;
            TxtDtConcluido.Visible = MvInstalacao.Status == 1;
            TxtEndereco.Text = MvInstalacao.Endereco;
            TxtContato.Text = MvInstalacao.Contato;
            TxtTelefone.Text = MvInstalacao.Telefone;
            TxtQtdeEquip.Value = MvInstalacao.QtdeEquip;
            if (Chk_Concluido.Checked)
            {
                MvInstalacao.Status = 1;
                TxtDtConcluido.Value = MvInstalacao.DtConcluido;
            }
            else
                MvInstalacao.Status = 0;
            
            
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlterarInstalacao == 0)
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                StaFormEdicao = true;
                Paginas.SelectTab(1);
                LimpaDados();
                FrmPrincipal.ControleBotoes(true);
            }
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (FrmPrincipal.Perfil_Usuario.AlterarInstalacao == 0)
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtCodCliente.Text.Trim() != "0")
            {                
                MvInstalacao.Data      = TxtData.Value;
                MvInstalacao.IdPessoa   = int.Parse(TxtCodCliente.Text);
                MvInstalacao.IdVenda    = int.Parse(TxtNumVenda.Value.ToString());
                MvInstalacao.DtPrevista = TxtDtPrevisao.Value;
                MvInstalacao.IdVendedor = int.Parse(LstVendedor.SelectedValue.ToString()); 
                MvInstalacao.Observacao = TxtObservacao.Text;
                MvInstalacao.Servico    = TxtServico.Text;
                MvInstalacao.Endereco   = TxtEndereco.Text;
                MvInstalacao.Contato    = TxtContato.Text;
                MvInstalacao.Telefone   = TxtTelefone.Text;
                MvInstalacao.QtdeEquip  = int.Parse(TxtQtdeEquip.Value.ToString());
                if (Chk_Concluido.Checked)
                    MvInstalacao.Status = 1;
                else
                    MvInstalacao.Status = 0;
                MvInstalacao.GravarDados();

                if (MvInstalacao.Status == 0)
                    Controle.ExecutaSQL("UPDATE ControleInstalacao SET DTConcluido=NULL WHERE ID_LANC=" + MvInstalacao.IdLanc.ToString());
                else
                    Controle.ExecutaSQL("UPDATE ControleInstalacao SET DTConcluido=CONVERT(DATETIME,'" + TxtDtConcluido.Value.Date.ToShortDateString() + "',103) WHERE ID_LANC=" + MvInstalacao.IdLanc.ToString());
                PopularGrid();
                PopularCampos(MvInstalacao.IdLanc);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Favor informar o Cliente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtData.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (FrmPrincipal.Perfil_Usuario.AlterarInstalacao == 0)
                    MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MvInstalacao.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        MvInstalacao.Excluir();
                        PopularGrid();
                        LimpaDados();
                        GridDados.Focus();
                    }
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
        }
        private void LimpaDados()
        {
            TxtCodigo.Text = "0";            
            TxtData.Value = DateTime.Now;
            SetaPessoa(0);            
            TxtServico.Text = "";
            TxtNumVenda.Value = 0;
            TxtObservacao.Text = "";
            TxtEndereco.Text = "";
            TxtContato.Text = "";
            TxtTelefone.Text = "";
            TxtDtPrevisao.Value = DateTime.Now.AddDays(2);            
            Chk_Concluido.Checked = false;
            TxtDtConcluido.Visible = false;
            LstVendedor.SelectedValue = "0";
            TxtQtdeEquip.Value = 0;
            MvInstalacao.LerDados(0);
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
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
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
        private void SetaPessoa(int IdPessoa)
        {
            CadPessoa.LerDados(IdPessoa);
            if (CadPessoa.IdPessoa > 0)
            {
                TxtCodCliente.Text = CadPessoa.IdPessoa.ToString();
                TxtCliente.Text = CadPessoa.RazaoSocial;
            }
            else
            {
                TxtCodCliente.Text = "0";
                TxtCliente.Text = "";
            }
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelMapInstalacao RelInstalacao = new Relatorios.RelMapInstalacao();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(sSQLFiltro + " ORDER BY T1.DTPREVISTA");
            RelInstalacao.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelInstalacao;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelInstalacao.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelInstalacao.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;
        }
        private void BtnPessoa_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
                BuscaPessoa.ShowDialog();
                if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                {
                    TxtCodCliente.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                    TxtCliente.Text = BuscaPessoa.CadPessoa.RazaoSocial;
                    TxtEndereco.Text = BuscaPessoa.CadPessoa.Endereco.Trim() + "," + BuscaPessoa.CadPessoa.Numero.Trim() + " - " + BuscaPessoa.CadPessoa.Bairro.Trim();
                    TxtContato.Text = BuscaPessoa.CadPessoa.Contato.Trim();
                    TxtTelefone.Text = BuscaPessoa.CadPessoa.Fone.Trim();
                }
                else
                {
                    TxtCodCliente.Text = "0";
                    TxtCliente.Text = "";
                    TxtEndereco.Text = "";
                    TxtContato.Text = "";
                    TxtTelefone.Text = "";
                }
                BuscaPessoa.Dispose();
            }
        }
        private void BtnPesqPessoa_Click(object sender, EventArgs e)
        {
            FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            BuscaPessoa.ShowDialog();
            if (BuscaPessoa.CadPessoa.IdPessoa > 0)
            {
                TxtPesqIdPessoa.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                TxtPesqCliente.Text = BuscaPessoa.CadPessoa.RazaoSocial;
            }
            else
            {
                TxtPesqIdPessoa.Text = "0";
                TxtPesqCliente.Text = "";
                
            }
            BuscaPessoa.Dispose();
        }
        private void Chk_Concluido_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Concluido.Checked)
            {
                TxtDtConcluido.Visible = true;
                TxtDtConcluido.Value = DateTime.Now;
            }
            else
                TxtDtConcluido.Visible = false;

        }
        private void TxtNumVenda_Validated(object sender, EventArgs e)
        {
            MvVenda Vendas = new MvVenda();
            Vendas.Controle = Controle;
            if (StaFormEdicao)
            {
                if (TxtNumVenda.Value > 0)
                {
                    Vendas.LerDados(int.Parse(TxtNumVenda.Value.ToString()));
                    if (Vendas.IdVenda == 0)
                    {
                        MessageBox.Show("Atenção: Venda não localizada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        TxtNumVenda.Value = 0;
                    }
                    else
                    {
                        LstVendedor.SelectedValue = Vendas.IdVendedor.ToString();
                        SetaPessoa(Vendas.IdPessoa);
                        TxtTelefone.Text = Vendas.Fone.Trim();
                        TxtEndereco.Text = Vendas.Endereco.Trim();                        
                    }
                }
            }
        }
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();            
            if (e.ColumnIndex == 9)
            {
                MaskCol.Mask = "(00) 0000-0000";
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            MaskCol.Dispose();
        }
    }
}
