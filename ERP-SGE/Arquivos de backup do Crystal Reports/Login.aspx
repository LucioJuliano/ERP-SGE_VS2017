<%@ Page Language="C#" MasterPageFile="~/ERP-SGE.master" AutoEventWireup="true" Inherits="Login" Title="Acesso ao Sistema" Codebehind="Login.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-left:35%; margin-top:15%">
   <asp:Login ID="Login1" runat="server"  BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4"
       BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
       ForeColor="#333333" Height="185px"   Width="300px" DisplayRememberMe="False" 
          FailureText="Usuário ou Senha invalida...." LoginButtonText="Confirma" 
          PasswordLabelText="Senha de Acesso:" RememberMeText="" TitleText="Acesso" 
          UserNameLabelText="CNPJ/CPF:" PasswordRequiredErrorMessage="Informe a senha" 
          UserNameRequiredErrorMessage="Informe o Usuário" 
        onauthenticate="Login1_Authenticate">
      <TitleTextStyle BackColor="#4b6c9e" Font-Bold="True" Font-Size="Large" ForeColor="White" BorderColor="White" />
      <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
      <TextBoxStyle Font-Size="15px" BackColor="Gainsboro" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" ForeColor="Black" Height="20px" Width="80px" />
      <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
       Font-Names="Verdana" Font-Size="Small" ForeColor="#284775" Font-Bold="True" />
      <LabelStyle Font-Bold="True" Font-Size="15px" />
       <LayoutTemplate>
           <table border="0" cellpadding="4" cellspacing="0" 
               style="border-collapse:collapse;">
               <tr>
                   <td>
                       <table border="0" cellpadding="0" style="height:185px;width:314px;">
                           <tr>
                               <td align="center" colspan="2" 
                                   style="color:White;background-color:#4B6C9E;border-color:White;font-size:Large;font-weight:bold;">
                                   Acesso</td>
                           </tr>
                           <tr>
                               <td align="right" style="font-size:15px;font-weight:bold;">
                                   <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">CNPJ/CPF:</asp:Label>
                               </td>
                               <td>
                                   <asp:TextBox ID="UserName" runat="server" BackColor="Gainsboro" 
                                       BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Size="15px" 
                                       ForeColor="Black" Height="20px" Width="150px"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                       ControlToValidate="UserName" ErrorMessage="Informe o Usuário" Font-Bold="True" 
                                       Font-Size="8px" ToolTip="Informe o Usuário" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                               </td>
                           </tr>
                           <tr>
                               <td align="right" style="font-size:15px;font-weight:bold;">
                                   <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha 
                                   de Acesso:</asp:Label>
                               </td>
                               <td>
                                   <asp:TextBox ID="Password" runat="server" BackColor="Gainsboro" 
                                       BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Size="15px" 
                                       ForeColor="Black" Height="20px" TextMode="Password" Width="147px"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                       ControlToValidate="Password" ErrorMessage="Informe a senha" Font-Bold="True" 
                                       Font-Size="8px" ToolTip="Informe a senha" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                               </td>
                           </tr>
                           <tr>
                               <td align="center" colspan="2" style="color:Red;">
                                   <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                               </td>
                           </tr>
                           <tr>
                               <td align="right" colspan="2">
                                   <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" 
                                       BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                       Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#284775" 
                                       Text="Confirma" ValidationGroup="Login1" />
                               </td>
                           </tr>
                       </table>
                   </td>
               </tr>
           </table>
       </LayoutTemplate>
      <ValidatorTextStyle Font-Bold="True" Font-Size="8px" />
   </asp:Login>   
  </div>
</asp:Content>

