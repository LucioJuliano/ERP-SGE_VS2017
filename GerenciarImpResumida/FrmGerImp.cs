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
using System.Collections;
using TServerSocket;
using CDSSoftware;
using System.IO;


namespace GerenciarImpResumida
{
    public partial class FrmGerencial : Form
    {
        Funcoes Controle             = new Funcoes();
        MvVenda Vendas               = new MvVenda();
        public string PortaImpResumida = "";
        public string PortaSocket      = "";
        public bool VersaoDistribuidor = false;
        public string NomeServidor     = "";
        public string StringConexao    = "";
        public string TipoImpressora   = "";
        private ServerSocket RecSocket;
        public SqlConnection Conexao;
        
        public SqlConnection AbrirConexao()
        {
            if (Conexao == null)
            {
                if (VersaoDistribuidor)
                    StringConexao = "Data Source=" + NomeServidor + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;";
                else
                    StringConexao = "Data Source=" + NomeServidor + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                
                Conexao = new SqlConnection(StringConexao);
                Conexao.Open();
                Controle.Conexao = Conexao;                
            }
            return Conexao;
        }        
        
        public FrmGerencial()
        {
            InitializeComponent();
            ArrayList Parametros = new ArrayList();
            StreamReader LerParam = new StreamReader("ERP-SGE.ini");
            while (!LerParam.EndOfStream)
                Parametros.Add(LerParam.ReadLine());
            PortaImpResumida   = Parametros[1].ToString();
            TipoImpressora     = Parametros[2].ToString();
            VersaoDistribuidor = Parametros[3].ToString() != "Versao=P";
            PortaSocket        = Parametros[5].ToString();
            NomeServidor       = Parametros[6].ToString();
            AbrirConexao();
        }

        private void FrmGerencial_Load(object sender, EventArgs e)
        {
            Vendas.Controle = Controle;
            RecSocket = new ServerSocket(int.Parse(PortaSocket));
            RecSocket.Active();
            Tempo.Enabled = true;
        }

        private void FrmGerencial_Shown(object sender, EventArgs e)
        {
            this.Hide();
            Icone.Visible = true;
        }

