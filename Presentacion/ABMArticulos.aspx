<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ABMArticulos.aspx.cs" Inherits="ABMArticulos" Title="Mantenimiento de Articulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<div>
<center>
    <h1 style="text-align: center">
        Mantenimiento de Articulos</h1>
        <p style="text-align: center">
            <table style="width: 642px; height: 51px">
                <tr>
                    <td style="width: 113px; text-align: right;">
                        Codigo de Barras:</td>
                    <td style="width: 322px; text-align: left;">
                        &nbsp;<br />
                        &nbsp;
                        <asp:TextBox ID="txtCodigoBarras" runat="server"></asp:TextBox>&nbsp; <span style="color: #cccccc">
                            <strong style="font-size: 8pt">&nbsp;<br />
                                <br />
                                &nbsp; &nbsp; Coloque un Codigo Numerico y Presione Buscar.</strong></span></td>
                            <br />
                    <td style="text-align: left">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" /></td>
                </tr>
                <tr>
                    <td style="width: 113px; text-align: right">
                    </td>
                    <td style="width: 322px">
                        <br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px; text-align: right;">
                        Nombre:</td>
                    <td style="width: 322px; text-align: left;">
                        &nbsp;
                        <asp:TextBox ID="txtNombre" runat="server" Enabled="False"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px; text-align: right;">
                        Precio:</td>
                    <td style="width: 322px; text-align: left;">
                        &nbsp;
                        <asp:TextBox ID="txtPrecio" runat="server" Enabled="False"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px; text-align: right;">
                        Stock:</td>
                    <td style="width: 322px; text-align: left;">
                        &nbsp;
                        <asp:TextBox ID="txtStock" runat="server" Enabled="False"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px; height: 62px; text-align: right;">
                        Descripción: &nbsp;</td>
                    <td style="width: 322px; height: 62px; text-align: left;">
                        &nbsp;
                        <asp:TextBox ID="txtDescripcion" runat="server" Enabled="False" Height="89px" TextMode="MultiLine"
                            Width="292px"></asp:TextBox></td>
                    <td style="height: 62px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px; text-align: right">
                        <br />
                        Categoria:</td>
                    <td style="width: 322px; text-align: left;">
                        <br />
                        &nbsp;&nbsp;<asp:DropDownList ID="ddlCategorias" runat="server" Enabled="False">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px; text-align: right;">
                        Imagen:</td>
                    <td style="width: 322px; text-align: left;">
                        &nbsp;<br />
                        &nbsp;
                        <asp:FileUpload ID="fuOrigenImagen" runat="server" Enabled="False"  /><br />
                    </td>
                    <td style="text-align: left">
                        <asp:Image ID="imgImagen" runat="server" Height="128px" Width="128px" /></td>
                </tr>
                <tr>
                    <td style="width: 113px">
                    </td>
                    <td style="width: 322px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 113px">
                    </td>
                    <td style="width: 322px; text-align: left;">
                        &nbsp;
                        <asp:Button ID="btnAgregar" runat="server" Enabled="False" Text="Agregar" OnClick="btnAgregar_Click" />
                        <asp:Button ID="btnModificar" runat="server" Enabled="False" Text="Modificar" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnEliminar" runat="server" Enabled="False" Text="Eliminar" OnClick="btnEliminar_Click" /></td>
                    <td style="text-align: left">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 6px; text-align: center">
                        <br />
                        <asp:Panel ID="Panel1" runat="server" BackColor="#C0FFC0" BorderColor="#E0E0E0" BorderStyle="Dashed" Visible="False">
                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="14pt"></asp:Label></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="height: 6px" colspan="3">
                        <br />
                        &nbsp;<asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Visible="False" OnClick="btnAceptar_Click" />
                        <asp:Button ID="btnSalir" runat="server" Text="Salir" Visible="False" OnClick="btnSalir_Click" Width="71px" /><br />
                        <br />
                        &nbsp;<br />
                    </td>
                </tr>
            </table>
        </p>
    </center>
    </div>
</asp:Content>

