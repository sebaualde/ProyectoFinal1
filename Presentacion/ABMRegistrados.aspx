<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ABMRegistrados.aspx.cs" Inherits="ABMRegistrados" Title="Mantenimiento de Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<div>
    <center>    <h1>
            Mantenimiento de Clientes</h1>
            <table>
                <tr>
                    <td style="text-align: right">
                        Cédula:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox></td>
                    <td style="text-align: left">
                        <asp:LinkButton ID="lbtnBuscar" runat="server" Font-Overline="False" ForeColor="Black"
                            OnClick="lbtnBuscar_Click1"><h2><img width="20px" height="20px" src="Imagenes/iconos/buscar.png">Buscar</h2></asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Nombre completo:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombreCompleto" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Nombre de usuario:
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombreUsuario" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Contraseña:
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Confirmar Contraseña:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtConfirmacion" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;Avatar:
                    </td>
                    <td>
                        <strong><span style="font-size: 10pt">SUBIR DESDE EL EQUIPO:</span><br />
                        </strong>
                        <asp:FileUpload ID="subirImagen" runat="server" Width="150px" />
                        <br />
                        <br />
                        <strong><span style="font-size: 10pt">USAR IMAGEN DEFAULT</span></strong><br />
                        <asp:RadioButtonList ID="RBLSexo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBLSexo_SelectedIndexChanged"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="hombre">Hombre</asp:ListItem>
                            <asp:ListItem Value="mujer">Mujer</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="text-align: left">
                        <asp:Image ID="imgAvatar" runat="server" Height="128px" Width="128px" /></td>
                </tr>
                <tr>
                    <td style="height: 26px; text-align: right">
                        Dirección de envío:
                    </td>
                    <td style="height: 26px; text-align: left;">
                        <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox></td>
                    <td style="height: 26px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 26px; text-align: right">
                        Nº Tarjeta de crédito</td>
                    <td style="height: 26px; text-align: left">
                        <asp:TextBox ID="txtNroTarjeta" runat="server"></asp:TextBox></td>
                    <td style="height: 26px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 26px; text-align: right">
                        Teléfono:</td>
                    <td style="height: 26px; text-align: left">
                        <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox></td>
                    <td style="height: 26px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnAgregar" runat="server" AlternateText="Agregar" ImageUrl="~/Imagenes/iconos/agregar.png"
                            OnClick="imgbtnAgregar_Click" ToolTip="Agregar" />
                        <asp:ImageButton ID="imgbtnEditar" runat="server" AlternateText="Editar" ImageUrl="~/Imagenes/iconos/editar.png"
                            OnClick="imgbtnEditar_Click" ToolTip="Editar" />
                        <asp:ImageButton ID="imgbtnBorrar" runat="server" AlternateText="Eliminar" ImageUrl="~/Imagenes/iconos/borrar.png"
                            OnClick="imgbtnBorrar_Click" ToolTip="Eliminar" /></td>
                    <td style="text-align: right">
                        <asp:ImageButton ID="imgbtnLimpiar" runat="server" AlternateText="Limpiar Formulario"
                            ImageUrl="~/Imagenes/iconos/limpiar.png" OnClick="imgbtnLimpiar_Click" ToolTip="Limpiar el Formulario" /></td>
                </tr>
            </table>
        
        <p>
            <asp:Label ID="lblMensaje" runat="server" EnableViewState="False"></asp:Label>&nbsp;</p>
        <p>
            <asp:Panel ID="PanelConfirmacion" runat="server" Height="50px" Visible="False" Width="125px">
                <br />
                <asp:LinkButton ID="lbtnConfirmarEliminacion" runat="server" Font-Bold="True" OnClick="lbtnConfirmarEliminacion_Click"><img width="20px" height="20px"  src="Imagenes/iconos/ok.png"> SI</asp:LinkButton>
                &nbsp; &nbsp;<asp:LinkButton ID="lbtnNoEliminar" runat="server" Font-Bold="True"
                    OnClick="lbtnNoEliminar_Click"><img width="20px" height="20px"  src="Imagenes/iconos/cancelar.png"> NO</asp:LinkButton></asp:Panel>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;&nbsp;</p>
    </center>
    </div>
</asp:Content>

