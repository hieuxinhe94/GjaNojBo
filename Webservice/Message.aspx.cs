using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Message : System.Web.UI.Page
{
    String userName = "";
    String passWord = "";
    Partners objPartner = new Partners();
    PartnerSMS objSMS = new PartnerSMS();
    String fullName = "";
   public DataTable objTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
           if (Session["ACCOUNT_CUSTOMER"].ToString() == null)
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

    public bool isMobileOs()
    {
        if (Request.UserAgent.IndexOf("Windows") > 0 || Request.UserAgent.IndexOf("Linux") > 0 || Request.UserAgent.IndexOf("Mac") > 0)
        {
            return false;
        }
            return true;
    }
}