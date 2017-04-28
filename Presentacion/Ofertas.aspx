<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="Ofertas.aspx.cs" Inherits="Ofertas" Title="Ofertas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<h1 style="text-align: center">
    <span style="color: #990033">Nuestras Ofertas</span></h1>
    <p style="text-align:center">
        <asp:Panel ID="Panel2" runat="server" BackColor="#C0FFC0" BorderColor="ControlLight"
            BorderStyle="Dashed" Height="60px" HorizontalAlign="Center">
            <br />
            <asp:Label ID="lblAgregado" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="Red"></asp:Label>&nbsp;</asp:Panel>
        &nbsp;</p>
    <p>
        <asp:GridView ID="gvArticulos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="4" EnableTheming="True" ForeColor="#333333" GridLines="None" HorizontalAlign="Center"
            OnPageIndexChanging="gvArticulos_PageIndexChanging" OnRowCommand="gvArticulos_RowCommand"
            OnSelectedIndexChanged="gvArticulos_SelectedIndexChanged" PageSize="4">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="CodigoBarras" HeaderText="Codigo" />
                <asp:ImageField DataImageUrlField="Imagen" HeaderText="Foto" NullDisplayText="No Disponible">
                    <ControlStyle Height="100px" Width="100px" />
                </asp:ImageField>
                <asp:BoundField DataField="Nombre" HeaderText="Articulo" />
                <asp:BoundField DataField="Precio" DataFormatString="USD {0:d}" HeaderText="Precio" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                <asp:ButtonField ButtonType="Button" CommandName="AgregarCarrito" ImageUrl="~/Imagenes/iconos/business-cart3.png"
                    Text="Agregar Carrito">
                    <ControlStyle BackColor="PaleGreen" BorderColor="#E0E0E0" BorderStyle="Dotted" Font-Bold="True"
                        Font-Size="10pt" ForeColor="Green" Height="50px" Width="120px" />
                </asp:ButtonField>
                <asp:CommandField HeaderText="Detalles" SelectText="Detalles..." ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" BorderStyle="None" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Wrap="False" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        &nbsp;</p>
    <p style="text-align: center">
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        &nbsp;</p>
</asp:Content>

