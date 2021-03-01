using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controle_Dados;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Controles
{
    [Serializable()]
    public class Login
    {
        public Funcoes Controle = new Funcoes();
        

        public Usuarios Verificar_Login(string NomeUsuario, string Pwd)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM Usuarios WHERE Usuario='" + NomeUsuario.Trim()+"'");
            if (Tabela.HasRows)
            {
                Tabela.Read();                                
                if (Tabela["Senha"].ToString().Trim()!=Controle.Crypt(Pwd.Trim()))
                {
                    return null;
                }
                else
                {
                    Usuarios Usuario = new Usuarios();
                    Usuario.Controle = Controle;
                    Usuario.LerDados(int.Parse(Tabela["Id_Usuario"].ToString()));
                    return Usuario;
                }                
            }
            else
                return null;                
        }
        public Pessoas Verificar_LoginDistribuidor(string CnpjCpf, string Pwd)
        {
            SqlDataReader Tabela;
            Tabela = Controle.ConsultaSQL("SELECT * FROM Pessoas WHERE Cnpj='" + CnpjCpf.Trim() + "'");
            if (Tabela.HasRows)
            {
                Tabela.Read();
                if (Tabela["Senha"].ToString().Trim() != Controle.Crypt(Pwd.Trim()))
                {
                    return null;
                }
                else
                {
                    Pessoas Usuario = new Pessoas();
                    Usuario.Controle = Controle;
                    Usuario.LerDados(int.Parse(Tabela["Id_Pessoa"].ToString()));
                    return Usuario;
                }
            }
            else
                return null;
        }        
    }
}
