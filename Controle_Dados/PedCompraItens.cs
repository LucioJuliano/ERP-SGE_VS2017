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
    public class PedCompraItens
    {
        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
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
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private decimal _Qtde;
        public decimal Qtde
        {
            get { return _Qtde; }
            set { _Qtde = value; }
        }
        private decimal _QtdeRecebida;
        public decimal QtdeRecebida
        {
            get { return _QtdeRecebida; }
            set { _QtdeRecebida = value; }
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
        private decimal _PIpi;
        public decimal PIpi
        {
            get { return _PIpi; }
            set { _PIpi = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {

            IdItem       = 0;
            IdDocumento  = 0;
            IdProduto    = 0;
            Descricao    = "";
            Qtde         = 0;
            QtdeRecebida = 0;
            VlrUnitario  = 0;
            VlrTotal     = 0;
            PIcms        = 0;
            PIpi         = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PedCompraItens WHERE Id_Item=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdItem       = Id;
                    IdDocumento  = int.Parse(Tabela["Id_Documento"].ToString());                    
                    IdProduto    = int.Parse(Tabela["Id_Produto"].ToString());
                    Descricao    = Tabela["Id_Produto"].ToString().Trim();
                    Qtde         = decimal.Parse(Tabela["Qtde"].ToString());
                    QtdeRecebida = decimal.Parse(Tabela["QtdeRecebida"].ToString());
                    VlrUnitario  = decimal.Parse(Tabela["VlrUnitario"].ToString());
                    VlrTotal     = decimal.Parse(Tabela["VlrTotal"].ToString());
                    PIcms        = decimal.Parse(Tabela["PIcms"].ToString());
                    PIpi         = decimal.Parse(Tabela["PIpi"].ToString());
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
                sSQL = "UPDATE PedCompraItens SET Id_Documento=@Id,Id_Item=@IdItem,Id_Produto=@IdProduto,Descricao=@Descricao,Qtde=@Qtde,QtdeRecebida=@QtdeRecebida,VlrUnitario=@VlrUnitario,VlrTotal=@VlrTotal,PIcms=@PIcms,PIpi=@PIpi Where Id_Item=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdItem);
            }
            else
            {
                IdItem = Controle.ProximoID("PedCompraItens");
                sSQL = "INSERT INTO PedCompraItens (Id_Documento,Id_Item,Id_Produto,Descricao,Qtde,QtdeRecebida,VlrUnitario,VlrTotal,PIcms,PIpi) VALUES(@Id,@IdItem,@IdProduto,@Descricao,@Qtde,@QtdeRecebida,@VlrUnitario,@VlrTotal,@PIcms,@PIpi)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");           Vr_param.Add(IdDocumento);
                Nm_param.Add("@IdItem");       Vr_param.Add(IdItem);
                Nm_param.Add("@IdProduto");    Vr_param.Add(IdProduto);
                Nm_param.Add("@Descricao");    Vr_param.Add(Descricao);
                Nm_param.Add("@Qtde");         Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@QtdeRecebida"); Vr_param.Add(Controle.FloatToStr(QtdeRecebida));
                Nm_param.Add("@VlrUnitario");  Vr_param.Add(Controle.FloatToStr(VlrUnitario,7));
                Nm_param.Add("@VlrTotal");     Vr_param.Add(Controle.FloatToStr(VlrUnitario * Qtde,2));
                Nm_param.Add("@PIcms");        Vr_param.Add(Controle.FloatToStr(PIcms, 2));
                Nm_param.Add("@PIpi");         Vr_param.Add(Controle.FloatToStr(PIpi, 2));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdItem > 0)
                Controle.ExecutaSQL("DELETE FROM PedCompraItens WHERE Id_Item=" + IdItem.ToString().Trim());
        }
    }
}
