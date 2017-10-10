﻿/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/


using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
