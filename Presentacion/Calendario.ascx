<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Calendario.ascx.cs" Inherits="Calendario" %>
<asp:Panel ID="Panel1" runat="server" BackColor="#E0E0E0" BorderColor="Transparent" Height="56px"
    Width="332px" Font-Bold="True">
    <br />
    Dia: &nbsp; &nbsp;<asp:DropDownList ID="ddlDia" runat="server">
    </asp:DropDownList>
    &nbsp;Mes:&nbsp;<asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    &nbsp; Año:&nbsp;<asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="True">
    </asp:DropDownList></asp:Panel>
