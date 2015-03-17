using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Xml;
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
    public partial class CompanySalaryInfosByTime : System.Web.UI.Page
    {
        public TextBox Keyword;
        public Button Search;
        public Repeater rptContents;
        protected string pageHtml;
        protected string key;

        protected int pagesize;
        protected int pagenum;

        protected string returnUrl;

        public TextBox lblStartTime;
        public TextBox lblNameOrIdCard;
       
        protected string StartTime = string.Empty;


        protected string NameOrIdCard = string.Empty;
        protected DataSet ds;
        public Label lblSupportStaff;
        protected string login_name = "匿名";
        protected string login_company = "匿名公司";


        protected void Page_Load(object sender, EventArgs e)
        {
            UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];
            if (usersInfo == null || usersInfo.Number == null || usersInfo.Number == "")
            {
                Response.Redirect("../index.htm");
            }
            else
            {
                key = usersInfo.CompanyId.ToString();
                login_name = usersInfo.Number;
                CompanyInfo company = new CompanyInfo();
                company = DataProviderZQRL.CompanyDAO.GetModelByCompanyId(usersInfo.CompanyId);
                if (company != null)
                {
                    login_company = company.Name;
                }
                DataTable dtTable = DataProviderZQRL.CompanyDAO.GetSupportStaffByCompanyId(key);
                string SupportStaff = string.Empty;
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    for (int k = 0; k < dtTable.Rows.Count; k++)
                    {
                        SupportStaff += dtTable.Rows[k]["name"].ToString() + dtTable.Rows[k]["phone"].ToString() + ",";
                    }
                }
                lblSupportStaff.Text = SupportStaff;
            }
            pagesize = TranslateUtils.ToInt(Request["pagesize"], 1);
            pagenum = TranslateUtils.ToInt(Request["pagenum"], 5);

            if (Request["StartTime"] != null && Request["StartTime"] != "")
            {
                StartTime = Request["StartTime"];
                lblStartTime.Text = StartTime;
            }
            if (Request["NameOrIdCard"] != null && Request["NameOrIdCard"] != "")
            {
                NameOrIdCard = Request["NameOrIdCard"];
                lblNameOrIdCard.Text = NameOrIdCard;
            }

            if (!IsPostBack)
            {
                //页面初始化
                LoadHtml();
            }
        }

        /// <summary>
        /// 加载信件
        /// </summary>
        private void LoadHtml()
        {
            int itemid = 0;
            //分页
            string pageSql = GetSql1();
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " id Desc ");

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private DateTime GetDefaultDate()
        {

            DateTime time = DateTime.Now;
            StringBuilder sb = new StringBuilder();

            sb.Append(" select distinct(date) from t_salary where 1=1 ");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and companyid = '{0}' ", key);
            }

            sb.AppendFormat(" order by date desc");

            DataSet ds;
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sb.ToString());
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                time = Convert.ToDateTime(ds.Tables[0].Rows[0][0].ToString()); ;
            }
            return time;


            //sb.Append(" order by date desc ");

            //sb.Append(" select s.id as salaryId,s.personid as salaryPersonId,s.agreementid,s.companyid as salaryCompanyId,s.date,s.salaryDetail, ");
            //sb.Append(" s.SettlementCode,s.SettlementMonth,ea.id as employeeAgreementId,ea.personID as EApersonId,ea.companyid as EAcompanyId,ea.companyname, ");
            //sb.Append(" ea.contractStartDate,ea.contractEndDate,ea.agreementStartDate,ea.agreementEndDate,e.id as employeeId,e.personName,e.idcard as allidcard, ");
            //sb.Append("  case when LEN(e.idcard)=15 then left(e.idcard,4)+'*********'+RIGHT(e.idcard,4)  ");
            //sb.Append("  when LEN(e.idcard)=18 then left(e.idcard,7)+'********'+right(e.idcard,4)  ");
            //sb.Append("  else e.idcard end as idcard, ");
            //sb.Append("e.sex,e.birthday,e.country, ");
            //sb.Append(" e.nation,e.policy,e.familyType,e.phone,e.mail,e.emergencyperson,e.emergencyphone,e.bank,e.openbank, ");
            //sb.Append("  case when LEN(e.bankaccount) >8 then LEFT(e.bankaccount,4) + LEFT('**********',LEN(e.bankaccount)-8)+RIGHT(e.bankaccount,4)  ");
            //sb.Append("  else e.bankaccount end as bankaccount, ");
            //sb.Append("e.fundaccount,e.familyAddress, ");
            //sb.Append(" e.familyPostCode,e.livingAddress,e.livingPostCode,e.taxAddress,e.taxPostCode,e.otherAddress,e.otherPostCode ");
            //sb.Append(" from t_salary s,t_employeeAgreement ea,t_employee e ");
            //sb.Append(" where 1=1 and s.agreementid=ea.id and ea.personID=e.id ");

            //if (!string.IsNullOrEmpty(key))
            //{
            //    sb.AppendFormat(" and ea.companyid like '%{0}%' ", key);
            //}
            //if (!string.IsNullOrEmpty(StartTime))
            //{
            //    string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
            //    sb.AppendFormat(" and date >= '{0}' ", sTime);
            //}
            //if (!string.IsNullOrEmpty(EndTime))
            //{
            //    string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
            //    sb.AppendFormat(" and date <= '{0}' ", eTime);
            //}
            //if (!string.IsNullOrEmpty(NameOrIdCard))
            //{
            //    sb.AppendFormat(" and ( e.personName like '%{0}%' or e.idcard like '%{0}%' ) ", NameOrIdCard);
            //}
            //if (itemid != 0)
            //{
            //    sb.AppendFormat(" and s.id = '{0}' ", itemid);
            //}


            //return sb.ToString();
        }

        private string GetSql1()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select employee.personName,employee.sex,");
            sb.Append("  case when LEN(employee.idcard)=15 then left(employee.idcard,4)+'*********'+RIGHT(employee.idcard,4)  ");
            sb.Append("  when LEN(employee.idcard)=18 then left(employee.idcard,7)+'********'+right(employee.idcard,4)  ");
            sb.Append("  else employee.idcard end as idcard, ");
            sb.Append(" salary.* from  t_salary as salary  left join t_employee as employee  on (salary.personid=employee.id) where 1=1 ");
            sb.AppendFormat(" and salary.companyid = '{0}' ", key);
            if (!string.IsNullOrEmpty(StartTime))
            {
                DateTime sTime = Convert.ToDateTime(StartTime.Replace("年", "-"));
                sb.AppendFormat(" and salary.date >= '{0}' ", sTime.Date);
                sb.AppendFormat(" and salary.date <'{0}' ", sTime.Date.AddMonths(1));
            }
            else
            {
                this.GetDefaultDate();
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( employee.personName like '%{0}%' or employee.idcard like '%{0}%' or employee.nickname like '%{0}%' ) ", NameOrIdCard);
            }
             
            return sb.ToString();
        }


        protected string _pageUrl;
        protected string PageUrl
        {
            get
            {
                if (StringUtils.IsNullorEmpty(this._pageUrl))
                {
                    this._pageUrl = string.Format("CompanySalaryInfosByTime.aspx?StartTime={0}&NameOrIdCard={1}", StartTime, NameOrIdCard);
                }
                return this._pageUrl;
            }
        }

        protected void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                int itemid = Convert.ToInt32(rowv["id"]);

                Literal ltlTitle = rptContents.Controls[0].FindControl("ltlTitle") as Literal;

                Literal ltlValue = e.Item.FindControl("ltlValue") as Literal;

                StringBuilder sbTitle = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                if (e.Item.ItemIndex == 0)
                {
                    sbTitle.AppendFormat("<td style='background-color: #a9cfe5;height: 36px;line-height: 36px;color: #fff;text-align: center;font-size: 13px;'>{0}</td>", "日期");
                    sbTitle.AppendFormat("<td style='background-color: #a9cfe5;height: 36px;line-height: 36px;color: #fff;text-align: center;font-size: 13px;'>{0}</td>", "序号");
                    sbTitle.AppendFormat("<td style='background-color: #a9cfe5;height: 36px;line-height: 36px;color: #fff;text-align: center;font-size: 13px;'>{0}</td>", "姓名");
                    sbTitle.AppendFormat("<td style='background-color: #a9cfe5;height: 36px;line-height: 36px;color: #fff;text-align: center;font-size: 13px;'>{0}</td>", "身份证");

                }


                sbValue.Append("<tr>");
                sbValue.AppendFormat("<td height='2' align='center' class='u9'>{0}</td>", rowv["date"].ToString().Split(' ')[0].Replace('/','-'));
                sbValue.AppendFormat("<td height='2' align='center' class='u9'>{0}</td>", (e.Item.ItemIndex + 1).ToString());
                sbValue.AppendFormat("<td height='2' align='center' class='u9'>{0}</td>", rowv["personName"].ToString());
                sbValue.AppendFormat("<td height='2' align='center' class='u9'>{0}</td>", rowv["idcard"].ToString());


                string salaryDetail = rowv["salaryDetail"].ToString();
                XmlDocument doc = new XmlDocument();
                byte[] encodedString = Encoding.UTF8.GetBytes(salaryDetail);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(encodedString);
                ms.Flush();
                doc.Load(ms);

                XmlNodeList xxList = doc.GetElementsByTagName("SettlementSalaryItemInfo");

                foreach (XmlNode xxNode in xxList)
                {
                    if (e.Item.ItemIndex == 0)
                    {
                        sbTitle.AppendFormat("<td style='background-color: #a9cfe5;height: 36px;line-height: 36px;color: #fff;text-align: center;font-size: 13px;'>{0}</td>", xxNode.Attributes["SalaryItemName"].Value);
                    }

                    sbValue.AppendFormat("<td height='2' align='center' class='u9'>{0}</td>", xxNode.Attributes["Result"].Value);

                }
                sbValue.Append("</tr>");

                if (e.Item.ItemIndex == 0)
                {
                    ltlTitle.Text = sbTitle.ToString();
                }
                ltlValue.Text = sbValue.ToString();

            }
        }

        public class SalaryItem
        {
            public string ItemName { get; set; }
            public string ItemValue { get; set; }
        }

        public void exportExcel_OnClick(object sender, EventArgs E)
        {
            StartTime = lblStartTime.Text;
            NameOrIdCard = lblNameOrIdCard.Text;
            string pageSql = GetSql1();
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    this.ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, pageSql.ToString());
                }
                catch (Exception e)
                {
                    throw new Exception("导出数据失败！\n" + e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            DataTable dtTable = this.ds.Tables[0];
            List<string> valueList = new List<string>();
            List<string> titleList = new List<string>();
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                valueList.Add((i + 1).ToString());
                valueList.Add(TranslateUtils.ToDateTime(dtTable.Rows[i]["date"].ToString()).ToString("yy-MM-dd"));
                valueList.Add(dtTable.Rows[i]["personName"].ToString());
                valueList.Add(dtTable.Rows[i]["sex"].ToString());
                valueList.Add(dtTable.Rows[i]["idcard"].ToString());

                string salaryDetail = dtTable.Rows[i]["salaryDetail"].ToString();
                XmlDocument doc = new XmlDocument();
                byte[] encodedString = Encoding.UTF8.GetBytes(salaryDetail);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(encodedString);
                ms.Flush();
                doc.Load(ms);

                XmlNodeList xxList = doc.GetElementsByTagName("SettlementSalaryItemInfo");

                foreach (XmlNode xxNode in xxList)
                {
                    valueList.Add(xxNode.Attributes["Result"].Value);
                    titleList.Add(xxNode.Attributes["SalaryItemName"].Value);
                }
            }
            titleList = titleList.Distinct().ToList();

            List<string> nameList = new List<string>();
            nameList.Add("序号");
            nameList.Add("日期");
            nameList.Add("姓名");
            nameList.Add("性别");
            nameList.Add("身份证");
            int count = 5;
            for (int i = 0; i < titleList.Count; i++)
            {
                nameList.Add(titleList[i]);
                count++;
            }

            string docFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            string filePath = PathUtils.GetTemporaryFilesPath(docFileName);

            CSVObject scvObject = new CSVObject();
            scvObject.ExportCSVFile(filePath, nameList, valueList, count );

            string fileUrl = PageUtils.GetTemporaryFilesUrl(docFileName);
            Response.Write("<script languge='javascript'>alert('导出成功!');  window.open('" + fileUrl + "')</script>");
        }
    }
}

