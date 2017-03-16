<%@ Page Title="KHÁCH HÀNG, NHÓM HÀNG" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CategoryPartner.aspx.cs" Inherits="Admin_CategoryPartner" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="AdminHeaderItem">
        KHÁCH HÀNG - NHÓM HÀNG
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tên khách hàng:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtName" runat="server" CssClass="AdminTextControl" Enabled="false"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Nhóm hàng :
        </div>
        <div class="AdminRightItem">
            <div class="AdminTextControl35" style="padding-top: 1px; display: table;">
                <asp:DropDownList ID="ddlCategoryParent" AutoPostBack="true" runat="server" Style="width: 100%; height: 28px; line-height: 28px;" OnSelectedIndexChanged="ddlCategoryParent_SelectedIndexChanged"></asp:DropDownList>
                
            </div>
            <div class="AdminTextControl35" style="padding-top: 1px; display: table;">
            <asp:DropDownList ID="ddlCategory" Height="28px" runat="server" Style="margin-left:5px; width: 99% !important; max-width: 100% !important;"></asp:DropDownList>
            </div>
            <div class="AdminTextControl10" style="padding-top: 5px; display: table;">
                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/btnAdd.png" OnClick="btnAdd_Click" Style="margin-top: -4px; padding-left: 5px;" />
            </div>
            <div class="AdminTextControl10R">
               &nbsp;
            </div>
            <div class="AdminTextControl10" style="padding-top: 5px;">
                &nbsp;
            </div>
        </div>
    </div>

    <div class="AdminItem" style="margin-top: 15px; display: table;">
        <div class="AdminLeftItem" style="display: table;">
            Nhóm hàng:
        </div>
        <div class="AdminRightItem" style="display: table;">
            <div class="QuotationDetailtBG">
                <asp:DataList ID="dtlCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" Width="100%">
                    <ItemTemplate>
                        <table class="DataListTable">
                            <tr>
                                <td class="DataListTableTdItemTT" style="width: 5%;">
                                    <%# Eval("TT") %>
                                </td>
                                <td class="DataListTableTdItemJustify" style="width: 90%;">
                                    <%# Eval("CategoryName") %>
                                </td>
                                <td class="DataListTableTdItemCenter" style="width: 5%;">
                                    <a href="CategoryPartner.aspx?pid=<%# Eval("PartnerId") %>&billCode=<%# Eval("CategoryCode") %>&id=<%# Eval("PartnerId") %>"><img src ="../Images/Delete.png" alt ="Xóa"></a>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>

                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px; background-color: #daf2a5; height: 26px;"
                    id="tblABC" runat="server">
                    <tr>
                        <td>
                            <cc1:CollectionPager ID="cpCategory" runat="server" BackText="" FirstText="Đầu"
                                ControlCssClass="ProductPage" LabelText="" LastText="Cuối" NextText="" UseSlider="true"
                                ResultsFormat="" BackNextLinkSeparator="" ResultsLocation="None" BackNextLocation="None"
                                PageNumbersSeparator="&nbsp;">
                            </cc1:CollectionPager>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <br />

    <div class="AdminItem" style="margin-top:55px !important;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
        </div>
    </div>
</asp:Content>

