using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Baogia : System.Web.UI.Page
{
    #region Declare objects
    private Categorys objCategory = new Categorys();
    private TVSSystem objFuncOther = new TVSSystem();
    private Products objProduct = new Products();
    private ReportConfig objReportConfig = new ReportConfig();
   
    public DataTable objTableReport= new DataTable();
    public DataTable objTableCategory = new DataTable();
    public DataTable objTableCategoryOnHomePage = new DataTable();
    public DataTable objTableNewProduct = new DataTable();
    public DataTable objTableProduct = new DataTable();
    public string strHtmlMenu = "", strHtmlBody = "", strHtmlNewProduct = "", strHtmlHeaderReport = "", strHtmlFooterReport = "";
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
                string tmpSpecification = "", tmpGuarantee = "";
                for (int i = 0; i < this.objTableNewProduct.Rows.Count; i++)
                {
                    this.strHtmlNewProduct += "<div class = \"BorderProduct\">";
                    this.strHtmlNewProduct += "<img class = \"ProductItem\"  onerror=\"imgCatchError(this)\" src = \"../Images/Products/" + this.objTableNewProduct.Rows[i]["UrlImage"].ToString() + "\" alt =\"San pham\" />";
                    this.strHtmlNewProduct += "<div class = \"ProductItemText\">" + this.objTableNewProduct.Rows[i]["Name"].ToString() + "</div>";
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
                    this.strHtmlNewProduct += "Đơn giá: " +String.Format("{0:0,0}", double.Parse(objTableNewProduct.Rows[i]["Price"].ToString()))+" đ";
                    this.strHtmlNewProduct += "</div></div><br>";
                }
            }
            #endregion

            #region Danh sach san pham cua khach hang - Giua trang
            this.objTableProduct = this.objProduct.getDataPartnerProductByCatId(200, Session["ACCOUNT_ID"].ToString(), this.itemId);
            if (this.objTableProduct.Rows.Count > 0)
            {
                DataTable objTableListCategory = new DataTable();
                objTableListCategory = this.objProduct.getDataPartnerCategory(Session["ACCOUNT_ID"].ToString());

                if (objTableListCategory.Rows.Count > 0)
                {
                    int countItemRow = 0;
                    for (int k = 0; k < objTableListCategory.Rows.Count; k++)
                    {
                        this.strHtmlBody += "<tbody>";
                        this.strHtmlBody += "<tr>";
                        this.strHtmlBody += "<td style =\"vertical-align:middle; font-weight:bold;\" colspan = \"6\">" + objTableListCategory.Rows[k]["CategoryName"].ToString() + "</td>";
                        this.strHtmlBody += "</tr>";
                        this.strHtmlBody += "</tbody>";

                        countItemRow = 0;
                        for (int j = 0; j < this.objTableProduct.Rows.Count; j++)
                        {
                            if (objTableListCategory.Rows[k]["CategoryId"].ToString() == this.objTableProduct.Rows[j]["CatId"].ToString())
                            {
                                countItemRow = countItemRow + 1;
                                this.strHtmlBody += "<tbody>";
                                this.strHtmlBody += "<tr>";
                                this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + countItemRow.ToString() + "</td>";
                                this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["Code"].ToString() + "</td>";
                                this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["Name"].ToString() + "</td>";
                                this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["Specification"].ToString() + "</td>";
                                this.strHtmlBody += "<td style =\"vertical-align:middle;\">" + this.objTableProduct.Rows[j]["Capacity"].ToString() + "</td>";
                                this.strHtmlBody += "<td style =\"vertical-align:middle; text-align:right;\">" + String.Format("{0:0,0}", double.Parse(this.objTableProduct.Rows[j]["Price"].ToString())) + " đ</td>";
                                this.strHtmlBody += "</tr>";
                                this.strHtmlBody += "</tbody>";
                            }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Cau hinh bao cao
        this.objTableReport = this.objReportConfig.getData();
        if (this.objTableReport.Rows.Count > 0)
        {
            this.strHtmlHeaderReport = this.objTableReport.Rows[0]["HeaderReport"].ToString();
            this.strHtmlFooterReport = this.objTableReport.Rows[0]["FooterReport"].ToString();
        }
        #endregion
    }
    #endregion

    #region method CreateTable
    public DataTable CreateTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("EmployeeCode", typeof(string));
        dt.Columns.Add("EmployeeName", typeof(string));
        dt.Columns.Add("Address", typeof(string));
        dt.Columns.Add("City", typeof(string));
        dt.Columns.Add("PinCode", typeof(Int32));
        dt.Columns.Add("PhoneNo", typeof(string));
        dt.Rows.Add("1", "2", "3", "4", 123, "12121");
        return dt;
    } 
    #endregion

    #region method btnExportToExcel_Click
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        this.objTableProduct = this.objProduct.getDataPartnerProductByCatId(1000, Session["ACCOUNT_ID"].ToString(), this.itemId);
        this.ExportToExcel(this.objTableProduct);
    } 
    #endregion

    #region method ExportToExcel
    private void ExportToExcel(DataTable dtExcel)
    {
        try
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Baogia.xls");

            HttpContext.Current.Response.Charset = "utf-8";

            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Arial;'>");
            HttpContext.Current.Response.Write("<BR><BR>");

            #region Header Page
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#FFFFFF' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Arial; background:white;'>");
            HttpContext.Current.Response.Write("<TR  valign='top'>");
            HttpContext.Current.Response.Write("<TD align='left' colspan='6'>" + this.strHtmlHeaderReport + "</TD>");
            HttpContext.Current.Response.Write("</TR><br> <span> Thời gian xuất hóa đơn : "+ objFuncOther.getTimeFromServer() +"</span>");
            HttpContext.Current.Response.Write("</Table>");
            #endregion

            #region Header Row

            HttpContext.Current.Response.Write("<Table border='1' bgColor='#FFFFFF' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Arial; background:white;'>");
            HttpContext.Current.Response.Write("<TR valign='middle' style='background:#D8D8D8;'>");
            HttpContext.Current.Response.Write("<TD align='left' style='width:10%; font-weight:bold;'>TT</TD>");
            HttpContext.Current.Response.Write("<TD align='center' style='width:10%; font-weight:bold;'>MÃ SẢN PHẨM</TD>");
            HttpContext.Current.Response.Write("<TD align='center' style='width:30%; font-weight:bold;'>TÊN SẢN PHẨM</TD>");
            HttpContext.Current.Response.Write("<TD align='center' style='width:20%; font-weight:bold;'>QUY CÁCH</TD>");
            HttpContext.Current.Response.Write("<TD align='center' style='width:20%; font-weight:bold;'>CÔNG SUẤT</TD>");
            HttpContext.Current.Response.Write("<TD align='center' style='width:10%; font-weight:bold;'>ĐƠN GIÁ</TD>");
            HttpContext.Current.Response.Write("</TR>");
            #endregion

            #region Detail Row
            
                DataTable objTableListCategory = new DataTable();
                objTableListCategory = this.objProduct.getDataPartnerCategory(Session["ACCOUNT_ID"].ToString());

                if (objTableListCategory.Rows.Count > 0)
                {
                    for (int k = 0; k < objTableListCategory.Rows.Count; k++)
                    {
                        #region Category Name
                        HttpContext.Current.Response.Write("<TR  valign='middle'>");
                        HttpContext.Current.Response.Write("<TD valign='middle' align='left' colspan='6' style = 'font-weight:bold;'>" + objTableListCategory.Rows[k]["CategoryName"].ToString() + "</TD>");
                        HttpContext.Current.Response.Write("</TR>");
                        #endregion
                        
                        for (int iRow = 0; iRow < dtExcel.Rows.Count; iRow++)
                        {
                            if (objTableListCategory.Rows[k]["CategoryId"].ToString() == dtExcel.Rows[iRow]["CatId"].ToString())
                            {
                                HttpContext.Current.Response.Write("<TR valign='top'>");
                                HttpContext.Current.Response.Write("<TD align='left'>" + (iRow + 1).ToString() + "</TD>");
                                HttpContext.Current.Response.Write("<TD align='left'>" + dtExcel.Rows[iRow]["Code"].ToString() + "</TD>");
                                HttpContext.Current.Response.Write("<TD align='left'>" + dtExcel.Rows[iRow]["Name"].ToString() + "</TD>");
                                HttpContext.Current.Response.Write("<TD align='left'>" + dtExcel.Rows[iRow]["Specification"].ToString() + "</TD>");
                                HttpContext.Current.Response.Write("<TD align='left'>" + dtExcel.Rows[iRow]["Capacity"].ToString() + "</TD>");
                                HttpContext.Current.Response.Write("<TD align='right'>" + String.Format("{0:0,0}", double.Parse(dtExcel.Rows[iRow]["Price"].ToString())) + "</TD>");
                                HttpContext.Current.Response.Write("</TR>");
                            }
                        }
                    }
                }
            HttpContext.Current.Response.Write("</Table>");
            #endregion

            #region Footer Page
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#FFFFFF' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Arial; background:white;'>");
            HttpContext.Current.Response.Write("<TR  valign='top'>");
            HttpContext.Current.Response.Write("<TD align='left' colspan='6'>" + this.strHtmlFooterReport + "</TD>");
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("</Table>");
            #endregion

            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }   
    #endregion
   
}