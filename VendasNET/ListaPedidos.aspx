<%@ Page Language="C#" MasterPageFile="~/VendasNET.master" AutoEventWireup="true" CodeFile="ListaPedidos.aspx.cs" Inherits="ListaPedidos" Title="Lista dos Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
        <table bgcolor="#4b6c9e" border="0" width="100%">
         <tr>
          <th bgcolor="#4b6c9e" align="center"> 
           <b style="font-size: 18px; color: #FFFFFF">Consulta dos Pedidos Realizados</b>
          </th>
         </tr>
        </table> 
        <br />
          <asp:Panel ID="PanelPesq" runat="server" BackColor="#4B6C9E">
              <asp:Label ID="Label4" runat="server" Font-Size="11pt" ForeColor="White" Font-Bold="True" Text="Cliente::"></asp:Label>
              <asp:TextBox ID="TxtCliente" runat="server" AutoPostBack="True" Width="200px" 
                  ontextchanged="TxtCliente_TextChanged"></asp:TextBox>              
              &nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label2" runat="server" Font-Size="11pt" ForeColor="White" Font-Bold="True" Text="No.Venda:"></asp:Label>
              <asp:TextBox ID="TxtNumPed" runat="server" AutoPostBack="True" ontextchanged="TxtNumPed_TextChanged" Width="90px"></asp:TextBox>              
              &nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label1" runat="server" Text="Status:" Font-Bold="True" 
                  Font-Size="11pt" ForeColor="White"></asp:Label>
              <asp:DropDownList ID="LstTpMov" runat="server" AutoPostBack="True" 
                  onselectedindexchanged="LstTpMov_SelectedIndexChanged">                  
                  <asp:ListItem Value="0">Em Aberto</asp:ListItem>
                  <asp:ListItem Value="1">Confirmado</asp:ListItem>
                  <asp:ListItem Value="2">Faturado</asp:ListItem>                  
                  <asp:ListItem Value="3">Entregue</asp:ListItem>                  
                  <asp:ListItem Value="4">Cancelados</asp:ListItem>                  
                  <asp:ListItem Value="5">Em Rota</asp:ListItem>
                  <asp:ListItem Value="6">Todos</asp:ListItem>
              </asp:DropDownList>
              &nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label3" runat="server" Text="Vendedor:" Font-Bold="True" Font-Size="11pt" ForeColor="White"></asp:Label>
              <asp:DropDownList ID="LstVendedor" runat="server" Width="150px" 
                  AutoPostBack="True" onselectedindexchanged="LstVendedor_SelectedIndexChanged"> </asp:DropDownList>              
          </asp:Panel>        
        <br />
        
        <asp:GridView ID="GridDados" runat="server" AutoGenerateColumns="False"  
            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" PageSize="15" 
            onpageindexchanging="GridDados_PageIndexChanging" 
            onrowdatabound="GridDados_RowDataBound" AllowPaging="True" 
            ShowFooter="True" 
            EmptyDataText="Nenhum registro(s) para visualizar" Font-Bold="True" 
              Font-Size="9pt" onselectedindexchanged="GridDados_SelectedIndexChanged">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:CommandField SelectText="Visualizar" 
                    ShowSelectButton="True" ButtonType="Image" 
                    SelectImageUrl="~/Pesquisa.png" ></asp:CommandField>
                <asp:BoundField DataField="Id_Venda" HeaderText="No.Venda" DataFormatString="{0:d6}">                    
                </asp:BoundField>
                <asp:BoundField DataField="Data" HeaderText="Data" 
                    DataFormatString="{0:d}" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status">
                </asp:BoundField>
                <asp:BoundField DataField="Pessoa" HeaderText="Cliente">
                    <HeaderStyle Width="250px" />
                </asp:BoundField>                                
                <asp:BoundField DataField="VlrTotal" HeaderText="Vlr.Pedido" 
                    DataFormatString="{0:n2}">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PrevEntrega" HeaderText="Prev.Entrega" 
                    DataFormatString="{0:d}" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Entregador" HeaderText="Entregador">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>                                
                <asp:BoundField DataField="Vendedor" HeaderText="Vendedor">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>                                
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

