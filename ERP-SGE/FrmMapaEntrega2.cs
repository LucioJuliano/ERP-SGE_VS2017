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


namespace ERP_SGE
{
    public partial class FrmMapaEntrega2 : Form
    {

        Funcoes Controle           = new Funcoes();
        MapaEntrega Mapa           = new MapaEntrega();
        MapaEntregaItens MapaItens = new MapaEntregaItens();
        MvVenda Vendas             = new MvVenda();
        Auditoria RegAuditoria     = new Auditoria();
        Veiculos CadVeic           = new Veiculos();
        Entregadores Entregador    = new Entregadores();
        Pessoas CadPessoa          = new Pessoas();
            

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private DataSet TabItens;
        private BindingSource Source_Itens;

        public FrmMapaEntrega2()
        {
            InitializeComponent();
        }

        private void FrmMapaEntrega2_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;
            Mapa.Controle         = Controle;
            MapaItens.Controle    = Controle;
            Vendas.Controle       = Controle;
            RegAuditoria.Controle = Controle;
            CadVeic.Controle      = Controle;
            Entregador.Controle   = Controle;
            CadPessoa.Controle    = Controle;
            TabItens              = new DataSet();
            Source_Itens          = new BindingSource();
            Rb_Pendente.Checked   = true;
            CamposLista();
            PopularGrid();
        }

