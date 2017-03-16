using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Account : System.Web.UI.Page
{
    #region declare objects
    private DataTable objTable = new DataTable();
    private Account objAccount = new Account();
    private int currPage = 0;
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 3, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        try
        {
            this.currPage = int.Parse(Request.QueryString["Page"].ToString());
        }
        catch
        {
            this.currPage = 0;
        }

        if (!Page.IsPostBack)
        {
            this.objTable = this.objAccount.getData();
            cpAccount.MaxPages = 1000;
            cpAccount.PageSize = 15;
            cpAccount.DataSource = this.objTable.DefaultView;
            cpAccount.BindToControl = dtlAccount;
            dtlAccount.DataSource = cpAccount.DataSourcePaged;
            dtlAccount.DataBind();
            if (this.objTable.Rows.Count < 9)
            {
                this.tblABC.Visible = false;
            }
            else
            {
                this.tblABC.Visible = true;
            }
        }
    }
    #endregion
}