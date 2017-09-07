namespace PitneyBowes.Developer.ShippingApi
{
    public interface IDeliveryCommitment
    {
        string MinEstimatedNumberOfDays { get; set; }
        string MaxEstimatedNumberOfDays { get; set; }
        string EstimatedDeliveryDateTime { get; set; }
        string Guarantee { get; set; }
        string AdditionalDetails { get; set; }
     }

}