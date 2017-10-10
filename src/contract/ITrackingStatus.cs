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

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ITrackingStatus
    {
        string PackageCount { get; set; }
        string Carrier { get; set; }
        string TrackingNumber { get; set; }
        string ReferenceNumber { get; set; }
        TrackingStatusCode Status { get; set; }
        DateTimeOffset UpdatedDateTime { get; set; }
        DateTimeOffset ShipDateTime { get; set; }
        DateTimeOffset EstimatedDeliveryDateTime { get; set; }
        DateTimeOffset DeliveryDateTime { get; set; }
        string DeliveryLocation { get; set; }
        string DeliveryLocationDescription { get; set; }
        string SignedBy { get; set; }
        Decimal Weight { get; set; }
        UnitOfWeight? WeightOUM { get; set; }
        string ReattemptDate { get; set; }
        DateTime ReattemptTime { get; set; }
        IAddress DestinationAddress { get; set; }
        IAddress SenderAddress { get; set; }
        IEnumerable<ITrackingEvent> ScanDetailsList { get; set; }
    }
}
