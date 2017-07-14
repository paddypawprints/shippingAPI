using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;

namespace com.pb.shippingapi
{
    public static class ShippingAPIEnvironment
    {
        public enum Environment
        {
            Sandbox,
            Production
        }

        public enum Message
        {
            EnvDoesNotExist
        }
        private static Dictionary<string, string> _defaultConfigs = new Dictionary<string, string>();
        private static bool _initialized = false;
        private static void Init(Environment env = Environment.Sandbox)
        { //TODO: do it properly without race condition
            _initialized = true;
            _defaultConfigs.Add("SANDBOX_ENDPOINT", "https://api-sandbox.pitneybowes.com");
            _defaultConfigs.Add("PRODUCTION_ENDPOINT", "https://api-sandbox.pitneybowes.com");
            _defaultConfigs.Add("PTOKEN_URI", "/oauth");
            _defaultConfigs.Add("SHIPMENT_URI", "/shippingservices/v1/shipments");
            DefaultEnvironment = env;
            GetConfig = (s)=>_defaultConfigs[s];
            LogWarning = (s)=>{};
            LogError = (s)=>{};
            LogConfigError = (s)=>{};

        }

        private static Environment _defaultEnvironment = Environment.Sandbox;
        internal static HttpClient Client = new HttpClient();
        public static Environment DefaultEnvironment 
        {
            get 
            {  if (!_initialized ) Init();
                return _defaultEnvironment; 
            }
            set 
            {   if (!_initialized ) Init();
                _defaultEnvironment = value;
            }
        }

        public static Func<string,string> GetConfig;
        public static Action<string> LogWarning;
        public static Action<string> LogError;
        public static Action<string> LogConfigError;
        public static string ShipmentURI()
        {
            return ShipmentURI( DefaultEnvironment );
        }
        public static string ShipmentURI( Environment env)
        {

            if (!_initialized ) Init();
            switch ( env )
            {
                case Environment.Sandbox:
                {
                    return GetConfig("SANDBOX_ENDPOINT") + GetConfig("SHIPMENT_URI");
                }
                case Environment.Production:
                {
                    return GetConfig("PRODUCTION_ENDPOINT") + GetConfig("SHIPMENT_URI");
                }
            }
            throw new Exception();
        }
       public static string TokenURI()
        {
            return TokenURI( DefaultEnvironment );
        }
        public static string TokenURI( Environment env)
        {
            if (!_initialized ) Init();
            switch ( env )
            {
                case Environment.Sandbox:
                {
                     return GetConfig("SANDBOX_ENDPOINT") + GetConfig("TOKEN_URI");
                }
                case Environment.Production:
                {
                    return  GetConfig("PRODUCTION_ENDPOINT") + GetConfig("SHIPMENT_URI");

                }
            }
            LogConfigError("No such environment");
            throw new Exception();
        }
    }

}