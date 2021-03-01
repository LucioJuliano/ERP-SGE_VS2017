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
using System.Collections;
using System.Data.SqlClient;
using System.Xml;

namespace ERP_SGE
{
    public partial class FrmBuscaProduto : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public int IdProduto = 0;
        public Produtos CadProd;        
        public ArrayList ListaCodPrd;
        public int IdVendedor      = 0;
        public bool VendDist       = false;
        public bool VerGrpLstVenda = false;
        public bool LstMvEst       = false;
        public decimal VlrPedido   = 0;
        public decimal IdVenda     = 0;
        public int TipoCliente     = 0;
        public bool PedCompra      = false;

        private DataSet TabItens;
        private BindingSource Source_Itens;
        
        public FrmBuscaProduto()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            CadProd = new Produtos();
            Controle.Conexao = FrmPrincipal.Conexao;
            CadProd.Controle = Controle;
            TabItens         = new DataSet();
            Source_Itens     = new BindingSource();
            CadProd.LerDados(0);
            IdProduto  = 0;
            if (VerGrpLstVenda)
                ListaGrupo = FrmPrincipal.PopularCombo("SELECT T1.Id_Grupo,T1.Grupo FROM GrupoProduto T1 WHERE T1.ATIVO=1 AND T1.LISTAVENDA=1 AND EXISTS (SELECT * FROM Produtos T2 WHERE T2.Id_Grupo=T1.Id_Grupo AND T2.Ativo=1) ORDER BY Grupo", ListaGrupo, "Todos");
            else
                ListaGrupo = FrmPrincipal.PopularCombo("SELECT T1.Id_Grupo,T1.Grupo FROM GrupoProduto T1 WHERE T1.ATIVO=1 AND EXISTS (SELECT * FROM Produtos T2 WHERE T2.Id_Grupo=T1.Id_Grupo AND T2.Ativo=1) ORDER BY Grupo", ListaGrupo, "Todos");
            ListaCodPrd = new ArrayList();
            ListaCodPrd.Add("0");

            if (FrmPrincipal.Perfil_Usuario.UsaPrcEspDist == 0)
                GridDados.Columns[12].Visible = false;
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

