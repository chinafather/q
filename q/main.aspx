<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="q.main" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <title>系统主页</title>
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <script src="lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>    
    <script src="lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script> 
 
        <script type="text/javascript">
            var tab = null;
            var accordion = null;
           
            $(function () {
                $("#home").attr("src", "welcome.aspx");
                //布局
                $("#layout1").ligerLayout({ leftWidth: 190, height: '100%', heightDiff: -34, space: 4, onHeightChanged: f_heightChanged });

                var height = $(".l-layout-center").height();

                //Tab
                $("#framecenter").ligerTab({ height: height });

                //面板
                $("#accordion1").ligerAccordion({ height: height - 24, speed: null });

                $(".l-link").hover(function () {
                    $(this).addClass("l-link-over");
                }, function () {
                    $(this).removeClass("l-link-over");
                });
               

                tab = $("#framecenter").ligerGetTabManager();
                accordion = $("#accordion1").ligerGetAccordionManager();
                
                $("#pageloading").hide();

            });
            function f_heightChanged(options) {
                if (tab)
                    tab.addHeight(options.diff);
                if (accordion && options.middleHeight - 24 > 0)
                    accordion.setHeight(options.middleHeight - 24);
            }
            function f_addTab(tabid, text, url) {
                tab.addTabItem({ tabid: tabid, text: text, url: url });
            }


     </script> 
<style type="text/css"> 
    body,html{height:100%;}
    body{ padding:0px; margin:0;   overflow:hidden;}  
    .l-link{ display:block; height:26px; line-height:26px; padding-left:10px; text-decoration:underline; color:#333;}
    .l-link2{text-decoration:underline; color:white; margin-left:2px;margin-right:2px;}
    .l-layout-top{background:#102A49; color:White;}
    .l-layout-bottom{ background:#E5EDEF; text-align:center;}
    #pageloading{position:absolute; left:0px; top:0px; background:white url('loading.gif') no-repeat center; width:100%; height:100%;z-index:99999;}
    .l-link{ display:block; line-height:22px; height:22px; padding-left:16px;border:1px solid white; margin:4px;}
    .l-link-over{ background:#FFEEAC; border:1px solid #DB9F00;} 
    .l-winbar{ background:#2B5A76; height:30px; position:absolute; left:0px; bottom:0px; width:100%; z-index:99999;}
    .space{ color:#E7E7E7;}
    /* 顶部 */ 
    .l-topmenu{ margin:0; padding:0; height:31px; line-height:31px; background:url('lib/images/top.jpg') repeat-x bottom;  position:relative; border-top:1px solid #1D438B;  }
    .l-topmenu-logo{ color:#E7E7E7; padding-left:35px; line-height:26px;background:url('lib/images/topicon.gif') no-repeat 10px 5px;}
    .l-topmenu-welcome{  position:absolute; height:24px; line-height:24px;  right:30px; top:2px;color:#070A0C;}
    .l-topmenu-welcome a{ color:#E7E7E7; text-decoration:underline} 

 </style>
</head>
<body style="padding:0px;background:#EAEEF5;">
    <form id="form1" runat="server">
    <div id="pageloading"></div>  
<div id="topmenu" class="l-topmenu">
    <div class="l-topmenu-logo"><%=systemName %></div>
    <div class="l-topmenu-welcome">
         <span class="space">当前用户：</span><a href="###" class="l-link2"><%=HttpUtility.UrlDecode(Request.Cookies["user"].Value)%></a>&nbsp;&nbsp;<a href="login.aspx">注销</a>&nbsp;&nbsp;<a href="系统操作手册.doc">使用说明</a>
         
    </div> 
</div>
  <div id="layout1" style="width:99.2%; margin:0 auto; margin-top:4px; "> 
        <div position="left"  title="主要菜单" id="accordion1"> 
                     <div title="问卷管理" class="l-scroll">
                          
                         <a class="l-link" href="javascript:f_addTab('listpage1','问卷管理','wqlist.aspx')" style="<%=(secstr.IndexOf("[问卷管理]") > -1)?"":"display:none;" %>">问卷管理</a> 
                       
                    </div>
                    
                     <div title="报表管理">
                    <div style=" height:7px;"></div>
                          <a class="l-link" href="javascript:f_addTab('listpage','领用统计','report/lyreport.aspx')" style="<%=(secstr.IndexOf("[报表]") > -1)?"":"display:none;" %>">领用统计</a> 
                         <a class="l-link" href="javascript:f_addTab('yjdjbquery','财产物品验收登记','report/yjdjbquery.aspx')" style="<%=(secstr.IndexOf("[报表]") > -1)?"":"display:none;" %>">财产物品验收登记</a> 
                         <a class="l-link" href="javascript:f_addTab('yhpquery','易耗品汇总','report/yhpquery.aspx')" style="<%=(secstr.IndexOf("[报表]") > -1)?"":"display:none;" %>">易耗品汇总</a> 
<%--                         <a class="l-link" href="javascript:f_addTab('listpage','领用','rc/ly.aspx')" style="<%=(secstr.IndexOf("[报表]") > -1)?"":"display:none;" %>">物品明细统计</a> 
                         <a class="l-link" href="javascript:f_addTab('listpage','领用','rc/ly.aspx')" style="<%=(secstr.IndexOf("[报表]") > -1)?"":"display:none;" %>">库存统计</a> 
                         <a class="l-link" href="javascript:f_addTab('listpage','领用','rc/ly.aspx')" style="<%=(secstr.IndexOf("[报表]") > -1)?"":"display:none;" %>">盘点统计</a> --%>
                    </div> 
                    <div title="系统管理">
                    <div style=" height:7px;"></div>
                         
                         <a class="l-link" href="javascript:f_addTab('uselist','人员','userlist.aspx')" style="<%=(secstr.IndexOf("[人员]") > -1)?"":"display:none;" %>">人员</a> 
                         <%--<a class="l-link" href="javascript:f_addTab('listpage','领用','rc/ly.aspx')" style="<%=(secstr.IndexOf("[领用]") > -1)?"":"display:none;" %>">权限</a> --%>
                        <a class="l-link" href="javascript:f_addTab('xggrmm','修改个人密码','password.aspx')"  >修改个人密码</a> 
                        
                    </div>   
        </div>
        <div position="center" id="framecenter"> 
            <div tabid="home" title="我的主页" style="height:300px" >
                <iframe frameborder="0" name="home" id="home" src="welcome.aspx"></iframe>
            </div> 
        </div> 
        
    </div>
    <div  style="height:32px; line-height:32px; text-align:center;">
            Copyright © 
    </div>
    <div style="display:none"></div>
    </form>
</body>
</html>
