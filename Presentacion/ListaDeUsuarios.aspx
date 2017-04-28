<%@ Page Language="C#" MasterPageFile="~/Administradores.master" AutoEventWireup="true" CodeFile="ListaDeUsuarios.aspx.cs" Inherits="ListaDeUsuarios" Title="Lista de Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server" >
<center><div>
    <h1>
        Lista de Usuarios</h1>
        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
            <asp:ListItem Value="todos">Todos</asp:ListItem>
            <asp:ListItem Value="administradores">Administradores</asp:ListItem>
            <asp:ListItem Value="clientes">Clientes</asp:ListItem>
        </asp:DropDownList><br />
        <br />
        <asp:GridView ID="gvUsuarios" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="90%" AutoGenerateColumns="False" HorizontalAlign="Center">
            <Columns>
                <asp:ImageField DataImageUrlField="Imagen">
                    <ControlStyle Height="100px" Width="100px" />
                </asp:ImageField>
                <asp:BoundField DataField="Cedula" HeaderText="C&#233;dula" />
                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="DireccionEnvio" HeaderText="Direcci&#243;n de Env&#237;o" NullDisplayText="No Disponible" />
                <asp:BoundField DataField="NumeroTarjeta" HeaderText="N&#186; Tarjeta de cr&#233;dito" NullDisplayText="No Disponible" />
                <asp:BoundField DataField="Telefono" HeaderText="Tel&#233;fono" NullDisplayText="No Disponible" />
                <asp:BoundField DataField="Cargo" HeaderText="Cargo" NullDisplayText="No Disponible" />
            </Columns>
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
            <EditRowStyle BackColor="#7C6F57" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        &nbsp;&nbsp;<br />
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    <br />
        <br />
        
  
        <br />
    <br />
  </div>
  </center>
</asp:Content>

