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
    public class ProvDescontos
    {
        private int _IdCodigo;
        public int IdCodigo
        {
            get { return _IdCodigo; }
            set { _IdCodigo = value; }
        }
        private int _ProvDesc;
        public int ProvDesc
        {
            get { return _ProvDesc; }
            set { _ProvDesc = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }        
        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        private int _FolhaBruta;
        public int FolhaBruta 
        {
            get { return _FolhaBruta; }
            set { _FolhaBruta = value; }
        }
        

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdCodigo     = 0;
            ProvDesc     = 0;
            Descricao    = "";            
            Tipo         = 0;
            Valor        = 0;
            FolhaBruta   = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM ProventosDescontos WHERE Id_Codigo=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdCodigo     = Id;                    
                    ProvDesc     = int.Parse(Tabela["ProvDesc"].ToString());
                    Descricao    = Tabela["Descricao"].ToString().Trim();                    
                    Tipo         = int.Parse(Tabela["Tipo"].ToString());
                    Valor        = decimal.Parse(Tabela["Valor"].ToString());
                    if (Tabela["FolhaBruta"].ToString() != "")
                        FolhaBruta = int.Parse(Tabela["FolhaBruta"].ToString());
                }
            }            
        }


        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdCodigo > 0)
            {
                sSQL = "UPDATE ProventosDescontos SET Id_Codigo=@Id,ProvDesc=@ProvDesc,Descricao=@Descricao,Tipo=@Tipo,Valor=@Valor,FolhaBruta=@FolhaBruta Where Id_Codigo=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdCodigo);
            }
            else
            {
                IdCodigo = Controle.ProximoID("ProventosDescontos");
                sSQL = "INSERT INTO ProventosDescontos (Id_Codigo,ProvDesc,Descricao,Tipo,Valor,FolhaBruta) " +
                       " VALUES (@Id,@ProvDesc,@Descricao,@Tipo,@Valor,@FolhaBruta)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");           Vr_param.Add(IdCodigo);
                Nm_param.Add("@ProvDesc");     Vr_param.Add(ProvDesc);
                Nm_param.Add("@Descricao");    Vr_param.Add(Descricao);                
                Nm_param.Add("@Tipo");         Vr_param.Add(Tipo);
                Nm_param.Add("@Valor");        Vr_param.Add(Controle.FloatToStr(Valor, 2));
                Nm_param.Add("@FolhaBruta");   Vr_param.Add(FolhaBruta);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdCodigo > 0)
                Controle.ExecutaSQL("DELETE FROM ProventosDescontos WHERE Id_Codigo=" + IdCodigo.ToString().Trim());
        }

    }
}
