 

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TinNhan.aspx.cs" Inherits="Webservice_TinNhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
      
     <div class ="wrapper" style="width:100% !important; margin-left:0.5% ">

         <div class="alert alert-success alert-dismissable">
    
    <strong>Hộp thư của bạn</strong> 
  </div>


<% for (int i = 0; i < objTable.Rows.Count; i++)
   { %>

         <div class="alert alert-info  alert-dismissable">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">X</a>
    <strong><%=objTable.Rows[i]["Title"].ToString() %></strong> <%=objTable.Rows[i]["SMSContent"].ToString() %>
  </div>
            <% }%>
           <asp:Button CssClass="btn btn-info" runat="server" ID="btnSeen"  Text="Đã Xem" Width="150px"  OnClick="btnSeen_Click" />  
            </div>
</asp:Content>

