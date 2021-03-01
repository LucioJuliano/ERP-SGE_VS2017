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
    public partial class FrmRelFinanceiro : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public FrmRelFinanceiro()
        {
            InitializeComponent();
        }
        private void FrmRelFinanceiro_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Op01.Checked     = true;
            Dt1.Value        = DateTime.Now;
            Dt2.Value        = DateTime.Now;
            LstCaixa         = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstCaixa);
            LstFilial        = FrmPrincipal.PopularCombo("SELECT Id_Filial,SUBSTRING(FANTASIA,1,60) AS FILIAL FROM Empresa_Filial ORDER BY FANTASIA", LstFilial, "Todas");
            LstDepartamento  = FrmPrincipal.PopularCombo("SELECT ID_Departamento,Departamento FROM Departamentos ORDER BY Departamento", LstDepartamento,"Todos");
            LstCusto         = FrmPrincipal.PopularCombo("SELECT ID_Custo,Custo FROM CentroCusto ORDER BY Custo", LstCusto, "Todos");            
        }
        private void AtualizarTela(object sender, EventArgs e)
        {
            BoxCaixa.Visible  = Op01.Checked;
            label16.Visible   = !Op01.Checked;
            Dt2.Visible       = !Op01.Checked;
            BoxFilial.Visible = Op03.Checked || Op04.Checked || Op05.Checked;
            BoxDepart.Visible = Op03.Checked || Op04.Checked;
            BoxCusto.Visible  = Op03.Checked || Op04.Checked || Op05.Checked;            
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (Op01.Checked)
            {
                string sSql1 = "SELECT T1.ID_LANC,T4.AGENTE,T2.RAZAOSOCIAL,T1.DESCRICAO,T3.DOCUMENTO,CASE T1.TPLANC WHEN 1 THEN VALOR ELSE 0 END AS DEBITO," +
                             " CASE T1.TPLANC WHEN 2 THEN VALOR ELSE 0 END AS CREDITO FROM MVCONTACAIXA T1" +
                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                             " LEFT JOIN TIPODOCUMENTO T3 ON (T3.ID_DOCUMENTO=T1.ID_DOCUMENTO)" +
                             " LEFT JOIN AGENTECOBRADOR T4 ON (T4.ID_AGENTE=T1.ID_AGENTE)" +
                             " WHERE T1.STATUS=0 AND T1.DATA = Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.ID_CAIXA=" + LstCaixa.SelectedValue.ToString()+
                             " ORDER BY T4.AGENTE,T1.ID_LANC";

                string sSql2 = "SELECT T1.DOCUMENTO,ISNULL((SELECT TOP 1 SALDO FROM SALDOCONTACAIXA T2 WHERE T2.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() +
                               " AND T2.ID_DOCUMENTO=T1.ID_DOCUMENTO AND T2.DATA < Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) ORDER BY T2.DATA DESC),0) AS SALDO FROM TIPODOCUMENTO T1" +
                               " WHERE ISNULL((SELECT TOP 1 SALDO FROM SALDOCONTACAIXA T2 WHERE T2.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() +
                               " AND T2.ID_DOCUMENTO=T1.ID_DOCUMENTO AND T2.DATA < Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) ORDER BY T2.DATA DESC),0) <> 0";

                string sSql3 = "SELECT T1.DOCUMENTO,ISNULL((SELECT TOP 1 SALDO FROM SALDOCONTACAIXA T2 WHERE T2.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() +
                               " AND T2.ID_DOCUMENTO=T1.ID_DOCUMENTO AND T2.DATA <= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) ORDER BY T2.DATA DESC),0) AS SALDO FROM TIPODOCUMENTO T1" +
                               " WHERE ISNULL((SELECT TOP 1 SALDO FROM SALDOCONTACAIXA T2 WHERE T2.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() +
                               " AND T2.ID_DOCUMENTO=T1.ID_DOCUMENTO AND T2.DATA <= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) ORDER BY T2.DATA DESC),0) <>0 ";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelLivroCaixa Rel001 = new Relatorios.RelLivroCaixa();
                DataSet Extrato = new DataSet();
                DataSet SldAnt = new DataSet();
                DataSet Saldo = new DataSet();
                Extrato = Controle.ConsultaTabela(sSql1);
                SldAnt = Controle.ConsultaTabela(sSql2);
                Saldo = Controle.ConsultaTabela(sSql3);
                Rel001.Database.Tables[0].SetDataSource(Extrato.Tables[0]);
                Rel001.Database.Tables[1].SetDataSource(SldAnt.Tables[0]);
                Rel001.Database.Tables[2].SetDataSource(Saldo.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblData"])).Text   = "Data: " + Dt1.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblCaixa"])).Text  = "Caixa: " + LstCaixa.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op02.Checked) // Resumo da Receita X Despesa
            {
                string sSql = "SELECT T1.VENCIMENTO,SUM(CASE T1.PagRec WHEN 1 THEN T1.VLRORIGINAL ELSE 0 END) AS PAGAR, SUM(CASE T1.PagRec WHEN 2 THEN T1.VLRORIGINAL ELSE 0 END) AS RECEBER FROM LANCFINANCEIRO T1" +
                              " WHERE T1.STATUS=0 AND T1.VENCIMENTO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.VENCIMENTO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              " GROUP BY T1.VENCIMENTO ORDER BY T1.VENCIMENTO";
                                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoRecXDesp Rel001 = new Relatorios.RelResumoRecXDesp();
                DataSet Tab = new DataSet();                
                Tab = Controle.ConsultaTabela(sSql);                
                Rel001.Database.Tables[0].SetDataSource(Tab.Tables[0]);                
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op03.Checked) // Movimentação por Centro de Custo
            {
                string sSql = "SELECT T3.CUSTO,T4.FANTASIA AS FILIAL,T6.DEPARTAMENTO,T1.DATALANC,T2.RAZAOSOCIAL,T1.NUMDOCUMENTO,T1.NOTAFISCAL,T1.REFERENTE,T1.VENCIMENTO,T1.VLRORIGINAL,T1.DTBAIXA,T1.VLRBAIXA,T5.DOCUMENTO FROM LANCFINANCEIRO T1 " +
                              " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                              " LEFT JOIN CENTROCUSTO T3 ON (T3.ID_CUSTO=T1.ID_CUSTO)" +
                              " LEFT JOIN EMPRESA_FILIAL T4 ON (T4.ID_FILIAL=T1.ID_FILIAL)" +
                              " LEFT JOIN TIPODOCUMENTO T5 ON (T5.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO)" +
                              " LEFT JOIN DEPARTAMENTOS T6 ON (T6.ID_DEPARTAMENTO=T1.ID_DEPARTAMENTO)";

                sSql = sSql + " WHERE T1.STATUS=1 AND T1.PAGREC=1 AND  T1.DTBAIXA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTBAIXA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (int.Parse(LstFilial.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_FILIAL=" + LstFilial.SelectedValue.ToString();

                if (int.Parse(LstDepartamento.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_DEPARTAMENTO=" + LstDepartamento.SelectedValue.ToString();

                if (int.Parse(LstCusto.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_CUSTO=" + LstCusto.SelectedValue.ToString();

                sSql = sSql + " ORDER BY T1.DTBAIXA";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelMovimCCusto Rel001 = new Relatorios.RelMovimCCusto();
                DataSet Tab = new DataSet();
                Tab = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(Tab.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op04.Checked) // Resumo do Centro de Custo
            {
                string sSql = "SELECT T2.CUSTO,T3.FANTASIA AS FILIAL,T4.DEPARTAMENTO,SUM(T1.VLRBAIXA) AS TOTAL FROM LANCFINANCEIRO T1 "+
                              " LEFT JOIN CENTROCUSTO T2 ON (T2.ID_CUSTO=T1.ID_CUSTO) "+
                              " LEFT JOIN EMPRESA_FILIAL T3 ON (T3.ID_FILIAL=T1.ID_FILIAL) "+
                              " LEFT JOIN DEPARTAMENTOS T4 ON (T4.ID_DEPARTAMENTO=T1.ID_DEPARTAMENTO) "+
                              " WHERE T1.STATUS=1 AND T1.PAGREC=1 AND  T1.DTBAIXA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTBAIXA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";
                
                if (int.Parse(LstFilial.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_FILIAL=" + LstFilial.SelectedValue.ToString();

                if (int.Parse(LstDepartamento.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_DEPARTAMENTO=" + LstDepartamento.SelectedValue.ToString();

                if (int.Parse(LstCusto.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_CUSTO=" + LstCusto.SelectedValue.ToString();               

                sSql = sSql + "GROUP BY T2.CUSTO,T3.FANTASIA,T4.DEPARTAMENTO ORDER BY T2.CUSTO,T3.FANTASIA,T4.DEPARTAMENTO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoCCusto Rel001 = new Relatorios.RelResumoCCusto();
                DataSet Tab = new DataSet();
                Tab = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(Tab.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op05.Checked) // Totalizador por Centro de Custo
            {
                string sSql = "SELECT T2.CUSTO,SUM(T1.VLRBAIXA) AS TOTAL FROM LANCFINANCEIRO T1 " +
                              " LEFT JOIN CENTROCUSTO T2 ON (T2.ID_CUSTO=T1.ID_CUSTO) " +                              
                              " WHERE T1.STATUS=1 AND T1.PAGREC=1 AND  T1.DTBAIXA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTBAIXA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (int.Parse(LstFilial.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_FILIAL=" + LstFilial.SelectedValue.ToString();
                                
                if (int.Parse(LstCusto.SelectedValue.ToString()) > 0)
                    sSql = sSql + " AND T1.ID_CUSTO=" + LstCusto.SelectedValue.ToString();

                sSql = sSql + "GROUP BY T2.CUSTO ORDER BY T2.CUSTO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelTotalCCusto Rel001 = new Relatorios.RelTotalCCusto();
                DataSet Tab = new DataSet();
                Tab = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(Tab.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;

                if (int.Parse(LstFilial.SelectedValue.ToString()) > 0)
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = "Filial: " + LstFilial.Text.Trim();
                else
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = "Filial: Todas Filiais";

                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op06.Checked) // Listagem de Agendamento de CObrança
            {
                string sSql = "SELECT T1.Data,T1.DtRetorno,T3.RazaoSocial,T2.Vencimento,T2.VlrOriginal,T2.NumDocumento,T3.FONE,T3.Celular,T3.Contato,T1.Informacao FROM RegCobranca T1"+
                              "  LEFT JOIN LancFinanceiro T2 ON (T2.ID_LANC=T1.Id_PagRec)"+
                              "  LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)"+
                              " WHERE T1.DTRETORNO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTRETORNO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";
                
                sSql = sSql + " ORDER BY T1.DTRETORNO, T3.RAZAOSOCIAL";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelRetornoCob Rel001 = new Relatorios.RelRetornoCob();
                DataSet Tab = new DataSet();
                Tab = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(Tab.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();                
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op07.Checked) // Listagem de Cobrança Realizada
            {
                string sSql = "SELECT T1.Data,T1.DtRetorno,T3.RazaoSocial,T2.Vencimento,T2.VlrOriginal,T2.NumDocumento,T3.FONE,T3.Celular,T3.Contato,T1.Informacao FROM RegCobranca T1" +
                              "  LEFT JOIN LancFinanceiro T2 ON (T2.ID_LANC=T1.Id_PagRec)" +
                              "  LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)" +
                              " WHERE T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                sSql = sSql + " ORDER BY T1.DTRETORNO, T3.RAZAOSOCIAL";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelRegCobRealizada Rel001 = new Relatorios.RelRegCobRealizada();
                DataSet Tab = new DataSet();
                Tab = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(Tab.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
        }
    }
}
