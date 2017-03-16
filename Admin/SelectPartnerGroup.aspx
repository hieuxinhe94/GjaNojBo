<%@ Page Title="CHỌN KHÁCH HÀNG, ĐỐI TÁC" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="SelectPartnerGroup.aspx.cs" Inherits="Admin_SelectPartnerGroup" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
  
    <asp:ScriptManager runat="server">
        
    </asp:ScriptManager>
    <div class="AdminHeaderItem">
        KHÁCH HÀNG, ĐỐI TÁC
    </div>

    <div style="width: 100%;">
        <table class="DataListTableHeader" border="0">
            <tr style="height: 40px;">
                <td class="DataListTableHeaderTdItemTT" style="width: 4%;">All <input name="select-all" id="select-all" type="checkbox" title="" />
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 63%;">Khách hàng
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 20%;">Tài khoản
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 13%;">Truy cập cuối
                </td>
            </tr>
        </table>
    </div>
    <asp:DataList ID="dtlCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
        Width="100%">
        <ItemTemplate>
              <script>
                  $('#select-all').click(function () {
                 
                      $(' input[type="checkbox"]').prop('checked', this.checked)
                  })
    </script>
            <table class="DataListTable" border="0">
                <tr style="height: 40px;">
                    <td class="DataListTableTdItemCenter" style="width: 4%;">
                        <input type="checkbox" name="ckb<%# Eval("Id") %>" id="ckb<%# Eval("Id") %>" />
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 63%;">
                        <%# Eval("Name") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 20%;">
                        <%# Eval("Account") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 13%; font-size: 12px;">
                        <%# Eval("LastAccess") %>
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
    <br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Button ID="btnSelect" CssClass="btn btn-primary" runat="server" OnClick = "btnSelect_Click" Text="Chọn" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

