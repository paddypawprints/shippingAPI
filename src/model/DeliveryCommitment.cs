
using Newtonsoft.Json;

namespace com.pb.shippingapi.model
{
    public class DeliveryCommitment
    {
        [JsonProperty("minEstimatedNumberOfDays")]
        public string MinEstimatedNumberOfDays { get; set;}
        [JsonProperty("maxEstimatedNumberOfDays")]
        public string MaxEstimatedNumberOfDays { get; set;}
        [JsonProperty("estimatedDeliveryDateTime")]
        public string EstimatedDeliveryDateTime { get;set;}
        [JsonProperty("guarantee")]
        public string Guarantee {get;set;}
        [JsonProperty("additionalDetails")]
        public string AdditionalDetails {get;set;}
        
    }
}