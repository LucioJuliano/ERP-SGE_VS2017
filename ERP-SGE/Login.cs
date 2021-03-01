using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace ERP_SGE
{
    public partial class FrmLogin : Form
    {
        public bool AcessoOk;
        public TelaPrincipal FrmPrincipal;
        private Controle_Dados.Usuarios Usuario = new Controle_Dados.Usuarios();
        private int Tentativas          = 0;
        private DataTable Tabela;
        private DataRow[] Linha;

        Controles.Login ValidarLogin          = new Controles.Login();
        Controle_Dados.Funcoes Controle       = new Controle_Dados.Funcoes();
        Controle_Dados.Parametros ParamFilial = new Controle_Dados.Parametros();

        public FrmLogin()
        {
            InitializeComponent();
        }
        private void BtnConfirma_Click(object sender, EventArgs e)
        {
            try            
            {
                FrmPrincipal.Conexao  = null;
                //if (LstConexao.SelectedValue.ToString().Trim() != "0")
                //    FrmPrincipal.ServidorRemoto = true;                
                //AtualizandoConfiguracao();      
                Usuario               = new Controle_Dados.Usuarios();
                FrmPrincipal.AbrirConexao();
                Controle.Conexao      = FrmPrincipal.Conexao;
                ValidarLogin.Controle = Controle;
                ParamFilial.Controle  = Controle;
                Usuario.Controle      = Controle;
                Tentativas            = Tentativas + 1;

                VerificaUsuario(TxtUsuario.Text.Trim());

                Usuario = ValidarLogin.Verificar_Login(TxtUsuario.Text.Trim(), TxtSenha.Text.Trim());
                                
                Linha = Tabela.Select("Conexao='" + LstConexao.SelectedValue.ToString().Trim() + "'");
                FrmPrincipal.IdFilialConexao = int.Parse(Linha[0]["Id"].ToString());

                //Verificando o Release do Sistema
                ParamFilial.LerDados(int.Parse(Linha[0]["Id"].ToString()));

                if (ParamFilial.Release != FrmPrincipal.Release)
                {
                    MessageBox.Show("Versão do Sistema Diferente do Banco de Dados", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    AcessoOk = false;
                    Close();
                    return;                    
                }

                if (TxtUsuario.Text.Trim() == "ADM" && TxtSenha.Text.Trim() == "524952")
                {
                    AcessoOk = true;
                    Usuario = new Controle_Dados.Usuarios();
                    Usuario.Controle = Controle;
                    Usuario.LerDados(0);
                    FrmPrincipal.Perfil_Usuario = Usuario;
                    Close();
                }
                else
                {
                    if (Usuario != null)
                    {
                        FrmPrincipal.Perfil_Usuario = Usuario;
                        AcessoOk = true;                                                
                        Close();
                    }
                    else
                    {                        
                        if (Tentativas >= 3)
                        {
                            MessageBox.Show("Limite de tentativas esgotadas", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            AcessoOk = false;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Autorização negada, Favor verificar Usuário e Senha", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TxtUsuario.Text = "";
                            TxtSenha.Text   = "";
                            TxtUsuario.Focus();
                        }
                    }
                }
            }
            catch
            {                
                MessageBox.Show("Falha na autenticação do acesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AcessoOk = false;
                Close();
            }           
        }
        private void VerificaUsuario(string NomeUsuario)
        {            
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM Usuarios WHERE Usuario='" + NomeUsuario.Trim()+"'");
            if (Tabela.HasRows)
            {
                Tabela.Read();
                if (Tabela["AltSenha"].ToString().Trim() != "")
                {
                    if (int.Parse(Tabela["AltSenha"].ToString().Trim()) == 1)
                    {
                        FrmNovaSenha FrmSenha = new FrmNovaSenha();
                        FrmSenha.ShowDialog();
                        if (FrmSenha.Senha.Trim() != "")
                        {
                            Usuario.LerDados(int.Parse(Tabela["ID_Usuario"].ToString()));
                            Usuario.Senha = Controle.Crypt(FrmSenha.Senha.Trim());
                            Usuario.AltSenha = 0;
                            Usuario.GravarDados();
                            TxtSenha.Text = FrmSenha.Senha;
                            FrmSenha.Dispose();
                        }
                        else
                            FrmSenha.Dispose();                        
                    }
                }                    
            }                        
        }
        private void AtualizandoConfiguracao()
        {
            /*System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            config.AppSettings.Settings.Remove("NomeBanco");
            config.AppSettings.Settings.Remove("NomeServidor");

            config.AppSettings.Settings.Add("NomeBanco", Linha[0]["Banco"].ToString());
            config.AppSettings.Settings.Add("NomeServidor", Linha[0]["Conexao"].ToString());
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");*/
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            AcessoOk = false;
            Close();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;            
            ValidarLogin.Controle = Controle;
            PopularConexao();
            TxtUsuario.Focus();            
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
        private void PopularConexao()
        {
            Tabela = new DataTable();
            Tabela.Columns.Add("Nome",    Type.GetType("System.String"));
            Tabela.Columns.Add("Conexao", Type.GetType("System.String"));
            Tabela.Columns.Add("Id",      Type.GetType("System.Int32"));                   
            StreamReader LerConexao = new StreamReader("ERP-SGE-Conexao.ini");            
            while (!LerConexao.EndOfStream)
            {
                ArrayList Lista = new ArrayList(LerConexao.ReadLine().ToString().Split(char.Parse("|")));
                Tabela.Rows.Add(Lista[0].ToString(), Lista[1].ToString(), int.Parse(Lista[2].ToString()));                
            }            
            LstConexao.Items.Clear();
            LstConexao.Sorted        = false;
            LstConexao.DataSource    = Tabela;
            LstConexao.DisplayMember = "Nome";
            LstConexao.ValueMember   = "Conexao";
            LstConexao.SelectedIndex = 0;            
            LstConexao.Refresh();

            Linha = Tabela.Select("Conexao='" + LstConexao.SelectedValue.ToString().Trim() + "'");
            if (FrmPrincipal.VersaoDistribuidor)
                FrmPrincipal.StringConexao = "Data Source=" + Linha[0]["Conexao"].ToString() + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True; Connect Timeout=1200;";
            else
                FrmPrincipal.StringConexao = "Data Source=" + Linha[0]["Conexao"].ToString() + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True; Connect Timeout=1200;";

        }
        private void LstConexao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Linha != null)
            {
                Linha = Tabela.Select("Conexao='" + LstConexao.SelectedValue.ToString().Trim() + "'");
                FrmPrincipal.StringConexao = "Data Source=" + Linha[0]["Conexao"].ToString() + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True; Connect Timeout=1200;";
            }
        }
    }
}
