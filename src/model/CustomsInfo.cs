
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.pb.shippingapi.model
{
    public class CustomsInfo
    {
        [JsonProperty("reasonForExport")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReasonForExport ReasonForExport { get; set;}
        [JsonProperty("reasonForExportExplanation")]
        public string reasonForExportExplanation { get; set;}
        [JsonProperty("comments")]
        public string Comments { get;set;}
        [JsonProperty("invoiceNumber")]
        public string InvoiceNumber {get;set;}
        [JsonProperty("importerCustomsReference")]
        public string ImporterCustomsReference {get;set;}
        [JsonProperty("insuredNumber")]
        public string InsuredNumber {get;set;}
        [JsonProperty("insuredAmount")]
        public decimal InsuredAmount {get;set;}
        [JsonProperty("sdrValue")]
        public decimal SdrValue { get;set;}
        [JsonProperty("EELPFC")]
        public string EELPFC {get;set;}
        [JsonProperty("fromCustomsReference")]
        public string FromCustomsReference {get;set;}
        [JsonProperty("customsDeclaredValue")]
        public decimal CustomsDeclaredValue { get;set;}
        [JsonProperty("currencyCode")]
        public string CurrencyCode {get;set;}
        [JsonProperty("licenseNumber")]
        public string LicenseNumber {get;set;}
        [JsonProperty("certificateNumber")]
        public string CertificateNumber {get;set;}
    }
}