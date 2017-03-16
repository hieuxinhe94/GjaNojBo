<%@ Page Title="TÀI KHOẢN" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EditAccount.aspx.cs" Inherits="Admin_EditAccount" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="AdminHeaderItem">
        THÔNG TIN TÀI KHOẢN
    </div>
    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;Tài khoản:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtUserName" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>
    
    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;Họ tên:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtFullName" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;Địa chỉ email:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtEmail" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;Mật khẩu:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtPassWord" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;Nhóm tài khoản:
        </div>
        <div class="AdminRightItem">
            <asp:DropDownList ID="ddlGroupId" runat="server" Style="width: 100%; height: 26px; line-height: 26px; margin-top: 3px;"></asp:DropDownList>
        </div>
    </div>

    <div class="AdminItem" >
        <div class="AdminLeftItem" style ="display:table;">
            &nbsp;&nbsp;Nhóm sản phẩm quản lý
        </div>
        <div class="AdminRightItem" style ="display:table;">
            <asp:DataList ID="dtlAccountCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" OnDeleteCommand="dtlAccountCategory_DeleteCommand" DataKeyField="Id"
                Width="100%">
                <ItemTemplate>
                    <table class="DataListTable" border="0">
                        <tr style="background-color:yellowgreen">
                            <td class="DataListTableTdItemTT" style="width: 4%;">
                                <%# Eval("TT") %>
                            </td>
                            <td class="DataListTableTdItemJustify" style="width: 83%;     background-color: #3c763d !important;">
                                <%# Eval("Name") %>
                            </td>
                            <td class="DataListTableTdItemCenter" style="width: 3%;">
                                 <asp:ImageButton runat="server" ID="Delete" ImageUrl="../images/delete.png" CommandName="delete" />
                             
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px; background-color: #fbf4f4; height: 26px;"
                id="tblABC" runat="server">
                <tr>
                    <td style="padding-left: 6px;">
                        <cc1:CollectionPager ID="cpAccounrCategory" runat="server" BackText="" FirstText="Đầu"
                            ControlCssClass="ProductPage" LabelText="" LastText="Cuối" NextText="" UseSlider="true"
                            ResultsFormat="" BackNextLinkSeparator="" ResultsLocation="None" BackNextLocation="None"
                            PageNumbersSeparator="&nbsp;">
                        </cc1:CollectionPager>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <div style="width: 100%; height: 35px; line-height: 35px; margin-top: 10px;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:DropDownList ID="ddlCategoryParent" AutoPostBack = "true" runat="server" Style="width: 49%; height: 26px; line-height: 26px; margin-top: 3px;" ></asp:DropDownList>
            
        </div>
    </div>

    <div style="width: 100%; height: 35px; line-height: 35px; margin-top: 10px;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Button ID="btnAddCategory" CssClass="btn btn-default" runat="server" Text="Thêm nhóm" OnClick="btnAddCategory_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ckbState" Text="&nbsp;&nbsp;Kích hoạt" runat="server" Style="font-weight: normal;" />
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor = "Red"></asp:Label>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Button ID="btnSave" runat="server" Text="Ghi nhận" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Thoát" CssClass="btn btn-default" OnClick="btnCancel_Click" />
        </div>
    </div>

</asp:Content>

