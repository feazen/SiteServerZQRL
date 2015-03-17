using System;
using System.Reflection;
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

namespace SiteServer.ZQRL.ForegroundPages
{
    public partial class EmployeeInfos: System.Web.UI.Page
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
        public TextBox lblEndTime;
        public TextBox lblNameOrIdCard;
        protected string NameOrIdCard = string.Empty;
        protected string StartTime = string.Empty;
        protected string EndTime = string.Empty;
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
                CompanyInfo company=new CompanyInfo();
                company = DataProviderZQRL.CompanyDAO.GetModelByCompanyId(usersInfo.CompanyId);
                if (company!=null)
                {
                    login_company = company.Name;
                }
                DataTable dtTable = DataProviderZQRL.CompanyDAO.GetSupportStaffByCompanyId(key);
                string SupportStaff = string.Empty;
                if (dtTable!=null&&dtTable.Rows.Count>0)
                {
                    for (int k = 0; k < dtTable.Rows.Count; k++)
                    {
                        SupportStaff += dtTable.Rows[k]["name"].ToString() + dtTable.Rows[k]["phone"].ToString() + ",";
                    }
                }
                lblSupportStaff.Text = SupportStaff;
                //PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModel(usersInfo.PersonId);
                //if (personInfo != null)
                //{
                //    key = personInfo.ID.ToString();
                //}
                //key = usersInfo.Number;
            }
            pagesize = TranslateUtils.ToInt(Request["pagesize"], 1);
            pagenum = TranslateUtils.ToInt(Request["pagenum"], 5);

            if (Request["StartTime"] != null && Request["StartTime"] != "")
            {
                StartTime = Request["StartTime"];
                lblStartTime.Text = StartTime;
            }

            if (Request["EndTime"] != null && Request["EndTime"] != "")
            {
                EndTime = Request["EndTime"];
                lblEndTime.Text = EndTime;
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
            //分页
            string pageSql = GetSql();
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " contractStartDate Desc ");

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private string GetSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  select e.id as employeeId,e.personID,e.companyid,e.companyname,e.contractStartDate,e.contractEndDate, ");
            sb.Append("  e.agreementStartDate,e.agreementEndDate,e.Department,e.Position,e.JobNumber, ");
            sb.Append("  b.id, b.personName,b.idcard as allidcard, ");
            sb.Append("  case when LEN(b.idcard)=15 then left(b.idcard,4)+'*********'+RIGHT(b.idcard,4)  ");
		    sb.Append("  when LEN(b.idcard)=18 then left(b.idcard,7)+'********'+right(b.idcard,4)  ");
		    sb.Append("  else b.idcard end as idcard, ");
            sb.Append("  b.sex, b.birthday, b.country, b.nation, b.policy, b.familyType, b.phone, b.mail, b.emergencyperson, ");
            sb.Append("  b.emergencyphone, b.bank, b.openbank,  ");
            sb.Append("  case when LEN(b.bankaccount) >8 then LEFT(b.bankaccount,4) + LEFT('**********',LEN(b.bankaccount)-8)+RIGHT(b.bankaccount,4)  ");
		    sb.Append("  else b.bankaccount end as bankaccount, ");
            sb.Append("  b.fundaccount, b.familyAddress, b.familyPostCode, b.livingAddress, b.livingPostCode, ");
            sb.Append("  b.taxAddress, b.taxPostCode, b.otherAddress, b.otherPostCode, b.hometown, b.education, b.graduateschool, b.maritalstatus, b.childs, ");
            sb.Append("  b.cityname, b.accountType, b.emergencyperson2, b.emergencyphone2, b.childname1, b.childbirthday1, b.childname2, b.childbirthday2,b.nickname ");
            sb.Append("  from t_employeeAgreement e,t_employee b where 1=1 and e.personID=b.id ");

            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and companyid like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and contractStartDate >= '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and contractStartDate <= '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( b.personName like '%{0}%' or b.idcard like '%{0}%' or b.nickname like '%{0}%' ) ", NameOrIdCard);
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
                    //this._pageUrl = string.Format("NationalWelfareInfos.aspx?key={0}", key);
                    //this._pageUrl = "NationalWelfareInfos.aspx";
                    this._pageUrl = string.Format("EmployeeInfos.aspx?StartTime={0}&EndTime={1}", StartTime, EndTime);
                }
                return this._pageUrl;
            }
        }

        public void exportExcel_OnClick(object sender, EventArgs E)
        {
            StartTime = lblStartTime.Text;
            EndTime = lblEndTime.Text;
            NameOrIdCard = lblNameOrIdCard.Text;

            string pageSql = GetSql();
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
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                valueList.Add((i+1).ToString());
                valueList.Add(dtTable.Rows[i]["personName"].ToString());
                valueList.Add("\t"+dtTable.Rows[i]["allidcard"].ToString());
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["contractStartDate"].ToString())? TranslateUtils.ToDateTime(dtTable.Rows[i]["contractStartDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["contractEndDate"].ToString())?TranslateUtils.ToDateTime(dtTable.Rows[i]["contractEndDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(dtTable.Rows[i]["sex"].ToString());

                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["birthday"].ToString()) ? TranslateUtils.ToDateTime(dtTable.Rows[i]["birthday"].ToString()).ToString("yy-MM-dd") : string.Empty);
                valueList.Add(dtTable.Rows[i]["country"].ToString());
                valueList.Add(dtTable.Rows[i]["nation"].ToString());
                valueList.Add(dtTable.Rows[i]["policy"].ToString());
                valueList.Add(dtTable.Rows[i]["hometown"].ToString());
                valueList.Add(dtTable.Rows[i]["familyType"].ToString());
                valueList.Add(dtTable.Rows[i]["education"].ToString());
                valueList.Add(dtTable.Rows[i]["graduateschool"].ToString());
                valueList.Add(dtTable.Rows[i]["maritalstatus"].ToString());
                valueList.Add(dtTable.Rows[i]["childs"].ToString());
                valueList.Add(dtTable.Rows[i]["childname1"].ToString());
                valueList.Add(dtTable.Rows[i]["childname2"].ToString());
                valueList.Add(dtTable.Rows[i]["childbirthday1"].ToString());
                valueList.Add(dtTable.Rows[i]["childbirthday2"].ToString());
                valueList.Add(dtTable.Rows[i]["cityname"].ToString());
                valueList.Add(dtTable.Rows[i]["phone"].ToString());
                valueList.Add(dtTable.Rows[i]["mail"].ToString());
                valueList.Add(dtTable.Rows[i]["emergencyperson"].ToString());
                valueList.Add(dtTable.Rows[i]["emergencyperson2"].ToString());
                valueList.Add(dtTable.Rows[i]["Position"].ToString());
                valueList.Add(dtTable.Rows[i]["Department"].ToString());
                valueList.Add(dtTable.Rows[i]["JobNumber"].ToString());
                valueList.Add(dtTable.Rows[i]["bank"].ToString());
                valueList.Add(dtTable.Rows[i]["openbank"].ToString());
                valueList.Add(dtTable.Rows[i]["bankaccount"].ToString());
                valueList.Add(dtTable.Rows[i]["fundaccount"].ToString());
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["agreementStartDate"].ToString())?TranslateUtils.ToDateTime(dtTable.Rows[i]["agreementStartDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["agreementEndDate"].ToString())?TranslateUtils.ToDateTime(dtTable.Rows[i]["agreementEndDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["contractStartDate"].ToString())?TranslateUtils.ToDateTime(dtTable.Rows[i]["contractStartDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["contractEndDate"].ToString())?TranslateUtils.ToDateTime(dtTable.Rows[i]["contractEndDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(dtTable.Rows[i]["familyAddress"].ToString());
                valueList.Add(dtTable.Rows[i]["familyPostCode"].ToString());
                valueList.Add(dtTable.Rows[i]["livingAddress"].ToString());
                valueList.Add(dtTable.Rows[i]["livingPostCode"].ToString());
                valueList.Add(dtTable.Rows[i]["taxAddress"].ToString());
                valueList.Add(dtTable.Rows[i]["taxPostCode"].ToString());
                valueList.Add(dtTable.Rows[i]["otherAddress"].ToString());
                valueList.Add(dtTable.Rows[i]["otherPostCode"].ToString());
            }
            List<string> nameList = new List<string>();
            nameList.Add("序号");
            nameList.Add("姓名");
            nameList.Add("身份证");
            nameList.Add("服务起始时间");
            nameList.Add("服务终止时间");
            nameList.Add("性别");

            nameList.Add("出生年月");
            nameList.Add("国籍");
            nameList.Add("民族");
            nameList.Add("政治面貌");
            nameList.Add("籍贯");
            nameList.Add("户籍性质");
            nameList.Add("文化程度");
            nameList.Add("毕业学校");
            nameList.Add("婚姻");
            nameList.Add("生育情况");
            nameList.Add("子女姓名1");
            nameList.Add("子女姓名2");
            nameList.Add("子女出生日期1");
            nameList.Add("子女出生日期2");
            nameList.Add("工作城市");
            nameList.Add("常用电话");
            nameList.Add("常用邮箱");
            nameList.Add("紧急联系人1");
            nameList.Add("紧急联系人2");
            nameList.Add("职位");
            nameList.Add("部门");
            nameList.Add("工号");
            nameList.Add("所属银行");
            nameList.Add("开户行");
            nameList.Add("银行账户");
            nameList.Add("公积金账户");
            nameList.Add("合同开始日期");
            nameList.Add("合同终止日期");
            nameList.Add("福利起始月");
            nameList.Add("福利终止月");
            nameList.Add("户籍地址");
            nameList.Add("户籍地邮编");
            nameList.Add("居住地址");
            nameList.Add("居住地邮编");
            nameList.Add("税单地址");
            nameList.Add("税单地邮编");
            nameList.Add("其他地址");
            nameList.Add("其他邮编");

            string docFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            string filePath = PathUtils.GetTemporaryFilesPath(docFileName);

            CSVObject scvObject = new CSVObject();
            scvObject.ExportCSVFile(filePath, nameList, valueList,44);

            string fileUrl = PageUtils.GetTemporaryFilesUrl(docFileName);
            Response.Write("<script languge='javascript'>alert('导出成功!');  window.open('" + fileUrl + "')</script>");
        }

    }
}
