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
using System.Data.Sql;
using System.Data.SqlClient;

namespace ERP_SGE
{
    public partial class FrmBuscaFuncionarios : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public Funcionarios CadFunc = new Funcionarios();

        public FrmBuscaFuncionarios()
        {
            InitializeComponent();
        }
        
        private void FrmBuscaFuncionarios_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            CadFunc.Controle = Controle;
            CadFunc.IdFunc = 0;
            LstPesquisa.SelectedIndex = 0;
        }

        private void TxtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TxtPesquisa.Text.Trim() != "")
                {
                    try
                    {
                        DataSet Tabela = new DataSet();
                        string sSQL = "SELECT T1.ID_FUNC,T1.MATRICULA,T1.NOME,T2.DEPARTAMENTO,T3.FILIAL AS LotFilial FROM FUNCIONARIOS T1 " +
                                      " LEFT JOIN Departamentos T2 ON (T2.Id_Departamento=T1.Id_Departamento)" +
                                      " LEFT JOIN Empresa_Filial T3 ON (T3.Id_Filial=T1.Id_FilialTrab) WHERE T1.DTDEMISSAO IS NULL AND T3.ID_FILIAL<>2";

                        if (TxtPesquisa.Text.Trim() != "")
                        {
                            try
                            {
                                if (LstPesquisa.SelectedIndex == 0)
                                    Tabela = Controle.ConsultaTabela(string.Format(sSQL + " AND T1.NOME LIKE '%{0}%' order by T1.NOME", TxtPesquisa.Text.Trim()));
                                else if (LstPesquisa.SelectedIndex == 1)
                                    Tabela = Controle.ConsultaTabela(string.Format(sSQL + " AND T1.MATRICULA LIKE '%{0}%' order by T1.MATRICULA", TxtPesquisa.Text.Trim()));
                                else if (LstPesquisa.SelectedIndex == 2)
                                    Tabela = Controle.ConsultaTabela(string.Format(sSQL + " AND T1.CPF LIKE '%{0}%' order by T1.CPF", TxtPesquisa.Text.Trim()));
                            }
                            catch
                            {
                                MessageBox.Show("Erro ao pesquisar verifique o conteúdo da pesquisa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                            Tabela = Controle.ConsultaTabela(sSQL+" ORDER BY T1.NOME");

                        BindingSource Source = new BindingSource();
                        Source.DataSource = Tabela;
                        Source.DataMember = Tabela.Tables[0].TableName;
                        GridDados.DataSource = Source;
                        int item = Source.Find("Id_Func", CadFunc.IdFunc);
                        Source.Position = item;
                        
                        if (GridDados.CurrentRow != null)
                            GridDados.Focus();                                                   
                        else
                        {
                            MessageBox.Show("Nenhum registro foi localizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TxtPesquisa.Focus();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void GridDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GridDados.CurrentRow != null)
                    CadFunc.LerDados(int.Parse(GridDados.CurrentRow.Cells[0].Value.ToString()));
                else
                    CadFunc.LerDados(0);
                Close();
            }
        }
    }
}
