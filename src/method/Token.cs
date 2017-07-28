using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using com.pb.shippingapi.model;
using System.Text;
using System.Net;


namespace com.pb.shippingapi
{

    [JsonObject(MemberSerialization.OptIn)]
    internal class TokenRequest : IShippingApiRequest
    {
        [JsonProperty(PropertyName="grant_type")]
        public string GrantType {get;set;}

        [ShippingAPIHeader("Basic")]
        public StringBuilder Authorization {get;set;}
        [ShippingAPIHeader("ContentType")]
        public string ContentType {get;set;}

        public TokenRequest()
        {
            GrantType = "client_credentials";
            ContentType = "application/x-www-form-urlencoded";
            Authorization = new StringBuilder();
        }
        public void BasicAuth(string key, char[] secret )
        {
            var authHeader = new StringBuilder();
            authHeader.Append( key).Append(':').Append( secret);
            var buffer = new char[authHeader.Length];
            authHeader.CopyTo(0, buffer, 0, buffer.Length);
            authHeader.Clear();
            UrlHelper.Encode( Authorization, buffer );
            buffer.Initialize();
        }
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
        public static async Task<ShippingAPIResponse<Token>> token(ShippingApi.Session session = null )
        {
            var request = new TokenRequest();
             try
            {
                if (session == null) session = ShippingApi.DefaultSession;
                request.BasicAuth(session.GetConfigItem("ApiKey"), session.GetAPISecret());
                var response =  await WebMethod.Post<Token, TokenRequest>("/oauth/token", request, session );
                if (response.HttpStatus == HttpStatusCode.OK)
                {
                    session.AuthToken = response.APIResponse;
                }
                return response;
            }
            finally
            {
                request.Authorization.Clear();
            }
        }
    }
}