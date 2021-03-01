using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using Controle_Dados;

namespace Controle_Dados
{
    public class MvEstoqueItens
    {
        private int _IdMov;
        public int IdMov
        {
            get { return _IdMov; }
            set { _IdMov = value; }
        }
        private int _IdItem;
        public int IdItem
        {
            get { return _IdItem; }
            set { _IdItem = value; }
        }
        private int _IdProduto;
        public int IdProduto
        {
            get { return _IdProduto; }
            set { _IdProduto = value; }
        }
        private decimal _Qtde;
        public decimal Qtde
        {
            get { return _Qtde; }
            set { _Qtde = value; }
        }
        private decimal _VlrUnitario;
        public decimal VlrUnitario
        {
            get { return _VlrUnitario; }
            set { _VlrUnitario = value; }
        }
        private decimal _VlrSubTotal;
        public decimal VlrSubTotal
        {
            get { return _VlrSubTotal; }
            set { _VlrSubTotal = value; }
        }
        private decimal _VlrDesconto;
        public decimal VlrDesconto
        {
            get { return _VlrDesconto; }
            set { _VlrDesconto = value; }
        }
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private decimal _PIpi;
        public decimal PIpi
        {
            get { return _PIpi; }
            set { _PIpi = value; }
        }
        private decimal _VlrIpi;
        public decimal VlrIpi
        {
            get { return _VlrIpi; }
            set { _VlrIpi = value; }
        }
        private decimal _PIcms;
        public decimal PIcms
        {
            get { return _PIcms; }
            set { _PIcms = value; }
        }
        private decimal _VlrIcms;
        public decimal VlrIcms
        {
            get { return _VlrIcms; }
            set { _VlrIcms = value; }
        }
        private decimal _VlrPrcCompra;
        public decimal VlrPrcCompra
        {
            get { return _VlrPrcCompra; }
            set { _VlrPrcCompra = value; }
        }
        private decimal _VlrPrcClean;
        public decimal VlrPrcClean
        {
            get { return _VlrPrcClean; }
            set { _VlrPrcClean = value; }
        }
        private decimal _PercRed;
        public decimal PercRed
        {
            get { return _PercRed; }
            set { _PercRed = value; }
        }
        private decimal _VlrFrete;
        public decimal VlrFrete
        {
            get { return _VlrFrete; }
            set { _VlrFrete = value; }
        }
        private decimal _VlrIcms_Sub;
        public decimal VlrIcms_Sub
        {
            get { return _VlrIcms_Sub; }
            set { _VlrIcms_Sub = value; }
        }
        private string _NCM;
        public string NCM
        {
            get { return _NCM; }
            set { _NCM = value; }
        }


        private string _CodBarra;
        public string CodBarra
        {
            get { return _CodBarra; }
            set { _CodBarra = value; }
        }
        private int _IdCfop;
        public int IdCfop
        {
            get { return _IdCfop; }
            set { _IdCfop = value; }
        }
        private int _Cst;
        public int Cst
        {
            get { return _Cst; }
            set { _Cst = value; }
        }

