<%@ Page Title="BÁO GIÁ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Theodoigia.aspx.cs" Inherits="Theodoigia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        iframe {
            width: 100% !important;
            height: 1210px !important;
            max-width: 880px !important;
            max-height: 2000px !important;
        }
    </style>
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
                <% Response.Write(this.strHtmlNewProduct); %>
            </div>
            <div class="col-md-9" style="padding-right: 0px; margin-right: 0px; margin-right:-15px; margin-left:-15px;">
                <div class="row DefaultHeader" style ="margin:0px;">
                    <div class="BG1">
                       SẢN PHẨM THAY ĐỔI GIÁ
                    </div>
                    <div class="BG2">
                    </div>
                </div>

                <div class="table-responsive">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Mã sản phẩm</th>
                                <th>Tên sản phẩm</th>
                                <th style ="text-align:right;">Giá hiện tại</th>
                                <th style ="text-align:right;">Giá cũ</th>
                                <th>Ngày thay đổi</th>
                                <th>Diễn giải</th>
                            </tr>
                        </thead>
                        <% Response.Write(this.strHtmlBody); %>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

