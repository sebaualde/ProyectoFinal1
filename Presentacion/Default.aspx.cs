using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;
using Logica;

public partial class Default : System.Web.UI.Page
{
    static int ultimaPagina;
    static string seleccionDeOrdenamiento = ""; //variable para guardar en sesion la categoria y que deje los valores anteriores cuando se va a detalles de productos y se hace click en volver

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.CacheControl = "no-cache";

            if (!IsPostBack) //carga el ddlCategorias si no es un Postback
            {
                List<CategoriaArticulo> Categorias = LogicaCategoriaArticulo.Listar();

                if (Categorias.Count > 0)
                {
                    ddlCategorias.Items.Add(new ListItem("Todas", "Todas"));

                    foreach (CategoriaArticulo c in Categorias)
                    {
                        ddlCategorias.Items.Add(new ListItem(c.Nombre, c.Nombre));
                    }
                }
                else
                {
                    throw new ExcepcionPresentacion("¡ERROR! No hay Categorias ingresadas.");
                }

            }

            ControlDeSeleccionDeCarga();
        }
        catch (ApplicationException ex)
        {
            Panel2.Visible = false;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }

        catch
        {
            Panel2.Visible = false;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! Al cargar la pagina, no se encontro el repositorio de base de datos.";
        }

    }

    protected void ControlDeSeleccionDeCarga()
    {
        string mensaje = (string)Session["Mensaje"]; //entrega un mensaje desde otra pagina si se sucito un error por ejemplo de acceso a una pagina no permitida

        if (mensaje != null)
        {
            if (mensaje.Contains("¡ERROR!"))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

            lblMensaje.Text = mensaje;

            Session.Remove("Mensaje");
        }

        lblMensaje.Text = mensaje;
        Panel2.Visible = false;

        if (Session["CodigoArticulo"] == null && Session["NombreBuscado"] == null) //si en sesion no hay codigo de articulo ni nombre se carga normalmente
        {
            CargarGridView();
        }

        if (Session["CodigoArticulo"] != null) //control para cuando se vuelve de los detalles quedar en la misma pagina, categoria, ordenamiento y cantidad de articulos por pagina
        {
            RegresoMostrarDetalles();
        }

        if (Session["NombreBuscado"] != null) //se inicio una busqueda por nombre
        {            
            CargarGridXBsqueda();
            ddlOrdenXCategoria.Enabled = false;
        }
    }

    protected void CargarGridView()
    {
        string categoria = ddlCategorias.SelectedValue;

        List<Articulo> Articulos = LogicaArticulo.ListarTodosLosArticulos();

        bool ordenamiento; //variable para definir el ordenamiento por precio que tenga el grid view en la consulta a la base de datos

        if (categoria == "Todas")
        {
            if (ddlOrdenXPrecio.SelectedValue == "MayorAMenor")
            {
                ordenamiento = true;
                seleccionDeOrdenamiento = "MayorAMenor";

                Articulos = LogicaArticulo.Listar(ordenamiento);
            }

            if (ddlOrdenXPrecio.SelectedValue == "MenorAMayor")
            {
                ordenamiento = false;
                seleccionDeOrdenamiento = "MenorAMayor";

                Articulos = LogicaArticulo.Listar(ordenamiento);
            }


            if (ddlOrdenXCategoria.SelectedValue == "A - Z")
            {
                seleccionDeOrdenamiento = "A - Z";
                Articulos = LogicaArticulo.ListarArticulosGruposCategoria(true);

            }

            if (ddlOrdenXCategoria.SelectedValue == "Z - A")
            {
                seleccionDeOrdenamiento = "Z - A";
                Articulos = LogicaArticulo.ListarArticulosGruposCategoria(false);

            }

            if (ddlOrdenXPrecio.SelectedValue == "SinOrdenar" && ddlOrdenXCategoria.SelectedValue == "SinOrdenar")
            {
                seleccionDeOrdenamiento = "SinOrdenar";
            }

            Session["SeleccionDeOrenamiento"] = seleccionDeOrdenamiento;
            Cargar1(Articulos);
        }
        else
        {
            Articulos = LogicaArticulo.ListarXCategoriaDesordenado(categoria);

            if (ddlOrdenXPrecio.SelectedValue == "MayorAMenor")
            {
                ordenamiento = true;
                seleccionDeOrdenamiento = "MayorAMenor";

                Articulos = LogicaArticulo.ListarXCategoria(categoria, ordenamiento);
            }

            if (ddlOrdenXPrecio.SelectedValue == "MenorAMayor")
            {
                ordenamiento = false;
                seleccionDeOrdenamiento = "MenorAMayor";

                Articulos = LogicaArticulo.ListarXCategoria(categoria, ordenamiento);
            }

            if (Articulos.Count == 0) //si la categoria no tiene articulos se muestra un mensaje y se vacia el gridview anterior
            {
                DataTable dtSinArticulos = new DataTable();

                gvArticulos.DataSource = dtSinArticulos;
                gvArticulos.DataBind();

                throw new ExcepcionPresentacion("La categoria no tiene ningun articulo.");
            }

            if (ddlOrdenXPrecio.SelectedValue == "SinOrdenar" && ddlOrdenXCategoria.SelectedValue == "SinOrdenar")
            {
                seleccionDeOrdenamiento = "SinOrdenar";
            }

            Session["SeleccionDeOrenamiento"] = seleccionDeOrdenamiento;
            Cargar1(Articulos);
        }
    }

    protected void CargarGridXBsqueda()
    {
        string nombreBuscado = "%" + (string)Session["NombreBuscado"] + "%";
        bool tipoBusqueda = false;

        if (ddlOrdenXPrecio.SelectedValue == "SinOrdenar")
        {
            ddlOrdenXCategoria.Enabled = false;
        }

        List<Articulo> articulos = LogicaArticulo.BuscarXNombre(nombreBuscado, tipoBusqueda);
        seleccionDeOrdenamiento = "MayorAMenor";

        if (ddlOrdenXPrecio.SelectedValue == "MenorAMayor")
        {
            seleccionDeOrdenamiento = "MenorAMayor";
            tipoBusqueda = true;
            articulos = LogicaArticulo.BuscarXNombre(nombreBuscado, tipoBusqueda);
        }

        if (articulos.Count == 0) //si la categoria no tiene articulos se muestra un mensaje y se vacia el gridview anterior
        {
            DataTable dtSinArticulos = new DataTable();

            gvArticulos.DataSource = dtSinArticulos;
            gvArticulos.DataBind();
            Panel1.Visible = true;
            lblBuscando.Text = ((string)Session["NombreBuscado"]).ToUpper();

            throw new ExcepcionPresentacion("No se encontro ningun articulo con el nombre: " + ((string)Session["NombreBuscado"]).ToUpper());
        }

        Cargar1(articulos);
        Panel1.Visible = true;
        lblBuscando.Text = ((string)Session["NombreBuscado"]).ToUpper();
        Session.Remove("CodigoArticulo");
        Session["SeleccionDeOrenamiento"] = seleccionDeOrdenamiento;
    }

    protected void Cargar1(List<Articulo> articulos)
    {
        List<Articulo> Articulos = articulos;

        DataTable dtArticulos = new DataTable();
        dtArticulos.Columns.Add(new DataColumn("CodigoBarras"));
        dtArticulos.Columns.Add(new DataColumn("Imagen"));
        dtArticulos.Columns.Add(new DataColumn("Nombre"));
        dtArticulos.Columns.Add(new DataColumn("Precio"));
        dtArticulos.Columns.Add(new DataColumn("Stock"));
        dtArticulos.Columns.Add(new DataColumn("Categoria"));

        if ((string)Session["CategoriaSeleccionada"] != ddlCategorias.SelectedValue) // control para volver a la pagina 1 de la lista de articulos si se cambia la categoria
        {
            gvArticulos.PageIndex = 0;
        }

        int cantidadListado = Convert.ToInt32(ddlCantidadResultados.SelectedValue);

        switch (cantidadListado)
        {
            case 0:
                Cargar2(cantidadListado, Articulos, dtArticulos);

                break;
            case 2:
                Cargar2(cantidadListado, Articulos, dtArticulos);

                break;
            case 4:
                Cargar2(cantidadListado, Articulos, dtArticulos);

                break;
            case 6:
                Cargar2(cantidadListado, Articulos, dtArticulos);

                break;
            case 10:
                Cargar2(cantidadListado, Articulos, dtArticulos);

                break;
        }

        Session["ArticulosXPagina"] = cantidadListado; //variable usada para establecer cuantos eran los articulos por pagina antes de entrar en la pagina de detalles
    }

    protected void Cargar2(int cantidadListado, List<Articulo> Articulos, DataTable dtArticulos)
    {
        if (cantidadListado == 0) // selecciona mostrar todos los articulos o la cantidad seleccionada
        {
            gvArticulos.PageSize = Articulos.Count;
        }
        else
        {
            gvArticulos.PageSize = cantidadListado;
        }

        try
        {
            foreach (Articulo a in Articulos)
            {
                DataRow drwArticulos = dtArticulos.NewRow();
                drwArticulos["CodigoBarras"] = a.CodigoBarras;
                drwArticulos["Imagen"] = a.Imagen;
                drwArticulos["Nombre"] = a.Nombre;
                drwArticulos["Precio"] = a.Precio;
                drwArticulos["Stock"] = a.Stock;
                CategoriaArticulo categoria = a.Categoria;
                drwArticulos["categoria"] = categoria.Nombre;

                dtArticulos.Rows.Add(drwArticulos);
            }

            gvArticulos.DataSource = dtArticulos;

            if (Session["CodigoArticulo"] != null)
            {
                gvArticulos.PageIndex = ultimaPagina;
            }

            gvArticulos.DataBind();

            Session["CategoriaSeleccionada"] = ddlCategorias.SelectedValue;

        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! Al cargar la pagina.";
        }
    }

    protected void gvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvArticulos.PageIndex = e.NewPageIndex;
        gvArticulos.DataBind();

        ultimaPagina = gvArticulos.PageIndex;   //nos posiciona en la ultima pagina visitada antes de haber partido a la pagina de detalles    
    }

    protected void gvArticulos_SelectedIndexChanged(object sender, EventArgs e) //nos direcciona a la pagina de detalles del producto seleccionado
    {
        Session["CodigoArticulo"] = Convert.ToInt64(gvArticulos.SelectedRow.Cells[0].Text);
        Session["Categoria"] = ddlCategorias.SelectedValue;
        
        Response.Redirect("~/MostrarArticulo.aspx");
    }

    protected void RegresoMostrarDetalles()
    {
        ddlCategorias.SelectedValue = (string)Session["Categoria"];
        ddlCantidadResultados.SelectedValue = Convert.ToString((int)Session["ArticulosXPagina"]);
        seleccionDeOrdenamiento = (string)Session["SeleccionDeOrenamiento"];

        switch (seleccionDeOrdenamiento)
        {
            case "MayorAMenor":
                ddlOrdenXPrecio.SelectedValue = "MayorAMenor";
                ddlOrdenXCategoria.Enabled = false;

                break;
            case "MenorAMayor":
                ddlOrdenXPrecio.SelectedValue = "MenorAMayor";
                ddlOrdenXCategoria.Enabled = false;

                break;
            case "A - Z":
                ddlOrdenXCategoria.SelectedValue = "A - Z";
                ddlOrdenXPrecio.Enabled = false;

                break;
            case "Z - A":
                ddlOrdenXCategoria.SelectedValue = "Z - A";
                ddlOrdenXPrecio.Enabled = false;

                break;
        }

        if (Session["NombreBuscado"] != null) //carga nuevamente el grid con la ultima busqueda realizada
        {
            CargarGridXBsqueda();
        }
        else //carga el grid con la ultima configuracion establecida en los filtros
        {
            CargarGridView();
        }
        
        Session.Remove("CodigoArticulo");
        Session.Remove("SeleccionDeOrenamiento");
        Session.Remove("Categoria");
    }

    protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AgregarCarrito")
        {
            try
            {
                if (Session["Usuario"] is Administrador)
                {
                    throw new ExcepcionPresentacion("Un administrador logueado no puede agregar productos al carrito de compras");
                }
                else
                {
                    List<LineaPedido> lineasDePedido = new List<LineaPedido>();

                    if ((List<LineaPedido>)Session["CarritoLineaPedido"] != null)
                    {
                        lineasDePedido = (List<LineaPedido>)Session["CarritoLineaPedido"];
                    }

                    LineaPedido nuevaLinea = new LineaPedido();

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvArticulos.Rows[index];
                    long codigoArticulo = Convert.ToInt64(row.Cells[0].Text);

                    Articulo articulo = LogicaArticulo.Buscar(codigoArticulo, false);

                    int cantidadStockCarrito = 0;

                    foreach (LineaPedido lp in lineasDePedido)
                    {
                        if (lp.PArticulo.CodigoBarras == codigoArticulo)
                        {
                            cantidadStockCarrito += lp.Cantidad;
                        }
                    }

                    if (articulo.Stock > cantidadStockCarrito)
                    {
                        nuevaLinea.Cantidad = 1;

                        nuevaLinea.Numero = lineasDePedido.Count;
                        nuevaLinea.PArticulo = articulo;

                        lineasDePedido.Add(nuevaLinea);

                        Session["CarritoLineaPedido"] = lineasDePedido;

                        lblAgregado.ForeColor = System.Drawing.Color.Green;
                        lblAgregado.Text = "Producto Agregado con exito a su carrito de compras.";
                        Panel2.Visible = true;
                    }
                    else
                    {
                        lblAgregado.ForeColor = System.Drawing.Color.Red;
                        lblAgregado.Text = "Lo sentimos pero ya no hay stock de el producto.";
                        Panel2.Visible = true;
                    }
                    //Response.Redirect("~/Default.aspx");
                }
            }
            catch (ApplicationException ex)
            {
                lblAgregado.ForeColor = System.Drawing.Color.Red;
                lblAgregado.Text = "¡Error! " + ex.Message;
                Panel2.Visible = true;
            }

            catch
            {
                lblAgregado.ForeColor = System.Drawing.Color.Red;
                lblAgregado.Text = "¡Error! Al cargar la pagina.";
                Panel2.Visible = true;
            }
            
            //Response.Redirect("~/Default.aspx");
        }

    }

    protected void imgBtnEliminarFiltro_Click(object sender, ImageClickEventArgs e)
    {
        Panel1.Visible = false;
        lblBuscando.Text = "";
        ddlCategorias.SelectedValue = "Todas";
        ddlCantidadResultados.SelectedValue = "Todos";
        ddlOrdenXCategoria.Enabled = true;
        ddlOrdenXCategoria.SelectedValue = "SinOrdenar";
        ddlOrdenXPrecio.SelectedValue = "SinOrdenar";
        Session.Remove("NombreBuscado");
        Response.Redirect("~/Default.aspx");
    }

    protected void ddlOrdenXCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOrdenXPrecio.SelectedValue = "SinOrdenar";

        if (ddlOrdenXCategoria.SelectedValue != "SinOrdenar")
        {
            ddlOrdenXPrecio.Enabled = false;
        }
        else
        {
            ddlOrdenXPrecio.Enabled = true;
        }
    }

    protected void ddlOrdenXPrecio_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOrdenXCategoria.SelectedValue = "SinOrdenar";

        if (ddlOrdenXPrecio.SelectedValue != "SinOrdenar")
        {
            ddlOrdenXCategoria.Enabled = false;
        }
        else
        {
            if (Session["NombreBuscado"] != null)
            {
                ddlOrdenXCategoria.Enabled = false;
            }
            else
            {
                ddlOrdenXCategoria.Enabled = true;
            }
        }
    }
}