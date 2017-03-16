<%@ Page Title="SẢN PHẨM" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Detailt.aspx.cs" Inherits="Detailt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
                           
                               </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <%--    <% Response.Write(this.strHtmlMenu); %>--%>
            </div>
            <div class="col-md-9">
                <div class="row">
                    <div class="col-md-5 ProductDetailt">
                        <% Response.Write(this.strHtmlImage); %>
                    </div>
                    <div class="col-md-7">
                        <div class="ProductDetailtName">
                            <% Response.Write(this.strHtmlName); %>
                        </div>
                        <div class="ProductDetailtPrice">
                            <% Response.Write(this.strHtmlPrice); %>
                        </div>
                        <div class="ProductDetailtSpecification">
                            Quy cách: <% Response.Write(this.strHtmlSpecification); %>
                        </div>
                        <div class="ProductDetailtCapacity">
                            Công suất: <% Response.Write(this.strHtmlCapacity); %>
                        </div>
                        <div class="ProductDetailtGuarantee">
                            Bảo hành: <% Response.Write(this.strHtmlGuarantee); %> tháng
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <h4>Chi tiết sản phẩm</h4>
                </div>
                <div class="row ProductDetailtIntroContent">
                    <% Response.Write(this.strHtmlIntroContent); %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

