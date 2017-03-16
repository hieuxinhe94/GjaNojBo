<%@ Page Title="TÌM KIẾM" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TimKiem.aspx.cs" Inherits="TimKiem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-3 LeftMenu">
                <div class="HeaderSecssionBG">
                    DANH MỤC SẢN PHẨM
                </div>
                <% Response.Write(this.strHtmlMenu); %>
                <br />
                <br />
                <div class="HeaderSecssionProductBG">
                    SẢN PHẨM MỚI
                </div>
                <img class="ProductItem" src="../Images/Products/P1.png" alt="San pham" />
                <div class="ProductItemText">
                    ĐÈN LED TUÝP T5, 18W
                </div>
                <div class="FooterSecssionProductBG">
                    116,000 đ
                </div>
                <img class="ProductItem" src="../Images/Products/P1.png" alt="San pham" />
                <div class="ProductItemText">
                    ĐÈN LED TUÝP T5, 18W
                </div>
                <div class="FooterSecssionProductBG">
                    116,000 đ
                </div>
            </div>
            <div class="col-md-9">
                <div class="row DefaultHeader">KẾT QUẢ TÌM KIẾM &nbsp;<b style ="color:blue;">"<% Response.Write(this.strSearchKey); %>"</b></div>
                <% Response.Write(this.strHtmlBody); %>
            </div>
        </div>
    </div>
</asp:Content>

