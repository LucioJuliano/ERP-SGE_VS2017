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
    public partial class FrmMovCheques : Form
    {     
        Funcoes Controle = new Funcoes();
        ControleCheque MvCheque = new ControleCheque();
        Pessoas CadPessoa = new Pessoas();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private string sSQLFiltro = "";

        public FrmMovCheques()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao   = FrmPrincipal.Conexao;
            MvCheque.Controle  = Controle;
            CadPessoa.Controle = Controle;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            Chk_Periodo.Checked = true;
            Rb_Todos.Checked = true;
            TxtPesqIdDest.Text  = "0";
            TxtPesqIdResp.Text  = "0";                        
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "Nenhuma");
            LstPesqFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqFilial, "Todas");
            PopularGrid();
        }
        private void PopularGrid()
        {
            string Filtro = "";            
            if (Rb_Todos.Checked)
                Filtro = " T1.STATUS IN (0,1)";
            else
            {   
                if (Rb_Devolvido.Checked)
                    Filtro = " T1.STATUS=1";
                else
                    Filtro = " T1.STATUS=0 AND T1.ID_PESSOADEST > 0";
            }
            if (Chk_Periodo.Checked)
            {
                if (Rb_Vinculados.Checked)
                    Filtro = Filtro + " AND T1.DTDESTINO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTDESTINO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                else
                    Filtro = Filtro + " AND T1.DTVENCIMENTO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTVENCIMENTO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
            }
            if (int.Parse(TxtPesqIdDest.Text) > 0)
                Filtro = Filtro + " AND T1.ID_PESSOADEST=" + TxtPesqIdDest.Text;
            if (int.Parse(TxtPesqIdResp.Text) > 0)
                Filtro = Filtro + " AND T1.ID_PESSOA=" + TxtPesqIdResp.Text;
            if (TxtPesqTitular.Text.Trim()!="")
                Filtro = Filtro + " AND T1.TITULAR LIKE '%" + TxtPesqTitular.Text+"%'";
            if (TxtPesqCheque.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMCHEQUE LIKE '%" + TxtPesqCheque.Text + "%'";
            if (int.Parse(LstPesqFilial.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FILIAL=" + LstPesqFilial.SelectedValue.ToString();
            if (TxtPesqValor.Value > 0 && TxtPesqValor.Text.Trim() != "")
                Filtro = Filtro + " AND T1.VALOR=" + Controle.FloatToStr(TxtPesqValor.Value, 2);
            string sSQL = "SELECT T1.ID_LANC,CASE T1.STATUS WHEN 0 THEN ' ' WHEN 1 THEN 'Devolvido' END AS STATUS, T1.DATALANC,T1.TITULAR,T1.NUMAGENCIA,T1.NUMCONTA,T1.NUMCHEQUE,T1.DTVENCIMENTO,T1.VALOR,T2.RAZAOSOCIAL AS RESPONSAVEL," +
                        " T1.DTDESTINO,T3.RAZAOSOCIAL AS DESTINATARIO,T1.DOCUMVENDA,T4.FANTASIA AS FILIAL FROM MOVCHEQUEPRE T1" +
                        " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                        " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T1.ID_PESSOADEST)"+
                        " LEFT JOIN EMPRESA_FILIAL T4 ON (T4.ID_FILIAL=T1.ID_FILIAL) WHERE " + Filtro;
            sSQLFiltro = sSQL;
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(sSQL);
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_LANC", MvCheque.IdLanc);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            MvCheque.LerDados(Isn);
            TxtCodigo.Text = MvCheque.IdLanc.ToString();
            TxtData.Value  = MvCheque.DataLanc;
            SetaPessoa(MvCheque.IdPessoa, 1);
            SetaPessoa(MvCheque.IdPessoaDest, 2);
            TxtTitular.Text = MvCheque.Titular;
            TxtNumBanco.Value = MvCheque.NumBanco;
            TxtNumAgencia.Text = MvCheque.NumAgencia;
            TxtNumConta.Text = MvCheque.NumConta;
            TxtNumCheque.Text = MvCheque.NumCheque;
            TxtDtVencimento.Value = MvCheque.DtVencimento;
            TxtValor.Value = MvCheque.Valor;
            TxtObservacao.Text = MvCheque.Observacao;            
            TxtDtDestino.Visible = false;
            TxtDocumVenda.Text = MvCheque.DocumVenda;
            Chk_ChDev.Checked = MvCheque.Status == 1;
            LstFilial.SelectedValue = MvCheque.IdFilial.ToString();
            TxtCnpjCpf.Text = MvCheque.CnpjCpf;
            if (Chk_ChDev.Checked)
                MvCheque.Status = 1;
            else
                MvCheque.Status = 0;
            if (MvCheque.IdPessoaDest > 0)
            {
                TxtDtDestino.Visible = true;
                TxtDtDestino.Value = MvCheque.DtDestino;                
            }
            label17.Visible = TxtDtDestino.Visible;
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
            PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
            StaFormEdicao = true;
            FrmPrincipal.ControleBotoes(true);            
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtNumCheque.Text.Trim() != "")
            {
                int DestAnt = MvCheque.IdPessoaDest;
                MvCheque.DataLanc = TxtData.Value;
                MvCheque.IdPessoa = int.Parse(TxtCodResp.Text);
                MvCheque.IdPessoaDest = int.Parse(TxtCodDest.Text);                
                MvCheque.Titular = TxtTitular.Text;
                MvCheque.NumBanco = int.Parse(TxtNumBanco.Value.ToString());
                MvCheque.NumAgencia = TxtNumAgencia.Text;
                MvCheque.NumConta = TxtNumConta.Text;
                MvCheque.NumCheque = TxtNumCheque.Text;
                MvCheque.DtVencimento = TxtDtVencimento.Value;
                MvCheque.Valor = TxtValor.Value;
                MvCheque.Observacao = TxtObservacao.Text;
                MvCheque.DocumVenda = TxtDocumVenda.Text;
                MvCheque.IdFilial = int.Parse(LstFilial.SelectedValue.ToString());
                MvCheque.CnpjCpf = TxtCnpjCpf.Text;
                if (Chk_ChDev.Checked)
                    MvCheque.Status = 1;
                else
                    MvCheque.Status = 0;                
                MvCheque.GravarDados();

                if (MvCheque.IdPessoaDest == 0)
                   Controle.ExecutaSQL("UPDATE MOVCHEQUEPRE SET DTDESTINO=NULL WHERE ID_LANC=" + MvCheque.IdLanc.ToString());
                else
                     Controle.ExecutaSQL("UPDATE MOVCHEQUEPRE SET DTDESTINO=CONVERT(DATETIME,'" + TxtDtDestino.Value.Date.ToShortDateString() + "',103) WHERE ID_LANC=" + MvCheque.IdLanc.ToString());                

                PopularGrid();
                PopularCampos(MvCheque.IdLanc);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Numero do Cheque não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtNumCheque.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
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
                    MvCheque.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    MvCheque.Excluir();
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
        }
        private void LimpaDados()
        {
            TxtCodigo.Text       = "0";
            TxtCodigo.Text = "0";
            TxtData.Value = DateTime.Now;
            SetaPessoa(0, 1);
            SetaPessoa(0, 2);
            TxtTitular.Text = "";
            TxtNumBanco.Value = 0;
            TxtNumAgencia.Text = "";
            TxtNumConta.Text = "";
            TxtNumCheque.Text = "";
            TxtDtVencimento.Value = DateTime.Now;
            TxtValor.Value = 0;
            TxtObservacao.Text = "";            
            TxtDtDestino.Visible = false;
            TxtDocumVenda.Text = "";
            Chk_ChDev.Checked = false;
            LstFilial.SelectedValue = "0";
            TxtCnpjCpf.Text = "";
            MvCheque.LerDados(0);
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
        private void BtnPessoaResp_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
                BuscaPessoa.ShowDialog();
                if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                {
                    MvCheque.IdPessoa = BuscaPessoa.CadPessoa.IdPessoa;
                    TxtCodResp.Text = MvCheque.IdPessoa.ToString();
                    TxtResp.Text = BuscaPessoa.CadPessoa.RazaoSocial;
                }
                else
                {
                    TxtCodResp.Text = "0";
                    TxtResp.Text = "";
                }
                BuscaPessoa.Dispose();
                TxtNumBanco.Focus();

            }
        }
        private void BtnDestinatario_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
                BuscaPessoa.ShowDialog();
                if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                {
                    MvCheque.IdPessoa = BuscaPessoa.CadPessoa.IdPessoa;
                    TxtCodDest.Text = MvCheque.IdPessoa.ToString();
                    TxtDest.Text = BuscaPessoa.CadPessoa.RazaoSocial;
                    TxtDtDestino.Value = DateTime.Now;
                }
                else
                {
                    TxtCodDest.Text = "0";
                    TxtDest.Text = "";
                }
                BuscaPessoa.Dispose();

            }
        }
        private void SetaPessoa(int IdPessoa,int Op)
        {
            CadPessoa.LerDados(IdPessoa);
            if (Op == 1)
            {
                if (CadPessoa.IdPessoa > 0)
                {
                    TxtCodResp.Text = CadPessoa.IdPessoa.ToString();
                    TxtResp.Text = CadPessoa.RazaoSocial;
                }
                else
                {
                    TxtCodResp.Text = "0";
                    TxtResp.Text = "";
                }
            }
            if (Op == 2)
            {
                if (CadPessoa.IdPessoa > 0)
                {
                    TxtCodDest.Text = CadPessoa.IdPessoa.ToString();
                    TxtDest.Text = CadPessoa.RazaoSocial;
                }
                else
                {
                    TxtCodDest.Text = "0";
                    TxtDest.Text = "";
                }
            }            
        }        
        private void BtnPesqResp_Click(object sender, EventArgs e)
        {
            FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            BuscaPessoa.ShowDialog();
            if (BuscaPessoa.CadPessoa.IdPessoa > 0)
            {                
                TxtPesqIdResp.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                TxtPesqResp.Text = BuscaPessoa.CadPessoa.RazaoSocial;
            }
            else
            {
                TxtPesqIdResp.Text = "0";
                TxtPesqResp.Text = "";
            }
            BuscaPessoa.Dispose();
        }
        private void BtnPesqDest_Click(object sender, EventArgs e)
        {
            FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            BuscaPessoa.ShowDialog();
            if (BuscaPessoa.CadPessoa.IdPessoa > 0)
            {
                TxtPesqIdDest.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                TxtPesqDest.Text = BuscaPessoa.CadPessoa.RazaoSocial;
            }
            else
            {
                TxtPesqIdDest.Text = "0";
                TxtPesqDest.Text = "";
            }
            BuscaPessoa.Dispose();
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelMovChequePre RelCheque = new Relatorios.RelMovChequePre();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(sSQLFiltro+" ORDER BY T1.DTVENCIMENTO");
            RelCheque.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelCheque;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelCheque.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelCheque.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;
        }        
    }
}
