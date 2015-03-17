using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
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
    public partial class PersonInfoEdit : System.Web.UI.Page
    {
        protected Label lblPersonId;
        protected Label lblPersonName;
        protected Label lblIdCard;
        protected Label lblSex;
        protected Label lblBirthday;
        protected Label lblCountry;
        protected Label lblNation;
        //protected TextBox txtPolicy;
        public DropDownList ddlPolicy;
        protected FileUpload fulPolicy;
        protected Label lblPolicyMsg;
        protected TextBox txtFamilyType;
        protected FileUpload fulFamilyType;
        protected Label lblFamilyTypeMsg;
        //protected Label lblPhone;
        protected TextBox txtPhone;
        //protected Label lblMail;
        protected TextBox txtMail;
        //protected Label lblEmergencyPerson;
        protected TextBox txtEmergencyPerson;
        //protected Label lblEmergencyPhone;
        protected TextBox txtEmergencyPhone;
        protected Label lblBank;
        protected Label lblOpenBank;
        protected Label lblBankAccount;
        protected Label lblFundAccount;
        protected Label lblFamilyAddress;
        protected Label lblFamilyPostCode;
        //protected Label lblLivingAddress;
        protected TextBox txtLivingAddress;
        //protected Label lblLivingPostCode;
        protected TextBox txtLivingPostCode;
        protected Label lblTaxAddress;
        protected Label lblTaxPostCode;
        protected Label lblOtherAddress;
        protected Label lblOtherPostCode;
        protected Label lblCompanyName;
        protected Label lblContractStartDate;
        protected Label lblContractEndDate;

        protected Button BtnUploadPolicy;
        protected Button BtnUploadFamilyType;
        protected ImageButton UpdatePersonInfoBtn;

        protected HiddenField hidfilPolicyPath;
        protected HiddenField hidfilFamilyTypePath;
        protected HiddenField hidfilIdCard;

        protected string login_name = "匿名";
        protected string Policypath = string.Empty;
        protected string FamilyTypepath = string.Empty;
        protected long personId;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPolicy.Items.Add(new ListItem("党员", "0"));
                this.ddlPolicy.Items.Add(new ListItem("团员", "1"));
                this.ddlPolicy.Items.Add(new ListItem("群众", "2"));
                this.ddlPolicy.Items.Add(new ListItem("其他党派", "3"));
                 
                UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];
                if (usersInfo == null || usersInfo.Number == null || usersInfo.Number == "")
                {
                    Response.Redirect("../index.htm");
                }
                personId = usersInfo.PersonId;
                GetPersonInfoById(personId);
            }
        }

        private void GetPersonInfoById(long personId)
        {
            PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModel(personId);
            PersonInfo personInfo1 = DataProviderZQRL.PersonDAO.GetModel1(personId);
            login_name = personInfo.PersonName;
            if (personInfo != null && personInfo1!=null)
            {
                //lblPersonId.Text = personInfo.ID.ToString();
                lblPersonName.Text = personInfo.PersonName;
                lblIdCard.Text = personInfo.IdCard;
                hidfilIdCard.Value = personInfo1.IdCard;
                lblSex.Text = personInfo.Sex;
                lblBirthday.Text = personInfo.Birthday.ToString("yyyy年MM月dd日");
                lblCountry.Text = personInfo.Country;
                lblNation.Text = personInfo.Nation;

                if (personInfo.Policy == "党员")
                {
                    ddlPolicy.SelectedIndex = 0;
                }

                else if (personInfo.Policy == "团员")
                {
                    ddlPolicy.SelectedIndex = 1;
                }

                else if (personInfo.Policy == "群众")
                {
                    ddlPolicy.SelectedIndex = 2;
                }

                else if (personInfo.Policy == "其他党派")
                {
                    ddlPolicy.SelectedIndex = 3;
                }
                //ddlPolicy.SelectedIndex = personInfo.Policy.ToString();
                txtFamilyType.Text = personInfo.FamilyType;
                //lblPhone.Text = personInfo.Phone;
                txtPhone.Text = personInfo.Phone;
                //lblMail.Text = personInfo.Mail;
                txtMail.Text = personInfo.Mail;
                //lblEmergencyPerson.Text = personInfo.EmergencyPerson;
                txtEmergencyPerson.Text = personInfo.EmergencyPerson;
                //lblEmergencyPhone.Text = personInfo.EmergencyPhone;
                txtEmergencyPhone.Text = personInfo.EmergencyPhone;
                lblBank.Text = personInfo.Bank;
                lblOpenBank.Text = personInfo.OpenBank;
                lblBankAccount.Text = personInfo.BankAccount;
                lblFundAccount.Text = personInfo.FundAccount;
                lblFamilyAddress.Text = personInfo.FamilyAddress;
                lblFamilyPostCode.Text = personInfo.FamilyPostCode;
                txtLivingAddress.Text = personInfo.LivingAddress;
                txtLivingPostCode.Text = personInfo.LivingPostCode;
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

        protected void BtnUploadPolicy_Click(object sender, EventArgs e)
        {
            string name = fulPolicy.PostedFile.FileName;//获取文件名称
            int index = name.LastIndexOf(".");
            string lastName = name.Substring(index, name.Length - index);//文件后缀
            string newname =lblPersonName.Text+"_政治面貌_"+ DateTime.Now.ToString("yyyyMMddhhmmss") + lastName;//新文件名
            Policypath = Server.MapPath("~/upload/" + newname);
            hidfilPolicyPath.Value = Policypath;
            if (fulPolicy.HasFile)
            {
                try
                {
                    fulPolicy.SaveAs(Policypath);
                    lblPolicyMsg.Text = "文件上传成功!";
                }
                catch (Exception ex)
                {
                    lblPolicyMsg.Text = "文件上传不成功!";
                }
            }
        }

        protected void BtnUploadFamilyType_Click(object sender, EventArgs e)
        {
            string name = fulFamilyType.PostedFile.FileName;//获取文件名称
            int index = name.LastIndexOf(".");
            string lastName = name.Substring(index, name.Length - index);//文件后缀
            string newname = lblPersonName.Text + "_户籍性质_" + DateTime.Now.ToString("yyyyMMddhhmmss") + lastName;//新文件名
            FamilyTypepath = Server.MapPath("~/upload/" + newname);
            hidfilFamilyTypePath.Value = FamilyTypepath;
            if (fulFamilyType.HasFile)
            {
                try
                {
                    fulFamilyType.SaveAs(FamilyTypepath);
                    lblFamilyTypeMsg.Text = "文件上传成功.";
                }
                catch (Exception ex)
                {
                    lblFamilyTypeMsg.Text = "文件上传不成功.";
                }
            }
        }

        protected void UpdatePersonInfoBtn_Click(object sender, EventArgs e)
        {
            string to = string.Empty;
            string toer = string.Empty;
            UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];
            DataTable dtTable = DataProviderZQRL.PersonDAO.GetSupportStaffInfoByPersonId(usersInfo.PersonId);
            if (dtTable != null && dtTable.Rows.Count > 0)
            {
                to = dtTable.Rows[0]["mail"].ToString();
                toer = dtTable.Rows[0]["name"].ToString();
            }
            //string to = "743860161@qq.com";
            //string toer = "测试修改个人信息";
            string from = "huhu@hr-channel.com";
            string fromer = lblPersonName.Text;
            
            string Subject = "中企人力修改个人信息";
            string file1 = hidfilPolicyPath.Value;
            string file2 = hidfilFamilyTypePath.Value;
            string Body = SendContentBody();
            string SMTPHost = "smtp.ym.163.com";
            string SMTPuser = "huhu@hr-channel.com";
            string SMTPpass = "111111";

            try
            {
                sendmail(from, fromer, to, toer, Subject, Body, file1, file2, SMTPHost, SMTPuser, SMTPpass);
                string Url = "../Person/PersonInfoEdit.aspx";
                Response.Write("<script languge='javascript'>alert('修改信息已发送成功，客服将对您的信息进行核对并更改！'); window.location.href='" + Url + "'</script>");
            }
            catch (Exception)
            {
                string Url = "../Person/PersonInfoEdit.aspx";
                Response.Write("<script languge='javascript'>alert('发送邮件失败，请致电联系客服人员修改!'); window.location.href='" + Url + "'</script>");
            }
        }

        public bool sendmail(string sfrom, string sfromer, string sto, string stoer, string sSubject, string sBody, string sfile1,string sfile2, string sSMTPHost, string sSMTPuser, string sSMTPpass)
        {
            ////设置from和to地址
            MailAddress from = new MailAddress(sfrom, sfromer);
            MailAddress to = new MailAddress(sto, stoer);

            ////创建一个MailMessage对象
            MailMessage oMail = new MailMessage(from, to);

            //// 添加附件
            if (!string.IsNullOrEmpty(sfile1))
            {
                oMail.Attachments.Add(new Attachment(sfile1));
            }

            if (!string.IsNullOrEmpty(sfile2))
            {
                oMail.Attachments.Add(new Attachment(sfile2));
            }


            ////邮件标题
            oMail.Subject = sSubject;


            ////邮件内容
            oMail.Body = sBody;

            ////邮件格式
            oMail.IsBodyHtml = false;

            ////邮件采用的编码
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");

            ////设置邮件的优先级为高
            oMail.Priority = MailPriority.High;

            ////发送邮件
            SmtpClient client = new SmtpClient();
            ////client.UseDefaultCredentials = false; 
            client.Host = sSMTPHost;
            client.Credentials = new NetworkCredential(sSMTPuser, sSMTPpass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(oMail);
                return true;
            }
            catch (Exception err)
            {
                Response.Write(err.Message.ToString());
                return false;
            }
            finally
            {
                ////释放资源
                oMail.Dispose();
            }

        }

        protected string SendContentBody()
        {
            UsersInfo usersInfo = (UsersInfo)Session["UserInfo"];
            PersonInfo personInfo = DataProviderZQRL.PersonDAO.GetModel(usersInfo.PersonId);

            StringBuilder stringBuilder=new StringBuilder();
            stringBuilder.AppendFormat("{0}申请修改个人信息，个人证件号为：{1}\r\n", lblPersonName.Text, hidfilIdCard.Value);
            stringBuilder.Append("修改信息如下：");

            if (personInfo.Policy != ddlPolicy.SelectedValue)
            {
                stringBuilder.AppendFormat("政治面貌：{0}\r\n", ddlPolicy.SelectedItem.Text);
            }
            if (personInfo.FamilyType != txtFamilyType.Text)
            {
                stringBuilder.AppendFormat("户籍性质：{0}\r\n", txtFamilyType.Text);
            }
            if (personInfo.Phone != txtPhone.Text)
            {
                stringBuilder.AppendFormat("常用电话：{0}\r\n", txtPhone.Text);
            }
            if (personInfo.Mail != txtMail.Text)
            {
                stringBuilder.AppendFormat("常用邮箱：{0}\r\n", txtMail.Text);
            }
            if (personInfo.EmergencyPerson != txtEmergencyPerson.Text)
            {
                stringBuilder.AppendFormat("紧急联系人：{0}\r\n", txtEmergencyPerson.Text);
            }
            if (personInfo.EmergencyPhone != txtEmergencyPhone.Text)
            {
                stringBuilder.AppendFormat("紧急联系电话：{0}\r\n", txtEmergencyPhone.Text);
            }
            if (personInfo.LivingAddress != txtLivingAddress.Text)
            {
                stringBuilder.AppendFormat("居住地址：{0}\r\n", txtLivingAddress.Text);
            }
            if (personInfo.LivingPostCode != txtLivingPostCode.Text)
            {
                stringBuilder.AppendFormat("居住地址邮编：{0}\r\n", txtLivingPostCode.Text);
            }
            return stringBuilder.ToString();
        }
    }
}
