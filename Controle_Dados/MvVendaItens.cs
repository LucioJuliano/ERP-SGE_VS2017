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
    public class MvVendaItens
    {
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private int _IdItem;
        public int IdItem
        {
            get { return _IdItem; }
            set { _IdItem = value; }
        }
        private string _TipoItem;
        public string TipoItem
        {
            get { return _TipoItem; }
            set { _TipoItem = value; }
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
        private decimal _PComissao;
        public decimal PComissao
        {
            get { return _PComissao; }
            set { _PComissao = value; }
        }
        private decimal _VlrComissao;
        public decimal VlrComissao
        {
            get { return _VlrComissao; }
            set { _VlrComissao = value; }
        }
        private decimal _PDesconto;
        public decimal PDesconto
        {
            get { return _PDesconto; }
            set { _PDesconto = value; }
        }
        private decimal _VlrUntComissao;
        public decimal VlrUntComissao
        {
            get { return _VlrUntComissao; }
            set { _VlrUntComissao = value; }
        }
        private decimal _PrcCusto;
        public decimal PrcCusto
        {
            get { return _PrcCusto; }
            set { _PrcCusto = value; }
        }
        private decimal _PrcEspecial;
        public decimal PrcEspecial
        {
            get { return _PrcEspecial; }
            set { _PrcEspecial = value; }
        }        
        private decimal _PrcMinimo;
        public decimal PrcMinimo
        {
            get { return _PrcMinimo; }
            set { _PrcMinimo = value; }
        }
        private decimal _PrcVarejo;
        public decimal PrcVarejo
        {
            get { return _PrcVarejo; }
            set { _PrcVarejo = value; }
        }
        private decimal _PrcAtacado;
        public decimal PrcAtacado
        {
            get { return _PrcAtacado; }
            set { _PrcAtacado = value; }
        }
        private decimal _PrcSensacional;
        public decimal PrcSensacional
        {
            get { return _PrcSensacional; }
            set { _PrcSensacional = value; }
        }
        private int _Vinculado;
        public int Vinculado
        {
            get { return _Vinculado; }
            set { _Vinculado = value; }
        }
        private int _MargemNegocio;
        public int MargemNegocio
        {
            get { return _MargemNegocio; }
            set { _MargemNegocio = value; }
        }

        private int _IdPromocao;
        public int IdPromocao
        {
            get { return _IdPromocao; }
            set { _IdPromocao = value; }
        }

        private int _NaoRentab;
        public int NaoRentab
        {
            get { return _NaoRentab; }
            set { _NaoRentab = value; }
        }

        private int _PromQtdeItem;
        public int PromQtdeItem
        {
            get { return _PromQtdeItem; }
            set { _PromQtdeItem = value; }
        }

        private decimal _PComPromocao;
        public decimal PComPromocao
        {
            get { return _PComPromocao; }
            set { _PComPromocao = value; }
        }

        private int _ItemPed;
        public int ItemPed
        {
            get { return _ItemPed; }
            set { _ItemPed = value; }
        }

        private int _IdUsuLibPrc;
        public int IdUsuLibPrc
        {
            get { return _IdUsuLibPrc; }
            set { _IdUsuLibPrc = value; }
        }

        private decimal _PrcEspDist;
        public decimal PrcEspDist
        {
            get { return _PrcEspDist; }
            set { _PrcEspDist = value; }
        }
        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdItem = 0;
            TipoItem = "S";
            IdVenda = 0;
            IdProduto = 0;
            Qtde = 1;
            VlrUnitario    = 0;
            VlrTotal       = 0;
            PComissao      = 0;
            VlrComissao    = 0;
            PDesconto      = 0;
            VlrUntComissao = 0;
            PrcEspecial    = 0;
            PrcCusto       = 0;
            PrcMinimo      = 0;
            PrcVarejo      = 0;
            PrcAtacado     = 0;
            PrcSensacional = 0;
            Vinculado      = 0;
            MargemNegocio  = 0;
            IdPromocao     = 0;
            NaoRentab      = 0;
            PromQtdeItem   = 0;
            PComPromocao   = 0;
            ItemPed        = 0;
            IdUsuLibPrc    = 0;
            PrcEspDist     = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MvVendaItens WHERE Id_Item=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {                    
                    Tabela.Read();
                    IdItem         = Id;
                    IdVenda        = int.Parse(Tabela["Id_Venda"].ToString());
                    TipoItem       = Tabela["TipoItem"].ToString();
                    IdProduto      = int.Parse(Tabela["Id_Produto"].ToString());
                    Qtde           = decimal.Parse(Tabela["Qtde"].ToString());
                    VlrUnitario    = decimal.Parse(Tabela["VlrUnitario"].ToString());                    
                    VlrTotal       = decimal.Parse(Tabela["VlrTotal"].ToString());
                    PComissao      = decimal.Parse(Tabela["P_Comissao"].ToString());
                    VlrComissao    = decimal.Parse(Tabela["VlrComissao"].ToString());
                    PDesconto      = decimal.Parse(Tabela["P_Desconto"].ToString());
                    VlrUntComissao = decimal.Parse(Tabela["VlrUntComissao"].ToString());
                    PrcCusto       = decimal.Parse(Tabela["PrcCusto"].ToString());
                    PrcMinimo      = decimal.Parse(Tabela["PrcMinimo"].ToString());
                    PrcVarejo      = decimal.Parse(Tabela["PrcVarejo"].ToString());
                    PrcAtacado     = decimal.Parse(Tabela["PrcAtacado"].ToString());
                    PrcSensacional = decimal.Parse(Tabela["PrcSensacional"].ToString());

                    if (Tabela["PrcEspecial"].ToString() != "")
                        PrcEspecial = decimal.Parse(Tabela["PrcEspecial"].ToString());

                    if (Tabela["Vinculado"].ToString() != "")
                        Vinculado = int.Parse(Tabela["Vinculado"].ToString());

                    if (Tabela["MargemNegocio"].ToString() != "")
                        MargemNegocio = int.Parse(Tabela["MargemNegocio"].ToString());

                    if (Tabela["Id_Promocao"].ToString() != "")
                        IdPromocao = int.Parse(Tabela["Id_Promocao"].ToString());

                    if (Tabela["NaoRentab"].ToString() != "")
                        NaoRentab = int.Parse(Tabela["NaoRentab"].ToString());

                    if (Tabela["PromQtdeItem"].ToString() != "")
                        PromQtdeItem = int.Parse(Tabela["PromQtdeItem"].ToString());

                    if (Tabela["PComPromocao"].ToString() != "")
                        PComPromocao = decimal.Parse(Tabela["PComPromocao"].ToString());

                    if (Tabela["PrcEspDist"].ToString() != "")
                        PrcEspDist = decimal.Parse(Tabela["PrcEspDist"].ToString());                    

                    if (Tabela["ItemPed"].ToString() != "")
                        ItemPed = int.Parse(Tabela["ItemPed"].ToString());

                    if (Tabela["Id_UsuLibPrc"].ToString() != "")
                        IdUsuLibPrc = int.Parse(Tabela["Id_UsuLibPrc"].ToString());
                    
                    
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
                sSQL = "UPDATE MvVendaItens SET Id_Venda=@Id,Id_Item=@IdItem,TipoItem=@TipoItem,Id_Produto=@IdProduto,Qtde=@Qtde,VlrUnitario=@VlrUnitario,VlrTotal=@VlrTotal," +
                       "P_Comissao=@PComissao,VlrComissao=@VlrComissao,PrcCusto=@PrcCusto,PrcMinimo=@PrcMinimo,PrcVarejo=@PrcVarejo,PrcAtacado=@PrcAtacado,P_Desconto=@PDesconto,"+
                       "VlrUntComissao=@VlrUntComissao,PrcEspecial=@PrcEspecial,Id_Promocao=@IdPromocao,NaoRentab=@NaoRentab,PromQtdeItem=@PromQtdeItem,PComPromocao=@PComPromocao,"+
                       "ItemPed=@ItemPed,Id_UsuLibPrc=@IdUsuLibPrc,PrcSensacional=@PrcSensacional,PrcEspDist=@PrcEspDist Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("MvVendaItens");
                sSQL = "INSERT INTO MvVendaItens (Id_Venda,Id_Item,TipoItem,Id_Produto,Qtde,VlrUnitario,VlrTotal,P_Comissao,VlrComissao,PrcCusto,PrcMinimo,PrcVarejo,PrcAtacado,P_Desconto,VlrUntComissao,Vinculado,PrcEspecial,Id_Promocao,NaoRentab,PromQtdeItem,PComPromocao,ItemPed,Id_UsuLibPrc,PrcSensacional,PrcEspDist)" +
                       " VALUES(@Id,@IdItem,@TipoItem,@IdProduto,@Qtde,@VlrUnitario,@VlrTotal,@PComissao,@VlrComissao,@PrcCusto,@PrcMinimo,@PrcVarejo,@PrcAtacado,@PDesconto,@VlrUntComissao,@Vinculado,@PrcEspecial,@IdPromocao,@NaoRentab,@PromQtdeItem,@PComPromocao,@ItemPed,@IdUsuLibPrc,@PrcSensacional,@PrcEspDist)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");             Vr_param.Add(IdVenda);
                Nm_param.Add("@IdItem");         Vr_param.Add(IdItem);
                Nm_param.Add("@TipoItem");       Vr_param.Add(TipoItem);
                Nm_param.Add("@IdProduto");      Vr_param.Add(IdProduto);
                Nm_param.Add("@Qtde");           Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@VlrUnitario");    Vr_param.Add(Controle.FloatToStr(VlrUnitario, 2));
                Nm_param.Add("@VlrTotal");       Vr_param.Add(Controle.FloatToStr(VlrTotal, 2));
                Nm_param.Add("@PComissao");      Vr_param.Add(Controle.FloatToStr(PComissao, 2));
                Nm_param.Add("@VlrComissao");    Vr_param.Add(Controle.FloatToStr(VlrComissao, 2));
                Nm_param.Add("@PDesconto");      Vr_param.Add(Controle.FloatToStr(PDesconto, 2));
                Nm_param.Add("@VlrUntComissao"); Vr_param.Add(Controle.FloatToStr(VlrUntComissao, 2));
                Nm_param.Add("@PrcCusto");       Vr_param.Add(Controle.FloatToStr(PrcCusto, 2));
                Nm_param.Add("@PrcMinimo");      Vr_param.Add(Controle.FloatToStr(PrcMinimo, 2));
                Nm_param.Add("@PrcVarejo");      Vr_param.Add(Controle.FloatToStr(PrcVarejo, 2));
                Nm_param.Add("@PrcAtacado");     Vr_param.Add(Controle.FloatToStr(PrcAtacado, 2));
                Nm_param.Add("@PrcEspecial");    Vr_param.Add(Controle.FloatToStr(PrcEspecial, 2));
                Nm_param.Add("@PrcSensacional"); Vr_param.Add(Controle.FloatToStr(PrcSensacional, 2));
                Nm_param.Add("@PrcEspDist"); Vr_param.Add(Controle.FloatToStr(PrcEspDist, 2));
                Nm_param.Add("@Vinculado");      Vr_param.Add(Vinculado);
                Nm_param.Add("@IdPromocao");     Vr_param.Add(IdPromocao);
                Nm_param.Add("@NaoRentab");      Vr_param.Add(NaoRentab);
                Nm_param.Add("@PromQtdeItem");   Vr_param.Add(PromQtdeItem);
                Nm_param.Add("@PComPromocao");   Vr_param.Add(PComPromocao);
                Nm_param.Add("@ItemPed");        Vr_param.Add(ItemPed);
                Nm_param.Add("@IdUsuLibPrc");    Vr_param.Add(IdUsuLibPrc);                       
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
                Controle.ExecutaSQL("DELETE FROM MvVendaItens WHERE Id_Item=" + IdItem.ToString().Trim());
        }        
    }
}
