using System;
using System.Threading.Tasks;
using Newtonsoft.Json;


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

    internal class TokenResponse
    {
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken {get;set;}
        [JsonProperty(PropertyName="tokenType")]
        public string TokenType {get;set;}
        [JsonProperty(PropertyName="issuedAt")]
        public long IssuedAt { get;set;}
        [JsonProperty(PropertyName="expiresIn")]
        public long ExpiresIn {get;set;}
        [JsonProperty(PropertyName="clientID")]
        public string ClientID { get;set;}
        [JsonProperty(PropertyName="org")]
        public string Org { get;set;}
    }


    public class ShippingAPIAccessToken
    {
        
        public static explicit operator String( ShippingAPIAccessToken t )
        {
            return t.AccessToken;
        }
        internal TokenResponse _tokenResponse {get;set;}

        public string AccessToken 
        { 
            get { return _tokenResponse.AccessToken; }
        }
        public DateTimeOffset IssuedAtDt()
        {
            return DateTimeOffset.FromUnixTimeSeconds(_tokenResponse.IssuedAt);
        }
        public bool Expired()
        {
            return DateTimeOffset.Now >= IssuedAtDt().AddSeconds(_tokenResponse.ExpiresIn);
        }

        public static async Task<ShippingAPIResponse<ShippingAPIAccessToken>> Token(string APIKey, byte[] APISecret )
        {
            var request = new TokenRequest() { GrantType = "client_credentials", ContentType = "application/x-www-form-urlencoded"};
            var response =  await WebMethod.Post<TokenResponse, TokenRequest>(request );
            var v = new ShippingAPIAccessToken();
            v._tokenResponse = response.Res;
            var w = new ShippingAPIResponse<ShippingAPIAccessToken>();
            w.Res = v;
            w.HttpStatus = response.HttpStatus;
            return w;
        }
    }
}