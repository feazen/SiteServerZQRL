using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using BaiRong.Core.Data.Provider;
using SiteServer.ZQRL.Core;

namespace SiteServer.ZQRL.Provider.Data.SqlServer
{
    public class SmsDAO : DataProviderBase,ISmsDAO
    {
        /// <summary>
        /// 得到一个短信配置
        /// </summary>
        public SmsInfo GetModel(int KeyID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from pantum_tdSms ");
            strSql.Append(" where KeyID=@KeyID");
            SqlParameter[] parms = {
											new SqlParameter("@KeyID", SqlDbType.Int,4)};
            parms[0].Value = KeyID;

            SmsInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new SmsInfo();
                        model.KeyID = KeyID;
                        model.LoginMessage = SqlHelperGetField.GetString(rdr, "LoginMessage");
                        model.FindPassWord = SqlHelperGetField.GetString(rdr, "FindPassWord");
                        model.MobileYYFG = SqlHelperGetField.GetString(rdr, "MobileYYFG");
                        model.TJYYD = SqlHelperGetField.GetString(rdr, "TJYYD");
                        model.JXSXYYD = SqlHelperGetField.GetString(rdr, "JXSXYYD");
                        model.JXSQRCS = SqlHelperGetField.GetString(rdr, "JXSQRCS");
                        model.GLYQRCS = SqlHelperGetField.GetString(rdr, "GLYQRCS");
                        model.JXSFFCS = SqlHelperGetField.GetString(rdr, "JXSFFCS");
                        model.GLYFFCS = SqlHelperGetField.GetString(rdr, "GLYFFCS");
                        model.FGWC = SqlHelperGetField.GetString(rdr, "FGWC");
                        model.SDSH = SqlHelperGetField.GetString(rdr, "SDSH");
                        model.GGXX = SqlHelperGetField.GetString(rdr, "GGXX");
                    }
                }
            }
            catch
            {
                throw;
            }
            return model;
        }
    }
}
