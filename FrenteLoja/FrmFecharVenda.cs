using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP_SGE;
using Controle_Dados;
using Controles;

namespace FrenteLoja
{
    public partial class FrmFecharVenda : Form
    {
        Funcoes Controle = new Funcoes();
        TipoDocumento TpDoc = new TipoDocumento();
        public FrmFrenteLoja FrmFrenteLoja;
        public decimal VlrRecido = 0;
        public bool VendaFechada = false;
        public DataTable Pagamento;
        public DataTable PagCartao;

        public FrmFecharVenda()
        {
            InitializeComponent();
        }

        private void FrmFecharVenda_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmFrenteLoja.FrmPrincipal.Conexao;
            TpDoc.Controle = Controle;
            if (FrmFrenteLoja.FrmPrincipal.Parametros_Filial.EmissorCF == 3)
                LstTipoDoc = FrmFrenteLoja.FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento where MFE > 0 ORDER BY Id_Documento", LstTipoDoc, "");
            else
                LstTipoDoc = FrmFrenteLoja.FrmPrincipal.PopularCombo("SELECT ID_Documento,Documento FROM TipoDocumento ORDER BY Id_Documento", LstTipoDoc, "");
            LstTipoDoc.SelectedIndex = 0;
            LstTipoDoc.Focus();
            TxtDesconto.Value = FrmFrenteLoja.DescVenda;            
            //Criando a Tebale de Lançamento dos Pagamentos
            Pagamento = new DataTable();
            Pagamento.Columns.Add("ID_Documento", Type.GetType("System.Int32"));
            Pagamento.Columns.Add("Documento", Type.GetType("System.String"));
            Pagamento.Columns.Add("Adquirente", Type.GetType("System.String"));
            Pagamento.Columns.Add("VlrDoc", Type.GetType("System.Decimal"));
            Pagamento.Columns.Add("Vencimento", Type.GetType("System.DateTime"));
            Pagamento.Columns.Add("Baixa", Type.GetType("System.Int32"));
            Pagamento.Columns.Add("MFE", Type.GetType("System.Int32"));
            Pagamento.Columns.Add("IdPagMFe", Type.GetType("System.Int32"));
            Pagamento.Columns.Add("IdRespMFe", Type.GetType("System.Int32"));
            Pagamento.Columns.Add("IdFinanc", Type.GetType("System.Int32"));
            Pagamento.Columns.Add("ChaveCFe", Type.GetType("System.String"));
            // Pagamento Cartão
            PagCartao = new DataTable();
            PagCartao.Columns.Add("IdLanc",       Type.GetType("System.Int32"));
            PagCartao.Columns.Add("ID_Documento", Type.GetType("System.Int32"));
            PagCartao.Columns.Add("Documento",    Type.GetType("System.String"));
            PagCartao.Columns.Add("Adquirente",   Type.GetType("System.String"));
            PagCartao.Columns.Add("ID_Nota",      Type.GetType("System.Int32"));
            PagCartao.Columns.Add("VlrDoc",       Type.GetType("System.Decimal"));
            PagCartao.Columns.Add("IdPagMFe",     Type.GetType("System.Int32"));
            PagCartao.Columns.Add("IdRespMFe",    Type.GetType("System.Int32"));            
            PagCartao.Columns.Add("ChaveCFe",     Type.GetType("System.String"));
            PagCartao.Columns.Add("MFE",          Type.GetType("System.Int32"));
            PagCartao.Columns.Add("NParc",        Type.GetType("System.Int32"));

            AtualizarValores();
            /*
            StringBuilder cupom = new StringBuilder(200);
            cupom.Append(' ', 40);
            cupom.Append('-', 11);
            cupom.Append('\n');
            cupom.Append("TOTAL R$");
            cupom.AppendFormat(FrmFrenteLoja.TotVenda.ToString("N").PadLeft(43, ' '));
            cupom.Append('\n');
            FrmFrenteLoja.AtualizarDisplay(cupom.ToString());*/
        }

