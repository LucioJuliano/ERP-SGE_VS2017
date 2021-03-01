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
    public partial class FrmConfEntrega : Form
    {
        Funcoes Controle = new Funcoes();
        Auditoria RegAuditoria = new Auditoria();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private BindingSource Source_Venda;
        int IdVenda = 0;

        public FrmConfEntrega()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao      = FrmPrincipal.Conexao;
            RegAuditoria.Controle = Controle;
            Chk_Periodo.Checked   = false;
            Dt1.Value             = DateTime.Now.AddDays(-1);
            Dt2.Value             = DateTime.Now;
            Source_Venda          = new BindingSource();
            Rb_Aberto.Checked     = true;
            CamposLista();
            PopularGridVenda();
        }
        private void CamposLista()
        {            
            LstPesqEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstPesqEntregador, "Todas");            
            LstCaixa = FrmPrincipal.PopularCombo("SELECT T1.ID_CAIXA,T2.USUARIO FROM CAIXABALCAO T1 LEFT JOIN USUARIOS T2 ON (T2.ID_USUARIO=T1.ID_USUARIO) WHERE T1.DATA = Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)", LstCaixa);
        }
        private void PopularGridVenda()
        {
            string Filtro = "";
            Filtro = "WHERE T1.ID_ENTREGADOR > 0";
            if (Rb_Aberto.Checked)
                Filtro = "WHERE T1.ID_ENTREGADOR > 0 AND ((T1.STATUS = 2) OR (T1.STATUS = 1 AND T1.TPVENDA IN ('BONIF','EMVF','CO','PR','AM','TROCA')))";
                //Filtro = Filtro + " AND T1.STATUS = 2";
            if (Rb_Entregue.Checked)
                Filtro = Filtro + " AND T1.STATUS = 3";            
            if (TxtPesqNumDoc.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMDOCUMENTO LIKE '%" + TxtPesqNumDoc.Text.Trim() + "%'";
            if (TxtPesqNumVd.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_VENDA =" + TxtPesqNumVd.Text.Trim();
            if (TxtPesqNumMapa.Text.Trim() != "")
                Filtro = Filtro + " AND ME.ID_MAPA=" + TxtPesqNumMapa.Text.Trim();            
            if (TxtPesqPessoa.Text.Trim() != "")
                Filtro = Filtro + " AND T2.RAZAOSOCIAL Like '%" + TxtPesqPessoa.Text.Trim() + "%'";            
            if (int.Parse(LstPesqEntregador.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Entregador=" + LstPesqEntregador.SelectedValue.ToString();

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND T1.PREVENTREGA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.PREVENTREGA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,T1.DATA,CASE T1.STATUS WHEN 0 THEN 'Em Aberto' WHEN 1 THEN 'Confirmado' WHEN 2 THEN 'Faturado' WHEN 3 THEN 'Entregue' WHEN 4 THEN 'Cancelado' END AS STATUS," +
                                             " T1.NUMDOCUMENTO,ME.ID_MAPA, T1.FORMNF,T1.VLRTOTAL,T2.RAZAOSOCIAL as Cliente,T5.ENTREGADOR,T1.PREVENTREGA,RTRIM(T8.USUARIO)+' DT:'+Rtrim(CONVERT(CHAR,T7.DATA,103)) AS USUCAIXA,T6.FANTASIA AS FILIAL,"+
                                             " T4.VENDEDOR,T9.FORMAPGTO,T1.STATUS as CodSta,T1.ID_PESSOA AS CodPessoa,ISNULL(T1.ID_VDDESTINO,0) AS ID_VDDESTINO,ISNULL(T1.ID_VDORIGEM,0) AS ID_VDORIGEM,ISNULL(T1.ID_CAIXA,0) AS ID_CAIXA,T1.TPVENDA FROM MVVENDA T1  " +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                             " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR)"+ 
                                             " LEFT JOIN ENTREGADORES T5 ON (T5.ID_ENTREGADOR=T1.ID_ENTREGADOR)"+
                                             " LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T1.ID_FILIAL)" +
                                             " LEFT JOIN CAIXABALCAO T7 ON (T7.ID_CAIXA=T1.ID_CAIXA)"+
                                             " LEFT JOIN USUARIOS T8 ON (T8.ID_USUARIO=T7.ID_USUARIO) " +
                                             " LEFT JOIN FORMAPAGAMENTO T9 ON (T9.ID_FORMAPGTO=T1.ID_FORMAPGTO) " +
                                             " LEFT JOIN MAPAENTREGA ME ON (ME.ID_MAPA=(SELECT TOP 1 MP.ID_MAPA FROM MAPAENTREGAITENS MP WHERE MP.ID_VENDA=T1.ID_VENDA AND MP.STATUS IN (0,1)))" +                                             
                                             Filtro + " ORDER BY T1.ID_VENDA DESC");
            Source_Venda.DataSource = Tabela;
            Source_Venda.DataMember = Tabela.Tables[0].TableName;
            GridVenda.DataSource = Source_Venda;
            int item = Source_Venda.Find("Id_Venda", IdVenda);
            Source_Venda.Position = item;
            Totalizar();
        }

        private void Totalizar()
        {
            decimal Total = 0;
            for (int I = 0; I <= GridVenda.Rows.Count - 1; I++)
                Total = Total + decimal.Parse(GridVenda.Rows[I].Cells[6].Value.ToString());
            LblTotal.Text = string.Format("{0:N2}", Total);
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
        private void BtnPesqVD_Click(object sender, EventArgs e)
        {
            PopularGridVenda();
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            Confirma_Entrega();
        }

        private void Confirma_Entrega()
        {
            int IdCX = int.Parse(LstCaixa.SelectedValue.ToString());
                        
            if (GridVenda.CurrentRow == null)
                MessageBox.Show("Não existe Registro para Confirmar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                int IdVenda = 0;
                IdVenda = int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString());

                if (int.Parse(GridVenda.CurrentRow.Cells[16].Value.ToString()) > 0 && int.Parse(GridVenda.CurrentRow.Cells[18].Value.ToString()) == 0)
                {
                    MessageBox.Show("Solicite a Confirmação na Filial de Entrega", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (int.Parse(GridVenda.CurrentRow.Cells[14].Value.ToString()) == 3)
                {
                    MessageBox.Show("Entrega ja confirmada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (int.Parse(GridVenda.CurrentRow.Cells[17].Value.ToString()) > 0)
                    {
                        if (!ConfirmaLocalEntrega(IdVenda))
                            return;
                    }
                }
                else if (GridVenda.CurrentRow.Cells[8].Value.ToString().Trim() == "")
                    MessageBox.Show("Entregador não informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                else if (Cb_IncCx.Checked && IdCX == 0)
                    MessageBox.Show("Favor Selecionar o Caixa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    if (IdCX > 0)
                    {
                        if (VerificaCaixa(IdCX))
                        {
                            MessageBox.Show("Entrega não pode ser confirmada e incluida no caixa (caixa ja fechado)", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (int.Parse(GridVenda.CurrentRow.Cells[18].Value.ToString()) > 0)
                    {
                        if (VerificaCaixa(int.Parse(GridVenda.CurrentRow.Cells[18].Value.ToString())))
                        {
                            MessageBox.Show("Entrega não pode ser confirmada, em um caixa ja fechado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    //Verificando o Mapa de Entrega
                    SqlDataReader TabMapa = Controle.ConsultaSQL("SELECT T2.STATUS FROM MapaEntregaItens T1 LEFT JOIN MAPAENTREGA T2 ON (T2.ID_MAPA=T1.ID_MAPA) WHERE T1.STATUS=0 AND T1.ID_VENDA=" + IdVenda.ToString());
                    if (TabMapa.HasRows)
                    {
                        TabMapa.Read();
                        if (TabMapa["STATUS"].ToString() == "0")
                        {
                            MessageBox.Show("Mapa de Entrega não foi Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    string TpVd = GridVenda.CurrentRow.Cells[19].Value.ToString().Trim();
                    if (int.Parse(GridVenda.CurrentRow.Cells[14].Value.ToString()) != 2 && (TpVd == "PV" || TpVd == "TROCA" || TpVd == "OE" || TpVd == "AM"))
                    {
                        MessageBox.Show("Atenção: Movimento não Faturado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (MessageBox.Show("Confirma a Entrega ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //Atualizando o Troca de Estoque
                        MvVenda Vendas  = new MvVenda();
                        Vendas.Controle = Controle;
                        Vendas.LerDados(IdVenda);

                        if (Vendas.TpVenda != "PV" && Vendas.TpVenda != "TROCA" && IdCX > 0)
                        {
                            MessageBox.Show("Atenção: Esse movimento não pode ser incluido no caixa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        decimal VlrVenda = decimal.Parse(GridVenda.CurrentRow.Cells[6].Value.ToString());

                        if (int.Parse(GridVenda.CurrentRow.Cells[17].Value.ToString()) > 0)
                        {
                            if (!ConfirmaLocalEntrega(IdVenda))
                                return;
                        }

                        if (IdCX > 0 && Cb_IncCx.Checked)
                            Controle.ExecutaSQL("Update MvVenda Set Id_Caixa=" + IdCX.ToString() + ",Status=3,DataConfirmacao=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103),PrevEntrega=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) where Id_Venda=" + IdVenda.ToString());
                        else
                            Controle.ExecutaSQL("Update MvVenda Set Status=3,DataConfirmacao=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) where Id_Venda=" + IdVenda.ToString());

                        Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_VENDA IN (SELECT ID_VENDA FROM MVVENDA WHERE TPVENDA='OE' AND VINCULOVD='" + string.Format("{0:D6}", IdVenda.ToString()) + "')");
                        Controle.ExecutaSQL("UPDATE MvVenda Set Faturado=1 Where VinculoVd='" + string.Format("{0:D6}", IdVenda.ToString()) + "'");
                        Controle.ExecutaSQL("Update MapaEntregaItens set Status=1 where Id_Venda=" + IdVenda.ToString() + " and Status=0");

                        //Verificando se credito um credito pro cliente
                        if (VlrVenda < 0 && int.Parse(GridVenda.CurrentRow.Cells[17].Value.ToString()) == 0)
                            Controle.ExecutaSQL("UPDATE PESSOAS SET CREDITO=Round(CREDITO+" + Controle.FloatToStr(-1 * VlrVenda, 2) + ",2) WHERE ID_PESSOA=" + int.Parse(GridVenda.CurrentRow.Cells[15].Value.ToString()));
                        
                        if (Vendas.TpVenda == "CO")
                            Controle.ExecutaSQL("UPDATE PESSOAS SET COMODATO=1 WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString());

                        if (Vendas.TpVenda == "TROCA")
                        {
                            if (Vendas.SemMovEst == 0)
                            {
                                Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                                ControleEstoque.Controle = Controle;

                                SqlDataReader TabEntrada = Controle.ConsultaSQL("SELECT T1.*, '' AS NCM FROM MvVendaItens T1 WHERE T1.TipoItem='E' and T1.Id_Venda=" + Vendas.IdVenda.ToString());
                                if (TabEntrada.HasRows)
                                    ControleEstoque.MovimentoEstoque(TabEntrada, 1, 1, false, Vendas.TpVenda, Vendas.Data,0);
                            }
                        }                        
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString()), GridVenda.CurrentRow.Cells[3].Value.ToString(), 7, "Confirmação da Entrega");
                        MessageBox.Show("Entrega confirmada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridVenda.CurrentRow.Cells[14].Value = "3";
                        GridVenda.CurrentRow.Cells[2].Value = "Entregue";
                        //PopularGridVenda();
                    }
                }
            }
        }

        private bool ConfirmaLocalEntrega(int IdVenda)
        {
            MvVenda Vendas = new MvVenda();
            Vendas.Controle = Controle;
            Vendas.LerDados(IdVenda);

            // Conectando ao Servidor Origem da Venda
            SqlConnection ServidorOrigem;

            Filiais FilialOrigem = new Filiais();
            FilialOrigem.Controle = Controle;
            FilialOrigem.LerDados(Vendas.IdFilialOrigem);

            if (FilialOrigem.ServidorRemoto == "")
            {
                MessageBox.Show("Atenção: Configuração do Servidor de Origem inválido", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            try
            {
                string conexao = "";
                conexao = "Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                ServidorOrigem = new SqlConnection(conexao);
                ServidorOrigem.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor destino, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Funcoes ControleOrigem = new Funcoes();
            ControleOrigem.Conexao = ServidorOrigem;

            Controles.Verificar Verifica = new Verificar();
            Verifica.Controle = ControleOrigem;

            int IdEntregador = Verifica.Verificar_ExisteCadastro("ID_ENTREGADOR", "SELECT ISNULL(ID_ENTREGADOR,0) AS ID_ENTREGADOR FROM PARAMETROS WHERE ID_FILIAL=" + Vendas.IdFilialEntrega.ToString());

            if (IdEntregador == 0)
            {
                MessageBox.Show("Atenção: Entregador não configurado na Filial de Origem da Venda", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            MvVenda MvOrigem = new MvVenda();
            MvOrigem.Controle = ControleOrigem;
            MvOrigem.LerDados(Vendas.IdVdOrigem);
            //            
            if (MvOrigem.Status == 2 || MvOrigem.Status == 3)
            {
                decimal VlrVenda = decimal.Parse(GridVenda.CurrentRow.Cells[6].Value.ToString());
                if (MvOrigem.Status == 3 && Vendas.VlrTotal==0)
                   ControleOrigem.ExecutaSQL("Update MvVenda Set Status=3,Id_Entregador=" + IdEntregador.ToString() + ", DataConfirmacao=Convert(DateTime,'" + Vendas.PrevEntrega.Date.ToShortDateString() + "',103) where Id_Venda=" + MvOrigem.IdVenda.ToString());
                else
                    ControleOrigem.ExecutaSQL("Update MvVenda Set Status=3,Id_Entregador=" + IdEntregador.ToString() + ", PrevEntrega=Convert(DateTime,'" + Vendas.PrevEntrega.Date.ToShortDateString() + "',103), DataConfirmacao=Convert(DateTime,'" + Vendas.PrevEntrega.Date.ToShortDateString() + "',103) where Id_Venda=" + MvOrigem.IdVenda.ToString());
                ControleOrigem.ExecutaSQL("UPDATE MvVenda Set Faturado=1 Where VinculoVd='" + string.Format("{0:D6}", MvOrigem.IdVenda.ToString()) + "'");
                if (MvOrigem.TpVenda=="CO")
                    Controle.ExecutaSQL("UPDATE PESSOAS SET COMODATO=1 WHERE ID_PESSOA=" + MvOrigem.IdPessoa.ToString());

                if (MvOrigem.Status == 2 && VlrVenda < 0 && int.Parse(GridVenda.CurrentRow.Cells[17].Value.ToString()) == 0)
                    Controle.ExecutaSQL("UPDATE PESSOAS SET CREDITO=Round(CREDITO+" + Controle.FloatToStr(-1 * VlrVenda, 2) + ",2) WHERE ID_PESSOA=" + MvOrigem.IdPessoa.ToString());
            }
            return true;
        }

        private bool CancelaLocalEntrega(int IdVenda)
        {
            MvVenda Vendas  = new MvVenda();
            Vendas.Controle = Controle;
            Vendas.LerDados(IdVenda);

            // Conectando ao Servidor Origem da Venda
            SqlConnection ServidorOrigem;

            Filiais FilialOrigem  = new Filiais();
            FilialOrigem.Controle = Controle;
            FilialOrigem.LerDados(Vendas.IdFilialOrigem);

            if (FilialOrigem.ServidorRemoto == "")
            {
                MessageBox.Show("Atenção: Configuração do Servidor de Origem inválido", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            try
            {
                string conexao = "";
                conexao = "Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                ServidorOrigem = new SqlConnection(conexao);
                ServidorOrigem.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor destino, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Funcoes ControleOrigem = new Funcoes();
            ControleOrigem.Conexao = ServidorOrigem;

            MvVenda MvOrigem  = new MvVenda();
            MvOrigem.Controle = ControleOrigem;
            MvOrigem.LerDados(Vendas.IdVdOrigem);
            //
            if (MvOrigem.Status == 3)
            {
                MessageBox.Show("Solicite primeiro o Cancelamento da Entrega na Filial de Origem da Venda ", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            int IdVenda = 0;
            IdVenda = int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString());

            MvVenda Vendas = new MvVenda();
            Vendas.Controle = Controle;

            if (GridVenda.CurrentRow == null)
                MessageBox.Show("Não existe Registro para Confirmar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (int.Parse(GridVenda.CurrentRow.Cells[14].Value.ToString()) != 3)
                {
                    MessageBox.Show("Entrega não foi confirmada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (GridVenda.CurrentRow.Cells[8].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("Entregador não informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (int.Parse(GridVenda.CurrentRow.Cells[14].Value.ToString()) == 3 && (GridVenda.CurrentRow.Cells[19].Value.ToString().Trim() != "AM" && GridVenda.CurrentRow.Cells[19].Value.ToString().Trim() != "BONIF"))
                {
                    Vendas.LerDados(IdVenda);

                    if (Vendas.IdCaixa > 0)
                    {
                        if (VerificaCaixa(Vendas.IdCaixa))
                        {
                            MessageBox.Show("Movimento já Entregue e Caixa já fechado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }                    
                }

                if (MessageBox.Show("Confirma o Cancelamento da Entrega ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (int.Parse(GridVenda.CurrentRow.Cells[17].Value.ToString()) > 0)
                    {
                        if (!CancelaLocalEntrega(IdVenda))
                            return;
                    }
                    decimal VlrVenda = decimal.Parse(GridVenda.CurrentRow.Cells[6].Value.ToString());

                    Controle.ExecutaSQL("Update MvVenda Set ID_Entregador=0,Status=2, DataConfirmacao=Null where Id_Venda=" + IdVenda.ToString());
                    Controle.ExecutaSQL("UPDATE MvVenda Set Faturado=0 Where VinculoVd='" + string.Format("{0:D6}", IdVenda.ToString()) + "'");
                    Controle.ExecutaSQL("Update MapaEntregaItens set Status=2 where Id_Venda=" + IdVenda.ToString() + " and Status=1");

                    //Verificando se credito um credito pro cliente
                    if (VlrVenda < 0 && int.Parse(GridVenda.CurrentRow.Cells[17].Value.ToString()) == 0)
                        Controle.ExecutaSQL("UPDATE PESSOAS SET CREDITO=Round(CREDITO-" + Controle.FloatToStr(-1 * VlrVenda, 2) + ",2) WHERE ID_PESSOA=" + int.Parse(GridVenda.CurrentRow.Cells[15].Value.ToString()));

                    //Atualizando o Troca de Estoque

                    Vendas.LerDados(IdVenda);

                    if (Vendas.TpVenda == "TROCA")
                    {
                        if (Vendas.SemMovEst == 0)
                        {
                            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                            ControleEstoque.Controle = Controle;

                            SqlDataReader TabEntrada = Controle.ConsultaSQL("SELECT T2.* FROM MVVENDA T1 LEFT JOIN MVVENDAITENS T2 ON (T2.ID_VENDA=T1.ID_VENDA) WHERE T2.TipoItem='E' AND T1.ID_VDMASTER=" + Vendas.IdVdMaster.ToString());
                            if (TabEntrada.HasRows)
                                ControleEstoque.MovimentoEstoque(TabEntrada, 1, 2, false, Vendas.TpVenda, Vendas.Data,0);
                        }
                    }

                    //Registrando Movimento de Auditoria
                    FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString()), GridVenda.CurrentRow.Cells[3].Value.ToString(), 7, "Cancelando a confirmação da Entrega");
                    MessageBox.Show("Entrega cancelada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GridVenda.CurrentRow.Cells[14].Value = "2";
                    GridVenda.CurrentRow.Cells[2].Value = "Faturado";
                    PopularGridVenda();
                }
            }
        }
        private void Cb_IncCx_CheckedChanged(object sender, EventArgs e)
        {
            LstCaixa.Enabled = Cb_IncCx.Checked;
            if (!Cb_IncCx.Checked)
                LstCaixa.SelectedValue = 0;
        }
        private void GridVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Confirma_Entrega();
        }
        private void BtnBaixaAut_Click(object sender, EventArgs e)
        {

            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
            ControleEstoque.Controle = Controle;

            int IdCX = int.Parse(LstCaixa.SelectedValue.ToString());

            if (GridVenda.CurrentRow == null)
            {
                MessageBox.Show("Não existe Registro para Confirmar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (Cb_IncCx.Checked && IdCX == 0)
            {
                MessageBox.Show("Favor Selecionar o Caixa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (MessageBox.Show("Atenção: Apenas os movimentos faturamentos são confirmados, Confirma Baixa Geral de Entrega ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlDataReader TabMapa; 
                    for (int I = 0; I <= GridVenda.Rows.Count - 1; I++)
                    {
                        int IdVenda = 0;
                        IdVenda = int.Parse(GridVenda.Rows[I].Cells[0].Value.ToString());
                        decimal VlrVenda = decimal.Parse(GridVenda.Rows[I].Cells[6].Value.ToString());
                        
                        TabMapa = Controle.ConsultaSQL("SELECT T2.STATUS FROM MapaEntregaItens T1 LEFT JOIN MAPAENTREGA T2 ON (T2.ID_MAPA=T1.ID_MAPA) WHERE T1.STATUS=0 AND T1.ID_VENDA=" + IdVenda.ToString());
                        if (TabMapa.HasRows)
                        {
                            TabMapa.Read();
                            if (TabMapa["STATUS"].ToString() == "0")
                                continue;                                
                        }

                        if (IdCX > 0 && Cb_IncCx.Checked)
                            Controle.ExecutaSQL("Update MvVenda Set Id_Caixa=" + IdCX.ToString() + ",Status=3,DataConfirmacao=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) where Id_Venda=" + IdVenda.ToString());
                        else
                            Controle.ExecutaSQL("Update MvVenda Set Status=3,DataConfirmacao=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) where Id_Venda=" + IdVenda.ToString());

                        Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_VENDA IN (SELECT ID_VENDA FROM MVVENDA WHERE TPVENDA='OE' AND VINCULOVD='" + string.Format("{0:D6}", IdVenda.ToString()) + "')");
                        Controle.ExecutaSQL("UPDATE MvVenda Set Faturado=1 Where VinculoVd='" + string.Format("{0:D6}", IdVenda.ToString()) + "'");
                        Controle.ExecutaSQL("Update MapaEntregaItens set Status=1 where Id_Venda=" + IdVenda.ToString() + " and Status=0");

                        //Verificando se credito um credito pro cliente
                        if (VlrVenda < 0)
                            Controle.ExecutaSQL("UPDATE PESSOAS SET CREDITO=Round(CREDITO+" + Controle.FloatToStr(-1 * VlrVenda, 2) + ",2) WHERE ID_PESSOA=" + int.Parse(GridVenda.Rows[I].Cells[15].Value.ToString()));

                        //Atualizando o Troca de Estoque
                        MvVenda Vendas = new MvVenda();
                        Vendas.Controle = Controle;
                        Vendas.LerDados(IdVenda);

                        if (Vendas.TpVenda == "TROCA")
                        {
                            if (Vendas.SemMovEst == 0)
                            {
                                SqlDataReader TabEntrada = Controle.ConsultaSQL("SELECT T1.*, '' AS NCM FROM MvVendaItens T1 WHERE T1.TipoItem='E' and T1.Id_Venda=" + Vendas.IdVenda.ToString());
                                if (TabEntrada.HasRows)
                                    ControleEstoque.MovimentoEstoque(TabEntrada, 1, 1, false, Vendas.TpVenda, Vendas.Data,0);
                            }
                        }
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridVenda.Rows[I].Cells[0].Value.ToString()), GridVenda.Rows[I].Cells[3].Value.ToString(), 7, "Confirmação da Entrega Automatica");                        
                        GridVenda.Rows[I].Cells[14].Value = "3";
                        GridVenda.Rows[I].Cells[2].Value = "Entregue";
                    }
                    MessageBox.Show("Confirmação de Entrega Concluida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        private void BtnCancItemMapa_Click(object sender, EventArgs e)
        {
            if (GridVenda.CurrentRow == null)
                MessageBox.Show("Não existe Registro para Cancelar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (int.Parse(GridVenda.CurrentRow.Cells[14].Value.ToString()) == 3)
                {
                    MessageBox.Show("Movimento já Entregue, Favor Cancelar a Entrega", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (int.Parse(GridVenda.CurrentRow.Cells[14].Value.ToString()) == 2)
                {
                    //Verificando o Mapa de Entrega
                    SqlDataReader TabMapa = Controle.ConsultaSQL("SELECT T2.STATUS FROM MapaEntregaItens T1 LEFT JOIN MAPAENTREGA T2 ON (T2.ID_MAPA=T1.ID_MAPA) WHERE T1.STATUS=0 AND T1.ID_VENDA=" + GridVenda.CurrentRow.Cells[0].Value.ToString());
                    if (TabMapa.HasRows)
                    {
                        TabMapa.Read();
                        if (TabMapa["STATUS"].ToString() == "0")
                        {
                            MessageBox.Show("Mapa de Entrega não foi Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }                   

                    if (MessageBox.Show("Confirma a Retirada da venda do Mapa de Entrega ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Controle.ExecutaSQL("Update MapaEntregaItens set Status=2 where Id_Venda=" + GridVenda.CurrentRow.Cells[0].Value.ToString() + " and Status=0");
                        Controle.ExecutaSQL("Update MVVENDA SET ID_CAIXA=0,ID_ENTREGADOR=0 where Id_Venda=" + GridVenda.CurrentRow.Cells[0].Value.ToString());
                        
                        FrmPrincipal.RegistrarAuditoria(this.Text, int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString()), GridVenda.CurrentRow.Cells[3].Value.ToString(), 7, "Retirado do Mapa de Entrega");
                        MessageBox.Show("Cancelamento Concluido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PopularGridVenda();
                    }
                }
            }
        }

        private bool VerificaCaixa(int IdCaixa)
        {
            SqlDataReader TabDeb;
            TabDeb = Controle.ConsultaSQL("SELECT * FROM CAIXABALCAO WHERE ID_CAIXA=" + IdCaixa.ToString() + " AND STATUS=1");
            if (TabDeb.HasRows)
            {
                MessageBox.Show("Atenção: Caixa já fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int I = 0; I <= GridVenda.Rows.Count - 1; I++)
            {
                if (GridVenda.Rows[I].Cells[3].Value.ToString().Substring(0, 2) == "PV")
                {
                    int IdVenda = 0;
                    IdVenda = int.Parse(GridVenda.Rows[I].Cells[0].Value.ToString());
                    if (int.Parse(GridVenda.Rows[I].Cells[17].Value.ToString()) > 0)
                        ConfirmaLocalEntrega(IdVenda);
                }
            }
            MessageBox.Show("Concluido");
        }       
    }
}
