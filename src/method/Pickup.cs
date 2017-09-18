using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;


namespace PitneyBowes.Developer.ShippingApi.Method
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PickupCancelRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiResource("pickups", AddId = true, PathSuffix = "/cancel")]
        public string PickupId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public static class PickupMethods
    {
        public async static Task<ShippingApiResponse<T>> Schedule<T>(T request, ShippingApi.Session session = null) where T : IPickup, new()
        {
            var scheduleRequest = new JsonPickup<T>(request);
            if (session == null) session = ShippingApi.DefaultSession;
            scheduleRequest.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, JsonPickup<T>>("/shippingservices/v1/pickups/schedule", scheduleRequest, session);
        }
        public async static Task<ShippingApiResponse<PickupCancelRequest>> CancelPickup(PickupCancelRequest request, ShippingApi.Session session = null)
        {
            request.Status = "Success";
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<PickupCancelRequest, PickupCancelRequest>("/shippingservices/v1/pickups", request, session);
        }
    }

}
