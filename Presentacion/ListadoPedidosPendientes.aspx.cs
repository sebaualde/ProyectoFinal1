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

public partial class ListadoPedidosPendientes : System.Web.UI.Page
{
    static int ultimaPagina;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null || !(Session["Usuario"] is Administrador))
        {
            Session["Mensaje"] = "No tienes permiso para acceder a esta página.";
            Response.Redirect("~/Error.aspx");
        }

        try
        {
            Response.CacheControl = "no-cache";

            List<Pedido> Pedidos = LogicaPedido.ListarPedidosPendientes();

            DataTable dtPedidosPendientes = new DataTable();
            dtPedidosPendientes.Columns.Add(new DataColumn("Imagen"));
            dtPedidosPendientes.Columns.Add(new DataColumn("Numero"));
            dtPedidosPendientes.Columns.Add(new DataColumn("NombreCompleto"));
            dtPedidosPendientes.Columns.Add(new DataColumn("Generado"));
            dtPedidosPendientes.Columns.Add(new DataColumn("Enviado"));

            foreach (Pedido p in Pedidos)
            {
                DataRow drwPedidos = dtPedidosPendientes.NewRow();
                drwPedidos["Imagen"] = p.Registrado.Imagen;
                drwPedidos["Numero"] = p.Numero;
                drwPedidos["NombreCompleto"] = p.Registrado.NombreCompleto;
                drwPedidos["Generado"] = "Si";
                drwPedidos["Enviado"] = p.Enviado ? "Si" : "No";

                dtPedidosPendientes.Rows.Add(drwPedidos);
            }

            gvPedidosPendientes.DataSource = dtPedidosPendientes;

            if (Session["NumeroPedido"] != null)
            {
                gvPedidosPendientes.PageIndex = ultimaPagina;
            }

            gvPedidosPendientes.DataBind();

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

    protected void gvPedidosPendientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["NumeroPedido"] = Convert.ToInt32(gvPedidosPendientes.SelectedRow.Cells[2].Text);
            Session["PedidosGeneral"] = false; //variable para definir el comportamiento del boton volver en la pagina de detalles

            Response.Redirect("~/MostrarDetallesPedido.aspx");
        }
        catch
        {
            lblMensaje.Text = "¡Error! No se pudo desplegar los detalles del pedido.";
        }
    }

    protected void gvPedidosPendientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPedidosPendientes.PageIndex = e.NewPageIndex;
        gvPedidosPendientes.DataBind();

        ultimaPagina = gvPedidosPendientes.PageIndex;   //nos posiciona en la ultima pagina visitada antes de haber partido a la pagina de detalles    
    }
}