        private string _Validade;
        public string Validade
        {
            get { return _Validade; }
            set { _Validade = value; }
        }
        private string _Lote;
        public string Lote
        {
            get { return _Lote; }
            set { _Lote = value; }
        }
        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdItem       = 0;
            IdMov        = 0;
            IdProduto    = 0;
            Qtde         = 0;
            VlrUnitario  = 0;
            VlrSubTotal  = 0;
            VlrTotal     = 0;
            PIpi         = 0;
            VlrIpi       = 0;
            PIcms        = 0;
            VlrIcms      = 0;
            VlrPrcCompra = 0;
            PercRed      = 0;
            VlrFrete     = 0;
            VlrIcms_Sub  = 0;
            NCM          = "";
            IdCfop       = 0;
            Cst          = 0;
            VlrPrcClean  = 0;
            CodBarra     = "";
            Lote         = " ";
            Validade     = " ";



            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MvEstoqueItens WHERE Id_Item=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdItem       = Id;
                    IdMov        = int.Parse(Tabela["Id_Mov"].ToString());
                    IdProduto    = int.Parse(Tabela["Id_Produto"].ToString());
                    Qtde         = decimal.Parse(Tabela["Qtde"].ToString());
                    VlrUnitario  = decimal.Parse(Tabela["VlrUnitario"].ToString());
                    VlrDesconto  = decimal.Parse(Tabela["VlrDesconto"].ToString());
                    VlrSubTotal  = decimal.Parse(Tabela["VlrSubTotal"].ToString());
                    VlrTotal     = decimal.Parse(Tabela["VlrTotal"].ToString());
                    PIpi         = decimal.Parse(Tabela["P_Ipi"].ToString());
                    VlrIpi       = decimal.Parse(Tabela["VlrIpi"].ToString());
                    PIcms        = decimal.Parse(Tabela["P_Icms"].ToString());
                    VlrIcms      = decimal.Parse(Tabela["VlrIcms"].ToString());
                    VlrPrcCompra = decimal.Parse(Tabela["VlrPrcCompra"].ToString());
                    PercRed      = decimal.Parse(Tabela["PercRed"].ToString());
                    NCM          = Tabela["NCM"].ToString().Trim();
                    CodBarra     = Tabela["CodBarra"].ToString().Trim();
                    Lote         = Tabela["Lote"].ToString().Trim();
                    Validade     = Tabela["Validade"].ToString().Trim();
                    if (Tabela["VlrFrete"].ToString() != "")
                        VlrFrete = decimal.Parse(Tabela["VlrFrete"].ToString());
                    if (Tabela["VlrIcms_Sub"].ToString() != "")
                        VlrIcms_Sub = decimal.Parse(Tabela["VlrIcms_Sub"].ToString());                        
                    if (Tabela["Id_Cfop"].ToString() != "")
                        IdCfop = int.Parse(Tabela["Id_Cfop"].ToString());
                    if (Tabela["CST"].ToString() != "")
                        Cst = int.Parse(Tabela["CST"].ToString());
                    if (Tabela["VlrPrcClean"].ToString() != "")
                        VlrPrcClean = decimal.Parse(Tabela["VlrPrcClean"].ToString());
                    
                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdItem > 0)
            {
                sSQL = "UPDATE MvEstoqueItens SET Id_Mov=@Id,Id_Item=@IdItem,Id_Produto=@IdProduto,Qtde=@Qtde,VlrUnitario=@VlrUnitario,VlrDesconto=@VlrDesconto,VlrSubTotal=@VlrSubTotal,VlrTotal=@VlrTotal,"+
                       "P_Ipi=@PIpi,VlrIpi=@VlrIpi,P_Icms=@PIcms,VlrIcms=@VlrIcms,VlrPrcCompra=@VlrPrcCompra,PercRed=@PercRed,VlrFrete=@VlrFrete,VlrIcms_Sub=@VlrIcms_Sub,NCM=@NCM,Id_Cfop=@IdCfop,Cst=@Cst,"+
                       "VlrPrcClean=@VlrPrcClean,CodBarra=@CodBarra,Lote=@Lote,Validade=@Validade Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("MvEstoqueItens");
                sSQL = "INSERT INTO MvEstoqueItens (Id_Mov,Id_Item,Id_Produto,Qtde,VlrUnitario,VlrDesconto,VlrSubTotal,VlrTotal,P_Ipi,VlrIpi,P_Icms,VlrIcms,VlrPrcCompra,PercRed,VlrFrete,VlrIcms_Sub,NCM,Id_Cfop,Cst,VlrPrcClean,CodBarra,Lote,Validade)" +
                       " VALUES(@Id,@IdItem,@IdProduto,@Qtde,@VlrUnitario,@VlrDesconto,@VlrSubTotal,@VlrTotal,@PIpi,@VlrIpi,@PIcms,@VlrIcms,@VlrPrcCompra,@PercRed,@VlrFrete,@VlrIcms_Sub,@NCM,@IdCfop,@Cst,@VlrPrcClean,@CodBarra,@Lote,@Validade)";
            }
            if (sSQL != "")
            {                
                //Calculo do Imposto
                VlrTotal = VlrSubTotal - VlrDesconto;
                VlrIpi  = 0;
                VlrIcms = 0;

                if (PIpi > 0 && VlrTotal > 0)
                    VlrIpi = (VlrTotal * PIpi) / 100;

                if (PIcms > 0 && VlrTotal > 0)
                {
                    if (PercRed > 0)
                        VlrIcms = ((VlrTotal - (VlrTotal * PercRed / 100)) * PIcms) / 100;
                    else
                        VlrIcms = (VlrTotal * PIcms) / 100;
                }
                //--------------------------------------------
                Nm_param.Add("@Id");           Vr_param.Add(IdMov);
                Nm_param.Add("@IdItem");       Vr_param.Add(IdItem);
                Nm_param.Add("@IdProduto");    Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");         Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@VlrUnitario");  Vr_param.Add(Controle.FloatToStr(VlrUnitario,7));
                Nm_param.Add("@VlrDesconto");  Vr_param.Add(Controle.FloatToStr(VlrDesconto,2));
                Nm_param.Add("@VlrSubTotal");  Vr_param.Add(Controle.FloatToStr(VlrSubTotal,2));
                Nm_param.Add("@VlrTotal");     Vr_param.Add(Controle.FloatToStr(VlrTotal,2));
                Nm_param.Add("@PIpi");         Vr_param.Add(Controle.FloatToStr(PIpi,2));
                Nm_param.Add("@VlrIpi");       Vr_param.Add(Controle.FloatToStr(VlrIpi,2));
                Nm_param.Add("@PIcms");        Vr_param.Add(Controle.FloatToStr(PIcms,2));
                Nm_param.Add("@VlrIcms");      Vr_param.Add(Controle.FloatToStr(VlrIcms,2));
                Nm_param.Add("@VlrPrcCompra"); Vr_param.Add(Controle.FloatToStr(VlrPrcCompra, 2));
                Nm_param.Add("@PercRed");      Vr_param.Add(Controle.FloatToStr(PercRed, 2));
                Nm_param.Add("@VlrFrete");     Vr_param.Add(Controle.FloatToStr(VlrFrete, 2));
                Nm_param.Add("@VlrIcms_Sub");  Vr_param.Add(Controle.FloatToStr(VlrIcms_Sub,2));
                Nm_param.Add("@NCM");          Vr_param.Add(NCM);
                Nm_param.Add("@IdCfop");       Vr_param.Add(IdCfop);
                Nm_param.Add("@Cst");          Vr_param.Add(Cst);
                Nm_param.Add("@VlrPrcClean");  Vr_param.Add(Controle.FloatToStr(VlrPrcClean, 2));
                Nm_param.Add("@CodBarra");     Vr_param.Add(CodBarra);
                Nm_param.Add("@Lote");         Vr_param.Add(Lote);
                Nm_param.Add("@Validade");     Vr_param.Add(Validade);


                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
                CalcularImposto();
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
                Controle.ExecutaSQL("DELETE FROM MvEstoqueItens WHERE Id_Item=" + IdItem.ToString().Trim());
        }
        public void CalcularImposto()
        {
            DataSet Itens = new DataSet();
            Itens = Controle.ConsultaTabela("SELECT T1.*,T2.CREDITOIPI FROM MVESTOQUEITENS T1 LEFT JOIN PRODUTOS T2 ON (T2.ID_PRODUTO=T1.ID_PRODUTO) WHERE T1.ID_MOV=" + IdMov.ToString());
            decimal T_BIcms = 0;
            decimal V_Icms = 0;
            decimal V_Ipi = 0;
            decimal VlrItem = 0;
            decimal CredIPI = 0;
            decimal T_Frete = 0;
            decimal BIcmsSub = 0;
            decimal TIcmsSub = 0;
            if (Itens.Tables[0].Rows.Count > 0)
            {
                for (int I = 0; I <= Itens.Tables[0].Rows.Count - 1; I++)
                {
                    if (decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms"].ToString()) > 0)
                    {
                        if (decimal.Parse(Itens.Tables[0].Rows[I]["PercRed"].ToString()) > 0)
                            VlrItem = decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString()) - Math.Round((decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Itens.Tables[0].Rows[I]["PercRed"].ToString()) / 100), 2); //,MidpointRounding.AwayFromZero);
                        else
                            VlrItem = decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString());

                        T_BIcms = T_BIcms + VlrItem;
                        V_Icms = V_Icms + decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms"].ToString());
                    }
                    if (decimal.Parse(Itens.Tables[0].Rows[I]["VlrIpi"].ToString()) > 0)
                        V_Ipi = V_Ipi + decimal.Parse(Itens.Tables[0].Rows[I]["VlrIpi"].ToString());

                    T_Frete = T_Frete + decimal.Parse(Itens.Tables[0].Rows[I]["VlrFrete"].ToString());
                    if (decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms_Sub"].ToString()) > 0)
                    {
                        BIcmsSub = BIcmsSub + decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString());
                        TIcmsSub = TIcmsSub + decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms_Sub"].ToString());
                    }
                }
            }
            Controle.ExecutaSQL("UPDATE MVESTOQUE SET B_IcmsSub=" + Controle.FloatToStr(BIcmsSub, 2) + ",VlrIcmsSub = " + Controle.FloatToStr(TIcmsSub, 2) + ",VlrFrete=" + Controle.FloatToStr(T_Frete, 2) + ", OutrosIPI=" + Controle.FloatToStr(CredIPI, 2) + ", B_Icms=" + Controle.FloatToStr(T_BIcms, 2) + ",VlrIcms=" + Controle.FloatToStr(V_Icms, 2) + ",VlrIpi=" + Controle.FloatToStr(V_Ipi, 2) + "WHERE ID_MOV=" + IdMov.ToString());
        }
    }
}
