using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL.Core
{
    public interface IUsersDAO
    {
        UsersInfo GetModel(string LoginName);

        UsersInfo GetModelByIdCard(string IdCard);

        UsersInfo GetModel(string LoginName, string PassWord);

        bool IsExistUserByLoginName(string LoginName);

        bool IsExistUserByLoginName(string LoginName, string IdCard);

        bool IsExistUserByIdCard(string IdCard);

        UsersInfo GetModelById(long Id);

        bool ChangePwd(string LoginName, string IdCard, string Password, string type);

        bool UpdateLastLoginTime(string LoginName);

        bool UpdateFlgRegistStatus(string LoginName,string email);

        long Add(UsersInfo model);
    }
}
