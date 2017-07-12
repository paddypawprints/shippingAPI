using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using com.pb.shippingapi.model;


namespace com.pb.shippingapi
{

    internal class TokenRequest
    {
       [JsonProperty(PropertyName="grant_type")]
        public string GrantType {get;set;}

        [ShippingAPIHeader("Authorization")]
        public byte[] BasicAuth {get;set;}
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
        public static async Task<ShippingAPIResponse<Token>> token(string APIKey, byte[] APISecret )
        {
            var request = new TokenRequest() { GrantType = "client_credentials", ContentType = "application/x-www-form-urlencoded"};
            return await WebMethod.Post<Token, TokenRequest>(request );
        }
    }
}