<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ABMCategoriaArticulos.aspx.cs" Inherits="ABMCategoriaArticulos" Title="Mantenimiento de Categorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<div>
<center>
    <h1 style="text-align: center; font-size: 24pt;">
        Mantenimiento de Categorias</h1>
        <p style="text-align: center">
            <table style="width: 616px; height: 109px">
                <tr>
                    <td style="width: 211px; text-align: right">
                        Nombre de la Categoria:</td>
                    <td style="width: 280px; text-align: left;">
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox></td>
                    <td style="text-align: center; width: 113px;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" /></td>
                </tr>
                <tr>
                    <td style="width: 211px; height: 73px; text-align: right">
                        Descripción:</td>
                    <td style="width: 280px; height: 73px; text-align: left;">
                        <asp:TextBox ID="txtDescripcion" runat="server" Enabled="False" Height="60px" TextMode="MultiLine"
                            Width="270px"></asp:TextBox></td>
                    <td style="height: 73px; width: 113px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 211px; height: 53px; text-align: center">
                    </td>
                    <td style="width: 280px; height: 53px; text-align: center">
                        <asp:Button ID="btnAgregar" runat="server" Enabled="False" Text="Agregar" OnClick="btnAgregar_Click" />
                        <asp:Button ID="btnModificar" runat="server" Enabled="False" Text="Modificar" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnEliminar" runat="server" Enabled="False" Text="Eliminar" OnClick="btnEliminar_Click" />
                    </td>
                    <td style="height: 53px; text-align: center; width: 113px;">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" /></td>
                </tr>
            </table>
        </p>
        <p>
                        <asp:Panel ID="Panel1" runat="server" BackColor="#C0FFC0" BorderColor="#E0E0E0" BorderStyle="Dashed"
                            Visible="False" Width="60%" Height="50px">
                            <asp:Label ID="lblMensaje" runat="server" BackColor="Transparent" Font-Bold="True" Font-Size="14pt"></asp:Label></asp:Panel>
                        &nbsp;</p>
    <p>
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Visible="False" OnClick="btnAceptar_Click" />
        &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnSalir" runat="server" Text="Salir" Visible="False" OnClick="btnSalir_Click" Width="70px" /></p>
    <p>
                        &nbsp;</p>
    </center>
    </div>
</asp:Content>

