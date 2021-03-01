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
    public partial class FrmReplicarFinanc : Form
    {

        Funcoes Controle = new Funcoes();        
        private BindingSource Source_Lanc;
        public  TelaPrincipal FrmPrincipal;
        public  bool Concluido;
        public  Financeiro LancFinanc;
        
        public FrmReplicarFinanc()
        {
            InitializeComponent();
        }

        private void FrmReplicarFinanc_Load(object sender, EventArgs e)
        {
            Controle.Conexao    = FrmPrincipal.Conexao;
            LancFinanc.Controle = Controle;            
            Source_Lanc         = new BindingSource();
        }        

        private void TxtNumParc_Validated(object sender, EventArgs e)
        {
            CalcularParcelas(int.Parse(TxtNumParc.Value.ToString()));
        }

        private DataTable CriarTabela()
        {
            DataTable Tabela = new DataTable();
            Tabela.Columns.Add("Parcela",      Type.GetType("System.Int32"));            
            Tabela.Columns.Add("Vencimento",   Type.GetType("System.DateTime"));
            Tabela.Columns.Add("Valor",        Type.GetType("System.Decimal"));
            Tabela.Columns.Add("NumDocumento", Type.GetType("System.String"));            
            return Tabela;
        }

        private void CalcularParcelas(int NumParc)
        {
            DataTable TabLanc = CriarTabela();
            int NParc = 1;            
            DateTime DtVenc    = TxtDtBase.Value;
            decimal VrParc     = LancFinanc.VlrOriginal;
            int dia = TxtDtBase.Value.Day;
            int mes = TxtDtBase.Value.Month;
            int ano = TxtDtBase.Value.Year;
            DtVenc = new DateTime(ano, mes,dia);
            

            while (NParc <= NumParc)
            {
                TabLanc.Rows.Add(NParc, DtVenc.Date, VrParc, LancFinanc.NumDoc.Trim());
                DtVenc = DtVenc.AddMonths(1);
                dia = TxtDtBase.Value.Day;
                mes = DtVenc.Date.Month;
                ano = DtVenc.Date.Year;
                try
                {
                    if (mes == 2 && dia > 28)
                        dia = 28;
                    DtVenc = new DateTime(ano, mes, dia);
                }
                catch
                { }

                NParc++;
            }
            Source_Lanc.DataSource = TabLanc;
            Source_Lanc.DataMember = TabLanc.TableName;
            GridDados.DataSource   = Source_Lanc;
            GridDados.Refresh();
            GridDados.Focus();
        }

        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void GridDados_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DateTime Dt = DateTime.Parse(GridDados.CurrentCell.Value.ToString());
                if (Dt.Date < TxtDtBase.Value.Date)
                {
                    MessageBox.Show("Vencimento não pode ser inferior a data base", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Lanc.CancelEdit();
                }
            }                    
        }

        private void GridDados_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                MessageBox.Show("Verifique a Data do Vencimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Lanc.CancelEdit();
            }
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma o Movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    BtnConcluir.Enabled = false;
                    FrmPrincipal.BSta_BarProcesso.Value = 0;
                    FrmPrincipal.BSta_BarProcesso.Maximum = Source_Lanc.Count;
                    
                    // Processando os Dados
                    for (int i = 0; i <= Source_Lanc.Count - 1; i++)
                    {
                        if (decimal.Parse(GridDados.Rows[i].Cells[2].Value.ToString()) > 0)
                        {
                            LancFinanc.IdLanc      = 0;
                            LancFinanc.Vencimento  = DateTime.Parse(GridDados.Rows[i].Cells[1].Value.ToString());
                            LancFinanc.VlrOriginal = decimal.Parse(GridDados.Rows[i].Cells[2].Value.ToString());
                            LancFinanc.NumDoc      = GridDados.Rows[i].Cells[3].Value.ToString().Trim();
                            LancFinanc.GravarDados();
                        }
                        FrmPrincipal.BSta_BarProcesso.Value = FrmPrincipal.BSta_BarProcesso.Value + 1;
                    }
                    FrmPrincipal.BSta_BarProcesso.Value = 0;
                    Concluido = true;
                    MessageBox.Show("Concluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ocorreu um erro ao tentar confirmar tente novamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnConcluir.Enabled = true;
                    FrmPrincipal.BSta_BarProcesso.Value = 0;
                }
            }
        }

        private void FrmReplicarFinanc_Shown(object sender, EventArgs e)
        {
            TxtDtBase.Value = LancFinanc.Vencimento;
            TxtDtBase.Value = TxtDtBase.Value.AddDays(30);
            int dia = LancFinanc.Vencimento.Day;
            int mes = TxtDtBase.Value.Month;
            int ano = TxtDtBase.Value.Year;            
            TxtDtBase.Value = new DateTime(ano, mes, dia);
            CalcularParcelas(1);
        }

        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
