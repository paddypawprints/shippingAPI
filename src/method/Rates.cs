using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi
{

    [JsonObject(MemberSerialization.OptIn)]
    public class RatesRequest<T> : JsonWrapper<T>, IRates, IShippingApiRequest where T : IRates, new()
    {
        public RatesRequest() : base() { }

        public RatesRequest(T t) : base(t) { }

        [JsonProperty("carrier", Order = 0)]
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
            get => Wrapped.BaseCharge;
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

        [JsonProperty("destinationZone")]
        public int DestinationZone
        {
            get => Wrapped.DestinationZone;
            set { Wrapped.DestinationZone = value; }
        }
        public string ContentType { get => "application/json"; set => throw new NotImplementedException(); }
        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
    }

    public static class RatesMethods
    {
        public async static Task<ShippingAPIResponse<T>> Rates<T>(T request, ShippingApi.Session session = null) where T : IRates, new()
        {
            var ratesRequest = new RatesRequest<T>(request);
            if (session == null) session = ShippingApi.DefaultSession;
            ratesRequest.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, RatesRequest<T>>("/shippingservices/v1/rates", ratesRequest, session);
        }

    }
}
