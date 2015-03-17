<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.PersonSalaryInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>国家福利查询 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
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
<li class=""><a href="PersonSalaryInfos.aspx" class="sonMenuSelected">薪资查询</a></li>
<li class=""><a href="PersonNationalWelfareInfos.aspx">国家福利查询</a></li>
<li class=""><a href="PersonMedicalclaimInfos.aspx">就医理赔查询</a></li>
<li class=""><a href="PersonResidenceManageInfos.aspx">证件办理情况</a></li>
</ul>
</div>
<div id="content">
<div class="searchBox2">
  <table width="645" border="0" align="center" cellpadding="0" cellspacing="0">
    <tbody><tr>
      <td width="65" height="40" align="center">开始时间</td>
      <td width="171" align="center"><span class="searchBox fl">
          <asp:TextBox runat="server" name="lblStartTime" id="lblStartTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"></asp:TextBox>
      </span></td>
      <td width="81" align="center">结束时间</td>
      <td width="179" align="center"><span class="searchBox fl">
          <asp:TextBox runat="server" name="lblEndTime" id="lblEndTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"></asp:TextBox>
      </span></td>
      <td width="149" align="left"><a href="javascript:void(0)" onclick="searchInfo()"><img src="../images/bb1.jpg" width="71" height="23"></a></td>
    </tr>
  </tbody></table>
</div>
<div class="table viewlist">
<div class="scroll">
<table id="tableList" width="715" border="0" cellspacing="0" cellpadding="0">
  <tbody><tr>
             <th width="715" height="46" align="center" class="u1">编号</th>
    <th width="715" height="46" align="center" class="u1">日期</th>
    </tr>
      <asp:Repeater ID="rptContents" runat="server" OnItemDataBound="rptContents_ItemDataBound">
    <ItemTemplate>
        <tr class="u6">
        <td  align="center" class="ico">
<%# this.rptContents.Items.Count + 1%> 
            <input type="hidden" value="<%# Eval("id")%>" /> 
</td>
        <td  align="center">
           <%#   String.Format("{0:yyyy-MM-dd} ", Eval("date"))%></td></tr>
        <tr class="u7">
            <td colspan="6" align="center" valign="middle"><div class="kk">
     <table width="719" border="0" cellspacing="0" cellpadding="0" style="background-image:none;">

        <tbody>
            <asp:Repeater runat="server" ID="rptItemContents">
                            <ItemTemplate>
                                <tr class="u4">
                                     <td align="center">
                                     <%# Eval("ItemName")%></td><td>
                                     <%# Eval("ItemValue")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
      </tbody></table>
    </div></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
</tbody></table>
</div>

<div id="page">
                    <div class="oh page">
                        <%=pageHtml %>
                    </div>
                </div>
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
        window.location.href = "SalaryInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime+"&pagesize="+pageNum+"&pagenum=5";
    }

    function searchInfo() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        window.location.href = "SalaryInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime;
    }
</script>
</html>