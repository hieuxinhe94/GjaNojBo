using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Inbox : System.Web.UI.Page
{
    #region declare objects
    private DataTable objTable = new DataTable();
    private PartnerSMS objPartnerSMS = new PartnerSMS();
    private Account objAccount = new Account();
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

    public static int TIME_TO_DELETE = 3; // day


    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 10, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        if (!Page.IsPostBack)
        {
            this.objTable = this.objPartnerSMS.getData(Session["ACCOUNT"].ToString());
            cpPartnerSMS.MaxPages = 1000;
            cpPartnerSMS.PageSize = 15;
            cpPartnerSMS.DataSource = this.objTable.DefaultView;
            cpPartnerSMS.BindToControl = dtlPartnerSMS;
            dtlPartnerSMS.DataSource = cpPartnerSMS.DataSourcePaged;
            dtlPartnerSMS.DataBind();
            if (this.objTable.Rows.Count < 15)
            {
                this.tblABC.Visible = false;
            }
        }
    }
    #endregion
    protected void dtlPartnerSMS_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        int id = (int)dtlPartnerSMS.DataKeys[e.Item.ItemIndex];
        PartnerSMS objPartner = new PartnerSMS();
        objPartner.delData(id);
        Response.Redirect(Request.RawUrl);
    }
    protected void btnSaveTime_Click(object sender, EventArgs e)
    {
        TIME_TO_DELETE =  int.Parse(txtTimeDelete.Text);
        objPartnerSMS.delData(TIME_TO_DELETE);
        Response.Redirect(Request.RawUrl);
    }
}