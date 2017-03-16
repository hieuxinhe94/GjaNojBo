using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditPartner : System.Web.UI.Page
{
    #region Declare objects
    private Partners objPartner = new Partners();
    private TVSImage objImage = new TVSImage();
    private Account objAccount = new Account();
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    private int itemid = 0;
    #endregion

    #region method PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 4, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        try
        {
            this.itemid = int.Parse(Request["id"].ToString());
        }
        catch
        {
            this.itemid = 0;
        }
        if (!Page.IsPostBack)
        {
            this.getPartnerPlaceHolder();
        }
    }
    #endregion

    #region method getPartnerPlaceHolder
    protected void getPartnerPlaceHolder()
    {
        if (this.itemid != 0)
        {
            DataTable objTable = objPartner.getPartnerById(this.itemid);
            if (objTable.Rows.Count > 0)
            {
                this.txtName.Text = objTable.Rows[0]["Name"].ToString();
                this.txtAddress.Text = objTable.Rows[0]["Address"].ToString();
                this.txtEmail.Text = objTable.Rows[0]["Email"].ToString();
                this.txtPhone.Text = objTable.Rows[0]["Phone"].ToString();
                this.txtAcc.Text = objTable.Rows[0]["Account"].ToString();
                this.txtSocialNetwork.Text = objTable.Rows[0]["SocialNetwork"].ToString();
                if (objTable.Rows[0]["State"].ToString() == "True")
                {
                    this.ckbState.Checked = true;
                }
                else
                {
                    this.ckbState.Checked = false;
                }

                //this.lblImg1.Text = "<img width = \"125px\" height = \"100px\"  src = \"../Images/Partners/" + objTable.Rows[0]["Avartar"].ToString() + "\">";
            }
        }
    }
    #endregion

    #region method btnSave_Click 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.lblMsg.Text = "";

        if (this.txtName.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tên của khách hàng";
            this.txtName.Focus();
            return;
        }

        if (this.txtPhone.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập số điện thoại của  khách hàng";
            this.txtPhone.Focus();
            return;
        }

        if (this.itemid == 0)
        {
            if (this.objPartner.checkUserName(this.txtAcc.Text.Trim()))
            {
                this.lblMsg.Text = "Tài khoản \"" + this.txtAcc.Text + "\" đã được sử dụng, vui lòng chọn tài khoản khác.";
                this.txtAcc.Focus();
                return;
            }
        }

        //if (upImage1.PostedFile.FileName != "")
        //    if (!saveImage1())
        //    {
        //        return;
        //    }

        if (objPartner.addPartner(this.itemid, this.txtName.Text, this.txtAddress.Text, this.txtPhone.Text, this.txtEmail.Text, this.ckbState.Checked, this.txtAcc.Text, this.txtPwd.Text, "", this.txtSocialNetwork.Text) < 0)
        {
            this.lblMsg.Text = "Lỗi xảy ra khi lưu thông tin ";
        }
        else
        {
            if (this.itemid > 0 && this.ckbChangePwd.Checked)
            {
                this.objPartner.updatePartnerPassword(this.txtAcc.Text, this.txtPwd.Text);
                this.lblMsg.Text = "<span style = \"color:blue;\">Bạn đã thay đổi mật khẩu thành công</span>";
            }
            else
            {
                Response.Redirect("ListPartner.aspx");
            }
        }
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListPartner.aspx");
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
}