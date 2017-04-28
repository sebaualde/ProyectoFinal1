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

public partial class DetallesDelPedido : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";

        if (Session["NumeroPedido"] == null && Session["PedidoCarrito"] == null)
        {
            Session["Mensaje"] = "¡ERROR! Para acceder a los detalles del pedido debe seleccionar un link de detalles.";
            Response.Redirect("~/Error.aspx");
        }      

        try
        {
            Pedido pedido = LogicaPedido.BuscarPedido((int)Session["NumeroPedido"]);

            btnEnviarPedido.Visible = true;

            if (pedido.Enviado)
            {
                btnEnviarPedido.Visible = false;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "El pedido ya fue enviado al cliente.";
            }
            else
            {
                btnEnviarPedido.Visible = true;
            }

            if (pedido.Numero == 0)
            {
                List<Pedido> pedidosSolicitados = (List<Pedido>)Application["PedidoSolicitados"];

                foreach (Pedido p in pedidosSolicitados)
                {
                    if (p.Numero == (int)Session["NumeroPedido"])
                    {
                        pedido = p;
                    }
                }

                btnEnviarPedido.Visible = false;  
            }

            imgFoto.ImageUrl = pedido.Registrado.Imagen;
            lblNombreUsuario.Text = pedido.Registrado.NombreCompleto;
            lblCodigoPedido.Text = "|| Pedido número: " + pedido.Numero + " ||     Solicitado el día : " + pedido.Fecha.ToShortDateString() + "  ||";

            List<LineaPedido> pedidos = pedido.LineasPedidos;

            DataTable dtLineaPedido = new DataTable();
            dtLineaPedido.Columns.Add(new DataColumn("Nombre"));
            dtLineaPedido.Columns.Add(new DataColumn("Cantidad"));
            dtLineaPedido.Columns.Add(new DataColumn("Precio"));
            dtLineaPedido.Columns.Add(new DataColumn("PrecioTotal"));

            double PrecioTotal = 0;

            foreach (LineaPedido lp in pedidos)
            {
                DataRow drwLineaPedido = dtLineaPedido.NewRow();
                drwLineaPedido["Nombre"] = lp.PArticulo.Nombre;
                drwLineaPedido["Cantidad"] = lp.Cantidad;
                drwLineaPedido["Precio"] = lp.PArticulo.Precio;
                drwLineaPedido["PrecioTotal"] = lp.Cantidad * lp.PArticulo.Precio;

                PrecioTotal += lp.Cantidad * lp.PArticulo.Precio;

                dtLineaPedido.Rows.Add(drwLineaPedido);
            }

            lblPrecioTotal.Text = "Precio Final = USD " + PrecioTotal;

            gvDetallePedido.DataSource = dtLineaPedido;
            gvDetallePedido.DataBind();

            AparecerDesaparecerBotones(pedido);

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

    protected void gvDetallePedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetallePedido.PageIndex = e.NewPageIndex;
        gvDetallePedido.DataBind();
    }

    protected void lbtnVolver_Click(object sender, EventArgs e)
    {
        if ((bool)Session["PedidosGeneral"])
        {
            Response.Redirect("~/ListadoPedidosGeneral.aspx");
        }
        else
        {
            Response.Redirect("~/ListadoPedidosPendientes.aspx");
        }
    }
    

    protected void btnEnviarPedido_Click(object sender, EventArgs e)
    {
        try
        {
            LogicaPedido.ModificarEnvioPedido((int)Session["NumeroPedido"], true);

            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = "¡Pedido enviado exitosamente al cliente.!";

            btnEnviarPedido.Visible = false;
        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! Al enviar el pedido.";
        }
    }

    static bool seleccion; 

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = "¿Esta seguro que desea eliminar este pedido?";

        seleccion = true;

        btnEliminar.Enabled = false;
        btnAceptar.Visible = true;
        btnCancelar.Visible = true;   
    }

    protected void btnDuplicar_Click(object sender, EventArgs e)
    {
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = "¿Esta seguro que desea duplicar este pedido?";

        seleccion = false;

        btnDuplicar.Enabled = false;
        btnAceptar.Visible = true;
        btnCancelar.Visible = true;

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (seleccion)
        {
            try
            {
                Pedido pedido = LogicaPedido.BuscarPedido((int)Session["NumeroPedido"]);

                LogicaPedido.EliminarPedido(pedido);

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "¡Pedido eliminado exitosamente.!";

                DesactivarBotones();
                btnEliminar.Visible = false;

            }
            catch (ApplicationException ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "¡Error! " + ex.Message;
            }

            catch
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "¡Error! Al eliminar el pedido.";
            }
        }
        else
        {
            try
            {
                Pedido pedido = LogicaPedido.BuscarPedido((int)Session["NumeroPedido"]);

                bool noDuplicar = false;
                string articuloEliminado = "";

                foreach (LineaPedido lp in pedido.LineasPedidos)
                {

                    if (lp.PArticulo.Eliminado == true)
                    {
                        noDuplicar = true;
                        articuloEliminado += lp.PArticulo.Nombre;
                    }
                }

                if (noDuplicar)
                {
                    btnDuplicar.Visible = false;
                    btnAceptar.Visible = false;
                    btnCancelar.Visible = false;
                    throw new ExcepcionPresentacion("No se puede duplicar el pedido porque " + articuloEliminado + " ya no esta/n disponible/s.");
                }
                else
                {
                    pedido.Enviado = false; //duplica pero no lo envia

                    LogicaPedido.AgregarPedido(pedido);

                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Pedido duplicado exitosamente.!";

                    DesactivarBotones();
                    btnDuplicar.Visible = false;
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
                lblMensaje.Text = "¡Error! Al duplicar el pedido.";
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (seleccion)
        {
            btnEliminar.Enabled = true;
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            lblMensaje.Text = "";
        }
        else
        {
            btnDuplicar.Enabled = true;
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            lblMensaje.Text = "";
        }
    }

    protected void AparecerDesaparecerBotones(Pedido pedido)
    {
        if ((Usuario)Session["Usuario"] is Administrador)
        {
            btnDuplicar.Visible = false;
            btnEliminar.Visible = false;
        }
        else if ((Usuario)Session["Usuario"] is UsuarioRegistrado)
        {
            if (pedido.Enviado == false)
            {
                btnDuplicar.Visible = false;
                btnEliminar.Visible = true;
            }
            else
            {
                btnDuplicar.Visible = true;
                btnEliminar.Visible = false;
            }

            btnEnviarPedido.Visible = false;
        }
    }

    protected void DesactivarBotones()
    {
        btnAceptar.Visible = false;
        btnCancelar.Visible = false;
    }
}
