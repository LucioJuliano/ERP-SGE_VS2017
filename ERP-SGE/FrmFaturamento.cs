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
    public partial class FrmFaturamento : Form
    {
        Funcoes Controle = new Funcoes();        
        Pessoas CadPessoa = new Pessoas();
        TabelasAux TabAux = new TabelasAux();
        MvVenda Venda = new MvVenda();

        public TelaPrincipal FrmPrincipal;        
        public string TipoMov;

        public FrmFaturamento()
        {
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao    = FrmPrincipal.Conexao;            
            TabAux.Controle     = Controle;
            CadPessoa.Controle  = Controle;
            Venda.Controle      = Controle;
            Dt1.Value           = DateTime.Now;
            Dt2.Value           = DateTime.Now;
            Chk_Periodo.Checked = false;
            CamposLista();
            PopularGrid();
        }
        private void CamposLista()
        {
            LstEntrega     = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstEntrega, "Todas");
            LstPesqFilial  = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqFilial, "Todas");
            LstPesqFilial.SelectedValue = 0;
            LstEntrega.SelectedValue    = FrmPrincipal.IdFilialConexao;

        }
        private void PopularGrid()
        {
            string Filtro = "";
            Filtro = "WHERE T1.STATUS=1 AND T1.TPVENDA IN ('PV','VF','AM','CO','OE','TROCA','BONIF')";          

            if (TxtPesqNumDoc.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMDOCUMENTO LIKE '%" + TxtPesqNumDoc.Text.Trim() + "%'";
            if (TxtPesqNumVd.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_VENDA =" + TxtPesqNumVd.Text.Trim();
            if (TxtPesqPessoa.Text.Trim() != "")
                Filtro = Filtro + " AND T2.RAZAOSOCIAL Like '%" + TxtPesqPessoa.Text.Trim() + "%'";
            if (int.Parse(LstEntrega.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FilialEntrega=" + LstEntrega.SelectedValue.ToString();
            if (int.Parse(LstPesqFilial.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FILIAL=" + LstPesqFilial.SelectedValue.ToString();
            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";
            
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,T1.DATA,T1.NUMDOCUMENTO,T1.ID_PESSOA,T2.RAZAOSOCIAL as Cliente,T1.VLRTOTAL,T1.ID_FORMAPGTO,T7.FORMAPGTO,T4.VENDEDOR,T1.PREVENTREGA,"+
                                             "T6.FANTASIA AS FILIAL,T5.USUARIO,T8.FANTASIA AS FILIALENTREGA,T1.DtEnvioRec FROM MVVENDA T1  " +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                             " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                                             " LEFT JOIN USUARIOS T5 ON (T5.ID_USUARIO=T1.ID_USUARIO) " +
                                             " LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T1.ID_FILIAL) " +
                                             " LEFT JOIN FORMAPAGAMENTO T7 ON (T7.ID_FORMAPGTO=T1.ID_FORMAPGTO) " +
                                             " LEFT JOIN EMPRESA_FILIAL T8 ON (T8.ID_FILIAL=T1.ID_FILIALENTREGA) " + Filtro + " ORDER BY T1.ID_VENDA DESC");

            BindingSource Source = new BindingSource();
            Source.DataSource    = Tabela;
            Source.DataMember    = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
        }
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
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
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (GridDados.SelectedRows.Count > 0)
            {
                if (VerificaSelecao())
                {
                    //Totalizando o Movimento
                    CadPessoa.LerDados(int.Parse(GridDados.SelectedRows[0].Cells[3].Value.ToString()));
                    decimal Total   = 0;
                    string RefVd    = "";
                    string RefDocVd = ""; 
                   
                    for (int I = 0; I <= GridDados.SelectedRows.Count - 1; I++)
                    {
                        Total = Total + decimal.Parse(GridDados.SelectedRows[I].Cells[5].Value.ToString());
                        if (RefDocVd == "")
                            RefDocVd = GridDados.SelectedRows[I].Cells[2].Value.ToString().Trim();
                        else
                            RefDocVd = RefDocVd + "," + GridDados.SelectedRows[I].Cells[2].Value.ToString().Trim();

                        if (RefVd == "")
                            RefVd = GridDados.SelectedRows[I].Cells[0].Value.ToString().Trim();
                        else
                            RefVd = RefVd + "," + GridDados.SelectedRows[I].Cells[0].Value.ToString().Trim();                        
                    } 
                    // Fechamento Financeiro                                        
                    Venda.LerDados(int.Parse(GridDados.SelectedRows[0].Cells[0].Value.ToString()));
                    if (Venda.Status == 2)
                    {
                        MessageBox.Show("Movimento já Faturado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Venda.Status == 3)
                    {
                        MessageBox.Show("Movimento já Entregue", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Venda.Status == 4)
                    {
                        MessageBox.Show("Movimento foi cancelado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Venda.Status == 0)
                    {
                        MessageBox.Show("Favor confirmar o movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //if (!ValidadeMovimento())
                    //    return;

                    if (Total <= 0 || Venda.TpVenda == "BONIF" || Venda.TpVenda == "CO")
                    {
                        FrmPrincipal.RegistrarAuditoria(this.Text, Venda.IdVenda, Venda.NumDocumento, 6, "Faturamento do Movimento");
                        Controle.ExecutaSQL("UPDATE MvVenda Set Status=2,Id_VdMaster=" + GridDados.SelectedRows[0].Cells[0].Value.ToString() + " Where Id_Venda in (" + RefVd + ")");
                        MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        FrmFechaMovimento FrmFecha = new FrmFechaMovimento();
                        FrmFecha.FrmPrincipal = FrmPrincipal;
                        FrmFecha.TxtPessoa.Text = CadPessoa.RazaoSocial;
                        FrmFecha.TxtVlrSubTotal.Value = Total;
                        FrmFecha.TxtVlrDesconto.Value = 0;
                        FrmFecha.TxtVlrTotal.Value = Total;
                        FrmFecha.IdPessoa = CadPessoa.IdPessoa;
                        FrmFecha.NumVd = int.Parse(GridDados.SelectedRows[0].Cells[0].Value.ToString());
                        FrmFecha.Referente = "PEDIDO DE VENDA";
                        FrmFecha.NumDoc = GridDados.SelectedRows[0].Cells[2].Value.ToString();
                        FrmFecha.IdFilial = Venda.IdFilial;
                        FrmFecha.Obs = "Faturamento das Vendas:" + RefDocVd;
                        FrmFecha.PagRec = 2;
                        if (Venda.IdFormaPgto > 0)
                            FrmFecha.IdPgto = Venda.IdFormaPgto;
                        else
                            FrmFecha.IdPgto = CadPessoa.IdFormaPgto;
                        FrmFecha.LstFormaPgto.Enabled = CadPessoa.BloqFormaPgto == 0;
                        FrmFecha.ShowDialog();

                        if (FrmFecha.Concluido)
                        {
                            //Registrando Movimento de Auditoria
                            Venda.Concluir(2);
                            FrmPrincipal.RegistrarAuditoria(this.Text, Venda.IdVenda, Venda.NumDocumento, 6, "Faturamento do Movimento");
                            Controle.ExecutaSQL("UPDATE MvVenda Set Status=2,Id_FormaPgto=" + int.Parse(FrmFecha.LstFormaPgto.SelectedValue.ToString()) + ",Id_VdMaster=" + FrmFecha.NumVd.ToString() + " Where Status=1 and Id_Venda in (" + RefVd + ")");
                            MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        FrmFecha.Dispose();
                    }
                }
                else
                    MessageBox.Show("Favor Selecionar pedidos do mesmo cliente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            PopularGrid();
        }
        private bool VerificaSelecao()
        {
            int Id = int.Parse(GridDados.SelectedRows[0].Cells[3].Value.ToString());
            for (int I = 0; I <= GridDados.SelectedRows.Count - 1; I++)
            {
                if (int.Parse(GridDados.SelectedRows[I].Cells[3].Value.ToString()) != Id)
                    return false;
            }
            return true;
        }

        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }

        private bool ValidadeMovimento()
        {
            TimeSpan Dias = DateTime.Now.Subtract(Venda.Data);
            if (Dias.Days > 5 && Venda.TpVenda != "OC")
            {
                MessageBox.Show("Validade do movimento é apenas de 5 dias", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
                return true;
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            Venda.LerDados(int.Parse(GridDados.SelectedRows[0].Cells[0].Value.ToString()));

            FrmMvVendas FrmVD  = new FrmMvVendas();
            FrmVD.FrmPrincipal = this.FrmPrincipal;
            FrmVD.TipoMov      = Venda.TpVenda;
            FrmVD.Inicializar();
            
            FrmVD.Vendas.LerDados(int.Parse(GridDados.SelectedRows[0].Cells[0].Value.ToString()));

            if (FrmVD.Vendas.IdVenda > 0)
                FrmVD.ImprimirVenda(FrmVD.Vendas.IdVenda);
            FrmVD.Dispose();
        }

        private void LstPesqFilial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
