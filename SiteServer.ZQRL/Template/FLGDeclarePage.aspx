<%@ Page Language="C#" AutoEventWireup="true" Inherits="SiteServer.ZQRL.ForegroundPages.FLGDeclarePage" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 

1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>福利高跳转页面 - <stl:channel type="meta_title" channelIndex="首页"></stl:channel></title>



</head>

<body style="font:16px Microsoft Yahei;margin:0px;text-align:center;">
    <form id="form1" runat="server">
<div style="width:600px;height:260px;margin:10px auto;background-color: #e6e6e6; ">
<div style="width:500px;height:5px;margin:0 auto;"></div>
    
<div  style="text-align:center; font-size:25px;width:500px;height:30px;margin:10px auto;">
<strong>声     明</strong>
</div>
   <div style="width:500px; height500px; text-align:center; width:500px;height:150px;margin:0 auto;overflow:auto">
        　　<p style="text-indent:2em; text-align:left; line-height:25px;">在您点击进入“中企商城”之前，请务必仔细阅读并透彻理解本声明的全部内容。
　　          <br />
              &nbsp;&nbsp;如您同意并认可本声明全部内容的，请点击下方“同意并授权”键；您也可以选择“不同意”，我们将不继续为您跳转至中企商城。
		    <br/>
            1、“中企商城”系“福利高平台”（www.fuligold.com）提供的专属福利平台之一，由上海富友金融网络技术有限公司全权运营并直接负责网络安全。该商城的可用性、安全性及商城所包含的产品、服务及信息等全部内容均由上海富友金融网络技术有限公司负责。
              
            <br/>
            2、为了方便您的使用，若您通过该链接进入商城，则表明您已授权同意将您中企人力官网的账户及密码直接生成为中企商城的账户及密码，并默认登陆。
              
              <br/>
           3、“中企商城”的用户隐私保密政策以“福利高平台”的政策为准，该商城用户的账户隐私及资金安全保护，均由运营商即上海富友金融网络技术有限公司提供并全权负责。
              <br/>
　　鉴于上述，中企人力在此特别提醒您：
              <br/>
              1、请务必注意保护您的账户及密码安全，提高您账户的安全性并防止非法用户恶意获取您的账户及密码信息，若因该商城或“福利高平台”的原因，或者因您使用该商城账户的原因而导致的后果，包括但不限于您账户或账户资金损失等情形，中企人力均不承担任何责任。
互联网上不排除因黑客行为或用户保管疏忽导致账号、密码遭他人非法使用，此类情况亦与中企人力无关。
              <br />
              2、为维护中企人力官网用户的权益，提高中企人力服务质量，中企人力有权随时对本声明内容进行修订并予以发布；此外，中企人力对本声明的全部条款及内容拥有最终解释权。
              <br/>
        　　</p>
       <p style="text-indent:2em; text-align:right; line-height:25px;"> 
           中企人力
           <br/>
           2015年02月15日
            <br/>
           </p>
        </div>
        <div>
            绑定邮箱：
             <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
            <br/>
            <asp:Button ID="btnconfirm" runat="server" Text="同意并授权" OnClick="btnconfirm_Click"/> 
        &nbsp;&nbsp;
       <asp:Button ID="btncancel" runat="server" Text="不同意" OnClick="btncancel_Click" />
       
            </div>
    </div>
        
    </form>
        
</body>
    <script src="../js/Logout.js" type="text/javascript" language="javascript"></script>
<script type="text/javascript">
 
</script>
</html>