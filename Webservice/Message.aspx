<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Message" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HỘP THƯ CỦA TÔI</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
  <style>

  </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class ="wrapper" style="width:75% !important; margin-left:12.5% ">
<% for (int i = 0; i < objTable.Rows.Count; i++)
   { %>
        <div class="card card-inverse card-primary mb-3 text-center" style="background-color:azure">
          <div class="card-block">
            <blockquote class="card-blockquote">
              <p><%=objTable.Rows[i]["SMSContent"].ToString() %></p>
              <footer>Gửi từ <%=objTable.Rows[i]["UserName"].ToString() %> <cite title="Source Title"><%=objTable.Rows[i]["DayCreate"].ToString() %></cite></footer>
            </blockquote>
          </div>
        </div>
            <% }%>
            </div>
    </form>
</body>
</html>
