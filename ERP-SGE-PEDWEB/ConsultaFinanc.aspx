<%@ Page Language="C#" MasterPageFile="~/ERP-SGE.master" AutoEventWireup="true" Inherits="ConsultaFinanc" Title="Movimentação Financeira" Codebehind="ConsultaFinanc.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
     <table bgcolor="#4b6c9e" border="0" width="100%">
      <tr>
        <th bgcolor="#4b6c9e" align="center"> 
          <b style="font-size: 18px; color: #FFFFFF">Consulta dos Lançamento Financeiros</b>
        </th>
      </tr>
     </table> 
     <table id="Table1" runat="server" style="width: 100%;">        
       <tr>   
         <td>
           <div>
             <asp:GridView ID="GridDados" runat="server" AutoGenerateColumns="False" 
                   CellPadding="4" ForeColor="#333333" GridLines="Horizontal" PageSize="15" 
                   onpageindexchanging="GridDados_PageIndexChanging" AllowPaging="True" 
                   ShowFooter="True" Font-Overline="False" Font-Size="9pt"                    
                   EmptyDataText="Nenhum registro(s) para visualizar" Font-Bold="True">
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <Columns>
                <asp:BoundField DataField="Id_Lanc" DataFormatString="{0:d6}" 
                    HeaderText="No.Lanç.">
                    <HeaderStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="Datalanc" HeaderText="Data" DataFormatString="{0:d}">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Id_Venda" HeaderText="No. Pedido">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                   <asp:BoundField DataField="VlrOriginal" DataFormatString="{0:n2}" 
                       HeaderText="Vlr. do Titulo">
                       <HeaderStyle Width="80px" />
                       <ItemStyle HorizontalAlign="Right" />
                   </asp:BoundField>
                <asp:BoundField DataField="Vencimento" HeaderText="Vencimento" 
                    DataFormatString="{0:d}">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Documento" HeaderText="Documento">
                    <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="NotaFiscal" HeaderText="Formulário NF">
                    <HeaderStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="Referente" HeaderText="Referente">
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                   <asp:BoundField DataField="VlrBaixa" DataFormatString="{0:n2}" 
                       HeaderText="Vlr. Pago">
                       <HeaderStyle Width="80px" />
                       <ItemStyle HorizontalAlign="Right" />
                   </asp:BoundField>
                   <asp:BoundField DataField="DtBaixa" DataFormatString="{0:d}" 
                       HeaderText="Data Pag.">
                       <HeaderStyle Width="80px" />
                       <ItemStyle HorizontalAlign="Center" />
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
</asp:Content>

