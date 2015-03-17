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
using System.Data.SqlClient;
using System.Collections;


namespace SiteServer.ZQRL.ForegroundPages
{
    public partial class AjaxWindowsService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SendLoginSMSAndEmail();

            this.SendEffectDateSMSAndEmail();
             
        }

        public void SendEffectDateSMSAndEmail()
        {
            double days = 0;
            DataTable dt = this.GetEffectDateDataSet().Tables[0];
            Hashtable companyEmailHashtable = new Hashtable();
            Dictionary<string, ArrayList> companyDictionary = new System.Collections.Generic.Dictionary<string, ArrayList>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string personName = dt.Rows[i]["personName"].ToString();
                    string phone = dt.Rows[i]["phone"].ToString();
                    string companyid = dt.Rows[i]["companyid"].ToString();
                    string companyname = dt.Rows[i]["companyname"].ToString();
                    string mail = dt.Rows[i]["mail"].ToString();
                    string effectDate = dt.Rows[i]["effectDate"].ToString();

                    if (!string.IsNullOrEmpty(effectDate))
                    {
                        DateTime theEffectDate = TranslateUtils.ToDateTime(effectDate);
                        if ((theEffectDate - DateTime.Now).TotalDays == 60 || (theEffectDate - DateTime.Now).TotalDays == 30)
                        {
                            try
                            {
                                days = (theEffectDate - DateTime.Now).TotalDays;
                                if (TranslateUtils.ToLong(companyid) <= 0)
                                {
                                    csharpEmppApi.SendSMS(phone, string.Format("【中企人力】您的居住证有效期还有{0}天即将过期，请尽快与客服联系以免您的居住证积分失效。", days));
                                }
                                else
                                {
                                    if (!companyEmailHashtable.ContainsKey(companyid))
                                    {
                                        companyEmailHashtable.Add(companyid,mail);
                                    }

                                    ArrayList employeeArrayList = new ArrayList();
                                    if (!companyDictionary.ContainsKey(companyid))
                                    {
                                        employeeArrayList.Add(personName);
                                        companyDictionary.Add(companyid, employeeArrayList);
                                    }
                                    else
                                    {
                                        employeeArrayList.Add(personName);
                                    }
                                    //EmailHelper.SendEmailByGovernmentEmail(string.Empty, mail, "贵司员工居住证过期提醒", string.Format("【中企人力】1、贵司的{0}居住证有效期还有{1}天即将过期，请尽快与客服联系以免您的居住证积分失效。", "", days));
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
            }

            if (companyDictionary.Count > 0)
            {
                foreach (string key in companyDictionary.Keys)
                { 
                    string mail = companyEmailHashtable[key].ToString();
                    ArrayList employeeArrayList = companyDictionary[key] as ArrayList;
                    if (!string.IsNullOrEmpty(mail))
                    {
                        EmailHelper.SendEmailByGovernmentEmail(string.Empty, mail, "贵司员工居住证过期提醒", string.Format("【中企人力】1、贵司的{0}居住证有效期还有{1}天即将过期，请尽快与客服联系以免您的居住证积分失效。", TranslateUtils.ObjectCollectionToSqlInStringWithoutQuote(employeeArrayList), days));
                    }
                }
            }
        }

        public void SendLoginSMSAndEmail()
        {

            DataTable dt = this.GetLoginDateDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string number = dt.Rows[i]["number"].ToString();
                    string uemail = dt.Rows[i]["uemail"].ToString();
                    string cmail = dt.Rows[i]["cmail"].ToString();
                    string usertype = dt.Rows[i]["usertype"].ToString();
                    string lastLoginTime = dt.Rows[i]["lastLoginTime"].ToString();
                    if (!string.IsNullOrEmpty(lastLoginTime))
                    { 
                        DateTime theLastLoginTime = TranslateUtils.ToDateTime(lastLoginTime);

                        if ((theLastLoginTime - DateTime.Now).TotalDays == 30)
                        {
                            if (usertype == "1")
                            {
                                if (!string.IsNullOrEmpty(uemail))
                                {
                                    EmailHelper.SendEmailByGovernmentEmail(string.Empty, uemail, string.Format("{0}，请查询您的各项福利！", number), string.Format("亲爱的{0}：<br/>我们有好几个星期没在 中企人力 上见到您了。<br/>马上登录 中企人力 ，您将直观的看到您的各项最新福利政策！<br/><br/><a href='http://www.hr-channel.com'>登录</a><br/>（按钮点击直接链接到www.hr-channel.com）", number));
                                }
                            }
                            else if (usertype == "2")
                            {
                                if (!string.IsNullOrEmpty(cmail))
                                {
                                    EmailHelper.SendEmailByGovernmentEmail(string.Empty, cmail, string.Format("{0}，请查询您的各项福利！", number), string.Format("亲爱的{0}：<br/>我们有好几个星期没在 中企人力 上见到您了。<br/>马上登录 中企人力 ，您将直观的看到您的各项最新福利政策！<br/><br/><a href='http://www.hr-channel.com'>登录</a><br/>（按钮点击直接链接到www.hr-channel.com）", number));
                                }
                            }
                        }
                    }
                }
            }
        }

        public DataSet GetEffectDateDataSet()
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("select p.personName,p.phone,e.companyid, e.companyname,c.mail,r.effectDate from  t_residencemanage  as r ");
            sbSql.Append("left join t_employeeAgreement as e on(e.id=r.agreementid) ");
            sbSql.Append(" left join t_employee as p on(p.id=r.personid)");
            sbSql.Append("left join t_company as c on(e.companyid=c.id)");
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    return SqlHelper.ExecuteDataset(conn, CommandType.Text, sbSql.ToString());
                }
                catch (Exception e)
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public DataSet GetLoginDateDataSet()
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("select u.number,u.usertype,u.lastLoginTime,e.mail as uemail,c.mail as cmail from t_user as u ");
            sbSql.Append(" left join t_employee as e on (u.personid=e.id) left join t_company as c on(u.companyid=c.id) ");
            
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    return SqlHelper.ExecuteDataset(conn, CommandType.Text, sbSql.ToString());
                }
                catch (Exception e)
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
         
    }
}
