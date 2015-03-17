<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.CompanyMedicalclaimInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>企业就医理赔查询 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
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
<li class=""><a href="CompanyNationalWelfareInfos.aspx" >国家福利查询</a></li>
<li class=""><a href="CompanyMedicalclaimInfos.aspx" class="sonMenuSelected">就医理赔查询</a></li>
<li class=""><a href="CompanyResidenceManageInfos.aspx">证件办理情况</a></li>
<li class=""><a href="CompanyFundrateInfo.aspx">全国福利政策</a></li>
<li class=""><a href="#">政策法规查询</a></li>
</ul>
</div>
<div id="content">
    <div class="kf">客服：<asp:Label ID="lblSupportStaff" runat="server"></asp:Label></div>
<div class="searchBox2" style=" margin-top:10px;">
  <table width="704" border="0" align="center" cellpadding="0" cellspacing="0">
    <tbody><tr>
      <td width="98" height="40" align="center">单据提交 起始</td>
      <td width="90" align="center"><span class="searchBox fl">
          <asp:TextBox runat="server" name="lblStartTime" id="lblStartTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Width="85px"></asp:TextBox>
      </span></td>
      <td width="27" align="center">终止</td>
      <td width="89" align="center"><span class="searchBox fl">
          <asp:TextBox runat="server" name="lblEndTime" id="lblEndTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Width="85px"></asp:TextBox>
      </span></td>
      <td width="21" align="center">&nbsp;</td>
      <td width="117" align="left"><span class="searchBox fl">
          <asp:TextBox runat="server" name="lblNameOrIdCard" ID="lblNameOrIdCard" Width="110px" placeholder="姓名或证件号码"></asp:TextBox>
      </span></td>
        <td width="84" align="center"><a href="javascript:void(0)" onclick="searchInfo()"><img src="../images/bb4.jpg" width="54" height="21"></a></td>
      <td width="72" align="left"><a href="javascript:void(0)" onclick="exportExcel()"><img src="../images/bb5.jpg" width="68" height="21"></a></td>
      <td width="106" align="left"><asp:ImageButton runat="server" ID="exportExcel" OnClick="exportExcel_OnClick" ImageUrl="../images/bb5.jpg" Width="68px" Height="21px" /></td>
    </tr>
  </tbody></table>
</div>
<div class="table viewlist">
<table width="715" border="0" cellspacing="0" cellpadding="0" id="tableList">
  <tbody>
      <tr>
      <th width="90" height="46" align="center" class="u1" style="padding-left:10px;"><input type="hidden" name="fid" id="fid" value="vvv1">
        序号</th>
      <th width="71" align="center" class="u1">被保险人 </th>
      <th width="136" align="center" class="u1">身份证 </th>
      <th width="100" align="center" class="u1"> 日期  </th>
      <th width="101" align="center" class="u1"> 医疗报销时间&nbsp;</th>
      <th width="99" align="center" class="u1">医疗报销金额</th>
      <%--<th width="69" align="center" class="u1">是否报销 </th>--%>
      <th width="49" align="center" class="u1"> 详情</th>
      </tr>
<asp:Repeater ID="rptContents" runat="server" OnItemDataBound="rptContents_ItemDataBound">
    <ItemTemplate>
        <tr class="u6">
      <td align="center" valign="middle" class="sico" style="width:80px; padding-left:10px;" id="icovvv1"><%# this.rptContents.Items.Count + 1%></td>
      <td align="center" valign="middle"><%# Eval("insPerson")%></td>
      <td align="center" valign="middle"> <%# Eval("insPersonIDNumber")%></td>
      <td align="center" valign="middle"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("applyDate"))%></td>
      <td align="center" valign="middle"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("reimburseDate"))%></td>
      <td align="center" valign="middle"><a href="#"> <%# String.Format("{0:F}", Eval("reimburseAmount"))%></a></td>
      <%--<td align="center" valign="middle"><%# Eval("isReimburse")%> </td>--%>
      <td align="center" valign="middle"><a href="#">详情</a><span style="display: none"><%# Eval("id")%></span></td>
    </tr>
        <tr class="u7">
      <td colspan="7" align="center" valign="middle">
          <div class="scroll" style="width: 715px">
          <table width="1200" border="0" cellspacing="0" cellpadding="0" style="background-image:none">
        <tbody><tr>
          <td colspan="11" style=" padding-left:20px" class="u9">报销比例：<%# Eval("reimburseRate")%></td>
        </tr>
        <tr>
          <td colspan="11" style=" padding-left:20px" class="u9">费用计算公式：（<%# Eval("feeFomula")%>）X 报销比例</td>
        </tr>
        <tr>
          <td class="u81" style="background-color: #a9cfe5">单证号</td>
          <td class="u81" style="background-color: #a9cfe5">单证日期</td>
          <td class="u81" style="background-color: #a9cfe5">就诊医院</td>
          <td class="u81" style="background-color: #a9cfe5">收据总费用</td>
          <td class="u81" style="background-color: #a9cfe5">帐户支付</td>
          <td class="u81" style="background-color: #a9cfe5">附加支付</td>
          <td class="u81" style="background-color: #a9cfe5">统筹支付</td>
          <td class="u81" style="background-color: #a9cfe5">自付</td>
          <td class="u81" style="background-color: #a9cfe5">分类自付</td>
          <td class="u81" style="background-color: #a9cfe5">自费</td>
          <td class="u81" style="background-color: #a9cfe5">备注</td>
        </tr>
            <asp:Repeater runat="server" ID="rptItemContents">
                            <ItemTemplate>
        <tr><td height="2" align="center" class="u9"><%# Eval("number")%></td>
    <td align="center" class="u9"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("billDate"))%></td>
    <td align="center" class="u9"><%# Eval("hospital")%></td>
    <td align="center" class="u9"><%# String.Format("{0:F}", Eval("totalFee"))%></td>
    <td align="center" class="u9"><%# String.Format("{0:F}", Eval("accountPay"))%></td>
    <td align="center" class="u9"><%# String.Format("{0:F}", Eval("addedPay"))%></td>
    <td align="center" class="u9"><%# String.Format("{0:F}", Eval("panPay"))%></td>
    <td align="center" class="u9"><%# String.Format("{0:F}", Eval("selfResponse"))%></td>
    <td align="center" class="u9"><%# String.Format("{0:F}", Eval("classify"))%></td>
    <td align="center" class="u9"><%# String.Format("{0:F}", Eval("selfPay"))%></td>
    <td align="center" class="u9"><%# Eval("remark")%></td>
    
  </tr>
                            </ItemTemplate></asp:Repeater>
      </tbody></table></div>
      </td>
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
        window.location.href = "CompanyMedicalclaimInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime + "&NameOrIdCard=" + NameOrIdCard + "&pagesize=" + pageNum + "&pagenum=5";
    }

    function searchInfo() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        var NameOrIdCard = $("#lblNameOrIdCard").val();
        window.location.href = "CompanyMedicalclaimInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime + "&NameOrIdCard=" + NameOrIdCard;
    }
</script>
</html>