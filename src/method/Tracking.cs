using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi.Method
{
    public class TrackingRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiResource("tracking", AddId = true)]
        public string TrackingNumber { get; set; }

        [ShippingApiQuery("packageIdentifierType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageIdentifierType PackageIdentifierType { get => PackageIdentifierType.TrackingNumber; }
    
        [ShippingApiQuery("carrier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier { get; set; }
    }

    public static class TrackingMethods
    {
        public async static Task<ShippingApiResponse<T>> Tracking<T>(TrackingRequest request, ISession session = null) where T : ITrackingStatus, new()
        {
            return await WebMethod.Get<T, TrackingRequest>("/shippingservices/v1/", request, session);
        }
    }
}
