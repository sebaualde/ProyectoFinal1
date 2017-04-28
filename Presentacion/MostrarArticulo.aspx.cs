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

public partial class MostrarArticulo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.CacheControl = "no-cache";

            if (Session["CodigoArticulo"] == null)
            {
                Session["Mensaje"] = "¡ERROR! Para acceder a los detalles de articulos debe seleccionar un link de detalles.";
                Response.Redirect("~/Error.aspx");
            }

            long codigo = (long)Session["CodigoArticulo"];
            bool buscar = false;

            Articulo articulo = LogicaArticulo.Buscar(codigo, buscar);
            CategoriaArticulo categoria = articulo.Categoria;

            if (articulo != null)
            {
                txtcodigoBarras.Text = articulo.CodigoBarras.ToString();
                lblNombre.Text = articulo.Nombre;
                txtPrecio.Text = "USD " + articulo.Precio.ToString() + "   Contado.";
                txtStock.Text = articulo.Stock.ToString();
                lblDescripcion.Text = articulo.Descripcion;
                txtCategoria.Text = articulo.Categoria.Nombre;
                imgFoto.ImageUrl = articulo.Imagen;
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se encontró el articulo con el código de barras " + codigo + ".";
            }

            Panel2.Visible = false;
        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! No se pudo mostrar el articulo.";
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["Usuario"] is Administrador)
            {
                throw new ExcepcionPresentacion("Un administrador logueado no puede agregar productos al carrito de compras");
            }
            else
            {
                List<LineaPedido> lineasDePedido = (List<LineaPedido>)Session["CarritoLineaPedido"];

                LineaPedido nuevaLinea = new LineaPedido();

                long codigo = (long)Session["CodigoArticulo"];
                bool buscar = false;

                int cantidadSolicitada;

                try
                {
                    cantidadSolicitada = Convert.ToInt32(txtCantidad.Text.Trim());
                }
                catch
                {
                    throw new ExcepcionPresentacion("La cantidad solicitada del articulo no es correcta o esta vacia.");
                }

                Articulo articulo = LogicaArticulo.Buscar(codigo, buscar);

                int cantidadStockCarrito = 0;

                if (lineasDePedido != null)
                {
                    foreach (LineaPedido lp in lineasDePedido)
                    {
                        if (lp.PArticulo.CodigoBarras == articulo.CodigoBarras)
                        {
                            cantidadStockCarrito += lp.Cantidad;
                        }
                    }
                }

                if (articulo.Stock >= cantidadSolicitada && cantidadSolicitada > 0 && articulo.Stock > cantidadStockCarrito)
                {
                    nuevaLinea.Cantidad = cantidadSolicitada;
                }
                else
                {
                    throw new ExcepcionPresentacion("La cantidad solicitada no es correcta o sobrepasa el stock (" + articulo.Stock + " ) del producto.");
                }


                nuevaLinea.PArticulo = articulo;

                if (lineasDePedido != null)
                {
                    nuevaLinea.Numero = lineasDePedido.Count;
                    lineasDePedido.Add(nuevaLinea);
                }
                else
                {

                    nuevaLinea = new LineaPedido(0, cantidadSolicitada, articulo);
                    lineasDePedido = new List<LineaPedido>();
                    lineasDePedido.Add(nuevaLinea);
                }



                Session["CarritoLineaPedido"] = lineasDePedido;

                Panel2.Visible = true;
                lblAgregado.ForeColor = System.Drawing.Color.Green;
                lblAgregado.Text = "Producto Agregado con exito a su carrito de compras.";
                lblMensaje.Text = "";
                txtCantidad.Text = "";
                //Response.Redirect("~/MostrarArticulo.aspx");
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
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! No se pudo agregar el articulo al carrito.";
        }
    }
}
