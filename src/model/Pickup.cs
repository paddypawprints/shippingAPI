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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Pickup : IPickup
    {
        virtual public string TransactionId { get; set; }
        virtual public IAddress PickupAddress{get; set;}
        virtual public Carrier Carrier{get; set;}
        virtual public IEnumerable<IPickupCount> PickupSummary{get; set;}
        virtual public string Reference{get; set;}
        virtual public PackageLocation PackageLocation{get; set;}
        virtual public string SpecialInstructions{get; set;}
        virtual public DateTime PickupDate { get; set; }
        virtual public string PickupConfirmationNumber { get; set; }
        virtual public string PickupId { get; set; }

        public void AddPickupCount(IPickupCount p)
        {
            ModelHelper.AddToEnumerable<IPickupCount, IPickupCount>(p, () => PickupSummary, (x) => PickupSummary = x);
        }
    }
}
