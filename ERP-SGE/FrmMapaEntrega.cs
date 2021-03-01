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
    public partial class FrmMapaEntrega : Form
    {
        Funcoes Controle = new Funcoes();
        Auditoria RegAuditoria = new Auditoria();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private BindingSource Source_Venda;
        int IdVenda = 0;

        public FrmMapaEntrega()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao      = FrmPrincipal.Conexao;
            RegAuditoria.Controle = Controle;
            Chk_Periodo.Checked   = true;
            Dt1.Value             = DateTime.Now.AddDays(-1);
            Dt2.Value             = DateTime.Now;
            Source_Venda          = new BindingSource();
            CamposLista();
            PopularGridVenda();
        }
        private void CamposLista()
        {
            LstEntregador     = FrmPrincipal.PopularComboGrid("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstEntregador);
            LstPesqEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstPesqEntregador, "Todos");
            //LstPesqVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", LstPesqVendedor, "Todos");
            LstCaixa          = FrmPrincipal.PopularCombo("SELECT T1.ID_CAIXA,T2.USUARIO FROM CAIXABALCAO T1 LEFT JOIN USUARIOS T2 ON (T2.ID_USUARIO=T1.ID_USUARIO) WHERE T1.DATA = Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)", LstCaixa);
            LstPesqFilial     = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqFilial, "Todas");
        }
        private void PopularGridVenda()
        {
            string Filtro = "";
            Filtro = "WHERE ((T1.STATUS = 2) OR (T1.STATUS = 1 AND T1.TPVENDA IN ('BONIF','EMVF','CO','PR','AM','TROCA')))";
            if (TxtPesqNumDoc.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMDOCUMENTO LIKE '%" + TxtPesqNumDoc.Text.Trim() + "%'";
            if (TxtPesqNumVd.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_VENDA =" + TxtPesqNumVd.Text.Trim();
            if (TxtPesqPessoa.Text.Trim() != "")
                Filtro = Filtro + " AND T2.RAZAOSOCIAL Like '%" + TxtPesqPessoa.Text.Trim() + "%'";
            if (int.Parse(LstCaixa.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_CAIXA=" + LstCaixa.SelectedValue.ToString();
            if (int.Parse(LstPesqEntregador.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Entregador=" + LstPesqEntregador.SelectedValue.ToString();
            if (int.Parse(LstPesqFilial.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Filial=" + LstPesqFilial.SelectedValue.ToString();
            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,T3.DESCRICAO AS MOVIMENTO,T1.DATA,T1.NUMDOCUMENTO,T1.FORMNF,T2.RAZAOSOCIAL as Cliente,T1.VLRTOTAL,T1.ID_ENTREGADOR as CodEnt,T1.ID_ENTREGADOR,T1.PREVENTREGA,T4.VENDEDOR,T6.FANTASIA AS FILIAL,T7.FORMAPGTO FROM MVVENDA T1 " +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                             " LEFT JOIN TABELASAUX T3 ON (T3.CAMPO='VENDA' AND T3.CHAVE=T1.TPVENDA ) " +
                                             " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                                             " LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T1.ID_FILIAL) " +
                                             " LEFT JOIN FORMAPAGAMENTO T7 ON (T7.ID_FORMAPGTO=T1.ID_FORMAPGTO) " + Filtro + " ORDER BY T1.ID_VENDA DESC");
            Source_Venda.DataSource = Tabela;
            Source_Venda.DataMember = Tabela.Tables[0].TableName;
            GridVenda.DataSource    = Source_Venda;
            int item = Source_Venda.Find("Id_Venda", IdVenda);
            Source_Venda.Position = item;
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
        private void GridVenda_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int IdEntregador = 0;
            if (e.ColumnIndex == 7)
            {
                IdEntregador = int.Parse(GridVenda.CurrentRow.Cells[7].Value.ToString());
                GridVenda.CurrentRow.Cells[9].Value = DateTime.Now.Date.ToShortDateString();
            }
            else if (e.ColumnIndex == 8)
            {
                IdEntregador = int.Parse(GridVenda.CurrentRow.Cells[8].Value.ToString());
                GridVenda.CurrentRow.Cells[9].Value = DateTime.Now.Date.ToShortDateString();
            }
            else if (e.ColumnIndex == 9)
            {
                IdEntregador  = int.Parse(GridVenda.CurrentRow.Cells[7].Value.ToString());
                DateTime Dt   = DateTime.Parse(GridVenda.CurrentCell.Value.ToString());
                DateTime DtVd = DateTime.Parse(GridVenda.CurrentRow.Cells[2].Value.ToString());
                if (Dt.Date < DtVd.Date)
                {
                    MessageBox.Show("Previsão de Entrega não pode ser inferior a data da venda", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Venda.CancelEdit();
                    IdEntregador = 0;
                }
                else if (IdEntregador == 0)
                {
                    MessageBox.Show("Entregador não informado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Venda.CancelEdit();
                    IdEntregador = 0;
                }

            }  
            Controles.Verificar ExisteEntregador = new Controles.Verificar();
            ExisteEntregador.Controle = Controle;
            DateTime DtEntrega = DateTime.Parse(GridVenda.CurrentRow.Cells[9].Value.ToString());
            if (ExisteEntregador.Verificar_ExisteCadastro("Id_Entregador", "SELECT * FROM ENTREGADORES WHERE ID_ENTREGADOR=" + IdEntregador.ToString()) > 0 || IdEntregador == 0)
            {
                Controle.ExecutaSQL("Update MvVenda set Id_Entregador=" + IdEntregador.ToString() + ",PREVENTREGA=Convert(DateTime,'" + DtEntrega.ToShortDateString() + "',103)  Where Id_Venda=" + GridVenda.CurrentRow.Cells[0].Value.ToString());
                GridVenda.CurrentRow.Cells[7].Value = IdEntregador.ToString();
                GridVenda.CurrentRow.Cells[8].Value = IdEntregador.ToString();
                //Registrando Movimento de Auditoria
                if (IdEntregador > 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString()), GridVenda.CurrentRow.Cells[3].Value.ToString(), 7, "Mapa do Entregador: " + IdEntregador.ToString() + " Prev.Entrega" + DtEntrega.ToShortDateString());
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString()), GridVenda.CurrentRow.Cells[3].Value.ToString(), 7, "Retirando do Mapa de Entrega");
            }
            else
                MessageBox.Show("Código do entregador não existe", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            //PopularGridVenda();
            GridVenda.CurrentCell = GridVenda.CurrentRow.Cells[e.ColumnIndex];
        }
        private void GridVendaDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                MessageBox.Show("Data Previsão de Entrega invalida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Venda.CancelEdit();
            }
        }

        private void BtnPesqVD_Click(object sender, EventArgs e)
        {
            PopularGridVenda();
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (LstPesqEntregador.SelectedValue.ToString() == "0")
                MessageBox.Show("Selecione um Entregador", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                string sSql = "SELECT T1.ID_VENDA,T1.NUMDOCUMENTO,T1.FORMNF,T2.RAZAOSOCIAL,RTRIM(T1.ENDERECO)+','+RTRIM(T1.NUMERO)+' '+RTRIM(T1.COMPLEMENTO) AS ENDERECO," +
                              " T1.FONE,T1.CEP,T1.BAIRRO,T1.CIDADE,T3.ENTREGADOR,T4.VENDEDOR,T5.FORMAPGTO,T1.ID_VDMASTER,T1.VLRTOTAL,T7.VENCIMENTO,T7.VLRORIGINAL,T1.PREVENTREGA,T8.DOCUMENTO FROM MVVENDA T1" +
                              " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                              " LEFT JOIN ENTREGADORES T3 ON (T3.ID_ENTREGADOR=T1.ID_ENTREGADOR)" +
                              " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " LEFT JOIN FORMAPAGAMENTO T5 ON (T5.ID_FORMAPGTO=T1.ID_FORMAPGTO)" +
                              " LEFT JOIN LANCFINANCEIRO T7 ON (T7.ID_VENDA=T1.ID_VENDA)"+
                              " LEFT JOIN TIPODOCUMENTO T8 ON (T8.ID_DOCUMENTO=T7.ID_TIPODOCUMENTO)" +
                              " WHERE ((T1.STATUS = 2) OR (T1.STATUS = 1 AND T1.TPVENDA IN ('BONIF','EMVF','CO','PR','AM','TROCA'))) AND T1.ID_ENTREGADOR=" + LstPesqEntregador.SelectedValue.ToString();
                if (Chk_Periodo.Checked)
                    sSql = sSql + " AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                sSql = sSql + " ORDER BY T2.RAZAOSOCIAL,T1.ID_VENDA";

                BtnImprimir.Enabled = false;
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelMapEntrega RelMapa = new Relatorios.RelMapEntrega();
                DataSet TabRel = new DataSet();                
                TabRel = Controle.ConsultaTabela(sSql);                
                RelMapa.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = RelMapa;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                BtnImprimir.Enabled = true;
            }
        }        
    }
}
