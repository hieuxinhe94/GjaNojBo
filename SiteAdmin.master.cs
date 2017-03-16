using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteAdmin : MasterPage
{
    #region declare objects
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    public string strSearchKey = "", strName = "", strAddress = "", strPhone = "", strEmail = "", strHotline = "", strFullName = "";
    private AboutUs objAboutUs = new AboutUs();
    private DataTable objTable = new DataTable();
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
        if (Session["ACCOUNT"] != null)
        {
            this.strFullName = Session["FULLNAME"].ToString();
        }
        if (!Page.IsPostBack)
        {
            this.objTable = this.objAboutUs.getData();
            if (this.objTable.Rows.Count > 0)
            {
                this.strName = this.objTable.Rows[0]["Name"].ToString().ToUpper();
                this.strAddress = this.objTable.Rows[0]["Address"].ToString();
                this.strPhone = this.objTable.Rows[0]["Phone"].ToString();
                this.strEmail = this.objTable.Rows[0]["Email"].ToString();
                this.strHotline = this.objTable.Rows[0]["Hotline"].ToString();
            }
        }
    } 
    #endregion

    #region method Unnamed_LoggingOut
    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    } 
    #endregion
}