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
using System.Collections;
using System.Data.SqlClient;


namespace ERP_SGE
{
    public partial class FrmFecharCxBalcao : Form
    {
        Funcoes Controle            = new Funcoes();
        CaixaBalcao CxBalcao        = new CaixaBalcao();
        FechamentoCxBalcao FecharCx = new FechamentoCxBalcao();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private DataTable Tabela;        
        public int IdCaixa = 0;
        public bool CxFechado = false;

        public FrmFecharCxBalcao()
        {
            InitializeComponent();
        }

        private void FrmFecharCxBalcao_Load(object sender, EventArgs e)
        {
            LblCaixa.Text = "Caixa: " + IdCaixa.ToString() + " - "+FrmPrincipal.Perfil_Usuario.Usuario.Trim();
            Controle.Conexao  = FrmPrincipal.Conexao;
            CxBalcao.Controle = Controle;
            FecharCx.Controle = Controle;
            CxBalcao.LerCaixa(IdCaixa);
            TabelaCx();
            PopularGrid();
        }

        private void PopularGrid()
        {
            string sSQL = "SELECT T1.ID_CAIXA,T3.ID_DOCUMENTO,T3.Documento,IsNull(T3.ResumoCx,0) as ResumoCx,SUM(t2.vlroriginal) AS TOTAL FROM MvVenda T1" +
                          " LEFT JOIN LancFinanceiro T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                          " LEFT JOIN TipoDocumento T3 ON (T3.Id_Documento=T2.Id_TipoDocumento)" +
                          " WHERE T1.Status=3 and T2.Id_Lanc > 0 AND T1.Id_Caixa=" + IdCaixa.ToString() +
                          "  group by T1.ID_CAIXA,T3.ID_DOCUMENTO,T3.Documento,T3.ResumoCx" +
                          " ORDER BY 3";

            DataSet ConsCx = new DataSet();
            ConsCx = Controle.ConsultaTabela(sSQL);
            for (int I = 0; I <= ConsCx.Tables[0].Rows.Count - 1; I++)
            {
                Tabela.Rows.Add(int.Parse(ConsCx.Tables[0].Rows[I]["ID_DOCUMENTO"].ToString()), ConsCx.Tables[0].Rows[I]["DOCUMENTO"].ToString(), decimal.Parse(ConsCx.Tables[0].Rows[I]["TOTAL"].ToString()),
                                0, 0, 0, 0, int.Parse(ConsCx.Tables[0].Rows[I]["RESUMOCX"].ToString()));
            }            
            //Calculando as Receitas e Despesas
            ConsCx = Controle.ConsultaTabela("SELECT T1.ID_DOCUMENTO,T2.DOCUMENTO,ISNULL(T2.RESUMOCX,0) AS RESUMOCX,T1.TIPO,SUM(T1.VALOR) AS TOTAL FROM MvCaixaBalcao T1" +
                                             " LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_DOCUMENTO) WHERE T1.STATUS=1 AND T1.Id_Caixa=" + IdCaixa.ToString() + " AND T1.Id_Documento > 0" +
                                             " GROUP BY T1.ID_DOCUMENTO,T2.DOCUMENTO,T2.RESUMOCX,T1.TIPO");
            bool Linha = false;
            for (int I = 0; I <= ConsCx.Tables[0].Rows.Count - 1; I++)
            {
                Linha = false;
                for (int L = 0; L <= Tabela.Rows.Count - 1; L++)
                {
                    if (Tabela.Rows[L]["ID_DOCUMENTO"].ToString().Trim() == ConsCx.Tables[0].Rows[I]["ID_DOCUMENTO"].ToString().Trim())
                    {
                        if (int.Parse(ConsCx.Tables[0].Rows[I]["TIPO"].ToString()) == 0)
                            Tabela.Rows[L]["VLRDESPESA"] = ConsCx.Tables[0].Rows[I]["TOTAL"].ToString().Trim();
                        else
                            Tabela.Rows[L]["VLRRECEITA"] = ConsCx.Tables[0].Rows[I]["TOTAL"].ToString().Trim();
                        Linha = true;
                        break;
                    }
                }

                if (!Linha)
                {
                    if (int.Parse(ConsCx.Tables[0].Rows[I]["TIPO"].ToString()) == 1)
                        Tabela.Rows.Add(int.Parse(ConsCx.Tables[0].Rows[I]["ID_DOCUMENTO"].ToString()), ConsCx.Tables[0].Rows[I]["DOCUMENTO"].ToString(), 0,
                                    decimal.Parse(ConsCx.Tables[0].Rows[I]["TOTAL"].ToString()), 0, 0, 0, int.Parse(ConsCx.Tables[0].Rows[I]["RESUMOCX"].ToString()));
                    else
                        Tabela.Rows.Add(int.Parse(ConsCx.Tables[0].Rows[I]["ID_DOCUMENTO"].ToString()), ConsCx.Tables[0].Rows[I]["DOCUMENTO"].ToString(), 0,
                                0, decimal.Parse(ConsCx.Tables[0].Rows[I]["TOTAL"].ToString()), 0, 0, int.Parse(ConsCx.Tables[0].Rows[I]["RESUMOCX"].ToString()));
                }
            }
            AtualizarGrid();

        }
        private void AtualizarGrid()
        {
            BindingSource Source = new BindingSource();
            GridCx.DataSource = Tabela;
            Source.DataSource = Tabela;
            GridCx.DataSource = Source;

            //Totalizando
            decimal VlrCalc = 0, SaldoDia = 0, SaldoFin = 0, SaldoInf = 0;
            
            for (int i = 0; i <= GridCx.Rows.Count - 1; i++)
            {
                VlrCalc = (decimal.Parse(GridCx.Rows[i].Cells[2].Value.ToString()) + decimal.Parse(GridCx.Rows[i].Cells[3].Value.ToString())) - decimal.Parse(GridCx.Rows[i].Cells[4].Value.ToString());
                GridCx.Rows[i].Cells[6].Value = decimal.Parse(GridCx.Rows[i].Cells[5].Value.ToString()) - VlrCalc;
                SaldoDia = SaldoDia + (decimal.Parse(GridCx.Rows[i].Cells[5].Value.ToString()) - VlrCalc);
                if (int.Parse(GridCx.Rows[i].Cells[7].Value.ToString()) == 1)
                {
                    SaldoInf = SaldoInf + decimal.Parse(GridCx.Rows[i].Cells[5].Value.ToString());
                    SaldoFin = SaldoFin + ((decimal.Parse(GridCx.Rows[i].Cells[2].Value.ToString()) + decimal.Parse(GridCx.Rows[i].Cells[3].Value.ToString())) - decimal.Parse(GridCx.Rows[i].Cells[4].Value.ToString()));
                }
            }
            LblSado.Text     = string.Format("{0:N2}", SaldoDia);
            LblSaldoFin.Text = string.Format("{0:N2}", SaldoFin);
            LblDifFin.Text   = string.Format("{0:N2}", SaldoInf - SaldoFin); ;

        }
        private void TabelaCx()
        {
            Tabela = new DataTable();
            Tabela.Columns.Add("Id_Documento", Type.GetType("System.Int32"));
            Tabela.Columns.Add("Documento",    Type.GetType("System.String"));
            Tabela.Columns.Add("VlrCalculado", Type.GetType("System.Decimal"));
            Tabela.Columns.Add("VlrReceita",   Type.GetType("System.Decimal"));
            Tabela.Columns.Add("VlrDespesa",   Type.GetType("System.Decimal"));
            Tabela.Columns.Add("VlrInformado", Type.GetType("System.Decimal"));
            Tabela.Columns.Add("VlrDif",       Type.GetType("System.Decimal"));
            Tabela.Columns.Add("ResumoCx",     Type.GetType("System.Int32"));
            Tabela.PrimaryKey = new DataColumn[] {Tabela.Columns["Id_Documento"] };
        }

