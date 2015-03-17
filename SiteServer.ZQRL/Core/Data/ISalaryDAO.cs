using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL.Core
{
    public interface ISalaryDAO
    {
        List<SalaryInfo> GeSalaryListByPersonId(long personId);
    }
}
