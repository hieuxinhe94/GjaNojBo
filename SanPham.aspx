<%@ Page Title="TRANG CHỦ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="SanPham.aspx.cs" Inherits="SanPham" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-3 LeftMenu">
                <div class="HeaderSecssionBG">
                    DANH MỤC SẢN PHẨM
                </div>
                <div class="nav-side-menu">
                    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
                        <i class="fa fa-bars fa-2x toggle-btn" data-toggle="collapse" data-target="#menu-content"></i>
                            <div class="menu-list">
                                <ul id="menu-content" class="menu-content collapse out">
                                    <% Response.Write(this.strHtmlMenu); %>
                                    <li>
                                      <a href="#">
                                      <i class="fa fa-user fa-lg"></i> You
                                      </a>
                                      </li>
                                     <li>
                                      <a href="#">
                                      <i class="fa fa-users fa-lg"></i> Cài đặt
                                      </a>
                                    </li>
                                </ul>
                         </div>
                    </div>
                <br />
                <br />
                <div class="HeaderSecssionProductBG">
                    SẢN PHẨM MỚI
                </div>
                <% Response.Write(this.strHtmlNewProduct); %>
            </div>
            <div class="col-md-9">
                <% Response.Write(this.strHtmlBody); %>
                <%--<div class ="row DefaultRow">
                    <div class="col-md-3" style ="padding:0px;">
                        <div class="DefaultProductItemBorder">
                            <a href ="#"><img src ="Images/Products/P1.png" alt =""></a>
                            <p>
                               <a href ="#"> Đèn led tuýp T5, 18W</a>
                            </p>
                            <hr />
                            <div class ="DefaultProductItemPrice">
                                116.000 đ
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3" style ="padding:0px;">
                        <div class="DefaultProductItemBorder">
                            <a href ="#"><img src ="Images/Products/P1.png" alt =""></a>
                            <p>
                               <a href ="#"> Đèn led tuýp T5, 18W</a>
                            </p>
                            <hr />
                            <div class ="DefaultProductItemPrice">
                                116.000 đ
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3" style ="padding:0px;">
                       <div class="DefaultProductItemBorder">
                            <a href ="#"><img src ="Images/Products/P1.png" alt =""></a>
                            <p>
                               <a href ="#"> Đèn led tuýp T5, 18W</a>
                            </p>
                            <hr />
                            <div class ="DefaultProductItemPrice">
                                116.000 đ
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3" style ="padding:0px;">
                        <div class="DefaultProductItemBorder">
                            <a href ="#"><img src ="Images/Products/P1.png" alt =""></a>
                            <p>
                               <a href ="#"> Đèn led tuýp T5, 18W</a>
                            </p>
                            <hr />
                            <div class ="DefaultProductItemPrice">
                                116.000 đ
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

</asp:Content>
