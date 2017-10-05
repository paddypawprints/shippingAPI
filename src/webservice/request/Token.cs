using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;
using System.Text;
using System.Net;


namespace PitneyBowes.Developer.ShippingApi
{

    [JsonObject(MemberSerialization.OptIn)]
    internal class TokenRequest : ShippingApiRequest, IDisposable
    {
        [JsonProperty(PropertyName="grant_type")]
        public string GrantType {get => "client_credentials";}

        [ShippingApiHeaderAttribute("Basic")]
        public override StringBuilder Authorization {get;set;}


        public override string ContentType  => "application/x-www-form-urlencoded"; 

        public void BasicAuth(string key, StringBuilder secret ) //TODO: make this better
        {
            var authHeader = new StringBuilder();
            authHeader.Append( key).Append(':').Append( secret);
            secret.Clear();
            var buffer = new char[authHeader.Length];
            authHeader.CopyTo(0, buffer, 0, buffer.Length);
            authHeader.Clear();
            var bytes = Encoding.UTF8.GetBytes( buffer );
            var base64array = new char[(bytes.Length * 4) / 3 + 10];
            Convert.ToBase64CharArray(bytes, 0, bytes.Length, base64array, 0);
            if (Authorization == null) Authorization = new StringBuilder(base64array.Length);
            else Authorization.Clear();
            foreach( var c in base64array)
            {
                Authorization.Append(c);
                if (c == '=') break;
            }
            bytes.Initialize();
            buffer.Initialize();
            base64array.Initialize();
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
        public static async Task<ShippingApiResponse<T>> token<T>(ISession session = null) where T : IToken, new()

#pragma warning restore IDE1006 // Naming Styles
        {
            using (var request = new TokenRequest())
            {
                if (session == null) session = Globals.DefaultSession;
                request.BasicAuth(session.GetConfigItem("ApiKey"), session.GetAPISecret());
                var jsonResponse = await session.Requester.HttpRequest<JsonToken<T>, TokenRequest>("/oauth/token", HttpVerb.POST, request, false, session);
                if (jsonResponse.APIResponse != null)
                    return new ShippingApiResponse<T>() { APIResponse = jsonResponse.APIResponse.Wrapped, Errors = jsonResponse.Errors, HttpStatus = jsonResponse.HttpStatus, Success = jsonResponse.Success };
                else
                    return new ShippingApiResponse<T>() { APIResponse = default(T), Errors = jsonResponse.Errors, HttpStatus = jsonResponse.HttpStatus, Success = jsonResponse.Success };
            }
        }
    }
}