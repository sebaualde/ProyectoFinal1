<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Login" %>

<%@ Register Src="ControlLogin.ascx" TagName="ControlLogin" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
   <div id="headlogin">
       <asp:Image ID="Image1" runat="server" Height="15px" ImageUrl="~/Imagenes/iconos/llave.png"
           Width="15px" />
       Iniciar Sesión</div><div class="cajalogin"> <center><uc1:ControlLogin ID="ControlLogin1" runat="server"/></center></div>
</asp:Content>

