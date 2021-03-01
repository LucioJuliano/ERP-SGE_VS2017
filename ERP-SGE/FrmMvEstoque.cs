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
    public partial class FrmMvEstoque : Form
    {
        Funcoes Controle = new Funcoes();
        MvEstoque MovEstoque = new MvEstoque();
        MvEstoqueItens ItemMvEstoque = new MvEstoqueItens();
        Pessoas CadPessoa = new Pessoas();
        TabelasAux TabAux = new TabelasAux();
        Auditoria RegAuditoria = new Auditoria();
        Filiais CadFilial = new Filiais();


        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;
        private int IdCFOPAnt = 0;
        private int CSTAnt    = 0;
        // Tabelas
        private DataSet TabItens;
        private BindingSource Source_Itens;
        public FrmMvEstoque()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles
            CamposLista();
            Controle.Conexao       = FrmPrincipal.Conexao;
            MovEstoque.Controle    = Controle;
            ItemMvEstoque.Controle = Controle;
            CadPessoa.Controle     = Controle;
            TabAux.Controle        = Controle;
            RegAuditoria.Controle  = Controle;
            CadFilial.Controle     = Controle;
            MovEstoque.IdMov = 0;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            Rb_Pendente.Checked = true;
            Chk_Periodo.Checked = false;
            PopularGrid();
            // Instanciando as Tabelas
            TabItens = new DataSet();
            Source_Itens = new BindingSource();
            CadFilial.LerDados(FrmPrincipal.IdFilialConexao);
        }
        private void CamposLista()
        {
            LstPesqTpMv   = FrmPrincipal.PopularCombo("SELECT CHAVE,SUBSTRING(DESCRICAO,1,30) AS DESCRICAO FROM TABELASAUX  WHERE CAMPO='TPMVEST' AND EXISTS (SELECT * FROM ACESSOUSUARIO WHERE OPCAO='OP_'+RTRIM(CHAVE)  AND ACESSO=1 AND ID_USUARIO="+FrmPrincipal.Perfil_Usuario.IdUsuario.ToString()+") ORDER BY DESCRICAO", LstPesqTpMv);
            LstTipMov     = FrmPrincipal.PopularCombo("SELECT CHAVE,SUBSTRING(DESCRICAO,1,30) AS DESCRICAO FROM TABELASAUX  WHERE CAMPO='TPMVEST' AND EXISTS (SELECT * FROM ACESSOUSUARIO WHERE OPCAO='OP_'+RTRIM(CHAVE ) AND ACESSO=1 AND ID_USUARIO=" + FrmPrincipal.Perfil_Usuario.IdUsuario.ToString() + ") ORDER BY DESCRICAO", LstTipMov);
            LstFilial     = FrmPrincipal.PopularCombo("SELECT ID_FILIAL,SUBSTRING(FANTASIA,1,40) AS FILIAL FROM EMPRESA_FILIAL", LstFilial);
            LstFilial     = FrmPrincipal.PopularCombo("SELECT ID_FILIAL,SUBSTRING(FANTASIA,1,40) AS FILIAL FROM EMPRESA_FILIAL", LstFilial);
            LstCFOPItem   = FrmPrincipal.PopularComboGrid("SELECT Id_Cfop,Cfop+' '+Natureza FROM Cfop where substring(cfop,1,1) in (1,2) ORDER BY Cfop", LstCFOPItem);

            //Atualizando a Lista CST
            DataTable TabCST = Controle.LstCST();
            ColCST.DataSource    = TabCST;
            ColCST.DisplayMember = "DescCST";
            ColCST.ValueMember   = "CST";
        }        
        private void PopularGrid()
        {
            string Filtro = "";
            string Ordem = "ORDER BY T1.DATA DESC";
            if (Rb_Todos.Checked)
                Filtro = " WHERE T1.Status >= 0";
            else
            {
                if (Rb_Pendente.Checked)
                    Filtro = " WHERE T1.Status = 0";
                else
                    Filtro = " WHERE T1.Status = 1";
            }

            if (LstPesqTpMv.SelectedValue.ToString() != "0")
                Filtro = Filtro + " AND T1.TPMOV='" + LstPesqTpMv.SelectedValue.ToString() + "'";
            else
            {
                string LstMv = "";
                for (int I = 0; I <= LstPesqTpMv.Items.Count - 1; I++)
                {
                    DataRowView item = (DataRowView)LstPesqTpMv.Items[I];
                    {
                        if (LstMv == "")
                            LstMv = LstMv + "'" + item.Row[0].ToString() + "'";
                        else
                            LstMv = LstMv + ",'" + item.Row[0].ToString() + "'";
                    }
                }
                Filtro = Filtro + " AND T1.TPMOV in (" + LstMv + ")";
            }
            
            if (Chk_Periodo.Checked)
                Filtro = Filtro + " and T1.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqPessoa.Text.Trim() != "")
            {
                Filtro = Filtro + " and T3.RazaoSocial like '%" + TxtPesqPessoa.Text.Trim() + "%'";
                Ordem = " ORDER BY T3.RAZAOSOCIAL, T1.DATA DESC";
            }
            if (TxtPesNumPedido.Text.Trim() != "")
            {
                Filtro = Filtro + " and T1.NumPedido like '%" + TxtPesNumPedido.Text.Trim() + "%'";
                Ordem = " ORDER BY T1.NUMPEDIDO, T1.DATA DESC";
            }
            if (TxtPesqNumDoc.Text.Trim() != "")
            {
                Filtro = Filtro + " and T1.NumDocumento like '%" + TxtPesqNumDoc.Text.Trim() + "%'";
                Ordem = " ORDER BY T1.NUMPEDIDO, T1.DATA DESC";
            }
            if (TxtPesqForm.Text.Trim() != "")
            {
                Filtro = Filtro + " and T1.NumFormulario like '%" + TxtPesqForm.Text.Trim() + "%'";
                Ordem = " ORDER BY T1.NUMFORMULARIO, T1.DATA DESC";
            }
            if (TxtPesqMov.Text.Trim() != "")
            {
                Filtro = Filtro + " and T1.Id_Mov=" + TxtPesqMov.Text.Trim() ;
                Ordem = " ORDER BY T1.Id_Mov";
            }
            try
            {
                DataSet Tabela = new DataSet();
                //Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Mov,CASE T1.STATUS WHEN 0 THEN 'Pendente' WHEN 1 THEN 'Confirmado' END AS STATUS,CASE T1.CONFERIDO WHEN 0 THEN 'NÃO' WHEN 1 THEN 'SIM' END AS CONFERIDO,  T2.DESCRICAO AS TIPOMOV,T1.DTENTSAI,T3.RAZAOSOCIAL AS PESSOA,T1.DOCUMENTO,T1.NUMDOCUMENTO,T4.NUMPEDIDO,T1.VLRTOTAL" +
                Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Mov,CASE T1.STATUS WHEN 0 THEN 'Pendente' WHEN 1 THEN 'Confirmado' END AS STATUS, T1.CONFERIDO,T2.DESCRICAO AS TIPOMOV,T3.RAZAOSOCIAL AS PESSOA," +
                                                               "T5.FANTASIA AS NMFILIAL,T1.DTENTSAI,T1.DOCUMENTO,T1.NUMDOCUMENTO,T4.NUMPEDIDO,T1.VLRTOTAL,T6.Usuario" +
                                                               "   FROM MVESTOQUE T1 " +
                                                               " LEFT JOIN TABELASAUX T2 ON (T2.CAMPO='TPMVEST' AND T2.CHAVE=T1.TPMOV)" +
                                                               " LEFT JOIN PESSOAS    T3 ON (T3.ID_PESSOA=T1.ID_PESSOA)" +
                                                               " LEFT JOIN PEDCOMPRA  T4 ON (T4.Id_Documento=T1.Id_PedCompra) " +
                                                               " LEFT JOIN EMPRESA_FILIAL T5 ON (T5.Id_Filial=T1.Id_FilialOrigDest)  " +
                                                               " LEFT JOIN USUARIOS  T6 ON (T6.Id_Usuario=T1.Id_Usuario) " + Filtro) + " ORDER BY T1.DATA,T1.ID_MOV DESC");
                BindingSource Source = new BindingSource();
                Source.DataSource = Tabela;
                Source.DataMember = Tabela.Tables[0].TableName;
                GridDados.DataSource = Source;
                int item = Source.Find("Id_Mov", MovEstoque.IdMov);
                Source.Position = item;
            }
            catch
            {
                MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopularCampos(int Isn)
        {
            //Paginas.SelectTab(1);
            BoxPesquisa.Enabled = true;
            MovEstoque.LerDados(Isn);
            TxtCodigo.Text              = MovEstoque.IdMov.ToString();
            TxtData.Value               = MovEstoque.Data;
            TxtDocumento.Text           = MovEstoque.Documento;
            TxtNumDocumento.Text        = MovEstoque.NumDocumento;
            TxtNumFormulario.Text       = MovEstoque.NumFormulario;
            LstFilial.SelectedValue     = MovEstoque.IdFilialOrigDest.ToString();
            LstTipMov.SelectedValue     = MovEstoque.TpMov;
            TxtSolicitante.Text         = MovEstoque.Solicitante;
            TxtAutorizado.Text          = MovEstoque.Autorizado;
            TxtDtEntSai.Value           = MovEstoque.DtEntSai;
            TxtDtEmissao.Value          = MovEstoque.DtEmissao;
            TxtVlrSubTotal.Value        = MovEstoque.VlrSubTotal;
            TxtVlrDesconto.Value        = MovEstoque.VlrDesconto;
            TxtVlrTotal.Value           = MovEstoque.VlrTotal;
            TxtBIcms.Value              = MovEstoque.BIcms;
            TxtVlrIcms.Value            = MovEstoque.VlrIcms;
            TxtBIcmsSub.Value           = MovEstoque.BIcmsSub;
            TxtVlrIcmsSub.Value         = MovEstoque.VlrIcmsSub;
            TxtVlrFrete.Value           = MovEstoque.VlrFrete;
            TxtVlrSeguro.Value          = MovEstoque.VlrSeguro;
            TxtVlrOutraDesp.Value       = MovEstoque.VlrOutraDesp;            
            TxtVlrIpi.Value             = MovEstoque.VlrIpi;
            TxtObservacao.Text          = MovEstoque.Observacao;
            TxtObsSelo.Text             = MovEstoque.ObsSelo;
            TxtChaveNFE.Text            = MovEstoque.ChaveNFE;
            TxtNfeSerie.Text            = MovEstoque.NFeSerie;
            TxtObsPendencia.Text        = MovEstoque.ObsPendencia;
            Ck_Conferido.Enabled        = MovEstoque.Status    == 1;
            Ck_Conferido.Checked        = MovEstoque.Conferido == 1;
            LstPendAvaria.SelectedIndex = MovEstoque.TpAvaria;
            if (MovEstoque.TpFrete == 0)   Rb_CIF.Checked        = true; else Rb_FOB.Checked        = true;
            if (MovEstoque.TipoPgto == 0)  Rb_Avista.Checked     = true; else Rb_Aprazo.Checked     = true;
            if (MovEstoque.NtServico == 1) Cb_NtServico.Checked  = true; else Cb_NtServico.Checked  = false;
            if (MovEstoque.Pendencia == 1) Cb_PendAvaria.Checked = true; else Cb_PendAvaria.Checked = false;
            SetaPessoa(MovEstoque.IdPessoa);            
            Hab_Botoes();
        }
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            StaFormEdicao = true;
            Paginas.SelectTab(1);
            LimpaDados();
            PopularGridItens();
            FrmPrincipal.ControleBotoes(true);
            if (LstTipMov.SelectedValue.ToString() == "ENTNF")
                LstTipMov.SelectedValue = "ENTNF";
            TxtDocumento.Focus();
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow == null && MovEstoque.IdMov==0)
            {
                Paginas.SelectTab(0);
                MessageBox.Show("Não existe Registro para Edição", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (MovEstoque.IdMov != 0)
                    PopularCampos(MovEstoque.IdMov);
                else
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));                

                if (MovEstoque.Status == 1)
                    MessageBox.Show("Movimento já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                else
                {
                    PopularGridItens();
                    if (MovEstoque.TpMov == "E_TRA" && MovEstoque.IdMovChave > 0)
                    {
                        MessageBox.Show("Atenção: Transfência não pode ser modificada, solicite a filial origem para alterar", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }                    
                    StaFormEdicao = true;
                    FrmPrincipal.ControleBotoes(true);
                    TxtDocumento.Focus();
                    Hab_Botoes();
                }
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (LstTipMov.SelectedValue.ToString() != "0")
            {
                if (PnlValores.Visible && TxtObsSelo.Text.Trim() == "")
                    MessageBox.Show("Observações do Selo não informado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MovEstoque.IdMov            = int.Parse(TxtCodigo.Text);
                MovEstoque.Data             = TxtData.Value;
                MovEstoque.Documento        = TxtDocumento.Text;
                MovEstoque.NumDocumento     = TxtNumDocumento.Text;
                MovEstoque.NumFormulario    = TxtNumFormulario.Text;
                MovEstoque.IdFilialOrigDest = int.Parse(LstFilial.SelectedValue.ToString());                
                MovEstoque.TpMov            = LstTipMov.SelectedValue.ToString();
                MovEstoque.Solicitante      = TxtSolicitante.Text;
                MovEstoque.Autorizado       = TxtAutorizado.Text;
                MovEstoque.DtEntSai         = TxtDtEntSai.Value;
                MovEstoque.DtEmissao        = TxtDtEmissao.Value;
                MovEstoque.ChaveNFE         = TxtChaveNFE.Text.Trim();
                MovEstoque.VlrSubTotal      = TxtVlrSubTotal.Value;
                MovEstoque.VlrDesconto      = TxtVlrDesconto.Value;
                MovEstoque.VlrTotal         = TxtVlrTotal.Value;
                MovEstoque.BIcms            = TxtBIcms.Value;
                MovEstoque.VlrIcms          = TxtVlrIcms.Value;
                MovEstoque.BIcmsSub         = TxtBIcmsSub.Value;
                MovEstoque.VlrIcmsSub       = TxtVlrIcmsSub.Value;
                MovEstoque.VlrFrete         = TxtVlrFrete.Value;
                MovEstoque.VlrSeguro        = TxtVlrSeguro.Value;
                MovEstoque.VlrOutraDesp     = TxtVlrOutraDesp.Value;                
                MovEstoque.VlrIpi           = TxtVlrIpi.Value;
                MovEstoque.Observacao       = TxtObservacao.Text;
                MovEstoque.ObsSelo          = TxtObsSelo.Text;
                MovEstoque.NFeSerie         = TxtNfeSerie.Text;
                MovEstoque.ObsPendencia     = TxtObsPendencia.Text;

                if (Rb_CIF.Checked) MovEstoque.TpFrete         = 0; else MovEstoque.TpFrete   = 1;
                if (Rb_Avista.Checked) MovEstoque.TipoPgto     = 0; else MovEstoque.TipoPgto  = 1;
                if (Ck_Conferido.Checked) MovEstoque.Conferido = 1; else MovEstoque.Conferido = 0;
                if (Cb_NtServico.Checked) MovEstoque.NtServico = 1; else MovEstoque.NtServico = 0;

                MovEstoque.TpAvaria = LstPendAvaria.SelectedIndex;

                if (Cb_PendAvaria.Visible)
                {
                    if (Cb_PendAvaria.Checked) MovEstoque.Pendencia= 1; else MovEstoque.Pendencia = 0;
                }
                else
                    MovEstoque.Pendencia = 0;
                

                if (MovEstoque.IdUsuario == 0)
                    MovEstoque.IdUsuario = FrmPrincipal.Perfil_Usuario.IdUsuario;

                StaFormEdicao = false;

                if (TabAux.Estoque != 1)
                    MovEstoque.IdPedCompra = 0;

                MovEstoque.GravarDados();
                if (int.Parse(TxtCodigo.Text) == 0)
                    FrmPrincipal.RegistrarAuditoria(this.Text, MovEstoque.IdMov, MovEstoque.NumDocumento, 1, LstTipMov.Text.Trim());
                else
                    FrmPrincipal.RegistrarAuditoria(this.Text, MovEstoque.IdMov, MovEstoque.NumDocumento, 2, LstTipMov.Text.Trim());
                PopularGrid();
                PopularCampos(MovEstoque.IdMov);
                PopularGridItens();
                FrmPrincipal.ControleBotoes(false);
            }
            else
            {
                MessageBox.Show("Favor selecione o tipo movimento", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                MovEstoque.IdMov = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                MovEstoque.LerDados(MovEstoque.IdMov);

                if (MovEstoque.TpMov == "E_TRA" && MovEstoque.IdMovChave > 0)
                {
                    MessageBox.Show("Atenção: Transfência não pode ser Excluida, solicite a filial origem para Excluir", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MovEstoque.Status == 1)
                    MessageBox.Show("Movimento já Concluído, para poder excluir você deve cancelar o Movimento", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                
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
                        MovEstoque.IdMov = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        MovEstoque.Excluir();
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, MovEstoque.IdMov, MovEstoque.NumDocumento, 3, LstTipMov.Text.Trim());
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
            TxtCodigo.Text          = "0";
            TxtCodPessoa.Text       = "0";
            TxtFornecedor.Text      = "";
            TxtData.Value           = DateTime.Now;
            TxtDocumento.Text       = "";
            TxtNumDocumento.Text    = "";
            TxtNumFormulario.Text   = "";
            LstFilial.SelectedValue = 0;
            TxtSolicitante.Text     = ""; ;
            TxtAutorizado.Text      = "";
            TxtDtEntSai.Value       = DateTime.Now;
            Rb_CIF.Checked          = true;
            TxtVlrSubTotal.Value    = 0;
            TxtVlrDesconto.Value    = 0;
            TxtVlrTotal.Value       = 0;
            TxtBIcms.Value          = 0;
            TxtVlrIcms.Value        = 0;
            TxtBIcmsSub.Value       = 0;
            TxtVlrIcmsSub.Value     = 0;
            TxtVlrFrete.Value       = 0;
            TxtVlrSeguro.Value      = 0;
            TxtVlrOutraDesp.Value   = 0;            
            TxtVlrIpi.Value         = 0;
            TxtObservacao.Text      = "";
            TxtDtEmissao.Value      = DateTime.Now;
            TxtChaveNFE.Text        = "";
            TxtObsSelo.Text         = "";
            TxtNfeSerie.Text        = "1";
            TxtObsPendencia.Text    = "";
            LstPendAvaria.SelectedIndex = 0;
            Rb_Avista.Checked       = true;
            MovEstoque.LerDados(0);
            Ck_Conferido.Checked  = false;
            Ck_Conferido.Enabled  = false;
            Cb_NtServico.Checked  = false;
            Cb_PendAvaria.Checked = false;
            PopularGridItens();
            Hab_Botoes();
        }
        private void Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (GridDados.CurrentRow != null)
            {
                if (GridDados.CurrentRow.Cells[0].Value.ToString() != "")
                    BtnEditar_Click(FrmPrincipal.BtnEditar, null);
            }*/

        }
        private void Paginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Paginas.SelectedIndex == 0)
                PopularGrid();

            if (GridDados.CurrentRow != null)
            {
                if (Paginas.SelectedIndex == 0)
                {
                    BtnCancelar_Click(FrmPrincipal.BtnCancelar, null);
                }
                else
                {
                    PopularCampos(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                    PagCab.SelectTab(0);
                    PagItens.SelectTab(0);
                    PopularGridItens();
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
        // Controle dos Itens
        private void PopularGridItens()
        {
            if (MovEstoque.IdMov > 0)
                PopularCampos(MovEstoque.IdMov);

            TabItens = Controle.ConsultaTabela("SELECT T1.ID_ITEM,T2.REFERENCIA,T2.DESCRICAO,T1.NCM,T1.CodBarra,T1.QTDE,T1.VLRPRCCOMPRA,T1.VLRUNITARIO,T1.VLRDESCONTO,T1.VLRTOTAL,T1.P_ICMS,T1.P_IPI,T1.PercRed,T1.VlrFrete,T1.VlrIcms_Sub,IsNull(T1.ID_Cfop,0) as ID_Cfop,Isnull(T1.CST,0) as CST,T1.Lote,T1.Validade " +
                                               " FROM MvEstoqueItens T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.Id_Mov=" + MovEstoque.IdMov.ToString()+" order by t1.id_item");
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridItens.DataSource = Source_Itens;
            Navegador.BindingSource = Source_Itens;
            int item = Source_Itens.Find("ID_Item", ItemMvEstoque.IdItem);
            Source_Itens.Position = item;
            Hab_Botoes();            
            //Atualizando o Total do Movimento
            TotalMovimento();
        }

        private void TotalMovimento()
        {
            decimal VlrSubTotal = 0;
            for (int I = 0; I <= GridItens.RowCount - 1; I++)
            {
                VlrSubTotal = VlrSubTotal + decimal.Parse(GridItens.Rows[I].Cells[9].Value.ToString());
            }
            MovEstoque.VlrSubTotal = VlrSubTotal;
            MovEstoque.VlrTotal    = (VlrSubTotal - MovEstoque.VlrDesconto);
            TxtVlrSubTotal.Value   = VlrSubTotal;
            TxtVlrTotal.Value      = MovEstoque.VlrTotal;
            if (MovEstoque.IdMov > 0)
            {
                MovEstoque.GravarDados();
                PopularCampos(MovEstoque.IdMov);
            }
        }
        private void GridItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridItens.CurrentRow == null || GridItens.Rows.Count - 1 == GridItens.CurrentRow.Index)
                {
                    // e.SuppressKeyPress = true;
                    IncluirItem();
                }
            }
        }
        private void GridItens_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (StaFormEdicao)
            {
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Source_Itens.CancelEdit();
                e.Cancel = true;
            }
            else
            {
                if (MovEstoque.TpMov == "E_TRA" && MovEstoque.IdMovChave > 0)
                {
                    MessageBox.Show("Atenção: Transfência não pode ser modificada, solicite a filial origem para alterar", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                    return;
                }

                if (MovEstoque.Status == 1)
                {
                    MessageBox.Show("Movimento já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                    e.Cancel = true;
                }
            }
        }
        private void GridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MovEstoque.IdMov > 0 && !StaFormEdicao)
            {
                decimal Qtde         = decimal.Parse(GridItens.CurrentRow.Cells[5].Value.ToString());
                decimal VlrComp      = decimal.Parse(GridItens.CurrentRow.Cells[6].Value.ToString());
                decimal VlrUnt       = decimal.Parse(GridItens.CurrentRow.Cells[7].Value.ToString());
                decimal VlrDesc      = decimal.Parse(GridItens.CurrentRow.Cells[8].Value.ToString());
                decimal PIcms        = decimal.Parse(GridItens.CurrentRow.Cells[10].Value.ToString());
                decimal PIpi         = decimal.Parse(GridItens.CurrentRow.Cells[11].Value.ToString());
                decimal PReducao     = decimal.Parse(GridItens.CurrentRow.Cells[12].Value.ToString());
                decimal VlrFrete     = decimal.Parse(GridItens.CurrentRow.Cells[13].Value.ToString());
                decimal VlrIcms_Sub  = decimal.Parse(GridItens.CurrentRow.Cells[14].Value.ToString());

                if (VlrDesc > (Qtde * VlrUnt))
                {
                    MessageBox.Show("Valor do desconto maior que o total do item", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Source_Itens.CancelEdit();
                }
                else
                {
                    ItemMvEstoque.LerDados(int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString()));
                    ItemMvEstoque.IdItem       = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                    ItemMvEstoque.Qtde         = Qtde;
                    ItemMvEstoque.VlrPrcCompra = VlrComp;
                    ItemMvEstoque.VlrUnitario = VlrUnt;
                    ItemMvEstoque.VlrDesconto = VlrDesc;
                    ItemMvEstoque.VlrSubTotal = VlrUnt * Qtde;
                    ItemMvEstoque.PIcms       = PIcms;
                    ItemMvEstoque.PIpi        = PIpi;
                    ItemMvEstoque.PercRed     = PReducao;
                    ItemMvEstoque.VlrFrete    = VlrFrete;
                    ItemMvEstoque.VlrIcms_Sub = VlrIcms_Sub;
                    ItemMvEstoque.NCM         = GridItens.CurrentRow.Cells[3].Value.ToString();
                    ItemMvEstoque.CodBarra    = GridItens.CurrentRow.Cells[4].Value.ToString();
                    ItemMvEstoque.Lote     = GridItens.CurrentRow.Cells[17].Value.ToString();
                    ItemMvEstoque.Validade    = GridItens.CurrentRow.Cells[18].Value.ToString();
                    if (MovEstoque.TpMov == "ENTNF")
                    {
                        ItemMvEstoque.IdCfop = int.Parse(GridItens.CurrentRow.Cells[15].Value.ToString());
                        ItemMvEstoque.Cst    = int.Parse(GridItens.CurrentRow.Cells[16].Value.ToString());
                    }
                    else
                    {
                        ItemMvEstoque.IdCfop = 0;
                        ItemMvEstoque.Cst    = 0;
                    }
                    ItemMvEstoque.GravarDados();
                    IdCFOPAnt = ItemMvEstoque.IdCfop;
                    CSTAnt    = ItemMvEstoque.Cst;
                    PopularGridItens();
                    GridItens.CurrentCell = GridItens.CurrentRow.Cells[e.ColumnIndex];
                }
            }
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MovEstoque.IdMov > 0)
                    IncluirItem();
                else
                    Source_Itens.CancelEdit();
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (MovEstoque.Status == 1)
                MessageBox.Show("Movimento já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);            
            else
            {
                if (MovEstoque.TpMov == "E_TRA" && MovEstoque.IdMovChave > 0)
                {
                    MessageBox.Show("Atenção: Transfência não pode ser modificada, solicite a filial origem para alterar", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (GridItens.CurrentRow != null)
                {
                    if (StaFormEdicao)
                        MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        if (MovEstoque.IdMov > 0 && !StaFormEdicao)
                        {
                            if (MessageBox.Show("Confirma a Exclusão do Item", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ItemMvEstoque.IdMov = MovEstoque.IdMov;
                                ItemMvEstoque.IdItem = int.Parse(GridItens.CurrentRow.Cells[0].Value.ToString());
                                ItemMvEstoque.Excluir();
                                //Registrando Movimento de Auditoria
                                FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvEstoque.IdItem, MovEstoque.NumDocumento, 3, "Excluindo");
                                ItemMvEstoque.IdItem = 0;
                                GridItens.Rows.Remove(GridItens.CurrentRow);
                                //Atualizando o Total do Movimento
                                TotalMovimento();
                            }
                        }
                    }
                }
            }
        }
        private void IncluirItem()
        {
            if (StaFormEdicao)
                MessageBox.Show("Favor gravar o Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MovEstoque.TpMov == "E_TRA" && MovEstoque.IdMovChave > 0)
                {
                    MessageBox.Show("Atenção: Transfência não pode ser modificada, solicite a filial origem para alterar", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MovEstoque.Status == 1)
                {
                    MessageBox.Show("Movimento já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Source_Itens.CancelEdit();
                }                
                else
                {
                    if (MovEstoque.IdMov > 0)
                    {
                        FrmBuscaProduto BuscaPrd = new FrmBuscaProduto();
                        BuscaPrd.FrmPrincipal    = this.FrmPrincipal;
                        BuscaPrd.IdProduto       = 0;
                        BuscaPrd.LstMvEst        = false;
                        BuscaPrd.ShowDialog();
                        
                        for (int I = 0; I <= BuscaPrd.ListaCodPrd.Count - 1; I++)
                        {
                            ArrayList PrdQtde = new ArrayList(BuscaPrd.ListaCodPrd[I].ToString().Split(char.Parse("|")));
                            BuscaPrd.CadProd.LerDados(int.Parse(PrdQtde[0].ToString()));
                            BuscaPrd.IdProduto = BuscaPrd.CadProd.IdProduto;

                            if (BuscaPrd.IdProduto > 0)
                            {
                                if (BuscaPrd.CadProd.ProdutoKit == 1)
                                {
                                    MessageBox.Show("Esse produto é um Kit e não pode ser movimentando: " + BuscaPrd.CadProd.Descricao.Trim(), "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Source_Itens.CancelEdit();
                                    //BuscaPrd.Dispose();
                                    //return;
                                    continue;
                                }

                                Verificar ExistePrd = new Verificar();
                                ExistePrd.Controle = Controle;
                                if (!ExistePrd.VerificarExite_LancProduto("SELECT * FROM MvEstoqueItens WHERE Id_Mov=" + MovEstoque.IdMov.ToString() + " and Id_Produto=" + BuscaPrd.IdProduto.ToString()))
                                {
                                    ItemMvEstoque.IdMov = MovEstoque.IdMov;
                                    ItemMvEstoque.IdProduto = BuscaPrd.IdProduto;
                                    if (TabAux.Chave == "COMPR" || TabAux.Chave == "ENTNF")
                                    {
                                        if (MovEstoque.IdFilialOrigDest == 2)
                                        {
                                            ItemMvEstoque.VlrPrcCompra = BuscaPrd.CadProd.UltPrcCompra;                                            
                                            ItemMvEstoque.VlrUnitario  = BuscaPrd.CadProd.UltPrcCompra2;
                                        }
                                        else
                                        {                                            
                                            ItemMvEstoque.VlrPrcCompra = BuscaPrd.CadProd.UltPrcCompra;
                                            ItemMvEstoque.VlrUnitario  = BuscaPrd.CadProd.UltPrcCompra;
                                        }
                                    }
                                    else
                                    {
                                        ItemMvEstoque.VlrUnitario = BuscaPrd.CadProd.PrcVarejo;
                                        ItemMvEstoque.VlrPrcCompra = 0;
                                    }
                                    if (decimal.Parse(PrdQtde[1].ToString()) > 0)
                                        ItemMvEstoque.Qtde = decimal.Parse(PrdQtde[1].ToString());
                                    else
                                        ItemMvEstoque.Qtde = 1;                                   
                                    

                                    int[] CFOPCST = ULT_CfopCSTItem(ItemMvEstoque.IdProduto,MovEstoque.IdFilialOrigDest);
                                    if (CFOPCST[0] == 0)
                                    {
                                        if (CadPessoa.IdUF != CadFilial.Uf)
                                            ItemMvEstoque.IdCfop = BuscaPrd.CadProd.IdCfopEF;
                                        else
                                            ItemMvEstoque.IdCfop = BuscaPrd.CadProd.IdCfopED;
                                    }
                                    else
                                    {
                                        ItemMvEstoque.IdCfop = CFOPCST[0];
                                        ItemMvEstoque.Cst = CFOPCST[1];
                                    }

                        

                                    ItemMvEstoque.VlrDesconto = 0;
                                    ItemMvEstoque.VlrSubTotal = ItemMvEstoque.VlrUnitario;
                                    ItemMvEstoque.VlrTotal    = ItemMvEstoque.VlrUnitario;
                                    ItemMvEstoque.IdItem      = 0;
                                    ItemMvEstoque.PIcms       = BuscaPrd.CadProd.IcmsIss;
                                    ItemMvEstoque.PIpi        = BuscaPrd.CadProd.Ipi;
                                    ItemMvEstoque.PercRed     = BuscaPrd.CadProd.Reducao;
                                    ItemMvEstoque.VlrFrete    = 0;
                                    ItemMvEstoque.VlrIcms_Sub = 0;
                                    ItemMvEstoque.NCM         = BuscaPrd.CadProd.NCM;
                                    ItemMvEstoque.CodBarra    = BuscaPrd.CadProd.CodBarra;
                                    ItemMvEstoque.Lote        = "";
                                    ItemMvEstoque.Validade    = "";

                                    if (ItemMvEstoque.IdCfop == 0)
                                        ItemMvEstoque.IdCfop = IdCFOPAnt;
                                    if (ItemMvEstoque.Cst == 0)
                                        ItemMvEstoque.Cst = CSTAnt;

                                    ItemMvEstoque.GravarDados();
                                    //Registrando Movimento de Auditoria
                                    FrmPrincipal.RegistrarAuditoria(this.Text + " Item", ItemMvEstoque.IdItem, MovEstoque.NumDocumento, 1, "Incluindo Item Vr.Unit:" + ItemMvEstoque.VlrUnitario.ToString() + "  Qtde:" + ItemMvEstoque.Qtde.ToString());
                                    //PopularGridItens();
                                    //GridItens.CurrentCell = GridItens.CurrentRow.Cells[3];
                                }
                                else
                                {
                                    MessageBox.Show("Produto já cadastrado no Movimento: " + BuscaPrd.CadProd.Descricao.Trim(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    continue;
                                    //Source_Itens.CancelEdit();
                                }
                            }
                            else
                                continue;
                        }
                        PopularGridItens();
                        if (GridItens.CurrentRow != null)
                           GridItens.CurrentCell = GridItens.CurrentRow.Cells[5];
                        BuscaPrd.Dispose();                        
                    }
                }
            }
        }
        private void Hab_Botoes()
        {
            LstTipMov.Enabled       = StaFormEdicao;
            TxtVlrDesconto.Enabled  = StaFormEdicao;
            BtnEnviarTranf.Enabled  = MovEstoque.Status == 1;
            BtnImpNFE.Visible = !StaFormEdicao && !FrmPrincipal.VersaoDistribuidor && MovEstoque.Status == 0 && (MovEstoque.TpMov == "ENTNF" || MovEstoque.TpMov == "COMPR" || MovEstoque.TpMov == "S_TRA");
            //Cb_PendAvaria.Visible   = TabAux.Chave == "COMPR";
            //TxtObsPendencia.Visible = Cb_PendAvaria.Visible;
            PnlAvaria.Visible = TabAux.Chave == "COMPR";
            
            if (StaFormEdicao || MovEstoque.IdMov == 0)
            {
                BtnConcluir.Enabled = false;
                BtnCancMov.Enabled  = false;
                BtnImprimir.Enabled = false;
                BtnImpPed.Enabled   = false;
            }
            else
            {                
                BtnConcluir.Enabled = MovEstoque.Status == 0;
                BtnImpPed.Enabled   = MovEstoque.Status == 0;
                BtnCancMov.Enabled  = MovEstoque.Status == 1;                                
                BtnImprimir.Enabled = true;                
            }            
        }
        private bool VerificarStatus()
        {
            MvEstoque StaMov = new MvEstoque();
            StaMov.Controle = Controle;
            StaMov.LerDados(MovEstoque.IdMov);
            if (StaMov.Status == 1)
            {
                MessageBox.Show("Movimento já Confirmado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
                return true;
        }
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (!StaFormEdicao)
            {
                if (!VerificarStatus())
                {
                    Hab_Botoes();
                    return;
                }

                BtnConcluir.Enabled = false;
                Application.DoEvents();
                if (MovEstoque.IdPessoa == 0)
                {
                    MessageBox.Show("Favor informar a Pessoa do Movimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hab_Botoes();
                    return;
                }
                if (MovEstoque.IdFilialOrigDest == 0)
                    MessageBox.Show("Favor informar a Filial", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (MovEstoque.TpMov=="S_TRA" && MovEstoque.IdFilialOrigDest == FrmPrincipal.IdFilialConexao)
                    {
                        MessageBox.Show("Atenção: Local de Origem e Destino iguais", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hab_Botoes();
                        return;
                    }
                     
                    if(MovEstoque.TpMov=="ENTNF" && !ValidarCFOP(MovEstoque.IdMov))
                    {   
                        Hab_Botoes();
                        return;
                    }

                    if (MessageBox.Show("Confirma a conclusão do Movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        TabelasAux TabAux = new TabelasAux();
                        TabAux.Controle = Controle;
                        TabAux.LerTabela("TPMVEST", MovEstoque.TpMov);

                        SqlDataReader Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MovEstoque.IdMov.ToString());
                        if (Tab.HasRows)
                        {
                            MovEstoque.IdFormaPgto = 0;
                            if (TabAux.Financeiro > 0)
                            {
                                // Fechamento Financeiro
                                FrmFechaMovimento FrmFecha = new FrmFechaMovimento();
                                FrmFecha.FrmPrincipal         = FrmPrincipal;
                                FrmFecha.TxtPessoa.Text       = TxtFornecedor.Text;
                                FrmFecha.TxtVlrSubTotal.Value = TxtVlrSubTotal.Value;
                                FrmFecha.TxtVlrDesconto.Value = TxtVlrDesconto.Value;
                                FrmFecha.TxtVlrTotal.Value    = TxtVlrTotal.Value;
                                FrmFecha.IdPessoa             = MovEstoque.IdPessoa;
                                FrmFecha.NumMov               = MovEstoque.IdMov;
                                FrmFecha.Referente            = LstTipMov.Text.Trim();
                                FrmFecha.NumDoc               = MovEstoque.NumDocumento;
                                FrmFecha.PagRec               = TabAux.Financeiro;
                                FrmFecha.IdPgto               = CadPessoa.IdFormaPgto;
                                FrmFecha.IdFilial             = MovEstoque.IdFilialOrigDest;
                                FrmFecha.ShowDialog();
                                if (FrmFecha.Concluido)
                                    MovEstoque.IdFormaPgto = int.Parse(FrmFecha.LstFormaPgto.SelectedValue.ToString());
                                else
                                {
                                    BtnConcluir.Enabled = true;
                                    return;
                                }
                                FrmFecha.Dispose();
                                //MovEstoque.Concluir();
                                //MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Paginas.SelectTab(0);
                            }
                            if (TabAux.Estoque > 0)
                            {
                                //Verificando Itens com Estoque Menor ou Igual a Zero para notificação de entrada via email
                                string ListaPrd = "";
                                if (MovEstoque.TpMov == "COMPR")
                                {
                                    DataSet PrdItens = new DataSet();
                                    PrdItens = Controle.ConsultaTabela("SELECT T3.REFERENCIA,T3.DESCRICAO,T1.QTDE FROM MVESTOQUEITENS T1 LEFT JOIN MVESTOQUE T2 ON (T2.ID_MOV=T1.ID_MOV)" +
                                                                       " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO) WHERE T2.TPMOV='COMPR' AND T2.ID_MOV=" + MovEstoque.IdMov.ToString() + " AND ENVIAREMAIL=1 AND T3.SALDOESTOQUE <= 0");
                                    if (PrdItens.Tables[0].Rows.Count > 0)
                                    {
                                        ListaPrd = "Fornecedor: " + TxtFornecedor.Text + " \n";
                                        for (int I = 0; I <= PrdItens.Tables[0].Rows.Count - 1; I++)
                                            ListaPrd = ListaPrd + " Ref.: " + PrdItens.Tables[0].Rows[I]["Referencia"].ToString().Trim() + " - " + PrdItens.Tables[0].Rows[I]["Descricao"].ToString().Trim() + "  Qtde:" + PrdItens.Tables[0].Rows[I]["QTDE"].ToString().Trim() + " \n";
                                    }
                                }
                                //
                                Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                                ControleEstoque.Controle = Controle;
                                ControleEstoque.MovimentoEstoque(Tab, TabAux.Estoque, 1, TabAux.Chave == "COMPR", MovEstoque.TpMov, MovEstoque.DtEntSai, MovEstoque.IdFilialOrigDest);
                                //
                                if (ListaPrd != "")
                                {
                                    EnviarEmail EnvEmail = new EnviarEmail();
                                    EnvEmail.Controle = Controle;
                                    EnvEmail.MontarEmail("  Chegada de Produtos  ", "        *** Entrada de Estoque ***  \n  \n " + ListaPrd);
                                }
                            }
                            {

                                MovEstoque.Concluir();
                            }
                            //Registrando Movimento de Auditoria
                            FrmPrincipal.RegistrarAuditoria(this.Text, MovEstoque.IdMov, MovEstoque.NumDocumento, 5, "Confirmação do Movimento");
                            MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PopularCampos(MovEstoque.IdMov);
                        }
                    }
                }
                PopularCampos(MovEstoque.IdMov);
            }
            
        }
        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            BtnCancMov.Enabled = false;
            Application.DoEvents();
            if (!StaFormEdicao)
            {
               /* if (MovEstoque.Data.Date.Year != DateTime.Now.Date.Year)
                {
                    MessageBox.Show("Ano do movimento, não autorizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    BtnCancMov.Enabled = true;
                    return;
                }*/
                if (MovEstoque.Documento == "BALANÇO AUT.")
                {
                    MessageBox.Show("Movimento não pode ser modificado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (FrmPrincipal.Perfil_Usuario.CancMovEst == 0)
                {                                        
                    FrmAutorizacao Autorizacao = new FrmAutorizacao();
                    Autorizacao.FrmPrincipal = FrmPrincipal;
                    Autorizacao.ShowDialog();
                    //Verificando se o Acesso foi liberado
                    if (Autorizacao.AcessoOk)
                    {
                        if (Autorizacao.Usuario.CancMovEst == 0)
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
                //
                /*TimeSpan Dias = DateTime.Now.Subtract(MovEstoque.Data);
                if (Dias.Days > 5)
                {
                    MessageBox.Show("Movimento com mais de 5 dias concluido, movimento nao pode ser cancelado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }*/

                if (MovEstoque.Documento == "BALANÇO AUT.")
                {

                }
                if (MessageBox.Show("Confirma o Cancelamento do Movimento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (MovEstoque.TpMov=="S_TRA" && MovEstoque.IdMovChave >0)
                    {
                        if (!CancelaTransfDestino())
                            return;
                    }
                    TabelasAux TabAux = new TabelasAux();
                    TabAux.Controle = Controle;
                    TabAux.LerTabela("TPMVEST", MovEstoque.TpMov);
                    SqlDataReader Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + MovEstoque.IdMov.ToString());
                    if (Tab.HasRows)
                    {
                        if (TabAux.Estoque > 0)
                        {
                            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
                            ControleEstoque.Controle = Controle;
                            ControleEstoque.MovimentoEstoque(Tab, TabAux.Estoque, 2, false, MovEstoque.TpMov, MovEstoque.DtEntSai,MovEstoque.IdFilialOrigDest);                            
                        }
                    }
                    if (TabAux.Financeiro > 0)
                       Controle.ExecutaSQL("DELETE FROM LANCFINANCEIRO WHERE Id_Mov=" + MovEstoque.IdMov.ToString());

                    MovEstoque.Cancelar();
                    //Registrando Movimento de Auditoria
                    FrmPrincipal.RegistrarAuditoria(this.Text, MovEstoque.IdMov, MovEstoque.NumDocumento, 4, "Cancelando");
                    MessageBox.Show("Movimento cancelado", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopularCampos(MovEstoque.IdMov);
                    //Paginas.SelectTab(0);
                }
            }
        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            /*FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelMvEstoque RelMvEstoque = new Relatorios.RelMvEstoque();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(MovEstoque.SqlRelatorio(MovEstoque.IdMov));
            //RelMvEstoque.SetDataSource(TabRel);
            RelMvEstoque.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelMvEstoque;
            FrmRel.ShowDialog();*/
        }
        private void BtnBuscaPessoa_Click(object sender, EventArgs e)
        {
            if (StaFormEdicao)
            {
                if (MovEstoque.Status == 1)
                    MessageBox.Show("Movimento já Concluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    FrmBuscaPessoa BuscaPessoa = new FrmBuscaPessoa();
                    BuscaPessoa.FrmPrincipal = this.FrmPrincipal;                    
                    BuscaPessoa.ShowDialog();
                    if (BuscaPessoa.CadPessoa.IdPessoa > 0)
                    {
                        LstFilial.SelectedValue = BuscaPessoa.CadPessoa.IdFilial.ToString();
                        SetaPessoa(BuscaPessoa.CadPessoa.IdPessoa);

                    }
                }
            }
        }
        private void SetaPessoa(int IdPessoa)
        {            
            CadPessoa.LerDados(IdPessoa);
            MovEstoque.IdPessoa = CadPessoa.IdPessoa;
            TxtCodPessoa.Text   = CadPessoa.IdPessoa.ToString();
            TxtFornecedor.Text  = CadPessoa.RazaoSocial.Trim();            
        }
        private void LstTipMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabAux.Controle != null)
            {
                
                TabAux.LerTabela("TPMVEST", LstTipMov.SelectedValue.ToString());                
                LblFilial.Text         = "Filial Origem:";
                BtnEnviarTranf.Visible = LstTipMov.SelectedValue.ToString() == "S_TRA"; 
                if (LstTipMov.SelectedValue.ToString() == "S_TRA")
                {
                    LblFilial.Visible     = true;
                    LstFilial.Visible = true;
                    LblFilial.Text        = "Filial Destino:";
                }
                else if (LstTipMov.SelectedValue.ToString() == "E_TRA")
                {
                    LblFilial.Visible     = true;
                    LstFilial.Visible = true;
                    LblFilial.Text        = "Filial Origem:";
                }
                
                //Verifica se mostra Aba de Valores
                if (TabAux.Chave == "COMPR" || TabAux.Chave == "ENTNF")
                    LstFilial = FrmPrincipal.PopularCombo("SELECT ID_FILIAL,SUBSTRING(FANTASIA,1,40) AS FILIAL FROM EMPRESA_FILIAL", LstFilial);            
                //PnlValores.Visible = TabAux.Financeiro > 0;
                //lblCFOP.Visible    = TabAux.Dief > 0;
                //LstCfop.Visible    = TabAux.Dief > 0;
                //BtnImpPed.Visible  = TabAux.Estoque == 1;
                //ColVlrCompra.ReadOnly = TabAux.Estoque != 1;
                //ColVlrCompra.Visible = TabAux.Chave == "COMPR";
                GridItens.Columns[3].Visible = TabAux.Chave == "COMPR";
                GridItens.Columns[4].Visible = TabAux.Chave == "COMPR";
                GridItens.Columns[6].Visible = TabAux.Chave == "COMPR";
                GridItens.Columns[15].Visible = TabAux.Chave == "ENTNF";
                GridItens.Columns[16].Visible = TabAux.Chave == "ENTNF";
                GridItens.Columns[17].Visible = TabAux.Chave == "COMPR";
                GridItens.Columns[18].Visible = TabAux.Chave == "COMPR";
                Cb_NtServico.Visible = TabAux.Chave == "ENTNF";

                
                if (TabAux.Dief > 0)
                {                    
                    TxtSolicitante.Text   = ""; ;
                    TxtAutorizado.Text    = "";
                    Rb_CIF.Checked        = true;
                    TxtBIcms.Value        = 0;
                    TxtVlrIcms.Value      = 0;
                    TxtBIcmsSub.Value     = 0;
                    TxtVlrIcmsSub.Value   = 0;
                    TxtVlrFrete.Value     = 0;
                    TxtVlrSeguro.Value    = 0;
                    TxtVlrOutraDesp.Value = 0;
                    TxtVlrIpi.Value       = 0;
                }
                PopularGridItens();
            }
        }
        private void TxtVlrDesconto_Validated(object sender, EventArgs e)
        {            
            if (StaFormEdicao)

                TxtVlrTotal.Value = TxtVlrSubTotal.Value - TxtVlrDesconto.Value;
        }
        private void PagCab_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_LANC,T1.DATALANC,T1.NUMDOCUMENTO,T1.VENCIMENTO,T1.VLRORIGINAL,T1.DTBAIXA,T1.VLRJURO,T1.VLRMULTA,T1.VLRBAIXA,T2.DOCUMENTO AS TIPODOC FROM LANCFINANCEIRO T1" +
                " LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE T1.Id_Mov=" + MovEstoque.IdMov.ToString());
            GridFinanc.DataSource = Tabela;
            GridFinanc.DataMember = Tabela.Tables[0].TableName;
        }
        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           /* if (e.ColumnIndex == 1)
            {
                if (e.Value.ToString().Trim() == "Confirmado")
                    GridDados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Silver;
            }*/
        }
        private void BtnImpPed_Click(object sender, EventArgs e)
        {
            FrmBuscaPedCompra BuscaPed = new FrmBuscaPedCompra();
            BuscaPed.FrmPrincipal = FrmPrincipal;           

            if (MovEstoque.IdPedCompra > 0)
            {
                if (MessageBox.Show("Já existe um pedido de compra vinculado, deseja realmente continuar ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    BuscaPed.ShowDialog();
            }
            else
                BuscaPed.ShowDialog();

            if (BuscaPed.MvPedCompra.IdDocumento > 0)
            {
                if (MessageBox.Show("Confirma a importação dos dados ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (MovEstoque.IdPedCompra > 0)
                        Controle.ExecutaSQL("DELETE FROM MVESTOQUEITENS WHERE ID_MOV=" + MovEstoque.IdMov.ToString());

                    Produtos CadPrd = new Produtos();
                    CadPrd.Controle = Controle;
                    DataSet ConsItens = new DataSet();
                    ConsItens = Controle.ConsultaTabela("SELECT * FROM PEDCOMPRAITENS WHERE ID_DOCUMENTO=" + BuscaPed.MvPedCompra.IdDocumento.ToString()+" order by id_item");
                    if (ConsItens.Tables[0].Rows.Count > 0)
                    {
                        FrmPrincipal.BSta_BarProcesso.Maximum = ConsItens.Tables[0].Rows.Count;
                        MvEstoqueItens Itens = new MvEstoqueItens();
                        Itens.Controle = Controle;
                        for (int I = 0; I <= ConsItens.Tables[0].Rows.Count - 1; I++)
                        {
                            CadPrd.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString()));
                            int[] CFOPCST = ULT_CfopCSTItem(Itens.IdProduto,MovEstoque.IdFilialOrigDest);

                            Itens.LerDados(0);                            
                            Itens.IdMov        = MovEstoque.IdMov;
                            Itens.IdProduto    = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                            Itens.Qtde         = decimal.Parse(ConsItens.Tables[0].Rows[I]["Qtde"].ToString());
                            Itens.VlrUnitario  = decimal.Parse(ConsItens.Tables[0].Rows[I]["VlrUnitario"].ToString());
                            Itens.PercRed      = CadPrd.Reducao;
                            Itens.VlrSubTotal  = Itens.Qtde * Itens.VlrUnitario;
                            Itens.VlrPrcCompra = CadPrd.UltPrcCompra;
                            Itens.NCM          = CadPrd.NCM;
                            Itens.CodBarra     = CadPrd.CodBarra;
                            Itens.VlrFrete     = 0;
                            Itens.IdCfop       = CFOPCST[0];
                            Itens.Cst          = CFOPCST[1];
                            Itens.PIpi         = decimal.Parse(ConsItens.Tables[0].Rows[I]["PIpi"].ToString());
                            Itens.PIcms        = decimal.Parse(ConsItens.Tables[0].Rows[I]["PIcms"].ToString());
                            Itens.GravarDados();
                            FrmPrincipal.BSta_BarProcesso.Maximum = FrmPrincipal.BSta_BarProcesso.Maximum + 1;
                        }
                        MovEstoque.IdPedCompra = BuscaPed.MvPedCompra.IdDocumento;
                        MovEstoque.IdPessoa    = BuscaPed.MvPedCompra.IdPessoa;
                        MovEstoque.Observacao  = BuscaPed.MvPedCompra.Observacao;
                        MovEstoque.GravarDados();
                        PopularGridItens();
                        MessageBox.Show("Importação concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmPrincipal.BSta_BarProcesso.Maximum = 0;
                    }
                }
            }
        }
        private void BtnImprimir_Click_1(object sender, EventArgs e)
        {
            if (MovEstoque.Status == 2)
            {
                MessageBox.Show("Não pode imprimir movimento cancelado", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MovEstoque.Status == 0)
            {
                MessageBox.Show("Confirme antes de imprimir o movimento", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sSql = "SELECT T5.Descricao AS DESCMOV, T2.Id_Mov, T2.Data, T2.TPMov, T2.Id_PedCompra, T2.Id_Pessoa, T2.Documento, T2.NumDocumento, T2.NumFormulario, " +
                          "T2.ObsSelo, T2.Id_Filial, T2.Id_FilialOrigDest, T2.Id_FormaPgto, T2.Solicitante, T2.Autorizado, T2.DtEntSai, T2.TpFrete, T2.ID_Cfop, T2.VlrSubTotal, " +
                          "T2.VlrDesconto, T2.VlrTotal, T2.B_Icms, T2.VlrIcms, T2.B_IcmsSub, T2.VlrIcmsSub, T2.VlrFrete, T2.VlrSeguro, T2.VlrOutrasDesp, T2.VlrIpi, T2.Observacao," +
                          "T2.Status, T3.Referencia, T3.Descricao, T1.Qtde, T1.VlrPrcCompra, T1.VlrUnitario, T1.VlrSubTotal AS SUBITEM, T1.VlrDesconto AS DESCITEM, " +
                          "T1.VlrTotal AS TOTALITEM, T4.RazaoSocial, T1.P_IPI, T1.P_ICMS, T1.VlrIPI AS IPIITEM, T1.PercRed, T1.VLRFRETE AS FRETEITENS,T6.FANTASIA AS FILIAL, " +
                          "Rtrim(T4.Endereco)+', '+RTrim(T4.Numero)+'    '+Rtrim(T4.Complemento) as Endereco " +
                          " FROM MVESTOQUEITENS T1" +
                          " LEFT JOIN MVESTOQUE T2 ON (T2.ID_MOV=T1.ID_MOV)" +
                          " LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                          " LEFT JOIN PESSOAS T4 ON (T4.ID_PESSOA=T2.ID_PESSOA)" +
                          " LEFT JOIN TABELASAUX T5 ON (T5.CAMPO='TPMVEST' AND T5.CHAVE=T2.TPMOV)" +
                          " LEFT JOIN EMPRESA_FILIAL T6 ON (T6.ID_FILIAL=T2.ID_FILIALORIGDEST)" +
                          " WHERE T1.ID_MOV=" + MovEstoque.IdMov.ToString() + " order by t1.id_item";
            BtnImprimir.Enabled = false;
            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelMvEstoque Rel001 = new Relatorios.RelMvEstoque();
            DataSet TabRel = new DataSet();
            DataSet TabVenc = new DataSet();
            TabRel = Controle.ConsultaTabela(sSql);
            TabVenc = Controle.ConsultaTabela("SELECT VENCIMENTO,VLRORIGINAL FROM LANCFINANCEIRO WHERE ID_MOV=" + MovEstoque.IdMov.ToString() + " ORDER BY VENCIMENTO"); ;
            Rel001.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
            Rel001.Database.Tables[1].SetDataSource(TabVenc.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = Rel001;
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section2.ReportObjects["LblFilial"])).Text = FrmPrincipal.LstFilial.Text.Trim();
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(Rel001.Section5.ReportObjects["LblRodaPe"])).Text = FrmPrincipal.Rel_RodaPe;            
            FrmRel.ShowDialog();
            BtnImprimir.Enabled = true;
        }       
        private void Ck_Conferido_CheckedChanged(object sender, EventArgs e)
        {
            if (!StaFormEdicao && MovEstoque.IdMov > 0)
            {
                if (MovEstoque.Status == 1)
                {
                    if (Ck_Conferido.Checked)
                    {
                        if (FrmPrincipal.Perfil_Usuario.SolicAutCanc == 1)
                        {
                            FrmAutorizacao Autorizacao = new FrmAutorizacao();
                            Autorizacao.FrmPrincipal = FrmPrincipal;
                            Autorizacao.ShowDialog();
                            //Verificando se o Acesso foi liberado
                            if (Autorizacao.AcessoOk)
                            {
                                if (Autorizacao.Usuario.SolicAutCanc == 1)
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
                        Controle.ExecutaSQL("UPDATE MVESTOQUE SET CONFERIDO=1 WHERE ID_MOV=" + MovEstoque.IdMov);
                    }
                    else
                        Controle.ExecutaSQL("UPDATE MVESTOQUE SET CONFERIDO=0 WHERE ID_MOV=" + MovEstoque.IdMov);                    
                }
            }
        }
        private void GridDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (GridDados.CurrentRow != null)
                {
                    if (GridDados.CurrentRow.Cells[1].Value.ToString() == "Confirmado")
                    {
                        if (GridDados.CurrentRow.Cells[2].Value.ToString() == "1")
                        {
                            if (FrmPrincipal.Perfil_Usuario.SolicAutCanc == 1)
                            {
                                FrmAutorizacao Autorizacao = new FrmAutorizacao();
                                Autorizacao.FrmPrincipal = FrmPrincipal;
                                Autorizacao.ShowDialog();
                                //Verificando se o Acesso foi liberado
                                if (Autorizacao.AcessoOk)
                                {
                                    if (Autorizacao.Usuario.SolicAutCanc == 1)
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
                            Controle.ExecutaSQL("UPDATE MVESTOQUE SET CONFERIDO=0 WHERE ID_MOV=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                        }
                        else
                            Controle.ExecutaSQL("UPDATE MVESTOQUE SET CONFERIDO=1 WHERE ID_MOV=" + GridDados.CurrentRow.Cells[0].Value.ToString());
                    }
                }
            }
        }

        private void BtnEnviarTranf_Click(object sender, EventArgs e)
        {
            BtnEnviarTranf.Enabled = false;
            Application.DoEvents();
            SqlConnection ServidorDestino;
            Filiais FilialDest = new Filiais();
            FilialDest.Controle = Controle;
            FilialDest.LerDados(MovEstoque.IdFilialOrigDest);
            
            if (MovEstoque.IdFilialOrigDest == 0)
            {
                MessageBox.Show("Atenção: Selecione a Filial Destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarTranf.Enabled = true;
                return;
            }
            if (FilialDest.ServidorRemoto == "")
            {
                MessageBox.Show("Atenção: Configuração do Servidor Destino inválida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarTranf.Enabled = true;
                return;
            }
            if (MovEstoque.IdFilialOrigDest == FrmPrincipal.IdFilialConexao)
            {
                MessageBox.Show("Atenção: Local de Origem e Destino iguais", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarTranf.Enabled = true;
                return;
            }
            //Conectando ao Servidor Destino            
            try
            {
                string conexao = "";
                if (FrmPrincipal.VersaoDistribuidor)
                    conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;";
                else
                {
                    ///conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                    ///
                    conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                }

                ServidorDestino = new SqlConnection(conexao);
                ServidorDestino.Open();            
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor destino, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarTranf.Enabled = true;
                return;
            }

            Funcoes ControleDest = new Funcoes();
            ControleDest.Conexao = ServidorDestino;
                        
            MvEstoqueItens ItensDest = new MvEstoqueItens();
            ItensDest.Controle = ControleDest;

            MvEstoque MvDestino  = new MvEstoque();
            MvDestino.Controle   = ControleDest;
                                        
            if (MovEstoque.IdMovChave > 0)
            {
                if (MessageBox.Show("Transferência já enviada, deseja atualizar ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    BtnEnviarTranf.Enabled = true;
                    return;
                }
                MvDestino.LerDados(MovEstoque.IdMovChave);
                //if (MvDestino.IdMov == 0)
                //{
                 //   MessageBox.Show("Atenção: Transferência não localizada no servidor destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ///    BtnEnviarTranf.Enabled = true;
                //    return;
                //}
                if (MvDestino.Status == 1)
                {
                    MessageBox.Show("Atenção: Transferência Confirmada no Servidor Destino, para atualizar solicite o cancelamento a filial destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnEnviarTranf.Enabled = true;
                    return;
                }
                MvDestino.Excluir();
            }

            if (MessageBox.Show("Confirma o Envio da Transferência para a Filial Destino ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {                    
                    int IdPessoa = 0;
                    Filiais CadFilial = new Filiais();
                    CadFilial.Controle = Controle;
                    CadFilial.LerDados(FrmPrincipal.IdFilialConexao);
                                        
                    SqlDataReader PesqCadPessoa = ControleDest.ConsultaSQL("SELECT * FROM Pessoas WHERE Cnpj='" + CadFilial.Cnpj.Trim() + "'");
                    if (PesqCadPessoa.HasRows)
                    {
                        PesqCadPessoa.Read();                        
                        IdPessoa = int.Parse(PesqCadPessoa["Id_Pessoa"].ToString());
                    }

                    if (MovEstoque.IdFilialOrigDest == 9)
                        IdPessoa = 81943; // MovEstoque.IdPessoa;

                    MvDestino.LerDados(0);
                    MvDestino.Data             = DateTime.Now;
                    MvDestino.IdPessoa         = IdPessoa;
                    MvDestino.Documento        = "Transf.Int";
                    MvDestino.NumDocumento     = string.Format("{0:D8}", MovEstoque.IdMov);
                    MvDestino.DtEmissao        = MovEstoque.DtEmissao;
                    MvDestino.DtEntSai         = DateTime.Now;
                    MvDestino.IdFilialOrigDest = FrmPrincipal.IdFilialConexao;
                    MvDestino.IdMovChave       = MovEstoque.IdMov;
                    MvDestino.Solicitante      = MovEstoque.Solicitante;
                    MvDestino.Autorizado       = MovEstoque.Autorizado;
                    MvDestino.Observacao       = MovEstoque.Observacao;
                    MvDestino.TpMov            = "E_TRA";
                    MvDestino.GravarDados();
                    //
                    MovEstoque.IdMovChave   = MvDestino.IdMov;
                    MovEstoque.NumDocumento = string.Format("{0:D8}", MvDestino.IdMov);
                    MovEstoque.GravarDados();
                    //
                    Produtos CadPrd = new Produtos();
                    CadPrd.Controle = ControleDest;

                    DataSet ConsItens = new DataSet();
                    ConsItens = Controle.ConsultaTabela("SELECT T2.REFERENCIA,T1.* FROM MVESTOQUEITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_MOV=" + MovEstoque.IdMov.ToString());

                    if (ConsItens.Tables[0].Rows.Count > 0)
                    {
                        FrmPrincipal.BSta_BarProcesso.Maximum = ConsItens.Tables[0].Rows.Count;
                        for (int I = 0; I <= ConsItens.Tables[0].Rows.Count - 1; I++)
                        {
                            CadPrd.LerDados(ConsItens.Tables[0].Rows[I]["Referencia"].ToString().Trim());
                            if (CadPrd.IdProduto > 0)
                            {
                                ItensDest.LerDados(0);
                                ItensDest.IdMov       = MvDestino.IdMov;
                                ItensDest.IdProduto   = CadPrd.IdProduto;
                                ItensDest.Qtde        = decimal.Parse(ConsItens.Tables[0].Rows[I]["Qtde"].ToString());
                                ItensDest.VlrUnitario = decimal.Parse(ConsItens.Tables[0].Rows[I]["VlrUnitario"].ToString());
                                ItensDest.VlrSubTotal = ItensDest.Qtde * ItensDest.VlrUnitario;
                                ItensDest.GravarDados();
                            }
                            else
                            {
                                MessageBox.Show("Atenção: Produto: " + ConsItens.Tables[0].Rows[I]["Referencia"].ToString().Trim() + " não localizado no Servidor Destino, Solicite ao destino atualizar o Cadastro de produtos e tente novmente ", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MvDestino.Excluir();
                                MovEstoque.IdMovChave = 0;
                                MovEstoque.GravarDados();
                                BtnEnviarTranf.Enabled = true;
                                return;
                            }                                
                            FrmPrincipal.BSta_BarProcesso.Maximum = FrmPrincipal.BSta_BarProcesso.Maximum + 1;                            
                        }                        
                        MvDestino.GravarDados();
                        MessageBox.Show("Transferência concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmPrincipal.BSta_BarProcesso.Maximum = 0;
                    }
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro: " + erro.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MvDestino.Excluir();
                    MovEstoque.IdMovChave = 0;
                    MovEstoque.GravarDados();
                    BtnEnviarTranf.Enabled = true;
                    return;
                }
            }


        }
        private bool CancelaTransfDestino()
        {
            if (MovEstoque.IdMovChave == 0)
                return true;

            SqlConnection ServidorDestino;
            Filiais FilialDest = new Filiais();
            FilialDest.Controle = Controle;
            FilialDest.LerDados(MovEstoque.IdFilialOrigDest);

            if (FilialDest.ServidorRemoto == "")
            {
                MessageBox.Show("Atenção: Configuração do Servidor Destino inválida", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarTranf.Enabled = true;
                return false;
            }
            //Conectando ao Servidor Destino            
            try
            {
                string conexao = "";
                if (FrmPrincipal.VersaoDistribuidor)
                    conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;";
                else
                {
                    conexao = "Data Source=" + FilialDest.ServidorRemoto + FilialDest.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                }

                ServidorDestino = new SqlConnection(conexao);
                ServidorDestino.Open();
            }
            catch
            {
                MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor destino, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnEnviarTranf.Enabled = true;
                return false;
            }

            Funcoes ControleDest = new Funcoes();
            ControleDest.Conexao = ServidorDestino;
                        
            MvEstoque MvDestino = new MvEstoque();
            MvDestino.Controle = ControleDest;

            if (MovEstoque.IdMovChave > 0)
            {
                MvDestino.LerDados(MovEstoque.IdMovChave);
                if (MvDestino.IdMov == 0)
                {
                    MessageBox.Show("Atenção: Transferência não localizada no servidor destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return false;
                }
                if (MvDestino.Status == 1)
                {
                    MessageBox.Show("Atenção: Transferência Confirmada no Servidor Destino, para atualizar solicite o cancelamento a filial destino", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return false;
                }
                MvDestino.Excluir();
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MvEstoque Mv = new MvEstoque();
            Mv.Controle = Controle;

            TabelasAux TabAux = new TabelasAux();
            TabAux.Controle = Controle;

            Controles.ControleEstoque ControleEstoque = new ControleEstoque();
            ControleEstoque.Controle = Controle;

            MvEstoqueItens MvItens = new MvEstoqueItens();
            MvItens.Controle = Controle;

            button1.Text = GridDados.Rows.Count.ToString();
            for (int I = 0; I <= GridDados.RowCount - 1; I++)
            {
                button1.Text = GridDados.Rows.Count.ToString() + " - " + I.ToString(); ;
                button1.Update();
                int NumVd = int.Parse(GridDados.Rows[I].Cells[0].Value.ToString());
                Mv.LerDados(NumVd);

                TabAux.LerTabela("TPMVEST", Mv.TpMov);
                if (Mv.Status == 1)
                {
                    SqlDataReader Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + Mv.IdMov.ToString());
                    if (Tab.HasRows)
                        ControleEstoque.MovimentoEstoque(Tab, TabAux.Estoque, 1, TabAux.Chave == "COMPR", Mv.TpMov, Mv.DtEntSai,Mv.IdFilial);
                }
                /*TabAux.LerTabela("TPMVEST", Mv.TpMov);
                SqlDataReader Tab = Controle.ConsultaSQL("SELECT * FROM MVESTOQUEITENS WHERE Id_Mov=" + Mv.IdMov.ToString());
                while (Tab.Read())
                {
                    MvItens.LerDados(int.Parse(Tab["ID_ITEM"].ToString()));
                    MvItens.GravarDados();                    
                }*/
            }
            MessageBox.Show("Fim");
        }
        private void BtnImpNFE_Click(object sender, EventArgs e)
        {
            FrmMovImpNFE FrmImpNF = new FrmMovImpNFE();
            FrmImpNF.FrmPrincipal         = FrmPrincipal;            
            FrmImpNF.ShowDialog();

            if (FrmImpNF.CadNota.IdNota > 0)
            {
                MovEstoque.NumFormulario = string.Format("{0:D6}",FrmImpNF.CadNota.NumFormulario);
                MovEstoque.DtEmissao     = FrmImpNF.CadNota.DtEmissao;
                MovEstoque.Documento     = "NFE";
                MovEstoque.ChaveNFE      = FrmImpNF.CadNota.ChaveNfe;
                MovEstoque.TpFrete       = FrmImpNF.CadNota.Frete;
                MovEstoque.GravarDados();

                Produtos CadPrd = new Produtos();
                CadPrd.Controle = Controle;
                DataSet ConsItens = new DataSet();
                ConsItens = Controle.ConsultaTabela("SELECT * FROM NOTAFISCALITENS WHERE ID_NOTA=" + FrmImpNF.CadNota.IdNota.ToString());
                if (ConsItens.Tables[0].Rows.Count > 0)
                {
                    FrmPrincipal.BSta_BarProcesso.Maximum = ConsItens.Tables[0].Rows.Count;
                    MvEstoqueItens Itens = new MvEstoqueItens();
                    Itens.Controle = Controle;
                    for (int I = 0; I <= ConsItens.Tables[0].Rows.Count - 1; I++)
                    {
                        CadPrd.LerDados(int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString()));
                        //int[] CFOPCST = ULT_CfopCSTItem(CadPrd.IdProduto, MovEstoque.IdFilialOrigDest);

                        Itens.LerDados(0);
                        Itens.IdMov        = MovEstoque.IdMov;
                        Itens.IdProduto    = int.Parse(ConsItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                        Itens.Qtde         = decimal.Parse(ConsItens.Tables[0].Rows[I]["Qtde"].ToString());
                        Itens.VlrUnitario  = decimal.Parse(ConsItens.Tables[0].Rows[I]["VlrUnitario"].ToString());
                        Itens.PercRed      = decimal.Parse(ConsItens.Tables[0].Rows[I]["PercRed"].ToString());
                        Itens.PIpi         = decimal.Parse(ConsItens.Tables[0].Rows[I]["PIPI"].ToString());
                        Itens.VlrIpi       = decimal.Parse(ConsItens.Tables[0].Rows[I]["VLRIPI"].ToString());
                        Itens.PIcms        = decimal.Parse(ConsItens.Tables[0].Rows[I]["PICMS"].ToString());
                        Itens.VlrIcms      = decimal.Parse(ConsItens.Tables[0].Rows[I]["VLRICMS"].ToString());
                        Itens.VlrSubTotal  = Itens.Qtde * Itens.VlrUnitario;
                        Itens.VlrPrcCompra = CadPrd.UltPrcCompra;
                        Itens.NCM          = CadPrd.NCM;
                        Itens.CodBarra     = CadPrd.CodBarra;
                        Itens.VlrFrete     = 0;
                        Itens.IdCfop       = 71;
                        Itens.Cst          = 8;
                        Itens.GravarDados();
                        FrmPrincipal.BSta_BarProcesso.Maximum = FrmPrincipal.BSta_BarProcesso.Maximum + 1;
                    }
                    MovEstoque.GravarDados();
                    Itens.CalcularImposto();
                    PopularGridItens();
                }
                MessageBox.Show("Importação concluida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmPrincipal.BSta_BarProcesso.Maximum = 0;
            }
        }

        private void Cb_PendAvaria_CheckedChanged(object sender, EventArgs e)
        {
            if (!StaFormEdicao && MovEstoque.IdMov > 0)
            {
                if (MovEstoque.Status == 1)
                {
                    if (Cb_PendAvaria.Checked)
                    {
                        if (FrmPrincipal.Perfil_Usuario.SolicAutCanc == 1)
                        {
                            FrmAutorizacao Autorizacao = new FrmAutorizacao();
                            Autorizacao.FrmPrincipal = FrmPrincipal;
                            Autorizacao.ShowDialog();
                            //Verificando se o Acesso foi liberado
                            if (Autorizacao.AcessoOk)
                            {
                                if (Autorizacao.Usuario.SolicAutCanc == 1)
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
                        Controle.ExecutaSQL("UPDATE MVESTOQUE SET PENDENCIA=1 WHERE ID_MOV=" + MovEstoque.IdMov);
                    }
                    else
                        Controle.ExecutaSQL("UPDATE MVESTOQUE SET PENDENCIA=0 WHERE ID_MOV=" + MovEstoque.IdMov);
                }
            }
        }

        private bool ValidarCFOP(int IdMov)
        {
            SqlDataReader Tab;
            //Verificando os Itens
            Tab = Controle.ConsultaSQL("select * from MvEstoqueItens WHERE (ISNULL(Id_Cfop,0)=0 OR ISNULL(CST,0)=0) and Id_Mov = "+IdMov.ToString()); 
            if (Tab.HasRows)
            {
                MessageBox.Show("Atenção: Favor verificar o CFOP e o CST dos Itens", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private int[] ULT_CfopCSTItem(int IdPrd, int IdFilial)
        {
            int[] Itens = new int[2];
            Itens[0] = 0;
            Itens[1] = 0;
            SqlDataReader Tab;
            //Verificando os Itens
            Tab = Controle.ConsultaSQL("SELECT TOP 1 ISNULL(T1.ID_CFOP,0) as ID_CFOP,ISNULL(CST,0) AS CST FROM MvEstoqueItens T1" +
                                " LEFT JOIN MvEstoque T2 ON (T2.Id_Mov=T1.Id_Mov)" +
                                " WHERE T2.TPMov='ENTNF' AND T2.Status=1 and T2.Id_FilialOrigDest=" + IdFilial.ToString() +
                                " AND T1.ID_PRODUTO=" + IdPrd.ToString() +
                                " ORDER BY T1.Id_Mov DESC");
            if (Tab.HasRows)
            {
                Tab.Read();
                Itens[0] = int.Parse(Tab["ID_Cfop"].ToString());
                Itens[1] = int.Parse(Tab["CST"].ToString());
            }            
            return Itens;
        }

        private void TxtPesqNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtPesqNumDoc.Text.Trim()!="")
                PopularGrid();
        }
    }
}

