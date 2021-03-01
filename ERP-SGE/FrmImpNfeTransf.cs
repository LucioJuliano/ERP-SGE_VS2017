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
    public partial class FrmImpNfeTransf : Form
    {
        public int IdNota = 0;
        public int IdCfop = 0;
        public TelaPrincipal FrmPrincipal;
        NotaFiscalItens Itens    = new NotaFiscalItens();
        Funcoes Controle         = new Funcoes();
        GrupoProduto GrupoPrd    = new GrupoProduto();
        public Pessoas CadPessoa = new Pessoas();
        private bool Tag = true;
        public bool RelFiscal;
        public DataTable TabelaFiscal;

        public FrmImpNfeTransf()
        {
            InitializeComponent();
        }

        private DataTable GeraTabelaRel()
        {
            DataTable INM = new DataTable();
            INM.Columns.Add("Filial", Type.GetType("System.String"));
            INM.Columns.Add("Grupo", Type.GetType("System.String"));
            INM.Columns.Add("Referencia", Type.GetType("System.String"));            
            INM.Columns.Add("Descricao", Type.GetType("System.String"));
            INM.Columns.Add("QtdeCompra", Type.GetType("System.Decimal"));
            INM.Columns.Add("QtdeSaida", Type.GetType("System.Decimal"));            
            return INM;
        }
        private void FrmImpNfeTransf_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Itens.Controle   = Controle;
            Dt1.Value        = DateTime.Now;
            Dt2.Value        = DateTime.Now;
            LstFilial        = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_Filial WHERE SERVIDORREMOTO<>'' ORDER BY Filial", LstFilial, "Selecionar");
            CkListGrupos     = FrmPrincipal.PopularCheckList("SELECT Id_Grupo,SubString(Grupo,1,30) as Grupo FROM GrupoProduto Where ExcluirNFETrans=0 ORDER BY Grupo", CkListGrupos, "", "");

            for (int I = 0; I <= CkListGrupos.Items.Count - 1; I++)
                CkListGrupos.SetItemChecked(I, true);

            TabelaFiscal = GeraTabelaRel();
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstFilial.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Atenção: Selecione o Local Origem", "Selecionar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string CodGrupo = "0";

            for (int I = 0; I <= CkListGrupos.Items.Count - 1; I++)
            {
                if (CkListGrupos.GetItemChecked(I))
                {
                    DataRowView item = (DataRowView)CkListGrupos.Items[I];
                    if (CodGrupo == "0")
                        CodGrupo = item.Row[0].ToString();
                    else
                        CodGrupo = CodGrupo + "," + item.Row[0].ToString();
                }
            }

            if (CodGrupo == "0")
            {
                MessageBox.Show("Atenção: Selecione o Grupo", "Selecionar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Confirma o processo dos Itens ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (RelFiscal)
                {
                    ProcRelFiscal();
                    return;
                }
                BtnConfirmar.Enabled = false;
                ProcBar.Value = 0;

                Produtos CadPrd = new Produtos();
                CadPrd.Controle = Controle;

                GrupoPrd.Controle = Controle;

                SqlConnection ServidorOrigem;
                Filiais FilialOrigem = new Filiais();
                FilialOrigem.Controle = Controle;
                FilialOrigem.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));
                try
                {
                    if (FrmPrincipal.VersaoDistribuidor)
                        ServidorOrigem = new SqlConnection("Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;");
                    else
                        ServidorOrigem = new SqlConnection("Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;");
                    
                    ServidorOrigem.Open();
                }
                catch
                {
                    MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor origem, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnConfirmar.Enabled = true;
                    return;
                }

                Funcoes ControleOrigem = new Funcoes();
                ControleOrigem.Conexao = ServidorOrigem;

                try
                {
                    DataSet TabItens = new DataSet();
                    TabItens = ControleOrigem.ConsultaTabela("WITH RESULTADO AS" +
                                                       " (select t3.Referencia,SUM(T1.qtde) as Qtde from NotaFiscalItens t1" +
                                                       " left join NotaFiscal t2 on (t2.Id_Nota=t1.Id_Nota)" +
                                                       " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                                                       " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                                                       " where t2.Status=1 and t2.EntSaida=0" +
                                                       "   and t2.Id_Filial=" + LstFilial.SelectedValue.ToString() +
                                                       "   and t4.Id_Grupo in (" + CodGrupo + ")" +
                                                       "   and T2.DTEMISSAO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DTEMISSAO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                                                       " Group By t3.Referencia " +
                                                       "UNION ALL" +
                                                       " select t3.Referencia,SUM(T1.qtde) as Qtde from CupomFIscalItens t1" +
                                                       " left join CupomFiscal t2 on (t2.Id_Lanc=t1.Id_Lanc)" +
                                                       " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                                                       " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                                                       " where t2.Status=1" +
                                                       "   and t4.Id_Grupo in (" + CodGrupo + ")" +
                                                       "   and T2.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                                                       " Group By t3.Referencia)" +
                                                       " SELECT REFERENCIA,SUM(QTDE) AS QTDE FROM RESULTADO GROUP BY REFERENCIA");

                    if (TabItens.Tables[0].Rows.Count > 0)
                    {
                        ProcBar.Value   = 0;
                        ProcBar.Maximum = TabItens.Tables[0].Rows.Count;
                        Controle.ExecutaSQL("DELETE FROM NOTAFISCALITENS WHERE ID_NOTA=" + IdNota.ToString());
                        bool FindLinha;
                        for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                        {
                            CadPrd.LerDados(TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString());

                            if (CadPrd.IdProduto == 0)
                            {
                                MessageBox.Show("Atenção: Referencia: " + TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString() + ", não Localizada no Servidor:", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                Itens.LerDados(0);
                                Itens.IdNota = IdNota;
                                Itens.IdCfop = IdCfop;
                                Itens.IdProduto = CadPrd.IdProduto;
                                Itens.SitTributaria = CadPrd.SitTributaria; ;
                                Itens.PIcms = CadPrd.IcmsIss;
                                Itens.PercRed = CadPrd.Reducao;
                                Itens.Qtde = decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString());
                                Itens.VlrUnitario = CadPrd.UltPrcCompra;

                                if (IdCfop != 45)
                                {
                                    if (CadPrd.IdGrupo == 53)
                                    {
                                        if (CadPessoa.PDescNFGrpTalimpo > 0)
                                            Itens.VlrUnitario = Math.Round(CadPrd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpTalimpo / 100)), 2);
                                    }
                                    else
                                    {
                                        if (CadPessoa.PDescNFGrpOutros > 0)
                                            Itens.VlrUnitario = Math.Round(CadPrd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpOutros / 100)), 2);
                                    }
                                }
                                Itens.GravarDados();

                            }
                            ProcBar.Value = ProcBar.Value + 1;
                        }
                    }
                    MessageBox.Show("Importação concluida", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Atenção: Ocorreu um erro na importação dos Itens", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Controle.ExecutaSQL("DELETE FROM NOTAFISCALITENS WHERE ID_NOTA=" + IdNota.ToString());
                    Close();
                }
            }
        }


        private void ProcRelFiscal()
        {

            string CodGrupo = "0";

            for (int I = 0; I <= CkListGrupos.Items.Count - 1; I++)
            {
                if (CkListGrupos.GetItemChecked(I))
                {
                    DataRowView item = (DataRowView)CkListGrupos.Items[I];
                    if (CodGrupo == "0")
                        CodGrupo = item.Row[0].ToString();
                    else
                        CodGrupo = CodGrupo + "," + item.Row[0].ToString();
                }
            }

            if (CodGrupo == "0")
            {
                MessageBox.Show("Atenção: Selecione o Grupo", "Selecionar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BtnConfirmar.Enabled = false;
            ProcBar.Value = 0;

            Produtos CadPrd = new Produtos();
            CadPrd.Controle = Controle;

            GrupoPrd.Controle = Controle;

            SqlConnection ServidorOrigem;
            Filiais FilialOrigem = new Filiais();
            FilialOrigem.Controle = Controle;
            FilialOrigem.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));
            try
            {
                if (FrmPrincipal.VersaoDistribuidor)
                    ServidorOrigem = new SqlConnection("Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;");
                else
                    ServidorOrigem = new SqlConnection("Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;");

                ServidorOrigem.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor origem, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnConfirmar.Enabled = true;
                return;
            }

            Funcoes ControleOrigem = new Funcoes();
            ControleOrigem.Conexao = ServidorOrigem;

            try
            {
                DataSet TabItens = new DataSet();
                if (int.Parse(LstFilial.SelectedValue.ToString()) == 1)
                {
                    TabItens = Controle.ConsultaTabela("select t3.Referencia,SUM(T1.qtde) as Qtde from NotaFiscalItens t1" +
                                                             " left join NotaFiscal t2 on (t2.Id_Nota=t1.Id_Nota)" +
                                                             " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                                                             " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                                                             " where t2.Status=1 and t2.EntSaida=0" +
                                                             "   and t2.Id_Filial=" + LstFilial.SelectedValue.ToString() +
                                                             "   and t4.Id_Grupo in (" + CodGrupo + ")" +
                                                             "   and T2.DTEMISSAO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DTEMISSAO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                                                             " Group By t3.Referencia ");
                }
                else
                {
                    TabItens = ControleOrigem.ConsultaTabela("select t3.Referencia,SUM(T1.qtde) as Qtde from NotaFiscalItens t1" +
                                                             " left join NotaFiscal t2 on (t2.Id_Nota=t1.Id_Nota)" +
                                                             " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                                                             " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                                                             " where t2.Status=1 and t2.EntSaida=0" +
                                                             "   and t2.Id_Filial=" + LstFilial.SelectedValue.ToString() +
                                                             "   and t4.Id_Grupo in (" + CodGrupo + ")" +
                                                             "   and T2.DTEMISSAO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DTEMISSAO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                                                             " Group By t3.Referencia ");
                }
                if (TabItens.Tables[0].Rows.Count > 0)
                {
                    ProcBar.Value = 0;
                    ProcBar.Maximum = TabItens.Tables[0].Rows.Count;
                    bool FindLinha;
                    for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                    {
                        CadPrd.LerDados(TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString());

                        if (CadPrd.IdProduto == 0)
                        {
                            MessageBox.Show("Atenção: Referencia: " + TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString() + ", não Localizada no Servidor:", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            FindLinha = false;
                            for (int A = 0; A <= TabelaFiscal.Rows.Count - 1; A++)
                            {
                                if (TabelaFiscal.Rows[A]["Referencia"].ToString().Trim() == CadPrd.Referencia.Trim())
                                {
                                    FindLinha = true;
                                    TabelaFiscal.Rows[A]["QtdeSaida"] = decimal.Parse(TabelaFiscal.Rows[A]["QtdeSaida"].ToString()) + decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString());
                                    break;
                                }
                            }

                            if (!FindLinha)
                            {
                                GrupoPrd.LerDados(CadPrd.IdGrupo);
                                TabelaFiscal.Rows.Add(LstFilial.Text.ToString(), GrupoPrd.Grupo, CadPrd.Referencia.Trim(), CadPrd.Descricao.Trim(), 0, decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString()));
                            }
                        }
                        ProcBar.Value = ProcBar.Value + 1;
                    }
                }

                //Cupom Fiscal
                
                TabItens = ControleOrigem.ConsultaTabela("select t3.Referencia,SUM(T1.qtde) as Qtde from CupomFIscalItens t1" +
                                                         " left join CupomFiscal t2 on (t2.Id_Lanc=t1.Id_Lanc)" +
                                                         " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                                                         " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                                                         " where t2.Status=1" +
                                                         "   and t4.Id_Grupo in (" + CodGrupo + ")" +
                                                         "   and T2.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                                                         " Group By t3.Referencia");
                
                if (TabItens.Tables[0].Rows.Count > 0)
                {
                    ProcBar.Value = 0;
                    ProcBar.Maximum = TabItens.Tables[0].Rows.Count;
                    bool FindLinha;
                    for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                    {
                        CadPrd.LerDados(TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString());

                        if (CadPrd.IdProduto == 0)
                        {
                            MessageBox.Show("Atenção: Referencia: " + TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString() + ", não Localizada no Servidor:", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            FindLinha = false;
                            for (int A = 0; A <= TabelaFiscal.Rows.Count - 1; A++)
                            {
                                if (TabelaFiscal.Rows[A]["Referencia"].ToString().Trim() == CadPrd.Referencia.Trim())
                                {
                                    FindLinha = true;
                                    TabelaFiscal.Rows[A]["QtdeSaida"] = decimal.Parse(TabelaFiscal.Rows[A]["QtdeSaida"].ToString()) + decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString());
                                    break;
                                }
                            }

                            if (!FindLinha)
                            {
                                GrupoPrd.LerDados(CadPrd.IdGrupo);
                                TabelaFiscal.Rows.Add(LstFilial.Text.ToString(), GrupoPrd.Grupo, CadPrd.Referencia.Trim(), CadPrd.Descricao.Trim(), 0, decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString()));
                            }
                        }
                        ProcBar.Value = ProcBar.Value + 1;
                    }
                }

                //-----------------------------
                //Movimento de Entradas
                TabItens = new DataSet();
                TabItens = Controle.ConsultaTabela("select t3.Referencia,SUM(T1.qtde) as Qtde from MvEstoqueItens t1" +
                                                   " left join MvEstoque t2 on (t2.Id_Mov=t1.Id_MOv)" +
                                                   " left join Produtos t3 on (t3.Id_Produto=t1.Id_Produto)" +
                                                   " left join GrupoProduto t4 on (t4.Id_Grupo=t3.Id_Grupo)" +
                                                   " where t2.Status=1 and t2.TpMov='ENTNF'" +
                                                   "   and t2.Id_FilialOrigDest=" + LstFilial.SelectedValue.ToString() +
                                                   "   and t4.Id_Grupo in (" + CodGrupo + ")" +
                                                   "   and T2.DTEntSai >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DTEntSai <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)" +
                                                   " Group By t3.Referencia ");

                if (TabItens.Tables[0].Rows.Count > 0)
                {
                        ProcBar.Value = 0;
                        ProcBar.Maximum = TabItens.Tables[0].Rows.Count;
                        bool FindLinha;
                        for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                        {
                            CadPrd.LerDados(TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString());

                            if (CadPrd.IdProduto == 0)
                            {
                                MessageBox.Show("Atenção: Referencia: " + TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString() + ", não Localizada no Servidor:", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (RelFiscal)
                                {
                                    FindLinha = false;
                                    for (int A = 0; A <= TabelaFiscal.Rows.Count - 1; A++)
                                    {
                                        if (TabelaFiscal.Rows[A]["Referencia"].ToString().Trim() == CadPrd.Referencia.Trim())
                                        {
                                            FindLinha = true;
                                            TabelaFiscal.Rows[A]["QtdeCompra"] = decimal.Parse(TabelaFiscal.Rows[A]["QtdeCompra"].ToString()) + decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString());
                                            break;
                                        }
                                    }

                                    if (!FindLinha)
                                    {
                                        GrupoPrd.LerDados(CadPrd.IdGrupo);
                                        TabelaFiscal.Rows.Add(LstFilial.Text.ToString(), GrupoPrd.Grupo, CadPrd.Referencia.Trim(), CadPrd.Descricao.Trim(), decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString()), 0);
                                    }
                                }
                            }
                            ProcBar.Value = ProcBar.Value + 1;
                        }
                }
                MessageBox.Show("Importação concluida", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro na importação dos Itens", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Controle.ExecutaSQL("DELETE FROM NOTAFISCALITENS WHERE ID_NOTA=" + IdNota.ToString());
                Close();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (Tag)
            {
                for (int I = 0; I <= CkListGrupos.Items.Count - 1; I++)
                    CkListGrupos.SetItemChecked(I, false);
                LblTag.Text = "Seleciona Todos";
                Tag = false;
            }
            else
            {
                for (int I = 0; I <= CkListGrupos.Items.Count - 1; I++)
                    CkListGrupos.SetItemChecked(I, true);
                LblTag.Text = "Desmarca Todos";
                Tag = true;
            }

        }

        
    }
}
