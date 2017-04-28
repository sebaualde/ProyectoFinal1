<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetallesDelPedido.ascx.cs" Inherits="DetallesDelPedido" %>
<div>
    <h1 style="text-align: center">
                        <asp:Label ID="lblNombreUsuario" runat="server" Font-Bold="True" Font-Names="Arial Black" ForeColor="Maroon" style="font-size: 24pt"></asp:Label>
        </h1>
    <p style="text-align: center">
                        <asp:Image ID="imgFoto" runat="server" Height="128px" Width="128px" />&nbsp;</p>
    <p style="text-align: center">
                        <asp:Label ID="lblCodigoPedido" runat="server" Font-Bold="True" ForeColor="#C04000"></asp:Label>&nbsp;</p>
    <p style="text-align: center">
                        <asp:Label ID="lblPrecioTotal" runat="server" Font-Bold="True" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>&nbsp;</p>
    <p style="text-align: center">
                        <asp:GridView ID="gvDetallePedido" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None"  OnPageIndexChanging="gvDetallePedido_PageIndexChanging" HorizontalAlign="Center">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Articulo" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio /u" DataFormatString="USD {0:c}" />
                                <asp:BoundField DataField="PrecioTotal" HeaderText="Total" />
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
        &nbsp;</p>
    <p style="text-align: center">
        &nbsp;
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;&nbsp;</p>
    <p style="text-align: center">
        &nbsp;
        <asp:Button ID="btnEnviarPedido" runat="server" Text="Enviar Pedido" Width="110px" OnClick="btnEnviarPedido_Click" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
        <asp:Button ID="btnDuplicar" runat="server" Text="Duplicar" OnClick="btnDuplicar_Click" />&nbsp;</p>
    <p style="text-align: center">
        <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar"
            Visible="False" />
        &nbsp;<asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar"
            Visible="False" />
        &nbsp;&nbsp;</p>
    <p style="text-align: center">
        &nbsp;</p>
    
    </div>