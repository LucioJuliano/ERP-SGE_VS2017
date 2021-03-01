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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Imaging;
using System.IO;
using System.Data.SqlClient;


namespace ERP_SGE
{
    public partial class FrmCadEmpresaFilial : Form
    {
        Funcoes Controle = new Funcoes();
        Filiais CadFiliais = new Filiais();
        Parametros CadParam = new Parametros();
        Pessoas CadPessoa = new Pessoas();
        Auditoria RegAuditoria = new Auditoria();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        public FrmCadEmpresaFilial()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;
            CadFiliais.Controle   = Controle;
            CadParam.Controle     = Controle;
            CadPessoa.Controle    = Controle;
            RegAuditoria.Controle = Controle;
            CadFiliais.IdFilial   = 0;            
            ChkNome.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Filial,Filial,Fantasia,Cnpj,Fone1,Fax FROM Empresa_Filial ORDER BY Filial");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Filial", CadFiliais.IdFilial);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            CadFiliais.LerDados(Isn);
            TxtCodigo.Text          = CadFiliais.IdFilial.ToString();
            TxtFilial.Text          = CadFiliais.Filial;
            TxtFantasia.Text        = CadFiliais.Fantasia;
            TxtCnpj.Text            = CadFiliais.Cnpj;
            TxtInscUF.Text          = CadFiliais.InscUF;
            TxtCep.Text             = CadFiliais.Cep;
            TxtEndereco.Text        = CadFiliais.Endereco;
            TxtNumero.Text          = CadFiliais.Numero;
            TxtComplemento.Text     = CadFiliais.Complemento;
            TxtBairro.Text          = CadFiliais.Bairro;
            TxtCidade.Text          = CadFiliais.Cidade;
            LstUF.SelectedValue     = CadFiliais.Uf.ToString();
            TxtFone1.Text           = CadFiliais.Fone1;
            TxtFone2.Text           = CadFiliais.Fone2;
            TxtFax.Text             = CadFiliais.Fax;
            TxtEmail.Text           = CadFiliais.Email;            
            LstRegime.SelectedIndex = CadFiliais.Regime;            
            TxtCodMun.Value         = CadFiliais.CodMun;
            // Lendo os parametros
            CadParam.LerDados(CadFiliais.IdFilial);            
            Cb_SldEstoque.Checked       = CadParam.EstoqueZero   == 1;
            Cb_ClienteAtraso.Checked    = CadParam.ClienteAtraso == 1;
            Cb_LimiteCredito.Checked    = CadParam.LimiteCredito == 1;            
            Cb_NotaIPI.Checked          = CadParam.NotaIPI == 1;
            TxtNumNota.Value            = CadParam.NotaFiscal;
            TxtNumForm.Value            = CadParam.Formulario;
            TxtNotaNFE.Value            = CadParam.NotaNFE;
            TxtFormularioNFE.Value      = CadParam.FormularioNFE;
            TxtLinhasNota.Value         = CadParam.LinhasNota;            
            cb_ClienteAtrasoWS.Checked  = CadParam.WSClienteAtraso == 1 && !FrmPrincipal.VersaoDistribuidor;            
            cb_WSCadPessoa.Checked      = CadParam.WSCadPessoa     == 1 && !FrmPrincipal.VersaoDistribuidor;
            Cb_NFE.Checked              = CadParam.NFE == 1;
            Cb_VerCancBxFin.Checked     = CadParam.VerCancBxFin == 1;
            Cb_WSNumNFE.Checked         = CadParam.WSNumNFE == 1;
            TxtPercPIS.Value            = CadParam.PercPIS;
            TxtPercCofins.Value         = CadParam.PercCOFINS;
            LstAmbiente.SelectedIndex   = CadParam.NFEAmbiente;
            LstVersao.SelectedIndex     = CadParam.NFEVersao;
            TxtCertificado.Text         = CadParam.Certificado;
            TxtSmtp.Text                = CadParam.Smtp;
            TxtPorta.Value              = CadParam.Porta;
            TxtEmailSmtp.Text           = CadParam.Email;
            TxtSenhaSmtp.Text           = CadParam.Senha;
            txtClieInativo.Value        = CadParam.CliDiasInativo;
            TxtInssFaixa1.Value         = CadParam.InssFaixa1;
            TxtInssFaixa2.Value         = CadParam.InssFaixa2;
            TxtInssFaixa3.Value         = CadParam.InssFaixa3;
            TxtInssPerc1.Value          = CadParam.InssPerc1;
            TxtInssPerc2.Value          = CadParam.InssPerc2;
            TxtInssPerc3.Value          = CadParam.InssPerc3;
            TxtObsNF.Text               = CadParam.ObsNF;
            TxtCodigoMFe.Text           = CadParam.CodigoMFe;
            TxtChaveMfe.Text            = CadParam.ChaveMFe;
            TxtChaveRequisicao.Text     = CadParam.ChaveRequisicao;
            TxtSerialPOS.Text           = CadParam.SerialPOS;
            TxtChaveValidador.Text      = CadParam.ChaveValidador;
            LstEntregador.SelectedValue = CadParam.IdEntregador.ToString();
            LstEmissorCF.SelectedIndex  = CadParam.EmissorCF;            
            SetaPessoa(CadParam.IdConsumidor);
            LerFoto();

        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtCnpj.Focus();
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
                TxtCnpj.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtCnpj.Text.Trim() != "")
            {
                CadFiliais.IdFilial    = int.Parse(TxtCodigo.Text);
                CadFiliais.Filial      = TxtFilial.Text;
                CadFiliais.Fantasia    = TxtFantasia.Text;
                CadFiliais.Cnpj        = TxtCnpj.Text;
                CadFiliais.InscUF      = TxtInscUF.Text;
                CadFiliais.Cep         = TxtCep.Text.Replace("-", "");
                CadFiliais.Endereco    = TxtEndereco.Text;
                CadFiliais.Numero      = TxtNumero.Text;
                CadFiliais.Complemento = TxtComplemento.Text;
                CadFiliais.Bairro      = TxtBairro.Text;
                CadFiliais.Cidade      = TxtCidade.Text;
                CadFiliais.Uf          = int.Parse(LstUF.SelectedValue.ToString());
                CadFiliais.Fone1       = TxtFone1.Text;
                CadFiliais.Fone2       = TxtFone2.Text;
                CadFiliais.Fax         = TxtFax.Text;
                CadFiliais.Email       = TxtEmail.Text;                
                CadFiliais.Regime      = LstRegime.SelectedIndex;
                CadFiliais.CodMun      = int.Parse(TxtCodMun.Value.ToString());
                CadFiliais.GravarDados();
                //
                              
                //Registrando Auditoria
                if (!Cb_SldEstoque.Checked && CadParam.EstoqueZero == 1)
                    FrmPrincipal.RegistrarAuditoria(this.Text, CadFiliais.IdFilial, CadFiliais.Cnpj, 2, "Alteração Controle de Estoque");

                if (Cb_SldEstoque.Checked) CadParam.EstoqueZero = 1; else CadParam.EstoqueZero = 0;
                if (Cb_ClienteAtraso.Checked) CadParam.ClienteAtraso = 1; else CadParam.ClienteAtraso = 0;
                if (cb_ClienteAtrasoWS.Checked && Cb_ClienteAtraso.Checked && !FrmPrincipal.VersaoDistribuidor) CadParam.WSClienteAtraso = 1; else CadParam.WSClienteAtraso = 0;
                if (cb_WSCadPessoa.Checked && !FrmPrincipal.VersaoDistribuidor) CadParam.WSCadPessoa = 1; else CadParam.WSCadPessoa = 0;
                if (Cb_LimiteCredito.Checked) CadParam.LimiteCredito = 1; else CadParam.LimiteCredito = 0;
                if (Cb_NFE.Checked) CadParam.NFE = 1; else CadParam.NFE = 0;
                if (Cb_WSNumNFE.Checked) CadParam.WSNumNFE = 1; else CadParam.WSNumNFE = 0;
                if (Cb_NotaIPI.Checked) CadParam.NotaIPI = 1; else CadParam.NotaIPI = 0;
                if (Cb_VerCancBxFin.Checked) CadParam.VerCancBxFin = 1; else CadParam.VerCancBxFin = 0;
                CadParam.NotaFiscal     = int.Parse(TxtNumNota.Value.ToString());
                CadParam.Formulario     = int.Parse(TxtNumForm.Value.ToString());
                CadParam.NotaNFE        = int.Parse(TxtNotaNFE.Value.ToString());
                CadParam.FormularioNFE  = int.Parse(TxtFormularioNFE.Value.ToString());
                CadParam.LinhasNota     = int.Parse(TxtLinhasNota.Value.ToString());                
                CadParam.IdFilial       = CadFiliais.IdFilial;
                CadParam.IdConsumidor   = int.Parse(TxtCodCliente.Text);
                CadParam.PercPIS        = decimal.Parse(TxtPercPIS.Value.ToString());
                CadParam.PercCOFINS     = decimal.Parse(TxtPercCofins.Value.ToString());
                CadParam.NFEAmbiente    = LstAmbiente.SelectedIndex;
                CadParam.NFEVersao      = LstVersao.SelectedIndex;
                CadParam.Certificado    = TxtCertificado.Text;
                CadParam.Smtp           = TxtSmtp.Text;
                CadParam.Porta          = int.Parse(TxtPorta.Value.ToString());
                CadParam.Email          = TxtEmailSmtp.Text;
                CadParam.Senha          = TxtSenhaSmtp.Text;
                CadParam.CliDiasInativo = int.Parse(txtClieInativo.Value.ToString());
                CadParam.InssFaixa1     = TxtInssFaixa1.Value;
                CadParam.InssFaixa2     = TxtInssFaixa2.Value;
                CadParam.InssFaixa3     = TxtInssFaixa3.Value;
                CadParam.InssPerc1      = TxtInssPerc1.Value;
                CadParam.InssPerc2      = TxtInssPerc2.Value;
                CadParam.InssPerc3      = TxtInssPerc3.Value;
                CadParam.ObsNF          = TxtObsNF.Text;
                CadParam.CodigoMFe      = TxtCodigoMFe.Text;
                CadParam.ChaveMFe       = TxtChaveMfe.Text;
                CadParam.ChaveRequisicao = TxtChaveRequisicao.Text;
                CadParam.SerialPOS      = TxtSerialPOS.Text;
                CadParam.ChaveValidador = TxtChaveValidador.Text;
                CadParam.IdEntregador   = int.Parse(LstEntregador.SelectedValue.ToString());
                CadParam.EmissorCF      = LstEmissorCF.SelectedIndex;
                CadParam.GravarDados(int.Parse(TxtCodigo.Text) == 0);                
                //
                FrmPrincipal.RegistrarAuditoria(this.Text, CadFiliais.IdFilial, CadFiliais.Cnpj, 2, "Alteração no Cadastro da Empresa");                
                PopularGrid();
                PopularCampos(CadFiliais.IdFilial);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
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
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CadFiliais.IdFilial = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    CadFiliais.Excluir();
                    PopularGrid();
                    LimpaDados();
                    GridDados.Focus();
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
            LstUF         = FrmPrincipal.PopularCombo("SELECT Id_UF,Sigla FROM Estados ORDER BY SIGLA", LstUF);            
            LstEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstEntregador, "Nenhum");
        }
        private void LimpaDados()
        {
            TxtCodigo.Text      = "0";            
            TxtFilial.Text      = "";
            TxtFantasia.Text    = "";
            TxtCnpj.Text        = "";
            TxtInscUF.Text      = "";
            TxtCep.Text         = "";
            TxtEndereco.Text    = "";
            TxtNumero.Text      = "";
            TxtComplemento.Text = "";
            TxtBairro.Text      = "";
            TxtCidade.Text      = "";
            LstUF.SelectedValue = 0;
            TxtFone1.Text       = "";
            TxtFone2.Text       = "";
            TxtFax.Text         = "";
            TxtEmail.Text       = "";
            TxtCodMun.Value     = 0;
            LstRegime.SelectedValue = "0";
            LstEntregador.SelectedValue = "0";                       
            Cb_SldEstoque.Checked = false;
            Cb_ClienteAtraso.Checked = false;
            cb_ClienteAtrasoWS.Checked = false;
            cb_WSCadPessoa.Checked = false;
            Cb_LimiteCredito.Checked = false;
            Cb_NFE.Checked = false;
            Cb_WSNumNFE.Checked = false;
            Cb_NotaIPI.Checked = false;
            Cb_VerCancBxFin.Checked = false;
            TxtNumNota.Value = 0;
            TxtNumForm.Value = 0;
            TxtNotaNFE.Value = 0;
            TxtFormularioNFE.Value = 0;
            TxtLinhasNota.Value = 0;            
            TxtCodCliente.Text = "0";
            TxtCliente.Text = " ";
            TxtPercPIS.Value = 0;
            TxtPercCofins.Value = 0;
            LstAmbiente.SelectedIndex = 0;
            LstVersao.SelectedIndex = 0;
            TxtCertificado.Text = "";
            TxtSmtp.Text = "";
            TxtPorta.Value = 0;
            TxtEmailSmtp.Text = "";
            TxtSenhaSmtp.Text = "";
            TxtObsNF.Text = "";
            TxtCodigoMFe.Text = "";
            TxtChaveMfe.Text = "";
            TxtChaveRequisicao.Text = "";
            TxtSerialPOS.Text = "";
            TxtChaveValidador.Text = "";
            txtClieInativo.Value = 0;
            LstEmissorCF.SelectedIndex = 0;
            PicFoto.Image = null;
            CadFiliais.LerDados(0);
            CadParam.LerDados(0);
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
                    BtnEditar_Click(FrmPrincipal.BtnEditar, null);
            }
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            if (TxtPesquisa.Text.Trim() != "")
            {
                try
                {
                    DataSet Tabela = new DataSet();
                    if (ChkCodigo.Checked)
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Filial,Filial,Fantasia,Cnpj,Fone1,Fax FROM Empresa_Filial WHERE ID_Filial={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Filial,Filial,Fantasia,Cnpj,Fone1,Fax FROM Empresa_Filial WHERE Filial LIKE '%{0}%' order by Filial", TxtPesquisa.Text.Trim()));
                    GridDados.DataSource = Tabela;
                    GridDados.DataMember = Tabela.Tables[0].TableName;
                }
                catch
                {
                    MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                PopularGrid();
            }
        }
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
        private void TxtCnpj_Validated(object sender, EventArgs e)
        {
            if (TxtCnpj.Text != "")
            {
                if (!Controle.ValidarCnpj(TxtCnpj.Text))
                {
                    MessageBox.Show("CNPJ inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtCnpj.Focus();
                }
              /*  else
                {
                    Verificar ExisteFilia = new Verificar();
                    ExisteFilia.Controle = Controle;
                    if (!ExisteFilia.Verificar_CadFilial(int.Parse(TxtCodigo.Text), TxtCnpj.Text))
                    {
                        MessageBox.Show("CNPJ já cadastrado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtCnpj.Focus();
                    }
                }*/
            }
        }
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();
            if (e.ColumnIndex == 3)
            {                
                MaskCol.Mask = "00,000,000/0000-00";
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
        private void TxtCep_Validated(object sender, EventArgs e)
        {
            if (TxtCep.Text.Replace("-", "").Trim() != "")
            {
                if (TxtCep.Text.Replace("-", "").Trim() != CadFiliais.Cep.Trim())
                {
                    Verificar VerificaUF = new Verificar();
                    VerificaUF.Controle = Controle;
                    ConsultaCEP ConsultaCEP = new ConsultaCEP();
                    ConsultaCEP.VerificaCEP(TxtCep.Text);
                    TxtEndereco.Text    = ConsultaCEP.Endereco;
                    TxtBairro.Text      = ConsultaCEP.Bairro;
                    TxtCidade.Text      = ConsultaCEP.Cidade;
                    LstUF.SelectedValue = VerificaUF.Busca_IdUF(ConsultaCEP.UF);
                }
            }
        }

        private void TxtInscUF_Validated(object sender, EventArgs e)
        {
            if (TxtInscUF.Text != "")
            {
                if (!Controle.ValidarCgf(TxtInscUF.Text))
                {
                    MessageBox.Show("Inscrição Estadual inválida", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtInscUF.Focus();
                }                
            }
        }
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
            BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
            BuscaPessoa.ShowDialog();
            if (BuscaPessoa.CadPessoa.IdPessoa > 0)
            {
                TxtCodCliente.Text = BuscaPessoa.CadPessoa.IdPessoa.ToString();
                TxtCliente.Text = BuscaPessoa.CadPessoa.RazaoSocial;
            }
            else
            {
                TxtCodCliente.Text = "0";
                TxtCliente.Text = " ";
            }
        }
        private void SetaPessoa(int IdPessoa)
        {
            CadPessoa.LerDados(IdPessoa);
            CadParam.IdConsumidor = CadPessoa.IdPessoa;
            TxtCodCliente.Text = CadPessoa.IdPessoa.ToString();
            TxtCliente.Text = CadPessoa.RazaoSocial.Trim();            
        }

        private void Cb_ClienteAtraso_CheckedChanged(object sender, EventArgs e)
        {
            cb_ClienteAtrasoWS.Visible = Cb_ClienteAtraso.Checked && !FrmPrincipal.VersaoDistribuidor;
        }

        private void BtnCertificado_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                try
                {
                    NF_e CertNFE = new NF_e();
                    X509Certificate2 oCertificado = new X509Certificate2();
                    oCertificado = CertNFE.SelecionarCertificado();                    
                    if (oCertificado != null)
                        TxtCertificado.Text = oCertificado.Subject;
                    
                }
                catch
                {
                    MessageBox.Show("Falha na leitura do certificado digital", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
            }
        }
        
        private void BtnFoto_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                if (CadFiliais.IdFilial > 0)
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
                        //
                        SqlCommand CmdSql;
                        SqlDataReader Tabela;
                        Tabela = Controle.ConsultaSQL("SELECT * FROM TABIMAGENS WHERE TABELA='FILIAL' AND ID_CHAVE="+ CadFiliais.IdFilial.ToString());
                        if (Tabela.HasRows)
                        {
                            CmdSql = new SqlCommand("Update TABIMAGENS set Imagem=@Imagem WHERE TABELA='FILIAL' and Id_Chave=" + CadFiliais.IdFilial.ToString(), Controle.Conexao);
                            CmdSql.Parameters.AddWithValue("@IMAGEM", photo_aray);
                        }
                        else
                        {
                            CmdSql = new SqlCommand("INSERT INTO TABIMAGENS (ID_CHAVE,TABELA,IMAGEM) VALUES (@CHAVE,@TABELA,@IMAGEM)", Controle.Conexao);
                            CmdSql.Parameters.AddWithValue("@CHAVE", CadFiliais.IdFilial);
                            CmdSql.Parameters.AddWithValue("@TABELA", "FILIAL");
                            CmdSql.Parameters.AddWithValue("@IMAGEM", photo_aray);
                        }
                        CmdSql.ExecuteNonQuery();
                    }
                }
            }
        }
        private void LerFoto()
        {
            if (CadFiliais.IdFilial > 0)
            {
                PicFoto.Image = null;
                //SqlDataAdapter Tab = new SqlDataAdapter(new SqlCommand("SELECT IMAGEM FROM TABIMAGENS WHERE TABELA='FILIAL' AND ID_CHAVE=" + CadFiliais.IdFilial.ToString(), Controle.Conexao));
                SqlDataReader DBFoto;
                DBFoto = Controle.ConsultaSQL("SELECT IMAGEM FROM TABIMAGENS WHERE TABELA='FILIAL' AND ID_CHAVE=" + CadFiliais.IdFilial.ToString());
                byte[] photo_aray;
                if (DBFoto.HasRows)
                {
                    DBFoto.Read();
                    photo_aray = (byte[])DBFoto["IMAGEM"];
                    MemoryStream ms = new MemoryStream(photo_aray);
                    PicFoto.Image = Image.FromStream(ms);
                }
                else
                    PicFoto.Image = null;
            }

        }
    }
}
