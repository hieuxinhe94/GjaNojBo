using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TimKiem : System.Web.UI.Page
{
    #region declare objects
    private Categorys objCategory = new Categorys();
    private Products objProduct = new Products();
    public DataTable objTableCategory = new DataTable();
    public DataTable objTableCategoryOnHomePage = new DataTable();
    public string strHtmlMenu = "", strHtmlBody = "", strSearchKey = "";
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT_CUSTOMER"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        try
        {
            this.strSearchKey = Request.QueryString["s"].ToString();
        }
        catch
        {
            this.strSearchKey = "";
        }
        //if (!Page.IsPostBack)
        //{
        this.objTableCategory = this.objCategory.getDataAsLeftMenu(Session["ACCOUNT_ID"].ToString());
        this.objTableCategoryOnHomePage = this.objCategory.getDataOnHomePage(Session["ACCOUNT_ID"].ToString());

        if (this.objTableCategory.Rows.Count > 0)
        {
            for (int i = 0; i < this.objTableCategory.Rows.Count; i++)
            {
                this.strHtmlMenu += "<div class=\"HeaderSecssionItem\"><a href = \"../Category?id=" + this.objTableCategory.Rows[i]["Id"].ToString() + "\">" + this.objTableCategory.Rows[i]["Name"].ToString() + "</a></div>";
            }
        }

        if (this.objTableCategoryOnHomePage.Rows.Count > 0)
        {
            DataTable objProduct = new DataTable();
            objProduct = this.objProduct.getDataSearch(this.strSearchKey);
            int countItemRow = 0;
            if (objProduct.Rows.Count > 0)
            {
                this.strHtmlBody += "<div class =\"row DefaultRow\">";
                for (int j = 0; j < objProduct.Rows.Count; j++)
                {
                    countItemRow = countItemRow + 1;
                    this.strHtmlBody += "<div class=\"col-md-3 DefaultCell\" style =\"padding:0px;\">";
                    this.strHtmlBody += "<div class=\"DefaultProductItemBorder\">";
                    this.strHtmlBody += "<a href =\"#\"><img  onerror=\"imgCatchError(this)\" style = \"height:120px; width:100%;\" src =\"Images/Products/" + objProduct.Rows[j]["UrlImage"].ToString() + "\" alt =\"\"></a>";
                    this.strHtmlBody += "<p><a href =\"#\">" + objProduct.Rows[j]["Name"].ToString().Replace(this.strSearchKey, "<span style = \"color:blue;\">" + this.strSearchKey + "</span>") + "</a></p>";
                    this.strHtmlBody += "<hr /><div class =\"DefaultProductItemPrice\">" + String.Format("{0:0,0}", double.Parse(objProduct.Rows[j]["Price"].ToString())) + " đ</div>";
                    this.strHtmlBody += "</div>";
                    this.strHtmlBody += "</div>";

                    if (countItemRow == 4)
                    {
                        countItemRow = 0;
                        this.strHtmlBody += "</div><br><div class =\"row DefaultRow\">";
                    }

                }
                this.strHtmlBody += "</div>";
            }
            this.strHtmlBody += "<br><br>";
        }
        //}
    }
    #endregion
}