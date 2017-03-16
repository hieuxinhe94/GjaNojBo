using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SelectPartnerGroup : System.Web.UI.Page
{
    #region declare objects
    private DataTable objTable = new DataTable();
    private Partners objPartner = new Partners();
    private Account objAccount = new Account();
    private int ItemId = 0;
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    #endregion

    #region method Page_Load
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
            this.ItemId = int.Parse(Request.QueryString["Id"].ToString());
        }
        catch
        {
            this.ItemId = 0;
        }

        if (!Page.IsPostBack)
        {
            
            this.objTable = this.objPartner.getAllPartner();
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
        }

    }
    #endregion

    #region method btnSelect_Click
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            NameValueCollection nvc = Request.Form;
            int partnerId = 0;
            if (nvc.Count > 0)
            {
                foreach (string s in nvc)
                    if (s.Contains("ckb"))
                    {
                        foreach (string v in nvc.GetValues(s))
                        {
                            try
                            {
                                partnerId = int.Parse(s.Replace("ckb", "").Trim());
                            }
                            catch
                            {

                            }
                            if (this.ItemId > 0 && partnerId > 0)
                            {
                                this.objPartner.setPartnerGroupDetailt(this.ItemId, partnerId);
                            }
                        }
                    }
            }
            Response.Redirect("Directory.aspx");
        }
        catch
        {
        }
    } 
    #endregion
    
}