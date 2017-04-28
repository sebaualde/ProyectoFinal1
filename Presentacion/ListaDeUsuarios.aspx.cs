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

public partial class ListaDeUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Response.CacheControl = "no-cache";

                CargarListaUsuarios();
            }
        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al cargar la página.";
        }
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CargarListaUsuarios();
        }
        catch
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.CssClass = "labelerror";
            lblMensaje.Text = "Se produjo un error al listar los usuarios.";

        }

    }

    protected void CargarListaUsuarios()
    {
        List<Usuario> usuarios = new List<Usuario>();

        if (ddlTipo.SelectedValue == "todos" || ddlTipo.SelectedValue == "clientes")
        {
            foreach (UsuarioRegistrado ur in LogicaUsuario.ListarUsuariosRegistrados())
            {
                usuarios.Add(ur);
            }
        }

        if (ddlTipo.SelectedValue == "todos" || ddlTipo.SelectedValue == "administradores")
        {
            foreach (Administrador ad in LogicaUsuario.ListarAdministradores())
            {
                usuarios.Add(ad);
            }
        }

        if (usuarios.Count == 0)
        {
            Session["Mensaje"] = "¡ERROR! En estos momentos no hay ningun usuario registrado en el sistema.";
            Response.Redirect("~/Default.aspx");
        }

        DataTable dtUsuarios = new DataTable();
        dtUsuarios.Columns.Add(new DataColumn("Imagen"));
        dtUsuarios.Columns.Add(new DataColumn("Cedula"));
        dtUsuarios.Columns.Add(new DataColumn("NombreCompleto"));
        dtUsuarios.Columns.Add(new DataColumn("NombreUsuario"));
        dtUsuarios.Columns.Add(new DataColumn("DireccionEnvio"));
        dtUsuarios.Columns.Add(new DataColumn("NumeroTarjeta"));
        dtUsuarios.Columns.Add(new DataColumn("Telefono"));
        dtUsuarios.Columns.Add(new DataColumn("Cargo"));

        foreach (Usuario u in usuarios)
        {
            DataRow drwUsuario = dtUsuarios.NewRow();
            drwUsuario["Imagen"] = u.Imagen;
            drwUsuario["Cedula"] = u.Cedula;
            drwUsuario["NombreCompleto"] = u.NombreCompleto;
            drwUsuario["NombreUsuario"] = u.NombreUsuario;

            if (u is UsuarioRegistrado)
            {
                drwUsuario["DireccionEnvio"] = ((UsuarioRegistrado)u).DireccionEnvio;
                drwUsuario["NumeroTarjeta"] = ((UsuarioRegistrado)u).NumeroTarjeta;
                drwUsuario["Telefono"] = ((UsuarioRegistrado)u).Telefono;
            }
            else if (u is Administrador)
            {
                drwUsuario["Cargo"] = ((Administrador)u).Cargo;
            }

            dtUsuarios.Rows.Add(drwUsuario);
        }

        gvUsuarios.DataSource = dtUsuarios;
        gvUsuarios.DataBind();

    }
}
