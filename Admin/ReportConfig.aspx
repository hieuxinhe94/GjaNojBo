<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ReportConfig.aspx.cs" ValidateRequest = "false" Inherits="Admin_ReportConfig" %>

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
        CẤU HÌNH BÁO CÁO
    </div>

    <div class="AdminItem" style ="display:table;">
        <div class="AdminLeftItem" style ="display:table;">
            Tiêu đề trên:
        </div>
        <div class="AdminRightItem" style ="display:table;">
            <CKEditor:CKEditorControl ID="txtHeaderReport" CssClass="editor1" runat="server" Height="125" Width="100%" BasePath="~/ckeditor"></CKEditor:CKEditorControl>
        </div>
    </div>

    <div class="AdminItem" style ="display:table;">
        <div class="AdminLeftItem" style ="display:table;">
            Tiêu đề dưới:
        </div>
        <div class="AdminRightItem" style ="display:table;">
            <CKEditor:CKEditorControl ID="txtFooterReport" CssClass="editor1" runat="server" Height="125" Width="100%" BasePath="~/ckeditor"></CKEditor:CKEditorControl>
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

