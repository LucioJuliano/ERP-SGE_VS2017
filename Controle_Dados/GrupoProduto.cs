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
    public class GrupoProduto
    {
        private int _IdGrupo;
        public int IdGrupo
        {
            get { return _IdGrupo; }
            set { _IdGrupo = value; }
        }
        private string _Grupo;
        public string Grupo
        {
            get { return _Grupo; }
            set { _Grupo = value; }
        }
        private int _CstDief;
        public int CstDief
        {
            get { return _CstDief; }
            set { _CstDief = value; }
        }
        private int _CstSped;
        public int CstSped
        {
            get { return _CstSped; }
            set { _CstSped = value; }
        }
        private int _ListaWeb;
        public int ListaWeb
        {
            get { return _ListaWeb; }
            set { _ListaWeb = value; }
        }
        private int _ListaEstMin;
        public int ListaEstMin
        {
            get { return _ListaEstMin; }
            set { _ListaEstMin = value; }
        }
        private int _ListaVenda;
        public int ListaVenda
        {
            get { return _ListaVenda; }
            set { _ListaVenda = value; }
        }

        private int _ExcluirNFETrans;
        public int ExcluirNFETrans 
        {
            get { return _ExcluirNFETrans; }
            set { _ExcluirNFETrans = value; }
        }

        private int _Estoque;
        public int Estoque
        {
            get { return _Estoque; }
            set { _Estoque = value; }
        }

        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }

        private decimal _PercVerDesc;
        public decimal PercVerDesc
        {
            get { return _PercVerDesc; }
            set { _PercVerDesc = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdGrupo         = 0;
            Grupo           = "";
            CstDief         = 0;
            CstSped         = 0;
            ListaWeb        = 0;
            ListaEstMin     = 0;
            ListaVenda      = 0;
            ExcluirNFETrans = 0;
            Estoque         = 0;
            Ativo           = 0;
            PercVerDesc     = 0;
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM GrupoProduto WHERE Id_Grupo=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdGrupo     = Id;
                    Grupo       = Tabela["Grupo"].ToString().Trim();
                    CstDief     = int.Parse(Tabela["Cst_Dief"].ToString());
                    CstSped     = int.Parse(Tabela["Cst_Sped"].ToString());
                    PercVerDesc = decimal.Parse(Tabela["PercVerDesc"].ToString());
                    if (Tabela["ListaWeb"].ToString() != "")
                        ListaWeb = int.Parse(Tabela["ListaWeb"].ToString());
                    if (Tabela["ListaEstMin"].ToString() != "")
                        ListaEstMin = int.Parse(Tabela["ListaEstMin"].ToString());
                    if (Tabela["ListaVenda"].ToString() != "")
                        ListaVenda = int.Parse(Tabela["ListaVenda"].ToString());
                    if (Tabela["ExcluirNFETrans"].ToString() != "")
                        ExcluirNFETrans = int.Parse(Tabela["ExcluirNFETrans"].ToString());
                    if (Tabela["Estoque"].ToString() != "")
                        Estoque = int.Parse(Tabela["Estoque"].ToString());
                    if (Tabela["Ativo"].ToString() != "")
                        Ativo = int.Parse(Tabela["Ativo"].ToString());                            
                }
            }            
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdGrupo > 0)
            {
                sSQL = "UPDATE GrupoProduto SET Id_Grupo=@Id,Grupo=@Grupo,Cst_Dief=@CstDief,Cst_Sped=@CstSped,"+
                       "ListaWeb=@ListaWeb,ListaEstMin=@ListaEstMin,ListaVenda=@ListaVenda,ExcluirNFETrans=@ExcluirNFETrans,Estoque=@Estoque,Ativo=@Ativo,PercVerDesc=@PercVerDesc  Where Id_Grupo=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdGrupo);
            }
            else
            {
                IdGrupo = Controle.ProximoID("GrupoProduto");
                sSQL = "INSERT INTO GrupoProduto (ID_Grupo,Grupo,Cst_Dief,Cst_Sped,ListaWeb,ListaEstMin,ListaVenda,ExcluirNFETrans,Estoque,Ativo,PercVerDesc) " +
                       " VALUES(@Id,@Grupo,@CstDief,@CstSped,@ListaWeb,@ListaEstMin,@ListaVenda,@ExcluirNFETrans,@Estoque,@Ativo,@PercVerDesc)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdGrupo);
                Nm_param.Add("@Grupo"); Vr_param.Add(Grupo);
                Nm_param.Add("@CstDief"); Vr_param.Add(CstDief);
                Nm_param.Add("@CstSped"); Vr_param.Add(CstSped);
                Nm_param.Add("@ListaWeb"); Vr_param.Add(ListaWeb);
                Nm_param.Add("@ListaEstMin"); Vr_param.Add(ListaEstMin);
                Nm_param.Add("@ListaVenda"); Vr_param.Add(ListaVenda);
                Nm_param.Add("@ExcluirNFETrans"); Vr_param.Add(ExcluirNFETrans);
                Nm_param.Add("@Estoque"); Vr_param.Add(Estoque);
                Nm_param.Add("@Ativo"); Vr_param.Add(Ativo);
                Nm_param.Add("@PercVerDesc"); Vr_param.Add(Controle.FloatToStr(PercVerDesc, 2));  
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdGrupo > 0)
            {
                Controle.ExecutaSQL("DELETE FROM GrupoProduto WHERE Id_Grupo=" + IdGrupo.ToString().Trim());
            }
        }
    }
}
