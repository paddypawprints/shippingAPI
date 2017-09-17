using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi.Method
{

    public class ReprintManifestRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiHeaderAttribute("X-PB-TransactionId")]
        public string TransactionId { get; set; }

        [ShippingApiQuery("originalTransactionId")]
        public string OriginalTransactionId { get; set; }
    }

    public class RetryManifestRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json";  }

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiResource("manifest", AddId = true)]
        public string ManifestId { get; set; }
    }
    public static class ManifestMethods
    {
        public async static Task<ShippingApiResponse<T>> Create<T>(T request, ShippingApi.Session session = null) where T : IManifest, new()
        {
            var manifestRequest = new JsonManifest<T>(request);
            if (session == null) session = ShippingApi.DefaultSession;
            manifestRequest.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, JsonManifest<T>>("/shippingservices/v1/manifests", manifestRequest, session);
        }
        public async static Task<ShippingApiResponse<T>> Reprint<T>(ReprintManifestRequest request, ShippingApi.Session session = null) where T : IManifest, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, ReprintManifestRequest> ("/shippingservices/v1", request, session);
        }
        public async static Task<ShippingApiResponse<T>> Retry<T>(RetryManifestRequest request, ShippingApi.Session session = null) where T : IManifest, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, RetryManifestRequest>("/shippingservices/v1", request, session);
        }

    }
}
