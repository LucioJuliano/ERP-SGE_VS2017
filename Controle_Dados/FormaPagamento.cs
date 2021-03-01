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
    public class FormaPagamento
    {
        private int _IdFormaPgto;
        public int IdFormaPgto
        {
            get { return _IdFormaPgto; }
            set { _IdFormaPgto = value; }
        }
        private string _FormaPgto;
        public string FormaPgto
        {
            get { return _FormaPgto; }
            set { _FormaPgto = value; }
        }
        private int _NumParcelas;
        public int NumParcelas
        {
            get { return _NumParcelas; }
            set { _NumParcelas = value; }
        }
        private int _PrimParcela;
        public int PrimParcela
        {
            get { return _PrimParcela; }
            set { _PrimParcela = value; }
        }
        private int _Intervalo;
        public int Intervalo
        {
            get { return _Intervalo; }
            set { _Intervalo = value; }
        }
        private int _Financeiro;
        public int Financeiro
        {
            get { return _Financeiro; }
            set { _Financeiro = value; }
        }
        private int _Baixa;
        public int Baixa
        {
            get { return _Baixa; }
            set { _Baixa = value; }
        }
        private decimal _Desconto;
        public decimal Desconto
        {
            get { return _Desconto; }
            set { _Desconto = value; }
        }
        private int _IdTpDocumento;
        public int IdTpDocumento
        {
            get { return _IdTpDocumento; }
            set { _IdTpDocumento = value; }
        }
        private int _IdServidor;
        public int IdServidor
        {
            get { return _IdServidor; }
            set { _IdServidor = value; }
        }

        private decimal _VlrParcelamento;
        public decimal VlrParcelamento
        {
            get { return _VlrParcelamento; }
            set { _VlrParcelamento = value; }
        }

        private int _VerDebito;
        public int VerDebito
        {
            get { return _VerDebito; }
            set { _VerDebito = value; }
        }
        private int _VerCredito;
        public int VerCredito
        {
            get { return _VerCredito; }
            set { _VerCredito = value; }
        }

        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }
        private int _BloqPF;
        public int BloqPF
        {
            get { return _BloqPF; }
            set { _BloqPF = value; }
        }

        private int _LibClieNovo;
        public int LibClieNovo
        {
            get { return _LibClieNovo; }
            set { _LibClieNovo = value; }
        }
        

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdFormaPgto     = 0;
            FormaPgto       = "";
            NumParcelas     = 0;
            PrimParcela     = 0;
            Intervalo       = 0;
            Financeiro      = 0;
            Baixa           = 0;
            Desconto        = 0;
            IdTpDocumento   = 0;
            IdServidor      = 0;
            VlrParcelamento = 0;
            VerCredito      = 0;
            VerDebito       = 0;
            Ativo           = 0;
            BloqPF          = 0;
            LibClieNovo     = 0;


            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM FormaPagamento WHERE Id_FormaPgto=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdFormaPgto   = Id;
                    FormaPgto     = Tabela["FormaPgto"].ToString().Trim();
                    NumParcelas   = int.Parse(Tabela["NumParcelas"].ToString());
                    PrimParcela   = int.Parse(Tabela["PrimParcela"].ToString());
                    Intervalo     = int.Parse(Tabela["Intervalo"].ToString());
                    Financeiro    = int.Parse(Tabela["Financeiro"].ToString());
                    Baixa         = int.Parse(Tabela["Baixa"].ToString());
                    Desconto      = decimal.Parse(Tabela["Desconto"].ToString());
                    IdTpDocumento = int.Parse(Tabela["Id_TipoDocumento"].ToString());
                    IdServidor    = int.Parse(Tabela["IdServidor"].ToString());

                    if (Tabela["VlrParcelamento"].ToString() != "")
                        VlrParcelamento = decimal.Parse(Tabela["VlrParcelamento"].ToString());

                    if (Tabela["VerDebito"].ToString() != "")
                        VerDebito = int.Parse(Tabela["VerDebito"].ToString());

                    if (Tabela["VerCredito"].ToString() != "")
                        VerCredito = int.Parse(Tabela["VerCredito"].ToString());

                    if (Tabela["Ativo"].ToString() != "")
                        Ativo = int.Parse(Tabela["Ativo"].ToString());

                    if (Tabela["BloqPF"].ToString() != "")
                        BloqPF = int.Parse(Tabela["BloqPF"].ToString());

                    if (Tabela["LibClieNovo"].ToString() != "")
                        LibClieNovo = int.Parse(Tabela["LibClieNovo"].ToString());
                    



                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdFormaPgto > 0)
            {
                sSQL = "UPDATE FormaPagamento SET Id_FormaPgto=@Id,FormaPgto=@FormaPgto,NumParcelas=@NumParcelas,PrimParcela=@PrimParcela,Intervalo=@Intervalo,"+
                       "Financeiro=@Financeiro,Baixa=@Baixa,Desconto=@Desconto,Id_TipoDocumento=@IdTpDocumento,IdServidor=@IdServidor,VlrParcelamento=@VlrParcelamento,"+
                       "VerDebito=@VerDebito,VerCredito=@VerCredito,Ativo=@Ativo,BloqPF=@BloqPF,LibClieNovo=@LibClieNovo Where Id_FormaPgto=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdFormaPgto);
            }
            else
            {
                IdFormaPgto = Controle.ProximoID("FormaPagamento");
                sSQL = "INSERT INTO FormaPagamento (ID_FormaPgto,FormaPgto,NumParcelas,PrimParcela,Intervalo,Financeiro,Baixa,Desconto,Id_TipoDocumento,IdServidor,VlrParcelamento,VerDebito,VerCredito,Ativo,BloqPF,LibClieNovo)" +
                       " VALUES(@Id,@FormaPgto,@NumParcelas,@PrimParcela,@Intervalo,@Financeiro,@Baixa,@Desconto,@IdTpDocumento,@IdServidor,@VlrParcelamento,@VerDebito,@VerCredito,@Ativo,@BloqPF,@LibClieNovo)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdFormaPgto);
                Nm_param.Add("@FormaPgto"); Vr_param.Add(FormaPgto);
                Nm_param.Add("@NumParcelas"); Vr_param.Add(NumParcelas);
                Nm_param.Add("@PrimParcela"); Vr_param.Add(PrimParcela);
                Nm_param.Add("@Intervalo"); Vr_param.Add(Intervalo);
                Nm_param.Add("@Financeiro"); Vr_param.Add(Financeiro);
                Nm_param.Add("@Baixa"); Vr_param.Add(Baixa);
                Nm_param.Add("@Desconto"); Vr_param.Add(Controle.FloatToStr(Desconto,2));
                Nm_param.Add("@IdTpDocumento"); Vr_param.Add(IdTpDocumento);
                Nm_param.Add("@IdServidor"); Vr_param.Add(IdServidor);
                Nm_param.Add("@VlrParcelamento"); Vr_param.Add(Controle.FloatToStr(VlrParcelamento, 2));
                Nm_param.Add("@VerDebito"); Vr_param.Add(VerDebito);
                Nm_param.Add("@VerCredito"); Vr_param.Add(VerCredito);
                Nm_param.Add("@Ativo"); Vr_param.Add(Ativo);
                Nm_param.Add("@BloqPF"); Vr_param.Add(BloqPF);
                Nm_param.Add("@LibClieNovo"); Vr_param.Add(LibClieNovo);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdFormaPgto > 0)
            {
                Controle.ExecutaSQL("DELETE FROM FormaPagamento WHERE Id_FormaPgto=" + IdFormaPgto.ToString().Trim());
            }
        }
    }
}
