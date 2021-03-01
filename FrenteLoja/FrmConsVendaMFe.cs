using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP_SGE;
using Controle_Dados;
using Controles;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Collections;
using System.Runtime.InteropServices;

namespace FrenteLoja
{
    public partial class FrmConsVendaMFe : Form
    {
        Funcoes Controle = new Funcoes();
        public FrmFrenteLoja FrmFrenteLoja;

        public int IdCaixa = 0;
        public FrmConsVendaMFe()
        {
            InitializeComponent();
        }

        private void FrmConsVendaMFe_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmFrenteLoja.FrmPrincipal.Conexao;
            LstPesquisa.SelectedIndex = 0;
            PopuparGrid();
        }

        private void PopuparGrid()
        {
            string sSQL = "SELECT T1.ID_VENDA,CASE T1.STATUS WHEN 3 THEN 'Concluida' WHEN 4 THEN 'Cancelado' END AS STATUS," +
                         " T1.PESSOA,T1.VLRTOTAL,T3.ChaveNFe,T3.NumNota,T3.Id_Nota,T5.DOCUMENTO,T4.IdRespMFe,T4.TpPag,T3.NFE,T3.Status as STANFE FROM MVVENDA T1 " +
                         " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) " +
                         " LEFT JOIN NotaFiscal T3 ON (T3.ID_Venda=T1.ID_Venda) " +
                         " LEFT JOIN PagamentoMFE T4 ON (T4.Id_Venda=T1.Id_VENDA) " +
                         " LEFT JOIN TipoDocumento t5 on(t5.Id_Documento=t4.Id_Documento)";
                         
            if (LstPesquisa.SelectedIndex == 1)
                sSQL = sSQL + " WHERE T4.IDRespMFE=0 and T5.MFE in (3,4) and T3.ChaveNFE<>'' and T3.NFE=3 ORDER BY T1.ID_VENDA DESC";
            else if (LstPesquisa.SelectedIndex == 2)
                sSQL = sSQL + " WHERE T1.ID_CAIXA = " + IdCaixa.ToString()+" T1.Stastus=3 > 0 ORDER BY T1.ID_VENDA DESC";
            else
                sSQL = sSQL + " WHERE T1.ID_CAIXA=" + IdCaixa.ToString() + " ORDER BY T1.ID_VENDA DESC";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(sSQL);
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[1].Value.ToString() == "Cancelado")
                {
                    MessageBox.Show("Venda Cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (GridDados.CurrentRow.Cells[10].Value.ToString() == "2")
                {
                    if (GridDados.CurrentRow.Cells[11].Value.ToString() != "1")
                    {
                        MessageBox.Show("Venda não gerou Cupom Fiscal", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmFrenteLoja.ImprimirNFce(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                }
                else if (GridDados.CurrentRow.Cells[10].Value.ToString() == "3")
                {
                    if (GridDados.CurrentRow.Cells[11].Value.ToString() != "1")
                    {
                        MessageBox.Show("Venda não gerou Cupom Fiscal", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmFrenteLoja.ImprimirCFe(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                }
                else
                {
                    if (FrmFrenteLoja.FrmPrincipal.TipoImpResumida == "ELGIN_I9")
                    {
                        PrintDialog Imp = new PrintDialog();
                        Imp.PrinterSettings = new PrinterSettings();
                        if (DialogResult.OK == Imp.ShowDialog(this))
                        {
                            Controles.ImpElginI9 imp = new Controles.ImpElginI9();
                            imp.Controle.Conexao = FrmFrenteLoja.FrmPrincipal.Conexao;
                            imp.ImprimirNaoFiscal(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()), Imp.PrinterSettings.PrinterName);
                        }
                    }
                    else
                        FrmFrenteLoja.ImpMiniImpBematech(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                }
                /*Controle_Dados.MvVenda Venda = new MvVenda();
                Venda.Controle = Controle;

                string FormaPgto = "";
                DataSet Parcelas = new DataSet();
                Parcelas = Controle.ConsultaTabela("SELECT T1.VENCIMENTO,T1.VLRORIGINAL,T2.DOCUMENTO FROM LancFinanceiro T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE T1.Id_Venda=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                for (int I = 0; I <= Parcelas.Tables[0].Rows.Count - 1; I++)
                {
                    DateTime Dt = DateTime.Parse(Parcelas.Tables[0].Rows[I]["Vencimento"].ToString());
                    FormaPgto = FormaPgto + Dt.Date.ToShortDateString() + "   R$" + string.Format("{0:N2}", decimal.Parse(Parcelas.Tables[0].Rows[I]["VlrOriginal"].ToString())) + "   " + Parcelas.Tables[0].Rows[I]["Documento"].ToString();
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                ERP_SGE.Relatorios.RelVendas RelVenda = new ERP_SGE.Relatorios.RelVendas();                                
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(Venda.SqlRelatorio(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString())));
                RelVenda.SetDataSource(TabRel.Tables[0]);                
                FrmRel.cryRepRelatorio.ReportSource = RelVenda;
                RelVenda.Section1.Height = 180;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section2.ReportObjects["LblFilial"])).Text    = FrmFrenteLoja.FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section5.ReportObjects["LblRodaPe"])).Text    = FrmFrenteLoja.FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section5.ReportObjects["LblFormaPgto"])).Text = FormaPgto;
                FrmRel.ShowDialog();*/
            }
        }

        private void BtnDetalhes_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                FrmDetVenda Frm = new FrmDetVenda();
                Frm.FrmFrenteLoja = FrmFrenteLoja;
                Frm.IdVenda = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Frm.ShowDialog();
            }
        }

        private void BtnCancCFe_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[1].Value.ToString() == "Cancelado")
                {
                    MessageBox.Show("Venda já Cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (GridDados.CurrentRow.Cells[4].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("Venda não gerou Cupom Fiscal Eletrônico", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Confirma o Cancelamento da Venda ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    EnviarCancelamento();
                    PopuparGrid();
                }

            }
        }

        private void EnviarCancelamento()
        {
            if (GridDados.CurrentRow != null)
            {
                ArrayList XmlCanc = new ArrayList();
                XmlCanc.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                XmlCanc.Add("<CFeCanc>");
                XmlCanc.Add("<infCFe chCanc=\"" + GridDados.CurrentRow.Cells[4].Value.ToString().Trim() + "\">");
                XmlCanc.Add("<ide>");
                XmlCanc.Add("<CNPJ>11831930000103</CNPJ>");
                XmlCanc.Add("<signAC>" + FrmFrenteLoja.FrmPrincipal.Parametros_Filial.ChaveMFe.ToString() + "</signAC>");
                XmlCanc.Add("<numeroCaixa>001</numeroCaixa>");
                XmlCanc.Add("</ide>");
                XmlCanc.Add("<emit/>");
                XmlCanc.Add("<dest/>");
                XmlCanc.Add("<total/>");
                XmlCanc.Add("<infAdic/>");
                XmlCanc.Add("</infCFe>");
                XmlCanc.Add("</CFeCanc>");

                string Xml = "";
                for (int i = 0; i <= XmlCanc.Count - 1; i++)
                    Xml = Xml + XmlCanc[i].ToString();

                int NrSessao = Controle.ProximoID("SessaoMFE");

                NFce ImpMFe = new NFce();
                ImpMFe.Controle = Controle;
                ImpMFe.Inicializar_parametros(FrmFrenteLoja.FrmPrincipal.Perfil_Usuario.IdFilial);

                string retorno = Marshal.PtrToStringAnsi(CFeSatDll.CancelarUltimaVenda(NrSessao, FrmFrenteLoja.FrmPrincipal.Parametros_Filial.CodigoMFe.ToString(), GridDados.CurrentRow.Cells[4].Value.ToString().Trim(), Xml.ToString()));
                string[] ParamRet = ImpMFe.GravarXmlRetCancMFE(int.Parse(GridDados.CurrentRow.Cells[5].Value.ToString()), retorno);

                if (ParamRet[1].ToString() == "07000")
                {
                    Controle.ExecutaSQL("Update MvVenda set Id_LancCF=0 Where Id_Venda=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                    Controle.ExecutaSQL("UPDATE NotaFiscal Set Status=2,DataCancel=convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) Where Id_Nota=" + GridDados.CurrentRow.Cells[6].Value.ToString());
                    MessageBox.Show("Cupom Fiscal Cancelado", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Erro ao Cancelar o Cupom Fiscal, Motivo:" + ParamRet[3].ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void LstPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopuparGrid();
        }

        private void BtnVerCartao_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {

                if (GridDados.CurrentRow.Cells[1].Value.ToString() == "Cancelado")
                {
                    MessageBox.Show("Venda já Cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (GridDados.CurrentRow.Cells[4].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("Venda não gerou Cupom Fiscal", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (GridDados.CurrentRow.Cells[9].Value.ToString().Trim() == "3" || GridDados.CurrentRow.Cells[9].Value.ToString().Trim() == "4")
                {

                    DataTable PagCartao = new DataTable();
                    PagCartao = new DataTable();
                    PagCartao.Columns.Add("IdLanc", Type.GetType("System.Int32"));
                    PagCartao.Columns.Add("ID_Documento", Type.GetType("System.Int32"));
                    PagCartao.Columns.Add("Documento", Type.GetType("System.String"));
                    PagCartao.Columns.Add("Adquirente", Type.GetType("System.String"));
                    PagCartao.Columns.Add("ID_Nota", Type.GetType("System.Int32"));
                    PagCartao.Columns.Add("VlrDoc", Type.GetType("System.Decimal"));
                    PagCartao.Columns.Add("IdPagMFe", Type.GetType("System.Int32"));
                    PagCartao.Columns.Add("IdRespMFe", Type.GetType("System.Int32"));
                    PagCartao.Columns.Add("ChaveCFe", Type.GetType("System.String"));
                    PagCartao.Columns.Add("MFE", Type.GetType("System.Int32"));
                    PagCartao.Columns.Add("NParc", Type.GetType("System.Int32"));


                    SqlDataReader TabFinanc = Controle.ConsultaSQL("select t2.Id_Documento,t2.Documento,t2.Adquirente,t1.id_nota,t1.Valor,T2.MFe,t1.IdPagMFe,t1.IdRespMFe,t3.ChaveNFe,T1.NParc, t1.Id_Lanc from PagamentoMFE t1" +
                                                                   " left join TipoDocumento t2 on(t2.Id_Documento = t1.Id_Documento)" +
                                                                   " left join NotaFiscal t3 on (t3.Id_Nota = t1.Id_Nota)" +
                                                                   " where isnull(t1.IdRespMFe, '0') = '0' and t1.id_venda =" + GridDados.CurrentRow.Cells[0].Value.ToString());

                    while (TabFinanc.Read())
                    {
                        PagCartao.Rows.Add(int.Parse(TabFinanc["Id_Lanc"].ToString()), int.Parse(TabFinanc["ID_Documento"].ToString()), TabFinanc["Documento"].ToString(), TabFinanc["Adquirente"].ToString(), int.Parse(TabFinanc["Id_Nota"].ToString()), TabFinanc["Valor"].ToString(), int.Parse(TabFinanc["IdPagMFe"].ToString()), int.Parse(TabFinanc["IDRespMFe"].ToString()), TabFinanc["ChaveNFe"].ToString(), TabFinanc["MFE"].ToString(), int.Parse(TabFinanc["NParc"].ToString()));
                    }

                    FrmFrenteLoja.VerificarCartao(PagCartao);
                    PopuparGrid();
                }
                else
                {
                    MessageBox.Show("Venda não foi paga em Cartão", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
    }
}
