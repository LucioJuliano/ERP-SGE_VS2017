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
    public class PromocaoProdutos
    {
        private int _IdPromocao;
        public int IdPromocao
        {
            get { return _IdPromocao; }
            set { _IdPromocao = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private DateTime _DtInicio;
        public DateTime DtInicio
        {
            get { return _DtInicio; }
            set { _DtInicio = value; }
        }
        private DateTime _DtFinal;
        public DateTime DtFinal
        {
            get { return _DtFinal; }
            set { _DtFinal = value; }
        }
        private string _Autorizado;
        public string Autorizado
        {
            get { return _Autorizado; }
            set { _Autorizado = value; }
        }
        
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }

        private int _Segunda;
        public int Segunda
        {
            get { return _Segunda; }
            set { _Segunda = value; }
        }
        private int _Terca;
        public int Terca
        {
            get { return _Terca; }
            set { _Terca = value; }
        }
        private int _Quarta;
        public int Quarta
        {
            get { return _Quarta; }
            set { _Quarta= value; }
        }
        private int _Quinta;
        public int Quinta
        {
            get { return _Quinta; }
            set { _Quinta = value; }
        }
        private int _Sexta;
        public int Sexta
        {
            get { return _Sexta; }
            set { _Sexta = value; }
        }
        private int _Sabado;
        public int Sabado
        {
            get { return _Sabado; }
            set { _Sabado = value; }
        }
        private int _Domingo;
        public int Domingo
        {
            get { return _Domingo; }
            set { _Domingo = value; }
        }
        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }
        private int _IdServidor;
        public int IdServidor
        {
            get { return _IdServidor; }
            set { _IdServidor = value; }
        }

        private int _QtdeTotal;
        public int QtdeTotal
        {
            get { return _QtdeTotal; }
            set { _QtdeTotal = value; }
        }
        private int _QtdeItem;
        public int QtdeItem
        {
            get { return _QtdeItem; }
            set { _QtdeItem = value; }
        }
        private decimal _PDesc;
        public decimal PDesc
        {
            get { return _PDesc; }
            set { _PDesc = value; }
        }        
        private int _TipoPromocao;
        public int TipoPromocao
        {
            get { return _TipoPromocao; }
            set { _TipoPromocao = value; }
        }
        private decimal _PComissao;
        public decimal PComissao
        {
            get { return _PComissao; }
            set { _PComissao = value; }
        }

        private decimal _VlrPedido;
        public decimal VlrPedido
        {
            get { return _VlrPedido; }
            set { _VlrPedido = value; }
        }

        private int _TipoCliente;
        public int TipoCliente
        {
            get { return _TipoCliente; }
            set { _TipoCliente = value; }
        }

        private int _IdProduto;
        public int IdProduto
        {
            get { return _IdProduto; }
            set { _IdProduto = value; }
        }

        private int _DescSegUnd;
        public int DescSegUnd
        {
            get { return _DescSegUnd; }
            set { _DescSegUnd = value; }
        }

        private int _QtdeSen;
        public int QtdeSen
        {
            get { return _QtdeSen; }
            set { _QtdeSen = value; }
        }
        private int _QtdeEsp;
        public int QtdeEsp
        {
            get { return _QtdeEsp; }
            set { _QtdeEsp = value; }
        }
        private int _QtdeVar;
        public int QtdeVar
        {
            get { return _QtdeVar; }
            set { _QtdeVar = value; }
        }
        private int _QtdeMin;
        public int QtdeMin
        {
            get { return _QtdeMin; }
            set { _QtdeMin = value; }
        }
        private int _QtdeAta;
        public int QtdeAta
        {
            get { return _QtdeAta; }
            set { _QtdeAta = value; }
        }

        private int _PorUsuario;
        public int PorUsuario
        {
            get { return _PorUsuario; }
            set { _PorUsuario = value; }
        }




        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdPromocao   = 0;
            Descricao    = "";
            DtInicio     = DateTime.Now;
            DtFinal      = DateTime.Now;
            Autorizado   = "";            
            Observacao   = "";
            Ativo        = 0;
            Segunda      = 0;
            Terca        = 0;
            Quarta       = 0;
            Quinta       = 0;
            Sexta        = 0;
            Sabado       = 0;
            Domingo      = 0;
            IdServidor   = 0;
            QtdeTotal    = 0;
            QtdeItem     = 0;
            PDesc        = 0;
            TipoPromocao = 0;
            PComissao    = 0;
            VlrPedido    = 0;
            TipoCliente  = 0;
            IdProduto    = 0;
            DescSegUnd   = 0;
            QtdeSen      = 0;
            QtdeEsp      = 0;
            QtdeVar      = 0;
            QtdeMin      = 0;
            QtdeAta      = 0;
            PorUsuario   = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PromocaoProdutos WHERE Id_Promocao=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdPromocao   = Id;
                    Descricao    = Tabela["Descricao"].ToString().Trim();
                    DtInicio     = DateTime.Parse(Tabela["DtInicio"].ToString());
                    DtFinal      = DateTime.Parse(Tabela["DtFinal"].ToString());
                    Autorizado   = Tabela["Autorizado"].ToString().Trim();
                    Observacao   = Tabela["Observacao"].ToString().Trim();
                    Ativo        = int.Parse(Tabela["Ativo"].ToString());
                    Segunda      = int.Parse(Tabela["Segunda"].ToString());
                    Terca        = int.Parse(Tabela["Terca"].ToString());
                    Quarta       = int.Parse(Tabela["Quarta"].ToString());
                    Quinta       = int.Parse(Tabela["Quinta"].ToString());
                    Sexta        = int.Parse(Tabela["Sexta"].ToString());
                    Sabado       = int.Parse(Tabela["Sabado"].ToString());
                    Domingo      = int.Parse(Tabela["Domingo"].ToString());
                    IdServidor   = int.Parse(Tabela["Id_Servidor"].ToString());
                    QtdeTotal    = int.Parse(Tabela["QtdeTotal"].ToString());
                    QtdeItem     = int.Parse(Tabela["QtdeItem"].ToString());
                    PDesc        = decimal.Parse(Tabela["PDesc"].ToString());
                    PComissao    = decimal.Parse(Tabela["PComissao"].ToString());
                    TipoPromocao = int.Parse(Tabela["TipoPromocao"].ToString());
                    VlrPedido    = decimal.Parse(Tabela["VlrPedido"].ToString());
                    TipoCliente  = int.Parse(Tabela["TipoCliente"].ToString());
                    IdProduto    = int.Parse(Tabela["Id_Produto"].ToString());
                    DescSegUnd   = int.Parse(Tabela["DescSegUnd"].ToString());
                    QtdeSen      = int.Parse(Tabela["QtdeSen"].ToString());                    
                    QtdeEsp      = int.Parse(Tabela["QtdeEsp"].ToString());
                    QtdeVar      = int.Parse(Tabela["QtdeVar"].ToString());
                    QtdeMin      = int.Parse(Tabela["QtdeMin"].ToString());
                    QtdeAta      = int.Parse(Tabela["QtdeAta"].ToString());
                    PorUsuario   = int.Parse(Tabela["PorUsuario"].ToString());

                }
            }
        }


        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdPromocao > 0)
            {
                sSQL = "UPDATE PromocaoProdutos SET Id_Promocao=@Id,Descricao=@Descricao,DtInicio=Convert(DateTime,@DtInicio,103),DtFinal=Convert(DateTime,@DtFinal,103)," +
                       "Autorizado=@Autorizado,Observacao=@Observacao,Ativo=@Ativo,Segunda=@Segunda,Terca=@Terca,Quarta=@Quarta,Quinta=@Quinta,Sexta=@Sexta,Sabado=@Sabado,"+
                       "Domingo=@Domingo,Id_Servidor=@IdServidor,QtdeTotal=@QtdeTotal,QtdeItem=@QtdeItem,PDesc=@PDesc,TipoPromocao=@TipoPromocao,PComissao=@PComissao,"+
                       "VlrPedido=@VlrPedido,TipoCliente=@TipoCliente,Id_Produto=@IdProduto,DescSegUnd=@DescSegUnd,QtdeSen=@QtdeSen,QtdeEsp=@QtdeEsp,QtdeVar=@QtdeVar,QtdeMin=@QtdeMin,QtdeAta=@QtdeAta,PorUsuario=@PorUsuario Where Id_Promocao=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdPromocao);
            }
            else
            {
                IdPromocao = Controle.ProximoID("PROMOCAOPRODUTOS");
                sSQL = "INSERT INTO PromocaoProdutos (Id_Promocao,Descricao,DtInicio,DtFinal,Autorizado,Observacao,Ativo,Segunda,Terca,Quarta,Quinta,Sexta,Sabado,Domingo,Id_Servidor,"+
                       "QtdeTotal,QtdeItem,PDesc,TipoPromocao,PComissao,VlrPedido,TipoCliente,Id_Produto,DescSegUnd,QtdeSen,QtdeEsp,QtdeVar,QtdeMin,QtdeAta,PorUsuario) " +
                       " VALUES (@Id,@Descricao,Convert(DateTime,@DtInicio,103),Convert(DateTime,@DtFinal,103),@Autorizado,@Observacao,@Ativo,@Segunda,@Terca,@Quarta,@Quinta,@Sexta,@Sabado,@Domingo,@IdServidor,"+
                       "@QtdeTotal,@QtdeItem,@PDesc,@TipoPromocao,@PComissao,@VlrPedido,@TipoCliente,@IdProduto,@DescSegUnd,@QtdeSen,@QtdeEsp,@QtdeVar,@QtdeMin,@QtdeAta,@PorUsuario)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");           Vr_param.Add(IdPromocao);
                Nm_param.Add("@Descricao");    Vr_param.Add(Descricao);
                Nm_param.Add("@DtInicio");     Vr_param.Add(DtInicio.ToShortDateString());
                Nm_param.Add("@DtFinal");      Vr_param.Add(DtFinal.ToShortDateString());
                Nm_param.Add("@Autorizado");   Vr_param.Add(Autorizado);
                Nm_param.Add("@Observacao");   Vr_param.Add(Observacao);
                Nm_param.Add("@Ativo");        Vr_param.Add(Ativo);
                Nm_param.Add("@Segunda");      Vr_param.Add(Segunda);
                Nm_param.Add("@Terca");        Vr_param.Add(Terca);
                Nm_param.Add("@Quarta");       Vr_param.Add(Quarta);
                Nm_param.Add("@Quinta");       Vr_param.Add(Quinta);
                Nm_param.Add("@Sexta");        Vr_param.Add(Sexta);
                Nm_param.Add("@Sabado");       Vr_param.Add(Sabado);
                Nm_param.Add("@Domingo");      Vr_param.Add(Domingo);
                Nm_param.Add("@IdServidor");   Vr_param.Add(IdServidor);
                Nm_param.Add("@QtdeTotal");    Vr_param.Add(QtdeTotal);
                Nm_param.Add("@QtdeItem");     Vr_param.Add(QtdeItem);
                Nm_param.Add("@PDesc");        Vr_param.Add(PDesc);
                Nm_param.Add("@TipoPromocao"); Vr_param.Add(TipoPromocao);
                Nm_param.Add("@PComissao");    Vr_param.Add(Controle.FloatToStr(PComissao, 2));
                Nm_param.Add("@VlrPedido");    Vr_param.Add(Controle.FloatToStr(VlrPedido, 2));
                Nm_param.Add("@TipoCliente");  Vr_param.Add(TipoCliente);
                Nm_param.Add("@IdProduto");    Vr_param.Add(IdProduto);
                Nm_param.Add("@DescSegUnd");   Vr_param.Add(DescSegUnd);
                Nm_param.Add("@QtdeSen");      Vr_param.Add(QtdeSen);
                Nm_param.Add("@QtdeEsp");      Vr_param.Add(QtdeEsp);
                Nm_param.Add("@QtdeVar");      Vr_param.Add(QtdeVar);
                Nm_param.Add("@QtdeMin");      Vr_param.Add(QtdeMin);
                Nm_param.Add("@QtdeAta");      Vr_param.Add(QtdeAta);
                Nm_param.Add("@PorUsuario");   Vr_param.Add(PorUsuario);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdPromocao > 0)
                Controle.ExecutaSQL("DELETE FROM PromocaoProdutos WHERE Id_Promocao=" + IdPromocao.ToString().Trim());
        }

        public int VerificarPromocaoServidor(int Id)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM PromocaoProdutos WHERE Id_Servidor=" + Id.ToString().Trim());
            if (Tabela.HasRows)
            {
                Tabela.Read();
                return int.Parse(Tabela["Id_Promocao"].ToString());
            }
            else
                return 0;
        }
    }
}

