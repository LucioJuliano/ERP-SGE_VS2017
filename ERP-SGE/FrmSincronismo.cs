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
    public partial class FrmSincronismo : Form
    {
        public TelaPrincipal FrmPrincipal;
        Funcoes Controle = new Funcoes();

        public FrmSincronismo()
        {
            InitializeComponent();
        }

        private void FrmSincronismo_Load(object sender, EventArgs e)
        {
            Controle.Conexao = FrmPrincipal.Conexao;
            Dt1.Value = DateTime.Now;
            Dt2.Value = DateTime.Now;
            LstFilial = FrmPrincipal.PopularCombo("SELECT Id_Filial,Substring(FANTASIA,1,40) as Filial FROM Empresa_Filial WHERE SERVIDORREMOTO<>'' AND ID_FILIAL<>" + FrmPrincipal.IdFilialConexao.ToString() + "  ORDER BY Filial", LstFilial, "Selecionar");
            LstFilial.SelectedValue = "0";
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (int.Parse(LstFilial.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Atenção: Selecione o Local Origem", "Selecionar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Confirma o Sincronismo das Notas ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlConnection ServidorOrigem;
                Filiais FilialOrigem = new Filiais();
                FilialOrigem.Controle = Controle;
                FilialOrigem.LerDados(int.Parse(LstFilial.SelectedValue.ToString()));

                try
                {                    
                    if (FrmPrincipal.VersaoDistribuidor)
                        ServidorOrigem = new SqlConnection("Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta + "; Initial Catalog=BD_ERP_SGE; User ID=Distribuidor; Password=systalimpo; MultipleActiveResultSets=True;");
                    else
                        ServidorOrigem = new SqlConnection("Data Source=" + FilialOrigem.ServidorRemoto + FilialOrigem.Porta +"; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;");
                    ServidorOrigem.Open();
                }
                catch
                {
                    MessageBox.Show("Atenção: Ocorreu um erro ao conectar ao servidor origem, tente novamente", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnEnviar.Enabled = true;
                    return;
                }

                Funcoes ControleOrigem = new Funcoes();
                ControleOrigem.Conexao = ServidorOrigem;

                Controles.Verificar VerifCad = new Verificar();
                VerifCad.Controle=Controle;
                //
                BtnEnviar.Enabled = false;
                ProcBar.Value = 0;
                Application.DoEvents();

                NotaFiscal Nota = new NotaFiscal();
                Nota.Controle = Controle;
                NotaFiscalItens ItensNota = new NotaFiscalItens();
                ItensNota.Controle = Controle;
                
                try
                {
                    DataSet ConsNota = new DataSet();
                    ConsNota = ControleOrigem.ConsultaTabela("SELECT T2.CNPJ,T1.* FROM NOTAFISCAL T1 LEFT JOIN PESSOAS T2 ON (T2.ID_PESSOA=T1.ID_PESSOA) WHERE T1.NFE=1 AND T1.Status<>0 AND T1.DTEMISSAO >= Convert(DateTime,'" + Dt1.Value.Date.ToString() + "',103) AND T1.DTEMISSAO <= Convert(DateTime,'" + Dt2.Value.Date.ToString() + "',103)");
                    int IdPessoa = 0;
                    int IdNota   = 0;
                                        
                    Produtos CadPrd = new Produtos();
                    CadPrd.Controle = Controle;
                    DataSet ConsItens = new DataSet();

                    if (ConsNota.Tables[0].Rows.Count > 0)
                    {
                        ProcBar.Maximum = ConsNota.Tables[0].Rows.Count;
                        ProcBar.Value = 0;
                        for (int I = 0; I <= ConsNota.Tables[0].Rows.Count - 1; I++)
                        {
                            IdPessoa = VerifCad.Verificar_ExisteCadastro("ID_PESSOA", "SELECT ID_PESSOA FROM Pessoas WHERE Cnpj='" + ConsNota.Tables[0].Rows[I]["CNPJ"].ToString().Trim() + "'");

                            if (IdPessoa == 0)
                            {
                                MessageBox.Show("Atenção: Destinatario da Nota:" + ConsNota.Tables[0].Rows[I]["NumNota"].ToString().Trim() + " de CNPJ/CPF:" + ConsNota.Tables[0].Rows[I]["CNPJ"].ToString().Trim() + " não localizada no Servidor, Favor verificar a nota no servidor Origem ", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ProcBar.Value = ProcBar.Value + 1;
                                continue;
                            }

                            IdNota = VerifCad.Verificar_ExisteCadastro("ID_NOTA", "SELECT ID_NOTA FROM NOTAFISCAL WHERE NUMNOTA=" + ConsNota.Tables[0].Rows[I]["NUMNOTA"].ToString() + " AND ID_FILIAL=" + ConsNota.Tables[0].Rows[I]["Id_Filial"].ToString() + " AND NFE=" + ConsNota.Tables[0].Rows[I]["NFE"].ToString());
                                                        
                            Nota.LerDados(IdNota);
                                                                             
                            if (Nota.Status == 0)
                            {
                                Nota.Data             = DateTime.Parse(ConsNota.Tables[0].Rows[I]["DATA"].ToString());
                                Nota.IdFilial         = int.Parse(ConsNota.Tables[0].Rows[I]["Id_Filial"].ToString());
                                Nota.IdPessoa         = IdPessoa;
                                Nota.IdCfop           = int.Parse(ConsNota.Tables[0].Rows[I]["Id_Cfop"].ToString());
                                Nota.IdTransportadora = int.Parse(ConsNota.Tables[0].Rows[I]["Id_Transportadora"].ToString());
                                Nota.Frete            = int.Parse(ConsNota.Tables[0].Rows[I]["Frete"].ToString());
                                Nota.EntSaida         = int.Parse(ConsNota.Tables[0].Rows[I]["EntSaida"].ToString());
                                Nota.DtEmissao        = DateTime.Parse(ConsNota.Tables[0].Rows[I]["DTEMISSAO"].ToString());
                                Nota.NumFormulario    = int.Parse(ConsNota.Tables[0].Rows[I]["NumFormulario"].ToString());
                                Nota.NumNota          = int.Parse(ConsNota.Tables[0].Rows[I]["NumNota"].ToString());
                                Nota.ProtocoloNfe     = ConsNota.Tables[0].Rows[I]["ProtocoloNFE"].ToString().Trim();
                                Nota.ChaveNfe         = ConsNota.Tables[0].Rows[I]["ChaveNFE"].ToString().Trim();
                                Nota.ReciboNfe        = ConsNota.Tables[0].Rows[I]["ReciboNfe"].ToString().Trim();
                                Nota.VlrProdutos      = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrProdutos"].ToString());
                                Nota.VlrNota          = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrNota"].ToString());
                                Nota.VlrDesconto      = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrDesconto"].ToString());
                                Nota.BIcms            = decimal.Parse(ConsNota.Tables[0].Rows[I]["BIcms"].ToString());
                                Nota.VlrIcms          = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrIcms"].ToString());
                                Nota.BIcmsSub         = decimal.Parse(ConsNota.Tables[0].Rows[I]["BIcmsSub"].ToString());
                                Nota.VlrIcmsSub       = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrIcmsSub"].ToString());
                                Nota.VlrFrete         = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrFrete"].ToString());
                                Nota.VlrSeguro        = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrSeguro"].ToString());
                                Nota.VlrOutraDesp     = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrOutraDesp"].ToString());
                                Nota.VlrIpi           = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrIpi"].ToString());
                                Nota.Observacao       = ConsNota.Tables[0].Rows[I]["Observacao"].ToString().Trim();
                                Nota.Status           = int.Parse(ConsNota.Tables[0].Rows[I]["Status"].ToString());
                                Nota.NFE              = int.Parse(ConsNota.Tables[0].Rows[I]["NFE"].ToString());
                                Nota.QtdeVolume       = int.Parse(ConsNota.Tables[0].Rows[I]["QtdeVolume"].ToString());
                                Nota.Especie          = ConsNota.Tables[0].Rows[I]["Especie"].ToString();
                                Nota.Marca            = ConsNota.Tables[0].Rows[I]["Marca"].ToString();
                                Nota.PesoBruto        = decimal.Parse(ConsNota.Tables[0].Rows[I]["PesoBruto"].ToString());
                                Nota.PesoLiquido      = decimal.Parse(ConsNota.Tables[0].Rows[I]["PesoLiquido"].ToString());
                                Nota.NmPessoa         = ConsNota.Tables[0].Rows[I]["RazaoSocial"].ToString();
                                Nota.CnpjCpf          = ConsNota.Tables[0].Rows[I]["CnpjCpf"].ToString();
                                Nota.InscUf           = ConsNota.Tables[0].Rows[I]["Insc_UF"].ToString();
                                Nota.Cep              = ConsNota.Tables[0].Rows[I]["Cep"].ToString();
                                Nota.Endereco         = ConsNota.Tables[0].Rows[I]["Endereco"].ToString();
                                Nota.Numero           = ConsNota.Tables[0].Rows[I]["Numero"].ToString();
                                Nota.Bairro           = ConsNota.Tables[0].Rows[I]["Bairro"].ToString();
                                Nota.Compl            = ConsNota.Tables[0].Rows[I]["Complemento"].ToString();
                                Nota.Cidade           = ConsNota.Tables[0].Rows[I]["Cidade"].ToString();
                                Nota.IdUf             = int.Parse(ConsNota.Tables[0].Rows[I]["ID_Uf"].ToString());
                                Nota.Telefone         = ConsNota.Tables[0].Rows[I]["Telefone"].ToString();
                                Nota.VlrAcrescimo     = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrAcrescimo"].ToString());
                                Nota.ProtocoloNfe     = ConsNota.Tables[0].Rows[I]["ProtocoloNFE"].ToString();
                                Nota.ChaveNfe         = ConsNota.Tables[0].Rows[I]["ChaveNfe"].ToString();
                                Nota.ReciboNfe        = ConsNota.Tables[0].Rows[I]["ReciboNfe"].ToString();
                                Nota.VlrAcrescimo     = decimal.Parse(ConsNota.Tables[0].Rows[I]["VlrAcrescimo"].ToString());
                                Nota.Consumidor       = int.Parse(ConsNota.Tables[0].Rows[I]["Consumidor"].ToString());
                                Nota.Atendimento      = int.Parse(ConsNota.Tables[0].Rows[I]["Atendimento"].ToString());
                                Nota.DestOperacao     = int.Parse(ConsNota.Tables[0].Rows[I]["DestOperacao"].ToString());
                                Nota.Finalidade       = int.Parse(ConsNota.Tables[0].Rows[I]["Finalidade"].ToString());
                                Nota.ChaveNfeDev      = ConsNota.Tables[0].Rows[I]["ChaveNfeDev"].ToString();
                                Nota.NatOp            = int.Parse(ConsNota.Tables[0].Rows[I]["NatOp"].ToString());
                                Nota.GravarDados();
                                
                                Controle.ExecutaSQL("UPDATE NotaFiscal Set Protocolonfe='" + ConsNota.Tables[0].Rows[I]["ProtocoloNFE"].ToString() + "',ChaveNfe='" + ConsNota.Tables[0].Rows[I]["ChaveNfe"].ToString() + "',ReciboNfe='" + ConsNota.Tables[0].Rows[I]["ReciboNfe"].ToString() + "' Where Id_Nota=" + Nota.IdNota.ToString());

                                if (int.Parse(ConsNota.Tables[0].Rows[I]["Status"].ToString()) == 2)
                                    Controle.ExecutaSQL("UPDATE NotaFiscal Set Status=2,DataCancel=convert(DateTime,'" + ConsNota.Tables[0].Rows[I]["DATACANCEL"].ToString() + "',103),ProtocoloCanc='" + ConsNota.Tables[0].Rows[I]["ProtocoloCanc"].ToString() + "' Where Id_Nota=" + Nota.IdNota.ToString());
                                

                                // Incluindo os Itens
                                ConsItens = ControleOrigem.ConsultaTabela("SELECT T2.REFERENCIA,T1.* FROM NOTAFISCALITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_NOTA=" + ConsNota.Tables[0].Rows[I]["ID_Nota"].ToString());
                                if (ConsItens.Tables[0].Rows.Count > 0)
                                {
                                    FrmPrincipal.BSta_BarProcesso.Maximum = ConsItens.Tables[0].Rows.Count;
                                    for (int B = 0; B <= ConsItens.Tables[0].Rows.Count - 1; B++)
                                    {
                                        CadPrd.LerDados(ConsItens.Tables[0].Rows[B]["Referencia"].ToString().Trim());
                                        if (CadPrd.IdProduto > 0)
                                        {
                                            ItensNota.LerDados(0);
                                            ItensNota.IdNota        = Nota.IdNota;
                                            ItensNota.IdProduto     = CadPrd.IdProduto;
                                            ItensNota.Qtde          = decimal.Parse(ConsItens.Tables[0].Rows[B]["Qtde"].ToString());
                                            ItensNota.VlrUnitario   = decimal.Parse(ConsItens.Tables[0].Rows[B]["VlrUnitario"].ToString());
                                            ItensNota.VlrTotal      = decimal.Parse(ConsItens.Tables[0].Rows[B]["VlrTotal"].ToString());
                                            ItensNota.PIpi          = decimal.Parse(ConsItens.Tables[0].Rows[B]["PIPI"].ToString());
                                            ItensNota.VlrIpi        = decimal.Parse(ConsItens.Tables[0].Rows[B]["VlrIpi"].ToString());
                                            ItensNota.VlrIcms       = decimal.Parse(ConsItens.Tables[0].Rows[B]["VlrIcms"].ToString());
                                            ItensNota.PIcms         = decimal.Parse(ConsItens.Tables[0].Rows[B]["PIcms"].ToString());
                                            ItensNota.PIcmsSub      = decimal.Parse(ConsItens.Tables[0].Rows[B]["PIcmsSub"].ToString());
                                            ItensNota.VlrIcmsSub    = decimal.Parse(ConsItens.Tables[0].Rows[B]["VlrIcmsSub"].ToString());
                                            ItensNota.PercRed       = decimal.Parse(ConsItens.Tables[0].Rows[B]["PercRed"].ToString());
                                            ItensNota.SitTributaria = int.Parse(ConsItens.Tables[0].Rows[B]["SitTributaria"].ToString());
                                            ItensNota.IdCfop        = int.Parse(ConsItens.Tables[0].Rows[B]["Id_Cfop"].ToString());
                                            ItensNota.IdReducao     = int.Parse(ConsItens.Tables[0].Rows[B]["Id_Reducao"].ToString());
                                            ValidarCST(Nota, ItensNota);
                                            ItensNota.GravarDados();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Atenção: Produto:" + ConsItens.Tables[0].Rows[B]["Referencia"].ToString().Trim() + " não localizada no Servidor, Favor verificar a nota no servidor Origem ", "Conclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Nota.Status = 0;
                                            Nota.GravarDados();
                                            BtnEnviar.Enabled = true;
                                            return;
                                        }                                        
                                        FrmPrincipal.BSta_BarProcesso.Maximum = FrmPrincipal.BSta_BarProcesso.Maximum + 1;
                                    }                                    
                                }
                                Nota.GravarDados();
                            }
                            else
                            {
                                Nota.GravarDados();
                                if (int.Parse(ConsNota.Tables[0].Rows[I]["Status"].ToString()) == 1)
                                    Controle.ExecutaSQL("UPDATE NotaFiscal Set Protocolonfe='" + ConsNota.Tables[0].Rows[I]["ProtocoloNFE"].ToString() + "',ChaveNfe='" + ConsNota.Tables[0].Rows[I]["ChaveNfe"].ToString() + "',ReciboNfe='" + ConsNota.Tables[0].Rows[I]["ReciboNfe"].ToString() + "' Where Id_Nota=" + IdNota.ToString());

                                if (int.Parse(ConsNota.Tables[0].Rows[I]["Status"].ToString()) == 2)
                                    Controle.ExecutaSQL("UPDATE NotaFiscal Set Status=2,DataCancel=convert(DateTime,'" + ConsNota.Tables[0].Rows[I]["DATACANCEL"].ToString() + "',103),ProtocoloCanc='" + ConsNota.Tables[0].Rows[I]["ProtocoloCanc"].ToString() + "' Where Id_Nota=" + IdNota.ToString());                                
                                
                            }
                            ProcBar.Value = ProcBar.Value + 1;
                            Application.DoEvents();                                
                        }                        
                    }
                    MessageBox.Show("Processo concluido", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnEnviar.Enabled = true;
                    return;
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro: " + erro.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BtnEnviar.Enabled = true;
                    return;
                }
            }        
        }
        private void ValidarCST(NotaFiscal CadNFe, NotaFiscalItens Item)
        {
            Filiais CadFilial = new Filiais();
            CadFilial.Controle = Controle;
            CadFilial.LerDados(CadNFe.IdFilial);

            Produtos CadPrd = new Produtos();
            CadPrd.Controle = Controle;
            CadPrd.LerDados(Item.IdProduto);

            if (Item.SitTributaria == 0 && (CadNFe.IdFilial == 1 || CadNFe.IdFilial == 6) && CadNFe.IdUf != CadFilial.Uf && CadNFe.InscUf.Trim() != "")
                Item.Cst = 10;
            else
            {
                if (Item.SitTributaria == 0)
                {
                    Item.Cst = 1;
                    //if (Item.PercRed > 0)
                    //    Item.Cst = 3;
                }
                else if (Item.SitTributaria == 1)
                {
                    Item.Cst = 6;
                }
                else if (Item.SitTributaria == 2)
                    Item.Cst = 5;
                else if (Item.SitTributaria == 3)
                {
                    Item.Cst = 8;
                    //if (Item.PercRed > 0)
                    //    Item.Cst = 9;
                }
            }
        }


    }
}
