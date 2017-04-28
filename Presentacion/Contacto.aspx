<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="Contacto.aspx.cs" Inherits="Contacto" Title="Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<center>
<h1 style="text-align: center">Contacto con nuestra empresa</h1>
        <asp:Panel ID="Panel1" runat="server" Height="50px" Width="807px">
            Si desea ser un Usuario registrado de nuestra pagina para poder realizar compras
            y pedidos, envienos sus datos (Cedula, Nombre Completo, Nombre de Usuario, Contraseña,
            Foto(opcional), Dirección, Nº Tarjeta y Telefono ) a nuestro mail de administrador.</asp:Panel>
        &nbsp;
    <p style="text-align: center">
        <table style="width: 284px; text-align: left;">
            <tr>
                <td style="height: 50px; width: 74px;">
                    <asp:Image ID="imgMail" runat="server" ImageUrl="~/Imagenes/mail.png" /></td>
                <td style="height: 50px">
                    <span style="color: #0000cc">miTienda@gmail.com<br />
                        admin@miTienda.com</span></td>
            </tr>
            <tr>
                <td style="width: 74px">
                    <asp:Image ID="imgFacebook" runat="server" ImageUrl="~/Imagenes/facebook.png" Height="50px" Width="50px" /></td>
                <td>
                    <span style="color: #0000cc">www.facebook.com/MiTienda</span></td>
            </tr>
            <tr>
                <td style="width: 74px">
                    <asp:Image ID="imgTwitter" runat="server" ImageUrl="~/Imagenes/twitter.png" Width="50px" /></td>
                <td>
                    <span style="color: #0000cc">@miTienda</span></td>
            </tr>
        </table>
    </p>
    </center>
</asp:Content>

