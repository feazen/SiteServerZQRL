using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL
{
    public class PersonInfo
    {
        private long id;
        private string personName;
        private string idcard;
        private string sex;
        private DateTime birthday;
        private string country;
        private string nation;
        private string policy;
        private string familyType;
        private string phone;
        private string mail;
        private string emergencyperson;
        private string emergencyphone;
        private string bank;
        private string openbank;
        private string bankaccount;
        private string fundaccount;
        private string familyAddress;
        private string familyPostCode;
        private string livingAddress;
        private string livingPostCode;
        private string taxAddress;
        private string taxPostCode;
        private string otherAddress;
        private string otherPostCode;
        private long companyid;
        private string companyname;
        private DateTime contractStartDate;
        private DateTime contractEndDate;

        public long ID
        {
            set { id = value; }
            get { return id; }
        }

        public string PersonName
        {
            set { personName = value; }
            get { return personName; }
        }

        public string IdCard
        {
            set { idcard = value; }
            get { return idcard; }
        }

        public string Sex
        {
            set { sex = value; }
            get { return sex; }
        }

        public DateTime Birthday
        {
            set { birthday = value; }
            get { return birthday; }
        }

        public string Country
        {
            set { country = value; }
            get { return country; }
        }

        public string Nation
        {
            set { nation = value; }
            get { return nation; }
        }

        public string Policy
        {
            set { policy = value; }
            get { return policy; }
        }

        public string FamilyType
        {
            set { familyType = value; }
            get { return familyType; }
        }

        public string Phone
        {
            set { phone = value; }
            get { return phone; }
        }

        public string Mail
        {
            set { mail = value; }
            get { return mail; }
        }

        public string EmergencyPerson
        {
            set { emergencyperson = value; }
            get { return emergencyperson; }
        }

        public string EmergencyPhone
        {
            set { emergencyphone = value; }
            get { return emergencyphone; }
        }

        public string Bank
        {
            set { bank = value; }
            get { return bank; }
        }

        public string OpenBank
        {
            set { openbank = value; }
            get { return openbank; }
        }

        public string BankAccount
        {
            set { bankaccount = value; }
            get { return bankaccount; }
        }

        public string FundAccount
        {
            set { fundaccount = value; }
            get { return fundaccount; }
        }

        public string FamilyAddress
        {
            set { familyAddress = value; }
            get { return familyAddress; }
        }

        public string FamilyPostCode
        {
            set { familyPostCode = value; }
            get { return familyPostCode; }
        }

        public string LivingAddress
        {
            set { livingAddress = value; }
            get { return livingAddress; }
        }

        public string LivingPostCode
        {
            set { livingPostCode = value; }
            get { return livingPostCode; }
        }

        public string TaxAddress
        {
            set { taxAddress = value; }
            get { return taxAddress; }
        }

        public string TaxPostCode
        {
            set { taxPostCode = value; }
            get { return taxPostCode; }
        }

        public string OtherAddress
        {
            set { otherAddress = value; }
            get { return otherAddress; }
        }

        public string OtherPostCode
        {
            set { otherPostCode = value; }
            get { return otherPostCode; }
        }

        public long CompanyId
        {
            set { companyid = value; }
            get { return companyid; }
        }

        public string CompanyName
        {
            set { companyname = value; }
            get { return companyname; }
        }

        public DateTime ContractStartDate
        {
            set { contractStartDate = value; }
            get { return contractStartDate; }
        }

        public DateTime ContractEndDate
        {
            set { contractEndDate = value; }
            get { return contractEndDate; }
        }

    }
}
