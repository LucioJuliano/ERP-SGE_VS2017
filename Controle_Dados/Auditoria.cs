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
    public class Auditoria
    {
        private int _IdLanc;
        public int IdLanc
        {
            get { return _IdLanc; }
            set { _IdLanc = value; }
        }
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        private string _Terminal;
        public string Terminal
        {
            get { return _Terminal; }
            set { _Terminal = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private string _Opcao;
        public string Opcao
        {
            get { return _Opcao; }
            set { _Opcao = value; }
        }
        private int _IdChave;
        public int IdChave
        {
            get { return _IdChave; }
            set { _IdChave = value; }
        }
        private String _Documento;
        public String Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        private int _Operacao;  // 1-Incluir 2-Alterar 3-Excluir 4-Cancelar 5-Confirmar 6-Faturamento 7-Outros 
        public int Operacao
        {
            get { return _Operacao; }
            set { _Operacao = value; }
        }
        private string _Descricao;
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public Funcoes Controle;
        public void Registrar()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();

            IdLanc = Controle.ProximoID("AUDITORIA");
            sSQL = "INSERT INTO AUDITORIA (Id_Lanc,Id_Usuario,Terminal,Data,Opcao,Id_Chave,Documento,Operacao,Descricao) VALUES(@Id,@IdUsuario,@Terminal,Convert(DateTime,@Data,103),@Opcao,@IdChave,@Documento,@Operacao,@Descricao)";

            Nm_param.Add("@Id"); Vr_param.Add(IdLanc);            
            Nm_param.Add("@IdUsuario"); Vr_param.Add(IdUsuario);
            Nm_param.Add("@Terminal"); Vr_param.Add(Terminal);
            Nm_param.Add("@Data"); Vr_param.Add(Data);
            Nm_param.Add("@Opcao"); Vr_param.Add(Opcao);
            Nm_param.Add("@IdChave"); Vr_param.Add(IdChave);
            Nm_param.Add("@Documento"); Vr_param.Add(Documento);
            Nm_param.Add("@Operacao"); Vr_param.Add(Operacao);
            Nm_param.Add("@Descricao"); Vr_param.Add(Descricao);
            Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
        }
    }
}
