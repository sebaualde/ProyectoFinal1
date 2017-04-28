<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="PanelAdmin.aspx.cs" Inherits="PanelAdmin" Title="Panel de Administración" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
    <center><h1 style="text-align: center">
        Panel de Administración</h1>
    <p style="text-align: center">
        <table>
            <tr>
                <td colspan="5" style="width: 376px">
                    <h2 style="text-align: left">
                        Usuarios:</h2>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; width: 90px;">
                    <a href="ABMAdmin.aspx" style="text-decoration: none"><asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/iconos/administrator.png" />
                        <br />
                    Gestionar&nbsp;<br />
                        Administradores</a></td>
                <td style="height: 36px; text-align: center; width: 90px;">
                    <a href="ABMRegistrados.aspx" style="text-decoration: none"><asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/iconos/usuarios.png" />                 <br />
                    Gestión 
                        <br />
                        de Clientes</a></td>
                <td style="width: 90px; height: 36px; text-align: center">
                <a href="ListaDeUsuarios.aspx" style="text-decoration: none">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/iconos/listausuarios.png" />
                    <br />
                    <span style="color: #0000ff">
                    Lista de 
                        <br />
                        Usuarios</span><span style="color: #0000ff"></span></td>
            </tr>
        </table>
        <br />
    </p><hr style="width: 60%" />
    <p style="text-align: center">
        <table>
            <tr>
                <td colspan="4" style="width: 376px">
                    <h2 style="text-align: left">
                        Artículos:</h2>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; height: 36px; text-align: center">
                    <a href="ABMArticulos.aspx" style="text-decoration: none"><asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/iconos/articulos.png" />
                        <br />
                    Gestión de Artículos</a></td>
                <td style="height: 36px; text-align: center; width: 150px;">
                    <a href="ABMCategoriaArticulos.aspx" style="text-decoration: none"><asp:Image ID="Image5" runat="server" ImageUrl="~/Imagenes/iconos/categorias.png" />
                        <br />
                    Gestión de Categorías</a></td>
            </tr>
        </table>
        &nbsp;</p><hr style="width: 60%" />
        <p style="text-align: center">
        </p>
    <p style="text-align: center">
        <table style="text-align: center">
            <tr>
                <td colspan="5" style="width: 376px">
                    <h2 style="text-align: left">
                        Pedidos:</h2>
                </td>
            </tr>
            <tr>
                <td style="height: 36px; text-align: center; width: 90px;">
                <a href="ListadoPedidosPendientes.aspx" style="text-decoration: none">
                    &nbsp;<asp:Image ID="Image7" runat="server" ImageUrl="~/Imagenes/iconos/pedidos.png" /><br />
                        <span style="color: #0000ff">
                        Pendientes</span></td>
                <td style="height: 36px; text-align: center; width: 90px;">
                <a href="ListadoPedidosGeneral.aspx" style="text-decoration: none"> <asp:Image ID="Image6" runat="server" ImageUrl="~/Imagenes/iconos/pedidos.png" /><br />
                    <span style="color: #0000ff">
                            Generales</span></td>
                <td style="height: 36px; text-align: center; width: 90px;">
                <a href="ListaPedidosEntregadosPorFecha.aspx" style="text-decoration: none">
                    <asp:Image ID="Image8" runat="server" ImageUrl="~/Imagenes/iconos/pedidos.png" />
                    <br />
                    <span style="color: #0000ff">Por Fechas</span></td>
            </tr>
        </table>
    </p>
        <p>
            &nbsp;</p>
    <p>
        &nbsp;</p></center>
</asp:Content>

