<%@ Page Language="C#" AutoEventWireup="true" Inherits="PrevRelatorio" Codebehind="PrevRelatorio.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>ERP SGE -Sistema de Gestão Empresarial Versão  2.0 .NET - 2010</title>           
</head>
<body>
  <form id="form1" runat="server">        
    <div>
       <asp:Button ID="BtnRetorno" CssClass="Direita" Height="30px" runat="server" text="Voltar Tela Pesquisa"  Font-Bold="True" OnClick="BtnRetorno_Click" Width="164px" />                   
    </div>    
    <div>        
        <CR:CrystalReportViewer ID="crvRelatorio" runat="server" AutoDataBind="true"  
            EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" 
            oninit="crvRelatorio_Init" ToolPanelView="None" DisplayGroupTree="False" />        
    </div>
  </form>
</body>
</html>
