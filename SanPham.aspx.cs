using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SanPham : Page
{
    #region declare objects
    private Categorys objCategory = new Categorys();
    private Products objProduct = new Products();
    public DataTable objTableCategory = new DataTable();
    public DataTable objTableCategoryOnHomePage = new DataTable();
    public DataTable objTableNewProduct = new DataTable();
    public string strHtmlMenu = "", strHtmlBody = "", strHtmlNewProduct = "";
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT_CUSTOMER"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!Page.IsPostBack)
        {
            this.objTableCategoryOnHomePage = this.objCategory.getDataOnHomePage(Session["ACCOUNT_ID"].ToString());

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

            if (this.objTableCategoryOnHomePage.Rows.Count > 0)
            {
                DataTable objProduct = new DataTable();
                for (int i = 0; i < this.objTableCategoryOnHomePage.Rows.Count; i++)
                {
                    this.strHtmlBody += "<div class=\"row DefaultHeader\"><a href = \"../Category.aspx?id=" + this.objTableCategoryOnHomePage.Rows[i]["Id"].ToString() + "\">" + this.objTableCategoryOnHomePage.Rows[i]["Name"].ToString() + "</a></div>";
                    objProduct = this.objProduct.getDataByCategoryId(int.Parse(this.objTableCategoryOnHomePage.Rows[i]["Id"].ToString()), 4);
                    if (objProduct.Rows.Count > 0)
                    {
                        this.strHtmlBody += "<div class =\"row DefaultRow\">";
                        for (int j = 0; j < objProduct.Rows.Count; j++)
                        {
                            this.strHtmlBody += "<div class=\"col-md-3 DefaultCell\" style =\"padding:0px;\">";
                            this.strHtmlBody += "<div class=\"DefaultProductItemBorder\">";
                            this.strHtmlBody += "<a href =\"#\"><img  onerror=\"imgCatchError(this)\" style = \"height:120px; width:100%;\" src =\"Images/Products/" + objProduct.Rows[j]["UrlImage"].ToString() + "\" alt =\"\"></a>";
                            this.strHtmlBody += "<p><a href =\"#\">" + objProduct .Rows[j]["Name"].ToString()+ "</a></p>";
                            this.strHtmlBody += "<hr /><div class =\"DefaultProductItemPrice\">" + String.Format("{0:0,0}", double.Parse(objProduct.Rows[j]["Price"].ToString())) + " đ</div>";
                            this.strHtmlBody += "</div>";
                            this.strHtmlBody += "</div>";
                        }
                        this.strHtmlBody += "</div>";
                    }
                    this.strHtmlBody += "<br><br>";
                }
            }

            this.objTableNewProduct = this.objProduct.getDataNewPost(3, Session["ACCOUNT_ID"].ToString());
            if (this.objTableNewProduct.Rows.Count > 0)
            {
                for (int i = 0; i < this.objTableNewProduct.Rows.Count; i++)
                {
                    this.strHtmlNewProduct += "<img  onerror=\"imgCatchError(this)\" class = \"ProductItem\" src = \"../Images/Products/" + this.objTableNewProduct.Rows[i]["UrlImage"].ToString() + "\" alt =\"San pham\" />";
                    this.strHtmlNewProduct += "<div class = \"ProductItemText\">" + this.objTableNewProduct.Rows[i]["Name"].ToString() + "</div>";
                    this.strHtmlNewProduct += "<div class = \"FooterSecssionProductBG\">";
                    this.strHtmlNewProduct += String.Format("{0:0,0}", double.Parse(objTableNewProduct.Rows[i]["Price"].ToString()));
                    this.strHtmlNewProduct += "</div><br>";
                }
            }
        }
    }
    #endregion
}