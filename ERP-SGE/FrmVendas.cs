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
using CDSSoftware;
using System.Collections;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Printing;

namespace ERP_SGE
{
    public partial class FrmMvVendas : Form
    {
        public MvVenda Vendas            = new MvVenda();
        public MvVendaItens ItemMvVendas = new MvVendaItens();
        Funcoes Controle            = new Funcoes();        
        Pessoas CadPessoa           = new Pessoas();
        TabelasAux TabAux           = new TabelasAux();
        Auditoria RegAuditoria      = new Auditoria();
        Parametros ParamFilial      = new Parametros();  // Determina qual filial vai verificar o estoque          
        Vendedores CadVend          = new Vendedores();
        FormaPagamento CadFormaPgto = new FormaPagamento();
        
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        public bool NovoReg       = false;
        public string TipoMov;
        // Tabelas
        private DataSet TabItens;
        private BindingSource Source_Itens;
        private TClientSocket.ClientSocket ImpSocket;
        private bool RegistroInf = false;

        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        // Um método que verifica se esta conectado
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }

        public FrmMvVendas()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        public void Inicializar()
        {
            //Instanciando os Controles            
            Controle.Conexao      = FrmPrincipal.Conexao;
            Vendas.Controle       = Controle;
            ItemMvVendas.Controle = Controle;
            CadPessoa.Controle    = Controle;
            TabAux.Controle       = Controle;
            RegAuditoria.Controle = Controle;
            ParamFilial.Controle  = Controle;
            CadVend.Controle      = Controle;
            CadFormaPgto.Controle = Controle;
            CamposLista();

            Vendas.IdVenda        = 0;
            Rb_Aberto.Checked     = true;
            Rb_TpVd.Checked       = true;
            Chk_Periodo.Checked   = false;
            Dt1.Value             = DateTime.Now;
            Dt2.Value             = DateTime.Now;
            PopularGrid();
            // Instanciando as Tabelas
            TabItens              = new DataSet();
            Source_Itens          = new BindingSource();
            BoxTpVenda.Visible    = TipoMov != "OC" && TipoMov != "PI";
            PnlVdTroca.Visible    = TipoMov == "TROCA" && !FrmPrincipal.VersaoDistribuidor;
            PnlPedCompra.Visible  = TipoMov == "PV";
            BoxUltOrc.Visible     = TipoMov == "OC" && !FrmPrincipal.VersaoDistribuidor;
            LblCusto.Visible      = FrmPrincipal.Perfil_Usuario.MostraCustoVd == 1;
            LblPrcEspDist.Visible = FrmPrincipal.Perfil_Usuario.UsaPrcEspDist == 1;
            ParamFilial.LerDados(FrmPrincipal.Parametros_Filial.IdFilial);
            
            Hab_Botoes();

            LblVlrUltOrc.Text = "";
            LblDtUltOrc.Text = "";
            LblVendUltOrc.Text = "";
            LblFilialUltOrc.Text = "";
            LstPesqPessoa.SelectedIndex = 0;

                        //Abrindo a Conexao Socket
            ImpSocket = new TClientSocket.ClientSocket(FrmPrincipal.IPSocket, int.Parse(FrmPrincipal.PortaSocket));
            ImpSocket.Connect();
        }

