<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.PersonMedicalclaimInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

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
<li class=""><a href="PersonMedicalclaimInfos.aspx" class="sonMenuSelected">就医理赔查询</a></li>
<li class=""><a href="PersonResidenceManageInfos.aspx">证件办理情况</a></li>
</ul>
</div>
<div id="content">
<div class="searchBox2">
  <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
    <tbody><tr>
      <td width="101" height="40" align="center">单据提交 起始</td>
      <td width="165" align="center"><span class="searchBox fl">
        <asp:TextBox runat="server" name="lblStartTime" id="TextBox1" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"></asp:TextBox>
      </span></td>
      <td width="36" align="center">终止</td>
      <td width="161" align="center"><span class="searchBox fl">
        <asp:TextBox runat="server" name="lblEndTime" id="TextBox2" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})"></asp:TextBox>
      </span></td>
      <td width="81" align="left"><a href="javascript:void(0)" onclick="searchInfo()"><img src="../images/bb1.jpg" width="71" height="23"></a></td>
      <td width="152" align="left"><a href="javascript:void(0)" onclick="SWInfo()"><img src="../images/bb6.jpg" width="71" height="23"></a></td>
    </tr>
  </tbody></table>
</div>
<table width="715" border="0" cellspacing="0" cellpadding="0" id="tableList">
  <tbody><tr>
    <th width="715" height="46" align="center" class="u1" style="padding-left:30px;">日期</th>
    <th width="715" align="center" class="u1">被保险人 </th>
    <th width="715" align="center" class="u1">医疗报销时间 </th>
    <th width="715" align="center" class="u1"> 医疗报销金额</th>
    <th width="715" align="center" class="u1">是否报销&nbsp;</th>
    <th width="715" align="center" class="u1">详情
<input type="hidden" name="fid" id="fid" value="vvv1">
 </th>
    </tr>
  <asp:Repeater ID="rptContents" runat="server" OnItemDataBound="rptContents_ItemDataBound">
    <ItemTemplate>
        <tr class="u6">
            <td align="center" valign="middle" class="ico"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("applyDate"))%></td>
    <td align="center" valign="middle"><%# Eval("insPerson")%></td>
    <td align="center" valign="middle"><%#   String.Format("{0:yyyy-MM-dd} ", Eval("reimburseDate"))%></td>
    <td align="center" valign="middle"><%# String.Format("{0:F}", Eval("reimburseAmount"))%></td>
    <td align="center" valign="middle"> <%# Eval("isReimburse")%></td>
    <td align="center" valign="middle"><a href="#">详情</a><span style="display: none"><%# Eval("number")%></span></td>
        </tr>
        <tr class="u7">
            <td colspan="6" align="center" valign="middle">

    <table width="715" border="0" cellspacing="0" cellpadding="0" style="background-image:none">
  <tbody><tr>
    <td colspan="11" style=" padding-left:20px" class="u9">报销比例：<%# Eval("reimburseRate")%></td>
  </tr>
  <tr>
    <td colspan="11" style=" padding-left:20px" class="u9">费用计算公式：（<%# Eval("feeFomula")%>）X 报销比例</td>
  </tr>
  <tr>                                                         
    <td class="u8">单证号</td>
    <td class="u8">单证日期</td>
    <td class="u8">就诊医院</td>
    <td class="u8">收据总费用</td>
    <td class="u8">帐户支付</td>
    <td class="u8">附加支付</td>
    <td class="u8">统筹支付</td>
    <td class="u8">自付</td>
    <td class="u8">分类自付</td>
    <td class="u8">自费</td>
    <td class="u8">备注</td>
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
      </ItemTemplate>
          </asp:Repeater>
    </tbody>
    </table>
   
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
<script type="text/javascript">
    function GoPages() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        var pageNum = $("#PageNum").val();
        if (pageNum == "") {
            pageNum = "1";
        }
        window.location.href = "PersonMedicalclaimInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime+"&pagesize="+pageNum+"&pagenum=5";
    }

    function searchInfo() {
        var StartTime = $("#lblStartTime").val();
        var EndTime = $("#lblEndTime").val();
        window.location.href = "PersonMedicalclaimInfos.aspx?StartTime=" + StartTime + "&EndTime=" + EndTime;
    }
</script>
</html>