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
    public partial class FrmPDV : Form
    {
        Funcoes Controle  = new Funcoes();
        MvVenda Vendas    = new MvVenda();
        Pessoas CadPessoa = new Pessoas();
        public TelaPrincipal FrmPrincipal;
        public FrmPDV()
        {
            InitializeComponent();
        }
        private int VerificarCaixa()
        {
            //Verificar se tem caixa Aberto
            int IdCaixa = 0;
            Controles.Verificar VerifCx = new Controles.Verificar();
            VerifCx.Controle = Controle;
            IdCaixa = VerifCx.Verificar_ExisteCadastro("Id_Caixa", "SELECT T1.ID_CAIXA FROM CAIXABALCAO T1 WHERE T1.STATUS=0 AND T1.ID_Usuario=" + FrmPrincipal.Perfil_Usuario.IdUsuario.ToString());
            return IdCaixa;
        }
        private void FrmPDV_Load(object sender, EventArgs e)
        {
            Controle.Conexao   = FrmPrincipal.Conexao;
            Vendas.Controle    = Controle;
            CadPessoa.Controle = Controle;
            NumPedido.Focus();
        }       
        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (NumPedido.Value > 0)
            {
                int IdCaixa = VerificarCaixa();
                if (IdCaixa == 0)
                {
                    MessageBox.Show("Favor verificar o status do Caixa", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Vendas.LerDados(int.Parse(NumPedido.Value.ToString()));
                if (Vendas.IdVenda == 0)
                    MessageBox.Show("Pedido não localizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (Vendas.Status == 0)
                    MessageBox.Show("Pedido em aberto", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                else if (Vendas.Status == 3)
                    MessageBox.Show("Pedido entregue", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (Vendas.Status == 4)
                    MessageBox.Show("Pedido cancelado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (Vendas.Status == 2)
                    {                     
                        if (MessageBox.Show("Movimento já Faturado, Deseja refazer o faturamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    CadPessoa.LerDados(Vendas.IdPessoa);
                    string RefVd = Vendas.IdVenda.ToString();
                    string RefDocVd = Vendas.NumDocumento;
                    // Fechamento Financeiro 
                    FrmFechaMovimento FrmFecha = new FrmFechaMovimento();
                    FrmFecha.FrmPrincipal         = FrmPrincipal;
                    FrmFecha.TxtPessoa.Text       = CadPessoa.RazaoSocial;
                    FrmFecha.TxtVlrSubTotal.Value = Vendas.VlrSubTotal;
                    FrmFecha.TxtVlrDesconto.Value = Vendas.VlrDesconto;
                    FrmFecha.TxtVlrTotal.Value    = Vendas.VlrTotal;
                    FrmFecha.IdPessoa             = CadPessoa.IdPessoa;
                    FrmFecha.NumVd                = Vendas.IdVenda;
                    FrmFecha.Referente            = "PEDIDO DE VENDA";
                    FrmFecha.NumDoc               = Vendas.NumDocumento;
                    FrmFecha.IdFilial             = Vendas.IdFilial;
                    FrmFecha.Obs                  = "Faturamento das Vendas:" + RefDocVd;
                    FrmFecha.PagRec               = 2;
                    FrmFecha.FechaPDV             = true;
                    if (Vendas.IdFormaPgto > 0)
                        FrmFecha.IdPgto = Vendas.IdFormaPgto;
                    else
                        FrmFecha.IdPgto = CadPessoa.IdFormaPgto;
                    FrmFecha.LstFormaPgto.Enabled = CadPessoa.BloqFormaPgto == 0;

                    FrmFecha.ShowDialog();

                    if (FrmFecha.Concluido)
                    {                        
                        if (FrmPrincipal.Perfil_Usuario.IdEntregador > 0)
                            Controle.ExecutaSQL("UPDATE MvVenda Set DtHrFaturamento=GetDate(),Id_Entregador=" + FrmPrincipal.Perfil_Usuario.IdEntregador.ToString() + ",Id_Caixa=" + IdCaixa.ToString() + ",Status=2,Id_FormaPgto=" + int.Parse(FrmFecha.LstFormaPgto.SelectedValue.ToString()) + ",Id_VdMaster=" + FrmFecha.NumVd.ToString() + ",VinculoVd='" + RefDocVd.Trim() + "' Where Id_Venda in (" + RefVd + ")");
                        else
                            Controle.ExecutaSQL("UPDATE MvVenda Set DtHrFaturamento=GetDate(),Id_Caixa=" + IdCaixa.ToString() + ",Status=2,Id_FormaPgto=" + int.Parse(FrmFecha.LstFormaPgto.SelectedValue.ToString()) + ",Id_VdMaster=" + FrmFecha.NumVd.ToString() + ",VinculoVd='" + RefDocVd.Trim() + "' Where Id_Venda in (" + RefVd + ")");
                        //Registrando Movimento de Auditoria
                        FrmPrincipal.RegistrarAuditoria(this.Text, Vendas.IdVenda, Vendas.NumDocumento, 6, "Faturamento do Movimento");
                        MessageBox.Show("Movimento concluído", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    FrmFecha.Dispose();
                }
            }
        }
        private void BtnLanc_Click(object sender, EventArgs e)
        {
            int IdCaixa = VerificarCaixa();
            if (IdCaixa == 0)
            {
                MessageBox.Show("Favor verificar o status do Caixa", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                FrmMvCaixaBalcao MvCaixa = new FrmMvCaixaBalcao();
                MvCaixa.FrmPrincipal = FrmPrincipal;
                MvCaixa.IdCaixa      = IdCaixa;
                MvCaixa.ShowDialog();
                MvCaixa.Dispose();
            }
        }

        private void BtnCupom_Click(object sender, EventArgs e)
        {
            //try
            BtnCupom.Enabled = false;
            {
                int IdCaixa = VerificarCaixa();
                if (IdCaixa == 0)
                {
                    MessageBox.Show("Favor verificar o status do Caixa", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    FrmPrincipal.PDV_ImpressoraFiscal.ImpFiscal = FrmPrincipal.TipoImpressoraFiscal();
                    FrmPrincipal.PDV_ImpressoraFiscal.ImpCupomFiscal(int.Parse(NumPedido.Value.ToString()));
                }
            }
            BtnCupom.Enabled = true;
            //catch
            //{
            //    MessageBox.Show("Erro imprimindo cupom fiscal", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
        }

        private void BtnCancCupom_Click(object sender, EventArgs e)
        {
            try
            {
                int IdCaixa = VerificarCaixa();
                if (IdCaixa == 0)
                {
                    MessageBox.Show("Favor verificar o status do Caixa", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    FrmPrincipal.PDV_ImpressoraFiscal.ImpFiscal = FrmPrincipal.TipoImpressoraFiscal();
                    FrmPrincipal.PDV_ImpressoraFiscal.CancelarCupom();
                }
            }
            catch
            {
                MessageBox.Show("Erro imprimindo cupom fiscal", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void BtnImpRecibo_Click(object sender, EventArgs e)
        {
            string sSQL = "SELECT TOP 1 T1.ID_PESSOA,T7.Vencimento, T1.NumDocumento+' NFe: '+rtrim(T9.FormNF) as NumDocumento, T1.VlrTotal as VlrOriginal, T1.PESSOA AS RazaoSocial, RTRIM(T1.Endereco) + ',' + RTRIM(T1.Numero) " +
                          " + ' ' + RTRIM(T1.Complemento) AS ENDERECO, T1.Cep, T1.Bairro, T1.Cidade, T3.Sigla, T1.CNPJCPF as Cnpj," +
                          " T4.FormaPgto,T1.Fone,T8.Vendedor,' ' as Documento,T0.IMAGEM AS LOGO," +
                          " T6.Filial as NomeFilial, RTRIM(T6.Endereco) + ',' + RTRIM(T6.Numero) + ' ' + RTRIM(T6.Complemento) AS ENDFILIAL, T6.Cep AS CepFilial, T6.Bairro as BairroFilial,T6.Cidade as CidFilial,T6.Fone1 AS FoneFilial,T6.Cnpj as CnpjFilial,T6.Insc_UF as InscFilial" +
                          " FROM  MvVenda AS T1 " +
                          " LEFT OUTER JOIN Pessoas AS T2 ON T2.Id_Pessoa = T1.Id_Pessoa " +
                          " LEFT OUTER JOIN Estados AS T3 ON T3.Id_Uf = T2.Id_Uf" +
                          " LEFT OUTER JOIN FormaPagamento AS T4 ON T4.Id_FormaPgto = T1.Id_FormaPgto" +
                          " LEFT OUTER JOIN Empresa_Filial as T6 on T6.Id_Filial=t1.Id_Filial" +
                          " LEFT OUTER JOIN LancFinanceiro as T7 on T7.Id_Venda=T1.Id_Venda" +
                          " LEFT OUTER JOIN Vendedores as T8 on T8.Id_Vendedor=T1.Id_Vendedor" +
                          " LEFT OUTER JOIN MVVENDA AS T9 ON (T9.ID_VENDA=T1.ID_VENDA)" +
                          " LEFT OUTER JOIN TABIMAGENS AS T0 ON (T0.ID_CHAVE=T6.ID_FILIAL AND T0.TABELA='FILIAL')" +
                          "  WHERE T1.ID_VENDA=" + NumPedido.Value.ToString();

            FrmRelatorios FrmRel = new FrmRelatorios();
            Relatorios.RelRecibo RelRec = new Relatorios.RelRecibo();
            DataSet TabRel = new DataSet();
            TabRel = Controle.ConsultaTabela(sSQL);
            RelRec.SetDataSource(TabRel.Tables[0]);
            FrmRel.cryRepRelatorio.ReportSource = RelRec;            
            FrmRel.ShowDialog();            
        }
    }
}
