using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Theodoigia : System.Web.UI.Page
{
    #region Declare objects
    private Categorys objCategory = new Categorys();
    private Products objProduct = new Products();
    public DataTable objTableCategory = new DataTable();
    public DataTable objTableCategoryOnHomePage = new DataTable();
    public DataTable objTableNewProduct = new DataTable();
    public DataTable objTableProduct = new DataTable();
    public string strHtmlMenu = "", strHtmlBody = "", strHtmlNewProduct = "";
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

        #region Load trang
        if (!Page.IsPostBack)
        {
            #region Danh sach nhom san pham - ben trai
            this.objTableCategory = this.objCategory.getDataAsLeftMenu(Session["ACCOUNT_ID"].ToString());

            if (this.objTableCategory.Rows.Count > 0)
            {
                for (int i = 0; i < this.objTableCategory.Rows.Count; i++)
                {
                    this.strHtmlMenu += "<div class=\"HeaderSecssionItem\"><a href = \"../Baogia.aspx?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">" + this.objTableCategory.Rows[i]["Name"].ToString() + "</a></div>";
                }
            }
            #endregion

            #region Danh sach san pham - ben trai - phia duoi
            this.objTableNewProduct = this.objProduct.getDataNewPost(3, Session["ACCOUNT_ID"].ToString());
            if (this.objTableNewProduct.Rows.Count > 0)
            {
                for (int i = 0; i < this.objTableNewProduct.Rows.Count; i++)
                {
                    this.strHtmlNewProduct += "<img class = \"ProductItem\" src = \"../Images/Products/" + this.objTableNewProduct.Rows[i]["UrlImage"].ToString() + "\" alt =\"San pham\" />";
                    this.strHtmlNewProduct += "<div class = \"ProductItemText\">" + this.objTableNewProduct.Rows[i]["Name"].ToString() + "</div>";
                    this.strHtmlNewProduct += "<div class = \"FooterSecssionProductBG\">";
                    this.strHtmlNewProduct += "Đơn giá: " +String.Format("{0:0,0}", double.Parse(objTableNewProduct.Rows[i]["Price"].ToString()))+" đ";
                    this.strHtmlNewProduct += "</div><br>";
                }
            }
            #endregion

            #region Danh sach san pham cua khach hang - Giua trang
            this.objTableProduct = this.objProduct.getDataPartnerProductPrice(Session["ACCOUNT_ID"].ToString());
            int countItemRow = 0;
            if (this.objTableProduct.Rows.Count > 0)
            {
                for (int j = 0; j < this.objTableProduct.Rows.Count; j++)
                {
                    countItemRow = countItemRow + 1;
                    this.strHtmlBody += "<tbody>";
                    this.strHtmlBody += "<tr>";
                    this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + countItemRow.ToString() + "</td>";
                    this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["Code"].ToString() + "</td>";
                    this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["Name"].ToString() + "</td>";
                    this.strHtmlBody += "<td style =\"vertical-align:middle; text-align:right;\">" + String.Format("{0:0,0}", double.Parse(this.objTableProduct.Rows[j]["Price"].ToString())) + " đ</td>";
                    this.strHtmlBody += "<td style =\"vertical-align:middle; text-align:right;\">" + String.Format("{0:0,0}", double.Parse(this.objTableProduct.Rows[j]["PriceOld"].ToString())) + " đ</td>";
                    this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["DayChange"].ToString() + "</td>";
                    this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["Note"].ToString() + "</td>";
                    this.strHtmlBody += "</tr>";
                    this.strHtmlBody += "</tbody>";
                }
            }
            #endregion
        }
        #endregion
    }
    #endregion
}