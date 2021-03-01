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
    public partial class FrmLancLivroCxa : Form
    {
        Funcoes Controle = new Funcoes();
        MvContaCaixa MvLivroCxa = new MvContaCaixa();
        Pessoas CadPessoa = new Pessoas();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private string sSQLFiltro = "";

        public FrmLancLivroCxa()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            MvLivroCxa.Controle = Controle;
            CadPessoa.Controle = Controle;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            Chk_Periodo.Checked = true;                        
            LstPesqCaixa = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstPesqCaixa);
            LstCaixa     = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstCaixa);
            LstCaixaDest = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstCaixaDest);
            LstTpDocDest = FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstTpDocDest);
            LstTipoDoc   = FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstTipoDoc);
            LstAgente    = FrmPrincipal.PopularCombo("SELECT ID_Agente,Agente FROM AgenteCobrador ORDER BY Agente", LstAgente);
            PopularGrid();
        }
        private void PopularGrid()
        {
            string Filtro = " WHERE 1=1";            
            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
            if (TxtPesqPessoa.Text.Trim() !="")
                Filtro = Filtro + " AND T3.RAZAOSOCIAL LIKE '%" + TxtPesqPessoa.Text.Trim()+"%'";
            if (TxtPesqValor.Value > 0 && TxtPesqValor.Text.Trim() != "")
                Filtro = Filtro + " AND T1.VALOR=" + Controle.FloatToStr(TxtPesqValor.Value, 2);
            if (int.Parse(LstPesqCaixa.SelectedValue.ToString())!=0)
                Filtro = Filtro + " AND T1.ID_CAIXA=" + LstPesqCaixa.SelectedValue.ToString();

            string sSQL = "SELECT T1.ID_LANC,T2.CAIXA,CASE T1.STATUS WHEN 0 THEN ' ' ELSE 'Canc.' END AS STATUS, T1.DATA,T3.RAZAOSOCIAL,T1.DESCRICAO,T4.DOCUMENTO,CASE T1.TPLANC WHEN 1 THEN T1.VALOR ELSE 0 END AS DEBITO," +
                          "  CASE T1.TPLANC WHEN 2 THEN T1.VALOR ELSE 0 END AS CREDITO FROM MVCONTACAIXA T1 " +
                          " LEFT JOIN CONTACAIXA T2 ON (T2.ID_CAIXA=T1.ID_CAIXA)" +
                          " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T1.ID_PESSOA)" +
                          " LEFT JOIN TIPODOCUMENTO T4 ON (T4.ID_DOCUMENTO=T1.ID_DOCUMENTO)" + Filtro;
            sSQLFiltro = sSQL;
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(sSQL);
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_LANC", MvLivroCxa.IdLanc);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);            
            MvLivroCxa.LerDados(Isn);
            TxtCodigo.Text     = MvLivroCxa.IdLanc.ToString();
            TxtData.Value      = MvLivroCxa.Data;
            TxtValor.Value     = MvLivroCxa.Valor;
            TxtObservacao.Text = MvLivroCxa.Observacao;
            TxtDescricao.Text  = MvLivroCxa.Descricao;
            LstTipoDoc.SelectedValue   = MvLivroCxa.IdDocumento;
            LstTpDocDest.SelectedValue = MvLivroCxa.IdTpDocDest;
            LstCaixa.SelectedValue     = MvLivroCxa.IdCaixa;
            LstCaixaDest.SelectedValue = MvLivroCxa.IdCaixaDest;
            LstAgente.SelectedValue    = MvLivroCxa.IdAgente;
            if (MvLivroCxa.TpLanc == 1)
                Rb_Despesa.Checked = true;
            else
                Rb_Receita.Checked = true;
            SetaPessoa(MvLivroCxa.IdPessoa);            
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                MessageBox.Show("Registro não pode ser alterado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            /*PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
            StaFormEdicao = true;
            FrmPrincipal.ControleBotoes(true);*/
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstTipoDoc.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor Informar o tipo de documento financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (int.Parse(LstAgente.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor Informar o Agente Cobrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (int.Parse(LstCaixa.SelectedValue.ToString()) != 0)
            {
                if (int.Parse(LstCaixaDest.SelectedValue.ToString()) > 0)
                {
                    if (int.Parse(LstCaixaDest.SelectedValue.ToString()) == int.Parse(LstCaixa.SelectedValue.ToString()))
                        MessageBox.Show("Caixa Destino igual o de origem", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (int.Parse(LstTpDocDest.SelectedValue.ToString()) == 0)
                    {
                        MessageBox.Show("Informar o Documento Financeiro de Destino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (MessageBox.Show("Confirma a Tranferência", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                if (MvLivroCxa.StatusLivroCxa(int.Parse(LstCaixa.SelectedValue.ToString()),TxtData.Value) == 1)
                {
                    MessageBox.Show("Atenção: Data do Livro Caixa encerrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MvLivroCxa.IdLanc      = int.Parse(TxtCodigo.Text);
                MvLivroCxa.Data        = TxtData.Value;
                MvLivroCxa.IdCaixa     = int.Parse(LstCaixa.SelectedValue.ToString());                
                MvLivroCxa.IdDocumento = int.Parse(LstTipoDoc.SelectedValue.ToString());
                MvLivroCxa.Observacao  = TxtObservacao.Text;
                MvLivroCxa.IdPessoa    = int.Parse(TxtCodPessoa.Text);
                MvLivroCxa.Valor       = TxtValor.Value;
                MvLivroCxa.IdUsuario   = FrmPrincipal.Perfil_Usuario.IdUsuario;
                MvLivroCxa.IdAgente    = int.Parse(LstAgente.SelectedValue.ToString());
                if (int.Parse(LstCaixaDest.SelectedValue.ToString()) > 0)
                {
                    MvLivroCxa.TpLanc      = 1;
                    MvLivroCxa.IdCaixaDest = int.Parse(LstCaixaDest.SelectedValue.ToString());
                    MvLivroCxa.IdTpDocDest = int.Parse(LstTpDocDest.SelectedValue.ToString());
                    MvLivroCxa.Descricao   = "Tranferência (Saida): " + TxtDescricao.Text;
                }
                else
                {
                    MvLivroCxa.Descricao = TxtDescricao.Text;
                    if (Rb_Despesa.Checked) MvLivroCxa.TpLanc = 1; else MvLivroCxa.TpLanc = 2;
                }

                MvLivroCxa.GravarDados();
                MvLivroCxa.Atlz_SaldoContaCaixa(MvLivroCxa.TpLanc, MvLivroCxa.IdCaixa, MvLivroCxa.IdDocumento, MvLivroCxa.Data, MvLivroCxa.Valor);
                
                if (int.Parse(LstCaixaDest.SelectedValue.ToString()) > 0)
                {
                                        
                    MvLivroCxa.IdDocumento = int.Parse(LstTpDocDest.SelectedValue.ToString());
                    MvLivroCxa.IdCaixa     = int.Parse(LstCaixaDest.SelectedValue.ToString());
                    MvLivroCxa.IdCaixaDest = 0;
                    MvLivroCxa.IdTpDocDest = 0;
                    MvLivroCxa.Descricao   = " Cx:" + LstCaixa.Text.Trim() + " Lanç.:" + MvLivroCxa.IdLanc.ToString() + " Tranf.(Entrada): " + TxtDescricao.Text;
                    MvLivroCxa.TpLanc      = 2;
                    MvLivroCxa.IdLanc      = 0;
                    MvLivroCxa.GravarDados();
                    MvLivroCxa.Atlz_SaldoContaCaixa(MvLivroCxa.TpLanc, MvLivroCxa.IdCaixa, MvLivroCxa.IdDocumento, MvLivroCxa.Data, MvLivroCxa.Valor);
                }
                PopularGrid();
                PopularCampos(MvLivroCxa.IdLanc);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Informa o Caixa de Movimentação", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LstCaixa.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                MvLivroCxa.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                if (MvLivroCxa.Status == 1)
                {
                    MessageBox.Show("Lançamento já Cancelamento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MvLivroCxa.StatusLivroCxa(MvLivroCxa.IdCaixa, MvLivroCxa.Data) == 1)
                {
                    MessageBox.Show("Atenção: Data do Livro Caixa encerrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MvLivroCxa.IdLancOrig > 0)
                {
                    MessageBox.Show("Estorno nao pode ser efetuado, Lançamento Gerado pelo Financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                /*if (MessageBox.Show("Confirma o Cancelamento do Lançamento", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {                    
                    if (MvLivroCxa.IdLanc > 0)
                    {
                        MvLivroCxa.Descricao = "ESTORNO <-> Ref. Lanç.: " + MvLivroCxa.IdLanc.ToString();
                        MvLivroCxa.IdLanc = 0;
                        MvLivroCxa.Data = DateTime.Now;
                        MvLivroCxa.IdUsuario = FrmPrincipal.Perfil_Usuario.IdUsuario;
                        if (MvLivroCxa.TpLanc == 1) MvLivroCxa.TpLanc = 2; else MvLivroCxa.TpLanc = 1;
                        MvLivroCxa.GravarDados();
                        MvLivroCxa.Estorno();
                        PopularGrid();
                        LimpaDados();
                        GridDados.Focus();
                    }
                }*/
                if (MessageBox.Show("Confirma o Cancelamento do Lançamento", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {                    
                    if (MvLivroCxa.IdLanc > 0)
                    {
                        int TpLanc = MvLivroCxa.TpLanc;
                        MvLivroCxa.Status = 1;
                        MvLivroCxa.IdUsuario = FrmPrincipal.Perfil_Usuario.IdUsuario;
                        if (MvLivroCxa.TpLanc == 1) TpLanc = 2; else TpLanc = 1;
                        MvLivroCxa.GravarDados();
                        MvLivroCxa.Atlz_SaldoContaCaixa(TpLanc, MvLivroCxa.IdCaixa, MvLivroCxa.IdDocumento, MvLivroCxa.Data, MvLivroCxa.Valor);
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
            TxtCodigo.Text             = "0";            
            TxtData.Value              = DateTime.Now;
            TxtValor.Value             = 0;
            TxtDescricao.Text          = "";
            TxtObservacao.Text         = "";
            LstTipoDoc.SelectedValue   = "0";
            LstCaixa.SelectedValue     = "0";
            LstAgente.SelectedValue    = "0";
            LstCaixaDest.SelectedValue = "0";
            LstTpDocDest.SelectedValue = "0";
            Rb_Despesa.Checked         = true;
            SetaPessoa(0);
            MvLivroCxa.LerDados(0);
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
                TxtCodPessoa.Text = CadPessoa.IdPessoa.ToString();
                TxtPessoa.Text = CadPessoa.RazaoSocial;
            }
            else
            {
                TxtCodPessoa.Text = "0";
                TxtPessoa.Text    = "";
            }
        }        
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
                BuscaPessoa.ShowDialog();
                if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                {
                    MvLivroCxa.IdPessoa = BuscaPessoa.CadPessoa.IdPessoa;
                    TxtCodPessoa.Text = MvLivroCxa.IdPessoa.ToString();
                    TxtPessoa.Text = BuscaPessoa.CadPessoa.RazaoSocial;                    
                }
                else
                {
                    TxtCodPessoa.Text = "0";
                    TxtPessoa.Text = "";
                }
                BuscaPessoa.Dispose();
            }
        }
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString().Trim() == "Canc.")
                    GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Salmon;
            }
        }

        private void BtnEnc_Click(object sender, EventArgs e)
        {
            FrmEncLivroCx Frm = new FrmEncLivroCx();
            Frm.FrmPrincipal  = FrmPrincipal;
            Frm.ShowDialog();
        }        
    }
}
