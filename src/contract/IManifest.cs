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
    public interface IManifest
    {
        string TransactionId { get; set; }
        Carrier Carrier { get; set; }
        DateTimeOffset SubmissionDate { get; set; }
        IAddress FromAddress { get; set; }
        string InductionPostalCode { get; set; }
        IEnumerable<string> ParcelTrackingNumbers { get; set; }
        void AddParcelTrackingNumber(string t);
        IEnumerable<IParameter> Parameters { get; set; }
        IParameter AddParameter(IParameter p);
        string ManifestId { get; set; }
        string ManifestTrackingNumber { get; set; }
        IEnumerable<IDocument> Documents { get; set; }
        IDocument AddDocument(IDocument d);
    }
}