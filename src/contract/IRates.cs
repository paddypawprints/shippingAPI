﻿/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using Newtonsoft.Json;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IRates
    {
        Carrier Carrier { get; set; }
        Services ServiceId { get; set; }
        ParcelType ParcelType { get; set; }
        IEnumerable<ISpecialServices> SpecialServices { get; set; }
        ISpecialServices AddSpecialservices(ISpecialServices s);
        string InductionPostalCode { get; set; }
        IParcelWeight DimensionalWeight { get; set; }
        decimal BaseCharge { get; set; }
        decimal TotalCarrierCharge { get; set; }
        decimal AlternateBaseCharge { get; set; }
        decimal AlternateTotalCharge { get; set; }
        IDeliveryCommitment DeliveryCommitment { get; set; }
        string CurrencyCode { get; set; }
        int DestinationZone { get; set; }
    }
}