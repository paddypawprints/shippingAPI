using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ShippingApiAttribute : Attribute
    {
        public string Name { get;set; }
        public string Format { get; set; }
        public ShippingApiAttribute(string name)
        {
            Name = name;
        }

    }

    public class ShippingApiHeaderAttribute : ShippingApiAttribute
    {
        public bool OmitIfEmpty { get; set; }
        public ShippingApiHeaderAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }
    public class ShippingApiQueryAttribute : ShippingApiAttribute
    {
        public bool OmitIfEmpty { get; set; }
        public ShippingApiQueryAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }

    public class ShippingApiResourceAttribute : ShippingApiAttribute
    {
        public bool AddId { get; set; }
        public string PathSuffix { get; set; }
        public ShippingApiResourceAttribute(string name, bool addId = true, string pathSuffix = null) : base(name)
        {
            AddId = addId;
            PathSuffix = PathSuffix;
        }

    }
}