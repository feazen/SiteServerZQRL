<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.CompanyNationalWelfareInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>企业国家福利查询 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
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
    <div style="color: #ffffff; position: absolute;top: -20px;right: 0px">当前公司：<%=login_company %>&nbsp;&nbsp;&nbsp;&nbsp;当前登录用户：<%=login_name %>&nbsp;&nbsp;&nbsp;&nbsp;<a style="color: #ffffff;"  href="javasript:void(0)" onclick="logOut();return false;">退出</a></div>
<div class="top" style="height:21px;"><img src="../images/c1.jpg" width="1009" height="21"></div>
<div class="middle oh">
<div id="sonMenu">
<div class="title">企业福利查询</div>
<ul>
<li class=""><a href="EmployeeInfos.aspx">员工信息</a></li>
<li class=""><a href="CompanySalaryInfosByPerson.aspx">员工薪酬</a></li>
<li class=""><a href="CompanyNationalWelfareInfos.aspx" class="sonMenuSelected">国家福利查询</a></li>
<li class=""><a href="CompanyMedicalclaimInfos.aspx">就医理赔查询</a></li>
<li class=""><a href="CompanyResidenceManageInfos.aspx">证件办理情况</a></li>
<li class=""><a href="CompanyFundrateInfo.aspx" >全国福利政策</a></li>
<li class=""><a href="#">政策法规查询</a></li>
</ul>
</div>
<div id="content">
    <div class="kf">客服：<asp:Label ID="lblSupportStaff" runat="server"></asp:Label></div>
<div class="searchBox2">
    <table width="704" border="0" align="center" cellpadding="0" cellspacing="0">
    <tbody><tr>
      <td width="58" height="40" align="center">开始时间</td>
      <td width="94" align="center"><span class="searchBox fl">
        <asp:TextBox runat="server" name="lblStartTime" id="lblStartTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Width="110px"></asp:TextBox>
      </span></td>
      <td width="56" align="center">结束时间</td>
      <td width="96" align="center"><span class="searchBox fl">
       <asp:TextBox runat="server" name="lblEndTime" id="lblEndTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Width="110px"></asp:TextBox>
      </span></td>
      <td width="160" align="left"><span class="searchBox fl">
        <asp:TextBox runat="server" name="lblNameOrIdCard" ID="lblNameOrIdCard" placeholder="姓名或证件号码"></asp:TextBox>
      </span></td>
      <td width="71" align="left"><a href="javascript:void(0)" onclick="searchInfo()"><img src="../images/bb1.jpg" width="71" height="23"></a></td>
<td width="71" align="left"><asp:ImageButton runat="server" ID="exportExcel" OnClick="exportExcel_OnClick" ImageUrl="../images/bb5.jpg" Width="68px" Height="21px" /></td>
    </tr>
  </tbody></table>
</div>
<div class="table viewlist">
<table width="715" border="0" cellspacing="0" cellpadding="0"  id="tableList">
  <tbody>
      <tr>
    <th width="101" height="46" align="center" class="u1" style="padding-left:10px;">
      序号 </th>
    <th width="174" align="center" class="u1">姓名 </th>
    <th width="155" align="center" class="u1">性别 </th>
    <th width="285" align="center" class="u1">身份证</th>
  </tr>
<asp:Repeater ID="rptContents" runat="server" OnItemDataBound="rptContents_ItemDataBound">
    <ItemTemplate>
        <tr class="u6">
    <td align="center" valign="middle" class="ico" style="width:80px; padding-left:10px;"><%# this.rptContents.Items.Count + 1%> </td>
    <td align="center" valign="middle"> <%# Eval("personName")%></td>
    <td align="center" valign="middle"> <%# Eval("sex")%></td>
    <td align="center" valign="middle"> <%# Eval("idcard")%> <span style="display: none"><%# Eval("employeeId")%></span></td>
  </tr>
        <tr class="u7" >
    <td colspan="4" align="center" valign="middle"><div class="scroll" style="width:710px">
      <table width="3000" border="0" cellspacing="0" cellpadding="0" style=" background-image:none; margin-top:3px;">
        <tbody><tr>
          <td width="219" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">日期</td>
          <td width="235" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">养保个人<br>
            缴费基数</td>
          <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">养保个人<br>
            缴费比例</td>
          <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">养保企业<br>
            缴费基数</td>
          <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">养保企业<br>
            缴费比例</td>
          <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">医保个人<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">医保个人<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">医保企业<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">医保企业<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">失保个人<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">失保个人<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">失保企业<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">失保企业<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">工伤企业<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">工伤企业<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">生育企业<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">生育企业<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">大病医疗个人<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">大病医疗个人<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">大病医疗企业<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">大病医疗企业<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">残障金<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">残障金<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">公积金个人<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">公积金个人<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">公积金企业<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">公积金企业<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">补充公积金个人<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">补充公积金个人<br>
            缴费比例</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">补充公积金企业<br>
            缴费基数</td>
            <td width="261" align="center" valign="middle" class="u13" style="background-color: #a9cfe5;">补充公积金企业<br>
            缴费比例</td>
        </tr>
            <asp:Repeater runat="server" ID="rptItemContents">
                            <ItemTemplate>
        <tr>
          <td align="center" class="u17"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("feeDate "))%></td>
          <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfEndowmentInsBase "))%> </td>
          <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfEndowmentInsRate "))%>%</td>
          <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpEndowmentInsBase "))%> </td>
          <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpEndowmentInsRate "))%>%</td>
          <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfMedicalInsBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfMedicalInsrRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpMedicalInsBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpMedicalInsRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfJoblessInsBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfJoblessInsRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpJoblessInsBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpfJoblessInsRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpworkInjuredBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpworkInjuredRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpBirthBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpBirthRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfIllnessBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfIllnessRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpIllnessBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpIllnessRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpDisableBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpDisableRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfFundBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfFundRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpFundBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpFundRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfAddFundBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("selfAddFundRate "))%>%</td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpAddFundBase "))%></td>
            <td align="center" class="u17"><%# String.Format("{0:F}", Eval("cmpAddFundRate "))%>%</td>
        </tr></ItemTemplate></asp:Repeater>
      </tbody></table>
    </div></td>
  </tr>
    </ItemTemplate>
</asp:Repeater>
  
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
        window.location.href = "CompanyNationalWelfareInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime + "&NameOrIdCard=" + NameOrIdCard + "&pagesize=" + pageNum + "&pagenum=5";
    }

    function searchInfo() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        var NameOrIdCard = $("#lblNameOrIdCard").val();
        window.location.href = "CompanyNationalWelfareInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime + "&NameOrIdCard=" + NameOrIdCard;
    }
</script>
</html>