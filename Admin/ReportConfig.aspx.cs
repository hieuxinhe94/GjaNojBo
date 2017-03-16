using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ReportConfig : System.Web.UI.Page
{
    #region Declare objects
    private ReportConfig objReportConfig = new ReportConfig();
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
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 12, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        this.txtHeaderReport.config.toolbar = new object[]
        {
            new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript", "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv",
                            "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" , "Link", "Unlink", "Anchor", "Image", "Table", "HorizontalRule", "PageBreak", "Font", "FontSize","TextColor", "BGColor" },
        };

        this.txtFooterReport.config.toolbar = new object[]
        {
            new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript", "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv",
                            "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" , "Link", "Unlink", "Anchor", "Image", "Table", "HorizontalRule", "PageBreak", "Font", "FontSize","TextColor", "BGColor" },
        };

        if (!Page.IsPostBack)
        {
            this.objTable = this.objReportConfig.getData();
            if (this.objTable.Rows.Count > 0)
            {
                this.txtHeaderReport.Text = this.objTable.Rows[0]["HeaderReport"].ToString();
                this.txtFooterReport.Text = this.objTable.Rows[0]["FooterReport"].ToString();
                this.ckbState.Checked = bool.Parse(this.objTable.Rows[0]["State"].ToString());
            }
        }
    }
    #endregion

    #region method btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.objReportConfig.setData(1, this.txtHeaderReport.Text, this.txtFooterReport.Text, this.ckbState.Checked);
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    #endregion
}