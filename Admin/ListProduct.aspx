<%@ Page Title="SẢN PHẨM" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ListProduct.aspx.cs" Inherits="Admin_ListProduct" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .input_text_short_10 {
        }
    </style>
    <div style="width: 100%; height: 35px; line-height: 35px; margin-bottom: 10px;">
        <div class="AdminLeftItem">
            HÀNG HÓA :
        </div>
        <div class="AdminRightItem">
            <asp:DropDownList ID="ddlCategoryParent" AutoPostBack="true" runat="server" Style="width: 29.5%; height: 28px; line-height: 28px; margin-top: 3px;" OnSelectedIndexChanged="ddlCategoryParent_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" Style="width: 30%; height: 28px; line-height: 28px; margin-top: 3px;" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" Style="width: 35% !important; height: 28px; line-height: 28px;"></asp:TextBox>
            <asp:ImageButton ID="btnSearch" ImageUrl="../images/Search.png" runat="server" Style="margin-bottom: -8px;" OnClick="btnSearch_Click" />
        </div>
    </div>

    <div style="width: 100%;">
        <table class="DataListTableHeader" border="0">
            <tr style="height: 0px;">
                <td class="DataListTableHeaderTdItemTT" style="width: 3%;">#
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 9%;">Mã sản phẩm
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 30%;">Tên sản phẩm <asp:ImageButton runat="server" ImageUrl="/Admin/images/up.png" Height="15" ID="btnShortByName" OnClick="btnShortByName_Click" />
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 16%;">Quy cách
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 15%;">Công suất <asp:ImageButton runat="server" ImageUrl="/Admin/images/up.png" Height="15" ID="btnShortByCongSuat" OnClick="btnShortByCongSuat_Click" />
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 8%;">Đơn giá <asp:ImageButton runat="server" ImageUrl="/Admin/images/up.png" Height="15" ID="btnShortByCost"  OnClick="btnShortByCost_Click"/>
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 7%;">BH  <asp:ImageButton runat="server" ImageUrl="/Admin/images/up.png" Height="15" ID="btnShortByBH" OnClick="btnShortByBH_Click" />
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 6%;">T.Thái <asp:ImageButton runat="server" ImageUrl="/Admin/images/up.png" Height="15" ID="btnShortByState" OnClick="btnShortByState_Click" />
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Sửa
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Xóa
                </td>
            </tr>
        </table>
    </div>
    <asp:DataList ID="dtlCategory"  ItemStyle-Height="10" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" ItemStyle-BorderColor="WhiteSmoke" ItemStyle-BorderStyle="Solid"  ItemStyle-Font-Size="Small"
        Width="100%">
        <ItemTemplate>
            <table class="DataListTable" border="0">
                <tr style="height: 10px;">
                    <td class="DataListTableTdItemTT" style="width: 3%;">
                        <%# Eval("TT") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 9%;">
                        <%# Eval("Code") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 30%;">
                        <%# Eval("Name") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 16%;">
                        <%# Eval("Specification") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 15%;">
                        <%# Eval("Capacity") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: %;">
                        <input type="text" class="form-control" value="<%# Eval("Price") %>" style="width: 90px; text-align: right; margin-top: 5px; margin-bottom: 5px;" name="txt<%# Eval("Id") %>" id="txt<%# Eval("Id") %>">
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 7%;">
                        <%# Eval("Guarantee") %> tháng
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 6%;">
                        <%# Eval("StateName") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;">
                        <a href="AddProduct.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/Edit.png" alt=""></a>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;">
                        <a href="DelProduct.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/delete.png" alt=""></a>
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
 <asp:UpdateProgress ID="UpdateProgress1"  DynamicLayout="true" DisplayAfter="200" runat="server" AssociatedUpdatePanelID="UpdatePanel">
<ProgressTemplate>
    <div class="modal">
        
       
        ĐANG GỬI THÔNG BÁO ĐẾN CHO CÁC KHÁCH HÀNG 
        <div class="center">
            <img alt="" src="../Admin/images/loading_animation.gif" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>


    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <a href="AddProduct.aspx">
                <input type="text" value="Thêm mới" class="btn btn-primary" style="width: 90px !important;" /></a>
            <asp:Button ID="btnChangePrice" runat="server" CssClass="btn btn-primary" Text="Thay đổi giá" OnClick="btnChangePrice_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>

    
</asp:Content>

