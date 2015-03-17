using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL
{
    public class UsersInfo
    {
        private long id; // KeyID主键
        private string number; // 用户ID        
        private string password;
        private string idcard;
        private int usertype;
        private long personid;
        private long companyid;

        private int isShowOther;
        private int isShowFL;
        private int isShowZC;
        private int isRegistFLG;

        private DateTime lastLoginTime;
        public long ID
        {
            set { id = value; }
            get { return id; }
        }

        public string Number
        {
            set { number = value; }
            get { return number; }
        }

        public string PassWord
        {
            set { password = value; }
            get { return password; }
        }

        public string IdCard
        {
            set { idcard = value; }
            get { return idcard; }
        }

        public int UserType
        {
            set { usertype = value; }
            get { return usertype; }
        }

        public long PersonId
        {
            set { personid = value; }
            get { return personid; }
        }
        public long CompanyId
        {
            set { companyid = value; }
            get { return companyid; }
        }

        public int IsShowOther
        {
            set { isShowOther = value; }
            get { return isShowOther; }
        }
        public int IsShowFL
        {
            set { isShowFL = value; }
            get { return isShowFL; }
        }
        public int IsShowZC
        {
            set { isShowZC = value; }
            get { return isShowZC; }
        }

        public DateTime LastLoginTime 
        {
            set { lastLoginTime = value; }
            get { return lastLoginTime; }
        }

        public int IsRegistFLG
        {
            set { isRegistFLG = value; }
            get { return isRegistFLG; }
        }

    }
}
