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


public partial class Administradores : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null || !(Session["Usuario"] is Administrador))
        {
            Session["Mensaje"] = "No tienes permiso para acceder a esta página.";
            Response.Redirect("~/Error.aspx");
        }

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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string nombreBuscado = txtBuscar.Text;

        if (txtBuscar.Text != string.Empty)
        {
            Session["NombreBuscado"] = nombreBuscado;
        }

        Response.Redirect("~/default.aspx");
    }
}
