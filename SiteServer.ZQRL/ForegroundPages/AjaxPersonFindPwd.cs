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
using BaiRong.Core.Cryptography;

namespace SiteServer.ZQRL.ForegroundPages
{
    public partial class AjaxPersonFindPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (action == "findPwd")
            {
                PersonFindPwd();
            }
            else if (action == "CheckLoginName")
            {
                LoginNameCheck();
            }
            else if (action == "CheckIdCard")
            {
                IdCardCheck();
            }
            else
            {
                PersonFindPwd();
            }
         }

        public void ChangePwd(PersonInfo personInfo, string MobileCode, string VerifyCode, string LoginId,string IdCard, string PassWord, string type)
        {  
                if (MobileCode == VerifyCode)
                {
                    string thePassword = string.Empty;
                    byte[] b = Encoding.UTF8.GetBytes(PassWord);
                    b = new MD5CryptoServiceProvider().ComputeHash(b);
                    for (int i = 0; i < b.Length; i++)
                        thePassword += b[i].ToString("x").PadLeft(2, '0');

                    bool isSuccess = DataProviderZQRL.UsersDAO.ChangePwd(LoginId,IdCard, thePassword,type);

                    if (isSuccess)
                    {
                        Response.Write("0000");
                    }
                    else
                    {
                        Response.Write("0003");
                    }
                }
                else
                {
                    Response.Write("0001");
                }
         }

        protected void PersonFindPwd()
        {
            string LoginId = Request["LoginId"].ToString().Trim();
            string PassWord = Request["PassWord"].ToString().Trim();
            string VerifyCode = Request["VerifyCode"].ToString().Trim();
            string IdCard = Request["IdCard"].ToString().Trim();
            string Type = Request["type"].ToString().Trim();


            string MobileCode = string.Empty;
            if (Session["MobileCode"] != null && !string.IsNullOrEmpty(Session["MobileCode"].ToString()))
            {
                MobileCode = Session["MobileCode"].ToString().Trim();
            }

            if (Type == "0")
            {
                if (DataProviderZQRL.UsersDAO.IsExistUserByLoginName(LoginId))
                {
                    UsersInfo userInfo = DataProviderZQRL.UsersDAO.GetModel(LoginId);
                    PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModelByIdCardWithReg(userInfo.IdCard);
                    if (personInfo != null)
                    {
                        ChangePwd(personInfo, MobileCode, VerifyCode, LoginId, IdCard, PassWord, Type);
                    }
                    else
                    {
                        Response.Write("0002");
                    }
                }
                else
                {
                    Response.Write("0002");
                }
            }
            else if (Type == "1")
            {
                PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModelByIdCardWithReg(IdCard);
                if (personInfo != null)
                {
                    ChangePwd(personInfo, MobileCode, VerifyCode, LoginId, IdCard, PassWord, Type);
                }
                else
                {
                    Response.Write("0002");
                }
            }
        }

        protected void LoginNameCheck()
        {
            string LoginId = Request["LoginName"].ToString().Trim();
            if (DataProviderZQRL.UsersDAO.IsExistUserByLoginName(LoginId))
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
            if (DataProviderZQRL.UsersDAO.IsExistUserByIdCard(IdCard))
            {
                Response.Write("0000");
            }
            else
            {
                Response.Write("0003");
            }
        }
     }
}
