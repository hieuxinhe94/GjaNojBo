using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ComposeNotice : System.Web.UI.Page
{
    #region declare objects
    private DataTable objTable = new DataTable();
    private Partners objPartner = new Partners();
    private PartnerSMS objPartnerSMS = new PartnerSMS();
    private Account objAccount = new Account();
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

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
        this.txtContent.Focus();
        if (!Page.IsPostBack)
        {
            #region get Directory
            this.objTable = this.objPartner.getDataPartnerGroup();
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 9;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 9)
            {
                this.tblABC.Visible = false;
            }
            #endregion
        }
    } 
    #endregion

    #region method btnSent_Click
    protected void btnSent_Click(object sender, EventArgs e)
    {
        if (this.txtContent.Value.Trim() == "")
        {
            return;
        }
        try
        {
            NameValueCollection nvc = Request.Form;
            int GroupId = 0;
            bool SentSMS = false;
            if (nvc.Count > 0)
            {
                foreach (string s in nvc)
                    if (s.Contains("ckb"))
                    {
                        foreach (string v in nvc.GetValues(s))
                        {
                            try
                            {
                                GroupId = int.Parse(s.Replace("ckb", "").Trim());
                            }
                            catch
                            {

                            }
                            if (GroupId > 0)
                            {
                                this.objPartnerSMS.sendSMSPartnerByGroup(this.txtTilte.Value.Trim(),this.txtContent.Value.Trim(), GroupId, Session["ACCOUNT"].ToString());
                                SentSMS = true;
                            }
                        }
                    }
            }
            if (SentSMS)
            {
                Response.Redirect("Inbox.aspx");
            }
        }
        catch
        {
        }
    } 
    #endregion
}