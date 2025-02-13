﻿using System;
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
    public partial class FrmCadCfop : Form
    {
        Funcoes Controle = new Funcoes();
        CFOP Cfops = new CFOP();
        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadCfop()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;            
            Cfops.Controle = Controle;
            Cfops.IdCfop = 0;
            ChkNome.Checked = true;
            PopularGrid();
        }
        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT Id_Cfop,Cfop,Descricao FROM CFOP ORDER BY CFOP");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Cfop", Cfops.IdCfop);
            Source.Position = item;
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Cfops.LerDados(Isn);
            TxtCodigo.Text = Cfops.IdCfop.ToString();
            TxtCfop.Text = Cfops.Cfop;
            TxtNatureza.Text = Cfops.Naturaza;
            TxtDescricao.Text = Cfops.Descricao;
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtCfop.Focus();
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
                TxtCfop.Focus();
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtCfop.Text.Trim() != "")
            {
                Cfops.IdCfop    = int.Parse(TxtCodigo.Text);
                Cfops.Cfop      = TxtCfop.Text;
                Cfops.Naturaza  = TxtNatureza.Text;
                Cfops.Descricao = TxtDescricao.Text;
                Cfops.GravarDados();
                PopularGrid();
                PopularCampos(Cfops.IdCfop);
                StaFormEdicao = false;
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("CFOP não Informado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCfop.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Cfops.IdCfop = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Cfops.Excluir();
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
            FrmPrincipal.ClickBtnNovo += new EventHandler(this.BtnNovo_Click);
            FrmPrincipal.ClickBtnEditar += new EventHandler(this.BtnEditar_Click);
            FrmPrincipal.ClickBtnGravar += new EventHandler(this.BtnGravar_Click);
            FrmPrincipal.ClickBtnExcluir += new EventHandler(this.BtnExcluir_Click);
            FrmPrincipal.ClickBtnCancelar += new EventHandler(this.BtnCancelar_Click);
            FrmPrincipal.ClickBtnFechar += new EventHandler(this.BtnFechar_Click);
            FrmPrincipal.ControleBotoes(StaFormEdicao);
        }
        private void LimpaDados()
        {
            TxtCodigo.Text    = "0";
            TxtCfop.Text      = "";
            TxtNatureza.Text  = "";
            TxtDescricao.Text = "";
            Cfops.LerDados(0);
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
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Cfop,Cfop,Descricao FROM CFOP WHERE CFOP={0}", TxtPesquisa.Text.Trim()));
                    else
                        Tabela = Controle.ConsultaTabela(string.Format("SELECT Id_Cfop,Cfop,Descricao FROM CFOP WHERE Descricao LIKE '%{0}%' order by Descricao", TxtPesquisa.Text.Trim()));
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

        private void TxtCfop_Validated(object sender, EventArgs e)
        {
            if (TxtCfop.Text != "")
            {
                Verificar ExisteCFOP = new Verificar();
                 ExisteCFOP.Controle = Controle;
                 if (!ExisteCFOP.VerificarCadCFOP(int.Parse(TxtCodigo.Text), TxtCfop.Text))
                 {
                     MessageBox.Show("CFOP já cadastrado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     TxtCfop.Focus();
                 }
            }
        }
    }
}
