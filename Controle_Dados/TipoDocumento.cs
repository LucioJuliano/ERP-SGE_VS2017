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
    public class TipoDocumento
    {
        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        private string _Documento;
        public string Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        private decimal _TxAdm;
        public decimal TxAdm
        {
            get { return _TxAdm; }
            set { _TxAdm = value; }
        }
        private decimal _TxJuro;
        public decimal TxJuro
        {
            get { return _TxJuro; }
            set { _TxJuro = value; }
        }
        private decimal _TxMulta;
        public decimal TxMulta
        {
            get { return _TxMulta; }
            set { _TxMulta = value; }
        }

        private int _CodEcf;
        public int CodEcf
        {
            get { return _CodEcf; }
            set { _CodEcf = value; }
        }
        private int _Baixa;
        public int Baixa
        {
            get { return _Baixa; }
            set { _Baixa = value; }
        }
        private int _Dias;
        public int Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }
        private int _IdServidor;
        public int IdServidor
        {
            get { return _IdServidor; }
            set { _IdServidor = value; }
        }
        private int _ResumoCx;
        public int ResumoCx
        {
            get { return _ResumoCx; }
            set { _ResumoCx = value; }
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

        private int _MFe;
        public int MFe
        {
            get { return _MFe; }
            set { _MFe = value; }
        }

        private string _Adquirente;
        public string Adquirente
        {
            get { return _Adquirente; }
            set { _Adquirente = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdDocumento = 0;
            Documento   = "";
            TxAdm       = 0;
            TxMulta     = 0;
            TxJuro      = 0;
            CodEcf      = 0;
            Baixa       = 0;
            Dias        = 0;
            IdServidor  = 0;
            ResumoCx    = 0;
            Ativo       = 0;
            BloqPF      = 0;
            MFe         = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM TipoDocumento WHERE Id_Documento=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdDocumento = Id;
                    Documento   = Tabela["Documento"].ToString().Trim();
                    TxAdm       = decimal.Parse(Tabela["TxAdm"].ToString());
                    TxJuro      = decimal.Parse(Tabela["TxJuro"].ToString());
                    TxMulta     = decimal.Parse(Tabela["TxMulta"].ToString());
                    CodEcf      = int.Parse(Tabela["CodECF"].ToString());
                    Baixa       = int.Parse(Tabela["Baixa"].ToString());
                    Dias        = int.Parse(Tabela["Dias"].ToString());
                    IdServidor  = int.Parse(Tabela["IdServidor"].ToString());
                    Adquirente  = Tabela["Adquirente"].ToString();

                    if (Tabela["ResumoCx"].ToString() != "")
                        ResumoCx = int.Parse(Tabela["ResumoCx"].ToString());

                    if (Tabela["Ativo"].ToString() != "")
                        Ativo = int.Parse(Tabela["Ativo"].ToString());

                    if (Tabela["BloqPF"].ToString() != "")
                        BloqPF = int.Parse(Tabela["BloqPF"].ToString());

                    if (Tabela["MFe"].ToString() != "")
                        MFe = int.Parse(Tabela["MFe"].ToString());

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
                sSQL = "UPDATE TipoDocumento SET Id_Documento=@Id,Documento=@Nm,TxAdm=@TxAdm,TxJuro=@TxJuro,TxMulta=@TxMulta,CodEcf=@CodEcf,Baixa=@Baixa,Dias=@Dias,IdServidor=@IdServidor,ResumoCx=@ResumoCx,Ativo=@Ativo,BloqPF=@BloqPF,MFe=@MFe,Adquirente=@Adquirente Where Id_Documento=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdDocumento);
            }
            else
            {
                IdDocumento = Controle.ProximoID("TipoDocumento");
                sSQL = "INSERT INTO TipoDocumento (ID_Documento,Documento,TxAdm,TxJuro,TxMulta,CodECF,Baixa,Dias,IdServidor,ResumoCx,Ativo,BloqPF,MFe,Adquirente) VALUES(@Id,@Nm,@TxAdm,@TxJuro,@TxMulta,@CodEcf,@Baixa,@Dias,@IdServidor,@ResumoCx,@Ativo,@BloqPF,@MFe,@Adquirente)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");         Vr_param.Add(IdDocumento);
                Nm_param.Add("@Nm");         Vr_param.Add(Documento);
                Nm_param.Add("@TxAdm");      Vr_param.Add(Controle.FloatToStr(TxAdm,2));
                Nm_param.Add("@TxJuro");     Vr_param.Add(Controle.FloatToStr(TxJuro, 2));
                Nm_param.Add("@TxMulta");    Vr_param.Add(Controle.FloatToStr(TxMulta, 2));
                Nm_param.Add("@CodEcf");     Vr_param.Add(CodEcf);
                Nm_param.Add("@Baixa");      Vr_param.Add(Baixa);
                Nm_param.Add("@Dias");       Vr_param.Add(Dias);
                Nm_param.Add("@IdServidor"); Vr_param.Add(IdServidor);
                Nm_param.Add("@ResumoCx");   Vr_param.Add(ResumoCx);
                Nm_param.Add("@Ativo");      Vr_param.Add(Ativo);
                Nm_param.Add("@BloqPF");     Vr_param.Add(BloqPF);
                Nm_param.Add("@MFe");        Vr_param.Add(MFe);
                Nm_param.Add("@Adquirente"); Vr_param.Add(Adquirente);

                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdDocumento > 0)
            {
                Controle.ExecutaSQL("DELETE FROM TipoDocumento WHERE Id_Documento=" + IdDocumento.ToString().Trim());
            }
        }

    }
}

