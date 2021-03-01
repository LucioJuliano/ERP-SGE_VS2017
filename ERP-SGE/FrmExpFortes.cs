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
    public partial class FrmExpFortes : Form
    {
        Funcoes Controle = new Funcoes();
        public TelaPrincipal FrmPrincipal;
        public FrmExpFortes()
        {
            InitializeComponent();
        }
        private void FrmExpFortes_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,60) as Filial FROM Empresa_FIlial ORDER BY FANTASIA", LstFilial, "Todas");
            LstFilial.SelectedValue = FrmPrincipal.LstFilial.SelectedValue;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            Ck_Invetario.Checked = false;
        }
        private void BtnSalvarDestino_Click(object sender, EventArgs e)
        {
            ArqDestino.ShowDialog();
            TxtArqDestino.Text = ArqDestino.FileName;
        }
        private void BtnProcessar_Click(object sender, EventArgs e)
        {
            if (TxtArqDestino.Text.Trim() == "")
            {
                MessageBox.Show("Favor Informar o Arquivo Destino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Confirma o Processamento ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BtnProcessar.Enabled = false;
                ArrayList ArqTemp = new ArrayList();
                int Linhas = 2;

                //Cabeçalho
                ArqTemp.Add("CAB|100|ERP-SGE|" + Controle.FormatarData(DateTime.Now) + "|" + Controle.Space(TxtEmpresaAC.Text.Trim(), 62).Trim() + "|" + Controle.FormatarData(Dt1.Value) + "|" + Controle.FormatarData(Dt2.Value) + "|ERP-SGE " + Dt1.Value.ToShortDateString() + " a " + Dt2.Value.ToShortDateString()+"|N");

                if (Ck_NF_Entrada.Checked || Ck_NF_Saida.Checked)
                {
                    //Participante        
                    ArrayList LstParticipantes = new ArrayList(Participante());
                    BarProc.Maximum = LstParticipantes.Count;
                    BarProc.Value = 0;
                    for (int Prd = 0; Prd <= LstParticipantes.Count - 1; Prd++)
                    {
                        ArqTemp.Add(LstParticipantes[Prd].ToString());
                        BarProc.Value = BarProc.Value + 1;
                        Linhas = Linhas + 1;
                    }
                }

                //Lista dos Grupos
                ArrayList LstGrp = new ArrayList(ListaGrupo());
                BarProc.Maximum = LstGrp.Count;
                BarProc.Value = 0;
                for (int Grp = 0; Grp <= LstGrp.Count - 1; Grp++)
                {
                    ArqTemp.Add(LstGrp[Grp].ToString());
                    BarProc.Value = BarProc.Value + 1;
                    Linhas = Linhas + 1;
                }

                //Lista das Unidades de Media
                ArrayList LstUnd = new ArrayList(ListaUnidades());
                BarProc.Maximum = LstUnd.Count;
                BarProc.Value = 0;
                for (int Und = 0; Und <= LstUnd.Count - 1; Und++)
                {
                    ArqTemp.Add(LstUnd[Und].ToString());
                    BarProc.Value = BarProc.Value + 1;
                    Linhas = Linhas + 1;
                }

                //Lista de Produtos
                ArrayList LstPrd = new ArrayList(ListaProdutos());
                BarProc.Maximum = LstPrd.Count;
                BarProc.Value = 0;
                for (int Prd = 0; Prd <= LstPrd.Count - 1; Prd++)
                {
                    ArqTemp.Add(LstPrd[Prd].ToString());
                    BarProc.Value = BarProc.Value + 1;
                    Linhas = Linhas + 1;
                }

                //Notas de Entrada
                if (Ck_NF_Entrada.Checked)
                {
                    ArrayList LstNota = new ArrayList(NotaEntrada());
                    BarProc.Maximum = LstNota.Count;
                    BarProc.Value = 0;
                    for (int Item = 0; Item <= LstNota.Count - 1; Item++)
                    {
                        ArqTemp.Add(LstNota[Item].ToString());
                        BarProc.Value = BarProc.Value + 1;
                        Linhas = Linhas + 1;
                    }
                }

                //Notas de Saidas
                if (Ck_NF_Saida.Checked)
                {
                    ArrayList LstNotaSaida = new ArrayList(NotaSaida());
                    BarProc.Maximum = LstNotaSaida.Count;
                    BarProc.Value = 0;
                    for (int Item = 0; Item <= LstNotaSaida.Count - 1; Item++)
                    {
                        ArqTemp.Add(LstNotaSaida[Item].ToString());
                        BarProc.Value = BarProc.Value + 1;
                        Linhas = Linhas + 1;
                    }
                }
                //Cupom Fiscal
                if (Ck_CFiscal.Checked)
                {
                    ArrayList LstCF = new ArrayList(CupomFiscalSaida());
                    BarProc.Maximum = LstCF.Count;
                    BarProc.Value = 0;
                    for (int Item = 0; Item <= LstCF.Count - 1; Item++)
                    {
                        ArqTemp.Add(LstCF[Item].ToString());
                        BarProc.Value = BarProc.Value + 1;
                        Linhas = Linhas + 1;
                    }
                }
                //Inventario
                if (Ck_Invetario.Checked)
                {
                    ArrayList LstInventario = new ArrayList(ListaInventario());
                    BarProc.Maximum = LstInventario.Count;
                    BarProc.Value = 0;
                    for (int Item = 0; Item <= LstInventario.Count - 1; Item++)
                    {
                        ArqTemp.Add(LstInventario[Item].ToString());
                        BarProc.Value = BarProc.Value + 1;
                        Linhas = Linhas + 1;
                    }
                }
                //Fim do arquivo
                ArqTemp.Add("TRA|" + Linhas.ToString());

                // Gravando o arquivo destino                                                
                StreamWriter SaveDestino = new StreamWriter(ArqDestino.FileName, true, Encoding.ASCII);
                //StreamWriter SaveDestino = new StreamWriter(ArqDestino.FileName);                               
                BarProc.Maximum = ArqTemp.Count;
                BarProc.Value = 0;
                for (int I = 0; I <= ArqTemp.Count - 1; I++)
                {
                    SaveDestino.WriteLine(ArqTemp[I].ToString());
                    BarProc.Value = BarProc.Value + 1;
                    Linhas = Linhas + 1;
                }
                SaveDestino.Close();
                MessageBox.Show("Processamento Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnProcessar.Enabled = true;
            }
        }
        private ArrayList Participante()
        {
            string sSQL = "SELECT T2.TIPO,T2.ID_PESSOA,T2.RAZAOSOCIAL,T2.ID_UF,T2.CNPJ,CASE T2.INSC_UF WHEN 'ISENTO' THEN '' ELSE T2.INSC_UF END AS INSC_UF,T3.SIGLA,T3.CODIBGE,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO,T2.BAIRRO,T2.CEP,T2.FONE,CASE T2.INSC_UF WHEN 'ISENTO' THEN '2' WHEN '' THEN '2' ELSE '1' END AS ISENTO_ICMS FROM NOTAFISCAL T1 " +
                        "  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) " +
                        "  LEFT JOIN ESTADOS T3 ON (T3.ID_UF=T2.ID_UF)" +
                        " WHERE T1.DTEMISSAO >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                        "   AND T1.DTEMISSAO <=Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)" +
                        "   AND T1.ID_FILIAL=" + LstFilial.SelectedValue.ToString() +
                        "   AND T2.ID_PESSOA IS NOT NULL" +
                        " UNION " +
                        " SELECT T2.TIPO,T2.ID_PESSOA,T2.RAZAOSOCIAL,T2.ID_UF,T2.CNPJ,CASE T2.INSC_UF WHEN 'ISENTO' THEN '' ELSE T2.INSC_UF END AS INSC_UF,T3.SIGLA,T3.CODIBGE,T2.ENDERECO,T2.NUMERO,T2.COMPLEMENTO,T2.BAIRRO,T2.CEP,T2.FONE,CASE T2.INSC_UF WHEN 'ISENTO' THEN '2' WHEN '' THEN '2' ELSE '1' END AS ISENTO_ICMS FROM MVESTOQUE T1" +
                        "  LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA)" +
                        "  LEFT JOIN ESTADOS T3 ON (T3.ID_UF=T2.ID_UF)" +
                        " WHERE T1.DTENTSAI >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                        "   AND T1.DTENTSAI <=Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)" +
                        "   AND T1.TPMOV='ENTNF'" +
                        "   AND T1.STATUS=1" +                        
                        "   AND T1.ID_FILIALORIGDEST=" + LstFilial.SelectedValue.ToString() +
                        "   AND T2.ID_PESSOA IS NOT NULL";
            
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            ArrayList Lista = new ArrayList();
            string Numero = "";
            if (Tab.HasRows)
            {
                try
                {
                    string InsUF = "";
                    string TipoContr = "3";
                    while (Tab.Read())
                    {
                        InsUF = "";
                        if (int.Parse(Tab["Tipo"].ToString()) == 0)
                        {
                            InsUF = Tab["INSC_UF"].ToString().Replace("-", "").Replace(".", "").Trim();
                            TipoContr = Tab["ISENTO_ICMS"].ToString();
                        }
                                                

                        Numero = Tab["NUMERO"].ToString().Trim().Replace("A", "").Replace("B", "").Replace("K", "").Replace("M", "").Replace("S", "").Replace("N", "").Replace("/", "").Replace("-", "");
                        Lista.Add("PAR|" + string.Format("{0:D8}", int.Parse(Tab["ID_PESSOA"].ToString())) + "|" + Controle.Space(Tab["RAZAOSOCIAL"].ToString(), 60).Trim() + "|" + Tab["SIGLA"].ToString() + "|" + Tab["CNPJ"].ToString().Trim() + "|"+"" +
                                  "||S|S|N|N|N|S|N|N|35|" + Controle.Space(Tab["ENDERECO"].ToString(), 50).Trim() + "|" + Numero + "|" + Controle.Space(Tab["COMPLEMENTO"].ToString(), 20).Trim() + "|01|" + Controle.Space(Tab["BAIRRO"].ToString(), 50).Trim() + "|" + Tab["CEP"].ToString().Replace("_Objetivo", "0") + "|" + Tab["CODIBGE"].ToString().Substring(2, 5) + "||||N|||1058|N|" + TipoContr +"||N|N|");
                    }
                }
                catch
                {
                    MessageBox.Show("Verificar a Ficha da Pessoa " + Tab["ID_PESSOA"].ToString() + "  " + Tab["RAZAOSOCIAL"].ToString());
                }
            }
            return Lista;
        }
        private ArrayList NotaEntrada()
        {
            string sSQL = "SELECT T1.*,T2.CFOP,T4.SIGLA FROM MVESTOQUE T1" +
                          " LEFT JOIN CFOP T2 ON (T2.ID_CFOP=T1.ID_CFOP)" +
                          " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T1.ID_PESSOA)" +
                          " LEFT JOIN ESTADOS T4 ON (T4.ID_UF=T3.ID_UF)" +
                          " WHERE T1.DTENTSAI >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                          "   AND T1.DTENTSAI <=Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)" +
                          "   AND T1.TPMOV='ENTNF'" +
                          "   AND T1.STATUS=1" +                
                          "   AND T1.ID_FILIALORIGDEST=" + LstFilial.SelectedValue.ToString();
            
            SqlDataReader TabItens;
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            ArrayList Lista = new ArrayList();

            if (Tab.HasRows)
            {
                //try
                {
                    string Pgto = "";
                    string Frete = "N";
                    decimal TDesconto = 0;
                    decimal PDescItem = 0;
                    decimal B_IPI = 0;
                    decimal TB_IPI = 0;
                    while (Tab.Read())
                    {
                        TabItens = Controle.ConsultaSQL("SELECT T1.*,T2.UNIDADE,T2.CREDITOIPI FROM MVESTOQUEITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_MOV=" + Tab["ID_MOV"].ToString());
                        TDesconto = 0;
                        PDescItem = 0;
                        if (TabItens.HasRows)
                        {
                            while (TabItens.Read())
                            {
                                // if (decimal.Parse(TabItens["PercRed"].ToString()) > 0)
                                //      TDesconto = TDesconto + (Math.Round(decimal.Parse(TabItens["VLRTOTAL"].ToString()) * (decimal.Parse(TabItens["PercRed"].ToString()) / 100), 2));
                            }
                        }

                        if (decimal.Parse(Tab["Vlrdesconto"].ToString()) > 0)
                        {
                            //PDescItem = Math.Round(100 / (decimal.Parse(Tab["VlrSubTotal"].ToString()) / decimal.Parse(Tab["VlrDesconto"].ToString())), 2);
                            PDescItem = 100 / (decimal.Parse(Tab["VlrSubTotal"].ToString()) / decimal.Parse(Tab["VlrDesconto"].ToString()));
                        }

                        //Nota
                        if (int.Parse(Tab["TipoPgto"].ToString()) == 0)
                            Pgto = "V";
                        else
                            Pgto = "P";

                        if (int.Parse(Tab["TpFrete"].ToString()) == 0)
                            Frete = "D";
                        if (int.Parse(Tab["TpFrete"].ToString()) == 1)
                            Frete = "R";

                        //if (Tab["ChaveNFE"].ToString().Trim() != "")
                            Lista.Add("NFM|0001|E|NFE|N||" + Tab["NFeSerie"].ToString().Trim() + "||" + Tab["NumDocumento"].ToString().Trim() + "|||" + Controle.FormatarData(DateTime.Parse(Tab["DtEmissao"].ToString())) + "||" + Controle.FormatarData(DateTime.Parse(Tab["DTEntSai"].ToString())) + "|" + string.Format("{0:D8}", int.Parse(Tab["Id_Pessoa"].ToString())) +
                                      "|N||||||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRSUBTOTAL"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VlrFrete"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSEGURO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLROUTRASDESP"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()) + decimal.Parse(Tab["OUTROSIPI"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRICMSSUB"].ToString()), 2) + "||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()) + TDesconto, 2) +
                                      "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRTOTAL"].ToString()), 2) + "||N|N|N||||||||" + Frete + "|" + Pgto + "||||||||||||||||||" + Tab["ChaveNFE"].ToString().Trim() + "|||||||||||||N||||||");
                        /*else
                            Lista.Add("NFM|0001|E|NF1|N||" + Tab["NFeSerie"].ToString().Trim() + "||" + Tab["NumDocumento"].ToString().Trim() + "|||" + Controle.FormatarData(DateTime.Parse(Tab["DtEmissao"].ToString())) + "||" + Controle.FormatarData(DateTime.Parse(Tab["DTEntSai"].ToString())) + "|" + string.Format("{0:D8}", int.Parse(Tab["Id_Pessoa"].ToString())) +
                                      "|N||||||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRSUBTOTAL"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VlrFrete"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSEGURO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLROUTRASDESP"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()) + decimal.Parse(Tab["OUTROSIPI"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRICMSSUB"].ToString()), 2) + "||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()) + TDesconto, 2) +
                                      "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRTOTAL"].ToString()), 2) + "||N|N|N||||||||" + Frete + "|" + Pgto + "|||||||||||||||||||||||||||");*/

                        //Itens
                            TabItens = Controle.ConsultaSQL("SELECT T1.*,T2.UNIDADE,T2.CREDITOIPI,T3.CFOP FROM MVESTOQUEITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) LEFT JOIN CFOP T3 ON (T3.ID_CFOP=T1.ID_CFOP) WHERE T1.ID_MOV=" + Tab["ID_MOV"].ToString());
                        decimal TBIcms = 0;
                        decimal TVIcms = 0;

                        if (TabItens.HasRows)
                        {
                            decimal BIcms = 0;
                            decimal BIcmsSub = 0;
                            decimal VDesconto = 0;
                            decimal BPisCofins = 0;
                            TB_IPI = 0;
                            string TpIPI = "1";
                            string TpIcms = "1";
                            string CSTIcms = "00";
                            bool FindLinha;
                            string CstPIS = "01";
                            string CstCOF = "01";
                            string CstIPI = "00";
                            decimal TOutros = 0;
                            

                            DataTable INM = new DataTable();
                            INM.Columns.Add("CFOP", Type.GetType("System.String"));
                            INM.Columns.Add("VlrTotal", Type.GetType("System.Decimal"));
                            INM.Columns.Add("BIcms", Type.GetType("System.Decimal"));
                            INM.Columns.Add("VlrIcms", Type.GetType("System.Decimal"));
                            INM.Columns.Add("BIpi", Type.GetType("System.Decimal"));
                            INM.Columns.Add("VlrIpi", Type.GetType("System.Decimal"));
                            INM.Columns.Add("PIcms", Type.GetType("System.Decimal"));
                            INM.Columns.Add("PIpi", Type.GetType("System.Decimal"));
                            INM.Columns.Add("CST", Type.GetType("System.String"));
                            INM.Columns.Add("Outros", Type.GetType("System.Decimal"));

                            while (TabItens.Read())
                            {
                                TpIPI = "3";
                                if (int.Parse(TabItens["CreditoIPI"].ToString()) == 1)
                                    TpIPI = "1";

                                BIcms     = 0;
                                TpIcms    = "3";
                                VDesconto = 0;
                                CSTIcms   = "30";
                                B_IPI     = 0;
                                BIcmsSub  = 0;
                                TOutros   = 0;

                                if (PDescItem > 0)
                                {
                                    VDesconto = VDesconto + Math.Round((decimal.Parse(TabItens["VlrTotal"].ToString()) * PDescItem / 100), 2); //,MidpointRounding.AwayFromZero);
                                }

                                if (decimal.Parse(TabItens["P_Icms"].ToString()) > 0)
                                {                                    
                                    if (decimal.Parse(TabItens["PercRed"].ToString()) > 0)
                                    {
                                        BIcms = decimal.Parse(TabItens["VlrTotal"].ToString()) - Math.Round((decimal.Parse(TabItens["VlrTotal"].ToString()) * decimal.Parse(TabItens["PercRed"].ToString()) / 100), 2);//,MidpointRounding.AwayFromZero);
                                        // VDesconto = Math.Round((decimal.Parse(TabItens["VlrTotal"].ToString()) * decimal.Parse(TabItens["PercRed"].ToString()) / 100),2); //,MidpointRounding.AwayFromZero);
                                    }
                                    else
                                        BIcms = decimal.Parse(TabItens["VlrTotal"].ToString());
                                    //BIcms = (BIcms - VDesconto) + decimal.Parse(TabItens["VlrFrete"].ToString());
                                    BIcms = (BIcms) + decimal.Parse(TabItens["VlrFrete"].ToString());
                                    TpIcms = "1";
                                }

                                CSTIcms = CSTEntrada(int.Parse(TabItens["CST"].ToString()));

                                TBIcms = TBIcms + BIcms;
                                TVIcms = TVIcms + (BIcms * decimal.Parse(TabItens["P_Icms"].ToString()) / 100);

                                if (decimal.Parse(TabItens["P_Ipi"].ToString()) > 0 && (Tab["CFOP"].ToString().Trim().Replace(".", "") == "1101" || Tab["CFOP"].ToString().Trim().Replace(".", "") == "2101") && (int.Parse(LstFilial.SelectedValue.ToString()) != 1 && int.Parse(LstFilial.SelectedValue.ToString()) != 6 && int.Parse(LstFilial.SelectedValue.ToString()) != 7))
                                {
                                    B_IPI = decimal.Parse(TabItens["VlrTotal"].ToString());
                                    TB_IPI = TB_IPI + decimal.Parse(TabItens["VlrTotal"].ToString());                                    
                                }
                                else
                                    TOutros = TOutros + decimal.Parse(TabItens["VlrTotal"].ToString());

                                if (Tab["CFOP"].ToString().Trim().Replace(".", "") == "1101" || Tab["CFOP"].ToString().Trim().Replace(".", "") == "2101")
                                    BPisCofins = (decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto);
                                else
                                    BPisCofins = (decimal.Parse(TabItens["VlrTotal"].ToString()) + decimal.Parse(TabItens["VlrIcms_Sub"].ToString()) + decimal.Parse(TabItens["VlrIpi"].ToString()) + decimal.Parse(TabItens["VlrFrete"].ToString())) - VDesconto;
                                
                                if (TabItens["NCM"].ToString().Trim() == "10063011" || TabItens["NCM"].ToString().Trim() == "15079011" || TabItens["NCM"].ToString().Trim() == "17019900" || TabItens["NCM"].ToString().Trim() == "19021900" || TabItens["NCM"].ToString().Trim() == "22072010" || TabItens["NCM"].ToString().Trim() == "30049099" || TabItens["NCM"].ToString().Trim() == "33049990" || TabItens["NCM"].ToString().Trim() == "33051000" || TabItens["NCM"].ToString().Trim() == "33072010" || TabItens["NCM"].ToString().Trim() == "33072090" || TabItens["NCM"].ToString().Trim() == "33074900" || TabItens["NCM"].ToString().Trim() == "34011190" || TabItens["NCM"].ToString().Trim() == "34012010"
                                        || TabItens["NCM"].ToString().Trim() == "39159000" || TabItens["NCM"].ToString().Trim() == "48181000" || TabItens["NCM"].ToString().Trim() == "07133990" || TabItens["NCM"].ToString().Trim() == "33059000")
                                {
                                    CstPIS = "73";
                                    CstCOF = "73";
                                    //BPisCofins = 0;
                                }
                                else
                                {
                                    CstPIS = "50";
                                    CstCOF = "50"; 
                                }

                                if (int.Parse(LstFilial.SelectedValue.ToString()) == 1 || int.Parse(LstFilial.SelectedValue.ToString()) == 6 || int.Parse(LstFilial.SelectedValue.ToString()) == 7)
                                {
                                    Lista.Add("PNM|" + string.Format("{0:D8}", int.Parse(TabItens["ID_PRODUTO"].ToString())) + "|" + TabItens["CFOP"].ToString().Replace(".", "") + "||0|" + CSTIcms + "|" + TabItens["UNIDADE"].ToString().Trim() + "|" + Controle.FloatToStr(decimal.Parse(TabItens["QTDE"].ToString()), 3) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrIpi"].ToString()), 2) + "|" + TpIcms + "|" + Controle.FloatToStr(BIcms, 2) + "|" +
                                              Controle.FloatToStr(decimal.Parse(TabItens["P_Icms"].ToString()), 2) + "||" + Controle.FloatToStr(decimal.Parse(TabItens["VlrIcms_Sub"].ToString()), 2) + "|||||||||||||||||||||01|"+CstPIS+"|"+CstCOF+"|" + Controle.FloatToStr(BPisCofins, 2) + "|" + Controle.FloatToStr(BPisCofins, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrFrete"].ToString()), 2) + "|0|" + Controle.FloatToStr(VDesconto, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()) + decimal.Parse(TabItens["VlrFrete"].ToString()) - VDesconto, 2) + "||||" +
                                              "|||1||||1|||||||||||||||||||||");                                    
                                }
                                else
                                {
                                    if (B_IPI > 0)
                                        CstIPI = "00";
                                    else
                                        CstIPI = "49";

                                    Lista.Add("PNM|" + string.Format("{0:D8}", int.Parse(TabItens["ID_PRODUTO"].ToString())) + "|" + TabItens["CFOP"].ToString().Replace(".", "") + "||0|" + CSTIcms + "|" + TabItens["UNIDADE"].ToString().Trim() + "|" + Controle.FloatToStr(decimal.Parse(TabItens["QTDE"].ToString()), 3) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()), 2) + "||" + TpIcms + "|" + Controle.FloatToStr(BIcms, 2) + "|" +
                                        Controle.FloatToStr(decimal.Parse(TabItens["P_Icms"].ToString()), 2) + "||" + Controle.FloatToStr(decimal.Parse(TabItens["VlrIcms_Sub"].ToString()), 2) + "|||||||||||||||||" + TpIPI + "|" + Controle.FloatToStr(B_IPI, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["P_Ipi"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrIpi"].ToString()), 2) + "|"+ CstIPI+"|"+CstPIS+"|"+CstCOF+"|" + Controle.FloatToStr(BPisCofins, 2) + "|" + Controle.FloatToStr(BPisCofins, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrFrete"].ToString()), 2) + "|0|" + Controle.FloatToStr(VDesconto, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()) + decimal.Parse(TabItens["VlrFrete"].ToString()) - VDesconto, 2) + "||||" +
                                        "|||1||||1|||||||||||||||||||||");
                                }

                                FindLinha = false;
                                for (int I = 0; I <= INM.Rows.Count - 1; I++)
                                {
                                    if (INM.Rows[I]["CFOP"].ToString().Trim() == TabItens["CFOP"].ToString().Replace(".", "").Trim())
                                    {
                                        FindLinha = true;
                                        INM.Rows[I]["VlrTotal"] = decimal.Parse(INM.Rows[I]["VlrTotal"].ToString()) + ((decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto) + decimal.Parse(TabItens["VlrIcms_sub"].ToString()));
                                        INM.Rows[I]["BIcms"]    = decimal.Parse(INM.Rows[I]["BIcms"].ToString()) + BIcms;
                                        INM.Rows[I]["BIpi"]     = decimal.Parse(INM.Rows[I]["BIpi"].ToString()) + B_IPI;
                                        INM.Rows[I]["VlrIcms"]  = decimal.Parse(INM.Rows[I]["VlrIcms"].ToString()) + decimal.Parse(TabItens["VlrIcms"].ToString());
                                        INM.Rows[I]["VlrIpi"]   = decimal.Parse(INM.Rows[I]["VlrIpi"].ToString()) + decimal.Parse(TabItens["VlrIpi"].ToString());
                                        INM.Rows[I]["Outros"]   = decimal.Parse(INM.Rows[I]["Outros"].ToString()) + TOutros;
                                        break;
                                    }
                                }

                                if (!FindLinha)
                                    INM.Rows.Add(TabItens["CFOP"].ToString().Replace(".", ""), ((decimal.Parse(TabItens["VlrTotal"].ToString())+decimal.Parse(TabItens["VlrIcms_sub"].ToString())) - VDesconto), BIcms, decimal.Parse(TabItens["VlrIcms"].ToString()), B_IPI, decimal.Parse(TabItens["VlrIpi"].ToString()), decimal.Parse(TabItens["P_Icms"].ToString()), decimal.Parse(TabItens["P_Ipi"].ToString()), CSTIcms, TOutros);
                            }
                            decimal VlrOutro = 0;
                            for (int I = 0; I <= INM.Rows.Count - 1; I++)
                            { 
                                VlrOutro = 0;
                                if (decimal.Parse(INM.Rows[I]["Outros"].ToString()) > 0)
                                    VlrOutro = decimal.Parse(INM.Rows[I]["Outros"].ToString()) + decimal.Parse(INM.Rows[I]["VlrIpi"].ToString());

                                Lista.Add("INM|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VLRTOTAL"].ToString()) + decimal.Parse(INM.Rows[I]["VlrIpi"].ToString()), 2) + "|" + Tab["Sigla"].ToString().Trim() + "|" + INM.Rows[I]["CFOP"] + "||" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["BIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["PIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VlrIcms"].ToString()), 2) + "||" + Controle.FloatToStr(VlrOutro, 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["BIpi"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VlrIpi"].ToString()), 2) + "||||||N|0|" + INM.Rows[I]["CST"].ToString() + "|||||");
                            }
                            //Lista.Add("INM|" + Controle.FloatToStr(decimal.Parse(Tab["VLRTOTAL"].ToString()), 2) + "|" + Tab["Sigla"].ToString().Trim() + "|" + Tab["CFOP"].ToString().Trim().Replace(".", "") + "||" + Controle.FloatToStr(TBIcms, 2) + "||" + Controle.FloatToStr(TVIcms, 2) + "|||" + Controle.FloatToStr(TB_IPI, 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()), 2) + "||||||");
                            INM.Dispose();
                        }
                    }
                }
                //catch
                //{
                //    MessageBox.Show("Verificar a Nota de Entrada " + Tab["NumDocumento"].ToString());
                //}
            }
            return Lista;
        }
        private ArrayList NotaSaida()
        {
            Parametros ParamFilial = new Parametros();
            ParamFilial.Controle = Controle;
            ParamFilial.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));

            string sSQL = "SELECT T1.*,T2.CFOP,T4.SIGLA,T5.NUMAUTORIZACAO, Case T1.Consumidor when 0 then 'N' else 'S' end as ConsFinal FROM NOTAFISCAL T1" +
                          " LEFT JOIN CFOP T2 ON (T2.ID_CFOP=T1.ID_CFOP)" +
                          " LEFT JOIN PESSOAS T3 ON (T3.ID_PESSOA=T1.ID_PESSOA)" +
                          " LEFT JOIN ESTADOS T4 ON (T4.ID_UF=T3.ID_UF)" +
                          " LEFT JOIN AIDF T5 ON (T1.NUMFORMULARIO >= T5.NUMINICIAL AND T1.NUMFORMULARIO <= T5.NUMFINAL) " +
                          " WHERE T1.NFE=1 AND T1.DTEMISSAO >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                          "   AND T1.DTEMISSAO<=Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)" +
                          "   AND T1.ID_FILIAL=" + LstFilial.SelectedValue.ToString();

            SqlDataReader TabItens;
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            ArrayList Lista = new ArrayList();
            ArrayList ListaINM = new ArrayList();
            if (Tab.HasRows)
            {
                try
                {
                    string Frete = "N";
                    decimal PDescItem = 0;
                    string EntSai = "S";
                    decimal B_IPI = 0;
                    decimal TB_IPI = 0;

                    while (Tab.Read())
                    {
                        if (int.Parse(Tab["Frete"].ToString()) == 0)
                            Frete = "R";
                        if (int.Parse(Tab["Frete"].ToString()) == 1)
                            Frete = "D";

                        EntSai = "S";
                        if (int.Parse(Tab["EntSaida"].ToString()) == 1)
                            EntSai = "E";

                        PDescItem = 0;
                        if (decimal.Parse(Tab["Vlrdesconto"].ToString()) > 0)
                            PDescItem = 100 / (decimal.Parse(Tab["VlrProdutos"].ToString()) / decimal.Parse(Tab["VlrDesconto"].ToString()));
                                                
                        //Nota
                        if (Tab["Status"].ToString() == "2")
                        {
                            if (EntSai == "S")
                            {
                                Lista.Add("NFM|0001|" + EntSai + "|NFE|S||1||" + Tab["NumNota"].ToString().Trim() + "|||" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "|1||||||||||||||||||||||||||||||||||||||||||||||||||||||" + Tab["ChaveNFE"].ToString().Trim() + "|||||||||||" + Tab["ChaveNfeDev"].ToString().Trim()+"||"+Tab["ConsFinal"].ToString()+"|1|||||");
                                ///if (int.Parse(Tab["NFE"].ToString()) == 1)
                                //
                               // else
                               //     Lista.Add("NFM|0001|" + EntSai + "|NF1|S|" + Tab["NumAutorizacao"].ToString().Trim() + "|||" + Tab["NumNota"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "|1||||||||||||||||||||||||||||||||||||||||||||||||||||||" + Tab["ChaveNFE"].ToString() + "|||||||||");
                            }
                            else
                            {
                                //if (int.Parse(Tab["NFE"].ToString()) == 1)
                                    Lista.Add("NFM|0001|" + EntSai + "|NFE|S||1||" + Tab["NumNota"].ToString().Trim() + "|||" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "|1||||||||||||||||||||||||||||||||||||||||||||||||||||||" + Tab["ChaveNFE"].ToString() + "|||||||||||" + Tab["ChaveNfeDev"].ToString().Trim() + "||" + Tab["ConsFinal"].ToString() + "|1|||||");
                                /*else
                                    Lista.Add("NFM|0001|" + EntSai + "|NF1|S|" + Tab["NumAutorizacao"].ToString().Trim() + "|||" + Tab["NumNota"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "|||||||||||||||||||||||||||||||||||||||||||||||||||||||" + Tab["ChaveNFE"].ToString() + "|||||||||");*/
                            }
                        }
                        else if (Tab["Status"].ToString() == "1")
                        {
                            if (EntSai == "S")
                            {
                               /* if (int.Parse(Tab["NFE"].ToString()) == 1)
                                    Lista.Add("NFM|0001|" + EntSai + "|NFE|S||1||" + Tab["NumNota"].ToString().Trim() + "|||" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "|0|" + Controle.FormatarData(DateTime.Parse(Tab["DTEMISSAO"].ToString())) + "|" + string.Format("{0:D8}", int.Parse(Tab["Id_Pessoa"].ToString())) +
                                              "|N||||||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VlrFrete"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSEGURO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLROUTRADESP"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) +
                                              "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRNOTA"].ToString()), 2) + "||N|N|N|||||||||V|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "||||||||||||||" + Tab["ChaveNFE"].ToString() + "|||||||||");
                                else
                                    Lista.Add("NFM|0001|" + EntSai + "|NF1|S|" + Tab["NumAutorizacao"].ToString().Trim() + "|||" + Tab["NumNota"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "|0|" + Controle.FormatarData(DateTime.Parse(Tab["DTEMISSAO"].ToString())) + "|" + string.Format("{0:D8}", int.Parse(Tab["Id_Pessoa"].ToString())) +
                                              "|N||||||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VlrFrete"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSEGURO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLROUTRADESP"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) +
                                              "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRNOTA"].ToString()), 2) + "||N|N|N|||||||||V|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "||||||||||||||" + Tab["ChaveNFE"].ToString() + "|||||||||");*/
                                Lista.Add("NFM|0001|" + EntSai + "|NFE|S||1||" + Tab["NumNota"].ToString().Trim() + "|||" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "|0|" + Controle.FormatarData(DateTime.Parse(Tab["DTEMISSAO"].ToString())) + "|" + string.Format("{0:D8}", int.Parse(Tab["Id_Pessoa"].ToString())) +
                                          "|N||||||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VlrFrete"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSEGURO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLROUTRADESP"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) +
                                          "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRNOTA"].ToString()), 2) + "||N|N|N|||||||||V|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "||||||||||||||" + Tab["ChaveNFE"].ToString().Trim() + "|||||||||||||" + Tab["ConsFinal"].ToString() + "|1|||||");
                            }
                            else
                            {
                                //if (int.Parse(Tab["NFE"].ToString()) == 1)
                                    Lista.Add("NFM|0001|" + EntSai + "|NFE|S||1||" + Tab["NumNota"].ToString().Trim() + "|||" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "||" + Controle.FormatarData(DateTime.Parse(Tab["DTEMISSAO"].ToString())) + "|" + string.Format("{0:D8}", int.Parse(Tab["Id_Pessoa"].ToString())) +
                                              "|N||||||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VlrFrete"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSEGURO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLROUTRADESP"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) +
                                              "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRNOTA"].ToString()), 2) + "||N|N|N|||||||||V||||||||||||||||||" + Tab["ChaveNFE"].ToString() + "|||||||||||||N|1|||||");
                                /*else
                                    Lista.Add("NFM|0001|" + EntSai + "|NF1|S|" + Tab["NumAutorizacao"].ToString().Trim() + "|||" + Tab["NumNota"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Tab["NumFormulario"].ToString().Trim() + "|" + Controle.FormatarData(DateTime.Parse(Tab["DTEmissao"].ToString())) + "||" + Controle.FormatarData(DateTime.Parse(Tab["DTEMISSAO"].ToString())) + "|" + string.Format("{0:D8}", int.Parse(Tab["Id_Pessoa"].ToString())) +
                                              "|N||||||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VlrFrete"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSEGURO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLROUTRADESP"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) +
                                              "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRNOTA"].ToString()), 2) + "||N|N|N|||||||||V|||" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRPRODUTOS"].ToString()), 2) + "||||||||||||||" + Tab["ChaveNFE"].ToString() + "|||||||||");*/
                            }

                            DataTable INM = new DataTable();
                            INM.Columns.Add("CFOP", Type.GetType("System.String"));
                            INM.Columns.Add("VlrTotal", Type.GetType("System.Decimal"));
                            INM.Columns.Add("BIcms", Type.GetType("System.Decimal"));
                            INM.Columns.Add("VlrIcms", Type.GetType("System.Decimal"));
                            INM.Columns.Add("BIpi", Type.GetType("System.Decimal"));
                            INM.Columns.Add("VlrIpi", Type.GetType("System.Decimal"));
                            INM.Columns.Add("PIcms", Type.GetType("System.Decimal"));
                            INM.Columns.Add("PIpi", Type.GetType("System.Decimal"));
                            INM.Columns.Add("CST", Type.GetType("System.String"));

                            //Itens                            
                            TabItens = Controle.ConsultaSQL("SELECT T1.*,T2.UNIDADE,T2.SitTributaria,T2.Reducao,T3.CFOP,T2.NCM FROM NOTAFISCALITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) LEFT JOIN CFOP T3 ON (T3.ID_CFOP=T1.ID_CFOP) WHERE T1.VlrTotal > 0 and T1.ID_NOTA=" + Tab["ID_NOTA"].ToString() + " ORDER BY T1.ID_CFOP");
                            decimal PIcms = 0;
                            if (TabItens.HasRows)
                            {
                                decimal BIcms = 0;
                                string TpIPI = "1";
                                string TpIcms = "1";
                                decimal VDesconto = 0;
                                string CSTIcms = "00";
                                TB_IPI = 0;
                                bool FindLinha;
                                string CstPIS = "01";
                                string CstCOF = "01";
                                decimal B_PISCOF = 0;

                                while (TabItens.Read())
                                {
                                    TpIPI = "52";
                                    if (decimal.Parse(TabItens["PIpi"].ToString()) > 0)
                                        TpIPI = "50";

                                    BIcms = 0;
                                    TpIcms = "2";
                                    VDesconto = 0;
                                    CSTIcms = "00";
                                    B_IPI = 0;

                                    CSTIcms = "60";
                                    if (decimal.Parse(TabItens["PIcms"].ToString()) > 0)
                                    {
                                        CSTIcms = "00";

                                        if (ParamFilial.IdFilial == 1 && Tab["INSC_UF"].ToString().Trim() != "" && Tab["ID_UF"].ToString().Trim() != "7")
                                            CSTIcms = "90";

                                        if (decimal.Parse(TabItens["PercRed"].ToString()) > 0)
                                        {
                                            if (ParamFilial.IdFilial == 1 && Tab["INSC_UF"].ToString().Trim() != "" && Tab["ID_UF"].ToString().Trim() != "7")
                                                CSTIcms = "90";
                                            else
                                                CSTIcms = "20";
                                            BIcms = decimal.Parse(TabItens["VlrTotal"].ToString()) - Math.Round((decimal.Parse(TabItens["VlrTotal"].ToString()) * decimal.Parse(TabItens["PercRed"].ToString()) / 100), 2); //,MidpointRounding.AwayFromZero);
                                            // VDesconto = Math.Round((decimal.Parse(TabItens["VlrTotal"].ToString()) * decimal.Parse(TabItens["PercRed"].ToString()) / 100), 2); //,MidpointRounding.AwayFromZero);
                                        }
                                        else
                                            BIcms = decimal.Parse(TabItens["VlrTotal"].ToString());
                                        //BIcms = decimal.Parse(TabItens["VlrTotal"].ToString());
                                        TpIcms = "1";
                                    }

                                    if (PDescItem > 0)
                                    {
                                        VDesconto = Math.Round(VDesconto + (decimal.Parse(TabItens["VlrTotal"].ToString()) * PDescItem / 100), 2);
                                        //VDesconto = VDesconto + Math.Round((decimal.Parse(TabItens["VlrTotal"].ToString()) * PDescItem / 100),2);;
                                    }
                                    if (decimal.Parse(TabItens["PIpi"].ToString()) > 0)
                                    {
                                        B_IPI = decimal.Parse(TabItens["VlrTotal"].ToString());
                                        TB_IPI = TB_IPI + decimal.Parse(TabItens["VlrTotal"].ToString());
                                    }

                                    //Atualização depois
                                   /* if (TabItens["NCM"].ToString().Trim() == "10063011" || TabItens["NCM"].ToString().Trim() == "15079011" || TabItens["NCM"].ToString().Trim() == "17019900" || TabItens["NCM"].ToString().Trim() == "19021900" || TabItens["NCM"].ToString().Trim() == "22072010" || TabItens["NCM"].ToString().Trim() == "30049099" || TabItens["NCM"].ToString().Trim() == "33049990" || TabItens["NCM"].ToString().Trim() == "33051000" || TabItens["NCM"].ToString().Trim() == "33072010" || TabItens["NCM"].ToString().Trim() == "33072090" || TabItens["NCM"].ToString().Trim() == "33074900" || TabItens["NCM"].ToString().Trim() == "34011190" || TabItens["NCM"].ToString().Trim() == "34012010"
                                        || TabItens["NCM"].ToString().Trim() == "39159000" || TabItens["NCM"].ToString().Trim() == "48181000" || TabItens["NCM"].ToString().Trim() == "07133990" || TabItens["NCM"].ToString().Trim() == "33059000")
                                    {
                                        CstPIS = "06";
                                        CstCOF = "06";
                                        B_PISCOF = decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto; ;
                                    }
                                    else
                                    {
                                        CstPIS = "01";
                                        CstCOF = "01";
                                        B_PISCOF = decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto;

                                    }*/
                                    CstPIS = "01";
                                    CstCOF = "01";
                                    B_PISCOF = decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto;
                                    if (EntSai == "S")
                                        Lista.Add("PNM|" + string.Format("{0:D8}", int.Parse(TabItens["ID_PRODUTO"].ToString())) + "|" + TabItens["CFOP"].ToString().Replace(".", "") + "||0|" + CSTIcms + "|" + TabItens["UNIDADE"].ToString().Trim() + "|" + Controle.FloatToStr(decimal.Parse(TabItens["QTDE"].ToString()), 3) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()), 3) + "||" + TpIcms + "|" + Controle.FloatToStr(BIcms, 2) + "|" +
                                                  Controle.FloatToStr(decimal.Parse(TabItens["PIcms"].ToString()), 2) + "|||||||||||||||||||1|" + Controle.FloatToStr(B_IPI, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["PIpi"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrIpi"].ToString()), 2) + "|" + TpIPI + "|"+CstPIS+"|"+CstCOF+"|" + Controle.FloatToStr(B_PISCOF, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto, 2) + "|0|0|" + Controle.FloatToStr(VDesconto, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto, 2) + "||||" +
                                                  "|||1||||1|||||||||||||||||||||");
                                    else
                                        Lista.Add("PNM|" + string.Format("{0:D8}", int.Parse(TabItens["ID_PRODUTO"].ToString())) + "|" + TabItens["CFOP"].ToString().Replace(".", "") + "||0|" + CSTIcms + "|" + TabItens["UNIDADE"].ToString().Trim() + "|" + Controle.FloatToStr(decimal.Parse(TabItens["QTDE"].ToString()), 3) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()), 3) + "||" + TpIcms + "|" + Controle.FloatToStr(BIcms, 2) + "|" +
                                              Controle.FloatToStr(decimal.Parse(TabItens["PIcms"].ToString()), 2) + "|||||||||||||||||||1|" + Controle.FloatToStr(B_IPI, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["PIpi"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrIpi"].ToString()), 2) + "|00|50|50|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()) + decimal.Parse(TabItens["VlrIpi"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()) + decimal.Parse(TabItens["VlrIpi"].ToString()), 2) + "|0|0|" + Controle.FloatToStr(VDesconto, 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto, 2) + "||||" +
                                              "|||1||||1|||||||||||||||||||||");



                                    FindLinha = false;
                                    for (int I = 0; I <= INM.Rows.Count - 1; I++)
                                    {
                                        if (INM.Rows[I]["CFOP"].ToString().Trim() == TabItens["CFOP"].ToString().Replace(".", "").Trim())
                                        {
                                            FindLinha = true;
                                            INM.Rows[I]["VlrTotal"] = decimal.Parse(INM.Rows[I]["VlrTotal"].ToString()) + (decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto);
                                            INM.Rows[I]["BIcms"] = decimal.Parse(INM.Rows[I]["BIcms"].ToString()) + BIcms;
                                            INM.Rows[I]["BIpi"] = decimal.Parse(INM.Rows[I]["BIpi"].ToString()) + B_IPI;
                                            INM.Rows[I]["VlrIcms"] = decimal.Parse(INM.Rows[I]["VlrIcms"].ToString()) + decimal.Parse(TabItens["VlrIcms"].ToString());
                                            INM.Rows[I]["VlrIpi"] = decimal.Parse(INM.Rows[I]["VlrIpi"].ToString()) + decimal.Parse(TabItens["VlrIpi"].ToString());
                                            break;
                                        }
                                    }
                                    if (!FindLinha)
                                        INM.Rows.Add(TabItens["CFOP"].ToString().Replace(".", ""), (decimal.Parse(TabItens["VlrTotal"].ToString()) - VDesconto), BIcms, decimal.Parse(TabItens["VlrIcms"].ToString()), B_IPI, decimal.Parse(TabItens["VlrIpi"].ToString()), decimal.Parse(TabItens["PIcms"].ToString()), decimal.Parse(TabItens["PIpi"].ToString()), CSTIcms);
                                }
                            }                        
                            PIcms = 0;
                            if (decimal.Parse(Tab["VLRICMS"].ToString()) > 0)
                                PIcms = (1 - Math.Round((Math.Round((decimal.Parse(Tab["BICMS"].ToString()) - decimal.Parse(Tab["VLRICMS"].ToString())), 2) / decimal.Parse(Tab["BICMS"].ToString())), 2)) * 100;

                            for (int I = 0; I <= INM.Rows.Count - 1; I++)
                                Lista.Add("INM|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VLRTOTAL"].ToString()) + decimal.Parse(INM.Rows[I]["VlrIpi"].ToString()), 2) + "|" + Tab["Sigla"].ToString().Trim() + "|" + INM.Rows[I]["CFOP"] + "||" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["BIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["PIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VlrIcms"].ToString()), 2) + "|||" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["BIpi"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VlrIpi"].ToString()), 2) + "||||||N|0|" + INM.Rows[I]["CST"].ToString() + "|||||");
                            INM.Dispose();
                           // Lista.Add("INM|" + Controle.FloatToStr(decimal.Parse(Tab["VLRNOTA"].ToString()), 2) + "|" + Tab["Sigla"].ToString().Trim() + "|" + Tab["CFOP"].ToString().Trim().Replace(".", "") + "||" + Controle.FloatToStr(decimal.Parse(Tab["BICMS"].ToString()), 2) + "|" + Controle.FloatToStr(PIcms, 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRICMS"].ToString()), 2) + "|||" + Controle.FloatToStr(TB_IPI, 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRIPI"].ToString()), 2) + "||||||");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Verificar a Nota de Saida " + Tab["NumNota"].ToString());
                }
            }
            return Lista;
        }

        private ArrayList CupomFiscalSaida()
        {
            Filiais CadFilial = new Filiais();
            CadFilial.Controle = Controle;
            CadFilial.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));

            Parametros ParamFilial = new Parametros();
            ParamFilial.Controle = Controle;
            ParamFilial.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));


            
            string sSQL = "";

            sSQL = "SELECT T1.DATA,ISNULL(SUM(T1.VLRSUBTOTAL),0) AS VLRSUBTOTAL,ISNULL(SUM(T1.VLRTOTAL),0) AS VLRTOTAL,ISNULL(SUM(T1.VLRDESCONTO),0) AS VLRDESCONTO FROM CUPOMFISCAL T1" +
                   " WHERE T1.DATA >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                   "   AND T1.DATA <=Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)" +
                   "   AND T1.STATUS=1 GROUP BY T1.DATA ORDER BY T1.DATA";

            SqlDataReader TabCF;
            SqlDataReader TabItens;
            SqlDataReader Tab = Controle.ConsultaSQL(sSQL);
            ArrayList Lista = new ArrayList();
            if (Tab.HasRows)
            {
                try
                {
                    decimal TIcmsSub = 0;
                    decimal TIcmsIsento = 0;
                    decimal TIcmsNaoTrib = 0;
                    decimal TIcms = 0;
                    decimal TBIcms = 0;
                    string CFOP = "";
                    while (Tab.Read())
                    {
                        TabItens = Controle.ConsultaSQL("SELECT * FROM CUPOMFISCALITENS T1 " +
                                                        " LEFT JOIN CUPOMFISCAL T2 ON (T2.ID_LANC=T1.ID_LANC) " +
                                                        " WHERE T2.DATA =Convert(DateTime,'" + Tab["DATA"].ToString() + "',103)" +
                                                        "   AND T2.STATUS=1 ");
                        TIcmsSub = 0;
                        TIcmsIsento = 0;
                        TIcmsNaoTrib = 0;
                        TIcms = 0;
                        TBIcms = 0;
                        CFOP = "";

                        if (TabItens.HasRows)
                        {
                            while (TabItens.Read())
                            {
                                TIcmsNaoTrib = TIcmsNaoTrib + decimal.Parse(TabItens["VlrNaoTributado"].ToString());
                                TIcmsIsento  = TIcmsIsento + decimal.Parse(TabItens["VlrIsento"].ToString());
                                TIcmsSub     = TIcmsSub + decimal.Parse(TabItens["VlrSubstituicao"].ToString());
                                TIcms        = TIcms + decimal.Parse(TabItens["VlrIcms"].ToString());
                                TBIcms       = TBIcms + decimal.Parse(TabItens["Vlr_BIcms"].ToString());
                            }
                        }
                        Lista.Add("CFC|0001|" + Controle.FormatarData(DateTime.Parse(Tab["DATA"].ToString())) + "|001|0001|0001|0001|0001|0001|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSUBTOTAL"].ToString()) - decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSUBTOTAL"].ToString()) - decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) + "||" + Controle.FloatToStr(decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) + "|" + Controle.FloatToStr(TIcmsSub, 2) + "|" + Controle.FloatToStr(TIcmsIsento, 2) + "|" + Controle.FloatToStr(TIcmsNaoTrib, 2) + "||N||||||" + Controle.FloatToStr(decimal.Parse(Tab["VLRSUBTOTAL"].ToString()) - decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(Tab["VLRSUBTOTAL"].ToString()) - decimal.Parse(Tab["VLRDESCONTO"].ToString()), 2) + "||||||||N||");

                        sSQL = "SELECT * FROM CUPOMFISCAL T1 WHERE T1.DATA =Convert(DateTime,'" + Tab["DATA"].ToString() + "',103) AND T1.STATUS=1 ";

                        DataTable INM = new DataTable();
                        INM.Columns.Add("CFOP", Type.GetType("System.String"));
                        INM.Columns.Add("VlrTotal", Type.GetType("System.Decimal"));
                        INM.Columns.Add("BIcms", Type.GetType("System.Decimal"));
                        INM.Columns.Add("VlrIcms", Type.GetType("System.Decimal"));
                        INM.Columns.Add("BIpi", Type.GetType("System.Decimal"));
                        INM.Columns.Add("VlrIpi", Type.GetType("System.Decimal"));
                        INM.Columns.Add("PIcms", Type.GetType("System.Decimal"));
                        INM.Columns.Add("PIpi", Type.GetType("System.Decimal"));
                        INM.Columns.Add("CST", Type.GetType("System.String"));

                        TabCF = Controle.ConsultaSQL(sSQL);
                        while (TabCF.Read())
                        {                           

                            Lista.Add("CCF|" + TabCF["NUM_CF"].ToString() + "||" + Controle.FloatToStr(decimal.Parse(TabCF["VlrDesconto"].ToString()), 2) + "|||||||||||||||||||||||||");

                            //Itens                            
                            TabItens = Controle.ConsultaSQL("SELECT T1.*,T2.UNIDADE FROM CUPOMFISCALITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_LANC=" + TabCF["ID_LANC"].ToString());
                            if (TabItens.HasRows)
                            {
                                string TpIcms = "1";
                                string CSTIcms = "00";
                                bool FindLinha;
                                while (TabItens.Read())
                                {
                                    if (TabItens["SitTributaria"].ToString() == "0")
                                    {
                                        TpIcms  = "0";
                                        CSTIcms = "00";
                                        CFOP    = "5102";
                                    }
                                    if (TabItens["SitTributaria"].ToString() == "1")
                                    {
                                        TpIcms  = "3";
                                        CSTIcms = "41";
                                        CFOP    = "5103";
                                    }
                                    if (TabItens["SitTributaria"].ToString() == "2")
                                    {
                                        TpIcms  = "2";
                                        CSTIcms = "40";
                                        CFOP    = "5102";
                                    }
                                    if (TabItens["SitTributaria"].ToString() == "3")
                                    {
                                        TpIcms  = "1";
                                        CSTIcms = "60";
                                        CFOP    = "5403";
                                    }
                                    //TBIcms = TBIcms + decimal.Parse(TabItens["Vlr_BIcms"].ToString());                                    
                                    if (CadFilial.Regime == 2)
                                        Lista.Add("PCC|" + string.Format("{0:D8}", int.Parse(TabItens["ID_PRODUTO"].ToString())) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["QTDE"].ToString()), 3) + "|" + TabItens["UNIDADE"].ToString().Trim() + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()), 2) + "|" + CFOP + "|" + TpIcms + "|" + Controle.FloatToStr(decimal.Parse(TabItens["Vlr_BIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["P_Icms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VLRTOTAL"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VLRTOTAL"].ToString()), 2) + "|0|" + CSTIcms + "|01|01|||||" + Controle.FloatToStr(decimal.Parse(TabItens["VLRTOTAL"].ToString()), 2) + "|1||||1||||||");
                                    else
                                        Lista.Add("PCC|" + string.Format("{0:D8}", int.Parse(TabItens["ID_PRODUTO"].ToString())) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["QTDE"].ToString()), 3) + "|" + TabItens["UNIDADE"].ToString().Trim() + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VlrTotal"].ToString()), 2) + "|" + CFOP + "|" + TpIcms + "|" + Controle.FloatToStr(decimal.Parse(TabItens["Vlr_BIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["P_Icms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VLRTOTAL"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(TabItens["VLRTOTAL"].ToString()), 2) + "|||01|01|||||" + Controle.FloatToStr(decimal.Parse(TabItens["VLRTOTAL"].ToString()), 2) + "|1||||1|||||0|500");

                                    FindLinha = false;
                                    for (int I = 0; I <= INM.Rows.Count - 1; I++)
                                    {
                                        if (INM.Rows[I]["CFOP"].ToString().Trim() == CFOP)
                                        {
                                            FindLinha = true;
                                            INM.Rows[I]["VlrTotal"] = decimal.Parse(INM.Rows[I]["VlrTotal"].ToString()) + (decimal.Parse(TabItens["VlrTotal"].ToString()));
                                            INM.Rows[I]["BIcms"]    = decimal.Parse(INM.Rows[I]["BIcms"].ToString()) + decimal.Parse(TabItens["Vlr_BIcms"].ToString());
                                            INM.Rows[I]["VlrIcms"]  = decimal.Parse(INM.Rows[I]["VlrIcms"].ToString()) + decimal.Parse(TabItens["VlrIcms"].ToString());                                            
                                            break;
                                        }
                                    }

                                    if (!FindLinha)
                                        INM.Rows.Add(CFOP, (decimal.Parse(TabItens["VlrTotal"].ToString())), decimal.Parse(TabItens["Vlr_BIcms"].ToString()), decimal.Parse(TabItens["VlrIcms"].ToString()), 0, 0, decimal.Parse(TabItens["P_Icms"].ToString()), 0, CSTIcms);
                                }
                            }
                        }

                        for (int I = 0; I <= INM.Rows.Count - 1; I++)
                        {
                            if (CadFilial.Regime == 2)
                                Lista.Add("ICF|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VLRTOTAL"].ToString()), 2) + "|CE|" + INM.Rows[I]["CFOP"] + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["BIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["PIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VlrIcms"].ToString()), 2) + "||||N|N|0|" + INM.Rows[I]["CST"] + "||||||");                                
                            else
                                Lista.Add("ICF|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VLRTOTAL"].ToString()), 2) + "|CE|" + INM.Rows[I]["CFOP"] + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["BIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["PIcms"].ToString()), 2) + "|" + Controle.FloatToStr(decimal.Parse(INM.Rows[I]["VlrIcms"].ToString()), 2) + "||||N|N|||0|102||||");                                
                                
                        }
                        INM.Dispose();

                        //Lista.Add("ICF|" + Controle.FloatToStr(TBIcms, 2) + "|CE|5102|" + Controle.FloatToStr(TBIcms, 2) + "|17|" + Controle.FloatToStr(TIcms, 2) + "|||N|N|N");
                    }
                }
                catch
                {
                   // MessageBox.Show("Verificar a Venda:" + Tab["ID_VENDA"].ToString());
                }
            }
            return Lista;
        }
        private ArrayList ListaGrupo()
        {
            string sSQL = "SELECT * FROM GRUPOPRODUTO";
            SqlDataReader ConsGrupo = Controle.ConsultaSQL(sSQL);
            ArrayList ListaGrp = new ArrayList();
            if (ConsGrupo.HasRows)
            {
                while (ConsGrupo.Read())
                {
                    ListaGrp.Add("GRP|" + string.Format("{0:D8}", int.Parse(ConsGrupo["ID_GRUPO"].ToString())) + "|" + Controle.Space(ConsGrupo["GRUPO"].ToString().Trim(), 40) + "|7|99|");
                }
            }
            return ListaGrp;
        }
        private ArrayList ListaUnidades()
        {
            string sSQL = "SELECT CHAVE,DESCRICAO FROM TABELASAUX WHERE CAMPO='UNIDADE'";
            SqlDataReader ConsUnidade = Controle.ConsultaSQL(sSQL);
            ArrayList ListaUnd = new ArrayList();
            if (ConsUnidade.HasRows)
            {
                while (ConsUnidade.Read())
                {
                    ListaUnd.Add("UND|" + ConsUnidade["CHAVE"].ToString() + "|" + Controle.Space(ConsUnidade["DESCRICAO"].ToString().Trim(), 60));
                }
            }
            return ListaUnd;
        }
        private ArrayList ListaProdutos()
        {
            string sSQL = "SELECT DISTINCT T2.ID_PRODUTO,T2.REFERENCIA,T2.DESCRICAO,T2.UNIDADE,T5.DESCRICAO AS DESCUND,T2.ID_GRUPO,T2.REDUCAO,T2.ID_GENERO,T2.ID_GRUPO,T2.SITTRIBUTARIA,T2.NCM FROM NOTAFISCALITENS T1" +
                        "   LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                        "   LEFT JOIN NOTAFISCAL T3 ON (T3.ID_NOTA=T1.ID_NOTA)" +
                        "   LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T2.ID_GRUPO)" +
                        "   LEFT JOIN TABELASAUX T5 ON (T5.CHAVE=T2.UNIDADE) " +
                        " WHERE T3.DTEMISSAO >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                        "   AND T3.DTEMISSAO <=Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)" +
                        "   AND T3.ID_FILIAL=" + LstFilial.SelectedValue.ToString() +
                        "   AND T2.DESCRICAO IS NOT NULL" +
                        " UNION " +
                        "SELECT DISTINCT T2.ID_PRODUTO,T2.REFERENCIA,T2.DESCRICAO,T2.UNIDADE,T5.DESCRICAO AS DESCUND,T2.ID_GRUPO,T2.REDUCAO,T2.ID_GENERO,T2.ID_GRUPO,T2.SITTRIBUTARIA,T2.NCM FROM MVESTOQUEITENS T1" +
                        "   LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO)" +
                        "   LEFT JOIN MVESTOQUE T3 ON (T3.ID_MOV=T1.ID_MOV AND T3.TPMOV='ENTNF')" +
                        "   LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T2.ID_GRUPO)" +
                        "   LEFT JOIN TABELASAUX T5 ON (T5.CHAVE=T2.UNIDADE) " +
                        " WHERE T3.DTENTSAI >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                        "   AND T3.DTENTSAI <=Convert(DateTime,'" + Dt2.Value.ToShortDateString() + "',103)" +
                        "   AND T3.ID_FILIALORIGDEST=" + LstFilial.SelectedValue.ToString() +
                        "   AND T2.DESCRICAO IS NOT NULL" +
                        " UNION " +
                        "SELECT DISTINCT T3.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO,T3.UNIDADE,T5.DESCRICAO AS DESCUND,T3.ID_GRUPO,T3.REDUCAO,T3.ID_GENERO,T3.ID_GRUPO,T3.SITTRIBUTARIA,T3.NCM FROM CUPOMFISCALITENS T1" +
                        "   LEFT JOIN CUPOMFISCAL T2 ON (T2.ID_LANC=T1.ID_LANC) " +
                        "   LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.ID_PRODUTO)" +
                        "   LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO) " +
                        "   LEFT JOIN TABELASAUX T5 ON (T5.CHAVE=T3.UNIDADE) " +
                        " WHERE T2.DATA >=Convert(DateTime,'" + Dt1.Value.ToShortDateString() + "',103)" +
                        "   AND T3.DESCRICAO IS NOT NULL ";
            /* " UNION " +
             "SELECT DISTINCT T3.ID_PRODUTO,T3.REFERENCIA,T3.DESCRICAO,T3.UNIDADE,T5.DESCRICAO AS DESCUND,T3.ID_GRUPO,T3.REDUCAO,T3.ID_GENERO,T3.ID_GRUPO,T3.SITTRIBUTARIA FROM INVENTARIO T1" +                        
             "   LEFT JOIN PRODUTOS T3 ON (T3.ID_PRODUTO=T1.IDPRODUTO)" +
             "   LEFT JOIN GRUPOPRODUTO T4 ON (T4.ID_GRUPO=T3.ID_GRUPO) " +
             "   LEFT JOIN TABELASAUX T5 ON (T5.CHAVE=T3.UNIDADE) " +
             " ORDER BY T2.ID_PRODUTO";*/
            SqlDataReader ConsProdutos = Controle.ConsultaSQL(sSQL);
            ArrayList ListaPrd = new ArrayList();
            if (ConsProdutos.HasRows)
            {
                string Cst_Icms = "";
                while (ConsProdutos.Read())
                {
                    if (int.Parse(ConsProdutos["SitTributaria"].ToString()) == 0)
                        Cst_Icms = "000";
                    else if (int.Parse(ConsProdutos["SitTributaria"].ToString()) == 3)
                        Cst_Icms = "010";
                    else if (decimal.Parse(ConsProdutos["REDUCAO"].ToString()) > 0)
                        Cst_Icms = "020";
                    else
                        Cst_Icms = "030";
                    ListaPrd.Add("PRO|" + string.Format("{0:D8}", int.Parse(ConsProdutos["ID_PRODUTO"].ToString())) + "|" + Controle.Space(ConsProdutos["DESCRICAO"].ToString().Trim(), 60) + "|" + Controle.Space(ConsProdutos["REFERENCIA"].ToString().Trim(), 14) + "|" + ConsProdutos["NCM"].ToString().Trim() + "|" + Controle.Space(ConsProdutos["UNIDADE"].ToString().Trim(), 3) + "|8|22||"
                                 + string.Format("{0:D3}", int.Parse(ConsProdutos["ID_GRUPO"].ToString())) + "|" + string.Format("{0:D2}", int.Parse(ConsProdutos["ID_GENERO"].ToString())) + "||" + Controle.FloatToStr(decimal.Parse(ConsProdutos["REDUCAO"].ToString()), 2) + "||" + Cst_Icms + "|||||||||N||||||||||||");
                    ListaPrd.Add("OUM|" + string.Format("{0:D8}", int.Parse(ConsProdutos["ID_PRODUTO"].ToString())) + "|" + Controle.Space(ConsProdutos["UNIDADE"].ToString().Trim(), 3) + "|1.000|");//+Controle.Space(ConsProdutos["DESCUND"].ToString().Trim(),40));
                }
            }
            return ListaPrd;
        }

        private ArrayList ListaInventario()
        {
            SqlDataReader ConsProdutos = Controle.ConsultaSQL("SELECT * FROM INVENTARIO");
            ArrayList ListaPrd = new ArrayList();
            if (ConsProdutos.HasRows)
            {
                while (ConsProdutos.Read())
                    ListaPrd.Add("IIV|0001|" + Controle.FormatarData(Dt1.Value) + "|" + string.Format("{0:D8}", int.Parse(ConsProdutos["IDPRODUTO"].ToString())) + "|" + Controle.FloatToStr(decimal.Parse(ConsProdutos["QTDE"].ToString()), 3) + "|" + Controle.Space(ConsProdutos["UND"].ToString().Trim(), 3) + "|" + Controle.FloatToStr(decimal.Parse(ConsProdutos["Valor"].ToString()), 2));
            }
            return ListaPrd;
        }

        private string CSTEntrada(int Op)
        {
            if (Op == 0)
                return "";
            else if (Op == 1)
                return "00";
            else if (Op == 2)
                return "10";
            else if (Op == 3)
                return "20";
            else if (Op == 4)
                return "30";
            else if (Op == 5)
                return "40";
            else if (Op == 6)
                return "41";
            else if (Op == 7)
                return "50";
            else if (Op == 8)
                return "60";
            else if (Op == 9)
                return "70";
            else if (Op == 10)
                return "90";
            else
                return "";
        }

    }
}

