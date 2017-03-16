using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddQuotation : System.Web.UI.Page
{
    #region declare objects
    public int itemId = 0, partnerId = 0;
    public string itemCode = "";
    private Quotation objQuotation = new Quotation();
    private DataTable objTable = new DataTable();
    private Categorys objCategory = new Categorys();
    private Products objProduct = new Products();
    private Partners objPartner = new Partners();
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
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 8, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
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
            this.partnerId = int.Parse(Request.QueryString["pid"].ToString());
        }
        catch
        {
            this.partnerId = 0;
        }
        try
        {
            this.itemCode = Request.QueryString["billCode"].ToString();
        }
        catch
        {
            this.itemCode = "";
        }
        if (this.partnerId > 0 && this.itemCode != "")
        {
            this.objQuotation.delDataPartner(this.itemCode, this.partnerId.ToString());
        }
        if(!Page.IsPostBack)
        {
            if (this.itemId > 0)
            {
                this.txtCode.ReadOnly = true;
            }
            this.objTable = this.objQuotation.getData(this.itemId);
            if (this.objTable.Rows.Count > 0)
            {
                this.txtCode.Text = this.objTable.Rows[0]["Code"].ToString();
                this.txtName.Text = this.objTable.Rows[0]["Name"].ToString();
                this.txtFileUrl.Text = this.objTable.Rows[0]["FileUrl"].ToString();
                this.checkState.Checked = bool.Parse(this.objTable.Rows[0]["State"].ToString());
                //this.ddlPartner.SelectedValue = this.objTable.Rows[0]["PartnerId"].ToString();

                this.objTable = this.objQuotation.getDataPartner(this.itemId.ToString(), this.txtCode.Text);
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
                this.getPartner();
            }
        }
    } 
    #endregion

    #region method btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.lblMsg.Text = "";
        if (this.txtCode.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập mã số của báo giá";
            this.txtCode.Focus();
            return;
        }
        if (this.itemId == 0)
        {
            if (this.objQuotation.checkCode(this.txtCode.Text.Trim()))
            {
                this.lblMsg.Text = "Mã số báo giá bạn nhận đã tồn tại";
                this.txtCode.Focus();
                return;
            }
        }
        if (this.txtName.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tên gọi của báo giá";
            this.txtName.Focus();
            return;
        }
        if (this.txtFileUrl.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập đường dẫn file báo giá";
            this.txtFileUrl.Focus();
            return;
        }
        //if (this.txtDiscount.Text.Trim() == "")
        //{
        //    this.lblMsg.Text = "Bạn chưa nhập mức chiết khấu của báo giá";
        //    this.txtDiscount.Focus();
        //    return;
        //}
        //double tmpDiscount = 0;
        //try
        //{
        //    tmpDiscount = double.Parse(this.txtDiscount.Text);
        //}
        //catch
        //{
        //    this.lblMsg.Text = "Bạn chưa nhập mức chiết khấu của báo giá";
        //    this.txtDiscount.Focus();
        //    return;
        //}
        if (this.objQuotation.setData(this.itemId, this.txtCode.Text, this.txtName.Text, "0", this.txtFileUrl.Text, 0, this.checkState.Checked) == 1)
        {
            Response.Redirect("ListQuotation.aspx");
        }
        else
        {
            this.lblMsg.Text = "Lỗi xảy ra khi tạo báo giá!";
            return;
        }
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListQuotation.aspx");
    }
    #endregion

    #region method btnAdd_Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (this.ddlPartner.Items.Count == 0)
        {
            return;
        }
        this.objQuotation.setDataPartner(this.txtCode.Text, this.ddlPartner.SelectedValue.ToString(), this.ddlPartner.SelectedItem.Text.ToString());

        this.objTable = this.objQuotation.getDataPartner(this.itemId.ToString(), this.txtCode.Text);
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

    #region method getPartner
    public void getPartner()
    {
        DataTable objTable = this.objPartner.getDataForCombobox(this.txtCode.Text);
        if (objTable.Rows.Count > 1)
        {
            this.ddlPartner.DataSource = objTable;
            this.ddlPartner.DataTextField = "Name";
            this.ddlPartner.DataValueField = "Id";
            this.ddlPartner.DataBind();
        }
    }
    #endregion
}