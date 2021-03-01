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
    public partial class FrmCadAtividades : Form
    {
        Funcoes      Controle  = new Funcoes();
        RamoAtividade CadAtiv  = new RamoAtividade();
        GrupoAtividade GrpAtiv = new GrupoAtividade();

        public TelaPrincipal FrmPrincipal;
        public bool StaFormEdicao = false;

        public FrmCadAtividades()
        {
            InitializeComponent();
        }
        private void FrmCadAtivides_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            CadAtiv.Controle = Controle;
            GrpAtiv.Controle = Controle;
            PopularContas();
        }
        private void PopularContas()
        {
            DataSet TabCta = new DataSet();
            DataSet TabGrupo = new DataSet();
            // Incluindo as Contas de Receitas
            TabGrupo = Controle.ConsultaTabela("SELECT * FROM GRUPOATIVIDADE ORDER BY GRUPO");
            for (int I = 0; I <= TabGrupo.Tables[0].Rows.Count - 1; I++)
            {
                LstContas.Nodes[0].Nodes.Add(TabGrupo.Tables[0].Rows[I]["GRUPO"].ToString().Trim(), TabGrupo.Tables[0].Rows[I]["GRUPO"].ToString().Trim());
                LstContas.Nodes[0].Nodes[I].Tag = "G" + string.Format("{0:D3}", int.Parse(TabGrupo.Tables[0].Rows[I]["ID_GRPATIVIDADE"].ToString()));
                LstContas.Nodes[0].Nodes[I].ImageIndex = 3;
                LstContas.Nodes[0].Nodes[I].NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                TabCta = Controle.ConsultaTabela("SELECT * FROM RAMOATIVIDADE WHERE ID_GRPATIVIDADE=" + TabGrupo.Tables[0].Rows[I]["ID_GRPATIVIDADE"].ToString() + " ORDER BY ATIVIDADE");
                for (int C = 0; C <= TabCta.Tables[0].Rows.Count - 1; C++)
                {
                    LstContas.Nodes[0].Nodes[I].Nodes.Add(TabCta.Tables[0].Rows[C]["ATIVIDADE"].ToString().Trim(), TabCta.Tables[0].Rows[C]["ATIVIDADE"].ToString().Trim());
                    LstContas.Nodes[0].Nodes[I].Nodes[C].Tag = "C" + string.Format("{0:D3}", int.Parse(TabCta.Tables[0].Rows[C]["ID_ATIVIDADE"].ToString()));
                    LstContas.Nodes[0].Nodes[I].Nodes[C].ImageIndex = 1;
                }
            }
            LstContas.ExpandAll();
        }
        private void BtnInc_Click(object sender, EventArgs e)
        {
            if (LstContas.SelectedNode == null)
            {
                MessageBox.Show("Favor Selecionar uma Atividade", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((string)LstContas.SelectedNode.Tag == "R")
            {
                GrpAtiv.LerDados(0);
                GrpAtiv.Grupo = "Novo Grupo";                
                GrpAtiv.GravarDados();
                LstContas.SelectedNode.Nodes.Add("Novo Grupo");
                LstContas.SelectedNode.Expand();
                LstContas.SelectedNode.LastNode.Tag = "G" + string.Format("{0:D3}", GrpAtiv.IdGrpAtividade);
                LstContas.SelectedNode.LastNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                LstContas.SelectedNode.LastNode.BeginEdit();
            }
            else if (LstContas.SelectedNode.Tag.ToString().Substring(0, 1) == "G")
            {
                GrpAtiv.LerDados(int.Parse(LstContas.SelectedNode.Tag.ToString().Substring(1, 3)));
                CadAtiv.LerDados(0);
                CadAtiv.IdGrpAtividade = GrpAtiv.IdGrpAtividade;
                CadAtiv.Atividade = "Nova Atividade";
                CadAtiv.GravarDados();
                //
                LstContas.SelectedNode.Nodes.Add("Nova Conta");
                LstContas.SelectedNode.Expand();
                LstContas.SelectedNode.LastNode.Tag = "C" + string.Format("{0:D3}", CadAtiv.IdAtividade);
                LstContas.SelectedNode.LastNode.BeginEdit();
            }
        }
        private void LstContas_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Node.Tag.ToString().Substring(0, 1) == "G")
                {
                    GrpAtiv.LerDados(int.Parse(e.Node.Tag.ToString().Substring(1, 3)));
                    GrpAtiv.Grupo = e.Label;
                    GrpAtiv.GravarDados();
                }
                if (e.Node.Tag.ToString().Substring(0, 1) == "C")
                {
                    CadAtiv.LerDados(int.Parse(e.Node.Tag.ToString().Substring(1, 3)));
                    CadAtiv.Atividade = e.Label;
                    CadAtiv.GravarDados();
                }
            }
        }
        private void BtnExc_Click(object sender, EventArgs e)
        {
            if (LstContas.SelectedNode == null)
            {
                MessageBox.Show("Favor Selecionar uma Atividade", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((string)LstContas.SelectedNode.Tag == "R")
            {
                MessageBox.Show("Grupo principal não pode ser excluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LstContas.SelectedNode.Tag.ToString().Substring(0, 1) == "G")
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM RamoAtividade WHERE Id_GrpAtividade=" + LstContas.SelectedNode.Tag.ToString().Substring(1, 3));
                if (Tabela.HasRows)
                {
                    MessageBox.Show("Existem Atividade Cadastrada nesse grupo, favor excluir primeiro a Atividade", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Confirma a Exclusão do Grupo", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GrpAtiv.LerDados(int.Parse(LstContas.SelectedNode.Tag.ToString().Substring(1, 3)));
                    GrpAtiv.Excluir();
                    LstContas.SelectedNode.Remove();
                }
            }
            if (LstContas.SelectedNode.Tag.ToString().Substring(0, 1) == "C")
            {
                
                if (MessageBox.Show("Confirma a Exclusão da Atividade", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CadAtiv.LerDados(int.Parse(LstContas.SelectedNode.Tag.ToString().Substring(1, 3)));
                    CadAtiv.Excluir();
                    LstContas.SelectedNode.Remove();
                }
            }

        }
    }
}
