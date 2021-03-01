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
    public partial class FrmRelAdm : Form
    {
        Funcoes Controle = new Funcoes();
        FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();        
        public TelaPrincipal FrmPrincipal;

        public FrmRelAdm()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao         = FrmPrincipal.Conexao;
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
            Op01.Checked = true;
            Dt1.Value    = DateTime.Now;
            Dt2.Value    = DateTime.Now;
            CamposLista();
            TxtAno.Value = DateTime.Now.Year;
            LstMes.SelectedIndex = DateTime.Now.Month - 1;
            TxtCodPrd.Text       = "0";
            TxtDescricao.Text    = "";
            TxtVllrMinimo.Value  = 300;
        }
        private void CamposLista()
        {            
            LstEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstEntregador, "Todos");
            LstVeiculo    = FrmPrincipal.PopularCombo("SELECT Id_VEICULO,Substring(Veiculo,1,30)+' / '+Placa as VEICULO FROM VEICULOS ORDER BY VEICULO", LstVeiculo, "Todos");
            LstUsuario    = FrmPrincipal.PopularCombo("SELECT Id_Usuario,Usuario FROM Usuarios where telemarketing=1 ORDER BY Usuario", LstUsuario, "Todos");
            LstPromocao   = FrmPrincipal.PopularCombo("SELECT Id_Promocao,Substring(Descricao,1,60) as Descricao FROM PromocaoProdutos ORDER BY Descricao", LstPromocao, "Todas");
            LstEquipe     = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,Vendedor FROM Vendedores where cotafinanceira=1 ORDER BY Vendedor", LstEquipe, "Todas");

            if (FrmPrincipal.Perfil_Usuario.SeusMov == 1)
            {
                CkListVendedor = FrmPrincipal.PopularCheckList("SELECT Id_Vendedor,SubString(Vendedor,1,30) as Vendedor FROM Vendedores Where Id_Vendedor=" + FrmPrincipal.Perfil_Usuario.IdVendedor.ToString(), CkListVendedor, "", "");
                if (CkListVendedor.Items.Count > 0)
                    CkListVendedor.SetItemChecked(0, true);
                CkListVendedor.Enabled = false;
            }
            else
            {
                CkListVendedor = FrmPrincipal.PopularCheckList("SELECT Id_Vendedor,SubString(Vendedor,1,30) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", CkListVendedor, "", "");

                SqlDataReader TabVend = Controle.ConsultaSQL("SELECT ID_VENDEDOR FROM VENDEDORES WHERE EntraRel=1 and ativo=1");
                while (TabVend.Read())
                {
                    for (int I = 0; I <= CkListVendedor.Items.Count - 1; I++)
                    {
                        DataRowView item = (DataRowView)CkListVendedor.Items[I];
                        if (item.Row[0].ToString() == TabVend["ID_VENDEDOR"].ToString())
                            CkListVendedor.SetItemChecked(I, true);
                    }
                }

            } 
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
            Ck_AgruparResultado.Visible = false;
            CkListVendedor.Visible     = true;
            PnlPessoa.Visible          = Op01.Checked;
            PnlPeriodo.Visible         = !Op03.Checked && !Op08.Checked && !Op09.Checked && !Op10.Checked && !Op11.Checked && !Op15.Checked && !Op17.Checked;
            PnlAnoMes.Visible          = Op01.Checked || Op03.Checked || Op08.Checked || Op09.Checked || Op10.Checked || Op15.Checked;
            PnlAno.Visible             = Op03.Checked || Op08.Checked || Op09.Checked || Op10.Checked || Op15.Checked; ;
            PnlMes.Visible             = Op08.Checked || Op09.Checked || Op15.Checked;        
            //PnlVendedor.Visible        = !Op04.Checked && !Op05.Checked && !Op17.Checked;
            PnlVeiculos.Visible        = Op04.Checked;
            PnlEntregador.Visible      = Op05.Checked;
            PnlUsuarioTele.Visible     = Op07.Checked;
            PnlVendedor.Visible        = !Op04.Checked && !Op05.Checked && !Op07.Checked && !Op17.Checked && !Op18.Checked;
            PnlPromocao.Visible        = Op13.Checked || Op14.Checked || Op17.Checked;
            PnlProduto.Visible         = Op14.Checked;
            PnlVlrMinimo.Visible       = Op06.Checked;
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
                    LstCodPessoas = "0";
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
                SqlDataReader TabCli = Controle.ConsultaSQL("SELECT * FROM Vendedores WHERE EntraRel=1 and Id_VendGrupo=" + LstEquipe.SelectedValue.ToString());
                while (TabCli.Read())
                    CodVendedor = CodVendedor + "," + TabCli["ID_Vendedor"].ToString();
            }
            
            BtnImprimir.Enabled = false;
            if (Op01.Checked) // Demonstrativo de Vendas por Faixa de Comissão
            {
                DataTable TabFaixa = new DataTable();
                TabFaixa.Columns.Add("ID_VENDEDOR", Type.GetType("System.Int32"));
                TabFaixa.Columns.Add("VENDEDOR", Type.GetType("System.String"));
                TabFaixa.Columns.Add("ID_PREMIO", Type.GetType("System.Int32"));
                TabFaixa.Columns.Add("P_COMISSAO", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("TOTAL", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("TOTALCOMISSAO", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("TOTALMEDIA", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("MEDIACOMISSAO", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("NREG_ATIVAO", Type.GetType("System.Int32"));
                TabFaixa.Columns.Add("TOTAL_ATIVAO", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("TOTAL_ENTREGA", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("TOTGERAL_ENTREGA", Type.GetType("System.Decimal"));
                TabFaixa.Columns.Add("TREG_ATIVADO", Type.GetType("System.Int32"));

                string sSql = "SELECT T3.ID_VENDEDOR,T3.VENDEDOR,T3.ID_PREMIO,T1.P_COMISSAO,SUM(ISNULL(T1.VLRUNTCOMISSAO,0)*ISNULL(T1.QTDE,0)) AS TOTAL,SUM(ISNULL(T1.VLRCOMISSAO,0)) AS TOTALCOMISSAO, " +
                              "  SUM(CASE T1.NaoRentab  WHEN 1 THEN 0 ELSE ISNULL(T1.VLRUNTCOMISSAO,0)*ISNULL(T1.QTDE,0) END) AS TOTALMEDIA," +
                              "  SUM(CASE T1.NaoRentab  WHEN 1 THEN 0 ELSE ISNULL(T1.VLRCOMISSAO,0) END) AS MEDIACOMISSAO," +
                              "(SELECT COUNT(*) FROM MvVenda V1 " +
                              " WHERE V1.TpVenda in ('PV','VF') and isnull(V1.id_vdorigem,0)=0 " +
                              //"   AND (MONTH(V1.PREVENTREGA) = " + ((LstMes.SelectedIndex) + 1).ToString() + " AND YEAR(V1.PREVENTREGA) = " + TxtAno.Value.ToString() + ")" +
                              "   AND V1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND V1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "   AND V1.STATUS=3 " +
                              "   AND V1.Id_Vendedor=T3.Id_Vendedor " +
                              "   AND V1.cli_reativado=1 " +
                              "   AND T3.VlrReAtivClie > 0 " +
                              "   AND V1.CNPJCPF not in ('99999999999','99999999999999')" +
                              "   AND V1.VlrTotal >= T3.VlrReAtivClie) AS NREG_ATIVAO," +
                              "(SELECT COUNT(*) FROM MvVenda V1 " +
                              " WHERE V1.TpVenda in ('PV','VF') and isnull(V1.id_vdorigem,0)=0 " +
                              //"   AND (MONTH(V1.PREVENTREGA) = " + ((LstMes.SelectedIndex) + 1).ToString() + " AND YEAR(V1.PREVENTREGA) = " + TxtAno.Value.ToString() + ")" +
                              "   AND V1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND V1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "   AND V1.STATUS=3 " +
                              "   AND V1.Id_Vendedor in (" + CodVendedor + ") " +
                              "   AND V1.cli_reativado=1 " +
                              "   AND T3.VlrReAtivClie > 0 " +
                              "   AND V1.CNPJCPF not in ('99999999999','99999999999999')" +
                              "   AND V1.VlrTotal >= T3.VlrReAtivClie) AS TREG_ATIVADO," +
                              " (SELECT ISNULL(SUM(ISNULL(VLRTOTAL,0)),0) FROM MvVenda V1 " +
                              " WHERE V1.TpVenda in ('PV','VF') and isnull(V1.id_vdorigem,0)=0 " +
                              //"   AND (MONTH(V1.PREVENTREGA) = " + ((LstMes.SelectedIndex) + 1).ToString() + " AND YEAR(V1.PREVENTREGA) = " + TxtAno.Value.ToString() + ")" +
                              "   AND V1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND V1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "   AND V1.STATUS=3 " +
                              "   AND V1.Id_Vendedor=T3.Id_Vendedor " +
                              "   AND V1.cli_reativado=1" +
                              "   AND T3.VlrReAtivClie > 0 " +
                              "   AND V1.CNPJCPF not in ('99999999999','99999999999999')" +
                              "   AND V1.VlrTotal >= T3.VlrReAtivClie) AS TOTAL_ATIVAO," +
                              " (SELECT ISNULL(SUM(ISNULL(VLRTOTAL,0)),0) FROM MvVenda V1 " +
                              " WHERE V1.TpVenda in ('PV','VF') and isnull(V1.id_vdorigem,0)=0" +
                              "   AND (MONTH(V1.PREVENTREGA) = " + Dt1.Value.Date.Month.ToString() + " AND YEAR(V1.PREVENTREGA) = " + Dt1.Value.Date.Year.ToString() + "  AND DAY(V1.PREVENTREGA) <= 20)" +
                              "   AND V1.STATUS=3 " +
                              "   AND V1.Id_Vendedor=T3.Id_Vendedor) AS TOTAL_ENTREGA," +
                              " (SELECT ISNULL(SUM(ISNULL(VLRTOTAL,0)),0) FROM MvVenda V1 " +
                              " WHERE V1.TpVenda in ('PV','VF') and isnull(V1.id_vdorigem,0)=0" +
                              "   AND (MONTH(V1.PREVENTREGA) = " + Dt1.Value.Date.Month.ToString() + " AND YEAR(V1.PREVENTREGA) = " + Dt1.Value.Date.Year.ToString() + "  AND DAY(V1.PREVENTREGA) <= 20)" +
                              "   AND V1.STATUS=3" +
                              "   AND V1.Id_Vendedor in (" + CodVendedor + "))  AS TOTGERAL_ENTREGA "+ 
                              " FROM MVVENDAITENS T1 " +
                              "   LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                              "   LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                              "   LEFT JOIN PESSOAS T4 ON (T4.ID_PESSOA=T2.ID_PESSOA)" +
                              " WHERE T2.STATUS=3 AND T2.TPVENDA IN ('PV','VF')" +
                    //"   AND (MONTH(t2.PREVENTREGA) = " + ((LstMes.SelectedIndex) + 1).ToString() + " AND YEAR(t2.PREVENTREGA) = " + TxtAno.Value.ToString() + ")";
                              "   AND T2.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) and isnull(t2.id_vdorigem,0)=0 ";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (int.Parse(TxtCodCliente.Text) > 0)
                    sSql = sSql + " AND T2.Id_Pessoa=" + TxtCodCliente.Text;

                sSql = sSql + " GROUP BY T3.ID_VENDEDOR,T3.VENDEDOR,T3.ID_PREMIO,T3.VlrReAtivClie,T1.P_COMISSAO ORDER BY 1,4";

                //Atualizando a Tabela
                string sSQLVend="";
                if (CodVendedor == "0")
                    sSQLVend="SELECT * FROM VENDEDORES WHERE EntraRel=1";
                else
                    sSQLVend="SELECT * FROM VENDEDORES WHERE Id_Vendedor in (" + CodVendedor + ")";

                SqlDataReader LerSQL = Controle.ConsultaSQL(sSQLVend);                
                while (LerSQL.Read())
                    TabFaixa.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                                
                LerSQL = Controle.ConsultaSQL(sSql);
                bool FindLinha = false;
                decimal TotEntrega  = 0;
                decimal TRegAtivado = 0;
                while (LerSQL.Read())
                {

                    TotEntrega  = decimal.Parse(LerSQL["TOTGERAL_ENTREGA"].ToString()); ;
                    TRegAtivado = int.Parse(LerSQL["TREG_ATIVADO"].ToString());

                    FindLinha = false;
                    for (int I = 0; I <= TabFaixa.Rows.Count - 1; I++)
                    {
                        if (TabFaixa.Rows[I]["ID_Vendedor"].ToString().Trim() == LerSQL["ID_Vendedor"].ToString().Trim() && decimal.Parse(TabFaixa.Rows[I]["P_Comissao"].ToString().Trim()) == 0)
                        {
                            FindLinha = true;
                            TabFaixa.Rows[I]["P_COMISSAO"]       = decimal.Parse(LerSQL["P_COMISSAO"].ToString());
                            TabFaixa.Rows[I]["TOTAL"]            = decimal.Parse(LerSQL["TOTAL"].ToString());
                            TabFaixa.Rows[I]["TOTALCOMISSAO"]    = decimal.Parse(LerSQL["TOTALCOMISSAO"].ToString());
                            TabFaixa.Rows[I]["TOTALMEDIA"]       = decimal.Parse(LerSQL["TOTALMEDIA"].ToString());
                            TabFaixa.Rows[I]["MEDIACOMISSAO"]    = decimal.Parse(LerSQL["MEDIACOMISSAO"].ToString());
                            TabFaixa.Rows[I]["NREG_ATIVAO"]      = int.Parse(LerSQL["NREG_ATIVAO"].ToString());
                            TabFaixa.Rows[I]["TOTAL_ATIVAO"]     = decimal.Parse(LerSQL["TOTAL_ATIVAO"].ToString());
                            TabFaixa.Rows[I]["TOTAL_ENTREGA"]    = decimal.Parse(LerSQL["TOTAL_ENTREGA"].ToString());
                            TabFaixa.Rows[I]["TOTGERAL_ENTREGA"] = decimal.Parse(LerSQL["TOTGERAL_ENTREGA"].ToString());
                            TabFaixa.Rows[I]["TREG_ATIVADO"]     = int.Parse(LerSQL["TREG_ATIVADO"].ToString());
                            break;
                        }                        
                    }

                    if (!FindLinha)
                    {
                        TabFaixa.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), 0, decimal.Parse(LerSQL["P_COMISSAO"].ToString()), decimal.Parse(LerSQL["TOTAL"].ToString()), decimal.Parse(LerSQL["TOTALCOMISSAO"].ToString()), decimal.Parse(LerSQL["TOTALMEDIA"].ToString()), decimal.Parse(LerSQL["MEDIACOMISSAO"].ToString()), int.Parse(LerSQL["NREG_ATIVAO"].ToString()), decimal.Parse(LerSQL["TOTAL_ATIVAO"].ToString()), decimal.Parse(LerSQL["TOTAL_ENTREGA"].ToString()), decimal.Parse(LerSQL["TOTGERAL_ENTREGA"].ToString()), int.Parse(LerSQL["TREG_ATIVADO"].ToString()));
                    }
                }

                for (int I = 0; I <= TabFaixa.Rows.Count - 1; I++)
                {
                    if (decimal.Parse(TabFaixa.Rows[I]["TOTGERAL_ENTREGA"].ToString()) == 0)
                        TabFaixa.Rows[I]["TOTGERAL_ENTREGA"] = TotEntrega;

                    if (decimal.Parse(TabFaixa.Rows[I]["TREG_ATIVADO"].ToString()) == 0)
                        TabFaixa.Rows[I]["TREG_ATIVADO"] = TRegAtivado;
                }
                //Select d Resumo
                string sSql2 = "SELECT T1.P_COMISSAO,SUM(ISNULL(T1.VLRUNTCOMISSAO,0)*ISNULL(T1.QTDE,0)) AS TOTAL,SUM(ISNULL(T1.VLRCOMISSAO,0)) AS TOTALCOMISSAO FROM MVVENDAITENS T1 " +
                               " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                               " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                               " WHERE T2.STATUS=3 AND T2.TPVENDA IN ('PV','VF') and isnull(T2.id_vdorigem,0)=0" +
                               "   AND T2.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";
                               //"   AND (MONTH(T2.PREVENTREGA) = " + ((LstMes.SelectedIndex) + 1).ToString() + " AND YEAR(T2.PREVENTREGA) = " + TxtAno.Value.ToString() + ")";                             
                              
                if (CodVendedor == "0")
                    sSql2 = sSql2 + " AND T3.EntraRel=1";
                else
                    sSql2 = sSql2 + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                 if (int.Parse(TxtCodCliente.Text) > 0)
                     sSql2 = sSql2 + " AND T2.Id_Pessoa=" + TxtCodCliente.Text;

                sSql2 = sSql2 + " GROUP BY T1.P_COMISSAO ORDER BY 1";

                string sSql4 = "Select C3.COTAFINANCEIRA,C1.Id_Vendedor,C2.Grupo,C1.VlrCota,C1.VlrPremio,C1.VlrCota2,C1.VlrPremio2,C1.TpPremio,(Select sum(Isnull(T1.VlrUntReal,0)*T1.Qtde) from MvVendaItens t1 " +
                               " left join MvVenda t2 on (t2.id_venda=t1.id_venda) " +
                               " left join produtos t3 on (t3.id_produto=t1.id_produto) " +
                                " where t2.id_vendedor=C1.Id_Vendedor " +
                                "  and t2.TpVenda='PV' and t2.status=3" +
                                //"  AND (MONTH(T2.PREVENTREGA) = "+ ((LstMes.SelectedIndex)+1).ToString()+" AND YEAR(T2.PREVENTREGA) = "+TxtAno.Value.ToString()+")"+
                                "   AND T2.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) "+
                                "  and (t3.id_grupo=c1.id_grupo or t3.id_grupo=(Case when C1.Id_GrupoAux > 0 then C1.Id_GrupoAux else C1.Id_Grupo end))) as VlrVenda," +
                                " (Select sum(Isnull(T1.VlrUntReal,0)*T1.Qtde) from MvVendaItens t1  " +
                                "   left join MvVenda t2 on (t2.id_venda=t1.id_venda) " +
                                "   left join produtos t3 on (t3.id_produto=t1.id_produto) " +
                                "   LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T2.ID_VENDEDOR)";
                if (CodVendedor == "0")
                    sSql4 = sSql4 + " WHERE T4.EntraRel=1 ";
                else
                    sSql4 = sSql4 + " WHERE t2.Id_Vendedor in (" + CodVendedor + ")";

                sSql4 = sSql4 + "  and t2.TpVenda='PV' and t2.status=3 and isnull(t2.id_vdorigem,0)=0 " +
                                "   AND T2.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) "+
                               // "  AND (MONTH(T2.PREVENTREGA) = "+ ((LstMes.SelectedIndex)+1).ToString()+" AND YEAR(T2.PREVENTREGA) = "+TxtAno.Value.ToString()+")"+                              
                                "  and (t3.id_grupo=c1.id_grupo or t3.id_grupo=(Case when C1.Id_GrupoAux > 0 then C1.Id_GrupoAux else C1.Id_Grupo end))) as VlrVendaTotal " +
                                " From CotaGrupoPrdVendedor C1" +
                                "  left join GrupoProduto C2 on (C2.id_grupo=C1.id_grupo)" +
                                " LEFT JOIN Vendedores C3 ON (C3.Id_Vendedor=C1.Id_Vendedor)";

                if (CodVendedor == "0")
                    sSql4 = sSql4 + " WHERE C3.EntraRel=1";
                else
                    sSql4 = sSql4 + " WHERE C1.Id_Vendedor in (" + CodVendedor + ")";

                sSql4 = sSql4 + " Order by C1.Id_vendedor,C2.Grupo";

                string sSql5 = "SELECT T1.*, (SELECT COUNT(*) FROM MvVenda V1 WHERE V1.TpVenda in ('PV','VF') and isnull(V1.id_vdorigem,0)=0 " +                               
                               " AND V1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND V1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) "+
                               " AND V1.STATUS=3 AND V1.Id_Vendedor=T1.Id_Vendedor"+
                               " AND V1.cli_reativado=1 AND T1.VlrReAtivClie > 0 "+
                               " AND V1.CNPJCPF not in ('99999999999','99999999999999')"+
                               " AND V1.VlrTotal >= T1.VlrReAtivClie) AS NREG_ATIVAO FROM VENDEDORES T1 ";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelFaixaComissao Rel001 = new Relatorios.RelFaixaComissao();
                DataSet TabRel1 = new DataSet();
                DataSet TabRel2 = new DataSet();
                DataSet TabRel3 = new DataSet();
                DataSet TabRel4 = new DataSet();                
                TabRel1 = Controle.ConsultaTabela(sSql);
                TabRel2 = Controle.ConsultaTabela(sSql2);
                TabRel3 = Controle.ConsultaTabela(sSql5);
                TabRel4 = Controle.ConsultaTabela(sSql4);
                //Rel001.Database.Tables[0].SetDataSource(TabRel1.Tables[0]);

                Rel001.SetDataSource(TabFaixa);
                Rel001.Database.Tables[1].SetDataSource(TabRel2.Tables[0]);
                Rel001.Database.Tables[2].SetDataSource(TabRel3.Tables[0]);
                Rel001.Database.Tables[3].SetDataSource(TabRel4.Tables[0]);  
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                if (int.Parse(TxtCodCliente.Text) > 0)
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + " Cliente:" + TxtCliente.Text.Trim();
                else
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text  = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op02.Checked) // Gráfico Total da Vendas por Vendedor
            {
                string sSql = "SELECT T2.VENDEDOR,ISNULL(SUM(T1.VLRTOTAL),0) AS TOTAL FROM MVVENDA T1" +
                              " LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " WHERE T1.STATUS=3 AND T1.TPVENDA IN ('PV','VF') and isnull(t1.id_vdorigem,0)=0" +
                              "   AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + "  GROUP BY T2.VENDEDOR";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelGrafResumoVd Rel001 = new Relatorios.RelGrafResumoVd();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text  = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op03.Checked) // Demonstrativo de Vendas Anual por Vendedor
            {
                //Atualizando a Tabela
                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("ID_VENDEDOR", Type.GetType("System.Int32"));
                Tab01.Columns.Add("VENDEDOR", Type.GetType("System.String"));
                Tab01.Columns.Add("MES", Type.GetType("System.Int32"));
                Tab01.Columns.Add("TOTAL", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("AnoAnterior", Type.GetType("System.Decimal"));

                string sSql = "SELECT T1.ID_VENDEDOR,T2.VENDEDOR,MONTH(T1.PREVENTREGA) AS MES,isnull(SUM(ISNULL(T1.VLRTOTAL,0)),0) AS AnoAnterior,0 as total" +
                              " FROM MVVENDA T1 " +
                              "   LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                              " WHERE T1.STATUS=3 AND T1.TPVENDA IN ('PV','VF') and IsNull(T1.Id_VdOrigem,0)=0 " +
                              "  AND YEAR(T1.PREVENTREGA)=" + (TxtAno.Value - 1).ToString();
                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                sSql = sSql + "  GROUP BY T1.ID_VENDEDOR,T2.VENDEDOR,MONTH(T1.PREVENTREGA)";
                sSql = sSql + " UNION ";
                sSql = sSql + "SELECT T1.ID_VENDEDOR,T2.VENDEDOR,MONTH(T1.PREVENTREGA) AS MES,0 AS AnoAnterior, isnull(SUM(ISNULL(T1.VLRTOTAL,0)),0) AS Total" +
                           " FROM MVVENDA T1 " +
                           "   LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                           " WHERE T1.STATUS=3 AND T1.TPVENDA IN ('PV','VF') and IsNull(T1.Id_VdOrigem,0)=0 " +
                           "  AND YEAR(T1.PREVENTREGA)=" + (TxtAno.Value).ToString();
                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                sSql = sSql + "  GROUP BY T1.ID_VENDEDOR,T2.VENDEDOR,MONTH(T1.PREVENTREGA) ORDER BY 2,3";

                SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);
                bool FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;                    
                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {

                        if (Tab01.Rows[I]["ID_Vendedor"].ToString().Trim() == LerSQL["ID_Vendedor"].ToString().Trim() && Tab01.Rows[I]["MES"].ToString().Trim() == LerSQL["MES"].ToString().Trim())
                        {
                            FindLinha = true;
                            if (int.Parse(LerSQL["MES"].ToString().Trim()) >= DateTime.Now.Date.Month && (TxtAno.Value).ToString() == DateTime.Now.Date.Year.ToString().Trim())
                            {
                                Tab01.Rows[I]["AnoAnterior"] = decimal.Parse(Tab01.Rows[I]["AnoAnterior"].ToString()) + decimal.Parse("0,0");
                                //Tab01.Rows[I]["AnoAnterior"] = decimal.Parse(Tab01.Rows[I]["AnoAnterior"].ToString()) + decimal.Parse(LerSQL["AnoAnterior"].ToString());
                            }
                            else
                            {
                                Tab01.Rows[I]["TOTAL"] = decimal.Parse(Tab01.Rows[I]["TOTAL"].ToString()) + decimal.Parse(LerSQL["Total"].ToString());
                                Tab01.Rows[I]["AnoAnterior"] = decimal.Parse(Tab01.Rows[I]["AnoAnterior"].ToString()) + decimal.Parse(LerSQL["AnoAnterior"].ToString());
                            }
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        if (int.Parse(LerSQL["MES"].ToString().Trim()) >= DateTime.Now.Date.Month && (TxtAno.Value).ToString() == DateTime.Now.Date.Year.ToString().Trim())
                            //Tab01.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), int.Parse(LerSQL["MES"].ToString()), 0, decimal.Parse(LerSQL["AnoAnterior"].ToString()));
                            Tab01.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), int.Parse(LerSQL["MES"].ToString()), 0, 0);
                        else
                        {
                            Tab01.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), int.Parse(LerSQL["MES"].ToString()), decimal.Parse(LerSQL["TOTAL"].ToString()), decimal.Parse(LerSQL["AnoAnterior"].ToString()));
                        }
                    }
                    /*for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {
                        if (Tab01.Rows[I]["ID_Vendedor"].ToString().Trim() == LerSQL["ID_Vendedor"].ToString().Trim() && Tab01.Rows[I]["MES"].ToString().Trim() == LerSQL["MES"].ToString().Trim())
                        {
                            FindLinha = true;                            
                            Tab01.Rows[I]["TOTAL"]       = decimal.Parse(Tab01.Rows[I]["TOTAL"].ToString()) + decimal.Parse(LerSQL["Total"].ToString());
                            Tab01.Rows[I]["AnoAnterior"] = decimal.Parse(Tab01.Rows[I]["AnoAnterior"].ToString()) + decimal.Parse(LerSQL["AnoAnterior"].ToString());
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        Tab01.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), int.Parse(LerSQL["MES"].ToString()), decimal.Parse(LerSQL["TOTAL"].ToString()), decimal.Parse(LerSQL["AnoAnterior"].ToString()));
                    }*/
                }


                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelAnualVdVendedor Rel001 = new Relatorios.RelAnualVdVendedor();
                DataSet TabRel = new DataSet();
                //TabRel = Controle.ConsultaTabela(sSql);
                //Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Ano: " + TxtAno.Value.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblAnoAnterior"])).Text = "Ano: " + (TxtAno.Value - 1).ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblAnoAtual"])).Text = "Ano: " + TxtAno.Value.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op04.Checked) // Resumo do Mapa de Entrega por Veiculo
            {
                string sSql = "select t2.data,rtrim(t4.Veiculo)+' / '+t4.Placa as Veiculo,t3.Entregador,COUNT(*) AS CARGA,COUNT(CASE T1.STATUS WHEN 1 THEN 1 END) AS ENTREGUE,COUNT(CASE T1.STATUS WHEN 2 THEN 1 END) AS RETORNO," +
                              " SUM(T5.VLRTOTAL) AS TOTAL,SUM(CASE T1.STATUS WHEN 2 THEN T5.VLRTOTAL ELSE 0 END) AS VLRRETORNO from MapaEntregaItens T1" +
                              "  left join MapaEntrega t2 on (t2.Id_Mapa=t1.Id_Mapa)" +
                              "  left join Entregadores t3 on (t3.Id_Entregador=t2.Id_Entregador)" +
                              "  left join Veiculos t4 on (t4.Id_Veiculo=t2.Id_Veiculo)" +
                              "  left join MvVenda t5 on (t5.Id_Venda=t1.Id_Venda) " +
                              " WHERE T4.VLRCARGA > 0 AND T2.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (LstVeiculo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T4.Id_Veiculo=" + LstVeiculo.SelectedValue.ToString();

                sSql = sSql + " GROUP BY t2.data,t4.Veiculo,t4.Placa,t3.Entregador";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoMapaEntVeiculo Rel001 = new Relatorios.RelResumoMapaEntVeiculo();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text  = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op05.Checked) // Resumo do Mapa de Entrega por Entregador
            {
                string sSql = "select t2.data,t3.Entregador,rtrim(t4.Veiculo)+' / '+t4.Placa as Veiculo,COUNT(*) AS CARGA,COUNT(CASE T1.STATUS WHEN 1 THEN 1 END) AS ENTREGUE,COUNT(CASE T1.STATUS WHEN 2 THEN 1 END) AS RETORNO," +
                              " SUM(T5.VLRTOTAL) AS TOTAL,SUM(CASE T1.STATUS WHEN 2 THEN T5.VLRTOTAL ELSE 0 END) AS VLRRETORNO from MapaEntregaItens T1" +
                              "  left join MapaEntrega t2 on (t2.Id_Mapa=t1.Id_Mapa)" +
                              "  left join Entregadores t3 on (t3.Id_Entregador=t2.Id_Entregador)" +
                              "  left join Veiculos t4 on (t4.Id_Veiculo=t2.Id_Veiculo)" +
                              "  left join MvVenda t5 on (t5.Id_Venda=t1.Id_Venda)" +
                              " WHERE T2.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (LstEntregador.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T3.Id_Entregador=" + LstEntregador.SelectedValue.ToString();
                sSql = sSql + " GROUP BY t2.data,t3.Entregador,t4.Veiculo,t4.Placa";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoMapaEntEntregador Rel001 = new Relatorios.RelResumoMapaEntEntregador();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text  = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op06.Checked) // Lista de Clientes novos e reativados
            {
                string sSql = "SELECT T2.VENDEDOR,T1.DATA,T1.Pessoa,T1.PrevEntrega,T1.Id_Venda,T1.NumDocumento,T1.VlrTotal,T3.Usuario," +
                              "(SELECT TOP 1 V1.Data FROM MvVenda V1 WHERE V1.TpVenda='PV' AND V1.Status=3 AND V1.Id_Venda < t1.ID_VENDA AND V1.Id_Pessoa=t1.ID_pessoa order by V1.id_venda desc) as DtUltCompra,T4.ENTREGADOR FROM MvVenda T1" +
                              " LEFT JOIN Vendedores T2 ON (T2.Id_Vendedor=T1.Id_Vendedor)" +
                              " Left join Usuarios t3 on (t3.Id_Usuario=t1.Id_Usuario)" +
                              " LEFT JOIN ENTREGADORES T4 ON (T4.ID_ENTREGADOR=T1.ID_ENTREGADOR) " +
                              " WHERE T1.cli_reativado=1 and t1.status=3" +
                              //" WHERE t1.status=3" +
                              " and t1.VlrTotal >= " + TxtVllrMinimo.Value.ToString().Replace(",",".") +
                              " and t1.CNPJCPF not in ('99999999999','99999999999999')" +
                              " and T1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) and isnull(T1.id_vdorigem,0)=0";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " ORDER BY T2.Vendedor,T1.DATA,T1.Pessoa";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelClientesReativados Rel001 = new Relatorios.RelClientesReativados();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString() + "  Valor minimo do Pedido:" + TxtVllrMinimo.Value.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op07.Checked) // Lista de Clientes novos e reativados telemarkting
            {
                string sSql = "SELECT T1.Usuario,t4.usuario as digitador,T3.VENDEDOR,T2.DATA,T2.Pessoa,T2.PrevEntrega,T2.Id_Venda,T2.NumDocumento,T2.VlrTotal,"+
                              "(SELECT TOP 1 V1.Data FROM MvVenda V1 WHERE V1.TpVenda='PV' AND V1.Status=3 AND V1.Id_Venda < t2.ID_VENDA AND V1.Id_Pessoa=t2.ID_pessoa order by V1.id_venda desc) as DtUltCompra,T5.ENTREGADOR FROM Usuarios t1" +
                              "  LEFT JOIN MvVenda t2 ON (T2.Id_Usuario=T1.Id_Usuario OR t2.Id_Vendedor IN (SELECT T5.Id_Vendedor FROM Vendedores T5 WHERE T5.ID_USUARIO=t1.Id_Usuario)) " +
                              "  LEFT JOIN Vendedores T3 ON (T3.Id_Vendedor=T2.Id_Vendedor and t3.id_usuario=t1.id_usuario) " +
                              "  Left join Usuarios t4 on (t4.Id_Usuario=t2.Id_Usuario)" +
                              "  LEFT JOIN ENTREGADORES T5 ON (T5.ID_ENTREGADOR=T2.ID_ENTREGADOR)"+
                              " WHERE T2.cli_reativado=1 and t2.status=3 and isnull(t2.id_vdorigem,0)=0" +
                              //"  and t2.VlrTotal >= 150 and t2.VlrTotal < 300" +
                              "  and t2.VlrTotal >= 400" +
                              "  and t2.CNPJCPF not in ('99999999999','99999999999999')" +
                              "  and T2.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) " +
                              "  and t1.telemarketing=1" +
                              "  AND t2.Id_Vendedor IN (SELECT T5.Id_Vendedor FROM Vendedores T5 WHERE T5.ID_USUARIO=t1.Id_Usuario)";


                if (LstUsuario.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_Usuario=" + LstUsuario.SelectedValue.ToString();
    
                sSql = sSql + "order by T1.Usuario,T3.Vendedor,t2.data,T2.Pessoa";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelClientesReativadosTelem Rel001 = new Relatorios.RelClientesReativadosTelem();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op08.Checked) // Clientes com Comodato Pendente
            {
                string sSql = "SELECT T4.VENDEDOR,T1.RazaoSocial,T1.Fantasia,T1.Fone,T1.Celular,T1.Contato," +
                              " (SELECT TOP 1 DATA FROM MvVenda T3 " +
                              "   WHERE T3.Status=3 AND T3.TpVenda='PV' AND T3.Id_Pessoa=T1.Id_Pessoa ORDER BY T3.DATA DESC) as DtUltCompra FROM Pessoas T1 " +
                              " LEFT JOIN Vendedores T4 ON (T4.ID_VENDEDOR=T1.Id_Vendedor) " +
                              " WHERE T1.comodato=1 and not exists (SELECT * FROM MvVenda T2 WHERE T2.Id_Pessoa=T1.Id_Pessoa " +
                              " AND T2.Status in (2,3) and T2.TpVenda='PV' and MONTH(T2.PrevEntrega)=" + ((LstMes.SelectedIndex)+1).ToString() + " and YEAR(T2.PrevEntrega)=" + TxtAno.Value.ToString() + ")";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T4.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";


                sSql = sSql + " ORDER BY T4.Vendedor,7";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelComodatoPend Rel001 = new Relatorios.RelComodatoPend();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Ano:"+TxtAno.Value.ToString()+" Mes: "+LstMes.SelectedItem.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op09.Checked) // Clientes com Comodato Pendente por Item
            {
                int UltDia = DateTime.DaysInMonth(int.Parse(TxtAno.Value.ToString()),int.Parse(((LstMes.SelectedIndex) + 1).ToString()));
                DateTime DtBase = DateTime.Parse(UltDia.ToString() + "/" + ((LstMes.SelectedIndex) + 1).ToString() + "/" + TxtAno.Value.ToString());
                string sSql = "SELECT T5.VENDEDOR,T1.ID_PESSOA,T2.CNPJ,T2.RazaoSocial,T3.Id_Grade,T4.Grade,SUM(T1.QTDE) AS QTDEEQUIP,SUM(T1.QTDE*T4.Qtde) AS Cota," +
                              " Isnull((select Isnull(SUM(V1.Qtde * Case Isnull(V3.QtdeUnd,0) when 0 then 1 else V3.QtdeUnd end),0) from MvVendaItens V1" +
                              " left join MvVenda V2 on (V2.Id_Venda=V1.Id_Venda) " +
                              " left join Produtos V3 on (V3.Id_produto=V1.Id_Produto) " +
                              " where V2.TpVenda='PV' and V2.Status=3 " +
                              "  and V2.Id_Pessoa=T1.Id_Pessoa" +
                              "  and month(V2.Data)=" + ((LstMes.SelectedIndex) + 1).ToString() + " and year(V2.Data)=" + TxtAno.Value.ToString() +
                              "  and V1.Id_Produto in (Select I1.Id_Produto From GradeComodatoVinc I1 where I1.Id_Grade=T3.Id_Grade)),0) as QtdeCompra," +
                              " (select top 1 Data From MvVenda where Id_Pessoa=T1.Id_Pessoa and Status=3 and TpVenda='PV'" +
                              "   AND convert(datetime,Data,103) <= convert(datetime,'" + DtBase.ToShortDateString() + "',103) order by Id_Venda Desc) as DtUltCompra, " +
                              " (select top 1 Data From MvVendaItens I1 left join MvVenda I2 on (I2.Id_Venda=I1.Id_Venda)" +
                              "   where I2.Id_Pessoa=T1.Id_Pessoa and I2.Status=3 and I2.TpVenda='PV' " +
                              "   AND convert(datetime,I2.Data,103) <= convert(datetime,'"+DtBase.ToShortDateString()+"',103) "+                              
                              "   and I1.Id_Produto in (Select I1.Id_Produto From GradeComodatoVinc I1 where I1.Id_Grade=T3.Id_Grade) order by I2.Id_Venda Desc) as UltCompComodato " +
                              " FROM ClientesPrdComodato T1" +
                              "   LEFT JOIN Pessoas T2 ON (T2.Id_Pessoa=T1.Id_Pessoa)  " +
                              "   LEFT JOIN GradeComodatoItens T3 ON (T3.Id_Produto=T1.Id_Produto)" +
                              "   LEFT JOIN GradeComodato T4 ON (T4.Id_Grade=T3.Id_Grade)" +
                              "   LEFT JOIN Vendedores T5 ON (T5.Id_Vendedor=T2.Id_Vendedor)" +
                              " WHERE T2.Comodato=1 AND" +
                              "  (Isnull((select Isnull(SUM(V1.Qtde * Case Isnull(V3.QtdeUnd,0) when 0 then 1 else V3.QtdeUnd end),0) from MvVendaItens V1" +
                              "    left join MvVenda V2 on (V2.Id_Venda=V1.Id_Venda) " +
                              "    left join Produtos V3 on (V3.Id_produto=V1.Id_Produto) " +
                              "   where V2.TpVenda='PV' " +
                              "    and V2.Status=3 " +
                              "    and V2.Id_Pessoa=T1.Id_Pessoa" +
                              "    and month(V2.Data)=" + ((LstMes.SelectedIndex) + 1).ToString() + " and year(V2.Data)=" + TxtAno.Value.ToString() +
                              "    and V1.Id_Produto in (Select I1.Id_Produto From GradeComodatoVinc I1 where I1.Id_Grade=T3.Id_Grade)),0)" +
                              "  < Isnull((SELECT SUM(C1.QTDE*C4.qtde) FROM ClientesPrdComodato C1 " +
                              "      LEFT JOIN GradeComodatoItens C3 ON (C3.Id_Produto=T1.Id_Produto) " +
                              "      LEFT JOIN GradeComodato C4 ON (C4.Id_Grade=T3.Id_Grade)" +
                              "      WHERE C1.Id_Lanc=T1.Id_Lanc and C1.ID_PESSOA=T2.Id_Pessoa AND C3.ID_GRADE=T4.ID_GRADE),0))";
                         
                if (CodVendedor != "0")
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " GROUP BY T5.VENDEDOR,T1.ID_PESSOA,T2.CNPJ,T2.RazaoSocial,T3.Id_Grade,T4.Grade ORDER BY T5.VENDEDOR,T2.RazaoSocial,T4.Grade";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelComodatoPendItens Rel001 = new Relatorios.RelComodatoPendItens();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Ano:" + TxtAno.Value.ToString() + " Mes: " + LstMes.SelectedItem.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op10.Checked) // Demonstrativo do Faturamento Anual
            {
                
                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("ID_VENDEDOR", Type.GetType("System.Int32"));
                Tab01.Columns.Add("VENDEDOR", Type.GetType("System.String"));
                Tab01.Columns.Add("MES", Type.GetType("System.Int32"));
                Tab01.Columns.Add("TOTAL", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("AnoAnterior", Type.GetType("System.Decimal"));

                string sSql = "SELECT 0 as ID_Vendedor,'' as Vendedor,MONTH(T1.PREVENTREGA) AS MES,isnull(SUM(ISNULL(T1.VLRTOTAL,0)),0) AS AnoAnterior,0 as total" +
                              " FROM MVVENDA T1 " +
                              "   LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                              " WHERE T1.STATUS=3 AND T1.TPVENDA IN ('PV','VF') and IsNull(T1.Id_VdOrigem,0)=0 " +
                              "  AND YEAR(T1.PREVENTREGA)=" + (TxtAno.Value - 1).ToString();
                if (CodVendedor != "0")
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + "  GROUP BY T1.ID_VENDEDOR,T2.VENDEDOR,MONTH(T1.PREVENTREGA)";
                sSql = sSql + " UNION ";
                sSql = sSql + "SELECT 0 as ID_Vendedor,'' as Vendedor,MONTH(T1.PREVENTREGA) AS MES,0 AS AnoAnterior, isnull(SUM(ISNULL(T1.VLRTOTAL,0)),0) AS Total" +
                           " FROM MVVENDA T1 " +
                           "   LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                           " WHERE T1.STATUS=3 AND T1.TPVENDA IN ('PV','VF') and IsNull(T1.Id_VdOrigem,0)=0 " +
                           "  AND YEAR(T1.PREVENTREGA)=" + (TxtAno.Value).ToString();
                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";
                sSql = sSql + "  GROUP BY T1.ID_VENDEDOR,T2.VENDEDOR,MONTH(T1.PREVENTREGA) ORDER BY 2,3";

                SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);
                bool FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;
                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {                        

                        if (Tab01.Rows[I]["ID_Vendedor"].ToString().Trim() == LerSQL["ID_Vendedor"].ToString().Trim() && Tab01.Rows[I]["MES"].ToString().Trim() == LerSQL["MES"].ToString().Trim())
                        {
                            FindLinha = true;
                            if (int.Parse(LerSQL["MES"].ToString().Trim()) >= DateTime.Now.Date.Month && (TxtAno.Value).ToString() == DateTime.Now.Date.Year.ToString().Trim())
                            {                             
                                Tab01.Rows[I]["AnoAnterior"] = decimal.Parse(Tab01.Rows[I]["AnoAnterior"].ToString()) + decimal.Parse("0,0");
                                //Tab01.Rows[I]["AnoAnterior"] = decimal.Parse(Tab01.Rows[I]["AnoAnterior"].ToString()) + decimal.Parse(LerSQL["AnoAnterior"].ToString());
                            }
                            else
                            {
                                Tab01.Rows[I]["TOTAL"] = decimal.Parse(Tab01.Rows[I]["TOTAL"].ToString()) + decimal.Parse(LerSQL["Total"].ToString());
                                Tab01.Rows[I]["AnoAnterior"] = decimal.Parse(Tab01.Rows[I]["AnoAnterior"].ToString()) + decimal.Parse(LerSQL["AnoAnterior"].ToString());
                            }
                            break;
                        }
                    }
                    if (!FindLinha)
                    {
                        if (int.Parse(LerSQL["MES"].ToString().Trim()) >= DateTime.Now.Date.Month && (TxtAno.Value).ToString() == DateTime.Now.Date.Year.ToString().Trim())
                            //Tab01.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), int.Parse(LerSQL["MES"].ToString()), 0, decimal.Parse(LerSQL["AnoAnterior"].ToString()));
                            Tab01.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), int.Parse(LerSQL["MES"].ToString()), 0, 0);
                        else
                        {
                            Tab01.Rows.Add(int.Parse(LerSQL["ID_Vendedor"].ToString()), LerSQL["Vendedor"].ToString(), int.Parse(LerSQL["MES"].ToString()), decimal.Parse(LerSQL["TOTAL"].ToString()), decimal.Parse(LerSQL["AnoAnterior"].ToString()));
                        }
                    }
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelAnualVdVendedor Rel001 = new Relatorios.RelAnualVdVendedor();
                DataSet TabRel = new DataSet();
                //TabRel = Controle.ConsultaTabela(sSql);
                //Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["Text20"])).Text = "Demonstrativo do Faturamento Anual";
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["Text5"])).Text = "";
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["Text9"])).Text = "Total Geral R$:";
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Ano: " + TxtAno.Value.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblAnoAnterior"])).Text = "Ano: " + (TxtAno.Value - 1).ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblAnoAtual"])).Text = "Ano: " + TxtAno.Value.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.Section)(Rel001.ReportDefinition.Sections["Section4"])).SectionFormat.EnableSuppress = true;
                
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op11.Checked) // Clientes Pre Inativos
            {
                string sSql = "select t2.Vendedor,T1.RazaoSocial,t1.Cnpj,t1.Fone,t1.Celular,t1.Contato," +
                              " (Select Top 1 Data from MvVenda where Id_Pessoa=t1.Id_Pessoa and TpVenda='PV' and Status=3 order by Data desc) as UltCompra" +
                              " from Pessoas t1  " +
                              "  left join Vendedores t2 on (t2.Id_Vendedor=t1.Id_Vendedor)" +
                              " where t1.Ativo=1 " +
                              " and exists (SELECT TOP 1 * FROM MVVENDA " +
                              " WHERE TPVENDA IN ('PV','VF') " +
                              " AND ID_PESSOA=t1.Id_Pessoa" +
                              " AND STATUS=3 AND DATA > CONVERT(DATETIME,CONVERT(CHAR,GETDATE()-90,103),103))" +
                              " and not exists (SELECT TOP 1 * FROM MVVENDA " +
                              " WHERE TPVENDA IN ('PV','VF') " +
                              "  AND ID_PESSOA=t1.Id_Pessoa" +
                              "  AND STATUS=3 AND DATA > CONVERT(DATETIME,CONVERT(CHAR,GETDATE()-60,103),103))";
                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " ORDER BY T2.VENDEDOR,T1.RazaoSocial";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelClientesPreInativo Rel001 = new Relatorios.RelClientesPreInativo();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op12.Checked) // Resumo das Vendas por Vendedor
            {
                string sSql = "SELECT T2.ID_VENDEDOR,T2.VENDEDOR,T3.Entregador,ISNULL(SUM(T1.VLRTOTAL),0) AS TOTAL,COUNT(*) as NReg," +
                              "(SELECT COUNT(*) FROM PESSOAS P1 WHERE P1.ID_PESSOA IN (SELECT DISTINCT V1.ID_PESSOA FROM MvVenda V1 " +
                              " WHERE  V1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND V1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) "+
                              "  AND  V1.Id_Vendedor=T2.ID_VENDEDOR"+
                              "  AND V1.STATUS=3 AND V1.TPVENDA IN ('PV','VF'))) AS NClientes "+
                              "FROM MVVENDA T1" +
                              " LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                              " LEFT JOIN Entregadores T3 ON (T3.Id_Entregador=T1.Id_Entregador)" +
                              " WHERE T1.STATUS=3 AND T1.TPVENDA IN ('PV','VF') and isnull(T1.id_vdorigem,0)=0" +
                              "  and T1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";

                if (CodVendedor == "0")
                    sSql = sSql + " AND T2.EntraRel=1";
                else
                    sSql = sSql + " AND T1.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + " GROUP BY T2.ID_VENDEDOR,T2.VENDEDOR,T3.Entregador order by T2.VENDEDOR";

                string sSql2 = "SELECT '1' AS TIPO,t2.Id_Vendedor,t3.vendedor,T4.Referencia,T4.Descricao,SUM(T1.Qtde) AS QTDE, SUM(T1.VLRUNTREAL*T1.QTDE) AS TOTAL FROM MvVendaItens T1 " +
                             " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                             " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR)" +
                             " LEFT JOIN Produtos T4 ON (T4.Id_Produto=T1.Id_Produto)" +
                             " WHERE T2.STATUS=3 AND T2.TPVENDA IN ('PV','VF')" +
                             "  and T2.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) and isnull(t2.id_vdorigem,0)=0 ";
                
                if (CodVendedor == "0")
                    sSql2 = sSql2 + " AND T3.EntraRel=1";
                else
                    sSql2 = sSql2 + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                string sSql3 = sSql2 + " GROUP BY t2.id_vendedor,t3.vendedor,T4.Referencia,T4.Descricao ORDER BY 6 DESC";

                sSql2 = sSql2 + " GROUP BY t2.id_vendedor,t3.vendedor,T4.Referencia,T4.Descricao ORDER BY 7 DESC";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelFichaVdVendedor Rel001 = new Relatorios.RelFichaVdVendedor();
                DataSet TabRel = new DataSet();
                DataSet TabRel1 = new DataSet();
                DataSet TabRel2 = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                TabRel1 = Controle.ConsultaTabela(sSql2);
                TabRel2 = Controle.ConsultaTabela(sSql3);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                Rel001.Database.Tables[1].SetDataSource(TabRel1.Tables[0]);
                Rel001.Database.Tables[2].SetDataSource(TabRel2.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op13.Checked) // Resumo das Vendas dos Produtos em Promoção
            {
                string sSql = "SELECT T5.Vendedor,T4.Descricao AS Promocao,T3.REFERENCIA,T3.Descricao,SUM(T1.Qtde) AS QTDE,SUM(T1.QTDE*T1.VlrUnitario) AS TOTAL FROM MvVendaItens T1" +
                              " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                              " LEFT JOIN Produtos T3 ON (T3.Id_Produto=T1.Id_Produto)" +
                              " LEFT JOIN PromocaoProdutos T4 ON (T4.Id_Promocao=T1.Id_Promocao)" +
                              " LEFT JOIN Vendedores T5 ON (T5.Id_Vendedor=T2.Id_Vendedor) " +
                              " WHERE T1.Id_Promocao > 0 AND T2.Status=3 AND T2.TpVenda='PV' " +
                              "  and T2.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) and isnull(t2.id_vdorigem,0)=0";
                if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";
                if (LstPromocao.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_Promocao=" + LstPromocao.SelectedValue.ToString();

                sSql = sSql + " GROUP BY T5.Vendedor,T4.Descricao,T3.REFERENCIA,T3.Descricao";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelResumoVdPromocao Rel001 = new Relatorios.RelResumoVdPromocao();
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
            if (Op14.Checked) // ÇListagem das Vendas dos Produtos em Promoção
            {
                string sSql = "SELECT T5.Vendedor,T4.Descricao AS Promocao,T3.REFERENCIA,T3.Descricao,T2.DATA,T2.PREVENTREGA,T2.Id_Venda,t2.pessoa,T1.Qtde,T1.VlrUnitario FROM MvVendaItens T1" +
                              " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                              " LEFT JOIN Produtos T3 ON (T3.Id_Produto=T1.Id_Produto)" +
                              " LEFT JOIN PromocaoProdutos T4 ON (T4.Id_Promocao=T1.Id_Promocao)" +
                              " LEFT JOIN Vendedores T5 ON (T5.Id_Vendedor=T2.Id_Vendedor) " +
                              " WHERE T1.Id_Promocao > 0 AND T2.Status=3 AND T2.TpVenda='PV' " +
                              "  and T2.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) and isnull(t2.id_vdorigem,0)=0";

                      if (CodVendedor == "0")
                    sSql = sSql + " AND T5.EntraRel=1";
                else
                    sSql = sSql + " AND T2.Id_Vendedor in (" + CodVendedor + ")";

                if (LstPromocao.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_Promocao=" + LstPromocao.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.Id_Produto=" + TxtCodPrd.Text;
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelVendaPromocao Rel001 = new Relatorios.RelVendaPromocao();
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

            if (Op15.Checked) // 
            {
                string sSql = "SELECT DISTINCT T1.DATA,T3.VENDEDOR,T3.Financeiro4," +
                              " (SELECT ISNULL(SUM(V1.VLRTOTAL),0) FROM MvVenda V1 " +
                              "   WHERE V1.Status=1 AND V1.TpVenda='PV' and isnull(v1.id_vdorigem,0)=0 " +
                              "    AND V1.Data=T1.DATA" +
                              "    AND V1.Id_Vendedor=T3.Id_Vendedor) AS CONFIRMADO," +
                              " (SELECT ISNULL(SUM(V1.VLRTOTAL),0) FROM MvVenda V1 " +
                              "   WHERE V1.Status=2 AND V1.TpVenda='PV' and isnull(v1.id_vdorigem,0)=0  " +
                              "    AND V1.Data=T1.DATA" +
                              "    AND V1.Id_Vendedor=T3.Id_Vendedor) AS FATURADO," +
                              " (SELECT ISNULL(SUM(V1.VLRTOTAL),0) FROM MvVenda V1 " +
                              "   WHERE V1.Status=3 and isnull(v1.id_vdorigem,0)=0  " +
                              "   AND V1.TpVenda='PV' " +
                              "   AND V1.PrevEntrega=T1.DATA" +
                              "   AND V1.Id_Vendedor=T3.Id_Vendedor) AS ENTREGAS," +
                              " (SELECT ISNULL(COUNT(DISTINCT V1.Id_Pessoa),0) FROM MvVenda V1 " +
                              "  WHERE V1.Status IN (1,2,3) and isnull(v1.id_vdorigem,0)=0 " +
                              "   AND V1.TpVenda='PV' " +
                              "   AND V1.Id_Pessoa<>(SELECT Id_Consumidor FROM Parametros WHERE Id_Filial=" + FrmPrincipal.IdFilialConexao.ToString() + ")" +
                              "   AND V1.Data=T1.DATA" +
                              "   AND V1.Id_Vendedor=T3.Id_Vendedor) AS CLIECAD," +
                              " (SELECT ISNULL(COUNT(*),0) FROM MvVenda V1 " +
                              "   WHERE V1.Status IN (1,2,3) and isnull(v1.id_vdorigem,0)=0  " +
                              "    AND V1.TpVenda='PV' " +
                              "    AND V1.Id_Pessoa=(SELECT Id_Consumidor FROM Parametros WHERE Id_Filial=" + FrmPrincipal.IdFilialConexao.ToString() + ")" +
                              "    AND V1.Data=T1.DATA" +
                              "    AND V1.Id_Vendedor=T3.Id_Vendedor) AS CLIEBALCAO," +
                              " (SELECT ISNULL(COUNT(*),0) FROM MvVenda V1 " +
                              "   WHERE V1.Status IN (3) and isnull(v1.id_vdorigem,0)=0 " +
                              "    AND V1.TpVenda='PV'" +
                              "    AND V1.Cli_Reativado=1" +
                              "    AND V1.VlrTotal >= T3.VlrReAtivClie" +
                              "    AND V1.Data=T1.DATA" +
                              "    AND V1.Id_Vendedor=T3.Id_Vendedor) AS CLIEATIVO," +
                              "  (SELECT COUNT(*) FROM DATAS D1 WHERE D1.ATIVO=1 AND MONTH(D1.DATA)=" + ((LstMes.SelectedIndex) + 1).ToString() + " AND YEAR(D1.Data)=" + TxtAno.Value.ToString() + ") AS DIASMES" +
                              " FROM DATAS T1  LEFT JOIN MVVENDA T2 ON (T2.DATA=T1.DATA) LEFT JOIN Vendedores T3 ON (T3.ATIVO=1)" +
                              "  WHERE MONTH(T1.DATA)=" + ((LstMes.SelectedIndex) + 1).ToString() + " AND YEAR(T1.DATA)=" + TxtAno.Value.ToString() + " AND T1.Ativo=1";
                              
                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T3.Id_Vendedor in (" + CodVendedor + ")";
                
                sSql = sSql + " ORDER BY T3.Vendedor,T1.DATA ";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelAcompDiarioVd Rel001 = new Relatorios.RelAcompDiarioVd();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Ano:" + TxtAno.Value.ToString() + " Mes: " + LstMes.SelectedItem.ToString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }

            if (Op16.Checked) // Rentabilidade Diaria
            {

                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("VENDEDOR", Type.GetType("System.String"));
                Tab01.Columns.Add("DATA", Type.GetType("System.DateTime"));
                Tab01.Columns.Add("STATUS", Type.GetType("System.Int32"));
                Tab01.Columns.Add("Confirmado", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("Faturado", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("Entregue", Type.GetType("System.Decimal"));
                Tab01.Columns.Add("TotalComissao", Type.GetType("System.Decimal"));

                string sSql = "SELECT T3.VENDEDOR,T2.DATA,T2.STATUS,SUM(ISNULL(T1.VLRUNTCOMISSAO,0)*ISNULL(T1.QTDE,0)) AS TOTAL,SUM(ISNULL(T1.VLRCOMISSAO,0)) AS TOTALCOMISSAO FROM MvVendaItens T1"+
                              "  LEFT JOIN MvVenda T2 ON (T2.ID_VENDA=T1.ID_VENDA)"+
                              "  LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR)"+
                              " WHERE T2.TpVenda='PV' AND T2.Status IN (1,2,3) " +
                              "  and T2.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) and isnull(t2.id_vdorigem,0)=0";
                if (CodVendedor == "0")
                    sSql = sSql + " AND T3.EntraRel=1";
                else
                    sSql = sSql + " AND T3.Id_Vendedor in (" + CodVendedor + ")";

                sSql = sSql + "  GROUP BY T3.VENDEDOR,T2.DATA,T2.STATUS ORDER BY T3.Vendedor,T2.DATA";

                SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);
                bool FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;
                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {

                        if (Tab01.Rows[I]["Vendedor"].ToString().Trim() == LerSQL["Vendedor"].ToString().Trim() && Tab01.Rows[I]["DATA"].ToString().Trim() == LerSQL["DATA"].ToString().Trim())
                        {
                            FindLinha = true;
                            if (int.Parse(LerSQL["STATUS"].ToString().Trim()) ==1)
                                Tab01.Rows[I]["Confirmado"] = decimal.Parse(Tab01.Rows[I]["Confirmado"].ToString()) + decimal.Parse(LerSQL["TOTAL"].ToString());                            
                            else if (int.Parse(LerSQL["STATUS"].ToString().Trim()) == 2)
                                Tab01.Rows[I]["Faturado"]   = decimal.Parse(Tab01.Rows[I]["Faturado"].ToString()) + decimal.Parse(LerSQL["TOTAL"].ToString());
                            else if (int.Parse(LerSQL["STATUS"].ToString().Trim()) == 3)
                                Tab01.Rows[I]["Entregue"]   = decimal.Parse(Tab01.Rows[I]["Entregue"].ToString()) + decimal.Parse(LerSQL["TOTAL"].ToString());
                            Tab01.Rows[I]["TOTALCOMISSAO"] = decimal.Parse(Tab01.Rows[I]["TOTALCOMISSAO"].ToString()) + decimal.Parse(LerSQL["TOTALCOMISSAO"].ToString());
                            break;
                        }
                    }

                    if (!FindLinha)
                    {
                        if (int.Parse(LerSQL["STATUS"].ToString().Trim()) == 1)
                            Tab01.Rows.Add(LerSQL["Vendedor"].ToString(), LerSQL["DATA"].ToString(), 0, decimal.Parse(LerSQL["TOTAL"].ToString()), 0, 0, decimal.Parse(LerSQL["TOTALCOMISSAO"].ToString()));
                        else if (int.Parse(LerSQL["STATUS"].ToString().Trim()) == 2)
                            Tab01.Rows.Add(LerSQL["Vendedor"].ToString(), LerSQL["DATA"].ToString(), 0, 0, decimal.Parse(LerSQL["TOTAL"].ToString()), 0, decimal.Parse(LerSQL["TOTALCOMISSAO"].ToString()));
                        else if (int.Parse(LerSQL["STATUS"].ToString().Trim()) == 3)
                            Tab01.Rows.Add(LerSQL["Vendedor"].ToString(), LerSQL["DATA"].ToString(), 0, 0, 0, decimal.Parse(LerSQL["TOTAL"].ToString()), decimal.Parse(LerSQL["TOTALCOMISSAO"].ToString()));
                    }
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelRentabDiaria Rel001 = new Relatorios.RelRentabDiaria();
                DataSet TabRel = new DataSet();
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op17.Checked) // Produtos em Promoção
            {
                string sSql = "select t2.Descricao as Promocao,t3.referencia,t3.Descricao as Produto,t1.PrcEspecial,t1.PrcVarejo,t1.PrcMinimo,t1.PrcAtacado,t2.DtInicio,t2.DtFinal,t2.Pcomissao,t2.Segunda,t2.Terca,t2.Quarta,t2.Quinta,t2.Sexta,t2.Sabado,t2.Domingo from PromocaoProdutosItens t1" +
                              " left join PromocaoProdutos t2 on(t2.Id_Promocao = t1.Id_Promocao)" +
                              " left join produtos t3 on(t3.id_produto = t1.id_produto)" +
                              " where t1.Ativo = 1 and t2.Ativo = 1";
                if (LstPromocao.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.Id_Promocao=" + LstPromocao.SelectedValue.ToString();

                sSql = sSql + " order by t2.Descricao,t3.Descricao";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelListaPromocoes Rel001 = new Relatorios.RelListaPromocoes();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            if (Op18.Checked) // Faturamento Geral
            {
                DataTable Tab01 = new DataTable();
                Tab01.Columns.Add("Filial", Type.GetType("System.String"));
                Tab01.Columns.Add("Equipe", Type.GetType("System.String"));
                Tab01.Columns.Add("Vendedor", Type.GetType("System.String"));
                Tab01.Columns.Add("TotVendedor", Type.GetType("System.Decimal"));

                string sSql = "SELECT '' AS FILIAL ,T3.VENDEDOR AS EQUIPE,T2.VENDEDOR,round(SUM(T1.VLRTOTAL),2) AS TOTVENDEDOR FROM MVVENDA T1" +
                              "  LEFT JOIN VENDEDORES T2 ON(T2.ID_VENDEDOR = T1.ID_VENDEDOR)" +
                              " LEFT JOIN VENDEDORES T3 ON(T3.ID_VENDEDOR = T2.Id_VendGrupo)" +
                              " WHERE T1.TpVenda = 'PV' AND Status = 3" +
                              " and T1.PrevEntrega >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PrevEntrega <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                              " AND ISNULL(T1.Id_VdOrigem,0) = 0" +
                              " AND ISNULL(T2.Id_VendGrupo,0) > 0" +
                              " GROUP BY T3.VENDEDOR,T2.VENDEDOR" +
                              " ORDER BY 1,2,3";

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

                            if (FrmPrincipal.IdFilialConexao == int.Parse(LerFiliais["ID_Filial"].ToString()))
                                StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                            SqlConnection ConexaoInterna = new SqlConnection(StringConexao);
                            ConexaoInterna.Open();
                            Funcoes ControleInterno = new Funcoes();
                            ControleInterno.Conexao = ConexaoInterna;
                            SqlDataReader LerSQL = ControleInterno.ConsultaSQL(sSql);
                            bool FindLinha = false;
                            while (LerSQL.Read())
                                Tab01.Rows.Add(LerFiliais["FANTASIA"].ToString().Trim()+ "/"+ LerSQL["EQUIPE"].ToString().Trim(), LerSQL["EQUIPE"].ToString().Trim(), LerSQL["VENDEDOR"].ToString().Trim(), decimal.Parse(LerSQL["TOTVENDEDOR"].ToString()));

                            ConexaoInterna.Dispose();
                        }
                        catch
                        {
                        }
                    }
                }
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelFatGeral Rel001 = new Relatorios.RelFatGeral();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                //((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                //((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
            }
            BtnImprimir.Enabled = true;
        }

        /*private void BtnBuscaPrd_Click(object sender, EventArgs e)
        {
            BuscaPrd.IdProduto = 0;
            BuscaPrd.ShowDialog();
            if (BuscaPrd.IdProduto > 0)
            {
                LstGrupo.SelectedValue = "0";
                TxtCodPrd.Text = BuscaPrd.CadProd.IdProduto.ToString();
                TxtDescricao.Text = BuscaPrd.CadProd.Descricao;
            }
            else
                TxtCodPrd.Text = "0";
        }*/

        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            BuscaPessoa.ShowDialog();
            if (BuscaPessoa.CadPessoa.IdPessoa > 0)
            {
                TxtCodCliente.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                TxtCliente.Text    = BuscaPessoa.CadPessoa.RazaoSocial;
            }
            else
            {
                TxtCodCliente.Text = "0";
                TxtCliente.Text    = " ";
            }
        }

        private void BtnBuscaPrd_Click(object sender, EventArgs e)
        {
            BuscaPrd.IdProduto = 0;
            BuscaPrd.ShowDialog();
            if (BuscaPrd.IdProduto > 0)
            {
                TxtCodPrd.Text = BuscaPrd.CadProd.IdProduto.ToString();
                TxtDescricao.Text = BuscaPrd.CadProd.Descricao;
            }
            else
                TxtCodPrd.Text = "0";
        }
               
                  
    }
}