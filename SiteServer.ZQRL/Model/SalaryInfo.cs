using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL
{
    public class SalaryInfo
    {
        private long id;
        private long personid;
        private long agreementid;
        private long companyid;
        private DateTime date;
        private string salaryDetail;

        public long ID
        {
            set { id = value; }
            get { return id; }
        }
        public long PersonId
        {
            set { personid = value; }
            get { return personid; }
        }
        public long AgreementId
        {
            set { agreementid = value; }
            get { return agreementid; }
        }
        public long CompanyId
        {
            set { companyid = value; }
            get { return companyid; }
        }
        public DateTime  Date
        {
            set { date = value; }
            get { return date; }
        }
        public string SalaryDetail
        {
            set { salaryDetail = value; }
            get { return salaryDetail; }
        }
    }
}
