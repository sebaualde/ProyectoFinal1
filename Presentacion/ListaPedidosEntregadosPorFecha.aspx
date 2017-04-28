<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ListaPedidosEntregadosPorFecha.aspx.cs" Inherits="ListaPedidosEntregadosPorFecha" Title="Pedidos entregados por Fecha" %>

<%@ Register Src="Calendario.ascx" TagName="Calendario" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
   <center><h1 style="text-align: center">
        Pedidos entregados por Fecha</h1>
    <h2><p style="text-align: center">
        <span style="color: #cc0033">
        Mostrar pedidos realizados entre</span>&nbsp;
        </p></h2>
       <p style="text-align: center">
           <strong><span style="color: #ff0033"></span></strong>
           <table style="width: 647px; text-align:center">
               <tr>
                   <td style="width: 112px; height: 79px; text-align: center;">
                       <uc1:Calendario
               id="calInicio" runat="server"></uc1:Calendario>
                       <strong><span style="color: #ff0033">Fecha Inicio</span></strong></td>
                   <td style="width: 124px; height: 79px; text-align: center;">
                       <uc1:Calendario id="calFin"
               runat="server"></uc1:Calendario>
                       <strong><span style="color: #ff0033">Fecha Fin</span></strong></td>
               </tr>
               <tr>
                   <td style="width: 112px">
                   </td>
                   <td style="width: 124px">
                   </td>
               </tr>
               <tr>
                   <td colspan="2" style="height: 26px">
        <asp:Button ID="btnListar" runat="server" OnClick="btnListar_Click" Text="Listar" /></td>
               </tr>
           </table>
           &nbsp;&nbsp;
       </p>
       <p>
           <strong><span style="color: #ff0033"></span></strong></p>
       <p style="text-align: center">
        &nbsp;</p>
       <p style="text-align: center">
        <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="90%" EnableViewState="False">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="Direccion" HeaderText="Direcci&#243;n de Entrega" />
                <asp:BoundField DataField="CantidadArticulos" HeaderText="Cantidad de Art&#237;culos" >
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        &nbsp;</p>
    <p style="text-align: center">
        <asp:LinkButton ID="lbtnExportar" runat="server" Font-Size="XX-Large" Font-Overline="False" Font-Underline="False" ForeColor="#004C98" ToolTip="Crear Documento XML" EnableViewState="False" Visible="False" OnClick="lbtnExportar_Click"><h2><img src="Imagenes/iconos/xml.png"> Exportar  XML</h2></asp:LinkButton>&nbsp;&nbsp;</p>
    <p style="text-align: center">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="False"></asp:Label>&nbsp;</p>
    <p style="text-align: center">
        &nbsp;</p></center>
</asp:Content>

