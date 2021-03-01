using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrenteLoja
{
    public partial class FrmCancelarItem : Form
    {        
        public FrmFrenteLoja FrmFrenteLoja;

        public FrmCancelarItem()
        {
            InitializeComponent();
        }
        private void TxtNumItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                this.BtnConfirmar.Focus();
            }
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                SendKeys.Send("");
            }
        }
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (TxtNumItem.Text.Trim() == "")
            {
                MessageBox.Show("Favor informar o numero do item", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtNumItem.Focus();
                return;
            }
            int NumItem = -1;
            for (int I = 0; I <= FrmFrenteLoja.TabItens.Rows.Count - 1; I++)
            {
                if (int.Parse(FrmFrenteLoja.TabItens.Rows[I]["ITEM"].ToString()) == int.Parse(TxtNumItem.Text))
                {
                    if (int.Parse(FrmFrenteLoja.TabItens.Rows[I]["STATUS"].ToString()) == 0)
                    {
                        MessageBox.Show("Item já cancelado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtNumItem.Focus();
                        return;
                    }
                    else
                    {
                        NumItem = I;
                        break;
                    }
                }
            }

            if (NumItem == -1)
            {
                MessageBox.Show("Item não localizado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtNumItem.Focus();
            }
            else
            {
                StringBuilder cupom = new StringBuilder(30);
                cupom.Append("Item Cancelado: ");
                cupom.Append(TxtNumItem.Text.PadLeft(3, '0'));
                cupom.Append(decimal.Negate(decimal.Parse(FrmFrenteLoja.TabItens.Rows[NumItem]["VLRTOTAL"].ToString())).ToString("N").PadLeft(32, ' '));
                cupom.Append('\n');
                FrmFrenteLoja.TotItens = FrmFrenteLoja.TotItens - 1;
                FrmFrenteLoja.TotVenda = FrmFrenteLoja.TotVenda - decimal.Parse(FrmFrenteLoja.TabItens.Rows[NumItem]["VLRTOTAL"].ToString());
                FrmFrenteLoja.TabItens.Rows[NumItem]["STATUS"] = 0;
                FrmFrenteLoja.AtualizarDisplay(cupom.ToString());
                Close();
            }
        }

        private void BtnCancMov_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
