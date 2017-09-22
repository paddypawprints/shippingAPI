using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonManifest<T> : JsonWrapper<T>, IShippingApiRequest, IManifest where T : IManifest, new()
    {
        public JsonManifest() : base() { }

        public JsonManifest(T t) : base(t) { }

        public string RecordingSuffix => "";
        public string RecordingFullPath(string resource, Session session)
        {
            return ShippingApiRequest.RecordingFullPath(this, resource, session);
        }

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

        public bool ShouldSerializeManifestId() => false;

        [JsonProperty("ManifestId")]
        public string ManifestId
        {
            get => Wrapped.ManifestId;
            set { Wrapped.ManifestId = value; }
        }

        public bool ShouldSerializeManifestTrackingNumber() => false;

        [JsonProperty("manifestTrackingNumber")]
        public string ManifestTrackingNumber
        {
            get => Wrapped.ManifestTrackingNumber;
            set { Wrapped.ManifestTrackingNumber = value; }
        }

        public bool ShouldSerializeDocuments() => false;

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

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.AddRequestResource(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }
        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }
        public void SerializeBody(StreamWriter writer, Session session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }


        public string ContentType => "application/json";

        [ShippingApiHeaderAttribute("Bearer")]
        public StringBuilder Authorization { get; set; }

    }
}