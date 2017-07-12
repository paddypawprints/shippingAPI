using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace com.pb.shippingapi
{
    public class ShippingAPIEnvironment
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

        private static string SANDBOX_ENDPOINT="https://api-sandbox.pitneybowes.com";
        private static string PRODUCTION_ENDPOINT="https://api.pitneybowes.com";
        private static string SHIPMENT_URI = "/shippingservices/v1/shipments";
        private static string TOKEN_URI = "/oauth";

        private static Environment _defaultEnvironment = Environment.Sandbox;
        internal static HttpClient Client = new HttpClient();
        public static Environment DefaultEnvironment 
        {
            get { return _defaultEnvironment; }
            set { _defaultEnvironment = value;}
        }


        public static string ShipmentURI()
        {
            return ShipmentURI( DefaultEnvironment );
        }
        public static string ShipmentURI( Environment env)
        {

            switch ( env )
            {
                case Environment.Sandbox:
                {
                    return SANDBOX_ENDPOINT + SHIPMENT_URI;
                }
                case Environment.Production:
                {
                    return PRODUCTION_ENDPOINT + SHIPMENT_URI;
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

            switch ( env )
            {
                case Environment.Sandbox:
                {
                    return SANDBOX_ENDPOINT + TOKEN_URI;
                }
                case Environment.Production:
                {
                    return PRODUCTION_ENDPOINT + TOKEN_URI;
                }
            }
            throw new Exception();
        }
    }

}