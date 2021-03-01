<%@ Page Language="C#" MasterPageFile="~/ERP-SGE.master" AutoEventWireup="true" Inherits="Pedido" Title="Solicitação de Pedido de Compra" Codebehind="Pedido.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 116px;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
   <div>      
     <table bgcolor="#4b6c9e" border="0" width="100%">
       <tr><th bgcolor="#4b6c9e" align="center"> <b style="font-size: 18px; color: #FFFFFF">Solicitação de Pedido de Compra</b></th></tr>
     </table>           
     <hr />
     <table id="Table1" cellspacing="1" cellpadding="1" border="0" style="font-size: 12px; width: 470px;">         
      <tr>
       <td>       
          <table id="Table2" cellspacing="1" cellpadding="1" border="0" style="font-size: 12px; width: 410px;">         
            <tr>
              <td align="right" class="style1">Solicitante:</td>
	          <td style="WIDTH: 300px;"><asp:TextBox ID="TxtSolicitante" runat="server" Width="280px" BackColor="#ffffd9" Font-Names="Arial" Enabled="False"></asp:TextBox></td>	              	         	         
	        </tr>	        
	        <tr>
	          <td align="right" class="style1">Prazo de Pagamento:</td>
	          <td style="WIDTH: 300px;"><asp:TextBox ID="TxtPrazoPgto" runat="server" Width="280px" BackColor="#ffffd9" Font-Names="Arial" MaxLength="20"></asp:TextBox></td>	              	         	         
	        </tr>
	        <tr>  
	          <td align="right" class="style1">Tipo do Pedido:</td>
	          <td><asp:DropDownList ID="LstTpPedido" Width="280px" runat="server"> 
                  <asp:ListItem>Pedido</asp:ListItem>
                  <asp:ListItem>Solicitação Entrega Mercadoria</asp:ListItem>
                  </asp:DropDownList> </td>	         
	        </tr>	        
          </table>      
       </td>        
       <td align="right" class="style1">Observações:</td>
	   <td><asp:TextBox ID="TxtObs" runat="server" Width="224px" BackColor="#ffffd9" 
               Font-Names="Arial" Font-Size="9" Height="60px" TextMode="MultiLine"></asp:TextBox></td>	     	     	                    	          	     	                   
	   <td>
   	      <asp:Button ID="BtnEnviar" 
               style="background-image: url('Confirma.jpg'); height:25px; background-position:5% 100%; background-repeat:no-repeat;" 
               runat="server" text="Enviar Pedido" Font-Bold="True" BackColor="#3A4F63" 
               Font-Names="Arial" Font-Size="9pt" ForeColor="White" Width="130px" 
               onclick="BtnEnviar_Click"/>
   	   <hr />
   	      <asp:Button ID="BtnCancelar" 
               style="background-image: url('Cancelar.jpg'); height:25px; background-position:5% 100%; background-repeat:no-repeat;" 
               runat="server" text="Cancelar" UseSubmitBehavior="False" 
               OnClientClick="this.disabled=true;" Font-Bold="True" BackColor="#3A4F63" 
               Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="130px" 
               onclick="BtnCancelar_Click" />
       </td>
      </tr>      
     </table>     
   </div>
   <hr />   
     <asp:Label ID="LblInformacao" runat="server" Font-Bold="True" Font-Size="14pt" 
          ForeColor="#990000">Observação: Produtos sujeito a disponibilidade em 
      estoque</asp:Label>
   <hr />   
   <div>
     <table id="Table3" cellspacing="1" cellpadding="1" border="0" style="font-size: 12px; width: 821px;">
       <tr>                
         <td style="width: 50px;" align="left"></td>         
         <td style="width: 100px;" align="left">Referência</td>         
         <td style="width: 60px;"  align="left">Código</td>
         <td style="width: 350px;" align="left">Descrição do Titulo</td>                         	                     
         <td style="width: 70px;" align="left">Quantidade</td>	                                                       
	     <td style="width: 80px;"  align="left">Vrl.Unitário</td>
	     <td style="width: 80px;"  align="left">Vrl.Total</td>	     
       </tr>
         <caption>
             <hr />
             <tr>
                 <td style="WIDTH: 50px;">                     
                   <asp:ImageButton ID="BtnPesqPrd" runat="server" ImageUrl="~/Pesquisa.png"/>
                 </td>
                 <td style="WIDTH: 100px;">                     
                     <asp:TextBox ID="TxtReferencia" runat="server" BackColor="#ffffd9" Enabled="True" 
                         Font-Names="Arial"  Width="100px" AutoPostBack="True" 
                         ontextchanged="TxtReferencia_TextChanged"></asp:TextBox>                     
                 </td>
                 <td style="WIDTH: 60px;">
                     <asp:TextBox ID="TxtCodigo" runat="server" BackColor="#ffffd9" Enabled="false" 
                         Font-Names="Arial" Width="60px"></asp:TextBox>
                 </td>                 
                 <td style="WIDTH: 350px;">
                     <asp:TextBox ID="TxtDescricao" runat="server" BackColor="#ffffd9" Enabled="false" 
                         Font-Names="Arial" Width="350px"></asp:TextBox>
                 </td>
                 <td style="WIDTH: 70px;">
                     <asp:TextBox ID="TxtQtde" runat="server" BackColor="#ffffd9" Enabled="true" 
                         Font-Names="Arial" Width="70px" AutoPostBack="True" MaxLength="9" 
                         ontextchanged="TxtQtde_TextChanged"></asp:TextBox>
                 </td>
                 <td style="WIDTH: 80px;">
                     <asp:TextBox ID="TxtVlrUnt" runat="server" BackColor="#ffffd9" Enabled="false" 
                         Font-Names="Arial" Width="80px"></asp:TextBox>
                 </td>
                 <td style="WIDTH: 80px;">
                     <asp:TextBox ID="TxtVlrTotal" runat="server" BackColor="#ffffd9" Enabled="false" 
                         Font-Names="Arial" Width="80px"></asp:TextBox>
                 </td>                 
             </tr>
         </caption>
     </table>
   </div>
   <hr />
   <div>
     <asp:GridView ID="GridDados" runat="server" AutoGenerateColumns="False" 
                   CellPadding="4" ForeColor="#333333" GridLines="Horizontal"  
                   ShowFooter="True" Font-Overline="False" Font-Size="9pt"                    
                   EmptyDataText="Nenhum registro(s) para visualizar" 
           Font-Bold="True" onselectedindexchanged="GridDados_SelectedIndexChanged">
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
                <asp:BoundField DataField="Qtde" HeaderText="Quantidade" 
                       DataFormatString="{0:n0}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>                
                <asp:BoundField DataField="VlrUnitario" HeaderText="Preço" 
                       DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="VlrTotal" HeaderText="Vlr. Total" 
                       DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                   <asp:CommandField ButtonType="Image" SelectImageUrl="~/BtnExcluir.jpg" 
                       ShowSelectButton="True" />
               </Columns>
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <EditRowStyle BackColor="#999999" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             </asp:GridView>
   </div>
   <hr />
   <div>
     <table id="Table4" cellspacing="1" cellpadding="1" border="0" style="font-size: 12px; width: 821px;">
       <tr>                
        <td style="width: 50px;" align="left"></td>         
        <td style="width: 100px;" align="left"></td>         
        <td style="width: 60px;"  align="left"></td>
        <td style="width: 350px;" align="left"></td>                         	                             
	    <td style="width: 80px; color: #000000; font-size: 18px; background-color: #CCCCCC;" align="left">Total R$:</td>
	    <td style="WIDTH: 150px;">
	          <asp:TextBox ID="TxtTotal" runat="server" BackColor="#ffffd9" 
               Enabled="false" Font-Bold="True" Font-Names="Arial" Font-Size="12pt" 
               ForeColor="Black" style="margin-left: 0px" Width="110px">0,00</asp:TextBox>
       </tr>
     </table>
   </div>
   <hr />
  </ContentTemplate> 
 </asp:UpdatePanel>  
</asp:Content>

