using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controles;
using System.Data.SqlClient;
using System.Data.Sql;
using Controle_Dados;


namespace ERP_SGE
{
    public partial class FrmCadCtaCusto : Form
    {
        Funcoes Controle     = new Funcoes();
        CentroCusto CtaCusto = new CentroCusto();
        GrupoCusto  GrpCusto = new GrupoCusto();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadCtaCusto()
        {
            InitializeComponent();
        }
        private void FrmCadCtaCusto_Load(object sender, EventArgs e)
        {
            Controle.Conexao  = FrmPrincipal.Conexao;
            CtaCusto.Controle = Controle;
            GrpCusto.Controle = Controle;
            PopularContas();
        }
        private void PopularContas()
        {
            DataSet TabCta = new DataSet();
            DataSet TabGrupo = new DataSet();
            // Incluindo as Contas de Receitas
            TabGrupo = Controle.ConsultaTabela("SELECT * FROM GRUPOCCUSTO WHERE TIPO=0 ORDER BY GRUPO");
            for (int I = 0; I <= TabGrupo.Tables[0].Rows.Count - 1; I++)
            {                
                LstContas.Nodes[0].Nodes.Add(TabGrupo.Tables[0].Rows[I]["GRUPO"].ToString().Trim(), TabGrupo.Tables[0].Rows[I]["GRUPO"].ToString().Trim());
                LstContas.Nodes[0].Nodes[I].Tag = "G" + string.Format("{0:D3}", int.Parse(TabGrupo.Tables[0].Rows[I]["ID_GRPCUSTO"].ToString()));
                LstContas.Nodes[0].Nodes[I].ImageIndex = 3;
                LstContas.Nodes[0].Nodes[I].NodeFont = new Font("Microsoft Sans Serif",9,FontStyle.Bold);
                
                TabCta = Controle.ConsultaTabela("SELECT * FROM CENTROCUSTO WHERE ID_GRPCUSTO=" + TabGrupo.Tables[0].Rows[I]["ID_GRPCUSTO"].ToString() + " ORDER BY CUSTO");
                for (int C = 0; C <= TabCta.Tables[0].Rows.Count - 1; C++)
                {
                    LstContas.Nodes[0].Nodes[I].Nodes.Add(TabCta.Tables[0].Rows[C]["CUSTO"].ToString().Trim(), TabCta.Tables[0].Rows[C]["CUSTO"].ToString().Trim());
                    LstContas.Nodes[0].Nodes[I].Nodes[C].Tag = "C" + string.Format("{0:D3}", int.Parse(TabCta.Tables[0].Rows[C]["ID_CUSTO"].ToString()));
                    LstContas.Nodes[0].Nodes[I].Nodes[C].ImageIndex = 1;
                }
            }
            // Incluindo as Contas de Despesas
            TabGrupo = Controle.ConsultaTabela("SELECT * FROM GRUPOCCUSTO WHERE TIPO=1 ORDER BY ID_GRPCUSTO");
            for (int I = 0; I <= TabGrupo.Tables[0].Rows.Count - 1; I++)
            {

                LstContas.Nodes[1].Nodes.Add(TabGrupo.Tables[0].Rows[I]["GRUPO"].ToString().Trim(), TabGrupo.Tables[0].Rows[I]["GRUPO"].ToString().Trim());
                LstContas.Nodes[1].Nodes[I].Tag = "G" + string.Format("{0:D3}", int.Parse(TabGrupo.Tables[0].Rows[I]["ID_GRPCUSTO"].ToString()));
                LstContas.Nodes[1].Nodes[I].ImageIndex = 4;
                LstContas.Nodes[1].Nodes[I].NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);                

                TabCta = Controle.ConsultaTabela("SELECT * FROM CENTROCUSTO WHERE ID_GRPCUSTO=" + TabGrupo.Tables[0].Rows[I]["ID_GRPCUSTO"].ToString() + " ORDER BY ID_CUSTO");
                for (int C = 0; C <= TabCta.Tables[0].Rows.Count - 1; C++)
                {
                    LstContas.Nodes[1].Nodes[I].Nodes.Add(TabCta.Tables[0].Rows[C]["CUSTO"].ToString().Trim(), TabCta.Tables[0].Rows[C]["CUSTO"].ToString().Trim());
                    LstContas.Nodes[1].Nodes[I].Nodes[C].Tag = "C" + string.Format("{0:D3}", int.Parse(TabCta.Tables[0].Rows[C]["ID_CUSTO"].ToString()));
                    LstContas.Nodes[1].Nodes[I].Nodes[C].ImageIndex = 2;
                }
            }
            LstContas.ExpandAll();
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (LstContas.SelectedNode == null)
            {
                MessageBox.Show("Favor Selecionar uma Conta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((string)LstContas.SelectedNode.Tag == "R" || (string)LstContas.SelectedNode.Tag == "D")
            {
                GrpCusto.LerDados(0);
                GrpCusto.Grupo = "Novo Grupo";
                if ((string)LstContas.SelectedNode.Tag == "R") GrpCusto.Tipo = 0; else GrpCusto.Tipo = 1;
                GrpCusto.GravarDados();
                LstContas.SelectedNode.Nodes.Add("Novo Grupo");
                LstContas.SelectedNode.Expand();
                LstContas.SelectedNode.LastNode.Tag = "G" + string.Format("{0:D3}", GrpCusto.IdGrpCusto);
                LstContas.SelectedNode.LastNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                LstContas.SelectedNode.LastNode.BeginEdit();
            }
            else if (LstContas.SelectedNode.Tag.ToString().Substring(0,1) == "G")
            {
                GrpCusto.LerDados(int.Parse(LstContas.SelectedNode.Tag.ToString().Substring(1, 3)));
                CtaCusto.LerDados(0);
                CtaCusto.IdGrpCusto = GrpCusto.IdGrpCusto;
                CtaCusto.Custo = "Nova Conta";
                CtaCusto.GravarDados();
                //
                LstContas.SelectedNode.Nodes.Add("Nova Conta");
                LstContas.SelectedNode.Expand();
                LstContas.SelectedNode.LastNode.Tag = "C" + string.Format("{0:D3}", CtaCusto.IdCusto);
                LstContas.SelectedNode.LastNode.BeginEdit();
            }
        }        
        private void LstContas_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Node.Tag.ToString().Substring(0, 1) == "G")
                {
                    GrpCusto.LerDados(int.Parse(e.Node.Tag.ToString().Substring(1, 3)));
                    GrpCusto.Grupo = e.Label;
                    GrpCusto.GravarDados();
                }
                if (e.Node.Tag.ToString().Substring(0, 1) == "C")
                {
                    CtaCusto.LerDados(int.Parse(e.Node.Tag.ToString().Substring(1, 3)));
                    CtaCusto.Custo = e.Label;
                    CtaCusto.GravarDados();
                }
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {            
            if (LstContas.SelectedNode == null)
            {
                MessageBox.Show("Favor Selecionar uma Conta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((string)LstContas.SelectedNode.Tag == "R" || (string)LstContas.SelectedNode.Tag == "D")
            {
                MessageBox.Show("Grupo principal não pode ser excluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //int.Parse(LstContas.SelectedNode.Tag.ToString().Substring(1, 3))
            if (LstContas.SelectedNode.Tag.ToString().Substring(0, 1) == "G")
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CentroCusto WHERE Id_GrpCusto=" + LstContas.SelectedNode.Tag.ToString().Substring(1, 3));
                if (Tabela.HasRows)
                {
                    MessageBox.Show("Existem contas nesse grupo, favor excluir primeiro a conta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Confirma a Exclusão do Grupo", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GrpCusto.LerDados(int.Parse(LstContas.SelectedNode.Tag.ToString().Substring(1, 3)));
                    GrpCusto.Excluir();
                    LstContas.SelectedNode.Remove();
                }
            }
            if (LstContas.SelectedNode.Tag.ToString().Substring(0, 1) == "C")
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM LancFinanceiro WHERE Id_Custo=" + LstContas.SelectedNode.Tag.ToString().ToString().Substring(1, 3));
                if (Tabela.HasRows)
                {
                    MessageBox.Show("Existem lançamentos no financeiro nesta contas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Confirma a Exclusão da Conta", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CtaCusto.LerDados(int.Parse(LstContas.SelectedNode.Tag.ToString().Substring(1, 3)));
                    CtaCusto.Excluir();
                    LstContas.SelectedNode.Remove();
                }
            }
            
        }        
    }
}
