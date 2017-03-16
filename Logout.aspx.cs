using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ACCOUNT_CUSTOMER"] = null;
        Session["FULLNAME_CUSTOMER"] = null; ;
        Session["ACCOUNT_ID"] = null;
        Response.Redirect("Login.aspx");
    }
}