using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DelQuotation : System.Web.UI.Page
{
    #region declare objects
    private Account objAccount = new Account();
    private Quotation objQuotation = new Quotation();
    private string itemId = "";
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 8, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        try
        {
            this.itemId = Request.QueryString["id"].ToString();
        }
        catch
        {
            this.itemId = "";
        }
    } 
    #endregion

    #region method btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.objQuotation.delData(this.itemId);
        Response.Redirect("ListQuotation.aspx");
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListQuotation.aspx");
    }
    #endregion
}