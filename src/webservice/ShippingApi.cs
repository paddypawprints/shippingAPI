using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;

namespace com.pb.shippingapi
{

    public static class ShippingApi
    {
            public class Session
        {
            public static implicit operator Session(string name)
            {
                return ShippingApi._defaultSessions[name];
            }
            public string Name;
            public string AuthToken;
            public HttpClient Client = new HttpClient();
            public Func<string,string> GetConfig = ShippingApi.GetConfig;
            public Action<string> LogWarning = ShippingApi.LogWarning;
            public Action<string> LogError= ShippingApi.LogError;
            public Action<string> LogConfigError= ShippingApi.LogConfigError;
            public Func<char[]> GetAPISecret = ShippingApi.GetAPISecret;
            public string EndPoint {get;set;}
        }
        public static Func<string,string> GetConfig = (s)=>{ return _defaultConfigs[s];};
        public static Action<string> LogWarning = (s)=>{};
        public static Action<string> LogError= (s)=>{};
        public static Action<string> LogConfigError= (s)=>{};
        public static Func<char[]> GetAPISecret = ()=>{ return new char[1] {'0'};};

        private static Dictionary<string, string> _defaultConfigs = new Dictionary<string, string>();

        public static Session DefaultSession;
        public static void Init()
        { //TODO: do it properly without race condition

             _defaultConfigs.Add("SANDBOX_ENDPOINT", "https://api-sandbox.pitneybowes.com");
            _defaultConfigs.Add("PRODUCTION_ENDPOINT", "https://api-sandbox.pitneybowes.com");

            var sandbox = new Session() { Name = "sandbox", EndPoint = GetConfig("SANDBOX_ENDPOINT")};
            var production = new Session() { Name= "production", EndPoint = GetConfig("PRODUCTION_ENDPOINT")};
            _defaultSessions.Add("sandbox", sandbox );
            _defaultSessions.Add("production", production );
            DefaultSession = sandbox;
        }

        internal static Dictionary<string, Session >  _defaultSessions = new Dictionary<string, Session>();
    }
}