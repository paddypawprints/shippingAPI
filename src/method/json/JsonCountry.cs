
/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCountry<T> : JsonWrapper<T>, ICountry where T : ICountry, new()
    {
        public JsonCountry() : base() { }

        public JsonCountry(T t) : base(t) { }

        [JsonProperty("countryCode")]
        virtual public string CountryCode
        {
            get => Wrapped.CountryCode;
            set { Wrapped.CountryCode = value; }
        }
        [JsonProperty("countryName")]
        virtual public string CountryName
        {
            get => Wrapped.CountryName;
            set { Wrapped.CountryName = value; }
        }
    }


}