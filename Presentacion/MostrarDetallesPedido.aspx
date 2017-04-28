<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="MostrarDetallesPedido.aspx.cs" Inherits="MostrarDetallesPedido" Title="Detalles del Pedido Pendiente" %>

<%@ Register Src="DetallesDelPedido.ascx" TagName="DetallesDelPedido" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<div>
    <h1 style="text-align: center">Detalles del Pedido Pendiente</h1>
    <p style="text-align: center">
        <uc2:DetallesDelPedido ID="DetallesDelPedido1" runat="server" />
        &nbsp;&nbsp;</p>
    
    </div>
</asp:Content>

