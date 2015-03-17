using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.Net.Mail;

namespace SiteServer.ZQRL
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件。
        /// </summary>
        /// <param name="sender">发送者。</param>
        /// <param name="toEmail">接收邮件地址。</param>
        /// <param name="emailSubject">邮件主题。</param>
        /// <param name="emailBody">邮件内容。</param>
        /// <returns>空值：发送成功；非空值：发送失败原因。</returns>
        public static string SendEmailByGovernmentEmail(string sender, string toEmail, string emailSubject, string emailBody)
        {
            try
            {
                MailMessage email = new MailMessage();
                //email.From = new MailAddress("event@pantum.cn", sender);
                email.From = new MailAddress("huhu@hr-channel.com", sender);
                email.To.Add(toEmail);//收件人邮箱地址可以是多个以实现群发
                email.Subject = emailSubject;
                email.Body = emailBody;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = true; //是否为html格式 
                email.Priority = MailPriority.Normal;  //发送邮件的优先等级
                SmtpClient sc = new SmtpClient();
                //sc.Host = "smtp.exmail.qq.com";      //指定发送邮件的服务器地址或IP
                sc.Host = "smtp.ym.163.com";
                //sc.Port = 25;//指定发送邮件端口
                sc.Port = 25;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;//指定如何发送电子邮件
                sc.UseDefaultCredentials = false;//是否随请求一起发送
                sc.EnableSsl = false;//安全连接设置
                //sc.Credentials = new System.Net.NetworkCredential("event@pantum.cn", "PANtum6259251"); //指定登录服务器的用户名和密
                sc.Credentials = new System.Net.NetworkCredential("huhu@hr-channel.com", "111111"); //指定登录服务器的用户名和密
                sc.Send(email);   
            }
            catch (Exception e)
            {
                //return "邮件发送失败（原因：" + e.Message + "）";
                return e.Message;
            }

            return "1";
            //return string.Empty;
        }
    }
}
