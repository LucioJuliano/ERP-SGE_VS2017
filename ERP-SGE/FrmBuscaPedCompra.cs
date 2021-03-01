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
    public partial class FrmBuscaPedCompra : Form
    {
        Funcoes Controle = new Funcoes();
        public PedCompra MvPedCompra;
        public TelaPrincipal FrmPrincipal;

        public FrmBuscaPedCompra()
        {
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            //Instanciando os Controles
            Controle.Conexao = FrmPrincipal.Conexao;
            MvPedCompra = new PedCompra();
            MvPedCompra.Controle = Controle;
            MvPedCompra.IdDocumento = 0;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            Chk_Periodo.Checked = false;
            PopularGrid();            
        }
        private void PopularGrid()
        {
            string Filtro = "";
            Filtro = Filtro + " WHERE T1.Status = 2";
            if (Chk_Periodo.Checked)
                Filtro = Filtro + " and T1.Data >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) and T1.Data <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)";

            if (TxtPesqFornecedor.Text.Trim() != "")
                Filtro = Filtro + " and T2.RazaoSocial like '%" + TxtPesqFornecedor.Text.Trim() + "%'";
            if (TxtPesNumPedido.Text.Trim() != "")
                Filtro = Filtro + " and T1.NumPedido like '%" + TxtPesNumPedido.Text.Trim() + "%'";
            if (TxtPesNumNfe.Text.Trim() != "")
                Filtro = Filtro + " and T1.NumNFe like '%" + TxtPesNumNfe.Text.Trim() + "%'";
            try
            {
                DataSet Tabela = new DataSet();
                Tabela = Controle.ConsultaTabela(string.Format("SELECT T1.Id_Documento,T1.Data,T1.NumPedido,T1.NumNFe,T2.RazaoSocial as Fornecedor,T1.PrevEntrega,T1.DataRecebimento,T1.VlrTotal " +
                                                               " FROM PedCompra T1 LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" + Filtro + " ORDER BY T2.RAZAOSOCIAL,DATA DESC"));
                BindingSource Source = new BindingSource();
                Source.DataSource = Tabela;
                Source.DataMember = Tabela.Tables[0].TableName;
                GridDados.DataSource = Source;
            }
            catch
            {
                MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GridDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GridDados.CurrentRow != null)
                    MvPedCompra.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                else
                    MvPedCompra.IdDocumento = 0;
                Close();
            }
        }
        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }

        private void MudarCampo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void TxtPesNumNfe_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void TxtPesNumNfe_Validated(object sender, EventArgs e)
        {
            if (TxtPesNumNfe.Text.Trim() != "")
                PopularGrid();
        }
    }
}
