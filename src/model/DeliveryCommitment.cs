
namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class DeliveryCommitment : IDeliveryCommitment
    {
        virtual public string MinEstimatedNumberOfDays { get; set;}
        virtual public string MaxEstimatedNumberOfDays { get; set;}
        virtual public string EstimatedDeliveryDateTime { get;set;}
        virtual public string Guarantee {get;set;}
        virtual public string AdditionalDetails {get;set;}        
    }

}