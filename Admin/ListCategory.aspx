<%@ Page Title="NHÓM HÀNG HÓA" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ListCategory.aspx.cs" Inherits="Admin_ListCategory" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

     <div style="width: 100%; height: 35px; line-height: 35px; margin-bottom: 10px;">
        <div class="AdminLeftItem">
            NHÓM HÀNG HÓA :
        </div>
        <div class="AdminRightItem">
            <asp:DropDownList ID="ddlCategoryParent" AutoPostBack="true" runat="server" Style="width: 29.5%; height: 28px; line-height: 28px; margin-top: 3px;" OnSelectedIndexChanged="ddlCategoryParent_SelectedIndexChanged"></asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" Style="width: 35% !important; height: 28px; line-height: 28px;"></asp:TextBox>
            <asp:ImageButton ID="btnSearch" ImageUrl="../images/Search.png" runat="server" Style="margin-bottom: -8px;" OnClick="btnSearch_Click" />
        </div>
    </div>
    <div style="width: 70%; margin-top:10px" >
        <table class="DataListTableHeader" >
            <tr style ="height:40px;">
                <td class="DataListTableHeaderTdItemTT" style="width: 3%;">#
                </td>
                 <td class="DataListTableHeaderTdItemJustify" style="width: 20%;">Nhóm sản phẩm
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 9%;">Mã nhóm
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 40%;">Tên nhóm
                </td>
                 <td class="DataListTableHeaderTdItemCenter" style="width: 15%;">Trạng thái
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 6%;">Sửa
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 6%;">Xóa
                </td>
            </tr>
        </table>
    </div>
    <asp:DataList ID="dtlCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" Width="70%" CellPadding="1" ItemStyle-BorderColor="WhiteSmoke" BorderStyle="Solid"  ItemStyle-BorderStyle="Solid" > 
        <ItemTemplate>
            <table class="DataListTable" >
                <tr style ="height:20px; ">
                    <td class="DataListTableTdItemTT" style="width: 3%;">
                        <%# Eval("TT") %>
                    </td>
                     <td class="DataListTableTdItemJustify" style="width: 20%; font-family:'Comic Sans MS'; font-size:11px !important;">
                        <%# Eval("ParentName") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 9%;">
                        <%# Eval("Code") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 40%; font-family:'Lucida Sans Unicode' ; font-size:12px !important;">
                        <%# Eval("Name") %>
                    </td>
                   
                    <td class="DataListTableTdItemCenter" style="width: 15%;">
                        <%# Eval("StateName") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 6%;">
                        <a href="AddCategory.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/Edit.png" alt="Sửa"></a>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 6%;">
                        <a href="DelCategory.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/Delete.png" alt="Xóa"></a>
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
    <a href="AddCategory.aspx">
        <input type="text" value="Thêm mới" class="btn btn-primary" style="width: 90px !important;" /></a>
</asp:Content>

