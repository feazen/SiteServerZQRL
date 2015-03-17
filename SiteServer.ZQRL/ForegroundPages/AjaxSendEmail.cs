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
    public partial class AjaxSendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cNumber = Request["CNumber"];
            string action = Request["action"];
            CompanyInfo companyInfo = null;
            //PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModelByEmail(loginId);
            if (action == "register")
            {
                 companyInfo = DataProviderZQRL.CompanyDAO.GetModelByCNumberWithReg(cNumber);
            }
            else if (action == "findPassword")
            {
                string LoginName = Request["LoginName"];

                if (string.IsNullOrEmpty(LoginName))
                {
                    companyInfo = DataProviderZQRL.CompanyDAO.GetModelByCNumberWithReg(cNumber);
                 }
                else
                {
                    UsersInfo userInfo = DataProviderZQRL.UsersDAO.GetModel(LoginName);
                    if (userInfo != null)
                    {
                        //companyInfo = DataProviderZQRL.CompanyDAO.GetModelByCNumberWithReg(userInfo.IdCard);
                        companyInfo = DataProviderZQRL.CompanyDAO.GetModelByCompanyId(userInfo.CompanyId);
                    }
                 }
            }


            if (companyInfo != null && !string.IsNullOrEmpty(companyInfo.Mail))
            {
                string strMobileCode = SmsHelper.GenerateMobileCode();
                Session["MobileCode"] = strMobileCode;

                string strSMS = string.Empty;
                if (action == "register")
                {
                    strSMS = string.Format("您在中企人力信息平台的账户注册验证码为{0}，请尽快输入以免失效，谢谢！", strMobileCode);
                }
                else if (action == "findPassword")
                {
                    strSMS = string.Format("您在中企人力信息平台的账户是：{0},账户注册验证码为{1}，请尽快输入以免失效，谢谢！",Request["LoginName"], strMobileCode);
                }

                string strResult = EmailHelper.SendEmailByGovernmentEmail(string.Empty, companyInfo.Mail, "企业会员注册验证码", strSMS);
                if (strResult == "1")
                {
                    Response.Write("1");
                }
                else
                {
                    Response.Write(strResult);
                }
            }
            else
            {
                Response.Write("0");
            }
        }
    }
}
