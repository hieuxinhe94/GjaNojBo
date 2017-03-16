<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ComposeNotice.aspx.cs" Inherits="Admin_ComposeNotice" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <div class="ComposeNotice">
        <div class="ComposeNoticeLeft">
            <div class="Compose-Message">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        SOẠN TIN NHẮN
                    </div>
                    <div class="panel-body panel-body-com-m">
                          <span> • Tiêu đề : </span>
                          <textarea rows="1" class="form-control1 control2"  runat ="server" id = "txtTilte" style="height: 50px !important; font-size:20px !important; font-family:'Comic Sans MS' ; text-decoration-color:red; "></textarea>
                        <span>• Nội dung tin nhắn : </span>
                        <textarea rows="3" class="form-control1 control2" runat ="server" id = "txtContent" style="height: 150px !important; font-size:18px !important; font-family:'Comic Sans MS' ;  "></textarea>
                        <hr />
                        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSent" CssClass="btn btn-primary" runat="server" Text="Gửi tin nhắn" OnClick="btnSent_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="ComposeNoticeRight">
            <div class="stats-info stats-info1">
                <div class="panel-heading">
                    <h4 class="panel-title">NHÓM DANH BẠ</h4>
                </div>
                <div class="panel-body panel-body2">
                    <ul class="list-unstyled">
                        <asp:DataList ID="dtlCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                            Width="100%">
                            <ItemTemplate>
                                <li><%# Eval("Name") %> &nbsp;&nbsp;<%--(<%# Eval("CountItem") %>)--%><div class="text-success pull-right">
                                    <input type="checkbox" name="ckb<%# Eval("Id") %>" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>
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
            </div>
        </div>
    </div>
</asp:Content>

