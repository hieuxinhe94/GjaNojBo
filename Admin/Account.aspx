<%@ Page Title="TÀI KHOẢN" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Admin_Account" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="AdminHeaderItem">
        TÀI KHOẢN
    </div>
    <div style="width: 100%;">
        <table class="DataListTableHeader">
            <tr style ="height:40px;">
                <td class="DataListTableHeaderTdItemTT" style="width: 3%;">#
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 22%;">Nhóm tài khoản
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 24%;">Tài khoản
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 45%;">Tên tài khoản
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Sửa
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Xóa
                </td>
            </tr>
        </table>
    </div>
    <asp:DataList ID="dtlAccount" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
        Width="100%">
        <ItemTemplate>
            <table class="DataListTable" border="0">
                <tr style ="height:40px;">
                    <td class="DataListTableTdItemTT" style="width: 3%;">
                        <%# Eval("TT") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 22%;">
                        <%# Eval("GroupName") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 24%;">
                        <%# Eval("UserName") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 45%;">
                        <%# Eval("FullName") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;">
                        <a href="EditAccount.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/Edit.png" alt=""></a>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;">
                        <a href="DelAccount.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/delete.png" alt=""></a>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px; background-color: #fbf4f4; height: 26px;"
        id="tblABC" runat="server">
        <tr>
            <td style="padding-left: 6px;">
                <cc1:CollectionPager ID="cpAccount" runat="server" BackText="" FirstText="Đầu"
                    ControlCssClass="ProductPage" LabelText="" LastText="Cuối" NextText="" UseSlider="true"
                    ResultsFormat="" BackNextLinkSeparator="" ResultsLocation="None" BackNextLocation="None"
                    PageNumbersSeparator="&nbsp;">
                </cc1:CollectionPager>
            </td>
        </tr>
    </table>
    <br />
    <a href="EditAccount.aspx">
        <input type="text" value="Thêm mới" class="btn btn-primary" style="width: 90px !important;" /></a>
</asp:Content>

