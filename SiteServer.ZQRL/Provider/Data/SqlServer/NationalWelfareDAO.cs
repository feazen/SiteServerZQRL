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
    public class NationalWelfareDAO : DataProviderBase,INationalWelfareDAO
    {
        public List<NationalWelfareInfo> GetNationalWelfareListByPersonId(long personId)
        {
            List<NationalWelfareInfo> nationalWelfareList=new List<NationalWelfareInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_nationalwelfare ");
            strSql.Append(" where personid=@personId ");
            //2015-3-10增加，只查询2015年的数据
            strSql.Append(" and feeDate >= CAST('2015-01-01' as date)");
            SqlParameter[] parms = {
								    new SqlParameter("@personId", SqlDbType.BigInt,64)};
            parms[0].Value = personId;
            NationalWelfareInfo nationalWelfareInfo = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text,strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        nationalWelfareInfo=new NationalWelfareInfo();
                        nationalWelfareInfo.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        nationalWelfareInfo.PersonId=SqlHelperGetField.GetInt64(rdr,"personid");
                        nationalWelfareInfo.AgreementId=SqlHelperGetField.GetInt64(rdr,"agreementid");
                        nationalWelfareInfo.FeeDate=SqlHelperGetField.GetDateTime(rdr,"feeDate");
                        nationalWelfareInfo.SelfEndowmentInsBase=SqlHelperGetField.GetFloat(rdr,"selfEndowmentInsBase");
                        nationalWelfareInfo.SelfEndowmentInsRate=SqlHelperGetField.GetFloat(rdr,"selfEndowmentInsRate");
                        nationalWelfareInfo.SelfEndowmentIns=SqlHelperGetField.GetFloat(rdr,"selfEndowmentIns");
                        nationalWelfareInfo.CmpEndowmentInsBase=SqlHelperGetField.GetFloat(rdr,"cmpEndowmentInsBase");
                        nationalWelfareInfo.CmpEndowmentInsRate=SqlHelperGetField.GetFloat(rdr,"cmpEndowmentInsRate");
                        nationalWelfareInfo.CmpEndowmentIns=SqlHelperGetField.GetFloat(rdr,"cmpEndowmentIns");
                        nationalWelfareInfo.SelfMedicalInsBase=SqlHelperGetField.GetFloat(rdr,"selfMedicalInsBase");
                        nationalWelfareInfo.SelfMedicalInsrRate=SqlHelperGetField.GetFloat(rdr,"selfMedicalInsrRate");
                        nationalWelfareInfo.SelfMedicalIns=SqlHelperGetField.GetFloat(rdr,"selfMedicalIns");
                        nationalWelfareInfo.CmpMedicalInsBase=SqlHelperGetField.GetFloat(rdr,"cmpMedicalInsBase");
                        nationalWelfareInfo.CmpfJoblessInsRate=SqlHelperGetField.GetFloat(rdr,"cmpMedicalInsRate");
                        nationalWelfareInfo.CmpMedicalIns=SqlHelperGetField.GetFloat(rdr,"cmpMedicalIns");
                        nationalWelfareInfo.SelfJoblessInsBase=SqlHelperGetField.GetFloat(rdr,"selfJoblessInsBase");
                        nationalWelfareInfo.SelfJoblessInsRate=SqlHelperGetField.GetFloat(rdr,"selfJoblessInsRate");
                        nationalWelfareInfo.SelfJoblessIns=SqlHelperGetField.GetFloat(rdr,"selfJoblessIns");
                        nationalWelfareInfo.CmpJoblessInsBase=SqlHelperGetField.GetFloat(rdr,"cmpJoblessInsBase");
                        nationalWelfareInfo.CmpfJoblessInsRate=SqlHelperGetField.GetFloat(rdr,"cmpfJoblessInsRate");
                        nationalWelfareInfo.CmpJoblessIns=SqlHelperGetField.GetFloat(rdr,"cmpJoblessIns");
                        nationalWelfareInfo.CmpworkInjuredBase=SqlHelperGetField.GetFloat(rdr,"cmpworkInjuredBase");
                        nationalWelfareInfo.CmpworkInjuredRate=SqlHelperGetField.GetFloat(rdr,"cmpworkInjuredRate");
                        nationalWelfareInfo.CmpworkInjured=SqlHelperGetField.GetFloat(rdr,"cmpworkInjured");
                        nationalWelfareInfo.CmpBirthBase=SqlHelperGetField.GetFloat(rdr,"cmpBirthBase");
                        nationalWelfareInfo.CmpBirthRate=SqlHelperGetField.GetFloat(rdr,"cmpBirthRate");
                        nationalWelfareInfo.CmpBirth=SqlHelperGetField.GetFloat(rdr,"cmpBirth");
                        nationalWelfareInfo.SelfIllnessBase=SqlHelperGetField.GetFloat(rdr,"selfIllnessBase");
                        nationalWelfareInfo.SelfIllnessRate=SqlHelperGetField.GetFloat(rdr,"selfIllnessRate");
                        nationalWelfareInfo.SelfIllness=SqlHelperGetField.GetFloat(rdr,"selfIllness");
                        nationalWelfareInfo.CmpIllnessBase=SqlHelperGetField.GetFloat(rdr,"cmpIllnessBase");
                        nationalWelfareInfo.CmpIllnessRate=SqlHelperGetField.GetFloat(rdr,"cmpIllnessRate");
                        nationalWelfareInfo.CmpIllness=SqlHelperGetField.GetFloat(rdr,"cmpIllness");
                        nationalWelfareInfo.CmpDisableBase=SqlHelperGetField.GetFloat(rdr,"cmpDisableBase");
                        nationalWelfareInfo.CmpDisableRate=SqlHelperGetField.GetFloat(rdr,"cmpDisableRate");
                        nationalWelfareInfo.CmpDisable=SqlHelperGetField.GetFloat(rdr,"cmpDisable");
                        nationalWelfareInfo.SelfFundBase=SqlHelperGetField.GetFloat(rdr,"selfFundBase");
                        nationalWelfareInfo.SelfFundRate=SqlHelperGetField.GetFloat(rdr,"selfFundRate");
                        nationalWelfareInfo.SelfFund=SqlHelperGetField.GetFloat(rdr,"selfFund");
                        nationalWelfareInfo.CmpFundBase=SqlHelperGetField.GetFloat(rdr,"cmpFundBase");
                        nationalWelfareInfo.CmpFundRate=SqlHelperGetField.GetFloat(rdr,"cmpFundRate");
                        nationalWelfareInfo.CmpFund=SqlHelperGetField.GetFloat(rdr,"cmpFund");
                        nationalWelfareInfo.SelfAddFundBase=SqlHelperGetField.GetFloat(rdr,"selfAddFundBase");
                        nationalWelfareInfo.SelfAddFundRate=SqlHelperGetField.GetFloat(rdr,"selfAddFundRate");
                        nationalWelfareInfo.SelfAddFund=SqlHelperGetField.GetFloat(rdr,"selfAddFund");
                        nationalWelfareInfo.CmpAddFundBase=SqlHelperGetField.GetFloat(rdr,"cmpAddFundBase");
                        nationalWelfareInfo.CmpAddFundRate=SqlHelperGetField.GetFloat(rdr,"cmpAddFundRate");
                        nationalWelfareInfo.CmpAddFund=SqlHelperGetField.GetFloat(rdr,"cmpAddFund");
                       nationalWelfareInfo.EffectDate=SqlHelperGetField.GetDateTime(rdr,"effectDate");
                        nationalWelfareInfo.SettleMentCode=SqlHelperGetField.GetString(rdr,"SettlementCode");

                        nationalWelfareList.Add(nationalWelfareInfo);
                    }
                    rdr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nationalWelfareList;
        }
    }
}
