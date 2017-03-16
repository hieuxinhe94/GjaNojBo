<%@ Page Title="THÊM BÁO GIÁ" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AddQuotation.aspx.cs" Inherits="Admin_AddQuotation" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="AdminHeaderItem">
        LẬP BÁO GIÁ
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Mã báo giá:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtCode" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tên báo giá:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtName" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            File báo giá:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtFileUrl" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Khách hàng :
        </div>
        <div class="AdminRightItem">
            <div class="AdminTextControl50" style="padding-top: 1px; display: table;">
                <asp:DropDownList ID="ddlPartner" Height="28px" Width="100%" runat="server" Style="width: 100% !important; max-width: 100% !important;"></asp:DropDownList>
            </div>
            <div class="AdminTextControl10" style="padding-top: 5px; display: table;">
                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/btnAdd.png" OnClick="btnAdd_Click" Style="margin-top: -4px; padding-left: 5px;" />
            </div>
            <div class="AdminTextControl10R">
                Trạng thái:
            </div>
            <div class="AdminTextControl10" style="padding-top: 5px;">
                <asp:CheckBox ID="checkState" runat="server" Text="&nbsp;&nbsp;Sử dụng" />
            </div>
        </div>
    </div>

    <div class="AdminItem" style="display: table; margin-top: 5px; height: 20px; line-height: 20px; background-color: #f6f6f6;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="AdminMsg"></asp:Label>
        </div>
    </div>

    <div class="AdminItem" style="margin-top: 15px; display: table;">
        <div class="AdminLeftItem" style="display: table;">
            Khách báo giá:
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
                                    <%# Eval("PartnerName") %>
                                </td>
                                <td class="DataListTableTdItemCenter" style="width: 5%;">
                                    <a href="AddQuotation.aspx?pid=<%# Eval("PartnerId") %>&billCode=<%# Eval("QuotationCode") %>&id=<%# Eval("QuotationId") %>">
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
            </div>
        </div>
    </div>
    <br />
    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Button ID="btnSave" runat="server" Text="Ghi nhận" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" CssClass="btn btn-default" OnClick="btnCancel_Click" />
        </div>
    </div>

</asp:Content>

