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
using System.Collections;
using System.Data.SqlClient;
using System.Xml;

namespace ERP_SGE
{
    public partial class FrmReajustePrc : Form
    {

        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public Produtos CadProd;
        public int IdGrupo = 0;
        private DataSet TabItens;
        private BindingSource Source_Itens;
        public FrmReajustePrc()
        {
            InitializeComponent();
        }

        private void PopularGrid()
        {
            DataSet Tabela = new DataSet();
            string sSQL = "SELECT T1.Id_Produto,T1.Descricao,T1.Referencia,T1.UltPrcCompra,T1.PrcAtacado,T1.PrcMinimo,T1.PrcVarejo,T1.PrcEspecial,T1.PrcSensacional," +
                          "T1.AntPrcCompra,T1.AntAtacado,T1.AntMinimo,T1.AntVarejo,T1.AntEspecial,T1.AntSensacional FROM Produtos T1 Where T1.Ativo=1 and T1.Id_Grupo=" + IdGrupo.ToString();

            if (TxtDescricao.Text.Trim() != "")
                sSQL = string.Format(sSQL + " and T1.Descricao LIKE '%{0}%' ", TxtDescricao.Text.Trim());

            sSQL = sSQL + " Order By T1.Descricao";

            TabItens = Controle.ConsultaTabela(sSQL);
            Source_Itens.DataSource = TabItens;
            Source_Itens.DataMember = TabItens.Tables[0].TableName;
            GridDados.DataSource    = Source_Itens;
        }

