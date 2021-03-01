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
    public partial class FrmImpBoleto : Form
    {
        Funcoes Controle = new Funcoes();
        MvVenda Vendas = new MvVenda();
        FormaPagamento FormaPgto = new FormaPagamento();

        public TelaPrincipal FrmPrincipal;
        public bool ImpBoleto;
        public bool ImpProm = false;

        public FrmImpBoleto()
        {
            InitializeComponent();            
            ImpBoleto = true;
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (TxtIdVenda.Value > 0)
            {
                Vendas.LerDados(int.Parse(TxtIdVenda.Value.ToString()));
                if (Vendas.IdVenda == 0)
                {
                    MessageBox.Show("Venda não localizada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string sSQL = "";
                    if (!ImpBoleto && Rb_Sim.Checked)
                    {
                        sSQL = "SELECT TOP 1 T1.ID_PESSOA,T7.Vencimento, T1.NumDocumento+' NFe: '+rtrim(T9.FormNF) as NumDocumento, T1.VlrTotal as VlrOriginal, T1.PESSOA AS RazaoSocial, RTRIM(T1.Endereco) + ',' + RTRIM(T1.Numero) " +
                                      " + ' ' + RTRIM(T1.Complemento) AS ENDERECO, T1.Cep, T1.Bairro, T1.Cidade, T3.Sigla, T1.CNPJCPF as Cnpj," +
                                      " T4.FormaPgto,T1.Fone,T8.Vendedor,' ' as Documento,T0.IMAGEM AS LOGO," +
                                      " T6.Filial as NomeFilial, RTRIM(T6.Endereco) + ',' + RTRIM(T6.Numero) + ' ' + RTRIM(T6.Complemento) AS ENDFILIAL, T6.Cep AS CepFilial, T6.Bairro as BairroFilial,T6.Cidade as CidFilial,T6.Fone1 AS FoneFilial,T6.Cnpj as CnpjFilial,T6.Insc_UF as InscFilial"+
                                      " FROM  MvVenda AS T1 " +
                                      " LEFT OUTER JOIN Pessoas AS T2 ON T2.Id_Pessoa = T1.Id_Pessoa " +
                                      " LEFT OUTER JOIN Estados AS T3 ON T3.Id_Uf = T2.Id_Uf" +
                                      " LEFT OUTER JOIN FormaPagamento AS T4 ON T4.Id_FormaPgto = T1.Id_FormaPgto" +
                                      " LEFT OUTER JOIN Empresa_Filial as T6 on T6.Id_Filial=t1.Id_Filial" +
                                      " LEFT OUTER JOIN LancFinanceiro as T7 on T7.Id_Venda=T1.Id_Venda" +
                                      " LEFT OUTER JOIN Vendedores as T8 on T8.Id_Vendedor=T1.Id_Vendedor" +
                                      " LEFT OUTER JOIN MVVENDA AS T9 ON (T9.ID_VENDA=T1.ID_VENDA)" +
                                      " LEFT OUTER JOIN TABIMAGENS AS T0 ON (T0.ID_CHAVE=T6.ID_FILIAL AND T0.TABELA='FILIAL')" +
                                      "  WHERE T1.ID_VENDA=" + Vendas.IdVdMaster.ToString();
                    }
                    else
                    {
                        if (ImpProm)
                            sSQL = "SELECT  T1.ID_PESSOA,T1.DataLanc, T1.Vencimento, T1.NumDocumento, T1.VlrOriginal, T2.RazaoSocial, RTRIM(T2.Endereco) + ',' + RTRIM(T2.Numero) ";
                        else
                            sSQL = "SELECT  T1.ID_PESSOA,T1.DataLanc, T1.Vencimento, T1.NumDocumento+' NFe: '+rtrim(T9.FormNF) as NumDocumento, T1.VlrOriginal, T2.RazaoSocial, RTRIM(T2.Endereco) + ',' + RTRIM(T2.Numero) ";

                            sSQL = sSQL + " + ' ' + RTRIM(T2.Complemento) AS ENDERECO, T2.Cep, T2.Bairro, T2.Cidade, T3.Sigla, T2.Cnpj, T2.Insc_UF,(((T1.VlrOriginal / 30) * 10) / 100) AS MultaBol," +
                                          " T4.FormaPgto,T2.Fone,T5.Documento, RTRIM(T1.LinhaBoleto) AS LinhaDigBoleto, RTRIM(T1.CodBarraBoleto) AS CodBarraBoleto, RTRIM(T1.NossoNumero) AS NossoNumeroBoleto," +
                                          " T6.FANTASIA AS Filial,T6.Juros as JuroBoleto,T6.Multa as MultaBoleto,T6.Instrucao,T7.NumAgencia,T7.Conta,T7.DigConta,T8.Vendedor,T0.IMAGEM AS LOGO, " +
                                          " T6.Filial as NomeFilial, RTRIM(T6.Endereco) + ',' + RTRIM(T6.Numero) + ' ' + RTRIM(T6.Complemento) AS ENDFILIAL, T6.Cep AS CepFilial, T6.Bairro as BairroFilial,T6.Cidade as CidFilial,T6.Fone1 AS FoneFilial,T6.Cnpj as CnpjFilial,T6.Insc_UF as InscFilial" +
                                          " FROM  LancFinanceiro AS T1 " +
                                          " LEFT OUTER JOIN Pessoas AS T2 ON T2.Id_Pessoa = T1.Id_Pessoa " +
                                          " LEFT OUTER JOIN Estados AS T3 ON T3.Id_Uf = T2.Id_Uf" +
                                          " LEFT OUTER JOIN FormaPagamento AS T4 ON T4.Id_FormaPgto = T1.Id_FormaPgto" +
                                          " LEFT OUTER JOIN TipoDocumento AS T5 ON T5.Id_Documento = T1.Id_TipoDocumento" +
                                          " LEFT OUTER JOIN Empresa_Filial as T6 on T6.Id_Filial=t1.Id_Filial" +
                                          " LEFT OUTER JOIN Bancos as T7 on T7.Id_Banco=T6.Id_Banco" +
                                          " LEFT OUTER JOIN MVVENDA AS T9 ON (T9.ID_VENDA=T1.ID_VENDA)" +
                                          " LEFT OUTER JOIN Vendedores as T8 on T8.Id_Vendedor=T9.Id_Vendedor" +
                                          " LEFT OUTER JOIN TABIMAGENS AS T0 ON (T0.ID_CHAVE=T6.ID_FILIAL AND T0.TABELA='FILIAL')" +
                                          "  WHERE T1.ID_VENDA=" + Vendas.IdVdMaster.ToString();
                    }
                    if (ImpBoleto)
                    {
                        FormaPgto.LerDados(Vendas.IdFormaPgto);
                        if (FormaPgto.PrimParcela <= 1)
                        {
                            MessageBox.Show("Boleto não pode ser impresso, Verificar a Forma de pagamento", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        BtnImprimir.Enabled = false;                        
                        if (Rb_BoletoA4.Checked)
                        {
                            FrmRelatorios FrmRel = new FrmRelatorios();
                            Relatorios.RelBoletoA4 RelBoleto = new Relatorios.RelBoletoA4();
                            DataSet TabRel = new DataSet();
                            TabRel = Controle.ConsultaTabela(sSQL);
                            RelBoleto.SetDataSource(TabRel.Tables[0]);
                            FrmRel.cryRepRelatorio.ReportSource = RelBoleto;
                            FrmRel.ShowDialog();
                        }
                        else
                        {
                            FrmRelatorios FrmRel = new FrmRelatorios();
                            Relatorios.RelBoleto RelBoleto = new Relatorios.RelBoleto();                            
                            DataSet TabRel = new DataSet();
                            TabRel = Controle.ConsultaTabela(sSQL);
                            RelBoleto.SetDataSource(TabRel.Tables[0]);
                            FrmRel.cryRepRelatorio.ReportSource = RelBoleto;
                            FrmRel.ShowDialog();
                        }
                        BtnImprimir.Enabled = true;
                    }
                    else 
                    {
                        //NumeroPorExtenso Extenso = new NumeroPorExtenso(Vendas.VlrTotal);
                        if (!ImpProm)
                        {
                            FrmRelatorios FrmRel = new FrmRelatorios();
                            Relatorios.RelRecibo RelRec = new Relatorios.RelRecibo();
                            DataSet TabRel = new DataSet();
                            TabRel = Controle.ConsultaTabela(sSQL);
                            RelRec.SetDataSource(TabRel.Tables[0]);
                            FrmRel.cryRepRelatorio.ReportSource = RelRec;
                            FrmRel.ShowDialog();
                        }
                        else
                        {
                            string sSQLPrd = "Select rtrim(T2.descricao) as Descricao from MvVendaItens t1 left join Produtos t2 on (T2.id_produto=T1.Id_Produto) where t1.id_Venda=" + Vendas.IdVdMaster.ToString();

                            FrmRelatorios FrmRel = new FrmRelatorios();
                            Relatorios.RelPromissoria RelRec = new Relatorios.RelPromissoria();
                            DataSet TabRel = new DataSet();
                            DataSet TabRelPrd = new DataSet();
                            TabRel = Controle.ConsultaTabela(sSQL);
                            TabRelPrd = Controle.ConsultaTabela(sSQLPrd);
                            //RelRec.SetDataSource(TabRel.Tables[0]);
                            RelRec.Database.Tables[0].SetDataSource(TabRel.Tables[0]);
                            RelRec.Database.Tables[1].SetDataSource(TabRelPrd.Tables[0]);
                            FrmRel.cryRepRelatorio.ReportSource = RelRec;
                            FrmRel.ShowDialog();
                        }
                        BtnImprimir.Enabled = false;                        
                    }             
                }
            }
            BtnImprimir.Enabled = true;
        }

        private void FrmImpBoleto_Load(object sender, EventArgs e)
        {
            Controle.Conexao      = FrmPrincipal.Conexao;
            Vendas.Controle       = Controle;
            FormaPgto.Controle    = Controle;
            Rb_BoletoForm.Checked = true;
            //BoxBoleto.Visible     = false;
            BoxRecibo.Visible     = !ImpBoleto && !ImpProm;
            Rb_Nao.Checked        = true;
            Rb_BoletoA4.Checked   = true;
        }

        private void FrmImpBoleto_Shown(object sender, EventArgs e)
        {
            if (ImpBoleto)
                this.Text = "Imprimir Boleto de Venda";
            else
                this.Text = "Imprimir Recibo de Venda";
            //BoxBoleto.Visible = ImpBoleto;
        }
    }
}
