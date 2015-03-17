<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.CompanyRegister" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>企业用户注册 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>
<link rel="stylesheet" type="text/css" href="../css/global.css" />
<link rel="stylesheet" type="text/css" href="../css/main.css" />
<link href="../My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css">
<link rel="stylesheet" type="text/css" href="../css/tipTip.css" />
<script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
<script type="text/javascript" src="../js/ScrollPic.js"></script>
<script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="../js/jquery.tipTip.js"></script>
<script type="text/javascript" src="../js/jquery.cookie.js"></script>
<script type="text/javascript" src="../js/CompanyRegister.js"></script>
</head>
<body style=" background-color:#2162a5">
<form id="form1" class="form-inline" runat="server">
<stl:include file="@/include/header.html"></stl:include>
<div id="area" class="marA">
<div class="top"></div>
<div class="middle oh" style="background-image:url(../images/back.jpg); height:543px;">
<div class="regTop">会员注册</div>
<div class="regTab" >
<div class="regA2"><a href="/Person/PersonRegister.aspx">个人会员</a></div>
<div class="regA1" style="padding-top:3px;"><a href="/Company/CompanyRegister.aspx">企业会员</a></div>
</div>
<br>
<table width="403" border="0" align="center" cellpadding="0" cellspacing="0" class="regTable" style="margin-top:10px; ">
  <tbody><tr>
    <td width="115">账户名</td>
    <td width="288">
      <input id="loginName" type="text"   >
   </td>
  </tr>
  <tr>
    <td>密码</td>
    <td>
      <input id="loginPassword" type="password"   >
</td>
  </tr>
  <tr>
    <td>重复密码</td>
    <td>
      <input id="AgainPassword" type="password"   >
</td>
  </tr>
  <tr>
    <td>组织机构代码证</td>
    <td>
      <input id="IdCard" type="text"   >
</td>
  </tr>
  <tr>
    <td>验证码</td>
    <td>
<input id="VerifyCode" type="text"   style=" width:160px;" >
<a id="GetVerifyCodeBtn" href="javascript:void(0);" style=" width:120px;height:28px">发送验证码到企业邮箱</a>
</td>
  </tr>
  <tr>
    <td height="49">&nbsp;</td>
    <td valign="bottom"><a id="RegisterBtn" href="javascript:void(0);"></a></td>
  </tr>
</tbody></table>
<div style=" margin-top:50px; text-align:center; font-size:13px; font-weight:bold; color:#808080">注册企业因邮箱发生变化无法收到验证码，请致电：<span style=" color:#f5b485">400 089 9090</span> 更新您的企业邮箱信息。</div>
</div>



<div class="bottom marA"></div>
</div>

       <stl:include file="@/include/footer.html"></stl:include><!--footer2 end-->
</form>
</body>
</html>