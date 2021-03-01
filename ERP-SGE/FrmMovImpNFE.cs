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
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace ERP_SGE
{
    public partial class FrmMovImpNFE : Form
    {

        Funcoes Controle = new Funcoes();
        public NotaFiscal CadNota = new NotaFiscal();
        public TelaPrincipal FrmPrincipal;

        public FrmMovImpNFE()
        {
            InitializeComponent();
        }

        private void FrmMovImpNFE_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            CadNota.Controle = Controle;
            LstPesqFilialNT = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstPesqFilialNT, "Todas");
            PopularGrid();
        }

        private void PopularGrid()
        {
            CadNota.LerDados(0);
            string Filtro = " WHERE T1.STATUS=1 ";
            
            if (TxtPesqNumVenda.Text.Trim() != "")
                Filtro = Filtro + " AND T1.ID_VENDA =" + TxtPesqNumVenda.Text.Trim();
            if (TxtPesqNumNota.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMNOTA =" + TxtPesqNumNota.Text.Trim();
            if (TxtPesqNumForm.Text.Trim() != "")
                Filtro = Filtro + " AND T1.NUMFORMULARIO =" + TxtPesqNumForm.Text.Trim();
            if (TxtPesqPessoaNT.Text.Trim() != "")
                Filtro = Filtro + " AND T2.RAZAOSOCIAL Like '%" + TxtPesqPessoaNT.Text.Trim() + "%'";
            if (int.Parse(LstPesqFilialNT.SelectedValue.ToString()) > 0)
                Filtro = Filtro + " AND T1.Id_FILIAL=" + LstPesqFilialNT.SelectedValue.ToString();
            if (Chk_PeriodoNT.Checked)
                Filtro = Filtro + " AND T1.DTEMISSAO >= Convert(DateTime,'" + DtNT1.Value.Date.ToString() + "',103) AND T1.DTEMISSAO <= Convert(DateTime,'" + DtNT2.Value.Date.ToString() + "',103)";

            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.ID_NOTA,T1.DTEMISSAO,T1.NUMNOTA,T1.NUMFORMULARIO,T2.RAZAOSOCIAL AS PESSOA,T1.VLRNOTA,T1.PROTOCOLONFE,T3.FANTASIA AS FILIAL FROM NOTAFISCAL T1" +
                                             "  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)  LEFT JOIN EMPRESA_FILIAL T3 ON (T3.ID_FILIAL=T1.ID_FILIAL) " + Filtro + " ORDER BY T1.NUMNOTA");
            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;
            int item = Source.Find("Id_Nota", CadNota.IdNota);
            Source.Position = item;
        }

        private void GridDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GridDados.CurrentRow != null)
                {
                    if (MessageBox.Show("Confirma a Importação dos Itens dessa Nota Fiscal", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        CadNota.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                        Close();
                    }
                }
            }
        }

        private void BtnPesqNT_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }
    }
}
