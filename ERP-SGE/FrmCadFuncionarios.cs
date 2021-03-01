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
using System.Drawing.Imaging;
using System.IO;
using System.Data.SqlClient;
using System.Data.Sql;

namespace ERP_SGE
{
    public partial class FrmCadFuncionarios : Form
    {
        Funcoes Controle         = new Funcoes();
        Funcionarios Func        = new Funcionarios();
        Auditoria RegAuditoria   = new Auditoria();
        ProvDescFunc Eventos     = new ProvDescFunc();
        ProvDescFunc EventosFixo = new ProvDescFunc();
        Pessoas CadPessoa        = new Pessoas();

        private DataSet TabEventos;
        private BindingSource Source_Eventos;

        private DataSet TabEventosFixo;
        private BindingSource Source_EventosFixo;

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadFuncionarios()
        {
            InitializeComponent();
        }

        private void FrmCadFuncionarios_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;
            Func.Controle         = Controle;
            Func.IdFunc           = 0;
            Eventos.Controle      = Controle;
            EventosFixo.Controle  = Controle;
            RegAuditoria.Controle = Controle;
            CadPessoa.Controle    = Controle;
            TabEventos            = new DataSet();            
            Source_Eventos        = new BindingSource();
            TabEventosFixo        = new DataSet();
            Source_EventosFixo    = new BindingSource();

