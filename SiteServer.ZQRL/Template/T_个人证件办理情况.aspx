<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.PersonResidenceManageInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>个人就医理赔查询 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
<link rel="stylesheet" type="text/css" href="../css/global.css" />
<link rel="stylesheet" type="text/css" href="../css/main.css" />
<link href="../My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css">
<script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
<script type="text/javascript" src="../js/ScrollPic.js"></script>
<script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="../js/foldTable.js"></script>
</head>
<body style="background-color: #2f83cf;">
<form id="form1" class="form-inline" runat="server">
<stl:include file="@/include/header.html"></stl:include>
<div id="area" class="marA">
    <div style="color: #ffffff; position: absolute;top: -20px;right: 0px">当前登录用户：<%=login_name %>&nbsp;&nbsp;&nbsp;&nbsp;<a style="color: #ffffff;"  href="javasript:void(0)" onclick="logOut();return false;">退出</a></div>
<div class="top" style="height:21px;"><img src="../images/c1.jpg" width="1009" height="21"></div>
<div class="middle oh">
<div id="sonMenu">
<div class="title">个人福利查询</div>
<ul>
<li class=""><a href="PersonInfos.aspx" >个人信息查询</a></li>
<li class=""><a href="PersonSalaryInfos.aspx">薪资查询</a></li>
<li class=""><a href="PersonNationalWelfareInfos.aspx">国家福利查询</a></li>
<li class=""><a href="PersonMedicalclaimInfos.aspx">就医理赔查询</a></li>
<li class=""><a href="PersonResidenceManageInfos.aspx" class="sonMenuSelected">证件办理情况</a></li>
</ul>
</div>
<div id="content">
<%--<div class="time">
<div class="title2"><span>证件有效期：</span>有效期：2014.08.01—2015.07.31 </div>
</div>--%>
<div class="table">
<table width="715" border="0" cellspacing="0" cellpadding="0" style="background-image:url(../images/m4.png)">
  <tbody><tr>
    <th width="233" height="46" align="center" class="u5 padL20">居住证办理类型</th>
      <th width="166" align="center" class="u5"> 有效期</th>
    <th width="166" align="center" class="u5">申请日期</th>
    <th width="150" align="left" class="u5">状态</th>
    </tr>
  <asp:Repeater ID="rptContents" runat="server">
    <ItemTemplate>
<tr>
        <td align="center" class="u2"><%# Eval("paperType")%></td>
        <td align="center" class="u2"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("effectDate"))%></td>
        <td align="center" class="u2"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("applyeDate"))%></td>
        <td align="center" class="u2"><%# Eval("paperStatus")%></td></tr>
    </ItemTemplate>
      </asp:Repeater>
</tbody></table>
</div>
</div>

</div>

<div class="bottom marA"><img src="../images/c3.jpg" width="1009" height="44"></div>
</div>
        <stl:include file="@/include/footer.html"></stl:include><!--footer2 end-->
</form>
</body>
    <script src="../js/Logout.js" type="text/javascript" language="javascript"></script>
<script type="text/javascript">
    function GoPages() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        var pageNum = $("#PageNum").val();
        if (pageNum == "") {
            pageNum = "1";
        }
        window.location.href = "PersonResidenceManageInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime+"&pagesize="+pageNum+"&pagenum=5";
    }

    function searchInfo() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        window.location.href = "PersonResidenceManageInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime;
    }
</script>
</html>