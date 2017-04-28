<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ABMAdmin.aspx.cs" Inherits="ABMAdmin" Title="Mantenimiento de Administradores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<div><center>

        <h1>
            Mantenimiento de Administradores</h1>
        <p>
            <table>
                <tr>
                    <td style="text-align: right">
                        Cédula:</td>
                    <td style="width: 158px">
                        <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox></td>
                    <td style="text-align: left">
                        <asp:LinkButton ID="lbtnBuscar" runat="server" Font-Overline="False" ForeColor="Black" OnClick="lbtnBuscar_Click1"><h2><img width="20px" height="20px" src="Imagenes/iconos/buscar.png">Buscar</h2></asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Nombre completo:</td>
                    <td style="width: 158px">
                        <asp:TextBox ID="txtNombreCompleto" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Nombre de usuario:
                    </td>
                    <td style="width: 158px">
                        <asp:TextBox ID="txtNombreUsuario" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Contraseña:
                    </td>
                    <td style="width: 158px">
                        <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Confirmar Contraseña:</td>
                    <td style="width: 158px">
                        <asp:TextBox ID="txtConfirmacion" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;Avatar:
                    </td>
                    <td style="width: 158px">
                        <strong><span style="font-size: 10pt">SUBIR DESDE EL EQUIPO:<br />
                        </span></strong>
                        <asp:FileUpload ID="subirImagen" runat="server" Width="150px" /><br />
                        <br />
                        <strong><span style="font-size: 10pt">USAR IMAGEN DEFAULT:<br />
                        </span></strong>
                        <asp:RadioButtonList ID="RBLSexo" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBLSexo_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="hombre">Hombre</asp:ListItem>
                            <asp:ListItem Value="mujer">Mujer</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="text-align: left">
                        <asp:Image ID="imgAvatar" runat="server" Height="128px" Width="128px" /></td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 26px;">
                        Cargo:
                    </td>
                    <td style="height: 26px; width: 158px;">
                        <asp:TextBox ID="txtCargo" runat="server"></asp:TextBox></td>
                    <td style="height: 26px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 158px">
                        <asp:ImageButton ID="imgbtnAgregar" runat="server" ImageUrl="~/Imagenes/iconos/agregar.png" AlternateText="Agregar" ToolTip="Agregar" OnClick="imgbtnAgregar_Click" />
                        <asp:ImageButton ID="imgbtnEditar" runat="server" ImageUrl="~/Imagenes/iconos/editar.png" AlternateText="Editar" ToolTip="Editar" OnClick="imgbtnEditar_Click" />
                        <asp:ImageButton ID="imgbtnBorrar" runat="server" ImageUrl="~/Imagenes/iconos/borrar.png" AlternateText="Eliminar" ToolTip="Eliminar" OnClick="imgbtnBorrar_Click" /></td>
                    <td style="text-align: right">
                        <asp:ImageButton ID="imgbtnLimpiar" runat="server" ImageUrl="~/Imagenes/iconos/limpiar.png" AlternateText="Limpiar Formulario" ToolTip="Limpiar el Formulario" OnClick="imgbtnLimpiar_Click" /></td>
                </tr>
            </table>
        </p>
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
        &nbsp;</p>
    </center>
    
    </div>
</asp:Content>

