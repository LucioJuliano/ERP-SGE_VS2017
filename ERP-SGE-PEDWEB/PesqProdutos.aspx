<%@ Page Language="C#" AutoEventWireup="true" Inherits="PesqProdutos" Codebehind="PesqProdutos.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self">
    <title>Pesquisa de Produtos</title>
    <style type="text/css">
        .style1
        {
            width: 89px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" width: 100%; background-color: #fff; border: 1px solid #496077;"> 
      <div style="background-color: #4b6c9e">   
      <table id="Table2" runat="server" style="width: 100%;"> 
       <tr >
         <td class="style1">
           <asp:Image ID="Image1" runat="server" ImageUrl="MiniLogoTipo.jpg" Height="28px" />
         </td>
         <td >
          <h1 style="font-size: 12px; margin: 10px auto 0px auto; color: #FFFFFF; z-index: auto;" >ERP SGE -Sistema de Gestão Empresarial Versão  2.0 .NET - 2010   Copyright ©  - Talimpo</h1>
         </td>
       </tr>           
      </table>
      </div>
      <table bgcolor="#4b6c9e" border="0" width="100%">
        <tr>
         <th bgcolor="#4b6c9e" align="center"> 
           <b style="font-size: 18px; color: #FFFFFF">Pesquisa de Produtos</b>
         </th>
        </tr>
      </table> 
      <table id="Table1" runat="server" style="width: 100%;"> 
       <tr >    
         <td>                        
             <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="8pt" Text="Grupo:"></asp:Label>                 
             <asp:DropDownList ID="LstGrupo" runat="server" Font-Size="10pt"  Width="180px" Height="18px"></asp:DropDownList> 
             <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="8pt" Text="Referência:"></asp:Label>                 
             <asp:TextBox ID="TxtReferencia" runat="server" Font-Size="10pt" Width="77px" 
                 Height="18px"></asp:TextBox>
             <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="8pt" Text="Descrição:"></asp:Label>                 
             <asp:TextBox ID="TxtDescricao" runat="server" Font-Size="10pt" Width="225px" 
                 Height="18px"></asp:TextBox>
             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Pesquisa.png" onclick="ImageButton1_Click"/>                          
         </td>      
       </tr> 
       <tr>   
         <td>
           <div>
             <asp:GridView ID="GridDados" runat="server" AutoGenerateColumns="False" 
                   CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                    AllowPaging="True" 
                   ShowFooter="True" Font-Overline="False" Font-Size="8pt"                    
                   EmptyDataText="Nenhum registro(s) para visualizar" Font-Bold="True" 
                   Font-Underline="False" Width="700px" 
                   onpageindexchanging="GridDados_PageIndexChanging"
                   onselectedindexchanged="GridDados_SelectedIndexChanged" PageSize="14">
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <Columns>
                   <asp:CommandField InsertVisible="False" SelectText="Selecionar" 
                       ShowCancelButton="False" ShowSelectButton="True" />
                <asp:BoundField DataField="Referencia" 
                    HeaderText="Referência">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Descricao" HeaderText="Descrição">
                    <HeaderStyle Width="300px" Wrap="False" />
                 <HeaderStyle Width="450px"></HeaderStyle>
                </asp:BoundField>
                   <asp:BoundField DataField="Grupo" HeaderText="Grupo">
                       <FooterStyle Wrap="False" />
                       <HeaderStyle Width="130px" Wrap="False" />
                       <ItemStyle Width="100px" Wrap="False" />
                   </asp:BoundField>
                <asp:BoundField DataField="PrcAtacado" HeaderText="Preço" 
                       DataFormatString="{0:n2}">
                    <HeaderStyle Width="70px" />
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
    </form>
</body>
</html>
