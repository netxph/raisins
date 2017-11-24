using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Raisins.Client
{
    public abstract class AppConfig
    {

        const string SERVER_BASE_URI_KEY = "ServerBaseUri";

        [ThreadStatic]
        private static AppConfig _instance;

        public static AppConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DefaultAppConfig();
                }

                return _instance;
            }
            set { _instance = value; }
        }

        public abstract string OnGet(string key);

        public static string GetUrl(string path)
        {
            var baseUri = Instance.OnGet(SERVER_BASE_URI_KEY);
            return $"{baseUri}/{path}";
        }

        public static string Get(string key)
        {
            return Instance.OnGet(key);
        }
    }

    public class DefaultAppConfig : AppConfig
    {
        public override string OnGet(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
