using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace com.pb.shippingapi.model
{
    public class Manifest
    {
        [JsonProperty("carrier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier { get;set;}
        [JsonProperty("submissionDate")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset SubmissionDate {get;set;}
        [JsonProperty("fromAddress")]
        public Address FromAddress {get;set;}
        [JsonProperty("inductionPostalCode")]
        public string InductionPostalCode {get;set;}
        [JsonProperty("parcelTrackingNumbers")]
        public IEnumerable<string> ParcelTrackingNumbers {get;set;}
        [JsonProperty("parameters")]
        public IEnumerable<Parameter> Parameters;
        [JsonProperty("ManifestId")]
        public string ManifestId {get;set;}
        [JsonProperty("manifestTrackingNumber")]
        public string ManifestTrackingNumber {get;set;}
        [JsonProperty("documents")]
        public IEnumerable<Document> Documents {get;set;}

    }
}