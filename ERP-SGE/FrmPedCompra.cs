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
    public partial class FrmPedCompra : Form
    {
        Funcoes Controle             = new Funcoes();
        PedCompra MvPedCompra        = new PedCompra();
        PedCompraItens ItemPedCompra = new PedCompraItens();
        Auditoria RegAuditoria       = new Auditoria();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        // Tabelas
        private DataSet TabItens;        
        private BindingSource Source_Itens;        

        public FrmPedCompra()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles
            Controle.Conexao        = FrmPrincipal.Conexao;
            MvPedCompra.Controle    = Controle;
            ItemPedCompra.Controle  = Controle;
            RegAuditoria.Controle   = Controle;
            MvPedCompra.IdDocumento = 0;
            Dt1.Value               = DateTime.Now;
            Dt2.Value               = DateTime.Now;
            Rb_EmEspera.Checked     = true;
            Chk_Periodo.Checked     = false;
            PopularGrid();
            // Instanciando as Tabelas
            TabItens     = new DataSet();
            Source_Itens = new BindingSource();            
            //
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "");
            LstTransp = FrmPrincipal.PopularCombo("SELECT Id_Transportadora,Fantasia from Transportadoras ORDER BY FANTASIA", LstTransp);
        }
        private void PopularGrid()
        {
            string Filtro = "";
            if (Rb_NaoConf.Checked)
                Filtro = " WHERE T1.Status = 0";
            else if (Rb_EmEspera.Checked)
                Filtro = " WHERE T1.Status = 1";
            else
                Filtro = Filtro + " WHERE T1.Status = 2";

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " and T1.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqFornecedor.Text.Trim() != "")
                Filtro = Filtro + " and T2.RazaoSocial like '%" + TxtPesqFornecedor.Text.Trim() + "%'";

            if (TxtPesNumPedido.Text.Trim() != "")
                Filtro = Filtro + " and T1.NumPedido like '%" + TxtPesNumPedido.Text.Trim() + "%'";

            if (TxtPesNumNfe.Text.Trim() != "")
                Filtro = Filtro + " and T1.NumNFe like '%" + TxtPesNumNfe.Text.Trim() + "%'";

            try
            {
                DataSet Tabela = new DataSet();
                Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Documento,CASE T1.STATUS WHEN 0 THEN 'Não Confirmado' WHEN 1 THEN 'Em Espera' WHEN 2 THEN 'Recebido' END AS Status,T1.Data,T1.NumPedido,T1.NumNFe,T1.PrevEntrega,T2.RazaoSocial as Fornecedor,T3.FANTASIA AS FILIAL,T1.VlrTotal,T4.Usuario " +
                                                               " FROM PedCompra T1 LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) LEFT JOIN EMPRESA_FILIAL T3 ON (T3.ID_FILIAL=T1.ID_FILIAL) LEFT JOIN USUARIOS T4 ON (T4.ID_USUARIO=T1.ID_USUARIO) " + Filtro + " ORDER BY T2.RAZAOSOCIAL,T1.ID_DOCUMENTO DESC"));
                BindingSource Source = new BindingSource();
                Source.DataSource = Tabela;
                Source.DataMember = Tabela.Tables[0].TableName;
                GridDados.DataSource = Source;
                int item = Source.Find("Id_Documento", MvPedCompra.IdDocumento);
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
            MvPedCompra.LerDados(Isn);
            TxtCodigo.Text          = MvPedCompra.IdDocumento.ToString();
            TxtData.Value           = MvPedCompra.Data;            
            TxtNumPedido.Text       = MvPedCompra.NumPedido;
            TxtVendedor.Text        = MvPedCompra.Vendedor;
            TxtFone.Text            = MvPedCompra.Fone;
            TxtPrevEntrega.Value    = MvPedCompra.PrevEntrega;
            TxtVlrTotal.Value       = MvPedCompra.VlrTotal;
            TxtObservacao.Text      = MvPedCompra.Observacao;
            TxtFormaPgto.Text       = MvPedCompra.FormaPgto;
            TxtNumNFe.Text          = MvPedCompra.NumNFe;
            LstFilial.SelectedValue = MvPedCompra.IdFilial.ToString();
            LstTransp.SelectedValue = MvPedCompra.IdTransp.ToString();
            SetaPessoa(MvPedCompra.IdPessoa);
            if (MvPedCompra.Status == 2) TxtDataRec.Text = MvPedCompra.DataRecebimento.Date.ToShortDateString();
            Hab_Botoes();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularGridItens();
            FrmPrincipal.ControleBotoes(true);
            BuscaPessoa();
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
                if (MvPedCompra.Status == 1)
                    MessageBox.Show("Pedido de Compra já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (MvPedCompra.Status == 2)
                    MessageBox.Show("Pedido de Compra já Recebido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    PopularGridItens();
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    //BuscaPessoa();
                    Hab_Botoes();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (int.Parse(TxtCodPessoa.Text) > 0)
            {

                MvPedCompra.IdDocumento = int.Parse(TxtCodigo.Text);
                MvPedCompra.Data        = TxtData.Value;
                MvPedCompra.NumPedido   = TxtNumPedido.Text;
                MvPedCompra.Vendedor    = TxtVendedor.Text;
                MvPedCompra.Fone        = TxtFone.Text;
                MvPedCompra.PrevEntrega = TxtPrevEntrega.Value;
                MvPedCompra.Observacao  = TxtObservacao.Text;
                MvPedCompra.VlrTotal    = TxtVlrTotal.Value;
                MvPedCompra.FormaPgto   = TxtFormaPgto.Text;
                MvPedCompra.NumNFe      = TxtNumNFe.Text;
                MvPedCompra.IdFilial    = int.Parse(LstFilial.SelectedValue.ToString());
                MvPedCompra.IdTransp    = int.Parse(LstTransp.SelectedValue.ToString());
                MvPedCompra.IdUsuario   = FrmPrincipal.Perfil_Usuario.IdUsuario;
                StaFormEdicao = false;

                MvPedCompra.GravarDados();

                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, MvPedCompra.IdDocumento, MvPedCompra.NumPedido, 1, "Novo Ped.: " + TxtFornecedor.Text);
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, MvPedCompra.IdDocumento, MvPedCompra.NumPedido, 2, "Alteração: " + TxtFornecedor.Text);

                PopularGrid();
                PopularCampos(MvPedCompra.IdDocumento);
                PopularGridItens();
                FrmPrincipal.ControleBotoes(false);

            }
            else
            {
                MessageBox.Show("Favor informar o Fornecedor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                MvPedCompra.IdDocumento = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                MvPedCompra.LerDados(MvPedCompra.IdDocumento);
                if (MvPedCompra.Status == 1)
                    MessageBox.Show("Pedido de Compra já Concluído, para poder excluir você deve cancelar o Pedido de Compra", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (MvPedCompra.Status == 2)
                    MessageBox.Show("Pedido de Compra já Recebido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {                    
                    if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MvPedCompra.IdDocumento = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        FrmPrincipal.RegistrarAuditoria(this.Text, MvPedCompra.IdDocumento, MvPedCompra.NumPedido, 3, "Excluir Ped.: " + TxtFornecedor.Text);
                        MvPedCompra.Excluir();
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
            TxtCodigo.Text       = "0";
            TxtData.Value        = DateTime.Now;
            TxtNumPedido.Text    = "";
            TxtVendedor.Text     = "";
            TxtFone.Text         = "";
            TxtPrevEntrega.Value = DateTime.Now;
            TxtObservacao.Text   = "";
            TxtVlrTotal.Value    = 0;
            TxtDataRec.Text      = "";
            TxtCodPessoa.Text    = "0";
            TxtFornecedor.Text   = "";
            TxtFormaPgto.Text    = "";
            TxtNumNFe.Text       = "";
            MvPedCompra.LerDados(0);
            LstFilial.SelectedValue = FrmPrincipal.Perfil_Usuario.IdFilial.ToString();
            LstFilial.SelectedValue = 0;
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
            FrmPrincipal.ClickBtnFechar   += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
        // Controle dos Itens
        private void PopularGridItens()
        {            
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T2.REFERENCIA,T1.DESCRICAO,T1.QTDE,T1.QTDERECEBIDA,T1.VLRUNITARIO,T1.VLRTOTAL,T1.PICMS,T1.PIPI FROM PedCompraITENS T1" +
                                               "  LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.Id_Documento=" + MvPedCompra.IdDocumento.ToString());                                               
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("ID_Item", ItemPedCompra.IdItem);
            Source_Itens.Position = item;
            Hab_Botoes();

            //Atualizando o Total do Pedido de Compra
            TotalMovimento();            
        }
        private void TotalMovimento()
        {
            decimal VlrSubTotal = 0;
            decimal VlrIpi      = 0;
            decimal VlrIcms     = 0;
            for (int I = 0; I <= GridItens.RowCount - 1; I++)
            {
                VlrSubTotal = VlrSubTotal + decimal.Parse(GridItens.Rows[I].Cells[6].Value.ToString());
                if (decimal.Parse(GridItens.Rows[I].Cells[7].Value.ToString()) > 0)
                    VlrIcms = VlrIcms + Math.Round((decimal.Parse(GridItens.Rows[I].Cells[6].Value.ToString()) * decimal.Parse(GridItens.Rows[I].Cells[7].Value.ToString())) / 100, 2);
                if (decimal.Parse(GridItens.Rows[I].Cells[8].Value.ToString()) > 0)
                    VlrIpi = VlrIpi + Math.Round((decimal.Parse(GridItens.Rows[I].Cells[6].Value.ToString()) * decimal.Parse(GridItens.Rows[I].Cells[8].Value.ToString())) / 100, 2);
            }
            MvPedCompra.VlrSubTotal = VlrSubTotal;
            MvPedCompra.VlrIcms     = VlrIcms;
            MvPedCompra.VlrIpi      = VlrIpi;
            MvPedCompra.VlrTotal    = VlrSubTotal+VlrIpi;
            TxtVlrSubTotal.Value    = VlrSubTotal;
            TxtVlrIPI.Value         = VlrIpi;
            TxtVlrIcms.Value        = VlrIcms;
            TxtVlrTotal.Value       = VlrSubTotal + VlrIpi;
            if (MvPedCompra.IdDocumento > 0)
                MvPedCompra.GravarDados();
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
                MessageBox.Show("Favor gravar o Pedido de Compra", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (MvPedCompra.Status == 2)
                {
                    MessageBox.Show("Pedido de Compra já Recebido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                }
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MvPedCompra.IdDocumento > 0 && !StaFormEdicao)
            {
                if (MvPedCompra.Status == 1)
                {
                    if (decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString())!=decimal.Parse(GridItens.CurrentRow.Cells[4].Value.ToString()))
                        MessageBox.Show("Quantidade recebida esta diferente da quantidade do pedido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                ItemPedCompra.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                ItemPedCompra.IdItem       = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                ItemPedCompra.Descricao    = GridItens.CurrentRow.Cells[2].Value.ToString().Trim();
                ItemPedCompra.Qtde         = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                ItemPedCompra.QtdeRecebida = decimal.Parse(GridItens.CurrentRow.Cells[4].Value.ToString());
                ItemPedCompra.VlrUnitario  = decimal.Parse(GridItens.CurrentRow.Cells[5].Value.ToString());                
                ItemPedCompra.PIcms        = decimal.Parse(GridItens.CurrentRow.Cells[7].Value.ToString());
                ItemPedCompra.PIpi         = decimal.Parse(GridItens.CurrentRow.Cells[8].Value.ToString());
                ItemPedCompra.GravarDados();
                PopularGridItens();
                GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex]; 
            }
        }        
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Pedido de Compra", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MvPedCompra.IdDocumento > 0)
                    IncluirItem();
                else
                    Source_Itens.CancelEdit();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (MvPedCompra.Status == 1)
                MessageBox.Show("Pedido de Compra já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (MvPedCompra.Status == 2)
                MessageBox.Show("Pedido de Compra já Recebido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (GridItens.CurrentRow != null)
                {
                    if (StaFormEdicao)
                        MessageBox.Show("Favor gravar o Pedido de Compra", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        if (MvPedCompra.IdDocumento > 0 && !StaFormEdicao)
                        {
                            if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ItemPedCompra.IdDocumento = MvPedCompra.IdDocumento;
                                ItemPedCompra.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                                ItemPedCompra.Excluir();
                                ItemPedCompra.IdItem = 0;
                                GridItens.Rows.Remove(GridItens.CurrentRow);
                                TotalMovimento();
                                //PopularGridItens();
                            }
                        }
                    }
                }                
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Pedido de Compra", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MvPedCompra.Status == 1)
                {
                    MessageBox.Show("Pedido de Compra já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                }
                else if (MvPedCompra.Status == 2)
                {
                    MessageBox.Show("Pedido de Compra já Recebido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();                 
                }
                else
                {
                    if (MvPedCompra.IdDocumento > 0)
                    {
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.PedCompra       = true;
                        BuscaPrd.ShowDialog();
                        ItemPedCompra.LerDados(0);

                        if (BuscaPrd.ListaCodPrd.Count > 1)
                        {
                            for (int I = 0; I <= BuscaPrd.ListaCodPrd.Count - 1; I++)
                            {
                                ArrayList PrdQtde = new ArrayList(BuscaPrd.ListaCodPrd[I].ToString().Split(char.Parse("|")));
                                BuscaPrd.CadProd.LerDados(int.Parse(PrdQtde[0].ToString()));
                                BuscaPrd.IdProduto = BuscaPrd.CadProd.IdProduto;
                                if (BuscaPrd.IdProduto > 0)
                                {
                                    Verificar ExistePrd = new Verificar();
                                    ExistePrd.Controle = Controle;
                                    decimal PrcNF = 0;
                                    if (!ExistePrd.VerificarExite_PrdPedCompra(MvPedCompra.IdDocumento, BuscaPrd.IdProduto))
                                    {
                                        SqlDataReader Tabela;
                                        Tabela = Controle.ConsultaSQL("SELECT TOP 1 T1.VLRUNITARIO FROM MVESTOQUEITENS T1 LEFT JOIN MVESTOQUE T2 ON (T2.ID_MOV=T1.ID_MOV)" +
                                                                      " WHERE T1.ID_PRODUTO=" + BuscaPrd.CadProd.IdProduto.ToString() + "  AND T2.STATUS=1 AND T2.TPMOV='COMPR' ORDER BY DTENTSAI DESC");
                                        if (Tabela.HasRows)
                                        {
                                            Tabela.Read();
                                            PrcNF = decimal.Parse(Tabela["VlrUnitario"].ToString());
                                        }
                                        ItemPedCompra.IdItem = 0;
                                        ItemPedCompra.IdDocumento = MvPedCompra.IdDocumento;
                                        ItemPedCompra.IdProduto = BuscaPrd.IdProduto;
                                        ItemPedCompra.Descricao = BuscaPrd.CadProd.Descricao;
                                        if (decimal.Parse(PrdQtde[1].ToString()) > 0)
                                            ItemPedCompra.Qtde = decimal.Parse(PrdQtde[1].ToString());
                                        else
                                            ItemPedCompra.Qtde = 1;
                                        ItemPedCompra.QtdeRecebida = 0;
                                        ItemPedCompra.VlrUnitario  = PrcNF;
                                        ItemPedCompra.VlrTotal     = PrcNF;
                                        ItemPedCompra.PIcms        = UltICMS(BuscaPrd.IdProduto);
                                        ItemPedCompra.PIpi         = UltIPI(BuscaPrd.IdProduto);
                                        ItemPedCompra.GravarDados();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Produto: " + BuscaPrd.CadProd.Descricao.Trim() + " já cadastrado no Pedido de Compra", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        continue;
                                    }
                                }
                                else
                                    continue;
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Deseja incluir um item sem cadastro ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ItemPedCompra.IdItem = 0;
                                ItemPedCompra.IdDocumento = MvPedCompra.IdDocumento;
                                ItemPedCompra.GravarDados();
                            }
                        }

                        PopularGridItens();
                        if (GridItens.CurrentRow != null)
                            GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];
                        BuscaPrd.Dispose();
                    }
                }
            }
        }        
        private void Hab_Botoes()
        {
            if (StaFormEdicao || MvPedCompra.IdDocumento == 0)
            {
                BtnConcluir.Enabled = false;
                BtnCancMov.Enabled = false;
                BtnImprimir.Enabled = false;
            }
            else
            {
                if (MvPedCompra.Status == 1)
                   BtnConcluir.Text = "Confirmar Receb.";
                else
                    BtnConcluir.Text = "Concluir Pedido ";
                BtnConcluir.Enabled = MvPedCompra.Status < 2;
                BtnCancMov.Enabled = MvPedCompra.Status > 0;
                BtnImprimir.Enabled = true;
                GridItens.Columns[3].ReadOnly = true;
                GridItens.Columns[4].ReadOnly = true;
                GridItens.Columns[5].ReadOnly = true;
                GridItens.Columns[7].ReadOnly = true;
                GridItens.Columns[8].ReadOnly = true;              
                if (MvPedCompra.Status == 0)
                {
                    GridItens.Columns[3].ReadOnly = false;
                    GridItens.Columns[4].ReadOnly = true;
                    GridItens.Columns[5].ReadOnly = false;
                    GridItens.Columns[7].ReadOnly = false;
                    GridItens.Columns[8].ReadOnly = false;              
                }
                else if (MvPedCompra.Status == 1)
                {
                    GridItens.Columns[3].ReadOnly = true;
                    GridItens.Columns[4].ReadOnly = false;
                    GridItens.Columns[5].ReadOnly = true;                    
                }
            }
        }        
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (MvPedCompra.FormaPgto=="")
                {
                    MessageBox.Show("Favor informar a Forma de Pagamento ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Confirma a conclusão do Pedido de Compra ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MvPedCompra.Concluir();
                    MessageBox.Show("Pedido de Compra concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopularCampos(MvPedCompra.IdDocumento);
                    //Paginas.SelectTab(0);
                }                
            }
        }
        private void BtnCancMov_Click(object sender, EventArgs e)
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
                            if (MessageBox.Show("Confirma o Concelamento do Pedido de Compra ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                MvPedCompra.Cancelar();
                                MessageBox.Show("Pedido de Compra cancelado", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                PopularCampos(MvPedCompra.IdDocumento);
                                //Paginas.SelectTab(0);
                            }
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Confirma o Concelamento do Pedido de Compra ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MvPedCompra.Cancelar();
                        MessageBox.Show("Pedido de Compra cancelado", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Paginas.SelectTab(0);
                    }
                }
            }
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelPedCompra RelPedCompra = new Relatorios.RelPedCompra();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(MvPedCompra.SqlRelatorio(MvPedCompra.IdDocumento));            
            RelPedCompra.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelPedCompra;
            //((CrystalDecisions.CrystalReports.Engine.TextObject)(RelPedCompra.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelPedCompra.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;            
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;
        }
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            BuscaPessoa();
        }

        private void BuscaPessoa()
        {
            if (MvPedCompra.Status == 1)
                MessageBox.Show("Pedido de Compra já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (MvPedCompra.Status == 2)
                MessageBox.Show("Pedido de Compra já Recebido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (StaFormEdicao)
                {
                    FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                    BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
                    BuscaPessoa.ShowDialog();
                    if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                    {
                        SetaPessoa(BuscaPessoa.CadPessoa.IdPessoa);
                        if (MvPedCompra.FormaPgto.Trim() == "")
                        {
                            MvPedCompra.FormaPgto = BuscaPessoa.CadPessoa.PrazoPgto;
                            TxtFormaPgto.Text = MvPedCompra.FormaPgto;
                        }
                    }
                }
            }
        }
        private void SetaPessoa(int IdPessoa)
        {
            Pessoas CadPessoa = new Pessoas();
            CadPessoa.Controle = Controle;
            CadPessoa.LerDados(IdPessoa);
            MvPedCompra.IdPessoa = CadPessoa.IdPessoa;
            TxtCodPessoa.Text    = CadPessoa.IdPessoa.ToString();
            TxtFornecedor.Text   = CadPessoa.RazaoSocial.Trim();            
        }

        private void BtnFicha_Click(object sender, EventArgs e)
        {
            if (MvPedCompra.IdDocumento > 0 && MvPedCompra.IdPessoa > 0)
            {
                FrmFichaFinanc Ficha = new FrmFichaFinanc();
                Ficha.FrmPrincipal   = FrmPrincipal;
                Ficha.IdPessoa       = MvPedCompra.IdPessoa;
                Ficha.LblPessoa.Text = TxtFornecedor.Text;
                Ficha.TpCliente      = false;
                Ficha.ShowDialog();
            }
        }

        private decimal UltIPI(int IdPrd)
        {
            SqlDataReader Tab = Controle.ConsultaSQL("SELECT TOP 1 T1.PIPI FROM PedCompraItens T1  LEFT JOIN PedCompra T2 ON(T2.Id_Documento = T1.Id_Documento)" +
                                                   " WHERE T2.Status = 2 and T1.Id_Produto = " + IdPrd.ToString() + " ORDER BY T2.Data DESC");
            if (Tab.HasRows)
            {
                Tab.Read();
                return decimal.Parse(Tab["PIPI"].ToString());
            }
            else
                return 0;
            
        }
        private decimal UltICMS(int IdPrd)
        {
            SqlDataReader Tab = Controle.ConsultaSQL("SELECT TOP 1 T1.PICMS FROM PedCompraItens T1  LEFT JOIN PedCompra T2 ON(T2.Id_Documento = T1.Id_Documento)" +
                                                   " WHERE T2.Status = 2 and T1.Id_Produto = " + IdPrd.ToString() + " ORDER BY T2.Data DESC");
            if (Tab.HasRows)
            {
                Tab.Read();
                return decimal.Parse(Tab["PICMS"].ToString());
            }
            else
                return 0;

        }

    }
}
