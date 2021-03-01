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
using System.Data.Sql;
using System.Data.SqlClient;

namespace ERP_SGE
{
    public partial class FrmPrevCustos : Form
    {
        Funcoes Controle = new Funcoes();
        Funcionarios CadFunc = new Funcionarios();
        PrevCustos PrevCta = new PrevCustos();

        public TelaPrincipal FrmPrincipal;

        private DataSet TabLanc;
        private BindingSource Source_Lanc;

        public FrmPrevCustos()
        {
            InitializeComponent();
        }

        private void FrmPrevCustos_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;            
            PrevCta.Controle = Controle;
            TabLanc = new DataSet();
            Source_Lanc = new BindingSource();
            PrevCta.IdLanc = 0;
            TxtAno.Value = DateTime.Now.Year;
            ColLstConta  = FrmPrincipal.PopularComboGrid("SELECT T2.ID_CUSTO,'('+RTRIM(T1.GRUPO)+') <=> '+RTRIM(T2.CUSTO) FROM GRUPOCCUSTO T1 LEFT JOIN CENTROCUSTO T2 ON (T2.ID_GRPCUSTO=T1.ID_GRPCUSTO) WHERE T1.TIPO=1 ORDER BY T1.TIPO,T1.GRUPO,T2.CUSTO", ColLstConta);
            LstFilial    = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilial, "Todas");
            LstDepart    = FrmPrincipal.PopularCombo("SELECT ID_Departamento,Departamento FROM Departamentos ORDER BY Departamento", LstDepart);
            LstCusto     = FrmPrincipal.PopularCombo("SELECT T2.ID_CUSTO,'('+RTRIM(T1.GRUPO)+') <=> '+RTRIM(T2.CUSTO) FROM GRUPOCCUSTO T1 LEFT JOIN CENTROCUSTO T2 ON (T2.ID_GRPCUSTO=T1.ID_GRPCUSTO) WHERE T1.TIPO=1 ORDER BY T1.TIPO,T1.GRUPO,T2.CUSTO", LstCusto);
        }

        private void Frm_Activated(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
            FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.BtnFechar.Enabled = true;
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Eventos Proventos e Descontos
        private void PopularGrid()
        {
            string sSQL = "SELECT ID_LANC,ID_CUSTO,JANEIRO,FEVEREIRO,MARCO,ABRIL,MAIO,JUNHO,JULHO,AGOSTO,SETEMBRO,OUTUBRO,NOVEMBRO,DEZEMBRO FROM PREVCUSTOS WHERE ANO=" + TxtAno.Value.ToString();

            if (int.Parse(LstFilial.SelectedValue.ToString()) > 0)
                sSQL = sSQL + " AND ID_FILIAL=" + LstFilial.SelectedValue.ToString();

            if (int.Parse(LstDepart.SelectedValue.ToString()) > 0)
                sSQL = sSQL + " AND ID_Departamento=" + LstDepart.SelectedValue.ToString();

            if (int.Parse(LstCusto.SelectedValue.ToString()) > 0)
                sSQL = sSQL + " AND ID_Custo=" + LstCusto.SelectedValue.ToString();

            sSQL = sSQL + " ORDER BY 1";
            TabLanc = Controle.ConsultaTabela(sSQL);
            Source_Lanc.DataSource = TabLanc;
            Source_Lanc.DataMember = TabLanc.Tables[0].TableName;
            GridDados.DataSource = Source_Lanc;
            Navegador.BindingSource = Source_Lanc;
            int item = Source_Lanc.Find("Id_Lanc", PrevCta.IdLanc);
            Source_Lanc.Position = item;
            //Totalizar();

        }
        private void Totalizar()
        {
            /*decimal TotProv = 0;
            decimal TotDesc = 0;
            for (int I = 0; I <= GridEventos.RowCount - 1; I++)
            {
                if (GridEventos.Rows[I].Cells[1].Value.ToString() == "0")
                    TotProv = TotProv + decimal.Parse(GridEventos.Rows[I].Cells[4].Value.ToString());
                else
                    TotDesc = TotDesc + decimal.Parse(GridEventos.Rows[I].Cells[4].Value.ToString());
            }
            LblTotalProv.Text = string.Format("{0:N2}", TotProv);
            LblTotalDesc.Text = string.Format("{0:N2}", TotDesc);
            LblSaldo.Text = string.Format("{0:N2}", TotProv - TotDesc);*/
        }

        private void GridDados_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void GridDados_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            /*if (int.Parse(TxtCodFunc.Text) == 0)
            {
                Source_Eventos.CancelEdit();
                e.Cancel = true;
            }*/

        }
        private void GridDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            PrevCta.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
            PrevCta.IdCusto   = int.Parse(GridDados.CurrentRow.Cells[1].Value.ToString());
            PrevCta.Janeiro   = decimal.Parse(GridDados.CurrentRow.Cells[2].Value.ToString());
            PrevCta.Fevereiro = decimal.Parse(GridDados.CurrentRow.Cells[3].Value.ToString());
            PrevCta.Marco     = decimal.Parse(GridDados.CurrentRow.Cells[4].Value.ToString());
            PrevCta.Abril     = decimal.Parse(GridDados.CurrentRow.Cells[5].Value.ToString());
            PrevCta.Maio      = decimal.Parse(GridDados.CurrentRow.Cells[6].Value.ToString());
            PrevCta.Junho     = decimal.Parse(GridDados.CurrentRow.Cells[7].Value.ToString());
            PrevCta.Julho     = decimal.Parse(GridDados.CurrentRow.Cells[8].Value.ToString());
            PrevCta.Agosto    = decimal.Parse(GridDados.CurrentRow.Cells[9].Value.ToString());
            PrevCta.Setembro  = decimal.Parse(GridDados.CurrentRow.Cells[10].Value.ToString());
            PrevCta.Outubro   = decimal.Parse(GridDados.CurrentRow.Cells[11].Value.ToString());
            PrevCta.Novembro  = decimal.Parse(GridDados.CurrentRow.Cells[12].Value.ToString());
            PrevCta.Dezembro  = decimal.Parse(GridDados.CurrentRow.Cells[13].Value.ToString());
            PrevCta.GravarDados();
            PopularGrid();
            GridDados.CurrentCell = GridDados.CurrentRow.Cells[e.ColumnIndex];

        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstFilial.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor Informar a Filial", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.Parse(LstDepart.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor Informar o Departamento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
            IncluirItem();
            
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PrevCta.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                PrevCta.Excluir();
                PrevCta.IdLanc = 0;
                PopularGrid();
            }
        }
        private void IncluirItem()
        {
            PrevCta.LerDados(0);
            PrevCta.IdFilial = int.Parse(LstFilial.SelectedValue.ToString());
            PrevCta.IdDepart = int.Parse(LstDepart.SelectedValue.ToString());
            PrevCta.IdCusto  = int.Parse(LstCusto.SelectedValue.ToString());
            PrevCta.Ano      = int.Parse(TxtAno.Value.ToString());
            PrevCta.GravarDados();
            PopularGrid();
            GridDados.CurrentCell = GridDados.CurrentRow.Cells[2];

        }

        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstFilial.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Favor Informar a Filial", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sSQL = "SELECT T5.Fantasia AS Filial,T4.Departamento,T4.Responsavel,T3.Grupo,T2.Custo,T1.* FROM PREVCUSTOS T1 " +
                        " LEFT JOIN CentroCusto T2 ON (T1.ID_CUSTO=T2.Id_Custo)" +
                        " LEFT JOIN GrupoCCusto T3 ON (T3.Id_GrpCusto=T2.Id_GrpCusto)" +
                        " LEFT JOIN Departamentos T4 ON (T4.Id_Departamento=T1.Id_Departamento)" +
                        " LEFT JOIN Empresa_Filial T5 ON (T5.Id_Filial=T1.ID_FILIAL)" +
                        " WHERE T1.ANO=" + TxtAno.Value.ToString();

            if (int.Parse(LstDepart.SelectedValue.ToString()) > 0)
                sSQL = sSQL + " AND T1.ID_Departamento=" + LstDepart.SelectedValue.ToString();

            if (int.Parse(LstCusto.SelectedValue.ToString()) > 0)
                sSQL = sSQL + " AND T1.ID_Custo=" + LstCusto.SelectedValue.ToString();

            sSQL = sSQL + " ORDER BY T4.Departamento,T3.Grupo,T2.Custo";

            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelPrevCustos Rel001 = new Relatorios.RelPrevCustos();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(sSQL);
            Rel001.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = Rel001;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Ano:" + TxtAno.Value.ToString();
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;
        }        
    }
}
