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

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Mensaje"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = (string)Session["Mensaje"];

        Session["Mensaje"] = null;
    }
}
