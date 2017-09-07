using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;
using System.Text;
using System.Net;


namespace PitneyBowes.Developer.ShippingApi
{

    [JsonObject(MemberSerialization.OptIn)]
    internal class TokenRequest : IShippingApiRequest, IDisposable
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

        public void Dispose()
        {
            Authorization.Clear();
        }

    }
    public static class TokenExtensions
    {
        public static bool Expired(this Model.Token t)
        {
            return DateTimeOffset.Now >= t.IssuedAt.AddSeconds(t.ExpiresIn);
        }
    }

    public static class TokenMethods
    {
#pragma warning disable IDE1006 // Naming Styles
        public static async Task<ShippingAPIResponse<T>> token<T>(ShippingApi.Session session = null) where T : IToken, new()

#pragma warning restore IDE1006 // Naming Styles
        {
            using (var request = new TokenRequest())
            {
                if (session == null) session = ShippingApi.DefaultSession;
                request.BasicAuth(session.GetConfigItem("ApiKey"), session.GetAPISecret());
                var jsonResponse = await WebMethod.Post<PitneyBowes.Developer.ShippingApi.Json.JsonToken<T>, TokenRequest>("/oauth/token", request, session);
                if (jsonResponse.HttpStatus == HttpStatusCode.OK)
                {
                    session.AuthToken = jsonResponse.APIResponse;
                }
                return new ShippingAPIResponse<T>() { APIResponse = jsonResponse.APIResponse.Wrapped, Errors = jsonResponse.Errors, HttpStatus = jsonResponse.HttpStatus, Success = jsonResponse.Success };
            }
        }
    }
}