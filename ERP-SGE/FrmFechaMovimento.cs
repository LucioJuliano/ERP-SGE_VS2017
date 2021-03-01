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

namespace ERP_SGE
{
    public partial class FrmFechaMovimento : Form
    {
        Funcoes Controle = new Funcoes();        
        public FormaPagamento FrmPgto = new FormaPagamento();
        private MvVenda Venda = new MvVenda();
        private Pessoas CadPessoa = new Pessoas();
        private BindingSource Source_Lanc;
        public TelaPrincipal FrmPrincipal;
        public int IdPessoa;
        public int NumMov;
        public int NumVd;
        public string NumDoc;
        public int PagRec;
        public string Referente;
        public int IdPgto;
        public int IdFilial;
        public bool Concluido;
        public string NotaFiscal = "";
        public string Obs        = "";
        public bool FechaPDV     = false;        

        public FrmFechaMovimento()
        {
            InitializeComponent();            
        }

        private void FrmFechaMovimento_Load(object sender, EventArgs e)
        {
            LstFormaPgto       = FrmPrincipal.PopularCombo("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento where ativo=1 ORDER BY FormaPgto", LstFormaPgto);
            LstAgente          = FrmPrincipal.PopularCombo("SELECT Id_Agente,Agente FROM AgenteCobrador ORDER BY Agente", LstAgente);
            LstTpDoc           = FrmPrincipal.PopularComboGrid("SELECT ID_DOCUMENTO,DOCUMENTO FROM TIPODOCUMENTO where ativo=1 ORDER BY DOCUMENTO", LstTpDoc);
            Controle.Conexao   = FrmPrincipal.Conexao;
            FrmPgto.Controle   = Controle;
            Venda.Controle     = Controle;
            CadPessoa.Controle = Controle;
            TxtDtBase.Value    = DateTime.Now;
            Source_Lanc        = new BindingSource();
            Concluido          = false;

            LstFormaPgto.SelectedValue = IdPgto;

            if (NumVd > 0)
                LstFormaPgto.Enabled = FrmPrincipal.Perfil_Usuario.AlterarVenda == 1;

            if (IdPgto > 0)
                CalcularParcelas(IdPgto,0);

        }
        private void LstFormaPgto_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (LstFormaPgto.Focused)
            {
                if (int.Parse(LstFormaPgto.SelectedValue.ToString()) > 0)
                {
                    CalcularParcelas(int.Parse(LstFormaPgto.SelectedValue.ToString()),0);                   
                }
            }
        }

