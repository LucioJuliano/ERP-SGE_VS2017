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
    public class Produtos
    {
        private int _IdProduto;
        public int IdProduto
        {
            get { return _IdProduto; }
            set { _IdProduto = value; }
        }
        private string _Referencia;
        public string Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private string _DescResumida;
        public string DescResumida
        {
            get { return _DescResumida; }
            set { _DescResumida = value; }
        }
        private int _IdGrupo;
        public int IdGrupo
        {
            get { return _IdGrupo; }
            set { _IdGrupo = value; }
        }
        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }
        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private string _RefFornecedor;
        public string RefFornecedor
        {
            get { return _RefFornecedor; }
            set { _RefFornecedor = value; }
        }
        private string _CodBarra;
        public string CodBarra
        {
            get { return _CodBarra; }
            set { _CodBarra = value; }
        }
        private decimal _IcmsIss;
        public decimal IcmsIss
        {
            get { return _IcmsIss; }
            set { _IcmsIss = value; }
        }
        private decimal _Reducao;
        public decimal Reducao
        {
            get { return _Reducao; }
            set { _Reducao = value; }
        }
        private decimal _Ipi;
        public decimal Ipi
        {
            get { return _Ipi; }
            set { _Ipi = value; }
        }
        private int _SitTributaria;
        public int SitTributaria
        {
            get { return _SitTributaria; }
            set { _SitTributaria = value; }
        }
        private string _CodSefaz;
        public string CodSefaz
        {
            get { return _CodSefaz; }
            set { _CodSefaz = value; }
        }
        private decimal _EstMinimo;
        public decimal EstMinimo
        {
            get { return _EstMinimo; }
            set { _EstMinimo = value; }
        }
        private decimal _EstMaximo;
        public decimal EstMaximo
        {
            get { return _EstMaximo; }
            set { _EstMaximo = value; }
        }
        private decimal _PesoLiquido;
        public decimal PesoLiquido
        {
            get { return _PesoLiquido; }
            set { _PesoLiquido = value; }
        }
        private decimal _PesoBruto;
        public decimal PesoBruto
        {
            get { return _PesoBruto; }
            set { _PesoBruto = value; }
        }
        private string _Unidade;
        public string Unidade
        {
            get { return _Unidade; }
            set { _Unidade = value; }
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
        private decimal _PrcSensacional;
        public decimal PrcSensacional
        {
            get { return _PrcSensacional; }
            set { _PrcSensacional = value; }
        }
        private decimal _PrcAtacado;
        public decimal PrcAtacado
        {
            get { return _PrcAtacado; }
            set { _PrcAtacado = value; }
        }

        private decimal _PrcEspDist;
        public decimal PrcEspDist
        {
            get { return _PrcEspDist; }
            set { _PrcEspDist = value; }
        }
        private decimal _UltPrcCompra;
        public decimal UltPrcCompra
        {
            get { return _UltPrcCompra; }
            set { _UltPrcCompra = value; }
        }
        private decimal _UltPrcCompra2;
        public decimal UltPrcCompra2
        {
            get { return _UltPrcCompra2; }
            set { _UltPrcCompra2 = value; }
        }
        private decimal _Custo;
        public decimal Custo
        {
            get { return _Custo; }
            set { _Custo = value; }
        }
        private DateTime _DtCadastro;
        public DateTime DtCadastro
        {
            get { return _DtCadastro; }
            set { _DtCadastro = value; }
        }
        private DateTime _DtUltCompra;
        public DateTime DtUltCompra
        {
            get { return _DtUltCompra; }
            set { _DtUltCompra = value; }
        }
        private DateTime _DtUltVenda;
        public DateTime DtUltVenda
        {
            get { return _DtUltVenda; }
            set { _DtUltVenda = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private string _Composicao;
        public string Composicao
        {
            get { return _Composicao; }
            set { _Composicao = value; }
        }
        private decimal _SaldoEstoque;
        public decimal SaldoEstoque
        {
            get { return _SaldoEstoque; }
            set { _SaldoEstoque = value; }
        }
        private int _ProdutoKit;
        public int ProdutoKit
        {
            get { return _ProdutoKit; }
            set { _ProdutoKit = value; }
        }
        private int _IdGenero;
        public int IdGenero
        {
            get { return _IdGenero; }
            set { _IdGenero = value; }
        }
        private DateTime _DtAlteracao;
        public DateTime DtAlteracao
        {
            get { return _DtAlteracao; }
            set { _DtAlteracao = value; }
        }
        private int _EnviarEmail;
        public int EnviarEmail
        {
            get { return _EnviarEmail; }
            set { _EnviarEmail = value; }
        }
        private int _CreditoIPI;
        public int CreditoIPI
        {
            get { return _CreditoIPI; }
            set { _CreditoIPI = value; }
        }
        private string _NCM;
        public string NCM
        {
            get { return _NCM; }
            set { _NCM = value; }
        }
        private int _IdCfopVD; // Cfop de Venda dentro do Estado        
        public int IdCfopVD
        {
            get { return _IdCfopVD; }
            set { _IdCfopVD = value; }
        }
        private int _IdCfopVF; // Cfop de Venda dentro do Estado        
        public int IdCfopVF
        {
            get { return _IdCfopVF; }
            set { _IdCfopVF = value; }
        }
        private int _IdCfopED; // Cfop de Venda dentro do Estado        
        public int IdCfopED
        {
            get { return _IdCfopED; }
            set { _IdCfopED = value; }
        }
        private int _IdCfopEF; // Cfop de Venda dentro do Estado        
        public int IdCfopEF
        {
            get { return _IdCfopEF; }
            set { _IdCfopEF = value; }
        }

        private int _QtdeCaixa; 
        public int QtdeCaixa
        {
            get { return _QtdeCaixa; }
            set { _QtdeCaixa = value; }
        }
        private int _QtdeCxDist; 
        public int QtdeCxDist
        {
            get { return _QtdeCxDist; }
            set { _QtdeCxDist = value; }
        }
        private decimal _IcmsIss2;
        public decimal IcmsIss2
        {
            get { return _IcmsIss2; }
            set { _IcmsIss2 = value; }
        }        
        private int _SitTrib2;
        public int SitTrib2
        {
            get { return _SitTrib2; }
            set { _SitTrib2 = value; }
        }
        private int _QtdeUnd;
        public int QtdeUnd
        {
            get { return _QtdeUnd; }
            set { _QtdeUnd = value; }
        }
        private int _IdReducao; // Cfop de Venda dentro do Estado        
        public int IdReducao
        {
            get { return _IdReducao; }
            set { _IdReducao = value; }
        }
        private int _IdPromocao; // Cfop de Venda dentro do Estado        
        public int IdPromocao
        {
            get { return _IdPromocao; }
            set { _IdPromocao = value; }
        }
        private DateTime _DtAltPrc; // Cfop de Venda dentro do Estado        
        public DateTime DtAltPrc
        {
            get { return _DtAltPrc; }
            set { _DtAltPrc = value; }
        }
        private int _Pontos; // Cfop de Venda dentro do Estado        
        public int Pontos
        {
            get { return _Pontos; }
            set { _Pontos = value; }
        }
        private int _LocEstRua; // Cfop de Venda dentro do Estado        
        public int LocEstRua
        {
            get { return _LocEstRua; }
            set { _LocEstRua = value; }
        }        
        private string  _Palete; // Cfop de Venda dentro do Estado        
        public string Palete
        {
            get { return _Palete; }
            set { _Palete = value; }
        }
        private string _DetProduto; // Cfop de Venda dentro do Estado        
        public string DetProduto
        {
            get { return _DetProduto; }
            set { _DetProduto = value; }
        }
        private string _Foto;
        public string Foto
        {
            get { return _Foto; }
            set { _Foto = value; }
        }

        /*private int _CstIcms;
        public int CstIcms
        {
            get { return _CstIcms; }
            set { _CstIcms = value; }
        }
        private int _CstIpi;
        public int CstIpi
        {
            get { return _CstIpi; }
            set { _CstIpi = value; }
        }
        private int _CstCofins;
        public int CstCofins
        {
            get { return _CstCofins; }
            set { _CstCofins = value; }
        }
        private int _CstPis;
        public int CstPis
        {
            get { return _CstPis; }
            set { _CstPis = value; }
        }*/

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdProduto     = 0;
            Referencia    = "";
            Descricao     = "";
            DescResumida  = "";
            IdGrupo       = 0;
            Ativo         = 0;
            Tipo          = 0;
            RefFornecedor = "";
            CodBarra      = "";
            IcmsIss       = 17;
            Reducao       = 0;
            Ipi           = 0;
            SitTributaria = 0;
            IcmsIss2      = 17;            
            SitTrib2      = 0;
            CodSefaz      = "";
            EstMinimo     = 0;
            EstMaximo     = 0;
            PesoBruto     = 0;
            PesoLiquido   = 0;
            Unidade       = "";
            PrcSensacional = 0;
            PrcEspecial   = 0;
            PrcMinimo     = 0;
            PrcVarejo     = 0;
            PrcAtacado    = 0;
            PrcEspDist    = 0;
            UltPrcCompra  = 0;
            UltPrcCompra2 = 0;
            Custo         = 0;
            DtCadastro    = DateTime.Now;
            DtAlteracao   = DateTime.Now;
            DtAltPrc      = DateTime.Now;
            Observacao    = "";
            Composicao    = "";
            SaldoEstoque  = 0;
            ProdutoKit    = 0;
            IdGenero      = 0;
            EnviarEmail   = 0;
            CreditoIPI    = 0;
            NCM           = "";
            IdCfopVD      = 0;
            IdCfopVF      = 0;
            QtdeCaixa     = 0;
            QtdeCxDist    = 0;
            IdReducao     = 0;
            QtdeUnd       = 0;
            IdPromocao    = 0;
            Pontos        = 0;
            LocEstRua     = 0;
            Palete        = "";
            DetProduto    = "";
            Foto          = "";
            IdCfopED      = 0;
            IdCfopEF      = 0;

            /*CstIcms = 0;
            CstIpi = 0;
            CstCofins = 0;
            CstPis = 0;*/

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Produtos WHERE Id_Produto=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdProduto     = Id;
                    Referencia    = Tabela["Referencia"].ToString().Trim();
                    Descricao     = Tabela["Descricao"].ToString().Trim();
                    DescResumida  = Tabela["DescResumida"].ToString().Trim();
                    IdGrupo       = int.Parse(Tabela["Id_Grupo"].ToString());
                    Ativo         = int.Parse(Tabela["Ativo"].ToString());
                    Tipo          = int.Parse(Tabela["Tipo"].ToString());
                    RefFornecedor = Tabela["RefFornecedor"].ToString().Trim();
                    CodBarra      = Tabela["CodBarra"].ToString().Trim();
                    IcmsIss       = decimal.Parse(Tabela["IcmsIss"].ToString());
                    Reducao       = decimal.Parse(Tabela["Reducao"].ToString());
                    Ipi           = decimal.Parse(Tabela["Ipi"].ToString());
                    SitTributaria = int.Parse(Tabela["SitTributaria"].ToString());
                    CodSefaz      = Tabela["CodSefaz"].ToString().Trim();
                    EstMinimo     = decimal.Parse(Tabela["EstMinimo"].ToString());
                    EstMaximo     = decimal.Parse(Tabela["EstMaximo"].ToString());
                    PesoBruto     = decimal.Parse(Tabela["PesoBruto"].ToString());
                    PesoLiquido   = decimal.Parse(Tabela["PesoLiquido"].ToString());
                    Unidade       = Tabela["Unidade"].ToString();
                    PrcSensacional = decimal.Parse(Tabela["PrcSensacional"].ToString());
                    PrcMinimo     = decimal.Parse(Tabela["PrcMinimo"].ToString());
                    PrcVarejo     = decimal.Parse(Tabela["PrcVarejo"].ToString());
                    PrcAtacado    = decimal.Parse(Tabela["PrcAtacado"].ToString());
                    UltPrcCompra  = decimal.Parse(Tabela["UltPrcCompra"].ToString());
                    Custo         = decimal.Parse(Tabela["Custo"].ToString());
                    DtCadastro    = DateTime.Parse(Tabela["DtCadastro"].ToString());
                    Observacao    = Tabela["Observacao"].ToString();
                    Composicao    = Tabela["Composicao"].ToString();
                    SaldoEstoque  = decimal.Parse(Tabela["SaldoEstoque"].ToString());
                    ProdutoKit    = int.Parse(Tabela["ProdutoKit"].ToString());
                    IdGenero      = int.Parse(Tabela["Id_Genero"].ToString());
                    EnviarEmail   = int.Parse(Tabela["EnviarEmail"].ToString());
                    NCM           = Tabela["NCM"].ToString().Trim();
                    Palete        = Tabela["PALETE"].ToString().Trim();
                    DetProduto    = Tabela["DetProduto"].ToString().Trim();
                    Foto          = Tabela["Foto"].ToString().Trim();

                    if (Tabela["CreditoIPI"].ToString() != "")
                        CreditoIPI = int.Parse(Tabela["CreditoIPI"].ToString());
                    if (Tabela["IcmsIss2"].ToString() != "")
                        IcmsIss2 = decimal.Parse(Tabela["IcmsIss2"].ToString());                    
                    if (Tabela["SitTrib2"].ToString() != "")
                        SitTrib2 = int.Parse(Tabela["SitTrib2"].ToString());                    
                    if (Tabela["DtAlteracao"].ToString() != "")
                        DtAlteracao = DateTime.Parse(Tabela["DtAlteracao"].ToString());
                    else
                        DtAlteracao = DateTime.Now;

                    if (Tabela["DtUltCompra"].ToString() != "")
                        DtUltCompra = DateTime.Parse(Tabela["DtUltCompra"].ToString());
                    if (Tabela["DtUltVenda"].ToString() != "")
                        DtUltVenda = DateTime.Parse(Tabela["DtUltVenda"].ToString());
                    if (Tabela["ID_Cfop_VD"].ToString() != "")
                        IdCfopVD =int.Parse(Tabela["ID_Cfop_VD"].ToString());

                    if (Tabela["ID_Cfop_VF"].ToString() != "")
                        IdCfopVF = int.Parse(Tabela["ID_Cfop_VF"].ToString());

                    if (Tabela["ID_Cfop_ED"].ToString() != "")
                        IdCfopED = int.Parse(Tabela["ID_Cfop_ED"].ToString());

                    if (Tabela["ID_Cfop_EF"].ToString() != "")
                        IdCfopEF = int.Parse(Tabela["ID_Cfop_EF"].ToString());

                    if (Tabela["QtdeCaixa"].ToString() != "")
                        QtdeCaixa = int.Parse(Tabela["QtdeCaixa"].ToString());

                    if (Tabela["QtdeCxDist"].ToString() != "")
                        QtdeCxDist = int.Parse(Tabela["QtdeCxDist"].ToString());

                    if (Tabela["PrcEspecial"].ToString() != "")
                        PrcEspecial = decimal.Parse(Tabela["PrcEspecial"].ToString());

                    if (Tabela["PrcEspDist"].ToString() != "")
                        PrcEspDist = decimal.Parse(Tabela["PrcEspDist"].ToString());

                    if (Tabela["Id_Reducao"].ToString() != "")
                        IdReducao = int.Parse(Tabela["Id_Reducao"].ToString());

                    if (Tabela["QtdeUnd"].ToString() != "")
                        QtdeUnd = int.Parse(Tabela["QtdeUnd"].ToString());

                    if (Tabela["Id_Promocao"].ToString() != "")
                        IdPromocao = int.Parse(Tabela["Id_Promocao"].ToString());

                    if (Tabela["UltPrcCompra2"].ToString() != "")
                        UltPrcCompra2 = decimal.Parse(Tabela["UltPrcCompra2"].ToString());

                    if (Tabela["Pontos"].ToString() != "")
                        Pontos = int.Parse(Tabela["Pontos"].ToString());

                    if (Tabela["DtAltPrc"].ToString() != "")
                        DtAltPrc = DateTime.Parse(Tabela["DtAltPrc"].ToString());
                    else
                        DtAltPrc = DateTime.Now;

                    if (Tabela["LocEstRua"].ToString() != "")
                        LocEstRua = int.Parse(Tabela["LocEstRua"].ToString());                                       
                                                            
                    /* CstIcms = int.Parse(Tabela["Cst_Icms"].ToString()); 
                    CstIpi = int.Parse(Tabela["Cst_Ipi"].ToString());
                    CstCofins = int.Parse(Tabela["Cst_Cofins"].ToString());
                    CstPis = int.Parse(Tabela["Cst_Pis"].ToString());*/
                }                
            }
            
        }
        public void LerDados(string IdRef)
        {
            IdProduto = 0;
            Referencia = "";
            Descricao = "";
            DescResumida = "";
            IdGrupo = 0;
            Ativo = 0;
            Tipo = 0;
            RefFornecedor = "";
            CodBarra = "";
            IcmsIss = 17;
            Reducao = 0;
            IcmsIss2 = 17;            
            SitTrib2 = 0;
            Ipi = 0;
            SitTributaria = 0;
            CodSefaz = "";
            EstMinimo = 0;
            EstMaximo = 0;
            PesoBruto = 0;
            PesoLiquido = 0;
            Unidade = "";
            PrcSensacional = 0;
            PrcEspecial = 0;
            PrcMinimo = 0;
            PrcVarejo = 0;
            PrcAtacado = 0;
            PrcEspDist = 0;
            UltPrcCompra = 0;
            UltPrcCompra2 = 0;
            Custo = 0;
            DtCadastro = DateTime.Now;
            DtAlteracao = DateTime.Now;
            DtAltPrc = DateTime.Now;
            Observacao = "";
            Composicao = "";
            SaldoEstoque = 0;
            ProdutoKit = 0;
            IdGenero = 0;
            EnviarEmail = 0;
            CreditoIPI = 0;
            NCM = "";
            IdCfopVD = 0;
            IdCfopVF = 0;
            IdCfopED = 0;
            IdCfopEF = 0;
            QtdeCaixa = 0;
            QtdeCxDist = 0;
            IdReducao = 0;
            QtdeUnd = 0;
            IdPromocao = 0;
            Pontos = 0;
            LocEstRua = 0;
            Palete = "";
            Foto = "";
            DetProduto = "";
            
           
            /*CstIcms = 0;
            CstIpi = 0;
            CstCofins = 0;
            CstPis = 0;*/

            if (IdRef.Trim() != "")
            {
                SqlDataReader Tabela;
                if (IdRef.Trim().Length > 11)
                    Tabela = Controle.ConsultaSQL("SELECT * FROM Produtos WHERE CodBarra='" + IdRef.ToString().Trim() + "'");
                else
                    Tabela = Controle.ConsultaSQL("SELECT * FROM Produtos WHERE Referencia='" + IdRef.ToString().Trim() + "'");

                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdProduto     = int.Parse(Tabela["Id_Produto"].ToString());
                    Referencia    = Tabela["Referencia"].ToString().Trim();
                    Descricao     = Tabela["Descricao"].ToString().Trim();
                    DescResumida  = Tabela["DescResumida"].ToString().Trim();
                    IdGrupo       = int.Parse(Tabela["Id_Grupo"].ToString());
                    Ativo         = int.Parse(Tabela["Ativo"].ToString());
                    Tipo          = int.Parse(Tabela["Tipo"].ToString());
                    RefFornecedor = Tabela["RefFornecedor"].ToString().Trim();
                    CodBarra      = Tabela["CodBarra"].ToString().Trim();
                    IcmsIss       = decimal.Parse(Tabela["IcmsIss"].ToString());
                    Reducao       = decimal.Parse(Tabela["Reducao"].ToString());
                    Ipi           = decimal.Parse(Tabela["Ipi"].ToString());
                    SitTributaria = int.Parse(Tabela["SitTributaria"].ToString());
                    CodSefaz      = Tabela["CodSefaz"].ToString().Trim();
                    EstMinimo     = decimal.Parse(Tabela["EstMinimo"].ToString());
                    EstMaximo     = decimal.Parse(Tabela["EstMaximo"].ToString());
                    PesoBruto     = decimal.Parse(Tabela["PesoBruto"].ToString());
                    PesoLiquido   = decimal.Parse(Tabela["PesoLiquido"].ToString());
                    Unidade       = Tabela["Unidade"].ToString();                    
                    PrcMinimo     = decimal.Parse(Tabela["PrcMinimo"].ToString());
                    PrcVarejo     = decimal.Parse(Tabela["PrcVarejo"].ToString());
                    PrcAtacado    = decimal.Parse(Tabela["PrcAtacado"].ToString());
                    PrcSensacional = decimal.Parse(Tabela["PrcSensacional"].ToString());
                    UltPrcCompra  = decimal.Parse(Tabela["UltPrcCompra"].ToString());
                    Custo         = decimal.Parse(Tabela["Custo"].ToString());
                    DtCadastro    = DateTime.Parse(Tabela["DtCadastro"].ToString());
                    NCM           = Tabela["NCM"].ToString().Trim();
                    Observacao    = Tabela["Observacao"].ToString();
                    Composicao    = Tabela["Composicao"].ToString();
                    SaldoEstoque  = decimal.Parse(Tabela["SaldoEstoque"].ToString());
                    ProdutoKit    = int.Parse(Tabela["ProdutoKit"].ToString());
                    IdGenero      = int.Parse(Tabela["Id_Genero"].ToString());
                    EnviarEmail   = int.Parse(Tabela["EnviarEmail"].ToString());
                    Palete        = Tabela["PALETE"].ToString().Trim();
                    DetProduto    = Tabela["DetProduto"].ToString().Trim();
                    Foto          = Tabela["Foto"].ToString().Trim();

                    if (Tabela["IcmsIss2"].ToString() != "")
                        IcmsIss2 = decimal.Parse(Tabela["IcmsIss2"].ToString());
                    
                    if (Tabela["SitTrib2"].ToString() != "")
                        SitTrib2 = int.Parse(Tabela["SitTrib2"].ToString());

                    if (Tabela["CreditoIPI"].ToString() != "")
                        CreditoIPI = int.Parse(Tabela["CreditoIPI"].ToString());
                    else
                        CreditoIPI = 0;

                    if (Tabela["DtAlteracao"].ToString() != "")
                        DtAlteracao = DateTime.Parse(Tabela["DtAlteracao"].ToString());
                    else
                        DtAlteracao = DateTime.Now;

                    if (Tabela["DtAltPrc"].ToString() != "")
                        DtAltPrc = DateTime.Parse(Tabela["DtAltPrc"].ToString());
                    else
                        DtAltPrc = DateTime.Now;

                    if (Tabela["DtUltCompra"].ToString() != "")
                        DtUltCompra = DateTime.Parse(Tabela["DtUltCompra"].ToString());

                    if (Tabela["DtUltVenda"].ToString() != "")
                        DtUltVenda = DateTime.Parse(Tabela["DtUltVenda"].ToString());
                    
                    if (Tabela["ID_Cfop_VD"].ToString() != "")
                        IdCfopVD = int.Parse(Tabela["ID_Cfop_VD"].ToString());

                    if (Tabela["ID_Cfop_VF"].ToString() != "")
                        IdCfopVF = int.Parse(Tabela["ID_Cfop_VF"].ToString());

                    if (Tabela["ID_Cfop_ED"].ToString() != "")
                        IdCfopED = int.Parse(Tabela["ID_Cfop_ED"].ToString());

                    if (Tabela["ID_Cfop_EF"].ToString() != "")
                        IdCfopEF = int.Parse(Tabela["ID_Cfop_EF"].ToString());

                    if (Tabela["QtdeCaixa"].ToString() != "")
                        QtdeCaixa = int.Parse(Tabela["QtdeCaixa"].ToString());

                    if (Tabela["QtdeCxDist"].ToString() != "")
                        QtdeCxDist = int.Parse(Tabela["QtdeCxDist"].ToString());

                    if (Tabela["PrcEspecial"].ToString() != "")
                        PrcEspecial = decimal.Parse(Tabela["PrcEspecial"].ToString());

                    if (Tabela["PrcEspDist"].ToString() != "")
                        PrcEspDist = decimal.Parse(Tabela["PrcEspDist"].ToString());

                    if (Tabela["Id_Reducao"].ToString() != "")
                        IdReducao = int.Parse(Tabela["Id_Reducao"].ToString());

                    if (Tabela["QtdeUnd"].ToString() != "")
                        QtdeUnd = int.Parse(Tabela["QtdeUnd"].ToString());

                    if (Tabela["Id_Promocao"].ToString() != "")
                        IdPromocao = int.Parse(Tabela["Id_Promocao"].ToString());

                    if (Tabela["UltPrcCompra2"].ToString() != "")
                        UltPrcCompra2 = decimal.Parse(Tabela["UltPrcCompra2"].ToString());

                    if (Tabela["Pontos"].ToString() != "")
                        Pontos = int.Parse(Tabela["Pontos"].ToString());

                    if (Tabela["LocEstRua"].ToString() != "")
                        LocEstRua = int.Parse(Tabela["LocEstRua"].ToString());
                                                           
                    /* CstIcms = int.Parse(Tabela["Cst_Icms"].ToString()); 
                    CstIpi = int.Parse(Tabela["Cst_Ipi"].ToString());
                    CstCofins = int.Parse(Tabela["Cst_Cofins"].ToString());
                    CstPis = int.Parse(Tabela["Cst_Pis"].ToString());*/
                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdProduto > 0)
            {
                sSQL = "UPDATE Produtos SET Id_Produto=@Id,Referencia=@Referencia,Descricao=@Descricao,DescResumida=@DescResumida,Id_Grupo=@IdGrupo,"+
                       "Ativo=@Ativo,Tipo=@Tipo,RefFornecedor=@RefFornecedor,CodBarra=@CodBarra,IcmsIss=@IcmsIss,Reducao=@Reducao,Ipi=@Ipi,SitTributaria=@SitTributaria,"+
                       "CodSefaz=@CodSefaz,EstMinimo=@EstMinimo,EstMaximo=@EstMaximo,PesoBruto=@PesoBruto,PesoLiquido=@PesoLiquido,Unidade=@Unidade,PrcMinimo=@PrcMinimo,"+
                       "PrcVarejo=@PrcVarejo,PrcAtacado=@PrcAtacado,UltPrcCompra=@UltPrcCompra,Custo=@Custo,DtCadastro=Convert(DateTime,@DtCadastro,103),Observacao=@Observacao,"+
                       "DtAlteracao=Convert(DateTime,@DtAlteracao,103),Composicao=@Composicao,ProdutoKit=@ProdutoKit,Id_Genero=@IdGenero,EnviarEmail=@EnviarEmail,CreditoIPI=@CreditoIPI,NCM=@NCM,"+
                       "Id_Cfop_VD=@IdCfopVD,Id_Cfop_VF=@IdCfopVF,PrcEspecial=@PrcEspecial,QtdeCaixa=@QtdeCaixa,QtdeCxDist=@QtdeCxDist,Id_Reducao=@IdReducao,IcmsIss2=@IcmsIss2,SitTrib2=@SitTrib2,"+
                       "QtdeUnd=@QtdeUnd,Id_Promocao=@IdPromocao,UltPrcCompra2=@UltPrcCompra2,Pontos=@Pontos,DtAltPrc=Convert(DateTime,@DtAltPrc,103),LocEstRua=@LocEstRua,Palete=@Palete,"+
                       "PrcSensacional=@PrcSensacional,DetProduto=@DetProduto,PrcEspDist=@PrcEspDist,Id_Cfop_ED=@IdCfopED,Id_Cfop_EF=@IdCfopEF Where Id_Produto=@Chave";                       
                Nm_param.Add("@Chave"); Vr_param.Add(IdProduto);
            }
            else
            {
                IdProduto = Controle.ProximoID("Produtos");

                if (Referencia.Equals(""))
                    Referencia = string.Format("{0:D6}", Controle.ProximoID("SeqRefPrd"));

                sSQL = "INSERT INTO Produtos (Id_Produto,Referencia,Descricao,DescResumida,Id_Grupo,Ativo,Tipo,RefFornecedor,CodBarra,IcmsIss,Reducao,Ipi,SitTributaria," +
                       "CodSefaz,EstMinimo,EstMaximo,PesoBruto,PesoLiquido,Unidade,PrcMinimo,PrcVarejo,PrcAtacado,UltPrcCompra,Custo,DtCadastro,Observacao,Composicao,SaldoEstoque,"+
                       "ProdutoKit,Id_Genero,DtAlteracao,EnviarEmail,CreditoIPI,NCM,Id_Cfop_VD,Id_Cfop_VF,PrcEspecial,QtdeCaixa,QtdeCxDist,Id_Reducao,IcmsIss2,SitTrib2,QtdeUnd,Id_Promocao,UltPrcCompra2,Pontos,DtAltPrc,LocEstRua,Palete,PrcSensacional,"+
                       "DetProduto,PrcEspDist,Foto,Id_Cfop_ED,Id_Cfop_EF)" +                    
                       "VALUES (@Id,@Referencia,@Descricao,@DescResumida,@IdGrupo,@Ativo,@Tipo,@RefFornecedor,@CodBarra,@IcmsIss,@Reducao,@Ipi,@SitTributaria,@CodSefaz,@EstMinimo,@EstMaximo,@PesoBruto,@PesoLiquido,@Unidade,@PrcMinimo," +
                       "@PrcVarejo,@PrcAtacado,@UltPrcCompra,@Custo,Convert(DateTime,@DtCadastro,103),@Observacao,@Composicao,0,@ProdutoKit,@IdGenero,Convert(DateTime,@DtAlteracao,103),@EnviarEmail,@CreditoIPI,@NCM,@IdCfopVD,@IdCfopVF,@PrcEspecial,"+
                       "@QtdeCaixa,@QtdeCxDist,@IdReducao,@IcmsIss2,@SitTrib2,@QtdeUnd,@IdPromocao,@UltPrcCompra2,@Pontos,Convert(DateTime,@DtAltPrc,103),@LocEstRua,@Palete,@PrcSensacional,@DetProduto,@PrcEspDist,convert(varbinary,'0'),@IdCfopED,@IdCfopEF)"; 
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");            Vr_param.Add(IdProduto);
                Nm_param.Add("@Referencia");    Vr_param.Add(Referencia);
                Nm_param.Add("@Descricao");     Vr_param.Add(Descricao);
                Nm_param.Add("@DescResumida");  Vr_param.Add(DescResumida);
                Nm_param.Add("@IdGrupo");       Vr_param.Add(IdGrupo);
                Nm_param.Add("@Ativo");         Vr_param.Add(Ativo);
                Nm_param.Add("@Tipo");          Vr_param.Add(Tipo);
                Nm_param.Add("@RefFornecedor"); Vr_param.Add(RefFornecedor);
                Nm_param.Add("@CodBarra");      Vr_param.Add(CodBarra);
                Nm_param.Add("@IcmsIss");       Vr_param.Add(Controle.FloatToStr(IcmsIss,2));
                Nm_param.Add("@Reducao");       Vr_param.Add(Controle.FloatToStr(Reducao,2));
                Nm_param.Add("@Ipi");           Vr_param.Add(Controle.FloatToStr(Ipi,2));
                Nm_param.Add("@SitTributaria"); Vr_param.Add(SitTributaria);
                Nm_param.Add("@CodSefaz");      Vr_param.Add(CodSefaz);
                Nm_param.Add("@EstMinimo");     Vr_param.Add(Controle.FloatToStr(EstMinimo));
                Nm_param.Add("@EstMaximo");     Vr_param.Add(Controle.FloatToStr(EstMaximo));
                Nm_param.Add("@PesoBruto");     Vr_param.Add(Controle.FloatToStr(PesoBruto));
                Nm_param.Add("@PesoLiquido");   Vr_param.Add(Controle.FloatToStr(PesoLiquido));
                Nm_param.Add("@Unidade");       Vr_param.Add(Unidade);
                Nm_param.Add("@PrcMinimo");     Vr_param.Add(Controle.FloatToStr(PrcMinimo,2));
                Nm_param.Add("@PrcVarejo");     Vr_param.Add(Controle.FloatToStr(PrcVarejo,2));
                Nm_param.Add("@PrcAtacado");    Vr_param.Add(Controle.FloatToStr(PrcAtacado,2));
                Nm_param.Add("@UltPrcCompra");  Vr_param.Add(Controle.FloatToStr(UltPrcCompra,2));
                Nm_param.Add("@Custo");         Vr_param.Add(Controle.FloatToStr(Custo,2));
                Nm_param.Add("@DtCadastro");    Vr_param.Add(DtCadastro.ToShortDateString());
                Nm_param.Add("@Observacao");    Vr_param.Add(Observacao);
                Nm_param.Add("@Composicao");    Vr_param.Add(Composicao);
                Nm_param.Add("@ProdutoKit");    Vr_param.Add(ProdutoKit);
                Nm_param.Add("@IdGenero");      Vr_param.Add(IdGenero);
                Nm_param.Add("@DtAlteracao");   Vr_param.Add(DtAlteracao.ToShortDateString());
                Nm_param.Add("@EnviarEmail");   Vr_param.Add(EnviarEmail);
                Nm_param.Add("@CreditoIPI");    Vr_param.Add(CreditoIPI);
                Nm_param.Add("@NCM");           Vr_param.Add(NCM);
                Nm_param.Add("@IdCfopVD");      Vr_param.Add(IdCfopVD);
                Nm_param.Add("@IdCfopVF");      Vr_param.Add(IdCfopVF);
                Nm_param.Add("@PrcEspecial");   Vr_param.Add(PrcEspecial);
                Nm_param.Add("@QtdeCaixa");     Vr_param.Add(QtdeCaixa);
                Nm_param.Add("@QtdeCxDist");    Vr_param.Add(QtdeCxDist);
                Nm_param.Add("@IdReducao");     Vr_param.Add(IdReducao);
                Nm_param.Add("@IcmsIss2");      Vr_param.Add(Controle.FloatToStr(IcmsIss2, 2));                
                Nm_param.Add("@SitTrib2");      Vr_param.Add(SitTrib2);
                Nm_param.Add("@QtdeUnd");       Vr_param.Add(QtdeUnd);
                Nm_param.Add("@IdPromocao");    Vr_param.Add(IdPromocao);
                Nm_param.Add("@UltPrcCompra2"); Vr_param.Add(Controle.FloatToStr(UltPrcCompra2, 2));
                Nm_param.Add("@Pontos");        Vr_param.Add(Pontos);
                Nm_param.Add("@DtAltPrc");      Vr_param.Add(DtAltPrc.ToShortDateString());
                Nm_param.Add("@LocEstRua");     Vr_param.Add(LocEstRua);
                Nm_param.Add("@Palete");        Vr_param.Add(Palete);
                Nm_param.Add("@DetProduto");    Vr_param.Add(DetProduto);
                Nm_param.Add("@PrcSensacional"); Vr_param.Add(Controle.FloatToStr(PrcSensacional, 2));
                Nm_param.Add("@PrcEspDist");    Vr_param.Add(Controle.FloatToStr(PrcEspDist, 2));
                Nm_param.Add("@IdCfopED");      Vr_param.Add(IdCfopED);
                Nm_param.Add("@IdCfopEF");      Vr_param.Add(IdCfopEF);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdProduto > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Produtos WHERE Id_Produto=" + IdProduto.ToString().Trim());
            }
        }        
    }
}
