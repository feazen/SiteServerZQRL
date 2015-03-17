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
    public partial class AjaxLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
                action = Request.Form["action"];
            string outPutStr = "";
            switch (action)
            {
                case "NorLogin":
                    string nLoginName = Request["nLoginName"];
                    string nPwd = Request["NPwd"];
                    outPutStr = ValidateNorLogin(nLoginName, nPwd);
                    break;
                case "ComLogin":
                    string cLoginName = Request["CLoginName"];
                    string cPwd = Request["CPwd"];
                    outPutStr = ValidateComLogin(cLoginName, cPwd);
                    break;
                default:
                    break;
            }

            Response.Write(outPutStr);
        }


        private string ValidateNorLogin(string nLoginName, string nPwd)
        {
            string value = "false";

            //if ((DateTime.Now - TranslateUtils.ToDateTime("2015-01-10")).TotalDays >= 0)
            //{
            //    //Response.Redirect("message.html");
            //    value = "message";
            //}
            //else
            //{
                try
                {
                    UsersInfo usersInfo = new UsersInfo();
                    string MD5pwd = string.Empty;

                    byte[] b = Encoding.UTF8.GetBytes(nPwd);
                    b = new MD5CryptoServiceProvider().ComputeHash(b);
                    for (int i = 0; i < b.Length; i++)
                        MD5pwd += b[i].ToString("x").PadLeft(2, '0');

                    usersInfo = DataProviderZQRL.UsersDAO.GetModel(nLoginName, MD5pwd);
                    Session["UserInfo"] = usersInfo;
                    if (usersInfo != null && usersInfo.Number.Trim().Equals(nLoginName.Trim()) && usersInfo.PassWord.Equals(MD5pwd) && usersInfo.UserType == 1)
                    {
                        value = "true";
                        DataProviderZQRL.UsersDAO.UpdateLastLoginTime(nLoginName);
                    }
                    else
                    {
                        value = "false";
                    }
                }
                catch (Exception)
                {
                    value = "false";
                }
            //}
            return value;

        }

        private string ValidateComLogin(string cLoginName, string cPwd)
        {
            string value = "false";

            //if ((DateTime.Now - TranslateUtils.ToDateTime("2015-01-10")).TotalDays >= 0)
            //{
            //    //Response.Redirect("message.html");
            //    value = "message";
            //}
            //else
            //{

                try
                {
                    UsersInfo usersInfo = new UsersInfo();
                    string MD5pwd = string.Empty;

                    byte[] b = Encoding.UTF8.GetBytes(cPwd);
                    b = new MD5CryptoServiceProvider().ComputeHash(b);
                    for (int i = 0; i < b.Length; i++)
                        MD5pwd += b[i].ToString("x").PadLeft(2, '0');

                    usersInfo = DataProviderZQRL.UsersDAO.GetModel(cLoginName, MD5pwd);
                    Session["UserInfo"] = usersInfo;
                    if (usersInfo != null && usersInfo.Number.Trim().Equals(cLoginName.Trim()) && usersInfo.PassWord.Equals(MD5pwd) && usersInfo.UserType == 2)
                    {
                        value = "true";
                        DataProviderZQRL.UsersDAO.UpdateLastLoginTime(cLoginName);
                    }
                    else
                    {
                        value = "false";
                    }
                }
                catch (Exception)
                {

                    value = "false";
                }
            //}
            return value;
        }

    }
}
