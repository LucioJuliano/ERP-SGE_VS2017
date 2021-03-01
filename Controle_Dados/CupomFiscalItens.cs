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
    public class CupomFiscalItens
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
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
        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private decimal _VlrBIcms;
        public decimal VlrBIcms
        {
            get { return _VlrBIcms; }
            set { _VlrBIcms = value; }
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
        private decimal _VlrIsento;
        public decimal VlrIsento
        {
            get { return _VlrIsento; }
            set { _VlrIsento = value; }
        }
        private decimal _VlrSubstituicao;
        public decimal VlrSubstituicao
        {
            get { return _VlrSubstituicao; }
            set { _VlrSubstituicao = value; }
        }
        private decimal _VlrNaotributado;
        public decimal VlrNaotributado
        {
            get { return _VlrNaotributado; }
            set { _VlrNaotributado = value; }
        }
        private int _SitTributaria;
        public int SitTributaria
        {
            get { return _SitTributaria; }
            set { _SitTributaria = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdLanc          = 0;
            IdItem          = 0;
            IdProduto       = 0;
            Qtde            = 0;
            VlrUnitario     = 0;
            VlrTotal        = 0;
            VlrBIcms        = 0;
            PIcms           = 0;
            VlrIcms         = 0;
            VlrSubstituicao = 0;
            VlrIsento       = 0;
            VlrNaotributado = 0;
            SitTributaria   = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM CUPOMFISCALITENS WHERE Id_ITEM=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdItem          = Id;
                    IdLanc          = int.Parse(Tabela["ID_Lanc"].ToString());
                    IdProduto       = int.Parse(Tabela["Id_Produto"].ToString());
                    Qtde            = decimal.Parse(Tabela["Qtde"].ToString());             
                    VlrUnitario     = decimal.Parse(Tabela["VlrUnitario"].ToString());
                    VlrTotal        = decimal.Parse(Tabela["VlrTotal"].ToString());
                    VlrBIcms        = decimal.Parse(Tabela["Vlr_BIcms"].ToString());
                    PIcms           = decimal.Parse(Tabela["P_Icms"].ToString());
                    VlrIcms         = decimal.Parse(Tabela["VlrIcms"].ToString());
                    VlrIsento       = decimal.Parse(Tabela["VlrIsento"].ToString());
                    VlrSubstituicao = decimal.Parse(Tabela["VlrSubstituicao"].ToString());
                    VlrNaotributado = decimal.Parse(Tabela["VlrNaoTributado"].ToString());
                    SitTributaria   = int.Parse(Tabela["SitTributaria"].ToString());
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
                sSQL = "UPDATE CUPOMFISCALITENS SET ID_ITEM=@Id,Id_LANC=@IdLanc,Id_Produto=@IdProduto,Qtde=@Qtde,VlrUnitario=@VlrUnitario,VlrTotal=@VlrTotal,Vlr_BIcms=@VlrBIcms,P_Icms=@PIcms,VlrIcms=@VlrIcms,"+
                       "VlrIsento=@VlrIsento,VlrSubstituicao=@VlrSubstituicao,VlrNaoTributado=@VlrNaoTributado,SitTributaria=@SitTributaria Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("CUPOMFISCALITENS");
                sSQL = "INSERT INTO CUPOMFISCALITENS (ID_ITEM,Id_LANC,Id_Produto,Qtde,VlrUnitario,VlrTotal,Vlr_BIcms,P_Icms,VlrIcms,VlrIsento,VlrSubstituicao,VlrNaoTributado,SitTributaria) VALUES " +
                                                   " (@Id,@IdLANC,@IdProduto,@Qtde,@VlrUnitario,@VlrTotal,@VlrBIcms,@PIcms,@VlrIcms,@VlrIsento,@VlrSubstituicao,@VlrNaoTributado,@SitTributaria) ";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");              Vr_param.Add(IdItem);
                Nm_param.Add("@IdLanc");          Vr_param.Add(IdLanc);
                Nm_param.Add("@Idproduto");       Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");            Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@VlrUnitario");     Vr_param.Add(Controle.FloatToStr(VlrUnitario, 2));
                Nm_param.Add("@VlrTotal");        Vr_param.Add(Controle.FloatToStr(VlrTotal, 2));
                Nm_param.Add("@VlrBICms");        Vr_param.Add(Controle.FloatToStr(VlrBIcms, 2));
                Nm_param.Add("@PIcms");           Vr_param.Add(Controle.FloatToStr(PIcms, 2));
                Nm_param.Add("@VlrIcms");         Vr_param.Add(Controle.FloatToStr(VlrIcms, 2));
                Nm_param.Add("@VlrIsento");       Vr_param.Add(Controle.FloatToStr(VlrIsento, 2));
                Nm_param.Add("@VlrSubsTituicao"); Vr_param.Add(Controle.FloatToStr(VlrSubstituicao, 2));
                Nm_param.Add("@VlrNaoTributado"); Vr_param.Add(Controle.FloatToStr(VlrNaotributado, 2));
                Nm_param.Add("@SitTributaria");   Vr_param.Add(SitTributaria);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }        
    }
}
