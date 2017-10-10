/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

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
        public async static Task<ShippingApiResponse<T>> Schedule<T>(T request, ISession session = null) where T : IPickup, new()
        {
            var scheduleRequest = new JsonPickup<T>(request);
            return await WebMethod.Post<T, JsonPickup<T>>("/shippingservices/v1/pickups/schedule", scheduleRequest, session);
        }
        public async static Task<ShippingApiResponse<PickupCancelRequest>> CancelPickup(PickupCancelRequest request, ISession session = null)
        {
            request.Status = "Success";
            return await WebMethod.Post<PickupCancelRequest, PickupCancelRequest>("/shippingservices/v1/pickups", request, session);
        }
    }

}
