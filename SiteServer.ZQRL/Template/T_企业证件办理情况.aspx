<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.CompanyResidenceManageInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>企业证件办理情况 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
<link rel="stylesheet" type="text/css" href="../css/global.css" />
<link rel="stylesheet" type="text/css" href="../css/main.css" />
<link href="../My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css">
<script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
<script type="text/javascript" src="../js/ScrollPic.js"></script>
<script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
</head>
<body style="background-color: #2f83cf;">
<form id="form1" class="form-inline" runat="server">
<stl:include file="@/include/header.html"></stl:include>
<div id="area" class="marA">
    <div style="color: #ffffff; position: absolute;top: -20px;right: 0px">当前公司：<%=login_company %>&nbsp;&nbsp;&nbsp;&nbsp;当前登录用户：<%=login_name %>&nbsp;&nbsp;&nbsp;&nbsp;<a style="color: #ffffff;"  href="javasript:void(0)" onclick="logOut();return false;">退出</a></div>
<div class="top" style="height:21px;"><img src="../images/c1.jpg" width="1009" height="21"></div>
<div class="middle oh">
<div id="sonMenu">
<div class="title">企业福利查询</div>
<ul>
<li class=""><a href="EmployeeInfos.aspx">员工信息</a></li>
<li class=""><a href="CompanySalaryInfosByPerson.aspx">员工薪酬</a></li>
<li class=""><a href="CompanyNationalWelfareInfos.aspx" >国家福利查询</a></li>
<li class=""><a href="CompanyMedicalclaimInfos.aspx">就医理赔查询</a></li>
<li class=""><a href="CompanyResidenceManageInfos.aspx"  class="sonMenuSelected">证件办理情况</a></li>
<li class=""><a href="CompanyFundrateInfo.aspx">全国福利政策</a></li>
<li class=""><a href="#">政策法规查询</a></li>
</ul>
</div>
<div id="content">
    <div class="kf">客服：<asp:Label ID="lblSupportStaff" runat="server"></asp:Label></div>
    <div class="searchBox2" style=" margin-top:10px;">
  <table width="704" border="0" align="center" cellpadding="0" cellspacing="0">
    <tbody><tr>
      <td width="124" height="40" align="center">申请日期 起始</td>
      <td width="90" align="center"><span class="searchBox fl">
        <asp:TextBox runat="server" name="lblStartTime" id="lblStartTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Width="90px"></asp:TextBox>
      </span></td>
      <td width="70" align="center">终止</td>
      <td width="128" align="center"><span class="searchBox fl">
        <asp:TextBox runat="server" name="lblEndTime" id="lblEndTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Width="90px"></asp:TextBox>
      </span></td>
      <td width="211" align="left"><span class="searchBox fl">
        <asp:TextBox runat="server" name="lblNameOrIdCard" ID="lblNameOrIdCard" placeholder="姓名或证件号码"></asp:TextBox>
      </span></td>
      <td width="81" align="center"><a href="javascript:void(0)" onclick="searchInfo()"><img src="../images/bb4.jpg" width="54" height="21"></a></td>
    </tr>
  </tbody></table>
</div>
<div class="table viewlist">
    <table width="715" border="0" cellspacing="0" cellpadding="0" id="tableList">
  <tbody><tr>
    <th width="101" height="46" align="center" class="u1" style="padding-left:10px;"><input type="hidden" name="fid" id="fid" value="vvv1">
      序号 </th>
    <th width="174" align="center" class="u1">姓名 </th>
    <th width="126" align="center" class="u1">性别 </th>
    <th width="200" align="center" class="u1">身份证</th>
    <th width="114" align="center" class="u1">有效期&nbsp;      </th>
    </tr><asp:Repeater ID="rptContents" runat="server">
    <ItemTemplate>
  <tr class="u6">
    <td align="center" valign="middle" class="ico" style="width:80px; padding-left:10px;" id="icovvv1"> <%# this.rptContents.Items.Count + 1%> </td>
    <td align="center" valign="middle"> <%# Eval("personName")%></td>
    <td align="center" valign="middle"> <%# Eval("sex")%></td>
    <td align="center" valign="middle"> <%# Eval("idcard")%> </td>
    <td align="center" valign="middle"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("effectDate"))%> </td>
  </tr>
  <tr class="u7" id="vvv1" style="display: none;">
    <td colspan="5" align="center" valign="middle"><div class="scroll">
      <table width="715" border="0" cellspacing="0" cellpadding="0" style=" background-image:none; margin-top:3px;">
        <tbody><tr>
          <td width="317" class="u11">居住证办理类型</td>
          <td width="237" align="center" class="u11">日期</td>
          <td width="161" class="u11">状态</td>
        </tr>
        <tr>
          <td class="u12"><%# Eval("paperType")%></td>
          <td align="center" class="u12"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("applyeDate"))%></td>
          <td class="u12"><%# Eval("paperStatus")%></td>
        </tr>
      </tbody></table>
    </div></td>
  </tr></ItemTemplate></asp:Repeater>
</tbody></table>
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
    <script src="../js/onkeyupBtn.js" type="text/javascript" language="javascript"></script>
<script type="text/javascript">
    function GoPages() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        var NameOrIdCard = $("#lblNameOrIdCard").val();
        var pageNum = $("#PageNum").val();
        if (pageNum == "") {
            pageNum = "1";
        }
        window.location.href = "CompanyResidenceManageInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime + "&NameOrIdCard=" + NameOrIdCard + "&pagesize=" + pageNum + "&pagenum=5";
    }

    function searchInfo() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        var NameOrIdCard = $("#lblNameOrIdCard").val();
        window.location.href = "CompanyResidenceManageInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime + "&NameOrIdCard=" + NameOrIdCard;
    }
</script>
</html>