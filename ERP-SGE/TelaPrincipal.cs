using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using Controles;
using System.Security;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;


namespace ERP_SGE
{
    public partial class TelaPrincipal : Form
    {
        // Eventtos Publicos
        public event EventHandler ClickBtnNovo;
        public event EventHandler ClickBtnEditar;
        public event EventHandler ClickBtnGravar;
        public event EventHandler ClickBtnExcluir;
        public event EventHandler ClickBtnCancelar;        
        public event EventHandler ClickBtnFechar;
        public event EventHandler LimpaClick;

        // Telas 
        private FrmCadEstados CadEstado;
        private FrmCadEmpresaFilial CadFilial;
        private FrmCadUsuario CadUsuario;
        private FrmCadAtividades CadAtividade;
        private FrmCadCfop CadCFOP;
        private FrmCadRotas CadRotas;
        private FrmCadDepartamento CadDepartamentos;
        private FrmCadTransportadora CadTransportadora;
        private FrmCadVendedor CadVendedor;
        private FrmCadEntregador CadEntregador;        
        private FrmCadTipoDocumento CadTipoDocum;
        private FrmCadBancos CadBanco;
        private FrmCadContaCaixa CadCaixa;
        private FrmCadFormaPgto CadFormaPgto;
        private FrmCadPessoas CadPessoa;
        private FrmCadGrupoProduto CadGrupoPrd;
        private FrmCadProduto CadProduto;
        private FrmCotacao Cotacao;
        private FrmPedCompra PedCompra;
        private FrmMvEstoque MovEstoque;
        private FrmFinanceiro FrmFinanc;
        private FrmCxaAbrir FrmAbrirCx;
        private FrmMvVendas MvVendas;
        private FrmFaturamento Faturamento;
        private FrmNotaFiscal NotaFiscal;
        //private FrmMapaEntrega MapaEntrega;
        private FrmConfEntrega ConfEntrega;
        private FrmMovCheques MovCheque;
        private FrmCadGenero CadGenero;
        private FrmInstalacao FrmInst;
        private FrmAgenteCobrador FrmAgCobrador;
        private FrmAIDF FrmCadAIDF;
        private FrmLancLivroCxa FrmLivroCxa;
        private FrmCadFuncionarios FrmCadFunc;
        private FrmAgendaVisita FrmAgVisita;
        private FrmPromocoes FrmPromocao;
        private FrmEventosProvDesc FrmEventosPD;
        private FrmGerarFolha FrmGerarFP;
        private FrmPrevCustos FrmPrvCusto;
        private FrmVeiculos FrmVeiculos;
        private FrmMapaEntrega2 MapaEntrega;
        private FrmBalanco FrmBalanc;
        private FrmPremiacao FrmPremio;
        private FrmGradeComodato FrmGradeComod;
        private FrmProducao FrmCadProducao;
        private FrmPromocaoPrd FrmPromPrd;
        private FrmMapaProducao FrmMapaProd;
        //Variaves Publicas
        public string StringConexao; // = "Data Source=Servidor;Initial Catalog=BDSIP;Integrated Security=True;MultipleActiveResultSets=True;";                
        public SqlConnection Conexao;
        public int TipoECF;        
        public Controle_Dados.Usuarios Perfil_Usuario;
        public Controle_Dados.Parametros Parametros_Filial;
        public string Rel_RodaPe = "";
        public string PortaImpResumida = "";
        public string TipoImpResumida = "";
        public bool VersaoDistribuidor = false;
        public int IdFilialConexao = 0;
        public string Release = "2.22.000";
        public string PortaSocket = "";
        public string IPSocket = "";
        public string PathAtlz = "";
        public string URLMatriz = "";

        //Variaves Privadas
        Controle_Dados.Funcoes Controle       = new Controle_Dados.Funcoes();
        Controle_Dados.Auditoria RegAuditoria = new Controle_Dados.Auditoria();
        public ImpressoraFiscal PDV_ImpressoraFiscal = new ImpressoraFiscal();        

        // Funções Publicas
        public SqlConnection AbrirConexao()
        {            
            if (Conexao == null)
            {
                //if (ServidorRemoto)
                //    Conexao = new SqlConnection(StringConexaoRemoto);
                //else
                Conexao = new SqlConnection(StringConexao);                
                Conexao.Open();
                Controle.Conexao      = Conexao;
                RegAuditoria.Controle = Controle;
                BSta_Banco.Text       = Conexao.Database;
                BSta_Servidor.Text    = Conexao.DataSource;                
                BSta_Estacao.Text     = Conexao.WorkstationId;
                BSta_StaConexao.Text  = "Conectado";
                BCima_LblCaixa.Text   = "";
            }
            return Conexao;
        }        
        public ComboBox PopularCombo(string SQL, ComboBox Combo)
        {
            DataSet Tabela = new DataSet();            
            Tabela = Controle.ConsultaTabela(SQL);            
            Tabela.Tables[0].Rows.Add(0, "Nenhum");
            Combo.DataSource    = null;
            Combo.DisplayMember = null;
            Combo.ValueMember   = null;
            Combo.Items.Clear();
            Combo.DataSource = Tabela.Tables[0];
            Combo.DisplayMember = Tabela.Tables[0].Columns[1].ColumnName;
            Combo.ValueMember = Tabela.Tables[0].Columns[0].ColumnName;
            Combo.SelectedValue = 0;
            return Combo;
        }
        public ComboBox PopularCombo(string SQL, ComboBox Combo,string ItemAdd)
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(SQL);
            Combo.DataSource = null;
            Combo.DisplayMember = null;
            Combo.ValueMember = null;
            Combo.Items.Clear();
            if (ItemAdd != "")
                Tabela.Tables[0].Rows.Add(0, ItemAdd);            
            Combo.DataSource = Tabela.Tables[0];
            Combo.DisplayMember = Tabela.Tables[0].Columns[1].ColumnName;
            Combo.ValueMember = Tabela.Tables[0].Columns[0].ColumnName;
            Combo.SelectedValue = 0;            
            return Combo;
        }
        public CheckedListBox PopularCheckList(string SQL, CheckedListBox CkList,string CmpSort, string ItemAdd)
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(SQL);
            if (ItemAdd != "")
                Tabela.Tables[0].Rows.Add(0, ItemAdd);

