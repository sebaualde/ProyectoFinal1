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

using Logica;
using EntidadesCompartidas.Excepciones;
using EntidadesCompartidas.ObjetosNegocio;

public partial class OpcionesUsuario : System.Web.UI.UserControl
{
    static private bool _realizarPedido;

    public bool RealizarPedido
    {
        get
        {
            return _realizarPedido;
        }
    }

    public OpcionesUsuario()
    { 
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            imgUsuario.ImageUrl = ((Usuario)Session["Usuario"]).Imagen;
            lbtnNombreUsuario.Text = ((Usuario)Session["Usuario"]).NombreUsuario;

            if ((Usuario)Session["Usuario"] is UsuarioRegistrado)
            {
                lbtnNombreUsuario.Enabled = true;
                lbtnNombreUsuario.PostBackUrl = "~/MiPerfil.aspx";
                PanelAdministrador.Visible = false;
                PanelCliente.Visible = true;
                imgUsuario.Enabled = true;

                List<LineaPedido> lineasDePedido = (List<LineaPedido>)Session["CarritoLineaPedido"];

                double precioTotal = 0;

                if (lineasDePedido != null)
                {
                    foreach (LineaPedido lp in lineasDePedido)
                    {
                        precioTotal += lp.Cantidad * lp.PArticulo.Precio;
                    }
                }

                lblCarrito.Text = "USD " + Convert.ToString(precioTotal);
            }
            else if((Usuario)Session["Usuario"] is Administrador)
            {
                PanelCliente.Visible = false;
                PanelAdministrador.Visible = true;
                imgUsuario.Enabled = false;
            }
        }
    }
 
    protected void lbtnSalir_Click(object sender, EventArgs e)
    {
        Session["Usuario"] = null;
        Session.Remove("CarritoLineaPedido");
        Response.Redirect("Default.aspx");
    }

    protected void lbtnRealizarPedido_Click(object sender, EventArgs e)
    {
        try
        {
            List<LineaPedido> lineasDePedido = (List<LineaPedido>)Session["CarritoLineaPedido"];

            if (lineasDePedido != null)
            {

                int numeroPedido = (int)Application["NumeroPedido"] + 1; ;

                Application["NumeroPedido"] = numeroPedido;

                DateTime fechaPedido = DateTime.Today;

                double precioTotal = 0;

                foreach (LineaPedido lp in lineasDePedido)
                {
                    precioTotal += lp.Cantidad * lp.PArticulo.Precio;
                }

                bool enviado = false;

                UsuarioRegistrado registrado = (UsuarioRegistrado)Session["Usuario"];

                Pedido pedidoUsuario = new Pedido(numeroPedido, fechaPedido, precioTotal, enviado, registrado, lineasDePedido);

                List<Pedido> pedidosSolicitados = (List<Pedido>)Application["PedidoSolicitados"];

                pedidosSolicitados.Add(pedidoUsuario);

                Application["PedidoSolicitados"] = pedidosSolicitados;                
                Session.Remove("CarritoLineaPedido");
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "¡Pedido Realizado con Exito!";
                _realizarPedido = true;
            }
            else
            {
                _realizarPedido = false;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "El carrito esta vacio.";
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
    protected void imgUsuario_Click(object sender, ImageClickEventArgs e)
    {
        if ((Usuario)Session["Usuario"] is UsuarioRegistrado)
        {
            Response.Redirect("~/MiPerfil.aspx");
        }        

    }
}
