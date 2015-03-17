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
    public partial class CompanyNationalWelfareInfos : System.Web.UI.Page
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
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " birthday Desc ");

            //SqlPager pager = new  SqlPager();
            //pager.SelectCommand = pageSql;
            //pager.SortField = "birthday";
            //pager.SortMode = SortMode.DESC;
            //pager.ConnectionString = BaiRongDataProvider.ConnectionString;
            //pager.ControlToPaginate = this.rptContents;
            //pager.DataBind();

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private string GetSql()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("  select distinct(b.id) as employeeId,b.personName,b.sex,b.idcard as allidcard, ");
            sb.Append("  case when LEN(b.idcard)=15 then left(b.idcard,4)+'*********'+RIGHT(b.idcard,4)  ");
            sb.Append("  when LEN(b.idcard)=18 then left(b.idcard,7)+'********'+right(b.idcard,4)  ");
            sb.Append("  else b.idcard end as idcard, ");
            sb.Append("  b.birthday, b.socialcity from t_employeeAgreement e,t_employee b,t_nationalwelfare n where 1=1 and e.personid=b.id and n.agreementid=e.id  ");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and e.companyid like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and n.feeDate >= '{0}' ", sTime);//birthday
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and n.feeDate <= '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( b.personName like '%{0}%' or b.idcard like '%{0}%' ) ", NameOrIdCard);
            }

            return sb.ToString();
        }

        private string GetSql1()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select e.id as employeeAgreementId,e.personID as ePersonID,e.companyid,e.companyname,e.contractStartDate,e.contractEndDate,e.agreementStartDate,e.agreementEndDate, ");
            sb.Append(" b.id as employeeId,b.personName,b.idcard,b.sex,b.birthday,b.country,b.nation,b.policy,b.familyType,b.phone,b.mail,b.emergencyperson,b.emergencyphone, ");
            sb.Append(" b.bank,b.openbank,b.bankaccount,b.fundaccount,b.familyAddress,b.familyPostCode,b.livingAddress,b.livingPostCode,b.taxAddress,b.taxPostCode,b.otherAddress,b.otherPostCode,");
            sb.Append(" n.id as nationalwelfareId,n.personid as nPersonID,n.agreementid,n.feeDate,n.selfEndowmentInsBase,n.selfEndowmentInsRate,n.selfEndowmentIns, ");
            sb.Append(" n.cmpEndowmentInsBase,n.cmpEndowmentInsRate,n.cmpEndowmentIns,n.selfMedicalInsBase,n.selfMedicalInsrRate,n.selfMedicalIns, ");
            sb.Append(" n.cmpMedicalInsBase,n.cmpMedicalInsRate,n.cmpMedicalIns,n.selfJoblessInsBase,n.selfJoblessInsRate,n.selfJoblessIns, ");
            sb.Append(" n.cmpJoblessInsBase,n.cmpfJoblessInsRate,n.cmpJoblessIns,n.cmpworkInjuredBase,n.cmpworkInjuredRate,n.cmpworkInjured, ");
            sb.Append(" n.cmpBirthBase,n.cmpBirthRate,n.cmpBirth,n.selfIllnessBase,n.selfIllnessRate,n.selfIllness,n.cmpIllnessBase,n.cmpIllnessRate,n.cmpIllness, ");
            sb.Append(" n.cmpDisableBase,n.cmpDisableRate,n.cmpDisable,n.selfFundBase,n.selfFundRate,n.selfFund,n.cmpFundBase,n.cmpFundRate,n.cmpFund, ");
            sb.Append(" n.selfAddFundBase,n.selfAddFundRate,n.selfAddFund,cmpAddFundBase,n.cmpAddFundRate,n.cmpAddFund,n.effectDate,n.SettlementCode ");
            sb.Append(" from t_employeeAgreement e,t_employee b,t_nationalwelfare n where 1=1 and e.personid=b.id and n.agreementid=e.id ");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and e.companyid like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and n.feeDate > '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and n.feeDate < '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( b.personName like '%{0}%' or b.idcard like '%{0}%' or b.nickname like '%{0}%' ) ", NameOrIdCard);
            }
            //if (!string.IsNullOrEmpty(eid))
            //{
            //    sb.AppendFormat(" and b.id = '{0}' ", eid);
            //}

            return sb.ToString();
        }

        private string GetSql1(string eid)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select e.id as employeeAgreementId,e.personID as ePersonID,e.companyid,e.companyname,e.contractStartDate,e.contractEndDate,e.agreementStartDate,e.agreementEndDate, ");
            sb.Append(" b.id as employeeId,b.personName,b.idcard,b.sex,b.birthday,b.country,b.nation,b.policy,b.familyType,b.phone,b.mail,b.emergencyperson,b.emergencyphone, ");
            sb.Append(" b.bank,b.openbank,b.bankaccount,b.fundaccount,b.familyAddress,b.familyPostCode,b.livingAddress,b.livingPostCode,b.taxAddress,b.taxPostCode,b.otherAddress,b.otherPostCode,");
            sb.Append(" n.id as nationalwelfareId,n.personid as nPersonID,n.agreementid,n.feeDate,n.selfEndowmentInsBase,n.selfEndowmentInsRate,n.selfEndowmentIns, ");
            sb.Append(" n.cmpEndowmentInsBase,n.cmpEndowmentInsRate,n.cmpEndowmentIns,n.selfMedicalInsBase,n.selfMedicalInsrRate,n.selfMedicalIns, ");
            sb.Append(" n.cmpMedicalInsBase,n.cmpMedicalInsRate,n.cmpMedicalIns,n.selfJoblessInsBase,n.selfJoblessInsRate,n.selfJoblessIns, ");
            sb.Append(" n.cmpJoblessInsBase,n.cmpfJoblessInsRate,n.cmpJoblessIns,n.cmpworkInjuredBase,n.cmpworkInjuredRate,n.cmpworkInjured, ");
            sb.Append(" n.cmpBirthBase,n.cmpBirthRate,n.cmpBirth,n.selfIllnessBase,n.selfIllnessRate,n.selfIllness,n.cmpIllnessBase,n.cmpIllnessRate,n.cmpIllness, ");
            sb.Append(" n.cmpDisableBase,n.cmpDisableRate,n.cmpDisable,n.selfFundBase,n.selfFundRate,n.selfFund,n.cmpFundBase,n.cmpFundRate,n.cmpFund, ");
            sb.Append(" n.selfAddFundBase,n.selfAddFundRate,n.selfAddFund,cmpAddFundBase,n.cmpAddFundRate,n.cmpAddFund,n.effectDate,n.SettlementCode ");
            sb.Append(" from t_employeeAgreement e,t_employee b,t_nationalwelfare n where 1=1 and e.personid=b.id and n.agreementid=e.id ");
            //2015-3-10增加，只查询2015年的数据
            sb.Append(" and n.feeDate >= CAST('2015-01-01' as date)");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and e.companyid like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and feeDate > '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and feeDate < '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(NameOrIdCard))
            {
                sb.AppendFormat(" and ( b.personName like '%{0}%' or b.idcard like '%{0}%' ) ", NameOrIdCard);
            }


            if (!string.IsNullOrEmpty(eid))
            {
                sb.AppendFormat(" and b.id = '{0}' ", eid);
            }

            return sb.ToString();
        }

        protected void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rptItemContents") as Repeater;
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                string itemid = rowv["employeeId"].ToString();
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
                    this._pageUrl = string.Format("CompanyNationalWelfareInfos.aspx?StartTime={0}&EndTime={1}&NameOrIdCard={2}", StartTime, EndTime, NameOrIdCard);
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
                valueList.Add(dtTable.Rows[i]["personName"].ToString());
                valueList.Add(dtTable.Rows[i]["sex"].ToString());
                valueList.Add("\t" + dtTable.Rows[i]["idcard"].ToString());

                valueList.Add(dtTable.Rows[i]["selfEndowmentInsBase"].ToString());
                valueList.Add(dtTable.Rows[i]["selfEndowmentInsRate"].ToString() + "%");
                valueList.Add(dtTable.Rows[i]["cmpEndowmentInsBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpEndowmentInsRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["selfMedicalInsBase"].ToString());
                valueList.Add(dtTable.Rows[i]["selfMedicalInsrRate"].ToString() + "%");
                valueList.Add(dtTable.Rows[i]["cmpMedicalInsBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpMedicalInsRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["selfJoblessInsBase"].ToString());
                valueList.Add(dtTable.Rows[i]["selfJoblessInsRate"].ToString() + "%");
                valueList.Add(dtTable.Rows[i]["cmpJoblessInsBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpfJoblessInsRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["cmpworkInjuredBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpworkInjuredRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["cmpBirthBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpBirthRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["selfIllnessBase"].ToString());
                valueList.Add(dtTable.Rows[i]["selfIllnessRate"].ToString() + "%");
                valueList.Add(dtTable.Rows[i]["cmpIllnessBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpIllnessRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["cmpDisableBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpDisableRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["selfFundBase"].ToString());
                valueList.Add(dtTable.Rows[i]["selfFundRate"].ToString() + "%");
                valueList.Add(dtTable.Rows[i]["cmpFundBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpFundRate"].ToString() + "%");

                valueList.Add(dtTable.Rows[i]["selfAddFundBase"].ToString());
                valueList.Add(dtTable.Rows[i]["selfAddFundRate"].ToString() + "%");
                valueList.Add(dtTable.Rows[i]["cmpAddFundBase"].ToString());
                valueList.Add(dtTable.Rows[i]["cmpAddFundRate"].ToString() + "%");
            }
            List<string> nameList = new List<string>();
            nameList.Add("序号");
            nameList.Add("姓名");
            nameList.Add("性别");
            nameList.Add("身份证");

            nameList.Add("养保个人缴费基数");
            nameList.Add("养保个人缴费比例");
            nameList.Add("养保企业缴费基数");
            nameList.Add("养保企业缴费比例");
            nameList.Add("医保个人缴费基数");
            nameList.Add("医保个人缴费比例");
            nameList.Add("医保企业缴费基数");
            nameList.Add("医保企业缴费比例");
            nameList.Add("失保个人缴费基数");
            nameList.Add("失保个人缴费比例");
            nameList.Add("失保企业缴费基数");
            nameList.Add("失保企业缴费比例");
            nameList.Add("工伤企业缴费基数");
            nameList.Add("工伤企业缴费比例");
            nameList.Add("生育企业缴费基数");
            nameList.Add("生育企业缴费比例");
            nameList.Add("大病医疗个人缴费基数");
            nameList.Add("大病医疗个人缴费比例");
            nameList.Add("大病医疗企业缴费基数");
            nameList.Add("大病医疗企业缴费比例");
            nameList.Add("残障金缴费基数");
            nameList.Add("残障金缴费比例");
            nameList.Add("公积金个人缴费基数");
            nameList.Add("公积金个人缴费比例");
            nameList.Add("公积金企业缴费基数");
            nameList.Add("公积金企业缴费比例");
            nameList.Add("补充公积金个人缴费基数");
            nameList.Add("补充公积金个人缴费比例");
            nameList.Add("补充公积金企业缴费基数");
            nameList.Add("补充公积金企业缴费比例");

            string docFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            string filePath = PathUtils.GetTemporaryFilesPath(docFileName);

            CSVObject scvObject = new CSVObject();
            scvObject.ExportCSVFile(filePath, nameList, valueList, 34);

            string fileUrl = PageUtils.GetTemporaryFilesUrl(docFileName);
            Response.Write("<script languge='javascript'>alert('导出成功!');  window.open('" + fileUrl + "')</script>");
        }
    }
}
