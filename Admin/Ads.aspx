<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Ads.aspx.cs" ValidateRequest = "false" Inherits="Admin_Ads" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script>
        var roxyFileman = '/fileman/index.html';
        $(function () {
            CKEDITOR.replace('MainContent_txtContent', {
                filebrowserBrowseUrl: roxyFileman,
                filebrowserImageBrowseUrl: roxyFileman + '?type=image',
                removeDialogTabs: 'link:upload;image:upload'
            });
        });
    </script>
    <div class="AdminHeaderItem">
        THÔNG TIN QUẢNG CÁO
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tiêu đề:
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtTitle" runat="server" class="AdminTextControl"></asp:TextBox>
        </div>
    </div>

    <div class="AdminItem" style ="display:table;">
        <div class="AdminLeftItem" style ="display:table;">
            Nội dung:
        </div>
        <div class="AdminRightItem" style ="display:table;">
            <CKEditor:CKEditorControl ID="txtContent" CssClass="editor1" runat="server" Height="210" Width="100%" BasePath="~/ckeditor"></CKEditor:CKEditorControl>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:CheckBox ID="ckbState" Text="&nbsp;&nbsp;Kích hoạt" runat="server" Style="font-weight: normal;" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Button ID="btnSave" runat="server" Text="Ghi nhận" CssClass="btn btn-primary" OnClick ="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" CssClass="btn btn-default" OnClick ="btnCancel_Click" />
        </div>
    </div>
</asp:Content>

