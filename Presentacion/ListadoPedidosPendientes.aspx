<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ListadoPedidosPendientes.aspx.cs" Inherits="ListadoPedidosPendientes" Title="Listado de Pedidos Pendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<div>
    <h1 style="text-align: center">Listado de Pedidos Pendientes</h1>
        <p>
            &nbsp;</p>
        <p style="text-align: center">
            <asp:GridView ID="gvPedidosPendientes" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvPedidosPendientes_SelectedIndexChanged"  AllowPaging="True" OnPageIndexChanging="gvPedidosPendientes_PageIndexChanging" HorizontalAlign="Center">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                <Columns>
                    <asp:ImageField HeaderText="Foto" DataImageUrlField="Imagen">
                    <ControlStyle Height="100px" Width="100px" />
                    </asp:ImageField>
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre " />
                    <asp:BoundField DataField="Numero" HeaderText="Numero de Pedido" />
                    <asp:BoundField DataField="Generado" HeaderText="Generado" />
                    <asp:BoundField DataField="Enviado" HeaderText="Enviado" />
                    <asp:CommandField SelectText="Detalles del Pedido..." ShowSelectButton="True" />
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
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;</p>
    <p style="text-align: center">
        &nbsp;&nbsp;</p>
    <p>
        &nbsp;</p>
    
    </div>
</asp:Content>

