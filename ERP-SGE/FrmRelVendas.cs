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
    public partial class FrmRelVendas : Form
    {
        Funcoes Controle = new Funcoes();
        FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
        FrmBuscaProduto BuscaPrd   = new FrmBuscaProduto(); 
        public TelaPrincipal FrmPrincipal;
        public FrmRelVendas()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao = FrmPrincipal.Conexao;
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
            Op01.Checked             = true;
            Dt1.Value                = DateTime.Now;
            Dt2.Value                = DateTime.Now;
            Rb_Entregue.Checked      = true;
            Rb_PorQtde.Checked       = true;
            Rb_OEP_Faturar.Checked   = true;
            Rb_Sintetico.Checked     = true;
            Rb_AgVisConc.Checked     = true;            
            CamposLista();
            PnlOpcoes.Height = 280;            
            this.Height      = 620;
            BtnImprimir.Location = new  System.Drawing.Point(450,400);
            TxtAno.Value = DateTime.Now.Year;
        }
        private void CamposLista()
        {
            LstGrupo      = FrmPrincipal.PopularCombo("SELECT Id_Grupo,Grupo FROM GrupoProduto ORDER BY Grupo", LstGrupo,"Todos");
            LstCaixa      = FrmPrincipal.PopularCombo("SELECT T1.ID_CAIXA,T2.USUARIO FROM CAIXABALCAO T1 LEFT JOIN USUARIOS T2 ON (T2.ID_USUARIO=T1.ID_USUARIO) WHERE T1.DATA = Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103)", LstCaixa);
            LstEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstEntregador, "Todas");
            LstEntrega    = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstEntrega, "Todas");
            LstTipoMov    = FrmPrincipal.PopularCombo("select Chave,Descricao from TabelasAux where Campo='VENDA' order by Descricao", LstTipoMov, "Todos");
            LstEquipe     = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,Vendedor FROM Vendedores where cotafinanceira=1 ORDER BY Vendedor", LstEquipe, "Todas");
            //LstVendedor   = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", LstVendedor, "Todos");
            
            //if (FrmPrincipal.Perfil_Usuario.IdVendedor > 0)
            //    LstVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();            
            //LstVendedor.Enabled = FrmPrincipal.Perfil_Usuario.SeusMov != 1;
            
            if (FrmPrincipal.Perfil_Usuario.SeusMov == 1)
            {
                CkListVendedor = FrmPrincipal.PopularCheckList("SELECT Id_Vendedor,SubString(Vendedor,1,30) as Vendedor FROM Vendedores Where Id_Vendedor=" + FrmPrincipal.Perfil_Usuario.IdVendedor.ToString(), CkListVendedor, "", "");
                if (CkListVendedor.Items.Count > 0)
                    CkListVendedor.SetItemChecked(0, true);
                CkListVendedor.Enabled = false;
                LstEquipe.Enabled      = false;

            }
            else
                CkListVendedor = FrmPrincipal.PopularCheckList("SELECT Id_Vendedor,SubString(Vendedor,1,30) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", CkListVendedor, "", "");
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
            BoxAnaliticoSint.Visible    = Op03.Checked || Op14.Checked;
            PnlOp01.Visible             = Op01.Checked || Op30.Checked; ;
            PnlPessoa.Visible           = Op05.Checked || Op07.Checked || Op11.Checked || Op10.Checked || Op13.Checked || Op16.Checked || Op18.Checked || Op20.Checked || Op21.Checked || Op15.Checked; ;
            BoxVlrQtd.Visible           = Op05.Checked || Op06.Checked;
            PnlPrd.Visible              = Op06.Checked || Op12.Checked || Op13.Checked || Op14.Checked || Op17.Checked || Op18.Checked || Op19.Checked || Op20.Checked || Op27.Checked || Op29.Checked;
            Dt2.Visible                 = !Op08.Checked && !Op09.Checked;
            BoxOE.Visible               = Op11.Checked;
            label16.Visible             = !Op08.Checked && !Op09.Checked;
            PnlUsuCx.Visible            = Op08.Checked || Op09.Checked;            
            TxtCliente.Enabled          = Op10.Checked || Op15.Checked;
            Ck_AgruparResultado.Visible = Op04.Checked || Op05.Checked || Op06.Checked || Op18.Checked;
            label5.Visible              = !Op08.Checked && !Op09.Checked && !Op19.Checked && !Op26.Checked && !Op30.Checked; 
            CkListVendedor.Visible      = !Op08.Checked && !Op09.Checked && !Op19.Checked && !Op26.Checked && !Op30.Checked;
            PnlPeriodo.Visible           = !Op16.Checked && !Op10.Checked; ;
            PnlAno.Visible              = Op16.Checked;
            Ck_TodasFiliais.Visible     = Op19.Checked;
            BoxAgendaVista.Visible      = Op22.Checked;
            LblBairro.Visible           = Op10.Checked || Op15.Checked;
            TxtBairro.Visible           = Op10.Checked || Op15.Checked;
            LstEquipe.Visible           = !Op08.Checked && !Op09.Checked && !Op19.Checked && !Op26.Checked && !Op30.Checked; 
            LblEquipe.Visible           = !Op08.Checked && !Op09.Checked && !Op19.Checked && !Op26.Checked && !Op30.Checked; 
          
            Dt1.Enabled                 = true;
            
            if (Op14.Checked)
            {
                Dt1.Value=DateTime.Parse("01/01/2014");
                Dt1.Enabled = false;
            }

            if (Op15.Checked || Op17.Checked)
                label6.Text = "Data de Cadastro:";
            else
                label6.Text = "   Período de: ";
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            string NmVendedor    = "TODOS";
            string CodVendedor   = "0";
            string LstCodPessoas = "";
            for (int I = 0; I <= CkListVendedor.Items.Count - 1; I++)
            {
                if (CkListVendedor.GetItemChecked(I))
                {
                    DataRowView item = (DataRowView)CkListVendedor.Items[I];
                    if (CodVendedor == "0")
                        CodVendedor = item.Row[0].ToString();
                    else
                        CodVendedor = CodVendedor + "," + item.Row[0].ToString();

                    if (NmVendedor == "TODOS")
                        NmVendedor = item.Row[1].ToString().Trim();
                    else
                        NmVendedor = NmVendedor + "/" + item.Row[1].ToString().Trim();
                }
            }

            if (int.Parse(TxtCodCliente.Text) > 0)
            {
                if (BuscaPessoa.CadPessoa.Clie_Forn == 4)
                {
                    LstCodPessoas="0";
                    SqlDataReader TabCli = Controle.ConsultaSQL("SELECT * FROM PESSOAS WHERE ID_VINCULO=" + TxtCodCliente.Text);
                    while (TabCli.Read())
                        LstCodPessoas = LstCodPessoas + "," + TabCli["ID_PESSOA"].ToString();                    
                }
                else
                    LstCodPessoas = TxtCodCliente.Text;

            }

            if (int.Parse(LstEquipe.SelectedValue.ToString()) > 0)
            {
                CodVendedor = "0";
                SqlDataReader TabCli = Controle.ConsultaSQL("SELECT * FROM Vendedores WHERE Id_VendGrupo=" + LstEquipe.SelectedValue.ToString());
                while (TabCli.Read())
                    CodVendedor = CodVendedor + "," + TabCli["ID_Vendedor"].ToString();

            }

            BtnImprimir.Enabled = false;            
            if (Op01.Checked) // Prestação de Conta das Entrega
            {
                string sSql = "SELECT  T6.DESCRICAO,T1.Id_Venda, T1.NumDocumento, T1.FormNF, T2.RazaoSocial, RTRIM(T1.Endereco) + ',' + RTRIM(T1.Numero) + ' ' + RTRIM(T1.Complemento) AS ENDERECO, " +
                              "  T1.Fone, T1.Cep, T1.Bairro, T1.Cidade, T3.Entregador, T4.Vendedor, T5.FormaPgto, T1.Id_VdMaster,T1.VLRTOTAL,T1.DATACONFIRMACAO,T1.PREVENTREGA,T9.FANTASIA AS LOCALENTREGA " +
                              " FROM  MvVenda AS T1 " +
                              "  LEFT OUTER JOIN Pessoas AS T2 ON T2.Id_Pessoa = T1.Id_Pessoa "+
                              "  LEFT OUTER JOIN Entregadores AS T3 ON T3.Id_Entregador = T1.Id_Entregador " +
                              "  LEFT OUTER JOIN Vendedores AS T4 ON T4.Id_Vendedor = T1.Id_Vendedor " +
                              "  LEFT OUTER JOIN FormaPagamento AS T5 ON T5.Id_FormaPgto = T1.Id_FormaPgto" +
                              "  LEFT OUTER JOIN TABELASAUX AS T6 ON T6.CAMPO='VENDA' AND T6.CHAVE=T1.TPVENDA" +
                              "  LEFT OUTER JOIN EMPRESA_FILIAL AS T9 ON (T9.Id_Filial = T1.Id_FilialEntrega)"; 
                sSql = sSql + " WHERE t1.TPVENDA<>'PI' AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (Rb_Aberto.Checked)
                    sSql = sSql + " AND T1.Id_Entregador > 0 and T1.Status < 3 ";

                if (LstEntregador.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_Entregador=" + LstEntregador.SelectedValue.ToString();

                if (CodVendedor != "0")                    
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";      
                //else
                //    sSql = sSql + " AND T4.EntraRel=1";
                if (LstEntrega.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_FilialEntrega=" + LstEntrega.SelectedValue.ToString();

                if (Rb_Entregue.Checked)
                    sSql = sSql + " AND T1.Status=3";

                if (LstTipoMov.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.TpVenda='" + LstTipoMov.SelectedValue.ToString().Trim()+"'";

                if (Rb_SemEntregador.Checked)
                    sSql = sSql + " AND ((T1.Id_Entregador=0 and T1.Status=2 and T1.TPVENDA<>'VF') or ( T1.Id_Entregador=0 and T1.Status=1 and T1.TPVENDA not in ('PV','VF')))";
                sSql = sSql + " ORDER BY T3.Entregador,T1.preventrega,t6.descricao";     
                           
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelConfEntrega Rel001 = new Relatorios.RelConfEntrega();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Previsão de Entrega:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + "  Filial Entrega: " + LstEntrega.Text.ToString().Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op02.Checked) // Vendas por Periodo
            {
                string sSql = "SELECT T1.ID_VENDA,T1.IMPNF,T1.FORMNF,T1.NUMDOCUMENTO,T1.VLRSUBTOTAL,T1.VLRDESCONTO,T1.VLRTOTAL,T1.PESSOA," +
                              "T1.STATUS,T1.PREVENTREGA,T1.DATACONFIRMACAO,T2.ENTREGADOR,T3.VENDEDOR,T1.DATA FROM MVVENDA T1" +
                              " LEFT JOIN ENTREGADORES T2 ON (T2.ID_ENTREGADOR=T1.ID_ENTREGADOR)" +
                              " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)";
                sSql = sSql + " WHERE T1.STATUS in (1,2,3) AND T1.TPVENDA in ('PV','VF') AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";      

                sSql = sSql + " ORDER BY T1.DATA,T1.PESSOA";
                FrmRelatorios FrmRel = new FrmRelatorios();
                if (FrmPrincipal.VersaoDistribuidor)
                {
                    FrmRel.cryRepRelatorio.ShowExportButton = true;
                    FrmRel.cryRepRelatorio.ShowPrintButton = true;
                }
                else
                {
                    FrmRel.cryRepRelatorio.ShowExportButton = false;
                    FrmRel.cryRepRelatorio.ShowPrintButton = false;
                }
                FrmRel.cryRepRelatorio.ShowExportButton = true;
                FrmRel.cryRepRelatorio.ShowPrintButton = true;

                Relatorios.RelVendasPeriodo Rel001 = new Relatorios.RelVendasPeriodo();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text  = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text  = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op03.Checked) // Comissão
            {
                string sSql = "SELECT T5.DESCRICAO AS MOVIMENTO,T4.VENDEDOR,T3.RAZAOSOCIAL,T2.DATA,T2.PREVENTREGA,T2.DATACONFIRMACAO,T2.ID_VENDA,T2.NUMDOCUMENTO,T2.FORMNF,T2.VLRSUBTOTAL,T2.VLRDESCONTO,T2.CREDITO,T2.VLRTOTAL,SUM(T1.VLRCOMISSAO) AS COMISSAO FROM MVVENDAITENS T1" +
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T2.ID_PESSOA)" +
                              " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " LEFT JOIN TABELASAUX T5 ON (T5.CAMPO='VENDA' AND T5.CHAVE=T2.TPVENDA)" +
                              " WHERE T2.STATUS=3 AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              "   AND T5.COMISSAO=1";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T4.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";      

                sSql = sSql + " GROUP BY T5.DESCRICAO,T4.VENDEDOR,T3.RAZAOSOCIAL,T2.DATA,T2.PREVENTREGA,T2.DATACONFIRMACAO,T2.ID_VENDA,T2.NUMDOCUMENTO,T2.FORMNF,T2.VLRSUBTOTAL,T2.VLRDESCONTO,T2.CREDITO,T2.VLRTOTAL ORDER BY T4.VENDEDOR,T2.PREVENTREGA";
                //Select do Perc comissao
                string sSql2 = "select t1.Id_Venda,IsNull(t1.P_Comissao,0) as comissao ,Isnull(sum(t1.VlrUntReal*t1.Qtde),0) as Total " +
                               "  from MvVendaItens t1 left join mvvenda t2 on (t2.id_venda=t1.id_venda)"+
                               "  LEFT JOIN TABELASAUX T3 ON (T3.CAMPO='VENDA' AND T3.CHAVE=T2.TPVENDA)" +
                               "  LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                               " WHERE STATUS=3 AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                               "   AND T3.COMISSAO=1";

                if (CodVendedor == "0")
                    sSql2 = sSql2 + " AND T4.EntraRel=1";
                else
                    sSql2 = sSql2 + " AND T2.Id_Vendedor in (" + CodVendedor + ")";      

                sSql2 = sSql2 + "group by t1.id_venda,t1.P_Comissao ORDER BY T1.ID_VENDA";                                
                if (Rb_Sintetico.Checked)
                {
                    FrmRelatorios FrmRel = new FrmRelatorios();
                    if (FrmPrincipal.VersaoDistribuidor)
                    {
                        FrmRel.cryRepRelatorio.ShowExportButton = true;
                        FrmRel.cryRepRelatorio.ShowPrintButton = true;
                    }
                    else
                    {
                        FrmRel.cryRepRelatorio.ShowExportButton = false;
                        FrmRel.cryRepRelatorio.ShowPrintButton = false;
                    }
                    FrmRel.cryRepRelatorio.ShowExportButton = true;
                    FrmRel.cryRepRelatorio.ShowPrintButton = true;

                    Relatorios.RelComissao01 Rel001 = new Relatorios.RelComissao01();
                    DataSet TabRel = new DataSet(); 
                    DataSet TabPercCom = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql);                    
                    Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    //((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                }
                else
                {
                    FrmRelatorios FrmRel = new FrmRelatorios();
                    Relatorios.RelComissao02 Rel001 = new Relatorios.RelComissao02();
                    if (FrmPrincipal.VersaoDistribuidor)
                    {
                        FrmRel.cryRepRelatorio.ShowExportButton = true;
                        FrmRel.cryRepRelatorio.ShowPrintButton = true;
                    }
                    else
                    {
                        FrmRel.cryRepRelatorio.ShowExportButton = false;
                        FrmRel.cryRepRelatorio.ShowPrintButton = false;
                    }
                    DataSet TabRel = new DataSet();
                    DataSet TabPercCom = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql);
                    TabPercCom = Controle.ConsultaTabela(sSql2);
                    Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                    Rel001.Database.Tables[1].SetDataSource(TabPercCom.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    //((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                }                
            }

            if (Op04.Checked) // Curva ABC Vendedor X Cliente
            {
                string sSql ="";
                if (Ck_AgruparResultado.Checked)
                    sSql = "SELECT '" + NmVendedor + "' AS VENDEDOR,T1.ID_PESSOA,T2.RAZAOSOCIAL,SUM(VLRTOTAL) AS TOTAL FROM MVVENDA T1 ";
                else
                    sSql = "SELECT T1.ID_VENDEDOR,T3.VENDEDOR,T1.ID_PESSOA,T2.RAZAOSOCIAL,SUM(VLRTOTAL) AS TOTAL FROM MVVENDA T1 ";
                sSql = sSql + " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) " +
                              " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                              " WHERE T1.STATUS=3 AND T1.TPVENDA in ('PV','VF') AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";


                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                if (Ck_AgruparResultado.Checked)
                    sSql = sSql + " GROUP BY T1.ID_PESSOA,T2.RAZAOSOCIAL ORDER BY 4 DESC";
                else
                    sSql = sSql + " GROUP BY T1.ID_VENDEDOR,T3.VENDEDOR,T1.ID_PESSOA,T2.RAZAOSOCIAL ORDER BY T3.VENDEDOR,5 DESC";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelVendedorXCliente Rel001 = new Relatorios.RelVendedorXCliente();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op05.Checked) // Curva ABC Vendedor X Cliente X Produto
            {   
                string sSql1 ="";
                if (Ck_AgruparResultado.Checked)
                    sSql1 = "SELECT 0 AS ID_VENDEDOR,'" + NmVendedor + "' AS VENDEDOR,T1.ID_PESSOA,T2.RAZAOSOCIAL,SUM(VLRTOTAL) AS TOTAL FROM MVVENDA T1 ";
                else
                    sSql1 = "SELECT T1.ID_VENDEDOR,T3.VENDEDOR,T1.ID_PESSOA,T2.RAZAOSOCIAL,SUM(VLRTOTAL) AS TOTAL FROM MVVENDA T1 ";
                sSql1 = sSql1 + " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) " +
                                " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR) " +                                
                                " WHERE T1.STATUS=3 AND T1.TPVENDA in ('PV','VF') AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (CodVendedor == "0")
                    sSql1 = sSql1 + " AND T3.EntraRel=1";
                else
                    sSql1 = sSql1 + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql1 = sSql1 + " AND T1.Id_Pessoa IN (" + LstCodPessoas + ")";

                if (Ck_AgruparResultado.Checked)
                    sSql1 = sSql1 + " GROUP BY T1.ID_PESSOA,T2.RAZAOSOCIAL ORDER BY 5 DESC";
                else
                    sSql1 = sSql1 + " GROUP BY T1.ID_VENDEDOR,T3.VENDEDOR,T1.ID_PESSOA,T2.RAZAOSOCIAL ORDER BY T3.VENDEDOR,5 DESC";                

                string sSql2 = "";
                if (Ck_AgruparResultado.Checked)
                    sSql2 = "SELECT 0 AS ID_VENDEDOR,'" + NmVendedor + "' AS VENDEDOR,T2.ID_PESSOA,T3.REFERENCIA,T3.DESCRICAO,SUM(T1.QTDE) AS QTDE ,SUM(T1.VLRUNTREAL*T1.QTDE) AS TOTALPRD ";
                else
                    sSql2 = "SELECT T2.ID_VENDEDOR,T2.ID_PESSOA,T3.REFERENCIA,T3.DESCRICAO,SUM(T1.QTDE) AS QTDE ,SUM(T1.VLRUNTREAL*T1.QTDE) AS TOTALPRD ";
                sSql2 = sSql2 + " FROM MVVENDAITENS T1 " +
                                " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) " +
                                " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) " +
                                " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T2.ID_VENDEDOR) " +
                                " WHERE T1.TIPOITEM IN ('S','N') " +
                                " AND T2.STATUS=3 AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql2 = sSql2 + " AND T2.Id_Pessoa IN (" + LstCodPessoas + ")";

                if (CodVendedor == "0")
                    sSql2 = sSql2 + " AND T4.EntraRel=1";
                else
                    sSql2 = sSql2 + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (Ck_AgruparResultado.Checked)
                    sSql2 = sSql2 + " GROUP BY T2.ID_PESSOA,T3.REFERENCIA,T3.DESCRICAO";
                else
                    sSql2 = sSql2 + " GROUP BY T2.ID_VENDEDOR,T2.ID_PESSOA,T3.REFERENCIA,T3.DESCRICAO";

                if (Rb_PorQtde.Checked)
                {
                    if (Ck_AgruparResultado.Checked)
                        sSql2 = sSql2 + " ORDER BY QTDE DESC";
                    else
                        sSql2 = sSql2 + " ORDER BY T2.ID_VENDEDOR,QTDE DESC";
                }
                else
                {
                    if (Ck_AgruparResultado.Checked)
                        sSql2 = sSql2 + " ORDER BY TOTALPRD DESC";
                    else
                        sSql2 = sSql2 + " ORDER BY T2.ID_VENDEDOR,TOTALPRD DESC";
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelABCVendxClieXPrd Rel001 = new Relatorios.RelABCVendxClieXPrd();
                DataSet TabRel = new DataSet();
                DataSet TabPrd = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql1);
                TabPrd = Controle.ConsultaTabela(sSql2);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                Rel001.Database.Tables[1].SetDataSource(TabPrd.Tables[0]);                
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op06.Checked) // Vendas Produto e Grupo
            {
                string sSql = "";
                if (Ck_AgruparResultado.Checked)
                {
                    sSql = "SELECT '" + NmVendedor + "' AS VENDEDOR,T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO,SUM(T1.QTDE) AS QTDETOTAL,SUM(T1.VLRUNTREAL*T1.QTDE) AS VALORTOTAL FROM MVVENDAITENS T1 ";
                }
                else
                    sSql = "SELECT T5.VENDEDOR,T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO,SUM(T1.QTDE) AS QTDETOTAL,SUM(T1.VLRUNTREAL*T1.QTDE) AS VALORTOTAL FROM MVVENDAITENS T1 ";

                sSql = sSql + " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              " LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO)" +
                              " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " WHERE T1.TIPOITEM IN ('S','N') AND T2.STATUS=3 AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";                
                    
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T3.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();
                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                if (Ck_AgruparResultado.Checked)
                    sSql = sSql + " GROUP BY T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO ";
                else
                    sSql = sSql + " GROUP BY T5.VENDEDOR,T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO ";

                /*sSql = sSql + " UNION ALL ";

                if (Ck_AgruparResultado.Checked)
                    sSql = sSql + "SELECT '" + NmVendedor + "' AS VENDEDOR,T4.GRUPO,T6.REFERENCIA,T6.DESCRICAO,SUM(T1.QTDE*T3.QTDE) AS QTDETOTAL,SUM(T6.PRCMINIMO*(T1.QTDE*T3.QTDE)) AS VALORTOTAL FROM MVVENDAITENS T1 ";
                else
                    sSql = sSql + "SELECT T5.VENDEDOR,T4.GRUPO,T6.REFERENCIA,T6.DESCRICAO,SUM(T1.QTDE*T3.QTDE) AS QTDETOTAL,SUM(T6.PRCMINIMO*(T1.QTDE*T3.QTDE)) AS VALORTOTAL FROM MVVENDAITENS T1 ";

                sSql = sSql + " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) " +
                              " LEFT JOIN PRODUTOS PRD ON (PRD.ID_PRODUTO=T1.ID_PRODUTO) "+
                              " LEFT JOIN PRODUTOSKIT T3 ON (T3.ID_PRDMASTER=PRD.ID_PRODUTO) "+
                              " LEFT JOIN PRODUTOS T6 ON (T6.ID_PRODUTO=T3.ID_PRODUTO) "+   
                              " LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T6.ID_GRUPO) "+
                              " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR) "+
                              " WHERE T1.TIPOITEM IN ('S','N') AND T2.STATUS=3 AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")"; 

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T6.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();
                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T6.Id_Produto=" + TxtCodPrd.Text;
                sSql = sSql + " GROUP BY T5.VENDEDOR,T4.GRUPO,T6.REFERENCIA,T6.DESCRICAO ";*/

                if (Rb_PorQtde.Checked)
                {
                    if (Ck_AgruparResultado.Checked)
                        sSql = sSql + " ORDER BY T4.GRUPO,QTDETOTAL DESC";
                    else
                        sSql = sSql + " ORDER BY T5.VENDEDOR,T4.GRUPO,QTDETOTAL DESC";
                }
                else
                {
                    if (Ck_AgruparResultado.Checked)
                        sSql = sSql + " ORDER BY T4.GRUPO,VALORTOTAL DESC";
                    else
                        sSql = sSql + " ORDER BY T5.VENDEDOR,T4.GRUPO,VALORTOTAL DESC";
                }
                //Atualizando a Tabela
                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("VENDEDOR", Type.GetType("System.String"));
                Tab01.Columns.Add("GRUPO", Type.GetType("System.String"));
                Tab01.Columns.Add("REFERENCIA", Type.GetType("System.String"));
                Tab01.Columns.Add("DESCRICAO", Type.GetType("System.String"));
                Tab01.Columns.Add("QTDETOTAL", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("VALORTOTAL", Type.GetType("System.Decimal"));                
                //
                SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);                                  
                bool FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;
                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {
                        if (Tab01.Rows[I]["Vendedor"].ToString() == LerSQL["Vendedor"].ToString() && Tab01.Rows[I]["GRUPO"].ToString() == LerSQL["GRUPO"].ToString() && Tab01.Rows[I]["Referencia"].ToString() == LerSQL["Referencia"].ToString())
                        {
                            FindLinha = true;
                            Tab01.Rows[I]["QTDETOTAL"] = decimal.Parse(Tab01.Rows[I]["QTDETOTAL"].ToString()) + decimal.Parse(LerSQL["QtdeTotal"].ToString());
                            Tab01.Rows[I]["VALORTOTAL"] = decimal.Parse(Tab01.Rows[I]["VALORTOTAL"].ToString()) + decimal.Parse(LerSQL["VALORTotal"].ToString());
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        Tab01.Rows.Add(LerSQL["Vendedor"].ToString(), LerSQL["GRUPO"].ToString(), LerSQL["Referencia"].ToString(), LerSQL["Descricao"].ToString(), decimal.Parse(LerSQL["QtdeTotal"].ToString()), decimal.Parse(LerSQL["ValorTotal"].ToString()));
                    }
                }

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelVendaPrdGrupo Rel001 = new Relatorios.RelVendaPrdGrupo();
                DataSet TabRel = new DataSet();                
                TabRel = Controle.ConsultaTabela(sSql);                
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op07.Checked) // Mapa de Comodatos
            {
                string sSql = "SELECT T4.RAZAOSOCIAL,T2.ID_VENDA,T2.NUMDOCUMENTO,T5.VENDEDOR,T3.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T2.DATA FROM MVVENDAITENS T1" +
                              "  LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              "  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              "  LEFT JOIN PESSOAS T4 ON (T4.ID_PESSOA=T2.ID_PESSOA)" +
                              "  LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " WHERE T2.STATUS=3 AND T2.TPVENDA='CO' AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (CodVendedor != "0")
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql = sSql + " AND T2.Id_Pessoa IN (" + LstCodPessoas + ")";

                sSql = sSql + " ORDER BY T4.RAZAOSOCIAL,T1.ID_VENDA";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelComodato Rel001 = new Relatorios.RelComodato();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op08.Checked) // Movimentação de Vendas do Caixa
            {                
                Controles.Verificar VerifCx = new Controles.Verificar();
                VerifCx.Controle = Controle;
                int IdCaixa = VerifCx.Verificar_ExisteCadastro("Id_Caixa", "SELECT ID_CAIXA FROM CAIXABALCAO WHERE Convert(Char,Data,103)='" + Dt1.Value.ToShortDateString() + "' AND ID_USUARIO="+FrmPrincipal.Perfil_Usuario.IdUsuario.ToString());
                 
                string sSql = "SELECT T1.PREVENTREGA,T6.DOCUMENTO,T5.VENDEDOR,T1.ID_VENDA,T1.NUMDOCUMENTO,T2.RAZAOSOCIAL,T1.VLRTOTAL,T3.VLRORIGINAL,T3.VENCIMENTO,T4.DESCRICAO,T1.FORMNF,T7.AGENTE,T8.ENTREGADOR FROM MVVENDA T1" +
                              " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                              " JOIN LANCFINANCEIRO T3 ON (T3.ID_VENDA=T1.ID_VENDA)" +
                              " LEFT JOIN TABELASAUX T4 ON (T4.CHAVE=T1.TPVENDA)" +
                              " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " LEFT JOIN TIPODOCUMENTO T6 ON (T6.ID_DOCUMENTO=T3.ID_TIPODOCUMENTO)"+
                              " LEFT JOIN AGENTECOBRADOR T7 ON (T7.ID_AGENTE=T3.ID_AGENTE)" +
                              " LEFT JOIN ENTREGADORES T8 ON (T8.ID_ENTREGADOR=T1.ID_ENTREGADOR)" +
                              " WHERE T1.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() + " AND T1.TPVENDA IN ('PV','TROCA','VF') AND T1.VLRTOTAL > 0 AND T1.STATUS=3";
                
                //" WHERE T1.STATUS IN (2,3) AND ;
                //if (CodVendedor != "0")           
                //    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " ORDER BY T1.PREVENTREGA,T6.DOCUMENTO,T5.VENDEDOR,T1.ID_VENDA";
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelCxMovVenda Rel001 = new Relatorios.RelCxMovVenda();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Caixa: " + LstCaixa.Text + "      Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op09.Checked) // Resumo do Caixa Balcão
            {
                Controles.Verificar VerifCx = new Controles.Verificar();
                VerifCx.Controle = Controle;
                int IdCaixa = VerifCx.Verificar_ExisteCadastro("Id_Caixa", "SELECT ID_CAIXA FROM CAIXABALCAO WHERE Convert(Char,Data,103)='" + Dt1.Value.ToShortDateString() + "' AND ID_USUARIO="+FrmPrincipal.Perfil_Usuario.IdUsuario.ToString());

                string sSql = "SELECT T1.TIPO,'Despesa' AS TIPOLANC,CASE T1.STATUS WHEN 1 THEN 'Confirmado' WHEN 2 THEN 'Cancelado' ELSE ' ' END AS STATUS," +
                              " T1.DESCRICAO,-1*T1.VALOR as VALOR,T2.DOCUMENTO,T3.VLRINICIAL FROM MVCAIXABALCAO T1" +
                              " LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_DOCUMENTO)" +
                              " LEFT JOIN CAIXABALCAO T3 ON (T3.ID_CAIXA=T1.ID_CAIXA)" +
                              "  WHERE T1.STATUS<>2 AND T1.TIPO=0 AND T1.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() +
                              " UNION" +
                              " SELECT T1.TIPO,'Receita' AS TIPOLANC,CASE T1.STATUS WHEN 1 THEN 'Confirmado' WHEN 2 THEN 'Cancelado' ELSE ' ' END AS STATUS," +
                              " T1.DESCRICAO,T1.VALOR,T2.DOCUMENTO,T3.VLRINICIAL FROM MVCAIXABALCAO T1" +
                              " LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_DOCUMENTO)" +
                              " LEFT JOIN CAIXABALCAO T3 ON (T3.ID_CAIXA=T1.ID_CAIXA)" +
                              " WHERE T1.STATUS<>2 AND T1.TIPO=1 AND T1.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() +
                              " UNION" +
                              " SELECT 2 AS TIPO,'Movimentação de Vendas do Caixa' AS TIPOLANC,'Confirmado' as STATUS,'Total do Movimento',SUM(T2.VLRORIGINAL) AS VALOR,'Total Movimento' as Documento,0" +
                              " FROM MVVENDA T1" +
                              " JOIN LANCFINANCEIRO T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              " LEFT JOIN TIPODOCUMENTO T3 ON (T3.ID_DOCUMENTO=T2.ID_TIPODOCUMENTO) " +
                              " WHERE T1.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() + " AND T1.TPVENDA IN ('PV','TROCA','VF') AND T1.VLRTOTAL > 0 AND T3.RESUMOCX=1 AND T1.STATUS=3 " +
                              " ORDER BY 1";

                /*string sSql2 = "SELECT T4.ID_CAIXA,T5.USUARIO,T3.Documento,SUM(t2.vlroriginal) AS TOTAL FROM MvVenda T1 " +
                               " LEFT JOIN LancFinanceiro T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                               " LEFT JOIN TipoDocumento T3 ON (T3.Id_Documento=T2.Id_TipoDocumento)  " +
                               " LEFT JOIN CaixaBalcao T4 ON (T4.ID_CAIXA=" + LstCaixa.SelectedValue.ToString() + ")" +
                               " LEFT JOIN Usuarios T5 ON (T5.Id_Usuario=T4.Id_Usuario) " +
                               " WHERE T1.Status=3  AND T1.Id_Caixa=T4.Id_Caixa" +
                               " group by T4.ID_CAIXA,T5.USUARIO,T3.Documento" +
                               " ORDER BY 1,2,3";*/

                string sSql2 = "SELECT T1.Id_Caixa,T2.Documento,T1.VlrCalculado,T1.VlrReceita,T1.VlrDespesa,T1.VlrInformado,T1.ResumoCx FROM FechamentoCxBalcao T1" +
                              " LEFT JOIN TipoDocumento T2 ON (T2.Id_Documento=T1.Id_Documento) WHERE T1.Id_Caixa=" + LstCaixa.SelectedValue.ToString();

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoCaixa Rel001 = new Relatorios.RelResumoCaixa();
                DataSet TabRel = new DataSet();
                DataSet TabResFin = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                TabResFin = Controle.ConsultaTabela(sSql2);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                Rel001.Database.Tables[1].SetDataSource(TabResFin.Tables[0]);                
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text ="Caixa: " + LstCaixa.Text + "       Período de Venda:" + Dt1.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op10.Checked) // Clientes Inativos
            {
                string sSql = "SELECT T3.VENDEDOR,T1.ID_PESSOA,T1.RAZAOSOCIAL,T1.FANTASIA,T1.CNPJ,T1.INSC_UF,T1.CEP,T1.ENDERECO,T1.NUMERO,T1.COMPLEMENTO," +
                              " T1.BAIRRO,T1.CIDADE,T2.SIGLA,T1.FONE,T1.CELULAR,T1.CONTATO,T1.ATIVO,(SELECT TOP 1 DATA FROM MVVENDA V1 WHERE V1.ID_PESSOA=T1.ID_PESSOA AND V1.STATUS=3 AND V1.TPVENDA='PV'" +
                              " ORDER BY DATA DESC) AS ULTCOMPRA,T1.DATACADASTRO  FROM PESSOAS T1" +
                              " LEFT JOIN ESTADOS T2 ON (T2.ID_UF=T1.ID_UF)" +
                              " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " WHERE NOT EXISTS (SELECT * FROM MVVENDA T4  WHERE T4.ID_PESSOA=T1.ID_PESSOA AND T4.STATUS=3 AND T4.TPVENDA='PV' AND T4.PREVENTREGA >= (GetDate() - 90) ) " +
                              "  AND EXISTS (SELECT * FROM MVVENDA T4 WHERE T4.ID_PESSOA=T1.ID_PESSOA AND T4.STATUS=3 AND T4.TPVENDA='PV' AND T4.PREVENTREGA >= (GetDate() - 365) )";
                              //" WHERE NOT EXISTS (SELECT * FROM MVVENDA T4 WHERE T4.ID_PESSOA=T1.ID_PESSOA AND T4.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T4.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) )";
                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                if (TxtBairro.Text.Trim()!="")
                    sSql = sSql + " AND T1.BAIRRO LIKE '%" + TxtBairro.Text.Trim() + "%'";

                if (TxtCliente.Text.Trim() != "")
                    sSql = sSql + " AND T1.RAZAOSOCIAL LIKE '%" + TxtCliente.Text.Trim() + "%'";

                sSql = sSql + " ORDER BY T1.RAZAOSOCIAL";
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelClienteInativo Rel001 = new Relatorios.RelClienteInativo();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Data que Antecede:" + Dt1.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op11.Checked) // Movimentação das Ordens de entrega
            {
                if (Rb_OEP_Faturar.Checked)
                {
                    string sSql = "SELECT T4.VENDEDOR,T2.RAZAOSOCIAL,T1.ID_VENDA,T1.NUMDOCUMENTO,T1.DATA,T1.PREVENTREGA,T1.VLRTOTAL,T3.ENTREGADOR,T1.VINCULOVD FROM MVVENDA T1 " +
                                  " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                  " LEFT JOIN ENTREGADORES T3 ON (T3.ID_ENTREGADOR=T1.ID_ENTREGADOR)" +
                                  " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                                  " WHERE T1.TPVENDA='OE' AND T1.FATURADO=0 AND T1.STATUS in (3)" +
                                  " AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                    if (CodVendedor == "0")
                        sSql = sSql + " AND T4.EntraRel=1";
                    else
                        sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                    if (int.Parse(TxtCodCliente.Text) > 0)
                        sSql = sSql + " AND T1.Id_Pessoa IN (" + LstCodPessoas + ")";
                    sSql = sSql + " ORDER BY T4.VENDEDOR,T2.RAZAOSOCIAL";
                    FrmRelatorios FrmRel = new FrmRelatorios();
                    Relatorios.RelOE_A_Faturar Rel001 = new Relatorios.RelOE_A_Faturar();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql);
                    Rel001.SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Entrega:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                }
                else if (Rb_Faturadas.Checked)
                {
                    string sSql = "SELECT T5.ID_VENDA AS NUMVD,T5.NUMDOCUMENTO AS DOCVD,T5.DATA AS DTVENDA,T5.PREVENTREGA AS DTENTREGA,CASE T5.STATUS WHEN 2 THEN 'FATURADO' WHEN 3 THEN 'ENTREGUE' END AS STATUS,T4.VENDEDOR,T2.RAZAOSOCIAL,T1.ID_VENDA,T1.NUMDOCUMENTO,T1.DATA,T1.PREVENTREGA,T1.VLRTOTAL,T3.ENTREGADOR,T1.VINCULOVD FROM MVVENDA T1 "+
                                  " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)"+
                                  " LEFT JOIN ENTREGADORES T3 ON (T3.ID_ENTREGADOR=T1.ID_ENTREGADOR)"+
                                  " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR)"+
                                  " JOIN MVVENDA T5 ON (T5.ID_VENDA=T1.VINCULOVD)  "+
                                  " WHERE T1.TPVENDA='OE'  AND T1.FATURADO=1  AND T1.STATUS=3  AND T5.STATUS IN (2,3)"+
                                  " AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                    if (CodVendedor == "0")
                        sSql = sSql + " AND T4.EntraRel=1";
                    else
                        sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                    if (int.Parse(TxtCodCliente.Text) > 0)
                        sSql = sSql + " AND T1.Id_Pessoa IN (" + LstCodPessoas + ")";

                    sSql = sSql + " ORDER BY T4.VENDEDOR,T1.VINCULOVD";
                    FrmRelatorios FrmRel = new FrmRelatorios();
                    Relatorios.RelOE_Faturada Rel001 = new Relatorios.RelOE_Faturada();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql);
                    Rel001.SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Entrega:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                }
            }
            if (Op12.Checked) // Vendas por Produto 
            {
                string sSql = "SELECT T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO,T2.STATUS,T3.PONTOS,SUM(T1.QTDE) AS QTDETOTAL,isnull(SUM(T1.VLRUNTREAL*T1.QTDE),0) AS VALORTOTAL FROM MVVENDAITENS T1 " +
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              " LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO)" +                              
                              " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " WHERE T1.TIPOITEM IN ('S','N') AND T2.STATUS IN (2,3)  AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";                
                              //" WHERE T3.PRODUTOKIT=0 AND T1.TIPOITEM IN ('S','N') AND T2.STATUS IN (2,3)  AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";                
                              
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T3.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                sSql = sSql + " GROUP BY T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO,T2.STATUS,T3.PONTOS ";
                /*sSql = sSql + " UNION ALL ";
                sSql = sSql + "SELECT T4.GRUPO,T6.REFERENCIA,T6.DESCRICAO,T2.STATUS,SUM(T1.QTDE*T3.QTDE) AS QTDETOTAL,isnull(SUM(T6.PRCMINIMO*(T1.QTDE*T3.QTDE)),0) AS VALORTOTAL FROM MVVENDAITENS T1 " +
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) " +
                              " LEFT JOIN PRODUTOS PRD ON (PRD.ID_PRODUTO=T1.ID_PRODUTO) " +
                              " LEFT JOIN PRODUTOSKIT T3 ON (T3.ID_PRDMASTER=PRD.ID_PRODUTO) " +
                              " LEFT JOIN PRODUTOS T6 ON (T6.ID_PRODUTO=T3.ID_PRODUTO) " +
                              " LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T6.ID_GRUPO) " +
                              " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " WHERE PRD.PRODUTOKIT=1 AND T1.TIPOITEM IN ('S','N') AND T2.STATUS IN (2,3) AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";                
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T6.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T6.Id_Produto=" + TxtCodPrd.Text;
                sSql = sSql + " GROUP BY T4.GRUPO,T6.REFERENCIA,T6.DESCRICAO,T2.STATUS ";*/

                sSql = sSql + " ORDER BY T4.GRUPO,T3.DESCRICAO";                

                //Atualizando a Tabela
                DataTable Tab01 = new DataTable();                
                Tab01.Columns.Add("GRUPO",        Type.GetType("System.String"));
                Tab01.Columns.Add("REFERENCIA",   Type.GetType("System.String"));
                Tab01.Columns.Add("DESCRICAO",    Type.GetType("System.String"));
                Tab01.Columns.Add("QTDETOTAL",    Type.GetType("System.Decimal"));
                Tab01.Columns.Add("VALORTOTAL",   Type.GetType("System.Decimal"));
                Tab01.Columns.Add("QTDEFATURADA", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("PONTOS",       Type.GetType("System.Int32"));
                //
                SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);
                bool FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;
                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {
                        if (Tab01.Rows[I]["GRUPO"].ToString() == LerSQL["GRUPO"].ToString() && Tab01.Rows[I]["Referencia"].ToString() == LerSQL["Referencia"].ToString())
                        {
                            FindLinha = true;
                            if (int.Parse(LerSQL["Status"].ToString()) == 2)
                                Tab01.Rows[I]["QTDEFATURADA"] = decimal.Parse(Tab01.Rows[I]["QTDEFATURADA"].ToString()) + decimal.Parse(LerSQL["QtdeTotal"].ToString());
                            else
                                Tab01.Rows[I]["QTDETOTAL"] = decimal.Parse(Tab01.Rows[I]["QTDETOTAL"].ToString()) + decimal.Parse(LerSQL["QtdeTotal"].ToString());

                            Tab01.Rows[I]["VALORTOTAL"] = decimal.Parse(Tab01.Rows[I]["VALORTOTAL"].ToString()) + decimal.Parse(LerSQL["VALORTotal"].ToString());
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        if (int.Parse(LerSQL["Status"].ToString()) == 2)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["Referencia"].ToString(), LerSQL["Descricao"].ToString(), 0, decimal.Parse(LerSQL["ValorTotal"].ToString()), decimal.Parse(LerSQL["QtdeTotal"].ToString()), int.Parse(LerSQL["Pontos"].ToString()));
                        else
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["Referencia"].ToString(), LerSQL["Descricao"].ToString(), decimal.Parse(LerSQL["QtdeTotal"].ToString()), decimal.Parse(LerSQL["ValorTotal"].ToString()), 0, int.Parse(LerSQL["Pontos"].ToString()));
                    }
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelVendaProdutos Rel001 = new Relatorios.RelVendaProdutos();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                //Rel001.SetDataSource(TabRel.Tables[0]);
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op13.Checked) // Vendas de Produto por Cliente
            {
                string sSql = "SELECT T5.RAZAOSOCIAL,T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO,T2.STATUS,SUM(T1.QTDE) AS QTDETOTAL,isnull(SUM(T1.VLRUNTREAL*T1.QTDE),0) AS VALORTOTAL FROM MVVENDAITENS T1 " +
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              " LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO)" +
                              " LEFT JOIN PESSOAS T5 ON (T5.ID_PESSOA=T2.ID_PESSOA)" +
                              " LEFT JOIN VENDEDORES T6 ON (T6.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " WHERE T1.TIPOITEM IN ('S','N') AND T2.STATUS IN (2,3)  AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                              //" WHERE T3.PRODUTOKIT=0 AND T1.TIPOITEM IN ('S','N') AND T2.STATUS IN (2,3)  AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T3.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();
                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;
                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql = sSql + " AND T2.Id_Pessoa IN (" + LstCodPessoas + ")";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T6.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " GROUP BY T5.RAZAOSOCIAL,T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO,T2.STATUS ";
                /*sSql = sSql + " UNION ALL ";
                sSql = sSql + "SELECT T5.RAZAOSOCIAL,T4.GRUPO,T6.REFERENCIA,T6.DESCRICAO,T2.STATUS,SUM(T1.QTDE*T3.QTDE) AS QTDETOTAL,isnull(SUM(T6.PRCMINIMO*(T1.QTDE*T3.QTDE)),0) AS VALORTOTAL FROM MVVENDAITENS T1 " +
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) " +
                              " LEFT JOIN PRODUTOS PRD ON (PRD.ID_PRODUTO=T1.ID_PRODUTO) " +
                              " LEFT JOIN PRODUTOSKIT T3 ON (T3.ID_PRDMASTER=PRD.ID_PRODUTO) " +
                              " LEFT JOIN PRODUTOS T6 ON (T6.ID_PRODUTO=T3.ID_PRODUTO) " +
                              " LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T6.ID_GRUPO) " +
                              " LEFT JOIN PESSOAS T5 ON (T5.ID_PESSOA=T2.ID_PESSOA)" +
                              " LEFT JOIN VENDEDORES T7 ON (T7.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " WHERE PRD.PRODUTOKIT=1 AND T1.TIPOITEM IN ('S','N') AND T2.STATUS IN (2,3) AND T2.TPVENDA in ('PV','VF') AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T6.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();
                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T6.Id_Produto=" + TxtCodPrd.Text;
                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql = sSql + " AND T2.Id_Pessoa IN (" + LstCodPessoas + ")";
                if (CodVendedor == "0")
                    sSql = sSql + " AND T7.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";
                sSql = sSql + " GROUP BY T5.RAZAOSOCIAL,T4.GRUPO,T6.REFERENCIA,T6.DESCRICAO,T2.STATUS ";*/

                sSql = sSql + " ORDER BY T4.GRUPO,T3.DESCRICAO";

                //Atualizando a Tabela
                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("GRUPO", Type.GetType("System.String"));
                Tab01.Columns.Add("REFERENCIA", Type.GetType("System.String"));
                Tab01.Columns.Add("PESSOA", Type.GetType("System.String"));
                Tab01.Columns.Add("DESCRICAO", Type.GetType("System.String"));
                Tab01.Columns.Add("QTDETOTAL", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("VALORTOTAL", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("QTDEFATURADA", Type.GetType("System.Decimal"));
                //
                SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);
                bool FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;
                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {
                        if (Tab01.Rows[I]["PESSOA"].ToString().Trim() == LerSQL["RAZAOSOCIAL"].ToString().Trim() && Tab01.Rows[I]["GRUPO"].ToString().Trim() == LerSQL["GRUPO"].ToString().Trim() && Tab01.Rows[I]["Referencia"].ToString().Trim() == LerSQL["Referencia"].ToString().Trim())
                        {
                            FindLinha = true;
                            if (int.Parse(LerSQL["Status"].ToString()) == 2)
                                Tab01.Rows[I]["QTDEFATURADA"] = decimal.Parse(Tab01.Rows[I]["QTDEFATURADA"].ToString()) + decimal.Parse(LerSQL["QtdeTotal"].ToString());
                            else
                                Tab01.Rows[I]["QTDETOTAL"] = decimal.Parse(Tab01.Rows[I]["QTDETOTAL"].ToString()) + decimal.Parse(LerSQL["QtdeTotal"].ToString());

                            Tab01.Rows[I]["VALORTOTAL"] = decimal.Parse(Tab01.Rows[I]["VALORTOTAL"].ToString()) + decimal.Parse(LerSQL["VALORTotal"].ToString());
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        if (int.Parse(LerSQL["Status"].ToString()) == 2)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString().Trim(), LerSQL["Referencia"].ToString().Trim(), LerSQL["RAZAOSOCIAL"].ToString().Trim(), LerSQL["Descricao"].ToString().Trim(), 0, decimal.Parse(LerSQL["ValorTotal"].ToString()), decimal.Parse(LerSQL["QtdeTotal"].ToString()));
                        else
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString().Trim(), LerSQL["Referencia"].ToString().Trim(), LerSQL["RAZAOSOCIAL"].ToString().Trim(), LerSQL["Descricao"].ToString().Trim(), decimal.Parse(LerSQL["QtdeTotal"].ToString()), decimal.Parse(LerSQL["ValorTotal"].ToString()), 0);
                    }
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelVendaPrdCliente Rel001 = new Relatorios.RelVendaPrdCliente();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                //Rel001.SetDataSource(TabRel.Tables[0]);
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + " Vendedor:" + NmVendedor;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op14.Checked) // Margem de Negociação dos Produtos
            {
                string sSql = "SELECT T5.VENDEDOR,T4.GRUPO,T1.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO,ISNULL(SUM(CASE T5.DISTRIBUIDOR WHEN 1 THEN ((T1.VLRUNTCOMISSAO-T1.PRCATACADO)*T1.QTDE)*50/100 ELSE (T1.VLRUNTCOMISSAO*T1.QTDE)*5/100 END),0) AS CREDITO,0 DEBITO,0 AS DEB_BONIF FROM MVVENDAITENS T1 " +
                              "  LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) " +
                              "  LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              "  LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO)" +
                              "  LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              "  LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T2.ID_PESSOA)" +
                              " WHERE T2.CREDITO=0 AND T2.PREVENTREGA >= Convert(DateTime,'01/01/2014',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              " AND T2.STATUS=3 AND T3.PRODUTOKIT=0  AND ISNULL(T1.MARGEMNEGOCIO,0)=0 AND T1.TIPOITEM IN ('S','N') AND T2.TPVENDA='PV' AND T6.MARGEMNEGOCIO=0 AND T1.VLRUNTCOMISSAO >= (CASE T5.DISTRIBUIDOR WHEN 1 THEN T1.PRCATACADO ELSE T1.PRCVAREJO END)";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T4.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                sSql = sSql + " GROUP BY T5.VENDEDOR,T4.GRUPO,T1.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO" +
                              " UNION ALL " +
                              " SELECT T5.VENDEDOR,T4.GRUPO,T1.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO,0 AS CREDITO,ISNULL(SUM((T1.PRCATACADO-T1.VLRUNTCOMISSAO)*T1.QTDE),0) AS DEBITO,0 AS DEB_BONIF FROM MVVENDAITENS T1" +
                              "   LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              "   LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              "   LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO)" +
                              "   LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T2.ID_PESSOA)" +
                              " WHERE T2.CREDITO=0 AND T2.PREVENTREGA >= Convert(DateTime,'01/01/2014',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) "+
                              " AND T2.STATUS=3 AND T3.PRODUTOKIT=0 AND ISNULL(T1.MARGEMNEGOCIO,0)=0 AND T1.TIPOITEM IN ('S','N') AND T2.TPVENDA='PV'  AND T1.VLRUNTCOMISSAO < T1.PRCATACADO AND T6.MARGEMNEGOCIO=0";
                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T4.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();
                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;
                sSql = sSql + " GROUP BY T5.VENDEDOR,T4.GRUPO,T1.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO";

                //Somando as Bonificações
                sSql = sSql + " UNION ALL " +
                              " SELECT T5.VENDEDOR,T4.GRUPO,T1.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO,0 AS CREDITO,0 AS DEBITO, ISNULL(SUM(T1.VLRTOTAL),0) AS DEB_BONIF FROM MVVENDAITENS T1" +
                              "   LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              "   LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              "   LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO)" +
                              "   LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T2.ID_PESSOA)" +
                              " WHERE T5.DISTRIBUIDOR=1 AND T2.CREDITO=0 AND T2.PREVENTREGA >= Convert(DateTime,'01/01/2014',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "   AND T2.STATUS=3 AND T3.PRODUTOKIT=0 AND ISNULL(T1.MARGEMNEGOCIO,0)=0 AND T1.TIPOITEM IN ('S','N') AND T2.TPVENDA='BONIF' AND T6.MARGEMNEGOCIO=0";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T4.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();
                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;
                sSql = sSql + " GROUP BY T5.VENDEDOR,T4.GRUPO,T1.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO" +
                              " ORDER BY T5.VENDEDOR,T4.GRUPO,T3.DESCRICAO";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelMargemNegocio Rel001 = new Relatorios.RelMargemNegocio();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda: 01/01/2014 a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op15.Checked) // Relação de Clientes
            {
                string sSql = "SELECT T3.VENDEDOR,T1.ID_PESSOA,T1.RAZAOSOCIAL,T1.FANTASIA,T1.CNPJ,T1.INSC_UF,T1.CEP,T1.ENDERECO,T1.NUMERO,T1.COMPLEMENTO," +
                             " T1.BAIRRO,T1.CIDADE,T2.SIGLA,T1.FONE,T1.CELULAR,T1.CONTATO,T1.ATIVO,(SELECT TOP 1 DATA FROM MVVENDA V1 WHERE V1.ID_PESSOA=T1.ID_PESSOA AND V1.STATUS=3"+
                             " ORDER BY DATA DESC) AS ULTCOMPRA,T1.DATACADASTRO  FROM PESSOAS T1" +
                             " LEFT JOIN ESTADOS T2 ON (T2.ID_UF=T1.ID_UF)" +
                             " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                             " WHERE T1.DATACADASTRO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATACADASTRO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                if (TxtBairro.Text.Trim() != "")
                    sSql = sSql + " AND T1.BAIRRO LIKE '%" + TxtBairro.Text.Trim() + "%'";

                if (TxtCliente.Text.Trim() != "")
                    sSql = sSql + " AND T1.RAZAOSOCIAL LIKE '%" + TxtCliente.Text.Trim() + "%'";

                if (TxtCliente.Text.Trim() != "")
                    sSql = sSql + " AND T1.RAZAOSOCIAL LIKE '%" + TxtCliente.Text.Trim() + "%'";
                sSql = sSql + " ORDER BY T1.RAZAOSOCIAL";
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelClientes Rel001 = new Relatorios.RelClientes();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Data de Cadastro:" + Dt1.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op16.Checked) // Acompanhamento Anual de Vendas por Cliente
            {
                string sSql = "SELECT DISTINCT T3.VENDEDOR,T2.ID_PESSOA,T2.RAZAOSOCIAL,T2.ATIVO,T2.DATACADASTRO,(SELECT TOP 1 DATA FROM MVVENDA V1 WHERE V1.ID_PESSOA=T1.ID_PESSOA AND V1.STATUS=3 AND V1.TPVENDA IN ('PV','VF') ORDER BY DATA DESC) AS ULTCOMPRA," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=1),0) AS 'JAN'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=2),0) AS 'FEV'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=3),0) AS 'MAR'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=4),0) AS 'ABR'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=5),0) AS 'MAI'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=6),0) AS 'JUN'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=7),0) AS 'JUL'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=8),0) AS 'AGO'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=9),0) AS 'SET'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=10),0) AS 'OUT'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=11),0) AS 'NOV'," +
                              " ISNULL((SELECT SUM(VLRTOTAL) FROM MVVENDA V1 WHERE V1.ID_VENDEDOR=T1.ID_VENDEDOR AND V1.STATUS=3 AND V1.ID_PESSOA=T1.ID_PESSOA AND V1.TPVENDA IN ('PV','VF') AND YEAR(V1.PREVENTREGA)=" + TxtAno.Value.ToString() + " AND MONTH(V1.PREVENTREGA)=12),0) AS 'DEZ'," +
                              " T2.Fone,T2.Celular,T2.Contato" +
                              " FROM MVVENDA T1 " +
                              "  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                              "  LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " WHERE T1.STATUS=3 AND T2.RAZAOSOCIAL IS NOT NULL  AND T1.TPVENDA IN ('PV','VF')  AND YEAR(T1.PREVENTREGA)=" + TxtAno.Value.ToString();
                              
                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql = sSql + " AND T1.Id_Pessoa IN (" + LstCodPessoas + ")";
                sSql = sSql + " ORDER BY RAZAOSOCIAL,VENDEDOR";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelAcompVdCliente Rel001 = new Relatorios.RelAcompVdCliente();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Ano:" + TxtAno.Value.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op17.Checked) // Clientes Cadastrados por Aquisição de Produtos
            {
                string sSql = "SELECT T5.VENDEDOR,T2.PESSOA,T4.ID_GRUPO,T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO,SUM(T1.QTDE) AS QTDE FROM MVVENDAITENS T1 " +
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                              " LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO)" +
                              " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T2.ID_PESSOA)" +
                              " WHERE T2.TPVENDA IN ('PV','VF') AND T2.STATUS=3" +
                              " AND T6.DATACADASTRO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T6.DATACADASTRO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";                              

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T3.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T3.Id_Produto=" + TxtCodPrd.Text;
                sSql = sSql + " GROUP BY T5.VENDEDOR,T2.PESSOA,T4.ID_GRUPO,T4.GRUPO,T3.REFERENCIA,T3.DESCRICAO ORDER BY T5.VENDEDOR,T2.PESSOA,T4.GRUPO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelCadCli_AquisPrd Rel001 = new Relatorios.RelCadCli_AquisPrd();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text  = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op18.Checked) // Total das Vendas por Grupo/Produto por Cliente
            {
                string sSql1 = "";
                if (Ck_AgruparResultado.Checked)
                    sSql1 = sSql1 + "SELECT 0 as ID_VENDEDOR,'" + NmVendedor + "' AS VENDEDOR,T5.ID_GRUPO,T5.GRUPO,isnull(SUM(T1.QTDE*T1.VLRUNTREAL),0) AS TOTALITEM FROM MVVENDAITENS T1";
                else
                    sSql1 = sSql1 + "SELECT T6.ID_VENDEDOR,T6.VENDEDOR,T5.ID_GRUPO,T5.GRUPO,Isnull(SUM(T1.QTDE*T1.VLRUNTREAL),0) AS TOTALITEM FROM MVVENDAITENS T1";

                sSql1 = sSql1 + " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T2.ID_PESSOA)" +
                                " LEFT JOIN PRODUTOS T4 ON (T4.ID_PRODUTO=T1.ID_PRODUTO)" +
                                " LEFT JOIN GRUPOPRODUTO T5 ON (T5.ID_GRUPO=T4.ID_GRUPO)" +
                                " LEFT JOIN VENDEDORES T6 ON (T6.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                                " WHERE T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                                "  AND T2.TPVENDA IN ('PV','VF') AND T2.STATUS in (3) ";
                
                if (CodVendedor == "0")
                    sSql1 = sSql1 + " AND T6.EntraRel=1";
                else
                    sSql1 = sSql1 + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql1 = sSql1 + " AND T5.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql1 = sSql1 + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql1 = sSql1 + " AND T2.Id_Pessoa IN (" + LstCodPessoas + ")";

                if (Ck_AgruparResultado.Checked)
                    sSql1 = sSql1 + " GROUP BY T5.ID_GRUPO,T5.GRUPO ORDER BY T5.GRUPO,5 DESC";
                else
                    sSql1 = sSql1 + " GROUP BY T6.ID_VENDEDOR,T6.VENDEDOR,T5.ID_GRUPO,T5.GRUPO ORDER BY T5.GRUPO,5 DESC";

                string sSql2 ="";
                if (Ck_AgruparResultado.Checked)
                    sSql2 = sSql2 + "SELECT 0 as ID_VENDEDOR,'" + NmVendedor + "' AS VENDEDOR,T5.ID_GRUPO,T5.GRUPO,T3.RAZAOSOCIAL,isnull(SUM(T1.QTDE*T1.VLRUNTREAL),0) AS TOTALITEM,ISNULL(SUM(T1.QTDE),0) AS QTDE FROM MVVENDAITENS T1";
                else
                    sSql2 = sSql2 + "SELECT T6.ID_VENDEDOR,T6.VENDEDOR,T5.ID_GRUPO,T5.GRUPO,T3.RAZAOSOCIAL,isnull(SUM(T1.QTDE*T1.VLRUNTREAL),0) AS TOTALITEM,ISNULL(SUM(T1.QTDE),0) AS QTDE FROM MVVENDAITENS T1";

                sSql2 = sSql2 + " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T2.ID_PESSOA)" +
                                " LEFT JOIN PRODUTOS T4 ON (T4.ID_PRODUTO=T1.ID_PRODUTO)" +
                                " LEFT JOIN GRUPOPRODUTO T5 ON (T5.ID_GRUPO=T4.ID_GRUPO)" +
                                " LEFT JOIN VENDEDORES T6 ON (T6.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                                " WHERE T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                                "  AND T2.TPVENDA IN ('PV','VF') AND T2.STATUS in (3) ";

                if (CodVendedor == "0")
                    sSql2 = sSql2 + " AND T6.EntraRel=1";
                else
                    sSql2 = sSql2 + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql2 = sSql2 + " AND T5.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql2 = sSql2 + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql2 = sSql2 + " AND T2.Id_Pessoa IN (" + LstCodPessoas + ")";

                if (Ck_AgruparResultado.Checked)
                    sSql2 = sSql2 + " GROUP BY T5.ID_GRUPO,T5.GRUPO,T3.RAZAOSOCIAL ORDER BY T5.GRUPO, 6 DESC";
                else
                    sSql2 = sSql2 + " GROUP BY T6.ID_VENDEDOR,T6.VENDEDOR,T5.ID_GRUPO,T5.GRUPO,T3.RAZAOSOCIAL ORDER BY T6.VENDEDOR,T5.GRUPO, 6 DESC";

                //Atualizando a Tabela
                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("ID_VENDEDOR", Type.GetType("System.Int32"));
                Tab01.Columns.Add("VENDEDOR", Type.GetType("System.String"));
                Tab01.Columns.Add("ID_GRUPO", Type.GetType("System.Int32"));
                Tab01.Columns.Add("GRUPO", Type.GetType("System.String"));
                Tab01.Columns.Add("TOTALITEM", Type.GetType("System.Decimal"));                
                //
                SqlDataReader LerSQL = Controle.ConsultaSQL(sSql1);
                bool FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;
                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {
                        if (Tab01.Rows[I]["Vendedor"].ToString() == LerSQL["Vendedor"].ToString() && Tab01.Rows[I]["GRUPO"].ToString() == LerSQL["GRUPO"].ToString())
                        {
                            FindLinha = true;
                            Tab01.Rows[I]["TOTALITEM"] = decimal.Parse(Tab01.Rows[I]["TOTALITEM"].ToString()) + decimal.Parse(LerSQL["TOTALITEM"].ToString());                            
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        Tab01.Rows.Add(int.Parse(LerSQL["ID_VENDEDOR"].ToString()),LerSQL["VENDEDOR"].ToString(),int.Parse(LerSQL["ID_GRUPO"].ToString()), LerSQL["GRUPO"].ToString(), decimal.Parse(LerSQL["TOTALITEM"].ToString()));
                    }
                }
                //Atualizando a Tabela
                DataTable Tab02 = new DataTable();
                Tab02.Columns.Add("ID_VENDEDOR", Type.GetType("System.Int32"));
                Tab02.Columns.Add("VENDEDOR", Type.GetType("System.String"));
                Tab02.Columns.Add("ID_GRUPO", Type.GetType("System.Int32"));
                Tab02.Columns.Add("GRUPO", Type.GetType("System.String"));
                Tab02.Columns.Add("RAZAOSOCIAL", Type.GetType("System.String"));
                Tab02.Columns.Add("TOTALITEM", Type.GetType("System.Decimal"));
                Tab02.Columns.Add("QTDE", Type.GetType("System.Decimal"));
                //
                LerSQL = Controle.ConsultaSQL(sSql2);
                FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;
                    for (int I = 0; I <= Tab02.Rows.Count - 1; I++)
                    {
                        if (Tab02.Rows[I]["Vendedor"].ToString() == LerSQL["Vendedor"].ToString() && Tab02.Rows[I]["GRUPO"].ToString() == LerSQL["GRUPO"].ToString() && Tab02.Rows[I]["RAZAOSOCIAL"].ToString() == LerSQL["RAZAOSOCIAL"].ToString())
                        {
                            FindLinha = true;
                            Tab02.Rows[I]["TOTALITEM"] = decimal.Parse(Tab02.Rows[I]["TOTALITEM"].ToString()) + decimal.Parse(LerSQL["TOTALITEM"].ToString());
                            Tab02.Rows[I]["QTDE"]      = decimal.Parse(Tab02.Rows[I]["QTDE"].ToString()) + decimal.Parse(LerSQL["QTDE"].ToString());
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        Tab02.Rows.Add(int.Parse(LerSQL["ID_VENDEDOR"].ToString()), LerSQL["VENDEDOR"].ToString(), int.Parse(LerSQL["ID_GRUPO"].ToString()), LerSQL["GRUPO"].ToString(), LerSQL["RAZAOSOCIAL"].ToString(), decimal.Parse(LerSQL["TOTALITEM"].ToString()), decimal.Parse(LerSQL["QTDE"].ToString()));
                    }
                }

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelTotVdGrupo Rel001 = new Relatorios.RelTotVdGrupo();
                
                //DataSet Tab01 = new DataSet();
                //DataSet Tab02 = new DataSet();
                //Tab01 = Controle.ConsultaTabela(sSql1);
                //Tab02 = Controle.ConsultaTabela(sSql2);
                Rel001.Database.Tables[0].SetDataSource(Tab01);
                Rel001.Database.Tables[1].SetDataSource(Tab02);                       
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                if (int.Parse(TxtCodPrd.Text) > 0)
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + "  Produto: " + TxtDescricao.Text.Trim();
                else
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                                    
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op19.Checked) // Preço Médio de Venda dos Produto
            {
                string sSql = "SELECT T4.VENDEDOR,T3.GRUPO,PRD.REFERENCIA,PRD.DESCRICAO,T2.TPVENDA,PRD.PRCESPECIAL,PRD.PRCVAREJO,PRD.PRCMINIMO,PRD.PRCATACADO,"+
                              " SUM(T1.QTDE) AS QTDE,SUM(ISNULL(T1.VLRUNTREAL,0)*T1.QTDE) AS VLRTOTAL,PRD.PRCESPECIAL,PRD.PRCVAREJO,PRD.PRCMINIMO,PRD.PRCATACADO FROM PRODUTOS PRD" +
                              " LEFT JOIN MVVENDAITENS T1 ON (T1.ID_PRODUTO=PRD.ID_PRODUTO)" +
                              " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +                              
                              " LEFT JOIN GRUPOPRODUTO T3 ON (T3.ID_GRUPO=PRD.ID_GRUPO)" +
                              " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              " WHERE T2.STATUS='3' AND T2.TPVENDA IN ('PV','VF') AND ISNULL(T2.ID_VDORIGEM,0)=0" +
                              //" LEFT JOIN PESSOAS T5 ON (T5.ID_PESSOA=T2.ID_PESSOA)" +
                              //" WHERE T2.STATUS='3' AND T2.TPVENDA IN ('PV','VF','BONIF') AND T4.EntraRel=1" +
                              //"   AND T5.CLIE_FORN NOT IN (3,6)" +
                              //"   AND T1.VLRUNITARIO > T1.PRCATACADO "+
                              "   AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";
                                              
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T3.ID_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND PRD.REFERENCIA='" + BuscaPrd.CadProd.Referencia + "'";

                sSql = sSql + " GROUP BY T4.VENDEDOR,T3.GRUPO,PRD.REFERENCIA,PRD.DESCRICAO,T2.TPVENDA,PRD.PRCESPECIAL,PRD.PRCVAREJO,PRD.PRCMINIMO,PRD.PRCATACADO ";

                string NmFiliais = "";

                //Atualizando a Tabela
                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("VENDEDOR",   Type.GetType("System.String"));
                Tab01.Columns.Add("GRUPO",      Type.GetType("System.String"));
                Tab01.Columns.Add("REFERENCIA", Type.GetType("System.String"));                
                Tab01.Columns.Add("DESCRICAO",  Type.GetType("System.String"));                
                Tab01.Columns.Add("QTDE",       Type.GetType("System.Decimal"));
                Tab01.Columns.Add("VLRTOTAL",   Type.GetType("System.Decimal"));
                Tab01.Columns.Add("QTDEBONIF",  Type.GetType("System.Decimal"));
                Tab01.Columns.Add("VLRBONIF",   Type.GetType("System.Decimal"));
                Tab01.Columns.Add("PRCESPECIAL",Type.GetType("System.Decimal"));
                Tab01.Columns.Add("PRCVAREJO",  Type.GetType("System.Decimal"));
                Tab01.Columns.Add("PRCMINIMO",  Type.GetType("System.Decimal"));
                Tab01.Columns.Add("PRCATACADO", Type.GetType("System.Decimal"));                

                //                
                if (Ck_TodasFiliais.Checked)
                {
                    SqlDataReader LerFiliais = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL ORDER BY ID_FILIAL");
                    while (LerFiliais.Read())
                    {
                        if (LerFiliais["ServidorRemoto"].ToString().Trim() != "")
                        {
                            try
                            {
                                string StringConexao = "";

                                if (FrmPrincipal.VersaoDistribuidor)
                                    StringConexao = "Data Source=" + LerFiliais["ServidorRemoto"].ToString() + LerFiliais["Porta"].ToString() + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;";
                                else
                                    StringConexao = "Data Source=" + LerFiliais["ServidorRemoto"].ToString() + LerFiliais["Porta"].ToString() + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                                                                
                                if (FrmPrincipal.IdFilialConexao==int.Parse(LerFiliais["ID_Filial"].ToString()))
                                    StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                                SqlConnection ConexaoInterna = new SqlConnection(StringConexao);
                                ConexaoInterna.Open();
                                Funcoes ControleInterno = new Funcoes();
                                ControleInterno.Conexao = ConexaoInterna;
                                SqlDataReader LerSQL = ControleInterno.ConsultaSQL(sSql);
                                bool FindLinha = false;
                                while (LerSQL.Read())
                                {
                                    FindLinha = false;
                                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                                    {
                                        if (Tab01.Rows[I]["Referencia"].ToString().Trim() == LerSQL["Referencia"].ToString().Trim())
                                        {
                                            FindLinha = true;
                                            if (LerSQL["TPVENDA"].ToString() == "BONIF")
                                            {
                                                Tab01.Rows[I]["QTDEBONIF"] = decimal.Parse(Tab01.Rows[I]["QTDEBONIF"].ToString()) + decimal.Parse(LerSQL["QTDE"].ToString());
                                                Tab01.Rows[I]["VLRBONIF"] = decimal.Parse(Tab01.Rows[I]["VLRBONIF"].ToString()) + decimal.Parse(LerSQL["VLRTOTAL"].ToString());
                                            }
                                            else
                                            {
                                                Tab01.Rows[I]["QTDE"] = decimal.Parse(Tab01.Rows[I]["QTDE"].ToString()) + decimal.Parse(LerSQL["QTDE"].ToString());
                                                Tab01.Rows[I]["VLRTOTAL"] = decimal.Parse(Tab01.Rows[I]["VLRTOTAL"].ToString()) + decimal.Parse(LerSQL["VLRTOTAL"].ToString());
                                            }
                                            break;
                                        }
                                    }
                                    if (!FindLinha)
                                    {
                                        if (LerSQL["TPVENDA"].ToString() == "BONIF")
                                            Tab01.Rows.Add(LerSQL["VENDEDOR"].ToString().Trim(), LerSQL["GRUPO"].ToString().Trim(), LerSQL["Referencia"].ToString().Trim(), LerSQL["Descricao"].ToString().Trim(), 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), decimal.Parse(LerSQL["VlrTotal"].ToString()), decimal.Parse(LerSQL["PRCESPECIAL"].ToString()), decimal.Parse(LerSQL["PRCVAREJO"].ToString()), decimal.Parse(LerSQL["PRCMINIMO"].ToString()), decimal.Parse(LerSQL["PRCATACADO"].ToString()));
                                        else
                                            Tab01.Rows.Add(LerSQL["VENDEDOR"].ToString().Trim(), LerSQL["GRUPO"].ToString().Trim(), LerSQL["Referencia"].ToString().Trim(), LerSQL["Descricao"].ToString().Trim(), decimal.Parse(LerSQL["Qtde"].ToString()), decimal.Parse(LerSQL["VlrTotal"].ToString()), 0, 0, decimal.Parse(LerSQL["PRCESPECIAL"].ToString()), decimal.Parse(LerSQL["PRCVAREJO"].ToString()), decimal.Parse(LerSQL["PRCMINIMO"].ToString()), decimal.Parse(LerSQL["PRCATACADO"].ToString()));
                                    }
                                }
                                NmFiliais = NmFiliais + LerFiliais["ID_FILIAL"].ToString().Trim() + " / ";
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else
                {
                    SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);
                    bool FindLinha = false;
                    while (LerSQL.Read())
                    {
                        FindLinha = false;
                        for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                        {
                            if (Tab01.Rows[I]["Referencia"].ToString().Trim() == LerSQL["Referencia"].ToString().Trim())
                            {
                                FindLinha = true;
                                if (LerSQL["TPVENDA"].ToString() == "BONIF")
                                {
                                    Tab01.Rows[I]["QTDEBONIF"] = decimal.Parse(Tab01.Rows[I]["QTDEBONIF"].ToString()) + decimal.Parse(LerSQL["QTDE"].ToString());
                                    Tab01.Rows[I]["VLRBONIF"] = decimal.Parse(Tab01.Rows[I]["VLRBONIF"].ToString()) + decimal.Parse(LerSQL["VLRTOTAL"].ToString());
                                }
                                else
                                {
                                    Tab01.Rows[I]["QTDE"] = decimal.Parse(Tab01.Rows[I]["QTDE"].ToString()) + decimal.Parse(LerSQL["QTDE"].ToString());
                                    Tab01.Rows[I]["VLRTOTAL"] = decimal.Parse(Tab01.Rows[I]["VLRTOTAL"].ToString()) + decimal.Parse(LerSQL["VLRTOTAL"].ToString());
                                }
                                break;
                            }
                        }
                        if (!FindLinha)
                        {
                            if (LerSQL["TPVENDA"].ToString() == "BONIF")
                                Tab01.Rows.Add(LerSQL["VENDEDOR"].ToString().Trim(), LerSQL["GRUPO"].ToString().Trim(), LerSQL["Referencia"].ToString().Trim(), LerSQL["Descricao"].ToString().Trim(), 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), decimal.Parse(LerSQL["VlrTotal"].ToString()), decimal.Parse(LerSQL["PRCESPECIAL"].ToString()), decimal.Parse(LerSQL["PRCVAREJO"].ToString()), decimal.Parse(LerSQL["PRCMINIMO"].ToString()), decimal.Parse(LerSQL["PRCATACADO"].ToString()));
                            else
                                Tab01.Rows.Add(LerSQL["VENDEDOR"].ToString().Trim(), LerSQL["GRUPO"].ToString().Trim(), LerSQL["Referencia"].ToString().Trim(), LerSQL["Descricao"].ToString().Trim(), decimal.Parse(LerSQL["Qtde"].ToString()), decimal.Parse(LerSQL["VlrTotal"].ToString()), 0, 0, decimal.Parse(LerSQL["PRCESPECIAL"].ToString()), decimal.Parse(LerSQL["PRCVAREJO"].ToString()), decimal.Parse(LerSQL["PRCMINIMO"].ToString()), decimal.Parse(LerSQL["PRCATACADO"].ToString()));
                        }
                    }
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelPrcMedioVd Rel001 = new Relatorios.RelPrcMedioVd();
                DataSet TabRel = new DataSet();
                            
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                if (Ck_TodasFiliais.Checked)
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + "   Agrupamento de Todas Filiais: " + NmFiliais;
                else
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op20.Checked) // Extrato de Venda dos Produtos por Cliente
            {
                string sSql = "SELECT T3.RAZAOSOCIAL,T5.GRUPO,T4.REFERENCIA,T4.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRUNTREAL,T2.DATA,T8.DESCRICAO AS TIPOMOV,"+
                              " T2.ID_VENDA,T2.NUMDOCUMENTO,T2.PREVENTREGA,T7.ENTREGADOR,T6.VENDEDOR FROM MVVENDAITENS T1 "+
                              "  LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)"+
                              "  LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T2.ID_PESSOA)"+
                              "  LEFT JOIN PRODUTOS T4 ON (T4.ID_PRODUTO=T1.ID_PRODUTO)"+
                              "  LEFT JOIN GRUPOPRODUTO T5 ON (T5.ID_GRUPO=T4.ID_GRUPO)"+
                              "  LEFT JOIN VENDEDORES T6 ON (T6.ID_VENDEDOR=T2.ID_VENDEDOR)"+
                              "  LEFT JOIN ENTREGADORES T7 ON (T7.ID_ENTREGADOR=T2.ID_ENTREGADOR)"+
                              "  LEFT JOIN TABELASAUX T8 ON (T8.CHAVE=T2.TPVENDA)"+
                              " WHERE T2.STATUS=3 AND T2.TPVENDA IN ('PV','VF') AND T1.TIPOITEM in ('S','N')" +
                              "   AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T6.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T5.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql = sSql + " AND T3.Id_Pessoa IN (" + LstCodPessoas + ")";

                sSql = sSql + " ORDER BY T3.RAZAOSOCIAL,T5.GRUPO,T4.DESCRICAO,T2.PREVENTREGA DESC";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelExtVendaPrdCliente Rel001 = new Relatorios.RelExtVendaPrdCliente();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);                
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op21.Checked) // Pedidos de Vendas por Cliente
            {
                string sSql = "SELECT CASE T1.STATUS WHEN 1 THEN 'CONFIRMADO' WHEN 2 THEN 'FATURADO' WHEN 3 THEN 'ENTREGUE' END AS STATUS,T2.RAZAOSOCIAL,T1.ID_VENDA,T1.DATA,T1.PREVENTREGA,T1.NUMDOCUMENTO,T1.VLRSUBTOTAL,T1.VLRDESCONTO,T1.CREDITO,T1.VLRTOTAL,T1.FORMNF,T3.VENDEDOR,T4.ENTREGADOR,T5.FORMAPGTO,T1.PRAZOPGTO FROM MVVENDA T1" +
                              " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                              " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " LEFT JOIN ENTREGADORES T4 ON (T4.ID_ENTREGADOR=T1.ID_ENTREGADOR)" +
                              " LEFT JOIN FORMAPAGAMENTO T5 ON (T5.ID_FORMAPGTO=T1.ID_FORMAPGTO)" +
                              " WHERE T1.STATUS IN (1,2,3) AND T1.TPVENDA IN ('PV','VF')" +
                              "   AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (CodVendedor != "0")
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                                
                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql = sSql + " AND T1.Id_Pessoa IN (" + LstCodPessoas + ")";

                sSql = sSql + " ORDER BY T2.RAZAOSOCIAL,1,5";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelPedVDporCliente Rel001 = new Relatorios.RelPedVDporCliente();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op22.Checked) // Mapa de Vista
            {
                string sSql = "SELECT T5.VENDEDOR as CONSULTOR ,T3.Vendedor,T1.DtVisita,T1.Objetivo,T1.DtRetorno,T1.Retorno,T1.CLIENTE,T1.ID_VENDA,T4.NumDocumento AS DOCVENDA," +
                              " CASE T1.STATUS WHEN 0 THEN 'Em Aberta' WHEN 1 THEN 'Pendente' WHEN 2 THEN 'Concluida' END AS STATUS FROM AgendaVisita T1 "+
                              " LEFT JOIN Pessoas T2 ON (T2.Id_Pessoa=T1.Id_Pessoa)"+
                              " LEFT JOIN Vendedores T3 ON (T3.Id_Vendedor=T1.Id_Vendedor)"+
                              " LEFT JOIN MvVenda T4 ON (T4.Id_Venda=T1.Id_Venda)"+
                              " LEFT JOIN Vendedores T5 ON (T5.Id_Vendedor=T1.Id_VendVisita)" +
                              "  WHERE CONVERT(DATETIME,Convert(CHAR,T1.DTVISITA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,Convert(CHAR,T1.DTVISITA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (Rb_AgVisPend.Checked)
                    sSql = sSql + " AND T1.Status=1";
                if (Rb_AgVisConc.Checked)
                    sSql = sSql + " AND T1.Status=2";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                
                sSql = sSql + " ORDER BY T5.Vendedor,T1.DtVisita";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelMapaVisita Rel001 = new Relatorios.RelMapaVisita();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Visita:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op23.Checked) // Resumo das Visita
            {
                string sSql = "SELECT T2.VENDEDOR,CASE T1.STATUS WHEN 0 THEN 'Em Aberta' WHEN 1 THEN 'Pendente' WHEN 2 THEN 'Concluida' END AS STATUS,COUNT(*) AS QVISITA,ISNULL(SUM(T3.VLRTOTAL),0) AS VLRTOTAL from AgendaVisita T1"+
                              " LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR)"+
                              " LEFT JOIN MVVENDA T3 ON (T3.ID_VENDA=T1.ID_VENDA) "+
                              "  WHERE CONVERT(DATETIME,Convert(CHAR,T1.DTVISITA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,Convert(CHAR,T1.DTVISITA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";
                                
                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " GROUP BY T2.VENDEDOR,T1.STATUS";// ORDER BY T2.Vendedor,T1.DtVisita";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelGrafAgVisita Rel001 = new Relatorios.RelGrafAgVisita();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Visita:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op24.Checked) // Lista de Orçamentos por Vendedor
            {
                string sSql = "SELECT T3.VENDEDOR,T1.DATA AS DTORCAM,T2.DATA AS DTVENDA,T2.PREVENTREGA,T1.PESSOA AS RAZAOSOCIAL,T1.ID_VENDA AS NUMORCM,T1.NUMDOCUMENTO AS DOCORCAM,T1.VLRTOTAL AS VLRORCAM,T2.ID_VENDA AS NUMVENDA,T2.NUMDOCUMENTO AS DOCVENDA,T2.VLRTOTAL AS VLRPEDIDO,T5.USUARIO," +
                              "T4.Fone,T4.Celular,T4.Contato,(SELECT TOP 1 DATA FROM MVVENDA WHERE ID_PESSOA=T4.ID_PESSOA AND STATUS=3 AND TPVENDA='PV' ORDER BY DATA DESC) AS ULTCOMPRA FROM MVVENDA T1 " +
                              " LEFT JOIN MVVENDA T2 ON (T2.NUMDOCUMENTO=T1.VINCULOVD AND T1.VINCULOVD<>'' AND T2.STATUS in (1,2,3))" +
                              " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " LEFT JOIN PESSOAS T4 ON (T4.ID_PESSOA=T1.ID_PESSOA) " +
                              " LEFT JOIN USUARIOS T5 ON (T5.ID_USUARIO=T1.ID_USUARIO) " +
                              " WHERE T1.TPVENDA='OC' AND CONVERT(DATETIME,Convert(CHAR,T1.PREVENTREGA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,Convert(CHAR,T1.PREVENTREGA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " ORDER BY T3.VENDEDOR,T1.DATA DESC,T2.STATUS";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelListaOrcam Rel001 = new Relatorios.RelListaOrcam();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + " (Por Data de Entrega)";
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op25.Checked) // Resumo dos Orçamentos Realizados
            {
                string sSql = "SELECT T3.VENDEDOR, ISNULL(SUM(T1.VLRTOTAL),0) AS TOTORCAM,COUNT(*) AS REG_ORC, ISNULL(SUM(T2.VLRTOTAL),0) AS TOTVENDA,ISNULL(SUM(CASE ISNULL(T2.ID_VENDA,0) WHEN 0 THEN 0 ELSE 1 END),0) AS REG_VENDA FROM MVVENDA T1 "+
                              " LEFT JOIN MVVENDA T2 ON (T2.NUMDOCUMENTO=T1.VINCULOVD AND T1.VINCULOVD<>'' AND T2.STATUS=3)" +
                              " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)"+
                              " LEFT JOIN PESSOAS T4 ON (T4.ID_PESSOA=T1.ID_PESSOA)"+
                              " WHERE T1.TPVENDA='OC' AND CONVERT(DATETIME,Convert(CHAR,T1.PREVENTREGA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,Convert(CHAR,T1.PREVENTREGA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                sSql = sSql + " GROUP BY T3.VENDEDOR";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoOrcamento Rel001 = new Relatorios.RelResumoOrcamento();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op26.Checked) // Resumo de Atendimentos Telemarketing
            {

                string sSql = " SELECT DISTINCT T2.ID_Usuario,T2.Usuario,T3.VENDEDOR, " +
                              " (SELECT COUNT(*) FROM MvVenda V1 WHERE V1.TpVenda='OC' " +
                              "   AND CONVERT(DATETIME,Convert(CHAR,V1.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "   AND CONVERT(DATETIME,Convert(CHAR,V1.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "   AND V1.Id_Vendedor=T1.Id_Vendedor and V1.Id_Usuario=T2.Id_Usuario) AS QtdeOrc," +
                              " (SELECT ISNULL(SUM(V2.VlrTotal),0) FROM MvVenda V2 WHERE V2.TpVenda='OC' " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V2.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V2.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "    AND V2.Id_Vendedor=T1.Id_Vendedor and V2.Id_Usuario=T2.Id_Usuario) AS TotalOrc, " +
                              " (SELECT COUNT(*) FROM MvVenda V3 JOIN MvVenda V4 ON (V4.NUMDOCUMENTO=V3.VINCULOVD AND V3.NumDocumento<>'' AND V4.Status in (1,2,3))" +
                              "    WHERE V3.TpVenda='OC' AND CONVERT(DATETIME,Convert(CHAR,V3.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V3.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "    AND V3.Id_Vendedor=T1.Id_Vendedor and V3.Id_Usuario=T2.Id_Usuario) AS QtdeOrcVD," +
                              " (SELECT ISNULL(SUM(V4.VlrTotal),0) FROM MvVenda V3 LEFT JOIN MvVenda V4 ON (V4.NUMDOCUMENTO=V3.VINCULOVD AND V3.NumDocumento<>'' AND V4.Status in (1,2,3)) " +
                              "    WHERE V3.TpVenda='OC' AND CONVERT(DATETIME,Convert(CHAR,V3.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V3.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              "    AND V3.Id_Vendedor=T1.Id_Vendedor and V3.Id_Usuario=T2.Id_Usuario) AS TotalOrcVD," +
                              " (SELECT COUNT(*) FROM MvVenda V5 WHERE V5.TpVenda in ('PV','VF') " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V5.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V5.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "    AND V5.Status in (1,2,3) AND SUBSTRING(V5.VINCULOVD,1,2)<>'OC' AND V5.Id_Vendedor=T1.Id_Vendedor and V5.Id_Usuario=T2.Id_Usuario) AS QtdeVD," +
                              " (SELECT ISNULL(SUM(V6.VlrTotal),0) FROM MvVenda V6 WHERE V6.TpVenda in ('PV','VF') AND CONVERT(DATETIME,Convert(CHAR,V6.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V6.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              "    AND V6.Status in (1,2,3) AND SUBSTRING(V6.VINCULOVD,1,2)<>'OC' AND V6.Id_Vendedor=T1.Id_Vendedor and V6.Id_Usuario=T2.Id_Usuario) AS TotalVD ," +
                              " (SELECT COUNT(*) FROM MvVenda V7 WHERE V7.TpVenda in ('PV','VF') AND CONVERT(DATETIME,Convert(CHAR,V7.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V7.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              "    AND V7.Status in (1,2,3) AND V7.Id_Vendedor=T1.Id_Vendedor and V7.Id_Usuario=T2.Id_Usuario and V7.cli_reativado=1) AS Reativacao , " +
                              " (SELECT ISNULL(SUM(V7.VlrTotal),0) FROM MvVenda V7 WHERE V7.TpVenda in ('PV','VF') AND CONVERT(DATETIME,Convert(CHAR,V7.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "    AND CONVERT(DATETIME,Convert(CHAR,V7.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              "    AND SUBSTRING(V7.VINCULOVD,1,2)<>'OC'" +
                              "    AND V7.Status in (1,2,3) AND V7.Id_Vendedor=T1.Id_Vendedor and V7.Id_Usuario=T2.Id_Usuario AND V7.cli_reativado=1) AS TotalReativacao " +
                              " FROM MVVENDA T1 " +
                              "   LEFT JOIN Usuarios T2 ON (T2.Id_Usuario=T1.Id_Usuario)" +
                              "   LEFT JOIN Vendedores T3 ON (T3.Id_Vendedor=T1.Id_Vendedor)" +
                              "  WHERE " +
                              "   CONVERT(DATETIME,Convert(CHAR,T1.DATA,103),103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              "   AND CONVERT(DATETIME,Convert(CHAR,T1.DATA,103),103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "   AND T2.telemarketing=1 order by 1,2";
                
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoAtendTelem Rel001 = new Relatorios.RelResumoAtendTelem();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op27.Checked) // Resumo de Vendas do Grupo por Vendedor
            {
                string sSql = "select t4.Grupo,t5.Vendedor,round(SUM(t1.VlrUntReal*t1.Qtde),2) as total from MvVendaItens t1" +
                              " left join MvVenda t2 on (t2.Id_Venda=t1.Id_Venda)"+
                              " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)"+
                              " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)"+
                              " left join Vendedores t5 on (t5.Id_Vendedor=t2.Id_Vendedor)"+                              
                              " Where t2.Status=3 and t2.TpVenda in ('PV','VF')"+
                              "   AND T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";                             

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T4.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                sSql = sSql + " Group by t4.Grupo,t5.Vendedor Order by 1,2";
 
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoVdGrpVendedor Rel001 = new Relatorios.RelResumoVdGrpVendedor();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text  = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op28.Checked) // Campanha Bettanin 2019
            {
                string StrSQLVend = "";
                if (CodVendedor == "0")
                    StrSQLVend = " AND T4.EntraRel=1";
                else
                    StrSQLVend= " AND T4.Id_Vendedor in (" + CodVendedor + ")";


                string sSql = "SELECT T4.Vendedor,'1. TOTAL DA VENDA' as ItemCamp,Round(SUM(T1.VLRUNTREAL*T1.QTDE),2) AS VALORTOTAL,FLOOR(Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)/1000)*10 AS PONTOS from mvvendaitens t1" +
                              " left join MvVenda t2 on (t2.id_Venda=t1.id_venda)  left join produtos t3 on (t3.id_produto=t1.id_produto)  left join vendedores t4 on (t4.id_vendedor=t2.Id_Vendedor)" +
                              " where t2.TpVenda='PV' and t2.Status=3 and isnull(t2.Id_VdOrigem,0)=0"+
                              " and T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              " and t3.id_grupo=373 "+ StrSQLVend + " Group by T4.Vendedor" +
                              " Union all" +
                              " Select T4.Vendedor,'2. MOP/BALDE NOVIÇA/BRILHOS' as ItemCamp,Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)  AS VALORTOTAL,FLOOR(Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)/200)*5 AS PONTOS from mvvendaitens t1" +
                              "  left join MvVenda t2 on (t2.id_Venda=t1.id_venda)  left join produtos t3 on (t3.id_produto=t1.id_produto)  left join vendedores t4 on (t4.id_vendedor=t2.Id_Vendedor)" +
                              " where t2.TpVenda='PV' and t2.Status=3 and isnull(t2.Id_VdOrigem,0)=0" +
                              " and T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              " and t3.Referencia in ('999998','100026','999996','1000032','1000024','1000034','999997','1000033') " + StrSQLVend + " Group by  T4.Vendedor" +
                              " Union all" +
                              " Select T4.Vendedor,'3. WIPES/LENÇOS/ODORIZADORES' as ItemCamp,Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)  AS VALORTOTAL,FLOOR(Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)/100)*5 AS PONTOS from mvvendaitens t1" +
                              " left join MvVenda t2 on (t2.id_Venda=t1.id_venda)  left join produtos t3 on (t3.id_produto=t1.id_produto)  left join vendedores t4 on (t4.id_vendedor=t2.Id_Vendedor)" +
                              " where t2.TpVenda='PV' and t2.Status=3 and isnull(t2.Id_VdOrigem,0)=0" +
                              " and T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              " and t3.Referencia in ('1000012','1000013','1000007','1000008','1000003','999999','1000001','1000000') " + StrSQLVend + " Group by  T4.Vendedor" +
                              " Union all" +
                              " Select T4.Vendedor,'4. VASS. NOVIÇA' as ItemCamp,Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)  AS VALORTOTAL,FLOOR(Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)/100)*5 AS PONTOS from mvvendaitens t1" +
                              " left join MvVenda t2 on (t2.id_Venda=t1.id_venda)  left join produtos t3 on (t3.id_produto=t1.id_produto)  left join vendedores t4 on (t4.id_vendedor=t2.Id_Vendedor)" +
                              " where t2.TpVenda='PV' and t2.Status=3 and isnull(t2.Id_VdOrigem,0)=0" +
                              " and T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              " and t3.Referencia in ('1000009') " + StrSQLVend + " Group by  T4.Vendedor" +
                              " Union all" +
                              " Select T4.Vendedor,'5. RODOS' as ItemCamp,Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)  AS VALORTOTAL,FLOOR(Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)/100)*5 AS PONTOS from mvvendaitens t1" +
                              " left join MvVenda t2 on (t2.id_Venda=t1.id_venda) left join produtos t3 on (t3.id_produto=t1.id_produto) left join vendedores t4 on (t4.id_vendedor=t2.Id_Vendedor)" +
                              " where t2.TpVenda='PV' and t2.Status=3 and isnull(t2.Id_VdOrigem,0)=0" +
                              " and T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              " and t3.Referencia in ('1000031','999991','999992')  " + StrSQLVend + " Group by  T4.Vendedor" +
                              " Union all" +
                              " Select T4.Vendedor,'6. ESPONJAS' as ItemCamp,Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)  AS VALORTOTAL,FLOOR(Round(SUM(T1.VLRUNTREAL*T1.QTDE),2)/100)*5 AS PONTOS from mvvendaitens t1" +
                              " left join MvVenda t2 on (t2.id_Venda=t1.id_venda)  left join produtos t3 on (t3.id_produto=t1.id_produto)  left join vendedores t4 on (t4.id_vendedor=t2.Id_Vendedor)" +
                              " where t2.TpVenda='PV' and t2.Status=3 and isnull(t2.Id_VdOrigem,0)=0" +
                              " and T2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              " and t3.Referencia in ('1000030','1000037','1000082','1000016','1000035','1000036') " + StrSQLVend + " Group by  T4.Vendedor" +
                              " Union all" +
                              " SELECT T4.Vendedor,'7. CLIENTES NOVOS/REATIVADOS' as ItemCamp,0.00 AS VALORTOTAL,COUNT(*) AS PONTOS FROM MvVenda T1" +
                              "  left join vendedores T4 on (T4.id_vendedor=T1.Id_Vendedor)" +
                              " where t1.id_Venda in (Select distinct V1.id_venda from MvVendaItens V1"+
                              "                         left join MvVenda V2 on(V2.id_Venda = V1.id_venda)"+
                              "                         left join produtos V3 on(V3.id_produto = V1.id_produto)"+
                              "                         where V2.TpVenda = 'PV' and V2.Status = 3 and isnull(V2.Id_VdOrigem,0)= 0"+
                              "                          and v2.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND v2.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              "                          and V3.id_grupo = 373 and V2.cli_reativado = 1 and V2.ID_Vendedor=t4.id_Vendedor)" + StrSQLVend + " Group by T4.Vendedor"+
                              " order by 1,2";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelBattanin2019 Rel001 = new Relatorios.RelBattanin2019();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op29.Checked) // Campanha por Pontos
            {
                string sSql = "select T5.Vendedor,T4.Grupo,T3.Referencia,T3.Descricao,T3.Pontos,Sum(T1.Qtde) as QtdeTotal from MvVendaItens T1"+
                               " left join MvVenda T2 on (T2.Id_Venda = T1.id_Venda)"+
                               " left join Produtos T3 on (T3.Id_Produto = T1.Id_Produto)"+
                               " left join grupoproduto T4 on (T4.Id_Grupo = T3.ID_Grupo)"+
                               " Left Join Vendedores T5 on(T5.ID_Vendedor= T2.ID_Vendedor)"+
                               " where T2.Status=3 and T2.TpVenda in ('PV','VF')"+
                               " and Isnull(T2.Id_VdOrigem,0)= 0 and T3.Pontos > 0"+                            
                               " AND T2.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T4.Id_GRUPO=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;

                sSql = sSql + " Group By T5.Vendedor,T4.Grupo,T3.Referencia,T3.Descricao,T3.Pontos  Order by T5.Vendedor,T4.Grupo,T3.Descricao";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelCampPontos Rel001 = new Relatorios.RelCampPontos();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op30.Checked) // Acompanhamento das Entregas
            {
                string sSql = "SELECT T1.Data,T1.Pessoa,T1.Id_Venda,T1.Endereco,T1.Bairro,T2.Fantasia,T1.DtEnvioRec,T1.Id_VdDestino,T1.VlrTotal,T3.Vendedor,T1.Status FROM MvVenda T1" +
                              " left join Empresa_Filial T2 on(t2.Id_Filial = t1.id_filialEntrega)" +
                              " left join Vendedores T3 on(t3.Id_Vendedor = t1.id_Vendedor)";
                sSql = sSql + " WHERE t1.TPVENDA<>'PI' AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (Rb_Aberto.Checked)
                    sSql = sSql + " AND T1.Id_Entregador > 0 and T1.Status < 3 ";

                if (LstEntregador.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_Entregador=" + LstEntregador.SelectedValue.ToString();

                if (LstEntrega.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_FilialEntrega=" + LstEntrega.SelectedValue.ToString();

                if (Rb_Entregue.Checked)
                    sSql = sSql + " AND T1.Status=3";

                if (LstTipoMov.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.TpVenda='" + LstTipoMov.SelectedValue.ToString().Trim() + "'";

                if (Rb_SemEntregador.Checked)
                    sSql = sSql + " AND (T1.Id_Entregador=0 and T1.Status in (1,2) and T1.TPVENDA<>'VF')";

                sSql = sSql + " order by T1.Data,T1.Bairro,T1.Id_Venda";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelAcompEntrega Rel001 = new Relatorios.RelAcompEntrega();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Periodo de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + "  Filial Entrega: " + LstEntrega.Text.ToString().Trim();                
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
        private void BtnBuscaPrd_Click(object sender, EventArgs e)
        {               
            BuscaPrd.IdProduto = 0;
            BuscaPrd.ShowDialog();
            if (BuscaPrd.IdProduto > 0)
            {
                LstGrupo.SelectedValue = "0";
                TxtCodPrd.Text    = BuscaPrd.CadProd.IdProduto.ToString();
                TxtDescricao.Text = BuscaPrd.CadProd.Descricao;
            }
            else
                TxtCodPrd.Text = "0";
        }
        private void Dt1_Validated(object sender, EventArgs e)
        {
            if (Op08.Checked || Op09.Checked)
                LstCaixa = FrmPrincipal.PopularCombo("SELECT T1.ID_CAIXA,T2.USUARIO FROM CAIXABALCAO T1 LEFT JOIN USUARIOS T2 ON (T2.ID_USUARIO=T1.ID_USUARIO) WHERE T1.DATA = Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103)", LstCaixa);
        }
    }
}
 