using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCcPaymentDetails<T> : JsonWrapper<T>, ICcPaymentDetails where T : ICcPaymentDetails, new()
    {
        public JsonCcPaymentDetails() : base() { }
        public JsonCcPaymentDetails(T t) : base(t) { }

        [JsonProperty("ccType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CreditCardType CcType
        {
            get => Wrapped.CcType;
            set { Wrapped.CcType = value; }
        }
        [JsonProperty("ccTokenNumber")]
        public string CcTokenNumber
        {
            get => Wrapped.CcTokenNumber;
            set { Wrapped.CcTokenNumber = value; }
        }
        [JsonProperty("ccExpirationDate")]
        public string CcExpirationDate
        {
            get => Wrapped.CcExpirationDate;
            set { Wrapped.CcExpirationDate = value; }
        }
        [JsonProperty("cccvvNumber")]
        public string CccvvNumber
        {
            get => Wrapped.CccvvNumber;
            set { Wrapped.CccvvNumber = value; }
        }
        [JsonProperty("ccAddress")]
        public IAddress CcAddress
        {
            get => Wrapped.CcAddress;
            set { Wrapped.CcAddress = value; }
        }
    }
}
