<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpcionesUsuario.ascx.cs" Inherits="OpcionesUsuario" %>
<table>
    <tr>
        <td style="text-align: center; height: 92px;">
            <asp:ImageButton ID="imgUsuario" runat="server" Height="90px" OnClick="imgUsuario_Click"
                Width="90px" /></td>
        <td style="text-align: center; height: 92px; width: 251px;">
            <asp:Panel ID="PanelCliente" runat="server" Height="50px" Width="100%">
            <asp:ImageButton ID="imgbtnPedidos" runat="server" Height="32px"
                Width="32px" ImageUrl="~/Imagenes/iconos/pedidos.png" ToolTip="Mis Pedidos" PostBackUrl="~/MisPedidos.aspx" />
            &nbsp;<asp:ImageButton ID="imgbtnCarrito" runat="server" ImageUrl="~/Imagenes/iconos/carrito.png" ToolTip="Carrito" PostBackUrl="~/MiCarrito.aspx" />&nbsp;
            <asp:LinkButton ID="lbtnRealizarPedido" runat="server" Font-Overline="False" Font-Underline="False" ForeColor="Black" ToolTip="Realizar Pedido" OnClick="lbtnRealizarPedido_Click"><img src="Imagenes/iconos/confirmarpedido.png">Realizar Pedido</asp:LinkButton>
            <asp:Label ID="lblCarrito" runat="server" ForeColor="Red" BorderColor="#00C000" Font-Bold="True" Font-Size="15pt"></asp:Label></asp:Panel>
            <asp:Panel ID="PanelAdministrador" runat="server" Height="50px" Width="100%">
                <asp:LinkButton ID="lbtnAdministracion" runat="server" Font-Overline="False" Font-Underline="False"
                    ForeColor="Black" PostBackUrl="~/PanelAdmin.aspx" ToolTip="Centro de Administración"><img src="Imagenes/iconos/admin.png">Administración</asp:LinkButton></asp:Panel>
            <br />
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align: center; height: 22px;">
            <asp:LinkButton ID="lbtnNombreUsuario" runat="server" Enabled="False" Font-Underline="False"
                ForeColor="Black"></asp:LinkButton></td>
        <td style="text-align: right; height: 22px; width: 251px;">
            &nbsp;<asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/Imagenes/iconos/logout.png"
                Width="16px" /><asp:LinkButton ID="lbtnSalir" runat="server" Font-Overline="False" Font-Underline="False" ForeColor="Black" OnClick="lbtnSalir_Click">Cerrar Sesión</asp:LinkButton></td>
    </tr>
    <tr>
        <td colspan="2" style="height: 22px; text-align: center">
            </td>
    </tr>
</table>
