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
    public class PrevCustos
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _Ano;
        public int Ano
        {
            get { return _Ano; }
            set { _Ano = value; }
        }
        private int _IdFilial;
        public int IdFilial
        {
            get { return _IdFilial; }
            set { _IdFilial = value; }
        }
        private int _IdDepart;
        public int IdDepart
        {
            get { return _IdDepart; }
            set { _IdDepart = value; }
        }
        private int _IdCusto;
        public int IdCusto
        {
            get { return _IdCusto; }
            set { _IdCusto = value; }
        }        
        private decimal _Janeiro;
        public decimal Janeiro
        {
            get { return _Janeiro; }
            set { _Janeiro = value; }
        }
        private decimal _Fevereiro;
        public decimal Fevereiro
        {
            get { return _Fevereiro; }
            set { _Fevereiro = value; }
        }
        private decimal _Marco;
        public decimal Marco
        {
            get { return _Marco; }
            set { _Marco = value; }
        }
        private decimal _Abril;
        public decimal Abril
        {
            get { return _Abril; }
            set { _Abril = value; }
        }
        private decimal _Maio;
        public decimal Maio
        {
            get { return _Maio; }
            set { _Maio = value; }
        }
        private decimal _Junho;
        public decimal Junho
        {
            get { return _Junho; }
            set { _Junho = value; }
        }
        private decimal _Julho;
        public decimal Julho
        {
            get { return _Julho; }
            set { _Julho = value; }
        }
        private decimal _Agosto;
        public decimal Agosto
        {
            get { return _Agosto; }
            set { _Agosto = value; }
        }
        private decimal _Setembro;
        public decimal Setembro
        {
            get { return _Setembro; }
            set { _Setembro = value; }
        }
        private decimal _Outubro;
        public decimal Outubro
        {
            get { return _Outubro; }
            set { _Outubro = value; }
        }
        private decimal _Novembro;
        public decimal Novembro
        {
            get { return _Novembro; }
            set { _Novembro = value; }
        }
        private decimal _Dezembro;
        public decimal Dezembro
        {
            get { return _Dezembro; }
            set { _Dezembro = value; }
        }
        private decimal _Total;
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public Funcoes Controle;

        public void LerDados(int Id)
        {
            IdLanc    = 0;
            IdFilial  = 0;
            IdDepart  = 0;
            IdCusto   = 0;
            Ano       = DateTime.Now.Year;
            Janeiro   = 0;
            Fevereiro = 0;
            Marco     = 0;
            Abril     = 0;
            Maio      = 0;
            Junho     = 0;
            Julho     = 0;
            Agosto    = 0;
            Setembro  = 0;
            Outubro   = 0;
            Novembro  = 0;
            Dezembro  = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM PrevCustos WHERE Id_Lanc=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdLanc    = Id;
                    Ano       = int.Parse(Tabela["Ano"].ToString());
                    IdCusto   = int.Parse(Tabela["Id_Custo"].ToString());
                    IdFilial  = int.Parse(Tabela["Id_Filial"].ToString());
                    IdDepart  = int.Parse(Tabela["Id_Departamento"].ToString());
                    Janeiro   = decimal.Parse(Tabela["Janeiro"].ToString());
                    Fevereiro = decimal.Parse(Tabela["Fevereiro"].ToString());
                    Marco     = decimal.Parse(Tabela["Marco"].ToString());
                    Abril     = decimal.Parse(Tabela["Abril"].ToString());
                    Maio      = decimal.Parse(Tabela["Maio"].ToString());
                    Junho     = decimal.Parse(Tabela["Junho"].ToString());
                    Julho     = decimal.Parse(Tabela["Julho"].ToString());
                    Agosto    = decimal.Parse(Tabela["Agosto"].ToString());
                    Setembro  = decimal.Parse(Tabela["Setembro"].ToString());
                    Outubro   = decimal.Parse(Tabela["Outubro"].ToString());
                    Novembro  = decimal.Parse(Tabela["Novembro"].ToString());
                    Dezembro  = decimal.Parse(Tabela["Dezembro"].ToString());
                    Total     = decimal.Parse(Tabela["Total"].ToString());
                    
                }
            }            
        }

        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdCusto > 0)
            {
                sSQL = "UPDATE PrevCustoS SET Id_Lanc=@Id,Ano=@Ano,Id_Filial=@IdFilial,Id_Departamento=@IdDepart,Id_Custo=@IdCusto,Janeiro=@Janeiro,Fevereiro=@Fevereiro,Marco=@Marco,Abril=@Abril,Maio=@Maio,Junho=@Junho,Julho=@Julho,"+
                       "Agosto=@Agosto,Setembro=@Setembro,Outubro=@Outubro,Novembro=@Novembro,Dezembro=@Dezembro,Total=@Total Where Id_Lanc=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdLanc);
            }
            else
            {
                IdLanc = Controle.ProximoID("PREVCUSTOS");
                sSQL = "INSERT INTO PrevCustos (Id_Lanc,Ano,Id_Filial,Id_Departamento,Id_Custo,Janeiro,Fevereiro,Marco,Abril,Maio,Junho,Julho,Agosto,Setembro,Outubro,Novembro,Dezembro,Total)" +
                       "VALUES (@Id,@Ano,@IdFilial,@IdDepart,@IdCusto,@Janeiro,@Fevereiro,@Marco,@Abril,@Maio,@Junho,@Julho,@Agosto,@Setembro,@Outubro,@Novembro,@Dezembro,@Total)";
            }
            if (sSQL != "")
            {
                Total = (Janeiro + Fevereiro + Marco + Abril + Maio + Junho + Julho + Agosto + Setembro + Outubro + Novembro + Dezembro);
                Nm_param.Add("@Id"); Vr_param.Add(IdLanc);
                Nm_param.Add("@Ano"); Vr_param.Add(Ano);
                Nm_param.Add("@IdFilial"); Vr_param.Add(IdFilial);
                Nm_param.Add("@IdDepart"); Vr_param.Add(IdDepart);
                Nm_param.Add("@IdCusto"); Vr_param.Add(IdCusto);
                Nm_param.Add("@Janeiro"); Vr_param.Add(Controle.FloatToStr(Janeiro, 2));
                Nm_param.Add("@Fevereiro"); Vr_param.Add(Controle.FloatToStr(Fevereiro, 2));
                Nm_param.Add("@Marco"); Vr_param.Add(Controle.FloatToStr(Marco, 2));
                Nm_param.Add("@Abril"); Vr_param.Add(Controle.FloatToStr(Abril, 2));
                Nm_param.Add("@Maio"); Vr_param.Add(Controle.FloatToStr(Maio, 2));
                Nm_param.Add("@Junho"); Vr_param.Add(Controle.FloatToStr(Junho, 2));
                Nm_param.Add("@Julho"); Vr_param.Add(Controle.FloatToStr(Julho, 2));
                Nm_param.Add("@Agosto"); Vr_param.Add(Controle.FloatToStr(Agosto, 2));
                Nm_param.Add("@Setembro"); Vr_param.Add(Controle.FloatToStr(Setembro, 2));
                Nm_param.Add("@Outubro"); Vr_param.Add(Controle.FloatToStr(Outubro, 2));
                Nm_param.Add("@Novembro"); Vr_param.Add(Controle.FloatToStr(Novembro, 2));
                Nm_param.Add("@Dezembro"); Vr_param.Add(Controle.FloatToStr(Dezembro, 2));
                Nm_param.Add("@Total"); Vr_param.Add(Controle.FloatToStr(Total, 2));                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdLanc > 0)
                Controle.ExecutaSQL("DELETE FROM PrevCustos WHERE Id_Lanc=" + IdLanc.ToString().Trim());
        }
    }
}
