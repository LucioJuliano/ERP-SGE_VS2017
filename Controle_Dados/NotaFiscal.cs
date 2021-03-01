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
    public class NotaFiscal
    {
        private int _IdNota;
        public int IdNota
        {
            get { return _IdNota; }
            set { _IdNota = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private int _IdVenda;
        public int IdVenda
        {
            get { return _IdVenda; }
            set { _IdVenda = value; }
        }
        private int _IdPessoa;
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        private string _NmPessoa;
        public string NmPessoa
        {
            get { return _NmPessoa; }
            set { _NmPessoa = value; }
        }
        private string _CnpjCpf;
        public string CnpjCpf
        {
            get { return _CnpjCpf; }
            set { _CnpjCpf = value; }
        }
        private string _InscUf;
        public string InscUf
        {
            get { return _InscUf; }
            set { _InscUf = value; }
        }
        private string _Cep;
        public string Cep
        {
            get { return _Cep; }
            set { _Cep = value; }
        }
        private string _Endereco;
        public string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; }
        }
        private string _Compl;
        public string Compl
        {
            get { return _Compl; }
            set { _Compl = value; }
        }
        private string _Numero;
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        private string _Bairro;
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        private string _Cidade;
        public string Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }
        private int _IdUf;
        public int IdUf
        {
            get { return _IdUf; }
            set { _IdUf = value; }
        }
        private string _Telefone;
        public string Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value; }
        }
        private int _IdCfop;
        public int IdCfop
        {
            get { return _IdCfop; }
            set { _IdCfop = value; }
        }
        private int _IdTransportadora;
        public int IdTransportadora
        {
            get { return _IdTransportadora; }
            set { _IdTransportadora = value; }
        }
        private int _Frete;
        public int Frete
        {
            get { return _Frete; }
            set { _Frete = value; }
        }
        private int _EntSaida;
        public int EntSaida
        {
            get { return _EntSaida; }
            set { _EntSaida = value; }
        }
        private DateTime _DtEmissao;
        public DateTime DtEmissao
        {
            get { return _DtEmissao; }
            set { _DtEmissao = value; }
        }
        private int _NumFormulario;
        public int NumFormulario
        {
            get { return _NumFormulario; }
            set { _NumFormulario = value; }
        }
        private int _NumNota;
        public int NumNota
        {
            get { return _NumNota; }
            set { _NumNota = value; }
        }
        private decimal _BIcms;
        public decimal BIcms
        {
            get { return _BIcms; }
            set { _BIcms = value; }
        }
        private decimal _VlrIcms;
        public decimal VlrIcms
        {
            get { return _VlrIcms; }
            set { _VlrIcms = value; }
        }
        private decimal _BIcmsSub;
        public decimal BIcmsSub
        {
            get { return _BIcmsSub; }
            set { _BIcmsSub = value; }
        }
        private decimal _VlrIcmsSub;
        public decimal VlrIcmsSub
        {
            get { return _VlrIcmsSub; }
            set { _VlrIcmsSub = value; }
        }
        private decimal _VlrFrete;
        public decimal VlrFrete
        {
            get { return _VlrFrete; }
            set { _VlrFrete = value; }
        }
        private decimal _VlrOutraDesp;
        public decimal VlrOutraDesp
        {
            get { return _VlrOutraDesp; }
            set { _VlrOutraDesp = value; }
        }
        private decimal _VlrSeguro;
        public decimal VlrSeguro
        {
            get { return _VlrSeguro; }
            set { _VlrSeguro = value; }
        }
        private decimal _VlrIpi;
        public decimal VlrIpi
        {
            get { return _VlrIpi; }
            set { _VlrIpi = value; }
        }
        private decimal _VlrProdutos;
        public decimal VlrProdutos
        {
            get { return _VlrProdutos; }
            set { _VlrProdutos = value; }
        }
        private decimal _VlrNota;
        public decimal VlrNota
        {
            get { return _VlrNota; }
            set { _VlrNota = value; }
        }
        private decimal _VlrDesconto;
        public decimal VlrDesconto
        {
            get { return _VlrDesconto; }
            set { _VlrDesconto = value; }
        }
        private string _Observacao;
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        private int _QtdeVolume;
        public int QtdeVolume
        {
            get { return _QtdeVolume; }
            set { _QtdeVolume = value; }
        }
        private string _Especie;
        public string Especie
        {
            get { return _Especie; }
            set { _Especie = value; }
        }
        private string _Marca;
        public string Marca
        {
            get { return _Marca; }
            set { _Marca = value; }
        }
        private decimal _PesoBruto;
        public decimal PesoBruto
        {
            get { return _PesoBruto; }
            set { _PesoBruto = value; }
        }
        private decimal _PesoLiquido;
        public decimal PesoLiquido
        {
            get { return _PesoLiquido; }
            set { _PesoLiquido = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private int _SeqImp;
        public int SeqImp
        {
            get { return _SeqImp; }
            set { _SeqImp = value; }
        }
        private DateTime _DtCancel;
        public DateTime DtCancel
        {
            get { return _DtCancel; }
            set { _DtCancel = value; }
        }
        private string _ProtocoloNfe;
        public string ProtocoloNfe
        {
            get { return _ProtocoloNfe; }
            set { _ProtocoloNfe = value; }
        }
        private string _ReciboNfe;
        public string ReciboNfe
        {
            get { return _ReciboNfe; }
            set { _ReciboNfe = value; }
        }
        private string _ChaveNfe;
        public string ChaveNfe
        {
            get { return _ChaveNfe; }
            set { _ChaveNfe = value; }
        }
        private int _NFE;
        public int NFE
        {
            get { return _NFE; }
            set { _NFE = value; }
        }
        private string _Pais;
        public string Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }
        private decimal _VlrAcrescimo;
        public decimal VlrAcrescimo
        {
            get { return _VlrAcrescimo; }
            set { _VlrAcrescimo = value; }
        }

        private int _Consumidor;
        public int Consumidor
        {
            get { return _Consumidor; }
            set { _Consumidor = value; }
        }
        private int _Atendimento;
        public int Atendimento
        {
            get { return _Atendimento; }
            set { _Atendimento = value; }
        }
        private int _DestOperacao;
        public int DestOperacao
        {
            get { return _DestOperacao; }
            set { _DestOperacao = value; }
        }

        private int _Finalidade;
        public int Finalidade
        {
            get { return _Finalidade; }
            set { _Finalidade = value; }
        }

        private string _ChaveNfeDev;
        public string ChaveNfeDev
        {
            get { return _ChaveNfeDev; }
            set { _ChaveNfeDev = value; }
        }
        private int _NatOp;
        public int NatOp
        {
            get { return _NatOp; }
            set { _NatOp = value; }
        }

        private int _CodMun;
        public int CodMun
        {
            get { return _CodMun; }
            set { _CodMun = value; }
        }

        private string _NumPedido; 
        public string NumPedido
        {
            get { return _NumPedido; }
            set { _NumPedido = value; }
        }

        private decimal _PercDifal;
        public decimal PercDifal
        {
            get { return _PercDifal; }
            set { _PercDifal = value; }
        }

        private decimal _ICMSInterno;
        public decimal ICMSInterno
        {
            get { return _ICMSInterno; }
            set { _ICMSInterno = value; }
        }

        private DateTime _DataCarta;
        public DateTime DataCarta
        {
            get { return _DataCarta; }
            set { _DataCarta = value; }
        }
        private string _ProtocoloCarta;
        public string ProtocoloCarta
        {
            get { return _ProtocoloCarta; }
            set { _ProtocoloCarta = value; }
        }

        private string _CartaCorrecao;
        public string CartaCorrecao
        {
            get { return _CartaCorrecao; }
            set { _CartaCorrecao = value; }
        }

        private string _VencFatura;
        public string VencFatura
        {
            get { return _VencFatura; }
            set { _VencFatura = value; }
        }

        private int _MeioPag;
        public int MeioPag
        {
            get { return _MeioPag; }
            set { _MeioPag = value; }
        }
        public Funcoes Controle;

        public void LerDados(int Id)
        {
            try
            {
                IdNota = 0;
                Data = DateTime.Now;
                IdFilial = 0;
                IdVenda = 0;
                IdPessoa = 0;
                IdCfop = 0;
                IdTransportadora = 0;
                Frete = 0;
                EntSaida = 0;
                DtEmissao = DateTime.Now;
                NumFormulario = 0;
                NumNota = 0;
                VlrProdutos = 0;
                VlrNota = 0;
                VlrDesconto = 0;
                BIcms = 0;
                VlrIcms = 0;
                BIcmsSub = 0;
                VlrIcmsSub = 0;
                VlrFrete = 0;
                VlrSeguro = 0;
                VlrOutraDesp = 0;
                VlrIpi = 0;
                Observacao = "";
                Status = 0;
                SeqImp = 1;
                QtdeVolume = 0;
                Especie = "";
                Marca = "";
                PesoBruto = 0;
                PesoLiquido = 0;
                NmPessoa = "";
                CnpjCpf = "";
                InscUf = "";
                Cep = "";
                Endereco = "";
                Numero = "";
                Bairro = "";
                Compl = "";
                Cidade = "";
                IdUf = 0;
                Telefone = "";
                ProtocoloNfe = "";
                ReciboNfe = "";
                ChaveNfe = "";
                NFE = 0;
                Pais = "1058";
                VlrAcrescimo = 0;
                Consumidor = 0;
                Atendimento = 0;
                DestOperacao = 0;
                Finalidade = 0;
                ChaveNfeDev = "";
                NatOp = 0;
                CodMun = 2304400;
                NumPedido = "";
                PercDifal = 0;
                ICMSInterno = 0;
                CartaCorrecao = "";
                ProtocoloCarta = "";
                VencFatura = "";
                MeioPag = 0;

                if (Id > 0)
                {
                    SqlDataReader Tabela;
                    Tabela = Controle.ConsultaSQL("SELECT * FROM NotaFiscal WHERE Id_Nota=" + Id.ToString().Trim());
                    if (Tabela.HasRows)
                    {
                        Tabela.Read();
                        IdNota = Id;
                        Data = DateTime.Parse(Tabela["Data"].ToString());
                        IdFilial = int.Parse(Tabela["Id_Filial"].ToString());
                        IdVenda = int.Parse(Tabela["Id_Venda"].ToString());
                        IdPessoa = int.Parse(Tabela["Id_Pessoa"].ToString());
                        IdCfop = int.Parse(Tabela["Id_Cfop"].ToString());
                        IdTransportadora = int.Parse(Tabela["Id_Transportadora"].ToString());
                        Frete = int.Parse(Tabela["Frete"].ToString());
                        EntSaida = int.Parse(Tabela["EntSaida"].ToString());
                        DtEmissao = DateTime.Parse(Tabela["DtEmissao"].ToString());
                        NumFormulario = int.Parse(Tabela["NumFormulario"].ToString().Trim());
                        NumNota = int.Parse(Tabela["NumNota"].ToString().Trim());
                        VlrProdutos = decimal.Parse(Tabela["VlrProdutos"].ToString());
                        VlrNota = decimal.Parse(Tabela["VlrNota"].ToString());
                        VlrDesconto = decimal.Parse(Tabela["VlrDesconto"].ToString());
                        BIcms = decimal.Parse(Tabela["BIcms"].ToString());
                        VlrIcms = decimal.Parse(Tabela["VlrIcms"].ToString());
                        BIcmsSub = decimal.Parse(Tabela["BIcmsSub"].ToString());
                        VlrIcmsSub = decimal.Parse(Tabela["VlrIcmsSub"].ToString());
                        VlrFrete = decimal.Parse(Tabela["VlrFrete"].ToString());
                        VlrSeguro = decimal.Parse(Tabela["VlrSeguro"].ToString());
                        VlrOutraDesp = decimal.Parse(Tabela["VlrOutraDesp"].ToString());
                        VlrIpi = decimal.Parse(Tabela["VlrIpi"].ToString());
                        Observacao = Tabela["Observacao"].ToString().Trim();
                        Status = int.Parse(Tabela["Status"].ToString());
                        SeqImp = int.Parse(Tabela["SeqImp"].ToString());
                        QtdeVolume = int.Parse(Tabela["QtdeVolume"].ToString());
                        Especie = Tabela["Especie"].ToString().Trim();
                        Marca = Tabela["Marca"].ToString().Trim();
                        PesoBruto = decimal.Parse(Tabela["PesoBruto"].ToString());
                        PesoLiquido = decimal.Parse(Tabela["PesoLiquido"].ToString());
                        CnpjCpf = Tabela["CnpjCpf"].ToString().Trim();
                        NmPessoa = Tabela["RazaoSocial"].ToString().Trim();
                        InscUf = Tabela["Insc_Uf"].ToString().Trim();
                        Cep = Tabela["Cep"].ToString().Trim();
                        Endereco = Tabela["Endereco"].ToString().Trim();
                        Numero = Tabela["Numero"].ToString().Trim();
                        Bairro = Tabela["Bairro"].ToString().Trim();
                        Compl = Tabela["Complemento"].ToString().Trim();
                        Cidade = Tabela["Cidade"].ToString().Trim();
                        IdUf = int.Parse(Tabela["Id_Uf"].ToString().Trim());
                        Telefone = Tabela["Telefone"].ToString().Trim();
                        ProtocoloNfe = Tabela["ProtocoloNfe"].ToString().Trim();
                        ChaveNfe = Tabela["ChaveNfe"].ToString().Trim();
                        ReciboNfe = Tabela["ReciboNfe"].ToString().Trim();
                        NFE = int.Parse(Tabela["NFE"].ToString().Trim());
                        ChaveNfeDev = Tabela["ChaveNfeDev"].ToString().Trim();
                        NumPedido = Tabela["NumPedido"].ToString().Trim();
                        CartaCorrecao = Tabela["CartaCorrecao"].ToString().Trim();
                        ProtocoloCarta = Tabela["ProtocoloCarta"].ToString().Trim();
                        VencFatura = Tabela["VencFatura"].ToString().Trim();
                        MeioPag = int.Parse(Tabela["MeioPag"].ToString().Trim());

                        if (Tabela["Pais"].ToString() != "")
                            Pais = Tabela["Pais"].ToString().Trim();

                        if (Tabela["VlrAcrescimo"].ToString() != "")
                            VlrAcrescimo = decimal.Parse(Tabela["VlrAcrescimo"].ToString());
                        if (Tabela["Consumidor"].ToString() != "")
                            Consumidor = int.Parse(Tabela["Consumidor"].ToString());
                        if (Tabela["Atendimento"].ToString() != "")
                            Atendimento = int.Parse(Tabela["Atendimento"].ToString());
                        if (Tabela["DestOperacao"].ToString() != "")
                            DestOperacao = int.Parse(Tabela["DestOperacao"].ToString());
                        if (Tabela["Finalidade"].ToString() != "")
                            Finalidade = int.Parse(Tabela["Finalidade"].ToString());
                        if (Tabela["NatOp"].ToString() != "")
                            NatOp = int.Parse(Tabela["NatOp"].ToString());
                        if (Tabela["CodMun"].ToString() != "")
                            CodMun = int.Parse(Tabela["CodMun"].ToString());

                        if (Tabela["ICMSInterno"].ToString() != "")
                            ICMSInterno = decimal.Parse(Tabela["ICMSInterno"].ToString());
                        if (Tabela["PercDifal"].ToString() != "")
                            PercDifal = int.Parse(Tabela["PercDifal"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Controle.RegistroLog(e.ToString());
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdNota > 0)
            {
                sSQL = "UPDATE NotaFiscal SET Id_Nota = @Id,Data=Convert(DateTime,@Data,103),Id_Filial = @IdFilial,Id_Venda = @IdVenda,Id_Pessoa = @IdPessoa,Id_Cfop = @IdCfop," +
                       "Id_Transportadora = @IdTransportadora,Frete = @Frete,EntSaida = @EntSaida,DtEmissao=Convert(DateTime,@DtEmissao,103),NumFormulario = @NumFormulario,NumNota = @NumNota," +
                       "VlrProdutos = @VlrProdutos,VlrDesconto=@VlrDesconto,VlrNota = @VlrNota,BIcms = @BIcms,VlrIcms = @VlrIcms,BIcmsSub = @BIcmsSub,VlrIcmsSub = @VlrIcmsSub,VlrFrete = @VlrFrete,VlrSeguro = @VlrSeguro," +
                       "VlrOutraDesp = @VlrOutraDesp,VlrIpi = @VlrIpi,Observacao = @Observacao,Status = @Status,QtdeVolume = @QtdeVolume,Especie = @Especie,Marca = @Marca,PesoBruto = @PesoBruto," +
                       "PesoLiquido = @PesoLiquido,RazaoSocial=@NmPessoa,CnpjCpf = @CnpjCpf,Insc_Uf = @InscUf,Cep = @Cep,Endereco = @Endereco,Numero=@Numero,Bairro = @Bairro,Complemento = @Compl,Cidade = @Cidade," +
                       "Id_Uf = @IdUf,Telefone = @Telefone, SeqImp=@SeqImp,NFE=@NFE,Pais=@Pais,VlrAcrescimo=@VlrAcrescimo,Consumidor=@Consumidor,Atendimento=@Atendimento,DestOperacao=@DestOperacao,Finalidade=@Finalidade,"+
                       "ChaveNfeDev=@ChaveNfeDev,NatOp=@NatOp,CodMun=@CodMun,NumPedido=@NumPedido,ICMSInterno=@ICMSInterno,PercDifal=@PercDifal,VencFatura=@VencFatura,MeioPag=@MeioPag Where Id_Nota=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdNota);
            }
            else
            {                
                IdNota = Controle.ProximoID("NotaFiscal");                
                sSQL = "INSERT INTO NotaFiscal (Id_Nota,Data,Id_Filial,Id_Venda,Id_Pessoa,Id_Cfop,Id_Transportadora,Frete,EntSaida,DtEmissao,NumFormulario,NumNota,VlrProdutos,VlrDesconto,VlrNota,BIcms,VlrIcms,BIcmsSub,VlrIcmsSub," +
                       "VlrFrete,VlrSeguro,VlrOutraDesp,VlrIpi,Observacao,Status,QtdeVolume,Especie,Marca,PesoBruto,PesoLiquido,RazaoSocial,CnpjCpf,Insc_Uf,Cep,Endereco,Numero,Bairro,Complemento,Cidade,Id_Uf,Telefone,SeqImp,ProtocoloNfe,ReciboNfe,"+
                       "ChaveNfe,NFE,Pais,VlrAcrescimo,Consumidor,Atendimento,DestOperacao,Finalidade,ChaveNfeDev,NatOp,CodMun,NumPedido,ICMSInterno,PercDifal,VencFatura,MeioPag) " +
                       "Values (@Id,Convert(DateTime,@Data,103),@IdFilial,@IdVenda,@IdPessoa,@IdCfop,@IdTransportadora,@Frete,@EntSaida,Convert(DateTime,@DtEmissao,103),@NumFormulario,@NumNota,@VlrProdutos,@VlrDesconto,@VlrNota," +
                       "@BIcms,@VlrIcms,@BIcmsSub,@VlrIcmsSub,@VlrFrete,@VlrSeguro,@VlrOutraDesp,@VlrIpi,@Observacao,@Status,@QtdeVolume,@Especie,@Marca,@PesoBruto,@PesoLiquido,@NmPessoa,@CnpjCpf,@InscUf,@Cep,@Endereco,@Numero,@Bairro," +
                       "@Compl,@Cidade,@IdUf,@Telefone,@SeqImp,' ',' ',' ',@NFE,@Pais,@VlrAcrescimo,@Consumidor,@Atendimento,@DestOperacao,@Finalidade,@ChaveNfeDev,@NatOp,@CodMun,@NumPedido,@ICMSInterno,@PercDifal,@VencFatura,@MeioPag)";
            }
            if (sSQL != "")
            {
                // Calcular os Impostos
                CalcularImposto();
                //------------------                
                VlrNota = (VlrProdutos + VlrIpi + VlrOutraDesp + VlrAcrescimo + VlrFrete) - VlrDesconto;

                Nm_param.Add("@Id");               Vr_param.Add(IdNota);
                Nm_param.Add("@Data");             Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@IdFilial");         Vr_param.Add(IdFilial);
                Nm_param.Add("@IdVenda");          Vr_param.Add(IdVenda);
                Nm_param.Add("@IdPessoa");         Vr_param.Add(IdPessoa);
                Nm_param.Add("@IdCfop");           Vr_param.Add(IdCfop);
                Nm_param.Add("@IdTransportadora"); Vr_param.Add(IdTransportadora);
                Nm_param.Add("@Frete");            Vr_param.Add(Frete);
                Nm_param.Add("@EntSaida");         Vr_param.Add(EntSaida);
                Nm_param.Add("@DtEmissao");        Vr_param.Add(DtEmissao.ToShortDateString());
                Nm_param.Add("@NumFormulario");    Vr_param.Add(NumFormulario);
                Nm_param.Add("@NumNota");          Vr_param.Add(NumNota);
                Nm_param.Add("@VlrProdutos");      Vr_param.Add(Controle.FloatToStr(VlrProdutos, 2));
                Nm_param.Add("@VlrDesconto");      Vr_param.Add(Controle.FloatToStr(VlrDesconto, 2));
                Nm_param.Add("@VlrNota");          Vr_param.Add(Controle.FloatToStr(VlrNota, 2));
                Nm_param.Add("@BIcms");            Vr_param.Add(Controle.FloatToStr(BIcms, 2));
                Nm_param.Add("@VlrIcms");          Vr_param.Add(Controle.FloatToStr(VlrIcms, 2));
                Nm_param.Add("@BIcmsSub");         Vr_param.Add(Controle.FloatToStr(BIcmsSub, 2));
                Nm_param.Add("@VlrIcmsSub");       Vr_param.Add(Controle.FloatToStr(VlrIcmsSub, 2));
                Nm_param.Add("@VlrFrete");         Vr_param.Add(Controle.FloatToStr(VlrFrete, 2));
                Nm_param.Add("@VlrSeguro");        Vr_param.Add(Controle.FloatToStr(VlrSeguro, 2));
                Nm_param.Add("@VlrOutraDesp");     Vr_param.Add(Controle.FloatToStr(VlrOutraDesp, 2));
                Nm_param.Add("@VlrIpi");           Vr_param.Add(Controle.FloatToStr(VlrIpi, 2));
                Nm_param.Add("@Observacao");       Vr_param.Add(Observacao);
                Nm_param.Add("@Status");           Vr_param.Add(Status);
                Nm_param.Add("@QtdeVolume");       Vr_param.Add(QtdeVolume);
                Nm_param.Add("@Especie");          Vr_param.Add(Especie);
                Nm_param.Add("@Marca");            Vr_param.Add(Marca);
                Nm_param.Add("@PesoBruto");        Vr_param.Add(Controle.FloatToStr(PesoBruto, 3));
                Nm_param.Add("@PesoLiquido");      Vr_param.Add(Controle.FloatToStr(PesoLiquido, 3));
                Nm_param.Add("@NmPessoa");         Vr_param.Add(NmPessoa);
                Nm_param.Add("@CnpjCpf");          Vr_param.Add(CnpjCpf);
                Nm_param.Add("@InscUf");           Vr_param.Add(InscUf);
                Nm_param.Add("@Cep");              Vr_param.Add(Cep);
                Nm_param.Add("@Endereco");         Vr_param.Add(Endereco);
                Nm_param.Add("@Numero");           Vr_param.Add(Numero);
                Nm_param.Add("@Bairro");           Vr_param.Add(Bairro);
                Nm_param.Add("@Compl");            Vr_param.Add(Compl);
                Nm_param.Add("@Cidade");           Vr_param.Add(Cidade);
                Nm_param.Add("@IdUf");             Vr_param.Add(IdUf);
                Nm_param.Add("@Telefone");         Vr_param.Add(Telefone);
                Nm_param.Add("@SeqImp");           Vr_param.Add(SeqImp);
                Nm_param.Add("@NFE");              Vr_param.Add(NFE);
                Nm_param.Add("@Pais");             Vr_param.Add(Pais);
                Nm_param.Add("@VlrAcrescimo");     Vr_param.Add(Controle.FloatToStr(VlrAcrescimo, 2));
                Nm_param.Add("@Consumidor");       Vr_param.Add(Consumidor);
                Nm_param.Add("@Atendimento");      Vr_param.Add(Atendimento);
                Nm_param.Add("@DestOperacao");     Vr_param.Add(DestOperacao);
                Nm_param.Add("@Finalidade");       Vr_param.Add(Finalidade);
                Nm_param.Add("@ChaveNfeDev");      Vr_param.Add(ChaveNfeDev);
                Nm_param.Add("@NatOp");            Vr_param.Add(NatOp);
                Nm_param.Add("@CodMun");           Vr_param.Add(CodMun);
                Nm_param.Add("@NumPedido");        Vr_param.Add(NumPedido);
                Nm_param.Add("@ICMSInterno");      Vr_param.Add(Controle.FloatToStr(ICMSInterno, 2));
                Nm_param.Add("@PercDifal");        Vr_param.Add(Controle.FloatToStr(PercDifal, 2));
                Nm_param.Add("@VencFatura");       Vr_param.Add(VencFatura);
                Nm_param.Add("@MeioPag");          Vr_param.Add(MeioPag);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdNota > 0)
            {
                Controle.ExecutaSQL("DELETE FROM NotaFiscal       WHERE Id_Nota=" + IdNota.ToString().Trim());
                Controle.ExecutaSQL("DELETE FROM NotaFiscalItens  WHERE Id_Nota=" + IdNota.ToString().Trim());
            }
        }

        public void Concluir()
        {
            Controle.ExecutaSQL("UPDATE NotaFiscal Set Status=1 Where Id_Nota=" + IdNota.ToString());
            Status = 1;
        }
        public void Cancelar()
        {
            Controle.ExecutaSQL("UPDATE NotaFiscal Set Status=2,DataCancel=convert(DateTime,'" + DateTime.Now.Date.ToShortDateString() + "',103) Where Id_Nota=" + IdNota.ToString());
            Status = 2;            
        }
        public void CalcularImposto()
        {
            DataSet Itens = new DataSet();
            Itens = Controle.ConsultaTabela("SELECT * FROM NOTAFISCALITENS WHERE ID_NOTA=" + IdNota.ToString());
            decimal T_BIcms = 0;
            decimal V_Icms = 0;
            decimal V_Ipi = 0;
            decimal VlrItem = 0;
            if (Itens.Tables[0].Rows.Count > 0)
            {
                for (int I = 0; I <= Itens.Tables[0].Rows.Count - 1; I++)
                {
                    if (decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms"].ToString()) > 0)
                    {
                        if (decimal.Parse(Itens.Tables[0].Rows[I]["PercRed"].ToString( )) > 0)
                            VlrItem = decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString()) - Math.Round((decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString()) * decimal.Parse(Itens.Tables[0].Rows[I]["PercRed"].ToString()) / 100),2);// MidpointRounding.AwayFromZero);
                        else
                            VlrItem = decimal.Parse(Itens.Tables[0].Rows[I]["VlrTotal"].ToString());

                        T_BIcms = T_BIcms + VlrItem;
                        V_Icms = V_Icms + decimal.Parse(Itens.Tables[0].Rows[I]["VlrIcms"].ToString());
                    }
                    if (decimal.Parse(Itens.Tables[0].Rows[I]["VlrIpi"].ToString()) > 0)
                        V_Ipi = V_Ipi + decimal.Parse(Itens.Tables[0].Rows[I]["VlrIpi"].ToString());
                }
            }
            BIcms   = T_BIcms;
            VlrIcms = V_Icms;
            VlrIpi  = V_Ipi;
        }
    }
}
