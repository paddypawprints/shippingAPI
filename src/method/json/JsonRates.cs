using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonRates<T> : JsonWrapper<T>, IShippingApiRequest, IRates where T:IRates, new()
    {
        public JsonRates() : base() { }

        public JsonRates(T t) : base(t) { }

        [JsonProperty("carrier", Order =0)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier
        {
            get => Wrapped.Carrier;
            set { Wrapped.Carrier = value; }
        }
        [JsonProperty("serviceId", Order = 4)]
        [JsonConverter(typeof(StringEnumConverter))]
        public USPSServices ServiceId
        {
            get => Wrapped.ServiceId;
            set { Wrapped.ServiceId = value; }
        }
        [JsonProperty("parcelType", Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public USPSParcelType ParcelType
        {
            get => Wrapped.ParcelType;
            set { Wrapped.ParcelType = value; }
        }
        [JsonProperty("specialServices", Order = 3)]
        public IEnumerable<ISpecialServices> SpecialServices
        {
            get => Wrapped.SpecialServices;
            set { Wrapped.SpecialServices = value; }
        }

        public ISpecialServices AddSpecialservices(ISpecialServices s)
        {
            return Wrapped.AddSpecialservices(s);
        }

        [JsonProperty("inductionPostalCode", Order = 2)]
        public string InductionPostalCode
        {
            get => Wrapped.InductionPostalCode;
            set { Wrapped.InductionPostalCode = value; }
        }
        [JsonProperty("dimensionalWeight")]
        public IParcelWeight DimensionalWeight
        {
            get => Wrapped.DimensionalWeight;
            set { Wrapped.DimensionalWeight = value; }
        }

        public bool ShouldSerializeBaseCharge() => false;
        
        [JsonProperty("baseCharge")]
        public decimal BaseCharge
        {
            get =>Wrapped.BaseCharge;
            set { Wrapped.BaseCharge = value; }
        }

        public bool ShouldSerializeTotalCarrierCharge() => false;

        [JsonProperty("totalCarrierCharge")]
        virtual public decimal TotalCarrierCharge
        {
            get => Wrapped.TotalCarrierCharge;
            set { Wrapped.TotalCarrierCharge = value; }
        }

        public bool ShouldSerializeAlternateBaseCharge() => false;
        [JsonProperty("alternateBaseCharge")]
        public decimal AlternateBaseCharge
        {
            get => Wrapped.AlternateBaseCharge;
            set { Wrapped.AlternateBaseCharge = value; }
        }

        public bool ShouldSerializeAlternateTotalCharge() => false;

        [JsonProperty("alternateTotalCharge")]
        public decimal AlternateTotalCharge
        {
            get => Wrapped.AlternateTotalCharge;
            set { Wrapped.AlternateTotalCharge = value; }
        }
        [JsonProperty("deliveryCommitment")]
        public IDeliveryCommitment DeliveryCommitment
        {
            get => Wrapped.DeliveryCommitment;
            set { Wrapped.DeliveryCommitment = value; }
        }
        [JsonProperty("currencyCode")]
        public string CurrencyCode
        {
            get => Wrapped.CurrencyCode;
            set { Wrapped.CurrencyCode = value; }
        }
        public bool ShouldSerializeDestinationZone() => false;

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.AddRequestResource(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }

        public IEnumerable<Tuple<ShippingAPIHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ShippingApi.Session session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }

        [JsonProperty("destinationZone")]
        public int DestinationZone
        {
            get => Wrapped.DestinationZone;
            set { Wrapped.DestinationZone = value; }
        }

        public string ContentType => "application/json";

        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
    }
}