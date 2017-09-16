using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonSpecialServices<T> : JsonWrapper<T>, ISpecialServices where T:ISpecialServices,new()
    {
        public JsonSpecialServices() : base() { }

        public JsonSpecialServices(T t) : base(t) { }

        public JsonSpecialServices(SpecialServiceCodes c) : base()
        {
            Wrapped.SpecialServiceId = c;
        }

        [JsonProperty("specialServiceId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpecialServiceCodes SpecialServiceId
        {
            get => Wrapped.SpecialServiceId;
            set { Wrapped.SpecialServiceId = value; }
        }
        [JsonProperty("inputParameters")]
        public IEnumerable<IParameter> InputParameters
        {
            get => Wrapped.InputParameters;
            set { Wrapped.InputParameters = value; }
        }
        public void AddParameter(IParameter p)
        {
            Wrapped.AddParameter(p);
        }
        public bool ShouldSerializeFee() => Fee > 0.0M;
        [JsonProperty("fee")]
        public decimal Fee
        {
            get => Wrapped.Fee;
            set { Wrapped.Fee = value; }
        }

    }

}