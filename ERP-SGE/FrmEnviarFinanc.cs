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
using System.Xml;
using System.Xml.Linq;
using System.Collections;

namespace ERP_SGE
{
    public partial class FrmEnviarFinanc : Form
    {
        public TelaPrincipal FrmPrincipal;        
        Funcoes Controle = new Funcoes();

        public FrmEnviarFinanc()
        {
            InitializeComponent();
        }
        private void FrmEnviarFinanc_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
        }
        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirma o Envio dos dados ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BtnEnviar.Enabled = false;
                    BtnEnviar.Text = "Aguarde...";
                    ProcBar.Value = 0;
                    Application.DoEvents();

                    string sSQL = "SELECT T1.*,T7.DATA,T2.IDSERVIDOR AS IDSERVPESSOA, T2.CNPJ,T3.IDSERVIDOR AS IDTIPODOC,T4.IDSERVIDOR AS IDFORMAPGTO,T5.VENDEDOR AS NOMEVENDEDOR,T8.USUARIO FROM LANCFINANCEIRO T1 " +
                                " LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) " +
                                " LEFT JOIN TIPODOCUMENTO T3 ON (T3.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) " +
                                " LEFT JOIN FORMAPAGAMENTO T4 ON (T4.ID_FORMAPGTO=T1.ID_FORMAPGTO) " +
                                " LEFT JOIN MVVENDA T6 ON (T6.ID_VENDA=T1.ID_VENDA)" +
                                " LEFT JOIN VENDEDORES T5 ON (T5.ID_VENDEDOR=T6.ID_VENDEDOR) " +                                                                                                
                                " LEFT JOIN CAIXABALCAO T7 ON (T7.ID_CAIXA=T6.ID_CAIXA) " +
                                " LEFT JOIN USUARIOS T8 ON (T8.ID_USUARIO=T7.ID_USUARIO) " +
                                " WHERE T6.TPVENDA IN('PV','TROCA','VF') AND T6.STATUS = 3 AND T6.ID_CAIXA > 0 AND T7.STATUS = 1 AND T3.IDSERVIDOR > 0 AND T4.IDSERVIDOR > 0  AND T7.DATA >= Convert(DateTime, '" + Dt1.Value.Date.ToString() + "', 103) AND T7.DATA <= Convert(DateTime, '" + Dt2.Value.Date.ToString() + "', 103) ";
                    //" WHERE T6.TPVENDA IN ('PV','TROCA','VF') AND T6.STATUS=3 AND T6.ID_CAIXA > 0 AND T7.STATUS=1 AND T3.IDSERVIDOR > 0 AND T4.IDSERVIDOR > 0 AND T2.IDSERVIDOR > 0 AND T7.DATA >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T7.DATA <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";
                    //" WHERE T1.DATALANC >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DATALANC <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103) ";
                    if (!Cb_UpDados.Checked)
                        sSQL = sSQL + " AND (T1.IDLANCSERV=0 OR T1.IDLANCSERV IS NULL)";

                    DataSet TabReg = Controle.ConsultaTabela(sSQL);                    

                    if (TabReg.Tables[0].Rows.Count > 0)
                    {                        
                        DataSet TabEnvio = TabReg.Clone();
                        int IdRet = 0;
                        Controles.EnviarFinanceiro.RegFaturamento EnviarXML = new Controles.EnviarFinanceiro.RegFaturamento();
                        EnviarXML.Url = "http://" + FrmPrincipal.URLMatriz + "/ERP-SGE_WebService/RegFaturamento.asmx?WSDL";
                    
                        ProcBar.Maximum = TabReg.Tables[0].Rows.Count;
                        for (int I = 0; I <= TabReg.Tables[0].Rows.Count - 1; I++)                        
                        {   
                            //XMLFinanc.LoadXml(Tab.GetXml());
                            //Enviando os registros                                    
                            TabEnvio.Tables[0].Rows.Add(TabReg.Tables[0].Rows[I].ItemArray);
                            IdRet = EnviarXML.Enviar(TabEnvio, FrmPrincipal.Perfil_Usuario.Usuario.Trim());
                            if (IdRet > 0)
                            {
                                //XmlNodeReader xmlReader = new XmlNodeReader(XMLRet);
                                //TabRet.ReadXml(xmlReader);
                                Controle.ExecutaSQL("Update LancFinanceiro set IdLancServ=" + IdRet.ToString() + " WHERE ID_LANC=" + TabReg.Tables[0].Rows[I]["Id_Lanc"].ToString().Trim());
                            }
                            TabEnvio.Tables[0].Rows.Clear();
                            ProcBar.Value = ProcBar.Value + 1;
                            Application.DoEvents();
                        }

                        MessageBox.Show("Processo de Envio Concluido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Nenhum Registro foi enviado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnEnviar.Enabled = true;
                    BtnEnviar.Text = "Enviar";
                    Application.DoEvents();                    
                }
            }
            catch
            {
                MessageBox.Show("Erro ao enviar os dados, tente novamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            BtnEnviar.Enabled = true;
            BtnEnviar.Text = "Enviar";
            Application.DoEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
