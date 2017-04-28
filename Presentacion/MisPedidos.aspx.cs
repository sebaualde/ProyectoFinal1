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
using Logica;
using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;
using System.Collections.Generic;

public partial class MisPedidos : System.Web.UI.Page
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
            else
            {
                CargarGridPedidos();
            }
           

        }
        catch
        {
            Panel1.Visible = false;
            lblMensaje.Text = "¡Error! No se pudo desplegar los detalles del pedido.";
        }
    }

    protected void CargarGridPedidos()
    {
        DataTable dtPedidos = new DataTable();
        dtPedidos.Columns.Add(new DataColumn("Numero"));
        dtPedidos.Columns.Add(new DataColumn("Fecha"));
        dtPedidos.Columns.Add(new DataColumn("PrecioTotal"));
        dtPedidos.Columns.Add(new DataColumn("Enviado"));
        dtPedidos.Columns.Add(new DataColumn("AltEnviado"));
        
        List<Pedido> pedidos = LogicaPedido.ListarPedidosPorUsuario(((Usuario)Session["Usuario"]).Cedula); //lista enviados y pendientes en base de datos

        foreach (Pedido p in pedidos)
        {
            DataRow drwPedido = dtPedidos.NewRow();
            drwPedido["Numero"] = p.Numero;
            drwPedido["Fecha"] = p.Fecha;
            drwPedido["PrecioTotal"] = p.PrecioTotal;
            drwPedido["Enviado"] = p.Enviado ? "~/Imagenes/iconos/tick.png" : "~/Imagenes/iconos/clock.png";
            drwPedido["AltEnviado"] = p.Enviado ? "Enviado" : "Pendiente";

            dtPedidos.Rows.Add(drwPedido);
        }

        if (dtPedidos.Rows.Count == 0)
        {
            lblMensaje.Text = "No hay pedidos para mostrar.";
            Panel1.Visible = false;
        }

        gvMisPedidos.DataSource = dtPedidos;
        gvMisPedidos.DataBind();
    }

    protected void gvMisPedidos_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            Session["NumeroPedido"] = Convert.ToInt32(gvMisPedidos.SelectedRow.Cells[0].Text);
            Session["PedidosGeneral"] = true;

            Response.Redirect("~/MostrarDetallesPedido.aspx");
        }
        catch
        {
            Panel1.Visible = false;
            lblMensaje.Text = "¡Error! No se pudo desplegar los detalles del pedido.";
        }
    }
}