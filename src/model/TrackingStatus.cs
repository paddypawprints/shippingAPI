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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class TrackingStatus : ITrackingStatus
    {
        virtual public string PackageCount{get; set;}
        virtual public string Carrier{get; set;}
        virtual public string TrackingNumber{get; set;}
        virtual public string ReferenceNumber{get; set;}
        virtual public TrackingStatusCode Status{get; set;}
        virtual public DateTimeOffset UpdatedDateTime{get; set;}
        virtual public DateTimeOffset ShipDateTime{get; set;}
        virtual public DateTimeOffset EstimatedDeliveryDateTime{get; set;}
        virtual public DateTimeOffset DeliveryDateTime{get; set;}
        virtual public string DeliveryLocation{get; set;}
        virtual public string DeliveryLocationDescription{get; set;}
        virtual public string SignedBy{get; set;}
        virtual public Decimal Weight{get; set;}
        virtual public UnitOfWeight? WeightOUM{get; set;}
        virtual public string ReattemptDate{get; set;}
        virtual public DateTime ReattemptTime{get; set;}
        virtual public IAddress DestinationAddress{get; set;}
        virtual public IAddress SenderAddress{get; set;}
        virtual public IEnumerable<ITrackingEvent> ScanDetailsList{get; set;}
    }
}
