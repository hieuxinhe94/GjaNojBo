using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddCategory : System.Web.UI.Page
{
    #region declare objects
    private int itemId = 0;
    private Categorys objCategory = new Categorys();
    private Account objAccount = new Account();
    private DataTable objTable = new DataTable();
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 5, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        if (Session["PARENTCATID"] == null)
        {
            Session["PARENTCATID"] = "0";
        }
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
            if (Session["PARENTCATID"] != null)
            {
                this.ddlCategoryParent.SelectedValue = Session["PARENTCATID"].ToString();
            }
            this.getCategory();
        }
    }
    #endregion

    #region method getCategory
    protected void getCategory()
    {
        if (this.itemId != 0)
        {
            objCategory = new Categorys();
            DataTable objTable = objCategory.getCategoryById(itemId);
               
            if (objTable.Rows.Count > 0)
            {
                this.txtName.Text = objTable.Rows[0]["Name"].ToString();
                this.txtCode.Text = objTable.Rows[0]["Code"].ToString();
                this.ddlCategoryParent.SelectedValue = objTable.Rows[0]["ParentId"].ToString();
                if (objTable.Rows[0]["State"].ToString() == "1")
                {
                    this.checkState.Checked = true;
                }
                else
                {
                    this.checkState.Checked = false;
                }
            }
        }
    }
    #endregion

    #region method checkInputOption()
    private int checkInputOption()
    {
        this.lblMsg.Text = "";

        if (this.txtName.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tên nhóm hàng hóa";
            this.txtName.Focus();
            return -1;
        }

        if (this.txtCode.Text.Trim() == "" || txtCode.Text.Trim().Count() > 9)
        {
            this.lblMsg.Text = "Bạn chưa nhập mã nhóm hàng hóa";
            this.txtCode.Focus();
            return -1;
        }
        return 1;
    }
    #endregion

    #region method btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (upImage1.PostedFile.FileName != "")
            if (!saveImage1())
            {
                return;
            }

        if (!(checkInputOption() > 0)) return;
        this.objCategory.addCategory(this.itemId,this.txtCode.Text,this.txtName.Text,this.checkState.Checked,int.Parse(this.ddlCategoryParent.SelectedValue.ToString()),this.txtImage.Text);   
        Response.Redirect("ListCategory.aspx");
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListCategory.aspx");
    }
    #endregion

    #region method saveImage1
    private bool saveImage1()
    {
        string strBaseLoactionImg = "";
        try
        {
            strBaseLoactionImg = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["CATEGORYIMAGE"].ToString());
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
                upImage1.PostedFile.SaveAs(strBaseLoactionImg);
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
}