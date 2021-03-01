using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class FrmAtlzCadPessoa : Form
    {
        public string AtlzEmail = "";
        public int IdPessoa    = 0;
        public string NmPessoa = "";
        public Pessoas CadPessoa;

        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        private int NumTentativas = 1;

        public FrmAtlzCadPessoa()
        {
            InitializeComponent();
        }

        private void FrmAtlzCadPessoa_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            LstAtividade = FrmPrincipal.PopularCombo("SELECT T1.Id_Atividade,SUBSTRING(T2.GRUPO,1,30)+' / '+RTRIM(T1.Atividade) AS ATIVIDADE FROM RamoAtividade T1 LEFT JOIN GRUPOATIVIDADE T2 ON (T2.ID_GRPATIVIDADE=T1.ID_GRPATIVIDADE) ORDER BY T2.GRUPO,T1.Atividade", LstAtividade);

            TxtEmail.Text              = CadPessoa.Email;
            TxtFone.Text               = CadPessoa.Fone;
            TxtFax.Text                = CadPessoa.Fax;
            TxtCelular.Text            = CadPessoa.Celular;
            LstAtividade.SelectedValue = CadPessoa.IdAtividade;
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)        
        {
            if (TxtEmail.Text.Trim() == "")
            {
                if (NumTentativas < 3)
                {
                    MessageBox.Show("Favor Informar o Email do Cliente para atualização do cadastro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    NumTentativas = NumTentativas + 1;
                    return;
                }
                else
                {
                    MessageBox.Show("Cadastro não Atualizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            try
            {
                if (TxtEmail.Text.Trim() != "")
                {
                    CadPessoa.Email       = TxtEmail.Text.Trim();
                    CadPessoa.Fone        = TxtFone.Text;
                    CadPessoa.Fax         = TxtFax.Text;
                    CadPessoa.Celular     = TxtCelular.Text;
                    CadPessoa.IdAtividade = int.Parse(LstAtividade.SelectedValue.ToString());
                    CadPessoa.GravarDados();
                    FrmPrincipal.RegistrarAuditoria(this.Text, IdPessoa, CadPessoa.Cnpj, 2, "Alteração de Cadastros (Vendas)");
                    //Controle.ExecutaSQL("Update Pessoas set Email='" + TxtEmail.Text.Trim() + "' where Id_pessoa=" + IdPessoa.ToString());
                    MessageBox.Show("Cadastro Atualizado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Erro ao Atualizar o Cadastro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
