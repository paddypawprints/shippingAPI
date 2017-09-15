using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonPaymentInfo<T> : JsonWrapper<T>, IPaymentInfo where T : IPaymentInfo, new()
    {
        public JsonPaymentInfo() : base() { }
        public JsonPaymentInfo(T t) : base(t) { }

        [JsonProperty("paymentType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentType PaymentType
        {
            get => Wrapped.PaymentType;
            set { Wrapped.PaymentType = value; }
        }
        [JsonProperty("paymentMethod")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentMethod PaymentMethod
        {
            get => Wrapped.PaymentMethod;
            set { Wrapped.PaymentMethod = value; }
        }
        [JsonProperty("ppPaymentDetails")]
        public IPpPaymentDetails PpPaymentDetails
        {
            get => Wrapped.PpPaymentDetails;
            set { Wrapped.PpPaymentDetails = value; }
        }
        [JsonProperty("ccPaymentDetails")]
        public ICcPaymentDetails CcPaymentDetails
        {
            get => Wrapped.CcPaymentDetails;
            set { Wrapped.CcPaymentDetails = value; }
        }
    }
}
