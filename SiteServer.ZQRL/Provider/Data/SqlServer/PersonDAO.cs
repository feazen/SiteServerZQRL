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
    public class PersonDAO: DataProviderBase, IPersonDAO
    {
        public PersonInfo GetModel(long personId)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select a.*,b.companyname,b.contractStartDate,b.contractEndDate from t_employee a,t_employeeAgreement b ");
            strSql.Append(" select a.id  ");
            strSql.Append(" ,a.personName ");
            strSql.Append("  ,case when LEN(a.idcard)=15 then left(a.idcard,4)+'*********'+RIGHT(a.idcard,4)  ");
            strSql.Append("  when LEN(a.idcard)=18 then left(a.idcard,7)+'********'+right(a.idcard,4)  ");
            strSql.Append("  else a.idcard end as idcard ");
            strSql.Append(" ,a.sex ");
            strSql.Append(" ,a.birthday  ");
            strSql.Append(" ,a.country ");
            strSql.Append(" ,a.nation ");
            strSql.Append(" ,a.policy ");
            strSql.Append(" ,a.familyType ");
            strSql.Append(" ,a.phone  ");
            strSql.Append(" ,a.mail  ");
            strSql.Append(" ,a.emergencyperson  ");
            strSql.Append(" ,a.emergencyphone  ");
            strSql.Append(" ,a.bank ");
            strSql.Append(" ,a.openbank ");
            strSql.Append("  ,case when LEN(a.bankaccount) >8 then LEFT(a.bankaccount,4) + LEFT('**********',LEN(a.bankaccount)-8)+RIGHT(a.bankaccount,4)  ");
            strSql.Append("  else a.bankaccount end as bankaccount ");
            strSql.Append(" ,a.fundaccount  ");
            strSql.Append(" ,a.familyAddress  ");
            strSql.Append(" ,a.familyPostCode  ");
            strSql.Append(" ,a.livingAddress ");
            strSql.Append(" ,a.livingPostCode ");
            strSql.Append(" ,a.taxAddress ");
            strSql.Append(" ,a.taxPostCode  ");
            strSql.Append(" ,a.otherAddress  ");
            strSql.Append(" ,a.otherPostCode  ");
            strSql.Append(" ,a.hometown  ");
            strSql.Append(" ,a.education  ");
            strSql.Append(" ,a.graduateschool  ");
            strSql.Append(" ,a.maritalstatus  ");
            strSql.Append(" ,a.childs  ");
            strSql.Append(" ,a.cityname  ");
            strSql.Append(" ,a.accountType ");
            strSql.Append(" ,a.emergencyperson2  ");
            strSql.Append(" ,a.emergencyphone2  ");
            strSql.Append(" ,a.childname1 ");
            strSql.Append(" ,a.childbirthday1  ");
            strSql.Append(" ,a.childname2  ");
            strSql.Append(" ,a.childbirthday2,b.companyname,b.contractStartDate,b.contractEndDate from t_employee a,t_employeeAgreement b  ");
            strSql.Append(" where a.id=@personId and a.id=b.personID ");
            SqlParameter[] parms = {
								    new SqlParameter("@personId", SqlDbType.BigInt,64)};
            parms[0].Value = personId;
            PersonInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new PersonInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.Sex = SqlHelperGetField.GetString(rdr, "sex");
                        model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
                        model.Country = SqlHelperGetField.GetString(rdr, "country");
                        model.Nation = SqlHelperGetField.GetString(rdr, "nation");
                        model.Policy = SqlHelperGetField.GetString(rdr, "policy");
                        model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
                        model.Phone = SqlHelperGetField.GetString(rdr, "phone");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
                        model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
                        model.Bank = SqlHelperGetField.GetString(rdr, "bank");
                        model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
                        model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
                        model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
                        model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
                        model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
                        model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
                        model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
                        model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
                        model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
                        model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
                        model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
                        model.CompanyName = SqlHelperGetField.GetString(rdr, "companyname");
                        model.ContractStartDate = SqlHelperGetField.GetDateTime(rdr, "contractStartDate");
                        model.ContractEndDate = SqlHelperGetField.GetDateTime(rdr, "contractEndDate");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public PersonInfo GetModel1(long personId)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select a.*,b.companyname,b.contractStartDate,b.contractEndDate from t_employee a,t_employeeAgreement b ");
            strSql.Append(" select a.id  ");
            strSql.Append(" ,a.personName ");
            //strSql.Append("  ,case when LEN(a.idcard)=15 then left(a.idcard,4)+'*********'+RIGHT(a.idcard,4)  ");
            //strSql.Append("  when LEN(a.idcard)=18 then left(a.idcard,7)+'********'+right(a.idcard,4)  ");
            //strSql.Append("  else a.idcard end as idcard ");
            strSql.Append(" ,a.idcard ");
            strSql.Append(" ,a.sex ");
            strSql.Append(" ,a.birthday  ");
            strSql.Append(" ,a.country ");
            strSql.Append(" ,a.nation ");
            strSql.Append(" ,a.policy ");
            strSql.Append(" ,a.familyType ");
            strSql.Append(" ,a.phone  ");
            strSql.Append(" ,a.mail  ");
            strSql.Append(" ,a.emergencyperson  ");
            strSql.Append(" ,a.emergencyphone  ");
            strSql.Append(" ,a.bank ");
            strSql.Append(" ,a.openbank ");
            strSql.Append("  ,case when LEN(a.bankaccount) >8 then LEFT(a.bankaccount,4) + LEFT('**********',LEN(a.bankaccount)-8)+RIGHT(a.bankaccount,4)  ");
            strSql.Append("  else a.bankaccount end as bankaccount ");
            strSql.Append(" ,a.fundaccount  ");
            strSql.Append(" ,a.familyAddress  ");
            strSql.Append(" ,a.familyPostCode  ");
            strSql.Append(" ,a.livingAddress ");
            strSql.Append(" ,a.livingPostCode ");
            strSql.Append(" ,a.taxAddress ");
            strSql.Append(" ,a.taxPostCode  ");
            strSql.Append(" ,a.otherAddress  ");
            strSql.Append(" ,a.otherPostCode  ");
            strSql.Append(" ,a.hometown  ");
            strSql.Append(" ,a.education  ");
            strSql.Append(" ,a.graduateschool  ");
            strSql.Append(" ,a.maritalstatus  ");
            strSql.Append(" ,a.childs  ");
            strSql.Append(" ,a.cityname  ");
            strSql.Append(" ,a.accountType ");
            strSql.Append(" ,a.emergencyperson2  ");
            strSql.Append(" ,a.emergencyphone2  ");
            strSql.Append(" ,a.childname1 ");
            strSql.Append(" ,a.childbirthday1  ");
            strSql.Append(" ,a.childname2  ");
            strSql.Append(" ,a.childbirthday2,b.companyname,b.contractStartDate,b.contractEndDate from t_employee a,t_employeeAgreement b  ");
            strSql.Append(" where a.id=@personId and a.id=b.personID ");
            SqlParameter[] parms = {
								    new SqlParameter("@personId", SqlDbType.BigInt,64)};
            parms[0].Value = personId;
            PersonInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new PersonInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.Sex = SqlHelperGetField.GetString(rdr, "sex");
                        model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
                        model.Country = SqlHelperGetField.GetString(rdr, "country");
                        model.Policy = SqlHelperGetField.GetString(rdr, "policy");
                        model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
                        model.Phone = SqlHelperGetField.GetString(rdr, "phone");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
                        model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
                        model.Bank = SqlHelperGetField.GetString(rdr, "bank");
                        model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
                        model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
                        model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
                        model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
                        model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
                        model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
                        model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
                        model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
                        model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
                        model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
                        model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
                        model.CompanyName = SqlHelperGetField.GetString(rdr, "companyname");
                        model.ContractStartDate = SqlHelperGetField.GetDateTime(rdr, "contractStartDate");
                        model.ContractEndDate = SqlHelperGetField.GetDateTime(rdr, "contractEndDate");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        public PersonInfo GetModelByPhone(string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.companyid,b.companyname,b.contractStartDate,b.contractEndDate from t_employee a,t_employeeAgreement b ");
            strSql.Append(" where a.phone=@phone and a.id=b.personID ");
            SqlParameter[] parms = {
								    new SqlParameter("@phone", SqlDbType.NVarChar,64)};
            parms[0].Value = phone;
            PersonInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new PersonInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.Sex = SqlHelperGetField.GetString(rdr, "sex");
                        model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
                        model.Country = SqlHelperGetField.GetString(rdr, "country");
                        model.Policy = SqlHelperGetField.GetString(rdr, "policy");
                        model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
                        model.Phone = SqlHelperGetField.GetString(rdr, "phone");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
                        model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
                        model.Bank = SqlHelperGetField.GetString(rdr, "bank");
                        model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
                        model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
                        model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
                        model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
                        model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
                        model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
                        model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
                        model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
                        model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
                        model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
                        model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
                        model.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        model.CompanyName = SqlHelperGetField.GetString(rdr, "companyname");
                        model.ContractStartDate = SqlHelperGetField.GetDateTime(rdr, "contractStartDate");
                        model.ContractEndDate = SqlHelperGetField.GetDateTime(rdr, "contractEndDate");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public PersonInfo GetModelByPhoneWithReg(string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select [id]  ");
            strSql.Append(" ,[personName]  ");
            strSql.Append(" ,[idcard]  ");
            strSql.Append("  ,case when LEN(idcard)=15 then left(idcard,4)+'*********'+RIGHT(idcard,4)  ");
            strSql.Append("  when LEN(idcard)=18 then left(idcard,7)+'********'+right(idcard,4)  ");
            strSql.Append("  else idcard end as idcard ");
            strSql.Append(" ,[sex]  ");
            strSql.Append(" ,[birthday]  ");
            strSql.Append(" ,[country]  ");
            strSql.Append(" ,[nation]  ");
            strSql.Append(" ,[policy]  ");
            strSql.Append(" ,[familyType]  ");
            strSql.Append(" ,[phone]  ");
            strSql.Append(" ,[mail]  ");
            strSql.Append(" ,[emergencyperson]  ");
            strSql.Append(" ,[emergencyphone]  ");
            strSql.Append(" ,[bank]  ");
            strSql.Append(" ,[openbank]  ");
            strSql.Append("  case when LEN(bankaccount) >8 then LEFT(bankaccount,4) + LEFT('**********',LEN(bankaccount)-8)+RIGHT(bankaccount,4)  ");
            strSql.Append("  else bankaccount end as bankaccount, ");
            strSql.Append(" ,[fundaccount]  ");
            strSql.Append(" ,[familyAddress]  ");
            strSql.Append(" ,[familyPostCode]  ");
            strSql.Append(" ,[livingAddress]  ");
            strSql.Append(" ,[livingPostCode]  ");
            strSql.Append(" ,[taxAddress]  ");
            strSql.Append(" ,[taxPostCode]  ");
            strSql.Append(" ,[otherAddress]  ");
            strSql.Append(" ,[otherPostCode]  ");
            strSql.Append(" ,[hometown]  ");
            strSql.Append(" ,[education]  ");
            strSql.Append(" ,[graduateschool]  ");
            strSql.Append(" ,[maritalstatus]  ");
            strSql.Append(" ,[childs]  ");
            strSql.Append(" ,[cityname]  ");
            strSql.Append(" ,[accountType]  ");
            strSql.Append(" ,[emergencyperson2]  ");
            strSql.Append(" ,[emergencyphone2]  ");
            strSql.Append(" ,[childname1]  ");
            strSql.Append(" ,[childbirthday1]  ");
            strSql.Append(" ,[childname2]  ");
            strSql.Append(" ,[childbirthday2] from t_employee  ");
            strSql.Append(" where phone=@phone  ");
            SqlParameter[] parms = {
								    new SqlParameter("@phone", SqlDbType.NVarChar,64)};
            parms[0].Value = phone;
            PersonInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new PersonInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.Sex = SqlHelperGetField.GetString(rdr, "sex");
                        model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
                        model.Country = SqlHelperGetField.GetString(rdr, "country");
                        model.Policy = SqlHelperGetField.GetString(rdr, "policy");
                        model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
                        model.Phone = SqlHelperGetField.GetString(rdr, "phone");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
                        model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
                        model.Bank = SqlHelperGetField.GetString(rdr, "bank");
                        model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
                        model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
                        model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
                        model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
                        model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
                        model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
                        model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
                        model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
                        model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
                        model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
                        model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
                        //model.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        //model.CompanyName = SqlHelperGetField.GetString(rdr, "companyname");
                        //model.ContractStartDate = SqlHelperGetField.GetDateTime(rdr, "contractStartDate");
                        //model.ContractEndDate = SqlHelperGetField.GetDateTime(rdr, "contractEndDate");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public PersonInfo GetModelByEmail(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.companyid,b.companyname,b.contractStartDate,b.contractEndDate from t_employee a,t_employeeAgreement b ");
            strSql.Append(" where a.mail=@email and a.id=b.personID ");
            SqlParameter[] parms = {
								    new SqlParameter("@email", SqlDbType.NVarChar,64)};
            parms[0].Value = email;
            PersonInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new PersonInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.Sex = SqlHelperGetField.GetString(rdr, "sex");
                        model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
                        model.Country = SqlHelperGetField.GetString(rdr, "country");
                        model.Policy = SqlHelperGetField.GetString(rdr, "policy");
                        model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
                        model.Phone = SqlHelperGetField.GetString(rdr, "phone");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
                        model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
                        model.Bank = SqlHelperGetField.GetString(rdr, "bank");
                        model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
                        model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
                        model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
                        model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
                        model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
                        model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
                        model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
                        model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
                        model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
                        model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
                        model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
                        model.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        model.CompanyName = SqlHelperGetField.GetString(rdr, "companyname");
                        model.ContractStartDate = SqlHelperGetField.GetDateTime(rdr, "contractStartDate");
                        model.ContractEndDate = SqlHelperGetField.GetDateTime(rdr, "contractEndDate");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public PersonInfo GetModelByIdCardWithReg(string idcard)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select [id]  ");
      strSql.Append(" ,[personName]  ");
      strSql.Append(" ,[idcard]  ");
      strSql.Append("  ,case when LEN(idcard)=15 then left(idcard,4)+'*********'+RIGHT(idcard,4)  ");
      strSql.Append("  when LEN(idcard)=18 then left(idcard,7)+'********'+right(idcard,4)  ");
      strSql.Append("  else idcard end as idcard ");
      strSql.Append(" ,[sex]  ");
      strSql.Append(" ,[birthday]  ");
      strSql.Append(" ,[country]  ");
      strSql.Append(" ,[nation]  ");
      strSql.Append(" ,[policy]  ");
      strSql.Append(" ,[familyType]  ");
      strSql.Append(" ,[phone]  ");
      strSql.Append(" ,[mail]  ");
      strSql.Append(" ,[emergencyperson]  ");
      strSql.Append(" ,[emergencyphone]  ");
      strSql.Append(" ,[bank]  ");
      strSql.Append(" ,[openbank],  ");
      strSql.Append("  case when LEN(bankaccount) >8 then LEFT(bankaccount,4) + LEFT('**********',LEN(bankaccount)-8)+RIGHT(bankaccount,4)  ");
      strSql.Append("  else bankaccount end as bankaccount ");
      strSql.Append(" ,[fundaccount]  ");
      strSql.Append(" ,[familyAddress]  ");
      strSql.Append(" ,[familyPostCode]  ");
      strSql.Append(" ,[livingAddress]  ");
      strSql.Append(" ,[livingPostCode]  ");
      strSql.Append(" ,[taxAddress]  ");
      strSql.Append(" ,[taxPostCode]  ");
      strSql.Append(" ,[otherAddress]  ");
      strSql.Append(" ,[otherPostCode]  ");
      strSql.Append(" ,[hometown]  ");
      strSql.Append(" ,[education]  ");
      strSql.Append(" ,[graduateschool]  ");
      strSql.Append(" ,[maritalstatus]  ");
      strSql.Append(" ,[childs]  ");
      strSql.Append(" ,[cityname]  ");
      strSql.Append(" ,[accountType]  ");
      strSql.Append(" ,[emergencyperson2]  ");
      strSql.Append(" ,[emergencyphone2]  ");
      strSql.Append(" ,[childname1]  ");
      strSql.Append(" ,[childbirthday1]  ");
      strSql.Append(" ,[childname2]  ");
      strSql.Append(" ,[childbirthday2] from t_employee  ");

            strSql.Append(" where idcard=@idcard ");
            SqlParameter[] parms = {
								    new SqlParameter("@idcard", SqlDbType.NVarChar,64)};
            parms[0].Value = idcard;
            PersonInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new PersonInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.PersonName = SqlHelperGetField.GetString(rdr, "personName");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.Sex = SqlHelperGetField.GetString(rdr, "sex");
                        model.Birthday = SqlHelperGetField.GetDateTime(rdr, "birthday");
                        model.Country = SqlHelperGetField.GetString(rdr, "country");
                        model.Policy = SqlHelperGetField.GetString(rdr, "policy");
                        model.FamilyType = SqlHelperGetField.GetString(rdr, "familyType");
                        model.Phone = SqlHelperGetField.GetString(rdr, "phone");
                        model.Mail = SqlHelperGetField.GetString(rdr, "mail");
                        model.EmergencyPerson = SqlHelperGetField.GetString(rdr, "emergencyperson");
                        model.EmergencyPhone = SqlHelperGetField.GetString(rdr, "emergencyphone");
                        model.Bank = SqlHelperGetField.GetString(rdr, "bank");
                        model.OpenBank = SqlHelperGetField.GetString(rdr, "openbank");
                        model.BankAccount = SqlHelperGetField.GetString(rdr, "bankaccount");
                        model.FundAccount = SqlHelperGetField.GetString(rdr, "fundaccount");
                        model.FamilyAddress = SqlHelperGetField.GetString(rdr, "familyAddress");
                        model.FamilyPostCode = SqlHelperGetField.GetString(rdr, "familyPostCode");
                        model.LivingAddress = SqlHelperGetField.GetString(rdr, "livingAddress");
                        model.LivingPostCode = SqlHelperGetField.GetString(rdr, "livingPostCode");
                        model.TaxAddress = SqlHelperGetField.GetString(rdr, "taxAddress");
                        model.TaxPostCode = SqlHelperGetField.GetString(rdr, "taxPostCode");
                        model.OtherAddress = SqlHelperGetField.GetString(rdr, "otherAddress");
                        model.OtherPostCode = SqlHelperGetField.GetString(rdr, "otherPostCode");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public DataTable GetSupportStaffInfoByPersonId(long personId)
        {
            DataSet ds = null;
            string sqlString = string.Format(@"select name, mail from t_supportStaff where id = (select staffID from t_company where id = (select top 1 companyid from [t_employeeAgreement] where personID = '{0}'))  ", personId);
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqlString.ToString());
                }
                catch (Exception e)
                {
                    throw new Exception("失败！\n" + e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return ds.Tables[0];
        }

        public bool IsExistUserByIdCard(string IdCard)
        {
            bool exists = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_employee ");
            strSql.Append(" where  idcard=@IdCard");
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

    }
}