        private void CamposLista()
        {            
            LstPesqVendedor   = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstPesqVendedor, "Todos");
            LstPesqFilial     = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqFilial,"Todas");
            LstFilial         = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "");
            LstEntrega        = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstEntrega, "Informe a Filial de Entrega");
            LstPesqEntrega    = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqEntrega, "Todas Filiais");            
            LstTipoVenda      = FrmPrincipal.PopularCombo("SELECT CHAVE,SUBSTRING(DESCRICAO,1,30) AS DESCRICAO FROM TABELASAUX WHERE CAMPO='VENDA' ORDER BY DESCRICAO", LstTipoVenda);
            LstVendedor       = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstVendedor);
            LstUF             = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUF);
            LstFormaPgto      = FrmPrincipal.PopularCombo("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento where ativo=1 ORDER BY FormaPgto", LstFormaPgto);
            ColNomeFilial     = FrmPrincipal.PopularComboGrid("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", ColNomeFilial);
            ColVendedor       = FrmPrincipal.PopularComboGrid("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores ORDER BY Vendedor", ColVendedor);            
            LstPesqEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstPesqEntregador, "Todos");            
            LstPais           = FrmPrincipal.PopularCombo("SELECT CHAVE,DESCRICAO AS PAIS FROM TABELASAUX WHERE CAMPO='PAIS' ORDER BY DESCRICAO", LstPais);

            LstTipoVenda.SelectedValue    = TipoMov;
            LstPesqVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();
            LstPesqVendedor.Enabled       = FrmPrincipal.Perfil_Usuario.SeusMov == 0;
            LstVendedor.Enabled           = FrmPrincipal.Perfil_Usuario.VendedorBalcao == 0;
            TabAux.LerTabela("VENDA", TipoMov);     

            //
            CadVend.LerDados(FrmPrincipal.Perfil_Usuario.IdVendedor);
            if (CadVend.CotaFinanceira == 1 && CadVend.IdVendGrupo > 0 && !FrmPrincipal.VersaoDistribuidor)
            {

                LstPesqVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor, SubString(Vendedor, 1, 40) as Vendedor FROM Vendedores" +
                                                            " WHERE ATIVO = 1 and Id_Vendedor in (Select Id_Vendedor from Vendedores where Id_VendGrupo in (select Id_Vendedor from vendedores where CotaFinanceira = 1 and id_vendedor in (select id_vendedor from usuarios where id_filial = " + FrmPrincipal.Perfil_Usuario.IdFilial + "))) ORDER BY Vendedor", LstPesqVendedor);
                //LstPesqVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 " +
                //                                        " and Id_Vendedor in (Select Id_Vendedor from Vendedores where Id_VendGrupo=" + CadVend.IdVendedor.ToString() + ") ORDER BY Vendedor", LstPesqVendedor);

                //LstVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 " +
                //                                        " and Id_Vendedor in (Select Id_Vendedor from Vendedores where Id_VendGrupo=" + CadVend.IdVendedor.ToString() + ") ORDER BY Vendedor", LstVendedor);

                LstVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor, SubString(Vendedor, 1, 40) as Vendedor FROM Vendedores"+
                                                        " WHERE ATIVO = 1 and Id_Vendedor in (Select Id_Vendedor from Vendedores where Id_VendGrupo in (select Id_Vendedor from vendedores where CotaFinanceira = 1 and id_vendedor in (select id_vendedor from usuarios where id_filial = "+FrmPrincipal.Perfil_Usuario.IdFilial+ "))) ORDER BY Vendedor", LstVendedor);

                LstPesqVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();
                LstPesqVendedor.Enabled = true;
            }
        }
        private void PopularGrid()
        {
            string Filtro = "";
            if (Rb_Todos.Checked)
                Filtro = "WHERE T1.STATUS <= 4";
            else if (Rb_Aberto.Checked)
                Filtro = "WHERE T1.STATUS = 0";
            else if (Rb_Confirmado.Checked)
                Filtro = "WHERE T1.STATUS = 1";
            else if (Rb_Faturado.Checked)
                Filtro = "WHERE T1.STATUS = 2";
            else if (Rb_Entregue.Checked)
                Filtro = "WHERE T1.STATUS = 3";
            else if (Rb_Cancelado.Checked)
                Filtro = "WHERE T1.STATUS = 4";
            if (TxtPesqNumDoc.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMDOCUMENTO LIKE '%" + TxtPesqNumDoc.Text.Trim() + "%'";
            if (TxtPesqNumVd.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_VENDA =" + TxtPesqNumVd.Text.Trim();
            if (TxtPesqPessoa.Text.Trim() != "")
            {
                if (LstPesqPessoa.SelectedIndex==0)
                   Filtro = Filtro + " AND T2.RAZAOSOCIAL Like '%" + TxtPesqPessoa.Text.Trim() + "%'";
                else
                   Filtro = Filtro + " AND T2.CNPJ Like '%" + TxtPesqPessoa.Text.Trim() + "%'";
            }
            if (int.Parse(LstPesqFilial.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FILIAL = " + LstPesqFilial.SelectedValue.ToString();
            if (int.Parse(LstPesqEntregador.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Entregador = " + LstPesqEntregador.SelectedValue.ToString();
            if (int.Parse(LstPesqEntrega.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FilialEntrega = " + LstPesqEntrega.SelectedValue.ToString();

            if (!Rb_TpVd.Checked)
            {
                if (Rb_VdBalcao.Checked)
                    Filtro = Filtro + " AND T1.VDBALCAO=1";
                else
                    Filtro = Filtro + " AND T1.VDBALCAO=0";
            }

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (FrmPrincipal.Perfil_Usuario.SeusMov == 1)
            {
                Filtro = Filtro + " AND (T1.ID_USUARIO=" + FrmPrincipal.Perfil_Usuario.IdUsuario.ToString() + " OR T1.Id_VENDEDOR=" + LstPesqVendedor.SelectedValue.ToString() + ")";                
            }
            else
            {
                if (int.Parse(LstPesqVendedor.SelectedValue.ToString()) > 0)
                    Filtro = Filtro + " AND T1.Id_VENDEDOR=" + LstPesqVendedor.SelectedValue.ToString();
            }

            Filtro = Filtro + " AND T1.TPVENDA='" + TipoMov.Trim() + "'";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,T1.DATA,CASE T1.STATUS WHEN 0 THEN 'Em Aberto' WHEN 1 THEN 'Confirmado' WHEN 2 THEN 'Faturado' WHEN 3 THEN 'Entregue' WHEN 4 THEN 'Cancelado' END AS STATUS," +
                                             " T1.NUMDOCUMENTO,T1.PESSOA,T1.VLRTOTAL,T1.PREVENTREGA,T3.ENTREGADOR,T1.ID_FILIAL,T1.IMPNF,T1.FORMNF,T7.NUM_CF AS NUMCF,T1.ID_VENDEDOR,T1.ID_CAIXA,T1.DATACANCEL,T5.USUARIO,T1.VINCULOVD,T1.VDIMPFAT,T1.DtEnvioRec,T1.DtHrFaturamento FROM MVVENDA T1 " +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                             " LEFT JOIN ENTREGADORES T3 ON (T3.ID_ENTREGADOR=T1.ID_ENTREGADOR)  " +
                                             " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR) " +
                                             " LEFT JOIN USUARIOS T5 ON (T5.ID_USUARIO=T1.ID_USUARIO) " +
                                             " LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T1.ID_FILIAL) " +
                                             " LEFT JOIN CUPOMFISCAL T7 ON (T7.ID_LANC=T1.ID_LANCCF)" +
                                             Filtro + " ORDER BY T1.DATA DESC,T1.ID_VENDA DESC");

            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Venda", Vendas.IdVenda);
            Source.Position = item; 

            if (TipoMov == "OC" || TipoMov == "CO")
            {
                if (TipoMov == "OC")
                    GridDados.Columns[0].HeaderText = "No.Orç.";
                else if (TipoMov == "CO")
                    GridDados.Columns[0].HeaderText = "Comodato";                
            }
            ColTpItem.Items.Clear();
            ColTpItem.Items.Add("S");
            ColTpItem.Items.Add("E");
            ColTpItem.Items.Add("N");
            ColTpItem.ToolTipText = "S/E/N";

            ColItemPed.Visible = TipoMov == "PV";

            if (TipoMov == "TROCA")
            {
                ColTpItem.Items.Clear();
                ColTpItem.Items.Add("S");
                ColTpItem.Items.Add("E");
                ColTpItem.Items.Add("N");
                ColTpItem.ToolTipText = "S/E/N";
            }
            else
            {
                ColTpItem.Items.Clear();
                ColTpItem.Items.Add("S");
                ColTpItem.Items.Add("N");
                ColTpItem.ToolTipText = "S/N";
            }
        }

        private void MostraUltPedFat() //Mostrando o Ult.Pedido faturado
        {
            SqlDataReader TabUltPed;
            TabUltPed = Controle.ConsultaSQL("SELECT TOP 1 * FROM AUDITORIA WHERE OPERACAO=6 AND OPCAO='Pedido de Venda' AND ID_USUARIO=" + FrmPrincipal.Perfil_Usuario.IdUsuario.ToString() + "  ORDER BY ID_LANC DESC");
            if (TabUltPed.Read())
            {
                LblUltPedFat.Text = TabUltPed["Documento"].ToString();
            }
        }

        private void PopularCampos(int Isn)
        {
            LblVlrUltOrc.Text    = "";
            LblDtUltOrc.Text     = "";
            LblVendUltOrc.Text   = "";
            LblFilialUltOrc.Text = "";

            if (Paginas.SelectedIndex == 0)
                Paginas.SelectTab(1);

            BoxPesquisa.Enabled = true;
            Vendas.LerDados(Isn);
            TxtCodigo.Text           = Vendas.IdVenda.ToString();

            if (Vendas.IdVenda == 0)
                Vendas.Data = FrmPrincipal.DtHrServidor();

            TxtData.Value            = Vendas.Data;
            LstFilial.SelectedValue  = Vendas.IdFilial.ToString();
            LstEntrega.SelectedValue = Vendas.IdFilialEntrega.ToString();
            TxtNumDocumento.Text     = Vendas.NumDocumento;
            if (Vendas.IdVendedor == 0)
            {
                if (FrmPrincipal.Perfil_Usuario.IdVendedor == 0)
                    LstVendedor.SelectedValue = 0;
                else
                    LstVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor;
            }
            else
                LstVendedor.SelectedValue = Vendas.IdVendedor.ToString();

            LstTipoVenda.SelectedValue = TipoMov;
            LstFormaPgto.SelectedValue = Vendas.IdFormaPgto.ToString();
            TxtVlrSubTotal.Value       = Vendas.VlrSubTotal;
            TxtVlrDesconto.Value       = Vendas.VlrDesconto;
            TxtVlrCredito.Value        = Vendas.VlrCredito;
            TxtVlrTotal.Value          = Vendas.VlrTotal;            
            TxtObservacao.Text         = Vendas.Observacao;
            TxtCnpj.Text               = Vendas.CnpjCpf;            
            TxtNmPessoa.Text           = Vendas.NmPessoa;
            TxtInscUF.Text             = Vendas.InscUF;
            TxtCep.Text                = Vendas.Cep;
            TxtEndereco.Text           = Vendas.Endereco;            
            TxtNumero.Text             = Vendas.Numero;
            TxtComplemento.Text        = Vendas.Complemento;
            TxtCidade.Text             = Vendas.Cidade;
            TxtBairro.Text             = Vendas.Bairro;
            LstPais.SelectedValue      = Vendas.Pais;
            TxtFone.Text               = Vendas.Fone;
            TxtPrazoPgto.Text          = Vendas.PrazoPgto;
            LstUF.SelectedValue        = Vendas.IdUF.ToString();
            TxtVinculoVd.Text          = Vendas.VinculoVd.Trim();
            TxtIdVdTroca.Text          = Vendas.IdVdTroca.ToString();
            Chk_ImpNF.Checked          = Vendas.ImpNF == 1;
            Cb_VdBalcao.Checked        = Vendas.VdBalcao == 1;
            Ck_SemMovEst.Checked       = Vendas.SemMovEst == 1;
            TxtVdDestino.Text          = Vendas.IdVdDestino.ToString();
            TxtMostraObs.Text          = Vendas.ObsCancelamento;
            TxtNumPedido.Text          = Vendas.NumPedido;
            SetaPessoa(Vendas.IdPessoa);
            ParamFilial.LerDados(Vendas.IdFilial);            
            Hab_Botoes();            
        }

        private void InformarPromocao()
        {
            string filtro="";
            PopularPromocao();

            if (CadPessoa.Clie_Forn == 0 || CadPessoa.Clie_Forn == 2)
                filtro = " AND T1.Distribuidor=2";
            else if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 4 || CadPessoa.Clie_Forn == 6)
                filtro = " AND T1.Distribuidor IN (0,1)";

            string LstProdutos = "";
            if (Vendas.IdPessoa > 0 && Vendas.IdPessoa != FrmPrincipal.Parametros_Filial.IdConsumidor && GridPromocao.Rows.Count > 0)
            {
                string sSQL = "select t2.Referencia,t2.Descricao FROM Promocoes T1 " +
                              "  left join Produtos t2 on (t2.Id_Produto=t1.Id_Produto)" +
                              "  WHERE convert(DateTime,convert(char,'" + FrmPrincipal.DtHrServidor().ToShortDateString() + "',103),103) >= T1.DTINICIO " +
                              "    AND convert(DateTime,convert(char,'" + FrmPrincipal.DtHrServidor().ToShortDateString() + "',103),103) <= T1.DTFINAL " +
                              "    AND NOT EXISTS (SELECT * FROM MvVendaItens T3 " +
                              "                      LEFT JOIN MvVenda T4 ON (T4.Id_Venda=T3.Id_Venda)" +
                              "                    WHERE T4.Data >= T1.DtInicio " +
                              "                      AND T4.Data <= T1.DtFinal " +
                              "                      AND T3.Id_Produto=T1.ID_PRODUTO" +
                              "                      AND T4.TpVenda IN ('PV','OE') " +
                              "                      AND T4.Id_Pessoa=" + Vendas.IdPessoa.ToString() +
                              "                      AND T3.TipoItem='S' AND T4.Status IN (1,2,3)) " + filtro;
                              

                SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
                if (Tab.HasRows)
                {
                    while (Tab.Read())
                        LstProdutos = LstProdutos + "Produto: " + Tab["Referencia"].ToString().Trim() + " - " + Tab["Descricao"].ToString().Trim() + "\n";
                }

                if (LstProdutos!="")
                   MessageBox.Show("Produto (Promoção/Destaque):" + "\n" + LstProdutos, "Atenção: Oferecer esses produtos ao cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            if (TipoMov == "PI")
            {
                MessageBox.Show("Não pode fazer pedido nesse tipo de movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
            {
                MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            NovoReg = true;
            Vendas.LerDados(0);
            LimpaDados();
            PopularCampos(0);

            if (Paginas.SelectedIndex == 0)
                Paginas.SelectTab(1);
            NovoReg       = false;            
            StaFormEdicao = true;
            LstVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();
            LstFilial.SelectedValue   = FrmPrincipal.Perfil_Usuario.IdFilial;
            LstEntrega.SelectedValue  = FrmPrincipal.IdFilialConexao;

            Chk_ImpNF.Checked   = TipoMov == "PV" || TipoMov == "VF" || TipoMov == "PC";
            Cb_VdBalcao.Checked = FrmPrincipal.Perfil_Usuario.VendedorBalcao == 1;

            PopularGridItens();
            FrmPrincipal.ControleBotoes(true);
            TxtNumDocumento.Focus();
            SetaPessoa(FrmPrincipal.Parametros_Filial.IdConsumidor);
            PopularEndereco(FrmPrincipal.Parametros_Filial.IdConsumidor);            
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
                if (TipoMov == "PI")
                {
                    MessageBox.Show("Não pode alterar itens nesse tipo de movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));

                if (Vendas.Status == 1)
                    MessageBox.Show("Movimento já Confirmado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Vendas.Status == 2)
                    MessageBox.Show("Movimento já Faturado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Vendas.Status == 3)
                    MessageBox.Show("Movimento já Entregue", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (Vendas.Status == 0 || Vendas.Status == 4)
                {
                    if (!ValidadeMovimento())
                        return;

                    PopularGridItens();
                    if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
                    {
                        MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    TxtObservacao.Focus();
                    Hab_Botoes();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (!VerificarStatus())
                return;

            if (int.Parse(LstEntrega.SelectedValue.ToString()) == 0 && !FrmPrincipal.VersaoDistribuidor)
            {
                MessageBox.Show("Favor informar o Local de Entrega", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
            {
                MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (LstVendedor.SelectedValue.ToString()!="0")
            {
                Vendas.IdVenda         = int.Parse(TxtCodigo.Text);
                Vendas.Data            = TxtData.Value;                
                Vendas.TpVenda         = LstTipoVenda.SelectedValue.ToString();
                Vendas.IdVendedor      = int.Parse(LstVendedor.SelectedValue.ToString());
                Vendas.IdFormaPgto     = int.Parse(LstFormaPgto.SelectedValue.ToString());
                Vendas.VlrSubTotal     = TxtVlrSubTotal.Value;
                Vendas.VlrCredito      = TxtVlrCredito.Value;
                Vendas.VlrDesconto     = TxtVlrDesconto.Value;
                Vendas.VlrTotal        = TxtVlrTotal.Value;
                Vendas.IdUltUsuario    = FrmPrincipal.Perfil_Usuario.IdUsuario;
                Vendas.Observacao      = TxtObservacao.Text;
                Vendas.CnpjCpf         = TxtCnpj.Text;
                Vendas.NmPessoa        = TxtNmPessoa.Text;
                Vendas.InscUF          = TxtInscUF.Text;
                Vendas.Cep             = TxtCep.Text.Replace("-", ""); ;
                Vendas.Endereco        = TxtEndereco.Text;
                Vendas.Pais            = LstPais.SelectedValue.ToString();
                Vendas.Numero          = TxtNumero.Text;
                Vendas.Complemento     = TxtComplemento.Text;
                Vendas.Cidade          = TxtCidade.Text;
                Vendas.Bairro          = TxtBairro.Text;
                Vendas.PrazoPgto       = TxtPrazoPgto.Text;
                Vendas.IdFilial        = int.Parse(LstFilial.SelectedValue.ToString());
                Vendas.IdFilialEntrega = int.Parse(LstEntrega.SelectedValue.ToString());
                Vendas.Fone            = TxtFone.Text;
                Vendas.IdUF            = int.Parse(LstUF.SelectedValue.ToString());
                Vendas.IdVdTroca       = int.Parse(TxtIdVdTroca.Text);
                Vendas.NumPedido       = TxtNumPedido.Text;
                if (Chk_ImpNF.Checked)    Vendas.ImpNF = 1;     else Vendas.ImpNF = 0;
                if (Cb_VdBalcao.Checked)  Vendas.VdBalcao = 1;  else Vendas.VdBalcao = 0;
                if (Ck_SemMovEst.Checked) Vendas.SemMovEst = 1; else Vendas.SemMovEst = 0;
                //                
                Vendas.IdUsuario = FrmPrincipal.Perfil_Usuario.IdUsuario;
                if (Vendas.IdVenda == 0)
                    Vendas.IdFilialOrigem = FrmPrincipal.IdFilialConexao;

                StaFormEdicao = false;                
                Vendas.GravarDados();

                //Registrando Movimento de Auditoria
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 1, "Novo Ped.:" + Vendas.NmPessoa);
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 2, "Alteração:" + Vendas.NmPessoa);

                PopularGrid();
                PopularCampos(Vendas.IdVenda);                
                PopularGridItens();
                FrmPrincipal.ControleBotoes(false);
                GridItens.Focus();
                InformarPromocao();
                if (Vendas.VlrSubTotal == 0)
                    IncluirItem();
            }
            else
            {
                MessageBox.Show("Favor informar o Vendedor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LstVendedor.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (TipoMov == "PI")
                {
                    MessageBox.Show("Não pode excluir nesse tipo de movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Vendas.Status == 4)
                {
                    MessageBox.Show("Movimento Cancelado, Não pode ser excluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
                {
                    MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Vendas.IdVenda = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Vendas.LerDados(Vendas.IdVenda);
                if (Vendas.Status > 0 && Vendas.Status < 4)
                    MessageBox.Show("Cancele o movimento para pode excluir", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                else
                {
                    if (Vendas.IdUsuario != FrmPrincipal.Perfil_Usuario.IdUsuario)
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
                        Vendas.IdVenda = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        Vendas.Excluir();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 3, "Excluindo");
                        Vendas.LerDados(0);
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

            /*if (e.Control && e.KeyCode == Keys.F)
            {
                if (Paginas.SelectedIndex != 1 || Vendas.IdVenda == 0)
                    return;

                if (Vendas.IdVdOrigem == 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.VendedorBalcao == 0 && Vendas.IdVendedor != FrmPrincipal.Perfil_Usuario.IdVendedor)
                        return;

                    if (Vendas.IdVendedor != FrmPrincipal.Perfil_Usuario.IdVendedor)
                        return;
                }
                                
                if (Vendas.Status == 1 || Vendas.Status == 2)
                {
                    FrmFormaPgtoVd Frm = new FrmFormaPgtoVd();
                    Frm.FrmPrincipal   = FrmPrincipal;
                    Frm.Vendas         = Vendas;
                    Frm.ShowDialog();
                    PopularCampos(Vendas.IdVenda);
                }
            }*/

            if (e.Control && e.KeyCode == Keys.D)
            {
                if (Vendas.IdVenda > 0 && !StaFormEdicao)
                {
                    if (FrmPrincipal.Perfil_Usuario.IgnoraDescVd == 0)
                    {
                        FrmAutorizacao Autorizacao = new FrmAutorizacao();
                        Autorizacao.FrmPrincipal = FrmPrincipal;
                        Autorizacao.ShowDialog();
                        //Verificando se o Acesso foi liberado
                        if (Autorizacao.AcessoOk)
                        {
                            if (Autorizacao.Usuario.IgnoraDescVd == 0)
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }                    
                    }
                    if (Vendas.IgnoraDesc == 0)
                        Vendas.IgnoraDesc = 1;
                    else
                        Vendas.IgnoraDesc = 0;
                    Vendas.GravarDados();
                    //
                    SqlDataReader TabComissao = Controle.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                                     " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + Vendas.IdVenda.ToString());
                    Controles.Comissao CalcComissao = new Controles.Comissao();
                    CalcComissao.Controle = Controle;
                    decimal PDesconto = 0;
                    if (Vendas.IgnoraDesc == 0)
                    {
                        if ((Vendas.VlrDesconto + Vendas.VlrCredito) > 0)
                            PDesconto = 100 / (Vendas.VlrSubTotal / (Vendas.VlrDesconto + Vendas.VlrCredito));
                    }
                    if (Vendas.Data.Date.Year >= 2019)
                        CalcComissao.CalcularMovimento2019(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                    else
                        CalcComissao.CalcularMovimento(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                    PopularGridItens();
                }
            }

            if (e.Control && e.KeyCode == Keys.R) //Retira o Item da Rentabilidade
            {
                if (GridItens.CurrentRow != null)
                {
                    if (Vendas.IdVenda > 0 && !StaFormEdicao)
                    {
                        if (FrmPrincipal.Perfil_Usuario.IgnoraDescVd == 0)
                        {
                            FrmAutorizacao Autorizacao = new FrmAutorizacao();
                            Autorizacao.FrmPrincipal = FrmPrincipal;
                            Autorizacao.ShowDialog();
                            //Verificando se o Acesso foi liberado
                            if (Autorizacao.AcessoOk)
                            {
                                if (Autorizacao.Usuario.IgnoraDescVd == 0)
                                {
                                    MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                        }
                        ItemMvVendas.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));                        
                        if (ItemMvVendas.NaoRentab== 0)
                            ItemMvVendas.NaoRentab = 1;
                        else
                            ItemMvVendas.NaoRentab = 0;
                        ItemMvVendas.GravarDados();
                        FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 9, "Registro de Alteração na Rentabilidade: " + ItemMvVendas.NaoRentab.ToString());
                        PopularGridItens();
                        GridItens.CurrentCell = GridItens.CurrentRow.Cells[6];                
                    }
                }

            }

        }
        private void LimpaDados()
        {            
            TxtCodigo.Text             = "0";
            TxtData.Value              = FrmPrincipal.DtHrServidor();
            TxtNumDocumento.Text       = "";                                    
            LstVendedor.SelectedValue  = 0;
            LstFormaPgto.SelectedValue = 0;
            TxtVlrSubTotal.Value       = 0;
            TxtVlrDesconto.Value       = 0;
            TxtVlrDesconto.Value       = 0; ;
            TxtVlrTotal.Value          = 0;            
            TxtObservacao.Text         = "";
            TxtVinculoVd.Text          = "";
            TxtCodCliente.Text         = "0";
            TxtCliente.Text            = "";
            TxtPrazoPgto.Text          = "";
            TxtIdVdTroca.Text          = "0";
            Cb_VdBalcao.Checked        = false;
            Ck_SemMovEst.Checked       = false;
            LblPrcSensacional.Text     = "P.S: ";
            LblPrcEspecial.Text        = "P.E: ";
            LblPrcVarejo.Text          = "P.V: ";
            LblPrcMinimo.Text          = "P.M: ";
            LblPrcDist.Text            = "P.D: ";
            LblPrcEspDist.Text         = "P.E.D: ";
            LblCusto.Text              = "";            
            TxtNumPedido.Text          = "";
            SetaPessoa(0);
            Vendas.LerDados(0);            
            PopularEndereco(0);
            PopularGridItens();            
            Hab_Botoes();
        }
        private void Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           /* if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[0].Value.ToString() != "")
                    BtnEditar_Click(FrmPrincipal.BtnEditar, null);
            }*/

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
                    if (NovoReg)
                        PopularCampos(0);
                    else
                        PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));                    
                    PagCab.SelectTab(0);
                    PagItens.SelectTab(0);
                    PopularGridItens();
                    PopularPromocao();
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
            FrmPrincipal.ClickBtnNovo     += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar   += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar   += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir  += new EventHandler(this.BtnExcluir_Click);
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
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T1.TIPOITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T1.P_COMISSAO,ISNULL(T1.MARGEMNEGOCIO,0) AS MARGEMNEGOCIO,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado, " +
                                               " T2.IcmsIss,T2.Reducao, CASE T2.SITTRIBUTARIA WHEN 0 THEN 'T' WHEN 1 THEN 'N' WHEN 2 THEN 'I' WHEN 3 THEN 'S' END AS ST,  T1.VlrComissao, T1.Id_Produto, ISNULL(T1.ID_PROMOCAO,0) AS ID_PROMOCAO,ISNULL(T1.NAORENTAB,0) AS NAORENTAB,ISNULL(T1.ITEMPED,0) AS ITEMPED, ISNULL(T1.PCOMPROMOCAO,0) as PCOMPROMOCAO, ISNULL(T2.ID_GRUPO,0) AS ID_GRUPO, T1.PRCSENSACIONAL, T1.PRCCUSTO,IsNUll(T1.PRCESPDIST,0) as PRCESPDIST" +
                                               " FROM MvVendaItens T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString()+" order by T1.ID_ITEM");

            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource    = Source_Itens;
            Navegador.BindingSource = Source_Itens;

            int item = Source_Itens.Find("ID_Item", ItemMvVendas.IdItem);
            Source_Itens.Position = item;
            Hab_Botoes();

            //Definindo a Chave Primaria
            DataColumn[] key = new DataColumn[1];
            key[0] = TabItens.Tables[0].Columns["ID_ITEM"];
            TabItens.Tables[0].PrimaryKey = key;            
            //Atualizando o Total do Movimento                                    
            TotalMovimento();
            CalcularComissaoPedido();
            MostraRentabilidade();
            GridItens.Refresh();
        }
        private void TotalMovimento()
        {            
            decimal VlrSubTotal = 0;
            decimal VlrTotalPro = 0;
            for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
            {
                if (TabItens.Tables[0].Rows[I].RowState == DataRowState.Deleted)
                    TabItens.Tables[0].Rows.RemoveAt(I);
                else
                {
                    if (TabItens.Tables[0].Rows[I]["TIPOITEM"].ToString() == "E")
                        VlrSubTotal = VlrSubTotal - decimal.Parse(TabItens.Tables[0].Rows[I]["VLRTOTAL"].ToString());
                    else
                        VlrSubTotal = VlrSubTotal + decimal.Parse(TabItens.Tables[0].Rows[I]["VLRTOTAL"].ToString());

                    if (int.Parse(TabItens.Tables[0].Rows[I]["ID_PROMOCAO"].ToString()) > 0)
                        VlrTotalPro = VlrTotalPro + decimal.Parse(TabItens.Tables[0].Rows[I]["VLRTOTAL"].ToString());
                }
            }
            LbTotalPromocao.Text = "T.P: " + string.Format("{0:N2}", VlrTotalPro);
            Vendas.VlrSubTotal   = VlrSubTotal;
            Vendas.VlrTotal      = VlrSubTotal - (Vendas.VlrDesconto + Vendas.VlrCredito);
            TxtVlrSubTotal.Value = VlrSubTotal;
            TxtVlrTotal.Value    = VlrSubTotal - (Vendas.VlrDesconto + Vendas.VlrCredito);

            if (Vendas.IdVenda > 0)
                Vendas.GravarDados();
        }

        private void MostraRentabilidade()
        {
            GridItens.Refresh();
            decimal VlrComissao = 0;
            decimal TotalVd     = 0;

            for (int I = 0; I <= GridItens.RowCount - 1; I++)
            {
                if (GridItens.Rows[I].Cells[1].Value.ToString() != "E")
                    VlrComissao = VlrComissao + decimal.Parse(GridItens.Rows[I].Cells[16].Value.ToString());
            }

            LblMCom.Text = "0,00";
            TotalVd      = Vendas.VlrTotal;

            if (Vendas.IgnoraDesc == 1)
                TotalVd = Vendas.VlrSubTotal;

            if (TotalVd > 0)
            {
                LblMCom.Text = string.Format("{0:N2}", (VlrComissao / TotalVd) * 100) + "%";
                LblMCom.ForeColor = System.Drawing.Color.Teal;

                if ((VlrComissao / TotalVd) * 100 < decimal.Parse("3,00"))
                    LblMCom.ForeColor = System.Drawing.Color.Red;

                if (Vendas.IgnoraDesc == 1)
                    LblMCom.ForeColor = System.Drawing.Color.Blue;
            }
        }


        private void MostraUltCompra()
        {
            /*LblVlrUltCompra.Text    = "0,00";
            LblDtUltCompra.Text     = "";
            LblVendUltCompra.Text   = "";
            LblFilialUltCompra.Text = "";

           
            if (CadPessoa.IdPessoa == 0 && CadPessoa.IdPessoa == FrmPrincipal.Parametros_Filial.IdConsumidor)
                return;
            if (CadPessoa.Cnpj == "00000000000000" || CadPessoa.Cnpj == "11111111111111" || CadPessoa.Cnpj == "22222222222222" || CadPessoa.Cnpj == "33333333333333" || CadPessoa.Cnpj == "44444444444444"
             || CadPessoa.Cnpj == "55555555555555" || CadPessoa.Cnpj == "66666666666666" || CadPessoa.Cnpj == "77777777777777" || CadPessoa.Cnpj == "88888888888888" || CadPessoa.Cnpj == "99999999999999")
                return;
            if (CadPessoa.Cnpj == "00000000000" || CadPessoa.Cnpj == "11111111111" || CadPessoa.Cnpj == "22222222222" || CadPessoa.Cnpj == "33333333333" || CadPessoa.Cnpj == "44444444444"
             || CadPessoa.Cnpj == "55555555555" || CadPessoa.Cnpj == "66666666666" || CadPessoa.Cnpj == "77777777777" || CadPessoa.Cnpj == "88888888888" || CadPessoa.Cnpj == "99999999999")
                return;

            string CnpjCpf = CadPessoa.Cnpj;
            DataSet Venda = new DataSet();
            DataTable Resultado = new DataTable();
            Resultado.Columns.Add("Filial",   Type.GetType("System.String"));
            Resultado.Columns.Add("IdVenda",  Type.GetType("System.Int32"));
            Resultado.Columns.Add("Data",     Type.GetType("System.DateTime"));
            Resultado.Columns.Add("Valor",    Type.GetType("System.Decimal"));
            Resultado.Columns.Add("Vendedor", Type.GetType("System.String"));
            Resultado.DefaultView.Sort = "Data desc";
            Controles.UltimaCompraCliente.UltCompraCliente RegVenda = new Controles.UltimaCompraCliente.UltCompraCliente();
            XmlNode Retorno;
            try
            {
                SqlDataReader LerSQL = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL ORDER BY FILIAL ");                
                while (LerSQL.Read())
                {
                    if (LerSQL["ServidorRemoto"].ToString().Trim() != "")
                    {
                        try
                        {                            
                            RegVenda.Url = "http://" + LerSQL["ServidorRemoto"].ToString().Trim() + "/ERP-SGE_WebService/UltCompraCliente.asmx?WSDL";
                            Retorno = RegVenda.UltimaCompra(CnpjCpf);
                        
                            if (Retorno != null)
                            {
                                Venda = new DataSet();
                                XmlNodeReader XmlVenda = new XmlNodeReader(Retorno);
                                Venda.ReadXml(XmlVenda);

                                for (int I = 0; I <= Venda.Tables[0].Rows.Count - 1; I++)
                                    Resultado.Rows.Add(LerSQL["Fantasia"].ToString().Trim(), int.Parse(Venda.Tables[0].Rows[I]["Id_Venda"].ToString()), DateTime.Parse(Venda.Tables[0].Rows[I]["Data"].ToString()), decimal.Parse(Venda.Tables[0].Rows[I]["VlrTotal"].ToString().Replace(".", ",")), Venda.Tables[0].Rows[I]["Vendedor"].ToString());                                
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                if (Resultado.Rows.Count > 0)
                {
                    DataSet Tabela   = new DataSet();
                    DataView TabSort = Resultado.DefaultView;
                    TabSort.Sort     = "Data Desc";
                    Tabela.Tables.Add(TabSort.ToTable());

                    LblFilialUltCompra.Text = Tabela.Tables[0].Rows[0]["Filial"].ToString();
                    LblVendUltCompra.Text   = Tabela.Tables[0].Rows[0]["Vendedor"].ToString();
                    LblDtUltCompra.Text     = Tabela.Tables[0].Rows[0]["Data"].ToString();
                    LblVlrUltCompra.Text    = string.Format("{0:N2}", decimal.Parse(Tabela.Tables[0].Rows[0]["Valor"].ToString()));
                }                
            }
            catch
            {
                
            }*/
        }

        private void MostraUltOrcamento()
        {
            
            LblVlrUltOrc.Text    = "";
            LblDtUltOrc.Text     = "";
            LblVendUltOrc.Text   = "";
            LblFilialUltOrc.Text = "";
            BoxUltOrc.Text       = "Orçamento";
            bool CliNovo = true;
            bool CliReat = true;
            if (TipoMov != "OC")
                return;

            if (CadPessoa.IdPessoa == 0 && CadPessoa.IdPessoa == FrmPrincipal.Parametros_Filial.IdConsumidor)
                return;
            if (CadPessoa.IdServidor == 0)
            {
                if (CadPessoa.Cnpj == "00000000000000" || CadPessoa.Cnpj == "11111111111111" || CadPessoa.Cnpj == "22222222222222" || CadPessoa.Cnpj == "33333333333333" || CadPessoa.Cnpj == "44444444444444"
                 || CadPessoa.Cnpj == "55555555555555" || CadPessoa.Cnpj == "66666666666666" || CadPessoa.Cnpj == "77777777777777" || CadPessoa.Cnpj == "88888888888888" || CadPessoa.Cnpj == "99999999999999")
                    return;
                if (CadPessoa.Cnpj == "00000000000"   || CadPessoa.Cnpj == "11111111111"     || CadPessoa.Cnpj == "22222222222"    || CadPessoa.Cnpj == "33333333333"    || CadPessoa.Cnpj == "44444444444"
                 || CadPessoa.Cnpj == "55555555555"   || CadPessoa.Cnpj == "66666666666"     || CadPessoa.Cnpj == "77777777777"    || CadPessoa.Cnpj == "88888888888"    || CadPessoa.Cnpj == "99999999999")
                    return;
            }
            string CnpjCpf = CadPessoa.Cnpj;
            DataSet Venda = new DataSet();
            DataTable Resultado = new DataTable();
            Resultado.Columns.Add("Filial",   Type.GetType("System.String"));
            Resultado.Columns.Add("IdVenda",  Type.GetType("System.Int32"));
            Resultado.Columns.Add("Data",     Type.GetType("System.DateTime"));
            Resultado.Columns.Add("Valor",    Type.GetType("System.Decimal"));
            Resultado.Columns.Add("Vendedor", Type.GetType("System.String"));
            Resultado.Columns.Add("NOVO", Type.GetType("System.Int32"));
            Resultado.Columns.Add("REATIV", Type.GetType("System.Int32"));
            
            Resultado.DefaultView.Sort = "Data desc";
            Controles.UltimaCompraCliente.UltCompraCliente RegVenda = new Controles.UltimaCompraCliente.UltCompraCliente();
            XmlNode Retorno;
            try
            {
                
                SqlDataReader LerSQL = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL ORDER BY FILIAL ");                
                while (LerSQL.Read())
                {
                    if (LerSQL["ServidorRemoto"].ToString().Trim() != "")
                    {
                        try
                        {
                            if (int.Parse(LerSQL["ID_Filial"].ToString()) == FrmPrincipal.IdFilialConexao && !FrmPrincipal.VersaoDistribuidor)
                                RegVenda.Url = "http://SERVIDOR/ERP-SGE_WebService/UltCompraCliente.asmx?WSDL";
                            else
                                RegVenda.Url = "http://" + LerSQL["ServidorRemoto"].ToString().Trim() + LerSQL["Porta"].ToString() + "/ERP-SGE_WebService/UltCompraCliente.asmx?WSDL";

                            Retorno = RegVenda.UltimoOrcamento(CnpjCpf,CadPessoa.IdServidor);
                        
                            if (Retorno != null)
                            {
                                Venda = new DataSet();
                                XmlNodeReader XmlVenda = new XmlNodeReader(Retorno);
                                Venda.ReadXml(XmlVenda);

                                for (int I = 0; I <= Venda.Tables[0].Rows.Count - 1; I++)
                                {
                                    if (int.Parse(Venda.Tables[0].Rows[I]["NovoCli"].ToString()) == 0)
                                        CliNovo = false;
                                    if (int.Parse(Venda.Tables[0].Rows[I]["Reativ"].ToString()) == 0)
                                        CliReat = false;
                                    Resultado.Rows.Add(LerSQL["Fantasia"].ToString().Trim(), int.Parse(Venda.Tables[0].Rows[I]["Id_Venda"].ToString()), DateTime.Parse(Venda.Tables[0].Rows[I]["DTHR"].ToString()), decimal.Parse(Venda.Tables[0].Rows[I]["VlrTotal"].ToString().Replace(".", ",")), Venda.Tables[0].Rows[I]["Vendedor"].ToString(), int.Parse(Venda.Tables[0].Rows[I]["NovoCli"].ToString()), int.Parse(Venda.Tables[0].Rows[I]["Reativ"].ToString()));
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                if (Resultado.Rows.Count > 0)
                {
                    DataSet Tabela   = new DataSet();
                    DataView TabSort = Resultado.DefaultView;
                    TabSort.Sort     = "Data Desc";
                    Tabela.Tables.Add(TabSort.ToTable());
                    BoxUltOrc.Text = "Orçamento";
                    if (CliNovo)
                        BoxUltOrc.Text = "Orçamento(Novo)";
                    else
                    {
                        if (CliReat)
                            BoxUltOrc.Text = "Orçamento(Reativação)";
                    }


                    LblFilialUltOrc.Text = Tabela.Tables[0].Rows[0]["Filial"].ToString();
                    LblVendUltOrc.Text   = Tabela.Tables[0].Rows[0]["Vendedor"].ToString();
                    LblDtUltOrc.Text     = Tabela.Tables[0].Rows[0]["Data"].ToString();
                    LblVlrUltOrc.Text    = string.Format("{0:N2}", decimal.Parse(Tabela.Tables[0].Rows[0]["Valor"].ToString()));
                }                
            }
            catch
            {
                
            }
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
            if (TipoMov == "PI")
            {
                MessageBox.Show("Não pode alterar itens nesse tipo de movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
                return;
            }
            if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
            {
                MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
                return;
            }
            if (StaFormEdicao)
            {
                MessageBox.Show("Favor gravar o movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                /*if (Vendas.Status == 3 && e.ColumnIndex == 8)
                {
                    e.Cancel = true;
                    return;
                }*/
                if (!VerificarStatus())
                {
                    if (e.ColumnIndex != 8)
                    {
                        Source_Itens.CancelEdit();
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (Vendas.Status > 0 && Vendas.Status < 4)
                    {
                        MessageBox.Show("Movimento já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Source_Itens.CancelEdit();
                        e.Cancel = true;
                    }
                    if (!ValidadeMovimento())
                    {
                        Source_Itens.CancelEdit();
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal PrcAtacado     = 0;
            decimal PrcVarejo      = 0;
            decimal PrcMinimo      = 0;
            decimal PrcEspecial    = 0;
            decimal PrcSensacional = 0;
            decimal PrcEspDist     = 0;

            if (Vendas.Status == 3 && e.ColumnIndex == 8)
            {
                Controle.ExecutaSQL("UPDATE MVVENDAITENS SET MARGEMNEGOCIO=" + GridItens.CurrentRow.Cells[8].Value.ToString() + " WHERE ID_ITEM=" + GridItens.CurrentRow.Cells[0].Value.ToString());                
                return;
            }
            if (!VerificarStatus())
            {   
                Source_Itens.CancelEdit();
                PopularGridItens();
                return;
            }       
            if (Vendas.IdVenda > 0 && !StaFormEdicao)
            {                
                Produtos CadProd = new Produtos();
                CadProd.Controle = Controle;
                decimal Qtde     = decimal.Parse(GridItens.CurrentRow.Cells[4].Value.ToString());
                decimal VlrUnt   = Math.Round(decimal.Parse(GridItens.CurrentRow.Cells[5].Value.ToString()),2);                
                               
                ItemMvVendas.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                if (ItemMvVendas.Vinculado > 0)
                {
                    MessageBox.Show("Atenção: esse item foi vinculado não pode ser modificado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                    return;
                }

                if (ItemMvVendas.PromQtdeItem == 1)
                {
                    MessageBox.Show("Atenção: Produto não pode ser alterado, esta vinculado a uma Promoção", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                    return;
                }
                ItemMvVendas.IdItem   = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                ItemMvVendas.TipoItem = GridItens.CurrentRow.Cells[1].Value.ToString();  
                CadProd.LerDados(ItemMvVendas.IdProduto);
                                
                PrcMinimo      = CadProd.PrcMinimo;
                PrcVarejo      = CadProd.PrcVarejo;
                PrcAtacado     = CadProd.PrcAtacado;
                PrcEspecial    = CadProd.PrcEspecial;
                PrcSensacional = CadProd.PrcSensacional;
                PrcEspDist     = CadProd.PrcEspDist;

                /*if (int.Parse(LstEntrega.SelectedValue.ToString()) != FrmPrincipal.IdFilialConexao && Vendas.TpVenda != "TROCA")
                {
                    ItemMvVendas.TipoItem = "N";
                    GridItens.CurrentRow.Cells[1].Value = "N";
                } */

                if (ItemMvVendas.IdPromocao > 0 && VlrUnt == ItemMvVendas.PrcVarejo)
                {
                    PrcMinimo      = ItemMvVendas.PrcMinimo;
                    PrcVarejo      = ItemMvVendas.PrcVarejo;
                    PrcAtacado     = ItemMvVendas.PrcAtacado;
                    PrcEspecial    = ItemMvVendas.PrcEspecial;
                    PrcSensacional = ItemMvVendas.PrcSensacional;
                    PrcEspDist     = ItemMvVendas.PrcAtacado;
                }
                else
                {
                    ItemMvVendas.IdPromocao   = 0;
                    ItemMvVendas.PComPromocao = 0;
                }
                                                
                if (TipoMov == "EMVF")
                {
                    if (SaldoEstoqueCliente(ItemMvVendas.IdProduto, Vendas.IdPessoa) < Qtde)
                    {
                        MessageBox.Show("Quantidade maior que o saldo do cliente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Source_Itens.CancelEdit();
                        PopularGridItens();
                        GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                        return;
                    }
                }
                if ((CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6) && CadPessoa.NaoVerifQtdeCx == 0 && CadProd.QtdeCxDist == 1 && CadProd.QtdeCaixa > 0 && (TipoMov == "VF" || TipoMov == "PV" || TipoMov == "OE" || TipoMov == "PC"))
                {
                    if (Qtde < CadProd.QtdeCaixa)
                    {
                        MessageBox.Show("Menor quantidade de venda do produto é: " + CadProd.QtdeCaixa.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Qtde = CadProd.QtdeCaixa;
                    }
                    else
                    {
                        if ((Qtde % CadProd.QtdeCaixa) > 0)
                        {
                            MessageBox.Show("Favor Verificar a Quantidade do produto", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Source_Itens.CancelEdit();
                            return;
                        }
                    }
                }
                else
                {
                    if (CadProd.QtdeCaixa > 0)
                    {
                        if (Qtde < CadProd.QtdeCaixa && CadProd.QtdeCxDist == 0)
                        {
                            MessageBox.Show("Menor quantidade de venda do produto é: " + CadProd.QtdeCaixa.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Qtde = CadProd.QtdeCaixa;
                        }
                        else
                        {
                            if (CadProd.QtdeCaixa > 0 && CadProd.QtdeCxDist == 0)
                            {
                                if ((Qtde % CadProd.QtdeCaixa) > 0)
                                {
                                    MessageBox.Show("Favor informar a quantidade sendo multiplos de: " + CadProd.QtdeCaixa.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Source_Itens.CancelEdit();
                                    return;
                                }
                            }
                        }
                    }
                }

                              
                if ((e.ColumnIndex==4 && TabAux.Estoque == 2 && ParamFilial.EstoqueZero == 1 && ItemMvVendas.TipoItem == "S") || (Vendas.IdFilialEntrega!=FrmPrincipal.IdFilialConexao && ItemMvVendas.TipoItem=="N"))
                {
                    decimal SaldoPrd = 0;                    
                    decimal SaldoDeposito=0;
                    string msg = "";
                    if (Vendas.IdFilialEntrega != FrmPrincipal.IdFilialConexao)
                    {
                        SaldoPrd = BuscaSldFilialEntrega(Vendas.IdFilialEntrega, CadProd.Referencia);
                        msg = "Filial de Entrega \n";
                    }
                    else
                        SaldoPrd = SaldoProduto(CadProd.IdProduto);


                    
                    if (SaldoPrd < Qtde && (SaldoPrd + SaldoDeposito >= Qtde))
                    {
                        if (MessageBox.Show("Quantidade Dispónivel no Depósito, deseja fazer a liberação automatica ? ", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            SetaLiberacaoProduto(Vendas.IdFilialEntrega, 0, Vendas.IdVenda, CadProd.Referencia, SaldoPrd);
                        else
                            SaldoDeposito = 0;
                    }

                    if (SaldoPrd < Qtde)
                    {
                        if (SaldoPrd == -999999)
                            MessageBox.Show("Atenção: Erro ao verificar o saldo na Filial de Entrega", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (Vendas.IdFilialEntrega != FrmPrincipal.IdFilialConexao)
                            MessageBox.Show(msg + "Produto não tem saldo Suficiente no estoque na Filial de Entrega ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (!Vendas.ProdutoLiberado(CadProd.IdProduto) && VerGrupo_Estoque(Controle,CadProd.Referencia))
                        {
                            if (FrmPrincipal.Perfil_Usuario.LiberaEstoque == 0)
                            {
                                MessageBox.Show(msg + "Produto não tem saldo Suficiente no estoque, Favor Solicitar liberação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Source_Itens.CancelEdit();
                                PopularGridItens();
                                GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                                return;
                            }
                            else
                            {
                                MessageBox.Show(msg + "Produto não tem saldo Suficiente no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               // Source_Itens.CancelEdit();
                               // PopularGridItens();
                               // GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                               // return;
                            }
                        }
                    }                    
                }

                if (TabAux.VerPrcMim == 1 && e.ColumnIndex == 5 && Vendas.TpVenda != "EMVF")
                {
                    if (VlrUnt < PrcAtacado && CadPessoa.LiberaPrc == 0)
                    {
                        if (CadProd.IdGrupo == 53 && !FrmPrincipal.VersaoDistribuidor)
                        {
                            MessageBox.Show("Preço Unitário menor que o Preço de Distribuidor: " + string.Format("{0:N2}", CadProd.PrcAtacado), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            if (FrmPrincipal.Perfil_Usuario.LiberaPrcCusto == 0)
                            {
                                FrmAutorizacao Autorizacao = new FrmAutorizacao();
                                Autorizacao.FrmPrincipal = FrmPrincipal;
                                Autorizacao.ShowDialog();
                                //Verificando se o Acesso foi liberado
                                if (Autorizacao.AcessoOk)
                                {
                                    if (Autorizacao.Usuario.LiberaPrcCusto == 0)
                                    {
                                        MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 5 || CadPessoa.Clie_Forn == 6)
                                            VlrUnt = PrcAtacado;
                                        else
                                            VlrUnt = PrcSensacional;
                                    }
                                    else
                                    {
                                        FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Liberação de Preço:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + " -> " + VlrUnt.ToString() + " Usuario: " + Autorizacao.Usuario.Usuario);
                                        ItemMvVendas.IdUsuLibPrc = Autorizacao.Usuario.IdUsuario;
                                    }
                                }
                                else
                                {
                                    if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 5 || CadPessoa.Clie_Forn == 6)
                                        VlrUnt = PrcAtacado;
                                    else
                                        VlrUnt = PrcSensacional;
                                }
                            }
                            else
                            {
                                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Liberação de Preço:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + " -> " + VlrUnt.ToString() + " Usuario: " + FrmPrincipal.Perfil_Usuario.Usuario);
                                ItemMvVendas.IdUsuLibPrc = FrmPrincipal.Perfil_Usuario.IdUsuario;
                            }
                        }
                        else
                        {
                            decimal PrcVerCusto = Math.Round(CadProd.UltPrcCompra * (1 + (VerificaDescGrupoPrd(CadProd.IdProduto) / 100)), 2);

                            if (PrcVerCusto == 0)
                                PrcVerCusto = Math.Round(CadProd.UltPrcCompra, 2);

                            if (VlrUnt < PrcVerCusto && FrmPrincipal.Perfil_Usuario.LiberaPrcCusto == 0 )
                            {
                                MessageBox.Show("Preço Unitário menor que o permitido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                FrmAutorizacao Autorizacao = new FrmAutorizacao();
                                Autorizacao.FrmPrincipal = FrmPrincipal;
                                Autorizacao.ShowDialog();
                                //Verificando se o Acesso foi liberado
                                if (Autorizacao.AcessoOk)
                                {
                                    if (Autorizacao.Usuario.LiberaPrcCusto == 0)
                                    {
                                        MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 5 || CadPessoa.Clie_Forn == 6)
                                            VlrUnt = PrcAtacado;
                                        else
                                            VlrUnt = PrcSensacional;
                                    }
                                    else
                                    {
                                        FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Liberação de Preço:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + " -> " + VlrUnt.ToString() + " Usuario: " + Autorizacao.Usuario.Usuario);
                                        ItemMvVendas.IdUsuLibPrc = Autorizacao.Usuario.IdUsuario;
                                    }
                                }
                                else
                                {
                                    if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 5 || CadPessoa.Clie_Forn == 6)
                                        VlrUnt = PrcAtacado;
                                    else
                                        VlrUnt = PrcSensacional;
                                }
                            }
                            else
                            {
                                if (FrmPrincipal.Perfil_Usuario.PrcDistrib == 0)
                                {
                                    MessageBox.Show("Preço Unitário menor que o Preço de Distribuidor: " + string.Format("{0:N2}", CadProd.PrcAtacado), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    FrmAutorizacao Autorizacao = new FrmAutorizacao();
                                    Autorizacao.FrmPrincipal = FrmPrincipal;
                                    Autorizacao.ShowDialog();
                                    //Verificando se o Acesso foi liberado
                                    if (Autorizacao.AcessoOk)
                                    {
                                        if (Autorizacao.Usuario.PrcDistrib == 0)
                                        {
                                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            VlrUnt = PrcSensacional;
                                        }
                                        else
                                        {
                                            FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Liberação de Preço:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + " -> " + VlrUnt.ToString() + " Usuario: " + Autorizacao.Usuario.Usuario);
                                            ItemMvVendas.IdUsuLibPrc = Autorizacao.Usuario.IdUsuario;
                                        }
                                    }
                                    else
                                    {
                                        if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 5 || CadPessoa.Clie_Forn == 6)
                                            VlrUnt = PrcAtacado;
                                        else
                                           VlrUnt = PrcSensacional;
                                    }

                                }
                                else
                                {
                                    FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Liberação Aut. de Preço:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + " -> " + VlrUnt.ToString());
                                    ItemMvVendas.IdUsuLibPrc = FrmPrincipal.Perfil_Usuario.IdUsuario;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (VlrUnt < PrcMinimo && CadPessoa.LiberaPrc == 0 && (CadPessoa.Clie_Forn == 0 || CadPessoa.Clie_Forn == 1 || CadPessoa.Clie_Forn == 2 || CadPessoa.Clie_Forn == 4))
                        {
                            MessageBox.Show("Preço Unitário menor que o Preço de Minimo: " + string.Format("{0:N2}", CadProd.PrcMinimo), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            if (FrmPrincipal.Perfil_Usuario.LiberaPreco == 0)
                            {
                                FrmAutorizacao Autorizacao = new FrmAutorizacao();
                                Autorizacao.FrmPrincipal = FrmPrincipal;
                                Autorizacao.ShowDialog();
                                //Verificando se o Acesso foi liberado
                                if (Autorizacao.AcessoOk)
                                {
                                    if (Autorizacao.Usuario.LiberaPreco == 0)
                                    {
                                        MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        VlrUnt = PrcSensacional;
                                    }
                                    else
                                    {
                                        FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Liberação de Preço:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + " -> " + VlrUnt.ToString() + " Usuario: " + Autorizacao.Usuario.Usuario);
                                        ItemMvVendas.IdUsuLibPrc = Autorizacao.Usuario.IdUsuario;
                                    }
                                }
                                else
                                    VlrUnt = PrcSensacional;
                            }
                            else
                            {
                                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Liberação Aut. de Preço:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + " -> " + VlrUnt.ToString());
                                ItemMvVendas.IdUsuLibPrc = FrmPrincipal.Perfil_Usuario.IdUsuario;
                            }
                        }
                    }
                }

                int ItemPed = int.Parse(GridItens.CurrentRow.Cells[20].Value.ToString());

                ItemMvVendas.Qtde           = Qtde;
                ItemMvVendas.VlrUnitario    = VlrUnt;
                ItemMvVendas.VlrTotal       = VlrUnt * Qtde;
                ItemMvVendas.VlrUntComissao = VlrUnt;
                ItemMvVendas.PrcCusto       = CadProd.UltPrcCompra;
                ItemMvVendas.PrcMinimo      = PrcMinimo;
                ItemMvVendas.PrcVarejo      = PrcVarejo;
                ItemMvVendas.PrcAtacado     = PrcAtacado;
                ItemMvVendas.PrcEspecial    = PrcEspecial;
                ItemMvVendas.PrcSensacional = PrcSensacional;
                ItemMvVendas.TipoItem       = GridItens.CurrentRow.Cells[1].Value.ToString();
                ItemMvVendas.MargemNegocio  = int.Parse(GridItens.CurrentRow.Cells[8].Value.ToString());
                ItemMvVendas.ItemPed        = ItemPed;
                ItemMvVendas.GravarDados();
                               
                //Registrando Movimento de Auditoria
                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 2, "Alterando Item Produto:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + "  Qtde:" + ItemMvVendas.Qtde.ToString());
                PopularGridItens();
                GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];                
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (VerificarStatus())
                    IncluirItem();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (TipoMov == "PI")
            {
                MessageBox.Show("Não pode excluir itens nesse tipo de movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (GridItens.CurrentRow != null)
            {
                if (StaFormEdicao)
                    MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (Vendas.IdVenda > 0 && !StaFormEdicao)
                    {
                        if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
                        {
                            MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (!ValidadeMovimento())
                            return;
                        
                        if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (!VerificarStatus())
                                return;
                                                                                   
                            ItemMvVendas.IdVenda = Vendas.IdVenda;
                            ItemMvVendas.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                            ItemMvVendas.LerDados(ItemMvVendas.IdItem);

                            if (ItemMvVendas.Vinculado > 0)
                            {
                                MessageBox.Show("Atenção: esse item foi vinculado não pode ser excluido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //PopularGridItens();                                    
                                return;
                            }
                            if (ItemMvVendas.PromQtdeItem == 1)
                            {
                                if (MessageBox.Show("Atenção Produto faz parte de um grade promocional, todos os itens da grade será excluido, confirma ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Controle.ExecutaSQL("DELETE FROM MVVENDAITENS WHERE ID_VENDA=" + Vendas.IdVenda.ToString() + " AND ID_PROMOCAO=" + ItemMvVendas.IdPromocao.ToString());
                                    FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 3, "Excluindo Grade de Promoção:" + ItemMvVendas.IdPromocao.ToString());
                                    ItemMvVendas.IdItem = 0;                                                        
                                    PopularGridItens();
                                }
                            }
                            
                            int IdItem = ItemMvVendas.IdItem;
                            ItemMvVendas.Excluir();

                            DataRow item = TabItens.Tables[0].Rows.Find(IdItem);
                            if (item != null)
                                TabItens.Tables[0].Rows.Remove(item);  

                            //Registrando Movimento de Auditoria
                            FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 3, "Excluindo Produto"+ItemMvVendas.IdProduto.ToString());
                            ItemMvVendas.IdItem = 0;                                                        
                            //GridItens.Rows.Remove(GridItens.CurrentRow);                            
                            //PopularGridItens();                            
                            TotalMovimento();
                        }
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
            {
                MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal PrcAtacado     = 0;
            decimal PrcVarejo      = 0;
            decimal PrcMinimo      = 0;
            decimal PrcEspecial    = 0;
            decimal PrcSensacional = 0;
            decimal PrcEspDist     = 0;
            int     IdPromocao     = 0;
            decimal PComPromocao   = 0;

            if (TipoMov == "PI")
            {
                MessageBox.Show("Não pode incluir itens nesse tipo de movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (!VerificarStatus())                
                    return;                
                else
                {
                    if (Vendas.IdVenda > 0)
                    {
                        if (!ValidadeMovimento())
                            return;

                        if (TipoMov == "EMVF")
                        {
                            if (Vendas.IdPessoa == 0)
                            {
                                MessageBox.Show("Favor informar o Cliente", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        CadVend.LerDados(Vendas.IdVendedor);
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.IdVendedor      = Vendas.IdVendedor;
                        BuscaPrd.VerGrpLstVenda  = true;
                        BuscaPrd.VendDist        = CadVend.Distribuidor == 1;
                        BuscaPrd.VlrPedido       = Vendas.VlrTotal;
                        BuscaPrd.IdVenda         = Vendas.IdVenda;
                        BuscaPrd.TipoCliente     = CadPessoa.Clie_Forn;
                        BuscaPrd.ShowDialog();

                        for (int I = 0; I <= BuscaPrd.ListaCodPrd.Count - 1; I++)
                        {
                            ArrayList PrdQtde = new ArrayList(BuscaPrd.ListaCodPrd[I].ToString().Split(char.Parse("|")));
                            BuscaPrd.CadProd.LerDados(int.Parse(PrdQtde[0].ToString()));
                            BuscaPrd.IdProduto = BuscaPrd.CadProd.IdProduto;

                            if (BuscaPrd.IdProduto > 0)
                            {
                                if (!VerificarStatus())
                                    return;

                                PrcMinimo      = BuscaPrd.CadProd.PrcMinimo;
                                PrcVarejo      = BuscaPrd.CadProd.PrcVarejo;
                                PrcAtacado     = BuscaPrd.CadProd.PrcAtacado;
                                PrcEspecial    = BuscaPrd.CadProd.PrcEspecial;
                                PrcSensacional = BuscaPrd.CadProd.PrcSensacional;
                                PrcEspDist     = BuscaPrd.CadProd.PrcEspDist;
                                IdPromocao   = int.Parse(PrdQtde[2].ToString());
                                PComPromocao = decimal.Parse(PrdQtde[4].ToString());
                                
                                if ((int.Parse(PrdQtde[3].ToString()) == 1 || int.Parse(PrdQtde[3].ToString()) == 4) && (TipoMov == "PV" || TipoMov == "OC"))
                                {
                                    FrmLancItensProm FrmItens = new FrmLancItensProm();
                                    FrmItens.FrmPrincipal     = FrmPrincipal;
                                    FrmItens.IdPromocao       = IdPromocao;
                                    FrmItens.IdVenda          = Vendas.IdVenda;
                                    FrmItens.ShowDialog();

                                    

                                    if (FrmItens.ListaCodPrd.Count > 1)
                                    {
                                        IncluirItensPromocao(FrmItens.ListaCodPrd, IdPromocao, BuscaPrd.IdProduto);
                                        PopularGridItens();
                                        if (GridItens.CurrentRow != null)
                                            GridItens.CurrentCell = GridItens.CurrentRow.Cells[4];

                                        FrmItens.Dispose();
                                        BuscaPrd.Dispose();
                                        return;
                                    }
                                    else
                                    {
                                        IdPromocao   = 0;
                                        PComPromocao = 0;
                                        FrmItens.Dispose();
                                        if (BuscaPrd.CadProd.IdPromocao > 0)
                                        {
                                            BuscaPrd.Dispose();
                                            return;
                                        }
                                    }
                                        
                                }
                                else if (int.Parse(PrdQtde[2].ToString()) != 0 && (TipoMov == "PV" || TipoMov == "OC"))
                                {
                                    SqlDataReader TabPromocao = Controle.ConsultaSQL("select * from PromocaoProdutosItens T1" +
                                                                                     " WHERE T1.Id_Promocao=" + IdPromocao.ToString() + " AND T1.Id_Produto=" + BuscaPrd.IdProduto.ToString());
                                    
                                    while (TabPromocao.Read())
                                    {
                                        PrcMinimo      = decimal.Parse(TabPromocao["PrcMinimo"].ToString());
                                        PrcVarejo      = decimal.Parse(TabPromocao["PrcVarejo"].ToString());
                                        PrcAtacado     = decimal.Parse(TabPromocao["PrcAtacado"].ToString());
                                        PrcEspecial    = decimal.Parse(TabPromocao["PrcEspecial"].ToString());
                                        PrcSensacional = decimal.Parse(TabPromocao["PrcSensacional"].ToString());                                        
                                    }                                    
                                }


                                
                                ItemMvVendas.LerDados(0);                                
                                ItemMvVendas.IdVenda    = Vendas.IdVenda;
                                ItemMvVendas.IdProduto  = BuscaPrd.IdProduto;                                
                                ItemMvVendas.IdPromocao = IdPromocao;

                                if (IdPromocao > 0)
                                    ItemMvVendas.PComPromocao = PComPromocao;
                                else
                                    ItemMvVendas.PComPromocao = 0;
                                                                
                                if (decimal.Parse(PrdQtde[1].ToString()) > 0)
                                    ItemMvVendas.Qtde = decimal.Parse(PrdQtde[1].ToString());
                                else
                                    ItemMvVendas.Qtde = 1;

                                if ((CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6) && BuscaPrd.CadProd.QtdeCxDist == 1 && BuscaPrd.CadProd.QtdeCaixa > 0 && (TipoMov == "VF" || TipoMov == "PV" || TipoMov == "OE"))
                                {
                                    if ((ItemMvVendas.Qtde % BuscaPrd.CadProd.QtdeCaixa) > 0)
                                        ItemMvVendas.Qtde = BuscaPrd.CadProd.QtdeCaixa;
                                }
                                else
                                {
                                    if (BuscaPrd.CadProd.QtdeCaixa > 0 && BuscaPrd.CadProd.QtdeCxDist == 0)
                                        ItemMvVendas.Qtde = BuscaPrd.CadProd.QtdeCaixa;
                                }
                                
                                if (TipoMov == "EMVF")
                                {
                                    if (SaldoEstoqueCliente(ItemMvVendas.IdProduto, Vendas.IdPessoa) < ItemMvVendas.Qtde)
                                    {
                                        MessageBox.Show("Produto: " + BuscaPrd.CadProd.Descricao.Trim() + " não tem saldo Suficiente ou não informado para esse cliente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);                                        
                                        continue;                                        
                                    }
                                }

                                if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 5 || CadPessoa.Clie_Forn == 6)
                                {

                                    if (PrcEspDist > 0 && FrmPrincipal.Perfil_Usuario.UsaPrcEspDist==1 && (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6))
                                    {
                                        ItemMvVendas.PComPromocao = 0;
                                        ItemMvVendas.IdPromocao   = 0;
                                        ItemMvVendas.VlrUnitario  = PrcEspDist;
                                    }
                                    else
                                        ItemMvVendas.VlrUnitario = PrcAtacado;
                                }
                                else if (CadPessoa.Clie_Forn == 7)
                                    ItemMvVendas.VlrUnitario = PrcMinimo;
                                else if (ItemMvVendas.IdPromocao > 0)
                                    ItemMvVendas.VlrUnitario = PrcVarejo;
                                else
                                {
                                    ItemMvVendas.VlrUnitario = PrcSensacional;
                                }
                                
                                decimal SaldoPrd = 0;
                                decimal SaldoDeposito = 0;
                                
                                
                                if (Vendas.TpVenda != "OC" && TabAux.Estoque == 2 && ParamFilial.EstoqueZero == 1 )
                                {
                                    string msg = "";
                                    if (Vendas.IdFilialEntrega != FrmPrincipal.IdFilialConexao)
                                    {
                                        SaldoPrd = BuscaSldFilialEntrega(Vendas.IdFilialEntrega, BuscaPrd.CadProd.Referencia);
                                        msg = "Filial de Entrega \n";
                                    }
                                    else                                        
                                        SaldoPrd = SaldoProduto(BuscaPrd.CadProd.IdProduto);
                                                                        
                                    
                                    if (SaldoPrd < ItemMvVendas.Qtde && (SaldoPrd + SaldoDeposito >= ItemMvVendas.Qtde))
                                    {
                                        if (MessageBox.Show("Quantidade Dispónivel no Depósito, deseja fazer a liberação automatica ? ", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            SetaLiberacaoProduto(Vendas.IdFilialEntrega, 0, Vendas.IdVenda, BuscaPrd.CadProd.Referencia, SaldoPrd);
                                        else
                                            SaldoDeposito = 0;

                                    }
                                    if (SaldoPrd < ItemMvVendas.Qtde)
                                    {
                                        if (SaldoPrd == -999999)                                        
                                            MessageBox.Show("Atenção: Erro ao verificar o saldo na Filial de Entrega", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        else if (Vendas.IdFilialEntrega != FrmPrincipal.IdFilialConexao)
                                            MessageBox.Show(msg + "Produto:" + BuscaPrd.CadProd.Descricao.Trim() + " não tem saldo Suficiente no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);                                        
                                        else
                                        {                                            
                                            if (!Vendas.ProdutoLiberado(BuscaPrd.CadProd.IdProduto) && VerGrupo_Estoque(Controle,BuscaPrd.CadProd.Referencia))
                                            {
                                                MessageBox.Show(msg + "Produto:" + BuscaPrd.CadProd.Descricao.Trim() + " não tem saldo Suficiente no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                if (FrmPrincipal.Perfil_Usuario.LiberaEstoque == 0)
                                                {
                                                    if (Vendas.TpVenda == "TROCA")
                                                        ItemMvVendas.Qtde = 0;                                                    
                                                }
                                            }
                                        }
                                    }                                    
                                }
                                //else
                                //    ItemMvVendas.Qtde = 1;
                                ItemMvVendas.IdItem         = 0;
                                ItemMvVendas.VlrTotal       = ItemMvVendas.VlrUnitario * ItemMvVendas.Qtde;
                                ItemMvVendas.VlrUntComissao = BuscaPrd.CadProd.PrcSensacional;                                
                                ItemMvVendas.PrcCusto       = BuscaPrd.CadProd.UltPrcCompra;
                                ItemMvVendas.PrcMinimo      = PrcMinimo;
                                ItemMvVendas.PrcVarejo      = PrcVarejo;
                                ItemMvVendas.PrcAtacado     = PrcAtacado;
                                ItemMvVendas.PrcEspecial    = PrcEspecial;
                                ItemMvVendas.PrcSensacional = PrcSensacional;
                                ItemMvVendas.PrcEspDist     = PrcEspDist;
                                ItemMvVendas.PComissao      = 0;
                                ItemMvVendas.VlrComissao    = 0;
                                ItemMvVendas.PDesconto      = 0;
                                ItemMvVendas.Vinculado      = 0;
                                ItemMvVendas.TipoItem       = "S";

                                if (int.Parse(LstEntrega.SelectedValue.ToString()) != FrmPrincipal.IdFilialConexao && Vendas.TpVenda != "TROCA")
                                    ItemMvVendas.TipoItem = "N";

                                ItemMvVendas.GravarDados();                                
                                //Registrando Movimento de Auditoria
                                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 1, "Incluindo Item Produto:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + "  Qtde:" + ItemMvVendas.Qtde.ToString());                                
                                //PopularGridItens();
                                //GridItens.CurrentCell = GridItens.CurrentRow.Cells[4];
                            }
                            else
                                continue;
                        }
                        PopularGridItens();
                        if (GridItens.CurrentRow != null)
                            GridItens.CurrentCell = GridItens.CurrentRow.Cells[4];
                        BuscaPrd.Dispose();
                    }
                }
            }
        }
        private void IncluirItensPromocao(ArrayList Lista, int IdPromocao, int IdPrdKit)
        {
            Produtos CadProd  = new Produtos();
            CadProd.Controle  = Controle;
            int IdProduto     = 0;
            decimal VlrUnt    = 0;
            decimal PComProm  = 0;
            decimal TQtde     = 0;
            decimal QtdePromo = 0;

            for (int I = 0; I <= Lista.Count - 1; I++)
            {
                ArrayList PrdQtde = new ArrayList(Lista[I].ToString().Split(char.Parse("|")));
                IdProduto = int.Parse(PrdQtde[0].ToString());
                CadProd.LerDados(IdProduto);
                
                SqlDataReader TabPromocao = Controle.ConsultaSQL("select * from PromocaoProdutosItens T1"+
                                                                  " LEFT JOIN PROMOCAOPRODUTOS T2 ON (T2.ID_PROMOCAO=T1.ID_PROMOCAO) "+
                                                                  " WHERE T1.Id_Promocao=" + IdPromocao.ToString()+" and T1.ID_PRODUTO="+IdProduto.ToString());

                while (TabPromocao.Read())
                {
                    VlrUnt = decimal.Parse(TabPromocao["PrcSensacional"].ToString());
                    PComProm = decimal.Parse(TabPromocao["PComissao"].ToString());
                    QtdePromo = decimal.Parse(TabPromocao["QtdeTotal"].ToString());

                    if (decimal.Parse(TabPromocao["PDesc"].ToString()) > 0 && int.Parse(TabPromocao["DescSegUnd"].ToString()) != 1)
                        VlrUnt = Math.Round(VlrUnt * ((100 - decimal.Parse(TabPromocao["PDesc"].ToString())) / 100), 2) ;

                    if (int.Parse(TabPromocao["DescSegUnd"].ToString()) == 1)
                    {
                        /*SqlDataReader TabVlrUnt = Controle.ConsultaSQL("select Top 1 T1.VlrUnitario from MvVendaItens t1  " +
                                                                       " where t1.Id_Produto in (Select id_produto from PromocaoProdutosItens t2 where t2.Id_Promocao=" + IdPromocao.ToString() + ")" +
                                                                       " and t1.Id_Promocao=0 and t1.id_venda=" + Vendas.IdVenda.ToString() + " and T1.ID_PRODUTO=" + IdProduto.ToString());

                        VlrUnt = 0;
                        while (TabVlrUnt.Read())
                            VlrUnt = decimal.Parse(TabVlrUnt["VlrUnitario"].ToString());*/

                        //decimal VlrUntDesc = Math.Floor(Math.Round(decimal.Parse(TabPromocao["PrcSensacional"].ToString()) * ((100 - decimal.Parse(TabPromocao["PDesc"].ToString())) / 100), 2));
                        decimal VlrUntDesc = Math.Round(decimal.Parse(TabPromocao["PrcSensacional"].ToString()) * ((100 - decimal.Parse(TabPromocao["PDesc"].ToString())) / 100), 2);

                        ItemMvVendas.LerDados(0);
                        ItemMvVendas.IdVenda = Vendas.IdVenda;
                        ItemMvVendas.IdProduto = IdProduto;
                        ItemMvVendas.IdPromocao = IdPromocao;
                        ItemMvVendas.IdItem = 0;
                        ItemMvVendas.VlrUnitario = VlrUntDesc;
                        ItemMvVendas.Qtde = decimal.Parse(PrdQtde[1].ToString());
                        ItemMvVendas.VlrTotal = ItemMvVendas.VlrUnitario * decimal.Parse(PrdQtde[1].ToString());
                        ItemMvVendas.VlrUntComissao = VlrUntDesc;
                        ItemMvVendas.PrcCusto = CadProd.UltPrcCompra;
                        ItemMvVendas.PrcMinimo = VlrUntDesc;
                        ItemMvVendas.PrcVarejo = VlrUntDesc;
                        ItemMvVendas.PrcAtacado = VlrUntDesc;
                        ItemMvVendas.PrcEspecial = decimal.Parse(TabPromocao["PrcEspecial"].ToString());
                        ItemMvVendas.PrcSensacional = decimal.Parse(TabPromocao["PrcSensacional"].ToString());
                        ItemMvVendas.PComissao = 0;
                        ItemMvVendas.VlrComissao = 0;
                        ItemMvVendas.PDesconto = 0;
                        ItemMvVendas.Vinculado = 0;
                        ItemMvVendas.PromQtdeItem = 1;
                        ItemMvVendas.PComPromocao = decimal.Parse(TabPromocao["PComissao"].ToString());
                        TQtde = TQtde + ItemMvVendas.Qtde;
                        if (int.Parse(LstEntrega.SelectedValue.ToString()) != FrmPrincipal.IdFilialConexao && Vendas.TpVenda != "TROCA")
                            ItemMvVendas.TipoItem = "N";
                        else
                            ItemMvVendas.TipoItem = "S";
                        ItemMvVendas.GravarDados();
                        FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 1, "Incluindo Item Produto Promoção:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + "  Qtde:" + ItemMvVendas.Qtde.ToString());

                    }

                    if (int.Parse(TabPromocao["TipoPromocao"].ToString()) == 4)
                        VlrUnt = decimal.Parse(PrdQtde[2].ToString());
                                       
                    ItemMvVendas.LerDados(0);
                    ItemMvVendas.IdVenda        = Vendas.IdVenda;
                    ItemMvVendas.IdProduto      = IdProduto;
                    ItemMvVendas.IdPromocao     = IdPromocao;
                    ItemMvVendas.IdItem         = 0;
                    ItemMvVendas.VlrUnitario    = VlrUnt;
                    ItemMvVendas.Qtde           = decimal.Parse(PrdQtde[1].ToString());
                    ItemMvVendas.VlrTotal       = ItemMvVendas.VlrUnitario * decimal.Parse(PrdQtde[1].ToString());
                    ItemMvVendas.VlrUntComissao = VlrUnt;
                    ItemMvVendas.PrcCusto       = CadProd.UltPrcCompra;
                    ItemMvVendas.PrcMinimo      = VlrUnt;
                    ItemMvVendas.PrcVarejo      = VlrUnt;
                    ItemMvVendas.PrcAtacado     = VlrUnt;
                    ItemMvVendas.PrcEspecial    = decimal.Parse(TabPromocao["PrcEspecial"].ToString());
                    ItemMvVendas.PrcSensacional = decimal.Parse(TabPromocao["PrcSensacional"].ToString());
                    ItemMvVendas.PComissao      = 0;
                    ItemMvVendas.VlrComissao    = 0;
                    ItemMvVendas.PDesconto      = 0;
                    ItemMvVendas.Vinculado      = 0;
                    ItemMvVendas.PromQtdeItem   = 1;
                    ItemMvVendas.PComPromocao   = decimal.Parse(TabPromocao["PComissao"].ToString());
                    TQtde = TQtde + ItemMvVendas.Qtde;
                    if (int.Parse(LstEntrega.SelectedValue.ToString()) != FrmPrincipal.IdFilialConexao && Vendas.TpVenda != "TROCA")
                        ItemMvVendas.TipoItem = "N";
                    else
                        ItemMvVendas.TipoItem = "S";
                    ItemMvVendas.GravarDados();
                    FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvVendas.IdItem, Vendas.NumDocumento, 1, "Incluindo Item Produto Promoção:" + ItemMvVendas.IdProduto.ToString() + "  Vr.Unit:" + ItemMvVendas.VlrUnitario.ToString() + "  Qtde:" + ItemMvVendas.Qtde.ToString());                                
                }
            }

            //Verificando o KIT do Produto
            SqlDataReader KitPromocao = Controle.ConsultaSQL("select * from ProdutosKit T1 WHERE T1.Id_PrdMaster=" + IdPrdKit.ToString());
            while (KitPromocao.Read())
            {
                IdProduto = int.Parse(KitPromocao["Id_Produto"].ToString());
                CadProd.LerDados(IdProduto);

                ItemMvVendas.LerDados(0);
                ItemMvVendas.IdVenda        = Vendas.IdVenda;
                ItemMvVendas.IdProduto      = IdProduto;
                ItemMvVendas.IdPromocao     = IdPromocao;
                ItemMvVendas.IdItem         = 0;
                if (decimal.Parse(KitPromocao["Valor"].ToString()) > 0)
                    ItemMvVendas.VlrUnitario = decimal.Parse(KitPromocao["Valor"].ToString());
                else
                    ItemMvVendas.VlrUnitario = CadProd.PrcVarejo;
                ItemMvVendas.Qtde           = TQtde / QtdePromo; //decimal.Parse(KitPromocao["QTDE"].ToString());
                ItemMvVendas.VlrTotal       = ItemMvVendas.VlrUnitario * ItemMvVendas.Qtde;
                ItemMvVendas.VlrUntComissao = VlrUnt;
                ItemMvVendas.PrcCusto       = CadProd.UltPrcCompra;
                ItemMvVendas.PrcMinimo      = CadProd.PrcMinimo;
                ItemMvVendas.PrcVarejo      = CadProd.PrcVarejo;
                ItemMvVendas.PrcAtacado     = CadProd.PrcAtacado;
                ItemMvVendas.PrcEspecial    = CadProd.PrcEspecial;
                ItemMvVendas.PrcSensacional = CadProd.PrcSensacional;
                ItemMvVendas.PComissao      = 0;
                ItemMvVendas.VlrComissao    = 0;
                ItemMvVendas.PDesconto      = 0;
                ItemMvVendas.Vinculado      = 0;
                ItemMvVendas.PromQtdeItem   = 1;
                ItemMvVendas.PComPromocao   = PComProm;

                if (int.Parse(LstEntrega.SelectedValue.ToString()) != FrmPrincipal.IdFilialConexao && Vendas.TpVenda != "TROCA")
                    ItemMvVendas.TipoItem = "N";
                else
                    ItemMvVendas.TipoItem = "S";
                ItemMvVendas.GravarDados();
            }
        }
        private void Hab_Botoes()
        {            
            TxtVlrDesconto.Enabled = StaFormEdicao && FrmPrincipal.Perfil_Usuario.BloqDesc == 0;
            TxtVlrCredito.Visible  = TipoMov == "PV" || TipoMov == "VF";
            TxtVlrCredito.Enabled  = StaFormEdicao;
            label12.Visible        = TxtVlrCredito.Visible;
            BtnFinalizar.Visible   = TipoMov == "OE" && Vendas.Status == 3 && !StaFormEdicao && FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 1;
            BtnImpOE.Visible       = TipoMov == "PV" && !StaFormEdicao;
            BtnEnviarVd.Visible    = (TipoMov == "PC" || TipoMov == "OE" || TipoMov == "AM" || TipoMov == "PV" || TipoMov == "CO" || TipoMov == "BONIF" || TipoMov == "TROCA") && !StaFormEdicao && (Vendas.Status == 2 || Vendas.Status == 3) && Vendas.IdFilialEntrega != FrmPrincipal.IdFilialConexao;            
            //BtnEnviarVd.Visible   = (TipoMov == "AM" || TipoMov == "PV" || TipoMov == "CO" || TipoMov == "BONIF" || TipoMov == "TROCA") && !StaFormEdicao && !FrmPrincipal.VersaoDistribuidor && (Vendas.Status == 2 || Vendas.Status == 3) && Vendas.IdFilialEntrega != FrmPrincipal.IdFilialConexao;            
            BtnFaturamento.Visible = false;
            ColCC.Visible          = (TipoMov == "PV" || TipoMov == "VF" || TipoMov == "PC") && Vendas.Status == 3;
            ColCC.ReadOnly         = FrmPrincipal.Perfil_Usuario.AltItemVD == 0;
            PnlUltPedFat.Visible   = TipoMov == "PV" && FrmPrincipal.Perfil_Usuario.Faturamento == 1;
            Ck_SemMovEst.Visible   = (FrmPrincipal.Perfil_Usuario.SemMovEst == 1 && (TipoMov != "PC" && TipoMov != "PV" && TipoMov != "VF" && TipoMov != "EMVF" && TipoMov != "OC"));
            //PnlEntrega.Visible   = !FrmPrincipal.VersaoDistribuidor;
            BtnImpUltVenda.Visible = Vendas.Status == 0 && Vendas.IdVenda > 0 && Vendas.IdPessoa != 0 && Vendas.IdPessoa != FrmPrincipal.Parametros_Filial.IdConsumidor;

            if (TipoMov == "PV" && FrmPrincipal.Perfil_Usuario.Faturamento == 1)
                MostraUltPedFat();

            if (StaFormEdicao || Vendas.IdVenda == 0)
            {
                BtnConfirmar.Enabled = false;
                BtnCancMov.Enabled   = false;
                BtnImprimir.Enabled  = false;
            }
            else
            {
                BtnConfirmar.Enabled   = Vendas.Status == 0 || Vendas.Status == 4;
                BtnCancMov.Enabled     = true;// Vendas.Status >= 2 && Vendas.Status < 4;
                BtnImprimir.Enabled    = true;
                BtnFaturamento.Visible = ((TabAux.Financeiro > 0 || FrmPrincipal.Perfil_Usuario.Faturamento == 1) && (TipoMov != "EMVF" && TipoMov != "PR" && TipoMov != "BONIF" && TipoMov != "AM" && TipoMov != "CO" && Vendas.VlrTotal > 0));
            }

            if (TipoMov == "PI") 
            {
                BtnConfirmar.Visible = false;
                BtnCancMov.Visible   = false;
            }
        }
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            BtnConfirmar.Enabled = false;
            Application.DoEvents();
            if (!StaFormEdicao)
            {
                try
                {
                    if (TipoMov != "PC")
                    {
                        if (MessageBox.Show("Confirma essa forma de pagamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    if (!VerificarStatus())
                    {
                        BtnConfirmar.Enabled = true;
                        return;
                    }

                    PopularGridItens();
                    Vendas.LerDados(Vendas.IdVenda);

                    CadFormaPgto.LerDados(Vendas.IdFormaPgto);
                    
                    if (Vendas.CliReativado==1 && CadFormaPgto.LibClieNovo==0 && Vendas.IdUsuLibClieNv==0)
                    {
                        MessageBox.Show("Forma de Paganmeno não é permitida para clientes novos ou reativados", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }

                    if (TipoMov == "OE" && Vendas.IdPessoa == FrmPrincipal.Parametros_Filial.IdConsumidor)
                    {
                        MessageBox.Show("Não é permitido fazer ordem de entrega para cliente consumidor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }                

                    if (TipoMov == "PV" || TipoMov == "VF" )
                    {
                        if (Vendas.IdFormaPgto == 0)
                        {
                            MessageBox.Show("Favor colocar a forma de pagamento", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            BtnConfirmar.Enabled = true;
                            return;
                        }
                    }
                    
                    if (!ValidadeMovimento())
                    {
                        BtnConfirmar.Enabled = true;
                        return;
                    }
                    
                    if (Vendas.IdPessoa == 0 && TipoMov != "OC")
                    {
                        MessageBox.Show("Favor informar o Cliente", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }

                    if (Vendas.VlrTotal < 0 && TipoMov == "PV")
                    {
                        MessageBox.Show("Atenção: Total do movimento esta negativo", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }

                    if (CadFormaPgto.VerDebito == 1)
                    {
                        if (VerificaDebito())
                        {
                            BtnConfirmar.Enabled = true;
                            return;
                        }
                    }

                    if (CadFormaPgto.BloqPF==1 && CadPessoa.Tipo==1 && Vendas.IdUsuboleto==0)
                    {
                        MessageBox.Show("Atenção: Forma de pagamento não autorizada para Pessoa Fisica.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }

                    if ((TipoMov == "BONIF" || TipoMov == "AM") && Vendas.Observacao.Length < 10)
                    {
                        MessageBox.Show("Atenção: Favor informa o campo Observação", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }

                    if (Vendas.IdFilialEntrega == 0)
                    {
                        MessageBox.Show("Favor informa o Local de Entrega", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }

                    if (Vendas.IdVdOrigem > 0)
                    {
                        MessageBox.Show("Favor soliciar a confirmação na Filial de Origem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }
                                        
                    if (Vendas.IdVdOrigem == 0 && TipoMov != "OC" && TipoMov != "BONIF" && TipoMov != "PR" && TipoMov != "AM" && TipoMov != "EMVF" && CadFormaPgto.VerCredito==1)
                    {
                        if (LimiteCreditoCliente() + Vendas.VlrTotal > CadPessoa.LimiteCredito)
                        {
                            if (Vendas.IdUsuAutDeb == 0)
                            {
                                MessageBox.Show("Atenção: Limite de Crédito do cliente excedido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (FrmPrincipal.Perfil_Usuario.LiberaDebito == 1)
                                    Vendas.SetaAutDebito(FrmPrincipal.Perfil_Usuario.IdUsuario, Vendas.VlrTotal);
                                else
                                {
                                    BtnConfirmar.Enabled = true;
                                    return;
                                }
                            }
                        }
                    }
                    // Bloquio para liberação de venda quando cliente for Distribuidor 
                    if (CadPessoa.Clie_Forn == 3 && !FrmPrincipal.VersaoDistribuidor && Vendas.IdVdOrigem == 0 && TipoMov != "OC" && TipoMov != "BONIF" && TipoMov != "PR" && TipoMov != "AM" && TipoMov != "EMVF" && CadFormaPgto.VerCredito == 1)
                    {
                        if (Vendas.IdUsuAutDeb == 0)
                        {
                            MessageBox.Show("Atenção: Cliente é Distribuidor, solicite liberação ao financeiro", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (FrmPrincipal.Perfil_Usuario.LiberaDebito == 1)
                                Vendas.SetaAutDebito(FrmPrincipal.Perfil_Usuario.IdUsuario, Vendas.VlrTotal);
                            else
                            {
                                BtnConfirmar.Enabled = true;
                                return;
                            }
                        }
                    }
                    
                    if (Vendas.IdUsuAutDeb > 0 && Vendas.VlrTotal > Vendas.VlrLiberado && FrmPrincipal.Perfil_Usuario.LiberaDebito == 0)
                    {
                        MessageBox.Show("Atenção: Valor da Venda maior que o Valor Liberado pelo Financeiro", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnConfirmar.Enabled = true;
                        return;
                    }  
                    // Verificando a Trocar de produtos
                    if (TipoMov == "TROCA" && !FrmPrincipal.VersaoDistribuidor && Vendas.IdVdOrigem == 0)
                    {
                       if (!VerificarTroca(Vendas.IdVenda))
                        {
                            BtnConfirmar.Enabled = true;
                            return;
                        }
                    }
                    //
                    if (TipoMov == "EMVF")
                    {
                        if (!VerificarSaldoPrdCliente(Vendas.IdVenda))
                        {
                            BtnConfirmar.Enabled = true;
                            return;
                        }
                    }
                    //
                    if (TipoMov == "PV" || TipoMov == "OC")
                    {
                        if (!ValidarPromocao())
                        {
                            BtnConfirmar.Enabled = true;
                            return;
                        }
                    }
                    //
                    if (MessageBox.Show("Confirma a conclusão da Venda ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (!VerificarStatus())
                        {
                            BtnConfirmar.Enabled = true;
                            return;
                        }                       
                        //
                        TabAux.LerTabela("VENDA", Vendas.TpVenda);                        
                        SqlDataReader TabItens = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE Id_Venda=" + Vendas.IdVenda.ToString());
                    
                        if (TipoMov == "TROCA")
                        {
                            SqlDataReader TabTroca = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE TipoItem='E' and Id_Venda=" + Vendas.IdVenda.ToString());
                            if (!TabTroca.HasRows)
                            {
                                MessageBox.Show("Favor informar os itens de entrada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                BtnConfirmar.Enabled = true;
                                return;
                            }
                        }
                        if (TipoMov != "VF" && TabAux.Estoque == 2 && ParamFilial.EstoqueZero == 1 && Vendas.SemMovEst == 0)
                        {
                            if (!VerificarSaldoItens(Vendas.IdVenda) && FrmPrincipal.Perfil_Usuario.LiberaEstoque == 0 )
                            {
                                BtnConfirmar.Enabled = true;
                                return;
                            }
                        }
                        if (VerificaItemQtdeZero())  // Verifica se existe algum item com a quantidade zerada
                        {
                            BtnConfirmar.Enabled = true;
                            return;
                        }
                        if (TipoMov != "VF")
                        {
                            if (VerificaItemInativo())  // Verifica se existe algum item Inativo no cadastro de Produto
                            {
                                BtnConfirmar.Enabled = true;
                                return;
                            }
                        }
                        if (TabItens.HasRows)
                        {
                            // Atualizando o Credito do Cliente
                            if (Vendas.VlrCredito > 0 && Vendas.IdVdOrigem == 0)
                            {
                                CadPessoa.LerDados(Vendas.IdPessoa);
                                if (Vendas.VlrCredito > CadPessoa.Credito)
                                {
                                    MessageBox.Show("Favor verificar o Saldo de Crédito do Cliente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    BtnConfirmar.Enabled = true;
                                    return;
                                }
                                Controle.ExecutaSQL("UPDATE PESSOAS SET CREDITO=Round(CREDITO-" + Controle.FloatToStr(Vendas.VlrCredito, 2) + ",2) WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString());
                            }

                            if ((TabAux.Financeiro > 0 || FrmPrincipal.Perfil_Usuario.Faturamento == 1) && (Vendas.TpVenda != "EMVF" && Vendas.TpVenda != "PC" && Vendas.TpVenda != "PR" && Vendas.TpVenda != "BONIF" && Vendas.TpVenda != "CO" && Vendas.VlrTotal > 0)) // && Vendas.TpVenda!="AM"))
                            {
                                // Fechamento Financeiro
                                FrmFechaMovimento FrmFecha    = new FrmFechaMovimento();
                                FrmFecha.FrmPrincipal         = FrmPrincipal;
                                FrmFecha.TxtPessoa.Text       = TxtCliente.Text;
                                FrmFecha.TxtVlrSubTotal.Value = TxtVlrSubTotal.Value;
                                FrmFecha.TxtVlrDesconto.Value = TxtVlrDesconto.Value;
                                FrmFecha.TxtVlrTotal.Value    = TxtVlrTotal.Value;
                                FrmFecha.IdPessoa             = Vendas.IdPessoa;
                                FrmFecha.NumVd                = Vendas.IdVenda;
                                FrmFecha.Referente            = LstTipoVenda.Text.Trim();
                                FrmFecha.NumDoc               = Vendas.NumDocumento.ToString();
                                FrmFecha.PagRec               = 2;

                                if (Vendas.IdFormaPgto > 0)
                                    FrmFecha.IdPgto = Vendas.IdFormaPgto;
                                else
                                    FrmFecha.IdPgto = CadPessoa.IdFormaPgto;

                                FrmFecha.LstFormaPgto.Enabled = CadPessoa.BloqFormaPgto == 0;
                                FrmFecha.ShowDialog();

                                if (FrmFecha.Concluido)
                                {                                    
                                    Vendas.IdFormaPgto = int.Parse(FrmFecha.LstFormaPgto.SelectedValue.ToString());
                                    Vendas.Status = 2;
                                    Vendas.Concluir(2);
                                    //Registrando Movimento de Auditoria                                    
                                    FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 6, "Faturamento do Movimento");
                                    // Calculando a Comissao
                                    if (TabAux.Comissao == 1)
                                    {
                                        SqlDataReader TabComissao = Controle.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                                                         " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + Vendas.IdVenda.ToString());
                                        Controles.Comissao CalcComissao = new Controles.Comissao();
                                        CalcComissao.Controle = Controle;
                                        decimal PDesconto = 0;
                                        if (Vendas.IgnoraDesc == 0)
                                        {
                                            if ((Vendas.VlrDesconto + Vendas.VlrCredito) > 0)
                                                PDesconto = 100 / (Vendas.VlrSubTotal / (Vendas.VlrDesconto + Vendas.VlrCredito));
                                        }
                                        if (Vendas.Data.Date.Year >= 2019)
                                            CalcComissao.CalcularMovimento2019(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                                        else
                                            CalcComissao.CalcularMovimento(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                                    }                                    
                                }
                                else
                                {
                                    BtnConfirmar.Enabled = true;
                                    return;
                                }
                                FrmFecha.Dispose();
                            }
                            else
                            {
                                if (Vendas.VlrTotal < 0)
                                {
                                    Vendas.Status = 2;
                                    Vendas.Concluir(2);
                                    //Registrando Movimento de Auditoria                                    
                                    FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 6, "Faturamento do Movimento");
                                    MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
                                }
                                else
                                {
                                    if (Vendas.VlrTotal == 0 || Vendas.TpVenda == "EMVF")
                                    {
                                        //Registrando Movimento de Auditoria
                                        Vendas.Status = 2;
                                        Vendas.Concluir(2);                                        
                                        FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 6, "Faturamento do Movimento");
                                    }
                                    else
                                    {
                                        if (TipoMov == "BONIF" || TipoMov == "PC")
                                        {
                                            Vendas.Status = 2;
                                            Vendas.Concluir(2);                                            
                                            FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 6, "Faturamento do Movimento");
                                        }
                                        else
                                        {
                                            Vendas.Status = 1;
                                            Vendas.Concluir(1);
                                            FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 5, "Confirmação do Movimento");
                                        }
                                    }
                                }
                                // Calculando a Comissao
                                if (TabAux.Comissao == 1)
                                {
                                    Controles.Comissao CalcComissao = new Controles.Comissao();
                                    CalcComissao.Controle = Controle;
                                    decimal PDesconto = 0;
                                    if (Vendas.IgnoraDesc == 0)
                                    {
                                        if ((Vendas.VlrDesconto + Vendas.VlrCredito) > 0)
                                            PDesconto = 100 / (Vendas.VlrSubTotal / (Vendas.VlrDesconto + Vendas.VlrCredito));
                                    }
                                    SqlDataReader TabComissao = Controle.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                                                     " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + Vendas.IdVenda.ToString());
                                    if (Vendas.Data.Date.Year >= 2019)
                                        CalcComissao.CalcularMovimento2019(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                                    else
                                        CalcComissao.CalcularMovimento(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                                }
                            }
                            // Movimento de Baixa do Estoque
                            if (Vendas.SemMovEst == 0)
                            {
                                Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                                ControleEstoque.Controle = Controle;

                                if (TipoMov == "VF")
                                    ControleEstoque.EstoqueCliente(TabItens, 1, 1, Vendas.IdPessoa);
                                if (TipoMov == "EMVF")
                                    ControleEstoque.EstoqueCliente(TabItens, 2, 1, Vendas.IdPessoa);
                                //SqlDataReader Tab = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE (TipoItem<>'N') and Id_Venda=" + Vendas.IdVenda.ToString());
                                if (TabAux.Estoque > 0)
                                {
                                    SqlDataReader TabSaida = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE TipoItem='S' and Id_Venda=" + Vendas.IdVenda.ToString());
                                    ControleEstoque.MovimentoEstoque(TabSaida, TabAux.Estoque, 1, false, Vendas.TpVenda, Vendas.Data,0);                                    
                                }
                            }
                            //------                            
                            //Atualizando Cadastro do Cliente para o Vendedor
                            if (CadPessoa.IdVendedor == 0 || (Vendas.CliReativado == 1 && CadVend.EntraRel == 1))
                                Controle.ExecutaSQL("Update Pessoas set Id_Vendedor=" + Vendas.IdVendedor.ToString() + " where id_pessoa=" + CadPessoa.IdPessoa.ToString());

                            //Registrando Movimento de Auditoria
                            MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Vendas.LerDados(Vendas.IdVenda);
                            PopularGridItens();
                            Hab_Botoes();                            
                        }
                    }
                }
                catch (Exception Error)
                {
                    MessageBox.Show("Favor Informar esse Erro:" + Error.ToString() + " ao Setor de TI");
                }
            }
            BtnConfirmar.Enabled = true;
            Hab_Botoes();
        }        
        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            BtnCancMov.Enabled = false;
            Application.DoEvents();
            //if (!StaFormEdicao)
            Vendas.LerDados(Vendas.IdVenda);
            string MsgCompl = "";

           /* if (Vendas.Data.Date.Year != DateTime.Now.Date.Year)                
            {
                MessageBox.Show("Ano do movimento, não autorizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnCancMov.Enabled = true;
                return;
            }*/

            if (Vendas.Status == 0)
            {
                MessageBox.Show("Movimento não foi confirmado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnCancMov.Enabled = true;
                return;
            }
            else if (Vendas.Status == 4)
            {
                MessageBox.Show("Movimento já cancelado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnCancMov.Enabled = true;
                return;                
            }
                                                
            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
            ControleEstoque.Controle = Controle;
            TabelasAux TabAux = new TabelasAux();
            TabAux.Controle = Controle;
            TabAux.LerTabela("VENDA", Vendas.TpVenda);
                        
            if (Vendas.TpVenda == "PV" && FrmPrincipal.IdFilialConexao!=Vendas.IdFilialEntrega && Vendas.IdVdDestino > 0 && (Vendas.Status == 2 || Vendas.Status == 3))
            {
                if (!Ver_StaEnvioVd(Vendas.IdVdDestino))
                {
                    BtnCancMov.Enabled = true;
                    return;                
                }
            }
            
            if (Vendas.Status == 3 && FrmPrincipal.Perfil_Usuario.AlteraFinanceiro==0)
            {
                MessageBox.Show("Movimento já Entregue Favor Solicitar o Cancelamento ao Setor Financeiro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnCancMov.Enabled = true;
                return;
            }
            else if (Vendas.IdEntregador > 0)
            {
                MessageBox.Show("Movimento em Rota para Entrega, Favor verificar com o Setor de Expedição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnCancMov.Enabled = true;
                return;
            }
            else if (Vendas.Status == 2 )
            {
                if (Vendas.TpVenda == "AM")
                {
                    if (FrmPrincipal.Perfil_Usuario.CancAmostra == 0)
                    {
                        FrmAutorizacao Autorizacao = new FrmAutorizacao();
                        Autorizacao.FrmPrincipal = FrmPrincipal;
                        Autorizacao.ShowDialog();
                        //Verificando se o Acesso foi liberado
                        if (Autorizacao.AcessoOk)
                        {
                            if (Autorizacao.Usuario.CancAmostra == 0)
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                BtnCancMov.Enabled = true;
                                return;
                            }
                            MsgCompl = "Autorizado:" + Autorizacao.Usuario.Usuario;
                        }
                        else
                        {
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            BtnCancMov.Enabled = true;
                            return;
                        }
                    }
                }
                else
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
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                BtnCancMov.Enabled = true;
                                return;
                            }
                            MsgCompl = "Autorizado:" + Autorizacao.Usuario.Usuario;
                        }
                        else
                        {
                            MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            BtnCancMov.Enabled = true;
                            return;
                        }
                    }
                }
            }

            if (ParamFilial.VerCancBxFin == 1)
            {
                if (VerificaBaixaTitulo(Vendas.IdVenda))
                {
                    BtnCancMov.Enabled = true;
                    return;
                }
            }
                        
            if (Vendas.Status == 3)
            {
                if (VerificaCaixa(Vendas.IdCaixa))
                {
                    if (Vendas.TpVenda != "AM")
                    {
                        if (FrmPrincipal.Perfil_Usuario.CancAmostra == 0)
                        {
                            FrmAutorizacao Autorizacao = new FrmAutorizacao();
                            Autorizacao.FrmPrincipal = FrmPrincipal;
                            Autorizacao.ShowDialog();
                            //Verificando se o Acesso foi liberado
                            if (Autorizacao.AcessoOk)
                            {
                                if (Autorizacao.Usuario.CancAmostra == 0)
                                {
                                    MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    BtnCancMov.Enabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                BtnCancMov.Enabled = true;
                                return;
                            }
                        }
                    }                    
                }
            }

            if (MessageBox.Show("Confirma o Cancelamento do Movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TelaInfAdicionais();

                Vendas.LerDados(Vendas.IdVenda);

                if (Vendas.Status == 0)
                {
                    MessageBox.Show("Movimento não foi confirmado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    BtnCancMov.Enabled = true;
                    return;
                }
                else if (Vendas.Status == 4)
                {
                    MessageBox.Show("Movimento já cancelado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    BtnCancMov.Enabled = true;
                    return;
                }

                if (Vendas.ObsCancelamento == "" || !RegistroInf)
                {
                    MessageBox.Show("Favor informar o Motivo do Cancelamento nas informações Adicionais.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    BtnCancMov.Enabled = true;
                    return;
                }              

                if (Vendas.SemMovEst == 0)
                {
                    SqlDataReader TabItens = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE Id_Venda=" + Vendas.IdVenda.ToString());
                    if (TabItens.HasRows)
                    {
                        if (TipoMov == "VF")
                            ControleEstoque.EstoqueCliente(TabItens, 1, 2, Vendas.IdPessoa);
                        if (TipoMov == "EMVF")
                            ControleEstoque.EstoqueCliente(TabItens, 2, 2, Vendas.IdPessoa);

                        SqlDataReader TabSaida = Controle.ConsultaSQL("SELECT T2.* FROM MVVENDA T1 LEFT JOIN MVVENDAITENS T2 ON (T2.ID_VENDA=T1.ID_VENDA) WHERE T2.TipoItem='S' AND T1.ID_VDMASTER=" + Vendas.IdVdMaster.ToString());
                        
                        if (TabAux.Estoque > 0)
                            ControleEstoque.MovimentoEstoque(TabSaida, TabAux.Estoque, 2, false, Vendas.TpVenda, Vendas.Data,0);                                        
                    }
                }
                if (Vendas.VlrCredito > 0 && Vendas.IdVdOrigem == 0)
                    Controle.ExecutaSQL("UPDATE PESSOAS SET CREDITO=Round(CREDITO+" + Controle.FloatToStr(Vendas.VlrCredito, 2) + ",2) WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString());
                                
                Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE Id_Venda=" + Vendas.IdVdMaster.ToString());
                Vendas.Cancelar(Vendas.Status);
                Controle.ExecutaSQL("UPDATE MvVenda Set Id_Caixa=0,Faturado=0 Where VinculoVd='" + string.Format("{0:D6}", Vendas.IdVenda.ToString()) + "'");
                //Registrando Movimento de Auditoria
                FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 4, "Cancelamento  " + MsgCompl);
                MessageBox.Show("Movimento cancelado", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopularCampos(Vendas.IdVenda);                
            }
            BtnCancMov.Enabled = true;
        }

        public void ImprimirVenda(int IdVenda)
        {
            if ((Vendas.Status == 4 || Vendas.Status == 0) && Vendas.TpVenda != "OC")
                return;

            string FormaPgto = "";
            if (Vendas.TpVenda == "PV" || Vendas.TpVenda == "VF")
            {
                DataSet Parcelas = new DataSet();
                Parcelas = Controle.ConsultaTabela("SELECT T1.VENCIMENTO,T1.VLRORIGINAL,T2.DOCUMENTO FROM LancFinanceiro T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString());

                for (int I = 0; I <= Parcelas.Tables[0].Rows.Count - 1; I++)
                {
                    DateTime Dt = DateTime.Parse(Parcelas.Tables[0].Rows[I]["Vencimento"].ToString());
                    FormaPgto = FormaPgto + Dt.Date.ToShortDateString() + "   R$" + string.Format("{0:N2}", decimal.Parse(Parcelas.Tables[0].Rows[I]["VlrOriginal"].ToString())) + "   " + Parcelas.Tables[0].Rows[I]["Documento"].ToString();
                }
            }
            
            if (FrmPrincipal.Perfil_Usuario.ImpResumido == 1)
            {
                if (MessageBox.Show("Imprimir Tamanho Resumido ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (FrmPrincipal.VersaoDistribuidor)
                    {
                        if (FrmPrincipal.TipoImpResumida == "MP4000")
                            ImpMiniImpBematech(1);
                        else if (FrmPrincipal.TipoImpResumida == "MP4200")
                            ImpMiniImpBematech(2);
                        else if (FrmPrincipal.TipoImpResumida == "ELGIN_I9")
                        {
                            PrintDialog Imp = new PrintDialog();
                            Imp.PrinterSettings = new PrinterSettings();
                            if (DialogResult.OK == Imp.ShowDialog(this))
                            {
                                Controles.ImpElginI9 imp = new Controles.ImpElginI9();
                                imp.Controle.Conexao = FrmPrincipal.Conexao;
                                imp.ImprimirNaoFiscal(Vendas.IdVenda, Imp.PrinterSettings.PrinterName);
                            }

                        }
                        else
                            ImpSocket.SendText(Vendas.IdVenda.ToString());
                    }
                    else
                        ImpNormal(FormaPgto,true);
                    //ImprimirResumido(FormaPgto);
                }
                else
                    ImpNormal(FormaPgto,false);
            }
            else
            {
                ImpNormal(FormaPgto,false);
            }
            
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (Vendas.VdImpFat == 1)
            {
                if (MessageBox.Show("Pedido já foi impresso pelo Faturamento, deseja imprimir novamente ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BtnImprimir.Enabled = false;
                    ImprimirVenda(Vendas.IdVenda);
                    BtnImprimir.Enabled = true;
                }
            }
            else
            {
                BtnImprimir.Enabled = false;
                ImprimirVenda(Vendas.IdVenda);
                BtnImprimir.Enabled = true;
            }
        }
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            
            if (StaFormEdicao)
            {
                if (Vendas.Status == 1)
                    MessageBox.Show("Movimento já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                    BuscaPessoa.FrmPrincipal = this.FrmPrincipal;                    
                    BuscaPessoa.ShowDialog();
                    if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                    {
                        if (BuscaPessoa.CadPessoa.Clie_Forn == 4)
                        {
                            MessageBox.Show("Pessoa selecionada é um Grupo ou Rede de Empresas.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        CadPessoa.LerDados(BuscaPessoa.CadPessoa.IdPessoa);
                        Vendas.IdPessoa = BuscaPessoa.CadPessoa.IdPessoa;
                                                                                               
                        Vendas.IdFormaPgto         = BuscaPessoa.CadPessoa.IdFormaPgto;
                        LstFormaPgto.SelectedValue = CadPessoa.IdFormaPgto.ToString();
                        Vendas.PrazoPgto           = CadPessoa.PrazoPgto;
                        TxtPrazoPgto.Text          = CadPessoa.PrazoPgto;
                        SetaPessoa(BuscaPessoa.CadPessoa.IdPessoa);

                        if (BuscaPessoa.CadPessoa.IdVendedor > 0 && FrmPrincipal.Perfil_Usuario.VendedorBalcao==0)
                            LstVendedor.SelectedValue = BuscaPessoa.CadPessoa.IdVendedor.ToString();

                        if (CadPessoa.IdFilial > 0)
                                LstFilial.SelectedValue = CadPessoa.IdFilial.ToString();

                        if (BuscaPessoa.CadPessoa.IdPessoa!=FrmPrincipal.Parametros_Filial.IdConsumidor)
                        {
                            FrmAtlzCadPessoa FrmAtlz = new FrmAtlzCadPessoa();
                            FrmAtlz.FrmPrincipal = FrmPrincipal;
                            FrmAtlz.IdPessoa     = BuscaPessoa.CadPessoa.IdPessoa;
                            FrmAtlz.CadPessoa    = BuscaPessoa.CadPessoa;
                            FrmAtlz.ShowDialog();
                        }

                        PopularEndereco(BuscaPessoa.CadPessoa.IdPessoa);
                        
                        if (CadPessoa.Credito > 0 && TipoMov == "PV")
                        {
                            if (MessageBox.Show("Deseja utilizar o crédito do cliente ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                TxtVlrCredito.Value = CadPessoa.Credito;
                            else
                                TxtVlrCredito.Value = 0;
                            TxtVlrTotal.Value = TxtVlrSubTotal.Value - (TxtVlrDesconto.Value + TxtVlrCredito.Value);
                        }
                        if (TipoMov == "PV")
                        {
                            SqlDataReader TabVerOE = Controle.ConsultaSQL("SELECT * FROM MVVENDA  WHERE STATUS=3 AND TPVENDA='OE' AND FATURADO=0 AND ID_PESSOA=" + Vendas.IdPessoa.ToString());
                            if (TabVerOE.HasRows)
                                MessageBox.Show("Atenção: Cliente tem ordem de entregar a ser faturada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                        if (TipoMov == "PV" || TipoMov == "VF")
                        {
                            if (FrmPrincipal.Parametros_Filial.CliDiasInativo > 0)
                                Vendas.CliReativado = VerificaReativacaoCliente(BuscaPessoa.CadPessoa.IdPessoa);                            
                        }
                    }
                    else
                    {
                        CadPessoa.LerDados(0);
                        //MostraUltCompra();
                        //MostraUltOrcamento();
                        Vendas.IdPessoa     = 0;
                        Vendas.VlrCredito   = 0;
                        TxtVlrCredito.Value = 0;
                        PopularEndereco(0);
                    }
                }
            }
        }
        private int VerificaReativacaoCliente(int IdPessoa)
        {
            SqlDataReader TabVerifInativo = Controle.ConsultaSQL("SELECT TOP 1 * FROM MVVENDA WHERE TPVENDA IN ('PV','VF') AND ID_PESSOA=" + IdPessoa.ToString() +
                                                                 " AND STATUS in (1,2,3) AND DATA > CONVERT(DATETIME,CONVERT(CHAR,GETDATE()-" + FrmPrincipal.Parametros_Filial.CliDiasInativo.ToString() + ",103),103) order by data desc");

            if (!TabVerifInativo.HasRows)  // Caso nao tenha Registro Informar Reativação do Cliente
                return 1;
            else
                return 0;
        }

        private void SetaPessoa(int IdPessoa)
        {
            CadPessoa.LerDados(IdPessoa);            
            Vendas.IdPessoa    = CadPessoa.IdPessoa;
            TxtCodCliente.Text = CadPessoa.IdPessoa.ToString();
            TxtCliente.Text    = CadPessoa.RazaoSocial.Trim();
            if (CadPessoa.IdPessoa > 0)
            {                
                if (CadPessoa.Tipo == 0)
                    SetaMaskCnpj(1);
                else
                    SetaMaskCnpj(2);
            }
            else
                SetaMaskCnpj(1);
            TxtCnpj.Enabled      = CadPessoa.IdPessoa == 0 && TipoMov == "OC";
            TxtNmPessoa.Enabled  = CadPessoa.IdPessoa == 0 && TipoMov == "OC";
            TxtInscUF.Enabled    = CadPessoa.IdPessoa == 0 && TipoMov == "OC";
            LstFormaPgto.Enabled = true;
            if (CadPessoa.BloqFormaPgto == 1)
            {
                LstFormaPgto.SelectedValue = CadPessoa.IdFormaPgto.ToString();
                LstFormaPgto.Enabled = false;
            }

            //if (TipoMov=="OC" && IdPessoa != FrmPrincipal.Parametros_Filial.IdConsumidor)
               // MostraUltOrcamento();
        }
        private void PopularEndereco(int IdPessoa)
        {
            if (IdPessoa > 0)
            {                
                TxtCnpj.Text          = CadPessoa.Cnpj;
                TxtNmPessoa.Text      = CadPessoa.RazaoSocial;
                TxtInscUF.Text        = CadPessoa.InscUF;
                TxtCep.Text           = CadPessoa.Cep;
                TxtEndereco.Text      = CadPessoa.Endereco;
                LstPais.SelectedValue = CadPessoa.Pais;
                TxtNumero.Text        = CadPessoa.Numero;
                TxtComplemento.Text   = CadPessoa.Complemento;
                TxtCidade.Text        = CadPessoa.Cidade;
                TxtBairro.Text        = CadPessoa.Bairro;
                LstUF.SelectedValue   = CadPessoa.IdUF.ToString();
                TxtFone.Text          = CadPessoa.Fone;
            }
            else
            {
                Vendas.IdPessoa       = 0;
                TxtCodCliente.Text    = "0";                
                TxtCliente.Text       = "";
                TxtCnpj.Text          = "";
                TxtNmPessoa.Text      = "";
                TxtInscUF.Text        = "";
                TxtCep.Text           = "";
                TxtEndereco.Text      = "";
                LstPais.SelectedValue = "1058";
                TxtNumero.Text        = "";
                TxtComplemento.Text   = "";
                TxtCidade.Text        = "";
                TxtBairro.Text        = "";
                LstUF.SelectedValue   = 0;
                TxtFone.Text          = "";
            }
        }
        private void LstTipMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabAux.Controle != null)
            {
                TabAux.LerTabela("VENDA", LstTipoVenda.SelectedValue.ToString());
            }
        }
        private void TxtVlrDesconto_Validated(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                TxtVlrTotal.Value = TxtVlrSubTotal.Value - (TxtVlrDesconto.Value + TxtVlrCredito.Value);
        }
        private void TxtVlrCredito_Validated(object sender, EventArgs e)
        {            
            if (StaFormEdicao)
            {
                if (TxtVlrCredito.Value > CadPessoa.Credito)
                {
                    MessageBox.Show("Valor do Crédito maior que o Saldo do Cliente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtVlrCredito.Value = 0;
                }
                TxtVlrTotal.Value = TxtVlrSubTotal.Value - (TxtVlrDesconto.Value + TxtVlrCredito.Value);
            }
        }    
        private void PagCab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PagCab.SelectedIndex == 2)
                PopularFinanceiro();
        }
        
        private void PopularPromocao()
        {                        
            /*DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("select t2.Referencia,t2.Descricao," +
                                             " CASE ISNULL(T1.PrcSensacional,0)  WHEN 0 THEN T2.PrcSensacional ELSE T1.PRCSENSACIONAL END AS PrcSensacional," +
                                             " CASE ISNULL(T1.PrcEspecial,0)     WHEN 0 THEN T2.PrcEspecial ELSE T1.PRCESPECIAL END AS PrcEspecial," +
                                             " CASE ISNULL(T1.PrcVarejo,0)       WHEN 0 THEN T2.PrcVarejo   ELSE T1.PrcVarejo END AS PrcVarejo," +
                                             " CASE ISNULL(T1.PrcMinimo,0)       WHEN 0 THEN T2.PrcMinimo   ELSE T1.PrcMinimo END AS PrcMinimo," +
                                             " CASE ISNULL(T1.PrcAtacado,0)      WHEN 0 THEN T2.PrcAtacado  ELSE T1.PrcAtacado END AS PrcAtacado from Promocoes t1" +
                                             "  left join Produtos t2 on (t2.Id_Produto=t1.Id_Produto)" +
                                             " WHERE convert(DateTime,convert(char,'" + FrmPrincipal.DtHrServidor().ToShortDateString() + "',103),103) >= T1.DTINICIO "+
                                             "   AND convert(DateTime,convert(char,'" + FrmPrincipal.DtHrServidor().ToShortDateString() + "',103),103) <= T1.DTFINAL ");

            GridPromocao.DataSource = Tabela;
            GridPromocao.DataMember = Tabela.Tables[0].TableName;*/
        }
        
        private void PopularFinanceiro()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.DATALANC,T1.NUMDOCUMENTO,T2.DOCUMENTO,T1.VENCIMENTO,T1.VLRORIGINAL,T1.DTBAIXA,T1.VLRJURO,T1.VLRMULTA,T1.VLRBAIXA FROM LANCFINANCEIRO T1 " +
                                             " LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE Id_Venda > 0 and Id_Venda=" + Vendas.IdVdMaster.ToString());
            GridFinanc.DataSource = Tabela;
            GridFinanc.DataMember = Tabela.Tables[0].TableName;
        }
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {            
            LblVdImp.Text = "";
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[17].Value.ToString() == "1" && FrmPrincipal.Perfil_Usuario.Faturamento == 1)
                {
                    LblVdImp.Text = "Doc.Já Impresso";
                }
            }
        }
        private void TxtCnpj_Validated(object sender, EventArgs e)
        {            
            if (TxtCnpj.Text != "" && StaFormEdicao)
            {
                Verificar ExisteCad = new Verificar();
                ExisteCad.Controle = Controle;
                int CodPessoa = ExisteCad.Verificar_ExisteCadastro("ID_PESSOA", "SELECT ID_PESSOA FROM PESSOAS WHERE CNPJ='" + TxtCnpj.Text.Trim() + "'");
                if (CodPessoa > 0)
                {
                    //if (VerificaDebito())
                    //    return;
                    SetaPessoa(CodPessoa);
                    PopularEndereco(CodPessoa);
                }                
                if (TxtCnpj.Text.Trim().Length > 11)
                {
                    if (!Controle.ValidarCnpj(TxtCnpj.Text))
                    {
                        MessageBox.Show("CNPJ inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtCnpj.Focus();
                    }
                    else
                        SetaMaskCnpj(1);
                }
                else
                {
                    if (!Controle.ValidarCpf(TxtCnpj.Text))
                    {
                        MessageBox.Show("CPF inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtCnpj.Focus();
                    }
                    else
                        SetaMaskCnpj(2);
                }
                if (CadPessoa.IdPessoa == 0 && TipoMov!="OC")
                {
                    MessageBox.Show("Cliente não Localizado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtCnpj.Text = "";
                }
            }
        }
        private void SetaMaskCnpj(int Tipo) //1-Cnpj / 2-CPF
        {
            if (Tipo == 1)
            {
                TxtCnpj.Mask = "00,000,000/0000-00";
                label11.Text = "CNPJ:";
                label9.Text  = "Razão Social:";
                label14.Text = "Insc.Estadual:";
            }
            else
            {
                TxtCnpj.Mask = "000,000,000-00";
                label11.Text = "CPF:";
                label9.Text  = "Nome:";
                label14.Text = "RG:";
            }
        }
        private decimal LimiteCreditoCliente()
        {
            decimal Total = 0;
            SqlDataReader TabDeb;
            TabDeb = Controle.ConsultaSQL("SELECT Isnull(Sum(VlrOriginal),0) as Valor FROM LANCFINANCEIRO WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString() + " AND STATUS=0");
            //TabDeb = Controle.ConsultaSQL("SELECT Isnull(Sum(VlrOriginal),0) as Valor FROM LANCFINANCEIRO WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString() + " AND VENCIMENTO < Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) AND STATUS=0");
            if (TabDeb.HasRows)
            {
                TabDeb.Read();
                Total = Total + decimal.Parse(TabDeb["Valor"].ToString());
            }
            TabDeb = Controle.ConsultaSQL("SELECT Isnull(sum(VALOR),0) as Valor FROM MOVCHEQUEPRE WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString() + " AND (DTVENCIMENTO >= Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) OR STATUS=1)");
            if (TabDeb.HasRows)
            {
                TabDeb.Read();
                Total = Total + decimal.Parse(TabDeb["Valor"].ToString());
            }
            return Total;
        }

        private bool VerificaBaixaTitulo(int IdVenda)
        {
            SqlDataReader TabDeb;
            TabDeb = Controle.ConsultaSQL("SELECT * FROM LANCFINANCEIRO WHERE ID_VENDA=" + IdVenda.ToString() + " AND DTBAIXA IS NOT NULL");
            if (TabDeb.HasRows)
            {
                MessageBox.Show("Atenção: Movimento com titulo já baixado, entre em contato com o financeiro", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        private bool VerificaCaixa(int IdCaixa)
        {
            SqlDataReader TabDeb;
            TabDeb = Controle.ConsultaSQL("SELECT * FROM CAIXABALCAO WHERE ID_CAIXA=" + IdCaixa.ToString()+" AND STATUS=1");
            if (TabDeb.HasRows)
            {
                MessageBox.Show("Atenção: Caixa já fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        private bool VerificaDebito()
        {
            bool DebWebServ = true;
            if (Vendas.CnpjCpf == "00000000000000" || Vendas.CnpjCpf == "11111111111111" || Vendas.CnpjCpf == "22222222222222" || Vendas.CnpjCpf == "33333333333333" || Vendas.CnpjCpf == "44444444444444"
             || Vendas.CnpjCpf == "55555555555555" || Vendas.CnpjCpf == "66666666666666" || Vendas.CnpjCpf == "77777777777777" || Vendas.CnpjCpf == "88888888888888" || Vendas.CnpjCpf == "99999999999999")
                DebWebServ=false;
            if (Vendas.CnpjCpf == "00000000000" || Vendas.CnpjCpf == "11111111111" || Vendas.CnpjCpf == "22222222222" || Vendas.CnpjCpf == "33333333333" || Vendas.CnpjCpf == "44444444444"
             || Vendas.CnpjCpf == "55555555555" || Vendas.CnpjCpf == "66666666666" || Vendas.CnpjCpf == "77777777777" || Vendas.CnpjCpf == "88888888888" || Vendas.CnpjCpf == "99999999999")
                DebWebServ=false;
            
            if (FrmPrincipal.Parametros_Filial.ClienteAtraso == 0 || TabAux.VerificaDeb == 0)
                return false;            
            
            bool AutFinanc = false;
            decimal Total = 0;
            SqlDataReader TabDeb;

            if (FrmPrincipal.Parametros_Filial.WSClienteAtraso == 1 && DebWebServ)  
            {   
                try
                {
                    Controles.Serv_DebitoCliente.DebitoCliente WsDebitoCliente = new Controles.Serv_DebitoCliente.DebitoCliente();                    
                    WsDebitoCliente.Url = "http://" + FrmPrincipal.URLMatriz + "/WSDebitoCliente/DebitoCliente.asmx?swdl";

                    if (WsDebitoCliente.VerificarDebito(Vendas.CnpjCpf) == 1)
                    {
                        MessageBox.Show("Atenção: Cliente com Cheques ou titulos em aberto, Favor contactar o setor financeiro", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AutFinanc = true;
                    }
                }
                catch 
                {                    
                }
            }
            else
            {                
                TabDeb = Controle.ConsultaSQL("SELECT * FROM LANCFINANCEIRO WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString() + " AND VENCIMENTO < Convert(DateTime,'" + DateTime.Now.Date.AddDays(-1).ToShortDateString() + "',103) AND STATUS=0");
                if (TabDeb.HasRows)
                {
                    MessageBox.Show("Atenção: Cliente com titulos em aberto", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AutFinanc = true;
                }
                TabDeb = Controle.ConsultaSQL("SELECT * FROM MOVCHEQUEPRE WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString() + " AND STATUS=1");
                if (TabDeb.HasRows)
                {
                    MessageBox.Show("Atenção: Cliente com cheque devolvido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AutFinanc = true;
                }
            }            
            
            if (AutFinanc)
            {
            
                if (TipoMov=="OC" || TipoMov=="BONIF" || TipoMov=="PR" || TipoMov=="AM")
                    return false;

                if (FrmPrincipal.Perfil_Usuario.LiberaDebito == 1)
                {
                    Vendas.SetaAutDebito(FrmPrincipal.Perfil_Usuario.IdUsuario,Vendas.VlrTotal);
                    return false;
                }
                else
                {
                    if (Vendas.IdUsuAutDeb == 0)
                        return true;
                    else
                        return false;
                }
            }
            else
                return false;
        }
        private decimal SaldoEstoqueCliente(int IdPrd, int IdPessoa)
        {
            SqlDataReader TabEstoque;           
            TabEstoque = Controle.ConsultaSQL("SELECT * FROM SALDOPRDCLIENTE WHERE ID_PESSOA=" + IdPessoa.ToString() + " AND ID_PRODUTO=" + IdPrd.ToString());
            if (TabEstoque.HasRows)
            {
                TabEstoque.Read();
                return decimal.Parse(TabEstoque["SALDO"].ToString());
            }
            else
                return 0;
        }

        private void BtnFaturamento_Click(object sender, EventArgs e)
        {   
            MvVenda StaVenda = new MvVenda();
            StaVenda.Controle = Controle;
            StaVenda.LerDados(Vendas.IdVenda);

            if (VerificaBaixaTitulo(Vendas.IdVenda))
                return;

            if (StaVenda.Status == 2 && StaVenda.IdEntregador!=0)
            {
                MessageBox.Show("Movimento já Faturado e em Rota", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (StaVenda.Status == 3)
            {
                MessageBox.Show("Movimento já Entregue", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (StaVenda.Status == 0 || StaVenda.Status == 4)
            {
                MessageBox.Show("Favor confirmar o movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {                
                if (Vendas.IdVenda > 0 && Vendas.TpVenda!="PC")
                {
                    Vendas.LerDados(Vendas.IdVenda);

                    if (!ValidadeMovimento())
                        return;

                    // Fechamento Financeiro
                    FrmFechaMovimento FrmFecha    = new FrmFechaMovimento();
                    FrmFecha.FrmPrincipal         = FrmPrincipal;
                    FrmFecha.TxtPessoa.Text       = TxtCliente.Text;
                    FrmFecha.TxtVlrSubTotal.Value = TxtVlrSubTotal.Value;
                    FrmFecha.TxtVlrDesconto.Value = TxtVlrDesconto.Value;
                    FrmFecha.TxtVlrTotal.Value    = TxtVlrTotal.Value;
                    FrmFecha.IdPessoa             = Vendas.IdPessoa;
                    FrmFecha.NumVd                = Vendas.IdVenda;
                    FrmFecha.Referente            = LstTipoVenda.Text.Trim();
                    FrmFecha.NumDoc               = Vendas.NumDocumento.ToString();
                    FrmFecha.IdFilial             = Vendas.IdFilial;
                    FrmFecha.PagRec               = 2;
                    if (Vendas.IdFormaPgto > 0)
                        FrmFecha.IdPgto = Vendas.IdFormaPgto;
                    else
                        FrmFecha.IdPgto = CadPessoa.IdFormaPgto;
                    FrmFecha.LstFormaPgto.Enabled = CadPessoa.BloqFormaPgto == 0;
                    FrmFecha.ShowDialog();
                    if (FrmFecha.Concluido)
                    {
                        Vendas.Concluir(2);
                        Vendas.Status      = 2;
                        Vendas.IdFormaPgto = int.Parse(FrmFecha.LstFormaPgto.SelectedValue.ToString());                        
                        Vendas.GravarDados();
                        Vendas.LerDados(Vendas.IdVenda);
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 6, "Faturamento do Movimento (Vendas)");
                        MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Paginas.SelectTab(0);
                        
                    }
                    FrmFecha.Dispose();
                }
            }
        }

        private void GridItens_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           if (e.ColumnIndex == 1)
           {
               if (e.Value.ToString().Trim() == "N")
                   GridItens.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
               if (e.Value.ToString().Trim() == "E")
                   GridItens.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Teal;
               if (e.Value.ToString().Trim() == "V")
                   GridItens.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
           }

           if (GridItens.CurrentRow != null)
           {               
               LblPrcSensacional.Text = "P.S: " + string.Format("{0:N2}", decimal.Parse(GridItens.CurrentRow.Cells[23].Value.ToString()));
               LblPrcEspecial.Text    = "P.E: " + string.Format("{0:N2}", decimal.Parse(GridItens.CurrentRow.Cells[9].Value.ToString()));
               LblPrcVarejo.Text      = "P.V: " + string.Format("{0:N2}", decimal.Parse(GridItens.CurrentRow.Cells[10].Value.ToString()));
               LblPrcMinimo.Text      = "P.M: " + string.Format("{0:N2}", decimal.Parse(GridItens.CurrentRow.Cells[11].Value.ToString()));
               LblPrcDist.Text        = "P.D: " + string.Format("{0:N2}", decimal.Parse(GridItens.CurrentRow.Cells[12].Value.ToString()));               
               if (decimal.Parse(GridItens.CurrentRow.Cells[25].Value.ToString()) > 0 && FrmPrincipal.Perfil_Usuario.UsaPrcEspDist==1)
                    LblPrcEspDist.Text = "P.E.D: " + string.Format("{0:N2}", decimal.Parse(GridItens.CurrentRow.Cells[25].Value.ToString()));
               else
                    LblPrcEspDist.Text = "";
               LblTotGrp.Text         = "Total G. Talimpo R$: " + string.Format("{0:N2}", TotalGrupoTalimpo());
               LblCusto.Text          = "P.C: " + string.Format("{0:N2}", decimal.Parse(GridItens.CurrentRow.Cells[24].Value.ToString()));



                if (int.Parse(GridItens.CurrentRow.Cells[18].Value.ToString()) > 0)
               {
                    LblPrcSensacional.ForeColor = System.Drawing.Color.Maroon;
                    LblPrcEspecial.ForeColor = System.Drawing.Color.Maroon;
                    LblPrcVarejo.ForeColor   = System.Drawing.Color.Maroon;
                    LblPrcMinimo.ForeColor   = System.Drawing.Color.Maroon;
                    LblPrcDist.ForeColor     = System.Drawing.Color.Maroon;
               }
               else
               {
                    LblPrcSensacional.ForeColor = System.Drawing.Color.Black;
                    LblPrcEspecial.ForeColor = System.Drawing.Color.Black;
                    LblPrcVarejo.ForeColor   = System.Drawing.Color.Black;
                    LblPrcMinimo.ForeColor   = System.Drawing.Color.Black;
                    LblPrcDist.ForeColor     = System.Drawing.Color.Black;
               }
               ImgNR.Visible = int.Parse(GridItens.CurrentRow.Cells[19].Value.ToString()) > 0;
           }
        }

        private decimal TotalGrupoTalimpo()
        {
            decimal VlrTotal = 0;
            for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
            {
                if (TabItens.Tables[0].Rows[I].RowState == DataRowState.Deleted)
                    TabItens.Tables[0].Rows.RemoveAt(I);
                else
                {
                    if (TabItens.Tables[0].Rows[I]["TIPOITEM"].ToString() == "S" && int.Parse(TabItens.Tables[0].Rows[I]["ID_GRUPO"].ToString().Trim())==53 && !FrmPrincipal.VersaoDistribuidor)
                        VlrTotal = VlrTotal + decimal.Parse(TabItens.Tables[0].Rows[I]["VLRTOTAL"].ToString());
                }
            }
            return VlrTotal;

        }

        private void GridDados_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {

                Vendas.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                if (e.ColumnIndex != 9 && e.ColumnIndex != 12)
                {
                    if (!ValidadeMovimento())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
        private void GridDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.SeusMov == 0)
            {                
                if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 12)
                {
                    if (GridDados.CurrentRow != null)
                    {
                        Vendas.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                        if ((Vendas.Status == 3 && e.ColumnIndex != 9) && e.ColumnIndex != 12)
                        {
                            MessageBox.Show("Movimento já Entregue", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (Vendas.FormNF.Trim() != "" && e.ColumnIndex != 12)
                        {
                            MessageBox.Show("Nota Fiscal já Emitida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GridDados.CurrentRow.Cells[9].Value = Vendas.ImpNF.ToString();
                            return;
                        }
                        else
                        {
                            if (Vendas.IdVenda > 0)
                            {
                                if (Vendas.IdVdOrigem > 0 && e.ColumnIndex == 12)
                                {
                                    MessageBox.Show("Favor Solicitar alteração na Filial de Origem", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
                                    return;
                                }

                                //if (FrmPrincipal.Perfil_Usuario.CancVenda == 0 && e.ColumnIndex == 12)
                                if (e.ColumnIndex == 12)
                                {
                                    MessageBox.Show("Solicitação não Autorização", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    GridDados.CurrentRow.Cells[12].Value = Vendas.IdVendedor;
                                    return;
                                }

                                //Registrando o Arquivo de Auditoria
                                FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 2, "Filial: " + Vendas.IdFilial.ToString() + " <=> " + GridDados.CurrentRow.Cells[8].Value.ToString() + " Vendedor: " + Vendas.IdVendedor.ToString() + " <=> " + GridDados.CurrentRow.Cells[12].Value.ToString());
                                Vendas.IdFilial = int.Parse(GridDados.CurrentRow.Cells[8].Value.ToString());
                                //Vendas.IdVendedor = int.Parse(GridDados.CurrentRow.Cells[12].Value.ToString());
                                //Controle.ExecutaSQL("Update MvVenda Set Id_Vendedor=" + Vendas.IdVendedor.ToString() + ", Id_Filial=" + Vendas.IdFilial.ToString() + ",ImpNF=" + GridDados.CurrentRow.Cells[9].Value.ToString() + " Where Id_Venda=" + Vendas.IdVenda.ToString());
                                Controle.ExecutaSQL("Update MvVenda Set Id_Filial=" + Vendas.IdFilial.ToString() + ",ImpNF=" + GridDados.CurrentRow.Cells[9].Value.ToString() + " Where Id_Venda=" + Vendas.IdVenda.ToString());
                                // PopularGrid();

                                if (e.ColumnIndex == 9)
                                {
                                    Vendas.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                                    TabelasAux TabAux = new TabelasAux();
                                    TabAux.Controle = Controle;
                                    TabAux.LerTabela("VENDA", Vendas.TpVenda);
                                    if (TabAux.Comissao == 1)
                                    {
                                        SqlDataReader TabComissao = Controle.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                                                         " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + Vendas.IdVenda.ToString());                                    
                                        Controles.Comissao CalcComissao = new Controles.Comissao();
                                        CalcComissao.Controle = Controle;
                                        CadPessoa.LerDados(Vendas.IdPessoa);
                                        decimal PDesconto = 0;
                                        if (Vendas.IgnoraDesc == 0)
                                        {
                                            if ((Vendas.VlrDesconto + Vendas.VlrCredito) > 0)
                                                PDesconto = 100 / (Vendas.VlrSubTotal / (Vendas.VlrDesconto + Vendas.VlrCredito));
                                        }
                                        if (Vendas.Data.Date.Year >= 2019)
                                            CalcComissao.CalcularMovimento2019(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                                        else
                                            CalcComissao.CalcularMovimento(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("Usuário não autorizado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnCopiarVenda_Click(object sender, EventArgs e)
        {
            bool AtlzPrc = false;
            if (GridDados.CurrentRow == null)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
                {
                    MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MvVenda VdOrigem = new MvVenda();  VdOrigem.Controle  = Controle;
                MvVenda VdDestino = new MvVenda(); VdDestino.Controle = Controle;
                VdOrigem.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));

                if (VdOrigem.TpVenda == "OC" && VdOrigem.VinculoVd != "")
                {
                    MessageBox.Show("Orçamento ja Gerou o Pedido No.: " + VdOrigem.VinculoVd.Trim(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (VdOrigem.TpVenda == "PI" && VdOrigem.Status == 1)
                    MessageBox.Show("Pedido ja confirmado. Gerou o Pedido No.: " + VdOrigem.VinculoVd.Trim(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                
                if (MessageBox.Show("Confirma o movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Deseja Atualizar os Preços ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        AtlzPrc = true;

                    CadPessoa.LerDados(VdOrigem.IdPessoa);
                    VdDestino.LerDados(0);
                    
                    // Gravando Nova Venda
                    VdDestino.IdVenda = 0;
                    VdDestino.Data        = DateTime.Now;
                    VdDestino.ImpNF       = VdOrigem.ImpNF;                    
                    VdDestino.IdFilial    = VdOrigem.IdFilial;
                    VdDestino.IdVendedor  = VdOrigem.IdVendedor;
                    VdDestino.IdUsuario   = FrmPrincipal.Perfil_Usuario.IdUsuario;                    
                    VdDestino.IdFormaPgto = VdOrigem.IdFormaPgto;

                    if (VdOrigem.TpVenda == "OC" || VdOrigem.TpVenda == "PI")
                        VdDestino.TpVenda = "PV";
                    else
                        VdDestino.TpVenda = VdOrigem.TpVenda;

                    VdDestino.NumDocumento = "";                    
                    VdDestino.Observacao   = VdOrigem.Observacao;
                    VdDestino.FormNF       = "";
                    VdDestino.Status       = 0;
                    VdDestino.VlrSubTotal  = VdOrigem.VlrSubTotal;
                    VdDestino.VlrDesconto  = VdOrigem.VlrDesconto;
                    VdDestino.VlrCredito   = 0;
                    VdDestino.VlrTotal     = VdOrigem.VlrTotal;
                    VdDestino.ImpNF        = VdOrigem.ImpNF;

                    if (CadPessoa.IdPessoa > 0)
                    {
                        VdDestino.IdPessoa    = CadPessoa.IdPessoa;
                        VdDestino.CnpjCpf     = CadPessoa.Cnpj;
                        VdDestino.NmPessoa    = CadPessoa.RazaoSocial;
                        VdDestino.InscUF      = CadPessoa.InscUF;
                        VdDestino.Cep         = CadPessoa.Cep;
                        VdDestino.Endereco    = CadPessoa.Endereco;
                        VdDestino.Complemento = CadPessoa.Complemento;
                        VdDestino.Numero      = CadPessoa.Numero;
                        VdDestino.Cidade      = CadPessoa.Cidade;
                        VdDestino.Bairro      = CadPessoa.Bairro;
                        VdDestino.IdUF        = CadPessoa.IdUF;
                        VdDestino.Fone        = CadPessoa.Fone;
                        VdDestino.Pais        = CadPessoa.Pais;
                        VdDestino.IdFilial    = CadPessoa.IdFilial;
                    }

                    VdDestino.VinculoVd    = VdOrigem.IdVenda.ToString();                    
                    VdDestino.IdUsuAutDeb  = 0;
                    VdDestino.PrevEntrega  = DateTime.Now;
                    VdDestino.DtHrLanc     = DateTime.Now;
                    VdDestino.IdUltUsuario = FrmPrincipal.Perfil_Usuario.IdUsuario;
                    VdDestino.Faturado     = 0;

                    if (VdOrigem.IdFilialEntrega == 0)
                        VdDestino.IdFilialEntrega = FrmPrincipal.IdFilialConexao;
                    else
                        VdDestino.IdFilialEntrega = VdOrigem.IdFilialEntrega;

                    //Verifica se é uma Reativação ou Novo Cliente
                    VdDestino.CliReativado = VerificaReativacaoCliente(VdDestino.IdPessoa);
                    VdDestino.GravarDados();
                    //
                    FrmPrincipal.RegistrarAuditoria(this.Text, VdDestino.IdVenda, VdDestino.NumDocumento, 1, "Copia da Venda:" + VdOrigem.IdVenda.ToString()+ " Cliente:"+VdOrigem.NmPessoa);

                    CadPessoa.LerDados(VdDestino.IdPessoa);
                    //Incluindo os Itens                    
                    MvVendaItens ItensVdDestino = new MvVendaItens(); ItensVdDestino.Controle = Controle;
                    Produtos CadPrd = new Produtos(); CadPrd.Controle = Controle;
                    DataSet TabItens = new DataSet();
                    TabItens = Controle.ConsultaTabela("SELECT * FROM MvVendaItens WHERE Id_Venda=" + VdOrigem.IdVenda.ToString());
                    decimal VlrSubTotal = 0;
                    for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)                        
                    {
                        //TabItens.Read();
                        CadPrd.LerDados(int.Parse(TabItens.Tables[0].Rows[I]["Id_Produto"].ToString()));

                        ItensVdDestino.IdItem      = 0;
                        ItensVdDestino.IdVenda     = VdDestino.IdVenda;
                        ItensVdDestino.TipoItem    = TabItens.Tables[0].Rows[I]["TipoItem"].ToString();
                        ItensVdDestino.IdProduto   = int.Parse(TabItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                        ItensVdDestino.Qtde        = decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString());
                        ItensVdDestino.VlrUnitario = decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString());

                        int UsuLibPrc = 0;
                        if (TabItens.Tables[0].Rows[I]["Id_UsuLibPrc"].ToString() != "" && VdOrigem.TpVenda == "OC")
                            UsuLibPrc = int.Parse(TabItens.Tables[0].Rows[I]["Id_UsuLibPrc"].ToString());

                        if (AtlzPrc)
                        {
                            if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6)
                                ItensVdDestino.VlrUnitario = CadPrd.PrcAtacado;
                            else if (CadPessoa.Clie_Forn == 7)
                                ItensVdDestino.VlrUnitario = CadPrd.PrcMinimo;
                            else
                                ItensVdDestino.VlrUnitario = CadPrd.PrcSensacional;
                        }
                        else if (UsuLibPrc == 0)
                        {
                            if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6)
                            {
                                if (decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString()) < CadPrd.PrcAtacado)
                                    ItensVdDestino.VlrUnitario = CadPrd.PrcAtacado;
                            }
                            else if (CadPessoa.Clie_Forn == 7)
                            {
                                if (decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString()) < CadPrd.PrcMinimo)
                                    ItensVdDestino.VlrUnitario = CadPrd.PrcMinimo;
                            }
                            else
                            {
                                if (decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString()) < CadPrd.PrcMinimo && TipoMov != "PI")
                                    ItensVdDestino.VlrUnitario = CadPrd.PrcMinimo;
                            }
                        }

                        ItensVdDestino.PComissao      = 0;
                        ItensVdDestino.VlrComissao    = 0;                        
                        ItensVdDestino.PDesconto      = 0;
                        ItensVdDestino.VlrUntComissao = 0;

                        if (int.Parse(TabItens.Tables[0].Rows[I]["Id_UsuLibPrc"].ToString()) == 0)
                        {
                            ItensVdDestino.PrcCusto       = CadPrd.UltPrcCompra;
                            ItensVdDestino.PrcSensacional = CadPrd.PrcSensacional;
                            ItensVdDestino.PrcEspecial    = CadPrd.PrcEspecial;
                            ItensVdDestino.PrcMinimo      = CadPrd.PrcMinimo;
                            ItensVdDestino.PrcVarejo      = CadPrd.PrcVarejo;
                            ItensVdDestino.PrcAtacado     = CadPrd.PrcAtacado;
                        }
                        else
                        {
                            ItensVdDestino.PrcCusto       = CadPrd.UltPrcCompra;
                            ItensVdDestino.PrcSensacional = decimal.Parse(TabItens.Tables[0].Rows[I]["PrcSensacional"].ToString());
                            ItensVdDestino.PrcEspecial    = decimal.Parse(TabItens.Tables[0].Rows[I]["PrcEspecial"].ToString());
                            ItensVdDestino.PrcMinimo      = decimal.Parse(TabItens.Tables[0].Rows[I]["PrcMinimo"].ToString());
                            ItensVdDestino.PrcVarejo      = decimal.Parse(TabItens.Tables[0].Rows[I]["PrcVarejo"].ToString());
                            ItensVdDestino.PrcAtacado     = decimal.Parse(TabItens.Tables[0].Rows[I]["PrcAtacado"].ToString());
                        }                        
                        ItensVdDestino.VlrTotal       = ItensVdDestino.VlrUnitario * ItensVdDestino.Qtde;

                        if (CadPrd.SaldoEstoque <= 0 && ItensVdDestino.TipoItem == "S" && TipoMov!="PI")
                        {
                            MessageBox.Show("Produto:" + CadPrd.Descricao.Trim() + " não tem saldo Suficiente no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ItensVdDestino.Qtde = 0;
                            ItensVdDestino.VlrTotal = ItensVdDestino.VlrUnitario * ItensVdDestino.Qtde;
                        }

                        if (TipoMov == "EMVF")
                        {
                            if (SaldoEstoqueCliente(ItensVdDestino.IdProduto, VdDestino.IdPessoa) == 0)
                                MessageBox.Show("Produto não tem saldo Suficiente ou não informado para esse cliente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                ItensVdDestino.GravarDados();
                        }
                        else
                            ItensVdDestino.GravarDados();                        
                        
                        if (ItensVdDestino.TipoItem == "E")
                           VlrSubTotal  = VlrSubTotal - ItensVdDestino.VlrTotal;
                        else
                            VlrSubTotal = VlrSubTotal + ItensVdDestino.VlrTotal;
                        
                        VdDestino.VlrSubTotal = VlrSubTotal;
                        VdDestino.VlrTotal    = VlrSubTotal - (VdDestino.VlrDesconto + VdDestino.VlrCredito);
                        if (VdDestino.IdVenda > 0)
                        {
                            VdDestino.IdVdMaster = VdOrigem.IdVenda;
                            VdDestino.GravarDados();
                        }
                    }
                    MessageBox.Show("Movimento concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (VdOrigem.TpVenda == "PI")
                    {
                        Controle.ExecutaSQL("Update MvVenda set Status=1,VinculoVD='" + VdDestino.IdVenda.ToString() + " /" + VdDestino.NumDocumento.ToString() + "' WHERE ID_VENDA=" + VdOrigem.IdVenda.ToString());
                        Controle.ExecutaSQL("Update MvVenda set VinculoVD='" + VdOrigem.IdVenda.ToString() + " /" + VdOrigem.NumDocumento.ToString() + "' WHERE ID_VENDA=" + VdDestino.IdVenda.ToString());
                    }
                    else if (VdOrigem.TpVenda == "OC")
                    {
                        Controle.ExecutaSQL("Update MvVenda set VinculoVD='" + VdDestino.NumDocumento.ToString() + "' WHERE ID_VENDA=" + VdOrigem.IdVenda.ToString());
                        Controle.ExecutaSQL("Update MvVenda set VinculoVD='" + VdOrigem.NumDocumento.ToString()  + "' WHERE ID_VENDA=" + VdDestino.IdVenda.ToString());
                    }
                    PopularGrid();
                }
            }
        }
        private void BtnFinalizar_Click(object sender, EventArgs e)
        {
            if (Vendas.Status > 0)
            {
                
                if (MessageBox.Show("Confirma a Finalização da Ordem de Entrega", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_VENDA=" + Vendas.IdVdMaster.ToString());
                    Controle.ExecutaSQL("UPDATE MVVENDA SET FATURADO=1 WHERE ID_VENDA=" + Vendas.IdVdMaster.ToString());
                    DataSet Tabela = new DataSet();
                    Tabela = Controle.ConsultaTabela("SELECT ID_LANC,DATALANC,NUMDOCUMENTO,VENCIMENTO,VLRORIGINAL,DTBAIXA,VLRJURO,VLRMULTA,VLRBAIXA FROM LANCFINANCEIRO WHERE Id_Venda > 0 and Id_Venda=" + Vendas.IdVdMaster.ToString());
                    GridFinanc.DataSource = Tabela;
                    GridFinanc.DataMember = Tabela.Tables[0].TableName;
                    //Registrando Movimento de Auditoria
                    FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 3, "Finalização da Ordem de Entrega");
                    MessageBox.Show("Ordem de entrega finalizada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }


        private bool ValidarPromocao()
        {
            string sSQL = "select T2.TipoCliente,T2.VlrPedido,T2.TipoPromocao,T2.Descricao as DescPromocao,T3.Referencia,t1.Qtde,t2.Id_Produto as IdPrdProm, "+
                          " (select sum(Qtde) from MvVendaItens where Id_Promocao=t1.id_promocao and id_Venda=" + Vendas.IdVenda.ToString()+") as QtdeProm,"+
                          " (select sum(VlrTotal) from MvVendaItens where Id_Promocao=t1.id_promocao and id_Venda=" + Vendas.IdVenda.ToString()+") as TotalItem,T2.DescSegUnd,T1.id_promocao from MvVendaItens t1" +
                          " left join PromocaoProdutos t2 on (t2.Id_Promocao=t1.id_promocao)"+
                          " left join produtos t3 on (t3.id_produto=t1.id_Produto)"+
                          " where t1.id_Promocao > 0 and t1.id_Venda=" + Vendas.IdVenda.ToString();

            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (Tab["TipoCliente"].ToString() != "0")
                    {
                        if (Tab["TipoCliente"].ToString() == "1" && CadPessoa.Clie_Forn != 0)
                        {
                            MessageBox.Show("Verifique o tipo de Cliente da Promoção:"+Tab["DescPromocao"].ToString().Trim()+ " Ref. "+Tab["Referencia"].ToString().Trim(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (Tab["TipoCliente"].ToString() == "2" && (CadPessoa.Clie_Forn != 3 || CadPessoa.Clie_Forn != 6 || CadPessoa.Clie_Forn != 7))
                        {
                            MessageBox.Show("Verifique o tipo de Cliente da Promoção:"+Tab["DescPromocao"].ToString().Trim()+ " Ref. "+Tab["Referencia"].ToString().Trim(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }


                    if (Tab["TipoPromocao"].ToString() == "2")
                    {
                        if ( (Vendas.VlrTotal-decimal.Parse(Tab["TotalItem"].ToString())) < (decimal.Parse(Tab["VlrPedido"].ToString())*decimal.Parse(Tab["QtdeProm"].ToString())))
                        {
                            MessageBox.Show("Valor do Pedido inferior o da Promoção:" + Tab["DescPromocao"].ToString().Trim() + " Ref. " + Tab["Referencia"].ToString().Trim(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }

                    if (Tab["TipoPromocao"].ToString() == "3")
                    {
                        decimal QtdeItem = decimal.Parse(Tab["QtdeProm"].ToString());
                           
                        sSQL = "SELECT * FROM MvVendaItens where id_produto=" + Tab["IdPrdProm"].ToString().Trim() + " and id_Venda=" + Vendas.IdVenda.ToString();

                        Tab = Controle.ConsultaSQL(sSQL);
                        if (Tab.HasRows)
                        {
                            Tab.Read();
                            
                            if (decimal.Parse(Tab["Qtde"].ToString()) < QtdeItem)
                            {
                                MessageBox.Show("Verifique a Quantidade do Produto Base", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            else
                                return true;
                        }
                        else
                        {
                            MessageBox.Show("Poduto base da Promoção nao consta no Pedido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    /*if (int.Parse(Tab["DescSegUnd"].ToString()) == 1)
                    {
                        if (!VerificarDescSegundaUND(int.Parse(Tab["Id_Promocao"].ToString())))
                            return false;
                    }*/
                }
            }
            return true;
        }

        private bool VerificarDescSegundaUND(int IdPromocao)
        {
            SqlDataReader TabQtdeVd = Controle.ConsultaSQL("select Isnull(sum(t1.Qtde),0) as QtdeVd from MvVendaItens t1  " +
                                                        " where t1.Id_Produto in (Select id_produto from PromocaoProdutosItens t2 where t2.Id_Promocao=" + IdPromocao.ToString() + ")" +
                                                        " and t1.Id_Promocao=0 and t1.id_venda=" +  Vendas.IdVenda.ToString());

            decimal QtdeVd = 0;
            while (TabQtdeVd.Read())
                QtdeVd = decimal.Parse(TabQtdeVd["QtdeVd"].ToString());

            if (QtdeVd == 0)
            {
                MessageBox.Show("Produto principal da Promoção não encontrado no Pedido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                SqlDataReader TabQtdeProm = Controle.ConsultaSQL("select Isnull(sum(t1.Qtde),0) as QtdeProm from MvVendaItens t1  " +
                                                        " where t1.Id_Produto in (Select id_produto from PromocaoProdutosItens t2 where t2.Id_Promocao=" + IdPromocao.ToString() + ")" +
                                                        " and t1.Id_Promocao>0 and t1.id_venda=" + Vendas.IdVenda.ToString());

                decimal QtdeProm = 0;
                while (TabQtdeProm.Read())
                    QtdeProm = decimal.Parse(TabQtdeProm["QtdeProm"].ToString());

                if (QtdeVd < QtdeProm)
                {
                    MessageBox.Show("Favor Verificar a Quantidade dos Itens na Promoção", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        private bool VerificaItemQtdeZero()
        {            
            for (int I = 0; I <= GridItens.RowCount - 1; I++)
            {
                if (decimal.Parse(GridItens.Rows[I].Cells[4].Value.ToString()) <= 0)
                {
                    MessageBox.Show("Verificar a Quantidade Informada no Produto: " + GridItens.Rows[I].Cells[2].Value.ToString().Trim() + " - " + GridItens.Rows[I].Cells[3].Value.ToString().Trim(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }                    
            }
            return false;
        }
        private bool VerificaItemInativo()
        {
            string sSQL = "SELECT T2.REFERENCIA,T2.DESCRICAO FROM MVVENDAITENS T1  " +
                          " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.TIPOITEM='S' AND T2.ATIVO=0 AND T1.ID_VENDA=" + Vendas.IdVenda.ToString();
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    MessageBox.Show("Produto Inativo:" + Tab["Referencia"].ToString().Trim() + " - " + Tab["Descricao"].ToString().Trim() + " ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;

        }
        private decimal VerificaDescGrupoPrd(int IdPrd)
        {
            string sSQL = "SELECT T2.PercVerDesc FROM Produtos T1  " +
                          " LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO) WHERE T1.ID_PRODUTO=" + IdPrd.ToString();
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    return decimal.Parse(Tab["PercVerDesc"].ToString());
                }
            }
            return decimal.Parse(Tab["0,0"].ToString());

        }
        private bool VerificarSaldoItens(int IdVenda)
        {            
            string sSQL = "SELECT T2.REFERENCIA,T2.DESCRICAO,T2.PRODUTOKIT,T1.ID_PRODUTO,T1.QTDE FROM MVVENDAITENS T1  "+
                          " LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) "+
                          " LEFT JOIN GRUPOPRODUTO T3 ON (T3.ID_GRUPO=T2.ID_GRUPO) "+
                          " WHERE T1.TIPOITEM='S' AND T3.ESTOQUE=0" +
                          " AND (T2.SALDOESTOQUE <= 0 OR T2.SALDOESTOQUE < T1.QTDE)" +
                          " AND T1.ID_VENDA=" + IdVenda.ToString() + " AND NOT EXISTS (SELECT * FROM LIBERACAOPRODUTO T4 WHERE T4.ID_VENDA=T1.ID_VENDA AND T4.ID_PRODUTO=T1.ID_PRODUTO)";
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);

            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                   /*/ if (Vendas.IdFilialEntrega != FrmPrincipal.IdFilialConexao)
                    {
                        SaldoPrd = BuscaSldFilialEntrega(Vendas.IdFilialEntrega, CadProd.Referencia);
                        msg = "Filial de Entrega \n";
                    }
                    else
                        SaldoPrd = SaldoProduto(CadProd.IdProduto);


                    if (!FrmPrincipal.VersaoDistribuidor && IsConnected() && Vendas.IdFilialEntrega == 2)
                        SaldoDeposito = SaldoProdutoDeposito(CadProd.Referencia);

                    if (SaldoPrd < Qtde && (SaldoPrd + SaldoDeposito >= Qtde))
                    {
                        if (MessageBox.Show("Quantidade Dispónivel no Depósito, deseja fazer a liberação automatica ? ", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            SetaLiberacaoProduto(Vendas.IdFilialEntrega, 0, Vendas.IdVenda, CadProd.Referencia, SaldoPrd);
                        else
                            SaldoDeposito = 0;
                    }*/


                    if (int.Parse(Tab["PRODUTOKIT"].ToString()) == 1)
                    {
                        if (SaldoProduto(int.Parse(Tab["ID_PRODUTO"].ToString())) < decimal.Parse(Tab["QTDE"].ToString()))
                        {
                            MessageBox.Show("Produto:" + Tab["Referencia"].ToString().Trim() + " - " + Tab["Descricao"].ToString().Trim() + " não tem saldo Suficiente no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Produto:" + Tab["Referencia"].ToString().Trim() + " - " + Tab["Descricao"].ToString().Trim() + " não tem saldo Suficiente no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                return true;
            }
            else
                return true;
        }

        private bool VerificarSaldoPrdCliente(int IdVenda)
        {
            SqlDataReader TabSldCliente;
            TabSldCliente = Controle.ConsultaSQL("SELECT T4.REFERENCIA,T4.DESCRICAO FROM MVVENDAITENS T1  LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                                              " left JOIN SaldoPrdCliente T3 ON (T3.Id_Pessoa=T2.Id_Pessoa AND T3.Id_Produto=T1.Id_Produto)" +
                                              " left join produtos t4 on (t4.id_produto=t1.id_produto)" +
                                              " WHERE T3.Saldo < T1.Qtde AND T1.Id_Venda=" + IdVenda.ToString());
            if (TabSldCliente.HasRows)
            {
                TabSldCliente.Read();
                MessageBox.Show("Atenção: Cliente não tem Saldo do Produto: " + TabSldCliente["REFERENCIA"].ToString().Trim() + " - " + TabSldCliente["DESCRICAO"].ToString().Trim(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
                return true;
        }
        private decimal SaldoProduto(int IdPrd)
        {
            SqlDataReader Tab = Controle.ConsultaSQL("SELECT CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1  LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)" +
                                                     " WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado FROM Produtos T1 WHERE T1.ID_PRODUTO=" + IdPrd.ToString());
            if (Tab.HasRows)
            {
                Tab.Read();                
                return decimal.Parse(Tab["SALDOESTOQUE"].ToString());
            }
            else
                return 0;
        }

        private decimal SaldoProdutoDeposito(string Ref)
        {
            Controles.Serv_SaldoEstoque.ConsultaSaldo wsSaldo = new Controles.Serv_SaldoEstoque.ConsultaSaldo();
            decimal Saldo = 0;
            try
            {
                Saldo = 0;
                wsSaldo.Url = "http://177.104.127.50/WSSaldoEstoque/BuscaSaldoEstoque.asmx?swdl";
                Saldo = wsSaldo.SaldoEstoque(Ref, FrmPrincipal.URLMatriz,"");                
                wsSaldo.Dispose();
            }
            catch
            {
                wsSaldo.Dispose();
            }
            return Saldo;
        }
        private bool VerificarTroca(int IdVenda)
        {
            MvVenda TrocaVenda  = new MvVenda();
            TrocaVenda.Controle = Controle;
            TrocaVenda.LerDados(Vendas.IdVenda);
            if (TrocaVenda.IdVdTroca == 0)
            {
                MessageBox.Show("Atenção: Favor informar a Venda e Origem da troca", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            MvVenda VdOrigem  = new MvVenda();
            VdOrigem.Controle = Controle;
            VdOrigem.LerDados(TrocaVenda.IdVdTroca);
            if (TrocaVenda.IdPessoa != VdOrigem.IdPessoa)
            {
                MessageBox.Show("Atenção: Cliente informado na troca diferente da Venda origem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (VdOrigem.Status==4)
            {
                MessageBox.Show("Atenção: Venda de Origem esta Cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (VdOrigem.Status != 3)
            {
                MessageBox.Show("Atenção: Venda de Origem não Entregue", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (VdOrigem.TpVenda != "PV" && VdOrigem.TpVenda != "CO" && VdOrigem.TpVenda != "EMVF")
            {
                MessageBox.Show("Atenção: Numero da Venda de Origem, não é um pedido de venda ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificando os itens
            SqlDataReader Tab = Controle.ConsultaSQL("SELECT T2.REFERENCIA,T2.DESCRICAO FROM MVVENDAITENS T1  LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                                                     " WHERE T1.TIPOITEM='E' AND T1.ID_VENDA=" + IdVenda.ToString() + " AND NOT EXISTS (SELECT * FROM MVVENDAITENS T3 " +
                                                     " WHERE T3.ID_PRODUTO=T1.ID_PRODUTO AND T3.ID_VENDA=" + TrocaVenda.IdVdTroca.ToString() + ")");
            if (Tab.HasRows)
            {
                while (Tab.Read())
                    MessageBox.Show("Atenção: Produto: " + Tab["REFERENCIA"].ToString().Trim() + " - " + Tab["DESCRICAO"].ToString().Trim() + " não localizado na Venda Origem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Tab = Controle.ConsultaSQL("SELECT T2.REFERENCIA,T2.DESCRICAO FROM MVVENDAITENS T1  LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                                       " WHERE T1.TIPOITEM='E' AND T1.ID_VENDA=" + IdVenda.ToString() + 
                                       " AND T1.QTDE > (SELECT TOP 1 T3.QTDE FROM MVVENDAITENS T3 WHERE T3.ID_PRODUTO=T1.ID_PRODUTO AND T3.ID_VENDA=" + TrocaVenda.IdVdTroca.ToString() + ")");

            if (Tab.HasRows)
            {
                while (Tab.Read())
                    MessageBox.Show("Atenção: Quantidade maior que informada na Venda Origem, Produto: " + Tab["REFERENCIA"].ToString().Trim() + " - " + Tab["DESCRICAO"].ToString().Trim() , "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (VdOrigem.TpVenda == "PV")
            {
                Tab = Controle.ConsultaSQL("SELECT T2.REFERENCIA,T2.DESCRICAO FROM MVVENDAITENS T1  LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                                           " WHERE T1.TIPOITEM='E' AND T1.ID_VENDA=" + IdVenda.ToString() +
                                           " AND T1.VLRUNITARIO <> (SELECT TOP 1 T3.VLRUNITARIO FROM MVVENDAITENS T3 WHERE T3.ID_PRODUTO=T1.ID_PRODUTO AND T3.ID_VENDA=" + TrocaVenda.IdVdTroca.ToString() + ")");
                if (Tab.HasRows)
                {
                    while (Tab.Read())
                        MessageBox.Show("Atenção:Valor do Produto: " + Tab["REFERENCIA"].ToString().Trim() + " - " + Tab["DESCRICAO"].ToString().Trim() + " diferente da venda origem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }
        private bool VerificarStatus()
        {
            MvVenda StaVenda = new MvVenda();
            StaVenda.Controle = Controle;
            StaVenda.LerDados(Vendas.IdVenda);

            if (StaVenda.Status == 1)
            {
                MessageBox.Show("Movimento já Confirmado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Vendas.LerDados(Vendas.IdVenda);
                Hab_Botoes();
                return false;
            }
            else if (StaVenda.Status == 2)
            {
                MessageBox.Show("Movimento foi Faturado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Vendas.LerDados(Vendas.IdVenda);
                Hab_Botoes();
                return false;
            }
            else if (StaVenda.Status == 3)
            {
                MessageBox.Show("Movimento foi Entregue", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Vendas.LerDados(Vendas.IdVenda);
                Hab_Botoes();
                return false;
            }
            else
                return true;

        }        
        private bool ValidadeMovimento()
        {
            /*TimeSpan Dias = DateTime.Now.Subtract(Vendas.Data); //FrmPrincipal.DataServidor.Subtract(Vendas.Data);
            if (Dias.Days > 5 && Vendas.TpVenda!="OC")
            {
                MessageBox.Show("Validade do movimento é apenas de 5 dias", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else*/
                return true;
        }
        private void BtnImpOE_Click(object sender, EventArgs e)
        {
            if (Vendas.IdPessoa == 0)
                MessageBox.Show("Favor informar o cliente da venda", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (VerificarStatus())
            {
                if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
                {
                    MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                FrmVinculoOE FrmOrdem = new FrmVinculoOE();
                FrmOrdem.FrmPrincipal = FrmPrincipal;
                FrmOrdem.IdPessoa     = Vendas.IdPessoa;
                FrmOrdem.IdPV         = Vendas.IdVenda;
                FrmOrdem.ShowDialog();
                PopularGridItens();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Gerando as datas na tabela data
           /* DateTime dt = DateTime.Now;
            int Reg=1;
            dt = DateTime.Parse("01/01/2016");
            while (dt.Date <= DateTime.Parse("31/12/2025").Date)
            {
                if (dt.DayOfWeek != DayOfWeek.Sunday)
                   Controle.ExecutaSQL("Insert into Datas (Id_Lanc,Data,Ativo) values (" + Reg.ToString() + ",Convert(DateTime,'" + dt.ToShortDateString() + "',103),1)");
                Reg = Reg + 1;
                dt=dt.AddDays(1);
            }
            MessageBox.Show("Fim");
            return;*/

            string msg = "0003/001";            
            //Dados.Disconnect();            
            //return;
            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
            ControleEstoque.Controle = Controle;

            Controles.ImpressoraFiscal ImpFiscal = new ImpressoraFiscal();
            ImpFiscal.Controle = Controle;

            TabelasAux TabAux = new TabelasAux();
            TabAux.Controle = Controle;

            MvVenda Vd = new MvVenda();
            Vd.Controle = Controle;
            button1.Text = GridDados.Rows.Count.ToString();

            int NumVd = int.Parse(GridDados.Rows[0].Cells[0].Value.ToString());
            Vd.LerDados(NumVd);

            TabAux.LerTabela("VENDA", Vd.TpVenda);

            SqlDataReader TabItens = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE Id_Venda=" + Vd.IdVenda.ToString());
            /*ControleEstoque.EstoqueCliente(TabItens, 1, 1, Vd.IdPessoa);
            return;*/
            //
            //SqlDataReader Tabela;
            //Tabela = Controle.ConsultaSQL("SELECT T2.DATA AS DTCAIXA,T1.*FROM MVVENDA T1 LEFT JOIN CAIXABALCAO T2 ON (T2.ID_CAIXA=T1.ID_CAIXA)" +
             //                             " WHERE T2.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T2.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ORDER BY T2.DATA");
           /* DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T2.DATA AS DTCAIXA,T1.* FROM MVVENDA T1 LEFT JOIN CAIXABALCAO T2 ON (T2.ID_CAIXA=T1.ID_CAIXA)" +
                                             "  WHERE T1.VLRTOTAL > 0 AND T1.TPVENDA='PV' AND T1.DATACF >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATACF <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ORDER BY T1.DATACF");
            button1.Text = Tabela.Tables[0].Rows.Count.ToString();
            for (int I = 0; I <= Tabela.Tables[0].Rows.Count - 1; I++)
            {
                
                if (int.Parse(Tabela.Tables[0].Rows[I]["Id_LANCCF"].ToString().Trim()) > 0)
                {
                    if (int.Parse(Tabela.Tables[0].Rows[I]["Status"].ToString()) == 3)
                    {
                        ImpFiscal.RegistrarCupom(DateTime.Parse(Tabela.Tables[0].Rows[I]["DATACF"].ToString()), int.Parse(Tabela.Tables[0].Rows[I]["Id_Venda"].ToString()), int.Parse(Tabela.Tables[0].Rows[I]["FormNF"].ToString().Replace("ECF-", "").ToString()), decimal.Parse(Tabela.Tables[0].Rows[I]["VlrTotal"].ToString()), decimal.Parse(Tabela.Tables[0].Rows[I]["VlrDesconto"].ToString()) + decimal.Parse(Tabela.Tables[0].Rows[I]["Credito"].ToString()));
                    }
                }
                button1.Text = Tabela.Tables[0].Rows.Count.ToString() + "-" + I.ToString();
                button1.Update();
                Application.DoEvents();
            }*/
            Vd = new MvVenda();
            Vd.Controle = Controle;
            button1.Text = GridDados.Rows.Count.ToString();
            //Atualizar Estoque
            for (int I = 0; I <= GridDados.RowCount - 1; I++)
            {
                button1.Text = GridDados.Rows.Count.ToString() + "-" + I.ToString(); ;
                button1.Update();
                Application.DoEvents();
                NumVd = int.Parse(GridDados.Rows[I].Cells[0].Value.ToString());
                Vd.LerDados(NumVd);
                //Atualizar Estoque
                if (Vd.Status >= 1 && Vd.Status < 4)
                {
                    TabAux.LerTabela("VENDA", Vd.TpVenda);
                    if (TabAux.Estoque > 0)
                    {
                        SqlDataReader TabSaida = Controle.ConsultaSQL("SELECT *, '' AS NCM FROM MvVendaItens WHERE TipoItem='S' and Id_Venda=" + Vd.IdVenda.ToString());
                        if (TabSaida.HasRows)
                        {
                            ControleEstoque.MovimentoEstoque(TabSaida, TabAux.Estoque, 1, false, Vd.TpVenda, Vd.Data, 0);
                            if (Vd.TpVenda == "TROCA" && Vd.Status == 3)
                            {
                                SqlDataReader TabEntrada = Controle.ConsultaSQL("SELECT *,'' AS NCM FROM MvVendaItens WHERE TipoItem='E' and Id_Venda=" + Vd.IdVenda.ToString());
                                if (TabEntrada.HasRows)
                                    ControleEstoque.MovimentoEstoque(TabEntrada, 1, 1, false, Vd.TpVenda, Vd.Data, 0);
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Fim");
        }
        private void ImprimirResumido(string FormaPgto)
        {
            try
            {                
                if (Vendas.TpVenda == "PV" || Vendas.TpVenda == "VF")
                {
                    DataSet Parcelas = new DataSet();
                    Parcelas = Controle.ConsultaTabela("SELECT T1.VENCIMENTO,T1.VLRORIGINAL,T2.DOCUMENTO FROM LancFinanceiro T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString());

                    for (int I = 0; I <= Parcelas.Tables[0].Rows.Count - 1; I++)
                    {
                        DateTime Dt = DateTime.Parse(Parcelas.Tables[0].Rows[I]["Vencimento"].ToString());
                        FormaPgto = FormaPgto + Dt.Date.ToShortDateString() + "   R$" + string.Format("{0:N2}", decimal.Parse(Parcelas.Tables[0].Rows[I]["VlrOriginal"].ToString())) + "   " + Parcelas.Tables[0].Rows[I]["Documento"].ToString();
                    }
                }
                Filiais CadFilial = new Filiais();
                CadFilial.Controle=Controle;
                CadFilial.LerDados(Vendas.IdFilial);

                DataSet TabItens = new DataSet();
                TabItens = Controle.ConsultaTabela(Vendas.SqlRelatorio(Vendas.IdVenda));

                bool ImpCab = true;
                ImprimeTexto ImpTxt = new ImprimeTexto();
                ImpTxt.Inicio(FrmPrincipal.PortaImpResumida);                                          
                //ImpTxt.Inicio("LPT1");
                string TipoItem = "";

                int Lin = 0;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (ImpCab)
                    {
                       // ImpTxt.ImpLF(ImpTxt.Normal + ImpTxt.NegritoOff + Controle.Space(CadFilial.Filial.Trim(), 40) + "   CNPJ:" + CadFilial.Cnpj.Trim());
                       // ImpTxt.ImpLF(ImpTxt.Comprimido + ImpTxt.NegritoOff + Controle.Space(CadFilial.Endereco.Trim() + "," + CadFilial.Numero, 50) + "   Fone:" + Controle.Space(CadFilial.Fone1.Trim(), 10));
                       // ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                        ImpTxt.ImpLF(ImpTxt.Normal + ImpTxt.NegritoOff + "Data: " + DateTime.Parse(TabItens.Tables[0].Rows[I]["Data"].ToString()).ToShortDateString() + " Doc.VD: " + TabItens.Tables[0].Rows[I]["NumDocumento"].ToString().Trim() + "/"+string.Format("{0:D6}",int.Parse(TabItens.Tables[0].Rows[I]["Id_Venda"].ToString()))+"   " + TabItens.Tables[0].Rows[I]["Movimento"].ToString().Trim() + "   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Cliente.: " + TabItens.Tables[0].Rows[I]["Id_Pessoa"].ToString().Trim() + " - " + Controle.Space(TabItens.Tables[0].Rows[I]["Fantasia"].ToString().Trim(), 70) + " / " + Controle.Space(TabItens.Tables[0].Rows[I]["Pessoa"].ToString().Trim(), 60));
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Endereco: " + Controle.Space(TabItens.Tables[0].Rows[I]["Endereco"].ToString().Trim() + " No.: " + TabItens.Tables[0].Rows[I]["Numero"].ToString(), 100));
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "CEP.....: " + TabItens.Tables[0].Rows[I]["CEP"].ToString().Trim() + "    Bairro:" + TabItens.Tables[0].Rows[I]["Bairro"].ToString() + " CIDADE: " + Controle.Space(TabItens.Tables[0].Rows[I]["CIDADE"].ToString(), 30) + " UF:" + TabItens.Tables[0].Rows[I]["UF"].ToString());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "CNPJ/CPF: " + TabItens.Tables[0].Rows[I]["CNPJCPF"].ToString() + " Insc.Estadual:" + TabItens.Tables[0].Rows[I]["InscUF"].ToString());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Cod.   Referencia  Produto                                                                    Qtde. Und.         Vlr.Unit.   Vlr.Total");
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                        ImpCab = false;
                    }
                    if (TipoItem != TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim())
                    {
                        if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "S")
                        {
                            ImpTxt.ImpLF("*** Saida ***");
                            Lin = Lin + 1;
                        }
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "E")
                        {
                            ImpTxt.ImpLF("*** DEVOLUÇÃO ***");
                            Lin = Lin + 1;
                        }
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "N")
                        {
                            ImpTxt.ImpLF("*** SEM MOVIMENTO ***");
                            Lin = Lin + 1;
                        }
                    }
                    string Descricao = TabItens.Tables[0].Rows[I]["Descricao"].ToString().Trim().Replace("ç", "c").Replace("Ç", "C").Replace("á", "a").Replace("Á", "A").Replace("ã", "a").Replace("Â", "A").Replace("õ", "o").Replace("Õ", "O").Replace("é", "e").Replace("É", "E");
                    ImpTxt.ImpLF(string.Format("{0:D6}", int.Parse(TabItens.Tables[0].Rows[I]["Id_Produto"].ToString())) + "  " + Controle.Space(TabItens.Tables[0].Rows[I]["Referencia"].ToString(), 10) + " " + Controle.Space(Descricao, 70) + "  " +
                                 Controle.NumSpace(string.Format("{0:N3}", decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString())).ToString(), 8) + "  " + Controle.Space(TabItens.Tables[0].Rows[I]["Unidade"].ToString(), 5) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString())).ToString(), 12) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["TotalItem"].ToString())).ToString(), 12));
                    Lin = Lin + 1;
                    TipoItem = TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim();

                    if (Lin > 14)
                    {
                        ImpCab = true;
                        Lin = 0;
                        for (int L = 1; L <= 12; L++)
                            ImpTxt.ImpLF("");                        
                    }
                }
                for (int I = Lin; I <= 14; I++)
                {
                    ImpTxt.ImpLF("");
                }
                ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                ImpTxt.ImpLF("Vendedor: " + Controle.Space(TabItens.Tables[0].Rows[0]["Vendedor"].ToString().Trim(), 20) + " Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 20) + "   " + Controle.Space(FormaPgto, 38) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 12));
                //ImpTxt.ImpLF("Vendedor: " + Controle.Space(TabItens.Tables[0].Rows[0]["Vendedor"].ToString().Trim(), 20) + " Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 20) + "   " + Controle.Space(TabItens.Tables[0].Rows[0]["PrazoPgto"].ToString().Trim(), 20) + Controle.Space(" ", 18) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 12));
                ImpTxt.ImpLF(Controle.Space("Obs: " + Controle.Space(TabItens.Tables[0].Rows[0]["Observacao"].ToString().Trim(), 95),104) + " (-) Desconto  R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrDesconto"].ToString())).ToString(), 12));
                ImpTxt.ImpLF(Controle.Space(" ", 104) + "                  -------------");
                ImpTxt.ImpLF(Controle.Space(" ", 104) + " (=) Total     R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrTotal"].ToString())).ToString(), 12));                
                ImpTxt.ImpLF(" ");
                ImpTxt.ImpLF("Recebido em:______/_____/________                                                  _______________________");
                ImpTxt.ImpLF("                                                                                          Comprador");
                ImpTxt.ImpLF(ImpTxt.Comprimido + "Documento sem valor Fiscal");
                ImpTxt.Fim();
            }
            catch
            { 
            }
            /*FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelVdResumida RelVenda = new Relatorios.RelVdResumida();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(Vendas.SqlRelatorio(Vendas.IdVenda));
            RelVenda.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelVenda;
            RelVenda.Section1.Height = 180;
            
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section2.ReportObjects["LblFilial"])).Text    = LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section5.ReportObjects["LblRodaPe"])).Text    = FrmPrincipal.Rel_RodaPe;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section5.ReportObjects["LblFormaPgto"])).Text = FormaPgto;
            ((CrystalDecisions.CrystalReports.Engine.PrintOptions)(RelVenda.PrintOptions)).ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(600, 600,600,600));                                       
            //FrmRel.cryRepRelatorio.PrintReport();
            FrmRel.ShowDialog();*/
            
            if (Vendas.Status == 2 && Vendas.VdImpFat == 0)
            {
                Vendas.VdImpFat = 1;
                Vendas.GravarDados();
            }

        }
        private void ImpMiniImpBematech(int Modelo)
        {
            //try
            {
                DataSet TabItens = new DataSet();
                TabItens = Controle.ConsultaTabela(Vendas.SqlRelatorio(Vendas.IdVenda));

                bool ImpCab = true;
                Controles.ImpressoraFiscal MiniImp = new ImpressoraFiscal();                
                string TipoItem = "";

                StringBuilder ImpNota = new StringBuilder();
                
                int Lin = 0;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (ImpCab)
                    {
                        ImpNota.AppendLine("Data:" + DateTime.Parse(TabItens.Tables[0].Rows[I]["Data"].ToString()).ToShortDateString() + " Doc.: " + TabItens.Tables[0].Rows[I]["NumDocumento"].ToString().Trim() + "/" + string.Format("{0:D6}", int.Parse(TabItens.Tables[0].Rows[I]["Id_Venda"].ToString())) + "    " + TabItens.Tables[0].Rows[I]["Movimento"].ToString().Trim());
                        ImpNota.AppendLine("Cliente.: " + Controle.Space(TabItens.Tables[0].Rows[I]["Id_Pessoa"].ToString().Trim() + "-" + TabItens.Tables[0].Rows[I]["Pessoa"].ToString().Trim(), 55));
                        ImpNota.AppendLine("Endereco: " + Controle.Space(TabItens.Tables[0].Rows[I]["Endereco"].ToString().Trim() + " No.: " + TabItens.Tables[0].Rows[I]["Numero"].ToString(), 55));
                        ImpNota.AppendLine("CNPJ/CPF: " + TabItens.Tables[0].Rows[I]["CNPJCPF"].ToString() + " Insc.Estadual:" + TabItens.Tables[0].Rows[I]["InscUF"].ToString());
                        ImpNota.AppendLine(" ");
                        ImpNota.AppendLine("Ref.  Produto                          Qtde.   Vr.Unit.  Vr.Total");
                        ImpNota.AppendLine("-----------------------------------------------------------------");
                        ImpCab = false;
                    }
                    if (TipoItem != TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim())
                    {
                        if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "S")
                            ImpNota.AppendLine("*** Saida ***");
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "E")
                            ImpNota.AppendLine("*** DEVOLUÇÃO ***");
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "N")
                            ImpNota.AppendLine("*** SEM MOVIMENTO ***");
                    }
                    string Descricao = TabItens.Tables[0].Rows[I]["Descricao"].ToString().Trim().Replace("ç", "c").Replace("Ç", "C").Replace("á", "a").Replace("Á", "A").Replace("ã", "a").Replace("Â", "A").Replace("õ", "o").Replace("Õ", "O").Replace("é", "e").Replace("É", "E");
                    ImpNota.AppendLine(Controle.Space(TabItens.Tables[0].Rows[I]["Referencia"].ToString(), 8) + " " + Controle.Space(Descricao, 25) + "    " +
                                 Controle.NumSpace(string.Format("{0:N3}", decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString())).ToString(), 6) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString())).ToString(), 8) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["TotalItem"].ToString())).ToString(), 8));
                    TipoItem = TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim();
                }
                ImpNota.AppendLine("-----------------------------------------------------------------");
                ImpNota.AppendLine("Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 24) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 10));
                ImpNota.AppendLine("                                     (-) Desconto  R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrDesconto"].ToString())).ToString(), 10));
                ImpNota.AppendLine("                                                      -----------");
                ImpNota.AppendLine("                                     (=) Total     R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrTotal"].ToString())).ToString(), 10));
                ImpNota.AppendLine("-----------------------------------------------------------------");
                ImpNota.AppendLine("Documento sem valor Fiscal");
                MiniImp.ImpMiniBemacth(ImpNota.ToString(), FrmPrincipal.PortaImpResumida, Modelo);                
            }
            //catch
            {
            }
        }

        /*private void ImpMiniImpBematech22(int Modelo)
        {
            //try
            {
                DataSet TabItens = new DataSet();
                TabItens = Controle.ConsultaTabela(Vendas.SqlRelatorio(Vendas.IdVenda));

                bool ImpCab = true;
                Controles.ImpressoraFiscal MiniImp = new ImpressoraFiscal();
                string TipoItem = "";

                int Lin = 0;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (ImpCab)
                    {
                        MiniImp.ImpMiniBemacth("Data:" + DateTime.Parse(TabItens.Tables[0].Rows[I]["Data"].ToString()).ToShortDateString() + " Doc.: " + TabItens.Tables[0].Rows[I]["NumDocumento"].ToString().Trim() + "/" + string.Format("{0:D6}", int.Parse(TabItens.Tables[0].Rows[I]["Id_Venda"].ToString())) + "    " + TabItens.Tables[0].Rows[I]["Movimento"].ToString().Trim(), FrmPrincipal.PortaImpResumida, Modelo);
                        MiniImp.ImpMiniBemacth("Cliente.: " + Controle.Space(TabItens.Tables[0].Rows[I]["Id_Pessoa"].ToString().Trim() + "-" + TabItens.Tables[0].Rows[I]["Pessoa"].ToString().Trim(), 55), FrmPrincipal.PortaImpResumida, Modelo);
                        MiniImp.ImpMiniBemacth("Endereco: " + Controle.Space(TabItens.Tables[0].Rows[I]["Endereco"].ToString().Trim() + " No.: " + TabItens.Tables[0].Rows[I]["Numero"].ToString(), 55), FrmPrincipal.PortaImpResumida, Modelo);
                        MiniImp.ImpMiniBemacth("CNPJ/CPF: " + TabItens.Tables[0].Rows[I]["CNPJCPF"].ToString() + " Insc.Estadual:" + TabItens.Tables[0].Rows[I]["InscUF"].ToString(), FrmPrincipal.PortaImpResumida, Modelo);
                        MiniImp.ImpMiniBemacth(" ", FrmPrincipal.PortaImpResumida, Modelo);
                        MiniImp.ImpMiniBemacth("Ref.  Produto                          Qtde.   Vr.Unit.  Vr.Total", FrmPrincipal.PortaImpResumida, Modelo);
                        MiniImp.ImpMiniBemacth("-----------------------------------------------------------------", FrmPrincipal.PortaImpResumida, Modelo);
                        ImpCab = false;
                    }
                    if (TipoItem != TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim())
                    {
                        if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "S")
                            MiniImp.ImpMiniBemacth("*** Saida ***", FrmPrincipal.PortaImpResumida, Modelo);
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "E")
                            MiniImp.ImpMiniBemacth("*** DEVOLUÇÃO ***", FrmPrincipal.PortaImpResumida, Modelo);
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "N")
                            MiniImp.ImpMiniBemacth("*** SEM MOVIMENTO ***", FrmPrincipal.PortaImpResumida, Modelo);
                    }
                    string Descricao = TabItens.Tables[0].Rows[I]["Descricao"].ToString().Trim().Replace("ç", "c").Replace("Ç", "C").Replace("á", "a").Replace("Á", "A").Replace("ã", "a").Replace("Â", "A").Replace("õ", "o").Replace("Õ", "O").Replace("é", "e").Replace("É", "E");
                    MiniImp.ImpMiniBemacth(Controle.Space(TabItens.Tables[0].Rows[I]["Referencia"].ToString(), 8) + " " + Controle.Space(Descricao, 25) + "    " +
                                 Controle.NumSpace(string.Format("{0:N3}", decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString())).ToString(), 6) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString())).ToString(), 8) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["TotalItem"].ToString())).ToString(), 8), FrmPrincipal.PortaImpResumida, Modelo);
                    TipoItem = TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim();
                }
                MiniImp.ImpMiniBemacth("-----------------------------------------------------------------", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 24) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 10), FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("                                     (-) Desconto  R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrDesconto"].ToString())).ToString(), 10), FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("                                                      -----------", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("                                     (=) Total     R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrTotal"].ToString())).ToString(), 10), FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("-----------------------------------------------------------------", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("Documento sem valor Fiscal", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, Modelo);
            }
            //catch
            {
            }
        } */     
               
        private void ImpNormal(string FormaPgto, bool ImpResumido)
        {
            Filiais Filial  = new Filiais();
            Filial.Controle = Controle;
            Filial.LerDados(Vendas.IdFilial);

            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelVendas RelVenda = new Relatorios.RelVendas();
            
            if (ImpResumido)
            {               
                RelVenda.Section5.SectionFormat.EnableSuppress = true;
                RelVenda.Refresh();
            }
            else
                RelVenda.Section4.SectionFormat.EnableSuppress = true;
            
            /*System.Drawing.Printing.PrinterSettings printersettings = new System.Drawing.Printing.PrinterSettings();
            printersettings.DefaultPageSettings.Landscape = false;
            System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
            pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("ImpTeste", 148, 210);
            RelVenda.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
            RelVenda.PrintToPrinter(printersettings, pageSettings, false);
            RelVenda.Section5.SectionFormat.EnableSuppress = true;
            RelVenda.Refresh();*/

            DataSet TabRel = new DataSet();
            DataSet TabKit = new DataSet();
            TabRel = Controle.ConsultaTabela(Vendas.SqlRelatorio(Vendas.IdVenda));
            TabKit = Controle.ConsultaTabela(" select t1.Id_PrdMaster,t2.Referencia,t2.Descricao,t1.Qtde,T2.Unidade from ProdutosKit t1 left join Produtos t2 on (t2.Id_Produto=t1.Id_Produto)");
            //RelVenda.SetDataSource(TabRel.Tables[0]);
            RelVenda.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
            RelVenda.Database.Tables[1].SetDataSource(TabKit.Tables[0]);                
            FrmRel.cryRepRelatorio.ReportSource = RelVenda;            
            //RelVenda.Section1.Height = 180;
           
            if (Vendas.TpVenda == "OC")
            {
                if (MessageBox.Show("Imprimir em papel timbrado ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RelVenda.Section1.Height = 2500;                    
                }
                ((CrystalDecisions.CrystalReports.Engine.Section)(RelVenda.ReportDefinition.Sections["DetailSection1"])).SectionFormat.EnableSuppress = true;
            }
            RelVenda.ParameterFields[0].CurrentValues.AddValue(ImpResumido);
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section2.ReportObjects["LblFilial"])).Text    = Filial.Fantasia;
            //((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section5.ReportObjects["LblRodaPe"])).Text    = FrmPrincipal.Rel_RodaPe;            
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section5.ReportObjects["LblFormaPgto"])).Text = FormaPgto;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelVenda.Section5.ReportObjects["LblFormaPgto2"])).Text = FormaPgto;
            FrmRel.ShowDialog();
            

            if (Vendas.Status == 2)
            {
                FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 9, "Solicitação de Impressão do Pedido");
                if (Vendas.VdImpFat == 0)
                {
                    Vendas.VdImpFat = 1;
                    Vendas.GravarDados();
                }
            }
                
        }                        
        private void button2_Click(object sender, EventArgs e)
        {
            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
            ControleEstoque.Controle = Controle;

            Controles.ImpressoraFiscal ImpFiscal = new ImpressoraFiscal();
            ImpFiscal.Controle = Controle;

            TabelasAux TabAux = new TabelasAux();
            TabAux.Controle = Controle;

            MvVenda Vd = new MvVenda();
            Vd.Controle = Controle;
            button2.Text = GridDados.Rows.Count.ToString();
            //Atualizar Estoque
            for (int I = 0; I <= GridDados.RowCount - 1; I++)
            {
                button2.Text = GridDados.Rows.Count.ToString() + "-" + I.ToString(); ;
                button2.Update();
                Application.DoEvents();
                int NumVd = int.Parse(GridDados.Rows[I].Cells[0].Value.ToString());
                Vd.LerDados(NumVd);
                CadPessoa.LerDados(Vd.IdPessoa);

                if (Vd.Status > 0 && Vd.Status < 4)// && ((Vd.VlrDesconto + Vd.VlrCredito) > 0))
                {
                    //Atualiar Comissao                  
                    SqlDataReader TabComissao = Controle.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                                                                     " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and t1.Id_Venda=" + Vd.IdVenda.ToString());
                    Controles.Comissao CalcComissao = new Controles.Comissao();
                    CalcComissao.Controle = Controle;
                    decimal PDesconto = 0;
                    if (Vd.IgnoraDesc == 0)
                    {
                        if ((Vd.VlrDesconto + Vd.VlrCredito) > 0)
                            PDesconto = 100 / (Vd.VlrSubTotal / (Vd.VlrDesconto + Vd.VlrCredito));
                    }
                    if (Vd.Data.Date.Year >= 2019)
                    {
                        if (!CalcComissao.CalcularMovimento2019(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa))
                            MessageBox.Show("Erro Venda: " + Vd.IdVenda.ToString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (!CalcComissao.CalcularMovimento(TabComissao, PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa))
                            MessageBox.Show("Erro Venda: " + Vd.IdVenda.ToString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            MessageBox.Show("Fim");
        }
        private void GridDados_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (GridDados.Rows[e.RowIndex].Cells[2].Value.ToString() == "Confirmado")
                    GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LemonChiffon;
                if (GridDados.Rows[e.RowIndex].Cells[2].Value.ToString() == "Faturado")
                    GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                if (GridDados.Rows[e.RowIndex].Cells[2].Value.ToString() == "Entregue")
                    GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                if (GridDados.Rows[e.RowIndex].Cells[2].Value.ToString() == "Cancelado")
                    GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Salmon;
            }
        }
        private void LstEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                if (!VerificarStatus())
                    return;

                if (TipoMov == "TROCA")
                {
                    if (int.Parse(LstEntrega.SelectedValue.ToString()) != FrmPrincipal.IdFilialConexao)
                    {
                        Vendas.SemMovEst = 0;
                        Ck_SemMovEst.Checked = true;
                    }
                    else
                    {
                        Vendas.SemMovEst = 0;
                        Ck_SemMovEst.Checked = false;
                    }
                }
                else
                {
                    if (int.Parse(LstEntrega.SelectedValue.ToString()) != FrmPrincipal.IdFilialConexao)
                    {                        
                        Controle.ExecutaSQL("Update MVvendaItens set TipoItem='N' where Id_venda=" + Vendas.IdVenda.ToString());
                    }
                    else
                        Controle.ExecutaSQL("Update MVvendaItens set TipoItem='S' where Vinculado=0 and Id_venda=" + Vendas.IdVenda.ToString());

                    PopularGridItens();
                }

            }
        }
        private void GridFinanc_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Favor gravar o movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (Vendas.TpVenda != "PV")
                {
                    MessageBox.Show("Atenção: Movimento não é um Pedido de Venda", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    return;
                }

                if (Vendas.Status == 3)
                {
                    MessageBox.Show("Atenção: Movimento já entregue", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    return;
                }

                if (Vendas.Status == 1)
                {
                    MessageBox.Show("Atenção: Fature o movimento antes de alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    return;
                }

                if (FrmPrincipal.Perfil_Usuario.AlterarVenda == 0 && !FrmPrincipal.VersaoDistribuidor)
                {
                    MessageBox.Show("Autorização negada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    return;
                }
            }
        }
        private void GridFinanc_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Financeiro LancFinac = new Financeiro();
                LancFinac.Controle = Controle;
                LancFinac.LerDados(int.Parse(GridFinanc.CurrentRow.Cells[0].Value.ToString()));

                if (LancFinac.Status != 0)
                {
                    MessageBox.Show("Atenção: Documento já baixado pelo Financeiro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopularFinanceiro();
                    return;
                }

                //Atualizando
                LancFinac.Vencimento = DateTime.Parse(GridFinanc.CurrentRow.Cells[4].Value.ToString());
                LancFinac.GravarDados();
                PopularFinanceiro();
                MessageBox.Show("Atenção: Alteração Concluida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch 
            {
                MessageBox.Show("Falha na atualização, tente novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PopularFinanceiro();
            }
        }        
        //Enviar Venda pra outra Filial
        private void BtnEnviarVd_Click(object sender, EventArgs e)
        {
            BtnEnviarVd.Enabled = false;
            Application.DoEvents();

            Vendas.LerDados(Vendas.IdVenda);

            FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 1, "Tentando Enviar Venda");
            
            if (Vendas.Status <= 1 || Vendas.Status == 4)
            {
                MessageBox.Show("Atenção: Efetua o Faturamento da venda", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (Vendas.IdFilialEntrega == 0)
            {
                MessageBox.Show("Atenção: Selecione a Filial Destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarVd.Enabled = true;
                return;
            }

            if (Vendas.IdFilialEntrega == FrmPrincipal.IdFilialConexao)
            {
                MessageBox.Show("Atenção: Local de Origem e Destino iguais", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarVd.Enabled = true;
                return;
            }            

            // Conectando ao Servidor Destino
            SqlConnection ServidorDestino;
            Filiais FilialDest = new Filiais();
            FilialDest.Controle = Controle;
            FilialDest.LerDados(Vendas.IdFilialEntrega);

            if (FilialDest.ServidorRemoto == "")
            {
                MessageBox.Show("Atenção: Configuração do Servidor Destino inválida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarVd.Enabled = true;
                return;
            }
            try
            {
                string conexao = "";
                conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

                ServidorDestino = new SqlConnection(conexao);
                ServidorDestino.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor destino, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarVd.Enabled = true;
                return;
            }

            Funcoes ControleDest = new Funcoes();
            ControleDest.Conexao = ServidorDestino;

            MvVenda MvDestino = new MvVenda();
            MvDestino.Controle = ControleDest;
            MvDestino.LerDados(Vendas.IdVdDestino);
                        
            if (MvDestino.Status == 1 || MvDestino.Status == 2 || MvDestino.Status == 3)
            {
                MessageBox.Show("Atenção: Solicite o Cancelamento da Venda na Filial de Entrega ", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarVd.Enabled = true;
                return;
            }
            
            DataSet TabCliente = Controle.ConsultaTabela("SELECT * FROM PESSOAS WHERE ID_PESSOA=" + Vendas.IdPessoa.ToString());
            XmlDocument XMLCadPessoa = new XmlDocument();
            XMLCadPessoa.LoadXml(TabCliente.GetXml());

            DataSet TabVenda = Controle.ConsultaTabela("SELECT T1.*,T2.FormaPgto AS NomeFormaPgto,T3.Vendedor AS NomeVendedor FROM MvVenda T1 " +
                                                       " LEFT JOIN FORMAPAGAMENTO T2 ON (T2.Id_FormaPgto=T1.Id_FormaPgto)" +
                                                       " LEFT JOIN Vendedores T3 ON (T3.Id_Vendedor=T1.Id_Vendedor)" +
                                                       " WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString());

            XmlDocument XMLVenda = new XmlDocument();
            XMLVenda.LoadXml(TabVenda.GetXml());

            DataSet TabItens = Controle.ConsultaTabela("SELECT T1.*,T2.Referencia,T2.Descricao FROM MvVendaItens T1 " +
                                                       " LEFT JOIN Produtos T2 ON (T2.Id_Produto=T1.Id_Produto)" +
                                                       " WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString());
            XmlDocument XMLItens = new XmlDocument();
            XMLItens.LoadXml(TabItens.GetXml());

            string LstEstPrd = "";
            if (TabItens.Tables[0].Rows.Count > 0)
            {
                FrmPrincipal.BSta_BarProcesso.Maximum = TabItens.Tables[0].Rows.Count;
                decimal Saldo = 0;
                
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (TabItens.Tables[0].Rows[I]["TIPOITEM"].ToString() == "N" && int.Parse(TabItens.Tables[0].Rows[I]["VINCULADO"].ToString()) == 0 && VerGrupo_Estoque(ControleDest, TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString()))
                    {
                        Saldo = 0;
                        Saldo = Ver_SldPrdFilialDestino(ControleDest, TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString());

                        //if (!FrmPrincipal.VersaoDistribuidor && IsConnected() && Vendas.IdFilialEntrega == 2)
                        //    SaldoDeposito = SaldoProdutoDeposito(TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString());

                        //if (Saldo < decimal.Parse(TabItens.Tables[0].Rows[I]["QTDE"].ToString()) && (Saldo + SaldoDeposito >= decimal.Parse(TabItens.Tables[0].Rows[I]["QTDE"].ToString())))
                        //    SetaLiberacaoProduto(Vendas.IdFilialEntrega, 0, Vendas.IdVenda, TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString(), Saldo);
                        //else
                        //{
                        if (Saldo <= 0 || Saldo < decimal.Parse(TabItens.Tables[0].Rows[I]["QTDE"].ToString()))
                        {
                            if (!Ver_LibPrdFilialDestino(ControleDest, TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString(), FrmPrincipal.IdFilialConexao, Vendas.IdVenda))
                                LstEstPrd = LstEstPrd + "Produto: " + TabItens.Tables[0].Rows[I]["REFERENCIA"].ToString().Trim() + " - " + TabItens.Tables[0].Rows[I]["Descricao"].ToString().Trim() + "\n";
                        }
                        //}
                    }
                }
            }

            if (LstEstPrd != "")
            {
                MessageBox.Show(LstEstPrd+", não tem saldo Suficiente no estoque, Favor Solicitar liberação: ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarVd.Enabled = true;
                return;
            }
            
            if (MessageBox.Show("Confirma o Envio da Venda ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (MvDestino.Status != 4)
                    MvDestino.Excluir();

                Controles.RegistrarPedidoVenda.RegistroVenda RegVd = new Controles.RegistrarPedidoVenda.RegistroVenda();
                if (FilialDest.IdFilial == 7)
                    RegVd.Url = "http://" + FilialDest.ServidorRemoto.ToString().Trim() + "/ERP-SGE_WebServiceLoja/RegistroVenda.asmx?WSDL";
                else
                    RegVd.Url = "http://" + FilialDest.ServidorRemoto.ToString().Trim() + "/ERP-SGE_WebService/RegistroVenda.asmx?WSDL";

               /* StreamWriter XmlNota;
                string vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\XML\\XMLCadPessoa.xml";
                XmlNota = File.CreateText(vArqXML);
                XmlNota.Write(XMLCadPessoa.OuterXml.ToString());
                XmlNota.Close();

                StreamWriter XmlNota2;
                vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\XML\\XMLVenda.xml";
                XmlNota2 = File.CreateText(vArqXML);
                XmlNota2.Write(XMLVenda.OuterXml.ToString());
                XmlNota2.Close();

                StreamWriter XmlNota3;
                vArqXML = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\XML\\XMLItens.xml";
                XmlNota3 = File.CreateText(vArqXML);
                XmlNota3.Write(XMLItens.OuterXml.ToString());
                XmlNota3.Close();
                return;*/

                string IdVd = RegVd.RegistrarVenda(FrmPrincipal.IdFilialConexao, XMLCadPessoa, XMLVenda, XMLItens);
                if (IdVd.Substring(0, 2) == "-1")
                    MessageBox.Show("Atenção: Cliente não Localizado na Filial Destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (IdVd.Substring(0, 2) == "-2")
                    MessageBox.Show("Atenção: Produto não Localizado, " + IdVd.Replace("-2","").Trim(), "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (IdVd.Substring(0, 2) == "-3")
                    MessageBox.Show("Atenção: Erro no envio da venda", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    int IdVdRec = int.Parse(IdVd);
                    Controle.ExecutaSQL("Update MvVenda set ID_VdDestino=" + IdVdRec.ToString() + ",DtEnvioRec=GetDate() WHERE ID_VENDA=" + Vendas.IdVenda.ToString());
                    MessageBox.Show("Envio da Venda Conluida, No. Venda: " + IdVdRec.ToString(), "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Vendas.IdCaixa==0 && Vendas.TpVenda=="PV")
                        Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_VENDA=" + Vendas.IdVenda.ToString());

                    PopularCampos(Vendas.IdVenda);
                    FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 1, "Envio da Venda para: " + FilialDest.Fantasia);
                }
                
            } 
            BtnEnviarVd.Enabled = true;
            ServidorDestino.Dispose();
        }
        private bool Ver_LibPrdFilialDestino(Funcoes Funcao, string Ref, int IdFilial, int IdVenda)
        {
            SqlDataReader Tab = Funcao.ConsultaSQL("SELECT * FROM LiberacaoProduto T1" +
                                                   " WHERE T1.ID_FILIAL=" + IdFilial.ToString() +
                                                   "   AND T1.Id_Venda=" + IdVenda.ToString() +
                                                   "   AND T1.Id_Produto=(SELECT TOP 1 T2.ID_PRODUTO FROM Produtos T2 WHERE T2.ATIVO=1 AND T2.Referencia='" + Ref + "')");
            if (Tab.HasRows)
                return true;
            else
                return false;
        }
        private decimal Ver_SldPrdFilialDestino(Funcoes Funcao,string Ref)
        {            
            SqlDataReader Tab = Funcao.ConsultaSQL("SELECT CASE T1.PRODUTOKIT WHEN 0 THEN T1.SaldoEstoque  ELSE (SELECT MIN(KT2.SALDOESTOQUE) FROM PRODUTOSKIT KT1  LEFT JOIN PRODUTOS KT2 ON (KT2.ID_PRODUTO=KT1.ID_PRODUTO)" +
                                                   " WHERE KT1.ID_PRDMASTER=T1.ID_PRODUTO) END AS SALDOESTOQUE,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado FROM Produtos T1 WHERE T1.REFERENCIA='" + Ref.ToString()+"'");
            if (Tab.HasRows)
            {
                Tab.Read();
                return decimal.Parse(Tab["SALDOESTOQUE"].ToString());
            }
            else
                return 0;
        }
        private bool VerGrupo_Estoque(Funcoes Funcao, string Ref)
        { 
            SqlDataReader Tab = Funcao.ConsultaSQL("SELECT ISNULL(T2.Estoque,0) AS ESTOQUE  FROM Produtos T1  LEFT JOIN GRUPOPRODUTO T2 ON (T2.ID_GRUPO=T1.ID_GRUPO)  WHERE T1.REFERENCIA='" + Ref.ToString() + "'");
            if (Tab.HasRows)
            {
                Tab.Read();
                if (int.Parse(Tab["ESTOQUE"].ToString()) == 1)
                    return false;

            }            
            return true;
        }
        private decimal BuscaSldFilialEntrega(int IdFilial, string Ref)
        {
            try
            {
                Filiais FilialDest = new Filiais();
                FilialDest.Controle = Controle;
                FilialDest.LerDados(IdFilial);
                decimal Saldo = -999999;
                //Verificando o Saldo do Estoque
                if (FilialDest.ServidorRemoto.Trim() != "")
                {
                    Controles.Serv_SaldoEstoque.ConsultaSaldo WsSaldo = new Controles.Serv_SaldoEstoque.ConsultaSaldo();
                    if (FilialDest.IdFilial==7)
                        WsSaldo.Url = "http://" + FilialDest.ServidorRemoto.Trim() + "/WSSaldoEstoqueLoja/BuscaSaldoEstoque.asmx?swdl";
                    else
                        WsSaldo.Url = "http://" + FilialDest.ServidorRemoto.Trim() + "/WSSaldoEstoque/BuscaSaldoEstoque.asmx?swdl";
                    Saldo = WsSaldo.SaldoEstoque(Ref, FilialDest.ServidorRemoto.Trim(), FilialDest.Porta.Trim());
                }
                return Saldo;
            }
            catch
            {
                MessageBox.Show("Atenção: Erro no servidor de destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        private bool Ver_StaEnvioVd(int IdVenda )
        {
            // Conectando ao Servidor Destino
            SqlConnection ServidorDestino;
            Filiais FilialDest  = new Filiais();
            FilialDest.Controle = Controle;
            FilialDest.LerDados(Vendas.IdFilialEntrega);

            if (FilialDest.ServidorRemoto == "")
            {
                MessageBox.Show("Atenção: Configuração do Servidor Destino inválida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                return false;
            }
            try
            {
                string conexao = "";
                conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                ServidorDestino = new SqlConnection(conexao);
                ServidorDestino.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor destino, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                return false;
            }

            Funcoes ControleDest = new Funcoes();
            ControleDest.Conexao = ServidorDestino;

            MvVenda MvDestino = new MvVenda();
            MvDestino.Controle = ControleDest;

            MvDestino.LerDados(IdVenda);

            if (MvDestino.IdVenda == 0)
            {
                MessageBox.Show("Venda não Localizada na Filial de Entrega", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ServidorDestino.Dispose();
                return true;
            }

            if (MvDestino.Status!=4)
            {
                MessageBox.Show("Favor Soliciar o Cancelamento na Filial de Entrega", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ServidorDestino.Dispose();
                return false;
            }
            ServidorDestino.Dispose();
            return true;
        }

        public bool SetaLiberacaoProduto(int IdFilial, int IdUsu, int IdVenda, string Ref, decimal Saldo)
        {
            // Conectando ao Servidor Destino
            SqlConnection ServidorDestino;
            Filiais FilialDest = new Filiais();
            FilialDest.Controle = Controle;
            FilialDest.LerDados(IdFilial);

            if (FilialDest.ServidorRemoto == "")
            {
                MessageBox.Show("Atenção: Configuração do Servidor Destino inválida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                string conexao = "";
                conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                ServidorDestino = new SqlConnection(conexao);
                ServidorDestino.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor destino, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Funcoes ControleDest = new Funcoes();
            ControleDest.Conexao = ServidorDestino;

            Produtos CadPrd = new Produtos();
            CadPrd.Controle = ControleDest;

            CadPrd.LerDados(Ref);

            if (CadPrd.IdProduto == 0)
            {
                MessageBox.Show("Atenção: Produto não localizado na Filial de Entrega", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                SqlDataReader Tabela;
                Tabela = ControleDest.ConsultaSQL("SELECT * FROM LiberacaoProduto WHERE Id_Filial=" + FrmPrincipal.IdFilialConexao.ToString() + "and Id_Venda=" + IdVenda.ToString() + " and Id_Produto=" + CadPrd.IdProduto.ToString());

                if (Tabela.HasRows)
                    ControleDest.ExecutaSQL("Update LiberacaoProduto Set Id_Filial=" + FrmPrincipal.IdFilialConexao.ToString() + ", Id_Usuario=" + IdUsu.ToString() + ",Data=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103),Estoque=" + Controle.FloatToStr(Saldo, 3) + " Where Id_Venda=" + IdVenda.ToString() + " and Id_Produto=" + CadPrd.IdProduto.ToString());
                else
                    ControleDest.ExecutaSQL("Insert into LiberacaoProduto (Id_Venda,Id_Produto,Id_Usuario,Data,Id_Filial,Estoque) Values (" + IdVenda.ToString() + "," + CadPrd.IdProduto.ToString() + "," + IdUsu.ToString() + ",Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103)," + FrmPrincipal.IdFilialConexao.ToString() + "," + Controle.FloatToStr(Saldo, 3) + ")");
                ServidorDestino.Dispose();
                return true;
            }
            catch
            {
                ServidorDestino.Dispose();
                return false;
            }
        }

        private void BtnInfAdd_Click(object sender, EventArgs e)
        {
            TelaInfAdicionais();
        }
        private void TelaInfAdicionais()
        {
            if (Vendas.IdVenda > 0 && !StaFormEdicao)
            {
                RegistroInf = false;
                FrmObsCancelamento Frm = new FrmObsCancelamento();
                Frm.FrmPrincipal = FrmPrincipal;
                Frm.IdVenda = Vendas.IdVenda;
                Frm.ShowDialog();
                RegistroInf = Frm.RegInf;
                Frm.Dispose();
                PopularCampos(Vendas.IdVenda);
            }
        }        
        private void CalcularComissaoPedido()
        {
            if (Vendas.VlrTotal > 0 && (Vendas.Status == 0 || Vendas.Status == 4) && (Vendas.TpVenda == "PV" || Vendas.TpVenda == "VF" || Vendas.TpVenda == "OC" || Vendas.TpVenda == "OE"))
            {

                CadVend.LerDados(Vendas.IdVendedor);
                DataRow Item;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (TabItens.Tables[0].Rows[I].RowState != DataRowState.Deleted)
                    {
                        Controles.Comissao CalcComissao = new Controles.Comissao();
                        CalcComissao.Controle = Controle;
                        decimal PDesconto = 0;
                        if (Vendas.IgnoraDesc == 0)
                        {
                            if ((Vendas.VlrDesconto + Vendas.VlrCredito) > 0)
                                PDesconto = 100 / (Vendas.VlrSubTotal / (Vendas.VlrDesconto + Vendas.VlrCredito));
                        }
                        if (Vendas.Data.Date.Year >= 2019)
                            Item = CalcComissao.CalcularMovimento2019(TabItens.Tables[0].Rows[I], PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa, CadVend.Comissao);
                        else
                            Item = CalcComissao.CalcularMovimento(TabItens.Tables[0].Rows[I], PDesconto, (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 6), FrmPrincipal.Parametros_Filial, CadPessoa.ComissaoFixa, CadPessoa.IdPessoa, CadVend.Comissao);
                        TabItens.Tables[0].Rows[I]["VlrComissao"] = Item["VlrComissao"];
                        TabItens.Tables[0].Rows[I]["P_Comissao"]  = Item["P_Comissao"];
                    }
                }
            }            
        }

        private void BtnImpUltVenda_Click(object sender, EventArgs e)
        {
            FrmUltMovCliente FrmUltMov = new FrmUltMovCliente();
            FrmUltMov.FrmPrincipal = FrmPrincipal;
            FrmUltMov.IdPessoa     = Vendas.IdPessoa;
            FrmUltMov.Vendas       = Vendas;
            FrmUltMov.CadPessoa    = CadPessoa;
            FrmUltMov.ShowDialog();
            FrmUltMov.Dispose();
            PopularGridItens();            
        }
    }
}

