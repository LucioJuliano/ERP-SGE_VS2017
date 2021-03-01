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
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.IO;

namespace ERP_SGE
{
    public partial class FrmNotaFiscal : Form
    {
        Funcoes Controle   = new Funcoes();
        Pessoas CadPessoa  = new Pessoas();
        NotaFiscal CadNota = new NotaFiscal();
        Estados CadUF      = new Estados();
        NotaFiscalItens ItemCadNota = new NotaFiscalItens();        
        Parametros ParamFilial      = new Parametros();
        ReducaoFiscal RedFiscal     = new ReducaoFiscal();
                        

        public TelaPrincipal FrmPrincipal;        
        public bool StaFormEdicao = false;
        private int CfopItem      = 0;

        private DataSet TabItens;
        private BindingSource Source_Itens;

        public FrmNotaFiscal()
        {
            InitializeComponent();            
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao      = FrmPrincipal.Conexao;            
            CadPessoa.Controle    = Controle;
            CadNota.Controle      = Controle;
            ItemCadNota.Controle  = Controle;
            ParamFilial.Controle  = Controle;
            RedFiscal.Controle    = Controle;
            CadUF.Controle        = Controle;
            TabItens              = new DataSet();
            Source_Itens          = new BindingSource();
            Chk_Periodo.Checked   = false;
            Chk_PeriodoNT.Checked = false;
            Rb_NtPendente.Checked = true;
            Rb_NFe.Checked        = true;
            Dt1.Value   = DateTime.Now.AddDays(-1);
            Dt2.Value   = DateTime.Now;
            DtNT1.Value = DateTime.Now;
            DtNT2.Value = DateTime.Now;                   
            CamposLista();
            PopularGridVenda();     
        }
        private void CamposLista()
        {
            LstTransportadora             = FrmPrincipal.PopularCombo("SELECT Id_Transportadora,RazaoSocial FROM Transportadoras ORDER BY RazaoSocial", LstTransportadora);
            LstPesqFilial                 = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqFilial, "Todas");
            LstPesqFilialNT               = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqFilialNT, "Todas");
            LstFilial                     = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial);
            LstCfop                       = FrmPrincipal.PopularCombo("SELECT Id_Cfop,Cfop+'  '+Natureza FROM Cfop ORDER BY Natureza", LstCfop);
            LstUF                         = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUF);
            LstUFDesc                     = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUFDesc);
            LstUFPercurso                 = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUFPercurso);
            LstPesqFilial.SelectedValue   = FrmPrincipal.Perfil_Usuario.IdFilial.ToString();
            LstPesqFilialNT.SelectedValue = FrmPrincipal.Perfil_Usuario.IdFilial.ToString();
            LstFilial.SelectedValue       = FrmPrincipal.Perfil_Usuario.IdFilial.ToString();
            LstPais                       = FrmPrincipal.PopularCombo("SELECT CHAVE,DESCRICAO AS PAIS FROM TABELASAUX WHERE CAMPO='PAIS' ORDER BY DESCRICAO", LstPais);
            LstCfopItem                   = FrmPrincipal.PopularComboGrid("SELECT Id_Cfop,Cfop+'  '+Natureza FROM Cfop ORDER BY Natureza", LstCfopItem);
            ParamFilial.LerDados(int.Parse(FrmPrincipal.Perfil_Usuario.IdFilial.ToString()));
            BtnNFE.Visible = ParamFilial.NFE == 1;

            DataTable TabST = new DataTable();
            TabST.Columns.Add("IdST", Type.GetType("System.Int32"));
            TabST.Columns.Add("SitTrib", Type.GetType("System.String"));
            TabST.Rows.Add(0, "Tributado");
            TabST.Rows.Add(1, "Não Tributado");
            TabST.Rows.Add(2, "Isento");
            TabST.Rows.Add(3, "Substituição");            
            ColST.DataSource    = TabST;
            ColST.DisplayMember = "SitTrib";
            ColST.ValueMember   = "IdST";

            DataTable TabCST     = Controle.LstCST();
            LstCst.DataSource    = TabCST;
            LstCst.DisplayMember = "DescCST";
            LstCst.ValueMember = "CST";
        }
        private void PopularGridVenda()
        {
            string Filtro = "";
            Filtro = "WHERE T1.IMPNF = 1";
            if (TxtPesqNumDoc.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMDOCUMENTO LIKE '%" + TxtPesqNumDoc.Text.Trim() + "%'";
            if (TxtPesqNumVd.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_VENDA =" + TxtPesqNumVd.Text.Trim()+ " AND T1.STATUS IN (2,3)";
            else
                Filtro = Filtro + " AND T1.STATUS = 2";

            if (TxtPesqPessoa.Text.Trim() != "")
                Filtro = Filtro + " AND T2.RAZAOSOCIAL Like '%" + TxtPesqPessoa.Text.Trim() + "%'";
            
            if (int.Parse(LstPesqFilial.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FILIAL=" + LstPesqFilial.SelectedValue.ToString();
            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,T1.DATA,T1.NUMDOCUMENTO,T1.FORMNF,T1.ID_PESSOA,T2.RAZAOSOCIAL as Cliente,T1.VLRTOTAL,T4.VENDEDOR,T1.PREVENTREGA,T6.FANTASIA AS FILIAL,T5.USUARIO FROM MVVENDA T1  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                             " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR) LEFT JOIN USUARIOS T5 ON (T5.ID_USUARIO=T1.ID_USUARIO) LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T1.ID_FILIAL)" + Filtro + " ORDER BY T1.ID_VENDA DESC");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridVenda.DataSource = Source;
            int item = Source.Find("Id_Venda", CadNota.IdVenda);
            Source.Position = item;
        }
        private void PopularGrid()
        {
            string Filtro = "";
            if (Rb_NtPendente.Checked)
                Filtro = "WHERE T1.STATUS = 0";
            else if (Rb_NtEmitida.Checked)
                Filtro = "WHERE T1.STATUS = 1";
            else if (Rb_NtCancelada.Checked)
                Filtro = "WHERE T1.STATUS = 2";
            else
                Filtro = "WHERE 1=1";

            if (Rb_NFe.Checked)
                Filtro = Filtro + " AND T1.NFE=1";
            if (Rb_NFce.Checked)
                Filtro = Filtro + " AND T1.NFE=2";

            if (TxtPesqNumVenda.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_VENDA =" + TxtPesqNumVenda.Text.Trim();
            if (TxtPesqNumNota.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMNOTA =" + TxtPesqNumNota.Text.Trim();
            if (TxtPesqNumForm.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMFORMULARIO =" + TxtPesqNumForm.Text.Trim();
            if (TxtPesqPessoaNT.Text.Trim() != "")
                Filtro = Filtro + " AND T2.RAZAOSOCIAL Like '%" + TxtPesqPessoaNT.Text.Trim() + "%'";
            if (int.Parse(LstPesqFilialNT.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FILIAL=" + LstPesqFilialNT.SelectedValue.ToString();
            if (Chk_PeriodoNT.Checked)
                Filtro = Filtro + " AND T1.DTEMISSAO >= Convert(DateTime,'" + DtNT1.Value.Date.ToString() + "',103) AND T1.DTEMISSAO <= Convert(DateTime,'" + DtNT2.Value.Date.ToString() + "',103)";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_NOTA,T1.ID_VENDA,T1.DTEMISSAO,CASE T1.STATUS WHEN 0 THEN 'Pendente' WHEN 1 THEN 'Emitida' WHEN 2 THEN 'Cancelada' END AS STATUS,T1.NUMNOTA,T1.NUMFORMULARIO,T2.RAZAOSOCIAL AS PESSOA,T1.VLRNOTA,T1.DATACANCEL,T3.FANTASIA AS FILIAL,T1.PROTOCOLONFE,T1.ProtocoloCanc FROM NOTAFISCAL T1" +
                                             "  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)  LEFT JOIN EMPRESA_FILIAL T3 ON (T3.ID_FILIAL=T1.ID_FILIAL) " + Filtro + " ORDER BY T1.NUMNOTA");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Nota", CadNota.IdNota);
            Source.Position = item;
        }
        private void BtnPesqVD_Click(object sender, EventArgs e)
        {
            PopularGridVenda();
        }
        private void Frm_Activated(object sender, EventArgs e)
        {            
            FrmPrincipal.LimpaClickBotoes();
            if (PagPesq.SelectedIndex > 0)
            {
                FrmPrincipal.ClickBtnNovo += new EventHandler(this.BtnNovo_Click);
                FrmPrincipal.ClickBtnEditar += new EventHandler(this.BtnEditar_Click);
                FrmPrincipal.ClickBtnGravar += new EventHandler(this.BtnGravar_Click);
                FrmPrincipal.ClickBtnExcluir += new EventHandler(this.BtnExcluir_Click);
                FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
                FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
                FrmPrincipal.ControleBotoes(StaFormEdicao);
            }
            else
            {
                FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
                FrmPrincipal.BtnFechar.Enabled = !StaFormEdicao;
            }
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }        
        private void PopularCampos(int Isn)
        {            
            BoxPesquisa.Enabled = true;
            CadNota.LerDados(Isn);
            TxtCodigo.Text          = CadNota.IdNota.ToString();
            TxtCodCliente.Text      = CadNota.IdPessoa.ToString();
            TxtData.Value           = CadNota.Data;

            if (CadNota.IdNota > 0)
                TxtDtEmissao.Value = CadNota.DtEmissao;
            else
                TxtDtEmissao.Value = FrmPrincipal.DtHrServidor();

            TxtNumFormulario.Value  = CadNota.NumFormulario;
            TxtNumNotaFiscal.Value  = CadNota.NumNota;
            TxtIdVenda.Value        = CadNota.IdVenda;
            LstFilial.SelectedValue = CadNota.IdFilial.ToString();
            LstCfop.SelectedValue   = CadNota.IdCfop.ToString();

            if (CadNota.Frete == 0) Rb_Emitente.Checked   = true; else Rb_Destinatario.Checked = true;
            if (CadNota.EntSaida == 0) Rb_NtSaida.Checked = true; else Rb_NtEntrada.Checked    = true;

            TxtVlrProduto.Value   = CadNota.VlrProdutos;
            TxtVlrDesconto.Value  = CadNota.VlrDesconto;
            TxtVlrNota.Value      = CadNota.VlrNota;
            TxtObservacao.Text    = CadNota.Observacao;

            TxtBIcms.Value        = CadNota.BIcms;
            TxtVlrIcms.Value      = CadNota.VlrIcms;
            TxtBIcmsSub.Value     = CadNota.BIcmsSub;
            TxtVlrIcmsSub.Value   = CadNota.VlrIcmsSub;
            TxtVlrFrete.Value     = CadNota.VlrFrete;
            TxtVlrSeguro.Value    = CadNota.VlrSeguro;
            TxtVlrOutraDesp.Value = CadNota.VlrOutraDesp;
            TxtVlrIpi.Value       = CadNota.VlrIpi;
            TxtVlrAcresicmo.Value = CadNota.VlrAcrescimo;

            TxtCnpj.Text          = CadNota.CnpjCpf;
            TxtNmPessoa.Text      = CadNota.NmPessoa;
            TxtInscUF.Text        = CadNota.InscUf;
            TxtCep.Text           = CadNota.Cep;
            TxtEndereco.Text      = CadNota.Endereco;
            TxtNumero.Text        = CadNota.Numero;
            TxtComplemento.Text   = CadNota.Compl;
            TxtCidade.Text        = CadNota.Cidade;
            TxtBairro.Text        = CadNota.Bairro;
            LstPais.SelectedValue = CadNota.Pais;
            TxtFone.Text          = CadNota.Telefone;            
            LstUF.SelectedValue   = CadNota.IdUf.ToString();            
            TxtQtdeVolume.Value   = CadNota.QtdeVolume;
            TxtPesoBruto.Value    = CadNota.PesoBruto;
            TxtPesoLiquido.Value  = CadNota.PesoLiquido;
            TxtEspecie.Text       = CadNota.Especie;
            TxtMarca.Text         = CadNota.Marca;
            TxtChaveDev.Text      = CadNota.ChaveNfeDev;
            TxtCodMun.Value       = CadNota.CodMun;
            TxtNumPedido.Text     = CadNota.NumPedido;
            TxtICMSInterno.Value  = CadNota.ICMSInterno;
            TxtPercDifal.Value    = CadNota.PercDifal;
            TxtProtocoloCarta.Text = CadNota.ProtocoloCarta;
            TxtCartaCorrecao.Text = CadNota.CartaCorrecao;
            TxtVencFatura.Text    = CadNota.VencFatura;

            if (CadNota.ICMSInterno == 0)
            {
                CadUF.LerDados(CadNota.IdUf);
                TxtICMSInterno.Value = CadUF.ICMSInterno;
                TxtPercDifal.Value   = CadUF.PercDifal;
            }

            LstNatureza.SelectedIndex        = CadNota.NatOp;
            LstTransportadora.SelectedValue  = CadNota.IdTransportadora.ToString();
            LstTipoAtendimento.SelectedIndex = CadNota.Atendimento;
            LstDestinoOperacao.SelectedIndex = CadNota.DestOperacao;
            LstFinalidade.SelectedIndex      = CadNota.Finalidade;
            LstMeioPag.SelectedIndex         = CadNota.MeioPag;
            if (CadNota.Consumidor == 0) Rb_ConsNao.Checked = true; else Rb_ConsSim.Checked = true;
            SetaPessoa(CadNota.IdPessoa);

            //Ck_NFE.Visible = ParamFilial.NFE == 1;
            //Ck_NFE.Enabled = CadNota.IdNota == 0;
            //Ck_NFE.Checked = CadNota.NFE == 1;

            Hab_Botoes();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularCampos(0);
            TxtEspecie.Text = "VARIADAS";
            TxtMarca.Text   = "VARIADAS";
            PopularGridItens();
            FrmPrincipal.ControleBotoes(true);
            LstCfop.Focus();
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
                if (Paginas.SelectedIndex == 1)
                    PopularCampos(int.Parse(TxtCodigo.Text));
                else
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                Paginas.SelectTab(1);
               
                {
                    PopularGridItens();
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    TxtObservacao.Focus();
                    Hab_Botoes();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        { 

            if (LstMeioPag.SelectedIndex==0 && FrmPrincipal.VersaoDistribuidor)
            {
                MessageBox.Show("Favor informar o Meio de Pagamento", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (LstFilial.SelectedValue.ToString() == "0")
                MessageBox.Show("Favor informar a Filial", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            else
            {
               // if (int.Parse(TxtCodCliente.Text) > 0)
               // {
                    CadNota.IdNota        = int.Parse(TxtCodigo.Text);
                    CadNota.IdPessoa      = int.Parse(TxtCodCliente.Text);
                    CadNota.Data          = TxtData.Value;
                    CadNota.DtEmissao     = TxtDtEmissao.Value;
                    CadNota.NumFormulario = int.Parse(TxtNumFormulario.Value.ToString());
                    CadNota.NumNota       = int.Parse(TxtNumNotaFiscal.Value.ToString());
                    CadNota.IdVenda       = int.Parse(TxtIdVenda.Value.ToString());
                    CadNota.IdFilial      = int.Parse(LstFilial.SelectedValue.ToString());
                    CadNota.IdCfop        = int.Parse(LstCfop.SelectedValue.ToString());

                    if (Rb_Emitente.Checked) CadNota.Frete = 0; else CadNota.Frete = 1;
                    if (Rb_NtSaida.Checked)  CadNota.EntSaida = 0; else CadNota.EntSaida = 1;

                    CadNota.VlrProdutos  = TxtVlrProduto.Value;
                    CadNota.VlrDesconto  = TxtVlrDesconto.Value;
                    CadNota.VlrNota      = TxtVlrNota.Value;
                    CadNota.Observacao   = TxtObservacao.Text;

                    CadNota.BIcms        = TxtBIcms.Value;
                    CadNota.VlrIcms      = TxtVlrIcms.Value;
                    CadNota.BIcmsSub     = TxtBIcmsSub.Value;
                    CadNota.VlrIcmsSub   = TxtVlrIcmsSub.Value;
                    CadNota.VlrFrete     = TxtVlrFrete.Value;
                    CadNota.VlrSeguro    = TxtVlrSeguro.Value;
                    CadNota.VlrOutraDesp = TxtVlrOutraDesp.Value;
                    CadNota.VlrIpi       = TxtVlrIpi.Value;
                    CadNota.VlrAcrescimo = TxtVlrAcresicmo.Value;

                    CadNota.CnpjCpf     = TxtCnpj.Text;
                    CadNota.NmPessoa    = TxtNmPessoa.Text;
                    CadNota.InscUf      = TxtInscUF.Text;
                    CadNota.Cep         = TxtCep.Text.Replace("-", ""); 
                    CadNota.Endereco    = TxtEndereco.Text;
                    CadNota.Numero      = TxtNumero.Text;
                    CadNota.Compl       = TxtComplemento.Text;
                    CadNota.Cidade      = TxtCidade.Text;
                    CadNota.Bairro      = TxtBairro.Text;
                    CadNota.Telefone    = TxtFone.Text;
                    CadNota.IdUf        = int.Parse(LstUF.SelectedValue.ToString());
                    CadNota.Pais        = LstPais.SelectedValue.ToString();
                    CadNota.QtdeVolume  = int.Parse(TxtQtdeVolume.Value.ToString());
                    CadNota.PesoBruto   = TxtPesoBruto.Value;
                    CadNota.PesoLiquido = TxtPesoLiquido.Value;
                    CadNota.Especie     = TxtEspecie.Text;
                    CadNota.Marca       = TxtMarca.Text;
                    CadNota.ChaveNfeDev = TxtChaveDev.Text;
                    CadNota.NumPedido   = TxtNumPedido.Text;
                    CadNota.ICMSInterno = TxtICMSInterno.Value;
                    CadNota.PercDifal   = TxtPercDifal.Value;
                    CadNota.CodMun      = int.Parse(TxtCodMun.Value.ToString());
                    CadNota.VencFatura  = TxtVencFatura.Text;
                

                    if (CadNota.ICMSInterno == 0)
                    {
                        CadUF.LerDados(CadNota.IdUf);
                        CadNota.ICMSInterno = CadUF.ICMSInterno;
                        CadNota.PercDifal = CadUF.PercDifal;
                    }


                    //if (Ck_NFE.Checked) CadNota.NFE = 1; else CadNota.NFE = 0;
                    CadNota.NFE = 1;
                    CadNota.IdTransportadora = int.Parse(LstTransportadora.SelectedValue.ToString());                    
                    CadNota.Atendimento      = LstTipoAtendimento.SelectedIndex;
                    CadNota.DestOperacao     = LstDestinoOperacao.SelectedIndex;
                    CadNota.Finalidade       = LstFinalidade.SelectedIndex;
                    CadNota.NatOp            = LstNatureza.SelectedIndex;
                    CadNota.MeioPag          = LstMeioPag.SelectedIndex;
                if (Rb_ConsNao.Checked) CadNota.Consumidor = 0; else CadNota.Consumidor = 1;
                    Ck_NFE.Checked = true;
                    StaFormEdicao  = false;

                    if (CadNota.IdNota == 0)
                    {
                        ParamFilial.LerDados(CadNota.IdFilial);
                        CadNota.Observacao = ParamFilial.ObsNF;
                        if (Ck_NFE.Checked)
                        {                            
                            if (ParamFilial.WSNumNFE == 1)
                            {
                                try
                                {
                                    Controles.GerarNumNF.GerarNumNF GerarN_NFE = new Controles.GerarNumNF.GerarNumNF();
                                    GerarN_NFE.Url = "http://" + FrmPrincipal.URLMatriz + "/ERP-SGE_WebService/GerarNumNF.asmx?WSDL";
                                    ArrayList NumNF = new ArrayList(GerarN_NFE.ProxNotaFiscal(CadNota.IdFilial, true));
                                    if (int.Parse(NumNF[0].ToString()) == 0)
                                    {
                                        MessageBox.Show("Atenção: Numero de Nota Fiscal não foi gerado, Favor verificar conexão com o servidor.", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    CadNota.NumNota = int.Parse(NumNF[0].ToString());
                                    CadNota.NumFormulario = int.Parse(NumNF[1].ToString());
                                }
                                catch
                                {
                                    MessageBox.Show("Atenção: Numero de Nota Fiscal não foi gerado, Favor verificar conexão com o servidor.", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                FrmPrincipal.Parametros_Filial.ProxNotaFiscal(CadNota.IdFilial, true);
                                CadNota.NumNota       = FrmPrincipal.Parametros_Filial.NotaNFE;
                                CadNota.NumFormulario = FrmPrincipal.Parametros_Filial.FormularioNFE;
                            }
                        }
                        else
                        {
                            FrmPrincipal.Parametros_Filial.ProxNotaFiscal(CadNota.IdFilial, false);
                            CadNota.NumNota = FrmPrincipal.Parametros_Filial.NotaFiscal;
                            CadNota.NumFormulario = FrmPrincipal.Parametros_Filial.Formulario;
                        }
                    }
                    /*CadNota.Consumidor = 1;
                    if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 4 || CadPessoa.Clie_Forn == 6 || CadPessoa.Clie_Forn == 7)
                        CadNota.Consumidor = 0;*/
                    CadNota.GravarDados();
                    //Registro de Auditoria                
                    if (int.Parse(TxtCodigo.Text) == 0)
                        FrmPrincipal.RegistrarAuditoria(this.Text, CadNota.IdNota, CadNota.NumNota.ToString(), 1, "Cadastro Nota Fiscal");
                    else
                        FrmPrincipal.RegistrarAuditoria(this.Text, CadNota.IdNota, CadNota.NumNota.ToString(), 2, "Alteração Nota Fiscal");
                    PopularCampos(CadNota.IdNota);
                    PopularGrid();
                    PopularGridItens();
                    FrmPrincipal.ControleBotoes(false);
                    GridItens.Focus();
               // }
               // else                
               //     MessageBox.Show("Favor informar o destinatário", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                CadNota.IdNota = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                CadNota.LerDados(CadNota.IdNota);
                if (CadNota.Status == 1)
                    MessageBox.Show("Nota Fiscal já foi emitida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (CadNota.Status == 2)
                    MessageBox.Show("Nota Fiscal já cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        CadNota.IdNota = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        CadNota.Excluir();
                        //Registro de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, CadNota.IdNota, CadNota.NumNota.ToString(), 3, "Excluir Nota Fiscal");
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
                LimpaDados();
                Paginas.SelectTab(0);
                GridDados.Focus();
                
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
            TxtCodigo.Text        = "0";
            TxtData.Value         = DateTime.Now;
            TxtDtEmissao.Value    = FrmPrincipal.DtHrServidor();
            TxtNumFormulario.Value= 0;
            TxtNumNotaFiscal.Value= 0;
            TxtIdVenda.Value      = 0;                        
            TxtVlrProduto.Value   = 0;
            TxtVlrDesconto.Value  = 0;
            TxtVlrNota.Value      = 0;
            TxtObservacao.Text    = "";

            TxtBIcms.Value        = 0;
            TxtVlrIcms.Value      = 0;
            TxtBIcmsSub.Value     = 0;
            TxtVlrIcmsSub.Value   = 0;
            TxtVlrFrete.Value     = 0;
            TxtVlrSeguro.Value    = 0;
            TxtVlrOutraDesp.Value = 0;
            TxtVlrIpi.Value       = 0;
            TxtVlrAcresicmo.Value = 0;

            TxtCnpj.Text          = "";
            TxtNmPessoa.Text      = "";
            TxtInscUF.Text        = "";
            TxtCep.Text           = "";
            TxtEndereco.Text      = "";
            TxtNumero.Text        = "";
            TxtComplemento.Text   = "";
            TxtCidade.Text        = "";
            TxtBairro.Text        = "";
            TxtFone.Text          = "";
            LstUF.SelectedValue   = "0";
            LstPais.SelectedValue = "1058";
            TxtQtdeVolume.Value   = 0;
            TxtPesoBruto.Value    = 0;
            TxtPesoLiquido.Value  = 0;
            TxtEspecie.Text       = "";
            TxtMarca.Text         = "";
            TxtChaveDev.Text      = "";
            TxtCodMun.Value       = 0;
            TxtNumPedido.Text     = "";
            TxtICMSInterno.Value  = 0;
            TxtPercDifal.Value    = 0;
            TxtVencFatura.Text    = "";
            Rb_ConsNao.Checked    = true;
            LstNatureza.SelectedIndex        = 0;
            LstTipoAtendimento.SelectedIndex = 0;
            LstDestinoOperacao.SelectedIndex = 0;
            LstTransportadora.SelectedValue  = "0";
            LstFinalidade.SelectedIndex      = 0;
            LstMeioPag.SelectedIndex         = 0;
            CadNota.LerDados(0);
            SetaPessoa(0);            
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
        private void PagPesq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PagPesq.SelectedIndex == 0)
            {
                BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
                FrmPrincipal.LimpaClickBotoes();
                FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
                FrmPrincipal.BtnFechar.Enabled = true;
                StaFormEdicao = false;
                PopularGridVenda();
            }
            else
            {
                Frm_Activated(null, null);
                PopularGrid();
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
                    Paginas.SelectTab(1);
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
        // Controle dos Itens
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T1.PICMS,T1.PIPI,T1.PERCRED,T1.SitTributaria,T1.ID_CFOP,IsNull(T1.CST,0) as CST,ISNULL(T1.ITEMPED,0) AS ITEMPED,T1.CODPRDCLIENTE " +
                                               " FROM NOTAFISCALItens T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.Id_Nota=" + CadNota.IdNota.ToString()+" ORDER BY T1.ID_ITEM");

            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource    = Source_Itens;
            Navegador.BindingSource = Source_Itens;

            int item = Source_Itens.Find("ID_Item", ItemCadNota.IdItem);
            Source_Itens.Position = item;
            Hab_Botoes();

            //Atualizando o Total do Movimento
            decimal VlrProduto = 0;
            for (int I = 0; I <= GridItens.RowCount - 1; I++)
            {
                VlrProduto = VlrProduto + decimal.Parse(GridItens.Rows[I].Cells[5].Value.ToString());
            }
            CadNota.VlrProdutos = VlrProduto;
            CadNota.VlrNota     = VlrProduto - CadNota.VlrDesconto;
            TxtVlrProduto.Value = VlrProduto;
            TxtVlrNota.Value    = VlrProduto - CadNota.VlrDesconto;
            if (CadNota.IdNota > 0)
            {
                CadNota.GravarDados();
                PopularCampos(CadNota.IdNota);
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
            if (StaFormEdicao)
            {
                MessageBox.Show("Favor gravar o movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (CadNota.Status == 1)
                {
                    MessageBox.Show("Nota Fiscal já foi emitida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                }
                else if (CadNota.Status == 2)
                {
                    MessageBox.Show("Nota Fiscal já cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                                 
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (CadNota.IdNota > 0 && !StaFormEdicao)
            {                
                decimal Qtde   = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                decimal VlrUnt = decimal.Parse(GridItens.CurrentRow.Cells[4].Value.ToString());
                decimal PIcms  = decimal.Parse(GridItens.CurrentRow.Cells[6].Value.ToString());
                decimal PIpi   = decimal.Parse(GridItens.CurrentRow.Cells[7].Value.ToString());
                decimal PRed   = decimal.Parse(GridItens.CurrentRow.Cells[8].Value.ToString());

                


                ItemCadNota.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                bool AtlzCFOP = false;
                if (ItemCadNota.IdCfop != int.Parse(GridItens.CurrentRow.Cells[10].Value.ToString()))
                {
                    if (MessageBox.Show("Atualizar o CFOP de todos os Itens ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        AtlzCFOP = true;
                }


                ItemCadNota.IdItem        = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());                
                ItemCadNota.Qtde          = Qtde;
                ItemCadNota.VlrUnitario   = VlrUnt;
                ItemCadNota.VlrTotal      = VlrUnt * Qtde;
                ItemCadNota.PIcms         = PIcms;
                ItemCadNota.PIpi          = PIpi;
                ItemCadNota.PercRed       = PRed;
                ItemCadNota.SitTributaria = int.Parse(GridItens.CurrentRow.Cells[9].Value.ToString());
                ItemCadNota.IdCfop        = int.Parse(GridItens.CurrentRow.Cells[10].Value.ToString());
                ItemCadNota.Cst           = int.Parse(GridItens.CurrentRow.Cells[11].Value.ToString());
                ItemCadNota.ItemPed       = int.Parse(GridItens.CurrentRow.Cells[12].Value.ToString());
                ItemCadNota.CodPrdCliente = GridItens.CurrentRow.Cells[13].Value.ToString();

                if (PRed == 0)
                    ItemCadNota.IdReducao = 0;
                               
                //ValidarCST(CadNota, ItemCadNota);
                ItemCadNota.GravarDados();
                CfopItem = ItemCadNota.IdCfop;
                //Registro de Auditoria
                
                if (AtlzCFOP)
                    Controle.ExecutaSQL("update notafiscalitens set id_Cfop=" + CfopItem.ToString() + ",CST=" + ItemCadNota.Cst.ToString()+",SitTributaria="+ ItemCadNota.SitTributaria.ToString()+ " where id_nota="+CadNota.IdNota.ToString());

                FrmPrincipal.RegistrarAuditoria(this.Text, ItemCadNota.IdItem, CadNota.NumNota.ToString(), 2, "Alteração Item Nota Fiscal");
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
                if (CadNota.IdNota > 0)
                    IncluirItem();
                else
                    Source_Itens.CancelEdit();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (CadNota.Status == 1)
                MessageBox.Show("Nota Fiscal já foi emitida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (CadNota.Status == 2)
                MessageBox.Show("Nota Fiscal já cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (GridItens.CurrentRow != null)
                {
                    if (StaFormEdicao)
                        MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        if (CadNota.IdNota > 0 && !StaFormEdicao)
                        {
                            if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ItemCadNota.IdNota = CadNota.IdNota;
                                ItemCadNota.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                                ItemCadNota.Excluir();
                                //Registro de Auditoria
                                FrmPrincipal.RegistrarAuditoria(this.Text, ItemCadNota.IdItem, CadNota.NumNota.ToString(), 3, "Excluir Item Nota Fiscal");
                                ItemCadNota.IdItem = 0;
                                PopularGridItens();
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
                if (CadNota.Status == 1)
                {
                    MessageBox.Show("Nota Fiscal já foi emitida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                }
                else if (CadNota.Status == 2)
                {
                    MessageBox.Show("Nota Fiscal já cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                }                
                else
                {
                    if (CadNota.IdNota > 0)
                    {
                        Filiais CadFilial = new Filiais();
                        CadFilial.Controle=Controle;
                        CadFilial.LerDados(CadNota.IdFilial);

                        Parametros ParamFilial = new Parametros();
                        ParamFilial.Controle = Controle;
                        ParamFilial.LerDados(CadNota.IdFilial);

                        if (CadNota.NFE == 0)
                        {
                            if (GridItens.Rows.Count > ParamFilial.LinhasNota)
                            {
                                MessageBox.Show("Nota Fiscal ja complemento o limite itens", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Source_Itens.CancelEdit();
                                return;
                            }
                        }

                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.ShowDialog();
                        for (int I = 0; I <= BuscaPrd.ListaCodPrd.Count - 1; I++)
                        {
                            ArrayList PrdQtde = new ArrayList(BuscaPrd.ListaCodPrd[I].ToString().Split(char.Parse("|")));
                            BuscaPrd.CadProd.LerDados(int.Parse(PrdQtde[0].ToString()));
                            BuscaPrd.IdProduto = BuscaPrd.CadProd.IdProduto;

                            if (BuscaPrd.IdProduto > 0)
                            {
                                Verificar ExistePrd = new Verificar();
                                ExistePrd.Controle = Controle;
                                if (ExistePrd.VerificarExite_LancProduto("SELECT * FROM NotaFiscalItens WHERE Id_Nota=" + CadNota.IdNota.ToString() + " and Id_Produto=" + BuscaPrd.IdProduto.ToString()))
                                    MessageBox.Show("Produto: " + BuscaPrd.CadProd.Descricao.Trim() + " já cadastrado no Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ItemCadNota.LerDados(0);
                                ItemCadNota.IdNota    = CadNota.IdNota;
                                ItemCadNota.IdProduto = BuscaPrd.IdProduto;

                                if (CadPessoa.Clie_Forn == 3)
                                    ItemCadNota.VlrUnitario = BuscaPrd.CadProd.PrcAtacado;
                                else
                                    ItemCadNota.VlrUnitario = BuscaPrd.CadProd.PrcVarejo;

                                if (BuscaPrd.CadProd.IdGrupo == 53)
                                {
                                    if (CadPessoa.PDescNFGrpTalimpo > 0)
                                        ItemCadNota.VlrUnitario = BuscaPrd.CadProd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpTalimpo / 100));
                                }
                                else
                                {
                                    if (CadPessoa.PDescNFGrpOutros > 0)
                                        ItemCadNota.VlrUnitario = BuscaPrd.CadProd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpOutros / 100));
                                }

                                if (CadNota.NatOp==1)
                                    ItemCadNota.VlrUnitario = BuscaPrd.CadProd.UltPrcCompra;

                                if (decimal.Parse(PrdQtde[1].ToString()) > 0)
                                    ItemCadNota.Qtde = decimal.Parse(PrdQtde[1].ToString());
                                else
                                    ItemCadNota.Qtde = 1;
                                                                
                                ItemCadNota.IdItem = 0;

                                if (CadNota.IdUf != CadFilial.Uf)
                                {                                    
                                    ItemCadNota.PIcms = 12;
                                }
                                else
                                {
                                    if (CadNota.IdFilial == 2)
                                        ItemCadNota.PIcms = BuscaPrd.CadProd.IcmsIss2;
                                    else
                                        ItemCadNota.PIcms = BuscaPrd.CadProd.IcmsIss;
                                }

                                ItemCadNota.IdCfop = CadNota.IdCfop;
                                if (ParamFilial.NotaIPI == 1)
                                    ItemCadNota.PIpi = BuscaPrd.CadProd.Ipi;
                                else
                                    ItemCadNota.PIpi = 0;
                                
                                ItemCadNota.VlrTotal      = ItemCadNota.VlrUnitario;

                                if (CadNota.IdFilial == 2)
                                    ItemCadNota.SitTributaria = BuscaPrd.CadProd.SitTrib2;
                                else
                                    ItemCadNota.SitTributaria = BuscaPrd.CadProd.SitTributaria;

                                if (ItemCadNota.SitTributaria == 3)
                                    ItemCadNota.PIcms = 0;

                                if (CfopItem == 45)
                                    ItemCadNota.IdCfop = CfopItem;
                                else
                                {
                                    if (CadNota.IdUf != CadFilial.Uf)
                                    {
                                        if (ItemCadNota.SitTributaria == 3  && (CadNota.IdFilial == 1 || CadNota.IdFilial == 6 || CadNota.IdFilial == 7))
                                        {
                                            ItemCadNota.PIcms = 12;
                                            ItemCadNota.IdCfop = 2;
                                            ItemCadNota.SitTributaria = 0;
                                        }
                                        else
                                        {
                                            if (ItemCadNota.SitTributaria == 3)
                                                ItemCadNota.IdCfop = 50;
                                            else
                                                ItemCadNota.IdCfop = 2;
                                        }
                                    }
                                    else
                                    {
                                        if (ItemCadNota.SitTributaria == 3)
                                            ItemCadNota.IdCfop = 40;
                                        else
                                            ItemCadNota.IdCfop = CfopItem;
                                    }
                                }
                                //Redução Fiscal
                                if (ItemCadNota.SitTributaria == 0 && CadNota.IdUf == CadFilial.Uf)
                                {
                                    if (BuscaPrd.CadProd.IdReducao > 0 )
                                    {
                                        RedFiscal.LerDados(BuscaPrd.CadProd.IdReducao);
                                        ItemCadNota.IdReducao = BuscaPrd.CadProd.IdReducao;
                                        ItemCadNota.PercRed = RedFiscal.Perc;
                                    }
                                    else
                                        ItemCadNota.PercRed = BuscaPrd.CadProd.Reducao;
                                }
                                else
                                    ItemCadNota.PercRed = 0;

                                if (ItemCadNota.SitTributaria != 0)
                                    ItemCadNota.PIcms = 0;

                                ValidarCST(CadNota, ItemCadNota);

                                //Alteração do CFOP Clena
                                if (!FrmPrincipal.VersaoDistribuidor && CadNota.IdFilial == 2 && BuscaPrd.CadProd.IdGrupo==53)
                                {
                                    if (CadNota.IdUf == CadFilial.Uf)
                                        ItemCadNota.IdCfop = 66;
                                    else
                                        ItemCadNota.IdCfop = 67;
                                }

                                ItemCadNota.GravarDados();                                
                                //Registro de Auditoria
                                FrmPrincipal.RegistrarAuditoria(this.Text, ItemCadNota.IdItem, CadNota.NumNota.ToString(), 1, "Cadastro Item Nota Fiscal");
                                //PopularGridItens();
                                //GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];
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
        private void Hab_Botoes()
        {
            //LstTipMov.Enabled = StaFormEdicao;
            TxtVlrDesconto.Enabled = StaFormEdicao;
            BtnEnviaXML.Visible    = CadNota.Status == 1 && CadNota.NFE == 1;
            BtnRevalidNfe.Visible  = CadNota.NFE == 1 && CadNota.ChaveNfe != "";
            BtnImpTransf.Visible   = !FrmPrincipal.VersaoDistribuidor && CadNota.Status == 0; ;
            Btn_Carta.Visible      = CadNota.Status == 1;
            LstMeioPag.Enabled     = FrmPrincipal.VersaoDistribuidor;
            BtnImpMDFe.Visible     = CadNota.Status == 1 && CadNota.NFE == 1 && !StaFormEdicao; 
            BtnEncMDFe.Visible     = CadNota.Status == 1 && CadNota.NFE == 1 && !StaFormEdicao; 
            BtnImpTransfMv.Visible = CadNota.Status == 0 && CadNota.NFE == 1 && !StaFormEdicao && CadNota.NatOp==1;
            if (StaFormEdicao || CadNota.IdNota == 0)
            {                
                BtnCancMov.Enabled  = false;
                BtnImprimir.Enabled = false;
            }
            else
            {
                BtnCancMov.Enabled = true;// CadNota.Status == 1;
                BtnImprimir.Enabled = true;// CadNota.Status == 0;
            }
        }        
        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            // if (!StaFormEdicao)
            {
                if (FrmPrincipal.Perfil_Usuario.CancelarNF == 0)
                {
                    FrmAutorizacao Autorizacao = new FrmAutorizacao();
                    Autorizacao.FrmPrincipal = FrmPrincipal;
                    Autorizacao.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (Autorizacao.AcessoOk)
                    {
                        if (Autorizacao.Usuario.CancelarNF == 0)
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


                if (MessageBox.Show("Confirma o Cancelamento da Nota Fiscal ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CadNota.ProtocoloNfe != "")
                    {
                        if (CadNota.Observacao.Trim().Length > 15)
                        {
                            Controles.NF_e Nfe = new Controles.NF_e();
                            Nfe.Controle = Controle;
                            Nfe.Inicializar_parametros(CadNota.IdFilial);
                            string Protocolo = Nfe.CancelarNFe(CadNota.ChaveNfe.Trim(), CadNota.ProtocoloNfe.Trim(), CadNota.Observacao.Trim());

                            if (Protocolo != "" && int.Parse(Nfe.cStat) <= 136)
                            {
                                CadNota.Cancelar();
                                Controle.ExecutaSQL("Update NotaFiscal set ProtocoloCanc='" + Protocolo + "' Where Id_Nota=" + CadNota.IdNota.ToString());
                                FrmPrincipal.RegistrarAuditoria(this.Text, CadNota.IdNota, CadNota.NumNota.ToString(), 6, "Cancelamento da Nota Fiscal");
                                MessageBox.Show("Nota Fiscal foi cancelada", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("Nota Fiscal não foi cancelada, Motivo:" + Nfe.vMotivoRet, "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Informa no Campo Observção a justificativa do cancelamento", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Nota Fiscal não transmitida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    /*else
                    {
                        CadNota.Cancelar();
                        MessageBox.Show("Nota Fiscal foi cancelada", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Paginas.SelectTab(0);
                    }*/
                }
            }
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Favor Verificar autorização do certificado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //return;

            if (CadNota.VlrNota <= 0)
            {
                MessageBox.Show("Favor Verificar o Valor Total", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CadNota.Status == 2)
            {
                MessageBox.Show("Nota Fiscal Cancelada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            Filiais CadFilial = new Filiais();
            CadFilial.Controle = Controle;
            CadFilial.LerDados(CadNota.IdFilial);

            BtnImprimir.Enabled = false;
            if (CadNota.IdPessoa == 0)
            {
                MessageBox.Show("Favor informar o destinatário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
            if (!ValidarCFOP(CadNota.IdNota,CadFilial.Uf))
                return;

            string sSQL = "SELECT T2.ENTSAIDA,T2.RAZAOSOCIAL,T2.CNPJCPF,T2.INSC_UF,T2.DTEMISSAO,RTRIM(T2.ENDERECO)+','+RTRIM(T2.NUMERO)+' '+RTRIM(T2.COMPLEMENTO) AS ENDERECO," +
                          " T2.TELEFONE,T2.CEP,T2.BAIRRO,T2.CIDADE,T4.SIGLA,T3.REFERENCIA,T3.DESCRICAO,T3.UNIDADE,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T1.PICMS,T1.PIPI,T1.VlrIpi as VlrIpiItem," +
                          " T2.BICMS,T2.VLRICMS,T2.BICMSSUB,T2.VLRICMSSUB,T2.VLRFRETE,T2.VLRSEGURO,T2.VLROUTRADESP,T2.VLRFRETE,T2.VLRIPI,T2.VLRPRODUTOS,T2.VLRNOTA,T2.VLRDESCONTO,T2.FRETE," +
                          " T2.QTDEVOLUME,T2.ESPECIE,T2.MARCA,T2.PESOBRUTO,T2.PESOLIQUIDO,T2.NUMFORMULARIO,T2.NUMNOTA,T2.OBSERVACAO,T8.CFOP,CASE T2.ID_VENDA WHEN 0 THEN ' ' ELSE T7.VENDEDOR END AS VENDEDOR," +
                          " T2.ChaveNFe,T2.ProtocoloNFe,T2.ReciboNFe,T1.VLRICMS AS VLRICMSITEM,T3.REDUCAO,T1.SITTRIBUTARIA,T3.NCM,T1.PERCRED, T9.RAZAOSOCIAL AS TRANSPORTADORA, T9.ENDERECO AS ENDTRANSP, T9.NUMERO AS NUMTRANSP,T9.COMPLEMENTO AS COMPTRANSP," +
                          " T9.INSC_UF AS CGFTRANSP, T9.CIDADE AS CIDTRANSP,ISNULL(T1.ID_REDUCAO,0) AS ID_REDUCAO,T0.CODRED,T0.REFREDUCAO," +
                          " CASE ISNULL(T2.NATOP,0) WHEN 0 THEN 'VENDA' WHEN 1 THEN 'TRANSFERÊNCIA' WHEN 2 THEN 'BONIFICAÇÃO' WHEN 3 THEN 'DEVOLUÇÃO' WHEN 4 THEN 'COMPRA' WHEN 5 THEN 'REMESSA' WHEN 6 THEN 'DEMOSTRAÇÃO' WHEN 7 THEN 'RETORNO' WHEN 8 THEN 'EXPOSIÇÃO'"+
                          " WHEN 9 THEN 'OUTRAS' WHEN 10 THEN 'VENDA A ORDEM' WHEN 11 THEN 'REMESSA MERC. POR CONTA E ORDERM DE TERC.' WHEN 12 THEN 'COMPLEMENTAR' END AS NATUREZA, IsNull(T1.CST,0) as CST," +
                          " T1.ID_PROMOCAO,T9.CNPJ AS CNPJTRANS, E1.SIGLA AS UFTRANSF,T2.VENCFATURA FROM NOTAFISCALITENS T1" +
                          " LEFT JOIN NOTAFISCAL T2 ON (T2.ID_NOTA=T1.ID_NOTA)" +
                          " LEFT JOIN PRODUTOS   T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                          " LEFT JOIN ESTADOS    T4 ON (T4.ID_UF=T2.ID_UF)" +
                          " LEFT JOIN CFOP       T5 ON (T5.ID_CFOP=T2.ID_CFOP)" +
                          " LEFT JOIN CFOP       T8 ON (T8.ID_CFOP=T1.ID_CFOP)" +
                          " LEFT OUTER JOIN MVVENDA AS T6 ON T6.ID_VENDA=T2.ID_VENDA" +
                          " LEFT OUTER JOIN VENDEDORES AS T7 ON T7.ID_VENDEDOR=T6.ID_VENDEDOR  " +
                          " LEFT JOIN TRANSPORTADORAS AS T9 ON (T9.ID_TRANSPORTADORA=T2.ID_TRANSPORTADORA)" +
                          " LEFT JOIN REDUCAOFISCAL AS T0 ON (T0.ID_REDUCAO=T1.ID_REDUCAO)" +
                          " LEFT JOIN ESTADOS    E1 ON (E1.ID_UF=T9.ID_UF)" +
                          " WHERE T1.ID_NOTA=" + CadNota.IdNota.ToString() + " ORDER BY T1.ID_ITEM";


            Parametros ParamFilial = new Parametros();
            ParamFilial.Controle = Controle;
            ParamFilial.LerDados(CadNota.IdFilial);
            FrmRelatorios FrmRel = new FrmRelatorios();
            
            if (CadNota.NFE == 1)
            {

                string TxtReducao = "";
                        //
                SqlDataReader TabRed = Controle.ConsultaSQL("SELECT DISTINCT T2.CODRED +' - '+T2.RefReducao as TxtReducao FROM NotaFiscalItens T1"+
                                                            " LEFT JOIN ReducaoFiscal T2 ON (T2.Id_Reducao=T1.ID_REDUCAO)"+
                                                            " WHERE T1.ID_NOTA="+CadNota.IdNota.ToString()+" AND T1.ID_REDUCAO > 0");
                while (TabRed.Read())
                    TxtReducao = TxtReducao + "( "+TabRed["TxtReducao"].ToString().Trim()+" ) "+Environment.NewLine;

                if (!FrmPrincipal.VersaoDistribuidor)
                {
                    if (CadNota.IdFilial == 3 || CadNota.IdFilial == 4 || CadNota.IdFilial == 5)
                    {
                        SqlDataReader TabPrd = Controle.ConsultaSQL("SELECT * FROM NotaFiscalItens T1" +
                                                                    " LEFT JOIN PRODUTOS T2 ON (T2.Id_Produto=T1.ID_Produto)" +
                                                                    " WHERE T1.ID_NOTA=" + CadNota.IdNota.ToString() + " AND T2.ID_GRUPO=288");
                        while (TabPrd.Read())
                            TxtReducao = "Empresa Optante do Simples Nacional com crédito de 3,10% no CFOP 5.102 dos produtos";
                    }
                }
                TabRed = Controle.ConsultaSQL("SELECT Top 1 T1.Id_Promocao FROM NotaFiscalItens T1" +
                                              " WHERE T1.ID_NOTA=" + CadNota.IdNota.ToString() + " AND T1.ID_PROMOCAO > 0");


                while (TabRed.Read())
                    TxtReducao = TxtReducao + "(P** Preço Promocional) " + Environment.NewLine;
                        

                if (CadNota.ProtocoloNfe.Trim() == "")
                {
                    Controles.NF_e Nfe = new Controles.NF_e();
                    Nfe.Controle = Controle;
                    Nfe.Inicializar_parametros(CadNota.IdFilial);
                    
                    string XmlNota = Nfe.GerarXmlNF(CadNota.IdNota, CadFilial, 1);
                    Nfe.EnviarNFE(CadNota.NumNota,XmlNota);
                    Controle.ExecutaSQL("Update NotaFiscal set ChaveNFe='" + Nfe.nChaveNF + "' Where Id_Nota=" + CadNota.IdNota.ToString());

                    if (Nfe.nProtocoloNF != "" && Nfe.nReciboNF != "" && int.Parse(Nfe.cStat) !=0 && (int.Parse(Nfe.cStat) <= 104 || int.Parse(Nfe.cStat) == 302))
                    {
                        Controle.ExecutaSQL("Update NotaFiscal set ReciboNFe='" + Nfe.nReciboNF + "',ProtocoloNFe='" + Nfe.nProtocoloNF + "' Where Id_Nota=" + CadNota.IdNota.ToString());
                        Nfe.GravarXmlRetornoNFE(CadNota.NumNota, XmlNota, Nfe.vXMLRetorno);

                        if (int.Parse(Nfe.cStat) == 302)
                        {
                            CadNota.Concluir();
                            MessageBox.Show("Nota Fiscal Eletrônica não permitida, Motivo: " + Nfe.vMotivoRet, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            BtnImprimir.Enabled = true;
                            return;
                        }
                        MessageBox.Show("Nota Fiscal Eletrônica transmitida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Imprimindo a Nota
                        Relatorios.RelNotaEletronica RelNF = new Relatorios.RelNotaEletronica();
                        DataSet TabRel = new DataSet();
                        TabRel = Controle.ConsultaTabela(sSQL);
                        RelNF.SetDataSource(TabRel.Tables[0]);

                        CrystalDecisions.Shared.ParameterValues P_Regime = new CrystalDecisions.Shared.ParameterValues();
                        P_Regime.AddValue(CadFilial.Regime);
                        RelNF.ParameterFields[0].CurrentValues = P_Regime;

                        FrmRel.cryRepRelatorio.ReportSource = RelNF;
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblEndEmitente"])).Text = CadFilial.Endereco.Trim() + "," + CadFilial.Numero.Trim() + " - " + CadFilial.Bairro.Trim() + "  CEP:" + CadFilial.Cep.Trim() + " - " + CadFilial.Cidade.Trim() + " Fone:" + CadFilial.Fone1.Trim();
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblEmitente"])).Text = CadFilial.Filial.Trim();
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblInscUF"])).Text = CadFilial.InscUF.Trim();
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblCnpj"])).Text = CadFilial.Cnpj.Substring(0, 2) + "." + CadFilial.Cnpj.Substring(2, 3) + "." + CadFilial.Cnpj.Substring(5, 3) + "/" + CadFilial.Cnpj.Substring(8, 4) + "-" + CadFilial.Cnpj.Substring(12, 2);
                        ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["TxtReducao"])).Text = TxtReducao;
                        
                        FrmRel.ShowDialog();
                        CadNota.Concluir();
                        SalvaNFePDF(RelNF, ParamFilial.IdFilial);
                        //Enviando o XML
                        if (CadPessoa.EmailNFE.Trim() != "")
                            EnviarNFE(CadNota.NumNota, CadPessoa.EmailNFE.Trim());
                    }
                    else
                    {
                        MessageBox.Show("Nota Fiscal Eletrônica não foi transmitida, Motivo: " + Nfe.vMotivoRet, "Falha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnImprimir.Enabled = true;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Nota Fiscal já transmitida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);                    

                    //Imprimindo a Nota
                    Relatorios.RelNotaEletronica RelNF = new Relatorios.RelNotaEletronica();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSQL);
                    RelNF.SetDataSource(TabRel.Tables[0]);

                    CrystalDecisions.Shared.ParameterValues P_Regime = new CrystalDecisions.Shared.ParameterValues();
                    P_Regime.AddValue(CadFilial.Regime);
                    RelNF.ParameterFields[0].CurrentValues = P_Regime;

                    FrmRel.cryRepRelatorio.ReportSource = RelNF;
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblEndEmitente"])).Text = CadFilial.Endereco.Trim() + "," + CadFilial.Numero.Trim() + " - " + CadFilial.Bairro.Trim() + "  CEP:" + CadFilial.Cep.Trim() + " - " + CadFilial.Cidade.Trim() + " Fone:" + CadFilial.Fone1.Trim();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblEmitente"])).Text = CadFilial.Filial.Trim();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblInscUF"])).Text = CadFilial.InscUF.Trim();
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblCnpj"])).Text = CadFilial.Cnpj.Substring(0, 2) + "." + CadFilial.Cnpj.Substring(2, 3) + "." + CadFilial.Cnpj.Substring(5, 3) + "/" + CadFilial.Cnpj.Substring(8, 4) + "-" + CadFilial.Cnpj.Substring(12, 2);
                    ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["TxtReducao"])).Text = TxtReducao;
                    FrmRel.ShowDialog();
                    CadNota.Concluir();
                    SalvaNFePDF(RelNF, ParamFilial.IdFilial);
                }
            }
            else
            {
                if (FrmPrincipal.Parametros_Filial.NotaIPI == 1)
                {
                    Relatorios.RelNotaFiscal2 RelNF = new Relatorios.RelNotaFiscal2();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSQL);
                    RelNF.SetDataSource(TabRel.Tables[0]);
                    FrmRel.cryRepRelatorio.ReportSource = RelNF;
                    FrmRel.ShowDialog();
                    CadNota.Concluir();
                }
                else
                {
                    //if (ParamFilial.IdFilial == 3)
                    //{
                    Relatorios.RelNotaFiscalTalimpinho RelNF = new Relatorios.RelNotaFiscalTalimpinho();
                    DataSet TabRel = new DataSet();
                    TabRel = Controle.ConsultaTabela(sSQL);
                    RelNF.SetDataSource(TabRel.Tables[0]);
                    CrystalDecisions.Shared.ParameterValues P_Regime = new CrystalDecisions.Shared.ParameterValues();
                    P_Regime.AddValue(CadFilial.Regime);
                    RelNF.ParameterFields[0].CurrentValues = P_Regime;
                    FrmRel.cryRepRelatorio.ReportSource = RelNF;
                    FrmRel.ShowDialog();
                    CadNota.Concluir();
                    /*}
                    else
                    {
                        Relatorios.RelNotaFiscal RelNF = new Relatorios.RelNotaFiscal();
                        DataSet TabRel = new DataSet();
                        TabRel = Controle.ConsultaTabela(sSQL);
                        RelNF.SetDataSource(TabRel.Tables[0]);
                        CrystalDecisions.Shared.ParameterValues P_Regime = new CrystalDecisions.Shared.ParameterValues();
                        P_Regime.AddValue(CadFilial.Regime);
                        RelNF.ParameterFields[0].CurrentValues = P_Regime;
                        FrmRel.cryRepRelatorio.ReportSource = RelNF;
                        FrmRel.ShowDialog();
                        CadNota.Concluir();
                    }*/
                }
            }
            BtnImprimir.Enabled = true;
        }

                
        private void SalvaNFePDF(Relatorios.RelNotaEletronica RelNfe,int IdFilial)
        {
            DirectoryInfo VerPath = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfePDF\\" + string.Format("{0:D3}", IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())));
            if (!VerPath.Exists)
                VerPath.Create();

            string vArqPDF = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfePDF\\" + string.Format("{0:D3}", IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}",int.Parse(DateTime.Now.Date.Month.ToString())) + "\\Nfe-" + string.Format("{0:D8}", CadNota.NumNota) + ".PDF";            
            RelNfe.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, vArqPDF);
        }
        private bool ValidarCFOP(int IdNota, int UF)
        {
            SqlDataReader Tab;
            if (UF == CadNota.IdUf)
            {   
                //Verificando os Itens
                Tab = Controle.ConsultaSQL("select * from NotaFiscalItens t1 " +
                                           " left join CFOP t3 on (t3.Id_CFOP=t1.id_cfop) "+ 
                                           " where t1.Id_Nota = " + IdNota.ToString() +
                                           " AND  SUBSTRING(T3.CFOP,1,1) NOT IN ('1','5')");
                if (Tab.HasRows)
                {
                    MessageBox.Show("Atenção: Favor verificar o CFOP,  informar o CFOP para dentro do Estado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }
            else
            {                
                Tab = Controle.ConsultaSQL("select t1.Id_Item from NotaFiscalItens t1" +
                                           " left join CFOP t3 on (t3.Id_CFOP=t1.id_cfop) "+                                           
                                           " where t1.Id_Nota=" + IdNota.ToString() +
                                           " AND  SUBSTRING(T3.CFOP,1,1) NOT IN ('2','6')");
                if (Tab.HasRows)
                {
                    MessageBox.Show("Atenção: Favor verificar o CFOP,  informar o CFOP para fora do Estado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }                        
            return true;
        }
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                if (CadNota.Status == 1)
                    MessageBox.Show("Nota Fiscal já foi emitida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (CadNota.Status == 2)
                    MessageBox.Show("Nota Fiscal já cancelada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                    BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
                    BuscaPessoa.ShowDialog();
                    if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                    {
                        SetaPessoa(BuscaPessoa.CadPessoa.IdPessoa);                        
                        PopularEndereco(BuscaPessoa.CadPessoa.IdPessoa);
                    }
                    else
                    {
                        CadNota.IdPessoa = 0;
                        PopularEndereco(0);
                    }
                }
            }
        }
        private void SetaPessoa(int IdPessoa)
        {
            CadPessoa.LerDados(IdPessoa);
            CadNota.IdPessoa   = CadPessoa.IdPessoa;
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
                TxtNumero.Text        = CadPessoa.Numero;
                TxtComplemento.Text   = CadPessoa.Complemento;
                TxtCidade.Text        = CadPessoa.Cidade;
                TxtBairro.Text        = CadPessoa.Bairro;
                LstUF.SelectedValue   = CadPessoa.IdUF.ToString();
                LstPais.SelectedValue = CadPessoa.Pais;
                TxtFone.Text          = CadPessoa.Fone;
                TxtCodMun.Value       = CadPessoa.CodMun;
            }
            else
            {
                CadNota.IdPessoa      = 0;
                TxtCodCliente.Text    = "";
                TxtCliente.Text       = "";
                TxtCnpj.Text          = "";
                TxtNmPessoa.Text      = "";
                TxtInscUF.Text        = "";
                TxtCep.Text           = "";
                TxtEndereco.Text      = "";
                TxtNumero.Text        = "";
                TxtComplemento.Text   = "";
                TxtCidade.Text        = "";
                TxtBairro.Text        = "";
                LstUF.SelectedValue   = 0;
                TxtCodMun.Value       = 0;
                LstPais.SelectedValue = "1058";
                TxtFone.Text          = "";
            }
        }        
        private void TxtVlrDesconto_Validated(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                TxtVlrNota.Value = TxtVlrProduto.Value - TxtVlrDesconto.Value;
        }        
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /* if (e.ColumnIndex == 1)
             {
                 if (e.Value.ToString().Trim() == "Confirmado")
                     GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Silver;
             }*/
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
            }
        }
        private void SetaMaskCnpj(int Tipo) 
        {
            if (Tipo == 1)
            {
                TxtCnpj.Mask = "00,000,000/0000-00";
                label27.Text = "CNPJ:";
                label26.Text = "Razão Social:";
                label25.Text = "Insc.Estadual:";
            }
            else
            {
                TxtCnpj.Mask = "000,000,000-00";
                label27.Text = "CPF:";
                label26.Text = "Nome:";
                label25.Text = "RG:";
            }
        }
        private void BtnPesqNT_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }
        private void BtnGerarNF_Click(object sender, EventArgs e)
        {
            GeraNF(false);

        }
        private void GeraNF(bool NFE)
        {
            if (GridVenda.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Confirma Nota Fiscal ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BtnGerarNF.Enabled = false;
                    int IdVenda        = int.Parse(GridVenda.CurrentRow.Cells[0].Value.ToString());
                    string NumForms    = "";
                    DataSet ConsItens  = new DataSet();
                    ConsItens = Controle.ConsultaTabela("SELECT * FROM MVVENDAITENS T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) WHERE T1.ID_VENDA=" + IdVenda.ToString()+" ORDER BY T1.ID_ITEM");
                    if (ConsItens.Tables[0].Rows.Count > 0)
                    {
                        FrmPrincipal.BSta_BarProcesso.Maximum = ConsItens.Tables[0].Rows.Count;
                        Produtos CadProd      = new Produtos();
                        NotaFiscalItens Itens = new NotaFiscalItens();
                        MvVenda Vendas        = new MvVenda();
                        Vendas.Controle       = Controle;
                        Itens.Controle        = Controle;
                        CadProd.Controle      = Controle;
                        Vendas.LerDados(IdVenda);

                        Filiais CadFilial  = new Filiais();
                        CadFilial.Controle = Controle;
                        
                        /*if (Vendas.Status != 2)
                        {
                            MessageBox.Show("Atenção: Venda não faturada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }*/

                        int NLin = 1;
                        int SeqNota = 0;
                        for (int I = 0; I <= ConsItens.Tables[0].Rows.Count - 1; I++)
                        {
                            if (NLin == 1)
                            {
                                string CnpjCpf = ConsItens.Tables[0].Rows[I]["CnpjCpf"].ToString().Trim();
                                if (CnpjCpf == "00000000000000" || CnpjCpf == "11111111111111" || CnpjCpf == "22222222222222" || CnpjCpf == "33333333333333" || CnpjCpf == "44444444444444"
                                 || CnpjCpf == "55555555555555" || CnpjCpf == "66666666666666" || CnpjCpf == "77777777777777" || CnpjCpf == "88888888888888" || CnpjCpf == "99999999999999")
                                {
                                    MessageBox.Show("Favor verificar o CNPJ: " + CnpjCpf + " inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (CnpjCpf == "00000000000" || CnpjCpf == "11111111111" || CnpjCpf == "22222222222" || CnpjCpf == "33333333333" || CnpjCpf == "44444444444"
                                 || CnpjCpf == "55555555555" || CnpjCpf == "66666666666" || CnpjCpf == "77777777777" || CnpjCpf == "88888888888" || CnpjCpf == "99999999999")
                                {
                                    MessageBox.Show("Favor verificar o CPF: " + CnpjCpf + " inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    BtnGerarNF.Enabled = true;
                                    return;
                                }
                                SeqNota++;
                                CadFilial.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Filial"].ToString())); 
                                CadPessoa.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Pessoa"].ToString()));
                                CadNota.LerDados(0);
                                CadNota.DtEmissao  = FrmPrincipal.DtHrServidor();
                                CadNota.IdFilial   = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Filial"].ToString());
                                CadNota.CnpjCpf    = ConsItens.Tables[0].Rows[I]["CnpjCpf"].ToString();
                                CadNota.IdPessoa   = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Pessoa"].ToString());
                                CadNota.NmPessoa   = ConsItens.Tables[0].Rows[I]["Pessoa"].ToString();
                                CadNota.InscUf     = ConsItens.Tables[0].Rows[I]["InscUF"].ToString();
                                CadNota.Cep        = ConsItens.Tables[0].Rows[I]["Cep"].ToString().Replace("-", "");
                                CadNota.Endereco   = ConsItens.Tables[0].Rows[I]["Endereco"].ToString();
                                CadNota.Numero     = ConsItens.Tables[0].Rows[I]["Numero"].ToString();
                                CadNota.Compl      = ConsItens.Tables[0].Rows[I]["Complemento"].ToString();
                                CadNota.Cidade     = ConsItens.Tables[0].Rows[I]["Cidade"].ToString();
                                CadNota.Bairro     = ConsItens.Tables[0].Rows[I]["Bairro"].ToString();
                                CadNota.Telefone   = ConsItens.Tables[0].Rows[I]["Fone"].ToString();
                                CadNota.IdUf       = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Uf"].ToString());
                                CadNota.Pais       = ConsItens.Tables[0].Rows[I]["Pais"].ToString();
                                CadNota.Observacao = ConsItens.Tables[0].Rows[I]["Observacao"].ToString();
                                CadNota.NumPedido  = ConsItens.Tables[0].Rows[I]["NumPedido"].ToString();
                                CadNota.Especie    = "VARIADAS";
                                CadNota.Marca      = "VARIADAS";
                                CadNota.IdCfop     = CadPessoa.IdCfop;
                                CadNota.Frete      = CadPessoa.Frete;
                                CadNota.CodMun     = CadPessoa.CodMun;
                                CadNota.EntSaida   = 0;
                                CadNota.IdVenda    = IdVenda;
                                CadNota.SeqImp     = SeqNota;
                                CadNota.Status     = 0;
                                CadNota.Consumidor = 1;
                                if (CadPessoa.Clie_Forn == 3 || CadPessoa.Clie_Forn == 4 || CadPessoa.Clie_Forn == 6 || CadPessoa.Clie_Forn == 7)
                                    CadNota.Consumidor = 0;
                                ParamFilial.LerDados(CadNota.IdFilial);
                                
                                CadUF.LerDados(CadNota.IdUf);
                                CadNota.ICMSInterno = CadUF.ICMSInterno;
                                CadNota.PercDifal   = CadUF.PercDifal;

                                if (SeqNota == 1)
                                    CadNota.VlrDesconto = Vendas.VlrDesconto;

                                if (NFE) CadNota.NFE = 1; else CadNota.NFE = 0;
                                
                                if (NFE)
                                {                                    
                                    if (ParamFilial.WSNumNFE == 1)
                                    {
                                        try
                                        {
                                            Controles.GerarNumNF.GerarNumNF GerarN_NFE = new Controles.GerarNumNF.GerarNumNF();
                                            GerarN_NFE.Url = "http://" + FrmPrincipal.URLMatriz + "/ERP-SGE_WebService/GerarNumNF.asmx?WSDL";
                                            ArrayList NumNF = new ArrayList(GerarN_NFE.ProxNotaFiscal(CadNota.IdFilial, true));
                                            if (int.Parse(NumNF[0].ToString()) == 0)
                                            {
                                                MessageBox.Show("Atenção: Numero de Nota Fiscal não foi gerado, Favor verificar conexão com o servidor.", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            CadNota.NumNota = int.Parse(NumNF[0].ToString());
                                            CadNota.NumFormulario = int.Parse(NumNF[1].ToString());
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Atenção: Numero de Nota Fiscal não foi gerado, Favor verificar conexão com o servidor.", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        FrmPrincipal.Parametros_Filial.ProxNotaFiscal(CadNota.IdFilial, true);
                                        CadNota.NumNota       = FrmPrincipal.Parametros_Filial.NotaNFE;
                                        CadNota.NumFormulario = FrmPrincipal.Parametros_Filial.FormularioNFE;
                                    }                                    
                                }
                                else
                                {
                                    FrmPrincipal.Parametros_Filial.ProxNotaFiscal(CadNota.IdFilial,false);
                                    CadNota.NumNota       = FrmPrincipal.Parametros_Filial.NotaFiscal;
                                    CadNota.NumFormulario = FrmPrincipal.Parametros_Filial.Formulario;
                                }
                                CadNota.IdTransportadora  = CadPessoa.IdTransportadora;
                                CadNota.ReciboNfe         = "";
                                CadNota.ProtocoloNfe      = "";

                                if (ParamFilial.ObsNF.Trim() != "")
                                    CadNota.Observacao = ParamFilial.ObsNF;

                                CadNota.ChaveNfe = "";
                                CadNota.GravarDados();

                                //Registro de Auditoria
                                FrmPrincipal.RegistrarAuditoria(this.Text, ItemCadNota.IdItem, CadNota.NumNota.ToString(), 1, "Gerar Nota Fiscal");
                                if (NumForms == "") 
                                    NumForms = string.Format("{0:D6}", CadNota.NumFormulario); 
                                else 
                                    NumForms = NumForms + "/" + string.Format("{0:D6}", CadNota.NumFormulario);
                            }
                            CadProd.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString()));

                            //Verificação do Kit do Produto
                            if (CadProd.ProdutoKit == 1 && CadPessoa.KitNfe == 0)
                                Itens = IncluirKit(Itens, ConsItens.Tables[0].Rows[I],CadProd, CadFilial);
                            else
                            {
                                Itens.LerDados(0);
                                Itens.IdNota      = CadNota.IdNota;
                                Itens.IdProduto   = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                                Itens.Qtde        = decimal.Parse(ConsItens.Tables[0].Rows[I]["Qtde"].ToString());
                                Itens.VlrUnitario = decimal.Parse(ConsItens.Tables[0].Rows[I]["VlrUnitario"].ToString());
                                Itens.ItemPed     = int.Parse(ConsItens.Tables[0].Rows[I]["ItemPed"].ToString());
                                Itens.IdPromocao  = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Promocao"].ToString());
                                if (TxtPercDescNFe.Value > 0)
                                    Itens.VlrUnitario = Math.Round(Itens.VlrUnitario * (1 - (TxtPercDescNFe.Value / 100)),2);
                                else
                                {
                                    if (CadProd.IdGrupo == 53)
                                    {
                                        if (CadPessoa.PDescNFGrpTalimpo > 0)
                                        {

                                            Itens.VlrUnitario = CadProd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpTalimpo / 100));
                                        }
                                    }
                                    else
                                    {
                                        {
                                            if (CadPessoa.PDescNFGrpOutros > 0)
                                                Itens.VlrUnitario = CadProd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpOutros / 100));
                                        }
                                    }
                                }
                                Itens.VlrTotal = Itens.Qtde * Itens.VlrUnitario;

                                                                
                                if (CadNota.IdUf != CadFilial.Uf)
                                    Itens.PIcms = 12;
                                else
                                {
                                    if (CadNota.IdFilial == 2)
                                        Itens.PIcms = CadProd.IcmsIss2;
                                    else
                                        Itens.PIcms = CadProd.IcmsIss;
                                }

                                if (CadNota.IdFilial == 2)
                                    Itens.SitTributaria = CadProd.SitTrib2;
                                else
                                    Itens.SitTributaria = CadProd.SitTributaria;

                                Itens.IdCfop = CadNota.IdCfop;

                                if (Itens.SitTributaria == 3)
                                    Itens.PIcms = 0;

                                if (ParamFilial.NotaIPI == 1)
                                    Itens.PIpi = CadProd.Ipi;

                                if (CadNota.IdUf != CadFilial.Uf)
                                {
                                    if (Itens.SitTributaria == 3 && (CadNota.IdFilial == 1 || CadNota.IdFilial == 6 || CadNota.IdFilial == 7))
                                    {
                                        Itens.PIcms         = 12;
                                        Itens.IdCfop        = 2;
                                        Itens.SitTributaria = 0;
                                    }
                                    else
                                    {
                                        if (Itens.SitTributaria == 3)
                                            Itens.IdCfop = 50;
                                        else
                                            Itens.IdCfop = 2;
                                    }
                                }
                                else
                                {
                                    if (Itens.SitTributaria == 3)
                                        Itens.IdCfop = 40;
                                    else
                                        Itens.IdCfop = 1;
                                }

                                if (Itens.SitTributaria != 0)
                                    Itens.PIcms = 0;

                                if (Itens.SitTributaria == 0 && CadNota.IdUf == CadFilial.Uf)
                                {
                                    if (CadProd.IdReducao > 0)
                                    {
                                        RedFiscal.LerDados(CadProd.IdReducao);
                                        Itens.IdReducao = CadProd.IdReducao;
                                        Itens.PercRed   = RedFiscal.Perc;
                                    }
                                    else
                                        Itens.PercRed = CadProd.Reducao;
                                }
                                else
                                    Itens.PercRed = 0;

                                ValidarCST(CadNota, Itens);

                                //Alteração do CFOP Clena
                                if (!FrmPrincipal.VersaoDistribuidor && CadNota.IdFilial == 2 && CadProd.IdGrupo == 53)
                                {
                                     if (CadNota.IdUf == CadFilial.Uf)
                                         Itens.IdCfop = 66;
                                     else
                                         Itens.IdCfop = 67;
                                }
                                Itens.GravarDados();                                
                            }
                            if (CadNota.IdUf != CadFilial.Uf)
                                CadNota.DestOperacao = 1;

                            CadNota.GravarDados();
                            FrmPrincipal.BSta_BarProcesso.Maximum = FrmPrincipal.BSta_BarProcesso.Maximum + 1;

                            if (CadNota.NFE == 0)
                            {
                                if (NLin >= FrmPrincipal.Parametros_Filial.LinhasNota)
                                    NLin = 1;
                                else
                                    NLin = NLin + 1;
                            }
                            else
                                NLin = NLin + 1;
                        }

                        if (NumForms != "")
                        {
                            Controle.ExecutaSQL("UPDATE MVVENDA SET FORMNF='" + NumForms + "' WHERE ID_VENDA=" + IdVenda.ToString());
                            Controle.ExecutaSQL("UPDATE LANCFINANCEIRO SET NOTAFISCAL='" + NumForms + "' WHERE ID_VENDA=" + IdVenda.ToString());
                        }

                        if (!FrmPrincipal.VersaoDistribuidor)
                        {
                            if (CadNota.VlrIpi > 0)
                            {
                                CadNota.VlrDesconto = CadNota.VlrDesconto + CadNota.VlrIpi;
                                CadNota.GravarDados();
                            }
                        }
                        MessageBox.Show("Nota Fiscal Concluida, No. dos Formulários Gerados: " + NumForms, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PopularGridVenda();
                        FrmPrincipal.BSta_BarProcesso.Maximum = 0;
                    }
                }
            }
            else
                MessageBox.Show("Selecione uma venda", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BtnGerarNF.Enabled = true;
        }

        private NotaFiscalItens IncluirKit(NotaFiscalItens Itens, DataRow ItemKit, Produtos CadProd, Filiais CadFilial)
        {
            SqlDataReader TabKit = Controle.ConsultaSQL("SELECT * FROM ProdutosKit T1 Left Join Produtos T2 on (T2.Id_Produto=T1.Id_Produto) WHERE T1.Id_PrdMaster=" + ItemKit["ID_Produto"].ToString());
            decimal TotalKit = 0;
            while (TabKit.Read())
                TotalKit = TotalKit + (decimal.Parse(TabKit["PRCESPECIAL"].ToString())*decimal.Parse(TabKit["QTDE"].ToString()));

            decimal PrcDesc = 100 - (100 / (TotalKit / decimal.Parse(ItemKit["VlrUnitario"].ToString())));
            decimal VlrUnt  = 0;
            TabKit = Controle.ConsultaSQL("SELECT * FROM ProdutosKit T1 Left Join Produtos T2 on (T2.Id_Produto=T1.Id_Produto) WHERE T1.Id_PrdMaster=" + ItemKit["ID_Produto"].ToString());

            decimal TItem = 0;
            while (TabKit.Read())
            {
                CadProd.LerDados(int.Parse(TabKit["ID_PRODUTO"].ToString()));
                VlrUnt = decimal.Parse(TabKit["PRCESPECIAL"].ToString()) * (1 - (PrcDesc / 100));
                Itens.LerDados(0);
                Itens.IdNota      = CadNota.IdNota;
                Itens.IdProduto   = int.Parse(TabKit["Id_Produto"].ToString());
                Itens.Qtde        = decimal.Parse(ItemKit["Qtde"].ToString()) * decimal.Parse(TabKit["Qtde"].ToString());                
                Itens.VlrUnitario = VlrUnt;

                if (CadProd.IdGrupo == 53)
                {
                    if (CadPessoa.PDescNFGrpTalimpo > 0)
                        Itens.VlrUnitario = CadProd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpTalimpo / 100));
                }
                else
                {
                    if (CadPessoa.PDescNFGrpOutros > 0)
                        Itens.VlrUnitario = CadProd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpOutros / 100));
                }

                Itens.VlrTotal = Itens.Qtde * Itens.VlrUnitario;
                TItem          = TItem + Math.Round(Itens.VlrTotal,2);

                if (CadNota.IdUf != CadFilial.Uf)
                    Itens.PIcms = 12;
                else
                {
                    if (CadNota.IdFilial == 2)
                        Itens.PIcms = CadProd.IcmsIss2;
                    else
                        Itens.PIcms = CadProd.IcmsIss;
                }
                

                if (CadNota.IdFilial == 2)
                    Itens.SitTributaria = CadProd.SitTrib2;
                else
                    Itens.SitTributaria = CadProd.SitTributaria;

                Itens.IdCfop = CadNota.IdCfop;
                if (Itens.SitTributaria == 3)
                    Itens.PIcms = 0;

                if (ParamFilial.NotaIPI == 1)
                    Itens.PIpi = CadProd.Ipi;

                if (Itens.SitTributaria==0 && CadNota.IdUf == CadFilial.Uf)
                {
                    if (CadProd.IdReducao > 0)
                    {
                        RedFiscal.LerDados(CadProd.IdReducao);
                        Itens.IdReducao = CadProd.IdReducao;
                        Itens.PercRed = RedFiscal.Perc;
                    }
                    else
                        Itens.PercRed = CadProd.Reducao;
                }
                else
                    Itens.PercRed = 0;

                if (CadNota.IdUf != CadFilial.Uf)
                {
                    if (Itens.SitTributaria == 3 && (CadNota.IdFilial == 1 || CadNota.IdFilial == 6 || CadNota.IdFilial == 7))
                    {
                        Itens.PIcms = 12;
                        Itens.IdCfop = 2;
                        Itens.SitTributaria = 0;
                    }
                    else
                    {
                        if (Itens.SitTributaria == 3)
                            Itens.IdCfop = 50;
                        else
                            Itens.IdCfop = 2;
                    }
                }
                else
                {
                    if (CadProd.IdCfopVD > 0)
                    {
                        if (Itens.SitTributaria == 3)
                            Itens.IdCfop = 40;
                        else
                            Itens.IdCfop = CadProd.IdCfopVD;
                    }
                }

                if (Itens.SitTributaria != 0)
                    Itens.PIcms = 0;

                if (Itens.VlrTotal > 0)
                {
                    ValidarCST(CadNota, Itens);
                    Itens.GravarDados();
                }
            }
            
            if (Math.Round(TItem, 2) != decimal.Parse(ItemKit["VlrTotal"].ToString()))
            {
                decimal Dif = decimal.Parse(ItemKit["VlrTotal"].ToString()) - Math.Round(TItem, 2);
                Itens.VlrUnitario = Itens.VlrUnitario + Dif;
                Itens.GravarDados();
            }
            return Itens;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NotaFiscal Nt = new NotaFiscal();
            Nt.Controle = Controle;

            NotaFiscalItens NtItens = new NotaFiscalItens();
            NtItens.Controle = Controle;
            Filiais CadFilial = new Filiais();
            CadFilial.Controle = Controle;
            

            for (int I = 0; I <= GridDados.RowCount - 1; I++)
            {
                int NumNt = int.Parse(GridDados.Rows[I].Cells[0].Value.ToString());
                button1.Text = GridDados.Rows.Count.ToString() + " - " + I.ToString();
                button1.Update();
                Application.DoEvents();
                CadNota.LerDados(NumNt);
                PopularGridItens();                
              
            }
            MessageBox.Show("Fim");
        }
        private void BtnNFE_Click(object sender, EventArgs e)
        {
            GeraNF(true);
        }
        private void EnviarNFE(int NumNota, string Email)
        {
            Parametros ParamFilial = new Parametros();
            ParamFilial.Controle = Controle;
            ParamFilial.LerDados(CadNota.IdFilial);
            if (ParamFilial.Smtp != "")
            {
                EnviarEmail Transmitir = new EnviarEmail();
                Transmitir.Smtp  = ParamFilial.Smtp;
                Transmitir.Porta = ParamFilial.Porta;
                Transmitir.Email = ParamFilial.Email;
                Transmitir.Senha = ParamFilial.Senha;
                Transmitir.Enviar_EmailXmlNFE("XML NOTA ELETRONICA", Email, NumNota, CadNota.IdFilial, CadNota.DtEmissao);
            }
        }        
        private void BtnEnviaXML_Click(object sender, EventArgs e)
        {
            BtnEnviaXML.Enabled = false;
            Application.DoEvents();
            if (CadPessoa.EmailNFE.Trim() != "")
                EnviarNFE(CadNota.NumNota, CadPessoa.EmailNFE);
            else
                MessageBox.Show("Favor Cadastrar o EMAIL pra envio o XML da Nota Eletronica");
            BtnEnviaXML.Enabled = true;
        }

        private void BtnRevalidNfe_Click(object sender, EventArgs e)
        {
            if (CadNota.ChaveNfe != "")
            {
                if (MessageBox.Show("Confirma a Revalidação da Nota Fiscal Numero: "+CadNota.NumNota, "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BtnRevalidNfe.Enabled = false;
                    Application.DoEvents();

                    Filiais CadFilial = new Filiais();
                    CadFilial.Controle = Controle;
                    CadFilial.LerDados(CadNota.IdFilial);

                    Controles.NF_e Nfe = new Controles.NF_e();
                    Nfe.Controle = Controle;
                    Nfe.Inicializar_parametros(CadNota.IdFilial);
                    
                    string NfeProtocolo = Nfe.ConsultaProcotoclo(CadFilial.Cnpj,CadNota.ChaveNfe);
                    if (NfeProtocolo != "")
                    {
                        Controle.ExecutaSQL("Update NotaFiscal set Status=1,ProtocoloNFe='" + NfeProtocolo + "' Where Id_Nota=" + CadNota.IdNota.ToString());
                        CadNota.LerDados(CadNota.IdNota);
                        MessageBox.Show("Revalidação Concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Numero de Procoloco não localizado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Favor Verificar a Chave da NFE", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            BtnRevalidNfe.Enabled = true;
        }

        private void ValidarCST(NotaFiscal CadNFe, NotaFiscalItens Item)
        {
            Filiais CadFilial = new Filiais();
            CadFilial.Controle = Controle;
            CadFilial.LerDados(CadNFe.IdFilial);

            Produtos CadPrd = new Produtos();
            CadPrd.Controle = Controle;
            CadPrd.LerDados(Item.IdProduto);
            
            
            //if (Item.SitTributaria == 0 && (CadNFe.IdFilial == 1 || CadNFe.IdFilial == 6) && CadNFe.IdUf != CadFilial.Uf)
            if (Item.SitTributaria == 0 && (CadNFe.IdFilial == 1 || CadNFe.IdFilial == 6) && CadNFe.IdUf != CadFilial.Uf && CadNFe.InscUf.Trim() != "")            
                Item.Cst = 10;
            else
            {
                if (Item.SitTributaria == 0)
                {
                    Item.Cst = 1;

                    if (Item.PercRed > 0)
                        Item.Cst = 3;
                }
                else if (Item.SitTributaria == 1)
                {
                    Item.Cst = 6;
                }
                else if (Item.SitTributaria == 2)
                    Item.Cst = 5;
                else if (Item.SitTributaria == 3)
                {
                    Item.Cst = 8;
                    //if (Item.PercRed > 0)
                    //    Item.Cst = 9;
                }                
            }            
        }
        private void BtnImpTransf_Click(object sender, EventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (CadNota.IdNota > 0)
                {
                   /* if (CadNota.IdCfop == 0)
                    {
                        MessageBox.Show("Favor Verificar o CFOP da nota", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }*/
                    if (CadNota.IdPessoa == 0)
                    {
                        MessageBox.Show("Favor Informar o Destinatario da Nota", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmImpNfeTransf Frm = new FrmImpNfeTransf();
                    Frm.IdNota       = CadNota.IdNota;
                    Frm.IdCfop       = CadNota.IdCfop;
                    Frm.CadPessoa    = CadPessoa;
                    Frm.FrmPrincipal = FrmPrincipal;
                    Frm.ShowDialog();
                    PopularGridItens();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NotaFiscal Nt = new NotaFiscal();
            Nt.Controle = Controle;

            NotaFiscalItens NtItens = new NotaFiscalItens();
            NtItens.Controle = Controle;


            //button2.Text = GridDados.Rows.Count.ToString() + " - " + I.ToString(); ;
            //button2.Update();
            int NumNt = CadNota.IdNota;
            Nt.LerDados(NumNt);
            //button2.Text = GridDados.Rows.Count.ToString() + " - " + I.ToString();
            SqlDataReader Tab = Controle.ConsultaSQL("SELECT * FROM NOTAFISCALITENS WHERE Id_NOTA=" + Nt.IdNota.ToString());
            int Item = 1;
            while (Tab.Read())
            {
                button2.Text = Item.ToString();
                button2.Update();
                Application.DoEvents();
                NtItens.LerDados(int.Parse(Tab["ID_ITEM"].ToString()));
                if (NtItens.IdItem > 0)
                {
                    ValidarCST(Nt, NtItens);
                    NtItens.GravarDados();
                }
                Item = Item + 1;
            }
            Nt.GravarDados();
            PopularGridItens();
            MessageBox.Show("Fim");
        }

        private void Btn_Carta_Click(object sender, EventArgs e)
        {
            if (CadNota.ProtocoloNfe != "" && CadNota.Status==1)
            {
                if (CadNota.ProtocoloCarta != "")
                {
                    MessageBox.Show("Carta de Correção já emitida.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               
                if (MessageBox.Show("Confirma o Envio da Carta de Correção " + CadNota.NumNota, "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CadNota.ProtocoloNfe != "")
                    {
                        if (TxtCartaCorrecao.Text.Trim().Length > 15)
                        {
                            Controles.NF_e Nfe = new Controles.NF_e();
                            Nfe.Controle = Controle;
                            Nfe.Inicializar_parametros(CadNota.IdFilial);
                            string Protocolo = Nfe.CartaCorrecao(CadNota.ChaveNfe.Trim(), CadNota.ProtocoloNfe.Trim(), TxtCartaCorrecao.Text.Trim());

                            if (Protocolo != "" && int.Parse(Nfe.cStat) <= 136)
                            {                               
                                Controle.ExecutaSQL("Update NotaFiscal set CartaCorrecao='"+TxtCartaCorrecao.Text.Trim()+"', ProtocoloCarta='" + Protocolo + "',DataCarta=convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) Where Id_Nota=" + CadNota.IdNota.ToString());
                                FrmPrincipal.RegistrarAuditoria(this.Text, CadNota.IdNota, CadNota.NumNota.ToString(), 6, "Carta de Correção da Nota Fiscal");
                                MessageBox.Show("Carta de Correção Registrada", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CadNota.CartaCorrecao = TxtCartaCorrecao.Text.Trim();
                                CadNota.ProtocoloCarta = Protocolo;
                                TxtProtocoloCarta.Text = Protocolo;
                            }
                            else
                                MessageBox.Show("Carta de Correção não foi Registrada, Motivo:" + Nfe.vMotivoRet, "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Informa o conteudo da Carta de Correção", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Numero de Procoloco não localizado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Favor Verificar a Chave da NFE", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            BtnRevalidNfe.Enabled = true;
        }

        private void Btn_Visualizar_Click(object sender, EventArgs e)
        {

            if (CadNota.VlrNota <= 0)
            {
                MessageBox.Show("Favor Verificar o Valor Total", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CadNota.Status == 2)
            {
                MessageBox.Show("Nota Fiscal Cancelada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Filiais CadFilial = new Filiais();
            CadFilial.Controle = Controle;
            CadFilial.LerDados(CadNota.IdFilial);

            Btn_Visualizar.Enabled = false;
            if (CadNota.IdPessoa == 0)
            {
                MessageBox.Show("Favor informar o destinatário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Btn_Visualizar.Enabled = true;
                return;
            }


            if (!ValidarCFOP(CadNota.IdNota, CadFilial.Uf))
            {
                Btn_Visualizar.Enabled = true;
                return;
            }

            string sSQL = "SELECT T2.ENTSAIDA,T2.RAZAOSOCIAL,T2.CNPJCPF,T2.INSC_UF,T2.DTEMISSAO,RTRIM(T2.ENDERECO)+','+RTRIM(T2.NUMERO)+' '+RTRIM(T2.COMPLEMENTO) AS ENDERECO," +
                          " T2.TELEFONE,T2.CEP,T2.BAIRRO,T2.CIDADE,T4.SIGLA,T3.REFERENCIA,T3.DESCRICAO,T3.UNIDADE,T1.QTDE,T1.VLRUNITARIO,T1.VLRTOTAL,T1.PICMS,T1.PIPI,T1.VlrIpi as VlrIpiItem," +
                          " T2.BICMS,T2.VLRICMS,T2.BICMSSUB,T2.VLRICMSSUB,T2.VLRFRETE,T2.VLRSEGURO,T2.VLROUTRADESP,T2.VLRFRETE,T2.VLRIPI,T2.VLRPRODUTOS,T2.VLRNOTA,T2.VLRDESCONTO,T2.FRETE," +
                          " T2.QTDEVOLUME,T2.ESPECIE,T2.MARCA,T2.PESOBRUTO,T2.PESOLIQUIDO,T2.NUMFORMULARIO,T2.NUMNOTA,T2.OBSERVACAO,T8.CFOP,CASE T2.ID_VENDA WHEN 0 THEN ' ' ELSE T7.VENDEDOR END AS VENDEDOR," +
                          " T2.ChaveNFe,T2.ProtocoloNFe,T2.ReciboNFe,T1.VLRICMS AS VLRICMSITEM,T3.REDUCAO,T1.SITTRIBUTARIA,T3.NCM,T1.PERCRED, T9.RAZAOSOCIAL AS TRANSPORTADORA, T9.ENDERECO AS ENDTRANSP, T9.NUMERO AS NUMTRANSP,T9.COMPLEMENTO AS COMPTRANSP," +
                          " T9.INSC_UF AS CGFTRANSP, T9.CIDADE AS CIDTRANSP,ISNULL(T1.ID_REDUCAO,0) AS ID_REDUCAO,T0.CODRED,T0.REFREDUCAO," +
                          " CASE ISNULL(T2.NATOP,0) WHEN 0 THEN 'VENDA' WHEN 1 THEN 'TRANSFERÊNCIA' WHEN 2 THEN 'BONIFICAÇÃO' WHEN 3 THEN 'DEVOLUÇÃO' WHEN 4 THEN 'COMPRA' WHEN 5 THEN 'REMESSA' WHEN 6 THEN 'DEMOSTRAÇÃO' WHEN 7 THEN 'RETORNO' WHEN 8 THEN 'EXPOSIÇÃO'" +
                          " WHEN 9 THEN 'OUTRAS' WHEN 10 THEN 'VENDA A ORDEM' WHEN 11 THEN 'REMESSA MERC. POR CONTA E ORDERM DE TERC.' WHEN 12 THEN 'COMPLEMENTAR' END AS NATUREZA, IsNull(T1.CST,0) as CST," +
                          " T1.ID_PROMOCAO,T9.CNPJ AS CNPJTRANS, E1.SIGLA AS UFTRANSF,T2.VENCFATURA FROM NOTAFISCALITENS T1" +
                          " LEFT JOIN NOTAFISCAL T2 ON (T2.ID_NOTA=T1.ID_NOTA)" +
                          " LEFT JOIN PRODUTOS   T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                          " LEFT JOIN ESTADOS    T4 ON (T4.ID_UF=T2.ID_UF)" +
                          " LEFT JOIN CFOP       T5 ON (T5.ID_CFOP=T2.ID_CFOP)" +
                          " LEFT JOIN CFOP       T8 ON (T8.ID_CFOP=T1.ID_CFOP)" +
                          " LEFT OUTER JOIN MVVENDA AS T6 ON T6.ID_VENDA=T2.ID_VENDA" +
                          " LEFT OUTER JOIN VENDEDORES AS T7 ON T7.ID_VENDEDOR=T6.ID_VENDEDOR  " +
                          " LEFT JOIN TRANSPORTADORAS AS T9 ON (T9.ID_TRANSPORTADORA=T2.ID_TRANSPORTADORA)" +
                          " LEFT JOIN REDUCAOFISCAL AS T0 ON (T0.ID_REDUCAO=T1.ID_REDUCAO)" +
                          " LEFT JOIN ESTADOS    E1 ON (E1.ID_UF=T9.ID_UF)" +
                          " WHERE T1.ID_NOTA=" + CadNota.IdNota.ToString() + " ORDER BY T1.ID_ITEM";


            Parametros ParamFilial = new Parametros();
            ParamFilial.Controle = Controle;
            ParamFilial.LerDados(CadNota.IdFilial);
            FrmRelatorios FrmRel = new FrmRelatorios();


            string TxtReducao = "";
            //
            SqlDataReader TabRed = Controle.ConsultaSQL("SELECT DISTINCT T2.CODRED +' - '+T2.RefReducao as TxtReducao FROM NotaFiscalItens T1" +
                                                        " LEFT JOIN ReducaoFiscal T2 ON (T2.Id_Reducao=T1.ID_REDUCAO)" +
                                                        " WHERE T1.ID_NOTA=" + CadNota.IdNota.ToString() + " AND T1.ID_REDUCAO > 0");
            while (TabRed.Read())
                TxtReducao = TxtReducao + "( " + TabRed["TxtReducao"].ToString().Trim() + " ) " + Environment.NewLine;

            if (!FrmPrincipal.VersaoDistribuidor)
            {
                if (CadNota.IdFilial == 3 || CadNota.IdFilial == 4 || CadNota.IdFilial == 5)
                {
                    SqlDataReader TabPrd = Controle.ConsultaSQL("SELECT * FROM NotaFiscalItens T1" +
                                                                " LEFT JOIN PRODUTOS T2 ON (T2.Id_Produto=T1.ID_Produto)" +
                                                                " WHERE T1.ID_NOTA=" + CadNota.IdNota.ToString() + " AND T2.ID_GRUPO=288");
                    while (TabPrd.Read())
                        TxtReducao = "Empresa Optante do Simples Nacional com crédito de 3,10% no CFOP 5.102 dos produtos";
                }
            }
            TabRed = Controle.ConsultaSQL("SELECT Top 1 T1.Id_Promocao FROM NotaFiscalItens T1" +
                                          " WHERE T1.ID_NOTA=" + CadNota.IdNota.ToString() + " AND T1.ID_PROMOCAO > 0");


            while (TabRed.Read())
                TxtReducao = TxtReducao + "(P** Preço Promocional) " + Environment.NewLine;


            if (CadNota.ProtocoloNfe.Trim() == "")
            {
                Relatorios.RelNotaEletronica RelNF = new Relatorios.RelNotaEletronica();
                DataSet TabRel = new DataSet();
                TabRel = Controle.ConsultaTabela(sSQL);
                RelNF.SetDataSource(TabRel.Tables[0]);

                CrystalDecisions.Shared.ParameterValues P_Regime = new CrystalDecisions.Shared.ParameterValues();
                P_Regime.AddValue(CadFilial.Regime);
                RelNF.ParameterFields[0].CurrentValues = P_Regime;

                FrmRel.cryRepRelatorio.ReportSource = RelNF;
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblEndEmitente"])).Text = CadFilial.Endereco.Trim() + "," + CadFilial.Numero.Trim() + " - " + CadFilial.Bairro.Trim() + "  CEP:" + CadFilial.Cep.Trim() + " - " + CadFilial.Cidade.Trim() + " Fone:" + CadFilial.Fone1.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblEmitente"])).Text = CadFilial.Filial.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblInscUF"])).Text = CadFilial.InscUF.Trim();
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["LblCnpj"])).Text = CadFilial.Cnpj.Substring(0, 2) + "." + CadFilial.Cnpj.Substring(2, 3) + "." + CadFilial.Cnpj.Substring(5, 3) + "/" + CadFilial.Cnpj.Substring(8, 4) + "-" + CadFilial.Cnpj.Substring(12, 2);
                ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelNF.Section2.ReportObjects["TxtReducao"])).Text = TxtReducao;
                FrmRel.ShowDialog();
            }
            else
                MessageBox.Show("Nota Fiscal Eletrônica já foi transmitida", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Information);           

            Btn_Visualizar.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Controles.NF_e Nfe = new Controles.NF_e();
            Nfe.Controle = Controle;
            Nfe.Inicializar_parametros(7);
            Nfe.BuscaXMLNFe("0");
        }

        private void BtnImpMDFe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Erro no Envio, favor Verificar a Versão do MFE e do MDF");
        }

        private void BtnImpTransfMv_Click(object sender, EventArgs e)
        {
            FrmImpMvEstoque FrmImpNF = new FrmImpMvEstoque();
            FrmImpNF.FrmPrincipal = FrmPrincipal;
            FrmImpNF.ShowDialog();

            if (FrmImpNF.IdMov > 0)
            {                
                Produtos CadPrd = new Produtos();
                CadPrd.Controle = Controle;
                DataSet ConsItens = new DataSet();
                ConsItens = Controle.ConsultaTabela("SELECT * FROM MvEstoqueItens WHERE ID_Mov=" + FrmImpNF.IdMov.ToString());
                if (ConsItens.Tables[0].Rows.Count > 0)
                {
                    FrmPrincipal.BSta_BarProcesso.Maximum = ConsItens.Tables[0].Rows.Count;                                        
                    for (int I = 0; I <= ConsItens.Tables[0].Rows.Count - 1; I++)
                    {
                        CadPrd.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString()));
                        ItemCadNota.LerDados(0);
                        ItemCadNota.IdNota        = CadNota.IdNota;
                        ItemCadNota.IdProduto     = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                        ItemCadNota.Qtde          = decimal.Parse(ConsItens.Tables[0].Rows[I]["Qtde"].ToString());
                        ItemCadNota.VlrUnitario   = CadPrd.UltPrcCompra;
                        ItemCadNota.IdCfop        = 70;
                        ItemCadNota.Cst           = 8;
                        ItemCadNota.SitTributaria = 3;

                        if (CadPrd.IdGrupo == 53)
                        {
                            if (CadPessoa.PDescNFGrpTalimpo > 0)
                                ItemCadNota.VlrUnitario = Math.Round(CadPrd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpTalimpo / 100)), 2);
                        }
                        else
                        {
                            if (CadPessoa.PDescNFGrpOutros > 0)
                                ItemCadNota.VlrUnitario = Math.Round(CadPrd.PrcAtacado * (1 - (CadPessoa.PDescNFGrpOutros / 100)), 2);
                        }
                        
                        ItemCadNota.GravarDados();
                        FrmPrincipal.BSta_BarProcesso.Maximum = FrmPrincipal.BSta_BarProcesso.Maximum + 1;
                    }
                    CadNota.GravarDados();                    
                    PopularGridItens();
                }
                MessageBox.Show("Importação concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmPrincipal.BSta_BarProcesso.Maximum = 0;
                FrmImpNF.Dispose();
            }
        }
    }    
}

