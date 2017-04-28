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

public partial class Ofertas : System.Web.UI.Page
{
    static int ultimaPagina;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Panel2.Visible = false;

            List<Articulo> Articulos = LogicaArticulo.Listar(false);

            DataTable dtArticulos = new DataTable();
            dtArticulos.Columns.Add(new DataColumn("CodigoBarras"));
            dtArticulos.Columns.Add(new DataColumn("Imagen"));
            dtArticulos.Columns.Add(new DataColumn("Nombre"));
            dtArticulos.Columns.Add(new DataColumn("Precio"));
            dtArticulos.Columns.Add(new DataColumn("Stock"));
            dtArticulos.Columns.Add(new DataColumn("Categoria"));

        
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

        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! Al cargar la pagina, no se encontro el repositorio de base de datos.";
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

        Response.Redirect("~/MostrarArticulo.aspx");
    }

    protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AgregarCarrito")
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

            if (articulo.Stock >= 1)
            {
                nuevaLinea.Cantidad = 1;
            }
            else
            {
                lblAgregado.ForeColor = System.Drawing.Color.Red;
                lblAgregado.Text = "Lo sentimos pero ya no hay stock de el producto.";
                Panel2.Visible = true;
            }

            nuevaLinea.Numero = lineasDePedido.Count;
            nuevaLinea.PArticulo = articulo;

            lineasDePedido.Add(nuevaLinea);

            Session["CarritoLineaPedido"] = lineasDePedido;

            lblAgregado.ForeColor = System.Drawing.Color.Green;
            lblAgregado.Text = "Producto Agregado con exito a su carrito de compras.";
            Panel2.Visible = true;
            //Response.Redirect("~/Ofertas.aspx");
        }

    }
}
