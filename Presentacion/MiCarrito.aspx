<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="MiCarrito.aspx.cs" Inherits="MiCarrito" Title="Mi Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<h1 style="text-align: center">Mi carrito de compras.</h1>
    <p style="text-align: center">
        <asp:Panel ID="Panel2" runat="server" Visible="False" HorizontalAlign="Center">
            <asp:Image ID="imgFoto" runat="server" Height="128px" Width="128px" />
            <br />
            <br />
            <asp:Label ID="lblNombreUsuario" runat="server" Font-Bold="True" Font-Names="Arial Black"
                ForeColor="Maroon" Style="font-size: 24pt"></asp:Label></asp:Panel>
        &nbsp;</p>
    <p style="text-align: center">
        <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnPageIndexChanging="gvCarrito_SelectedIndexChanged"
            OnSelectedIndexChanged="gvCarrito_SelectedIndexChanged" >
            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="Numero" HeaderText="Numero" />
                <asp:BoundField DataField="Nombre" HeaderText="Articulo" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" DataFormatString="USD {0:c}" HeaderText="Precio /u" />
                <asp:BoundField DataField="PrecioTotal" DataFormatString="USD {0:c}" HeaderText="Total" />
                <asp:CommandField ButtonType="Button" HeaderText="Eliminar Articulo" ShowSelectButton="True" SelectImageUrl="~/iconos/borrar.png" SelectText="Eliminar" >
                    <ItemStyle BorderColor="Red" />
                </asp:CommandField>
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
        <asp:Panel ID="Panel1" runat="server" BackColor="#C0FFC0" Height="59px" BorderColor="#E0E0E0" BorderStyle="Dashed" Visible="False" HorizontalAlign="Center">
            <br />
            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="16pt" ForeColor="#FF0000"></asp:Label></asp:Panel>
        &nbsp;</p>
    <p style="text-align: center">
        <asp:Button ID="btnSolicitarPedido" runat="server" Text="Generar Pedido" OnClick="btnSolicitarPedido_Click" />
        <asp:Button ID="btnEliminarPedido" runat="server" Text="Vaciar Carrito" OnClick="btnEliminarPedido_Click" /></p>
    <p style="text-align: center">
        &nbsp;</p>
</asp:Content>

