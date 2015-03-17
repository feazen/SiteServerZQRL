<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.EmployeeInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>员工信息查询 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
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
<div class="middle oh">
<div id="sonMenu">
<div class="title">企业福利查询</div>
<ul>
<li class=""><a href="EmployeeInfos.aspx" class="sonMenuSelected">员工信息</a></li>
<li class=""><a href="CompanySalaryInfosByTime.aspx" >员工薪酬</a></li>
<li class=""><a href="CompanyNationalWelfareInfos.aspx" >国家福利查询</a></li>
<li class=""><a href="CompanyMedicalclaimInfos.aspx">就医理赔查询</a></li>
<li class=""><a href="CompanyResidenceManageInfos.aspx">证件办理情况</a></li>
<li class=""><a href="CompanyFundrateInfo.aspx" >全国福利政策</a></li>
<li class=""><a href="#">政策法规查询</a></li>
</ul>
</div>
<div id="content">
<div class="kf">客服：<asp:Label ID="lblSupportStaff" runat="server"></asp:Label></div>
<div class="searchBox2">
  <table width="645" border="0" align="center" cellpadding="0" cellspacing="0">
    <tbody><tr>
      <td width="81" height="40" align="center">开始时间</td>
      <td width="171" align="center"><span class="searchBox fl">
          <asp:TextBox runat="server" name="lblStartTime" id="lblStartTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"></asp:TextBox>
      </span></td>
      <td width="81" align="center">结束时间</td>
      <td width="179" align="center"><span class="searchBox fl">
          <asp:TextBox runat="server" name="lblEndTime" id="lblEndTime" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"></asp:TextBox>
      </span></td>
      <td width="95" align="left"><a href="javascript:void(0)" onclick="searchInfo()"><img src="../images/bb1.jpg" width="71" height="23"></a></td>
<%--<td width="71" align="left"><a href="javascript:void(0)" onclick="exportExcel()"><img src="../images/bb5.jpg" width="68" height="21"></a></td>--%>
                                        <td width="71" align="left">
                                            <asp:ImageButton runat="server" ID="exportExcel" OnClick="exportExcel_OnClick" ImageUrl="../images/bb5.jpg" Width="68px" Height="21px" />
                                        </td>
    </tr>
  </tbody></table>
</div>
<div class="table viewlist">
<table width="715" border="0" cellspacing="0" cellpadding="0" id="tableList">
  <tbody><tr>
    <th width="97" height="47" align="center" class="u1" style="padding-left:10px;"><input type="hidden" name="fid" id="fid" value="vvv1">
      序号 </th>
    <th width="115" align="center" class="u1">姓名 </th>
    <th width="73" align="center" class="u1">性别 </th>
    <th width="148" align="center" class="u1">身份证</th>
    <th width="153" align="center" class="u1"> 服务起始时间</th>
    <th width="129" align="center" class="u1">服务终止时间</th>
    </tr>
<asp:Repeater ID="rptContents" runat="server">
    <ItemTemplate>
        <tr class="u6">
        <td  align="center" class="ico">
