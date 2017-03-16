using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    #region Declare objects
    private Partners objPartner = new Partners();
    private AboutUs objAboutUs = new AboutUs();
    private DataTable objTable = new DataTable();
    public string strHtmlIntro1 = "", strHtmlIntro2 = "";
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.objTable = this.objAboutUs.getData();
            if (this.objTable.Rows.Count > 0)
            {
                this.strHtmlIntro1 = this.objTable.Rows[0]["Greeting"].ToString();
                this.strHtmlIntro2 = this.objTable.Rows[0]["Greeting1"].ToString();
            }
        }
    } 
    #endregion

    #region method btnLogin_Click
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        this.lblMsg.Text = "";

        if (this.txtUserName.Value.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tên tài khoản!";
            this.txtUserName.Focus();
            return;
        }

        if (this.txtPassWord.Value.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập mật khẩu của tài khoản!";
            this.txtPassWord.Focus();
            return;
        }
        string FullName = "";
        if (this.objPartner.checkForLogin(this.txtUserName.Value.Trim(), this.txtPassWord.Value.Trim(), ref FullName))
        {
            Session["ACCOUNT_CUSTOMER"] = this.txtUserName.Value.Trim();
            Session["FULLNAME_CUSTOMER"] = FullName;
            Session["ACCOUNT_ID"] = this.objPartner.getPartnerIdByAccount(this.txtUserName.Value.Trim()).ToString();
            // set online 
            TVSSystem objInfo = new TVSSystem();
            objInfo.setOnline(this.txtUserName.Value.Trim().ToString());
            //
            Response.Redirect("Default.aspx");
        }
        else
        {
            this.lblMsg.Text = "Thông tin đăng nhập không hợp lệ!";
        }
    } 
    #endregion
}