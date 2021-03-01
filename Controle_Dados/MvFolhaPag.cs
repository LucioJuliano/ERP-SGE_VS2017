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
    public class MvFolhaPag
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdFunc;
        public int IdFunc
        {
            get { return _IdFunc; }
            set { _IdFunc = value; }
        }
        private string _MesAno;
        public string MesAno
        {
            get { return _MesAno; }
            set { _MesAno = value; }
        }        
        private int _IdProvDesc;
        public int IdProvDesc
        {
            get { return _IdProvDesc; }
            set { _IdProvDesc = value; }
        }
        private decimal _QtdeRef;
        public decimal QtdeRef
        {
            get { return _QtdeRef; }
            set { _QtdeRef = value; }
        }
        private decimal _Valor;
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private decimal _VlrDigitado;
        public decimal VlrDigitado
        {
            get { return _VlrDigitado; }
            set { _VlrDigitado = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc        = 0;
            IdFunc        = 0;            
            MesAno        = "";
            IdProvDesc    = 0;
            Valor         = 0;
            QtdeRef       = 0;
            Descricao     = "";
            VlrDigitado   = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM MvFolhaPag WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc        = Id;
                    IdFunc        = int.Parse(Tabela["Id_Func"].ToString());
                    MesAno        = Tabela["MesAno"].ToString().Trim();                    
                    IdProvDesc    = int.Parse(Tabela["Id_ProvDesc"].ToString());
                    Valor         = decimal.Parse(Tabela["Valor"].ToString());
                    QtdeRef       = decimal.Parse(Tabela["Qtde_Ref"].ToString());
                    VlrDigitado   = decimal.Parse(Tabela["VlrDigitado"].ToString());
                    Descricao     = Tabela["Descricao"].ToString().Trim();
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
                sSQL = "UPDATE MvFolhaPag SET Id_Lanc=@Id,Id_Func=@IdFunc,MesAno=@MesAno,Id_ProvDesc=@IdProvDesc,Qtde_Ref=@QtdeRef,Valor=@Valor,"+
                       "Descricao=@Descricao,VlrDigitado=@VlrDigitado Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("MvFolhaPag");
                sSQL = "INSERT INTO MvFolhaPag (Id_Lanc,Id_Func,MesAno,Id_ProvDesc,Qtde_Ref,Valor,Descricao,VlrDigitado) " +
                       " VALUES (@Id,@IdFunc,@MesAno,@IdProvDesc,@QtdeRef,@Valor,@Descricao,@VlrDigitado)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");          Vr_param.Add(IdLanc);
                Nm_param.Add("@IdFunc");      Vr_param.Add(IdFunc);
                Nm_param.Add("@MesAno");      Vr_param.Add(MesAno);                
                Nm_param.Add("@IdProvDesc");  Vr_param.Add(IdProvDesc);
                Nm_param.Add("@Valor");       Vr_param.Add(Controle.FloatToStr(Valor, 2));
                Nm_param.Add("@QtdeRef");     Vr_param.Add(Controle.FloatToStr(QtdeRef, 2));
                Nm_param.Add("@VlrDigitado"); Vr_param.Add(Controle.FloatToStr(VlrDigitado, 2));                
                Nm_param.Add("@Descricao");   Vr_param.Add(Descricao);
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM MvFolhaPag WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}
