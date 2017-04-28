<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="MiPerfil.aspx.cs" Inherits="MiPerfil" Title="Mi Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
    <center>
                <asp:Label ID="lblNombreUsuario" runat="server" Font-Names="Arial Black" Font-Size="32pt" ForeColor="#004C98"></asp:Label>&nbsp;
    
    <div style="width:402px;"><div class="cajaperfilhead">
        <span style="font-size: 18px; font-family: Arial"><strong>TUS DATOS</strong></span></div>
        <div class="cajaperfil">
        <table width="85%">
        <tr>
            <td rowspan="2" style="width: 125px; text-align: center">
                <asp:Image ID="imgFotoPerfil" runat="server" Height="125px" Width="125px" /></td>
        </tr>
        <tr>
            <td valign="top" style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px">
                <strong>
                    Nombre completo:</strong><asp:Label ID="lblNombreCompleto" runat="server"></asp:Label><br />
                <strong>
                    Cédula: </strong>
                    <asp:Label ID="lblCedula" runat="server"></asp:Label><br />
                <strong>
                    Dirección de envío:</strong>
                    <asp:Label ID="lblDireccion" runat="server"></asp:Label><br />
                    <strong>
                    Tarjeta de crédito:</strong>
                    <asp:Label ID="lblTarjeta" runat="server"></asp:Label><br />
                    <strong>
                    Teléfono de contacto:</strong>
                    <asp:Label ID="lblTelefono" runat="server"></asp:Label></td>
        </tr>
    </table> </div>
    </div>
    
    <div style="width:402px;"><div class="cajaperfilhead">
        <span style="font-size: 18px; font-family: Arial"><strong>TU ACTIVIDAD</strong></span></div>
        <div class="cajaperfil">
            <strong>
                <table width="100%">
                    <tr>
                        <td style="text-align: center">
                            PEDIDOS</td>
                        <td style="text-align: center">
                            ENTREGADOS</td>
                        <td style="text-align: center">
                            PENDIENTES</td>
                        <td style="text-align: center">
                            COSTO TOTAL</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                    <asp:Label ID="lblCdadPedidos" runat="server" Font-Size="32pt" ForeColor="ActiveCaption"></asp:Label></td>
                        <td style="text-align: center">
                    <asp:Label ID="lblPedidosEntregados" runat="server" Font-Size="32pt" ForeColor="ActiveCaption"></asp:Label></td>
                        <td style="text-align: center">
                    <asp:Label ID="lblPedidosPendientes" runat="server" Font-Size="32pt" ForeColor="ActiveCaption"></asp:Label></td>
                        <td style="text-align: center">
                    <asp:Label ID="lblCostoTotal" runat="server" Font-Size="32pt" ForeColor="ActiveCaption"></asp:Label></td>
                    </tr>
                </table>
            </strong>
                    </div>
    </div>
    
        &nbsp;</center>
    <center>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;</center>
  
      
       
</asp:Content>

