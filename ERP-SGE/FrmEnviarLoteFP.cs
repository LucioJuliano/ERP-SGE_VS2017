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
using System.IO;
using System.Collections;

namespace ERP_SGE
{
    public partial class FrmEnviarLoteFP : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        Filiais CadFilial = new Filiais();


        public FrmEnviarLoteFP()
        {
            InitializeComponent();
        }
        private void FrmEnviarLoteFP_Load(object sender, EventArgs e)
        {
            Controle.Conexao   = FrmPrincipal.Conexao;
            CadFilial.Controle = Controle;
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "");
        }

        private void BtnLote_Click(object sender, EventArgs e)
        {
            ArrayList Lote = new ArrayList();

            CadFilial.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));

            if (MessageBox.Show("Confirma o processamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Lote.Add("03300010 2" + CadFilial.Cnpj.ToString() + "0033427800830359857804279 0000130043542 TALIMPO PRODUTOS DE LIMPEZA   BANCO SANTANDER                1230220210820001000000                                                           teste");
                Lote.Add("03300011C3001031 2" + CadFilial.Cnpj.ToString() + "003342780083035985780479 0000130043542 TALIMPO PRODUTOS DE LIMPEZA   " + Controle.Space(" ", 70).Trim() + "00000" + Controle.Space(" ", 35).Trim() + Controle.Space(" ", 20).Trim());
                Lote.Add("033000130001A100000003304653 0000010321818 "+ Controle.Space("LUCIO JULIANO DA SILVIERA",30).Trim()+ Controle.Space("000044022021",20).Trim()+"05022021BRL000000000000000000000000185000"+Controle.Space(" ",20).Trim()+"05022021000000000000000"+Controle.Space(" ",40).Trim()+"00          6          ");
                Lote.Add("033000153000000000185000000000000000000000000000" + Controle.Space(" ", 175).Trim());
                Lote.Add("03300019         0000014" + Controle.Space(" ", 211).Trim());
            }

            // Gravando o arquivo destino                                                
            StreamWriter SaveDestino = new StreamWriter(@"D:\ERP-SGE\FOLHA\Folha022021.txt", true, Encoding.ASCII);
            //StreamWriter SaveDestino = new StreamWriter(ArqDestino.FileName);                               
            for (int I = 0; I <= Lote.Count - 1; I++)
            {
                SaveDestino.WriteLine(Lote[I].ToString());                
            }
            SaveDestino.Close();
        }

        
    }
}