            ColEvento       = FrmPrincipal.PopularComboGrid("SELECT Id_Codigo,CASE PROVDESC WHEN 0 THEN '(P) - '+SubString(Descricao,1,40) ELSE '(D) - '+SubString(Descricao,1,40) END Descricao FROM ProventosDescontos ORDER BY Descricao", ColEvento);
            ColEventoFixo   = FrmPrincipal.PopularComboGrid("SELECT Id_Codigo,CASE PROVDESC WHEN 0 THEN '(P) - '+SubString(Descricao,1,40) ELSE '(D) - '+SubString(Descricao,1,40) END Descricao FROM ProventosDescontos ORDER BY Descricao", ColEventoFixo);
            LstProvDescParc = FrmPrincipal.PopularCombo("SELECT Id_Codigo,CASE PROVDESC WHEN 0 THEN '(P) - '+SubString(Descricao,1,40) ELSE '(D) - '+SubString(Descricao,1,40) END Descricao FROM ProventosDescontos ORDER BY Descricao", LstProvDescParc);
            LstVendedor     = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstVendedor);
            LstFilialPesq   = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilialPesq,"Todas");
            LstFilialPesq.SelectedValue = 0;
            LstPesquisa.SelectedIndex   = 0;
            LstMesEventos.SelectedIndex = DateTime.Now.Month;
            TxtAnoEventos.Value         = DateTime.Now.Year;
            LstMesEventos.SelectedIndex = DateTime.Now.Month;
            LstMesParc.SelectedIndex    = DateTime.Now.Month;
            AnoParc.Value               = DateTime.Now.Year;
            NumParc.Value               = 1;            
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            string sSQL = "SELECT T1.ID_FUNC,T1.MATRICULA,T1.CPF,T1.NOME,TELEFONE,T1.CELULAR,T2.DEPARTAMENTO,T1.DTDEMISSAO,T3.FANTASIA FROM FUNCIONARIOS T1 LEFT JOIN Departamentos T2 ON (T2.Id_Departamento=T1.Id_Departamento) LEFT JOIN Empresa_Filial T3 ON (T3.Id_Filial=T1.Id_FilialTrab) ";

            string WhereFilial= " WHERE T1.ID_FUNC > 0";
            if (int.Parse(LstFilialPesq.SelectedValue.ToString()) > 0)
                WhereFilial = WhereFilial+ " AND T1.ID_FILIALTRAB=" + LstFilialPesq.SelectedValue.ToString();
            
            if (Cb_Ativos.Checked)
                WhereFilial = WhereFilial + " AND T1.DtDemissao is null";

            if (TxtPesquisa.Text.Trim() != "")
            {
                try
                {
                    if (LstPesquisa.SelectedIndex == 0)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + WhereFilial + " AND  T1.NOME LIKE '%{0}%' order by T1.NOME", TxtPesquisa.Text.Trim()));
                    else if (LstPesquisa.SelectedIndex == 1)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + WhereFilial + " AND  T1.MATRICULA LIKE '%{0}%' " + WhereFilial+" order by T1.MATRICULA", TxtPesquisa.Text.Trim()));
                    else if (LstPesquisa.SelectedIndex == 2)
                        Tabela = Controle.ConsultaTabela(string.Format(sSQL + WhereFilial + " AND T1.CPF LIKE '%{0}%' " + WhereFilial+" order by T1.CPF", TxtPesquisa.Text.Trim()));                    
                }
                catch
                {
                    MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                Tabela = Controle.ConsultaTabela(sSQL+WhereFilial+" order by t1.nome");

            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Func", Func.IdFunc);
            Source.Position = item;
        }

        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            Func.LerDados(Isn);                        
            TxtCodigo.Text                = Func.IdFunc.ToString();
            TxtCpf.Text                   = Func.Cpf;
            TxtNome.Text                  = Func.Nome;
            TxtMatricula.Value            = Func.Matricula;            
            TxtCep.Text                   = Func.Cep;
            TxtEndereco.Text              = Func.Endereco;
            TxtNumero.Text                = Func.Numero;
            TxtComplemento.Text           = Func.Complemento;
            TxtBairro.Text                = Func.Bairro;
            TxtCidade.Text                = Func.Cidade;
            LstUF.SelectedValue           = Func.IdUf.ToString();
            TxtFone.Text                  = Func.Telefone;            
            TxtEmail.Text                 = Func.Email;            
            TxtCelular.Text               = Func.Celular;
            TxtRg.Text                    = Func.Rg;
            TxtDtEmissao.Value            = Func.DtEmissao;
            TxtPai.Text                   = Func.NomePai;
            TxtMae.Text                   = Func.NomeMae;
            TxtCtps.Text                  = Func.Ctps;
            TxtSerie.Text                 = Func.Serie;
            TxtPis.Text                   = Func.PIS;
            TxtCnh.Text                   = Func.CNH;
            TxtDtNascim.Value             = Func.DtNasc;
            LstEscolaridade.SelectedIndex = Func.Escolaridade;
            LstEstadoCivil.SelectedIndex  = Func.EstadoCivil;
            TxtTitulo.Text                = Func.TituloEleitoral;
            LstTipoConta.SelectedIndex    = Func.TipoConta;
            TxtBanco.Text                 = Func.Banco;
            TxtAgencia.Text               = Func.Agencia;
            TxtConta.Text                 = Func.Conta;
            TxtRefPessoal.Text            = Func.RefPessoal;
            TxtRefTelefone.Text           = Func.RefTelefone;
            TxtParentesco.Text            = Func.Parentesco;
            LstFilialTrab.SelectedValue   = Func.IdFilialTrab.ToString();
            LstFilialReg.SelectedValue    = Func.IdFilialReg.ToString();
            LstDepartamento.SelectedValue = Func.IdDepartamento.ToString();
            TxtSalarioCtps.Value          = Func.SalarioCtps;
            TxtSalarioAtual.Value         = Func.SalarioAtual;
            TxtAdiantSalario.Value        = Func.AdiantSalario;
            TxtDependentes.Value          = Func.Dependentes;
            TxtFuncao.Text                = Func.Funcao;
            TxtCBO.Text                   = Func.CBO;
            TxtDtAdmissao.Value           = Func.DtAdmissao;
            TxtObsAdvertencia.Text        = Func.ObsAdvertencia;
            TxtObsAltSalario.Text         = Func.ObsAltSalario;
            TxtObsOutras.Text             = Func.ObsOutras;
            TxtCurso.Text                 = Func.Curso;
            TxtCelular2.Text              = Func.Celular2;
            LstVendedor.SelectedValue     = Func.IdVendedor;            
            SetaPessoa(Func.IdPessoa);
            LerFoto();
            
                        
            if (Func.PlanoSaude == 1)  CK_PlanoSaude.Checked = true; else CK_PlanoSaude.Checked = false;
            if (Func.ContratoExp == 1) CK_Contrato.Checked   = true; else CK_Contrato.Checked   = false;
            if (Func.Demissao == 1)    Ck_Demissao.Checked   = true; else Ck_Demissao.Checked   = false;
            if (Func.SalBaseHR == 0)   Rb_BaseHrCTPS.Checked = true; else Rb_BaseHrAtual.Checked = true;

            if (Ck_Demissao.Checked)
            {
                TxtDtDemissao.Value = Func.DtDemissao;
                TxtMotivoDemissao.Text = Func.MotivoDemissao;
            }
            ValidadeDemissao();
            PopularGridEventos();
            PopularGridEventosFixo();
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
                FrmPrincipal.ControleBotoes(true);
                TxtCpf.Focus();
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
                PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);
                TxtCpf.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtCpf.Text.Trim() != "" && int.Parse(TxtMatricula.Value.ToString()) > 0)
            {
                Func.IdFunc           = int.Parse(TxtCodigo.Text);
                Func.Cpf              = TxtCpf.Text;
                Func.Nome             = TxtNome.Text;
                Func.Matricula        = int.Parse(TxtMatricula.Value.ToString());
                Func.Cep              = TxtCep.Text.Replace("-", "");
                Func.Endereco         = TxtEndereco.Text;
                Func.Numero           = TxtNumero.Text;
                Func.Complemento      = TxtComplemento.Text;
                Func.Bairro           = TxtBairro.Text;
                Func.Cidade           = TxtCidade.Text;
                Func.IdUf             = int.Parse(LstUF.SelectedValue.ToString());
                Func.Telefone         = TxtFone.Text;
                Func.Email            = TxtEmail.Text;
                Func.Celular          = TxtCelular.Text;
                Func.Rg               = TxtRg.Text;
                Func.DtEmissao        = TxtDtEmissao.Value;
                Func.NomePai          = TxtPai.Text;
                Func.NomeMae          = TxtMae.Text;
                Func.Ctps             = TxtCtps.Text;
                Func.Serie            = TxtSerie.Text;
                Func.PIS              = TxtPis.Text;
                Func.CNH              = TxtCnh.Text;
                Func.DtNasc           = TxtDtNascim.Value;
                Func.Escolaridade     = int.Parse(LstEscolaridade.SelectedIndex.ToString());
                Func.EstadoCivil      = int.Parse(LstEstadoCivil.SelectedIndex.ToString());
                Func.TituloEleitoral  = TxtTitulo.Text;
                Func.TipoConta        = int.Parse(LstTipoConta.SelectedIndex.ToString());
                Func.Banco            = TxtBanco.Text;
                Func.Agencia          = TxtAgencia.Text;
                Func.Conta            = TxtConta.Text;
                Func.RefPessoal       = TxtRefPessoal.Text;
                Func.RefTelefone      = TxtRefTelefone.Text;
                Func.Parentesco       = TxtParentesco.Text;
                Func.IdFilialTrab     = int.Parse(LstFilialTrab.SelectedValue.ToString());
                Func.IdFilialReg      = int.Parse(LstFilialReg.SelectedValue.ToString());
                Func.IdDepartamento   = int.Parse(LstDepartamento.SelectedValue.ToString());
                Func.SalarioCtps      = TxtSalarioCtps.Value;
                Func.SalarioAtual     = TxtSalarioAtual.Value;
                Func.AdiantSalario    = TxtAdiantSalario.Value;
                Func.Dependentes      = int.Parse(TxtDependentes.Value.ToString());
                Func.Funcao           = TxtFuncao.Text;
                Func.CBO              = TxtCBO.Text;
                Func.DtAdmissao       = TxtDtAdmissao.Value;
                Func.ObsAdvertencia   = TxtObsAdvertencia.Text;
                Func.ObsAltSalario    = TxtObsAltSalario.Text;
                Func.ObsOutras        = TxtObsOutras.Text;
                Func.Curso            = TxtCurso.Text;
                Func.Celular2          = TxtCelular2.Text;
                Func.IdVendedor       = int.Parse(LstVendedor.SelectedValue.ToString());
                Func.IdPessoa         = int.Parse(TxtCodCliente.Text);
                if (CK_PlanoSaude.Checked) Func.PlanoSaude = 1; else Func.PlanoSaude = 0;
                if (CK_Contrato.Checked)  Func.ContratoExp = 1; else Func.ContratoExp = 0;
                if (Ck_Demissao.Checked) Func.Demissao = 1; else Func.Demissao= 0;
                if (Rb_BaseHrCTPS.Checked) Func.SalBaseHR = 0; else Func.SalBaseHR = 1;
                if (Func.Demissao == 1)
                {
                    Func.DtDemissao = TxtDtDemissao.Value;
                    Func.MotivoDemissao = TxtMotivoDemissao.Text;
                }
                Func.GravarDados();
                if (TxtCodigo.Text == "0")
                    FrmPrincipal.RegistrarAuditoria(this.Text, Func.IdFunc, Func.Cpf, 1, "Inclusão de Funcionario");
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Func.IdFunc, Func.Cpf, 2, "Alteração do Cad.Funcionario");
                PopularGrid();
                PopularCampos(Func.IdFunc);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("CPF ou Matricula não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCpf.Focus();
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
                        Func.IdFunc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        Func.Excluir();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, Func.IdFunc, Func.Cpf, 3, "Exclusão do Lançamento");
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
            FrmPrincipal.ClickBtnNovo += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
            FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
            //
            LstDepartamento = FrmPrincipal.PopularCombo("SELECT Id_Departamento,Departamento FROM Departamentos ORDER BY Departamento", LstDepartamento);
            LstFilialTrab   = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilialTrab);
            LstFilialReg    = FrmPrincipal.PopularCombo("SELECT Id_Filial,SubString(FANTASIA,1,80) as Filial FROM Empresa_Filial ORDER BY FANTASIA", LstFilialReg);
            LstUF           = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUF);            
        }
        private void LimpaDados()
        {
            TxtCodigo.Text                = "0";
            TxtCpf.Text                   = "";
            TxtNome.Text                  = "";
            TxtMatricula.Value            = 0;
            TxtCep.Text                   = "";
            TxtEndereco.Text              = "";
            TxtNumero.Text                = "";
            TxtComplemento.Text           = "";
            TxtBairro.Text                = "";
            TxtCidade.Text                = "";
            LstUF.SelectedValue           = 7;
            TxtFone.Text                  = "";
            TxtEmail.Text                 = "";
            TxtCelular.Text               = "";
            TxtRg.Text                    = "";
            TxtDtEmissao.Value            = DateTime.Now;
            TxtPai.Text                   = "";
            TxtMae.Text                   = "";
            TxtCtps.Text                  = "";
            TxtSerie.Text                 = "";
            TxtPis.Text                   = "";
            TxtCnh.Text                   = "";
            TxtDtNascim.Value             = DateTime.Now;
            LstEscolaridade.SelectedIndex = 0;
            LstEstadoCivil.SelectedIndex  = 0;
            TxtTitulo.Text                = "";
            LstTipoConta.SelectedIndex    = 0;
            TxtBanco.Text                 = "";
            TxtAgencia.Text               = "";
            TxtConta.Text                 = "";
            TxtRefPessoal.Text            = "";
            TxtRefTelefone.Text           = "";
            TxtParentesco.Text            = "";
            LstFilialTrab.SelectedValue   = 0;
            LstFilialReg.SelectedValue    = 0;
            LstDepartamento.SelectedValue = 0;
            LstVendedor.SelectedValue     = 0;
            TxtSalarioCtps.Value          = 0;
            TxtSalarioAtual.Value         = 0;
            TxtAdiantSalario.Value        = 0;
            TxtDependentes.Value          = 0;
            TxtFuncao.Text                = "";
            TxtCBO.Text                   = "";
            TxtDtAdmissao.Value           = DateTime.Now;
            TxtObsAdvertencia.Text        = "";
            TxtObsAltSalario.Text         = "";
            TxtObsOutras.Text             = "";
            TxtCurso.Text                 = "";
            TxtCelular2.Text              = "";
            CK_PlanoSaude.Checked         = false;
            CK_Contrato.Checked           = false;
            Rb_BaseHrCTPS.Checked         = true;
            PicFoto.Image                 = null;
            SetaPessoa(0);
            Func.LerDados(0);
            PopularGridEventos();
            PopularGridEventosFixo();
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
        private bool VerificaCNPJCPF()
        {
            if (TxtCpf.Text != "" && StaFormEdicao)
            {
                Verificar ExiteCpf = new Verificar();
                ExiteCpf.Controle = Controle;

                if (!Controle.ValidarCpf(TxtCpf.Text))
                {
                    MessageBox.Show("CPF inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtCpf.Focus();
                    return false;
                }
                /*else
                {
                    int CodPesq = ExiteCpf.Verificar_ExisteCadastro("Id_Func", "SELECT * FROM FUNCIONARIOS WHERE CPF='" + TxtCpf.Text.Trim() + "'");
                    if (CodPesq > 0 && CodPesq != int.Parse(TxtCodigo.Text))
                    {
                        MessageBox.Show("CPF já cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        PopularCampos(CodPesq);
                        StaFormEdicao = false;
                        FrmPrincipal.ControleBotoes(false);
                        TxtCpf.Focus();
                        return false;
                    }
                }*/

            }
            return true;
        }

        private void TxtCep_Validated(object sender, EventArgs e)
        {
            if (TxtCep.Text.Replace("-", "").Trim() != "")
            {
                if (TxtCep.Text.Replace("-", "").Trim() != Func.Cep.Trim())
                {
                    Verificar VerificaUF = new Verificar();
                    VerificaUF.Controle = Controle;
                    ConsultaCEP ConsultaCEP = new ConsultaCEP();
                    ConsultaCEP.VerificaCEP(TxtCep.Text);
                    TxtEndereco.Text = ConsultaCEP.Tipo.ToUpper() + " " + ConsultaCEP.Endereco.ToUpper();
                    TxtBairro.Text = ConsultaCEP.Bairro.ToUpper();
                    TxtCidade.Text = ConsultaCEP.Cidade.ToUpper();
                    LstUF.SelectedValue = VerificaUF.Busca_IdUF(ConsultaCEP.UF);
                }
            }
        }

        private void TxtCpf_Validated(object sender, EventArgs e)
        {
            VerificaCNPJCPF();
        }

        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();
            if (e.ColumnIndex == 2)
            {
                MaskCol.Mask = "000,000,000-00";                
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            else if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                MaskCol.Mask = "(00) 0000-0000";
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            MaskCol.Dispose();
        }

        //Eventos Proventos e Descontos Mensal       
        private void PopularGridEventos()
        {
            if (LstMesEventos.SelectedIndex == 0)
                TabEventos = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.ID_PROVDESC,T1.MESANO,T1.VALOR,T1.DESCRICAO FROM PROVDESCFUNC T1  WHERE MESANO<>'00/0000' AND T1.ID_FUNC=" + Func.IdFunc.ToString());
            else
                TabEventos = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.ID_PROVDESC,T1.MESANO,T1.VALOR,T1.DESCRICAO FROM PROVDESCFUNC T1  WHERE MESANO='" + string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString() + "' AND T1.ID_FUNC=" + Func.IdFunc.ToString());
            Source_Eventos.DataSource = TabEventos;
            Source_Eventos.DataMember = TabEventos.Tables[0].TableName;
            GridEventos.DataSource = Source_Eventos;
            NavEvento.BindingSource = Source_Eventos;
            int item = Source_Eventos.Find("Id_Lanc", Eventos.IdLanc);
            Source_Eventos.Position = item;
        }
        private void GridEventos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridEventos.CurrentRow == null || GridEventos.Rows.Count - 1 == GridEventos.CurrentRow.Index)
                {
                    IncluirItem();
                }
            }
        }
        private void GridEventos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Eventos.CancelEdit();
                e.Cancel = true;
            }

            if (Func.IdFunc == 0)
            {
                Source_Eventos.CancelEdit();
                e.Cancel = true;
            }

        }
        private void GridEventos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Func.IdFunc > 0)
                {
                    decimal Valor = decimal.Parse(GridEventos.CurrentRow.Cells[3].Value.ToString());
                    Eventos.LerDados(int.Parse(GridEventos.CurrentRow.Cells[0].Value.ToString()));
                    Eventos.Valor = Valor;
                    Eventos.IdProvDesc = int.Parse(GridEventos.CurrentRow.Cells[1].Value.ToString());
                    Eventos.Descricao = GridEventos.CurrentRow.Cells[4].Value.ToString();
                    Eventos.GravarDados();
                    PopularGridEventos();
                    GridEventos.CurrentCell = GridEventos.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Func.IdFunc > 0)
                    IncluirItem();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Func.IdFunc > 0)
                {
                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Eventos.IdLanc = int.Parse(GridEventos.CurrentRow.Cells[0].Value.ToString());
                        Eventos.Excluir();
                        Eventos.IdLanc = 0;
                        PopularGridEventos();
                    }

                }
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Func.IdFunc > 0)
                {
                    Eventos.IdFunc = Func.IdFunc;
                    Eventos.IdLanc = 0;
                    Eventos.IdProvDesc = 0;
                    Eventos.Valor = 0;
                    Eventos.MesAno = string.Format("{0:D2}", LstMesEventos.SelectedIndex) + @"/" + TxtAnoEventos.Value.ToString();
                    Eventos.Descricao = "";
                    Eventos.GravarDados();
                    PopularGridEventos();
                    GridEventos.CurrentCell = GridEventos.CurrentRow.Cells[1];
                }
            }
        }

        //Eventos Proventos e Descontos Fixo     
        private void PopularGridEventosFixo()
        {
            TabEventosFixo = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.ID_PROVDESC,T1.VALOR,T1.DESCRICAO FROM PROVDESCFUNC T1  WHERE T1.MESANO='00/0000' AND T1.ID_FUNC=" + Func.IdFunc.ToString());
            Source_EventosFixo.DataSource = TabEventosFixo;
            Source_EventosFixo.DataMember = TabEventosFixo.Tables[0].TableName;
            GridEventoFixo.DataSource     = Source_EventosFixo;
            NavEventoFixo.BindingSource   = Source_EventosFixo;
            int item = Source_EventosFixo.Find("Id_Lanc", EventosFixo.IdLanc);
            Source_EventosFixo.Position = item;
        }
        private void GridEventoFixo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridEventoFixo.CurrentRow == null || GridEventoFixo.Rows.Count - 1 == GridEventoFixo.CurrentRow.Index)
                {
                    IncluirItemEvFixo();
                }
            }
        }
        private void GridEventoFixo_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_EventosFixo.CancelEdit();
                e.Cancel = true;
            }

            if (Func.IdFunc == 0)
            {
                Source_EventosFixo.CancelEdit();
                e.Cancel = true;
            }

        }
        private void GridEventoFixo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Func.IdFunc > 0)
                {
                    decimal Valor = decimal.Parse(GridEventoFixo.CurrentRow.Cells[2].Value.ToString());
                    EventosFixo.LerDados(int.Parse(GridEventoFixo.CurrentRow.Cells[0].Value.ToString()));
                    EventosFixo.Valor      = Valor;
                    EventosFixo.IdProvDesc = int.Parse(GridEventoFixo.CurrentRow.Cells[1].Value.ToString());
                    EventosFixo.Descricao  = GridEventoFixo.CurrentRow.Cells[3].Value.ToString();
                    EventosFixo.GravarDados();
                    PopularGridEventosFixo();
                    GridEventoFixo.CurrentCell = GridEventoFixo.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnIncFixo_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Func.IdFunc > 0)
                    IncluirItemEvFixo();
            }
        }
        private void BtnExcFixo_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Func.IdFunc > 0)
                {
                    if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        EventosFixo.IdLanc = int.Parse(GridEventoFixo.CurrentRow.Cells[0].Value.ToString());
                        EventosFixo.Excluir();
                        EventosFixo.IdLanc = 0;
                        PopularGridEventosFixo();
                    }

                }
            }
        }
        private void IncluirItemEvFixo()
        {
            if (StaFormEdicao)
                MessageBox.Show("Cadastro do Funcionario em edição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Func.IdFunc > 0)
                {
                    EventosFixo.IdFunc = Func.IdFunc;
                    EventosFixo.IdLanc = 0;
                    EventosFixo.IdProvDesc = 0;
                    EventosFixo.Valor = 0;
                    EventosFixo.MesAno = "00/0000";
                    EventosFixo.Descricao = "";
                    EventosFixo.GravarDados();
                    PopularGridEventosFixo();
                    GridEventoFixo.CurrentCell = GridEventoFixo.CurrentRow.Cells[1];
                }
            }
        }
        //--------------------------------------------------------//
        private void Ck_Demissao_Click(object sender, EventArgs e)
        {
            ValidadeDemissao();
        }

        private void ValidadeDemissao()
        {
            PnlDemissao.Visible = Ck_Demissao.Checked;

            if (PnlDemissao.Visible)
            {
                TxtDtDemissao.Value = Func.DtDemissao;
                TxtMotivoDemissao.Text = Func.MotivoDemissao;
            }
        }

        private void BtnGeraParc_Click(object sender, EventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (Func.IdFunc > 0)
                {
                    if (int.Parse(LstProvDescParc.SelectedValue.ToString())==0)
                    {
                        MessageBox.Show("Favor Informar um Proventos ou Desconto", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LstProvDescParc.Focus();
                        return;
                    }
                    if (ValorParc.Value <= 0)
                    {
                        MessageBox.Show("Favor Informar o Valor do Lançamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ValorParc.Focus();
                        return;
                    }

                    if (MessageBox.Show("Confirma o Lançamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int Mes = LstMesParc.SelectedIndex;
                        int Ano = int.Parse(AnoParc.Value.ToString());

                        for (int I = 1; I <= int.Parse(NumParc.Value.ToString()); I++)
                        {
                            Eventos.IdFunc     = Func.IdFunc;
                            Eventos.IdLanc     = 0;
                            Eventos.IdProvDesc = int.Parse(LstProvDescParc.SelectedValue.ToString());
                            Eventos.Valor      = ValorParc.Value;
                            Eventos.MesAno     = string.Format("{0:D2}", Mes) + @"/" + Ano; ;
                            Eventos.Descricao = "Parc " + string.Format("{0:D2}", I) + "/" + string.Format("{0:D2}", int.Parse(NumParc.Value.ToString()));
                            Eventos.GravarDados();
                            //
                            if (Mes == 12)
                            {
                                Ano = Ano + 1;
                                Mes = 1;
                            }
                            else
                                Mes = Mes + 1;
                        }
                        PopularGridEventos();                        
                    }
                }
            }
        }

        private void LstMesEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularGridEventos();
        }

        private void TxtAnoEventos_ValueChanged(object sender, EventArgs e)
        {
            PopularGridEventos();
        }

        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            BuscaPessoa.ShowDialog();
            if (BuscaPessoa.CadPessoa.IdPessoa > 0)
            {
                TxtCodCliente.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                TxtCliente.Text    = BuscaPessoa.CadPessoa.RazaoSocial;
            }
            else
            {
                TxtCodCliente.Text = "0";
                TxtCliente.Text    = " ";
            }
        }

        private void SetaPessoa(int IdPessoa)
        {
            CadPessoa.LerDados(IdPessoa);
            Func.IdPessoa      = CadPessoa.IdPessoa;
            TxtCodCliente.Text = CadPessoa.IdPessoa.ToString();
            TxtCliente.Text    = CadPessoa.RazaoSocial.Trim();
        }

        private void BtnFoto_Click(object sender, EventArgs e)
        {
            if (Func.IdFunc > 0)
            {
                BuscaFoto.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
                DialogResult res = BuscaFoto.ShowDialog();
                if (res == DialogResult.OK)
                {
                    PicFoto.Image = Image.FromFile(BuscaFoto.FileName);
                    // Salvar Imagem
                    MemoryStream ms = new MemoryStream();
                    PicFoto.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] photo_aray = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);
                    SqlCommand CmdSql = new SqlCommand("Update Funcionarios set Foto=@Foto WHERE id_func=" + Func.IdFunc.ToString(), Controle.Conexao);
                    CmdSql.Parameters.AddWithValue("@Foto", photo_aray);
                    CmdSql.ExecuteNonQuery();
                }
            }
        }
        private void LerFoto()
        {
            if (Func.IdFunc > 0)
            {
                PicFoto.Image = null;
                if (Func.Foto != "")
                {
                    SqlDataAdapter Tab = new SqlDataAdapter(new SqlCommand("SELECT FOTO  FROM FUNCIONARIOS WHERE ID_FUNC=" + Func.IdFunc.ToString(), Controle.Conexao));
                    DataSet DBFoto = new DataSet();
                    Tab.Fill(DBFoto);

                    byte[] photo_aray = (byte[])DBFoto.Tables[0].Rows[0]["FOTO"];                    
                    MemoryStream ms = new MemoryStream(photo_aray);
                    PicFoto.Image = Image.FromStream(ms);
                }
            }
        }
    }
}
