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
    
    [Serializable()]    
    public class Usuarios
    {
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private string _Usuario;
        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Senha;
        public string Senha
        {
            get { return _Senha; }
            set { _Senha = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private int _SeusMov;
        public int SeusMov
        {
            get { return _SeusMov; }
            set { _SeusMov = value; }
        }
        private int _SolicAutCanc;
        public int SolicAutCanc
        {
            get { return _SolicAutCanc; }
            set { _SolicAutCanc = value; }
        }
        private int _LiberaDebito;
        public int LiberaDebito
        {
            get { return _LiberaDebito; }
            set { _LiberaDebito = value; }
        }
        private int _LiberaEstoque;
        public int LiberaEstoque
        {
            get { return _LiberaEstoque; }
            set { _LiberaEstoque = value; }
        }
        private int _LiberaPreco;
        public int LiberaPreco
        {
            get { return _LiberaPreco; }
            set { _LiberaPreco = value; }
        }
        private int _MostraPreco;
        public int MostraPreco
        {
            get { return _MostraPreco; }
            set { _MostraPreco = value; }
        }
        private int _Faturamento;
        public int Faturamento
        {
            get { return _Faturamento; }
            set { _Faturamento = value; }
        }
        private int _MultplaInstancia;
        public int MultplaInstancia
        {
            get { return _MultplaInstancia; }
            set { _MultplaInstancia = value; }
        }
        private int _AlteraFinanceiro;
        public int AlteraFinanceiro
        {
            get { return _AlteraFinanceiro; }
            set { _AlteraFinanceiro = value; }
        }
        private int _AlterarProduto;
        public int AlterarProduto
        {
            get { return _AlterarProduto; }
            set { _AlterarProduto = value; }
        }
        private int _AlterarPessoa;
        public int AlterarPessoa
        {
            get { return _AlterarPessoa; }
            set { _AlterarPessoa = value; }
        }
        private int _AlterarInstalacao;
        public int AlterarInstalacao
        {
            get { return _AlterarInstalacao; }
            set { _AlterarInstalacao = value; }
        }
        private int _ImpResumido;
        public int ImpResumido
        {
            get { return _ImpResumido; }
            set { _ImpResumido = value; }
        }
        private int _EmailAltPrd;
        public int EmailAltPrd
        {
            get { return _EmailAltPrd; }
            set { _EmailAltPrd = value; }
        }
        private DateTime _DtHrUltAcesso;
        public DateTime DtHrUltAcesso
        {
            get { return _DtHrUltAcesso; }
            set { _DtHrUltAcesso = value; }
        }
        private int _IdEntregador;
        public int IdEntregador
        {
            get { return _IdEntregador; }
            set { _IdEntregador = value; }
        }
        private int _SemMovEst;
        public int SemMovEst
        {
            get { return _SemMovEst; }
            set { _SemMovEst = value; }
        }
        private int _VerificarEstMin;
        public int VerificarEstMin
        {
            get { return _VerificarEstMin; }
            set { _VerificarEstMin = value; }
        }
        private int _EnviarFinanc;
        public int EnviarFinanc
        {
            get { return _EnviarFinanc; }
            set { _EnviarFinanc = value; }
        }
        private int _LimpaEstoque;
        public int LimpaEstoque
        {
            get { return _LimpaEstoque; }
            set { _LimpaEstoque = value; }
        }
        private int _AtualizaEstoque;
        public int AtualizaEstoque
        {
            get { return _AtualizaEstoque; }
            set { _AtualizaEstoque = value; }
        }
        private int _ExcluirReg;
        public int ExcluirReg
        {
            get { return _ExcluirReg; }
            set { _ExcluirReg = value; }
        }
        private int _AtlzBD;
        public int AtlzBD
        {
            get { return _AtlzBD; }
            set { _AtlzBD = value; }
        }
        private int _AltItemVD;
        public int AltItemVD
        {
            get { return _AltItemVD; }
            set { _AltItemVD = value; }
        }

        private int _BloqDesc;
        public int BloqDesc
        {
            get { return _BloqDesc; }
            set { _BloqDesc = value; }
        }

        private int _Telemarketing;
        public int Telemarketing
        {
            get { return _Telemarketing; }
            set { _Telemarketing = value; }
        }

        private int _CadDistrib;
        public int CadDistrib
        {
            get { return _CadDistrib; }
            set { _CadDistrib = value; }
        }

        private int _VerSldDeposito;
        public int VerSldDeposito
        {
            get { return _VerSldDeposito; }
            set { _VerSldDeposito = value; }
        }

        private int _AtivarProduto;
        public int AtivarProduto
        {
            get { return _AtivarProduto; }
            set { _AtivarProduto = value; }
        }

        private int _CancelarNF;
        public int CancelarNF
        {
            get { return _CancelarNF; }
            set { _CancelarNF = value; }
        }

        private int _CancVenda;
        public int CancVenda
        {
            get { return _CancVenda; }
            set { _CancVenda = value; }
        }

        private int _AlterarVenda;
        public int AlterarVenda
        {
            get { return _AlterarVenda; }
            set { _AlterarVenda = value; }
        }

        private int _IgnoraDescVd;
        public int IgnoraDescVd
        {
            get { return _IgnoraDescVd; }
            set { _IgnoraDescVd = value; }
        }

        private int _CancAmostra;
        public int CancAmostra
        {
            get { return _CancAmostra; }
            set { _CancAmostra = value; }
        }
        private int _CancMovEst;
        public int CancMovEst
        {
            get { return _CancMovEst; }
            set { _CancMovEst = value; }
        }
        private int _AltSenha;
        public int AltSenha
        {
            get { return _AltSenha; }
            set { _AltSenha = value; }
        }


        private int _VendedorBalcao;
        public int VendedorBalcao
        {
            get { return _VendedorBalcao; }
            set { _VendedorBalcao = value; }
        }

        private int _PrcDistrib;
        public int PrcDistrib
        {
            get { return _PrcDistrib; }
            set { _PrcDistrib = value; }
        }

        private int _AutCadPF;
        public int AutCadPF
        {
            get { return _AutCadPF; }
            set { _AutCadPF = value; }
        }
        private int _UsuCaixaLj;
        public int UsuCaixaLj
        {
            get { return _UsuCaixaLj; }
            set { _UsuCaixaLj = value; }
        }


        private int _LiberaPrcCusto;
        public int LiberaPrcCusto
        {
            get { return _LiberaPrcCusto; }
            set { _LiberaPrcCusto = value; }
        }
        private int _MostraCustoVd;
        public int MostraCustoVd
        {
            get { return _MostraCustoVd; }
            set { _MostraCustoVd = value; }
        }

        private int _BxVdFrenteLj;
        public int BxVdFrenteLj
        {
            get { return _BxVdFrenteLj; }
            set { _BxVdFrenteLj = value; }
        }

        private int _UsaPrcEspDist;
        public int UsaPrcEspDist
        {
            get { return _UsaPrcEspDist; }
            set { _UsaPrcEspDist = value; }
        }

        private int _IdPromocao;
        public int IdPromocao
        {
            get { return _IdPromocao; }
            set { _IdPromocao = value; }
        }


        public Funcoes Controle;        

        public void LerDados(int Id)
        {
            IdUsuario         = 0;
            Usuario           = "";
            Senha             = "";
            Email             = "";
            IdFilial          = 0;
            IdVendedor        = 0;
            SeusMov           = 1;
            LiberaDebito      = 0;
            LiberaEstoque     = 0;
            LiberaPreco       = 0;
            MostraPreco       = 0;
            SolicAutCanc      = 1;
            Faturamento       = 0;
            AlteraFinanceiro  = 0;
            MultplaInstancia  = 0;
            AlterarPessoa     = 0;
            AlterarProduto    = 0;
            AlterarInstalacao = 0;
            ImpResumido       = 0;
            EmailAltPrd       = 0;
            IdEntregador      = 0;
            SemMovEst         = 0;
            VerificarEstMin   = 0;
            EnviarFinanc      = 0;
            LimpaEstoque      = 0;
            AtualizaEstoque   = 0;
            ExcluirReg        = 0;
            AtlzBD            = 0;
            AltItemVD         = 0;
            BloqDesc          = 0;
            Telemarketing     = 0;
            CadDistrib        = 0;
            VerSldDeposito    = 0;
            AtivarProduto     = 0;
            CancelarNF        = 0;
            CancVenda         = 0;
            AlterarVenda      = 0;
            IgnoraDescVd      = 0;
            CancAmostra       = 0;
            CancMovEst        = 0;
            AltSenha          = 0;
            VendedorBalcao    = 0;
            PrcDistrib        = 0;
            AutCadPF          = 0;
            UsuCaixaLj        = 0;            
            LiberaPrcCusto    = 0;
            MostraCustoVd     = 0;
            BxVdFrenteLj      = 0;
            UsaPrcEspDist     = 0;
            IdPromocao        = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Usuarios WHERE Id_Usuario=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdUsuario = Id;
                    Usuario = Tabela["Usuario"].ToString().Trim();                    
                    Senha = Tabela["Senha"].ToString().Trim();
                    Email = Tabela["Email"].ToString().Trim();
                    IdFilial = int.Parse(Tabela["Id_Filial"].ToString());
                    IdVendedor       = int.Parse(Tabela["Id_Vendedor"].ToString());
                    SeusMov          = int.Parse(Tabela["SeusMov"].ToString());
                    SolicAutCanc     = int.Parse(Tabela["SolicAutCanc"].ToString());
                    LiberaDebito     = int.Parse(Tabela["LiberaDebito"].ToString());
                    LiberaEstoque    = int.Parse(Tabela["LiberaEstoque"].ToString());
                    LiberaPreco      = int.Parse(Tabela["LiberaPreco"].ToString());
                    MostraPreco      = int.Parse(Tabela["MostraPreco"].ToString());
                    Faturamento      = int.Parse(Tabela["Faturamento"].ToString());
                    AlteraFinanceiro = int.Parse(Tabela["AlteraFinanceiro"].ToString());
                    AlterarProduto   = int.Parse(Tabela["AlterarProduto"].ToString());
                    AlterarPessoa    = int.Parse(Tabela["AlterarPessoa"].ToString());
                    AlterarInstalacao= int.Parse(Tabela["AlterarInstalacao"].ToString());
                    MultplaInstancia = int.Parse(Tabela["MultplaInstancia"].ToString());
                    ImpResumido      = int.Parse(Tabela["ImpResumido"].ToString());
                    EmailAltPrd      = int.Parse(Tabela["EmailAltPrd"].ToString());
                    IdEntregador     = int.Parse(Tabela["Id_Entregador"].ToString());
                    SemMovEst        = int.Parse(Tabela["SemMovEst"].ToString());
                    LiberaPrcCusto   = int.Parse(Tabela["LiberaPrcCusto"].ToString());
                    MostraCustoVd    = int.Parse(Tabela["MostraCustoVd"].ToString());
                    

                    if (Tabela["DtHrUltAcesso"].ToString().Trim() != "")
                        DtHrUltAcesso = DateTime.Parse(Tabela["DtHrUltAcesso"].ToString().Trim());
                    if (Tabela["VerificarEstMin"].ToString().Trim() != "")
                        VerificarEstMin = int.Parse(Tabela["VerificarEstMin"].ToString());
                    if (Tabela["EnviarFinanc"].ToString().Trim() != "")
                        EnviarFinanc = int.Parse(Tabela["EnviarFinanc"].ToString());
                    if (Tabela["LimpaEstoque"].ToString().Trim() != "")
                        LimpaEstoque = int.Parse(Tabela["LimpaEstoque"].ToString());
                    if (Tabela["AtualizaEstoque"].ToString().Trim() != "")
                        AtualizaEstoque = int.Parse(Tabela["AtualizaEstoque"].ToString());
                    if (Tabela["ExcluirReg"].ToString().Trim() != "")
                        ExcluirReg = int.Parse(Tabela["ExcluirReg"].ToString());
                    if (Tabela["Atlz_BD"].ToString().Trim() != "")
                        AtlzBD = int.Parse(Tabela["Atlz_BD"].ToString());
                    if (Tabela["AltItemVD"].ToString().Trim() != "")
                        AltItemVD = int.Parse(Tabela["AltItemVD"].ToString());
                    if (Tabela["BloqDesc"].ToString().Trim() != "")
                        BloqDesc = int.Parse(Tabela["BloqDesc"].ToString());
                    if (Tabela["Telemarketing"].ToString().Trim() != "")
                        Telemarketing = int.Parse(Tabela["Telemarketing"].ToString());
                    if (Tabela["CadDistrib"].ToString().Trim() != "")
                        CadDistrib = int.Parse(Tabela["CadDistrib"].ToString());
                    if (Tabela["VerSldDeposito"].ToString().Trim() != "")
                        VerSldDeposito = int.Parse(Tabela["VerSldDeposito"].ToString());
                    if (Tabela["AtivarProduto"].ToString().Trim() != "")
                        AtivarProduto = int.Parse(Tabela["AtivarProduto"].ToString());
                    if (Tabela["CancelarNF"].ToString().Trim() != "")
                        CancelarNF = int.Parse(Tabela["CancelarNF"].ToString());
                    if (Tabela["CancVenda"].ToString().Trim() != "")
                        CancVenda = int.Parse(Tabela["CancVenda"].ToString());
                    if (Tabela["AlterarVenda"].ToString().Trim() != "")
                        AlterarVenda = int.Parse(Tabela["AlterarVenda"].ToString());
                    if (Tabela["IgnoraDescVd"].ToString().Trim() != "")
                        IgnoraDescVd = int.Parse(Tabela["IgnoraDescVd"].ToString());
                    if (Tabela["CancAmostra"].ToString().Trim() != "")
                        CancAmostra = int.Parse(Tabela["CancAmostra"].ToString());
                    if (Tabela["CancMovEst"].ToString().Trim() != "")
                        CancMovEst = int.Parse(Tabela["CancMovEst"].ToString());

                    if (Tabela["AltSenha"].ToString().Trim() != "")
                        AltSenha = int.Parse(Tabela["AltSenha"].ToString());

                    if (Tabela["VendedorBalcao"].ToString().Trim() != "")
                        VendedorBalcao = int.Parse(Tabela["VendedorBalcao"].ToString());

                    if (Tabela["PrcDistrib"].ToString().Trim() != "")
                        PrcDistrib = int.Parse(Tabela["PrcDistrib"].ToString());

                    if (Tabela["Aut_CadPF"].ToString().Trim() != "")
                        AutCadPF = int.Parse(Tabela["Aut_CadPF"].ToString());

                    if (Tabela["UsuCaixaLj"].ToString().Trim() != "")
                        UsuCaixaLj = int.Parse(Tabela["UsuCaixaLj"].ToString());

                    if (Tabela["BxVdFrenteLj"].ToString().Trim() != "")
                        BxVdFrenteLj = int.Parse(Tabela["BxVdFrenteLj"].ToString());

                    if (Tabela["UsaPrcEspDist"].ToString().Trim() != "")
                        UsaPrcEspDist = int.Parse(Tabela["UsaPrcEspDist"].ToString());

                    if (Tabela["Id_Promocao"].ToString().Trim() != "")
                        IdPromocao = int.Parse(Tabela["Id_Promocao"].ToString());

                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdUsuario > 0)
            {
                sSQL = "UPDATE USUARIOS SET Id_Usuario=@Id,Usuario=@Nm,Senha=@Psw,Id_Filial=@IdFilial,Id_Vendedor=@IdVendedor,SeusMov=@SeusMov,SolicAutCanc=@SolicAutCanc,LiberaDebito=@LiberaDebito,AlterarInstalacao=@AlterarInstalacao," +
                       "LiberaEstoque=@LiberaEstoque,MostraPreco=@MostraPreco,Faturamento=@Faturamento,LiberaPreco=@LiberaPreco,MultplaInstancia=@MultplaInstancia,AlteraFinanceiro=@AlteraFinanceiro,AlterarProduto=@AlterarProduto,AlterarPessoa=@AlterarPessoa,"+
                       "ImpResumido=@ImpResumido,Email=@Email,EmailAltPrd=@EmailAltPrd,Id_Entregador=@IdEntregador,SemMovEst=@SemMovEst,VerificarEstMin=@VerificarEstMin,EnviarFinanc=@EnviarFinanc,LimpaEstoque=@LimpaEstoque,AtualizaEstoque=@AtualizaEstoque,ExcluirReg=@ExcluirReg,"+
                       "Atlz_BD=@AtlzBD,AltItemVD=@AltItemVD,BloqDesc=@BloqDesc,Telemarketing=@Telemarketing,CadDistrib=@CadDistrib,VerSldDeposito=@VerSldDeposito,AtivarProduto=@AtivarProduto,CancelarNF=@CancelarNF,CancVenda=@CancVenda,AlterarVenda=@AlterarVenda,"+
                       "IgnoraDescVd=@IgnoraDescVd,CancAmostra=@CancAmostra,CancMovEst=@CancMovEst,AltSenha=@AltSenha,VendedorBalcao=@VendedorBalcao,PrcDistrib=@PrcDistrib,Aut_CadPF=@AutCadPF,UsuCaixaLj=@UsuCaixaLj,LiberaPrcCusto=@LiberaPrcCusto,MostraCustoVd=@MostraCustoVd,"+
                       "BxVdFrenteLj=@BxVdFrenteLj,UsaPrcEspDist=@UsaPrcEspDist,Id_Promocao=@IdPromocao Where Id_Usuario=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdUsuario);
            }
            else
            {
                IdUsuario = Controle.ProximoID("Usuarios");
                sSQL = "INSERT INTO USUARIOS (ID_USUARIO,USUARIO,SENHA,ID_FILIAL,ID_Vendedor,SeusMov,SolicAutCanc,LiberaDebito,LiberaEstoque,MostraPreco,Faturamento,LiberaPreco,MultplaInstancia,AlteraFinanceiro,AlterarProduto,AlterarPessoa,AlterarInstalacao,ImpResumido,Email,EmailAltPrd,"+
                       "Id_Entregador,SemMovEst,VerificarEstMin,EnviarFinanc,LimpaEstoque,AtualizaEstoque,ExcluirReg,Atlz_BD,AltItemVD,BloqDesc,Telemarketing,CadDistrib,VerSldDeposito,AtivarProduto,CancelarNF,CancVenda,AlterarVenda,IgnoraDescVd,CancAmostra,CancMovEst,AltSenha,VendedorBalcao,PrcDistrib,Aut_CadPF,UsuCaixaLj,LiberaPrcCusto,MostraCustoVd,BxVdFrenteLj,UsaPrcEspDist,Id_Promocao) " +
                       " VALUES(@Id,@Nm,@Psw,@IdFilial,@IdVendedor,@SeusMov,@SolicAutCanc,@LiberaDebito,@LiberaEstoque,@MostraPreco,@Faturamento,@LiberaPreco,@MultplaInstancia,@AlteraFinanceiro,@AlterarProduto,@AlterarPessoa,@AlterarInstalacao,@ImpResumido,@Email,@EmailAltPrd,@IdEntregador,"+
                       "@SemMovEst,@VerificarEstMin,@EnviarFinanc,@LimpaEstoque,@AtualizaEstoque,@ExcluirReg,@AtlzBD,@AltItemVD,@BloqDesc,@Telemarketing,@CadDistrib,@VerSldDeposito,@AtivarProduto,@CancelarNF,@CancVenda,@AlterarVenda,@IgnoraDescVd,@CancAmostra,@CancMovEst,@AltSenha,@VendedorBalcao,@PrcDistrib,@AutCadPF,@UsuCaixaLj,@LiberaPrcCusto,@MostraCustoVd,@BxVdFrenteLj,@UsaPrcEspDist,@IdPromocao)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdUsuario);
                Nm_param.Add("@Nm"); Vr_param.Add(Usuario);
                Nm_param.Add("@Email"); Vr_param.Add(Email);                
                Nm_param.Add("@Psw"); Vr_param.Add(Senha);
                Nm_param.Add("@IdFilial"); Vr_param.Add(IdFilial);
                Nm_param.Add("@IdVendedor"); Vr_param.Add(IdVendedor);
                Nm_param.Add("@SeusMov"); Vr_param.Add(SeusMov);
                Nm_param.Add("@SolicAutCanc"); Vr_param.Add(SolicAutCanc);
                Nm_param.Add("@LiberaDebito"); Vr_param.Add(LiberaDebito);
                Nm_param.Add("@LiberaEstoque"); Vr_param.Add(LiberaEstoque);
                Nm_param.Add("@MostraPreco"); Vr_param.Add(MostraPreco);
                Nm_param.Add("@Faturamento"); Vr_param.Add(Faturamento);
                Nm_param.Add("@LiberaPreco"); Vr_param.Add(LiberaPreco);
                Nm_param.Add("@MultplaInstancia"); Vr_param.Add(MultplaInstancia);
                Nm_param.Add("@AlteraFinanceiro"); Vr_param.Add(AlteraFinanceiro);
                Nm_param.Add("@AlterarProduto"); Vr_param.Add(AlterarProduto);
                Nm_param.Add("@AlterarPessoa"); Vr_param.Add(AlterarPessoa);
                Nm_param.Add("@AlterarInstalacao"); Vr_param.Add(AlterarInstalacao);
                Nm_param.Add("@ImpResumido"); Vr_param.Add(ImpResumido);
                Nm_param.Add("@EmailAltPrd"); Vr_param.Add(EmailAltPrd);
                Nm_param.Add("@IdEntregador"); Vr_param.Add(IdEntregador);
                Nm_param.Add("@SemMovEst"); Vr_param.Add(SemMovEst);
                Nm_param.Add("@VerificarEstMin"); Vr_param.Add(VerificarEstMin);
                Nm_param.Add("@EnviarFinanc"); Vr_param.Add(EnviarFinanc);
                Nm_param.Add("@LimpaEstoque"); Vr_param.Add(LimpaEstoque);
                Nm_param.Add("@AtualizaEstoque"); Vr_param.Add(AtualizaEstoque);
                Nm_param.Add("@ExcluirReg"); Vr_param.Add(ExcluirReg);
                Nm_param.Add("@AtlzBD"); Vr_param.Add(AtlzBD);
                Nm_param.Add("@AltItemVD"); Vr_param.Add(AltItemVD);
                Nm_param.Add("@BloqDesc"); Vr_param.Add(BloqDesc);
                Nm_param.Add("@Telemarketing"); Vr_param.Add(Telemarketing);
                Nm_param.Add("@CadDistrib"); Vr_param.Add(CadDistrib);
                Nm_param.Add("@VerSldDeposito"); Vr_param.Add(VerSldDeposito);
                Nm_param.Add("@AtivarProduto"); Vr_param.Add(AtivarProduto);
                Nm_param.Add("@CancelarNF"); Vr_param.Add(CancelarNF);
                Nm_param.Add("@CancVenda"); Vr_param.Add(CancVenda);
                Nm_param.Add("@AlterarVenda"); Vr_param.Add(AlterarVenda);
                Nm_param.Add("@IgnoraDescVd"); Vr_param.Add(IgnoraDescVd);
                Nm_param.Add("@CancAmostra"); Vr_param.Add(CancAmostra);
                Nm_param.Add("@CancMovEst"); Vr_param.Add(CancMovEst);
                Nm_param.Add("@AltSenha"); Vr_param.Add(AltSenha);
                Nm_param.Add("@VendedorBalcao"); Vr_param.Add(VendedorBalcao);
                Nm_param.Add("@PrcDistrib"); Vr_param.Add(PrcDistrib);
                Nm_param.Add("@AutCadPF"); Vr_param.Add(AutCadPF);
                Nm_param.Add("@UsuCaixaLj"); Vr_param.Add(UsuCaixaLj);
                Nm_param.Add("@LiberaPrcCusto"); Vr_param.Add(LiberaPrcCusto);
                Nm_param.Add("@MostraCustoVd"); Vr_param.Add(MostraCustoVd);
                Nm_param.Add("@BxVdFrenteLj"); Vr_param.Add(BxVdFrenteLj);
                Nm_param.Add("@UsaPrcEspDist"); Vr_param.Add(UsaPrcEspDist);
                Nm_param.Add("@IdPromocao"); Vr_param.Add(IdPromocao);
                Controle.ExecutaSQL(sSQL,Nm_param,Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdUsuario > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Usuarios WHERE Id_Usuario=" + IdUsuario.ToString().Trim());
            }
        }        
    }
}


