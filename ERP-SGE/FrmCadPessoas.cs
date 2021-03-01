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
using System.Xml;
using System.Data.SqlClient;


namespace ERP_SGE
{
    public partial class FrmCadPessoas: Form
    {
        Funcoes Controle                = new Funcoes();
        Pessoas Pessoa                  = new Pessoas();        
        Auditoria RegAuditoria          = new Auditoria();
        ComissaoPrdCliente ComissaoPrd  = new ComissaoPrdCliente();
        ClientesPrdComodato PrdComodato = new ClientesPrdComodato();

        private DataSet TabItens;
        private BindingSource Source_Itens;

        private DataSet TabComodItens;
        private BindingSource Source_ComodItens;

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        public FrmCadPessoas()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao          = FrmPrincipal.Conexao;
            ComissaoPrd.Controle      = Controle;
            Pessoa.Controle           = Controle;
            PrdComodato.Controle      = Controle;
            Pessoa.IdPessoa           = 0;
            RegAuditoria.Controle     = Controle;
            LstPesquisa.SelectedIndex = 1;
            label41.Visible           = !FrmPrincipal.VersaoDistribuidor;
            TxtSenha.Visible          = !FrmPrincipal.VersaoDistribuidor;
            TabItens          = new DataSet();
            Source_Itens      = new BindingSource();
            TabComodItens     = new DataSet();
            Source_ComodItens = new BindingSource();
            //PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            string sSQL = "SELECT T1.Id_Pessoa,T1.Cnpj,T1.RazaoSocial,T1.Fantasia,T1.Fone,T1.CONTATO,T2.VENDEDOR,Rtrim(T1.Endereco)+','+RTrim(T1.Numero)+' '+Rtrim(T1.Complemento)+' '+RTrim(T1.Cep)+' '+RTrim(T1.Bairro)+' '+Rtrim(T1.Cidade) as Logradouro FROM Pessoas T1 LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) ";
            if (TxtPesquisa.Text.Trim() != "")
            {
                try
                {   
                    if (LstPesquisa.SelectedIndex == 0)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Cnpj LIKE '%{0}%' order by T1.Cnpj", TxtPesquisa.Text.Trim()));
                    else if (LstPesquisa.SelectedIndex == 1)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.RazaoSocial LIKE '%{0}%' order by T1.RazaoSocial", TxtPesquisa.Text.Trim()));
                    else if (LstPesquisa.SelectedIndex == 2)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Fantasia LIKE '%{0}%' order by T1.Fantasia", TxtPesquisa.Text.Trim()));
                    else if (LstPesquisa.SelectedIndex == 3)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Endereco LIKE '%{0}%' order by T1.Endereco", TxtPesquisa.Text.Trim()));
                    else if (LstPesquisa.SelectedIndex == 4)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + " WHERE T1.Fone LIKE '%{0}%' order by T1.Fone", TxtPesquisa.Text.Trim()));                    
                }
                catch
                {
                    MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                Tabela = Controle.ConsultaTabela(sSQL);

            BindingSource Source = new BindingSource();
            Source.DataSource    = Tabela;
            Source.DataMember    = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Pessoa", Pessoa.IdPessoa);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            Pessoa.LerDados(Isn);
            if (Pessoa.Tipo == 0)
                Rb_Juridica.Checked = true;
            else
                Rb_Fisica.Checked = true;
            TelaCadastro();
            TxtCodigo.Text                  = Pessoa.IdPessoa.ToString();
            TxtRazaoSocial.Text             = Pessoa.RazaoSocial;
            TxtFantasia.Text                = Pessoa.Fantasia;
            TxtCnpj.Text                    = Pessoa.Cnpj;
            TxtInscUF.Text                  = Pessoa.InscUF;
            TxtCep.Text                     = Pessoa.Cep;
            TxtEndereco.Text                = Pessoa.Endereco;
            TxtNumero.Text                  = Pessoa.Numero;
            TxtComplemento.Text             = Pessoa.Complemento;
            TxtBairro.Text                  = Pessoa.Bairro;
            TxtCidade.Text                  = Pessoa.Cidade;
            LstUF.SelectedValue             = Pessoa.IdUF.ToString();
            LstPais.SelectedValue           = Pessoa.Pais;
            TxtFone.Text                    = Pessoa.Fone;
            TxtFax.Text                     = Pessoa.Fax;
            TxtEmail.Text                   = Pessoa.Email;
            TxtContato.Text                 = Pessoa.Contato;
            TxtCelular.Text                 = Pessoa.Celular;
            LstCusto.SelectedValue          = Pessoa.IdCusto.ToString();
            LstDepartamento.SelectedValue   = Pessoa.IdDepartamento.ToString();
            LstAtividade.SelectedValue      = Pessoa.IdAtividade.ToString();
            LstTransportadora.SelectedValue = Pessoa.IdTransportadora.ToString();
            LstCFOP.SelectedValue           = Pessoa.IdCfop.ToString();
            LstRotas.SelectedValue          = Pessoa.IdRota.ToString();
            LstFormaPgto.SelectedValue      = Pessoa.IdFormaPgto.ToString();            
            Chk_Ativo.Checked               = Pessoa.Ativo == 1;
            Chk_BloqPgto.Checked            = Pessoa.BloqFormaPgto == 1;
            LstTipoCadastro.SelectedIndex   = Pessoa.Clie_Forn;
            TxtCepCobranca.Text             = Pessoa.CepCobranca;
            TxtEndCobranca.Text             = Pessoa.EndCobranca;
            TxtNumCobranca.Text             = Pessoa.NumCobranca;
            TxtComplCobranca.Text           = Pessoa.ComplCobranca;
            TxtBairroCobranca.Text          = Pessoa.BairroCobranca;
            TxtCidadeCobranca.Text          = Pessoa.CidadeCobranca;
            LstUfCobranca.SelectedValue     = Pessoa.IdUfCobranca.ToString();
            TxtLimiteCredito.Value          = Pessoa.LimiteCredito;
            TxtDataCadastro.Value           = Pessoa.DataCadastro.Date;
            LstFilial.SelectedValue         = Pessoa.IdFilial;
            LstVendedor.SelectedValue       = Pessoa.IdVendedor;
            TxtCredito.Value                = Pessoa.Credito;
            TxtObsSERASA.Text               = Pessoa.ObsSerasa;
            TxtSenha.Text                   = Pessoa.Senha;
            TxtIDServidor.Text              = Pessoa.IdServidor.ToString();
            Ck_MargemNegocio.Checked        = Pessoa.MargemNegocio == 1;
            TxtEmailNFE.Text                = Pessoa.EmailNFE;
            TxtObs.Text                     = Pessoa.Observacao;
            TxtComissaoFixa.Value           = Pessoa.ComissaoFixa;
            TxtObsEntrega.Text              = Pessoa.ObsEntrega;
            TxtPrazoPgto.Text               = Pessoa.PrazoPgto;
            TxtDescNFTalimpo.Value          = Pessoa.PDescNFGrpTalimpo;
            TxtDescNFOutros.Value           = Pessoa.PDescNFGrpOutros;
            TxtCodMun.Value                 = Pessoa.CodMun;
            Ck_NotificaAltPrc.Checked       = Pessoa.NotificaAltPrc == 1;
            Ck_ForaMediaCom.Checked         = Pessoa.ForaMediaCom == 1;
            Ck_NaoVerifQtdeCx.Checked       = Pessoa.NaoVerifQtdeCx == 1;
            Ck_Comodato.Checked             = Pessoa.Comodato == 1;
            Ck_KitNfe.Checked               = Pessoa.KitNfe == 1;
            Ck_NaoPrzPg.Checked             = Pessoa.NaoVerPrazoPg == 1;
            Ck_LiberaPrc.Checked            = Pessoa.LiberaPrc == 1;
            Ck_Serasa.Checked                = Pessoa.Serasa == 1;
            TxtLimiteCredito.Enabled        = FrmPrincipal.Perfil_Usuario.AlteraFinanceiro==1;


            if (Pessoa.Clie_Forn == 4)
                Pessoa.IdVinculo = 0;

            if (Pessoa.Frete == 0)
                Rb_Emitente.Checked = true;
            else
                Rb_Destinatario.Checked = true;
            SetaPessoa(Pessoa.IdVinculo);          
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            else
            {
                StaFormEdicao = true;
                Paginas.SelectTab(1);
                LimpaDados();
                PopularGridItens();
                Rb_Juridica.Checked = true;
                Chk_Ativo.Checked = true;
                LstTipoCadastro.SelectedIndex = 0;
                TxtLimiteCredito.Value = 1000;
                TelaCadastro();
                LstFilial.SelectedValue = FrmPrincipal.Parametros_Filial.IdFilial.ToString();
                TxtDataCadastro.Value = DateTime.Now;
                FrmPrincipal.ControleBotoes(true);
                TxtCnpj.Focus();
            }
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
                if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                    MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                    TelaCadastro();
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    TxtCnpj.Focus();
                    PopularGridItens();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            bool AltCadastro = false;
            if (TxtCnpj.Text.Trim() != "")
            {
                if (VerificaCNPJCPF())
                {
                    if (int.Parse(TxtCodigo.Text) == 0)
                    {
                        if (FrmPrincipal.Perfil_Usuario.CadDistrib == 0 && LstTipoCadastro.SelectedIndex == 3)
                        {
                            MessageBox.Show("Usuário não Autorização para Cadastrar Distribuidor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (LstTipoCadastro.SelectedIndex == 0 && Rb_Fisica.Checked && (!FrmPrincipal.VersaoDistribuidor))
                    {
                        if (TxtEndereco.Text.Trim() == "" || TxtNumero.Text.Trim() == "" || TxtBairro.Text.Trim() == "" || TxtCidade.Text.Trim() == "" || TxtCelular.Text.Trim() == "")
                        {
                            MessageBox.Show("Cadastro incompleto, favor verificar os Dados", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;                            
                        }

                        if (FrmPrincipal.Perfil_Usuario.AutCadPF == 0)
                        {
                            FrmAutorizacao Autorizacao = new FrmAutorizacao();
                            Autorizacao.FrmPrincipal = FrmPrincipal;
                            Autorizacao.ShowDialog();
                            //Verificando se o Acesso foi liberado
                            if (Autorizacao.AcessoOk)
                            {
                                if (Autorizacao.Usuario.AutCadPF== 0)
                                {
                                    MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else
                                    FrmPrincipal.RegistrarAuditoria(this.Text, Pessoa.IdPessoa, Pessoa.Cnpj, 9, "Autorização de Cadastro");
                            }

                            else
                            {
                                MessageBox.Show("Autorização negada para esse usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                            FrmPrincipal.RegistrarAuditoria(this.Text, Pessoa.IdPessoa, Pessoa.Cnpj, 9, "Autorização de Cadastro");
                    }

                    Pessoa.IdPessoa          = int.Parse(TxtCodigo.Text);
                    Pessoa.RazaoSocial       = TxtRazaoSocial.Text;
                    Pessoa.Fantasia          = TxtFantasia.Text;
                    Pessoa.Cnpj              = TxtCnpj.Text;
                    Pessoa.InscUF            = TxtInscUF.Text;
                    Pessoa.Cep               = TxtCep.Text.Replace("-", "");
                    Pessoa.Endereco          = TxtEndereco.Text;
                    Pessoa.Numero            = TxtNumero.Text;
                    Pessoa.Complemento       = TxtComplemento.Text;
                    Pessoa.Bairro            = TxtBairro.Text;
                    Pessoa.Cidade            = TxtCidade.Text;
                    Pessoa.IdUF              = int.Parse(LstUF.SelectedValue.ToString());
                    Pessoa.Pais              = LstPais.SelectedValue.ToString();
                    Pessoa.Fone              = TxtFone.Text;
                    Pessoa.Fax               = TxtFax.Text;
                    Pessoa.Email             = TxtEmail.Text;
                    Pessoa.Contato           = TxtContato.Text;
                    Pessoa.Celular           = TxtCelular.Text;
                    Pessoa.IdCusto           = int.Parse(LstCusto.SelectedValue.ToString());
                    Pessoa.IdDepartamento    = int.Parse(LstDepartamento.SelectedValue.ToString());
                    Pessoa.IdAtividade       = int.Parse(LstAtividade.SelectedValue.ToString());
                    Pessoa.IdTransportadora  = int.Parse(LstTransportadora.SelectedValue.ToString());
                    Pessoa.IdCfop            = int.Parse(LstCFOP.SelectedValue.ToString());
                    Pessoa.IdFormaPgto       = int.Parse(LstFormaPgto.SelectedValue.ToString());
                    Pessoa.IdRota            = int.Parse(LstRotas.SelectedValue.ToString());
                    Pessoa.IdVendedor        = int.Parse(LstVendedor.SelectedValue.ToString());
                    Pessoa.Clie_Forn         = LstTipoCadastro.SelectedIndex;
                    Pessoa.Observacao        = TxtObs.Text;
                    Pessoa.LimiteCredito     = TxtLimiteCredito.Value;
                    Pessoa.CepCobranca       = TxtCepCobranca.Text.Replace("-", "");
                    Pessoa.EndCobranca       = TxtEndCobranca.Text;
                    Pessoa.NumCobranca       = TxtNumCobranca.Text;
                    Pessoa.ComplCobranca     = TxtComplCobranca.Text;
                    Pessoa.BairroCobranca    = TxtBairroCobranca.Text;
                    Pessoa.CidadeCobranca    = TxtCidadeCobranca.Text;
                    Pessoa.IdUfCobranca      = int.Parse(LstUfCobranca.SelectedValue.ToString());
                    Pessoa.DataCadastro      = TxtDataCadastro.Value;
                    Pessoa.ComissaoFixa      = TxtComissaoFixa.Value;
                    Pessoa.IdFilial          = int.Parse(LstFilial.SelectedValue.ToString());
                    Pessoa.ObsSerasa         = TxtObsSERASA.Text;
                    Pessoa.IdServidor        = int.Parse(TxtIDServidor.Text);
                    Pessoa.CodMun            = int.Parse(TxtCodMun.Value.ToString());
                    Pessoa.EmailNFE          = TxtEmailNFE.Text;
                    Pessoa.ObsEntrega        = TxtObsEntrega.Text;
                    Pessoa.PrazoPgto         = TxtPrazoPgto.Text;
                    Pessoa.PDescNFGrpTalimpo = TxtDescNFTalimpo.Value;
                    Pessoa.PDescNFGrpOutros  = TxtDescNFOutros.Value;
                    if (Ck_Comodato.Checked)       Pessoa.Comodato       = 1; else Pessoa.Comodato       = 0;
                    if (Chk_Ativo.Checked)         Pessoa.Ativo          = 1; else Pessoa.Ativo          = 0;
                    if (Rb_Juridica.Checked)       Pessoa.Tipo           = 0; else Pessoa.Tipo           = 1;
                    if (Rb_Emitente.Checked)       Pessoa.Frete          = 0; else Pessoa.Frete          = 1;
                    if (Ck_KitNfe.Checked)         Pessoa.KitNfe         = 1; else Pessoa.KitNfe         = 0;
                    if (Chk_BloqPgto.Checked)      Pessoa.BloqFormaPgto  = 1; else Pessoa.BloqFormaPgto  = 0;
                    if (Ck_MargemNegocio.Checked)  Pessoa.MargemNegocio  = 1; else Pessoa.MargemNegocio  = 0;
                    if (Ck_ForaMediaCom.Checked)   Pessoa.ForaMediaCom   = 1; else Pessoa.ForaMediaCom   = 0;
                    if (Ck_NaoVerifQtdeCx.Checked) Pessoa.NaoVerifQtdeCx = 1; else Pessoa.NaoVerifQtdeCx = 0;
                    if (Ck_NaoPrzPg.Checked)       Pessoa.NaoVerPrazoPg  = 1; else Pessoa.NaoVerPrazoPg  = 0;
                    if (Ck_LiberaPrc.Checked)      Pessoa.LiberaPrc      = 1; else Pessoa.LiberaPrc = 0;
                    if (Ck_Serasa.Checked)         Pessoa.Serasa         = 1; else Pessoa.Serasa = 0;
                    if (Ck_NotificaAltPrc.Checked && Ck_NotificaAltPrc.Visible) Pessoa.NotificaAltPrc = 1; else Pessoa.NotificaAltPrc = 0;

                    if (TxtSenha.Text.Trim() != Pessoa.Senha.Trim())
                        Pessoa.Senha = Controle.Crypt(TxtSenha.Text.Trim());

                    Pessoa.GravarDados();
                    if (TxtCodigo.Text == "0")
                        FrmPrincipal.RegistrarAuditoria(this.Text, Pessoa.IdPessoa, Pessoa.Cnpj, 1, "Inclusão o Lançamento");
                    else
                        FrmPrincipal.RegistrarAuditoria(this.Text, Pessoa.IdPessoa, Pessoa.Cnpj, 2, "Alteração do Lançamento");

                    EnviarXML();                    
                    
                    PopularGrid();
                    PopularCampos(Pessoa.IdPessoa);
                    PopularGridItens();
                    StaFormEdicao = false;
                    FrmPrincipal.ControleBotoes(false);                    
                }
            }
            else
            {
                MessageBox.Show("CNPJ não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCnpj.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                    MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        Pessoa.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));                        
                        Pessoa.Excluir();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text,Pessoa.IdPessoa, Pessoa.Cnpj, 3, "Exclusão do Lançamento");
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
            //
            LstUF             = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUF);
            LstUfCobranca     = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUfCobranca);
            LstCusto          = FrmPrincipal.PopularCombo("SELECT T2.ID_CUSTO,'('+RTRIM(T1.GRUPO)+') <=> '+RTRIM(T2.CUSTO) FROM GRUPOCCUSTO T1 LEFT JOIN CENTROCUSTO T2 ON (T2.ID_GRPCUSTO=T1.ID_GRPCUSTO) ORDER BY T1.TIPO,T1.GRUPO,T2.CUSTO", LstCusto);
            LstDepartamento   = FrmPrincipal.PopularCombo("SELECT Id_Departamento,Departamento FROM Departamentos ORDER BY Departamento", LstDepartamento);
            LstAtividade      = FrmPrincipal.PopularCombo("SELECT T1.Id_Atividade,SUBSTRING(T2.GRUPO,1,30)+' / '+RTRIM(T1.Atividade) AS ATIVIDADE FROM RamoAtividade T1 LEFT JOIN GRUPOATIVIDADE T2 ON (T2.ID_GRPATIVIDADE=T1.ID_GRPATIVIDADE) ORDER BY T2.GRUPO,T1.Atividade", LstAtividade);
            LstTransportadora = FrmPrincipal.PopularCombo("SELECT Id_Transportadora,RazaoSocial FROM Transportadoras ORDER BY RazaoSocial", LstTransportadora);
            LstCFOP           = FrmPrincipal.PopularCombo("SELECT Id_Cfop,Cfop+'  '+Natureza FROM Cfop ORDER BY Natureza", LstCFOP);
            LstFormaPgto      = FrmPrincipal.PopularCombo("SELECT Id_FormaPgto,FormaPgto FROM FormaPagamento ORDER BY FormaPgto", LstFormaPgto);
            LstRotas          = FrmPrincipal.PopularCombo("SELECT Id_Rota,Rota FROM Rotas ORDER BY Rota", LstRotas);
            LstFilial         = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilial);
            LstVendedor       = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", LstVendedor);
            LstPais           = FrmPrincipal.PopularCombo("SELECT CHAVE,DESCRICAO AS PAIS FROM TABELASAUX WHERE CAMPO='PAIS' ORDER BY DESCRICAO", LstPais);

            LstFormaPgto.Enabled = FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 1;
            Chk_BloqPgto.Enabled = FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 1;
        }
        private void LimpaDados()
        {
            TxtCodigo.Text                  = "0";
            TxtRazaoSocial.Text             = "";
            TxtFantasia.Text                = "";
            TxtCnpj.Text                    = "";
            TxtInscUF.Text                  = "";
            TxtCep.Text                     = "";
            TxtEndereco.Text                = "";
            TxtNumero.Text                  = "";
            TxtComplemento.Text             = "";
            TxtBairro.Text                  = "";
            TxtCidade.Text                  = "";
            LstUF.SelectedValue             = 0;
            TxtFone.Text                    = "";
            TxtFax.Text                     = "";
            TxtEmail.Text                   = "";
            TxtContato.Text                 = "";
            TxtCelular.Text                 = "";
            TxtObs.Text                     = "";
            LstFormaPgto.SelectedValue      = 0;
            LstRotas.SelectedValue          = 0;
            LstCusto.SelectedValue          = 0;
            LstDepartamento.SelectedValue   = 0;
            LstAtividade.SelectedValue      = 0;
            LstTransportadora.SelectedValue = 0;
            LstCFOP.SelectedValue           = 0;
            LstVendedor.SelectedValue       = 0;
            LstPais.SelectedValue           = "1058";
            TxtCepCobranca.Text             = "";
            TxtEndCobranca.Text             = "";
            TxtNumCobranca.Text             = "";
            TxtComplCobranca.Text           = "";
            TxtBairroCobranca.Text          = "";
            TxtCidadeCobranca.Text          = "";
            LstUfCobranca.SelectedValue     = 0;
            TxtLimiteCredito.Value          = 0;
            TxtComissaoFixa.Value           = 0;
            TxtObsSERASA.Text               = "";
            TxtSenha.Text                   = "";
            TxtIDServidor.Text              = "0";
            TxtEmailNFE.Text                = "";
            Rb_Juridica.Checked             = true;
            Chk_BloqPgto.Checked            = false;
            Rb_Emitente.Checked             = true;
            Ck_MargemNegocio.Checked        = false;
            Ck_ForaMediaCom.Checked         = false;
            TxtCodCliente.Text              = "0";
            TxtCliente.Text                 = "";
            TxtObsEntrega.Text              = "";
            TxtPrazoPgto.Text               = "";
            TxtDescNFTalimpo.Value          = 0;
            TxtDescNFOutros.Value           = 0;
            TxtCodMun.Value                 = 0;
            Ck_NotificaAltPrc.Checked       = false;
            Ck_NaoVerifQtdeCx.Checked       = false;
            Ck_Comodato.Checked             = false;
            Ck_KitNfe.Checked               = false;
            Ck_NaoPrzPg.Checked             = false;
            Ck_LiberaPrc.Checked            = false;
            Ck_Serasa.Checked               = false;
            Pessoa.LerDados(0);
            PopularGridItens();
        }
        private void Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (GridDados.CurrentRow != null)
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
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                    PopularGridItens();
                    PopularGridItensComod();
                }
            }
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();            
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
        private void TxtCnpj_Validated(object sender, EventArgs e)
        {
            VerificaCNPJCPF();
        }
        private bool VerificaCNPJCPF()
        {
            if (TxtCnpj.Text != "" && StaFormEdicao)
            {
                Verificar ExiteCnpjCpf = new Verificar();
                ExiteCnpjCpf.Controle = Controle;
                if (Rb_Juridica.Checked)
                {
                    if (!Controle.ValidarCnpj(TxtCnpj.Text))
                    {
                        MessageBox.Show("CNPJ inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtCnpj.Focus();
                        return false;
                    }
                    else
                    {
                        if (TxtCnpj.Text.Trim() == "99999999999999")
                            return true;
                        else
                        {
                            int CodPesq = ExiteCnpjCpf.Verificar_ExisteCadastro("ID_Pessoa", "SELECT * FROM PESSOAS WHERE CNPJ='" + TxtCnpj.Text.Trim() + "'");
                            if (CodPesq > 0 && CodPesq != int.Parse(TxtCodigo.Text))
                            {
                                MessageBox.Show("CNPJ já cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                PopularCampos(CodPesq);
                                StaFormEdicao = false;
                                FrmPrincipal.ControleBotoes(false);
                                TxtCnpj.Focus();
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if (!Controle.ValidarCpf(TxtCnpj.Text))
                    {
                        MessageBox.Show("CPF inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtCnpj.Focus();
                        return false;
                    }
                    else
                    {
                        if (TxtCnpj.Text.Trim() == "99999999999")
                            return true;
                        else
                        {
                            int CodPesq = ExiteCnpjCpf.Verificar_ExisteCadastro("ID_Pessoa", "SELECT * FROM PESSOAS WHERE CNPJ='" + TxtCnpj.Text.Trim() + "'");
                            if (CodPesq > 0 && CodPesq != int.Parse(TxtCodigo.Text))
                            {
                                MessageBox.Show("CPF já cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                PopularCampos(CodPesq);
                                StaFormEdicao = false;
                                FrmPrincipal.ControleBotoes(false);                                
                                TxtCnpj.Focus();
                                return false;
                            }
                        }
                    }
                }                
            }
            return true;
        }
        private void Rb_Juridica_Click(object sender, EventArgs e)
        {
            TelaCadastro();
            if (Pessoa.Tipo == 1)
            {
                TxtCnpj.Text = "";
                TxtInscUF.Text = "";
            }
            Pessoa.Tipo = 0; 
        }
        private void Rb_Fisica_Click(object sender, EventArgs e)
        {
            TelaCadastro();
            if (Pessoa.Tipo == 0)
            {
                TxtCnpj.Text = "";
                TxtInscUF.Text = "";
            }
            Pessoa.Tipo = 1; 
        }
        private void TelaCadastro()
        {
            if (Rb_Juridica.Checked)
            {
                TxtCnpj.Mask = "00,000,000/0000-00";
                label2.Text  = "CNPJ:";
                label6.Text  = "Insc.Estadual:";
                label5.Text  = "Razão Social:";
                label4.Text  = "Fantasia:";                               
            }
            else            
            {
                TxtCnpj.Mask = "000,000,000-00";
                label2.Text  = "CPF:";
                label6.Text  = "RG:";
                label5.Text  = "Nome:";
                label4.Text  = "Apelido:";                
            }
        }
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();
            if (e.ColumnIndex == 1)
            {
                if (e.Value.ToString().Trim().Length <= 11)                                    
                    MaskCol.Mask = "000,000,000-00";
                else
                    MaskCol.Mask = "00,000,000/0000-00";
                    
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            else if (e.ColumnIndex == 4)
            {
                MaskCol.Mask = "(00) 0000-0000";
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            MaskCol.Dispose();
        }
        private void TxtCep_Validated(object sender, EventArgs e)
        {
            if (TxtCep.Text.Replace("-", "").Trim() != "")
            {
                if (TxtCep.Text.Replace("-","").Trim()!=Pessoa.Cep.Trim())
                {
                    Verificar VerificaUF = new Verificar();
                    VerificaUF.Controle = Controle;
                    ConsultaCEP ConsultaCEP = new ConsultaCEP();
                    ConsultaCEP.VerificaCEP(TxtCep.Text);
                    TxtEndereco.Text    = ConsultaCEP.Endereco.ToUpper();
                    TxtBairro.Text      = ConsultaCEP.Bairro.ToUpper();
                    TxtCidade.Text      = ConsultaCEP.Cidade.ToUpper();
                    LstUF.SelectedValue = VerificaUF.Busca_IdUF(ConsultaCEP.UF);                    
                }
            }
        }
        private void TxtCepCobranca_Validated(object sender, EventArgs e)
        {
            if (TxtCepCobranca.Text.Replace("-", "").Trim() != "")
            {
                if (TxtCepCobranca.Text.Replace("-", "").Trim() != Pessoa.CepCobranca.Trim())
                {
                    Verificar VerificaUF = new Verificar();
                    VerificaUF.Controle = Controle;
                    ConsultaCEP ConsultaCEP = new ConsultaCEP();
                    ConsultaCEP.VerificaCEP(TxtCepCobranca.Text);
                    TxtEndCobranca.Text    = ConsultaCEP.Tipo + " " + ConsultaCEP.Endereco;
                    TxtBairroCobranca.Text = ConsultaCEP.Bairro;
                    TxtCidadeCobranca.Text = ConsultaCEP.Cidade;
                    LstUfCobranca.SelectedValue = VerificaUF.Busca_IdUF(ConsultaCEP.UF);
                }
            }
        }
        private void TxtInscUF_Validated(object sender, EventArgs e)
        {
            if (TxtInscUF.Text != "" && Rb_Juridica.Checked)
            {
                if (!Controle.ValidarCgf(TxtInscUF.Text))
                {
                    if (MessageBox.Show("Inscrição Estadual inválida, Deseja continuar", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        return;
                    else
                        TxtInscUF.Focus();
                }
            }
        }    
        //Comissão fixa por Produto
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO,T1.P_COMISSAO FROM COMISSAOPRDCLIENTE T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_PESSOA=" + Pessoa.IdPessoa.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource    = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("Id_Lanc", ComissaoPrd.IdLanc);
            Source_Itens.Position = item;
        }
        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    IncluirItem();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro de Pessoa em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
            {
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (Pessoa.IdPessoa == 0)
                {
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                }
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Pessoa.IdPessoa > 0)
                {
                    decimal PCom = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                    ComissaoPrd.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    ComissaoPrd.PComissao = PCom;
                    ComissaoPrd.GravarDados();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {

        }
        private void BtnExc_Click(object sender, EventArgs e)
        {

        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro de Pessoa em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Pessoa.IdPessoa > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.VerGrpLstVenda  = true;
                        BuscaPrd.ShowDialog();
                        if (BuscaPrd.IdProduto > 0)
                        {

                            Verificar ExistePrd   = new Verificar();
                            ExistePrd.Controle    = Controle;
                            ComissaoPrd.PComissao = 0;
                            ComissaoPrd.IdLanc    = 0;
                            ComissaoPrd.IdPessoa  = Pessoa.IdPessoa;
                            ComissaoPrd.IdProduto = BuscaPrd.IdProduto;
                            ComissaoPrd.GravarDados();
                            PopularGridItens();
                            GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];

                        }
                        else
                            Source_Itens.CancelEdit();
                        BuscaPrd.Dispose();
                    }
                }
            }
        }
        private void EnviarXML()
        {
            try
            {                
                DataSet Tab = Controle.ConsultaTabela("SELECT T1.*,ISNULL(T2.VENDEDOR,' ') AS NOMEVENDEDOR FROM PESSOAS T1 LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) WHERE T1.ID_PESSOA=" + Pessoa.IdPessoa.ToString());
                XmlDocument XMLCad = new XmlDocument();
                XMLCad.LoadXml(Tab.GetXml());
                //
                int IdCodServ = 0;
                Controles.Serv_CadPessoa.AtualizarCadastro EnviarCad = new Controles.Serv_CadPessoa.AtualizarCadastro();
                EnviarCad.Url = "http://" + FrmPrincipal.URLMatriz + "/WSCadPessoa/AtualizarCadastro.asmx?swdl";
                IdCodServ = EnviarCad.Atualizar(XMLCad, FrmPrincipal.Perfil_Usuario.IdUsuario, FrmPrincipal.Parametros_Filial.IdFilial);
                if (IdCodServ > 0)
                    Controle.ExecutaSQL("Update Pessoas Set IdServidor=" + IdCodServ.ToString() + " WHERE Id_Pessoa=" + Pessoa.IdPessoa.ToString());

                Tab = Controle.ConsultaTabela("SELECT T1.*,ISNULL(T2.VENDEDOR,' ') AS NOMEVENDEDOR FROM PESSOAS T1 LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) WHERE T1.ID_PESSOA=" + Pessoa.IdPessoa.ToString());
                XMLCad = new XmlDocument();
                XMLCad.LoadXml(Tab.GetXml());

                SqlDataReader LerSQL = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL WHERE ID_FILIAL not in ("+FrmPrincipal.IdFilialConexao.ToString()+")");
                while (LerSQL.Read())
                {
                    if (LerSQL["ServidorRemoto"].ToString().Trim() != "")
                    {
                        try
                        {
                            if (LerSQL["ID_FILIAL"].ToString().Trim() == "7")
                                EnviarCad.Url = "http://" + LerSQL["ServidorRemoto"].ToString().Trim() + "/WSCadPessoaLoja/AtualizarCadastro.asmx?swdl";
                            else
                            EnviarCad.Url = "http://" + LerSQL["ServidorRemoto"].ToString().Trim() + "/WSCadPessoa/AtualizarCadastro.asmx?swdl";
                            EnviarCad.AtualizarFilial(XMLCad, FrmPrincipal.Perfil_Usuario.IdUsuario, FrmPrincipal.Parametros_Filial.IdFilial);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {                
            }
        }

        private void LstTipoCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlComplemento.Enabled    = LstTipoCadastro.SelectedIndex != 4;
            Ck_NotificaAltPrc.Visible = LstTipoCadastro.SelectedIndex == 3 && !FrmPrincipal.VersaoDistribuidor;
        }

        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                BuscaPessoa.FrmPrincipal   = this.FrmPrincipal;
                BuscaPessoa.RedeGrp        = true;
                BuscaPessoa.Text           = "Pesquisa de Rede ou Grupo de Empresas";
                BuscaPessoa.ShowDialog();
                if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                {
                    SetaPessoa(BuscaPessoa.CadPessoa.IdPessoa);
                    Pessoa.IdVinculo = BuscaPessoa.CadPessoa.IdPessoa;
                }
                else
                {
                    Pessoa.IdVinculo   = 0;
                    TxtCodCliente.Text = "0";
                    TxtCliente.Text    = "";
                }
            }
        }
        private void SetaPessoa(int Id)
        {
            Pessoas LerCad  = new Pessoas();
            LerCad.Controle = Controle;
            LerCad.LerDados(Id);
            TxtCodCliente.Text = LerCad.IdPessoa.ToString();
            TxtCliente.Text    = LerCad.RazaoSocial.Trim();                        
        }

        //Produtos em Comodato
        private void PopularGridItensComod()
        {
            TabComodItens = Controle.ConsultaTabela("SELECT T1.ID_LANC,T2.REFERENCIA,T2.DESCRICAO,T1.QTDE FROM CLIENTESPRDCOMODATO T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_PESSOA=" + Pessoa.IdPessoa.ToString());
            Source_ComodItens.DataSource = TabComodItens;
            Source_ComodItens.DataMember = TabComodItens.Tables[0].TableName;
            GridItensComod.DataSource    = Source_ComodItens;
            NavegadorComod.BindingSource = Source_ComodItens;
            int item = Source_ComodItens.Find("ID_Lanc", PrdComodato.IdLanc);
            Source_ComodItens.Position = item;
        }
        private void GridItensComod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItensComod.CurrentRow == null || GridItensComod.Rows.Count - 1 == GridItensComod.CurrentRow.Index)
                {
                    IncluirItemComod();
                }
            }
        }
        private void GridItensComod_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_ComodItens.CancelEdit();
                e.Cancel = true;
            }

            if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
            {
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Source_ComodItens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (Pessoa.IdPessoa == 0)
                {
                    Source_ComodItens.CancelEdit();
                    e.Cancel = true;
                }
            }
        }
        private void GridItensComod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Pessoa.IdPessoa > 0)
                {
                    decimal Qtde = decimal.Parse(GridItensComod.CurrentRow.Cells[3].Value.ToString());
                    PrdComodato.LerDados(int.Parse(GridItensComod.CurrentRow.Cells[0].Value.ToString()));
                    PrdComodato.Qtde = Qtde;
                    PrdComodato.GravarDados();
                    PopularGridItensComod();
                    GridItensComod.CurrentCell = GridItensComod.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnIncComod_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do produto em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Pessoa.IdPessoa > 0)
                    IncluirItemComod();
            }
        }
        private void BtnExcComod_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Pessoa.IdPessoa > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            PrdComodato.IdLanc = int.Parse(GridItensComod.CurrentRow.Cells[0].Value.ToString());
                            PrdComodato.Excluir();
                            PrdComodato.IdLanc = 0;
                            PopularGridItensComod();
                        }
                    }
                }
            }
        }
        private void IncluirItemComod()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Pessoa.IdPessoa > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlterarPessoa == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal = this.FrmPrincipal;
                        BuscaPrd.IdProduto = 0;
                        BuscaPrd.ShowDialog();
                        if (BuscaPrd.IdProduto > 0)
                        {
                            Verificar ExistePrd   = new Verificar();
                            ExistePrd.Controle    = Controle;
                            PrdComodato.Qtde      = 1;
                            PrdComodato.IdLanc    = 0;
                            PrdComodato.IdPessoa  = Pessoa.IdPessoa;
                            PrdComodato.IdProduto = BuscaPrd.IdProduto;
                            PrdComodato.GravarDados();
                            PopularGridItensComod();
                            GridItensComod.CurrentCell = GridItensComod.CurrentRow.Cells[3];
                        }
                        else
                            Source_ComodItens.CancelEdit();
                        BuscaPrd.Dispose();
                    }
                }
            }
        }

        private void BtnImpComodato_Click(object sender, EventArgs e)
        {
            ClientesPrdComodato Itens = new ClientesPrdComodato();
            Itens.Controle = Controle;
            DataSet TabItens = new DataSet();
            TabItens = Controle.ConsultaTabela("select t2.Id_Pessoa,t1.id_produto,Isnull(SUM(convert(int,t1.qtde)),0) as Qtde from mvvendaitens t1" +
                                               " left join mvvenda t2 on (t2.id_venda=t1.id_Venda)" +
                                               " where t2.tpvenda='CO' and t2.status=3 " +
                                               "   group by t2.Id_Pessoa,t1.id_produto");
            
            for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
            {
                BtnImpComodato.Text = TabItens.Tables[0].Rows.Count.ToString() + " / " + I.ToString();
                Application.DoEvents();
                Itens.LerDados(0);
                Itens.IdPessoa  = int.Parse(TabItens.Tables[0].Rows[I]["Id_Pessoa"].ToString());
                Itens.IdProduto = int.Parse(TabItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                Itens.Qtde      = int.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString());
                Itens.GravarDados();
            }
            MessageBox.Show("Concluido");

        }

      
    }
}