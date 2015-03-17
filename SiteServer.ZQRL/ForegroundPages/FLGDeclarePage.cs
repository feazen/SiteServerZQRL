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

using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Threading;


namespace SiteServer.ZQRL.ForegroundPages
{
    public partial class FLGDeclarePage : System.Web.UI.Page
    {
        protected TextBox txtemail;
        string registURL = null;    //注册链接
        string loginURL = null;     //登录链接URL
        string loginId = null;      //用户名
        string userNm = null;       //账户名称
        string mobileNo = null;     //手机号
        string email = null;        //邮箱
        string sex = null;          //性别 男1，女0
        string entryTm = null;      //入职日期 格式："20150122";
        //string firmNo = "zqrl";   //企业账户名称（测试账套）
        string firmNo = "hr-channel";     //企业账户名称（正式账套）
        //string insNo = "2014112010000079";//账号名称     2014122210000024（正式环境）   2014112010000079（测试环境）
        string insNo = "2014122210000024";//正式环境

        protected void Page_Load(object sender, EventArgs e)
        { 
        }

        /// <summary>
        /// 描述：点击确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="E"></param>
        public void btnconfirm_Click(object sender, EventArgs E)
       {
            //注册用户并打开福利高页面
           string inputemail = txtemail.Text;
           if ("".Equals(inputemail))
           {
               Response.Write("<script type='text/javascript'>alert('请填写邮箱');</script>");
               return;
           }
           else {
               email = inputemail;
           }
           registURL = "https://www.fuligold.com/fwp/fuiouEyAction .action";//正式环境
           loginURL = "https://www.fuligold.com/hr-channel";//正式环境
           //string IdCard = Request["IdCard"];
           //string action = Request["action"];
           //Thread thread = Thread.CurrentThread;
           //Thread.Sleep(5000);
            
           if (registFLGUser() && loginId != null && firmNo != null)
           {
               //当用户已经存在或者注册成功时，直接登录
               userFLGLoginbyGet(loginId + "@" + firmNo);
           }
           else
           {
               //其他情况链接到成功网页
               Response.Redirect(loginURL);
           }
        }
        /// <summary>
        /// 点击取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="E"></param>
        public void btncancel_Click(object sender, EventArgs E)
        {
            //取消打开
            Response.Write("<script type='text/javascript'>window.close();</script>");
        }

        //描述：福利高用户登录
        private void userFLGLoginbyGet(string userid)
        {
            string s_today = DateTime.Now.ToString("yyyyMMdd");
            //string codeNo = s_today + "|zqrl0227|" + userid;//测试账套05
            string codeNo = s_today + "|01c094871c|" + userid;//正式账套
            string token = formatMD5(codeNo).ToLower();  //Md5pass为输出加密文本的文本框
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "/loginAction!zqrlLogin?userid=" + userid;
            //postData += ("&password=" + password_md5);
            postData += ("&insNo=" + insNo);
            postData += ("&token=" + token);
            string url = loginURL + postData;
            Response.Redirect(url);
            //showAttentionURL(attentionMSG,url);
        }

     

        /// <summary>
        /// 描述：注册福利高用户
        ///  
        /// </summary>
        /// <returns>返回true</returns>注册成功或者账户已经存在
        private bool registFLGUser()
        {
            //此处需重新初始化账户
            try
            {
                if (Session["UserInfo"] == null)
                    return false;
                UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];

                string token;
                if (usersInfo != null && usersInfo.UserType == 1)
                {
                    long personId = usersInfo.PersonId;
                    PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModel(personId);
                    if (personInfo == null)
                        return false;

                    loginId = usersInfo.Number.Trim();  //用户名
                    userNm = personInfo.PersonName;     //账户名称
                    mobileNo = personInfo.Phone;        //手机号
                    //email = personInfo.Mail;            //邮箱
                    if (personInfo.Sex.Equals("男"))
                        sex = "1";                      //性别 男1，女0
                    else
                        sex = "0";
                    entryTm = personInfo.ContractStartDate.ToString("yyyyMMdd");//入职日期 格式："20150122";

                    //判断账户信息是否存在
                    if (!checkLoginValue())
                        return false;
                    //string str_token = loginId + "|" + mobileNo + "|" + "0306zqrl";//测试账套04
                    string str_token = loginId + "|" + mobileNo + "|" + "e4539e7a64";//正式账套
                    token = formatMD5(str_token).ToLower();
                }
                else
                {
                    return false;
                }
                UTF8Encoding encoding = new UTF8Encoding();
                string postData = "loginId=" + loginId;
                postData += ("&userNm=" + userNm);
                postData += ("&mobileNo=" + mobileNo);
                postData += ("&email=" + email);
                postData += ("&sex=" + sex);
                postData += ("&entryTm=" + entryTm);
                postData += ("&firmNo=" + firmNo);
                postData += ("&insNo=" + insNo);
                //postData += ("&loginPwd=" + loginPwd_md5);
                postData += ("&token=" + token);

                byte[] data = encoding.GetBytes(postData);

                // Prepare web request...
                HttpWebRequest myRequest =
                 (HttpWebRequest)WebRequest.Create(registURL);

                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();

                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                // Get response
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                Console.WriteLine(content);


                //Response.Write("<script languge='javascript'>alert('" + content + "'); </script>");

                if (content != null)
                {
                    string type = content.Substring(0, 4);
                    if ("0000".Equals(type) || "1012".Equals(type))
                    {
                        //新增注册成功
                        DataProviderZQRL.UsersDAO.UpdateFlgRegistStatus(loginId, email);//更新注册状态
                        UsersInfo userInfo = DataProviderZQRL.UsersDAO.GetModel(loginId);
                        Session["UserInfo"] = userInfo;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

       
        /// <summary>
        /// 描述：判断必填信息是否存在
        /// 此处为福利高必填项 注意：邮箱必须拥有并且准确
        /// </summary>
        /// <returns></returns>
        private bool checkLoginValue()
        {
            if (loginId == null || "".Equals(loginId))
                return false;
            else if (userNm == null || "".Equals(userNm))
                return false;
            else if (mobileNo == null || "".Equals(mobileNo))
                return false;
            else if (email == null || "".Equals(email))
                return false;
            else if (sex == null || "".Equals(sex))
                return false;
            else if (entryTm == null || "".Equals(entryTm))
                return false;
            else if (firmNo == null || "".Equals(firmNo))
                return false;
            return true;
        }

        /**
        *描述：MD5，加密字符串 
        *
        *
        */
        private string formatMD5(string s)
        {
            if (s == null)
                return s;
            else
            {
                byte[] result = Encoding.Default.GetBytes(s);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] output = md5.ComputeHash(result);
                String s_md5 = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
                return s_md5;
            }
        }
    }
}
