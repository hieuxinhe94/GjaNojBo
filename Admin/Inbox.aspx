<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="Admin_Inbox" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="AdminHeaderItem">
        TIN NHẮN ĐÃ GỬI
     <span style="float:right">XÓA TIN NHẮN TỪ  : <asp:TextBox ID="txtTimeDelete" type="number" runat="server"  Text="3" Width="50px"></asp:TextBox> (ngày) 
         <asp:Button runat="server" ID="btnSaveTime" Text="THỰC HIỆN"  OnClick="btnSaveTime_Click" /> </span>   
    </div>

    <div style="width: 100%;">
        <table class="DataListTableHeader">
            <tr style="height: 40px;">
                 <td class="DataListTableHeaderTdItemJustify" style="width: 15%;">Tiêu đề 
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 35%;">Tin nhắn
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 15%;">Khách hàng
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 13%;">Ngày gửi
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 8%;">TRẠNG THÁI 
                </td>
                  <td class="DataListTableHeaderTdItemCenter" style="width: 18%;"> QUẢN LÝ
                </td>
            </tr>
        </table>
    </div>
    <asp:DataList ID="dtlPartnerSMS" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" Width="100%" OnDeleteCommand="dtlPartnerSMS_DeleteCommand" DataKeyField="Id">
        <ItemTemplate>
            <table class="DataListTable">
                <tr style="height: 40px;">
                     <td class="DataListTableTdItemJustify" style="width: 15%;">
                        <%# Eval("Title") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 35%; font-size:12px;   ">
                        <%# Eval("SMSContent") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 15%;">
                        <%# Eval("PartnerName") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 12%; font-size:13px;">
                        <%# Eval("DayCreate") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 8%; font-size:11px;" >
                        <%# Eval("StateName") %>
                    </td>
                     <td class="DataListTableTdItemCenter" style="width: 18%;">
                                    &nbsp;&nbsp;&nbsp;
                            <asp:ImageButton runat="server" ID="Delete" ImageUrl="../images/delete.png" CommandName="delete" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>

    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px; background-color: #daf2a5; height: 26px;"
        id="tblABC" runat="server">
        <tr>
            <td>
                <cc1:CollectionPager ID="cpPartnerSMS" runat="server" BackText="" FirstText="Đầu"
                    ControlCssClass="ProductPage" LabelText="" LastText="Cuối" NextText="" UseSlider="true"
                    ResultsFormat="" BackNextLinkSeparator="" ResultsLocation="None" BackNextLocation="None"
                    PageNumbersSeparator="&nbsp;">
                </cc1:CollectionPager>
            </td>
        </tr>
    </table>
</asp:Content>

