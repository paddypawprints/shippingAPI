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
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCustomsInfo<T> : JsonWrapper<T>, ICustomsInfo where T:ICustomsInfo, new()
    {
        public JsonCustomsInfo() : base() { }

        public JsonCustomsInfo(T t) : base(t) { }

        [JsonProperty("reasonForExport")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReasonForExport ReasonForExport
        {
            get => Wrapped.ReasonForExport;
            set { Wrapped.ReasonForExport = value; }
        }
        [JsonProperty("reasonForExportExplanation")]
        public string reasonForExportExplanation
        {
            get => Wrapped.reasonForExportExplanation;
            set { Wrapped.reasonForExportExplanation = value; }
        }
        [JsonProperty("comments")]
        public string Comments
        {
            get => Wrapped.Comments;
            set { Wrapped.Comments = value; }
        }
        [JsonProperty("invoiceNumber")]
        public string InvoiceNumber
        { 
            get => Wrapped.InvoiceNumber;
            set { Wrapped.InvoiceNumber = value; }
        }
        [JsonProperty("importerCustomsReference")]
        public string ImporterCustomsReference
        {
            get => Wrapped.ImporterCustomsReference;
            set { Wrapped.ImporterCustomsReference = value; }
        }
        [JsonProperty("insuredNumber")]
        public string InsuredNumber
        {
            get => Wrapped.InsuredNumber;
            set { Wrapped.InsuredNumber = value; }
        }
        [JsonProperty("insuredAmount")]
        public decimal InsuredAmount
        {
            get => Wrapped.InsuredAmount;
            set { Wrapped.InsuredAmount = value; }
        }
        [JsonProperty("sdrValue")]
        public decimal SdrValue
        {
            get => Wrapped.SdrValue;
            set { Wrapped.SdrValue = value; }
        }
        [JsonProperty("EELPFC")]
        public string EELPFC
        {
            get => Wrapped.EELPFC;
            set { Wrapped.EELPFC = value; }
        }
        [JsonProperty("fromCustomsReference")]
        public string FromCustomsReference
        {
            get => Wrapped.FromCustomsReference;
            set { Wrapped.FromCustomsReference = value; }
        }
        [JsonProperty("customsDeclaredValue")]
        public decimal CustomsDeclaredValue
        {
            get => Wrapped.CustomsDeclaredValue;
            set { Wrapped.CustomsDeclaredValue = value; }
        }
        [JsonProperty("currencyCode")]
        public string CurrencyCode
        {
            get => Wrapped.CurrencyCode;
            set { Wrapped.CurrencyCode = value; }
        }
        [JsonProperty("licenseNumber")]
        public string LicenseNumber
        {
            get => Wrapped.LicenseNumber;
            set { Wrapped.LicenseNumber = value; }
        }
        [JsonProperty("certificateNumber")]
        public string CertificateNumber
        {
            get => Wrapped.CertificateNumber;
            set { Wrapped.CertificateNumber = value; }
        }
    }
}