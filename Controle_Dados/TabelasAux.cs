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
    public class TabelasAux
    {
        private int _IdChave;
        public int IdChave
        {
            get { return _IdChave; }
            set { _IdChave = value; }
        }
        private string _Chave;
        public string Chave
        {
            get { return _Chave; }
            set { _Chave = value; }
        }
        private string _Campo;
        public string Campo
        {
            get { return _Campo; }
            set { _Campo = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        private int _Estoque; // 1-Entrada / 2-Saida
        public int Estoque
        {
            get { return _Estoque; }
            set { _Estoque = value; }
        }
        private int _Financeiro; // 1-Pagar / 2-Receber
        public int Financeiro
        {
            get { return _Financeiro; }
            set { _Financeiro = value; }
        }
        private int _Dief; // 1-Entrada / 2-Saida
        public int Dief
        {
            get { return _Dief; }
            set { _Dief = value; }
        }
        private int _VerificaDeb; // 1-Entrada / 2-Saida
        public int VerificaDeb
        {
            get { return _VerificaDeb; }
            set { _VerificaDeb = value; }
        }
        private int _Comissao;
        public int Comissao
        {
            get { return _Comissao; }
            set { _Comissao = value; }
        }
        private int _VerPrcMim;
        public int VerPrcMim
        {
            get { return _VerPrcMim; }
            set { _VerPrcMim = value; }
        }

        public Funcoes Controle;

        public void LerTabela(string Cmp, string CmpChave)
        {            
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM TABELASAUX WHERE RTRIM(CHAVE)='"+CmpChave+"' AND RTRIM(CAMPO)='"+Cmp+"'");
            if (Tabela.HasRows)
            {
                Tabela.Read();                
                IdChave = int.Parse(Tabela["Id_Chave"].ToString());
                Chave = Tabela["Chave"].ToString();
                Campo = Tabela["Campo"].ToString();
                Descricao = Tabela["Descricao"].ToString();
                Estoque = int.Parse(Tabela["Estoque"].ToString());
                Financeiro = int.Parse(Tabela["Financeiro"].ToString());
                Dief = int.Parse(Tabela["Dief"].ToString());
                VerificaDeb = int.Parse(Tabela["VerificaDeb"].ToString());
                Comissao = int.Parse(Tabela["Comissao"].ToString());
                VerPrcMim = int.Parse(Tabela["VerPrcMin"].ToString());                
            }
            else
            {
                IdChave = 0;
                Chave = "";
                Campo = "";
                Descricao = "";
                Estoque = 0;
                Financeiro = 0;
                Dief = 0;
                VerificaDeb = 0;
                Comissao = 0;
                VerPrcMim = 0;
            }
        }
    }
}
