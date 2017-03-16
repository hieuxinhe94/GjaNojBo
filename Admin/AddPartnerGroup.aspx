<%@ Page Title="XUẤT XỨ" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AddPartnerGroup.aspx.cs" Inherits="Admin_AddPartnerGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="AdminHeaderItem">
        NHÓM DANH BẠ
    </div>
    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tên gọi:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtName" runat="server" CssClass="AdminTextControl"></asp:TextBox>
        </div>
    </div>
    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:CheckBox ID="checkState" runat="server" CssClass="textbox_small" Text = "&nbsp;&nbsp;Kích hoạt"></asp:CheckBox>
        </div>
    </div>
    <br />
    <div class="AdminItem" style ="display:table; margin-top:-5px; height:20px; line-height:20px; background-color:#f6f6f6;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="AdminMsg"></asp:Label>
        </div>
    </div>
    <br />
    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Button ID="btnSave" runat="server" Text="Ghi nhận" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" CssClass="btn btn-default" OnClick="btnCancel_Click" /></div>
    </div>
</asp:Content>

