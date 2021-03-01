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
    public class MensagemAlerta
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private int _IdUsuRem;
        public int IdUsuRem
        {
            get { return _IdUsuRem; }
            set { _IdUsuRem = value; }
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
        private int _TpAlerta;
        public int TpAlerta
        {
            get { return _TpAlerta; }
            set { _TpAlerta = value; }
        }
        private DateTime _DtAlerta;
        public DateTime DtAlerta
        {
            get { return _DtAlerta; }
            set { _DtAlerta = value; }
        }        
        private string _Alerta;
        public string Alerta
        {
            get { return _Alerta; }
            set { _Alerta = value; }
        }
        private int _DiaSemana;
        public int DiaSemana
        {
            get { return _DiaSemana; }
            set { _DiaSemana = value; }
        }
        private int _DiaMes;
        public int DiaMes
        {
            get { return _DiaMes; }
            set { _DiaMes = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc    = 0;
            IdUsuario = 0;
            IdUsuRem  = 0;
            IdPessoa  = 0;
            Data      = DateTime.Now;
            TpAlerta  = 0;
            DtAlerta  = DateTime.Now;            
            Alerta    = "";
            DiaSemana = 0;
            DiaMes    = 0;
            Status    = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ALERTAS WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc    = Id;
                    IdUsuario = int.Parse(Tabela["Id_Usuario"].ToString());
                    IdUsuRem  = int.Parse(Tabela["Id_UsuRem"].ToString());
                    IdPessoa  = int.Parse(Tabela["Id_Pessoa"].ToString());                    
                    Data      = DateTime.Parse(Tabela["Data"].ToString());
                    TpAlerta  = int.Parse(Tabela["TpAlerta"].ToString());
                    DtAlerta  = DateTime.Parse(Tabela["DtAlerta"].ToString());                    
                    Alerta    = Tabela["Alerta"].ToString().Trim();                    
                    DiaSemana = int.Parse(Tabela["DiaSemana"].ToString());
                    DiaMes    = int.Parse(Tabela["DiaMes"].ToString());
                    Status    = int.Parse(Tabela["Status"].ToString());                    
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
                sSQL = "UPDATE ALERTAS SET Id_Lanc=@Id,Id_Usuario=@IdUsuario,Id_UsuRem=@IdUsuRem,Id_Pessoa=@IdPessoa,Data=Convert(DateTime,@Data,103),TpAlerta=@TpAlerta,DtAlerta=Convert(DateTime,@DtAlerta,103),Alerta=@Alerta,"+
                       "DiaSemana=@DiaSemana,DiaMes=@DiaMes,Status=@Status Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("Alertas");
                sSQL = "INSERT INTO ALERTAS  (Id_Lanc,Id_Usuario,Id_UsuRem,Id_Pessoa,Data,TpAlerta,DtAlerta,Alerta,DiaSemana,DiaMes,Status)" +
                       " Values(@Id,@IdUsuario,@IdUsuRem,@IdPessoa,Convert(DateTime,@Data,103),@TpAlerta,Convert(DateTime,@DtAlerta,103),@Alerta,@DiaSemana,@DiaMes,@Status)";
            }
            if (sSQL != "")
            {   
                Nm_param.Add("@Id");        Vr_param.Add(IdLanc);
                Nm_param.Add("@IdUsuario"); Vr_param.Add(IdUsuario);
                Nm_param.Add("@IdUsuRem");  Vr_param.Add(IdUsuRem);
                Nm_param.Add("@IdPessoa");  Vr_param.Add(IdPessoa);
                Nm_param.Add("@Data");      Vr_param.Add(Data.ToShortDateString());
                Nm_param.Add("@TpAlerta");  Vr_param.Add(TpAlerta);
                Nm_param.Add("@DtAlerta");  Vr_param.Add(DtAlerta.ToShortDateString());
                Nm_param.Add("@Alerta");    Vr_param.Add(Alerta);
                Nm_param.Add("@DiaSemana"); Vr_param.Add(DiaSemana);
                Nm_param.Add("@DiaMes");    Vr_param.Add(DiaMes);
                Nm_param.Add("@Status");    Vr_param.Add(Status);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
            {
                Controle.ExecutaSQL("DELETE FROM ALERTAS  WHERE Id_Lanc=" + IdLanc.ToString().Trim());                
            }
        }

    }
}
