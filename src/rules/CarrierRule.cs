namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class CarrierRule : IRateRule
    {
        public Carrier Carrier { get; set; }
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }
        public IndexedList<Services, ServiceRule> ServiceRules { get; set; }

        public void Accept(IRateRuleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
