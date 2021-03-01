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
    public class NotaFiscalItens
    {
        private int _IdNota;
        public int IdNota
        {
            get { return _IdNota; }
            set { _IdNota = value; }
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
        private decimal _PIcmsSub;
        public decimal PIcmsSub
        {
            get { return _PIcmsSub; }
            set { _PIcmsSub = value; }
        }
        private decimal _VlrIcmsSub;
        public decimal VlrIcmsSub
        {
            get { return _VlrIcmsSub; }
            set { _VlrIcmsSub = value; }
        }
        private decimal _PercRed;
        public decimal PercRed
        {
            get { return _PercRed; }
            set { _PercRed = value; }
        }
        private int _SitTributaria;
        public int SitTributaria
        {
            get { return _SitTributaria; }
            set { _SitTributaria = value; }
        }
        private int _IdCfop;
        public int IdCfop
        {
            get { return _IdCfop; }
            set { _IdCfop = value; }
        }

        private int _IdReducao;
        public int IdReducao
        {
            get { return _IdReducao; }
            set { _IdReducao = value; }
        }

        private int _Cst;
        public int Cst
        {
            get { return _Cst; }
            set { _Cst = value; }
        }

        private int _ItemPed;
        public int ItemPed
        {
            get { return _ItemPed; }
            set { _ItemPed = value; }
        }

        private int _IdPromocao;
        public int IdPromocao
        {
            get { return _IdPromocao; }
            set { _IdPromocao = value; }
        }
        private string _CodPrdCliente;
        public string CodPrdCliente
        {
            get { return _CodPrdCliente; }
            set { _CodPrdCliente = value; }
        }

        
        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdItem        = 0;
            IdNota        = 0;
            IdProduto     = 0;
            Qtde          = 0;
            VlrUnitario   = 0;
            VlrTotal      = 0;
            PIpi          = 0;
            VlrIpi        = 0;
            PIcms         = 0;
            VlrIcms       = 0;
            PIcmsSub      = 0;
            VlrIcmsSub    = 0;
            PercRed       = 0;
            SitTributaria = 0;
            IdCfop        = 0;
            IdReducao     = 0;
            Cst           = 0;
            ItemPed       = 0;
            IdPromocao    = 0;
            CodPrdCliente = "";

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM NotaFiscalItens WHERE Id_Item=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdItem        = Id;
                    IdNota        = int.Parse(Tabela["Id_Nota"].ToString());
                    IdProduto     = int.Parse(Tabela["Id_Produto"].ToString());
                    Qtde          = decimal.Parse(Tabela["Qtde"].ToString());
                    VlrUnitario   = decimal.Parse(Tabela["VlrUnitario"].ToString());                                        
                    VlrTotal      = decimal.Parse(Tabela["VlrTotal"].ToString());
                    PIpi          = decimal.Parse(Tabela["PIpi"].ToString());
                    VlrIpi        = decimal.Parse(Tabela["VlrIpi"].ToString());
                    PIcms         = decimal.Parse(Tabela["PIcms"].ToString());
                    VlrIcms       = decimal.Parse(Tabela["VlrIcms"].ToString());
                    PIcmsSub      = decimal.Parse(Tabela["PIcmsSub"].ToString());
                    VlrIcmsSub    = decimal.Parse(Tabela["VlrIcmsSub"].ToString());
                    PercRed       = decimal.Parse(Tabela["PercRed"].ToString());
                    CodPrdCliente = Tabela["PercRed"].ToString().Trim();

                    if (Tabela["SitTributaria"].ToString() != "")
                        SitTributaria = int.Parse(Tabela["SitTributaria"].ToString());

                    if (Tabela["Id_Cfop"].ToString() != "")
                        IdCfop = int.Parse(Tabela["Id_Cfop"].ToString());

                    if (Tabela["Id_Reducao"].ToString() != "")
                        IdReducao = int.Parse(Tabela["Id_Reducao"].ToString());

                    if (Tabela["Cst"].ToString() != "")
                        Cst = int.Parse(Tabela["Cst"].ToString());

                    if (Tabela["ItemPed"].ToString() != "")
                        ItemPed = int.Parse(Tabela["ItemPed"].ToString());

                    if (Tabela["Id_Promocao"].ToString() != "")
                        IdPromocao = int.Parse(Tabela["Id_Promocao"].ToString());
                    
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
                sSQL = "UPDATE NotaFiscalItens SET Id_Nota=@Id,Id_Item=@IdItem,Id_Produto=@IdProduto,Qtde=@Qtde,VlrUnitario=@VlrUnitario,VlrTotal=@VlrTotal,PIpi=@PIpi,"+
                       "VlrIpi=@VlrIpi,PIcms=@PIcms,VlrIcms=@VlrIcms,PIcmsSub=@PIcmsSub,VlrIcmsSub=@VlrIcmsSub,PercRed=@PercRed,SitTributaria=@SitTributaria,Id_Cfop=@IdCfop,"+
                       "Id_Reducao=@IdReducao,Cst=@Cst,ItemPed=@ItemPed,Id_Promocao=@IdPromocao,CodPrdCliente=@CodPrdCliente Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("NotaFiscalItens");
                sSQL = "INSERT INTO NotaFiscalItens (Id_Nota,Id_Item,Id_Produto,Qtde,VlrUnitario,VlrTotal,PIpi,VlrIpi,PIcms,VlrIcms,PIcmsSub,VlrIcmsSub,PercRed,SitTributaria,Id_Cfop,Id_Reducao,Cst,ItemPed,Id_Promocao,CodPrdCliente) " +
                       " VALUES(@Id,@IdItem,@IdProduto,@Qtde,@VlrUnitario,@VlrTotal,@PIpi,@VlrIpi,@PIcms,@VlrIcms,@PIcmsSub,@VlrIcmsSub,@PercRed,@SitTributaria,@IdCfop,@IdReducao,@Cst,@ItemPed,@IdPromocao,@CodPrdCliente)";
            }
            if (sSQL != "")
            {   
                //Calculo do Imposto
                VlrTotal   = Qtde * VlrUnitario;
                VlrIpi     = 0;
                VlrIcms    = 0;
                VlrIcmsSub = 0;

                if (PIpi > 0 && VlrTotal > 0)
                    VlrIpi = (VlrTotal * PIpi) / 100;

                if (PIcms > 0 && VlrTotal > 0)
                {
                    if (PercRed > 0)
                        VlrIcms = ((VlrTotal-(VlrTotal*PercRed/100)) * PIcms) / 100;
                    else
                        VlrIcms = (VlrTotal * PIcms) / 100;
                }

                if (PIcmsSub > 0 && VlrTotal > 0)
                    VlrIcmsSub = (VlrTotal * PIcmsSub) / 100;  
                //--------------------------------------------
                Nm_param.Add("@Id");            Vr_param.Add(IdNota);
                Nm_param.Add("@IdItem");        Vr_param.Add(IdItem);
                Nm_param.Add("@IdProduto");     Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");          Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@VlrUnitario");   Vr_param.Add(Controle.FloatToStr(VlrUnitario, 3));
                Nm_param.Add("@VlrTotal");      Vr_param.Add(Controle.FloatToStr(VlrTotal, 2));
                Nm_param.Add("@PIpi");          Vr_param.Add(Controle.FloatToStr(PIpi, 2));
                Nm_param.Add("@VlrIpi");        Vr_param.Add(Controle.FloatToStr(VlrIpi, 2));
                Nm_param.Add("@PIcms");         Vr_param.Add(Controle.FloatToStr(PIcms, 2));
                Nm_param.Add("@VlrIcms");       Vr_param.Add(Controle.FloatToStr(VlrIcms, 2));
                Nm_param.Add("@PIcmsSub");      Vr_param.Add(Controle.FloatToStr(PIcmsSub, 2));
                Nm_param.Add("@VlrIcmsSub");    Vr_param.Add(Controle.FloatToStr(VlrIcmsSub, 2));
                Nm_param.Add("@PercRed");       Vr_param.Add(Controle.FloatToStr(PercRed, 2));
                Nm_param.Add("@SitTributaria"); Vr_param.Add(SitTributaria);
                Nm_param.Add("@IdCfop");        Vr_param.Add(Controle.FloatToStr(IdCfop, 2));
                Nm_param.Add("@IdReducao");     Vr_param.Add(IdReducao);
                Nm_param.Add("@Cst");           Vr_param.Add(Cst);
                Nm_param.Add("@ItemPed");       Vr_param.Add(ItemPed);
                Nm_param.Add("@IdPromocao");    Vr_param.Add(IdPromocao);
                Nm_param.Add("@CodPrdCliente"); Vr_param.Add(CodPrdCliente);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
                Controle.ExecutaSQL("DELETE FROM NotaFiscalItens WHERE Id_Item=" + IdItem.ToString().Trim());
        }
    }
}
