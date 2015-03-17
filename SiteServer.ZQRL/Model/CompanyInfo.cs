using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL
{
    public class CompanyInfo
    {
        private long id;
        private string name;
        private string number;
        private string mail;
        private long staffID;
        private long staffID2;
        private int isShowOther;
        private int isShowFL;
        private int isShowZC;
        public long ID
        {
            set { id = value; }
            get { return id; }
        }

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public string Number
        {
            set { number = value; }
            get { return number; }
        }

        public string Mail
        {
            set { mail = value; }
            get { return mail; }
        }

        public long StaffID
        {
            set { staffID = value; }
            get { return staffID; }
        }

        public long StaffID2
        {
            set { staffID2 = value; }
            get { return staffID2; }
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
    }
}
