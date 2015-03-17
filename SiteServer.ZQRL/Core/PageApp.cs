using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace SiteServer.ZQRL
{
    /// <summary>
    /// PageApp(分页类) 的摘要说明。
    /// 支持多个字段排序，但字段的排序类型不能省略
    /// </summary>
    public class PageApp
    {
        protected string sql;
        protected int pageSize; //当前页码数
        protected int pageNum; //每页显示的记录数
        protected int prePageSize; //上一页页码数
        protected int nextPageSize; //下一页页码数
        protected DataSet ds; //当前页的数据集
        protected int totalNum; //总记录数
        protected int totalPage; //总页数
        protected string navigateHtml; //导航条
        protected bool hasPrePage; //是否有上一页
        protected bool hasNextPage; //是否有下一页

        /// <summary>
        /// PageApp(分页类) 构造函数
        /// </summary>
        /// <param name="pageSize">当前页码数</param>
        /// <param name="pageNum">每页显示的记录数</param>
        /// <param name="sql">要分页的数据集SQL，不含排序</param>
        /// <param name="order">ORDER BY后的字符串，不含ORDER BY，可包含ASC或DESC</param>
        public PageApp(int pageSize, int pageNum, string sql, string order)
        {
            this.sql = sql;
            this.pageSize = pageSize;
            this.pageNum = pageNum;

            //得到总记录数

            object obj = SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, "select count(*) from (" + this.sql + ") a");
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                this.totalNum = 0;
            }
            else
            {
                this.totalNum = int.Parse(obj.ToString());
            }

            //得到总页数
            if (this.pageNum <= 0)
            {
                this.pageNum = 10;
            }

            if (this.totalNum % this.pageNum == 0)
                this.totalPage = this.totalNum / this.pageNum;
            else
                this.totalPage = this.totalNum / this.pageNum + 1;

            if (this.pageSize <= 0)
            {
                this.pageSize = 1;
            }
            if (this.pageSize > this.totalPage)
            {
                this.pageSize = this.totalPage;
            }

            //得到上一页码数
            if (this.pageSize <= 1)
            {
                this.hasPrePage = false;
                this.prePageSize = 1;
            }
            else
            {
                this.hasPrePage = true;
                this.prePageSize = this.pageSize - 1;
            }

            //得到下一页码数
            if (this.pageSize >= this.totalPage)
            {
                this.hasNextPage = false;
                this.nextPageSize = this.totalPage;
            }
            else
            {
                this.hasNextPage = true;
                this.nextPageSize = this.pageSize + 1;
            }

            //得到当前页的数据集
            string order1 = order.Trim().ToLower();
            string order2 = order1.Replace(" asc", " asc_1#").Replace(" desc", " asc").Replace(" asc_1#", " desc");
            StringBuilder strSql = new StringBuilder();
            if (this.pageSize <= this.totalPage / 2)
            { //当页码数小于等于总页数的一半
                strSql.Append("select * from ");
                strSql.Append("(select top ");
                strSql.Append(this.pageNum);
                strSql.Append(" * from ");
                strSql.Append("(select top ");
                strSql.Append((this.pageSize * this.pageNum));
                strSql.Append(" * from (");
                strSql.Append(sql);
                strSql.Append(") a order by ");
                strSql.Append(order1);
                strSql.Append(") a order by ");
                strSql.Append(order2);
                strSql.Append(") a order by ");
                strSql.Append(order1);
            }
            else
            { //当前页码数大于总页数的一半
                strSql.Append("select top ");
                strSql.Append(this.pageNum);
                strSql.Append(" * from ");
                strSql.Append("(select top ");
                strSql.Append((this.totalNum - (this.pageSize - 1) * this.pageNum));
                strSql.Append(" * from (");
                strSql.Append(sql);
                strSql.Append(") a order by ");
                strSql.Append(order2);
                strSql.Append(") a order by ");
                strSql.Append(order1);
            }

            //System.Web.HttpContext.Current.Response.Write(strSql);
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    this.ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql.ToString());
                }
                catch (Exception e)
                {
                    throw new Exception("获得分页信息列表失败！\n" + e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        /// <summary>
        /// 得到分页导航条
        /// </summary>
        /// <param name="pageUrl">分页跳转的页面，可以带参数的形式（/index/index.aspx?city=北京）</param>
        /// <returns></returns>
        public string GetNavigateHtml(string pageUrl)
        {
            int Pages = 9;
            int startPage = 0;
            int endPage = 0;
            StringBuilder strHtml = new StringBuilder();

            string leftOmit = "", rightOmit = ""; //左右省略号

            //if (this.TotalPage == 1 || this.TotalPage == 0)
            if (this.TotalPage == 0)
            { //只有0页或一页
                // 不显示导航条
            }
            else
            { //多于一页

               // strHtml.Append("共&nbsp;" + this.TotalPage + "&nbsp;页&nbsp;" + this.TotalNum + "&nbsp;条记录&nbsp;");
                // 展示起始页和结束页的设置
                if (this.TotalPage <= Pages)
                {
                    startPage = 1;
                    endPage = this.TotalPage;
                }
                else
                {
                    if (this.PageSize <= (Pages - 1) / 2 + 1)
                    {
                        startPage = 1;
                        endPage = Pages;
                        rightOmit = "...";
                    }
                    else if (this.PageSize >= this.TotalPage - (Pages - 1) / 2)
                    {
                        endPage = this.TotalPage;
                        startPage = this.TotalPage - Pages + 1;
                        leftOmit = "...";
                    }
                    else
                    {
                        startPage = this.PageSize - (Pages - 1) / 2;
                        endPage = this.PageSize + (Pages - 1) / 2;
                        leftOmit = "...";
                        rightOmit = "...";
                    }
                }

                if (!leftOmit.Equals(""))
                {
                    strHtml.Append("<a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", "1"), "pagenum", this.PageNum.ToString()) + "'>&nbsp;<font face='webdings' title='首页'>9</font></a><a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", this.PrePageSize.ToString()), "pagenum", this.PageNum.ToString()) + "'><font face='webdings' title='上一页'>3</font></a>&nbsp;");
                }
                strHtml.Append(leftOmit);
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == this.PageSize)
                    {
                        //strHtml.Append("[<font color='red'>" + i + "</font>]&nbsp;");
                        strHtml.Append("<a class='current ' href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", i.ToString()), "pagenum", this.PageNum.ToString()) + "'>" + i + "</a>" +
                                       "" +
                                       "");
                    }
                    else
                    {
                        strHtml.Append("<a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", i.ToString()), "pagenum", this.PageNum.ToString()) + "'>" + i + "</a>&nbsp;");
                    }
                }
                strHtml.Append(rightOmit);
                if (!rightOmit.Equals(""))
                {
                    strHtml.Append("&nbsp;<a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", this.NextPageSize.ToString()), "pagenum", this.PageNum.ToString()) + "'>&nbsp;<font face='webdings' title='下一页'>4</font></a><a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", this.TotalPage.ToString()), "pagenum", this.PageNum.ToString()) + "'><font face='webdings' title='最后一页'>:</font></a>");
                }
                //strHtml.Append("<span class='fl span2'>跳转到</span><input type='text' class='fl' /><a  href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", i.ToString()), "pagenum", this.PageNum.ToString()) + "'>" + i + "</a>&nbsp;");
                strHtml.Append("<span class=' span2'>跳转到</span><input id='PageNum' type='text' /><a  href='javascript:void(0)' onclick='GoPages()'>Go</a>&nbsp;");
            }
            return strHtml.ToString();
        }

        /// <summary>
        /// 得到分页导航条
        /// </summary>
        /// <param name="pageUrl">分页跳转的页面，可以带参数的形式（/index/index.aspx?city=北京）</param>
        /// <returns></returns>
        public string GetNavigateHtmlEN(string pageUrl)
        {
            int Pages = 9;
            int startPage = 0;
            int endPage = 0;
            StringBuilder strHtml = new StringBuilder();

            string leftOmit = "", rightOmit = ""; //左右省略号

            //if (this.TotalPage == 1 || this.TotalPage == 0)
            if (this.TotalPage == 0)
            { //只有0页或一页
                // 不显示导航条
                return "";
            }
            else
            { //多于一页
                strHtml.Append("Total&nbsp;" + this.TotalPage + "&nbsp;Pages&nbsp;" + this.TotalNum + "&nbsp;Rows&nbsp;");

                // 展示起始页和结束页的设置
                if (this.TotalPage <= Pages)
                {
                    startPage = 1;
                    endPage = this.TotalPage;
                }
                else
                {
                    if (this.PageSize <= (Pages - 1) / 2 + 1)
                    {
                        startPage = 1;
                        endPage = Pages;
                        rightOmit = "...";
                    }
                    else if (this.PageSize >= this.TotalPage - (Pages - 1) / 2)
                    {
                        endPage = this.TotalPage;
                        startPage = this.TotalPage - Pages + 1;
                        leftOmit = "...";
                    }
                    else
                    {
                        startPage = this.PageSize - (Pages - 1) / 2;
                        endPage = this.PageSize + (Pages - 1) / 2;
                        leftOmit = "...";
                        rightOmit = "...";
                    }
                }

                if (!leftOmit.Equals(""))
                {
                    strHtml.Append("<a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", "1"), "pagenum", this.PageNum.ToString()) + "'>&nbsp;<font face='webdings' title='First Page'>9</font></a><a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", this.PrePageSize.ToString()), "pagenum", this.PageNum.ToString()) + "'><font face='webdings' title='Previous Page'>3</font></a>&nbsp;");
                }
                strHtml.Append(leftOmit);
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == this.PageSize)
                    {
                        strHtml.Append("[<font color='red'>" + i + "</font>]&nbsp;");
                    }
                    else
                    {
                        strHtml.Append("<a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", i.ToString()), "pagenum", this.PageNum.ToString()) + "'>[" + i + "]</a>&nbsp;");
                    }
                }
                strHtml.Append(rightOmit);
                if (!rightOmit.Equals(""))
                {
                    strHtml.Append("&nbsp;<a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", this.NextPageSize.ToString()), "pagenum", this.PageNum.ToString()) + "'>&nbsp;<font face='webdings' title='Next Page'>4</font></a><a href='" + Universal.LocationAddParam(Universal.LocationAddParam(pageUrl, "pagesize", this.TotalPage.ToString()), "pagenum", this.PageNum.ToString()) + "'><font face='webdings' title='Last Page'>:</font></a>");
                }
            }

            return strHtml.ToString();
        }

        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
        }

        public int PageNum
        {
            get
            {
                return this.pageNum;
            }
        }

        public int PrePageSize
        {
            get
            {
                return this.prePageSize;
            }
        }

        public int NextPageSize
        {
            get
            {
                return this.nextPageSize;
            }
        }

        public int TotalNum
        {
            get
            {
                return this.totalNum;
            }
        }

        public int TotalPage
        {
            get
            {
                return this.totalPage;
            }
        }

        public DataSet Ds
        {
            get
            {
                return this.ds;
            }
        }

        public bool HasPrePage
        {
            get
            {
                return this.hasPrePage;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return this.hasNextPage;
            }
        }

    }
}
