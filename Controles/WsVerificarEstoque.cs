using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controle_Dados;
using System.Data.SqlClient;
using System.Data;

namespace Controles
{
    public class WsVerificarEstoque
    {
        public Funcoes Controle;

        public DataTable Ver_Saldo(string RefPrd, bool VersaoDistr)
        {
            Serv_SaldoEstoque.ConsultaSaldo WsSaldo = new Serv_SaldoEstoque.ConsultaSaldo();

            DataTable Resultado = new DataTable();
            Resultado.Columns.Add("Filial", Type.GetType("System.String"));
            Resultado.Columns.Add("Saldo", Type.GetType("System.Decimal"));
            Resultado.Columns.Add("PrevEntrega", Type.GetType("System.String"));
            Resultado.Columns.Add("UltBalanco", Type.GetType("System.String"));
            try
            {
                SqlDataReader LerSQL = Controle.ConsultaSQL("SELECT * FROM EMPRESA_FILIAL ORDER BY fantasia ");
                decimal Saldo = 0;
                string PrevEntrega = "";
                string UltBalanco = "";
                while (LerSQL.Read())
                {
                    if (LerSQL["ServidorRemoto"].ToString().Trim() != "")
                    {
                        try
                        {
                            if (LerSQL["ID_FILIAL"].ToString().Trim() == "7")
                                WsSaldo.Url = "http://" + LerSQL["ServidorRemoto"].ToString().Trim() + "/WSSaldoEstoqueLoja/BuscaSaldoEstoque.asmx?swdl";
                            else
                                WsSaldo.Url = "http://" + LerSQL["ServidorRemoto"].ToString().Trim() + "/WSSaldoEstoque/BuscaSaldoEstoque.asmx?swdl";

                            Saldo       = WsSaldo.SaldoEstoque(RefPrd, LerSQL["ServidorRemoto"].ToString().Trim(), LerSQL["Porta"].ToString().Trim());
                            PrevEntrega = WsSaldo.PrevEntrega(RefPrd, LerSQL["ServidorRemoto"].ToString().Trim(), LerSQL["Porta"].ToString().Trim());
                            UltBalanco  = WsSaldo.DtBalanco(RefPrd, LerSQL["ServidorRemoto"].ToString().Trim(), LerSQL["Porta"].ToString().Trim());
                            Resultado.Rows.Add(LerSQL["Fantasia"].ToString().Trim(), Saldo, PrevEntrega, UltBalanco);
                        }
                        catch
                        {
                        }
                    }
                }
                return Resultado;
            }
            catch
            {
                return Resultado;
            }
        }
    }
}
