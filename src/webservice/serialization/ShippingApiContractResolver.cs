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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ShippingApiContractResolver : DefaultContractResolver
    {

        public static readonly ShippingApiContractResolver Instance = new ShippingApiContractResolver();
        public SerializationRegistry Registry { get; set; }

        protected override JsonContract CreateContract(Type objectType )
        {
            // this will only be called once and then cached 
            JsonContract contract = base.CreateContract(objectType);
            var c = Registry.GetConverter(objectType);
            if (c != null)
                contract.Converter = c;
            return contract;
        }

    }

}