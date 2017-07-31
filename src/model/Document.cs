
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace com.pb.shippingapi.model
{
    public class Document
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public DocumentType Type { get; set;}
        [JsonProperty("size")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Size Size { get; set;}
        [JsonProperty("fileFormat")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FileFormat FileFormat { get;set;}
        [JsonProperty("contentType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentType ContentType {get;set;}
        [JsonProperty("printDialogOption")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PrintDialogOption printDialogOption {get;set;}
        [JsonProperty("contents")]
        public string Contents {get;set;}
        [JsonProperty("pages")]
        public IEnumerable<string> Pages {get;set;}
        
    }
}