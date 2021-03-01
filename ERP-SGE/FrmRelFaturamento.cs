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
    public partial class FrmRelFaturamento : Form
    {
        Funcoes Controle = new Funcoes();
        FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
        public TelaPrincipal FrmPrincipal;

        public FrmRelFaturamento()
        {
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao         = FrmPrincipal.Conexao;
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            Op01.Checked             = true;
            Dt1.Value                = DateTime.Now.AddDays(-1);
            Dt2.Value                = DateTime.Now;            
            Rb_Emitida.Checked       = true;
            Rb_Form.Checked          = true;
            CamposLista();

        }
        private void CamposLista()
        {
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,60) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "Todas");                        
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void AtualizarTela(object sender, EventArgs e)
        {
            BoxStatus.Visible   = Op01.Checked;
            BoxTipoNota.Visible = Op01.Checked;
            BoxFilial.Visible   = Op01.Checked;
            PnlPessoa.Visible   = Op03.Checked;
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            BtnImprimir.Enabled = false;            
            if (Op01.Checked) 
            {
                string sSql = "SELECT T2.RazaoSocial, T1.DtEmissao, T1.NumNota, T1.NumFormulario, T1.VlrNota, T1.BIcms, T1.VlrIcms, T1.BIcmsSub, T1.VlrIcmsSub, T1.VlrIpi, T1.VlrFrete, " +
                              " T1.VlrSeguro, T1.VlrOutraDesp, T3.CFOP, T3.Descricao,T4.FANTASIA AS Filial" +
                              " FROM  NotaFiscal AS T1 " +
                              " LEFT OUTER JOIN Pessoas AS T2 ON T2.Id_Pessoa = T1.Id_Pessoa " +
                              " LEFT OUTER JOIN CFOP AS T3 ON T3.Id_CFOP = T1.Id_Cfop" +
                              " LEFT OUTER JOIN Empresa_Filial AS T4 ON T4.Id_Filial = T1.Id_Filial" +
                              " WHERE T1.DtEmissao >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DtEmissao <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (Rb_Emitida.Checked)
                    sSql = sSql + " AND T1.Status = 1 ";
                if (Rb_Cancelada.Checked)
                    sSql = sSql + " AND T1.Status = 2 ";

                if (Rb_Form.Checked)
                    sSql = sSql + " AND T1.NFE = 0 ";
                else
                    sSql = sSql + " AND T1.NFE = 1 ";

                if (LstFilial.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_Filial=" + LstFilial.SelectedValue.ToString();
                sSql = sSql + " ORDER BY T1.ID_FILIAL,T1.DTEMISSAO,T1.NumFormulario";
                                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelNotaFiscais Rel001 = new Relatorios.RelNotaFiscais();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op02.Checked) // Cupom Fiscal
            {
                string sSql = "SELECT CASE T1.STATUS WHEN 2 THEN 'CANCELADO' ELSE ' ' END AS STATUS,T1.DATA,T1.NUM_CF,T1.VLRSUBTOTAL,T1.VLRDESCONTO,T1.VLRTOTAL,T2.NUMDOCUMENTO,T2.PESSOA FROM CUPOMFISCAL T1"+
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)"+
                              " WHERE T1.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ORDER BY T1.DATA";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelCupomFiscais Rel001 = new Relatorios.RelCupomFiscais();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op03.Checked)
            {
                if (int.Parse(TxtCodCliente.Text) == 0)
                {
                    MessageBox.Show("Favor selecionar um clinete", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string sSql = "SELECT T2.Cnpj+' '+T2.RazaoSocial as RazaoSocial,T1.DtEmissao, T1.NumNota, T1.NumFormulario, T1.VlrNota, T1.BIcms, T1.VlrIcms, T1.BIcmsSub, T1.VlrIcmsSub, T1.VlrIpi, T1.VlrFrete, " +
                              " T1.VlrSeguro, T1.VlrOutraDesp, T3.CFOP, T3.Descricao,T4.FANTASIA AS Filial" +
                              " FROM  NotaFiscal AS T1 " +
                              " LEFT OUTER JOIN Pessoas AS T2 ON T2.Id_Pessoa = T1.Id_Pessoa " +
                              " LEFT OUTER JOIN CFOP AS T3 ON T3.Id_CFOP = T1.Id_Cfop" +
                              " LEFT OUTER JOIN Empresa_Filial AS T4 ON T4.Id_Filial = T1.Id_Filial" +
                              " WHERE T1.Status = 1 and T1.DtEmissao >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DtEmissao <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                                    
                
                sSql = sSql + " AND T1.Id_Pessoa="+TxtCodCliente.Text;
                sSql = sSql + " ORDER BY T1.ID_FILIAL,T2.RazaoSocial,T1.DTEMISSAO,T1.NumFormulario";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelNotaFiscais Rel001 = new Relatorios.RelNotaFiscais();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op04.Checked) // Vendas com pendencia de Cupom Fiscal ou Nota
            {
                string sSql = "select t2.Data,t2.Pessoa,t2.NumDocumento,t3.Referencia,t3.Descricao,t1.Qtde,t1.VlrUnitario,t1.VlrTotal,t2.VlrDesconto+t2.Credito AS Desconto,t2.VlrTotal as TotalVenda from MvVendaItens t1" +
                              " left join MvVenda t2 on (t2.Id_Venda=t1.Id_Venda)" +
                              " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                              " WHERE T2.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              " and t2.TpVenda='PV' and t2.ID_LancCF=0 AND t2.ImpNF=0" +
                              " order by t2.data,t2.NumDocumento";
                              

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelVdSemCFNF Rel001 = new Relatorios.RelVdSemCFNF();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            BtnImprimir.Enabled = true;
        }

        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            BuscaPessoa.ShowDialog();
            if (BuscaPessoa.CadPessoa.IdPessoa > 0)
            {
                TxtCodCliente.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                TxtCliente.Text = BuscaPessoa.CadPessoa.RazaoSocial;
            }
            else
            {
                TxtCodCliente.Text = "0";
                TxtCliente.Text = " ";
            }
        }
    }
}
