namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public interface IRateRule
    {
        void Accept(IRateRuleVisitor visitor);
    }
}
