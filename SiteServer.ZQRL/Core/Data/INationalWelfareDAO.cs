using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SiteServer.ZQRL.Core
{
    public interface INationalWelfareDAO
    {
        List<NationalWelfareInfo> GetNationalWelfareListByPersonId(long personId);

    }
}
