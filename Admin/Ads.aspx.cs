using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Ads : System.Web.UI.Page
{
    #region Declare objects
    private Ads objAds = new Ads();
    private DataTable objTable = new DataTable();
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
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 11, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        txtContent.config.toolbar = new object[]
        {
            new object[] { "Source", "-", "Scayt" },
            new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField", "-", "Bold", "Italic", "Underline", "Strike", "-", "Subscript" },
            "/",
            new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
            new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
            new object[] { "Link", "Unlink", "Anchor" },
            new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "Iframe", "Font", "FontSize","TextColor", "BGColor" },
        };

        if (!Page.IsPostBack)
        {
            this.objTable = this.objAds.getData();
            if (this.objTable.Rows.Count > 0)
            {
                this.txtTitle.Text = this.objTable.Rows[0]["Title"].ToString();
                this.txtContent.Text = this.objTable.Rows[0]["AdsContent"].ToString();
                this.ckbState.Checked = bool.Parse(this.objTable.Rows[0]["State"].ToString());
            }
        }
    }
    #endregion

    #region method btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.txtTitle.Text == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tiêu đề quảng cáo";
            this.txtTitle.Focus();
            return;
        }
        this.objAds.setData(1, this.txtTitle.Text, this.txtContent.Text, this.ckbState.Checked);
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    #endregion
}