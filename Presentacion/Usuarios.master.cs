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


public partial class Usuarios : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblFecha.Text = DateTime.Today.ToLongDateString();
        if (Session["Usuario"] == null)
        {
            ControlLogin1.Visible = true;
            OpcionesUsuario1.Visible = false;
        }
        else
        {
            ControlLogin1.Visible = false;
            OpcionesUsuario1.Visible = true;
        }
    }

    protected void imgBtnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string nombreBuscado = txtBuscar.Text;

        if (txtBuscar.Text != string.Empty)
        {
            Session["NombreBuscado"] = nombreBuscado;
            Session["CategoriaSeleccionada"] = "0";
            Session["CategoriaSeleccionada"] = "Todas";
        }

        Response.Redirect("~/default.aspx");
    }
}
