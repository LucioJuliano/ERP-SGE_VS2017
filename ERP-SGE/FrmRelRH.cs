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
    public partial class FrmRelRH : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;  
        
        public FrmRelRH()
        {
            InitializeComponent();
        }

        private void FrmRelRH_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            LstMesEventos.SelectedIndex = DateTime.Now.Month - 1;
            TxtAnoEventos.Value = DateTime.Now.Year;
            Op01.Checked = true;

            LstDepartamento = FrmPrincipal.PopularCombo("SELECT Id_Departamento,Departamento FROM Departamentos ORDER BY Departamento", LstDepartamento,"Todos");
            LstFilial       = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilial, "Todos");
            LstFilialCtps   = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilialCtps, "Todos");
            LstEventos      = FrmPrincipal.PopularCombo("SELECT Id_Codigo,CASE PROVDESC WHEN 0 THEN '(P)-'+SubString(Descricao,1,40) ELSE '(D)-'+SubString(Descricao,1,40) END Descricao FROM ProventosDescontos ORDER BY Descricao", LstEventos, "Todos");            
        }

        private void AtualizarTela(object sender, EventArgs e)
        {
            BoxEventos.Visible    = Op04.Checked || Op08.Checked;
            BoxMesAno.Visible     = !Op05.Checked && !Op08.Checked && !Op11.Checked && !Op12.Checked;  
            BoxDepart.Visible     = !Op08.Checked;
            BoxFilial.Visible     = !Op08.Checked;
            BoxFiliaCtps.Visible  = !Op08.Checked && !Op10.Checked;
            BoxFunc.Visible       = Op08.Checked || Op11.Checked;
            Cb_Quizena.Visible    = Op06.Checked || Op09.Checked;
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (Op01.Checked)
            {
                string sSql = "SELECT T3.Fantasia as Filial,T4.Departamento,T2.Nome,(T1.Valor-(SELECT ISNULL(SUM(T5.VALOR),0) FROM  MVFOLHAPAG T5 WHERE T5.Id_ProvDesc=10 AND T5.ID_FUNC=T1.ID_FUNC AND T5.MESANO=T1.MESANO)) AS VALOR" +
                              ",T1.MesAno,T2.Banco,T2.Agencia,T2.Conta FROM MVFOLHAPAG T1 " +
                              " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                              " LEFT JOIN Empresa_Filial T3 ON (T3.Id_Filial=T2.Id_FilialTrab)" +
                              " LEFT JOIN Departamentos T4 ON (T4.Id_Departamento=T2.Id_Departamento)" +
                              " WHERE T3.Id_Filial<>2 and T1.MesAno='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "' AND T1.ID_PROVDESC=4";

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T2.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T2.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T2.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();


                sSql = sSql + " ORDER BY T3.Fantasia,T4.Departamento,T2.Nome";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelRhQuizena Rel001 = new Relatorios.RelRhQuizena();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + "     CTPS:" + LstFilialCtps.Text;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op02.Checked)
            {
                string sSQL1 = "SELECT DISTINCT T3.ID_FILIAL,T3.Fantasia,T2.ID_FUNC,T2.Nome,T4.Departamento,T1.MesAno FROM MvFolhaPag T1" +
                               " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                               " LEFT JOIN Empresa_Filial T3 ON (T3.Id_Filial=T2.Id_FilialTrab)" +
                               " LEFT JOIN Departamentos T4 ON (T4.Id_Departamento=T2.Id_Departamento)" +
                               " WHERE T3.Id_Filial<>2 and T1.ID_PROVDESC<>10 AND T1.MesAno='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'";

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                sSQL1 = sSQL1 + " ORDER BY T3.Fantasia,T2.Nome";


                string sSQL2 = "SELECT T1.ID_FUNC,T3.ID_FILIAL,T3.Fantasia,T2.Nome,T4.Descricao AS NomeProvDesc,T4.ProvDesc,T1.Qtde_Ref,T1.Valor,T1.Descricao,T1.MesAno,T2.funcao FROM MvFolhaPag T1" +
                                   " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                                   " LEFT JOIN Empresa_Filial T3 ON (T3.Id_Filial=T2.Id_FilialTrab)" +
                                   " LEFT JOIN ProventosDescontos T4 ON (T4.Id_Codigo=T1.Id_ProvDesc)" +
                                   " WHERE T3.Id_Filial<>2 and T1.ID_PROVDESC<>10 AND T1.MesAno='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'";

                 if (LstFilial.SelectedValue.ToString() != "0")
                     sSQL2 = sSQL2 + " AND T2.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                 if (LstDepartamento.SelectedValue.ToString() != "0")
                     sSQL2 = sSQL2 + " AND T2.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                 sSQL2 = sSQL2 + " ORDER BY T3.Fantasia,T2.Nome,T4.ProvDesc,T4.Id_Codigo";
                              

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelFgFolhaDem Rel001 = new Relatorios.RelFgFolhaDem();
                DataSet TabRel = new DataSet();
                DataSet TabRel2 = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                TabRel2 = Controle.ConsultaTabela(sSQL2);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                Rel001.Database.Tables[1].SetDataSource(TabRel2.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + "     CTPS:" + LstFilialCtps.Text; ;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op03.Checked || Op06.Checked || Op09.Checked)
            {
                string sSQL1 = "SELECT T3.ID_FILIAL,T3.Fantasia,T5.Departamento,T2.ID_FUNC,T2.Nome,T2.Banco,T2.Agencia,T2.Conta," +
                               "T2.DTADMISSAO,T2.SALARIOATUAL,T2.SALARIOCTPS," +
                               " CASE T2.TIPOCONTA WHEN 0 THEN 'CORRENTE' WHEN 1 THEN 'POUPANÇA' ELSE ' ' END AS TIPOCONTA,";

                if (Cb_Quizena.Checked)
                    sSQL1 = sSQL1 + " (1 * isnull(T1.Valor,0)) as Liquido ";
                else
                    sSQL1 = sSQL1 + " isnull(SUM(case t4.provdesc when 0 then T1.Valor end),0)-isnull(SUM(case t4.provdesc when 1 then T1.Valor end),0) as Liquido";

                sSQL1 = sSQL1 + " FROM  MvFolhaPag T1" +
                               " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                               " LEFT JOIN Empresa_Filial T3 ON (T3.Id_Filial=T2.Id_FilialTrab)" +
                               " LEFT JOIN ProventosDescontos T4 ON (T4.Id_Codigo=T1.Id_ProvDesc)" +
                               " LEFT JOIN Departamentos T5 ON (T5.Id_Departamento=T2.Id_Departamento)" +
                               " WHERE T3.Id_Filial<>2 and T1.ID_PROVDESC<>10 AND T1.MesAno='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'";

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                if (Op06.Checked || Op09.Checked)
                {
                    if (Op06.Checked)
                        sSQL1 = sSQL1 + "AND rtrim(T2.Banco)<>''";
                    else
                        sSQL1 = sSQL1 + "AND RTRIM(T2.Banco)=''";

                    if (Cb_Quizena.Checked)
                        sSQL1 = sSQL1 + "AND T1.Id_ProvDesc=4";
                }
                if (!Cb_Quizena.Checked)
                    sSQL1 = sSQL1 + " Group by T3.ID_FILIAL,T3.Fantasia,T5.Departamento,T2.ID_FUNC,T2.Nome,T2.Banco,T2.Agencia,T2.Conta,T2.DTADMISSAO,T2.SALARIOATUAL,T2.SALARIOCTPS,TIPOCONTA";

                FrmRelatorios FrmRel = new FrmRelatorios();
                if (LstMesEventos.SelectedIndex == 13)
                {
                    Relatorios.RelResumoFolhaPg13 Rel0013 = new Relatorios.RelResumoFolhaPg13();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSQL1);
                    Rel0013.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel0013;
                    if (Op06.Checked || Op09.Checked)
                    {
                        if (Op06.Checked)
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel0013.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + " Listagem (Banco)     CTPS:" + LstFilialCtps.Text;
                        else
                            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel0013.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + " Listagem (Dinheiro)     CTPS:" + LstFilialCtps.Text;
                    }
                    else
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel0013.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + "     CTPS:" + LstFilialCtps.Text;
                    FrmRel.ShowDialog();
                    Rel0013.Dispose();

                }
                else
                {
                    Relatorios.RelResumoFolhaPg Rel001 = new Relatorios.RelResumoFolhaPg();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSQL1);
                    Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;

                    if (Op06.Checked)
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + " Listagem (Banco)     CTPS:" + LstFilialCtps.Text;
                    else
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + "     CTPS:" + LstFilialCtps.Text;
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                }
            }

            if (Op04.Checked)
            {
                string sSQL1 = "SELECT T5.Fantasia,T3.PROVDESC,CASE T3.PROVDESC WHEN 0 THEN '(P)-'+SubString(T3.Descricao,1,40) ELSE '(D)-'+SubString(T3.Descricao,1,40) END Descricao,T2.Nome,T4.Departamento,T1.Valor,T1.MesAno FROM MvFolhaPag T1" +
                               " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                               " LEFT JOIN ProventosDescontos T3 ON (T3.Id_Codigo=T1.Id_ProvDesc)" +
                               " LEFT JOIN Departamentos T4 ON (T4.Id_Departamento=T2.Id_Departamento)" +
                               " LEFT JOIN Empresa_Filial T5 ON (T5.Id_Filial=T2.Id_FilialTrab)" +
                               " WHERE T5.Id_Filial<>2 and T1.MesAno='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'";
  

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                if (LstEventos.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_ProvDesc=" + LstEventos.SelectedValue.ToString();

                sSQL1 = sSQL1 + " ORDER BY T2.Id_FilialTrab,T1.Id_ProvDesc,T2.NOME";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelRHLancEventos Rel001 = new Relatorios.RelRHLancEventos();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString() + "     CTPS:" + LstFilialCtps.Text;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op05.Checked)
            {
                string sSQL1 = "SELECT ISNULL(T2.Fantasia,'  ') AS FILIAL_TRAB,T4.DEPARTAMENTO,T3.Fantasia AS FILIAL_REG,T1.Nome,T1.salarioCtps,T1.SalarioAtual,T1.AdiantSalario,T1.DtAdmissao,T1.FUNCAO FROM Funcionarios T1" +
                               " LEFT JOIN Empresa_Filial T2 ON (T2.Id_Filial=T1.Id_FilialTrab)" +
                               " LEFT JOIN Empresa_Filial T3 ON (T3.Id_Filial=T1.Id_FilialReg)" +
                               " LEFT JOIN Departamentos T4 ON (T4.Id_Departamento=T1.Id_Departamento)" +
                               " Where T3.Id_Filial<>2 and T1.DtDemissao IS NULL";

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();
                                
                sSQL1 = sSQL1 + " ORDER BY T1.Id_FilialTrab,T1.NOME";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelRHListaSalarios Rel001 = new Relatorios.RelRHListaSalarios();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                //((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op07.Checked)
            {
                string sSQL1 = "SELECT T5.Fantasia,T4.Departamento,T3.PROVDESC,CASE T3.PROVDESC WHEN 0 THEN '(P)-'+SubString(T3.Descricao,1,40) ELSE '(D)-'+SubString(T3.Descricao,1,40) END Descricao,IsNull(SUM(T1.Valor),0) AS TOTAL"+
                               "  FROM MvFolhaPag T1 "+
                               " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func) "+
                               " LEFT JOIN ProventosDescontos T3 ON (T3.Id_Codigo=T1.Id_ProvDesc) "+
                               " LEFT JOIN Departamentos T4 ON (T4.Id_Departamento=T2.Id_Departamento) "+
                               " LEFT JOIN Empresa_Filial T5 ON (T5.Id_Filial=T2.Id_FilialTrab) "+
                               " WHERE T5.Id_Filial<>2 and T1.MesAno='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'";
   
                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                sSQL1 = sSQL1 + " GROUP BY T5.Fantasia,T4.Departamento,T3.PROVDESC,T3.Descricao ORDER BY 1,2,3,4";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumProvDesc Rel001 = new Relatorios.RelResumProvDesc();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op08.Checked)
            {

                if (int.Parse(TxtCodFunc.Text) == 0)
                {
                    MessageBox.Show("Favor Informar Funcionario", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                string sSQL1 = "SELECT 1 as Tipo,SUBSTRING(t1.mesano,4,4) as Ano,SUBSTRING(t1.mesano,1,2) as Mes,T1.Id_ProvDesc,t2.Nome,T3.ProvDesc,T3.Descricao,T1.MesAno,t1.Descricao as ObsLanc,t1.Valor AS VlrPrevisto,isnull(T4.Valor,0) AS VLRDESCONTO  " +
                               " FROM ProvDescFunc T1" +
                               "  LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                               "  LEFT JOIN ProventosDescontos T3 ON (T3.Id_Codigo=T1.Id_ProvDesc) " +
                               "  LEFT JOIN MvFolhaPag T4 ON (T4.Id_Func=T1.Id_Func AND T4.Id_ProvDesc=T1.Id_ProvDesc AND T4.MesAno=T1.MesAno AND T4.Valor=T1.Valor)" +
                               " WHERE T1.Id_Func=" + TxtCodFunc.Text +
                               "  and T1.MesAno='00/0000'";

                if (LstEventos.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_ProvDesc=" + LstEventos.SelectedValue.ToString();

                sSQL1 = sSQL1 + " UNION" +
                               " SELECT 2 as Tipo,SUBSTRING(t1.mesano,4,4) as Ano,SUBSTRING(t1.mesano,1,2) as Mes,T1.Id_ProvDesc,t2.Nome,T3.ProvDesc,T3.Descricao,T1.MesAno,t1.Descricao as ObsLanc,t1.Valor AS VlrPrevisto,T4.Valor AS VLRDESCONTO  " +
                               " FROM ProvDescFunc T1" +
                               " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                               " LEFT JOIN ProventosDescontos T3 ON (T3.Id_Codigo=T1.Id_ProvDesc)   " +
                               " LEFT JOIN MvFolhaPag T4 ON (T4.Id_Func=T1.Id_Func AND T4.Id_ProvDesc=T1.Id_ProvDesc AND T4.MesAno=T1.MesAno AND T4.Valor=T1.Valor)" +
                               " WHERE T1.Id_Func=" + TxtCodFunc.Text +
                               " and T1.MesAno<>'00/0000'";
                 if (LstEventos.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_ProvDesc=" + LstEventos.SelectedValue.ToString();

                 sSQL1 = sSQL1 + "ORDER BY T3.ProvDesc";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelFichaFuncProvDesc Rel001 = new Relatorios.RelFichaFuncProvDesc();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op10.Checked)
            {
                string sSQL1 = "select T4.Filial,T5.Departamento,T2.Nome,SUM(T1.Valor) as Total from MvFolhaPag t1"+
                               " left join Funcionarios t2 on (t2.Id_Func=t1.Id_Func)"+
                               " left join ProventosDescontos t3 on (t3.Id_Codigo=t1.Id_ProvDesc)"+
                               " left join Empresa_Filial t4 on (t4.Id_Filial=t2.Id_FilialTrab)"+
                               " LEFT JOIN Departamentos T5 ON (T5.Id_Departamento=T2.Id_Departamento) "+
                               " WHERE T4.Id_Filial<>2 and T3.FOLHABRUTA=1 AND T1.MesAno='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'";
                
                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();
                                
                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T2.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                sSQL1 = sSQL1 + " GROUP BY T4.Filial,T5.Departamento,T2.Nome";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResFolhaBruta Rel001 = new Relatorios.RelResFolhaBruta();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Mes:" + LstMesEventos.Text + " ANO:" + TxtAnoEventos.Value.ToString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op11.Checked)
            {
                string sSQL1 = "select t1.*,t2.Departamento,t3.Fantasia as FilialReg,t4.Fantasia as FilialTrab,t5.RazaoSocial as NomeCompra from Funcionarios t1" +
                               " left join Departamentos t2 on (t2.Id_Departamento=t1.Id_Departamento)" +
                               " left join Empresa_Filial t3 on (t3.Id_Filial=t1.Id_FilialReg)" +
                               " left join Empresa_Filial t4 on (t4.Id_Filial=t1.Id_FilialTrab)" +
                               " left join Pessoas t5 on (t5.Id_Pessoa=t1.Id_Pessoa)";

                sSQL1 = sSQL1 + " WHERE T1.DtDemissao is null";

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                if (int.Parse(TxtCodFunc.Text)>0)
                    sSQL1 = sSQL1 + " AND T1.Id_Func=" + TxtCodFunc.Text;
                               

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelFichaFuncionario Rel001 = new Relatorios.RelFichaFuncionario();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op12.Checked)
            {
                string sSQL1 = "SELECT T2.Fantasia,T3.Departamento,T1.Nome,"+
                               " ISNULL((SELECT P1.VALOR FROM ProvDescFunc P1 WHERE P1.ID_FUNC=T1.ID_FUNC AND P1.Id_ProvDesc=12 AND P1.MesAno='00/0000'),0) AS VLRUNIMED,"+
                               " ISNULL((SELECT ISNULL(P2.VALOR,0) FROM ProvDescFunc P2 WHERE P2.ID_FUNC=T1.ID_FUNC AND P2.Id_ProvDesc=34 AND P2.MesAno='00/0000'),0) AS VLRBENFEMP FROM Funcionarios T1"+
                               " LEFT JOIN Empresa_Filial T2 ON (T2.Id_Filial=T1.Id_FilialTrab)"+
                               " LEFT JOIN Departamentos T3 ON (T3.Id_Departamento=T1.Id_Departamento)"+
                               " WHERE T1.DtDemissao is null";

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_FilialTrab=" + LstFilial.SelectedValue.ToString();

                if (LstFilialCtps.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_FilialReg=" + LstFilialCtps.SelectedValue.ToString();

                if (LstDepartamento.SelectedValue.ToString() != "0")
                    sSQL1 = sSQL1 + " AND T1.Id_Departamento=" + LstDepartamento.SelectedValue.ToString();

                if (int.Parse(TxtCodFunc.Text) > 0)
                    sSQL1 = sSQL1 + " AND T1.Id_Func=" + TxtCodFunc.Text;

                sSQL1 = sSQL1 + " ORDER BY T2.Fantasia,T3.Departamento,T1.Nome";


                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelUnimed Rel001 = new Relatorios.RelUnimed();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL1);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

        }

        private void BtnBuscaFunc_Click(object sender, EventArgs e)
        {
            FrmBuscaFuncionarios PesqFunc = new FrmBuscaFuncionarios();
            PesqFunc.FrmPrincipal = this.FrmPrincipal;
            PesqFunc.ShowDialog();
            if (PesqFunc.CadFunc.IdFunc > 0)
            {
                TxtCodFunc.Text = PesqFunc.CadFunc.IdFunc.ToString();
                TxtFuncionario.Text = PesqFunc.CadFunc.Nome.Trim();
            }
            else
            {
                TxtCodFunc.Text = "0";
                TxtFuncionario.Text = "";
            }
            PesqFunc.Dispose();
        }

    }
}
