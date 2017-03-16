using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ListProduct : System.Web.UI.Page
{
    #region declare objects
    private DataTable objTable = new DataTable();
    private Products objProduct = new Products();
    private Categorys objCategory = new Categorys();
    private Account objAccount = new Account();
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!Page.IsPostBack)
        {

            this.ddlCategoryParent.DataSource = this.objAccount.getDataCategoryToCombobox(0, Session["ACCOUNT"].ToString());
            this.ddlCategoryParent.DataTextField = "Name";
            this.ddlCategoryParent.DataValueField = "Id";
            this.ddlCategoryParent.DataBind();

            if (this.ddlCategoryParent.Items.Count > 0)
            {
                this.ddlCategory.DataSource = this.objAccount.getDataCategoryToCombobox1(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "Id";
                this.ddlCategory.DataBind();
            }

            this.getData();
        }
    }
    #endregion

    #region method getData
    private void getData()
    {
        if (this.ddlCategory.Items.Count > 0)
        {
            this.objTable = this.objProduct.getDataByCategoryId(int.Parse(this.ddlCategory.SelectedValue.ToString()), this.txtSearch.Text.Trim());
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 50;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 10)
            {
                this.tblABC.Visible = false;
            }
        }
    } 
    #endregion

    #region method ddlCategoryParent_SelectedIndexChanged
    protected void ddlCategoryParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlCategory.DataSource = this.objAccount.getDataCategoryToCombobox1(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
        this.ddlCategory.DataTextField = "Name";
        this.ddlCategory.DataValueField = "Id";
        this.ddlCategory.DataBind();
    }
    #endregion

    #region method ddlCategory_SelectedIndexChanged
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.getData();
    } 
    #endregion

    #region method btnSearch_Click
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.getData();
    } 
    #endregion

    #region method btnChangePrice_Click
    protected void btnChangePrice_Click(object sender, EventArgs e)
    {
        try
        {
            NameValueCollection nvc = Request.Form;
            int productId = 0;
            double productPrice = 0;
            if (nvc.Count > 0)
            {
                foreach (string s in nvc)
                    if (s.Contains("txt"))
                    {
                        foreach (string v in nvc.GetValues(s))
                        {
                            try
                            {
                                productId = int.Parse(s.Replace("txt", "").Trim());
                            }
                            catch
                            {

                            }
                            try
                            {
                                productPrice = double.Parse(v.Trim());
                            }
                            catch
                            {

                            }
                            if (productId > 0) {
                                this.objProduct.setPrice(productId, productPrice, Session["ACCOUNT"].ToString(),ddlCategory.SelectedValue);
                            }

                        }
                    }
            }
        }
        catch
        {
        }
    }
     #endregion

    #region shortName
    protected void btnShortByName_Click(object sender, ImageClickEventArgs e)
    {
         if (this.ddlCategory.Items.Count > 0)
        {
            this.objTable = this.objProduct.getDataByCategoryId(int.Parse(this.ddlCategory.SelectedValue.ToString()), this.txtSearch.Text.Trim(), "tblProduct.Name");
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 50;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 10)
            {
                this.tblABC.Visible = false;
            }}
    }
    #endregion

    #region shortCông suất
    protected void btnShortByCongSuat_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ddlCategory.Items.Count > 0)
        {
            this.objTable = this.objProduct.getDataByCategoryId(int.Parse(this.ddlCategory.SelectedValue.ToString()), this.txtSearch.Text.Trim(), "tblProduct.Capacity");
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 50;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 10)
            {
                this.tblABC.Visible = false;
            }
        }
    }
    #endregion

    #region shortGiá
    protected void btnShortByCost_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ddlCategory.Items.Count > 0)
        {
            this.objTable = this.objProduct.getDataByCategoryId(int.Parse(this.ddlCategory.SelectedValue.ToString()), this.txtSearch.Text.Trim(), "tblProduct.Price");
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 50;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 10)
            {
                this.tblABC.Visible = false;
            }
        }
    }
    #endregion

    #region short BẢo hành
    protected void btnShortByBH_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ddlCategory.Items.Count > 0)
        {
            this.objTable = this.objProduct.getDataByCategoryId(int.Parse(this.ddlCategory.SelectedValue.ToString()), this.txtSearch.Text.Trim(), "tblProduct.Guarantee");
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 50;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 10)
            {
                this.tblABC.Visible = false;
            }
        }


    }
    #endregion

    #region short Trạng thái
    protected void btnShortByState_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ddlCategory.Items.Count > 0)
        {
            this.objTable = this.objProduct.getDataByCategoryId(int.Parse(this.ddlCategory.SelectedValue.ToString()), this.txtSearch.Text.Trim(), "tblProduct.State");
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 50;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 10)
            {
                this.tblABC.Visible = false;
            }
        }
    }
    #endregion
}