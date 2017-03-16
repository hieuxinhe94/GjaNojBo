<%@ Page Title="MENU - CHUYÊN MỤC" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AddCategory.aspx.cs" Inherits="Admin_AddCategory" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="AdminHeaderItem">
        NHÓM HÀNG HÓA
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Nhóm cha:
        </div>
        <div class="AdminRightItem">
            <asp:DropDownList ID="ddlCategoryParent" AutoPostBack="true" runat="server" Style="width: 49%; height: 26px; line-height: 26px; margin-top: 3px;"></asp:DropDownList>
        </div>
    </div>
    <div class="AdminItem">
        <div class="AdminLeftItem">
            Mã nhóm:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtCode" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tên nhóm:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtName" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Trạng thái:
        </div>
        <div class="AdminRightItem">
            <asp:CheckBox ID="checkState" runat="server" Text="&nbsp;&nbsp;Sử dụng" />
        </div>
    </div>
      <div style="width: 12%; height: 30px; line-height: 30px; float: left; font-size: 13px; font-family: Arial;">
           
            <br />
            <br />
            Ảnh đại diện ()
            <br />
            <asp:Label ID="lblImg1" Text="Ảnh minh hoạ" CssClass="MQTT_Normal_Text" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="txtImage" runat="server" Width="10px" Visible="false"></asp:TextBox>
            <label class="file-upload" style="margin-top: -12px;">
                <span><strong>Upload Image</strong></span>
                <asp:FileUpload ID="upImage1" runat="server" Width="100px" CssClass="FileUploadImage"
                    Height="22px" />
            </label>
        </div>

    <div class="AdminItem" style="display: table; margin-top: -5px; height: 20px; line-height: 20px; background-color: #f6f6f6;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="AdminMsg"></asp:Label>
        </div>



    </div>

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

