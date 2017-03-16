using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddPartnerGroup : System.Web.UI.Page
{
    #region declare objects
    private int itemId = 0;
    private Partners objPartner = new Partners();
    private DataTable objTable = new DataTable();
    private Account objAccount = new Account();
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtName.Focus();
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 8, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
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
            if (this.itemId != 0)
            {
                DataTable objTable = objPartner.getPartnerGroupById(itemId);
                if (objTable.Rows.Count > 0)
                {
                    this.txtName.Text = objTable.Rows[0]["Name"].ToString();
                    if (objTable.Rows[0]["State"].ToString() == "True")
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
    } 
    #endregion

    #region method checkInputOption()
    private int checkInputOption()
    {
        this.lblMsg.Text = "";
        if (this.txtName.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tên chuyên mục.";
            this.txtName.Focus();
            return -1;
        }
        return 1;
    }
    #endregion

    #region method btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.lblMsg.Text = "";
        if (this.txtName.Text.Trim() == "")
        {
            this.lblMsg.Text = "Bạn chưa nhập tên chuyên mục.";
            this.txtName.Focus();
            return;
        }
        this.objPartner.setPartnerGroup(this.itemId,this.txtName.Text, this.checkState.Checked);
        Response.Redirect("Directory.aspx");
    }
    #endregion

    #region method btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Directory.aspx");
    }
    #endregion

}