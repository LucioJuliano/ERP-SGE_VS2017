using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controle_Dados;
using System.Data;
using System.Data.SqlClient;

namespace Controles
{
    [Serializable()]
    public class Verificar
    {
        public Funcoes Controle = new Funcoes();
        public enum StatusCaixaBalcao
        {
            Aberto,
            NaoAberto,
            Fechado,
            DtCxDif
        }

        public bool Verificar_CadFilial(int ID,string CNPJ)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM Empresa_Filial WHERE CNPJ='" + CNPJ.Trim() + "'");
            if (Tabela.HasRows)
            {
                Tabela.Read();
                if (Tabela["ID_Filial"].ToString().Trim() == ID.ToString())
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        public bool Verificar_ExisteCadastro(int ID, string Campo, string sSQL)
        {
            
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL(sSQL);
            if (Tabela.HasRows)
            {
                Tabela.Read();
                if (Tabela[Campo].ToString().Trim() == ID.ToString())
                    return false;
                else
                    return true;
            }
            else
                return false;
            
        }
        public int Verificar_ExisteCadastro(string Campo, string sSQL)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL(sSQL);
            if (Tabela.HasRows)
            {
                Tabela.Read();
                return int.Parse(Tabela[Campo].ToString().Trim());                    
            }
            else
                return 0;
        }
        
        public bool VerificarCadCFOP(int ID, string NCFOP)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM CFOP WHERE CFOP='" + NCFOP.Trim() + "'");
            if (Tabela.HasRows)
            {
                Tabela.Read();
                if (Tabela["ID_CFOP"].ToString().Trim() == ID.ToString())
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        public int Busca_IdUF(string UF)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM Estados WHERE Sigla='" + UF.Trim() + "'");
            if (Tabela.HasRows)
            {
                Tabela.Read();
                return int.Parse(Tabela["ID_UF"].ToString());                
            }
            else
                return 0;
        }
        public string Busca_SiglaUF(int Id)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM Estados WHERE Id_UF=" + Id.ToString());
            if (Tabela.HasRows)
            {
                Tabela.Read();
                return Tabela["SIGLA"].ToString().Trim();
            }
            else
                return "";
        }
        public bool VerificarExite_PrdCotacao(int IdCotacao, int IdProduto)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM CotacaoItens WHERE Id_Cotacao=" + IdCotacao.ToString() + " and Id_Produto=" + IdProduto.ToString());
            if (Tabela.HasRows)
               return true;
            else
               return false;
        }
        public bool VerificarExite_PessoaCotacao(int IdCotacao, int IdPessoa, int IdItem)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM CotacaoPessoas WHERE Id_Cotacao=" + IdCotacao.ToString() + " and IdPessoa=" + IdPessoa.ToString() + " and Id_Item=" + IdItem.ToString());
            if (Tabela.HasRows)
                return true;
            else
                return false;
        }
        public bool VerificarExite_PrdPedCompra(int IdDocumento, int IdProduto)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM PedCompraItens WHERE Id_Documento=" + IdDocumento.ToString() + " and Id_Produto=" + IdProduto.ToString());
            if (Tabela.HasRows)
                return true;
            else
                return false;
        }
        public bool VerificarExite_LancProduto(string sSql)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL(sSql);
            if (Tabela.HasRows)
                return true;
            else
                return false;
        }
        public StatusCaixaBalcao StatusCxaBalcao(int IdUsuario)
        {            
            CaixaBalcao Cx = new CaixaBalcao();
            Cx.Controle = Controle;
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM CaixaBalcao WHERE Status=0 and Id_Usuario="+IdUsuario.ToString());
            if (Tabela.HasRows)
            {
                Tabela.Read();
                if (DateTime.Parse(Tabela["DtHrAbertura"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                    return StatusCaixaBalcao.Aberto;
                else
                    return StatusCaixaBalcao.DtCxDif;
            }
            else
            {
                Tabela = Controle.ConsultaSQL("SELECT * FROM CaixaBalcao WHERE Convert(Char,Data,103)='" + DateTime.Now.Date.ToShortDateString() + "' and Id_Usuario="+IdUsuario.ToString());
                if (Tabela.HasRows)
                {
                    return StatusCaixaBalcao.Fechado;
                }
                else
                    return StatusCaixaBalcao.NaoAberto;
            }
        }
        public int VerificarCaixa(int IdUsuario)
        {
            int IdCaixa = 0;            
            IdCaixa = Verificar_ExisteCadastro("Id_Caixa", "SELECT ID_CAIXA FROM CAIXABALCAO WHERE STATUS=0 and Id_Usuario="+IdUsuario.ToString());
            return IdCaixa;
        }
    }
}
