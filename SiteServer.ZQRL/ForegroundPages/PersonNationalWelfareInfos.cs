using System;
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
    public partial class PersonNationalWelfareInfos : System.Web.UI.Page
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

        protected string StartTime=string.Empty;
        protected string EndTime=string.Empty;
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
                PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModel(usersInfo.PersonId);
                login_name = personInfo.PersonName;
                if (personInfo != null)
                {
                    key = personInfo.ID.ToString();
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
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " feeDate Desc ");

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private string GetSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from t_nationalwelfare  where 1=1 ");
            //2015-3-10增加，只查询2015年的数据
            sb.Append(" and feeDate >= CAST('2015-01-01' as date)");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and personid like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and feeDate >= '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and feeDate <= '{0}' ", eTime);
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
                    this._pageUrl = string.Format("PersonNationalWelfareInfos.aspx?StartTime={0}&EndTime={1}", StartTime, EndTime);
                }
                return this._pageUrl;
            }
        }

    }
}
