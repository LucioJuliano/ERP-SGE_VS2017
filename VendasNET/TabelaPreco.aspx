<%@ Page Language="C#" MasterPageFile="~/VendasNET.master" AutoEventWireup="true" CodeFile="TabelaPreco.aspx.cs" Inherits="TabelaPreco" Title="TABELA DE PREÇO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
   <div>
     <table bgcolor="#4b6c9e" border="0" width="100%">
      <tr>
        <th bgcolor="#4b6c9e" align="center"> 
          <b style="font-size: 18px; color: #FFFFFF">Tabela de Preço dos Produtos</b>
        </th>
      </tr>
     </table> 
     <table id="Table1" runat="server" style="width: 100%;"> 
       <tr >    
         <td>
             <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Referência:"></asp:Label>                 
             <asp:TextBox ID="TxtReferencia" runat="server" Font-Size="8pt" Width="99px"></asp:TextBox>
             <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="10pt" Text="Descrição:"></asp:Label>                 
             <asp:TextBox ID="TxtDescricao" runat="server" Font-Size="8pt" Width="250px"></asp:TextBox>
             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Pesquisa.png" onclick="ImageButton1_Click" />
         </td>      
       </tr> 
       <tr>   
         <td>
           <div>
             <asp:GridView ID="GridDados" runat="server" AutoGenerateColumns="False" 
                   CellPadding="4" ForeColor="#333333" GridLines="Horizontal" PageSize="10" 
                   onpageindexchanging="GridDados_PageIndexChanging" AllowPaging="True" 
                   ShowFooter="True" Font-Overline="False" Font-Size="9pt"                    
                   EmptyDataText="Nenhum registro(s) para visualizar" Font-Bold="True">
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <Columns>
                <asp:BoundField DataField="Id_Produto" DataFormatString="{0:d6}" 
                    HeaderText="Cod.">
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                   <asp:BoundField DataField="Referencia" HeaderText="Ref.">
                       <HeaderStyle Width="40px" />
                   </asp:BoundField>
                <asp:BoundField DataField="Descricao" HeaderText="Descrição">
                    <HeaderStyle Width="80px" />
                 <HeaderStyle Width="250px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SALDOESTOQUE" HeaderText="Estoque" 
                       DataFormatString="{0:n3}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PrcVarejo" HeaderText="Prç.Var." 
                       DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PrcMinimo" HeaderText="Prç.Min." 
                       DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PrcAtacado" HeaderText="Prç.Dist" 
                       DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>                
               </Columns>
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <EditRowStyle BackColor="#999999" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             </asp:GridView>
           </div>
         </td>      
       </tr>
     </table>
   </div>
  </ContentTemplate> 
 </asp:UpdatePanel>  
</asp:Content>

