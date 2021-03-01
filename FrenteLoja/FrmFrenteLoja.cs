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
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using MessagingToolkit.QRCode.Codec;
using System.Drawing.Imaging;
using ERP_SGE;
using Epson;
using System.Xml;

namespace FrenteLoja
{
    public partial class FrmFrenteLoja : Form
    {
        public enum StatusCaixa
        {
            Fechado,
            Aberto,
            EmVenda
        }
        public enum SanGriaSuprimento
        {
            Sangria,
            Suprimento
        }
        public TelaPrincipal FrmPrincipal;
        Controle_Dados.Funcoes Controle         = new Controle_Dados.Funcoes();
        Controle_Dados.Auditoria RegAuditoria   = new Controle_Dados.Auditoria();
        public Controle_Dados.MvVenda Vendas    = new Controle_Dados.MvVenda();        
        public Controle_Dados.Produtos CadPrd   = new Controle_Dados.Produtos();
        public Controle_Dados.Filiais CadFilial = new Controle_Dados.Filiais();
        public FrmBuscaPessoa FrmPesqPessoa;
        public StatusCaixa StaCaixa;
        public int IdCaixa       = 0;
        public int IdPessoa      = 0;
        public int TotItens      = 0;
        public decimal TotVenda  = 0;
        public decimal DescVenda = 0;
        public bool AltPreco     = false;        
        public int IdPromocao    = 0;
        public string ChaveCFe   = "";
        public int NumNFCe       = 0;
        public int IdVdImp       = 0;
        public DataTable TabItens;

        public FrmFrenteLoja()
        {
            InitializeComponent();
        }

