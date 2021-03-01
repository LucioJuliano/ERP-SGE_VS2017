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
    public partial class FrmLancItensProm : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public int IdPromocao = 0;
        public int IdProduto  = 0;
        public Produtos CadProd;
        public ArrayList ListaCodPrd;
        private PromocaoProdutos Promocao;
        public int IdVenda = 0;
             
        
        public FrmLancItensProm()
        {
            InitializeComponent();
        }

        private void FrmLancItensProm_Load(object sender, EventArgs e)
        {
            Promocao          = new PromocaoProdutos();
            CadProd           = new Produtos();
            Controle.Conexao  = FrmPrincipal.Conexao;
            CadProd.Controle  = Controle;
            Promocao.Controle = Controle;
            CadProd.LerDados(0);            
            ListaCodPrd = new ArrayList();
            ListaCodPrd.Add("0");            
            PopulaGrid();
        }
        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void PopulaGrid()
        {
            DataSet Tabela = new DataSet();
            string sSQL = "SELECT T3.Id_Produto,T3.Descricao,T3.Referencia,0 AS QTDE,T1.PrcSensacional,0.00 as VlrFinal,T1.PrcEspecial,T1.PrcVarejo,T1.PrcMinimo,T1.PrcAtacado FROM PromocaoProdutosItens T1" +
                          "  LEFT JOIN PromocaoProdutos T2 ON (T2.Id_Promocao=T1.Id_Promocao)" +
                          "  LEFT JOIN Produtos T3 ON (T3.Id_Produto=T1.Id_Produto)" +
                          " WHERE T1.ATIVO=1 AND T1.Id_Promocao=" + IdPromocao.ToString()+" ORDER BY T3.DESCRICAO";
            
            Tabela = Controle.ConsultaTabela(sSQL);
            GridDados.DataSource = Tabela;
            GridDados.DataMember = Tabela.Tables[0].TableName;
            if (GridDados.CurrentRow != null)
                GridDados.Focus();
            GridDados.CurrentCell = GridDados.CurrentRow.Cells[3];
            Promocao.LerDados(IdPromocao);

            if (GridDados.CurrentRow != null)
            {
                for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                   GridDados.Rows[I].Cells[5].Value = Math.Round(decimal.Parse(GridDados.Rows[I].Cells[4].Value.ToString()) * ((100 - Promocao.PDesc) / 100), 2);                                 
            }
        }
        private void GridDados_KeyDown(object sender, KeyEventArgs e)
        {
            ListaCodPrd.Clear();
            ListaCodPrd.Add("0");
                        
            if (e.KeyCode == Keys.Enter)
            {
                if (!VerificaQtde())
                    return;

                if (GridDados.CurrentRow != null)
                {
                    IdProduto = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());                    
                    for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                    {
                        if (GridDados.Rows[I].Cells[3].Value.ToString() != "")
                        {
                            if (decimal.Parse(GridDados.Rows[I].Cells[3].Value.ToString()) > 0)
                            {
                                ListaCodPrd.Add(GridDados.Rows[I].Cells[0].Value.ToString() + "|" + GridDados.Rows[I].Cells[3].Value.ToString()+ "|" + GridDados.Rows[I].Cells[5].Value.ToString());
                            }
                        }
                    }            
                }
                else
                {
                    IdProduto = 0;
                }
                Close();
            }
        }

        private bool VerificaQtde()
        {
            decimal QtdeTotal = 0;
            if (GridDados.CurrentRow != null)
            {
                for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                {
                    if (GridDados.Rows[I].Cells[3].Value.ToString() != "")
                        QtdeTotal = QtdeTotal + decimal.Parse(GridDados.Rows[I].Cells[3].Value.ToString());
                }
                if (Promocao.TipoPromocao==4)
                {
                    return true;
                }
                if ((QtdeTotal % Promocao.QtdeTotal) > 0)
                {
                    MessageBox.Show("Favor Verificar a Quantidade Total da Promoção", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                int Qtdekit = int.Parse((QtdeTotal / Promocao.QtdeTotal).ToString()) * Promocao.QtdeItem;

                for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                {
                    if (GridDados.Rows[I].Cells[3].Value.ToString() != "")
                    {
                        if (decimal.Parse(GridDados.Rows[I].Cells[3].Value.ToString()) > Qtdekit)
                        {
                            MessageBox.Show("Favor Verificar a Quantidade do Item: " + GridDados.Rows[I].Cells[2].Value.ToString().Trim(), "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                }

               /* if (Promocao.DescSegUnd == 1)
                {
                    if (!VerificarDescSegundaUND(QtdeTotal))
                        return false;
                    else
                    {

                    }
                }  */              
            }
            return true;
        }

        private bool VerificarDescSegundaUND(decimal QtdeTotal)
        {
            if (GridDados.CurrentRow != null)
            {
                for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                {
                    //SqlDataReader TabQtdeVd = Controle.ConsultaSQL("select Isnull(sum(t1.Qtde),0) as QtdeVd from MvVendaItens t1  " +
                    //                                           " where t1.Id_Produto in (Select id_produto from PromocaoProdutosItens t2 where t2.Id_Promocao=" + IdPromocao.ToString() + ")" +
                    //                                           " and t1.Id_Promocao=0 and t1.id_venda=" + IdVenda.ToString());                
                    if (GridDados.Rows[I].Cells[3].Value.ToString().Trim()!="")
                {
                        if (int.Parse(GridDados.Rows[I].Cells[3].Value.ToString()) > 0)
                        {

                            SqlDataReader TabQtdeVd = Controle.ConsultaSQL("select Isnull(sum(t1.Qtde),0) as QtdeVd from MvVendaItens t1  " +
                                                                       " where t1.Id_Produto in (Select id_produto from PromocaoProdutosItens t2 where t2.Id_Promocao=" + IdPromocao.ToString() + ")" +
                                                                       " and t1.Id_Promocao=1 and t1.id_venda=" + IdVenda.ToString() + " and T1.Id_produto=" + GridDados.Rows[I].Cells[0].Value.ToString());
                            decimal QtdeProm = 0;
                            while (TabQtdeVd.Read())
                                QtdeProm = decimal.Parse(TabQtdeVd["QtdeVd"].ToString());
                             
                            TabQtdeVd = Controle.ConsultaSQL("select Isnull(sum(t1.Qtde),0) as QtdeVd from MvVendaItens t1  " +
                                                                       " where t1.Id_Produto in (Select id_produto from PromocaoProdutosItens t2 where t2.Id_Promocao=" + IdPromocao.ToString() + ")" +
                                                                       " and t1.Id_Promocao=0 and t1.id_venda=" + IdVenda.ToString() + " and T1.Id_produto=" + GridDados.Rows[I].Cells[0].Value.ToString());
                            decimal QtdeVd = 0;
                            while (TabQtdeVd.Read())
                                QtdeVd = decimal.Parse(TabQtdeVd["QtdeVd"].ToString());

                            if (QtdeVd == 0)
                            {
                                MessageBox.Show("Produto principal da Promoção não encontrado no Pedido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            else
                            {
                                if (QtdeVd != int.Parse(GridDados.Rows[I].Cells[3].Value.ToString()) + QtdeProm)
                                {
                                    MessageBox.Show("Favor Verificar a Quantidade dos Itens na Promoção", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                            SqlDataReader TabVlrUnt = Controle.ConsultaSQL("select Top 1 T1.VlrUnitario,T1.PrcSensacional from MvVendaItens t1  " +
                                                                           " where t1.Id_Produto in (Select id_produto from PromocaoProdutosItens t2 where t2.Id_Promocao=" + IdPromocao.ToString() + ")" +
                                                                           " and t1.Id_Promocao=0 and t1.id_venda=" + IdVenda.ToString() + " and T1.Id_produto=" + GridDados.Rows[I].Cells[0].Value.ToString());

                            decimal VlrUnt = 0;
                            while (TabVlrUnt.Read())
                            {
                                VlrUnt = decimal.Parse(TabVlrUnt["VlrUnitario"].ToString());

                                if (VlrUnt < decimal.Parse(TabVlrUnt["PrcSensacional"].ToString()))
                                {
                                    MessageBox.Show("Favor Verificar o Preço do Produto principal", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }


                            //if (GridDados.CurrentRow != null)
                            //{
                            //    for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                            GridDados.Rows[I].Cells[5].Value = Math.Round(VlrUnt * ((100 - Promocao.PDesc) / 100), 2);
                            // }
                        }
                    }
                }
            }
            return true;
        }

        private void GridDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Totalizar();
        }

        private void Totalizar()
        {
            int Qtde      = 0;
            decimal Total = 0;

            if (GridDados.CurrentRow != null)
            {
                for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                {
                    if (GridDados.Rows[I].Cells[3].Value.ToString() != "")
                    {
                        if (decimal.Parse(GridDados.Rows[I].Cells[3].Value.ToString()) > 0)
                        {                            
                            Qtde = Qtde + int.Parse(GridDados.Rows[I].Cells[3].Value.ToString());
                            if (Promocao.TipoPromocao != 4)
                            {
                                GridDados.Rows[I].Cells[5].Value = Math.Round(decimal.Parse(GridDados.Rows[I].Cells[4].Value.ToString()) * ((100 - Promocao.PDesc) / 100), 2);
                                Total = Total + (Math.Round(decimal.Parse(GridDados.Rows[I].Cells[4].Value.ToString()) * ((100 - Promocao.PDesc) / 100), 2) * int.Parse(GridDados.Rows[I].Cells[3].Value.ToString()));
                            }
                        }
                    }
                }
                if (Promocao.TipoPromocao == 4)
                {
                    for (int I = 0; I <= GridDados.Rows.Count - 1; I++)
                    {
                        if (GridDados.Rows[I].Cells[3].Value.ToString() != "")
                        {
                            if (decimal.Parse(GridDados.Rows[I].Cells[3].Value.ToString()) > 0)
                            {
                                GridDados.Rows[I].Cells[5].Value = GridDados.Rows[I].Cells[4].Value.ToString();

                                if (Qtde >= Promocao.QtdeEsp && Promocao.QtdeEsp > 0)
                                    GridDados.Rows[I].Cells[5].Value = GridDados.Rows[I].Cells[6].Value.ToString();
                                if (Qtde >= Promocao.QtdeVar && Promocao.QtdeVar > 0)
                                    GridDados.Rows[I].Cells[5].Value = GridDados.Rows[I].Cells[7].Value.ToString();
                                if (Qtde >= Promocao.QtdeMin && Promocao.QtdeMin > 0)
                                    GridDados.Rows[I].Cells[5].Value = GridDados.Rows[I].Cells[8].Value.ToString();
                                if (Qtde >= Promocao.QtdeAta && Promocao.QtdeAta > 0)
                                    GridDados.Rows[I].Cells[5].Value = GridDados.Rows[I].Cells[9].Value.ToString();
                                
                                Total = Total + (decimal.Parse(GridDados.Rows[I].Cells[5].Value.ToString()) * int.Parse(GridDados.Rows[I].Cells[3].Value.ToString()));
                            }
                        }
                    }

                }
            }
            
            LblItens.Text = Qtde.ToString();
            LblTotal.Text = string.Format("{0:n2}", Total);
        }
    }
}
