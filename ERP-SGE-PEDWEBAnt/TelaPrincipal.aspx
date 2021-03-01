<%@ Page Language="C#" MasterPageFile="~/ERP-SGE.master" AutoEventWireup="true" Inherits="TelaPrincipal" Title="Tela Principal" Codebehind="TelaPrincipal.aspx.cs" %>

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
              <asp:Label ID="Label1" runat="server" Text="Tipo Movimento:" Font-Bold="True" 
                  Font-Size="11pt" ForeColor="White"></asp:Label>
              <asp:DropDownList ID="LstTpMov" runat="server" AutoPostBack="True" 
                  onselectedindexchanged="LstTpMov_SelectedIndexChanged">
                  <asp:ListItem Value="0">Todos</asp:ListItem>
                  <asp:ListItem Value="1">Pedido Venda</asp:ListItem>
                  <asp:ListItem Value="2">Solicitação Pedido</asp:ListItem>
                  <asp:ListItem Value="3">Venda Financeira</asp:ListItem>
                  <asp:ListItem Value="4">Entrega de Mercadoria</asp:ListItem>
              </asp:DropDownList>
              &nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label2" runat="server" Font-Size="11pt" ForeColor="White" Font-Bold="True"
                  Text="No. Pedido:"></asp:Label>
              <asp:TextBox ID="TxtNumPed" runat="server" AutoPostBack="True" 
                  ontextchanged="TxtNumPed_TextChanged" Width="90px"></asp:TextBox>
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
                <asp:BoundField DataField="Id_Venda" DataFormatString="{0:d6}" 
                    HeaderText="No.Pedido">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status">
                    <HeaderStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="VlrTotal" HeaderText="Vlr. do Pedido" 
                    DataFormatString="{0:n2}">
                    <HeaderStyle Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PrevEntrega" HeaderText="Prev.Entrega" 
                    DataFormatString="{0:d}" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Entregador" HeaderText="Entregador">
                    <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="FormNF" HeaderText="Formulário NF">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="TipoVenda" HeaderText="Tipo de Movimento">
                    <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="VinculoVD" DataFormatString="{0:d6}" 
                    HeaderText="Venda Vinculada">
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