<%# this.rptContents.Items.Count + 1%> 
</td>
        <td  align="center">
            <%# Eval("personName")%></td>
        <td  align="center" >
            <%# Eval("sex")%></td>
        <td  align="center">
            <%# Eval("idcard")%></td>
        <td  align="center">
            <%#   String.Format("{0:yyyy-MM-dd} ", Eval("contractStartDate"))%></td>
        <td  align="center">
            <%#   String.Format("{0:yyyy-MM-dd} ", Eval("contractEndDate"))%></td></tr>
        <tr class="u7">
            <td colspan="6" align="center" valign="middle"><div class="kk">
      <table width="719" border="0" cellspacing="0" cellpadding="0" style="background-image:none;">

        <tbody><tr class="u4">
          <td width="113">姓名</td>
          <td width="161"><span><%# Eval("personName")%></span></td>
          <td width="117">工作城市</td>
          <td width="93">&nbsp;</td>
          <td width="96">福利起始月</td>
          <td width="139"> <%#   String.Format("{0:yyyy-MM-dd} ", Eval("contractStartDate"))%></td>  
        </tr>
        <tr class="u4">
          <td>身份证</td>
          <td><span><%# Eval("idcard")%></span></td>
          <td>户籍性质</td>
          <td><%# Eval("familyType")%></td>
          <td>福利终止月</td>
          <td><%#   String.Format("{0:yyyy-MM-dd} ", Eval("contractEndDate"))%></td>
        </tr>
        <tr class="u4">
          <td>性别</td>
          <td><span><%# Eval("sex")%></span></td>
          <td>常用电话</td>
          <td><%# Eval("phone")%></td>
          <td>户籍地址</td>
          <td><%# Eval("familyAddress")%></td>
        </tr>
        <tr class="u4">
          <td>出生年月</td>
          <td><%#   String.Format("{0:yyyy-MM-dd} ", Eval("birthday"))%></td> 
          <td>常用邮箱</td>
          <td><%# Eval("mail")%></td>
          <td>户籍地邮编</td>
          <td><%# Eval("familyPostCode")%></td>
        </tr>
        <tr class="u4">
          <td>国籍</td>
          <td><%# Eval("country")%></td>
          <td>紧急联系人1</td>
          <td><%# Eval("emergencyperson")%></td>
          <td>居住地址</td>
          <td><%# Eval("livingAddress")%></td>
        </tr>
        <tr class="u4">
          <td>民族</td>
          <td><%# Eval("nation")%></td>
          <td>紧急联系人2</td>
          <td>&nbsp;</td>
          <td>居住地邮编</td>
          <td><%# Eval("livingPostCode")%></td>
        </tr>
        <tr class="u4">
          <td>政治面貌</td>
          <td><%# Eval("policy")%></td>
          <td>职位</td>
          <td>&nbsp;</td>
          <td>税单地址</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="u4">
          <td>籍贯</td>
          <td>&nbsp;</td>
          <td>部门</td>
          <td>&nbsp;</td>
          <td>税单地邮编</td>
          <td><%# Eval("taxPostCode")%></td>
        </tr>
        <tr class="u4">
          <td>户籍性质</td>
          <td><%# Eval("familyType")%></td>
          <td>工号</td>
          <td>&nbsp;</td>
          <td>其他地址</td>
          <td><%# Eval("otherAddress")%></td>
        </tr>
        <tr class="u4">
          <td>文化程度</td>
          <td>&nbsp;</td>
          <td>所属银行</td>
          <td><%# Eval("bank")%></td>
          <td>其他邮编</td>
          <td><%# Eval("otherPostCode")%></td>
        </tr>
        <tr class="u4">
          <td>毕业学校<br></td>
          <td>&nbsp;</td>
          <td>开户行</td>
          <td><%# Eval("openbank")%></td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="u4">
          <td>婚姻</td>
          <td>&nbsp;</td>
          <td>银行账户</td>
          <td><%# Eval("bankaccount")%></td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="u4">
          <td>生育情况</td>
          <td>&nbsp;</td>
          <td>公积金账户</td>
          <td><%# Eval("fundaccount")%></td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="u4">
          <td>子女姓名1</td>
          <td>&nbsp;</td>
          <td>合同开始日期</td>
          <td><%#   String.Format("{0:yyyy-MM-dd} ", Eval("agreementStartDate"))%></td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="u4">
          <td>子女姓名2</td>
          <td>&nbsp;</td>
          <td>合同终止日期</td>
          <td><%#   String.Format("{0:yyyy-MM-dd} ", Eval("agreementEndDate"))%></td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="u4">
          <td>子女出生日期1</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="u4">
          <td>子女出生日期2</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
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

<div class="bottom marA" style="height: 44px;"><img src="../images/c3.jpg" width="1009" height="44"></div>
</div>
         <stl:include file="@/include/footer.html"></stl:include><!--footer2 end-->
</div>
</form>
</body>
    <script src="../js/Logout.js" type="text/javascript" language="javascript"></script>
    <script src="../js/onkeyupBtn.js" type="text/javascript" language="javascript"></script>
<script type="text/javascript">
    function GoPages() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        var pageNum = $("#PageNum").val();
        if (pageNum == "") {
            pageNum = "1";
        }
        window.location.href = "EmployeeInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime+"&pagesize="+pageNum+"&pagenum=5";
    }

    function searchInfo() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        window.location.href = "EmployeeInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime;
    }
</script>
</html>