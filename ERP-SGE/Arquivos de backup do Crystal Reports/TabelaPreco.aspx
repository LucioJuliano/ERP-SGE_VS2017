<%@ Page Language="C#" MasterPageFile="~/ERP-SGE.master" AutoEventWireup="true" Inherits="TabelaPreco" Title="Tabela de Preço dos Produtos" Codebehind="TabelaPreco.aspx.cs" %>

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
            <table>
              <tr>
                <td>
                  <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="8pt" Text="Grupo:"></asp:Label>                 
                  <asp:DropDownList ID="LstGrupo" runat="server" Font-Size="10pt"  Width="180px" Height="18px"></asp:DropDownList> 
                  <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Referência:"></asp:Label>                 
                  <asp:TextBox ID="TxtReferencia" runat="server" Font-Size="8pt" Width="133px"></asp:TextBox>
                  <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="10pt" Text="Descrição:"></asp:Label>                 
                  <asp:TextBox ID="TxtDescricao" runat="server" Font-Size="8pt" Width="300px"></asp:TextBox>
                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Pesquisa.png" onclick="ImageButton1_Click"/>             
                </td>
                <td style="width: 40px">
                </td>
                <td>
                  <asp:ImageButton ID="ImpTabPrc" runat="server" ImageUrl="~/Impressora.png" 
                        onclick="ImpTabPrc_Click"/>             
                </td>
              </tr>
           </table>
         </td>      
         
       </tr> 
       <tr>   
         <td>
           <div>
             <asp:GridView ID="GridDados" runat="server" AutoGenerateColumns="False" 
                   CellPadding="4" ForeColor="#333333" GridLines="Horizontal" PageSize="25" 
                   onpageindexchanging="GridDados_PageIndexChanging" AllowPaging="True" 
                   ShowFooter="True" Font-Overline="False" Font-Size="9pt"                    
                   EmptyDataText="Nenhum registro(s) para visualizar" Font-Bold="True">
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <Columns>
                <asp:BoundField DataField="Id_Produto" DataFormatString="{0:d6}" 
                    HeaderText="Codigo">
                    <HeaderStyle Width="70px" />
                </asp:BoundField>
                   <asp:BoundField DataField="Referencia" HeaderText="Referência">
                       <HeaderStyle Width="70px" />
                   </asp:BoundField>
                <asp:BoundField DataField="Descricao" HeaderText="Descrição">
                    <HeaderStyle Width="80px" />
                 <HeaderStyle Width="450px"></HeaderStyle>
                </asp:BoundField>
                   <asp:BoundField DataField="CodBarra" HeaderText="Cód. Barra">
                       <HeaderStyle Width="80px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="Grupo" HeaderText="Grupo">
                       <HeaderStyle Width="200px" />
                   </asp:BoundField>
                <asp:BoundField DataField="PrcAtacado" HeaderText="Preço" 
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

