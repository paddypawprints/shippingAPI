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
    public class JsonParameter<T> : JsonWrapper<T>, IParameter where T:IParameter, new()
    {
        
        public JsonParameter() : base() { }

        public JsonParameter(T t) : base(t) { }

        public JsonParameter( string name, string value) : base()
        {
            Wrapped.Name = name;
            Wrapped.Value = value;
        }

        [JsonProperty("name")]
        public string Name
        {
            get =>Wrapped.Name;
            set { Wrapped.Name = value; }
        }
        [JsonProperty("value")]
        public string Value
        {
            get => Wrapped.Value;
            set { Wrapped.Value = value; }
        }
    }

}