        private void FrmFrenteLoja_Load(object sender, EventArgs e)
        {
            FrmPrincipal = new TelaPrincipal();
            if (FrmPrincipal.LerConfig())
            {
                try
                {
                    FrmLogin Login = new FrmLogin();
                    Login.LblFilial.Visible  = false;
                    Login.LstConexao.Visible = false;
                    Login.FrmPrincipal = FrmPrincipal;
                    Login.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (!Login.AcessoOk)
                        Close();
                    else
                    {

                       // ArrayList Parametros = new ArrayList();
                       // StreamReader LerParam = new StreamReader("ERP-SGE_FreteLoja.ini");
                       // while (!LerParam.EndOfStream)
                       //     Parametros.Add(LerParam.ReadLine());
                         
                        Controle.Conexao = FrmPrincipal.Conexao;
                        FrmPrincipal.Parametros_Filial = new Controle_Dados.Parametros();
                        FrmPrincipal.Parametros_Filial.Controle = Controle;
                        if (FrmPrincipal.Perfil_Usuario != null)
                        {
                            if (FrmPrincipal.Perfil_Usuario.MultplaInstancia == 0)
                            {
                                Process Instancia = Process.GetCurrentProcess();
                                string NomeInstancia = Instancia.ProcessName;
                                if (Process.GetProcessesByName(NomeInstancia).Length > 1)
                                {
                                    MessageBox.Show("Já existe uma instância do aplicativo em aberto");
                                    Close();
                                }
                            }
                            BSta_NmUsuario.Text = FrmPrincipal.Perfil_Usuario.Usuario;
                            FrmPrincipal.Parametros_Filial.LerDados(FrmPrincipal.Perfil_Usuario.IdFilial);
                            CadFilial.Controle = Controle;
                            Vendas.Controle    = Controle;
                            CadFilial.LerDados(FrmPrincipal.Perfil_Usuario.IdFilial);
                            RegAuditoria.Controle = Controle;
                            BSta_Banco.Text       = FrmPrincipal.Conexao.Database;
                            BSta_Servidor.Text    = FrmPrincipal.Conexao.DataSource;
                            BSta_Estacao.Text     = FrmPrincipal.Conexao.WorkstationId;
                            BSta_StaConexao.Text  = "Conectado";
                            //
                        }
                        else
                            FrmPrincipal.Parametros_Filial.LerDados(0);
                        //
                        CadPrd.Controle = Controle;
                        VerificarCaixa(FrmPrincipal.Perfil_Usuario.IdUsuario);
                        FrmPesqPessoa = new FrmBuscaPessoa();
                        FrmPesqPessoa.FrmPrincipal = FrmPrincipal;
                        FrmPesqPessoa.CadPessoa.Controle = Controle;
                        FrmPesqPessoa.CadPessoa.IdPessoa = FrmPrincipal.Parametros_Filial.IdConsumidor;
                        LstVendedor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstVendedor);
                        LstVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor;
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
        private void VerificarCaixa(int IdUsuario)
        {   
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM CaixaBalcao WHERE Status=0 and Id_Usuario=" + IdUsuario.ToString().Trim());
            if (Tabela.HasRows)
            {
                Tabela.Read();
                IdCaixa = int.Parse(Tabela["Id_Caixa"].ToString());
                StaCaixa = StatusCaixa.Aberto;
            }
            else
                StaCaixa = StatusCaixa.Fechado;

            AtualizarTela();
            TxtCodBarra.Focus();
        }
        private void TxtCodBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar.ToString().ToUpper() == Keys.X.ToString() && TxtCodBarra.Text.Trim() == "")
            {
                e.Handled = true;
                SendKeys.Send("");
                TxtQtde.Enabled = true;
                TxtQtde.Text = "";
                TxtQtde.Focus();
            }*/
            if (e.KeyChar.ToString().ToUpper() == Keys.P.ToString() && TxtCodBarra.Text.Trim() == "")
            {
                FrmBuscaProduto Frm = new FrmBuscaProduto();
                Frm.FrmPrincipal = FrmPrincipal;
                Frm.ShowDialog();

                if (Frm.CadProd.IdProduto > 0)
                {
                    ArrayList PrdQtde = new ArrayList(Frm.ListaCodPrd[1].ToString().Split(char.Parse("|")));
                    IdPromocao = int.Parse(PrdQtde[2].ToString());

                    if (Frm.CadProd.CodBarra.Trim() != "")
                        TxtCodBarra.Text = Frm.CadProd.CodBarra.Trim();
                    else
                        TxtCodBarra.Text = Frm.CadProd.Referencia.Trim();
                }
                e.Handled = true;
                SendKeys.Send("");
                TxtCodBarra.Focus();
            }
            // Desconto
            /* if (e.KeyChar.ToString().ToUpper() == Keys.D.ToString() && TxtCodBarra.Text.Trim() == "")
             {
                 e.Handled = true;
                 SendKeys.Send("");
                 TxtVlrDesc.Enabled = true;
                 TxtVlrDesc.Text = "";
                 TxtVlrDesc.Focus();
             }*/
            //
            else if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab && TxtCodBarra.Text.Trim() != "")
            {
                if (BuscaProduto())
                {
                    e.Handled = true;
                    SendKeys.Send("");
                    TxtQtde.Enabled = true;
                    TxtQtde.Value = 1;
                    TxtQtde.Text = "";
                    TxtQtde.Focus();
                }
            }
            else if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != (char)Keys.Back && e.KeyChar != 'X')
            {
                e.Handled = true;
                SendKeys.Send("");
            }
        }
        private void TxtVlrUnt_Validated(object sender, EventArgs e)
        {
            AltPreco = false;
            if (TxtVlrUnt.Text.Trim() == "")
                TxtVlrUnt.Value = 0;
            TxtVlrUnt.Enabled = false;
            if (TxtVlrUnt.Value > 0)
                AltPreco = true;
            TxtCodBarra.Focus();
        }
        private void FrmFrenteLoja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
            if (e.Alt && e.KeyCode == Keys.C && StaCaixa != StatusCaixa.Fechado)
            {
                FrmPesqPessoa.ShowDialog();
                if (FrmPesqPessoa.CadPessoa.IdPessoa > 0) // && StaCaixa == StatusCaixa.EmVenda)
                {
                    LblCliente.Text = "Cliente: " + FrmPesqPessoa.CadPessoa.RazaoSocial;
                    IdPessoa = FrmPesqPessoa.CadPessoa.IdPessoa;
                }
                else
                {
                    IdPessoa = FrmPrincipal.Parametros_Filial.IdConsumidor;
                    LblCliente.Text = "Cliente: ";
                }
            }
            if (e.Alt && e.KeyCode == Keys.V && StaCaixa != StatusCaixa.EmVenda) // Consulta as Vendas Realizadas
            {
                FrmConsVendaMFe Frm = new FrmConsVendaMFe();
                Frm.FrmFrenteLoja = this;
                Frm.IdCaixa = IdCaixa;
                Frm.ShowDialog();
            }
            if (e.Alt && e.KeyCode == Keys.I && StaCaixa != StatusCaixa.EmVenda) // Importação de vendas
            {
                FrmConsVenda Frm  = new FrmConsVenda();
                Frm.FrmFrenteLoja = this;
                IdVdImp           = 0;
                Frm.ShowDialog();

                if (IdVdImp > 0)
                   FrmPrincipal.RegistrarAuditoria("Frente de Loja", IdVdImp, IdVdImp.ToString(), 1, "Frente Loja: Importação de Venda");

            }
            if (e.KeyCode == Keys.F2)
            {
                if (StaCaixa == StatusCaixa.Fechado)
                {
                    FrmCxaAbrir FrmAbrirCx = new FrmCxaAbrir();
                    FrmAbrirCx.FrmPrincipal = FrmPrincipal;
                    FrmAbrirCx.ShowDialog();
                    VerificarCaixa(FrmPrincipal.Perfil_Usuario.IdUsuario);
                    FrmAbrirCx.Dispose();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                if (StaCaixa == StatusCaixa.Aberto)
                {
                    /*Controle_Dados.CaixaBalcao CxBalcao = new Controle_Dados.CaixaBalcao();
                    CxBalcao.Controle = Controle;
                    CxBalcao.LerCaixa(IdCaixa);
                    CxBalcao.Status = 1;
                    CxBalcao.DtHrEnc = DateTime.Now;
                    CxBalcao.FecharCaixa();
                    //if (MessageBox.Show("Imprime a Leitura Z ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    PDV_ImpressoraFiscal.ImpFiscal = TipoImpressoraFiscal();
                    //    PDV_ImpressoraFiscal.LeituraZ();
                    //}
                    MessageBox.Show("Caixa Fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StaCaixa = StatusCaixa.Fechado;
                    AtualizarTela();*/

                    FrmFecharCxBalcao FrmFecharCx = new FrmFecharCxBalcao();
                    FrmFecharCx.FrmPrincipal = FrmPrincipal;
                    FrmFecharCx.IdCaixa = IdCaixa;
                    FrmFecharCx.ShowDialog();

                    if (FrmFecharCx.CxFechado)
                    {
                        Controle_Dados.CaixaBalcao CxBalcao = new Controle_Dados.CaixaBalcao();
                        CxBalcao.Controle = Controle;
                        CxBalcao.LerCaixa(IdCaixa);
                        CxBalcao.Status = 1;
                        CxBalcao.DtHrEnc = DateTime.Now;
                        CxBalcao.FecharCaixa();
                        MessageBox.Show("Caixa Fechado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StaCaixa = StatusCaixa.Fechado;
                        AtualizarTela();
                    }
                }
            }
            if (e.KeyCode == Keys.F4 && StaCaixa != StatusCaixa.Fechado)
            {
                FrmSangriaSuprimento FrmMov = new FrmSangriaSuprimento();
                FrmMov.FrmFrenteLoja = this;
                FrmMov.Text = "SANGRIA DE CAIXA";
                FrmMov.TpMovim = SanGriaSuprimento.Sangria;
                FrmMov.ShowDialog();
                FrmMov.Dispose();
            }
            if (e.KeyCode == Keys.F5 && StaCaixa != StatusCaixa.Fechado)
            {
                FrmSangriaSuprimento FrmMov = new FrmSangriaSuprimento();
                FrmMov.FrmFrenteLoja = this;
                FrmMov.Text = "SUPRIMENTO DE CAIXA";
                FrmMov.TpMovim = SanGriaSuprimento.Suprimento;
                FrmMov.ShowDialog();
                FrmMov.Dispose();
            }
            if (e.KeyCode == Keys.F6 && StaCaixa == StatusCaixa.EmVenda)
            {
                FrmCancelarItem Frm = new FrmCancelarItem();
                Frm.FrmFrenteLoja = this;
                Frm.ShowDialog();
                Frm.Dispose();
            }
            if (e.KeyCode == Keys.F7 && TxtCodBarra.Text.Trim() != "" && StaCaixa != StatusCaixa.Fechado)
            {
                e.Handled = true;
                SendKeys.Send("");
                TxtVlrUnt.Enabled = true;
                TxtVlrUnt.Text = "";
                TxtVlrUnt.Focus();
            }
            if (e.KeyCode == Keys.F8 && StaCaixa == StatusCaixa.EmVenda)
            {
                try
                {
                    FrmFecharVenda Frm = new FrmFecharVenda();
                    Frm.FrmFrenteLoja = this;
                    DescVenda = Vendas.VlrDesconto;
                    Frm.ShowDialog();

                    if (Frm.VendaFechada)
                    {
                        StringBuilder cupom = new StringBuilder(200);
                        cupom.Append(' ', 40);
                        cupom.Append('-', 15);
                        cupom.Append('\n');
                        cupom.Append("Sub.TOTAL R$");
                        cupom.AppendFormat(TotVenda.ToString("N").PadLeft(43, ' '));
                        cupom.Append('\n');
                        cupom.Append("Desconta R$");
                        cupom.AppendFormat(DescVenda.ToString("N").PadLeft(44, ' '));
                        cupom.Append('\n');
                        cupom.Append("TOTAL R$");
                        cupom.AppendFormat((TotVenda - DescVenda).ToString("N").PadLeft(47, ' '));
                        cupom.Append('\n');
                        cupom.Append(' ', 40);
                        cupom.Append('-', 15);
                        cupom.Append('\n');
                        AtualizarDisplay(cupom.ToString());
                        //
                        Controle_Dados.Financeiro Financ = new Controle_Dados.Financeiro();
                        Financ.Controle = Controle;
                        //
                        Controle_Dados.MvVendaItens VendasItem = new Controle_Dados.MvVendaItens();
                        VendasItem.Controle = Controle;

                        if (IdVdImp > 0)
                            FrmPrincipal.RegistrarAuditoria("Frente de Loja", IdVdImp, IdVdImp.ToString(), 2, "Frente Loja: Fechamento da Venda");

                        Vendas.LerDados(IdVdImp);
                        Vendas.Data = DateTime.Now;
                        Vendas.TpVenda = "PV";
                        Vendas.VlrSubTotal = TotVenda;
                        Vendas.VlrDesconto = DescVenda;
                        Vendas.VlrTotal = TotVenda - DescVenda;
                        Vendas.IdCaixa = IdCaixa;
                        Vendas.IdVendedor = int.Parse(LstVendedor.SelectedValue.ToString());
                        Vendas.IdUltUsuario = FrmPrincipal.Perfil_Usuario.IdUsuario;
                        Vendas.IdPessoa = FrmPesqPessoa.CadPessoa.IdPessoa;
                        Vendas.CnpjCpf = FrmPesqPessoa.CadPessoa.Cnpj;
                        Vendas.NmPessoa = FrmPesqPessoa.CadPessoa.RazaoSocial;
                        Vendas.InscUF = FrmPesqPessoa.CadPessoa.InscUF;
                        Vendas.Cep = FrmPesqPessoa.CadPessoa.Cep.Replace("-", ""); ;
                        Vendas.Endereco = FrmPesqPessoa.CadPessoa.Endereco;
                        Vendas.Numero = FrmPesqPessoa.CadPessoa.Numero;
                        Vendas.Complemento  = FrmPesqPessoa.CadPessoa.Complemento;
                        Vendas.Cidade       = FrmPesqPessoa.CadPessoa.Cidade;
                        Vendas.Bairro       = FrmPesqPessoa.CadPessoa.Bairro;
                        Vendas.Fone         = FrmPesqPessoa.CadPessoa.Fone; ;
                        Vendas.IdUF         = FrmPesqPessoa.CadPessoa.IdUF;                        
                        Vendas.IdFilial     = FrmPrincipal.Parametros_Filial.IdFilial;
                        Vendas.IdEntregador = FrmPrincipal.Perfil_Usuario.IdEntregador;

                        if (FrmPrincipal.Perfil_Usuario.BxVdFrenteLj == 1)
                            Vendas.Status = 3;
                        else
                            Vendas.Status = 2;

                        Vendas.DataConfirmacao = DateTime.Now;
                        Vendas.ImpNF      = 1;
                        Vendas.VdBalcao   = 1;
                        Vendas.SemMovEst  = 0;
                        Vendas.IdUsuario  = FrmPrincipal.Perfil_Usuario.IdUsuario;
                        Vendas.FrenteLoja = 1;

                        if (Vendas.VlrSubTotal == 0)
                        {
                            Vendas.LerDados(0);
                            Vendas.IdVenda = 0;
                            IdVdImp       = 0;
                        }
                        else
                        {
                            Vendas.GravarDados();
                            FrmPrincipal.RegistrarAuditoria("Frente de Loja", Vendas.IdVenda, Vendas.NumDocumento, 3, "Frente Loja: Venda Concluida");

                            //Gravando os Itens
                            if (Vendas.IdVenda > 0)
                            {
                                Controle.ExecutaSQL("Update MvVenda set Status=2,DtHrFaturamento=GetDate(),Id_entregador=" + FrmPrincipal.Perfil_Usuario.IdEntregador.ToString() + " where id_Venda=" + Vendas.IdVenda.ToString());

                                if (FrmPrincipal.Perfil_Usuario.BxVdFrenteLj == 1)
                                    Vendas.Status = 3;
                                else
                                    Vendas.Status = 2;

                                for (int I = 0; I <= TabItens.Rows.Count - 1; I++)
                                {
                                    if (int.Parse(TabItens.Rows[I]["STATUS"].ToString()) == 1)
                                    {
                                        if (int.Parse(TabItens.Rows[I]["IDITEM"].ToString()) == 0)
                                        {
                                            VendasItem.LerDados(0);
                                            VendasItem.IdVenda = Vendas.IdVenda;
                                            VendasItem.IdItem = 0;
                                            VendasItem.IdProduto = int.Parse(TabItens.Rows[I]["ID_Produto"].ToString());
                                            VendasItem.Qtde = decimal.Parse(TabItens.Rows[I]["QTDE"].ToString());
                                            VendasItem.VlrUnitario = decimal.Parse(TabItens.Rows[I]["VLRUNITARIO"].ToString());
                                            VendasItem.VlrTotal = decimal.Parse(TabItens.Rows[I]["VLRTOTAL"].ToString());
                                            VendasItem.VlrUntComissao = decimal.Parse(TabItens.Rows[I]["VLRUNITARIO"].ToString());
                                            VendasItem.PrcCusto = decimal.Parse(TabItens.Rows[I]["PRCCUSTO"].ToString());
                                            VendasItem.PrcMinimo = decimal.Parse(TabItens.Rows[I]["PRCMINIMO"].ToString());
                                            VendasItem.PrcVarejo = decimal.Parse(TabItens.Rows[I]["PRCVAREJO"].ToString());
                                            VendasItem.PrcAtacado = decimal.Parse(TabItens.Rows[I]["PRCATACADO"].ToString());
                                            VendasItem.PrcEspecial = decimal.Parse(TabItens.Rows[I]["PRCESPECIAL"].ToString());
                                            VendasItem.PrcSensacional = decimal.Parse(TabItens.Rows[I]["PRCSENSACIONAL"].ToString());
                                            VendasItem.TipoItem = "S";
                                            VendasItem.GravarDados();
                                        }
                                    }
                                }
                            }
                            //Baixa no Estoque                    
                            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                            ControleEstoque.Controle = Controle;
                            SqlDataReader TabItensVd = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE Id_Venda=" + Vendas.IdVenda.ToString());

                            if (IdVdImp == 0)
                                ControleEstoque.MovimentoEstoque(TabItensVd, 2, 1, false, "PV", Vendas.Data, 0);

                            //Atualizando o Financeiro
                            Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE ID_VENDA=" + Vendas.IdVenda.ToString());
                            decimal TotLanc = 0;

                            FrmPrincipal.RegistrarAuditoria("Frente de Loja", Vendas.IdVenda, Vendas.NumDocumento, 4, "Frente Loja: Gerando Financeiro");

                            for (int I = 0; I <= Frm.Pagamento.Rows.Count - 1; I++)
                            {
                                TotLanc = TotLanc + decimal.Parse(Frm.Pagamento.Rows[I]["VLRDOC"].ToString());
                                Financ.LerDados(0);
                                Financ.PagRec = 2;
                                Financ.IdCaixa = IdCaixa;
                                Financ.IdMov = 0;
                                Financ.IdVenda = Vendas.IdVenda;
                                Financ.IdPessoa = FrmPesqPessoa.CadPessoa.IdPessoa;
                                Financ.IdFilial = Vendas.IdFilial;
                                Financ.Vencimento = DateTime.Parse(Frm.Pagamento.Rows[I]["VENCIMENTO"].ToString());
                                if (TotLanc > Vendas.VlrTotal)
                                    Financ.VlrOriginal = decimal.Parse(Frm.Pagamento.Rows[I]["VLRDOC"].ToString()) - (TotLanc - (TotVenda - DescVenda));
                                else
                                    Financ.VlrOriginal = decimal.Parse(Frm.Pagamento.Rows[I]["VLRDOC"].ToString());

                                Financ.IdTipoDocumento = int.Parse(Frm.Pagamento.Rows[I]["ID_DOCUMENTO"].ToString());
                                Financ.IdFormaPgto = Vendas.IdFormaPgto;
                                Financ.IdCusto = FrmPesqPessoa.CadPessoa.IdCusto;
                                Financ.IdDepartamento = FrmPesqPessoa.CadPessoa.IdDepartamento;
                                Financ.IdVendedor = Vendas.IdVendedor;
                                Financ.NumDoc = Vendas.NumDocumento.Trim() + "/" + string.Format("{0:D2}", I + 1);
                                Financ.Referente = "VENDA FRENTE DE LOJA";
                                Financ.IdUsuLanc = FrmPrincipal.Perfil_Usuario.IdUsuario;
                                Financ.GravarDados();
                                Frm.Pagamento.Rows[I]["IdFinanc"] = Financ.IdLanc.ToString();

                                if (int.Parse(Frm.Pagamento.Rows[I]["Baixa"].ToString()) == 1)
                                {
                                    Financ.DtBaixa = Financ.Vencimento;
                                    Financ.VlrBaixa = Financ.VlrOriginal;
                                    Financ.Baixar();
                                }
                            }

                            Controle_Dados.PagamentoMFE PagMFE = new Controle_Dados.PagamentoMFE();
                            PagMFE.Controle = Controle;
                            Controle.ExecutaSQL("DELETE FROM PAGAMENTOMFE WHERE ID_VENDA=" + Vendas.IdVenda.ToString());
                            for (int I = 0; I <= Frm.PagCartao.Rows.Count - 1; I++)
                            {
                                PagMFE.LerDados(0);
                                PagMFE.IdVenda = Vendas.IdVenda;
                                PagMFE.IdDocumento = int.Parse(Frm.PagCartao.Rows[I]["Id_Documento"].ToString());
                                PagMFE.Valor = decimal.Parse(Frm.PagCartao.Rows[I]["VLRDOC"].ToString());
                                PagMFE.NParc = int.Parse(Frm.PagCartao.Rows[I]["NParc"].ToString());
                                PagMFE.TpPag = int.Parse(Frm.PagCartao.Rows[I]["MFE"].ToString());
                                PagMFE.GravarDados();
                                Frm.PagCartao.Rows[I]["IdLanc"] = PagMFE.IdLanc.ToString();
                                //
                                StringBuilder cupomPag = new StringBuilder(105);
                                cupomPag.AppendFormat("{0,-16:G}", Controle.Space(Frm.PagCartao.Rows[I]["DOCUMENTO"].ToString(), 24));
                                cupomPag.AppendFormat(decimal.Parse(Frm.PagCartao.Rows[I]["VLRDOC"].ToString()).ToString("N").PadLeft(31, ' '));
                                cupomPag.Append('\n');
                                AtualizarDisplay(cupomPag.ToString());
                            }
                            //----------------------------------------------
                            StringBuilder cupomFim = new StringBuilder(105);
                            cupomFim.Append(' ', 40);
                            cupomFim.Append('-', 15);
                            cupomFim.Append('\n');
                            cupomFim.AppendFormat("{0,-16:G}", Controle.Space("TROCO", 20));
                            cupomFim.AppendFormat((Frm.VlrRecido - (TotVenda - DescVenda)).ToString("N").PadLeft(35, ' '));
                            cupomFim.Append('\n');
                            AtualizarDisplay(cupomFim.ToString());

                            LblTroco.Text = string.Format("{0:N2}", Frm.VlrRecido - (TotVenda - DescVenda));
                            PnlTroco.Visible = true;
                            Application.DoEvents();
                            MessageBox.Show("Venda Conluida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            //Imprimindo a Venda                    
                            /* string FormaPgto = "";
                             DataSet Parcelas = new DataSet();
                             Parcelas = Controle.ConsultaTabela("SELECT T1.VENCIMENTO,T1.VLRORIGINAL,T2.DOCUMENTO FROM LancFinanceiro T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString());
                             for (int I = 0; I <= Parcelas.Tables[0].Rows.Count - 1; I++)
                             {
                                 DateTime Dt = DateTime.Parse(Parcelas.Tables[0].Rows[I]["Vencimento"].ToString());
                                 FormaPgto = FormaPgto + Dt.Date.ToShortDateString() + "   R$" + string.Format("{0:N2}", decimal.Parse(Parcelas.Tables[0].Rows[I]["VlrOriginal"].ToString())) + "   " + Parcelas.Tables[0].Rows[I]["Documento"].ToString();
                             }*/

                            if (MessageBox.Show("Imprime Cupom Fiscal ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (FrmPrincipal.Parametros_Filial.EmissorCF == 3)
                                {
                                    if (EnviarMFe(Vendas.IdVenda))
                                    {
                                        for (int I = 0; I <= Frm.PagCartao.Rows.Count - 1; I++)
                                        {
                                            Frm.PagCartao.Rows[I]["ChaveCFe"] = ChaveCFe;
                                            Frm.PagCartao.Rows[I]["Id_Nota"] = NumNFCe;
                                            Controle.ExecutaSQL("Update PagamentoMFE set Id_Nota=" + NumNFCe.ToString() + " Where Id_Lanc=" + Frm.PagCartao.Rows[I]["IdLanc"].ToString());
                                        }

                                        if (!VerificarCartao(Frm.PagCartao))
                                            MessageBox.Show("Falha ao Enviar Pagamento do Cartão, a Venda ficarar pendendo para Envio do Pagamento do Cartão", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        ImprimirCFe(Vendas.IdVenda);
                                    }
                                }
                                else if (FrmPrincipal.Parametros_Filial.EmissorCF == 4)
                                    ImprimirNFce(Vendas.IdVenda);
                            }
                            else
                            {
                                if (MessageBox.Show("Imprime Pedido ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    if (FrmPrincipal.TipoImpResumida == "ELGIN_I9")
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
                                        ImpMiniImpBematech(Vendas.IdVenda);
                                }
                            }
                        }
                        Frm.Dispose();
                        FecharVenda();
                    }
                }
                catch
                {
                    FrmPrincipal.RegistrarAuditoria("Frente de Loja", Vendas.IdVenda, Vendas.NumDocumento, 4, "Frente Loja: Erro venda No."+IdVdImp.ToString()+" ou "+Vendas.IdVenda.ToString());                    
                    IdVdImp = 0;
                    FecharVenda();
                }
            }
            if (e.KeyCode == Keys.F9 && StaCaixa == StatusCaixa.EmVenda)
            {
                if (MessageBox.Show("Confirma o cancelamento da venda ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MessageBox.Show("Venda Cancelada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FecharVenda();
                }
            }
        }

        public bool VerificarCartao(DataTable TabPag)
        {

            bool VendaOk = true;

            for (int I = 0; I <= TabPag.Rows.Count - 1; I++)
            {
                if (int.Parse(TabPag.Rows[I]["MFE"].ToString()) == 3 || int.Parse(TabPag.Rows[I]["MFE"].ToString()) == 4)
                {                    
                    FrmMFeEnviarPgCartao FrmCartao = new FrmMFeEnviarPgCartao();
                    FrmCartao.FrmFrenteLoja = this;
                    FrmCartao.PagCartao     = TabPag.Rows[I];
                    FrmCartao.VlrVenda      = TotVenda - DescVenda;
                    FrmCartao.ShowDialog();
                    Controle.ExecutaSQL("Update PagamentoMFE set IdPagMFE=" + FrmCartao.IdPagamento.ToString() + ",IdRespMFe=" + FrmCartao.IdRespFiscal.ToString() +" where Id_Lanc=" + TabPag.Rows[I]["IdLanc"].ToString());                    
                    FrmCartao.Dispose();                    
                }
            }
            return VendaOk;
        }
        public void ImpMiniImpBematech(int IdVenda)
        {
            //try
            {
                Controle_Dados.MvVenda Vendas = new Controle_Dados.MvVenda();
                Vendas.Controle = Controle;
                DataSet TabItens = new DataSet();
                TabItens = Controle.ConsultaTabela(Vendas.SqlRelatorio(IdVenda));

                bool ImpCab = true;
                Controles.ImpressoraFiscal MiniImp = new ImpressoraFiscal();
                string TipoItem = "";

                int Lin = 0;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (ImpCab)
                    {
                        MiniImp.ImpMiniBemacth("Data:" + DateTime.Parse(TabItens.Tables[0].Rows[I]["Data"].ToString()).ToShortDateString() + " Doc.: " + TabItens.Tables[0].Rows[I]["NumDocumento"].ToString().Trim() + "/" + string.Format("{0:D6}", int.Parse(TabItens.Tables[0].Rows[I]["Id_Venda"].ToString())) + "    " + TabItens.Tables[0].Rows[I]["Movimento"].ToString().Trim(), FrmPrincipal.PortaImpResumida, 1);
                        MiniImp.ImpMiniBemacth("Cliente.: " + Controle.Space(TabItens.Tables[0].Rows[I]["Id_Pessoa"].ToString().Trim() + "-" + TabItens.Tables[0].Rows[I]["Pessoa"].ToString().Trim(), 55), FrmPrincipal.PortaImpResumida, 1);
                        MiniImp.ImpMiniBemacth("Endereco: " + Controle.Space(TabItens.Tables[0].Rows[I]["Endereco"].ToString().Trim() + " No.: " + TabItens.Tables[0].Rows[I]["Numero"].ToString(), 55), FrmPrincipal.PortaImpResumida, 1);
                        MiniImp.ImpMiniBemacth("CNPJ/CPF: " + TabItens.Tables[0].Rows[I]["CNPJCPF"].ToString() + " Insc.Estadual:" + TabItens.Tables[0].Rows[I]["InscUF"].ToString(), FrmPrincipal.PortaImpResumida, 1);
                        MiniImp.ImpMiniBemacth(" ", FrmPrincipal.PortaImpResumida, 1);
                        MiniImp.ImpMiniBemacth("Ref.  Produto                          Qtde.   Vr.Unit.  Vr.Total", FrmPrincipal.PortaImpResumida, 1);
                        MiniImp.ImpMiniBemacth("-----------------------------------------------------------------", FrmPrincipal.PortaImpResumida, 1);
                        ImpCab = false;
                    }
                    if (TipoItem != TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim())
                    {
                        if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "S")
                            MiniImp.ImpMiniBemacth("*** Saida ***", FrmPrincipal.PortaImpResumida, 1);
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "E")
                            MiniImp.ImpMiniBemacth("*** DEVOLUÇÃO ***", FrmPrincipal.PortaImpResumida, 1);
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "N")
                            MiniImp.ImpMiniBemacth("*** SEM MOVIMENTO ***", FrmPrincipal.PortaImpResumida, 1);
                    }
                    string Descricao = TabItens.Tables[0].Rows[I]["Descricao"].ToString().Trim().Replace("ç", "c").Replace("Ç", "C").Replace("á", "a").Replace("Á", "A").Replace("ã", "a").Replace("Â", "A").Replace("õ", "o").Replace("Õ", "O").Replace("é", "e").Replace("É", "E");
                    MiniImp.ImpMiniBemacth(Controle.Space(TabItens.Tables[0].Rows[I]["Referencia"].ToString(), 8) + " " + Controle.Space(Descricao, 25) + "    " +
                                 Controle.NumSpace(string.Format("{0:N3}", decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString())).ToString(), 6) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString())).ToString(), 8) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["TotalItem"].ToString())).ToString(), 8), FrmPrincipal.PortaImpResumida, 1);
                    TipoItem = TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim();
                }
                MiniImp.ImpMiniBemacth("-----------------------------------------------------------------", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 24) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 10), FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("                                     (-) Desconto  R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrDesconto"].ToString())).ToString(), 10), FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("                                                      -----------", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("                                     (=) Total     R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrTotal"].ToString())).ToString(), 10), FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("-----------------------------------------------------------------", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("Documento sem valor Fiscal", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
                MiniImp.ImpMiniBemacth("  ", FrmPrincipal.PortaImpResumida, 1);
            }
            //catch
            {
            }
        }

        private void TxtQtde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                //TxtCodBarra.Focus();
                //RegistrarItem();
                //BuscaProduto();
                if (TxtQtde.Text.Trim() == "")
                    TxtQtde.Value = 1;
                TxtQtde.Enabled = false;
                RegistrarItem(0);
                TxtCodBarra.Focus();
            }
        }
        private void AtualizarTela()
        {
            PnlVenda.Enabled = StaCaixa != StatusCaixa.Fechado;
            LblMensagem.Text = "";
            LblCliente.Text = "Cliente: ";
            if (StaCaixa == StatusCaixa.Aberto)
                LblMensagem.Text = "Próximo Cliente";
            else if (StaCaixa == StatusCaixa.Fechado)
                LblMensagem.Text = "Caixa Fechado";
        }
        private bool BuscaProduto()
        {
            if (TxtCodBarra.Text.Trim() != "")
            {
                CadPrd.LerDados(TxtCodBarra.Text.Trim());
                if (CadPrd.IdProduto > 0)
                {
                    TxtProduto.Text = CadPrd.Descricao.Trim();
                    if (!AltPreco)
                        TxtVlrUnt.Value = CadPrd.PrcSensacional;

                    if (IdPromocao != 0)
                    {
                        SqlDataReader TabPromocao = Controle.ConsultaSQL("select * from PromocaoProdutosItens T1" +
                                                                         " WHERE T1.Id_Promocao=" + IdPromocao.ToString() + " AND T1.Id_Produto=" + CadPrd.IdProduto.ToString());

                        while (TabPromocao.Read())
                        {
                            TxtVlrUnt.Value = decimal.Parse(TabPromocao["PrcSensacional"].ToString());
                        }
                    }
                    TxtVlrTotal.Value = TxtQtde.Value * TxtVlrUnt.Value;
                    return true;
                }
                else
                {
                    MessageBox.Show("Produto não Localizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCodBarra.Text = "";
                    TxtCodBarra.Focus();
                }
            }
            return false;
            //TxtCodBarra.Text  = "";
            //TxtQtde.Value     = 1;
            //TxtCodBarra.Focus();
        }
        public void RegistrarItem(int IdItem)
        {
            if (StaCaixa == StatusCaixa.Aberto)
                IniciaVenda();

            //if (!AltPreco)
            //    TxtVlrUnt.Value = CadPrd.PrcEspecial;

            TxtVlrTotal.Value = Math.Round(TxtQtde.Value * TxtVlrUnt.Value,2);
            TotItens = TotItens + 1;
            TotVenda = TotVenda + TxtVlrTotal.Value;
            AltPreco = false;
            // Mostra o Item
            TabItens.Rows.Add(TotItens, CadPrd.IdProduto, CadPrd.Referencia, CadPrd.Descricao, TxtQtde.Value, TxtVlrUnt.Value, TxtVlrDesc.Value, TxtVlrTotal.Value, CadPrd.Custo, CadPrd.PrcMinimo, CadPrd.PrcVarejo, CadPrd.PrcAtacado, CadPrd.PrcEspecial, CadPrd.PrcSensacional, IdItem, 1);
            StringBuilder cupom = new StringBuilder(200);
            cupom.AppendFormat("{0:000}", TotItens);
            cupom.Append(' ', 2);
            cupom.AppendFormat(Controle.Space(CadPrd.Descricao, 29));
            cupom.Append(' ', 2);
            cupom.AppendFormat(string.Format("{0:N3}",TxtQtde.Value).PadLeft(7, ' '));
            cupom.AppendFormat(TxtVlrTotal.Value.ToString("N").PadLeft(12, ' '));
            cupom.Append('\n');
            AtualizarDisplay(cupom.ToString());
            Application.DoEvents();
            TxtCodBarra.Text = "";
            TxtQtde.Value = 1;
            TxtCodBarra.Focus();
        }
        //Procedimento da venda
        private void IniciaVenda()
        {
            Vendas.LerDados(0);
            DescVenda = 0;
            IdVdImp   = 0;
            StaCaixa = StatusCaixa.EmVenda;
            AtualizarTela();
            MontarCabecalhoCupom();
            FrmPesqPessoa.CadPessoa.LerDados(FrmPesqPessoa.CadPessoa.IdPessoa);
            LblCliente.Text  = "Cliente: " + FrmPesqPessoa.CadPessoa.RazaoSocial;
            PnlTroco.Visible = false;

            //Criando a Tabela de Itens
            TabItens = new DataTable();
            TabItens.Columns.Add("ITEM", Type.GetType("System.Int32"));
            TabItens.Columns.Add("ID_PRODUTO", Type.GetType("System.Int32"));
            TabItens.Columns.Add("REFERENCIA", Type.GetType("System.String"));
            TabItens.Columns.Add("DESCRICAO", Type.GetType("System.String"));
            TabItens.Columns.Add("QTDE", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("VLRUNITARIO", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("VLRDESCONTO", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("VLRTOTAL", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("PRCCUSTO", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("PRCMINIMO", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("PRCVAREJO", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("PRCATACADO", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("PRCESPECIAL", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("PRCSENSACIONAL", Type.GetType("System.Decimal"));
            TabItens.Columns.Add("IDITEM", Type.GetType("System.Int32"));
            TabItens.Columns.Add("STATUS", Type.GetType("System.Int32")); // 0-CANCELADO / 1-NORMAL
        }
        private void FecharVenda()
        {
            TxtProduto.Text = "";
            //PnlTroco.Visible = false;
            TotItens           = 0;
            TotVenda           = 0;
            TxtQtde.Value      = 1;
            TxtVlrUnt.Value    = 0;
            TxtVlrDesc.Value   = 0;
            TxtVlrTotal.Value  = 0;
            DescVenda          = 0;
            IdVdImp            = 0;
            TxtCupom.Text      = "";
            LblTotalVenda.Text = "0,00";
            LblNumItens.Text   = "0";
            StaCaixa           = StatusCaixa.Aberto;
            FrmPesqPessoa.CadPessoa.IdPessoa = FrmPrincipal.Parametros_Filial.IdConsumidor;
            AtualizarTela();
            Application.DoEvents();
        }
        private void MontarCabecalhoCupom()
        {
            StringBuilder cupom = new StringBuilder(500);
            //cupom.Append(cliche);
            cupom.Append("CNPJ:");
            cupom.Append(CadFilial.Cnpj);
            cupom.Append('\n');
            cupom.Append("IE:");
            cupom.Append(CadFilial.InscUF);
            cupom.Append('\n');
            cupom.Append('-', 55);
            cupom.Append('\n');
            cupom.Append(' ', 19);
            cupom.Append("CUPOM FISCAL");
            cupom.Append('\n');
            cupom.Append("ITEM  ");
            cupom.Append("DESCRIÇÃO                         ");
            cupom.Append("QTD  ");
            cupom.Append("TOTAL ITEM");
            cupom.Append('\n');
            cupom.Append('-', 55);
            cupom.Append('\n');
            AtualizarDisplay(cupom.ToString());
        }
        public void AtualizarDisplay(string mensagem)
        {
            TxtCupom.ScrollToCaret();
            TxtCupom.Focus();
            TxtCupom.SelectedText = mensagem;
            TxtCupom.ScrollToCaret();
            TxtCodBarra.Focus();
            LblNumItens.Text = string.Format("{0:D6}", TotItens);
            LblTotalVenda.Text = string.Format("{0:N2}", TotVenda);
            Application.DoEvents();
        }
        private void FrmFrenteLoja_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (StaCaixa == StatusCaixa.EmVenda)
            {
                e.Cancel = true;
            }
        }
        private void TxtVlrDesc_Validated(object sender, EventArgs e)
        {
            if (TxtVlrDesc.Text.Trim() == "")
                TxtVlrDesc.Value = 0;
            TxtVlrDesc.Enabled = false;
            TxtCodBarra.Focus();
        }
        public void ImprimirNFce(int IdVenda)
        {
            Controle_Dados.NotaFiscal CadNota = new Controle_Dados.NotaFiscal();
            NFce ImpNFce = new NFce();
            ImpNFce.Controle = Controle;
            CadNota.Controle = Controle;

            //Gerando a NFce
            int NumNFCe = ImpNFce.GerarNFce(IdVenda,2);

            if (NumNFCe > 0)
            {
                CadNota.LerDados(NumNFCe);
                ImpNFce.Inicializar_parametros(FrmPrincipal.Perfil_Usuario.IdFilial);
                string XmlNota = ImpNFce.GerarXmlNFce(NumNFCe, CadFilial, 1);
                ImpNFce.EnviarNFce(XmlNota);
                Controle.ExecutaSQL("Update NotaFiscal set ChaveNFe='" + ImpNFce.nChaveNF + "' Where Id_Nota=" + NumNFCe.ToString());

                if (ImpNFce.nProtocoloNF != "" && ImpNFce.nReciboNF != "" && int.Parse(ImpNFce.cStat) != 0 && (int.Parse(ImpNFce.cStat) <= 104 || int.Parse(ImpNFce.cStat) == 302))
                {
                    Controle.ExecutaSQL("Update NotaFiscal set ReciboNFe='" + ImpNFce.nReciboNF + "',ProtocoloNFe='" + ImpNFce.nProtocoloNF + "' Where Id_Nota=" + CadNota.IdNota.ToString());
                    ImpNFce.GravarXmlRetornoNFE(CadNota.NumNota, XmlNota, ImpNFce.vXMLRetorno);
                    CadNota.Concluir();

                    // Imprimir
                    if (InterfaceEpsonNF.IniciaPorta("USB") != 1)
                        MessageBox.Show("Erro ao abrir a porta de comunicação.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        try
                        {
                            InterfaceEpsonNF.EPSON_NFCe_Imprimir(ImpNFce.UltXmlPrc, "C");                            
                        }
                        catch (Exception e)
                        {
                            InterfaceEpsonNF.FechaPorta();
                            MessageBox.Show("Falha ao imprimirNota Cupom Fiscal Eletrônica: " + e.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        };
                        InterfaceEpsonNF.FechaPorta();
                    }
                }
                else
                {
                    MessageBox.Show("Cupom Fiscal Eletrônica não foi transmitida, Motivo: " + ImpNFce.vMotivoRet, "Falha", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return;
                }
            }
        }

        public void ImprimirCFe(int IdVenda)
        {
            string sSql1 = "SELECT T4.Filial,T4.Fantasia,T4.Cnpj AS CNPJFILIAL,T4.INSC_UF AS IEFILIAL ,RTRIM(T4.Endereco)+','+RTRIM(T4.Numero)+' '+RTRIM(T4.Complemento)+' - '+RTRIM(T4.Bairro)+' - '+RTRIM(T4.CIDADE)+' CEP:'+RTRIM(T4.Cep) AS EndFilial,T2.CNPJCPF,T2.PESSOA,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO,T2.BAIRRO,T2.VLRDESCONTO,T2.CREDITO,T3.REFERENCIA,"+
                           " T3.DESCRICAO,T3.SITTRIBUTARIA,T3.ICMSISS,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL AS TOTALITEM, T2.VLRTOTAL AS VLRVENDA,T2.VLRSUBTOTAL,"+
                           " T1.ID_PRODUTO,T3.NCM,T1.ID_VENDA,T3.UNIDADE,T5.QrCode,SUBSTRING(T5.ChaveNFE,4,44) as ChaveNFe," +
                           " (Select isnull(Sum(Valor), 0) From PagamentoMFE where id_venda = t1.id_venda) as VlrRecbido FROM MVVENDAITENS T1"+
                           " LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                           " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)"+
                           " LEFT JOIN Empresa_Filial T4 ON (T4.ID_FILIAL=T2.ID_FILIAL)"+
                           " left join NotaFiscal t5 on (T5.id_nota=T2.id_lancCF) WHERE T1.ID_VENDA=" + IdVenda.ToString();

            string sSql2 = "SELECT T1.ID_VENDA,T1.VALOR,T2.ID_DOCUMENTO,T2.DOCUMENTO,T2.CODECF,T1.NPARC FROM PAGAMENTOMFE T1 "+
                           " LEFT JOIN TIPODOCUMENTO T2 ON(T2.ID_DOCUMENTO = T1.ID_DOCUMENTO) WHERE T1.ID_VENDA = " + IdVenda.ToString();

            
            FrmRelatorios FrmRel = new FrmRelatorios();            
            ERP_SGE.Relatorios.RelCFeSAT Rel001 = new ERP_SGE.Relatorios.RelCFeSAT();            
            DataSet TabRel = new DataSet();
            DataSet TabPrd = new DataSet();
            TabRel = Controle.ConsultaTabela(sSql1);
            TabPrd = Controle.ConsultaTabela(sSql2);
            Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
            Rel001.Database.Tables[1].SetDataSource(TabPrd.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = Rel001;
            /*((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblPeriodo"])).Text = "Período de Venda:" + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;*/
            //((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section1.ReportObjects["QrCode"])).image = FrmPrincipal.Rel_RodaPe;                         
            // ((CrystalDecisions.CrystalReports.Engine.PrintOptions)(Rel001.PrintOptions)).PrinterName = "TP-450";
            FrmRel.ShowDialog();

            Rel001.Dispose();
        }

        private bool EnviarMFe(int IdVenda)
        {
            try
            {
                Controle_Dados.NotaFiscal CadNota = new Controle_Dados.NotaFiscal();
                NFce ImpMFe = new NFce();
                ImpMFe.Controle = Controle;
                CadNota.Controle = Controle;

                //Gerando a NFce
                NumNFCe = ImpMFe.GerarNFce(IdVenda, 3);

                if (NumNFCe > 0)
                {
                    CadNota.LerDados(NumNFCe);
                    ImpMFe.Inicializar_parametros(FrmPrincipal.Perfil_Usuario.IdFilial);
                    string XmlNota = ImpMFe.GerarXmlMFE(NumNFCe, CadFilial, 1);
                    string retorno = Marshal.PtrToStringAnsi(CFeSatDll.EnviarDadosVenda(ImpMFe.NrSessao, ImpMFe.ParamNFE.CodigoMFe.ToString(), XmlNota));
                    
                    string[] ParamRet = ImpMFe.GravarXmlRetornoMFE(CadNota.NumNota, retorno);

                    if (ParamRet[1].ToString() == "06000")
                    {
                        ChaveCFe = ParamRet[8].ToString();
                        Controle.ExecutaSQL("Update NotaFiscal set ChaveNFe='" + ParamRet[8].ToString() + "' Where Id_Nota=" + CadNota.IdNota.ToString());
                        CadNota.Concluir();

                        XmlDocument oXML = new XmlDocument();
                        oXML.Load(ImpMFe.UltXmlPrc);
                        try
                        {
                            string QrCode = oXML.GetElementsByTagName("assinaturaQRCODE")[0].InnerText;
                            string ChaveQrCode = ParamRet[8].ToString().Trim() + "|" + oXML.GetElementsByTagName("dEmi")[0].InnerText.Trim() + oXML.GetElementsByTagName("hEmi")[0].InnerText.Trim() + "|" + string.Format("{0:N2}", Vendas.VlrTotal).Replace(",", ".") + "||" + QrCode.Trim();
                            GravandoQrCode(CadNota.IdNota, ChaveQrCode);                            
                        }     
                        catch
                        {

                        }
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Cupom Fiscal Eletrônico não Transmitido, Motivo:" + ParamRet[3].ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Cupom Fiscal Eletrônico não Transmitido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Falha ao Enviar o CF-e , Erro:" + erro.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void GravandoQrCode(int IdNota,string QrTxt)
        {
            PictureBox PicFoto = new PictureBox();
            QRCodeEncoder Qr = new QRCodeEncoder();

            Bitmap Qrcode = Qr.Encode(QrTxt);
            PicFoto.Image = Qrcode as Image;

            MemoryStream ms = new MemoryStream();
            PicFoto.Image.Save(ms, ImageFormat.Jpeg);
            byte[] photo_aray = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(photo_aray, 0, photo_aray.Length);
            SqlCommand CmdSql = new SqlCommand("Update NotaFiscal set QrCode=@QrCode WHERE id_nota=" + IdNota.ToString(), Controle.Conexao);
            CmdSql.Parameters.AddWithValue("@QrCode", photo_aray);
            CmdSql.ExecuteNonQuery();
        }

    }
}
