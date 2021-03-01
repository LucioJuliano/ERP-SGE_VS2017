<%@ Page Language="C#" MasterPageFile="~/VendasNET.master" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="Clientes" Title="Consulta de Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
        <table bgcolor="#4b6c9e" border="0" width="100%">
         <tr>
          <th bgcolor="#4b6c9e" align="center"> 
           <b style="font-size: 18px; color: #FFFFFF">Consulta de Clientes</b>
          </th>
         </tr>
        </table> 
        <br />
          <asp:Panel ID="PanelPesq" runat="server" BackColor="#4B6C9E">
              <asp:Label ID="Label1" runat="server" Text="Pesquisa por:" Font-Bold="True" 
                  Font-Size="11pt" ForeColor="White"></asp:Label>
              <asp:DropDownList ID="LstTpMov" runat="server" AutoPostBack="True">                  
                  <asp:ListItem Value="0">Razão Social</asp:ListItem>
                  <asp:ListItem Value="1">Nome Fantasia</asp:ListItem>
                  <asp:ListItem Value="2">CNPJ ou CPF</asp:ListItem>                  
                  <asp:ListItem Value="3">Endereço</asp:ListItem>                                    
              </asp:DropDownList>
              &nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label4" runat="server" Font-Size="11pt" ForeColor="White" Font-Bold="True" Text="Conteúdo:"></asp:Label>
              <asp:TextBox ID="TxtPesquisa" runat="server" AutoPostBack="True" Width="300px" ontextchanged="TxtPesquisa_TextChanged"></asp:TextBox> 
              &nbsp;
              <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Pesquisa.png" 
                  onclick="ImageButton1_Click" Height="19px" Width="21px" />                           
          </asp:Panel>        
        <br />
        
        <asp:GridView ID="GridDados" runat="server" AutoGenerateColumns="False"  
            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" PageSize="15" 
            onpageindexchanging="GridDados_PageIndexChanging" 
            onrowdatabound="GridDados_RowDataBound" AllowPaging="True" 
            ShowFooter="True" 
            EmptyDataText="Nenhum registro(s) para visualizar" Font-Bold="True" 
              Font-Size="9pt" >
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="Id_Pessoa" HeaderText="Cód." DataFormatString="{0:d6}"></asp:BoundField>
                <asp:BoundField DataField="CNPJ" HeaderText="CNPJ/CPF"></asp:BoundField>                                
                <asp:BoundField DataField="RazaoSocial" HeaderText="Razão Social"><HeaderStyle Width="300px" /></asp:BoundField>                                
                <asp:BoundField DataField="Fantasia" HeaderText="Nome Fantasia"><HeaderStyle Width="200px" /></asp:BoundField>                                
                <asp:BoundField DataField="Fone" HeaderText="Telefone"></asp:BoundField>                                
                <asp:BoundField DataField="Contato" HeaderText="Contato"><HeaderStyle Width="100px" /></asp:BoundField>                                                
                <asp:BoundField DataField="Logradouro" HeaderText="Endereço"><HeaderStyle Width="300px" /></asp:BoundField>
                <asp:BoundField DataField="Vendedor" HeaderText="Vendedor"><HeaderStyle Width="100px" /></asp:BoundField>                                
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                Visualizar
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
   </div>
</asp:Content>

