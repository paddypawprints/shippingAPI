
using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class Document
    {
        [JsonProperty("type")]
        public DocumentType Type { get; set;}
        [JsonProperty("size")]
        public Size Size { get; set;}
        [JsonProperty("fileFormat")]
        public FileFormat FileFormat { get;set;}
        [JsonProperty("contentType")]
        public ContentType ContentType {get;set;}
        [JsonProperty("printDialogOption")]
        public PrintDialogOption printDialogOption {get;set;}
        [JsonProperty("contents")]
        public ContentType Contents {get;set;}
        [JsonProperty("pages")]
        public string[] Pages {get;set;}
        
    }
}