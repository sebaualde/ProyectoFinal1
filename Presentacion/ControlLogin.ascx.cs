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

public partial class ControlLogin : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {

            Page.Form.DefaultButton = imgBtnLogin.UniqueID; //al presionar enter va al boton seleccionado

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

    }

    protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                throw new ExcepcionPresentacion("Debe ingresar su cédula.");
            }

            if (string.IsNullOrEmpty(txtContrasenia.Text))
            {
                throw new ExcepcionPresentacion("Debe ingresar su contraseña.");
            }

            int cedulaUsuario;

            try
            {               
                cedulaUsuario = Convert.ToInt32(txtCedula.Text);
            }
            catch
            {
                throw new ExcepcionPresentacion("Ingrese una cédula válida.");
            }

            string contrasenia = txtContrasenia.Text;

            Usuario usuario = LogicaUsuario.Buscar(cedulaUsuario, true);

           if (usuario != null && usuario.Contrasenia == contrasenia)
            {
                Session["Usuario"] = usuario;
            }
            else
            {
                throw new ExcepcionPresentacion("Cédula y/o contraseña incorrecta(s).");
            }

            Response.Redirect("Default.aspx");
        }
        catch (ApplicationException ex)
        {
            imgError.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = ex.Message;
        }
        catch
        {
            imgError.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "Se produjo un error al ingresar.";
        }
    }
}
