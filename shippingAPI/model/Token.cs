 using System;
 using Newtonsoft.Json;
 using Newtonsoft.Json.Converters; 
 
 
 namespace com.pb.shippingapi.model
 {
    public class Token
    {
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken {get;set;}
        [JsonProperty(PropertyName="tokenType")]
        public string TokenType {get;set;}
        [JsonProperty(PropertyName="issuedAt")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))] // TODO: probably need to write my own
        public DateTimeOffset IssuedAt { get;set;}
        [JsonProperty(PropertyName="expiresIn")]
        public long ExpiresIn {get;set;}
        [JsonProperty(PropertyName="clientID")]
        public string ClientID { get;set;}
        [JsonProperty(PropertyName="org")]
        public string Org { get;set;}
    }
 }