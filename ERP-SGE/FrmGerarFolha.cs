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
    public partial class FrmGerarFolha : Form
    {
        Funcoes Controle     = new Funcoes();
        Funcionarios CadFunc = new Funcionarios();
        MvFolhaPag MvFolha   = new MvFolhaPag();
        Verificar VerifCad   = new Verificar();

        public TelaPrincipal FrmPrincipal;        

        private DataSet TabEventos;
        private BindingSource Source_Eventos;

        public FrmGerarFolha()
        {
            InitializeComponent();
        }
        private void FrmGerarFolha_Load(object sender, EventArgs e)
        {
            Controle.Conexao  = FrmPrincipal.Conexao;
            CadFunc.Controle  = Controle;
            MvFolha.Controle  = Controle;
            TabEventos        = new DataSet();
            Source_Eventos    = new BindingSource();
            VerifCad.Controle = Controle;
            MvFolha.IdLanc   = 0;
            if (DateTime.Now.Month == 1)
            {
                LstMesEventos.SelectedIndex = 12;
                TxtAnoEventos.Value = DateTime.Now.Year-1;
            }
            else
            {
                LstMesEventos.SelectedIndex = DateTime.Now.Month - 1;
                TxtAnoEventos.Value = DateTime.Now.Year;
            }
            ColEvento = FrmPrincipal.PopularComboGrid("SELECT Id_Codigo,CASE PROVDESC WHEN 0 THEN '(P)-'+SubString(Descricao,1,40) ELSE '(D)-'+SubString(Descricao,1,40) END Descricao FROM ProventosDescontos ORDER BY Descricao", ColEvento);
            LstFilialPesq = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilialPesq, "Todas");
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
        private void BtnBuscaFunc_Click(object sender, EventArgs e)
        {
            FrmBuscaFuncionarios PesqFunc = new FrmBuscaFuncionarios();
            PesqFunc.FrmPrincipal = this.FrmPrincipal;
            PesqFunc.ShowDialog();
            SetaFunc(PesqFunc.CadFunc.IdFunc);
            PopularGridEventos();
        }
        private void SetaFunc(int IdFunc)
        {

            CadFunc.LerDados(IdFunc);
            TxtCodFunc.Text     = CadFunc.IdFunc.ToString();
            TxtFuncionario.Text = CadFunc.Nome.Trim();
            TxtMatricula.Text   = string.Format("{0:D6}", CadFunc.Matricula) + " - " + CadFunc.Funcao;
            TxtLotado.Text = BuscaFilialTrab(CadFunc.IdFilialTrab);
        }

        private string BuscaFilialTrab(int IdFilial)
        {
            SqlDataReader FilialTab;
            FilialTab = Controle.ConsultaSQL("SELECT FANTASIA FROM EMPRESA_FILIAL WHERE ID_FILIAL=" + IdFilial.ToString());
            if (FilialTab.HasRows)
            {
                FilialTab.Read();
                return FilialTab["Fantasia"].ToString().Trim();
            }
            else
                return "";
        }
        private void BtnGeraAdiant_Click(object sender, EventArgs e)
        {

            if (LstMesEventos.SelectedIndex != DateTime.Now.Date.Month)
            {
                MessageBox.Show("Selecione o Mês Atual", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Confirma Gerar a Folha de Adiantamento (Quizenal)?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SetaFunc(0);
                PopularGridEventos();
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                {
                    Controle.ExecutaSQL("DELETE FROM MVFOLHAPAG WHERE MESANO='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "' AND ID_PROVDESC=4 " +
                                        " AND ID_FUNC IN (SELECT ID_FUNC FROM FUNCIONARIOS WHERE ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString()+")");
                }
                else
                {
                    Controle.ExecutaSQL("DELETE FROM MVFOLHAPAG WHERE MESANO='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "' AND ID_PROVDESC=4");
                }
                                
                //---Gerando a Folha
                //--Lançamento da Quizena
                DataSet TabFunc = new DataSet();
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                    TabFunc = Controle.ConsultaTabela("SELECT Id_Func,SalarioCtps,SalarioAtual,AdiantSalario FROM Funcionarios WHERE Demissao=0 AND AdiantSalario > 0 and ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString());
                else
                    TabFunc = Controle.ConsultaTabela("SELECT Id_Func,SalarioCtps,SalarioAtual,AdiantSalario FROM Funcionarios WHERE Demissao=0 and AdiantSalario > 0");

                BarProc.Visible = true;
                if (TabFunc.Tables[0].Rows.Count > 0)
                {
                    BarProc.Maximum = TabFunc.Tables[0].Rows.Count;
                    BarProc.Value   = 0;

                    for (int I = 0; I <= TabFunc.Tables[0].Rows.Count - 1; I++)
                    {
                        //Gerando o Lanç. de Adiantamento do Salario Codigo 4
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc      = int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc      = 0;
                        MvFolha.IdProvDesc  = 4;
                        MvFolha.Valor       = decimal.Parse(TabFunc.Tables[0].Rows[I]["AdiantSalario"].ToString());
                        MvFolha.QtdeRef     = 1;
                        MvFolha.Descricao   = "";
                        MvFolha.MesAno      = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                    }
                }
                FrmPrincipal.RegistrarAuditoria(this.Text, 0, MvFolha.MesAno, 1, "Gerando Adiantamento Quizenal");
                MessageBox.Show("Processo Concluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BarProc.Visible = false;
                BarProc.Maximum = 0;
                BarProc.Value   = 0;
            }
        }
        private void BtnGerarFP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SetaFunc(0);
                PopularGridEventos();
                DataSet TabMvFunc = new DataSet();
               
                //---Gerando a Folha
                //--Lançamento do Salario
                DataSet TabFunc = new DataSet();
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                    TabFunc = Controle.ConsultaTabela("SELECT Id_Func,SalarioCtps,SalarioAtual FROM Funcionarios WHERE DtDemissao IS NULL AND ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString());
                else
                    TabFunc = Controle.ConsultaTabela("SELECT Id_Func,SalarioCtps,SalarioAtual FROM Funcionarios WHERE DtDemissao IS NULL");

                BarProc.Visible = true;
                if (TabFunc.Tables[0].Rows.Count > 0)
                {
                    BarProc.Maximum = TabFunc.Tables[0].Rows.Count;
                    BarProc.Value   = 0;

                    for (int I = 0; I <= TabFunc.Tables[0].Rows.Count - 1; I++)
                    {
                        //Limpando os Movimentos Fixo e Mensais do Cadastro do Funcionario
                        Controle.ExecutaSQL("DELETE FROM MVFOLHAPAG WHERE MVFOLHAPAG.MESANO='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "' AND (MVFOLHAPAG.ID_PROVDESC IN (SELECT T2.Id_ProvDesc FROM ProvDescFunc T2 WHERE T2.Id_Func=MVFOLHAPAG.Id_Func) OR MVFOLHAPAG.ID_PROVDESC IN (1,2,3,5,6,8)) AND MVFOLHAPAG.Id_Func=" + TabFunc.Tables[0].Rows[I]["Id_Func"].ToString());

                        //Verificando se o Funcionario tem Saldo Negativo Mes Anterior
                        decimal VlrNegativo=0;
                        if (LstMesEventos.SelectedIndex==1)
                           VlrNegativo=VerifSaldoNegativo(int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString()),"12/"+(TxtAnoEventos.Value-1).ToString());
                        else
                           VlrNegativo=VerifSaldoNegativo(int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString()),string.Format("{0:D2}",LstMesEventos.SelectedIndex-1) + @"/" + string.Format("{0:D4}",(int)TxtAnoEventos.Value));

                        if (VlrNegativo < 0)
                        {
                            //Gerando o Lanç. de Saldo Negativo Mes Anterior
                            MvFolha.LerDados(0);
                            MvFolha.IdFunc = int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                            MvFolha.IdLanc = 0;
                            MvFolha.IdProvDesc = 5;
                            MvFolha.Valor = -1*VlrNegativo;
                            MvFolha.QtdeRef = 1;
                            MvFolha.Descricao = "";
                            MvFolha.MesAno = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                            MvFolha.VlrDigitado = MvFolha.Valor;
                            MvFolha.GravarDados();
                        }
                        
                        //Gerando o Lanç. do Salario Codigo 1
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc     = int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc     = 0;
                        MvFolha.IdProvDesc = 1;
                        MvFolha.Valor      = decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioAtual"].ToString());
                        MvFolha.QtdeRef    = 1;
                        MvFolha.Descricao  = "";
                        MvFolha.MesAno     = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();

                        //Gerando o Lanç. do INSS Codigo 2
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc     = int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc     = 0;
                        MvFolha.IdProvDesc = 2;

                        if (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) <= FrmPrincipal.Parametros_Filial.InssFaixa1)
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc1;
                            MvFolha.Valor   = (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) * FrmPrincipal.Parametros_Filial.InssPerc1) / 100;
                        }
                        else if (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) > FrmPrincipal.Parametros_Filial.InssFaixa1 && decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) <= FrmPrincipal.Parametros_Filial.InssFaixa2)
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc2;
                            MvFolha.Valor   = (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) * FrmPrincipal.Parametros_Filial.InssPerc2) / 100;
                        }
                        else if (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) > FrmPrincipal.Parametros_Filial.InssFaixa2 && decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) <= FrmPrincipal.Parametros_Filial.InssFaixa3)
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc3;
                            MvFolha.Valor   = (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) * FrmPrincipal.Parametros_Filial.InssPerc3) / 100;
                        }
                        else
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc3;
                            MvFolha.Valor   = (FrmPrincipal.Parametros_Filial.InssFaixa3 * FrmPrincipal.Parametros_Filial.InssPerc3) / 100;
                        }
                        if (MvFolha.Valor > 0)
                        {
                            if (MvFolha.QtdeRef == 9)
                                MvFolha.Valor = Math.Round(MvFolha.Valor - decimal.Parse("16,50"),2);
                            if (MvFolha.QtdeRef == 12)
                                MvFolha.Valor = Math.Round(MvFolha.Valor - decimal.Parse("82,604"),2);
                            if (MvFolha.QtdeRef == 14)
                                MvFolha.Valor = Math.Round(MvFolha.Valor - decimal.Parse("148,708"),2);
                        }
                        MvFolha.MesAno = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                    }
                }
                //--Lançamento de Comissão
                DataSet TabComissao = new DataSet();
                TabComissao = Controle.ConsultaTabela("SELECT T4.ID_FUNC,T3.VENDEDOR,SUM(ISNULL(T1.VLRUNTCOMISSAO,0)*ISNULL(T1.QTDE,0)) AS TOTAL,SUM(ISNULL(T1.VLRCOMISSAO,0)) AS TOTALCOMISSAO FROM MVVENDAITENS T1" +
                                                      " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                      " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) " +
                                                      " LEFT JOIN Funcionarios T4 ON (T4.ID_VENDEDOR=T2.ID_VENDEDOR) " +
                                                      " WHERE T2.STATUS=3 AND T2.TPVENDA IN ('PV','VF')" +
                                                      " AND MONTH(T2.PREVENTREGA)=" + LstMesEventos.SelectedIndex.ToString() +
                                                      " AND YEAR(T2.PREVENTREGA)=" + TxtAnoEventos.Value.ToString() +
                                                      " AND T4.ID_VENDEDOR > 0" +
                                                      " GROUP BY T4.ID_FUNC,T3.VENDEDOR");

                if (TabComissao.Tables[0].Rows.Count > 0)
                {
                    BarProc.Maximum = TabComissao.Tables[0].Rows.Count;
                    BarProc.Value = 0;

                    for (int I = 0; I <= TabComissao.Tables[0].Rows.Count - 1; I++)
                    {
                        //Gerando o Lanç. de Comissão Codigo 3
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc      = int.Parse(TabComissao.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc      = 0;
                        MvFolha.IdProvDesc  = 3;
                        MvFolha.Valor       = decimal.Parse(TabComissao.Tables[0].Rows[I]["TOTALCOMISSAO"].ToString());
                        MvFolha.QtdeRef     = 1;
                        MvFolha.Descricao   = "";
                        MvFolha.MesAno      = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                    }
                }

                //Lancamentos dos Proventos e Descontos Mensal                
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                {
                    TabMvFunc = Controle.ConsultaTabela("SELECT T1.*,T2.salarioCtps,T2.SalarioAtual FROM ProvDescFunc T1  LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                                                        " WHERE T2.DtDemissao IS NULL AND T1.MESANO='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'" +
                                                        " AND T2.ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString());
                }
                else
                {
                    TabMvFunc = Controle.ConsultaTabela("SELECT T1.*,T2.salarioCtps,T2.SalarioAtual FROM ProvDescFunc T1  LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                                                        " WHERE T2.DtDemissao IS NULL AND T1.MESANO='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'");                                                        
                }

                BarProc.Visible = true;
                if (TabMvFunc.Tables[0].Rows.Count > 0)
                {
                    BarProc.Maximum = TabMvFunc.Tables[0].Rows.Count;
                    BarProc.Value = 0;

                    for (int I = 0; I <= TabMvFunc.Tables[0].Rows.Count - 1; I++)
                    {
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc      = int.Parse(TabMvFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc      = 0;
                        MvFolha.IdProvDesc  = int.Parse(TabMvFunc.Tables[0].Rows[I]["Id_ProvDesc"].ToString());
                        MvFolha.Valor       = decimal.Parse(TabMvFunc.Tables[0].Rows[I]["Valor"].ToString());
                        MvFolha.QtdeRef     = 1;
                        MvFolha.MesAno      = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                        MvFolha.Descricao   = TabMvFunc.Tables[0].Rows[I]["Descricao"].ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                    }                    
                }

                //Lancamentos dos Proventos e Descontos Fixo              
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                {
                    TabMvFunc = Controle.ConsultaTabela("SELECT T1.*,T2.salarioCtps,T2.SalarioAtual FROM ProvDescFunc T1 " +
                                                        " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                                                        " WHERE T2.DtDemissao IS NULL AND T1.MESANO='00/0000' AND T2.ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString());
                }
                else
                {
                    TabMvFunc = Controle.ConsultaTabela("SELECT T1.*,T2.salarioCtps,T2.SalarioAtual FROM ProvDescFunc T1 " +
                                                        " LEFT JOIN Funcionarios T2 ON (T2.Id_Func=T1.Id_Func)" +
                                                        " WHERE T2.DtDemissao IS NULL AND T1.MESANO='00/0000'");
                }
                BarProc.Visible = true;
                if (TabMvFunc.Tables[0].Rows.Count > 0)
                {
                    BarProc.Maximum = TabMvFunc.Tables[0].Rows.Count;
                    BarProc.Value = 0;

                    for (int I = 0; I <= TabMvFunc.Tables[0].Rows.Count - 1; I++)
                    {
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc      = int.Parse(TabMvFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc      = 0;
                        MvFolha.IdProvDesc  = int.Parse(TabMvFunc.Tables[0].Rows[I]["Id_ProvDesc"].ToString());
                        MvFolha.Valor       = decimal.Parse(TabMvFunc.Tables[0].Rows[I]["Valor"].ToString());
                        MvFolha.QtdeRef     = 1;
                        MvFolha.MesAno      = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                        MvFolha.Descricao   = TabMvFunc.Tables[0].Rows[I]["Descricao"].ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                    }
                }

                //Lancamentos Compras dos Funcionarios
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                {
                    TabMvFunc = Controle.ConsultaTabela("select t1.Id_Func,t1.Nome,SUM(t2.VlrBaixa) as Total from funcionarios t1" +
                                                        " left join LancFinanceiro t2 on (t2.Id_Pessoa=t1.ID_Pessoa)" +
                                                        " where MONTH(t2.DataLanc)=" + LstMesEventos.SelectedIndex.ToString() + " and year(t2.DataLanc)=" + TxtAnoEventos.Value.ToString() +
                                                        " and t2.Id_FormaPgto=73 and t2.DtBaixa is not null" +
                                                        " and T1.Demissao=0 AND T1.ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString() +
                                                        " group by t1.Id_Func,t1.Nome");
                }
                else
                {
                    TabMvFunc = Controle.ConsultaTabela("select t1.Id_Func,t1.Nome,SUM(t2.VlrBaixa) as Total from funcionarios t1" +
                                                        " left join LancFinanceiro t2 on (t2.Id_Pessoa=t1.ID_Pessoa)" +
                                                        " where MONTH(t2.DtBaixa) = " + LstMesEventos.SelectedIndex.ToString() + " and year(t2.DtBaixa) = " + TxtAnoEventos.Value.ToString() +
                                                        " and t2.Id_FormaPgto=73 and t2.DtBaixa is not null and T1.DtDemissao IS NULL " +
                                                        " group by t1.Id_Func,t1.Nome");
                }
                BarProc.Visible = true;
                if (TabMvFunc.Tables[0].Rows.Count > 0)
                {
                    BarProc.Maximum = TabMvFunc.Tables[0].Rows.Count;
                    BarProc.Value = 0;

                    for (int I = 0; I <= TabMvFunc.Tables[0].Rows.Count - 1; I++)
                    {
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc      = int.Parse(TabMvFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc      = 0;
                        MvFolha.IdProvDesc  = 8;
                        MvFolha.Valor       = decimal.Parse(TabMvFunc.Tables[0].Rows[I]["Total"].ToString());
                        MvFolha.QtdeRef     = 1;
                        MvFolha.MesAno      = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                        MvFolha.Descricao   = "Compras";
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                    }
                }
                FrmPrincipal.RegistrarAuditoria(this.Text, 0, MvFolha.MesAno, 1, "Gerando a Folha Mensal");
                MessageBox.Show("Processo Concluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BarProc.Visible = false;
                BarProc.Maximum = 0;
                BarProc.Value   = 0;
            }
        }
        private decimal VerifSaldoNegativo(int IdFunc, string MesAno)
        {
            SqlDataReader TabDeb;
            TabDeb = Controle.ConsultaSQL("SELECT ISNULL(SUM(case t4.provdesc when 0 then isnull(T1.Valor,0) end),0)-ISNULL(SUM(case t4.provdesc when 1 then isnull(T1.Valor,0) end),0) as Liquido FROM MvFolhaPag T1"+
                                          " LEFT JOIN ProventosDescontos T4 ON (T4.Id_Codigo=T1.Id_ProvDesc)"+
                                          " WHERE T1.MesAno='"+MesAno+"' AND T1.ID_FUNC="+IdFunc.ToString());
            if (TabDeb.HasRows)
            {
                TabDeb.Read();
                if (decimal.Parse(TabDeb["Liquido"].ToString()) < 0)
                    return decimal.Parse(TabDeb["Liquido"].ToString());
                else
                    return 0;
            }
            else
                return 0;            
        }
        //Eventos Proventos e Descontos
        private void PopularGridEventos()
        {
            TabEventos = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.PROVDESC,T1.ID_PROVDESC,T1.QTDE_REF,T1.VALOR,T1.DESCRICAO FROM MVFOLHAPAG T1 " +
                                                 " LEFT JOIN ProventosDescontos T2 ON (T2.Id_Codigo=T1.ID_PROVDESC)" +
                                                 " WHERE T1.ID_FUNC=" + TxtCodFunc.Text + " AND T1.MESANO='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "'");
                                                 
            Source_Eventos.DataSource = TabEventos;
            Source_Eventos.DataMember = TabEventos.Tables[0].TableName;
            GridEventos.DataSource    = Source_Eventos;
            Navegador.BindingSource   = Source_Eventos;
            Totalizar();
            int item = Source_Eventos.Find("Id_Lanc", MvFolha.IdLanc);
            Source_Eventos.Position = item;
            

        }
        private void Totalizar()
        {
            decimal TotProv = 0;
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
            LblSaldo.Text     = string.Format("{0:N2}", TotProv - TotDesc);            
        }

        private void GridEventos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridEventos.CurrentRow == null || GridEventos.Rows.Count - 1 == GridEventos.CurrentRow.Index)
                {
                    IncluirItem();
                }
            }
        }
        private void GridEventos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {   
            if (int.Parse(TxtCodFunc.Text) == 0)
            {
                Source_Eventos.CancelEdit();
                e.Cancel = true;
            }

        }
        private void GridEventos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (int.Parse(TxtCodFunc.Text) > 0)
            {
                decimal Qtde = 0;
                if (GridEventos.CurrentRow.Cells[3].Value.ToString() != "")
                    Qtde = decimal.Parse(GridEventos.CurrentRow.Cells[3].Value.ToString());

                decimal Valor = decimal.Parse(GridEventos.CurrentRow.Cells[4].Value.ToString());

                MvFolha.LerDados(int.Parse(GridEventos.CurrentRow.Cells[0].Value.ToString()));

                if (MvFolha.IdProvDesc > 0)
                {
                    if (MvFolha.IdProvDesc != int.Parse(GridEventos.CurrentRow.Cells[2].Value.ToString()) || MvFolha.Valor != Valor)
                    {
                        if (MessageBox.Show("Confirma Atualização ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            GridEventos.CurrentCell = GridEventos.CurrentRow.Cells[e.ColumnIndex];
                            PopularGridEventos();
                            return;
                        }
                    }
                }
                MvFolha.Valor         = Valor;
                MvFolha.QtdeRef       = Qtde;                
                MvFolha.IdProvDesc    = int.Parse(GridEventos.CurrentRow.Cells[2].Value.ToString());
                MvFolha.Descricao     = GridEventos.CurrentRow.Cells[5].Value.ToString();
                MvFolha.VlrDigitado   = Valor; ;

                if (MvFolha.IdProvDesc == 6 && Qtde > 0)
                {
                    decimal VlrHr = 0;
                    if (CadFunc.SalBaseHR == 0)
                        VlrHr = (CadFunc.SalarioCtps / 220) * decimal.Parse("1,7");
                    else
                        VlrHr = (CadFunc.SalarioAtual / 220) * decimal.Parse("1,7");

                    MvFolha.Valor = VlrHr * Qtde;
                }
                MvFolha.GravarDados();
                //Verificando se o Lançamento é Hora Extra das Lojas
                if (MvFolha.IdProvDesc == 6 && CadFunc.IdFilialTrab != 1 && CadFunc.IdFilialTrab != 6)
                {
                    bool Premiacao = false;
                    for (int I = 0; I <= GridEventos.RowCount - 1; I++)
                    {
                        if (int.Parse(GridEventos.Rows[I].Cells[2].Value.ToString()) == 7)
                            Premiacao = true;
                    }

                    if (Premiacao)
                    {
                        Controle.ExecutaSQL("Update MvFolhaPag set Valor=VlrDigitado-" + Controle.FloatToStr(MvFolha.Valor, 2) + " WHERE ID_FUNC=" + MvFolha.IdFunc.ToString() + " AND MESANO='" + MvFolha.MesAno.ToString() + "' AND ID_PROVDESC=7");
                        Controle.ExecutaSQL("Update MvFolhaPag set Valor=VlrDigitado WHERE ID_FUNC=" + MvFolha.IdFunc.ToString() + " AND MESANO='" + MvFolha.MesAno.ToString() + "' AND ID_PROVDESC=3");
                    }
                    else
                    {
                        Controle.ExecutaSQL("Update MvFolhaPag set Valor=VlrDigitado-" + Controle.FloatToStr(MvFolha.Valor, 2) + " WHERE ID_FUNC=" + MvFolha.IdFunc.ToString() + " AND MESANO='" + MvFolha.MesAno.ToString() + "' AND ID_PROVDESC=3");
                        Controle.ExecutaSQL("Update MvFolhaPag set Valor=VlrDigitado WHERE ID_FUNC=" + MvFolha.IdFunc.ToString() + " AND MESANO='" + MvFolha.MesAno.ToString() + "' AND ID_PROVDESC=7");
                    }
                }
                FrmPrincipal.RegistrarAuditoria(this.Text, MvFolha.IdFunc, MvFolha.MesAno, 2, "Alterando o Evento: " + MvFolha.IdProvDesc.ToString());
                PopularGridEventos();
                GridEventos.CurrentCell = GridEventos.CurrentRow.Cells[e.ColumnIndex];
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (int.Parse(TxtCodFunc.Text) == 0)
                MessageBox.Show("Favor Selecionar um Funcionario", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                IncluirItem();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (int.Parse(TxtCodFunc.Text) == 0)
                MessageBox.Show("Favor Selecionar um Funcionario", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                    MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {   
                        MvFolha.LerDados(int.Parse(GridEventos.CurrentRow.Cells[0].Value.ToString()));
                        FrmPrincipal.RegistrarAuditoria(this.Text, MvFolha.IdFunc, MvFolha.MesAno, 3, "Excluindo o Evento: " + MvFolha.IdProvDesc.ToString());
                        MvFolha.Excluir();
                        MvFolha.IdLanc = 0;
                        PopularGridEventos();
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (int.Parse(TxtCodFunc.Text) == 0)
                MessageBox.Show("Favor Selecionar um Funcionario", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {

                if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                    MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MvFolha.LerDados(0);
                    MvFolha.IdFunc        = int.Parse(TxtCodFunc.Text);
                    MvFolha.IdLanc        = 0;                    
                    MvFolha.IdProvDesc    = 0;                    
                    MvFolha.Valor         = 0;
                    MvFolha.VlrDigitado   = 0;
                    MvFolha.QtdeRef       = 1;
                    MvFolha.MesAno        = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                    MvFolha.Descricao     = "";
                    MvFolha.GravarDados();
                    PopularGridEventos();
                    GridEventos.CurrentCell = GridEventos.CurrentRow.Cells[2];
                    FrmPrincipal.RegistrarAuditoria(this.Text, MvFolha.IdFunc, MvFolha.MesAno, 1, "Incluindo o Evento: " + MvFolha.IdProvDesc.ToString());
                }
            }
        }

        private void Btn13Sal_Click(object sender, EventArgs e)
        {
            if (LstMesEventos.SelectedIndex != 13)
            {
                MessageBox.Show("Selecione o Mês do 13º Salario", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Confirma Gerar a Folha do 13º Salário?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SetaFunc(0);
                PopularGridEventos();
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                {
                    Controle.ExecutaSQL("DELETE FROM MVFOLHAPAG WHERE MESANO=" + @"'13/" + TxtAnoEventos.Value.ToString() + "' AND ID_PROVDESC in (9,2) " +
                                        " AND ID_FUNC IN (SELECT ID_FUNC FROM FUNCIONARIOS WHERE ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString() + ")");
                }
                else
                {
                    Controle.ExecutaSQL("DELETE FROM MVFOLHAPAG WHERE MESANO=" + @"'13/" + TxtAnoEventos.Value.ToString() + "' AND ID_PROVDESC in (9,2)");
                }

                //---Gerando a Folha
                //--13º Salario
                DataSet TabFunc = new DataSet();
                if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                    TabFunc = Controle.ConsultaTabela("SELECT * FROM Funcionarios WHERE Demissao=0 AND ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString());
                else
                    TabFunc = Controle.ConsultaTabela("SELECT * FROM Funcionarios WHERE Demissao=0 ");

                BarProc.Visible = true;
                if (TabFunc.Tables[0].Rows.Count > 0)
                {
                    BarProc.Maximum = TabFunc.Tables[0].Rows.Count;
                    BarProc.Value = 0;
                    DateTime DtAdm = DateTime.Now;
                    decimal Salario = 0;
                    decimal SalarioCtps = 0;

                    for (int I = 0; I <= TabFunc.Tables[0].Rows.Count - 1; I++)
                    {
                        DtAdm = DateTime.Parse(TabFunc.Tables[0].Rows[I]["DtAdmissao"].ToString());

                        //Gerando o Lanç. de Adiantamento do Salario Codigo 4
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc     = int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc     = 0;
                        MvFolha.IdProvDesc = 9;
                        MvFolha.QtdeRef    = 12;
                        if (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioAtual"].ToString()) == 0)
                            MvFolha.Valor = decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString());
                        else
                            MvFolha.Valor = decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioAtual"].ToString());
                        SalarioCtps   = decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString());

                        if (DtAdm.Year.ToString() == TxtAnoEventos.Value.ToString())
                        {
                            if (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioAtual"].ToString()) == 0)
                                Salario = decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) / 12;
                            else
                                Salario = decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioAtual"].ToString()) / 12;
                            
                            int Mes = 13 - DtAdm.Month;

                            if (DtAdm.Day > 15)
                                Mes = Mes - 1;

                            MvFolha.Valor   = Salario * Mes;
                            SalarioCtps     = (decimal.Parse(TabFunc.Tables[0].Rows[I]["SalarioCtps"].ToString()) / 12) * Mes;
                            MvFolha.QtdeRef = Mes;

                        }

                        if (MvFolha.Valor > 0)
                        {
                            if (MvFolha.QtdeRef == 9)
                                MvFolha.Valor = Math.Round(MvFolha.Valor - decimal.Parse("16,50"), 2);
                            if (MvFolha.QtdeRef == 12)
                                MvFolha.Valor = Math.Round(MvFolha.Valor - decimal.Parse("82,604"), 2);
                            if (MvFolha.QtdeRef == 14)
                                MvFolha.Valor = Math.Round(MvFolha.Valor - decimal.Parse("148,708"), 2);
                        }

                        MvFolha.Descricao = "13 Salario";
                        MvFolha.MesAno =  @"13/" + TxtAnoEventos.Value.ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();

                        //Lançando o INSS
                        //Gerando o Lanç. do INSS Codigo 2
                        MvFolha.LerDados(0);
                        MvFolha.IdFunc = int.Parse(TabFunc.Tables[0].Rows[I]["Id_Func"].ToString());
                        MvFolha.IdLanc = 0;
                        MvFolha.IdProvDesc = 2;

                        if (SalarioCtps <= FrmPrincipal.Parametros_Filial.InssFaixa1)
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc1;
                            MvFolha.Valor = (SalarioCtps * FrmPrincipal.Parametros_Filial.InssPerc1) / 100;
                        }
                        else if (SalarioCtps > FrmPrincipal.Parametros_Filial.InssFaixa1 && SalarioCtps <= FrmPrincipal.Parametros_Filial.InssFaixa2)
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc2;
                            MvFolha.Valor   = (SalarioCtps * FrmPrincipal.Parametros_Filial.InssPerc2) / 100;
                        }
                        else if (SalarioCtps > FrmPrincipal.Parametros_Filial.InssFaixa2 && SalarioCtps <= FrmPrincipal.Parametros_Filial.InssFaixa3)
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc3;
                            MvFolha.Valor = (SalarioCtps * FrmPrincipal.Parametros_Filial.InssPerc3) / 100;
                        }
                        else
                        {
                            MvFolha.QtdeRef = FrmPrincipal.Parametros_Filial.InssPerc3;
                            MvFolha.Valor = (FrmPrincipal.Parametros_Filial.InssFaixa3 * FrmPrincipal.Parametros_Filial.InssPerc3) / 100;
                        }
                        MvFolha.MesAno = @"13/" + TxtAnoEventos.Value.ToString();
                        MvFolha.VlrDigitado = MvFolha.Valor;
                        MvFolha.GravarDados();

                        BarProc.Value = BarProc.Value + 1;
                    }
                }
                MessageBox.Show("Processo Concluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BarProc.Visible = false;
                BarProc.Maximum = 0;
                BarProc.Value = 0;
            }
        }

       
    }
}
