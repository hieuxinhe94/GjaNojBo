using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    public TVSSystem objInfo = new TVSSystem();
   public System.Data.DataTable objOnlinetbl = new System.Data.DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objOnlinetbl = objInfo.gettUserOnline();
        }
        catch
        {
            return;
        }
    }
}
