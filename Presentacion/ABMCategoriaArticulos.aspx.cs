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

public partial class ABMCategoriaArticulos : System.Web.UI.Page
{
    static string opcion = ""; //variable para definir el comportamiento del boton Aceptar segun si se usa para Eliminar o modificar 
    static bool buscar = false; //variable definida para hacer la busqueda e identificar el estado de eliminado de la categoria

    protected void Page_Load(object sender, EventArgs e)
    {
        txtNombre.Focus();

        if (Session["Usuario"] == null || !(Session["Usuario"] is Administrador))
        {
            Session["Mensaje"] = "No tienes permiso para acceder a esta página.";
            Response.Redirect("~/Error.aspx");
        }

    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNombre.Text.Trim() == string.Empty)
            {
                throw new ExcepcionPresentacion("El nombre de la categoria a buscar no puede quedar vacío.");
            }

            string nombre = txtNombre.Text.Trim();

            buscar = false; // bandera para buscar categorias NO eliminadas logicamente en la base de datos 

            CategoriaArticulo categoria = LogicaCategoriaArticulo.Buscar(nombre, buscar);

            if (categoria != null)
            {
                txtNombre.Text = categoria.Nombre;
                txtDescripcion.Text = categoria.Descripcion;

                AparecerDesaparecerBotonesBuscarNoNULL();

                Panel1.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "¡Categoria Encontrada!";
            }
            else
            {
                AparecerDesaparecerBotonesBuscarEsNULL();

                Panel1.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se encontró ningúna categoria con el nombre: '" + nombre + "', si desea puede Agregarla.";
            }
        }
        catch (ApplicationException ex)
        {
            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡ERROR! " + ex.Message;
        }
        catch
        {
            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡ERROR! Se produjo un error al buscar la categoria.";
        }
    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                throw new ExcepcionLogica("El Nombre de la categoria no puede quedar vacío.");
            }

            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(descripcion))
            {
                throw new ExcepcionLogica("La descripción de la categoria no puede quedar vacía.");
            }

            bool eliminado = false;

            CategoriaArticulo categoria = new CategoriaArticulo(nombre, descripcion, eliminado);

            buscar = true; //bandera para buscar la categoria en caso de que la misma este eliminada (true)  
            //de forma logica en la base de datos y poder reemplazarla o agregarla de manera normal

            CategoriaArticulo buscarCategoria = LogicaCategoriaArticulo.Buscar(nombre, buscar);

            if (buscarCategoria == null)
            {
                LogicaCategoriaArticulo.Agregar(categoria);
            }
            else
            {
                LogicaCategoriaArticulo.Modificar(categoria);
            }

            LimpiarFormulario();
            txtDescripcion.Enabled = false;

            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = "¡Categoria agregada con éxito!";
        }
        catch (ApplicationException ex)
        {
            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }

        catch
        {
            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! No se pudo agregar la categoria.";
        }
    }


    protected void btnModificar_Click(object sender, EventArgs e)
    {
        AparecerDesaparecerBotonesEliminarModificar();

        txtDescripcion.Enabled = true;

        opcion = "Modificar";

        Panel1.Visible = true;
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = "¿Esta seguro que desea modificar esta categoria?.";

    }


    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        AparecerDesaparecerBotonesEliminarModificar();

        opcion = "Eliminar";

        Panel1.Visible = true;
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = "¿Esta seguro que desea Eliminar esta categoria?.";

    }


    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        switch (opcion)
        {
            case "Modificar":
                try
                {
                    string nombre = txtNombre.Text.Trim();

                    string descripcion = txtDescripcion.Text.Trim();

                    bool eliminado = false;

                    CategoriaArticulo categoria = new CategoriaArticulo(nombre, descripcion, eliminado);

                    LogicaCategoriaArticulo.Modificar(categoria);

                    AparecerDesaparecerBotonesClickAceptar();

                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Categoria modificada con éxito!";
                }

                catch (ApplicationException ex)
                {
                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "¡Error! " + ex.Message;
                }

                catch
                {
                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "¡Error! No se pudo modificar la categoria.";
                }
                break;

            case "Eliminar":
                try
                {
                    string nombre = txtNombre.Text.Trim();

                    LogicaCategoriaArticulo.Eliminar(nombre);

                    AparecerDesaparecerBotonesClickAceptar();

                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Categoria eliminada con éxito!";

                }
                catch (ApplicationException ex)
                {
                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "¡Error! " + ex.Message;
                }

                catch
                {
                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "¡Error! No se pudo eliminar la categoria.";
                }
                break;
        }
    }

    //----------------------------MANTENIMIENTO DE BOTONES Y LIMPIEZA DE FORMULARIO----------------------------------

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        btnAceptar.Visible = false;
        btnSalir.Visible = false;
        lblMensaje.Text = "";

        btnEliminar.Enabled = true;
        btnModificar.Enabled = true;
        txtDescripcion.Enabled = false;

        Panel1.Visible = false;
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            LimpiarFormulario();
        }

        catch (ApplicationException ex)
        {
            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error! " + ex.Message;
        }

        catch
        {
            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "¡Error al intentar limpiar el formulario!";
        }
    }


    protected void LimpiarFormulario()
    {
        txtNombre.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        txtNombre.Enabled = true;

        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;

        btnAceptar.Visible = false;
        btnSalir.Visible = false;

        Panel1.Visible = false;

        lblMensaje.Text = "";
    }

    protected void AparecerDesaparecerBotonesBuscarNoNULL()
    {
        txtNombre.Enabled = false;

        btnEliminar.Enabled = true;
        btnModificar.Enabled = true;
    }

    protected void AparecerDesaparecerBotonesBuscarEsNULL()
    {
        btnAgregar.Enabled = true;

        txtDescripcion.Enabled = true;
        txtNombre.Enabled = false;

        txtDescripcion.Focus();
    }

    protected void AparecerDesaparecerBotonesEliminarModificar()
    {
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;

        btnAceptar.Visible = true;
        btnSalir.Visible = true;
    }

    protected void AparecerDesaparecerBotonesClickAceptar()
    {
        btnAceptar.Visible = false;
        btnSalir.Visible = false;

        txtNombre.Text = "";
        txtNombre.Enabled = true;

        txtDescripcion.Text = "";
        txtDescripcion.Enabled = false;
    }

}
