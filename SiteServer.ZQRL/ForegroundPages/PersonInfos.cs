using System;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using BaiRong.Core;
//using UserCenter.Core;
using System.Web.UI.HtmlControls;
using System.Web;
using BaiRong.Controls;
using SiteServer.ZQRL;
using System.Collections.Generic;
using System.Data;
using SiteServer.ZQRL.Core.Data;

namespace SiteServer.ZQRL.ForegroundPages
{
    public partial class PersonInfos : System.Web.UI.Page
    {
        protected Label lblPersonId;
        protected Label lblPersonName;
        protected Label lblIdCard;
        protected Label lblSex;
        protected Label lblBirthday;
        protected Label lblCountry;
        protected Label lblNation;
        protected Label lblPolicy;
        protected Label lblFamilyType;
        protected Label lblPhone;
        protected Label lblMail;
        protected Label lblEmergencyPerson;
        protected Label lblEmergencyPhone;
        protected Label lblBank;
        protected Label lblOpenBank;
        protected Label lblBankAccount;
        protected Label lblFundAccount;
        protected Label lblFamilyAddress;
        protected Label lblFamilyPostCode;
        protected Label lblLivingAddress;
        protected Label lblLivingPostCode;
        protected Label lblTaxAddress;
        protected Label lblTaxPostCode;
        protected Label lblOtherAddress;
        protected Label lblOtherPostCode;
        protected Label lblCompanyName;
        protected Label lblContractStartDate;
        protected Label lblContractEndDate;
        protected string login_name="匿名";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];
                if (usersInfo == null || usersInfo.Number == null || usersInfo.Number == "")
                {
                    Response.Redirect("../index.htm");
                }

                GetPersonInfoById(usersInfo.PersonId);
            }
        }

        private void GetPersonInfoById(long personId)
        {
            PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModel(personId);
            login_name = personInfo.PersonName;
            if (personInfo != null)
            {
                //lblPersonId.Text = personInfo.ID.ToString();
                lblPersonName.Text = personInfo.PersonName;
                lblIdCard.Text = personInfo.IdCard;
                lblSex.Text = personInfo.Sex;
                lblBirthday.Text = personInfo.Birthday.ToString("yyyy年MM月dd日");
                lblCountry.Text = personInfo.Country;
                lblNation.Text = personInfo.Nation;
                lblPolicy.Text = personInfo.Policy;
                lblFamilyType.Text = personInfo.FamilyType;
                lblPhone.Text = personInfo.Phone;
                lblMail.Text = personInfo.Mail;
                lblEmergencyPerson.Text = personInfo.EmergencyPerson;
                lblEmergencyPhone.Text = personInfo.EmergencyPhone;
                lblBank.Text = personInfo.Bank;
                lblOpenBank.Text = personInfo.OpenBank;
                lblBankAccount.Text = personInfo.BankAccount;
                lblFundAccount.Text = personInfo.FundAccount;
                lblFamilyAddress.Text = personInfo.FamilyAddress;
                lblFamilyPostCode.Text = personInfo.FamilyPostCode;
                lblLivingAddress.Text = personInfo.LivingAddress;
                lblLivingPostCode.Text = personInfo.LivingPostCode;
                lblTaxAddress.Text = personInfo.TaxAddress;
                lblTaxPostCode.Text = personInfo.TaxPostCode;
                lblOtherAddress.Text = personInfo.OtherAddress;
                lblOtherPostCode.Text = personInfo.OtherPostCode;
                lblCompanyName.Text = personInfo.CompanyName;
                lblContractStartDate.Text = personInfo.ContractStartDate.ToString("yyyy年MM月dd日");
                if (personInfo.ContractEndDate.ToString() == "0001/1/1 0:00:00")
                {
                    lblContractEndDate.Text = string.Empty;
                }
                else
                {
                    lblContractEndDate.Text = personInfo.ContractEndDate.ToString("yyyy年MM月dd日");
                }
            }

        }

    }
}
