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
using System.Collections;
namespace ERP_SGE
{
    public partial class FrmAutDebitoPessoa : Form
    {
        Funcoes Controle  = new Funcoes();
        MvVenda Vendas    = new MvVenda();
        Filiais CadFilial = new Filiais();

        public TelaPrincipal FrmPrincipal;
        public bool LiberaPrd;
        public int IdProduto = 0;
        public decimal Saldo = 0;
        public FrmAutDebitoPessoa()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao        = FrmPrincipal.Conexao;
            TxtVlrLiberado.Visible  = !LiberaPrd;
            LblVlrLiberacao.Visible = !LiberaPrd;
            Vendas.Controle         = Controle;
            CadFilial.Controle      = Controle;
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "");
            //LstFilial.Visible     = LiberaPrd;
            LstFilial.SelectedValue = FrmPrincipal.IdFilialConexao.ToString();
            Rb_Financeira.Checked   = true;
            BoxTipo.Visible         = !LiberaPrd;
            Ck_NaoPrzPg.Visible     = !LiberaPrd;            
            Ck_NaoPrzPg.Checked     = false;
        }
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (TxtIdVenda.Value > 0)
            {
                if (int.Parse(LstFilial.SelectedValue.ToString()) == FrmPrincipal.IdFilialConexao)
                {
                    Vendas.LerDados(int.Parse(TxtIdVenda.Value.ToString()));
                    if (Vendas.IdVenda == 0)
                    {
                        MessageBox.Show("Venda não localizada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
                if (MessageBox.Show("Confirma a liberação", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (LiberaPrd)
                    {
                        SqlDataReader Tabela;
                        Tabela = Controle.ConsultaSQL("SELECT * FROM LiberacaoProduto WHERE Id_Filial=" + LstFilial.SelectedValue.ToString() + "and Id_Venda=" + TxtIdVenda.Value.ToString() + " and Id_Produto=" + IdProduto.ToString());

                        if (Tabela.HasRows)
                            Controle.ExecutaSQL("Update LiberacaoProduto Set Id_Filial=" + LstFilial.SelectedValue.ToString() + ", Id_Usuario=" + FrmPrincipal.Perfil_Usuario.IdUsuario.ToString() + ",Data=Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103),Estoque=" + Controle.FloatToStr(Saldo, 3) + " Where Id_Venda=" + TxtIdVenda.Value.ToString() + " and Id_Produto=" + IdProduto.ToString());
                        else
                            Controle.ExecutaSQL("Insert into LiberacaoProduto (Id_Venda,Id_Produto,Id_Usuario,Data,Id_Filial,Estoque) Values (" + TxtIdVenda.Value.ToString() + "," + IdProduto.ToString() + "," + FrmPrincipal.Perfil_Usuario.IdUsuario.ToString() + ",Convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103)," + LstFilial.SelectedValue.ToString() + "," + Controle.FloatToStr(Saldo, 3) + ")");
                        MessageBox.Show("liberação concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        if (int.Parse(LstFilial.SelectedValue.ToString()) == FrmPrincipal.IdFilialConexao)
                        {
                            Vendas.LerDados(int.Parse(TxtIdVenda.Value.ToString()));
                            if (Rb_Financeira.Checked)
                                Vendas.SetaAutDebito(FrmPrincipal.Perfil_Usuario.IdUsuario, TxtVlrLiberado.Value);
                            else if (Rb_Parcela.Checked)
                                Vendas.SetaAutParcelas(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            else if (Rb_PessoaF.Checked)
                                Vendas.SetaAutPessoaF(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            else if (Rb_PrimeiraCompra.Checked)
                                Vendas.SetaAutPrimeira(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            else
                            {
                                Vendas.SetaAutDebito(FrmPrincipal.Perfil_Usuario.IdUsuario, TxtVlrLiberado.Value);
                                Vendas.SetaAutParcelas(FrmPrincipal.Perfil_Usuario.IdUsuario,Ck_NaoPrzPg.Checked);
                                Vendas.SetaAutPessoaF(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                                Vendas.SetaAutPrimeira(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            }

                            MessageBox.Show("Liberação concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                        else
                        {
                            SqlConnection ServidorDestino;
                            Filiais FilialDest = new Filiais();
                            FilialDest.Controle = Controle;
                            FilialDest.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));

                            if (FilialDest.ServidorRemoto == "")
                            {
                                MessageBox.Show("Atenção: Configuração do Servidor Destino inválida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                return;
                            }

                            Funcoes ControleDest = new Funcoes();
                            ControleDest.Conexao = ServidorDestino;

                            MvVenda VendaDest  = new MvVenda();
                            VendaDest.Controle = ControleDest;

                            VendaDest.LerDados(int.Parse(TxtIdVenda.Value.ToString()));

                            if (VendaDest.IdVenda == 0)
                            {
                                MessageBox.Show("Venda não localizada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (Rb_Financeira.Checked)
                                VendaDest.SetaAutDebito(FrmPrincipal.Perfil_Usuario.IdUsuario, TxtVlrLiberado.Value);
                            else if (Rb_Parcela.Checked)
                                VendaDest.SetaAutParcelas(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            else if (Rb_PessoaF.Checked)
                                VendaDest.SetaAutPessoaF(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            else if (Rb_PrimeiraCompra.Checked)
                                VendaDest.SetaAutPrimeira(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            else
                            {
                                VendaDest.SetaAutDebito(FrmPrincipal.Perfil_Usuario.IdUsuario, TxtVlrLiberado.Value);
                                VendaDest.SetaAutParcelas(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                                VendaDest.SetaAutPessoaF(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                                VendaDest.SetaAutPrimeira(FrmPrincipal.Perfil_Usuario.IdUsuario, Ck_NaoPrzPg.Checked);
                            }
                            MessageBox.Show("Liberação concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
        }

        private void FrmAutDebitoPessoa_Shown(object sender, EventArgs e)
        {
            if (LiberaPrd)
                this.Text = "Liberação de Estoque";
        }
    }
}