        private void Imprimir(string IdVd)
        {
            try
            {
                Vendas.LerDados(int.Parse(IdVd));
                string FormaPgto = "";
                if (Vendas.TpVenda == "PV" || Vendas.TpVenda == "VF")
                {
                    DataSet Parcelas = new DataSet();
                    Parcelas = Controle.ConsultaTabela("SELECT T1.VENCIMENTO,T1.VLRORIGINAL,T2.DOCUMENTO FROM LancFinanceiro T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString());

                    for (int I = 0; I <= Parcelas.Tables[0].Rows.Count - 1; I++)
                    {
                        DateTime Dt = DateTime.Parse(Parcelas.Tables[0].Rows[I]["Vencimento"].ToString());
                        FormaPgto = FormaPgto + Dt.Date.ToShortDateString() + "   R$" + string.Format("{0:N2}", decimal.Parse(Parcelas.Tables[0].Rows[I]["VlrOriginal"].ToString())) + "   " + Parcelas.Tables[0].Rows[I]["Documento"].ToString();
                    }
                }
                Filiais CadFilial = new Filiais();
                CadFilial.Controle = Controle;
                CadFilial.LerDados(Vendas.IdFilial);

                DataSet TabItens = new DataSet();
                DataSet TabKit = new DataSet();
                TabItens = Controle.ConsultaTabela(Vendas.SqlRelatorio(Vendas.IdVenda));

                bool ImpCab = true;
                ImprimeTexto ImpTxt = new ImprimeTexto();                
                ImpTxt.Inicio(PortaImpResumida);                                                
                
                string TipoItem = "";
                string Qtde = "";

                int Lin = 0;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (ImpCab)
                    {
                        // ImpTxt.ImpLF(ImpTxt.Normal + ImpTxt.NegritoOff + Controle.Space(CadFilial.Filial.Trim(), 40) + "   CNPJ:" + CadFilial.Cnpj.Trim());
                        // ImpTxt.ImpLF(ImpTxt.Comprimido + ImpTxt.NegritoOff + Controle.Space(CadFilial.Endereco.Trim() + "," + CadFilial.Numero, 50) + "   Fone:" + Controle.Space(CadFilial.Fone1.Trim(), 10));
                        // ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                        ImpTxt.ImpLF(ImpTxt.Normal + ImpTxt.NegritoOff + "Data: " + DateTime.Parse(TabItens.Tables[0].Rows[I]["Data"].ToString()).ToShortDateString() + " Doc.VD: " + TabItens.Tables[0].Rows[I]["NumDocumento"].ToString().Trim() + "/" + string.Format("{0:D6}", int.Parse(TabItens.Tables[0].Rows[I]["Id_Venda"].ToString())) + "   " + TabItens.Tables[0].Rows[I]["Movimento"].ToString().Trim() + "   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Cliente.: " + TabItens.Tables[0].Rows[I]["Id_Pessoa"].ToString().Trim() + " - " + Controle.Space(TabItens.Tables[0].Rows[I]["Fantasia"].ToString().Trim(), 70) + " / " + Controle.Space(TabItens.Tables[0].Rows[I]["Pessoa"].ToString().Trim(), 60));
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Endereco: " + Controle.Space(TabItens.Tables[0].Rows[I]["Endereco"].ToString().Trim() + " No.: " + TabItens.Tables[0].Rows[I]["Numero"].ToString(), 100));
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "CEP.....: " + TabItens.Tables[0].Rows[I]["CEP"].ToString().Trim() + "    Bairro:" + TabItens.Tables[0].Rows[I]["Bairro"].ToString() + " CIDADE: " + Controle.Space(TabItens.Tables[0].Rows[I]["CIDADE"].ToString(), 30) + " UF:" + TabItens.Tables[0].Rows[I]["UF"].ToString());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "CNPJ/CPF: " + TabItens.Tables[0].Rows[I]["CNPJCPF"].ToString() + " Insc.Estadual:" + TabItens.Tables[0].Rows[I]["InscUF"].ToString());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Cod.   Referencia  Produto                                                                    Qtde. Und.         Vlr.Unit.   Vlr.Total");
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                        ImpCab = false;
                    }
                    if (TipoItem != TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim())
                    {
                        if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "S")
                        {
                            ImpTxt.ImpLF("*** Saida ***");
                            Lin = Lin + 1;
                        }
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "E")
                        {
                            ImpTxt.ImpLF("*** DEVOLUÇÃO ***");
                            Lin = Lin + 1;
                        }
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "N")
                        {
                            ImpTxt.ImpLF("*** SEM MOVIMENTO ***");
                            Lin = Lin + 1;
                        }
                    }

                    if (int.Parse(TabItens.Tables[0].Rows[I]["ProdutoKit"].ToString()) == 1)
                        Qtde = "        ";
                    else
                        Qtde = Controle.NumSpace(string.Format("{0:N3}", decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString())).ToString(), 8);

                    string Descricao = TabItens.Tables[0].Rows[I]["Descricao"].ToString().Trim().Replace("ç", "c").Replace("Ç", "C").Replace("á", "a").Replace("Á", "A").Replace("ã", "a").Replace("Â", "A").Replace("õ", "o").Replace("Õ", "O").Replace("é", "e").Replace("É", "E");
                    ImpTxt.ImpLF(string.Format("{0:D6}", int.Parse(TabItens.Tables[0].Rows[I]["Id_Produto"].ToString())) + "  " + Controle.Space(TabItens.Tables[0].Rows[I]["Referencia"].ToString(), 10) + " " + Controle.Space(Descricao, 70) + "  " +
                                 Qtde + "  " + Controle.Space(TabItens.Tables[0].Rows[I]["Unidade"].ToString(), 5) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString())).ToString(), 12) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["TotalItem"].ToString())).ToString(), 12));
                    Lin = Lin + 1;

                    if (int.Parse(TabItens.Tables[0].Rows[I]["ProdutoKit"].ToString()) == 1)
                    {
                        TabKit = Controle.ConsultaTabela("select t1.Id_PrdMaster,t2.Referencia,t2.Descricao,t1.Qtde,t2.Unidade from ProdutosKit t1" +
                                                         " left join Produtos t2 on (t2.Id_Produto=t1.Id_Produto)  where t1.Id_PrdMaster=" + TabItens.Tables[0].Rows[I]["Id_Produto"].ToString());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "     ----------------------------------------------------------------------------------------------------------------------------");
                        for (int k = 0; k <= TabKit.Tables[0].Rows.Count - 1; k++)
                        {
                            Descricao = TabKit.Tables[0].Rows[k]["Descricao"].ToString().Trim().Replace("ç", "c").Replace("Ç", "C").Replace("á", "a").Replace("Á", "A").Replace("ã", "a").Replace("Â", "A").Replace("õ", "o").Replace("Õ", "O").Replace("é", "e").Replace("É", "E");
                            ImpTxt.ImpLF("     " + Controle.Space(TabKit.Tables[0].Rows[k]["Referencia"].ToString(), 10) + " " + Controle.Space(Descricao, 70) + "  " + Controle.NumSpace(string.Format("{0:N3}", decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString()) * decimal.Parse(TabKit.Tables[0].Rows[k]["Qtde"].ToString())).ToString(), 8) + "  " + Controle.Space(TabKit.Tables[0].Rows[k]["Unidade"].ToString(), 5));
                            Lin = Lin + 1;
                            if (Lin > 14)
                            {
                                ImpTxt.ImpLF("");
                                Lin = 0;
                                for (int L = 1; L <= 12; L++)
                                    ImpTxt.ImpLF("");
                            }

                        }

                    }

                    TipoItem = TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim();

                    if (Lin > 14)
                    {
                        ImpCab = true;
                        Lin = 0;
                        for (int L = 1; L <= 12; L++)
                            ImpTxt.ImpLF("");
                    }
                }
                for (int I = Lin; I <= 14; I++)
                {
                    ImpTxt.ImpLF("");
                }
                ImpTxt.ImpLF(ImpTxt.Comprimido + "--------------------------------------------------------------------------------------------------------------------------------------");
                ImpTxt.ImpLF("Vendedor: " + Controle.Space(TabItens.Tables[0].Rows[0]["Vendedor"].ToString().Trim(), 20) + " Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 20) + "   " + Controle.Space(FormaPgto, 38) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 12));
                //ImpTxt.ImpLF("Vendedor: " + Controle.Space(TabItens.Tables[0].Rows[0]["Vendedor"].ToString().Trim(), 20) + " Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 20) + "   " + Controle.Space(TabItens.Tables[0].Rows[0]["PrazoPgto"].ToString().Trim(), 20) + Controle.Space(" ", 18) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 12));
                ImpTxt.ImpLF(Controle.Space("Obs: " + Controle.Space(TabItens.Tables[0].Rows[0]["Observacao"].ToString().Trim(), 95), 104) + " (-) Desconto  R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrDesconto"].ToString())).ToString(), 12));
                ImpTxt.ImpLF(Controle.Space(" ", 104) + "                  -------------");
                ImpTxt.ImpLF(Controle.Space(" ", 104) + " (=) Total     R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrTotal"].ToString())).ToString(), 12));
                ImpTxt.ImpLF(" ");
                ImpTxt.ImpLF("Recebido em:______/_____/________                                                  _______________________");
                ImpTxt.ImpLF("                                                                                          Comprador");
                ImpTxt.ImpLF(ImpTxt.Comprimido + "Documento sem valor Fiscal");
                ImpTxt.Fim();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao Imprimir:" + erro.ToString());
            }
            
            if (Vendas.Status == 2 && Vendas.VdImpFat == 0)
            {
                Vendas.VdImpFat = 1;
                Vendas.GravarDados();
            }
        }

        private void MiniImprimir(string IdVd)
        {
            try
            {                
                Vendas.LerDados(int.Parse(IdVd));
                string FormaPgto = "";
                if (Vendas.TpVenda == "PV" || Vendas.TpVenda == "VF")
                {
                    DataSet Parcelas = new DataSet();
                    Parcelas = Controle.ConsultaTabela("SELECT T1.VENCIMENTO,T1.VLRORIGINAL,T2.DOCUMENTO FROM LancFinanceiro T1 LEFT JOIN TIPODOCUMENTO T2 ON (T2.ID_DOCUMENTO=T1.ID_TIPODOCUMENTO) WHERE T1.Id_Venda=" + Vendas.IdVenda.ToString());

                    for (int I = 0; I <= Parcelas.Tables[0].Rows.Count - 1; I++)
                    {
                        DateTime Dt = DateTime.Parse(Parcelas.Tables[0].Rows[I]["Vencimento"].ToString());
                        FormaPgto = FormaPgto + Dt.Date.ToShortDateString() + "   R$" + string.Format("{0:N2}", decimal.Parse(Parcelas.Tables[0].Rows[I]["VlrOriginal"].ToString())) + "   " + Parcelas.Tables[0].Rows[I]["Documento"].ToString();
                    }
                }
                Filiais CadFilial = new Filiais();
                CadFilial.Controle = Controle;
                CadFilial.LerDados(Vendas.IdFilial);

                DataSet TabItens = new DataSet();
                TabItens = Controle.ConsultaTabela(Vendas.SqlRelatorio(Vendas.IdVenda));

                bool ImpCab = true;
                ImprimeTexto ImpTxt = new ImprimeTexto();
                ImpTxt.Inicio(PortaImpResumida);                
                string TipoItem = "";

                int Lin = 0;
                for (int I = 0; I <= TabItens.Tables[0].Rows.Count - 1; I++)
                {
                    if (ImpCab)
                    {
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Data:" + DateTime.Parse(TabItens.Tables[0].Rows[I]["Data"].ToString()).ToShortDateString() + " Doc.: " + TabItens.Tables[0].Rows[I]["NumDocumento"].ToString().Trim() + "/" + string.Format("{0:D6}", int.Parse(TabItens.Tables[0].Rows[I]["Id_Venda"].ToString())) + "    " + TabItens.Tables[0].Rows[I]["Movimento"].ToString().Trim());
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Cliente.: " + Controle.Space(TabItens.Tables[0].Rows[I]["Id_Pessoa"].ToString().Trim() + "-" + TabItens.Tables[0].Rows[I]["Pessoa"].ToString().Trim(), 50) );
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Endereco: " + Controle.Space(TabItens.Tables[0].Rows[I]["Endereco"].ToString().Trim() + " No.: " + TabItens.Tables[0].Rows[I]["Numero"].ToString(), 50));
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "CNPJ/CPF: " + TabItens.Tables[0].Rows[I]["CNPJCPF"].ToString() + " CGF:" + TabItens.Tables[0].Rows[I]["InscUF"].ToString());                        
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "Ref.  Produto                          Qtde.   Vr.Unit. Vr.Total");
                        ImpTxt.ImpLF(ImpTxt.Comprimido + "----------------------------------------------------------------");

                        ImpCab = false;                        
                    }
                    if (TipoItem != TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim())
                    {
                        if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "S")
                        {
                            ImpTxt.ImpLF("*** Saida ***");
                            Lin = Lin + 1;
                        }
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "E")
                        {
                            ImpTxt.ImpLF("*** DEVOLUÇÃO ***");
                            Lin = Lin + 1;
                        }
                        else if (TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim() == "N")
                        {
                            ImpTxt.ImpLF("*** SEM MOVIMENTO ***");
                            Lin = Lin + 1;
                        }
                    }
                    
                    string Descricao = TabItens.Tables[0].Rows[I]["Descricao"].ToString().Trim().Replace("ç", "c").Replace("Ç", "C").Replace("á", "a").Replace("Á", "A").Replace("ã", "a").Replace("Â", "A").Replace("õ", "o").Replace("Õ", "O").Replace("é", "e").Replace("É", "E");

                    ImpTxt.ImpLF(ImpTxt.Comprimido + Controle.Space(TabItens.Tables[0].Rows[I]["Referencia"].ToString(), 8) + " " + Controle.Space(Descricao, 25) + "    " +
                                 Controle.NumSpace(string.Format("{0:N3}", decimal.Parse(TabItens.Tables[0].Rows[I]["Qtde"].ToString())).ToString(), 6) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["VlrUnitario"].ToString())).ToString(), 8) + "  " + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[I]["TotalItem"].ToString())).ToString(), 8));

                    TipoItem = TabItens.Tables[0].Rows[I]["TipoItem"].ToString().Trim();
                    
                }                
                ImpTxt.ImpLF(ImpTxt.Comprimido + "----------------------------------------------------------------");
                ImpTxt.ImpLF(ImpTxt.Comprimido + "Forma Pgto: " + Controle.Space(TabItens.Tables[0].Rows[0]["FormaPgto"].ToString().Trim(), 24) + " (+) Sub Total R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrSubTotal"].ToString())).ToString(), 10));
                ImpTxt.ImpLF(ImpTxt.Comprimido + "                                     (-) Desconto  R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrDesconto"].ToString())).ToString(), 10));
                ImpTxt.ImpLF(ImpTxt.Comprimido + "                                                     -----------");                
                ImpTxt.ImpLF(ImpTxt.Comprimido + "                                     (=) Total     R$:" + Controle.NumSpace(string.Format("{0:N2}", decimal.Parse(TabItens.Tables[0].Rows[0]["VlrTotal"].ToString())).ToString(), 10));
                ImpTxt.ImpLF(ImpTxt.Comprimido + "----------------------------------------------------------------");
                ImpTxt.ImpLF(ImpTxt.Normal + ImpTxt.NegritoOn + "Documento sem valor Fiscal");                
                ImpTxt.ImpLF("  ");
                ImpTxt.ImpLF("  ");
                ImpTxt.ImpLF("  ");
                ImpTxt.ImpLF("  ");
                ImpTxt.ImpLF("  ");
                ImpTxt.ImpLF("  ");
                ImpTxt.ImpLF("  ");
                ImpTxt.ImpLF("  ");                
                ImpTxt.Fim();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao Imprimir:" + erro.ToString());
            } 

            if (Vendas.Status == 2 && Vendas.VdImpFat == 0)
            {
                Vendas.VdImpFat = 1;
                Vendas.GravarDados();
            }
        }

        private void Tempo_Tick(object sender, EventArgs e)
        {
            string msg = RecSocket.ReceivedText;
            if (msg != "")
            {
                Icone.Text = "Impressão Resumida: " + msg.Replace("\0", "");
                if (TipoImpressora == "MINIMATRICIAL")
                    MiniImprimir(msg.Replace("\0", ""));
                else
                    Imprimir(msg.Replace("\0", ""));
            }
        }
    }
}
