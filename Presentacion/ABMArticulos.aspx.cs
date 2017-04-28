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

public partial class ABMArticulos : System.Web.UI.Page
{
    static string opcion = ""; //variable para definir el comportamiento del boton Aceptar segun si se usa para Eliminar o modificar 
    bool buscar = false; // variable definida para determinar si el articulo que se quiere agregar esta eliminado de forma logica en la base de datos y poder reemplazarlo

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null || !(Session["Usuario"] is Administrador))
        {
            Session["Mensaje"] = "No tienes permiso para acceder a esta página.";
            Response.Redirect("~/Error.aspx");
        }

        txtCodigoBarras.Focus();

        try
        {
            if (!IsPostBack)
            {
                imgImagen.ImageUrl = "~/Imagenes/ImagenDefault.png";

                List<CategoriaArticulo> Categorias = LogicaCategoriaArticulo.Listar();

                if (Categorias.Count > 0)
                {
                    ddlCategorias.Items.Add(new ListItem("Seleccione Una Categoria", "Ninguno"));

                    foreach (CategoriaArticulo c in Categorias)
                    {
                        ddlCategorias.Items.Add(new ListItem(c.Nombre, c.Nombre));
                    }
                }
                else
                {
                    Session["Mensaje"] = "¡ERROR! No hay Categorias ingresadas.";
                    Response.Redirect("~/Error.aspx");
                }
            }
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
            lblMensaje.Text = "¡Error! Al cargar la pagina.";
        }
    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            long codigoBarras;

            try
            {
                codigoBarras = Convert.ToInt64(txtCodigoBarras.Text.Trim());
            }
            catch
            {
                throw new ExcepcionPresentacion("El Código de barras no es válido.");
            }

            buscar = false;//busca solo los articulos no eliminados de la base de datos

            Articulo articulo = LogicaArticulo.Buscar(codigoBarras, buscar);

            if (articulo != null)
            {
                txtNombre.Text = articulo.Nombre;
                txtPrecio.Text = articulo.Precio.ToString();
                txtStock.Text = articulo.Stock.ToString();
                txtDescripcion.Text = articulo.Descripcion;
                imgImagen.ImageUrl = articulo.Imagen;
                ddlCategorias.SelectedValue = articulo.Categoria.Nombre;

                AparecerDesaparecerBotonesBuscarNoNULL();

                Panel1.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "¡Articulo Encontrado!";

            }
            else
            {
                AparecerDesaparecerBotonesBuscarNULL();

                Panel1.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se encontró ningún Articulo con el Código de Barras " + codigoBarras + ", si lo desea puede agregar un articulo con ese codigo.";
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
            lblMensaje.Text = "¡ERROR! Se produjo un error al buscar el Articulo.";
        }

    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            long codigoBarras = 1;

            if (txtCodigoBarras.Text.Trim() != string.Empty)
            {
                codigoBarras = Convert.ToInt64(txtCodigoBarras.Text.Trim());
            }

            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                throw new ExcepcionLogica("El Nombre del articulo no puede quedar vacío.");
            }

            double precio;

            try
            {
                precio = Convert.ToDouble(txtPrecio.Text.Trim());
            }
            catch
            {
                throw new ExcepcionPresentacion("El Precio no es válido.");
            }

            int stock;

            try
            {
                stock = Convert.ToInt32(txtStock.Text.Trim());
            }
            catch
            {
                throw new ExcepcionPresentacion("El Stock no es válido.");
            }

            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(descripcion))
            {
                throw new ExcepcionLogica("La descripcion del articulo no puede quedar vacía.");
            }

            string nombreCategoria = ddlCategorias.SelectedValue;

            if (nombreCategoria == "Ninguno")
            {
                throw new ExcepcionPresentacion("Debe seleccionar una categoria de la lista.");
            }

            bool buscarCategoria = false; //busca categorias no eliminadas de manera logica
            CategoriaArticulo categoria = LogicaCategoriaArticulo.Buscar(nombreCategoria, buscarCategoria);

            string origenImagen = fuOrigenImagen.PostedFile.FileName; //saca la ruta de donde se copia la imagen del Fileupload

            string destinoImagen;

            if (origenImagen != string.Empty)
            {
                string nombreImagen = txtCodigoBarras.Text.Trim() + System.IO.Path.GetFileName(origenImagen);//sacamos el codigo del articulo y se lo agregamos al nombre de la imagen

                string extension = nombreImagen.Substring(nombreImagen.Length - 4, 4); 

                if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png" && extension.ToLower() != ".bmp" && extension.ToLower() != "jpeg")
                {
                    throw new ExcepcionPresentacion("Formato de imagen no válido, coloque un archivo con extension: .jpg , .png , .bmp  o .jpeg ");
                }

                destinoImagen = Server.MapPath("~/uploads/articulos/") + nombreImagen; // ruta de destino donde se copiara la imagen

                System.IO.File.Copy(origenImagen, destinoImagen, true);//copia la imagen y reemplaza si existe una con el mismo nombre

                destinoImagen = "~/uploads/articulos/" + nombreImagen; //generamos una ruta corta para la base de datos
            }
            else
            {
                throw new ExcepcionPresentacion("Debe seleccionar una imagen para el Articulo.");
            }

            bool eliminado = false;

            Articulo articulo = new Articulo(codigoBarras, nombre, precio, stock, descripcion, destinoImagen, categoria, eliminado);

            buscar = true; // busca los articulos aunque este eliminados de manera logica
            Articulo buscarArticulo = LogicaArticulo.Buscar(codigoBarras, buscar);

            if (buscarArticulo == null)
            {
                LogicaArticulo.Agregar(articulo);
            }
            else
            {
                LogicaArticulo.Modificar(articulo);
            }

            LimpiarFormulario();

            Panel1.Visible = true;
            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = "¡Articulo" + (stock > 1 ? "s" : "") + " agregado" + (stock > 1 ? "s" : "") + " con éxito! con el código de barras: " + codigoBarras;
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
            lblMensaje.Text = "¡Error! No se pudo agregar el Articulo.";
        }
    }


    protected void btnModificar_Click(object sender, EventArgs e)
    {
        AparecerDesaparecerBotonesModificar();

        opcion = "Modificar";

        Panel1.Visible = true;
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = "Modifique el Articulo y oprima Aceptar para guardar los cambios o Salir para Cancelar.";
    }



    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        AparecerDesaparecerBotonesEliminar();

        opcion = "Eliminar";

        Panel1.Visible = true;
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = "¿Esta seguro que desea Eliminar este Articulo?.";
    }


    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        switch (opcion)
        {
            case "Modificar":
                try
                {
                    long codigoBarras = Convert.ToInt64(txtCodigoBarras.Text.Trim());

                    string nombre = txtNombre.Text.Trim();

                    if (string.IsNullOrEmpty(nombre))
                    {
                        throw new ExcepcionLogica("El Nombre del articulo no puede quedar vacío.");
                    }

                    double precio;

                    try
                    {
                        precio = Convert.ToDouble(txtPrecio.Text.Trim());
                    }
                    catch
                    {
                        throw new ExcepcionPresentacion("El Precio no es válido.");
                    }

                    int stock;

                    try
                    {
                        stock = Convert.ToInt32(txtStock.Text.Trim());
                    }
                    catch
                    {
                        throw new ExcepcionPresentacion("El Stock no es válido.");
                    }

                    string descripcion = txtDescripcion.Text.Trim();

                    if (string.IsNullOrEmpty(descripcion))
                    {
                        throw new ExcepcionLogica("La descripcion del articulo no puede quedar vacía.");
                    }

                    string nombreCategoria = ddlCategorias.SelectedValue;

                    if (nombreCategoria == "Ninguno")
                    {
                        throw new ExcepcionPresentacion("Debe seleccionar una categoria de la lista.");
                    }

                    bool buscarCategoria = false;
                    CategoriaArticulo categoria = LogicaCategoriaArticulo.Buscar(nombreCategoria, buscarCategoria);

                    buscar = false;
                    Articulo articuloImagen = LogicaArticulo.Buscar(codigoBarras, buscar);

                    string origenImagen = fuOrigenImagen.PostedFile.FileName;
                    string destinoImagen = articuloImagen.Imagen; //si no se proporciona una direccion de imagen queda la que tenia anteriormente

                    if (origenImagen != string.Empty)
                    {
                        string eliminarImagen = Server.MapPath(articuloImagen.Imagen);

                        if (System.IO.File.Exists(eliminarImagen) == true)
                        {
                            System.IO.File.Delete(eliminarImagen);
                        }

                        string nombreImagen = txtCodigoBarras.Text.Trim() + System.IO.Path.GetFileName(origenImagen);
                        destinoImagen = Server.MapPath("~/uploads/articulos/") + nombreImagen;

                        System.IO.File.Copy(origenImagen, destinoImagen, true);

                        destinoImagen = "~/uploads/articulos/" + nombreImagen;
                    }

                    bool eliminado = false;

                    Articulo articulo = new Articulo(codigoBarras, nombre, precio, stock, descripcion, destinoImagen, categoria, eliminado);

                    LogicaArticulo.Modificar(articulo);

                    AparecerDesaparecerBotonesAceptar();

                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Articulo modificado con éxito!";
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
                    lblMensaje.Text = "¡Error! No se pudo modificar el Articulo.";
                }
                break;

            case "Eliminar":
                try
                {
                    long codigoBarras = Convert.ToInt64(txtCodigoBarras.Text.Trim());

                    string nombre = txtNombre.Text.Trim();

                    bool buscarArticulo = false;
                    Articulo articuloImagen = LogicaArticulo.Buscar(codigoBarras, buscarArticulo);

                    LogicaArticulo.Eliminar(codigoBarras);

                    string eliminarImagen = Server.MapPath(articuloImagen.Imagen);

                    if (System.IO.File.Exists(eliminarImagen) == true)
                    {
                        System.IO.File.Delete(eliminarImagen);
                    }

                    AparecerDesaparecerBotonesAceptar();

                    Panel1.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Articulo eliminado con éxito!";

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
                    lblMensaje.Text = "¡Error! No se pudo eliminar el Articulo.";
                }
                break;
        }
    }

    //--------------------------MANTENIMIENTO DE BOTONES Y LIMPIEZA DE FORMULARIO------------------------------------------------------------


    protected void btnSalir_Click(object sender, EventArgs e)
    {
        btnAceptar.Visible = false;
        btnSalir.Visible = false;
        lblMensaje.Text = "";

        btnEliminar.Enabled = true;
        btnModificar.Enabled = true;
        Panel1.Visible = false;
        controlesEnFalse();
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
        AparecerDesaparecerBotonesAceptar();
        Panel1.Visible = false;
        lblMensaje.Text = "";
    }

    protected void AparecerDesaparecerBotonesBuscarNULL()
    {
        txtNombre.Focus();
        controlesEnTrue();

        txtCodigoBarras.Enabled = false;
        btnAgregar.Enabled = true;
    }


    protected void AparecerDesaparecerBotonesBuscarNoNULL()
    {
        txtCodigoBarras.Enabled = false;

        btnModificar.Enabled = true;
        btnEliminar.Enabled = true;
    }

    protected void AparecerDesaparecerBotonesModificar()
    {
        controlesEnTrue();

        AparecerDesaparecerBotonesEliminar();
    }

    protected void AparecerDesaparecerBotonesEliminar()
    {
        btnAceptar.Visible = true;
        btnSalir.Visible = true;

        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;

    }

    protected void AparecerDesaparecerBotonesAceptar()
    {
        txtCodigoBarras.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtPrecio.Text = string.Empty;
        txtStock.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        ddlCategorias.SelectedValue = "Ninguno";
        imgImagen.Visible = true;
        imgImagen.ImageUrl = "~/Imagenes/ImagenDefault.png";

        txtCodigoBarras.Enabled = true;

        controlesEnFalse();

        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;

        btnAceptar.Visible = false;
        btnSalir.Visible = false;
    }

    protected void controlesEnFalse()
    {
        txtNombre.Enabled = false;
        txtPrecio.Enabled = false;
        txtStock.Enabled = false;
        txtDescripcion.Enabled = false;
        fuOrigenImagen.Enabled = false;
        ddlCategorias.Enabled = false;
    }

    protected void controlesEnTrue()
    {
        txtNombre.Enabled = true;
        txtPrecio.Enabled = true;
        txtDescripcion.Enabled = true;
        txtStock.Enabled = true;
        fuOrigenImagen.Enabled = true;
        ddlCategorias.Enabled = true;
    }
}

