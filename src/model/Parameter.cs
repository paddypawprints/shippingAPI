
namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Parameter : IParameter
    {
        public Parameter()
        {
        }
        public Parameter( string name, string value)
        {
            Name = name;
            Value = value;
        }
        virtual public string Name {get;set;}
        virtual public string Value {get;set;}
    }

}