        private void AtualizarValores()
        {
            LblTotalVenda.Text = string.Format("{0:N2}", FrmFrenteLoja.TotVenda - TxtDesconto.Value);
            LblVlrRec.Text     = string.Format("{0:N2}", VlrRecido);
            LblVlrDif.Text     = string.Format("{0:N2}", VlrRecido - (Math.Round(FrmFrenteLoja.TotVenda,2) - TxtDesconto.Value));
            Application.DoEvents();
            //
            if (VlrRecido >= (Math.Round(FrmFrenteLoja.TotVenda,2) - TxtDesconto.Value))
            {
                if (FrmFrenteLoja.FrmPrincipal.Parametros_Filial.EmissorCF == 3)
                {
                    for (int I = 0; I <= Pagamento.Rows.Count - 1; I++)
                    {
                        if (int.Parse(Pagamento.Rows[I]["MFE"].ToString()) == 3 || int.Parse(Pagamento.Rows[I]["MFE"].ToString()) == 4)
                        {
                            if (Math.Round(VlrRecido,2) > (Math.Round(FrmFrenteLoja.TotVenda,2) - TxtDesconto.Value))
                            {
                                MessageBox.Show("Valor Recebibo maior que o valor da venda", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Pagamento.Rows.Clear();
                                PagCartao.Rows.Clear();
                                VlrRecido = 0;
                                LblVlrRec.Text = string.Format("{0:N2}", VlrRecido);
                                LblVlrDif.Text = string.Format("{0:N2}", VlrRecido - (FrmFrenteLoja.TotVenda - TxtDesconto.Value));
                                return;
                            }
                        }
                    }
                }
                VendaFechada = true;
                Close();
            }
        }

        private void LstTipoDoc_Enter(object sender, EventArgs e)
        {
            TxtVlrMov.Enabled = false;
            TxtVlrMov.Value   = 0;
            TxtNParc.Value    = 0;
            TxtNParc.Enabled  = false;
        }
        private void TxtVlrMov_Validated(object sender, EventArgs e)
        {
            if (TxtVlrMov.Text.Trim() == "")
                TxtVlrMov.Value = 0;

            if (TxtVlrMov.Value == 0)
                return;


            if (MessageBox.Show("Confirma o valor recebido ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TpDoc.LerDados(int.Parse(LstTipoDoc.SelectedValue.ToString()));
                VlrRecido = VlrRecido + Math.Round(TxtVlrMov.Value, 2);

                PagCartao.Rows.Add(0, int.Parse(LstTipoDoc.SelectedValue.ToString()), LstTipoDoc.SelectedText.Trim(), TpDoc.Adquirente, 0, TxtVlrMov.Value, 0, 0, "", TpDoc.MFe, TxtNParc.Value);

                if (TpDoc.MFe == 3)
                {
                    int NParc = 1;
                    if (TxtNParc.Value > 0)
                        NParc = int.Parse(TxtNParc.Value.ToString());

                    DateTime DtVenc    = DateTime.Now.Date.AddDays(30);
                    decimal VrParc     = Math.Round(TxtVlrMov.Value / NParc, 2);
                    decimal VrPrimParc = VrParc + (TxtVlrMov.Value - (Math.Round(VrParc * NParc, 2)));
                    int NumParcelas    = 1;

                    while (NumParcelas <= NParc)
                    {
                        Pagamento.Rows.Add(int.Parse(LstTipoDoc.SelectedValue.ToString()), LstTipoDoc.SelectedText.Trim(), TpDoc.Adquirente, VrPrimParc, DtVenc.Date, TpDoc.Baixa, TpDoc.MFe, 0, 0, 0, "");
                        VrPrimParc = VrParc;
                        DtVenc = DtVenc.AddDays(30);
                        NumParcelas++;
                    }
                }
                else
                   Pagamento.Rows.Add(int.Parse(LstTipoDoc.SelectedValue.ToString()), LstTipoDoc.SelectedText.Trim(), TpDoc.Adquirente, TxtVlrMov.Value, DateTime.Now.Date, TpDoc.Baixa, TpDoc.MFe, 0, 0, 0, "");

                /*StringBuilder cupom = new StringBuilder(105);
                cupom.AppendFormat("{0,-16:G}", Controle.Space(LstTipoDoc.SelectedText.ToString(),20));
                cupom.AppendFormat(TxtVlrMov.Value.ToString("N").PadLeft(31, ' '));
                cupom.Append('\n');
                FrmFrenteLoja.AtualizarDisplay(cupom.ToString());*/
            }
            AtualizarValores();
        }
        private void TxtVlrMov_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                LstTipoDoc.Focus();
            }
        }
        private void LstTipoDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                if (LstTipoDoc.SelectedValue == null)
                {
                    MessageBox.Show("Favor Selecione uma Forma de Pagamento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                TpDoc.LerDados(int.Parse(LstTipoDoc.SelectedValue.ToString()));
                TxtVlrMov.Enabled = true;
                TxtVlrMov.Text    = "";
                TxtNParc.Value    = 0;
                //TxtData.Value     = DateTime.Now.AddDays(TpDoc.Dias);
                //TxtData.Visible   = TpDoc.Dias > 0;
                //LblVenc.Visible   = TpDoc.Dias > 0;
                if (TpDoc.MFe == 3)
                {
                    TxtNParc.Enabled = true;
                    TxtNParc.Value = 0;
                    TxtNParc.Focus();
                }
                else
                    TxtVlrMov.Focus();
            }
        }

        private void TxtDesconto_Validated(object sender, EventArgs e)
        {
            FrmFrenteLoja.DescVenda = TxtDesconto.Value;
            AtualizarValores();
        }

        private void LstTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            VendaFechada = false;
            Close();
        }
    }
}
