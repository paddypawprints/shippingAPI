
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonDocument<T> : JsonWrapper<T>, IDocument where T : IDocument, new()
    {
        public JsonDocument() : base() { }

        public JsonDocument(T t) : base(t) { }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type", Order = 2)]
        public DocumentType Type
        {
            get => Wrapped.Type;
            set { Wrapped.Type = value;  }
        }
        [JsonProperty("size", Order = 0)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Size Size
        {
            get => Wrapped.Size;
            set { Wrapped.Size = value; }
        }
        [JsonProperty("fileFormat", Order = 4)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FileFormat FileFormat
        {
            get;
            set;
        }
        [JsonProperty("contentType", Order = 3)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentType ContentType
        {
            get => Wrapped.ContentType;
            set { Wrapped.ContentType = value; }
        }
        [JsonProperty("printDialogOption", Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PrintDialogOption PrintDialogOption
        {
            get => Wrapped.PrintDialogOption;
            set { Wrapped.PrintDialogOption = value; }
        }
        [JsonProperty("contents")]
        virtual public string Contents
        {
            get => Wrapped.Contents;
            set { Wrapped.Contents = value; }
        }
        [JsonProperty("pages")]
        public IEnumerable<string> Pages
        {
            get => Wrapped.Pages;
            set { Wrapped.Pages = value;  }
        }
        public void AddPage(string s)
        {
            Wrapped.AddPage(s);
        }
        
    }

}