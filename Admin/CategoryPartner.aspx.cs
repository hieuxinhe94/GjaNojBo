using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CategoryPartner : System.Web.UI.Page
{
    #region Declare objects
    private Partners objPartner = new Partners();
    private Categorys objCategorys = new Categorys();
    private Account objAccount = new Account();
    private DataTable objTable = new DataTable();
    private int itemId = 0;
    public string itemCode = "";
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        try
        {
            this.itemId = int.Parse(Request.QueryString["id"].ToString());
        }
        catch
        {
            this.itemId = 0;
        }
        try
        {
            this.itemCode = Request.QueryString["billCode"].ToString();
        }
        catch
        {
            this.itemCode = "";
        }
        if (this.itemId > 0 && this.itemCode != "")
        {
            this.objCategorys.delDataPartner(this.itemCode, this.itemId.ToString());
        }
        if (!Page.IsPostBack)
        {
            this.getCategory();
            this.getPartnerInformation();

            this.objTable = this.objCategorys.getDataPartner(this.itemId.ToString());
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 10;
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

    #region method btnAdd_Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
       

        if (this.ddlCategory.Items.Count == 0)
        {
            return;
        }
        if (this.ddlCategory.SelectedValue == "0")
        {
            foreach(ListItem ll in this.ddlCategory.Items) 
            {
                this.objCategorys.setDataPartner(ll.Value, this.itemId.ToString(), this.txtName.Text.ToString());
            }
        }

        this.objCategorys.setDataPartner(this.ddlCategory.SelectedValue.ToString(), this.itemId.ToString(), this.txtName.Text.ToString());
        this.objTable = this.objCategorys.getDataPartner(this.itemId.ToString());
        cpCategory.MaxPages = 1000;
        cpCategory.PageSize = 10;
        cpCategory.DataSource = this.objTable.DefaultView;
        cpCategory.BindToControl = dtlCategory;
        dtlCategory.DataSource = cpCategory.DataSourcePaged;
        dtlCategory.DataBind();
        if (this.objTable.Rows.Count < 10)
        {
            this.tblABC.Visible = false;
        }
    }
    #endregion

    #region method getCategory
    public void getCategory()
    {
        DataTable objTable = this.objCategorys.getDataForCombobox(this.itemId);
        if (objTable.Rows.Count > 1)
        {
            
            this.ddlCategoryParent.DataSource = objTable;
            this.ddlCategoryParent.DataTextField = "Name";
            this.ddlCategoryParent.DataValueField = "Code";
            this.ddlCategoryParent.SelectedValue = "0";
            this.ddlCategoryParent.DataBind();
        }
    }
    #endregion

    #region method getPartnerInformation
    protected void getPartnerInformation()
    {
        if (this.itemId != 0)
        {
            DataTable objTable = objPartner.getPartnerById(this.itemId);
            if (objTable.Rows.Count > 0)
            {
                this.txtName.Text = objTable.Rows[0]["Name"].ToString();
            }
        }
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListPartner.aspx");
    }
    #endregion

    #region method ddlCategoryParent_SelectedIndexChanged
    protected void ddlCategoryParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlCategoryParent.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "alertas", "if(confirm('Vui lòng chọn từng nhóm và nhấn Add+')){window.location.reload(true)}", false);
            
        }
        
        else
            {
            this.ddlCategory.DataSource = this.objAccount.getDataCategoryToCombobox2(this.ddlCategoryParent.SelectedValue.ToString());
            this.ddlCategory.DataTextField = "Name";
            this.ddlCategory.DataValueField = "Code";
            this.ddlCategory.SelectedValue = "0";
            this.ddlCategory.DataBind();
            
            }
        this.objTable = this.objCategorys.getDataPartner(this.itemId.ToString());
        cpCategory.MaxPages = 1000;
        cpCategory.PageSize = 10;
        cpCategory.DataSource = this.objTable.DefaultView;
        cpCategory.BindToControl = dtlCategory;
        dtlCategory.DataSource = cpCategory.DataSourcePaged;
        dtlCategory.DataBind();
        if (this.objTable.Rows.Count < 10)
        {
            this.tblABC.Visible = false;
        }
        
    }
    #endregion
}