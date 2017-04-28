<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ListadoPedidosGeneral.aspx.cs" Inherits="ListadoPedidosGeneral" Title="Listado General de Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<h1 style="text-align: center">Listado General de Pedidos</h1>
    <p style="text-align: center">
        <strong>Filtro</strong> <strong>de</strong> <strong>Esatdo</strong> <strong>del</strong>
        <strong>Pedido</strong>
        <asp:DropDownList ID="ddlEstadoPedido" runat="server" AutoPostBack="True">
            <asp:ListItem>Todos</asp:ListItem>
            <asp:ListItem>Pendientes</asp:ListItem>
            <asp:ListItem>Enviados</asp:ListItem>
        </asp:DropDownList></p>
    <p style="text-align: center">
        <strong>Busqueda</strong> <strong>de</strong> <strong>Pedidos</strong> <strong>por</strong>
        <strong>cédula</strong>:
        <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" /></p>
    <p style="text-align: center">
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;</p>
    <p style="text-align: center">
        <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333" GridLines="None" AllowPaging="True" OnSelectedIndexChanged="gvPedidos_SelectedIndexChanged" HorizontalAlign="Center" OnPageIndexChanging="gvPedidos_PageIndexChanging">
            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center"/>
            <Columns>
                <asp:ImageField HeaderText="Foto" DataImageUrlField="Imagen">
                <ControlStyle Height="100px" Width="100px" />
                </asp:ImageField>
                <asp:BoundField DataField="Cedula" HeaderText="C&#233;dula" />
                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                <asp:BoundField DataField="Numero" HeaderText="N&#250;mero del Pedido" />
                <asp:BoundField DataField="Generado" HeaderText="Generado" />
                <asp:BoundField DataField="Enviado" HeaderText="Enviado" />
                <asp:CommandField SelectText="Detalles..." ShowSelectButton="True" />
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
        &nbsp;&nbsp;</p>
    <p>
        &nbsp;</p>

</asp:Content>

