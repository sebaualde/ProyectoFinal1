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

public partial class ListadoPedidosGeneral : System.Web.UI.Page
{
    static int ultimaPagina;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.CacheControl = "no-cache";
            lblMensaje.Text = "";

            List<Pedido> Pedidos;

            if (Session["BuscadoPorCedula"] != null)
            {
                Pedidos = LogicaPedido.ListarPedidosPorUsuario((int)Session["BuscadoPorCedula"]);

                Session.Remove("BuscadoPorCedula");
            }
            else
            {
                string tipoListado = ddlEstadoPedido.SelectedValue;

                switch (tipoListado)
                {
                    case "Todos":

                        Pedidos = LogicaPedido.ListarTodosLosPedidos();
                        CargarGridView(Pedidos);

                        break;
                    case "Pendientes":

                        Pedidos = LogicaPedido.ListarPedidosPendientes();
                        CargarGridView(Pedidos);

                        break;
                    case "Enviados":

                        Pedidos = LogicaPedido.ListarPedidosEnviados();
                        CargarGridView(Pedidos);

                        break;
                }
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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            int cedula;

            try
            {
                cedula = Convert.ToInt32(txtCedula.Text.Trim());
            }
            catch
            {
                throw new ExcepcionPresentacion("Ingrese una cédula válida, sin puntos ni guiones para realizar la busqueda.");
            }

            List<Pedido> Pedidos = LogicaPedido.ListarPedidosPorUsuario(cedula);

            Session["BuscadoPorCedula"] = cedula;

            CargarGridView(Pedidos);

            txtCedula.Text = "";
          
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
    
    protected void CargarGridView(List<Pedido> Pedidos)
    {
        DataTable dtPedidos = new DataTable();
        dtPedidos.Columns.Add(new DataColumn("Cedula"));
        dtPedidos.Columns.Add(new DataColumn("Imagen"));
        dtPedidos.Columns.Add(new DataColumn("Numero"));
        dtPedidos.Columns.Add(new DataColumn("NombreCompleto"));
        dtPedidos.Columns.Add(new DataColumn("Generado"));
        dtPedidos.Columns.Add(new DataColumn("Enviado"));

        foreach (Pedido p in Pedidos)
        {
            DataRow drwPedidos = dtPedidos.NewRow();
            drwPedidos["Cedula"] = p.Registrado.Cedula;
            drwPedidos["Imagen"] = p.Registrado.Imagen;
            drwPedidos["Numero"] = p.Numero;
            drwPedidos["NombreCompleto"] = p.Registrado.NombreCompleto;
            drwPedidos["Generado"] = "Si";
            drwPedidos["Enviado"] = p.Enviado ? "Si" : "No";

            dtPedidos.Rows.Add(drwPedidos);
        }

        if (dtPedidos.Rows.Count == 0)
        {
            throw new ExcepcionPresentacion("No se encontro ningún pedido para la cedula: " + txtCedula.Text);
        }

        gvPedidos.DataSource = dtPedidos;

        if (Session["NumeroPedido"] != null)
        {
            gvPedidos.PageIndex = ultimaPagina;
        }

        gvPedidos.DataBind();

    }

    
    protected void gvPedidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["NumeroPedido"] = Convert.ToInt32(gvPedidos.SelectedRow.Cells[3].Text);
        Session["PedidosGeneral"] = true;

        Response.Redirect("~/MostrarDetallesPedido.aspx");
    }

    protected void gvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPedidos.PageIndex = e.NewPageIndex;
        gvPedidos.DataBind();
        
        ultimaPagina = gvPedidos.PageIndex;   //nos posiciona en la ultima pagina visitada antes de haber partido a la pagina de detalles    
    }
}
