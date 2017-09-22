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

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiResource("tracking", AddId = true)]
        public string TrackingNumber { get; set; }

        [ShippingApiQuery("packageIdentifierType")]
        public string PackageIdentifierType { get => "ParcelTrackingNumber"; }
    
        [ShippingApiQuery("carrier")]
        Carrier Carrier { get; set; }
    }

    public static class TrackingMethods
    {
        public async static Task<ShippingApiResponse<T>> Tracking<T>(TrackingRequest request, Session session = null) where T : IRates, new()
        {
            if (session == null) session = SessionDefaults.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<T, TrackingRequest>("/shippingservices/v1/", request, session);
        }
    }
}
