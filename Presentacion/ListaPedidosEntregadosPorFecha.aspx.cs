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
using EntidadesCompartidas.Excepciones;
using EntidadesCompartidas.ObjetosNegocio;
using System.Collections.Generic;
using System.Xml;

public partial class ListaPedidosEntregadosPorFecha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnListar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fechaInicio;

            try
            {
                fechaInicio = calInicio.FechaSeleccionada;
            }
            catch
            {
                throw new ExcepcionPresentacion("Fecha de inicio no válida");
            }

            DateTime fechaFin;

            try
            {
                fechaFin = calFin.FechaSeleccionada;
            }
            catch
            {
                throw new ExcepcionPresentacion("Fecha final no válida");
            }

            CargarGridPedidos(fechaInicio, fechaFin);

        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! No se pudo listar los pedidos.";
        }
    }

    protected void lbtnExportar_Click(object sender, EventArgs e)
    {
        try
        {
          CrearXML((DateTime)Session["FInicio"], (DateTime)Session["FFin"]);

        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! No se pudo exportar los pedidos.";
        }
    }

    protected void CargarGridPedidos(DateTime fInicio, DateTime fFinal)
    {
        DataTable dtPedidos = new DataTable();
        dtPedidos.Columns.Add(new DataColumn("Fecha"));
        dtPedidos.Columns.Add(new DataColumn("Cliente"));
        dtPedidos.Columns.Add(new DataColumn("Direccion"));
        dtPedidos.Columns.Add(new DataColumn("CantidadArticulos"));

        List<Pedido> pedidos = LogicaPedido.ListarPedidosPorFecha(fInicio, fFinal);

        foreach (Pedido p in pedidos)
        { 
            List<LineaPedido> lineasDePedido = p.LineasPedidos;

            int cantidadArticulos = 0;

            foreach (LineaPedido lp in lineasDePedido)
            {
                cantidadArticulos += lp.Cantidad;
            }

            DataRow drwPedido = dtPedidos.NewRow();
            drwPedido["Fecha"] = p.Fecha.ToShortDateString();
            drwPedido["Cliente"] = p.Registrado.NombreCompleto;
            drwPedido["Direccion"] = p.Registrado.DireccionEnvio;
            drwPedido["CantidadArticulos"] = cantidadArticulos;

            dtPedidos.Rows.Add(drwPedido);
        }

        gvPedidos.DataSource = dtPedidos;
        gvPedidos.DataBind();

        if (dtPedidos.Rows.Count > 0)
        {
            Session["FInicio"]= fInicio;
            Session["FFin"] = fFinal;

            lbtnExportar.Visible = true;
        }

    }

    protected void CrearXML(DateTime fInicio, DateTime fFinal)
    {
        string ruta = Server.MapPath("~/xml/pedidosPorFecha.xml");

        XmlDocument doc = new XmlDocument();

        doc.Load(ruta);

        XmlNode nodoRaiz = doc.DocumentElement;

        doc.DocumentElement.RemoveAll();
       
        List<Pedido> pedidos = LogicaPedido.ListarPedidosPorFecha(fInicio, fFinal);

       
     
        foreach (Pedido p in pedidos)
        {
            List<LineaPedido> lineasDePedido = p.LineasPedidos;

            int cantidadArticulos = 0;

            foreach (LineaPedido lp in lineasDePedido)
            {
                cantidadArticulos += lp.Cantidad;
            }

            XmlElement xmlpedido = doc.CreateElement("Pedido");
            
            XmlElement xmlfecha = doc.CreateElement("Fecha");
            xmlfecha.InnerText = p.Fecha.ToShortDateString();
            xmlpedido.AppendChild(xmlfecha);
            
            XmlElement xmlcliente = doc.CreateElement("Cliente");
            xmlcliente.InnerText = p.Registrado.NombreCompleto;
            xmlpedido.AppendChild(xmlcliente);

            XmlElement xmldireccion = doc.CreateElement("DireccionDeEnvio");
            xmldireccion.InnerText = p.Registrado.DireccionEnvio;
            xmlpedido.AppendChild(xmldireccion);

            XmlElement xmlcantidadarticulos = doc.CreateElement("CantidadTotalArticulos");
            xmlcantidadarticulos.InnerText = cantidadArticulos.ToString();
            xmlpedido.AppendChild(xmlcantidadarticulos);

            nodoRaiz.AppendChild(xmlpedido);
        }

         
         doc.Save(ruta);

         lblMensaje.ForeColor = System.Drawing.Color.Green;
         lblMensaje.Text = "¡Listado exportado con éxito!";
    }

    
}
