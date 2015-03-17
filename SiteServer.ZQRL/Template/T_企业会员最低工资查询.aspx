<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.CompanyMinwageInfos" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

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
<li class=""><a href="CompanyMedicalclaimInfos.aspx" >就医理赔查询</a></li>
<li class=""><a href="CompanyResidenceManageInfos.aspx">证件办理情况</a></li>
<li class=""><a href="CompanyFundrateInfo.aspx" class="sonMenuSelected">全国福利政策</a></li>
<li class=""><a href="#">政策法规查询</a></li>
</ul>
</div>
<div id="content">
    <div class="CompanyFundrate">
        <select  id="selectCompanyFund" style=" width:160px;" onchange="self.location.href=options[selectedIndex].value">
              <option value="CompanyFundrateInfo.aspx"> 社保公积金基数比例</option>
              <option  value="CompanyFundsearchInfo.aspx"> 社保医保公积金查询</option>
              <option selected="selected" value="CompanyMinwageInfo.aspx"> 最低工资</option>
              <option value="CompanyBearpolicyInfo.aspx"> 生育政策</option>
              <option value="CompanyLosejobpolicyInfo.aspx"> 失业政策</option>
              <option value="CompanyRemotemedicalInfo.aspx"> 异地就医</option>
              <option value="CompanyDisbilitymoneyInfo.aspx"> 残保金</option>
        </select>
    </div>
<div class="kf">客服：<asp:Label ID="lblSupportStaff" runat="server"></asp:Label></div>
<div class="searchBox2" style=" margin-top:10px;line-height: 42px;">
  <table width="704" border="0" align="center" cellpadding="0" cellspacing="0">
    <tbody>
        <tr>
      <td width="43" align="center">省份</td>
      <td width="71" align="center"><span class="searchBox fl">
        <asp:DropDownList ID="ddlProvince" runat="server" DataTextField="Name" DataValueField="Id" style=" width:60px;" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_OnSelectedIndexChanged">
            </asp:DropDownList>
      </span></td>
      <td width="44" align="center">城市</td>
      <td width="74" align="left"><span class="searchBox fl">
          <asp:DropDownList ID="ddlCity" runat="server" DataTextField="Name" DataValueField="Id" style=" width:60px;">
            </asp:DropDownList>
      </span></td>
      <td width="46" align="left">年份</td>
      <td width="125" align="left"><span class="searchBox fl">
       <asp:TextBox runat="server" name="lblYearTime" id="lblYearTime" onclick="WdatePicker({dateFmt:'yyyy年'})" Width="120px"></asp:TextBox>
      </span></td>
        <td width="86" align="center"><a href="javascript:void(0)" onclick="searchInfo()"><img src="../images/bb4.jpg" width="54" height="21"></a></td>
      </tr>
  </tbody></table>
</div>
<div class="table">
<div class="scroll">
  <table width="2000" border="0" cellspacing="0" cellpadding="0" id="tableList" style=" background-image:url(../images/s18.jpg)">
    <tbody><tr>
        <th  height="63"  align="center" class="u1"> 大区</th>
        <th  height="63"  align="center" class="u1"> 省份</th>
        <th  height="63"  align="center" class="u1">城市</th>
        <th  height="63"  align="center" class="u1">通知时间</th>
        <th  height="63"  align="center" class="u1">最低工资</th>
        <th  height="63"  align="center" class="u1">年度</th>
        <th  height="63"  align="center" class="u1">文件链接</th>
        <th  height="63"  align="center" class="u1">文件链接网址</th>
        <th  height="63"  align="center" class="u1">离职员工社保凭证</th>
        <th  height="63"  align="center" class="u1">备注</th>
      </tr>
    <asp:Repeater ID="rptContents" runat="server">
    <ItemTemplate>
    <tr>
        <td align="center" valign="middle" class="u14"> <%# Eval("area_Name")%></td>
        <td align="center" valign="middle" class="u14"> <%# Eval("province_Name")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("city_Name")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("noticedate")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("minwage")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("minuntotalhour")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("year")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("filetxt")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("filelink")%></td>
        <td align="center" valign="middle" class="u14"><%# Eval("remark")%></td>
      </tr>
        </ItemTemplate>
        </asp:Repeater>
  </tbody></table>
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
        var yearTime = $("#lblYearTime").val();
        var pageNum = $("#PageNum").val();
        //var fundType = $('#ddlFundtype option:selected').val();
        var provinceType = $('#ddlProvince option:selected').val();
        var cityType = $('#ddlCity option:selected').val();
        if (pageNum == "") {
            pageNum = "1";
        }
        window.location.href = "CompanyMinwageInfo.aspx?YearTime=" + yearTime + "&provinceType=" + provinceType + "&cityType=" + cityType + "&pagesize=" + pageNum + "&pagenum=5";
    }

    function searchInfo() {
        var yearTime = $("#lblYearTime").val();
        // var fundType = $('#ddlFundtype option:selected').val();
        var provinceType = $('#ddlProvince option:selected').val();
        var cityType = $('#ddlCity option:selected').val();
        window.location.href = "CompanyMinwageInfo.aspx?YearTime=" + yearTime + "&provinceType=" + provinceType + "&cityType=" + cityType;
    }
</script>
</html>