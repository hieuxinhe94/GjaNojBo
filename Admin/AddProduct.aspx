<%@ Page Title="SẢN PHẨM" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="AdminHeaderItem">
        <div style="float: left; width: 12%; text-align:right;">SẢN PHẨM :&nbsp;&nbsp;</div>
        <div style="float: right; width: 88%;">
            <div style="float: left; width: 45%;">
                <asp:DropDownList ID="ddlCategoryParent" AutoPostBack = "true" runat="server" Style="width: 99%; height: 28px; line-height: 28px;" OnSelectedIndexChanged="ddlCategoryParent_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div style="float: left; width: 30%;">
                <asp:DropDownList ID="ddlCategory" Height="28px" runat="server" Style ="width:100% !important; max-width:100% !important;"></asp:DropDownList>
            </div>
            <div style="float: left; width: 12%; font-weight:normal !important; text-transform:lowercase; text-align:right;">
                <span style ="text-transform:uppercase;">M</span>ã sản phẩm :
            </div>
            <div style="float: right; width: 12%; font-weight:normal !important; text-transform:lowercase; padding-left:5px;">
                <asp:TextBox ID="txtCode" runat="server" CssClass="AdminTextControl"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Tên sản phẩm :
        </div>
        <div class="AdminRightItem">
            <asp:TextBox ID="txtName" runat="server" CssClass="AdminTextControl"></asp:TextBox>
        </div>
    </div>
    <div class="AdminItem">
        <div class="AdminLeftItem">
            Quy cách :
        </div>
        <div class="AdminRightItem">
            <div class="AdminRow">
                <div class="AdminTextControl30">
                    <asp:TextBox ID="txtSpecification" runat="server" class="AdminTextControl"></asp:TextBox>
                </div>
                <div class="AdminTextControl10R">
                    Công suất :
                </div>
                 <div class="AdminTextControl60">
                    <asp:TextBox ID="txtCapacity" runat="server" class="AdminTextControl"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <div class="AdminItem">
        <div class="AdminLeftItem">
            Giá bán :
        </div>
        <div class="AdminRightItem">
            <div class="AdminRow">
                <div class="AdminTextControl10">
                    <asp:TextBox ID="txtPrice" runat="server" class="AdminTextControl" Text="0"></asp:TextBox>
                </div>
                <div class="AdminTextControl10R">
                    Chiết khấu :
                </div>
                <div class="AdminTextControl10">
                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="AdminTextControl" Text="0"></asp:TextBox>
                </div>
                <div class="AdminTextControl10R">
                    Bảo hành :
                </div>
                <div class="AdminTextControl10">
                    <asp:TextBox ID="txtGuarantee" runat="server" CssClass="AdminTextControl" Text="0"></asp:TextBox>
                </div>
                <div class="AdminTextControl10R">
                    Xuất xứ :
                </div>
                <div class="AdminTextControl10">
                    <asp:DropDownList ID="ddlNational" Height="28px" runat="server"></asp:DropDownList>
                </div>
                <div class="AdminTextControl10" style ="padding-top:3px; font-weight:normal !important;">
                    &nbsp;&nbsp;<asp:CheckBox ID="chkState" runat="server" Text="&nbsp;&nbsp;Sử dụng"></asp:CheckBox>
                </div>
            </div>
        </div>
    </div>

    <div class="AdminItem" style ="display:table;">
        <div class="AdminLeftItem" style ="display:table;">
            <div style ="border:solid 1px red; display:table; width:100%; margin-bottom:15px; border:solid 1px #cdcdcd; text-align:center;">
                <asp:Label ID="lblImg1" Text="Ảnh minh hoạ" runat="server"></asp:Label>
            </div>
            <asp:TextBox ID="txtImage" runat="server" Width="10px" Visible="false"></asp:TextBox>
            <label class="file-upload" style="margin-top: -12px;">
                <span><strong>Upload Image</strong></span>
                <asp:FileUpload ID="upImage1" runat="server" Width="100px" CssClass="FileUploadImage"
                    Height="22px" />
            </label>
        </div>
        <div class="AdminRightItem" style ="display:table;">
            <CKEditor:CKEditorControl ID="txtContent" CssClass="editor1" runat="server" Height="230"
                Width="100%" BasePath="~/ckeditor"></CKEditor:CKEditorControl>
        </div>
    </div>

   <div class="AdminItem" style ="display:table; margin-top:5px; height:20px; line-height:20px; background-color:#f6f6f6;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="AdminMsg"></asp:Label>
        </div>
    </div>

    <div class="AdminItem" style ="margin-top:15px;">
        <div class="AdminLeftItem">
            &nbsp;&nbsp;
        </div>
        <div class="AdminRightItem">
            <asp:Button ID="btnSave" runat="server" Text="Ghi nhận" CssClass ="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Thoát" CssClass ="btn btn-default" OnClick="btnCancel_Click" />
        </div>
    </div>

</asp:Content>

