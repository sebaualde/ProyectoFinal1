<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="MisPedidos.aspx.cs" Inherits="MisPedidos" Title="Mis Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
    <h1 style="text-align: center">
        Mis Pedidos</h1>
    <center>    <asp:GridView ID="gvMisPedidos" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="60%" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvMisPedidos_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Numero" HeaderText="N&#250;mero" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" SortExpression="si" />
                <asp:ImageField DataAlternateTextField="AltEnviado" DataImageUrlField="Enviado" HeaderText="Enviado">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="50px" />
                </asp:ImageField>
                <asp:CommandField SelectText="Detalles" ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        &nbsp;</center>
    <center>
        <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/iconos/clock.png" /><strong>
            Pendiente<br />
            </strong>
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/iconos/tick.png" /><strong>
            Enviado</strong></asp:Panel>
        &nbsp;</center>
    <center>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;</center>
        
</asp:Content>

