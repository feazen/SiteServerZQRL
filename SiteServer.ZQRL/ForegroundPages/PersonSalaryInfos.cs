using System;
using System.IO;
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
    public partial class PersonSalaryInfos : System.Web.UI.Page
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
            UsersInfo usersInfo = (UsersInfo) Session["UserInfo"];
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

        private void LoadHtml()
        {
            int itemid = 0;
            //分页
            string pageSql = GetSql(itemid);
            PageApp pa = new PageApp(pagesize, pagenum, pageSql, " date Desc ");

            this.rptContents.DataSource = pa.Ds;
            this.rptContents.DataBind();
            this.pageHtml = pa.GetNavigateHtml(PageUrl);
        }

        private string GetSql(int itemid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from t_salary  where 1=1 ");
            sb.Append(" and SettlementMonth >= '201501'");
            if (!string.IsNullOrEmpty(key))
            {
                sb.AppendFormat(" and personid like '%{0}%' ", key);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                string sTime = StartTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and date > '{0}' ", sTime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                string eTime = EndTime.Replace("年", "-").Replace("月", "-").Replace("日", "");
                sb.AppendFormat(" and date < '{0}' ", eTime);
            }
            if (itemid != 0)
            {
                sb.AppendFormat(" and id = '{0}' ", itemid);
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
                    this._pageUrl = string.Format("PersonSalaryInfos.aspx?StartTime={0}&EndTime={1}", StartTime, EndTime);
                }
                return this._pageUrl;
            }
        }

        protected void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rptItemContents") as Repeater;
                DataRowView rowv = (DataRowView) e.Item.DataItem;
                int itemid = Convert.ToInt32(rowv["id"]);
                string pageSql = GetSql(itemid);
                PageApp pa1 = new PageApp(pagesize, pagenum, pageSql, " date Desc ");
                string salaryDetail = pa1.Ds.Tables[0].Rows[0]["salaryDetail"].ToString();
                XmlDocument doc = new XmlDocument();
                byte[] encodedString = Encoding.UTF8.GetBytes(salaryDetail);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(encodedString);
                ms.Flush();
                doc.Load(ms);

                XmlNodeList xxList = doc.GetElementsByTagName("SettlementSalaryItemInfo");
                List<SalaryItem> salaryItemlList=new List<SalaryItem>();

                foreach (XmlNode xxNode in xxList)
                {
                    SalaryItem salaryItem=new SalaryItem();
                    salaryItem.ItemName = xxNode.Attributes["SalaryItemName"].Value;
                    if ("服务费".Equals(salaryItem.ItemName)) {
                        continue;
                    }
                    salaryItem.ItemValue = xxNode.Attributes["Result"].Value;
                    salaryItemlList.Add(salaryItem);
                }

                //rep.DataSource = pa1.Ds;
                rep.DataSource = salaryItemlList;
                rep.DataBind();
                }
            }
        public class SalaryItem
            {
                public string ItemName { get; set; }
                public string ItemValue { get; set; }
            }

    }
}
