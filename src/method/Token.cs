using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using com.pb.shippingapi.model;
using System.Text;


namespace com.pb.shippingapi
{

    internal class TokenRequest
    {
       [JsonProperty(PropertyName="grant_type")]
        public string GrantType {get;set;}

        [ShippingAPIHeader("Authorization")]
        public StringBuilder Authorization {get;set;}
        [ShippingAPIHeader("ContentType")]
        public string ContentType {get;set;}

    }
    public static class TokenExtensions
    {
        public static bool Expired(this Token t)
        {
            return DateTimeOffset.Now >= t.IssuedAt.AddSeconds(t.ExpiresIn);
        }
    }

    public static class TokenMethods
    {
        public static async Task<ShippingAPIResponse<Token>> token(string APIKey, ShippingApi.Session session = null )
        {
            if (session == null) session = ShippingApi.DefaultSession;
            string apiKey = session.GetConfig("APIKey");
            char[] apiSecret = null;

            var request = new TokenRequest() 
            { 
                GrantType = "client_credentials", 
                ContentType = "application/x-www-form-urlencoded"
            };
            try
            {
                apiSecret= session.GetAPISecret();
                var authHeader = new StringBuilder();
                authHeader.Append( apiKey);
                authHeader.Append(':');
                authHeader.Append( apiSecret);
                var buffer = new char[authHeader.Length];
                authHeader.CopyTo(0, buffer, 0, buffer.Length);
                authHeader.Clear();
                request.Authorization.Append("Basic ");
                UrlHelper.Encode(request.Authorization, buffer );
                buffer.Initialize();      

                return await WebMethod.Post<Token, TokenRequest>("/oauth", request, session );
            }
            finally
            {
                apiSecret.Initialize();
                request.Authorization.Clear();
            }
        }
    }
}