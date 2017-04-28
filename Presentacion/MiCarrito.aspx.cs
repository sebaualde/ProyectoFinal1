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


public partial class MiCarrito : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
 
        cargarGridview();
        
    }

    protected void btnSolicitarPedido_Click(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {           
            try
            {
                List<LineaPedido> lineasDePedido = (List<LineaPedido>)Session["CarritoLineaPedido"];

                if (lineasDePedido != null)
                {
                    int numeroPedido = 1;

                    DateTime fechaPedido = DateTime.Today;

                    double precioTotal = 0;

                    foreach (LineaPedido lp in lineasDePedido)
                    {
                        precioTotal += lp.Cantidad * lp.PArticulo.Precio;
                    }

                    bool enviado = false;

                    UsuarioRegistrado registrado = (UsuarioRegistrado)Session["Usuario"];

                    Pedido pedidoUsuario = new Pedido(numeroPedido, fechaPedido, precioTotal, enviado, registrado, lineasDePedido);

                    LogicaPedido.AgregarPedido(pedidoUsuario);

                    Session.Remove("CarritoLineaPedido");

                    Panel1.Visible = true;
                    btnEliminarPedido.Visible = false;
                    btnSolicitarPedido.Visible = false;

                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Pedido generado exitosamente!.";

                    //esto es una prueba
                    //string aa = ((UserControl)this.Master.Controls[2]).ID;
                    //((Label)((UserControl)this.Master.Controls[2]).Controls[2]).Text = precioTotal;
                }
                else
                {
                    gvCarrito.Visible = false;
                    throw new ExcepcionPresentacion("Usted ya solicito este pedido.");
                }
            }
            catch (ApplicationException ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "¡Error! " + ex.Message;
            }

            catch
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "¡Error! Al generar el pedido.";
            }
        }
        else
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! Para realizar un pedido debe estar registrado, comuniquese con el administrador para registrarse.";
        }
    }

    protected void btnEliminarPedido_Click(object sender, EventArgs e)
    {
        try
        {
            if ((List<LineaPedido>)Session["CarritoLineaPedido"] != null)
            {
                Session.Remove("CarritoLineaPedido");

                Panel1.Visible = true;
                gvCarrito.Visible = false;
                btnEliminarPedido.Visible = false;
                btnSolicitarPedido.Visible = false;

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "¡Carrito vaciado con exito!";
            }
            else
            {
                Panel1.Visible = true;
                gvCarrito.Visible = false;
                btnEliminarPedido.Visible = false;
                btnSolicitarPedido.Visible = false;

                throw new ExcepcionPresentacion("El carrito ya se encuantra vacio.");
            }
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

    protected void gvCarrito_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Panel1.Visible = true;
            
            List<LineaPedido> pedidos = (List<LineaPedido>)Session["CarritoLineaPedido"];
            LineaPedido BorrarLinea = new LineaPedido();

            if (pedidos != null)
            {
                foreach (LineaPedido lp in pedidos)
                {
                    if (lp.Numero == Convert.ToInt32(gvCarrito.SelectedRow.Cells[0].Text))
                    {
                        BorrarLinea = lp;
                    }
                }

                pedidos.Remove(BorrarLinea);
                Session["CarritoLineaPedido"] = pedidos;

                cargarGridview();

                if (((List<LineaPedido>)Session["CarritoLineaPedido"]).Count == 0)
                {
                    btnEliminarPedido.Visible = false;
                    btnSolicitarPedido.Visible = false;

                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No hay más productos en su carrito de compras.";
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Articulo eliminado con exito del carrito.!";
                }
            }
            else
            { 
                gvCarrito.Visible = false;
                throw new ExcepcionPresentacion("El carrito de compras no tiene más productos.");
            }
           
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

    protected void cargarGridview()
    {
        if (Session["CarritoLineaPedido"] != null)
        {
            gvCarrito.Visible = true;

            try
            {
                if (Session["Usuario"] != null)
                {
                    Panel2.Visible = true;
                    imgFoto.ImageUrl = ((Usuario)Session["Usuario"]).Imagen;
                    lblNombreUsuario.Text = ((Usuario)Session["Usuario"]).NombreUsuario;
                }
                else
                {
                    Panel2.Visible = false;
                }

                List<LineaPedido> pedidos = (List<LineaPedido>)Session["CarritoLineaPedido"];

                DataTable dtLineaPedido = new DataTable();
                dtLineaPedido.Columns.Add(new DataColumn("Numero"));
                dtLineaPedido.Columns.Add(new DataColumn("Nombre"));
                dtLineaPedido.Columns.Add(new DataColumn("Cantidad"));
                dtLineaPedido.Columns.Add(new DataColumn("Precio"));
                dtLineaPedido.Columns.Add(new DataColumn("PrecioTotal"));

                double PrecioTotal = 0;

                foreach (LineaPedido lp in pedidos)
                {
                    DataRow drwLineaPedido = dtLineaPedido.NewRow();
                    drwLineaPedido["Numero"] = lp.Numero;
                    drwLineaPedido["Nombre"] = lp.PArticulo.Nombre;
                    drwLineaPedido["Cantidad"] = lp.Cantidad;
                    drwLineaPedido["Precio"] = lp.PArticulo.Precio;
                    drwLineaPedido["PrecioTotal"] = lp.Cantidad * lp.PArticulo.Precio;

                    PrecioTotal += lp.Cantidad * lp.PArticulo.Precio;

                    dtLineaPedido.Rows.Add(drwLineaPedido);
                }

                Panel1.Visible = true;
                lblMensaje.Text = "Precio Final = USD " + PrecioTotal;

                gvCarrito.DataSource = dtLineaPedido;
                gvCarrito.DataBind();

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
        else
        {
            Panel1.Visible = true;
            btnEliminarPedido.Visible = false;
            btnSolicitarPedido.Visible = false;
            lblMensaje.Text = "No hay nigún producto en su carrito de compras.";
        }
    }

}
