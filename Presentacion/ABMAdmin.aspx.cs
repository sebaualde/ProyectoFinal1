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

public partial class ABMAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtCedula.Focus();

            if (!IsPostBack)
            {
                if (!subirImagen.HasFile)
                {
                    if (RBLSexo.SelectedValue == "hombre")
                    {
                        imgAvatar.ImageUrl = "~/Imagenes/avatarAdminH.png";
                    }
                    else
                    {
                        imgAvatar.ImageUrl = "~/Imagenes/avatarAdminM.png";
                    }
                }
            }
        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al cargar la página.";

        }
    }

    protected void imgbtnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int cedula;

            try
            {
                cedula = Convert.ToInt32(txtCedula.Text);
            }
            catch
            {
                throw new ExcepcionPresentacion("Ingrese una cédula válida, sin puntos ni guiones");
            }

            string nombreCompleto = null;

            if (txtNombreCompleto.Text != String.Empty)
            {
                nombreCompleto = txtNombreCompleto.Text;
            }

            string nombreUsuario = null;

            if (txtNombreUsuario.Text != String.Empty)
            {
                nombreUsuario = txtNombreUsuario.Text;
            }

            string contrasenia = null;

            if (txtContrasenia.Text != txtConfirmacion.Text)
            {
                throw new ExcepcionPresentacion("Las contraseñas no coinciden");
            }
            
            if (txtContrasenia.Text != String.Empty)
            {
                contrasenia = txtContrasenia.Text;
            }

            string cargo = null;

            if (txtCargo.Text != String.Empty)
            {
                cargo = txtCargo.Text;
            }

            string imagen = imgAvatar.ImageUrl;

            if (subirImagen.HasFile)
            {
                imagen = "~/uploads/usuarios/" + nombreUsuario + ".jpg";
            }

            Administrador administrador = new Administrador(cedula, nombreCompleto, nombreUsuario, contrasenia, imagen, cargo);

            try
            {
                if (subirImagen.HasFile)
                {
                    string imagenLocal = subirImagen.PostedFile.FileName;

                    string extension = imagenLocal.Substring(imagenLocal.Length - 4, 4);

                    if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png" && extension.ToLower() != ".gif" && extension.ToLower() != "jpeg")
                    {
                        throw new ExcepcionPresentacion("Formato de imagen no válido");
                    }

                    string imagenServidor = Server.MapPath("~/uploads/usuarios/") + nombreUsuario + ".jpg";

                    System.IO.File.Copy(imagenLocal, imagenServidor, true);

                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch
            {
                throw new ExcepcionPresentacion("No se pudo subir el archivo.");
            }

            LogicaUsuario.Agregar(administrador);

            LimpiarFormulario();

            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.CssClass = "labelok";
            lblMensaje.Text = "Usuario Agregado con éxito";
        }

        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "¡ERROR! " + ex.Message + ".";
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al agregar el usuario.";
        }

    }

    protected void lbtnBuscar_Click1(object sender, EventArgs e)
    {
        try
        {
            int cedula;

            try
            {
                cedula = Convert.ToInt32(txtCedula.Text);
            }
            catch
            {
                throw new ExcepcionPresentacion("Cédula no válida");
            }

            Usuario usuario = LogicaUsuario.Buscar(cedula, true);

            if (usuario != null && usuario is Administrador)
            {
                txtNombreCompleto.Text = usuario.NombreCompleto;
                txtNombreUsuario.Text = usuario.NombreUsuario;
                
                imgAvatar.ImageUrl = usuario.Imagen;
                txtCargo.Text = ((Administrador)usuario).Cargo;


                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.CssClass = "labelok";
                lblMensaje.Text = "¡Usuario encontrado!";
            }
            else
            {
                LimpiarFormulario();

                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.CssClass = "labelerror";
                lblMensaje.Text = "No se encontró un administrador con la cédula " + cedula;
            }
        }

        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "¡ERROR! " + ex.Message + ".";
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al buscar el usuario.";
        }
    }

    protected void imgbtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int cedula;

            try
            {
                cedula = Convert.ToInt32(txtCedula.Text);
            }
            catch
            {
                throw new ExcepcionPresentacion("La cédula no es válida");
            }

            Usuario usuario = LogicaUsuario.Buscar(cedula, true);

            if (usuario == null || !(usuario is Administrador))
            {
                throw new ExcepcionPresentacion("No se encontró ningún administrador con la cédula " + cedula);
            }

            string nombreCompleto = txtNombreCompleto.Text;
            usuario.NombreCompleto = nombreCompleto;

            string nombreUsuario = txtNombreUsuario.Text;
            usuario.NombreUsuario = nombreUsuario;

            if (txtContrasenia.Text != txtConfirmacion.Text)
            {
                throw new ExcepcionPresentacion("Las contraseñas no coinciden");
            }

            string contrasenia = txtContrasenia.Text;
            usuario.Contrasenia = contrasenia;

            string imagen = imgAvatar.ImageUrl;

            if (subirImagen.HasFile)
            {
                imagen = "~/uploads/usuarios/" + nombreUsuario + ".jpg";
            }

            usuario.Imagen = imagen;

            string cargo = txtCargo.Text;
            ((Administrador)usuario).Cargo = cargo;

            try
            {
                if (subirImagen.HasFile)
                {
                    string imagenLocal = subirImagen.PostedFile.FileName;

                    string extension = imagenLocal.Substring(imagenLocal.Length - 4, 4);

                    if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png" && extension.ToLower() != ".gif" && extension.ToLower() != "jpeg")
                    {
                        throw new ExcepcionPresentacion("Formato de imagen no válido");
                    }

                    string imagenServidor = Server.MapPath("~/uploads/usuarios/") + nombreUsuario + ".jpg";

                    System.IO.File.Copy(imagenLocal, imagenServidor, true);

                }
            }

            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch
            {
                throw new ExcepcionPresentacion("No se pudo subir el archivo.");
            }


            LogicaUsuario.Modificar(usuario);

            LimpiarFormulario();


            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.CssClass = "labelok";
            lblMensaje.Text = "¡Usuario modificado con éxito!";

        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "¡ERROR! " + ex.Message + ".";
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al modificar el usuario.";
        }
    }

    protected void imgbtnBorrar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int cedula;

            
            try
            {
                cedula = Convert.ToInt32(txtCedula.Text);
            }
            catch
            {
                throw new ExcepcionPresentacion("La cédula no es válida");
            }

            if (cedula == ((Administrador)Session["Usuario"]).Cedula)
            {
                throw new ExcepcionPresentacion("No se puede borrar a usted mismo");
            }


            Usuario usuario = LogicaUsuario.Buscar(cedula, true);

            if (usuario != null && usuario is Administrador)
            {
                Session["UsuarioAEliminar"] = usuario;

                lblMensaje.Text = "¿Está seguro de que desea eliminar el usuario <b>" + usuario.NombreUsuario + "</b>?";

                PanelConfirmacion.Visible = true;
            }
            else
            {
                throw new ExcepcionPresentacion("No se encontró un administrador con cédula" + cedula);
            }
        }

        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "¡ERROR! " + ex.Message + ".";
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al agregar el usuario.";
        }
    }

    protected void imgbtnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            LimpiarFormulario();
        }

        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "¡ERROR! No se pudo limpiar el formulario.";
        }
    }

    private void LimpiarFormulario()
    {
        txtCedula.Text = "";
        txtNombreCompleto.Text = "";
        txtNombreUsuario.Text = "";
        txtContrasenia.Text = "";
        RBLSexo.SelectedItem.Value = "hombre";
        txtCargo.Text = "";
        imgAvatar.ImageUrl = "~/Imagenes/avatarAdminH.png";
    }

    protected void RBLSexo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBLSexo.SelectedValue == "hombre")
        {
            imgAvatar.ImageUrl = "~/Imagenes/avatarAdminH.png";
        }
        else
        {
            imgAvatar.ImageUrl = "~/Imagenes/avatarAdminM.png";
        }
    }

    protected void lbtnConfirmarEliminacion_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario usuario = (Usuario)Session["UsuarioAEliminar"];

            if (usuario != null)
            {
                LogicaUsuario.Eliminar(usuario);

                Session["UsuarioAEliminar"] = null;

                PanelConfirmacion.Visible = false;

                LimpiarFormulario();

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.CssClass = "labelok";
                lblMensaje.Text = "¡El usuario " + usuario.NombreUsuario + " se eliminó correctamente!";
            }
        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "¡ERROR! " + ex.Message;
        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al eliminar el usuario.";
        }
    }

    protected void lbtnNoEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["UsuarioAEliminar"] = null;

            PanelConfirmacion.Visible = false;

            LimpiarFormulario();

        }
        catch (ApplicationException ex)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "¡ERROR! " + ex.Message;
        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al eliminar el usuario.";
        }
    }
}
