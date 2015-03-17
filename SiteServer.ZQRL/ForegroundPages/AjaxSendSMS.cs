using System;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using BaiRong.Core;
//using UserCenter.Core;
using System.Web.UI.HtmlControls;
using System.Web;
using BaiRong.Controls;
using SiteServer.ZQRL;
using System.Collections.Generic;
using System.Data;
using SiteServer.ZQRL.Core.Data;
 

namespace SiteServer.ZQRL.ForegroundPages
{
    public partial class AjaxSendSMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string IdCard = Request["IdCard"];
            string action = Request["action"];
            PersonInfo personInfo = null;
            if (action == "register")
            {
                personInfo = DataProviderZQRL.PersonDAO.GetModelByIdCardWithReg(IdCard);
            }
            else if (action == "findPassword")
            {
                string LoginName = Request["LoginName"];
                if (string.IsNullOrEmpty(LoginName))
                {
                    personInfo = DataProviderZQRL.PersonDAO.GetModelByIdCardWithReg(IdCard);
                }
                else
                {
                    UsersInfo userInfo = DataProviderZQRL.UsersDAO.GetModel(LoginName);
                    if (userInfo != null)
                    {
                        personInfo = DataProviderZQRL.PersonDAO.GetModelByIdCardWithReg(userInfo.IdCard);
                    }
                }
            }
             
            if (personInfo != null && !string.IsNullOrEmpty(personInfo.Phone))
            {
                string strMobileCode = SmsHelper.GenerateMobileCode();
                Session["MobileCode"] = strMobileCode;
                //string strSMS = string.Format("您的验证码是{0}", strMobileCode);
                string strSMS = string.Empty;
                if (action == "register")
                {
                   strSMS= string.Format("您在中企人力信息平台的账户注册验证码为{0}，请尽快输入以免失效，谢谢！", strMobileCode);
                }
                else if (action == "findPassword")
                {
                    strSMS = string.Format("您在中企人力信息平台的账户是:{0},账户注册验证码为:{1}，请尽快输入以免失效，谢谢！", Request["LoginName"], strMobileCode);
                }
                    
                //string strResult = SmsHelper.SendSMS(personInfo.Phone, strSMS);
                string strResult = csharpEmppApi.SendSMS(personInfo.Phone, strSMS);
                if (strResult == "1")
                {
                    Response.Write("1");
                }
                else
                {
                    Response.Write(strResult);
                }
            }
        }
    }
}
