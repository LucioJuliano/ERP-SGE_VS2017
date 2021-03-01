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
    public partial class FrmFinanceiro : Form
    {
        Funcoes Controle              = new Funcoes();
        Financeiro Financ             = new Financeiro();        
        Pessoas CadPessoa             = new Pessoas();
        MvContaCaixa MvLivroCx        = new MvContaCaixa();
        TabAuxFinanceiro TabAuxFinanc = new TabAuxFinanceiro();


        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private string SqlConsulta = "";

        // Tabelas
        private DataSet TabItens;
        private BindingSource Source_Itens;

        public FrmFinanceiro()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles
            CamposLista();
            Controle.Conexao      = FrmPrincipal.Conexao;
            Financ.Controle       = Controle;            
            CadPessoa.Controle    = Controle;
            MvLivroCx.Controle    = Controle;
            TabAuxFinanc.Controle = Controle;
            Financ.IdLanc       = 0;
            Chk_Periodo.Checked = true;
            Dt1.Value           = DateTime.Now;
            Dt2.Value           = DateTime.Now;
            TabItens            = new DataSet();
            Source_Itens        = new BindingSource();
            Rb_TpReceb.Checked  = true;
            Rb_PesqStaVencer.Checked = true;
            Rb_TodasDesp.Checked     = true;
            Box_PesqDesp.Visible     = false;
            PopularGrid();           
        }
        private void CamposLista()
        {
            LstPesqFilial    = FrmPrincipal.PopularCombo("SELECT ID_FILIAL,SUBSTRING(FANTASIA,1,60) AS FILIAL FROM EMPRESA_FILIAL ORDER BY FANTASIA", LstPesqFilial);
            LstPesqCusto     = FrmPrincipal.PopularCombo("SELECT ID_CUSTO,CUSTO FROM CENTROCUSTO ORDER BY CUSTO", LstPesqCusto);
            LstPesqTipoDoc   = FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstPesqTipoDoc);
            LstPesqCaixa     = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstPesqCaixa);
            LstPesqBanco     = FrmPrincipal.PopularCombo("SELECT Id_Banco,Substring(Banco,1,20)+' Ag:'+NumAgencia+' CC:'+Conta as Banco FROM Bancos", LstPesqBanco);
            LstPesqFormaPgto = FrmPrincipal.PopularCombo("SELECT ID_FormaPgto,FormaPgto FROM FormaPagamento ORDER BY FormaPgto", LstPesqFormaPgto);
            LstPesqVendedor  = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", LstPesqVendedor);
            LstPesqDepart    = FrmPrincipal.PopularCombo("SELECT ID_Departamento,Departamento FROM Departamentos ORDER BY Departamento", LstPesqDepart);
            LstPesqAgente    = FrmPrincipal.PopularCombo("SELECT ID_Agente,Agente FROM AgenteCobrador ORDER BY Agente", LstPesqAgente);            
            LstUsuario       = FrmPrincipal.PopularCombo("SELECT ID_Usuario,Usuario FROM Usuarios ORDER BY Usuario", LstUsuario);
            LstPesqTipoMov   = FrmPrincipal.PopularCombo("SELECT CHAVE,SUBSTRING(DESCRICAO,1,40) AS DESCRICAO FROM TABELASAUX WHERE CHAVE IN ('OE','PV','TROCA','AM','VF') AND CAMPO='VENDA' ORDER BY DESCRICAO", LstPesqTipoMov);
            // Campos da Ficha
            LstFilial        = FrmPrincipal.PopularCombo("SELECT ID_FILIAL,SUBSTRING(FANTASIA,1,60) AS FILIAL FROM EMPRESA_FILIAL ORDER BY FANTASIA", LstFilial);
            LstCusto         = FrmPrincipal.PopularCombo("SELECT ID_CUSTO,CUSTO FROM CENTROCUSTO ORDER BY CUSTO", LstCusto);
            LstDepartamento  = FrmPrincipal.PopularCombo("SELECT ID_Departamento,Departamento FROM Departamentos ORDER BY Departamento", LstDepartamento);
            LstTipoDoc       = FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Documento", LstTipoDoc);
            LstVendedor      = FrmPrincipal.PopularCombo("SELECT Id_Vendedor,SubString(Vendedor,1,40) as Vendedor FROM Vendedores Where Ativo=1 ORDER BY Vendedor", LstVendedor);            
            LstBanco         = FrmPrincipal.PopularCombo("SELECT Id_Banco,Substring(Banco,1,20)+' Ag:'+NumAgencia+' CC:'+Conta as Banco FROM Bancos", LstBanco);
            LstCaixa         = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstCaixa);
            LstFormaPgto     = FrmPrincipal.PopularCombo("SELECT ID_FormaPgto,FormaPgto FROM FormaPagamento ORDER BY FormaPgto", LstFormaPgto);
            LstAgente        = FrmPrincipal.PopularCombo("SELECT ID_Agente,Agente FROM AgenteCobrador ORDER BY Agente", LstAgente);
            LstCaixaPgto     = FrmPrincipal.PopularCombo("SELECT ID_Caixa,Caixa FROM ContaCaixa ORDER BY Caixa", LstCaixaPgto);
            ColFilial        = FrmPrincipal.PopularComboGrid("SELECT Id_Filial,Substring(FANTASIA,1,60) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", ColFilial);
            ColCustoTabAux   = FrmPrincipal.PopularComboGrid("SELECT ID_CUSTO,CUSTO FROM CENTROCUSTO ORDER BY CUSTO", ColCustoTabAux);
            LstTipoCadastro.SelectedIndex = 0;
        }
        private void PopularGrid()
        {
            string Filtro = "";
            string Orderna = " ORDER BY T1.VENCIMENTO DESC";
            if (Chk_Periodo.Checked)
            {
                if (int.Parse(LstUsuario.SelectedValue.ToString()) > 0)
                {
                    if (Rb_PesqStaLiq.Checked)
                        Filtro = " Where T1.DtBaixa >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.DtBaixa <= Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)";
                    else
                        Filtro = " Where T1.DataLanc >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.DataLanc <= Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)";
                }
                else
                {
                    if (Rb_PesqStaTodos.Checked)
                        Filtro = " Where T1.Vencimento >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.Vencimento <= Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)";
                    else if (Rb_PesqStaAtraso.Checked)
                        Filtro = " Where T1.Vencimento < Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.Status=0";
                    else if (Rb_PesqStaLiq.Checked)
                    {
                        Filtro = " Where T1.DtBaixa >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.DtBaixa <= Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103) and T1.Status=1";
                        Orderna = "ORDER BY T1.DTBAIXA DESC";
                    }
                    else if (Rb_PesqStaVencer.Checked)
                        Filtro = " Where T1.Vencimento >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.Vencimento <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) and T1.Status=0";
                }
            }
            else
            {
                Filtro = " WHERE 1=1";

                if (Rb_PesqStaLiq.Checked)
                    Filtro = Filtro + " and T1.Status = 1";
                else if (Rb_PesqStaAtraso.Checked)
                    Filtro = " Where T1.Vencimento < Convert(DateTime,'" + DateTime.Now.Date.ToString() + "',103) and T1.Status=0";
                else if (Rb_PesqStaVencer.Checked)
                    Filtro = " Where T1.Vencimento >= Convert(DateTime,'" + DateTime.Now.Date.ToString() + "',103) and T1.Status=0";
            }

            
            if (Cb_PesInativa.Checked)
                Filtro = Filtro + " and T1.Inativa = 1";

            if (Cb_PesPerdido.Checked)
                Filtro = Filtro + " and T1.Perdido = 1";
            else
                Filtro = Filtro + " and T1.Perdido = 0";

            if (!Rb_TodasDesp.Checked && Box_PesqDesp.Visible)
            {
                if (Rd_PesqForn.Checked)
                    Filtro = Filtro + " and T1.DespForn = 0";
                else
                    Filtro = Filtro + " and T1.DespForn = 1";
            }
            
            if (!Rb_TpTodos.Checked)
            {
                if (Rb_TpPagar.Checked)
                    Filtro = Filtro + " and T1.PagRec = 1";
                else
                    Filtro = Filtro + " and T1.PagRec = 2";
            }
            if (TxtPesqNumDoc.Text.Trim() != "")
                Filtro = Filtro + " and T1.NumDocumento Like '%" + TxtPesqNumDoc.Text.Trim() + "%'";
            if (TxtPesqNF.Text.Trim() != "")
                Filtro = Filtro + " and T1.NotaFiscal Like '%" + TxtPesqNF.Text.Trim() + "%'";
            if (TxtPesqNumVenda.Value > 0)
                Filtro = Filtro + " and T1.Id_Venda =" + TxtPesqNumVenda.Value.ToString();
            if (TxtPesqPessoa.Text.Trim() != "")
                Filtro = Filtro + " and T2.RazaoSocial Like '%" + TxtPesqPessoa.Text.Trim() + "%'";
            if (int.Parse(LstPesqFilial.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Filial=" + LstPesqFilial.SelectedValue.ToString();
            if (int.Parse(LstPesqCusto.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Custo=" + LstPesqCusto.SelectedValue.ToString();
            if (int.Parse(LstPesqTipoDoc.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_TipoDocumento=" + LstPesqTipoDoc.SelectedValue.ToString();
            if (int.Parse(LstPesqCaixa.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Caixa=" + LstPesqCaixa.SelectedValue.ToString();
            if (int.Parse(LstPesqBanco.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Banco=" + LstPesqBanco.SelectedValue.ToString();
            if (int.Parse(LstPesqFormaPgto.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FormaPgto=" + LstPesqFormaPgto.SelectedValue.ToString();
            if (int.Parse(LstPesqVendedor.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Vendedor=" + LstPesqVendedor.SelectedValue.ToString();
            if (int.Parse(LstPesqDepart.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Departamento=" + LstPesqDepart.SelectedValue.ToString();
            if (int.Parse(LstPesqAgente.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_Agente=" + LstPesqAgente.SelectedValue.ToString();
            if (LstPesqTipoMov.SelectedValue.ToString() != "0")
                Filtro = Filtro + " AND T7.TpVenda='" + LstPesqTipoMov.SelectedValue.ToString()+"'";

            if (LstTipoCadastro.SelectedIndex > 0)
            {
                Filtro = Filtro + " AND T2.Clie_Forn=" + (LstTipoCadastro.SelectedIndex - 1).ToString();
            }

            if (TxtPesqValor.Value > 0 && TxtPesqValor.Text.Trim()!="")
                Filtro = Filtro + " AND T1.VlrOriginal=" + Controle.FloatToStr(TxtPesqValor.Value, 2);

            if (int.Parse(LstUsuario.SelectedValue.ToString()) > 0)
            {
                if (Rb_PesqStaLiq.Checked)
                    Filtro = Filtro + " AND T1.Id_UsuBaixa=" + LstUsuario.SelectedValue.ToString();
                else
                    Filtro = Filtro + " AND T1.Id_UsuLanc=" + LstUsuario.SelectedValue.ToString();
            }
            try
            {
                SqlConsulta = "SELECT T1.ID_LANC,T1.DATALANC,CASE T1.DOCREC WHEN 1 THEN 'X' ELSE '' END AS DOCREC,CASE T1.PAGREC WHEN 1 THEN 'À Pagar' WHEN 2 THEN 'À Receber' ELSE ' ' END AS TIPOLANC,T1.NUMDOCUMENTO," +
                              " T2.RAZAOSOCIAL AS PESSOA,T2.FONE,T1.VENCIMENTO,ISNULL(T1.VlrOriginal,0) AS VlrOriginal,T1.DTBAIXA,T1.VLRBAIXA,T6.AGENTE,T1.REFERENTE,T1.ID_VENDA,T3.FORMAPGTO, T4.FANTASIA AS FILIAL, T5.VENDEDOR,T8.USUARIO AS USUBAIXA,T9.CUSTO,T0.GRUPO,ISNULL(TE.PENDENCIA,0) AS PENDESTOQUE " +
                              " FROM LANCFINANCEIRO T1 LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) LEFT JOIN FORMAPAGAMENTO T3 ON (T3.ID_FORMAPGTO=T1.ID_FORMAPGTO)" +
                              " LEFT JOIN EMPRESA_FILIAL T4 ON (T4.ID_FILIAL=T1.ID_FILIAL) LEFT OUTER JOIN VENDEDORES AS T5 ON T5.ID_VENDEDOR=T1.ID_VENDEDOR LEFT JOIN AGENTECOBRADOR T6 ON (T6.ID_AGENTE=T1.ID_AGENTE) "+
                              " LEFT JOIN MVVENDA T7 ON (T7.ID_VENDA=T1.ID_VENDA) LEFT JOIN USUARIOS T8 ON (T8.ID_USUARIO=T1.ID_USUBAIXA) LEFT JOIN CENTROCUSTO T9 ON (T9.ID_CUSTO=T1.ID_CUSTO) LEFT JOIN GRUPOCCUSTO T0 ON (T0.ID_GRPCUSTO=T9.ID_GRPCUSTO) LEFT JOIN MvEstoque  TE ON (TE.ID_MOV=T1.Id_Mov)" + Filtro;

                DataSet Tabela = new DataSet();
                Tabela = Controle.ConsultaTabela(SqlConsulta + Orderna);
                BindingSource Source = new BindingSource();
                Source.DataSource    = Tabela;
                Source.DataMember    = Tabela.Tables[0].TableName;
                GridDados.DataSource = Source;
                int item = Source.Find("Id_Lanc", Financ.IdLanc);
                Source.Position = item;

                decimal T_ARec = 0;
                decimal T_Rec  = 0;
                decimal T_APag = 0;
                decimal T_Pag  = 0;

                for (int I = 0; I <= GridDados.RowCount - 1; I++)
                {
                    if (GridDados.Rows[I].Cells[3].Value.ToString() == "À Pagar")
                    {
                        if (decimal.Parse(GridDados.Rows[I].Cells[10].Value.ToString()) > 0)
                            T_Pag = T_Pag + decimal.Parse(GridDados.Rows[I].Cells[10].Value.ToString());
                        else
                            T_APag = T_APag + decimal.Parse(GridDados.Rows[I].Cells[8].Value.ToString());
                    }
                    else
                    {
                        if (decimal.Parse(GridDados.Rows[I].Cells[10].Value.ToString()) > 0)
                            T_Rec = T_Rec + decimal.Parse(GridDados.Rows[I].Cells[10].Value.ToString());
                        else
                            T_ARec = T_ARec + decimal.Parse(GridDados.Rows[I].Cells[8].Value.ToString());
                    }
                }
                Lbl_A_Pag.Text = string.Format("{0:N2}", T_APag);
                Lbl_Pag.Text   = string.Format("{0:N2}", T_Pag); 
                Lbl_A_Rec.Text = string.Format("{0:N2}", T_ARec);
                Lbl_Rec.Text   = string.Format("{0:N2}", T_Rec);
            }
            catch
            {
                MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularCampos(int Isn)
        {
            Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            Financ.LerDados(Isn);
            TxtCodigo.Text = Financ.IdLanc.ToString();
            if (Financ.PagRec == 1) Rb_Pagar.Checked   = true;
            if (Financ.PagRec == 2) Rb_Receber.Checked = true;
            TxtDataLanc.Value             = Financ.DataLanc;
            TxtNumDoc.Text                = Financ.NumDoc;
            TxtReferente.Text             = Financ.Referente;
            TxtNotaFiscal.Text            = Financ.NotaFiscal;                        
            TxtVlrOriginal.Value          = Financ.VlrOriginal;
            TxtVlrAtual.Value             = Financ.VlrAtual;
            TxtVencimento.Value           = Financ.Vencimento;
            TxtObservacao.Text            = Financ.Observacao;
            TxtIdVenda.Value              = Financ.IdVenda;   
            LstFilial.SelectedValue       = Financ.IdFilial.ToString();
            LstCusto.SelectedValue        = Financ.IdCusto.ToString();
            LstDepartamento.SelectedValue = Financ.IdDepartamento.ToString();
            LstTipoDoc.SelectedValue      = Financ.IdTipoDocumento.ToString();
            LstVendedor.SelectedValue     = Financ.IdVendedor.ToString();
            LstBanco.SelectedValue        = Financ.IdBanco.ToString();
            LstCaixa.SelectedValue        = Financ.IdCaixa.ToString();
            LstFormaPgto.SelectedValue    = Financ.IdFormaPgto.ToString();
            LstAgente.SelectedValue       = Financ.IdAgente.ToString();
            LstCaixaPgto.SelectedValue    = Financ.IdCaixaPgto.ToString();
            Cb_Inativa.Checked            = Financ.Inativa == 1;
            Cb_Perdido.Checked            = Financ.Perdido == 1;
            Ck_DocRec.Checked             = Financ.DocRec == 1;
            Rb_Fornecedor.Checked         = Financ.DespForn == 0;
            Rb_Despesa.Checked            = Financ.DespForn == 1;
            

            if (Financ.Status > 0)
                TxtDtBaixa.Value = Financ.DtBaixa;

            TxtVlrMulta.Value    = Financ.VlrMulta;
            TxtVlrJuros.Value    = Financ.VlrJuro;
            TxtVlrDesconto.Value = Financ.VlrDesconto;
            TxtVlrBaixa.Value    = Financ.VlrBaixa;
            BoxTipoLanc.Enabled  = Financ.IdLanc == 0;
                        
            SetaPessoa(Financ.IdPessoa);
            Hab_Botoes();
            PopularGridItens();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
            {
                MessageBox.Show("Usuário não tem acesso para alterar o financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            FrmPrincipal.ControleBotoes(true);
            TxtNumDoc.Focus();
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
            {                
                MessageBox.Show("Usuário não tem acesso para alterar o financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                    
            if (GridDados.CurrentRow == null)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                if (Financ.Status == 1)
                    MessageBox.Show("Lançamento já Baixado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    TxtNumDoc.Focus();
                    Hab_Botoes();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text.Trim() != "")
            {
                if (int.Parse(TxtCodPessoa.Text) == 0)
                {
                    MessageBox.Show("Favor informar a Pessoa", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Financ.IdLanc          = int.Parse(TxtCodigo.Text);
                    Financ.DataLanc        = TxtDataLanc.Value;
                    Financ.IdPessoa        = int.Parse(TxtCodPessoa.Text);
                    Financ.NumDoc          = TxtNumDoc.Text;
                    Financ.Referente       = TxtReferente.Text;
                    Financ.NotaFiscal      = TxtNotaFiscal.Text;
                    Financ.VlrOriginal     = TxtVlrOriginal.Value;
                    Financ.Vencimento      = TxtVencimento.Value;
                    Financ.Observacao      = TxtObservacao.Text;
                    Financ.IdVenda         = int.Parse(TxtIdVenda.Value.ToString());
                    Financ.IdFilial        = int.Parse(LstFilial.SelectedValue.ToString());
                    Financ.IdCusto         = int.Parse(LstCusto.SelectedValue.ToString());
                    Financ.IdDepartamento  = int.Parse(LstDepartamento.SelectedValue.ToString());
                    Financ.IdTipoDocumento = int.Parse(LstTipoDoc.SelectedValue.ToString());
                    Financ.IdVendedor      = int.Parse(LstVendedor.SelectedValue.ToString());
                    Financ.IdBanco         = int.Parse(LstBanco.SelectedValue.ToString());
                    Financ.IdCaixa         = int.Parse(LstCaixa.SelectedValue.ToString());
                    Financ.IdFormaPgto     = int.Parse(LstFormaPgto.SelectedValue.ToString());
                    Financ.IdAgente        = int.Parse(LstAgente.SelectedValue.ToString());
                    Financ.IdUsuLanc       = FrmPrincipal.Perfil_Usuario.IdUsuario;
                    Financ.IdCaixaPgto     = int.Parse(LstCaixaPgto.SelectedValue.ToString());
                    Financ.VlrAtual        = CalcValorAtual(Financ.IdTipoDocumento, Financ.VlrOriginal);
                    if (Rb_Pagar.Checked)   Financ.PagRec = 1;
                    if (Rb_Receber.Checked) Financ.PagRec = 2;                    
                    if (Cb_Inativa.Checked) Financ.Inativa = 1; else Financ.Inativa = 0;
                    if (Cb_Perdido.Checked) Financ.Perdido = 1; else Financ.Perdido = 0;
                    if (Ck_DocRec.Checked)  Financ.DocRec  = 1; else Financ.DocRec  = 0;
                    if (Rb_Fornecedor.Checked) Financ.DespForn = 0; else Financ.DespForn = 1;
                    StaFormEdicao          = false;                    
                    Financ.GravarDados();

                    if (int.Parse(TxtCodigo.Text) == 0 && Financ.PagRec==1)
                    {
                        TabAuxFinanc.LerDados(0);
                        TabAuxFinanc.IdLanc   = Financ.IdLanc;
                        TabAuxFinanc.IdFilial = Financ.IdFilial;
                        TabAuxFinanc.IdCusto  = Financ.IdCusto;
                        TabAuxFinanc.Valor    = Financ.VlrOriginal;
                        TabAuxFinanc.GravarDados();
                    }
                    //Registrando Movimento de Auditoria
                    if (TxtCodigo.Text=="0")                        
                        FrmPrincipal.RegistrarAuditoria(this.Text, Financ.IdLanc, Financ.NumDoc, 1, "Inclusão o Lançamento");
                    else
                        FrmPrincipal.RegistrarAuditoria(this.Text, Financ.IdLanc, Financ.NumDoc, 2, "Alteração do Lançamento");
                    PopularGrid();
                    PopularCampos(Financ.IdLanc);
                    FrmPrincipal.ControleBotoes(false);
                }
            }
            else
            {
                MessageBox.Show("Informe o Numero do Documento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtNumDoc.Focus();
            }
        }
        private decimal CalcValorAtual(int IdDoc, decimal Vlr)
        {
            if (Vlr > 0)
            {
                SqlDataReader TabDoc;
                TabDoc = Controle.ConsultaSQL("SELECT * FROM TIPODOCUMENTO WHERE ID_DOCUMENTO=" + IdDoc.ToString());

                if (TabDoc.HasRows)
                {
                    TabDoc.Read();

                    if (decimal.Parse(TabDoc["TxAdm"].ToString()) > 0)
                    {
                        decimal VlrRet = Vlr;
                        VlrRet = Vlr - ((Vlr * decimal.Parse(TabDoc["TxAdm"].ToString())) / 100);
                        return VlrRet;
                    }
                }                    
            }
            return Vlr;
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
            {
                MessageBox.Show("Usuário não tem acesso para alterar o financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (GridDados.CurrentRow != null)
            {
                Financ.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                Financ.LerDados(Financ.IdLanc);
                if (Financ.Status == 1)
                    MessageBox.Show("Lançamento já Baixado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (MessageBox.Show("Confirma a Exclusão do Registro", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Financ.IdLanc = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        Financ.Excluir();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, Financ.IdLanc, Financ.NumDoc, 3, "Exclusão do Lançamento");
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
            TxtCodigo.Text                = "0";
            Rb_Pagar.Checked              = true;
            TxtDataLanc.Value             = DateTime.Now;
            TxtNumDoc.Text                = "";
            TxtReferente.Text             = "";
            TxtNotaFiscal.Text            = "";
            TxtVlrOriginal.Value          = 0;
            TxtVlrAtual.Value             = 0;
            TxtVencimento.Value           = DateTime.Now;
            TxtObservacao.Text            = "";
            TxtCodPessoa.Text             = "0";
            TxtPessoa.Text                = "";
            TxtIdVenda.Value              = 0;
            LstFilial.SelectedValue       = 0;
            LstCusto.SelectedValue        = 0;
            LstDepartamento.SelectedValue = 0;
            LstTipoDoc.SelectedValue      = 0;
            LstVendedor.SelectedValue     = 0;
            LstBanco.SelectedValue        = 0;
            LstCaixa.SelectedValue        = 0;
            LstFormaPgto.SelectedValue    = 0;
            LstAgente.SelectedValue       = 0;
            LstCaixaPgto.SelectedValue    = 0;
            Cb_Inativa.Checked            = false;
            Cb_Perdido.Checked            = false;
            BoxTipoLanc.Enabled           = true;
            Ck_DocRec.Checked             = false;
            Rb_Despesa.Checked            = false;
            Rb_Fornecedor.Checked         = true;
            Financ.LerDados(0);
            PopularGridItens();
            Hab_Botoes();
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
                    Hab_Botoes();
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
        private void Hab_Botoes()
        {
            BoxBaixa.Enabled = false;
            BtnEnviaEmail.Visible  = Financ.Status == 0 && Financ.IdLanc > 0 && !FrmPrincipal.VersaoDistribuidor;
            BtnEnviaEmails.Visible =  !FrmPrincipal.VersaoDistribuidor;
            BtnReplicar.Visible   = Financ.IdLanc > 0;

            if (StaFormEdicao || Financ.IdLanc == 0)
            {
                BoxBaixa.Enabled    = false;
                BtnBaixar.Enabled   = false;
                BtnCancelar.Enabled = false;
                BtnConcluir.Enabled = false;
            }
            else 
            {                
                BtnBaixar.Enabled   = Financ.Status == 0;
                BtnCancelar.Enabled = Financ.Status == 1;
                BtnConcluir.Enabled = Financ.Status == 0;
            }
        }
        private void BtnBaixar_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
            {
                MessageBox.Show("Usuário não tem acesso para alterar o financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Financ.IdLanc == 0)
                return;

            if (Financ.PagRec == 1)
            {
                decimal Total = 0;
                for (int I = 0; I <= GridItens.RowCount - 1; I++)
                    Total = Total + decimal.Parse(GridItens.Rows[I].Cells[3].Value.ToString());

                if (Financ.VlrOriginal!=Total)
                    MessageBox.Show("Valor Total Diferente do Demonstrativo Financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            if (Verif_Pendencia(Financ.IdMov))
                return;

            if (!StaFormEdicao)
            {
                BoxBaixa.Enabled  = true;
                TxtDtBaixa.Value  = DateTime.Now;
                TxtVlrBaixa.Value = Financ.VlrOriginal;
                TxtVlrMulta.Focus();                
            }
        }

        private bool Verif_Pendencia(int IdMov)
        {
            if (IdMov > 0)
            {
                SqlDataReader TabEstoque;
                TabEstoque = Controle.ConsultaSQL("SELECT PENDENCIA,OBSPENDENCIA FROM MVESTOQUE WHERE ID_MOV=" + IdMov.ToString());
                if (TabEstoque.HasRows)
                {
                    TabEstoque.Read();
                    if (int.Parse(TabEstoque["PENDENCIA"].ToString()) == 1)
                    {
                        MessageBox.Show("Titulo com Pendência no Estoque: " + TabEstoque["OBSPENDENCIA"].ToString().Trim(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }
                    
                }
                return false;
            }
            else
                return false;
        }
        private void BtnConfirmarBaixa_Click(object sender, EventArgs e)
        {
            if (TxtDtBaixa.Value.Date < Financ.DataLanc.Date)            
                MessageBox.Show("Data da Baixa menor que a do lançamento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (int.Parse(LstCaixa.SelectedValue.ToString()) == 0)
                MessageBox.Show("Favor informar o Caixa Financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (int.Parse(LstTipoDoc.SelectedValue.ToString()) == 0)
                MessageBox.Show("Favor informar o Documento Financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (int.Parse(LstAgente.SelectedValue.ToString()) == 0)
                MessageBox.Show("Favor informar o Agente Cobrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (int.Parse(LstTipoDoc.SelectedValue.ToString()) > 0 && int.Parse(LstCaixa.SelectedValue.ToString()) > 0)
            {
                if (MvLivroCx.StatusLivroCxa(int.Parse(LstCaixa.SelectedValue.ToString()),TxtDtBaixa.Value) == 1)
                {
                    MessageBox.Show("Atenção: Data do Livro Caixa encerrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (MessageBox.Show("Confirma a baixa do titulo ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Financ.Status = 1;
                Financ.DtBaixa = TxtDtBaixa.Value;
                Financ.VlrMulta = TxtVlrMulta.Value;
                Financ.VlrJuro = TxtVlrJuros.Value;
                Financ.VlrDesconto = TxtVlrDesconto.Value;
                Financ.VlrBaixa = TxtVlrBaixa.Value;
                Financ.IdUsuBaixa = FrmPrincipal.Perfil_Usuario.IdUsuario;
                Financ.IdCaixa = int.Parse(LstCaixa.SelectedValue.ToString());
                Financ.IdTipoDocumento = int.Parse(LstTipoDoc.SelectedValue.ToString());
                Financ.IdAgente        = int.Parse(LstAgente.SelectedValue.ToString());
                StaFormEdicao = false;
                if ((Financ.VlrBaixa + Financ.VlrDesconto) < Financ.VlrOriginal)
                {
                    Financeiro NovoLanc = new Financeiro();
                    NovoLanc.Controle = Controle;
                    NovoLanc.LerDados(0);
                    if (NovoLanc.IdLancOrigem > 0)
                        NovoLanc.IdLancOrigem = Financ.IdLancOrigem;
                    else
                        NovoLanc.IdLancOrigem = Financ.IdLanc;
                    NovoLanc.IdLanc = 0;
                    NovoLanc.DataLanc = DateTime.Now;
                    NovoLanc.PagRec = Financ.PagRec;
                    NovoLanc.IdMov = Financ.IdMov;
                    NovoLanc.IdVenda = Financ.IdVenda;
                    NovoLanc.IdPessoa = Financ.IdPessoa;
                    NovoLanc.IdFilial = Financ.IdFilial;
                    NovoLanc.IdTipoDocumento = Financ.IdTipoDocumento;
                    NovoLanc.IdAgente = Financ.IdAgente;
                    NovoLanc.IdCusto = Financ.IdCusto;
                    NovoLanc.IdDepartamento = Financ.IdDepartamento;
                    NovoLanc.IdVendedor = Financ.IdVendedor;
                    NovoLanc.IdFormaPgto = Financ.IdFormaPgto;
                    NovoLanc.IdBanco = Financ.IdBanco;
                    NovoLanc.IdCaixa = Financ.IdCaixa;
                    NovoLanc.NumDoc = Financ.NumDoc;
                    NovoLanc.NotaFiscal = Financ.NotaFiscal;
                    NovoLanc.Referente = "Pagamento Parcial Lanç. Origem:" + Financ.IdLanc.ToString();
                    NovoLanc.Vencimento = Financ.Vencimento;
                    NovoLanc.VlrOriginal = Financ.VlrOriginal - Financ.VlrBaixa;
                    NovoLanc.VlrAtual = NovoLanc.VlrOriginal;
                    NovoLanc.VlrJuro = 0;
                    NovoLanc.VlrMulta = 0;
                    NovoLanc.VlrDesconto = 0;
                    NovoLanc.VlrBaixa = 0;
                    NovoLanc.Status = 0;
                    NovoLanc.Observacao = Financ.Observacao;
                    NovoLanc.GravarDados();
                    MessageBox.Show("Baixa identificada como [Pagamento Parcial]", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Financ.Baixar();
                //Registrando Movimento de Auditoria
                FrmPrincipal.RegistrarAuditoria(this.Text, Financ.IdLanc, Financ.NumDoc, 5, "Baixa do Lançamento");
                PopularCampos(Financ.IdLanc);
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                PopularCampos(Financ.IdLanc);            
            }
        }        
        private void BtnCancBaixa_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
            {
                MessageBox.Show("Usuário não tem acesso para alterar o financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!StaFormEdicao)
            {                
                if (MvLivroCx.StatusLivroCxa(Financ.IdCaixa, Financ.DtBaixa) == 1)
                {
                    MessageBox.Show("Atenção: Data do Livro Caixa encerrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Confirma o Cancelamento do Movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Financ.CancelarBaixa();
                    //Registrando Movimento de Auditoria
                    FrmPrincipal.RegistrarAuditoria(this.Text, Financ.IdLanc, Financ.NumDoc, 4, "Cancelamento da Baixa");
                    PopularCampos(Financ.IdLanc);
                    FrmPrincipal.ControleBotoes(false);
                }
            }
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            
            if (Rb_TpTodos.Checked)
            {
                MessageBox.Show("Favor marcar um tipo de lançamentos (À Pagar ou À Receber)", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string Filtro = "";
            PopularGrid();
            if (TxtPesqNumDoc.Text.Trim() != "" || TxtPesqPessoa.Text.Trim()!="")
            {
                Filtro = TxtPesqNumDoc.Text.Trim() + " " + TxtPesqPessoa.Text.Trim();
            }
            else
            {
                Filtro = "De:" + Dt1.Value.Date.ToShortDateString() + " a " + Dt2.Value.Date.ToShortDateString();
            }

            if (Rb_TpPagar.Checked)
                Filtro = Filtro + " À Pagar ";
            else
                Filtro = Filtro + " À Receber ";
            
            if (TxtPesqNF.Text.Trim() != "")
                Filtro = Filtro + " Nota Fiscal:" + TxtPesqNF.Text.Trim();                        
            if (int.Parse(LstPesqFilial.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Filial:" + LstPesqFilial.Text.Trim();
            if (int.Parse(LstPesqCusto.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Custo:" + LstPesqCusto.Text.Trim();
            if (int.Parse(LstPesqTipoDoc.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Tipo Documento:" + LstPesqTipoDoc.Text.Trim();
            if (int.Parse(LstPesqCaixa.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Caixa:" + LstPesqCaixa.Text.Trim();
            if (int.Parse(LstPesqBanco.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Banco:" + LstPesqBanco.Text.Trim();
            if (int.Parse(LstPesqFormaPgto.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Forma Pgto:" + LstPesqFormaPgto.Text.Trim();
            if (int.Parse(LstPesqVendedor.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Vendedor:" + LstPesqVendedor.Text.Trim();
            if (int.Parse(LstPesqDepart.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Departamento:" + LstPesqDepart.Text.Trim();
            if (int.Parse(LstPesqAgente.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Ag.Cobrador:" + LstPesqAgente.Text.Trim();
            if (LstPesqTipoMov.SelectedValue.ToString() != "0")
                Filtro = Filtro + " Movimento:" + LstPesqTipoMov.Text.Trim();
            if (int.Parse(LstUsuario.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " Usuário:" + LstUsuario.Text.Trim();
            if (Cb_PesInativa.Checked)
                Filtro = Filtro + " Lanç. INATIVOS";

            if (LstTipoCadastro.SelectedIndex > 0)
            {
                if (LstTipoCadastro.SelectedIndex - 1 == 0)
                    Filtro = Filtro + " Tipo de Pessoa: Cliente";
                else if (LstTipoCadastro.SelectedIndex - 1 == 1)
                    Filtro = Filtro + " Tipo de Pessoa: Fornecedor";
                else if (LstTipoCadastro.SelectedIndex - 1 == 2)
                    Filtro = Filtro + " Tipo de Pessoa: Cliente e Fornecedor";
                else if (LstTipoCadastro.SelectedIndex - 1 == 3)
                    Filtro = Filtro + " Tipo de Pessoa: Distribuidor";            
            }            
            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelFinanceiro RelFinac = new Relatorios.RelFinanceiro();
            DataSet TabRel = new DataSet();
            if (Cb_PesInativa.Checked)
                TabRel = Controle.ConsultaTabela(SqlConsulta + " AND INATIVA=1 ORDER BY T1.PAGREC,T1.VENCIMENTO");
            else
                TabRel = Controle.ConsultaTabela(SqlConsulta + " AND (INATIVA=0 OR INATIVA IS NULL) ORDER BY T1.PAGREC,T1.VENCIMENTO");
            RelFinac.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelFinac;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelFinac.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelFinac.Section2.ReportObjects["LblFiltro"])).Text = Filtro;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(RelFinac.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;
        }
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                if (Financ.Status == 1)
                    MessageBox.Show("Lançamento já Baixado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                    BuscaPessoa.FrmPrincipal = this.FrmPrincipal;                    
                    BuscaPessoa.ShowDialog();
                    if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                    {
                        SetaPessoa(BuscaPessoa.CadPessoa.IdPessoa);
                        if (Financ.IdLanc == 0)
                        {
                            LstCusto.SelectedValue        = BuscaPessoa.CadPessoa.IdCusto.ToString();
                            LstDepartamento.SelectedValue = BuscaPessoa.CadPessoa.IdDepartamento.ToString();
                            LstFilial.SelectedValue       = BuscaPessoa.CadPessoa.IdFilial.ToString();                            
                        }
                    }
                }
            }
        }
        private void SetaPessoa(int IdPessoa)
        {
            CadPessoa.LerDados(IdPessoa);
            Financ.IdPessoa   = CadPessoa.IdPessoa;
            TxtCodPessoa.Text = CadPessoa.IdPessoa.ToString();
            TxtPessoa.Text    = CadPessoa.RazaoSocial.Trim();
        }        
        private void TxtVlrDesconto_Validated(object sender, EventArgs e)
        {
            TxtVlrBaixa.Value = (TxtVlrOriginal.Value + TxtVlrMulta.Value + TxtVlrJuros.Value) - TxtVlrDesconto.Value;
        }        
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /* if (e.ColumnIndex == 1)
             {
                 if (e.Value.ToString().Trim() == "Confirmado")
                     GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Silver;
             }*/
        }
        private void BtnBaixaAut_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
            {
                MessageBox.Show("Usuário não tem acesso para alterar o financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FrmBaixaAutFinan FrmBxa = new FrmBaixaAutFinan();
            FrmBxa.FrmPrincipal = FrmPrincipal;
            FrmBxa.ShowDialog();
            if (FrmBxa.Baixar)
            {
                if (int.Parse(FrmBxa.LstCaixa.SelectedValue.ToString()) > 0 && int.Parse(FrmBxa.LstTipoDoc.SelectedValue.ToString()) > 0)
                {
                    if (MvLivroCx.StatusLivroCxa(int.Parse(FrmBxa.LstCaixa.SelectedValue.ToString()), DateTime.Now) == 1)
                    {
                        MessageBox.Show("Atenção: Data do Livro Caixa encerrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                BtnBaixaAut.Enabled = false;
                FrmPrincipal.BSta_BarProcesso.Maximum = GridDados.Rows.Count - 1;
                for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                {                    
                    Financ.LerDados(int.Parse(GridDados.Rows[I].Cells[0].Value.ToString()));
                    if (Financ.IdLanc > 0 && Financ.Status == 0)
                    {
                        Financ.Status          = 1;
                        Financ.DtBaixa         = DateTime.Now;
                        Financ.VlrMulta        = 0;
                        Financ.VlrJuro         = 0;
                        Financ.VlrDesconto     = 0;
                        Financ.VlrBaixa        = Financ.VlrOriginal;
                        Financ.IdUsuBaixa      = FrmPrincipal.Perfil_Usuario.IdUsuario;
                        if (int.Parse(FrmBxa.LstCaixa.SelectedValue.ToString()) > 0)
                            Financ.IdCaixa = int.Parse(FrmBxa.LstCaixa.SelectedValue.ToString());
                        if (int.Parse(FrmBxa.LstTipoDoc.SelectedValue.ToString()) > 0)
                            Financ.IdTipoDocumento = int.Parse(FrmBxa.LstTipoDoc.SelectedValue.ToString());
                        if (int.Parse(FrmBxa.LstAgente.SelectedValue.ToString()) > 0)
                            Financ.IdAgente = int.Parse(FrmBxa.LstAgente.SelectedValue.ToString());
                        StaFormEdicao = false;
                        Financ.Baixar();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, Financ.IdLanc, Financ.NumDoc, 5, "Baixa Automatica do Lançamento");
                        if (FrmPrincipal.BSta_BarProcesso.Value < FrmPrincipal.BSta_BarProcesso.Maximum)
                            FrmPrincipal.BSta_BarProcesso.Value = FrmPrincipal.BSta_BarProcesso.Value + 1;
                    }
                }
                MessageBox.Show("Titulos Baixados", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopularGrid();
                BtnBaixaAut.Enabled = true;            
            }
        }
        private void GridDados_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MaskedTextBox MaskCol = new MaskedTextBox();            
            if (e.ColumnIndex == 6)
            {
                MaskCol.Mask = "(00) 0000-0000";
                MaskCol.Text = e.Value.ToString();
                e.Value = MaskCol.Text;
            }
            MaskCol.Dispose();
        }
        private void Rb_Pagar_CheckedChanged(object sender, EventArgs e)
        {
            LstCusto = FrmPrincipal.PopularCombo("SELECT T2.ID_CUSTO,'('+RTRIM(T1.GRUPO)+') <=> '+RTRIM(T2.CUSTO) FROM GRUPOCCUSTO T1 LEFT JOIN CENTROCUSTO T2 ON (T2.ID_GRPCUSTO=T1.ID_GRPCUSTO) WHERE T1.TIPO=1 ORDER BY T1.TIPO,T1.GRUPO,T2.CUSTO", LstCusto);
            Ck_DocRec.Visible    = true;
            Box_DespForn.Visible = true;
        }
        private void Rb_Receber_CheckedChanged(object sender, EventArgs e)
        {
            LstCusto = FrmPrincipal.PopularCombo("SELECT T2.ID_CUSTO,'('+RTRIM(T1.GRUPO)+') <=> '+RTRIM(T2.CUSTO) FROM GRUPOCCUSTO T1 LEFT JOIN CENTROCUSTO T2 ON (T2.ID_GRPCUSTO=T1.ID_GRPCUSTO) WHERE T1.TIPO=0 ORDER BY T1.TIPO,T1.GRUPO,T2.CUSTO", LstCusto);
            Ck_DocRec.Visible    = false;
            Box_DespForn.Visible = false;
        }

        private void BtnEnviaEmail_Click(object sender, EventArgs e)
        {
            if (CadPessoa.Email.Trim() == "")
            {
                FrmAtlzCadPessoa FrmAtlz = new FrmAtlzCadPessoa();
                FrmAtlz.FrmPrincipal     = FrmPrincipal;
                FrmAtlz.IdPessoa         = CadPessoa.IdPessoa;
                FrmAtlz.CadPessoa        = CadPessoa;
                FrmAtlz.ShowDialog();
            }
            CadPessoa.LerDados(Financ.IdPessoa);
            if (CadPessoa.Email.Trim() == "")
            {
                MessageBox.Show("Email não informado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string texto = MensagemEmailCobranca();            
            FrmEnviarEmail FrmEmail = new FrmEnviarEmail();
            FrmEmail.FrmPrincipal   = FrmPrincipal;
            FrmEmail.destino        = CadPessoa.Email;
            FrmEmail.copia          = "";
            FrmEmail.assunto        = "Titulo em Aberto";
            FrmEmail.texto          = texto;            
            FrmEmail.ShowDialog();
        }

        private string MensagemEmailCobranca()
        {
            string texto = CadPessoa.RazaoSocial.Trim() + " \r\n";
            texto = texto + " \r\n Consta em nosso sistema até a presente data, o título em aberto a baixo descriminado:" + " \r\n" + " \r\n";
            texto = texto + "Vencimento: " + Financ.Vencimento.ToShortDateString() + " \r\n";
            texto = texto + "Valor: " + string.Format("{0:N2}", Financ.VlrOriginal) + " \r\n" + " \r\n";
            texto = texto + "Solicitamos, por gentileza, que entrem em contato conosco. Caso o titulo já tenha sido liquidado, favor informar a data e a forma de liquidação." + " \r\n" + " \r\n" + " \r\n";
            texto = texto + LstFilial.Text.Trim() + " \r\n";
            texto = texto + "Setor Financeiro" + " \r\n";
            texto = texto + "(85) 3260-0550 / (85) 9.9202-4475";
            return texto;
        }
        private void BtnReplicar_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
            {
                MessageBox.Show("Usuário não tem acesso para alterar o financeiro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FrmReplicarFinanc Frm = new FrmReplicarFinanc();
            Frm.FrmPrincipal = FrmPrincipal;
            Frm.LancFinanc   = Financ;
            Frm.ShowDialog();

        }

        //Tab Aux Financeiro
        //Comissão fixa por Produto
        private void PopularGridItens()
        {
            TabItens = Controle.ConsultaTabela("SELECT ID_ITEM,ID_FILIAL,ID_CUSTO,VALOR FROM TABAUXFINANCEIRO WHERE ID_LANC=" + Financ.IdLanc.ToString());
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("Id_Item", TabAuxFinanc.IdItem);
            Source_Itens.Position = item;

            decimal Total = 0;
            for (int I = 0; I <= GridItens.RowCount - 1; I++)
                Total = Total + decimal.Parse(GridItens.Rows[I].Cells[3].Value.ToString());

            LblTotalFin.Text = string.Format("{0:N2}", Total);
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
                MessageBox.Show("Lançamento em Edição, favor gravar o registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro== 0)
            {
                MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (Financ.IdLanc == 0)
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
                if (Financ.IdLanc> 0)
                {                    
                    TabAuxFinanc.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    TabAuxFinanc.IdLanc   = Financ.IdLanc;
                    TabAuxFinanc.IdFilial = int.Parse(GridItens.CurrentRow.Cells[1].Value.ToString());
                    TabAuxFinanc.IdCusto  = int.Parse(GridItens.CurrentRow.Cells[2].Value.ToString());
                    TabAuxFinanc.Valor    = decimal.Parse(GridItens.CurrentRow.Cells[3].Value.ToString());
                    TabAuxFinanc.GravarDados();
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Lançamento em Edição, favor gravar o registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Financ.IdLanc > 0)
                    IncluirItem();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (GridItens.CurrentRow == null)
                return;
            
            if (StaFormEdicao)
                MessageBox.Show("Lançamento em Edição, favor gravar o registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Financ.IdLanc > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            TabAuxFinanc.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                            TabAuxFinanc.Excluir();
                            TabAuxFinanc.IdItem = 0;
                            PopularGridItens();
                        }
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Lançamento em Edição, favor gravar o registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (Financ.IdLanc > 0)
                {
                    if (FrmPrincipal.Perfil_Usuario.AlteraFinanceiro == 0)
                        MessageBox.Show("Usuário não Autorizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        TabAuxFinanc.LerDados(0);
                        TabAuxFinanc.IdLanc   = Financ.IdLanc;
                        TabAuxFinanc.IdFilial = Financ.IdFilial;
                        TabAuxFinanc.GravarDados();
                        PopularGridItens();
                        GridItens.CurrentCell = GridItens.CurrentRow.Cells[1];
                    }
                }
            }
        }

        private void Rb_TpPagar_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Rb_TpPagar_Click(object sender, EventArgs e)
        {
            Box_PesqDesp.Visible = true;
        }

        private void Rb_TpReceb_Click(object sender, EventArgs e)
        {
            Box_PesqDesp.Visible = false;
        }

        private void Rb_TpTodos_Click(object sender, EventArgs e)
        {
            Box_PesqDesp.Visible = false;
        }

        private void GridDados_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
                if (GridDados.Rows[e.RowIndex].Cells[20].Value.ToString() == "1")
                    GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void BtnEnviaEmails_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma o Envio do Email de Cobrança ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FrmEnviarEmail FrmEmail = new FrmEnviarEmail();
                FrmEmail.FrmPrincipal = FrmPrincipal;

                FrmPrincipal.BSta_BarProcesso.Maximum = GridDados.RowCount;
                FrmPrincipal.BSta_BarProcesso.Value = 0;
                
                for (int I = 0; I <= GridDados.RowCount - 1; I++)
                {
                    Financ.LerDados(int.Parse(GridDados.Rows[I].Cells[0].Value.ToString()));
                    LstFilial.SelectedValue = Financ.IdFilial.ToString();
                    if (Financ.PagRec == 2)
                    {
                        CadPessoa.LerDados(Financ.IdPessoa);
                        if (CadPessoa.Email.Trim() == "")
                        {
                            MessageBox.Show("Email não Cadastrado do Cliente: " + CadPessoa.RazaoSocial.Trim(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string texto = MensagemEmailCobranca();
                            FrmEmail.txtEmailDest.Text = CadPessoa.Email;
                            FrmEmail.txtCopia.Text     = "";
                            FrmEmail.txtAssunto.Text   = "Grupo Talimpo Titulo em Aberto";
                            FrmEmail.txtTexto.Text     = texto;
                            FrmEmail.EnviarEmail();
                        }
                        FrmPrincipal.BSta_BarProcesso.Value = FrmPrincipal.BSta_BarProcesso.Value + 1;
                    }                    
                }
                MessageBox.Show("Email Enviado");
                FrmEmail.Dispose();
            }
        }

        private void BtnCobranca_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                Financ.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                
                if (Financ.PagRec == 2 && Financ.IdLanc > 0)
                {
                    FrmRegCobranca Frm = new FrmRegCobranca();
                    Frm.FrmPrincipal   = FrmPrincipal;
                    Frm.IdPagRec       = Financ.IdLanc;
                    Frm.ShowDialog();
                }

            }
        }

        private void BtnRegCob_Click(object sender, EventArgs e)
        {
            if (Financ.PagRec == 2 && Financ.IdLanc > 0)
            {
                FrmRegCobranca Frm = new FrmRegCobranca();
                Frm.FrmPrincipal = FrmPrincipal;
                Frm.IdPagRec = Financ.IdLanc;
                Frm.ShowDialog();
            }

        }

             
    }
}
