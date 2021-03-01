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
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace FrenteLoja
{
    public partial class FrmConsVenda : Form
    {
        Funcoes Controle = new Funcoes();
        public FrmFrenteLoja FrmFrenteLoja;
        public FrmConsVenda()
        {
            InitializeComponent();
        }

        private void FrmConsVenda_Load(object sender, EventArgs e)
        {            
            Controle.Conexao = FrmFrenteLoja.FrmPrincipal.Conexao;            
            PopuparGrid();
        }

        private void PopuparGrid()
        {
            FrmFrenteLoja.IdVdImp = 0;
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_VENDA,CASE T1.STATUS WHEN 1 THEN 'Confirmada' WHEN 2 THEN 'Faturada' END AS STATUS," +
                                             " T1.NUMDOCUMENTO,T1.PESSOA,T1.VLRTOTAL FROM MVVENDA T1 " +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) "+
                                             " WHERE T1.TPVENDA='PV' AND T1.ID_CAIXA=0 and t1.Status in (1,2) ORDER BY T1.ID_VENDA DESC");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
        }
               

        private void BtnImportar_Click(object sender, EventArgs e)
        {
            FrmFrenteLoja.IdVdImp = 0;
            FrmFrenteLoja.Vendas.LerDados(0);
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a importação da Venda ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GridDados.Enabled = false;
                    Application.DoEvents();
                    try
                    {
                        SqlDataReader TabVenda = Controle.ConsultaSQL("SELECT T2.ID_PESSOA,T2.ID_VENDEDOR,T1.* FROM MVVENDAITENS T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA) WHERE T1.id_venda =" + GridDados.CurrentRow.Cells[0].Value.ToString());
                        while (TabVenda.Read())
                        {
                            FrmFrenteLoja.CadPrd.LerDados(int.Parse(TabVenda["ID_Produto"].ToString()));
                            FrmFrenteLoja.FrmPesqPessoa.CadPessoa.LerDados(int.Parse(TabVenda["ID_Pessoa"].ToString()));
                            FrmFrenteLoja.TxtQtde.Value   = decimal.Parse(TabVenda["Qtde"].ToString());
                            FrmFrenteLoja.TxtVlrUnt.Value = decimal.Parse(TabVenda["VlrUnitario"].ToString());
                            FrmFrenteLoja.IdPessoa        = FrmFrenteLoja.FrmPesqPessoa.CadPessoa.IdPessoa;
                            FrmFrenteLoja.LblCliente.Text = "Cliente: " + FrmFrenteLoja.FrmPesqPessoa.CadPessoa.RazaoSocial;
                            FrmFrenteLoja.LstVendedor.SelectedValue = int.Parse(TabVenda["ID_Vendedor"].ToString());
                            FrmFrenteLoja.RegistrarItem(int.Parse(TabVenda["ID_Item"].ToString()));
                            FrmFrenteLoja.TxtQtde.Value   = 1;
                            FrmFrenteLoja.TxtVlrUnt.Value = 0;
                        }
                        FrmFrenteLoja.IdVdImp = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                        FrmFrenteLoja.Vendas.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                        Close();
                    }
                    catch (Exception er)
                    {
                        GridDados.Enabled = true;
                        MessageBox.Show("Atenção: Ocorreu um erro na importação: " + er.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }
    }
}


