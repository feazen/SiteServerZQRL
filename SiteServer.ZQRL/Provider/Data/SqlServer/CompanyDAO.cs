using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using BaiRong.Core;
using BaiRong.Core.Data.Provider;
using SiteServer.ZQRL.Core;

namespace SiteServer.ZQRL.Provider.Data.SqlServer
{
    public class CompanyDAO : DataProviderBase, ICompanyDAO
    {
        public CompanyInfo GetModel(long companyId)
        {
            CompanyInfo mInfo = null;
            return mInfo;
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select a.*,b.companyname,b.contractStartDate,b.contractEndDate from t_employee a,t_employeeAgreement b ");
            //strSql.Append(" where a.id=@personId and a.id=b.personID ");
            //SqlParameter[] parms = {
            //                        new SqlParameter("@personId", SqlDbType.BigInt,64)};
            //parms[0].Value = personId;
            //PersonInfo model = null;

            //try
            //{
            //    using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
            //    {
            //        if (rdr.Read())
            //        {
            //            model = new PersonInfo();
            //            //model.ID = SqlHelperGetField.GetInt64(rdr, "id");
            //            model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
            //            model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
            //            model.Sex = SqlHelperGetField.GetString(rdr, "sex");
            //            model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
            //            model.Country = SqlHelperGetField.GetString(rdr, "country");
            //            model.Policy = SqlHelperGetField.GetString(rdr, "policy");
            //            model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
            //            model.Phone = SqlHelperGetField.GetString(rdr, "phone");
            //            model.Mail = SqlHelperGetField.GetString(rdr, "mail");
            //            model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
            //            model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
            //            model.Bank = SqlHelperGetField.GetString(rdr, "bank");
            //            model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
            //            model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
            //            model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
            //            model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
            //            model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
            //            model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
            //            model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
            //            model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
            //            model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
            //            model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
            //            model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
            //            model.CompanyName = SqlHelperGetField.GetString(rdr, "companyname");
            //            model.ContractStartDate = SqlHelperGetField.GetDateTime(rdr, "contractStartDate");
            //            model.ContractEndDate = SqlHelperGetField.GetDateTime(rdr, "contractEndDate");
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //return model;
        }

        public CompanyInfo GetModelByCompanyId(long companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_company  ");
            strSql.Append(" where id=@companyId ");
            SqlParameter[] parms = {
								    new SqlParameter("@companyId", SqlDbType.BigInt,64)};
            parms[0].Value = companyId;
            CompanyInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new CompanyInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.Name = SqlHelperGetField.GetString(rdr, "name");
                        model.Number = SqlHelperGetField.GetString(rdr, "number");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.StaffID = SqlHelperGetField.GetInt64(rdr, "staffID");
                        model.StaffID2 = SqlHelperGetField.GetInt64(rdr, "staffID2");
                        model.IsShowOther = SqlHelperGetField.GetInt(rdr, "isShowOther");
                        model.IsShowFL = SqlHelperGetField.GetInt(rdr, "isShowFL");
                        model.IsShowZC = SqlHelperGetField.GetInt(rdr, "isShowZC");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public CompanyInfo GetModelByCNumberWithReg(string cNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_company  ");
            strSql.Append(" where number=@cNumber ");
            SqlParameter[] parms = {
								    new SqlParameter("@cNumber", SqlDbType.NVarChar,64)};
            parms[0].Value = cNumber;
            CompanyInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new CompanyInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.Name = SqlHelperGetField.GetString(rdr, "name");
                        model.Number = SqlHelperGetField.GetString(rdr, "number");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.StaffID = SqlHelperGetField.GetInt64(rdr, "staffID");
                        model.StaffID2 = SqlHelperGetField.GetInt64(rdr, "staffID2");
                        model.IsShowOther = SqlHelperGetField.GetInt(rdr, "isShowOther");
                        //model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        //model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
                        //model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        //model.Sex = SqlHelperGetField.GetString(rdr, "sex");
                        //model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
                        //model.Country = SqlHelperGetField.GetString(rdr, "country");
                        //model.Policy = SqlHelperGetField.GetString(rdr, "policy");
                        //model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
                        //model.Phone = SqlHelperGetField.GetString(rdr, "phone");
                        //model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        //model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
                        //model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
                        //model.Bank = SqlHelperGetField.GetString(rdr, "bank");
                        //model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
                        //model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
                        //model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
                        //model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
                        //model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
                        //model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
                        //model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
                        //model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
                        //model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
                        //model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
                        //model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public DataTable GetSupportStaffByCompanyId(string companyId)
        {
            DataSet ds = null;
            string sqlString = string.Format(@"select ss.name,ss.phone from t_supportStaff ss,t_company cp where cp.id='{0}' and (ss.id=cp.staffID or ss.id=cp.staffID2)  ", companyId);
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqlString.ToString());
                }
                catch (Exception e)
                {
                    throw new Exception("获取列表失败！\n" + e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return ds.Tables[0];
        }

        public bool IsExistIdCard(string IdCard)
        {
            bool exists = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_company ");
            strSql.Append(" where  number=@IdCard");
            SqlParameter[] parms = {
								    new SqlParameter("@IdCard", SqlDbType.NVarChar,64)
                                    };
            parms[0].Value = IdCard;
            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                        {
                            exists = true;
                        }
                    }
                    rdr.Close();
                }
            }
            catch (Exception)
            {
                exists = false;
            }
            return exists;
        }

        public int GetTopProvinceId()
        {
            int id = 0;
            string sql = string.Empty;
            sql = "select top 1 id from [dbo].[t_province] where 1=1 ";

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, sql))
            {
                if (rdr.Read())
                {
                    id = ConvertHelper.GetInteger(rdr.GetValue(0));
                }
                rdr.Close();
            }
            return id;
        }

        public int GetTopFundtypeId()
        {
            int id = 0;
            string sql = string.Empty;
            sql = "select top 1 id from [dbo].[t_fundtype] where 1=1 ";

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, sql))
            {
                if (rdr.Read())
                {
                    id = ConvertHelper.GetInteger(rdr.GetValue(0));
                }
                rdr.Close();
            }
            return id;
        }
    }
}
