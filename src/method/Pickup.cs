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
        public async static Task<ShippingApiResponse<T>> Schedule<T>(T request, Session session = null) where T : IPickup, new()
        {
            var scheduleRequest = new JsonPickup<T>(request);
            return await WebMethod.Post<T, JsonPickup<T>>("/shippingservices/v1/pickups/schedule", scheduleRequest, session);
        }
        public async static Task<ShippingApiResponse<PickupCancelRequest>> CancelPickup(PickupCancelRequest request, Session session = null)
        {
            request.Status = "Success";
            return await WebMethod.Post<PickupCancelRequest, PickupCancelRequest>("/shippingservices/v1/pickups", request, session);
        }
    }

}
