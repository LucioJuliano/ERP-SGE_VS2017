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
    public partial class FrmImpMvEstoque : Form
    {
        Funcoes Controle       = new Funcoes();
        MvEstoque MovEstoque   = new MvEstoque();
        public int IdMov = 0;
        public TelaPrincipal FrmPrincipal;
        public FrmImpMvEstoque()
        {
            InitializeComponent();
        }

        private void PopularGrid()
        {
            string Filtro = " WHERE T1.Status = 1 and T1.TPMOV='S_TRA' and YEAR(T1.DtEntSai) >= 2020 ";

            if (TxtPesqNumDoc.Text.Trim() != "")
                Filtro = Filtro + " and T1.NumDocumento like '%" + TxtPesqNumDoc.Text.Trim() + "%'";
                
            if (TxtPesqMov.Text.Trim() != "")
                Filtro = Filtro + " and T1.Id_Mov=" + TxtPesqMov.Text.Trim();
            try
            {
                DataSet Tabela = new DataSet();
                Tabela = Controle.ConsultaTabela("SELECT T1.Id_Mov,T3.RAZAOSOCIAL AS PESSOA,T5.FANTASIA AS NMFILIAL,T1.DTENTSAI,T1.NUMDOCUMENTO,T1.VLRTOTAL FROM MVESTOQUE T1 " +
                                                 " LEFT JOIN PESSOAS  T3 ON (T3.ID_PESSOA=T1.ID_PESSOA)" +
                                                 " LEFT JOIN EMPRESA_FILIAL T5 ON (T5.Id_Filial=T1.Id_FilialOrigDest) " + Filtro + " ORDER BY T1.DATA,T1.ID_MOV DESC");
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

        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }

        private void BtnGerarNF_Click(object sender, EventArgs e)
        {
            if (GridDados.CurrentRow != null)
            {
                if (MessageBox.Show("Confirma a importação dos Itens ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    IdMov = int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString());
                    Close();
                }
            }


        }

        private void FrmImpMvEstoque_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            MovEstoque.Controle = Controle;

        }
    }
}
