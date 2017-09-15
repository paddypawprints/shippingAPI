using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonPpPaymentDetails<T> : JsonWrapper<T>, IPpPaymentDetails where T : IPpPaymentDetails, new()
    {
        public JsonPpPaymentDetails() : base() { }
        public JsonPpPaymentDetails(T t) : base(t) { }

        [JsonProperty("encryptedTIN")]
        public string EncryptedTIN
        {
            get => Wrapped.EncryptedTIN;
            set { Wrapped.EncryptedTIN = value; }
        }
        [JsonProperty("encryptedBPN")]
        public string EncryptedBPN
        {
            get => Wrapped.EncryptedBPN;
            set { Wrapped.EncryptedBPN = value; }
        }
    }
}
