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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            Session["Mensaje"] = "Usted ya inició sesión. Cierre la sesión actual si quiere acceder con otra cuenta.";

            Response.Redirect("Error.aspx");
        }
    }
}
