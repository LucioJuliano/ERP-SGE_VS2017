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
using CDSSoftware;
using System.Collections;


namespace ERP_SGE
{
    public partial class FrmAvisoAgenda : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public int IdVendedor = 0;

        public FrmAvisoAgenda()
        {
            InitializeComponent();
        }       

        private void FrmAvisoAgenda_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            DataSet Tabela = new DataSet();
            Tabela = Controle.ConsultaTabela("SELECT T1.DtVisita,CONVERT(CHAR,T1.OBJETIVO) AS OBJETIVO,T1.Cliente,T3.Usuario FROM AgendaVisita T1" +
                                             " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.Id_Pessoa) " +
                                             " LEFT JOIN USUARIOS T3 ON (T3.ID_USUARIO=T1.Id_UsuLanc) WHERE T1.STATUS=1 AND T1.Id_VendVisita=" + IdVendedor.ToString());

            BindingSource Source = new BindingSource();
            Source.DataSource = Tabela;
            Source.DataMember = Tabela.Tables[0].TableName;
            GridDados.DataSource = Source;

            if (Tabela.Tables[0].Rows.Count == 0)
                Close();
        }
    }
}
