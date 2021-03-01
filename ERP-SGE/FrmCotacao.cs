using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class FrmCotacao : Form
    {
        Funcoes Controle = new Funcoes();
        Cotacao MvCotacao = new Cotacao();
        CotacaoItens ItemCotacao = new CotacaoItens();
        CotacaoPessoas PessoaCotacao = new CotacaoPessoas();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        // Tabelas
        private DataSet TabItens;
        private DataSet TabParticipantes;
        private BindingSource Source_Itens;
        private BindingSource Source_Participantes;
        
        public FrmCotacao()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            
            //Instanciando os Controles
            Controle.Conexao       = FrmPrincipal.Conexao;
            MvCotacao.Controle     = Controle;
            ItemCotacao.Controle   = Controle;
            PessoaCotacao.Controle = Controle;
            MvCotacao.IdCotacao    = 0;            
            Chk_Periodo.Checked    = false;
            Rb_EmAberto.Checked    = true;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            PopularGrid();
            // Instanciando as Tabelas
            TabItens = new DataSet();
            Source_Itens = new BindingSource();
            TabParticipantes = new DataSet();
            Source_Participantes = new BindingSource();
        }
        private void PopularGrid()
        {
            string Filtro = "";
            if (Rb_EmAberto.Checked)
                Filtro = " WHERE Status=0";
            else
                Filtro = Filtro + " WHERE Status=1";
            if (Chk_Periodo.Checked)
                Filtro = Filtro + " and Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqResp.Text.Trim() != "")
                Filtro = Filtro + " and Responsavel like '%" + TxtPesqResp.Text.Trim() + "%'";
            if (TxtPesqDocum.Text.Trim() != "")
                Filtro = Filtro + " and Documento like '%" + TxtPesqDocum.Text.Trim() + "%'";

            try
            {
                DataSet Tabela = new DataSet();
                Tabela = Controle.ConsultaTabela(string.Format("SELECT ID_COTACAO,CASE STATUS WHEN 0 THEN 'Em Aberto' WHEN 1 THEN 'Concluido' END AS Status,Data,Documento,Responsavel,VlrTotal FROM COTACAO " + Filtro + " ORDER BY DATA DESC"));
                BindingSource Source = new BindingSource();
                Source.DataSource = Tabela;
                Source.DataMember = Tabela.Tables[0].TableName;
                GridDados.DataSource = Source;
                int item = Source.Find("ID_Cotacao", MvCotacao.IdCotacao);
                Source.Position = item;            
            }
            catch
            {
                MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            MvCotacao.LerDados(Isn);
            TxtCodigo.Text = MvCotacao.IdCotacao.ToString();
            TxtData.Value = MvCotacao.Data;
            if (MvCotacao.Tipo == 0) Rb_Tipo1.Checked = true; else Rb_Tipo2.Checked = true;
            TxtReponsavel.Text = MvCotacao.Responsavel;
            TxtDocumento.Text = MvCotacao.Documento;
            TxtVlrTotal.Value = MvCotacao.VlrTotal;
            TxtObservacao.Text = MvCotacao.Observacao;
            if (MvCotacao.Status == 1) TxtDataConclusao.Text = MvCotacao.DataConclusao.Date.ToShortDateString();
            Hab_Botoes();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularGridItens();
            FrmPrincipal.ControleBotoes(true);
            TxtReponsavel.Focus();
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
                if (MvCotacao.Status == 1)
                   MessageBox.Show("Cotação já Encerrada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                else
                {
                    PopularGridItens();
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    TxtReponsavel.Focus();
                    Hab_Botoes();
                }

            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtReponsavel.Text.Trim() != "")
            {
                MvCotacao.IdCotacao = int.Parse(TxtCodigo.Text);
                MvCotacao.Responsavel= TxtReponsavel.Text;
                MvCotacao.Documento = TxtDocumento.Text;
                MvCotacao.Observacao = TxtObservacao.Text;
                MvCotacao.Data = TxtData.Value;                
                if (Rb_Tipo1.Checked) MvCotacao.Tipo = 0; else MvCotacao.Tipo = 1;
                StaFormEdicao = false;
                if (MvCotacao.Concluido == 1)
                    MvCotacao.Concluido = 0;
                MvCotacao.GravarDados();
                PopularGrid();
                PopularCampos(MvCotacao.IdCotacao);
                PopularGridItens();               
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Responsável não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtReponsavel.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                MvCotacao.IdCotacao = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                MvCotacao.LerDados(MvCotacao.IdCotacao);
                if (MvCotacao.Status == 1)                
                    MessageBox.Show("Cotação já Encerrada, para poder excluir você deve cancelar a cotação", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MvCotacao.IdCotacao = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        MvCotacao.Excluir();
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
            TxtCodigo.Text = "0";
            TxtData.Value = DateTime.Now;
            TxtReponsavel.Text = "";
            TxtDocumento.Text = "";
            TxtObservacao.Text = "";
            TxtVlrTotal.Value = 0;
            TxtDataConclusao.Text = "";
            Rb_Tipo1.Checked = true;
            MvCotacao.LerDados(0);
            PopularGridItens();
            Hab_Botoes();
        }
        private void Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[0].Value.ToString() != "")
                    BtnEditar_Click(FrmPrincipal.BtnEditar, null);
            }

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
                    PagItens.SelectTab(0);
                    PopularGridItens();
                    Hab_Botoes();
                }
            }
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
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T3.RAZAOSOCIAL FROM COTACAOITENS T1" +
                                               "  LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                                               "  LEFT JOIN PESSOAS  T3 ON (T3.ID_PESSOA=T1.ID_PESSOA) WHERE T1.ID_COTACAO=" + MvCotacao.IdCotacao.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("ID_Item",ItemCotacao.IdItem);
            Source_Itens.Position = item;
            Hab_Botoes();                        
        }
        private void PopularGridPessoa()
        {
            ItemCotacao.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));                      
            TabParticipantes = Controle.ConsultaTabela("SELECT T1.ID_PESSOA,T2.RAZAOSOCIAL,T1.VLRUNITARIO FROM COTACAOPESSOAS T1" +
                                                       "  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                                       " WHERE T1.ID_ITEM=" + ItemCotacao.IdItem.ToString());
            Source_Participantes.DataSource = TabParticipantes;
            Source_Participantes.DataMember = TabParticipantes.Tables[0].TableName;
            GridParticipantes.DataSource    = Source_Participantes;
            Navegador.BindingSource = Source_Participantes;
            int item = Source_Participantes.Find("ID_Pessoa", PessoaCotacao.IdPessoa);
            Source_Participantes.Position = item;
            Hab_Botoes();          
        }
        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                //e.SuppressKeyPress = true;
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                    IncluirItem();                
            }
        }        
        private void GridParticipantes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                if (GridParticipantes.CurrentRow == null)
                    IncluirPessoa();
                else if (GridParticipantes.Rows.Count - 1 == GridParticipantes.CurrentRow.Index)
                    IncluirPessoa();                
            }
        }
        private void PagItens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PagItens.SelectedIndex == 0)
                PopularGridItens();
            else
            {
                if (GridItens.CurrentRow != null)
                {
                    PopularGridPessoa();
                    GridParticipantes.Focus();
                }
                else
                {
                    MessageBox.Show("Incluir um produto para poder vincular um participante", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PagItens.SelectTab(0);
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (MvCotacao.Status == 1)
                {
                    MessageBox.Show("Cotação já Encerrada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit(); 
                    e.Cancel = true;
                }
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MvCotacao.IdCotacao > 0 && !StaFormEdicao)
            {
                ItemCotacao.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                ItemCotacao.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                ItemCotacao.Qtde   = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                ItemCotacao.GravarDados();
                PopularGridItens();
            }
        }
        private void GridParticipantes_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Participantes.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (MvCotacao.Status == 1)
                {
                    MessageBox.Show("Cotação já Encerrada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    Source_Participantes.CancelEdit();
                    e.Cancel = true;
                }
            }
        }
        private void GridParticipantes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MvCotacao.IdCotacao > 0 && !StaFormEdicao)
            {
                PessoaCotacao.IdPessoa = int.Parse(GridParticipantes.CurrentRow.Cells[0].Value.ToString());
                PessoaCotacao.LerDados(ItemCotacao.IdItem);                
                PessoaCotacao.VlrUnitario = decimal.Parse(GridParticipantes.CurrentRow.Cells[2].Value.ToString());
                PessoaCotacao.Incluir = false;
                PessoaCotacao.GravarDados();
                if (MvCotacao.Concluido == 1)
                {
                    MvCotacao.Concluido = 0;
                    MvCotacao.GravarDados();
                    Hab_Botoes();
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (PagItens.SelectedIndex == 0)
                {
                    if (MvCotacao.IdCotacao > 0)
                        IncluirItem();
                    else
                        Source_Itens.CancelEdit(); 
                }
                else
                {
                    if (ItemCotacao.IdItem > 0)
                        IncluirPessoa();
                    else
                        Source_Participantes.CancelEdit();
                }
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (MvCotacao.Status == 1)
                MessageBox.Show("Cotação já Encerrada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (PagItens.SelectedIndex == 0)
                {
                    if (GridItens.CurrentRow != null)
                    {
                        if (StaFormEdicao)
                            MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            if (MvCotacao.IdCotacao > 0 && !StaFormEdicao)
                            {
                                if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    ItemCotacao.IdCotacao = MvCotacao.IdCotacao;
                                    ItemCotacao.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                                    ItemCotacao.Excluir();
                                    ItemCotacao.IdItem = 0;
                                    PopularGridItens();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (GridParticipantes.CurrentRow != null)
                    {
                        if (StaFormEdicao)
                            MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            if (MvCotacao.IdCotacao > 0 && ItemCotacao.IdItem > 0)
                            {
                                if (MessageBox.Show("Confirma a Exclusão do Participantes", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    PessoaCotacao.IdCotacao = MvCotacao.IdCotacao;
                                    PessoaCotacao.IdItem = ItemCotacao.IdItem;
                                    PessoaCotacao.IdPessoa = int.Parse(GridParticipantes.CurrentRow.Cells[0].Value.ToString());
                                    PessoaCotacao.Excluir();
                                    PessoaCotacao.IdItem = 0;
                                    PopularGridPessoa();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MvCotacao.Status == 1)
                {
                    MessageBox.Show("Cotação já Encerrada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                }
                else
                {
                    if (MvCotacao.IdCotacao > 0)
                    {
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.VerGrpLstVenda  = true;
                        BuscaPrd.ShowDialog();
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
                                    //Source_Itens.CancelEdit();
                                    //BuscaPrd.Dispose();
                                    //return;
                                    continue;
                                }
                                Verificar ExistePrd = new Verificar();
                                ExistePrd.Controle = Controle;
                                if (!ExistePrd.VerificarExite_PrdCotacao(MvCotacao.IdCotacao, BuscaPrd.IdProduto))
                                {
                                    ItemCotacao.IdCotacao = MvCotacao.IdCotacao;
                                    ItemCotacao.IdProduto = BuscaPrd.IdProduto;
                                    ItemCotacao.Qtde = 1;
                                    ItemCotacao.VlrTotal = 0;
                                    ItemCotacao.IdItem = 0;
                                    ItemCotacao.GravarDados();
                                    //PopularGridItens();
                                }
                                else
                                {
                                    MessageBox.Show("Produto: " + BuscaPrd.CadProd.Descricao.Trim() + " já cadastrado na cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    continue;
                                }
                            }
                            else
                                continue;
                        }
                        PopularGridItens();                        
                        BuscaPrd.Dispose();
                    }
                }
            }
        }
        private void IncluirPessoa()
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MvCotacao.Status == 1)
                {
                    MessageBox.Show("Cotação já Encerrada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Participantes.CancelEdit();
                }
                else
                {
                    if (MvCotacao.IdCotacao > 0 && ItemCotacao.IdItem > 0)
                    {
                        FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                        BuscaPessoa.FrmPrincipal = this.FrmPrincipal;                        
                        BuscaPessoa.ShowDialog();

                        if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                        {
                            Verificar Existe = new Verificar();
                            Existe.Controle = Controle;
                            if (!Existe.VerificarExite_PessoaCotacao(MvCotacao.IdCotacao, BuscaPessoa.CadPessoa.IdPessoa, ItemCotacao.IdItem))
                            {
                                PessoaCotacao.IdCotacao = MvCotacao.IdCotacao;
                                PessoaCotacao.IdItem = ItemCotacao.IdItem;
                                PessoaCotacao.IdPessoa = BuscaPessoa.CadPessoa.IdPessoa;
                                PessoaCotacao.VlrUnitario = 0;
                                PessoaCotacao.Incluir = true;
                                PessoaCotacao.GravarDados();
                                PopularGridPessoa();
                            }
                            else
                            {
                                MessageBox.Show("Pessoa já cadastrado na cotação para este item", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Source_Participantes.CancelEdit();
                            }
                        }
                        else
                            Source_Participantes.CancelEdit();
                        BuscaPessoa.Dispose();
                    }
                }
            }
        }
        private void Hab_Botoes()
        {
            if (StaFormEdicao || MvCotacao.IdCotacao==0)
            {
                BtnVerificar.Enabled = false;
                BtnConcluir.Enabled  = false;
                BtnCancelar.Enabled  = false;
                BtnImprimir.Enabled  = false;
            }
            else
            {
                BtnVerificar.Enabled = MvCotacao.Status == 0;
                BtnConcluir.Enabled  = (MvCotacao.Status == 0 && MvCotacao.Concluido == 1);
                BtnCancelar.Enabled  = MvCotacao.Status == 1;
                BtnImprimir.Enabled = (MvCotacao.Concluido == 1);
            }
        }
        private void BtnVerificar_Click(object sender, EventArgs e)
        {
            if (MvCotacao.Status == 1)
                MessageBox.Show("Cotação já Encerrada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (StaFormEdicao)
                    MessageBox.Show("Favor gravar a cotação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    decimal Total = 0;
                    BtnVerificar.Enabled = false;                     
                    FrmPrincipal.BSta_BarProcesso.Value = 0;
                    //
                    try
                    {
                        SqlDataReader ConsItens;
                        if (MvCotacao.Tipo == 0) // Verifica Cotação por Preço
                        {
                            ConsItens = Controle.ConsultaSQL("SELECT * FROM COTACAOITENS WHERE ID_COTACAO=" + MvCotacao.IdCotacao.ToString());
                            FrmPrincipal.BSta_BarProcesso.Maximum = Source_Itens.Count;
                            if (ConsItens.HasRows)
                            {
                                CotacaoItens Atu_Item = new CotacaoItens();
                                while (ConsItens.Read())
                                {
                                    SqlDataReader ConsParticipante;
                                    ConsParticipante = Controle.ConsultaSQL("SELECT TOP 1 * FROM COTACAOPESSOAS WHERE ID_ITEM=" + int.Parse(ConsItens["Id_Item"].ToString()) + " ORDER BY VLRUNITARIO");
                                    if (ConsParticipante.HasRows)
                                    {
                                        ConsParticipante.Read();
                                        Atu_Item.Controle = Controle;
                                        Atu_Item.IdCotacao = int.Parse(ConsParticipante["Id_Cotacao"].ToString());
                                        Atu_Item.IdItem = int.Parse(ConsParticipante["Id_Item"].ToString());
                                        Atu_Item.IdProduto = int.Parse(ConsItens["Id_Produto"].ToString());
                                        Atu_Item.Qtde = decimal.Parse(ConsItens["Qtde"].ToString());
                                        Atu_Item.IdPessoa = int.Parse(ConsParticipante["Id_Pessoa"].ToString());
                                        Atu_Item.VlrUnitario = decimal.Parse(ConsParticipante["VlrUnitario"].ToString());
                                        Atu_Item.GravarDados();
                                        Total = Total + (Atu_Item.Qtde * Atu_Item.VlrUnitario);
                                    }
                                    FrmPrincipal.BSta_BarProcesso.Value = FrmPrincipal.BSta_BarProcesso.Value + 1;
                                }
                            }
                        }
                        else // Verifica Cotação por Total Geral
                        {
                            ConsItens = Controle.ConsultaSQL("SELECT TOP 1 T1.ID_PESSOA,SUM(T2.QTDE*T1.VLRUNITARIO) AS TOTAL " +
                                                             " FROM COTACAOPESSOAS T1  LEFT JOIN COTACAOITENS T2 ON (T2.ID_ITEM=T1.ID_ITEM) " +
                                                             " WHERE T1.ID_COTACAO=" + MvCotacao.IdCotacao.ToString() + " GROUP BY T1.ID_PESSOA");                            
                            if (ConsItens.HasRows)
                            {
                                ConsItens.Read();
                                CotacaoItens Atu_Item = new CotacaoItens();
                                SqlDataReader ConsParticipante;
                                ConsParticipante = Controle.ConsultaSQL("SELECT T1.*,T2.ID_PRODUTO,T2.QTDE FROM CotacaoPessoas T1  LEFT JOIN COTACAOITENS T2 ON (T2.ID_ITEM=T1.ID_ITEM) " +
                                                                        "  WHERE T1.ID_COTACAO=" + MvCotacao.IdCotacao.ToString() + "  AND T1.ID_PESSOA=" + ConsItens["ID_PESSOA"].ToString());
                                FrmPrincipal.BSta_BarProcesso.Maximum = Source_Itens.Count;
                                if (ConsParticipante.HasRows)
                                {
                                    while (ConsParticipante.Read())
                                    {                                        
                                        Atu_Item.Controle = Controle;
                                        Atu_Item.IdCotacao = int.Parse(ConsParticipante["Id_Cotacao"].ToString());
                                        Atu_Item.IdItem = int.Parse(ConsParticipante["Id_Item"].ToString());
                                        Atu_Item.IdProduto = int.Parse(ConsParticipante["Id_Produto"].ToString());
                                        Atu_Item.Qtde = decimal.Parse(ConsParticipante["Qtde"].ToString());
                                        Atu_Item.IdPessoa = int.Parse(ConsParticipante["Id_Pessoa"].ToString());
                                        Atu_Item.VlrUnitario = decimal.Parse(ConsParticipante["VlrUnitario"].ToString());
                                        Atu_Item.GravarDados();
                                        Total = Total + (Atu_Item.Qtde * Atu_Item.VlrUnitario);
                                        FrmPrincipal.BSta_BarProcesso.Value = FrmPrincipal.BSta_BarProcesso.Value + 1;
                                    }
                                }
                            }
                        }
                        MvCotacao.Concluido = 1;
                        MvCotacao.VlrTotal = Total;
                        TxtVlrTotal.Value = Total;
                        MvCotacao.GravarDados();
                        BtnVerificar.Enabled = true;
                        PopularGridItens();                        
                        MessageBox.Show("Processo concluido", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmPrincipal.BSta_BarProcesso.Value = 0;                        
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Ocorreu o seguinte erro "+erro+",favor tentar novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MvCotacao.Concluido = 0;
                        MvCotacao.GravarDados();
                        BtnVerificar.Enabled = true;
                        FrmPrincipal.BSta_BarProcesso.Value = 0;
                        PopularGridItens();                        
                    }   
                }
            }                
        }
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (MvCotacao.Concluido == 0)
                    MessageBox.Show("Para concluir a cotação você deve fazer a verificação do vencedor", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (MessageBox.Show("Confirma a conclusão da cotação ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MvCotacao.Concluir();
                        MessageBox.Show("Cotação concluida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Paginas.SelectTab(0);
                    }
                }
            }                
        }
        private void BtnCancelar_Click_1(object sender, EventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (FrmPrincipal.Perfil_Usuario.SolicAutCanc == 1)
                {
                    FrmAutorizacao Autorizacao = new FrmAutorizacao();
                    Autorizacao.FrmPrincipal = FrmPrincipal;
                    Autorizacao.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (Autorizacao.AcessoOk)
                    {
                        if (Autorizacao.Usuario.SolicAutCanc == 1)
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            if (MessageBox.Show("Confirma o Concelamento da da cotação ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                MvCotacao.Cancelar();
                                MessageBox.Show("Cotação cancelada", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Paginas.SelectTab(0);
                            }
                        }
                    }
                }
            }
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelCotacao RelCotacao = new Relatorios.RelCotacao();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(MvCotacao.SqlRelatorio(MvCotacao.IdCotacao));            
            //RelCotacao.SetDataSource(TabRel);
            RelCotacao.SetDataSource(TabRel.Tables[0]);                        
            FrmRel.cryRepRelatorio.ReportSource = RelCotacao;
            FrmRel.ShowDialog();            
        }
    }
}
