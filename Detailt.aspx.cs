using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Detailt : System.Web.UI.Page
{
    #region declare objects
    private Categorys objCategory = new Categorys();
    private Products objProduct = new Products();
    public DataTable objTableCategory = new DataTable();
    public DataTable objProductDetailt = new DataTable();
    public string strHtmlMenu = "", strHtmlImage = "", strHtmlName = "", strHtmlPrice = "", strHtmlSpecification = "", strHtmlCapacity = "", strHtmlGuarantee = "", strHtmlIntroContent = "";
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

            #region Lay thong tin chi tiet ve san pham
            this.objProductDetailt = this.objProduct.getProductById(this.itemId);
            if (this.objProductDetailt.Rows.Count > 0)
            {
               
                    this.strHtmlImage = "<img  onerror=\"imgCatchError(this)\" src = \"Images/Products/" + this.objProductDetailt.Rows[0]["UrlImage"].ToString() + "\" alt = \"\">";
                this.strHtmlName = this.objProductDetailt.Rows[0]["Name"].ToString();
                this.strHtmlPrice = String.Format("{0:0,0}", double.Parse(this.objProductDetailt.Rows[0]["Price"].ToString())) + " đ";
                this.strHtmlSpecification = this.objProductDetailt.Rows[0]["Specification"].ToString();
                this.strHtmlCapacity = this.objProductDetailt.Rows[0]["Capacity"].ToString();
                this.strHtmlGuarantee = this.objProductDetailt.Rows[0]["Guarantee"].ToString();
                this.strHtmlIntroContent = this.objProductDetailt.Rows[0]["IntroContent"].ToString();
            }
            #endregion
        }
        #endregion
    }
    #endregion
}