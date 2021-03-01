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
using CDSSoftware;
using System.Collections;
using System.Net; // importe o namespace .Net
using System.Net.Mail; // importe o namespace .Net.Mail


namespace ERP_SGE
{
    public partial class FrmAgendaVisita : Form
    {
        Funcoes Controle       = new Funcoes();
        AgendaVisita Agenda    = new AgendaVisita();
        Auditoria RegAuditoria = new Auditoria();
        Pessoas CadPessoa      = new Pessoas();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmAgendaVisita()
        {
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles            
            Controle.Conexao      = FrmPrincipal.Conexao;
            Agenda.Controle       = Controle;
            CadPessoa.Controle    = Controle;
            RegAuditoria.Controle = Controle;
            Rb_Pendentes.Checked  = true;            
            Chk_Periodo.Checked   = false;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            CamposLista();
            PopularGrid();
        }

        private void CamposLista()
        {
            LstPesqVendedor  = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstPesqVendedor, "TODOS");            
            LstVendedor      = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstVendedor);
            LstConsultor     = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstConsultor);
            LstPesqConsultor = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores WHERE ATIVO=1 ORDER BY Vendedor", LstPesqConsultor,"TODOS");                       
            LstPesqVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();
            LstPesqVendedor.Enabled = FrmPrincipal.Perfil_Usuario.SeusMov == 0;            
        }
        private void PopularGrid()
        {
            string Filtro = "";
            if (Rb_Todas.Checked)
                Filtro = "WHERE T1.STATUS >= 0";
            if (Rb_Aberta.Checked)
                Filtro = "WHERE T1.STATUS = 0";
            else if (Rb_Pendentes.Checked)
                Filtro = "WHERE T1.STATUS = 1";
            else if (Rb_Concluidas.Checked)
                Filtro = "WHERE T1.STATUS = 2";

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND CONVERT(DATETIME,T1.DTVISITA,103) >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND CONVERT(DATETIME,T1.DTVISITA,103) <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (FrmPrincipal.Perfil_Usuario.SeusMov == 1)
            {
                Filtro = Filtro + " AND (T1.ID_USULANC=" + FrmPrincipal.Perfil_Usuario.IdUsuario.ToString() + " OR T1.Id_VENDEDOR=" + LstPesqVendedor.SelectedValue.ToString() + ")";
            }
            else
            {
                if (int.Parse(LstPesqVendedor.SelectedValue.ToString()) > 0)
                    Filtro = Filtro + " AND T1.Id_VENDEDOR=" + LstPesqVendedor.SelectedValue.ToString();
            }

            if (int.Parse(LstPesqConsultor.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_VENDVISITA=" + LstPesqConsultor.SelectedValue.ToString();

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.DTVISITA,T1.CLIENTE,T3.VENDEDOR,T5.VENDEDOR AS CONSULTOR,T1.DTRETORNO,CASE T1.STATUS WHEN 0 THEN 'Em Aberta' WHEN 1 THEN 'Pendente' WHEN 2 THEN 'Concluida' END AS STATUS,T4.NUMDOCUMENTO" +
                                             " FROM AGENDAVISITA T1 " +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                                             " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T1.ID_VENDEDOR)"+  
                                             " LEFT JOIN MVVENDA T4 ON (T4.ID_VENDA=T1.ID_VENDA) " +
                                             " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T1.ID_VENDVISITA)" +  
                                             Filtro + " ORDER BY T1.DTVISITA DESC");

            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Lanc", Agenda.IdLanc);
            Source.Position = item;            
        }
        private void PopularCampos(int Isn)
        {
            Agenda.LerDados(Isn);
            TxtCodigo.Text    = string.Format("{0:D5}",int.Parse(Agenda.IdLanc.ToString()));
            TxtData.Value     = Agenda.Data;
            TxtDtVisita.Value = Agenda.DtVisita;
            TxtObjetivo.Text  = Agenda.Objetivo;
            TxtPessoa.Text    = Agenda.Cliente;
            if (Agenda.DtRetorno.Year > 2000)
            {
                TxtDtRetorno.Value = Agenda.DtRetorno;
                TxtRetorno.Text    = Agenda.Retorno;
            }
            if (Agenda.IdVendedor == 0)
            {
                if (FrmPrincipal.Perfil_Usuario.IdVendedor == 0)
                    LstVendedor.SelectedValue = 0;
                else
                    LstVendedor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor;
            }
            else
                LstVendedor.SelectedValue = Agenda.IdVendedor.ToString();
            LstConsultor.SelectedValue = Agenda.IdVendVisita.ToString();
            SetaPessoa(Agenda.IdPessoa);
            
            Atlz_tela();
        }
        private void Atlz_tela()
        {
            PnlVisita.Enabled    = Agenda.Status != 2;
            BtnRegistrar.Enabled = Agenda.Status == 0 && Agenda.IdLanc > 0;
            BoxRetorno.Visible   = Agenda.Status >= 1;
            BtnConcluir.Enabled  = Agenda.Status == 1;
            BoxRetorno.Enabled   = Agenda.Status == 1;
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {            
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularCampos(0);
            LstVendedor.SelectedValue  = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();
            LstConsultor.SelectedValue = FrmPrincipal.Perfil_Usuario.IdVendedor.ToString();
            FrmPrincipal.ControleBotoes(true);            
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
                                
                if (Agenda.Status == 2)
                {
                    MessageBox.Show("Visita já concluida", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                StaFormEdicao = true;
                FrmPrincipal.ControleBotoes(true);                                
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {            
            if (LstConsultor.SelectedValue.ToString() != "0")
            {
                Agenda.IdLanc       = int.Parse(TxtCodigo.Text);
                Agenda.Data         = TxtData.Value;
                Agenda.DtVisita     = TxtDtVisita.Value;
                Agenda.IdVendedor   = int.Parse(LstVendedor.SelectedValue.ToString());                
                Agenda.Objetivo     = TxtObjetivo.Text;
                Agenda.Cliente      = TxtPessoa.Text;                
                Agenda.IdVendVisita = int.Parse(LstConsultor.SelectedValue.ToString());
                if (Agenda.IdLanc == 0)
                    Agenda.IdUsuLanc = FrmPrincipal.Perfil_Usuario.IdUsuario;
                StaFormEdicao = false;
                Agenda.GravarDados();

                if (Agenda.Status == 1)
                    EnviarEmail("Atualização do Registro da Visita");
                

                //Registrando Movimento de Auditoria
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, Agenda.IdLanc, TxtDtVisita.Value.ToString(), 1, "Pessoa:" + CadPessoa.RazaoSocial);
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Agenda.IdLanc, TxtDtVisita.Value.ToString(), 2, "Pessoa:" + CadPessoa.RazaoSocial);
                PopularGrid();
                PopularCampos(Agenda.IdLanc);
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Favor informar o Consultor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LstConsultor.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                Agenda.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Agenda.LerDados(Agenda.IdLanc);

                if (Agenda.Status == 1)
                {
                    MessageBox.Show("Visita já Agendada, esperando retorno", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (Agenda.Status == 2)
                {
                    MessageBox.Show("Visita já concluida", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

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
                    Agenda.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Agenda.Excluir();
                    //Registrando Movimento de Auditoria
                    FrmPrincipal.RegistrarAuditoria(this.Text, Agenda.IdLanc, TxtDtVisita.Value.ToString(), 3, "Excluindo");
                    PopularGrid();
                    LimpaDados();
                    GridDados.Focus();
                }
            }
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
        private void LimpaDados()
        {
            TxtCodigo.Text             = "0";
            TxtData.Value              = DateTime.Now;
            TxtDtVisita.Value          = DateTime.Now;
            LstVendedor.SelectedValue  = 0;
            LstConsultor.SelectedValue = 0;
            TxtObjetivo.Text           = "";
            TxtRetorno.Text            = "";
            TxtIdVenda.Value           = 0;
            Agenda.LerDados(0);
            PopularCampos(Agenda.IdLanc);
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
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }
        private void SetaPessoa(int IdPessoa)
        {
            CadPessoa.LerDados(IdPessoa);
            Agenda.IdPessoa   = CadPessoa.IdPessoa;
            TxtCodPessoa.Text = CadPessoa.IdPessoa.ToString();
            TxtPessoa.Text    = CadPessoa.RazaoSocial.Trim();   
            if (Agenda.Cliente.Trim()!="")
               TxtPessoa.Text = Agenda.Cliente;
        } 
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                BuscaPessoa.FrmPrincipal = this.FrmPrincipal;
                BuscaPessoa.ShowDialog();
                if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                {
                    CadPessoa.LerDados(BuscaPessoa.CadPessoa.IdPessoa);
                    Agenda.IdPessoa = BuscaPessoa.CadPessoa.IdPessoa;
                    SetaPessoa(BuscaPessoa.CadPessoa.IdPessoa);
                }
            }
        }
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma o Registro da visita", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Controle.ExecutaSQL("Update AgendaVisita set Status=1 where id_lanc=" + Agenda.IdLanc.ToString());
                PopularCampos(Agenda.IdLanc);
                MessageBox.Show("Visita Registrada, Aguarde enviando E-Mail de Registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                EnviarEmail("Agendamento de Visita");
            }                
        }
        private void EnviarEmail(string assunto)
        {

            Vendedores Vendedor = new Vendedores();
            Vendedor.Controle = Controle;
            Vendedor.LerDados(Agenda.IdVendedor);

            Vendedores Consultor = new Vendedores();
            Consultor.Controle = Controle;
            Consultor.LerDados(Agenda.IdVendVisita);

            if (Consultor.Email != "")
            {
                SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);
                cliente.EnableSsl = true;
                MailAddress remetente = new MailAddress("admin@talimpoce.com.br", assunto);
                NetworkCredential credenciais = new NetworkCredential("admin@talimpoce.com.br", "@dminTAL68", "");
                cliente.Credentials = credenciais;

                string Texto = "**** Agendamento de Visita ****" + " \n \n" + "No. Agendamento:" + string.Format("{0:D5}", Agenda.IdLanc) + " \n";
                Texto = Texto + "Cliente :" + Agenda.Cliente + " \n";
                Texto = Texto + "Endereço:" + CadPessoa.Endereco.Trim()+", No.:"+CadPessoa.Numero+" "+CadPessoa.Complemento.Trim() + " \n";
                Texto = Texto + "Bairro  :" + CadPessoa.Bairro.Trim()+" CEP:"+CadPessoa.Cep + " \n";
                Texto = Texto + "Telefone:" + CadPessoa.Fone.Trim() + " Celular:" + CadPessoa.Celular + " \n";
                
                Texto = Texto + "Data/Hora da Visita: " + Agenda.DtVisita.ToString() + "       Vendedor:" + Vendedor.Vendedor.Trim() + " \n \n";
                Texto = Texto + "Objetivo da Visita: " + Agenda.Objetivo.Trim();

                MailAddress destinatario = new MailAddress(Consultor.Email);
                MailMessage mensagem = new MailMessage(remetente, destinatario);
                mensagem.Body = Texto;
                mensagem.Subject = "Agendamento de Visita";
                try
                {
                    cliente.Send(mensagem);
                    MessageBox.Show("E-Mail Enviado com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                }
                catch (Exception e)
                {
                    MessageBox.Show("E-Mail não Enviado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                }
            }
        }
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (TxtRetorno.Text.Trim() == "")
            {
                MessageBox.Show("Favor informar o resultado da visita", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (TxtDtRetorno.Value.Date < TxtDtVisita.Value.Date)
            {
                MessageBox.Show("Atenção: Data de Retorno menor que a data de agendamento da visita", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (TxtIdVenda.Value > 0)
            {                
                SqlDataReader Tab = Controle.ConsultaSQL("SELECT * FROM MVVENDA WHERE ID_VENDA=" + TxtIdVenda.Value.ToString());
                if (Tab.HasRows)
                {
                    while (Tab.Read())
                    {
                        if (int.Parse(Tab["ID_Vendedor"].ToString()) != Agenda.IdVendedor)
                        {
                            MessageBox.Show("Atenção: Vendedor diferente do vendedor do pedido ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    Tab = Controle.ConsultaSQL("SELECT * FROM AGENDAVISITA WHERE ID_VENDA=" + TxtIdVenda.Value.ToString()+" AND ID_LANC<>"+Agenda.IdLanc.ToString());
                    if (Tab.HasRows)
                    {

                        MessageBox.Show("Atenção: Venda ja informada em outro Agendamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Atenção: Venda não Localizada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                
            }
            if (MessageBox.Show("Confirma Conclusão da Visita", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Controle.ExecutaSQL("Update AgendaVisita set Id_Venda="+TxtIdVenda.Value.ToString()+",Id_UsuRet="+FrmPrincipal.Perfil_Usuario.IdUsuario.ToString()+",DtRetorno=convert(DateTime,'"+TxtDtRetorno.Value.ToShortDateString()+"',103),Retorno='"+TxtRetorno.Text+"',Status=2 where id_lanc=" + Agenda.IdLanc.ToString());
                PopularCampos(Agenda.IdLanc);
                MessageBox.Show("Visita Concluida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }                
        }
    }
}
