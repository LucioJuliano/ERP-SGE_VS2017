using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controle_Dados;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

namespace Controles
{
    [Serializable()]
    public class ControleEstoque
    {
        public Funcoes Controle = new Funcoes();

        public bool MovimentoEstoque(SqlDataReader Tab,int TpMov,int Opcao,bool UpdCusto,string TabAuxMov,DateTime DtMovim, int IdFilial)
        {
            //TpMov 1-Entrada 2-Saida
            //Opcoes 1-Normal 2-Cancela         
            SqlDataReader TabKit;
            try
            {
                while (Tab.Read())
                {
                    //TabConsulta = Controle.ConsultaSQL("SELECT * FROM EXTRATOESTOQUE WHERE TPMOV='" + TabAuxMov + "' AND ID_ITEM=" + Tab["Id_Item"].ToString());
                    //if (!TabConsulta.HasRows)
                    // {
                    Mov_ExtratoEstoque(TabAuxMov, int.Parse(Tab["Id_Item"].ToString()));
                    if (TpMov == 1)
                    {
                        if (Opcao == 1)
                        {
                            if (UpdCusto)
                            {
                                if (Tab["NCM"].ToString().Trim() != "")
                                    Controle.ExecutaSQL("Update Produtos set NCM='" + Tab["NCM"].ToString().Trim() + "' WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString());


                                if (Tab["CodBarra"].ToString().Trim() != "")
                                    Controle.ExecutaSQL("Update Produtos set CodBarra='" + Tab["CodBarra"].ToString().Trim() + "' WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString());

                                Controle.ExecutaSQL("Update Produtos set Custo=(Custo+" + Controle.FloatToStr(decimal.Parse(Tab["VlrUnitario"].ToString())) + ")/2, UltPrcCompra=" + Controle.FloatToStr(decimal.Parse(Tab["VlrPrcCompra"].ToString())) + ",DtAlteracao=convert(datetime,Convert(char,GetDate(),103),103)  WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString());

                            }
                            Mov_ExtratoEstoque(TabAuxMov, int.Parse(Tab["Id_Item"].ToString()), int.Parse(Tab["Id_Produto"].ToString()), decimal.Parse(Tab["Qtde"].ToString()), decimal.Parse(Tab["VlrUnitario"].ToString()), DtMovim);
                            Atlz_SaldoEstoque("E", "I", int.Parse(Tab["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString()));

                            //Verificando se Produto tem movimento de Kit
                            TabKit = Controle.ConsultaSQL("SELECT T1.* FROM PRODUTOSKIT T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRDMASTER) WHERE T2.PRODUTOKIT=1 AND T1.ID_PRDMASTER=" + Tab["Id_Produto"].ToString());
                            while (TabKit.Read())
                            {
                                Mov_ExtratoEstoque(TabAuxMov, int.Parse(Tab["Id_Item"].ToString()), int.Parse(TabKit["Id_Produto"].ToString()), decimal.Parse(Tab["Qtde"].ToString()) * decimal.Parse(TabKit["Qtde"].ToString()), decimal.Parse(Tab["VlrUnitario"].ToString()), DtMovim);
                                Atlz_SaldoEstoque("E", "I", int.Parse(TabKit["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString()) * decimal.Parse(TabKit["Qtde"].ToString()));
                            }
                        }
                        else
                        {
                            Atlz_SaldoEstoque("E", "C", int.Parse(Tab["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString()));
                            //Verificando se Produto tem movimento de Kit
                            TabKit = Controle.ConsultaSQL("SELECT T1.* FROM PRODUTOSKIT T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRDMASTER) WHERE T2.PRODUTOKIT=1 AND T1.ID_PRDMASTER=" + Tab["Id_Produto"].ToString());
                            while (TabKit.Read())
                                Atlz_SaldoEstoque("E", "C", int.Parse(TabKit["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString())*decimal.Parse(TabKit["Qtde"].ToString()));
                            
                        }
                    }
                    else if (TpMov == 2)
                    {
                        if (Opcao == 1)
                        {                            
                            Mov_ExtratoEstoque(TabAuxMov, int.Parse(Tab["Id_Item"].ToString()), int.Parse(Tab["Id_Produto"].ToString()), decimal.Parse(Tab["Qtde"].ToString()), decimal.Parse(Tab["VlrUnitario"].ToString()), DtMovim);
                            Atlz_SaldoEstoque("S", "I", int.Parse(Tab["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString()));
                            //Verificando se Produto tem movimento de Kit
                            TabKit = Controle.ConsultaSQL("SELECT T1.* FROM PRODUTOSKIT T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRDMASTER) WHERE T2.PRODUTOKIT=1 AND T1.ID_PRDMASTER=" + Tab["Id_Produto"].ToString());
                            while (TabKit.Read())
                            {
                                Mov_ExtratoEstoque(TabAuxMov, int.Parse(Tab["Id_Item"].ToString()), int.Parse(TabKit["Id_Produto"].ToString()), decimal.Parse(Tab["Qtde"].ToString()) * decimal.Parse(TabKit["Qtde"].ToString()), decimal.Parse(Tab["VlrUnitario"].ToString()), DtMovim);
                                Atlz_SaldoEstoque("S", "I", int.Parse(TabKit["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString()) * decimal.Parse(TabKit["Qtde"].ToString()));                            
                            }
                            //Atualizando a Quantidade de Venda da Promoção
                            if (Tab["Id_Promocao"].ToString() != "")
                                Controle.ExecutaSQL("UPDATE PROMOCOES SET QTDEVENDA=QTDEVENDA + " + Controle.FloatToStr(decimal.Parse(Tab["Qtde"].ToString())) + " WHERE ID_LANC=" + Tab["Id_Promocao"].ToString());                                                        
                        }
                        else
                        {
                            
                            Atlz_SaldoEstoque("S", "C", int.Parse(Tab["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString()));
                            //Verificando se Produto tem movimento de Kit
                            TabKit = Controle.ConsultaSQL("SELECT T1.* FROM PRODUTOSKIT T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRDMASTER) WHERE T2.PRODUTOKIT=1 AND T1.ID_PRDMASTER=" + Tab["Id_Produto"].ToString());
                            while (TabKit.Read())
                            {                                
                                Atlz_SaldoEstoque("S", "C", int.Parse(TabKit["Id_Produto"].ToString()), DtMovim, decimal.Parse(Tab["Qtde"].ToString()) * decimal.Parse(TabKit["Qtde"].ToString()));
                            }
                            if (Tab["Id_Promocao"].ToString() != "")
                                Controle.ExecutaSQL("UPDATE PROMOCOES SET QTDEVENDA=QTDEVENDA - " + Controle.FloatToStr(decimal.Parse(Tab["Qtde"].ToString())) + " WHERE ID_LANC=" + Tab["Id_Promocao"].ToString());
                        }
                    }
                    //}
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void Mov_ExtratoEstoque(string TpMov, int Item, int IdPrd, decimal Qtde, decimal VlrUnt, DateTime DtMov)
        {
            try
            {
                ArrayList Nm_param = new ArrayList();
                ArrayList Vr_param = new ArrayList();
                string sSQL = "";

                int IdLanc = Controle.ProximoID("ExtratoEstoque");
                sSQL = "INSERT INTO EXTRATOESTOQUE (ID_LANC,TPMOV,ID_ITEM,ID_PRODUTO,QTDE,VLRUNITARIO,DATA,DTMOVIM)" +
                       " VALUES (@ID,@TPMOV,@IDITEM,@IDPRODUTO,@QTDE,@VLRUNITARIO,@DATA,Convert(DateTime,@DTMOVIM,103))";

                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);                
                Nm_param.Add("@TPMOV"); Vr_param.Add(TpMov.Trim());
                Nm_param.Add("@IDITEM"); Vr_param.Add(Item);
                Nm_param.Add("@IDPRODUTO"); Vr_param.Add(IdPrd);
                Nm_param.Add("@QTDE"); Vr_param.Add(Controle.FloatToStr(Qtde,3));
                Nm_param.Add("@VLRUNITARIO"); Vr_param.Add(Controle.FloatToStr(VlrUnt, 2));
                Nm_param.Add("@DATA"); Vr_param.Add(DateTime.Now);
                Nm_param.Add("@DTMOVIM"); Vr_param.Add(DtMov.ToShortDateString());
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);

            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void Mov_ExtratoEstoque(string TpMov, int Item)
        {
            try
            {
                Controle.ExecutaSQL("DELETE FROM EXTRATOESTOQUE WHERE TPMOV='" + TpMov.Trim() + "' AND ID_ITEM=" + Item.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Atlz_SaldoEstoque(string Mov, string Op, int IdPrd, DateTime Data, decimal Qtde) //Mov E-Entrada S-Saida // Op I-Incluir C-Cancelar
        {
            //Atlz_SaldoEstoque](@Mov Char(1),@OpMov Char(1),@IdPrd int, @Data DateTime, @Qtde decimal(18,3))	
            SqlCommand Cmd = new SqlCommand("Atlz_SaldoEstoque", Controle.Conexao);
            Cmd.CommandTimeout = 1200;
            Cmd.Parameters.Add("@Mov", SqlDbType.Char);
            Cmd.Parameters.Add("@OpMov", SqlDbType.Char);
            Cmd.Parameters.Add("@IdPrd", SqlDbType.Int);
            Cmd.Parameters.Add("@Data", SqlDbType.DateTime);
            Cmd.Parameters.Add("@Qtde", SqlDbType.Decimal);
            Cmd.Parameters[0].Value = Mov;
            Cmd.Parameters[1].Value = Op;
            Cmd.Parameters[2].Value = IdPrd;
            Cmd.Parameters[3].Value = Data.ToShortDateString();
            Cmd.Parameters[4].Value = Qtde;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.ExecuteNonQuery();            
        }
        
        /*public void Atlz_SaldoEstoque(string Mov,string Op, int IdPrd,DateTime Data,decimal Qtde) //Mov E-Entrada S-Saida // Op I-Incluir C-Cancelar
        {
            decimal SaldoAnterior = 0;
            // Verificando o Saldo Anterior

            SqlDataReader TabSldAnt;
            TabSldAnt = Controle.ConsultaSQL("SELECT * FROM SALDOESTOQUE WHERE Id_Produto=" + IdPrd.ToString() + " AND DATA < CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103) ORDER BY DATA DESC");
            if (TabSldAnt.HasRows)
            {
                TabSldAnt.Read();
                SaldoAnterior = decimal.Parse(TabSldAnt["Saldo"].ToString());
            }

            SqlDataReader TabSld;
            TabSld = Controle.ConsultaSQL("SELECT * FROM SALDOESTOQUE WHERE Id_Produto=" + IdPrd.ToString() + " AND DATA = CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)");
            if (TabSld.HasRows)
            {
                TabSld.Read();
                if (Mov == "E")
                {
                    if (Op == "I")
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUE SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",ENTRADAS=ENTRADAS+" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO+" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) + Qtde;
                    }
                    else
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUE SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",ENTRADAS=ENTRADAS-" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO-" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) - Qtde;
                    }
                }
                else
                {
                    if (Op == "I")
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUE SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",SAIDAS=SAIDAS+" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO-" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) - Qtde;
                    }
                    else
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUE SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",SAIDAS=SAIDAS-" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO+" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) + Qtde;
                    }
                }
            }
            else
            {
                int IdLanc = Controle.ProximoID("SaldoEstoque");
                if (Mov == "E")
                {
                    if (Op == "I")
                    {
                        Controle.ExecutaSQL("INSERT INTO SALDOESTOQUE (ID_LANC,ID_PRODUTO,DATA,SLDANTERIOR,ENTRADAS,SAIDAS,SALDO) VALUES (" + IdLanc.ToString() + "," + IdPrd.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 3) + "," + Controle.FloatToStr(Qtde, 3) + ",0," + Controle.FloatToStr(SaldoAnterior + Qtde, 3) + ")");
                        SaldoAnterior = SaldoAnterior + Qtde;
                    }
                    else
                    {
                        Controle.ExecutaSQL("INSERT INTO SALDOESTOQUE (ID_LANC,ID_PRODUTO,DATA,SLDANTERIOR,ENTRADAS,SAIDAS,SALDO) VALUES (" + IdLanc.ToString() + "," + IdPrd.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 3) + "," + Controle.FloatToStr(-1 * Qtde, 3) + ",0," + Controle.FloatToStr(SaldoAnterior - Qtde, 3) + ")");
                        SaldoAnterior = SaldoAnterior - Qtde;
                    }
                }
                else
                {
                    if (Op == "I")
                    {
                        Controle.ExecutaSQL("INSERT INTO SALDOESTOQUE (ID_LANC,ID_PRODUTO,DATA,SLDANTERIOR,ENTRADAS,SAIDAS,SALDO) VALUES (" + IdLanc.ToString() + "," + IdPrd.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 3) + ",0," + Controle.FloatToStr(Qtde, 3) + "," + Controle.FloatToStr(SaldoAnterior + (-1 * Qtde), 3) + ")");
                        SaldoAnterior = SaldoAnterior - Qtde;
                    }
                    else
                    {
                        Controle.ExecutaSQL("INSERT INTO SALDOESTOQUE (ID_LANC,ID_PRODUTO,DATA,SLDANTERIOR,ENTRADAS,SAIDAS,SALDO) VALUES (" + IdLanc.ToString() + "," + IdPrd.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 3) + ",0," + Controle.FloatToStr(-1 * Qtde, 3) + "," + Controle.FloatToStr(SaldoAnterior + Qtde, 3) + ")");
                        SaldoAnterior = SaldoAnterior + Qtde;
                    }
                }
            }
            // Atualização dos Saldo nas Datas posteriores
            DataSet SldMov = new DataSet();
            SldMov = Controle.ConsultaTabela("SELECT * FROM SALDOESTOQUE WHERE Id_Produto=" + IdPrd.ToString() + " AND DATA > CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103) ORDER BY DATA");
            if (SldMov.Tables[0].Rows.Count > 0)
            {               
                for (int I = 0; I <= SldMov.Tables[0].Rows.Count - 1; I++)
                {
                    Controle.ExecutaSQL("UPDATE SALDOESTOQUE SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",SALDO=" + Controle.FloatToStr(SaldoAnterior, 3) + "+(ENTRADAS-SAIDAS) WHERE ID_LANC=" + SldMov.Tables[0].Rows[I]["Id_Lanc"].ToString());
                    SaldoAnterior = SaldoAnterior + (decimal.Parse(SldMov.Tables[0].Rows[I]["Entradas"].ToString()) - decimal.Parse(SldMov.Tables[0].Rows[I]["Saidas"].ToString()));
                }
            }
            //Atualizando o Saldo Atual do Estoque no Cadastro do Produto
            Controle.ExecutaSQL("UPDATE PRODUTOS SET SALDOESTOQUE=" + Controle.FloatToStr(SaldoAnterior, 3)+" WHERE ID_PRODUTO=" + IdPrd.ToString());
        }*/

        private void Atlz_SaldoEstoqueFiscal(int IdFilial, string Mov, string Op, int IdPrd, DateTime Data, decimal Qtde) //Mov E-Entrada S-Saida // Op I-Incluir C-Cancelar
        {
            decimal SaldoAnterior = 0;
            // Verificando o Saldo Anterior

            SqlDataReader TabSldAnt;
            TabSldAnt = Controle.ConsultaSQL("SELECT * FROM SALDOESTOQUEFISCAL WHERE Id_Filial="+IdFilial.ToString()+" AND Id_Produto=" + IdPrd.ToString() + " AND DATA < CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103) ORDER BY DATA DESC");
            if (TabSldAnt.HasRows)
            {
                TabSldAnt.Read();
                SaldoAnterior = decimal.Parse(TabSldAnt["Saldo"].ToString());
            }
            SqlDataReader TabSld;
            TabSld = Controle.ConsultaSQL("SELECT * FROM SALDOESTOQUEFISCAL WHERE Id_Filial=" + IdFilial.ToString() + " AND Id_Produto=" + IdPrd.ToString() + " AND DATA = CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)");
            if (TabSld.HasRows)
            {
                TabSld.Read();
                if (Mov == "E")
                {
                    if (Op == "I")
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUEFISCAL SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",ENTRADAS=ENTRADAS+" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO+" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) + Qtde;
                    }
                    else
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUEFISCAL SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",ENTRADAS=ENTRADAS-" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO-" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) - Qtde;
                    }
                }
                else
                {
                    if (Op == "I")
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUEFISCAL SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",SAIDAS=SAIDAS+" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO-" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) - Qtde;
                    }
                    else
                    {
                        Controle.ExecutaSQL("UPDATE SALDOESTOQUEFISCAL SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",SAIDAS=SAIDAS-" + Controle.FloatToStr(Qtde, 3) + ",SALDO=SALDO+" + Controle.FloatToStr(Qtde, 3) + " WHERE ID_LANC=" + int.Parse(TabSld["ID_LANC"].ToString()));
                        SaldoAnterior = decimal.Parse(TabSld["Saldo"].ToString()) + Qtde;
                    }
                }
            }
            else
            {
                int IdLanc = Controle.ProximoID("SaldoEstoqueFiscal");
                if (Mov == "E")
                {
                    Controle.ExecutaSQL("INSERT INTO SALDOESTOQUEFISCAL (ID_LANC,ID_FILIAL,ID_PRODUTO,DATA,SLDANTERIOR,ENTRADAS,SAIDAS,SALDO) VALUES (" + IdLanc.ToString() + "," + IdFilial.ToString() + "," + IdPrd.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 3) + "," + Controle.FloatToStr(Qtde, 3) + ",0," + Controle.FloatToStr(SaldoAnterior + Qtde, 3) + ")");
                    SaldoAnterior = SaldoAnterior + Qtde;
                }
                else
                {
                    Controle.ExecutaSQL("INSERT INTO SALDOESTOQUEFISCAL (ID_LANC,ID_FILIAL,ID_PRODUTO,DATA,SLDANTERIOR,ENTRADAS,SAIDAS,SALDO) VALUES (" + IdLanc.ToString() + "," + IdFilial.ToString() + "," + IdPrd.ToString() + ",CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103)," + Controle.FloatToStr(SaldoAnterior, 3) + ",0," + Controle.FloatToStr(Qtde, 3) + "," + Controle.FloatToStr(SaldoAnterior + (-1 * Qtde), 3) + ")");
                    SaldoAnterior = SaldoAnterior - Qtde;
                }
            }
            // Atualização dos Saldo nas Datas posteriores
            DataSet SldMov = new DataSet();
            SldMov = Controle.ConsultaTabela("SELECT * FROM SALDOESTOQUEFISCAL WHERE Id_Filial=" + IdFilial.ToString() + " AND Id_Produto=" + IdPrd.ToString() + " AND DATA > CONVERT(DATETIME,'" + Data.ToShortDateString() + "',103) ORDER BY DATA");
            if (SldMov.Tables[0].Rows.Count > 0)
            {
                for (int I = 0; I <= SldMov.Tables[0].Rows.Count - 1; I++)
                {
                    Controle.ExecutaSQL("UPDATE SALDOESTOQUEFISCAL SET SLDANTERIOR=" + Controle.FloatToStr(SaldoAnterior, 3) + ",SALDO=" + Controle.FloatToStr(SaldoAnterior, 3) + "+(ENTRADAS-SAIDAS) WHERE ID_LANC=" + SldMov.Tables[0].Rows[I]["Id_Lanc"].ToString());
                    SaldoAnterior = SaldoAnterior + (decimal.Parse(SldMov.Tables[0].Rows[I]["Entradas"].ToString()) - decimal.Parse(SldMov.Tables[0].Rows[I]["Saidas"].ToString()));
                }
            }
        }

        public bool EstoqueCliente(SqlDataReader Tab, int TpMov, int Opcao, int IdPessoa)
        {
            //TpMov 1-Entrada 2-Said
            //Opcoes 1-Normal 2-Cancela            
            Verificar Ficha = new Verificar();
            Ficha.Controle = Controle;
            try
            {
                while (Tab.Read())
                {
                    if (Ficha.Verificar_ExisteCadastro("ID_PESSOA", "SELECT ID_PESSOA FROM SaldoPrdCliente WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString() + " AND ID_PESSOA=" + IdPessoa.ToString()) == 0)
                        Controle.ExecutaSQL("INSERT INTO SALDOPRDCLIENTE(ID_PESSOA,ID_PRODUTO,SALDO) VALUES(" + IdPessoa.ToString() + "," + Tab["ID_PRODUTO"].ToString() + "," + Controle.FloatToStr(decimal.Parse(Tab["Qtde"].ToString())) + ")");
                    else
                    {
                        if (TpMov == 1)
                        {
                            if (Opcao == 1)
                                Controle.ExecutaSQL("Update SaldoPrdCliente set Saldo=Saldo+" + Controle.FloatToStr(decimal.Parse(Tab["Qtde"].ToString())) + " WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString() + " AND ID_PESSOA=" + IdPessoa.ToString());
                            else
                                Controle.ExecutaSQL("Update SaldoPrdCliente set Saldo=Saldo-" + Controle.FloatToStr(decimal.Parse(Tab["Qtde"].ToString())) + " WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString() + " AND ID_PESSOA=" + IdPessoa.ToString());
                        }
                        else if (TpMov == 2)
                        {
                            if (Opcao == 1)
                                Controle.ExecutaSQL("Update SaldoPrdCliente set Saldo=Saldo-" + Controle.FloatToStr(decimal.Parse(Tab["Qtde"].ToString())) + " WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString() + " AND ID_PESSOA=" + IdPessoa.ToString());
                            else
                                Controle.ExecutaSQL("Update SaldoPrdCliente set Saldo=Saldo+" + Controle.FloatToStr(decimal.Parse(Tab["Qtde"].ToString())) + " WHERE ID_PRODUTO=" + Tab["ID_PRODUTO"].ToString() + " AND ID_PESSOA=" + IdPessoa.ToString());
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
