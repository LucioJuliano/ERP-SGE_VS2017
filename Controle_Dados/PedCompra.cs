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
    public class PedCompra
    {
        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private string _NumPedido;
        public string NumPedido
        {
            get { return _NumPedido; }
            set { _NumPedido = value; }
        }
        private string _Vendedor;
        public string Vendedor
        {
            get { return _Vendedor; }
            set { _Vendedor = value; }
        }
        private string _Fone;
        public string Fone
        {
            get { return _Fone; }
            set { _Fone = value; }
        }
        private DateTime _PrevEntrega;
        public DateTime PrevEntrega
        {
            get { return _PrevEntrega; }
            set { _PrevEntrega = value; }
        }
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private DateTime _DataRecebimento;
        public DateTime DataRecebimento
        {
            get { return _DataRecebimento; }
            set { _DataRecebimento = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private decimal _VlrSubTotal;
        public decimal VlrSubTotal
        {
            get { return _VlrSubTotal; }
            set { _VlrSubTotal = value; }
        }

        private decimal _VlrTotal;
        public decimal VlrTotal
        {
            get { return _VlrTotal; }
            set { _VlrTotal = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private string _FormaPgto;
        public string FormaPgto
        {
            get { return _FormaPgto; }
            set { _FormaPgto = value; }
        }
        private int _IdTransp;
        public int IdTransp
        {
            get { return _IdTransp; }
            set { _IdTransp = value; }
        }

        private string _NumNFe;
        public string NumNFe
        {
            get { return _NumNFe; }
            set { _NumNFe = value; }
        }

        private decimal _VlrIpi;
        public decimal VlrIpi
        {
            get { return _VlrIpi; }
            set { _VlrIpi = value; }
        }
        private decimal _VlrIcms;
        public decimal VlrIcms
        {
            get { return _VlrIcms; }
            set { _VlrIcms = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdDocumento = 0;
            Data        = DateTime.Now;
            IdPessoa    = 0;
            NumPedido   = "";
            Vendedor    = "";
            Fone        = "";
            PrevEntrega = DateTime.Now;
            IdUsuario   = 0;
            Observacao  = "";
            VlrTotal    = 0;
            IdFilial    = 0;
            Status      = 0;
            FormaPgto   = "";
            IdTransp    = 0;
            NumNFe      = "";
            VlrIpi      = 0;
            VlrIcms     = 0;
            VlrSubTotal = 0;


            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PedCompra WHERE Id_Documento=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdDocumento = Id;
                    Data        = DateTime.Parse(Tabela["Data"].ToString());
                    IdPessoa    = int.Parse(Tabela["Id_Pessoa"].ToString());
                    IdFilial    = int.Parse(Tabela["Id_Filial"].ToString());
                    NumPedido   = Tabela["NumPedido"].ToString().Trim();
                    Vendedor    = Tabela["Vendedor"].ToString().Trim();
                    Fone        = Tabela["Fone"].ToString().Trim();                    
                    PrevEntrega = DateTime.Parse(Tabela["PrevEntrega"].ToString());
                    IdUsuario   = int.Parse(Tabela["Id_Usuario"].ToString());
                    Observacao  = Tabela["Observacao"].ToString().Trim();
                    VlrTotal    = decimal.Parse(Tabela["VlrTotal"].ToString());
                    Status      = int.Parse(Tabela["Status"].ToString());
                    FormaPgto   = Tabela["FormaPgto"].ToString().Trim();
                    IdTransp    = int.Parse(Tabela["Id_Transportadora"].ToString());
                    NumNFe      = Tabela["NumNFe"].ToString().Trim();
                    if (Tabela["DataRecebimento"].ToString() != "")
                        DataRecebimento = DateTime.Parse(Tabela["DataRecebimento"].ToString());
                    if (Tabela["VlrIpi"].ToString() != "")
                        VlrIpi = decimal.Parse(Tabela["VlrIpi"].ToString());
                    if (Tabela["VlrIcms"].ToString() != "")
                        VlrIcms = decimal.Parse(Tabela["VlrIcms"].ToString());
                    if (Tabela["VlrSubTotal"].ToString() != "")
                        VlrSubTotal = decimal.Parse(Tabela["VlrSubTotal"].ToString());
                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdDocumento > 0)
            {
                sSQL = "UPDATE PedCompra SET Id_Documento=@Id,Id_Pessoa=@IdPessoa,Data=Convert(DateTime,@Data,103),NumPedido=@NumPedido,Vendedor=@Vendedor,Fone=@Fone,PrevEntrega=Convert(DateTime,@PrevEntrega,103),Id_Usuario=@IdUsuario,"+
                       "Observacao=@Obs,VlrTotal=@VlrTotal,Status=@Status,Id_Filial=@IdFilial,FormaPgto=@FormaPgto,Id_Transportadora=@IdTransp,NumNFe=@NumNFe,VlrIpi=@VlrIpi,VlrIcms=@VlrIcms,VlrSubTotal=@VlrSubTotal Where Id_Documento=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdDocumento);
            }
            else
            {
                IdDocumento = Controle.ProximoID("PedCompra");
                sSQL = "INSERT INTO PedCompra (Id_Documento,Id_Pessoa,Data,NumPedido,Vendedor,Fone,PrevEntrega,Id_Usuario,Observacao,VlrTotal,Status,Id_Filial,FormaPgto,Id_Transportadora,NumNFe,VlrIpi,VlrIcms,VlrSubTotal)" +
                        "VALUES (@Id,@IdPessoa,Convert(DateTime,@Data,103),@NumPedido,@Vendedor,@Fone,Convert(DateTime,@PrevEntrega,103),@IdUsuario,@Obs,@VlrTotal,@Status,@IdFilial,@FormaPgto,@IdTransp,@NumNFe,@VlrIpi,@VlrIcms,@VlrSubTotal)";
                NumPedido= string.Format("{0:D8}", Controle.ProximoID("NumPedCompra")); 
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");          Vr_param.Add(IdDocumento);
                Nm_param.Add("@IdPessoa");    Vr_param.Add(IdPessoa);
                Nm_param.Add("@Data");        Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@NumPedido");   Vr_param.Add(NumPedido);
                Nm_param.Add("@Vendedor");    Vr_param.Add(Vendedor);
                Nm_param.Add("@Fone");        Vr_param.Add(Fone);
                Nm_param.Add("@PrevEntrega"); Vr_param.Add(PrevEntrega.ToShortDateString());
                Nm_param.Add("@IdUsuario");   Vr_param.Add(IdUsuario);
                Nm_param.Add("@Obs");         Vr_param.Add(Observacao);
                Nm_param.Add("@VlrTotal");    Vr_param.Add(Controle.FloatToStr(VlrTotal,2));
                Nm_param.Add("@Status");      Vr_param.Add(Status);
                Nm_param.Add("@IdFilial");    Vr_param.Add(IdFilial);
                Nm_param.Add("@FormaPgto");   Vr_param.Add(FormaPgto);
                Nm_param.Add("@IdTransp");    Vr_param.Add(IdTransp);
                Nm_param.Add("@NumNFe");      Vr_param.Add(NumNFe);
                Nm_param.Add("@VlrIpi");      Vr_param.Add(Controle.FloatToStr(VlrIpi, 2));
                Nm_param.Add("@VlrIcms");     Vr_param.Add(Controle.FloatToStr(VlrIcms, 2));
                Nm_param.Add("@VlrSubTotal"); Vr_param.Add(Controle.FloatToStr(VlrSubTotal, 2));
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);                
            }
        }
        public void Excluir()
        {
            if (IdDocumento > 0)
            {
                Controle.ExecutaSQL("DELETE FROM PedCompra        WHERE Id_Documento=" + IdDocumento.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM PedCompraItens   WHERE Id_Documento=" + IdDocumento.ToString().Trim());                
            }
        }

        public void Concluir()
        {
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            string sSQL = "";
            if (Status == 0)
            {
                sSQL = "UPDATE PedCompra Set Status=1 Where Id_Documento=" + IdDocumento.ToString();
                Controle.ExecutaSQL(sSQL);
                Status = 1;
            }
            else
            {
                sSQL = "UPDATE PedCompra Set Status=2,DataRecebimento=Convert(DateTime,@Dt,103) Where Id_Documento=" + IdDocumento.ToString();
                Nm_param.Add("@Dt"); Vr_param.Add(DateTime.Now.ToShortDateString());
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Cancelar()
        {
            Controle.ExecutaSQL("UPDATE PedCompra Set Status=0,DataRecebimento=Null Where Id_Documento=" + IdDocumento.ToString());
            Controle.ExecutaSQL("UPDATE PedCompraItens Set QtdeRecebida=0 Where Id_Documento=" + IdDocumento.ToString());            
            Status = 0;            
        }
        public string SqlRelatorio(int IdDoc)
        {
            string TxtSql = "SELECT T1.Id_Documento, T1.Data, T1.NumPedido, T1.Vendedor, T1.Fone, T1.PrevEntrega, T1.DataRecebimento, T1.Observacao, T1.VlrTotal, T1.Status, " +
                            " CASE T1.Status WHEN 0 THEN 'Não Confirmado' WHEN 1 THEN 'Em Espera' WHEN 2 THEN 'Recebido' END AS Status, T2.Id_Item, T2.Id_Produto, T2.Qtde, T2.QtdeRecebida, T2.VlrUnitario," +
                            " T2.VlrTotal AS TOTALITEM, T3.Referencia,CASE T2.ID_PRODUTO WHEN 0 THEN T2.Descricao ELSE T3.DESCRICAO END AS DESCRICAO, T1.Id_Pessoa, T4.RazaoSocial AS FORNECEDOR,  T4.Cnpj AS CNPJFORN,T4.Endereco AS ENDFORN, T4.Numero AS NUMFORN, T4.Complemento AS COMPLFORN,T4.Bairro AS BAIRFORN," +
                            " T4.Cidade AS CIDFORN, T7.Sigla AS UFFORN, T5.FANTASIA AS Filial, T5.Cnpj AS CNPJFILIAL ,T5.Endereco AS ENDFILIAL, T5.Numero AS NUMFILIAL, T5.Complemento AS COMPLFILIAL, T5.Bairro AS BAIRFILIAL, T5.Cidade AS CIDFILIAL, " +
                            " T8.Sigla AS UFFILIAL, T6.Imagem,T9.RazaoSocial as Transportadora,T2.Pipi, T2.PIcms, T1.VlrIpi, T1.VlrIcms, T1.VlrSubTotal, T1.FormaPgto FROM PedCompra AS T1" +
                            " LEFT OUTER JOIN PedCompraItens AS T2 ON T2.Id_Documento = T1.Id_Documento" +
                            " LEFT OUTER JOIN Produtos AS T3 ON T3.Id_Produto = T2.Id_Produto" +
                            " LEFT OUTER JOIN Pessoas AS T4 ON T4.Id_Pessoa = T1.Id_Pessoa" +
                            " LEFT OUTER JOIN Empresa_Filial AS T5 ON T5.Id_Filial = T1.Id_Filial" +
                            " LEFT OUTER JOIN TabImagens AS T6 ON T6.Id_Chave = T1.Id_Filial" +
                            " LEFT OUTER JOIN Estados AS T7 ON T7.Id_UF = T4.Id_UF" +
                            " LEFT OUTER JOIN Estados AS T8 ON T8.Id_UF = T5.Id_UF" +
                            " LEFT OUTER JOIN Transportadoras AS T9 ON T9.Id_Transportadora = T1.Id_Transportadora" +
                            " Where T1.Id_Documento=" + IdDoc.ToString();
            return TxtSql;
        }      

    }
}
