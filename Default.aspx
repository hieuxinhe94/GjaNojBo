<%@ Page Title="TRANG CHỦ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
  

    <!-- BAT DAU PHAN QUANG CAO -->
    <%-- <div id="boxes">
        <div style="top: 199.5px; left: 551.5px; display: none;" id="dialog" class="window">
            Tiêu đề quảng cáo
    <div id="lorem">
        <p>Nội dung quảng cáo</p>
    </div>
        </div>
        <div style="width: 1478px; font-size: 32pt; color: white; height: 602px; display: none; opacity: 0.8;" id="mask"></div>
    </div>--%>
    <style>
                        #ul_category li {
    list-style-type:none;
    float:left;
    width:18%;
   
}
#ul_category:hover li {
    width:12%;
    display:table;
}
#ul_category:hover li figure {
    position:relative;
}
#ul_category:hover li figure figcaption {
    position:absolute;
    top:30%;
    right:0;
    display:none;
}
#ul_category li:hover  {
    width:200px !important ;
     height:200px !important;
}
#ul_category li img:hover   {
    width:200px !important ;
     height:200px !important;
    
        
}
#ul_category li img:hover  ~ #img_title  {
   display:none;
}
#ul_category li:hover figure figcaption {
    display:block;
}
#ul_category,
#ul_category:hover,
#ul_category li,
#ul_category li:hover {
    transition:all ease-out 0.5s;
}
                    </style>
    <% Response.Write(this.strAds); %>
    <!-- KET THUC PHAN QUANG CAO  -->
    <div id="mobile_menu_dropdown" style="display: none">
        <%Response.Write(strHtmlMenu2); %>
    </div>
    <div style="clear: both"></div>

    <div class="container">
        <div class="row">
            <div class="col-md-3 LeftMenu">
                <div class="HeaderSecssionBG">
                    DANH MỤC SẢN PHẨM
                </div>
                <!----->
                <div class="nav-side-menu">
                    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
                    <i class="fa fa-bars fa-2x toggle-btn" data-toggle="collapse" data-target="#menu-content"></i>
                    <div class="menu-list">
                        <ul id="menu-content" class="menu-content collapse out">
                            <% Response.Write(this.strHtmlMenu); %>
                            <li>
                               
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
            <div id="mainProduct" class="col-md-9">

                <div class="row ">   CÁC NHÓM SẢN PHẨM CỦA BẠN </div>


                <style>
                    

                </style>
              
                  <div class="container">
	<div class="row">
		<div id="my-carousel" class="carousel" data-ride="carousel" data-interval="false">
		 
			<div class="col-sm-3">
				
                  <script>

                      $('#my-carousel .goof').click(function (e) {
                          $('#soln-slide-1 .goof').removeClass('activate');
                          $(this).addClass('activate');
                      });

                </script>

                    <%  Partners objPartner = new Partners();
                        System.Data.DataTable objTbl = new System.Data.DataTable();
                        objTbl = objPartner.getPartnerDataParentCategory(Session["ACCOUNT_ID"].ToString());
                        int x = objTbl.Rows.Count;
                        for (int i = 0; i <x; i+=1)
                        {
                           
                    %>
                <div class="row">
					<div class="col-xs-10">
                        <%if(i==0) {%>
                        <div class="goof activate" data-target="#my-carousel" data-slide-to="0">
                        <% } else{ %>
                              <div class="goof"  data-target="#my-carousel" data-slide-to="<%=i.ToString() %>">
						<%} %>
                                    <img onerror="imgCatchError(this)" style="width:100%; height:110px ; margin-top: -50px;"  src="Images/Categorys/<%=objTbl.Rows[i]["UrlImage"].ToString() %>" alt="<%=objTbl.Rows[i]["Name"].ToString() %>" />
						<a href="../Category.aspx?Id=<%=objTbl.Rows[i]["Id"].ToString() %>">   
                            <h5 style="  z-index: 100; position: absolute; color: white; font-size: 12px;font-weight: bold; color:black; left: 20px;top: 50px; background-color:whitesmoke;">
							<%=objTbl.Rows[i]["Name"].ToString() %>
							</h5>
                            </a>	
						</div>
					</div>
				
                      </div>
				<%  } %>
		  	</div>
			<div class="col-sm-9" style="background:#fff">
			  	<div class="carousel-inner" role="listbox">
                    
                      <% for (int i = 0; i <x; i++)
                        {
                      %>

                      <%if(i==0) {%>
                     
				    <div class="item active gaf">
                        <% } else{ %>
                               <div class="item gaf">
						<%} %>

				
				      <h3 style="background-color: #FAFEDB; font-size:13px; text-decoration: none; color: #4ab91c;">
                          
                           
                    <%   
                             System.Data.DataTable objChildCategory = new System.Data.DataTable();
                             objChildCategory = this.objCategory.getChildDataAsLeftMenuVer2(Session["ACCOUNT_ID"].ToString(), objTbl.Rows[i]["Id"].ToString());
                    %>
                           <a href="../Category.aspx?Id=<%=objTbl.Rows[i]["Id"].ToString() %>"> <%=objTbl.Rows[i]["Name"].ToString() %></a><span class="badge"><%=objChildCategory.Rows.Count%></span> </h3>
                    <%for(int j = 0 ; j < objChildCategory.Rows.Count ; j ++) %>
                    <%{ %>
                                   <div class="col-xs-3">
                    <div class="goof" style="width:145px; height:150px; background-color:#fff !important">

                 <a href="../Category.aspx?Id=<%=objChildCategory.Rows[j]["Id"].ToString() %>"> 
                      <img onerror="imgCatchError(this)" style="width:100%; height:130px ; margin-top: -40px;"  src="Images/Categorys/<%=objChildCategory.Rows[j]["UrlImage"].ToString() %>" alt="<%=objChildCategory.Rows[j]["Name"].ToString() %>" />
                   
                  <p style="font-size:12px">   <%=objChildCategory.Rows[j]["Name"].ToString() %></p>  
                           </a> 
                        </div>
                                     </div>
                    <%} %>
				    </div>

                      <%} %>



		  		</div>
			</div>
		  
		</div>
	</div>
</div>

               
                    



                    </div>
                </div>
                <%--      <% Response.Write(this.strHtmlBody); %>--%>
            </div>
        </div>

</asp:Content>
