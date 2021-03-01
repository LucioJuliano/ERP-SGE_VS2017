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

namespace ERP_SGE
{
    public partial class FrmUltCompraCliente : Form
    {
        public string CnpjCpf;
        Funcoes Controle = new Funcoes();        
        public TelaPrincipal FrmPrincipal;

        public FrmUltCompraCliente()
        {
            InitializeComponent();
        }

        private void FrmUltCompraCliente_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            MostraUltimaCompra();
        }

        private void MostraUltimaCompra()
        {
            
            LblVlrUltCompra.Text    = "0,00";
            LblDtUltCompra.Text     = "";
            LblVendUltCompra.Text   = "";
            LblFilialUltCompra.Text = "";

            
            DataSet Venda = new DataSet();
            DataTable Resultado = new DataTable();
            Resultado.Columns.Add("Filial",   Type.GetType("System.String"));
            Resultado.Columns.Add("IdVenda",  Type.GetType("System.Int32"));
            Resultado.Columns.Add("Data",     Type.GetType("System.DateTime"));
            Resultado.Columns.Add("Valor",    Type.GetType("System.Decimal"));
            Resultado.Columns.Add("Vendedor", Type.GetType("System.String"));
            Resultado.DefaultView.Sort = "Data desc";

            Controles.UltimaCompraCliente.UltCompraCliente RegVenda = new Controles.UltimaCompraCliente.UltCompraCliente();
            XmlNode Retorno;
            try
            {
                SqlDataReader LerSQL = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL ORDER BY FILIAL ");
                while (LerSQL.Read())
                {
                    if (LerSQL["ServidorRemoto"].ToString().Trim() != "")
                    {
                        try
                        {
                            if (int.Parse(LerSQL["Id_Filial"].ToString()) == FrmPrincipal.IdFilialConexao)
                                RegVenda.Url = "http://SERVIDOR/ERP-SGE_WebService/UltCompraCliente.asmx?WSDL";
                            else
                                RegVenda.Url = "http://" + LerSQL["ServidorRemoto"].ToString().Trim() + "/ERP-SGE_WebService/UltCompraCliente.asmx?WSDL";
                            Retorno = RegVenda.UltimaCompra(CnpjCpf);

                            if (Retorno != null)
                            {
                                Venda = new DataSet();
                                XmlNodeReader XmlVenda = new XmlNodeReader(Retorno);
                                Venda.ReadXml(XmlVenda);

                                for (int I = 0; I <= Venda.Tables[0].Rows.Count - 1; I++)
                                    Resultado.Rows.Add(LerSQL["Fantasia"].ToString().Trim(), int.Parse(Venda.Tables[0].Rows[I]["Id_Venda"].ToString()), DateTime.Parse(Venda.Tables[0].Rows[I]["Data"].ToString()), decimal.Parse(Venda.Tables[0].Rows[I]["VlrTotal"].ToString().Replace(".", ",")), Venda.Tables[0].Rows[I]["Vendedor"].ToString());
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                
                if (Resultado.Rows.Count > 0)
                {
                    DataSet Tabela = new DataSet();
                    DataView TabSort = Resultado.DefaultView;
                    TabSort.Sort = "Data Desc";                    
                    Tabela.Tables.Add(TabSort.ToTable());

                    LblFilialUltCompra.Text = Tabela.Tables[0].Rows[0]["Filial"].ToString();
                    LblVendUltCompra.Text = Tabela.Tables[0].Rows[0]["Vendedor"].ToString();
                    LblDtUltCompra.Text = Tabela.Tables[0].Rows[0]["Data"].ToString();
                    LblVlrUltCompra.Text = string.Format("{0:N2}", decimal.Parse(Tabela.Tables[0].Rows[0]["Valor"].ToString()));
                    
                }
            }
            catch
            {

            }
        }

    }
}
