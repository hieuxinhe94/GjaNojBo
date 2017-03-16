using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category : System.Web.UI.Page
{
    #region declare objects
    public Categorys objCategory = new Categorys();
    public Products objProduct = new Products();
    public DataTable objTableCategory = new DataTable();
    public DataTable objTableCategoryOnHomePage = new DataTable();
    public string strHtmlMenu = "", strHtmlBody = "";
    private int itemId = 0;
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

        #region Lay bien truy van
        try
        {
            this.itemId = int.Parse(Request.QueryString["id"].ToString());
        }
        catch
        {
            this.itemId = 0;
        }
        #endregion

        #region Load trang
        if (!Page.IsPostBack)
        {
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

            #region Danh sach san pham theo nhom
            // kiem tra xem có là menu cha  hay không 
            if (this.objCategory.IsParentCategory(this.itemId))
            {
               

            }
            else
            {
                //
                this.objTableCategoryOnHomePage = this.objCategory.getDataByCategory(this.itemId);
                if (this.objTableCategoryOnHomePage.Rows.Count > 0)
                {
                    string tmpSpecification = "", tmpGuarantee = "";
                    DataTable objProduct = new DataTable();
                    int countItem = 0;
                    for (int i = 0; i < this.objTableCategoryOnHomePage.Rows.Count; i++)
                    {
                        this.strHtmlBody += "<div class=\"row DefaultHeader\">" + this.objTableCategoryOnHomePage.Rows[i]["Name"].ToString() + "</div> ";
                        objProduct = this.objProduct.getDataByCategoryId(int.Parse(this.objTableCategoryOnHomePage.Rows[i]["Id"].ToString()), 1000);
                        if (objProduct.Rows.Count > 0)
                        {
                            this.strHtmlBody += "<div class =\"row DefaultRow\">";
                            for (int j = 0; j < objProduct.Rows.Count; j++)
                            {
                                countItem = countItem + 1;
                                this.strHtmlBody += "<div class=\"col-md-3 DefaultCell\" style =\"padding:0px;\">";
                                this.strHtmlBody += "<div class=\"DefaultProductItemBorder\">";
                                if (objProduct.Rows[j]["UrlImage"].ToString().Trim() == "")
                                {
                                    this.strHtmlBody += "<a href =\"../Detailt.aspx?id=" + objProduct.Rows[j]["Id"].ToString().Trim() + "\"><img onerror=\"imgCatchError(this)\" style = \"height:120px; width:100%;\" src =\"Images/Products/NoImg.png\" alt =\"\"></a>";
                                }
                                else
                                {
                                    this.strHtmlBody += "<a href =\"../Detailt.aspx?id=" + objProduct.Rows[j]["Id"].ToString().Trim() + "\"><img  onerror=\"imgCatchError(this)\" style = \"height:120px; width:100%;\" src =\"Images/Products/" + objProduct.Rows[j]["UrlImage"].ToString() + "\" alt =\"\"></a>";
                                }
                                this.strHtmlBody += "<p><a href =\"../Detailt.aspx?id=" + objProduct.Rows[j]["Id"].ToString().Trim() + "\">" + objProduct.Rows[j]["Name"].ToString().ToUpper() + "</a></p>";
                                if (objProduct.Rows[j]["Specification"].ToString().Trim() == "")
                                {
                                    tmpSpecification = "&nbsp;";
                                }
                                else
                                {
                                    tmpSpecification = objProduct.Rows[j]["Specification"].ToString().Trim();
                                }
                                if (objProduct.Rows[j]["Guarantee"].ToString().Trim() == "")
                                {
                                    tmpGuarantee = "&nbsp;";
                                }
                                else
                                {
                                    tmpGuarantee = objProduct.Rows[j]["Guarantee"].ToString().Trim();
                                }
                                this.strHtmlBody += "<p style = \"padding:0px; margin-bottom:5px;\">" + tmpSpecification + "</p>";
                                this.strHtmlBody += "<p style = \"padding:0px; margin-bottom:-5px;\">Bảo hành: " + tmpGuarantee + " tháng</p>";
                                this.strHtmlBody += "<hr /><div class =\"DefaultProductItemPrice\">" + String.Format("{0:0,0}", double.Parse(objProduct.Rows[j]["Price"].ToString())) + " đ</div>";
                                this.strHtmlBody += "</div>";
                                this.strHtmlBody += "</div>";
                                if (countItem == 4)
                                {
                                    this.strHtmlBody += "</div><br><div class =\"row DefaultRow\">";
                                    countItem = 0;
                                }
                            }
                            if (countItem != 4)
                            {
                                this.strHtmlBody += "</div>";
                            }
                        }
                        this.strHtmlBody += "<br><br>";
                    }
                }
            }
            #endregion
        }
        #endregion
    }
    #endregion
}