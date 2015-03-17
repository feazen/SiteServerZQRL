using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL.Core
{
    public interface IPersonDAO
    {
        PersonInfo GetModel(long personId);

        PersonInfo GetModel1(long personId);

        PersonInfo GetModelByPhone(string phone);

        PersonInfo GetModelByPhoneWithReg(string phone);

        PersonInfo GetModelByEmail(string email);

        PersonInfo GetModelByIdCardWithReg(string idcard);

        DataTable GetSupportStaffInfoByPersonId(long personId);

        bool IsExistUserByIdCard(string IdCard);
    }
}
