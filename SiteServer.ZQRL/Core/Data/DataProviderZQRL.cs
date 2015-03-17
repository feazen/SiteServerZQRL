using System.Reflection;
using BaiRong.Core.Data.Provider;
using BaiRong.Model;
using BaiRong.Core;

namespace SiteServer.ZQRL.Core.Data
{
    public class DataProviderZQRL
    {
        private static string assemblyString;
        private static string namespaceString;

        static DataProviderZQRL()
		{
            assemblyString = "SiteServer.ZQRL";
            namespaceString = "SiteServer.ZQRL.Provider.Data.SqlServer";
		}

        private static IUsersDAO usersDAO;
        public static IUsersDAO UsersDAO
        {
            get
            {
                if (usersDAO == null)
                {
                    string className = namespaceString + ".UsersDAO";
                    usersDAO = (IUsersDAO)Assembly.Load(assemblyString).CreateInstance(className);
                }
                return usersDAO;
            }
        }

        private static IPersonDAO personDAO;

        public static IPersonDAO PersonDAO
        {
            get
            {
                if (personDAO == null)
                {
                    string className = namespaceString + ".PersonDAO";
                    personDAO = (IPersonDAO) Assembly.Load(assemblyString).CreateInstance(className);
                }
                return personDAO;
            }
        }

        private static ISmsDAO smsDAO;

        public static ISmsDAO SmsDAO
        {
            get
            {
                if (personDAO == null)
                {
                    string className = namespaceString + ".SmsDAO";
                    personDAO = (IPersonDAO)Assembly.Load(assemblyString).CreateInstance(className);
                }
                return smsDAO;
            }
        }

        private static ICompanyDAO companyDAO;

        public static ICompanyDAO CompanyDAO
        {
            get
            {
                if (companyDAO == null)
                {
                    string className = namespaceString + ".CompanyDAO";
                    companyDAO = (ICompanyDAO)Assembly.Load(assemblyString).CreateInstance(className);
                }
                return companyDAO;
            }
        }
    }
}
