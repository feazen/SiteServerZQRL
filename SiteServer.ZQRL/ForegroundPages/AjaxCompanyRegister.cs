using System;
using System.Security.Cryptography;
using System.Text;
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
    public partial class AjaxCompanyRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (action == "register")
            {
                CompanyRegister();
            }
            else if (action == "checkLoginName")
            {
                LoginNameCheck();
            }
            else if (action == "checkIdCard")
            {
                IdCardCheck();
            }
            else if (action == "checkVerifyCode")
            {
                VerifyCodeCheck();
            }
            else
            {
                CompanyRegister();
            }
        }

        protected void CompanyRegister()
        {
            string LoginId = Request["LoginId"].ToString().Trim();
            string PassWord = Request["PassWord"].ToString().Trim();
            string VerifyCode = Request["VerifyCode"].ToString().Trim();
            string IdCard = Request["IdCard"].ToString().Trim();

            string MobileCode = string.Empty;
            if (Session["MobileCode"] != null && !string.IsNullOrEmpty(Session["MobileCode"].ToString()))
            {
                MobileCode = Session["MobileCode"].ToString().Trim();
            }

            //PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModelByIdCardWithReg(IdCard);
            if (!DataProviderZQRL.UsersDAO.IsExistUserByLoginName(LoginId, IdCard))
            {
                CompanyInfo companyInfo = DataProviderZQRL.CompanyDAO.GetModelByCNumberWithReg(IdCard);
                if (companyInfo != null && !string.IsNullOrEmpty(companyInfo.Mail))
                {
                    if (MobileCode == VerifyCode)
                    {
                        UsersInfo usersInfo = new UsersInfo();
                        usersInfo.Number = LoginId;
                        byte[] b = Encoding.UTF8.GetBytes(PassWord);
                        b = new MD5CryptoServiceProvider().ComputeHash(b);
                        for (int i = 0; i < b.Length; i++)
                            usersInfo.PassWord += b[i].ToString("x").PadLeft(2, '0');
                        // usersInfo.PassWord = PassWord;
                        usersInfo.IdCard = IdCard;
                        usersInfo.UserType = 2;
                        usersInfo.PersonId = 0;
                        usersInfo.CompanyId = companyInfo.ID;

                        long num = DataProviderZQRL.UsersDAO.Add(usersInfo);
                        if (num > 0)
                        {
                            Session["UserInfo"] = DataProviderZQRL.UsersDAO.GetModelById(num);
                            Response.Write("0000");
                        }
                    }
                    else
                    {
                        Response.Write("0001");
                    }

                }
                else
                {
                    Response.Write("0002");
                }
            }
            else
            {
                Response.Write("0003");
            }
        }

        protected void LoginNameCheck()
        {
            string LoginId = Request["LoginId"].ToString().Trim();
            if (!DataProviderZQRL.UsersDAO.IsExistUserByLoginName(LoginId))
            {
                Response.Write("0000");
            }
            else
            {
                Response.Write("0003");
            }
        }
        protected void IdCardCheck()
        {
            string IdCard = Request["IdCard"].ToString().Trim();
            if (DataProviderZQRL.CompanyDAO.IsExistIdCard(IdCard))
            {
                if (!DataProviderZQRL.UsersDAO.IsExistUserByIdCard(IdCard))    
                {
                    Response.Write("0000");//已注册
                }
                else
                {
                    Response.Write("0002");//未注册
                }
            }
            else
            {
                Response.Write("0003"); //不存在
            }
        }

        protected void VerifyCodeCheck()
        {
            string VerifyCode = Request["VerifyCode"].ToString().Trim();

            string MobileCode = string.Empty;
            if (Session["MobileCode"] != null && !string.IsNullOrEmpty(Session["MobileCode"].ToString()))
            {
                MobileCode = Session["MobileCode"].ToString().Trim();
            }
            if (MobileCode == VerifyCode)
            {
                Response.Write("0000");
            }
            else
            {
                Response.Write("0001");
            }
        }
    }
}
