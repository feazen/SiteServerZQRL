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
    public partial class CompanyMedicalclaimInfos : System.Web.UI.Page
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

        protected string StartTime = string.Empty;
        protected string EndTime = string.Empty;

        public TextBox lblNameOrIdCard;
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
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " id Desc ");

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private string GetSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select t_medicalclaim.id, t_medicalclaim.insPerson,t_medicalclaim.insPersonIDNumber as allinsPersonIDNumber, ");
            //sb.Append(" t_medicalclaim.insPersonIDNumber, ");
            sb.Append("  case when LEN(t_medicalclaim.insPersonIDNumber)=15 then left(t_medicalclaim.insPersonIDNumber,4)+'*********'+RIGHT(t_medicalclaim.insPersonIDNumber,4)  ");
            sb.Append("  when LEN(t_medicalclaim.insPersonIDNumber)=18 then left(t_medicalclaim.insPersonIDNumber,7)+'********'+right(t_medicalclaim.insPersonIDNumber,4)  ");
            sb.Append("  else t_medicalclaim.insPersonIDNumber end as insPersonIDNumber, ");
            sb.Append(" t_medicalclaim.applyDate,t_medicalclaim.reimburseDate,t_medicalclaim.reimburseAmount,t_medicalclaim.isReimburse,t_medicalclaim.feeFomula,t_medicalclaim.reimburseRate,t_medicalclaim.number from t_medicalclaim ");

            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" where t_medicalclaim.personIDNumber in (select t1.idcard from t_employee t1 inner join t_employeeAgreement t2 on t1.id = t2.personid where t2.companyid like '%{0}%') ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate >= '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate <= '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( insPerson like '%{0}%' or insPersonIDNumber like '%{0}%' or personid in(select id from t_employee where nickname like '%{0}%') ) ", NameOrIdCard);
            }

            return sb.ToString();
        }

        private string GetSql1()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select t_medicalclaim.*,t_medicalclaimentry.id as medicalclaimentryId,t_medicalclaimentry.parent,t_medicalclaimentry.billDate, ");
            sb.Append(" t_medicalclaimentry.totalFee,t_medicalclaimentry.accountPay,t_medicalclaimentry.addedPay,t_medicalclaimentry.panPay, ");
            sb.Append(" t_medicalclaimentry.selfResponse,t_medicalclaimentry.classify,t_medicalclaimentry.selfPay,t_medicalclaimentry.remark,t_medicalclaimentry.hospital  ");
            sb.Append(" from t_medicalclaim,t_medicalclaimentry  ");
            sb.Append(" where t_medicalclaim.number=t_medicalclaimentry.parent ");

            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and t_medicalclaim.personIDNumber in (select t1.idcard from t_employee t1 inner join t_employeeAgreement t2 on t1.id = t2.personid where t2.companyid ='{0}') ", key);

                //sb.AppendFormat(" and t_medicalclaim.personIDNumber in (select t1.idcard from t_employee t1 inner join t_employeeAgreement t2 on t1.id = t2.personid where t2.companyid like '%{0}%') ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate >= '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate <= '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( insPerson like '%{0}%' or insPersonIDNumber like '%{0}%' or personid in(select id from t_employee where nickname like '%{0}%') ) ", NameOrIdCard);
               
            }
            //if (!string.IsNullOrEmpty(key))
            //{
            //    sb.AppendFormat(" and t_medicalclaim.personIDNumber in (select t1.idcard from t_employee t1 inner join t_employeeAgreement t2 on t1.id = t2.personid where t2.companyid like '%{0}%') ", key);
            //}
            

            return sb.ToString();
        }
        private string GetSql1(string id)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select t_medicalclaim.*,t_medicalclaimentry.id as medicalclaimentryId,t_medicalclaimentry.parent,t_medicalclaimentry.billDate, ");
            sb.Append(" t_medicalclaimentry.totalFee,t_medicalclaimentry.accountPay,t_medicalclaimentry.addedPay,t_medicalclaimentry.panPay, ");
            sb.Append(" t_medicalclaimentry.selfResponse,t_medicalclaimentry.classify,t_medicalclaimentry.selfPay,t_medicalclaimentry.remark,t_medicalclaimentry.hospital  ");
            sb.Append(" from t_medicalclaim,t_medicalclaimentry  ");
            sb.Append(" where t_medicalclaim.number=t_medicalclaimentry.parent and 1=1  ");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and t_medicalclaim.personIDNumber in (select t1.idcard from t_employee t1 inner join t_employeeAgreement t2 on t1.id = t2.personid where t2.companyid like '%{0}%') ", key);
            }
            if (!string.IsNullOrEmpty(id))
            {
                sb.AppendFormat(" and t_medicalclaim.id='{0}' ", id);
            }

            return sb.ToString();
        }

        protected void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rptItemContents") as Repeater;
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                string itemid = rowv["id"].ToString();
                DataSet ds;
                string pageSql = GetSql1(itemid);
                using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
                {
                    conn.Open();
                    try
                    {
                        ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, pageSql.ToString());
                    }
                    catch (Exception)
                    {
                        throw new Exception("失败！");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                DataTable dtTable = ds.Tables[0];

                //rep.DataSource = pa1.Ds;
                rep.DataSource = dtTable;
                rep.DataBind();
            }
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
                    this._pageUrl = string.Format("CompanyMedicalclaimInfos.aspx?StartTime={0}&EndTime={1}&NameOrIdCard={2}", StartTime, EndTime, NameOrIdCard);
                }
                return this._pageUrl;
            }
        }

        public void exportExcel_OnClick(object sender, EventArgs E)
        {
            StartTime = lblStartTime.Text;
            EndTime = lblEndTime.Text;
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
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                valueList.Add((i + 1).ToString());
                valueList.Add(dtTable.Rows[i]["insPerson"].ToString());
                valueList.Add("\t"+dtTable.Rows[i]["insPersonIDNumber"].ToString());
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["applyDate"].ToString())?TranslateUtils.ToDateTime(dtTable.Rows[i]["applyDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["reimburseDate"].ToString())?TranslateUtils.ToDateTime(dtTable.Rows[i]["reimburseDate"].ToString()).ToString("yy-MM-dd"):string.Empty);
                valueList.Add(dtTable.Rows[i]["reimburseAmount"].ToString());
                valueList.Add(dtTable.Rows[i]["reimburseRate"].ToString());
                valueList.Add(dtTable.Rows[i]["number"].ToString());
                valueList.Add(!string.IsNullOrEmpty(dtTable.Rows[i]["billDate"].ToString()) ? TranslateUtils.ToDateTime(dtTable.Rows[i]["billDate"].ToString()).ToString("yy-MM-dd") : string.Empty);
                valueList.Add(dtTable.Rows[i]["hospital"].ToString());
                valueList.Add(dtTable.Rows[i]["totalFee"].ToString());
                valueList.Add(dtTable.Rows[i]["accountPay"].ToString());
                valueList.Add(dtTable.Rows[i]["addedPay"].ToString());
                valueList.Add(dtTable.Rows[i]["panPay"].ToString());
                valueList.Add(dtTable.Rows[i]["selfResponse"].ToString());
                valueList.Add(dtTable.Rows[i]["classify"].ToString());
                valueList.Add(dtTable.Rows[i]["selfPay"].ToString());
                valueList.Add(dtTable.Rows[i]["remark"].ToString());		
            }
            List<string> nameList = new List<string>();
            nameList.Add("序号");
            nameList.Add("被保险人");
            nameList.Add("身份证");
            nameList.Add("日期");
            nameList.Add("医疗报销时间");
            nameList.Add("医疗报销金额");
            nameList.Add("报销比例");
            nameList.Add("单证号");
            nameList.Add("单证日期");
            nameList.Add("就诊医院");
            nameList.Add("收据总费用");
            nameList.Add("帐户支付");
            nameList.Add("附加支付");
            nameList.Add("统筹支付");
            nameList.Add("自付");
            nameList.Add("分类自付");
            nameList.Add("自费");
            nameList.Add("备注");																																		

            string docFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            string filePath = PathUtils.GetTemporaryFilesPath(docFileName);

            CSVObject scvObject = new CSVObject();
            scvObject.ExportCSVFile(filePath, nameList, valueList, 18);

            string fileUrl = PageUtils.GetTemporaryFilesUrl(docFileName);
            Response.Write("<script languge='javascript'>alert('导出成功!');  window.open('" + fileUrl + "');</script>");
        }
    }
}
