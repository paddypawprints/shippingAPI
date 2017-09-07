 using System;
 using Newtonsoft.Json;
 using Newtonsoft.Json.Converters; 
 
 
 namespace PitneyBowes.Developer.ShippingApi.Json
 {
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonToken<T> : JsonWrapper<T>, IToken where T:IToken, new()
    {
        public JsonToken() : base() { }

        public JsonToken(T t) : base(t) { }

        [JsonProperty(PropertyName="access_token")]
        public string AccessToken
        { 
            get => Wrapped.AccessToken;
            set { Wrapped.AccessToken = value; }
        }
        [JsonProperty(PropertyName="tokenType")]
        public string TokenType
        {
            get => Wrapped.TokenType;
            set { Wrapped.TokenType = value; }
        }
        [JsonProperty(PropertyName="issuedAt")]
        [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
        public DateTimeOffset IssuedAt
        {
            get => Wrapped.IssuedAt;
            set { Wrapped.IssuedAt = value; }
        }
        [JsonProperty(PropertyName="expiresIn")]
        public long ExpiresIn
        {
            get => Wrapped.ExpiresIn;
            set { Wrapped.ExpiresIn = value; }
        }
        [JsonProperty(PropertyName="clientID")]
        public string ClientID
        {
            get =>Wrapped.ClientID;
            set { Wrapped.ClientID = value; }
        }
        [JsonProperty(PropertyName="org")]
        public string Org
        {
            get => Wrapped.Org;
            set { Wrapped.Org = value; }
        }
    }
 }