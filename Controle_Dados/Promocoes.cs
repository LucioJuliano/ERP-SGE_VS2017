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
    public class Promocoes
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdProduto;
        public int IdProduto
        {
            get { return _IdProduto; }
            set { _IdProduto = value; }
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
        private int _VerifSldGeral;
        public int VerifSldGeral
        {
            get { return _VerifSldGeral; }
            set { _VerifSldGeral = value; }
        }
        private decimal _Qtde;
        public decimal Qtde
        {
            get { return _Qtde; }
            set { _Qtde = value; }
        }

        private decimal _QtdeVenda;
        public decimal QtdeVenda
        {
            get { return _QtdeVenda; }
            set { _QtdeVenda = value; }
        }

        private decimal _PrcVarejo;
        public decimal PrcVarejo
        {
            get { return _PrcVarejo; }
            set { _PrcVarejo = value; }
        }
        private decimal _PrcMinimo;
        public decimal PrcMinimo
        {
            get { return _PrcMinimo; }
            set { _PrcMinimo = value; }
        }
        private decimal _PrcAtacado;
        public decimal PrcAtacado
        {
            get { return _PrcAtacado; }
            set { _PrcAtacado = value; }
        }
        private decimal _PrcEspecial;
        public decimal PrcEspecial
        {
            get { return _PrcEspecial; }
            set { _PrcEspecial = value; }
        }

        private string _CodPromocao;
        public string CodPromocao
        {
            get { return _CodPromocao; }
            set { _CodPromocao = value; }
        }
        private int _Distribuidor;
        public int Distribuidor
        {
            get { return _Distribuidor; }
            set { _Distribuidor = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public Funcoes Controle;

        public void LerDados(int Id, string Codigo)
        {
            IdLanc        = 0;
            DtInicio      = DateTime.Now;
            DtFinal       = DateTime.Now;
            VerifSldGeral = 0;
            IdProduto     = 0;
            Qtde          = 0;
            QtdeVenda     = 0;
            PrcVarejo     = 0;
            PrcMinimo     = 0;
            PrcAtacado    = 0;
            PrcEspecial   = 0;
            CodPromocao   = "";
            Distribuidor  = 0;
            Observacao    = "";
            
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Promocoes WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc        = Id;
                    DtInicio      = DateTime.Parse(Tabela["DtInicio"].ToString());
                    DtFinal       = DateTime.Parse(Tabela["DtFinal"].ToString());
                    VerifSldGeral = int.Parse(Tabela["VerifSldGeral"].ToString());
                    IdProduto     = int.Parse(Tabela["Id_Produto"].ToString());                    
                    Qtde          = decimal.Parse(Tabela["Qtde"].ToString());
                    QtdeVenda     = decimal.Parse(Tabela["QtdeVenda"].ToString());
                    PrcVarejo     = decimal.Parse(Tabela["PrcVarejo"].ToString());
                    PrcMinimo     = decimal.Parse(Tabela["PrcMinimo"].ToString());
                    PrcAtacado    = decimal.Parse(Tabela["PrcAtacado"].ToString());
                    PrcEspecial   = decimal.Parse(Tabela["PrcEspecial"].ToString());
                    CodPromocao   = Tabela["CodPromocao"].ToString().Trim();
                    Distribuidor  = int.Parse(Tabela["Distribuidor"].ToString());
                    Observacao    = Tabela["Observacao"].ToString().Trim();
                }
            }
            if (Codigo !="")
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Promocoes WHERE CodPromocao='" + CodPromocao+"'");
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc        = Id;
                    DtInicio      = DateTime.Parse(Tabela["DtInicio"].ToString());
                    DtFinal       = DateTime.Parse(Tabela["DtFinal"].ToString());
                    VerifSldGeral = int.Parse(Tabela["VerifSldGeral"].ToString());
                    IdProduto     = int.Parse(Tabela["Id_Produto"].ToString());
                    Qtde          = decimal.Parse(Tabela["Qtde"].ToString());
                    QtdeVenda     = decimal.Parse(Tabela["QtdeVenda"].ToString());
                    PrcVarejo     = decimal.Parse(Tabela["PrcVarejo"].ToString());
                    PrcMinimo     = decimal.Parse(Tabela["PrcMinimo"].ToString());
                    PrcAtacado    = decimal.Parse(Tabela["PrcAtacado"].ToString());
                    PrcEspecial   = decimal.Parse(Tabela["PrcEspecial"].ToString());
                    CodPromocao   = Tabela["CodPromocao"].ToString().Trim();
                    Distribuidor  = int.Parse(Tabela["Distribuidor"].ToString());
                    Observacao    = Tabela["Observacao"].ToString().Trim();
                }
            }
        }

        
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdLanc > 0)
            {
                sSQL = "UPDATE Promocoes SET Id_Lanc=@Id,Id_Produto=@IdProduto,DtInicio=Convert(DateTime,@DtInicio,103),DtFinal=Convert(DateTime,@DtFinal,103),VerifSldGeral=@VerifSldGeral,Qtde=@Qtde,"+
                       "PrcVarejo=@PrcVarejo,PrcMinimo=@PrcMinimo,PrcAtacado=@PrcAtacado,PrcEspecial=@PrcEspecial,CodPromocao=@CodPromocao,Distribuidor=@Distribuidor,Observacao=@Observacao Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("PROMOCOES");
                sSQL = "INSERT INTO Promocoes (Id_Lanc,Id_Produto,DtInicio,DtFinal,VerifSldGeral,Qtde,PrcVarejo,PrcMinimo,PrcAtacado,PrcEspecial,CodPromocao,QtdeVenda,Distribuidor,Observacao) " +
                       " VALUES (@Id,@IdProduto,Convert(DateTime,@DtInicio,103),Convert(DateTime,@DtFinal,103),@VerifSldGeral,@Qtde,@PrcVarejo,@PrcMinimo,@PrcAtacado,@PrcEspecial,@CodPromocao,0,@Distribuidor,@Observacao)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");            Vr_param.Add(IdLanc);
                Nm_param.Add("@IdProduto");     Vr_param.Add(IdProduto);
                Nm_param.Add("@DtInicio");      Vr_param.Add(DtInicio.ToShortDateString());
                Nm_param.Add("@DtFinal");       Vr_param.Add(DtFinal.ToShortDateString());
                Nm_param.Add("@VerifSldGeral"); Vr_param.Add(VerifSldGeral);
                Nm_param.Add("@Qtde");          Vr_param.Add(Controle.FloatToStr(Qtde));
                Nm_param.Add("@PrcVarejo");     Vr_param.Add(Controle.FloatToStr(PrcVarejo, 2));
                Nm_param.Add("@PrcMinimo");     Vr_param.Add(Controle.FloatToStr(PrcMinimo, 2));
                Nm_param.Add("@PrcAtacado");    Vr_param.Add(Controle.FloatToStr(PrcAtacado, 2));
                Nm_param.Add("@PrcEspecial");   Vr_param.Add(Controle.FloatToStr(PrcEspecial, 2));
                Nm_param.Add("@CodPromocao");   Vr_param.Add(CodPromocao);
                Nm_param.Add("@Distribuidor");  Vr_param.Add(Distribuidor);
                Nm_param.Add("@Observacao");    Vr_param.Add(Observacao);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM Promocoes WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }

        public void AtualizaQtdeVd(string Op, decimal Qtde)
        {
            if (IdLanc > 0)
            {
                if (Op == "I")
                    Controle.ExecutaSQL("UPDATE PROMOCOES SET QTDEVENDA=QTDEVENDA + " + Controle.FloatToStr(Qtde) + " WHERE Id_Lanc=" + IdLanc.ToString().Trim());
                else
                    Controle.ExecutaSQL("UPDATE PROMOCOES SET QTDEVENDA=QTDEVENDA - " + Controle.FloatToStr(Qtde) + " WHERE Id_Lanc=" + IdLanc.ToString().Trim());
            }
        }

        public decimal VerificarQtdeVendida(int Id, string Codigo)
        {
            decimal QtdeVd = 0;
            SqlConnection ServidorRemoto;
            string conexao = "";

            if (VerifSldGeral == 0) // Retorna a Quantidade vendida localmente
                return QtdeVenda;  
            else //Verifica a Quantidade Vendida em todas as filiais
            {
                try
                {
                    SqlDataReader LerSQL = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL ORDER BY FILIAL ");
                    decimal Saldo = 0;
                    while (LerSQL.Read())
                    {
                        if (LerSQL["ServidorRemoto"].ToString().Trim() != "")
                        {
                            try
                            {
                                conexao = "Data Source=" + LerSQL["ServidorRemoto"].ToString().Trim() + LerSQL["Porta"].ToString().Trim() + "; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";
                                ServidorRemoto = new SqlConnection(conexao);
                                ServidorRemoto.Open();

                                Funcoes FuncaoServRemoto = new Funcoes();
                                FuncaoServRemoto.Conexao = ServidorRemoto;

                                Promocoes PromServRemoto = new Promocoes();
                                PromServRemoto.Controle = FuncaoServRemoto;

                                PromServRemoto.LerDados(Id,Codigo);
                                QtdeVd = QtdeVd + PromServRemoto.QtdeVenda;
                            }
                            catch
                            {
                            }
                        }
                    }
                    return QtdeVd;
                }
                catch
                {
                    return 0;
                }

            }

            return 0;
        }
    }
}

