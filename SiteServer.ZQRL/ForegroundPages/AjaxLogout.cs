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

namespace SiteServer.ZQRL.ForegroundPages
{
    public partial class AjaxLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //清除某个Session
            string outPutStr = "false";
            try
            {
                //Session["UserName"] = null;
                //Session.Remove("UserName");
                Session.Abandon();
                outPutStr = "True";
            }
            catch (Exception)
            {
                
                throw;
            }
            Response.Write(outPutStr);
        }
    }
}
