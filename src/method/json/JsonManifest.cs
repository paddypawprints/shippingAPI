using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonManifest<T> : JsonWrapper<T>, IManifest where T : IManifest, new()
    {
        public JsonManifest() : base() { }

        public JsonManifest(T t) : base(t) { }

        [JsonProperty("carrier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier
        {
            get => Wrapped.Carrier;
            set { Wrapped.Carrier = value; } 
        }

        [JsonProperty("submissionDate")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset SubmissionDate
        {
            get => Wrapped.SubmissionDate;
            set { Wrapped.SubmissionDate = value;  }
        }
        [JsonProperty("fromAddress")]
        public IAddress FromAddress
        { 
            get => Wrapped.FromAddress;
            set { Wrapped.FromAddress = value; }
        }
        [JsonProperty("inductionPostalCode")]
        public string InductionPostalCode
        {
            get => Wrapped.InductionPostalCode;
            set { Wrapped.InductionPostalCode = value; }
        }
        [JsonProperty("parcelTrackingNumbers")]
        public IEnumerable<string> ParcelTrackingNumbers
        {
            get => Wrapped.ParcelTrackingNumbers;
            set { Wrapped.ParcelTrackingNumbers = value; }
        }
        public void AddParcelTrackingNumber(string s)
        {
            Wrapped.AddParcelTrackingNumber(s);
        }
        [JsonProperty("parameters")]
        public IEnumerable<IParameter> Parameters
        {
            get => Wrapped.Parameters;
            set { Wrapped.Parameters = value; }
        }
        public IParameter AddParameter( IParameter p)
        {
            return Wrapped.AddParameter(p);
        }
        [JsonProperty("ManifestId")]
        public string ManifestId
        {
            get => Wrapped.ManifestId;
            set { Wrapped.ManifestId = value; }
        }
        [JsonProperty("manifestTrackingNumber")]
        public string ManifestTrackingNumber
        {
            get => Wrapped.ManifestTrackingNumber;
            set { Wrapped.ManifestTrackingNumber = value; }
        }
        [JsonProperty("documents")]
        public IEnumerable<IDocument> Documents
        {
            get => Wrapped.Documents;
            set { Wrapped.Documents = value; }
        }
        public IDocument AddDocument( IDocument d)
        {
            return Wrapped.AddDocument(d);
        }
    }
}