using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;


namespace Controle_Dados
{
    [Serializable()]
    public class Funcoes
    {
        //public string Conexao = "Data Source=Servidor;Initial Catalog=BDServiceOffice; Integrated Security=True; MultipleActiveResultSets=True;";
        
        public SqlConnection Conexao = null;
        public string StringConexao = "";
        public string NomeBanco = "";
        public string NomeServidor = "";
        
        public void RegistroLog(string erro)
        {
            string ArqLog = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\ERP_SGE_Log.LOG";
            //string ArqLog = "c:\\ERP_ERRO\\ERP_SGE_Log.LOG";
            StreamWriter Registrar;

            if (!File.Exists(ArqLog))
                Registrar = new StreamWriter(ArqLog);
            else
                Registrar = File.AppendText(ArqLog);

            Registrar.WriteLine(DateTime.Now.ToString());
            Registrar.WriteLine("   "+erro);
            Registrar.WriteLine("***----------------------------------------------------------------------***");
            Registrar.Close();
        }

        public void VerificarConexao()
        {
            //NomeBanco    = ConfigurationSettings.AppSettings.Get("NomeBanco").ToString();
            //NomeServidor = ConfigurationSettings.AppSettings.Get("NomeServidor").ToString();

            //if (StringConexao == "")
            //    StringConexao = "Data Source=SERVIDOR; Initial Catalog=BD_ERP_SGE; User ID=talimpo; Password=systalimpo; MultipleActiveResultSets=True;";

            while (Conexao == null || Conexao.State != ConnectionState.Open)
            {
                    try
                    {
                        Conexao = new SqlConnection(StringConexao);
                        Conexao.Open();
                    }
                    catch (Exception e)
                    {
                        RegistroLog(e.ToString());
                    }
                
            }

        }              
        // Funções de Consulta e Acesso ao Banco de Dados
        public DataSet ConsultaTabela(String SQL)
        {
            DataSet Tab = new DataSet();
            try
            {
                VerificarConexao();
                SqlCommand CmdSql = new SqlCommand(SQL, Conexao);
                CmdSql.CommandTimeout = 0;
                SqlDataAdapter Adapter = new SqlDataAdapter(CmdSql);                
                Adapter.Fill(Tab);
                return Tab;
            }
            catch (Exception e)
            {
                RegistroLog(e.ToString());
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Tab;
            }
        }
        // Funções de Consulta e Acesso ao Banco de Dados
        public DataSet ConsultaTabela(String SQL, String NomeTabela)
        {
            DataSet Tab = new DataSet();
            try
            {                
                VerificarConexao();
                SqlCommand CmdSql = new SqlCommand(SQL, Conexao);
                CmdSql.CommandTimeout = 0;
                SqlDataAdapter Adapter = new SqlDataAdapter(CmdSql);
                Adapter.Fill(Tab,NomeTabela);
                return Tab;
            }
            catch (Exception e)
            {
                RegistroLog(e.ToString());
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Tab;
            }
        }
        public SqlDataAdapter LerSQLAdapter(String SQL)
        {
            try
            {
                VerificarConexao();
                SqlCommand CmdSql = new SqlCommand(SQL, Conexao);
                CmdSql.CommandTimeout = 0;
                SqlDataAdapter Adapter = new SqlDataAdapter(CmdSql);
                return Adapter;
            }            
            catch (Exception e)
            {
                RegistroLog(e.ToString());
                return null;
            }
        }
        public SqlDataReader ConsultaSQL(string SQL)
        {
                       
            VerificarConexao();
            SqlDataReader Tabela;
            try
            {                
                SqlCommand objCmd = new SqlCommand(SQL, Conexao);
                objCmd.CommandTimeout = 0; 
                Tabela = objCmd.ExecuteReader();
                return Tabela;                
            }
            catch (Exception e)
            {
                RegistroLog(e.ToString());
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public void ExecutaSQL(string SQL)
        {
            try
            {                
                SqlConnection ConexaoInterna = new SqlConnection(Conexao.ConnectionString+" Password=systalimpo;");                
                ConexaoInterna.Open();
                
                SqlCommand SqlCmd = new SqlCommand(SQL, ConexaoInterna);
                SqlCmd.CommandTimeout = 0;
                SqlCmd.ExecuteNonQuery();
                ConexaoInterna.Dispose();
            }
            catch (Exception e)
            {
                RegistroLog(SQL);
                RegistroLog(e.ToString());                
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        public void ExecutaSQL(string SQL, ArrayList NmParam, ArrayList VrParam)
        {
            
            try
            {
                SqlConnection ConexaoInterna = new SqlConnection(Conexao.ConnectionString + " Password=systalimpo;");
                ConexaoInterna.Open();
                SqlCommand SqlCmd = new SqlCommand(SQL, ConexaoInterna);
                SqlCmd.CommandTimeout = 0;
                // Atualizando os Paramentos
                if (NmParam != null)
                {
                    for (int I = 0; I < NmParam.Count; I++)
                    {                        
                        SqlCmd.Parameters.Add(NmParam[I].ToString(), VrParam[I]);
                    }
                }
                SqlCmd.ExecuteNonQuery();
                ConexaoInterna.Dispose();
            }
            catch (Exception e)
            {
                RegistroLog(e.ToString());
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int ProximoID(String Tabela)
        {
            VerificarConexao();
            SqlCommand Cmd = new SqlCommand("ProximoID", Conexao);
            Cmd.Parameters.Add("@NomeTab", SqlDbType.Char);
            Cmd.Parameters[0].Value = Tabela;            
            Cmd.Parameters.Add("@Ret", SqlDbType.Int).Value = 0;
            Cmd.Parameters["@Ret"].Direction = ParameterDirection.Output;
            
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.ExecuteNonQuery();
            int Retorno = int.Parse(Cmd.Parameters["@Ret"].Value.ToString());
            Cmd.Dispose();
            return Retorno;
        }
    
        public int ProximoIDAnt(String Tabela)
        {
            try
            {
                SqlDataReader TabID;
                TabID = ConsultaSQL("SELECT ID FROM TABELAID WHERE TABELA='" + Tabela + "'");
                if (TabID.HasRows)
                {
                    ExecutaSQL("UPDATE TABELAID SET ID=ID+1 WHERE TABELA='" + Tabela + "'");                    
                    TabID = ConsultaSQL("SELECT ID FROM TABELAID WHERE TABELA='" + Tabela + "'");
                    TabID.Read();
                    return int.Parse(TabID["ID"].ToString());
                }
                else
                {
                    ExecutaSQL("INSERT INTO TABELAID(TABELA,ID) VALUES ('" + Tabela + "',1)"); ;                    
                    return 1;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
       
        public string SQLString(string CmpChave, string NmTabela, string OpSQL)
        {
            DataSet Tabela = new DataSet();
            Tabela = ConsultaTabela("SELECT * FROM " + NmTabela + " WHERE " + CmpChave + "=-1");
            string SqlText = "";
            string Pmt = "";
            string Virgula = "";
            if (OpSQL == "Inc")
            {
                SqlText = "INSERT INTO " + NmTabela + " (";
                for (int I = 0; I <= Tabela.Tables[0].Columns.Count - 1; I++)
                {
                    SqlText = SqlText + Virgula + Tabela.Tables[0].Columns[I].ColumnName;
                    if (Tabela.Tables[0].Columns[I].DataType.Name.ToUpper() == "STRING")
                        Pmt = Pmt + Virgula + "'{" + I.ToString() + "}'";
                    else if (Tabela.Tables[0].Columns[I].DataType.Name.ToUpper() == "DATETIME")
                        Pmt = Pmt + Virgula + "CONVERT(DATETIME,'{" + I.ToString() + "}',103)";
                    else
                        Pmt = Pmt + Virgula + "{" + I.ToString() + "}";
                    Virgula = ",";
                }
                SqlText = SqlText + ") VALUES(" + Pmt + ")";
            }
            else if (OpSQL == "Upd")
            {
                SqlText = "UPDATE " + NmTabela + " SET ";
                Virgula = "";
                for (int I = 0; I <= Tabela.Tables[0].Columns.Count - 1; I++)
                {
                    if (Tabela.Tables[0].Columns[I].DataType.Name.ToUpper() == "STRING")
                        SqlText = SqlText + Virgula + Tabela.Tables[0].Columns[I].ColumnName + "='{" + I.ToString() + "}'";
                    else if (Tabela.Tables[0].Columns[I].DataType.Name.ToUpper() == "DATETIME")
                        SqlText = SqlText + Virgula + Tabela.Tables[0].Columns[I].ColumnName + "=CONVERT(DATETIME,'{" + I.ToString() + "}',103)";
                    else
                        SqlText = SqlText + Virgula + Tabela.Tables[0].Columns[I].ColumnName + "={" + I.ToString() + "}";
                    Virgula = ",";
                }
            }
            return SqlText;
        }                
        // Funções de Formatação de String
        public string FloatToStr(decimal Vr)
        {            
            return Vr.ToString().Replace(",", ".");
        }
        public string FloatToStr(decimal Vr,int CasaDec)
        {
            Vr = Math.Round(Vr, CasaDec);//MidpointRounding.AwayFromZero);
            return Vr.ToString().Replace(",", ".");
        }
        
        public string NumSpace(string Vlr, int Qtd)
        {
            string Str = Vlr;
            for (int i = Vlr.Length; i < Qtd; i++)
            {
                Str = " " + Str;
            }
            return Str.Substring(0, Qtd);
        }
        public string Space(string Cmp, int Qtd)
        {
            string Str = Cmp;
            for (int i = Cmp.Length; i < Qtd; i++)
            {
                Str = Str + " ";
            }
            return Str.Substring(0, Qtd);
        }
        public string MaskValor(float Valor)
        {
            StringBuilder StringValor = new StringBuilder();
            if (Valor != 0)
                StringValor.AppendFormat("{0:n}", Valor);
            else
                StringValor.AppendFormat("{0:n}", 0.00);

            return StringValor.ToString();
        }
        public string Crypt(string texto)
        {            
            MD5 md5Hasher = MD5.Create();            
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(texto));
            StringBuilder textocrypto = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                textocrypto.Append(data[i].ToString("X2"));
            }
            return textocrypto.ToString();
        }        
        // Funções de Verificação
        public bool ValidarCnpj(string cnpj)
        {            
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj == "99999999999999")
                return true;
            if (cnpj.Length != 14)
                return false;
            if (cnpj=="00000000000000" || cnpj=="11111111111111" || cnpj=="22222222222222" || cnpj=="33333333333333" || cnpj=="44444444444444" 
             || cnpj=="55555555555555" || cnpj=="66666666666666" || cnpj=="77777777777777" || cnpj=="88888888888888" )
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        public bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444"
             || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888")
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        public bool ValidarCgf(string cgf)
        {
            int[] multiplicador1 = new int[8] { 9, 8, 7, 6, 5, 4, 3, 2 };            
            string tempCgf;
            string digito;
            int soma;
            int resto;
            cgf = cgf.Trim();
            cgf = cgf.Replace(".", "").Replace("-", "");
            if (cgf == "ISENTO" || cgf == "")
                return true;

            if (cgf == "000000000" || cgf == "111111111" || cgf == "222222222" || cgf == "333333333" || cgf == "444444444"
             || cgf == "555555555" || cgf == "666666666" || cgf == "777777777" || cgf == "888888888")
                return false;
            tempCgf = cgf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 8; i++)
                soma += int.Parse(tempCgf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto > 0)
            {
                if (resto == 10 || resto == 11)
                    resto = 0;
                else
                    resto = 11 - resto;
            }
            digito = resto.ToString();
            tempCgf = tempCgf + digito;
            soma = 0;            
            return cgf.EndsWith(digito);
        }
        public string FormatarData(DateTime Data)
        {
            string DtStr = "";
            DtStr = string.Format("{0:D4}{1:D2}{2:D2}", Data.Year, Data.Month, Data.Day);
            return DtStr;            
        }

        public DataTable LstCST()
        {
            DataTable TabCST = new DataTable();
            TabCST.Columns.Add("CST", Type.GetType("System.Int32"));
            TabCST.Columns.Add("DescCST", Type.GetType("System.String"));
            TabCST.Rows.Add(0, "Nenhum");
            TabCST.Rows.Add(1, "000-Tributada Integralmente");
            TabCST.Rows.Add(2, "010-Tributada e com cobrança do ICMS por Sub.Trib.");
            TabCST.Rows.Add(3, "020-Com redução de base de cálculo");
            TabCST.Rows.Add(4, "030-Isenta ou não Trib. e com cobrança do ICMS por Sub.Trib.");
            TabCST.Rows.Add(5, "040-Isenta");
            TabCST.Rows.Add(6, "041-Não Tributada");
            TabCST.Rows.Add(7, "050-Suspensão");
            TabCST.Rows.Add(8, "060-ICMS cobrado anteriomente por Sub.Trib.");
            TabCST.Rows.Add(9, "070-Redução e cobrança do ICMS por Sub.Trib");
            TabCST.Rows.Add(10,"090-Outras");
            return TabCST; 
        }
        public void EnviaSocket(string Ip, string msg)
        {
            /*TClientSocket.ClientSocket Dados = new TClientSocket.ClientSocket(Ip, PortaSocket);
            Dados.Connect();
            Dados.SendText(msg);
            Dados.Disconnect();*/
            
        }
    }
}
