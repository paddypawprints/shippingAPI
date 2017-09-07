
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonDeliveryCommitment<T> : JsonWrapper<T>, IDeliveryCommitment where T:IDeliveryCommitment, new()
    {
        public JsonDeliveryCommitment() : base() { }

        public JsonDeliveryCommitment(T t) : base(t) { }

        [JsonProperty("minEstimatedNumberOfDays")]
        virtual public string MinEstimatedNumberOfDays
        {
            get => Wrapped.MinEstimatedNumberOfDays;
            set { Wrapped.MinEstimatedNumberOfDays = value; } 
        }
        [JsonProperty("maxEstimatedNumberOfDays")]
        public string MaxEstimatedNumberOfDays
        {
            get => Wrapped.MaxEstimatedNumberOfDays;
            set { Wrapped.MaxEstimatedNumberOfDays = value; }
        }
        [JsonProperty("estimatedDeliveryDateTime")]
        public string EstimatedDeliveryDateTime
        {
            get => Wrapped.EstimatedDeliveryDateTime;
            set { Wrapped.EstimatedDeliveryDateTime = value; }
        }
        [JsonProperty("guarantee")]
        public string Guarantee
        {
            get => Wrapped.Guarantee;
            set { Wrapped.Guarantee = value; }
        }
        [JsonProperty("additionalDetails")]
        public string AdditionalDetails
        {
            get => Wrapped.AdditionalDetails;
            set { Wrapped.AdditionalDetails = value; }
        }
        
    }

}