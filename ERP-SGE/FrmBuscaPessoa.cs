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
using System.Data.Sql;
using System.Data.SqlClient;

namespace ERP_SGE
{
    public partial class FrmBuscaPessoa : Form
    {
        Funcoes Controle = new Funcoes();        
        public TelaPrincipal FrmPrincipal;
        public Pessoas CadPessoa = new Pessoas();
        public bool RedeGrp = false;
        public FrmBuscaPessoa()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {

            Controle.Conexao = FrmPrincipal.Conexao;            
            CadPessoa.Controle = Controle;
            CadPessoa.IdPessoa = 0;
            LstPesquisa.SelectedIndex = 1;
            BtnFicha.Enabled     = false;
            BtnUltCompra.Enabled = false;
        }

        private void TxtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnFicha.Enabled = false;
                BtnUltCompra.Enabled = false;
                if (TxtPesquisa.Text.Trim() != "")
                {
                    try
                    {
                        string sSQL = "SELECT T1.Id_Pessoa,CASE CLIE_FORN WHEN 0 THEN 'Cliente' WHEN 1 THEN 'Fornecedor' WHEN 2 THEN 'Cliente e Fornecedor' WHEN 3 THEN 'Distribuidor' "+
                                      " WHEN 4 THEN 'Rede ou Grupo' WHEN 5 THEN 'Funcionário' WHEN 6 THEN 'Revenda' WHEN 6 THEN 'Representante' ELSE '' END AS TipoCad,T1.Cnpj,T1.RazaoSocial," +
                                      "T1.Fantasia,T1.LimiteCredito,T1.Credito,T1.Fone,T1.CONTATO,T2.VENDEDOR,Rtrim(T1.Endereco)+','+RTrim(T1.Numero)+' '+Rtrim(T1.Complemento)+' '+RTrim(T1.Cep)+' '+RTrim(T1.Bairro)+' '+Rtrim(T1.Cidade) as Logradouro,T1.DATACADASTRO AS DTCADASTRO FROM Pessoas T1 LEFT JOIN VENDEDORES T2 ON (T2.ID_VENDEDOR=T1.ID_VENDEDOR) ";

                        if (RedeGrp)
                            sSQL = sSQL + " WHERE T1.CLIE_FORN=4 AND";
                        else
                            sSQL = sSQL + " WHERE";

                        DataSet Tabela = new DataSet();
                        if (LstPesquisa.SelectedIndex == 0)
                            Tabela = Controle.ConsultaTabela(string.Format(sSQL + " T1.Ativo=1 and T1.Cnpj LIKE '%{0}%' order by T1.Cnpj", TxtPesquisa.Text.Trim()));
                        else if (LstPesquisa.SelectedIndex == 1)
                            Tabela = Controle.ConsultaTabela(string.Format(sSQL + " T1.Ativo=1 and T1.RazaoSocial LIKE '%{0}%' order by T1.RazaoSocial", TxtPesquisa.Text.Trim()));
                        else if (LstPesquisa.SelectedIndex == 2)
                            Tabela = Controle.ConsultaTabela(string.Format(sSQL + " T1.Ativo=1 and T1.Fantasia LIKE '%{0}%' order by T1.Fantasia", TxtPesquisa.Text.Trim()));
                        else if (LstPesquisa.SelectedIndex == 3)
                            Tabela = Controle.ConsultaTabela(string.Format(sSQL + " T1.Ativo=1 and T1.Endereco LIKE '%{0}%' order by T1.Fantasia", TxtPesquisa.Text.Trim()));
                        else if (LstPesquisa.SelectedIndex == 4)
                            Tabela = Controle.ConsultaTabela(string.Format(sSQL + " T1.Ativo=1 and T1.Fone LIKE '%{0}%' order by T1.Fone" , TxtPesquisa.Text.Trim()));

                        
                        GridDados.DataSource = Tabela;
                        GridDados.DataMember = Tabela.Tables[0].TableName;
                        if (GridDados.CurrentRow != null)
                        {
                            GridDados.Focus();
                            BtnFicha.Enabled = true;
                            BtnUltCompra.Enabled = true && !FrmPrincipal.VersaoDistribuidor;
                        }
                        else
                        {
                            MessageBox.Show("Nenhum registro foi localizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TxtPesquisa.Focus();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void GridDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GridDados.CurrentRow != null)
                    CadPessoa.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                else
                    CadPessoa.LerDados(0);
                Close();
            }
        }

        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString().Trim().Length <= 11)
                    MaskCol.Mask = "000,000,000-00";
                else
                    MaskCol.Mask = "00,000,000/0000-00";

                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            else if (e.ColumnIndex == 7)
            {
                MaskCol.Mask = "(00) 0000-0000";
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            MaskCol.Dispose();
        }

        private void BtnFicha_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                FrmFichaFinanc Ficha = new FrmFichaFinanc();
                Ficha.FrmPrincipal   = FrmPrincipal;
                Ficha.IdPessoa       = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Ficha.LblPessoa.Text = GridDados.CurrentRow.Cells[0].Value.ToString() + "-" + GridDados.CurrentRow.Cells[3].Value.ToString();
                Ficha.LblDtCad.Text  = GridDados.CurrentRow.Cells[11].Value.ToString();
                Ficha.LimiteCredito  = decimal.Parse(GridDados.CurrentRow.Cells[5].Value.ToString());
                Ficha.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*SqlCommand Cmd = new SqlCommand("VerificarCreditoCliente", Controle.Conexao);
            int Cod = 0;       
            Cmd.Parameters.Add("@Cnpj", SqlDbType.Char);
            Cmd.Parameters[0].Value="43880630372";
            Cmd.Parameters.Add("@Ret", SqlDbType.Int);
            Cmd.Parameters[1].Direction = ParameterDirection.Output;            
            Cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = Cmd.ExecuteReader();
            Cod = (int)Cmd.Parameters["@Ret"].Value;*/
        }

        private void BtnUltCompra_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                string Cnpj = GridDados.CurrentRow.Cells[2].Value.ToString().Trim();
                if (Cnpj == "00000000000000" || Cnpj == "11111111111111" || Cnpj == "22222222222222" || Cnpj == "33333333333333" || Cnpj == "44444444444444"
                 || Cnpj == "55555555555555" || Cnpj == "66666666666666" || Cnpj == "77777777777777" || Cnpj == "88888888888888" || Cnpj == "99999999999999")
                    return;
                if (Cnpj == "00000000000" || Cnpj == "11111111111" || Cnpj == "22222222222" || Cnpj == "33333333333" || Cnpj == "44444444444"
                 || Cnpj == "55555555555" || Cnpj == "66666666666" || Cnpj == "77777777777" || Cnpj == "88888888888" || Cnpj == "99999999999")
                    return;

                FrmUltCompraCliente FrmUltCompra = new FrmUltCompraCliente();
                FrmUltCompra.FrmPrincipal = FrmPrincipal;
                FrmUltCompra.CnpjCpf = GridDados.CurrentRow.Cells[2].Value.ToString();
                FrmUltCompra.ShowDialog();
            }
        }

        
    }
}
