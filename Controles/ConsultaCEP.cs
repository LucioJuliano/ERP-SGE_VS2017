using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Controles
{
    public class ConsultaCEP
    {
        private string _Tipo;
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private string _Endereco;
        public string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; }
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
        private string _UF;
        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }
        public void VerificaCEP(string NumCep)
        {
            //WS_Servicos.VerificaCEP CEP = new WS_Servicos.VerificaCEP();

            //Limpa Referencias
            Bairro = "";
            Cidade = "";
            Endereco = "";
            Tipo = "";
            UF = "";

            DataSet Tabela = null ;
            //string txtcep = CEP.BuscaCEP(NumCep);

            WebRequest request = WebRequest.Create("http://viacep.com.br/ws/"+NumCep+"/xml/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            string dados = stream.ReadToEnd();

            System.IO.StringReader XmlCep = new System.IO.StringReader(dados);
            Tabela = new DataSet();
            Tabela.ReadXml(XmlCep);

            
            if (Tabela.Tables[0].Rows[0][0].ToString() == "true")
                return;

            if (Tabela != null)
            {
                if (Tabela.Tables[0].Rows.Count > 0)
                {
                    Bairro = Tabela.Tables[0].Rows[0]["bairro"].ToString();
                    Cidade = Tabela.Tables[0].Rows[0]["localidade"].ToString();
                    Endereco = Tabela.Tables[0].Rows[0]["logradouro"].ToString();
                    //Tipo = Tabela.Tables[0].Rows[0]["logradouro"].ToString();
                    UF = Tabela.Tables[0].Rows[0]["uf"].ToString();
                }
            }
        }        
    }
}
