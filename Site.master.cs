using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    #region declare objects
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    public string strSearchKey = "", strMsg = "", strName = "", strAddress = "", strPhone = "", strEmail = "", strHotline = "", strHtmlMenu = "", strHtmlMenu2 = "", strHtmlMobileMenu = "", strHtmlFullName = "", strHtmlDataUnView = "";
    private Categorys objCategory = new Categorys();
    private AboutUs objAboutUs = new AboutUs();
    private PartnerSMS objSMS = new PartnerSMS();
    private Products objProduct = new Products();
    private DataTable objTable = new DataTable();
    public DataTable objTbSMS = new DataTable();
    public DataTable objTableCategory = new DataTable();
    #endregion
   

    #region method Page_Init
    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    } 
    #endregion

    #region method master_Page_PreLoad
    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    } 
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Kiem tra phien lam viec
        if (Session["ACCOUNT_CUSTOMER"] == null)
        {
            
            Response.Redirect("Login.aspx");
        }
        #endregion

        #region Chao hoi
        this.objTbSMS = objSMS.getMSGbyAcount(Session["ACCOUNT_CUSTOMER"].ToString());
        this.strHtmlFullName = " Xin Chào " + Session["ACCOUNT_CUSTOMER"].ToString();
        #endregion

       

        #region Load trang
        if (!Page.IsPostBack)
        {
            this.objTableCategory = this.objCategory.getDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString());  //getDataAsLeftMenu

            this.strHtmlMenu += "<li class=\"dropdown\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown2\">SẢN PHẨM   <b class=\"caret\"></b>  </a><ul class=\"dropdown-menu\">";

            if (this.objTableCategory.Rows.Count > 0)
            {
                for (int i = 0; i < this.objTableCategory.Rows.Count; i++)
                {
                    this.strHtmlMobileMenu += "<li  style=\"height:150%;  \" class=\"dropdown\"><a  href=\"../Category.aspx?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">" + this.objTableCategory.Rows[i]["Name"].ToString() + "</a>"
                        + "</a>";
                    DataTable objTableChildCategory = new DataTable();
                    objTableChildCategory = this.objCategory.getChildDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString(), this.objTableCategory.Rows[i]["Id"].ToString());
                    //if (objTableChildCategory.Rows.Count > 0)
                    //{
                    //    this.strHtmlMobileMenu += "<ul class=\"dropdown-menu space-menu \" style=\" text-color:black !important; margin-left:100px; margin-bottom:10px !important; \">";
                    //    for (int j = 0; j < objTableChildCategory.Rows.Count; j++)
                    //    {
                    //        this.strHtmlMobileMenu += "<li> <a href=\"../Category.aspx?id=" + objTableChildCategory.Rows[j]["Id"].ToString() + " \"> " + objTableChildCategory.Rows[j]["Name"].ToString() + "  </a></li>";
                    //    }
                    //    this.strHtmlMobileMenu += " </ul>";
                    //}
                    this.strHtmlMobileMenu += "</li>";
                }
            }
            this.strHtmlMenu += strHtmlMobileMenu;
            this.strHtmlMenu += " </ul></li>";


            this.strHtmlDataUnView = this.objProduct.getDataByPartnerUnView(int.Parse(Session["ACCOUNT_ID"].ToString())).ToString();
            if (this.strHtmlDataUnView != "0")
            {
                string tmpCount = this.strHtmlDataUnView;
                this.strHtmlDataUnView = "<div class=\"Notification\">";
                this.strHtmlDataUnView += "<a href = \"Theodoigia.aspx\">" + tmpCount + " (TB)</a>";
                this.strHtmlDataUnView += "</div>";
            }
            else
            {
                string tmpCount = this.strHtmlDataUnView;
                this.strHtmlDataUnView = "<div class=\"UnNotification\">";
                this.strHtmlDataUnView += "GIANOIBO.COM";
                this.strHtmlDataUnView += "</div>";
            }
            this.objTable = this.objAboutUs.getData();
            if (this.objTable.Rows.Count > 0)
            {
                this.strName = this.objTable.Rows[0]["Name"].ToString().ToUpper();
                this.strAddress = this.objTable.Rows[0]["Address"].ToString();
                this.strPhone = this.objTable.Rows[0]["Phone"].ToString();
                this.strEmail = this.objTable.Rows[0]["Email"].ToString();
                this.strHotline = this.objTable.Rows[0]["Hotline"].ToString();
            }

            try
            {
                this.strSearchKey = Request.QueryString["s"].ToString();
            }
            catch
            {
                this.strSearchKey = "";
            }
            this.txtSearchKey.Value = this.strSearchKey;
        }
        #endregion
    }
    #endregion

    #region method Unnamed_LoggingOut
    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    } 
    #endregion
    
    #region method btnSearch_Click
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (this.txtSearchKey.Value.Trim() != "")
        {
            Response.Redirect("/TimKiem.aspx?s=" + this.txtSearchKey.Value);
        }
    } 
    #endregion

    #region method ImageButton1_Click
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (this.txtSaerchMobile.Value.Trim() != "")
        {
            Response.Redirect("/TimKiem.aspx?s=" + this.txtSaerchMobile.Value);
        }
    } 
    #endregion

    #region btnSeen_Click
    protected void btnSeen_Click(object sender, EventArgs e)
    {
        PartnerSMS objSms = new PartnerSMS();
        try
        {
            objSms.setSeenMsgByPartner(Session["ACCOUNT_CUSTOMER"].ToString());
        }
        catch{return;}

        Response.Redirect(Request.RawUrl);

    }
    #endregion
}