        private decimal VerificaRentab(int IdVd)
        {
            decimal VlrComissao = 0;
            decimal TotalVd     = 0;
            decimal Rentab      = 0;

            SqlDataReader Tab = Controle.ConsultaSQL("SELECT T2.IgnoraDesc,T2.VLRTOTAL AS VLRVENDA,T2.VLRSUBTOTAL AS VLRSUBVENDA,T1.* FROM MVVENDAITENS T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) WHERE T1.ID_VENDA=" + NumVd.ToString());
            while (Tab.Read())
            {
                TotalVd = decimal.Parse(Tab["VlrVenda"].ToString());

                if (int.Parse(Tab["IgnoraDesc"].ToString()) == 1)
                    TotalVd = decimal.Parse(Tab["VlrSubVenda"].ToString());

                VlrComissao = VlrComissao + decimal.Parse(Tab["VlrComissao"].ToString());                
            }

            if (TotalVd > 0)
                Rentab = (VlrComissao / TotalVd) * 100;

            return Rentab;

        }
        private void CalcularParcelas(int IdFormaPgto,int NumParc)
        {
            TxtNumParc.Enabled = true;
            FrmPgto.LerDados(IdFormaPgto);
            Venda.LerDados(NumVd);
            CadPessoa.LerDados(Venda.IdPessoa);

            if (FrmPgto.IdFormaPgto > 0)
            {

                if (FrmPgto.BloqPF == 1 && CadPessoa.Tipo == 1 && Venda.IdUsuboleto == 0 && NumVd > 0)
                {
                    MessageBox.Show("Atenção: Forma de pagamento não autorizada para Pessoa Fisica.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LstFormaPgto.SelectedValue = Venda.IdFormaPgto.ToString();
                    return;
                }
                DataTable TabLanc = CriarTabela();
                                
                int NParc = 1;
                int NumParcelas = 1;
                NumParcelas = FrmPgto.NumParcelas;
                
                if (NumParc > 0)
                    NumParcelas = NumParc;

                TxtNumParc.Value = NumParcelas;

                DateTime DtVenc    = TxtDtBase.Value.AddDays(FrmPgto.PrimParcela);
                decimal VrParc     = Math.Round(TxtVlrTotal.Value / NumParcelas, 2);
                decimal VrPrimParc = VrParc + (TxtVlrTotal.Value - (Math.Round(VrParc * NumParcelas, 2)));
                int Dias = FrmPgto.PrimParcela;

                decimal Rentab = 0;

                if (NumVd > 0)
                    Rentab = VerificaRentab(NumVd);

                while (NParc <= NumParcelas)
                {
                    TabLanc.Rows.Add(NParc, Dias, DtVenc.Date, VrPrimParc, FrmPgto.IdTpDocumento);                    
                    if (FrmPgto.Intervalo == 0)
                    {
                        if (CadPessoa.NaoVerPrazoPg == 0 && TxtVlrTotal.Value <= FrmPgto.VlrParcelamento && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0 && !FrmPrincipal.VersaoDistribuidor)
                        {
                            DtVenc = DtVenc;
                            Dias = Dias;
                        }
                        else if (CadPessoa.NaoVerPrazoPg == 0 && NumVd > 0 && Rentab <= decimal.Parse("2,40") && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0 && !FrmPrincipal.VersaoDistribuidor)
                        {
                            DtVenc = DtVenc;
                            Dias = Dias;
                        }
                        else
                        {
                            DtVenc = DtVenc.AddDays(30);
                            Dias = Dias + 30;
                        }
                    }
                    else
                    {
                        if (CadPessoa.NaoVerPrazoPg == 0 && TxtVlrTotal.Value <= FrmPgto.VlrParcelamento && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0 && !FrmPrincipal.VersaoDistribuidor)
                        {
                            DtVenc = DtVenc;
                            Dias = Dias;
                        }
                        else if (CadPessoa.NaoVerPrazoPg == 0 && NumVd > 0 && Rentab <= decimal.Parse("2,40") && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0 && !FrmPrincipal.VersaoDistribuidor)
                        {
                            DtVenc = DtVenc;
                            Dias = Dias;
                        }
                        else
                        {
                            DtVenc = DtVenc.AddDays(FrmPgto.Intervalo);
                            Dias = Dias + FrmPgto.Intervalo;
                        }
                    }
                    VrPrimParc = VrParc;
                    NParc++;
                }
                Source_Lanc.DataSource = TabLanc;
                Source_Lanc.DataMember = TabLanc.TableName;
                GridDados.DataSource   = Source_Lanc;
                GridDados.Refresh();
                GridDados.Focus();               
            }
        }
        private DataTable CriarTabela()
        {
            DataTable Tabela = new DataTable();
            Tabela.Columns.Add("Parcela",    Type.GetType("System.Int32"));
            Tabela.Columns.Add("Dias",       Type.GetType("System.Int32"));
            Tabela.Columns.Add("Vencimento", Type.GetType("System.DateTime"));
            Tabela.Columns.Add("Valor",      Type.GetType("System.Decimal"));
            Tabela.Columns.Add("IdTpDoc",    Type.GetType("System.Int32"));           
            return Tabela;
        }
        private void GridDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.ColumnIndex == 1)
            {
                if (NumVd > 0)
                {
                    decimal Rentab = VerificaRentab(NumVd);

                    if (CadPessoa.NaoVerPrazoPg == 0 && int.Parse(GridDados.CurrentCell.Value.ToString()) > 60 && Rentab <= decimal.Parse("2,40") && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0)
                    {
                        MessageBox.Show("Numero de dias não pode ser superior a 60, Motivo Rentabilidade inferior a 2,4% ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Source_Lanc.CancelEdit();
                        CalcularParcelas(IdPgto, 0);
                        return;
                    }
                    if (CadPessoa.NaoVerPrazoPg==0 && int.Parse(GridDados.CurrentCell.Value.ToString()) > 35 && TxtVlrTotal.Value <= FrmPgto.VlrParcelamento && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0)
                    {
                        MessageBox.Show("Numero de dias não pode ser superior a 30 ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Source_Lanc.CancelEdit();
                        CalcularParcelas(IdPgto, 0);
                        return;
                    }
                }
                GridDados.CurrentRow.Cells[2].Value= TxtDtBase.Value.Date.AddDays(int.Parse(GridDados.CurrentCell.Value.ToString()));             
            }
            else if (e.ColumnIndex == 2)
            {

                DateTime Dt = DateTime.Parse(GridDados.CurrentCell.Value.ToString());
                if (NumVd > 0)
                {
                    decimal Rentab = VerificaRentab(NumVd);
                    if (CadPessoa.NaoVerPrazoPg == 0 && (Dt.Date - DateTime.Now.Date).Days > 60 && Rentab <= decimal.Parse("2,40") && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0 && !FrmPrincipal.VersaoDistribuidor)
                    {
                        MessageBox.Show("Numero de dias não pode ser superior a 60, Motivo Rentabilidade inferior a 2,4% ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Source_Lanc.CancelEdit();
                        Source_Lanc.ResetCurrentItem();
                        CalcularParcelas(IdPgto, 0);
                        return;
                    }

                    if (CadPessoa.NaoVerPrazoPg == 0 && (Dt.Date - DateTime.Now.Date).Days > 35 && TxtVlrTotal.Value <= FrmPgto.VlrParcelamento && Venda.TpVenda == "PV" && Venda.IdUsuLibParc == 0 && !FrmPrincipal.VersaoDistribuidor)
                    {
                        MessageBox.Show("Vencimento não pode ser superior a 30 dias ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Source_Lanc.CancelEdit();
                        CalcularParcelas(IdPgto, 0);
                        return;
                    }
                }
                
                if (Dt.Date < TxtDtBase.Value.Date)
                {
                    MessageBox.Show("Vencimento não pode ser inferior a data base", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Lanc.CancelEdit();
                    GridDados.Refresh();
                }                
            }
            
        }
        private void GridDadosDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
               MessageBox.Show("Verifique a Data do Vencimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
               Source_Lanc.CancelEdit();                 
            }
        }
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            decimal Total = 0;
            bool TpDoc = false;
            for (int I = 0; I <= GridDados.RowCount - 1; I++)
            {
                Total = Total + decimal.Parse(GridDados.Rows[I].Cells[3].Value.ToString());
                if (int.Parse(GridDados.Rows[I].Cells[4].Value.ToString()) == 0)
                    TpDoc = true;
            }

            if (TpDoc)
            {
                MessageBox.Show("Favor informar o Tipo de Documento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridDados.Focus();
                return;
            }

            if (Math.Round(Total, 2) != Math.Round(TxtVlrTotal.Value, 2))
            {
                MessageBox.Show("Soma das Parcela(s) diferente do total do movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridDados.Focus();
            }
            else
            {
                MvVenda Venda = new MvVenda();
                Venda.Controle = Controle;
                Venda.LerDados(NumVd);

                if (Venda.Status == 2 && Venda.IdEntregador !=0 && !FechaPDV)
                {
                    MessageBox.Show("Movimento já Faturado e em Rota", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Venda.Status == 3)
                {
                    MessageBox.Show("Movimento já Entregue", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }                
                if (MessageBox.Show("Confirma o Fechamento do Movimento", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        BtnConcluir.Enabled = false;
                        FrmPrincipal.BSta_BarProcesso.Value = 0;
                        FrmPrincipal.BSta_BarProcesso.Maximum = Source_Lanc.Count;
                        // 
                        if (NumMov > 0)
                            Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_MOV=" + NumMov.ToString());
                        else if (NumVd > 0)
                            Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_VENDA=" + NumVd.ToString());                            
                        //
                        Financeiro MvFinanc = new Financeiro();
                        Pessoas Pessoa      = new Pessoas();
                        Pessoa.Controle     = Controle;
                        MvFinanc.Controle   = Controle;                        
                        Pessoa.LerDados(IdPessoa);
                        // Processando os Dados
                        for (int i = 0; i <= Source_Lanc.Count - 1; i++)
                        {
                            if (decimal.Parse(GridDados.Rows[i].Cells[3].Value.ToString()) > 0)
                            {
                                MvFinanc.LerDados(0);
                                MvFinanc.PagRec   = PagRec;
                                MvFinanc.IdMov    = NumMov;
                                MvFinanc.IdVenda  = NumVd;
                                MvFinanc.IdPessoa = IdPessoa;
                                if (IdFilial > 0)
                                    MvFinanc.IdFilial = IdFilial;
                                else
                                    MvFinanc.IdFilial = FrmPrincipal.Perfil_Usuario.IdFilial;
                                MvFinanc.Vencimento      = DateTime.Parse(GridDados.Rows[i].Cells[2].Value.ToString());
                                MvFinanc.VlrOriginal     = decimal.Parse(GridDados.Rows[i].Cells[3].Value.ToString());
                                MvFinanc.IdTipoDocumento = int.Parse(GridDados.Rows[i].Cells[4].Value.ToString());
                                MvFinanc.IdCusto         = Pessoa.IdCusto;
                                MvFinanc.IdDepartamento  = Pessoa.IdDepartamento;
                                MvFinanc.IdVendedor      = Venda.IdVendedor;
                                MvFinanc.IdFormaPgto     = int.Parse(LstFormaPgto.SelectedValue.ToString());
                                MvFinanc.IdAgente        = int.Parse(LstAgente.SelectedValue.ToString());
                                MvFinanc.NumDoc          = NumDoc.Trim() + "/" + string.Format("{0:D2}", i + 1);
                                MvFinanc.Referente       = Referente;
                                MvFinanc.NotaFiscal      = NotaFiscal;
                                MvFinanc.IdUsuLanc       = FrmPrincipal.Perfil_Usuario.IdUsuario;
                                MvFinanc.Observacao      = Obs;
                                MvFinanc.GravarDados();

                                if (FrmPgto.Baixa == 1 && FrmPrincipal.Perfil_Usuario.UsuCaixaLj == 1 )
                                {
                                    MvFinanc.DtBaixa  = MvFinanc.Vencimento;
                                    MvFinanc.VlrBaixa = MvFinanc.VlrOriginal;
                                    MvFinanc.Baixar();
                                }
                            }
                            FrmPrincipal.BSta_BarProcesso.Value = FrmPrincipal.BSta_BarProcesso.Value + 1;
                        }
                        FrmPrincipal.BSta_BarProcesso.Value = 0;
                        Concluido = true;
                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("Ocorreu um erro ao tentar confirmar tente novamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (NumMov > 0)
                            Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_MOV=" + NumMov.ToString());                            
                        else if (NumVd > 0)
                            Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_VENDA=" + NumVd.ToString());                            
                        BtnConcluir.Enabled = true;
                        FrmPrincipal.BSta_BarProcesso.Value = 0;
                    }
                }
            }
        }
        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            Concluido = false;
            Close();
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void FrmFechaMovimento_Shown(object sender, EventArgs e)
        {
            SqlDataReader Tabela;
            if (NumVd > 0)
                Tabela = Controle.ConsultaSQL("SELECT * FROM LancFinanceiro WHERE Id_Venda > 0 and Id_Venda=" + NumVd.ToString());
            else
                Tabela = Controle.ConsultaSQL("SELECT * FROM LancFinanceiro WHERE Id_Mov > 0 and Id_Mov=" + NumMov.ToString());           

            int NParc = 0;
            DataTable TabLanc = CriarTabela();
            while (Tabela.Read())
            {
                NParc++;
                DateTime DtVenc = DateTime.Parse(Tabela["Vencimento"].ToString());
                decimal VrParc  = decimal.Parse(Tabela["VlrOriginal"].ToString());                
                TabLanc.Rows.Add(NParc,0,DtVenc.Date, VrParc, int.Parse(Tabela["Id_TipoDocumento"].ToString()));                                 
            }
            if (NParc > 0)
            {
                Source_Lanc.DataSource = TabLanc;
                Source_Lanc.DataMember = TabLanc.TableName;
                GridDados.DataSource = Source_Lanc;
                GridDados.Refresh();
                GridDados.Focus();                
            }
        }
        private void TxtNumParc_Validated(object sender, EventArgs e)
        {
            CalcularParcelas(FrmPgto.IdFormaPgto,int.Parse(TxtNumParc.Value.ToString()));
        }

        private void GridDados_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            /*if (e.ColumnIndex == 4 && NumVd > 0)
            {
                if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0)
                {
                    Source_Lanc.CancelEdit();
                    e.Cancel = true;
                    return;
                }

            }*/
        }              
    }
}
