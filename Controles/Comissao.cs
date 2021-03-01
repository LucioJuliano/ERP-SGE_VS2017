using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controle_Dados;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Controles
{
    [Serializable()]
    public class Comissao
    {
        public Funcoes Controle = new Funcoes();

        //Calculo Anterior da Comissao
       /* public bool CalcularMovimento(SqlDataReader Tab, decimal PDescMv, bool NF, bool Distribuidor, int IdFormaPgto, Parametros ParamCalculo, decimal PComPessoa, int IdPessoa)
        {
            try
            {
                while (Tab.Read())
                {
                    FormaPagamento FormaPgto = new FormaPagamento();
                    FormaPgto.Controle = Controle;
                    FormaPgto.Desconto = 0;
                    FormaPgto.LerDados(IdFormaPgto);
                    int Item           = int.Parse(Tab["Id_Item"].ToString());
                    decimal QtdeItem   = decimal.Parse(Tab["Qtde"].ToString());
                    decimal PrcUnt     = Math.Round(decimal.Parse(Tab["VlrUnitario"].ToString()), 2);
                    decimal PrcUntVenda= Math.Round(decimal.Parse(Tab["VlrUnitario"].ToString()), 2);
                    decimal PrcMinimo  = Math.Round(decimal.Parse(Tab["PrcMinimo"].ToString()), 2);
                    decimal PrcAtacado = Math.Round(decimal.Parse(Tab["PrcAtacado"].ToString()), 2);
                    decimal PrcVarejo  = Math.Round(decimal.Parse(Tab["PrcVarejo"].ToString()), 2);
                    decimal PrcUntCom  = PrcUnt; ;
                    decimal PComissao  = 0;
                    decimal FaixaCom1  = ParamCalculo.Comissao1;
                    decimal FaixaCom2  = ParamCalculo.Comissao2;
                    decimal FaixaCom3  = ParamCalculo.Comissao3;
                    decimal ComisDist  = ParamCalculo.Comissao4;
                    decimal FaixaPrc   = 0;
                    decimal FaixaCom   = 0;
                    decimal VlrComissao=0;
                    decimal PDescPgto  = FormaPgto.Desconto;
                    PDescMv = Math.Round(PDescMv, 6);

                    if (PDescMv>0)
                        PrcUntVenda = PrcUntVenda * (1 - (PDescMv / 100));
                    
                    //Verificando se o Produto tem comissao fixa por cliente
                    SqlDataReader ComissaoPrd = Controle.ConsultaSQL("SELECT * FROM ComissaoPrdCliente WHERE Id_produto=" + int.Parse(Tab["ID_Produto"].ToString()) + " AND ID_PESSOA=" + IdPessoa.ToString());

                    if (ComissaoPrd.HasRows)
                    {
                        ComissaoPrd.Read();
                        if (decimal.Parse(ComissaoPrd["P_Comissao"].ToString()) > 0)
                            PComissao = decimal.Parse(ComissaoPrd["P_Comissao"].ToString());
                    }
                    else if (PComPessoa > 0)
                        PComissao = PComPessoa;
                    else if (decimal.Parse(Tab["PComVend"].ToString()) > 0)
                    {
                        PComissao = decimal.Parse(Tab["PComVend"].ToString());
                        PrcUnt = PrcUntVenda;
                    }
                    else
                    {
                        if ((PDescPgto > 0 && PDescMv > 0) && (PrcUnt >= PrcMinimo && PDescMv <= PDescPgto))
                            PrcUnt = PrcUnt;
                        else
                        {
                            if (PDescMv > 0)
                                PrcUnt = Math.Round(PrcUnt * (1 - (PDescMv / 100)), 2);
                        }

                        if (ComisDist > 0 && Distribuidor)
                            PComissao = ComisDist;
                        else
                        {
                           
                            FaixaPrc = (PrcMinimo - PrcAtacado) / 4;
                            FaixaCom = (FaixaCom1 - FaixaCom2) / 4;
                            if (PrcUnt >= PrcMinimo)
                                PComissao = FaixaCom1;
                            else if (PrcUnt >= PrcAtacado && PrcUnt < PrcMinimo)
                            {
                                if (PrcUnt >= (PrcMinimo - FaixaPrc))
                                    PComissao = FaixaCom1 - FaixaCom;
                                else if (PrcUnt >= (PrcMinimo - (FaixaPrc * 2)))
                                    PComissao = FaixaCom1 - (FaixaCom * 2);
                                else if (PrcUnt >= (PrcMinimo - (FaixaPrc * 3)))
                                    PComissao = FaixaCom1 - (FaixaCom * 3);
                                else
                                    PComissao = FaixaCom2;
                            }
                            else
                                PComissao = FaixaCom3;
                        }
                    }
                    // Atualizando o Item
                    VlrComissao = (PrcUnt * QtdeItem) * (PComissao / 100);
                    Controle.ExecutaSQL("Update MvVendaItens Set VlrUntReal=" + FloatToStr(PrcUntVenda,2) + ",P_Desconto=" + FloatToStr(PDescMv, 6) + ",P_Comissao=" + FloatToStr(PComissao, 2) + ",VlrUntComissao=" + FloatToStr(PrcUnt, 2) + ", VlrComissao=" + FloatToStr(VlrComissao, 2) + " WHERE ID_ITEM=" + Item.ToString());
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }*/

        public bool CalcularMovimento(SqlDataReader Tab, decimal PDescMv, bool Distribuidor, Parametros ParamCalculo, decimal PComPessoa, int IdPessoa)
        {
            try
            {
                //Faixa de Comissao
                decimal FaixaCom1 = decimal.Parse("4,0");
                decimal FaixaCom2 = decimal.Parse("3,5");
                decimal FaixaCom3 = decimal.Parse("3,0");
                decimal FaixaCom4 = decimal.Parse("2,8");
                decimal FaixaCom5 = decimal.Parse("2,6");
                decimal FaixaCom6 = decimal.Parse("2,4");
                decimal FaixaCom7 = decimal.Parse("1,7");
                decimal FaixaCom8 = decimal.Parse("1,0");
                decimal FaixaCom9 = decimal.Parse("0,5");

                while (Tab.Read())
                {                    
                    int Item             = int.Parse(Tab["Id_Item"].ToString());
                    decimal QtdeItem     = decimal.Parse(Tab["Qtde"].ToString());
                    decimal PrcUnt       = Math.Round(decimal.Parse(Tab["VlrUnitario"].ToString()), 2); //Valor Real para calculo comissao
                    decimal PrcUntVenda  = Math.Round(decimal.Parse(Tab["VlrUnitario"].ToString()), 2); //Valor Real Unitario de Venda
                    decimal PrcMinimo    = Math.Round(decimal.Parse(Tab["PrcMinimo"].ToString()), 2);
                    decimal PrcAtacado   = Math.Round(decimal.Parse(Tab["PrcAtacado"].ToString()), 2);
                    decimal PrcVarejo    = Math.Round(decimal.Parse(Tab["PrcVarejo"].ToString()), 2);
                    decimal PrcEspecial  = Math.Round(decimal.Parse(Tab["PrcEspecial"].ToString()), 2);
                    decimal PrcUntCom    = PrcUnt; ;
                    decimal PComissao    = 0;                                        
                    decimal FaixaPrc     = 0;                    
                    decimal VlrComissao  = 0;
                    decimal PComPromocao = decimal.Parse(Tab["PComPromocao"].ToString()); ;
                    PDescMv = Math.Round(PDescMv, 6);

                    if (PDescMv>0)
                        PrcUntVenda = PrcUntVenda * (1 - (PDescMv / 100));
                                        
                    //Verificando se o Produto tem comissao fixa por cliente
                    SqlDataReader ComissaoPrd = Controle.ConsultaSQL("SELECT * FROM ComissaoPrdCliente WHERE Id_produto=" + int.Parse(Tab["ID_Produto"].ToString()) + " AND ID_PESSOA=" + IdPessoa.ToString());
                    if (ComissaoPrd.HasRows)
                    {
                        ComissaoPrd.Read();
                        if (decimal.Parse(ComissaoPrd["P_Comissao"].ToString()) > 0)
                            PComissao = decimal.Parse(ComissaoPrd["P_Comissao"].ToString());
                    }
                    else if (PComPessoa > 0)
                        PComissao = PComPessoa;
                    else if (decimal.Parse(Tab["PComVend"].ToString()) > 0)
                    {
                        PComissao = decimal.Parse(Tab["PComVend"].ToString());
                        PrcUnt = PrcUntVenda;
                    }
                    else if (PComPromocao > 0)
                    {
                        PComissao = PComPromocao;
                        PrcUnt = PrcUntVenda;
                    }
                    else
                    {
                        if (PDescMv > 0)
                            PrcUnt = Math.Round(PrcUnt * (1 - (PDescMv / 100)), 2);

                        if (Distribuidor && ParamCalculo.Comissao4 > 0)
                            PComissao = FaixaCom8;
                        else
                        {
                            if (PrcUnt >= PrcEspecial)
                                PComissao = FaixaCom1;
                            else if (PrcUnt < PrcEspecial && PrcUnt >= PrcVarejo)
                            {
                                FaixaPrc = (PrcEspecial - PrcVarejo) / 2;
                                if (PrcUnt >= (PrcEspecial - FaixaPrc))
                                    PComissao = FaixaCom2;
                                else
                                    PComissao = FaixaCom3;
                            }
                            else if (PrcUnt < PrcVarejo && PrcUnt >= PrcMinimo)
                            {
                                FaixaPrc = (PrcVarejo - PrcMinimo) / 3;
                                if (PrcUnt >= (PrcVarejo - FaixaPrc))
                                    PComissao = FaixaCom4;
                                else if (PrcUnt >= (PrcVarejo - (FaixaPrc * 2)))
                                    PComissao = FaixaCom5;
                                else
                                    PComissao = FaixaCom6;
                            }
                            else if (PrcUnt < PrcMinimo && PrcUnt > PrcAtacado)
                                PComissao = FaixaCom7;
                            else if (PrcUnt == PrcAtacado)
                                PComissao = FaixaCom8;
                            else
                                PComissao = FaixaCom9;
                        }
                    }
                    // Atualizando o Item
                    VlrComissao = (PrcUnt * QtdeItem) * (PComissao / 100);
                    Controle.ExecutaSQL("Update MvVendaItens Set VlrUntReal=" + FloatToStr(PrcUntVenda,2) + ",P_Desconto=" + FloatToStr(PDescMv, 6) + ",P_Comissao=" + FloatToStr(PComissao, 2) + ",VlrUntComissao=" + FloatToStr(PrcUnt, 2) + ", VlrComissao=" + FloatToStr(VlrComissao, 3) + " WHERE ID_ITEM=" + Item.ToString());
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataRow CalcularMovimento(DataRow VdItem, decimal PDescMv, bool Distribuidor, Parametros ParamCalculo, decimal PComPessoa, int IdPessoa, decimal PComVend)
        {
            try
            {
                //Faixa de Comissao
                decimal FaixaCom1 = decimal.Parse("4,0");
                decimal FaixaCom2 = decimal.Parse("3,5");
                decimal FaixaCom3 = decimal.Parse("3,0");
                decimal FaixaCom4 = decimal.Parse("2,8");
                decimal FaixaCom5 = decimal.Parse("2,6");
                decimal FaixaCom6 = decimal.Parse("2,4");
                decimal FaixaCom7 = decimal.Parse("1,7");
                decimal FaixaCom8 = decimal.Parse("1,0");
                decimal FaixaCom9 = decimal.Parse("0,5");

                //SqlDataReader Tab = Controle.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                //                                         " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + VdItem["Id_Venda.ToString() + " and t1.Id_item=" + VdItem["Id_Item"].ToString());


                decimal QtdeItem     = decimal.Parse(VdItem["Qtde"].ToString());
                decimal PrcUnt       = Math.Round(decimal.Parse(VdItem["VlrUnitario"].ToString()), 2); //Valor Real para calculo comissao
                decimal PrcUntVenda  = Math.Round(decimal.Parse(VdItem["VlrUnitario"].ToString()), 2); //Valor Real Unitario de Venda
                decimal PrcMinimo    = Math.Round(decimal.Parse(VdItem["PrcMinimo"].ToString()), 2);
                decimal PrcAtacado   = Math.Round(decimal.Parse(VdItem["PrcAtacado"].ToString()), 2);
                decimal PrcVarejo    = Math.Round(decimal.Parse(VdItem["PrcVarejo"].ToString()), 2);
                decimal PrcEspecial  = Math.Round(decimal.Parse(VdItem["PrcEspecial"].ToString()), 2);
                decimal PrcUntCom    = PrcUnt; ;
                decimal PComissao    = 0;
                decimal FaixaPrc     = 0;
                decimal VlrComissao  = 0;
                decimal PComPromocao = decimal.Parse(VdItem["PComPromocao"].ToString());
                PDescMv = Math.Round(PDescMv, 6);

                if (PDescMv > 0)
                    PrcUntVenda = PrcUntVenda * (1 - (PDescMv / 100));
                                
                //Verificando se o Produto tem comissao fixa por cliente
                SqlDataReader ComissaoPrd = Controle.ConsultaSQL("SELECT * FROM ComissaoPrdCliente WHERE Id_produto=" + VdItem["Id_Produto"].ToString() + " AND ID_PESSOA=" + IdPessoa.ToString());

                if (ComissaoPrd.HasRows)
                {
                    ComissaoPrd.Read();
                    if (decimal.Parse(ComissaoPrd["P_Comissao"].ToString()) > 0)
                        PComissao = decimal.Parse(ComissaoPrd["P_Comissao"].ToString());
                }
                else if (PComPessoa > 0)
                    PComissao = PComPessoa;                
                else if (PComVend > 0)
                {
                    PComissao = PComVend;
                    PrcUnt    = PrcUntVenda;
                }
                else if (PComPromocao > 0)
                {
                    PComissao = PComPromocao;
                    PrcUnt = PrcUntVenda;
                }
                else
                {
                    if (PDescMv > 0)
                        PrcUnt = Math.Round(PrcUnt * (1 - (PDescMv / 100)), 2);

                    if (Distribuidor && ParamCalculo.Comissao4 > 0)
                        PComissao = FaixaCom8;
                    else
                    {
                        if (PrcUnt >= PrcEspecial)
                            PComissao = FaixaCom1;
                        else if (PrcUnt < PrcEspecial && PrcUnt >= PrcVarejo)
                        {
                            FaixaPrc = (PrcEspecial - PrcVarejo) / 2;
                            if (PrcUnt >= (PrcEspecial - FaixaPrc))
                                PComissao = FaixaCom2;
                            else
                                PComissao = FaixaCom3;
                        }
                        else if (PrcUnt < PrcVarejo && PrcUnt >= PrcMinimo)
                        {
                            FaixaPrc = (PrcVarejo - PrcMinimo) / 3;
                            if (PrcUnt >= (PrcVarejo - FaixaPrc))
                                PComissao = FaixaCom4;
                            else if (PrcUnt >= (PrcVarejo - (FaixaPrc * 2)))
                                PComissao = FaixaCom5;
                            else
                                PComissao = FaixaCom6;
                        }
                        else if (PrcUnt < PrcMinimo && PrcUnt > PrcAtacado)
                            PComissao = FaixaCom7;
                        else if (PrcUnt == PrcAtacado)
                            PComissao = FaixaCom8;
                        else
                            PComissao = FaixaCom9;
                    }
                }
                // Atualizando o Item
                // VlrComissao = Math.Round((PrcUnt * QtdeItem) * (PComissao / 100),2);
                VlrComissao = (PrcUnt * QtdeItem) * (PComissao / 100);
                VdItem["VlrComissao"] = VlrComissao;
                VdItem["P_COMISSAO"]  = PComissao;
            }
            catch
            {
            }
            return VdItem;
        }

        public string FloatToStr(decimal Vr, int CasaDec)
        {
            Vr = Math.Round(Vr, CasaDec); //, MidpointRounding.AwayFromZero);
            return Vr.ToString().Replace(",", ".");
        }
        public bool CalcularMovimento2019(SqlDataReader Tab, decimal PDescMv, bool Distribuidor, Parametros ParamCalculo, decimal PComPessoa, int IdPessoa)
        {
            try
            {
                //Faixa de Comissao
                decimal FaixaCom1 = decimal.Parse("5,0");
                decimal FaixaCom2 = decimal.Parse("4,0");
                decimal FaixaCom3 = decimal.Parse("3,0");                
                decimal FaixaCom4 = decimal.Parse("2,4");
                decimal FaixaCom5 = decimal.Parse("1,7");
                decimal FaixaCom6 = decimal.Parse("1,0");
                decimal FaixaCom7 = decimal.Parse("0,5");

                while (Tab.Read())
                {
                    int Item = int.Parse(Tab["Id_Item"].ToString());
                    decimal QtdeItem = decimal.Parse(Tab["Qtde"].ToString());
                    decimal PrcUnt         = Math.Round(decimal.Parse(Tab["VlrUnitario"].ToString()), 2); //Valor Real para calculo comissao
                    decimal PrcUntVenda    = Math.Round(decimal.Parse(Tab["VlrUnitario"].ToString()), 2); //Valor Real Unitario de Venda
                    decimal PrcAtacado     = Math.Round(decimal.Parse(Tab["PrcAtacado"].ToString()), 2);
                    decimal PrcMinimo      = Math.Round(decimal.Parse(Tab["PrcMinimo"].ToString()), 2);                    
                    decimal PrcVarejo      = Math.Round(decimal.Parse(Tab["PrcVarejo"].ToString()), 2);
                    decimal PrcEspecial    = Math.Round(decimal.Parse(Tab["PrcEspecial"].ToString()), 2);
                    decimal PrcSensacional = Math.Round(decimal.Parse(Tab["PrcSensacional"].ToString()), 2);
                    decimal PrcUntCom = PrcUnt; ;
                    decimal PComissao = 0;
                    decimal FaixaPrc = 0;
                    decimal VlrComissao = 0;
                    decimal PComPromocao = decimal.Parse(Tab["PComPromocao"].ToString()); ;
                    PDescMv = Math.Round(PDescMv, 6);

                    if (PDescMv > 0)
                        PrcUntVenda = PrcUntVenda * (1 - (PDescMv / 100));

                    //Verificando se o Produto tem comissao fixa por cliente
                    SqlDataReader ComissaoPrd = Controle.ConsultaSQL("SELECT * FROM ComissaoPrdCliente WHERE Id_produto=" + int.Parse(Tab["ID_Produto"].ToString()) + " AND ID_PESSOA=" + IdPessoa.ToString());
                    if (ComissaoPrd.HasRows)
                    {
                        ComissaoPrd.Read();
                        if (decimal.Parse(ComissaoPrd["P_Comissao"].ToString()) > 0)
                            PComissao = decimal.Parse(ComissaoPrd["P_Comissao"].ToString());
                    }
                    else if (PComPessoa > 0)
                        PComissao = PComPessoa;
                    else if (decimal.Parse(Tab["PComVend"].ToString()) > 0)
                    {
                        PComissao = decimal.Parse(Tab["PComVend"].ToString());
                        PrcUnt = PrcUntVenda;
                    }
                    else if (PComPromocao > 0)
                    {
                        PComissao = PComPromocao;
                        PrcUnt = PrcUntVenda;
                    }
                    else
                    {
                        if (PDescMv > 0)
                            PrcUnt = Math.Round(PrcUnt * (1 - (PDescMv / 100)), 2);

                        if (Distribuidor && ParamCalculo.Comissao4 > 0)
                            PComissao = FaixaCom6;
                        else
                        {
                            if (PrcUnt >= PrcSensacional)
                                PComissao = FaixaCom1;
                            else if (PrcUnt >= PrcEspecial)
                                PComissao = FaixaCom2;
                            else if (PrcUnt >= PrcVarejo)
                                PComissao = FaixaCom3;                            
                            else if (PrcUnt >= PrcMinimo)
                                PComissao = FaixaCom4;
                            else if (PrcUnt >= PrcAtacado)
                            { 
                                FaixaPrc = (PrcMinimo - PrcAtacado) / 2;
                                if (PrcUnt >= (PrcAtacado + FaixaPrc))
                                    PComissao = FaixaCom5;                                
                                else
                                    PComissao = FaixaCom6;
                            }
                            else if (PrcUnt < PrcAtacado)
                                PComissao = FaixaCom7;
                            
                        }
                    }
                    // Atualizando o Item
                    VlrComissao = (PrcUnt * QtdeItem) * (PComissao / 100);
                    Controle.ExecutaSQL("Update MvVendaItens Set VlrUntReal=" + FloatToStr(PrcUntVenda, 2) + ",P_Desconto=" + FloatToStr(PDescMv, 6) + ",P_Comissao=" + FloatToStr(PComissao, 2) + ",VlrUntComissao=" + FloatToStr(PrcUnt, 2) + ", VlrComissao=" + FloatToStr(VlrComissao, 3) + " WHERE ID_ITEM=" + Item.ToString());
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataRow CalcularMovimento2019(DataRow VdItem, decimal PDescMv, bool Distribuidor, Parametros ParamCalculo, decimal PComPessoa, int IdPessoa, decimal PComVend)
        {
            try
            {
                //Faixa de Comissao
                decimal FaixaCom1 = decimal.Parse("5,0");
                decimal FaixaCom2 = decimal.Parse("4,0");
                decimal FaixaCom3 = decimal.Parse("3,0");
                decimal FaixaCom4 = decimal.Parse("2,4");
                decimal FaixaCom5 = decimal.Parse("1,7");
                decimal FaixaCom6 = decimal.Parse("1,0");
                decimal FaixaCom7 = decimal.Parse("0,5");

                //SqlDataReader Tab = Controle.ConsultaSQL("SELECT T1.*,T3.COMISSAO AS PCOMVEND FROM MvVendaItens T1 LEFT JOIN MVVENDA T2 ON (T2.ID_VENDA=T1.ID_VENDA)" +
                //                                         " LEFT JOIN VENDEDORES T3 ON (T3.ID_VENDEDOR=T2.ID_VENDEDOR) WHERE T1.TipoItem<>'E' and T1.Id_Venda=" + VdItem["Id_Venda.ToString() + " and t1.Id_item=" + VdItem["Id_Item"].ToString());


                decimal QtdeItem = decimal.Parse(VdItem["Qtde"].ToString());
                decimal PrcUnt = Math.Round(decimal.Parse(VdItem["VlrUnitario"].ToString()), 2); //Valor Real para calculo comissao
                decimal PrcUntVenda = Math.Round(decimal.Parse(VdItem["VlrUnitario"].ToString()), 2); //Valor Real Unitario de Venda
                decimal PrcMinimo = Math.Round(decimal.Parse(VdItem["PrcMinimo"].ToString()), 2);
                decimal PrcAtacado = Math.Round(decimal.Parse(VdItem["PrcAtacado"].ToString()), 2);
                decimal PrcVarejo = Math.Round(decimal.Parse(VdItem["PrcVarejo"].ToString()), 2);
                decimal PrcEspecial = Math.Round(decimal.Parse(VdItem["PrcEspecial"].ToString()), 2);
                decimal PrcSensacional = Math.Round(decimal.Parse(VdItem["PrcSensacional"].ToString()), 2);
                decimal PrcUntCom = PrcUnt; ;
                decimal PComissao = 0;
                decimal FaixaPrc = 0;
                decimal VlrComissao = 0;
                decimal PComPromocao = decimal.Parse(VdItem["PComPromocao"].ToString());
                PDescMv = Math.Round(PDescMv, 6);

                if (PDescMv > 0)
                    PrcUntVenda = PrcUntVenda * (1 - (PDescMv / 100));

                //Verificando se o Produto tem comissao fixa por cliente
                SqlDataReader ComissaoPrd = Controle.ConsultaSQL("SELECT * FROM ComissaoPrdCliente WHERE Id_produto=" + VdItem["Id_Produto"].ToString() + " AND ID_PESSOA=" + IdPessoa.ToString());

                if (ComissaoPrd.HasRows)
                {
                    ComissaoPrd.Read();
                    if (decimal.Parse(ComissaoPrd["P_Comissao"].ToString()) > 0)
                        PComissao = decimal.Parse(ComissaoPrd["P_Comissao"].ToString());
                }
                else if (PComPessoa > 0)
                    PComissao = PComPessoa;
                else if (PComVend > 0)
                {
                    PComissao = PComVend;
                    PrcUnt = PrcUntVenda;
                }
                else if (PComPromocao > 0)
                {
                    PComissao = PComPromocao;
                    PrcUnt = PrcUntVenda;
                }
                else
                {
                    if (PDescMv > 0)
                        PrcUnt = Math.Round(PrcUnt * (1 - (PDescMv / 100)), 2);

                    if (Distribuidor && ParamCalculo.Comissao4 > 0)
                        PComissao = FaixaCom6;
                    else
                    {
                        if (PrcUnt >= PrcSensacional)
                            PComissao = FaixaCom1;
                        else if (PrcUnt >= PrcEspecial)
                            PComissao = FaixaCom2;
                        else if (PrcUnt >= PrcVarejo)
                            PComissao = FaixaCom3;
                        else if (PrcUnt >= PrcMinimo)
                            PComissao = FaixaCom4;
                        else if (PrcUnt >= PrcAtacado)
                        {
                            FaixaPrc = (PrcMinimo - PrcAtacado) / 2;
                            if (PrcUnt >= (PrcAtacado + FaixaPrc))
                                PComissao = FaixaCom5;
                            else
                                PComissao = FaixaCom6;
                        }
                        else if (PrcUnt < PrcAtacado)
                            PComissao = FaixaCom7;
                    }
                }
                // Atualizando o Item
                // VlrComissao = Math.Round((PrcUnt * QtdeItem) * (PComissao / 100),2);
                VlrComissao = (PrcUnt * QtdeItem) * (PComissao / 100);
                VdItem["VlrComissao"] = VlrComissao;
                VdItem["P_COMISSAO"] = PComissao;
            }
            catch
            {
            }
            return VdItem;
        }
    }
}
