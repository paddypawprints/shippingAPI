using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.pb.shippingapi.model
{
    public class Manifest
    {
        [JsonProperty("carrier")]
        public Carrier Carrier { get;set;}
        [JsonProperty("submissionDate")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset SubmissionDate {get;set;}
        [JsonProperty("fromAddress")]
        public Address FromAddress {get;set;}
        [JsonProperty("inductionPostalCode")]
        public string InductionPostalCode {get;set;}
        [JsonProperty("parcelTrackingNumbers")]
        public string[] ParcelTrackingNumbers {get;set;}
        [JsonProperty("parameters")]
        public Parameter[] Parameters;
        [JsonProperty("ManifestId")]
        public string ManifestId {get;set;}
        [JsonProperty("manifestTrackingNumber")]
        public string ManifestTrackingNumber {get;set;}
        [JsonProperty("documents")]
        public Document[] Documents {get;set;}

    }
}