<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="MostrarArticulo.aspx.cs" Inherits="MostrarArticulo" Title="Detalles del Articulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">

<div>
<center>
    <h1 style="text-align: center">Detalles del Articulo</h1>
        <p style="text-align: center">
            <table>
                <tr>
                    <td style="width: 293px; height: 6px">
                        <strong>Codigo: </strong>
                        <asp:TextBox ID="txtcodigoBarras" runat="server" BorderColor="White" BorderStyle="None"
                            ReadOnly="True" Width="118px"></asp:TextBox></td>
                    <td colspan="3" style="height: 6px; text-align: left">
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; <strong>Categoria:</strong>
                        <asp:TextBox ID="txtCategoria" runat="server" BorderColor="White" BorderStyle="None"
                            ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                        <asp:Image ID="imgFoto" runat="server" Height="393px" Width="473px" /></td>
                    <td colspan="2" style="text-align: left">
                        &nbsp;<asp:Label ID="lblNombre" runat="server" Font-Bold="True" Font-Size="XX-Large"
                            ForeColor="Maroon" Width="388px"></asp:Label>&nbsp;<br />
                        <br />
                        <span style="color: #ff0000"><strong style="font-size: 13pt">&nbsp; &nbsp; &nbsp; &nbsp; Precio:</strong></span> &nbsp; &nbsp; &nbsp;<asp:TextBox
                            ID="txtPrecio" runat="server" BorderColor="White" BorderStyle="None" Font-Bold="True"
                            Font-Size="Medium" ForeColor="Red" ReadOnly="True" Width="250px"></asp:TextBox><br />
                        <br />
                        <strong><span style="color: #00cc00">&nbsp; &nbsp; &nbsp; &nbsp; Stock:&nbsp;</span></strong>
                        <asp:TextBox ID="txtStock" runat="server" BorderColor="White" BorderStyle="None"
                            Font-Bold="True" ForeColor="#00C000" ReadOnly="True"></asp:TextBox><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="2" style="text-align: center">
                        &nbsp;<br />
                        <strong>Descripción:<br />
                            <br />
                            <asp:Label ID="lblDescripcion" runat="server" Font-Underline="False" Width="451px" Font-Bold="False" Font-Italic="False"></asp:Label><br />
                        </strong>
                        </td>
                    <td rowspan="2" style="width: 334px; text-align: center;">
                        <strong> Cantidad<br />
                        </strong>&nbsp;<asp:TextBox ID="txtCantidad" runat="server" Width="35px"></asp:TextBox></td>
                    <td rowspan="2" style="width: 2981px; text-align: left">
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="100px" ImageUrl="~/Imagenes/iconos/business-cart3.png"
                            Width="132px" OnClick="ImageButton1_Click" /></td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel2" runat="server" BackColor="#C0FFC0" BorderColor="ControlLight"
                            BorderStyle="Dashed" Height="60px">
                            <br />
                            <asp:Label ID="lblAgregado" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="Red"></asp:Label>&nbsp;</asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" rowspan="2">
                        <br />
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <br />
                    </td>
                </tr>
            </table>
        </p>
    </center>
    </div>
</asp:Content>

