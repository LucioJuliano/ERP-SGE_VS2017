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
using CrystalDecisions;

namespace ERP_SGE
{
    public partial class FrmRelEstoque : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        private string RefProduto = "";
        private string IdFiliais  = "";

        public FrmRelEstoque()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao     = FrmPrincipal.Conexao;
            Op01.Checked         = true;
            Dt1.Value            = DateTime.Now.AddDays(-1);
            Dt2.Value            = DateTime.Now;
            TabPrcVendas.Checked = true;
            PnlPrd.Visible       = false;
            CamposLista();
        }
        private void CamposLista()
        {            
            LstGrupo   = FrmPrincipal.PopularCombo("SELECT Id_Grupo,Grupo FROM GrupoProduto WHERE ATIVO=1 ORDER BY Grupo", LstGrupo,"Todos");
            LstFilial  = FrmPrincipal.PopularCombo("SELECT Id_Filial,SUBSTRING(FANTASIA,1,80) AS FILIAL FROM Empresa_Filial ORDER BY FANTASIA", LstFilial);            
            LstUsuario = FrmPrincipal.PopularCombo("SELECT Id_Usuario,Usuario FROM Usuarios Where LiberaEstoque=1 ORDER BY Usuario ", LstUsuario,"Todos");
            CkListMov  = FrmPrincipal.PopularCheckList("select Chave,Descricao from TabelasAux where Estoque in (1,2) order by Descricao", CkListMov, "", "");
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
            Ck_Balanco.Visible    = Op01.Checked || Op02.Checked;
            Ck_Zerado.Visible   = Op02.Checked;
            BoxOpTab.Visible    = Op01.Checked;
            LstGrupo.Visible    = Op01.Checked || Op02.Checked || Op05.Checked || Op08.Checked || Op09.Checked || Op10.Checked;
            PnlPessoa.Visible   = Op07.Checked || Op06.Checked;

            PnlPrd.Visible      = Op02.Checked || Op05.Checked || Op06.Checked || Op08.Checked || Op09.Checked || Op10.Checked || Op13.Checked;
            PnlUsuario.Visible  = Op03.Checked;
            BoxPeriodo.Visible  = Op03.Checked || Op04.Checked || Op05.Checked || Op06.Checked || Op09.Checked || Op10.Checked || Op12.Checked || Op13.Checked; 
            PnlMvEst.Visible    = Op05.Checked;

            label2.Visible      = PnlUsuario.Visible;            
            label8.Visible      = LstGrupo.Visible;
            PnlFilial.Visible   = Op04.Checked;

            LblDescPrd.Visible = Op02.Checked;
            TxtDescPrd.Visible = Op02.Checked;

            
            if (Op09.Checked)
            {
                Dt2.Visible     = false;
                label16.Visible = false;
                label6.Text     = "Data Base:";
            }
            else
            {
                Dt2.Visible     = true;
                label16.Visible = true;
                label6.Text     = "Periodo de:";
            }
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            BtnImprimir.Enabled = false;


            if (Op01.Checked) // Tabela de Preço
            {
                string sSql = "SELECT T2.ID_GRUPO,T2.GRUPO,T1.ID_PRODUTO,T1.REFERENCIA,T1.DESCRICAO,T1.PRCSENSACIONAL,T1.PRCESPECIAL,T1.PRCMINIMO,T1.PRCVAREJO,T1.PRCATACADO,T1.ULTPRCCOMPRA FROM PRODUTOS T1" +
                              " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO)";

                if (Ck_Balanco.Checked)
                    sSql = sSql + " WHERE T1.Ativo <= 1";
                else
                    sSql = sSql + " WHERE T1.Ativo = 1";
                                
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.ID_Grupo=" + LstGrupo.SelectedValue.ToString();
                sSql = sSql + " ORDER BY T2.Grupo,T1.DESCRICAO";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelTabPreco Rel001 = new Relatorios.RelTabPreco();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                // Passando o Parametro
                CrystalDecisions.Shared.ParameterValues P_TipoRel = new CrystalDecisions.Shared.ParameterValues();
                if (TabPrcGeral.Checked)
                    P_TipoRel.AddValue(0);
                if (TabPrcVendas.Checked)
                    P_TipoRel.AddValue(1);
                if (TabPrcDist.Checked)
                    P_TipoRel.AddValue(2);
                if (TabPrcVarejo.Checked)
                    P_TipoRel.AddValue(3);   
             
                Rel001.ParameterFields[0].CurrentValues = P_TipoRel;                                
                FrmRel.cryRepRelatorio.ReportSource     = Rel001;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();                
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op02.Checked) // Saldo do Estoque
            {
                //string sSql1 = "SELECT  T2.Id_Grupo, T2.Grupo, T1.Id_Produto, T1.Referencia, T1.Descricao, T1.SaldoEstoque FROM Produtos AS T1 LEFT OUTER JOIN GrupoProduto AS T2 ON T2.Id_Grupo = T1.Id_Grupo";
                string sSql1 = "SELECT GRPPRD.Id_Grupo, GRPPRD.Grupo, T1.Id_Produto, T1.Referencia, T1.Descricao, " +
                               " CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                               "  LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE FROM Produtos T1 " +
                               "   LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO)";

                string sSql2 = "SELECT T1.ID_PRODUTO,ISNULL(SUM(T1.QTDE),0)+ISNULL((SELECT SUM(KT1.QTDE*KT2.QTDE) AS PREVISTOKIT FROM PRODUTOSKIT KT1"+
                                                                   " LEFT JOIN MVVENDAITENS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRDMASTER)"+
                                                                   " LEFT JOIN MVVENDA KT3 ON (KT3.ID_VENDA=KT2.ID_VENDA) "+
                                                                   " LEFT JOIN PRODUTOS KT4 ON (KT4.ID_PRODUTO=KT2.ID_PRODUTO) "+
                                                                   " LEFT JOIN TABELASAUX KT5 ON (KT5.CAMPO='VENDA' AND KT5.CHAVE=KT3.TPVENDA) "+
                                                                   " WHERE KT3.STATUS IN(1,2) AND KT2.TIPOITEM='S' AND KT3.ID_ENTREGADOR=0  AND KT5.ESTOQUE=2 "+
                                                                   " AND KT1.ID_PRODUTO=T1.ID_PRODUTO GROUP BY KT1.ID_PRODUTO),0) AS PREVISTO FROM MVVENDAITENS T1 "+
                               " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                               " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)"+
                               " LEFT JOIN TABELASAUX T4 ON (T4.CAMPO='VENDA' AND T4.CHAVE=T2.TPVENDA)  "+
                               " WHERE T2.STATUS IN(1,2) AND T2.SEMMOVEST=0 AND T1.TIPOITEM='S' AND T2.ID_ENTREGADOR=0 AND T4.ESTOQUE=2";

                string sSql3 = "SELECT T2.ID_VENDA,T2.NUMDOCUMENTO,T4.DESCRICAO AS TIPOVENDA,T3.RAZAOSOCIAL,T2.PREVENTREGA,T1.ID_PRODUTO,T1.QTDE FROM MVVENDAITENS T1 " +
                               " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)  LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T2.ID_PESSOA) " +
                               " LEFT JOIN TABELASAUX T4 ON (T4.CAMPO='VENDA' AND T4.CHAVE=T2.TPVENDA) " +
                               " LEFT JOIN PRODUTOS T5 ON (T5.ID_PRODUTO=T1.ID_PRODUTO)" +
                               " WHERE T2.STATUS IN(1,2) AND T2.SEMMOVEST=0 AND T1.TIPOITEM='S'  AND T2.ID_ENTREGADOR=0 AND T4.ESTOQUE=2" +
                               " UNION " +
                               " SELECT T3.ID_VENDA,T3.NUMDOCUMENTO,T5.DESCRICAO AS TIPOVENDA,T4.RAZAOSOCIAL,T3.PREVENTREGA,T1.ID_PRODUTO,(T2.QTDE*T1.QTDE) AS QTDE FROM PRODUTOSKIT T1 " +
                               " LEFT JOIN MVVENDAITENS T2 ON (T2.ID_PRODUTO=T1.ID_PRDMASTER)  " +
                               " LEFT JOIN MVVENDA T3 ON (T3.ID_VENDA=T2.ID_VENDA)  " +
                               " LEFT JOIN PESSOAS T4 ON (T4.ID_PESSOA=T3.ID_PESSOA)  " +
                               " LEFT JOIN TABELASAUX T5 ON (T5.CAMPO='VENDA' AND T5.CHAVE=T3.TPVENDA)  " +
                               " LEFT JOIN PRODUTOS T6 ON (T6.ID_PRODUTO=T1.ID_PRODUTO) " +
                               " WHERE T3.STATUS IN(1,2) AND T3.SEMMOVEST=0 AND T2.TIPOITEM='S' AND T3.ID_ENTREGADOR=0  AND T5.ESTOQUE=2 ";

                if (Ck_Balanco.Checked)
                    sSql1 = sSql1 + " WHERE t1.produtokit=0";
                else
                {
                    sSql1 = sSql1 + " WHERE T1.Ativo = 1";
                    if (Ck_Zerado.Checked)
                        sSql1 = sSql1 + " AND SaldoEstoque <> 0";
                }

                if (TxtDescPrd.Text.Trim() != "")
                    sSql1 = sSql1 + " AND T1.DESCRICAO LIKE '%" + TxtDescPrd.Text.Trim() + "%'";

                if (LstGrupo.SelectedValue.ToString() != "0")                
                    sSql1 = sSql1 + " AND T1.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql1 = sSql1 + " AND T1.ID_Produto=" + TxtCodPrd.Text;

                sSql2 = sSql2 + " GROUP BY T1.ID_PRODUTO";

                if (Ck_Balanco.Checked)
                {
                    FrmRelatorios FrmRel = new FrmRelatorios();
                    Relatorios.RelContagemBalanco Rel001 = new Relatorios.RelContagemBalanco();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql1);
                    Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                    FrmRel.Dispose();
                }
                else
                {
                    FrmRelatorios FrmRel = new FrmRelatorios();
                    Relatorios.RelSldEstoque Rel001 = new Relatorios.RelSldEstoque();
                    DataSet TabRel = new DataSet();
                    DataSet TabPrev = new DataSet();
                    DataSet TabVenda = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql1);
                    TabPrev = Controle.ConsultaTabela(sSql2);
                    TabVenda = Controle.ConsultaTabela(sSql3);
                    Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                    Rel001.Database.Tables[1].SetDataSource(TabPrev.Tables[0]);
                    Rel001.Database.Tables[2].SetDataSource(TabVenda.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                    FrmRel.Dispose();
                }
                
            }
            if (Op03.Checked) // Produtos Liberados
            {
                string sSql = "SELECT T6.USUARIO,T2.REFERENCIA,T2.DESCRICAO,T1.DATA,T1.ID_VENDA,T3.NUMDOCUMENTO,T5.RAZAOSOCIAL,T4.DESCRICAO AS TIPOVENDA,T8.VENDEDOR,T7.ID_ITEM,T7.QTDE,T1.ESTOQUE,T2.SALDOESTOQUE" +
                              " FROM LIBERACAOPRODUTO T1" +
                              " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                              " LEFT JOIN MVVENDA T3 ON (T3.ID_VENDA=T1.ID_VENDA OR T3.ID_VDORIGEM=T1.ID_VENDA)" +
                              " LEFT JOIN TABELASAUX T4 ON (T4.CAMPO='VENDA' AND T4.CHAVE=T3.TPVENDA)" +
                              " LEFT JOIN PESSOAS T5 ON (T5.ID_PESSOA=T3.ID_PESSOA)" +
                              " LEFT JOIN USUARIOS T6 ON (T6.ID_USUARIO=T1.ID_USUARIO)" +
                              " LEFT JOIN MVVENDAITENS T7 ON (T7.ID_PRODUTO=T1.ID_PRODUTO AND T7.ID_VENDA=T3.ID_VENDA)" +
                              " LEFT JOIN VENDEDORES T8 ON (T8.ID_VENDEDOR=T3.ID_VENDEDOR)";
                sSql = sSql + " WHERE T7.QTDE > 0 AND T7.TIPOITEM='S' AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (LstUsuario.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.ID_Usuario=" + LstUsuario.SelectedValue.ToString();

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelLiberacaoPrd Rel001 = new Relatorios.RelLiberacaoPrd();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op04.Checked) // Notas de Entrada
            {
                if (LstFilial.SelectedValue.ToString() != "0")
                {
                    string sSql = "SELECT T1.ID_MOV,T1.DTENTSAI,T2.RAZAOSOCIAL,T1.NUMDOCUMENTO,T1.NUMFORMULARIO,T1.VLRTOTAL,T3.FANTASIA AS FILIAL,T1.B_ICMS,T1.VLRICMS," +
                                  " T1.B_ICMSSUB,T1.VLRICMSSUB,T1.VLRIPI,T4.CFOP,T1.TIPOPGTO,CHAVENFE,T1.OUTROSIPI FROM MVESTOQUE T1" +
                                  "  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                  "  LEFT JOIN EMPRESA_FILIAL T3 ON (T3.ID_FILIAL=T1.ID_FILIALORIGDEST)" +
                                  "  LEFT JOIN CFOP T4 ON (T4.ID_CFOP=T1.ID_CFOP)" +
                                  " WHERE T1.TPMOV='ENTNF' AND T1.STATUS=1" +
                                  "   AND T1.DTENTSAI >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTENTSAI <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                    if (LstFilial.SelectedValue.ToString() != "0")
                        sSql = sSql + " AND T1.ID_FILIALORIGDEST=" + LstFilial.SelectedValue.ToString();

                    FrmRelatorios FrmRel = new FrmRelatorios();
                    Relatorios.RelNFCompra Rel001 = new Relatorios.RelNFCompra();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSql);
                    Rel001.SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = Rel001;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = LstFilial.Text.Trim();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                    FrmRel.ShowDialog();
                    Rel001.Dispose();
                    FrmRel.Dispose();
                }
                else
                    MessageBox.Show("Favor selecionar uma Filial", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (Op05.Checked) // Extrato do Produto
            {
                string LstMov = "";
                for (int I = 0; I <= CkListMov.Items.Count - 1; I++)
                {
                    if (CkListMov.GetItemChecked(I))
                    {
                        DataRowView item = (DataRowView)CkListMov.Items[I];
                        if (LstMov == "")
                            LstMov = "'" + item.Row[0].ToString().Trim() + "'";
                        else
                            LstMov = LstMov + ",'" + item.Row[0].ToString().Trim() + "'";
                        
                    }
                }

                string sSql = "SELECT T1.DATA,T1.ID_ITEM,CASE T8.DATA WHEN Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) THEN ISNULL(T8.SLDANTERIOR,0) ELSE ISNULL(T8.SALDO,0) END AS SALDOANTERIOR,T1.ID_PRODUTO,T7.GRUPO,T1.DTMOVIM,T1.QTDE,T1.TPMOV,T1.VLRUNITARIO,T2.REFERENCIA,T2.DESCRICAO,T3.ESTOQUE,T3.DESCRICAO AS TIPOMOVIMENTO," +
                              "   T6.RAZAOSOCIAL,T5.DOCUMENTO,T5.NUMDOCUMENTO, F1.FANTASIA " +
                              " FROM EXTRATOESTOQUE T1" +
                              " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                              " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                              " LEFT JOIN MVESTOQUEITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM )" +
                              " LEFT JOIN MVESTOQUE T5 ON (T5.ID_MOV=T4.ID_MOV)" +
                              " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                              " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                              " LEFT JOIN Empresa_Filial F1 ON (F1.Id_Filial=T5.Id_FilialOrigDest)"+
                              " LEFT OUTER JOIN SALDOESTOQUE T8 ON (T8.ID_PRODUTO=T1.ID_PRODUTO AND T8.ID_LANC=(SELECT TOP 1 ID_LANC FROM SALDOESTOQUE  SLD"+
                              "    WHERE  SLD.ID_PRODUTO=T1.ID_PRODUTO"+
                              "  AND SLD.DATA <=Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              " ORDER BY SLD.ID_PRODUTO,SLD.DATA DESC))"+
                              " WHERE T3.CAMPO='TPMVEST'" +
                              "   AND T1.DTMOVIM >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTMOVIM <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T2.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

                if (LstMov != "")
                    sSql = sSql + " AND T1.TpMov in (" + LstMov + ")";

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.ID_Produto=" + TxtCodPrd.Text;                
                sSql = sSql + " UNION" +
                              " SELECT T1.DATA,T1.ID_ITEM,CASE T8.DATA WHEN Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) THEN ISNULL(T8.SLDANTERIOR,0) ELSE ISNULL(T8.SALDO,0) END AS SALDOANTERIOR,T1.ID_PRODUTO,T7.GRUPO,T1.DTMOVIM,T1.QTDE,T1.TPMOV,T1.VLRUNITARIO,T2.REFERENCIA,T2.DESCRICAO," +
                              "        CASE T4.TIPOITEM WHEN 'E' THEN 1 ELSE 2 END AS Estoque,T3.DESCRICAO AS TIPOMOVIMENTO," +
                              "        T6.RAZAOSOCIAL,CONVERT(CHAR,T5.ID_VENDA) AS DOCUMENTO,T5.NUMDOCUMENTO, ' ' AS FANTASIA" +
                              " FROM EXTRATOESTOQUE T1" +
                              " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                              " LEFT JOIN TABELASAUX T3 ON (T3.CHAVE=T1.TPMOV)" +
                              " LEFT JOIN MVVENDAITENS T4 ON (T4.ID_ITEM=T1.ID_ITEM AND T4.TIPOITEM IN ('S','E'))" +
                              " LEFT JOIN MVVENDA T5 ON (T5.ID_VENDA=T4.ID_VENDA)" +
                              " LEFT JOIN PESSOAS T6 ON (T6.ID_PESSOA=T5.ID_PESSOA)" +
                              " LEFT JOIN GRUPOPRODUTO T7 ON (T7.ID_GRUPO=T2.ID_GRUPO)" +
                              " LEFT OUTER JOIN SALDOESTOQUE T8 ON (T8.ID_PRODUTO=T1.ID_PRODUTO AND T8.ID_LANC=(SELECT TOP 1 ID_LANC FROM SALDOESTOQUE  SLD" +
                              "  WHERE  SLD.ID_PRODUTO=T1.ID_PRODUTO" +
                              "  AND SLD.DATA <=Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) " +
                              " ORDER BY SLD.ID_PRODUTO,SLD.DATA DESC))" +
                              " WHERE T3.CAMPO='VENDA' " +
                              "   AND T1.DTMOVIM >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTMOVIM <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T2.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

                if (LstMov != "")
                    sSql = sSql + " AND T1.TpMov in (" + LstMov + ")";

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.ID_Produto=" + TxtCodPrd.Text;
                sSql = sSql + " ORDER BY DTMOVIM,T1.DATA";
                
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelExtratoProduto Rel001 = new Relatorios.RelExtratoProduto();
                DataSet TabRel = new DataSet();                
                DataSet TabSld = new DataSet();                
                TabRel = Controle.ConsultaTabela(sSql);                
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);                
                CrystalDecisions.Shared.ParameterValues P_TipoRel = new CrystalDecisions.Shared.ParameterValues();
                if (LstMov != "")
                    P_TipoRel.AddValue("S");
                else
                    P_TipoRel.AddValue("N");
                Rel001.ParameterFields[0].CurrentValues = P_TipoRel;                                
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op06.Checked) // Extrato das Entregas de Mercadorias
            {
                string sSql = "SELECT T3.RAZAOSOCIAL,T2.ID_VENDA,T2.NUMDOCUMENTO,T2.DATA,T2.PREVENTREGA,T4.ID_PRODUTO,T4.REFERENCIA,T4.DESCRICAO,T1.QTDE,T1.VLRUNITARIO FROM MVVENDAITENS T1"+
                              "  LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)"+
                              "  LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T2.ID_PESSOA)"+
                              "  LEFT JOIN PRODUTOS T4 ON (T4.ID_PRODUTO=T1.ID_PRODUTO)"+
                              " WHERE T2.STATUS in (1,2,3)  AND T2.TPVENDA='EMVF'"+
                              "   AND T2.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.ID_Produto=" + TxtCodPrd.Text;
                if (TxtCodCliente.Text.Trim() != "0")
                    sSql = sSql + " AND T2.ID_Pessoa=" + TxtCodCliente.Text.Trim();
                sSql = sSql + " ORDER BY T3.RAZAOSOCIAL,T4.DESCRICAO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelExtratoEntregaMerc Rel001 = new Relatorios.RelExtratoEntregaMerc();
                DataSet TabRel = new DataSet();                
                TabRel = Controle.ConsultaTabela(sSql);                
                Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);                
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op07.Checked) // Saldo Estoque Venda Programada
            {
                string sSql = "SELECT T2.RAZAOSOCIAL,T3.REFERENCIA,T3.DESCRICAO,T1.SALDO FROM SALDOPRDCLIENTE T1" +
                              " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                              " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)";
                if (TxtCodCliente.Text.Trim() != "0")
                    sSql = sSql + " WHERE T1.SALDO <> 0 AND T1.ID_Pessoa=" + TxtCodCliente.Text.Trim();
                else
                    sSql = sSql + " WHERE T1.SALDO <> 0 ";
                sSql = sSql + " ORDER BY T2.RAZAOSOCIAL,T3.DESCRICAO";                

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelSldVdProg Rel001 = new Relatorios.RelSldVdProg();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;                
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op08.Checked) // Relação de Produtos com estoque minimo
            {
                string sSql = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,GRPPRD.GRUPO, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                              "   LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,(SELECT TOP 1 PED1.PREVENTREGA FROM PEDCOMPRA PED1" +
                              "   LEFT JOIN PEDCOMPRAITENS PED2 ON (PED2.ID_DOCUMENTO=PED1.ID_DOCUMENTO) WHERE PED2.ID_PRODUTO=T1.ID_PRODUTO AND PED1.STATUS=1 ORDER BY PED1.PREVENTREGA) AS PREVENTREGA,T1.ESTMINIMO " +
                              " FROM Produtos T1 " +
                              "   LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) " +
                              " WHERE T1.PRODUTOKIT=0 AND T1.Ativo=1 AND GRPPRD.LISTAESTMIN=1 " +
                              "   AND T1.SALDOESTOQUE <= T1.ESTMINIMO " +
                              "   AND T1.ESTMINIMO > 0";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.ID_Produto=" + TxtCodPrd.Text;

                sSql = sSql + " ORDER BY GRPPRD.GRUPO,T1.DESCRICAO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelEstMinimo Rel001 = new Relatorios.RelEstMinimo();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;                
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op09.Checked) // Inventario do Estoque
            {
                string sSql = "SELECT T1.*,T2.GRUPO,ISNULL((SELECT TOP 1 S1.SALDO FROM  SALDOESTOQUE S1 WHERE S1.ID_PRODUTO=T1.ID_PRODUTO AND S1.DATA <= CONVERT(DATETIME,'" + Dt1.Value.ToShortDateString() + "',103) ORDER BY S1.DATA DESC),0) AS SALDO FROM PRODUTOS T1 " +
                              " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE T1.ATIVO=1 AND T1.PRODUTOKIT=0 "+
                              " AND ISNULL((SELECT TOP 1 S1.SALDO FROM  SALDOESTOQUE S1 WHERE S1.ID_PRODUTO=T1.ID_PRODUTO AND S1.DATA <= CONVERT(DATETIME,'" + Dt1.Value.ToShortDateString() + "',103) ORDER BY S1.DATA DESC),0) > 0";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T1.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.ID_Produto=" + TxtCodPrd.Text;
                               
                sSql = sSql + " ORDER BY T2.GRUPO,T1.DESCRICAO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelInventario Rel001 = new Relatorios.RelInventario();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Data Base:" + Dt1.Value.ToShortDateString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }

            if (Op10.Checked)
            {
                DataTable Tab01 = EstoqueFiscal(RefProduto);
                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelCompraSaidaFiscal Rel001 = new Relatorios.RelCompraSaidaFiscal();
                Rel001.SetDataSource(Tab01);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString()+"  Filiais:"+IdFiliais;
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();                

            }
            if (Op11.Checked)
            {
                FrmEtqPreco Frm = new FrmEtqPreco();
                Frm.FrmPrincipal = FrmPrincipal;
                Frm.ShowDialog();                
                Frm.Dispose();
            }

            if (Op12.Checked) // Lista de Produtos Alterados
            {
                string sSql = "SELECT T2.Grupo,T1.Referencia,T1.Descricao,t1.PrcAtacado,T1.PrcMinimo,T1.PrcVarejo,T1.PrcEspecial from Produtos T1" +
                              " left join GrupoProduto T2 on (T2.Id_Grupo=T1.ID_Grupo) " +
                             "  WHERE T1.DTAltPrc >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTAltPrc <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                sSql = sSql + " ORDER BY T2.GRUPO,T1.DESCRICAO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelListaAltProdutos Rel001 = new Relatorios.RelListaAltProdutos();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }
            if (Op13.Checked) // Lista dos Produtos Cancmentos por Movimento
            {
                string sSql = "select t2.DataCancel,t2.data,t2.id_venda,T4.DESCRICAO AS MOVIMENTO,t2.numdocumento,t2.pessoa,t3.referencia,t3.descricao,t1.qtde from mvvendaitens t1"+
                              "  left join mvvenda t2 on (t2.id_venda=t1.id_venda)"+
                              "  left join produtos t3 on (t3.id_produto=t1.id_produto) "+
                              "  LEFT JOIN TABELASAUX T4 ON (T4.CAMPO='VENDA' AND T4.CHAVE=T2.TPVENDA)"+
                              " where t1.tipoitem<>'N' and t2.status=4"+
                              "  And t2.DataCancel >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND t2.DataCancel <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T1.ID_Produto=" + TxtCodPrd.Text;

                sSql = sSql + " ORDER BY T2.DataCancel,T3.DESCRICAO";

                FrmRelatorios FrmRel = new FrmRelatorios();
                Relatorios.RelMvCancVd Rel001 = new Relatorios.RelMvCancVd();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSql);
                Rel001.SetDataSource(TabRel.Tables[0]);
                FrmRel.cryRepRelatorio.ReportSource = Rel001;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
                FrmRel.ShowDialog();
                Rel001.Dispose();
                FrmRel.Dispose();
            }

            BtnImprimir.Enabled = true;          
        }
        
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
             FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
             BuscaPessoa.FrmPrincipal = this.FrmPrincipal;                    
             BuscaPessoa.ShowDialog();
             if (BuscaPessoa.CadPessoa.IdPessoa > 0)
             {
                 TxtCodCliente.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                 TxtCliente.Text = BuscaPessoa.CadPessoa.RazaoSocial;
             }
             else
             {
                 TxtCodCliente.Text = "0";
                 TxtCliente.Text = "0";
             }
        }

        private void BtnBuscaPrd_Click(object sender, EventArgs e)
        {
            FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
            BuscaPrd.FrmPrincipal = this.FrmPrincipal;
            BuscaPrd.IdProduto = 0;
            BuscaPrd.ShowDialog();
            if (BuscaPrd.IdProduto > 0)
            {
                LstGrupo.SelectedValue = "0";
                TxtCodPrd.Text = BuscaPrd.CadProd.IdProduto.ToString();
                TxtDescricao.Text = BuscaPrd.CadProd.Descricao;
                RefProduto = BuscaPrd.CadProd.Referencia;
            }
            else
            {
                TxtCodPrd.Text = "0";
                RefProduto = "";
            }

        }

        private DataTable EstoqueFiscal(string RefPrd)
        {
            DataTable Tab01 = new DataTable();
            Tab01.Columns.Add("GRUPO", Type.GetType("System.String"));
            Tab01.Columns.Add("REFERENCIA", Type.GetType("System.String"));
            Tab01.Columns.Add("DESCRICAO", Type.GetType("System.String"));
            Tab01.Columns.Add("L1_Entrada", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L2_Entrada", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L3_Entrada", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L4_Entrada", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L5_Entrada", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L6_Entrada", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L7_Entrada", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L1_Saida", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L2_Saida", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L3_Saida", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L4_Saida", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L5_Saida", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L6_Saida", Type.GetType("System.Decimal"));
            Tab01.Columns.Add("L7_Saida", Type.GetType("System.Decimal"));

            string sSql = "select t4.Grupo,t2.Id_FilialOrigDest,t3.Referencia,T3.DESCRICAO,SUM(T1.qtde) as Qtde from MvEstoqueItens t1" +
                          " left join MvEstoque t2 on (t2.Id_Mov=t1.Id_MOv)" +
                          " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                          " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                          " where t2.Status=1 and t2.TpMov='ENTNF'" +                          
                          "   and T2.DTEntSai >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DTEntSai <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
                          
            
            if (LstGrupo.SelectedValue.ToString() != "0")
                sSql = sSql + " AND T4.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

            if (int.Parse(TxtCodPrd.Text) > 0)
                sSql = sSql + " AND T3.ID_Produto=" + TxtCodPrd.Text;

            sSql = sSql + " Group By t4.Grupo,t2.Id_FilialOrigDest,t3.Referencia,T3.DESCRICAO ";
            
            // Processando as Entradas
            SqlDataReader LerSQL = Controle.ConsultaSQL(sSql);
            bool FindLinha = false;            
            while (LerSQL.Read())
            {
                FindLinha = false;                
                for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                {
                    if (Tab01.Rows[I]["REFERENCIA"].ToString().Trim() == LerSQL["REFERENCIA"].ToString().Trim())
                    {
                        FindLinha = true;
                        if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 1)
                            Tab01.Rows[I]["L1_Entrada"] = decimal.Parse(Tab01.Rows[I]["L1_Entrada"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 2)
                            Tab01.Rows[I]["L2_Entrada"] = decimal.Parse(Tab01.Rows[I]["L2_Entrada"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 3)
                            Tab01.Rows[I]["L3_Entrada"] = decimal.Parse(Tab01.Rows[I]["L3_Entrada"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 4)
                            Tab01.Rows[I]["L4_Entrada"] = decimal.Parse(Tab01.Rows[I]["L4_Entrada"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 5)
                            Tab01.Rows[I]["L5_Entrada"] = decimal.Parse(Tab01.Rows[I]["L5_Entrada"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 6)
                            Tab01.Rows[I]["L6_Entrada"] = decimal.Parse(Tab01.Rows[I]["L6_Entrada"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 7)
                            Tab01.Rows[I]["L7_Entrada"] = decimal.Parse(Tab01.Rows[I]["L7_Entrada"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());                        
                    }
                }

                if (!FindLinha)
                {
                    if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 1)
                        Tab01.Rows.Add(LerSQL["GRUPO"].ToString(),LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 2)
                        Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 3)
                        Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 4)
                        Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 5)
                        Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 6)
                        Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0, 0, 0);
                    else if (int.Parse(LerSQL["Id_FilialOrigDest"].ToString()) == 7)
                        Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0, 0);                                        
                }
            }

            // Saindas
            SqlDataReader LerFilial = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL ORDER BY ID_FILIAL");
            IdFiliais = "";
            while (LerFilial.Read())
            {
                SqlConnection ServidorOrigem;
                try
                {
                    ServidorOrigem = new SqlConnection("Data Source=" + LerFilial["ServidorRemoto"].ToString().Trim() + LerFilial["Porta"].ToString().Trim() + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;");
                    ServidorOrigem.Open();
                }
                catch
                {
                    MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor: " + LerFilial["Fantasia"].ToString().Trim(), "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }
                IdFiliais = IdFiliais + LerFilial["ID_FILIAL"].ToString().Trim() + " / ";
                Funcoes ControleOrigem = new Funcoes();
                ControleOrigem.Conexao = ServidorOrigem;

                //Nota Fiscal de Saidas NFe
                DataSet TabItens = new DataSet();
                sSql="Select t4.grupo,t3.Referencia,t3.descricao,SUM(T1.qtde) as Qtde from NotaFiscalItens t1" +
                     " left join NotaFiscal t2 on (t2.Id_Nota=t1.Id_Nota)" +
                     " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                     " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                     " where t2.Status=1 and t2.EntSaida=0"+
                     "   and t2.Id_Filial=" + LerFilial["ID_Filial"].ToString() +
                     "   and T2.DTEMISSAO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DTEMISSAO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (LstGrupo.SelectedValue.ToString() != "0")
                   sSql = sSql + " AND T4.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                   sSql = sSql + " AND T3.Referencia='" + RefProduto+"'";

                sSql = sSql + " Group By t4.grupo,t3.Referencia,t3.descricao";

                if (int.Parse(LerFilial["ID_Filial"].ToString())==1)
                    LerSQL = Controle.ConsultaSQL(sSql);
                else
                    LerSQL = ControleOrigem.ConsultaSQL(sSql);
                FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;

                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {

                        if (Tab01.Rows[I]["REFERENCIA"].ToString().Trim() == LerSQL["REFERENCIA"].ToString().Trim())
                        {
                            FindLinha = true;
                            if (int.Parse(LerFilial["ID_Filial"].ToString()) == 1)
                                Tab01.Rows[I]["L1_Saida"] = decimal.Parse(Tab01.Rows[I]["L1_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 2)
                                Tab01.Rows[I]["L2_Saida"] = decimal.Parse(Tab01.Rows[I]["L2_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 3)
                                Tab01.Rows[I]["L3_Saida"] = decimal.Parse(Tab01.Rows[I]["L3_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 4)
                                Tab01.Rows[I]["L4_Saida"] = decimal.Parse(Tab01.Rows[I]["L4_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 5)
                                Tab01.Rows[I]["L5_Saida"] = decimal.Parse(Tab01.Rows[I]["L5_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 6)
                                Tab01.Rows[I]["L6_Saida"] = decimal.Parse(Tab01.Rows[I]["L6_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 7)
                                Tab01.Rows[I]["L7_Saida"] = decimal.Parse(Tab01.Rows[I]["L7_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        }
                    }
                    if (!FindLinha)
                    {
                        if (int.Parse(LerFilial["ID_Filial"].ToString()) == 1)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 2)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 3)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 4)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 5)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 6)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 7)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()));
                    }
                }

                //Cupom Fiscal
                TabItens = new DataSet();
                sSql = "select t4.grupo,t3.Referencia,t3.descricao,SUM(T1.qtde) as Qtde from CupomFIscalItens t1" +
                       " left join CupomFiscal t2 on (t2.Id_Lanc=t1.Id_Lanc)" +
                       " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                       " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                       " where t2.Status=1" +                       
                       "   and T2.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

                if (LstGrupo.SelectedValue.ToString() != "0")
                    sSql = sSql + " AND T4.ID_Grupo=" + LstGrupo.SelectedValue.ToString();

                if (int.Parse(TxtCodPrd.Text) > 0)
                    sSql = sSql + " AND T3.Referencia='" + RefProduto+"'";

                sSql = sSql + " Group By t4.grupo,t3.Referencia,t3.descricao";

                LerSQL = ControleOrigem.ConsultaSQL(sSql);
                FindLinha = false;
                while (LerSQL.Read())
                {
                    FindLinha = false;

                    for (int I = 0; I <= Tab01.Rows.Count - 1; I++)
                    {

                        if (Tab01.Rows[I]["REFERENCIA"].ToString().Trim() == LerSQL["REFERENCIA"].ToString().Trim())
                        {
                            FindLinha = true;
                            if (int.Parse(LerFilial["ID_Filial"].ToString()) == 1)
                                Tab01.Rows[I]["L1_Saida"] = decimal.Parse(Tab01.Rows[I]["L1_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 2)
                                Tab01.Rows[I]["L2_Saida"] = decimal.Parse(Tab01.Rows[I]["L2_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 3)
                                Tab01.Rows[I]["L3_Saida"] = decimal.Parse(Tab01.Rows[I]["L3_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 4)
                                Tab01.Rows[I]["L4_Saida"] = decimal.Parse(Tab01.Rows[I]["L4_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 5)
                                Tab01.Rows[I]["L5_Saida"] = decimal.Parse(Tab01.Rows[I]["L5_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 6)
                                Tab01.Rows[I]["L6_Saida"] = decimal.Parse(Tab01.Rows[I]["L6_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                            else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 7)
                                Tab01.Rows[I]["L7_Saida"] = decimal.Parse(Tab01.Rows[I]["L7_Saida"].ToString()) + decimal.Parse(LerSQL["Qtde"].ToString());
                        }
                    }
                    if (!FindLinha)
                    {
                        if (int.Parse(LerFilial["ID_Filial"].ToString()) == 1)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 2)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 3)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 4)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 5)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0, 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 6)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()), 0);
                        else if (int.Parse(LerFilial["ID_Filial"].ToString()) == 7)
                            Tab01.Rows.Add(LerSQL["GRUPO"].ToString(), LerSQL["REFERENCIA"].ToString(), LerSQL["DESCRICAO"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, decimal.Parse(LerSQL["Qtde"].ToString()));
                    }
                }
            }
            return Tab01;

        }

       
               
    }
}
