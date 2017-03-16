<%@ Page Title="KHÁCH HÀNG" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AddPartner.aspx.cs" Inherits="Admin_EditPartner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="AdminHeaderItem">
        KHÁCH HÀNG, ĐỐI TÁC
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tên khách hàng:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtName" runat="server" CssClass="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Địa chỉ:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtAddress" runat="server" CssClass="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Điện thoại:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtPhone" runat="server" CssClass="AdminTextControl"></asp:TextBox>
        </div>
    </div>


    <div class="AdminItem">
        <div class="AdminLeftItem">
            Email:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="AdminTextControl"></asp:TextBox>
        </div>
    </div>


    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tài khoản:
        </div>
        <div class="AdminRightItem">
            <div class = "AdminLeftItem1"><asp:TextBox ID="txtAcc" runat="server" CssClass="AdminTextControl"></asp:TextBox></div>
            <div class = "AdminLeftItem1Text">Mật khẩu:</div>
            <div class = "AdminLeftItem1" style ="padding-left:6px;"><asp:TextBox ID="txtPwd" runat="server" CssClass="AdminTextControl"></asp:TextBox></div>
            <div class = "AdminLeftItem2" style ="padding-left:6px;">
                <asp:CheckBox ID="ckbChangePwd" Text ="&nbsp;&nbsp;Thay đổi mật khẩu" runat="server" /></div>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            TK mạng xã hội:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtSocialNetwork" runat="server" CssClass="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Trạng thái:
        </div>
        <div class="AdminRightItem">
            <asp:CheckBox ID="ckbState" Text="&nbsp;&nbsp;Kích hoạt " runat="server" />
        </div>
    </div>

    <%--<div class="AdminItem" style ="display:table;">
        <div class="AdminLeftItem" style ="display:table;">
            Hình đại diện:
        </div>
        <div class="AdminRightItem" style ="display:table;">
            <asp:Label ID="lblImg1" Text="Ảnh minh hoạ" CssClass="MQTT_Normal_Text" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="txtImage" runat="server" Width="10px" Visible="false"></asp:TextBox>
            <br />
            <label class="file-upload" style="margin-top: -12px;">
                <span><strong>Upload Image</strong></span>
                <asp:FileUpload ID="upImage1" runat="server" Width="100px" CssClass="FileUploadImage"
                    Height="22px" />
            </label>
        </div>
    </div>--%>

    <div class="AdminItem" style ="display:table; margin-top:-5px; height:20px; line-height:20px; background-color:#f6f6f6;">
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
            <asp:Button ID="btnSave" runat="server" Text="Ghi nhận" CssClass ="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Thoát" CssClass ="btn btn-default" OnClick="btnCancel_Click" />
        </div>
    </div>

</asp:Content>

