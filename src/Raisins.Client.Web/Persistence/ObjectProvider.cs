using Raisins.Client.Web.Services;

namespace Raisins.Client.Web.Persistence
{
    public class ObjectProvider
    {

        static object _lockObject = new object();

        protected static RaisinsDB DB { get; set; }

        public static RaisinsDB CreateDB()
        {
            return new RaisinsDB();
        }

        public static IHttpHelper CreateHttpHelper()
        {
            return new HttpHelper();
        }

    }
}