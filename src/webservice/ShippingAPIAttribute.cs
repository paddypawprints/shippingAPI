using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ShippingAPIAttribute : Attribute
    {
        public string Name { get;set; }
        public string Format { get; set; }
        public ShippingAPIAttribute(string name)
        {
            Name = name;
        }

    }

    public class ShippingAPIHeaderAttribute : ShippingAPIAttribute
    {
        public bool OmitIfEmpty { get; set; }
        public ShippingAPIHeaderAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }
    public class ShippingAPIQueryAttribute : ShippingAPIAttribute
    {
        public bool OmitIfEmpty { get; set; }
        public ShippingAPIQueryAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }

    public class ShippingAPIResourceAttribute : ShippingAPIAttribute
    {
        public bool AddId { get; set; }
        public string PathSuffix { get; set; }
        public ShippingAPIResourceAttribute(string name, bool addId = true, string pathSuffix = null) : base(name)
        {
            AddId = addId;
            PathSuffix = PathSuffix;
        }

    }
}