using System.Configuration;

namespace MapLinkApi.Repository
{
    internal class RepositoryUtils
    {
        public static string getTokenMapLink()
        {
            return ConfigurationManager.AppSettings["mapLinkToken"];
        }
    }
}
