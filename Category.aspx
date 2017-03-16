<%@ Page Title="SẢN PHẨM" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  <%
      int request;
      try {
           request = int.Parse(Request.QueryString["id"].ToString());
          }
        catch
         {
           return;
         }
       %>
    <script>
        // Đổi đường dẫn ở masterPage khi chuyển trang
        document.getElementById('element_with_setup').setAttribute('href', 'http://www.gianoibo.com/BaoGia.aspx?&id=' + <%=request.ToString()%> + '');
</script>
    <style>
        .carousel-inner .active.left { left: -33%; }
.carousel-inner .next        { left:  33%; }
.carousel-inner .prev        { left: -33%; }
.carousel-control.left,.carousel-control.right {background-image:none;}
.item:not(.prev) {visibility: visible;}
.item.right:not(.prev) {visibility: hidden;}
.rightest{ visibility: visible;}

    </style>

    <style>
        .glyphicon { margin-right:5px; }
.thumbnail
{
    margin-bottom: 20px;
    padding: 0px;
    -webkit-border-radius: 0px;
    -moz-border-radius: 0px;
    border-radius: 0px;
}

.item.list-group-item
{
    float: none;
    width: 100%;
    background-color: #fff;
    margin-bottom: 10px;
}
.item.list-group-item:nth-of-type(odd):hover,.item.list-group-item:hover
{
    background: #fafedb;
}

.item.list-group-item .list-group-image
{
    margin-right: 10px;
}
.item.list-group-item .thumbnail
{
    margin-bottom: 0px;
}
.item.list-group-item .caption
{
    padding: 9px 9px 0px 9px;
}
.item.list-group-item:nth-of-type(odd)
{
/*background: #eeeeee;*/
}

.item.list-group-item:before, .item.list-group-item:after
{
    display: table;
    content: " ";
}

.item.list-group-item img
{
    float: left;
}
.item.list-group-item:after
{
    clear: both;
}
.list-group-item-text
{
    margin: 0 0 11px;
}

    </style>

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
                                      <i class="fa fa-user fa-lg"></i>Tài khoản : <%=Session["ACCOUNT_CUSTOMER"].ToString() %>
                                      </a>
                                      </li>
                                </ul>
                         </div>
                    </div>
            </div>
           

            <div class="col-md-9">
                <div class="col-md-12">
             <%                
                           #region Kiểm tra xem đó là nhóm cha hay nhóm con
                           if (objCategory.IsParentCategory(request))
                           {
                               %>
                    <script>
                        $(document).ready(function () {
                            $('#list').click(function (event) { event.preventDefault(); $('#products .item').addClass('list-group-item'); });
                            $('#grid').click(function (event) { event.preventDefault(); $('#products .item').removeClass('list-group-item'); $('#products .item').addClass('grid-group-item'); });
                        });
                    </script>
                    <div class="container">
    <div class="well well-sm">
        <strong>CHỌN KIỂU XEM</strong>
        <div class="btn-group">
            <a href="#" id="list" class="btn btn-default btn-sm"><span class="glyphicon glyphicon-th-list">
            </span>DANH SÁCH</a> <a href="#" id="grid" class="btn btn-default btn-sm"><span
                class="glyphicon glyphicon-th"></span>LƯỚI</a>
        </div>
    </div>
    <div id="products" class="row list-group">
                    <%
                               objTableCategory = objCategory.getChildDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString(), request.ToString());
                    %>     
         <% for (int k = 0; k < objTableCategory.Rows.Count; k++) { %>

        <div class="item  col-xs-3 col-lg-3">
            <div class="thumbnail">
           <a href="../Category.aspx?Id=<%=objTableCategory.Rows[k]["Id"].ToString() %>">   
                   <img  class="group list-group-image" onerror="imgCatchError(this)" style="width:150px; height:130px ; "  src="Images/Categorys/<%=objTableCategory.Rows[k]["UrlImage"].ToString() %>" alt="<%=objTableCategory.Rows[k]["Name"].ToString() %>" />
                <div class="caption">
                    <h5 class="group inner list-group-item-heading">
                     <%=objTableCategory.Rows[k]["Name"].ToString() %></h5>
                </div>
                  </a>
            </div>
        </div>

        <%} %>
    
            </div> 
        </div>
        </div>
               <%
                     }
             else { 
            #endregion
               %>
   
            <div class="container">
            <div class="col-md-12">
                <% Response.Write(this.strHtmlBody); %>
                </div>
                </div>
                     <%
                        }
                     %>
        </div>
    </div>

       <!--Vào trang mới tải script-->
                <script>
                    // Instantiate the Bootstrap carousel
                    $('#myCarousel').carousel({
                        interval: 3600
                    });

                    $('.carousel .item').each(function () {
                        var next = $(this).next();
                        if (!next.length) {
                            next = $(this).siblings(':first');
                        }
                        next.children(':first-child').clone().appendTo($(this));

                        if (next.next().length > 0) {

                            next.next().children(':first-child').clone().appendTo($(this)).addClass('rightest');

                        }
                        else {
                            $(this).siblings(':first').children(':first-child').clone().appendTo($(this));

                        }
                    });
                </script>
        </div>
    </div>

</asp:Content>