        private void CamposLista()
        {
            LstPesqEntregador = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstPesqEntregador, "Todos");
            LstEntregador     = FrmPrincipal.PopularCombo("SELECT Id_Entregador,Entregador FROM Entregadores ORDER BY Entregador", LstEntregador, "Nenhum");
            LstVeiculo        = FrmPrincipal.PopularCombo("SELECT Id_VEICULO,Substring(Veiculo,1,30)+' / '+Placa as VEICULO FROM VEICULOS ORDER BY VEICULO", LstVeiculo, "Nenhum");
            LstPesqVeiculo    = FrmPrincipal.PopularCombo("SELECT Id_VEICULO,Substring(Veiculo,1,30)+' / '+Placa as VEICULO FROM VEICULOS ORDER BY VEICULO", LstPesqVeiculo, "Todos");
        }
        private void PopularGrid()
        {

            string Filtro = "";
            if (Rb_Todos.Checked)
                Filtro = "WHERE T1.STATUS <= 2";
            else if (Rb_Pendente.Checked)
                Filtro = "WHERE T1.STATUS = 0";
            else if (Rb_Concluido.Checked)
                Filtro = "WHERE T1.STATUS = 1";


            if (int.Parse(LstPesqEntregador.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Entregador=" + LstPesqEntregador.SelectedValue.ToString();

            if (int.Parse(LstPesqVeiculo.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Veiculo=" + LstPesqVeiculo.SelectedValue.ToString();

            if (TxtPesqConferente.Text != "")
                Filtro = Filtro + " AND T1.Conferente='" + TxtPesqConferente.Text.Trim() + "'";

            if (Chk_Periodo.Checked)
                Filtro = Filtro + " AND T1.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqNumMapa.Text != "")
                Filtro = Filtro + " AND T1.ID_MAPA=" + TxtPesqNumMapa.Text;
            
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.Id_Mapa,T1.Data,CASE T1.STATUS WHEN 1 THEN 'Concluido' else 'Pendente' end as STATUS,T2.Entregador,T1.Conferente,RTRIM(T3.VEICULO)+' / '+RTRIM(T3.PLACA) AS VEICULO FROM MapaEntrega T1 " +
                                             " LEFT JOIN Entregadores T2 ON (T2.Id_Entregador=T1.Id_Entregador)" +
                                             " LEFT JOIN VEICULOS T3 ON (T3.ID_VEICULO=T1.Id_Veiculo)" +
                                             Filtro + " ORDER BY T1.ID_MAPA DESC");

            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Mapa", Mapa.IdMapa);
            Source.Position = item;            
        }
        private void Hab_Botoes()
        {
            BtnConfirmar.Enabled    = !StaFormEdicao && Mapa.Status == 0;
            BtnValidar.Enabled      = !StaFormEdicao && Mapa.Status == 0;
            BtnImprimir.Enabled     = !StaFormEdicao && Mapa.Status == 1;
            BtnReativarLanc.Enabled = !StaFormEdicao && Mapa.Status == 1;
            Pnl3.Enabled            = !StaFormEdicao && Mapa.Status == 0;
            PnlDados1.Enabled       = StaFormEdicao  && Mapa.Status == 0;

        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            Mapa.LerDados(Isn);
            TxtCodigo.Text              = Mapa.IdMapa.ToString();
            TxtData.Value               = Mapa.Data;
            TxtConferente.Text          = Mapa.Conferente;
            TxtObservacao.Text          = Mapa.Obs;
            LstEntregador.SelectedValue = Mapa.IdEntregador.ToString();
            LstVeiculo.SelectedValue    = Mapa.IdVeiculo.ToString();
            Entregador.LerDados(Mapa.IdEntregador);
            Hab_Botoes();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularCampos(0);
            PopularGridItens();
            Hab_Botoes();
            FrmPrincipal.ControleBotoes(true);
            LstEntregador.Focus();
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
                if (Mapa.Status == 1)
                    MessageBox.Show("Mapa já Concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    PopularGridItens();
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    LstEntregador.Focus();
                    Hab_Botoes();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            Entregador.LerDados(int.Parse(LstEntregador.SelectedValue.ToString()));

            if (LstVeiculo.SelectedValue.ToString() == "0" && Entregador.MapaEntrega == 1)
            {
                MessageBox.Show("Favor informar o Veiculo", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LstVeiculo.Focus();
                return;
            }            

            if (LstEntregador.SelectedValue.ToString() != "0")
            {
                Mapa.IdMapa       = int.Parse(TxtCodigo.Text);
                Mapa.Data         = TxtData.Value;                
                Mapa.IdEntregador = int.Parse(LstEntregador.SelectedValue.ToString());
                Mapa.IdVeiculo    = int.Parse(LstVeiculo.SelectedValue.ToString());
                Mapa.Obs          = TxtObservacao.Text;
                Mapa.Conferente   = TxtConferente.Text;
                StaFormEdicao     = false;
                Mapa.GravarDados();

                //Registrando Movimento de Auditoria
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdVeiculo.ToString(), 1, "Gerando o Mapa de Entrega");
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdVeiculo.ToString(), 2, "Alterando o Mapa de Entrega");
                PopularGrid();
                PopularCampos(Mapa.IdMapa);
                PopularGridItens();
                FrmPrincipal.ControleBotoes(false);
                Panel2.Enabled = true;
                GridItens.Focus();
            }
            else
            {
                MessageBox.Show("Favor informar o Entregador", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LstEntregador.Focus();
            }
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                
                Mapa.IdMapa = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Mapa.LerDados(Mapa.IdMapa);

                if (Mapa.Status > 0)
                    MessageBox.Show("Mapa já Concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        Mapa.IdMapa = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        Mapa.Excluir();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, Mapa.IdEntregador.ToString(), 3, "Excluindo");
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
        private void LimpaDados()
        {
            TxtCodigo.Text = "0";
            TxtData.Value = DateTime.Now;
            TxtConferente.Text = "";
            LstEntregador.SelectedValue = 0;
            LstVeiculo.SelectedValue = 0;
            TxtObservacao.Text = "";
            Mapa.LerDados(0);
            PopularGridItens();
            Hab_Botoes();
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
                    Hab_Botoes();
                }
            }
            Hab_Botoes();
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
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
        private void Frm_Deactivate(object sender, EventArgs e)
        {
            FrmPrincipal.LimpaClickBotoes();
        }

        // Controle dos Itens
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela(" select T1.ID_LANC,T1.ID_VENDA,t2.NumDocumento,t3.RazaoSocial,t2.VlrTotal,t4.Vendedor,"+
                                               "(select top 1 rtrim(V1.NumDocumento)+' / '+rtrim(V1.Id_venda) from MvVenda V1 where V1.Status=2  and V1.Id_Pessoa=T2.ID_PESSOA "+
                                               "  and not exists (select * from MapaEntregaItens M1 where M1.Id_Venda=V1.Id_Venda and M1.Status<>2)) as Sugestao,CONVERT(CHAR,t1.Observacao) AS Observacao," +
                                               " t1.Status from MapaEntregaItens t1" +
                                               " left join MvVenda t2 on (t2.Id_Venda=t1.id_venda)" +
                                               " left join Pessoas t3 on (t3.Id_Pessoa=t2.Id_Pessoa)" +
                                               " left join Vendedores t4 on (t4.Id_Vendedor=t2.Id_Vendedor)" +
                                               " WHERE T1.Id_Mapa=" + Mapa.IdMapa.ToString() + " ORDER BY T1.ID_LANC");
            
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;

            int item = Source_Itens.Find("ID_Lanc", MapaItens.IdLanc);
            Source_Itens.Position = item;
            TotalMapa();
            Hab_Botoes();
        }

        private void TotalMapa()
        {
            decimal VlrTotal = 0;
            for (int I = 0; I <= GridItens.RowCount - 1; I++)
            {
                if (int.Parse(GridItens.Rows[I].Cells[8].Value.ToString()) != 2)
                {
                    if (decimal.Parse(GridItens.Rows[I].Cells[4].Value.ToString()) > 0)
                        VlrTotal = VlrTotal + decimal.Parse(GridItens.Rows[I].Cells[4].Value.ToString());
                }
            }
            LblTotal.Text = string.Format("{0:N2}", VlrTotal);
        }

        private void TxtIdVenda_Validated(object sender, EventArgs e)
        {            
            if (TxtIdVenda.Text.Trim() == "")
                return;

            Vendas.LerDados(int.Parse(TxtIdVenda.Value.ToString()));
            TxtPessoa.Text  = Vendas.NmPessoa;
            
            if (Vendas.IdVenda == 0)
            {
                MessageBox.Show("Atenção Movimento não Localizada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtIdVenda.Text = "";
                TxtIdVenda.Focus();
                return;
            }
            else
            {

                CadPessoa.LerDados(Vendas.IdPessoa);

                if (Entregador.MapaEntrega == 1 && CadPessoa.Clie_Forn == 5)
                {
                    MessageBox.Show("Atenção: Movimento de funcionario não pode ser incluido para esse entregador", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }

                if (Vendas.Status == 4)
                {
                    MessageBox.Show("Atenção: Movimento esta cancelado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }

                if (Vendas.Status == 3)
                {
                    MessageBox.Show("Atenção: Movimento ja entregue", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }

                if (Vendas.Status != 2 && (Vendas.TpVenda != "EMVF" && Vendas.TpVenda != "CO"))
                {
                    MessageBox.Show("Atenção Venda não esta faturada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }

                if (Vendas.IdVdDestino > 0)
                {
                    MessageBox.Show("Atenção: Venda Lançada para Entrega em outra Filial", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }

                if (Vendas.Data > Mapa.Data)
                {
                    MessageBox.Show("Atenção: Data da Venda Superior a Data do Mapa de Entrega", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();
                    return;
                }

                if (GridDados.CurrentRow != null)
                {
                    if (ValidarVenda(Vendas.IdVenda))
                    {
                        MapaItens.LerDados(0);
                        MapaItens.IdMapa = Mapa.IdMapa;
                        MapaItens.IdVenda = Vendas.IdVenda;
                        MapaItens.Status = 0;                        
                        MapaItens.GravarDados();
                        PopularGridItens();                        
                        Controle.ExecutaSQL("Update MvVenda set ID_Entregador=" + Mapa.IdEntregador.ToString() + ",PrevEntrega=Convert(DateTime,'" + Mapa.Data.ToShortDateString() + "',103) WHERE Id_Venda=" + MapaItens.IdVenda.ToString());
                        FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, MapaItens.IdVenda.ToString(), 1, "Lanç. Mapa de Entrega No:" + Mapa.IdMapa.ToString());
                    }
                    TxtIdVenda.Text = "";
                    TxtIdVenda.Focus();                    
                }
            }
        }

        private bool ValidarVenda(int IdVenda)
        {
            string sSQL = "SELECT * FROM MAPAENTREGAITENS WHERE ID_VENDA="+IdVenda.ToString()+" ORDER BY ID_LANC DESC";
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (int.Parse(Tab["ID_MAPA"].ToString()) != Mapa.IdMapa && int.Parse(Tab["STATUS"].ToString()) != 2)
                    {
                        MessageBox.Show("Atenção: Movimento informado em outro mapa: " + Tab["ID_MAPA"].ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    if (int.Parse(Tab["ID_MAPA"].ToString()) == Mapa.IdMapa && int.Parse(Tab["STATUS"].ToString()) != 2)
                    {
                        MessageBox.Show("Atenção: Movimento ja informado neste mapa", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }                    
                }                
            }
            return true;
        }

        private void TxtIdVenda_Enter(object sender, EventArgs e)
        {
            TxtPessoa.Text  = "";            
        }

        private void BtnExcluir_Click_1(object sender, EventArgs e)
        {
            if (GridItens.CurrentRow == null)
            {
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MapaItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                Controle.ExecutaSQL("Update MvVenda set ID_Entregador=0,PrevEntrega=Convert(DateTime,'" + Mapa.Data.ToShortDateString() + "',103) WHERE Id_Venda=" + MapaItens.IdVenda.ToString());
                FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, MapaItens.IdVenda.ToString(), 3, "Excluiu do Mapa Entrega");
                MapaItens.Excluir();
                PopularGridItens();
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {

            if (Mapa.Conferente == "")
            {
                MessageBox.Show("Favor informar o Conferente", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Entregador.LerDados(Mapa.IdEntregador);

            if (Entregador.IdEntregador == 0)
            {
                MessageBox.Show("Atenção: Entregador não Localizado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Entregador.MapaEntrega == 0)
            {
                if (MessageBox.Show("Confirma a Conclusão do Mapa ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Controle.ExecutaSQL("Update MvVenda set ID_Entregador=" + Mapa.IdEntregador.ToString() + ",PrevEntrega=Convert(DateTime,'" + Mapa.Data.ToShortDateString() + "',103) WHERE Id_Venda in (SELECT ID_VENDA FROM MAPAENTREGAITENS WHERE ID_MAPA=" + Mapa.IdMapa + ")");
                    Controle.ExecutaSQL("Update MapaEntrega set Status=1 where Id_Mapa=" + Mapa.IdMapa);
                    
                    Mapa.LerDados(Mapa.IdMapa);
                    PopularCampos(Mapa.IdMapa);
                    MessageBox.Show("Atenção: Mapa Concluido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;                
            }

            CadVeic.LerDados(Mapa.IdVeiculo);
            if (CadVeic.IdVeiculo==0)
            {
                MessageBox.Show("Atenção: Veiculo não Localizado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (decimal.Parse(LblTotal.Text) < CadVeic.VlrCarga)
            {
                MessageBox.Show("Atenção: Total do Mapa menor que o Valor de Carga do Veiculo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Verificação de Distribuidor
            string sSQL = "SELECT T2.Id_Pessoa,T3.RazaoSocial,SUM(T2.VlrTotal) AS TOTAL FROM MapaEntregaItens T1" +
                          " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                          " LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)" +
                          " WHERE T3.Clie_Forn=3 AND T1.ID_MAPA=" + Mapa.IdMapa.ToString() +
                          " GROUP BY T3.Clie_Forn,T2.Id_Pessoa,T3.RazaoSocial";

            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (decimal.Parse(Tab["TOTAL"].ToString()) < 300)
                    {
                        MessageBox.Show("Atenção: Total de pedidos R$"+string.Format("{0:N2}", decimal.Parse(Tab["TOTAL"].ToString()))+ " do Cliente: " + Tab["RazaoSocial"].ToString().Trim()+", inferior ao permitido que é de R$ 300,00", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    }
                }                
            }

            //Verificando Revenda
            sSQL = "SELECT T2.Id_Pessoa,T3.RazaoSocial,SUM(T2.VlrTotal) AS TOTAL FROM MapaEntregaItens T1" +
                   " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                   " LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)" +
                   " WHERE T3.Clie_Forn=6 AND T1.ID_MAPA=" + Mapa.IdMapa.ToString() +
                   " GROUP BY T3.Clie_Forn,T2.Id_Pessoa,T3.RazaoSocial";
            Tab = Controle.ConsultaSQL(sSQL);

            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (decimal.Parse(Tab["TOTAL"].ToString()) < 300)
                    {
                        MessageBox.Show("Atenção: Total de pedidos R$" + string.Format("{0:N2}", decimal.Parse(Tab["TOTAL"].ToString())) + " do Cliente: " + Tab["RazaoSocial"].ToString().Trim() + ", inferior ao permitido que é de R$ 300,00", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }
                }
            }

            //Verificando Revenda - Rede 
            sSQL = "SELECT T2.Id_Pessoa,T3.RazaoSocial,SUM(T2.VlrTotal) AS TOTAL FROM MapaEntregaItens T1" +
                   " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                   " LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)" +
                   " WHERE T3.Clie_Forn=6 AND T3.ID_VINCULO > 0 AND T1.ID_MAPA=" + Mapa.IdMapa.ToString() +
                   " GROUP BY T3.Clie_Forn,T2.Id_Pessoa,T3.RazaoSocial";
            Tab = Controle.ConsultaSQL(sSQL);

            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (decimal.Parse(Tab["TOTAL"].ToString()) < 300)
                    {
                        MessageBox.Show("Atenção: Total de pedidos R$" + string.Format("{0:N2}", decimal.Parse(Tab["TOTAL"].ToString())) + " do Cliente: " + Tab["RazaoSocial"].ToString().Trim() + ", inferior ao permitido que é de R$ 300,00", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }
                }
            }

            if (MessageBox.Show("Confirma a Conclusão do Mapa ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Controle.ExecutaSQL("Update MvVenda set ID_Entregador=" + Mapa.IdEntregador.ToString() + ",PrevEntrega=Convert(DateTime,'" + Mapa.Data.ToShortDateString() + "',103) WHERE Id_Venda in (SELECT ID_VENDA FROM MAPAENTREGAITENS WHERE ID_MAPA=" + Mapa.IdMapa + ")");
                Controle.ExecutaSQL("Update MapaEntrega set Status=1 where Id_Mapa=" + Mapa.IdMapa);
                    
                Mapa.LerDados(Mapa.IdMapa);
                PopularCampos(Mapa.IdMapa);
                MessageBox.Show("Atenção: Mapa Concluido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void GridItens_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 || e.ColumnIndex == 8)
            {
                if (e.Value.ToString().Trim() == "2")
                    GridItens.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
               /* else
                {
                    if (e.Value.ToString().Trim() != "")
                        GridItens.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }*/
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            CadVeic.LerDados(Mapa.IdVeiculo);

            string sSql = "SELECT T1.ID_VENDA,T1.NUMDOCUMENTO,T1.FORMNF,T2.RAZAOSOCIAL,RTRIM(T1.ENDERECO)+','+RTRIM(T1.NUMERO)+' '+RTRIM(T1.COMPLEMENTO) AS ENDERECO," +
                          " T1.FONE,T1.CEP,T1.BAIRRO,T1.CIDADE,T3.ENTREGADOR,T4.VENDEDOR,T5.FORMAPGTO,T1.ID_VDMASTER,T1.VLRTOTAL,T7.VENCIMENTO,T7.VLRORIGINAL,T1.PREVENTREGA,T8.DOCUMENTO FROM MAPAENTREGAITENS MP" +
                          " LEFT JOIN MVVENDA  T1 ON (T1.ID_VENDA=MP.ID_VENDA)" +
                          " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                          " LEFT JOIN ENTREGADORES T3 ON (T3.ID_ENTREGADOR=T1.ID_ENTREGADOR)" +
                          " LEFT JOIN VENDEDORES T4 ON (T4.ID_VENDEDOR=T1.ID_VENDEDOR)" +
                          " LEFT JOIN FORMAPAGAMENTO T5 ON (T5.ID_FORMAPGTO=T1.ID_FORMAPGTO)" +
                          " LEFT JOIN LANCFINANCEIRO T7 ON (T7.ID_VENDA=T1.ID_VENDA)" +
                          " LEFT JOIN TIPODOCUMENTO T8 ON (T8.ID_DOCUMENTO=T7.ID_TIPODOCUMENTO)" +                          
                          " WHERE MP.STATUS=0 AND MP.ID_MAPA=" + Mapa.IdMapa.ToString();
            sSql = sSql + " ORDER BY T2.RAZAOSOCIAL,T1.ID_VENDA";

            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelMapEntrega RelMapa = new Relatorios.RelMapEntrega();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(sSql);
            RelMapa.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelMapa;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section2.ReportObjects["LblPeriodo"])).Text = "Mapa: " + string.Format("{0:D5}", Mapa.IdMapa) + "   Veiculo: " + CadVeic.Veiculo.ToString().Trim() + "  Placa: " + CadVeic.Placa.ToString().Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelMapa.Section2.ReportObjects["LblConferente"])).Text = "Conferente: " + Mapa.Conferente.Trim();
            
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;

        }

        private void BtnValidar_Click(object sender, EventArgs e)
        {
            Entregador.LerDados(Mapa.IdEntregador);

            if (Entregador.IdEntregador == 0)
            {
                MessageBox.Show("Atenção: Entregador não Localizado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (Entregador.MapaEntrega == 0)                
               MessageBox.Show("Atenção: Validação do Mapa Concluido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            CadVeic.LerDados(Mapa.IdVeiculo);
            if (CadVeic.IdVeiculo == 0)
            {
                MessageBox.Show("Atenção: Veiculo não Localizado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (decimal.Parse(LblTotal.Text) < CadVeic.VlrCarga)
            {
                MessageBox.Show("Atenção: Total do Mapa menor que o Valor de Carga do Veiculo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Verificação de Distribuidor
            string sSQL = "SELECT T2.Id_Pessoa,T3.RazaoSocial,SUM(T2.VlrTotal) AS TOTAL FROM MapaEntregaItens T1" +
                          " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                          " LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)" +
                          " WHERE T3.Clie_Forn=3 AND T1.ID_MAPA=" + Mapa.IdMapa.ToString() +
                          " GROUP BY T3.Clie_Forn,T2.Id_Pessoa,T3.RazaoSocial";
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);

            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (decimal.Parse(Tab["TOTAL"].ToString()) < 300)
                    {
                        MessageBox.Show("Atenção: Total de pedidos R$" + string.Format("{0:N2}", decimal.Parse(Tab["TOTAL"].ToString())) + " do Cliente: " + Tab["RazaoSocial"].ToString().Trim() + ", inferior ao permitido que é de R$ 300,00", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }
                }
            }

            //Verificando Revenda
            sSQL = "SELECT T2.Id_Pessoa,T3.RazaoSocial,SUM(T2.VlrTotal) AS TOTAL FROM MapaEntregaItens T1" +
                   " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                   " LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)" +
                   " WHERE T3.Clie_Forn=6 AND T1.ID_MAPA=" + Mapa.IdMapa.ToString() +
                   " GROUP BY T3.Clie_Forn,T2.Id_Pessoa,T3.RazaoSocial";
            Tab = Controle.ConsultaSQL(sSQL);

            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (decimal.Parse(Tab["TOTAL"].ToString()) < 300)
                    {
                        MessageBox.Show("Atenção: Total de pedidos R$" + string.Format("{0:N2}", decimal.Parse(Tab["TOTAL"].ToString())) + " do Cliente: " + Tab["RazaoSocial"].ToString().Trim() + ", inferior ao permitido que é de R$ 300,00", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }
                }
            }
            //Verificando Revenda - Rede 
            sSQL = "SELECT T2.Id_Pessoa,T3.RazaoSocial,SUM(T2.VlrTotal) AS TOTAL FROM MapaEntregaItens T1" +
                   " LEFT JOIN MvVenda T2 ON (T2.Id_Venda=T1.Id_Venda)" +
                   " LEFT JOIN Pessoas T3 ON (T3.Id_Pessoa=T2.Id_Pessoa)" +
                   " WHERE T3.Clie_Forn=6 AND T3.ID_VINCULO > 0 AND T1.ID_MAPA=" + Mapa.IdMapa.ToString() +
                   " GROUP BY T3.Clie_Forn,T2.Id_Pessoa,T3.RazaoSocial";
            Tab = Controle.ConsultaSQL(sSQL);

            if (Tab.HasRows)
            {
                while (Tab.Read())
                {
                    if (decimal.Parse(Tab["TOTAL"].ToString()) < 300)
                    {
                        MessageBox.Show("Atenção: Total de pedidos R$" + string.Format("{0:N2}", decimal.Parse(Tab["TOTAL"].ToString())) + " do Cliente: " + Tab["RazaoSocial"].ToString().Trim() + ", inferior ao permitido que é de R$ 300,00", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }
                }
            }
            MessageBox.Show("Atenção: Validação do Mapa Concluido.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnReativarLanc_Click(object sender, EventArgs e)
        {
            if (GridItens.CurrentRow == null)
            {
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MapaItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
            Vendas.LerDados(MapaItens.IdVenda);

            if (Vendas.Status != 2 && (Vendas.TpVenda != "EMVF" && Vendas.TpVenda != "CO"))
            {
                MessageBox.Show("Atenção Venda não esta faturada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Confirma ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Controle.ExecutaSQL("Update MvVenda set ID_Entregador=" + Mapa.IdEntregador.ToString() + ",PrevEntrega=Convert(DateTime,'" + Mapa.Data.ToShortDateString() + "',103) WHERE Id_Venda="+MapaItens.IdVenda.ToString());
                Controle.ExecutaSQL("Update MapaEntregaItens set Status=0 where Id_Lanc=" + MapaItens.IdLanc);
                FrmPrincipal.RegistrarAuditoria(this.Text, Mapa.IdMapa, MapaItens.IdVenda.ToString(), 3, "Desfez o Cancelamento do Item");
                PopularGridItens();
                MessageBox.Show("Processo concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);                
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
        }

        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MapaItens.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
            if (MapaItens.IdLanc>0)
            {
                MapaItens.Obs = GridItens.CurrentRow.Cells[7].Value.ToString();
                MapaItens.GravarDados();
                PopularGridItens();
            }            
        }

    }
}
