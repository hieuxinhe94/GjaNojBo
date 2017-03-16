using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    #region declare objects
    public Categorys objCategory = new Categorys();
    public Products objProduct = new Products();
    private Ads objAds = new Ads();
    public DataTable objTableCategory = new DataTable();
    private DataTable objTableAds = new DataTable();
    public DataTable objTableCategoryOnHomePage = new DataTable();
    public DataTable objTableNewProduct = new DataTable();
    public string strHtmlMenu = "",strHtmlMenu2="", strHtmlBody = "", strHtmlNewProduct = "", strAds = "";
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Kiem tra phien lam viec
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
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

            }
            catch
            {

            }
            #endregion  
          
            Response.Redirect("Login.aspx");
        }

        if (Session["ACCOUNT_ID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        #endregion

        #region Danh sach nhom san pham - Cha - con : Danh cho di dong
        this.objTableCategory = this.objCategory.getDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString());  //getDataAsLeftMenu
        this.strHtmlMenu2 += "";

        if (this.objTableCategory.Rows.Count > 0)
        {
            for (int i = 0; i < this.objTableCategory.Rows.Count; i++)
            {
               
                // kiem tra xem co nhom con hay khong 
               if( Session["ACCOUNT_CUSTOMER"].ToString() =="admin")
               {
                   this.strHtmlMenu2 += " <li style=\"min-height: 40px;height: 42px;  text-align: left; font-size: 15px; padding-top: 10.5px;   background: url(../Images/design/bg-cate-bottom.png) repeat-x; \"  data-toggle=\"collapse\" data-target=\"#products2" + this.objTableCategory.Rows[i]["Id"].ToString() + "\" class=\"collapsed active\"> <a href=\"../Category?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">  <i class=\"glyphicon glyphicon-folder-open\"></i> " + this.objTableCategory.Rows[i]["Name"].ToString() + " </a> ";
               }
                
                DataTable objTableChildCategory = new DataTable();
                objTableChildCategory = this.objCategory.getChildDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString(), this.objTableCategory.Rows[i]["Id"].ToString());

                if (objTableChildCategory.Rows.Count > 0)
                {
                    if (Session["ACCOUNT_CUSTOMER"].ToString() != "admin")
                    {
                        this.strHtmlMenu2 += " <li style=\"min-height: 40px;height: 42px;  text-align: left; font-size: 15px; padding-top: 10.5px;   background: url(../Images/design/bg-cate-bottom.png) repeat-x; \"  data-toggle=\"collapse\" data-target=\"#products2" + this.objTableCategory.Rows[i]["Id"].ToString() + "\" class=\"collapsed active\"> <a href=\"../Category?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">  <i class=\"glyphicon glyphicon-folder-open\"></i> " + this.objTableCategory.Rows[i]["Name"].ToString() + " </a> ";
                    }
                   // this.strHtmlMenu2 += " <li style=\"min-height: 40px;height: 42px;  text-align: left; font-size: 15px; padding-top: 10.5px;   background: url(../Images/design/bg-cate-bottom.png) repeat-x; \"  data-toggle=\"collapse\" data-target=\"#products2" + this.objTableCategory.Rows[i]["Id"].ToString() + "\" class=\"collapsed active\"> <a href=\"../Category?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">  <i class=\"glyphicon glyphicon-folder-open\"></i> " + this.objTableCategory.Rows[i]["Name"].ToString() + " </a> ";
             
                    this.strHtmlMenu2 += "<span   aria-hidden=\"true\" style=\"float:right !important\"> <i class=\" glyphicon glyphicon-chevron-right \"></i> </span> </li>";

                    this.strHtmlMenu2 += "<ul  style=\"list-style: none !important; margin-left:-10% \" class=\"sub-menu collapse\" id=\"products2" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">";
                    for (int j = 0; j < objTableChildCategory.Rows.Count; j++)
                    {
                        this.strHtmlMenu2 += "<li style=\"min-height: 30px;height: 40px;vertical-algin:midded;border-right:  1px solid #cdcdcd; float:left; text-align: center; font-size: 12px; padding-top: 5.5px;width: 50%;   background: url(../Images/design/bg-cate-bottom.png) repeat-x; \">  <a href=\"../Category.aspx?id=" + objTableChildCategory.Rows[j]["Id"].ToString() + " \"> " + objTableChildCategory.Rows[j]["Name"].ToString() + "  </a></li>";
                    }
                    this.strHtmlMenu2 += "<div style=\"clear:both\"></div>";
                    this.strHtmlMenu2 += "</ul>";
                }

            }

        }
        #endregion

        #region Danh sach nhom san pham - ben trai
        this.objTableCategory = this.objCategory.getDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString());  //getDataAsLeftMenu
        this.strHtmlMenu += "";
       
        if (this.objTableCategory.Rows.Count > 0)
        {
            for (int i = 0; i < this.objTableCategory.Rows.Count; i++)
            {
                this.strHtmlMenu += "    <li  data-toggle=\"collapse\" data-target=\"#products" + this.objTableCategory.Rows[i]["Id"].ToString() + "\" class=\"collapsed active\"> <a href=\"../Category?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\"><i class=\"fa fa-gift fa-lg\"></i> " + this.objTableCategory.Rows[i]["Name"].ToString() + " </a> <span class=\"arrow\"></span> </li>";
            //    this.strHtmlMenu += "<div class=\"HeaderSecssionItem\"><a href = \"../Category?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">" + this.objTableCategory.Rows[i]["Name"].ToString() + "</a></div>";

                DataTable objTableChildCategory = new DataTable();
               objTableChildCategory = this.objCategory.getChildDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString(), this.objTableCategory.Rows[i]["Id"].ToString());

               if (objTableChildCategory.Rows.Count > 0)
               {
                   this.strHtmlMenu += " <ul class=\"sub-menu collapse\" id=\"products" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">";
                   for (int j = 0; j < objTableChildCategory.Rows.Count; j++)
                   {
                       this.strHtmlMenu += "<li  > <a href=\"../Category.aspx?id=" + objTableChildCategory.Rows[j]["Id"].ToString() + " \"> " + objTableChildCategory.Rows[j]["Name"].ToString() + "  </a></li>";

                   }
                   this.strHtmlMenu += "</ul>";
                }
            
            }
                
        }
        #endregion

        #region Danh sach san pham cua khach hang - Giua trang
        //this.objTableCategoryOnHomePage = this.objCategory.getDataOnHomePage(Session["ACCOUNT_ID"].ToString());
        //if (this.objTableCategoryOnHomePage.Rows.Count > 0)
        //{
        //    DataTable objProduct = new DataTable();
        //    objProduct = this.objProduct.getDataNewPost(20, Session["ACCOUNT_ID"].ToString());
        //    int countItemRow = 0;
        //    if (objProduct.Rows.Count > 0)
        //    {
        //        string tmpSpecification = "", tmpGuarantee = "";
        //        this.strHtmlBody += "<div class =\"row DefaultRow\">";
        //        for (int j = 0; j < objProduct.Rows.Count; j++)
        //        {
        //            countItemRow = countItemRow + 1;
        //            this.strHtmlBody += "<div class=\"col-md-3 DefaultCell\" style =\"padding:0px;\">";
        //            this.strHtmlBody += "<div class=\"DefaultProductItemBorder\">";
        //            if (objProduct.Rows[j]["UrlImage"].ToString().Trim() == "")
        //            {
        //                this.strHtmlBody += "<a href =\"../Detailt.aspx?id=" + objProduct.Rows[j]["Id"].ToString().Trim() + "\"><img onerror=\"imgCatchError(this)\" style = \"height:120px; width:100%;\" src =\"Images/Products/Gianoibo.ico\" alt =\"\"></a>";
        //            }
        //            else
        //            {
        //                this.strHtmlBody += "<a href =\"../Detailt.aspx?id=" + objProduct.Rows[j]["Id"].ToString().Trim() + "\"><img onerror=\"imgCatchError(this)\" style = \"height:120px; width:100%;\" src =\"Images/Products/" + objProduct.Rows[j]["UrlImage"].ToString() + "\" alt =\"\"></a>";
        //            }
        //            this.strHtmlBody += "<p><a href =\"../Detailt.aspx?id=" + objProduct.Rows[j]["Id"].ToString().Trim() + "\">" + objProduct.Rows[j]["Name"].ToString().ToUpper() + "</a></p>";
        //            if (objProduct.Rows[j]["Specification"].ToString().Trim() == "")
        //            {
        //                tmpSpecification = "&nbsp;";
        //            }
        //            else
        //            {
        //                tmpSpecification = objProduct.Rows[j]["Specification"].ToString().Trim();
        //            }
        //            if (objProduct.Rows[j]["Guarantee"].ToString().Trim() == "")
        //            {
        //                tmpGuarantee = "&nbsp;";
        //            }
        //            else
        //            {
        //                tmpGuarantee = objProduct.Rows[j]["Guarantee"].ToString().Trim();
        //            }
        //            this.strHtmlBody += "<p style = \"padding:0px; margin-bottom:5px;\">" + tmpSpecification + "</p>";
        //            this.strHtmlBody += "<p style = \"padding:0px; margin-bottom:-5px;\">Bảo hành: " + tmpGuarantee + " tháng</p>";
        //            this.strHtmlBody += "<hr /><div class =\"DefaultProductItemPrice\">" + String.Format("{0:0,0}", double.Parse(objProduct.Rows[j]["Price"].ToString())) + " đ</div>";
        //            this.strHtmlBody += "</div>";
        //            this.strHtmlBody += "</div>";

        //            if (countItemRow == 4)
        //            {
        //                countItemRow = 0;
        //                this.strHtmlBody += "</div><br><div class =\"row DefaultRow\">";
        //            }

        //        }
        //        this.strHtmlBody += "</div>";
        //    }
        //    this.strHtmlBody += "<br><br>";
        //}
        #endregion

        #region Danh sach san pham khach hang - Ben trai
        this.objTableNewProduct = this.objProduct.getDataNewPost(3, Session["ACCOUNT_ID"].ToString());
        if (this.objTableNewProduct.Rows.Count > 0)
        {
            string tmpSpecification = "", tmpGuarantee = "";
            for (int i = 0; i < this.objTableNewProduct.Rows.Count; i++)
            {
                this.strHtmlNewProduct += "<div class = \"BorderProduct\">";
                this.strHtmlNewProduct += "<a href =\"../Detailt.aspx?id=" + this.objTableNewProduct.Rows[i]["Id"].ToString().Trim() + "\"><img class = \"ProductItem\" onerror=\"imgCatchError(this)\"  src = \"../Images/Products/" + this.objTableNewProduct.Rows[i]["UrlImage"].ToString() + "\" alt =\"San pham\" /></a>";
                this.strHtmlNewProduct += "<a href =\"../Detailt.aspx?id=" + this.objTableNewProduct.Rows[i]["Id"].ToString().Trim() + "\"><div class = \"ProductItemText\">" + this.objTableNewProduct.Rows[i]["Name"].ToString() + "</div></a>";
                if (this.objTableNewProduct.Rows[i]["Specification"].ToString().Trim() == "")
                {
                    tmpSpecification = "&nbsp;";
                }
                else
                {
                    tmpSpecification = this.objTableNewProduct.Rows[i]["Specification"].ToString().Trim();
                }
                if (this.objTableNewProduct.Rows[i]["Guarantee"].ToString().Trim() == "")
                {
                    tmpGuarantee = "&nbsp;";
                }
                else
                {
                    tmpGuarantee = this.objTableNewProduct.Rows[i]["Guarantee"].ToString().Trim();
                }
                this.strHtmlNewProduct += "<p style = \"padding:0px; margin-bottom:5px;\">" + tmpSpecification + "</p>";
                this.strHtmlNewProduct += "<p style = \"padding:0px; margin-bottom:10px; Text-align:center;\">Bảo hành: " + tmpGuarantee + " tháng</p>";
                this.strHtmlNewProduct += "<div class = \"FooterSecssionProductBG\">";
                this.strHtmlNewProduct += "Đơn giá: " + String.Format("{0:0,0}", double.Parse(objTableNewProduct.Rows[i]["Price"].ToString())) + " đ";
                this.strHtmlNewProduct += ""+
                  "</div></div><br>";
            }
        }
        #endregion

        #region Noi dung quang cao
        this.objTableAds = this.objAds.getDataOnHOme();
        if (this.objTableAds.Rows.Count > 0)
        {
            this.strAds += "<div id=\"boxes\">";
            this.strAds += "<div style=\"top: 199.5px; left: 551.5px; display: none;\" id=\"dialog\" class=\"window\">";
            this.strAds += this.objTableAds.Rows[0]["Title"].ToString();
            this.strAds += "<div id=\"lorem\">";
            this.strAds += this.objTableAds.Rows[0]["AdsContent"].ToString();
            this.strAds += "</div>";
            this.strAds += "</div>";
            this.strAds += "<div style=\"width: 1478px; font-size: 32pt; color: white; height: 602px; display: none; opacity: 0.8;\" id=\"mask\"></div>";
            this.strAds += "</div>";
        }
        #endregion
    }
    #endregion
}