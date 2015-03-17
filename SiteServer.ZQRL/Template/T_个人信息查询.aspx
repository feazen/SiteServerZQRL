<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.PersonInfos" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>个人信息查询 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
<link rel="stylesheet" type="text/css" href="../css/global.css" />
<link rel="stylesheet" type="text/css" href="../css/main.css" />
<script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
<script type="text/javascript" src="../js/ScrollPic.js"></script>
</head>
<body style="background-color: #2f83cf;">
<form id="form1" class="form-inline" runat="server">
<stl:include file="@/include/header.html"></stl:include>
<div id="area" class="marA">
    <div style="color: #ffffff; position: absolute;top: -20px;right: 0px">当前登录用户：<%=login_name %>&nbsp;&nbsp;&nbsp;&nbsp;<a style="color: #ffffff;"  href="javasript:void(0)" onclick="logOut();return false;">退出</a></div>
<div class="top" style="height: 21px;"><img src="../images/c1.jpg" width="1009" height="21"></div>
<div class="middle oh">
<div id="sonMenu">
<div class="title">个人福利查询</div>
<ul>
<li class=""><a href="PersonInfos.aspx" class="sonMenuSelected">个人信息查询</a></li>
<li class=""><a href="PersonSalaryInfos.aspx">薪资查询</a></li>
<li class=""><a href="PersonNationalWelfareInfos.aspx">国家福利查询</a></li>
<li class=""><a href="PersonMedicalclaimInfos.aspx">就医理赔查询</a></li>
<li class=""><a href="PersonResidenceManageInfos.aspx">证件办理情况</a></li>
</ul>
</div>
  <div id="content">
            <div class="table mar0">
                <table width="719" border="0" cellspacing="0" cellpadding="0" style="background-image: url(../images/s10.jpg);">
                    <tr>
                        <td height="21" colspan="2" valign="top" style="padding-left: 10px; line-height: 33px; font-size: 14px; color: #FFF">个人信息</td>
                    </tr>
                    <tr>
                        <td height="" colspan="2" valign="top" style="padding-left: 10px; line-height: 33px; font-size: 14px; color: #FFF; height: 20px;"></td>
                    </tr>
                    <tr class="u4">
                        <td width="190">姓名：</td>
                        <td width="529"><span><asp:Label ID="lblPersonName" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>身份证：</td>
                        <td><span><asp:Label ID="lblIdCard" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>性别：</td>
                        <td><span><asp:Label ID="lblSex" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>出生年月：</td>
                        <td><span><asp:Label ID="lblBirthday" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>国籍：</td>
                        <td><span><asp:Label ID="lblCountry" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>民族：</td>
                        <td><span><asp:Label ID="lblNation" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>政治面貌：</td>
                        <td><span><asp:Label ID="lblPolicy" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>户籍性质：</td>
                        <td><span><asp:Label ID="lblFamilyType" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>常用电话：</td>
                        <td><span><asp:Label ID="lblPhone" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>常用邮箱：<br />
                        </td>
                        <td><span><asp:Label ID="lblMail" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>紧急联系人：</td>
                        <td><span><asp:Label ID="lblEmergencyPerson" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>紧急联系电话：</td>
                        <td><span><asp:Label ID="lblEmergencyPhone" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>公司名称：</td>
                        <td><span><asp:Label ID="lblCompanyName" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>所属银行：</td>
                        <td><span><asp:Label ID="lblBank" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>开户行：</td>
                        <td><span><asp:Label ID="lblOpenBank" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>银行账户：</td>
                        <td><span><asp:Label ID="lblBankAccount" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>公积金账户：</td>
                        <td><span><asp:Label ID="lblFundAccount" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>合同开始日期：</td>
                        <td><span><asp:Label ID="lblContractStartDate" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>合同终止日期：</td>
                        <td><span><asp:Label ID="lblContractEndDate" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>户籍地址：</td>
                        <td><span><asp:Label ID="lblFamilyAddress" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>户籍地址邮编</td>
                        <td><span><asp:Label ID="lblFamilyPostCode" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>居住地址：</td>
                        <td><span><asp:Label ID="lblLivingAddress" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>居住地址邮编：</td>
                        <td><span><asp:Label ID="lblLivingPostCode" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>税单地址：</td>
                        <td><span><asp:Label ID="lblTaxAddress" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>税单地址邮编：</td>
                        <td><span><asp:Label ID="lblTaxPostCode" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>其他地址：</td>
                        <td><span><asp:Label ID="lblOtherAddress" runat="server"></asp:Label></span></td>
                    </tr>
                    <tr class="u4">
                        <td>其他地址邮编：</td>
                        <td><span><asp:Label ID="lblOtherPostCode" runat="server"></asp:Label></span></td>
                    </tr>
                </table>
                <table width="714" border="0" cellspacing="0" cellpadding="0" style="background-image: none">
                    <tr>
                        <td width="714" height="49" align="center" valign="bottom">
                            <!--<img src="../images/bb2.jpg" width="111" height="30" />--></td>
                    </tr>
                </table>
                <p>&nbsp;</p>
            </div>
        </div></div>
        <div class="bottom marA">
            <img src="../images/c3.jpg" width="1009" height="44" /></div>
</div>
        <<stl:include file="@/include/footer.html"></stl:include><!--footer2 end-->
</form>
    <script src="../js/Logout.js" type="text/javascript" language="javascript"></script>
</body>
</html>