        private void GridCx_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GridCx.CurrentRow != null)
            {
                AtualizarGrid();
                GridCx.CurrentCell = GridCx.CurrentRow.Cells[5];
            }
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma o Fechamento do caixa ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Controle.ExecutaSQL("Delete From FECHAMENTOCXBALCAO where Id_Caixa=" + IdCaixa.ToString());
                for (int i = 0; i <= GridCx.Rows.Count - 1; i++)
                {                    
                    FecharCx.LerDados(0);
                    FecharCx.IdCaixa      = IdCaixa;
                    FecharCx.IdDocumento  = int.Parse(GridCx.Rows[i].Cells[0].Value.ToString());
                    FecharCx.VlrCalculado = decimal.Parse(GridCx.Rows[i].Cells[2].Value.ToString());
                    FecharCx.VlrReceita   = decimal.Parse(GridCx.Rows[i].Cells[3].Value.ToString());
                    FecharCx.VlrDespesa   = decimal.Parse(GridCx.Rows[i].Cells[4].Value.ToString());
                    FecharCx.VlrInformado = decimal.Parse(GridCx.Rows[i].Cells[5].Value.ToString());
                    FecharCx.ResumoCx     = int.Parse(GridCx.Rows[i].Cells[7].Value.ToString());
                    FecharCx.GravarDados();
                    //                    
                }
                CxBalcao.Status = 1;
                CxBalcao.DtHrEnc = DateTime.Now;                
                CxBalcao.FecharCaixa();
                CxFechado = true;
                MessageBox.Show("Caixa foi fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();

            }
        }
    }
}
