using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddProduct : System.Web.UI.Page
{
    #region declare Objects
    private int itemId = 0;
    private Products objProducts = new Products();
    private Categorys objCategory = new Categorys();
    private Nationals objNational = new Nationals();
    private DataTable objTable = new DataTable();
    private TVSImage objImage = new TVSImage();
    private Account objAccount = new Account();
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        txtContent.config.toolbar = new object[]
        {
            new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript", "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv",
                            "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" , "Link", "Unlink", "Anchor", "Image", "Table", "HorizontalRule", "PageBreak", "Font", "FontSize","TextColor", "BGColor" },
        };
        try
        {
            this.itemId = int.Parse(Request["id"].ToString());
        }
        catch
        {
            this.itemId = 0;
        }
        if (!Page.IsPostBack)
        {
            this.ddlCategoryParent.DataSource = this.objAccount.getDataCategoryToCombobox(0, Session["ACCOUNT"].ToString());
            this.ddlCategoryParent.DataTextField = "Name";
            this.ddlCategoryParent.DataValueField = "Id";
            this.ddlCategoryParent.DataBind();

            if (this.ddlCategoryParent.Items.Count > 0)
            {
                this.ddlCategory.DataSource = this.objAccount.getDataCategoryToCombobox1(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "Id";
                this.ddlCategory.DataBind();
            }

            this.getNational();
            this.getProducts();
        }
    } 
    #endregion

    #region method getProducts
    protected void getProducts()
    {
        if (this.itemId != 0)
        {
            this.ddlCategoryParent.DataSource = this.objAccount.getDataCategoryToCombobox(0, Session["ACCOUNT"].ToString());
            this.ddlCategoryParent.DataTextField = "Name";
            this.ddlCategoryParent.DataValueField = "Id";
            this.ddlCategoryParent.DataBind();
            DataTable objTable = objProducts.getProductByIdAdmin(itemId);
            if (objTable.Rows.Count > 0)
            {
                this.txtName.Text = objTable.Rows[0]["Name"].ToString();
                if (objTable.Rows[0]["State"].ToString() == "True")
                {
                    this.chkState.Checked = true;
                }
                else
                {
                    this.chkState.Checked = false;
                }
                this.txtCode.Text = objTable.Rows[0]["Code"].ToString();
                this.txtSpecification.Text = objTable.Rows[0]["Specification"].ToString();
                this.txtCapacity.Text = objTable.Rows[0]["Capacity"].ToString();
                this.txtPrice.Text = objTable.Rows[0]["Price"].ToString();
                this.txtDiscount.Text = objTable.Rows[0]["Discount"].ToString();
                if (objTable.Rows[0]["UrlImage"].ToString().Trim() != "")
                {
                    this.lblImg1.Text = "<img style = \"width:120px; height:80px;\" src = \"../Images/Products/" + objTable.Rows[0]["UrlImage"].ToString().Trim() + "\" alt = \"\"  title = \"\">";
                }
                this.ddlCategoryParent.SelectedValue = objTable.Rows[0]["ParentId"].ToString(); ;
                this.txtContent.Text = objTable.Rows[0]["IntroContent"].ToString();
                this.ddlNational.SelectedValue = objTable.Rows[0]["Madein"].ToString();
                this.txtGuarantee.Text = objTable.Rows[0]["Guarantee"].ToString();

                this.ddlCategory.DataSource = this.objAccount.getDataCategoryToCombobox1(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "Id";
                this.ddlCategory.DataBind();


                this.ddlCategory.SelectedValue = objTable.Rows[0]["CatId"].ToString();


            }
        }
    }
    #endregion

    #region method getNational
    public void getNational()
    {
        this.ddlNational.DataSource = objNational.getData();
        this.ddlNational.DataTextField = "Name";
        this.ddlNational.DataValueField = "Id";
        this.ddlNational.DataBind();
    } 
    #endregion

    #region method checkInputOption()
    public int checkInputOption()
    {
        if (this.ddlCategory.Items.Count == 0)
        {
            this.lblMsg.Text = "Bạn chưa chọn nhóm sản phẩm";
            this.txtName.Focus();
            return -1;
        }
        if (this.txtName.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tên chuyên mục.";
            this.txtName.Focus();
            return -1;
        }
        double tmpValue = 0;
        if (this.txtGuarantee.Text == "")
        {
            try
            {
                tmpValue = double.Parse(this.txtGuarantee.Text);
            }
            catch
            {
                this.lblMsg.Text = "Bạn chưa nhập mức chiết khấu của sản phẩm";
                this.txtGuarantee.Focus();
                return -1;
            }
        }
        return 1;
    }
    #endregion
   
    #region method btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (checkInputOption() < 0) return;

        if (upImage1.PostedFile.FileName != "")
            if (!saveImage1())
            {
                return;
            }

        if (objProducts.setProduct(this.itemId, this.txtCode.Text,
             this.txtName.Text,
             this.txtSpecification.Text,
             float.Parse(this.txtPrice.Text),
             float.Parse(this.txtDiscount.Text),
             this.chkState.Checked,
             this.txtImage.Text,
             this.txtContent.Text,
             this.ddlNational.SelectedIndex,
             float.Parse(txtGuarantee.Text),
             int.Parse(this.ddlCategory.SelectedValue.ToString()),
             this.txtCapacity.Text
             ) > 0)
             {
            // send notice for inbox 
                 PartnerSMS objCreateSMS = new PartnerSMS();
            // lấy danh sách những khách hàng được phân quyền đã
            Partners objPartner = new Partners();
            DataTable objTb = new DataTable();
            objTb = objPartner.getPartnerByCat(int.Parse(this.ddlCategory.SelectedValue.ToString()));
            if(objTb.Rows.Count > 0)
            {
                
                for(int j = 0 ; j < objTb.Rows.Count ; j++)
                {
                    if (objPartner.getPartnerByAccout(objTb.Rows[j]["AccountName"].ToString()).Rows.Count > 0) {
                        try { 
                     objCreateSMS.setData(" SẢN PHẨM MỚI "
                         ," "+  this.txtName.Text + " có giá  " + this.txtPrice.Text + " VNĐ . " +  this.txtSpecification.Text,
                       int.Parse(objPartner.getPartnerByAccout(objTb.Rows[j]["AccountName"].ToString()).Rows[0]["Id"].ToString()),
                        objTb.Rows[j]["AccountName"].ToString()
                         , Session["ACCOUNT"].ToString());
                            }
                        catch { return; }
                }}

            }
         //   objCreateSMS.setData("Thay đổi sản phẩm : "," "+  this.txtName.Text + " có giá  " + this.txtPrice.Text + " " +  this.txtSpecification.Text,,,);
            //
            Page.ClientScript.RegisterStartupScript(GetType(), "msg", "confirm('Sản phẩm đã cập nhật thành công')", true);
            Response.Redirect("AddProduct.aspx");
          }
       else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msg", "confirm('Thao tác thất bại , Vui lòng thực hiện lại ')", true);
            lblMsg.Text = "Thao tác không thành công";
            return;
        }
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListProduct.aspx");
    }
    #endregion

    #region method ScaleImage
    public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
    {
        var ratio = (double)maxHeight / image.Height;
        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    } 
    #endregion

    #region method saveImage1
    private bool saveImage1()
    {
        string strBaseLoactionImg = "";
        try
        {
            strBaseLoactionImg = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["PRODUCTSIMAGE"].ToString());
            if (upImage1.PostedFile.ContentLength > 5048576)
            {
                return false;
            }
            else
            {
                string sFileName = "A" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second + DateTime.Now.Millisecond.ToString();
                string strEx = System.IO.Path.GetFileName(upImage1.PostedFile.FileName);
                strEx = strEx.Substring(strEx.LastIndexOf("."), strEx.Length - strEx.LastIndexOf("."));
                strBaseLoactionImg += sFileName + strEx;
                strBaseLoactionImg = strBaseLoactionImg.Replace("/", "\\");

                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(upImage1.PostedFile.InputStream);
                System.Drawing.Image objImage = this.objImage.saveResizeImage(bmpPostedImage, 480);

                objImage.Save(strBaseLoactionImg, ImageFormat.Jpeg);

                this.txtImage.Text = sFileName + strEx;
                this.lblImg1.Text = "<img width = \"125px\" height = \"100px\"  src = \"../Images/Products/" + sFileName + strEx + "\">";
                return true;
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }
        return false;
    }
    #endregion   

    #region method ddlCategoryParent_SelectedIndexChanged
    protected void ddlCategoryParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlCategory.DataSource = this.objAccount.getDataCategoryToCombobox1(int.Parse(this.ddlCategoryParent.SelectedValue.ToString()));
        this.ddlCategory.DataTextField = "Name";
        this.ddlCategory.DataValueField = "Id";
        this.ddlCategory.DataBind();
    }
    #endregion
}