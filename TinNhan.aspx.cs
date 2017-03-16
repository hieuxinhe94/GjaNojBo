using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Webservice_TinNhan : System.Web.UI.Page
{
    String userName = "";
    String passWord = "";
    Partners objPartner = new Partners();
    PartnerSMS objSMS = new PartnerSMS();
    String fullName = "";
    public DataTable objTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["ACCOUNT_CUSTOMER"] == null)
        {
            #region Kiểm tra xem có được kích hoạt từ ứng dụng hay không ?
            try
            {
                String version = Request.QueryString["version"].ToString();
                String username = Request.QueryString["username"].ToString();
                String password = Request.QueryString["password"].ToString();

                Partners objPartner = new Partners();
                String FullName = "";
                if (objPartner.checkForLoginWithoutMd5(username.Trim(), password.Trim(), ref FullName))
                {
                    Session["ACCOUNT_CUSTOMER"] = username.Trim();
                    Session["FULLNAME_CUSTOMER"] = FullName;
                    Session["ACCOUNT_ID"] = objPartner.getPartnerIdByAccount(username.Trim()).ToString();

                    objTable = objSMS.getMSGbyAcount(userName);
                    return;
                  }
                       else
                       {
                    Response.Redirect("Login.aspx");
                      }
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }
            #endregion
        }
        else
        {
            objTable = objSMS.getMSGbyAcount(Session["ACCOUNT_CUSTOMER"].ToString());
        }
        // check OS here for attack
    }

    #region btnSeen_Click
    protected void btnSeen_Click(object sender, EventArgs e)
    {
        PartnerSMS objSms = new PartnerSMS();
        try
        {
            objSms.setSeenMsgByPartner(Session["ACCOUNT_CUSTOMER"].ToString());
        }
        catch { return; }

        Response.Redirect(Request.RawUrl);

    }
    #endregion
}