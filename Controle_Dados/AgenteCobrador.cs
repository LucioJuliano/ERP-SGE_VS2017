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
    public class AgenteCobrador
    {
        private int _IdAgente;
        public int IdAgente
        {
            get { return _IdAgente; }
            set { _IdAgente = value; }
        }
        private string _Agente;
        public string Agente
        {
            get { return _Agente; }
            set { _Agente = value; }
        }

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdAgente = 0;
            Agente = "";
            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM AgenteCobrador WHERE Id_Agente=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdAgente = Id;
                    Agente = Tabela["Agente"].ToString().Trim();                    
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdAgente > 0)
            {
                sSQL = "UPDATE AGENTECOBRADOR SET Id_Agente=@Id,Agente=@Agente Where Id_Agente=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdAgente);
            }
            else
            {
                IdAgente = Controle.ProximoID("AGENTECOBRADOR");
                sSQL = "INSERT INTO AGENTECOBRADOR (ID_AGENTE,AGENTE) VALUES(@Id,@Agente)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id"); Vr_param.Add(IdAgente);
                Nm_param.Add("@Agente"); Vr_param.Add(Agente);                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdAgente > 0)
            {
                Controle.ExecutaSQL("DELETE FROM AGENTECOBRADOR WHERE Id_Agente=" + IdAgente.ToString().Trim());
            }
        }
    }
}
