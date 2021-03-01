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
using System.Collections;


namespace ERP_SGE
{
    public partial class FrmBalanco : Form
    {

        Funcoes Controle = new Funcoes();
        Balanco MvBalanco = new Balanco();
        BalancoItens Itens = new BalancoItens();
        Auditoria RegAuditoria = new Auditoria();
        Produtos CadPrd = new Produtos();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        // Tabelas
        private DataSet TabItens;
        private BindingSource Source_Itens;

        public FrmBalanco()
        {
            InitializeComponent();
        }
        private void FrmBalanco_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles
            Controle.Conexao      = FrmPrincipal.Conexao;
            MvBalanco.Controle    = Controle;
            Itens.Controle        = Controle;
            CadPrd.Controle       = Controle;
            RegAuditoria.Controle = Controle;
            Rb_Pendente.Checked   = true;
            Chk_Periodo.Checked   = false;            
            // Instanciando as Tabelas
            TabItens = new DataSet();
            Source_Itens = new BindingSource();
            PopularGrid();
            Hab_Botoes();
        }
        

        private void PopularGrid()
        {

            string Filtro = "";
            if (Rb_Todos.Checked)
                Filtro = "WHERE STATUS <= 2";
            else if (Rb_Pendente.Checked)
                Filtro = "WHERE STATUS = 0";
            else if (Rb_Concluido.Checked)
                Filtro = "WHERE STATUS = 1";
                                    

            if (TxtPesqResp.Text != "")
                Filtro = Filtro + " AND Responsavel='" + TxtPesqResp.Text.Trim() + "'";

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqNumBalanco.Text != "")
                Filtro = Filtro + " AND ID_BALANCO=" + TxtPesqNumBalanco.Text;

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Balanco,Data,CASE STATUS WHEN 1 THEN 'Concluido' else 'Pendente' end as STATUS,Responsavel,ID_ENTRADA,ID_SAIDA,ID_SALDO FROM BALANCO " +
                                             Filtro + " ORDER BY ID_BALANCO DESC");

            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Balanco", MvBalanco.IdBalanco);
            Source.Position = item;
            Hab_Botoes();
        }
        private void Hab_Botoes()
        {
            BtnConfirmar.Enabled = !StaFormEdicao && MvBalanco.Status == 0;
            BtnCancMov.Enabled   = !StaFormEdicao && MvBalanco.Status == 1;            
            BtnImprimir.Enabled  = !StaFormEdicao;
            PnlDados1.Enabled    = StaFormEdicao && MvBalanco.Status == 0;
            
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            MvBalanco.LerDados(Isn);
            TxtCodigo.Text      = MvBalanco.IdBalanco.ToString();
            TxtData.Value       = MvBalanco.Data;
            TxtResponsavel.Text = MvBalanco.Responsavel;
            TxtObservacao.Text  = MvBalanco.Observacao;            
            Hab_Botoes();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularCampos(0);
            PopularGridItens();
            Hab_Botoes();
            FrmPrincipal.ControleBotoes(true);
            TxtResponsavel.Focus();
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                if (MvBalanco.Status == 1)
                    MessageBox.Show("Balanço já Concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    PopularGridItens();
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    TxtResponsavel.Focus();
                    Hab_Botoes();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            
                MvBalanco.IdBalanco   = int.Parse(TxtCodigo.Text);
                MvBalanco.Data        = TxtData.Value;
                MvBalanco.Observacao  = TxtObservacao.Text;
                MvBalanco.Responsavel = TxtResponsavel.Text;
                StaFormEdicao = false;
                MvBalanco.GravarDados();

                //Registrando Movimento de Auditoria
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, MvBalanco.IdBalanco, MvBalanco.Responsavel.ToString(), 1, "Iniciando a Digitação do Balanço");
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, MvBalanco.IdBalanco, MvBalanco.Responsavel.ToString(), 2, "Alterando o Balanço");
                PopularGrid();
                PopularCampos(MvBalanco.IdBalanco);
                PopularGridItens();
                FrmPrincipal.ControleBotoes(false);                
                GridItens.Focus();            
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {

                MvBalanco.IdBalanco = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                MvBalanco.LerDados(MvBalanco.IdBalanco);

                if (MvBalanco.Status > 0)
                    MessageBox.Show("Balanço já Concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (FrmPrincipal.Perfil_Usuario.ExcluirReg == 0)
                    {
                        FrmAutorizacao Autorizacao = new FrmAutorizacao();
                        Autorizacao.FrmPrincipal = FrmPrincipal;
                        Autorizacao.ShowDialog();
                        //Verificando se o Acesso foi liberado
                        if (Autorizacao.AcessoOk)
                        {
                            if (Autorizacao.Usuario.ExcluirReg == 0)
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MvBalanco.Excluir();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, MvBalanco.IdBalanco, MvBalanco.Responsavel, 3, "Excluindo o Balanço");
                        PopularGrid();
                        LimpaDados();
                        GridDados.Focus();
                    }
                }
            }
            else
                MessageBox.Show("Não existe Registro para Excluir", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
                Paginas.SelectTab(0);
                GridDados.Focus();
                LimpaDados();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.ToString());
            }
        }
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void LimpaDados()
        {
            TxtCodigo.Text      = "0";
            TxtData.Value       = DateTime.Now;
            TxtResponsavel.Text = "";
            TxtObservacao.Text  = "";
            MvBalanco.LerDados(0);
            PopularGridItens();
            Hab_Botoes();
        }
        private void Paginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Paginas.SelectedIndex == 0)
                PopularGrid();

            if (GridDados.CurrentRow != null)
            {
                if (Paginas.SelectedIndex == 0)
                    BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
                else
                {
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                    PopularGridItens();
                    Hab_Botoes();
                }
            }
            Hab_Botoes();
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }
        private void Frm_Activated(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
            FrmPrincipal.ClickBtnNovo += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
            FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }

        // Controle dos Itens
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela(" select T1.ID_ITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.SALDOESTOQUE FROM BALANCOITENS T1" +
                                               "  left join produtos t2 on (t2.id_produto=t1.id_produto)" +
                                               " WHERE T1.Id_Balanco=" + MvBalanco.IdBalanco.ToString() + " ORDER BY T1.ID_ITEM");
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;

            int item = Source_Itens.Find("ID_Item", Itens.IdItem);
            Source_Itens.Position = item;            
            Hab_Botoes();
        }

        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    // e.SuppressKeyPress = true;
                    IncluirItem();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (MvBalanco.Status == 1)
                {
                    MessageBox.Show("Atenção: Balanço ja Concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                    return;
                }
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MvBalanco.IdBalanco > 0 && !StaFormEdicao)
            {
                decimal Qtde = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                Itens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                Itens.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                Itens.Qtde = Qtde;
                Itens.GravarDados();
                PopularGridItens();
                GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];
                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", Itens.IdItem, MvBalanco.IdBalanco.ToString(), 3, "Alteração Item:" + Itens.IdProduto.ToString());

            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MvBalanco.IdBalanco > 0)
                    IncluirItem();
                else
                    Source_Itens.CancelEdit();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (MvBalanco.Status == 1)
                MessageBox.Show("Balanço já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (GridItens.CurrentRow != null)
                {
                    if (StaFormEdicao)
                        MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        if (MvBalanco.IdBalanco > 0 && !StaFormEdicao)
                        {
                            if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {                                
                                Itens.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                                Itens.Excluir();
                                //Registrando Movimento de Auditoria
                                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", Itens.IdItem, MvBalanco.IdBalanco.ToString(), 3, "Excluindo do Item");
                                Itens.IdItem = 0;
                                GridItens.Rows.Remove(GridItens.CurrentRow);                                
                            }
                        }
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                
                if (MvBalanco.Status == 1)
                {
                    MessageBox.Show("Balanço já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                }
                else
                {
                    if (MvBalanco.IdBalanco > 0)
                    {
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.LstMvEst        = true;
                        BuscaPrd.ShowDialog();
                        Verificar ExistePrd = new Verificar();
                        ExistePrd.Controle = Controle;

                        for (int I = 0; I <= BuscaPrd.ListaCodPrd.Count - 1; I++)
                        {
                            ArrayList PrdQtde = new ArrayList(BuscaPrd.ListaCodPrd[I].ToString().Split(char.Parse("|")));
                            BuscaPrd.CadProd.LerDados(int.Parse(PrdQtde[0].ToString()));
                            BuscaPrd.IdProduto = BuscaPrd.CadProd.IdProduto;
                            if (BuscaPrd.IdProduto > 0)
                            {
                                if (BuscaPrd.CadProd.ProdutoKit == 1)
                                {
                                    MessageBox.Show("Esse produto é um Kit e não pode ser movimentando: " + BuscaPrd.CadProd.Descricao.Trim(), "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    continue;
                                }
                                if (!ExistePrd.VerificarExite_LancProduto("SELECT * FROM BalancoItens WHERE Id_Balanco=" + MvBalanco.IdBalanco.ToString() + " and Id_Produto=" + BuscaPrd.IdProduto.ToString()))
                                {
                                    Itens.LerDados(0);
                                    Itens.IdBalanco    = MvBalanco.IdBalanco;
                                    Itens.IdProduto    = BuscaPrd.IdProduto;
                                    Itens.SaldoEstoque = BuscaPrd.CadProd.SaldoEstoque;
                                    if (decimal.Parse(PrdQtde[1].ToString()) > 0)
                                        Itens.Qtde = decimal.Parse(PrdQtde[1].ToString());
                                    else
                                        Itens.Qtde = 1;
                                    Itens.GravarDados();
                                    //Registrando Movimento de Auditoria
                                    FrmPrincipal.RegistrarAuditoria(this.Text + " Item", Itens.IdItem, MvBalanco.IdBalanco.ToString(), 1, "Incluindo Item " + Itens.IdProduto.ToString() + "  Qtde:" + Itens.Qtde.ToString());
                                }
                                else
                                {
                                    MessageBox.Show("Produto já cadastrado no Movimento: " + BuscaPrd.CadProd.Descricao.Trim(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    continue;
                                }
                            }
                            else
                                continue;
                        }
                        PopularGridItens();
                        if (GridItens.CurrentRow != null)
                            GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];
                        BuscaPrd.Dispose();
                    }
                }
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            string sSql = "SELECT T4.Grupo,T3.Referencia,T3.Descricao,T1.Qtde,T1.SALDOESTOQUE,T2.* FROM BalancoItens T1"+
                          " LEFT JOIN Balanco T2 ON (T2.Id_Balanco=T1.Id_Balanco)"+
                          " LEFT JOIN Produtos T3 ON (T3.Id_Produto=T1.Id_Produto)"+
                          " LEFT JOIN GRUPOProduto T4 ON (T4.ID_GRUPO=T3.Id_Grupo)"+
                          " WHERE T1.ID_BALANCO=" + MvBalanco.IdBalanco.ToString();
            sSql = sSql + " ORDER BY T4.GRUPO,T3.DESCRICAO";

            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelBalanco RelMapa = new Relatorios.RelBalanco();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(sSql);
            RelMapa.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelMapa;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;            
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (MvBalanco.Status == 1)
            {
                MessageBox.Show("Balanço já Concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MvEstoque MvEntrada   = new MvEstoque();
            MvEstoque MvSaida     = new MvEstoque();
            MvEstoque LancBalanco = new MvEstoque();

            MvEstoqueItens EntradaItens = new MvEstoqueItens();            
            MvEstoqueItens SaidaItens   = new MvEstoqueItens();
            MvEstoqueItens LancBalItens = new MvEstoqueItens();

            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
            ControleEstoque.Controle = Controle;

            MvEntrada.Controle    = Controle;
            MvSaida.Controle      = Controle;
            LancBalanco.Controle  = Controle;
            EntradaItens.Controle = Controle;
            SaidaItens.Controle   = Controle;
            LancBalItens.Controle = Controle;
            MvSaida.LerDados(0);
            MvEntrada.LerDados(0);
            LancBalanco.LerDados(0);

            BtnConfirmar.Enabled  = false;
            Application.DoEvents();

            if (MessageBox.Show("Confirma a Conclusão do Balanço?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BarProc.Visible = true;
                //Gerando o Movimento de Saida
                DataSet TabSaida = new DataSet();
                TabSaida = Controle.ConsultaTabela("SELECT * FROM BALANCOITENS WHERE SALDOESTOQUE > 0 AND ID_BALANCO=" + MvBalanco.IdBalanco.ToString());
                if (TabSaida.Tables[0].Rows.Count > 0)
                {
                    BarProc.Value = 0;
                    BarProc.Maximum = TabSaida.Tables[0].Rows.Count;

                    MvSaida.LerDados(0);
                    MvSaida.TpMov        = "ACESA";
                    MvSaida.Observacao   = "Movimento Gerado Automatico Pelo Sistema para Ajuste do Estoque Ref. Balanço:" + string.Format("{0:D6}", MvBalanco.IdBalanco);
                    MvSaida.Data         = MvBalanco.Data;
                    MvSaida.IdFilial     = FrmPrincipal.IdFilialConexao;
                    MvSaida.DtEmissao    = MvBalanco.Data;
                    MvSaida.DtEntSai     = MvBalanco.Data;
                    MvSaida.Solicitante  = MvBalanco.Responsavel;
                    MvSaida.Documento    = "BALANÇO AUT.";
                    MvSaida.NumDocumento = string.Format("{0:D6}", MvBalanco.IdBalanco);
                    MvSaida.GravarDados();                    

                    for (int I=0; I <= TabSaida.Tables[0].Rows.Count-1;I++)
                    {
                        SaidaItens.LerDados(0);
                        SaidaItens.IdMov     = MvSaida.IdMov;
                        SaidaItens.IdProduto = int.Parse(TabSaida.Tables[0].Rows[I]["ID_PRODUTO"].ToString());
                        SaidaItens.Qtde      = decimal.Parse(TabSaida.Tables[0].Rows[I]["SALDOESTOQUE"].ToString());
                        SaidaItens.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                        Application.DoEvents();
                    }
                }
                //Gerando o Movimento de Entrada
                DataSet TabEntrada = new DataSet();
                TabEntrada = Controle.ConsultaTabela("SELECT * FROM BALANCOITENS WHERE SALDOESTOQUE < 0 AND ID_BALANCO=" + MvBalanco.IdBalanco.ToString());
                if (TabEntrada.Tables[0].Rows.Count > 0)
                {
                    BarProc.Value = 0;
                    BarProc.Maximum = TabEntrada.Tables[0].Rows.Count;

                    MvEntrada.LerDados(0);
                    MvEntrada.TpMov        = "ACEEN";
                    MvEntrada.Observacao   = "Movimento Gerado Automatico Pelo Sistema para Ajuste do Estoque Ref. Balanço:" + string.Format("{0:D6}", MvBalanco.IdBalanco);
                    MvEntrada.Data         = MvBalanco.Data;
                    MvEntrada.IdFilial     = FrmPrincipal.IdFilialConexao;
                    MvEntrada.DtEmissao    = MvBalanco.Data;
                    MvEntrada.DtEntSai     = MvBalanco.Data;
                    MvEntrada.Solicitante  = MvBalanco.Responsavel;
                    MvEntrada.Documento    = "BALANÇO AUT.";
                    MvEntrada.NumDocumento = string.Format("{0:D6}",MvBalanco.IdBalanco);
                    MvEntrada.GravarDados();

                    for (int I = 0; I <= TabEntrada.Tables[0].Rows.Count - 1; I++)
                    {
                        EntradaItens.LerDados(0);
                        EntradaItens.IdMov     = MvEntrada.IdMov;
                        EntradaItens.IdProduto = int.Parse(TabEntrada.Tables[0].Rows[I]["ID_PRODUTO"].ToString());
                        EntradaItens.Qtde      = -1*decimal.Parse(TabEntrada.Tables[0].Rows[I]["SALDOESTOQUE"].ToString());
                        EntradaItens.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                        Application.DoEvents();
                    }
                }
                //Verificando os Lançamentos de Saida
                DataSet TabSaldo = new DataSet();
                TabSaldo = Controle.ConsultaTabela("SELECT * FROM BALANCOITENS WHERE ID_BALANCO=" + MvBalanco.IdBalanco.ToString());
                if (TabSaldo.Tables[0].Rows.Count > 0)
                {
                    BarProc.Value = 0;
                    BarProc.Maximum = TabSaldo.Tables[0].Rows.Count;

                    LancBalanco.LerDados(0);
                    LancBalanco.TpMov        = "BALAN";
                    LancBalanco.Observacao   = "Movimento Gerado Automatico Lançametno do Saldo de Balanço Ref. Balanço:" + string.Format("{0:D6}", MvBalanco.IdBalanco); 
                    LancBalanco.Data         = MvBalanco.Data;
                    LancBalanco.IdFilial     = FrmPrincipal.IdFilialConexao;
                    LancBalanco.DtEmissao    = MvBalanco.Data;
                    LancBalanco.DtEntSai     = MvBalanco.Data;
                    LancBalanco.Solicitante  = MvBalanco.Responsavel;
                    LancBalanco.Documento    = "BALANÇO AUT.";
                    LancBalanco.NumDocumento = string.Format("{0:D6}", MvBalanco.IdBalanco);
                    LancBalanco.GravarDados();

                    for (int I = 0; I <= TabSaldo.Tables[0].Rows.Count - 1; I++)
                    {
                        LancBalItens.LerDados(0);
                        LancBalItens.IdMov     = LancBalanco.IdMov;
                        LancBalItens.IdProduto = int.Parse(TabSaldo.Tables[0].Rows[I]["ID_PRODUTO"].ToString());
                        LancBalItens.Qtde      = decimal.Parse(TabSaldo.Tables[0].Rows[I]["QTDE"].ToString());
                        LancBalItens.GravarDados();
                        BarProc.Value = BarProc.Value + 1;
                        Application.DoEvents();
                    }
                }

                //Concluindo o Movimento de Entrada
                SqlDataReader Tab;
                if (MvEntrada.IdMov > 0)
                {
                    Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvEntrada.IdMov.ToString());                    
                    ControleEstoque.MovimentoEstoque(Tab, 1, 1, false, MvEntrada.TpMov, MvEntrada.DtEntSai, MvEntrada.IdFilial);
                    MvEntrada.Concluir();
                }
                //Concluindo o Movimento de Saida
                if (MvSaida.IdMov > 0)
                {
                    Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvSaida.IdMov.ToString());                    
                    ControleEstoque.MovimentoEstoque(Tab, 2, 1, false, MvSaida.TpMov, MvSaida.DtEntSai, MvSaida.IdFilial);
                    MvSaida.Concluir();
                }

                //Concluindo o Movimento do Novo Saldo
                if (LancBalanco.IdMov > 0)
                {
                    Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + LancBalanco.IdMov.ToString());
                    ControleEstoque.MovimentoEstoque(Tab, 1, 1, false, LancBalanco.TpMov, LancBalanco.DtEntSai, LancBalanco.IdFilial);
                    LancBalanco.Concluir();
                }
                Controle.ExecutaSQL("UPDATE BALANCO SET STATUS=1,ID_ENTRADA=" + MvEntrada.IdMov.ToString() + ",ID_SAIDA=" + MvSaida.IdMov.ToString() + ",ID_SALDO=" + LancBalanco.IdMov.ToString() + " WHERE ID_BALANCO=" + MvBalanco.IdBalanco.ToString());
                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", MvBalanco.IdBalanco, MvBalanco.IdBalanco.ToString(), 4, "Concluisão do Balanço");
                MessageBox.Show("Balanço Concluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BarProc.Visible = false;
                PopularCampos(MvBalanco.IdBalanco);            
            }
        }

        private void BtnCancMov_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Confirma o Cancelamento do Balanço ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                MvEstoque MvEntrada   = new MvEstoque();
                MvEstoque MvSaida     = new MvEstoque();
                MvEstoque LancBalanco = new MvEstoque();
                MvEntrada.Controle    = Controle;
                MvSaida.Controle      = Controle;
                LancBalanco.Controle  = Controle;

                Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                ControleEstoque.Controle = Controle;

                //Concluindo o Movimento de Entrada
                SqlDataReader Tab;
                if (MvBalanco.IdEntrada > 0)
                {
                    MvEntrada.LerDados(MvBalanco.IdEntrada);
                    Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvEntrada.IdMov.ToString());
                    ControleEstoque.MovimentoEstoque(Tab, 1, 2, false, MvEntrada.TpMov, MvEntrada.DtEntSai,MvEntrada.IdFilial);
                    MvEntrada.Excluir();
                }
                //Concluindo o Movimento de Saida
                if (MvBalanco.IdSaida > 0)
                {
                    MvSaida.LerDados(MvBalanco.IdSaida);
                    Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MvSaida.IdMov.ToString());
                    ControleEstoque.MovimentoEstoque(Tab, 2, 2, false, MvSaida.TpMov, MvSaida.DtEntSai, MvSaida.IdFilial);
                    MvSaida.Excluir();
                }
                //Concluindo o Movimento do Novo Saldo
                if (MvBalanco.IdSaldo > 0)
                {
                    LancBalanco.LerDados(MvBalanco.IdSaldo);
                    Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + LancBalanco.IdMov.ToString());
                    ControleEstoque.MovimentoEstoque(Tab, 1, 2, false, LancBalanco.TpMov, LancBalanco.DtEntSai,LancBalanco.IdFilial);
                    LancBalanco.Excluir();
                }
                Controle.ExecutaSQL("UPDATE BALANCO SET STATUS=0,ID_ENTRADA=0,ID_SAIDA=0,ID_SALDO=0 WHERE ID_BALANCO=" + MvBalanco.IdBalanco.ToString());
                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", MvBalanco.IdBalanco, MvBalanco.IdBalanco.ToString(), 4, "Cancelamento do Balanço");
                MessageBox.Show("Cancelamento do Balanço Concluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopularCampos(MvBalanco.IdBalanco);
            }

        }
                        
    }
}
