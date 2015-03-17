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
    public partial class PersonMedicalclaimInfos : System.Web.UI.Page
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
        protected string login_name = "匿名";
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];
            if (usersInfo == null || usersInfo.Number == null || usersInfo.Number == "")
            {
                Response.Redirect("../index.htm");
            }
            else
            {
                PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModel1(usersInfo.PersonId);
                login_name = personInfo.PersonName;
                if (personInfo != null)
                {
                    key = personInfo.IdCard;
                }
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
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " applyDate Desc ");

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private string GetSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select distinct(t_medicalclaim.number),t_medicalclaim.reimburseAmount,t_medicalclaimentry.parent,t_medicalclaim.applyDate,t_medicalclaim.insPerson,t_medicalclaim.reimburseDate,t_medicalclaim.isReimburse,t_medicalclaim.reimburseRate,t_medicalclaim.personIDNumber,t_medicalclaim.feeFomula from t_medicalclaim,t_medicalclaimentry where t_medicalclaimentry.parent=t_medicalclaim.number ");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and personIDNumber like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate > '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate < '{0}' ", eTime);
            }
            //2015-3-10增加，只查询2015年的数据
            //sb.AppendFormat(" and applyDate >= CAST('2015-01-01' as date)");
            return sb.ToString();
        }

        private string GetSql1(string number)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select t_medicalclaim.*,t_medicalclaimentry.id as medicalclaimentryId,t_medicalclaimentry.parent,t_medicalclaimentry.billDate,t_medicalclaimentry.totalFee,t_medicalclaimentry.accountPay,t_medicalclaimentry.addedPay,t_medicalclaimentry.panPay,t_medicalclaimentry.selfResponse,t_medicalclaimentry.classify,t_medicalclaimentry.selfPay,t_medicalclaimentry.remark,t_medicalclaimentry.hospital from t_medicalclaim,t_medicalclaimentry where t_medicalclaim.number=t_medicalclaimentry.parent and 1=1 ");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and personIDNumber like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate > '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and applyDate < '{0}' ", eTime);
            }
            if (!string.IsNullOrEmpty(number))
            {
                sb.AppendFormat(" and number = '{0}' ", number);
            }

            return sb.ToString();
        }

        protected void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rptItemContents") as Repeater;
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                string itemid = rowv["number"].ToString();
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
                    this._pageUrl = string.Format("PersonMedicalclaimInfos.aspx?StartTime={0}&EndTime={1}", StartTime, EndTime);
                }
                return this._pageUrl;
            }
        }
    }
}
