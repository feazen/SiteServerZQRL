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
    public partial class AjaxPersonRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (action == "register")
            {
                PersonRegister();
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
                PersonRegister();
            }
        }

        protected void PersonRegister()
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

            if (!DataProviderZQRL.UsersDAO.IsExistUserByLoginName(LoginId, IdCard))
            {
                PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModelByIdCardWithReg(IdCard);
                if (personInfo != null && !string.IsNullOrEmpty(personInfo.Phone))
                {
                    if (MobileCode == VerifyCode)
                    {
                        UsersInfo usersInfo = new UsersInfo();
                        usersInfo.Number = LoginId;
                        byte[] b = Encoding.UTF8.GetBytes(PassWord);
                        b = new MD5CryptoServiceProvider().ComputeHash(b);
                        for (int i = 0; i < b.Length; i++)
                            usersInfo.PassWord += b[i].ToString("x").PadLeft(2, '0');
                        //usersInfo.PassWord = PassWord;
                        usersInfo.IdCard = IdCard;
                        usersInfo.UserType = 1;
                        usersInfo.PersonId = personInfo.ID;
                        usersInfo.CompanyId = 0;

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
            if (!DataProviderZQRL.UsersDAO.IsExistUserByIdCard(IdCard))
            {
                if (DataProviderZQRL.PersonDAO.IsExistUserByIdCard(IdCard))
                {
                    Response.Write("0000");
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
