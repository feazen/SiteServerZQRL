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
    public partial class AjaxService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string json = string.Empty;
            
            try
            {
                UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];
                if (usersInfo != null)
                {
                    if (usersInfo.IsShowZC == 1)
                    {
                        json = "1";
                    }
                    else
                    {
                        json = "2";
                    }
                }
                else
                {
                    json = "0";
                }
            }
            catch (Exception)
            { 
                throw;
            }
            Response.Write(json);
        }
    }
}
