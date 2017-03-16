<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Directory.aspx.cs" Inherits="Admin_Directory" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="Dicrecroty">
        <div class="DicrectoryLeft">
            <div class="stats-info stats-info1">
                <div class="panel-heading">
                    <h4 class="panel-title">NHÓM DANH BẠ</h4>
                </div>
                <div class="panel-body panel-body2">
                    <ul class="list-unstyled">
                        <asp:DataList ID="dtlCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                            Width="100%">
                            <ItemTemplate>
                                <li><a href="?id=<%# Eval("Id") %>"><%# Eval("Name") %> &nbsp;&nbsp;<%--(<%# Eval("CountItem") %>)--%></a><div class="text-success pull-right">
                                    <a href="AddPartnerGroup.aspx?id=<%# Eval("Id") %>">
                                        <img src="../images/edit.png" alt="Sửa"></a>&nbsp;&nbsp;&nbsp;<a href="DelPartnerGroup.aspx?id=<%# Eval("Id") %>"><img src="../images/delete.png" alt="Xóa"></a>
                                    &nbsp;&nbsp;&nbsp;<a href="SelectPartnerGroup.aspx?id=<%# Eval("Id") %>"><img src="../images/user.png" alt="Xóa"></a>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>
                </div>
            </div>
            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px; background-color: #f8f8f8; height: 26px;"
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
            <a href="AddPartnerGroup.aspx">
                <input type="text" value="Thêm mới" class="btn btn-primary" style="width: 90px !important;" /></a>
        </div>
        <div class="DicrectoryRight">
            <div class="stats-info stats-info1">
                <div class="panel-heading">
                    <h4 class="panel-title">DANH BẠ</h4>
                </div>
                <div class="panel-body panel-body2">
                    <ul class="list-unstyled">
                        <asp:DataList ID="dtlPartners" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                            Width="100%">
                            <ItemTemplate>
                                <li><%# Eval("PartnerName") %><div class="text-success pull-right">
                                    <a href="?id=<%# Eval("GroupId") %>&pid=<%# Eval("PartnerId") %>">
                                    <img src="../images/delete.png" alt="Xóa"></a>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>
                </div>
            </div>
            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px; background-color: #f8f8f8; height: 26px;"
                id="tblABC1" runat="server">
                <tr>
                    <td>
                        <cc1:CollectionPager ID="clgPartners" runat="server" BackText="" FirstText="Đầu"
                            ControlCssClass="ProductPage" LabelText="" LastText="Cuối" NextText="" UseSlider="true"
                            ResultsFormat="" BackNextLinkSeparator="" ResultsLocation="None" BackNextLocation="None"
                            PageNumbersSeparator="&nbsp;">
                        </cc1:CollectionPager>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

