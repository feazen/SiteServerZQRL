using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using BaiRong.Core.Data.Provider;
using BaiRong.Model;
using SiteServer.ZQRL.Core;

namespace SiteServer.ZQRL.Provider.Data.SqlServer
{
    public class UsersDAO : DataProviderBase, IUsersDAO
    {
        public UsersInfo GetModel(string LoginName,string PassWord)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_user ");
            strSql.Append(" where number=@LoginName ");
            strSql.Append(" and password=@PassWord ");
            SqlParameter[] parms = {
								    new SqlParameter("@LoginName", SqlDbType.NVarChar,64),
                                    new SqlParameter("@PassWord",SqlDbType.NVarChar,64)};
            parms[0].Value = LoginName;
            parms[1].Value = PassWord;
            UsersInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new UsersInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.Number = SqlHelperGetField.GetString(rdr, "number");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "password");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "PassWord");
                        model.UserType = SqlHelperGetField.GetInt(rdr, "usertype");
                        model.PersonId = SqlHelperGetField.GetInt64(rdr, "personid");
                        model.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        model.IsShowOther = SqlHelperGetField.GetInt(rdr, "isShowOther");
                        model.IsShowFL = SqlHelperGetField.GetInt(rdr, "isShowFL");
                        model.IsShowZC = SqlHelperGetField.GetInt(rdr, "isShowZC");
                        model.IsRegistFLG = SqlHelperGetField.GetInt(rdr, "isRegistFLG");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public UsersInfo GetModel(string LoginName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_user ");
            strSql.Append(" where number=@LoginName ");
         
            SqlParameter[] parms = {
								    new SqlParameter("@LoginName", SqlDbType.NVarChar,64)
                                   };
            parms[0].Value = LoginName;
         
            UsersInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new UsersInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.Number = SqlHelperGetField.GetString(rdr, "number");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "password");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "PassWord");
                        model.UserType = SqlHelperGetField.GetInt(rdr, "usertype");
                        model.PersonId = SqlHelperGetField.GetInt64(rdr, "personid");
                        model.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        model.IsShowOther = SqlHelperGetField.GetInt(rdr, "isShowOther");
                        model.IsShowFL = SqlHelperGetField.GetInt(rdr, "isShowFL");
                        model.IsShowZC = SqlHelperGetField.GetInt(rdr, "isShowZC");
                        model.IsRegistFLG = SqlHelperGetField.GetInt(rdr, "isRegistFLG");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public UsersInfo GetModelByIdCard(string IdCard)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_user ");
            strSql.Append(" where idCard=@IdCard ");

            SqlParameter[] parms = {
								    new SqlParameter("@IdCard", SqlDbType.NVarChar,64)
                                   };
            parms[0].Value = IdCard;

            UsersInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new UsersInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.Number = SqlHelperGetField.GetString(rdr, "number");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "password");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "PassWord");
                        model.UserType = SqlHelperGetField.GetInt(rdr, "usertype");
                        model.PersonId = SqlHelperGetField.GetInt64(rdr, "personid");
                        model.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        model.IsShowOther = SqlHelperGetField.GetInt(rdr, "isShowOther");
                        model.IsShowFL = SqlHelperGetField.GetInt(rdr, "isShowFL");
                        model.IsShowZC = SqlHelperGetField.GetInt(rdr, "isShowZC");
                        model.IsRegistFLG = SqlHelperGetField.GetInt(rdr, "isRegistFLG");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
         
        public bool IsExistUserByLoginName(string LoginName)
        {
            bool exists = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_user ");
            strSql.Append(" where number=@LoginName ");
            SqlParameter[] parms = {
								    new SqlParameter("@LoginName", SqlDbType.NVarChar,64)};
            parms[0].Value = LoginName;

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

        public bool IsExistUserByLoginName(string LoginName, string IdCard)
        {
            bool exists = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_user ");
            strSql.Append(" where number=@LoginName or IdCard=@IdCard");
            SqlParameter[] parms = {
								    new SqlParameter("@LoginName", SqlDbType.NVarChar,64),
                                    new SqlParameter("@IdCard", SqlDbType.NVarChar,64)
                                    };
            parms[0].Value = LoginName;
            parms[1].Value = IdCard;
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

        public bool IsExistUserByIdCard( string IdCard)
        {
            bool exists = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_user ");
            strSql.Append(" where  IdCard=@IdCard");
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

        public UsersInfo GetModelById(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from t_user ");
            strSql.Append(" where id=@Id ");
            SqlParameter[] parms = {
								    new SqlParameter("@Id", SqlDbType.BigInt,64)};
            parms[0].Value = Id;
            UsersInfo model = null;

            try
            {
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
                {
                    if (rdr.Read())
                    {
                        model = new UsersInfo();
                        model.ID = SqlHelperGetField.GetInt64(rdr, "id");
                        model.Number = SqlHelperGetField.GetString(rdr, "number");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "password");
                        model.IdCard = SqlHelperGetField.GetString(rdr, "idcard");
                        model.PassWord = SqlHelperGetField.GetString(rdr, "PassWord");
                        model.UserType = SqlHelperGetField.GetInt(rdr, "usertype");
                        model.PersonId = SqlHelperGetField.GetInt64(rdr, "personid");
                        model.CompanyId = SqlHelperGetField.GetInt64(rdr, "companyid");
                        model.IsShowOther = SqlHelperGetField.GetInt(rdr, "isShowOther");
                        model.IsShowFL = SqlHelperGetField.GetInt(rdr, "isShowFL");
                        model.IsShowZC = SqlHelperGetField.GetInt(rdr, "isShowZC");
                        model.IsRegistFLG = SqlHelperGetField.GetInt(rdr, "isRegistFLG");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public bool ChangePwd(string LoginName, string IdCard, string Password, string type)
        {
            bool isSuccess = false;

            StringBuilder strSql = new StringBuilder();

            try
            {
                if (type == "0")
                {
                    strSql.Append("update t_user set password =@password ");
                    strSql.Append(" where number=@LoginName ");
                    SqlParameter[] parms = {
								    new SqlParameter("@LoginName",  SqlDbType.NVarChar,64),
                                    new SqlParameter("@Password",  SqlDbType.NVarChar,64)
                                   };
                    parms[0].Value = LoginName;
                    parms[1].Value = Password;
                    SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms);
                }
                else
                { 
                    strSql.Append("update t_user set password =@password ");
                    strSql.Append(" where idcard=@IdCard ");
                    SqlParameter[] parms = {
								    new SqlParameter("@IdCard",  SqlDbType.NVarChar,64),
                                    new SqlParameter("@Password",  SqlDbType.NVarChar,64)
                                   };
                    parms[0].Value = IdCard;
                    parms[1].Value = Password;
                    SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms);
                }
                 
                isSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        /// <summary>
        /// 描述：更新最后登录信息
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public bool UpdateLastLoginTime(string LoginName)
        {
            bool isSuccess = false;

            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append("update t_user set lastLoginTime =@lastLoginTime ");
                    strSql.Append(" where number=@LoginName ");
                    SqlParameter[] parms = {
								    new SqlParameter("@LoginName",  SqlDbType.NVarChar,64),
                                    new SqlParameter("@lastLoginTime",  SqlDbType.DateTime)
                                   };
                    parms[0].Value = LoginName;
                    parms[1].Value = DateTime.Now;
                    SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms);
                 
                isSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        
        /// <summary>
        /// 描述：更新福利高账户状态
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public bool UpdateFlgRegistStatus(string LoginName,string email)
        {
            bool isSuccess = false;

            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append("update t_user set isRegistFLG = 1 ,email = @email");
                
                    strSql.Append(" where number=@LoginName ");
                    SqlParameter[] parms = {
								    new SqlParameter("@LoginName",  SqlDbType.NVarChar,64),
                                    new SqlParameter("@email",  SqlDbType.NVarChar,64)
                                   };
                    parms[0].Value = LoginName;
                    parms[1].Value = email;
                    SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms);
                 
                isSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }
 

        //public UsersInfo GetModel(int KeyID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * from pantum_tdUser ");
        //    strSql.Append(" where KeyID=@KeyID");
        //    SqlParameter[] parms = {
        //                                    new SqlParameter("@KeyID", SqlDbType.Int,4)};
        //    parms[0].Value = KeyID;

        //    UserInfo model = null;

        //    try
        //    {
        //        using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parms))
        //        {
        //            if (rdr.Read())
        //            {
        //                model = new UserInfo();
        //                model.KeyID = KeyID;
        //                model.UserID = SqlHelperGetField.GetString(rdr, "UserID");
        //                model.WeiXinID = SqlHelperGetField.GetString(rdr, "WeiXinID");
        //                model.Name = SqlHelperGetField.GetString(rdr, "Name");
        //                model.NickName = SqlHelperGetField.GetString(rdr, "NickName");
        //                model.PassWord = SqlHelperGetField.GetString(rdr, "PassWord");
        //                model.UserType = SqlHelperGetField.GetString(rdr, "UserType");
        //                model.Mobile = SqlHelperGetField.GetString(rdr, "Mobile");
        //                model.AddTime = SqlHelperGetField.GetDateTime(rdr, "AddTime");
        //                model.State = SqlHelperGetField.GetString(rdr, "State");
        //                model.MobileCode = SqlHelperGetField.GetString(rdr, "MobileCode");
        //                model.MobileCodeTime = SqlHelperGetField.GetDateTime(rdr, "MobileCodeTime");
        //                model.LogTimes = SqlHelperGetField.GetInt(rdr, "LogTimes");
        //                model.ErrorLogTimes = SqlHelperGetField.GetInt(rdr, "ErrorLogTimes");
        //                model.QNum = SqlHelperGetField.GetString(rdr, "QNum");
        //                model.SinaWeiBo = SqlHelperGetField.GetString(rdr, "SinaWeiBo");
        //                model.Email = SqlHelperGetField.GetString(rdr, "Email");
        //                model.Addr = SqlHelperGetField.GetString(rdr, "Addr");
        //                model.GiftAddr = SqlHelperGetField.GetString(rdr, "GiftAddr");
        //                model.Token = SqlHelperGetField.GetString(rdr, "Token");
        //                model.LastLoginDate = SqlHelperGetField.GetDateTime(rdr, "LastLoginDate");
        //                model.DistCode = SqlHelperGetField.GetString(rdr, "DistCode");
        //                model.Sex = SqlHelperGetField.GetString(rdr, "Sex");
        //                model.AgencyID = SqlHelperGetField.GetInt(rdr, "AgencyID");
        //                model.IsCheckEmail = SqlHelperGetField.GetString(rdr, "IsCheckEmail");
        //                model.Phone = SqlHelperGetField.GetString(rdr, "Phone");
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return model;
        //}

        /// <summary>
        /// 新增加一个用户
        /// </summary>
        public long Add(UsersInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_user(");
            strSql.Append("number,password,idcard,usertype,personid,companyid )");
            strSql.Append(" values (");
            strSql.Append("@number,@password,@idcard,@usertype,@personid,@companyid)");
            SqlParameter[] parms = {
                                        new SqlParameter("@number", SqlDbType.NVarChar,64),
                                        new SqlParameter("@password", SqlDbType.NVarChar,64),
                                        new SqlParameter("@idcard", SqlDbType.NVarChar,64),
                                        new SqlParameter("@usertype", SqlDbType.Int,64),
                                        new SqlParameter("@personid", SqlDbType.BigInt,64),
                                        new SqlParameter("@companyid", SqlDbType.BigInt,64),

                                    };
            parms[0].Value = model.Number;
            parms[1].Value = model.PassWord;
            parms[2].Value = model.IdCard;
            parms[3].Value = model.UserType;
            parms[4].Value = model.PersonId;
            parms[5].Value = model.CompanyId;
            
            using (SqlConnection conn = new SqlConnection(SqlHelper.CONN_STRING))
            {
                conn.Open();
                try
                {
                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parms);
                    using (SqlDataReader rdr = SqlHelper.ExecuteReader(conn, CommandType.Text, "SELECT @@IDENTITY AS 'ID'"))
                    {
                        if (rdr.Read())
                        {
                            model.ID = int.Parse(rdr.GetDecimal(0).ToString());
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return model.ID;
        }
    }
}
