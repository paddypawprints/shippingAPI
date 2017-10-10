namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public interface IRateRuleVisitor
    {
        void Visit(CarrierRule carrierRule);
        void Visit(ServiceRule serviceRule);
        void Visit(ParcelTypeRule parcelRule);
        void Visit(SpecialServicesRule specialServicesRule);
    }
}