       /* private void MostraSldEstDep()
        {
            if (FrmPrincipal.Perfil_Usuario.VerSldDeposito == 1)
            {
                
                decimal Saldo = 0;
                for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                {
                    Controles.Serv_SaldoEstoque.ConsultaSaldo wsSaldo = new Controles.Serv_SaldoEstoque.ConsultaSaldo();
                    try
                    {
                        Saldo = 0;
                        wsSaldo.Url = "http://" + FrmPrincipal.URLMatriz + "/WSSaldoEstoque/BuscaSaldoEstoque.asmx?swdl";
                        Saldo = wsSaldo.SaldoEstoque(GridDados.Rows[I].Cells["ColRefPrd"].Value.ToString(), FrmPrincipal.URLMatriz, @"\DEPOSITO");
                        GridDados.Rows[I].Cells["ColSldDeposito"].Value = Saldo;
                        wsSaldo.Dispose();
                    }
                    catch
                    {
                        GridDados.Rows[I].Cells["ColSldDeposito"].Value = 0;
                        wsSaldo.Dispose();
                    }

                }
            }
        }*/
        private void GridDados_KeyDown(object sender, KeyEventArgs e)
        {
            ListaCodPrd.Clear();            
            ListaCodPrd.Add("0");
            
            if (e.KeyCode == Keys.Enter)
            {
                if (GridDados.CurrentRow != null)
                {
                    IdProduto = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    CadProd.LerDados(IdProduto);
                    for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                    {
                        if (GridDados.Rows[I].Selected || decimal.Parse(GridDados.Rows[I].Cells[6].Value.ToString()) > 0)
                        {
                            ListaCodPrd.Add(GridDados.Rows[I].Cells[0].Value.ToString() + "|" + GridDados.Rows[I].Cells[6].Value.ToString() + "|" + GridDados.Rows[I].Cells[14].Value.ToString() + "|" + GridDados.Rows[I].Cells[22].Value.ToString() + "|" + GridDados.Rows[I].Cells[24].Value.ToString());                            
                        }                            
                    }
                    //for (int I = GridDados.SelectedRows.Count-1; I >= 0 ; I--)
                    //    ListaCodPrd.Add(GridDados.SelectedRows[I].Cells[0].Value.ToString());
                    
                }
                else
                {                    
                    IdProduto  = 0;                    
                }
                Close();
            }
        }
        private void TxtReferencia_Validated(object sender, EventArgs e)
        {

            PesquisaProduto();

            /*
            if (TxtReferencia.Text.Trim() != "")
            {                
                DataSet Tabela = new DataSet();
                string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,GRPPRD.GRUPO, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                              " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,0 as SldDep, (SELECT TOP 1 PED1.PREVENTREGA FROM PEDCOMPRA PED1 " +
                              " LEFT JOIN PEDCOMPRAITENS PED2 ON (PED2.ID_DOCUMENTO=PED1.ID_DOCUMENTO) WHERE PED2.ID_PRODUTO=T1.ID_PRODUTO AND PED1.STATUS=1 ORDER BY PED1.PREVENTREGA) AS PREVENTREGA,0 as Qtde," +
                              "CASE ISNULL(T2.PrcEspecial,0)  WHEN 0 THEN T1.PrcEspecial ELSE T2.PRCESPECIAL END AS PrcEspecial," +
                              "CASE ISNULL(T2.PrcVarejo,0)    WHEN 0 THEN T1.PrcVarejo ELSE T2.PrcVarejo END AS PrcVarejo," +
                              "CASE ISNULL(T2.PrcMinimo,0)    WHEN 0 THEN T1.PrcMinimo ELSE T2.PrcMinimo END AS PrcMinimo," +
                              "CASE ISNULL(T2.PrcAtacado,0)   WHEN 0 THEN T1.PrcAtacado ELSE T2.PrcAtacado END AS PrcAtacado, T1.NCM, ISNULL(T2.ID_LANC,0) AS IDPROMOCAO,T1.ATIVO ";

                sSQL = sSQL + " FROM Produtos T1 LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) LEFT JOIN PROMOCOES T2 ON (T2.ID_PRODUTO=T1.Id_Produto AND T2.DTINICIO >= convert(DateTime,convert(char,GETDATE(),103),103) AND T2.DTFINAL <= convert(DateTime,convert(char,GETDATE(),103),103)) ";
                
                if (LstMvEst)
                    sSQL = sSQL + " WHERE GRPPRD.Ativo=1 ";
                else
                    sSQL = sSQL + " WHERE T1.Ativo=1 AND GRPPRD.Ativo=1 ";

                if (TxtReferencia.Text.Length > 11)
                    sSQL = sSQL + string.Format(" and T1.CodBarra LIKE '%{0}%' ", TxtReferencia.Text.Trim());
                else
                    sSQL = sSQL + string.Format(" and T1.Referencia LIKE '%{0}%' ", TxtReferencia.Text.Trim());

                if (int.Parse(ListaGrupo.SelectedValue.ToString()) > 0)
                    sSQL = sSQL + " AND T1.ID_GRUPO=" + ListaGrupo.SelectedValue.ToString();

                if (VerGrpLstVenda)
                    sSQL = sSQL + " AND GRPPRD.LISTAVENDA=1";

                sSQL = sSQL + " ORDER BY T1.Referencia";
                Tabela = Controle.ConsultaTabela(sSQL);
                GridDados.DataSource = Tabela;
                GridDados.DataMember = Tabela.Tables[0].TableName;
                if (GridDados.CurrentRow != null)
                    GridDados.Focus();
                else
                    TxtReferencia.Text = "";

                MostraSldEstDep();
            }*/
        }
        private void TxtDescricao_Validated(object sender, EventArgs e)
        {
            PesquisaProduto();
            /*
            if (TxtDescricao.Text.Trim() != "")
            {
                DataSet Tabela = new DataSet();
                string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,GRPPRD.GRUPO, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                              " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,0 as SldDep, (SELECT TOP 1 PED1.PREVENTREGA FROM PEDCOMPRA PED1 " +
                              " LEFT JOIN PEDCOMPRAITENS PED2 ON (PED2.ID_DOCUMENTO=PED1.ID_DOCUMENTO) WHERE PED2.ID_PRODUTO=T1.ID_PRODUTO AND PED1.STATUS=1 ORDER BY PED1.PREVENTREGA) AS PREVENTREGA,0 as Qtde,"+
                              "CASE ISNULL(T2.PrcEspecial,0)  WHEN 0 THEN T1.PrcEspecial ELSE T2.PRCESPECIAL END AS PrcEspecial," +
                              "CASE ISNULL(T2.PrcVarejo,0)  WHEN 0 THEN T1.PrcVarejo ELSE T2.PrcVarejo END AS PrcVarejo," +
                              "CASE ISNULL(T2.PrcMinimo,0)  WHEN 0 THEN T1.PrcMinimo ELSE T2.PrcMinimo END AS PrcMinimo," +
                              "CASE ISNULL(T2.PrcAtacado,0)  WHEN 0 THEN T1.PrcAtacado ELSE T2.PrcAtacado END AS PrcAtacado, T1.NCM, ISNULL(T2.ID_LANC,0) AS IDPROMOCAO,T1.ATIVO ";
                sSQL = string.Format(sSQL + " FROM Produtos T1 LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) LEFT JOIN PROMOCOES T2 ON (T2.ID_PRODUTO=T1.Id_Produto AND T2.DTINICIO >= convert(DateTime,convert(char,GETDATE(),103),103) AND T2.DTFINAL <= convert(DateTime,convert(char,GETDATE(),103),103)) WHERE T1.Descricao LIKE '%{0}%' ", TxtDescricao.Text.Trim());

                if (LstMvEst)
                    sSQL = sSQL + " AND GRPPRD.Ativo=1 ";
                else
                    sSQL = sSQL + " AND T1.Ativo=1 AND GRPPRD.Ativo=1 ";

                if (int.Parse(ListaGrupo.SelectedValue.ToString()) > 0)
                    sSQL = sSQL + " AND T1.ID_GRUPO=" + ListaGrupo.SelectedValue.ToString();

                if (VerGrpLstVenda)
                    sSQL = sSQL + " AND GRPPRD.LISTAVENDA=1";

                sSQL = sSQL + " ORDER BY T1.Descricao";

                Tabela = Controle.ConsultaTabela(sSQL);
                GridDados.DataSource = Tabela;
                GridDados.DataMember = Tabela.Tables[0].TableName;
                if (GridDados.CurrentRow != null)
                    GridDados.Focus();
                else
                    TxtDescricao.Text = "";

                MostraSldEstDep();
            }*/
        }
        private void TxtRefFornecedor_Validated(object sender, EventArgs e)
        {
            PesquisaProduto();
            /*
            if (TxtRefFornecedor.Text.Trim() != "")
            {
                DataSet Tabela = new DataSet();
                string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,GRPPRD.GRUPO, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                              " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,0 as SldDep,(SELECT TOP 1 PED1.PREVENTREGA FROM PEDCOMPRA PED1 " +
                              " LEFT JOIN PEDCOMPRAITENS PED2 ON (PED2.ID_DOCUMENTO=PED1.ID_DOCUMENTO) WHERE PED2.ID_PRODUTO=T1.ID_PRODUTO AND PED1.STATUS=1 ORDER BY PED1.PREVENTREGA) AS PREVENTREGA,0 as Qtde,"+
                              "CASE ISNULL(T2.PrcEspecial,0)  WHEN 0 THEN T1.PrcEspecial ELSE T2.PRCESPECIAL END AS PrcEspecial," +
                              "CASE ISNULL(T2.PrcVarejo,0)  WHEN 0 THEN T1.PrcVarejo ELSE T2.PrcVarejo END AS PrcVarejo," +
                              "CASE ISNULL(T2.PrcMinimo,0)  WHEN 0 THEN T1.PrcMinimo ELSE T2.PrcMinimo END AS PrcMinimo," +
                              "CASE ISNULL(T2.PrcAtacado,0)  WHEN 0 THEN T1.PrcAtacado ELSE T2.PrcAtacado END AS PrcAtacado, T1.NCM, ISNULL(T2.ID_LANC,0) AS IDPROMOCAO,T1.ATIVO ";

                sSQL = string.Format(sSQL + " FROM Produtos T1 LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) LEFT JOIN PROMOCOES T2 ON (T2.ID_PRODUTO=T1.Id_Produto AND T2.DTINICIO >= convert(DateTime,convert(char,GETDATE(),103),103) AND T2.DTFINAL <= convert(DateTime,convert(char,GETDATE(),103),103)) WHERE T1.RefFornecedor LIKE '%{0}%' ", TxtRefFornecedor.Text.Trim());

                if (LstMvEst)
                    sSQL = sSQL + " AND GRPPRD.Ativo=1 ";
                else
                    sSQL = sSQL + " AND T1.Ativo=1 AND GRPPRD.Ativo=1 ";

                if (int.Parse(ListaGrupo.SelectedValue.ToString()) > 0)
                    sSQL = sSQL + " AND T1.ID_GRUPO=" + ListaGrupo.SelectedValue.ToString();

                if (VerGrpLstVenda)
                    sSQL = sSQL + " AND GRPPRD.LISTAVENDA=1";

                sSQL = sSQL + " ORDER BY T1.RefFornecedor";

                Tabela = Controle.ConsultaTabela(sSQL);

                GridDados.DataSource = Tabela;
                GridDados.DataMember = Tabela.Tables[0].TableName;
                if (GridDados.CurrentRow != null)
                    GridDados.Focus();
                else
                {
                    TxtRefFornecedor.Text = "";
                    TxtReferencia.Focus();
                }

                MostraSldEstDep();
            }
            else
                TxtReferencia.Focus();*/

            
        }
        private void FrmBuscaProduto_Shown(object sender, EventArgs e)
        {
            PVarejo.Visible         = FrmPrincipal.Perfil_Usuario.MostraPreco == 1;
            PAtacado.Visible        = FrmPrincipal.Perfil_Usuario.MostraPreco == 1;
            PMinimo.Visible         = FrmPrincipal.Perfil_Usuario.MostraPreco == 1;
            ColIdPromocao.Visible   = false;            
            BtnLiberar.Visible      = FrmPrincipal.Perfil_Usuario.LiberaEstoque == 1;

            if (PedCompra)
            {
                PSensacional.Visible  = false;
                PEspecial.Visible     = false;
                PVarejo.Visible       = false;
                PMinimo.Visible       = false;
                PAtacado.Visible      = false;
                ColPrcEspDist.Visible = false;
                ColUltCompra.Visible  = true;
            }
        }
        private void BtnLiberar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {                
                FrmAutDebitoPessoa AutDeb = new FrmAutDebitoPessoa();
                AutDeb.FrmPrincipal = FrmPrincipal;
                AutDeb.LiberaPrd = true;
                AutDeb.IdProduto = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                AutDeb.Saldo     = decimal.Parse(GridDados.CurrentRow.Cells[4].Value.ToString());
                AutDeb.ShowDialog();
            }
        }
        private void ListaGrupo_Validated(object sender, EventArgs e)
        {
            if (ListaGrupo.Items.Count > -1)
            {
                if (ListaGrupo.SelectedValue.ToString() != "0")
                    PesquisaProduto();
            }
            /*
            if (ListaGrupo.Items.Count > -1)
            {
                if (ListaGrupo.SelectedValue.ToString() != "0")
                    {
                    DataSet Tabela = new DataSet();
                    string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,GRPPRD.GRUPO, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                              " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,0 as SldDep,(SELECT TOP 1 PED1.PREVENTREGA FROM PEDCOMPRA PED1 " +
                              " LEFT JOIN PEDCOMPRAITENS PED2 ON (PED2.ID_DOCUMENTO=PED1.ID_DOCUMENTO) WHERE PED2.ID_PRODUTO=T1.ID_PRODUTO AND PED1.STATUS=1 ORDER BY PED1.PREVENTREGA) AS PREVENTREGA,0 as Qtde,"+
                              "CASE ISNULL(T2.PrcEspecial,0)  WHEN 0 THEN T1.PrcEspecial ELSE T2.PRCESPECIAL END AS PrcEspecial," +
                              "CASE ISNULL(T2.PrcVarejo,0)  WHEN 0 THEN T1.PrcVarejo ELSE T2.PrcVarejo END AS PrcVarejo," +
                              "CASE ISNULL(T2.PrcMinimo,0)  WHEN 0 THEN T1.PrcMinimo ELSE T2.PrcMinimo END AS PrcMinimo," +
                              "CASE ISNULL(T2.PrcAtacado,0)  WHEN 0 THEN T1.PrcAtacado ELSE T2.PrcAtacado END AS PrcAtacado, T1.NCM, ISNULL(T2.ID_LANC,0) AS IDPROMOCAO,T1.ATIVO ";

                    sSQL = sSQL + " FROM Produtos T1 LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) LEFT JOIN PROMOCOES T2 ON (T2.ID_PRODUTO=T1.Id_Produto AND T2.DTINICIO >= convert(DateTime,convert(char,GETDATE(),103),103) AND T2.DTFINAL <= convert(DateTime,convert(char,GETDATE(),103),103)) WHERE T1.ID_GRUPO=" + ListaGrupo.SelectedValue.ToString();

                    if (!LstMvEst)
                        sSQL = sSQL + " AND T1.Ativo=1";

                    if (VerGrpLstVenda)
                        sSQL = sSQL + " AND GRPPRD.LISTAVENDA=1";
                    sSQL = sSQL +" ORDER BY T1.DESCRICAO";

                    Tabela = Controle.ConsultaTabela(sSQL);
                    GridDados.DataSource = Tabela;
                    GridDados.DataMember = Tabela.Tables[0].TableName;
                    MostraSldEstDep();
                }
            }*/
        }
        private void BtnSaldoGeral_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Favor selecionar apenas um produto");
                    return;
                }

                BtnSaldoGeral.Enabled = false;
                BtnLiberar.Enabled    = false;
                FrmSaldoGeralEstoque FrmSaldo = new FrmSaldoGeralEstoque();
                FrmSaldo.TxtCod.Text  = GridDados.CurrentRow.Cells[0].Value.ToString();
                FrmSaldo.TxtRef.Text  = GridDados.CurrentRow.Cells[2].Value.ToString();
                FrmSaldo.TxtDesc.Text = GridDados.CurrentRow.Cells[1].Value.ToString();
                FrmSaldo.FrmPrincipal = FrmPrincipal;
                FrmSaldo.ShowDialog();
                BtnSaldoGeral.Enabled = true;
                BtnLiberar.Enabled = true;                
            }
        }
        private void GridDados_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            
            if (GridDados.Rows[e.RowIndex].Cells[14].Value.ToString() != "0")
                GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Silver;
            else if (GridDados.Rows[e.RowIndex].Cells[15].Value.ToString() == "0")
                GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            else
                GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;            
        }

        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {            
            BoxPromocao.Visible = false;
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[14].Value.ToString() != "0")
                {
                    BoxPromocao.Visible = true;
                    LblPrcSensac.Text   = "P.S: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["P_Sensacional"].Value.ToString()));
                    LblPrcEspecial.Text = "P.E: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["P_Especial"].Value.ToString()));
                    LblPrcVarejo.Text   = "P.V: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["P_Varejo"].Value.ToString()));
                    LblPrcMinimo.Text   = "P.M: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["P_Minimo"].Value.ToString()));
                    LblPrcDist.Text     = "P.D: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["P_Atacado"].Value.ToString()));
                }
            }
        }
        private void PesquisaProduto()
        {

            if (TxtReferencia.Text.Trim() != "" || TxtDescricao.Text.Trim() != "" || TxtRefFornecedor.Text.Trim() != "" || int.Parse(ListaGrupo.SelectedValue.ToString()) > 0)
            {
                //" (SELECT TOP 1 PED1.PREVENTREGA FROM PEDCOMPRA PED1 " +
                DataSet Tabela = new DataSet();
                string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,GRPPRD.GRUPO, CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1 " +
                              " LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)  WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE," +
                              " (SELECT TOP 1 rtrim(convert(char,PED1.PREVENTREGA,103))+' Qtde: '+' Qtde:'+ Rtrim(convert(int,(SELECT SUM(Ped3.Qtde) FROM PEDCOMPRAITENS PED3 LEFT JOIN PEDCOMPRA PED4 ON (PED4.ID_DOCUMENTO=PED3.ID_DOCUMENTO) WHERE PED3.ID_PRODUTO=T1.ID_PRODUTO AND PED4.STATUS=1 AND PED4.PrevEntrega=PED1.PrevEntrega))) FROM PEDCOMPRA PED1" +
                              " LEFT JOIN PEDCOMPRAITENS PED2 ON (PED2.ID_DOCUMENTO=PED1.ID_DOCUMENTO) WHERE PED2.ID_PRODUTO=T1.ID_PRODUTO AND PED1.STATUS=1 ORDER BY PED1.PREVENTREGA) AS PREVENTREGA,0 as Qtde," +
                              "T1.PrcSensacional,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado,T1.PrcEspDist,T1.NCM, 0 AS IDPROMOCAO, T1.ATIVO, 0.00 as P_Sensacional,0.00 AS P_Especial,0.00 AS P_Varejo,0.00 AS P_Minimo,0.00 AS P_Atacado,'' as NmPromocao,0 as PromQtdeitem," +
                              "(SELECT TOP 1 B2.DATA FROM BalancoItens B1 left join Balanco B2 on (B2.Id_Balanco=B1.Id_Balanco) where B1.Id_Produto=T1.ID_PRODUTO and B2.Status=1 order by B2.Data desc) AS UltBalanco, 0.00 as PComPromocao," +
                              "(SELECT TOP 1 CONVERT(CHAR,M2.DATA,103) FROM MvEstoqueItens M1 LEFT JOIN MvEstoque M2 on(M2.Id_Mov = M1.Id_Mov) where M2.Status = 1 and M2.TPMov = 'COMPR' AND M1.Id_Produto=T1.Id_Produto ORDER BY M2.ID_MOV DESC) AS DTULTCOMPRA"; 

                sSQL = sSQL + " FROM Produtos T1 LEFT JOIN GRUPOPRODUTO GRPPRD ON (GRPPRD.ID_GRUPO=T1.ID_GRUPO) ";

                if (LstMvEst)
                    sSQL = sSQL + " WHERE GRPPRD.Ativo=1 ";
                else
                    sSQL = sSQL + " WHERE T1.Ativo=1 AND GRPPRD.Ativo=1 ";

                if (TxtReferencia.Text.Trim() != "")
                {
                    if (TxtReferencia.Text.Length > 11)
                        sSQL = sSQL + string.Format(" and T1.CodBarra LIKE '%{0}%' ", TxtReferencia.Text.Trim());
                    else
                        sSQL = sSQL + string.Format(" and T1.Referencia LIKE '%{0}%' ", TxtReferencia.Text.Trim());
                }

                if (TxtDescricao.Text.Trim() != "")
                    sSQL = string.Format(sSQL + " and T1.Descricao LIKE '%{0}%' ", TxtDescricao.Text.Trim());

                if (TxtRefFornecedor.Text.Trim() != "")
                    sSQL = string.Format(sSQL + " and T1.RefFornecedor LIKE '%{0}%' ", TxtRefFornecedor.Text.Trim());

                if (int.Parse(ListaGrupo.SelectedValue.ToString()) > 0)
                    sSQL = sSQL + " AND T1.ID_GRUPO=" + ListaGrupo.SelectedValue.ToString();

                if (VerGrpLstVenda)
                    sSQL = sSQL + " AND GRPPRD.LISTAVENDA=1";

                if (TxtReferencia.Text.Trim() != "")
                    sSQL = sSQL + " ORDER BY T1.Referencia";
                else
                    sSQL = sSQL + " ORDER BY T1.Descricao";

                TabItens = Controle.ConsultaTabela(sSQL);                
                Source_Itens.DataSource = TabItens;
                Source_Itens.DataMember = TabItens.Tables[0].TableName;
                GridDados.DataSource = Source_Itens;                

                if (GridDados.CurrentRow != null)
                    GridDados.Focus();
                else
                    TxtReferencia.Text = "";

                if (!LstMvEst)
                    VerificaPromocao();

                //MostraSldEstDep();
            }
        }

        private void VerificaPromocao()
        {
            String sSQL = "Select * from PromocaoProdutosItens T1" +
                          "  Left Join PromocaoProdutos t2 on (t2.Id_Promocao=T1.Id_Promocao) " +
                          " Where convert(DateTime,convert(char,GETDATE(),103),103) >= CONVERT(DATETIME,T2.DTINICIO,103) " +
                          "  AND convert(DateTime,convert(char,GETDATE(),103),103) <= CONVERT(DATETIME,T2.DTFINAL,103) " +
                          "  AND t2.ATIVO=1 AND T2.TIPOPROMOCAO in (0,4) AND T1.ATIVO=1";

            DateTime Data = FrmPrincipal.DtHrServidor();
            String sSQLDia = "";
            if (Data.DayOfWeek == DayOfWeek.Monday)
                sSQLDia = sSQLDia  + " and t2.Segunda=1";
            if (Data.DayOfWeek == DayOfWeek.Tuesday)
                sSQLDia = sSQLDia + " and t2.Terca=1";                
            if (Data.DayOfWeek == DayOfWeek.Wednesday)
                sSQLDia = sSQLDia + " and t2.Quarta=1";                
            if (Data.DayOfWeek == DayOfWeek.Thursday)
                sSQLDia = sSQLDia + " and t2.Quinta=1";                
            if (Data.DayOfWeek == DayOfWeek.Friday)
                sSQLDia = sSQLDia + " and t2.Sexta=1";                
            if (Data.DayOfWeek == DayOfWeek.Saturday)
                sSQLDia = sSQLDia + " and t2.Sabado=1";                
            if (Data.DayOfWeek == DayOfWeek.Sunday)
                sSQLDia = sSQLDia + " and t2.Domingo=1";

            if (FrmPrincipal.Perfil_Usuario.IdPromocao > 0)
                sSQLDia = sSQLDia + " or (T2.PorUsuario = 1 and t1.Id_Promocao = " + FrmPrincipal.Perfil_Usuario.IdPromocao.ToString() + ")";
            else
                sSQLDia = sSQLDia + " and T2.PorUsuario = 0";

            SqlDataReader TabPromocao = Controle.ConsultaSQL(sSQL+ sSQLDia);
            bool DiaPromocao=false;
            while (TabPromocao.Read())
            {
                if (TabPromocao["TipoCliente"].ToString() == "1" && TipoCliente != 0)
                    continue;

                if (TabPromocao["TipoCliente"].ToString() == "2" && (TipoCliente != 3 || TipoCliente != 6 || TipoCliente != 7))
                    continue;

                DiaPromocao = false;
                if (Data.DayOfWeek == DayOfWeek.Monday && int.Parse(TabPromocao["Segunda"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Tuesday && int.Parse(TabPromocao["Terca"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Wednesday && int.Parse(TabPromocao["Quarta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Thursday && int.Parse(TabPromocao["Quinta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Friday && int.Parse(TabPromocao["Sexta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Saturday && int.Parse(TabPromocao["Sabado"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Sunday && int.Parse(TabPromocao["Domingo"].ToString()) == 1)
                    DiaPromocao = true;

                if (DiaPromocao)
                {
                    for (int I = GridDados.Rows.Count - 1; I >= 0; I--)
                    {
                        if (int.Parse(TabPromocao["ID_PRODUTO"].ToString()) == int.Parse(GridDados.Rows[I].Cells[0].Value.ToString()))
                        {
                            GridDados.Rows[I].Cells["ColIdPromocao"].Value = int.Parse(TabPromocao["ID_PROMOCAO"].ToString());
                            GridDados.Rows[I].Cells["NmPromocao"].Value    = TabPromocao["Descricao"].ToString().Trim();
                            GridDados.Rows[I].Cells["P_Sensacional"].Value = decimal.Parse(TabPromocao["PRCSENSACIONAL"].ToString());
                            GridDados.Rows[I].Cells["P_Especial"].Value    = decimal.Parse(TabPromocao["PRCESPECIAL"].ToString());
                            GridDados.Rows[I].Cells["P_Varejo"].Value      = decimal.Parse(TabPromocao["PRCVAREJO"].ToString());
                            GridDados.Rows[I].Cells["P_Minimo"].Value      = decimal.Parse(TabPromocao["PRCMINIMO"].ToString());
                            GridDados.Rows[I].Cells["P_Atacado"].Value     = decimal.Parse(TabPromocao["PRCATACADO"].ToString());
                            GridDados.Rows[I].Cells["PComProm"].Value      = decimal.Parse(TabPromocao["PComissao"].ToString());
                            if (int.Parse(TabPromocao["TipoPromocao"].ToString()) == 4)
                                GridDados.Rows[I].Cells["PromQtdeKit"].Value = 4;
                            else
                                GridDados.Rows[I].Cells["PromQtdeKit"].Value = 0;
                        }
                    }
                }
            }

            //Verificando Promoção Qtde de Produto
            TabPromocao = Controle.ConsultaSQL("Select * from Produtos T1" +
                                               " Left Join PromocaoProdutos t2 on (t2.Id_Promocao=T1.Id_Promocao) " +
                                               " Where convert(DateTime,convert(char,GETDATE(),103),103) >= CONVERT(DATETIME,T2.DTINICIO,103) " +
                                               "  AND convert(DateTime,convert(char,GETDATE(),103),103) <= CONVERT(DATETIME,T2.DTFINAL,103) AND t2.ATIVO=1 AND T2.TIPOPROMOCAO=1" + sSQLDia);
            DiaPromocao = false;
            while (TabPromocao.Read())
            {

                if (TabPromocao["TipoCliente"].ToString() == "1" && TipoCliente != 0)
                    continue;

                if (TabPromocao["TipoCliente"].ToString() == "2" && (TipoCliente != 3 || TipoCliente != 6 || TipoCliente != 7))
                    continue;

                DiaPromocao = false;
                if (Data.DayOfWeek == DayOfWeek.Monday && int.Parse(TabPromocao["Segunda"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Tuesday && int.Parse(TabPromocao["Terca"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Wednesday && int.Parse(TabPromocao["Quarta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Thursday && int.Parse(TabPromocao["Quinta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Friday && int.Parse(TabPromocao["Sexta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Saturday && int.Parse(TabPromocao["Sabado"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Sunday && int.Parse(TabPromocao["Domingo"].ToString()) == 1)
                    DiaPromocao = true;

                //if (!DiaPromocao)
                //    return;

                for (int I = GridDados.Rows.Count - 1; I >= 0; I--)
                {
                    if (int.Parse(TabPromocao["ID_PRODUTO"].ToString()) == int.Parse(GridDados.Rows[I].Cells[0].Value.ToString()))
                    {
                        if (!DiaPromocao)
                        {
                            DataRow item = TabItens.Tables[0].Rows[I];
                            if (item != null)
                                TabItens.Tables[0].Rows.Remove(item);
                        }
                        else
                        {
                            GridDados.Rows[I].Cells["ColIdPromocao"].Value = int.Parse(TabPromocao["ID_PROMOCAO"].ToString());
                            GridDados.Rows[I].Cells["PComProm"].Value      = decimal.Parse(TabPromocao["PComissao"].ToString());
                            GridDados.Rows[I].Cells["PromQtdeKit"].Value   = 1;
                        }
                    }
                }
            }

            //Verificando Promoção Valor Pedido
            sSQL = "Select t1.*,t2.*,(select isnull(sum(Qtde),0)+1 from MvVendaItens where Id_Promocao=t1.id_promocao and id_Venda=" + IdVenda.ToString() + ") as QtdeProm," +
                 " (select isnull(sum(VlrTotal),0) from MvVendaItens where Id_Promocao=t1.id_promocao and id_Venda=" + IdVenda.ToString() + ") as TotalItem from PromocaoProdutosItens T1" +
                 " Left Join PromocaoProdutos t2 on (t2.Id_Promocao=T1.Id_Promocao) " +
                 " Where convert(DateTime,convert(char,GETDATE(),103),103) >= CONVERT(DATETIME,T2.DTINICIO,103) " +
                 "  AND convert(DateTime,convert(char,GETDATE(),103),103) <= CONVERT(DATETIME,T2.DTFINAL,103) " +
                 "  AND t2.ATIVO=1 AND T2.TIPOPROMOCAO=2 AND T1.ATIVO=1";

            

            if (FrmPrincipal.Perfil_Usuario.IdPromocao > 0)
                sSQL = sSQL + " or (T2.PorUsuario = 1 and t1.Id_Promocao = " + FrmPrincipal.Perfil_Usuario.IdPromocao.ToString() + ")";
            else
                sSQL = sSQL + " and T2.PorUsuario = 0";

            TabPromocao = Controle.ConsultaSQL(sSQL);

            DiaPromocao = false;
            while (TabPromocao.Read())
            {

                if (TabPromocao["TipoCliente"].ToString() == "1" && TipoCliente != 0)
                    continue;

                if (TabPromocao["TipoCliente"].ToString() == "2" && (TipoCliente != 3 || TipoCliente != 6 || TipoCliente != 7))
                    continue;

                DiaPromocao = false;
                
                if (Data.DayOfWeek == DayOfWeek.Monday && int.Parse(TabPromocao["Segunda"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Tuesday && int.Parse(TabPromocao["Terca"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Wednesday && int.Parse(TabPromocao["Quarta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Thursday && int.Parse(TabPromocao["Quinta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Friday && int.Parse(TabPromocao["Sexta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Saturday && int.Parse(TabPromocao["Sabado"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Sunday && int.Parse(TabPromocao["Domingo"].ToString()) == 1)
                    DiaPromocao = true;

                if (DiaPromocao)
                {
                    for (int I = GridDados.Rows.Count - 1; I >= 0; I--)
                    {
                        //if ((VlrPedido - decimal.Parse(TabPromocao["TotalItem"].ToString())) < (decimal.Parse(TabPromocao["VlrPedido"].ToString()) * decimal.Parse(TabPromocao["QtdeProm"].ToString())))
                        //if (int.Parse(TabPromocao["ID_PRODUTO"].ToString()) == int.Parse(GridDados.Rows[I].Cells[0].Value.ToString()) && VlrPedido >= decimal.Parse(TabPromocao["VLRPEDIDO"].ToString()))
                        if (int.Parse(TabPromocao["ID_PRODUTO"].ToString()) == int.Parse(GridDados.Rows[I].Cells[0].Value.ToString()) && (VlrPedido - decimal.Parse(TabPromocao["TotalItem"].ToString())) >= (decimal.Parse(TabPromocao["VlrPedido"].ToString()) * decimal.Parse(TabPromocao["QtdeProm"].ToString())))
                        {
                            GridDados.Rows[I].Cells["ColIdPromocao"].Value = int.Parse(TabPromocao["ID_PROMOCAO"].ToString());
                            GridDados.Rows[I].Cells["NmPromocao"].Value    = TabPromocao["Descricao"].ToString().Trim();
                            GridDados.Rows[I].Cells["P_Sensacional"].Value = decimal.Parse(TabPromocao["PRCSENSACIONAL"].ToString());
                            GridDados.Rows[I].Cells["P_Especial"].Value    = decimal.Parse(TabPromocao["PRCESPECIAL"].ToString());
                            GridDados.Rows[I].Cells["P_Varejo"].Value      = decimal.Parse(TabPromocao["PRCVAREJO"].ToString());
                            GridDados.Rows[I].Cells["P_Minimo"].Value      = decimal.Parse(TabPromocao["PRCMINIMO"].ToString());
                            GridDados.Rows[I].Cells["P_Atacado"].Value     = decimal.Parse(TabPromocao["PRCATACADO"].ToString());
                            GridDados.Rows[I].Cells["PComProm"].Value      = decimal.Parse(TabPromocao["PComissao"].ToString());
                        }
                    }
                }
            }

            //Verificando Promoção Valor Produto
            sSQL = "Select * from PromocaoProdutosItens T1" +
                 " Left Join PromocaoProdutos t2 on (t2.Id_Promocao=T1.Id_Promocao) " +
                 " Where convert(DateTime,convert(char,GETDATE(),103),103) >= CONVERT(DATETIME,T2.DTINICIO,103) " +
                 "  AND convert(DateTime,convert(char,GETDATE(),103),103) <= CONVERT(DATETIME,T2.DTFINAL,103) " +
                 "  AND t2.ATIVO=1 AND T2.TIPOPROMOCAO=3 AND T1.ATIVO=1" +
                 "  AND EXISTS (SELECT ID_PRODUTO FROM MVVENDAITENS T3 WHERE T3.ID_PRODUTO=T2.ID_PRODUTO AND ID_VENDA=" + IdVenda.ToString() + ")";


            if (FrmPrincipal.Perfil_Usuario.IdPromocao > 0)
                sSQL = sSQL + " or (T2.PorUsuario = 1 and t1.Id_Promocao = " + FrmPrincipal.Perfil_Usuario.IdPromocao.ToString() + ")";
            else
                sSQL = sSQL + " and T2.PorUsuario = 0";

            TabPromocao = Controle.ConsultaSQL(sSQL);

            DiaPromocao = false;
            while (TabPromocao.Read())
            {

                if (TabPromocao["TipoCliente"].ToString() == "1" && TipoCliente != 0)
                    continue;

                if (TabPromocao["TipoCliente"].ToString() == "2" && (TipoCliente != 3 || TipoCliente != 6 || TipoCliente != 7))
                    continue;

                DiaPromocao = false;
                               

                if (Data.DayOfWeek == DayOfWeek.Monday && int.Parse(TabPromocao["Segunda"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Tuesday && int.Parse(TabPromocao["Terca"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Wednesday && int.Parse(TabPromocao["Quarta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Thursday && int.Parse(TabPromocao["Quinta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Friday && int.Parse(TabPromocao["Sexta"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Saturday && int.Parse(TabPromocao["Sabado"].ToString()) == 1)
                    DiaPromocao = true;
                if (Data.DayOfWeek == DayOfWeek.Sunday && int.Parse(TabPromocao["Domingo"].ToString()) == 1)
                    DiaPromocao = true;

                if (DiaPromocao)
                {
                    for (int I = GridDados.Rows.Count - 1; I >= 0; I--)
                    {
                        if (int.Parse(TabPromocao["ID_PRODUTO"].ToString()) == int.Parse(GridDados.Rows[I].Cells[0].Value.ToString()))
                        {
                            GridDados.Rows[I].Cells["ColIdPromocao"].Value = int.Parse(TabPromocao["ID_PROMOCAO"].ToString());
                            GridDados.Rows[I].Cells["NmPromocao"].Value = TabPromocao["Descricao"].ToString().Trim();
                            GridDados.Rows[I].Cells["P_Sensacional"].Value = decimal.Parse(TabPromocao["PRCSENSACIONAL"].ToString());
                            GridDados.Rows[I].Cells["P_Especial"].Value = decimal.Parse(TabPromocao["PRCESPECIAL"].ToString());
                            GridDados.Rows[I].Cells["P_Varejo"].Value = decimal.Parse(TabPromocao["PRCVAREJO"].ToString());
                            GridDados.Rows[I].Cells["P_Minimo"].Value = decimal.Parse(TabPromocao["PRCMINIMO"].ToString());
                            GridDados.Rows[I].Cells["P_Atacado"].Value = decimal.Parse(TabPromocao["PRCATACADO"].ToString());
                            GridDados.Rows[I].Cells["PComProm"].Value = decimal.Parse(TabPromocao["PComissao"].ToString());
                        }
                    }
                }
            }
        }
    }
}
