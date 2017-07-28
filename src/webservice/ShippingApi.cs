using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using com.pb.shippingapi.model;

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
            public string Name {get;set;}
            public Token AuthToken {get;set;}

            private static object _clientLock = new object();
            private static Dictionary<string, HttpClient> _clientLookup = new Dictionary<string, HttpClient>();
            public HttpClient Client( string baseUrl )
            {
                HttpClient client;
                lock( _clientLock )
                {
                    if ( !_clientLookup.TryGetValue(baseUrl, out client))
                    {
                        client = new HttpClient();
                        client.BaseAddress = new Uri(baseUrl);
                        _clientLookup.Add(baseUrl, client);
                    }
                    return client;
                }
            } 
            public Func<string,string> GetConfigItem = ShippingApi.GetConfigItem;
            public Action<string,string> AddConfigItem = ShippingApi.AddConfigItem;
            public Action<string> LogWarning = ShippingApi.LogWarning;
            public Action<string> LogError= ShippingApi.LogError;
            public Action<string> LogConfigError= ShippingApi.LogConfigError;
            public Func<char[]> GetAPISecret = ShippingApi.GetAPISecret;
            public string EndPoint {get;set;}
        }
        public static Func<string,string> GetConfigItem = (s)=>{ return _defaultConfigs[s];};
        public static Action<string,string> AddConfigItem = (k,v)=>{ _defaultConfigs.Add(k,v);};
        public static Action<string> LogWarning = (s)=>{};
        public static Action<string> LogError= (s)=>{};
        public static Action<string> LogConfigError= (s)=>{};
        public static Func<char[]> GetAPISecret = ()=>{ return new char[1] {'0'};};

        private static Dictionary<string, string> _defaultConfigs = new Dictionary<string, string>();

        public static Session DefaultSession;

        private static object _initLock = new object();
        public static void Init()
        { 
            lock(_initLock)
            {
                _defaultConfigs.Add("SANDBOX_ENDPOINT", "https://api-sandbox.pitneybowes.com");
                _defaultConfigs.Add("PRODUCTION_ENDPOINT", "https://api-sandbox.pitneybowes.com");

                var sandbox = new Session() { Name = "sandbox", EndPoint = GetConfigItem("SANDBOX_ENDPOINT")};
                var production = new Session() { Name= "production", EndPoint = GetConfigItem("PRODUCTION_ENDPOINT")};
                _defaultSessions.Add("sandbox", sandbox );
                _defaultSessions.Add("production", production );
                DefaultSession = sandbox;
            }
        }

        internal static Dictionary<string, Session >  _defaultSessions = new Dictionary<string, Session>();
    }
}