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
    public class SalaryDAO : DataProviderBase, ISalaryDAO
    {
        public List<SalaryInfo> GeSalaryListByPersonId(long personId)
        {
            List<SalaryInfo> salaryInfoList = new List<SalaryInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_salary ");
            strSql.Append(" where personid=@personId ");
            //2015-3-10增加，只查询2015年的数据
            strSql.Append(" and SettlementMonth >= '201501'");
            SqlParameter[] parms = {
								    new SqlParameter("@personId", SqlDbType.BigInt,64)};
            parms[0].Value = personId;
            SalaryInfo salaryInfo = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        salaryInfo = new SalaryInfo();
                        salaryInfo.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        salaryInfo.PersonId = SqlHelperGetField.GetInt64(rdr, "personid");
                        salaryInfo.AgreementId = SqlHelperGetField.GetInt64(rdr, "agreementid");
                        salaryInfo.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        salaryInfo.Date = SqlHelperGetField.GetDateTime(rdr, "date");
                        salaryInfo.SalaryDetail = SqlHelperGetField.GetString(rdr, "salaryDetail");

                        salaryInfoList.Add(salaryInfo);
                    }
                    rdr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return salaryInfoList;
        }
    }
}
