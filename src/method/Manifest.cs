using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi.Method
{

    public class RetryManifestRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiHeaderAttribute("X-PB-TransactionId")]
        public string TransactionId { get; set; }

        [ShippingApiQuery("originalTransactionId")]
        public string OriginalTransactionId { get; set; }
    }

    public class ReprintManifestRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json";  }

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiResource("manifest", AddId = true)]
        public string ManifestId { get; set; }
    }
    public static class ManifestMethods
    {
        public async static Task<ShippingApiResponse<T>> Create<T>(T request, ISession session = null) where T : IManifest, new()
        {
            var manifestRequest = new JsonManifest<T>(request);
            return await WebMethod.Post<T, JsonManifest<T>>("/shippingservices/v1/manifests", manifestRequest, session);
        }
        public async static Task<ShippingApiResponse<T>> Reprint<T>(ReprintManifestRequest request, ISession session = null) where T : IManifest, new()
        {
            return await WebMethod.Post<T, ReprintManifestRequest> ("/shippingservices/v1", request, session);
        }
        public async static Task<ShippingApiResponse<T>> Retry<T>(RetryManifestRequest request, ISession session = null) where T : IManifest, new()
        {
            return await WebMethod.Post<T, RetryManifestRequest>("/shippingservices/v1", request, session);
        }

    }
}
