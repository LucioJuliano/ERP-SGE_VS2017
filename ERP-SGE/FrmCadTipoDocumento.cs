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

namespace ERP_SGE
{
    public partial class FrmCadTipoDocumento : Form
    {
        Funcoes Controle = new Funcoes();
        TipoDocumento Documentos = new TipoDocumento();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadTipoDocumento()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Documentos.Controle = Controle;
            Documentos.IdDocumento = 0;
            ChkNome.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Documento,Documento FROM TipoDocumento ORDER BY Documento");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Documento", Documentos.IdDocumento);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Documentos.LerDados(Isn);
            TxtCodigo.Text      = Documentos.IdDocumento.ToString();
            TxtDocumento.Text   = Documentos.Documento;            
            TxtTxAdm.Value      = Documentos.TxAdm;
            TxtTxMulta.Value    = Documentos.TxMulta;
            TxtTxJuro.Value     = Documentos.TxJuro;
            TxtDias.Value       = Documentos.Dias;
            TxtCodMFe.Value     = Documentos.MFe;
            TxtAdquirente.Text  = Documentos.Adquirente;
            Cb_Baixa.Checked    = Documentos.Baixa    == 1;
            Cb_ResumoCx.Checked = Documentos.ResumoCx == 1;
            TxtIdServidor.Value = Documentos.IdServidor;
            Chk_Ativo.Checked   = Documentos.Ativo    == 1;
            Cb_BloqPF.Checked   = Documentos.BloqPF   == 1;            
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtDocumento.Focus();
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
                TxtDocumento.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.Text.Trim() != "")
            {
                Documentos.IdDocumento = int.Parse(TxtCodigo.Text);
                Documentos.Documento   = TxtDocumento.Text;                
                Documentos.TxAdm       = TxtTxAdm.Value;
                Documentos.TxMulta     = TxtTxMulta.Value;
                Documentos.TxJuro      = TxtTxJuro.Value;
                Documentos.Adquirente  = TxtAdquirente.Text;
                Documentos.Dias        = int.Parse(TxtDias.Value.ToString());
                Documentos.MFe         = int.Parse(TxtCodMFe.Value.ToString());
                Documentos.IdServidor  = int.Parse(TxtIdServidor.Value.ToString());
                if (Cb_Baixa.Checked) Documentos.Baixa       = 1; else Documentos.Baixa = 0;
                if (Cb_ResumoCx.Checked) Documentos.ResumoCx = 1; else Documentos.ResumoCx = 0;
                if (Chk_Ativo.Checked) Documentos.Ativo      = 1; else Documentos.Ativo = 0;
                if (Cb_BloqPF.Checked) Documentos.BloqPF     = 1; else Documentos.BloqPF = 0;
                
                Documentos.GravarDados();
                PopularGrid();
                PopularCampos(Documentos.IdDocumento);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Tipo do Documento não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtDocumento.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Documentos.IdDocumento = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Documentos.Excluir();
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

        /*private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
            PopularGrid();
        }*/
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
        }
        private void LimpaDados()
        {
            TxtCodigo.Text      = "0";        
            TxtDocumento.Text   = "";
            TxtTxAdm.Value      = 0;
            TxtTxMulta.Value    = 0;
            TxtTxJuro.Value     = 0;
            TxtCodMFe.Value     = 0;
            TxtDias.Value       = 0;
            TxtIdServidor.Value = 0;
            TxtAdquirente.Text  = "";
            Cb_Baixa.Checked    = false;
            Cb_ResumoCx.Checked = false;
            Chk_Ativo.Checked   = false;
            Cb_BloqPF.Checked   = false;            
            Documentos.LerDados(0);
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
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Documento,Documento FROM TipoDocumento WHERE ID_Documento={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Documento,Documento FROM TipoDocumento WHERE Documento LIKE '%{0}%' order by Documento", TxtPesquisa.Text.Trim()));
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

    }
}
