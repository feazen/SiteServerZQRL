using System;
using System.Data.SqlClient;
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
    public partial class CompanyResidenceManageInfos : System.Web.UI.Page
    {
        public TextBox Keyword;
        public Button Search;
        public Repeater rptContents;
        protected string pageHtml;
        protected string key;
        protected DataSet ds;
        protected int pagesize;
        protected int pagenum;

        protected string returnUrl;

        public TextBox lblStartTime;
        public TextBox lblEndTime;

        protected string StartTime = string.Empty;
        protected string EndTime = string.Empty;

        public TextBox lblNameOrIdCard;
        protected string NameOrIdCard = string.Empty;
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
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " applyeDate Desc ");

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private string GetSql()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select e.id as employeeAgreementId,e.personID as EpersonID,e.companyid,e.companyname,e.contractStartDate, ");
            sb.Append(" e.contractEndDate,e.agreementStartDate,e.agreementEndDate, ");
            sb.Append(" r.id as residencemanageId,r.personid as RpersonID,r.agreementid,r.applyeDate,r.effectDate,r.paperType,r.paperStatus, ");
            sb.Append(" p.id,p.personName,p.idcard as allidcard, ");
            sb.Append("  case when LEN(p.idcard)=15 then left(p.idcard,4)+'*********'+RIGHT(p.idcard,4)  ");
            sb.Append("  when LEN(p.idcard)=18 then left(p.idcard,7)+'********'+right(p.idcard,4)  ");
            sb.Append("  else p.idcard end as idcard, ");
            sb.Append(" p.sex,p.birthday,p.country,p.nation,p.policy,p.familyType,p.phone,p.mail, ");
            sb.Append(" p.emergencyperson,p.emergencyphone,p.bank,p.openbank, ");
            sb.Append("  case when LEN(p.bankaccount) >8 then LEFT(p.bankaccount,4) + LEFT('**********',LEN(p.bankaccount)-8)+RIGHT(p.bankaccount,4)  ");
            sb.Append("  else p.bankaccount end as bankaccount, ");
            sb.Append(" p.fundaccount,p.familyAddress,p.familyPostCode, ");
            sb.Append(" p.livingAddress,p.livingPostCode,p.taxAddress,p.taxPostCode,p.otherAddress,p.otherPostCode ");
            sb.Append(" from t_employeeAgreement e,t_residencemanage r,t_employee p ");
            sb.Append(" where 1=1 and e.id=r.agreementid and p.id=r.personid ");

            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and e.companyid like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyeDate >= '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyeDate <= '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( p.personName like '%{0}%' or p.idcard like '%{0}%' or p.nickname  like '%{0}%' ) ", NameOrIdCard);
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
                    this._pageUrl = string.Format("CompanyResidenceManageInfos.aspx?StartTime={0}&EndTime={1}&NameOrIdCard={2}", StartTime, EndTime, NameOrIdCard);
                }
                return this._pageUrl;
            }
        }

        public void exportExcel_OnClick(object sender, EventArgs E)
        {
            StartTime = lblStartTime.Text;
            EndTime = lblEndTime.Text;
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
                valueList.Add((i + 1).ToString());
                valueList.Add(dtTable.Rows[i]["personName"].ToString());
                valueList.Add(dtTable.Rows[i]["allidcard"].ToString());
                valueList.Add(dtTable.Rows[i]["sex"].ToString());
                valueList.Add(TranslateUtils.ToDateTime(dtTable.Rows[i]["effectDate"].ToString()).ToString("yy-MM-dd"));
                valueList.Add(dtTable.Rows[i]["paperStatus"].ToString());
            }
            List<string> nameList = new List<string>();
            nameList.Add("序号");
            nameList.Add("姓名");
            nameList.Add("身份证");
            nameList.Add("性别");
            nameList.Add("有效期到");
            nameList.Add("办理状态");
            

            string docFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            string filePath = PathUtils.GetTemporaryFilesPath(docFileName);

            CSVObject scvObject = new CSVObject();
            scvObject.ExportCSVFile(filePath, nameList, valueList, 6);

            string fileUrl = PageUtils.GetTemporaryFilesUrl(docFileName);
            Response.Write("<script languge='javascript'>alert('导出成功!'); window.open('" + fileUrl + "')</script>");
        }
    }
}
