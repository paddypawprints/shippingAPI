using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    public class TrackingRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingAPIResource("tracking", AddId = true)]
        public string TrackingNumber { get; set; }

        [ShippingAPIQuery("packageIdentifierType")]
        public string PackageIdentifierType { get => "ParcelTrackingNumber"; }
    
        [ShippingAPIQuery("carrier")]
        Carrier Carrier { get; set; }
    }

    public static class TrackingMethods
    {
        public async static Task<ShippingAPIResponse<T>> Tracking<T>(TrackingRequest request, ShippingApi.Session session = null) where T : IRates, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<T, TrackingRequest>("/shippingservices/v1/", request, session);
        }
    }
}
