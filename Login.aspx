<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>ĐĂNG NHẬP - GIANOIBO.COM</title>
    <link href="../css/TVSStyle.css" rel="stylesheet" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <style>
        .panel-heading {
            padding: 5px 15px;
        }

        .panel-footer {
            padding: 1px 15px;
            color: #A0A0A0;
        }

        .profile-img {
            width: 96px;
            height: 96px;
            margin: 0 auto 10px;
            display: block;
            -moz-border-radius: 50%;
            -webkit-border-radius: 50%;
            border-radius: 50%;
        }

        .TitleLogin {
            text-align: center;
            margin-bottom: 20px;
            font-weight: bold;
            font-size: 15px;
        }

        .lblMsg {
            text-align: center;
            font-size: 14px;
            color: red;
        }

        .TVSCombobox {
            width: 91% !important;
            min-height: 28px;
            border: solid 1px #e8e8e8;
            line-height: 32px;
            height: 32px;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <div class="container" style="margin-top: 40px">
            <div class="row">
                <div class="col-sm-6 col-md-4 col-md-offset-4">
                    <div class="TitleLogin">
                        GIANOIBO.COM<br />
                        <%Response.Write(this.strHtmlIntro1); %>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="text-align: center;">
                            <strong>Đăng nhập vào hệ thống</strong>
                        </div>
                        <div class="panel-body">
                            <fieldset>
                                <div class="row">
                                    <div class="center-block">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-10  col-md-offset-1 ">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-user"></i>
                                                </span>
                                                <input class="form-control" placeholder="Tài khoản đăng nhập" runat="server" id="txtUserName" type="text" autofocus="autofocus" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-lock"></i>
                                                </span>
                                                <input class="form-control" placeholder="Mật khẩu" name="password" runat="server" type="password" id="txtPassWord" value="" />
                                            </div>
                                        </div>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="form-group" style="text-align: center; margin-top: 10px;">
                                                    <asp:Label ID="lblMsg" runat="server" Text="-:-" CssClass="lblMsg"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button runat="server" CssClass="btn btn-lg btn-primary btn-block" Text="Đăng nhập" ID="btnLogin" OnClick="btnLogin_Click"></asp:Button>
                                                </div>
                                                </div>                           
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                            </fieldset>
                        </div>
                        <div class="panel-footer ">
                            <%Response.Write(this.strHtmlIntro2); %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
