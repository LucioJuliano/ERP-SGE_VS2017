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
    public class Vendedores
    {
        private int _IdVendedor;
        public int IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }
        }
        private string _Vendedor;
        public string Vendedor
        {
            get { return _Vendedor; }
            set { _Vendedor = value; }
        }
        private decimal _Comissao;
        public decimal Comissao
        {
            get { return _Comissao; }
            set { _Comissao = value; }
        }
        private string _Telefone;
        public string Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value; }
        }
        private string _Celular;
        public string Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }
        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }
        private int _EntraRel;
        public int EntraRel
        {
            get { return _EntraRel; }
            set { _EntraRel = value; }
        }
        private int _Distribuidor;
        public int Distribuidor
        {
            get { return _Distribuidor; }
            set { _Distribuidor = value; }
        }

        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        private int _IdPremio;
        public int IdPremio
        {
            get { return _IdPremio; }
            set { _IdPremio = value; }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }



        private decimal _Financeiro1;
        public decimal Financeiro1
        {
            get { return _Financeiro1; }
            set { _Financeiro1 = value; }
        }
        private decimal _PercFinanc1;
        public decimal PercFinanc1
        {
            get { return _PercFinanc1; }
            set { _PercFinanc1 = value; }
        }
        private decimal _Rentabilidade1;
        public decimal Rentabilidade1
        {
            get { return _Rentabilidade1; }
            set { _Rentabilidade1 = value; }
        }
        private decimal _PercRentab1;
        public decimal PercRentab1
        {
            get { return _PercRentab1; }
            set { _PercRentab1 = value; }
        }
        private decimal _Cliente1;
        public decimal Cliente1
        {
            get { return _Cliente1; }
            set { _Cliente1 = value; }
        }
        private decimal _PercCliente1;
        public decimal PercCliente1
        {
            get { return _PercCliente1; }
            set { _PercCliente1 = value; }
        }
        private decimal _Financeiro2;
        public decimal Financeiro2
        {
            get { return _Financeiro2; }
            set { _Financeiro2 = value; }
        }
        private decimal _PercFinanc2;
        public decimal PercFinanc2
        {
            get { return _PercFinanc2; }
            set { _PercFinanc2 = value; }
        }
        private decimal _Financeiro3;
        public decimal Financeiro3
        {
            get { return _Financeiro3; }
            set { _Financeiro3 = value; }
        }
        private decimal _PercFinanc3;
        public decimal PercFinanc3
        {
            get { return _PercFinanc3; }
            set { _PercFinanc3 = value; }
        }
        private int _CotaFinanceira;
        public int CotaFinanceira
        {
            get { return _CotaFinanceira; }
            set { _CotaFinanceira = value; }
        }
        private decimal _VlrRentab;
        public decimal VlrRentab
        {
            get { return _VlrRentab; }
            set { _VlrRentab = value; }
        }
        private decimal _VlrCliente;
        public decimal VlrCliente
        {
            get { return _VlrCliente; }
            set { _VlrCliente = value; }
        }
        private decimal _VlrGrdClientes;
        public decimal VlrGrdClientes
        {
            get { return _VlrGrdClientes; }
            set { _VlrGrdClientes = value; }
        }
        private decimal _VlrGradePrd1;
        public decimal VlrGradePrd1
        {
            get { return _VlrGradePrd1; }
            set { _VlrGradePrd1 = value; }
        }
        private decimal _VlrGradePrd2;
        public decimal VlrGradePrd2
        {
            get { return _VlrGradePrd2; }
            set { _VlrGradePrd2 = value; }
        }
        private int _GradeClientes;
        public int GradeClientes
        {
            get { return _GradeClientes; }
            set { _GradeClientes = value; }
        }

        private decimal _VlrReAtivClie;
        public decimal VlrReAtivClie
        {
            get { return _VlrReAtivClie; }
            set { _VlrReAtivClie = value; }
        }

        private decimal _Financeiro4;
        public decimal Financeiro4
        {
            get { return _Financeiro4; }
            set { _Financeiro4 = value; }
        }
        private decimal _PercFinanc4;
        public decimal PercFinanc4
        {
            get { return _PercFinanc4; }
            set { _PercFinanc4 = value; }
        }
        private decimal _VlrGradePrd3;
        public decimal VlrGradePrd3
        {
            get { return _VlrGradePrd3; }
            set { _VlrGradePrd3 = value; }
        }

        private int _IdVendGrupo;
        public int IdVendGrupo
        {
            get { return _IdVendGrupo; }
            set { _IdVendGrupo = value; }
        }

        private decimal _PercEntrega;
        public decimal PercEntrega
        {
            get { return _PercEntrega; }
            set { _PercEntrega = value; }
        }

        private decimal _VlrPercEntrega;
        public decimal VlrPercEntrega
        {
            get { return _VlrPercEntrega; }
            set { _VlrPercEntrega = value; }
        }
               

        public Funcoes Controle;
        public void LerDados(int Id)
        {
            IdVendedor     = 0;
            Vendedor       = "";
            Comissao       = 0;
            Telefone       = "";
            Celular        = "";
            Ativo          = 0;
            EntraRel       = 0;
            Distribuidor   = 0;
            Email          = "";
            IdPremio       = 0;
            IdUsuario      = 0;
            Financeiro1    = 0;
            PercFinanc1    = 0;
            Rentabilidade1 = 0;
            PercRentab1    = 0;
            Cliente1       = 0;
            PercCliente1   = 0;
            Financeiro2    = 0;
            PercFinanc2    = 0;
            Financeiro3    = 0;
            PercFinanc3    = 0;            
            CotaFinanceira = 0;
            VlrRentab      = 0;
            VlrCliente     = 0;
            VlrGrdClientes = 0;
            VlrGradePrd1   = 0;
            VlrGradePrd2   = 0;
            GradeClientes  = 0;
            VlrReAtivClie  = 0;
            Financeiro4    = 0;    
            PercFinanc4    = 0;
            VlrGradePrd3   = 0;
            IdVendGrupo    = 0;
            PercEntrega    = 0;
            VlrPercEntrega = 0;

            if (Id > 0)
            {
                SqlDataReader Tabela;
                Tabela = Controle.ConsultaSQL("SELECT * FROM Vendedores WHERE Id_Vendedor=" + Id.ToString().Trim());
                if (Tabela.HasRows)
                {
                    Tabela.Read();
                    IdVendedor = Id;
                    Vendedor = Tabela["Vendedor"].ToString().Trim();
                    Comissao = decimal.Parse(Tabela["Comissao"].ToString());
                    Telefone = Tabela["Telefone"].ToString().Trim();
                    Celular  = Tabela["Celular"].ToString().Trim();
                    Ativo    = int.Parse(Tabela["Ativo"].ToString());
                    EntraRel = int.Parse(Tabela["EntraRel"].ToString());
                    Email    = Tabela["Email"].ToString().Trim();
                    if (Tabela["Distribuidor"].ToString().Trim() != "")
                        Distribuidor = int.Parse(Tabela["Distribuidor"].ToString());
                    if (Tabela["Id_Premio"].ToString().Trim() != "")
                        IdPremio = int.Parse(Tabela["Id_Premio"].ToString());
                    if (Tabela["Id_Usuario"].ToString().Trim() != "")
                        IdUsuario = int.Parse(Tabela["Id_Usuario"].ToString());

                    if (Tabela["Id_VendGrupo"].ToString().Trim() != "")
                        IdVendGrupo = int.Parse(Tabela["Id_VendGrupo"].ToString());

                    if (Tabela["Financeiro1"].ToString().Trim() != "")
                        Financeiro1 = decimal.Parse(Tabela["Financeiro1"].ToString());
                    if (Tabela["Perc_Financ1"].ToString().Trim() != "")
                        PercFinanc1 = decimal.Parse(Tabela["Perc_Financ1"].ToString());
                    if (Tabela["Rentabilidade1"].ToString().Trim() != "")
                        Rentabilidade1 = decimal.Parse(Tabela["Rentabilidade1"].ToString());
                    if (Tabela["Perc_Rentab1"].ToString().Trim() != "")
                        PercRentab1    = decimal.Parse(Tabela["Perc_Rentab1"].ToString());
                    if (Tabela["Cliente1"].ToString().Trim() != "")
                        Cliente1       = int.Parse(Tabela["Cliente1"].ToString());
                    if (Tabela["Perc_Cliente1"].ToString().Trim() != "")
                        PercCliente1   = decimal.Parse(Tabela["Perc_Cliente1"].ToString());
                    if (Tabela["Financeiro2"].ToString().Trim() != "")
                        Financeiro2    = decimal.Parse(Tabela["Financeiro2"].ToString());
                    if (Tabela["Perc_Financ2"].ToString().Trim() != "")
                        PercFinanc2    = decimal.Parse(Tabela["Perc_Financ2"].ToString());
                    if (Tabela["Financeiro3"].ToString().Trim() != "")
                        Financeiro3    = decimal.Parse(Tabela["Financeiro3"].ToString());
                    if (Tabela["Perc_Financ3"].ToString().Trim() != "")
                        PercFinanc3    = decimal.Parse(Tabela["Perc_Financ3"].ToString());
                    if (Tabela["Financeiro4"].ToString().Trim() != "")
                        Financeiro4    = decimal.Parse(Tabela["Financeiro4"].ToString());
                    if (Tabela["Perc_Financ4"].ToString().Trim() != "")
                        PercFinanc4    = decimal.Parse(Tabela["Perc_Financ4"].ToString());
                    if (Tabela["CotaFinanceira"].ToString().Trim() != "")
                        CotaFinanceira = int.Parse(Tabela["CotaFinanceira"].ToString());
                    if (Tabela["VlrRentab"].ToString().Trim() != "")
                        VlrRentab = decimal.Parse(Tabela["VlrRentab"].ToString());
                    if (Tabela["VlrCliente"].ToString().Trim() != "")
                        VlrCliente = decimal.Parse(Tabela["VlrCliente"].ToString());
                    if (Tabela["VlrGrdClientes"].ToString().Trim() != "")
                        VlrGrdClientes = decimal.Parse(Tabela["VlrGrdClientes"].ToString());
                    if (Tabela["VlrGradePrd1"].ToString().Trim() != "")
                        VlrGradePrd1 = decimal.Parse(Tabela["VlrGradePrd1"].ToString());
                    if (Tabela["VlrGradePrd2"].ToString().Trim() != "")
                        VlrGradePrd2 = decimal.Parse(Tabela["VlrGradePrd2"].ToString());
                    if (Tabela["VlrGradePrd3"].ToString().Trim() != "")
                        VlrGradePrd3 = decimal.Parse(Tabela["VlrGradePrd3"].ToString());
                    if (Tabela["GradeClientes"].ToString().Trim() != "")
                        GradeClientes = int.Parse(Tabela["GradeClientes"].ToString());
                    if (Tabela["VlrReAtivClie"].ToString().Trim() != "")
                        VlrReAtivClie = int.Parse(Tabela["VlrReAtivClie"].ToString());
                    if (Tabela["Perc_Entrega"].ToString().Trim() != "")
                        PercEntrega = decimal.Parse(Tabela["Perc_Entrega"].ToString());
                    if (Tabela["VlrPercEntrega"].ToString().Trim() != "")
                        VlrPercEntrega = decimal.Parse(Tabela["VlrPercEntrega"].ToString()); 
                    
                }
            }
        }
        public void GravarDados()
        {
            string sSQL = "";
            ArrayList Nm_param = new ArrayList();
            ArrayList Vr_param = new ArrayList();
            if (IdVendedor > 0)
            {
                sSQL = "UPDATE Vendedores SET Id_Vendedor=@Id,Vendedor=@Vendedor,Comissao=@Comissao,Telefone=@Telefone,Celular=@Celular,Ativo=@Ativo," +
                       "EntraRel=@EntraRel,Distribuidor=@Distribuidor,Email=@Email,Id_Premio=@IdPremio,Id_Usuario=@IdUsuario," +
                       "Financeiro1=@Financeiro1,Perc_Financ1=@PercFinanc1,Rentabilidade1=@Rentabilidade1,Perc_Rentab1=@PercRentab1,Cliente1=@Cliente1,Perc_Cliente1=@PercCliente1," +
                       "Financeiro2=@Financeiro2,Perc_Financ2=@PercFinanc2,Financeiro3=@Financeiro3,Perc_Financ3=@PercFinanc3," +
                       "Financeiro4=@Financeiro4,Perc_Financ4=@PercFinanc4," +
                       "CotaFinanceira=@CotaFinanceira,VlrRentab=@VlrRentab,VlrCliente=@VlrCliente,VlrGrdClientes=@VlrGrdClientes,VlrGradePrd1=@VlrGradePrd1,VlrGradePrd3=@VlrGradePrd3," +
                       "VlrGradePrd2=@VlrGradePrd2,GradeClientes=@GradeClientes,VlrReAtivClie=@VlrReAtivClie,Id_VendGrupo=@IdVendGrupo,Perc_Entrega=@PercEntrega,VlrPercEntrega=@VlrPercEntrega Where Id_Vendedor=@Chave";
                Nm_param.Add("@Chave"); Vr_param.Add(IdVendedor);
            }
            else
            {
                IdVendedor = Controle.ProximoID("Vendedor");
                sSQL = "INSERT INTO Vendedores (ID_Vendedor,Vendedor,Comissao,Telefone,Celular,Ativo,EntraRel,Distribuidor,Email,Id_Premio,Id_Usuario," +
                       "Financeiro1,Perc_Financ1,Rentabilidade1,Perc_Rentab1,Cliente1,Perc_Cliente1,Financeiro2,Perc_Financ2,Financeiro3,Perc_Financ3," +
                       "CotaFinanceira,VlrRentab,VlrCliente,VlrGrdClientes,VlrGradePrd1,VlrGradePrd2,GradeClientes,VlrReAtivClie,"+
                       "Financeiro4,Perc_Financ4,VlrGradePrd3,Id_VendGrupo,Perc_Entrega,VlrPercEntrega) " +
                       " VALUES(@Id,@Vendedor,@Comissao,@Telefone,@Celular,@Ativo,@EntraRel,@Distribuidor,@Email,@IdPremio,@IdUsuario," +
                       "@Financeiro1,@PercFinanc1,@Rentabilidade1,@PercRentab1,@Cliente1,@PercCliente1,@Financeiro2,@PercFinanc2,@Financeiro3,@PercFinanc3," +
                       "@CotaFinanceira,@VlrRentab,@VlrCliente,@VlrGrdClientes,@VlrGradePrd1,@VlrGradePrd2,@GradeClientes,@VlrReAtivClie,@Financeiro4,@PercFinanc4,@VlrGradePrd3,@IdVendGrupo,@PercEntrega,@VlrPercEntrega)";
            }
            if (sSQL != "")
            {
                Nm_param.Add("@Id");             Vr_param.Add(IdVendedor);
                Nm_param.Add("@Vendedor");       Vr_param.Add(Vendedor);
                Nm_param.Add("@Comissao");       Vr_param.Add(Comissao);
                Nm_param.Add("@Telefone");       Vr_param.Add(Telefone);
                Nm_param.Add("@Celular");        Vr_param.Add(Celular);
                Nm_param.Add("@Ativo");          Vr_param.Add(Ativo);
                Nm_param.Add("@EntraRel");       Vr_param.Add(EntraRel);
                Nm_param.Add("@Distribuidor");   Vr_param.Add(Distribuidor);
                Nm_param.Add("@Email");          Vr_param.Add(Email);
                Nm_param.Add("@IdPremio");       Vr_param.Add(IdPremio);
                Nm_param.Add("@IdUsuario");      Vr_param.Add(IdUsuario);
                Nm_param.Add("@IdVendGrupo");    Vr_param.Add(IdVendGrupo);
                Nm_param.Add("@Financeiro1");    Vr_param.Add(Controle.FloatToStr(Financeiro1, 2));
                Nm_param.Add("@PercFinanc1");    Vr_param.Add(Controle.FloatToStr(PercFinanc1, 2));
                Nm_param.Add("@Rentabilidade1"); Vr_param.Add(Controle.FloatToStr(Rentabilidade1, 2));
                Nm_param.Add("@PercRentab1");    Vr_param.Add(Controle.FloatToStr(PercRentab1, 2));
                Nm_param.Add("@Cliente1");       Vr_param.Add(Cliente1);
                Nm_param.Add("@PercCliente1");   Vr_param.Add(Controle.FloatToStr(PercCliente1, 2));
                Nm_param.Add("@Financeiro2");    Vr_param.Add(Controle.FloatToStr(Financeiro2, 2));
                Nm_param.Add("@PercFinanc2");    Vr_param.Add(Controle.FloatToStr(PercFinanc2, 2));
                Nm_param.Add("@Financeiro3");    Vr_param.Add(Controle.FloatToStr(Financeiro3, 2));
                Nm_param.Add("@PercFinanc3");    Vr_param.Add(Controle.FloatToStr(PercFinanc3, 2));
                Nm_param.Add("@Financeiro4");    Vr_param.Add(Controle.FloatToStr(Financeiro4, 2));
                Nm_param.Add("@PercFinanc4");    Vr_param.Add(Controle.FloatToStr(PercFinanc4, 2));
                Nm_param.Add("@CotaFinanceira"); Vr_param.Add(CotaFinanceira);
                Nm_param.Add("@VlrRentab");      Vr_param.Add(Controle.FloatToStr(VlrRentab, 2));
                Nm_param.Add("@VlrCliente");     Vr_param.Add(Controle.FloatToStr(VlrCliente, 2));
                Nm_param.Add("@VlrGrdClientes"); Vr_param.Add(Controle.FloatToStr(VlrGrdClientes, 2));
                Nm_param.Add("@VlrGradePrd1");   Vr_param.Add(Controle.FloatToStr(VlrGradePrd1, 2));
                Nm_param.Add("@VlrGradePrd2");   Vr_param.Add(Controle.FloatToStr(VlrGradePrd2, 2));
                Nm_param.Add("@VlrGradePrd3");   Vr_param.Add(Controle.FloatToStr(VlrGradePrd3, 2));
                Nm_param.Add("@GradeClientes");  Vr_param.Add(Controle.FloatToStr(GradeClientes, 2));
                Nm_param.Add("@VlrReAtivClie");  Vr_param.Add(Controle.FloatToStr(VlrReAtivClie, 2));
                Nm_param.Add("@PercEntrega");    Vr_param.Add(Controle.FloatToStr(PercEntrega, 2));
                Nm_param.Add("@VlrPercEntrega"); Vr_param.Add(Controle.FloatToStr(VlrPercEntrega, 2));                
                Controle.ExecutaSQL(sSQL, Nm_param, Vr_param);
            }
        }
        public void Excluir()
        {
            if (IdVendedor > 0)
            {
                Controle.ExecutaSQL("DELETE FROM Vendedores WHERE Id_Vendedor=" + IdVendedor.ToString().Trim());
            }
        }

    }
}
