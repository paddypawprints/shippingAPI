﻿/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;


namespace PitneyBowes.Developer.ShippingApi.Method
{


    [JsonObject(MemberSerialization.OptIn)]
    public class AddressSuggestions
    {
        [JsonProperty("suggestionType")]
        public string SuggestionType { get; set; } //TODO - figure out possible values and convert to enum
        [JsonProperty("address")]
        public IEnumerable<IAddress> Addresses { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class VerifySuggestResponse
    {
        [JsonProperty("address")]
        public IAddress Address { get; set; }
        [JsonProperty("suggestions")]
        public AddressSuggestions Suggestions { get; set; }
    }

    public static class AddressessMethods
    {
        public async static Task<ShippingApiResponse<T>> VerifyAddress<T>(T request, ISession session = null) where T : IAddress, new()
        {
            var verifyRequest = new JsonAddress<T>(request);
            return await WebMethod.Post<T, JsonAddress<T>>("/shippingservices/v1/addresses/verify", verifyRequest, session);
        }
        public async static Task<ShippingApiResponse<VerifySuggestResponse>> VerifySuggestAddress<T>(T request, ISession session = null) where T : IAddress, new()
        {
            var verifyRequest = new JsonAddress<T>(request) { Suggest = true };
            return await WebMethod.Post<VerifySuggestResponse, JsonAddress<T>>("/shippingservices/v1/addresses/verify-suggest", verifyRequest, session);
        }
    }
}
