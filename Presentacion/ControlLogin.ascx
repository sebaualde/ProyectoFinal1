<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlLogin.ascx.cs" Inherits="ControlLogin" %>
<table>
    <tr>
        <td style="text-align: right">
            Cédula:
<asp:TextBox ID="txtCedula" runat="server" Width="140px"></asp:TextBox></td>
        <td rowspan="2" style="text-align: center">
            <asp:ImageButton ID="imgBtnLogin" runat="server" ImageUrl="~/Imagenes/botonlogin.png" OnClick="imgBtnLogin_Click" /></td>
    </tr>
    <tr>
        <td style="text-align: right">
            Contraseña:
            <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: right">
            <asp:Label ID="lblCarrito" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="Red"></asp:Label>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/iconos/carrito.png"
                PostBackUrl="~/MiCarrito.aspx" />
            <asp:Image ID="imgError" runat="server" EnableViewState="False" Height="20px" ImageUrl="~/Imagenes/iconos/error.png"
                Visible="False" Width="20px" />
            <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
    </tr>
</table>
&nbsp;&nbsp;<br />
&nbsp;&nbsp;
