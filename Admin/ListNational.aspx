<%@ Page Title="Xuất xứ" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ListNational.aspx.cs" Inherits="Admin_ListNational" %>


<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="AdminHeaderItem">
        XUẤT XỨ HÀNG HÓA
    </div>
    <div style="width: 100%;">
        <table class="DataListTableHeader">
            <tr style ="height:40px;">
                <td class="DataListTableHeaderTdItemTT" style="width: 3%;">#
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 81%;">Xuất xứ
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 10%;">Trạng thái
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Sửa
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Xóa
                </td>
            </tr>
        </table>
    </div>
    <asp:DataList ID="dtlCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
        Width="100%">
        <ItemTemplate>
            <table class="DataListTable">
                <tr style ="height:40px;">
                    <td class="DataListTableTdItemTT" style="width: 3%;"><%# Eval("TT") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 81%;"><%# Eval("Name") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 10%;">
                        <%# Eval("State") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;"><a href="AddNationals.aspx?id=<%# Eval("Id") %>">
                        <img src="../Images/Edit.png" alt="Sửa"></a>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;"><a href="DelNational.aspx?id=<%# Eval("Id") %>">
                        <img src="../Images/delete.png" alt="Sửa"></a>
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
    <br />
    <a href="AddNationals.aspx">
        <input type="text" value="Thêm mới" class="btn btn-primary" style="width: 90px !important;" /></a>
</asp:Content>