        private void GridDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                LblPrcSensac.Text   = "P.S: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["PSensacional"].Value.ToString()));
                LblPrcEspecial.Text = "P.E: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["PEspecial"].Value.ToString()));
                LblPrcVarejo.Text   = "P.V: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["PVarejo"].Value.ToString()));
                LblPrcMinimo.Text   = "P.M: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["PMinimo"].Value.ToString()));
                LblPrcDist.Text     = "P.D: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["PAtacado"].Value.ToString()));
                LblPrcCusto.Text    = "P.C: " + string.Format("{0:N2}", decimal.Parse(GridDados.CurrentRow.Cells["PCusto"].Value.ToString()));
            }
        }

        private void FrmReajustePrc_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            CadProd          = new Produtos();
            CadProd.Controle = Controle;
            TabItens         = new DataSet();
            Source_Itens     = new BindingSource();
            LstTipoReajuste.SelectedIndex = 0;
            Cb_Arredonda.Checked = true;
            PopularGrid();
        }

        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void BtnReajuste_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma aplicação do Reajuste ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ProcBar.Maximum = GridDados.SelectedRows.Count;
                    ProcBar.Value = 0;
                    int IdProduto = 0;

                    for (int I = 0; I <= GridDados.SelectedRows.Count - 1; I++)
                    {
                        IdProduto = int.Parse(GridDados.SelectedRows[I].Cells[0].Value.ToString());
                        CadProd.LerDados(IdProduto);
                        CadProd.DtAlteracao = DateTime.Now;
                        Controle.ExecutaSQL("Update Produtos set AntPrcCompra=rtrim(UltPrcCompra),AntAtacado=rtrim(PrcAtacado),AntMinimo=rtrim(PrcMinimo),AntVarejo=rtrim(PrcVarejo),AntEspecial=rtrim(PrcEspecial),antsensacional=rtrim(PrcSensacional) where Id_produto=" + IdProduto.ToString());
                            
                            if (TxtCusto.Value > 0)
                        {
                            if (Cb_Arredonda.Checked)
                                CadProd.UltPrcCompra = Math.Round(CadProd.UltPrcCompra * (1 + (TxtCusto.Value / 100)), 1);
                            else
                                CadProd.UltPrcCompra = CadProd.UltPrcCompra * (1 + (TxtCusto.Value / 100));
                        }
                        if (LstTipoReajuste.SelectedIndex == 0)
                        {
                            if (TxtAtacado.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcAtacado = Math.Round(CadProd.PrcAtacado * (1 + (TxtAtacado.Value / 100)), 1);
                                else
                                    CadProd.PrcAtacado = CadProd.PrcAtacado * (1 + (TxtAtacado.Value / 100));
                            }
                            if (TxtMinimo.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcMinimo = Math.Round(CadProd.PrcMinimo * (1 + (TxtMinimo.Value / 100)), 1);
                                else
                                    CadProd.PrcMinimo = CadProd.PrcMinimo * (1 + (TxtMinimo.Value / 100));
                            }
                            if (TxtVarejo.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcVarejo = Math.Round(CadProd.PrcVarejo * (1 + (TxtVarejo.Value / 100)), 1);
                                else
                                    CadProd.PrcVarejo = CadProd.PrcVarejo * (1 + (TxtVarejo.Value / 100));
                            }
                            if (TxtEspecial.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcEspecial = Math.Round(CadProd.PrcEspecial * (1 + (TxtEspecial.Value / 100)), 1);
                                else
                                    CadProd.PrcEspecial = CadProd.PrcEspecial * (1 + (TxtEspecial.Value / 100));
                            }
                            if (TxtSensacional.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcSensacional = Math.Round(CadProd.PrcSensacional * (1 + (TxtSensacional.Value / 100)), 1);
                                else
                                    CadProd.PrcSensacional = CadProd.PrcSensacional * (1 + (TxtSensacional.Value / 100));
                            }
                        }
                        if (LstTipoReajuste.SelectedIndex == 1)
                        {
                            if (TxtAtacado.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcAtacado = Math.Round(CadProd.UltPrcCompra * (1 + (TxtAtacado.Value / 100)), 1);
                                else
                                    CadProd.PrcAtacado = CadProd.UltPrcCompra * (1 + (TxtAtacado.Value / 100));
                            }
                            if (TxtMinimo.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcMinimo = Math.Round(CadProd.UltPrcCompra * (1 + (TxtMinimo.Value / 100)), 1);
                                else
                                    CadProd.PrcMinimo = CadProd.UltPrcCompra * (1 + (TxtMinimo.Value / 100));
                            }
                            if (TxtVarejo.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcVarejo = Math.Round(CadProd.UltPrcCompra * (1 + (TxtVarejo.Value / 100)), 1);
                                else
                                    CadProd.PrcVarejo = CadProd.UltPrcCompra * (1 + (TxtVarejo.Value / 100));
                            }
                            if (TxtEspecial.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcEspecial = Math.Round(CadProd.UltPrcCompra * (1 + (TxtEspecial.Value / 100)), 1);
                                else
                                    CadProd.PrcEspecial = CadProd.UltPrcCompra * (1 + (TxtEspecial.Value / 100));
                            }
                            if (TxtSensacional.Value > 0)
                            {
                                if (Cb_Arredonda.Checked)
                                    CadProd.PrcSensacional = Math.Round(CadProd.UltPrcCompra * (1 + (TxtSensacional.Value / 100)), 1);
                                else
                                    CadProd.PrcSensacional = CadProd.UltPrcCompra * (1 + (TxtSensacional.Value / 100));
                            }
                        }
                        ProcBar.Value = ProcBar.Value + 1;
                        CadProd.GravarDados();
                    }
                    PopularGrid();
                }
            }
        }

        private void TxtDescricao_Validated(object sender, EventArgs e)
        {
            PopularGrid();
        }

        private void BtnDesfazer_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Desfazer o Reajuste ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ProcBar.Maximum = GridDados.SelectedRows.Count;
                    ProcBar.Value = 0;
                    int IdProduto = 0;

                    for (int I = 0; I <= GridDados.SelectedRows.Count - 1; I++)
                    {
                        IdProduto = int.Parse(GridDados.SelectedRows[I].Cells[0].Value.ToString());
                        Controle.ExecutaSQL("Update Produtos set UltPrcCompra=rtrim(AntPrcCompra),PrcAtacado=rtrim(AntAtacado),PrcMinimo=rtrim(AntMinimo),PrcVarejo=rtrim(AntVarejo),PrcEspecial=rtrim(AntEspecial),PrcSensacional=rtrim(AntSensacional) where Id_produto=" + IdProduto.ToString());
                        ProcBar.Value = ProcBar.Value + 1;                        
                    }
                    PopularGrid();
                }
            }
        }
    }
}
