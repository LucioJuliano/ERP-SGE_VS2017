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
    public partial class FrmDIEF : Form
    {
        Funcoes Controle = new Funcoes();        
        public TelaPrincipal FrmPrincipal;
        
        public FrmDIEF()
        {
            InitializeComponent();
        }
        private void FrmDIEF_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,60) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "Todas");                        
            TxtAno.Value = DateTime.Now.Year;
            LstMes.SelectedIndex = DateTime.Now.Month - 1;
            LstFilial.SelectedValue = FrmPrincipal.LstFilial.SelectedValue;

        }
        private void BtnAbrirOrigem_Click(object sender, EventArgs e)
        {
            ArqOrigem.ShowDialog();
            TxtArqOrigem.Text = ArqOrigem.FileName;
        }
        private void BtnSalvarDestino_Click(object sender, EventArgs e)
        {            
            ArqDestino.ShowDialog();
            TxtArqDestino.Text = ArqDestino.FileName;
        }
        private void BtnProcessar_Click(object sender, EventArgs e)
        {
            if (TxtArqOrigem.Text.Trim() == "")
            {
                MessageBox.Show("Favor Informar o Arquivo de Origem", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (TxtArqDestino.Text.Trim() == "")
            {
                MessageBox.Show("Favor Informar o Arquivo Destino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Confirma o Processamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BtnProcessar.Enabled = false;

                // Abrindo o Arquivo Origem
                StreamReader O_Txt = new StreamReader(ArqOrigem.FileName);                
                ArrayList ListaTxtDestino = new ArrayList();
                string[] Linhas = O_Txt.ReadToEnd().Split(char.Parse("\n"));
                ArrayList ListaTxtOritem = new ArrayList(Linhas);
                //ListaTxtOritem.Add(Linhas);
                BarProc.Maximum = ListaTxtOritem.Count;
                string NumForm = "";
                ArrayList LstItem = new ArrayList();
                for (int I = 0; I <= ListaTxtOritem.Count - 1; I++)
                {
                    if (ListaTxtOritem[I].ToString().Trim() != "")
                    {
                        ListaTxtDestino.Add(ListaTxtOritem[I].ToString());
                        if (ListaTxtOritem[I].ToString().Substring(0, 3) == "CTD")
                        {
                            ArrayList LstPrd = new ArrayList(ListaProdutos());
                            for (int Prd = 0; Prd <= LstPrd.Count - 1; Prd++)
                                ListaTxtDestino.Add(LstPrd[Prd].ToString());
                        }

                        if (NumForm != "")
                        {
                            for (int Item = 0; Item <= LstItem.Count - 1; Item++)
                                ListaTxtDestino.Add(LstItem[Item].ToString());
                            NumForm = "";
                            LstItem = null;
                        }

                        if (ListaTxtOritem[I].ToString().Substring(0, 3) == "DOC")
                        {
                            if (ListaTxtOritem[I].ToString().Substring(4, 1) == "2")
                                NumForm = ListaTxtOritem[I].ToString().Substring(21, 6);
                            else
                                NumForm = ListaTxtOritem[I].ToString().Substring(82, 10);                            
                            LstItem = new ArrayList(ListaItens(ListaTxtOritem[I].ToString().Substring(4, 1), NumForm));                            
                        }
                    }
                    BarProc.Value = BarProc.Value + 1;
                }
                StreamWriter ArqDief = new StreamWriter(ArqDestino.FileName,true,Encoding.ASCII);                
                for (int I = 0; I <= ListaTxtDestino.Count - 1; I++)
                    ArqDief.WriteLine(ListaTxtDestino[I].ToString());
                ArqDief.Close();
                MessageBox.Show("Processamento Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnProcessar.Enabled = true;
            }
        }
        private ArrayList ListaProdutos()
        {
            string sSQL = "SELECT DISTINCT T2.REFERENCIA,T2.DESCRICAO FROM NOTAFISCALITENS T1" +
                        "   LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                        "   LEFT JOIN NOTAFISCAL T3 ON (T3.ID_NOTA=T1.ID_NOTA)" +
                        " WHERE MONTH(T3.DTEMISSAO)=" + (LstMes.SelectedIndex + 1).ToString() +
                        "   AND YEAR(T3.DTEMISSAO)=" + TxtAno.Value.ToString() +
                        "   AND T3.ID_FILIAL=" + LstFilial.SelectedValue.ToString()+
                        " UNION " +
                        "SELECT DISTINCT T2.REFERENCIA,T2.DESCRICAO FROM MVESTOQUEITENS T1" +
                        "   LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                        "   LEFT JOIN MVESTOQUE T3 ON (T3.ID_MOV=T1.ID_MOV AND T3.TPMOV='ENTNF')" +
                        " WHERE MONTH(T3.DTENTSAI)=" + (LstMes.SelectedIndex + 1).ToString() +
                        "   AND YEAR(T3.DTENTSAI)=" + TxtAno.Value.ToString() +
                        "   AND T3.ID_FILIALORIGDEST=" + LstFilial.SelectedValue.ToString() +
                        " ORDER BY REFERENCIA";

            SqlDataReader ConsProdutos = Controle.ConsultaSQL(sSQL);
            ArrayList ListaPrd = new ArrayList();
            if (ConsProdutos.HasRows)
            {                
                while (ConsProdutos.Read())
                    ListaPrd.Add("PRD" + Controle.Space(ConsProdutos["REFERENCIA"].ToString().Trim(), 30) + Controle.Space(ConsProdutos["DESCRICAO"].ToString().Trim(), 60) + "UN1"+"                    ");                
            }
            return ListaPrd;
        }
        private ArrayList ListaItens(string TpMv, string Formulario)
        {
            string sSQL = "";
            if (TpMv == "3")
            {
                sSQL = "SELECT T1.*,T2.REFERENCIA FROM NOTAFISCALITENS T1" +
                       "  LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                       "  LEFT JOIN NOTAFISCAL T3 ON (T3.ID_NOTA=T1.ID_NOTA)" +
                       " WHERE T3.NUMFORMULARIO='" + Formulario + "'";
            }
            else
            {
                sSQL = "SELECT T1.QTDE,T1.VLRUNITARIO,T1.VLRICMS,T1.P_ICMS AS PICMS,T1.VLRTOTAL,T1.PERCRED,T2.REFERENCIA FROM MVESTOQUEITENS T1" +
                       "  LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                       "  LEFT JOIN MVESTOQUE T3 ON (T3.ID_MOV=T1.ID_MOV)" +
                       " WHERE T3.TPMOV='ENTNF' AND T3.NUMFORMULARIO='" + Formulario + "' AND MONTH(T3.DTENTSAI)=" + (LstMes.SelectedIndex + 1).ToString() +
                       " AND YEAR(T3.DTENTSAI)=" + TxtAno.Value.ToString();
            }

            SqlDataReader ConsItens = Controle.ConsultaSQL(sSQL);
            ArrayList ListaItens = new ArrayList();
            if (ConsItens.HasRows)
            {
                int Seq=1;
                decimal BIcms = 0;
                decimal VUnit = 0;
                decimal Qtde = 0;
                decimal VIcms=0;


                while (ConsItens.Read())
                {
                    Qtde = Math.Truncate(decimal.Parse(ConsItens["QTDE"].ToString()) * 100000000);
                    VUnit = Math.Truncate(decimal.Parse(ConsItens["VLRUNITARIO"].ToString()) * 1000000);
                    VIcms=Math.Truncate(decimal.Parse(ConsItens["VlrIcms"].ToString()) * 100);
                    BIcms = 0;
                    string Compl = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                    if (decimal.Parse(ConsItens["PIcms"].ToString()) > 0 && decimal.Parse(ConsItens["VlrTotal"].ToString()) > 0)
                    {
                        if (decimal.Parse(ConsItens["PIcms"].ToString()) > 0)
                            BIcms = (decimal.Parse(ConsItens["VlrTotal"].ToString()) - (decimal.Parse(ConsItens["VlrTotal"].ToString()) * decimal.Parse(ConsItens["PercRed"].ToString()) / 100));
                        else
                            BIcms = decimal.Parse(ConsItens["VlrTotal"].ToString());
                        BIcms = Math.Truncate(BIcms * 100);
                    }
                    if (TpMv == "3")
                        ListaItens.Add("ITE" + string.Format("{0:D4}", Seq) + Controle.Space(ConsItens["REFERENCIA"].ToString().Trim(), 30) + string.Format("{0:D17}", Int64.Parse(Qtde.ToString())) + string.Format("{0:D15}", Int64.Parse(VUnit.ToString())) + "000"
                        + string.Format("{0:D13}", Int64.Parse(BIcms.ToString())) + string.Format("{0:D13}", Int64.Parse(VIcms.ToString())) + Compl);
                    else
                        ListaItens.Add("ITE" + string.Format("{0:D4}", Seq) + Controle.Space(ConsItens["REFERENCIA"].ToString().Trim(), 30) + string.Format("{0:D17}", Int64.Parse(Qtde.ToString())) + string.Format("{0:D15}", Int64.Parse(VUnit.ToString())) + "   "
                        + string.Format("{0:D13}", Int64.Parse(BIcms.ToString())) + string.Format("{0:D13}", Int64.Parse(VIcms.ToString())) + Compl);
                    Seq = Seq + 1;
                }
                    
            }
            return ListaItens;
        }
    }
}