            if (CmpSort != "")
            {
                DataView TabSort = Tabela.Tables[0].DefaultView;
                TabSort.Sort = CmpSort;
                Tabela.Tables.Clear();
                Tabela.Tables.Add(TabSort.ToTable());
            }            
            CkList.Items.Clear();
            CkList.DataSource = Tabela.Tables[0];
            CkList.DisplayMember = Tabela.Tables[0].Columns[1].ColumnName;
            CkList.ValueMember   = Tabela.Tables[0].Columns[0].ColumnName;            
            if (CmpSort != "")
                CkList.SetItemChecked(0, true);
            return CkList;
        }
        public ListBox PopularListBox(string SQL, ListBox LstBox, string ItemAdd)
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(SQL);
            if (ItemAdd != "")
                Tabela.Tables[0].Rows.Add(0, ItemAdd);
            LstBox.Items.Clear();
            LstBox.DataSource = Tabela.Tables[0];
            LstBox.DisplayMember = Tabela.Tables[0].Columns[1].ColumnName;
            LstBox.ValueMember = Tabela.Tables[0].Columns[0].ColumnName;
            LstBox.SelectedValue = 0;
            return LstBox;
        }
        public DataGridViewComboBoxColumn PopularComboGrid(string SQL, DataGridViewComboBoxColumn Combo)
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela(SQL);
            Tabela.Tables[0].Rows.Add(0, "Nenhum");
            Combo.Items.Clear();
            Combo.DataSource    = Tabela.Tables[0];
            Combo.DisplayMember = Tabela.Tables[0].Columns[1].ColumnName;
            Combo.ValueMember   = Tabela.Tables[0].Columns[0].ColumnName;            
            return Combo;
        }
        public void LimpaClickBotoes()
        {
            BtnNovo.Enabled         = false;
            BtnEditar.Enabled       = false;
            BtnGravar.Enabled       = false;
            BtnExcluir.Enabled      = false;
            BtnCancelar.Enabled     = false;
            BtnFechar.Enabled       = false;
            BtnTrocaUsuario.Enabled = true;
            this.ClickBtnNovo       = LimpaClick;
            this.ClickBtnEditar     = LimpaClick;
            this.ClickBtnGravar     = LimpaClick;
            this.ClickBtnExcluir    = LimpaClick;
            this.ClickBtnCancelar   = LimpaClick;
            this.ClickBtnExcluir    = LimpaClick;
            this.ClickBtnFechar     = LimpaClick;
        }
        public void ControleBotoes(bool Edicao) //Op: True Ativa o botão e False: Desativa o botão
        {
            BtnNovo.Enabled     = !Edicao;
            BtnEditar.Enabled   = !Edicao;
            BtnExcluir.Enabled  = !Edicao;
            BtnGravar.Enabled   = Edicao;
            BtnCancelar.Enabled = Edicao;
            BtnFechar.Enabled   = !Edicao;
        }       
        //------------------------------------------------------
        // Funções Locais
        public bool LerConfig()
        {
            try
            {
                
                ArrayList Parametros = new ArrayList();
                StreamReader LerParam = new StreamReader("ERP-SGE.ini");
                while (!LerParam.EndOfStream)                
                    Parametros.Add(LerParam.ReadLine());

                //StringConexao  = Parametros[0].ToString();                
                TipoECF            = int.Parse(Parametros[0].ToString().Substring(4, 1));                
                PortaImpResumida   = Parametros[1].ToString();
                TipoImpResumida    = Parametros[2].ToString().ToUpper();
                VersaoDistribuidor = Parametros[3].ToString() != "Versao=P";
                IPSocket           = Parametros[4].ToString();
                PortaSocket        = Parametros[5].ToString();
                PathAtlz           = Parametros[7].ToString();
                URLMatriz          = Parametros[8].ToString();
                LerParam.Close();
                
                //Muda Menu no caso do parametro ser uma versão para distribuidor                                
                //Producao.Visible           = !VersaoDistribuidor;
                //OpCadProducao.Visible      = !VersaoDistribuidor;
                Rh.Visible                 = !VersaoDistribuidor;
                OpAgendaVisita.Visible     = !VersaoDistribuidor;
                OpControleInst.Visible     = !VersaoDistribuidor;
                OpCotacaoCompra.Visible    = !VersaoDistribuidor;
                OpDIEF.Visible             = !VersaoDistribuidor;
                OpVdInternet.Visible       = !VersaoDistribuidor;                
                if (TipoECF == 1)
                    PDV_ImpressoraFiscal.InicializarBematech();
                else if (TipoECF == 2)
                    PDV_ImpressoraFiscal.InicializarDaruma();                
                return true;
            }
            catch
            {
                MessageBox.Show("Arquivo de Configuração não encontrado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public Controles.ImpressoraFiscal.ModeloImpressora TipoImpressoraFiscal()
        {
            if (TipoECF == 1)
                return Controles.ImpressoraFiscal.ModeloImpressora.ImpBematech;
            else if (TipoECF == 2)
                return Controles.ImpressoraFiscal.ModeloImpressora.ImpDaruma;
            else if (TipoECF == 3)
                return Controles.ImpressoraFiscal.ModeloImpressora.ImpDarumaVelha;
            else if (TipoECF == 4)
                return Controles.ImpressoraFiscal.ModeloImpressora.MFE;
            else
                return Controles.ImpressoraFiscal.ModeloImpressora.Nenhuma;
        }
        private bool ChamaTela(string Tela)
        {
            for (int I = 0; I <= Application.OpenForms.Count - 1; I++)
            {
                if (Application.OpenForms[I].Name == Tela)
                    return false;
            }
            return true;
        }
        private void DesabilitaMenu()
        {

            for (int Nivel1 = 0; Nivel1 <= MenuOp.Items.Count - 1; Nivel1++)
                MenuOp.Items[Nivel1].Enabled = false;            
            // Castros
            for (int Nivel1 = 0; Nivel1 <= Cadastros.DropDownItems.Count - 1; Nivel1++)
                Cadastros.DropDownItems[Nivel1].Enabled = false;            
            //Pessoas
            for (int Nivel1 = 0; Nivel1 <= Pessoas.DropDownItems.Count - 1; Nivel1++)
                Pessoas.DropDownItems[Nivel1].Enabled = false;            
            //Estoque
            for (int Nivel1 = 0; Nivel1 <= Estoque.DropDownItems.Count - 1; Nivel1++)
                Estoque.DropDownItems[Nivel1].Enabled = false;            
            //Estoque (Produtos e Serviços
            for (int Nivel1 = 0; Nivel1 <= ProdutosServicoes.DropDownItems.Count - 1; Nivel1++)
                ProdutosServicoes.DropDownItems[Nivel1].Enabled = false;            
            //Vendas
            for (int Nivel1 = 0; Nivel1 <= Venda.DropDownItems.Count - 1; Nivel1++)
                Venda.DropDownItems[Nivel1].Enabled = false;
            // Caixa
            for (int Nivel1 = 0; Nivel1 <= OpCaixa.DropDownItems.Count - 1; Nivel1++)
                OpCaixa.DropDownItems[Nivel1].Enabled = false;
            // Vendas
            for (int Nivel1 = 0; Nivel1 <= VendasOp.DropDownItems.Count - 1; Nivel1++)
                VendasOp.DropDownItems[Nivel1].Enabled = false;
            // Expedição
            for (int Nivel1 = 0; Nivel1 <= Expedicao.DropDownItems.Count - 1; Nivel1++)
                Expedicao.DropDownItems[Nivel1].Enabled = false;            
            //Faturamento
            for (int Nivel1 = 0; Nivel1 <= Faturamentos.DropDownItems.Count - 1; Nivel1++)
                Faturamentos.DropDownItems[Nivel1].Enabled = false;
            //Financeiro
            for (int Nivel1 = 0; Nivel1 <= Financeiro.DropDownItems.Count - 1; Nivel1++)
                Financeiro.DropDownItems[Nivel1].Enabled = false;
            //Producao
           /* for (int Nivel1 = 0; Nivel1 <= Producao.DropDownItems.Count - 1; Nivel1++)
                Producao.DropDownItems[Nivel1].Enabled = false;
            //Materia Prima
            for (int Nivel1 = 0; Nivel1 <= OpCadProducao.DropDownItems.Count - 1; Nivel1++)
                OpCadProducao.DropDownItems[Nivel1].Enabled = false;*/
            //RH
            for (int Nivel1 = 0; Nivel1 <= Rh.DropDownItems.Count - 1; Nivel1++)
                Rh.DropDownItems[Nivel1].Enabled = false;
            //Relatorios
            for (int Nivel1 = 0; Nivel1 <= OpRelFaturamento.DropDownItems.Count - 1; Nivel1++)
                OpRelFaturamento.DropDownItems[Nivel1].Enabled = false;
            Sair.Enabled = true;         
        }
        private void AtualizarAcesso()
        {
            DesabilitaMenu();
            if (Perfil_Usuario.IdUsuario > 0)
            {                
                DataSet TabAcesso = new DataSet();
                TabAcesso = Controle.ConsultaTabela("SELECT * FROM ACESSOUSUARIO WHERE ID_USUARIO=" + Perfil_Usuario.IdUsuario.ToString());
                for (int I = 0; I <= TabAcesso.Tables[0].Rows.Count - 1; I++)
                {
                    for (int Nivel1 = 0; Nivel1 <= MenuOp.Items.Count - 1; Nivel1++)
                    { 
                        if (MenuOp.Items[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            MenuOp.Items[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    // Castros
                    for (int Nivel1 = 0; Nivel1 <= Cadastros.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Cadastros.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Cadastros.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    //Pessoas
                    for (int Nivel1 = 0; Nivel1 <= Pessoas.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Pessoas.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Pessoas.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    //Estoque
                    for (int Nivel1 = 0; Nivel1 <= Estoque.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Estoque.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Estoque.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    //Estoque (Produtos e Serviços
                    for (int Nivel1 = 0; Nivel1 <= ProdutosServicoes.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (ProdutosServicoes.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            ProdutosServicoes.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    //Vendas
                    for (int Nivel1 = 0; Nivel1 <= Venda.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Venda.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Venda.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    // Caixa
                    for (int Nivel1 = 0; Nivel1 <= OpCaixa.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (OpCaixa.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            OpCaixa.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    // Vendas
                    for (int Nivel1 = 0; Nivel1 <= VendasOp.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (VendasOp.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            VendasOp.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    // Expedição
                    for (int Nivel1 = 0; Nivel1 <= Expedicao.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Expedicao.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Expedicao.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }                    
                    //Faturamento
                    for (int Nivel1 = 0; Nivel1 <= Faturamentos.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Faturamentos.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Faturamentos.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    //Financeiro
                    for (int Nivel1 = 0; Nivel1 <= Financeiro.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Financeiro.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Financeiro.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                   /* //Producao
                    for (int Nivel1 = 0; Nivel1 <= Producao.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Producao.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Producao.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    //Materia Prima
                    for (int Nivel1 = 0; Nivel1 <= OpCadProducao.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (OpCadProducao.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            OpCadProducao.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }*/
                    //RH
                    for (int Nivel1 = 0; Nivel1 <= Rh.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Rh.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Rh.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    //Relatorios
                    for (int Nivel1 = 0; Nivel1 <= Relatorios.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (Relatorios.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            Relatorios.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }
                    for (int Nivel1 = 0; Nivel1 <= OpRelFaturamento.DropDownItems.Count - 1; Nivel1++)
                    {
                        if (OpRelFaturamento.DropDownItems[Nivel1].Name == TabAcesso.Tables[0].Rows[I]["Opcao"].ToString().Trim())
                            OpRelFaturamento.DropDownItems[Nivel1].Enabled = TabAcesso.Tables[0].Rows[I]["Acesso"].ToString().Trim() == "1";
                    }                    
                }                                
                //Producao.Visible           = !VersaoDistribuidor;
                //OpCadProducao.Visible      = !VersaoDistribuidor;
                Rh.Visible                 = !VersaoDistribuidor;
                OpAgendaVisita.Visible     = !VersaoDistribuidor;
                OpControleInst.Visible     = !VersaoDistribuidor;
                OpCotacaoCompra.Visible    = !VersaoDistribuidor;
                OpDIEF.Visible             = !VersaoDistribuidor;                
                OpVdInternet.Visible       = !VersaoDistribuidor;
                OpAgendaVisita.Visible     = !VersaoDistribuidor;
            }
        }
        public void RegistrarAuditoria(string Opcao, int Id, string Doc, int Operacao, string Descricao)
        {
            RegAuditoria.IdUsuario = Perfil_Usuario.IdUsuario;
            RegAuditoria.Terminal  = Conexao.WorkstationId;
            RegAuditoria.Data      = DtHrServidor();
            RegAuditoria.Opcao     = Opcao;
            RegAuditoria.IdChave   = Id;
            RegAuditoria.Documento = Doc;
            RegAuditoria.Operacao  = Operacao;
            RegAuditoria.Descricao = Descricao;
            RegAuditoria.Registrar();
        }
        private Controles.Verificar.StatusCaixaBalcao CaixaBalcao()
        {
            Controles.Verificar VerifCxa = new Verificar();
            VerifCxa.Controle = Controle;
            Controles.Verificar.StatusCaixaBalcao StaCx = VerifCxa.StatusCxaBalcao(Perfil_Usuario.IdUsuario);
            if (StaCx == Controles.Verificar.StatusCaixaBalcao.NaoAberto)
                BCima_LblCaixa.Text = "Não foi Aberto";
            else if (StaCx == Controles.Verificar.StatusCaixaBalcao.Aberto)
                BCima_LblCaixa.Text = "Aberto";
            else if (StaCx == Controles.Verificar.StatusCaixaBalcao.Fechado)
                BCima_LblCaixa.Text = "Já Fechado";
            else if (StaCx == Controles.Verificar.StatusCaixaBalcao.DtCxDif)
                BCima_LblCaixa.Text = "Aberto em Outra Data";
            return StaCx;
        }
        public TelaPrincipal()
        {   
            InitializeComponent();

            FileInfo Logo = new FileInfo(@"SGE_Logo.jpg");
            if (Logo.Exists)
                this.BackgroundImage = new Bitmap(@"SGE_Logo.jpg");

            LimpaClickBotoes();
            BSta_NmUsuario.Text     = "";
            BSta_Servidor.Text      = "";
            BSta_StaConexao.Text    = "Desconectado";
            BSta_Estacao.Text       = "";
            BtnAutDebito.Visible    = false;
            BtnEnviarFinanc.Visible = false;
            this.Text = "ERP - (SGE - Sistema de Gestão Empresarial) Versão "+Release;
            Rel_RodaPe = "ERP - (SGE - Sistema de Gestão Empresarial) Versão " + Release;
        }
        private void TelaPrincipal_Load(object sender, EventArgs e)
        {
            if (LerConfig())
            {
                try
                {
                    if (!Directory.Exists(@PathAtlz))                    
                        MessageBox.Show("Pasta de Atualização do Sistema não Localizada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        FileInfo ExeAtual = new FileInfo("ERP-SGE.EXE");
                        FileInfo ExeAtlz = new FileInfo(@PathAtlz + @"\ERP-SGE.EXE");

                        if (ExeAtual.LastWriteTime < ExeAtlz.LastWriteTime)
                        {
                            Process myProcess = System.Diagnostics.Process.Start("AtualizarERP-SGE.exe");
                            Close();
                        }
                    }
                    FrmLogin Login     = new FrmLogin();
                    Login.FrmPrincipal = this;
                    Login.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (!Login.AcessoOk)
                        Close();
                    else
                    {
                        LblLocalConexao.Text = Login.LstConexao.Text.Trim();
                        Parametros_Filial = new Controle_Dados.Parametros();
                        Parametros_Filial.Controle = Controle;
                        if (Perfil_Usuario != null)
                        {
                            if (Perfil_Usuario.MultplaInstancia == 0)
                            {
                                Process Instancia = Process.GetCurrentProcess();
                                string NomeInstancia = Instancia.ProcessName;
                                if (Process.GetProcessesByName(NomeInstancia).Length > 1)
                                {
                                    MessageBox.Show("Já existe uma instância do aplicativo em aberto");
                                    Close();
                                }
                            }
                            BSta_NmUsuario.Text = Perfil_Usuario.Usuario;
                            LstFilial = PopularCombo("SELECT Id_Filial,FANTASIA AS FILIAL FROM Empresa_Filial ORDER BY FANTASIA", LstFilial);
                            LstFilial.SelectedValue = IdFilialConexao;
                            Parametros_Filial.LerDados(IdFilialConexao);
                            BtnAutDebito.Visible    = Perfil_Usuario.LiberaDebito == 1;
                            BtnEnviarFinanc.Visible = Perfil_Usuario.EnviarFinanc == 1;
                            BtnAtlz.Visible         = Perfil_Usuario.AtlzBD == 1;
                            CaixaBalcao();                            
                            RegistrarAuditoria("Login Acesso", Perfil_Usuario.IdUsuario, "Login", 0, "Login de Acesso: "+Perfil_Usuario.Usuario);
                            
                            
                            
                        }
                        else
                            Parametros_Filial.LerDados(0);
                    }
                    AtualizarAcesso();
                    //Verificar Estoque Minimo
                    if (Perfil_Usuario.VerificarEstMin == 1)
                    {
                        FrmMostrarEstMin FrmEstMin = new FrmMostrarEstMin();
                        FrmEstMin.FrmPrincipal = this;
                        FrmEstMin.ShowDialog();
                    }
                    if (Perfil_Usuario.IdVendedor > 0)
                    {
                        FrmAvisoAgenda FrmVisita = new FrmAvisoAgenda();
                        FrmVisita.FrmPrincipal = this;
                        FrmVisita.IdVendedor = Perfil_Usuario.IdVendedor;
                        FrmVisita.ShowDialog();
                    }
                }
                catch 
                {                                        
                    MessageBox.Show("Servidor não Localizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            else
                Close();
        }
        private void TelaPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count > 1)
            {
                MessageBox.Show("Favor fechar todas as telas antes de sair do sistema", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }       
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void BtnTrocaUsuario_Click(object sender, EventArgs e)
        {
            TelaPrincipal_Load(this, new EventArgs());
        }        
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            this.ClickBtnNovo(this.ClickBtnNovo, new EventArgs());
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            this.ClickBtnEditar(this.ClickBtnEditar, new EventArgs());
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {   
            this.ClickBtnGravar(this.ClickBtnGravar, new EventArgs());
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            this.ClickBtnExcluir(this.ClickBtnExcluir, new EventArgs());
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.ClickBtnCancelar(this.ClickBtnCancelar, new EventArgs());
        }
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.ClickBtnFechar(this.ClickBtnFechar, new EventArgs());
        }
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
            BuscaPessoa.FrmPrincipal = this;
            BuscaPessoa.ShowDialog();
        }
        private void BtnBuscaProduto_Click(object sender, EventArgs e)
        {
            FrmBuscaProduto BuscaProduto = new FrmBuscaProduto();
            BuscaProduto.FrmPrincipal = this;
            BuscaProduto.ShowDialog();
        }
        private void BtnAutDebito_Click(object sender, EventArgs e)
        {
            FrmAutDebitoPessoa AutDeb = new FrmAutDebitoPessoa();
            AutDeb.FrmPrincipal = this;
            AutDeb.ShowDialog();
        }
        private void BtnEnviarFinanc_Click(object sender, EventArgs e)
        {
            FrmEnviarFinanc Frm = new FrmEnviarFinanc();
            Frm.FrmPrincipal = this;
            Frm.ShowDialog();
        }
        private void BtnAtlz_Click(object sender, EventArgs e)
        {
            FrmAtlzDados FrmAtlz = new FrmAtlzDados();
            FrmAtlz.FrmPrincipal = this;
            FrmAtlz.ShowDialog();
        }
        private void TelaPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Alt && e.KeyCode == Keys.F2)
            if (e.KeyCode == Keys.F2 && BtnBuscaProduto.Enabled)
                BtnBuscaProduto_Click(BtnBuscaProduto, null);
            else if (e.KeyCode == Keys.F3 && BtnBuscaPessoa.Enabled)
                BtnBuscaPessoa_Click(BtnBuscaPessoa, null);
            else if (e.KeyCode == Keys.F5 && BtnNovo.Enabled)
                BtnNovo_Click(BtnNovo, null);
            else if (e.KeyCode == Keys.F6 && BtnEditar.Enabled)
                BtnEditar_Click(BtnEditar, null);
            else if (e.KeyCode == Keys.F7 && BtnGravar.Enabled)
                BtnGravar_Click(BtnGravar, null);
            else if (e.KeyCode == Keys.F8 && BtnExcluir.Enabled)
                BtnExcluir_Click(BtnExcluir, null);
            else if (e.KeyCode == Keys.F9 && BtnCancelar.Enabled)
                BtnCancelar_Click(BtnCancelar, null);
            else if (e.KeyCode == Keys.F10 && BtnFechar.Enabled)
                BtnFechar_Click(BtnFechar, null);
        }
        
        private void OpFiliais_Click(object sender, EventArgs e)
        {   
            if (ChamaTela("FrmCadEmpresaFilial"))
            {
                CadFilial = new FrmCadEmpresaFilial();
                CadFilial.MdiParent = this;
                CadFilial.FrmPrincipal = this;
                CadFilial.WindowState = this.WindowState;
                CadFilial.Show();
            }
            else
                CadFilial.Activate();
        }
        private void OpUsuarios_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadUsuario"))
            {
                CadUsuario = new FrmCadUsuario();
                CadUsuario.MdiParent = this;
                CadUsuario.FrmPrincipal = this;
                CadUsuario.WindowState = this.WindowState;
                CadUsuario.Show();
            }
            else
                CadUsuario.Activate();
        }
        private void OpEstados_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadEstados"))
            {
                CadEstado = new FrmCadEstados();
                CadEstado.MdiParent = this;
                CadEstado.FrmPrincipal = this;
                CadEstado.WindowState = this.WindowState;
                CadEstado.Show();
            }
            else
                CadEstado.Activate();

        }
        private void OpRamoAtividade_Click(object sender, EventArgs e)
        {
            FrmCadAtividades Frm = new FrmCadAtividades();
            Frm.FrmPrincipal = this;
            Frm.ShowDialog();
        }
        private void OpCFOP_Click(object sender, EventArgs e)
        {
            
            if (ChamaTela("FrmCadCfop"))
            {
                CadCFOP = new FrmCadCfop();
                CadCFOP.MdiParent = this;
                CadCFOP.FrmPrincipal = this;
                CadCFOP.WindowState = this.WindowState;
                CadCFOP.Show();
            }
            else
                CadCFOP.Activate();
        }
        private void OpRotas_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadRotas"))
            {
                CadRotas = new FrmCadRotas();
                CadRotas.MdiParent = this;
                CadRotas.FrmPrincipal = this;
                CadRotas.WindowState = this.WindowState;
                CadRotas.Show();
            }
            else
                CadRotas.Activate();
        }
        private void OpDepartamento_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadDepartamento"))
            {
                CadDepartamentos = new FrmCadDepartamento();
                CadDepartamentos.MdiParent = this;
                CadDepartamentos.FrmPrincipal = this;
                CadDepartamentos.WindowState = this.WindowState;
                CadDepartamentos.Show();
            }
            else
                CadDepartamentos.Activate();
        }
        private void OpTransportadora_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadTransportadora"))
            {
                CadTransportadora = new FrmCadTransportadora();
                CadTransportadora.MdiParent = this;
                CadTransportadora.FrmPrincipal = this;
                CadTransportadora.WindowState = this.WindowState;
                CadTransportadora.Show();
            }
            else
                CadTransportadora.Activate();
        }       
        private void OpCentroCusto_Click(object sender, EventArgs e)
        {
            FrmCadCtaCusto FrmCta = new FrmCadCtaCusto();
            FrmCta.FrmPrincipal = this;
            FrmCta.ShowDialog();
        }
        private void OpTipoDocumento_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadTipoDocumento"))
            {
                CadTipoDocum = new FrmCadTipoDocumento();
                CadTipoDocum.MdiParent = this;
                CadTipoDocum.FrmPrincipal = this;
                CadTipoDocum.WindowState = this.WindowState;
                CadTipoDocum.Show();
            }
            else
                CadTipoDocum.Activate();
        }
        private void OpBancos_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadBancos"))
            {
                CadBanco = new FrmCadBancos();
                CadBanco.MdiParent = this;
                CadBanco.FrmPrincipal = this;
                CadBanco.WindowState = this.WindowState;
                CadBanco.Show();
            }
            else
                CadBanco.Activate();
        }
        private void OpContaCaixa_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadContaCaixa"))
            {
                CadCaixa = new FrmCadContaCaixa();
                CadCaixa.MdiParent = this;
                CadCaixa.FrmPrincipal = this;
                CadCaixa.WindowState = this.WindowState;
                CadCaixa.Show();
            }
            else
                CadCaixa.Activate();

        }
        private void OpFormaPgto_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadFormaPgto"))
            {
                CadFormaPgto = new FrmCadFormaPgto();
                CadFormaPgto.MdiParent = this;
                CadFormaPgto.FrmPrincipal = this;
                CadFormaPgto.WindowState = this.WindowState;
                CadFormaPgto.Show();
            }
            else
                CadFormaPgto.Activate();

        }
        private void OpPessoas_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadPessoas"))
            {
                CadPessoa = new FrmCadPessoas();
                CadPessoa.MdiParent = this;
                CadPessoa.FrmPrincipal = this;
                CadPessoa.WindowState = this.WindowState;
                CadPessoa.Show();
            }
            else
                CadPessoa.Activate();

        }
        private void OpGrupoPrd_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadGrupoProduto"))
            {
                CadGrupoPrd = new FrmCadGrupoProduto();
                CadGrupoPrd.MdiParent = this;
                CadGrupoPrd.FrmPrincipal = this;
                CadGrupoPrd.WindowState = this.WindowState;
                CadGrupoPrd.Show();
            }
            else
                CadGrupoPrd.Activate();
        }
        private void OpGeneroPrd_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadGenero"))
            {
                CadGenero = new FrmCadGenero();
                CadGenero.MdiParent = this;
                CadGenero.FrmPrincipal = this;
                CadGenero.WindowState = this.WindowState;
                CadGenero.Show();
            }
            else
                CadGenero.Activate();
        }
        private void produtosEServiçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadProduto"))
            {
                CadProduto = new FrmCadProduto();
                CadProduto.MdiParent = this;
                CadProduto.FrmPrincipal = this;
                CadProduto.WindowState = this.WindowState;
                CadProduto.Show();
            }
            else
                CadProduto.Activate();
        }
        private void OpVendedores_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadVendedor"))
            {
                CadVendedor = new FrmCadVendedor();
                CadVendedor.MdiParent = this;
                CadVendedor.FrmPrincipal = this;
                CadVendedor.WindowState = this.WindowState;
                CadVendedor.Show();
            }
            else
                CadVendedor.Activate();
        }
        private void OpEntregador_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadEntregador"))
            {
                CadEntregador = new FrmCadEntregador();
                CadEntregador.MdiParent = this;
                CadEntregador.FrmPrincipal = this;
                CadEntregador.WindowState = this.WindowState;
                CadEntregador.Show();
            }
            else
                CadEntregador.Activate();
        }
        private void OpCotacaoCompra_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCotacao"))
            {
                Cotacao = new FrmCotacao();
                Cotacao.MdiParent = this;
                Cotacao.FrmPrincipal = this;
                Cotacao.WindowState = this.WindowState;
                Cotacao.Show();
            }
            else
                Cotacao.Activate();
        }
        private void OpPedidoCompra_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmPedCompra"))
            {
                PedCompra = new FrmPedCompra();
                PedCompra.MdiParent = this;
                PedCompra.FrmPrincipal = this;
                PedCompra.WindowState = this.WindowState;
                PedCompra.Show();
            }
            else
                PedCompra.Activate();
        }
        private void OpMovEstoque_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvEstoque"))
            {
                MovEstoque = new FrmMvEstoque();
                MovEstoque.MdiParent = this;
                MovEstoque.FrmPrincipal = this;
                MovEstoque.WindowState = this.WindowState;
                MovEstoque.Show();
            }
            else
                MovEstoque.Activate();
            
        }
        private void OpCtaPagRec_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmFinanceiro"))
            {
                FrmFinanc = new FrmFinanceiro();
                FrmFinanc.MdiParent = this;
                FrmFinanc.FrmPrincipal = this;
                FrmFinanc.WindowState = this.WindowState;
                FrmFinanc.Show();
            }
            else
                FrmFinanc.Activate();
        }
        private void OpAbrirCx_Click(object sender, EventArgs e)
        {
            Controles.Verificar.StatusCaixaBalcao StaCx = CaixaBalcao();
            if (StaCx == Controles.Verificar.StatusCaixaBalcao.NaoAberto)
            {
                FrmAbrirCx = new FrmCxaAbrir();
                FrmAbrirCx.FrmPrincipal = this;
                FrmAbrirCx.ShowDialog();
                CaixaBalcao();
            }
            else
            {
                if (StaCx==Controles.Verificar.StatusCaixaBalcao.Aberto)
                    MessageBox.Show("Caixa já foi aberto", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (StaCx == Controles.Verificar.StatusCaixaBalcao.Fechado)
                    MessageBox.Show("Caixa já foi fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (StaCx == Controles.Verificar.StatusCaixaBalcao.DtCxDif)
                    MessageBox.Show("Caixa já aberto em outra data", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpOrcamento_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "OC";
                MvVendas.PagCab.Controls.RemoveAt(2);
                MvVendas.BoxStatus.Visible = false;
                MvVendas.LblVd.Text = "No.Orçam.:";
                MvVendas.LblVenda.Text = "No.Orçam.:";
                MvVendas.BoxItensPesq.Location = MvVendas.BoxStatus.Location;
                MvVendas.BtnConfirmar.Visible = false;
                MvVendas.BtnCancMov.Visible = false;
                MvVendas.Text = "Venda (Orçamento)";
                MvVendas.BtnCopiarVenda.Text = "Gerar Ped.Venda";
                MvVendas.BtnCopiarVenda.Visible = true;
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }
        private void OpOrdemEntrega_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "OE";                
                MvVendas.LblVd.Text = "No.Ordem:";
                MvVendas.LblVenda.Text = "No.Ordem:";
                MvVendas.Text = "Venda (Ordem de Entrega)";
                MvVendas.BtnCopiarVenda.Text = "Copiar Ordem Entrega";
                MvVendas.BtnCopiarVenda.Visible = true;
                MvVendas.Show();
            }
            else
                MvVendas.Activate();

        }
        private void OpPedidoVenda_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "PV";
                MvVendas.LblVd.Text = "No.Venda:";
                MvVendas.LblVenda.Text = "No.Venda:";
                MvVendas.Text = "Pedido de Venda";
                MvVendas.BtnCopiarVenda.Text = "Copiar Venda";
                MvVendas.BtnCopiarVenda.Visible = true;
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }
        private void OpComodato_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "CO";
                MvVendas.PagCab.Controls.RemoveAt(2);
                //MvVendas.BoxStatus.Visible = false;
                MvVendas.LblVd.Text = "Comodato:";
                MvVendas.LblVenda.Text = "Comodato:";
                //MvVendas.BoxItensPesq.Location = MvVendas.BoxStatus.Location;
                //MvVendas.BtnConfirmar.Visible = false;
                //MvVendas.BtnCancMov.Visible = false;                
                MvVendas.LstFormaPgto.Visible = false;
                MvVendas.LblPgto.Visible = false;
                MvVendas.Text = "Venda (Comodato)";
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }
        private void OpVdInternet_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "PI";
                MvVendas.LblVd.Text = "No.Venda:";
                MvVendas.LblVenda.Text = "No.Venda:";
                MvVendas.Text = "Pedido pela Internet (Distribuidor)";
                MvVendas.BtnCopiarVenda.Text = "Gerar Venda";
                MvVendas.BtnCopiarVenda.Visible = true;
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }
        private void OpTroca_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "TROCA";
                MvVendas.LblVd.Text = "No.Troca:";
                MvVendas.LblVenda.Text = "No.Troca:";
                MvVendas.Text = "Troca de Mercadoria";
                MvVendas.Show();
            }
            else
                MvVendas.Activate();

        }
        private void OpFaturamento_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmFaturamento"))
            {
                Faturamento = new FrmFaturamento();
                Faturamento.MdiParent = this;
                Faturamento.FrmPrincipal = this;
                Faturamento.WindowState = this.WindowState;
                Faturamento.TipoMov = "PV";                
                Faturamento.Show();
            }
            else
                Faturamento.Activate();
        }
        private void OpNotaFiscal_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmNotaFiscal"))
            {
                NotaFiscal = new FrmNotaFiscal();
                NotaFiscal.MdiParent = this;
                NotaFiscal.FrmPrincipal = this;
                NotaFiscal.WindowState = this.WindowState;                
                NotaFiscal.Show();
            }
            else
                NotaFiscal.Activate();
        }
        private void OpMapaEntrega_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMapaEntrega2"))
            {
                MapaEntrega = new FrmMapaEntrega2();
                MapaEntrega.MdiParent = this;
                MapaEntrega.FrmPrincipal = this;
                MapaEntrega.WindowState = this.WindowState;
                MapaEntrega.Show();
            }
            else
                MapaEntrega.Activate();
        }
        private void OpConfEntrega_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmConfEntrega"))
            {
                ConfEntrega = new FrmConfEntrega();
                ConfEntrega.MdiParent = this;
                ConfEntrega.FrmPrincipal = this;
                ConfEntrega.WindowState = this.WindowState;
                ConfEntrega.Show();
            }
            else
                ConfEntrega.Activate();
        }
        private void OpBoleto_Click(object sender, EventArgs e)
        {
            FrmImpBoleto ImpBoleto = new FrmImpBoleto();
            ImpBoleto.FrmPrincipal = this;
            ImpBoleto.ImpBoleto    = true;
            ImpBoleto.ImpProm = false;
            ImpBoleto.ShowDialog();
        }
        private void OpRelRecibo_Click(object sender, EventArgs e)
        {
            FrmImpBoleto ImpRecibo = new FrmImpBoleto();
            ImpRecibo.FrmPrincipal = this;
            ImpRecibo.ImpBoleto    = false;
            ImpRecibo.ImpProm = false;
            ImpRecibo.ShowDialog();
        }

        private void OpPromissoria_Click(object sender, EventArgs e)
        {
            FrmImpBoleto ImpPromissoria = new FrmImpBoleto();
            ImpPromissoria.FrmPrincipal = this;
            ImpPromissoria.ImpBoleto    = false;
            ImpPromissoria.ImpProm      = true;
            ImpPromissoria.ShowDialog();
        }

        private void OpBonificacao_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "BONIF";                
                MvVendas.LblVd.Text = "Bonificação:";
                MvVendas.LblVenda.Text = "Bonificação:";
                MvVendas.Text = "Venda (Bonificação)";
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }
        private void OpAmostra_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "AM";                
                MvVendas.LblVd.Text = "Amostra:";
                MvVendas.LblVenda.Text = "Amostra:";
                MvVendas.Text = "Venda (Amostra)";
                MvVendas.BtnCopiarVenda.Text = "Copiar Amostra";
                MvVendas.BtnCopiarVenda.Visible = true;
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }
        private void OpVendaFinanc_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "VF";
                MvVendas.LblVd.Text = "No.Venda:";
                MvVendas.LblVenda.Text = "No.Venda:";
                MvVendas.Text = "Venda Financeira";
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }  
        private void OpRelVenda_Click(object sender, EventArgs e)
        {
            FrmRelVendas RelVenda = new FrmRelVendas();
            RelVenda.FrmPrincipal = this;
            RelVenda.ShowDialog();
        }
        private void OpEntregaMerc_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState = this.WindowState;
                MvVendas.TipoMov = "EMVF";                
                MvVendas.LblVd.Text = "Entrega:";
                MvVendas.LblVenda.Text = "Entrega:";
                MvVendas.Text = "Entrega de Mercadoria (Venda Financeira)";
                MvVendas.BtnCopiarVenda.Text = "Copiar Venda";
                MvVendas.BtnCopiarVenda.Visible = true;
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }
        private void OpRelEstoque_Click(object sender, EventArgs e)
        {
            FrmRelEstoque RelEstoque = new FrmRelEstoque();
            RelEstoque.FrmPrincipal = this;
            RelEstoque.ShowDialog();
        }
        private void OpRelFatDiversos_Click(object sender, EventArgs e)
        {
            FrmRelFaturamento RelFat = new FrmRelFaturamento();
            RelFat.FrmPrincipal = this;
            RelFat.ShowDialog();
        }
        private void OpDIEF_Click(object sender, EventArgs e)
        {
            FrmDIEF DIEF = new FrmDIEF();
            DIEF.FrmPrincipal = this;
            DIEF.ShowDialog();
        }
        private void OpExportarACFortes_Click(object sender, EventArgs e)
        {
            FrmExpFortes FrmExp = new FrmExpFortes();
            FrmExp.FrmPrincipal = this;
            FrmExp.ShowDialog();           
        }
        private void OpControleCheque_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMovCheque"))
            {
                MovCheque = new FrmMovCheques();
                MovCheque.MdiParent = this;
                MovCheque.FrmPrincipal = this;
                MovCheque.WindowState = this.WindowState;
                MovCheque.Show();
            }
            else
                MovCheque.Activate();
        }
        private void OpControleInst_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmInstalacao"))
            {
                FrmInst = new FrmInstalacao();
                FrmInst.MdiParent = this;
                FrmInst.FrmPrincipal = this;
                FrmInst.WindowState = this.WindowState;
                FrmInst.Show();
            }
            else
                FrmInst.Activate();
        }
        private void OpPDV_Click(object sender, EventArgs e)
        {
            
            Controles.Verificar.StatusCaixaBalcao StaCx = CaixaBalcao();
            if (StaCx == Controles.Verificar.StatusCaixaBalcao.Aberto)
            {
                FrmPDV PDV = new FrmPDV();
                PDV_ImpressoraFiscal.Controle = Controle;
                PDV.FrmPrincipal = this;
                PDV.ShowDialog();
            }
            else
            {
                if (StaCx == Controles.Verificar.StatusCaixaBalcao.NaoAberto)
                    MessageBox.Show("Caixa não foi aberto", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (StaCx == Controles.Verificar.StatusCaixaBalcao.Fechado)
                    MessageBox.Show("Caixa já foi fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (StaCx == Controles.Verificar.StatusCaixaBalcao.DtCxDif)
                    MessageBox.Show("Caixa já aberto em outra data", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpLeituraX_Click(object sender, EventArgs e)
        {
            if (TipoImpressoraFiscal() != Controles.ImpressoraFiscal.ModeloImpressora.Nenhuma)
            {
                PDV_ImpressoraFiscal.ImpFiscal = TipoImpressoraFiscal();
                PDV_ImpressoraFiscal.LeituraX();
            }
            else           
                MessageBox.Show("Impressora Fiscal não instalada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
        private void OpFechaCaixa_Click(object sender, EventArgs e)
        {
            if (TipoImpressoraFiscal() != Controles.ImpressoraFiscal.ModeloImpressora.Nenhuma)
            {                
                Controles.Verificar.StatusCaixaBalcao StaCx = CaixaBalcao();
                if (StaCx == Controles.Verificar.StatusCaixaBalcao.Aberto)
                {
                    //if (MessageBox.Show("Confirma o Fechamento do caixa ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        
                        Controles.Verificar VerificarCaixa = new Verificar();
                        VerificarCaixa.Controle = Controle;
                        int IdCx = VerificarCaixa.VerificarCaixa(Perfil_Usuario.IdUsuario);
                        //Controle_Dados.CaixaBalcao CxBalcao = new Controle_Dados.CaixaBalcao();
                        //CxBalcao.Controle = Controle;
                        //CxBalcao.LerCaixa(VerificarCaixa.VerificarCaixa(Perfil_Usuario.IdUsuario));
                        //CxBalcao.Status = 1;
                        //CxBalcao.DtHrEnc = DateTime.Now;
                        //CxBalcao.FecharCaixa();
                        FrmFecharCxBalcao FrmFecharCx = new FrmFecharCxBalcao();
                        FrmFecharCx.FrmPrincipal = this;
                        FrmFecharCx.IdCaixa      = IdCx;
                        FrmFecharCx.ShowDialog();


                        if (FrmFecharCx.CxFechado)
                        {
                            /*if (MessageBox.Show("Imprime a Leitura Z ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                PDV_ImpressoraFiscal.ImpFiscal = TipoImpressoraFiscal();
                                PDV_ImpressoraFiscal.LeituraZ();
                            }*/
                            if (!VersaoDistribuidor && Perfil_Usuario.EnviarFinanc == 1)
                            {
                                FrmEnviarFinanc Frm = new FrmEnviarFinanc();
                                Frm.FrmPrincipal = this;
                                Frm.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    if (StaCx == Controles.Verificar.StatusCaixaBalcao.NaoAberto)
                        MessageBox.Show("Caixa não foi aberto", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (StaCx == Controles.Verificar.StatusCaixaBalcao.Fechado)
                        MessageBox.Show("Caixa já foi fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Impressora Fiscal não instalada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void OpLeituraZ_Click(object sender, EventArgs e)
        {
            if (TipoImpressoraFiscal() != Controles.ImpressoraFiscal.ModeloImpressora.Nenhuma)
            {
                PDV_ImpressoraFiscal.ImpFiscal = TipoImpressoraFiscal();
                PDV_ImpressoraFiscal.LeituraZ();
            }
            else
                MessageBox.Show("Impressora Fiscal não instalada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void OpSepararPrd_Click(object sender, EventArgs e)
        {
            FrmSepararPrd FrmSepPrd = new FrmSepararPrd();            
            FrmSepPrd.FrmPrincipal = this;
            FrmSepPrd.ShowDialog();
        }        
        private void OpAgCobrador_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmAgenteCobrador"))
            {
                FrmAgCobrador = new FrmAgenteCobrador();
                FrmAgCobrador.MdiParent = this;
                FrmAgCobrador.FrmPrincipal = this;
                FrmAgCobrador.WindowState = this.WindowState;
                FrmAgCobrador.Show();
            }
            else
                FrmAgCobrador.Activate();

        }
        private void OpAIDF_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmAIDF"))
            {
                FrmCadAIDF = new FrmAIDF();
                FrmCadAIDF.MdiParent = this;
                FrmCadAIDF.FrmPrincipal = this;
                FrmCadAIDF.WindowState = this.WindowState;
                FrmCadAIDF.Show();
            }
            else
                FrmCadAIDF.Activate();            
        }
        private void OpRelFinanceiro_Click(object sender, EventArgs e)
        {
            FrmRelFinanceiro RelFinanc = new FrmRelFinanceiro();
            RelFinanc.FrmPrincipal = this;
            RelFinanc.ShowDialog();
        }
        private void OpLivroCaixa_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmLancLivroCxa"))
            {
                FrmLivroCxa = new FrmLancLivroCxa();
                FrmLivroCxa.MdiParent = this;
                FrmLivroCxa.FrmPrincipal = this;
                FrmLivroCxa.WindowState = this.WindowState;
                FrmLivroCxa.Show();
            }
            else
                FrmLivroCxa.Activate();
        }
        private void OpVerifSldEst_Click(object sender, EventArgs e)
        {
            FrmAcertoEstoque FrmAcerto = new FrmAcertoEstoque();
            FrmAcerto.FrmPrincipal = this;
            FrmAcerto.ShowDialog();
        }
        private void OpMemoriaFiscal_Click(object sender, EventArgs e)
        {
            //ImprimirLeituraMemoriaFiscal
        }
        private void OpSincNota_Click(object sender, EventArgs e)
        {
            FrmSincronismo FrmSinc = new FrmSincronismo();
            FrmSinc.FrmPrincipal = this;
            FrmSinc.ShowDialog();
        }
        private void OpCadFuncionario_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmCadFuncionarios"))
            {
                FrmCadFunc = new FrmCadFuncionarios();
                FrmCadFunc.MdiParent = this;
                FrmCadFunc.FrmPrincipal = this;
                FrmCadFunc.WindowState = this.WindowState;
                FrmCadFunc.Show();
            }
            else
                FrmCadFunc.Activate();
        }
        private void OpAgendaVisita_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmAgendaVisita"))
            {
                FrmAgVisita = new FrmAgendaVisita();
                FrmAgVisita.MdiParent = this;
                FrmAgVisita.FrmPrincipal = this;
                FrmAgVisita.WindowState = this.WindowState;
                FrmAgVisita.Show();
            }
            else
                FrmAgVisita.Activate();
        }
        private void OpRelAdministrativo_Click(object sender, EventArgs e)
        {
            FrmRelAdm RelAdm = new FrmRelAdm();
            RelAdm.FrmPrincipal = this;
            RelAdm.ShowDialog();
        }
        private void OpPromocao_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmPromocoes"))
            {
                FrmPromocao = new FrmPromocoes();
                FrmPromocao.MdiParent = this;
                FrmPromocao.FrmPrincipal = this;
                FrmPromocao.WindowState = this.WindowState;
                FrmPromocao.Show();
            }
            else
                FrmPromocao.Activate();
        }
        private void OpEventosProvDesc_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmEventosProvDesc"))
            {
                FrmEventosPD = new FrmEventosProvDesc();
                FrmEventosPD.MdiParent = this;
                FrmEventosPD.FrmPrincipal = this;
                FrmEventosPD.WindowState = this.WindowState;
                FrmEventosPD.Show();
            }
            else
                FrmEventosPD.Activate();
        }

        private void OpGerarFolhaPag_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmGerarFolha"))
            {
                FrmGerarFP = new FrmGerarFolha();
                FrmGerarFP.MdiParent    = this;
                FrmGerarFP.FrmPrincipal = this;
                FrmGerarFP.WindowState  = this.WindowState;
                FrmGerarFP.Show();
            }
            else
                FrmGerarFP.Activate();
        }

        private void OpRelRH_Click(object sender, EventArgs e)
        {
            FrmRelRH FrmRel = new FrmRelRH();
            FrmRel.FrmPrincipal = this;
            FrmRel.ShowDialog();
        }

        private void OpLancPrevCusto_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmPrevCustos"))
            {
                FrmPrvCusto = new FrmPrevCustos();
                FrmPrvCusto.MdiParent    = this;
                FrmPrvCusto.FrmPrincipal = this;
                FrmPrvCusto.WindowState  = this.WindowState;
                FrmPrvCusto.Show();
            }
            else
                FrmPrvCusto.Activate();
        }

        private void OpVeiculos_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmVeiculos"))
            {
                FrmVeiculos = new FrmVeiculos();
                FrmVeiculos.MdiParent    = this;
                FrmVeiculos.FrmPrincipal = this;
                FrmVeiculos.WindowState  = this.WindowState;
                FrmVeiculos.Show();
            }
            else
                FrmVeiculos.Activate();
        }

        public DateTime DtHrServidor()
        {
            SqlDataReader TabDT = Controle.ConsultaSQL("SELECT GETDATE()");
            if (TabDT.HasRows)
            {
                TabDT.Read();
                return DateTime.Parse(TabDT[0].ToString());
            }
            else
                return DateTime.Now;
        }

        private void OpBalanco_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmBalanco"))
            {
                FrmBalanc = new FrmBalanco();
                FrmBalanc.MdiParent    = this;
                FrmBalanc.FrmPrincipal = this;
                FrmBalanc.WindowState  = this.WindowState;
                FrmBalanc.Show();
            }
            else
                FrmBalanc.Activate();
        }

        private void OpPremiacao_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmPremiacao"))
            {
                FrmPremio = new FrmPremiacao();
                FrmPremio.MdiParent = this;
                FrmPremio.FrmPrincipal = this;
                FrmPremio.WindowState = this.WindowState;
                FrmPremio.Show();
            }
            else
                FrmPremio.Activate();
        }

        private void OpGradeComodato_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmGradeComodato"))
            {
                FrmGradeComod = new FrmGradeComodato();
                FrmGradeComod.MdiParent = this;
                FrmGradeComod.FrmPrincipal = this;
                FrmGradeComod.WindowState = this.WindowState;
                FrmGradeComod.Show();
            }
            else
                FrmGradeComod.Activate();
        }

        private void OpCadProducao_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmProducao"))
            {
                FrmCadProducao              = new FrmProducao();
                FrmCadProducao.MdiParent    = this;
                FrmCadProducao.FrmPrincipal = this;
                FrmCadProducao.WindowState  = this.WindowState;
                FrmCadProducao.Show();
            }
            else
                FrmCadProducao.Activate();
        }

        private void OpPromocaoPrd_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmPromocaoPrd"))
            {
                FrmPromPrd              = new FrmPromocaoPrd();
                FrmPromPrd.MdiParent    = this;
                FrmPromPrd.FrmPrincipal = this;
                FrmPromPrd.WindowState  = this.WindowState;
                FrmPromPrd.Show();
            }
            else
                FrmPromPrd.Activate();
        }

        private void OpMapaProducao_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMapaProducao"))
            {
                FrmMapaProd = new FrmMapaProducao();
                FrmMapaProd.MdiParent    = this;
                FrmMapaProd.FrmPrincipal = this;
                FrmMapaProd.WindowState  = this.WindowState;
                FrmMapaProd.Show();
            }
            else
                FrmMapaProd.Activate();
        }

        private void OpPedConsignacao_Click(object sender, EventArgs e)
        {
            if (ChamaTela("FrmMvVendas"))
            {
                MvVendas = new FrmMvVendas();
                MvVendas.MdiParent    = this;
                MvVendas.FrmPrincipal = this;
                MvVendas.WindowState   = this.WindowState;
                MvVendas.TipoMov = "PC";
                MvVendas.LblVd.Text = "Pedido Consignação:";
                MvVendas.LblVenda.Text = "Consignação:";
                MvVendas.Text = "Venda (Pedido Consignação)";
                MvVendas.Show();
            }
            else
                MvVendas.Activate();
        }

        private void OpLoteSantander_Click(object sender, EventArgs e)
        {
            FrmEnviarLoteFP FrmLoteFP = new FrmEnviarLoteFP();
            FrmLoteFP.FrmPrincipal = this;
            FrmLoteFP.ShowDialog();
        }
    }
}
