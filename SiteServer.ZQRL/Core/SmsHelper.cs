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

namespace SiteServer.ZQRL
{
    public class SmsHelper
    {
        /// <summary>
        /// 1 操作成功;0 帐户格式不正确(正确的格式为:员工编号@企业编号)
        /// -1 服务器拒绝(速度过快、限时或绑定IP不对等)如遇速度过快可延时再发
        /// -2 密钥不正确
        /// -3 密钥已锁定
        /// -4 参数不正确(内容和号码不能为空，手机号码数过多，发送时间错误等)
        /// -5 无此帐户
        /// -6 帐户已锁定或已过期
        /// -7 帐户未开启接口发送
        /// -8 不可使用该通道组
        /// -9 帐户余额不足
        /// -10 内部错误
        /// -11 扣费失败
        /// </summary>
        /// <param name="strMobileNum">手机号码</param>
        /// <param name="strSMS">短信内容</param>
        /// <returns></returns>
        public static string SendSMS(string strMobileNum, string strSMS)
        {
            string strResult = string.Empty;
            string sendurl = "http://smsapi.c123.cn/OpenPlatform/OpenApi";
            string ac = "1001@500994300001";			//用户名
            string authkey = "A4FC84C70E77ABFB94467FB16C68BBDB";	//密钥
            string cgid = "2134";  //通道组编号
            string csid = "1";
            string m = strMobileNum;  //发送号码
            string c = strSMS;  //签名编号 ,可以为空时，使用系统默认的编号
            string action = "sendOnce";
            StringBuilder sbTemp = new StringBuilder();
            //POST 传值
            sbTemp.Append("action=" + action + "&ac=" + ac + "&authkey=" + authkey + "&m=" + m + "&cgid=" + cgid + "&csid=" + csid + "&c=" + c);
            byte[] bTemp = System.Text.Encoding.GetEncoding("utf-8").GetBytes(sbTemp.ToString());
            string postReturn = doPostRequest(sendurl, bTemp);
            Regex linkReg = new Regex("result=(.+)/>");
            MatchCollection linkCollection = linkReg.Matches(postReturn);
            string str = linkCollection[0].Groups[1].Value;
            str = str.Replace(">", "");
            strResult = str.Replace("\"", "");
            return strResult;
        }

        //POST方式发送得结果
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                //WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                //WriteErrLog(err.ToString());
            }

            return strResult;
        }

        public static string GenerateMobileCode()
        {
            int number;
            char code;
            string MobileCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('0' + (char)(number % 10));

                ////大小写“O”喔和“0”零，改为字母"p"
                //if (code == 'O' || code == 'o' || code == '0')
                //    code = 'p';

                MobileCode += code.ToString();
            }
            
            return MobileCode;
        }
    }
}
