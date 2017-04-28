<%@ Page Language="C#" MasterPageFile="~/Usuarios.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="MiTienda.com" MaintainScrollPositionOnPostback="true" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<div style="text-align: center">
        <strong>
            <asp:Panel ID="Panel1" runat="server" BackColor="#C0FFC0" BorderColor="ControlLight"
                BorderStyle="Dashed" Height="50px" Visible="False">
                Buscando por:
                <asp:Label ID="lblBuscando" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;
                || Eliminar Filtro
                <asp:ImageButton ID="imgBtnEliminarFiltro" runat="server" ImageUrl="~/iconos/cancelar.png"
                    OnClick="imgBtnEliminarFiltro_Click" /></asp:Panel>
        </strong>
        <br />
        <strong>&nbsp; &nbsp;&nbsp;
        Filtro por Categorias</strong> &nbsp;<strong>&nbsp;</strong><asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="True">
        </asp:DropDownList><strong> &nbsp;&nbsp;&nbsp; Ordenar Categorias de:&nbsp;<asp:DropDownList
            ID="ddlOrdenXCategoria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdenXCategoria_SelectedIndexChanged">
            <asp:ListItem Value="SinOrdenar">Sin Ordenar</asp:ListItem>
            <asp:ListItem>A - Z</asp:ListItem>
            <asp:ListItem>Z - A</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;&nbsp; &nbsp;Ordenar por Precio:</strong> 
    <asp:DropDownList ID="ddlOrdenXPrecio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdenXPrecio_SelectedIndexChanged">
        <asp:ListItem Value="SinOrdenar">Sin Ordenar</asp:ListItem>
        <asp:ListItem Value="MenorAMayor">Menor A Mayor</asp:ListItem>
        <asp:ListItem Value="MayorAMenor">Mayor A Menor</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;<br />
    <br />
        Mostrar
        <asp:DropDownList ID="ddlCantidadResultados" runat="server" AutoPostBack="True">
            <asp:ListItem Value="0">Todos</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
        </asp:DropDownList>
        articulos por pagina.<br />
    <br />
    <asp:Panel ID="Panel2" runat="server" BackColor="#C0FFC0" BorderColor="ControlLight"
                BorderStyle="Dashed" Height="60px">
        <br />
        <asp:Label ID="lblAgregado" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="Red"></asp:Label>&nbsp;</asp:Panel>
    &nbsp;
        <br />
        <asp:GridView ID="gvArticulos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvArticulos_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="gvArticulos_PageIndexChanging" HorizontalAlign="Center" OnRowCommand="gvArticulos_RowCommand" EnableTheming="True" >
            <Columns>
                <asp:BoundField DataField="CodigoBarras" HeaderText="Codigo"/>
                <asp:ImageField DataImageUrlField="Imagen" HeaderText="Foto" NullDisplayText="No Disponible">
                    <ControlStyle Height="100px" Width="100px" />
                </asp:ImageField>
                <asp:BoundField DataField="Nombre" HeaderText="Articulo" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="USD {0:d}" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                <asp:ButtonField  CommandName="AgregarCarrito"  ImageUrl="~/Imagenes/iconos/business-cart3.png"
                    Text="Agregar Carrito" ButtonType="Button">                    
                    <ControlStyle Height="50px" Width="120px" BackColor="PaleGreen" BorderColor="#E0E0E0" BorderStyle="Dotted" Font-Bold="True" Font-Size="10pt" ForeColor="Green"/>
                </asp:ButtonField>
                <asp:CommandField SelectText="Detalles..." ShowSelectButton="True" HeaderText="Detalles"/>
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" BorderStyle="None" VerticalAlign="Middle" Wrap="False" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <br />
        &nbsp;<asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;<br />
        <br />
        &nbsp; &nbsp;<br />
        &nbsp;

    </div>
</asp:Content>

