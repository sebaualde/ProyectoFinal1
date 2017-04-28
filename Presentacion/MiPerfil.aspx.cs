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
using EntidadesCompartidas.Excepciones;
using EntidadesCompartidas.ObjetosNegocio;
using Logica;
using System.Collections.Generic;

public partial class MiPerfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((Usuario)Session["Usuario"] == null)
            {
                Session["Mensaje"] = "Debes iniciar sesión para ver tus pedidos.";
                Response.Redirect("Error.aspx");
            }
            else if ((Usuario)Session["Usuario"] is Administrador)
            {
                Session["Mensaje"] = "Página disponible sólo para clientes. Ingrese a la administración para ver los pedidos de un usuario.";
                Response.Redirect("Error.aspx");
            }

            if (!((Usuario)Session["Usuario"] is UsuarioRegistrado))
            {
                Session["Mensaje"] = "Página disponible sólo para clientes.";
                Response.Redirect("Error.aspx");
            }
           
            List<Pedido> pedidosdelusuario = LogicaPedido.ListarPedidosPorUsuario(((Usuario)Session["Usuario"]).Cedula);

            int pedidosEntregados = 0;
            int pedidosPendientes = 0;
            int pedidosTotales = 0;
            double costoTotal = 0;

            foreach (Pedido p in pedidosdelusuario)
            {
                pedidosTotales++;
               
                if (p.Enviado)
                {
                    pedidosEntregados++;
                    
                }
                else
                {
                    pedidosPendientes++;
                }

                costoTotal += p.PrecioTotal;
            }           

            string tarjeta = "******" + ((UsuarioRegistrado)Session["Usuario"]).NumeroTarjeta.ToString().Substring(((UsuarioRegistrado)Session["Usuario"]).NumeroTarjeta.ToString().Length - 4, 4);
            
            
            lblNombreUsuario.Text = ((Usuario)Session["Usuario"]).NombreUsuario;
            imgFotoPerfil.ImageUrl = ((Usuario)Session["Usuario"]).Imagen;
            lblNombreCompleto.Text = ((Usuario)Session["Usuario"]).NombreCompleto;
            lblCedula.Text = ((Usuario)Session["Usuario"]).Cedula.ToString();
            lblDireccion.Text = ((UsuarioRegistrado)Session["Usuario"]).DireccionEnvio;
            lblTarjeta.Text = tarjeta;
            lblTelefono.Text = ((UsuarioRegistrado)Session["Usuario"]).Telefono.ToString();
            lblCdadPedidos.Text = pedidosTotales.ToString();
            lblPedidosEntregados.Text = pedidosEntregados.ToString();
            lblPedidosPendientes.Text = pedidosPendientes.ToString();
            lblCostoTotal.Text = costoTotal.ToString();
            

        }
        catch
        {
            lblMensaje.Text = "¡Error! No se pudo desplegar los detalles del pedido.";
        }
    }
    
}
