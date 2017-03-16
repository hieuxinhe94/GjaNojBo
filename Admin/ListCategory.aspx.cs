using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ListCategory : System.Web.UI.Page
{
    #region declare objects
    private DataTable objTable = new DataTable();
    private Categorys objCategory = new Categorys();
    private Account objAccount = new Account();
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Session["PARENTCATID"] == null)
        {
            Session["PARENTCATID"] = "0";
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 5, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        if (!Page.IsPostBack)
        {
            this.ddlCategoryParent.DataSource = this.objAccount.getDataCategoryToCombobox(0);
            this.ddlCategoryParent.DataTextField = "Name";
            this.ddlCategoryParent.DataValueField = "Id";
            this.ddlCategoryParent.DataBind();

            if (Session["PARENTCATID"] != null)
            {
                this.ddlCategoryParent.SelectedValue = Session["PARENTCATID"].ToString();
            }

            this.objTable = this.objCategory.getData(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 15;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 15)
            {
                this.tblABC.Visible = false;
            }
        }
    }
    #endregion

    #region method btnSearch_Click
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.objTable = this.objCategory.getData(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
        cpCategory.MaxPages = 1000;
        cpCategory.PageSize = 15;
        cpCategory.DataSource = this.objTable.DefaultView;
        cpCategory.BindToControl = dtlCategory;
        dtlCategory.DataSource = cpCategory.DataSourcePaged;
        dtlCategory.DataBind();
        if (this.objTable.Rows.Count < 15)
        {
            this.tblABC.Visible = false;
        }
    }
    #endregion

    #region method ddlCategoryParent_SelectedIndexChanged
    protected void ddlCategoryParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["PARENTCATID"] = this.ddlCategoryParent.SelectedValue.ToString();

        this.objTable = this.objCategory.getData(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
        cpCategory.MaxPages = 1000;
        cpCategory.PageSize = 15;
        cpCategory.DataSource = this.objTable.DefaultView;
        cpCategory.BindToControl = dtlCategory;
        dtlCategory.DataSource = cpCategory.DataSourcePaged;
        dtlCategory.DataBind();
        if (this.objTable.Rows.Count < 15)
        {
            this.tblABC.Visible = false;
        }
    }
    #endregion

}