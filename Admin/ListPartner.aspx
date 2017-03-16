<%@ Page Title="KHÁCH HÀNG, ĐỐI TÁC" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ListPartner.aspx.cs" Inherits="Admin_ListPartner" %>

<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
   
    <style>
body {
  font-family: 'PT Sans', sans-serif;
  font-size: 13px;
  font-weight: 400;
  color: #4f5d6e;
  position: relative;
  background: rgb(26, 49, 95);
  background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, rgba(26, 49, 95, 1)), color-stop(10%, rgba(26, 49, 95, 1)), color-stop(24%, rgba(29, 108, 141, 1)), color-stop(37%, rgba(41, 136, 151, 1)), color-stop(77%, rgba(39, 45, 100, 1)), color-stop(90%, rgba(26, 49, 95, 1)), color-stop(100%, rgba(26, 49, 95, 1)));
  filter: progid: DXImageTransform.Microsoft.gradient( startColorstr='#1a315f', endColorstr='#1a315f', GradientType=0);
}

.body-wrap {
  min-height: 700px;
}

.body-wrap {
  position: relative;
  z-index: 0;
}

.body-wrap:before,
.body-wrap:after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  z-index: -1;
  height: 260px;
  background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, rgba(26, 49, 95, 1)), color-stop(100%, rgba(26, 49, 95, 0)));
  background: linear-gradient(to bottom, rgba(26, 49, 95, 1) 0%, rgba(26, 49, 95, 0) 100%);
  filter: progid: DXImageTransform.Microsoft.gradient( startColorstr='#1a315f', endColorstr='#001a315f', GradientType=0);
}

.body-wrap:after {
  top: auto;
  bottom: 0;
  background: linear-gradient(to bottom, rgba(26, 49, 95, 0) 0%, rgba(26, 49, 95, 1) 100%);
  filter: progid: DXImageTransform.Microsoft.gradient( startColorstr='#001a315f', endColorstr='#1a315f', GradientType=0);
}

nav {
  margin-top: 60px;
  box-shadow: 5px 4px 5px #000;
}
    </style>

    <div class="AdminHeaderItem">
        KHÁCH HÀNG, ĐỐI TÁC
    </div>
    <div style="width: 100%;">
        <table class="DataListTableHeader">
            <tr style ="height:40px;">
                <td class="DataListTableHeaderTdItemTT" style="width: 3%;">#
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 30%;">Khách hàng
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 10%;">Tài khoản
                </td>
                <td class="DataListTableHeaderTdItemJustify" style="width: 20%;">Zalo / Facebook
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 18%;">Nhóm hàng
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 12%;">Truy cập cuối
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Sửa
                </td>
                <td class="DataListTableHeaderTdItemCenter" style="width: 3%;">Xóa
                </td>
            </tr>
        </table>
    </div>

    <asp:DataList ID="dtlCategory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
        Width="100%">

        <ItemTemplate>
             <script>       // Dư thừa , nhưng k quan trọng lắm 
                 $('ul.nav li.dropdown').hover(function () {
                     $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(500);
                 }, function () {
                     $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(500);
                 });
             </script>
            <table class="DataListTable">
                <tr style ="height:20px;">
                    <td class="DataListTableTdItemTT" style="width: 3%;">
                        <%# Eval("TT") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 30%;">
                       
            <ul class="nav navbar-nav" style="background-color:azure">
            <li class="dropdown">
              <a href="#" class="dropdown-toggle" data-toggle="dropdown"> <%# Eval("Name") %> <b class="caret"></b></a>
              <ul class="dropdown-menu">
                <li><a href="#">Địa chỉ :  <%# Eval("Address") %></a></li>
                <li><a href="#">ĐT :  <%# Eval("Phone") %></a></li>
                <li><a href="#">Email :  <%# Eval("Email") %></a></li>
                <li class="divider"></li>
                <li><a href="#">Ngày gia nhập :  <%# Eval("DayCreate") %></a></li>
                <li class="divider"></li>
              </ul>
            </li>
          </ul>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 10%;">
                        <%# Eval("Account") %>
                    </td>
                    <td class="DataListTableTdItemJustify" style="width: 20%;">
                        <img src ="../images/<%# Eval("SocialNetworkType") %>.png">&nbsp;<%# Eval("SocialNetwork") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 18%;">
                        <a href="CategoryPartner.aspx?id=<%# Eval("Id") %>"><%# Eval("CountCategory") %></a>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 12%; font-size:12px;">
                        <%# Eval("LastAccess") %>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;">
                        <a href="AddPartner.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/Edit.png" alt=""></a>
                    </td>
                    <td class="DataListTableTdItemCenter" style="width: 3%;">
                        <a href="DelPartner.aspx?id=<%# Eval("Id") %>">
                            <img src="../Images/delete.png" alt=""></a>
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
    <a href="AddPartner.aspx">
        <input type="text" value="Thêm mới" class="btn btn-primary" style="width: 90px !important;" /></a>
</asp:Content>

