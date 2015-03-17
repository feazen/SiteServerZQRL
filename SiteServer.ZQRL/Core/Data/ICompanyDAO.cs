using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL.Core
{
    public interface ICompanyDAO
    {
        CompanyInfo GetModel(long companyId);
        CompanyInfo GetModelByCNumberWithReg(string cNumber);
        DataTable GetSupportStaffByCompanyId(string companyId);
        CompanyInfo GetModelByCompanyId(long companyId);

        bool IsExistIdCard(string IdCard);
        int GetTopProvinceId();

        int GetTopFundtypeId();
    }
}
