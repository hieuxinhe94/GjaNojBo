using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Directory : System.Web.UI.Page
{
    #region declare objects
    private DataTable objTable = new DataTable();
    private DataTable objTablePartner = new DataTable();
    private Partners objPartner = new Partners();
    private Account objAccount = new Account();
    private bool View = false, Add = false, Edit = false, Del = false, Orther = false;
    private int itemId = 0, partnerId = 0;
    #endregion

    #region method Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ACCOUNT"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.objAccount.checkForFunction(Session["ACCOUNT"].ToString(), 9, ref View, ref Add, ref Edit, ref Del, ref Orther))
        {
            Response.Redirect("NoPermission.aspx");
        }
        try
        {
            this.itemId = int.Parse(Request.QueryString["Id"].ToString());
        }
        catch
        {
            this.itemId = 0;
        }
        try
        {
            this.partnerId = int.Parse(Request.QueryString["pid"].ToString());
        }
        catch
        {
            this.partnerId = 0;
        }

        if (this.itemId > 0 && this.partnerId > 0)
        {
            this.objPartner.delPartnerGroupDetailt(this.itemId, this.partnerId);
        }

        if (!Page.IsPostBack)
        {
            #region get Directory
            this.objTable = this.objPartner.getDataPartnerGroup();
            cpCategory.MaxPages = 1000;
            cpCategory.PageSize = 20;
            cpCategory.DataSource = this.objTable.DefaultView;
            cpCategory.BindToControl = dtlCategory;
            dtlCategory.DataSource = cpCategory.DataSourcePaged;
            dtlCategory.DataBind();
            if (this.objTable.Rows.Count < 9)
            {
                this.tblABC.Visible = false;
            }
            #endregion

            #region get Directory Partner
            if (this.itemId > 0)
            {
                this.objTablePartner = this.objPartner.getPartnerGroupDetailt(this.itemId);
                clgPartners.MaxPages = 1000;
                clgPartners.PageSize = 9;
                clgPartners.DataSource = this.objTablePartner.DefaultView;
                clgPartners.BindToControl = dtlPartners;
                dtlPartners.DataSource = clgPartners.DataSourcePaged;
                dtlPartners.DataBind();
                if (this.objTablePartner.Rows.Count < 9)
                {
                    this.tblABC1.Visible = false;
                }
            }
            #endregion
        }
    }
    #endregion
}